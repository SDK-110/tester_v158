using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace testapp
{
    class vantage_new_can_serial : SerialPort
    {
        string recebuf;
     
        volatile  byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
    public vantage_new_can_serial(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
           
            
            
        }








        public int MR_get_mcu_fw_ver(out string rsu, string mcu_name)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                switch (mcu_name.ToUpper())
                {
                    case "MCUA":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 00 3F 00 00 00 45");
                        }
                        break;
                    case "MCUB":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 00 3F 00 00 00 45");
                        }
                        break;
                    case "FPGA":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 00 3F 00 00 00 45");
                        }
                        break;
                    default:
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"00 01 00 3F 00 00 00 45");
                        }
                        break;
                }

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 50;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_PLD_TOGGLE_TEST(out string rsu, string status)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 00 01 {status.PadLeft(2, '0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_AC_IN_SET(out string rsu, string status1, string status2,string status3)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 01 {status1} {status2} {status3} 00 45");
                string pin_map = "";
                    pin_map = "AC in pin map:\n" +
                                     "   AC_IN_01   MCU_AC_IN_01    PH3        \n" +
                                     "   AC_IN_02   MCU_AC_IN_02	PA3        \n" +
                                     "   AC_IN_03   MCU_AC_IN_03	PA4        \n" +
                                     "   AC_IN_04   MCU_AC_IN_04	PA5        \n" +
                                     "   AC_IN_05   MCU_AC_IN_05	PA6        \n" +
                                     "   AC_IN_06   MCU_AC_IN_06	PA7        \n" +
                                     "   AC_IN_07   MCU_AC_IN_07	PC4        \n" +
                                     "   AC_IN_08   MCU_AC_IN_08	PC5        \n" +
                                     "   AC_IN_09   MCU_AC_IN_09	PB1        \n" +
                                     "   AC_IN_10   MCU_AC_IN_10	PB2        \n" +
                                     "   AC_IN_11   MCU_AC_IN_11	PI15       \n" +
                                     "   AC_IN_12   MCU_AC_IN_12	PF11       \n" +
                                     "   AC_IN_13   MCU_AC_IN_13	PF12       \n" +
                                     "   AC_IN_14   MCU_AC_IN_14	PF13       \n" +
                                     "   AC_IN_15   MCU_AC_IN_15	PF14       \n" +
                                     "   AC_IN_16   MCU_AC_IN_16	PF15       \n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 100;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_DC_IO_OUT_SET(out string rsu, string group_num, string status1, string status2)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 {group_num} 01 {status1} {status2} 00 00 45");
                string pin_map = "";
                if (group_num == "02")
                {


                    pin_map = "out pin map:\n" +
                                    "   MCU_DC_IN_01    PE2  <==>  DC_OU_01   \n  " +
                                    "   MCU_DC_IN_02    PE3  <==>  DC_OU_02   \n  " +
                                    "   MCU_DC_IN_03    PE4  <==>  DC_OU_03   \n  " +
                                    "   MCU_DC_IN_04    PE5  <==>  DC_OU_04   \n  " +
                                    "   MCU_DC_IN_05    PE6  <==>  DC_OU_05   \n  " +
                                    "   MCU_DC_IN_06    PI9  <==>  DC_OU_06   \n  " +
                                    "   MCU_DC_IN_07    PI10 <==>  DC_OU_07   \n  " +
                                    "   MCU_DC_IN_08    PI11 <==>  DC_OU_08   \n  " +
                                    "   MCU_DC_IN_09    PF0  <==>  DC_OU_09   \n  " +
                                    "   MCU_DC_IN_10    PF1  <==>  DC_OU_10   \n  " +
                                    "   MCU_DC_IN_11    PF2  <==>  DC_OU_11   \n  " +
                                    "   MCU_DC_IN_12    PF3  <==>  DC_OU_12   \n  ";






                }
                else
                {

                    pin_map = "out pin map:\n" +
                    "  J4-1	DC_IN_13  <==>    DC_OU_01     \n  " +
                    "  J4-2	DC_IN_14  <==>    DC_OU_02     \n  " +
                    "  J4-3	DC_IN_15  <==>    DC_OU_03     \n  " +
                    "  J4-4	DC_IN_16  <==>    DC_OU_04     \n  " +
                    "  J4-5	DC_IN_17  <==>    DC_OU_05     \n  " +
                    "  J4-6	DC_IN_18  <==>    DC_OU_06     \n  " +
                    "  J4-7	DC_IN_19  <==>    DC_OU_07       \n  " +
                    "  J4-8	DC_IN_20  <==>    DC_OU_08      \n  " +
                    "  J4-9	DC_IN_21  <==>    DC_OU_09     \n  " +
                    "  J4-10 DC_IN_22  <==>    DC_OU_10     \n  " +
                    "  J4-11 DC_IN_23  <==>    DC_OU_11     \n  " +
                    "  J4 - 12 DC_IN_24 <==>    DC_OU_12       \n  ";
                }


                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 100;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_PI_DATA_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "J7-2\tPI_DATA\r\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 06 01 {status.PadLeft(2, '0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_NTS_COMMAND_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "J7-2\tPI_DATA\r\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 05 01 {status.PadLeft(2, '0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_UART7_TX_RX_EN_ST_LOOP_TEST(out string rsu, string status1 = "01", string status2 = "00", string status3 = "00")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 04 01 {status1} 00 00 00 45");
                string pin_map = "";
               
                    pin_map = "MCUA_USART7_EN <==> MCUA_USART7_ST MCU_USART7_RX <==> MCU_USART7_TX\n";
               
             

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 50;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_MCU_CW_J8_PIN7_8_LOOP_TEST(out string rsu, string status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 {status} 00 00 00 45");
                string pin_map = "";
              
                    pin_map = "J8-7/8 short open\r\n\n";
             
                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_SAFE_KP_KM_RELAY_FB_TEST(out string rsu, string group, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 {group} 03 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = "get KM KP FB signal by mcu & fpga\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_SAFE_KP_KM_RELAY_OUT_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 00 03 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = "MR_SAFE_KP_KM_RELAY_OUT_TEST\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int MR_B2_COMM_FB_TEST(out string rsu, string status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 04 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = "B2 COMMAND AND FEEDBACK  test\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_SAFE_KM_RELAY_TEST(out string rsu, string status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 03 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = "SAFT REALY ";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }


        public int MR_DIPSWITCH_TEST(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 05 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = " DIPSW test  :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_USB_OTG_POWER_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 06 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = " USB OTG POWR ON  :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_USB_DISK_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 06 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = " USB DISK READ TEST  :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

       
          public int MR_PLD_TOGGLE_3V3_test(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 00 07 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = "  PLD_TOGGLE_3V3  TEST:\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_MCU_CPLD_SW_test(out string rsu,string sw_n ,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string pin_map = "";
                switch (sw_n) {

                    case "SW3": {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 07 {status} 00 00 00 45");


                            pin_map = " SW3_INS==>  MR_MCU_CPLD_INSP_test:\r\n\n";

                        }
                        break;
                    case "SW5":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 07 {status} 00 00 00 45");
                            pin_map = " SW4  MR_MCU_CPLD_CD BYPASS_test:\r\n\n";
                        }
                        break;
                    case "SW6":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 07 {status} 00 00 00 45");
                            pin_map = " SW5  MR_MCU_CPLD_HD BYPASS_test:\r\n\n";
                        }
                        break;



                }
            

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_RST_SW_test(out string rsu, string sw_n, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string pin_map = "";
                switch (sw_n)
                {

                    case "SW10":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 07 01 {status} 00 00 00 45");


                            pin_map = "SW10 FAULT RST FPGA IP TEST\r\n\n";

                        }
                        break;
                    case "SW11":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 08 01 {status} 00 00 00 45");
                            pin_map = "SW11 FAULT RET MCU TEST:\r\n\n";
                        }
                        break;
                    case "SW12":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 09" +
                                $" 01 {status} 00 00 00 45");
                            pin_map = "  SW12 EQ RST PIN TEST:\r\n\n";
                        }
                        break;



                }


                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int MR_SOFT_SW_test(out string rsu, string sw_n, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string pin_map = "";
                switch (sw_n)
                {

                    case "UP":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0A {status} 00 00 00 45");


                            pin_map = " UP_BUTTON_INS test:\r\n\n";

                        }
                        break;
                    case "ENABLE":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 0A {status} 00 00 00 45");
                            pin_map = "  ENABLE BUUTON test:\r\n\n";
                        }
                        break;
                    case "DN":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 0A {status} 00 00 00 45");
                            pin_map = "  DOWN BUTTON test:\r\n\n";
                        }
                        break;



                }


                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }



        public int MR_BOARD_address_test(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 08 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = " board address test :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 50;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }


        public int MR_MCU_SPI2_MCUB_SP1_loop_test(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0B 00 00 00 00 45");
                string pin_map = "";

                pin_map = "MR_MCU_SPI2_MCUB_SP1_loop_test :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_B2B_SPI5_6_LOOP_test(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 0C 00 00 00 00 45");
                string pin_map = "";

                pin_map = " MR_B2B_SPI5_6_LOOP_TEST :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_J6_RS485_NETWORK_LOP_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string pin_map = "";
                if (status == "485")
                {
                    pin_map = "  MR_J6_RS485_self_LOP_TEST:\r\n\n";
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0C 30 31 32 33 45");
                }
                {

                    pin_map = "  MR_J6_RS485_NETWORK_LOP_TEST:\r\n\n";
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 0C 30 31 32 33 45");
                }
              
                

             

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 50;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        
       public int MR_F_RAM_READ_WRITRE_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0D 30 31 32 33 45");
                string pin_map = "";

                pin_map = " MR_F_RAM_READ_WRITRE_TEST :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

       

      public int MR_CAN2_CAN3_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0E 30 31 32 33 45");
                string pin_map = "";

                pin_map = " MR_CAN2_CAN3_LOOP_TEST :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }


        public int MR_CAN4_CAN5_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0E 30 31 32 33 45");
                string pin_map = "";

                pin_map = " MR_CAN2_CAN3_LOOP_TEST :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp==""?"error": golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

 
        public int MR_MCUB_ADC_TMP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 11 00 00 00 00 45");
                string pin_map = "";

                pin_map = " TEAMPERATURE PIN TEST  :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        
        public int MR_QSPI_FLASH_U61_TEST(out string rsu, string status1, string status2, string status3)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 12 {status1} {status2} {status3} 00 45");
                string pin_map = "";

                pin_map = " MR_QSPI_FLASH_U61_TEST \r\n :\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int MR_MCUA_PA1_2_to_CPLD_PT9A_9B(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 10 {status} 00 00 00 45");
                string pin_map = "";

                pin_map = " MCUA PA1&2 <==>LCMX02-7000HC\tFPGA_PT9A\r\n\tFPGA-PT9B\r\n:\r\n\n";

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_LED_TEST(out string rsu, string ledn = "01", string set_status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };
                System.Threading.Thread.Sleep(200);
                switch (ledn)
                {

                    case "D88":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D50":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 02 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D51":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 03 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D53":
                        {
                           // System.Threading.Thread.Sleep(5000);
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 04 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;

                    case "D52":
                        {
                            //System.Threading.Thread.Sleep(5000);
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 05 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D91":
                        {
                            //System.Threading.Thread.Sleep(5000);
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 06 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D48":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 07 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D47":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 08 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D33":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 09 13 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    default:

                        {



                        }
                        break;


                }

            
                string test_str = "test " + ledn + ": " + set_status + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 100;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int MR_UI_SPI_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };
                System.Threading.Thread.Sleep(500);

                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 15 30 31 32 33 45");



                string test_str = "test" + "UI SPI" + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 50;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_UI_GPIO_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };


                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 16 {status.PadLeft(2, '0')} 00 00 00 45");



                string test_str = "test" + "UI GPIO" + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int MR_RTC_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };


                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 14 00 00 00 00 45");



                string test_str = "test" + "RTC READ " + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }



        public int MR_relay_board_interface_TEST(out string rsu, string realy_name = "01", string set_status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };

                switch (realy_name)
                {

                    case "NEUTRAL":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 09 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "Relay1":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 09 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "Relay2":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 09 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "Relay3":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 04 09 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;

                    case "Relay4":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 05 09 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;

                    default:

                        {



                        }
                        break;


                }


                string test_str = "test " + realy_name + ": " + set_status + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 100;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }




        public int CT_UI_GPIO_TEST(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };


                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 13 {status.PadLeft(2,'0')} 00 00 00 45");



                string test_str = "test" + "UI GPIO" + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_UI_SPI_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };
                System.Threading.Thread.Sleep(500);
            
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 12 30 31 32 33 45");
        


                string test_str = "test" + "UI SPI" + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_MCU_BOOT_TEST(out string rsu, string MCU = "A")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };

                if (MCU == "A") {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 11 00 00 00 00 45");
                }
                else {

                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 11 00 00 00 00 45");
                };
             


                string test_str = "test" + MCU +"\n";

                mylib.utility_func.callbackdebuginfo(test_str);

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_LED_TEST(out string rsu, string ledn = "01", string set_status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";

                byte[] sendbuf = { };

                switch (ledn) {

                    case "D26": {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 10 {set_status.PadLeft(2,'0')} 00 00 00 45");
                        }
                        break;
                    case "D31":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 02 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D32":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 03 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D28":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 04 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;

                    case "D27":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 05 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D29":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 06 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D48":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 07 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D47":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 08 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D33":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 09 10 {set_status.PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    default:

                        { 
                        
                        
                        
                        }
                        break;


                }


                string test_str ="test" + ledn +": " + set_status + "\n";

                mylib.utility_func.callbackdebuginfo(test_str);
                
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_U14_ISP_INTERFACE_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str =
                   " U14 SPI MCUB_SPI ACCELEROMETER TEST\n";
                ;

                mylib.utility_func.callbackdebuginfo(test_str);

                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0F 00 00 00 00 45");


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_IO_OUTPUT_TEST(out string rsu, string status )
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str =
                   " MCUB_PE0    PE0  FPGA_PT9A \n" +
                   " MCUB_PE1    PE1  FPGA_PT9B \n" +
                   " MCUB_PE2    PE2  FPGA_PT9C \n" +
                   " MCUB_PE3    PE3  FPGA_PT9D \n" +
                   " MCUB_PA0    PA0  FPGA MCUB_PLD_PIO1 \n" +
                   " MCUB_PA3    PA3  FPGA MCUB_PLD_PIO2 \n" +
                   " MCUB_PA6    PA6  FPGA MCUB_PLD_PIO3 \n" +
                   " MCUB_PC5    PC5  FPGA MCUB_PLD_PIO4 \n";
;

                mylib.utility_func.callbackdebuginfo(test_str);
               
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0E {status.PadLeft(2,'0')} 00 00 00 45");
              

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_CAN3_CAN4_LOOP_TEST(out string rsu, string status="1")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "CAN3<-->CAN4:\n";

                mylib.utility_func.callbackdebuginfo(test_str);
                if (status == "1")
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 0D 30 31 32 33 45");
                }
                else {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 0D 30 31 32 33 45");

                }
               
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 150;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_CAN2_CAN5_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "CAN2<-->CAN5:\n";
              
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0D 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 200;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_MCUA_SPI5_6_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "mcua_spi5<-->spi6 loop test\n" +
                   "MCUA_SPI5_NSS  <==>   MCUA_SPI6_NSS  \n" +
                   "MCUA_SPI5_SCK  <==>   MCUA_SPI6_SCK  \n" +
                   "MCUA_SPI5_MISO <==>   MCUA_SPI6_MISO \n" +
                   "MCUA_SPI5_MOSI <==>   MCUA_SPI6_MOSI \n";

                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0C 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_MCUA_SPI2_MCUB_SPI1_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "mcua_spi2<--> mcubspi1 loop test\n";

                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0B 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_J6_RS485_PLD_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "MCUA_UART4_RX\tPH14\nMCUA_UART4_TX\tPH13\nPLD_NET\tPR8A short together\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 0A 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_J6_RS485_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "MCUA_UART4_RX\tPH14\nMCUA_UART4_TX\tPH13\nPLD_NET\tPR8A short together\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 0A 30 31 32 33 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_USB_OTG_TEST(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "OTG_U_disk test \n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 09 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_USB_OTG_FS_PES_OC_4_D58_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "OTG_FS_PSE\tOTG_FS_OC \n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 09 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_SW7_STATUS_TEST(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "SW7 GET STATUS: \n" +
                   "SW7_9   MCU_DS_1	PH9  PL4A\n" +
                   "SW7_10  MCU_DS_2	PH10 PL4B\n" +
                   "SW7_11  MCU_DS_3	PH11 PL4C\n" +
                   "SW7_12  MCU_DS_4	PH12 PL5D\n" +
                   "SW7_13  MCU_DS_5	PH3  PL5C\n" +
                   "SW7_14  MCU_DS_6	PH7  PL4D\n" +
                   "SW7_15  MCU_DS_7	PH8  PL8A\n" +
                   "SW7_16 MCU_DS_8    PG0   PL8B\n";


                    ;
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 07 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_COP_SF1_4_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "test item:\n" +
                  " COP_SF1 PH5 <==>  DC_OUT01 \n" +
                  " COP_SF3 PA3 <==>  DC_OUT02 \n" +
                  " COP_SF4 PA4 <==>  DC_OUT03 \n" +
                  " COP_SF4 PA5 <==>  DC_OUT04 \n";

                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 08 {status.PadLeft(2, '0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_MCUA_ADDRESS_TEST(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "PD12\tPB11\tPA0\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 06 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_PI_DATA_TEST(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                string test_str = "J19\tPI_DATA <==> IN1\n";
                mylib.utility_func.callbackdebuginfo(test_str);
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 04 {status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_F_RAM_RW_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 05 30 31 32 33 45");
                string pin_map = "F_RAM READ WRITE TEST \n";
                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_UART2_TX_RX_LOOP_TEST(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 03 03 03 03 03 45");
                string pin_map = "MCU_USART2_RX <==> MCU_USART2_TX \n";
             

                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_UART7_TX_RX_EN_ST_LOOP_TEST(out string rsu,string status="01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 {status} 00 00 00 45");
                string pin_map = "";
                if (pin_map == "01")
                {
                  pin_map = "MCUA_USART7_EN <==> MCUA_USART7_ST\n";
                }
                else {

                  pin_map = "MCU_USART7_RX <==> MCU_USART7_TX \n";
                }
                
                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int CT_DC_IO_OUT_SET(out string rsu, string group_num,string status1,string status2)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 {group_num} 01 {status1} {status2} 00 00 45");
                string pin_map = "";
                if (group_num == "01")
                {


                    pin_map = "out pin map:\n" +
                                        "J2 - 1   DC_OUT_01 PG15  <==>   J1-1	DC_IN_01   PE2  PLD_DC_IN_01    \n  " +
                                       "J2 - 2    DC_OUT_02 PD6   <==>   J1-2	DC_IN_02   PE3  PLD_DC_IN_02    \n  " +
                                       "J2 - 3    DC_OUT_03 PD5   <==>   J1-3	DC_IN_03   PE4  PLD_DC_IN_03     \n  " +
                                       "J2 - 4    DC_OUT_04 PD4   <==>   J1-4	DC_IN_04   PE5  PLD_DC_IN_04     \n  " +
                                       "J2 - 5    DC_OUT_05 PD3   <==>   J1-5	DC_IN_05   PE6  PLD_DC_IN_05    \n  " +
                                       "J2 - 6    DC_OUT_06 PD2   <==>   J1-6	DC_IN_06   PI9  PLD_DC_IN_06    \n  " +
                                       "J2 - 7    DC_OUT_07 PB0   <==>   J1-7	DC_IN_07   PI10 PLD_DC_IN_07     \n  " +
                                       "J2 - 8    DC_OUT_08 PI3   <==>   J1-8	DC_IN_08   PI11 PLD_DC_IN_08    \n  " +
                                       "J2 - 9    DC_OUT_09 PI2   <==>   J1-9	DC_IN_09   PF0      \n  " +
                                       "J2 - 10   DC_OUT_10 PI1   <==>   J1-10	DC_IN_10   PF1      \n  " +
                                       "J2 - 11   DC_OUT_11 PI0   <==>   J1-11	DC_IN_11   PF2      \n  " +
                                       "J2 - 12   DC_OUT_12 PH15  <==>   J1-12	DC_IN_12   PF3      \n  ";



                }
                else {

                    pin_map = "out pin map:\n" +
                                        "J2 - 1   DC_OUT_01 PG15    <==>  J4-1	DC_IN_13   PF4     \n  " +
                                       "J2 - 2    DC_OUT_02 PD6     <==>  J4-2	DC_IN_14   PF5     \n  " +
                                       "J2 - 3    DC_OUT_03 PD5     <==>  J4-3	DC_IN_15   PF6     \n  " +
                                       "J2 - 4    DC_OUT_04 PD4     <==>  J4-4	DC_IN_16   PF7     \n  " +
                                       "J2 - 5    DC_OUT_05 PD3     <==>  J4-5	DC_IN_17   PF8     \n  " +
                                       "J2 - 6    DC_OUT_06 PD2     <==>  J4-6	DC_IN_18   PF9     \n  " +
                                       "J2 - 7    DC_OUT_07 PB0     <==>  J4-7	DC_IN_19   PF10      \n  " +
                                       "J2 - 8    DC_OUT_08 PI3     <==>  J4-8	DC_IN_20   PC0      \n  " +
                                       "J2 - 9    DC_OUT_09 PI2     <==>  J4-9	DC_IN_21   PC1     \n  " +
                                       "J2 - 10   DC_OUT_10 PI1     <==>  J4-10	DC_IN_22   PC2     \n  " +
                                       "J2 - 11   DC_OUT_11 PI0     <==>  J4-11	DC_IN_23   PC3     \n  " +
                                       "J2 - 12   DC_OUT_12 PH15    <==>  J4-12	DC_IN_24   PH2     \n  ";
                }


                mylib.utility_func.callbackdebuginfo(pin_map);
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 100;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_get_mcu_fw_ver(out string rsu, string mcu_name)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                switch (mcu_name.ToUpper())
                {
                    case "MCUA":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 00 3F 00 00 00 45");
                        }
                        break;
                    case "MCUB":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 00 3F 00 00 00 45");
                        }
                        break;
                    case "FPGA":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"52 03 00 3F 00 00 00 45");
                        }
                        break;
                    default:
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"00 01 00 3F 00 00 00 45");
                        }
                        break;
                }

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int CT_PLD_TOGGLE_TEST(out string rsu,string status)
        {
            try
            {
                System.Threading.Thread.Sleep(500);
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
               sendbuf = mylib.utility_func.strByts2ByteArray($"57 00 01 {status.PadLeft(2,'0')} 00 00 00 45");   
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }


        public int cop_test_led(out string rsu,string led_num, int onoff)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                switch (led_num.ToUpper())
                {
                    case "D25":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 06 {onoff.ToString().PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D26":
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 02 06 {onoff.ToString().PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    case "D46":
                        {
                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 03 06 {onoff.ToString().PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                    default:
                        {

                            sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 06 {onoff.ToString().PadLeft(2, '0')} 00 00 00 45");
                        }
                        break;
                }
                
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        
         public int test_cop_ui_interface_spi_test(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;

                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 05 30 31 32 33 45");


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int test_cop_J20_UART4_en_st_loop(out string rsu,string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;

                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 05 {status.ToString().PadLeft(2,'0')} 00 00 00 45");


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
       
        public int cop_test_ds_address_test(out string rsu, string status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
             
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 02 04 {status.PadLeft(2, '0')} 00 00 00 45");
                 

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int cop_test_can2_can3_loop(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 01 30 31 32 33 45");
                 
               

                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 40;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0 && rsu.Length==16) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int cop_test_board_address_test(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;

                sendbuf = mylib.utility_func.strByts2ByteArray($"52 01 04 02 00 00 00 45");


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int cop_test_io_in_out_loop_test(out string rsu, int staus_set)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                if (staus_set == 1)
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 AA AA AA 00 45");
                }
                else if( staus_set==0)
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 55 55 55 00 45");

                }else
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 FF 00 00 00 45");
                }


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 40;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0 && rsu.Length == 16) return 1;

                return -2;
            }
            catch(Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                rsu = "command error";
                return -1;
            }
        }
        public int cop_test_toggle_sft_test(out string rsu,int staus_set)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                if (staus_set == 1)
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 03 01 00 00 00 45");
                }
                else {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"57 01 03 00 00 00 00 45");

                }


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 40;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0 && rsu.Length == 16) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        

         public int COP_SF1_4_TO_IN5_8(out string rsu, int staus_set)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = null;
                if (staus_set == 1)
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 03 50 00 00 00 45");
                    mylib.utility_func.callbackdebuginfo("COP_SF4 to IN5-8 loop test, status is 0101");
                }
                else
                {
                    sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 03 a0 00 00 00 45");
                    mylib.utility_func.callbackdebuginfo("COP_SF4 to IN5-8 loop test, status is 1010");
                }


                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 40;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0 && rsu.Length == 16) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        public int test_code_switch(out string rsu)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
              byte[] sendbuf =  mylib.utility_func.strByts2ByteArray("52 00 01 2D 2D 2D 02 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int hb_test_led(out string rsu, string ledn="01", string set_status = "01")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"57 {ledn.PadLeft(2,'0')} 03 {set_status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }

        public int ul_dl_ub_db_test(out string rsu, string hilow_status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = mylib.utility_func.strByts2ByteArray($"52 00 02 {hilow_status.PadLeft(2,'0')} 00 00 00 45");
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 20;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                rsu = golb_pp == "" ? "error" : golb_pp;
                if (rev_count > 0) return 1;

                return -2;
            }
            catch
            {


                rsu = "command error";
                return -1;
            }
        }
        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(200);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            string pp = BitConverter.ToString(tmp).Replace("-", " ");
          //  System.Windows.Forms.MessageBox.Show("Test");
           mylib.utility_func.callbackdebuginfo("rev data:" + pp);
            if ( pp.ToUpper().StartsWith("53")==true && pp.ToUpper().EndsWith("45") == true) {

                golb_pp = pp.Replace(" ","");
                rev_count = m;
            }
            Array.Copy(tmp, rsubyt, m);
           
        }
      


  

        private  byte[] send_data_add_checksum(byte address_id, byte cmd, byte[] data)
        {

            byte[] rsu = new byte[data.Length + 7];
            byte len = (byte)(data.Length + 3);
            rsu[0] = 0x44;
            rsu[1] = 0x4e;
            rsu[2] = address_id;
            rsu[3] = len;
            rsu[4] = cmd;
            rsu[rsu.Length - 1] = 0x55;

            Array.Copy(data,0, rsu,5, data.Length);

            byte temp = rsu[3];

            for (int count =4; count < rsu.Length - 2; count++)
            {

                temp = (byte)(temp ^ rsu[count]);
            }

            rsu[rsu.Length - 2] = temp;
            rsu[rsu.Length - 1] = 0x55;
            return rsu;
        }

        ~vantage_new_can_serial() { 
            this.Close();
           
        }

    }

}

