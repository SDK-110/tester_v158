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

namespace testapp
{
    public delegate void set_test_item_text_event(string item,  string[] test_result);
    public partial class hayco : Form
    {
       
        public hayco()
        {
            InitializeComponent();
        }

   

        public void test_item_init() {

            this.radioButton1.ForeColor =  Color.Black;
            this.radioButton2.ForeColor =  Color.Black;
            this.radioButton3.ForeColor =  Color.Black;
            this.radioButton4.ForeColor =  Color.Black;
            this.radioButton5.ForeColor =  Color.Black;
            this.radioButton6.ForeColor =  Color.Black;
            this.radioButton7.ForeColor =  Color.Black;
            this.radioButton8.ForeColor =  Color.Black;
            this.radioButton9.ForeColor =  Color.Black;
            this.radioButton10.ForeColor = Color.Black;
            this.radioButton11.ForeColor = Color.Black;
            this.radioButton12.ForeColor = Color.Black;
            this.radioButton13.ForeColor = Color.Black;
            this.radioButton14.ForeColor = Color.Black;
            this.radioButton15.ForeColor = Color.Black;
            this.radioButton16.ForeColor = Color.Black;



        }


        public void set_test_item_text(string  item, string[] test_result) {

            switch (item)
            {
                case "1asfsd dsfadsfds":

                    {
                        if (test_result[0].ToUpper().IndexOf("OK")>=0)
                        {

                            this.radioButton1.ForeColor = Color.LightGreen;

                        }
                        else {

                            this.radioButton1.ForeColor = Color.Red;
                        }
                        if (test_result[1].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton8.ForeColor = Color.LightGreen;

                        }
                        else
                        {
                            this.radioButton8.ForeColor = Color.Red;

                        }
                        if (test_result[2].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton12.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton12.ForeColor = Color.Red;
                        }
                        if (test_result[3].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton16.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton16.ForeColor = Color.Red;
                        }
                        this.Invoke((Action)delegate
                        {
                            this.radioButton1.Text = $"静态电流测试 结果:{test_result[0]}";
                            this.radioButton8.Text = $"静态电流测试 结果:{test_result[1]}";
                            this.radioButton12.Text = $"静态电流测试 结果:{test_result[2]}";
                            this.radioButton16.Text = $"静态电流测试 结果:{test_result[3]}";
                        });
                    }
                 break;
                case "2asfsd dsfadsfds":

                    {
                        if (test_result[0].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton2.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton2.ForeColor = Color.Red;
                        }
                        if (test_result[1].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton7.ForeColor = Color.LightGreen;

                        }
                        else
                        {
                            this.radioButton7.ForeColor = Color.Red;

                        }
                        if (test_result[2].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton11.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton11.ForeColor = Color.Red;
                        }
                        if (test_result[3].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton15.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton15.ForeColor = Color.Red;
                        }
                        this.Invoke((Action)delegate
                        {

                            this.radioButton2.Text = $"马达负载电流测试 结果:{test_result[0]}";
                            this.radioButton7.Text = $"马达负载电流测试 结果:{test_result[1]}";
                            this.radioButton11.Text = $"马达负载电流测试 结果:{test_result[2]}";
                            this.radioButton15.Text = $"马达负载电流测试 结果:{test_result[3]}";
                        });
                       
                    }
                    break;
                case "3asfsd dsfadsfds":

                    {
                        if (test_result[0].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton3.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton3.ForeColor = Color.Red;
                        }
                        if (test_result[1].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton6.ForeColor = Color.LightGreen;

                        }
                        else
                        {
                            this.radioButton6.ForeColor = Color.Red;

                        }
                        if (test_result[2].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton10.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton10.ForeColor = Color.Red;
                        }
                        if (test_result[3].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton14.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton14.ForeColor = Color.Red;
                        }
                        this.Invoke((Action)delegate
                        {
                            this.radioButton3.Text = $"LED_5V电压测试 结果:{test_result[0]}";
                            this.radioButton6.Text = $"LED_5V电压测试 结果:{test_result[1]}";
                            this.radioButton10.Text = $"LED_5V电压测试 结果:{test_result[2]}";
                            this.radioButton14.Text = $"LED_5V电压测试 结果:{test_result[3]}";
                        });
                    }
                    break;

                case "4asfsd dsfadsfds":

                    {
                        if (test_result[0].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton4.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton4.ForeColor = Color.Red;
                        }
                        if (test_result[1].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton5.ForeColor = Color.LightGreen;

                        }
                        else
                        {
                            this.radioButton5.ForeColor = Color.Red;

                        }
                        if (test_result[2].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton9.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton9.ForeColor = Color.Red;
                        }
                        if (test_result[3].ToUpper().IndexOf("OK") >= 0)
                        {

                            this.radioButton13.ForeColor = Color.LightGreen;

                        }
                        else
                        {

                            this.radioButton13.ForeColor = Color.Red;
                        }
                        this.Invoke((Action)delegate
                        {
                            this.radioButton4.Text = $"3秒后LED熄灭测试 结果:{test_result[0]}";
                            this.radioButton5.Text = $"3秒后LED熄灭测试 结果:{test_result[1]}";
                            this.radioButton9.Text = $"3秒后LED熄灭测试 结果:{test_result[2]}";
                            this.radioButton13.Text = $"3秒后LED熄灭测试 结果:{test_result[3]}";
                        });
                    }
                    break;


            }
           


        }


        private void hayco_load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string[] TT = { "OK", "OK", "NG", "OK" };
            //set_test_item_text(0, ref TT);

            new Task(() => {


                test_log_tab a = new test_log_tab();
                test_log_tab.start_test_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                a.item = "1asfsd dsfadsfds";
                a.dtt = set_test_item_text;
                System.Threading.Thread.Sleep(5000);
                a.test_result = new string[] { "NG", "OK", "NG", "OK" };

                a[a.item] = a;
              
                a.item = "2asfsd dsfadsfds";
                a.dtt = set_test_item_text;
                System.Threading.Thread.Sleep(5000);
                a.test_result = new string[] { "NG[123]", "NG[4444]", "NG[555]", "OK[6666]" };
               
                a[a.item] = a;
                a.item = "3asfsd dsfadsfds";
                a.dtt = set_test_item_text;
                System.Threading.Thread.Sleep(5000);
                a.test_result = new string[] { "NG", "OK", "NG", "OK" };
              
                a[a.item] = a;
                
                a.item = "4asfsd dsfadsfds";
                a.dtt = set_test_item_text;
                System.Threading.Thread.Sleep(5000);
                a.test_result = new string[] { "NG", "NG", "NG", "OK" };

                a[a.item] = a;
                test_log_tab.end_test_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                a.save_log(ref a);




            }).Start();


            


        }
    }

  public  class test_log_tab {
        public  set_test_item_text_event dtt = null;
        string _item = "";
        string []_test_result =  new string[4] {"null","null","null","null" };
        string []_test_result_valu = new string[4] { "null", "null", "null", "null" };
        double span_start = DateTime.Now.Ticks;
       static public  string start_test_time = "";
        static public  string end_test_time = "";
        string[] _last_result = { "OK", "OK" ,"OK","OK"};
        public  double time_span => span_start;
        public string item {
            get {
                return _item;
            }

            set {
                _item = value;
               span_start = DateTime.Now.Ticks;
            }
        }

        public string [] test_result {

            get {

                return _test_result;
            }
            set {



                _test_result = value;

                span_start = (DateTime.Now.Ticks - span_start) / 10000000.000000;

            }

        }
      public   static   Dictionary<string, test_log_tab> tm;
        public test_log_tab()
        {
            if (tm != null) return;
            tm = new Dictionary<string, test_log_tab>();
            


        }


       public test_log_tab  this[string index] {

            get {

                return tm[index];
            }
            set {

                if (dtt != null) {

                    dtt(value.item, value.test_result);
                    if (value.test_result[0].ToUpper().IndexOf("NG") >=0)  _last_result[0] =   value.test_result[0].ToUpper();
                    if (value.test_result[1].ToUpper().IndexOf("NG") >= 0) _last_result[1] = value.test_result[1].ToUpper();
                    if (value.test_result[2].ToUpper().IndexOf("NG") >= 0) _last_result[2] = value.test_result[2].ToUpper();
                    if (value.test_result[3].ToUpper().IndexOf("NG") >= 0) _last_result[3] = value.test_result[3].ToUpper();
                }
                tm[index] = value;
            }

        }

        public void save_log(ref test_log_tab result ) {

            utility_func.testlog_save_for_hayco(ref result, "", "123456.csv", "S3302", "HAYCO", "NA", start_test_time, end_test_time, _last_result[0]+"|"+ _last_result[1] + "|"  + _last_result[2] + "|" + _last_result[3] );
          
           

        }
            
    }
}
