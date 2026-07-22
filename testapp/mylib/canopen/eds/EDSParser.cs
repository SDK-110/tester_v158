using System;
using System.Globalization;
using System.IO;
using System.Text;
using IniParser;
using IniParser.Model;

namespace testapp.mylib.canopen.eds
{
    /// <summary>Parses CANopen EDS/DCF files into an ObjectDictionary.
    /// Uses the existing ini-parser v2.5.2 NuGet dependency.</summary>
    public static class EDSParser
    {
        public static ObjectDictionary LoadFromFile(string edsPath)
        {
            if (!File.Exists(edsPath))
                throw new FileNotFoundException("EDS file not found", edsPath);
            var parser = new FileIniDataParser();
            IniData ini;
            try { ini = parser.ReadFile(edsPath); }
            catch (Exception ex)
            {
                throw new EDSParseException($"Failed to parse EDS: {ex.Message}", edsPath);
            }
            return Parse(ini);
        }

        public static ObjectDictionary LoadFromStream(Stream stream)
        {
            var parser = new FileIniDataParser();
            var ini = parser.ReadData(new StreamReader(stream));
            return Parse(ini);
        }

        public static ObjectDictionary LoadFromString(string edsContent)
        {
            var parser = new FileIniDataParser();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(edsContent)))
            using (var reader = new StreamReader(stream))
            {
                var ini = parser.ReadData(reader);
                return Parse(ini);
            }
        }

        private static ObjectDictionary Parse(IniData ini)
        {
            var od = new ObjectDictionary();

            var devInfo = ini["DeviceInfo"];
            if (devInfo != null)
            {
                od.VendorId = ParseHex(devInfo["VendorID"]);
                od.ProductCode = ParseHex(devInfo["ProductCode"]);
                od.RevisionNumber = ParseHex(devInfo["RevisionNumber"]);
            }

            foreach (var section in ini.Sections)
            {
                string name = section.SectionName;
                if (!name.StartsWith("Index")) continue;

                bool hasSub = name.Contains("Sub");
                string idxPart = name.Replace("Index", "");
                if (hasSub)
                {
                    string[] parts = idxPart.Split(new[] { "Sub" }, StringSplitOptions.None);
                    ushort idx = (ushort)ParseHex(parts[0]);
                    byte sub = (byte)ParseHex(parts[1]);

                    var entry = od.GetEntry(idx);
                    if (entry == null)
                    {
                        entry = new ODEntry { Index = idx };
                        od.AddEntry(entry);
                    }
                    var subEntry = entry.GetOrCreateSubEntry(sub);
                    subEntry.Name = section.Keys["ParameterName"];
                    subEntry.DataType = (byte)ParseHex(section.Keys["DataType"]);
                    subEntry.AccessType = section.Keys["AccessType"];
                    subEntry.DefaultValue = section.Keys["DefaultValue"];
                    subEntry.LowLimit = section.Keys["LowLimit"];
                    subEntry.HighLimit = section.Keys["HighLimit"];
                }
                else
                {
                    ushort idx = (ushort)ParseHex(idxPart);
                    var entry = od.GetEntry(idx);
                    if (entry == null)
                    {
                        entry = new ODEntry { Index = idx };
                        od.AddEntry(entry);
                    }
                    entry.Name = section.Keys["ParameterName"];
                    entry.ObjectType = (byte)ParseHex(section.Keys["ObjectType"]);
                    entry.DataType = (byte)ParseHex(section.Keys["DataType"]);
                    entry.AccessType = section.Keys["AccessType"];
                    entry.DefaultValue = section.Keys["DefaultValue"];
                    string pdoMap = section.Keys["PDOMapping"];
                    entry.IsPDOMappable = pdoMap == "1";
                }
            }

            return od;
        }

        private static uint ParseHex(string value)
        {
            if (string.IsNullOrEmpty(value)) return 0;
            value = value.Trim().Replace("0x", "").Replace("0X", "");
            if (uint.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out uint result))
                return result;
            return 0;
        }
    }
}
