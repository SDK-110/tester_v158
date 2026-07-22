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

    delegate void do_something();
    
    class BIOFI_PROD : SerialPort
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
        public do_something dosometing =null;
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

                    //this.Write(new byte[] { 0x03 }, 0, 1);
                    //this.Write(new byte[] { 0x1A }, 0, 1); /* 发送终止符号结束进程“Ctrl+C、Ctrl+Z对应的0x03和0x1A*/
                    wait_timeout = 1;

                }
            } while (loopwaitflog == 0 && ct-- >= 0);
        
        }
        public BIOFI_PROD( string port, int baudrate=115200) : base(port)
        {
            

            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
           // base.DataReceived += comm_DataReceived;
            str_rev.Capacity = 4056;
            base.NewLine = "\r";
            base.WriteTimeout = 2000;
            base.ReadTimeout = 30000;
            base.Open();
            base.ReceivedBytesThreshold = 1;
            

        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
          //  SerialPort sp = (SerialPort)sender;
          //  recebuf = sp.ReadExisting();
        }


        public byte get_checksum(byte [] datas) {


            byte rsu = 0xa4;

            foreach(byte t in datas) {

                rsu =  (byte)(rsu + t);
            }

            return rsu;
        }


        public byte[] hexstring2byte(string tr) {
            if (tr.Length % 2 != 0) return new byte[] { 0 };

            byte[] rsu = new byte[tr.Length / 2 ];
          
           
         
            int count = 0;
            for (int i = 0; i < tr.Length; i=i+2) {

                rsu[count] = byte.Parse(tr.Substring(i,2),System.Globalization.NumberStyles.AllowHexSpecifier);


                count++;
            }


            return rsu;
        
        
        
        
        }

        public string bytes2str(byte[] data) {

            string temp = "";

            foreach (var m in data) {

                temp = temp + $"{m:x2}";
            }


            return temp;


        }

        public string  command_tranf(byte command, byte[] payload,int lsb=0) {

          if( lsb==1) payload = payload.Reverse().ToArray();
            byte payload_size = (byte)payload.Length;
            byte checksum = get_checksum(payload);
            byte[] senddata = new byte[payload_size + 3];
            senddata[0] = command;
            senddata[payload_size+2] = checksum;
            senddata[1] = payload_size;
            for (int i = 0; i < payload_size; i++) {

                senddata[i + 2] = payload[i];

            }

            return bytes2str(senddata);



        }


      public  int send_command(byte command, byte[] payload,out string rsustr, int lsb = 0) {


            if (lsb == 1) payload = payload.Reverse().ToArray();
            byte payload_size = (byte)payload.Length;
            byte checksum = (byte)(get_checksum(payload) + command + payload_size);
            byte[] senddata = new byte[payload_size + 3];
            senddata[0] = command;
            senddata[payload_size + 2] = checksum;
            senddata[1] = payload_size;
            for (int i = 0; i < payload_size; i++)
            {

                senddata[i + 2] = payload[i];

            }
            this.ReadExisting();
            string sedstr = bytes2str(senddata).ToUpper();
          //  savelog(sedstr, 1);
            mylib.utility_func.callbackdebuginfo("send=> " + sedstr);
            this.WriteLine(sedstr);
            string rsultstr = "";
            try
            {
                if (dosometing != null) {

                    dosometing();
                }
                rsultstr = this.ReadLine();
                mylib.utility_func.callbackdebuginfo("rev=> " + rsultstr);
                //savelog(rsultstr, 2);
                if (rsultstr.IndexOf("N") >= 0)
                {
                     rsustr = "";
                    return -1;
                }
                rsustr = rsultstr;
                return 1;
            }
            catch (Exception e){
                rsustr = "";
              //  System.Windows.Forms.MessageBox.Show(e.ToString());
                return -2;
            }
        
        }











        public bool send_command_and_find_str_1(string command, string findstr,int delay=10) {

            setinit();
           
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
        ~BIOFI_PROD()
        {
            this.Close();
        }


        private void comm_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                var rever = ((SerialPort)sender);
                if (rever.BytesToRead <= 0)
                {
                    return;
                }

                str_rev.Append(rever.ReadLine());
                //setinit();
                //var rever = ((SerialPort)sender);

                //System.Threading.Thread.Sleep(200);
                //int m = rever.BytesToRead;
                //string tempstr = rever.ReadExisting();
                //callbackdebuginfo(tempstr + "\n");
                //str_rev.Append(tempstr);
                //if (tempstr.IndexOf("\r") >= 0) { loopwaitflog = 1; }



            }
            catch ( Exception a)
            {
                str_rev.Clear();

            }


        }

        public void callbackdebuginfo(string m)
        {

            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }

        public void savelog(string p, int send_or_rev) {
            string path = DateTime.Now.ToString("yyyy-MM-dd") + "BIOFI_PROD_COMMAND_RECORD.LOG";
            switch (send_or_rev)
            {
                case 1:
                    {
                        File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + ":\n send :" + p + "\n");

                    }
                    break;


                case 2:

                    {

                        File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + ":\n rev :" + p + "\n");
                    }
                    break;

                case 3:

                    {

                        File.AppendAllText(path, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "====" + p + "=====\n");
                    }
                    break;

            }


            }

    }

}

