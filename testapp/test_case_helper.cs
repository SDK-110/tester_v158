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
using System.Threading;
using testapp.mycontroler;

namespace testapp
{
   

    public  class test_case_helper 
    {

        private callback_dosomething ini_do_something = null;
        private callback_dosomething end_do_something = null;
        private callback_dosometing_take _run_msg_callback = null;
        public static rebuild.testcase_loader.callback_this update_call = null;
        BackgroundWorker backgroundWorker1  = null;
        Dictionary<string, pointfun> lib = null;
           [Browsable(true)]
        [Category("Custom3")]
        [Description("Specifies the value of the control.")]
        public callback_dosometing_take run_msg_callback
        {

            get { return run_msg_callback; }

            set { run_msg_callback = value; }
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

        public test_case_helper()
        {

           this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            this.backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            this.backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            this.backgroundWorker1.WorkerReportsProgress = true;

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
            lib = testcase_Dll.Getfun();
        

                for (int i = 0; i < proj.test_cases.Count; i++)
                {

                    proj[i].tf_handler = update_call;
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




        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            starttime = DateTime.Now.ToString("yyyy / MM / dd HH: mm:ss: ffff");
          
            ts1 = new TimeSpan(DateTime.Now.Ticks);
            e.Result = "pass";
            string stemp = "";


            string tempbuf;
            try
            {
                if (ini_do_something != null) ini_do_something();
                
                string globe_result = "pass";
                string z = "";
                string mangmu = "0";
                
                while (1 == 1)
                {

                    for (int rc = 0; rc < proj.test_cases.Count; rc++)
                    {
                        backgroundWorker1.ReportProgress(rc, null);
                        if (backgroundWorker1.CancellationPending == true) break;

                        int cout = int.Parse(proj.test_cases[rc].self_run_count);
                        while ((--cout) >= 0)
                        {
                            var runjud = proj.test_cases[rc].get_rusult(ref lib);
                            if (_run_msg_callback != null) _run_msg_callback(proj.test_cases[rc]);
                            if (runjud == judge_result.pass || runjud == judge_result.skip) break;
                          //  backgroundWorker1.ReportProgress(rc, viewloader.tester_proj.test_cases[rc].result_msg);
                           // if (this.NG_RUN_flog== "yes") { if (viewloader.tester_proj.test_cases[rc].get_judge_result == "fail") { e.Result = "fail"; goto exit; } }
                        }

                        if (this.NG_RUN_flog == "yes") {
                            if (proj.test_cases[rc].get_judge_result == "fail") {
                                e.Result = "fail";
                                if (int.Parse(proj.test_cases[rc].repeat_goto) == rc || proj.test_cases[rc].jump_loop_flog <= (-1 * case_jumper_times))
                                    goto exit; } }

                        if (proj.test_cases[rc].get_judge_result == "fail")
                        {

                            if (proj.test_cases.Count - rc - 1 == 0) {

                                e.Result = "fail";
                                goto exit;
                            }

                            if (proj.test_cases[rc].jump_loop_flog > (-1 * case_jumper_times))
                            {

                                mangmu = proj.test_cases[rc].repeat_goto;
                                if (int.Parse(mangmu) == (rc)) continue;
                                proj.test_cases[rc].jump_loop_flog -= 1;
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
              


            }
            catch (Exception) { }


            //  MessageBox.Show(a + "");
        }





    
    

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
         
         TimeSpan   ts2 = new TimeSpan(DateTime.Now.Ticks);

     
            string resultt = "pass";
            StringBuilder s = new StringBuilder();
          
            if (!File.Exists(production.log_path_name))
            {
                string m, v = "time,serialno,result";


                for (int rdi = 0; rdi < proj.test_cases.Count; rdi++)
                {


                    v = v + "," + proj.test_cases[rdi].testcase_description.Trim() + "( " + proj.test_cases[rdi].testcase_high_limit.Replace(",", "#") + "<-->" + proj.test_cases[rdi].testcase_low_limit.Replace(",", "#") + ")";


                }

                utility_func.testlog_save_path(production.log_path_name, v);
            }

            bool is_teshu_logger_detection = false;
            s.Append(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff") + ",");
            //    s.Append(((this.textBox1.Text == "") ? "skip" : this.textBox1.Text) + ",");
            s.Append(((production.SN == "") ? "NA" : production.SN) + ",");
            s.Append((string)e.Result + ",");
            int teshulogger_count = 0;
            for (int a = 0; a < proj.test_cases.Count; a++)
            {

                if (proj.test_cases[a].is_teshu_logger)
                {


                    is_teshu_logger_detection = true; testlogs.Add(new testlog()
                    {
                        teset_case_result = proj.test_cases[a].get_judge_result,
                        test_case_description = proj.test_cases[a].testcase_description,
                        test_case_judge = proj.test_cases[a].get_judge_result,
                        test_case_limit_hi = proj.test_cases[a].testcase_high_limit,
                        test_case_limit_low =proj.test_cases[a].testcase_low_limit,
                        test_case_result_unit = proj.test_cases[a].result_msg,
                        test_case_test_span = proj.test_cases[a].runtime + "",
                        test_case_item_number = teshulogger_count + ""


                    }); ;
                }
                teshulogger_count++;

                if (proj.test_cases[a].get_judge_result == "fail") resultt = "fail";

                s.Append(proj.test_cases[a].get_judge_result + ",");
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
