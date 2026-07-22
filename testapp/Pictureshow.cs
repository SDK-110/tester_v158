using rebuild.testcase_loader;
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
using test_antdui;

namespace testapp
{
    public delegate int action_callback();
    public partial class Pictureshow : Form
    {
        public static action_callback ok_callback,ng_callback;

        private static Pictureshow instance_obj = null;
        public static string regstr = "";
        public volatile int showflag = 0;
        public IntPtr ptrWnd;
        private DigitalInputMonitor _diMonitor;
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
        [DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        [return:MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hwnd, IntPtr hWndInsertAfter,int x, int y,int cx,int cy,uint uflags);

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
        public volatile bool isselect = false;
        public volatile int select_rsu = 100;
        private string picstr = "2.png";
    
        public static Pictureshow getInstance(){

                   if (instance_obj == null){
                       instance_obj = new Pictureshow();
}
            return instance_obj;

}
        public void show(string prompt, string otherprompt = "请按照规则输入序列：", string picstr = @"2.png") {

            select_rsu = 100;
            this.isselect = false;
            this.picstr = picstr;
            this.label1.Text = otherprompt;
            this.pictureBox1.BackgroundImageLayout= ImageLayout.Zoom;
            this.pictureBox1.BackgroundImage = Image.FromFile(picstr);
           
            this.ShowDialog();
        }
        private  Pictureshow()
        {

            InitializeComponent();
          
        }

        private void Pictureshow_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            select_rsu = 1;
            isselect = true;
            instance_obj.Invoke(new Action(()=> {

                instance_obj.Hide();

            }));
        }

        private void InitDiMonitor()
        {
            try
            {
                string port = testapp.glob_set.glob_ini_instance.getInstance().getSetupIniData["setport"]?["SRND_CM_12DI_port"];
                if (string.IsNullOrEmpty(port)) return;

                _diMonitor = DigitalInputMonitor.Instance;
                _diMonitor.InputRising += OnDiRising;
                _diMonitor.InputFalling += OnDiFalling;
                _diMonitor.ScanError += OnDiError;
                _diMonitor.Start();


            }
            catch { }
        }

        private void OnDiError(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiError(message)));
                return;
            }

            TestLoggerForm.Instance.AddLog($"DI error: {message}", Color.Red);
        }

        private void OnDiFalling(int channel)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiFalling(channel)));
                return;
            }
            TestLoggerForm.Instance.AddLog($"DI#{channel} ↓ (1→0)", Color.FromArgb(245, 34, 45));
        }

        private void OnDiRising(int channel)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiFalling(channel)));
                return;
            }

            if (channel == 1)
            {
                this.Invoke(new Action(() =>
                {
                    ok_click();


                }));
            }
            if (channel == 2)
            {
                this.Invoke(new Action(() =>
                {
                    ng_click();
                }

            ));

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            select_rsu = -1;
            isselect = true;
            instance_obj.Invoke(new Action(() => {

                instance_obj.Hide();

            }));
        }

        #region 窗体拖动
        private static bool IsDrag = false;
        private int enterX;
        private int enterY;
        private void setForm_MouseDown(object sender, MouseEventArgs e)
        {
            IsDrag = true;
            enterX = e.Location.X;
            enterY = e.Location.Y;
        }
        private void setForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseLeave(object sender, EventArgs e)
        {
            IsDrag = false;
            enterX = 0;
            enterY = 0;
        }
        private void setForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrag)
            {
                Left += e.Location.X - enterX;
                Top += e.Location.Y - enterY;
            }
        }
        #endregion

        private void Pictureshow_FormClosing(object sender, FormClosingEventArgs e)
        {
            showflag = 0;
            e.Cancel = true;
        }

        private void Pictureshow_Shown(object sender, EventArgs e)
        {
            this.showflag = 1;
        }

        public  void ok_click()
        {

            if(showflag == 1){

                button1.Invoke(new Action(() => { button1.PerformClick(); }));
            }

        

        }
        public void ng_click() {


            if (showflag == 1)
            {
                button2.Invoke(new Action(() => { button2.PerformClick(); }));

            }
        }
    }
}
