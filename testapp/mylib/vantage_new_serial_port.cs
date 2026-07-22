using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using Vila.Extensions;
using static FTD2XX_NET.FTDI;

namespace testapp
{
    class vantage_new_serial_port : SerialPort
    {
        string recebuf;
     
       // volatile  byte[] rsubyt = new byte[200];
        string golb_pp = "";
        //  volatile int rev_count = 0;
        volatile int rev_flog = 0;
    public vantage_new_serial_port(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
           
            
            
        }

        
        public int test_lcd_disp(out string rsu,string status)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                status =status.ToLower();
                byte[] sendbuf = null;
                switch (status) {
   
                    case "color":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray("57 00 01 01 00 00 00 45");
                        }
                        break;
                    case "white":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray("57 00 01 00 00 00 00 45");
                        }
                        break;
                    case "bl_on":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray("57 00 02 01 00 00 00 45");  
                        }
                        break;
                    case "bl_off":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray("57 00 02 00 00 00 00 45");
                        }
                        break;

                }
             
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public (int,uint,uint) test_lcd_tp(out string rsu)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 03 00 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1) {
                    var p = (1, (uint.Parse(golb_pp.Substring(6, 2), System.Globalization.NumberStyles.HexNumber) + uint.Parse(golb_pp.Substring(8, 2), System.Globalization.NumberStyles.HexNumber) * 256)
                        , (uint.Parse(golb_pp.Substring(10, 2), System.Globalization.NumberStyles.HexNumber) + uint.Parse(golb_pp.Substring(12, 2), System.Globalization.NumberStyles.HexNumber) * 256));
                    mylib.utility_func.callbackdebuginfo(p.ToString());
                    return p;
                };

                return (-2,0,0);
            }
            catch
            {


                rsu = "command error";
                return (-1,0,0);
            }
        }
        public int test_qspi(out string rsu)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 04 39 2f f0 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1)
                {
                  return 1;
                };

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int test_spi_loop(out string rsu)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 06 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1)
                {
                    return 1;
                };

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int test_led_test(out string rsu, string ch, string status)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"57 {ch.PadLeft(2,'0')} 07 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1)
                {
                    return 1;
                };

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }


        public int test_j6_PA0_PB11_PD12_io(out string rsu)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf;
               
               sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 05 07 00 00 00 45");

              
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1)
                {
                    if (rsu.Length==16)
                    {

                       
                        return 1;
                    }
                    else {

                      
                        return -3;


                    };

                  
                };

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int test_usb_host(out string rsu)
        {
            try
            {
                rev_flog = 0;
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 08 00 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_flog == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp;
                if (rev_flog == 1)
                {
                    return 1;
                };

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(200);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            string pp = BitConverter.ToString(tmp).Replace("-", " ");
          //  System.Windows.Forms.MessageBox.Show("Test");
           mylib.utility_func.callbackdebuginfo("rev data:" + pp);
            if ( pp.ToUpper().StartsWith("53")==true && pp.ToUpper().EndsWith("45") == true) {

                golb_pp = pp.Replace(" ","");
                rev_flog = 1;
            }
            //Array.Copy(tmp, rsubyt, m);
            //rev_count = m;
        }
      


  

        private  byte[] send_data_add_checksum(byte address_id, byte cmd, byte[] data)
        {

            byte[] rsu = new byte[data.Length + 7];
            byte len = (byte)(data.Length + 3);
            rsu[0] = 0x44;
            rsu[1] = 0x4e;
            rsu[2] = address_id;
            rsu[3] = len;
            rsu[4] = cmd;
            rsu[rsu.Length - 1] = 0x55;

            Array.Copy(data,0, rsu,5, data.Length);

            byte temp = rsu[3];

            for (int count =4; count < rsu.Length - 2; count++)
            {

                temp = (byte)(temp ^ rsu[count]);
            }

            rsu[rsu.Length - 2] = temp;
            rsu[rsu.Length - 1] = 0x55;
            return rsu;
        }

        ~vantage_new_serial_port() { 
            this.Close();
           
        }

    }

}

