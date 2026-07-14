namespace testapp.mylib.canopen
{
    /// <summary>One mapped variable in a PDO.</summary>
    public class PDOMappingEntry
    {
        public ushort Index { get; set; }
        public byte SubIndex { get; set; }
        public int BitLength { get; set; }
        public string Name { get; set; }
        public byte DataType { get; set; }
    }
}
