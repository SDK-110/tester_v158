using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
namespace testapp.mylib
{

    class weite_ttl_can4_1123 : SerialPort
    {
        string recebuf;

        volatile byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
        public string version { get { return golb_pp; } }
        public weite_ttl_can4_1123(string port, int baudrate = 9600) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.DataReceived += Do_DataReceived;
            base.ReadTimeout = 1000;
            base.WriteTimeout = 2000;

            base.Open();



        }

        public int set_pins(byte low_byte8, byte mid_byte8, byte hi_byte8, byte h2byte8)
        {
            try
            {
                this.ReadExisting();
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                byte[] sendbuf = send_bytes_dealwith(1, new byte[] { low_byte8, mid_byte8, hi_byte8, h2byte8 });

                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }

        public int set_ports_pin(string sendstr)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                byte[] sendbuf = send_bytes_dealwith(1, str2byte(sendstr));

                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {

                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                if (rev_count > 3 && rsubyt[0] == 0x4b) return 1;
                return -2;
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());


                return -1;
            }
        }

       
        private void Do_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            SerialPort sp = (SerialPort)sender;
            System.Threading.Thread.Sleep(150);

            int m = sp.BytesToRead;
            if (m <= 0) return;
            byte[] tmp = new byte[m];
            sp.Read(tmp, 0, m);
            if (m != 6) { return; }
            Array.Copy(tmp, rsubyt, m);
            rev_count = m;
        }
        #region //没有用的函数
        private UInt16 crc(Byte[] data)
        {

            UInt16 a = 0;
            for (int i = 0; i < data.Length; i++)
            {

                a += (UInt16)data[i];

            }
            a %= 0x100;
            return a;
        }

        private Byte[] tran_crc(Byte[] data)
        {

            Byte[] a = data;
            UInt16 cr = crc(data);
            a[a.Length - 1] = (byte)cr;

            return a;
        }
        #endregion


        public int read_pin_status(out byte[] rsult)
        {
            rsult = new byte[] { };
            golb_pp = "";
            rev_count = 0;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            for (int ct = 0; ct < 2; ct++)
            {
                byte[] sendbuf = send_bytes_dealwith(2, new byte[] { });
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                if (rev_count == 0) continue;
                try
                {
                    string tmp = "[";
                    for (int i = 0; i < rev_count; i++)
                    {

                        tmp = tmp + $" { rsubyt[i]:x2}";
                    }
                    string tmp2 = "";
                    if (rev_count >= 6)
                    {
                        for (int i = 1; i < rev_count-1; i++)
                        {
                            //按照约定反转
                            tmp2 = tmp2 + new string(Convert.ToString(rsubyt[i], 2).PadLeft(8, '0').Reverse().ToArray()) + " ";
                        }
                    }
                    mylib.utility_func.callbackdebuginfo("read_test_rev :" + tmp + "]" + "\r\n Reversed=>" + tmp2);
                    if (rev_count == 0) return -2;
                    if (rsubyt[0] != 0x61 || rev_count != 6) return -3;
                    rsult = new byte[] { rsubyt[1], rsubyt[2], rsubyt[3], rsubyt[4] };
                    return 1;


                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show("Test");
                    return -5;
                }

                return -6;
            }

            return -8;
        }

        public int Read_bus_template(out byte[] rsult)
        {
            rsult = new byte[] { };
            golb_pp = "";
            rev_count = 0;
            this.ReadExisting();
            Array.Clear(rsubyt, 0, 200);
            for (int ct = 0; ct < 2; ct++)
            {
                byte[] sendbuf = send_bytes_dealwith(3, new byte[] { });
                this.Write(sendbuf, 0, sendbuf.Length);
                int loopcout = 10;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                if (rev_count == 0) continue;
                try
                {
                    string tmp = "[";
                    for (int i = 0; i < rev_count; i++)
                    {

                        tmp = tmp + $" { rsubyt[i]:x2}";
                    }

                    mylib.utility_func.callbackdebuginfo("read_test_rev :" + tmp + "]");
                    if (rev_count == 0) return -2;
                    if (rsubyt[0] != 0x62 || rev_count != 6) return -3;
                    rsult = new byte[] { rsubyt[1], rsubyt[2] };
                    return 1;


                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show("Test");
                    return -5;
                }

                return -6;
            }

            return -8;
        }
        // crc校验函数
        #region crc 暂时没用保留
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }
        private Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }
        #endregion

        private byte[] send_bytes_dealwith(int  fun_code_send_rev, byte[] data)
        {
           
            byte[] rsu = new byte[6];
            switch (fun_code_send_rev)
            {
                case 1:
                    {
                       
                        rsu[0] = 0x60;
                    }
                    break;
                case 2:
                    {
                        rsu[0] = 0x61;
                    }
                    break;
                case 3:

                    {
                        rsu[0] = 0x62;
                    }
                    break;
            
              
                
            }

            for (int i = 0; i < data.Length; i++)
            {

                rsu[1+i] = data[i];
            }
            rsu[5] = (byte)(rsu[0] + rsu[1] + rsu[2] + rsu[3] + rsu[4]);
            return rsu;
        }


        public static byte[] str2byte(string frstr)
        {
            int bufsize = 0;
            string t1 = frstr.Replace(":1", "").Replace(":0", "").Replace(";", ",");
            string[] t1_num = t1.Split(",".ToCharArray());
            int maxpin = 0;
            for (int i = 0; i < t1_num.Length; i++)
            {
                if (int.Parse(t1_num[i].Trim()) > maxpin)
                {
                    maxpin = int.Parse(t1_num[i].Trim());
                }

            }

            if (maxpin <= 8)
            {
                bufsize = 1;
            }
            else if ((maxpin > 8) && (maxpin <= 16))
            {

                bufsize = 2;

            }
            else if ((maxpin > 16) && (maxpin <= 24))
            {

                bufsize = 3;
            }
            else
            {

                bufsize = 4;

            }

            byte[] tmp = new byte[bufsize];
            string bitstr = "";
            string[] groups = frstr.Split(";".ToCharArray());
            for (int gp = 0; gp < groups.Length; gp++)
            {
                string pinnames = groups[gp].Split(":".ToCharArray())[0];
                string status = groups[gp].Split(":".ToCharArray())[1];
                string[] pinnamearray = pinnames.Split(",".ToCharArray());

                for (int j = 0; j < pinnamearray.Length; j++)
                {
                    if (status == "1")
                    {
                        tmp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] = (byte)(tmp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] | 1 << ((int.Parse(pinnamearray[j].Trim()) - 1) % 8));
                    }
                    else
                    {

                        tmp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] = (byte)(tmp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] & ~(1 << ((int.Parse(pinnamearray[j].Trim()) - 1) % 8)));
                    }

                }
            }
            //foreach (var p in tmp)
            //{

            //    bitstr = bitstr + p;

            //}



            //byte[] buf = new byte[bitstr.Length / 8];
            //int cout = 0;
            //for (int i = 0; i < bitstr.Length; i += 8)
            //{

            //    //byte m =  Convert.ToByte("11111",2);
            //    int a = bitstr.Length - (i);
            //    if (a >= 8) { buf[cout] = Convert.ToByte(bitstr.Substring(i, 8), 2); }
            //    else { buf[cout] = Convert.ToByte(bitstr.Substring(i, a), 2); }
            //    cout++;

            //}
            //string debugstr = "";
            //for (int i = 0; i < buf.Length; i++)
            //{

            //    debugstr = debugstr + (string.Format("0x{0:x2}[", buf[i])) + string.Format("{0}];", Convert.ToString(buf[i], 2)).PadLeft(8, '0');

            //}
            //mylib.utility_func.callbackdebuginfo(debugstr);
            //return buf;

            string debugstr = "";
            for (int i = 0; i < tmp.Length; i++)
            {

                debugstr = debugstr + (string.Format("0x{0:x2}[", tmp[i])) + string.Format("{0}];", Convert.ToString(tmp[i], 2)).PadLeft(8, '0');

            }
            mylib.utility_func.callbackdebuginfo(debugstr);
            return tmp;
        }

        public static int byte2str_comp_str_mode(byte[] comp, string frstr)
        {

            string compstr = "";
            //  Array.Reverse(comp);
            for (int i = 0; i < comp.Length; i++)
            {
                string z = new string(Convert.ToString(comp[i], 2).PadLeft(8, '0').Reverse().ToArray());
                compstr = compstr + z;
            }
            mylib.utility_func.callbackdebuginfo(compstr);
            string[] groups = frstr.Split(";".ToCharArray());
            for (int gp = 0; gp < groups.Length; gp++)
            {
                string pinnames = groups[gp].Split(":".ToCharArray())[0];
                string status = groups[gp].Split(":".ToCharArray())[1];
                string[] pinnamearray = pinnames.Split(",".ToCharArray());

                for (int j = 0; j < pinnamearray.Length; j++)
                {
                    if (status == "1")
                    {
                        // Console.WriteLine(pinnamearray[j].Trim() + " :" + compstr.Substring(int.Parse(pinnamearray[j].Trim()), 1));
                        if (compstr.Substring(int.Parse(pinnamearray[j].Trim()) - 1, 1) != "1")
                        {

                            return -1;
                        }

                    }
                    else
                    {

                        if (compstr.Substring(int.Parse(pinnamearray[j].Trim()) - 1, 1) != "0")
                        {

                            return -2;
                        }
                    }

                }
            }

            return 1;




        }

        public static int byte2str_comp_mem_mode(byte[] comp, string frstr)
        {

            int bufsize = 0;

            string t1 = frstr.Replace(":1", "").Replace(":0", "").Replace(";", ",");
            string[] t1_num = t1.Split(",".ToCharArray());
            int maxpin = 0;
            for (int i = 0; i < t1_num.Length; i++)
            {
                if (int.Parse(t1_num[i].Trim()) > maxpin)
                {
                    maxpin = int.Parse(t1_num[i].Trim());
                }

            }


            if (((maxpin - 1) / 8 + 1) > comp.Length) { System.Windows.Forms.MessageBox.Show("参数错误"); return -7; }
            string compstr = "";
            //  Array.Reverse(comp);
            for (int i = 0; i < comp.Length; i++)
            {
                string z = Convert.ToString(comp[i], 2).PadLeft(8, '0');
                compstr = compstr + z;
            }
            mylib.utility_func.callbackdebuginfo(compstr);
            string[] groups = frstr.Split(";".ToCharArray());
            for (int gp = 0; gp < groups.Length; gp++)
            {
                string pinnames = groups[gp].Split(":".ToCharArray())[0];
                string status = groups[gp].Split(":".ToCharArray())[1];
                string[] pinnamearray = pinnames.Split(",".ToCharArray());

                for (int j = 0; j < pinnamearray.Length; j++)
                {
                    if (status == "1")
                    {
                        // Console.WriteLine(pinnamearray[j].Trim() + " :" + compstr.Substring(int.Parse(pinnamearray[j].Trim()), 1));
                        if ((comp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] & (1 << (int.Parse(pinnamearray[j].Trim()) - 1) % 8)) == 0)
                        {

                            return -1;
                        }

                    }
                    else
                    {

                        if ((comp[(int.Parse(pinnamearray[j].Trim()) - 1) / 8] & (1 << (int.Parse(pinnamearray[j].Trim()) - 1) % 8)) >= 1)
                        {

                            return -1;
                        }
                    }

                }
            }

            return 1;




        }





        ~weite_ttl_can4_1123()
        {
            this.Close();

        }

    }










}



