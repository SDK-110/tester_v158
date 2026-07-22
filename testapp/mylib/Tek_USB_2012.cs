using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Runtime.InteropServices;

namespace testapp
{
   
 public    class Tek_USB_2012
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

        private PortOperatorBase TEK_2012DEV;
        public Tek_USB_2012(string devicename) {


            //{
            
            TEK_2012DEV = PortUltility.usbport_op(devicename);

         
               
       try
            {
                 
                    //   DM3058OPerator.WriteLine("*RST");
                    //     System.Threading.Thread.Sleep(2000);
            
                TEK_2012DEV.WriteLine("*IDN?");
                // string v = DM3058OPerator.ReadLine();
                System.Threading.Thread.Sleep(30);
                string v = TEK_2012DEV.Read();
                 
             
            }
            catch (Exception)
            {
                 System.Windows.Forms.MessageBox.Show("The Rigol multimeter is not connected properly or the resource port is not set properly,please restart app again");
            }


        }
        



        public Tek_USB_2012(String devicename, int baudrate) {

            
                //{
                TEK_2012DEV = PortUltility.serial_op(devicename, baudrate);

            try
            {

                TEK_2012DEV.WriteLine("CMDSET RIGOL");
                TEK_2012DEV.WriteLine("*IDN?");
                string v = TEK_2012DEV.ReadLine();

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("DM3058万用表没有连接好或资源端口设置不对");
            }





        }

        public void reset() {

            TEK_2012DEV.WriteLine("*RST");
            TEK_2012DEV.WriteLine("*cls");
        }
        public void Close()
        {
            
            TEK_2012DEV.Close();
        }

        public bool isOpen() {

            return TEK_2012DEV.IsPortOpen;
        }


        public void set_ch_coupling(string ch, string value)
        {
              System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:COUPLing " + value);
              System.Threading.Thread.Sleep(50);
            //if (TEK_2012DEV.ReadExisting().ToUpper().IndexOf(value) >= 0) return true;
        }
        /// <summary>
        /// 设置电流探针
        /// </summary>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value"> 0.2 , 1 , 2 , 5 , 10 , 50 , 100 ,1000 </param>
        public void set_ch_currentprobe(string ch, string value)
        {
            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:CURRENTPROBE  " + value);
            System.Threading.Thread.Sleep(50);
        }
        /// <summary>
        /// 水平延时位置
        /// </summary>
        /// <param name="ch">延时时间科学计数法秒</param>
        public void set_delay_position(string value)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"HORizontal:DELay:POSition  " + value);
            System.Threading.Thread.Sleep(50);


        }


        /// <summary>
        /// 水平ZHU缩放比例尺
        /// </summary>
        /// <param name="ch">缩放时间科学计数法秒</param>
        public void set_main_scale(string value)
        {

            //   System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"HORizontal:MAIn:SCAle " + value);
            //  System.Threading.Thread.Sleep(50);


        }



        /// <summary>
        /// 水平主位置
        /// </summary>
        /// <param name="ch"> main trigger position</param>
        public void set_main_position(string value)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"HORizontal:MAIn:POSition  " + value);
            System.Threading.Thread.Sleep(50);


        }


        /// <summary>
        /// 水平延时缩放比例尺
        /// </summary>
        /// <param name="ch">缩放时间科学计数法秒</param>
        public void set_delay_scale(string value)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"HORizontal:DELay:SCAle " + value);
            System.Threading.Thread.Sleep(50);


        }



        /// <summary>
        /// 表笔量程
        /// </summary>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value"> 1,10,20,50,100,500,1000</param>
        public void set_ch_probe(string ch, string value)
        {

            //   System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:PRObe  " + value);
            //    System.Threading.Thread.Sleep(50);


        }

        public void set_ch_position(string ch, string value)
        {

            //    System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:POSition  " + value);
            //    System.Threading.Thread.Sleep(50);


        }

        /// <summary>
        /// 量程设定/DIV方式
        /// </summary>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value">科学技术法</param>
        public void set_ch_scale(string ch, string value)
        {

            //  System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:SCAle  " + value);
            //  System.Threading.Thread.Sleep(50);


        }

        /// <summary>
        /// 量程设定/DIV方式
        /// </summary>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value">科学技术法</param>
        public void set_ch_volts(string ch, string value)
        {

            //  System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:VOLts  " + value);
            //   System.Threading.Thread.Sleep(50);


        }

        /// <summary>
        /// 通道单位(电流/电压)
        /// </summary>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value">V,A</param>
        public void set_ch_Y_unit(string ch, string value)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"CH{ch}:YUNIT  \"{ value}\"");
            System.Threading.Thread.Sleep(50);


        }


        /// <summary>
        /// 设定快速测试源与通道关系
        /// </summary>
        /// <param name="source">1,2</param>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value"></param>
        public void set_immed_source(string source, string ch)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement: IMMed: SOUrce{source} CH{ch}");
            System.Threading.Thread.Sleep(50);


        }

        /// <summary>
        /// 设定快速测试源类型
        /// </summary>
        /// <param name="source">1,2</param>
        /// <param name="ch">1,2,3,4</param>
        /// <param name="value">TYPe 1.FREQuency,2.MEAN, 3.PERIod,4.PHAse,5.PK2pk,6.CRMs,7.MINImum,8.MAXImum,9.RISe,10.FALL,11.PWIdth,12.NWIdth,13.NONE </param>
        public void set_immed_type(int type)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement:IMMed:TYPe {mestype[type - 1]}");
            System.Threading.Thread.Sleep(50);


        }

        /// <summary>
        /// 设定快速测试源类型
        /// </summary>
        /// <param name="source">1,2</param>
        /// <param name="tongdao">1,2,3,4</param>
        /// <param name="type">TYPe 1.FREQuency,2.MEAN, 3.PERIod,4.PHAse,5.PK2pk,6.CRMs,7.MINImum,8.MAXImum,9.RISe,10.FALL,11.PWIdth,12.NWIdth,13.NONE </param>
        public void set_source_measuremnet_type(int tongdao, int type)
        {

         //   System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement:MEAS{tongdao}:TYPe {mestype[type - 1]}");
           // System.Threading.Thread.Sleep(50);


        }
        /// <summary>
        /// 绑定源与通道
        /// </summary>
        /// <param name="tongdao">面板上的标识</param>
        /// <param name="ch">示波器通道int</param>
        public void set_measurement_source_to_channel(int tongdao, int ch)
        {
           // System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement:MEAS{tongdao}:SOUrce CH{ch}");
          //  System.Threading.Thread.Sleep(50);

        }





        /// <summary>
        /// 获取快速结果
        /// </summary>
        /// <param name="rsult">结果</param>
        public bool get_immed_value(ref double rsult)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement:IMMed:VALue?");
            System.Threading.Thread.Sleep(100);
            string rsu = TEK_2012DEV.ReadLine();
            if (rsu.IndexOf("E") < 0) return false;
            rsult = double.Parse(rsu.Trim());
            return true;

        }



        /// <summary>
        /// 获取测量结果测试源类型
        /// </summary>
        /// <param name="rsult">结果</param>
        public bool get_measure_value(int source, ref double rsult)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MEASUrement:MEAS{source}:VALue?");
            System.Threading.Thread.Sleep(100);
            string rsu = TEK_2012DEV.ReadLine();
            if (rsu.IndexOf("E") < 0) return false;
            rsult = double.Parse(rsu.Trim());
            return true;

        }

        public void trigger_force()
        {


            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger FORCe");
            System.Threading.Thread.Sleep(50);

        }


        /// <summary>
        /// 触发耦合设定
        /// </summary>
        /// <param name="couple"> AC, DC , HFRej , LFRej</param>
        public void set_trigger_edge_coupling(string couple)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"MAIn:EDGE:COUPling {couple}");
            System.Threading.Thread.Sleep(50);

        }

        /// <summary>
        /// 上升或下降沿触发方式
        /// </summary>
        /// <param name="slope"> FALL, RISe</param>
        public void set_trigger_edge_slope(string slope)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger:MAIn:EDGE:SLOpe {slope}");
            System.Threading.Thread.Sleep(50);

        }



        public void set_acquire_status(int single_seq/*RUNSTOP,SEQuence*/)
        {
            TEK_2012DEV.WriteLine($"acquire:stopafter " + (single_seq == 0 ? "RUNSTOP" : "SEQuence"));
            System.Threading.Thread.Sleep(50);
            //TEK_2012DEV.WriteLine($"acquire:state {run_stop} ");
            //System.Threading.Thread.Sleep(50);



        }


        public void set_runstop_status(int run_stop)
        {
            //TEK_2012DEV.WriteLine($"acquire:stopafter {single_seq}");
            //System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"acquire:state " + (run_stop == 0 ? "run" : "Stop"));
            System.Threading.Thread.Sleep(50);



        }


        public void set_trigger_source_channel(int channel)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger:MAIn:EDGE:SOUrce  CH{channel}");
            System.Threading.Thread.Sleep(50);

        }

        public void set_channel_on_off(int channel, string onoff)
        {

             System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"select:ch{channel} {onoff}");
              System.Threading.Thread.Sleep(50);

        }

        public void set_trigger_level(string level)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger:MAIn:LEVel {level}");
            System.Threading.Thread.Sleep(50);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">AUTO,NORMal</param>
        public void set_trigger_mode(string mode)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger:MAIn:MODe {mode}");
            System.Threading.Thread.Sleep(50);

        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">EDGE,VIDeo,PULSE</param>
        public void set_trigger_type(string type)
        {

            System.Threading.Thread.Sleep(50);
            TEK_2012DEV.WriteLine($"TRIGger:MAIn:TYPe {type}");
            System.Threading.Thread.Sleep(50);

        }


        public void get_curve_data(int channel)
        {

            TEK_2012DEV.WriteLine($"data:source: CH{channel}");
            TEK_2012DEV.WriteLine($"data:ENCdg ASCIi");
            TEK_2012DEV.WriteLine($"WFMPre:BYT_Nr 2");
            TEK_2012DEV.WriteLine("DATa:STARt 1");
            TEK_2012DEV.WriteLine("DATa:stop 2500");
            TEK_2012DEV.WriteLine("CURVE?");
            string rs = TEK_2012DEV.ReadLine();




        }

        public void set_predefine()
        {


            TEK_2012DEV.WriteLine("*cls");


            set_channel_on_off(1, "on");
            set_channel_on_off(2, "on");
            set_ch_coupling("1", "DC");
            set_ch_coupling("2", "DC");
            set_ch_probe("1", "100");
            set_ch_probe("2", "100");
            set_ch_Y_unit("2", "A");
            set_ch_scale("1", "1E2");
            set_ch_scale("2", "1E2");
            set_main_scale("1E-1");
            //set_runstop_status(0);
            //set_acquire_status(0);
            set_acquire_status(0);
            set_measurement_source_to_channel(1, 1);
            set_source_measuremnet_type(1, 5);


        }


        ~Tek_USB_2012() {

            if(this.isOpen())
            TEK_2012DEV.Close();
        }

        public void callbackdebuginfo(string m)
        {
            m = DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss") + ": \r\n" + m;
            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }

    }
}
