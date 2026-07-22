using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class hipot_tonghui9310 : SerialPort
    {
        string recebuf;
        public hipot_tonghui9310(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_DataReceived;

            base.WriteTimeout = 2000;
            base.ReadTimeout = 65000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }

        public int test_sart()
        {
            try
            {

                this.WriteLine("FUNC:START");
                return 1;
            }
            catch { 
            
                return -1;
            }
        
        }

        public string get_result() {


            try
            {
                this.ReadExisting();    
                this.WriteLine("FUNC:START");
                string rsu = this.ReadLine();
                mylib.utility_func.callbackdebuginfo("rev data:" + rsu);
                return rsu.Replace(","," ");

            }
            catch {


                return "comm err";
            
            }
        
        
        }
        public int set_stop()
        {


            try
            {
                this.ReadExisting();
                this.WriteLine("FUNC:STOP");
                return 1;

            }
            catch
            {


                return -1;

            }


        }
        ~hipot_tonghui9310()
        {
            this.Close();
        }

    }

}

