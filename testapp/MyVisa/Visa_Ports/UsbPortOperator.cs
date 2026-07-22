using NationalInstruments.Visa;
using System;
using MyVISAInstrument.Mymodule;

namespace MyVISAInstrument.Ports
{
    internal class UsbPortOperator : PortOperatorBase, IPortType
    {
        public UsbPortOperator(string address) : base(new UsbSession(address), address)
        {
            if (!address.ToUpper().Contains("USB"))
                throw new ArgumentException("该地址不含USB字样");
        }
        public PortType PortType => PortType.Usb;
    }
}