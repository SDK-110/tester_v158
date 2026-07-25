using System.IO.Ports;

namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Wraps <see cref="System.IO.Ports.SerialPort"/> to implement
    /// <see cref="ISerialPortProvider"/> for real hardware communication.
    ///
    /// Flamel board serial parameters:
    ///   Baud rate: 115200
    ///   Data bits: 8
    ///   Parity:    None
    ///   Stop bits: One
    ///   Handshake: None (RTU mode, no flow control)
    /// </summary>
    public class SerialPortProvider : ISerialPortProvider
    {
        private readonly SerialPort _serialPort;

        public SerialPortProvider(
            string portName,
            int baudRate = 115200,
            int dataBits = 8,
            Parity parity = Parity.None,
            StopBits stopBits = StopBits.One)
        {
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits)
            {
                Handshake = Handshake.None,
                ReadTimeout = 1000,
                WriteTimeout = 500,
                ReadBufferSize = 4096,
                WriteBufferSize = 4096,
                RtsEnable = false,
                DtrEnable = false
            };
        }

        public void Open()
        {
            if (!_serialPort.IsOpen)
                _serialPort.Open();
        }

        public void Close()
        {
            if (_serialPort.IsOpen)
                _serialPort.Close();
        }

        public bool IsOpen => _serialPort.IsOpen;

        public void Write(byte[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        public int BytesToRead => _serialPort.BytesToRead;
    }
}
