using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.mylib;
using System.IO;
namespace testapp
{
    public partial class pycom_form1 : Form
    {


        string[] testcase;

    

        int i = 0;
        public pycom_form1()
        {

            testcase = File.ReadAllLines("pycom_ini.txt");
            InitializeComponent();
            
            this.listView1.Columns.Add("测试项目");
            this.listView1.Columns[0].TextAlign = HorizontalAlignment.Center;
            this.listView1.View = System.Windows.Forms.View.Details;
            listView1.Columns[0].Width = this.listView1.Width ;

            this.listView2.Columns.Add("测试项目");
            this.listView2.Columns[0].TextAlign = HorizontalAlignment.Center;
            this.listView2.View = System.Windows.Forms.View.Details;
            listView2.Columns[0].Width = this.listView2.Width;


            this.listView3.Columns.Add("测试项目");
            this.listView3.Columns[0].TextAlign = HorizontalAlignment.Center;
            this.listView3.View = System.Windows.Forms.View.Details;
             listView3.Columns[0].Width = this.listView3.Width;

            this.listView4.Columns.Add("测试项目");
            this.listView4.Columns[0].TextAlign = HorizontalAlignment.Center;
            this.listView4.View = System.Windows.Forms.View.Details;

            listView4.Columns[0].Width = this.listView4.Width;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightGray;

            StringBuilder rsult_str = new StringBuilder();
            rsult_str.AppendLine("Start Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            rsult_str.AppendLine("Test Item,Test description, Rusult,Test Value");
            button1.Text = "DUT1";
            this.listView1.Items.Clear();
            button1.Enabled = false;
            int i = 0;
            int count = 0;
            new Task(() =>
            {


               

                pip_run_jiaohu a = new pip_run_jiaohu("dut1.cmd", "",(o)=> {


                    if (i >= testcase.Count()) return;
                    string rsg = testcase[i].Split(";".ToArray())[1];
                    set_viewlist1_test_text(o, "MSG", "");
                    string reg_rsult =   utility_func.findstr_regex(rsg,o);
                    if (reg_rsult != "null") {
                        rsult_str.AppendLine( i + "," + testcase[i].Split(";".ToArray())[0] + ",pass," + reg_rsult);
                        set_viewlist1_test_text(testcase[i].Split(";".ToArray())[0], "pass", reg_rsult,i);
                        i++;
                    }
                   

                   
                });

                do {

                    System.Threading.Thread.Sleep(1000);


                }
                while (a.p_exit != 1);
                if (i < testcase.Count())
                {
                    set_viewlist1_test_text(testcase[i].Split(";".ToArray())[0], "fail", "", i);
                    rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",fail," + "NA");
                    rsult_str.AppendLine("Test Result:,FAILED");
                }
                else {


                    rsult_str.AppendLine("Test Result:,PASSED");
                }


                this.Invoke((Action)delegate ()
                {

                    if (i < testcase.Count())
                    {

                        button1.BackColor = Color.Red;
                        button1.Text = "DUT1_FAIL";
                    }
                    else {

                        button1.BackColor = Color.LightGreen;
                        button1.Text = "DUT1_PASS";
                    }
                    button1.Enabled = true;
                    rsult_str.AppendLine("End Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    utility_func.testlog_save_for_pycom(ref rsult_str, "", "DUT1" + "Testlog.csv");

                });

            }).Start();


         
           
     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = Color.LightGray;

            StringBuilder rsult_str = new StringBuilder();
            rsult_str.AppendLine("Start Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            rsult_str.AppendLine("Test Item,Test description, Rusult,Test Value");
            button2.Text = "DUT2";
            this.listView2.Items.Clear();
            button2.Enabled = false;
            int i = 0;
            int count = 0;
            new Task(() =>
            {




                pip_run_jiaohu a = new pip_run_jiaohu("dut2.cmd", "", (o) => {
                    if (i >= testcase.Count()) return;
                    string rsg = testcase[i].Split(";".ToArray())[1];
                    set_viewlist2_test_text(o, "MSG", "");
                    string reg_rsult = utility_func.findstr_regex(rsg, o);
                    if (reg_rsult != "null")
                    {
                        rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",pass," + reg_rsult);
                        set_viewlist2_test_text(testcase[i].Split(";".ToArray())[0], "pass", reg_rsult, i);
                        i++;
                    }



                });

                do
                {

                    System.Threading.Thread.Sleep(1000);


                }
                while (a.p_exit != 1);
            if (i < testcase.Count())
            {
                rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",fail," + "NA");
                rsult_str.AppendLine("Test Result:,FAILED");
                set_viewlist2_test_text(testcase[i].Split(";".ToArray())[0], "fail", "", i);
                }
                else
                {


                    rsult_str.AppendLine("Test Result:,PASSED");
                }



                this.Invoke((Action)delegate ()
                {

                    if (i < testcase.Count())
                    {

                        button2.BackColor = Color.Red;
                        button2.Text = "DUT2_FAIL";
                    }
                    else
                    {

                        button2.BackColor = Color.LightGreen;
                        button2.Text = "DUT2_PASS";
                    }
                    button2.Enabled = true;
                    rsult_str.AppendLine("End Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    utility_func.testlog_save_for_pycom(ref rsult_str, "", "DUT2" + "Testlog.csv");
                });

            }).Start();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = Color.LightGray;
            StringBuilder rsult_str = new StringBuilder();
            rsult_str.AppendLine("Start Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            rsult_str.AppendLine("Test Item,Test description, Rusult,Test Value");

            button3.Text = "DUT3";
            this.listView3.Items.Clear();
            button3.Enabled = false;
            int i = 0;
            int count = 0;
            new Task(() =>
            {




                pip_run_jiaohu a = new pip_run_jiaohu("dut3.cmd", "", (o) => {
                    if (i >= testcase.Count()) return;
                    string rsg = testcase[i].Split(";".ToArray())[1];
                    set_viewlist3_test_text(o, "MSG", "");
                    string reg_rsult = utility_func.findstr_regex(rsg, o);
                    if (reg_rsult != "null")
                    {
                        rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",pass," + reg_rsult);
                        set_viewlist3_test_text(testcase[i].Split(";".ToArray())[0], "pass", reg_rsult, i);
                        i++;
                    }



                });

                do
                {

                    System.Threading.Thread.Sleep(1000);


                }
                while (a.p_exit != 1);
            if (i < testcase.Count())
            {
                rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",fail," + "NA");
                rsult_str.AppendLine("Test Result:,FAILED");
                set_viewlist3_test_text(testcase[i].Split(";".ToArray())[0], "fail", "", i);
                }
                else
                {


                    rsult_str.AppendLine("Test Result:,PASSED");
                }


                this.Invoke((Action)delegate ()
                {

                    if (i < testcase.Count())
                    {

                        button3.BackColor = Color.Red;
                        button3.Text = "DUT3_FAIL";
                    }
                    else
                    {

                        button3.BackColor = Color.LightGreen;
                        button3.Text = "DUT3_PASS";
                    }
                    button3.Enabled = true;
                    rsult_str.AppendLine("End Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    utility_func.testlog_save_for_pycom(ref rsult_str, "", "DUT3" + "Testlog.csv");
                });

            }).Start();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.BackColor = Color.LightGray;

            StringBuilder rsult_str = new StringBuilder();
            rsult_str.AppendLine("Start Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            rsult_str.AppendLine("Test Item,Test description, Rusult,Test Value");

            button4.Text = "DUT4";
            this.listView4.Items.Clear();
            button4.Enabled = false;
            int i = 0;
            int count = 0;
            new Task(() =>
            {




                pip_run_jiaohu a = new pip_run_jiaohu("dut4.cmd", "", (o) => {
                    if (i >= testcase.Count()) return;
                    string rsg = testcase[i].Split(";".ToArray())[1];
                    set_viewlist1_test_text(o, "MSG", "");
                    string reg_rsult = utility_func.findstr_regex(rsg, o);
                    if (reg_rsult != "null")
                    {
                        rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",pass," + reg_rsult);
                        set_viewlist1_test_text(testcase[i].Split(";".ToArray())[0], "pass", reg_rsult, i);
                        i++;
                    }



                });

                do
                {

                    System.Threading.Thread.Sleep(1000);


                }
                while (a.p_exit != 1);
                if (i < testcase.Count())
                {
                    rsult_str.AppendLine(i + "," + testcase[i].Split(";".ToArray())[0] + ",fail," + "NA");
                    rsult_str.AppendLine("Test Result:,FAILED");
                    set_viewlist4_test_text(testcase[i].Split(";".ToArray())[0], "fail", "", i);
                }
                else
                {


                    rsult_str.AppendLine("Test Result:,PASSED");
                }


                    this.Invoke((Action)delegate ()
                {

                    if (i < testcase.Count())
                    {

                        button4.BackColor = Color.Red;
                        button4.Text = "DUT4_FAIL";
                    }
                    else
                    {

                        button1.BackColor = Color.LightGreen;
                        button1.Text = "DUT4_PASS";
                    }
                    button4.Enabled = true;
                    rsult_str.AppendLine("End Time:" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    utility_func.testlog_save_for_pycom(ref rsult_str, "", "DUT3" + "Testlog.csv");
                });

            }).Start();

        }


        private void Test_Form1_ResizeEnd(object sender, EventArgs e)
        {
          
        }

        private void Test_Form1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Columns[0].Width = this.listView1.Width;
            listView2.Columns[0].Width = this.listView2.Width;
            listView3.Columns[0].Width = this.listView3.Width;
            listView4.Columns[0].Width = this.listView4.Width;
        }

        public void set_viewlist1_test_text(string content, string pass_fail,  string testvalue, int tb=0)
        {


            this.Invoke((Action)delegate ()
            {

                switch( pass_fail.ToUpper() )

                {
                    case "FAIL":

                        {
                            this.listView1.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView1.Items[this.listView1.Items.Count-1].ForeColor = Color.Red;
                   
                            this.listView1.TopItem = this.listView1.Items[this.listView1.Items.Count - 1];
                        }
                        break;
                    case "PASS":
                        {
                            this.listView1.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = Color.LightGreen;

                            this.listView1.TopItem = this.listView1.Items[this.listView1.Items.Count - 1];

                        }
                        break;
                    case "MSG":
                        {

                            this.listView1.Items.Add(("  >>>" + " MSG:[" + content +  "]").PadRight(30, ' '));
                            this.listView1.Items[this.listView1.Items.Count - 1].ForeColor = Color.Black;
                            this.listView1.TopItem = this.listView1.Items[this.listView1.Items.Count - 1];


                        }
                        break;
                }



            });




        }




        public void set_viewlist2_test_text(string content, string pass_fail, string testvalue, int tb = 0)
        {


            this.Invoke((Action)delegate ()
            {

                switch (pass_fail.ToUpper())

                {
                    case "FAIL":

                        {
                            this.listView2.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView2.Items[this.listView2.Items.Count - 1].ForeColor = Color.Red;

                            this.listView2.TopItem = this.listView2.Items[this.listView2.Items.Count - 1];
                        }
                        break;
                    case "PASS":
                        {
                            this.listView2.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView2.Items[this.listView2.Items.Count - 1].ForeColor = Color.LightGreen;

                            this.listView2.TopItem = this.listView2.Items[this.listView2.Items.Count - 1];

                        }
                        break;
                    case "MSG":
                        {

                            this.listView2.Items.Add((">>>" + " MSG:[" + content + "]").PadRight(30, ' '));
                            this.listView2.Items[this.listView2.Items.Count - 1].ForeColor = Color.Black;
                            this.listView2.TopItem = this.listView2.Items[this.listView2.Items.Count - 1];


                        }
                        break;
                }



            });

        }

        public void set_viewlist3_test_text(string content, string pass_fail, string testvalue, int tb = 0)
        {


            this.Invoke((Action)delegate ()
            {

                switch (pass_fail.ToUpper())

                {
                    case "FAIL":

                        {
                            this.listView3.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView3.Items[this.listView3.Items.Count - 1].ForeColor = Color.Red;

                            this.listView3.TopItem = this.listView3.Items[this.listView3.Items.Count - 1];
                        }
                        break;
                    case "PASS":
                        {
                            this.listView3.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView3.Items[this.listView3.Items.Count - 1].ForeColor = Color.LightGreen;

                            this.listView3.TopItem = this.listView3.Items[this.listView3.Items.Count - 1];

                        }
                        break;
                    case "MSG":
                        {

                            this.listView3.Items.Add((">>>" + " MSG:[" + content + "]").PadRight(30, ' '));
                            this.listView3.Items[this.listView3.Items.Count - 1].ForeColor = Color.Black;
                            this.listView3.TopItem = this.listView3.Items[this.listView3.Items.Count - 1];


                        }
                        break;
                }



            });

        }

        public void set_viewlist4_test_text(string content, string pass_fail, string testvalue, int tb = 0)
        {


            this.Invoke((Action)delegate ()
            {

                switch (pass_fail.ToUpper())

                {
                    case "FAIL":

                        {
                            this.listView4.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView4.Items[this.listView4.Items.Count - 1].ForeColor = Color.Red;

                            this.listView4.TopItem = this.listView4.Items[this.listView4.Items.Count - 1];
                        }
                        break;
                    case "PASS":
                        {
                            this.listView4.Items.Add((tb + "." + content + "result:[" + testvalue + "]").PadRight(30, '-') + pass_fail);
                            this.listView4.Items[this.listView4.Items.Count - 1].ForeColor = Color.LightGreen;

                            this.listView4.TopItem = this.listView4.Items[this.listView4.Items.Count - 1];

                        }
                        break;
                    case "MSG":
                        {

                            this.listView4.Items.Add((">>>" + " MSG:[" + content + "]").PadRight(30, ' '));
                            this.listView4.Items[this.listView4.Items.Count - 1].ForeColor = Color.Black;
                            this.listView4.TopItem = this.listView4.Items[this.listView4.Items.Count - 1];


                        }
                        break;
                }



            });
        }
            private void 一起测_Click(object sender, EventArgs e)
        {
            this.button1.PerformClick();
            this.button2.PerformClick();
            this.button3.PerformClick();
            this.button4.PerformClick();
        }
    }
}
