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
    class chroma_dc_elecronic_63200_load : SerialPort
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
        public chroma_dc_elecronic_63200_load(string port, int baudrate) : base(port)
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
            base.WriteLine("*IDN?");
            System.Threading.Thread.Sleep(50);
            base.ReadExisting();
      
        }

        private void chroma_63200_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public int set_load_on_off(String onoff) {

            for (int i = 0; i < 3; i++)
            {
                this.ReadExisting();
                System.Threading.Thread.Sleep(50);
                this.WriteLine($"LOAD {onoff}");
                System.Threading.Thread.Sleep(100);
                if (onoff.ToUpper() == "ON") {
                    this.WriteLine("LOAD?");
                    System.Threading.Thread.Sleep(70);
                    if (this.ReadExisting().IndexOf($"1") >= 0) return 1;
                    continue;
                }
                if (onoff.ToUpper() == "OFF")
                {
                    this.WriteLine("LOAD?");
                    System.Threading.Thread.Sleep(70);
                    if (this.ReadExisting().IndexOf($"0") >= 0) return 1;
                    continue;
                }

            }
            return -2;





        }




        public int set_resitance_load_value(int L1, int L2) {

        for(int i = 0; i < 3; i++) { 
            System.Threading.Thread.Sleep(50);
            this.WriteLine($"RES:L1 {L1} OHM");
            this.WriteLine($"RES:L2 {L2} OHM");
            System.Threading.Thread.Sleep(50);
            string m =  this.ReadExisting();
            this.WriteLine($"RES:L1?");
            System.Threading.Thread.Sleep(100);
            if(this.ReadExisting().IndexOf($"{L1}")<0) continue;
            this.WriteLine($"RES:L2?");
            System.Threading.Thread.Sleep(100);
            if (this.ReadExisting().IndexOf($"{L2}") >= 0) return 1;

            }
            return -2;
        }


        public int set_cc_load_value(double L1, double L2)
        {

            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
                this.WriteLine($"CURR:STAT:L1 {L1}");
                this.WriteLine($"CURR:STAT:L2 {L2}");
                System.Threading.Thread.Sleep(50);
                this.WriteLine($"CURR:STAT:L1?");
                System.Threading.Thread.Sleep(100);
                if (this.ReadExisting().IndexOf($"{L1}") < 0) continue;
                this.WriteLine($"CURR:STAT:L2?");
                System.Threading.Thread.Sleep(100);
                if (this.ReadExisting().IndexOf($"{L2}") >= 0) return 1;

            }
            return -2;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0/1:CCL/H,2/3:CCDL/H,4/5:CRL/H,6/7:CVL/H,8/:CPL/H,10/11CCEL/H</param>
        /// <returns></returns>
        public int set_operational_mode(string mode) {


            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
                this.WriteLine($"MODE {mode} ");
                System.Threading.Thread.Sleep(50);
                this.WriteLine($"MODE?");
                System.Threading.Thread.Sleep(50);
                if (this.ReadExisting().IndexOf($"{mode}")>= 0) return 1;

            }
            return -2;
        


        }

        public void set_configure_voltage_on_for_curr(string voltage="370V") {


            System.Threading.Thread.Sleep(50);
            this.WriteLine($"CONF:VOLT:ON {voltage}");



        }


        public double get_load_voltage(int realtime=1)
        {
            double rs = -888;
            for (int i = 0; i < 3; i++) {
                this.ReadExisting();
            System.Threading.Thread.Sleep(50);
                if (realtime == 1)
                {
                    this.WriteLine($"MEAS:VOLTAGE?");
                }
                else {
                    this.WriteLine($"FETCh:VOLTAGE?");
                }
           
            System.Threading.Thread.Sleep(100);
           
            try
            {

                rs = double.Parse(this.ReadExisting().Trim());
                    break;
            }
            catch
            {

                    continue;

            }

            }
            return rs;
        }
            public double get_load_chrrent(int realtime = 1)
            {
            double rs = -888;
            for(int i = 0; i < 3; i++) { 
            System.Threading.Thread.Sleep(50);
                if (realtime == 1)
                {
                    this.WriteLine($"MEAS:CURRent?");

                }
                else {
                    this.WriteLine($"FETCh:CURRent?");
                }
                
                System.Threading.Thread.Sleep(100);
                
                try
                {

                    rs = double.Parse(this.ReadExisting().Trim());
                    break;
                }
                catch
                {
                    continue;


                }
            }

            return rs;
        }
        public double get_load_power(int realtime=1)
        {
            double rs = -888;
            for (int i = 0; i < 3; i++)
            {
                System.Threading.Thread.Sleep(50);
                if (realtime == 1)
                {
                    this.WriteLine($"MEAS:POWer?");
                }
                else {

                    this.WriteLine($"FETCh:POWer?");
                }
               
                System.Threading.Thread.Sleep(100);

                try
                {

                    rs = double.Parse(this.ReadExisting().Trim());
                    break;
                }
                catch
                {
                    continue;


                }
            }

            return rs;
        }



        ~chroma_dc_elecronic_63200_load()
        {
            this.Close();
        }

        public void callbackdebuginfo(string m)
        {
            m = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ": \r\n" + m;
            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }
    }
}

