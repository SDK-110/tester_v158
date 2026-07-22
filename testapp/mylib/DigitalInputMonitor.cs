using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using testapp.glob_set;

namespace testapp
{
    /// <summary>
    /// 12-channel digital input monitor (SRND-CM-12DI).
    /// Polls via Modbus RTU, fires events on rising/falling edges.
    /// </summary>
    public class DigitalInputMonitor : IDisposable
    {
        // ─── Modbus constants ────────────────────────────────────
        private const byte DEV_ADDR = 0x01;
        private const byte FUNC_READ_DI = 0x02;
        private static readonly byte[] READ_CMD = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x17, 0x00, 0x00 };

        // ─── Config keys in setup.ini [setport] ──────────────────
        private const string INI_PORT_KEY = "SRND_CM_12DI_port";
        private const string INI_BAUD_KEY = "SRND_CM_12DI_baudrate";

        // ─── Fields ──────────────────────────────────────────────
        private static DigitalInputMonitor _instance;
        private static readonly object _lockSingleton = new object();

        private SerialPort _port;
        private Timer _timer;
        private int _pollIntervalMs;
        private int _previousState;      // bitmask of all 24 bits
        private bool _running;
        private readonly object _lock = new object();
        private bool _disposed;

        // ─── Events ──────────────────────────────────────────────
        /// <summary>channel (0-23), new state (true=high)</summary>
        public event Action<int, bool> InputChanged;
        /// <summary>channel (0-23) rising edge (0→1)</summary>
        public event Action<int> InputRising;
        /// <summary>channel (0-23) falling edge (1→0)</summary>
        public event Action<int> InputFalling;
        /// <summary>communication error</summary>
        public event Action<string> ScanError;

        // ─── Properties ──────────────────────────────────────────
        public string PortName { get; private set; }
        public int BaudRate { get; private set; }
        public int PollIntervalMs
        {
            get => _pollIntervalMs;
            set
            {
                if (value < 50) value = 50;
                if (value > 5000) value = 5000;
                _pollIntervalMs = value;
                if (_running) RestartTimer();
            }
        }
        public bool IsRunning => _running;

        /// <summary>Snapshot of last known state (bitmask, bit0 = ch0).</summary>
        public int CurrentState => _previousState;

        public static DigitalInputMonitor Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockSingleton)
                    {
                        if (_instance == null)
                            _instance = new DigitalInputMonitor();
                    }
                }
                return _instance;
            }
        }

        // ─── Construction ────────────────────────────────────────
        private DigitalInputMonitor(int pollIntervalMs = 250)
        {
            _pollIntervalMs = Math.Max(50, Math.Min(5000, pollIntervalMs));
            LoadConfigFromIni();
        }

        /// <summary>Load port/baud from setup.ini [setport].</summary>
        private void LoadConfigFromIni()
        {
            var ini = glob_ini_instance.getInstance()?.getSetupIniData;
            if (ini == null)
            {
                PortName = "COM1";
                BaudRate = 9600;
                return;
            }
            var section = ini["setport"];
            PortName = section?[INI_PORT_KEY] ?? "COM1";
            int.TryParse(section?[INI_BAUD_KEY] ?? "9600", out int baud);
            BaudRate = baud > 0 ? baud : 9600;
        }

        // ─── Start / Stop ────────────────────────────────────────
        public void Start()
        {
            lock (_lock)
            {
                if (_running) return;
                if (_disposed) throw new ObjectDisposedException(nameof(DigitalInputMonitor));

                if (_port == null || !_port.IsOpen)
                    OpenPort();

                _previousState = 0;
                _running = true;
                _timer = new Timer(PollTick, null, 0, _pollIntervalMs);
            }
        }

        public void Stop()
        {
            lock (_lock)
            {
                _running = false;
                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                _timer?.Dispose();
                _timer = null;
                ClosePort();
            }
        }

        private void RestartTimer()
        {
            lock (_lock)
            {
                if (!_running) return;
                _timer?.Change(Timeout.Infinite, Timeout.Infinite);
                _timer?.Dispose();
                _timer = new Timer(PollTick, null, 0, _pollIntervalMs);
            }
        }

        // ─── Port management ─────────────────────────────────────
        private void OpenPort()
        {
            if (_port != null)
            {
                try { _port.Close(); } catch { }
                _port.Dispose();
            }

            _port = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                RtsEnable = true,
                ReadTimeout = 1000,
                WriteTimeout = 2000,
            };
            _port.Open();
        }

        private void ClosePort()
        {
            if (_port != null)
            {
                try
                {
                    if (_port.IsOpen) _port.Close();
                }
                catch { }
                _port.Dispose();
                _port = null;
            }
        }

        // ─── Polling ─────────────────────────────────────────────
        private void PollTick(object state)
        {
            if (!_running) return;

            int stateBits;
            lock (_lock)
            {
                if (!_running) return;
                stateBits = ReadAllChannels();
            }

            if (stateBits < 0) return; // read failed, error already fired

            // Detect edges
            int changed = _previousState ^ stateBits;
            if (changed == 0) return;

            int rising = stateBits & changed;   // 0→1
            int falling = _previousState & changed; // 1→0

            _previousState = stateBits;

            // Fire events for each changed channel
            for (int ch = 0; ch < 24; ch++)
            {
                int mask = 1 << ch;
                if ((changed & mask) == 0) continue;

                bool isHigh = (stateBits & mask) != 0;
                InputChanged?.Invoke(ch, isHigh);
                if (isHigh)
                    InputRising?.Invoke(ch);
                else
                    InputFalling?.Invoke(ch);
            }
        }

        // ─── Modbus read ─────────────────────────────────────────
        /// <summary>Reads all 24 discrete inputs. Returns bitmask or -1 on error.</summary>
        private int ReadAllChannels()
        {
            if (_port == null || !_port.IsOpen)
            {
                ScanError?.Invoke("Port not open");
                return -1;
            }

            try
            {
                byte[] cmd = BuildReadCommand();
                byte[] response = ExecuteModbus(cmd);
                if (response == null) return -1;

                return ParseResponse(response);
            }
            catch (Exception ex)
            {
                ScanError?.Invoke($"Read error: {ex.Message}");
                return -1;
            }
        }

        private byte[] BuildReadCommand()
        {
            byte[] cmd = (byte[])READ_CMD.Clone();
            ushort crc = Crc16(new ArraySegment<byte>(cmd, 0, 6).ToArray());
            cmd[6] = (byte)(crc & 0xFF);
            cmd[7] = (byte)((crc >> 8) & 0xFF);
            return cmd;
        }

        private byte[] ExecuteModbus(byte[] cmd)
        {
            // Flush buffers
            _port.DiscardInBuffer();
            _port.DiscardOutBuffer();

            _port.Write(cmd, 0, cmd.Length);

            // Wait for response (up to ~500ms)
            byte[] buffer = new byte[256];
            int total = 0;
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(50);
                int count = _port.BytesToRead;
                if (count > 0)
                {
                    total += _port.Read(buffer, total, Math.Min(count, buffer.Length - total));
                    if (total >= 8) break;
                }
            }

            if (total < 8)
            {
                ScanError?.Invoke("No response from DI module");
                return null;
            }

            // Validate response
            if (buffer[0] != DEV_ADDR || buffer[1] != FUNC_READ_DI || buffer[2] != 0x03)
            {
                ScanError?.Invoke("Invalid response header");
                return null;
            }

            byte[] data = new byte[total];
            Array.Copy(buffer, data, total);
            return data;
        }

        /// <summary>Parse 3 data bytes at offset 3 into a 24-bit bitmask.</summary>
        private int ParseResponse(byte[] response)
        {
            // response[3..5] = 3 data bytes
            byte[] reversed = { response[5], response[4], response[3] };
            int mask = 0;
            for (int i = 0; i < 3; i++)
            {
                mask |= reversed[i] << (8 * (2 - i));
            }
            return mask;
        }

        // ─── Synchronous read helper ────────────────────────────
        /// <summary>Read a single channel synchronously (bypasses polling).</summary>
        public int ReadChannel(int channel)
        {
            if (channel < 0 || channel > 23) return -1;
            int mask;
            lock (_lock)
            {
                mask = ReadAllChannels();
            }
            if (mask < 0) return -1;
            return (mask >> channel) & 1;
        }

        // ─── CRC16 ──────────────────────────────────────────────
        private static ushort Crc16(byte[] data)
        {
            return ModbusCrc16.Compute(data);
        }

        // ─── IDisposable ─────────────────────────────────────────
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                Stop();
            }
            _disposed = true;
        }

        ~DigitalInputMonitor()
        {
            Dispose(false);
        }
    }
}
