using NationalInstruments.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using PCHMI;
using RohdeSchwarz.RsCmwBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using testapp.mylib;
using Vila.Extensions;
using Vila.Win32;
// using Windows.UI.Xaml.Controls;

namespace testapp
{
    class comline : SerialPort
    {

        string sgw_dn1_ble_mac = "";
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
                System.Threading.Thread.Sleep(20);
                base.ReadExisting();
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


        public (int flog,string mac, double rssi) read_mac_rss_dn(double low) {
            base.ReadTimeout = 2000;
            string tmp = "";
            string rsu = "";
            int  count = 10;
            int found_flog = 0;
            string mac = "";
            string rssi = "";

            string pattern = @"0x([0-9A-Fa-f]+).*?(-?\d+)(?=,\sM\d{3,})";
            this.sgw_dn1_ble_mac = "";
                this.NewLine = "\r\n";
                this.ReadExisting();
                this.Write("AT+SCAN?");
                do
            {
                try
                {
                   
                    tmp = this.ReadLine();
                }
                catch (Exception ex)
                {


                }
                    mylib.utility_func.callbackdebuginfo(tmp);
                    if(count--==0)break;
                if (Regex.Match(tmp, @"M\d{3,}").Success)
                {


                   

                    // 创建 Regex 对象
                    Regex regex = new Regex(pattern);

                   
                    // 匹配输入字符串
                    System.Text.RegularExpressions.Match match = regex.Match(tmp);

                    if (match.Success)
                    {
                        // 获取捕获组
                        mac = match.Groups[1].Value; // 十六进制数
                        rssi = match.Groups[2].Value; // 数字

                        // 输出结果
                        mylib.utility_func.callbackdebuginfo("MAC: " + mac);
                        mylib.utility_func.callbackdebuginfo("RSSI: " + rssi);
                        
                        if (double.Parse(rssi) >= low)
                        {
                            rsu = tmp; found_flog = 1;
                            break;

                        }
                     
                    }
                    else
                    {
                        mylib.utility_func.callbackdebuginfo("No match found.");
                    }


                }
  

                } while (tmp.IndexOf("Devices Found")<0 );

            if (found_flog == 1)
            {
              //  string pattern = @"0x([0-9A-Fa-f]+).*?(-?\d+)(?=,\sM\d{3,})";

                // 创建 Regex 对象
                //Regex regex = new Regex(pattern);

                //System.Text.RegularExpressions.Match match = regex.Match(rsu);

                //if (match.Success)
                //{
                //    // 获取捕获组
                //    mac= match.Groups[1].Value; // 十六进制数
                //   rssi = match.Groups[2].Value; // 数字

                //    // 输出结果
                //   mylib.utility_func.callbackdebuginfo("MAC: " + mac);
                //    mylib.utility_func.callbackdebuginfo("RSSI: " + rssi);
                //}
                //else
                //{
                //    mylib.utility_func.callbackdebuginfo("No match found.");
                //}


                sgw_dn1_ble_mac = mac.Replace("0x","").Replace("0X", "");

                if (1 == sgw_dn_con_set_chr("ABF3")){
                    System.Threading.Thread.Sleep(1000);
                    //sgw_dn_ble_char_cmd("Ee 01 00 00");
                    // sgw_dn_ble_char_cmd_ret("read_imei");

                    //sgw_dn_ble_char_cmd_ret("read_bt_mac");

                    //sgw_dn_ble_char_cmd_ret("read_wifi_mac");

                   // string p = sgw_dn_ble_char_cmd_ret("read_sn");
                }
            

                return (1,mac, double.Parse(rssi));
            }
            else {

                return (0,"fdsaf", -12);

            }

            
        
        
        }


        public (int flog, string rsu) read_ime()
        {
 

             string rsu = sgw_dn_ble_char_cmd_ret("read_imei");
            mylib.utility_func.callbackdebuginfo(rsu);

            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }

        public (int flog, string rsu) read_wifi_mac()
        {


            string rsu = sgw_dn_ble_char_cmd_ret("read_wifi_mac");
            mylib.utility_func.callbackdebuginfo(rsu);


            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }
        public (int flog, string rsu) read_bt_mac()
        {


            string rsu = sgw_dn_ble_char_cmd_ret("read_bt_mac");
            mylib.utility_func.callbackdebuginfo(rsu);


            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }
        public (int flog, string rsu) read_sn()
        {


            string rsu = sgw_dn_ble_char_cmd_ret("read_sn");
            mylib.utility_func.callbackdebuginfo(rsu);


            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }

        public (int flog, string rsu) reset_lte_flog ()
        {


            string rsu = sgw_dn_ble_char_cmd_ret("reset_flog");
            mylib.utility_func.callbackdebuginfo(rsu);


            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }

        public (int flog, string rsu) set_lte_flog()
        {


            string rsu = sgw_dn_ble_char_cmd_ret("set_flog");
            mylib.utility_func.callbackdebuginfo(rsu);


            if (rsu.IndexOf("error") >= 0)
            {
                return (-1, rsu);
            }
            else
            {

                return (1, rsu);

            }




        }
        public int sgw_dn_con_set_chr(string uuid) {

            base.ReadTimeout = 2000;
            string tmp = "";
            string rsu = "";
            int count = 20;
            int found_flog = 0;

            this.NewLine = "\r\n";
            this.ReadExisting();
            if(sgw_dn1_ble_mac.Length==0) return -1;
            this.Write("AT+CON"+ sgw_dn1_ble_mac);
            do
            {
                try
                {

                    tmp = this.ReadLine();
                }
                catch (Exception ex)
                {


                }
                mylib.utility_func.callbackdebuginfo(tmp);
                if (count-- == 0) break;
                if (tmp.IndexOf("Chars Found") >= 0) { rsu = tmp; found_flog = 1; break; }

            } while (tmp.IndexOf("Devices Found") < 0);

            if (found_flog == 1)
            {
                this.ReadExisting();
                this.Write("AT+CHRX" + uuid);
                for (int i = 0; i < 3; i++)
                {
                    string tmp2 = "";
                    try {
                        System.Threading.Thread.Sleep(300);
                    tmp2=this.ReadExisting();
                     mylib.utility_func.callbackdebuginfo(tmp2);
                    
                    }catch (Exception ex) { }

                    if (tmp2.IndexOf("OK") >= 0) {
                        break;
                    }
                    if (i >= 2) {return -3; }
                }
                this.ReadExisting();
                this.Write("AT+CHTX" + uuid);
                if (found_flog == 1)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        string tmp2 = "";
                        try
                        {
                            System.Threading.Thread.Sleep(300);
                            tmp2 = this.ReadExisting();
                            mylib.utility_func.callbackdebuginfo(tmp2);

                        }
                        catch (Exception ex) { }

                        if (tmp2.IndexOf("OK") >= 0)
                        {
                            break;
                        }
                        if (i >= 2) { found_flog = 0; return -4; }
                    }

                }

           
                    return 1;
            }
            else
            {

                return -2;

            }



        }

        public int sgw_dn_ble_char_cmd(string cmd) {
            

                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray(cmd);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.ReadExisting();
                this.Write(sendbuf, 0, sendbuf.Length);
            System.Threading.Thread.Sleep(500);
            this.Write(sendbuf, 0, sendbuf.Length);
            System.Threading.Thread.Sleep(500);
            return 1;

        }

        public string sgw_dn_ble_char_cmd_ret(string cmd)
        {
           
            if (cmd.Length > 0)
            {

                byte[] sendbuf = null;
                switch (cmd) {
                    case "read_imei": {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e2 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x13)
                            {

                                byte[] buffer = new byte[rtm];
                                byte[] str_buf = new byte[15];
                                this.Read(buffer, 0, rtm);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));

                                Buffer.BlockCopy(buffer, 4, str_buf, 0, 15);
                                return Encoding.UTF8.GetString(str_buf);
                            }
                            else {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                }
                                return "read_imei_error";
                            }
                        }
                        break;
                    case "read_bt_mac":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e3 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x0a)
                            {

                                byte[] buffer = new byte[rtm];
                                byte[] str_buf = new byte[6];
                                this.Read(buffer, 0, rtm);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));

                                Buffer.BlockCopy(buffer, 4, str_buf, 0, 6);
                                return $"{str_buf[0]:x2}:{str_buf[1]:x2}:{str_buf[2]:x2}:{str_buf[3]:x2}:{str_buf[4]:x2}:{str_buf[5]:x2}".ToUpper();
                            } else
                            {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));


                                }
                                return "read_bt_error";
                            }
                        }
                
                        break;
                    case "read_wifi_mac":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e4 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x0a)
                            {

                                byte[] buffer = new byte[rtm];
                                byte[] str_buf = new byte[6];
                                this.Read(buffer, 0, rtm);
                                Buffer.BlockCopy(buffer, 4, str_buf, 0, 6);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                return $"{str_buf[0]:x2}:{str_buf[1]:x2}:{str_buf[2]:x2}:{str_buf[3]:x2}:{str_buf[4]:x2}:{str_buf[5]:x2}".ToUpper();

                            }
                            else
                            {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                }
                                return "read_WIFI_error";
                            }
                        }

                
                        break;
                    case "read_sn":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e5 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x19)
                            {

                                byte[] buffer = new byte[rtm];
                                byte[] str_buf = new byte[21];
                                this.Read(buffer, 0, rtm);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));

                                Buffer.BlockCopy(buffer, 4, str_buf, 0, 21);
                              
                                return Encoding.UTF8.GetString(str_buf);
                            }
                            else
                            {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                }
                                return "read_SN_error";
                            }
                        }
                        break;
                    case "reset_flog":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e1 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x5)
                            {

                                byte[] buffer = new byte[rtm];                            
                                this.Read(buffer, 0, rtm);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));

                                return BitConverter.ToString(buffer);
                            }
                            else
                            {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                }
                                return "flog_error";
                            }
                        }
                        break;
                    case "set_flog":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray("e0 01 00 00");

                            mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                            this.ReadExisting();
                            this.Write(sendbuf, 0, sendbuf.Length);
                            System.Threading.Thread.Sleep(800);
                            int rtm = this.BytesToRead;
                            if (rtm == 0x5)
                            {

                                byte[] buffer = new byte[rtm];
                                this.Read(buffer, 0, rtm);
                                mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));

                                return BitConverter.ToString(buffer);
                            }
                            else
                            {
                                if (rtm > 0)
                                {
                                    byte[] buffer = new byte[rtm];
                                    this.Read(buffer, 0, rtm);
                                    mylib.utility_func.callbackdebuginfo("rev_data:" + BitConverter.ToString(buffer));
                                }
                                return "flog_error";
                            }
                        }
                        break;
                    default:
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray("e2 01 00 00");
                        }
                        break;

                }
                
            
                
                

                
               
            }
            return "error";
        }
        public (int,double) read_rssi_by_mac(string mac= "34b7da6b8a42",int try_times=5, double shield_val = -45) {


            mac = mac.ToUpper().Replace("0X", "").Replace(":", "").Replace("-", "");

            this.ReadExisting();
            this.Write($"AT+CON{mac}");
            string str_ret = "";
            try
            {
                this.ReadLine();
                str_ret = this.ReadLine();
                mylib.utility_func.callbackdebuginfo(str_ret);
            }
            catch {

                str_ret = "error";
            }

            if (str_ret.IndexOf("OK+CONN") < 0) return (-1,double.NaN);
            for (int j = 0; j < 20; j++) {

                string tmp_str = this.ReadLine();
                mylib.utility_func.callbackdebuginfo(tmp_str);
                if (tmp_str.IndexOf("Chars Found") >= 0) {

                    this.ReadExisting();
                    break;
                }

            }
          
            this.Write("AT+RSSI?");
            for (int t = 0; t < try_times; t++) {


               string rssistr =  this.ReadLine();
                mylib.utility_func.callbackdebuginfo(rssistr);
                string pattern = @"(?<=RSSI\(dB\):\s)-\d+";

                System.Text.RegularExpressions.Match match = Regex.Match(rssistr, pattern);
                if (match.Success)
                {
                    if (double.Parse(match.Value) >= shield_val)
                    {
                       
                        return (1, double.Parse(match.Value));

                    }
                    else { 
                    
                    continue;
                    }
                }
                else {
                    continue;
                }


            }

            return (-2, double.NaN);
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

        public double[] ad_board_get_voltage_8ch() {



            for(int i =0; i < 3; i++) { 
                this.Write("A");
                System.Threading.Thread.Sleep(1000);

               string rs = this.ReadExisting().Trim();
                string[] rsd = rs.Split(",".ToArray());
                if (rsd.Count() == 8)
                {
                    double[] retdouble = new double[8];
                    for (int i1 = 0; i1 < 8; i1++)
                    {

                        retdouble[i1] = double.Parse(rsd[i1]);
                    }

                    return retdouble;

                }

               
            }



            return new double[] { -10086 };

        }


        public int get_fft_freq_amp_status(int threshold, double find_freq, int times)
        {
            StringBuilder strmsg = new StringBuilder();
           int  result = 0;
            this.ReadExisting();
            for (int try1 = 0; try1 < times; try1++)
            {
                System.Threading.Thread.Sleep(10);
                this.WriteLine($"{threshold}");
                System.Threading.Thread.Sleep(40);
                
                string str_rev = this.ReadExisting();
                if (str_rev.Length > 5) strmsg.AppendLine(str_rev);
              //  utility_func.callbackdebuginfo(str_rev);

                string findstr = @"(\d{1,6}.\d{2})Hz\s(\d{1,7})";
                MatchCollection matchs;
                Regex rex = new Regex(findstr, RegexOptions.IgnoreCase);
                matchs = rex.Matches(str_rev.ToString());
                int z = matchs.Count;
                if (z == 0) continue;
                double freq = 0;
                double amps = 0;
                double jud_freq = find_freq;
                double jud_amps = 0;
                double freq_temp = 0;
                double amps_temp = 0;
                for (int i2 = 0; i2 < z; i2++)
                {
                    freq = double.Parse(matchs[i2].Groups[1].Value);
                    amps = double.Parse(matchs[i2].Groups[2].Value);
                    utility_func.callbackdebuginfo(freq + ";" + amps);
                    if ((Math.Abs(jud_freq - freq) <= 500 || Math.Abs(jud_freq * 2 - (freq)) <= 500 )&& amps >= threshold)
                    {
                        freq_temp = freq;
                        amps_temp = amps;
                        break;
                    };
                }
                if (freq_temp == 0) continue;

                //for (int i2 = 0; i2 < z; i2++)
                //{

                //    freq = double.Parse(matchs[i2].Groups[1].Value);
                //    amps = double.Parse(matchs[i2].Groups[2].Value);
                //    if (freq_temp == freq) continue;
                //    if (amps_temp - amps <= 0)
                //    {
                //        result--;

                //        break;
                //    }

                //}

                result++;
            }
            utility_func.callbackdebuginfo(strmsg.ToString());
            return result;


        }


        public int get_fft_freq_amp_status_mul(double threshold, string[] freqs, int times)
        {
            StringBuilder strmsg = new StringBuilder();
            int result = 0;
            this.ReadExisting();
            double[] jud_freqs = new double[freqs.Length-1];
        
            for(int x =0; x<freqs.Length-1;x++)
            {

                jud_freqs[x] = double.Parse(freqs[x]);
               
            }
            for (int try1 = 0; try1 < times; try1++)
            {
                System.Threading.Thread.Sleep(10);
                this.WriteLine($"{threshold}");
                System.Threading.Thread.Sleep(40);

                string str_rev = this.ReadExisting();
                if (str_rev.Length > 5) strmsg.AppendLine(str_rev);
                //  utility_func.callbackdebuginfo(str_rev);

                string findstr = @"(\d{1,6}.\d{2})Hz\s(\d{1,7})";
                MatchCollection matchs;
                Regex rex = new Regex(findstr, RegexOptions.IgnoreCase);
                matchs = rex.Matches(str_rev.ToString());
                int z = matchs.Count;
                if (z == 0) continue;
                double freq = 0;
                double _amp = 0;
          
                double jud_amps = 0;
                double freq_temp = 0;
                double amps_temp = 0;
                for (int i2 = 0; i2 < z; i2++)
                {
                    freq = double.Parse(matchs[i2].Groups[1].Value);
                    _amp = double.Parse(matchs[i2].Groups[2].Value);
                    utility_func.callbackdebuginfo(freq + ";" + _amp);
                    foreach (double freq_j in jud_freqs) { 
                        if ((Math.Abs(freq_j - freq) <= 250 || Math.Abs(freq_j * 2 - (freq)) <= 250) && _amp >= threshold)
                        {
                            freq_temp = freq;
                            amps_temp = _amp;
                            goto next_i;
                        };
                    }
                }
                next_i:
                if (freq_temp == 0) continue;

                //for (int i2 = 0; i2 < z; i2++)
                //{

                //    freq = double.Parse(matchs[i2].Groups[1].Value);
                //    amps = double.Parse(matchs[i2].Groups[2].Value);
                //    if (freq_temp == freq) continue;
                //    if (amps_temp - amps <= 0)
                //    {
                //        result--;

                //        break;
                //    }

                //}

                result++;
            }
            utility_func.callbackdebuginfo(strmsg.ToString());
            return result;


        }
        /// <summary>
        /// 单片机采集声音
        /// </summary>
        /// <param name="threshold"></param>
        /// <param name="find_freq"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public int get_fft_freq_amp(int threshold, double find_freq, ref string result)
        {

            for (int i = 0; i < 3; i++)
            {
                this.WriteLine($"{threshold}");
                System.Threading.Thread.Sleep(500);
                string str_rev = this.ReadExisting();
                string findstr = @"(\d{1,6}.\d{2})Hz\s(\d{1,5})";
                MatchCollection matchs;
                Regex rex = new Regex(findstr, RegexOptions.IgnoreCase);
                matchs = rex.Matches(str_rev.ToString());
                int z = matchs.Count;
                if (z == 0) continue;
                double freq = 0;
                double amps = 0;
                double jud_freq = find_freq;
                double jud_amps = 0;

                for (int i2 = 0; i2 < z; i2++)
                {


                    freq = double.Parse(matchs[i2].Groups[1].Value);
                    amps = double.Parse(matchs[i2].Groups[2].Value);
                    result = "1:" + freq + ";" + amps;
                    if (Math.Abs(jud_freq - freq) <= 100 && amps >= threshold)
                    {

                        break;
                    };



                }
                if (Math.Abs(jud_freq - freq) > 100) continue;
                this.WriteLine($"{threshold}");
                System.Threading.Thread.Sleep(500);
                str_rev = this.ReadExisting();
                findstr = @"(\d{1,6}.\d{2})Hz\s(\d{1,5})";
                matchs = rex.Matches(str_rev.ToString());
                int z2 = matchs.Count;
                if (z2 == 0) continue;
                freq = 0;
                amps = 0;

                for (int i2 = 0; i2 < z; i2++)
                {


                    freq = double.Parse(matchs[i2].Groups[1].Value);
                    amps = double.Parse(matchs[i2].Groups[2].Value);
                    if (Math.Abs(jud_freq - freq) <= 100 && amps >= threshold)
                    {
                        result = "2:" + freq + ";" + amps;
                        return 1;
                    };


                }

            }
            if (result != null && result.Length > 0)
            {
                ;
            }
            else
            {
                result = "read_error";
            }

            return -1;


        }

        /// <summary>
        /// 采用的是usb转IIC 的适配器
        /// </summary>
        public string desaysv_iic_scan() {
            this.ReadExisting();
            byte[] send_data = new byte[] { 0x01, 0x01, 0x55, 0x57, 0x01, 0x40, 0x10, 0x00, 0x16, 0x00 };
            this.Write(send_data, 0, send_data.Length);
            System.Threading.Thread.Sleep(100);
            if (this.BytesToRead == 6 ) {

                byte[] rev = new byte[6] { 0, 0, 0, 0, 0, 0 };

                this.Read(rev, 0, rev.Length);

               mylib.utility_func.get_bytes_str(rev);

                if (rev[0] == 0x55 && rev[1] == 0x01 && rev[2] == 0x55 && rev[3] == 0x02 && rev[4] == 0x55 && rev[5] == 0x03)
                {

                    return "fail";
                }

            }


            return "pass";


        }
        public void festool_pwm(Int16 freq=50, Int16 duty=50) {

            byte[] freq_arry = BitConverter.GetBytes(freq);
            byte[] duty_arry = BitConverter.GetBytes(freq*10);
            this.ReadExisting();
            byte[] send_data = new byte[] { 0x03, 0x01, 0x55 ,0x59 , freq_arry[1], freq_arry[0], duty_arry[1], duty_arry[0], 0x16 };
            this.Write(send_data, 0, send_data.Length);


        }

        public string dn2_lte_uart1_test(string command, out int status,string regstr = @"(?<=recv\:)LR\d.\d.\d.\d-\d{5}")
        {

         
           this.ReadExisting();
            this.DtrEnable = true;
            System.Threading.Thread.Sleep(500);
            string rsu = "";
            try
            {

                for (int i = 0; i < 10; i++)
                {
                    rsu = this.ReadExisting();

                    this.WriteLine(command);
                    System.Threading.Thread.Sleep(500);
            
                    rsu = this.ReadExisting().Replace("\\d", " ").Replace("\\r", " ");
                    mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);



                    Regex regex = new Regex(regstr);
                    //Match match = regex.Match(rsu);
                    MatchCollection matches = regex.Matches(rsu);
                    if (matches.Count >= 1)
                    {
                        status = 1;
                        rsu = matches[0].Value;
                        return rsu;
                    }
                }

            }
            catch (Exception ex) {
            
            
            }
            status = -1;
            return rsu;

        }

        public (string,string) dn2_lte_uart1_iccid_eid_get( out int status)
        {

           
            this.ReadExisting();
            this.DtrEnable = true;
            System.Threading.Thread.Sleep(500);
            string rsu = "";
            string pattern = @"(?<=\+SQNCCID:\s*)""
                          (?<first>\d+)""
                          ,\s*""
                          (?<second>\d+)""
                         ";
            try
            {

                for (int i = 0; i < 10; i++)
                {
                    this.WriteLine("AT+CFUN=1");
                    System.Threading.Thread.Sleep(200);
                    rsu = this.ReadExisting();
                    mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                    this.WriteLine("AT+SQNCCID?");
                    System.Threading.Thread.Sleep(500);

                    rsu = this.ReadExisting().Replace("\\d", " ").Replace("\\r", " ");
                    mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);



                    var match = Regex.Match(rsu, pattern, RegexOptions.IgnorePatternWhitespace);
                    if (match.Success)
                    {
                        string first = match.Groups["first"].Value;
                        string second = match.Groups["second"].Value;
                        status = 1;
                        return (first, second);
                    }
                    else
                    {
                        status = -1;
                        return (string.Empty, string.Empty);
                    }

                }

            }
            catch (Exception ex)
            {


            }
            status = -1;
            return ("error", "error");

        }

        ~comline()
        {
            this.Close();
        }
    }
}

