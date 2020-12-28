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


    class comlineforingo_led : SerialPort
    {

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
        /*--------------message loop dll upload-------------*/

        volatile int isokctsreadled = 1; //ctsport LED读取空闲状态
        volatile int changecount = 0;
        volatile int autoreadledvalue = 0;

        public int autoledflog
        {
            set { autoreadledvalue = value; }
            get { return autoreadledvalue; }
}
        volatile int[] ctsreaddata = new int[] { 0, 0, 0, 0 };

        public int setchangecount {

            set { changecount = value; }
        
        }
        public comlineforingo_led(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.ReadTimeout = 15000;
            // base.DataReceived += Relay_aputus_DataReceived;
            if (base.IsOpen == false)
            {
                base.Open();

            }

            base.PinChanged += Comlineforingo_PinChanged;

            //setmainhwnd = FindWindow(null, mainwindtext);
        }
        /*
         ********從串口的cdc端子上讀取變換的次數 *******
         * 可用用於燈閃爍次數判定
             */
     public   int readchangedcount_fromcdcport( int delay ) {
           

            System.Threading.Thread.Sleep(delay);

            return changecount /2 ;
        }
     int[] ctsreadled() {
            int count = 0;
            isokctsreadled = 0;
            while (isokctsreadled == 0) {

                System.Threading.Thread.Sleep(100);
                if (count > 20) {

                    ctsreaddata[0] = -1;
                    ctsreaddata[1] = -1;
                    ctsreaddata[2] = -1;
                    ctsreaddata[3] = -1;
                    break;

                }

                count++;
            } ;
            return ctsreaddata;

        }

     public   int[] readled_common (int port = 1, int trytimes = 2){

            int[] ctsreaddata = new int[] { 0, 0, 0, 0 };
            int count = 0;
            do
            {
               this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture");
                    System.Threading.Thread.Sleep(450);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                    System.Threading.Thread.Sleep(50);
                }

                string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{3})\s([0-9]{3})\s([0-9]{3})\s([0-9]{5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if (ctsreaddata[0] > 0 || ctsreaddata[1] > 0 || ctsreaddata[2] > 0 || ctsreaddata[3] > 0 || count > 3) break;
                count++;
            } while (true);


            return ctsreaddata;



        }

        public bool getpinstatus(int pin /*1為DCD PIN1 ,2為DSR PIN6 ,3為 CTS PIN 7  */)
        {
            if (pin == 1)
            {
                return this.CDHolding;
            }
            else if (pin == 2)
            {

                return this.DsrHolding;

            }
            else if (pin == 3)
            {
                return this.CtsHolding;
            }
            else {


                return this.CDHolding;
            }
          


        }

        public  void setpinstatus(int pin /*1為DTR PIN4,2為RTS PIN 7*/,bool setval)
        {
            if (pin == 1) this.DtrEnable = setval;
            if (pin == 2) this.RtsEnable = setval;
           



        }
        private void Comlineforingo_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            
            if (e.EventType == SerialPinChange.Break) { 
            

            
            }
            if (e.EventType == SerialPinChange.CDChanged) {



                changecount++;


            }

            if (e.EventType == SerialPinChange.CtsChanged) {

                if (autoledflog == 0) return;
                SerialPort st = (SerialPort)sender;
                isokctsreadled = 0;
                
                for (int m = 0; m < 4; m++) {

                    ctsreaddata[m] = 0;
                
                }
                int count = 0;
                do
                {
                    st.ReadExisting();

                    for (int i = 0; i < 2; i++)
                    {

                        st.WriteLine("capture");
                        System.Threading.Thread.Sleep(500);

                        st.WriteLine("getrgbi01");
                        System.Threading.Thread.Sleep(50);
                    }

                    string ret = st.ReadExisting();
                    Regex rex = new Regex(@"([0-9]{3})\s([0-9]{3})\s([0-9]{3})\s([0-9]{5})", RegexOptions.IgnoreCase);
                    MatchCollection matchs = rex.Matches(ret);
                    for (int t = 0; t < matchs.Count; t++)
                    {
                        if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                        if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                        if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                        if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                    }
                    if (ctsreaddata[0] > 0 || ctsreaddata[1] > 0 || ctsreaddata[2] > 0 || ctsreaddata[3] > 0 || count > 3 ) break;
                    count++;
                } while (true);
                isokctsreadled = 1;
            }

            if (e.EventType == SerialPinChange.DsrChanged) { 
            


            }


            if (e.EventType == SerialPinChange.Ring) {
                if (this.forsendwinmessag != null) { 

                this.forsendwinmessag();

                }
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
                    System.Threading.Thread.Sleep(1200);
                    // ret = this.ReadLine();
                    ret =  this.ReadExisting();
                    if (ret != null)
                    {

                        Regex rex = new Regex(@"total\:\d+\s+loud\:\d+\snoise\:\d+\srms\:\d+", RegexOptions.IgnoreCase);
                        MatchCollection matchs = rex.Matches(ret);
                        for (int t = 0; t < matchs.Count; t++)
                        {
                            File.AppendAllText("debugdutcomm.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\r--->" + matchs[i].Groups[0].Value);
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





        ~comlineforingo_led()
        {
            this.Close();
        }
    }
}

