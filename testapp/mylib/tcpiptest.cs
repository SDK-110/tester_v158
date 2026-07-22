using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace testapp
{
    class tcpiptest
    {

 
        bool connerr = false;
        Socket client = null;
        IPAddress ip;
        IPEndPoint endPoint;
        public tcpiptest(string strip, int portnum)
        {
           
            ///IP地址
         

            try
            {
                ip = IPAddress.Parse(strip);
                ///端口号
                endPoint = new IPEndPoint(ip, portnum);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(endPoint);
                client.ReceiveTimeout = 20000;
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

                byte[] arrMsg = Encoding.ASCII.GetBytes(sendmsg + "\r\n");
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

                byte[] arrMsg = Encoding.ASCII.GetBytes(sendmsg + "\r\n");
                client.Send(arrMsg);
                ///定义客户端接收到的信息大小
                byte[] arrList = new byte[1024];
                ///接收到的信息大小(所占字节数)
                int length = client.Receive(arrList);
                string msg = Encoding.ASCII.GetString(arrList, 0, length);
              
                return msg;
            }
            catch (Exception)
            {
                ///关闭客户端
                client.Close();
                return "msg error";
            }





        }


        ~tcpiptest()
        {

            if (client != null && client.IsBound) client.Close();

        }


    }
}
