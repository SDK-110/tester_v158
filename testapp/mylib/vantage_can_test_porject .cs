using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;

namespace testapp
{


    public class vantage_can_1123_project {
        enum output_pinmap_pin
        {
            RB0, RB1, RB2, RB3, RB4, RB5, RC1, RC2, RD7, RF0, RF1, RG2, RG3, RG6, RG8,
            RG9, RG12, RG13, RG14
        }
        byte _out_module_byte_low8 = 0x00;
        byte _out_module_byte_mid8 = 0x00;
        byte _out_module_byte_hi8 = 0x00;
        enum input_pinmap
        {
            RB8, RB9, RB11, RB12, RB13, RB14, RB15, RC13, RC14, RC15, RD0, RD1, RD3, RD4, RD5, RD6,
            RD8, RD9, RD10, RD11
        }
        byte _input_module_byte_low8 = 0x00;
        byte _input_module_byte_mid8 = 0x00;
        byte _input_module_byte_hi8 = 0x00;
     //   chuangxincan canadt = new chuangxincan(126984);
        Dictionary<string, int> input_module_pinmap = new Dictionary<string, int>();
        Dictionary<string, int> output_pinmap = new Dictionary<string, int>();
        public vantage_can_1123_project()
        {
            #region setpin_map
            output_pinmap.Add("RB0".ToUpper(), (int)output_pinmap_pin.RB0);
            output_pinmap.Add("RB1".ToUpper(), (int)output_pinmap_pin.RB1);
            output_pinmap.Add("RB2".ToUpper(), (int)output_pinmap_pin.RB2);
            output_pinmap.Add("RB3".ToUpper(), (int)output_pinmap_pin.RB3);
            output_pinmap.Add("RB4".ToUpper(), (int)output_pinmap_pin.RB4);
            output_pinmap.Add("RB5".ToUpper(), (int)output_pinmap_pin.RB5);
            output_pinmap.Add("RC1".ToUpper(), (int)output_pinmap_pin.RC1);
            output_pinmap.Add("RC2".ToUpper(), (int)output_pinmap_pin.RC2);
            output_pinmap.Add("RD7".ToUpper(), (int)output_pinmap_pin.RD7);
            output_pinmap.Add("RF0".ToUpper(), (int)output_pinmap_pin.RF0);
            output_pinmap.Add("RF1".ToUpper(), (int)output_pinmap_pin.RF1);
            output_pinmap.Add("RG2".ToUpper(), (int)output_pinmap_pin.RG2);
            output_pinmap.Add("RG3".ToUpper(), (int)output_pinmap_pin.RG3);
            output_pinmap.Add("RG6".ToUpper(), (int)output_pinmap_pin.RG6);
            output_pinmap.Add("RG8".ToUpper(), (int)output_pinmap_pin.RG8);
            output_pinmap.Add("RG9".ToUpper(), (int)output_pinmap_pin.RG9);
            output_pinmap.Add("RG12".ToUpper(), (int)output_pinmap_pin.RG12);
            output_pinmap.Add("RG13".ToUpper(), (int)output_pinmap_pin.RG13);
            output_pinmap.Add("RG14".ToUpper(), (int)output_pinmap_pin.RG14);
            #endregion
            #region input_map
            input_module_pinmap.Add("RB8 ".ToUpper(), (int)input_pinmap.RB8);
            input_module_pinmap.Add("RB9 ".ToUpper(), (int)input_pinmap.RB9);
            input_module_pinmap.Add("RB11".ToUpper(), (int)input_pinmap.RB11);
            input_module_pinmap.Add("RB12".ToUpper(), (int)input_pinmap.RB12);
            input_module_pinmap.Add("RB13".ToUpper(), (int)input_pinmap.RB13);
            input_module_pinmap.Add("RB14".ToUpper(), (int)input_pinmap.RB14);
            input_module_pinmap.Add("RB15".ToUpper(), (int)input_pinmap.RB15);
            input_module_pinmap.Add("RC13".ToUpper(), (int)input_pinmap.RC13);
            input_module_pinmap.Add("RC14".ToUpper(), (int)input_pinmap.RC14);
            input_module_pinmap.Add("RC15".ToUpper(), (int)input_pinmap.RC15);
            input_module_pinmap.Add("RD0".ToUpper(), (int)input_pinmap.RD0);
            input_module_pinmap.Add("RD1".ToUpper(), (int)input_pinmap.RD1);
            input_module_pinmap.Add("RD3".ToUpper(), (int)input_pinmap.RD3);
            input_module_pinmap.Add("RD4".ToUpper(), (int)input_pinmap.RD4);
            input_module_pinmap.Add("RD5".ToUpper(), (int)input_pinmap.RD5);
            input_module_pinmap.Add("RD6".ToUpper(), (int)input_pinmap.RD6);
            input_module_pinmap.Add("RD8".ToUpper(), (int)input_pinmap.RD8);
            input_module_pinmap.Add("RD9".ToUpper(), (int)input_pinmap.RD9);
            input_module_pinmap.Add("RD10".ToUpper(), (int)input_pinmap.RD10);
            input_module_pinmap.Add("RD11".ToUpper(), (int)input_pinmap.RD11);
            #endregion
        }
        public void _outfun_module_setpin_sign(string pinnmae, int hi_low)
        {

            if (output_pinmap[pinnmae.ToUpper()] < 8)
            {
                if (hi_low == 1)
                {
                    _out_module_byte_low8 = (byte)(_out_module_byte_low8 | (1 << output_pinmap[pinnmae.ToUpper()]));
                }
                else
                {

                    _out_module_byte_low8 = (byte)(_out_module_byte_low8 & ~(1 << output_pinmap[pinnmae.ToUpper()]));
                }
            }
            if (output_pinmap[pinnmae.ToUpper()] >= 8 && output_pinmap[pinnmae.ToUpper()] < 16)
            {
                if (hi_low == 1)
                {
                    _out_module_byte_mid8 = (byte)(_out_module_byte_mid8 | (1 << (output_pinmap[pinnmae.ToUpper()]) % 8));
                }
                else
                {


                    _out_module_byte_mid8 = (byte)(_out_module_byte_mid8 & ~(1 << (output_pinmap[pinnmae.ToUpper()]) % 8));
                }


            }
            if (output_pinmap[pinnmae.ToUpper()] >= 16 && output_pinmap[pinnmae.ToUpper()] < 24)
            {
                if (hi_low == 1)
                {
                    _out_module_byte_hi8 = (byte)(_out_module_byte_hi8 | (1 << (output_pinmap[pinnmae.ToUpper()]) % 8));

                }
                else
                {

                    _out_module_byte_hi8 = (byte)(_out_module_byte_hi8 & ~(1 << (output_pinmap[pinnmae.ToUpper()]) % 8));
                }

            }

        }
        /// <summary>
        /// 批量设置引脚
        /// </summary>
        /// <param name="setstr"></param>
        /// <returns></returns>
        public int _outfun_piliang(string setstr)
        {

            string[] group = setstr.Split(";".ToArray());
            for (int i = 0; i < group.Length; i++)
            {

                string[] judstr = group[i].Split(":".ToArray());
                if (judstr.Length != 2) return -1;
                string set_lowhistr = judstr[1];
                if (set_lowhistr != "1" && set_lowhistr != "0") return -2;
                string[] pinname = judstr[0].Split(",".ToArray());

                for (int z = 0; z < pinname.Length; z++)
                {

                    _outfun_module_setpin_sign(pinname[z].Trim(), int.Parse(set_lowhistr));

                }

            //   if( set_pins(_out_module_byte_low8, _out_module_byte_mid8, _out_module_byte_hi8)!=1) return -1;

            }

            return 1;


        }


        public void _in_outfun_clean_pin()
        {

            this._out_module_byte_hi8 = 0x00;
            this._out_module_byte_mid8 = 0x00;
            this._out_module_byte_low8 = 0x00;

            _input_module_byte_low8 = 0x00;
            _input_module_byte_mid8 = 0x00;
            _input_module_byte_hi8 = 0x00;
        }

        public void input_module_pinset(int byte_low8, int byte_mid8, int byte_hi8)
        {
            this._input_module_byte_low8 = (byte)byte_low8;
            this._input_module_byte_mid8 = (byte)byte_mid8;
            this._input_module_byte_hi8 = (byte)byte_hi8;

        }

        public string getpinstr()
        {

            return Convert.ToString(Convert.ToString(_input_module_byte_hi8, 2).PadLeft(8, '0')) +
                    Convert.ToString(Convert.ToString(_input_module_byte_mid8, 2).PadLeft(8, '0')) +
                    Convert.ToString(Convert.ToString(_input_module_byte_low8, 2).PadLeft(8, '0'));
        }

        public int set_pins(byte low_byte8, byte mid_byte8, byte hi_byte8)
        {
            try
            {
        
                byte[] sendbuf = add_checksum(0x60, new byte[] {  low_byte8, mid_byte8, hi_byte8, 0x00 });
                uint response_id = 0;
                byte[] rslt = new byte[] { };
               // if(canadt.send_rev(sendbuf, 0x181, out response_id, out rslt)!=1) return -1;

               
                string tmp = "[";
                for (int i = 0; i < rslt.Length; i++)
                {

                    tmp = tmp + $" { rslt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                if (rslt.Length > 0 && rslt[0]==0x60) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }

        public byte [] set_port(string pinz)
        {
           
                _outfun_piliang(pinz);

               return  add_checksum(0x60, new byte[] { _out_module_byte_low8, _out_module_byte_mid8, _out_module_byte_hi8,0x00 });
             
            
         
        }

        public int read_ad_port(string port, byte io_number, out int rsult)
        {



            byte[] sendbuf = add_checksum(0x61, new byte[] { PIN_data_tran(port), (byte)io_number, (byte)(0x00) });
            rsult = 0;
            try
            {


                return -1;
            }
            catch
            {
                //  System.Windows.Forms.MessageBox.Show("Test");
                return -5;
            }

            return -6;
        }

        private byte[] add_checksum(byte cmd, byte[] data)
        {
            byte[] rsu = new byte[6];
            rsu[0] = cmd;
            rsu[1] = data[0];
            rsu[2] = data[1];
            rsu[3] = data[2];
            rsu[4] = data[3];
            rsu[5] = (byte)(cmd + data[0] + data[1] + data[2] + data[3]);
            return rsu;
        }

        private byte PIN_data_tran(string pin)
        {

            string port = pin.Substring(0, 2).ToUpper();
            switch (port)
            {

                case "PA":
                    {
                        return (byte)0X00;
                    }
                    break;

                case "PB":
                    {


                        return (byte)(0X01);

                    }
                    break;
                case "PC":
                    {


                        return (byte)(0X02);

                    }
                    break;

                case "PD":
                    {

                        return (byte)(0X03);
                    }
                    break;
                case "PF":
                    {

                        return (byte)(0X04);
                    }
                    break;

                case "PG":
                    {

                        return (byte)(0X05);
                    }
                    break;


            }

            return 0xff;


        }

    }

    class vantage_serial_0075 : SerialPort
      {
            string recebuf;
      
            volatile byte[] rsubyt = new byte[200];
            string golb_pp = "";
            volatile int rev_count = 0;
            public string version { get { return golb_pp; } }
            public vantage_serial_0075(string port, int baudrate = 9600) : base(port)
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

        public int set_pins(byte low_byte8, byte mid_byte8, byte hi_byte8,byte h2byte8)
        {
            try
            {
                rev_count = 0;
                Array.Clear(rsubyt, 0, 200);
                byte[] sendbuf = send_bytes_dealwith(true, new byte[] {low_byte8, mid_byte8, hi_byte8 , h2byte8 });

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
                    byte[] sendbuf = send_bytes_dealwith(true, str2byte(sendstr));

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
                    if (rev_count > 3 && rsubyt[0]==0xa5) return 1;
                    return -2;
                }
                catch
                {



                    return -1;
                }
            }

        public int read_ad_port(string port,byte io_number, out int rsult)
            {
              
                golb_pp = "";
                rev_count = 0;
                rsult = -1;
                this.ReadExisting();
                Array.Clear(rsubyt, 0, 200);

                byte[] sendbuf = send_bytes_dealwith(false, new byte[] { });

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
                    if (rsubyt[0] != 0x061 || rev_count != 5) return -3;

                    rsult = rsubyt[rev_count - 3] * 255 + rsubyt[rev_count - 4];
                    return 1;


                }
                catch
                {
                    //  System.Windows.Forms.MessageBox.Show("Test");
                    return -5;
                }

                return -6;
            }

            private void Do_DataReceived(object sender, SerialDataReceivedEventArgs e)
            {

                SerialPort sp = (SerialPort)sender;
                System.Threading.Thread.Sleep(50);

                int m = sp.BytesToRead;
               if (m <= 0)
                return;
                byte[] tmp = new byte[m];
                sp.Read(tmp, 0, m);
                if (m != 9 ) {
               // if (m != 27) 
                return; }
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
            for(int ct = 0; ct < 2; ct++) { 
                byte[] sendbuf = send_bytes_dealwith(false, new byte[] {});
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
                if (rev_count >= 9) { 
                    for (int i = 5; i < rev_count; i++)
                    {
                            //按照约定反转
                        tmp2 = tmp2 + new string(Convert.ToString(rsubyt[i],2).PadLeft(8,'0').Reverse().ToArray()) + " " ;
                    }
                }
                mylib.utility_func.callbackdebuginfo("read_test_rev :" + tmp + "]" + "\r\n Reversed=>" + tmp2);
                    if (rev_count == 0) return -2;
                    if (rsubyt[0] != 0xa5 || rev_count != 9) return -3;
                    rsult = new byte[] { rsubyt[5], rsubyt[6], rsubyt[7], rsubyt[8] };
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

        private byte[] send_bytes_dealwith( bool fun_code_send_rev, byte[] data)
            {
            int datalen = data.Length;
               byte[] rsu = new byte[5 + datalen];
                rsu[0] = 0xa5;
                rsu[1] = 0x5a;
                rsu[2] = (byte)(2 + datalen);
            if (fun_code_send_rev == true)
            {
                rsu[3] = 0x83;
                rsu[4] = 0x60;
            }
            else {

                rsu[3] = 0x83;
                rsu[4] = 0x61;
            }

            for (int i = 0; i < datalen; i++) {

                rsu[5 + i] = data[i];
            }
            return rsu;
            }


        public static byte[] str2byte(string frstr)
        {
            int bufsize = 0;
            string t1 = frstr.Replace(":1", "").Replace(":0", "").Replace(";", ",");
            string[] t1_num = t1.Split(",".ToCharArray());
            int maxpin = 0;
            for (int i = 0; i < t1_num.Length; i++) {
                if (int.Parse(t1_num[i].Trim()) > maxpin) {
                    maxpin = int.Parse(t1_num[i].Trim());
                }
                
            }

            if (maxpin <=8)
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
            else {

                bufsize = 4;

            }

            byte[] tmp = new byte[bufsize] ;
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
                        tmp[(int.Parse(pinnamearray[j].Trim()) -1)/8] = (byte)(tmp[(int.Parse(pinnamearray[j].Trim())-1) / 8]| 1<< ((int.Parse(pinnamearray[j].Trim())-1) % 8));
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
            mylib.utility_func.callbackdebuginfo(" str2byte=>" + debugstr);
            return tmp;
        }

        public static int byte2str_comp_str_mode(byte[] comp, string frstr)
        {

            string compstr = "";
            //  Array.Reverse(comp);
            for (int i = 0; i < comp.Length; i++)
            {
                string z = new string (Convert.ToString(comp[i], 2).PadLeft(8, '0').Reverse().ToArray());
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
                        if (compstr.Substring(int.Parse(pinnamearray[j].Trim()) -1, 1) != "1")
                        {

                            return -1;
                        }

                    }
                    else
                    {

                        if (compstr.Substring(int.Parse(pinnamearray[j].Trim())-1, 1) != "0")
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

         
            if (((maxpin - 1) / 8 + 1 )> comp.Length) { System.Windows.Forms.MessageBox.Show("参数错误"); return -7; }
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
                        if ((comp[(int.Parse(pinnamearray[j].Trim())-1) /8] & (1<<(int.Parse(pinnamearray[j].Trim())-1) % 8)) ==0 )
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





        ~vantage_serial_0075()
            {
                this.Close();

            }

   }

    








}


