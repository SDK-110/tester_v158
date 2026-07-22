using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace testapp
{

    enum status_test_488 {
        test_nodef,
        test_fail,
        Test_mode_didnot_start,
        motor_speed0_to_7_zero_crossing_All_passed,
        motor_speed_off_failed,
        motor_speed1_failed,
        motor_speed2_failed,
        motor_speed3_failed,
        motor_speed4_failed,
        motor_speed5_failed,
        motor_speed6_failed,
        motor_speed7_failed,
        zero_crossing_failed
    }
    class babyhappy_com : SerialPort
    {

        #region /*sendmessage dll 庫加載*/
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        string c = "";
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
        public static  IntPtr ptrWnd;
        // const string nameflog = "season-rf-test-qa:";
        const string nameflog = "season-rf-test-qa:";
        //string nameflog = "root@imx6snooplus-wifi-rf-test-mfg:";
        const string login = "login:";
        volatile int loopwaitflog = 0;
        StringBuilder str_rev =  new StringBuilder();
        volatile int wait_timeout = -1;
        volatile int loopcount = 0;
         
        private void setinit() {
           // this.ReadExisting();
            wait_timeout = -1;
            loopwaitflog = 0;
          if(str_rev.ToString().Length>500) str_rev.Clear();
            if (loopcount > 10) loopcount = 0;
            
        }

        void waiting_lp(int count) {

            int ct = count;
            do
            {
                System.Threading.Thread.Sleep(100);
                if (ct == 0) {

                    this.Write(new byte[] { 0x03 }, 0, 1);
                    this.Write(new byte[] { 0x1A }, 0, 1); /* 发送终止符号结束进程“Ctrl+C、Ctrl+Z对应的0x03和0x1A*/
                    wait_timeout = 1;
                    this.WriteLine("");
                }
            } while (loopwaitflog == 0 && ct-- >= 0);
        
        }
        public babyhappy_com( string port, int baudrate=9600) : base(port)
        {
            

            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += comm_DataReceived;
            str_rev.Capacity = 4056;
 
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
          //  SerialPort sp = (SerialPort)sender;
          //  recebuf = sp.ReadExisting();
        }


        public bool send_command_and_find_str_1(string command, string findstr,int delay=10) {

            setinit();
           
            this.WriteLine(command);
            System.Threading.Thread.Sleep(100);
            int m = -1;
            int count = 0;
            do
            {
                waiting_lp(delay);
               
                m = str_rev.ToString().IndexOf(findstr);

            } while (m < 0 && count++<5);

           

            string ruf = str_rev.ToString();

            str_rev.Clear();
            if (m >= 0) return true;
            else return false;


        }


        public string send_command_and_find_str(string command, string findstr, int delay = 10)
        {
            str_rev.Clear();
            setinit();
            this.WriteLine("");
            System.Threading.Thread.Sleep(100);
            this.WriteLine(command);
            System.Threading.Thread.Sleep(100);
            int m = -1;
            int count = 0;
            MatchCollection matchs;
            do
            {
                waiting_lp(delay);

               
                Regex rex = new Regex(findstr, RegexOptions.IgnoreCase);
                matchs = rex.Matches(str_rev.ToString());
                m = matchs.Count;
              
                

            } while (m < 0 && count++ < 5);



         

            str_rev.Clear();
            if (m <=0) { return "NULL"; }
            else {

                return matchs[0].Groups[1].Value;
            }


        }




        #region 知识储备库忽略
        /*
                public void set_sing_relay(byte relay_num, byte openorclose) {

                    Byte[] a = new Byte[] { 0x05, 0x01, 0X00,(byte)(relay_num-1),(byte)(openorclose),0x00 };

                    byte[] commend = tan_modbus(a);

                    int count = 0;
                    Byte[] m = new byte[commend.Length];

                    do
                    {
                        try
                        {
                            this.Write(commend, 0, commend.Length);
                            this.Read(m, 0, commend.Length);

                        }
                        catch (Exception)
                        {

                            count++;
                            if (count > 3) {

                                System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                                return;
                            }

                        }
                    } while (count != 0);


                }
                public void set_relay(byte relay_1_8, byte relay_9_16)
                {

                    Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

                    byte[] commend = tan_modbus(a);

                    int count = 0;
                    Byte[] m = new byte[commend.Length];

                    do
                    {
                        try
                        {
                            this.Write(commend, 0, commend.Length);
                            this.Read(m, 0, commend.Length);

                        }
                        catch (Exception)
                        {
                            count++;
                            if (count > 3)
                            {

                                System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                                return;
                            }

                        }
                    } while (count != 0 && count < 3);


                }
                // crc校验函数
                private   UInt16 crc16(Byte[] ptr)
                { return ModbusCrc16.Compute(ptr); }

                private  Byte[] tan_modbus(Byte[] data)
                { return ModbusCrc16.AppendCrc(data); }
        */
        #endregion
        ~babyhappy_com()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
                setinit();
                var rever = ((SerialPort)sender);
               
                System.Threading.Thread.Sleep(200);
                int m = rever.BytesToRead ;
                string tempstr = rever.ReadExisting();
                callbackdebuginfo(tempstr.Replace("Krovatka3!!S", "*******") + "\n\r");
                str_rev.Append(tempstr);
                if (tempstr.IndexOf(nameflog) >= 0) { loopwaitflog = 1; }
                if (tempstr.IndexOf("wlan0: link is not ready") >= 0) { rever.WriteLine(""); }
                if (tempstr.IndexOf(login) >= 0) {
                    if (loopcount == 0) {  System.Threading.Thread.Sleep(2000);}
                    System.Threading.Thread.Sleep(100);
                    rever.WriteLine("");
                    System.Threading.Thread.Sleep(100);
                    rever.WriteLine("");
                    System.Threading.Thread.Sleep(100);
                    rever.WriteLine("root");
                    System.Threading.Thread.Sleep(150);
                   rever.WriteLine("(Krovatka3!!S)");
                    System.Threading.Thread.Sleep(500);
                    rever.WriteLine("");
                    //loopwaitflog = 1;
                   
                }
                //if (tempstr.IndexOf("Password:") >= 0) {

                //    System.Threading.Thread.Sleep(10);
                //    rever.WriteLine("(Krovatka3!!S)");

                //}
               

            }
            catch( Exception a)
            {
               

            }


        }

        public void callbackdebuginfo(string m)
        {

            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }

    }

}

