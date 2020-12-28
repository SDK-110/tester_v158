using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace testapp
{
    class festoolRFID : SerialPort
    {

        public festoolRFID(string port, int baudrate) : base(port)
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

        public byte[] read(int len)
        {

            byte[] m = new byte[len];

            for (int i = 0; i < len; i++) { m[i] = 0; }
            this.Read(m, 0, len);

            return m;
        
        }

        public  string  readbystring(int len)
        {

            byte[] m = new byte[len];
            StringBuilder temp = new StringBuilder();
            for (int i = 0; i < len; i++) { m[i] = 0; }
            this.Read(m, 0, len);

            for (int i = 0; i < len; i++) {

               temp.Append (string.Format("{0:x2}", m[i]));
                
            }

            return temp.ToString();
        }

        public void write_comm(byte[] z)
        {


            this.Write(z, 0, z.Length);

        }

        public  DateTime byteA2time(byte[] m)
        {

            return new DateTime(m[0] >> 1, ((m[0] << 7) >> 7 * 8 + m[1] >> 5), (m[1] << 3 >> 3), m[2], m[3], 0);


        }

        public  byte[] time2byte(DateTime dt)
        {

            uint m = 0;
            byte[] p = new byte[] { 0, 0, 0, 0 };
            int y = dt.Year % 2000;
            int mo = dt.Month;
            int d = dt.Day;
            int h = dt.Hour;
            int mi = dt.Minute;


            m = (uint)((y << 25) + (mo << 21) + (h << 8) + mi);

            p[0] = (byte)(m >> 24);
            p[1] = (byte)((m << 8) >> 16);
            p[2] = (byte)((m << 16) >> 24);
            p[3] = (byte)((m << 24) >> 24);
            return p;

        }




        ~festoolRFID()
        {
            this.Close();
        }
    }
}

