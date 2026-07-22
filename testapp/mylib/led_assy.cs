using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace testapp
{
    class led_assy : SerialPort
    {

        public string led_return_buff_4debug="";
        string recebuf;
        public led_assy(string port, int baudrate=57600) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = false;
            // base.DataReceived += Relay_DataReceived;
            base.DtrEnable = false;
            base.WriteTimeout = 2000;
            base.ReadTimeout = 2000;
            base.Open();
        }

        private void Relay_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            recebuf = sp.ReadExisting();
        }
        #region /*废弃*/
        public void send(byte[] m)
        {

            this.Write(m, 0, m.Length);
        }

        public string read()
        {

            string a = (recebuf == null) ? "null" : recebuf;
            recebuf = "";
            return a;
        }

        private void set_rly(byte[] m)
        {

            if (m.Length == 3)
            {

                this.send(new Byte[] { 0XB1, 0X03 });
                this.send(m);
                this.send(new Byte[] { 0X0A });

            }

        }
        private void getcolor(byte ch,byte cor) {

            this.send(new Byte[] {0xA5,0X04 });

            this.send(new byte[] { ch, cor });
            this.send(new byte[] { 0x00, 0x00, 0x0A });


        }

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
        #endregion



        public void try_comm() {

           this.Write("GETSERIAL" + '\r');

            string m = this.ReadLine();


        }

        public int[] getRGBI(int port = 1,int trytimes=2,int level=3) {

            int[] ctsreaddata = new int[] { -2, -2, -2, -2};
            int count = 0;
            if (!this.IsOpen) this.Open();
                this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine($"capture{level:D}");

                    try
                    {

                        this.ReadLine();


                    }
                    catch
                    {

                        continue;
                    }

                    //System.Threading.Thread.Sleep(430);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                    string ret = "";
                    try
                    {

                        ret = this.ReadLine();

                    }
                    catch
                    {

                        continue;
                    }


                    // System.Threading.Thread.Sleep(50);
                    //  }

                    //  string ret = this.ReadExisting();
                    mylib.utility_func.callbackdebuginfo("led analytor meg: \n" + ret);
                    Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                    MatchCollection matchs = rex.Matches(ret);
                    for (int t = 0; t < matchs.Count; t++)
                    {
                        if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                        if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                        if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                        if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                    }
                    if (ctsreaddata[0] >= 0 || ctsreaddata[1] >= 0 || ctsreaddata[2] >= 0 || ctsreaddata[3] >= 0) break;
                   
                } 


                return ctsreaddata;

            

        }
        public int[] getRGBIFast(int port = 1, int trytimes = 2)
        {

            int[] ctsreaddata = new int[] { -3, -3, -3, -3 };
            int count = 0;
            if (!this.IsOpen) this.Open();
            this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture1");
                try
                {

                    this.ReadLine();
                }
                catch {

                    continue;
                }
                //    System.Threading.Thread.Sleep(100);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                // System.Threading.Thread.Sleep(50);

                string ret = "";

                try
                {

                    ret = this.ReadLine();
                }
                catch {

                    continue;
                }
               // string ret = this.ReadExisting();
               // mylib.utility_func.callbackdebuginfo(ret);
                Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if (ctsreaddata[0] > 0 || ctsreaddata[1] > 0 || ctsreaddata[2] > 0 || ctsreaddata[3] > 0) break;
              

            }

            return ctsreaddata;



        }
        public int[] getRGBIFast_zero(int port = 1, int trytimes = 2)
        {
            if (!this.IsOpen) this.Open();
            int[] ctsreaddata = new int[] { -1, -1, -1, -1 };
            int count = 0;
            do
            {
                this.ReadExisting();

                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture1");
                    System.Threading.Thread.Sleep(100);

                    this.WriteLine("getrgbi" + $"{port:D2}");
                    System.Threading.Thread.Sleep(50);
                }

                string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] < int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] < int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] < int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] < int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if (ctsreaddata[0] >= 0 || ctsreaddata[1] >= 0 || ctsreaddata[2] >= 0 || ctsreaddata[3] >= 0 || count > 3) break;
                count++;
            } while (true);


            return ctsreaddata;



        }



        public int[] getRGBI_Min(int port = 1, int trytimes =2)
        {
            if (!this.IsOpen) this.Open();
            int[] ctsreaddata = new int[] { 50, 50, 50, 300 };
            int count = 0;
            do
            {
                this.ReadExisting();
                string ret = "";
                for (int i = 0; i < trytimes; i++)
                {

                    this.WriteLine("capture2");
                    try
                    {
                        this.ReadLine();
                    }
                    catch {

                        continue;
                    }
                 //   System.Threading.Thread.Sleep(150);

                    
                    this.WriteLine("getrgbi" + $"{port:D2}");
                    //  System.Threading.Thread.Sleep(150);

                    try
                    {
                        ret=   this.ReadLine();
                    }
                    catch
                    {

                        continue;
                    }

                  //  ret  = this.ReadExisting();
                 mylib.utility_func.callbackdebuginfo("read value:\n" + ret);
                }
                Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
              
                for (int t = 0; t < matchs.Count; t++)
                {
                    if (ctsreaddata[0] > int.Parse(matchs[t].Groups[1].Value)) ctsreaddata[0] = int.Parse(matchs[t].Groups[1].Value);
                    if (ctsreaddata[1] > int.Parse(matchs[t].Groups[2].Value)) ctsreaddata[1] = int.Parse(matchs[t].Groups[2].Value);
                    if (ctsreaddata[2] > int.Parse(matchs[t].Groups[3].Value)) ctsreaddata[2] = int.Parse(matchs[t].Groups[3].Value);
                    if (ctsreaddata[3] > int.Parse(matchs[t].Groups[4].Value)) ctsreaddata[3] = int.Parse(matchs[t].Groups[4].Value);


                }
                if ((ctsreaddata[0] < 50 && ctsreaddata[1] < 50 && ctsreaddata[2] < 50 && ctsreaddata[3] < 300 )|| count > 3) break;
                count++;
            } while (true);


            return ctsreaddata;



        }




        public int get_status(int[] lighthilimit, int[] lightlowlimit, int times, int led_number, out int [] ind)
        {

              ind = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
            if (!this.IsOpen) this.Open();
            for (int lop = 0; lop < times; lop++)
            {

                int[] ctsreaddata = new int[] { -1, -1, -1, -1 };
                this.ReadExisting();
                this.WriteLine("capture3");
                try
                {

                    this.ReadLine();
                }
                catch { }
                //System.Threading.Thread.Sleep(450);
                this.WriteLine("getallrgbi");

                string ret = "";

                try
                {

                    ret =   this.ReadLine();
                }
                catch { }
               // System.Threading.Thread.Sleep(150);
             //   string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(ret);
                if (matchs.Count < led_number) {  continue; }
             

                    ctsreaddata[0] = int.Parse(matchs[led_number-1].Groups[1].Value);
                    ctsreaddata[1] = int.Parse(matchs[led_number - 1].Groups[2].Value);
                    ctsreaddata[2] = int.Parse(matchs[led_number - 1].Groups[3].Value);
                    ctsreaddata[3] = int.Parse(matchs[led_number - 1].Groups[4].Value);
                    testapp.mylib.utility_func.callbackdebuginfo("" + ctsreaddata[0] + "," + ctsreaddata[1] + "," + ctsreaddata[2] + "," + ctsreaddata[3]);

                    if (ctsreaddata[0] <= lighthilimit[0] &&
                        ctsreaddata[1] <= lighthilimit[1] &&
                        ctsreaddata[2] <= lighthilimit[2] &&
                        ctsreaddata[3] <= lighthilimit[3] &&
                        ctsreaddata[0] >= lightlowlimit[0] &&
                        ctsreaddata[1] >= lightlowlimit[1] &&
                        ctsreaddata[2] >= lightlowlimit[2] &&
                        ctsreaddata[3] >= lightlowlimit[3]

                        ) {


                        ///

                        ind[led_number - 1] += 1;
                    }
                   

            }
            return ind[led_number - 1];
        }


        public int get_status_together(string limitstr, out int[] ind)
        {

            if (!this.IsOpen) this.Open();
            string[] str_top1 = limitstr.Split("%".ToArray());
            ind = new int[str_top1.Length];
            
            int [] led_numbers = new int[str_top1.Length];
            int[,] limits  = new int[str_top1.Length,8];
            int xunhuan = 0; 

            for (int jzloop = 0; jzloop < str_top1.Length; jzloop++) {
                string[] str_top2 = str_top1[jzloop].Split(";".ToArray());
                if (str_top2.Length != 10) return -1;
                led_numbers[jzloop] = int.Parse(str_top2[0].Trim());
                limits[jzloop, 0] = int.Parse(str_top2[1].Trim());
                limits[jzloop, 1] = int.Parse(str_top2[2].Trim());
                limits[jzloop, 2] = int.Parse(str_top2[3].Trim());
                limits[jzloop, 3] = int.Parse(str_top2[4].Trim());
                limits[jzloop, 4] = int.Parse(str_top2[5].Trim());
                limits[jzloop, 5] = int.Parse(str_top2[6].Trim());
                limits[jzloop, 6] = int.Parse(str_top2[7].Trim());
                limits[jzloop, 7] = int.Parse(str_top2[8].Trim());
                int max_xunhuan = int.Parse(str_top2[9].Trim());
                xunhuan = (xunhuan > max_xunhuan) ? xunhuan : max_xunhuan;

            }

            this.WriteLine("capture3");
            try
            {

                this.ReadLine();
            }
            catch { }
            StringBuilder mbuf = new StringBuilder();
            for (int xh = 0; xh < xunhuan; xh++)
            {

               
                int[] ctsreaddata = new int[] { -1, -1, -1, -1 };
                MatchCollection matchs = null;
              //  for (int tryt = 0; tryt < 2; tryt++)
              //  {
                this.ReadExisting();
           
               // System.Threading.Thread.Sleep(450);
                this.WriteLine("getallrgbi");

                string ret = "";
                try
                {

                    ret = this.ReadLine();
                }
                catch { }

                //System.Threading.Thread.Sleep(150);
                //string ret = this.ReadExisting();
                Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                matchs = rex.Matches(ret);
              
                if (matchs.Count < led_numbers.Max()) { return -1; }
             //   }
                if (matchs == null) return -1;
                for (int lop = 0; lop < str_top1.Length; lop++)
                {

                    


                    ctsreaddata[0] = int.Parse(matchs[led_numbers[lop] - 1].Groups[1].Value);
                    ctsreaddata[1] = int.Parse(matchs[led_numbers[lop] - 1].Groups[2].Value);
                    ctsreaddata[2] = int.Parse(matchs[led_numbers[lop] - 1].Groups[3].Value);
                    ctsreaddata[3] = int.Parse(matchs[led_numbers[lop] - 1].Groups[4].Value);

                   
                   // testapp.mylib.utility_func.callbackdebuginfo("[" + (led_numbers[lop]) + "] =>" + ctsreaddata[0] + "," + ctsreaddata[1] + "," + ctsreaddata[2] + "," + ctsreaddata[3]);

                  //  testapp.mylib.utility_func.callbackdebuginfo("[" + limits[lop, 0] + "," + limits[lop, 1] + "," + limits[lop, 2] + "," + limits[lop, 3] + "<=>" +
                    //limits[lop, 4] + "," + limits[lop, 5] + "," + limits[lop, 6] + "," + limits[lop, 7] + "]");


                    mbuf.AppendLine("[" + (led_numbers[lop]) + "] =>" + ctsreaddata[0] + "," + ctsreaddata[1] + "," + ctsreaddata[2] + "," + ctsreaddata[3]);
                    mbuf.AppendLine("[" + limits[lop, 0] + "," + limits[lop, 1] + "," + limits[lop, 2] + "," + limits[lop, 3] + "<=>" +
                    limits[lop, 4] + "," + limits[lop, 5] + "," + limits[lop, 6] + "," + limits[lop, 7] + "]");

                    if (ctsreaddata[0] <= limits[lop, 0] &&ctsreaddata[1] <= limits[lop, 1] && ctsreaddata[2] <= limits[lop, 2] &&
                        ctsreaddata[3] <= limits[lop, 3] && ctsreaddata[0] >= limits[lop, 4] &&ctsreaddata[1] >= limits[lop, 5] &&
                        ctsreaddata[2] >= limits[lop, 6] && ctsreaddata[3] >= limits[lop, 7] )
                    {


                   
                        ind[lop] += 1;
                    }

               

                }

                string debugstr = "";
                for (int p = 0; p < ind.Length; p++)
                {

                    debugstr = debugstr + ind[p] + " ;";

                }
                mbuf.AppendLine(debugstr);
               
            }

            testapp.mylib.utility_func.callbackdebuginfo(mbuf.ToString());
            return 1;
        }

        public int get_status_together_t(string limitstr, out int[] ind)
        {

            if (!this.IsOpen) this.Open();
            string[] str_top1 = limitstr.Split("%".ToArray());
            ind = new int[str_top1.Length];

            int[] led_numbers = new int[str_top1.Length];
            int[,] limits = new int[str_top1.Length, 8];
            int xunhuan = 0;

            for (int jzloop = 0; jzloop < str_top1.Length; jzloop++)
            {
                string[] str_top2 = str_top1[jzloop].Split(";".ToArray());
                if (str_top2.Length != 10) return -1;
                led_numbers[jzloop] = int.Parse(str_top2[0].Trim());
                limits[jzloop, 0] = int.Parse(str_top2[1].Trim());
                limits[jzloop, 1] = int.Parse(str_top2[2].Trim());
                limits[jzloop, 2] = int.Parse(str_top2[3].Trim());
                limits[jzloop, 3] = int.Parse(str_top2[4].Trim());
                limits[jzloop, 4] = int.Parse(str_top2[5].Trim());
                limits[jzloop, 5] = int.Parse(str_top2[6].Trim());
                limits[jzloop, 6] = int.Parse(str_top2[7].Trim());
                limits[jzloop, 7] = int.Parse(str_top2[8].Trim());
                int max_xunhuan = int.Parse(str_top2[9].Trim());
                xunhuan = (xunhuan > max_xunhuan) ? xunhuan : max_xunhuan;

            }

            if (xunhuan > 5) xunhuan = 5;
                int[] ctsreaddata = new int[] { -1, -1, -1, -1 };
                MatchCollection matchs = null;
                //  for (int tryt = 0; tryt < 2; tryt++)
                //  {
                this.ReadExisting();
                this.WriteLine("getallfreq" + xunhuan);

                string ret = "";
                try
                {

                    ret = this.ReadLine();
                }
                catch { }

            //System.Threading.Thread.Sleep(150);
            //string ret = this.ReadExisting();
            Regex rex = new Regex(@"\d{2}\.\d\s\d{3}\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s\d{3}\.\d{2}\s([0-9]{1,5})", RegexOptions.IgnoreCase);
            matchs = rex.Matches(ret);
            if (matchs.Count < led_numbers.Max()) { return -1; }
                //   }
                if (matchs == null) return -1;
                for (int lop = 0; lop < str_top1.Length; lop++)
                {




                    ctsreaddata[0] = int.Parse(matchs[led_numbers[lop] - 1].Groups[1].Value);
                    ctsreaddata[1] = int.Parse(matchs[led_numbers[lop] - 1].Groups[2].Value);
                    ctsreaddata[2] = int.Parse(matchs[led_numbers[lop] - 1].Groups[3].Value);
                    ctsreaddata[3] = int.Parse(matchs[led_numbers[lop] - 1].Groups[4].Value);
                    testapp.mylib.utility_func.callbackdebuginfo("[" + (led_numbers[lop]) + "] =>" + ctsreaddata[0] + "," + ctsreaddata[1] + "," + ctsreaddata[2] + "," + ctsreaddata[3]);

                    testapp.mylib.utility_func.callbackdebuginfo("[" + limits[lop, 0] + "," + limits[lop, 1] + "," + limits[lop, 2] + "," + limits[lop, 3] + "<=>" +
                    limits[lop, 4] + "," + limits[lop, 5] + "," + limits[lop, 6] + "," + limits[lop, 7] + "]");
                    if (ctsreaddata[0] <= limits[lop, 0] && ctsreaddata[1] <= limits[lop, 1] && ctsreaddata[2] <= limits[lop, 2] &&
                        ctsreaddata[3] <= limits[lop, 3] && ctsreaddata[0] >= limits[lop, 4] && ctsreaddata[1] >= limits[lop, 5] &&
                        ctsreaddata[2] >= limits[lop, 6] && ctsreaddata[3] >= limits[lop, 7])
                    {



                        ind[lop] += 1;
                    }



                }

                string debugstr = "";
                for (int p = 0; p < ind.Length; p++)
                {

                    debugstr = debugstr + ind[p] + " ;";

                }

                testapp.mylib.utility_func.callbackdebuginfo("[" + debugstr + "]");
            


            return 1;
        }

        public int getnumber(int[] darkhilimit , int[] darklowlimit, int[] lighthilimit, int[] lightlowlimit, int[] leddef) {
            if (!this.IsOpen) this.Open();
            try
            {
                led_return_buff_4debug = "";
                for (int lop = 0; lop < 3; lop++)
                {
                    this.ReadExisting();
                    this.WriteLine("capture2");
                    System.Threading.Thread.Sleep(150);
                    int[] ctsreaddata = new int[] { -1, -1, -1, -1 };
                    int[] judbuf = new int[] { -1, -1, -1, -1, -1, -1, -1, -1 };
                
                        this.WriteLine("getallrgbi");
                        System.Threading.Thread.Sleep(300);
                      string ret = this.ReadExisting();
                    // System.Windows.Forms.MessageBox.Show("888" + ret);
                    //for (int i = 0; i < leddef.Length; i++)
                    //{
                    led_return_buff_4debug = ret;
                    Regex rex = new Regex(@"([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,3})\s([0-9]{1,5})", RegexOptions.IgnoreCase);
                        MatchCollection matchs = rex.Matches(ret);
                    if (matchs.Count < 8) continue;
                    for (int i = 0; i < leddef.Length; i++)
                    {


                   
                      
                            ctsreaddata[0] = int.Parse(matchs[leddef[i]-1].Groups[1].Value);
                            ctsreaddata[1] = int.Parse(matchs[leddef[i]-1].Groups[2].Value);
                            ctsreaddata[2] = int.Parse(matchs[leddef[i]-1].Groups[3].Value);
                            ctsreaddata[3] = int.Parse(matchs[leddef[i]-1].Groups[4].Value);
                        
                        int judtmp = 0;
                    


                        if (ctsreaddata[0] <= lighthilimit[0] &&
                            ctsreaddata[1] <= lighthilimit[1] &&
                            ctsreaddata[2] <= lighthilimit[2] &&
                            ctsreaddata[3] <= lighthilimit[3] &&
                            ctsreaddata[0] >= lightlowlimit[0] &&
                            ctsreaddata[1] >= lightlowlimit[1] &&
                            ctsreaddata[2] >= lightlowlimit[2] &&
                            ctsreaddata[3] >= lightlowlimit[3]

                            ) { judtmp = 4; goto jjjj; }

                        if (ctsreaddata[0] <= darkhilimit[0] &&
                           ctsreaddata[1] <= darkhilimit[1] &&
                           ctsreaddata[2] <= darkhilimit[2] &&
                           ctsreaddata[3] <= darkhilimit[3] &&
                           ctsreaddata[0] >= darklowlimit[0] &&
                           ctsreaddata[1] >= darklowlimit[1] &&
                           ctsreaddata[2] >= darklowlimit[2] &&
                           ctsreaddata[3] >= darklowlimit[3]

                          ) { judtmp = 3; goto jjjj; }

                        judtmp = 2;

                    jjjj:

                       judbuf[i] = judtmp;
                        led_return_buff_4debug = led_return_buff_4debug + "数码管:" + (i+1) +  judtmp + "\r\n";
                        //   System.Windows.Forms.MessageBox.Show("----" + judbuf[i]);

                    }

                    if (judbuf[0] == 2 || judbuf[1] == 2 || judbuf[2] == 2 || judbuf[3] == 2 || judbuf[4] == 2 || judbuf[5] == 2 || judbuf[6] == 2) continue;

                    if (judbuf[0] == 4 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 4 && judbuf[4] == 4 && judbuf[5] == 4 && judbuf[6] == 3) return 0;
                    if (judbuf[0] == 3 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 3 && judbuf[4] == 3 && judbuf[5] == 3 && judbuf[6] == 3) return 1;
                    if (judbuf[0] == 4 && judbuf[1] == 4 && judbuf[2] == 3 && judbuf[3] == 4 && judbuf[4] == 4 && judbuf[5] == 3 && judbuf[6] == 4) return 2;
                    if (judbuf[0] == 4 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 4 && judbuf[4] == 3 && judbuf[5] == 3 && judbuf[6] == 4) return 3;
                    if (judbuf[0] == 3 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 3 && judbuf[4] == 3 && judbuf[5] == 4 && judbuf[6] == 4) return 4;
                    if (judbuf[0] == 4 && judbuf[1] == 3 && judbuf[2] == 4 && judbuf[3] == 4 && judbuf[4] == 3 && judbuf[5] == 4 && judbuf[6] == 4) return 5;
                    if (judbuf[0] == 3 && judbuf[1] == 3 && judbuf[2] == 4 && judbuf[3] == 4 && judbuf[4] == 4 && judbuf[5] == 4 && judbuf[6] == 4) return 6;
                    if (judbuf[0] == 4 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 3 && judbuf[4] == 3 && judbuf[5] == 3 && judbuf[6] == 3) return 7;
                    if (judbuf[0] == 4 && judbuf[1] == 4 && judbuf[2] == 4 && judbuf[3] == 4 && judbuf[4] == 4 && judbuf[5] == 4 && judbuf[6] == 4) return 8;


                }

                return -1;
            }
            catch {

                return -2;
            }

        }

        /// <summary>
        /// /*1為DTR PIN4,2為RTS PIN 7*/ ,true = high
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="setval"></param>
        public void setpinstatus(int pin /*1為DTR PIN4,2為RTS PIN 7*/, bool setval)
        {
            if (pin == 1) this.DtrEnable = setval;
            if (pin == 2) this.RtsEnable = setval;




        }


        public int get_all_channel_data(ref int[] datarsu, int try_times = 1) {


            if (!this.IsOpen) this.Open();
            datarsu = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                  -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                  -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                                   -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
            };

            for (int lop = 0; lop < try_times; lop++)
            {


                this.ReadExisting();
                this.WriteLine("capture3");
                try
                {

                    this.ReadLine();
                }
                catch { return -3; }
                //System.Threading.Thread.Sleep(450);
                this.WriteLine("getallrgbi");

                string ret = "";

                try
                {

                    ret = this.ReadLine().Trim();

                    string[] tmp1 = ret.Split(" ".ToArray());
                    if (tmp1.Length != 80 && lop == try_times - 1) return -1;

                    for (int i = 0; i < 20*4 ; i++) {


                        datarsu[i] = int.Parse(tmp1[i].Trim());
                    }

                    return 1;
                }
                catch (Exception e){
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                    return -2;
                }

                
            }

            return -4;
        }
        #region 知识储备库忽略
        /*
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
                // crc校验函数
                private   UInt16 crc16(Byte[] ptr)
                { return ModbusCrc16.Compute(ptr); }

                private  Byte[] tan_modbus(Byte[] data)
                { return ModbusCrc16.AppendCrc(data); }
        */
        #endregion
        ~led_assy()
        {
            this.Close();
        }

    }

}

