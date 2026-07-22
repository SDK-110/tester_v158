using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;
namespace testapp
{
  
    class ssh_debug
    {
        SshClient sshClient;
        public ssh_debug(string host = "localhost", string userName = "eng-te", string psw = "123456")
        {
            try
            {
                sshClient = new SshClient(host, 22, userName, psw);
                sshClient.Connect();

            }
            catch {

                System.Windows.Forms.MessageBox.Show("connection error");

            }
            
        }


        public string send_command(string comm) {


           string  rsu =  sshClient.RunCommand(comm).Execute();


            return rsu;
        }







       ~ ssh_debug()
        {
            if (sshClient.IsConnected) sshClient.Disconnect();

        }





    }
}
