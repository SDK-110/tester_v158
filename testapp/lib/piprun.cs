using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace ClassLibrary1
{
  
    class piprun
    {
         private Process process1;
         private ProcessStartInfo startInfo;

        public string  getruninfo() {

            process1.Start();
            process1.WaitForExit(30000);
            return process1.StandardError.ReadToEnd() + process1.StandardOutput.ReadToEnd();

        }
        public piprun(string startapp, string arg) {


            this.process1 = new Process();
            this.startInfo = new ProcessStartInfo(startapp);
            this.startInfo.Arguments = arg;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.UseShellExecute = false; //不使用系统外壳程序启动
            startInfo.RedirectStandardInput = true; //重定向输入（一定是true）
            startInfo.RedirectStandardOutput = true; //重定向输出
            startInfo.RedirectStandardError = true;
            this.process1.StartInfo = startInfo;
             
        }

        public override string ToString()
        {
            return this.getruninfo();
        }
    }
}
