using System;
using System.IO.Ports;
using System.Threading;
using IniParser.Model;
using testapp.glob_set;

namespace test_antdui
{
    /// <summary>
    /// 外部输入触发源（按钮 / 微动开关）。
    /// 触发一次只代表“可靠的按下”，是否真的开始测试由 MainForm 决定
    /// （会走 _btnStart 的既有校验：SN 正则 / 多条码是否扫满）。
    /// </summary>
    public interface ITriggerInput : IDisposable
    {
        event Action Triggered;
        event Action<string> Status;
        bool IsRunning { get; }
        void Start();
        void Stop();
    }

    /// <summary>
    /// 去抖 + 重新武装状态机（两个硬件类共用）。
    /// active 连续保持 debounceMs → 触发一次；inactive 连续保持 rearmMs → 允许下一次触发。
    /// </summary>
    internal sealed class EdgeFilter
    {
        private bool _active;
        private bool _fired;
        private DateTime _since = DateTime.UtcNow;

        public bool Update(bool active, int debounceMs, int rearmMs)
        {
            DateTime now = DateTime.UtcNow;
            if (active != _active)
            {
                _active = active;
                _since = now;
                return false;
            }

            int elapsed = (int)(now - _since).TotalMilliseconds;
            if (active)
            {
                if (!_fired && elapsed >= debounceMs)
                {
                    _fired = true;
                    return true;
                }
            }
            else if (_fired && elapsed >= rearmMs)
            {
                _fired = false;
            }
            return false;
        }

        public void Reset()
        {
            _active = false;
            _fired = false;
            _since = DateTime.UtcNow;
        }
    }

    // ═══════════════════════════════════════════════════════════════
    // 硬件 1：串口检测引脚（CTS / DSR / DCD / RI）
    // ═══════════════════════════════════════════════════════════════
    public sealed class SerialPinTrigger : ITriggerInput
    {
        public enum PinKind { CTS, DSR, DCD, RI }

        private readonly string _portName;
        private readonly int _baudRate;
        private readonly PinKind _pin;
        private readonly bool _activeHigh;
        private readonly bool _rtsEnable;
        private readonly bool _dtrEnable;
        private readonly int _debounceMs;
        private readonly int _rearmMs;
        private readonly int _pollMs;

        private readonly EdgeFilter _filter = new EdgeFilter();
        private readonly object _ioLock = new object();

        private SerialPort _port;
        private System.Threading.Timer _timer;
        private volatile bool _running;
        private int _polling;
        private DateTime _nextOpenAt = DateTime.MinValue;
        private DateTime _riPulseAt = DateTime.MinValue;
        private string _lastStatus;

        // RI 只有边沿事件，没有可读电平：把一次脉冲当作“按下”，保持这么久供去抖采样
        private const int RiPulseMs = 120;

        public event Action Triggered;
        public event Action<string> Status;
        public bool IsRunning => _running;

        public SerialPinTrigger(string portName, int baudRate, PinKind pin,
                                bool activeHigh, int debounceMs, int rearmMs, int pollMs,
                                bool rtsEnable, bool dtrEnable)
        {
            _portName = portName;
            _baudRate = baudRate > 0 ? baudRate : 9600;
            _pin = pin;
            _activeHigh = activeHigh;
            _debounceMs = Math.Max(0, debounceMs);
            _rearmMs = Math.Max(0, rearmMs);
            _pollMs = Math.Max(5, pollMs);
            _rtsEnable = rtsEnable;
            _dtrEnable = dtrEnable;
        }

        public void Start()
        {
            if (_running) return;
            _running = true;
            _filter.Reset();
            _timer = new System.Threading.Timer(Poll, null, 0, _pollMs);
        }

        public void Stop()
        {
            lock (_ioLock)
            {
                _running = false;
                _timer?.Dispose();
                _timer = null;
                ClosePort();
            }
        }

        private void Poll(object state)
        {
            if (!_running) return;
            if (Interlocked.Exchange(ref _polling, 1) == 1) return;  // 上一次打开/读取还没结束

            try
            {
                if (_port == null || !_port.IsOpen)
                {
                    if (DateTime.UtcNow < _nextOpenAt) return;
                    OpenPort();
                    if (_port == null || !_port.IsOpen) return;
                }

                bool active;
                if (_pin == PinKind.RI)
                {
                    // RI 无电平可读：PinChanged 里记录脉冲时间
                    active = (DateTime.UtcNow - _riPulseAt).TotalMilliseconds <= RiPulseMs;
                }
                else
                {
                    bool level;
                    switch (_pin)
                    {
                        case PinKind.CTS: level = _port.CtsHolding; break;
                        case PinKind.DSR: level = _port.DsrHolding; break;
                        default: level = _port.CDHolding; break;
                    }
                    active = _activeHigh ? level : !level;
                }

                if (_filter.Update(active, _debounceMs, _rearmMs))
                    Triggered?.Invoke();
            }
            catch (Exception ex)
            {
                Report("串口引脚读取失败: " + ex.Message);
                ClosePort();
                _nextOpenAt = DateTime.UtcNow.AddSeconds(2);
            }
            finally
            {
                Interlocked.Exchange(ref _polling, 0);
            }
        }

        private void OpenPort()
        {
            try
            {
                lock (_ioLock)
                {
                    if (_running == false) return;
                    ClosePort();

                    _port = new SerialPort(_portName, _baudRate, Parity.None, 8, StopBits.One)
                    {
                        Handshake = Handshake.None,   // 必须 None：否则驱动会接管 CTS/DSR 等引脚
                        RtsEnable = _rtsEnable,
                        DtrEnable = _dtrEnable,
                        ReadTimeout = 200,
                        WriteTimeout = 200
                    };
                    if (_pin == PinKind.RI)
                        _port.PinChanged += OnPinChanged;
                    _port.Open();
                }

                _filter.Reset();
                string desc = $"触发器已就绪: {_portName}, {_pin}, active={(_activeHigh ? "high" : "low")}";
                if (desc != _lastStatus) Report(desc);
            }
            catch (Exception ex)
            {
                ClosePort();
                _nextOpenAt = DateTime.UtcNow.AddSeconds(2);
                string msg = $"触发器串口 {_portName} 打开失败: {ex.Message}";
                if (msg != _lastStatus) Report(msg);
            }
        }

        private void OnPinChanged(object sender, SerialPinChangedEventArgs e)
        {
            // 只关心 RI：其余引脚由轮询读取电平
            if (e.EventType == SerialPinChange.Ring)
                _riPulseAt = DateTime.UtcNow;
        }

        private void ClosePort()
        {
            lock (_ioLock)
            {
                if (_port == null) return;
                try
                {
                    if (_port.IsOpen) _port.Close();
                }
                catch { }
                try { _port.PinChanged -= OnPinChanged; } catch { }
                try { _port.Dispose(); } catch { }
                _port = null;
            }
        }

        private void Report(string message)
        {
            _lastStatus = message;
            Status?.Invoke(message);
        }

        public void Dispose() => Stop();
    }

    // ═══════════════════════════════════════════════════════════════
    // 硬件 2：SRND-CM-12DI（Modbus RTU，自包含实现）
    // ═══════════════════════════════════════════════════════════════
    public sealed class SrndDiTrigger : ITriggerInput
    {
        private readonly string _portName;
        private readonly int _baudRate;
        private readonly byte _addr;
        private readonly int _channel;       // 0-based：配置里的 1-24 减 1
        private readonly bool _activeHigh;
        private readonly int _pollMs;
        private readonly int _stablePolls;
        private readonly int _debounceMs;
        private readonly int _rearmMs;

        private readonly EdgeFilter _filter = new EdgeFilter();
        private readonly object _ioLock = new object();

        private SerialPort _port;
        private System.Threading.Timer _timer;
        private volatile bool _running;
        private int _polling;
        private DateTime _nextOpenAt = DateTime.MinValue;
        private string _lastStatus;
        private string _lastError;
        private DateTime _lastErrorAt = DateTime.MinValue;
        private int _stableCount;

        public event Action Triggered;
        public event Action<string> Status;
        public bool IsRunning => _running;

        public SrndDiTrigger(string portName, int baudRate, byte addr, int channel1Based,
                             bool activeHigh, int pollMs, int stablePolls, int debounceMs, int rearmMs)
        {
            _portName = portName;
            _baudRate = baudRate > 0 ? baudRate : 9600;
            _addr = addr;
            _channel = Math.Max(0, Math.Min(23, channel1Based - 1));
            _activeHigh = activeHigh;
            _pollMs = Math.Max(20, pollMs);
            _stablePolls = Math.Max(1, stablePolls);
            _debounceMs = Math.Max(0, debounceMs);
            _rearmMs = Math.Max(0, rearmMs);
        }

        public void Start()
        {
            if (_running) return;
            _running = true;
            _filter.Reset();
            _stableCount = 0;
            _timer = new System.Threading.Timer(Poll, null, 0, _pollMs);
        }

        public void Stop()
        {
            lock (_ioLock)
            {
                _running = false;
                _timer?.Dispose();
                _timer = null;
                ClosePort();
            }
        }

        private void Poll(object state)
        {
            if (!_running) return;
            if (Interlocked.Exchange(ref _polling, 1) == 1) return;  // 上一次读取还没结束

            try
            {
                if (_port == null || !_port.IsOpen)
                {
                    if (DateTime.UtcNow < _nextOpenAt) return;
                    OpenPort();
                    if (_port == null || !_port.IsOpen) return;
                }

                int mask = ReadAllChannels();
                if (mask < 0) return;

                bool raw = ((mask >> _channel) & 1) != 0;
                if (!_activeHigh) raw = !raw;

                // 连续 N 次读到同一状态才认可（工业现场抗抖动）
                if (raw) _stableCount++;
                else _stableCount = 0;

                bool stableActive = _stableCount >= _stablePolls;

                // 连续采样本身已提供 N 次确认，这里只再叠加用户配置的时间去抖
                int debounce = Math.Max(_debounceMs, _pollMs);
                if (_filter.Update(stableActive, debounce, _rearmMs))
                    Triggered?.Invoke();
            }
            finally
            {
                Interlocked.Exchange(ref _polling, 0);
            }
        }

        private void OpenPort()
        {
            try
            {
                lock (_ioLock)
                {
                    if (_running == false) return;
                    ClosePort();

                    _port = new SerialPort(_portName, _baudRate, Parity.None, 8, StopBits.One)
                    {
                        Handshake = Handshake.None,
                        RtsEnable = true,          // 与原 DigitalInputMonitor 保持一致（RS485 适配器常用）
                        ReadTimeout = 300,
                        WriteTimeout = 300
                    };
                    _port.Open();
                }

                _filter.Reset();
                _stableCount = 0;
                string desc = $"DI 触发器已就绪: {_portName}, CH{_channel + 1}, active={(_activeHigh ? "high" : "low")}";
                if (desc != _lastStatus) Report(desc);
            }
            catch (Exception ex)
            {
                ClosePort();
                _nextOpenAt = DateTime.UtcNow.AddSeconds(2);
                ReportError($"DI 串口 {_portName} 打开失败: {ex.Message}");
            }
        }

        /// <summary>读取 24 路离散输入，返回 bitmask（bit0 = CH1）；失败返回 -1。</summary>
        private int ReadAllChannels()
        {
            lock (_ioLock)
            {
                if (_port == null || !_port.IsOpen) return -1;

                try
                {
                    _port.DiscardInBuffer();
                    _port.DiscardOutBuffer();

                    byte[] cmd = new byte[8];
                    cmd[0] = _addr;
                    cmd[1] = 0x02;          // Read Discrete Inputs
                    cmd[2] = 0x00;
                    cmd[3] = 0x00;
                    cmd[4] = 0x00;
                    cmd[5] = 0x17;          // 24 路
                    ushort crc = Crc16(cmd, 6);
                    cmd[6] = (byte)(crc & 0xFF);
                    cmd[7] = (byte)(crc >> 8);

                    _port.Write(cmd, 0, cmd.Length);

                    DateTime deadline = DateTime.UtcNow.AddMilliseconds(Math.Max(100, _pollMs));
                    while (DateTime.UtcNow < deadline && _port.BytesToRead < 8)
                        Thread.Sleep(5);

                    int available = _port.BytesToRead;
                    if (available < 8)
                    {
                        ReportError("DI 模块无响应");
                        return -1;
                    }

                    byte[] buf = new byte[available];
                    int read = 0;
                    while (read < available)
                    {
                        int n = _port.Read(buf, read, available - read);
                        if (n <= 0) break;
                        read += n;
                    }

                    if (read < 8 || buf[0] != _addr || buf[1] != 0x02 || buf[2] < 3)
                    {
                        ReportError("DI 响应无效");
                        return -1;
                    }

                    // buf[3] = 输入 0-7（bit0 为 CH1），依次向上
                    _lastError = null;
                    return buf[3] | (buf[4] << 8) | (buf[5] << 16);
                }
                catch (Exception ex)
                {
                    ReportError("DI 读取失败: " + ex.Message);
                    ClosePort();
                    _nextOpenAt = DateTime.UtcNow.AddSeconds(2);
                    return -1;
                }
            }
        }

        private void ClosePort()
        {
            lock (_ioLock)
            {
                if (_port == null) return;
                try
                {
                    if (_port.IsOpen) _port.Close();
                }
                catch { }
                try { _port.Dispose(); } catch { }
                _port = null;
            }
        }

        private static ushort Crc16(byte[] data, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = 0; i < length; i++)
            {
                crc ^= data[i];
                for (int bit = 0; bit < 8; bit++)
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
            }
            return crc;
        }

        private void Report(string message)
        {
            _lastStatus = message;
            Status?.Invoke(message);
        }

        /// <summary>错误 10 秒内相同内容只报一次，避免轮询刷屏。</summary>
        private void ReportError(string message)
        {
            if (message == _lastError && (DateTime.UtcNow - _lastErrorAt).TotalSeconds < 10)
                return;
            _lastError = message;
            _lastErrorAt = DateTime.UtcNow;
            Status?.Invoke(message);
        }

        public void Dispose() => Stop();
    }

    // ═══════════════════════════════════════════════════════════════
    // 工厂：从 setup.ini [settrigger] 创建对应的触发器
    // ═══════════════════════════════════════════════════════════════
    public static class TriggerFactory
    {
        /// <summary>
        /// 读取 [settrigger]；未启用或配置不完整时返回 null。
        /// log 用于输出配置错误（可传 null）。
        /// </summary>
        public static ITriggerInput CreateFromIni(Action<string> log = null)
        {
            IniData ini;
            try { ini = glob_ini_instance.getInstance()?.getSetupIniData; }
            catch { return null; }

            KeyDataCollection sec = ini?["settrigger"];
            if (sec == null) return null;

            if (!GetBool(sec, "enable", false)) return null;

            string type = (sec["type"] ?? "serialpin").Trim().ToLowerInvariant();
            int debounce = GetInt(sec, "debounce_ms", 50);
            int rearm = GetInt(sec, "rearm_ms", 400);

            if (type == "srnd12di" || type == "srnd" || type == "di")
            {
                string port = (sec["di_port"] ?? "").Trim();
                if (port.Length == 0 && ini != null)
                    port = (ini["setport"]?["SRND_CM_12DI_port"] ?? "").Trim();
                if (port.Length == 0)
                {
                    log?.Invoke("[settrigger] 未配置 di_port，DI 触发器未启动");
                    return null;
                }

                int baud = GetInt(sec, "di_baudrate", 9600);
                int addr = GetInt(sec, "di_addr", 1);
                int channel = GetInt(sec, "di_channel", 1);
                int poll = GetInt(sec, "di_poll_ms", 50);
                int stable = GetInt(sec, "di_stable_polls", 3);
                bool activeHigh = GetActiveHigh(sec, "di_active");

                return new SrndDiTrigger(port, baud, (byte)Math.Max(1, Math.Min(247, addr)),
                                         channel, activeHigh, poll, stable, debounce, rearm);
            }

            // 默认：串口检测引脚
            string pinPort = (sec["port"] ?? "").Trim();
            if (pinPort.Length == 0)
            {
                log?.Invoke("[settrigger] 未配置 port，串口引脚触发器未启动");
                return null;
            }

            int pinBaud = GetInt(sec, "baudrate", 9600);
            string pinName = (sec["pin"] ?? "CTS").Trim();
            SerialPinTrigger.PinKind kind;
            if (!Enum.TryParse(pinName, true, out kind)) kind = SerialPinTrigger.PinKind.CTS;

            bool pinActiveHigh = GetActiveHigh(sec, "active");
            int pollMs = GetInt(sec, "poll_ms", 20);
            bool rts = GetBool(sec, "rts_enable", false);
            bool dtr = GetBool(sec, "dtr_enable", false);

            return new SerialPinTrigger(pinPort, pinBaud, kind, pinActiveHigh,
                                        debounce, rearm, pollMs, rts, dtr);
        }

        private static int GetInt(KeyDataCollection sec, string key, int defaultValue)
        {
            int value;
            if (sec != null && int.TryParse((sec[key] ?? "").Trim(), out value) && value > 0)
                return value;
            return defaultValue;
        }

        private static bool GetBool(KeyDataCollection sec, string key, bool defaultValue)
        {
            string text = (sec?[key] ?? "").Trim().ToLowerInvariant();
            if (text.Length == 0) return defaultValue;
            return text == "true" || text == "1" || text == "yes" || text == "on";
        }

        /// <summary>active=high/low；默认 high（按下接高电平）。</summary>
        private static bool GetActiveHigh(KeyDataCollection sec, string key)
        {
            string text = (sec?[key] ?? "").Trim().ToLowerInvariant();
            if (text == "low" || text == "0" || text == "false") return false;
            return true;
        }
    }
}
