using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using Vila.Extensions;

namespace testapp
{
    class sgw_DN_display_serial : SerialPort
    {
        string recebuf;

        volatile byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
        string sn = "";
        public string SN { get { return sn; } }
    public sgw_DN_display_serial(string port, int baudrate=9600) : base(port)
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

        public int read_boardver(out string bardver)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                this.ReadExisting();
                byte[] sendbuf = send_data_add_checksum(0x00, 0x43, new byte[] { });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                bardver = golb_pp;
                if (golb_pp.Length == 20){ bardver= golb_pp.Substring(10, 6); return 1; } 
                return -2;
            }
            catch
            {

                bardver = "error";

                return -1;
            }
        }
        public int write_boardver(string boardver = "123")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                int xuzheng = 48;
                if (boardver.Length < 3) { mylib.utility_func.callbackdebuginfo("board ver error!!!"); return -1; }
                this.ReadExisting();
                byte[] sendbuf = send_data_add_checksum(0x00, 0x42, new byte[] { (byte)(boardver[0]), (byte)(boardver[1]), (byte)(boardver[2])});
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }
        public int uart2_test()
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                int xuzhen = 48;
                this.ReadExisting();
                byte[] sendbuf = send_data_add_checksum(0x02, 0x50, new byte[] { });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
               
                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {

                

                return -1;
            }
        }
        public int read_sn(out string sn )
        {
            try
            {
                sn = "";
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                this.ReadExisting();
                byte[] sendbuf = send_data_add_checksum(0x00, 0x45, new byte[] { });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
               
                sn = golb_pp;
                this.sn = mylib.utility_func.HexStringToString(sn.Substring(10, 44)); 
                if (golb_pp.Length != 58) return -2;
              //  sn = mylib.utility_func.HexStringToString(golb_pp.Substring(10, 44));
                return 1;
              
            }
            catch
            {

                sn = "error";

                return -1;
            }
        }

        public int read_fw_ver(out string fw_ver)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = send_data_add_checksum(0x00, 0x0B, new byte[] { });
                this.ReadExisting();
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                fw_ver = golb_pp;
                if (rev_count== 10) return 1;
                return -2;
            }
            catch
            {

                fw_ver = "error";

                return -1;
            }
        }
        public int write_sn(string sn = "1111111111111111111111")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                int xuzhen = 48;
                this.ReadExisting();
                if (sn.Length < 6) { mylib.utility_func.callbackdebuginfo("sn error!!!"); return -1; }
                byte[] sendbuf = send_data_add_checksum(0x00, 0x44, mylib.utility_func.StringToFixedLengthByteArray(sn, 22));
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }
        public int get_key_status(out string key_status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                this.ReadExisting();
                golb_pp = "";
                byte[] sendbuf = send_data_add_checksum(0x00, 0x46, new byte[] { });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                key_status = golb_pp;

                if (golb_pp.Length == 16 && golb_pp.StartsWith("44")) { key_status = golb_pp.Substring(10, 2); return 1; }
                return -2;
            }
            catch
            {

                key_status = "error";

                return -1;
            }
        }
        public int get_pir_status(out string pir_status)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                golb_pp = "";
                byte[] sendbuf = send_data_add_checksum(0x00, 0x47, new byte[] {});
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                pir_status = golb_pp;
                if (golb_pp.Length==16 && golb_pp.StartsWith("44")){pir_status = golb_pp.Substring(10, 2); return 1;} 
                return -2;
            }
            catch(Exception ex)
            {
                mylib.utility_func.callbackdebuginfo("comm error:"  + ex.ToString());
                pir_status = "comm error";

                return -1;
            }
        }
        public int  set_enter_test_mode()
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                this.ReadExisting();
                golb_pp = "";
                byte[] sendbuf = send_data_add_checksum(0x00, 0x41, new byte[] { 0x1 });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                if (rev_count>0) return 1;
                return -2;
            }
            catch {



                return -1;
            }
        }

        public int led_test(string  status="ff ff ff ff ff")
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                status = status.Replace(" ", "");
                if (status.Length != 10) { mylib.utility_func.callbackdebuginfo("input status error"); return -3; }
                byte[] sendbuf = send_data_add_checksum(0x00, 0x48, 
                    new byte[] { byte.Parse(status.Substring(0,2), System.Globalization.NumberStyles.HexNumber),
                                 byte.Parse(status.Substring(2,2), System.Globalization.NumberStyles.HexNumber),
                                 byte.Parse(status.Substring(4,2), System.Globalization.NumberStyles.HexNumber),
                                 byte.Parse(status.Substring(6,2), System.Globalization.NumberStyles.HexNumber),
                                 byte.Parse(status.Substring(8,2), System.Globalization.NumberStyles.HexNumber) });
                mylib.utility_func.callbackdebuginfo("send_data:" + BitConverter.ToString(sendbuf));
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }

                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(200);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            string pp = BitConverter.ToString(tmp).Replace("-", " ");
          //  System.Windows.Forms.MessageBox.Show("Test");
           mylib.utility_func.callbackdebuginfo("rev data:" + pp);
            if ( pp.ToUpper().IndexOf("44 4E")>=0) {

                golb_pp = pp.Replace(" ","");
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


        #region zanshimeiyong
        // crc校验函数
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }

        private  Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }

        #endregion


  

        private  byte[] send_data_add_checksum(byte address_id, byte cmd, byte[] data)
        {

            byte[] rsu = new byte[data.Length + 7];
            byte len = (byte)(data.Length + 3);
            rsu[0] = 0x44;
            rsu[1] = 0x4e;
            rsu[2] = address_id;
            rsu[3] = len;
            rsu[4] = cmd;
            rsu[rsu.Length - 1] = 0x55;

            Array.Copy(data,0, rsu,5, data.Length);

            byte temp = rsu[3];

            for (int count =4; count < rsu.Length - 2; count++)
            {

                temp = (byte)(temp ^ rsu[count]);
            }

            rsu[rsu.Length - 2] = temp;
            rsu[rsu.Length - 1] = 0x55;
            return rsu;
        }

        ~sgw_DN_display_serial() { 
            this.Close();
           
        }

    }

}

