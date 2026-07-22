using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpExModule;
using System.IO.Ports;

namespace testapp.PLC
{
   public  class modbus_sets
    {

     

        public static string ex_module_crc16_str(string hex_str= "01 01 00 64 00 01")
        {
            string[] hexValues = hex_str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] data = new byte[hexValues.Length];
            for (int i = 0; i < hexValues.Length; i++)
                data[i] = Convert.ToByte(hexValues[i], 16);
            ushort crc = ModbusCrc16.Compute(data);
            return crc.ToString("X4");
        }
        public static string bytes_to_str(byte[] byteArray /* { 0x1A, 0x2B, 0x3C, 0xFF, 0xAA }*/)
        {


            // 使用LINQ Select函数转换每个byte为十六进制字符串
            string hexString = string.Join(" ", byteArray.Select(b => b.ToString("X2")).ToArray());

            return hexString;
        }

        public static byte[] bytes_str_2_byts(string bytes_str)
        {

            return mylib.utility_func.strByts2ByteArray(bytes_str);

        }

        public static void ex_modbus_sample() {


            SharpExModule.Ex_ModbusMasterRTU mds = new SharpExModule.Ex_ModbusMasterRTU();
            mds.OpenPort("COM1", 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.None);
            bool[] s = new bool[12] ;
            mds.Read0x01(1, 0x22, ref s);
            mds.Read0x02(1, 0x22, ref s);
            long[] a = new long[15];
            mds.Read0x03(1, 0x300,ref  a);
            mds.Read0x04(1, 0x300,ref a);
            mds.Write0x05(1, 0x22, true);
            mds.Write0x06(1, 0x22, 0x55);
            mds.Write0x0F(1, 0x55, new bool[] { true, true, true, true });
            mds.Write0x10(1, 0x22, new long[] { 1, 1, 1, 1 });
           byte [] bts =  mds.ReadDate(10, 3000);
            mds.Write("fsddaf");
            mds.WriteLine("fsafsfs");

        }

        public static void testt_code4bugs_modbus()
        {
            SerialPort serialPort = new SerialPort("COM9", 19200, Parity.Even, 7, StopBits.One);

            Code4Bugs.Utils.IO.SerialStream serial = new Code4Bugs.Utils.IO.SerialStream(serialPort);

          
          byte[] p =  Code4Bugs.Utils.IO.Modbus.Modbus.RequestFunc3(serial, 1, 0x22, 3);


        }
        public static byte[] modbus_ComputeCRC16_by_code4bugs(byte [] source) {
            ushort crc = ModbusCrc16.Compute(source);
            return new byte[] { (byte)(crc & 0xFF), (byte)((crc >> 8) & 0xFF) };
        }
        static int iz = 0;
        public static void nmodubs4_test() {

            SerialPort serialPort = new SerialPort("COM18", 9600, Parity.None, 8, StopBits.One);
            serialPort.WriteTimeout = 1000;
            serialPort.ReadTimeout = 1000;
            serialPort.Open();
            var mds = Modbus.Device.ModbusSerialMaster.CreateRtu(serialPort);
            try
            {
                if(iz++%2==0)

                for (int i = 0; i < 8; i++) mds.WriteMultipleRegisters(1, 0, new ushort[] { 0X00, 0X00, 0X00, 0X00, 0X00, 0X00, 0 });
                else
                    for (int i = 0; i < 8; i++) mds.WriteMultipleRegisters(1, 0, new ushort[] { 0X01, 0X01, 0X01, 0X01, 0X01, 0X01, 0 });


            }
            catch { }
            finally {


                serialPort.Close();

            }

        }

    }
}
