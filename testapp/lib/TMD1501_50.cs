using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace ClassLibrary1
{
    class TMD1501_50 : SerialPort
    {

        public TMD1501_50(string port, int baudrate) : base(port)
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
            
            //this.Write(new Byte[] { 0xAA, 0x55, 0x02, 0xF4, 0x00, 0xF6 }, 0, 6);
            //System.Threading.Thread.Sleep(200);
            //if (this.ReadByte()!=170) {

            //    throw new Exception("min current miter error ");
            //}


        }

        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

        public float read()
        {
           
                byte[] m = { 0, 0, 0, 0, 0, 0, 0, 0 };

                write_comm(new Byte[] { 0xAA, 0x55, 0x02, 0xFE, 0x01, 0x00 });
                System.Threading.Thread.Sleep(100);
            try
            {
                this.Read(m, 0, m.Length);
            }

            catch (Exception)
            {



                System.Windows.Forms.MessageBox.Show("min voltage meter is error,please check it");
            }
            if (m[0] == 170)
                {

                    float z = (float)((UInt16)((m[5] * 256 + m[4])) / 100.00);
                    if (z > 60) z = 60;
                    return z;



                }
                return (float)-60;
            
        
        }

        public void write_comm(byte[] z)
        {


            this.Write(z, 0, z.Length);

        }




        ~TMD1501_50()
        {
            this.Close();
        }
    }
}

