using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace DeviceLibrary
{
    class PS1024 : SerialPort
    {

        public PS1024(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.ReadTimeout = 2000;
            base.RtsEnable = true;
            base.DtrEnable = true;
            base.NewLine ="\n";
            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();
      
        }

        private void PS1024_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        void set_channel(int ch, int onoff) {

            if (onoff > 0)
            {

                this.WriteLine($"ch{ch}:ON");

            }
            else {

                this.WriteLine($"ch{ch}:OFF");

            }
                
        
        
        
        }

        ~PS1024()
        {
            this.Close();
        }
    }
}

