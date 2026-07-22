using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xktComm.Common;
using xktComm.PLC;
using xktComm.Tools;
using xktComm;
using SharpModbus;
using System.IO.Ports;
using melsec = HslCommunication.Profinet.Melsec;
using IoTClient.Clients.Modbus;
namespace testapp.PLC
{
  public  class plc_option
    {
        xktComm.ModbusRtu ModbusRtu = new xktComm.ModbusRtu();
        HslCommunication.Profinet.Melsec.MelsecFxSerial fx2n;
        HslCommunication.Instrument.Temperature.DAM3601 DAM3601;// = new HslCommunication.Instrument.Temperature.DAM3601();
        ModbusRtuClient client;
        public plc_option()
        {
            
        }


        public static void test_sharp_modbus_com() {




            var settings = new SharpModbus.Serial.SerialSettings()
            {
                PortName = "COM38",
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                 StopBits = StopBits.One
                
            };

            using (var master = SharpModbus.ModbusMaster.RTU(settings))
            {
              
              //var p =  master.ReadCoil(1, 0XA000);
              master.WriteCoil(1, 0,  true);
                //  master.WriteCoils(1, 3001, false, true);
              var p =  master.ReadHoldingRegister(1,0);

                //master.WriteRegister(1, 0, 0x333);
            }



        }
        public void test_sharp_modbus_tcp()
        {




            using (var master = ModbusMaster.TCP("10.77.0.2", 502))
            {
                master.WriteCoils(1, 4, false, true);
            }


        }

        public static void test_ex_module_modbus() {

            /*
             支持讯威电子
             新捷
             */
            SharpExModule.Ex_ModbusMasterRTU_V1 s = new SharpExModule.Ex_ModbusMasterRTU_V1();
            bool m = s.OpenPort("COM9", 9600, Parity.None, 8, StopBits.One);
            mylib.utility_func.callbackdebuginfo(m.ToString());
           short [] Z = new short[3];

            bool [] data = new bool[32];
            data[15] = true;
            data[12] = true;
            data[11] = true;
            s.Write0x0F(1, 0x00, data);
            
            //s.Write0x06(1,0,33);

            //s.Read0x02(1, 0x5000,ref data);
            //s.Write0x05(1,0x6000, true);
            //s.Write0x06(1, 0x0, 0x22);
            //s.Read0x03(1, 0x0, ref Z);
            
        }

        public static void melsec_serial() {

            melsec.MelsecFxSerial mserial = new melsec.MelsecFxSerial();

            mserial.SerialPortInni("COM9", 19200, 7, StopBits.One, Parity.Even);
            mserial.Open();
            for (int i = 0; i < 8; i++)
            { var p = mserial.Write($"Y1{i}",true); }

        }

        public void test_fx2n() {


            //  fx2n = new HslCommunication.Profinet.Melsec.MelsecFxSerial();
            //  fx2n.SerialPortInni("com2", 9600);
            //  fx2n.Open();
            //DAM3601.SerialPortInni("com2");
            //DAM3601.Open();

            client = new ModbusRtuClient("COM2", 9600, 8, StopBits.One, Parity.None);
            client.Open();
            client.Write("0000", true, 1, 1);





        }

        ~plc_option() {




        }

    }
}
