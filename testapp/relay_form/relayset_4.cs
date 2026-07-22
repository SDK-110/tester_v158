using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using testapp.glob_set;

namespace testapp
{
    public partial class relay_debug_4 : Form
    {
        private static relay_debug_4 instance_obj = null;
      volatile  byte ry08=0,ry16=0;
      volatile byte ry08_2 = 0, ry16_2 = 0;
      volatile byte ry08_3 = 0, ry16_3 = 0;
        volatile byte ry08_4 = 0, ry16_4 = 0;
      
        private IniParser.Model.IniData inidata= glob_ini_instance.getInstance().getSetupIniData;

           IntPtr  ptrWnd;
        #region /*--------------message loop dll upload-------------*/

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(
            IntPtr hWnd, // handle to destination window 
            uint Msg, // message 
            uint wParam, // first message parameter 
            uint lParam // second message parameter 
            );

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        public const int USER = 0x0400;
        public const int WM_SENDA = USER + 101;
        public const int WM_SENDB = USER + 102;
        public const int WM_SENDC = USER + 103;
        public const int WM_SENDD = USER + 104;
        public const int WM_SENDE = USER + 105;
        public const int WM_SHOWNUM = USER + 106;
        public const int WM_FASTID = USER + 107;
        public const int WM_SENDA_2 = USER + 108;
        public const int WM_SENDA_3 = USER + 109;
        public const int WM_SENDA_4 = USER + 122;
        #endregion
        private relay_debug_4()
        {
            InitializeComponent();
            ptrWnd = FindWindow(null, inidata["setproduct"]["name"]);
        }
        public static relay_debug_4 get_instance() {

            if (instance_obj == null) {
                instance_obj = new relay_debug_4();
            }
            instance_obj.Show();
            return instance_obj;
        }
        public void set_main_win_ptr(IntPtr win_d) {

            ptrWnd = win_d;
        
        }
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "off")
            {
                button4.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button4.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 <<3));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "off")
            {
                button3.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button3.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "off")
            {
                button6.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button6.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "off")
            {
                button2.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button2.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "off")
            {
                button5.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button5.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "off")
            {
                button7.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button7.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "off")
            {
                button8.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
               SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);
              //  SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            }
            else
            {

                button8.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (button16.Text == "off")
            {
                button16.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button16.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (button15.Text == "off")
            {
                button15.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button15.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (button14.Text == "off")
            {
                button14.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button14.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (button11.Text == "off")
            {
                button11.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button11.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "off")
            {
                button12.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button12.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (button13.Text == "off")
            {
                button13.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button13.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "off")
            {
                button10.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button10.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "off")
            {
                button9.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button9.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (button32.Text == "off")
            {
                button32.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button32.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (button31.Text == "off")
            {
                button31.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button31.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (button30.Text == "off")
            {
                button30.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button30.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (button27.Text == "off")
            {
                button27.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button27.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (button29.Text == "off")
            {
                button29.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button29.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (button25.Text == "off")
            {
                button25.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button25.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (button26.Text == "off")
            {
                button26.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button26.Text = "off";
                this.ry08_2 = (byte)(this.ry08 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (button24.Text == "off")
            {
                button24.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button24.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (button23.Text == "off")
            {
                button23.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button23.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (button22.Text == "off")
            {
                button22.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button22.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (button19.Text == "off")
            {
                button19.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button19.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (button20.Text == "off")
            {
                button20.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button20.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button21.Text == "off")
            {
                button21.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button21.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (button17.Text == "off")
            {
                button17.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button17.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (button18.Text == "off")
            {
                button18.Text = "on";
                this.ry16_2 = (byte)(this.ry16_2 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button18.Text = "off";
                this.ry16_2 = (byte)(this.ry16_2 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (button28.Text == "off")
            {
                button28.Text = "on";
                this.ry08_2 = (byte)(this.ry08_2 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button28.Text = "off";
                this.ry08_2 = (byte)(this.ry08_2 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_2);
                string temp2 = string.Format("{0:x2}", this.ry16_2);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            //1

            if (button48.Text == "off")
            {
                button48.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button48.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button47_Click(object sender, EventArgs e)
        {
            //2
            if (button47.Text == "off")
            {
                button47.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button47.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button46_Click(object sender, EventArgs e)
        {
            //3
            if (button46.Text == "off")
            {
                button46.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button46.Text = "off";
                this.ry08_3 = (byte)(this.ry08 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button43_Click(object sender, EventArgs e)
        {
            //4

            if (button43.Text == "off")
            {
                button43.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button43.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }



        }

        private void button44_Click(object sender, EventArgs e)
        {
            //5
            if (button44.Text == "off")
            {
                button44.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button44.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button45_Click(object sender, EventArgs e)
        {
            //6
            if (button45.Text == "off")
            {
                button45.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button45.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }



        }

        private void button41_Click(object sender, EventArgs e)
        {
            //7
            if (button41.Text == "off")
            {
                button41.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button41.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button42_Click(object sender, EventArgs e)
        {
            //8
            if (button42.Text == "off")
            {
                button42.Text = "on";
                this.ry08_3 = (byte)(this.ry08_3 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);
                //  SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
            }
            else
            {

                button42.Text = "off";
                this.ry08_3 = (byte)(this.ry08_3 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button40_Click(object sender, EventArgs e)
        {
            //9
            if (button40.Text == "off")
            {
                button40.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button40.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button39_Click(object sender, EventArgs e)
        {
            //10
            if (button39.Text == "off")
            {
                button39.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button39.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //11
            if (button38.Text == "off")
            {
                button38.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button38.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button35_Click(object sender, EventArgs e)
        {
            //12
            if (button35.Text == "off")
            {
                button35.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button35.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //13
            if (button36.Text == "off")
            {
                button36.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button36.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }


        }

        private void button37_Click(object sender, EventArgs e)
        {
            //14
            if (button37.Text == "off")
            {
                button37.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button37.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {

            SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, "00;00");
            SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, "00;00");
            SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, "00;00" );
            SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, "00;00");
            this.Hide();
            e.Cancel = true;
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (button64.Text == "off")
            {
                button64.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button64.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (button63.Text == "off")
            {
                button63.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button63.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (button62.Text == "off")
            {
                button62.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button62.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (button59.Text == "off")
            {
                button59.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button59.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (button60.Text == "off")
            {
                button60.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button60.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (button61.Text == "off")
            {
                button61.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button61.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (button57.Text == "off")
            {
                button57.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button57.Text = "off";
                this.ry08_4 = (byte)(this.ry08_2 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (button58.Text == "off")
            {
                button58.Text = "on";
                this.ry08_4 = (byte)(this.ry08_4 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button58.Text = "off";
                this.ry08_4 = (byte)(this.ry08_4 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (button56.Text == "off")
            {
                button56.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button56.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (button55.Text == "off")
            {
                button55.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button55.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (button54.Text == "off")
            {
                button54.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button54.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (button51.Text == "off")
            {
                button51.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button51.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (button52.Text == "off")
            {
                button52.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button52.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (button53.Text == "off")
            {
                button53.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button53.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (button49.Text == "off")
            {
                button49.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button49.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (button50.Text == "off")
            {
                button50.Text = "on";
                this.ry16_4 = (byte)(this.ry16_4 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button50.Text = "off";
                this.ry16_4 = (byte)(this.ry16_4 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_4);
                string temp2 = string.Format("{0:x2}", this.ry16_4);
                SendMessage(ptrWnd, WM_SENDA_4, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            //15
            if (button33.Text == "off")
            {
                button33.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button33.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button34_Click(object sender, EventArgs e)
        {
            //16
            if (button34.Text == "off")
            {
                button34.Text = "on";
                this.ry16_3 = (byte)(this.ry16_3 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button34.Text = "off";
                this.ry16_3 = (byte)(this.ry16_3 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08_3);
                string temp2 = string.Format("{0:x2}", this.ry16_3);
                SendMessage(ptrWnd, WM_SENDA_3, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "off")
            {
                button1.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp+";"+temp2);

            }
            else {

                button1.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA, IntPtr.Zero, temp + ";" + temp2);



            }

        }


    }
}
