using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace testapp
{
    class led_assy : SerialPort
    {


        string recebuf;
        public led_assy(string port, int baudrate=57600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;
 
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region /*废弃*/
        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

        public string read()
        {

            string a = (recebuf == null) ? "null" : recebuf;
            recebuf = "";
            return a;
        }

        private void set_rly(byte[] m)
        {

            if (m.Length == 3)
            {

                this.send(new Byte[] { 0XB1, 0X03 });
                this.send(m);
                this.send(new Byte[] { 0X0A });

            }

        }
        private void getcolor(byte ch,byte cor) {

            this.send(new Byte[] {0xA5,0X04 });

            this.send(new byte[] { ch, cor });
            this.send(new byte[] { 0x00, 0x00, 0x0A });


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
        #endregion



        public void try_comm() {

           this.Write("GETSERIAL" + '\r');

            string m = this.ReadLine();


        }

        public int[] getRGBI(int port = 1,int trytimes=2) {

            int[] ctsreaddata = new int[] { 0, 0, 0, 0 };
            int count = 0;
            do
            {
                this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture");
                    System.Threading.Thread.Sleep(400);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                    System.Threading.Thread.Sleep(50);
                }

                string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{3})\s([0-9]{3})\s([0-9]{3})\s([0-9]{5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if (ctsreaddata[0] > 0 || ctsreaddata[1] > 0 || ctsreaddata[2] > 0 || ctsreaddata[3] > 0 || count > 3) break;
                count++;
            } while (true);


            return ctsreaddata;



        }

        public int[] getRGBI_Min(int port = 1, int trytimes =2)
        {

            int[] ctsreaddata = new int[] { 50, 50, 50, 300 };
            int count = 0;
            do
            {
                this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture2");
                    System.Threading.Thread.Sleep(400);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                    System.Threading.Thread.Sleep(50);
                }

                string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{3})\s([0-9]{3})\s([0-9]{3})\s([0-9]{5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
              
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] > int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] > int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] > int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] > int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if ((ctsreaddata[0] < 50 && ctsreaddata[1] < 50 && ctsreaddata[2] < 50 && ctsreaddata[3] < 300 )|| count > 3) break;
                count++;
            } while (true);


            return ctsreaddata;



        }


        #region 知识储备库忽略
        /*
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
                // crc校验函数
                private   UInt16 crc16(Byte[] ptr)
                {

                    UInt16 crc = 0xffff;

                    for (int i = 0; i < ptr.Length; i++)
                    {
                        crc ^= ptr[i];
                        for (int j = 0; j < 8; j++)
                            if ((crc & 1) > 0)
                            {
                                crc >>= 1;
                                crc ^= 0XA001;
                            }
                            else
                            {
                                crc >>= 1;
                            }
                    }

                return(crc);
            }

                private  Byte[] tan_modbus(Byte[] data)
                {


                    Byte[] z = new Byte[(data.Length + 2)];

                    for (int i = 0; i < data.Length; i++)
                    {


                        z[i] = data[i];
                    }

                    UInt16 temp = crc16(data);
                    z[(data.Length)] = (Byte)(((Byte)temp << 8) >> 8);
                    z[(data.Length + 1)] = (Byte)(temp >> 8);

                    return z;
                }
        */
        #endregion
        ~led_assy()
        {
            this.Close();
        }

    }

}

