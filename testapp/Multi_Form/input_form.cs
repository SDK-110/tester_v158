using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.glob_set;
using WeifenLuo.WinFormsUI.Docking;
using 重构程序.viewmode;

namespace testapp.duochuangti
{
    public partial class input_form : DockContent
    {
        static input_form testt_sw;
  
        IniParser.Model.IniData inidata;
        private input_form()
        {
            InitializeComponent();
            inidata = glob_ini_instance.getInstance().getSetupIniData;
            if (inidata != null)
            {
                if (inidata["setbarcode"]["barenable"] == "true")
                {

                    this.button1.Visible = false;
                    this.textBox1.Visible = true;
                    this.button2 .Visible=false;
                    this.textBox2.Visible = true;
                    this.label1.Show();
                    this.label2.Show();

                }
                else
                {

                    this.textBox1.Visible = false;
                    this.button1.Visible = true;
                    this.textBox2.Visible = false;
                    this.button2.Visible = true;
                    this.label1.Hide();
                    this.label2.Hide();
                }
            }

            this.button1.Text = "WAITING TEST";

            this.button1.ForeColor = Color.Black;
            pictureBox1.Visible = false;
         

            this.button2.Text = "WAITING TEST";

            this.button2.ForeColor = Color.Black;
   
            pictureBox4.Visible = false;
        }

        public static input_form GetTrigger_Form_instance()
        {
            
            if (testt_sw == null) testt_sw = new input_form();

            return testt_sw;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.IsHidden) return;
            this.button1.Text = "DUT1 TESTING";

            this.button1.ForeColor = Color.Blue  ;
            test1_form.get_form_instance().run();
            pictureBox1.Visible = true;
           
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.IsHidden) return;
            this.button2.Text = "DUT2 TESTING";

            this.button2.ForeColor = Color.Blue;
            test2_form.get_form_instance().run();
        
            pictureBox4.Visible = true;
        }

        public void set_pass_dut1()
        {
            if (this.IsHidden) return;
            this.button1.Text = "DUT1 PASS";

            this.button1.ForeColor = Color.Green;
            this.label1.Text = "DUT1 PASS";
            this.label1.BackColor = Color.Green;
            pictureBox1.Visible = false;
           
            this.textBox1.Text = "";
            this.textBox1.Enabled = true;
        }

        public void set_pass_dut2()
        {
            if (this.IsHidden) return;
            this.button2.Text = "DUT2 PASS";

            this.button2.ForeColor = Color.Green;

            this.label2.Text = "DUT2 PASS";
            this.label2.BackColor = Color.Green;
      
            pictureBox4.Visible = false;
            this.textBox2.Text = "";
            this.textBox2.Enabled = true;
        }
        public void set_fail_dut1()
        {
            if (this.IsHidden) return;
            this.button1.Text = "DUT1 FAIL";

            this.button1.ForeColor = Color.Red;
            this.label1.Text = "DUT1 FAIL";
            this.label1.BackColor = Color.Red;
            pictureBox1.Visible = false;
        
            this.textBox1.Text="";
            this.textBox1.Enabled = true;
        }

        public void set_fail_dut2()
        {
            if (this.IsHidden) return;
            this.button2.Text = "DUTT2_FAIL";

            this.button2.ForeColor = Color.Red;
            this.label2.Text = "DUT2 FAIL";
            this.label2.BackColor = Color.Red;
           
            pictureBox4.Visible = false;
            this.textBox2.Text = "";
            this.textBox2.Enabled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (this.IsHidden) return;
            if (inidata["statu"]["plc_auto"] != null) return;
            if (e.KeyCode == Keys.Enter)
            {

                MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                if (reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

                {
                    debug_form.GetDebug_f_instance().clear();
                    this.textBox1.Enabled = false;
                    test1_form.get_form_instance().set_sn(this.textBox1.Text);
                    this.label1.Text = "DUT1 TESTING";
                    this.label1.BackColor = Color.Blue;
                    test1_form.get_form_instance().run();
                    pictureBox1.Visible = true;
                   
                }
                // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
                {

                    // MessageBox.Show("条码规则不对");
                }

            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.IsHidden) return;
            if (inidata["statu"]["plc_auto"] != null) return;
            if (e.KeyCode == Keys.Enter)
            {

                MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox2.Text);

                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                if (reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

                {
                    debug_form.GetDebug_f_instance().clear();
                    this.textBox2.Enabled = false;
                    test2_form.get_form_instance().set_sn(this.textBox2.Text);
                    this.label1.Text = "DUT2 TESTING";
                    this.label1.BackColor = Color.Blue;
                    test2_form.get_form_instance().run();
                  
                    pictureBox4.Visible = true;
                }
                // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
                {

                    // MessageBox.Show("条码规则不对");
                }

            }
        }
        public void set_input_box_clear_1()
        {



            
            this.textBox1.Enabled = false;

        }
        public void set_input_box_clear_2()
        {


            this.textBox2.Enabled=false;


        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;

        private void input_form_Shown(object sender, EventArgs e)
        {
            SendMessage(this.textBox1.Handle, EM_SETCUEBANNER, IntPtr.Zero, "please input sn");
            SendMessage(this.textBox2.Handle, EM_SETCUEBANNER, IntPtr.Zero, "please input sn");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.BackColor = Color.White;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            this.textBox2.BackColor = Color.White;
        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

            debug_form.GetDebug_f_instance().clear();
            if (this.IsHidden) return;
            if (inidata["statu"]["plc_auto"] != null) return;

            if (inidata["setbarcode"]["barenable"] == "false")

            {
                this.button1.PerformClick();
                this.button2.PerformClick();
            }

                MatchCollection reg = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox1.Text);

                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                if (reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

                {
                    this.textBox1.Enabled = false;
                    test1_form.get_form_instance().set_sn(this.textBox1.Text);
                    this.label1.Text = "DUT1 TESTING";
                    this.label1.BackColor = Color.Blue;
                    test1_form.get_form_instance().run();
                    pictureBox1.Visible = true;
                  
                }
                // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                else if (!(reg.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
                {

                    // MessageBox.Show("条码规则不对");
                }

            MatchCollection reg2 = new Regex(inidata["setbarcode"]["barreg"]).Matches(this.textBox2.Text);

            //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
            //  return;
            // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
            if (reg2.Count > 0 && inidata["setbarcode"]["barenable"] == "true")

            {
                this.textBox2.Enabled = false;
                test2_form.get_form_instance().set_sn(this.textBox2.Text);
                this.label1.Text = "DUT2 TESTING";
                this.label1.BackColor = Color.Blue;
                test2_form.get_form_instance().run();
            
                pictureBox4.Visible = true;
            }
            // else if (dt["setbarcode"]["barlen"] != this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
            else if (!(reg2.Count > 0 && inidata["setbarcode"]["barenable"] == "true"))
            {

                // MessageBox.Show("条码规则不对");
            }


        }
    }
}
