using System;
using System.Collections.Generic;
using testapp.mylib.canopen.eds;

namespace testapp.mylib.canopen
{
    /// <summary>Processes raw PDO data using EDS mapping into typed named values.</summary>
    public static class PDOProcessor
    {
        /// <summary>Apply EDS PDO mapping to raw PDO data, populating Values dictionary.</summary>
        public static PdoData Process(PdoData pdo, ObjectDictionary od, byte pdoNum)
        {
            if (od == null || pdo.RawData == null) return pdo;

            var mappings = GetMappings(od, pdoNum);
            int bitOffset = 0;
            foreach (var mapping in mappings)
            {
                int byteLen = (mapping.BitLength + 7) / 8;
                if (bitOffset / 8 + byteLen > pdo.RawData.Length) break;

                byte[] raw = new byte[byteLen];
                Array.Copy(pdo.RawData, bitOffset / 8, raw, 0, byteLen);
                var val = DataTypeMap.FromBytes(mapping.DataType, raw);
                pdo.Values[mapping.Name ?? $"0x{mapping.Index:X4}:{mapping.SubIndex}"] = val;
                bitOffset += mapping.BitLength;
            }
            return pdo;
        }

        private static List<PDOMappingEntry> GetMappings(ObjectDictionary od, byte pdoNum)
        {
            ushort mapIdx = (ushort)(0x1A00 + pdoNum - 1);
            var entry = od.GetEntry(mapIdx);
            var result = new List<PDOMappingEntry>();

            if (entry == null || entry.SubEntries.Count <= 1)
                return GetDefaultMapping(pdoNum);

            foreach (var kvp in entry.SubEntries)
            {
                if (kvp.Key == 0) continue; // skip "Number of entries"
                string val = kvp.Value.DefaultValue;
                if (string.IsNullOrEmpty(val)) continue;
                uint mapVal = 0;
                if (uint.TryParse(val.Replace("0x", "").Replace("0X", ""),
                    System.Globalization.NumberStyles.HexNumber,
                    System.Globalization.CultureInfo.InvariantCulture, out mapVal))
                {
                    result.Add(new PDOMappingEntry
                    {
                        Index = (ushort)((mapVal >> 16) & 0xFFFF),
                        SubIndex = (byte)((mapVal >> 8) & 0xFF),
                        BitLength = (byte)(mapVal & 0xFF),
                        Name = kvp.Value.Name
                    });
                }
            }
            return result;
        }

        private static List<PDOMappingEntry> GetDefaultMapping(byte pdoNum)
        {
            switch (pdoNum)
            {
                case 1: return new List<PDOMappingEntry>
                {
                    new PDOMappingEntry { Index = 0x60FD, SubIndex = 1, BitLength = 8, Name = "DI", DataType = 0x05 }
                };
                case 2: return new List<PDOMappingEntry>
                {
                    new PDOMappingEntry { Index = 0x6401, SubIndex = 1, BitLength = 16, Name = "AI0", DataType = 0x06 },
                    new PDOMappingEntry { Index = 0x6401, SubIndex = 2, BitLength = 16, Name = "AI1", DataType = 0x06 },
                    new PDOMappingEntry { Index = 0x6401, SubIndex = 3, BitLength = 16, Name = "AI2", DataType = 0x06 },
                    new PDOMappingEntry { Index = 0x6401, SubIndex = 4, BitLength = 16, Name = "AI3", DataType = 0x06 }
                };
                default: return new List<PDOMappingEntry>();
            }
        }
    }
}
