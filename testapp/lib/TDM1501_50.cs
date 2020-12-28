using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace ClassLibrary1
{
    class TDM1501_50 : SerialPort
    {

        public TDM1501_50(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
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

            write_comm(new Byte[] { 0xAA, 0x55, 0x02, 0xFE, 0x01, 0x00 });
            System.Threading.Thread.Sleep(100);
            this.Read(m, 0, m.Length);
            if (m[0] == 170)
            {

                float z = (float)((UInt16)((m[5] * 256 + m[4])) / 100.00);
                if (z > 60) z = 60;
                return z;  


            }
            return (float)(-60);
        }

        public void write_comm(byte[] z)
        {


            this.Write(z, 0, z.Length);

        }




        ~TDM1501_50()
        {
            this.Close();
        }
    }
}

