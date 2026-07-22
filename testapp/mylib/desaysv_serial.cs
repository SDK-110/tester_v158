using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class desaysv_serial : SerialPort
    {
        string recebuf;
     
        volatile  byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
      public string version { get { return golb_pp; } }
    public desaysv_serial(string port, int baudrate=9600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;
            
            base.Open();
           
            
            
        }

       

  

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(200);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);

            string hex_str = "";
            
            foreach (var h_str in tmp) {

                hex_str = hex_str + $" {h_str:2x}";
            }
            mylib.utility_func.callbackdebuginfo("DUT send:<>" + hex_str);
         //   string testpp = mylib.utility_func.findstr_regex("(\\d{2}[_|\\.]\\d{2})", pp);
            //if ( testpp!= "null") {

            //    golb_pp = testpp;
            //}
            Array.Copy(tmp, rsubyt, m);
            rev_count = m;
        }

        public int  send_command(byte fun_id3, byte parameter) {


            rev_count = 0;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            byte[] sendbuf = add_checksum(0xff, 0xcf, fun_id3, parameter);

            this.Write(sendbuf, 0, sendbuf.Length);
            int loopcout = 10;
            while (rev_count == 0 && loopcout-- >= 0)
            {

                System.Threading.Thread.Sleep(50);
            }
            try
            {
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("command rev msg :" + tmp + "]");
                if (rev_count == 0) return -2;
                if (rsubyt[1] == 0x06 || rev_count != 7) return -3;
                return 1;


            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -5;
            }

            return -6;

         

        


        }

        public int send_command_handshake()
        {


            rev_count = 0;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            byte[] sendbuf  = add_checksum(0xff, 0x02, 0xff, 0xff);

            this.Write(sendbuf, 0, sendbuf.Length);
            this.Write(sendbuf, 0, sendbuf.Length);
            int loopcout = 10;
            while (rev_count == 0 && loopcout-- >= 0)
            {

                System.Threading.Thread.Sleep(50);
            }
            try
            {
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("DUT connection msg :" + tmp + "]");
                if (rev_count == 0) return -2;
                if (rsubyt[0] == 0x06 || rev_count != 7) return -3;
                return 1;


            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -5;
            }

            return -6;




          


        }




        private  byte checksum_x_or(byte[] data)
        {

            byte checksum = 0;
            foreach (byte b in data)
            {

                checksum ^= b;

            }
            return checksum;
        }

        private  byte[] add_checksum(byte funcid1, byte funcid2, byte fun_id3, byte parameter)
        {

            byte len_c = 0x05;
            byte[] rsu = new byte[len_c + 1];
            rsu[0] = len_c;
            rsu[1] = funcid1;
            rsu[2] = funcid2;
            rsu[3] = fun_id3;
            rsu[4] = parameter;
            rsu[5] = 0x00;
            for (int count = 0; count < rsu.Length - 1; count++)
            {

                rsu[5] ^= rsu[count];
            }

            return rsu;
        }

        ~desaysv_serial() { 
            this.Close();
           
        }

    }

}

