using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace testapp.mylib
{
    /// <summary>
    /// RS485 回显服务参数。
    /// 数据是 ASCII 字符串, 默认以回车换行(CRLF)作帧结束符: 收满一行即整帧回写。
    /// </summary>
    public class SerialEchoOptions
    {
        public int Baud = 115200;
        public int TtlMs = 30000;        // 服务存活时间, 到期自动销毁
        public int TurnaroundMs = 5;     // 转向延时(ms): 收满一帧后等总线静默再发
        public int FrameLen = 0;         // >0: 定长帧, 优先于结束符
        public byte[] FrameEnd = new byte[] { 0x0D, 0x0A };   // 帧结束符(默认 CRLF)
        public int EchoGuardMs = 20;     // 写后丢弃自身回显的窗口(ms), 0 = 不丢弃
        public int MaxEchoKb = 0;        // 单口回显总量上限 KB, 0 = 不限

        /// <summary>
        /// 解析转义字符串为字节: 支持 \r \n \t \0 和 \xNN(十六进制)。
        /// 参数串以 ';' 和 '=' 分隔, 结束符里若含这两个字符须写成 \x3B / \x3D。
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
                        byte v;
                        if (i + 2 < s.Length && byte.TryParse(s.Substring(i + 1, 2),
                                System.Globalization.NumberStyles.HexNumber, null, out v))
                        {
                            list.Add(v);
                            i += 2;
                        }
                        else list.Add((byte)'x');
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
            return "any";
        }
    }

    /// <summary>
    /// 多串口 RS485 回显服务(原路返回)。
    ///
    /// 每个串口一条线程持有该端口并守 TTL; 数据由 DataReceived 回调处理:
    /// 累积到一帧完整(默认 CRLF 结束) → 等总线静默 TurnaroundMs → 整帧写回同一串口。
    /// 半双工总线不能"收到即回发", 否则会与对端驱动争用总线。
    /// 到期或 Stop/Dispose 后, 端口由各自的持有线程关闭。
    /// 全局单例: 重复 Start 会先停掉旧实例, 避免 COM 口被漏占。
    /// </summary>
    public class SerialEchoService
    {
        static readonly object _lock = new object();
        static SerialEchoService _current;

        /// <summary>当前运行中的实例, 未运行时为 null</summary>
        public static SerialEchoService Current { get { lock (_lock) return _current; } }

        public bool IsRunning { get { return _running; } }

        class PortWorker
        {
            public string Name;
            public SerialPort Port;
            public Thread Thread;
            public SerialDataReceivedEventHandler Handler;
            public readonly object Sync = new object();
            public volatile bool Stop;
            public DateTime IgnoreEchoUntil;              // 该时刻前收到的数据是自身回显, 丢弃
            public readonly StringBuilder Rx = new StringBuilder();
            public long RxBytes;
            public long TxBytes;
            public int Frames;
            public string Error = "";

            public string StatusText()
            {
                return Name + ":" + (Stop ? "closed" : "open") +
                       " rx=" + RxBytes + " tx=" + TxBytes + " frames=" + Frames +
                       (Rx.Length > 0 ? " leftover=" + Rx.Length : "") +
                       (Error.Length > 0 ? " err=" + Error : "");
            }
        }

        readonly List<PortWorker> _workers = new List<PortWorker>();
        SerialEchoOptions _opt;
        string _frameEnd = "\r\n";
        volatile bool _running;
        DateTime _deadline;
        int _alive;

        /// <summary>
        /// 打开全部串口并启动回显。所有口在调用线程内同步打开,
        /// 任一失败即回滚已打开的口并返回 null(detail 给出原因)。
        /// </summary>
        public static SerialEchoService Start(List<string> portNames, SerialEchoOptions opt, out string detail)
        {
            detail = "";
            if (opt == null) opt = new SerialEchoOptions();

            var old = Current;
            if (old != null && old.IsRunning)
                utility_func.callbackdebuginfo("[SerialEcho] previous instance stopped: " + old.Stop("restart"));

            var svc = new SerialEchoService();
            svc._opt = opt;
            svc._frameEnd = opt.FrameEnd != null ? Encoding.ASCII.GetString(opt.FrameEnd) : "";
            svc._deadline = DateTime.UtcNow.AddMilliseconds(opt.TtlMs);

            foreach (var name in portNames)
            {
                var w = new PortWorker();
                w.Name = name;

                try
                {
                    var sp = new SerialPort(name, opt.Baud, Parity.None, 8, StopBits.One);
                    sp.WriteTimeout = 1000;   // 必须有限值: 默认无限, 写卡住会把关闭流程一起挂死
                    sp.Open();
                    w.Port = sp;
                    w.Handler = (s, e) => svc.OnRecv(w, e);
                    sp.DataReceived += w.Handler;
                }
                catch (Exception ex)
                {
                    CloseWorker(w);

                    var opened = new List<string>();
                    foreach (var x in svc._workers) opened.Add(x.Name);

                    detail = CsvSafe("open_fail=" + name + ":" + ex.Message +
                                     "; opened=" + opened.Count + ":" + string.Join(";", opened));
                    utility_func.callbackdebuginfo("[SerialEcho] " + detail);

                    svc.CloseAll();
                    return null;
                }

                svc._workers.Add(w);
            }

            // 每口一条线程: 持有端口守 TTL, 退出时自己关端口
            svc._running = true;
            svc._alive = svc._workers.Count;
            foreach (var w in svc._workers)
            {
                var t = new Thread(svc.WorkFunc);
                t.IsBackground = true;
                t.Name = "serial-echo-" + w.Name;
                w.Thread = t;
                t.Start(w);
            }

            lock (_lock) _current = svc;

            detail = svc.Describe();
            utility_func.callbackdebuginfo("[SerialEcho] started: " + detail);
            return svc;
        }

        /// <summary>
        /// 端口持有线程: 守到 TTL 到期或被告知停止, 然后自己安全关闭端口。
        /// 收数据的活由 DataReceived 回调干, 这里不碰端口。
        /// </summary>
        void WorkFunc(object state)
        {
            var w = (PortWorker)state;

            while (!w.Stop && DateTime.UtcNow < _deadline)
                Thread.Sleep(100);

            CloseWorker(w);
            utility_func.callbackdebuginfo("[SerialEcho] " + w.Name + " closed: " + w.StatusText());

            if (Interlocked.Decrement(ref _alive) == 0)
            {
                _running = false;
                lock (_lock) { if (_current == this) _current = null; }
                utility_func.callbackdebuginfo("[SerialEcho] all ports closed");
            }
        }

        /// <summary>
        /// 收到数据: 累积到一帧完整 → 等总线静默(半双工转向) → 整帧原路写回。
        /// 必须整体 try/catch: 回调跑在 ThreadPool 线程上, 抛异常会直接终止进程。
        /// </summary>
     void OnRecv(PortWorker w, SerialDataReceivedEventArgs e)
        {
            if (e.EventType != SerialData.Chars) return;

            try
            {
                lock (w.Sync)
                {
                    if (w.Stop) return;                     // 已关闭, 不再读写

                    string s = w.Port.ReadExisting();
                    if (s.Length == 0) return;

                    // 自身回显: 刚写出去的内容可能被转接头回灌进 RX, 窗口内一律丢弃
                    if (_opt.EchoGuardMs > 0 && DateTime.UtcNow < w.IgnoreEchoUntil) return;

                    Interlocked.Add(ref w.RxBytes, s.Length);
                    w.Rx.Append(s);

                    while (true)
                    {
                        string frame = TakeFrame(w);
                        if (frame == null) return;

                        if (_opt.MaxEchoKb > 0 && w.TxBytes + frame.Length > (long)_opt.MaxEchoKb * 1024)
                        {
                            w.Error = "echo limit " + _opt.MaxEchoKb + "KB reached";
                            w.Stop = true;                  // 交给持有线程关闭
                            return;
                        }

                        if (_opt.TurnaroundMs > 0) Thread.Sleep(_opt.TurnaroundMs);
                        w.Port.Write(frame);                // 原路返回

                        Interlocked.Add(ref w.TxBytes, frame.Length);
                        w.Frames++;
                        w.IgnoreEchoUntil = DateTime.UtcNow.AddMilliseconds(_opt.EchoGuardMs);

                        utility_func.callbackdebuginfo("[SerialEcho] " + w.Name +
                            " echo " + frame.Length + "B: " + CsvSafe(frame));
                    }
                }
            }
            catch (Exception ex)
            {
                w.Error = "recv: " + ex.Message;
            }
        }

        /// <summary>从缓冲里取出一帧完整数据(定长 / 结束符); 不足一帧返回 null</summary>
        string TakeFrame(PortWorker w)
        {
            string buf = w.Rx.ToString();

            if (_opt.FrameLen > 0)
            {
                if (buf.Length < _opt.FrameLen) return null;
                w.Rx.Remove(0, _opt.FrameLen);
                return buf.Substring(0, _opt.FrameLen);
            }

            if (_frameEnd.Length > 0)
            {
                int i = buf.IndexOf(_frameEnd, StringComparison.Ordinal);
                if (i < 0) return null;
                i += _frameEnd.Length;
                w.Rx.Remove(0, i);
                return buf.Substring(0, i);
            }

            w.Rx.Clear();                                   // 无结束符: 收到即回
            return buf;
        }

        /// <summary>
        /// 停止并释放全部资源(幂等)。端口由各自的持有线程关闭。
        /// </summary>
        public string Stop(string reason)
        {
            if (!_running) return "already_stopped";

            foreach (var w in _workers) w.Stop = true;
            foreach (var w in _workers)
                if (w.Thread != null && w.Thread != Thread.CurrentThread)
                    try { w.Thread.Join(3000); } catch { }

            _running = false;
            lock (_lock) { if (_current == this) _current = null; }

            long rx = 0, tx = 0;
            int frames = 0;
            foreach (var w in _workers)
            {
                rx += w.RxBytes;
                tx += w.TxBytes;
                frames += w.Frames;
            }

            string res = CsvSafe("stopped=" + reason + "; rx=" + rx + "; tx=" + tx + "; frames=" + frames);
            utility_func.callbackdebuginfo("[SerialEcho] " + res + "; " + Status());
            return res;
        }

        void CloseAll()
        {
            foreach (var w in _workers) CloseWorker(w);
            _workers.Clear();
        }

        static void CloseWorker(PortWorker w)
        {
            lock (w.Sync)
            {
                w.Stop = true;
                try { if (w.Port != null && w.Handler != null) w.Port.DataReceived -= w.Handler; } catch { }
                try { if (w.Port != null && w.Port.IsOpen) w.Port.Close(); } catch { }
                try { if (w.Port != null) w.Port.Dispose(); } catch { }
            }
        }

        /// <summary>启动描述: "opened=4:COM3;COM5;COM7;COM9; baud=115200; frame=end:\r\n; turnaround=5ms; ttl=30000ms; stop_at=15:04:05"</summary>
        public string Describe()
        {
            var names = new List<string>();
            foreach (var w in _workers) names.Add(w.Name);

            return CsvSafe("opened=" + names.Count + ":" + string.Join(";", names) +
                   "; baud=" + _opt.Baud +
                   "; frame=" + _opt.FrameDesc() +
                   "; turnaround=" + _opt.TurnaroundMs + "ms" +
                   "; ttl=" + _opt.TtlMs + "ms" +
                   "; stop_at=" + _deadline.ToLocalTime().ToString("HH:mm:ss"));
        }

        /// <summary>各口状态: "COM3:open rx=1024 tx=1024 frames=8; COM5:..."</summary>
        public string Status()
        {
            var sb = new StringBuilder();
            foreach (var w in _workers)
            {
                if (sb.Length > 0) sb.Append("; ");
                sb.Append(w.StatusText());
            }
            return CsvSafe(sb.Length > 0 ? sb.ToString() : "no_port");
        }

        /// <summary>
        /// 结果字符串清洗(CSV 安全): 逗号 → ';', CR/LF/Tab → 字面转义 \r \n \t。
        /// 结果存 CSV: 逗号会被当成列分隔符导致列错位, CR/LF 会把一条记录断成多行。
        /// </summary>
        public static string CsvSafe(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.Replace(",", ";").Replace("\r", "\\r").Replace("\n", "\\n").Replace("\t", "\\t");
        }

        /// <summary>可打印文本原样显示, 回车换行等控制符转义为字面 \r \n \t, 用于日志</summary>
        public static string DescribeBytes(byte[] b)
        {
            if (b == null || b.Length == 0) return "";

            var sb = new StringBuilder();
            foreach (byte c in b)
            {
                if (c == 0x0D) sb.Append("\\r");
                else if (c == 0x0A) sb.Append("\\n");
                else if (c == 0x09) sb.Append("\\t");
                else if (c < 0x20 || c == 0x7F) sb.Append('.');
                else sb.Append((char)c);
            }
            return sb.ToString();
        }
    }
}
