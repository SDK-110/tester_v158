using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace DeviceLibrary
{
    class UTE9901_Power_meter : SerialPort
    {

        public UTE9901_Power_meter(string port, int baudrate) : base(port)
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

          
        }

        private void UTE9901_Power_meter_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

    
        public double  getcurrent()
        {
            this.DiscardInBuffer();
            this.WriteLine("MEAS:VOLT" + "?");
            System.Threading.Thread.Sleep(30);
            double rs = -1;
            string rsstr = "";
            try
            {
                rsstr = this.ReadLine();
            }
            catch {
                
            }
            if (double.TryParse(rsstr, out rs) == false) return -0xffff;
            return rs;
        }

  


        public  double getvolatage()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:VOLT" + "?");
            System.Threading.Thread.Sleep(30);
            double rs = -1;
            string rsstr = "";
            try
            {
                rsstr = this.ReadLine();
            }
            catch
            {

            }
            if (double.TryParse(rsstr, out rs)==false) return -0xffff;
            return rs;
            
        }

       public  double getpf()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:PF?");
            System.Threading.Thread.Sleep(30);
            double rs = -1;
            string rsstr = "";
            try
            {
                rsstr = this.ReadLine();
            }
            catch
            {

            }
            if (double.TryParse(rsstr, out rs)==false) return -0xffff;
            return rs;
            
        }

        public double getfreq()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:FREQ?");
            System.Threading.Thread.Sleep(30);
            double rs = -1;
            string rsstr = "";
            try
            {
                rsstr = this.ReadLine();
            }
            catch
            {

            }
            if (double.TryParse(rsstr, out rs) == false) return -0xffff;
            return rs;

        }
        public double [] getall()
        {

            this.DiscardInBuffer();
            this.WriteLine("MEAS:ALL?");
            System.Threading.Thread.Sleep(30);
            double rs = -1;
            string rsstr = "";
            try
            {
                rsstr = this.ReadLine();
            }
            catch
            {

            }
            string[] array_rsstr = rsstr.Split(",".ToArray());
            if (array_rsstr.Count() <= 0 ) return new double[] { -0xffff, -0xffff, -0xffff, -0xffff };
           
            return new double[] {double.Parse(array_rsstr[0]), double.Parse(array_rsstr[1]), double.Parse(array_rsstr[2]), double.Parse(array_rsstr[3]) };

        }




        ~UTE9901_Power_meter()
        {
            this.Close();
        }
    }
}

