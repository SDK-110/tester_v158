using NationalInstruments.Visa;
using System;
using MyVISAInstrument.Mymodule;

namespace MyVISAInstrument.Ports
{
    internal class GpibPortOperator : PortOperatorBase, IPortType
    {
        public GpibPortOperator(string address) : base(new GpibSession(address), address)
        {
            if (!address.ToUpper().Contains("GPIB"))
                throw new ArgumentException($"该地址不含GPIB字样");
        }
        public PortType PortType => PortType.Gpib;
    }
}