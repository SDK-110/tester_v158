using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{
    class legrand_proj_serial : SerialPort
    {
        string recebuf;
     
        volatile  byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
      public string version { get { return golb_pp; } }
    public legrand_proj_serial(string port, int baudrate=9600) : base(port)
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

        public int  set_port(string port, int low_hi)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                byte pin = PIN_data_tran(port);
                if (pin == 0xff) return -2;
                //0x00：读命令
                //0x01：写命令
                //0x02：返回命令
                //0x03：返回提示不支持这个命令
                byte[] sendbuf = add_checksum(0x00, 0x01, new byte[] { pin, (byte)low_hi });

                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                string tmp = "[";
                for (int i = 0; i < rev_count; i++) {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                if (rev_count>0) return 1;
                return -2;
            }
            catch {



                return -1;
            }
        }

        public int  read_ad_port(string port, out int rsult)
        {
            golb_pp = "";
            rev_count = 0;
            rsult = -1;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            byte pin = PIN_data_tran(port);
            if (pin == 0xff) return -1;
            byte[] sendbuf = add_checksum(0x00, 0x00, new byte[] { pin });
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
                mylib.utility_func.callbackdebuginfo("read_AD_rev :" + tmp + "]");
                if (rev_count == 0) return -2;
                if (rsubyt[1] == 0x03 || rev_count!=9) return -3;
                if (rsubyt[rev_count - 1] != 0x55 || rsubyt[rev_count - 5] != pin) return -4;

                rsult = rsubyt[rev_count - 3]*255 + rsubyt[rev_count - 4];
                return 1;


            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -5;
            }

            return -6;
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(200);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            string pp = Encoding.ASCII.GetString(tmp);
          //  System.Windows.Forms.MessageBox.Show("Test");
            string testpp = mylib.utility_func.findstr_regex("(\\d{2}[_|\\.]\\d{2})", pp);
            if ( testpp!= "null") {

                golb_pp = testpp;
            }
            Array.Copy(tmp, rsubyt, m);
            rev_count = m;
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

        public int read_pin_status(string port, out int rsult) {
            golb_pp = "";
            rev_count = 0;
            rsult = -1;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            byte pin = PIN_data_tran(port);
            if (pin == 0xff) return -1;
            byte[] sendbuf = add_checksum(0x00, 0x00, new byte[] { pin });
            this.Write(sendbuf, 0, sendbuf.Length);
            int loopcout = 10;
            while (rev_count == 0 && loopcout-- >= 0) {

                System.Threading.Thread.Sleep(50);
            }
            try
            {
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("read_test_rev :" + tmp + "]");
                if (rev_count == 0) return -2;
                if (rsubyt[1] == 0x03) return -3;
                if (rsubyt[rev_count - 1] != 0x55  || rsubyt[rev_count - 4]!= pin)
                    return -4;
                
                rsult = rsubyt[rev_count - 3];
                return 1;
               
   
            }
            catch {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -5;
            }

            return -6;
        }

        
        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }



 
        private byte PIN_data_tran(string pin) {

            string port = pin.Substring(0, 2).ToUpper();
            int m = 0xff;
            int.TryParse(pin.Substring(2), out m);
            if (m == 0xff) {
                return 0xff;
            }
            int portnum = m;
            switch (port) {

                case "PA":
                    {
                        return (byte)portnum;
                    }
                    break;

                case "PB":
                    {
                        int i = 1 << 4;
                        i = i + portnum;

                        return (byte)(i);

                    }
                    break;
                case "PC":
                    {

                        int i = 2 << 4;
                        i = i + portnum;

                        return (byte)(i);

                    }
                    break;

                case "PD":
                    {
                        int i = 3 << 4;
                        i = i + portnum;

                        return (byte)(i);
                    }
                    break;
                case "PE":
                    {
                        int i = 4 << 4;
                        i = i + portnum;

                        return (byte)(i);
                    }
                    break;


            }

            return 0xff;


        }

        private  byte[] add_checksum(byte index_num, byte cmd, byte[] data)
        {

            byte[] rsu = new byte[data.Length + 6];
            byte len = (byte)(data.Length + 3);
            rsu[0] = 0xaa;
            rsu[1] = len;
            rsu[2] = index_num;
            rsu[3] = cmd;

            Array.Copy(data,0, rsu,4, data.Length);

            byte temp = rsu[1];

            for (int count = 2; count < rsu.Length - 2; count++)
            {

                temp = (byte)(temp ^ rsu[count]);
            }

            rsu[rsu.Length - 2] = temp;
            rsu[rsu.Length - 1] = 0x55;
            return rsu;
        }

        ~legrand_proj_serial() { 
            this.Close();
           
        }

    }

}

