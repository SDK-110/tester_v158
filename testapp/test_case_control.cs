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

namespace testapp
{


    public partial class test_case_control : UserControl
    {
        [Browsable(true)]
     
        public int uut_number { get; set; } 
        TimeSpan ts1;
        string test_case_file = "";
        volatile string starttime, endtime;
        public EventHandler deal_withmsg;
        string NG_RUN_flog = "no";
        int case_jumper_times = 0;
        static public testcase_dll testcase_lib;
        List<testlog> testlogs = new List<testlog>();
        reogridviewloader viewloader;
        tester_project proj;
        Worksheet myworksheet1;
        string testcase_table_sel = "";
        string if_shieldboxport;
        production_info production;
        public int  init_flog{ set; get; }
        public int done_dealwith_flog { set; get; }
        static object obj_lock = new object();
        IniParser.Model.IniData dt = new IniParser.FileIniDataParser().ReadFile("setup.ini");
        public test_case_control()
        {
            InitializeComponent();
        }

        public void set_production_info(production_info production) {
            this.production = production;

        }

        public void set_testcase_action(string NG_RUN_flog = "no", int case_jumper_times = 0) {

            this.NG_RUN_flog = NG_RUN_flog;
            this.case_jumper_times = case_jumper_times;
        }

        public void set_init_4runlib_testcase(ref testcase_dll testcase_Dll,string test_cases= "project_tester_name.sproj", string testcase_table_sel="sheet1")
        {

            test_case_file = test_cases;
            proj = excel2tester_standard.read_excel_test_cases(test_cases,testcase_table_sel);
            testcase_table_sel = testcase_table_sel;
            testcase_lib = testcase_Dll;
            viewloader = new reogridviewloader(ref this.reoGridControl1, testcase_lib.Getfun(), proj);


            viewloader.table_load_into_viewer();
            for (int i = 0; i < viewloader.tester_proj.test_cases.Count; i++)
            {

                viewloader.tester_proj[i].tf_handler = update_dataview;
            }

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


            this.viewloader.myworksheet1.Cells[id, 4].Data = tester.result_msg;
            this.viewloader.myworksheet1.Cells[id, 5].Data = tester.get_judge_result;
            this.viewloader.myworksheet1.Cells[id, 6].Data = tester.runtime;

            this.Invoke((Action)delegate {


                if (tester.get_judge_result == "fail")
                {
                    this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor = Color.Red;

                }
                else if (tester.get_judge_result == "skip")
                {


                    this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor = Color.OrangeRed;
                }
                else
                {


                    this.viewloader.myworksheet1.Cells[id, 5].Style.TextColor = Color.Green;
                }



                this.viewloader.myworksheet1.ScrollToCell(this.viewloader.myworksheet1.Cells[id, 3]);
                this.viewloader.myworksheet1.SelectRows(id, 1);


                this.Invoke(new Action(() =>
                {

                    this.richTextBox1.AppendText("debug : [setp" + tester.id + $"][{tester.testcase_description}]:==>" + tester.result_msg + "\r\n");
                }));








            });

        }

        private void UserControl2_SizeChanged(object sender, EventArgs e)
        {
            if (viewloader != null) viewloader.view_update();
            
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            starttime = DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss: ffff");
            this.toolStripSplitButton1.Text= DateTime.Now.ToString("HH:mm:ss:ffff");
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            e.Result = "pass";
            string stemp = "";

            this.Invoke(new Action(() =>
            {
                viewloader.table_load_into_viewer();
                this.progressBar1.Value = 1;
                ModifyProgressBarColor.SetState(this.progressBar1, 1);
                this.progressBar1.Maximum = viewloader.tester_proj.test_cases.Count;
                this.richTextBox1.Text = "";
               if(deal_withmsg!=null) deal_withmsg(new msgpacketer() { state_num = msg_type.set_button_action, msg = "button_set" }, new EventArgs() { });
            }));

            string tempbuf;
            try
            {

               
               testcase_lib.Getfun()[$"DUT{init_flog}_init"]("pass", "pass", out tempbuf);
                   
                testcase_lib.Getfun()["record_buf_var_init"]("pass", "pass", out tempbuf);
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
                            if (runjud == judge_result.pass || runjud == judge_result.skip) break;
                          //  backgroundWorker1.ReportProgress(rc, viewloader.tester_proj.test_cases[rc].result_msg);
                           // if (this.NG_RUN_flog== "yes") { if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail") { e.Result = "fail"; goto exit; } }
                        }

                        if (this.NG_RUN_flog == "yes") {
                            if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail") {
                               // e.Result = "fail";
                                if (int.Parse(viewloader.tester_proj.test_cases[rc].repeat_goto) == rc || viewloader.tester_proj.test_cases[rc].jump_loop_flog <= (-1 * case_jumper_times))
                                    goto exit; } 
                        
                        }

                        if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail")
                        {

                            if (viewloader.tester_proj.test_cases.Count - rc - 1 == 0) {

                               // e.Result = "fail";
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


            e.Result = "pass";
            for (int ti = 0; ti < viewloader.tester_proj.test_cases.Count; ti++)
            {

                if (viewloader.tester_proj.test_cases[ti].get_judge_result == "fail")
                {
                    e.Result = "fail";
                    //  MessageBox.Show("Test==>" + viewloader.tester_proj.test_cases[ti].testcase_description);
                    break;
                }

            }
            if (new IniParser.FileIniDataParser().ReadFile("setup.ini")["setproduct"]["bingxing"] != "true") { 
            testcase_lib.Getfun()["testsysini"]("pass", "pass", out stemp, "00;00");
            }
            endtime = DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss: ffff");


        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                int i = e.ProgressPercentage;
                string resu = (string)e.UserState;
                this.progressBar1.Value = i;


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
                edit.load_projectt_file(test_case_file);
                edit.ShowDialog();
                set_init_4runlib_testcase(ref testcase_lib, test_case_file);
                viewloader.table_load_into_viewer(3);


            }

            if (key_down_4_flog == 1) {
                key_down_4_flog = 0;

              int  row_n = reoGridControl1.CurrentWorksheet.SelectionRange.Row;
                if (row_n > viewloader.tester_proj.test_cases.Count) return;
                var runjud = viewloader.tester_proj.test_cases[row_n].get_rusult(ref viewloader.testcase_lib);
                string msg_rsu = viewloader.tester_proj.test_cases[row_n].result_msg;
                mylib.utility_func.callbackdebuginfo("debug==>" + "\n rsult:[" +  runjud + "]>>[" + msg_rsu +"]");
            }

        }

        private void reoGridControl1_Click(object sender, EventArgs e)
        {

        }
        int key_down_flog = 0;
        int key_down_4_flog = 0;
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.toolStripStatusLabel3.Text = DateTime.Now.ToString("HH:mm:ss:ffff");
         TimeSpan   ts2 = new TimeSpan(DateTime.Now.Ticks);
            string temp_sn = Guid.NewGuid().ToString("N");
            this.toolStripStatusLabel5.Text = ts2.Subtract(ts1).Duration()+"";
            // MessageBox.Show((string)e.Result);
            if (e.Result != "pass") {
                ModifyProgressBarColor.SetState(this.progressBar1, 2);
                
            }
            this.progressBar1.Value = this.progressBar1.Maximum;
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
            s.Append(((production.SN == "") ? temp_sn : production.SN) + ",");
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
            lock (obj_lock) { 
            new System.Threading.Thread(() =>
            {
                utility_func.testlog_save_path(production.log_path_name, s.ToString());
            }).Start();

            }
            string opter = "S000331";
            try
            {
                opter = dt["setproduct"]["personal_number"];
            }
            catch
            {

                opter = "S000331";
            }
            switch (production.save_texing)
            {

                case "general_log":
                    new System.Threading.Thread(() =>
                    {
                       utility_func.testlog_save_path(production.log_path_name, s.ToString());
                    }).Start();
                    break;


                case "smx_mes_logger":
                    int ct = testlogs.Count;
                    if (ct < 15)
                    {

                        for (int v = 0; v < 15 - ct; v++)
                        {


                            testlogs.Add(new testlog()
                            {
                                teset_case_result = "NA",
                                test_case_description = " NA",
                                test_case_limit_hi = "NA",
                                test_case_limit_low = "NA",
                                test_case_item_number = "" + ct + v,
                                test_case_judge = "NA",
                                test_case_result_unit = "NA",
                                test_case_test_span = "NA",

                            });
                        }

                    }
                    if (ct > 15)
                    {

                        testlogs = testlogs.Take(15).ToList();

                    }
                    string line_number_n = "L01";
                    string personal_number_n = "12345";
                    string work_station_n = "FCT-001" ;
                    if (dt["setproduct"]["line_number"] != null && dt["setproduct"]["personal_number"] != null && dt["setproduct"]["work_station"] != null)
                    {
                        line_number_n = dt["setproduct"]["line_number"]+ "_" + uut_number;
                        personal_number_n = dt["setproduct"]["personal_number"];
                        work_station_n = dt["setproduct"]["work_station"]+ "_" + uut_number;
                        //surl = dt["setproduct"]["surl"];

                    }
                    if (resultt == "pass")
                    {

                        string filename = dt["setproduct"]["project"] + "_" + ((production.SN == "") ? temp_sn : production.SN.Trim()) + "_" + DateTime.Now.ToString("yyMMddhhmmss_ffff") + "_Passed.csv";


                        utility_func.testlog_new_save_for_smx(ref testlogs, "../smx/pass", filename,
                                           personal_number_n, line_number_n, work_station_n, dt["setproduct"]["project"], ((production.SN == "") ? temp_sn : production.SN.Trim()), this.toolStripSplitButton1.Text, this.toolStripStatusLabel3.Text, "Passed");



                    }
                    else
                    {

                        string filename = dt["setproduct"]["project"] + "_" + ((production.SN == "") ? temp_sn : production.SN.Trim()) + "_" + DateTime.Now.ToString("yyMMddhhmmss_ffff") + "_Failed.csv";

                        utility_func.testlog_new_save_for_smx(ref testlogs, "../smx/fail", filename,
                                           personal_number_n, line_number_n, work_station_n, dt["setproduct"]["project"], ((production.SN == "") ? temp_sn : production.SN.Trim()), this.toolStripSplitButton1.Text, this.toolStripStatusLabel3.Text, "Failed");

                    }

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
                            utility_func.testlog_save_for_sgw(ref testlogs, "./sgw/pass", filename, opter, production.project_name, (production.SN != null) ? production.SN : "null",
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
                                                            opter,
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
                                                               opter,
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
                                                               opter,
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
           

           testcase_lib.Getfun()[$"DUT{done_dealwith_flog}_done_dealwith"]("pass", "pass", out tempbuf);
            

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
    }

    public class msgpacketer{public  msg_type state_num; public string msg; };
    public enum msg_type {set_button_action = 0,
                          pass_fail_count = 1
                          }
    public delegate void my_sendmsg(object msg);
    public class production_info
    {
        public string SN = "";
        public string project_name = "null";
        public string log_path_name = "log_save.csv";
        public string sgw_record_mac;
        public string save_texing = "smx_mes_logger";

    }


    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }

}
