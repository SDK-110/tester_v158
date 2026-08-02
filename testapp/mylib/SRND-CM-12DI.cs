using IniParser;


using Org.BouncyCastle.Utilities;


using System;


using System.Collections.Generic;


using System.IO;


using System.IO.Ports;


using System.Linq;


using System.Text;


using System.Threading;


using System.Threading.Tasks;


using testapp.glob_set;


using Vila.Win32;





namespace testapp


{


    class SRND_CM_12DI : SerialPort


    {


      


        string app_text = "";


        private IntPtr ptrWnd;


        public const int USER = 0x0400;


        public const int WM_SEND_AUTOTEST = USER + 113;


        string recebuf;


        private bool is_set_timer;


        static Timer _timer;


        static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();


        volatile byte[] rsubyt = new byte[200];


        int[] ch_flog = new int[20];


        volatile int rev_count = 0;


        private string ch_info;


        private int is_done;





        public SRND_CM_12DI(string port, int baudrate=9600) : base(port)


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


            app_text = glob_ini_instance.getInstance().getSetupIniData["setproduct"]["name"];





            base.Open();


        








        }


        public int read_DI(int chinnel, out int rsult) {


            is_done = 0;


            _read_DI(chinnel, out rsult);


            int loopcout = 30;


            while (is_done != 1 && loopcout-- >= 0)


            {





                System.Threading.Thread.Sleep(50);


            }





            if (is_done == 1)


            {


               


                    string tmp = ch_info.Substring(ch_info.Length - chinnel - 1, 1);


                    rsult = int.Parse(tmp);


              


                return 1;


            }


            else {








                rsult = int.MaxValue;





                return -1;





            }


            





        }





        public int read_DI(int start_ch, out string rsult)


        {


            is_done = 0;


            _read_DI(1, out _);


            int loopcout = 30;


            while (is_done != 1 && loopcout-- >= 0)


            {





                System.Threading.Thread.Sleep(50);


            }





            if (is_done == 1)


            {





                rsult = ch_info.Substring((start_ch)<8? (ch_info.Length - 8):(ch_info.Length - start_ch), 8);





                return 1;


            }


            else


            {








                rsult = "ERROR";





                return -1;





            }








        }


        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)


        {


            try


            {


                SerialPort sp = (SerialPort)sender;


                System.Threading.Thread.Sleep(50);





                int m = sp.BytesToRead;





                byte[] tmp = new byte[m];


                sp.Read(tmp, 0, m);


                Array.Copy(tmp, rsubyt, m);


                rev_count = m;


            }


            catch { }


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





        private int _read_DI(int chinnel, out int rsult) {


            rev_count = 0;


            rsult = -1;


            is_done = 0;


            if (!this.IsOpen) this.Open();


            this.ReadExisting();


            Array.Clear(rsubyt, 0, 200);


            byte[] readstr = { 0x01, 0x02, 0x00, 0x00, 0x00, 0x17, 0, 0 };


            readstr[6] = (byte)(crc16(new ArraySegment<byte>(readstr, 0, 6).ToArray()) % 256);


            readstr[7] = (byte)(crc16(new ArraySegment<byte>(readstr, 0, 6).ToArray()) / 256);


            this.Write(readstr, 0, readstr.Length);


            int loopcout = 10;


            while (rev_count == 0 && loopcout-- >= 0) {





                System.Threading.Thread.Sleep(50);


            }


            try


            {


                if (rev_count == 0) return -1;


                if (rev_count > 7 && rsubyt[0] == 0x01 && rsubyt[1] == 0x02 && rsubyt[2] == 0x03)


                {





                    byte[] Channels_data = new ArraySegment<byte>(rsubyt, 3, 3).Reverse().ToArray();


                    ch_info = string.Join("", Channels_data.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));


                    mylib.utility_func.callbackdebuginfo(BitConverter.ToString(Channels_data));


                    string tmp = ch_info.Substring(ch_info.Length - chinnel - 1, 1);


                    rsult = int.Parse(tmp);


                    is_done = 1;


                    return 1;





                }


                else


                {





                    return -2;





                }


                is_done = 3;


            }


            catch


            {


                //  System.Windows.Forms.MessageBox.Show("Test");


                return -1;


            }


        }





        


        // crc校验函数


        private UInt16 crc16(Byte[] ptr)


        { return ModbusCrc16.Compute(ptr); }





        private  Byte[] tan_modbus(Byte[] data)


        { return ModbusCrc16.AppendCrc(data); }





        ~SRND_CM_12DI() {


            if (is_set_timer)


            {


                


                _cancellationTokenSource.Cancel();


                _timer.Dispose();


            }


           


            this.Close();


           


        }





        public void  set_timer() {





            is_set_timer = true;


            if (_timer != null) return;


            _timer = new Timer((o)=> {








                if (_cancellationTokenSource.Token.IsCancellationRequested)


                {


                  


                    return;


                }


               


                _read_DI(0, out _);








            }, null, 0, 500);


        }





    }





}





