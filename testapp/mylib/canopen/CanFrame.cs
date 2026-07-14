// mylib/canopen/CanFrame.cs
using System;

namespace testapp.mylib.canopen
{
    public struct CanFrame
    {
        public uint CobId;
        public byte Dlc;
        public byte[] Data;
        public bool IsRemoteFrame;
        public bool IsExtendedFrame;

        public CanFrame(uint cobId, byte[] data, bool isRemote = false, bool isExtended = false)
        {
            CobId = cobId;
            Dlc = (byte)Math.Min(data?.Length ?? 0, 8);
            Data = data ?? new byte[0];
            IsRemoteFrame = isRemote;
            IsExtendedFrame = isExtended;
        }

        public static CanFrame NMTCommand(NMTCommand cmd, byte nodeId) =>
            new CanFrame(CANopenID.NMT, new byte[] { (byte)cmd, nodeId });

        public static CanFrame SDOUploadRequest(byte nodeId, ushort index, byte subIndex)
        {
            byte[] data = new byte[8];
            data[0] = 0x40; // CCS=2, expedited, request
            data[1] = (byte)(index & 0xFF);
            data[2] = (byte)((index >> 8) & 0xFF);
            data[3] = subIndex;
            return new CanFrame(CANopenID.SDORx(nodeId), data);
        }

        public static CanFrame SDODownloadRequest(byte nodeId, ushort index, byte subIndex, byte[] payload)
        {
            byte[] data = new byte[8];
            int len = Math.Min(payload?.Length ?? 0, 4);
            byte n = (byte)(4 - len);
            data[0] = (byte)(0x20 | (n << 2) | 0x03); // CCS=2, n, e=1 (expedited), s=1 (data set)
            data[1] = (byte)(index & 0xFF);
            data[2] = (byte)((index >> 8) & 0xFF);
            data[3] = subIndex;
            if (payload != null)
                Array.Copy(payload, 0, data, 4, len);
            return new CanFrame(CANopenID.SDORx(nodeId), data);
        }

        public static CanFrame SYNC() =>
            new CanFrame(CANopenID.SYNC, new byte[0]); // CiA 301: SYNC has no data payload

        public override string ToString() =>
            $"COB-ID=0x{CobId:X3} DLC={Dlc} Data={BitConverter.ToString(Data)}";
    }
}
