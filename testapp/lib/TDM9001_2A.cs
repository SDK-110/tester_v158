using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace ClassLibrary1
{
    class TDM9001_2A : SerialPort
    {

        public TDM9001_2A(string port, int baudrate) : base(port)
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

            write_comm(new byte[] { 0xAA, 0x55, 0x02, 0xFE, 0x01, 0x00 });
            try
            {
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception) {


                System.Windows.Forms.MessageBox.Show("min ammeter port  is error,please check it");
            }
            this.Read(m, 0, m.Length);
            if (m[0] == 170)
            {
                float z = (float)((UInt16)((m[5] * 256 + m[4])) / 10.00);
                if (z > 1999) z = 1999;
                return z;


            }
            return (float)-1999;
        }

        public void write_comm(byte[] z)
        {


            this.Write(z, 0, z.Length);

        }




        ~TDM9001_2A()
        {
            this.Close();
        }
    }
}

