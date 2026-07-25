namespace testapp.mylib.modbus_ecloab
{
    /// <summary>
    /// CRC-16 calculation using the Modbus polynomial (0xA001).
    /// Used for RTU frame error checking.
    /// </summary>
    public static class Crc16
    {
        private static readonly ushort[] Table;

        static Crc16()
        {
            // Pre-compute lookup table for speed.
            Table = new ushort[256];
            for (int i = 0; i < 256; i++)
            {
                ushort crc = (ushort)i;
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001) != 0)
                        crc = (ushort)((crc >> 1) ^ 0xA001);
                    else
                        crc = (ushort)(crc >> 1);
                }
                Table[i] = crc;
            }
        }

        /// <summary>
        /// Compute CRC-16 over a portion of a byte buffer.
        /// </summary>
        public static ushort Compute(byte[] data, int offset, int length)
        {
            ushort crc = 0xFFFF;
            for (int i = offset; i < offset + length; i++)
            {
                crc = (ushort)((crc >> 8) ^ Table[(crc ^ data[i]) & 0xFF]);
            }
            return crc;
        }

        /// <summary>
        /// Compute CRC-16 over an entire byte buffer.
        /// </summary>
        public static ushort Compute(byte[] data)
        {
            return Compute(data, 0, data.Length);
        }
    }
}
