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
    
    public partial class sgw_customer_test : Form
    {
        testcase_dll m = new testcase_dll();
        int flog1 = 1;

        int successflog = 0;
        int start_flog = 0;
        int red_button_pressed = 0;
        int tishibianlliang = 0;
        volatile int volset_Atomic = 0;
        volatile int timer1folg = 0;
        volatile int timer2folg = 0;
        volatile int micsetvalue = 0;
        volatile int micset_Atomic = 0;
        volatile int phone_flog = 0;
        string result = "";
       volatile string[] caton = { "   <----请先点击它加载测试环境 ", "<----请先点击它加载测试环境",
            " 加载端口中，请稍后", "     加载端口中，请稍后",
            " 加载成功", " 加载成功 ",
            " 加载不成功请将产品断电，并按一下JLINK 复位开关 ", " 加载不成功请将产品断电，并按一下JLINK 复位开关  ",
            " 未发现产品，等待产品放入 ", "   未发现产品，等待产品放入"



        };

        public sgw_customer_test()
        {
            InitializeComponent();
            this.led1.OnColor = Color.LightGreen;
            this.label1.ForeColor = Color.LightGreen;
            gaugeLinear1.Enabled = false;
            gaugeLinear2.Enabled = false;
            this.timer1.Start();
        }

        private void led1_Click(object sender, EventArgs e)
        {
            utility_func.killproc("jlink.exe");
             successflog = 0;
            if (SerialProtFindHelper.GetSerialport_fromName("Quectel USB AT Port").IndexOf("COM") < 0) { this.led1.Value = false; return; }
            if (this.led1.Value == true) return;
            timer1.Start();
            this.led1.Value = true;
            this.led1.OnColor = Color.Yellow;

            tishibianlliang = 2;
            new Task(() => {



              //  if (m.Getfun()["test_temp"]("", "", out result) == "pass")
               if( m.Getfun()["schsa_project_test_api"]("ok", "ok", out result, "set_dut_enter_test_mode")=="pass")
                {

                    tishibianlliang = 4;
                    this.Invoke((Action)delegate ()
                    {

                        this.led1.OnColor = Color.LightGreen;
                        successflog = 1;
                        

                        m.Getfun()["schsa_project_test_api"]("06;1", "06;1", out result, "write_gpio_pin"); // 设定功放使能
                        gaugeLinear1.Enabled = true;
                        gaugeLinear2.Enabled = true;
                        timer2.Start();
                    });

                }
                else {
                    this.Invoke((Action)delegate ()
                    {

                        this.led1.OnColor = Color.Red;
                        tishibianlliang = 6;
                        successflog = 0;
                        timer2.Stop();
                    });

                };





            }).Start();
          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {



            if (timer1folg == 0)
            {
                timer1folg = 1;
                new Task(() =>
                {


                    if (SerialProtFindHelper.GetSerialport_fromName("Quectel USB AT Port").IndexOf("COM") < 0) { 
                        
                        tishibianlliang = 8;
                        this.Invoke((Action)delegate {

                            this.led1.Value = false;
                            gaugeLinear1.Enabled = false;


                        });
                        
                    
                    }


                    else
                    {
                        if (tishibianlliang == 8) tishibianlliang = 0;
                    }

                  

                    timer1folg = 0;

                }).Start();

            }


            if (flog1 == 1)
            {


                this.Invoke((Action)delegate {

                    this.label1.Text = caton[tishibianlliang];


                });

                flog1 = 0;
            }
            else
            {
                this.Invoke((Action)delegate {

                    this.label1.Text = caton[tishibianlliang + 1];

                });


                flog1 = 1;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (timer2folg == 1) return;
                timer2folg = 1;
                new Task(() =>
                {



                m.Getfun()["schsa_project_test_api"]("06;1", "06;1", out result, "write_gpio_pin"); // 设定功放使能

                if (m.Getfun()["schsa_project_test_api"]("26;0", "26;0", out result, "read_gpio_pin") == "pass")
                {
                    this.Invoke((Action)delegate
                    {
                        this.led2.Value = true;

                    });

                if (phone_flog == 1)
                {

                   
                    m.Getfun()["schsa_project_test_api"]("", "", out result, "hang_up_call");
                    m.Getfun()["schsa_project_test_api"]("7,11", "", out result, "mic_gain");
                    m.Getfun()["schsa_project_test_api"]("5", "", out result, "wpel_lte_set_clvl");
                    red_button_pressed = 0;
                            this.Invoke((Action)delegate
                            {
                                this.label3.Text = "____________________";
                                this.textBox1.Text = "____________________";
                                this.label10.Text = "_____________________________________";
                            });
                            


                        }


                    }
                    else
                    {

                        if (this != null)
                        {
                            this.Invoke((Action)delegate
                            {
                                if(this!=null&& this.led2!= null) this.led2.Value = false;

                            });


                        }
                    


                    }


                    if (m.Getfun()["schsa_project_test_api"]("32;0", "32;0", out result, "read_gpio_pin") == "pass") //红色按钮按下
                {

                        this.Invoke((Action)delegate
                        {
                            this.led3.Value = true;

                        });


                        if (red_button_pressed == 0)
                        {
                           
                            red_button_pressed = 1;
                            new Task(() =>
                            {

                                result = "";
                                if (m.Getfun()["schsa_project_test_api"]("", "", out result, "wpel_lte_read_imei") == "pass")
                                {
                                    this.Invoke((Action)delegate ()
                                    {


                                        this.label10.Text = result;



                                    });

                                }
                                else
                                {

                                    this.Invoke((Action)delegate ()
                                    {


                                        this.label10.Text = "_____________________________________";



                                    });


                                }
                                
                                result = "";
                                if (m.Getfun()["schsa_project_test_api"]("", "", out result, "wpel_lte_read_iccid") == "pass")
                                {
                                    this.Invoke((Action)delegate ()
                                    {


                                        this.label3.Text = result;



                                    });

                                }
                                else
                                {

                                    this.Invoke((Action)delegate ()
                                    {


                                        this.label3.Text = "____________________";



                                    });


                                }

                                result = "";
                                if (m.Getfun()["schsa_project_test_api"]("", "", out result, "wpel_lte_get_CSQ") == "pass")
                                {
                                    this.Invoke((Action)delegate ()
                                    {


                                        this.textBox1.Text = result;



                                    });


                                }
                                else
                                {
                                    this.Invoke((Action)delegate ()
                                    {


                                        this.textBox1.Text = "____________________";



                                    });

                                }

                                result = "";


                                if (m.Getfun()["schsa_project_test_api"](this.textBox2.Text, "", out result, "wpel_lte_call") == "pass")
                                {
                                    gaugeLinear1.Enabled = true;

                                    phone_flog = 1;
                                }
                                else
                                {


                                }





                            }).Start();




                        }
                    }
                    else
                    {
                        if (this != null) { 
                        this.Invoke((Action)delegate
                        {
                            this.led3.Value = false;

                        });

                        }





                    }


                    timer2folg = 0;


                }).Start();
            }
            catch { 
            
            }
        }

        private void gaugeLinear1_ValueChanged(object sender, double value)
        {


            if (volset_Atomic == 1) return;
            volset_Atomic = 1;
            new Task(() =>
            {


            this.Invoke((Action)delegate ()
            {

              if (m.Getfun()["schsa_project_test_api"]("" + ((int)gaugeLinear1.Value), "", out result, "wpel_lte_set_clvl") == "pass")
            

       
                    volset_Atomic = 0;
                });
               





            }).Start();

        }

   

        private void led1_Load(object sender, EventArgs e)
        {

        }

        private void sgw_customer_test_FormClosing(object sender, FormClosingEventArgs e)
        {
            utility_func.killproc("jlink.exe");
            timer1.Stop();
        }

        private void gaugeLinear2_ValueChanged(object sender, double value)
        {

            if (micset_Atomic == 1) return;
            micset_Atomic = 0;




            new Task(() =>
            {

                switch ((int)value)
                {

                    case 1:
                        m.Getfun()["schsa_project_test_api"]("3,6", "", out result, "mic_gain");
                        micsetvalue = 1;
                        break;
                    case 2:
                        m.Getfun()["schsa_project_test_api"]("5,8", "", out result, "mic_gain");
                        micsetvalue = 2;
                        break;
                    case 3:
                        m.Getfun()["schsa_project_test_api"]("7,11", "", out result, "mic_gain");
                        micsetvalue = 3;
                        break;

                    case 4:
                        m.Getfun()["schsa_project_test_api"]("7,13", "", out result, "mic_gain");
                        micsetvalue = 4;
                        break;
                    case 5:
                        m.Getfun()["schsa_project_test_api"]("7,15", "", out result, "mic_gain");
                        micsetvalue = 5;
                        break;
                    default:

                        break;




                }




            }).Start();

            



        }
    }
}
