using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace testapp
{
    class TDM9001_2A : SerialPort
    {
        volatile int changecount = 0;
        public TDM9001_2A(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
         //   base.Handshake = Handshake.None;
            base.RtsEnable = false;
            base.DtrEnable = false;
            base.ReadTimeout = 10000;
            // base.DataReceived += Relay_aputus_DataReceived;
            base.PinChanged += TDM9001_2A_PinChanged;
            
            if (base.IsOpen == false)
            {
                base.Open();

            }
        }

        private void TDM9001_2A_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if(e.EventType== SerialPinChange.CtsChanged)
            {
                if(((SerialPort)sender).CtsHolding == true){

                    changecount++;
                }
             
            }
           
        }

        public void  changecountclear() {

             changecount = 0;

        }

        public int getchangecount() {


            return changecount;
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
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
            this.DiscardInBuffer();
            byte[] m = { 0, 0, 0, 0, 0, 0, 0, 0 }; 

            write_comm(new byte[] { 0xAA, 0x55, 0x02, 0xFE, 0x01, 0x00 });
            try
            {
                System.Threading.Thread.Sleep(100);
                this.Read(m, 0, m.Length);
            }
            catch (Exception) {


              //  System.Windows.Forms.MessageBox.Show("min ammeter port  is error,please check it");
            }
           
            if (m[0] == 170)
            {
                int val = m[5] * 256 + m[4];

                if (val > 32768) val = val - 65536;

                float z = (float)(val / 10.00);
                if (z > 1999) z = 1999;
                return z;


            }
            return (float)-1;
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

