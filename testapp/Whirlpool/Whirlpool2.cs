using Code4Bugs.Utils.Types;
using MetroFramework.Controls;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using PCHMI;
using ReaLTaiizor;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.mylib;
using testapp.useful;
using unvell.ReoGrid.IO.OpenXML.Schema;

using System.Xml.Serialization;
using NationalInstruments.Restricted;
namespace testapp.whirlpool
{
    public partial class whirlpool2 :MetroFramework.Forms.MetroForm
    {
        List<setup_cable_test> cableTests = new List<setup_cable_test>();
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);
        string filename = "";
        AutoResetEvent are = new AutoResetEvent(false);
        product_setting globe_setting;
        volatile int _buf_flog=0;
        volatile int singel_where = 0;
        volatile uint frame_cout = 0;
        volatile uint stop_floag = 0;
        //Dictionary<string, string> buf = new Dictionary<string, string>();
        //CircularArray<string> buf = new CircularArray<string>(300);
        ConcurrentQueue<string> buf = new ConcurrentQueue<string>();
        System.Collections.Concurrent.ConcurrentQueue<string> buf_log = new System.Collections.Concurrent.ConcurrentQueue<string>();
        private static Stopwatch stopwatch = new Stopwatch();
        private static TimeSpan lastExecutionTime = TimeSpan.Zero;
        luoyinji lyj;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken;
        public whirlpool2()
        {
            InitializeComponent();
            get_wire_config();

            dataGridView1.VirtualMode = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            cancellationToken = cancellationTokenSource.Token;
            //List<testapp.mylib.testlog> log = new List<testlog>();
            //string m = "DP: O / S / C PASS 001 O / S TEST...............PASS\n" +
            //   "002 4W CONDUCTANCE TEST....PASS\n" +
            //   "003 Cond: A01 - A02: 0.154Ohm\n" +
            //   "DP:End Test\n" +
            //   "DP: TEST PASS\n" +
            //   "001 O / S TEST...............PASS\n" +
            //   "002 4W CONDUCTANCE TEST....PASS\n" +
            //   "003 Cond: A01 - A02: 0.154Ohm\n" +
            //   "DP:End Test\n";


            //string[] roin_rsus = m.Split("\n".ToArray());
            //foreach (var item in cableTests)
            //{

            //    foreach (string rsu in roin_rsus)
            //    {
            //        Match _match = Regex.Match(rsu, item.test_reg_str);

            //        if (_match.Success)
            //        {

            //            string value = _match.Groups[1].Value;
            //            if (item.is_digit == "true")
            //            {

            //                double v = double.Parse(value);

            //                if (v >= double.Parse(item.limit_low) && v <= double.Parse(item.limit_hi))
            //                {
            //                    log.Add(new testlog()
            //                    {
            //                        teset_case_result = value,
            //                        test_case_description = item.test_disp,
            //                        test_case_limit_hi = item.limit_hi,
            //                        test_case_limit_low = item.limit_low,
            //                        test_case_item_number = "",
            //                        test_case_judge = "pass",
            //                        test_case_result_unit = "value",
            //                        test_case_test_span = "1ms",


            //                    });

            //                }
            //                else
            //                {

            //                    log.Add(new testlog()
            //                    {
            //                        teset_case_result = value,
            //                        test_case_description = item.test_disp,
            //                        test_case_limit_hi = item.limit_hi,
            //                        test_case_limit_low = item.limit_low,
            //                        test_case_item_number = "",
            //                        test_case_judge = "fail",
            //                        test_case_result_unit = "string",
            //                        test_case_test_span = "1ms",


            //                    });


            //                }


            //            }
            //            else
            //            {


            //                if (value == item.limit_hi)
            //                {

            //                    log.Add(new testlog()
            //                    {
            //                        teset_case_result = value,
            //                        test_case_description = item.test_disp,
            //                        test_case_limit_hi = item.limit_hi,
            //                        test_case_limit_low = item.limit_low,
            //                        test_case_item_number = "",
            //                        test_case_judge = "pass",
            //                        test_case_result_unit = "string",
            //                        test_case_test_span = "1ms",


            //                    });


            //                }
            //                else
            //                {

            //                    log.Add(new testlog()
            //                    {
            //                        teset_case_result = value,
            //                        test_case_description = item.test_disp,
            //                        test_case_limit_hi = item.limit_hi,
            //                        test_case_limit_low = item.limit_low,
            //                        test_case_item_number = "",
            //                        test_case_judge = "fail",
            //                        test_case_result_unit = "string",
            //                        test_case_test_span = "1ms",


            //                    });



            //                }




            //            }

            //            break;
            //        }
            //    }

            //}



        }

        private void roundButton1_Click(object sender, EventArgs e)
        {
            //  lyj.WriteLine("K_ESC");
            //  lyj.send_command("K_TEST");
            //  lyj.WriteLine("K_TEST");
            // System.Threading.Thread.Sleep(300);
            //  lyj.WriteLine("K_TEST");
            // new sqlite_handle("TestData");

            // get_sqlite_table_toview();

            // new FestoolCom("com3", 9600);

          //  buf.clear();


        }

  

     public  class product_setting {
        public string customer { set; get; } = "Whirlpool Tester";
        public string Maufacture { set; get; } = "Season SDK";
        public string Product_name { set; get; } = "Auto Line";
        
         public string PIC { set; get; } = "Isia";
         public string Operator { set; get; } = "Tom";
         public string  Test_Filename { set; get; } = "AUTO_12345";

            public int tested { get {

                    return passed + failed;
                } 
            }
            public int passed { set; get; } = 0;
            public int failed { set; get; } = 0;
            public string last_sn { set; get; } = "AUTO_12345";
            public string work_station = "TEST01";
            public string line = "L4";
            public string sn_reg { set; get; } = @"\d+";
            public string COM_PORT { set; get; } = @"COM1;115200";
        }

        private void monitoring1_FormClosing(object sender, FormClosingEventArgs e)
        {
            testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");
        }

        private void monitoring1_Load(object sender, EventArgs e)
        {
            this.metroTabControl1.TabPages.Remove(metroTabPage3);
            this.metroTabControl1.SelectedTab= metroTabPage2;
            this.metroDateTime1.Value = this.metroDateTime1.Value.AddDays(-3);
            this.metroDateTime2.Value = DateTime.Now;

         //   Task.Factory.StartNew(() =>
         //   {

                this.Invoke(new Action(() =>
                {
                    if (!File.Exists("setup_p.xml"))
                    {
                        if (globe_setting == null) globe_setting = new product_setting();
                        testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");

                    }
                    if (globe_setting == null) globe_setting = testapp.useful.XmlHelper.DeserializeFromXml<product_setting>("setup_p.xml");

                    
                    update_ui();
                    this.label1.Text = "WAIT";
                    this.label1.ForeColor = System.Drawing.Color.Blue;
                }));


          //  });

        }


        public void update_ui() {
            this.label8.Text = globe_setting.customer;
            this.label3.Text = globe_setting.tested.ToString();
            this.label4.Text = globe_setting.passed.ToString();
            this.label6.Text = globe_setting.failed.ToString();
            percentageProgressBar1.Maximum = percentageProgressBar2.Maximum= percentageProgressBar3.Maximum = (globe_setting.tested == 0) ? 1 : globe_setting.tested;
            percentageProgressBar1.Value = globe_setting.passed;
            percentageProgressBar2.Value = globe_setting.passed;
            percentageProgressBar3.Value = globe_setting.failed;
            PIC.Text = globe_setting.PIC;
            this.Product_Name.Text = globe_setting.Product_name;
            this.Maufacturer.Text = globe_setting.Maufacture;
            this.test_file_name.Text = globe_setting.Test_Filename;
          this.work_station.Text = globe_setting.work_station;
            this.Line_number.Text = globe_setting.line;
            this.Operator.Text = globe_setting.Operator;
            this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename}| Operator: {globe_setting.Operator}| last_SN: {globe_setting.last_sn}| Work_Station:{globe_setting.work_station}| Line:{globe_setting.line}";
            this.label20.Text = $"P/N:{globe_setting.Product_name}";



        }
        private void roundButton2_Click(object sender, EventArgs e)
        {

            globe_setting.PIC = PIC.Text;
            globe_setting.Product_name = this.Product_Name.Text;
            globe_setting.Maufacture = this.Maufacturer.Text;
            globe_setting.Operator = this.Operator.Text;
            globe_setting.Test_Filename = this.test_file_name.Text;
            globe_setting.work_station = this.work_station.Text;
            globe_setting.line = this.Line_number.Text ;
            testapp.useful.XmlHelper.SerializeToXml<product_setting>(globe_setting, "setup_p.xml");
        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            /*

            MatchCollection reg = new Regex(globe_setting.sn_reg).Matches(this.metroTextBox1.Text);
            if (reg.Count == 0) return;
            metroTextBox1.Enabled = false;
            richTextBox1.Text = string.Empty;
            metroTextBox1.Enabled = false;
            globe_setting.last_sn = this.metroTextBox1.Text.Trim().Replace("\n", "").Replace("\r", "");
            label1.Text = "RUN";
            while (buf_log.TryDequeue(out _)) ;
                _buf_flog = 0;
            Stopwatch stopwatch = new Stopwatch();
            DateTime dateTime_start = DateTime.Now;
            stopwatch.Start();
            singel_where = 0;
        await  Task.Factory.StartNew(async () =>
            {

                while (singel_where==0)
                {

                    lyj.WriteLine("K_TEST");

                   await Task.Delay(150);
                }


            });
            

            int cout = 50;
            do
            {
               
                if (cout % 10 == 5) { if (stop_floag == 0) { break; } stop_floag = 0; }
                await Task.Delay(100);

            } while ( cout-- > 0);
            
            string tmp = "";
            List<testapp.mylib.testlog> log = new List<testlog>();
            StringBuilder stringBuilder = new StringBuilder();
            string pass_fail_flog = "PASS";
            string tmp2 = "";
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            if (buf_log.Count > 2)
            {
                while (buf_log.TryDequeue(out tmp))
                {
                    if (tmp.ToUpper().IndexOf("FAIL") >= 0) { pass_fail_flog = "Fail"; tmp2 = "FAIL"; } else { tmp2 = "PASS"; }
                    stringBuilder.Append(tmp+"\n");
                   //sqh.InsertRecord(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), globe_setting.last_sn, globe_setting.Test_Filename, tmp2, globe_setting.Maufacture,
                      //  globe_setting.Product_name, globe_setting.PIC, globe_setting.Operator, tmp);

                }
                

                log.Add(new testlog()
                {
                    teset_case_result = stringBuilder.ToString(),
                    test_case_description = globe_setting.Product_name + " OPEN/SHORT TEST",
                    test_case_limit_hi = "0.035mΩ",
                    test_case_limit_low = "0.00mΩ",
                    test_case_item_number = "0",
                    test_case_judge= tmp2,
                    test_case_result_unit="String",
                    test_case_test_span= (elapsedTime.TotalMilliseconds).ToString(),


                }) ;


                utility_func.testlog_save_for_smx(ref log, "../whirlpool/"+ globe_setting.Product_name, globe_setting.Product_name+"_"+ globe_setting.last_sn + ".csv",
                                                globe_setting.Operator,globe_setting.Test_Filename,globe_setting.Product_name,globe_setting.last_sn, dateTime_start.ToString("yyyy-MM-dd HH:mm:ss"),DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), tmp2);

                StringBuilder s = new StringBuilder();
                int i = 0;
                if (!File.Exists("result.csv"))
                {
                    string m, v = "time,serialno,result";


                    for (int rdi = 0; rdi < log.Count; rdi++)
                    {


                        v = v + "," + log[rdi].test_case_description.Trim() + "( " + log[rdi].test_case_limit_hi.Replace(",", "#") + "<-->" + log[rdi].test_case_limit_low.Replace(",", "#") + ")";
                        i++;

                    }
                    v = Encoding.ASCII.GetString(Encoding.Default.GetBytes(v));

                    File.AppendAllText("result.csv", v + '\n');
                }

                for (int a = 0; a < log.Count; a++)
                {
                    s.Append(log[a].test_case_result_unit + ",");
                }

                s.Remove(s.Length - 1, 1);
                s.AppendLine();
                new System.Threading.Thread(() =>
                    {
                        File.AppendAllText("result.csv", s.ToString());
                    }).Start();

               

            }
            else
            {
                this.label1.Text = "EMPTY";
                this.label1.ForeColor = Color.Red;
                pass_fail_flog = "Fail";
            }

            this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename} | Operator: {globe_setting.Operator} | last_SN: {globe_setting.last_sn}";
            if (pass_fail_flog == "Fail")
            {
                this.label1.Text = "FAIL";
                this.label1.ForeColor = Color.Red;
                globe_setting.failed += 1;


            }
            else
            {
                this.label1.Text = "PASS";
                this.label1.ForeColor = Color.LightGreen;
                globe_setting.passed += 1;
            }


            update_ui();
            metroTextBox1.Text = String.Empty;
            metroTextBox1.Enabled = true;
            metroTextBox1.Focus();
          



    */



        }

        private async void metroTextBox1_KeyDown(object sender, KeyEventArgs e)
        {

          
            if (e.KeyCode == Keys.Enter) {


                MatchCollection reg = new Regex(globe_setting.sn_reg).Matches(this.metroTextBox1.Text);
                if (reg.Count == 0) return;
                metroTextBox1.Enabled = false;
                richTextBox1.Text = string.Empty;
                metroTextBox1.Enabled = false;
                globe_setting.last_sn = this.metroTextBox1.Text.Trim().Replace("\n", "").Replace("\r", "");
                label1.Text = "RUN";
                while (buf_log.TryDequeue(out _)) ;
                _buf_flog = 0;
                Stopwatch stopwatch = new Stopwatch();
                DateTime dateTime_start = DateTime.Now;
                stopwatch.Start();
                singel_where = 0;
                await Task.Factory.StartNew(async () =>
                {
                    int z = 10;
                    while (singel_where == 0&& z-->0)
                    {

                        lyj.WriteLine("K_TEST");

                        await Task.Delay(250);
                    }


                });


                int cout = 50;
                do
                {

                    if (cout % 10 == 5) { if (stop_floag == 0) { break; } stop_floag = 0; }
                    await Task.Delay(100);

                } while (cout-- > 0);

                string tmp = "";
                List<testapp.mylib.testlog> log = new List<testlog>();
                List<testapp.mylib.testlog> log2 = new List<testlog>();
                StringBuilder stringBuilder = new StringBuilder();
                string pass_fail_flog = "PASS";
                string tmp2 = "PASS";
                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;
                if (buf_log.Count > 2)
                {
                    while (buf_log.TryDequeue(out tmp))
                    {
                        if (tmp.ToUpper().IndexOf("FAIL") >= 0) { pass_fail_flog = "Fail"; tmp2 = "FAIL"; } 
                        stringBuilder.Append(tmp + "\n");
                        //sqh.InsertRecord(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), globe_setting.last_sn, globe_setting.Test_Filename, tmp2, globe_setting.Maufacture,
                        //  globe_setting.Product_name, globe_setting.PIC, globe_setting.Operator, tmp);

                    }

                    if (1 == 0) { 
                    log.Add(new testlog()
                    {
                        teset_case_result = stringBuilder.ToString(),
                        test_case_description = globe_setting.Product_name + " OPEN/SHORT TEST",
                        test_case_limit_hi = "NA",
                        test_case_limit_low = "NA",
                        test_case_item_number = "0",
                        test_case_judge = tmp2,
                        test_case_result_unit = "String",
                        test_case_test_span = (elapsedTime.TotalMilliseconds).ToString(),


                    });

                    for (int gi = 1; gi < 15; gi++)
                    {
                        log.Add(new testlog()
                        {
                            teset_case_result = "NA",
                            test_case_description = " NA",
                            test_case_limit_hi = "NA",
                            test_case_limit_low = "NA",
                            test_case_item_number = "" + gi,
                            test_case_judge = "NA",
                            test_case_result_unit = "NA",
                            test_case_test_span = "NA",


                        });

                    }
                    }
                    string[] roin_rsus = stringBuilder.ToString().Split("\n".ToArray());
                    int cout_tmp = 0;
                    foreach (var item in cableTests)
                    {
                       
                        foreach (string rsu in roin_rsus)
                        {
                            Match match = Regex.Match(rsu, item.test_reg_str);

                            //if  ( rsu.IndexOf("(X)")>=0 &&  item.test_reg_str == @"\d{3}\sCond:\s[A-Z][0-9]{2}-\s[A-Z][0-9]{2}:\s(\d*\.?\d+)Ω\.{2，}\(X\)") {



                            //    Match VVV = Regex.Match(rsu, @"\d{3}\sCond:\s[A-Z][0-9]{2}-\s[A-Z][0-9]{2}:\s(\d*\.?\d+)Ω\s\.{2,}\(X\)");

                            //}

                            if (match.Success)
                            {

                                string value = match.Groups[1].Value;
                                if (item.is_digit == "true")
                                {

                                    double v = double.Parse(value);

                                    if (v >= double.Parse(item.limit_low) && v <= double.Parse(item.limit_hi))
                                    {

                                        log.Add(new testlog()
                                        {

                                            teset_case_result = value,
                                            test_case_description = item.test_disp,
                                            test_case_limit_hi = item.limit_hi,
                                            test_case_limit_low = item.limit_low,
                                            test_case_item_number = cout_tmp++ + "",
                                            test_case_judge = "pass",
                                            test_case_result_unit = "value",
                                            test_case_test_span = "1ms",


                                        });


                                    }
                                    else
                                    {

                                        if (roin_rsus.IndexOf("(X)") >= 0)
                                        {

                                            log.Add(new testlog()
                                            {

                                                teset_case_result = value,
                                                test_case_description = rsu,
                                                test_case_limit_hi = item.limit_hi,
                                                test_case_limit_low = item.limit_low,
                                                test_case_item_number = cout_tmp++ + "",
                                                test_case_judge = "fail",
                                                test_case_result_unit = "value",
                                                test_case_test_span = "1ms",


                                            });

                                        }
                                        else {
                                            log.Add(new testlog()
                                            {
                                                teset_case_result = value,
                                                test_case_description = rsu,
                                                test_case_limit_hi = item.limit_hi,
                                                test_case_limit_low = item.limit_low,
                                                test_case_item_number = cout_tmp++ + "",
                                                test_case_judge = "fail",
                                                test_case_result_unit = "string",
                                                test_case_test_span = "1ms",


                                            });
                                        }

                             


                                    }

                                }

                                else
                                {


                                    if (value == item.limit_hi)
                                    {

                                        log.Add(new testlog()
                                        {
                                            teset_case_result = value,
                                            test_case_description = item.test_disp,
                                            test_case_limit_hi = item.limit_hi,
                                            test_case_limit_low = item.limit_low,
                                            test_case_item_number = cout_tmp++ + "",
                                            test_case_judge = "pass",
                                            test_case_result_unit = "string",
                                            test_case_test_span = "1ms",


                                        });


                                    }
                                    else
                                    {

                                        log.Add(new testlog()
                                        {
                                            teset_case_result = value,
                                            test_case_description = item.test_disp,
                                            test_case_limit_hi = item.limit_hi,
                                            test_case_limit_low = item.limit_low,
                                            test_case_item_number = cout_tmp++ + "",
                                            test_case_judge = "fail",
                                            test_case_result_unit = "string",
                                            test_case_test_span = "1ms",


                                        });



                                    }




                                }

                                break;
                            }
                        }

                    }
                    int log_cout= log.Count;
                    if (log_cout <= 15)
                    {
                        
                        for (int lct = 0; lct < 15 - log_cout; lct++) {

                            log.Add(new testlog()
                            {
                                teset_case_result = "NA",
                                test_case_description = " NA",
                                test_case_limit_hi = "NA",
                                test_case_limit_low = "NA",
                                test_case_item_number = "" + (log_cout + lct),
                                test_case_judge = "NA",
                                test_case_result_unit = "NA",
                                test_case_test_span = "NA",


                            });

                        }

                    }
                    else if (log.Count > 15) {


                        log.RemoveRange(15, log.Count - 15);

                    }

                    log2.Add(new testlog()
                    {
                        teset_case_result = stringBuilder.ToString(),
                        test_case_description = globe_setting.Product_name + " Test_Description",
                        test_case_limit_hi = "NA",
                        test_case_limit_low = "NA",
                        test_case_item_number = "0",
                        test_case_judge = tmp2,
                        test_case_result_unit = "String",
                        test_case_test_span = (elapsedTime.TotalMilliseconds).ToString(),


                    });
          
                    utility_func.testlog_new_save_for_smx(ref log, $"../{globe_setting.customer.Split(" ".ToArray())[0]}/" + globe_setting.Product_name, DateTime.Now.ToString("yyyyMMddhhmmss") + "_" + globe_setting.Product_name + "_" + globe_setting.last_sn + "_"+ (tmp2== "FAIL" ? "Failed":"Passed")+ ".csv",
                                                    globe_setting.Operator, globe_setting.line, globe_setting.work_station, globe_setting.Product_name, globe_setting.last_sn, dateTime_start.ToString("yyyy-MM-dd HH:mm:ss"), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (tmp2 == "FAIL" ? "Failed" : "Passed"));

                    StringBuilder s = new StringBuilder();
                    int i = 0;
                    if (!File.Exists("result.csv"))
                    {
                        string m, v = "time,serialno,result";


                        for (int rdi = 0; rdi < log2.Count; rdi++)
                        {


                            v = v + "," + log2[rdi].test_case_description.Trim() + "( " + log2[rdi].test_case_limit_hi.Replace(",", "#") + "<-->" + log2[rdi].test_case_limit_low.Replace(",", "#") + ")";
                            i++;

                        }
                        v = Encoding.ASCII.GetString(Encoding.Default.GetBytes(v));

                        File.AppendAllText("result.csv", v + '\n');
                    }

                    s.Append(DateTime.Now.ToString() + "," + globe_setting.last_sn + "," + tmp2 + ",");
                    for (int a = 0; a < log2.Count; a++)
                    {
                        s.Append(log2[a].teset_case_result.Replace("\n",",").Replace("\r","").Replace("Ω", "Ohm") + ",");
                    }

                    s.Remove(s.Length - 1, 1);
                    s.AppendLine();
                    new System.Threading.Thread(() =>
                    {
                        File.AppendAllText("result.csv", s.ToString());
                    }).Start();



                }
                else
                {
                    this.label1.Text = "EMPTY";
                    this.label1.ForeColor = Color.Red;
                    pass_fail_flog = "Fail";
                }

                this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename} | Operator: {globe_setting.Operator} | last_SN: {globe_setting.last_sn}";
                if (pass_fail_flog == "Fail")
                {
                    this.label1.Text = "FAIL";
                    this.label1.ForeColor = Color.Red;
                    globe_setting.failed += 1;


                }
                else
                {
                    this.label1.Text = "PASS";
                    this.label1.ForeColor = Color.LightGreen;
                    globe_setting.passed += 1;
                }


                update_ui();
                metroTextBox1.Text = String.Empty;
                metroTextBox1.Enabled = true;
                metroTextBox1.Focus();


            }
        }

        private void monitoring1_KeyDown(object sender, KeyEventArgs e)
        {
           
        }


     


        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as MetroTabControl).SelectedTab== metroTabPage1) {
                if (globe_setting == null) return;
                this.label20.Text = $"P/N:{globe_setting.Product_name}";
                this.label14.Text = $"Test_Filename: {globe_setting.Test_Filename}| Operator: {globe_setting.Operator}| last_SN: {globe_setting.last_sn}| Work_Station:{globe_setting.work_station}| Line:{globe_setting.line}";

            }
        }






        private void whirlpool2_Shown(object sender, EventArgs e)
        {
            FlashWindow(Handle, true);
            metroButton1.Enabled = false;
            metroTextBox1.Enabled = false;
            lyj = new luoyinji(globe_setting.COM_PORT.Split(';')[0]);
            lyj.get_msg = (o) =>
            {

                this.Invoke((Action)delegate
                {
                    string tmp = o.Replace("$a", "").Replace("?[", "Ω");
                    if (tmp.IndexOf("Start Test")>=0){

                        _buf_flog = 1;
                        singel_where = 1;
                        lastExecutionTime = TimeSpan.Zero;
                        stopwatch.Start();
                        frame_cout = 0;
                    }
                    stop_floag = 1;
                    TimeSpan currentTime = stopwatch.Elapsed;

                    // 计算与上一次执行的时间差
                    TimeSpan timeDifference = currentTime - lastExecutionTime;

                    // 输出时间差
                    if (timeDifference.TotalMilliseconds < 200 && tmp.IndexOf("End Test")>=0) {

                        frame_cout++;
                    };

                    // 更新上一次执行时间
                    lastExecutionTime = currentTime;

                    if (_buf_flog==1) {
                       buf_log.Enqueue(tmp);
                    }
                    string s = "";
                    if (_buf_flog == 1  && frame_cout > 1 ) {
                        
                        do
                        {


                          buf_log.TryDequeue(out s);
                        } while (s.IndexOf("End Test") > 0);

                        frame_cout--;             
                    }



                    buf.Enqueue(tmp);
                    if (buf.Count > 500)
                    {
                        int itemsToRemove = buf.Count - 300;
                        string[] itemsToRemoveArray = new string[itemsToRemove];
                        // 移除要保留的元素之前的元素
                        for (int i = 0; i < itemsToRemove; i++)
                        {
                            buf.TryDequeue(out itemsToRemoveArray[i]);
                        }
                    }
                    this.richTextBox1.AppendText(tmp + "\n");
                    this.richTextBox1.ScrollToCaret();
                });

            };
            are = new AutoResetEvent(true);
            timer1.Start();
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if (are.WaitOne(0))
            {
                label19.Text = $"Current document NOT Found";
                label19.ForeColor = Color.Red;
                Task.Factory.StartNew(() => {  
                // buf.Clear();
                lyj.WriteLine("K_ESC");
                     Task.Delay(100).Wait();
                    lyj.WriteLine("K_ESC");
                    Task.Delay(150).Wait();
                    lyj.WriteLine("K_TEST");
                string tmp = "";
                    if (buf.Count > 0) {

                        for (int i = 0; i < buf.Count; i++) {

                            buf.TryDequeue(out tmp);
                            if (tmp.IndexOf("File:") >= 0) {
                              
                               // while (buf_log.TryDequeue(out _))

                                 timer1.Enabled = false;
                                filename = tmp;
                                this.Invoke(new Action(()=> {

                                    label19.Text = $"Current document is :{filename.Substring(5)}";
                                    if (filename.Substring(5) != globe_setting.Test_Filename)
                                    {
                                        label19.Text = $"Current document is :{filename.Substring(5)},It should be:{globe_setting.Test_Filename},It is incorrect. ";
                                        label19.ForeColor = Color.Red;
                                        //   MessageBox.Show(" Current document ERROR,  app is about to exit ");
                                        // Application.Exit();
                                    }
                                    else {

                                        label19.ForeColor = Color.Green;
                                        metroButton1.Enabled = true;
                                        metroTextBox1.Enabled = true;
                                    }
                                }));
                               
                                
                                break;
                            }
                        }
                    
                    }
                  
                    are.Set();
                });

            }

        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
           
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            //// 根据需要提供数据行的值
            //// 这里可以根据具体的逻辑从数据源中获取数据
            //object value = GetDataValue(rowIndex, columnIndex);

            //// 将值赋给e.Value
            //e.Value = value;
        }

        private void roundButton3_Click(object sender, EventArgs e)
        {

        }

        private void save_config(List<setup_cable_test> cableTests) {


            XmlSerializer serializer = new XmlSerializer(typeof(List<setup_cable_test>));
            using (FileStream fileStream = new FileStream("wire_testcase_cofig.xml", FileMode.Create))
            {
                serializer.Serialize(fileStream, cableTests);
            }
        }

        private void get_wire_config() {

            if (!File.Exists("wire_testcase_cofig.xml")) {
                cableTests.Add(new setup_cable_test()
                {
                    test_disp = "Cond: A01 - A02 test",
                    test_reg_str = @"Cond: A01 - A02: (\d+.\d+)",
                    limit_hi = "0.5",
                    limit_low = "0.0",
                    is_digit="true"
                });


                save_config(cableTests);

            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<setup_cable_test>));

            using (FileStream fileStream = new FileStream("wire_testcase_cofig.xml", FileMode.Open))
            {
               cableTests = (List<setup_cable_test>)serializer.Deserialize(fileStream);
            }

        }
    }
    
    public class setup_cable_test

    {
        [XmlAttribute]
        public string test_disp { get; set; }
        [XmlAttribute]
        public string test_reg_str { get; set; }
        [XmlAttribute]
        public string limit_hi { get; set; }
        [XmlAttribute]
        public string limit_low { get; set; }
        [XmlAttribute]
        public string is_digit { get; set; }
    }
}
