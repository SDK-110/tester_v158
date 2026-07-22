using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp
{
    public delegate void callback(string[] args);


    public partial class message : Form
    {
        public static string  regstr ="";
        public IntPtr ptrWnd;
        #region /*sendmessage dll 庫加載*/

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
        /*跨线程消息*/
        static int USER = 0x0400;
        int WM_SENDA = USER + 101;
        int WM_SENDB = USER + 102;
        int WM_SENDC = USER + 103;
        int WM_SENDD = USER + 104;
        int WM_SEND_SET_CC1310LOSS = USER + 110;
        int WM_SEND_SET_BTLOSS = USER + 111;
        int WM_SEND_SET_WIFILOSS = USER + 112;
        int WM_SEND_AUTOTEST = USER + 113;
        #endregion

        public callback diaoyong;
        #region  /*-------------LOOP FUNCTION BACKPROC-----------*/
        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        protected override void DefWndProc(ref Message ms)
        {

            if (ms.Msg == WM_SENDA)
            {

            }

            base.DefWndProc(ref ms);
        }
        #endregion

        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        public bool isselect = false;



        public volatile string return_v;
        public message(string  prompt,string otherprompt="请按照规则输入序列：")
        {
            this.ControlBox = false;
            InitializeComponent();
            ptrWnd = FindWindow(null, this.Text);
            this.Text = prompt;
            this.label1.Text = otherprompt;

        }

  

        private void mesgbox_dealwith(string[] args)
        {

        }

        private void message_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 16; i++)
            {

                comboBox1.Items.Add(i);

            }

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            return_v = comboBox1.Text;
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {





        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {




            if (e.KeyCode == Keys.Enter)
            {
             
                MatchCollection reg = new Regex(regstr.Trim()).Matches(this.textBox1.Text);

                //  MessageBox.Show("Test-->" + reg.Count + "-->" + this.textBox1.Text);
                //  return;
                // if (dt["setbarcode"]["barlen"] == this.textBox1.Text.Length.ToString() && dt["setbarcode"]["barenable"] == "true")
                if (reg.Count > 0)

                {

                    return_v = textBox1.Text;
                    isselect = true;
                    this.Hide();
                }
                else{

                    textBox1.Text="错误，请重新输入";
                    textBox1.SelectAll();
                }









            }







        }
    }

}
