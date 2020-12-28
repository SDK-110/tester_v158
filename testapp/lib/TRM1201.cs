using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace ClassLibrary1
{
    class TRM1201 : SerialPort
    {

        public TRM1201(string port, int baudrate = 115200) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.ReadTimeout = 2000;
            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();
        }

        //private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    SerialPort sp = (SerialPort)sender;
        //    //   recebuf = sp.ReadExisting();
        //}


        public Int16 readres(int range) {
           setrange((byte)range);
            byte[] ret = new byte[] {0,0,0,0,0,0,0,0};

            int ct = 0;

            try
            {
                do
                {
                    for (int i = 0; i < ret.Length; i++) {

                        ret[i] = 0;
                    }
                    this.Write(new byte[] { 0xAA, 0x55, 0x02, 0xFE, 0x01, 0x00 }, 0, 6);
                    System.Threading.Thread.Sleep(100);
                    this.Read(ret, 0, ret.Length);
                 
                    if (ct > 3)
                    {

                     //   System.Windows.Forms.MessageBox.Show("1电阻表通讯不畅，请注意");
                        break;
                    }
                    ct++;
                } while (ret[0] != 0xaa);

            }
            catch (Exception) {


                System.Windows.Forms.MessageBox.Show("2电阻表通讯不畅，请注意");
            }
            if((Int16)(ret[5] * 256 + ret[4]) > 1999 || (Int16)(ret[5] * 256 + ret[4])< -1999)
            {

                return -1;
            }

            if (range == 1) {

                return (Int16)(ret[5] * 256 + ret[4]);
 
            }

            if (range == 2) {

                return (Int16)((ret[5] * 256 + ret[4])*10);
            }

            if (range == 3) {

                return (Int16)((ret[5] * 256 + ret[4]) * 100);
            }

            if (range == 4) {

                return (Int16)((ret[5] * 256 + ret[4]) * 1000);

            }

            return -2;
        }
        public void setrange(byte range) {

            byte[] setr = new byte[] { 0xA1, range };
            byte[] commandb = getcrccommand(setr);

            byte[] ret = new byte[] { 0, 0, 0, 0, 0, 0};
           // int i = 0;
            try
            {
                //do
                //{
                    for (int ip = 0; ip < ret.Length; ip++)
                    {

                        ret[ip] = 0;
                    }
                    this.Write(commandb, 0, commandb.Length);
                    System.Threading.Thread.Sleep(100);
                //    this.Read(ret, 0, ret.Length);
                //    if (i > 3) { /*System.Windows.Forms.MessageBox.Show("3电阻表通讯不畅，请注意");*/ break; }
                //    i++;

                //} while (ret[0] != 0xaa);

            }
            catch (Exception) {


            }


        }

        public  byte[] getcrccommand(byte[] m)
        {

            UInt16 crctemp = 0, temp = 0;
            byte[] rs = new byte[m.Length + 5];
            for (int i = 0; i < m.Length + 5; i++)
            {
                rs[i] = 0;
            }
            for (int i = 0; i < m.Length; i++)
            {
                rs[3 + i] = m[i];
                temp = (UInt16)(temp + m[i]);

            }


            crctemp = (UInt16)(temp + m.Length + 1);
            byte bL = (byte)crctemp;
            byte bH = (Byte)(crctemp >> 8);

            rs[0] = 0xAA;
            rs[1] = 0x55;
            rs[2] = (byte)(m.Length + 1);
            rs[rs.Length - 1] = bL;
            rs[rs.Length - 2] = bH;
            return rs;
        }


        ~TRM1201()
        {
            this.Close();
        }
    }
}

