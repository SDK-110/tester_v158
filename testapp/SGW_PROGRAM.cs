using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace testapp
{
    public partial class SGW_PROGRAM : Form
    {
        object _lock = new object();

        testcase_dll control;

        private object obj = new object();
        volatile int m = 0;
        int delay = 300;
        volatile int exit_flog = 0, exit_flog1 = 0, exit_flog2 = 0, exit_flog3 = 0, exit_flog4 = 0;
        string pattern =System.IO.File.ReadAllText("prog_pattern.txt").Trim();
        private string _step="step2_ok";
        private string _step2 = "step3_ok";
        volatile int  dut1=0, dut2 = 0, dut3 = 0, dut4 = 0;
        volatile int dut1_1 = 0, dut2_1 = 0, dut3_1 = 0, dut4_1 = 0;
        string run_path = "";
        public SGW_PROGRAM()
        {
            InitializeComponent();
            Task.Factory.StartNew(() =>
            {

                control = new testcase_dll();
            });

            run_path = System.AppDomain.CurrentDomain.BaseDirectory;
        }

        private void SGW_PROGRAM_Load(object sender, EventArgs e)
        {
              var iniread = new IniParser.FileIniDataParser().ReadFile("setup.ini")["setproduct"]["project"];
            this.Text = iniread;
    }

        private async void roundButton1_Click(object sender, EventArgs e)
        {

          

        

            string filepath = "./DUT1_BUF.txt";

                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);

                if (false == Regex.IsMatch(this.textBox1.Text, pattern))
                {
                    this.label1.BackColor = Color.DarkRed;
                    this.label1.Text = "SN ERROR";
                    return;
                }
            try
            {

                this.label5.Text = $"SN:{this.textBox1.Text}";
                this.roundedButton1.Enabled = false;
                this.lTrackBar1.L_SliderColor = Color.LightSeaGreen;
                this.lTrackBar1.L_Value = 0;
                this.label1.BackColor = Color.Transparent;
                this.label1.Text = "Running";

                exit_flog1 = 0;
                this.textBox2.Text = "";
                set_control("#1,2:1#");

                await Task.Delay(200);
                set_control("#3:1#");
                await Task.Delay(200);
               // mylib.utility_func.WinExec(run_path + "FW_PRGO_SCRIPT/F01S3/dut1_set_espefuse_1v8_and_power_selection.cmd", 1);
                mylib.utility_func.WinExec("dut1_espfuse_burn_erase_flash.CMD", 1);
                await Task.Factory.StartNew(() =>
                 {


                     string content = "";
                     for (int i = 0; i < 100 * delay; i++)
                     {

                         if (exit_flog == 1 || exit_flog1 == 1) break;

                         try
                         {

                             this.Invoke(new Action(() =>
                             {

                                 this.lTrackBar1.L_Value += 1;
                                 if (this.lTrackBar1.L_Value == this.lTrackBar1.L_Maximum) this.lTrackBar1.L_Value = 0;
                             }));


                             content = System.IO.File.ReadAllText(filepath);

                             if (content.Length >= 3)
                             {
                                 if (content.IndexOf(_step) >= 0 && dut1 == 0 ) {
                                     dut1 = 1;
                                     set_control("#1,2,3:0#");

                                     System.Threading.Thread.Sleep(100);
                                     set_control("#1,2:1#");
                                     System.Threading.Thread.Sleep(100);
                                     set_control("#3:1#");
                                     System.Threading.Thread.Sleep(100);

                                     mylib.utility_func.WinExec("dut1_program_flash.CMD", 1);
                                 }
                                 this.Invoke(new Action(() =>
                                 {


                                     this.textBox2.Text = content;


                                 }));

                                 if (content.IndexOf(_step2) >= 0 && dut1_1 == 0)
                                 {
                                     dut1_1 = 1;
                                     set_control("#1,2,3:0#");

                                     System.Threading.Thread.Sleep(100);
                                     set_control("#3:1#");
                                     System.Threading.Thread.Sleep(4000);
                                     mylib.utility_func.WinExec("dut1_program_lte.CMD", 1);
                                 }

                             }






                             if (content.IndexOf("pass") >= 0)
                             {

                                 this.Invoke(new Action(() =>
                                 {
                                     this.label1.BackColor = Color.Green;
                                     this.label1.Text = "PASS";
                                     this.lTrackBar1.L_SliderColor = Color.Green;
                                     string rsu = this.textBox1.Text.Trim() + "," + DateTime.Now.ToString() + "," + "pass" + "," + this.textBox2.Text.Replace("\r", " ").Replace("\n", " ");
                                     test_log_save(rsu);
                                     this.textBox1.Text = "";
                                 }));
                                 
                                 exit_flog1 = 1;

                             }

                             if (content.IndexOf("fail") >= 0)
                             {
                                 this.label1.BackColor = Color.Red;
                                 this.label1.Text = "Fail";
                                 this.Invoke(new Action(() =>
                                 {

                                     this.lTrackBar1.L_SliderColor = Color.Red;
                                     string rsu = this.textBox1.Text.Trim() + "," + DateTime.Now.ToString() + "," + "fail" + "," + this.textBox2.Text.Replace("\r", " ").Replace("\n", " ");
                                     test_log_save(rsu);
                                     this.textBox1.Text = "";
                                 }));
                                 exit_flog1 = 1;
                             }



                         }
                         catch
                         {


                         }

                         System.Threading.Thread.Sleep(100);
                     }



                     this.Invoke(new Action(() =>
                     {

                         this.lTrackBar1.L_Value = this.lTrackBar1.L_Maximum;
                         this.roundedButton1.Enabled = true;
                     }));
                 });

            }
            catch (Exception ex)
            {

                this.textBox2.Text = ex.ToString();
            }
            finally {

                try
                {
                    set_control("#1,2,3:0#");
                    dut1 = 0; dut1_1 = 0;
                }
                catch { }
            }
        }

        private async void roundButton2_Click(object sender, EventArgs e)
        {

           
                string filepath = "./DUT2_BUF.txt";

                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);

                if (false == Regex.IsMatch(this.textBox4.Text, pattern))
                {
                    this.label2.BackColor = Color.DarkRed;
                    this.label2.Text = "SN ERROR";
                    return;
                }
            try
            {

                this.label6.Text = $"SN:{this.textBox4.Text}";
                this.roundedButton2.Enabled = false;
                this.lTrackBar2.L_SliderColor = Color.LightSeaGreen;
                this.lTrackBar2.L_Value = 0;
                this.label2.BackColor = Color.Transparent;
                this.label2.Text = "Running";
                exit_flog2 = 0;
                this.textBox3.Text = "";
                set_control("#4,5:1#");
                await Task.Delay(200);
                set_control("#6:1#");
                await Task.Delay(200);
                mylib.utility_func.WinExec("dut2_espfuse_burn_erase_flash.CMD", 1);
            await     Task.Factory.StartNew(() =>
                {

                    

                    string content = "";
                    for (int i = 0; i < 100 * delay; i++)
                    {

                        if (exit_flog == 1 || exit_flog2 == 1) break;

                        try
                        {

                            this.Invoke(new Action(() =>
                            {

                                this.lTrackBar2.L_Value += 1;
                                if (this.lTrackBar2.L_Value == this.lTrackBar2.L_Maximum) this.lTrackBar2.L_Value = 0;
                            }));


                            content = System.IO.File.ReadAllText(filepath);

                            if (content.Length >= 3)
                            {
                                if (content.IndexOf(_step) >= 0  && dut2==0) {
                                    dut2 = 1;
                                    set_control("#4,5,6:0#");
                                    System.Threading.Thread.Sleep(100);
                                    set_control("#4,5:1#");
                                    System.Threading.Thread.Sleep(100);
                                    set_control("#6:1#");
                                    System.Threading.Thread.Sleep(200);


                                    mylib.utility_func.WinExec("dut2_program_flash.CMD", 1);
                                }

                                if (content.IndexOf(_step2) >= 0 && dut2_1 == 0)
                                {
                                    dut2_1 = 1;
                                    set_control("#4,5,6:0#");

                                    System.Threading.Thread.Sleep(200);
                                    set_control("#6:1#");
                                    System.Threading.Thread.Sleep(4000);
                                    mylib.utility_func.WinExec("dut2_program_lte.CMD", 1);
                                }



                                this.Invoke(new Action(() =>
                                {


                                    this.textBox3.Text = content;


                                }));

                            }

                            if (content.IndexOf("pass") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label2.BackColor = Color.Green;
                                    this.label2.Text = "PASS";
                                    this.lTrackBar2.L_SliderColor = Color.Green;
                                    string rsu = this.textBox4.Text.Trim() + "," + DateTime.Now.ToString() + "," + "pass" + "," + this.textBox3.Text.Replace("\r", " ").Replace("\n", " ");
                                    test_log_save(rsu);
                                    this.textBox4.Text = "";
                                }));
                                exit_flog2 = 1;
                            }

                            if (content.IndexOf("fail") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label2.BackColor = Color.Red;
                                    this.label2.Text = "Fail";
                                    this.lTrackBar2.L_SliderColor = Color.Red;
                                    string rsu = this.textBox4.Text.Trim() + "," + DateTime.Now.ToString() + "," + "fail" + "," + this.textBox3.Text.Replace("\r", " ").Replace("\n", " ");
                                    test_log_save(rsu);
                                    this.textBox4.Text = "";
                                }));
                                exit_flog2 = 1;
                            }



                        }
                        catch
                        {


                        }

                        System.Threading.Thread.Sleep(100);
                    }



                    this.Invoke(new Action(() =>
                    {

                        this.lTrackBar2.L_Value = this.lTrackBar2.L_Maximum;
                        this.roundedButton2.Enabled = true;
                    }));
                });

            }
            catch(Exception ex)
            {
                this.textBox4.Text = ex.ToString();
            }
            finally {
                dut2 = 0;
                dut2_1 = 0;
                set_control("#4,5,6:0#");
            }
        }

        private async void roundButton3_Click(object sender, EventArgs e)
        {
          
                string filepath = "./DUT3_BUF.txt";

                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);

                if (false == Regex.IsMatch(this.textBox6.Text, pattern))
                {
                    this.label3.BackColor = Color.DarkRed;
                    this.label3.Text = "SN ERROR";
                    return;
                }
            try
            {
                this.label7.Text = $"SN:{this.textBox6.Text}";
                this.roundedButton3.Enabled = false;
                this.lTrackBar3.L_SliderColor = Color.LightSeaGreen;
                this.lTrackBar3.L_Value = 0;
                this.label3.BackColor = Color.Transparent;
                this.label3.Text = "Running";
                exit_flog3 = 0;
                this.textBox5.Text = "";

                set_control("#7,8:1#");
                 await    Task.Delay(200);
                set_control("#9:1#");
                 await    Task.Delay(200);

                mylib.utility_func.WinExec("dut3_espfuse_burn_erase_flash.CMD", 1);
           await     Task.Factory.StartNew(() =>
                {

                 
                    string content = "";
                    for (int i = 0; i < 100 * delay; i++)
                    {

                        if (exit_flog == 1 || exit_flog3 == 1) break;

                        try
                        {

                            this.Invoke(new Action(() =>
                            {

                                this.lTrackBar3.L_Value += 1;
                                if (this.lTrackBar3.L_Value == this.lTrackBar3.L_Maximum) this.lTrackBar3.L_Value = 0;
                            }));


                            content = System.IO.File.ReadAllText(filepath);

                            if (content.Length >= 3)
                            {

                                if (content.IndexOf(_step) >= 0 && dut3==0)
                                {
                                    dut3 = 1;


                                    set_control("#7,8,9:0#");
                                    System.Threading.Thread.Sleep(100);

                                    set_control("#7,8:1#");
                                    System.Threading.Thread.Sleep(100);
                                    set_control("#9:1#");
                                    System.Threading.Thread.Sleep(200);
                                    mylib.utility_func.WinExec("dut3_program_flash.CMD", 1);
                                }

                                if (content.IndexOf(_step2) >= 0 && dut3_1 == 0)
                                {
                                    dut3_1 = 1;


                                    set_control("#7,8,9:0#");
                                    System.Threading.Thread.Sleep(100);
                                    set_control("#9:1#");
                                    System.Threading.Thread.Sleep(4000);
                                    mylib.utility_func.WinExec("dut3_program_lte.CMD", 1);
                                }



                                this.Invoke(new Action(() =>
                                {


                                    this.textBox5.Text = content;


                                }));

                            }

                            if (content.IndexOf("pass") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label3.BackColor = Color.Green;
                                    this.label3.Text = "PASS";
                                    this.lTrackBar3.L_SliderColor = Color.Green;
                                    string rsu = this.textBox6.Text.Trim() + "," + DateTime.Now.ToString() + "," + "pass" + "," + this.textBox5.Text.Replace("\r", " ").Replace("\n", " ");
                                    test_log_save(rsu);
                                    this.textBox6.Text = "";
                                }));
                                exit_flog3 = 1;
                            }

                            if (content.IndexOf("fail") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label3.BackColor = Color.Red;
                                    this.label3.Text = "Fail";
                                    this.lTrackBar3.L_SliderColor = Color.Red;
                                    string rsu = this.textBox6.Text.Trim() + "," + DateTime.Now.ToString() + "," + "fail" + "," + this.textBox5.Text.Replace("\r", " ").Replace("\n", " ");
                                    test_log_save(rsu);
                                    this.textBox6.Text = "";
                                }));
                                exit_flog3 = 1;
                            }



                        }
                        catch
                        {


                        }

                        System.Threading.Thread.Sleep(100);
                    }



                    this.Invoke(new Action(() =>
                    {

                        this.lTrackBar3.L_Value = this.lTrackBar3.L_Maximum;
                        this.roundedButton3.Enabled = true;
                    }));
                });
            }
            catch (Exception ex)
            {

                this.textBox5.Text = ex.ToString();
            }
            finally {
                dut3_1 = 0;
                dut3 = 0;
                set_control("#7,8,9:0#");
            }

        }

        private void SGW_PROGRAM_KeyPress(object sender, KeyPressEventArgs e)
        {
            MessageBox.Show("Test");
        }

        private void roundButton5_Click(object sender, EventArgs e)
        {
            //if (this.textBox1.Text != string.Empty) {

                roundButton1_Click(sender, e);

           // }

          //  if (this.textBox4.Text != string.Empty)
           // {

                roundButton2_Click(sender, e);

          //  }
          //  if (this.textBox6.Text != string.Empty)
           // {

                roundButton3_Click(sender, e);

          //  }
          //  if (this.textBox8.Text != string.Empty)
          //  {

                roundButton4_Click(sender, e);

        //    }
        }

        private async void roundButton4_Click(object sender, EventArgs e)
        {
          
                string filepath = "./DUT4_BUF.txt";

                if (System.IO.File.Exists(filepath)) System.IO.File.Delete(filepath);

                if (false == Regex.IsMatch(this.textBox8.Text, pattern))
                {
                    this.label4.BackColor = Color.DarkRed;
                    this.label4.Text = "SN ERROR";
                    return;
                }
            try
            {
                this.label8.Text = $"SN:{this.textBox8.Text}";
                this.roundedButton4.Enabled = false;
                this.lTrackBar4.L_SliderColor = Color.LightSeaGreen;
                this.lTrackBar4.L_Value = 0;
                this.label4.BackColor = Color.Transparent;
                this.label4.Text = "Running";
                exit_flog4 = 0;
                this.textBox7.Text = "";
                set_control("#10,11:1#");
                await Task.Delay(200);
                set_control("#12:1#");
                await Task.Delay(200);
                mylib.utility_func.WinExec("dut4_espfuse_burn_erase_flash.CMD", 1);
              //  mylib.utility_func.WinExec("./FW_PRGO_SCRIPT/F01S3/dut1_erase_flash.cmd", 1);

                await       Task.Factory.StartNew(() =>
                {


                    string content = "";
                    for (int i = 0; i < 100 * delay; i++)
                    {

                        if (exit_flog == 1 || exit_flog4 == 1) break;

                        try
                        {

                            this.Invoke(new Action(() =>
                            {

                                this.lTrackBar4.L_Value += 1;
                                if (this.lTrackBar4.L_Value == this.lTrackBar4.L_Maximum) this.lTrackBar4.L_Value = 0;
                            }));


                            content = System.IO.File.ReadAllText(filepath);

                            if (content.Length >= 3)
                            {

                                if (content.IndexOf(_step) >= 0 && dut4==0) {
                                    dut4 = 1;

                                    set_control("#10,11,12:0#");
                                    System.Threading.Thread.Sleep(100);
                                    set_control("#10,11:1#");
                                    System.Threading.Thread.Sleep(100);

                                    set_control("#12:1#");
                                    System.Threading.Thread.Sleep(200);
                                    mylib.utility_func.WinExec("dut4_program_flash.CMD", 1);


                                }
                                if (content.IndexOf(_step2) >= 0 && dut4_1 == 0)
                                {
                                    dut4_1 = 1;

                                    set_control("#10,11,12:0#");

                                    System.Threading.Thread.Sleep(100);
                                    set_control("#12:1#");
                                    System.Threading.Thread.Sleep(4000);
                                    mylib.utility_func.WinExec("dut4_program_lte.CMD", 1);


                                }




                                this.Invoke(new Action(() =>
                                {


                                    this.textBox7.Text = content;


                                }));

                            }

                            if (content.IndexOf("pass") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label4.BackColor = Color.Green;
                                    this.label4.Text = "PASS";
                                    this.lTrackBar4.L_SliderColor = Color.Green;
                                    string rsu = this.textBox8.Text.Trim() + "," + DateTime.Now.ToString() + "," + "pass" + "," + this.textBox7.Text.Replace("\r", " ").Replace("\n", " ");
                                    this.textBox8.Text = "";
                                    test_log_save(rsu);
                                }));
                                exit_flog4 = 1;
                            }

                            if (content.IndexOf("fail") >= 0)
                            {

                                this.Invoke(new Action(() =>
                                {
                                    this.label4.BackColor = Color.Red;
                                    this.label4.Text = "Fail";
                                    this.lTrackBar4.L_SliderColor = Color.Red;
                                    string rsu = this.textBox8.Text.Trim() + "," + DateTime.Now.ToString() + "," + "fail" + "," + this.textBox7.Text.Replace("\r", " ").Replace("\n", " ");
                                    this.textBox8.Text = "";
                                    test_log_save(rsu);
                                }));
                                exit_flog4 = 1;
                            }

                            System.Threading.Thread.Sleep(100);

                        }
                        catch
                        {


                        }


                    }



                    this.Invoke(new Action(() =>
                    {

                        this.lTrackBar4.L_Value = this.lTrackBar1.L_Maximum;
                        this.roundedButton4.Enabled = true;
                    }));
                });
            }
            catch (Exception ex)
            {
                this.textBox8.Text = ex.ToString();


            }
            finally {
                try
                {
                    dut4_1 = 0;
                    dut4 = 0;
                    set_control("#10,11,12:0#");
                }
                catch { }
            }

        }

        private void SGW_PROGRAM_FormClosing(object sender, FormClosingEventArgs e)
        {
            exit_flog = 1;
        }

        private void lTrackBar1_LValueChanged(object sender, DemoControls.LEventArgs e)
        {

        }


        public void test_log_save(string save_data) {

            lock (obj)
            {

                if (!System.IO.File.Exists("./prog_test_data.csv")) {

                    using (System.IO.StreamWriter wr = new System.IO.StreamWriter("./prog_test_data.csv", true))
                    {

                        wr.Write("SN,Date,Result,TEST_Description" + "\n");

                    }

                }
           
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter("./prog_test_data.csv",true)) {

                wr.Write(save_data + "\n");

            }

            }
        }

        public void set_control(string cont_str) {

            lock (_lock) {



                control.Getfun()["relay_set"]("pass", "pass", out _, cont_str);

            }


        }
    }
}
