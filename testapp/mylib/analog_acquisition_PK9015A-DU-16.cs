using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using Code4Bugs.Utils.IO.Modbus;
using Code4Bugs.Utils.IO;
namespace testapp
{
    class analog_acquisition_PK9015A_DU_16 : SerialPort
    {
        string recebuf;
        SerialStream serial;

        public analog_acquisition_PK9015A_DU_16(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
            serial = new SerialStream(this);
            serial.ReadTimeout = 200;
            
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

        public void set_sing_relay(byte relay_num, byte openorclose) {

            Byte[] a = new Byte[] { 0x05, 0x01, 0X00,(byte)(relay_num-1),(byte)(openorclose),0x00 };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {

                    count++;
                    if (count > 3) {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0);
        
        
        }
        public void set_relay(byte relay_1_8, byte relay_9_16)
        {

            Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {
                    count++;
                    if (count > 3)
                    {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0 && count < 3);


        }
        #endregion

        public int read_V( int address, int leng , out double[] rsu, int ranges=50) {
            rsu = new double[] { -1 };
            try
            {
                var revbytes = serial.RequestFunc3(1, address, leng);
               
                
             
                var response = revbytes.ToResponseFunc3().Data;

                if (response.Length < 2 || response.Length % 2 != 0) return -1;
                int rsulengh = response.Length / 2;
                double[] rsubuf = new double[rsulengh];

                for (int i = 0; i < rsulengh; i++) {

                  byte h = response[i * 2];
                  byte l  = response[i * 2 + 1];

                    rsubuf[i] = (h * 256 + l) / 10000.00000 * ranges;

                }

                rsu = rsubuf;
                return 1;
            }
            catch {
              //  System.Windows.Forms.MessageBox.Show("Test");
                rsu =  new double[] { -1};
                return -2;
            }
        }

       

        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~analog_acquisition_PK9015A_DU_16() { 
            this.Close();
            serial.Dispose();
        }

    }

}

