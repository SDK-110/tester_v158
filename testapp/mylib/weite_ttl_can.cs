using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using NationalInstruments.DataInfrastructure;
// using Windows.UI.Xaml.Controls;

namespace testapp.mylib
{

    class weite_ttl_can : SerialPort
    {
        string recebuf;

        volatile byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
        public string version { get { return golb_pp; } }
        public weite_ttl_can(string port, int baudrate = 9600) : base(port)
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
            base.NewLine = "\r\n";
            base.Open();
          
          


        }

        public void clear_rev_data() {
            this.ReadExisting();
            rev_count = 0;
            Array.Clear(rsubyt, 0, 200);


        }

        public int data_rev_do_something(string STR_Standard_Frame_ID = "0X058F" , string original_hex_data= "00 00 03 00 03 00")
        {
            try
            {

                clear_rev_data();
                uint fram_id = uint.Parse(STR_Standard_Frame_ID.ToUpper().Replace("0X", ""), System.Globalization.NumberStyles.HexNumber);
                byte[] datas= utility_func.strByts2ByteArray(original_hex_data);
                send_data( fram_id,datas );
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                if (rev_count == 0) return -4;
                byte[] _rev_data_tmp = new byte[rev_count];
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {
                    _rev_data_tmp[i] = rsubyt[i];
                    tmp = tmp + $" { rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                var _id_data = get_id_and_data(_rev_data_tmp);
                mylib.utility_func.callbackdebuginfo("set_test_rev :[" +$" {_id_data.Item1:x}"+":"+ utility_func.get_bytes_str(_id_data.Item2)+ "]");
                if (rev_count > 0) return 1;
                return -2;
            }
            catch
            {



                return -1;
            }
        }



        public (int,string) data_rev_not_doanything(string comp_hex_data = "00 00 03 00 03 00")
        {
            try
            {

                clear_rev_data();             
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                if (rev_count == 0) return (-4,null);
                byte[] _rev_data_tmp = new byte[rev_count];
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {
                    _rev_data_tmp[i] = rsubyt[i];
                    tmp = tmp + $" {rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                var _id_data = get_id_and_data(_rev_data_tmp);
                if (_id_data.Item1 == uint.MaxValue) {
                    mylib.utility_func.callbackdebuginfo("rev_messag_error");
                    return (-3,null);
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :[" + $" {_id_data.Item1:x}" + ":" + utility_func.get_bytes_str(_id_data.Item2) + "]");

                byte[] compdats = utility_func.strByts2ByteArray(comp_hex_data);
                bool is_equ = true;
                for(int i=0;i<_id_data.Item2.Length;i++)
                {

                    if (_id_data.Item2[i] != compdats[i]) {

                        is_equ = false;

                        break;
                    }


                }

                if (is_equ) { return (1, utility_func.get_bytes_str(_id_data.Item2)); } else {

                    return (2, utility_func.get_bytes_str(_id_data.Item2));
                }
                return (-2,null);
            }
            catch
            {



                return (-1,null);
            }
        }

        public (int, string) data_get()
        {
            try
            {

                clear_rev_data();
                int loopcout = 30;
                while (rev_count == 0 && loopcout-- >= 0)
                {

                    System.Threading.Thread.Sleep(50);
                }
                if (rev_count == 0) return (-4, null);
                byte[] _rev_data_tmp = new byte[rev_count];
                string tmp = "[";
                for (int i = 0; i < rev_count; i++)
                {
                    _rev_data_tmp[i] = rsubyt[i];
                    tmp = tmp + $" {rsubyt[i]:x2}";
                }
                mylib.utility_func.callbackdebuginfo("set_test_rev :" + tmp + "]");
                var _id_data = get_id_and_data(_rev_data_tmp);
                if (_id_data.Item1 == uint.MaxValue)
                {
                    mylib.utility_func.callbackdebuginfo("rev_messag_error");
                    return (-3, null);
                }

                mylib.utility_func.callbackdebuginfo("set_test_rev :[" + $" {_id_data.Item1:x}" + ":" + utility_func.get_bytes_str(_id_data.Item2) + "]");

                return (1, utility_func.get_bytes_str(_id_data.Item2));

            }
             
            
            catch
            {



                return (-1, null);
            }
        }



        public (UInt32, byte[]) get_id_and_data(byte[] rawdata) {

          
            if (!(rawdata.Length > 8 && rawdata[0] == 0x41 && rawdata[1] == 0x54 && rawdata[rawdata.Length - 2] == 0x0d && rawdata[rawdata.Length - 1] == 0x0a)) { return(UInt32.MaxValue,new byte[] { }); }
          var id_biaoji =   bytes2id(new byte[] { rawdata[2], rawdata[3], rawdata[4], rawdata[5] });
            int cout = rawdata[6];
            if (cout == 0) {
                return (UInt32.MaxValue, new byte[] { });
            }
            byte[] rsu_data_bytes = new byte[cout];
            for (int i = 0; i < cout; i++) {

                rsu_data_bytes[i] = rawdata[7 + i];
            }

            return (id_biaoji.Item1, rsu_data_bytes);


        }

        private void Do_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                System.Threading.Thread.Sleep(150);

                int m = sp.BytesToRead;
                if (m <= 0) return;
                byte[] tmp = new byte[m];
                sp.Read(tmp, 0, m);
              //  utility_func.callbackdebuginfo(utility_func.get_bytes_str(tmp));
                if (!(tmp.Length > 8 && tmp[0] == 0x41 && tmp[1] == 0x54 && tmp[tmp.Length - 2] == 0x0d && tmp[tmp.Length - 1] == 0x0a)) { return; }
                Array.Copy(tmp, rsubyt, m);

                rev_count = m;
            }
            catch {

            }
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


          // crc校验函数
        #region crc 暂时没用保留
        private UInt16 crc16(Byte[] ptr)
        { return ModbusCrc16.Compute(ptr); }
        private Byte[] tan_modbus(Byte[] data)
        { return ModbusCrc16.AppendCrc(data); }
        #endregion

  
        public int send_data(UInt32 id, byte [] data, bool is_kuozhanzhen=false) {
            try
            {
               
                clear_rev_data();
                byte[] s_data = new byte[2 + data.Length + 4 + 2 + 1];

                s_data[0] = 0x41;
                s_data[1] = 0x54;
                byte[] id_bytes = fram_id_zhuanhuan(id, is_kuozhanzhen, false);
                s_data[2] = id_bytes[0];
                s_data[3] = id_bytes[1];
                s_data[4] = id_bytes[2];
                s_data[5] = id_bytes[3];
                s_data[6] = (byte)data.Length;
                s_data[s_data.Length - 2] = 0x0d;
                s_data[s_data.Length - 1] = 0x0a;
                int cout = 0;
                foreach (var tp in data)
                {

                    s_data[7 + cout] = tp;
                    cout++;
                }
                this.Write(s_data, 0, s_data.Length);
                return 1;
            }
            catch {

                return -1;
            }

        }
        public static byte[] fram_id_zhuanhuan(UInt32 id, bool is_kuozhanzhen = false, bool is_yuancheng_zhen=false)
        {

            if (is_kuozhanzhen)
            {
                if (is_yuancheng_zhen)
                {

                    string tmp = Convert.ToString(id, 2).PadLeft(29, '0');


                   
                    tmp = tmp + "1" + "1" + "0";
                    UInt32 tmp_int = Convert.ToUInt32(tmp, 2);
                    return BitConverter.GetBytes(tmp_int).Reverse().ToArray();

                }
                else
                {


                    string tmp = Convert.ToString(id, 2).PadLeft(29, '0');


                    tmp = tmp + "1" + "0" + "0";
                    Console.WriteLine(tmp);
                    UInt32 tmp_int = Convert.ToUInt32(tmp, 2);


                    return BitConverter.GetBytes(tmp_int).Reverse().ToArray();
                    ;
                }


            }
            else
            {


                if (is_yuancheng_zhen)
                {

                    string tmp = Convert.ToString(id, 2).PadLeft(29, '0');


                    string id_31_21 = tmp.Substring(tmp.Length - 11);
                    string id_20_3 = "000000000000000000";


                    tmp = id_31_21 + id_20_3 + "0" + "1" + "0";

                    UInt32 tmp_int = Convert.ToUInt32(tmp, 2);
                    return BitConverter.GetBytes(tmp_int).Reverse().ToArray();
                    ;

                }
                else
                {

                    string tmp = Convert.ToString(id, 2).PadLeft(29, '0');


                    string id_31_21 = tmp.Substring(tmp.Length-11, 11);
                    string id_20_3 = "000000000000000000";


                    tmp = id_31_21 + id_20_3 + "0" + "0" + "0";

                    UInt32 tmp_int = Convert.ToUInt32(tmp, 2);
                    return BitConverter.GetBytes(tmp_int).Reverse().ToArray();
                    ;
                }







            }



        }

        public static (UInt32,byte,byte) bytes2id(byte[] id_bytes) {

            UInt32 tp = BitConverter.ToUInt32(id_bytes,0);

            string m1 = Convert.ToString(id_bytes[0], 2).PadLeft(8, '0') +
                Convert.ToString(id_bytes[1], 2).PadLeft(8, '0') +
                Convert.ToString(id_bytes[2], 2).PadLeft(8, '0') +
                Convert.ToString(id_bytes[3], 2).PadLeft(8, '0');
            int kz_or_bz = int.Parse(m1.Substring(29, 1));
            int bd_or_yc = int.Parse(m1.Substring(30, 1));
            uint id = 0;
            if (kz_or_bz == 1)
            {
                id = Convert.ToUInt32(m1.Substring(0, 29), 2);

            }
            else {

                id= Convert.ToUInt32(m1.Substring(0, 11), 2);

            }

           
          

            return (id, (byte)kz_or_bz, (byte)bd_or_yc);
        }

        ~weite_ttl_can()
        {
            base.DataReceived -= Do_DataReceived;
            this.Close();

        }

    }










}



