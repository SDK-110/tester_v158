using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace testapp
{
    class mypyb : SerialPort
    {
        string recebuf;
        public mypyb(string port, int baudrate=115200) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DtrEnable = true;
            //     base.WriteTimeout = 2000;
            base.ReadTimeout = 3000;
            base.NewLine = "\r\n";
            //   base.DataReceived += Relay_aputus_DataReceived;
            
            base.Open();
            base.WriteLine("");
            base.WriteLine("");
            base.WriteLine("import pyb\r\nimport machine");
            base.ReadExisting();

            
        }

        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
        }

        public void set_led(string lednum,int status ) {

            base.ReadExisting();
            if (status == 1)
            {
                base.WriteLine(string.Format("pyb.LED({0}).on()", lednum));
            }
            else {
                base.WriteLine(string.Format("pyb.LED({0}).off()", lednum));
            }
          

        }

        public void set_pinoutput(string pinname, int status)
        {

            base.ReadExisting();
            if (status == 1)
            {
                base.WriteLine(string.Format("pyb.Pin('{0}', pyb.Pin.OUT_PP).on()", pinname));
            }
            else
            {
                base.WriteLine(string.Format("pyb.Pin('{0}', pyb.Pin.OUT_PP).off()", pinname));
            }


        }
        public int getpinIO(string pinname, int pinstatus = 1)
        {

            this.ReadExisting();
            if (pinstatus == 1)
            {
                Regex rex = new Regex(@"value\(\)\r\n(\d)\r\n\>\>", RegexOptions.IgnoreCase);

                base.WriteLine(string.Format("pyb.Pin('{0}', pyb.Pin.IN, pyb.Pin.PULL_UP).value()", pinname));

                MatchCollection matchs = rex.Matches(base.ReadExisting());
                if (matchs.Count > 0)
                {

                    return int.Parse(matchs[0].Groups[1].Value);

                }
                else
                {

                    return -1;  //返回错误
                };
            }
            else
            {


                Regex rex = new Regex(@"value\(\)\r\n(\d)\r\n\>\>", RegexOptions.IgnoreCase);

                base.WriteLine(string.Format("pyb.Pin('{0}', pyb.Pin.IN, pyb.Pin.PULL_DOWN).value()", pinname));

                MatchCollection matchs = rex.Matches(base.ReadExisting());
                if (matchs.Count > 0)
                {

                    return int.Parse(matchs[0].Groups[1].Value);

                }
                else
                {

                    return -1;  //返回错误
                };
            }
        }

        public double getpinADC(string pinname)
        {
            this.ReadExisting();


            Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);

            base.WriteLine(string.Format("pyb.ADC(pyb.Pin('{0}')).read()", pinname));

            MatchCollection matchs = rex.Matches(base.ReadExisting());
            if (matchs.Count > 0)
            {

                return double.Parse(matchs[0].Groups[1].Value) / 4096 * 3.3;

            }
            else
            {

                return -100;  //返回错误
            };
        }

     public void  utility_SPI_write_test(int baudrate=600000,int st=1,int polarity=1,int phase=0,string data="" )
            {
                this.ReadExisting();


                //Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);

           base.WriteLine(string.Format("spi= pyb.SPI({4}, pyb.SPI.MASTER, baudrate={0}, polarity={1}, phase={2})\r\nspi.write('{3}')", baudrate,polarity,phase,data,st));


            this.ReadExisting();
        }


        public string  utility_SPI_read_test(int cnt)
        {
            this.ReadExisting();
            base.WriteLine(string.Format("spi.read({0})", cnt));
            Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(base.ReadExisting());
            base.WriteLine("spi.deinit()");
            this.ReadExisting();

            return "";
        }

        public void utility_IIC_write_test(int freq = 200000, int address = 0x10, string m = "")
        {
            this.ReadExisting();


            //Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);

            base.WriteLine(string.Format("i2c= machine.I2C('{0}', {1})\r\ni2c.writeto(0x{2:x2},'{3}')","X",freq, address,m));


            this.ReadExisting();
        }

        public string utility_I2C_read_test(int address, int read_cnt)
        {
            this.ReadExisting();
            base.WriteLine(string.Format("i2c.readfrom({0},{1})", address,read_cnt));
            Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(base.ReadExisting());
            base.WriteLine("i2c.deinit()");
            this.ReadExisting();

            return "";
        }


        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

 
        public void set_rly(String setrly)
        {
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
   
        




        ~mypyb()
        {
            this.Close();
        }

    }

}

