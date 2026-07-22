//#define hbi_test_log
#define testlogs_teshu
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using IniParser;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using testapp.mylib;
using DeviceLibrary;
using 重构程序.viewmode;
using 重构程序.testcase_loader;
using rebuild.testcase_loader;
using unvell.ReoGrid;
using testapp.glob_set;

/*         try { ((System.ComponentModel.ISupportInitialize)(this.rep1)).EndInit(); }
            catch (Exception) { MessageBox.Show("你没有注册reportX,请注册"); };
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    */










namespace testapp
{


    public partial class parallel_form : Form
    {
        protected override CreateParams CreateParams {


            get {

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;


            }





        }
        public static  test_sub_stop test_xianzhi_stop = null;
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
        public const int WM_SENDA_2 = USER + 108;
        public const int WM_SENDA_3 = USER + 109;
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;
        public const int WM_SEND_RF_REF = USER + 114;
        public const int WM_SENDMYREALY_1 = USER + 115;
        public const int WM_SENDMYREALY_2 = USER + 116;
        public const int WM_SENDMACSAVE = USER + 117;
        public const int WM_BLE_PATH_LOSS_CH0 = USER + 118;
        public const int WM_BLE_PATH_LOSS_CH20 = USER + 119;
        public const int WM_BLE_PATH_LOSS_CH39 = USER + 120;
        public const int WM_TEST_TRIGGER_RUN = USER + 121;
        public const int WM_SENDA_4 = USER + 122;
        public const int WM_SK_RELAY1_SET = USER + 123;
        public const int WM_SK_RELAY2_SET = USER + 124;
        public const int WM_CHANGE_TEXT_BOX1 = USER + 125;
        IntPtr ptrWnd;


        #endregion
        /*--------------message loop dll upload-------------*/
        int timestart = 0;
        /// <summary>
        /// sgw垃圾
        /// </summary>
        string start_time_utc = "";
        List<testlog> testlogs = new List<testlog>();
        DateTime start_time_for_sgw;
        string sgw_record_sn = "";
        string sgw_record_mac = "";
        TimeSpan ts1;
        int i = 2;
        volatile int tablestaues = 0;
        Series series, series2;
        float[] values = new float[24];
        float[] values2 = new float[24];
        Dictionary<int, string> result_temp = new Dictionary<int, string>();
        static public testcase_dll testcase_lib;
        volatile int test_running = 0; // for smx _汽车刹车检测项目
        volatile int test_running_flog1 = 0;
        //用于自动测试系统
      volatile  int auto_status1 = 0;
      volatile  int auto_status2 = 0;
      volatile  int auto_status3 = 0;
        long  test4plc  = DateTime.Now.Ticks;
        private IniParser.FileIniDataParser iniread = glob_ini_instance.getInstance().fileIni;
        IniParser.Model.IniData inidata;
        int p = 0;
        p_reogridviewloader viewloader;
        tester_project mu;
        Worksheet myworksheet1;
        private bool glob_is_cancel;

        public parallel_form()
        {

            //try
            //{
            //  m = new testcase_dll();
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("你的设备资源被霸占请检查");

            //    this.Close();

            //}
            
            InitializeComponent();
            inidata = glob_ini_instance.getInstance().getSetupIniData;
            easyChartX1.AxisX.MajorGridColor = Color.Green;
            easyChartX1.AxisY.MajorGridEnabled = true;
            easyChartX1.AxisY.MajorGridColor = Color.Green;
            easyChartX1.AxisY.MajorGridCount = 20;
            easyChartX1.AxisY.ViewMinimum = -100;
            easyChartX1.AxisY.ViewMaximum = 10;
            /***
                if (dt["setbarcode"]["barenable"] == "true")
                {

                    button2.Enabled = false;
                    textBox1.Focus();
                }
                else {


                    this.textBox1.Enabled = false;
                    this.button2.Focus();
                }

                this.Text = dt["setproduct"]["name"];
            **/
            //  this.Text = dt["setproduct"]["name"];
            // ptrWnd = FindWindow(null, this.Text);
            //  m.ptrWnd = ptrWnd;

        }

        #region  /*-------------LOOP FUNCTION BACKPROC-----------*/
        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        protected override void DefWndProc(ref Message ms)
        {


            switch (ms.Msg) {

                case WM_SENDMACSAVE:
                    {

                        string retdate = Marshal.PtrToStringAnsi(ms.LParam);
                        inidata["setproduct"]["MACADDR"] = retdate;

                        glob_ini_instance.getInstance().write2Ini(inidata);


                    }

                    break;

                case WM_SEND_RF_REF:
                    {

                        double[] dmbvalue = RFExplorer.rf_table_rsu.Values.ToArray();
                        double[] freqvale = RFExplorer.rf_table_rsu.Keys.ToArray();


                        easyChartX1.Plot(dmbvalue, freqvale[0], (freqvale[freqvale.Length - 1] - freqvale[0]) / (freqvale.Length - 1));


                    }
                    break;
                case WM_SENDA:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board"] != null)
                        {

                            testcase_lib.Getfun()["relay_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                        //  MessageBox.Show(Marshal.PtrToStringAnsi(ms.LParam));
                        //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);

                        // dt["cmw100ParameterSet"]["buletoothloss"] = Marshal.PtrToStringAnsi(m.LParam);
                        //   glob_ini_instance.getInstance().write2Ini(dt);


                    }
                    break;
                case WM_SENDA_2:
                    {

                        string tmp = "";
                        if (inidata["setport"]["Relay_board2"] != null)
                        {

                            testcase_lib.Getfun()["relay2_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_3:
                    {

                        string tmp = "";
                        if (inidata["setport"]["Relay_board3"] != null)
                        {

                            testcase_lib.Getfun()["relay3_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_4:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board4"] != null)
                        {

                            testcase_lib.Getfun()["relay4_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }


                    }
                    break;
                case WM_SENDB:
                    {
                        //MessageBox.Show(Marshal.PtrToStringAnsi(m.LParam));
                        //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);
                        // dt["cmw100ParameterSet"]["cc1310loss"] = Marshal.PtrToStringAnsi(m.LParam);
                        // glob_ini_instance.getInstance().write2Ini(dt);

                        this.richTextBox1.AppendText(Marshal.PtrToStringAnsi(ms.LParam) + "\n");
                        this.richTextBox1.ScrollToCaret();


                    }
                    break;
                case WM_SENDD:
                    {
                        this.Close();
                    }
                    break;
                case WM_SENDC:
                    {
                        string[] retdate = Marshal.PtrToStringAnsi(ms.LParam).Split(";".ToArray());
                        inidata["cmw100statuscheck"]["statusyear"] = retdate[0];
                        inidata["cmw100statuscheck"]["statusmonth"] = retdate[1];
                        inidata["cmw100statuscheck"]["statusday"] = retdate[2];
                        inidata["cmw100statuscheck"]["statushour"] = retdate[3];
                        glob_ini_instance.getInstance().write2Ini(inidata);


                    }
                    break;
                case WM_SEND_SET_CC1310LOSS:
                    {

                        inidata["cmw100ParameterSet"]["cc1310loss"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);
                    }
                    break;

                case WM_SEND_SET_BTLOSS:
                    {

                        inidata["cmw100ParameterSet"]["buletoothpathloss"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);

                    }
                    break;
                case WM_BLE_PATH_LOSS_CH0:
                    {
                        inidata["cmw100ParameterSet"]["buletoothpathloss_ch0"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);
                    }
                    break;
                case WM_BLE_PATH_LOSS_CH20:
                    {
                        inidata["cmw100ParameterSet"]["buletoothpathloss_ch20"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);
                    }
                    break;
                case WM_BLE_PATH_LOSS_CH39:
                    {

                        inidata["cmw100ParameterSet"]["buletoothpathloss_ch39"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);
                    }
                    break;

                case WM_SEND_SET_WIFILOSS:
                    {
                        inidata["cmw100ParameterSet"]["wifipathloss"] = Marshal.PtrToStringAnsi(ms.LParam);
                        glob_ini_instance.getInstance().write2Ini(inidata);


                    }
                    break;

                case WM_SEND_AUTOTEST:
                    {/*自動測試消息*/

                        if (backgroundWorker1.IsBusy) return;
                        if (inidata["setproduct"]["ifautotest"].Trim() == "false") return;
                        if (inidata["setbarcode"]["barenable"] == "true")
                        {

                            MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                            //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                            //  return;
                            // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                            if (reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

                            {
                                // button2.PerformClick();
                                testcase_lib.input_sn = this.textBox1.Text;
                                textBox1.Enabled = false;
                                this.timestart = 1;
                                this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
                                ts1 = new TimeSpan(DateTime.Now.Ticks);
                                this.textBox4.Text = "";
                                this.textBox5.Text = "";
                                label3.Text = "running test1";
                                label3.BackColor = Color.GreenYellow;
                                backgroundWorker1.RunWorkerAsync();
                            }
                            // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                            else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
                            {

                                // MessageBox.Show("条码规则不对");
                            }

                        }
                        else
                        {

                            button2.PerformClick();
                        }



                    }
                    break;
                case WM_SENDMYREALY_1:
                    {
                        if (inidata["setport"]["myrelay_board"] != null)
                        {

                            string temp;
                            testcase_lib.Getfun()["myrelay_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                    }
                    break;
                case WM_SENDMYREALY_2:
                    {
                        if (inidata["setport"]["myrelay_board2"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["myrelay_set2"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                    }
                    break;
                case WM_SK_RELAY1_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["sk_relay1_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }


                    }
                    break;
                case WM_SK_RELAY2_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board2"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["sk_relay2_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;

                default:
                    break;

                case WM_TEST_TRIGGER_RUN:
                    {
                        if (test_running_flog1 == 1 || DateTime.Now.Ticks - test4plc < 3000) break;
                        switch (Marshal.PtrToStringAnsi(ms.LParam)) {
                         
                            case "1":
                                {


                                  

                                    MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                                    //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                                    //  return;
                                    // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                                    if (reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

                                    {
                                        // button2.PerformClick();
                                        testcase_lib.input_sn = this.textBox1.Text;
                                        textBox1.Enabled = false;
                                        this.timestart = 1;
                                        this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
                                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                                        this.textBox4.Text = "";
                                        this.textBox5.Text = "";
                                        label3.Text = "running test0";
                                        label3.BackColor = Color.GreenYellow;
                                        backgroundWorker1.RunWorkerAsync();
                                    }
                                    // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                                    else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
                                    {

                                        this.timestart = 1;
                                        this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
                                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                                        this.textBox4.Text = "";
                                        this.textBox5.Text = "";
                                        label3.Text = "running test0";
                                        label3.BackColor = Color.GreenYellow;
                                        viewloader.table_load_into_viewer();
                                        backgroundWorker1.RunWorkerAsync();
                                    }









                                   
                                }
                                break;

                            case "2":
                                {
                                    this.textBox1.Focus();
                                    SendKeys.Send("{ENTER}");
                                }
                                break;


                        }
                      

                    }

                    break;

                case WM_CHANGE_TEXT_BOX1:
                    {
                        if (test_running == 1) return;
                        this.textBox1.Text = Marshal.PtrToStringAnsi(ms.LParam)==""?"NA": Marshal.PtrToStringAnsi(ms.LParam) + '\r';

                        Task.Factory.StartNew(() =>
                        {
                       
                        System.Threading.Thread.Sleep(1000);
                            this.Invoke(new Action(() =>
                            {
                                SendMessage(this.Handle, 0x0112, (IntPtr)0xf120,null);
                                this.textBox1.Focus();
                                SendKeys.Send("{ENTER}");
                            }));
                           
                        });
                      

                    }
                    break;






            }












            base.DefWndProc(ref ms);
        }
        #endregion
        /*-------------LOOP FUNCTION BACKPROC-----------*/

        private void Form1_Load(object sender, EventArgs e)
        {

            #region //chart 绘图
            series = chart1.Series[0];
            series.LegendText = "每小时PASS数";
            // 画样条曲线（Spline）
            series.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series.BorderWidth = 2;
            // 线的颜色：红色
            series.Color = System.Drawing.Color.Green;
            // 图示上的文字
            series.IsValueShownAsLabel = true;
            series2 = chart2.Series[0];
            series2.LegendText = "每小时NG数";
            // 画样条曲线（Spline）
            series2.ChartType = SeriesChartType.Column;
            // 线宽2个像素
            series2.BorderWidth = 2;
            // 线的颜色：红色
            series2.Color = System.Drawing.Color.Red;
            // 图示上的文字
            series2.IsValueShownAsLabel = true;
            #endregion

            //this.textBox2.Left = 5;
            //this.textBox2.Top = this.Bottom-this.Height;
           // this.skinEngine1.SkinFile = "./Warm.ssk";
            //  skinEngine1.AddForm(this);
          //  skinEngine1.SkinFormOnly = true;
            //skinEngine1.SkinDialogs = true;
            chart_display(1);
            this.label1.Text = string.Format("TOTAL：{0}PCS |NG :{1}|OK:{2}", inidata["recorder"]["title"], inidata["recorder"]["titleng"], inidata["recorder"]["titleok"]);

            p = 0;
            ChartArea chartArea = chart1.ChartAreas[0];
            ChartArea chartArea2 = chart2.ChartAreas[0];
            #region 迭代绘图资料
            foreach (int i in new int[] { 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 1, 2, 3, 4, 5, 6, 7 })
            {
                CustomLabel label = new CustomLabel();
                label.Text = i + "";
                label.ToPosition = p * 2;
                chartArea.AxisX.CustomLabels.Add(label);
                chartArea.AxisX.Interval = 1;
                chartArea2.AxisX.Interval = 1;
                //CustomLabel label2 = new CustomLabel();
                //  label2.Text = "";

                //   label2.ToPosition = 1D;
                chartArea2.AxisX.CustomLabels.Add(label);



                p++;
            }
            #endregion
            #region 语言设定
            if (inidata["language"]["english"] == "1")
            {


                this.语言配置ToolStripMenuItem.Text = "setup_language";
                this.英语ToolStripMenuItem.Text = "SetTOEnglish ";
                this.汉语ToolStripMenuItem.Text = "SetToChinese";
                this.toolStripMenuItem1.Text = "SetUp";
                this.修改配置ToolStripMenuItem.Text = "Modify_Test_parameters";
                this.修改后重新加载ToolStripMenuItem.Text = "After_Modify_Reload";
                this.清理白板数据ToolStripMenuItem.Text = "Clear_TheDayShift_TestData";
                this.请夜班数据ToolStripMenuItem.Text = "Clear_TheNightShift_TestData";
                this.同时清除白夜班数据ToolStripMenuItem.Text = "Clear_AllShift_TestData";
                this.series.LegendText = "passed per hour";
                this.series2.LegendText = "fail per hour";
                this.设置项目ToolStripMenuItem.Text = "OtherSetUp";
                this.label2.Text = "please scan barcode :";
                this.打开校验程序表ToolStripMenuItem.Text = "load calibration table";
                this.重新加载测试表ToolStripMenuItem.Text = "after reload test table";
                this.调试DEBUGToolStripMenuItem.Text = "relay debug";
                this.productionInfoToolStripMenuItem.Text = "product_info_set";

            }
            else
            {


                this.语言配置ToolStripMenuItem.Text = "语言设置";
                this.英语ToolStripMenuItem.Text = "设置到英语 ";
                this.汉语ToolStripMenuItem.Text = "汉语";
                this.toolStripMenuItem1.Text = "设置";
                this.修改配置ToolStripMenuItem.Text = "修改配置";
                this.设置项目ToolStripMenuItem.Text = "设置项目";
                this.修改后重新加载ToolStripMenuItem.Text = "修改后重新加载";
                this.清理白板数据ToolStripMenuItem.Text = "清理白板数据";
                this.请夜班数据ToolStripMenuItem.Text = "请夜班数据";
                this.同时清除白夜班数据ToolStripMenuItem.Text = "同时清除白夜班数据";
                this.series.LegendText = "每小时PASS数";
                this.series2.LegendText = "每小时NG数";
                this.label2.Text = "条码扫入：";
                this.打开校验程序表ToolStripMenuItem.Text = "打开校验程序表";
                this.重新加载测试表ToolStripMenuItem.Text = "重新加载测试表";
                this.调试DEBUGToolStripMenuItem.Text = "调试debug";
                this.productionInfoToolStripMenuItem.Text = "生产信息设定";






            }

            #endregion







            mu = excel2tester_standard.read_excel_test_cases(SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0]);
            this.reoGridControl1.CurrentWorksheet.SetSettings(WorksheetSettings.View_ShowRowHeader, false);
        }

        private void update_dataview(tester_standard_style tester, int id)
        {

         
                string[] str_split = tester.parallel_get_result.Split(',');
                for (int i = 0; i < str_split.Length; i++)
                {

                    this.viewloader.myworksheet1.Cells[id, 4 + i].Data = str_split[i];
                }
            if (tester.parallel_judge_result_msg.ToUpper() == "PASS")
            {
                this.viewloader.myworksheet1.Cells[id, 4].Data = "PASS";
                this.viewloader.myworksheet1.Cells[id, 5].Data = "PASS";
                this.viewloader.myworksheet1.Cells[id, 6].Data = "PASS";
                this.viewloader.myworksheet1.Cells[id, 7].Data = "PASS";
            }

                this.viewloader.myworksheet1.Cells[id, 8].Data = tester.parallel_judge_result_msg;
                this.viewloader.myworksheet1.Cells[id, 9].Data = tester.runtime;
                //viewloader.dt.Rows[id]["测试值"] = tester.result_msg;
                //viewloader.dt.Rows[id]["结论"] = tester.get_judge_result;
                //viewloader.dt.Rows[id]["运行时间"] = tester.runtime;
                this.Invoke((Action)delegate
                {


                    if (tester.parallel_judge_result_msg.ToUpper() == "PASS") {


                        this.viewloader.myworksheet1.Cells[id, 8].Style.TextColor = Color.Green;
                        this.viewloader.myworksheet1.Cells[id, 4].Style.TextColor = Color.Green;
                        this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor = Color.Green;
                        this.viewloader.myworksheet1.Cells[id, 6].Style.TextColor = Color.Green;
                        this.viewloader.myworksheet1.Cells[id, 7].Style.TextColor = Color.Green;
                    }

                    if (tester.parallel_judge_result_msg.ToUpper() == "FAIL")
                    {


                        this.viewloader.myworksheet1.Cells[id, 8].Style.TextColor = Color.Red;
                        this.viewloader.myworksheet1.Cells[id, 4].Style.TextColor = Color.Red;
                        this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor= Color.Red;
                        this.viewloader.myworksheet1.Cells[id, 6].Style.TextColor=Color.Red;
                        this.viewloader.myworksheet1.Cells[id, 7].Style.TextColor= Color.Red;
                    }

                    if (tester.get_judge_result.ToUpper() == "skip".ToUpper())
                    {

                        this.viewloader.myworksheet1.Cells[id, 4].Style.TextColor = Color.OrangeRed;
                        this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor = Color.OrangeRed;
                        this.viewloader.myworksheet1.Cells[id, 6].Style.TextColor = Color.OrangeRed;
                        this.viewloader.myworksheet1.Cells[id, 7].Style.TextColor = Color.OrangeRed;
                        this.viewloader.myworksheet1.Cells[id, 8].Style.TextColor = Color.OrangeRed;
                    }

                    if (tester.parallel_judge_result_msg.Split(',').Length> 1)
                        {

                            int count = 0;
                            foreach(string s in tester.parallel_judge_result_msg.Split(','))
                            {
                                if (s.ToUpper() == "P")
                                {
                                    this.viewloader.myworksheet1.Cells[id, 4+count].Style.TextColor = Color.Green;
                                }
                               if (s.ToUpper() == "F") 
                                { 

                                    this.viewloader.myworksheet1.Cells[id, 4 + count].Style.TextColor = Color.Red;

                                }
                              count++;
                            }
                           


                        }


                    this.viewloader.myworksheet1.ScrollToCell(this.viewloader.myworksheet1.Cells[id, 3]);
                    this.viewloader.myworksheet1.SelectRows(id, 1);




                });

            
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

 

            //rep1.Left = 5;
            //this.textBox2.Height = (int)(this.Height * 0.2);
            //this.textBox2.Top= this.Height- (int)(this.Height * 0.2) +30;
            //this.textBox2.Width = this.Width;


            //rep1.Width = this.Width / 2;
            //rep1.Height = (int)(this.Height * 0.8);
            //chart1.Width = (int)(this.Width / 2);
            //chart1.Top = rep1.Top;
            //chart1.Left = rep1.Right;
            //chart2.Width = (int)(this.Width / 2);
            //chart2.Left = rep1.Right;
            //chart2.Top = chart1.Bottom;

            //button2.Left = rep1.Right;
            //button2.Top = label1.Bottom;
            //this.groupBox1.Left = rep1.Right;
            //this.richTextBox1.Left = this.rep1.Right;
            if (viewloader != null) viewloader.view_update();
            this.Refresh();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (inidata["statu"]["plc_auto"] != null) return;
            if (button2.Enabled == false) return;

            // rep1.ClearCell(7, 2, 7, 300);
            // rep1.ClearCell(8, 2, 8, 300);
            //SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            this.textBox1.Text = "";
            this.timestart = 1;
            this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            label3.Text = "running test0";
            label3.BackColor = Color.GreenYellow;
            button2.Enabled = false;
            viewloader.table_load_into_viewer();
            backgroundWorker1.RunWorkerAsync();




        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            test_running = test_running_flog1 = 1;
            var case_jumper = inidata["statu"]["case_jumper"];
          
            string stemp = "";
            this.Invoke(new Action(() =>
             {
                 //this.timer3.Enabled = false;
                 this.richTextBox1.Text = "";
                 this.textBox2.Text = "";
             }));

            string tempbuf;
            try
            {
                testcase_lib.Getfun()["record_buf_var_init"]("pass", "pass", out tempbuf);
               
                string mangmu = "0";
               
                start_time_utc = mylib.utility_func.get_utc_str();
                while (1 == 1)
                {

                    for (int rc = 0; rc < viewloader.tester_proj.test_cases.Count; rc++) {
                        if (backgroundWorker1.CancellationPending == true) { glob_is_cancel = true; break; }

                        int cout = int.Parse(viewloader.tester_proj.test_cases[rc].self_run_count);
                        while ((--cout) >= 0)
                        {
                            var runjud = viewloader.tester_proj.test_cases[rc].parallel_get_rusult(ref viewloader.testcase_lib);
                            backgroundWorker1.ReportProgress(rc, viewloader.tester_proj.test_cases[rc].result_msg);
                            if (runjud == judge_result.pass || runjud == judge_result.skip) break ;
                          
                            
                        }

         
                        this.Invoke(new Action(() =>
                        {

                            this.textBox2.AppendText("debug : --> setp" + (rc) + ":-->" + viewloader.tester_proj.test_cases[rc].result_msg + "\r\n");
                        }));

                       
                    }
                    break;
                }
            exit:
                ;
   

            }
            catch (Exception m) {


                MessageBox.Show(m.ToString());
            
            }
            string[] array_result = { "P", "P", "P", "P" };
            for (int ti = 0; ti < viewloader.tester_proj.test_cases.Count; ti++)
            {

                string[] str_j = viewloader.tester_proj.test_cases[ti].parallel_judge_result_msg.ToUpper().Split(',');

                if (viewloader.tester_proj.test_cases[ti].parallel_judge_result_msg.ToUpper() == "FAIL") array_result[0] = "F";
                for (int z = 0; z < str_j.Length; z++) {
                    if (str_j[z].ToUpper() == "F") array_result[z] = "F";


                }

            }
            e.Result= array_result[0] +","+ array_result[1] + ","+ array_result[2] + "," + array_result[3] ;
            string tempstr = "";
            testcase_lib.Getfun()["testsysini"]("pass", "pass", out stemp, "00;00");
         
            
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int i = e.ProgressPercentage;
                string resu = (string)e.UserState;


                label3.Text = "running" + $"{ i.ToString()}";

            }
            catch (Exception) { }


            //  MessageBox.Show(a + "");

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region 取消线程处理
            if (glob_is_cancel == true) { glob_is_cancel = false;

                this.timestart = 2;
                if (inidata["setbarcode"]["barenable"] != "true")
                {
                    this.button2.Enabled = true;
                    this.button2.Focus();
                }
                else
                {

                    textBox1.Enabled = true;

                    // this.textBox1.SelectAll();
                    this.sgw_record_sn = this.textBox1.Text;
                    this.textBox1.Text = "";
                    this.textBox1.Focus();
                }

                return;

            }

            #endregion

            // MessageBox.Show((string)e.Result);
            string resultt = "pass";
            StringBuilder s = new StringBuilder();
           
            if (!File.Exists("result.csv"))
            {
                string m, v = "time,serialno,result";


                for (int rdi = 0; rdi < viewloader.tester_proj.test_cases.Count; rdi++)
                {


                    v = v + "," + viewloader.tester_proj.test_cases[rdi].testcase_description.Trim() + "( " + viewloader.tester_proj.test_cases[rdi].testcase_high_limit.Replace(",", "#") + "<-->" + viewloader.tester_proj.test_cases[rdi].testcase_low_limit.Replace(",", "#") + ")";
                    i++;

                }
                v = Encoding.ASCII.GetString(Encoding.Default.GetBytes(v));

                File.AppendAllText("result.csv", v + '\n');
            }

            bool is_teshu_logger_detection = false;
            s.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ",");
            //    s.Append(((this.textBox1.Text == "") ? "skip" : this.textBox1.Text) + ",");
            s.Append(((this.textBox1.Text == "") ? "NA" : this.textBox1.Text.Trim()) + ",");
            string sn_temp = (this.textBox1.Text == "") ? "NA" : this.textBox1.Text.Trim();
            s.Append(((string)e.Result).Replace(',',';') + ",");
            int teshulogger_count = 0;


            if (((string)e.Result).IndexOf('F')>0  && inidata["setproduct"]["lock"] != null) {
                this.Invoke(new Action(() => {

                    this.label3.Text = "DataBase error";
                    this.label3.BackColor = Color.DarkRed;
                
                }));
               
            }
#if hbi_test_log
            log_tool hbi_lg = null;
            if (dt["setproduct"]["logger"] !=null && dt["setproduct"]["logger"] == "hbi" && this.textBox1.Text.Length>2)
            {
                
                hbi_lg = new log_tool(testcase_lib._hbi_sn, testcase_lib._hbi_mac,start_time_utc, ((string)e.Result).ToUpper());
            }
#endif
            testlogs.Clear();
            for (int a = 0; a < viewloader.tester_proj.test_cases.Count; a++)
            {

                if (viewloader.tester_proj.test_cases[a].is_teshu_logger)
                {
#if hbi_test_log
                    if (dt["setproduct"]["logger"] != null && dt["setproduct"]["logger"] =="hbi" && this.textBox1.Text.Length > 2)
                    {
                        hbi_lg.add_item(viewloader.tester_proj.test_cases[a].testcase_description,
                                   (viewloader.tester_proj.test_cases[a].runtime / 1000).ToString(),
                                   viewloader.tester_proj.test_cases[a].utc_long,
                                   viewloader.tester_proj.test_cases[a].get_judge_result.ToUpper(),
                                   new
                                   {
                                       test_result = viewloader.tester_proj.test_cases[a].result_msg.ToUpper(),
                                       limit_h = viewloader.tester_proj.test_cases[a].testcase_high_limit.ToUpper(),
                                       limit_l = viewloader.tester_proj.test_cases[a].testcase_low_limit.ToUpper()
                                   }



                        );
                    }
                    // new log_tool(((this.textBox1.Text == "") ? "NA" : this.textBox1.Text)).add_item(viewloader.tester_proj.test_cases[a].testcase_description,
                    //    viewloader.tester_proj.test_cases[a].result_msg);
#endif

#if testlogs_teshu
                    is_teshu_logger_detection = true; 
                    testlogs.Add(new testlog()
                    {
                        teset_case_result = viewloader.tester_proj.test_cases[a].parallel_get_result,
                        test_case_description = viewloader.tester_proj.test_cases[a].testcase_description,
                        test_case_judge = viewloader.tester_proj.test_cases[a].parallel_judge_result_msg,
                        test_case_limit_hi = viewloader.tester_proj.test_cases[a].testcase_high_limit,
                        test_case_limit_low = viewloader.tester_proj.test_cases[a].testcase_low_limit,
                        test_case_result_unit = viewloader.tester_proj.test_cases[a].parallel_get_result,
                        test_case_test_span = viewloader.tester_proj.test_cases[a].runtime + "",
                        test_case_item_number = teshulogger_count + ""


                    }); ;
#endif
                }
                teshulogger_count++;

                if (viewloader.tester_proj.test_cases[a].parallel_judge_result_msg.ToUpper().IndexOf('F')>0 || viewloader.tester_proj.test_cases[a].parallel_judge_result_msg.ToUpper()=="FAIL")
                { //MessageBox.Show("Test" + viewloader.tester_proj.test_cases[a].testcase_description);
                    resultt = "fail"; }

                    s.Append(viewloader.tester_proj.test_cases[a].parallel_judge_result_msg.Replace(',',';')+ ",");
            }
#if hbi_test_log
            if(hbi_lg!=null)
            hbi_lg.log_csv_save();
#endif


#if avalon_net_upload
                if (dt["setproduct"]["setalalonlogenable"] != null && dt["setproduct"]["avalon_net_work_test"] != null)
                {
                    //   utility_func.execl_logsave(s);

                    utility_func.mysql_logsave(s, dt["setproduct"]["avalon_net_work_test"] + "/endtestpostdata.aardio");
                }
            
#endif
            s.Remove(s.Length - 1, 1);
            s.AppendLine();
            if (tablestaues == 0)
            {
                new System.Threading.Thread(() =>
                {
                    File.AppendAllText("result.csv", s.ToString());
                }).Start();

            }
            else
            {


                File.AppendAllText("cmw100RF_cab_log.csv", s.ToString());

            }
            if (inidata["setbarcode"]["barenable"] != "true")
            {
                this.button2.Enabled = true;
                this.button2.Focus();
            }
            else
            {

                textBox1.Enabled = true;

                // this.textBox1.SelectAll();
                this.sgw_record_sn = this.textBox1.Text;
                this.textBox1.Text = "";
                this.textBox1.Focus();
            }
            int rsc = 0;
            foreach (var st  in (e.Result as string).ToUpper() )
            {
                if (st == 'P') rsc++;
            }

            count(rsc);
            inidata["recorder"]["titleok"] = (int.Parse(inidata["recorder"]["titleok"]) + rsc).ToString();
            glob_ini_instance.getInstance().write2Ini(inidata);
            label3.Text = e.Result.ToString().Replace(',','|');
            if ((string)resultt == "pass")
            {

                if (inidata["setport"]["shieldboxport"] != null)
                {
                    string c = "";
                    testcase_lib.Getfun()["shieldboxopen"]("", "", out c);
                }

              
                label3.BackColor = Color.Green;
            }
            if (resultt == "fail")
            {
               
                label3.BackColor = Color.Red;
               
            }
            chart_display(1);

            inidata["recorder"]["title"] = (int.Parse(inidata["recorder"]["title"]) + 4).ToString();
            glob_ini_instance.getInstance().write2Ini(inidata);

            this.label1.Text = string.Format("total：{0}PCS |NG :{1}|OK:{2}", inidata["recorder"]["title"], inidata["recorder"]["titleng"], inidata["recorder"]["titleok"]);

            this.timestart = 2;
            this.textBox4.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + "";


            TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts3 = ts2.Subtract(ts1).Duration();
            this.textBox5.Text = ts3.ToString() + "";
            string project_flog = inidata["setproduct"]["logger"];
#if testlogs_teshu
            new System.Threading.Thread(() =>
            {

                if (sn_temp.Length <= 2) return;
                if (is_teshu_logger_detection == true) {

                    switch (project_flog) {

                        case "desay_logger":

                            string line_number = "L01";
                            string personal_number = "12345";
                            string surl = "http://192.168.11.220:8081/api/v1/trans/test-station";
                            if (inidata["setproduct"]["line_number"] != null && inidata["setproduct"]["personal_number"] !=null) { 
                            line_number = inidata["setproduct"]["line_number"];
                            personal_number = inidata["setproduct"]["personal_number"];
                                surl = inidata["setproduct"]["surl"];

                            }
                            if (resultt == "pass" )
                            {
                                
                                string filename = inidata["setproduct"]["project"] + "_" + sn_temp.Trim() + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Passed.csv";
                                utility_func.testlog_save_for_smx(ref testlogs,
                                                                "../smx/pass",
                                                                filename,
                                                                personal_number,
                                                               line_number,
                                                                inidata["setproduct"]["project"],
                                                               sn_temp.Trim(),                                
                                                               textBox3.Text,
                                                               textBox4.Text,
                                                                "Passed"
                                                                );
                                try
                                {
                                    Task.Factory.StartNew(() =>
                                    {
                                        int t = 0;
                                        for (int i = 0; i < 2; i++)
                                        {
                                            System.Threading.Thread.Sleep(1000);
                                            while (utility_func.IsOccupied("../smx/pass/" + filename))
                                            {

                                                System.Threading.Thread.Sleep(1000);

                                            }

                                            t = utility_func.post_form_file(filePath: "../smx/pass/" + filename, barcode: sn_temp.Trim(), workstation: line_number, Status: "Pass", url: surl

                                                 );

                                           // t = utility_func.post_form_file();
                                            if (t == 1) break;
                                        }

                                        if (t != 1)
                                        {

                                            utility_func.callbackdebuginfo("web post error !");
                                            utility_func.ex_exe_run("@lock.exe  Note:!!!!!web_post_error!!!!");
                                        }
                                    });

                                }
                                catch { 
                                
                                
                                
                                
                                }

                            }
                            else
                            {

                                string filename = inidata["setproduct"]["project"] + "_" + sn_temp.Trim() + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                                utility_func.testlog_save_for_smx(ref testlogs,
                                                                "../smx/fail",
                                                                filename,
                                                                personal_number,
                                                               line_number,
                                                                inidata["setproduct"]["project"],
                                                              sn_temp.Trim(),
                                                              textBox3.Text,
                                                              textBox4.Text,
                                                                "Failed"
                                                                );

                                try
                                {
                                    Task.Factory.StartNew(() =>
                                    {

                                        for (int i = 0; i < 2; i++)
                                        {
                                            System.Threading.Thread.Sleep(1000);
                                            while (utility_func.IsOccupied("../smx/fail/" + filename))
                                            {

                                                System.Threading.Thread.Sleep(1000);

                                            }

                                            int t = utility_func.post_form_file(filePath: "../smx/fail/" + filename, barcode: sn_temp.Trim(), workstation: line_number, Status: "Fail", url: surl

                                                   );

                                            if (t != 1)
                                            {

                                                utility_func.callbackdebuginfo("web post error !");
                                                utility_func.ex_exe_run("@lock.exe");
                                            }

                                        }
                                    });
                                }
                                catch { }

                            }

                            break;

                        case "wiring_harness_log":

                                        if (resultt == "pass")
                                        {

                                            if (inidata["wiring_harness"] != null)
                                            {
                                                string sn = (this.textBox1.Text == "") ? "NA" : this.textBox1.Text;

                                                string filename = sn + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + ((string)resultt).ToUpper() + ".csv";
                                                string product_name = inidata["wiring_harness"]["cable_test_order_part_number"];
                                                string path_str = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\SMX" + "\\OK";


                                                mylib.utility_func.testlog_save_for_smx(ref testlogs, path_str + @"\" + product_name + @"\" + DateTime.Now.ToString("yyyyMMdd"), filename, filename, product_name, product_name, sn, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), (string)resultt);

                                            }

                                        }
                                        else
                                        {

                                            if (inidata["wiring_harness"] != null)
                                            {
                                                string sn = (this.textBox1.Text == "") ? "NA" : this.textBox1.Text;

                                                string filename = sn + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + ((string)resultt).ToUpper() + ".csv";
                                                string product_name = inidata["wiring_harness"]["cable_test_order_part_number"];
                                                string path_str = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\SMX" + "\\NG";


                                                mylib.utility_func.testlog_save_for_smx(ref testlogs, path_str + @"\" + product_name + @"\" + DateTime.Now.ToString("yyyyMMdd"), filename, filename, product_name, product_name, sn, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), (string)resultt);



                                            }
                                        }                  
                        break;

                        case "sgw_logger":

                            
                                if (resultt == "pass")
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_mac + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Passed.csv";
                                    utility_func.testlog_save_for_sgw(ref testlogs,
                                                                    "./sgw/pass",
                                                                    filename,
                                                                   "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                   (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                   sgw_record_mac,
                                                                   textBox3.Text,
                                                                   textBox4.Text,
                                                                    "Passed"
                                                                    );

                                }
                                else
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_mac + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                                    utility_func.testlog_save_for_sgw(ref testlogs,
                                                                    "./sgw/fail",
                                                                    filename,
                                                                   "'" + "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                  (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                    sgw_record_mac,
                                                                  textBox3.Text,
                                                                  textBox4.Text,
                                                                    "Failed"
                                                                    );



                                
                            }

                            break;
                        case "asm_logger":
                           
                                if (resultt == "pass")
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_sn.Replace(@"\","-").Replace(@"/","-") + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Passed.csv";
                                    utility_func.testlog_save_for_asm(ref testlogs,
                                                                    "./asm_lg/pass",
                                                                    filename,
                                                                   "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                   (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                   textBox3.Text,
                                                                   textBox4.Text,
                                                                    "Passed"
                                                                    );

                                }
                                else
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_sn + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                                    utility_func.testlog_save_for_asm(ref testlogs,
                                                                    "./asm_lg/fail",
                                                                    filename,
                                                                   "'" + "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                  (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                  textBox3.Text,
                                                                  textBox4.Text,
                                                                    "Failed"
                                                                    );



                                
                            }

                            break;
                        case "pontial_coil_logger":

                            
                                if (resultt == "pass")
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_sn + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Passed.csv";
                                    utility_func.testlog_save_for_asm(ref testlogs,
                                                                    "./pontial_coil_logger/pass",
                                                                    filename,
                                                                   "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                   (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                   textBox3.Text,
                                                                   textBox4.Text,
                                                                    "Passed"
                                                                    );

                                }
                                else
                                {

                                    string filename = inidata["setproduct"]["project"] + "_" + sgw_record_sn + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                                    utility_func.testlog_save_for_asm(ref testlogs,
                                                                    "./pontial_coil_logger/fail",
                                                                    filename,
                                                                   "'" + "S000331",
                                                                    inidata["setproduct"]["project"],
                                                                  (sgw_record_sn != null) ? sgw_record_sn : "null",
                                                                  textBox3.Text,
                                                                  textBox4.Text,
                                                                    "Failed"
                                                                    );



                                
                            }
                            break;
                        default:
                            break;



                    }
    

                }

               


                

                
            }).Start(); ;
#endif
            
            GC.Collect();
                test_running =  test_running_flog1 = 0;
           // this.timer3.Enabled = true;

          
        }
    

private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    try
    {
        string c = "pass";

        
        backgroundWorker1.CancelAsync();
        backgroundWorker1.Dispose();

       testcase_lib.Getfun()["releaseport"]("pass", "pass", out c, "");
  }
   catch (Exception ex) {/* MessageBox.Show(ex.StackTrace + ex.ToString());*/}



}



private void 修改配置ToolStripMenuItem_Click(object sender, EventArgs e)
{
            var fr = test_case_edit.get_instance(); ;
            fr.is_called_by_other = 修改后重新加载ToolStripMenuItem_Click;
            fr.Show();
}

private void 修改后重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
{
            this.reoGridControl1.ClearActionHistory();
            mu = excel2tester_standard.read_excel_test_cases(SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0]);
            viewloader = new p_reogridviewloader(ref this.reoGridControl1, testcase_lib.Getfun(), mu);
            

            //mu = json2tester_standard.red_json_test_project("project_tester_name.json");


            viewloader.table_load_into_viewer(3);
            for (int i = 0; i < viewloader.tester_proj.test_cases.Count; i++)
            {

                viewloader.tester_proj[i].tf_handler = update_dataview;
            }
            tablestaues = 0;
            this.Invoke(new Action(()=> {

                this.reoGridControl1.Refresh();
             

            }));

            
}

private void Form1_KeyDown(object sender, KeyEventArgs e)
{

    if (e.KeyData == Keys.Space) {

                if (inidata["setbarcode"]["barenable"] == "true")
                {

                    MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                    //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                    //  return;
                    // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                        if (reg.Count == 0) return;
                        testcase_lib.input_sn = this.textBox1.Text;
                        textBox1.Enabled = false;
                        this.timestart = 1;
                        this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                        this.textBox4.Text = "";
                        this.textBox5.Text = "";
                        label3.Text = "running test0";
                        label3.BackColor = Color.GreenYellow;
                        backgroundWorker1.RunWorkerAsync();
                        textBox1.Focus();
                }
                else
                {

                    if(button2.Enabled==true )
                    { 
                    button2.PerformClick();
                    this.button2.Focus();
                    }
                }



             
        
    }


}


public void count(int rs) {

    DateTime z = DateTime.Now;

     int m = z.Hour;
#region
    switch (m) {
        case 0:
            if (rs == 1)
            {
                inidata["recorder"]["time0OK"] = (int.Parse(inidata["recorder"]["time0OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else {
                inidata["recorder"]["time0NG"] = (int.Parse(inidata["recorder"]["time0NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }

            break;

        case 1:
            if (rs == 1)
            {
                inidata["recorder"]["time1OK"] = (int.Parse(inidata["recorder"]["time1OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time1NG"] = (int.Parse(inidata["recorder"]["time1NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 2:
            if (rs == 1)
            {
                inidata["recorder"]["time2OK"] = (int.Parse(inidata["recorder"]["time2OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time2NG"] = (int.Parse(inidata["recorder"]["time2NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 3:
            if (rs == 1)
            {
                inidata["recorder"]["time3OK"] = (int.Parse(inidata["recorder"]["time3OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time3NG"] = (int.Parse(inidata["recorder"]["time3NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 4:
            if (rs == 1)
            {
                inidata["recorder"]["time4OK"] = (int.Parse(inidata["recorder"]["time4OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time4NG"] = (int.Parse(inidata["recorder"]["time4NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 5:
            if (rs == 1)
            {
                inidata["recorder"]["time5OK"] = (int.Parse(inidata["recorder"]["time5OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time5NG"] = (int.Parse(inidata["recorder"]["time5NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;

        case 6:
            if (rs == 1)
            {
                inidata["recorder"]["time6OK"] = (int.Parse(inidata["recorder"]["time6OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time6NG"] = (int.Parse(inidata["recorder"]["time6NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 7:
            if (rs == 1)
            {
                inidata["recorder"]["time7OK"] = (int.Parse(inidata["recorder"]["time7OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time7NG"] = (int.Parse(inidata["recorder"]["time7NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;

        case 8:
            if (rs == 1)
            {
                inidata["recorder"]["time8OK"] = (int.Parse(inidata["recorder"]["time8OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time8NG"] = (int.Parse(inidata["recorder"]["time8NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 9:
            if (rs == 1)
            {
                inidata["recorder"]["time9OK"] = (int.Parse(inidata["recorder"]["time9OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time9NG"] = (int.Parse(inidata["recorder"]["time9NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 10:
            if (rs == 1)
            {
                inidata["recorder"]["time10OK"] = (int.Parse(inidata["recorder"]["time10OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time10NG"] = (int.Parse(inidata["recorder"]["time10NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 11:
            if (rs == 1)
            {
                inidata["recorder"]["time11OK"] = (int.Parse(inidata["recorder"]["time11OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time11NG"] = (int.Parse(inidata["recorder"]["time11NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 12:
            if (rs == 1)
            {
                inidata["recorder"]["time12OK"] = (int.Parse(inidata["recorder"]["time12OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time12NG"] = (int.Parse(inidata["recorder"]["time12NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 13:
            if (rs == 1)
            {
                inidata["recorder"]["time13OK"] = (int.Parse(inidata["recorder"]["time13OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time13NG"] = (int.Parse(inidata["recorder"]["time13NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 14:
            if (rs == 1)
            {

                inidata["recorder"]["time14OK"] = (int.Parse(inidata["recorder"]["time14OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time14NG"] = (int.Parse(inidata["recorder"]["time14NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 15:
            if (rs == 1)
            {
                inidata["recorder"]["time15OK"] = (int.Parse(inidata["recorder"]["time15OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time15NG"] = (int.Parse(inidata["recorder"]["time15NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;

        case 16:
            if (rs == 1)
            {
                inidata["recorder"]["time16OK"] = (int.Parse(inidata["recorder"]["time16OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time16NG"] = (int.Parse(inidata["recorder"]["time16NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 17:
            if (rs == 1)
            {
                inidata["recorder"]["time17OK"] = (int.Parse(inidata["recorder"]["time17OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time17NG"] = (int.Parse(inidata["recorder"]["time17NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 18:
            if (rs == 1)
            {
                inidata["recorder"]["time18OK"] = (int.Parse(inidata["recorder"]["time18OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time18NG"] = (int.Parse(inidata["recorder"]["time18NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 19:
            if (rs == 1)
            {
                inidata["recorder"]["time19OK"] = (int.Parse(inidata["recorder"]["time19OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time19NG"] = (int.Parse(inidata["recorder"]["time19NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;

        case 20:
            if (rs == 1)
            {
                inidata["recorder"]["time20OK"] = (int.Parse(inidata["recorder"]["time20OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time20NG"] = (int.Parse(inidata["recorder"]["time20NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 21:
            if (rs == 1)
            {
                inidata["recorder"]["time21OK"] = (int.Parse(inidata["recorder"]["time21OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time21NG"] = (int.Parse(inidata["recorder"]["time21NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 22:
            if (rs == 1)
            {
                inidata["recorder"]["time22OK"] = (int.Parse(inidata["recorder"]["time22OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time22NG"] = (int.Parse(inidata["recorder"]["time22NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;
        case 23:
            if (rs == 1)
            {
                inidata["recorder"]["time23OK"] = (int.Parse(inidata["recorder"]["time23OK"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            else
            {
                inidata["recorder"]["time23NG"] = (int.Parse(inidata["recorder"]["time23NG"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(inidata);
            }
            break;


        default:
            break;


    }
#endregion

}

private void 清理白板数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
    inidata["recorder"]["time8NG"] = "0";
    inidata["recorder"]["time8OK"] = "0";
    inidata["recorder"]["time9NG"] = "0";
    inidata["recorder"]["time9OK"] = "0";
    inidata["recorder"]["time10NG"] = "0";
    inidata["recorder"]["time10OK"] = "0";
    inidata["recorder"]["time11NG"] = "0";
    inidata["recorder"]["time11OK"] = "0";
    inidata["recorder"]["time12NG"] = "0";
    inidata["recorder"]["time12OK"] = "0";
    inidata["recorder"]["time13NG"] = "0";
    inidata["recorder"]["time13OK"] = "0";
    inidata["recorder"]["time14NG"] = "0";
    inidata["recorder"]["time14OK"] = "0";
    inidata["recorder"]["time15NG"] = "0";
    inidata["recorder"]["time15OK"] = "0";
    inidata["recorder"]["time16NG"] = "0";
    inidata["recorder"]["time16OK"] = "0";
    inidata["recorder"]["time17NG"] = "0";
    inidata["recorder"]["time17OK"] = "0";
    inidata["recorder"]["time18NG"] = "0";
    inidata["recorder"]["time18OK"] = "0";
    inidata["recorder"]["time19NG"] = "0";
    inidata["recorder"]["time19OK"] = "0";
    glob_ini_instance.getInstance().write2Ini(inidata);
    chart_display(1);

}

private void 请夜班数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
    inidata["recorder"]["time20NG"] = "0";
    inidata["recorder"]["time20OK"] = "0";
    inidata["recorder"]["time21NG"] = "0";
    inidata["recorder"]["time21OK"] = "0";
    inidata["recorder"]["time22NG"] = "0";
    inidata["recorder"]["time22OK"] = "0";
    inidata["recorder"]["time23NG"] = "0";
    inidata["recorder"]["time23OK"] = "0";
    inidata["recorder"]["time0NG"] = "0";
    inidata["recorder"]["time0OK"] = "0";
    inidata["recorder"]["time1NG"] = "0";
    inidata["recorder"]["time1OK"] = "0";
    inidata["recorder"]["time2NG"] = "0";
    inidata["recorder"]["time2OK"] = "0";
    inidata["recorder"]["time3NG"] = "0";
    inidata["recorder"]["time3OK"] = "0";
    inidata["recorder"]["time4NG"] = "0";
    inidata["recorder"]["time4OK"] = "0";
    inidata["recorder"]["time5NG"] = "0";
    inidata["recorder"]["time5OK"] = "0";
    inidata["recorder"]["time6NG"] = "0";
    inidata["recorder"]["time6OK"] = "0";
    inidata["recorder"]["time7NG"] = "0";
    inidata["recorder"]["time7OK"] = "0";
    glob_ini_instance.getInstance().write2Ini(inidata);
    chart_display(1);
}

private void 同时清除白夜班数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
#region //clear ini 
    inidata["recorder"]["time0OK"] = "0";
    inidata["recorder"]["time1OK"] = "0";
    inidata["recorder"]["time2OK"] = "0";
    inidata["recorder"]["time3OK"] = "0";
    inidata["recorder"]["time4OK"] = "0";
    inidata["recorder"]["time5OK"] = "0";
    inidata["recorder"]["time6OK"] = "0";
    inidata["recorder"]["time7OK"] = "0";
    inidata["recorder"]["time8OK"] = "0";
    inidata["recorder"]["time9OK"] = "0";
    inidata["recorder"]["time10OK"] = "0";
    inidata["recorder"]["time11OK"] = "0";
    inidata["recorder"]["time12OK"] = "0";
    inidata["recorder"]["time13OK"] = "0";
    inidata["recorder"]["time14OK"] = "0";
    inidata["recorder"]["time15OK"] = "0";
    inidata["recorder"]["time16OK"] = "0";
    inidata["recorder"]["time17OK"] = "0";
    inidata["recorder"]["time18OK"] = "0";
    inidata["recorder"]["time19OK"] = "0";
    inidata["recorder"]["time20OK"] = "0";
    inidata["recorder"]["time21OK"] = "0";
    inidata["recorder"]["time22OK"] = "0";
    inidata["recorder"]["time23OK"] = "0";
    inidata["recorder"]["time0NG"] = "0";
    inidata["recorder"]["time1NG"] = "0";
    inidata["recorder"]["time2NG"] = "0";
    inidata["recorder"]["time3NG"] = "0";
    inidata["recorder"]["time4NG"] = "0";
    inidata["recorder"]["time5NG"] = "0";
    inidata["recorder"]["time6NG"] = "0";
    inidata["recorder"]["time7NG"] = "0";
    inidata["recorder"]["time8NG"] = "0";
    inidata["recorder"]["time9NG"] = "0";
    inidata["recorder"]["time10NG"] = "0";
    inidata["recorder"]["time11NG"] = "0";
    inidata["recorder"]["time12NG"] = "0";
    inidata["recorder"]["time13NG"] = "0";
    inidata["recorder"]["time14NG"] = "0";
    inidata["recorder"]["time15NG"] = "0";
    inidata["recorder"]["time16NG"] = "0";
    inidata["recorder"]["time17NG"] = "0";
    inidata["recorder"]["time18NG"] = "0";
    inidata["recorder"]["time19NG"] = "0";
    inidata["recorder"]["time20NG"] = "0";
    inidata["recorder"]["time21NG"] = "0";
    inidata["recorder"]["time22NG"] = "0";
    inidata["recorder"]["time23NG"] = "0";
#endregion //
    glob_ini_instance.getInstance().write2Ini(inidata);
    chart_display(1);
}



private void textBox1_KeyDown(object sender, KeyEventArgs e)
{
    if (inidata["statu"]["plc_auto"] != null) return;
    if (e.KeyCode == Keys.Enter) {

                MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);
            
                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
        if (reg.Count>0 && inidata["setbarcode"]["barenable"] == "true")

          {
            // button2.PerformClick();
            testcase_lib.input_sn = this.textBox1.Text;
            textBox1.Enabled = false;
            this.timestart = 1;
            this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            this.textBox4.Text = "";
            this.textBox5.Text = "";
            label3.Text = "running test1";
            label3.BackColor = Color.GreenYellow;
          viewloader.table_load_into_viewer();
          backgroundWorker1.RunWorkerAsync();
        }
                // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
        else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
        {

           // MessageBox.Show("条码规则不对");
        }


    }
}

private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
{

}

private void 设置项目ToolStripMenuItem_Click(object sender, EventArgs e)
{
    parameterset setwin = new parameterset();
    setwin.Show(this);

}

private void textBox2_SizeChanged(object sender, EventArgs e)
{
    //this.textBox2.Top = this.rep1.Top + this.rep1.Height;
    //this.ResizeRedraw = true;
    //this.textBox2.Refresh();
}

private void timer1_Tick(object sender, EventArgs e)
{

    if (timestart == 1)
    {

        this.progressBar1.PerformStep();
        if (this.progressBar1.Value >= this.progressBar1.Maximum) this.progressBar1.Value = 0;

        TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
        TimeSpan ts3 = ts2.Subtract(ts1).Duration();
        this.textBox6.Text = ts3.ToString() + "";
    }
    else if (timestart == 2)
    {


     this.progressBar1.Value = this.progressBar1.Maximum;

    }



}

private void 英语ToolStripMenuItem_Click(object sender, EventArgs e)
{
    this.语言配置ToolStripMenuItem.Text = "setup_language";
    this.英语ToolStripMenuItem.Text = "SetTOEnglish ";
    this.汉语ToolStripMenuItem.Text = "SetToChinese";
    this.toolStripMenuItem1.Text = "SetUp";
    this.修改配置ToolStripMenuItem.Text = "Modify_Test_parameters";
    this.修改后重新加载ToolStripMenuItem.Text = "After_Modify_Reload";
    this.清理白板数据ToolStripMenuItem.Text = "Clear_TheDayShift_TestData";
    this.请夜班数据ToolStripMenuItem.Text = "Clear_TheNightShift_TestData";
    this.同时清除白夜班数据ToolStripMenuItem.Text = "Clear_AllShift_TestData";
    this.series.LegendText = "passed per hour";
    this.series2.LegendText = "fail per hour";
    this.label2.Text = "please scan barcode :";
    this.设置项目ToolStripMenuItem.Text = "OtherSetUp";
    this.打开校验程序表ToolStripMenuItem.Text = "load calibration table";
    this.重新加载测试表ToolStripMenuItem.Text = "after reload test table";
    this.调试DEBUGToolStripMenuItem.Text = "relay debug";
            this.productionInfoToolStripMenuItem.Text = "production_info_set";
            inidata["language"]["english"] = "1";

    glob_ini_instance.getInstance().write2Ini(inidata);
}

private void 汉语ToolStripMenuItem_Click(object sender, EventArgs e)
{
    this.语言配置ToolStripMenuItem.Text = "语言设置";
    this.英语ToolStripMenuItem.Text = "设置到英语 ";
    this.汉语ToolStripMenuItem.Text = "汉语";
    this.toolStripMenuItem1.Text = "设置";
    this.设置项目ToolStripMenuItem.Text = "设置项目";
    this.修改配置ToolStripMenuItem.Text = "修改配置";
    this.修改后重新加载ToolStripMenuItem.Text = "修改后重新加载";
    this.清理白板数据ToolStripMenuItem.Text = "清理白板数据";
    this.请夜班数据ToolStripMenuItem.Text = "请夜班数据";
    this.同时清除白夜班数据ToolStripMenuItem.Text = "同时清除白夜班数据";
    this.series.LegendText = "每小时PASS数";
    this.series2.LegendText = "每小时NG数";
    this.label2.Text = "条码扫入：";
    this.打开校验程序表ToolStripMenuItem.Text = "打开校验程序表";
    this.重新加载测试表ToolStripMenuItem.Text = "重新加载测试表";
   this.调试DEBUGToolStripMenuItem.Text = "调试debug";
    this.productionInfoToolStripMenuItem.Text = "生产信息";
    inidata["language"]["english"] = "0";
    glob_ini_instance.getInstance().write2Ini(inidata);

}

        private void 打开校验程序表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablestaues = 1;
  

        }

        private void 重新加载测试表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablestaues = 0;
        

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 调试DEBUGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Relay_debug form3 = new Relay_debug();

            //form3.Show();
            relay_debug_4 form4 = relay_debug_4.get_instance();
            form4.Show();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void debugmyrelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myrelaysetter  form4= new myrelaysetter();
            form4.Show();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
            this.button2.Enabled = false;
            this.textBox1.Enabled = false;
            this.Text = inidata["setproduct"]["name"];
            ptrWnd = FindWindow(null, this.Text);
            testcase_lib = new testcase_dll();
            testcase_lib.ptrWnd = ptrWnd;
            RFExplorer.ptrWnd = ptrWnd;
            babyhappy_com.ptrWnd = ptrWnd;
            serial_blue_dongle_tools.ptrWnd = ptrWnd;
            Tek_TPS2040_Oscilloscopes.ptrWnd = ptrWnd;
            chroma_dc_elecronic_63200_load.ptrWnd = ptrWnd;
            Tek_USB_2012.ptrWnd = ptrWnd;
            comlineforingo_led.ptrWnd = ptrWnd;
            victor_4090C.ptrWnd = ptrWnd;
            utility_func.ptrWnd= ptrWnd;
            plc_xx_com.ptrWnd = ptrWnd;
           //  timer2.Enabled = true;
            viewloader = new p_reogridviewloader(ref this.reoGridControl1, testcase_lib.Getfun(), mu);

            if (inidata["setproduct"]["line_number"] != null && inidata["setproduct"]["personal_number"] != null)
            {
                this.toolStripTextBox1.Text = inidata["setproduct"]["personal_number"];
                


            }

            if (inidata["setproduct"]["hbi_region"] != null)
            {
                this.toolStripStatusLabel2.ForeColor = Color.Blue;
                this.toolStripStatusLabel2.Text = "HBI Regin: [" + inidata["setproduct"]["hbi_region"] + "]";
            }
#if wiring_harness
            if (inidata["wiring_harness"] != null) {


                if (inidata["wiring_harness"]["cable_test_production_line"] != null) {

                    this.toolStripStatusLabel3.ForeColor = Color.Blue;
                    this.toolStripStatusLabel3.Text = $"LineNo: [{inidata["wiring_harness"]["cable_test_production_line"]}]";

                }

                if (inidata["wiring_harness"]["cable_test_vendor_code"] != null)
                {

                    this.toolStripStatusLabel4.ForeColor = Color.Blue;
                    this.toolStripStatusLabel4.Text = $"VendorNo: [{inidata["wiring_harness"]["cable_test_vendor_code"]}]";

                }


                if (inidata["wiring_harness"]["cable_test_order_part_number"] != null)
                {

                    this.toolStripStatusLabel5.ForeColor = Color.Blue;
                    this.toolStripStatusLabel5.Text = $"PN: [{inidata["wiring_harness"]["cable_test_order_part_number"]}]";

                }

                

           if (inidata["wiring_harness"]["cable_test_meritor_Drawing_Revision"] != null)
                {

                    this.toolStripStatusLabel2.ForeColor = Color.Blue;
                    this.toolStripStatusLabel2.Text = $"DarwingRev: [{inidata["wiring_harness"]["cable_test_meritor_Drawing_Revision"].PadLeft(5, '-')}]";

                }





                //  MessageBox.Show($"{(DateTime.Now-new DateTime(DateTime.Now.Year,1,1)).Days+1}");

                // MessageBox.Show($"{(DateTime.Now.DayOfYear)}");
            }


#endif 

            //mu = json2tester_standard.red_json_test_project("project_tester_name.json");


                viewloader.table_load_into_viewer();
            for (int i = 0; i < viewloader.tester_proj.test_cases.Count; i++)
            {

                viewloader.tester_proj[i].tf_handler = update_dataview;
            }

            if (inidata["setbarcode"]["barenable"] == "true")
            {
                this.textBox1.Enabled = true;
                button2.Enabled = false;
                textBox1.Focus();
            }
            else
            {

                this.button2.Enabled = true;
                this.textBox1.Enabled = false;
                this.button2.Focus();
            }
            if (inidata["setproduct"]["ifautotest"] != null) {

                this.timer3.Enabled = true;
            }
 
        }

  
        private void timer2_Tick(object sender, EventArgs e)
        {

            //return;
            string res = "";
            if (testcase_lib.Getfun()["get_ingoproj_led_for_auto_det"]("", "", out res) == "pass" && File.Exists("ng_flog.txt"))
            {
              //  if (!File.Exists("ng_flog.txt")) return;
                if (inidata["setbarcode"]["barenable"] != "true")
                {
                    this.button2.Enabled = false;
                   
                }
                else
                {

                    textBox1.Enabled = false;

                }
            }
            else {
               if (File.Exists("ng_flog.txt")) { 
                new Task(() =>
                {
                    if (test_running == 0)
                    {
                        try
                        {
                            File.Delete("ng_flog.txt");
                        }
                        catch(Exception ee)
                        {

                        }

                    }

                }).Start();
                }

                if (inidata["setbarcode"]["barenable"] != "true")
                    {
                        this.button2.Enabled = true;

                    }
                    else
                    {

                        textBox1.Enabled = true;

                    }



                

            }


        }
        
        //用于自动化检测用
        //*
        // * 
        // * 
        // * 
        //

        private void timer3_Tick(object sender, EventArgs e)
        {

            //if(test_running==0){ 

            //   this.textBox1.Text = "123456789123";
            //    this.textBox1.Focus();
            //    SendKeys.SendWait("{ENTER}");
            //}

            if (testcase_lib.get_ingo_auto_test_pin() == 1)
            {
                auto_status2 = 0;
                if (auto_status1++ > 10) {
                    auto_status1 = 0;
                    if (test_running_flog1 == 0 && auto_status3==0) {
                        auto_status3 = 1;
                        this.Focus();
                      //  testcase_lib.get_barcode_str_to_input();
                        SendKeys.SendWait("{ENTER}");
                    }

                };


            }
            else {

                auto_status1 = 0;
            }


            if (testcase_lib.get_ingo_auto_test_pin() == 0)
            {

                if (auto_status2++ > 30)
                {
                    auto_status2 = 0;
                    if (auto_status1 == 0 && auto_status3 == 1)
                    {
                        auto_status3 = 0;
                        this.Focus();
                    }

                };


            }
            else
            {

                
            }





        }

        private void skrelaydebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
           sk_relay32 form4 = sk_relay32.get_instaance();
            form4.Show();
        }

        private void cancelTESTToolStripMenuItem_Click(object sender, EventArgs e)
        {

     

                backgroundWorker1.CancelAsync();

          
            
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            


                if (inidata["setproduct"]["line_number"] != null && inidata["setproduct"]["personal_number"] != null)
            {
                 inidata["setproduct"]["personal_number"]= this.toolStripTextBox1.Text;
                glob_ini_instance.getInstance().write2Ini(inidata);


            }
        }

        public void chart_display(int bc ) {


    foreach (var series in chart1.Series)
    {
        series.Points.Clear();
    }



        // 在chart中显示数据
        int x = 0;

        series.LegendText = "Number of pass per hour";
        values[0] = int.Parse(inidata["recorder"]["time8OK"]);
        values[1] = int.Parse(inidata["recorder"]["time9OK"]);
        values[2] = int.Parse(inidata["recorder"]["time10OK"]);
        values[3] = int.Parse(inidata["recorder"]["time11OK"]);
        values[4] = int.Parse(inidata["recorder"]["time12OK"]);
        values[5] = int.Parse(inidata["recorder"]["time13OK"]);
        values[6] = int.Parse(inidata["recorder"]["time14OK"]);
        values[7] = int.Parse(inidata["recorder"]["time15OK"]);
        values[8] = int.Parse(inidata["recorder"]["time16OK"]);
        values[9] = int.Parse(inidata["recorder"]["time17OK"]);
        values[10] = int.Parse(inidata["recorder"]["time18OK"]);
        values[11] = int.Parse(inidata["recorder"]["time19OK"]);
        values[12] = int.Parse(inidata["recorder"]["time20OK"]);
        values[13] = int.Parse(inidata["recorder"]["time21OK"]);
        values[14] = int.Parse(inidata["recorder"]["time22OK"]);
        values[15] = int.Parse(inidata["recorder"]["time23OK"]);
        values[16] = int.Parse(inidata["recorder"]["time0OK"]);
        values[17] = int.Parse(inidata["recorder"]["time1OK"]);
        values[18] = int.Parse(inidata["recorder"]["time2OK"]);
        values[19] = int.Parse(inidata["recorder"]["time3OK"]);
        values[20] = int.Parse(inidata["recorder"]["time4OK"]);
        values[21] = int.Parse(inidata["recorder"]["time5OK"]);
        values[22] = int.Parse(inidata["recorder"]["time6OK"]);
        values[23] = int.Parse(inidata["recorder"]["time7OK"]);




    foreach (float v in values)
        {
            series.Points.AddXY(x, v);
            x++;
        }



    foreach (var series in chart2.Series)
        {
            series.Points.Clear();
        }


        int x2 = 0;
        series2.LegendText = "Number of fail per hour";
        values2[0] = int.Parse(inidata["recorder"]["time8NG"]);
        values2[1] = int.Parse(inidata["recorder"]["time9NG"]);
        values2[2] = int.Parse(inidata["recorder"]["time10NG"]);
        values2[3] = int.Parse(inidata["recorder"]["time11NG"]);
        values2[4] = int.Parse(inidata["recorder"]["time12NG"]);
        values2[5] = int.Parse(inidata["recorder"]["time13NG"]);
        values2[6] = int.Parse(inidata["recorder"]["time14NG"]);
        values2[7] = int.Parse(inidata["recorder"]["time15NG"]);
        values2[8] = int.Parse(inidata["recorder"]["time16NG"]);
        values2[9] = int.Parse(inidata["recorder"]["time17NG"]);
        values2[10] = int.Parse(inidata["recorder"]["time18NG"]);
        values2[11] = int.Parse(inidata["recorder"]["time19NG"]);
        values2[12] = int.Parse(inidata["recorder"]["time20NG"]);
        values2[13] = int.Parse(inidata["recorder"]["time21NG"]);
        values2[14] = int.Parse(inidata["recorder"]["time22NG"]);
        values2[15] = int.Parse(inidata["recorder"]["time23NG"]);
        values2[16] = int.Parse(inidata["recorder"]["time0NG"]);
        values2[17] = int.Parse(inidata["recorder"]["time1NG"]);
        values2[18] = int.Parse(inidata["recorder"]["time2NG"]);
        values2[19] = int.Parse(inidata["recorder"]["time3NG"]);
        values2[20] = int.Parse(inidata["recorder"]["time4NG"]);
        values2[21] = int.Parse(inidata["recorder"]["time5NG"]);
        values2[22] = int.Parse(inidata["recorder"]["time6NG"]);
        values2[23] = int.Parse(inidata["recorder"]["time7NG"]);

        foreach (float v in values2)
        {
            series2.Points.AddXY(x2, v);
            x2++;
        }

}










    }













}





