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

  
    class plc_xx_com : SerialPort
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
        int WM_TEST_TRIGGER_RUN = USER + 121;
        #endregion
        public static  IntPtr ptrWnd;
        List<byte> rsult_buffer = new List<byte>();
        volatile int wait_timeout = -1;
        volatile int loopcount = 0;
         
        private void setinit() {
           // this.ReadExisting();
      
          if(rsult_buffer.Count>4096) rsult_buffer.Clear();
       
            
        }


        public plc_xx_com( string port, int baudrate=9600) : base(port)
        {
            

            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += comm_DataReceived;
            rsult_buffer.Capacity = 4096;
            base.ReceivedBytesThreshold = 1;
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();
        }


        void set_data_int() {

            rsult_buffer.Clear();
            loopcount = 0;
        }



        void find_and_trigger(byte[] rsu) {

            string tmp = "";

            foreach (var p in rsu) {

                tmp = tmp + $" {p:x2}";
            }


            callbackdebuginfo(tmp);

            if (rsu.Length > 5 && rsu[2] == 0x55) {


                SendMessage(ptrWnd, WM_TEST_TRIGGER_RUN, IntPtr.Zero, "1");

            }



        }


        int  delay_wait(int try_10ms) {

            for (int i = 0; i < try_10ms; i++) {
                System.Threading.Thread.Sleep(10);
                if (loopcount == 1) {

                    return 1;
                };
            }


            return -1;
        }


        public void read_X_int(byte pin_y) {

            rsult_buffer.Clear();
            byte[] command = my_tan_modbus(new byte[] { 0x01, 0x02, 0x50, 0x01, 0x00, 0x01 });
            this.Write(command,0,command.Length);

            if (delay_wait(20) == 1)
            {

                byte[] rsu = rsult_buffer.ToArray();
            }
           


        }

        public void write_Y_out(byte pin_y)
        {

            rsult_buffer.Clear();
            byte[] command = my_tan_modbus(new byte[] { 0X01 ,0X0F ,0X60 ,0X00 ,0X00 ,0X10 ,0X02 ,pin_y ,0X00 });
            this.Write(command, 0, command.Length);

            if (delay_wait(20) == 1)
            {

                byte[] rsu = rsult_buffer.ToArray();
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
        ~plc_xx_com()
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
                if (m <= 0) return;
                byte[] tm = new byte[m];
                rever.Read(tm, 0, m);
              
                foreach (byte z in tm) {

                 rsult_buffer.Add(z);

                }
                loopcount = 1;
                find_and_trigger(tm);
            }
            catch( Exception a)
            {
               

            }


        }

        public void callbackdebuginfo(string m)
        {

            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ffff") + ":\n" +  m);

        }


        private UInt16 my_crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        public Byte[] my_tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

    }

}

