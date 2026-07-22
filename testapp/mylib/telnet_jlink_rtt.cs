using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PrimS.Telnet;

namespace testapp.mylib
{
    public delegate void callback_dosomething();
    public delegate void callback_dosometing_take(object o);
    class telnet_jlink_rtt
    {
       public callback_dosomething to_connect_reset = null;
        Client telnet_client;
        public telnet_jlink_rtt()
        {
        //    mylib.utility_func.killproc("jlink");
        }
        ~telnet_jlink_rtt()
        {
            try
            {
                if (telnet_client.IsConnected) telnet_client.Dispose();
            }
            catch { }
            mylib.utility_func.killproc("jlink");
        }

        public int load_rtt_telent_evn(string jlink_RTT_server_bat="sgwjk.cmd",int delay =500)
        {
            if (to_connect_reset != null) {

                to_connect_reset();
            }
            mylib.utility_func.ex_exe_run(jlink_RTT_server_bat);

            System.Threading.Thread.Sleep(delay);

            if (true == to_connect(delay:200))
            {

                return 1;
            }
            else {

                return -1;
            }
            

        }

        public bool to_connect(string test_str = "test enter 0xdeadbeef",int delay=300) {


            CancellationToken token = new CancellationToken(false);
            telnet_client = new Client("127.0.0.1", 19021, token);
            
          //  System.Threading.Thread.Sleep(500);
            if (telnet_client.IsConnected)
            {             
              //  telnet_client.WriteLineAsync(test_str);
                string p =  telnet_client.TerminatedReadAsync("\n", new TimeSpan(0, 0, 2), 100).Result;
              //  mylib.utility_func.callbackdebuginfo("rtt_rev_cbf=> " + p.Trim());

                return true;
            }

            return false;
        }

        public string[] send_comm_str_and_rev_result(string comm_str,int delay=100) {
           // mylib.utility_func.callbackdebuginfo("Send=>" + comm_str);
            if (!telnet_client.IsConnected) {

                if (load_rtt_telent_evn() != 1) {

                    return new string[] { "-1" };
                }
            }

            telnet_client.WriteLineAsync(comm_str);

            System.Threading.Thread.Sleep(delay);
            string p = "";
            int count_p = 0;

            do
            {
                System.Threading.Thread.Sleep(100);

                var m = telnet_client.TerminatedReadAsync(new System.Text.RegularExpressions.Regex(@"\s{2,}", System.Text.RegularExpressions.RegexOptions.IgnoreCase),
                                                      new TimeSpan(10), 50);

                m.Wait();
                p = m.Result;

                if (count_p++ > 10) break;
            } while (p == null || p.Length == 0);
            //    mylib.utility_func.callbackdebuginfo("Rev=>" + p);
            return new string[]{
                    "1",
                    p


            };

            return new string[] { "-2" };


        }

        public string get_result_for_debug = "";
        public int get_result_reg(string comm_str ,string reg , out string rsult_str,int delays=100) {
            get_result_for_debug = "null";
            mylib.utility_func.callbackdebuginfo("Send=>" + comm_str);
            string[] p_rsu = send_comm_str_and_rev_result(comm_str,delays);
            rsult_str = "";
            if (p_rsu[0] != "1") return -1;
            mylib.utility_func.callbackdebuginfo("Rev=>" + p_rsu[1]);
            get_result_for_debug = mylib.utility_func.findstr_regex(@"gv\[\d\]:[\s]{0,1}(\d+)", p_rsu[1]); 
            string m = mylib.utility_func.findstr_regex($"({reg})", p_rsu[1]);
            mylib.utility_func.callbackdebuginfo("Rev_reg=>" +"[" + reg + ":"+ m + "]");
            if (m == "null") return -2;

            rsult_str = m;

            return  1;

        }

        public int _get_result_reg(string comm_str, string reg, out string rsult_str, int delays = 300)
        {
            mylib.utility_func.callbackdebuginfo("Send=>" + comm_str);
            string[] p_rsu = send_comm_str_and_rev_result(comm_str, delays);
            rsult_str = "";
            if (p_rsu[0] != "1") return -1;
            mylib.utility_func.callbackdebuginfo("Rev=>" + p_rsu[1]);

            string m = mylib.utility_func.findstr_regex(reg, p_rsu[1]);
            mylib.utility_func.callbackdebuginfo("Rev_reg=>" + "[" + reg + ":" + m + "]");
            if (m == "null") return -2;

            rsult_str = m;

            return 1;

        }
    }
}
