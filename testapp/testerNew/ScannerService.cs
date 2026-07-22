using System;
using System.IO.Ports;
using System.Text;

namespace test_antdui
{
    public class ScannerService : IDisposable
    {
        private SerialPort _port;
        private readonly StringBuilder _buffer = new StringBuilder();

        public bool IsConnected => _port?.IsOpen ?? false;
        public string PortName { get; }
        public int BaudRate { get; }

        public event Action<string> BarcodeScanned;
        public event Action<bool> ConnectionChanged;

        public ScannerService(string portName, int baudRate)
        {
            PortName = portName;
            BaudRate = baudRate;
        }

        public void Start()
        {
            if (_port != null) Stop();

            try
            {
                _port = new SerialPort(PortName, BaudRate, Parity.None, 8, StopBits.One)
                {
                    Handshake = Handshake.None,
                    RtsEnable = true,
                    ReadTimeout = 2000
                };
                _port.DataReceived += OnDataReceived;
                _port.Open();
                _port.DiscardInBuffer();
                ConnectionChanged?.Invoke(true);
            }
            catch (Exception ex)
            {
                ConnectionChanged?.Invoke(false);
                System.Diagnostics.Debug.WriteLine($"Scanner port open failed: {ex.Message}");
            }
        }

        public void Stop()
        {
            if (_port != null)
            {
                try
                {
                    if (_port.IsOpen)
                    {
                        _port.DataReceived -= OnDataReceived;
                        _port.Close();
                    }
                }
                catch { }
                _port.Dispose();
                _port = null;
            }
            ConnectionChanged?.Invoke(false);
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = _port.ReadExisting();
                _buffer.Append(data);

                string buf = _buffer.ToString();
                int idx = buf.IndexOf('\n');
                if (idx >= 0)
                {
                    string line = buf.Substring(0, idx).Trim('\r', '\n', ' ');
                    _buffer.Clear();
                    _buffer.Append(buf.Substring(idx + 1));

                    if (!string.IsNullOrEmpty(line))
                        BarcodeScanned?.Invoke(line);
                }
            }
            catch { }
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
