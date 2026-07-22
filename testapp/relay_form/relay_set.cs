using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.mylib;
using testapp;
using IniParser;
using testapp.glob_set;
namespace testapp
{
    public partial class relay_set : UserControl
    {
        testcase_dll m = null;
        string relay_resource = "relay_set";
        string relay_port_resource = "Relay_board";
      
        string setup_inifile = "setup.ini";
        public string setup_ini => setup_inifile;
        public string realy_port_name => relay_port_resource;
        public string  RelayRes{
            get {

                return relay_resource;
            }
            set {

                relay_resource = value;
            }
}
        public relay_set()
        {
            InitializeComponent();
        }
        public void set_lib_ref(ref testcase_dll m)
        {

            this.m = m;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] ==null) return;
            string tempout = "";
            if (button1.Text == "on")
            {
                button1.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "1:0");
            }
            else {

                button1.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "1:1");

            }
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button2.Text == "on")
            {
                button2.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "2:0");
            }
            else
            {

                button2.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "2:1");

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button3.Text == "on")
            {
                button3.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "3:0");
            }
            else
            {

                button3.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "3:1");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button4.Text == "on")
            {
                button4.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "4:0");
            }
            else
            {

                button4.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "4:1");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button5.Text == "on")
            {
                button5.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "5:0");
            }
            else
            {

                button5.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "5:1");

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button6.Text == "on")
            {
                button6.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "6:0");
            }
            else
            {

                button6.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "6:1");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button7.Text == "on")
            {
                button7.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "7:0");
            }
            else
            {

                button7.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "7:1");

            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button8.Text == "on")
            {
                button8.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "8:0");
            }
            else
            {

                button7.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "8:1");

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button9.Text == "on")
            {
                button9.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "9:0");
            }
            else
            {

                button9.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "9:1");

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (m == null || glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button10.Text == "on")
            {
                button10.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "10:0");
            }
            else
            {

                button9.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "10:1");

            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button11.Text == "on")
            {
                button11.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "11:0");
            }
            else
            {

                button11.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "11:1");

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button12.Text == "on")
            {
                button12.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "12:0");
            }
            else
            {

                button12.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "12:1");

            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button13.Text == "on")
            {
                button13.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "13:0");
            }
            else
            {

                button13.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "13:1");

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button14.Text == "on")
            {
                button14.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "14:0");
            }
            else
            {

                button14.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "14:1");

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button15.Text == "on")
            {
                button15.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "15:0");
            }
            else
            {

                button15.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "15:1");

            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (m == null ||  glob_ini_instance.getInstance().getSetupIniData["setport"][realy_port_name] == null) return;
            string tempout = "";
            if (button16.Text == "on")
            {
                button16.Text = "off";

                m.Getfun()[relay_resource]("", "", out tempout, "16:0");
            }
            else
            {

                button16.Text = "on";
                m.Getfun()[relay_resource]("", "", out tempout, "16:1");

            }
        }
    }
}
