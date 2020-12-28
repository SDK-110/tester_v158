using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;
using IniParser;
using IniParser.Model;
using Microsoft.VisualBasic;
using testapp.mylib;

namespace testapp
{
    public delegate string pointfun(string a, string b, out string c,string d="");
    public delegate void debuginfosend(string trm);
    delegate void callbackfuc();
    public class testcase_dll
    {
        #region /*sendmessage dll 庫加載*/
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);
        string c = "";
        /*跨线程消息*/
        static  int USER = 0x0400;
         int WM_SENDA = USER + 101;
         int WM_SENDB = USER + 102;
        int WM_SENDC = USER + 103;
        int WM_SENDD = USER + 104;
        int WM_SEND_SET_CC1310LOSS = USER + 110;
        int WM_SEND_SET_BTLOSS = USER + 111;
        int WM_SEND_SET_WIFILOSS = USER + 112;
        int WM_SEND_AUTOTEST = USER + 113;
        #endregion 
        /*跨线程消息*/
        public string trf;
        string temp = ""; //mac 地址输入缓存
        string macflog = "";
        bool calibrationflog1 = false;
        bool calibrationflog2 = false;
        bool calibrationflog3 = false;
        public IntPtr ptrWnd;
        /*hackrf 宕机break 最大等待时间*/
        const int MAXWAITtime = 10000;
        volatile int  killflog=0;
        volatile int  diecount = 0;
        //definde sendmessage hwnd;
        // SendMessage(ptrWnd, WM_SENDTAG, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        private IniParser.Model.IniData dt;
        string cmw100addr;
        DM3058 dm3058=null;
        GPD3033 gpd3033 = null;
        // serial_no_visa relay;
        TDM9001_2A mincurm = null;
        TMD1501_50 minvm = null;
        sevy_relay ry = null;
        sevy_relay ry2 = null;
        led_assy ledassyer = null;
        TRM1201 TRM1201reader = null;
        chroma19701 chrm = null;
        comline commandline = null;
        led_assy_self led_sensor = null;
        piprun pr = null;
        vc8145cmeter vc8145 = null;
        myrelay relayself = null;
        HMS3000_Instruments hms = null;
        comline btdongle = null;
        smartFreqSpec smartFS = null;
        comlineforingo_led ingoproj_led = null;
        comline shieldbox = null;
        comlineNRTS micread_noise = null;

        /*变量声明*/

        volatile string btdogmac = "";


        /*变量声明*/
        public chroma19701 Chroma19701t
        {

            get { return chrm; }

        }
        public testcase_dll()
        {
            dt = iniread.ReadFile("setup.ini");
            cmw100addr = iniread.ReadFile("setup.ini")["setport"]["cmw100address"];
            #region  //注册case 函数
            m.Add("shieldboxopen", shieldboxopen);
            m.Add("TDM9001_2A_read", TDM9001_2A_read);
            m.Add("TMD1501_50_read", TMD1501_50_read);
            m.Add("TRM1201ReadRes", TRM1201ReadRes);
            m.Add("TRM1201_read", TRM1201_read);
            m.Add("bt_dongle_delay_regread", bt_dongle_delay_regread);
            m.Add("btdongle_save_MAC", btdongle_save_MAC);
            m.Add("smartfreqspc_read", smartfreqspc_read);
            m.Add("myrelay_set", myrelay_set);
            m.Add("relay_set", relay_set);
            m.Add("relay2_set", relay2_set);
            m.Add("cloor_assy", cloor_assy);
            m.Add("cloor_assy_Min", cloor_assy_Min);
            m.Add("PipRunning", PipRunning);
            m.Add("PipRunning_regular", PipRunning_regular);
            m.Add("delay", delay);
            m.Add("hipottest_dc", hipottest_dc);
            m.Add("message_prompt", message_prompt); //提示框
            m.Add("commline_pass_fail", commline_pass_fail);
            m.Add("commline_readval", commline_readval);
            m.Add("commline_send_noreturn", commline_send_noreturn);
            m.Add("commline_write_NonEnter", commline_write_NonEnter);
            m.Add("commline_closeport", commline_closeport);
            m.Add("commline_openport", commline_openport);
            m.Add("commline_send_signeCR", commline_send_signeCR);
            m.Add("commline_read_delay_reg", commline_read_delay_reg);
            m.Add("noiseware_readmic", noiseware_readmic);
            m.Add("noiseware_writecommand", noiseware_writecommand);
            m.Add("noiseaware_singlecommand", noiseaware_singlecommand);
            m.Add("noiseware_getkeypressstatus", noiseware_getkeypressstatus);
            m.Add("noiseware_getNonpressstatus", noiseware_getNonpressstatus);
            m.Add("noiseware_readAccelerometer", noiseware_readAccelerometer);
            m.Add("noiseware_getAccelerometerstatus", noiseware_getAccelerometerstatus);
            m.Add("mic_read_noise_bydanpianji", mic_read_noise);

            m.Add("noiseware_getAccelerometerstatus_dd", noiseware_getAccelerometerstatus_dd);
            m.Add("noiseware_intouartAndgetMAC", noiseware_intouartAndgetMAC);
            m.Add("noiseware_save_sn_MAC", noiseware_save_sn_MAC);
            m.Add("noise_bluethooth_readpower", noise_bluethooth_readpower);
            m.Add("noise_wifi_readpower", noise_wifi_readpower);
            m.Add("noise_cc1310_readpower", noise_cc1310_readpower);
            m.Add("pipgetmacclosecomport",pipgetmacclosecomport);
            m.Add("noise_PipRunning_fwupdate", noise_PipRunning_fwupdate);
            m.Add("noiseware_esp32tool_wifisign_gen", noiseware_esp32tool_wifisign_gen);
            m.Add("LED_Read", LED_Read);
            m.Add("LED_Read_PACK", LED_Read_PACK);
            m.Add("read_wifi_rssi", read_wifi_rssi);
            m.Add("read_info_window", read_info_window);
            m.Add("md3058_read_capactance", md3058_read_capactance);
            m.Add("md3058_read_DC_20V", md3058_read_DC_20V);
            m.Add("md3058_read_DC_200V", md3058_read_DC_200V);
            m.Add("md3058_read_DC_2mA", md3058_read_DC_2mA);
            m.Add("md3058_read_DC_20mA", md3058_read_DC_20mA);
            m.Add("md3058_read_DC_200mA", md3058_read_DC_200mA);
            m.Add("md3058_read_DC_10A", md3058_read_DC_10A);
            m.Add("md3058_read_resistance", md3058_read_resistance);
            m.Add("md3058_read_resistance_range", md3058_read_resistance_range);
            m.Add("pyrunner", pyrunner);
            m.Add("pipgetmac", pipgetmac);
            m.Add("vc8145cmeter_read_dcv", vc8145cmeter_read_dcv);
            m.Add("vc8145cmeter_read_dci", vc8145cmeter_read_dci);
            m.Add("vc8145cmeter_read_acv", vc8145cmeter_read_acv);
            m.Add("vc8145cmeter_read_aci", vc8145cmeter_read_aci);
            m.Add("vc8145cmeter_read_cap", vc8145cmeter_read_cap);
            m.Add("vc8145cmeter_read_freq", vc8145cmeter_read_freq);
            m.Add("vc8145cmeter_read_diode", vc8145cmeter_read_diode);
            m.Add("vc8145cmeter_read_ohm", vc8145cmeter_read_ohm);
            m.Add("gpd3303_setvoltage", gpd3303_setvoltage);
            m.Add("gpd3303_setcurrt", gpd3303_setcurrt);
            m.Add("gpd3303_readvoltage", gpd3303_readvoltage);
            m.Add("gpd3303_readcurrt", gpd3303_readcurrt);
            m.Add("gpd3303_off", gpd3303_off);
            m.Add("gpd3303_on", gpd3303_on);
            m.Add("rfid_reader_Manufacturer_ID_rwtest", rfid_reader_Manufacturer_ID_rwtest);
            m.Add("rfid_reader_Production_date_rwtest", rfid_reader_Production_date_rwtest);
            m.Add("rfid_reader_pcba_software_rwtest", rfid_reader_pcba_software_rwtest);
            m.Add("rfid_reader_pcba_software_DrawingIndicex_rtest", rfid_reader_pcba_software_DrawingIndicex_rtest);
            m.Add("rfid_reader_pcba_electronic_rwtest", rfid_reader_pcba_electronic_rwtest);
            m.Add("rfid_reader_pcba_barePCB_rwtest", rfid_reader_pcba_barePCB_rwtest);
            m.Add("rfid_reader_pcba_assembledPCB_rwtest", rfid_reader_pcba_assembledPCB_rwtest);
            m.Add("rfid_reader_pcba_schematic_rwtest", rfid_reader_pcba_schematic_rwtest);
            m.Add("rfid_reader_pcba_DrawingIndices_rwtest", rfid_reader_pcba_DrawingIndices_rwtest);
            m.Add("testsysini", testsysini);
            m.Add("releaseport", releaseport);
            m.Add("cmw100CalibationCheck", cmw100CalibationCheck);
            m.Add("cmw100CalibationBlueTooth_Txloss", cmw100CalibationBlueTooth_Txloss);
            m.Add("cmw100Calibation_CC1310_Txloss", cmw100Calibation_CC1310_Txloss);
            m.Add("cmw100CalibationWIFI_Txloss",cmw100CalibationWIFI_Txloss);
            m.Add("cmw100_GPS_BER", cmw100_GPS_BER);
            m.Add("cmw100CalibationBlueTooth_TxTest", cmw100CalibationBlueTooth_TxTest);
            m.Add("cmw100_sepctrumsnip", cmw100_sepctrumsnip);
            m.Add("cmw100_bluetooth_readpower", cmw100_bluetooth_readpower);
            m.Add("cmw100calibation_save", cmw100calibation_save);
            m.Add("hackrf_read_cc1310", hackrf_read_cc1310);
            m.Add("hackrf_read_cc1310_900_1", hackrf_read_cc1310_900_1);
            m.Add("hackrf_read_cc1310_900_2", hackrf_read_cc1310_900_2);
            m.Add("hackrf_read_cc1310_900_3", hackrf_read_cc1310_900_3);
            m.Add("hackrf_read_bt", hackrf_read_bt);
            m.Add("hackrf_read_wifi", hackrf_read_wifi);

            /*ingo专用*/
            m.Add("ingoread_led_bright_value",ingoread_led_bright_value);
            m.Add("ingofwverget_fromcurrent_cts_port", ingofwverget_fromcurrent_cts_port);
            m.Add("ingofwverget_fromledjudge_cdc_port", ingofwverget_fromledjudge_cdc_port);
            m.Add("ingofwverget_clearcount", ingofwverget_clearcount);
            m.Add("getdectpin_fromledjudger", getdectpin_fromledjudger);
            m.Add("ingoread_led_bright_value_ALL", ingoread_led_bright_value_ALL);

            /*outdoor 专用*/
            m.Add("noiseware_outdoor_getfwver",noiseware_outdoor_getfwver);
            m.Add("noiseware_outdoor_getNVflashstatus", noiseware_outdoor_getNVflashstatus);
            m.Add("noiseware_outdoor_MicTest", noiseware_outdoor_MicTest);
            m.Add("noiseware_outdoor_startsubgiga", noiseware_outdoor_startsubgiga);
            m.Add("noiseware_outdoor_stopsubgiga", noiseware_outdoor_stopsubgiga);

            #endregion


            #region   //通讯资源加载
            ///*屏蔽q
            #region 暂时不用的继电器板子
            /*暂时不用
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa"] != null) {
                       try
                       {
                           string sr_n_port = iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa"];
                           int sr_n_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa_baudrate"]);
                           relay = new ClassLibrary1.serial_no_visa(sr_n_port, sr_n_bautrate);
                       }
                       catch (Exception) {

                           System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["Relay_board"] +"不存在或被霸占,请检查" );
                       }
          
        }
暂时不用*/
            #endregion

            if (iniread.ReadFile("setup.ini")["setport"]["comlineformicread_port"] != null)
            {
                try
                {
                    string comlineformicread_port = iniread.ReadFile("setup.ini")["setport"]["comlineformicread_port"];
                    int comlineformicread_baudrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["comlineformicread_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    micread_noise = new comlineNRTS(comlineformicread_port, comlineformicread_baudrate);


                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["comlineformicread_port"] + " micnoiseread comm  are occupied ");

                }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["shieldboxport"] != null)
            {
                try
                {
                    string shieldboxport = iniread.ReadFile("setup.ini")["setport"]["shieldboxport"];
                    int shieldboxport_baudrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["shieldboxbaudrate"]);
                    //   MessageBox.Show(sr_port);
                    shieldbox = new comline(shieldboxport, shieldboxport_baudrate);


                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["shieldboxport"] + " shieldbox comm  are occupied ");

                }
            }

            if (iniread.ReadFile("setup.ini")["setport"]["comlineforingo_led_port"] != null)
            {
                try
                {
                    string comlineforingo_led_port = iniread.ReadFile("setup.ini")["setport"]["comlineforingo_led_port"];
                    int comlineforingo_led_baudrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["comlineforingo_led_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    ingoproj_led = new comlineforingo_led(comlineforingo_led_port, comlineforingo_led_baudrate);

                    //  relayself.WriteLine("@00000000000000000000000000000000@");
                    ingoproj_led.DiscardInBuffer();
                    //  ingoproj_led.setmainhwnd = this.ptrWnd;
                    ingoproj_led.setinterfacefuc = callbackwinmessage;
                  

                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["comlineforingo_led_port"] + "ingoledsp  are occupied ");

                }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["smartFreqSpecport"] != null)
            {
                try
                {
                    string smartspecport = iniread.ReadFile("setup.ini")["setport"]["smartFreqSpecport"];
                    int smartspecsmartbautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["smartFreqSpecbaudrate"]);
                    //   MessageBox.Show(sr_port);
                    smartFS = new smartFreqSpec(smartspecport, smartspecsmartbautrate);

                    //  relayself.WriteLine("@00000000000000000000000000000000@");
                    smartFS.DiscardInBuffer();
                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["smartFreqSpecport"] + "smartspec  are occupied ");

                }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["btdongleport"] != null)
            {
                try
                {
                    string bt_dongleport = iniread.ReadFile("setup.ini")["setport"]["btdongleport"];
                    int bt_donglebautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["btdonglerate"]);
                    //   MessageBox.Show(sr_port);
                    btdongle = new comline(bt_dongleport, bt_donglebautrate);

                    //  relayself.WriteLine("@00000000000000000000000000000000@");
                    btdongle.DiscardInBuffer();
                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["btdongleport"] + "btdongle  are occupied ");

                }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["HMS3000port"] != null)
            {
                try
                {
                    string hms3_port = iniread.ReadFile("setup.ini")["setport"]["HMS3000port"];
                    int hms3_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["HMS3000_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    hms  = new HMS3000_Instruments(hms3_port, hms3_bautrate);

                  //  relayself.WriteLine("@00000000000000000000000000000000@");
                    relayself.DiscardInBuffer();
                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["HMS3000port"] + "hms3000 Communication port is occupied or does not exist");

                }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["myrelay_board"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["myrelay_board"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["myrelay_board_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    relayself = new myrelay(sr_port, sr_bautrate);

                    relayself.WriteLine("@00000000000000000000000000000000@");
                    relayself.DiscardInBuffer();
                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["myrelay"] + ":The self-made relay board does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["gpd3033"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["gpd3033"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["gpd3033_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    gpd3033 = new GPD3033(sr_port, sr_bautrate);
                    gpd3033.DiscardInBuffer();
                    gpd3033.WriteLine("*IDN?");

                    string ret = gpd3033.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["gpd3033"] + ":gpd3033 does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["vc8145cmeter"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["vc8145cmeter"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["vc8145cmeter_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    vc8145 = new vc8145cmeter(sr_port, sr_bautrate);
                    ////vc8145.DiscardInBuffer();
                    vc8145.WriteLine("#*RST");
                    System.Threading.Thread.Sleep(500);
                    vc8145.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    vc8145.WriteLine("#*ONL");
                    // System.Threading.Thread.Sleep(500);
                    ////vc8145.DiscardInBuffer();
                    ////vc8145.WriteLine("#*ONL");
                    vc8145.ReadLine();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["vc8145cmeter"] + ":vc8145  does not exist or is occupied, please check");

                }
            }

            if (iniread.ReadFile("setup.ini")["setport"]["DM3058"] != null) {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["DM3058"];
                    // int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["DM3058_baudrate"]);
                    //   MessageBox.Show(sr_port);
                    dm3058 = new DM3058(sr_port.Trim());
                }
                catch (Exception) {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["DM3058"] + ":dm3058 does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"] != null) {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    mincurm = new TDM9001_2A(sr_port, sr_bautrate);
                    //  mincurm.read();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"] + " :tmd9001does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"] != null) {

                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TMD1501_50_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    minvm = new TMD1501_50(sr_port, sr_bautrate);

                    //   minvm.read();


                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"] + " :tmd1501 does not exist or is occupied, please check");

                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board"] != null) {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["Relay_board"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["Relay_board_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    ry = new sevy_relay(sr_port, sr_bautrate);

                    //  ry.set_relay(0X00,0x00);


                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["Relay_board"] + "485 relay board1  does not exist or is occupied, please check");

                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board2"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["Relay_board2"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["Relay_board2_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    ry2 = new sevy_relay(sr_port, sr_bautrate);

                    //  ry.set_relay(0X00,0x00);


                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["Relay_board2"] + "485 relay board2  does not exist or is occupied, please check");

                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["color_assyer"] != null) {
                try
                {
                    string color_assyer_port = iniread.ReadFile("setup.ini")["setport"]["color_assyer"];
                    int color_assyer_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["color_assyer_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    ledassyer = new led_assy(color_assyer_port, color_assyer_bautrate);

                   // ledassyer.try_comm();


                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["color_assyer"] + " :color judger module relay board1  does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TRM1201"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["TRM1201"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TRM1201_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    TRM1201reader = new TRM1201(sr_port, sr_bautrate);
                    //  mincurm.read();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TRM1201"] + ":TRM1201  does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["chroma19701"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["chroma19701"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["chroma19701_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    chrm = new chroma19701(sr_port, sr_bautrate);
                    chrm.write_comm(new byte[] { 0x2E, 0X01 });

                    int t = chrm.ReadByte();
                    if (t != 0xab) { throw new Exception(); }

                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["chroma19701"] + " :chroma19701 does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["comline"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["comline"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["comline_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    commandline = new comline(sr_port, sr_bautrate);
                    if (commandline.IsOpen == false) commandline.Close();
                    commandline.setdebuginfosend = callbackdebuginfo;
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["comline"] + " :comline does not exist or is occupied, please check");

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["led_sensor"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["led_sensor"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["led_sensor_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    led_sensor = new led_assy_self(sr_port, sr_bautrate);

                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["led_sensor"] + "The self-made led judger module does not exist or is occupied, please check");

                }
            }




            //屏蔽 */

            #endregion
        }
        private Dictionary<string, pointfun> m = new Dictionary<string, pointfun>();



        /* ingo  project test used 
         
            *******project 2020 09 03*********
            1為DCD PIN1 ,2為DSR PIN6 ,3為 CTS PIN 7  
         */
        #region /*for ingo*/
        string getdectpin_fromledjudger(string a, string b, out string c, string d = "") {

            c = "fail";
            string  judge = "fail";

            if (d == null) d = "1;3;30";

            string[] tm = d.Split(";".ToArray());

            bool ret = false;
            int count = int.Parse(tm[1]);

            do
            {
                System.Threading.Thread.Sleep(int.Parse(tm[2]));
               ret = ingoproj_led.getpinstatus(int.Parse(tm[0]));
                if (ret != bool.Parse(a)) { return "fail"; }
                
                if (count-- == 0) { judge = "pass"; break; }

            } while (true);





            c = "pass";

            return judge;

        }

        string ingofwverget_fromcurrent_cts_port (string a, string b, out string c, string d = ""){

            c="fail";
           string   judge="fail";
            mincurm.changecountclear();
            if (d == null) d = "3000";

            System.Threading.Thread.Sleep(int.Parse(d.Trim()));

            int z = mincurm.getchangecount();
            if (int.Parse(b) >= z && int.Parse(a) <= z)
            {

                judge = "pass";
            }
            else {
                judge = "fail";
            }
            c = z + "";
            return judge;
        }



        string ingofwverget_clearcount(string a, string b, out string c, string d = "")
        {
            c = "pass";
         
            ingoproj_led.setchangecount = 0;

            return "pass";
        }
        string ingofwverget_fromledjudge_cdc_port(string a, string b, out string c, string d = "")
        {

            c = "fail";
            string judge = "fail";

          //  ingoproj_led.setchangecount = 0;
            int z = ingoproj_led.readchangedcount_fromcdcport(3000);
            if (int.Parse(b) >= z && int.Parse(a) <= z)
            {

                judge = "pass";
            }
            else
            {
                judge = "fail";
            }
            c = z + "";
            return judge;
        }

        string ingoread_led_bright_value(string a, string b, out string c, string d = "") {

            c = "fail";
          

            string judge1 = "fail";
            int cu = 3;
            if (d == null) d = "1;3;1";

            string[] pt = d.Split(";".ToCharArray());
            if (pt.Length > 1)
            {
                cu = int.Parse(pt[1]);
            }
            d = pt[0];

            do
            {
                string[] lowlimit = b.Split(";".ToCharArray());
                int[] ll = new int[] { int.Parse(lowlimit[0]), int.Parse(lowlimit[1]), int.Parse(lowlimit[2]), int.Parse(lowlimit[3]) };

                string[] uplimit = a.Split(";".ToCharArray());

                int[] ul = new int[] { int.Parse(uplimit[0]), int.Parse(uplimit[1]), int.Parse(uplimit[2]), int.Parse(uplimit[3]) };

             
                int[] rsut = ingoproj_led.readled_common(int.Parse(pt[0]));
                c = rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                if (rsut[0] > ll[0] && rsut[0] < ul[0])
                {
                    if (rsut[1] > ll[1] && rsut[1] < ul[1])
                    {
                        if (rsut[2] > ll[2] && rsut[2] < ul[2])
                        {
                            if (rsut[3] > ll[3] && rsut[3] < ul[3])
                            {
                                if (int.Parse(pt[2]) == 1) {

                                    if (rsut[0] > rsut[2] && rsut[0] > rsut[1])
                                    {

                                        judge1 = "pass";

                                    }
                                    else {
                                        c = "not is redled:" + c;
                                        judge1 = "fail";

                                    }
                                }
                                if (int.Parse(pt[2]) == 2)
                                {

                                    if (rsut[1] > rsut[2] && rsut[1] > rsut[0])
                                    {
                                      
                                        judge1 = "pass";

                                    }
                                    else {
                                        c = "not is greenled:" + c;
                                        judge1 = "fail";

                                    }
                                }
                                if (int.Parse(pt[2]) == 3)
                                {

                                    if (rsut[2] > rsut[1] && rsut[2] > rsut[0])
                                    {
                                     
                                        judge1 = "pass";

                                    }
                                    else {

                                        c = "not is blueled:" + c;
                                        judge1 = "fail";

                                    }
                                }


                            }
                            else
                            {

                                c = "intensity componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                                judge1 = "fail";


                            }

                        }
                        else
                        {

                            c = "blue componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                            judge1 = "fail";



                        }

                    }
                    else
                    {
                        c = "green componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                        judge1 = "fail";


                    }


                }
                else
                {

                    c = "red componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                    judge1 = "fail";


                }
                cu--;
            } while (judge1 == "fail" && cu > 0);



            return judge1;

        }

        /*比较灯的红绿蓝数值，但不红绿色判断*/
        string ingoread_led_bright_value_ALL(string a, string b, out string c, string d = "")
        {

            c = "fail";
            string judge1 = "fail";
            int cu = 3;
            if (d == null) d = "1;1;1";

            string[] pt = d.Split(";".ToCharArray());
            if (pt.Length > 1)
            {
                cu = int.Parse(pt[1]);
            }
            d = pt[0];

            do
            {
                string[] lowlimit = b.Split(";".ToCharArray());
                int[] ll = new int[] { int.Parse(lowlimit[0]), int.Parse(lowlimit[1]), int.Parse(lowlimit[2]), int.Parse(lowlimit[3]) };

                string[] uplimit = a.Split(";".ToCharArray());

                int[] ul = new int[] { int.Parse(uplimit[0]), int.Parse(uplimit[1]), int.Parse(uplimit[2]), int.Parse(uplimit[3]) };


                int[] rsut = ingoproj_led.readled_common(int.Parse(pt[0]));
                c = rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                if (rsut[0] > ll[0] && rsut[0] < ul[0])
                {
                    if (rsut[1] > ll[1] && rsut[1] < ul[1])
                    {
                        if (rsut[2] > ll[2] && rsut[2] < ul[2])
                        {
                            if (rsut[3] > ll[3] && rsut[3] < ul[3])
                            {


                                judge1 = "pass";
                                c = rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;


                            }
                            else
                            {

                                c = "intensity componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                                judge1 = "fail";


                            }

                        }
                        else
                        {

                            c = "blue componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                            judge1 = "fail";



                        }

                    }
                    else
                    {
                        c = "green componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                        judge1 = "fail";


                    }


                }
                else
                {

                    c = "red componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                    judge1 = "fail";


                }
                cu--;
            } while (judge1 == "fail" && cu > 0);



            return judge1;

        }

        #endregion 




        string mic_read_noise(string a, string b, out string c, string d = "") {
            c = "fail";
            string judge = "fail";
            if (d == null) d = "1";
            try
            {
                micread_noise.Write(d + '\n');
                System.Threading.Thread.Sleep(int.Parse(d) * 1000 + 100);

                string p = micread_noise.ReadExisting();
                int i = int.Parse(p.Trim());

                if (i == 1) { judge = "fail"; }
                if (i == 0) { judge = "pass"; }
            }
            catch(Exception e) {

                c = "error";
                judge = "fail";
            }
           


            return judge;
        }

        /* 释放资源     */
        string releaseport(string a, string b, out string c, string d = "")
        {
            c = "pass";
            #region /*释放资源*/

            if (iniread.ReadFile("setup.ini")["setport"]["comlineformicread_port"] != null)
            {
                try
                {
                    if (micread_noise.IsOpen == true) micread_noise.Close();


                    //   string ret = relayself.ReadLine();
                }
                catch (Exception)
                {
                }
            }

            if (iniread.ReadFile("setup.ini")["setport"]["shieldboxport"] != null)
            {
                try
                {
                    if (shieldbox.IsOpen == true)
                    {
                        shieldbox.Close();
                    }
                  
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["comlineforingo_led_port"] != null)
            {
                try
                {
                    if (ingoproj_led.IsOpen == true)
                    {
                        ingoproj_led.Close();
                    }
                    
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["smartFreqSpecport"] != null)
            {
                try
                {

                    smartFS.Close();

                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["btdongleport"] != null)
            {
                try
                {

                    btdongle.Close();

                }
                catch (Exception)
                {

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["HMS3000port"] != null)
            {
                try
                {
                    if (hms.IsOpen == true)
                    {
                        hms.Close();
                    }
                  
                }
                catch (Exception)
                {

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["myrelay_board"] != null)
            {
                try
                {
                    if (relayself.IsOpen == true)
                    {
                        relayself.Close();
                    }
                   

                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["gpd3033"] != null)
            {
                try
                {
                    if (gpd3033.IsOpen == true)
                    {
                        gpd3033.Close();
                    }
                   

                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["vc8145cmeter"] != null)
            {
                try
                {
                    if (vc8145.IsOpen == true)
                    {
                        vc8145.Close();
                    }
                   

                }
                catch (Exception)
                {

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["DM3058"] != null)
            {
                try
                {
                   //if(dm3058.isOpen())
                   //{
                   //    dm3058.Close();
                   //}
                  
                }
                catch (Exception)
                {

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"] != null)
            {
                try
                {
                    if (mincurm.IsOpen == true)
                    {
                        mincurm.Close();

                    }
                   
                }
                catch (Exception)
                {

                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"] != null)
            {
                try
                {
                    if (minvm.IsOpen == true)
                    {
                        minvm.Close();
                    }
                   
                }
                catch (Exception)
                {
                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board"] != null)
            {
                try
                {
                    if (ry.IsOpen == true)
                    {
                        ry.Close();
                    }
                  
                    //  ry.set_relay(0X00,0x00);
                }
                catch (Exception)
                {
                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board2"] != null)
            {
                try
                {
                    if (ry2.IsOpen == true)
                    {
                        ry2.Close();
                    }
                   
                }
                catch (Exception)
                {
                }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["color_assyer"] != null)
            {
                try
                {
                    if (ledassyer.IsOpen == true)
                    {
                        ledassyer.Close();
                    }
                    
                    // ledassyer.try_comm();
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TRM1201"] != null)
            {
                try
                {
                   if (TRM1201reader.IsOpen == true){
                        TRM1201reader.Close();
                    }
                   
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["chroma19701"] != null)
            {
                try
                {
                    if (chrm.IsOpen == true)
                    {
                        chrm.Close();
                    }
                    
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["comline"] != null)
            {
                try
                {
                    if (commandline.IsOpen ==true) commandline.Close();
                }
                catch (Exception)
                {
                }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["led_sensor"] != null)
            {
                try
                {
                    if (led_sensor.IsOpen == true)
                    {
                        led_sensor.Close();
                    }
                    
                }
                catch (Exception)
                {
                }
            }

            #endregion
            return "pass";
        }
            string shieldboxopen(string a, string b, out string c, string d = "") {
            c = "pass";


            shieldbox.WriteLine("open");

            return "pass";
        
        
        }

        /*hackrf 模块*/
        string hackrf_read_cc1310(string a, string b, out string c, string d = "")
        {

           string hackrf_sweepPath =  iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string cc1310_parameter = iniread.ReadFile("setup.ini")["hackrf"]["cc1310_parameter"];
            //  MessageBox.Show(hackrf_sweepPath);
          
            double hackrf_cc1310_pathloss = 0;
            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_pathloss"] != null)
            {

                hackrf_cc1310_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_pathloss"].Trim());
            }

       

             c = "";
          //  return "pass";
            if (d == null) d = "433.3";
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = double.Parse(d.Trim());
            double startfreq = 423;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -100;
            bool isoveroffset = true;
            int wb = 25; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置
            double pkpower = -100;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 30; // 查找最大寬度 ：30*0.01M 大約0.3M
            for (int repget = 0; repget < 5; repget++)
            {
               // killproc("hackrf_sweep");
                Process process1 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
                startInfo.Arguments = cc1310_parameter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.RedirectStandardError = true;
                process1.StartInfo = startInfo;
                process1.ErrorDataReceived += Process1_ErrorDataReceived;
                try
                {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();
                    
                    //System.Threading.Thread thread = new System.Threading.Thread(() =>
                    //{

                    //    System.Threading.Thread.Sleep(MAXWAITtime);
                    //    killproc("hackrf_sweep");


                    //});
                    //thread.Start();
                    process1.BeginErrorReadLine();
                }
                catch
                {

                    MessageBox.Show("cc1310 loading error");
                    SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
                }
                for (int i = 0; i < point * 4; i++)
                {
                    stockdata[i] = -100;


                }
                //   StringBuilder m = new StringBuilder();

                int count = 0;
              
                try
                {
                   // int ts = Environment.TickCount;
                   // runstartts = Environment.TickCount;
                    do
                    {
                        try
                        {
                            string z = process1.StandardOutput.ReadLine();
                            //  process1.StandardError.ReadLine();
                            if (z == null) continue;
                            string[] p = z.Split(",".ToCharArray());
                            //  System.IO.File.AppendAllText("2.csv",z + "\n");
                            //for (int i = 0; i < 4; i++)
                            //{

                            if (p.Length != data_len) continue;
                            if (Double.Parse(p[2].Trim()) == 423000000)
                            {

                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }

                            }

                            if (Double.Parse(p[2].Trim()) == 428000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 433000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 438000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                                //}



                            }
                        }
                        catch
                        {

                            count++;

                            if (count > 30) throw new Exception("Too many Parse error");

                        }

                        //if (Environment.TickCount - ts > MAXWAITtime) {
                        //    process1.Kill();

                        //    break; }
                    } while (!process1.StandardOutput.EndOfStream);
                    process1.Close();

                    for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                    {

                        if (comp < stockdata[i])
                        {

                            comp = stockdata[i];
                            pkp = i;

                        }
                        // m.Append(stockdata[i] + ",");
                    }

                    if (Math.Abs(centerfreqpoint - pkp) < wb)
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = false;
                    }
                    else
                    {

                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = true;

                    }





                    //   System.IO.File.AppendAllText("1.csv", m.ToString());


                }
                catch
                {


                    c = "Parse error";
                    return "fail";


                }

                if ((pkpower + hackrf_cc1310_pathloss) > double.Parse(b.Trim())) break;

                }
            
            if (double.Parse(a.Trim()) > (pkpower + hackrf_cc1310_pathloss) && double.Parse(b.Trim()) < (pkpower + hackrf_cc1310_pathloss) &&  isoveroffset ==false)
            {

                c = (pkpower + hackrf_cc1310_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else {
                c = (pkpower + hackrf_cc1310_pathloss) + ":" + freqoffset;
                if (isoveroffset)
                {
                    c = "overoffset" + c;

                }
                
                return "fail";


            }

          




        }

        string hackrf_read_cc1310_900_1(string a, string b, out string c, string d = "")
        {
            double hack_cc1310_900_1_pathloss = 0;
            string hackrf_sweepPath = iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string cc1310_parameter = iniread.ReadFile("setup.ini")["hackrf"]["cc1310_900M_1_parameter"];
            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_1_pathloss"] != null) {

                hack_cc1310_900_1_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_1_pathloss"].Trim());
            }
            //  MessageBox.Show(hackrf_sweepPath);

            //System.Threading.Thread thread = new System.Threading.Thread(() =>
            //{

            //    System.Threading.Thread.Sleep(MAXWAITtime);
            //    killproc("hackrf_sweep");


            //});


            c = "";
            //  return "pass";
            if (d == null) d = "902.2";
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = double.Parse(d.Trim());
            double startfreq = 890;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -100;
            bool isoveroffset = true;
            int wb = 25; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置
            double pkpower = -100;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 30; // 查找最大寬度 ：30*0.01M 大約0.3M
            for (int repget = 0; repget < 5; repget++)
            {
               

                Process process1 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
                startInfo.Arguments = cc1310_parameter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.RedirectStandardError = true;
                process1.StartInfo = startInfo;
                process1.ErrorDataReceived += Process1_ErrorDataReceived;
                try
                {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();

                    process1.BeginErrorReadLine();
                }
                catch(Exception e)
                {

                    MessageBox.Show("cc1310 loading error");
                    SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
                }
                for (int i = 0; i < point * 4; i++)
                {
                    stockdata[i] = -100;


                }
                //   StringBuilder m = new StringBuilder();

                int count = 0;

                try
                {
                   // int ts = Environment.TickCount;
                   // runstartts = Environment.TickCount;
                    do
                    {
                        try
                        {
                            string z = process1.StandardOutput.ReadLine();
                            //  process1.StandardError.ReadLine();
                            if (z == null) continue;
                            string[] p = z.Split(",".ToCharArray());
                            //  System.IO.File.AppendAllText("2.csv",z + "\n");
                            //for (int i = 0; i < 4; i++)
                            //{

                            if (p.Length != data_len) continue;
                            if (Double.Parse(p[2].Trim()) == 890000000)
                            {

                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }

                            }

                            if (Double.Parse(p[2].Trim()) == 895000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 900000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 905000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                                //}



                            }
                        }
                        catch
                        {

                            count++;

                            if (count > 30) throw new Exception("Too many Parse error");

                        }

                        //if (Environment.TickCount - ts > MAXWAITtime) {
                        //    process1.Kill();

                        //    break; }
                    } while (!process1.StandardOutput.EndOfStream);
                    process1.Close();

                    for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                    {

                        if (comp < stockdata[i])
                        {

                            comp = stockdata[i];
                            pkp = i;

                        }
                        // m.Append(stockdata[i] + ",");
                    }

                    if (Math.Abs(centerfreqpoint - pkp) < wb)
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = false;
                    }
                    else
                    {

                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = true;

                    }





                    //   System.IO.File.AppendAllText("1.csv", m.ToString());


                }
                catch
                {


                    c = "Parse error";
                    return "fail";


                }

                if ((pkpower + hack_cc1310_900_1_pathloss) > double.Parse(b.Trim())) break;

            }

            if (double.Parse(a.Trim()) > (pkpower + hack_cc1310_900_1_pathloss) && double.Parse(b.Trim()) < (pkpower + hack_cc1310_900_1_pathloss) && isoveroffset == false)
            {

                c = (pkpower + hack_cc1310_900_1_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else
            {
                c = (pkpower + hack_cc1310_900_1_pathloss) + ":" + freqoffset;
                if (isoveroffset)
                {
                    c = "overoffset" + c;

                }

                return "fail";


            }






        }
        string hackrf_read_cc1310_900_2(string a, string b, out string c, string d = "")
        {
            double hackrf_cc1310_900_2_pathloss = 0;
            string hackrf_sweepPath = iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string cc1310_parameter = iniread.ReadFile("setup.ini")["hackrf"]["cc1310_900M_2_parameter"];
            //  MessageBox.Show(hackrf_sweepPath);
            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_2_pathloss"] != null) {

                hackrf_cc1310_900_2_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_2_pathloss"].Trim());
            }
            c = "";
            //  return "pass";
            if (d == null) d = "915.0";
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = double.Parse(d.Trim());
            double startfreq = 910;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -100;
            bool isoveroffset = true;
            int wb = 25; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置
            double pkpower = -100;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 30; // 查找最大寬度 ：30*0.01M 大約0.3M
            for (int repget = 0; repget < 5; repget++)
            {
             
                Process process1 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
                startInfo.Arguments = cc1310_parameter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.RedirectStandardError = true;
                process1.StartInfo = startInfo;
                process1.ErrorDataReceived += Process1_ErrorDataReceived;
                try
                {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();
                    process1.BeginErrorReadLine();
                }
                catch
                {

                    MessageBox.Show("cc1310 loading error");
                    SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
                }
                for (int i = 0; i < point * 4; i++)
                {
                    stockdata[i] = -100;


                }
                //   StringBuilder m = new StringBuilder();

                int count = 0;
//               int test = 0;
                try
                {
                 //   int ts = Environment.TickCount;
                  //  runstartts = Environment.TickCount;
                    do
                    {
                        try
                        {
  //                          test++;
                            string z = process1.StandardOutput.ReadLine();
                            //  process1.StandardError.ReadLine();
                            if (z == null) continue;
                            string[] p = z.Split(",".ToCharArray());
                            //  System.IO.File.AppendAllText("2.csv",z + "\n");
                            //for (int i = 0; i < 4; i++)
                            //{

                            if (p.Length != data_len) continue;
                            if (Double.Parse(p[2].Trim()) == 910000000)
                            {

                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }

                            }

                            if (Double.Parse(p[2].Trim()) == 915000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 920000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 925000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                                //}



                            }
                        }
                        catch
                        {

                            count++;

                            if (count > 30) throw new Exception("Too many Parse error");

                        }

                        //if (Environment.TickCount - ts > MAXWAITtime) {
                        //    process1.Kill();

                        //    break; }
                    } while (!process1.StandardOutput.EndOfStream);
                    process1.Close();
                //    StringBuilder m = new StringBuilder();
                    for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                    {

                        if (comp < stockdata[i])
                        {

                            comp = stockdata[i];
                            pkp = i;

                        }
                        
                    }


                    if (Math.Abs(centerfreqpoint - pkp) < wb)
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = false;
                    }
                    else
                    {

                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = true;

                    }





                    //   System.IO.File.AppendAllText("1.csv", m.ToString());


                }
                catch
                {


                    c = "Parse error";
                    return "fail";


                }

                if ((pkpower + hackrf_cc1310_900_2_pathloss) > double.Parse(b.Trim())) break;

            }

            if (double.Parse(a.Trim()) > (pkpower + hackrf_cc1310_900_2_pathloss) && double.Parse(b.Trim()) < (pkpower + hackrf_cc1310_900_2_pathloss) && isoveroffset == false)
            {

                c = (pkpower + hackrf_cc1310_900_2_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else
            {
                c = (pkpower + hackrf_cc1310_900_2_pathloss) + ":" + freqoffset;
                if (isoveroffset)
                {
                    c = "overoffset" + c;

                }

                return "fail";


            }






        }
        string hackrf_read_cc1310_900_3(string a, string b, out string c, string d = "")
        {
            double hackrf_cc1310_900_3_pathloss = 0;
            string hackrf_sweepPath = iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string cc1310_parameter = iniread.ReadFile("setup.ini")["hackrf"]["cc1310_900M_3_parameter"];

            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_3_pathloss"] != null) {

                hackrf_cc1310_900_3_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_cc1310_900_3_pathloss"].Trim());
            
            }


            //  MessageBox.Show(hackrf_sweepPath);
            //System.Threading.Thread thread = new System.Threading.Thread(() =>
            //{

                //    System.Threading.Thread.Sleep(MAXWAITtime);
                //    killproc("hackrf_sweep");


                //});

                c = "";
            //  return "pass";
            if (d == null) d = "927.0";
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = double.Parse(d.Trim());
            double startfreq = 920;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -100;
            bool isoveroffset = true;
            int wb = 25; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置
            double pkpower = -100;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 30; // 查找最大寬度 ：30*0.01M 大約0.3M
            for (int repget = 0; repget < 5; repget++)
            {
               
                Process process1 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
                startInfo.Arguments = cc1310_parameter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.RedirectStandardError = true;
                process1.StartInfo = startInfo;
                process1.ErrorDataReceived += Process1_ErrorDataReceived;
                try
                {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();

                    //System.Threading.Thread thread = new System.Threading.Thread(() =>
                    //{

                      
                    //    System.Threading.Thread.Sleep(MAXWAITtime);
                    //    killproc("hackrf_sweep");
                    //    MessageBox.Show("Test");

                    //});

                    //thread.Start();
                    //    if (thread.IsAlive) { try { thread.Abort(); } catch (Exception e) { MessageBox.Show(e.Message);} }
                    // thread.Start();
                    process1.BeginErrorReadLine();
                }
                catch(Exception e)
                {

                    MessageBox.Show(e.Message);
                    SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
                }
                for (int i = 0; i < point * 4; i++)
                {
                    stockdata[i] = -100;


                }
                //   StringBuilder m = new StringBuilder();

                int count = 0;

                try
                {
                   // int ts = Environment.TickCount;
                   // runstartts = Environment.TickCount;
                    do
                    {
                        try
                        {
                            string z = process1.StandardOutput.ReadLine();
                            //  process1.StandardError.ReadLine();
                            if (z == null) continue;
                            string[] p = z.Split(",".ToCharArray());
                            //  System.IO.File.AppendAllText("2.csv",z + "\n");
                            //for (int i = 0; i < 4; i++)
                            //{

                            if (p.Length != data_len) continue;
                            if (Double.Parse(p[2].Trim()) == 920000000)
                            {

                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }

                            }

                            if (Double.Parse(p[2].Trim()) == 925000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 930000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 935000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                                //}



                            }
                        }
                        catch
                        {

                            count++;

                            if (count > 30) throw new Exception("Too many Parse error");

                        }

                        //if (Environment.TickCount - ts > MAXWAITtime) {
                        //    process1.Kill();

                        //    break; }
                    } while (!process1.StandardOutput.EndOfStream);
                    process1.Close();

                    for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                    {

                        if (comp < stockdata[i])
                        {

                            comp = stockdata[i];
                            pkp = i;

                        }
                        // m.Append(stockdata[i] + ",");
                    }

                    if (Math.Abs(centerfreqpoint - pkp) < wb)
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = false;
                    }
                    else
                    {

                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = true;

                    }





                    //   System.IO.File.AppendAllText("1.csv", m.ToString());


                }
                catch
                {


                    c = "Parse error";
                    return "fail";


                }

                if ((pkpower + hackrf_cc1310_900_3_pathloss) > double.Parse(b.Trim())) break;

            }

            if (double.Parse(a.Trim()) > (pkpower + hackrf_cc1310_900_3_pathloss) && double.Parse(b.Trim()) < (pkpower + hackrf_cc1310_900_3_pathloss) && isoveroffset == false)
            {

                c = (pkpower + hackrf_cc1310_900_3_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else
            {
                c = (pkpower + hackrf_cc1310_900_3_pathloss) + ":" + freqoffset;
                if (isoveroffset)
                {
                    c = "overoffset" + c;

                }

                return "fail";


            }






        }
        private void Process1_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            string m = e.Data;
            
            // m = null;
            if (m == null) return;
            if (m.IndexOf("0 total sweeps completed") >= 0) { diecount++; }
            if(diecount>30 && killflog ==0)
            {
                diecount = 0;
                killflog = 1;
                //  killproc("hackrf_sweep");
               // ((Process)sender).Close();
               new System.Threading.Thread(() =>
                {
                    killproc("hackrf_sweep");

                }).Start();






            }
            //    killproc("hackrf_sweep");
            //    return;
            //}
            //   System.Threading.Thread.Sleep(1);
            // Debug.Write(m);
            //  ((Process)sender).Close();

        }

        string hackrf_read_bt(string a, string b, out string c, string d = "")
        {

            double hackrf_bt_pathloss = 0;
            string hackrf_sweepPath = iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string btwifi_parameter = iniread.ReadFile("setup.ini")["hackrf"]["bt_wifi_parameter"];

            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_bt_pathloss"] != null) {

                hackrf_bt_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_bt_pathloss"].Trim());
            }
            c = "";
            
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = 2402;
            double startfreq = 2400;
            bool isoveroffset = true;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -222;//用於保留最大值緩存，預設一個初始值，沒有意義
            int wb = 90; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置，預設一個初始值，沒有意義
            double pkpower = -222;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 100; // 查找最大寬度 ：300*0.01M 大約1.0M

            for (int repget = 0; repget < 5; repget++)
            {
              
                Process process1 = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
                startInfo.Arguments = btwifi_parameter;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;
                startInfo.UseShellExecute = false; //不使用系统外壳程序启动
                startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
                startInfo.RedirectStandardOutput = true; //重定向输出
                startInfo.RedirectStandardError = true;
                process1.StartInfo = startInfo;

                process1.ErrorDataReceived += Process1_ErrorDataReceived;
                try
                {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();
                   process1.BeginErrorReadLine();

                    //System.Threading.Thread thread = new System.Threading.Thread(() =>
                    //{

                    //    System.Threading.Thread.Sleep(MAXWAITtime);
                    //    killproc("hackrf_sweep");


                    //});
                    //thread.Start();



                }
                catch(Exception e)
                {

                    MessageBox.Show(e.Message + e.StackTrace);
                   // SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
                }

                for (int i = 0; i < point * 4; i++)
                {
                    stockdata[i] = -222;


                }
                //  StringBuilder m = new StringBuilder();


                int count = 0;
                int test = 0;
                try
                {
                  //  int ts = Environment.TickCount;
                 //   runstartts = Environment.TickCount;
                    do
                    {
                        try
                        {
                            string z = process1.StandardOutput.ReadLine();
                            //  System.IO.File.AppendAllText("3.csv", z + "\n");
                            test++;
                            if (z == null) continue;

                            string[] p = z.Split(",".ToCharArray());


                            if (p.Length != data_len) continue;
                            if (Double.Parse(p[2].Trim()) == 2400000000)
                            {

                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }



                                }

                            }

                            if (Double.Parse(p[2].Trim()) == 2405000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 2410000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop].Trim()))
                                    {

                                        stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                            }

                            if (Double.Parse(p[2].Trim()) == 2415000000)
                            {
                                for (int levelloop = 0; levelloop < point; levelloop++)
                                {

                                    if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                    {

                                        stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                    }

                                }
                                //}



                            }



                        }
                        catch
                        {
                            count++;

                            if (count > 30) throw new Exception("Too many Parse error");

                        }

                        //if (Environment.TickCount - ts > MAXWAITtime) {
                        //process1.Kill();
                        //break; }

                    } while (!process1.StandardOutput.EndOfStream);
            
                    process1.Close();


                    for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                    {

                        if (comp < stockdata[i])//循環找區間内峰值
                        {

                            comp = stockdata[i];
                            pkp = i;

                        }
                        // m.Append(stockdata[i] + ",");
                    }

                    if (Math.Abs(centerfreqpoint - pkp) < wb)
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = false;
                    }
                    else
                    {
                        pkpower = stockdata[pkp];
                        freqoffset = 0.01 * (pkp - centerfreqpoint);
                        isoveroffset = true;

                    }


                    //   System.IO.File.AppendAllText("1.csv", m.ToString());


                }
                catch (Exception e)
                {

                    MessageBox.Show(e.Message + e.StackTrace);
                    c = "Parse error";
                    return "fail";


                }

                if (((pkpower + hackrf_bt_pathloss) > double.Parse(b.Trim()))&& ((pkpower + hackrf_bt_pathloss) < double.Parse(a.Trim()))) break;

            }
            //for (int i = 0; i < stockdata.Length; i++)
            //{

            //    System.IO.File.AppendAllText("bt.csv", stockdata[i] + ",");
            //}



            if (double.Parse(a.Trim()) > (pkpower + hackrf_bt_pathloss) && double.Parse(b.Trim()) < (pkpower + hackrf_bt_pathloss) && isoveroffset == false)
            {

                c = (pkpower + hackrf_bt_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else
            {

                c = (pkpower + hackrf_bt_pathloss) + ":" + freqoffset;
                if (isoveroffset == true) {
                    c = "offset over :" + c;
                }
                return "fail";


            }







        }
        string hackrf_read_wifi(string a, string b, out string c, string d = "")
        {
            double hackrf_wifi_pathloss = 0;
            string hackrf_sweepPath = iniread.ReadFile("setup.ini")["hackrf"]["hackrf_sweepPath"];
            string btwifi_parameter = iniread.ReadFile("setup.ini")["hackrf"]["bt_wifi_parameter"];
            if (iniread.ReadFile("setup.ini")["hackrf"]["hackrf_wifi_pathloss"] != null) {

                hackrf_wifi_pathloss = double.Parse(iniread.ReadFile("setup.ini")["hackrf"]["hackrf_wifi_pathloss"].Trim());
            }

            c = "";
            const int point = 501;
            const int data_len = 507;
            float[] stockdata = new float[point * 4];
            double centerfreq = 2412;
            double startfreq = 2400;
           bool isoveroffset = true;
            int centerfreqpoint = (int)((centerfreq - startfreq) / 0.01);
            double comp = -222;
            int wb = 650; //允許的中心偏移位置極限
            int pkp = 0;//峰值點位置，預設一個初始值，沒有意義
            double pkpower = -222;//預設一個初始值，沒有意義
            double freqoffset = -222; //預設一個初始值，沒有意義
            const int maxoffsetpoint = 700; // 查找最大寬度 ：100*0.01M 大約1M

            for (int repget = 0; repget < 5; repget++)
            {
             
                Process process1 = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo(hackrf_sweepPath);
            startInfo.Arguments = btwifi_parameter;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.UseShellExecute = false; //不使用系统外壳程序启动
            startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
            startInfo.RedirectStandardOutput = true; //重定向输出
            startInfo.RedirectStandardError = true;
            process1.StartInfo = startInfo;
            
            process1.ErrorDataReceived += Process1_ErrorDataReceived;

       
            try
            {
                    diecount = 0; //kill sweep.exe 用
                    killflog = 0;
                    process1.Start();
               
                process1.BeginErrorReadLine();

                System.Threading.Thread thread = new System.Threading.Thread(() =>
                {

                    System.Threading.Thread.Sleep(MAXWAITtime);
                    killproc("hackrf_sweep");


                });
                thread.Start();
            }
            catch {

                MessageBox.Show("wifibat loading error");
                SendMessage(ptrWnd, WM_SENDD, IntPtr.Zero, "");
            }
           
            for (int i = 0; i < point * 4; i++)
            {
                stockdata[i] = -222;


            }
            // StringBuilder m = new StringBuilder();

            int count = 0;
            int debgucount = 0;
            string z;
            try
            {
               // int ts = Environment.TickCount;
                //runstartts = Environment.TickCount;
                do
                {
                   
                    try
                    {
                       z = process1.StandardOutput.ReadLine();
                        debgucount++;
                        if (z == null) continue;
                        string[] p = z.Split(",".ToCharArray());
                        //  System.IO.File.AppendAllText("2.csv",z + "\n");
                        //for (int i = 0; i < 4; i++)
                        //{

                        if (p.Length != data_len) continue;
                        if (Double.Parse(p[2].Trim()) == 2400000000)
                        {

                            for (int levelloop = 0; levelloop < point; levelloop++)
                            {

                                if (stockdata[levelloop] < float.Parse(p[data_len - point + levelloop]))
                                {

                                    stockdata[levelloop] = float.Parse(p[data_len - point + levelloop]);
                                }

                            }

                        }

                        if (Double.Parse(p[2].Trim()) == 2405000000)
                        {
                            for (int levelloop = 0; levelloop < point; levelloop++)
                            {

                                if (stockdata[point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                {

                                    stockdata[point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                }

                            }
                        }

                        if (Double.Parse(p[2].Trim()) == 2410000000)
                        {
                            for (int levelloop = 0; levelloop < point; levelloop++)
                            {

                                if (stockdata[2 * point + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                {

                                    stockdata[2 * point + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                }

                            }
                        }

                        if (Double.Parse(p[2].Trim()) == 2415000000)
                        {
                            for (int levelloop = 0; levelloop < point; levelloop++)
                            {

                                if (stockdata[(3 * point) + levelloop] < float.Parse(p[data_len - point + levelloop]))
                                {

                                    stockdata[(3 * point) + levelloop] = float.Parse(p[data_len - point + levelloop]);
                                }

                            }
                            //}



                        }
                    }
                    catch {
                        count++;
                        if (count > 30) throw new Exception("Too many Parse error");


                    }


                    //if (Environment.TickCount - ts > MAXWAITtime)
                    //{
                    //    process1.Kill();

                    //    break;
                    //}

                } while (!process1.StandardOutput.EndOfStream);
                    process1.Close();
             


                for (int i = centerfreqpoint - maxoffsetpoint; i < centerfreqpoint + maxoffsetpoint; i++)
                {

                    if (comp < stockdata[i])//循環找區間内峰值
                    {

                        comp = stockdata[i];
                        pkp = i;
                      
                    }
                    // m.Append(stockdata[i] + ",");
                }




                if (Math.Abs(centerfreqpoint - pkp) < wb)
                {
                    pkpower = stockdata[pkp];
                    freqoffset = 0.01 * (pkp - centerfreqpoint);
                    isoveroffset = false;
                }
                else
                {
                    pkpower = stockdata[pkp];
                    freqoffset = 0.01 * (pkp - centerfreqpoint);
                    isoveroffset = true;


                }


                //   System.IO.File.AppendAllText("1.csv", m.ToString());


            }
            catch
            {


                c = "Parse error";
                return "fail";


            }

                if (((pkpower + hackrf_wifi_pathloss) > double.Parse(b.Trim())) && ((pkpower + hackrf_wifi_pathloss) < double.Parse(a.Trim()))) break;
            }
            //for (int i = 0; i < stockdata.Length; i++)
            //{

            //    System.IO.File.AppendAllText("wifi.csv", stockdata[i] + ",");
            //}


            if (double.Parse(a.Trim()) > (pkpower + hackrf_wifi_pathloss) && double.Parse(b.Trim()) < (pkpower + hackrf_wifi_pathloss) && isoveroffset == false)
            {

                c = (pkpower + hackrf_wifi_pathloss) + ":" + freqoffset;
                return "pass";

            }
            else
            {
                
                c = (pkpower + hackrf_wifi_pathloss) + ":" + freqoffset;
                if (isoveroffset == true)
                {
                    c = "offset over :" + c;
                }
                return "fail";


            }

           

        }









        /*簡易頻譜儀*/
        string smartfreqspc_read(string a, string b, out string c, string d = "")
        {
            string judge = "pass";
            c = "pass";

            //smartFS.readstringuntil(88000, 300);

            UInt32 freq;
            double level;
            bool isfreq;
            smartFS.ScanFreqSpec(out freq, out level, out isfreq);

            c = "" + freq + ";" + level + ";" + isfreq;
            return judge;
        }





            string cmw100CalibationCheck(string a, string b, out string c, string d = "") {

            string judge = "pass";
            c = "pass";
            string jyear = iniread.ReadFile("setup.ini")["cmw100statuscheck"]["statusyear"];
            string jmonth = iniread.ReadFile("setup.ini")["cmw100statuscheck"]["statusmonth"];
            string jday = iniread.ReadFile("setup.ini")["cmw100statuscheck"]["statusday"];
            string jhour = iniread.ReadFile("setup.ini")["cmw100statuscheck"]["statushour"];
            string jcycle = iniread.ReadFile("setup.ini")["cmw100statuscheck"]["checkcycle"];
            DateTime dateTime = DateTime.Now;

            if (dateTime.Year != int.Parse(jyear))
            {
                c = "calibration overdue ";
                judge="fail";
            }
            if (dateTime.Month != int.Parse(jmonth))
            {
                c = "calibration overdue ";
                judge = "fail";
            }
            if (dateTime.Day != int.Parse(jday))
            {
                c = "calibration overdue ";
                judge = "fail";
            }
            if (dateTime.Hour - int.Parse(jhour) > int.Parse(jcycle))
            {
                c = "calibration overdue ";
                judge = "fail";
            }   
            return judge;
        }

        string cmw100calibation_save(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            if (this.calibrationflog1 == true && this.calibrationflog2 && this.calibrationflog3==true)
            {

                SendMessage(ptrWnd, WM_SENDC, IntPtr.Zero, DateTime.Now.Year + ";" + DateTime.Now.Month + ";" + DateTime.Now.Day + ";"+ DateTime.Now.Hour);
                judge = "pass";
                c = "pass";
            }
            else {

                judge = "fail";
                c = "fail";
            }

           

            return judge;
        }
        string cmw100CalibationBlueTooth_Txloss(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            bool fg;
            double loss = 0;
           bool isfreqdev = true;

            // MessageBox.Show(dt["cmw100ParameterSet"]["cc1310loss"]);

            int count = 0;
            cmw100_SpectrumAnalyzersNonSCPI BlueTooth;

            do
            {
                BlueTooth = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);
                fg = BlueTooth.init_SpectrumAnalyzers(2402000000 /* centerFreq =2402000000Hz*/,
                                                           0/*reflevel = 10 dbm*/,
                                                           2000000/*span =20000000 Hz*/,
                                                           200000/* RBW = 2000000Hz*/,
                                                           100000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                            5/*sweep time */);

                if (count >= 3)
                {

                    c = "cmw100 init error ";

                    return "fail";

                }
                count++;
            } while (!fg);


            double levelvale=-1000;
            double  offset = 1000000000;
            count = 0;

            do
            {




                if (count >= 3)
                {

                    if (!fg)
                    {
                        c = "read error ";

                        return "fail";
                    }
                    else
                    {


                        c = levelvale + "";
                        return "fail";
                    }

                }

                fg = BlueTooth.getmark_feq_level(50/*boardwdth*/, out levelvale, out offset,out isfreqdev, 0,1000);

                c = "" + levelvale + ";" + offset;


                string sample1_bluetooth_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1_bluetooth_standardvalues"].Trim();


                if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1sn"])
                {
                    if (Math.Abs(double.Parse(sample1_bluetooth_standardvalue) - levelvale) <2 )
                    {

                        loss = double.Parse(sample1_bluetooth_standardvalue) - levelvale;

                        break;
                    }


                }
                else if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2sn"])
                {

                    string sample2_bluetooth_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2_bluetooth_standardvalues"].Trim();

                    if (Math.Abs(double.Parse(sample1_bluetooth_standardvalue) - levelvale) < 2)
                    {

                        loss = double.Parse(sample2_bluetooth_standardvalue) - levelvale;
                        break;
                    }

                }
                else
                {
                    c = " can not is sample";
                    return "fail";
                }
                count++;
            } while (true);

         //   SendMessage(ptrWnd, WM_SEND_SET_BTLOSS, IntPtr.Zero, loss + "");
            c = "" + levelvale + ";" + offset;
            return "pass";

        }
        string cmw100CalibationWIFI_Txloss(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            bool fg;
            double offset = 0;
            // MessageBox.Show(dt["cmw100ParameterSet"]["cc1310loss"]);
            bool isfreqdev = true;
            int count = 0;
            cmw100_SpectrumAnalyzersNonSCPI wifitest;
          
            do
            {
                wifitest = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);
                fg = wifitest.init_SpectrumAnalyzers(2412000000 /* centerFreq =2402000000Hz*/,
                                                           0/*Eattenuation*/,
                                                           20000000/*span =20000000 Hz*/,
                                                           1000000/* RBW = 2000000Hz*/,
                                                           100000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                           5/*sweep time */);

                if (count >= 3)
                {

                    c = "cmw100 init error ";

                    return "fail";

                }
                count++;
            } while (!fg);


            double levelvale=-1000;
            double loss = 0;
            count = 0;

            do
            {




                if (count >= 3)
                {

                    if (!fg)
                    {
                        c = "read error ";

                        return "fail";
                    }
                    else
                    {


                        c = levelvale + "";
                        return "fail";
                    }

                }

                fg = wifitest.getmark_feq_level(50/*boardwdth*/, out levelvale, out offset,out isfreqdev, 0,3000);

                c = "" + levelvale + ";" + offset;


                string sample1_wifi_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1_wifi_standardvalues"].Trim();


                if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1sn"])
                {
                    if ((double.Parse(sample1_wifi_standardvalue) + 2) >= levelvale && (double.Parse(sample1_wifi_standardvalue) - 2) <= levelvale)
                    {

                        loss = double.Parse(sample1_wifi_standardvalue) - levelvale;

                        break;
                    }


                }
                else if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2sn"])
                {

                    string sample2_wifi_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2_wifi_standardvalues"].Trim();

                    if ((double.Parse(sample2_wifi_standardvalue) + 2) >= levelvale && (double.Parse(sample2_wifi_standardvalue) - 2) <= levelvale)
                    {

                        loss = double.Parse(sample2_wifi_standardvalue) - levelvale;
                        break;
                    }

                }
                else
                {
                    c = " can not is gold sample";
                    return "fail";
                }
                count++;
            } while (true);

         //   SendMessage(ptrWnd, WM_SEND_SET_WIFILOSS, IntPtr.Zero, loss + "");
            c = "" + levelvale + ";" + offset;
            return "pass";

        }
        string cmw100CalibationBlueTooth_TxTest(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            cmw100_bluetooth_tx txtest = new cmw100_bluetooth_tx();

            txtest.DisplayView();

            string ENPOER = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["buletoothloss"];
            txtest.set_input_config();
            txtest.set_input_mode();
            txtest.driver_start();
            txtest.measure_static_count();

           c= ""+ txtest.getTxPowerResult();

           // SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, "144");


            return judge;

        }

        string cmw100Calibation_CC1310_Txloss(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            bool fg;
            double offset = 1000000000;
            // MessageBox.Show(dt["cmw100ParameterSet"]["cc1310loss"]);

            int count = 0;
            cmw100_SpectrumAnalyzersNonSCPI cc1310test;

            do
            {
                cc1310test = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);
                fg = cc1310test.init_SpectrumAnalyzers(433300000 /* centerFreq =2402000000Hz*/,
                                                           0/*Eattenuation dbm*/,
                                                           10000000/*span =20000000 Hz*/,
                                                           1000000/* RBW = 2000000Hz*/,
                                                           100000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                            0.05/*sweep time */);

                if (count > 3)
                {

                    c = "cmw100 init error ";

                    return "fail";

                }
                count++;
            } while (!fg);


            double levelvale=-1000;
            bool isfreqdev=true;
    
            count = 0;

            do
            {




                if (count > 3)
                {
                    if (!fg)
                    {
                        c = "read error ";

                        return "fail";
                    }
                    else {


                        c = levelvale+"";
                        return "fail";
                    }

                }

                fg = cc1310test.getmark_feq_level(150/*boardwdth*/, out levelvale, out offset,out isfreqdev ,0,1000);

                c = "" + levelvale + ";" + offset;


                string sample1_cc1310_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1_cc1310_standardvalues"].Trim();


                if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample1sn"])
                {
                    if ((double.Parse(sample1_cc1310_standardvalue) + 2) >= levelvale && (double.Parse(sample1_cc1310_standardvalue) - 2) <= levelvale) {

                        offset = double.Parse(sample1_cc1310_standardvalue) - levelvale;
                        break;
                    }


                }
                else if (this.trf == iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2sn"])
                {

                    string sample2_cc1310_standardvalue = iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["sample2_cc1310_standardvalues"].Trim();

                    if ((double.Parse(sample2_cc1310_standardvalue) + 2) >= levelvale && (double.Parse(sample2_cc1310_standardvalue) - 2) <= levelvale)
                    {

                        offset = double.Parse(sample2_cc1310_standardvalue) - levelvale;
                        break;
                    }

                }
                else {
                    c = " can not is sample";
                    return "fail";     
                }
               count++;
            } while (true);

         //   SendMessage(ptrWnd, WM_SEND_SET_CC1310LOSS, IntPtr.Zero, offset+"");
            c = "" + levelvale + ";" + offset;
            return "pass";

        }


        string cmw100_GPS_BER(string a, string b, out string c, string d = "")
        {
            string judge = "pass";
            c = "pass";


            return judge;

        }

        string cmw100_BT_BERTest(string a, string b, out string c, string d = "")
        {
            string judge = "pass";
            c = "pass";

            cmw100_bluetooth_rx btrx = new cmw100_bluetooth_rx();

            btrx.bluetooth_ModeSet(1);



            return judge;

        }
        string cmw100_sepctrumsnip(string a, string b, out string c, string d = "") {
            string judge = "fail";
            c = "fail";

            if (d == null) d = "2.412;10;200;200;300;1001";

            string[] temp = d.Split(";".ToArray());

            double certerfreq = double.Parse(temp[0].Trim());
            double reflevel = double.Parse(temp[1].Trim());
            double span = double.Parse(temp[2].Trim());
            double rbw = double.Parse(temp[3].Trim());
            double vbw = double.Parse(temp[4].Trim());
            double swepoint = double.Parse(temp[5].Trim());
            cmw100_SpectrumAnalyzers spa = new cmw100_SpectrumAnalyzers();

            bool m = false;
            int count = 0;
            double markfreq = -1000;
            double marklevel = -1000;
            do
            {
                m = spa.init_SpectrumAnalyzers(certerfreq, reflevel, span, rbw, vbw, swepoint);
                if (count > 3) { c = "spect init fail"; return "fail";  }
                count++;

            } while (!m);


            do
            {

                m = spa.getmark_feq_level(out markfreq, out marklevel);
                if (count > 3) { c = "read fail"; return "fail"; }
                count++;

            } while (!m);

            c = markfreq + ";" + marklevel;
            double limitupfeq = double.Parse(a.Trim().Split(";".ToArray())[0]);
            double limitlowfeq = double.Parse(b.Trim().Split(";".ToArray())[0]);
            double limituppower = double.Parse(a.Trim().Split(";".ToArray())[1]);
            double limitlowpower = double.Parse(b.Trim().Split(";".ToArray())[1]);
            if (markfreq > limitlowfeq && markfreq < limitupfeq)
            {

                if (marklevel > limitlowpower && marklevel < limituppower)
                {


                    judge = "pass";


                }
                else {


                    judge = "fail";

                }


            }
            else {

                judge = "fail";
               



            }



            return judge;

        }



        string noise_bluethooth_readpower(string a, string b, out string c, string d = "")
        {
            c = "fail";
            string judge = "fail";
            bool fg=true;
            int count = 0;
            bool isfreqdev = true;
            cmw100_SpectrumAnalyzersNonSCPI bluetooth;
            double btloss = double.Parse(iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["buletoothpathloss"].Trim());

         

            do
            {
                bluetooth = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);

                fg = bluetooth.init_SpectrumAnalyzers(2402000000 /* centerFreq =2402000000Hz*/,
                                                           btloss/*Eattenuation dbm*/,
                                                           2000000/*span =10000000 Hz*/,
                                                           200000/* RBW = 2000000Hz*/,
                                                           100000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                           5/*sweep time S*/);


                if (count>= 3) {

                    c = "cmw100 init error ";
                   
                   return "fail";

                }
                count++;
            } while (!fg);

            double levelvale=-200;
            double offset=1000000000;
            count = 2;
            
            do
            {




                if (count >= 3)
                {

                    if (!fg)
                    {
                        c = "read  error ";

                        return "fail";
                    }
                    else
                    {
                        c = levelvale + "";
                        return "fail";



                    }

                }

                fg = bluetooth.getmark_feq_level(50/*boardwdth*/, out levelvale, out offset,out isfreqdev, 0,1000);

             //   fg = bluetooth.getmark_feq_level(0/*boardwdth*/, out levelvale, out isoffset, 1, 1000);

                c = "" + levelvale + "dbm;" + offset +"Hz" ;




                if (levelvale < double.Parse(a.Trim()) && levelvale > double.Parse(b.Trim()) && isfreqdev==false)
                {



                    judge = "pass";

                    break;
                }
                else
                {


                    judge = "fail";

                }
                count++;

            } while (!fg || judge == "fail");


            return judge;
        }

        string noise_wifi_readpower(string a, string b, out string c, string d = "")
        {
            c = "fail";
            string judge = "fail";
            bool fg;
            bool isfreqdev = true;
            int count = 0;
            cmw100_SpectrumAnalyzersNonSCPI wifi;
            double wifiloss = double.Parse(iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["wifipathloss"].Trim());
            do
            {
                wifi = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);

                fg = wifi.init_SpectrumAnalyzers(2412000000 /* centerFreq =2402000000Hz*/,
                                                           wifiloss/*Eattenuation*/,
                                                           20000000/*span =20000000 Hz*/,
                                                           1000000/* RBW = 2000000Hz*/,
                                                           200000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                           5/*sweep time */);

                if (count >3)
                {

                    c = "cmw100 init error ";

                    return "fail";

                }
                count++;
            } while (!fg);


            double levelvale=-300;
            double offset=100000000;
            count = 3;

            do
            {




                if (count > 3)
                {
                    if (!fg)
                    {
                        c = "read error ";

                        return "fail";
                    }
                    else {
                       
                        return "fail";
                    
                    
                    
                    }

                }

                fg = wifi.getmark_feq_level(50/*boardwdth*/, out levelvale, out offset,out isfreqdev, 0,3000);

                c = "" + levelvale + "dbm;" + offset + "Hz" +";" + isfreqdev;




                if (levelvale < double.Parse(a.Trim()) && levelvale > double.Parse(b.Trim()) && isfreqdev==false)
                {



                    judge = "pass";
                    break;
                }
                else
                {


                    judge = "fail";

                }








                count++;




            } while (true);








            return judge;
        }


        string noise_cc1310_readpower(string a, string b, out string c, string d = "")
        {
            c = "fail";
            string judge = "fail";
            bool fg;
            bool isfreqdev = true;
            int count = 2;
            cmw100_SpectrumAnalyzersNonSCPI cc1310;
            double cc1310loss = double.Parse(iniread.ReadFile("setup.ini")["cmw100ParameterSet"]["cc1310loss"].Trim());
            do
            {
                cc1310 = new cmw100_SpectrumAnalyzersNonSCPI(cmw100addr);
                fg = cc1310.init_SpectrumAnalyzers(433300000 /* centerFreq =2402000000Hz*/,
                                                           cc1310loss/*Eattenuation 10 dbm*/,
                                                           1000000/*span =10000000 Hz*/,
                                                           100000/* RBW = 2000000Hz*/,
                                                           10000/* double VBW = 100000Hz*/,
                                                           1/*double sweep = auto*/,
                                                           0.05/*sweep time */);

                if (count >= 3)
                {

                    c = "cmw100 init error ";

                    return "fail";

                }
                count++;
            } while (!fg);

            double levelvale=-200;
            double offset=1000000000;
            count = 3;

            do
            {




                if (count > 3)
                {

                    if (!fg)
                    {
                        c = "read error ";
                    }
                    else {


                        c = "" + levelvale + ";" + offset + ";" + isfreqdev;

                    }

                    return "fail";

                }

                fg = cc1310.getmark_feq_level(150/*boardwdth*/, out levelvale, out offset,out isfreqdev, 0,1000);
           
                c = "" + levelvale + ";" + offset + ";"+ isfreqdev;




                if (levelvale < double.Parse(a.Trim()) && levelvale > double.Parse(b.Trim()) && isfreqdev == false)
                {



                    judge = "pass";
                    break;
                }
                else
                {


                    judge = "fail";

                }








                count++;




            } while (!fg || judge =="fail");



     




            return judge;
        }

        string cmw100_bluetooth_readpower(string a, string b, out string c, string d = "")
        {
            c = "fail";
            string judge = "fail";


            cmw100_bluetooth_tx  bluetooth_Tx = new  cmw100_bluetooth_tx(cmw100addr);


            bluetooth_Tx.DisplayView();
            bluetooth_Tx.set_input_config();
            bluetooth_Tx.driver_start();
          double pw =  bluetooth_Tx.getTxfreqResult(2);
            bluetooth_Tx.driver_stop();

           c = "" + pw + ";";







            return judge;
        }

        string testsysini(string a, string b, out string c, string d = "")
        {
            string temp = "pass";
            if (ry != null) {
                System.Threading.Thread.Sleep(200);
                relay_set("pass", "pass", out temp , "00;00");
            }
            if (ry2 != null) {

                relay2_set("pass", "pass", out temp, "00;00");
            }
            if (relayself != null) {

                myrelay_set("pass", "pass", out temp, "@00000000000000000000000000000000@");
            }

            c = "pass";
            return "pass";
        }
    
        string myrelay_set(string a, string b, out string c, string d = "@00000000000000000000000000000000@") {
            c = "pass";
            string judge = "pass";
            relayself.set_rly(d.Trim());

            return judge;
        
        }
        

        string rfid_reader_Manufacturer_ID_rwtest(string a, string b, out string c, string d = "31")
        {
            string judge = "";
            c = "";

           
            string flog=  RFID_reader.write_manfacturerid(a, (byte)int.Parse(d));
            if (flog == "fail") { RFID_reader.beep(); c = "write_error"; return "fail"; }

            string ret = RFID_reader.read_manfacturerid((byte)int.Parse(d));
            if (ret == a) { judge = "pass"; } else { judge = "fail"; }

            c = ret ;

            return judge;
        }

        string rfid_reader_Production_date_rwtest(string a, string b, out string c, string d = "34")
        {
            string judge = "";
            c = "";

           // DateTime abc = new DateTime(2014, 10, 9, 12, 30, 00);
           string flog = RFID_reader.write_date(DateTime.Now, (byte)int.Parse(d));
            if (flog == "fail") { RFID_reader.beep(); c = "write_error"; return "fail"; }
            DateTime ret = RFID_reader.read_date((byte)int.Parse(d));

            if (ret.Year == DateTime.Now.Year) {  judge = "pass"; } else { judge = "fail"; }
            c = ret.ToString("yyyy-MM-dd HH: mm");

            return judge;
        }

        string rfid_reader_pcba_software_rwtest(string a, string b, out string c, string d = "18")
        {
            string judge = "";
            c = "";

           // RFID_reader.write_rfid_block(2, BitConverter.GetBytes(705807), out c);
            UInt32 flog = RFID_reader.read_softnum((byte)int.Parse(d));
            if (flog == 0) { RFID_reader.beep(); c = "write_error"; return "fail"; } else {
                c = "" + flog;
                if (flog == int.Parse(a))
                {

               

                    return "pass"; ;
                }
                else {
                   
                    return "fail";


                }
            
            }
          
        }

        string rfid_reader_pcba_software_DrawingIndicex_rtest(string a, string b, out string c, string d = "19")
        {
            string judge = "";
            c = "";
          // RFID_reader.write_rfid_block(3, new byte[] {0x2,0,0,0},out c);
           byte[] flog = RFID_reader.read_softdrawindex((byte)int.Parse(d));
            if (BitConverter.ToUInt32(flog,0)==0) { RFID_reader.beep(); c = "write_error"; return "fail"; }
            // UInt32 rtt = BitConverter.ToUInt32(flog, 0);

            char[] m = new char[1]; 
             m[0] = (char)(flog[0] + 64);
            c = new string(m);
           
            if (new string(m) == a) {  return "pass"; }
            judge = "fail";

            return judge;
        }

        string rfid_reader_pcba_electronic_rwtest(string a, string b, out string c, string d = "24")
        {
            string judge = "";
            c = "";
            if ("fail" == RFID_reader.write_schematic((UInt32)int.Parse(a),(byte)int.Parse(d))) { RFID_reader.beep(); c = "write_error"; return "fail"; }


           UInt32 uele =  RFID_reader.read_electronic((byte)int.Parse(d));
            c = uele + "";
            if (uele == int.Parse(a)) {  return "pass"; }
            judge = "fail";
            return judge;
        }

        string rfid_reader_pcba_barePCB_rwtest(string a, string b, out string c, string d = "25")
        {
            string judge = "";
            c = "";
            if ("fail" == RFID_reader.write_bare_pcb((UInt32)int.Parse(a), (byte)int.Parse(d))) { RFID_reader.beep(); c = "write_error"; return "fail"; }


            UInt32 ubare = RFID_reader.read_bare_pcb((byte)int.Parse(d));
            c = ubare + "";
            if (ubare == int.Parse(a)) { return "pass"; }
            judge = "fail";
            return judge;
        }

        string rfid_reader_pcba_assembledPCB_rwtest(string a, string b, out string c, string d = "26")
        {
            string judge = "";
            c = "";
            if ("fail" == RFID_reader.write_assembled_pcb((UInt32)int.Parse(a), (byte)int.Parse(d))) { RFID_reader.beep(); c = "write_error"; return "fail"; }


            UInt32 uass = RFID_reader.read_assembled_pcb((byte)int.Parse(d));
            c = uass + "";
            if (uass == int.Parse(a)) { return "pass"; }
            judge = "fail";
            return judge;
        }


        string rfid_reader_pcba_schematic_rwtest(string a, string b, out string c, string d = "27")
        {
            string judge = "";
            c = "";
            if ("fail" == RFID_reader.write_schematic((UInt32)int.Parse(a), (byte)int.Parse(d))) { RFID_reader.beep(); c = "write_error"; return "fail"; }


            UInt32 usch = RFID_reader.read_schematic((byte)int.Parse(d));
            c = usch + "";
            if (usch == int.Parse(a)) { return "pass"; }
            judge = "fail";
            return judge;
        }


        string rfid_reader_pcba_DrawingIndices_rwtest(string a, string b, out string c, string d = "29")
        {
            string judge = "";
            c = "";
            char [] toa = a.ToUpper().ToArray();

            byte  ele = (byte)(toa[0]- 64);
            byte bare = (byte)(toa[1] - 64);
            byte assm = (byte)(toa[2] - 64);
            byte schem = (byte)(toa[3] - 64);

            byte[] m = new byte[] { ele, bare, assm, schem };

            if ("fail" == RFID_reader.write_indices(m,(byte)int.Parse(d))) { RFID_reader.beep(); c = "write_error"; return "fail"; }


            byte[] usch = RFID_reader.read_indices((byte)int.Parse(d));
            char[] k = new char[4];
            k[0] = (char)(usch[0] + 64);
            k[1] = (char)(usch[1] + 64);
            k[2] = (char)(usch[2] + 64);
            k[3] = (char)(usch[3] + 64);

            if (BitConverter.ToUInt32(usch,0) == BitConverter.ToUInt32(m,0)) { c = new string(k);  return "pass"; }
            c = "fail";
            judge = "fail";
            return judge;
        }



        string gpd3303_setvoltage(string a, string b, out string c, string d="1;0.500") {
            string judge = "";
            c = "";

            string[] temp = d.Split(";".ToArray());
            if (temp.Count() == 2)
            {
                try
                {
                    gpd3033.setvolatage(temp[0], temp[1]);
                    judge = "pass";
                    gpd3033.WriteLine("VSET" + temp[0] + "?");
                    c = gpd3033.ReadLine().Trim();
                }
                catch (Exception) {

                    judge = "fail";
                    c = "eque_error";
                }
            }
            else {

                judge = "fail";
                c = "fail";
            }
            
          

            return judge;

        }

        string gpd3303_setcurrt(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";

            string[] temp = d.Split(";".ToArray());
            if (temp.Count() == 2)
            {
                try
                {
                    gpd3033.setvolatage(temp[0], temp[1]);
                    judge = "pass";
                    gpd3033.WriteLine("ISET" + temp[0] + "?");
                    c = gpd3033.ReadLine().Trim();
                }
                catch (Exception)
                {

                    judge = "fail";
                    c = "eque_error";
                }
            }
            else
            {

                judge = "fail";
                c = "fail";
            }



            return judge;
        }

        string gpd3303_readvoltage(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            int count = 0;
            string[] temp = d.Split(";".ToArray());
            if (temp.Count() == 2)
            {

                do
                {

                    try
                    {
                        judge = gpd3033.getvolatage(d);
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(judge);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }
                        float ret = float.Parse(reg[0].ToString());
                        if (float.Parse(a) > ret && float.Parse(b) < ret) { judge = "pass"; } else { judge = "fail"; }

                        c = ret + "";
                    }
                    catch (Exception)
                    {

                        judge = "fail";
                        c = "eque_error";
                    }
                    if (count > 3) break;
                    count++;
                } while (judge == "fail");
               
            }
            else
            {

                judge = "fail";
                c = "fail";
            }



            return judge;

        }

        string gpd3303_readcurrt(string a, string b, out string c, string d="1")
        {
            string judge = "";
            c = "";
            int count = 0;
            string[] temp = d.Split(";".ToArray());
            if (temp.Count() == 2)
            {

                do
                {

                    try
                    {
                        judge = gpd3033.getcurrent(d);
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(judge);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }
                        float ret = float.Parse(reg[0].ToString());
                        if (float.Parse(a) > ret && float.Parse(b) < ret) { judge = "pass"; } else { judge = "fail"; }

                        c = ret + "";
                    }
                    catch (Exception)
                    {

                        judge = "fail";
                        c = "eque_error";
                    }
                    if (count > 3) break;
                    count++;
                } while (judge == "fail");

            }
            else
            {

                judge = "fail";
                c = "fail";
            }



            return judge;


        }

        string gpd3303_off(string a, string b, out string c, string d)
        {
           gpd3033.OUTPUT();
            string judge = "pass";
            c = "pass";


            return judge;

        }

        string gpd3303_on(string a, string b, out string c, string d)
        {
            gpd3033.NOOUTPUT();
            string judge = "pass";
            c = "pass";


            return judge; 

        }

        string vc8145cmeter_read_dcv(string a, string b, out string c, string d) {
            c = "";
            string judge = "";
            int count = 0;
            string[] p = d.Split(";".ToArray());
            float z;
            do
            {
                z= vc8145.read_dcv(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                  
                }
                else
                {

                    judge = "fail";
                   
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return  judge;
        }
        string vc8145cmeter_read_dci(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
                z = vc8145.read_dci(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                  
                }
                else
                {

                    judge = "fail";
                   
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_acv(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
                z = vc8145.read_acv(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                    
                }
                else
                {

                    judge = "fail";
                    
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_aci(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
                z = vc8145.read_aci(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
              
                }
                else
                {

                    judge = "fail";
                  
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_cap(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
               z = vc8145.read_cap(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                    
                }
                else
                {

                    judge = "fail";
                    
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_freq(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z; ;
            string[] p = d.Split(";".ToArray());
            do
            {
             z = vc8145.read_freq(p[0], int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                   
                }
                else
                {

                    judge = "fail";
                   
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_diode(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
                 z = vc8145.read_diode(int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                   
                }
                else
                {

                    judge = "fail";
                   
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }
        string vc8145cmeter_read_ohm(string a, string b, out string c, string d)
        {
            c = "";
            string judge = "";
            int count = 0;
            float z;
            string[] p = d.Split(";".ToArray());
            do
            {
                z = vc8145.read_ohm(p[0],int.Parse(p[1]));
                if (float.Parse(a) > z && float.Parse(b) < z)
                {
                    judge = "pass";
                    
                }
                else
                {

                    judge = "fail";
                  ;
                }

                if (count > 3) break;
                count++;
            } while (judge == "fail");
            c = z + "";
            return judge;
        }


       







        string pipgetmac(string a, string b, out string c, string d) {

          

            //if (System.IO.File.Exists("MACAndSN.CSV")) {
            //    System.IO.File.Delete("MACAndSN.CSV");
            
            
            //}
            string judge = "";
            this.macflog = "";
            
            string m = new piprun(d, "").getruninfo();
       
            if (m.IndexOf(a.Trim()) >= 0)
            {
               MatchCollection reg = new Regex(@"[0-9a-fA-F]{2}([/\s:-][0-9a-fA-F]{2}){5}").Matches(m);
                if (reg.Count > 0)
                {
                    this.macflog = reg[0].Value.ToUpper();
                    // MessageBox.Show(reg[0].Value.ToUpper());
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter("MACAndSN.CSV", false))
                    //{
                    //    file.WriteLine(reg[0].Value.ToUpper() + "," + this.trf);

                    //}
                    judge = "pass";
                }
                else {
                    judge = "fail";
                }
                
            }
            else
            {
                judge = "fail";
            };


            if (this.macflog.Length > 5) { c = this.macflog; }
            else
            {

                c = "mac len size fail";
            }

          
            return judge;





        }

        string pipgetmacclosecomport(string a, string b, out string c, string d)
        {
            if (commandline.IsOpen == true)
            {

                commandline.Close();
                System.Threading.Thread.Sleep(100);

            }
           
            //if (System.IO.File.Exists("MACprintSN.txt"))
            //{
            //    System.IO.File.Delete("MACprintSN.txt");


            //}
            string judge = "";
            this.macflog = "";
            string m = new piprun(d, iniread.ReadFile("setup.ini")["setport"]["comline"]).getruninfo();

            if (m.IndexOf(a.Trim()) >= 0)
            {
                MatchCollection reg = new Regex(@"[0-9a-fA-F]{2}([/\s:-][0-9a-fA-F]{2}){5}").Matches(m);
                if (reg.Count > 0)
                {
                    this.macflog = reg[0].Value.ToUpper();
                    // MessageBox.Show(reg[0].Value.ToUpper());
                    //using (System.IO.StreamWriter file = new System.IO.StreamWriter("MACprintSN.txt", false))
                    //{
                    //    file.WriteLine(reg[0].Value.ToUpper() + "," + this.trf);

                    //}
                    judge = "pass";
                }
                else
                {
                    judge = "fail";
                }

            }
            else
            {
                judge = "fail";
            };


            if (this.macflog.Length > 5) { c = this.macflog; }
            else
            {

                c = "fail";
            }

            commandline.Open();
            return judge;





        }

        string noise_PipRunning_fwupdate(string a, string b, out string c, string d)
        {

            if (commandline.IsOpen == true)
            {

                commandline.Close();
                System.Threading.Thread.Sleep(100);

            }

            string judge = "";
            string param = iniread.ReadFile("setup.ini")["setport"]["comline"] != null ? iniread.ReadFile("setup.ini")["setport"]["comline"] : "";
            string m = new piprun(d, param).getruninfo();

            if (m.IndexOf(a.Trim()) >= 0)
            {

                MatchCollection reg = new Regex(a.Trim()).Matches(m);
                if (reg.Count > 0)
                {
                    c = reg[0].Value.Trim();

                    judge = "pass";
                }
                else
                {

                    judge = "fail";

                    c = "NotFind";

                }


            }
            else
            {
                judge = "fail";
                c = "NotFind";
            };


            return judge;


        }



        string pyrunner(string a, string b, out string cp, string d)
        {
            string judge = "";
            cp = "";
            pr = new piprun("python.exe", d);
            string m = pr.getruninfo();
           
            if (m.IndexOf("pass") >= 0) { judge = "pass"; cp = "pass"; }
            else if (m.IndexOf("fail") >= 0) {

                cp = "fail";
                judge = "fail";
            }
            else {

                judge = "fail";
                cp = "error";
                MessageBox.Show(m);
            }

            return judge;

        }

            string md3058_read_capactance(string a, string b, out string c, string d) {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.read_capactance();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-1.000;



                }

                if (rt * 1000000000 > float.Parse(b) && rt * 1000000000 < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt * 1000000000 + "";
            return judge;
        
        }

        string md3058_read_resistance(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.read_resistance();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-1.000;



                }

                if (rt > float.Parse(b) && rt  < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt  + "";
            return judge;

        }

        string md3058_read_resistance_range(string a, string b, out string c, string d="6")
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.read_resistance(int.Parse(d.Trim()));
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-1.000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }

        string md3058_read_DC_20V(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_20v() ;
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt  > float.Parse(b) && rt  < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt  + "";
            return judge;

        }
        string md3058_read_DC_200V(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_200v();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }
        string md3058_read_DC_2mA(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_2mA();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }
        string md3058_read_DC_20mA(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_20mA();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }
        string md3058_read_DC_200mA(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_200mA();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }
        string md3058_read_DC_10A(string a, string b, out string c, string d)
        {

            c = "";
            string ret;
            string judge = "";
            float rt;
            int count = 0;
            do
            {

                try
                {
                    ret = dm3058.dm3058_dc_read_10A();
                    rt = float.Parse(ret.Trim());



                }
                catch (Exception)
                {


                    rt = (float)-10000;



                }

                if (rt > float.Parse(b) && rt < float.Parse(a))
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

                if (count > 3) break;
                count++;

            } while (judge == "fail");


            c = rt + "";
            return judge;

        }


        string read_info_window(string a, string b, out string c, string d)
        {

            string[] prom = d.Split(";".ToArray());
            do
            {

                System.Threading.Thread.Sleep(100);
                temp = Interaction.InputBox(prom[0], prom[1],"", -1, -1);

                if (temp.Length == int.Parse(a))
                {

                    break;
                }
                else {

                    prom[0] = "input error , please reinput";
                }
            } while (true);
           
         



            c = "pass";
            return "pass";
        }

        string read_wifi_rssi(string a, string b, out string c, string d) {

                string judge = "";
                int count = 0;
                string p = "";
                string rsu = "";
                do
                {
                    p = "";
                    piprun read_exe = new piprun(@"wifi_mod/wifimac.exe", d);

                     rsu = read_exe.getruninfo();
              
                    if (rsu.IndexOf(d.Split(";".ToArray())[0]) > 0)
                    {

                        if (this.temp == rsu.Split(",".ToArray())[0]) {
                            judge = "pass";
                        }
                        else
                        {

                            p = "MAC address not matching";
                            judge = "fail";
                        }

                  

                    }
                    else
                    {


                        p = "wifi name is not matching";
                        judge = "fail";

                    }
                System.Threading.Thread.Sleep(500);
                    if (count++ > 5) break;
                } while (judge == "fail");

                c = p + ((rsu.IndexOf(",")>0)?rsu.Split(",".ToArray())[0] + "|" + rsu.Split(",".ToArray())[2]:"");

         

                return judge;
            }
        string LED_Read_PACK(string a, string b, out string c, string d)
            {


                string judge = "";
                string[] LED_UP = a.Split(";".ToArray());
                string[] LED_LOW = b.Split(";".ToArray());
                string[] led_ser = d.Split(";".ToArray());
                int[] jud = new int[LED_UP.Length];
                string view = "";
                for (int loop = 0; loop < LED_UP.Length; loop++)
                {
                    string[] rgbvup = LED_UP[loop].Split(",".ToArray());
                    string[] rgbvlow = LED_LOW[loop].Split(",".ToArray());
                    int[][] ret = led_sensor.getRGB_peak_0000to2000(1000);
                    int rvup = int.Parse(rgbvup[0]);
                    int gvup = int.Parse(rgbvup[1]);
                    int bvup = int.Parse(rgbvup[2]);

                    int rvlow = int.Parse(rgbvlow[0]);
                    int gvlow = int.Parse(rgbvlow[1]);
                    int bvlow = int.Parse(rgbvlow[2]);


                    string temp = (view.Length < 1) ? "" : ";";
                    view = view + temp + ret[int.Parse(led_ser[loop])][0] + "," + ret[int.Parse(led_ser[loop])][1] + "," + ret[int.Parse(led_ser[loop])][2];
                    if ((ret[int.Parse(led_ser[loop])][0] > rvlow && ret[int.Parse(led_ser[loop])][0] < rvup) &&
                        (ret[int.Parse(led_ser[loop])][1] > gvlow && ret[int.Parse(led_ser[loop])][1] < gvup) &&
                        (ret[int.Parse(led_ser[loop])][2] > bvlow && ret[int.Parse(led_ser[loop])][2] < bvup))
                    {

                        jud[loop] = 1;

                    }
                    else
                    {

                        jud[loop] = 0;
                    }


                }

                if (jud.Sum() == LED_UP.Length) { judge = "pass"; } else { judge = "fail"; }




                c = view;
                return judge;
            }

        string LED_Read(string a, string b, out string c, string d)
            {

      
                string judge = "";
                string[] LED_UP = a.Split(";".ToArray());
                string[] LED_LOW = b.Split(";".ToArray());
                string[] led_ser = d.Split(";".ToArray());
                int[] jud = new int[LED_UP.Length]; 
                string view = "";
                 for(int loop = 0; loop < LED_UP.Length; loop++) {  
                    string[] rgbvup = LED_UP[loop].Split(",".ToArray());
                    string[] rgbvlow = LED_LOW[loop].Split(",".ToArray());
                    int[][] ret = led_sensor.getRGB();
                    int rvup = int.Parse(rgbvup[0]);
                    int gvup = int.Parse(rgbvup[1]);
                    int bvup = int.Parse(rgbvup[2]);

                    int rvlow = int.Parse(rgbvlow[0]);
                    int gvlow = int.Parse(rgbvlow[1]);
                    int bvlow = int.Parse(rgbvlow[2]);

               //     MessageBox.Show("" + rvlow + "--" + gvlow + "--" + bvlow);
                 //   MessageBox.Show("" + rvup + "--" + gvup + "--" + bvup);

                //    MessageBox.Show("" + ret[int.Parse(led_ser[loop])][0] + "--" + ret[int.Parse(led_ser[loop])][1] + "--" + ret[int.Parse(led_ser[loop])][2]);


                    string temp = (view.Length < 1) ? "" : ";";
                     view  = view  + temp  +  ret[int.Parse(led_ser[loop])][0] + "," + ret[int.Parse(led_ser[loop])][1] + "," + ret[int.Parse(led_ser[loop])][2];
                    if ((ret[int.Parse(led_ser[loop])][0] > rvlow && ret[int.Parse(led_ser[loop])][0] < rvup) &&
                        (ret[int.Parse(led_ser[loop])][1] > gvlow && ret[int.Parse(led_ser[loop])][1] < gvup) &&
                        (ret[int.Parse(led_ser[loop])][2] > bvlow && ret[int.Parse(led_ser[loop])][2] < bvup))
                    {

                        jud[loop] = 1;

                    }
                    else {

                        jud[loop] = 0;
                    }


                }

                if (jud.Sum() == LED_UP.Length) { judge = "pass"; } else {  judge="fail";}




                c = view;
                return judge;
            }

        string commline_pass_fail(string a, string b, out string c, string d) {
                string judge = "";
                c = "";

            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);
               string ret= commandline.command_pass_fail(d, a);
                if (ret == "pass") {
                   c= judge = "pass";
                }
                else {

                  c=  judge = "fail";
                }
                return judge;
            }

        string commline_send_noreturn(string a, string b, out string c, string d)
        {
            string judge = "pass";
            c = "pass";
            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);
            commandline.WriteLine(d); ;
             
            return judge;
        }
        string commline_send_signeCR(string a, string b, out string c, string d)
        {
            string judge = "pass";
            c = "pass";
            string m = "";
            int count = 0;
            if (commandline.IsOpen == false) commandline.Open();
            
            do
            {
                System.Threading.Thread.Sleep(50);
                commandline.Write(d.Trim() + "\r");
                //  commandline.WriteLine("bt_tx_tone 1 0 0");
                System.Threading.Thread.Sleep(50);
                m = commandline.ReadExisting();
                if (count > 3) { judge = "fail"; break; }

            } while (m == null);

            return judge;
        }

        string commline_closeport(string a, string b, out string c, string d)
        {
            string judge = "pass";
            c = "pass";
            if (commandline.IsOpen == true)
            {

                commandline.Close();
            }
         



            System.Threading.Thread.Sleep(30);
          //  commandline.Open();

            return judge;
        }

        string commline_write_NonEnter(string a, string b, out string c, string d)
        {
            string judge = "pass";
            c = "pass";
            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);
            if (d == null) d = "AT+SCAN?";
            commandline.Write(d);



            System.Threading.Thread.Sleep(30);
            //  commandline.Open();

            return judge;
        }

        string commline_read_delay_reg(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);
            if (d == null) d = "5000;" + "noise\\:[0-9]+;[0-9]+";
            string[] psr = d.Split(";".ToArray());

            string m = commandline.read_value_fromreg(psr[1], int.Parse(psr[0]));
            if (m == "null")
            {
                c = "null";
                return "fail";

            }
            MatchCollection reg2 = new Regex(psr[2]).Matches(m);
            float value1 = float.Parse(reg2[0].Value);

            c = value1 + "";
            if (value1 < float.Parse(a) && value1 > float.Parse(b))
            {


                judge = "pass";
            }
            else
            {

                judge = "fail";
            }




            return judge;
        }

        string commline_openport(string a, string b, out string c, string d)
        {
            string judge = "pass";
            c = "pass";
            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);

            MessageBox.Show(commandline.PortName);

            System.Threading.Thread.Sleep(30);
            //  commandline.Open();

            return judge;
        }

        string commline_readval(string a, string b, out string c, string d)
        {
            string judge = "";
            c="";
            if (commandline.IsOpen == false) commandline.Open();
            System.Threading.Thread.Sleep(50);
            string[] uplimt = a.Split(";".ToArray());
            string[] lowlimt = b.Split(";".ToArray());
            string[] m = d.Split(";".ToArray());
            float [] ret = commandline.read_value_2float(m[0],m[1],1000);

            if (ret[0] > float.Parse(lowlimt[0]) && ret[0] < float.Parse(uplimt[0]) &&
               ret[1] > float.Parse(lowlimt[1]) && ret[1] < float.Parse(uplimt[1]))
            {

                judge = "pass";

            } else {

                judge = "fail";

            }
            c = ret[0] + ";" + ret[1];
            return judge;

        }


        string noiseware_readmic(string a, string b, out string c, string d)
        {
            string judge = "fail";
            c = "fail";
            if (d == null) d = "5000;" + "rms\\:[0-9]+";
            string [] psr = d.Split(";".ToArray());
            int count = 0;
            MessageBox.Show("Test");
            do
            {
                string m = commandline.read_value_fromreg(psr[1], int.Parse(psr[0]));
                if (m == "null")
                {
                    c = "null";
                    return "fail";

                }
                MatchCollection reg2 = new Regex(@"[0-9]+").Matches(m);
                float micvalue = float.Parse(reg2[0].Value);

                c = micvalue + "";
                if (micvalue < float.Parse(a) && micvalue > float.Parse(b))
                {


                    judge = "pass";
                    break;
                }
                else
                {

                    judge = "fail";
                }
                if (count > 3) break;
                count++;

            } while (judge == "fail");



            return judge;

        }

        string noiseware_readAccelerometer(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            if (d == null) d = "3000;ax=\\s+[+-][0-9]*\\.[0-9]+\\s+ay=\\s+[+-][0-9]*\\.[0-9]+\\s+az=\\s+[+-][0-9]*\\.[0-9]+";
            string[] psr = d.Split(";".ToArray());

            string m = commandline.read_value_fromreg(psr[1], int.Parse(psr[0]));

           

            if (m!="null")
            {

                // MatchCollection reg2 = new Regex(@"[+-][0-9]*\\.[0-9]+").Matches(m);
                //reg2[0].Value;reg2[1].Value;reg2[2].Value



                

                judge = "pass";
            }
            else
            {
              
                judge = "fail";
            }



            c = m;
            return judge;

        }


        string noiseware_esp32tool_wifisign_gen(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            string ret = "";
            int count = 0;
            do
            {
                try
                {
                    if (d == null) d = "tx_cbw40m_en 0;wifiscwout 1 1 0";
                    string[] psr = d.Split(";".ToArray());
                    foreach (string tmp in psr)
                    {

                        commandline.WriteLine(tmp);
                        System.Threading.Thread.Sleep(300);


                    }
                    ret = commandline.ReadExisting();
                    if (ret.Length > 0) { c = "pass"; judge = "pass"; } else {

                        c = "fail"; judge = "fail";

                    }
                }
                catch
                {

                    c = "comm error"; judge = "fail";
                }

                if (count > 3) break;
                count++;
            } while (judge=="fail");


            return judge;
        

        }




        public string noiseware_writecommand(string a, string b, out string c, string d="button")
        {

            if (commandline.IsOpen == false) {
                commandline.Open();
            }
            string judge = "pass";
            c = "pass";
            System.Threading.Thread.Sleep(500);
            string m = commandline.ReadExisting();
            commandline.WriteLine(d.Trim());
            System.Threading.Thread.Sleep(500);

         
            return "pass";

        }

        /* outdoor 指令集功能*/
        public string noiseware_outdoor_getfwver(string a, string b, out string c, string d )
        {
            if (d == null) d = "2000";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }

            string judge = "fail";
            c = "fail";
            Regex rex = new Regex("FW_VERSION:(\\d.\\d.\\d)\\s+(\\d{2})", RegexOptions.IgnoreCase);
            MatchCollection matchs =null;
           for (int i = 0; i < 3; i++)
            {

               System.Threading.Thread.Sleep(500);
              string m = commandline.ReadExisting();
               commandline.WriteLine("e");
               System.Threading.Thread.Sleep(int.Parse(d.Trim()));
               m = commandline.ReadExisting();
                matchs = rex.Matches(m);
                if (matchs.Count <= 0)
                {
                    System.Threading.Thread.Sleep(300);
                    commandline.WriteLine("r");
                    continue;
                }

                if (matchs.Count > 0) {


                   c= matchs[0].Groups[1].Value  +  matchs[0].Groups[2].Value;
                    break;
                   
                }
              
               
            }

            if (c == a.Trim())
            {

                judge = "pass";

            }
            else {

                judge = "fail";
            
            }


            return judge;

        }

        public string noiseware_outdoor_getNVflashstatus(string a, string b, out string c, string d)
        {
            if (d == null) d = "2000";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }

            string judge = "fail";
            c = "fail";
            Regex rex = new Regex("External\\s+Flash\\s+status:\\s+(\\d+)", RegexOptions.IgnoreCase);
            MatchCollection matchs = null;
            for (int i = 0; i < 3; i++)
             {

              System.Threading.Thread.Sleep(500);
             string m = commandline.ReadExisting();
              commandline.WriteLine("5");
            System.Threading.Thread.Sleep(int.Parse(d.Trim()));
               m = commandline.ReadExisting();
            matchs = rex.Matches(m);
             if (matchs.Count <= 0) continue;

            if (matchs.Count > 0)
            {


                c = matchs[0].Groups[1].Value;

            }
            }

            if (c == a.Trim())
            {

                judge = "pass";

            }
            else
            {

                judge = "fail";

            }


            return judge;

        }

        public string noiseware_outdoor_MicTest(string a, string b, out string c, string d)
        {
            if (d == null) d = "1000";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }

            string judge = "fail";
            c = "fail";
            Regex rex = new Regex("Microphone\\s+value:\\s+(\\d+)\\s+Noise\\s(\\d+)", RegexOptions.IgnoreCase);
            MatchCollection matchs = null;
            for (int i = 0; i < 3; i++)
            {

                System.Threading.Thread.Sleep(500);
                string m = commandline.ReadExisting();
                commandline.WriteLine("6"); //start MIC test
                System.Threading.Thread.Sleep(int.Parse(d.Trim()));
                m = commandline.ReadExisting();
                matchs = rex.Matches(m);
                if (matchs.Count <= 0) continue;
                int Micvalue = 0;
                int Noisevalue = 0;
                for (int cz = 0; i < matchs.Count; cz++)
                {
                    if (int.Parse(matchs[cz].Groups[1].Value) > Micvalue) Micvalue = int.Parse(matchs[cz].Groups[1].Value);
                    if (int.Parse(matchs[cz].Groups[2].Value) > Noisevalue) Noisevalue = int.Parse(matchs[cz].Groups[2].Value);

                }
                c = Micvalue +";" + Noisevalue;

                commandline.WriteLine("7"); //stop MIC test
                System.Threading.Thread.Sleep(500);
                if (int.Parse(a.Trim()) < Micvalue && int.Parse(b.Trim()) < Micvalue)
                {

                    judge = "pass";

                }
                else
                {

                    judge = "fail";

                }

              
            }
            return judge;
        }
        public string noiseware_outdoor_intodebug(string a, string b, out string c, string d)
        {
            if (d == null) d = "1000";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }
            string judge = "fail";
            c = "fail";
         
            for(int i = 0; i < 3; i++) { 
          
            System.Threading.Thread.Sleep(500);
            string m = commandline.ReadExisting();
            commandline.WriteLine("e");
            System.Threading.Thread.Sleep(int.Parse(d.Trim()));
            int flog = commandline.readstringforone("debug enabled");

                if (flog > 0) {

                    judge = "pass";

                    break;
                }
                
            }

            return judge;

        }

        public string noiseware_outdoor_startsubgiga(string a, string b, out string c, string d)
        {
            if (d == null) d = "1000;433.3;1";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }
            string judge = "fail";
            c = "fail";
            string[] comm = d.Split(";".ToCharArray());

            for (int i = 0; i < 3; i++)
            {

                System.Threading.Thread.Sleep(500);
                string m = commandline.ReadExisting();
                commandline.WriteLine(comm[2]);
                System.Threading.Thread.Sleep(int.Parse(comm[0]));
                int flog = commandline.readstringforone(comm[1]);

                if (flog > 0)
                {

                    judge = "pass";

                    break;
                }

            }

            return judge;

        }


        public string noiseware_outdoor_stopsubgiga(string a, string b, out string c, string d)
        {
            if (d == null) d = "1000;";
            if (commandline.IsOpen == false)
            {
                commandline.Open();
            }
            string judge = "fail";
            c = "fail";
   

            for (int i = 0; i < 3; i++)
            {

                System.Threading.Thread.Sleep(500);
                string m = commandline.ReadExisting();
                commandline.WriteLine("4");
                System.Threading.Thread.Sleep(int.Parse(d.Trim()));
                int flog = commandline.readstringforone("");

                //if (flog > 0)
                //{

                //    judge = "pass";

                //    break;
                //}
                break;

            }
            judge = "pass";
            c = "pass";
            return judge;

        }

        public string noiseware_intouartAndgetMAC(string a, string b, out string c, string d = "activate_uart")
        {
            string judge = "fail";
            c = "fail";
            if (d == null) d = "activate_uart";
            System.Threading.Thread.Sleep(500);

            commandline.WriteLine(d);
            string readmac = "";
            string tmp =  commandline.read_value_fromregNonclearbuffer(out readmac,@"sos\/[0-9a-fA-F]{2}([/\s:-][0-9a-fA-F]{2}){5}");

            if (tmp != "null") {

                judge = "pass";
                c = readmac;
                this.macflog = readmac;

              //  using (System.IO.StreamWriter file = new System.IO.StreamWriter("recordmacSN.csv", false))
              //  {
              //      file.WriteLine( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "," + readmac + "," + this.trf==""?"null": this.trf);

             //   }



            }

       

            return judge;

        }

        public string noiseware_save_sn_MAC(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
           
            try
            {
                if (this.macflog.Length > 5)
                {
                    string savepath = iniread.ReadFile("setup.ini")["setproduct"]["macsavepath"].Trim();
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(savepath +  "recordmacSN.csv", true))
                    {

                        int v = this.macflog.Length / 2;
                        string m = this.macflog;
                        //for (int i = 1; i < v; i++)
                        //{
                        //    m = m.Insert(2 + 3 * (i - 1), ":");
                        //}

                        string sn;
                        if (this.trf == null)
                        {
                            sn = "null";
                        }
                        else {

                            sn = this.trf;
                        }
                      
                      file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "," + m + "," + sn);
                        
                      
                        
                    }

                    c = "pass";
                    judge = "pass";

                }
                else {
                    c = "mac length error";
                    judge = "fail";
                }
                

            }
            catch (Exception e){


                c = "file save error";

            }
            this.macflog = "";
            return judge;
        }


            public string noiseware_getkeypressstatus(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            if (d == null) d = "PUSHED;3000";
            
            string[] psr = d.Trim().Split(";".ToArray());
             int m  = commandline.readstringuntil(psr[0],int.Parse(psr[1]));
           
            if (m ==1)
            {

                c = "found " + a;
                judge = "pass";
            }
            else
            {
                c = " unfound " + a;
                judge = "fail";
            }



            return judge;

        }

        public string noiseware_getNonpressstatus(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            if (d == null) d = "PUSHED;3000";

            string[] psr = d.Trim().Split(";".ToArray());
            int m = commandline.readstringuntil(psr[0], int.Parse(psr[1]));

            if (m == 0 || m==-1)
            {

                c = "unfound " + a;
                judge = "pass";
            }
            else
            {
                c = " found " + a;
                judge = "fail";
            }



            return judge;

        }

        public string noiseware_getAccelerometerstatus_dd(string a, string b, out string c, string d)/*控制抖動繼電器*/
        {
            string judge = "";
            c = "";
            int flog=-1;
            if (d == null) d = "detected;cleared;5000";
            
            string[] psr = d.Trim().Split(";".ToArray());

            for (int i = 0; i < (int)(int.Parse(psr[2]) / 1000); i++)
            {
                commandline.RtsEnable = true;
                System.Threading.Thread.Sleep(100);
                commandline.RtsEnable = false;
                System.Threading.Thread.Sleep(100);
                commandline.RtsEnable = true;
                System.Threading.Thread.Sleep(100);
                commandline.RtsEnable = false;
                System.Threading.Thread.Sleep(100);
                flog = commandline.readstringuntil(psr[0]);
                if (flog == 1) break;

            }

           
            if (flog == 1/* && m2==1*/)
            {

                c = "found " + a;
                judge = "pass";
            }
            else
            {
                c = " unfound " + a;
                judge = "fail";
            }



            return judge;

        }


        public string noiseware_getAccelerometerstatus(string a, string b, out string c, string d)
        {
            string judge = "";
            c = "";
            if (d == null) d = "detected;cleared;3000";

            string[] psr = d.Trim().Split(";".ToArray());
            int m = commandline.readstringuntil(psr[0], int.Parse(psr[2]));

            if (m == 1)
            {

                c = "found " + a;
                judge = "pass";
            }
            else
            {
                c = " unfound " + a;
                judge = "fail";
            }



            return judge;

        }


        public  string noiseaware_singlecommand(string a, string b, out string c, string d) {


            c = d.Trim()+ " have been executed";
            string temp;
            int count = 0,lenstr=0;

            do
            {

                if (count > 3) { c = "comm timeout";return "fail"; }
                try
                {
                    commandline.DiscardInBuffer();
                    commandline.ReadExisting();
                    commandline.WriteLine(d.Trim());
                    System.Threading.Thread.Sleep(100);
                    temp = commandline.ReadExisting();

                    lenstr = temp.Length;

                }
                catch { 
                
                
                
                }

                count++;

            } while (lenstr==0);



            return "pass";

        }







        string TRM1201_read(string a, string b, out string c, string d)
        {
            string judge = "";
            float m = TRM1201reader.readres();
            c = m + "";
            if (float.Parse(a) >= m && float.Parse(b) <= m)
            {

                judge = "pass";
            }
            else
            {

                judge = "fail";
            }



  
            return judge;
        }

        string delay(string a, string b, out string c, string d) {
        
            System.Threading.Thread.Sleep(int.Parse(a));
            c = "pass";
            return "pass";
        }
        string PipRunning(string a, string b, out string c, string d)
        {
            try
            {
               // System.Diagnostics.Process.Start("runner.exe");
            }
            catch (Exception)
            {

            }
            string judge = "";
            string m =  new piprun(d, "").getruninfo();
            
            if (m.IndexOf(a.Trim()) >=0) {

                judge = "pass";
            } else {
                judge = "fail";
            };
            c = judge;

            try
            {

                //Process[] avalible_p = Process.GetProcessesByName("runner");
                //foreach (Process win_yg in avalible_p)
                //{
                //    if (!win_yg.CloseMainWindow())
                //    {
                //        win_yg.Kill();
                //    }
                //}

            }
            catch (Exception)
            {

            }

            return judge;


        }
        string PipRunning_regular(string a, string b, out string c, string d)
        {
          
            string judge = "";
            string m = new piprun(d, "").getruninfo();

            if (m.IndexOf(a.Trim()) >= 0)
            {

                MatchCollection reg = new Regex(a.Trim()).Matches(m);
                if (reg.Count > 0)
                {
                    c = reg[0].Value.Trim();

                    judge = "pass";
                }
                else {

                    judge = "fail";

                    c = "NotFind";

                }

                    
            }
            else
            {
                judge = "fail";
                c = "NotFind";
            };
           

            return judge;


        }
        string message_prompt(string a, string b, out string c, string d="请确认是否按照要求正常表现") {

            string judge = "";
            DialogResult result = MessageBox.Show(d, "结果确认窗体", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                c = "pass";
                judge = "pass";

            }
            else {

                c = "fail";
                judge = "fail";

            }

            return judge;
        }
        string hipottest_dc(string a, string b, out string c, string d)
        {
            string judge = "";
            chrm.starttest();
           chroma19701.test_result rs =  chrm.getResult(0);
            float z = rs.meter2;

            if (float.Parse(a) < z && z < float.Parse(b))
            {
                judge = "pass";

            }
            else {

                judge = "fail";
            }

           
            c = z + "";

            return judge;

        }

        string bt_dongle_delay_regread(string a, string b, out string c, string d)
        {
           
            c = "";
            string judge = "";
            btdogmac = "";
            if (d == null) d = "-45;3000;\\d\\:\\s+0x([0-9|a-z|A-Z]{8})[0-9|a-z|A-Z]{4}\\,\\s+([-]\\d{2,3})\\,\\s+NoiseAwareG3_([0-9|a-z|A-Z]{4})";
            string[] p = d.Split(";".ToCharArray());

            int count = 0, dutnum = 0,count2=0,tempval=-200;
            do
            {
                btdongle.DiscardInBuffer();
                btdongle.ReadExisting();
                btdongle.Write("AT+SCAN?");
                System.Threading.Thread.Sleep(int.Parse(p[1]));
                string ret = btdongle.ReadExisting();
                Debug.WriteLine(ret);
                Regex rex = new Regex(p[2], RegexOptions.IgnoreCase);
                MatchCollection matchs =  rex.Matches(ret) ;


                count2 = 0; tempval = -200;
                for (int i = 0; i < matchs.Count; i++) {

                    if (int.Parse(matchs[i].Groups[2].Value) >  int.Parse(p[0])){

                        count2++;
                        if (int.Parse(matchs[i].Groups[2].Value) > tempval){
                            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero,  matchs[i].Groups[0].Value + "-->" +  matchs[i].Groups[1].Value + matchs[i].Groups[3].Value +";" + matchs[i].Groups[2].Value);
                            tempval = int.Parse(matchs[i].Groups[2].Value);
                            dutnum = i;
                        }

                       
                    }


                }


            


                if (count > 3)
                {
                    c = "found More than 2 unit or none";
                    judge = "fail";
                   
                    break;

                }


                if (count2 == 1)
                {


                    c ="" + matchs[dutnum].Groups[1].Value + matchs[dutnum].Groups[3].Value + "-->" + matchs[dutnum].Groups[2].Value;
                    btdogmac = matchs[dutnum].Groups[1].Value + matchs[dutnum].Groups[3].Value;
                    judge = "pass";
                    break;

                }



                count++;

             



            } while (true);


          

            return judge;

        }


        public string btdongle_save_MAC(string a, string b, out string c, string d = "")
        {
            string judge = "fail";
            c = "fail";
            if (this.btdogmac == null || this.btdogmac == "") { c = "MAC is null"; return "fail"; }
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter("recordDUTmacFromBTDONGLE.csv", false))
                {


                    int v = this.btdogmac.Length / 2;
                    string m = this.btdogmac;
                    for (int i = 1; i < v; i++)
                    {
                        m = m.Insert(2 + 3 * (i - 1), ":");
                    }

                    //  file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "," + m );

                    file.WriteLine(m);
                }
                c = "pass";
                judge = "pass";

            }
            catch (Exception e)
            {


                c = "file save error";

            }

            return judge;
        }







        string TRM1201ReadRes(string a, string b, out string c, string d) {

            string jud = "";
            float  m = TRM1201reader.readres(int.Parse(d));
            c = m + "";
            if (float.Parse(a) >= m && float.Parse(b) <= m)
            {

                jud = "pass";
            }
            else {

                jud = "fail";
            }


            return jud;
        }
        string TDM9001_2A_read(string a, string b, out string c,string d)
        {
            string jud ="";
            float z;
            int count = 0;
            do
            {
                z = mincurm.read();
                if (float.Parse(a) >= z && float.Parse(b) <= z)
                {

                    jud = "pass";

                }
                else
                {

                    jud = "fail";
                }

                count++;
            } while (jud == "fail" && count > 3);
            c = z +"";
           
           return jud;          /*"fail";*/
        }
        string TMD1501_50_read(string a, string b, out string c, string d)
        {
            string jud = "";
            int count = 0;
            float z;
            do
            {
                 z = minvm.read();

                if (float.Parse(a) >= z && float.Parse(b) <= z)
                {

                    jud = "pass";

                }
                else
                {

                    jud = "fail";
                }
                count++;
            } while (jud == "fail" && count<3);

            c = z + "";

            return jud;          /*"fail";*/
        }
        string relay_set(string a, string b, out string c, string d)
        {
            string[] p = d.Split(";".ToCharArray());
            ry.set_relay(Byte.Parse(p[0],System.Globalization.NumberStyles.HexNumber),Byte.Parse(p[1], System.Globalization.NumberStyles.HexNumber));

            c = "pass";
            return "pass";
        }

        string relay2_set(string a, string b, out string c, string d)
        {
            string[] p = d.Split(";".ToCharArray());
            ry2.set_relay(Byte.Parse(p[0], System.Globalization.NumberStyles.HexNumber), Byte.Parse(p[1], System.Globalization.NumberStyles.HexNumber));

            c = "pass";
            return "pass";
        }


        string cloor_assy(string a, string b, out string c,string d)
        {

            string judge1 = "";
            int cu = 3;

            /*第一個參數是通道，第二個參數是try次數*/
            if (d == null) d = "1;3";

            string[] pt = d.Split(";".ToCharArray());
            if (pt.Length > 1)
            {
                cu = int.Parse(pt[1]);
            }
            d = pt[0];

            do
            {
                string[] lowlimit = b.Split(";".ToCharArray());
                int[] ll = new int[] { int.Parse(lowlimit[0]), int.Parse(lowlimit[1]), int.Parse(lowlimit[2]), int.Parse(lowlimit[3]) };

                string[] uplimit = a.Split(";".ToCharArray());

                int[] ul = new int[] { int.Parse(uplimit[0]), int.Parse(uplimit[1]), int.Parse(uplimit[2]), int.Parse(uplimit[3]) };

                int[] rsut = ledassyer.getRGBI(int.Parse(d));

                if (rsut[0] > ll[0] && rsut[0] < ul[0])
                {
                    if (rsut[1] > ll[1] && rsut[1] < ul[1])
                    {
                        if (rsut[2] > ll[2] && rsut[2] < ul[2])
                        {
                            if (rsut[3] > ll[3] && rsut[3] < ul[3])
                            {

                                c = "pass:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                                judge1 = "pass";

                            }
                            else
                            {

                                c = "intensity componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                                judge1 = "fail";


                            }

                        }
                        else
                        {

                            c = "blue componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                            judge1 = "fail";



                        }

                    }
                    else
                    {
                        c = "green componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; 
                        judge1 = "fail";


                    }


                }
                else
                {

                    c = "red componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                    judge1 = "fail";


                }
                cu--;
            } while (judge1 == "fail" && cu > 0);



            return judge1;

        }
        string cloor_assy_Min(string a, string b, out string c, string d)
        {

            string judge1 = "";
            int cu = 3;
            if (d == null) d = "3;3";

            string[] pt = d.Split(";".ToCharArray());
            if (pt.Length > 1)
            {
                cu = int.Parse(pt[1]);
            }
            d = pt[0];

            do
            {
                string[] lowlimit = b.Split(";".ToCharArray());
                int[] ll = new int[] { int.Parse(lowlimit[0]), int.Parse(lowlimit[1]), int.Parse(lowlimit[2]), int.Parse(lowlimit[3]) };

                string[] uplimit = a.Split(";".ToCharArray());

                int[] ul = new int[] { int.Parse(uplimit[0]), int.Parse(uplimit[1]), int.Parse(uplimit[2]), int.Parse(uplimit[3]) };

                int[] rsut = ledassyer.getRGBI_Min(int.Parse(d));

                if (rsut[0] >= ll[0] && rsut[0] < ul[0])
                {
                    if (rsut[1] >= ll[1] && rsut[1] < ul[1])
                    {
                        if (rsut[2] >= ll[2] && rsut[2] < ul[2])
                        {
                            if (rsut[3] >= ll[3] && rsut[3] < ul[3])
                            {

                                c = "pass:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                                judge1 = "pass";

                            }
                            else
                            {

                                c = "intensity componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                                judge1 = "fail";


                            }

                        }
                        else
                        {

                            c = "blue componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                            judge1 = "fail";



                        }

                    }
                    else
                    {
                        c = "green componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3];
                        judge1 = "fail";


                    }


                }
                else
                {

                    c = "red componet ng:" + rsut[0] + ":" + rsut[1] + ":" + rsut[2] + ":" + rsut[3]; ;
                    judge1 = "fail";


                }
                cu--;
            } while (judge1 == "fail" && cu > 0);



            return judge1;

        }


        public void callbackwinmessage() {

            SendMessage(ptrWnd, WM_SEND_AUTOTEST, IntPtr.Zero, DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss")); /*自動測試消息*/

        }

        public void callbackdebuginfo(string m) {

            SendMessage(ptrWnd, WM_SENDB, IntPtr.Zero, m);

        }
        string cc(string a, string b, out string c,string d)
        {

        

          

            c = "tt";
            return "fsdaf";
        }



        string dd(string a, string b, out string c, string d)
        {



           

            c = "tt";
            return "dd";
        }

        public Dictionary<string, pointfun> Getfun()
        {



            return m;
        }

        private void  send_string(string [] abc) {

            foreach (string  a in abc)

            {
                string p = a.Remove(0);



            }


        }

        private void killproc(string procname) {

      
            Process[] allprocess = Process.GetProcessesByName(procname);

           
            foreach ( Process process in allprocess)
            {
                try
                {
                    process.Kill();
                }
                catch { }
            }

            }
       

      
    }
}

