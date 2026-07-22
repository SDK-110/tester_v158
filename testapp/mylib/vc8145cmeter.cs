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
    class vc8145cmeter : SerialPort
    {

        public vc8145cmeter(string port, int baudrate=9600) : base(port)
        {


            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            // base.DataReceived += Relay_aputus_DataReceived;
            base.ReadTimeout = 2000;
            base.WriteTimeout = 2000;
            base.Open();
            //try
            //{
            //    base.DiscardInBuffer();
            //    base.WriteLine("#*ONL");
            //    base.ReadLine();
            //}catch(Exception){

                
            //}

        }

        private void Relay_aputus_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            //   recebuf = sp.ReadExisting();
        }

        public float read_dcv(string range, int slow=1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "200MV")
            {

                this.WriteLine("#*INS00" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "2V")
            {
           

                    this.WriteLine("#*INS01" + setfast);
                    try
                    {
                        resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                        resp = this.ReadLine();
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                        return float.Parse(reg[0].ToString());
                    }
                    catch (Exception)
                    {


                        return (float)-1000000;
                    }

                }
                else if (range.ToUpper() == "20V")
                {
                    this.WriteLine("#*INS02" + setfast);
                    try
                    {
                        resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                        resp = this.ReadLine();
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                        return float.Parse(reg[0].ToString());
                    }
                    catch (Exception)
                    {


                        return (float)-1000000;
                    }

                }
                else if (range.ToUpper() == "200V")
                {
                    this.WriteLine("#*INS03" + setfast);
                    try
                    {
                        resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                        resp = this.ReadLine();
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                        return float.Parse(reg[0].ToString());
                    }
                    catch (Exception)
                    {


                        return (float)-1000000;
                    }

                }
                else if (range.ToUpper() == "1000V")
                {
                    this.WriteLine("#*INS04" + setfast);
                    try
                    {
                        resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                        resp = this.ReadLine();
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                        return float.Parse(reg[0].ToString());
                    }
                    catch (Exception)
                    {


                        return (float)-1000000;
                    }

                }
                else
                {
                    this.WriteLine("#*INS04" + setfast);
                    try
                    {
                        resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                        resp = this.ReadLine();
                        MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                        if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                        return float.Parse(reg[0].ToString());
                    }
                    catch (Exception)
                    {


                        return (float)-1000000;
                    }



                }

            
        }

        public float read_dci(string range, int slow=1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "0.2MA")
            {

                this.WriteLine("#*INS10" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "2MA")
            {


                this.WriteLine("#*INS11" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "20MA")
            {
                this.WriteLine("#*INS12" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "200MA")
            {
                this.WriteLine("#*INS13" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "10A")
            {
                this.WriteLine("#*INS14" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else
            {
                this.WriteLine("#*INS14" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }


        }

        public float read_ohm(string range, int slow=1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "200R")
            {

                this.WriteLine("#*INS20" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "2K")
            {


                this.WriteLine("#*INS21" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "20K")
            {
                this.WriteLine("#*INS22" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "200K")
            {
                this.WriteLine("#*INS23" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "2M")
            {
                this.WriteLine("#*INS24" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "20M")
            {
                this.WriteLine("#*INS25" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "60M")
            {
                this.WriteLine("#*INS26" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else
            {
                this.WriteLine("#*INS25" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }


        }
        public float read_acv(string range, int slow = 1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "200MV")
            {

                this.WriteLine("#*INS30" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "2V")
            {


                this.WriteLine("#*INS31" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "20V")
            {
                this.WriteLine("#*INS32" + setfast);
                
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);

                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "200V")
            {
                this.WriteLine("#*INS33" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "1000V")
            {
                this.WriteLine("#*INS34" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else
            {
                this.WriteLine("#*INS04" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }


        }

        public float read_aci(string range, int slow = 1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "0.2MA")
            {

                this.WriteLine("#*INS40" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "2MA")
            {


                this.WriteLine("#*INS41" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "20MA")
            {
                this.WriteLine("#*INS42" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "200MA")
            {
                this.WriteLine("#*INS43" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "10A")
            {
                this.WriteLine("#*INS44" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else
            {
                this.WriteLine("#*INS44" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }


        }

        public float read_freq(string range, int slow = 1)
        {
            this.DiscardInBuffer();

            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "10HZ")
            {

                this.WriteLine("#*INS50" + setfast);

                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "100HZ")
            {


                this.WriteLine("#*INS51" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "1000HZ")
            {
                this.WriteLine("#*INS52" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "10KHZ")
            {
                this.WriteLine("#*INS53" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "100KHZ")
            {
                this.WriteLine("#*INS54" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "1000KHZ")
            {
                this.WriteLine("#*INS55" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "10MHZ")
            {
                this.WriteLine("#*INS56" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
            
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else
            {
                this.WriteLine("#*INS55" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }


        }

        public float read_cap(string range, int slow = 1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

            if (range.ToUpper() == "10NF")
            {

                this.WriteLine("#*INS70" + setfast);

                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
            else if (range.ToUpper() == "100NF")
            {


                this.WriteLine("#*INS71" + setfast);
                try
                {
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "1000NF")
            {
                this.WriteLine("#*INS72" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(750);
                   resp= this.ReadTo("\n");
                 
                   System.Threading.Thread.Sleep(750);
                    this.WriteLine("#*RD?");
            
                    resp = this.ReadTo("\n");
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "10UF")
            {
                this.WriteLine("#*INS73" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "100UF")
            {
                this.WriteLine("#*INS74" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }

            }
            else if (range.ToUpper() == "1000UF")
            {
                this.WriteLine("#*INS75" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                   System.Threading.Thread.Sleep(1500);
                    this.DiscardInBuffer();
                    this.WriteLine("#*RD?");
                   // System.Threading.Thread.Sleep(1000);
                    resp = this.ReadLine();

                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

              
                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }
            else
            {
                this.WriteLine("#*INS73" + setfast);
                try
                {
                    System.Threading.Thread.Sleep(1500);
                    resp = this.ReadLine();
                    System.Threading.Thread.Sleep(1500);
                    this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }



            }
        }

        public float read_diode( int slow = 1)
        {
            this.DiscardInBuffer();
            string setfast = "";
            string resp = "";
            if (slow == 1) { setfast = "1"; }
            else
            {
                setfast = "0";

            }

                this.WriteLine("#*INS80" + setfast);

                try
                {
                    resp = this.ReadLine();
                System.Threading.Thread.Sleep(500);
                this.WriteLine("#*RD?");
                    resp = this.ReadLine();
                    MatchCollection reg = new Regex(@"(-?\d+)(\.\d+)|(-?\d+)(\d+)").Matches(resp);
                    if (reg.Count == 0) { throw new Exception("返回的数值不是浮点数"); }

                    return float.Parse(reg[0].ToString());
                }
                catch (Exception)
                {


                    return (float)-1000000;
                }





            }
           
        
        ~vc8145cmeter()
        {
            this.Close();
        }
    }
}

