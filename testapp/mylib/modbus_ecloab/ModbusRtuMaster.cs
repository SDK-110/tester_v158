using System;
using System.Threading;

namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Modbus RTU Master — builds request frames, sends them over a serial
    /// port, and parses the slave's response.
    ///
    /// Frame format (RTU):
    ///   [Slave Addr 1B][Function Code 1B][Data ...][CRC Lo 1B][CRC Hi 1B]
    ///
    /// Hardware: Flamel Biocide board, PIC32MX795F512L MCU + ISL83072 RS-485 transceiver.
    /// Serial:   115200 baud, 8 data bits, no parity, 1 stop bit (8N1).
    /// </summary>
    public class ModbusRtuMaster : IDisposable
    {
        private readonly ISerialPortProvider _port;
        private readonly int _readTimeout;
        private readonly int _writeTimeout;

        // Inter-frame gap for RTU: at least 3.5 character times.
        // At 115200 baud this is roughly 0.3 ms; we use 5 ms for safety margin.
        private const int InterFrameDelayMs = 5;

        // Modbus constant: ON value for Write Single Coil (FC05)
        private static readonly byte[] CoilOn = { 0xFF, 0x00 };
        private static readonly byte[] CoilOff = { 0x00, 0x00 };

        /// <summary>
        /// Create a Modbus RTU master bound to a serial port provider.
        /// </summary>
        /// <param name="port">Serial transport (real or simulated).</param>
        /// <param name="readTimeout">Response read timeout in ms (default 1000).</param>
        /// <param name="writeTimeout">Write timeout in ms (default 500).</param>
        public ModbusRtuMaster(ISerialPortProvider port, int readTimeout = 1000, int writeTimeout = 500)
        {
            _port = port ?? throw new ArgumentNullException(nameof(port));
            _readTimeout = readTimeout;
            _writeTimeout = writeTimeout;
        }

        // ──────────────────────────────────────────────────────────────
        //  Public Modbus operations
        // ──────────────────────────────────────────────────────────────

        /// <summary>
        /// Read Input Registers (FC 04). Used for sensor states, firmware version,
        /// conductivity, product level, board type, etc. on the Flamel board.
        /// </summary>
        public ushort[] ReadInputRegisters(byte slaveId, ushort startAddress, ushort count)
        {
            if (count == 0 || count > 125)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be 1..125");

            byte[] response = SendRequest(slaveId, (byte)ModbusFunctionCode.ReadInputRegisters,
                BuildReadRequest(startAddress, count));

            int byteCount = response[2];
            if (byteCount != count * 2)
                throw new ModbusException($"Byte count mismatch: expected {count * 2}, got {byteCount}");

            ushort[] result = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = (ushort)((response[3 + i * 2] << 8) | response[4 + i * 2]);
            }
            return result;
        }

        /// <summary>
        /// Read Holding Registers (FC 03). Used for control register 185 on the Flamel board.
        /// </summary>
        public ushort[] ReadHoldingRegisters(byte slaveId, ushort startAddress, ushort count)
        {
            if (count == 0 || count > 125)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be 1..125");

            byte[] response = SendRequest(slaveId, (byte)ModbusFunctionCode.ReadHoldingRegisters,
                BuildReadRequest(startAddress, count));

            int byteCount = response[2];
            if (byteCount != count * 2)
                throw new ModbusException($"Byte count mismatch: expected {count * 2}, got {byteCount}");

            ushort[] result = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = (ushort)((response[3 + i * 2] << 8) | response[4 + i * 2]);
            }
            return result;
        }

        /// <summary>
        /// Read Coils (FC 01). Used for relay status on the Flamel board.
        /// </summary>
        public bool[] ReadCoils(byte slaveId, ushort startAddress, ushort count)
        {
            if (count == 0 || count > 2000)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be 1..2000");

            byte[] response = SendRequest(slaveId, (byte)ModbusFunctionCode.ReadCoils,
                BuildReadRequest(startAddress, count));

            int byteCount = response[2];
            int expectedBytes = (count + 7) / 8;
            if (byteCount != expectedBytes)
                throw new ModbusException($"Byte count mismatch: expected {expectedBytes}, got {byteCount}");

            bool[] result = new bool[count];
            for (int i = 0; i < count; i++)
            {
                int byteIndex = 3 + i / 8;
                int bitIndex = i % 8;
                result[i] = (response[byteIndex] & (1 << bitIndex)) != 0;
            }
            return result;
        }

        /// <summary>
        /// Write Single Coil (FC 05). Used to control relays on the Flamel board.
        /// </summary>
        public void WriteSingleCoil(byte slaveId, ushort address, bool value)
        {
            byte[] pdu = new byte[4];
            pdu[0] = (byte)(address >> 8);
            pdu[1] = (byte)(address & 0xFF);
            byte[] coilValue = value ? CoilOn : CoilOff;
            pdu[2] = coilValue[0];
            pdu[3] = coilValue[1];

            SendRequest(slaveId, (byte)ModbusFunctionCode.WriteSingleCoil, pdu);
        }

        /// <summary>
        /// Write Single Register (FC 06). Used for control register 185.
        /// </summary>
        public void WriteSingleRegister(byte slaveId, ushort address, ushort value)
        {
            byte[] pdu = new byte[4];
            pdu[0] = (byte)(address >> 8);
            pdu[1] = (byte)(address & 0xFF);
            pdu[2] = (byte)(value >> 8);
            pdu[3] = (byte)(value & 0xFF);

            SendRequest(slaveId, (byte)ModbusFunctionCode.WriteSingleRegister, pdu);
        }

        // ──────────────────────────────────────────────────────────────
        //  Internal: frame building, sending, receiving
        // ──────────────────────────────────────────────────────────────

        private static byte[] BuildReadRequest(ushort startAddress, ushort count)
        {
            byte[] pdu = new byte[4];
            pdu[0] = (byte)(startAddress >> 8);
            pdu[1] = (byte)(startAddress & 0xFF);
            pdu[2] = (byte)(count >> 8);
            pdu[3] = (byte)(count & 0xFF);
            return pdu;
        }

        private static byte[] BuildFrame(byte slaveId, byte functionCode, byte[] pdu)
        {
            byte[] frame = new byte[1 + 1 + pdu.Length + 2];
            frame[0] = slaveId;
            frame[1] = functionCode;
            Buffer.BlockCopy(pdu, 0, frame, 2, pdu.Length);

            ushort crc = Crc16.Compute(frame, 0, frame.Length - 2);
            frame[frame.Length - 2] = (byte)(crc & 0xFF);
            frame[frame.Length - 1] = (byte)((crc >> 8) & 0xFF);
            return frame;
        }

        private byte[] SendRequest(byte slaveId, byte functionCode, byte[] pdu)
        {
            byte[] frame = BuildFrame(slaveId, functionCode, pdu);

            if (!_port.IsOpen)
                throw new ModbusException("Serial port is not open");

            Thread.Sleep(InterFrameDelayMs);
            _port.Write(frame, 0, frame.Length);

            return ReceiveResponse(slaveId, functionCode);
        }

        private byte[] ReceiveResponse(byte expectedSlaveId, byte expectedFc)
        {
            byte[] header = ReadBytes(2, _readTimeout);
            byte respSlave = header[0];
            byte respFc = header[1];

            if (respSlave != expectedSlaveId)
                throw new ModbusException($"Response from wrong slave: expected {expectedSlaveId}, got {respSlave}");

            if ((respFc & 0x80) != 0)
            {
                byte[] rest = ReadBytes(3, _readTimeout);
                byte exceptionCode = rest[0];
                throw new ModbusException(expectedSlaveId, respFc, exceptionCode,
                    $"Modbus exception from slave {expectedSlaveId}: {ModbusException.DescribeExceptionCode(exceptionCode)}");
            }

            if (respFc != expectedFc)
                throw new ModbusException($"Function code mismatch: expected 0x{expectedFc:X2}, got 0x{respFc:X2}");

            byte[] data;
            if (respFc == (byte)ModbusFunctionCode.ReadInputRegisters ||
                respFc == (byte)ModbusFunctionCode.ReadHoldingRegisters ||
                respFc == (byte)ModbusFunctionCode.ReadCoils ||
                respFc == (byte)ModbusFunctionCode.ReadDiscreteInputs)
            {
                byte[] byteCountBuf = ReadBytes(1, _readTimeout);
                byte byteCount = byteCountBuf[0];
                byte[] remaining = ReadBytes(byteCount + 2, _readTimeout);
                data = new byte[1 + byteCount];
                data[0] = byteCount;
                Buffer.BlockCopy(remaining, 0, data, 1, byteCount);

                byte[] fullFrame = new byte[5 + byteCount];
                fullFrame[0] = respSlave;
                fullFrame[1] = respFc;
                fullFrame[2] = byteCount;
                Buffer.BlockCopy(remaining, 0, fullFrame, 3, byteCount + 2);
                ValidateCrc(fullFrame);
            }
            else
            {
                byte[] remaining = ReadBytes(6, _readTimeout);
                data = new byte[4];
                Buffer.BlockCopy(remaining, 0, data, 0, 4);

                byte[] fullFrame = new byte[8];
                fullFrame[0] = respSlave;
                fullFrame[1] = respFc;
                Buffer.BlockCopy(remaining, 0, fullFrame, 2, 6);
                ValidateCrc(fullFrame);
            }

            byte[] result = new byte[2 + data.Length];
            result[0] = respSlave;
            result[1] = respFc;
            Buffer.BlockCopy(data, 0, result, 2, data.Length);
            return result;
        }

        private static void ValidateCrc(byte[] frame)
        {
            ushort computed = Crc16.Compute(frame, 0, frame.Length - 2);
            ushort received = (ushort)((frame[frame.Length - 1] << 8) | frame[frame.Length - 2]);
            if (computed != received)
                throw new ModbusException($"CRC mismatch: computed 0x{computed:X4}, received 0x{received:X4}");
        }

        private byte[] ReadBytes(int count, int timeoutMs)
        {
            byte[] buffer = new byte[count];
            int totalRead = 0;
            int elapsed = 0;
            int pollInterval = 5;

            while (totalRead < count)
            {
                if (_port.BytesToRead > 0)
                {
                    int toRead = Math.Min(count - totalRead, _port.BytesToRead);
                    int actuallyRead = _port.Read(buffer, totalRead, toRead);
                    totalRead += actuallyRead;
                }
                else
                {
                    Thread.Sleep(pollInterval);
                    elapsed += pollInterval;
                    if (elapsed > timeoutMs)
                    {
                        throw new ModbusException(
                            $"Serial read timeout: expected {count} bytes, got {totalRead} within {timeoutMs} ms");
                    }
                }
            }
            return buffer;
        }

        public void Dispose()
        {
            _port?.Close();
        }
    }
}
