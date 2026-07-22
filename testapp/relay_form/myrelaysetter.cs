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
    public partial class myrelaysetter : Form
    {
        volatile UInt32 relay_rec = Convert.ToUInt32("111111111111111111111111111", 2);
        volatile UInt32 relay_rec2 = Convert.ToUInt32("111111111111111111111111111", 2);
        IntPtr ptrWnd;
      
        
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
        public const int WM_SENDMYREALY_1 = USER + 115;
        public const int WM_SENDMYREALY_2 = USER + 116;

        #endregion
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        private IniParser.Model.IniData dt;
        public myrelaysetter()
        {
            InitializeComponent();
            ptrWnd = FindWindow(null, glob_ini_instance.getInstance().getSetupIniData["setproduct"]["name"]);
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "off")
            {
                button1.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 0));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button1.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 0));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Text == "off")
            {
                button2.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 1));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button2.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 1));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (button3.Text == "off")
            {
                button3.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 2));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button3.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 2));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (button4.Text == "off")
            {
                button4.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 3));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button4.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 3));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "off")
            {
                button5.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 4));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button5.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 4));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "off")
            {
                button6.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 5));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button6.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 5));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "off")
            {
                button7.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 6));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button7.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 6));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "off")
            {
                button8.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 7));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button8.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 7));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "off")
            {
                button9.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 8));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button9.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 8));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "off")
            {
                button10.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 9));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button10.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 9));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (button11.Text == "off")
            {
                button11.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 10));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button11.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 10));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "off")
            {
                button12.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 11));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button12.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 11));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (button13.Text == "off")
            {
                button13.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 12));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button13.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 12));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");




            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (button14.Text == "off")
            {
                button14.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 13));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button14.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 13));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (button15.Text == "off")
            {
                button15.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 14));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button15.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 14));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (button16.Text == "off")
            {
                button16.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 15));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button16.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 15));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (button17.Text == "off")
            {
                button17.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 16));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button17.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 16));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (button18.Text == "off")
            {
                button18.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 17));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button18.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 17));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (button19.Text == "off")
            {
                button19.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 18));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button19.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 18));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (button20.Text == "off")
            {
                button20.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 19));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button20.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 19));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");




            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button21.Text == "off")
            {
                button21.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 20));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button21.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 20));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");




            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (button22.Text == "off")
            {
                button22.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 21));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button22.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 21));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (button23.Text == "off")
            {
                button23.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 22));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button23.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 22));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (button24.Text == "off")
            {
                button24.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 23));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button24.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 23));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (button25.Text == "off")
            {
                button25.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 24));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button25.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 24));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (button26.Text == "off")
            {
                button26.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 25));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button26.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 25));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (button27.Text == "off")
            {
                button27.Text = "on";

                relay_rec = (uint)(relay_rec & ~(1 << 26));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button27.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 26));
                SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@" + Convert.ToString(relay_rec, 2).PadLeft(27, '0') + "@");





            }
        }

        //---------------
        private void button28_Click(object sender, EventArgs e)
        {

            if (button28.Text == "off")
            {
                button28.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 0));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button28.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 0));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }

        }

        private void button29_Click(object sender, EventArgs e)
        {

            if (button29.Text == "off")
            {
                button29.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 1));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button29.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 1));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (button30.Text == "off")
            {
                button30.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 2));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button30.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 2));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (button31.Text == "off")
            {
                button31.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 3));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button31.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 3));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (button32.Text == "off")
            {
                button32.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 4));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button32.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 4));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (button33.Text == "off")
            {
                button33.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 5));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button33.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 5));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (button34.Text == "off")
            {
                button34.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 6));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button34.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 6));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (button35.Text == "off")
            {
                button35.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 7));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button35.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 7));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (button36.Text == "off")
            {
                button36.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 8));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button36.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 8));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (button37.Text == "off")
            {
                button37.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 9));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button37.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 9));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (button38.Text == "off")
            {
                button38.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 10));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button38.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 10));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (button39.Text == "off")
            {
                button39.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 11));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button39.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 11));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (button40.Text == "off")
            {
                button40.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 12));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button40.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 12));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (button41.Text == "off")
            {
                button41.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 13));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button41.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 13));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (button42.Text == "off")
            {
                button42.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 14));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button42.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 14));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (button43.Text == "off")
            {
                button43.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 15));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button43.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 15));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (button44.Text == "off")
            {
                button44.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 16));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button44.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 16));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (button45.Text == "off")
            {
                button45.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 17));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button45.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 17));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (button46.Text == "off")
            {
                button46.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 18));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button46.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 18));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (button47.Text == "off")
            {
                button47.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 19));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button47.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 19));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (button48.Text == "off")
            {
                button48.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 20));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button48.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 20));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (button49.Text == "off")
            {
                button49.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 21));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button49.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 21));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (button50.Text == "off")
            {
                button50.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 22));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button50.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 22));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (button51.Text == "off")
            {
                button51.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 23));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button51.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 23));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (button52.Text == "off")
            {
                button52.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 24));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button52.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 24));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (button53.Text == "off")
            {
                button53.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 25));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button53.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 25));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (button54.Text == "off")
            {
                button54.Text = "on";

                relay_rec2 = (uint)(relay_rec2 & ~(1 << 26));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");

            }
            else
            {

                button54.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 26));
                SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@" + Convert.ToString(relay_rec2, 2).PadLeft(27, '0') + "@");





            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

            SendMessage(ptrWnd, WM_SENDMYREALY_2, IntPtr.Zero, "@111111111111111111111111111@");

            SendMessage(ptrWnd, WM_SENDMYREALY_1, IntPtr.Zero, "@111111111111111111111111111@");
        }
    }


}

