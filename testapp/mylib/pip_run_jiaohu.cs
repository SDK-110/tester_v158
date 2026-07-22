using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace testapp
{
    public delegate void send_message_pip(string msg);
    class pip_run_jiaohu
    {
        public send_message_pip msg_s;
        public int p_exit = 0;
        private   Process process1;
        private bool is_exit = false;
        private bool is_rev_msg = false;
        private StringBuilder rsu = new StringBuilder();
        public string  getruninfo(string strcommand) {
            is_rev_msg = false;
            pip_send_command(strcommand);
            do {
                System.Threading.Thread.Sleep(500);
            }while (!is_rev_msg);
            string m = rsu.ToString();
            rsu.Clear();
            return m;

        }
 

        public int  pip_send_command(string command) {

            try
            {
                process1.StandardInput.WriteLine(command);
                return 1;
            }
            catch {


                return -1;
            }

        }
        public pip_run_jiaohu(string startapp, string arg, send_message_pip msg_s=null ) {

            p_exit = 0;
            this.process1 = new Process();
            this.process1.StartInfo.FileName = startapp;
            process1.StartInfo.Arguments = arg;
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.RedirectStandardError = true;
            process1.StartInfo.CreateNoWindow = true;//true表示不显示黑框，false表示显示dos界面 
            process1.StartInfo.UseShellExecute = false;

            if (msg_s != null) this.msg_s = msg_s;

           process1.EnableRaisingEvents = true;
            
            process1.Exited += new EventHandler(p_Exited);
            process1.OutputDataReceived += new DataReceivedEventHandler(p_OutputDataReceived);
            process1.ErrorDataReceived += new DataReceivedEventHandler(p_ErrorDataReceived);

            process1.Start();
            
            process1.BeginOutputReadLine();
            process1.BeginErrorReadLine();

          
          
     
            process1.Exited += (o,p)=>{ p_exit = 1;

                if (msg_s != null)
                {

                    msg_s("__exit__%%__");
                }


            };
        }

        private void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data!=null) {
                   if (msg_s != null)
                    {

                        msg_s(e.Data);
                    }
                    rsu.Append(e.Data);
                  
                   is_rev_msg = true;
                }

            }
            catch { }
        }

        private void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data != null)
                {
                    if (msg_s != null)
                    {

                        msg_s(e.Data);
                    }
                    rsu.Append(e.Data);
                    pip_send_command(" ");
                    is_rev_msg = true;
                }

            }
            catch { }
        }

        private void p_Exited(object sender, EventArgs e)
        {
            try
            {
                is_exit = true;

            }
            catch { }
        }

        public override string ToString()
        {
            int times = 10;
            while (p_exit != 1 && times-->0) {

                System.Threading.Thread.Sleep(1000);
            }
            if (rsu.Length > 0) {
                string rsut = this.rsu.ToString();
                this.rsu.Clear();
                return rsut;
            }
            
       
            return "null";
            
        }

        ~pip_run_jiaohu() {


            try
            {
               
                process1.Kill();
                process1.Close();
                
            }
            catch { }
        
        }
    }
}
