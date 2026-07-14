using System;

namespace testapp.mylib.canopen.services
{
    public class SDOException : Exception
    {
        public ushort Index { get; }
        public byte SubIndex { get; }
        public SDOAbortCode AbortCode { get; }

        public SDOException(string message, ushort index, byte subIndex, SDOAbortCode code)
            : base(message)
        {
            Index = index;
            SubIndex = subIndex;
            AbortCode = code;
        }
    }
}
