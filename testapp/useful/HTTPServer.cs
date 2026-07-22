using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace useful
{
    class HTTPServer
    {
    }



    public class ServerHelper
    {
        HttpListener httpListener = new HttpListener();
        public void Setup(int port = 8080)
        {
            httpListener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            httpListener.Prefixes.Add(string.Format("http://127.0.0.1:{0}/", port));//如果发送到8080 端口没有被处理，则这里全部受理，+是全部接收
            httpListener.Start();//开启服务

            Receive();//异步接收请求

            
        }

        private void Receive()
        {
            httpListener.BeginGetContext(new AsyncCallback(EndReceive), null);
        }

        void EndReceive(IAsyncResult ar)
        {
            var context = httpListener.EndGetContext(ar);
            Dispather(context);//解析请求
            Receive();
        }

        RequestHelper RequestHelper;
        ResponseHelper ResponseHelper;
        void Dispather(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            RequestHelper = new RequestHelper(request);
            ResponseHelper = new ResponseHelper(response);
            RequestHelper.DispatchResources(fs =>
            {
                ResponseHelper.WriteToClient(fs);// 对相应的请求做出回应
            });
        }
    }
    public class RequestHelper
    {
        private HttpListenerRequest request;
        public RequestHelper(HttpListenerRequest request)
        {
            this.request = request;
        }
        public Stream RequestStream { get; set; }
        public void ExtracHeader()
        {
            RequestStream = request.InputStream;
        }

        public delegate void ExecutingDispatch(FileStream fs);
        public delegate void ExecutingDispatch1(string fs);
        public void DispatchResources(ExecutingDispatch1 action)
        {


            var m = ShowRequestData(request);
            //var rawUrl = request.RawUrl;//资源默认放在执行程序的wwwroot文件下，默认文档为index.html
            //string filePath = string.Format(@"{0}/wwwroot{1}", Environment.CurrentDirectory, rawUrl);//这里对应请求其他类型资源，如图片，文本等
            //if (rawUrl.Length == 1)
            //    filePath = string.Format(@"{0}/wwwroot/index.html", Environment.CurrentDirectory);//默认访问文件
            try
            {

                action(m);

               
            }
            catch (Exception e) {

                System.Windows.Forms.MessageBox.Show(e.ToString());
                return; }
        }
        public void ResponseQuerys()
        {
            var querys = request.QueryString;
            foreach (string key in querys.AllKeys)
            {
                VarityQuerys(key, querys[key]);
            }
        }

        private void VarityQuerys(string key, string value)
        {
            switch (key)
            {
                case "pic": Pictures(value); break;
                case "text": Texts(value); break;
                default: Defaults(value); break;
            }
        }

        private void Pictures(string id)
        {

        }

        private void Texts(string id)
        {

        }

        private void Defaults(string id)
        {

        }

        public string ShowRequestData(HttpListenerRequest request)
       {
           if (!request.HasEntityBody)
           {
               
               return "";
           }
           System.IO.Stream body = request.InputStream;
           System.Text.Encoding encoding = request.ContentEncoding;
           System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
           if (request.ContentType == null)
           {
               return "" ;
           }
      
           string s = reader.ReadToEnd();
           body.Close();
           reader.Close();
           // If you are finished with the request, it should be closed also.
           return s;
       }

    }
    public class ResponseHelper
    {
        private HttpListenerResponse response;
        public ResponseHelper(HttpListenerResponse response)
        {
            this.response = response;
            OutputStream = response.OutputStream;

        }
        public Stream OutputStream { get; set; }
        public class FileObject
        {
            public FileStream fs;
            public byte[] buffer;
        }
        public void WriteToClient(FileStream fs)
        {
            response.StatusCode = 200;
            byte[] buffer = new byte[1024];
            FileObject obj = new FileObject() { fs = fs, buffer = buffer };
            fs.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(EndWrite),obj);
        }
        public void WriteToClient(string fs)
        {
            response.StatusCode = 200;
            try
            {
                new Task(() =>
                {
                    byte[] m = System.Text.ASCIIEncoding.ASCII.GetBytes(fs);
                    OutputStream.Write(m, 0, m.Length);
                    //System.Threading.Thread.Sleep(2000);
                    OutputStream.Close();

                }).Start();
              
            }
            catch { }
        }


        void EndWrite(IAsyncResult ar)
        {
            var obj = ar.AsyncState as FileObject;
            var num = obj.fs.EndRead(ar);
            OutputStream.Write(obj.buffer, 0, num);
            if (num < 1)
            {
                obj.fs.Close(); //关闭文件流　　　　　　　　　　OutputStream.Close();//关闭输出流，如果不关闭，浏览器将一直在等待状态 　　　　　　　　　　return; 　　　　　　　　}
                obj.fs.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(EndWrite), obj);
            }
        }
        //void EndWrite(IAsyncResult ar)
        //{
        //    var obj = ar.AsyncState as FileObject;
        //    var num = obj.fs.EndRead(ar);
        //    OutputStream.Write(obj.buffer, 0, num);
        //    if (num < 1)
        //    {
        //        obj.fs.Close(); //关闭文件流　　　　　　　　　　OutputStream.Close();//关闭输出流，如果不关闭，浏览器将一直在等待状态 　　　　　　　　　　return; 　　　　　　　　}
        //        obj.fs.BeginRead(obj.buffer, 0, obj.buffer.Length, new AsyncCallback(EndWrite), obj);
        //    }
        //}
    }

}
