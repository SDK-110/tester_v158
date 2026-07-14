using System.Collections.Generic;

namespace testapp.mylib.canopen.eds
{
    /// <summary>One object dictionary entry (0x1000-0x9FFF).</summary>
    public class ODEntry
    {
        public ushort Index { get; set; }
        public string Name { get; set; }
        public byte ObjectType { get; set; }
        public byte DataType { get; set; }
        public string AccessType { get; set; }
        public string DefaultValue { get; set; }
        public bool IsPDOMappable { get; set; }
        public Dictionary<byte, ODSubEntry> SubEntries { get; set; } = new Dictionary<byte, ODSubEntry>();

        public ODSubEntry GetOrCreateSubEntry(byte subIndex)
        {
            if (!SubEntries.ContainsKey(subIndex))
                SubEntries[subIndex] = new ODSubEntry { SubIndex = subIndex };
            return SubEntries[subIndex];
        }
    }

    /// <summary>One sub-entry of an object dictionary entry.</summary>
    public class ODSubEntry
    {
        public byte SubIndex { get; set; }
        public string Name { get; set; }
        public byte DataType { get; set; }
        public string AccessType { get; set; }
        public string DefaultValue { get; set; }
        public string LowLimit { get; set; }
        public string HighLimit { get; set; }
    }
}
