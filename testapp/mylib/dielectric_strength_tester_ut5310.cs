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
    class dielectric_strength_tester_ut5310 : SerialPort
    {
        private string[] mestype = { "FREQuency", "MEAN", "PERIod", "PHAse", "PK2pk", "CRMs", "MINImum", "MAXImum", "RISe", "FALL", "PWIdth", "NWIdth", "NONE" };
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
        public dielectric_strength_tester_ut5310(string port, int baudrate) : base(port)
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
            set_predefine();
        }

        private void dielectric_strength_tester_ut5310_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }


        public int set_dc_volt(int stepnum, double voltage_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:VOLT {voltage_vol}V");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:VOLT?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - voltage_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_lowc(int stepnum, double lowc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:LOWC {lowc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:LOWC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - lowc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }
        public int set_dc_uppc(int stepnum, double uppc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:UPPC {uppc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:UPPC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - uppc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_arc(int stepnum, double arc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:ARC {arc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:ARC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - arc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_REAL(int stepnum, double arc_real_current_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:REAL {arc_real_current_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:REAL?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - arc_real_current_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_RTIM(int stepnum, double rtim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:RTIM {rtim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:RTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - rtim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_ftime(int stepnum, double ftim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:FTIM {ftim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:FTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ftim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_dc_ttime(int stepnum, double ttim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:TTIM {ttim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:FTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ttim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }
        public int set_dc_wtime(int stepnum, double wtim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:WTIM {wtim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:DC:WTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - wtim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ir_volt(int stepnum, double ir_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:VOLT {ir_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:VOLT?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ir_LOWR(int stepnum, double ir_low_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:LOWR {ir_low_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:LOWR?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_low_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ir_UPPR(int stepnum, double ir_uppr_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:UPPR {ir_uppr_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:UPPR?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_uppr_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }
        public int set_ir_rtim(int stepnum, double ir_rtime_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:RTIM {ir_rtime_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:RTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_rtime_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ir_ftim(int stepnum, double ir_ftime_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:FTIM {ir_ftime_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:FTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_ftime_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ir_ttim(int stepnum, double ir_ttime_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:TTIM {ir_ttime_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:IR:TTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ir_ttime_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_stop_start(int start_stop) { 

            try
            {
                if (start_stop == 1)
                {
                    this.WriteLine($"FUNC:START");
                }
                else {

                    this.WriteLine($"FUNC:STOP");
                };
              
             
                 return 1;
            }
            catch
            {
                return -1;
            }


        }


        public int set_ac_volt(int stepnum, double voltage_vol) {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:VOLT {voltage_vol}V");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:VOLT?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if(double.TryParse(rsu, out rs)==false) return -1;
                if (Math.Abs(rs - voltage_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch {
                return -3;
            }
        

        }

        public int set_ac_lowc(int stepnum, double lowc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:LOWC {lowc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:LOWC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - lowc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }
        public int set_ac_uppc(int stepnum, double uppc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:UPPC {uppc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:UPPC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - uppc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_arc(int stepnum, double arc_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:ARC {arc_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:ARC?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - arc_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_REAL(int stepnum, double arc_real_current_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:REAL {arc_real_current_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:REAL?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - arc_real_current_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_RTIM(int stepnum, double rtim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:RTIM {rtim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:RTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - rtim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_ftime(int stepnum, double ftim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:FTIM {ftim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:FTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ftim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_ttime(int stepnum, double ttim_vol)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:TTIM {ttim_vol}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:FTIM?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - ttim_vol) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }

        public int set_ac_freq(int stepnum, double freq)
        {

            try
            {

                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:FREQ {freq}");
                this.ReadExisting();
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:FREQ?");
                string rsu = this.ReadLine();
                double rs = -0xffff;
                if (double.TryParse(rsu, out rs) == false) return -1;
                if (Math.Abs(rs - freq) <= 0.00001) return 1;
                else return -2;
            }
            catch
            {
                return -3;
            }


        }
        public void set_predefine() {

            new Task(() =>
            {
                base.WriteLine("*IDN?");
                System.Threading.Thread.Sleep(50);
                base.ReadLine();
                this.WriteLine("FUNC:STOP");
                int stepnum = 1;
                double voltage_vol = 2500;
                double current_vol_low = 0.001;
                double current_vol_up = 0.001;
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:VOLT {voltage_vol}V");
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:LOWC {current_vol_low}A");
                this.WriteLine($"FUNC:SOUR:STEP {stepnum}:AC:UPPC {current_vol_up}A");

            }).Start();
           

   
        }



        ~dielectric_strength_tester_ut5310()
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

