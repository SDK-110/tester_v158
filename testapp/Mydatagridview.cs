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
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.duochuangti;
using System.Reflection;

namespace testapp
{
   

    public partial class Mydata_gridview : UserControl
    {

        private callback_dosomething ini_do_something = null;
        private callback_dosomething end_do_something = null;
        private callback_dosometing_take _run_msg_callback = null;

        [Browsable(true)]
        [Category("Custom3")]
        [Description("Specifies the value of the control.")]
        public callback_dosometing_take run_msg_callback
        {

            get { return _run_msg_callback; }

            set { _run_msg_callback = value; }
        }
        [Browsable(true)]
        [Category("Custom1")]
        [Description("Specifies the value of the control.")]
        public callback_dosomething obj_ini_do_something {

            get { return ini_do_something; }

            set { ini_do_something = value; }
        }
        [Browsable(true)]
        [Category("Custom2")]
        [Description("Specifies the value of the control.")]
        public callback_dosomething obj_end_do_something
        {

            get { return end_do_something; }

            set { end_do_something = value; }
        }

        public Mydata_gridview()
        {

           
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            Type dgvType = this.dataGridView1.GetType();
            PropertyInfo pi = dgvType.GetProperty("DoubleBuffered",
            BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(this.dataGridView1, true, null);
            statusStrip1.RenderMode = ToolStripRenderMode.Professional;

            // 设置 statusStrip 控件的 BackColor 属性为透明色
            statusStrip1.BackColor = Color.Transparent;

            // 针对每个 ToolStripStatusLabel 控件，将其 BackColor 属性设置为透明色
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                if (item is ToolStripStatusLabel)
                {
                    ToolStripStatusLabel label = (ToolStripStatusLabel)item;
                    label.BackColor = Color.Transparent;
                }
            }

        }

        public void set_production_info(production_info production) {
            
            this.production = production;

        }

        public void set_testcase_action(string NG_RUN_flog = "no", int case_jumper_times = 0) {

            this.NG_RUN_flog = NG_RUN_flog;
            this.case_jumper_times = case_jumper_times;
        }

        public void set_init_4runlib_testcase(ref testcase_dll testcase_Dll,string test_cases= "project_tester_name")
        {
            
            test_case_file = test_cases;
            proj = json2tester_standard.red_json_test_project(test_cases+".json");
            testcase_table_sel = testcase_table_sel;
            testcase_lib = testcase_Dll;
            viewloader = new datagrid_viewloader(ref this.dataGridView1, testcase_lib.Getfun(), proj);


            viewloader.table_load_into_viewer();
            for (int i = 0; i < viewloader.tester_proj.test_cases.Count; i++)
            {

                viewloader.tester_proj[i].tf_handler = update_dataview;
            }

        }

        public void resize() {

            viewloader.set_view_update();

        }
        public void run() {

            if (!backgroundWorker1.IsBusy) {

                backgroundWorker1.RunWorkerAsync();
            }

        }

        public void cancel_run() {

            if (backgroundWorker1.IsBusy) backgroundWorker1.WorkerSupportsCancellation = true;
        }
        private void UserControl2_Load(object sender, EventArgs e)
        {
           
        }

        private void update_dataview(tester_standard_style tester, int id)
        {


            this.viewloader.set_cell_value(id,4, tester.result_msg);
            this.viewloader.set_cell_value(id, 5, tester.get_judge_result);
            this.viewloader.set_cell_value(id, 6, tester.runtime.ToString());
          

            this.Invoke((Action)delegate {


                if (tester.get_judge_result == "fail")
                {
                    this.viewloader._set_cell_front_color(id, 5,Color.Red);

                }
                else if (tester.get_judge_result == "skip")
                {


                    this.viewloader._set_cell_front_color(id, 5, Color.OrangeRed);
                }
                else
                {


                    this.viewloader._set_cell_front_color(id, 5, Color.Green);
                }


                this.viewloader.set_row_color_and_2show(id);
                //this.viewloader.myworksheet1.ScrollToCell(this.viewloader.myworksheet1.Cells[id, 3]);
                //this.viewloader.myworksheet1.SelectRows(id, 1);


                this.Invoke(new Action(() =>
                {

                    this.richTextBox1.AppendText("debug : [setp" + tester.id + $"][{tester.testcase_description}]:==>" + tester.result_msg + "\r\n");
                }));








            });

        }

        private void UserControl2_SizeChanged(object sender, EventArgs e)
        {
            if (viewloader != null) viewloader.set_view_update();
            
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            starttime = DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss: ffff");
            this.toolStripSplitButton1.Text= DateTime.Now.ToString("HH: mm:ss: ffff");
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            e.Result = "pass";
            string stemp = "";

            this.Invoke(new Action(() =>
            {
                viewloader.table_load_into_viewer();
                this.lTrackBar1.L_Value = 1;
               
                this.lTrackBar1.L_Maximum = viewloader.tester_proj.test_cases.Count;
                this.richTextBox1.Text = "";
               if(deal_withmsg!=null) deal_withmsg(new msgpacketer() { state_num = msg_type.set_button_action, msg = "button_set" }, new EventArgs() { });
            }));

            string tempbuf;
            try
            {
                if (ini_do_something != null) ini_do_something();
                
                string globe_result = "pass";
                string z = "";
                string mangmu = "0";
                
                while (1 == 1)
                {

                    for (int rc = 0; rc < viewloader.tester_proj.test_cases.Count; rc++)
                    {
                        backgroundWorker1.ReportProgress(rc, null);
                        if (backgroundWorker1.CancellationPending == true) break;

                        int cout = int.Parse(viewloader.tester_proj.test_cases[rc].self_run_count);
                        while ((--cout) >= 0)
                        {
                            var runjud = viewloader.tester_proj.test_cases[rc].get_rusult(ref viewloader.testcase_lib);
                            if (_run_msg_callback != null) _run_msg_callback(viewloader.tester_proj.test_cases[rc]);
                            if (runjud == judge_result.pass || runjud == judge_result.skip) break;
                          //  backgroundWorker1.ReportProgress(rc, viewloader.tester_proj.test_cases[rc].result_msg);
                           // if (this.NG_RUN_flog== "yes") { if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail") { e.Result = "fail"; goto exit; } }
                        }

                        if (this.NG_RUN_flog == "yes") {
                            if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail") {
                                e.Result = "fail";
                                if (int.Parse(viewloader.tester_proj.test_cases[rc].repeat_goto) == rc || viewloader.tester_proj.test_cases[rc].jump_loop_flog <= (-1 * case_jumper_times))
                                    goto exit; } }

                        if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail")
                        {

                            if (viewloader.tester_proj.test_cases.Count - rc - 1 == 0) {

                                e.Result = "fail";
                                goto exit;
                            }

                            if (viewloader.tester_proj.test_cases[rc].jump_loop_flog > (-1 * case_jumper_times))
                            {

                                mangmu = viewloader.tester_proj.test_cases[rc].repeat_goto;
                                if (int.Parse(mangmu) == (rc)) continue;
                                viewloader.tester_proj.test_cases[rc].jump_loop_flog -= 1;
                                rc = int.Parse(mangmu) - 1;
                                continue;
                            }
                           

                        }

            

                      
                    }
                    break;
                }
            exit:

                ; ;
            }
            catch (Exception m)
            {


                MessageBox.Show(m.ToString());

            }

            if (end_do_something != null) end_do_something();
            endtime = DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss: ffff");

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int i = e.ProgressPercentage;
                string resu = (string)e.UserState;
                this.lTrackBar1.L_Value = i;


            }
            catch (Exception) { }


            //  MessageBox.Show(a + "");
        }

        private void reoGridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Z)
            {
                // Code to execute when Ctrl+C is pressed

               
               
            }
            base.OnKeyDown(e);
        }



        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                key_down_flog = 1;
                
                // Code to handle Ctrl + A key combination
                // 检测按下ctrl + a 组合键的逻辑
            }


            if (keyData == (Keys.Control | Keys.Q))
            {
               key_down_4_flog = 1;

                // Code to handle Ctrl + A key combination
                // 检测按下ctrl + a 组合键的逻辑
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void reoGridControl1_DoubleClick(object sender, EventArgs e)
        {
            if (key_down_flog == 1) { key_down_flog = 0;
                //ooooooooo

                sheet_edit edit = new sheet_edit();
               // edit.load_projectt_file(test_case_file);
             //   edit.ShowDialog();
             //   set_init_4runlib_testcase(ref testcase_lib, test_case_file);
              //  viewloader.table_load_into_viewer();


            }

            if (key_down_4_flog == 1) {
                key_down_4_flog = 0;

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // 获取选中的行号
                    int row_n = dataGridView1.SelectedRows[0].Index;

                    if (row_n > viewloader.tester_proj.test_cases.Count) return;
                    var runjud = viewloader.tester_proj.test_cases[row_n].get_rusult(ref viewloader.testcase_lib);
                    string msg_rsu = viewloader.tester_proj.test_cases[row_n].result_msg;
                    mylib.utility_func.callbackdebuginfo("debug==>" + "\n rsult:[" + runjud + "]>>[" + msg_rsu + "]");
                    // 处理选中的数据
                    // ...
                }
               
            }

        }

        private void reoGridControl1_Click(object sender, EventArgs e)
        {

        }
        int key_down_flog = 0;
        int key_down_4_flog = 0;

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.ScrollToCaret();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripStatusLabel3.Text = DateTime.Now.ToString("HH: mm:ss: ffff");
         TimeSpan   ts2 = new TimeSpan(DateTime.Now.Ticks);

            this.toolStripStatusLabel5.Text = ts2.Subtract(ts1).Duration()+"";
            // MessageBox.Show((string)e.Result);
            if (e.Result != "pass") {
                this.lTrackBar1.BackColor = Color.Green;
                
            }
            this.lTrackBar1.L_Value = this.lTrackBar1.L_Maximum;
            string resultt = "pass";
            StringBuilder s = new StringBuilder();
          
            if (!File.Exists(production.log_path_name))
            {
                string m, v = "time,serialno,result";


                for (int rdi = 0; rdi < viewloader.tester_proj.test_cases.Count; rdi++)
                {


                    v = v + "," + viewloader.tester_proj.test_cases[rdi].testcase_description.Trim() + "( " + viewloader.tester_proj.test_cases[rdi].testcase_high_limit.Replace(",", "#") + "<-->" + viewloader.tester_proj.test_cases[rdi].testcase_low_limit.Replace(",", "#") + ")";


                }

                utility_func.testlog_save_path(production.log_path_name, v);
            }

            bool is_teshu_logger_detection = false;
            s.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ",");
            //    s.Append(((this.textBox1.Text == "") ? "skip" : this.textBox1.Text) + ",");
            s.Append(((production.SN == "") ? "NA" : production.SN) + ",");
            s.Append((string)e.Result + ",");
            int teshulogger_count = 0;
            for (int a = 0; a < viewloader.tester_proj.test_cases.Count; a++)
            {

                if (viewloader.tester_proj.test_cases[a].is_teshu_logger)
                {


                    is_teshu_logger_detection = true; testlogs.Add(new testlog()
                    {
                        teset_case_result = viewloader.tester_proj.test_cases[a].get_judge_result,
                        test_case_description = viewloader.tester_proj.test_cases[a].testcase_description,
                        test_case_judge = viewloader.tester_proj.test_cases[a].get_judge_result,
                        test_case_limit_hi = viewloader.tester_proj.test_cases[a].testcase_high_limit,
                        test_case_limit_low = viewloader.tester_proj.test_cases[a].testcase_low_limit,
                        test_case_result_unit = viewloader.tester_proj.test_cases[a].result_msg,
                        test_case_test_span = viewloader.tester_proj.test_cases[a].runtime + "",
                        test_case_item_number = teshulogger_count + ""


                    }); ;
                }
                teshulogger_count++;

                if (viewloader.tester_proj.test_cases[a].get_judge_result == "fail") resultt = "fail";

                s.Append(viewloader.tester_proj.test_cases[a].get_judge_result + ",");
            }
            s.Remove(s.Length - 1, 1);
            switch (production.save_texing)
            {

                case "general_log":
                    new System.Threading.Thread(() =>
                    {
                       utility_func.testlog_save_path(production.log_path_name, s.ToString());
                    }).Start();
                    break;

                case "rf_calibration":
                    File.AppendAllText("cmw100RF_cab_log.csv", s.ToString());
                    break;
                case "sgw_log":

                    new System.Threading.Thread(() =>
                    {
                        if (resultt == "pass")
                        {

                            string filename = production.project_name + "_" + production.sgw_record_mac + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Passed.csv";
                            utility_func.testlog_save_for_sgw(ref testlogs, "./sgw/pass", filename, "S000331", production.project_name, (production.SN != null) ? production.SN : "null",
                                                                   production.sgw_record_mac,
                                                                   starttime,
                                                                   endtime,
                                                                    "Passed"
                                                                    );

                        }
                        else
                        {

                            string filename = production.project_name + "_" + production.sgw_record_mac + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                            utility_func.testlog_save_for_sgw(ref testlogs,
                                                            "./sgw/fail",
                                                            filename,
                                                           "'" + "S000331",
                                                            production.project_name,
                                                          (production.SN != null) ? production.SN : "null",
                                                            production.sgw_record_mac,
                                                          starttime,
                                                                   endtime,
                                                            "Failed"
                                                            );



                        }
                    }).Start();
                    break;
                case "asm_logger":
                    new System.Threading.Thread(() =>
                    {
                        if (resultt == "pass")
                            {

                        string filename = production.project_name + "_" + production.SN + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                        utility_func.testlog_save_for_asm(ref testlogs,
                                                                "./asm_lg/pass",
                                                                filename,
                                                               "S000331",
                                                                production.project_name,
                                                               (production.SN != null) ? production.SN : "null",
                                                               starttime,
                                                                   endtime,
                                                                "Passed"
                                                                );

                            }
                            else
                            {
                        string filename = production.project_name + "_" + production.SN + "_" + DateTime.Now.ToString("yyMMddhhmmss") + "_Failed.csv";
                        utility_func.testlog_save_for_asm(ref testlogs,
                                                                "./asm_lg/pass",
                                                                filename,
                                                               "S000331",
                                                                production.project_name,
                                                               (production.SN != null) ? production.SN : "null",
                                                              starttime,
                                                                   endtime,
                                                                "Failed"
                                                                );




                    }



            }).Start(); ;

                    break;


                default:
                    break;

            }


            if (deal_withmsg != null)
            {

                deal_withmsg(new msgpacketer() { state_num= msg_type.set_button_action , msg ="button_set"  }, new EventArgs() { });


            }
            string  tempbuf ;
            switch (done_dealwith_flog)
            {
                case 1:
                    {

                        testcase_lib.Getfun()["done_dealwith1"]("pass", "pass", out tempbuf);
                    }
                    break;
                case 2:
                    {

                        testcase_lib.Getfun()["done_dealwith2"]("pass", "pass", out tempbuf);
                    }
                    break;
                case 3:
                    {
                        testcase_lib.Getfun()["done_dealwith3"]("pass", "pass", out tempbuf);

                    }
                    break;
                case 4:
                    {
                        testcase_lib.Getfun()["done_dealwith4"]("pass", "pass", out tempbuf);

                    }
                    break;
                default:
                    break;


            }

            if ((string)resultt == "pass")
            {

                if (if_shieldboxport != null)
                {
                    string c = "";
                    testcase_lib.Getfun()["shieldboxopen"]("", "", out c);
                }

                if (deal_withmsg != null) deal_withmsg(new msgpacketer() { state_num = msg_type.pass_fail_count, msg = "pass" }, null);
              
            }
            if (resultt == "fail")
            {
                if(deal_withmsg!=null)deal_withmsg(new msgpacketer() { state_num = msg_type.pass_fail_count, msg = "fail" }, null);
            }
           
          
            GC.Collect();
          
        }

        TimeSpan ts1;
        string test_case_file = "";
        volatile string starttime, endtime;
        public EventHandler deal_withmsg;
        string NG_RUN_flog = "no";
        int case_jumper_times = 0;
        static public testcase_dll testcase_lib;
        List<testlog> testlogs = new List<testlog>();
        datagrid_viewloader viewloader;
        tester_project proj;
        Worksheet myworksheet1;
        string testcase_table_sel = "";
        string if_shieldboxport;
        production_info production;
        public int init_flog { set; get; }
        public int done_dealwith_flog { set; get; }
        public Dictionary<string, pointfun> _testcaselib;
    }



}
