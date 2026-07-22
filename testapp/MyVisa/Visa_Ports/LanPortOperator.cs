using NationalInstruments.Visa;
using System;
using MyVISAInstrument.Mymodule;

namespace MyVISAInstrument.Ports
{
    internal class LanPortOperator : PortOperatorBase, IPortType
    {
        public LanPortOperator(string address) : base(new TcpipSession(address), address)
        {
            if (!address.ToUpper().Contains("TCPIP"))
                throw new ArgumentException($"该地址不含TCPIP字样");
        }
        public PortType PortType => PortType.Lan;
    }
}