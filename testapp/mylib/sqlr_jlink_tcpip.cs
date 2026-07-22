using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace testapp
{

  public static class sqlr_jlink_tcpip
    {
       public static  List<string> commands = new List<string>();
       public static   StringBuilder container = new StringBuilder();

        public static string runcommands_return_all() {

            if (commands.Count == 0) return "null";
            foreach (var cm in commands) {

                //  string rs = new piprun("nrfjprog", cm).ToString();
                string rs = new piprun("echo.cmd", cm).ToString();
                container.Append(rs);

                
            }


            return container.ToString();

        }

        public static void set_DUT_dtm_ch0_rx_v27_cheung() {

         //   commands.Add("ble start 0xdeadbeef");
            commands.Add("ble dtm 1 0 0 0");
  
        }

        public static string set_dut_dtm_rst_v27_ccheung() {

            tcpiptest4jlink tcpiptest = new tcpiptest4jlink("localhost", 19021);
            if (tcpiptest == null) return "-1";

            return tcpiptest.sendMsg_and_revMsg("ble dtm 3 0 0 0");

        



    }
        public static string set_dut_enter_test_mode() {

            tcpiptest4jlink tcpiptest = new tcpiptest4jlink("127.0.0.1", 19021);
           
            if (tcpiptest == null) return "-1";
            System.Threading.Thread.Sleep(200);
            string temprsu = (tcpiptest.sendMsg_and_revMsg("gitdata", 1));
            System.Threading.Thread.Sleep(200);
            if (temprsu==""){ return "-1"; };
            temprsu = (tcpiptest.sendMsg_and_revMsg("test enter 0xdeadbeef", 1));
            
            return   temprsu;



        }


        public static string get_result_signal(string command) {


            tcpiptest4jlink tcpiptest = new tcpiptest4jlink("127.0.0.1", 19021);

            if (tcpiptest == null) return "-1";
            System.Threading.Thread.Sleep(200);
            string temprsu = (tcpiptest.sendMsg_and_revMsg("gitdata", 1));
            System.Threading.Thread.Sleep(100);
            if (temprsu == "") { return "-1"; };
            temprsu= (tcpiptest.sendMsg_and_revMsg("test enter 0xdeadbeef", 1));

          string  temprsu1 = tcpiptest.sendMsg_and_revMsg(command, 1);
            return temprsu1;

        }

        public static string  get_result_arrays(string command)
        {
            string[] m = {"read deviceid", "read deviceid", "read deviceid" };
            Dictionary<int, string> result = new Dictionary<int, string>();
            tcpiptest4jlink tcpiptest = new tcpiptest4jlink("127.0.0.1", 19021);

            if (tcpiptest == null) return "-1";
            System.Threading.Thread.Sleep(200);
            string temprsu = (tcpiptest.sendMsg_and_revMsg("gitdata", 1));
            System.Threading.Thread.Sleep(100);
            if (temprsu == "") { return "-1"; };
            temprsu = (tcpiptest.sendMsg_and_revMsg("test enter 0xdeadbeef", 1));
            for (int i = 0; i <30; i++)
            {
                System.Threading.Thread.Sleep(100);
                string   temprsu12 = (tcpiptest.sendMsg_and_revMsg(m[0], 1));
              //  if (!(temprsu12.IndexOf("ok") >= 0)) { return "-1"; }
                result[i] = temprsu12;

            }
          

            string temprsu1 = tcpiptest.sendMsg_and_revMsg(command, 1);
            return temprsu1;

        }


    }
}
