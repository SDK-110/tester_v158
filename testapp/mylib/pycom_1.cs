using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using testapp.mylib;

namespace testapp
{
    class pycom_1 : SerialPort
    {
        
        StringBuilder strbuf = new StringBuilder();
        string recebuf;
        int displayflog = 0;
        public pycom_1(string port, int baudrate=115200) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DtrEnable = true;
            base.WriteTimeout = 5000;
            base.ReadTimeout = 5000;
            base.NewLine = "\r\n";
            base.DataReceived += Relay_aputus_DataReceived;
           // base.WriteBufferSize = 5000;
          //  base.Open();
          //  base.WriteLine("");
          //  base.WriteLine("");
          //  base.WriteLine("import machine");
          //  base.WriteLine("import pycom");
          ////  base.WriteLine("from network import WLAN");
          //  base.WriteLine("from machine import Pin");
          //  base.ReadExisting();

            
        }

        public void clear_buf() { 

            strbuf.Clear();
}
        public string  get_buf()
        {

            return strbuf.ToString();
        }
        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
            SerialPort sp = (SerialPort)sender;
            if (sp.BytesToRead == 1) return;
            try
            {
               // System.Threading.Thread.Sleep(100);
                string m = sp.ReadExisting();
                strbuf.Append(m);
                if (displayflog == 1) {

                    mylib.utility_func.callbackdebuginfo(m.Trim());

                }
            }
            catch {
                return;
            }

        }

        public int set_Rts_dts(int rts, int dts) {
            strbuf.Clear();
            if (!base.IsOpen) base.Open();
            try
            {
                if (rts == 1)
                {
                    base.RtsEnable = true;
                }
                else
                {
                    base.RtsEnable = false;
                }
                if (dts == 1)
                {
                    base.DtrEnable = true;
                }
                else
                {
                    base.DtrEnable = false;
                }

                return 1;
            }
            catch {

                return -1;
            }


        }

        public void set_led(string lednum,int status ) {
            if (!base.IsOpen) base.Open();
            strbuf.Clear();
            base.ReadExisting();
            if (status == 1)
            {
                base.WriteLine(string.Format("pyb.LED({0}).on()", lednum));
            }
            else {
                base.WriteLine(string.Format("pyb.LED({0}).off()", lednum));
            }
          

        }

        public void read_imei()
        {
            base.DataReceived -= Relay_aputus_DataReceived;
            if (!base.IsOpen) base.Open();
            strbuf.Clear();
            base.ReadExisting();
          
            base.WriteLine("\r\nimport LTE\r\nlte=LTE.LTE()\r\nret = lte.send_at_cmd(\"ATI2\")\r\nprint(ret)\r\nprint(1234)\r\n");

            System.Threading.Thread.Sleep(3000);

            string m = base.ReadExisting();
            System.Windows.Forms.MessageBox.Show(m);

        }

        public void set_teshu_comm() {
            if (!base.IsOpen) base.Open();
            base.Write(new byte[] { 0x03 }, 0, 1); //取消执行

        }
        public string pycom_script_run(string script,int delay,string reg="") {
            if (!base.IsOpen) base.Open();
            string scriptstr = "";
            strbuf.Clear();
            try
            {
                scriptstr = File.ReadAllText(script);

            }
            catch (Exception e) {

                utility_func.callbackdebuginfo(e.ToString());

            }
            base.Write(new byte[] { 0x05 }, 0, 1);
           
            System.Threading.Thread.Sleep(100);
//传输速度过快导致错误，所以一节一节慢慢发送
            byte[] pstr = System.Text.ASCIIEncoding.ASCII.GetBytes(scriptstr);
            int t = 0;
            for (int i = 0; i < pstr.Length / 100; i++)
            {
                base.Write(pstr, i * 100, 100);
                System.Threading.Thread.Sleep(30);
                t++;
            }
            if (t * 100 < pstr.Length)
            {
                base.Write(pstr, t * 100, pstr.Length - (t * 100));
            }

          //  base.WriteLine(scriptstr);
            System.Threading.Thread.Sleep(100);
            base.Write(new byte[] { 0x04 }, 0, 1);
            System.Threading.Thread.Sleep(100);
            
            int count = delay;
            string tmp = "";
            do
            {
                System.Threading.Thread.Sleep(100);
                tmp = strbuf.ToString();
            } while (mylib.utility_func.findstr_regex($"({reg})", tmp) == "null" && count-- > 0);
            mylib.utility_func.callbackdebuginfo(strbuf.ToString());

            return mylib.utility_func.findstr_regex($"({reg})", tmp); ;
        }

        public int set_pinoutput(string pinname, int status)
        {
            if (!base.IsOpen) base.Open();
            strbuf.Clear();
            try
            {
                base.ReadExisting();
                if (status == 1)
                {
                    base.WriteLine(string.Format("Pin('{0}', Pin.OUT_PP).on()", pinname));
                }
                else
                {
                    base.WriteLine(string.Format("Pin('{0}', Pin.OUT_PP).off()", pinname));
                }
                return 1;
            }
            catch {

                return -1;

            }


        }
        public int getpinIO(string pinname, int pinstatus = 1)
        {
            if (!base.IsOpen) base.Open();
            strbuf.Clear();
            this.ReadExisting();
            if (pinstatus == 1)
            {
               
                Regex rex = new Regex(@"value\(\)\r\n(\d)\r\n\>\>", RegexOptions.IgnoreCase);

                base.WriteLine(string.Format("Pin('{0}', Pin.IN, Pin.PULL_UP).value()", pinname));

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

                base.WriteLine(string.Format("Pin('{0}', Pin.IN, Pin.PULL_DOWN).value()", pinname));
                System.Threading.Thread.Sleep(100);
                string rtstr = base.ReadExisting();
                MatchCollection matchs = rex.Matches(
                    rtstr
                    );
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
            if (!base.IsOpen) base.Open();
            this.ReadExisting();
            strbuf.Clear();

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
            if (!base.IsOpen) base.Open();
            this.ReadExisting();
            strbuf.Clear();

            //Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);

            base.WriteLine(string.Format("spi= pyb.SPI({4}, pyb.SPI.MASTER, baudrate={0}, polarity={1}, phase={2})\r\nspi.write('{3}')", baudrate,polarity,phase,data,st));


            this.ReadExisting();
        }


        public string  utility_SPI_read_test(int cnt)
        {
            if (!base.IsOpen) base.Open();
            this.ReadExisting();
            strbuf.Clear();
            base.WriteLine(string.Format("spi.read({0})", cnt));
            Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(base.ReadExisting());
            base.WriteLine("spi.deinit()");
            this.ReadExisting();

            return "";
        }

        public void utility_IIC_write_test(int freq = 200000, int address = 0x10, string m = "")
        {
            if (!base.IsOpen) base.Open();
            this.ReadExisting();
            strbuf.Clear();

            //Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);

            base.WriteLine(string.Format("i2c= machine.I2C('{0}', {1})\r\ni2c.writeto(0x{2:x2},'{3}')","X",freq, address,m));


            this.ReadExisting();
        }

        public string utility_I2C_read_test(int address, int read_cnt)
        {
            if (!base.IsOpen) base.Open();
            this.ReadExisting();
            strbuf.Clear();
            base.WriteLine(string.Format("i2c.readfrom({0},{1})", address,read_cnt));
            Regex rex = new Regex(@"read\(\)\r\n(\d{0,4})\r\n\>\>", RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(base.ReadExisting());
            base.WriteLine("i2c.deinit()");
            this.ReadExisting();

            return "";
        }


        public void send(byte[] m)
        {
            if (!base.IsOpen) base.Open();
            strbuf.Clear();
            this.Write(m, 0, m.Length);
        }

 
       
   
        




        ~pycom_1()
        {
            this.Close();
        }

    }

}

