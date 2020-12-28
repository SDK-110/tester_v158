using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
namespace testapp
{
  public  enum PortType
    {
        RS232, USB, GPIB, LAN,None
    }

  public  class PortUltility
    {

   


        private static string ToStringFromPortType(PortType portType)
        {
            switch (portType)
            {
                case PortType.USB: return "USB";
                case PortType.GPIB: return "GPIB";
                case PortType.LAN: return "TCPIP";
                case PortType.None:return "";
                case PortType.RS232:
                default: return "ASRL";
            }
        }

        public static string[] FindAddresses(PortType portType)
        {
            List<string> list = new List<string>();
            int result = VISA32.viOpenDefaultRM(out int sesn);
            StringBuilder desc = new StringBuilder();
            result = VISA32.viFindRsrc(sesn, $"{ToStringFromPortType(portType)}?*INSTR", out int vi, out int retCount, desc);
            ThrowIfResultExcepiton(result);
            for (int i = 0; i < retCount; i++)
            {
                list.Add(desc.ToString());
                if (i != retCount - 1)
                {
                    result = VISA32.viFindNext(vi, desc);
                    ThrowIfResultExcepiton(result);
                }
            }
            return list.ToArray();
        }

        public static string[] FindAddresses()
        {
           return FindAddresses(PortType.None);
        }

        public static void ThrowIfResultExcepiton(int result)
        {
            if (result != 0 && result !=VISA32.VI_ERROR_RSRC_NFOUND)
                throw new ResultException($"无效的结果编号：{result}");
        }
    

    public static RS232PortOperator  serial_op(string address,int baudRate,Parity parity=Parity.None,StopBits stopBits=StopBits.One,int dataBits=8){


            RS232PortOperator m = new RS232PortOperator(address, baudRate, parity, stopBits, dataBits);
            m.Open();
            return m;
           

    }

public static USBPortOperator usbport_op(string res){


            USBPortOperator m = new USBPortOperator(res);
            m.Open();
            return m;


        }

public static LANPortOperator lanport_op(string lan_name){



            LANPortOperator m =  new LANPortOperator(lan_name);
            m.Open();
            return m;
        }


public static GPIBPortOperator gpibport_op(string gpib_name){



            GPIBPortOperator m=  new GPIBPortOperator(gpib_name);
            m.Open();
            return m;

        }
    
    class ResultException : Exception
    {
        public ResultException(string message) : base(message) { }
    }
}
}
