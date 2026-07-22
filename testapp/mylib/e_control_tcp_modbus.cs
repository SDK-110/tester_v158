using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using SharpModbus;
using SharpExModule;
using System.Net.Sockets;

namespace testapp
{
    class e_control_tcp_modbus 
    {
        string recebuf;
        ModbusMaster tcpipmodbus;

        public e_control_tcp_modbus(string hostip, int port=502) 
        {

            tcpipmodbus = ModbusMaster.TCP(hostip, port);

            int a = tcpipmodbus.ReadHoldingRegister(1, 0);


        }

        public int get_regeiter_value(int slaveid ,int addr) {

            return tcpipmodbus.ReadHoldingRegister((byte)slaveid, (byte)addr);
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region //没有用的函数
        private UInt16 crc(Byte [] data) {

          UInt16 a = 0;
            for (int i = 0; i < data.Length; i++) {

                a += (UInt16)data[i];
            
            }
            a %= 0x100;
            return a;
        }

        private Byte[] tran_crc(Byte[] data) {

            Byte[] a = data;
            UInt16 cr = crc(data);
            a[a.Length - 1] = (byte)cr;

            return a;
        }

     
        #endregion

      
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~e_control_tcp_modbus() {
            if (tcpipmodbus != null) tcpipmodbus.Dispose();
        }

    }

}

