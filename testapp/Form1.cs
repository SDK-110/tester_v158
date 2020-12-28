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


/*         try { ((System.ComponentModel.ISupportInitialize)(this.rep1)).EndInit(); }
            catch (Exception) { MessageBox.Show("你没有注册reportX,请注册"); };
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    */









namespace testapp
{
    public partial class Form1 : Form
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
        public const int WM_SENDA_2 = USER + 108;
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;
        IntPtr ptrWnd;


        #endregion
        /*--------------message loop dll upload-------------*/
        int timestart = 0;
TimeSpan ts1;
int i = 2;
volatile int tablestaues = 0;
Series series, series2;
float[] values = new float[24];
float[] values2 = new float[24];
Dictionary<int, string> result_temp = new Dictionary<int, string>();
static  public testcase_dll m;

private IniParser.FileIniDataParser iniread = new FileIniDataParser();
IniParser.Model.IniData dt ;
int p = 0;

public Form1()
{

    //try
    //{
        m = new testcase_dll();
    //}
    //catch (Exception)
    //{

    //    MessageBox.Show("你的设备资源被霸占请检查");

    //    this.Close();

    //}
    rep1ini();
    InitializeComponent();
    dt = iniread.ReadFile("setup.ini");
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
            ptrWnd = FindWindow(null, this.Text);
            m.ptrWnd = ptrWnd;
           
        }

        #region  /*-------------LOOP FUNCTION BACKPROC-----------*/
        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        protected override void DefWndProc(ref Message ms)
        {

            if (ms.Msg == WM_SENDA)
            {
                string tmp = "";
                if (iniread.ReadFile("setup.ini")["setport"]["Relay_board"] != null) { 

                    m.Getfun()["relay_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
               }

                //  MessageBox.Show(Marshal.PtrToStringAnsi(ms.LParam));
                //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);

                // dt["cmw100ParameterSet"]["buletoothloss"] = Marshal.PtrToStringAnsi(m.LParam);
                //   iniread.WriteFile("setup.ini", dt);


            }
            if (ms.Msg == WM_SENDA_2) {

                string tmp = "";
                if (iniread.ReadFile("setup.ini")["setport"]["Relay_board2"] != null)
                {

                    m.Getfun()["relay2_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                }



            }

            if (ms.Msg == WM_SENDB)
            {
                //MessageBox.Show(Marshal.PtrToStringAnsi(m.LParam));
                //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);
                // dt["cmw100ParameterSet"]["cc1310loss"] = Marshal.PtrToStringAnsi(m.LParam);
                // iniread.WriteFile("setup.ini", dt);

                this.richTextBox1.AppendText(Marshal.PtrToStringAnsi(ms.LParam) + "\n" );
                


            }
            if (ms.Msg == WM_SENDD) {
                this.Close();
            }

            if (ms.Msg == WM_SENDC) {
              string [] retdate =   Marshal.PtrToStringAnsi(ms.LParam).Split(";".ToArray());
                dt["cmw100statuscheck"]["statusyear"]=retdate[0];
                dt["cmw100statuscheck"]["statusmonth"]= retdate[1];
                dt["cmw100statuscheck"]["statusday"]= retdate[2];
                dt["cmw100statuscheck"]["statushour"]= retdate[3];
                iniread.WriteFile("setup.ini", dt);


            }
            if (ms.Msg == WM_SEND_SET_CC1310LOSS) {

                dt["cmw100ParameterSet"]["cc1310loss"] = Marshal.PtrToStringAnsi(ms.LParam);
                iniread.WriteFile("setup.ini", dt);

            }
            if (ms.Msg == WM_SEND_SET_BTLOSS)
            {

                dt["cmw100ParameterSet"]["buletoothpathloss"] = Marshal.PtrToStringAnsi(ms.LParam);
                iniread.WriteFile("setup.ini", dt);

            }
            if (ms.Msg == WM_SEND_SET_WIFILOSS)
            {
                dt["cmw100ParameterSet"]["wifipathloss"] = Marshal.PtrToStringAnsi(ms.LParam);
                iniread.WriteFile("setup.ini", dt);


            }

            if (ms.Msg == WM_SEND_AUTOTEST) {/*自動測試消息*/

                if (backgroundWorker1.IsBusy) return;
                if (dt["setproduct"]["ifautotest"].Trim() == "false") return;
                if (dt["setbarcode"]["barenable"] == "true")
                {

                    MatchCollection reg = new Regex(dt["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                    //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                    //  return;
                    // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                    if (reg.Count > 0 && dt["setbarcode"]["barenable"] == "true")

                    {
                        // button2.PerformClick();
                        m.trf = this.textBox1.Text;
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
                    else if (!(reg.Count > 0 && dt["setbarcode"]["barenable"] == "true"))
                    {

                        // MessageBox.Show("条码规则不对");
                    }

                }
                else {

                    button2.PerformClick();
                }



                }





            base.DefWndProc(ref ms);
        }
        #endregion
        /*-------------LOOP FUNCTION BACKPROC-----------*/

        private void Form1_Load(object sender, EventArgs e)
{


    // rep1.ImportExcelOle(@"C:\Users\Administrator\Desktop\123.xls",1,1,8,200,true);
    rep1.OpenReport(@"testcasetable");
    if (!File.Exists("result.csv")) {
        string m,v="time,serialno,result";

        i = 2;
        do {
            m = rep1.GetCellValue(2, i);
            if (m == null) break;
        v = v  +  "," +  m + "( " +  rep1.GetCellValue(6, i).Replace(",","#") + "<-->" + rep1.GetCellValue(5, i).Replace(",", "#") + ")";
            i++;

        } while (m!=null&&m.Trim().Length>2);

        File.AppendAllText("result.csv", v + '\n');
    }

    rep1.SetFrozenRow(1, 1);
    rep1.CellReadOnly = true;
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

    this.textBox2.Left = 5;
    this.textBox2.Top = this.Bottom-this.Height;

    chart_display(1);
    // this.WindowState = FormWindowState.Maximized;
    rep1.RowCount = 300;
    rep1.ColCount = 10;
    rep1.PoleHeight = 0;
    rep1.PoleWidth = 0;
    int temp = rep1.Width;
    rep1.SetColWidth(1, (int)(temp * 0.1));
    rep1.SetColWidth(2, (int)(temp * 0.3));
    rep1.SetColWidth(3, (int)(temp * 0.1));
    rep1.SetColWidth(4, (int)(temp * 0.1));
    rep1.SetColWidth(5, (int)(temp * 0.1));
    rep1.SetColWidth(6, (int)(temp * 0.1));
    rep1.SetColWidth(7, (int)(temp * 0.1));
    rep1.SetColWidth(8, (int)(temp * 0.1));
    rep1.SetColHide(3, true);
    rep1.SetColHide(9, true);
    rep1.SetColHide(10, true);
    rep1.CellReadOnly = true;
    label1.Left = rep1.Right;
    label1.Top = chart2.Bottom;
    button2.Left = rep1.Right;
    label2.Top = button2.Bottom + 30;
    label3.Left = this.Right-label3.Width;
    this.groupBox1.Left = rep1.Right;
    textBox1.Top = label2.Bottom + 20;
    this.groupBox1.Top = this.rep1.Bottom  - this.groupBox1.Height;
    this.richTextBox1.Left = this.rep1.Right;
            this.richTextBox1.Top = this.textBox1.Bottom;
    this.label1.Text = string.Format("TOTAL：{0}PCS |NG :{1}|OK:{2}", dt["recorder"]["title"], dt["recorder"]["titleng"], dt["recorder"]["titleok"]);

    p = 0;
    ChartArea chartArea = chart1.ChartAreas[0];
    ChartArea chartArea2 = chart2.ChartAreas[0];

    foreach (int i in  new int [] { 8, 9, 10,11,12,13,14,15,16,17,18,19,20,21,22,23,1,2,3,4,5,6,7 }){ 
    CustomLabel label = new CustomLabel();
    label.Text = i+"";
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


    if (dt["language"]["english"] == "1")
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







            }




    this.WindowState = FormWindowState.Maximized;

}

private void Form1_SizeChanged(object sender, EventArgs e)
{

    rep1.Left = 5;
    this.textBox2.Height = (int)(this.Height * 0.2);
    this.textBox2.Top= this.Height- (int)(this.Height * 0.2) +30;
    this.textBox2.Width = this.Width;


    rep1.Width = this.Width / 2;
    rep1.Height = (int)(this.Height * 0.8);
    chart1.Width = (int)(this.Width / 2);
    chart1.Top = rep1.Top;
    chart1.Left = rep1.Right;
    chart2.Width = (int)(this.Width / 2);
    chart2.Left = rep1.Right;
    chart2.Top = chart1.Bottom;

    button2.Left = rep1.Right;
    button2.Top = label1.Bottom;
    this.groupBox1.Left = rep1.Right;
    this.richTextBox1.Left = this.rep1.Right;
    int temp = rep1.Width;
    rep1.SetColWidth(1, (int)(temp * 0.1));
    rep1.SetColWidth(2, (int)(temp * 0.36));
    rep1.SetColWidth(3, (int)(temp * 0.1));
    rep1.SetColWidth(4, (int)(temp * 0.1));
    rep1.SetColWidth(5, (int)(temp * 0.1));
    rep1.SetColWidth(6, (int)(temp * 0.1));
    rep1.SetColWidth(7, (int)(temp * 0.1));
    rep1.SetColWidth(8, (int)(temp * 0.12));
    rep1.SetColHide(9, true);
    rep1.SetColHide(3, true);
    rep1.SetColHide(10, true);
    label1.Left = rep1.Right;
    label1.Top = chart2.Bottom;
    this.textBox1.Left = rep1.Right;
    rep1.CellReadOnly = true;
    button2.Left = rep1.Right;
    label2.Top = button2.Bottom + 30;
    label2.Left = rep1.Right;
    textBox1.Top = label2.Bottom + 20;
    label3.Left = this.Right - label3.Width;
    this.groupBox1.Top = this.rep1.Bottom - this.groupBox1.Height;
    this.ResizeRedraw = true;
    this.Refresh();

}

private void button2_Click(object sender, EventArgs e)
{
            // rep1.ClearCell(7, 2, 7, 300);
            // rep1.ClearCell(8, 2, 8, 300);
//SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            this.timestart = 1;
    this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
    ts1 = new TimeSpan(DateTime.Now.Ticks);
    this.textBox4.Text = "";
    this.textBox5.Text = "";
    label3.Text = "running test1";
    label3.BackColor = Color.GreenYellow;
    button2.Enabled = false;
    backgroundWorker1.RunWorkerAsync();




}

private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
{
    string stemp = "";

    this.Invoke(new Action(() =>
    {
        this.richTextBox1.Text = "";
        this.textBox2.Text="";
    }));

    rep1.TopRow = 1;
    i = 2;
    result_temp.Clear();
    rep1.ClearCell(7, 2, 7, 300);
    rep1.ClearCell(8, 2, 8, 300);
    try
    {
        string globe_result= "pass";
       string z ="";
        while (rep1.GetCellValue(9, i) != null)
        {
                    if (backgroundWorker1.CancellationPending == true) break;

            if (rep1.GetCellValue(3, i) == "1" )
            {
                string g = rep1.GetCellValue(9, i).Trim();
                string otherp = rep1.GetCellValue(10, i);
                string result = "";
                string jud_result = m.Getfun()[g](rep1.GetCellValue(5, i), rep1.GetCellValue(6, i),out result,(otherp=="")?"null":otherp);
                z = result.Trim();
                backgroundWorker1.ReportProgress(i,jud_result);
                rep1.SetCellValue(7,i,result.Trim());
                if (jud_result == "fail" || backgroundWorker1.CancellationPending == true)
                {
                    globe_result = "fail";
                    if (iniread.ReadFile("setup.ini")["statu"]["NG_RUN"]!= "yes") break;

                }

            }
            else
            {

                z = "skip";
            }

            result_temp.Add(i - 1,z);

            this.Invoke(new Action(() =>
            {

                this.textBox2.AppendText("debug : --> setp" + (i-1)  + ":-->"+ z + "\r\n"); 
            }));

            i++;
        }

        e.Result = globe_result;
    }
    catch (Exception m) {


        MessageBox.Show(m.ToString());

    }
            if (dt["setport"]["Relay_board"] != null)
            {
                m.Getfun()["testsysini"]("pass", "pass", out stemp, "00;00");
            }


}

private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
{
    try
    {
        int i = e.ProgressPercentage;
        #region //选择框
        rep1.SetSelectCell(1, i, 6, i);
        if (i > 11) rep1.TopRow = i - 11;
        #endregion
        string resu = (string)e.UserState;
        rep1.SetCellValue(8, i,resu );
        if (resu == "pass") rep1.SetCellBackColor(8, i, 0x00FF00);
        if(resu=="fail") rep1.SetCellBackColor(8, i, 0x0000ff);

        label3.Text = "running" + $"{ i.ToString()}";

    }
    catch (Exception ) { }


  //  MessageBox.Show(a + "");

}

private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
{
    // MessageBox.Show((string)e.Result);
   
    StringBuilder s = new StringBuilder();
            
                s.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ",");
                s.Append(((this.textBox1.Text == "") ? "skip" : this.textBox1.Text) + ",");
                s.Append((string)e.Result + ",");
                for (int a = 0; a < result_temp.Count; a++)
                {




                    s.Append(result_temp[result_temp.Keys.ToArray()[a]] + ",");

                }
       
            s.Remove(s.Length - 1, 1);
                s.AppendLine();
            if (tablestaues == 0)
            {
                File.AppendAllText("result.csv", s.ToString());

            }
            else {


                File.AppendAllText("cmw100RF_cab_log.csv", s.ToString());

            }
                if (dt["setbarcode"]["barenable"] != "true")
                {
                    this.button2.Enabled = true;
                    this.button2.Focus();
                }
                else
                {

                    textBox1.Enabled = true;

                // this.textBox1.SelectAll();

                this.textBox1.Text = "";
                    this.textBox1.Focus();
            }
                if ((string)e.Result == "pass")
                {

                if (dt["setport"]["shieldboxport"] != null)
                {
                    string c = "";
                    m.Getfun()["shieldboxopen"]("", "", out c);
                }

                count(1);
                    dt["recorder"]["titleok"] = (int.Parse(dt["recorder"]["titleok"]) + 1).ToString();
                    iniread.WriteFile("setup.ini", dt);
                    label3.Text = "Test PASS";
                    label3.BackColor = Color.Green;
                }
                if ((string)e.Result == "fail")
                {
                    label3.Text = "Test Fail";
                    label3.BackColor = Color.Red;
                    count(0);
                    dt["recorder"]["titleng"] = (int.Parse(dt["recorder"]["titleng"]) + 1).ToString();
                    iniread.WriteFile("setup.ini", dt);
                }
                chart_display(1);

                dt["recorder"]["title"] = (int.Parse(dt["recorder"]["title"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);

                this.label1.Text = string.Format("total：{0}PCS |NG :{1}|OK:{2}", dt["recorder"]["title"], dt["recorder"]["titleng"], dt["recorder"]["titleok"]);

                this.timestart = 2;
                this.textBox4.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + "";


                TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
                TimeSpan ts3 = ts2.Subtract(ts1).Duration();
                this.textBox5.Text = ts3.ToString() + "";
           
}

private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    try
    {
        string c = "pass";

        
        backgroundWorker1.CancelAsync();
        backgroundWorker1.Dispose();

       m.Getfun()["releaseport"]("pass", "pass", out c, "");
  }
   catch (Exception ex) {/* MessageBox.Show(ex.StackTrace + ex.ToString());*/}



}



private void 修改配置ToolStripMenuItem_Click(object sender, EventArgs e)
{
    new Form2().Show();
}

private void 修改后重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
{
            tablestaues = 0;
            rep1.OpenReport(@"testcasetable");
    rep1.CellReadOnly = true;
    rep1.SetFrozenRow(1, 1);
    rep1.RowCount = 300;
    rep1.ColCount = 10;
    rep1.PoleHeight = 0;
    rep1.PoleWidth = 0;
    int temp = rep1.Width;
    rep1.SetColWidth(1, (int)(temp * 0.1));
    rep1.SetColWidth(2, (int)(temp * 0.4));
    rep1.SetColWidth(3, (int)(temp * 0.1));
    rep1.SetColWidth(4, (int)(temp * 0.1));
    rep1.SetColWidth(5, (int)(temp * 0.1));
    rep1.SetColWidth(6, (int)(temp * 0.1));
    rep1.SetColWidth(7, (int)(temp * 0.1));
    rep1.SetColWidth(8, (int)(temp * 0.12));
    rep1.SetColHide(9, true);
    rep1.SetColHide(3, true);
    rep1.SetColHide(10, true);
}

private void Form1_KeyDown(object sender, KeyEventArgs e)
{

    if (e.KeyData == Keys.Space) {

                if (dt["setbarcode"]["barenable"] == "true")
                {

                    MatchCollection reg = new Regex(dt["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                    //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                    //  return;
                    // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                        if (reg.Count == 0) return;
                        m.trf = this.textBox1.Text;
                        textBox1.Enabled = false;
                        this.timestart = 1;
                        this.textBox3.Text = DateTime.Now.ToString("HH:mm:ss:ffff") + ""; ;
                        ts1 = new TimeSpan(DateTime.Now.Ticks);
                        this.textBox4.Text = "";
                        this.textBox5.Text = "";
                        label3.Text = "running test1";
                        label3.BackColor = Color.GreenYellow;
                        backgroundWorker1.RunWorkerAsync();
                        textBox1.Focus();
                }
                else
                {


                    button2.PerformClick();
                    this.button2.Focus();
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
                dt["recorder"]["time0OK"] = (int.Parse(dt["recorder"]["time0OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else {
                dt["recorder"]["time0NG"] = (int.Parse(dt["recorder"]["time0NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }

            break;

        case 1:
            if (rs == 1)
            {
                dt["recorder"]["time1OK"] = (int.Parse(dt["recorder"]["time1OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time1NG"] = (int.Parse(dt["recorder"]["time1NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 2:
            if (rs == 1)
            {
                dt["recorder"]["time2OK"] = (int.Parse(dt["recorder"]["time2OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time2NG"] = (int.Parse(dt["recorder"]["time2NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 3:
            if (rs == 1)
            {
                dt["recorder"]["time3OK"] = (int.Parse(dt["recorder"]["time3OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time3NG"] = (int.Parse(dt["recorder"]["time3NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 4:
            if (rs == 1)
            {
                dt["recorder"]["time4OK"] = (int.Parse(dt["recorder"]["time4OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time4NG"] = (int.Parse(dt["recorder"]["time4NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 5:
            if (rs == 1)
            {
                dt["recorder"]["time5OK"] = (int.Parse(dt["recorder"]["time5OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time5NG"] = (int.Parse(dt["recorder"]["time5NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;

        case 6:
            if (rs == 1)
            {
                dt["recorder"]["time6OK"] = (int.Parse(dt["recorder"]["time6OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time6NG"] = (int.Parse(dt["recorder"]["time6NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 7:
            if (rs == 1)
            {
                dt["recorder"]["time7OK"] = (int.Parse(dt["recorder"]["time7OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time7NG"] = (int.Parse(dt["recorder"]["time7NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;

        case 8:
            if (rs == 1)
            {
                dt["recorder"]["time8OK"] = (int.Parse(dt["recorder"]["time8OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time8NG"] = (int.Parse(dt["recorder"]["time8NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 9:
            if (rs == 1)
            {
                dt["recorder"]["time9OK"] = (int.Parse(dt["recorder"]["time9OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time9NG"] = (int.Parse(dt["recorder"]["time9NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 10:
            if (rs == 1)
            {
                dt["recorder"]["time10OK"] = (int.Parse(dt["recorder"]["time10OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time10NG"] = (int.Parse(dt["recorder"]["time10NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 11:
            if (rs == 1)
            {
                dt["recorder"]["time11OK"] = (int.Parse(dt["recorder"]["time11OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time11NG"] = (int.Parse(dt["recorder"]["time11NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 12:
            if (rs == 1)
            {
                dt["recorder"]["time12OK"] = (int.Parse(dt["recorder"]["time12OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time12NG"] = (int.Parse(dt["recorder"]["time12NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 13:
            if (rs == 1)
            {
                dt["recorder"]["time13OK"] = (int.Parse(dt["recorder"]["time13OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time13NG"] = (int.Parse(dt["recorder"]["time13NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 14:
            if (rs == 1)
            {

                dt["recorder"]["time14OK"] = (int.Parse(dt["recorder"]["time14OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time14NG"] = (int.Parse(dt["recorder"]["time14NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 15:
            if (rs == 1)
            {
                dt["recorder"]["time15OK"] = (int.Parse(dt["recorder"]["time15OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time15NG"] = (int.Parse(dt["recorder"]["time15NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;

        case 16:
            if (rs == 1)
            {
                dt["recorder"]["time16OK"] = (int.Parse(dt["recorder"]["time16OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time16NG"] = (int.Parse(dt["recorder"]["time16NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 17:
            if (rs == 1)
            {
                dt["recorder"]["time17OK"] = (int.Parse(dt["recorder"]["time17OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time17NG"] = (int.Parse(dt["recorder"]["time17NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 18:
            if (rs == 1)
            {
                dt["recorder"]["time18OK"] = (int.Parse(dt["recorder"]["time18OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time18NG"] = (int.Parse(dt["recorder"]["time18NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 19:
            if (rs == 1)
            {
                dt["recorder"]["time19OK"] = (int.Parse(dt["recorder"]["time19OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time19NG"] = (int.Parse(dt["recorder"]["time19NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;

        case 20:
            if (rs == 1)
            {
                dt["recorder"]["time20OK"] = (int.Parse(dt["recorder"]["time20OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time20NG"] = (int.Parse(dt["recorder"]["time20NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 21:
            if (rs == 1)
            {
                dt["recorder"]["time21OK"] = (int.Parse(dt["recorder"]["time21OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time21NG"] = (int.Parse(dt["recorder"]["time21NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 22:
            if (rs == 1)
            {
                dt["recorder"]["time22OK"] = (int.Parse(dt["recorder"]["time22OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time22NG"] = (int.Parse(dt["recorder"]["time22NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;
        case 23:
            if (rs == 1)
            {
                dt["recorder"]["time23OK"] = (int.Parse(dt["recorder"]["time23OK"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            else
            {
                dt["recorder"]["time23NG"] = (int.Parse(dt["recorder"]["time23NG"]) + 1).ToString();
                iniread.WriteFile("setup.ini", dt);
            }
            break;


        default:
            break;


    }
    #endregion

}

private void 清理白板数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
    dt["recorder"]["time8NG"] = "0";
    dt["recorder"]["time8OK"] = "0";
    dt["recorder"]["time9NG"] = "0";
    dt["recorder"]["time9OK"] = "0";
    dt["recorder"]["time10NG"] = "0";
    dt["recorder"]["time10OK"] = "0";
    dt["recorder"]["time11NG"] = "0";
    dt["recorder"]["time11OK"] = "0";
    dt["recorder"]["time12NG"] = "0";
    dt["recorder"]["time12OK"] = "0";
    dt["recorder"]["time13NG"] = "0";
    dt["recorder"]["time13OK"] = "0";
    dt["recorder"]["time14NG"] = "0";
    dt["recorder"]["time14OK"] = "0";
    dt["recorder"]["time15NG"] = "0";
    dt["recorder"]["time15OK"] = "0";
    dt["recorder"]["time16NG"] = "0";
    dt["recorder"]["time16OK"] = "0";
    dt["recorder"]["time17NG"] = "0";
    dt["recorder"]["time17OK"] = "0";
    dt["recorder"]["time18NG"] = "0";
    dt["recorder"]["time18OK"] = "0";
    dt["recorder"]["time19NG"] = "0";
    dt["recorder"]["time19OK"] = "0";
    iniread.WriteFile("setup.ini", dt);
    chart_display(1);

}

private void 请夜班数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
    dt["recorder"]["time20NG"] = "0";
    dt["recorder"]["time20OK"] = "0";
    dt["recorder"]["time21NG"] = "0";
    dt["recorder"]["time21OK"] = "0";
    dt["recorder"]["time22NG"] = "0";
    dt["recorder"]["time22OK"] = "0";
    dt["recorder"]["time23NG"] = "0";
    dt["recorder"]["time23OK"] = "0";
    dt["recorder"]["time0NG"] = "0";
    dt["recorder"]["time0OK"] = "0";
    dt["recorder"]["time1NG"] = "0";
    dt["recorder"]["time1OK"] = "0";
    dt["recorder"]["time2NG"] = "0";
    dt["recorder"]["time2OK"] = "0";
    dt["recorder"]["time3NG"] = "0";
    dt["recorder"]["time3OK"] = "0";
    dt["recorder"]["time4NG"] = "0";
    dt["recorder"]["time4OK"] = "0";
    dt["recorder"]["time5NG"] = "0";
    dt["recorder"]["time5OK"] = "0";
    dt["recorder"]["time6NG"] = "0";
    dt["recorder"]["time6OK"] = "0";
    dt["recorder"]["time7NG"] = "0";
    dt["recorder"]["time7OK"] = "0";
    iniread.WriteFile("setup.ini", dt);
    chart_display(1);
}

private void 同时清除白夜班数据ToolStripMenuItem_Click(object sender, EventArgs e)
{
    #region //clear ini 
    dt["recorder"]["time0OK"] = "0";
    dt["recorder"]["time1OK"] = "0";
    dt["recorder"]["time2OK"] = "0";
    dt["recorder"]["time3OK"] = "0";
    dt["recorder"]["time4OK"] = "0";
    dt["recorder"]["time5OK"] = "0";
    dt["recorder"]["time6OK"] = "0";
    dt["recorder"]["time7OK"] = "0";
    dt["recorder"]["time8OK"] = "0";
    dt["recorder"]["time9OK"] = "0";
    dt["recorder"]["time10OK"] = "0";
    dt["recorder"]["time11OK"] = "0";
    dt["recorder"]["time12OK"] = "0";
    dt["recorder"]["time13OK"] = "0";
    dt["recorder"]["time14OK"] = "0";
    dt["recorder"]["time15OK"] = "0";
    dt["recorder"]["time16OK"] = "0";
    dt["recorder"]["time17OK"] = "0";
    dt["recorder"]["time18OK"] = "0";
    dt["recorder"]["time19OK"] = "0";
    dt["recorder"]["time20OK"] = "0";
    dt["recorder"]["time21OK"] = "0";
    dt["recorder"]["time22OK"] = "0";
    dt["recorder"]["time23OK"] = "0";
    dt["recorder"]["time0NG"] = "0";
    dt["recorder"]["time1NG"] = "0";
    dt["recorder"]["time2NG"] = "0";
    dt["recorder"]["time3NG"] = "0";
    dt["recorder"]["time4NG"] = "0";
    dt["recorder"]["time5NG"] = "0";
    dt["recorder"]["time6NG"] = "0";
    dt["recorder"]["time7NG"] = "0";
    dt["recorder"]["time8NG"] = "0";
    dt["recorder"]["time9NG"] = "0";
    dt["recorder"]["time10NG"] = "0";
    dt["recorder"]["time11NG"] = "0";
    dt["recorder"]["time12NG"] = "0";
    dt["recorder"]["time13NG"] = "0";
    dt["recorder"]["time14NG"] = "0";
    dt["recorder"]["time15NG"] = "0";
    dt["recorder"]["time16NG"] = "0";
    dt["recorder"]["time17NG"] = "0";
    dt["recorder"]["time18NG"] = "0";
    dt["recorder"]["time19NG"] = "0";
    dt["recorder"]["time20NG"] = "0";
    dt["recorder"]["time21NG"] = "0";
    dt["recorder"]["time22NG"] = "0";
    dt["recorder"]["time23NG"] = "0";
    #endregion //
    iniread.WriteFile("setup.ini", dt);
    chart_display(1);
}



private void textBox1_KeyDown(object sender, KeyEventArgs e)
{

    if (e.KeyCode == Keys.Enter) {

                MatchCollection reg = new Regex(dt["setbarcode"]["barreg"]).Matches(this.textBox1.Text);
            
                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                if (reg.Count>0 && dt["setbarcode"]["barenable"] == "true")

                {
            // button2.PerformClick();
            m.trf = this.textBox1.Text;
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
        else if (!(reg.Count > 0 && dt["setbarcode"]["barenable"] == "true"))
        {

           // MessageBox.Show("条码规则不对");
        }


    }
}

private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
{

}

private void label3_Click(object sender, EventArgs e)
{

}

private void button1_Click(object sender, EventArgs e)
{
            }

private void button1_Click_1(object sender, EventArgs e)
{

}

private void chart1_Click(object sender, EventArgs e)
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
    dt["language"]["english"] = "1";

    iniread.WriteFile("setup.ini", dt);
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
    dt["language"]["english"] = "0";
    iniread.WriteFile("setup.ini", dt);

}

        private void 打开校验程序表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablestaues = 1;
            rep1.OpenReport(@"cmw100calibrationtesttable");
            rep1.CellReadOnly = true;
            rep1.SetFrozenRow(1, 1);
            rep1.RowCount = 300;
            rep1.ColCount = 10;
            rep1.PoleHeight = 0;
            rep1.PoleWidth = 0;
            int temp = rep1.Width;
            rep1.SetColWidth(1, (int)(temp * 0.1));
            rep1.SetColWidth(2, (int)(temp * 0.4));
            rep1.SetColWidth(3, (int)(temp * 0.1));
            rep1.SetColWidth(4, (int)(temp * 0.1));
            rep1.SetColWidth(5, (int)(temp * 0.1));
            rep1.SetColWidth(6, (int)(temp * 0.1));
            rep1.SetColWidth(7, (int)(temp * 0.1));
            rep1.SetColWidth(8, (int)(temp * 0.12));
            rep1.SetColHide(9, true);
            rep1.SetColHide(3, true);
            rep1.SetColHide(10, true);

            if (!File.Exists("cmw100RF_cab_log.csv"))
            {
                string m, v = "time,serialno,result";

                i = 2;
                do
                {
                    m = rep1.GetCellValue(2, i);
                    if (m == null) break;
                    v = v + "," + m + "( " + rep1.GetCellValue(6, i).Replace(",", "#") + "<-->" + rep1.GetCellValue(5, i).Replace(",", "#") + ")";
                    i++;

                } while (m != null && m.Trim().Length > 2);

                File.AppendAllText("cmw100RF_cab_log.csv", v + '\n');
            }

        }

        private void 重新加载测试表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablestaues = 0;
            rep1.OpenReport(@"testcasetable");
            rep1.CellReadOnly = true;
            rep1.SetFrozenRow(1, 1);
            rep1.RowCount = 300;
            rep1.ColCount = 10;
            rep1.PoleHeight = 0;
            rep1.PoleWidth = 0;
            int temp = rep1.Width;
            rep1.SetColWidth(1, (int)(temp * 0.1));
            rep1.SetColWidth(2, (int)(temp * 0.4));
            rep1.SetColWidth(3, (int)(temp * 0.1));
            rep1.SetColWidth(4, (int)(temp * 0.1));
            rep1.SetColWidth(5, (int)(temp * 0.1));
            rep1.SetColWidth(6, (int)(temp * 0.1));
            rep1.SetColWidth(7, (int)(temp * 0.1));
            rep1.SetColWidth(8, (int)(temp * 0.12));
            rep1.SetColHide(9, true);
            rep1.SetColHide(3, true);
            rep1.SetColHide(10, true);

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void 调试DEBUGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();

            form3.Show();
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

        public void chart_display(int bc ) {


    foreach (var series in chart1.Series)
    {
        series.Points.Clear();
    }



        // 在chart中显示数据
        int x = 0;

        series.LegendText = "Number of pass per hour";
        values[0] = int.Parse(dt["recorder"]["time8OK"]);
        values[1] = int.Parse(dt["recorder"]["time9OK"]);
        values[2] = int.Parse(dt["recorder"]["time10OK"]);
        values[3] = int.Parse(dt["recorder"]["time11OK"]);
        values[4] = int.Parse(dt["recorder"]["time12OK"]);
        values[5] = int.Parse(dt["recorder"]["time13OK"]);
        values[6] = int.Parse(dt["recorder"]["time14OK"]);
        values[7] = int.Parse(dt["recorder"]["time15OK"]);
        values[8] = int.Parse(dt["recorder"]["time16OK"]);
        values[9] = int.Parse(dt["recorder"]["time17OK"]);
        values[10] = int.Parse(dt["recorder"]["time18OK"]);
        values[11] = int.Parse(dt["recorder"]["time19OK"]);
        values[12] = int.Parse(dt["recorder"]["time20OK"]);
        values[13] = int.Parse(dt["recorder"]["time21OK"]);
        values[14] = int.Parse(dt["recorder"]["time22OK"]);
        values[15] = int.Parse(dt["recorder"]["time23OK"]);
        values[16] = int.Parse(dt["recorder"]["time0OK"]);
        values[17] = int.Parse(dt["recorder"]["time1OK"]);
        values[18] = int.Parse(dt["recorder"]["time2OK"]);
        values[19] = int.Parse(dt["recorder"]["time3OK"]);
        values[20] = int.Parse(dt["recorder"]["time4OK"]);
        values[21] = int.Parse(dt["recorder"]["time5OK"]);
        values[22] = int.Parse(dt["recorder"]["time6OK"]);
        values[23] = int.Parse(dt["recorder"]["time7OK"]);




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
        values2[0] = int.Parse(dt["recorder"]["time8NG"]);
        values2[1] = int.Parse(dt["recorder"]["time9NG"]);
        values2[2] = int.Parse(dt["recorder"]["time10NG"]);
        values2[3] = int.Parse(dt["recorder"]["time11NG"]);
        values2[4] = int.Parse(dt["recorder"]["time12NG"]);
        values2[5] = int.Parse(dt["recorder"]["time13NG"]);
        values2[6] = int.Parse(dt["recorder"]["time14NG"]);
        values2[7] = int.Parse(dt["recorder"]["time15NG"]);
        values2[8] = int.Parse(dt["recorder"]["time16NG"]);
        values2[9] = int.Parse(dt["recorder"]["time17NG"]);
        values2[10] = int.Parse(dt["recorder"]["time18NG"]);
        values2[11] = int.Parse(dt["recorder"]["time19NG"]);
        values2[12] = int.Parse(dt["recorder"]["time20NG"]);
        values2[13] = int.Parse(dt["recorder"]["time21NG"]);
        values2[14] = int.Parse(dt["recorder"]["time22NG"]);
        values2[15] = int.Parse(dt["recorder"]["time23NG"]);
        values2[16] = int.Parse(dt["recorder"]["time0NG"]);
        values2[17] = int.Parse(dt["recorder"]["time1NG"]);
        values2[18] = int.Parse(dt["recorder"]["time2NG"]);
        values2[19] = int.Parse(dt["recorder"]["time3NG"]);
        values2[20] = int.Parse(dt["recorder"]["time4NG"]);
        values2[21] = int.Parse(dt["recorder"]["time5NG"]);
        values2[22] = int.Parse(dt["recorder"]["time6NG"]);
        values2[23] = int.Parse(dt["recorder"]["time7NG"]);

        foreach (float v in values2)
        {
            series2.Points.AddXY(x2, v);
            x2++;
        }

}











    }



















}





