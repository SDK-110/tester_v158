using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace testapp
{
    class AC_SOURCE_IVYTECH : SerialPort
    {
       double mes_voltage, mes_current, mes_fre, mes_power, mes_pf;
        public double MVOL {  get { return mes_voltage; } }
        public double MCUR {  get { return mes_current; } }
        public double MPOW {  get { return mes_power; } }
        public double MPF {  get{ return mes_pf; } }
        public AC_SOURCE_IVYTECH(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.ReadTimeout = 4000;
            base.RtsEnable = true;
            base.DtrEnable = true;

            // base.DataReceived += Relay_aputus_DataReceived;

            this.Open();

            base.WriteLine("SSHIFTH");
       //  base.WriteLine("REMOTE");
        // base.WriteLine("OUT0");
        }

        private void AC_SOURCE_IVYTECH_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public void setcurrent(string value)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
                this.ReadExisting();
                this.WriteLine("SCUR " + value);
                System.Threading.Thread.Sleep(50);
                if (this.ReadExisting().Length <= 0) continue;
                break;
            }
        }
       
        public void setvolatage(string value)
        {


            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
               
                this.ReadExisting();
                this.WriteLine("SVOL " + value);
                System.Threading.Thread.Sleep(50);
                if (this.ReadExisting().Length <= 0) continue;
                break;
            }
        
          
        }


        public void set_vol_slowly(double target_v, double spantime, int times)
        {
            System.Threading.Thread.Sleep(100);
            string zzz = this.ReadExisting();
            System.Threading.Thread.Sleep(100);
            this.WriteLine("?SVOL");
            System.Threading.Thread.Sleep(100);
            double rs;
            string m = this.ReadLine();
            double.TryParse(m, out rs);
            if (Math.Abs(rs - target_v) <= 0.01) return;
            if (rs >= 0) {


                if ((target_v - rs) > 0)
                {
                    double z = Math.Abs(target_v - rs) / times;

                    for (int i = 0; i <= times; i++)
                    {
                        System.Threading.Thread.Sleep((int)(spantime / times));
                        setvolatage((rs + (z * i)) + "");
                    }

                }
                else {

                    double z = Math.Abs(target_v - rs) / times;
                for (int i = 0; i <= times; i++)
                {
                    System.Threading.Thread.Sleep((int)(spantime / times));
                    setvolatage((rs - z*i) + "");

                }

                }

                setvolatage(target_v + "");

            }





        }


        public double get_Measure_current()
        {


            double rs = -1000;
            mes_current = -1000;
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    this.ReadExisting();
                    this.WriteLine("?MCUR");
                    System.Threading.Thread.Sleep(50);
                    string m = this.ReadLine();
                    if (!double.TryParse(m, out rs)) continue;
                }
                mes_current=rs;
                return rs;
            }
            catch
            {

                return -2000;
            }
     
        }



        public double get_measure_volatage()
        {
            double rs = -1000;
            mes_voltage = -1000;
            try
            {
              for(int i = 0; i < 3; i++) { 
                this.ReadExisting();
                this.WriteLine("?MVOL");
                System.Threading.Thread.Sleep(50);
               string m =  this.ReadLine();           
               if( !double.TryParse(m, out rs))  continue;
                }
                mes_voltage = rs;
                return rs;
            }
            catch {

                return -2000;
            }
        
        }


        public double get_measure_pow()
        {
            double rs = -1000;
            mes_power = -1000;
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    this.ReadExisting();
                    this.WriteLine("?MPOW");
                    System.Threading.Thread.Sleep(50);
                    string m = this.ReadLine();
                    if (!double.TryParse(m, out rs)) continue;
                }
                mes_power = rs;
                return rs;
            }
            catch
            {

                return -2000;
            }

        }

     

        public double get_result(int v)
        {

            switch (v)
            {
                case 1:
                    {

                        return get_measure_volatage();
                    }
                    break;
                case 2:
                    {

                        return get_Measure_current();
                    }
                    break;
                case 3:
                    {

                        return get_measure_freq();
                    }
                    break;
                case 4:
                    {

                        return get_measure_pow();
                    }
                    break;

                case 5:
                    {

                        return get_measure_mpf();
                    }
                    break;
                default:

                    return -30000;

            }


        }

        public void reset_par() {

            mes_pf = mes_current = mes_power = mes_voltage = 0;




        }
        public void setfreq(string value)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
                this.ReadExisting();
                this.WriteLine("SFRE " + value);
                System.Threading.Thread.Sleep(50);
                if (this.ReadExisting().Length <= 0) continue;
                break;
            }

           
        }


        public double get_measure_freq()
        {


            double rs = -1000;
            mes_fre = -1000;
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    this.ReadExisting();
                    this.WriteLine("?MFRE");
                    System.Threading.Thread.Sleep(50);
                    string m = this.ReadLine();
                    if (!double.TryParse(m, out rs)) continue;
                }
                mes_fre = rs;
                return rs;
            }
            catch
            {

                return -2000;
            }
          ;
        }

        public double get_measure_mpf()
        {
            double rs = -1000;
            mes_pf = -1000;
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    this.ReadExisting();
                    this.WriteLine("?MPF");
                    System.Threading.Thread.Sleep(50);
                    string m = this.ReadLine();
                    if (!double.TryParse(m, out rs)) continue;
                }
                mes_pf = rs;
                return rs;
            }
            catch
            {

                return -2000;
            }
         
        }




        public void set_V_I_F(string[] seter)
        {
 
                setvolatage(seter[0]);

                setfreq(seter[2]);

                setcurrent(seter[1]);

          
            
          

        }

        public void set_on_off(string on_off)
        {
            System.Threading.Thread.Sleep(70);
            on_off = on_off.ToLower();
            if (on_off == "on")
            {

                this.WriteLine("PON");

            }
            else
            {

                this.WriteLine("POFF");


            }


        }


        public void OUTPUT()
        {

            System.Threading.Thread.Sleep(50);
            this.WriteLine("PON");


        }
        public void NOOUTPUT()
        {

            System.Threading.Thread.Sleep(50);
            this.WriteLine("POFF");


        }


        ~AC_SOURCE_IVYTECH()
        {
            this.Close();
        }
    }
}

