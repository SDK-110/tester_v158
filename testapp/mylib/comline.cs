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
    class comline : SerialPort
    {
        public event debuginfosend debugsendstr;

        public debuginfosend setdebuginfosend
        {

            set{ debugsendstr = value; }
}

        void debugstrsend(string m) {

            if (debugsendstr != null) debugsendstr(m);

        }

        #region /*--------------message loop dll upload-------------*/

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(
            IntPtr hWnd, // handle to destination window 
            uint Msg, // message 
            uint wParam, // first message parameter 
            uint lParam // second message parameter 
            );

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public const int USER = 0x0400;
        public const int WM_SENDA = USER + 101;
        public const int WM_SENDB = USER + 102;
        public const int WM_SENDC = USER + 103;
        public const int WM_SENDD = USER + 104;
        public const int WM_SENDE = USER + 105;
        public const int WM_SHOWNUM = USER + 106;
        public const int WM_FASTID = USER + 107;
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;

        callbackfuc forsendwinmessag;

        public callbackfuc setinterfacefuc
        {

            set { forsendwinmessag = value; }
        }

        #endregion

        public comline(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
           // base.DtrEnable = true;
            base.ReadTimeout = 15000;
            // base.DataReceived += Relay_aputus_DataReceived;
            if (base.IsOpen == false)
            {
                base.Open();

            }
            base.PinChanged += Comline_PinChanged;
           
        }

        private void Comline_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            return;
            if(forsendwinmessag != null) { 
            forsendwinmessag();
            }
        }

        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }



        public int readstringuntil(string findstr, int sleep_sec_after_read = 5000) {
           // this.DiscardInBuffer();
            for (int i = 0; i < (int)(sleep_sec_after_read/1000); i++)
            {

                System.Threading.Thread.Sleep(1000);
                string m = this.ReadExisting();
              //  System.Windows.Forms.MessageBox.Show(m);
                int t = m.IndexOf(findstr);
                if (t >= 0) {


                    return 1;

                }

            }
          



            return -1;
        }

        public int readstringforone(string findstr)
        {
        

                System.Threading.Thread.Sleep(1000);
                string m = this.ReadExisting();

                int t = m.IndexOf(findstr);
                if (t >= 0)
                {


                    return 1;

                }

            




            return -1;
        }



        public string command_pass_fail(string command,string contain,int sleep_ms_after_read = 1000) {
           
            string ret;
            this.DiscardInBuffer();
            this.WriteLine(command);
            try {
                System.Threading.Thread.Sleep(sleep_ms_after_read);
                ret = this.ReadLine();
                ret = ret + this.ReadExisting();
            }
            catch {

                ret = "timeout";
            }
            if (ret.Contains(contain))
            {


                return "pass";
            }
            else {

                using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt", true))
                {
                    file.WriteLine(command + "--->" +  ret);

                }

                return "fail";
            }
        }


        public string read_value_fromreg(string findstr = "", int sleep_ms_after_read = 3000)
        {
            string ret="";
          

            for (int i = 0; i < (int)sleep_ms_after_read / 1000; i++)
            {

                this.DiscardInBuffer();
                
                try
                {
                    this.ReadExisting();
                    System.Threading.Thread.Sleep(1200);
                    // ret = this.ReadLine();
                    ret =  this.ReadExisting();
                    if (ret != null)
                    {

                        Regex rex = new Regex(@"total\:\d+\s+loud\:\d+\snoise\:\d+\srms\:\d+", RegexOptions.IgnoreCase);
                        MatchCollection matchs = rex.Matches(ret);
                        for (int t = 0; t < matchs.Count; t++)
                        {
                          //  File.AppendAllText("debugdutcomm.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\r--->" + matchs[i].Groups[0].Value + "\r\n");
                        }
                    }
                }
                catch
                {
                    ret = "null";
                }

                MatchCollection reg = new Regex(findstr).Matches(ret);

                if (reg.Count > 0)
                {


                    // String m = "fsdafsdfsd123MIC:1700.000";
                    //  String b = "MIC\\:1[1-9]\\d+\\.{0,1}\\d+";

                    ret = reg[0].Value;

                    //  MatchCollection reg2 = new Regex(@"\d+\.{0,1}\d+").Matches(m);

                    // return float.Parse(reg2[0].Value);
                    return ret;


                }
                else
                {

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt", true))
                    {
                        file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\r\n--->" + ret + "\r\n");

                    }

                    }

                    continue;
                  
                }




            return "null";
        }

        public string read_value_fromregNonclearbuffer(out string retstr,string findstr = "", int sleep_ms_after_read = 3000 )
        {
            string ret = "";
      

            for (int i = 0; i < (int)sleep_ms_after_read / 1000; i++)
            {

                try
                {
                    System.Threading.Thread.Sleep(1200);
                    // ret = this.ReadLine();
                    ret = this.ReadExisting();
                }
                catch
                {
                    ret = "null";
                }

                MatchCollection reg = new Regex(findstr).Matches(ret);

                if (reg.Count > 0)
                {


                    // String m = "fsdafsdfsd123MIC:1700.000";
                    //  String b = "MIC\\:1[1-9]\\d+\\.{0,1}\\d+";

                    ret = reg[0].Value;

                    //  MatchCollection reg2 = new Regex(@"\d+\.{0,1}\d+").Matches(m);

                    // return float.Parse(reg2[0].Value);

                    MatchCollection reg2 = new Regex(@"[0-9a-fA-F]{2}([/\s:-][0-9a-fA-F]{2}){5}").Matches(ret);


                    ret = reg2[0].Value;

                   

                    retstr = ret;
                    return "pass";

                }
                else
                {

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt", true))
                    {
                        file.WriteLine(ret + "--->" + ret);

                    }

                 
                    continue;

                }


            }

            retstr = ret;
            return "null";
        }

        public float[]  read_value_2float(string command, string findstr, int sleep_ms) {
                string ret;
                this.DiscardInBuffer();
                this.WriteLine(command);
                try
                {
                    ret = this.ReadLine();
                    ret = ret + this.ReadExisting();
                }
                catch
                {
                    ret = "timeout";
                }

                MatchCollection reg = new Regex(@"" + findstr).Matches(ret);

                if (reg.Count == 2)
                {

                    return new float[] { float.Parse(reg[0].Value), float.Parse(reg[1].Value) };

                }
                else {

                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt", true))
                    {
                        file.WriteLine(command + "--->" + ret);

                    }


                    return new float[] { 0, 0 };
                }

            }

        #region ********************带编码的字符串16进制字符串转换****************************
        private string StringToHexString(string s, Encoding encode)
        {
            byte[] b = encode.GetBytes(s);//按照指定编码将string编程字节数组
            string result = string.Empty;
            for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
            {
                result += "%" + Convert.ToString(b[i], 16);
            }
            return result;
        }

        private string HexStringToString(string hs, Encoding encode)
        {
            //以%分割字符串，并去掉空字符
            string[] chars = hs.Split(new char[] { '%' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] b = new byte[chars.Length];
            //逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length; i++)
            {
                b[i] = Convert.ToByte(chars[i], 16);
            }
            //按照指定编码将字节数组变为字符串
            return encode.GetString(b);
        }

        #endregion  ********************END 带编码的字符串16进制字符串转换****************************

        private  byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        private  string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }





        ~comline()
        {
            this.Close();
        }
    }
}

