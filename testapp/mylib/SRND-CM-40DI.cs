using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class SRND_CM_40DI : SerialPort
    {
        string recebuf;
     
       volatile  byte[] rsubyt = new byte[200];

        volatile int rev_count = 0;
    public SRND_CM_40DI(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
           
            
            
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(100);

            int m = sp.BytesToRead;
           
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            Array.Copy(tmp, rsubyt, m);
            rev_count = m;
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

        public int read_DI(int chinnel, out int rsult) {
            rev_count = 0;
            rsult = -1;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            byte[] readstr = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x38, 0x79, 0xD8 };
            this.Write(readstr, 0, readstr.Length);
            int loopcout = 10;
            while (rev_count == 0 && loopcout-- >= 0) {

                System.Threading.Thread.Sleep(50);
            }
            try
            {
                if (rev_count == 0) return -1;
                if (rev_count  >=12 && rsubyt[0] == 0x01 && rsubyt[1] == 0x02 && rsubyt[2] == 0x07 )
                {


                    //string chinnels = Convert.ToString(rsubyt[9], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[8], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[7], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[6], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[5], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[4], 2).PadLeft(8, '0') +
                    //                   Convert.ToString(rsubyt[3], 2).PadLeft(8, '0');


                    byte[] chinnes_data = new ArraySegment<byte>(rsubyt, 3, 7).Reverse().ToArray();
                    string chinnels1 = string.Join("", chinnes_data.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

                    string tmp = chinnels1.Substring(chinnels1.Length - chinnel-1, 1);
                    rsult = int.Parse(tmp);

                    return 1;
                }
                else {

                    return -2;
                }
   
            }
            catch {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -1;
            }
        }

        
        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~SRND_CM_40DI() { 
            this.Close();
           
        }

    }

}

