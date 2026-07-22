using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.glob_set;
using VMPro;
using WeifenLuo.WinFormsUI.Docking;
using 重构程序.viewmode;

namespace testapp.duochuangti
{
    public partial class main_m : Form
    {
        
        public main_m()
        {
            InitializeComponent();
            mylib.utility_func.add_msg(debug_form.GetDebug_f_instance().write_msg);
        }
       testcase_dll dll = new testcase_dll();
        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void main_m_Load(object sender, EventArgs e)
        {
           
          
            Application.DoEvents();
            string configFile = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DockPanel.config");
            if (File.Exists(configFile))
            {
                this.dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);

                //char_form char_f = char_form.get_form_instance();
                //char_f.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            }
            else {





                test1_form dut1 = test1_form.get_form_instance();
                test2_form dut2 = test2_form.get_form_instance();
                input_form trigger_Form = input_form.GetTrigger_Form_instance();
                debug_form debug_f = debug_form.GetDebug_f_instance();
                char_form char_f = char_form.get_form_instance();
                dut1.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockTop);
                dut2.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockTop);
                trigger_Form.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
                debug_f.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
                char_f.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);

                dockPanel1.DockRightPortion = dockPanel1.Width * 1 / 3;
                dockPanel1.DockLeftPortion = dockPanel1.Width * 1 / 3;




            }

            test2_form.get_form_instance().set_ini(dll);
            test1_form.get_form_instance().set_ini(dll);
            
        }



        private void main_m_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dll != null) dll.Getfun()["releaseport"]("pass", "pass", out _);

            }
            catch { };
           
            string configFile = Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "DockPanel.config");
            this.dockPanel1.SaveAsXml(configFile);
           
        }

        private DeserializeDockContent m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        /// <summary>
        /// 配置委托函数
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private static IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(test1_form).ToString())
            {
                return test1_form.get_form_instance();
            }
            else if (persistString == typeof(test2_form).ToString())
            {
                return test2_form.get_form_instance();
            }
            else if (persistString == typeof(debug_form).ToString())
            {
                return debug_form.GetDebug_f_instance();
            }
            else if (persistString == typeof(input_form).ToString())
            {
                return input_form.GetTrigger_Form_instance();
            }
            else if (persistString == typeof(char_form).ToString())
            {
                return char_form.get_form_instance();
            }

            else
            {
                return null;
            }
        }

        private void main_m_Resize(object sender, EventArgs e)
        {
          
        }

        private void main_m_Shown(object sender, EventArgs e)
        {
            ptrWnd = FindWindow(null, this.Text);
             inidata= glob_ini_instance.getInstance().getSetupIniData;
            
            this.Text = inidata["setproduct"]["project"];
        }

        private void materialToolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            test1_form dut1 = test1_form.get_form_instance();
            test2_form dut2 = test2_form.get_form_instance();
            input_form trigger_Form = input_form.GetTrigger_Form_instance();
            debug_form debug_f = debug_form.GetDebug_f_instance();
            char_form char_f = char_form.get_form_instance();
            dut1.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockTop);
            dut2.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockTop);
            trigger_Form.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
            debug_f.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);
            char_f.Show(this.dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockBottom);

            dockPanel1.DockRightPortion = dockPanel1.Width * 1 / 3;
            dockPanel1.DockLeftPortion = dockPanel1.Width * 1 / 3;




        }

        private void setdebugrelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            relay_debug_4 debug_relay = relay_debug_4.get_instance();
            debug_relay.set_main_win_ptr(ptrWnd);
            debug_relay.Show();
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
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;
        public const int WM_SEND_RF_REF = USER + 114;
        public const int WM_SENDMYREALY_1 = USER + 115;
        public const int WM_SENDMYREALY_2 = USER + 116;
        public const int WM_SENDMACSAVE = USER + 117;
        public const int WM_BLE_PATH_LOSS_CH0 = USER + 118;
        public const int WM_BLE_PATH_LOSS_CH20 = USER + 119;
        public const int WM_BLE_PATH_LOSS_CH39 = USER + 120;
        public const int WM_TEST_TRIGGER_RUN = USER + 121;
        public const int WM_SENDA_4 = USER + 122;
        public const int WM_SK_RELAY1_SET = USER + 123;
        public const int WM_SK_RELAY2_SET = USER + 124;
        public const int WM_CHANGE_TEXT_BOX1 = USER + 125;
        IntPtr ptrWnd;


        #endregion
        /*--------------message loop dll upload-------------*/

        #region  /*-------------LOOP FUNCTION BACKPROC-----------*/
        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        protected override void DefWndProc(ref Message ms)
        {


            switch (ms.Msg)
            {

                case WM_SENDMACSAVE:
                    {

                        string retdate = Marshal.PtrToStringAnsi(ms.LParam);
                        inidata["setproduct"]["MACADDR"] = retdate;

                        glob_ini_instance.getInstance().write2Ini(inidata);


                    }

                    break;


                case WM_SENDA:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board"] != null)
                        {

                            dll.Getfun()["relay_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                        //  MessageBox.Show(Marshal.PtrToStringAnsi(ms.LParam));
                        //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);

                        // dt["cmw100ParameterSet"]["buletoothloss"] = Marshal.PtrToStringAnsi(m.LParam);
                        //   glob_ini_instance.getInstance().write2Ini(dt);


                    }
                    break;
                case WM_SENDA_2:
                    {

                        string tmp = "";
                        if (inidata["setport"]["Relay_board2"] != null)
                        {

                            dll.Getfun()["relay2_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_3:
                    {

                        string tmp = "";
                        if (inidata["setport"]["Relay_board3"] != null)
                        {

                            dll.Getfun()["relay3_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_4:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board4"] != null)
                        {

                            dll.Getfun()["relay4_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }


                    }
                    break;

                case WM_SENDD:
                    {
                        this.Close();
                    }
                    break;


                case WM_SENDMYREALY_1:
                    {
                        if (inidata["setport"]["myrelay_board"] != null)
                        {

                            string temp;
                            dll.Getfun()["myrelay_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                    }
                    break;
                case WM_SENDMYREALY_2:
                    {
                        if (inidata["setport"]["myrelay_board2"] != null)
                        {
                            string temp;
                            dll.Getfun()["myrelay_set2"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                    }
                    break;
                case WM_SK_RELAY1_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board"] != null)
                        {
                            string temp;
                            dll.Getfun()["sk_relay1_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }


                    }
                    break;
                case WM_SK_RELAY2_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board2"] != null)
                        {
                            string temp;
                            dll.Getfun()["sk_relay2_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;

                default:
                    break;
            }

            base.DefWndProc(ref ms);
        }
        #endregion
        /*-------------LOOP FUNCTION BACKPROC-----------*/

        private IniParser.FileIniDataParser iniread = glob_ini_instance.getInstance().fileIni;
        IniParser.Model.IniData inidata;

        private void setdebugrelayseries2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sk_relay32 relay_4_16c = sk_relay32.get_instaance();
            relay_4_16c.set_main_ptr(ptrWnd);
            relay_4_16c.Show(); 
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            char_form.get_form_instance().clear_data();
            
        }

        private void loopTestTool4dut1StripMenuItem_Click(object sender, EventArgs e)
        {
            if (loopTestToolStripMenuItem.Checked == false)
            {

                loopTestToolStripMenuItem.Checked = true;
                test1_form.get_form_instance().loop_flog = 1;
            }
            else {

                loopTestToolStripMenuItem.Checked = false;
                test1_form.get_form_instance().loop_flog = 0;


            }
           
        }
    }
}
