using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using PCHMI;
using testapp.mycontroler;
using System.Drawing.Text;
using IoTClient;
using HslCommunication.ModBus;

namespace testapp.test_form
{
    public partial class test_temp : Form
    {
        static ManualResetEvent manualEvent = new ManualResetEvent(false);
        System.Windows.Forms.Timer tm;
         //SRND_CM_12DI _12DI = new SRND_CM_12DI("COM9");
        PrivateFontCollection privateFonts = new PrivateFontCollection();
        test_case_helper test_case = null;
        System.Threading.Timer test;
        ContainerControl test_point;

        public test_temp()
        {


            InitializeComponent();

          //  test_point = new test_control();
           // Pictureshow.getInstance().Show();
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            //  test_2.get_instance().Show();
            // testapp.PLC.plc_option.test_ex_module_modbus();
            testapp.PLC.plc_option.melsec_serial();
        }

 
        private void button2_Click(object sender, EventArgs e)
        {
            
            this.richTextBox1.Text="";

           test_case.run();

            Pictureshow.getInstance().TopLevel=false;
           Pictureshow.getInstance().Bounds = this.panel1.Bounds;
            Pictureshow.getInstance().Location = new Point(0, 0);
            panel1.Controls.Add(Pictureshow.getInstance());
            Pictureshow.getInstance().Show();
            //Main_f m = new Main_f();
            //m.TopLevel = false;
            //panel1.Controls.Add(m);
            //m.Show();
            while (Pictureshow.getInstance().Visible) {

                Application.DoEvents();
                
            }
            MessageBox.Show("Test");
        }

        private void test_jicheng(ContainerControl ctl) { 
        
        
        ctl.Hide();
        
        }
        private void test3_Load(object sender, EventArgs e)
        {
         
            privateFonts.AddFontFile("FontAwesome.ttf");

            Font font = new Font(privateFonts.Families[0], 10);
            this.button1.Font = new Font(font.FontFamily, 24);
            this.richTextBox1.Font = new Font(font.FontFamily, 50);
            this.Font= new Font(font.FontFamily,10);
            this.button1.Text = "\uf00c";
         
            test_case_helper.update_call = (w, z) =>
            {

                this.richTextBox1.Invoke((Action)delegate {
                   
                   this.richTextBox1.Text= w.testcase_description + " " + w.get_judge_result;
                    if (w.get_judge_result == "pass") {
                      
                        this.richTextBox1.AppendText("\uF00C");
                        this.richTextBox1.Select(this.richTextBox1.Text.Length - 1, 1);
                        richTextBox1.SelectionColor = Color.Green;
                        this.richTextBox1.DeselectAll();
                    }
                    else
                    {

                     
                        this.richTextBox1.AppendText("\uF00D");
                        this.richTextBox1.Select(this.richTextBox1.Text.Length - 1, 1);
                        richTextBox1.SelectionColor = Color.Red;
                        this.richTextBox1.DeselectAll();
                    }
                   
                   
                });

            };
            test_case = new test_case_helper();
            test_case.set_production_info(new production_info());
            Task.Factory.StartNew(() => {
                testcase_dll a = new testcase_dll();
                test_case.set_init_4runlib_testcase(ref a);
                test_case.set_testcase_action();

            });


        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {

            var p = new System.Threading.Timer((o) =>
            {


                (o as Form).Invoke((Action)delegate
                {

                    this.Text = DateTime.Now.ToString();
                });

            }, this, 0, 100);
            
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            textBox1.Focus();
            SendKeys.Send("{ENTER}");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                MessageBox.Show("Test");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // IoTClient.Clients.Modbus.ModbusRtuClient srnd_cm_12 = new IoTClient.Clients.Modbus.ModbusRtuClient("COM9", 9600, 8, StopBits.One, Parity.None);

            // //var p = srnd_cm_12.ReadCoil("0", 1,2);
            //var p = srnd_cm_12.Read("0", 1, 2, 16);
            // var z = p.Value;
            // srnd_cm_12.Close();

            IoTClient.Clients.Modbus.ModbusRtuClient srnd_cm_12 = new IoTClient.Clients.Modbus.ModbusRtuClient("COM7", 115200, 8, StopBits.One, Parity.None);

            //var p = srnd_cm_12.ReadCoil("0", 1,2);
            var p = srnd_cm_12.Open();
    for(int i=0;i<100;i++)
            srnd_cm_12.Write("1", i%2==0?true:false, 1,5);
              srnd_cm_12.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
           ModbusRtu hslcomm_modubus = new ModbusRtu(1);
            hslcomm_modubus.SerialPortInni("COM2", 115200,8, System.IO.Ports.StopBits.One, Parity.None);
            hslcomm_modubus.Open();
            hslcomm_modubus.ReadCoil("x=2;0", 16);


            hslcomm_modubus.Close();


    }

        private void button7_Click(object sender, EventArgs e)
        {
            test_jicheng(Pictureshow.getInstance());
        }
    }
}
