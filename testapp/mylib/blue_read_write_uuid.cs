using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace testapp
{


    class serial_blue_dongle_tools : SerialPort
    {

        #region /*sendmessage dll 庫加載*/
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        string c = "";
        /*跨线程消息*/
        static int USER = 0x0400;
        int WM_SENDA = USER + 101;
        int WM_SENDB = USER + 102;
        int WM_SENDC = USER + 103;
        int WM_SENDD = USER + 104;
        int WM_SEND_SET_CC1310LOSS = USER + 110;
        int WM_SEND_SET_BTLOSS = USER + 111;
        int WM_SEND_SET_WIFILOSS = USER + 112;
        int WM_SEND_AUTOTEST = USER + 113;
        #endregion
        public static IntPtr ptrWnd;
        const string OKFLOG = "OK";
        volatile int loopwaitflog = 0;
        StringBuilder str_rev = new StringBuilder();
        volatile int wait_timeout = -1;
        volatile int loopcount = 0;
        class _device {
            public string no;
            public string mac;
            public string rssi;
            public string name;

        }
        class device_char {
            public string no;
            public string suuid;
            public string tezheng;
        }
        List<_device> devices = new List<_device>();
        List<device_char> chars = new List<device_char>();
        private void setinit() {
            // this.ReadExisting();
            wait_timeout = -1;
            loopwaitflog = 0;
            if (str_rev.ToString().Length > 500) str_rev.Clear();
            if (loopcount > 10) loopcount = 0;

        }

        void waiting_lp(int count) {

            int ct = count;
            do
            {
                System.Threading.Thread.Sleep(100);
                if (ct == 0) {
                    wait_timeout = 1;

                }
            } while (loopwaitflog == 0 && ct-- >= 0);

        }
        public serial_blue_dongle_tools(string port, int baudrate = 9600) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += comm_DataReceived;
            str_rev.Capacity = 4056;

            base.WriteTimeout = 2000;
            base.ReadTimeout = 5000;
            base.Open();
        }




        public int read_name_rssi_mac(string reg_str = @"(\d+):\s0x([0-9|A-F]{12}),\s(-\d{2,3}),\s(\w+)"
                                        , int delay = 1000)
        {

            devices.Clear();
            // if (d == null) d = "-45;3000;\\d\\:\\s+0x([0-9|a-z|A-Z]{8})[0-9|a-z|A-Z]{4}\\,\\s+([-]\\d{2,3})\\,\\s+NoiseAwareG3_([0-9|a-z|A-Z]{4})";

           

            this.DiscardInBuffer();
            this.ReadExisting();
            this.Write("AT+SCAN?");
            System.Threading.Thread.Sleep(delay);
            string ret = this.ReadExisting();
            mylib.utility_func.callbackdebuginfo(ret);
            Regex rex = new Regex(reg_str, RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(ret);

            for (int i = 0; i < matchs.Count; i++)
            {

                devices.Add(new _device() { no = matchs[i].Groups[1].Value,
                    mac = matchs[i].Groups[2].Value,
                    rssi = matchs[i].Groups[3].Value,
                    name = matchs[i].Groups[4].Value


                });

            }


            return matchs.Count;

        }

        public int? get_rssi(string mac_or_name)
        {

            //int t = read_name_rssi_mac();

            //if (t == 0) return null;
            foreach (var m in devices) {

                if (m.name == mac_or_name || m.mac == mac_or_name.ToUpper()) return int.Parse(m.rssi);



            }

            return null;
        }

        public string  get_mac(string mac_or_name)
        {
            int t = read_name_rssi_mac();

            if (t == 0) return "null";
            foreach (var m in devices)
            {

                if (m.name == mac_or_name || m.mac == mac_or_name.ToUpper()) return m.mac;



            }

            return "null";
        }

        public int connect_device(string mac_name, int delay = 1500) {
            chars.Clear();
          //  var pd = (devices.Where((o) => (o.name == mac_name || o.mac == mac_name)));
          //  if (pd.Count() == 1) {
                try
                {
                //  this.Write($"AT+CON{pd.ToArray()[0].mac}");
                this.Write($"AT+CON{mac_name}");
                System.Threading.Thread.Sleep(delay);
                    string p = this.ReadExisting();
                    Regex rex = new Regex(@"(\d+):\s([0-9|A-F]{4}),\s(\w+)", RegexOptions.IgnoreCase);
                    MatchCollection matchs = rex.Matches(p);

                    for (int i = 0; i < matchs.Count; i++)
                    {

                        chars.Add(new device_char()
                        {
                            no = matchs[i].Groups[1].Value,
                            suuid = matchs[i].Groups[2].Value,
                            tezheng = matchs[i].Groups[3].Value

                        });

                    }

                    mylib.utility_func.callbackdebuginfo(p);

                    return 1;
                }
                catch {

                    return -1;
                }


         //   };


            return -1;
        }

        public int deconnect_device(int delay = 500)
        {


            try
            {
                this.Write($"AT+DISCON");
                System.Threading.Thread.Sleep(delay);
                string p = this.ReadExisting();
                return 1;
            }
            catch
            {

                return -1;
            }





            return -1;
        }

        public (string,byte?[]) get_char_value(string suuid,int delay=1000, int isstr=1) {
            suuid = "2A00";
           string po=  this.ReadExisting();
           this.DiscardInBuffer();
            this.Write("AT+RDCH" + suuid);
            System.Threading.Thread.Sleep(delay);

            //string m = this.ReadExisting();
            int t = this.BytesToRead;
            if (t <= 8) return ("-1",null);
            byte[] tmpbuf = new byte[t];
            this.Read(tmpbuf, 0, t);
            mylib.utility_func.callbackdebuginfo(System.Text.ASCIIEncoding.ASCII.GetString(tmpbuf));
            byte[] p = tmpbuf.Skip(8).ToArray();
            if (isstr == 1) {

               

                string rt = System.Text.ASCIIEncoding.ASCII.GetString(p).Trim();
                return (rt, null);
            }
            else {

                return ("1", null);
            }


            return ("-3", null);
        }

        public int set_chtxdevice(string suuid,int delay =500)
        {


            try
            {
                this.ReadExisting();
                this.Write($"AT+CHTX" + suuid);
                System.Threading.Thread.Sleep(delay);
                string p = this.ReadExisting();
                if (p.IndexOf("OK") >= 0) return 1;
                else {
                    return -3;
                }
            }
            catch
            {

                return -2;
            }





            return -1;
        }

        public int set_chrxdevice(string suuid, int delay = 500)
        {


            try
            {
                this.ReadExisting();
                this.Write($"AT+CHRX" + suuid);
                System.Threading.Thread.Sleep(delay);
                int t = this.BytesToRead;
                if (t <= 11) return -1;
                byte[] tmpbuf = new byte[t];
                this.Read(tmpbuf, 0, t);

                string ReadExisting = System.Text.ASCIIEncoding.ASCII.GetString(tmpbuf);
                mylib.utility_func.callbackdebuginfo(ReadExisting);
                byte[] p = tmpbuf.Skip(11).ToArray();
                if (ReadExisting.IndexOf("OK") >= 0) return 1;
                else
                {
                    return -3;
                }
            }
            catch
            {

                return -2;
            }





            return -1;
        }



        #region 知识储备库忽略
        /*
                public void set_sing_relay(byte relay_num, byte openorclose) {

                    Byte[] a = new Byte[] { 0x05, 0x01, 0X00,(byte)(relay_num-1),(byte)(openorclose),0x00 };

                    byte[] commend = tan_modbus(a);

                    int count = 0;
                    Byte[] m = new byte[commend.Length];

                    do
                    {
                        try
                        {
                            this.Write(commend, 0, commend.Length);
                            this.Read(m, 0, commend.Length);

                        }
                        catch (Exception)
                        {

                            count++;
                            if (count > 3) {

                                System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                                return;
                            }

                        }
                    } while (count != 0);


                }
                public void set_relay(byte relay_1_8, byte relay_9_16)
                {

                    Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

                    byte[] commend = tan_modbus(a);

                    int count = 0;
                    Byte[] m = new byte[commend.Length];

                    do
                    {
                        try
                        {
                            this.Write(commend, 0, commend.Length);
                            this.Read(m, 0, commend.Length);

                        }
                        catch (Exception)
                        {
                            count++;
                            if (count > 3)
                            {

                                System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                                return;
                            }

                        }
                    } while (count != 0 && count < 3);


                }
                // crc校验函数
                private   UInt16 crc16(Byte[] ptr)
                { return ModbusCrc16.Compute(ptr); }

                private  Byte[] tan_modbus(Byte[] data)
                { return ModbusCrc16.AppendCrc(data); }
        */
        #endregion
        ~serial_blue_dongle_tools()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
                setinit();
                var rever = ((SerialPort)sender);
               
                System.Threading.Thread.Sleep(200);
                int m = rever.BytesToRead ;
                string tempstr = rever.ReadExisting();
                callbackdebuginfo(tempstr + "\n\r");
                str_rev.Append(tempstr);
                if (tempstr.IndexOf(OKFLOG) >= 0 || tempstr.IndexOf("FAIL") >= 0 || tempstr.Length>=2) { loopwaitflog = 1; }
             
 
               

            }
            catch( Exception a)
            {
               

            }


        }

        public void callbackdebuginfo(string m)
        {
            m = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ": \r\n" + m;
            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }

    }

}

