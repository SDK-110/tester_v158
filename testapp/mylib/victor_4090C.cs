using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices;

namespace DeviceLibrary
{


    class victor_4090C : SerialPort
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
        public static IntPtr ptrWnd;

        public victor_4090C(string port, int baudrate) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.ReadTimeout = 2000;
            base.RtsEnable = true;
            base.DtrEnable = true;

            // base.DataReceived += Relay_aputus_DataReceived;

            base.Open();

            new Task(() =>
            {

                base.WriteLine("*IDN?");
                string m = base.ReadLine();
                set_frequcency("100000");
                set_bias_level("1000");
              
             


            }).Start();
            
        }

        private void victor_4090C_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }
        /// <summary>
        /// 设置偏置电压
        /// </summary>
        /// <param name="level">电压毫伏</param>
        public int  set_bias_level( string level)
        {
            this.ReadExisting();
            this.WriteLine("BIAS:VOLT:LEV: "  + level);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }



        /// <summary>
        /// 设置测试频率
        /// </summary>
        /// <param name="freq">hz:100,120,200,400,800,1K,2K,4K,8K,10K,15K,20K,40K,50K,80K,100K</param>
        public int set_frequcency(string freq)
        {
            this.ReadExisting();
            this.WriteLine("FREQ:CW " + freq);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        /// <summary>
        /// 设置偏置电压
        /// </summary>
        /// <param name="level">电压毫伏100,300,600,1000,1500,2000</param>
        public int set_voltage_level(string level)
        {
            this.ReadExisting();
            this.WriteLine("VOLT " + level);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        public int set_dev_mode(int onoff) {

            this.ReadExisting();
            this.WriteLine("FUNC:DEV:mode " + (onoff==0?"0":"1"));
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        /// <summary>
        /// 串并联
        /// </summary>
        /// <param name="serial_pallel">0串联，1并联</param>
        /// <returns></returns>
        public int set_euuivalent_mode(int serial_pallel=0)
        {

            this.ReadExisting();
            this.WriteLine("FUNC:DEV:EQU  " + (serial_pallel == 0 ? "SERial" : "PALlel"));
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        /// <summary>
        /// 模式设置
        /// </summary>
        /// <param name="mode">auto,R,C,L,Z,DCR,ECAP</param>
        /// <returns></returns>
        public int set_select_main_measure_mode(string mode)
        {
            this.ReadExisting();
            this.WriteLine("FUNC:IMP:A " + mode);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }


        public int set_select_range_auto(string mode/*1,0*/)
        {
            this.ReadExisting();
            this.WriteLine("FUNC:IMP:RANG:AUTO  " + mode);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }


        /// <summary>
        /// 模式设置
        /// </summary>
        /// <param name="mode">auto,R,C,L,Z,DCR,ECAP</param>
        /// <returns></returns>
        public int set_select_sub_measure_mode(string mode)
        {
            this.ReadExisting();
            this.WriteLine("FUNC:IMP:B " + mode);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        /// <summary>
        /// 量程
        /// </summary>
        /// <param name="range">30,100,1000,3000,10000,30000,100000</param>
        /// <returns></returns>
        public int set_select_measure_range(string range)
        {
            this.ReadExisting();
            this.WriteLine("FUNC:IMP:RANG  " + range);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }


        /// <summary>
        /// 获得数值
        /// </summary>
        /// <returns></returns>
        public double get_value() {


            this.ReadExisting();
            this.WriteLine("FETCH?");
            string rsu = this.ReadLine();
            string[] rsu_spilt = rsu.Split(",".ToCharArray());

            if (rsu_spilt.Length == 2) {

                return double.Parse(rsu_spilt[0]);
            
            }

            return -1;

        }


        /// <summary>
        /// 设置测试速度
        /// </summary>
        /// <param name="testspeed">FAST,MEDIUUM,SLOW</param>
        /// <returns></returns>
        public int set_aperture_mode(string testspeed="SLOW")
        {
            this.ReadExisting();
            this.WriteLine("APERture  " + testspeed);
            if (this.ReadLine().IndexOf("success") >= 0) return 1;
            return -1;
        }

        public void callbackdebuginfo(string m)
        {

            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }



        ~victor_4090C()
        {
            this.Close();
        }
    }
}

