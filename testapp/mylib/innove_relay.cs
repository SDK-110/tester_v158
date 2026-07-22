using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class innove_relay
    {

        SharpExModule.Ex_ModbusMasterRTU_V1 relay_obj=null;
        public bool[] data = new bool[32];
       

       
        public innove_relay(string port = "COM9", int baudrate=9600) 
        {




            relay_obj = new SharpExModule.Ex_ModbusMasterRTU_V1();
            bool m = relay_obj.OpenPort(port, baudrate, Parity.None, 8, StopBits.One);

            if (!m) {
               
                System.Windows.Forms.MessageBox.Show("innove relay board open error");

            }
           
            short[] Z = new short[3];

            
      
            relay_obj.Write0x0F(1, 0x00, data);


        }



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

       
        // crc校验函数
        private   UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }


        /// <summary>
        /// 信科继电器
        /// </summary>
        /// <param name="sendcmd"></param>
        /// <returns></returns>
        private byte [] ks_crc_sum_tranc(byte[] sendcmd) {

            byte[] trancrsu = new byte[8];
            trancrsu[0] = 0x55;

            for (int i = 0; i < sendcmd.Length; i++) {

                trancrsu[i + 1] = sendcmd[i];
            }
           
          
            int  tmp = 0;

            for(int i=0;i<sendcmd.Length+1; i++) {

                tmp = tmp + trancrsu[i];
            }

            trancrsu[7] = (byte)tmp;

            return trancrsu;


        }
        /// <summary>
        /// 青岛信科电子继电器板
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public int set_relay_kbca132s() {


            try
            {
               

                relay_obj.Write0x0F(1, 0, data);
                return 1;

            }
            catch {

                return -1;
            }
            }
        public int clear_all_realy()
        {


            try
            {
                for (int i = 0; i < 32; i++)
                {

                    data[i]= false;

                }

                relay_obj.Write0x0F(1, 0, data);
                return 1;

            }
            catch
            {

                return -1;
            }
        }

        ~innove_relay()
        {
            try
            {
                relay_obj.ClosePort();
                
            }
            catch { };
        }

        public void Dispose() {

            try
            {
                relay_obj.ClosePort();

            }
            catch { };

        }

    }

}

