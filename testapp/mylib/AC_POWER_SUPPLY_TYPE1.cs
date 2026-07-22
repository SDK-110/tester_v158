using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;


namespace testapp.mylib
{


    class AC_POWER_SUPPLY_TYPE1 : SerialPort
    {

        public AC_POWER_SUPPLY_TYPE1(string port, int baudrate) : base(port)
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

            base.WriteLine("POFF");
            
        }

        private void ac_power_aps5000A_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public void setcurrent( string value)
        {
            System.Threading.Thread.Sleep(30);
            this.WriteLine("SVOL "  + value);
        }
        public string getcurrent()
        {
            this.DiscardInBuffer();
            this.WriteLine("?SUR");
            System.Threading.Thread.Sleep(30);
            return this.ReadLine();
        }

        public void setvolatage( string value)
        {
            System.Threading.Thread.Sleep(30);
            this.WriteLine("SVOL " + value);
        }


        public void set_vol_slowly(double target_v, double spantime) { 
        
        
            


        
        
        
        }


        public string getvolatage()
        {

            this.DiscardInBuffer();
            this.WriteLine("?SVOL");
            System.Threading.Thread.Sleep(30);
            return this.ReadLine();
        }


        public void setfreq(string value)
        {
            System.Threading.Thread.Sleep(30);
            this.WriteLine("SFRE " + value);
        }


        public string getfreq()
        {

            this.DiscardInBuffer();
            this.WriteLine("?SFRE");
            System.Threading.Thread.Sleep(30);
            return this.ReadLine();
        }


        public void  set_V_I_F(string[] seter) {

            setvolatage(seter[0]);
            setfreq(seter[2]);
            setcurrent(seter[1]);
        
        
        }

        public void set_on_off(string on_off) {

            if (on_off == "on")
            {

                this.WriteLine("PON");

            }
            else {

                this.WriteLine("POFF");
            
            
            }
        
        
        }


        public void setoff() {


            this.WriteLine("POFF");
        
        
        }
        ~AC_POWER_SUPPLY_TYPE1()
        {
            this.Close();
        }
    }

}
