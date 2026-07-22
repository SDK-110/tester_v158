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
    public partial class innover_relay_32 : Form
    {
        private static innover_relay_32 instance_object = null;
        volatile UInt32 relay_rec = Convert.ToUInt32("111111111111111111111111111", 2);
        volatile UInt32 relay_rec2 = Convert.ToUInt32("111111111111111111111111111", 2);
        IntPtr ptrWnd;

        public static innover_relay_32 get_instaance() {

            if (instance_object == null) {

                instance_object = new innover_relay_32();
            }
            instance_object.Show();
            return instance_object;
        }
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

        public const int WM_INNOVE_RELAY1_SET = USER + 126;
        public const int WM_INNOVE_RELAY2_SET = USER + 127;

        #endregion
       
        private IniParser.Model.IniData iniData =  glob_ini_instance.getInstance().getSetupIniData;
        private innover_relay_32()
        {
            InitializeComponent();
            ptrWnd = FindWindow(null, iniData["setproduct"]["name"]);
        }

        public void set_main_ptr(IntPtr wind) { 
        
        
        this.ptrWnd = wind;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (button1.Text == "off")
            {
                button1.Text = "on";

              
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$1:1$");

            }
            else
            {

                button1.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$1:0$");



            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (button2.Text == "off")
            {
                button2.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$2:1$");

            }
            else
            {

                button2.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$2:0$");





            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (button3.Text == "off")
            {
                button3.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$3:1$");

            }
            else
            {

                button3.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$3:0$");





            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (button4.Text == "off")
            {
                button4.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$4:1$");

            }
            else
            {

                button4.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$4:0$");





            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button5.Text == "off")
            {
                button5.Text = "on";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$5:1$");

            }
            else
            {

                button5.Text = "off";

                relay_rec = (uint)(relay_rec | (1 << 4));
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$5:0$");





            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (button6.Text == "off")
            {
                button6.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$6:1$");

            }
            else
            {

                button6.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$6:0$");





            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (button7.Text == "off")
            {
                button7.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$7:1$");

            }
            else
            {

                button7.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$7:0$");





            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "off")
            {
                button8.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$8:1$");

            }
            else
            {

                button8.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$8:0$");





            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "off")
            {
                button9.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$9:1$");

            }
            else
            {

                button9.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$9:0$");





            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "off")
            {
                button10.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$10:1$");

            }
            else
            {

                button10.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$10:0$");





            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (button11.Text == "off")
            {
                button11.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$11:1$");

            }
            else
            {

                button11.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$11:0$");





            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (button12.Text == "off")
            {
                button12.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$12:1$");

            }
            else
            {

                button12.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$12:0$");





            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (button13.Text == "off")
            {
                button13.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$13:1$");

            }
            else
            {

                button13.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$13:0$");




            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (button14.Text == "off")
            {
                button14.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$14:1$");

            }
            else
            {

                button14.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$14:0$");





            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (button15.Text == "off")
            {
                button15.Text = "on";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$15:1$");

            }
            else
            {

                button15.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$15:0$");





            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (button16.Text == "off")
            {
                button16.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$16:1$");

            }
            else
            {

                button16.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$16:0$");





            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (button17.Text == "off")
            {
                button17.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$17:1$");

            }
            else
            {

                button17.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$17:0$");





            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (button18.Text == "off")
            {
                button18.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$18:1$");

            }
            else
            {

                button18.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$18:0$");





            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (button19.Text == "off")
            {
                button19.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$19:1$");

            }
            else
            {

                button19.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$19:0$");





            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (button20.Text == "off")
            {
                button20.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$20:1$");

            }
            else
            {

                button20.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$20:0$");




            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (button21.Text == "off")
            {
                button21.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$21:1$");

            }
            else
            {

                button21.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$21:0$");




            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (button22.Text == "off")
            {
                button22.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$22:1$");

            }
            else
            {

                button22.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$22:0$");





            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (button23.Text == "off")
            {
                button23.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$23:1$");

            }
            else
            {

                button23.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$23:0$");





            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (button24.Text == "off")
            {
                button24.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$24:1$");

            }
            else
            {

                button24.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$24:0$");





            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (button25.Text == "off")
            {
                button25.Text = "on";
                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$25:1$");

            }
            else
            {

                button25.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$25:0$");





            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (button26.Text == "off")
            {
                button26.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$26:1$");

            }
            else
            {

                button26.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$26:0$");





            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (button27.Text == "off")
            {
                button27.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$27:1$");

            }
            else
            {

                button27.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$27:0$");





            }
        }

        //---------------
        private void button28_Click(object sender, EventArgs e)
        {




            if (button28.Text == "off")
            {
                button28.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$1:1$");

            }
            else
            {

                button28.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$1:0$");





            }

        }

        private void button29_Click(object sender, EventArgs e)
        {

            if (button29.Text == "off")
            {
                button29.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$2:1$");

            }
            else
            {

                button29.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$2:0$");





            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (button30.Text == "off")
            {
                button30.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$3:1$");

            }
            else
            {

                button30.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$3:0$");





            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (button31.Text == "off")
            {
                button31.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$4:1$");

            }
            else
            {

                button31.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$4:0$");





            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (button32.Text == "off")
            {
                button32.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$5:1$");

            }
            else
            {

                button32.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$5:0$");





            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (button33.Text == "off")
            {
                button33.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$6:1$");

            }
            else
            {

                button33.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$6:0$");





            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (button34.Text == "off")
            {
                button34.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$7:1$");

            }
            else
            {

                button34.Text = "off";
                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$7:0$");





            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (button35.Text == "off")
            {
                button35.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$8:1$");

            }
            else
            {

                button35.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$8:0$");




            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (button36.Text == "off")
            {
                button36.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$9:1$");

            }
            else
            {

                button36.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$9:0$");





            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (button37.Text == "off")
            {
                button37.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$10:1$");

            }
            else
            {

                button37.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$10:0$");





            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (button38.Text == "off")
            {
                button38.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$11:1$");

            }
            else
            {

                button38.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$11:0$");





            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (button39.Text == "off")
            {
                button39.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$12:1$");

            }
            else
            {

                button39.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$12:0$");





            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (button40.Text == "off")
            {
                button40.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$13:1$");

            }
            else
            {

                button40.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$13:0$");





            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (button41.Text == "off")
            {
                button41.Text = "on";
                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$14:1$");

            }
            else
            {

                button41.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$14:0$");





            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (button42.Text == "off")
            {
                button42.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$15:1$");

            }
            else
            {

                button42.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$15:0$");





            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (button43.Text == "off")
            {
                button43.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$16:1$");

            }
            else
            {

                button43.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$16:0$");





            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (button44.Text == "off")
            {
                button44.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$17:1$");

            }
            else
            {

                button44.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$17:0$");





            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (button45.Text == "off")
            {
                button45.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$18:1$");

            }
            else
            {

                button45.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$18:0$");





            }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (button46.Text == "off")
            {
                button46.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$19:1$");

            }
            else
            {

                button46.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$19:0$");





            }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (button47.Text == "off")
            {
                button47.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$20:1$");

            }
            else
            {

                button47.Text = "off";

                relay_rec2 = (uint)(relay_rec2 | (1 << 19));
                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$20:0$");





            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (button48.Text == "off")
            {
                button48.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$21:1$");

            }
            else
            {

                button48.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$21:0$");





            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (button49.Text == "off")
            {
                button49.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$22:1$");

            }
            else
            {

                button49.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$22:0$");




            }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            if (button50.Text == "off")
            {
                button50.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$23:1$");

            }
            else
            {

                button50.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$23:0$");





            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            if (button51.Text == "off")
            {
                button51.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$24:1$");

            }
            else
            {

                button51.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$24:0$");





            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            if (button52.Text == "off")
            {
                button52.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$25:1$");

            }
            else
            {

                button52.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$25:0$");





            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            if (button53.Text == "off")
            {
                button53.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$26:1$");

            }
            else
            {

                button53.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$26:0$");





            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            if (button54.Text == "off")
            {
                button54.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$27:1$");

            }
            else
            {

                button54.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$27:0$");





            }
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {

            SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32:0$");

            SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32:0$");
            this.Hide();
            e.Cancel = true;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (button55.Text == "off")
            {
                button55.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$28:1$");

            }
            else
            {

                button55.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$28:0$");





            }
        }

        private void button56_Click(object sender, EventArgs e)
        {
            if (button56.Text == "off")
            {
                button56.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$29:1$");

            }
            else
            {

                button56.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$29:0$");





            }
        }

        private void button57_Click(object sender, EventArgs e)
        {
            if (button57.Text == "off")
            {
                button57.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$30:1$");

            }
            else
            {

                button57.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$30:0$");





            }
        }

        private void button58_Click(object sender, EventArgs e)
        {
            if (button58.Text == "off")
            {
                button58.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$31:1$");

            }
            else
            {

                button58.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$31:0$");





            }
        }

        private void button59_Click(object sender, EventArgs e)
        {
            if (button59.Text == "off")
            {
                button59.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$32:1$");

            }
            else
            {

                button59.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY1_SET, IntPtr.Zero, "$32:0$");





            }
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (button64.Text == "off")
            {
                button64.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$28:1$");

            }
            else
            {

                button64.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$28:0$");





            }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            if (button63.Text == "off")
            {
                button63.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$29:1$");

            }
            else
            {

                button63.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$29:0$");





            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            if (button62.Text == "off")
            {
                button62.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$30:1$");

            }
            else
            {

                button62.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$30:0$");





            }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            if (button61.Text == "off")
            {
                button61.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$31:1$");

            }
            else
            {

                button61.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$31:0$");





            }
        }

        private void button60_Click(object sender, EventArgs e)
        {
            if (button60.Text == "off")
            {
                button60.Text = "on";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$32:1$");

            }
            else
            {

                button60.Text = "off";

                SendMessage(ptrWnd, WM_INNOVE_RELAY2_SET, IntPtr.Zero, "$32:0$");





            }
        }
    }


}

