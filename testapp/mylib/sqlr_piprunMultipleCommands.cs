using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace testapp
{

  public static class  piprunMultipleCommands
    {
       public static  List<string> commands = new List<string>();
       public static   StringBuilder container = new StringBuilder();

        public static string runcommands_return_all() {
            container.Clear();
            if (commands.Count == 0) return "null";
            foreach (var cm in commands) {

                  string rs = new piprun("nrfjprog", cm).ToString();
                //string rs = new piprun("echo.cmd", cm).ToString();
                if (rs.IndexOf("The area to write is not erased") >= 0) return "pass";
                container.Append(rs);

                
            }

            string ret = container.ToString();
            if (ret.IndexOf("ERROR") < 0) return "pass";
            return "fail";

        }

        public static void set_write_UUID(string UUID) {
            commands.Clear();
           // string[] getuid_byte = UUID.Split(":".ToCharArray());

            commands.Add("-f nrf52 -r ");
            //commands.Add("-f nrf52--eraseuicr ");
            //commands.Add("--memwr 0x10001014 --val 0x000F8000 ");
            //commands.Add("--memwr 0x10001018 --val 0x000FE000 ");
            //    commands.Add(string.Format("--memwr 0x10001080 --val 0x{0}", getuid_byte[5]+ getuid_byte[4]+ getuid_byte[3]+ getuid_byte[1]));

            commands.Add(string.Format("--memwr 0x10001080 --val 0x{0}", UUID.Substring(10 , 2) + UUID.Substring(8, 2) + UUID.Substring(6, 2) + UUID.Substring(4, 2)));

            //  commands.Add(string.Format("--memwr 0x10001084 --val 0x{0}", (getuid_byte[1] + getuid_byte[0]).PadRight(8,'0')));
            commands.Add(string.Format("--memwr 0x10001084 --val 0x{0}", (UUID.Substring(2, 2) + UUID.Substring(0, 2)).PadRight(8, '0')));
            commands.Add("-f nrf52 -r");
        }



     }
}
