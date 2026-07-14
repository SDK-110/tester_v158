using System;
using System.Collections.Generic;

namespace testapp.mylib.canopen
{
    /// <summary>PDO data from a received CANopen PDO message.</summary>
    public class PdoData : EventArgs
    {
        public byte PdoNumber { get; set; }
        public byte[] RawData { get; set; }
        public Dictionary<string, object> Values { get; set; } = new Dictionary<string, object>();

        public T Get<T>(string name)
        {
            if (Values.TryGetValue(name, out var val))
                return (T)Convert.ChangeType(val, typeof(T));
            throw new KeyNotFoundException($"PDO value '{name}' not found");
        }

        public T Get<T>(int channelIndex)
        {
            string key = $"Ch{channelIndex}";
            if (Values.TryGetValue(key, out var val))
                return (T)Convert.ChangeType(val, typeof(T));
            throw new KeyNotFoundException($"PDO channel {channelIndex} not found");
        }
    }
}
