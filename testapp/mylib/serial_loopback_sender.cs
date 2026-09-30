using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace testapp.mylib
{
    /// <summary>
    /// 单口回环发送参数
    /// </summary>
    public class SerialLoopbackOptions
    {
        public int Baud = 115200;
        public string Data = "HERO_AUX_PING";            // 发送的字符串(不含结束符)
        public byte[] FrameEnd = new byte[] { 0x0D, 0x0A };  // 帧结束符(默认 CRLF)
        public int DurationMs = 3000;      // 总运行时长: 发/收持续这么久, 供 LED 检测
        public int IntervalMs = 500;       // 两次发送间隔
        public int ReplyTimeoutMs = 1000;  // 单次等回复超时
        public int LocalEchoMs = 10;       // 发送后丢弃本地回显的窗口(485 转接头可能回灌自身发送)
        public int WaitMs = 0;             // 判 pass/fail 的等待窗口, 0 = 用 DurationMs
    }

    /// <summary>
    /// 单口回环发送服务(RS485 半双工)。
    ///
    /// 周期执行"发一帧 → 等对端回复一帧": 发送前清空收缓冲, 发送后先丢弃本地回显窗口,
    /// 再按结束符等待回复; 收到第一帧回复即记录成功时间, 但线程继续跑到 DurationMs 才停,
    /// 便于随后用示波器/目视检查对应 TX/RX LED。
    /// 每次只在一轮"发—收"结束后才进入下一轮, 不存在同一口同时收发造成的总线争用。
    /// 支持多口并发(按端口名注册), 同口重复 Start 会先停掉旧实例。
    /// </summary>
    public class SerialLoopbackSender
    {
        static readonly object _lock = new object();
        static readonly Dictionary<string, SerialLoopbackSender> _active =
            new Dictionary<string, SerialLoopbackSender>(StringComparer.OrdinalIgnoreCase);

        SerialPort _port;
        Thread _thread;
        SerialLoopbackOptions _opt;
        string _portName = "";
        DateTime _deadlineUtc;

        volatile bool _running;
        volatile int _sent;
        volatile int _replies;
        volatile int _timeouts;
        long _rxBytes;
        int _firstReplyMs = -1;
        string _firstReplyText = "";
        string _error = "";

        public bool IsRunning { get { return _running; } }
        public int Replies { get { return _replies; } }
        public string PortName { get { return _portName; } }

        /// <summary>取某端口上运行中的发送服务, 无则 null</summary>
        public static SerialLoopbackSender Get(string port)
        {
            if (string.IsNullOrEmpty(port)) return null;
            lock (_lock)
            {
                SerialLoopbackSender s;
                return _active.TryGetValue(port, out s) ? s : null;
            }
        }

        /// <summary>
        /// 打开指定串口并在后台线程开始周期回环发送。
        /// 端口打不开立即返回 null, detail 给出原因。
        /// </summary>
        public static SerialLoopbackSender Start(string port, SerialLoopbackOptions opt, out string detail)
        {
            detail = "";
            if (opt == null) opt = new SerialLoopbackOptions();
            if (string.IsNullOrEmpty(port))
            {
                detail = "port_empty";
                return null;
            }

            var old = Get(port);
            if (old != null && old.IsRunning)
                utility_func.callbackdebuginfo("[SerialLoop] previous sender stopped: " + old.Stop("restart"));

            var s = new SerialLoopbackSender();
            s._opt = opt;
            s._portName = port;
            s._deadlineUtc = DateTime.UtcNow.AddMilliseconds(opt.DurationMs);

            try
            {
                var sp = new SerialPort(port, opt.Baud, Parity.None, 8, StopBits.One);
                sp.ReadTimeout = 50;          // 小超时, 保证停旗后线程能快速退出
                sp.WriteTimeout = 1000;
                sp.Open();
                sp.DiscardInBuffer();
                sp.DiscardOutBuffer();
                s._port = sp;
            }
            catch (Exception ex)
            {
                detail = SerialEchoService.CsvSafe("open_fail=" + port + ":" + ex.Message);
                utility_func.callbackdebuginfo("[SerialLoop] " + detail);
                s.ClosePort();
                return null;
            }

            lock (_lock) _active[port] = s;

            s._running = true;
            var t = new Thread(s.Loop);
            t.IsBackground = true;
            t.Name = "serial-loop-" + port;
            s._thread = t;
            t.Start();

            detail = s.Describe();
            utility_func.callbackdebuginfo("[SerialLoop] started: " + detail);
            return s;
        }

        /// <summary>等第一帧回复; 返回 true 表示收到过回复</summary>
        public bool WaitFirstReply(int waitMs)
        {
            if (_replies > 0) return true;

            long start = Environment.TickCount;
            while (Environment.TickCount - start < waitMs)
            {
                if (_replies > 0) return true;
                if (!_running) return _replies > 0;   // 线程已退出(出错或到期), 不再等
                Thread.Sleep(20);
            }
            return _replies > 0;
        }

        /// <summary>发送线程主循环: 发一帧 → 丢本地回显 → 等回复 → 间隔</summary>
        void Loop()
        {
            byte[] frame = BuildFrame();
            var buf = new byte[512];

            try
            {
                while (_running && DateTime.UtcNow < _deadlineUtc)
                {
                    long t0 = Environment.TickCount;

                    // 1) 清残帧后发送
                    try { _port.DiscardInBuffer(); } catch { }
                    try
                    {
                        _port.Write(frame, 0, frame.Length);
                        _sent++;
                    }
                    catch (Exception ex) { _error = "write: " + ex.Message; break; }

                    // 2) 丢弃本地回显: 485 转接头可能把自己发出的字节回灌进 RX,
                    //    不丢的话会把自身发送误判成对端回复
                    if (_opt.LocalEchoMs > 0) Thread.Sleep(_opt.LocalEchoMs);
                    try { _port.DiscardInBuffer(); } catch { }

                    // 3) 等回复(按结束符判帧)
                    string reply;
                    if (WaitReply(buf, out reply))
                    {
                        _replies++;
                        if (_replies == 1)
                        {
                            _firstReplyMs = (int)(Environment.TickCount - t0);
                            _firstReplyText = reply;
                            utility_func.callbackdebuginfo("[SerialLoop] " + _portName +
                                " first reply in " + _firstReplyMs + "ms: " + reply);
                        }
                    }
                    else
                    {
                        _timeouts++;
                        utility_func.callbackdebuginfo("[SerialLoop] " + _portName +
                            " send #" + _sent + " no reply in " + _opt.ReplyTimeoutMs + "ms");
                    }

                    // 4) 发送间隔
                    int gap = _opt.IntervalMs - (int)(Environment.TickCount - t0);
                    if (gap > 0) Thread.Sleep(gap);
                }
            }
            catch (Exception ex) { _error = "loop: " + ex.Message; }
            finally
            {
                _running = false;
                RemoveSelf();
                ClosePort();
                utility_func.callbackdebuginfo("[SerialLoop] " + _portName + " finished: " + Summary());
            }
        }

        /// <summary>等一帧回复; 返回是否收到</summary>
        bool WaitReply(byte[] buf, out string replyText)
        {
            replyText = "";
            var rx = new List<byte>();
            long start = Environment.TickCount;

            while (_running && Environment.TickCount - start < _opt.ReplyTimeoutMs)
            {
                int n;
                try { n = _port.Read(buf, 0, buf.Length); }
                catch (TimeoutException) { continue; }
                catch (Exception ex)
                {
                    _error = "read: " + ex.Message;
                    _running = false;
                    return false;
                }

                if (n <= 0) continue;
                _rxBytes += n;

                if (_opt.FrameEnd == null || _opt.FrameEnd.Length == 0)
                {
                    rx.AddRange(new ArraySegment<byte>(buf, 0, n));   // 无结束符: 收到即算回复
                    replyText = SerialEchoService.DescribeBytes(rx.ToArray());
                    return true;
                }

                for (int i = 0; i < n; i++)
                {
                    rx.Add(buf[i]);
                    if (EndsWith(rx, _opt.FrameEnd))
                    {
                        replyText = SerialEchoService.DescribeBytes(rx.ToArray());
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>发送内容 + 结束符(若 data 里已带结束符则不重复追加)</summary>
        byte[] BuildFrame()
        {
            byte[] body = Encoding.ASCII.GetBytes(_opt.Data ?? "");
            byte[] end = _opt.FrameEnd ?? new byte[0];

            if (end.Length > 0 && EndsWith(body, end))
                return body;

            var frame = new byte[body.Length + end.Length];
            Array.Copy(body, 0, frame, 0, body.Length);
            Array.Copy(end, 0, frame, body.Length, end.Length);
            return frame;
        }

        static bool EndsWith(byte[] data, byte[] suffix)
        {
            if (data == null || suffix == null || data.Length < suffix.Length) return false;
            int start = data.Length - suffix.Length;
            for (int i = 0; i < suffix.Length; i++)
                if (data[start + i] != suffix[i]) return false;
            return true;
        }

        static bool EndsWith(List<byte> data, byte[] suffix)
        {
            if (data == null || suffix == null || data.Count < suffix.Length) return false;
            int start = data.Count - suffix.Length;
            for (int i = 0; i < suffix.Length; i++)
                if (data[start + i] != suffix[i]) return false;
            return true;
        }

        /// <summary>停止并释放(幂等)</summary>
        public string Stop(string reason)
        {
            if (!_running) return "already_stopped";
            _running = false;

            if (_thread != null && _thread != Thread.CurrentThread)
                try { _thread.Join(2000); } catch { }

            RemoveSelf();
            ClosePort();

            string res = "stopped=" + reason + "; " + Summary();   // Summary 已做 CSV 清洗
            utility_func.callbackdebuginfo("[SerialLoop] " + res);
            return res;
        }

        /// <summary>停止全部发送服务(供测试对象 Dispose 时调用)</summary>
        public static string StopAll(string reason)
        {
            List<SerialLoopbackSender> list;
            lock (_lock) list = new List<SerialLoopbackSender>(_active.Values);

            int n = 0;
            foreach (var s in list)
            {
                if (s.IsRunning) { s.Stop(reason); n++; }
            }
            return "stopped_ports=" + n;
        }

        void RemoveSelf()
        {
            lock (_lock)
            {
                SerialLoopbackSender cur;
                if (_active.TryGetValue(_portName, out cur) && cur == this)
                    _active.Remove(_portName);
            }
        }

        void ClosePort()
        {
            try { if (_port != null && _port.IsOpen) _port.Close(); } catch { }
            try { if (_port != null) _port.Dispose(); } catch { }
        }

        /// <summary>启动描述</summary>
        public string Describe()
        {
            return SerialEchoService.CsvSafe(
                   "port=" + _portName + "; baud=" + _opt.Baud +
                   "; data=" + _opt.Data +
                   "; frame=" + SerialEchoService.DescribeBytes(_opt.FrameEnd) +
                   "; interval=" + _opt.IntervalMs + "ms" +
                   "; reply_timeout=" + _opt.ReplyTimeoutMs + "ms" +
                   "; duration=" + _opt.DurationMs + "ms" +
                   "; stop_at=" + _deadlineUtc.ToLocalTime().ToString("HH:mm:ss"));
        }

        /// <summary>结果摘要(无逗号, 可直接进 CSV 字段)</summary>
        public string Summary()
        {
            var sb = new StringBuilder();
            sb.Append("port=").Append(_portName);
            sb.Append("; sent=").Append(_sent);
            sb.Append("; replies=").Append(_replies);
            sb.Append("; timeouts=").Append(_timeouts);
            sb.Append("; rx=").Append(_rxBytes).Append("B");
            if (_firstReplyMs >= 0)
            {
                sb.Append("; first_reply=").Append(_firstReplyMs).Append("ms");
                if (_firstReplyText.Length > 0) sb.Append(": ").Append(_firstReplyText);
            }
            if (_error.Length > 0) sb.Append("; err=").Append(_error);
            return SerialEchoService.CsvSafe(sb.ToString());
        }
    }
}
