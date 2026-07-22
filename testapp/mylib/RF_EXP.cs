using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using RFExplorerCommunicator;
namespace testapp
{
    class RFExplorer
    {

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        public const int USER = 0x0400;
        public const int WM_SEND_RF_REF = USER + 114;
        public static IntPtr ptrWnd;

        private RFECommunicator m_objRFE = null;
        private bool IsCon = false;
        private bool m_bNewConfigurationReceived;
        private string port = "";
        private int baudRate = 500000;
        public static Dictionary<double,double> rf_table_rsu;
        /*测试中间变量*/
 
        private System.Timers.Timer timer;
        private string m_sRFEReceivedString;

        public RFExplorer(String port, int baudRate = 500000)
        {

            this.port = port;
            this.baudRate = baudRate;
            timer = new System.Timers.Timer(100);
            timer.AutoReset = true;

            try
            {
               

                m_objRFE = new RFECommunicator(false);
                m_objRFE.PortClosedEvent += new EventHandler(OnRFE_PortClosed);
                m_objRFE.ReportInfoAddedEvent += new EventHandler(OnRFE_ReportLog);
                m_objRFE.ReceivedConfigurationDataEvent += new EventHandler(OnRFE_ReceivedConfigData);
                m_objRFE.UpdateDataEvent += new EventHandler(OnRFE_UpdateData);
                m_objRFE.ConnectPort(port, baudRate);
                if (m_objRFE.PortConnected)
                {
                    IsCon = true;
                    m_objRFE.SendCommand_RequestConfigData();
                    m_objRFE.SendCommand_SweepDataPoints(10000);

                }
                timer.Elapsed += new System.Timers.ElapsedEventHandler((object sender,System.Timers.ElapsedEventArgs e)=> {

                    if (m_objRFE.PortConnected)
                    {
                        m_objRFE.ProcessReceivedString(true, out m_sRFEReceivedString);
                    }

                });
            }
            catch {

                IsCon = false;
                System.Windows.Forms.MessageBox.Show("RFExplorer OPEN ERROR ");
            }


        }

    
        #region 没有用到的回调函数
        private void OnRFE_ReceivedConfigData(object sender, EventArgs e)
        {
            m_bNewConfigurationReceived = true;
            // ReportDebug(m_sRFEReceivedString);
            m_objRFE.SweepData.CleanAll(); //we do not want mixed data sweep values
            m_objRFE.TrackingData.CleanAll();

        }
        private void OnRFE_ReportLog(object sender, EventArgs e)
        {
            EventReportInfo objArg = (EventReportInfo)e;
            // ReportDebug(objArg.Data);
        }


        private void OnRFE_PortClosed(object sender, EventArgs e)
        {
            IsCon = false;
            // ReportDebug("RF Explorer PortClosed");
        }
        #endregion
        private void OnRFE_UpdateData(object sender, EventArgs e)
        {
          

            
        }

        public int set_chan_5g_2g(bool is2g = true) {

            try
            {
                if (is2g)
                {
                    m_objRFE.SendCommand_EnableMainboard();
                }
                else
                {

                    m_objRFE.SendCommand_EnableExpansion();

                }
            }
            catch {


                return -1;
            }
            return 1;
        }

      //public static  void abc() {
            
      //      string a = new Random().Next(-70, -50) + ",-52.5,-57,-55,-54,-52,-54.5,-55.5,-53,-50,-52.5,-55.5,-55.5,-55,-51.5,-52,-51.5,-53,-53,-53.5,-53,-52.5,-48.5,-36,-36.5,-37,-37.5,-37,-34,-35.5,-37,-35.5,-34.5,-35.5,-36.5,-37.5,-35.5,-35,-35.5,-35.5,-36.5,-34.5,-35,-37,-36.5,-34.5,-34,-37.5,-35.5,-35.5,-35,-35.5,-35,-36,-36,-37.5,-35,-36.5,-37,-34,-35,-35.5,-37,-36,-36,-34.5,-34,-36,-33,-32,-34,-35,-37,-35.5,-34.5,-33.5,-34.5,-35,-34.5,-35,-36,-36,-35,-33.5,-35,-35,-34.5,-36.5,-37,-47.5,-49,-48.5,-49,-48.5,-51,-51,-48.5,-50,-51.5,-50,-50.5,-52.5,-49,-53,-51.5,-51,-51.5,-49,-53,-53,-51,-52";
      //      string b = "2397,2397.27027,2397.54054,2397.81081,2398.08108,2398.35135,2398.62162,2398.89189,2399.16216,2399.43243,2399.7027,2399.97297,2400.24324,2400.51351,2400.78378,2401.05405,2401.32432,2401.59459,2401.86486,2402.13513,2402.4054,2402.67567,2402.94594,2403.21621,2403.48648,2403.75675,2404.02702,2404.29729,2404.56756,2404.83783,2405.1081,2405.37837,2405.64864,2405.91891,2406.18918,2406.45945,2406.72972,2406.99999,2407.27026,2407.54053,2407.8108,2408.08107,2408.35134,2408.62161,2408.89188,2409.16215,2409.43242,2409.70269,2409.97296,2410.24323,2410.5135,2410.78377,2411.05404,2411.32431,2411.59458,2411.86485,2412.13512,2412.40539,2412.67566,2412.94593,2413.2162,2413.48647,2413.75674,2414.02701,2414.29728,2414.56755,2414.83782,2415.10809,2415.37836,2415.64863,2415.9189,2416.18917,2416.45944,2416.72971,2416.99998,2417.27025,2417.54052,2417.81079,2418.08106,2418.35133,2418.6216,2418.89187,2419.16214,2419.43241,2419.70268,2419.97295,2420.24322,2420.51349,2420.78376,2421.05403,2421.3243,2421.59457,2421.86484,2422.13511,2422.40538,2422.67565,2422.94592,2423.21619,2423.48646,2423.75673,2424.027,2424.29727,2424.56754,2424.83781,2425.10808,2425.37835,2425.64862,2425.91889,2426.18916,2426.45943,2426.7297,2426.99997";
      //      rf_table_rsu = new Dictionary<double, double>();
      //      for (int i = 0; i < a.Split(",".ToArray()).Length; i++) {


      //          rf_table_rsu.Add(double.Parse(b.Split(",".ToArray())[i]), double.Parse(a.Split(",".ToArray())[i]));





      //      }
      //      SendMessage(ptrWnd, WM_SEND_RF_REF, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
      //  }
        public int GetHoldData(double center_freq /*Mhz*/,int delta,int delay,out double []dbms, int selectc =1, int totle_sw=1) {


           
            
            dbms = new double []{-200,-200,-200,-200,-200,-200,-200 };
         
            m_bNewConfigurationReceived = false;
          //  m_objRFE.UseMaxHold = true;
            m_objRFE.SweepData.CleanAll();
            m_objRFE.SendCommand_SetMaxHold();
            m_objRFE.SendCommand_RequestConfigData();
            m_objRFE.UpdateDeviceConfig((center_freq - delta<0)?0:(center_freq - delta), center_freq + delta, 0f, -120f);
            timer.Start();
            //wait for device to reconfigure
            while (!m_bNewConfigurationReceived)
            {
               System.Threading.Thread.Sleep(10); //Wait 10ms
            }
            int count = 0;
            System.Threading.Thread.Sleep(delay);
            while (m_objRFE.SweepData.Count < 1 && count++ < 10) { System.Threading.Thread.Sleep(20); if (count > 9) return -2; }
            timer.Stop();

            #region  //-rflogfile.备份//
            if (totle_sw == 1) {
                rf_table_rsu = new Dictionary<double, double>();
                switch (selectc) {
                    case 1:
                        if ((!File.Exists("CH1_FREQ_DBM.csv")))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH1_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                            }


                        }
                        else
                        {



                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH1_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {

                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                            }

                        }
                            break;

                    case 7:

                        if ((!File.Exists("CH7_FREQ_DBM.csv")))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH7_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                            }


                        }
                        else
                        {



                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH7_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {

                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                            }




                        }
                        break;

                    case 14:

                        if ((!File.Exists("CH14_FREQ_DBM.csv")))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH14_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                            }


                        }
                        else
                        {



                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH14_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {

                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                            }




                        }
                        break;
                    case 36:

                        if ((!File.Exists("CH36_FREQ_DBM.csv")))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH36_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                            }


                        }
                        else
                        {



                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH36_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {

                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                            }


                         }
                        break;
                case 60:
                if ((!File.Exists("CH60_FREQ_DBM.csv")))
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH60_FREQ_DBM.csv", true))
                    {
                        string buffreq = "";
                        string bufdmb = "";
                        for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                        {
                            bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                            buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                            rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));

                                }
                        file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                        file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                    }


                }
                else
                {



                    using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH60_FREQ_DBM.csv", true))
                    {
                        string buffreq = "";
                        string bufdmb = "";
                        for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                        {

                            bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                            rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                         }
                        file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                    }




                }
                        break;
                    case 165:
                        if ((!File.Exists("CH165_FREQ_DBM.csv")))
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH165_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));

                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + buffreq + "\n");
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");

                            }


                        }
                        else
                        {



                            using (System.IO.StreamWriter file = new System.IO.StreamWriter("CH165_FREQ_DBM.csv", true))
                            {
                                string buffreq = "";
                                string bufdmb = "";
                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {

                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }
                                file.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + bufdmb + "\n");


                            }




                        }
                        break;

                    default:
                        break;
            }

        }


            #endregion

           
            SendMessage(ptrWnd, WM_SEND_RF_REF, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));

            //if (totle_sw != 60)
            //{
                double left_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 8));
                double right_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 8));
                double left_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 11));
                double right_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 11));
                double center_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq));
                double left_min_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM(m_objRFE.SweepData.MaxHoldData.GetMinDataPoint());
                double right_max_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)(m_objRFE.SweepData.MaxHoldData.TotalSteps));
                dbms = new double[] { left_min_DBM, left_10M_DBM, left_8M_DBM, center_DBM, right_8M_DBM, right_10M_DBM, right_max_DBM };
            //}
            //else {

            //    double left_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 39));
            //    double right_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 39));
            //    double left_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 41));
            //    double right_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 41));
            //    double center_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq));
            //    double left_min_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM(m_objRFE.SweepData.MaxHoldData.GetMinDataPoint());
            //    double right_max_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)(m_objRFE.SweepData.MaxHoldData.TotalSteps));
            //    dbms = new double[] { left_min_DBM, left_10M_DBM, left_8M_DBM, center_DBM, right_8M_DBM, right_10M_DBM, right_max_DBM };


            //}
            // ushort pk = m_objRFE.SweepData.MaxHoldData.GetPeakStep();


            // double maxfreq = m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ(pk);
            // double dbm= m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM(pk);

            //int m=  getstep(590);

            //      maxfreq = m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)m);



            return 0;
        }



        public int GetHoldData_offset_dbms(double center_freq /*Mhz*/, int delta, int delay, out double[] offset_dbm, int selectc = 1, int totle_sw = 1)
        {




            offset_dbm = new double[] { -200, -200};

            m_bNewConfigurationReceived = false;
            //  m_objRFE.UseMaxHold = true;
            m_objRFE.SweepData.CleanAll();
            m_objRFE.SendCommand_SetMaxHold();
            m_objRFE.SendCommand_RequestConfigData();
            m_objRFE.UpdateDeviceConfig((center_freq - delta < 0) ? 0 : (center_freq - delta), center_freq + delta, 0f, -120f);
            timer.Start();
            //wait for device to reconfigure
            while (!m_bNewConfigurationReceived)
            {
                System.Threading.Thread.Sleep(10); //Wait 10ms
            }
            int count = 0;
            System.Threading.Thread.Sleep(delay);
            while (m_objRFE.SweepData.Count < 1 && count++ < 10) { System.Threading.Thread.Sleep(20); if (count > 9) return -2; }
            timer.Stop();
            rf_table_rsu = new Dictionary<double, double>();
         
            string buffreq = "";
            string bufdmb = "";

             for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
             {
                 bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                 buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                 rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
             }

            SendMessage(ptrWnd, WM_SEND_RF_REF, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));

            ushort pk = m_objRFE.SweepData.MaxHoldData.GetPeakDataPoint();
            double maxfreq = m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ(pk);
            double dbm = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM(pk);
            offset_dbm[0] = center_freq- maxfreq;
            offset_dbm[1] = dbm;



            return 0;
        }

        public int _GetHoldData(double center_freq /*Mhz*/, int delta, int delay, out double[] dbms, int selectc = 1, int totle_sw = 1)
        {




            dbms = new double[] { -200, -200, -200, -200, -200, -200, -200 };

            m_bNewConfigurationReceived = false;
            //  m_objRFE.UseMaxHold = true;
            m_objRFE.SweepData.CleanAll();
            m_objRFE.SendCommand_SetMaxHold();
            m_objRFE.SendCommand_RequestConfigData();
            m_objRFE.UpdateDeviceConfig((center_freq - delta < 0) ? 0 : (center_freq - delta), center_freq + delta, 0f, -120f);
            timer.Start();
            //wait for device to reconfigure
            while (!m_bNewConfigurationReceived)
            {
                System.Threading.Thread.Sleep(10); //Wait 10ms
            }
            int count = 0;
            System.Threading.Thread.Sleep(delay);
            while (m_objRFE.SweepData.Count < 1 && count++ < 10) { System.Threading.Thread.Sleep(20); if (count > 9) return -2; }
            timer.Stop();

            #region  //显示//
            if (totle_sw == 1)
            {
                rf_table_rsu = new Dictionary<double, double>();
               
                                string buffreq = "";
                                string bufdmb = "";

                                for (int i = 0; i < m_objRFE.SweepData.MaxHoldData.TotalDataPoints; i++)
                                {
                                    bufdmb = bufdmb + "," + m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i);
                                    buffreq = buffreq + "," + m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i);
                                    rf_table_rsu.Add(m_objRFE.SweepData.MaxHoldData.GetFrequencyMHZ((ushort)i), m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)i));
                                }

            


            }


            #endregion


            SendMessage(ptrWnd, WM_SEND_RF_REF, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));

            //if (totle_sw != 60)
            //{
            double left_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 8));
            double right_8M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 8));
            double left_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq - 11));
            double right_10M_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq + 11));
            double center_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)getstep(center_freq));
            double left_min_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM(m_objRFE.SweepData.MaxHoldData.GetMinDataPoint());
            double right_max_DBM = m_objRFE.SweepData.MaxHoldData.GetAmplitudeDBM((ushort)(m_objRFE.SweepData.MaxHoldData.TotalSteps));
            dbms = new double[] { left_min_DBM, left_10M_DBM, left_8M_DBM, center_DBM, right_8M_DBM, right_10M_DBM, right_max_DBM };
         

            return 0;
        }


        private int getstep( double findfreq) {
            int count = 0;
         while (m_objRFE.SweepData.Count < 1 && count++ < 10) { System.Threading.Thread.Sleep(20); if (count > 9) return -2; }
        
            int totle = m_objRFE.SweepData.MaxHoldData.TotalSteps;
            double startfreq = m_objRFE.StartFrequencyMHZ;
            double endfreq = m_objRFE.CalculateEndFrequencyMHZ();

            int rt = (int)((findfreq - startfreq) / ((endfreq - startfreq) / (totle))) + 1;
            return rt;

        }


















        ~RFExplorer()
        {
            if (m_objRFE != null && IsCon) {
             
                m_objRFE.ClosePort();
                m_objRFE.Close();

            }

         

        }
        public void DisposePort() {

            if (m_objRFE != null)
            {
                try
                {
                    m_objRFE.Dispose();

                }
                catch {


                }
               
            }

            }




    }
}
