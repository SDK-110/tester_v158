using MyVISAInstrument.Mymodule.Extension;
using NAudio.Wave;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Org.BouncyCastle.Bcpg.OpenPgp;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using  System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
 using unvell.ReoGrid;
// using Windows.UI.Xaml.Controls;

namespace testapp.mylib
{


    public struct testlog
    {

        public string test_case_item_number;
        public string test_case_description;
        public string test_case_limit_low;
        public string test_case_limit_hi;
        public string test_case_result_unit;
        public string teset_case_result;
        public string test_case_judge;
        public string test_case_test_span;


    }




    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public struct ONE_BOARD_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
        public Byte[] b1;
        public Byte IOStatus;
        public Byte bHint;//1:debug message 0:none
        public Byte HintMsgLen;
        public UInt16 HintReserved; // u16  Hint保留位
        public UInt32 crc; //u32

    };


    public delegate void post_mesg(string v);


    public static class utility_func
    {

        static object lock_obj = new object();
       public static string GetByteCode(string input)
        {
            // 获取字符串的字节数组形式
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // 将字节数组转换为十六进制字符串形式
            string byteCode = string.Join(" ", bytes.Select(b => b.ToString("X2")));

            return byteCode;
        }
        // text box 提示背景
        public static void set_text_box_watermark_prompt(IntPtr hWnd,string watermark_prompt) {

            SendMessage(hWnd, EM_SETCUEBANNER, IntPtr.Zero, watermark_prompt);
        }
        private const int EM_SETCUEBANNER = 0x1501;

        static List<post_mesg> psmgs = new List<post_mesg>();
        public static void add_msg(post_mesg msg)
        {

            psmgs.Add(msg);
        }
        #region /*sendmessage dll 庫加載*/
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /*跨线程消息*/
        static int USER = 0x0400;
        static int WM_SENDA = USER + 101;
        static int WM_SENDB = USER + 102;
        static int WM_SENDC = USER + 103;
        static int WM_SENDD = USER + 104;
        static int WM_SEND_SET_CC1310LOSS = USER + 110;
        static int WM_SEND_SET_BTLOSS = USER + 111;
        static int WM_SEND_SET_WIFILOSS = USER + 112;
        static int WM_SEND_AUTOTEST = USER + 113;
        static int WM_SENDMACSAVE = USER + 117;
        static int WM_BLE_PATH_LOSS_CH0 = USER + 118;
        static int WM_BLE_PATH_LOSS_CH20 = USER + 119;
        static int WM_BLE_PATH_LOSS_CH39 = USER + 120;
        static int WM_CHANGE_TEXT_BOX1 = USER + 125;
        #endregion
        public static IntPtr ptrWnd;


     public  static  string _GetByteCode(string input)
        {
            // 获取字符串的字节数组形式
            byte[] bytes = Encoding.UTF8.GetBytes(input);

            // 将字节数组转换为十六进制字符串形式
            string byteCode = string.Join(" ", bytes.Select(b => b.ToString("X2")));

            return byteCode;
        }

        public static string GetStringFromByteCode(string byteCode)
        {
            // 将字节码字符串拆分为字节数组
            byte[] bytes = byteCode.Split(' ').Select(b => Convert.ToByte(b, 16)).ToArray();

            // 从字节数组还原为字符串
            string input = Encoding.UTF8.GetString(bytes);

            return input;
        }

        public static void callbackdebuginfo(string m)
        {
            foreach (var msg in psmgs)
            {
                msg(m);
            }
            m = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ": \r\n" + m;
            if (ptrWnd != IntPtr.Zero)
                SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);
            if (!testapp.duochuangti.debug_form.GetDebug_f_instance().IsHidden)
            {

                testapp.duochuangti.debug_form.GetDebug_f_instance().write_msg(m);
            }
            else {

                test_antdui.TestLoggerForm.Instance.AddLog(m);


            }


        }

        public static string ex_module_crc16_str()
        {
            string hex_str = "01 01 00 64 00 01";
            string[] hexValues = hex_str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] data = new byte[hexValues.Length];
            for (int i = 0; i < hexValues.Length; i++)
                data[i] = Convert.ToByte(hexValues[i], 16);
            ushort crc = ModbusCrc16.Compute(data);
            return crc.ToString("X4");
        }

        public static void sendsn_2inputbox(string sn)
        {
            if (ptrWnd != IntPtr.Zero)
                SendMessage(ptrWnd, WM_CHANGE_TEXT_BOX1, IntPtr.Zero, sn);

        }
        public static T ByteToStructure<T>(Byte[] dataBuffer)
        {
            object structure = null;
            int size = Marshal.SizeOf(typeof(T));
            IntPtr allocIntPtr = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(dataBuffer, 0, allocIntPtr, size);
                structure = Marshal.PtrToStructure(allocIntPtr, typeof(T));
            }
            finally
            {
                Marshal.FreeHGlobal(allocIntPtr);
            }
            return (T)structure;
        }

        public static byte[] StructToBytes(object structObj)
        {
            //得到结构体的大小
            int size = Marshal.SizeOf(structObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将结构体拷到分配好的内存空间
            Marshal.StructureToPtr(structObj, structPtr, false);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);
            //返回byte数组
            return bytes;
        }

        //
        //#define SW_HIDE             0 //隐藏窗口，活动状态给令一个窗口
        //#define SW_SHOWNORMAL       1 //用原来的大小和位置显示一个窗口，同时令其进入活动状态
        //#define SW_NORMAL           1
        //#define SW_SHOWMINIMIZED    2
        //#define SW_SHOWMAXIMIZED    3
        //#define SW_MAXIMIZE         3
        //#define SW_SHOWNOACTIVATE   4 //用最近的大小和位置显示一个窗口，同时不改变活动窗口
        //#define SW_SHOW             5 //用当前的大小和位置显示一个窗口，同时令其进入活动状态
        //#define SW_MINIMIZE         6 //最小化窗口，活动状态给令一个窗口
        //#define SW_SHOWMINNOACTIVE  7 //最小化一个窗口，同时不改变活动窗口
        //#define SW_SHOWNA           8 //用当前的大小和位置显示一个窗口，不改变活动窗口
        //#define SW_RESTORE          9 //与 SW_SHOWNORMAL  1 相同
        //#define SW_SHOWDEFAULT      10
        //#define SW_FORCEMINIMIZE    11
        //#define SW_MAX              11
        [DllImport("kernel32.dll")]
        public static extern int WinExec(string exeName, int operType);


        public static void ex_exe_run(string path)
        {


            //  WinExec(@"J:\aaa\endless_loop.cmd", 0);
            WinExec(path, 0);
        }

        public static void killproc(string procname)
        {


            Process[] allprocess = Process.GetProcessesByName(procname);


            foreach (Process process in allprocess)
            {
                try
                {
                    process.Kill();
                }
                catch { }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Regexstr"></param>
        /// <param name="findstr"></param>
        /// <returns></returns>
        public static string findstr_regex(string Regexstr, string findstr)
        {

            Regex rex = new Regex(Regexstr, RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(findstr);

            if (matchs.Count <= 0) return "null";
            else
            {

                return matchs[0].Groups[1].Value;
            }
        }
        public static string _findstr_regex(string Regexstr, string findstr)
        {

            Regex rex = new Regex(Regexstr, RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(findstr);

            if (matchs.Count <= 0) return "null";
            else
            {

                return matchs[0].Groups[0].Value;
            }
        }

        public static double findstr_regex_double_max(string Regexstr = @"WiFi\ssignal\sstrength:\s([-]\d{1,4})", string findstr = "")
        {

            Regex rex = new Regex(Regexstr, RegexOptions.IgnoreCase);
            MatchCollection matchs = rex.Matches(findstr);

            if (matchs.Count <= 0) return -88888888;
            else
            {

                double tempbuf = -8888888;
                for (int i = 0; i < matchs.Count; i++)
                {

                    if (tempbuf < double.Parse(matchs[0].Groups[1].Value)) tempbuf = double.Parse(matchs[0].Groups[1].Value);
                }
                return tempbuf;
            }
        }




        public static uint ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1, 0, 00, 00, System.DateTimeKind.Utc), TimeZoneInfo.Local);
            return (uint)(time - startTime).TotalSeconds;
        }

        public static DateTime LongDateTimeToDateTime(long longDateTime)
        {



            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return start.AddSeconds(longDateTime).ToLocalTime();


        }

        public static string Post(string url, Dictionary<string, string> dic)
        {
            string result = "";

            //添加Post 参数
            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            int i = 0;
            foreach (var item in dic)
            {
                if (i > 0)
                    builder.Append("&");
                builder.AppendFormat("{0}={1}", item.Key, item.Value);
                i++;
            }
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(builder.ToString());

            System.Net.HttpWebRequest _webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            _webRequest.Method = "POST";
            //_webRequest.ContentType = "application/json";
            //内容类型  
            _webRequest.ContentType = "application/x-www-form-urlencoded";
            _webRequest.Timeout = 1000 * 3;
            _webRequest.ContentLength = postData.Length;

            using (System.IO.Stream reqStream = _webRequest.GetRequestStream())
            {
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();
            }

            System.Net.HttpWebResponse resp = (System.Net.HttpWebResponse)_webRequest.GetResponse();
            System.IO.Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (System.IO.StreamReader reader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }


        public static void testlog_save_path(string path_name, string logtxt)
        {


            string path = System.IO.Path.GetDirectoryName(path_name);
            string filename = System.IO.Path.GetFileName(path_name);
            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }
            using (StreamWriter sw = new StreamWriter((path.Length > 0) ? path + @"\" + filename : filename, true))
            {
                sw.Write(logtxt + "\n");
            }
        }

        public static void testlog_save_for_sgw(ref List<testlog> rsts,
                                                   string path,
                                                   string filename,
                                                   string personal_number,
                                                   string product,
                                                   string serial_number,
                                                   string mac_id,
                                                   string start_time,
                                                     string end_time,
                                                   string result
                                                   )
        {

            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(path + @"\" + filename, true))
            {

                sw.Write("Date" + "," + DateTime.Now.ToString("yyMMdd") + "\n");
                sw.Write("Personal Number" + "," + personal_number + "\n");
                sw.Write("Product" + "," + product + "\n");
                sw.Write("Serial Number" + "," + serial_number + "\n");
                sw.Write("MAC_ID" + "," + mac_id + "\n");

                sw.Write("Start Time" + "," + start_time + "\n");
                sw.Write("NO.,item,High Limit,Low Limit,unit,Value,Result,Step Time" + "\n");
                int i = 0;
                foreach (var rs in rsts)
                {

                    sw.Write(i + "," + rs.test_case_description + "," + rs.test_case_limit_hi + "," +
                             rs.test_case_limit_low + "," + rs.test_case_result_unit + "," + rs.teset_case_result + "," + rs.test_case_judge + "," + rs.test_case_test_span + "\n");

                    i++;
                }
                sw.Write("Unix Time" + "," + $"{ConvertDateTimeInt(DateTime.Now):X}" + "\n");
                sw.Write("End Time" + "," + end_time + "\n");
                sw.Write("Test result" + "," + result);

            }



        }





        public static void testlog_new_save_for_smx(ref List<testlog> rsts,
                                              string path,
                                              string filename,
                                              string personal_number,
                                              string line_number,
                                              string work_station,
                                              string product_name,
                                              string serial_number,
                                              string start_time,
                                               string end_time,
                                              string result
                                              )
        {

            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(path + @"\" + filename, false))
            {

                sw.Write("Date" + "," + DateTime.Now.ToString("yyMMdd") + "\n");
                sw.Write("Line" + "," + line_number + "\n");
                sw.Write("Work Station" + "," + work_station + "\n");
                sw.Write("Person ID" + "," + personal_number + "\n");
                sw.Write("Program Name" + "," + product_name + "\n");
                sw.Write("Serial Number" + "," + serial_number + "\n");

                sw.Write("Start Time" + "," + start_time + "\n");
                sw.Write("NO.,item,High Limit,Low Limit,unit,Value,Result,Step Time" + "\n");
                int i = 0;
                foreach (var rs in rsts)
                {
                    string str = rs.test_case_description;
                    int startindex = str.IndexOf("(") + 1;
                    if (startindex > 0)
                    {
                        int endindex = str.IndexOf(")", startindex);

                        str = str.Substring(startindex, endindex - startindex);
                    }

                    if (startindex <= 0) str = "NA";
                    sw.Write(i + "," + rs.test_case_description + "," + rs.test_case_limit_hi + "," +
                             rs.test_case_limit_low + "," + str + "," + rs.teset_case_result + "," + rs.test_case_judge + "," + rs.test_case_test_span + "\n");

                    i++;
                }
                sw.Write("Unix Time" + "," + $"{ConvertDateTimeInt(DateTime.Now):X}" + "\n");
                sw.Write("End Time" + "," + end_time + "\n");
                sw.Write("Test result" + "," + result);

            }



        }







        /// <summary>
        /// MAS 系统上传数据
        /// </summary>
        public static void testlog_save_for_smx(ref List<testlog> rsts,
                                           string path,
                                           string filename,
                                           string personal_number,
                                           string line_number,
                                           string product,
                                           string serial_number,
                                           string start_time,
                                            string end_time,
                                           string result
                                           )
        {

            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(path + @"\" + filename, false))
            {

                sw.Write("Date" + "," + DateTime.Now.ToString("yyMMdd") + "\n");
                sw.Write("Line Number" + "," + line_number + "\n");
                sw.Write("Person ID" + "," + personal_number + "\n");
                sw.Write("Product" + "," + product + "\n");
                sw.Write("Serial Number" + "," + serial_number + "\n");

                sw.Write("Start Time" + "," + start_time + "\n");
                sw.Write("NO.,item,High Limit,Low Limit,unit,Value,Result,Step Time" + "\n");
                int i = 0;
                foreach (var rs in rsts)
                {
                    string str = rs.test_case_description;
                    int startindex = str.IndexOf("(") + 1;
                    if (startindex > 0)
                    {
                        int endindex = str.IndexOf(")", startindex);

                        str = str.Substring(startindex, endindex - startindex);
                    }

                    if (startindex <= 0) str = "NA";
                    sw.Write(i + "," + rs.test_case_description + "," + rs.test_case_limit_hi + "," +
                             rs.test_case_limit_low + "," + str + "," + rs.test_case_result_unit + "," + rs.test_case_judge + "," + rs.test_case_test_span + "\n");

                    i++;
                }
                sw.Write("Unix Time" + "," + $"{ConvertDateTimeInt(DateTime.Now):X}" + "\n");
                sw.Write("End Time" + "," + end_time + "\n");
                sw.Write("Test result" + "," + result);

            }



        }

        /// <summary>
        /// MAS 系统上传数据
        /// </summary>
        public static void sgw_signal_cable_log_save()
        {

            List<testlog> logger = new List<testlog>();

            string pass_fail = "FAIL";
            logger.Add(new testlog()
            {
                teset_case_result = pass_fail,
                test_case_limit_hi = "15",
                test_case_limit_low = "13",
                test_case_description = "test_A001_A003_short",
                test_case_test_span = "332.55",
            });

            string sn = "87654321";

            string filename = sn + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + pass_fail + ".csv";
            string product_name = "A_6152B1016";
            string path_str = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\SMX";


            testlog_save_for_smx(ref logger, path_str + @"\" + product_name + @"\" + DateTime.Now.ToString("yyyyMMdd"), filename, "112233","45678", product_name, sn, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), pass_fail);





        }

        public static void testlog_save_for_asm(ref List<testlog> rsts,
                                              string path,
                                              string filename,
                                              string personal_number,
                                              string product,
                                              string serial_number,
                                              string start_time,
                                                string end_time,
                                              string result
                                              )
        {
            lock (lock_obj) { 
            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(path + @"\" + filename.Replace(@"/", "-").Replace(@"\", "-"), true))
            {

                sw.Write("Date" + "," + DateTime.Now.ToString("yyMMdd") + "\n");
                sw.Write("Personal Number" + "," + personal_number + "\n");
                sw.Write("Product" + "," + product + "\n");
                sw.Write("Serial Number" + "," + serial_number + "\n");


                sw.Write("Start Time" + "," + start_time + "\n");
                sw.Write("NO.,item,High Limit,Low Limit,unit,Value,Result,Step Time" + "\n");
                int i = 0;
                foreach (var rs in rsts)
                {

                    sw.Write(i + "," + rs.test_case_description + "," + rs.test_case_limit_hi + "," +
                             rs.test_case_limit_low + "," + rs.test_case_result_unit + "," + rs.teset_case_result + "," + rs.test_case_judge + "," + rs.test_case_test_span + "\n");

                    i++;
                }

                sw.Write("End Time" + "," + end_time + "\n");
                sw.Write("Test result" + "," + result);

            }
            }
            rsts.Clear();

        }



        public static void testlog_save_for_hayco(ref test_log_tab rsts,
                                       string path,
                                       string filename,
                                       string personal_number,
                                       string product,
                                       string serial_number,
                                       string start_time,
                                       string end_time,
                                       string result
                                       )
        {

            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter((path.Length > 0) ? path + @"\" + filename : filename, true))
            {

                sw.Write("Date" + "," + DateTime.Now.ToString("yyMMdd") + "\n");
                sw.Write("Personal Number" + "," + personal_number + "\n");
                sw.Write("Product" + "," + product + "\n");
                sw.Write("Serial Number" + "," + serial_number + "\n");


                sw.Write("Start Time" + "," + start_time + "\n");
                sw.Write("NO.,Test_Item,Test_Result,Step_time" + "\n");
                int i = 0;
                foreach (var rs in test_log_tab.tm.Keys)
                {

                    sw.Write(i + "," + rs + "," + test_log_tab.tm[rs].test_result[0] + "|" + test_log_tab.tm[rs].test_result[1] + "|" +
                        test_log_tab.tm[rs].test_result[2] + "|" + test_log_tab.tm[rs].test_result[3] + "," + test_log_tab.tm[rs].time_span + "\n");

                    i++;
                }

                sw.Write("End Time" + "," + end_time + "\n");


            }



        }


        public static void testlog_save_for_pycom(ref StringBuilder builder,
                               string path,
                               string filename
                               )
        {

            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter((path.Length > 0) ? path + @"\" + filename : filename, true))
            {

                sw.Write(builder.ToString());

            }



        }
        /// <summary>
        /// 有效数字的处理
        /// </summary>
        /// <param name="bef">有效数字</param>
        /// <returns>三位有效数字，不足则补零</returns>
        public static string ReturnBef(double bef)
        {

            if (bef.ToString() != null)
            {

                char[] arr = bef.ToString().ToCharArray();
                switch (arr.Length)
                {
                    case 1:
                    case 2: return string.Concat(arr[0], ".", "00"); break;
                    case 3: return string.Concat(arr[0] + "." + arr[2] + "0"); break;
                    default: return string.Concat(arr[0] + "." + arr[2] + arr[3]); break;
                }
            }
            else
                return "000";
        }
        /// <summary>
        /// 幂的处理
        /// </summary>
        /// <param name="aft">幂数</param>
        /// <returns>三位幂数部分，不足则补零</returns>
        public static string ReturnAft(int aft)
        {

            if (aft.ToString() != null)
            {

                string end;
                char[] arr = System.Math.Abs(aft).ToString().ToCharArray();
                switch (arr.Length)
                {
                    case 1: { end = "" + arr[0]; break; }
                    case 2: { end = "" + arr[0] + arr[1]; break; }
                    default: { end = System.Math.Abs(aft).ToString(); break; }
                }
                return string.Concat(aft >= 0 ? "" : "-", end);
            }
            else
            {
                return "0";
            }
        }



        public static string KXJSF(double num)
        {

            double bef = System.Math.Abs(num);
            int aft = 0;
            while (bef >= 10 || (bef < 1 && bef != 0))
            {

                if (bef >= 10)
                {

                    bef = bef / 10;
                    aft++;
                }
                else
                {

                    bef = bef * 10;
                    aft--;
                }
            }
            return string.Concat(num >= 0 ? "" : "-", ReturnBef(bef), "E", ReturnAft(aft));
        }
        public static void mysql_logsave(StringBuilder rs, string url)
        {





            new System.Threading.Thread(() =>
            {

                try
                {
                    if (upload_mysql_value(rs.ToString(), url)[0] == "-1")
                    {


                        System.Windows.Forms.MessageBox.Show("数据上传异常");
                    };
                }
                catch (Exception e)
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("db_temp_save.csv", true))
                    {
                        file.WriteLine(rs.ToString() + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"));
                    }
                    System.Windows.Forms.MessageBox.Show("网络异常，请及时联系技术人员" + e);

                }


            }).Start();


        }




        public static void execl_logsave(StringBuilder rs)
        {



            new System.Threading.Thread(() =>
            {

                Worksheet worksheet;
                unvell.ReoGrid.ReoGridControl reo = new ReoGridControl();
                reo.Load(@"avalon_log.xlsx", unvell.ReoGrid.IO.FileFormat._Auto);
                worksheet = reo.Worksheets[0];
                reo.CurrentWorksheet = worksheet;
                int v = 0;
                for (int i = 2; i < 10000; i++)
                {

                    var m = worksheet[i, 0];
                    v = i;
                    if (m == null) break;

                }
                worksheet.RowCount = v + 100;
                string[] results = rs.ToString().Split(",".ToArray());

                for (int i = 0; i < results.Length; i++)
                {

                    if (i == 0) { worksheet[v, i] = v - 2; continue; }
                    if (i == 1)
                    {
                        worksheet[v, 1] = results[2];
                        continue;

                    }
                    if (i == 2)
                    {
                        worksheet[v, 2] = results[1];
                        continue;

                    }

                    worksheet[v, i] = results[i];

                }



                reo.Save(@"avalon_log.xlsx", unvell.ReoGrid.IO.FileFormat.Excel2007);




            }).Start();


        }










        public static string[] get_mysql_value(string serial, string url)
        {
            var para = new Dictionary<string, string>();
            para.Add("serial", serial);
            string m = Post(url, para);
            if (m != null)
            {

                if (m.IndexOf("-1") >= 0) return new string[] { "-1" };
                var t = m.Split(",".ToArray());
                return new string[] { t[0], t[1], t[2] };
            }
            else
            {

                return new string[] { "-1" };


            }


        }




        public static string[] get_mysql_value_tcp(string dbname = "sgw_data",
                                            string tablename = "sgw_button_data",
                                            string serial_number = "",
                                            string status_code = "1",
                                            string mysqlserver = "127.0.0.1"
                                            )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlserver, dbname, "root", "root");
                DataTable rst = mysql.Query($"select * from `{tablename}` where `serial_number` = '{serial_number}' ");
                string str_ret = "";
                if (rst.Rows.Count > 0)
                {

                    for (int ct = 0; ct < rst.Columns.Count; ct++)
                        str_ret = str_ret + ((ct == 0) ? "" : ",") + rst.Rows[0][ct];

                    return str_ret.Split(",".ToArray());
                }
                else
                {

                    return new string[] { "-1" };


                }


            }
            catch (Exception e)
            {

                return new string[] { "-2" };
            }
        }



        public static (int,string,string,string) do_pycom_mac_read_insert(string dbname = "pycom", string tablename = "f01_s3", string first_mac = "", string sec_mac = "", string loradevui = "")
        {

            try
            {

                Mysql mysql = new Mysql("127.0.0.1", dbname, "root", "root");

                DataTable rst = mysql.Query($"select * from `{tablename}` where `First_MAC` = '{first_mac}' ");
                string str_ret = "";
                if (rst.Rows.Count > 0)

                {
                    string str_com = $"update {tablename } set `First_MAC`= '{first_mac}' ,`Sec_MAC` = '{sec_mac}' ,`LoRa_DEVUI` = '{loradevui}', `datetime` =  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}' where  `First_MAC` = '{first_mac}' ";

                    if (mysql.ExecNonQuery(str_com) == 0) return (-2,null,null,null);

                    return (1, first_mac, sec_mac, loradevui);
                }
                else
                {
                    string com_str = $"insert into `{tablename }`(`First_MAC`,`Sec_MAC`,`LoRa_DEVUI`,`datetime`) values ('{first_mac}' , '{sec_mac}','{loradevui}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}')";

                    if (mysql.ExecNonQuery(com_str) == 0) return (-3, null, null, null);

                    return (0, first_mac, sec_mac, loradevui);
                }


            }
            catch(Exception e) {


                return (-1, null, null, null);


            }
           
        }

        public static int instert_mysql_value(string dbname = "sgw_data",
                                               string tablename = "sgw_button_data",
                                               string serial_number = "",
                                               string status_code = "1"
                                               )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("192.168.89.76", dbname, "root", "root");
                //Mysql mysql = new Mysql("127.0.0.1", dbname, "root", "root");
                DataTable rst = mysql.Query($"select * from `{tablename}` where `serial_number` = '{serial_number}' ");
                if (rst.Rows.Count > 0)
                {

                    if (mysql.ExecNonQuery($"update {tablename } set `serial_number`= '{serial_number}' ,`status_code` = '{status_code}' ,`test_date` =  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}' where  `serial_number` = '{serial_number}' ") == 0) return -1;




                }
                else
                {



                    if (mysql.ExecNonQuery($"insert into {tablename }(`id`,`serial_number`,`status_code`,`test_date`) values(null,'{serial_number}','{status_code}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}')") == 0) return -1;


                }

                return 1;
            }
            catch (Exception e)
            {

                return -2;
            }
        }


        public static int instert_mysql_value(
                                              string mysqlip = "",
                                              string dbname = "sgw_data",
                                              string tablename = "sgw_button_data",
                                              string querystr = "",
                                              string updatestr = "",
                                              string installstr = ""
                                              )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlip, dbname, "root", "root");
                DataTable rst = mysql.Query(querystr);
                if (rst.Rows.Count > 0)
                {

                    if (mysql.ExecNonQuery(updatestr) == 0) return -1;




                }
                else
                {



                    if (mysql.ExecNonQuery(installstr) == 0) return -1;


                }

                return 1;
            }
            catch (Exception e)
            {

                return -2;
            }
        }



        public static (int,string,string,string,string, string, string, string, string) tti_instert_mysql_value(
                                           string mysqlip = "127.0.0.1",
                                           string dbname = "tti_db",
                                           string tablename = "burn_in_record",
                                           string sn="",
                                           string model="",
                                           string values="",
                                           double offset_rate=0.02f

            )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlip, dbname, "root", "root");
                DataTable rst = mysql.Query($"select * from {tablename} where sn={sn}");
                if (rst.Rows.Count > 0)
                {
                    int rec = int.Parse(rst.Rows[0][11] as string);
                    int biaoji = 0;
                    string commandstr = "";
                    if (rec > 0 )
                    {
                        if (rec % 2 == 1)
                        {

                            if (Math.Abs(int.Parse(values.Split(';')[0]) - int.Parse(rst.Rows[0][3] as string)) / int.Parse(rst.Rows[0][3] as string) > offset_rate &&

                                Math.Abs(int.Parse(values.Split(';')[1]) - int.Parse(rst.Rows[0][4] as string)) / int.Parse(rst.Rows[0][4] as string) > offset_rate &&
                                Math.Abs(int.Parse(values.Split(';')[2]) - int.Parse(rst.Rows[0][5] as string)) / int.Parse(rst.Rows[0][5] as string) > offset_rate &&
                                 Math.Abs(int.Parse(values.Split(';')[3]) - int.Parse(rst.Rows[0][6] as string)) / int.Parse(rst.Rows[0][6] as string) > offset_rate
                                )
                            {

                                return (-3, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string);
                            }

                            commandstr = $" update {tablename} set  datetime='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', sec_voltage ='{values.Split(';')[0]}', sec_current='{values.Split(';')[1]}',sec_power='{values.Split(';')[2]}',sec_pf='{values.Split(';')[3]}',rec_test_times='{rec + 1}' where sn = '{sn}'";


                        }
                        else {

                            if (Math.Abs(int.Parse(values.Split(';')[0]) - int.Parse(rst.Rows[0][7] as string)) / int.Parse(rst.Rows[0][7] as string) > offset_rate &&

                                Math.Abs(int.Parse(values.Split(';')[1]) - int.Parse(rst.Rows[0][8] as string)) / int.Parse(rst.Rows[0][8] as string) > offset_rate &&
                                Math.Abs(int.Parse(values.Split(';')[2]) - int.Parse(rst.Rows[0][9] as string)) / int.Parse(rst.Rows[0][9] as string) > offset_rate &&
                                 Math.Abs(int.Parse(values.Split(';')[3]) - int.Parse(rst.Rows[0][10] as string)) / int.Parse(rst.Rows[0][10] as string) > offset_rate
                                )
                            {

                                return (-3, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string);
                            }


                            commandstr = $" update {tablename} set datetime='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', first_voltage ='{values.Split(';')[0]}', first_current='{values.Split(';')[1]}',first_power='{values.Split(';')[2]}',first_pf='{values.Split(';')[3]}',rec_test_times='{rec + 1}' where sn = '{sn}'";



                        }
                     
                     
                    }
                    else {


                        



                        commandstr = $" update {tablename} set datetime='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', first_voltage ='{values.Split(';')[0]}', first_current='{values.Split(';')[1]}',first_power='{values.Split(';')[2]}',first_pf='{values.Split(';')[3]}',rec_test_times='{rec + 1}' where sn = '{sn}'";
                  
                    }


                    if (mysql.ExecNonQuery(commandstr) == 0) return (-1, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string); ;

                   return (rec, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string); ;

                }
                else
                {

                  string    commandstr = $"insert into {tablename} (model_name,first_voltage,first_current,first_power,first_pf, sn, datetime,rec_test_times)values('{model}','{values.Split(';')[0]}','{values.Split(';')[1]}','{values.Split(';')[2]}','{values.Split(';')[3]}','{sn}','{DateTime.Now.ToString(" yyyy-MM-dd HH:mm:ss")}','1')";

                    if (mysql.ExecNonQuery(commandstr) == 0) return (-2, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string); ;
                   


                }

                return (1, rst.Rows[0][3] as string, rst.Rows[0][4] as string, rst.Rows[0][5] as string, rst.Rows[0][6] as string, rst.Rows[0][7] as string, rst.Rows[0][8] as string, rst.Rows[0][9] as string, rst.Rows[0][10] as string); ;
            }
            catch (Exception e)
            {

                return (-4, null,null,null,null,null,null,null,null) ;
            }
        }

        public static string[] get_mysql_value_tcp(string dbname = "sgw_data",
                                           string tablename = "sgw_button_data",
                                           string querystr = "",
                                           string mysqlserver = "127.0.0.1"
                                           )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlserver, dbname, "root", "root");
                DataTable rst = mysql.Query(querystr);
                string str_ret = "";
                if (rst.Rows.Count > 0)
                {

                    for (int ct = 0; ct < rst.Columns.Count; ct++)
                        str_ret = str_ret + ((ct == 0) ? "" : ",") + rst.Rows[0][ct];

                    return str_ret.Split(",".ToArray());
                }
                else
                {

                    return new string[] { "-1" };


                }


            }
            catch (Exception e)
            {

                return new string[] { "-2" };
            }
        }



        public static int post_data_and_delete_mysql_value(
                                                 string url = "",
                                                 string dbname = "sgw_data",
                                                 string tablename = "sgw_button_data",
                                                 string serial_number = "",
                                                 string status_code = "1"
                                                 )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("127.0.0.1", "{dbname}", "root", "root");
                DataTable rst = mysql.Query($"select * from `{tablename}` where `SerailNumber` = '{serial_number}' ");
                if (rst.Rows.Count > 0)
                {

                    if (upload_mysql_value((rst.Rows[0][1] as string) + "," + (rst.Rows[0][2] as string) + "," + (rst.Rows[0][3] as string), url)[0] != "1") return -1;


                    if (mysql.ExecNonQuery($"delete {tablename } where `serial_number`='{serial_number}'") == 0) return -1;




                }
                else
                {





                }

                return 1;
            }
            catch (Exception e)
            {

                return -3;
            }
        }








        public static string[] upload_mysql_value(string result, string url)
        {

            string[] results = result.ToString().Split(",".ToArray());
            results[results.Length - 1] = results[results.Length - 1].Trim();
            var para = new Dictionary<string, string>();
            for (int i = 0; i < results.Length; i++)
            {
                para.Add("p" + i, results[i]);

            }
            string m = Post(url, para);

            if (m != null)
            {

                if (m.IndexOf("error") >= 0 || m.IndexOf("fail") >= 0) return new string[] { "-1" };

                return new string[] { "1", m };
            }
            else
            {

                return new string[] { "-1" };


            }


        }


        public static string[] hbi_get_mysql_value(string dbname = "hbi_data",
                                    string tablename = "hbi_mac_data",
                                    string mac_address = "",
                                    string status_code = "1",
                                    string mysqlserver = "127.0.0.1"
                                    )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlserver, dbname, "root", "root");
                DataTable rst = mysql.Query($"select * from `{tablename}` order by id  limit 1,1 ");
                return new string[] { "1", rst.Rows[0][0].ToString(), rst.Rows[0][1].ToString() };



            }
            catch (Exception e)
            {

                return new string[] { "-1" };
            }
        }
        public static int hbi_del_mysql_value(string dbname = "hbi_data",
                                   string tablename = "hbi_mac_data",
                                   string mac_address = "",
                                   string mysqlserver = "127.0.0.1"
                                   )
        {

            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql(mysqlserver, dbname, "root", "root");

                if (mysql.ExecNonQuery($"delete from `{tablename}` where `mac_address` = '{mac_address}' ") <= 0) return -1;

                return 1;

            }
            catch (Exception e)
            {

                return -3;
            }
        }

        public static int getid_for_hbi(out string rs)
        {



            try
            {

                rs = "";
                string datebase = "hbi_test";
                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("192.168.89.76", $"{datebase}", "root", "root");
                DataTable rst = mysql.Query($"call get_rand()");
                if (rst.Rows.Count > 0)
                {

                    rs = rst.Rows[0][0].ToString();




                }
                else
                {


                    return -1;


                }

                return 1;
            }
            catch (Exception e)
            {
                rs = "";
                return -3;
            }











        }
        public static int getmac_for_hbi(out string rs)
        {



            try
            {

                rs = "is_empty";
                string datebase = "hbi_test";
                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("127.0.0.1", $"{datebase}", "root", "root");
                DataTable rst = mysql.Query($"call get_mac()");
                if (rst.Rows.Count > 0)
                {

                    rs = rst.Rows[0][0].ToString();

                    int qty = int.Parse(rst.Rows[0][1].ToString());


                }
                else
                {


                    return -1;


                }

                return 1;
            }
            catch (Exception e)
            {
                rs = "";
                return -3;
            }











        }
        public static int getweekday()
        {



            var m = new GregorianCalendar().GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return m;
        }

        public static (int, string) get_hbi_sn(string region)
        {



            try
            {

                string rs = "";

                int w = (int)DateTime.Now.DayOfWeek == 0 ? 7 : (int)DateTime.Now.DayOfWeek;
                int ws = mylib.utility_func.getweekday();

                Mysql mysql = new Mysql("192.168.89.76", $"hbi_test", "root", "root");

                DataTable rst = mysql.Query($"call get_rand()");
                if (rst.Rows.Count > 0)
                {

                    rs = rst.Rows[0][0].ToString();

                }
                else
                {


                    return (-1, "null");
                }

                return (1, (rs.Substring(0, 4) + DateTime.Now.Year.ToString().Substring(2, 2) + ws + region + "23" + w +
                    rs.Substring(4, 4)));
            }
            catch
            {

                return (-2, "error");

            }








        }
        public static string get_utc_str()
        {



            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }

        public static string get_utc_str_long()
        {

            return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
        }

        public static string get_uuid_str()
        {


            return System.Guid.NewGuid().ToString();
        }


        public static int test_mysql_creat_table()
        {

            string mysql_ip = "127.0.0.1";
            string dbname = "sg_test_db";
            string tablename = "hbi_packing_sn_mac";

            string sql = @"CREATE TABLE " + tablename + @"(
                            ID INT AUTO_INCREMENT PRIMARY KEY,
                            SN VARCHAR(255),
                            Result VARCHAR(255),
                            Description TEXT
                        );";

            string sql_insert = @"INSERT INTO hbi_packing_sn_mac( SN, Result, Description) VALUES( 'SN123456', 'Pass', '测试通过，设备运行正常，无异常告警。');";
            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("127.0.0.1", dbname, "root", "root");
                mysql.ExecNonQuery(sql_insert);


                return 1;
            }
            catch (Exception e)
            {

                return -2;
            }

        }

        public static int hbi_upload_sn_mac(string serial_number, string mac)
        {

            string mysql_ip = "127.0.0.1";
            string dbname = "bhi_test";
            string tablename = "hbi_packing_sn_mac";
            try
            {

                //  string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
                Mysql mysql = new Mysql("127.0.0.1", dbname, "root", "root");
                //Mysql mysql = new Mysql("127.0.0.1", dbname, "root", "root");
                DataTable rst = mysql.Query($"select * from `{tablename}` where `mac` = '{mac}' ");
                if (rst.Rows.Count > 0)
                {

                    if (mysql.ExecNonQuery($"update {tablename } set `serial_number`= '{serial_number}',`mac`={mac},`test_date` =  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}' where  `mac` = '{mac}' ") == 0) return -1;




                }
                else
                {



                    if (mysql.ExecNonQuery($"insert into {tablename }(`id`,`serial_number`,`mac`,`test_date`) values(null,'{serial_number}','{mac}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}')") == 0) return -1;


                }

                return 1;
            }
            catch (Exception e)
            {

                return -2;
            }

        }

        public static string get_bytes_str(byte[] rsu)
        {


            string str_rsu = "";

            foreach (byte v in rsu)
            {

                str_rsu = str_rsu + " " + $"{v:x2}";
            }
            callbackdebuginfo(str_rsu);
            return str_rsu;
        }

        public static int usb_plug_in = 0, usb_plug_out = 0;
        public static ManagementEventWatcher watcher = null;
        public static string get_usb_udisk_event(int sec)
        {

            if (watcher != null) { watcher.Dispose(); watcher = null; }
            watcher = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");

            // 设置事件处理程序
            watcher.EventArrived += delegate (object sender, EventArrivedEventArgs e)
            {

                int eventType = Convert.ToInt32(e.NewEvent.Properties["EventType"].Value);

                // 根据事件类型执行相应的操作
                if (eventType == 2)
                {
                    usb_plug_in = 1;
                    // 插入U盘后的操作
                    mylib.utility_func.callbackdebuginfo("U-DISK has been inserted");
                }
                else if (eventType == 3)
                {
                    usb_plug_out = 1;
                    mylib.utility_func.callbackdebuginfo("U-Disk has been ejected");
                }


            };


            watcher.Query = query;
            watcher.Start();
            //    int delay = sec * 10;
            //    while ((usb_plug_in != 1 || usb_plug_out != 1) && delay-- > 0)
            //    {

            //        System.Threading.Thread.Sleep(100);

            //    }

            //    if (delay <= 0)
            //    {

            //        return "fail";
            //    }
            //    // 停止监视事件
            //    watcher.Stop();

            //    return "pass";
            //}

            return "pass";

        }

        public static bool usb_plug_in_out() {


            if ((usb_plug_in == 1 || usb_plug_out == 1))
            {


                return true;
            }
            else {


                return false;
            }


        }

        public static string Serializer<T>(T serialObject) where T : class
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                System.IO.MemoryStream mem = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8);
                ser.Serialize(writer, serialObject);
                writer.Close();

                return Encoding.UTF8.GetString(mem.ToArray());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T Deserialize<T>(string str) where T : class
        {
            try
            {
                XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                StreamReader mem2 = new StreamReader(
                        new MemoryStream(System.Text.Encoding.UTF8.GetBytes(str)),
                        System.Text.Encoding.UTF8);

                return (T)mySerializer.Deserialize(mem2);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static int post_check(string sn = "SN202308180001", string workstation = "ICT",
                                   string url = "http://192.168.11.220:8081/api/v1/trans/start-check")
        {

            using (HttpClient client = new HttpClient())
            {
                var data = new { barcode = sn, workstation = workstation };
                var json = JsonSerializer.Serialize(data);

                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                var responsejson = response.Content.ReadAsStringAsync().Result;
                callbackdebuginfo(responsejson);
                if (responsejson.IndexOf("true") >= 0) return 1;

                return -1;
            }

        }
        static public int post_form_file(string filePath = @"F:\ICT_crash\WindowsFormsApp2\mark.cs",
                                               string barcode = "SN202308180001",
                                               string workstation = "ICT",
                                               string Status = "Fail",
                                               string url = "http://192.168.11.220:8081/api/v1/trans/test-station"

                                               )
        {
            //string filePath = @"F:\ICT_crash\WindowsFormsApp2\mark.cs"; // 替换为实际的文件路径


            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                using (var formData = new MultipartFormDataContent())
                {
                    // 读取文件内容
                    byte[] fileBytes = File.ReadAllBytes(filePath);

                    // 创建文件内容
                    var fileContent = new ByteArrayContent(fileBytes);

                    // 添加文件内容到表单数据
                    formData.Add(fileContent, "logFile", Path.GetFileName(filePath));
                    formData.Add(new StringContent(barcode), "barcode");
                    formData.Add(new StringContent(workstation), "workstation");
                    formData.Add(new StringContent(Status), "Status");


                    // 发送 POST 请求
                    HttpResponseMessage response = client.PostAsync(url, formData).Result;

                    // 检查响应状态
                    if (response.IsSuccessStatusCode)
                    {

                        string result = response.Content.ReadAsStringAsync().Result;
                        callbackdebuginfo("msg:" + result);
                        if (result.ToUpper().IndexOf("TRUE") >= 0) return 1;
                        return -1;
                    }
                    else
                    {
                        // 文件上传失败
                        callbackdebuginfo("msg:" + "文件上传失败");
                        return -2;
                    }
                }
            }


        }

        public static int post_form_file2(string filePath = @"F:\ICT_crash\WindowsFormsApp2\mark.cs",
                                               string barcode = "SN202308180001",
                                               string workstation = "ICT",
                                               string Status = "Fail",
                                               string url = "http://192.168.11.220:8081/api/v1/trans/test-station")
        {

    
            using ( var client = new HttpClient()) { 

                // 准备要发送的表单数据
                var data = new Dictionary<string, string>
                {
                                    { "barcode", barcode },
                                    { "workstation", workstation },
                                       { "Status", Status }
                };
            try
            {
                // 准备要上传的文件
                var fileContent = new StreamContent(File.OpenRead(filePath));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "file",
                    FileName = Path.GetFileName(filePath)
                };

                // 创建一个 MultipartFormDataContent 实例
                var content = new MultipartFormDataContent();
                foreach (var item in data)
                {
                    content.Add(new StringContent(item.Value), item.Key);
                }
                content.Add(fileContent);

                // 创建一个 HttpRequestMessage 实例
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Content = content;

                // 发送请求并等待响应
                var response = client.SendAsync(request).Result;

                // 读取响应的 JSON 数据
                var responseJson = response.Content.ReadAsStringAsync().Result;
                // var result = JsonSerializer.Deserialize<MyResultType>(responseJson);

                string result = response.Content.ReadAsStringAsync().Result;
                callbackdebuginfo("msg:" + result);
                if (result.ToUpper().IndexOf("TRUE") >= 0) return 1;

                return -1;
            }
            catch (Exception e1)
            {

                callbackdebuginfo("debug:" + e1.ToString());
                return -2;
            }

        }
        }

     public  static   byte[] StringToFixedLengthByteArray(string str, int length)
        {
            // 将字符串转换为字节数组
            byte[] stringBytes = Encoding.UTF8.GetBytes(str);

            // 创建一个固定长度的字节数组
            byte[] fixedLengthBytes = new byte[length];

            // 复制字符串字节到固定长度数组
            int copyLength = Math.Min(stringBytes.Length, length);
            Array.Copy(stringBytes, fixedLengthBytes, copyLength);

            // 剩余部分自动填充为 \0（因为 byte 数组默认初始化为 0）

            return fixedLengthBytes;
        }
        public static string HexStringToString(string hexString)
        {
            // 确保字符串长度是偶数
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must have an even length.");
            }

            // 将十六进制字符串转换为字节数组
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }

            // 将字节数组转换为字符串，忽略 \0
            return Encoding.ASCII.GetString(bytes).TrimEnd('\0');
        }
    


    private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;

        // 常量
        public const int OF_READWRITE = 2;
        public const int OF_SHARE_DENY_NONE = 0x40;
        [DllImport("kernel32.dll")]
        public static extern IntPtr _lopen(string lpPathName, int iReadWrite);
        // 关闭文件句柄
        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);
        public static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
        static public bool IsOccupied(string filePath)
        {
            IntPtr handler = _lopen(filePath, OF_READWRITE | OF_SHARE_DENY_NONE);
            CloseHandle(handler);
            return handler == HFILE_ERROR;
        }


        //////////private async void button1_ClickAsync(object sender, EventArgs e)
        //////////{
        //////////    string p = await DoSomethingAsync(this);
        //////////    label1.Text = p;
        //////////}
        
        public static async Task<string> DoSomethingAsync(object m)
        {
            // 模拟一个耗时的操作
            await Task.Delay(2000);

         //   var p = m as Form1;

           // p.Text = "fasdfsdf";
            return "ttttttttttttttttt";
        }

        public static byte[] strByts2ByteArray(String strbytes) {

            strbytes = strbytes.ToUpper().Replace("0X", "");
            string[] strbytes_array = strbytes.Split(" ".ToCharArray());

            byte[] out_byts = new byte[strbytes_array.Length];

            for(int i=0;i<strbytes_array.Length; i++)
            {

                out_byts[i]= byte.Parse(strbytes_array[i], NumberStyles.HexNumber);

            }


            return out_byts;
        
        
        }

        public static StringBuilder run_console_pip(string path = @"D:\my_workspac\temp\U10_PROG\TUV_U3_U10_Programming_Script\Read_MRAM_Data_U10_v02\Read_MRAM_Data.exe",int timeout=30000,string EndStr = "EEPROM Data Retrieved successfully: END")
        {
            var processExited = new ManualResetEvent(false);    
            string exePath = path;
             StringBuilder rsu = new StringBuilder();
            StringBuilder error = new StringBuilder();
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = exePath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                WorkingDirectory=Path.GetDirectoryName(exePath)
            };

            using (Process process = new Process())
            {
                Regex regex = new Regex(EndStr.ToLower(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
                process.EnableRaisingEvents = true;
                process.StartInfo = psi;
                process.OutputDataReceived +=(sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                       mylib.utility_func.callbackdebuginfo("rev:" + e.Data);
                        rsu.Append(e.Data+";");
                        // 检测关键提示，自动发送Enter
                        if (regex.IsMatch(e.Data.ToLower())==true)
                        {
                           process.StandardInput.WriteLine("\r\n");
                           process.StandardInput.Flush();
                           

                        }
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        mylib.utility_func.callbackdebuginfo("rev error:" + e.Data);
                        error.Append(e.Data + ";");

                    }
                };
                process.Exited += (s, e) =>
                {

                    processExited.Set();
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                //string errorOutput = process.StandardError.ReadLine();
                //if (!string.IsNullOrEmpty(errorOutput))
                //{
                //    mylib.utility_func.callbackdebuginfo("Error: " + errorOutput);
                //}


                //Task.Factory.StartNew(() =>
                //{

                //    Thread.Sleep(timeout);
                //    process.Kill();


                //});


                //process.WaitForExit();

                if (!processExited.WaitOne(timeout))
                {
                    try
                    {

                        process.Kill();
                    }
                    catch (Exception e)
                    {
                        mylib.utility_func.callbackdebuginfo("debug:" + e.ToString());

                    }
                }
                else { 
                
                process.WaitForExit();

                }


            }

            return rsu.Append(";" + error.ToString());
            
        }

        private static void Process_Exited(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public static void KillByName(string processName)
        {
            if (string.IsNullOrWhiteSpace(processName)) {

            callbackdebuginfo("进程名不能为空:" +  processName);
                return;
            }
               

            // 自动去除 .exe 后缀（GetProcessesByName 不需要后缀）
            string name = processName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
                ? processName.Substring(0, processName.Length - 4)
                : processName;

            Process[] processes = Process.GetProcessesByName(name);

            if (processes.Length == 0)
            {
               callbackdebuginfo($"⚠️ 未找到名为 \"{processName}\" 的进程。");
                return;
            }

            foreach (Process proc in processes)
            {
                try
                {
                    // 1. 优先尝试优雅关闭（向主窗口发送 WM_CLOSE 消息，适用于 GUI 程序）
                    bool hasMainWindow = proc.CloseMainWindow();

                    // 等待 3 秒，让程序自行保存数据并退出
                    if (hasMainWindow && proc.WaitForExit(3000))
                    {
                        callbackdebuginfo($"✅ 已优雅关闭: {proc.ProcessName} (PID: {proc.Id})");
                        continue;
                    }

                    // 2. 无界面 / 未响应 / 超时未退出，则强制结束
                    proc.Kill();
                    proc.WaitForExit(2000);
                    callbackdebuginfo($"🔪 已强制结束: {proc.ProcessName} (PID: {proc.Id})");
                }
                catch (System.ComponentModel.Win32Exception ex) when (ex.NativeErrorCode == 5)
                {
                    callbackdebuginfo($"🚫 拒绝访问: PID {proc.Id} 需要管理员权限或受系统保护。");
                }
                catch (InvalidOperationException)
                {
                    // 进程已在检查时退出，忽略即可
                }
                catch (Exception ex)
                {
                    callbackdebuginfo($"❌ 结束进程 {proc.Id} 时出错: {ex.Message}");
                }
            }
        }

    }



}
