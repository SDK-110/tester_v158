using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace testapp.mylib
{
    /// <summary>
    /// 单个回显串口的状态
    /// </summary>
    public class EchoPortStatus
    {
        public string Port;
        public bool Opened;
        public bool EchoStopped;   // 触发回显上限/异常后已停止回显
        public int Leftover;       // 停止时仍未凑齐一帧的残留字节数
        public string Error = "";
        public long RxBytes;
        public long TxBytes;
        public long Frames;

        public override string ToString()
        {
            return Port + ":" + (Opened ? "open" : "closed") +
                   " rx=" + RxBytes + " tx=" + TxBytes + " frames=" + Frames +
                   (Leftover > 0 ? " leftover=" + Leftover : "") +
                   (EchoStopped ? " echo_stopped" : "") +
                   (Error.Length > 0 ? " err=" + Error : "");
        }
    }

    /// <summary>
    /// RS485 回显服务参数。
    /// 数据是 ASCII 字符串, 以回车换行(CRLF)作帧结束符: 收满一行即整帧回写。
    /// 半双工总线上不能"收到即回发": 必须等一帧收齐、总线静默后再整帧转向发送,
    /// 否则会与对端驱动争用总线、截断首字节、把一帧拆成多帧。
    /// </summary>
    public class SerialEchoOptions
    {
        public int Baud = 115200;
        public int TtlMs = 30000;        // 服务存活时间, 到期自动销毁
        public int TurnaroundMs = 5;     // 转向延时(ms): 收齐一帧后等总线静默再发
        public int FrameLen = 0;         // >0: 定长帧, 收满 N 字节即回发(优先于结束符)
        public byte[] FrameEnd = new byte[] { 0x0D, 0x0A };   // 结束符定界(默认 CRLF), 收到即整帧回发
        public int EchoGuardMs = 20;     // 自身回显抑制窗口(ms), 0 = 不抑制
        public int MaxEchoKb = 0;        // 单口回显总量上限 KB, 0 = 不限

        /// <summary>
        /// 解析转义字符串为字节: 支持 \r \n \t \0 和 \xNN(十六进制)。
        /// 注意: 参数串以 ';' 和 '=' 分隔, 结束符里若含这两个字符必须写成 \x3B / \x3D。
        /// </summary>
        public static byte[] ParseBytes(string s)
        {
            if (string.IsNullOrEmpty(s)) return null;

            var list = new List<byte>();
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];
                if (ch != '\\' || i + 1 >= s.Length) { list.Add((byte)ch); continue; }

                char nx = s[++i];
                switch (nx)
                {
                    case 'r': list.Add(0x0D); break;
                    case 'n': list.Add(0x0A); break;
                    case 't': list.Add(0x09); break;
                    case '0': list.Add(0x00); break;
                    case '\\': list.Add(0x5C); break;
                    case 'x':
                    case 'X':
                        if (i + 2 < s.Length)
                        {
                            byte v;
                            if (byte.TryParse(s.Substring(i + 1, 2),
                                              System.Globalization.NumberStyles.HexNumber,
                                              null, out v))
                            {
                                list.Add(v);
                                i += 2;
                                break;
                            }
                        }
                        list.Add((byte)'x');
                        break;
                    default: list.Add((byte)nx); break;
                }
            }
            return list.ToArray();
        }

        /// <summary>帧定界方式描述, 用于日志/返回值</summary>
        public string FrameDesc()
        {
            if (FrameLen > 0) return "len:" + FrameLen;
            if (FrameEnd != null && FrameEnd.Length > 0) return "end:" + SerialEchoService.DescribeBytes(FrameEnd);
            return "idle:" + TurnaroundMs + "ms";
        }
    }

    /// <summary>
    /// 多串口 RS485 回显服务(原路返回)。
    ///
    /// 每个串口一条后台线程: 阻塞读 → 按帧累积(默认以 CRLF 为帧结束符, 收满一行) →
    /// 等总线静默 TurnaroundMs → 整帧一次性写回同一串口(含回车换行)。
    /// 本地回显抑制: 刚发出的内容若被转接头回灌进 RX, 在 EchoGuardMs 窗口内按字节匹配丢弃,
    /// 避免"自己发自己收"引发自激乒乓。
    /// 到期(TTL)或 Stop/Dispose 后安全销毁: 停旗 → Join 读线程 → 兜底 Close → Dispose。
    /// 全局单例: 重复 Start 会先停掉旧实例, 避免 COM 口被漏占。
    /// </summary>
    public class SerialEchoService
    {
        static readonly object _lock = new object();
        static SerialEchoService _current;

        /// <summary>当前运行中的实例, 未运行时为 null</summary>
        public static SerialEchoService Current { get { lock (_lock) return _current; } }

        class Worker
        {
            public SerialPort Port;
            public Thread Thread;
            public EchoPortStatus Status;
            public SerialEchoOptions Opt;
            public volatile bool Run;
            public List<byte> Rx = new List<byte>();
            public Queue<byte> PendingEcho;    // 待抑制的自身发送内容
            public DateTime LastWriteUtc;
            public long LimitBytes;
            public long TxBytes;
        }

        readonly List<Worker> _workers = new List<Worker>();
        readonly object _sync = new object();
        Thread _reaper;
        volatile bool _running;
        DateTime _deadlineUtc;
        int _ttlMs;
        int _readTimeoutMs;

        public bool IsRunning { get { return _running; } }

        /// <summary>
        /// 打开全部串口并启动回显。
        /// 所有口在调用线程内同步打开, 任一失败即回滚关闭已打开的口并返回 null。
        /// </summary>
        /// <param name="portNames">端口名列表, 如 "COM3"</param>
        /// <param name="opt">回显参数</param>
        /// <param name="detail">成功时为描述串, 失败时为错误原因</param>
        public static SerialEchoService Start(List<string> portNames, SerialEchoOptions opt, out string detail)
        {
            detail = "";
            if (opt == null) opt = new SerialEchoOptions();

            var old = Current;
            if (old != null && old.IsRunning)
                utility_func.callbackdebuginfo("[SerialEcho] previous instance stopped: " + old.Stop("restart"));

            // 帧定界不需要靠空闲判帧时, 读超时可以放宽, 减少空转
            bool framedByData = opt.FrameLen > 0 || (opt.FrameEnd != null && opt.FrameEnd.Length > 0);
            int readTimeout = framedByData ? 200 : Math.Max(1, opt.TurnaroundMs);

            var svc = new SerialEchoService();
            svc._ttlMs = opt.TtlMs;
            svc._readTimeoutMs = readTimeout;
            svc._deadlineUtc = DateTime.UtcNow.AddMilliseconds(opt.TtlMs);

            // Step 1: 同步打开所有端口 (115200 8N1)
            foreach (var name in portNames)
            {
                var w = new Worker();
                w.Opt = opt;
                w.Status = new EchoPortStatus();
                w.Status.Port = name;
                w.LimitBytes = opt.MaxEchoKb > 0 ? (long)opt.MaxEchoKb * 1024 : 0;

                try
                {
                    var sp = new SerialPort(name, opt.Baud, Parity.None, 8, StopBits.One);
                    sp.ReadTimeout = readTimeout;
                    sp.WriteTimeout = 1000;
                    sp.Open();
                    sp.DiscardInBuffer();
                    sp.DiscardOutBuffer();
                    w.Port = sp;
                    w.Status.Opened = true;
                }
                catch (Exception ex)
                {
                    var opened = new List<string>();
                    lock (svc._sync) foreach (var x in svc._workers) opened.Add(x.Status.Port);

                    detail = "open_fail=" + name + ":" + ex.Message +
                             "; opened=" + opened.Count + ":" + string.Join(";", opened);
                    utility_func.callbackdebuginfo("[SerialEcho] " + detail);
                    try { if (w.Port != null) { if (w.Port.IsOpen) w.Port.Close(); w.Port.Dispose(); } } catch { }
                    svc.CloseAll();
                    return null;
                }

                lock (svc._sync) svc._workers.Add(w);
            }

            // Step 2: 每口一条接收线程
            svc._running = true;
            lock (svc._sync)
            {
                foreach (var w in svc._workers)
                {
                    w.Run = true;
                    var t = new Thread(svc.ReadLoop);
                    t.IsBackground = true;
                    t.Name = "serial-echo-" + w.Status.Port;
                    w.Thread = t;
                    t.Start(w);
                }
            }

            // Step 3: TTL 到期自动销毁
            svc._reaper = new Thread(svc.ReaperLoop);
            svc._reaper.IsBackground = true;
            svc._reaper.Name = "serial-echo-reaper";
            svc._reaper.Start();

            lock (_lock) _current = svc;

            detail = svc.Describe();
            utility_func.callbackdebuginfo("[SerialEcho] started: " + detail);
            return svc;
        }

        /// <summary>
        /// 接收函数: 阻塞读 → 帧累积 → 总线静默后整帧原路回写。
        /// 只把 TimeoutException 当正常(空闲)处理; 其他异常(拔线/句柄失效)记录后退出该线程。
        /// </summary>
        void ReadLoop(object state)
        {
            var w = (Worker)state;
            var buf = new byte[1024];

            try
            {
                while (_running && w.Run)
                {
                    int n;
                    try { n = w.Port.Read(buf, 0, buf.Length); }
                    catch (TimeoutException)
                    {
                        // 空闲已达 TurnaroundMs: 总线静默, 视为一帧结束
                        if (w.Rx.Count > 0 && w.Opt.FrameLen <= 0 && w.Opt.FrameEnd == null)
                            Flush(w, "idle");
                        continue;
                    }
                    catch (Exception ex) { w.Status.Error = "read: " + ex.Message; break; }

                    if (n <= 0) continue;
                    w.Status.RxBytes += n;

                    int off;
                    int keep = SuppressLocalEcho(w, buf, n, out off);
                    for (int i = 0; i < keep; i++)
                        OnRxByte(w, buf[off + i]);
                }
            }
            catch (Exception ex) { w.Status.Error = "loop: " + ex.Message; }

            w.Status.Leftover = w.Rx.Count;
            w.Status.Opened = false;
            if (w.Status.Leftover > 0)
                utility_func.callbackdebuginfo("[SerialEcho] " + w.Status.Port +
                    " exit with incomplete frame: " + w.Status.Leftover + "B");
        }

        /// <summary>逐字节累积, 按定长/结束符即时判帧</summary>
        void OnRxByte(Worker w, byte b)
        {
            w.Rx.Add(b);

            if (w.Opt.FrameLen > 0)
            {
                if (w.Rx.Count >= w.Opt.FrameLen && w.Rx.Count % w.Opt.FrameLen == 0)
                    Flush(w, "len");
                return;
            }

            if (w.Opt.FrameEnd != null && w.Opt.FrameEnd.Length > 0)
            {
                if (b == w.Opt.FrameEnd[w.Opt.FrameEnd.Length - 1] && EndsWith(w.Rx, w.Opt.FrameEnd))
                    Flush(w, "end");
            }
            // 两种定界都没给 → 交给 Read 空闲超时路径判帧
        }

        /// <summary>整帧回写(原路返回)</summary>
        void Flush(Worker w, string reason)
        {
            if (w.Rx.Count == 0) return;

            byte[] frame = w.Rx.ToArray();
            w.Rx.Clear();

            // 转向延时: 让对端驱动完全释放总线并给方向切换留时间。
            // reason == "idle" 时总线已静默 TurnaroundMs, 无需再等。
            if (reason != "idle" && w.Opt.TurnaroundMs > 0)
                Thread.Sleep(w.Opt.TurnaroundMs);

            if (w.LimitBytes > 0 && w.TxBytes + frame.Length > w.LimitBytes)
            {
                w.Status.EchoStopped = true;
                w.Status.Error = "echo limit " + w.LimitBytes + "B reached";
                utility_func.callbackdebuginfo("[SerialEcho] " + w.Status.Port + " " + w.Status.Error);
                TryClose(w.Port);
                w.Run = false;
                return;
            }

            try
            {
                w.Port.Write(frame, 0, frame.Length);
                w.TxBytes += frame.Length;
                w.Status.TxBytes += frame.Length;
                w.Status.Frames++;
                w.LastWriteUtc = DateTime.UtcNow;

                if (w.Opt.EchoGuardMs > 0)
                {
                    if (w.PendingEcho == null) w.PendingEcho = new Queue<byte>();
                    foreach (var b in frame) w.PendingEcho.Enqueue(b);
                }

                utility_func.callbackdebuginfo("[SerialEcho] " + w.Status.Port + " echo " +
                    frame.Length + "B (" + reason + "): " + DescribeBytes(frame));
            }
            catch (TimeoutException) { }
            catch (Exception ex)
            {
                w.Status.Error = "write: " + ex.Message;
                w.Run = false;
            }
        }

        /// <summary>
        /// 自身回显抑制: 丢弃本口刚发出、又被转接头回灌进 RX 的字节(local echo)。
        /// 返回需要保留的数据长度; 保留数据从 offset 开始。
        /// </summary>
        int SuppressLocalEcho(Worker w, byte[] buf, int n, out int offset)
        {
            offset = 0;

            if (w.Opt.EchoGuardMs <= 0 || w.PendingEcho == null || w.PendingEcho.Count == 0)
                return n;

            if ((DateTime.UtcNow - w.LastWriteUtc).TotalMilliseconds > w.Opt.EchoGuardMs)
            {
                w.PendingEcho.Clear();
                return n;
            }

            int i = 0;
            while (i < n && w.PendingEcho.Count > 0)
            {
                if (buf[i] == w.PendingEcho.Peek()) { w.PendingEcho.Dequeue(); i++; }
                else break;   // 出现不匹配 → 不是自身回显, 停止抑制
            }

            w.PendingEcho.Clear();   // 无论是否匹配完, 本次判定即结束
            offset = i;
            return n - i;
        }

        static bool EndsWith(List<byte> data, byte[] suffix)
        {
            if (data.Count < suffix.Length) return false;
            int start = data.Count - suffix.Length;
            for (int i = 0; i < suffix.Length; i++)
                if (data[start + i] != suffix[i]) return false;
            return true;
        }

        /// <summary>TTL 看门狗: 到期自动销毁</summary>
        void ReaperLoop()
        {
            while (_running && DateTime.UtcNow < _deadlineUtc)
                Thread.Sleep(100);

            if (_running)
            {
                utility_func.callbackdebuginfo("[SerialEcho] TTL " + _ttlMs + "ms reached, auto stop");
                Stop("ttl");
            }
        }

        /// <summary>
        /// 停止并释放全部资源。幂等, 可从任意线程调用(reaper 自身调用不会自 Join)。
        /// </summary>
        public string Stop(string reason)
        {
            if (!_running) return "already_stopped";
            _running = false;

            List<Worker> snapshot;
            lock (_sync) snapshot = new List<Worker>(_workers);
            foreach (var w in snapshot) w.Run = false;

            // 1) 先让读线程自行退出(最迟 _readTimeoutMs)
            var stuck = new List<Worker>();
            foreach (var w in snapshot)
            {
                if (w.Thread == null || w.Thread == Thread.CurrentThread) continue;
                if (!w.Thread.Join(_readTimeoutMs + 1000)) stuck.Add(w);
            }

            // 2) 仍未退出的用 Close 打断阻塞读, 再等一次
            foreach (var w in stuck)
            {
                try { if (w.Port.IsOpen) w.Port.Close(); }
                catch (Exception ex) { w.Status.Error = "close: " + ex.Message; }
                try { w.Thread.Join(1000); } catch { }
            }

            // 3) 统一释放
            long rx = 0, tx = 0, frames = 0;
            foreach (var w in snapshot)
            {
                rx += w.Status.RxBytes;
                tx += w.Status.TxBytes;
                frames += w.Status.Frames;
                TryClose(w.Port);
                try { w.Port.Dispose(); } catch { }
                w.Status.Opened = false;
            }
            lock (_sync) _workers.Clear();

            if (_reaper != null && _reaper != Thread.CurrentThread)
                try { _reaper.Join(1000); } catch { }

            lock (_lock) { if (_current == this) _current = null; }

            string res = "stopped=" + reason + "; rx=" + rx + "; tx=" + tx + "; frames=" + frames;
            utility_func.callbackdebuginfo("[SerialEcho] " + res + "; " + Status());
            return res;
        }

        static void TryClose(SerialPort sp)
        {
            try { if (sp != null && sp.IsOpen) sp.Close(); } catch { }
        }

        void CloseAll()
        {
            List<Worker> snapshot;
            lock (_sync) { snapshot = new List<Worker>(_workers); _workers.Clear(); }
            foreach (var w in snapshot)
            {
                TryClose(w.Port);
                try { w.Port.Dispose(); } catch { }
            }
        }

        /// <summary>启动描述: "opened=4:COM3,COM5,COM7,COM9; baud=115200; frame=len:8; turnaround=5ms; ttl=30000ms; stop_at=15:04:05"</summary>
        public string Describe()
        {
            var names = new List<string>();
            SerialEchoOptions opt = null;
            lock (_sync)
            {
                foreach (var w in _workers) { names.Add(w.Status.Port); opt = w.Opt; }
            }
            if (opt == null) return "no_port";

            return "opened=" + names.Count + ":" + string.Join(";", names) +
                   "; baud=" + opt.Baud +
                   "; frame=" + opt.FrameDesc() +
                   "; turnaround=" + opt.TurnaroundMs + "ms" +
                   "; ttl=" + _ttlMs + "ms" +
                   "; stop_at=" + _deadlineUtc.ToLocalTime().ToString("HH:mm:ss");
        }

        /// <summary>各口状态: "COM3:open rx=1024 tx=1024 frames=8; COM5:..."</summary>
        public string Status()
        {
            var sb = new StringBuilder();
            lock (_sync)
            {
                foreach (var w in _workers)
                {
                    if (sb.Length > 0) sb.Append("; ");
                    sb.Append(w.Status.ToString());
                }
            }
            return sb.Length > 0 ? sb.ToString() : "no_port";
        }

        /// <summary>
        /// 按字符串显示内容, 用于日志: 回车换行等控制符转义, 其他不可打印字节显示为 '.'
        /// </summary>
        public static string DescribeBytes(byte[] b)
        {
            if (b == null || b.Length == 0) return "";

            var sb = new StringBuilder();
            int n = Math.Min(b.Length, 64);
            for (int i = 0; i < n; i++)
            {
                byte c = b[i];
                if (c == 0x0D) sb.Append("\\r");
                else if (c == 0x0A) sb.Append("\\n");
                else if (c == 0x09) sb.Append("\\t");
                else if (c < 0x20 || c == 0x7F) sb.Append('.');
                else sb.Append((char)c);
            }
            if (b.Length > n) sb.Append("...");
            return sb.ToString();
        }
    }
}
