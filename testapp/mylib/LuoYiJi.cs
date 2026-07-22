using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using xktComm;
using System.Collections;
using System.Security.Cryptography;

namespace testapp
{
    public delegate void callback_msg(string m);
     
    class luoyinji : SerialPort
    {

       public  callback_msg get_msg = null;
        string recebuf;
        StringBuilder str_recebuf = new StringBuilder();
       volatile  byte[] rsubyt = new byte[200];

        volatile int rev_count = 0;
    public luoyinji(string port, int baudrate=115200) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 4000;
            base.WriteTimeout =4000;   
            base.ReadBufferSize = 4096*100;
            base.NewLine = "\0";
            
            base.Open();
           
            
            
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;

                int count_read_old ;
                int count_read;
                do {
                    count_read_old = sp.BytesToRead;
                    System.Threading.Thread.Sleep(50);
                    count_read = sp.BytesToRead;
                } while (count_read_old!=count_read);
          


            byte[] tmp = new byte[sp.BytesToRead];
            sp.Read(tmp, 0, tmp.Length);
            tmp = tmp.Select(b => b == 0 ? (byte)10 : b).ToArray();
            string strs = Encoding.ASCII.GetString(tmp);
            string[] str_array = strs.Split("\n".ToCharArray());
            foreach (var rsu in str_array) {

               string rsu_t = rsu.Substring(0, rsu.Length - 4);
                if(get_msg!=null)get_msg(rsu_t);
            }
           
             //   string rsu = sp.ReadExisting();
           //     rsu= rsu.Substring(0,rsu.Length-4);
              //  if(get_msg!=null)get_msg(rsu);
            }
            catch { 
            
            
            }
          
            //int m = sp.BytesToRead;
            //byte[] tmp = new byte[m];
            //sp.Read(tmp, 0, m);
            //for(int i= 0;i<m;i++) { if (tmp[i] == 0) tmp[i] = 10; }
            //str_recebuf.Append(Encoding.ASCII.GetString(tmp));
            ////System.Windows.Forms.MessageBox.Show(str_recebuf.ToString());
            //Array.Copy(tmp, rsubyt, m);
            //rev_count = m;
        }
        #region //没有用的函数
        private UInt16 crc(Byte [] data) {

          UInt16 a = 0;
            for (int i = 0; i < data.Length; i++) {

                a += (UInt16)data[i];
            
            }
            a %= 0x100;
            return a;
        }

        private Byte[] tran_crc(Byte[] data) {

            Byte[] a = data;
            UInt16 cr = crc(data);
            a[a.Length - 1] = (byte)cr;

            return a;
        }

        public void set_sing_relay(byte relay_num, byte openorclose) {

            Byte[] a = new Byte[] { 0x05, 0x01, 0X00,(byte)(relay_num-1),(byte)(openorclose),0x00 };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {

                    count++;
                    if (count > 3) {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0);
        
        
        }
        public void set_relay(byte relay_1_8, byte relay_9_16)
        {

            Byte[] a = new Byte[] { 0x01, 0x0F, 0X00,0x00,0x00, 0x10,0x02, (byte)(relay_1_8), (byte)(relay_9_16) };

            byte[] commend = tan_modbus(a);

            int count = 0;
            Byte[] m = new byte[commend.Length];

            do
            {
                try
                {
                    this.Write(commend, 0, commend.Length);
                    this.Read(m, 0, commend.Length);

                }
                catch (Exception)
                {
                    count++;
                    if (count > 3)
                    {

                        System.Windows.Forms.MessageBox.Show("sevy_relay com is error ,please see a professional");
                        return;
                    }

                }
            } while (count != 0 && count < 3);


        }
        #endregion

        public string  tran_command( string p ) {
            
            clear_data_buf();
            byte[] readstr = Encoding.UTF8.GetBytes(p);

           ushort crc =  crc16(readstr);
            //byte[] send_byts = new byte[readstr.Length + 1];
            //send_byts[readstr.Length] = 0;
            //for(int m = 0; m < readstr.Length; m++)
            //{

            //    send_byts[m] = readstr[m];

            //}

            string crc_str = $"{crc:x4}";
            crc_str = crc_str.Substring(2, 2) + crc_str.Substring(0, 2);

            return  p.ToUpper() + crc_str.ToUpper();
        }

        private string get_rsult_str() {


          return   str_recebuf.ToString().Replace("\0", "\n");

            
        }


        private void clear_data_buf() {
            rev_count = 0;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            str_recebuf.Clear();

        }
        /// <summary>
        /// 500ms 等待
        /// </summary>
        /// <param name="lop"></param>
        private void  delay(int lop=10) {

            int loopcout = lop;
            while (rev_count == 0 && loopcout-- >= 0)
            {

                System.Threading.Thread.Sleep(50);
            }

        }

        public int send_command(string p)
        {

            clear_data_buf();
            delay();
            try
            {




                string gk = tran_command(p);

                this.WriteLine(gk);
             //int gg =    str_double_bytes2_int("0064");
             // float m = bytes_str_2float("41200000");

                // string gg = float2bytes_str(1.0f);

                // string m = this.ReadLine();
                return 1;

            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -1;
            }
        }


        private byte[] floatValue_2bytes(float floatvalue) {

            byte[] bytes = BitConverter.GetBytes(floatvalue);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        private string float2bytes_str(float floatvalue) {

            byte[]  bytes = floatValue_2bytes(floatvalue);
            string str_float_byts="";
            foreach (byte b in bytes)
            {
                str_float_byts = str_float_byts + $"{b:X2}";
            }


            return str_float_byts;
        }




        private  float bytes_str_2float(string str_bytes) {

            byte[] bytes_temp = new byte[4];
            bytes_temp[0] = byte.Parse(str_bytes.Substring(0, 2),System.Globalization.NumberStyles.HexNumber);
            bytes_temp[1] = byte.Parse(str_bytes.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            bytes_temp[2] = byte.Parse(str_bytes.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            bytes_temp[3] = byte.Parse(str_bytes.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
            float rs;
            if (BitConverter.IsLittleEndian)
            {
                rs = BitConverter.ToSingle(bytes_temp.Reverse().ToArray(), 0);
            }
            else {

                rs = BitConverter.ToSingle(bytes_temp, 0);
            }


            return rs;
        }


        private int str_double_bytes2_int(string double_bytes) {



            byte[] bytes_temp = new byte[2];
            bytes_temp[0] = byte.Parse(double_bytes.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            bytes_temp[1] = byte.Parse(double_bytes.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes_temp);

            short shortValue = BitConverter.ToInt16(bytes_temp, 0);

            return shortValue;
        }




        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }






        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        ~luoyinji() { 
            this.Close();
           
        }

    }

}

