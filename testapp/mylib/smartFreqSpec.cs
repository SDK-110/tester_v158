using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace testapp
{
    class smartFreqSpec : SerialPort
    {

        public smartFreqSpec(string port, int baudrate=115200) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.ReadTimeout = 1000;
            // base.DataReceived += __DataReceived;
            if (base.IsOpen == false)
            {
                base.Open();

            }
           
        }

        private void __DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        private bool getmaxpower(out double power , UInt32 freqcenter ,int sweeptime=120) {

            double Maxpowerlevel = -300;
            try
            {
                int count = 0;
                do
                {
                    if (this.IsOpen == false) this.Open();

                    this.DiscardInBuffer();
                    UInt32 freqstart = freqcenter;
                    UInt32 freqend = freqcenter;
                    UInt32 sweepstep = 0000001;
                    if (count > 3) { throw new Exception("not found value"); }
                    this.Write($"w1{freqstart}#w2{freqend}#w3{sweepstep:D7}#start#");
                    System.Threading.Thread.Sleep(sweeptime);
                    this.Write("stop#");
                    string ret = this.ReadExisting();
                    Regex rex = new Regex("#([+|-][0-9]{2}\\.[0-9])([0-9]{7})\\s+\\$", RegexOptions.IgnoreCase); //获取频率及电平值
                    MatchCollection matchs = rex.Matches(ret);
                    Maxpowerlevel = -300;
                    if (matchs.Count == 0) { count++; continue; }
                    for (int i = 0; i < matchs.Count; i++)
                    {

                        Debug.WriteLine("js--->" + matchs[i].Groups[1].Value);
                        if (double.Parse(matchs[i].Groups[1].Value) > Maxpowerlevel &&
                            int.Parse(matchs[i].Groups[2].Value) == freqcenter)
                        {

                            Maxpowerlevel = double.Parse(matchs[i].Groups[1].Value);
                        }


                    }


                    Debug.WriteLine("pj-->" + Maxpowerlevel);

                    // System.Windows.Forms.MessageBox.Show(matchs.Count + ";" + Maxpowerlevel);

                    this.DiscardInBuffer();
                    this.ReadExisting();

                    break;

                } while (true);
            }
            catch {
                power = -300;
                return false;

            }
            power = Maxpowerlevel;
            return true;
        }


        private UInt32[] getFreqSpec(UInt32 centerfreq=2402000/*KHz*/, UInt32 bandwidth= 1000/*KHz*/,int sample=11)
        {

            UInt32 freqstart = centerfreq - bandwidth/2;
            UInt32 freqend = centerfreq + bandwidth/2;

            int slip = sample;

            UInt32[] freqlist = new UInt32[slip];
            for (int i = 0; i < slip; i++)
            {

                freqlist[i] = centerfreq - (UInt32)(bandwidth / slip * ((slip) / 2 - i));


            }

            return freqlist;
        }

        public bool ScanFreqSpec(out UInt32 maxfreq,
                                 out double level,
                                 out bool isfreqdev,
                                 UInt32 centerfreq = 2402000/*KHz*/, 
                                 UInt32 bandwidth = 2000/*KHz*/,
                                 int sweeptime=120,
                                 int sample=11/*奇数*/,
                                 int asypoint=3)
        {
            Dictionary<UInt32, double> pinpu = new Dictionary<uint, double>();
            UInt32[] fs = getFreqSpec(centerfreq, bandwidth,sample);

            double maxflog=-888 ;
            int splistmaxpoint=fs.Length;
            for (int i =0;i<fs.Length;i++) {
                double maxpowr=-888;
                if (!getmaxpower(out maxpowr, fs[i], sweeptime)){
                    Debug.Write(maxpowr);
                    maxfreq = 444;
                    level = -300;
                    isfreqdev = true;
                    return false;
                }

                if (maxpowr > maxflog) {

                    maxflog = maxpowr;
                    splistmaxpoint = i;

                }
                pinpu.Add(fs[i], maxpowr);





            }

            if (Math.Abs(splistmaxpoint - (fs.Length - 1) / 2) < asypoint)
            {

                maxfreq = fs[splistmaxpoint];
                level = maxflog;
                isfreqdev = false;
                return true;

            }
            else {


                maxfreq = fs[splistmaxpoint];
                level = maxflog;
                isfreqdev = true;
                return true;
            }







        }





        #region 废弃
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
                        file.WriteLine(ret + "--->" + ret);

                    }

                    continue;
                  
                }


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
        #endregion 废弃

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





        ~smartFreqSpec()
        {
            this.Close();
        }
    }
}

