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

   
    class sqlr_ltemodule_serial : SerialPort
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
        
        volatile int loopwaitflog = 0;
        StringBuilder str_rev =  new StringBuilder();
        volatile int wait_timeout = -1;
        volatile int loopcount = 0;
         
        private void setinit() {
           // this.ReadExisting();
            wait_timeout = -1;
            loopwaitflog = 0;
            if (str_rev.ToString().Length > 500) str_rev.Clear();
            if (loopcount > 10) loopcount = 0;

        }

        void waiting_lp(int count) {

            int ct = count;
            do
            {
                System.Threading.Thread.Sleep(100);
                if (ct == 0) {

                    wait_timeout = 1;
  
                }
            } while (loopwaitflog == 0 && ct-- >= 0);
        
        }
        public sqlr_ltemodule_serial( string port, int baudrate=9600) : base(port)
        {
            

            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.NewLine = "\r\n";
            base.RtsEnable = true;
            base.DtrEnable = true;
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
            if (!this.IsOpen) this.Open();
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


        public string send_command_and_return(string command, string findstr="OK", int delay = 10)
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




            string p = str_rev.ToString();
            str_rev.Clear();
            if (m <= 0) { return "NULL"; }
            else
            {

                return p;
            }


        }


        public int  delete_ufs_wav_file() {

            for (int i = 0; i < 3; i++)
            {
                string rsu = send_command_and_find_str(@"AT+QFLST=""A.wav""", @"(ERROR)");
                if (rsu.IndexOf("ERROR") >= 0) return 1;
                rsu = send_command_and_find_str(@"AT+QFLST=""*""", @"(A\.wav)");
                if ( rsu == "NULL") continue;
               rsu = send_command_and_find_str(@"AT+QFDEL=""UFS:A.wav""", "(OK)");
                if (rsu == "NULL") continue;

                return 1;
            }
            return -1;
        }


        //AT+QENG="servingcell"

        public int query_cell_info ()
        {

            for (int i = 0; i < 3; i++)
            {
                string rsu = send_command_and_find_str(@"AT+QENG=""servingcell""", @"(OK)");
                if (rsu.IndexOf("OK") >= 0) return 1;
                if (rsu == "NULL") continue;
            }
            return -1;
        }

        public int record_sound_from_mic()
        {

            for (int i = 0; i < 5; i++)
            {
                string rsu = send_command_and_find_str(@"AT+QFLST=""*""", "(A.wav)");
                if (rsu == "NULL")
                {

                    rsu = send_command_and_find_str(@"AT+QAUDRD=1,""A.wav"",13,0", "(OK)");
                    if ( rsu == "NULL") continue;

                    System.Threading.Thread.Sleep(2000);

                    rsu = send_command_and_find_str(@"AT+QAUDRD=0", "(OK)");
                    if (rsu == "OK") return 1;
                }
                else {



                     delete_ufs_wav_file();
                     continue;

                };


                
            }
            return -1;
        }

        public int play_sound()
        {

            for (int i = 0; i < 3; i++)
            {
                if (send_command_and_find_str(@"AT+CLVL=5", "(OK)") == "NULL") continue;
                if (send_command_and_find_str(@"AT+QAUDPLAY=""A.wav""", "(OK)") == "NULL") continue;
       
                return 1;
            }
            return -1;
        }

        public int into_quit_FTM_mode(int into_quit)
        {

            for (int i = 0; i < 3; i++)
            {
                if (send_command_and_find_str($"AT+QRFTESTMODE={into_quit}", "(OK)") == "NULL") continue;

                return 1;
            }
            return -1;
        }


        public int FTM_Set_RF_ON_off(string band, string on_off)
        {

            for (int i = 0; i < 3; i++)
            {

                switch (band) {
                    case "band1":
                    {
                            if (send_command_and_find_str($@"AT+QRFTEST=""LTE BAND1"",18000,""{on_off}"",23,2,0", "(OK)") == "NULL") continue;
                     }
                     break;
                    case "band3":
                        {
                            if (send_command_and_find_str($@"AT+QRFTEST=""LTE BAND3"",19200,""{on_off}"",23,2,0", "(OK)") == "NULL") continue;
                        }
                        break;
                    case "band8":
                        {
                            if (send_command_and_find_str($@"AT+QRFTEST=""LTE BAND8"",21450,""{on_off}"",23,2,0", "(OK)") == "NULL") continue;
                        }
                        break;
                    case "band7":
                        {
                            if (send_command_and_find_str($@"AT+QRFTEST=""LTE BAND7"",20750,""{on_off}"",23,2,0", "(OK)") == "NULL") continue;
                        }
                        break;

                    case "band40":
                        {
                            if (send_command_and_find_str($@"AT+QRFTEST=""LTE BAND40"",38650,""{on_off}"",23,2,0", "(OK)") == "NULL") continue;
                        }
                        break;


                }
                

                return 1;
            }
            return -1;
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
        ~sqlr_ltemodule_serial()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
                setinit();
                var rever = ((SerialPort)sender);
                System.Threading.Thread.Sleep(350);
                string tempstr = rever.ReadExisting();
                if (tempstr.IndexOf("OK")>=0) loopwaitflog = 1;
                str_rev.Append(tempstr);



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

