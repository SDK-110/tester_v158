using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using Vila.Extensions;
using System.Text.RegularExpressions;
using static testapp.sgw_DN_proj;
using testapp.mylib;
using Windows.Globalization;


namespace testapp
{
  
    class sgw_DN_proj : SerialPort
    {
     public   enum LTE_Band { 
        
            B1=18300,
            B2=18900,
            B3=19575,
            B4=20175,
            B5=20525,
            B8=21625,
            B12=23095,
            B13=23230,
            B14=23300,
            B17=23790,
            B18=23925,
            B20=24300,
            B25=26365,
            B26=26965,
            B28=27435,
            B66=132322,
            B71=133297,
            B85=134092
        
        }
        public double energy_meter_voltage, energy_meter_current, energy_meter_powr;
        string recebuf;
       public glob_data glob_Data=new glob_data();
        volatile  byte[] rsubyt = new byte[200];
        string golb_pp = "";
        volatile int rev_count = 0;
        public sgw_DN_proj(string port, int baudrate=115200) : base(port)
        {
           
       
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
           // base.DataReceived += Relay_DataReceived;
            base.ReadTimeout = 8000;
            base.WriteTimeout = 1000;
            //base.NewLine = "\r";
            //base.ReceivedBytesThreshold = 1;
          



        }

        public string dn_qurey(string command,out int status) {


            if (DN_writeline(command) == 1)
            {
             status=1;
                return dn_readline();
            }
            else {

                status = 0;
                return "status error";
            }
        
        
        }

        public int DN_writeline(string command)
        {
            try
            {
               if(!this.IsOpen)this.Open();
                this.ReadExisting();
                this.WriteLine(command);
                mylib.utility_func.callbackdebuginfo("send command :" + command);
                return 1;
            }

            catch { 
            
            return -1;
            }

        }
        public int dn_serial_open_close(int status) {


            try
            {

                if (status == 0)
                {

                    if (this.IsOpen) { this.Close(); }

                    return 1;
                }
                else {

                    if (!this.IsOpen) { this.Open(); }

                    return 1;
                }
               
            }

            catch
            {
                mylib.utility_func.callbackdebuginfo("dn_serial_open_close error!");
                return -1;
            }


        }

        public string dn_readline()
        {
            try
            {
                if (!this.IsOpen) this.Open();
                string rs = this.ReadLine();
                mylib.utility_func.callbackdebuginfo("rev command :" + rs);
                this.Close();
                return rs;

            }
            catch {
                mylib.utility_func.callbackdebuginfo("time_out_error");
                return "time_out_error";
            
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

                golb_pp = pp;
            }
            Array.Copy(tmp, rsubyt, m);
            rev_count = m;
        }

        public string read_imei(out int status)
        {
            try {
                mylib.utility_func.callbackdebuginfo("send comm: read IMEI");
              
                string rsu = "";
                for (int i = 0; i < 10; i++)
                {
                    this.WriteLine("read IMEI");
                    System.Threading.Thread.Sleep(500);
                    try { 
                     if(this.BytesToRead>0) rsu = this.ReadExisting();
                    
                    }catch {  }

                    mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                    Regex regex = new Regex(@"(?<=ok\:)\s{0,1}\d{5,}");
                    //Match match = regex.Match(rsu);
                    MatchCollection matches = regex.Matches(rsu);
                    if (matches.Count >= 1)
                    {
                        status = 1;
                        glob_Data.imei = matches[0].Value.Trim();
                        rsu = matches[0].Value.Trim();
                        return rsu;
                        
                    }


                }

                
        
                
                    status = -1;
                    return rsu.Trim();
                
            }
            catch(Exception ex) {

                mylib.utility_func.callbackdebuginfo("error message:"+ex.Message);
                status = -2;
                return "comm error";
            
            
            }
        
        
        
        }

        public string set_lte_test_flag(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: set_lte_test_flag");

                string rsu = "";
                for (int i = 0; i < 10; i++)
                {
                    this.WriteLine("set_lte_test_flag");
                    System.Threading.Thread.Sleep(500);
                    try
                    {
                        if (this.BytesToRead > 0) rsu = this.ReadExisting();

                    }
                    catch { }

                    mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                    if (rsu.IndexOf("ok") >= 0)
                    {

                        status = 1;
                        rsu = rsu.Trim().Replace("\n", " ").Replace("\r", " ");
                        return rsu;
                    }
                    else {
                        status = 0;
                        rsu = "error";
                    }


                }




                status = -1;
                return rsu.Trim();

            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_iccid(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: read ICCID");
                string rsu = dn_qurey("read ICCID", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_eid(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: read eid");
                string rsu = dn_qurey("read EID", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string read_gd32_ver(out int status)
        {
         

                mylib.utility_func.callbackdebuginfo("read_gd32_ver");

                string rsu = "";
                status = 0;
                for (int i = 0; i < 10; i++)
                {
                    this.WriteLine("read gd32 version");
                    rsu = "";
                    try
                    {

                        System.Threading.Thread.Sleep(1000);
                        rsu = this.ReadExisting().Replace("\\d", " ").Replace("\\r", " ");
                        mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);



                        Regex regex = new Regex(@"(?<=ok\:)\d.\d+.\d+");
                        //Match match = regex.Match(rsu);
                        MatchCollection matches = regex.Matches(rsu);
                        if (matches.Count >= 1)
                        {
                            status = 1;
                            rsu = matches[0].Value;
                            break;
                        }







                    }
                    catch
                    {



                    }


                }


                return rsu;



           


        }
        public string write_board_ver(string bar_ver,out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write board " + bar_ver);
                string rsu = dn_qurey($"write board {bar_ver}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_board_ver( out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read board ");
                string rsu = dn_qurey($"read board", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string write_asset_id(string assetid, out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write asset_id " + assetid);
                string rsu = dn_qurey($"write asset_id {assetid}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_asset_id(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read asset_id");
                string rsu = dn_qurey($"read asset_id", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string write_client_id(string client_id, out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write client_id " + client_id);
                string rsu = dn_qurey($"write client_id {client_id}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_client_id(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read client_id");
                string rsu = dn_qurey($"read client_id", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string write_device_id(string device_id, out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write device_id " + device_id);
                string rsu = dn_qurey($"write device_id {device_id.PadRight(10,' ')}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_device_id(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read device_id");
                string rsu = dn_qurey($"read device_id", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string write_serial_number(string sn, out int status)
        {
            try
            {
                if (!this.IsOpen) this.Open();
                mylib.utility_func.callbackdebuginfo("send comm: write serial_number " + sn);
              

                this.ReadExisting();
                this.Write($"write serial_number {sn}");
                string rsu = "";
                for (int i = 0; i < 2; i++) {
                   
                    System.Threading.Thread.Sleep(500);
                    try
                    {

                        rsu = this.ReadLine();
                        mylib.utility_func.callbackdebuginfo("rev: " + rsu);
                    }
                    catch { }

                    if (rsu.ToUpper().IndexOf("OK") >= 0) {

                        status = 1;
                        return rsu.Trim();
                    
                    }

                    if (rsu.ToUpper().IndexOf("FAIL") >= 0) {

                        if (rsu.ToUpper().IndexOf("OK") >= 0)
                        {

                            status = -1;
                            return rsu.Trim();

                        }
                    }

                }

                status = -3;
                return rsu.Trim() ;
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_serial_number(out int status)
        {
            try
            {
                if (!this.IsOpen) this.Open();
                mylib.utility_func.callbackdebuginfo("send comm:read serial_number");
                string rsu = dn_qurey($"read serial_number", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    glob_Data.sn= rsu.Substring(3).Trim();
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string write_merch_type(string merch_type, out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write merch_type " + merch_type);
                string rsu = dn_qurey($"write merch_type {merch_type}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_merch_type(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read merch_type");
                string rsu = dn_qurey($"read merch_type", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string write_comp_config(string comp_config, out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm: write comp_config " + comp_config);
                string rsu = dn_qurey($"write comp_config {comp_config}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_comp_config(out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo("send comm:read comp_config");
                string rsu = dn_qurey($"read comp_config", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string test_rtc_start(out int status)
        {
            try
            {
               
                mylib.utility_func.callbackdebuginfo("send comm:rtc start");
                string rsu = dn_qurey($"rtc start", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string test_rtc_stop(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:rtc stop");
                string rsu = dn_qurey($"rtc stop", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_firmware(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:read firmware");
               // string rsu = dn_qurey($"read firmware", out status);
                this.WriteLine("read firmware");
                //  string rsu =this.ReadLine();
                string rsu = "";
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting().Trim();
                        break;
                    }
                }

                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_lteip(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:read lteip");
                string rsu = dn_qurey($"read lteip", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_ethip(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:read lteip");
                string rsu = dn_qurey($"read ethip", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string hwid_wifi_test(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:hwid wifi");
                // string rsu = dn_qurey($"hwid wifi", out status);
                this.ReadExisting();
                this.WriteLine("hwid wifi");
                string rsu = "";
                for (int i = 0; i < 15; i++) {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4) { 
                    rsu= this.ReadExisting() ;
                        break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    glob_Data.wifi_mac = rsu.Substring(3).Trim();
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string wifissid_write(string ssid, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:wifissid write " + ssid);
                string rsu = dn_qurey($"wifissid write " + ssid, out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string wifipw_write(string pw, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:wifipw write " +pw);
                string rsu = dn_qurey($"wifipw write " + pw, out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string enter_lte_test(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:enter_lte_test");
                string rsu = dn_qurey($"enter_lte_test", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.ToUpper().IndexOf("OK") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }


        public string get_lte_fw_ver(out int status)
        {
          

                mylib.utility_func.callbackdebuginfo("get_lte_fw_ver");

            string rsu = "";
            status = 0;
                for (int i = 0; i < 10; i++)
                {
                this.WriteLine("ATI1");
                rsu = "";
                try
                    {

                        System.Threading.Thread.Sleep(1000);
                        rsu = this.ReadExisting().Replace("\\d", " ").Replace("\\r", " ");
                        mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);



                    Regex regex = new Regex(@"(?<=recv\:)LR\d.\d.\d.\d-\d{5}");
                            //Match match = regex.Match(rsu);
                            MatchCollection matches = regex.Matches(rsu);
                            if (matches.Count >= 1)
                            {
                                status = 1;
                                rsu = matches[0].Value;
                                break;
                            }

                      


                    }
                    catch {



                    }


                }

                
                return rsu;



        }
        public string exit_lte_test(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:exit_lte_test");
                this.WriteLine("AT+SMCWTX=0");
                System.Threading.Thread.Sleep(1000);
                string tsr = this.ReadExisting();
                mylib.utility_func.callbackdebuginfo(" AT+SMCWTX=0; rev msg:" + tsr);
                string rsu = dn_qurey($"exit_lte_test", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.ToUpper().IndexOf("OK") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string lte_rf_test(out int status,string bandname, int power_inx=2300)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:rf_test");
                string rsu = "";

                string[] cmd = { "AT+CFUN=5", $"AT+SMCWTX=1,{(int)(LTE_Band)Enum.Parse(typeof(LTE_Band), bandname)},{power_inx}" };
                this.ReadExisting();
                foreach (string arg in cmd)
                {
                    mylib.utility_func.callbackdebuginfo($"send comm:{arg}");
                    if (arg == "AT+CFUN=5") { System.Threading.Thread.Sleep(2000); } else { System.Threading.Thread.Sleep(2000); }
                    this.WriteLine($"{arg}");
                  
                 
                }



                System.Threading.Thread.Sleep(2000);
                rsu = this.ReadExisting();
            
                utility_func.callbackdebuginfo($"LTE_module rev :\n" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.ToUpper().IndexOf("ERROR") >= 0) { goto lb_end; };








            lb_end:
                if (rsu.IndexOf("OK") >= 0)
                {
                    status = 1;
                    return rsu.Replace("\r", "").Replace("\n", "").ToUpper().Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Replace("\r", "").Replace("\n", "").ToUpper().Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string awsendpoint_write( out int status, string point = "iot.dev.duenorth.cloud")
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:awsendpoint write " + point);
                string rsu = dn_qurey($"awsendpoint write " + point, out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string hwid_bluetooth(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:hwid bluetooth");
                // string rsu = dn_qurey($"hwid bluetooth", out status);

                this.ReadExisting();
                this.WriteLine("hwid bluetooth");
                string rsu = "";
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    glob_Data.bt_mac= rsu.Substring(3).Trim();
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_battery_status(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:status battery");
                string rsu = dn_qurey($"status battery", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string ntc_adc_calibration(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm:auto_vcc_sample");
                this.ReadExisting();
                this.WriteLine("auto_vcc_sample");
                string rsu = "";
                for (int i = 0; i < 50; i++)
                {
                   rsu= this.ReadLine();
                    utility_func.callbackdebuginfo(rsu);
                    if (rsu.IndexOf("avg") > 0 ||rsu.ToUpper().IndexOf("FAIL") >= 0) break;

                }


               // string rsu = dn_qurey($"auto vcc_sample", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("avg") >= 0)
                {
                    Regex regex = new Regex(@"[-+]?\d*\.?\d+");
                    //Match match = regex.Match(rsu);
                    MatchCollection matches = regex.Matches(rsu);
                    if (matches.Count>=2)
                    {
                        status = 1;
                        // 将匹配的数值字符串转换为 int
                        return matches[0].Value + ";"+ matches[1].Value;
                    }
                    else {

                        status = -3;
                        return rsu.Trim();
                    }
                  
                   
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string ntc_42k_calibration(out int status,int ch)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm:auto_v42k_sample {ch}");
                this.ReadExisting();
                this.WriteLine($"auto_v42k_sample {ch}");
                string rsu = "";
                for (int i = 0; i < 50; i++)
                {
                    rsu = this.ReadLine();
                    utility_func.callbackdebuginfo(rsu);
                    if (rsu.IndexOf("avg") > 0 || rsu.IndexOf("fail") >= 0) break;
                    

                }


                // string rsu = dn_qurey($"auto vcc_sample", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("avg") >= 0)
                {
                    Regex regex = new Regex(@"[-+]?\d*\.?\d+");
                    Match match = regex.Match(rsu);

                    if (match.Success)
                    {
                        status = 1;
                        // 将匹配的数值字符串转换为 int
                        return match.Value;
                    }
                    else
                    {

                        status = -3;
                        return rsu.Trim();
                    }


                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string ntc_2k3_calibration(out int status, int ch)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm:auto_v2k3_sample {ch}");
                this.ReadExisting();
                this.WriteLine($"auto_v2300_sample {ch}");
                string rsu = "";
                for (int i = 0; i < 50; i++)
                {
                    rsu = this.ReadLine();
                    utility_func.callbackdebuginfo(rsu);
                    if (rsu.IndexOf("avg") > 0 || rsu.IndexOf("fail") >= 0) break;


                }


                // string rsu = dn_qurey($"auto vcc_sample", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("avg") >= 0)
                {
                    Regex regex = new Regex(@"[-+]?\d*\.?\d+");
                    Match match = regex.Match(rsu);

                    if (match.Success)
                    {
                        status = 1;
                        // 将匹配的数值字符串转换为 int
                        return match.Value;
                    }
                    else
                    {

                        status = -3;
                        return rsu.Trim();
                    }


                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_ntc_offset_write(string ch,string offset, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm:ntc offset write {ch} {offset} ");
                string rsu = dn_qurey($"offset write {ch} {offset}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string relay_on_off(string ch, string onoff, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm:relay {onoff} {ch}");
                //  string rsu = dn_qurey($"relay {onoff} {ch}", out status);

                this.ReadExisting();
                this.WriteLine($"relay {onoff} {ch}");
                string rsu = "";
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead >= 2)
                    {
                        rsu = this.ReadExisting();
                        if(rsu.IndexOf("ok")>=0)
                        break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.ToLower().IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_doorsw(string ch,out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm: status doorsw "+ch);
                string rsu = dn_qurey($"status doorsw {ch}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string buzzer_on_off(string onoff, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo("send comm: buzzer " + onoff);
                string rsu = dn_qurey($"buzzer {onoff}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string set_doorlock_status(string ch,string onoff, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: doorlock {onoff} {ch}");
                string rsu = dn_qurey($"doorlock {onoff} {ch}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_doorlock_status(string ch,  out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: status doorlock {ch}");
                string rsu = dn_qurey($"status doorlock {ch}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_usbcamer_status(string ch, out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: usb sw {ch}");
                // string rsu = dn_qurey($"usbcam read {ch}", out status);

                this.ReadExisting();
                this.WriteLine($"usb sw {ch}");
                string rsu = "";
                for (int p = 0; p < 15; p++) {
                    System.Threading.Thread.Sleep(250);
                    if (this.BytesToRead > 0) {

                        rsu = this.ReadExisting();
                        mylib.utility_func.callbackdebuginfo($"{rsu}");
                        if (rsu.IndexOf("ok")>=0)break;
                    }

                }


                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);

                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(rsu.IndexOf("ok"),rsu.Length- rsu.IndexOf("ok")-2).Trim().Replace(","," ");
                }
                else
                {

                    status = -1;
                    return rsu.Trim().Replace(",", " ");
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_hwid_eth( out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: hwid eth");
                //string rsu = dn_qurey($"hwid eth", out status);

                this.WriteLine("hwid eth");
                string rsu = "";
                for (int i = 0; i <15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                    glob_Data.eth_mac= rsu.Substring(3).Trim();
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string set_voltage_offset(out int status,double bizhi)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"set_voltage_offset");
               // string rsu = dn_qurey($"voffset {bizhi}", out status);

                this.ReadExisting();
                this.WriteLine($"voffset write {bizhi}");
                string rsu = "";
                for (int p = 0; p < 30; p++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead >= 2)
                    {

                        rsu = this.ReadExisting();
                        mylib.utility_func.callbackdebuginfo($"{rsu}");
                        if (rsu.IndexOf("ok") >= 0) break;
                    }

                }



                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {

                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string set_current_offset(out int status, double bizhi)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"set_current_offset");
                // string rsu = dn_qurey($"coffset {bizhi}", out status);

                this.ReadExisting();
                this.WriteLine($"coffset write {bizhi}");
                string rsu = "";
                for (int p = 0; p < 30; p++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead >= 2)
                    {

                        rsu = this.ReadExisting();
                        mylib.utility_func.callbackdebuginfo($"{rsu}");
                        if (rsu.IndexOf("ok") >= 0) break;
                    }

                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {

                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string set_power_offset(out int status, double bizhi)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"set_power_offset");
                // string rsu = dn_qurey($"poffset {bizhi}", out status);

                this.ReadExisting();
                this.WriteLine($"poffset write {bizhi}");
                string rsu = "";
                for (int p = 0; p < 30; p++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead >=2)
                    {

                        rsu = this.ReadExisting();
                        mylib.utility_func.callbackdebuginfo($"{rsu}");
                        if (rsu.IndexOf("ok") >= 0) break;
                    }

                }


                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {

                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_energy_meter_voltage(out int status)
        {
            try
            {
                energy_meter_voltage = -1;
                mylib.utility_func.callbackdebuginfo($"send comm: read_energy_meter_voltage");
               // string rsu = dn_qurey($"voltage read", out status);

                this.WriteLine("voltage read");
                string rsu = "";
                for (int i = 0; i < 30; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        if(rsu.IndexOf("ok")>=0)break;
                    }
                }


                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                  

                    Regex regex = new Regex(@"[-+]?\d*\.?\d+");
                    Match match = regex.Match(rsu);

                    if (match.Success)
                    {

                        // 将匹配的数值字符串转换为 Double
                        energy_meter_voltage = double.Parse(match.Value);
                        status = 1;
                        return energy_meter_voltage + "";
                    }
                    else {

                        status = -3;
                    }

                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string read_energy_meter_current(out int status)
        {
            try
            {
                energy_meter_current = -1;
                mylib.utility_func.callbackdebuginfo($"send comm:read_energy_meter_current");
                // string rsu = dn_qurey($"current read", out status);
                this.WriteLine("current read");
                string rsu = "";
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        if (rsu.IndexOf("ok") >= 0) break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {


                    Regex regex = new Regex(@"[-+]?\d*\.?\d+");
                    Match match = regex.Match(rsu);

                    if (match.Success)
                    {

                        // 将匹配的数值字符串转换为 Double
                        energy_meter_current = double.Parse(match.Value);
                        status = 1;
                        return energy_meter_current + "";
                    }
                    else
                    {

                        status = -3;
                    }

                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string read_offset(out int status, string type)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: {type}offset read");
                //string rsu = dn_qurey($"hwid eth", out status);

                this.WriteLine($"{type}offset read");
                string rsu = "";
                for (int i = 0; i < 15; i++)
                {
                    System.Threading.Thread.Sleep(200);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        break;
                    }
                }
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                rsu = rsu.Replace(",", " ");
                if (rsu.IndexOf("ok") >= 0)
                {
                  
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string get_ntc_info(out int status, int ch, out double[] numbers)
        {
            try
            {
                energy_meter_current = -1;
                mylib.utility_func.callbackdebuginfo($"send comm: ntc42k_calibration");
                //string rsu = dn_qurey($"ntc_info {ch}", out status);

                this.ReadExisting();
                this.WriteLine($"ntc_info {ch}");
                string rsu = "";
                for (int i = 0; i < 50; i++)
                {
                    rsu = this.ReadLine().Trim();
                    utility_func.callbackdebuginfo(rsu);
                    if (rsu.IndexOf("NTC") >= 0) break;

                }
                rsu = rsu.Replace(",", " ");

                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                if (rsu.IndexOf("NTC") >= 0)
                {


                    // 正则表达式匹配数值部分
                    Regex regex = new Regex(@"\d+\.?\d*");
                    MatchCollection matches = regex.Matches(rsu);

                    numbers = new double[matches.Count];
                    for (int i = 0; i < matches.Count; i++)
                    {
                        numbers[i] = double.Parse(matches[i].Value);
                    }

                    if (matches.Count == 4) { status = 1; } 
                    
                    else {

                        status = -3;
                    }

                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    numbers = new double[] { -1, -1, -1 };
                    return rsu.Trim().Replace(",","");
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                status = -1;
                numbers = new double[] { -1, -1, -1 };
                return "comm error";


            }



        }

        public string set_uart_loop(out int status, string loop_test_str)
        {
            try
            {
                energy_meter_current = -1;
                
                //string rsu = dn_qurey($"ntc_info {ch}", out status);

                this.ReadExisting();
                //string[] uarts = { "disp_loop_test", "rs232_loop_test", "ext1_loop_test", "ext2_loop_test" };
                mylib.utility_func.callbackdebuginfo($"send comm: {loop_test_str}");
                this.WriteLine($"{loop_test_str}");
                string rsu = "";
                for (int i = 0; i < 50; i++)
                {
                    rsu = this.ReadLine().Trim();
                    utility_func.callbackdebuginfo(rsu);
                    if (rsu.ToUpper().IndexOf("OK") >= 0 || rsu.ToUpper().IndexOf("FAIL") >= 0) { status = 1; return rsu.Trim(); } ;

                }
                status = -1;
                return rsu.Trim();
              
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_energy_meter_power(out int status)
        {
            try
            {
                energy_meter_powr = -1;
                mylib.utility_func.callbackdebuginfo($"send comm:read_energy_meter_power");
                // string rsu = dn_qurey($"power read", out status);

                this.WriteLine("power read");
                string rsu = "";
                for (int i = 0; i < 30; i++)
                {
                    System.Threading.Thread.Sleep(100);
                    if (this.BytesToRead > 4)
                    {
                        rsu = this.ReadExisting();
                        if (rsu.IndexOf("ok") >= 0) break;
                    }
                }


                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                if (rsu.IndexOf("ok") >= 0)
                {


                    Regex regex = new Regex(@"[-+]?\d*\.?\d+(?=[w|W])");
                    Match match = regex.Match(rsu);

                    if (match.Success)
                    {

                        // 将匹配的数值字符串转换为 Double
                        energy_meter_powr = double.Parse(match.Value);
                        status = 1;
                        return energy_meter_powr + "";
                    }
                    else
                    {

                        status = -3;
                    }

                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string read_ths_status(out int status)
        {
            try
            {

                mylib.utility_func.callbackdebuginfo($"send comm: ths test");
                string rsu = dn_qurey($"ths test", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Substring(3).Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string exit_test_mode(out int status)
        {
            try
            {
                string EPOCH =  BitConverter.ToString(BitConverter.GetBytes(mylib.utility_func.ConvertDateTimeInt(DateTime.Now))).Replace("-","");
                mylib.utility_func.callbackdebuginfo($"send comm: test exit 0x{EPOCH}");
                string rsu = dn_qurey($"test exit 0x{EPOCH}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        public string reset_dut(out int status)
        {
            try
            {
            mylib.utility_func.callbackdebuginfo($"send comm: reset -y");
                this.WriteLine($"reset -y");
               
                    status = 1;
                    return "ok";
              
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }

        public string set_gpio(int gpiopin,int onoff,out int status)
        {
            try
            {
                mylib.utility_func.callbackdebuginfo($"send comm: gpio write {gpiopin} {onoff}");
                string rsu = dn_qurey($"gpio write {gpiopin} {onoff}", out status);
                mylib.utility_func.callbackdebuginfo("rev msg:" + rsu);
                if (rsu.IndexOf("ok") >= 0)
                {
                    status = 1;
                    return rsu.Trim();
                }
                else
                {

                    status = -1;
                    return rsu.Trim();
                }
                status = 1;
                return "ok";

            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo("error message:" + ex.Message);
                status = -2;
                return "comm error";


            }



        }
        #region sgw_dndata_save
        internal class glob_data {

            public string eth_mac = "";
            public string sn = "";
           public  string imei = "";
            public string wifi_mac = "";
            public string bt_mac = "";
        }
        public void clear_glob_Data() {
            glob_Data.sn = "";
            glob_Data.imei = "";
            glob_Data.wifi_mac = "";
            glob_Data.bt_mac = "";
            glob_Data.eth_mac = "";
        }

        #endregion
       
        ~sgw_DN_proj() { 
            this.Close();
           
        }
      
    }

}

