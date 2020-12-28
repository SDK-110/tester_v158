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

namespace testapp
{
    public partial class Form3 : Form
    {
      volatile  byte ry08=0,ry16=0;
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        private IniParser.Model.IniData dt;

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
        #endregion
        public Form3()
        {
            InitializeComponent();
            ptrWnd = FindWindow(null, iniread.ReadFile("setup.ini")["setproduct"]["name"]);
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
                this.ry08 = (byte)(this.ry08 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button32.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (button31.Text == "off")
            {
                button31.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button31.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (button30.Text == "off")
            {
                button30.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button30.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (button27.Text == "off")
            {
                button27.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button27.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (button29.Text == "off")
            {
                button29.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button29.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (button25.Text == "off")
            {
                button25.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button25.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (button26.Text == "off")
            {
                button26.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button26.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (button24.Text == "off")
            {
                button24.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 0);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button24.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 0));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (button23.Text == "off")
            {
                button23.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 1);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button23.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 1));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (button22.Text == "off")
            {
                button22.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 2);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button22.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 2));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (button19.Text == "off")
            {
                button19.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 3);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button19.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 3));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (button20.Text == "off")
            {
                button20.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button20.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button21.Text == "off")
            {
                button21.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 5);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button21.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 5));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (button17.Text == "off")
            {
                button17.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 6);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button17.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 6));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (button18.Text == "off")
            {
                button18.Text = "on";
                this.ry16 = (byte)(this.ry16 | 1 << 7);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button18.Text = "off";
                this.ry16 = (byte)(this.ry16 & ~(1 << 7));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (button28.Text == "off")
            {
                button28.Text = "on";
                this.ry08 = (byte)(this.ry08 | 1 << 4);
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);

            }
            else
            {

                button28.Text = "off";
                this.ry08 = (byte)(this.ry08 & ~(1 << 4));
                string temp = string.Format("{0:x2}", this.ry08);
                string temp2 = string.Format("{0:x2}", this.ry16);
                SendMessage(ptrWnd, WM_SENDA_2, IntPtr.Zero, temp + ";" + temp2);



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
