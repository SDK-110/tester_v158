using System;

namespace testapp.mylib.canopen.eds
{
    public class EDSParseException : Exception
    {
        public string EdsPath { get; }
        public int LineNumber { get; }

        public EDSParseException(string message, string edsPath = null, int line = 0)
            : base(message)
        {
            EdsPath = edsPath;
            LineNumber = line;
        }
    }
}
