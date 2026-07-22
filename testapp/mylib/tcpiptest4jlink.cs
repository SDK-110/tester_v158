using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace testapp
{
  public   class tcpiptest4jlink
    {

 
        bool connerr = false;
        Socket client = null;
        IPAddress ip;
        IPEndPoint endPoint;
        public tcpiptest4jlink(string strip, int portnum)
        {
           
            ///IP地址
         

            try
            {
                ip = IPAddress.Parse(strip);
                ///端口号
                endPoint = new IPEndPoint(ip, portnum);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
              //  client.Connect(endPoint);
                client.Connect("localhost", portnum);
                client.ReceiveTimeout =600;
                client.SendTimeout = 50;

                System.Threading.Thread.Sleep(50);
                // byte[] arrList = new byte[0x6a];
                byte[] arrList = new byte[0x100];
                
                ///接收到的信息大小(所占字节数)
                int length = client.Receive(arrList);
                string msg = Encoding.ASCII.GetString(arrList, 0, length);
                System.Threading.Thread.Sleep(50);

            }
            catch (Exception e)
            {
               // System.Windows.Forms.MessageBox.Show(e.ToString());
                connerr = true;
               
            }

        }

      public string sendMsg_and_revMsg(string sendmsg)
        {


            if (connerr) return "conn error";

            try
            {

                byte[] arrMsg = Encoding.ASCII.GetBytes(sendmsg + "\n");
                client.Send(arrMsg);
                ///定义客户端接收到的信息大小
                byte[] arrList = new byte[1024];
                ///接收到的信息大小(所占字节数)
                int length = client.Receive(arrList);
                string msg = Encoding.ASCII.GetString(arrList, 0, length);
                client.Close();
                return msg;
            }
            catch (Exception)
            {
                ///关闭客户端
                client.Close();
                return "msg error";
            }

           



        }

      public string sendMsg_and_revMsg(string sendmsg, int non_close = 1)
        {


            if (connerr) return "conn error";

            try
            {
                //byte[] arrList0 = new byte[1024];
                //int length0 = client.Receive(arrList0);
                //if (length0 == 0) return "error";
                //string msg0 = Encoding.ASCII.GetString(arrList0, 0, length0);

                System.Threading.Thread.Sleep(20);
                byte[] arrMsg = Encoding.ASCII.GetBytes(sendmsg + "\n");
                if (client == null || client.Connected == false) return  "send error";
                client.Send(arrMsg);
                ///定义客户端接收到的信息大小
                byte[] arrList = new byte[1024];
                ///接收到的信息大小(所占字节数)


                int tmp = 21;
                do
                {
                    if (tmp == 0) return "wait error";
                    System.Threading.Thread.Sleep(50);
                } while (client.Available <3 && tmp-- > 0);

                //  System.Threading.Thread.Sleep(250);
                int length = client.Receive(arrList);
               
                string msg = Encoding.ASCII.GetString(arrList, 0, length);
              //  System.Windows.Forms.MessageBox.Show(length + ":" + msg);
                return msg;
            }
            catch (Exception e)
            {
               // System.Windows.Forms.MessageBox.Show(e.ToString());
                ///关闭客户端
              //  client.Close();
                return "msg error";
            }

           
        }


        public bool jlink_state
        {

            get { return client.Connected; }

        }

        public void jlink_close()
        {

            try {
                if (client != null && client.IsBound) { client.Close(); client.Dispose(); }
            }
            catch { 
            
            
            }

        }



        ~tcpiptest4jlink()
        {

            if (client != null && client.IsBound) client.Close();

        }


    }
}
