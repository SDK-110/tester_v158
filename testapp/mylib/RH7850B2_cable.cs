using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ConsoleApp1
{

    public class CustomSerialPort : SerialPort
    {
        public CustomSerialPort(string portName, int BaudRate= 9600) : base(portName, BaudRate)
        {
            base.NewLine = "\r";
            if (!base.IsOpen) base.Open();
        }

        ~CustomSerialPort() {

            if (base.IsOpen) base.Close();
        }

        private byte  check_sum(byte[] bytes) {
            int rs = bytes.Sum((o) => { return (int)o; });
     

           var  t =(byte)(rs);

            return t;
        }

        public string command_sum(string command= "01 00 01 30 01 00 00")
        {

            command =   command.Replace("0x", "").Replace("0X", "").Replace(",", "").Replace(" ","");
            byte[] byteArray = Encoding.UTF8.GetBytes(command);

            byte tmp =  check_sum(byteArray);
            string tmp_str = $"{tmp:x2}".ToUpper();
            return $"{command}{tmp_str}";
        }

        public void send_command_h_t(string command = "01 00 01 30 01 00 00") {
            base.ReadExisting();
            base.Write(new byte[] { 0X7E }, 0, 1);

            base.WriteLine(command_sum(command));
            

        }

    


    }

    class clush_str_frame {
        class frame_formt { 
           public string ver = "";
            public string add = "";
            public string status = "";
            public string RTN = "";
            public string LENGTH = "";
            public string data = "";
        public int d_len => int.Parse(LENGTH, System.Globalization.NumberStyles.HexNumber);
        public int d_add => int.Parse(add, System.Globalization.NumberStyles.HexNumber);
        }

          string [] frames ;
        public clush_str_frame(string frames)
        {
            frames = frames.Replace("~", "");
            this.frames = frames.Split('\r');


        }
        List<frame_formt> frame_Formts = new List<frame_formt>();
        public void clush_str_frame_fist()
        {
           foreach(var frame1 in frames) { 
           if (frame1 != "" && frame1!=null) {
            string ft = frame1.Replace("~", "").Replace("\r","");
            var f1 = new frame_formt();
            f1.ver = ft.Substring(0, 2);
            f1.add = ft.Substring(2, 4);
            f1.status = ft.Substring(6, 2);
            f1.RTN = ft.Substring(8, 2);
            f1.LENGTH = ft.Substring(10, 4);
            if (int.Parse(f1.LENGTH,System.Globalization.NumberStyles.HexNumber) != 0)
            {

                f1.data = ft.Substring(14, ft.Length-16);

            }
           
            frame_Formts.Add(f1);

            }
            }
        }


    }
}
