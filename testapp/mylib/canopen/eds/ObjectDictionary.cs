using System.Collections.Generic;
using System.Linq;

namespace testapp.mylib.canopen.eds
{
    /// <summary>In-memory object dictionary loaded from an EDS file.</summary>
    public class ObjectDictionary
    {
        public uint VendorId { get; set; }
        public uint ProductCode { get; set; }
        public uint RevisionNumber { get; set; }

        private readonly Dictionary<ushort, ODEntry> _entries = new Dictionary<ushort, ODEntry>();

        public void AddEntry(ODEntry entry)
        {
            if (entry != null)
                _entries[entry.Index] = entry;
        }

        public ODEntry GetEntry(ushort index)
        {
            _entries.TryGetValue(index, out var entry);
            return entry;
        }

        public bool TryGetEntry(ushort index, out ODEntry entry) =>
            _entries.TryGetValue(index, out entry);

        public ODSubEntry GetSubEntry(ushort index, byte subIndex)
        {
            if (_entries.TryGetValue(index, out var entry))
            {
                entry.SubEntries.TryGetValue(subIndex, out var sub);
                return sub;
            }
            return null;
        }

        public bool TryGetSubEntry(ushort index, byte subIndex, out ODSubEntry subEntry)
        {
            subEntry = null;
            return _entries.TryGetValue(index, out var entry) &&
                   entry.SubEntries.TryGetValue(subIndex, out subEntry);
        }

        /// <summary>Find an entry by name (case-insensitive substring match).</summary>
        public ODEntry FindByName(string name)
        {
            return _entries.Values.FirstOrDefault(e =>
                e.Name?.IndexOf(name, System.StringComparison.OrdinalIgnoreCase) >= 0);
        }

        /// <summary>Get index by parameter name.</summary>
        public ushort GetIndexByName(string name)
        {
            return FindByName(name)?.Index ?? 0;
        }

        public IEnumerable<ODEntry> AllEntries => _entries.Values;
        public int Count => _entries.Count;
    }
}
