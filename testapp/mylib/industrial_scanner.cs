using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using testapp.mylib;
// using Windows.UI.Xaml.Controls;

namespace testapp
{
    class industrial_scanner : SerialPort
    {
        StringBuilder _str = new StringBuilder();
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
        public const int WM_CHANGE_TEXT_BOX1 = USER + 125;

        callbackfuc forsendwinmessag;

        public callbackfuc setinterfacefuc
        {

            set { forsendwinmessag = value; }
        }

        #endregion

        public industrial_scanner(string port, int baudrate) : base(port)
        {

            
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
           // base.DtrEnable = true;
            base.ReadTimeout = 2000;
            base.DataReceived += Relay_aputus_DataReceived;
            if (base.IsOpen == false)
            {
                base.Open();
                System.Threading.Thread.Sleep(20);
                base.ReadExisting();
            }
          //  base.PinChanged += Comline_PinChanged;
           
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
            string recebuf = sp.ReadExisting();
            _str.Append(recebuf);
            if (_str.ToString().IndexOf('\n') >= 0) {

                string z = _str.ToString().Trim();
                _str.Clear();
                mylib.utility_func.sendsn_2inputbox(z);
            }
        }



        public string  readbarcode() {
            // this.DiscardInBuffer();
            string rsu = "";
            try
            {
                _str.Clear();
                this.ReadExisting();
                this.Write("T");

            }
            catch {


               
            }


            return rsu;

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

      




        ~industrial_scanner()
        {
            this.Close();
        }
    }
}

