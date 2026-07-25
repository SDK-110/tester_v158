using System;

namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Thrown when a Modbus slave returns an exception response,
    /// or when a response fails validation (CRC mismatch, wrong slave, etc.).
    /// </summary>
    public class ModbusException : Exception
    {
        /// <summary>Slave address that caused the error.</summary>
        public byte SlaveId { get; }

        /// <summary>Function code from the response (high bit set if exception).</summary>
        public byte FunctionCode { get; }

        /// <summary>Modbus exception code (0 if not an exception response).</summary>
        public byte ExceptionCode { get; }

        public ModbusException(byte slaveId, byte functionCode, byte exceptionCode, string message)
            : base(message)
        {
            SlaveId = slaveId;
            FunctionCode = functionCode;
            ExceptionCode = exceptionCode;
        }

        public ModbusException(string message)
            : base(message)
        {
            SlaveId = 0;
            FunctionCode = 0;
            ExceptionCode = 0;
        }

        /// <summary>Human-readable description of a Modbus exception code.</summary>
        public static string DescribeExceptionCode(byte code)
        {
            switch (code)
            {
                case 0x01: return "Illegal Function";
                case 0x02: return "Illegal Data Address";
                case 0x03: return "Illegal Data Value";
                case 0x04: return "Slave Device Failure";
                case 0x05: return "Acknowledge";
                case 0x06: return "Slave Device Busy";
                case 0x08: return "Memory Parity Error";
                case 0x0A: return "Gateway Path Unavailable";
                case 0x0B: return "Gateway Target Device Failed to Respond";
                default: return $"Unknown (0x{code:X2})";
            }
        }
    }
}
