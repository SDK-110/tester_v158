namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Modbus function codes used by the Flamel Biocide board.
    /// </summary>
    public enum ModbusFunctionCode : byte
    {
        /// <summary>Read Coils (FC 01)</summary>
        ReadCoils = 0x01,

        /// <summary>Read Discrete Inputs (FC 02)</summary>
        ReadDiscreteInputs = 0x02,

        /// <summary>Read Holding Registers (FC 03)</summary>
        ReadHoldingRegisters = 0x03,

        /// <summary>Read Input Registers (FC 04)</summary>
        ReadInputRegisters = 0x04,

        /// <summary>Write Single Coil (FC 05)</summary>
        WriteSingleCoil = 0x05,

        /// <summary>Write Single Register (FC 06)</summary>
        WriteSingleRegister = 0x06,

        /// <summary>Write Multiple Coils (FC 15)</summary>
        WriteMultipleCoils = 0x0F,

        /// <summary>Write Multiple Registers (FC 16)</summary>
        WriteMultipleRegisters = 0x10
    }
}
