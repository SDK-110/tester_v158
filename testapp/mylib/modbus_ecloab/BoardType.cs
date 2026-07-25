namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// Board type identified by register 141 and SW5 position.
    /// </summary>
    public enum BoardType : ushort
    {
        /// <summary>SW5 = NC position, Device ID = 21, Reg 141 = 2</summary>
        NoProduct = 2,

        /// <summary>SW5 = OX position, Device ID = 22, Reg 141 = 1</summary>
        Ox = 1,

        /// <summary>SW5 = Non-OX position, Device ID = 23, Reg 141 = 0</summary>
        NonOx = 0
    }

    /// <summary>
    /// Helper to get the default slave ID for a given board type.
    /// </summary>
    public static class BoardTypeExtensions
    {
        /// <summary>
        /// Get the Modbus slave ID associated with a board type.
        /// </summary>
        public static byte DefaultSlaveId(this BoardType type)
        {
            switch (type)
            {
                case BoardType.Ox: return 22;
                case BoardType.NonOx: return 23;
                case BoardType.NoProduct: return 21;
                default: return 22;
            }
        }

        /// <summary>Human-readable name for the board type.</summary>
        public static string DisplayName(this BoardType type)
        {
            switch (type)
            {
                case BoardType.Ox: return "OX";
                case BoardType.NonOx: return "Non-OX";
                case BoardType.NoProduct: return "No Product";
                default: return $"Unknown ({(ushort)type})";
            }
        }
    }
}
