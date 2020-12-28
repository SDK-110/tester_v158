using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace ClassLibrary1
{
    class GPD3033 : SerialPort
    {

        public GPD3033(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.ReadTimeout = 2000;
            base.RtsEnable = true;
            base.DtrEnable = true;

            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();

         base.WriteLine("REMOTE");
         base.WriteLine("OUT0");
        }

        private void GPD3033_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public void setcurrent(string ch, string value)
        {
          
            this.WriteLine("ISET" + ch + ":" + value);
        }
        public string getcurrent(string ch)
        {
            this.DiscardInBuffer();
            this.WriteLine("IOUT" + ch + "?");

            return this.ReadLine();
        }

        public void setvolatage(string ch, string value)
        {

            this.WriteLine("VSET" + ch + ":" + value);
        }


        public string getvolatage(string ch)
        {

            this.DiscardInBuffer();
            this.WriteLine("VOUT" + ch + "?");

            return this.ReadLine();
        }

       

        public void OUTPUT()
        {


            this.WriteLine("VOUT1");


        }
        public void NOOUTPUT()
        {


           this.WriteLine("VOUT0");


        }


        ~GPD3033()
        {
            this.Close();
        }
    }
}

