using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
namespace testapp
{
  
    class piprun
    {
         private Process process1;
         private ProcessStartInfo startInfo;

        public string  getruninfo() {
          
            //process1.Kill();
            process1.Start();
            process1.WaitForExit(30000);

            string m = process1.StandardError.ReadToEnd() + process1.StandardOutput.ReadToEnd();
            process1.Refresh();
            process1.Dispose();

            mylib.utility_func.callbackdebuginfo(m);
            return m;

        }
        public string getruninfofromwhile(int display=1)
        {
           // System.Windows.Forms.MessageBox.Show("Test");
            //process1.Kill();
            process1.Start();


            StringBuilder m = new StringBuilder();
            do
            {
                string tp = process1.StandardOutput.ReadLine();
                if (display == 1) {

                    mylib.utility_func.callbackdebuginfo(tp.Trim());
                }
                m.AppendLine(tp);

            } while (!process1.StandardOutput.EndOfStream);

            process1.Refresh();
            process1.Dispose();
            return m.ToString();

        }
        public piprun(string startapp, string arg) {


            //this.process1 = new Process();
            //this.startInfo = new ProcessStartInfo(startapp);
            //this.startInfo.Arguments = arg;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.CreateNoWindow = false;
            //startInfo.UseShellExecute = false; //不使用系统外壳程序启动
            //startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
            //startInfo.RedirectStandardOutput = true; //重定向输出
            //startInfo.RedirectStandardError = true;





            this.process1 = new Process();
            this.startInfo = new ProcessStartInfo(startapp);
            this.startInfo.Arguments = arg;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false; //不使用系统外壳程序启动
            startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
            startInfo.RedirectStandardOutput = true; //重定向输出
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            startInfo.WorkingDirectory = Path.GetDirectoryName(startapp);






            this.process1.StartInfo = startInfo;
             
        }

        public override string ToString()
        {

            return this.getruninfo();
        }
    }
}
