using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class myrelay : SerialPort
    {
        string recebuf;
        private UInt32 relay_rec= Convert.ToUInt32("111111111111111111111111111", 2);

        public UInt32 get_relay_rec
        {
            get {

                return relay_rec;
            }
        
        }
        public myrelay(string port, int baudrate=115200) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DtrEnable = true;
            //     base.WriteTimeout = 2000;
            base.ReadTimeout = 1000;
            //   base.DataReceived += Relay_aputus_DataReceived;

            base.Open();
        }

        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }

        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

 
        public void set_rly(String setrly)
        {
            relay_rec = Convert.ToUInt32(setrly.Replace("@", ""), 2);
            this.DiscardOutBuffer();
            if (setrly.Length >= 27)
            {
                String tm = setrly.Trim();
                this.WriteLine(tm);

                try
                {
                    this.ReadLine();
                }
                catch (Exception)
                {

                    System.Windows.Forms.MessageBox.Show("通讯不畅，请反馈技术人员");

                }
            }
        }
   
        




        ~myrelay()
        {
            this.Close();
        }

    }

}

