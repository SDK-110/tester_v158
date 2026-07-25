namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Abstraction over the serial transport layer so the Modbus master
    /// can work with a real <see cref="System.IO.Ports.SerialPort"/> or a
    /// simulated provider for testing without hardware.
    /// </summary>
    public interface ISerialPortProvider
    {
        /// <summary>Open the port.</summary>
        void Open();

        /// <summary>Close the port.</summary>
        void Close();

        /// <summary>Whether the port is currently open.</summary>
        bool IsOpen { get; }

        /// <summary>Write bytes to the port.</summary>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Read bytes into the buffer. Blocks until data is available or timeout.
        /// Returns the number of bytes actually read.
        /// </summary>
        int Read(byte[] buffer, int offset, int count);

        /// <summary>Number of bytes available to read immediately.</summary>
        int BytesToRead { get; }
    }
}
