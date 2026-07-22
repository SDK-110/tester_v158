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
    public partial class coil_project_4_unit : Form
    {
        EventHandler handler = null;
        ulong countflog = 0;
        byte[] buf_window = new byte[4];
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
                        ini_data["setproduct"]["MACADDR"] = retdate;

                        glob_ini_instance.getInstance().write2Ini(ini_data);


                    }

                    break;

                case WM_SENDA:
                    {
                        string tmp = "";
                        if (ini_data["setport"]["Relay_board"] != null)
                        {
                            if (dll != null)
                                dll.Getfun()["relay_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                        //  MessageBox.Show(Marshal.PtrToStringAnsi(ms.LParam));
                        //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);

                        // dt["cmw100ParameterSet"]["buletoothloss"] = Marshal.PtrToStringAnsi(m.LParam);
                        //  glob_ini_instance.getInstance().write2Ini(dt);


                    }
                    break;
                case WM_SENDA_2:
                    {

                        string tmp = "";
                        if (ini_data["setport"]["Relay_board2"] != null)
                        {
                            if (dll != null)
                                dll.Getfun()["relay2_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_3:
                    {

                        string tmp = "";
                        if (ini_data["setport"]["Relay_board3"] != null)
                        {
                            if(dll!=null)
                            dll.Getfun()["relay3_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDA_4:
                    {

                        MessageBox.Show(Marshal.PtrToStringAnsi(ms.LParam));
                        string tmp = "";
                        if (ini_data["setport"]["Relay_board4"] != null)
                        {
                            if (dll != null)
                                dll.Getfun()["relay4_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                        }



                    }
                    break;
                case WM_SENDB:
                    {
                        //MessageBox.Show(Marshal.PtrToStringAnsi(m.LParam));
                        //this.textBox1.Text = Marshal.PtrToStringAnsi(m.LParam);
                        // dt["cmw100ParameterSet"]["cc1310loss"] = Marshal.PtrToStringAnsi(m.LParam);
                        // glob_ini_instance.getInstance().write2Ini(dt);

                        //this.richTextBox1.AppendText(Marshal.PtrToStringAnsi(ms.LParam) + "\n");
                        //this.richTextBox1.ScrollToCaret();
                    }
                    break;
                case WM_SENDD:
                    {
                        this.Close();
                    }
                    break;
                case WM_SENDC:
                    {
                        string[] retdate = Marshal.PtrToStringAnsi(ms.LParam).Split(";".ToArray());
                        ini_data["cmw100statuscheck"]["statusyear"] = retdate[0];
                        ini_data["cmw100statuscheck"]["statusmonth"] = retdate[1];
                        ini_data["cmw100statuscheck"]["statusday"] = retdate[2];
                        ini_data["cmw100statuscheck"]["statushour"] = retdate[3];
                        glob_ini_instance.getInstance().write2Ini(ini_data);


                    }
                    break;

                case WM_SENDMYREALY_1:
                    {
                        if (ini_data["setport"]["myrelay_board"] != null)
                        {

                            string temp;
                            if (dll != null)
                                dll.Getfun()["myrelay_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }

                    }
                    break;
                case WM_SENDMYREALY_2:
                    {
                        if (ini_data["setport"]["myrelay_board2"] != null)
                        {
                            string temp;
                            if(dll!=null)
                            dll.Getfun()["myrelay_set2"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
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

        testcase_dll dll;

         IniParser.FileIniDataParser iniread;
           IniParser.Model.IniData ini_data;
        
        public coil_project_4_unit()
        {
            InitializeComponent();
            ini_data = glob_ini_instance.getInstance().getSetupIniData;
             iniread = glob_ini_instance.getInstance().fileIni;
        }

        private void coil_project_4_unit_Load(object sender, EventArgs e)
        {
            dll = new testcase_dll();
            dut4.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet4");
            dut3.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet3");
            dut1.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet1");
            dut2.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet2");
            dut4.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet1");
            dut4.deal_withmsg += deal_withmsgfun;
            dut3.deal_withmsg += deal_withmsgfun;
            dut2.deal_withmsg += deal_withmsgfun;
            dut1.deal_withmsg += deal_withmsgfun;


            dut4.set_production_info(new production_info() { log_path_name = "11111111111.csv" });
            dut3.set_production_info(new production_info() { log_path_name = "22222222.csv" });
            dut1.set_production_info(new production_info() { log_path_name = "333333.csv" });
            dut2.set_production_info(new production_info() { log_path_name = "444444.csv" });
            //   userControl21.cancel_run();
            // userControl21.run();
            timer1.Enabled = true;
            handler = defToolStripMenuItem_DoubleClick;
        }

        private void deal_withmsgfun(object sender, EventArgs e)
        {
            lock (this) { 
            var m = sender as msgpacketer;

            if (m.state_num == msg_type.pass_fail_count) {

                ini_data["recorder"]["title"] = (int.Parse(ini_data["recorder"]["title"]) + 1).ToString();
                    glob_ini_instance.getInstance().write2Ini(ini_data);

                if (m.msg == "pass")
                {

          
                        ini_data["recorder"]["titleok"] = (int.Parse(ini_data["recorder"]["titleok"]) + 1).ToString();
                        glob_ini_instance.getInstance().write2Ini(ini_data);

                }
                else {

                ini_data["recorder"]["titleng"] = (int.Parse(ini_data["recorder"]["titleng"]) + 1).ToString();
                glob_ini_instance.getInstance().write2Ini(ini_data);
            }

                    this.toolStripStatusLabel1.Text = string.Format("TOTAL：[{0}PCS]  |  NG :[{1}PCS] |  OK:[{2}PCS]", ini_data["recorder"]["title"], ini_data["recorder"]["titleng"], ini_data["recorder"]["titleok"]);
                

            }
            }


        }

        private void defToolStripMenuItem_DoubleClick(object sender, EventArgs e)
        {

           
           
            dut4.run();
            dut3.run();
            dut1.run();
            dut2.run();
        }

        private void debugrelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
             relay_debug_4  relay4  = relay_debug_4.get_instance();
          //  relay4.Show();
        }
        public void set_relay(int relay, string setstr) {

            if (dll != null) {
                string c;
                dll.Getfun()["relay" + ((relay==1)?"":(relay.ToString()))+ "_set" ]("pass","pass",out c ,setstr);

            }

        }

        private void coil_project_4_unit_Shown(object sender, EventArgs e)
        {
            this.Text = ini_data["setproduct"]["name"];

            this.toolStripStatusLabel1.Text = string.Format("TOTAL：[{0}PCS]  |  NG :[{1}PCS] |  OK:[{2}PCS]", ini_data["recorder"]["title"], ini_data["recorder"]["titleng"], ini_data["recorder"]["titleok"]);
        }

        private void edittestcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fr = TestcaseEdit4.get_instance();
            fr.is_called_by_other = update_view;
            fr.Show();
        }

        private void update_view(object sender, EventArgs e)
        {
            dut4.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet4");
            dut3.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet3");
            dut1.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet1");
            dut2.set_init_4runlib_testcase(ref dll, testcase_table_sel: "sheet2");
        }

        private void clearcounterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ini_data["recorder"]["title"] = 0.ToString();
            ini_data["recorder"]["titleok"] = 0.ToString();
            ini_data["recorder"]["titleng"] = 0.ToString();
            glob_ini_instance.getInstance().write2Ini(ini_data);
            this.toolStripStatusLabel1.Text = string.Format("TOTAL：[{0}PCS]  |  NG :[{1}PCS] |  OK:[{2}PCS]", ini_data["recorder"]["title"], ini_data["recorder"]["titleng"], ini_data["recorder"]["titleok"]);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int tmp = dll.USB2185_as_auto_det();
            if (tmp == 1 || tmp == 0) {

                buf_window[countflog % 4] = (byte)tmp;
                countflog++;
            }

            if (countflog >= 4) {


                int t = buf_window[0] ^ buf_window[1] ^ buf_window[2] ^ buf_window[3];

                if (t > 0 && tmp==1) {

                    if (handler != null) {

                        handler(this, null);

                    }

                }



            }

            

        }
    }


}
