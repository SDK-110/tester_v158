// mylib/canopen/CanOpenTypes.cs
namespace testapp.mylib.canopen
{
    public enum NMTState : byte
    {
        Unknown = 0xFF,
        Initialising = 0x00,
        Stopped = 0x04,
        Operational = 0x05,
        PreOperational = 0x7F
    }

    public enum NMTCommand : byte
    {
        Start = 0x01,
        Stop = 0x02,
        EnterPreOp = 0x80,
        ResetNode = 0x81,
        ResetComm = 0x82
    }

    public enum SDOAbortCode : uint
    {
        ToggleNotAlternated = 0x05040001,
        OutOfMemory = 0x05040005,
        UnsupportedAccess = 0x06010000,
        AttemptReadWriteOnly = 0x06010001,
        AttemptWriteReadOnly = 0x06010002,
        ObjectNotExist = 0x06020000,
        PDOMappingError = 0x06040041,
        GeneralError = 0x08000000
    }

    public enum COBFunction : byte
    {
        NMT       = 0x00,
        SYNC      = 0x01,
        EMCY      = 0x01, /// SYNC=0x01 and EMCY=0x01 share function code; differentiated by node-ID in COB-ID
        TPDO1     = 0x03,
        RPDO1     = 0x04,
        TPDO2     = 0x05,
        RPDO2     = 0x06,
        TPDO3     = 0x07,
        RPDO3     = 0x08,
        TPDO4     = 0x09,
        RPDO4     = 0x0A,
        SDOTx     = 0x0B,
        SDORx     = 0x0C,
        Heartbeat = 0x0E
    }

    public static class CANopenID
    {
        public const uint NMT           = 0x000;
        public const uint SYNC          = 0x080;

        public static uint EMCY(byte nodeId)     => (uint)(0x080 + nodeId);
        public static uint TPDO1(byte nodeId)    => (uint)(0x180 + nodeId);
        public static uint RPDO1(byte nodeId)    => (uint)(0x200 + nodeId);
        public static uint TPDO2(byte nodeId)    => (uint)(0x280 + nodeId);
        public static uint RPDO2(byte nodeId)    => (uint)(0x300 + nodeId);
        public static uint TPDO3(byte nodeId)    => (uint)(0x380 + nodeId);
        public static uint RPDO3(byte nodeId)    => (uint)(0x400 + nodeId);
        public static uint TPDO4(byte nodeId)    => (uint)(0x480 + nodeId);
        public static uint RPDO4(byte nodeId)    => (uint)(0x500 + nodeId);
        public static uint SDOTx(byte nodeId)   => (uint)(0x580 + nodeId);
        public static uint SDORx(byte nodeId)   => (uint)(0x600 + nodeId);
        public static uint Heartbeat(byte nodeId) => (uint)(0x700 + nodeId);

        /// <summary>Extract the node-ID from a CANopen COB-ID (bits 0-6).</summary>
        public static byte NodeIdFromCOBID(uint cobId) => (byte)(cobId & 0x7F);
        /// <summary>Extract the function code from a CANopen COB-ID (bits 7-10).</summary>
        public static byte FunctionFromCOBID(uint cobId) => (byte)((cobId >> 7) & 0x0F);
    }
}
