using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace DeviceLibrary
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
            System.Threading.Thread.Sleep(30);
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
            System.Threading.Thread.Sleep(30);
            return this.ReadLine();
        }

        public int set_vol_slowly(double target_v, double spantime, int times,int ch=0)
        {
            try
            {
                System.Threading.Thread.Sleep(30);
                string zzz = this.ReadExisting();
                System.Threading.Thread.Sleep(30);
                this.WriteLine("VOUT" + ch + "?");
                System.Threading.Thread.Sleep(100);
                double rs=-1;
                string m = this.ReadLine().Replace("V","").Replace(@"\n","").Replace(@"\r","");
                if (double.TryParse(m, out rs)==false) return -1;
                if (Math.Abs(rs - target_v) <= 0.01) return 1;
                if (rs >= 0)
                {


                    if ((target_v - rs) > 0)
                    {
                        double z = Math.Abs(target_v - rs) / times;

                        for (int i = 0; i <= times; i++)
                        {
                            System.Threading.Thread.Sleep((int)(spantime / times));
                            setvolatage($"{ch}", (rs + (z * i)) + "");
                        }

                    }
                    else
                    {

                        double z = Math.Abs(target_v - rs) / times;
                        for (int i = 0; i <= times; i++)
                        {
                            System.Threading.Thread.Sleep((int)(spantime / times));
                            setvolatage($"{ch}", (rs - z * i) + "");

                        }

                    }

                    setvolatage($"{ch}", target_v + "");
                    return 1;
                }


                return -2;

            }
            catch {


                return -3;

            }
        }



        public void OUTPUT()
        {

            System.Threading.Thread.Sleep(30);
            this.WriteLine("OUT1");


        }
        public void NOOUTPUT()
        {

            System.Threading.Thread.Sleep(50);
            this.WriteLine("OUT0");


        }


        ~GPD3033()
        {
            this.Close();
        }
    }
}

