// mylib/canopen/SLCANTransport.cs
using System;
using System.IO.Ports;

namespace testapp.mylib.canopen
{
    public interface ISLCANTransport
    {
        bool IsOpen { get; }
        void Open();
        void Close();
        void Send(uint cobId, byte[] data, bool isExtended = false, bool isRemote = false);
        event EventHandler<CanFrame> FrameReceived;
    }

    /// <summary>SLCAN transport using direct SerialPort (not SLCANSerialPort wrapper)
    /// to avoid DataReceived event conflicts.</summary>
    public class SLCANSerialPortTransport : ISLCANTransport, IDisposable
    {
        private SerialPort _port;
        private readonly string _comPort;
        private readonly int _baudRate;

        public SLCANSerialPortTransport(string comPort, int baudRate = 500000)
        {
            _comPort = comPort;
            _baudRate = baudRate;
        }

        public bool IsOpen => _port != null && _port.IsOpen;

        public void Open()
        {
            Close();
            _port = new SerialPort(_comPort, _baudRate);
            _port.NewLine = "\r";
            _port.ReadTimeout = 500;
            _port.WriteTimeout = 2000;
            _port.Open();
            // SLCAN init: set CAN bitrate and open CAN channel
            _port.WriteLine("S4\r");   // 500kbps
            System.Threading.Thread.Sleep(50);
            _port.WriteLine("O\r");    // open CAN
            System.Threading.Thread.Sleep(50);
            _port.DataReceived += OnDataReceived;
        }

        public void Close()
        {
            if (_port != null)
            {
                _port.DataReceived -= OnDataReceived;
                try { _port.Close(); } catch { }
                try { _port.Dispose(); } catch { }
                _port = null;
            }
        }

        public void Send(uint cobId, byte[] data, bool isExtended = false, bool isRemote = false)
        {
            if (_port == null || !_port.IsOpen)
                throw new InvalidOperationException("SLCAN port not open");
            string frame = BuildSLCANFrame(cobId, data, isExtended, isRemote);
            lock (_port) { _port.WriteLine(frame); }
        }

        public event EventHandler<CanFrame> FrameReceived;

        public void Dispose()
        {
            Close();
        }

        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var sp = (SerialPort)sender;
            try
            {
                while (sp.IsOpen && sp.BytesToRead > 0)
                {
                    string line = sp.ReadLine();
                    if (string.IsNullOrEmpty(line) || line.Length < 6) continue;
                    var frame = ParseSLCANLine(line);
                    if (frame.HasValue)
                        FrameReceived?.Invoke(this, frame.Value);
                }
            }
            catch { }
        }

        private static string BuildSLCANFrame(uint cobId, byte[] data, bool isExtended, bool isRemote)
        {
            if (isExtended)
            {
                string id = cobId.ToString("X8");
                string dlc = (data?.Length ?? 0).ToString("X1");
                string payload = BitConverter.ToString(data ?? new byte[0]).Replace("-", "");
                return "T" + id + dlc + payload;
            }
            else
            {
                string id = cobId.ToString("X3");
                string dlc = (data?.Length ?? 0).ToString("X1");
                string payload = BitConverter.ToString(data ?? new byte[0]).Replace("-", "");
                return "t" + id + dlc + payload;
            }
        }

        private static CanFrame? ParseSLCANLine(string line)
        {
            try
            {
                if (line.Length < 6) return null;
                char type = line[0];
                bool ext;
                int idLen, dataStart;
                if (type == 't') { ext = false; idLen = 3; dataStart = 5; }
                else if (type == 'T') { ext = true; idLen = 8; dataStart = 10; }
                else return null;

                if (line.Length < dataStart) return null;
                uint cobId = Convert.ToUInt32(line.Substring(1, idLen), 16);
                int dlc = Convert.ToInt32(line.Substring(1 + idLen, 1), 16);
                string hexData = line.Substring(dataStart).Trim();
                byte[] data = new byte[Math.Min(dlc, hexData.Length / 2)];
                for (int i = 0; i < data.Length; i++)
                    data[i] = Convert.ToByte(hexData.Substring(i * 2, 2), 16);
                return new CanFrame(cobId, data, isRemote: false, isExtended: ext);
            }
            catch { return null; }
        }
    }

    public class VirtualSLCANTransport : ISLCANTransport
    {
        private bool _isOpen;
        private readonly object _lock = new object();
        private event EventHandler<CanFrame> _frameReceived;
        private VirtualSLCANTransport _loopbackPeer;

        public bool IsOpen => _isOpen;

        public void Open() { _isOpen = true; }
        public void Close() { _isOpen = false; }

        public void Send(uint cobId, byte[] data, bool isExtended = false, bool isRemote = false)
        {
            if (!_isOpen) return;
            var frame = new CanFrame(cobId, data, isRemote, isExtended);
            lock (_lock)
            {
                _frameReceived?.Invoke(this, frame);
                _loopbackPeer?._frameReceived?.Invoke(_loopbackPeer, frame);
            }
        }

        public event EventHandler<CanFrame> FrameReceived
        {
            add { lock (_lock) { _frameReceived += value; } }
            remove { lock (_lock) { _frameReceived -= value; } }
        }

        public static void LinkPair(VirtualSLCANTransport a, VirtualSLCANTransport b)
        {
            a._loopbackPeer = b;
            b._loopbackPeer = a;
        }
    }
}
