using HslCommunication.Enthernet.Redis;
using NationalInstruments.DataInfrastructure;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SharpExModule;
using SLCANWithEvents;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace testapp.test_cases
{
    public class dometic_relay_cotrol_board : IDefaultAction, IDisposable
    {
        private byte tempearture = byte.MaxValue;
        private SerialPort _comPort;
        private SRND_CM_12DI InPutDeter = null;
        private const int Timeout = 1000; // 1秒超时
        testcase_dll tc;
        string id = "";
       private volatile int got_flog = 0;
        byte[] rsu_byts = null; 
        public dometic_relay_cotrol_board(testcase_dll _tc, string comPortName,string DIcomPortName)
        {
            // 初始化主串口
            _comPort = new SerialPort(comPortName, 115200, Parity.None, 8, StopBits.One);
            _comPort.ReadTimeout = Timeout;
            _comPort.WriteTimeout = Timeout;
            InPutDeter = new SRND_CM_12DI(DIcomPortName);

            tc = _tc;
        }

        public void OpenPorts()
        {
            try
            {
                if (!_comPort.IsOpen)
                    _comPort.Open();
                _comPort.DataReceived += _comPort_DataReceived;
                if (!InPutDeter.IsOpen) InPutDeter.Open();
               
            }
            catch (Exception ex)
            {
              mylib.utility_func.callbackdebuginfo($"打开端口时出错: {ex.Message}");
                
            }
            add_func_to_libs();
        }

        private void _comPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            int bytesToRead = sp.BytesToRead;
            byte[] buffer = new byte[bytesToRead];
          
            // 读取串口缓冲区中的数据
            sp.Read(buffer, 0, bytesToRead);
            if (bytesToRead > 0) {
                got_flog = bytesToRead;
                rsu_byts = new byte[bytesToRead];
                for(int i=0;i<bytesToRead;i++) { rsu_byts[i] = buffer[i]; }
            }
            // 将字节数组转换为十六进制字符串
            string hexString = BitConverter.ToString(buffer).Replace("-", " ");
            if (hexString.Length > 0) { mylib.utility_func.callbackdebuginfo("rev: " + hexString); }
            if (hexString.Length == 2) { 
                
                tempearture = rsu_byts[0]; 
            
            
            } else {

                tempearture = byte.MaxValue;
            }
        }

        public void add_func_to_libs()
        {
            //id = this.GetType().Name;
            id = "dometic_realy_";
         
            tc.funcs.Add(id + "set_mode", set_mode);
            tc.funcs.Add(id + "get_input_status", get_input_status);

            tc.golb_var_default["braking_pcba_tp25"] = "-100";
        }

 


        private string get_input_status(string a, string b, out string c, string d) {

            c = "fail";
            string p = "";
            if (InPutDeter.read_DI(0, out p) == 1) { 
            
            mylib.utility_func.callbackdebuginfo("Input status:(x7.x6.x5....x0)\n:" + p);
                c = "'"+ p;
                if (p == a.Replace("'", ""))
                {

                    return "pass";
                }
                else { 
                     return "fail";

                }
            }
            ;



            return "fail";
        
        }

        private string set_and_get_k1_status(string a, string b, out string c, string d)
        {

            if(!_comPort.IsOpen)_comPort.Open();
            for (int i = 0; i < 125; i++) {
                c = "fail";
                string p = "";
                mylib.utility_func.callbackdebuginfo($"send: {d}");
                _comPort.Write(new byte[] { System.Text.Encoding.UTF8.GetBytes(d)[0] }, 0, 1);

                Thread.Sleep(1000);
                if (InPutDeter.read_DI(0, out p) == 1) {
                  mylib.utility_func.callbackdebuginfo("Input status:(x7.x6.x5....x0)\n:" + p);
                    c = "'" + p;
                    if (p == a.Replace("'", ""))
                    {
                        return "pass";
                    }
                    else
                    {
                        continue;
                    }
                }


            }
              
            c = "fail";
           
           


            return "fail";

        }

        private string set_mode(string a, string b, out string c, string d)
        {
            if(!_comPort.IsOpen)_comPort.Open();
            if (d == "") d = "relay_off";
            switch(d)
            {
                case "relay_off":
                    d = "a";
                    break;
                case "LOFAN_ON":
                    d = "b";
                    break;
                case "HIFAN_ON":
                    d = "c";
                    break;
                case "COMP_ON":
                    d = "d";
                    break;
                case "FURN_ON":
                    d = "e";
                    break;
                case "HET_HI_ON":
                    d = "g";
                    break;
                case "relay6":
                    d = "f";
                    break;
                case "relay7":
                    d = "g";
                    break;
                default:
                    d = "a";
                    break;
            }
            c = "fail";
            got_flog = 0;
            _comPort.Write(new byte[] { System.Text.Encoding.UTF8.GetBytes(d)[0] }, 0, 1);
            mylib.utility_func.callbackdebuginfo("Send Data: " + d);
            int count = 0;
            while (got_flog == 0 || got_flog >1) { 
                
                System.Threading.Thread.Sleep(50);
                if (count++ > 10 ) { break; }
            }
            if (got_flog == 0 || got_flog > 1)
            {

                c = "error";
            }
            else {
                c = $"{tempearture:x2}";
                if (tempearture <= int.Parse(a,System.Globalization.NumberStyles.HexNumber) && tempearture >= int.Parse(b,System.Globalization.NumberStyles.HexNumber))
                {

                    return "pass";
                }
                else { 
                
                return "fail";
                }
               
            }

                return "fail";
        }


        public void InsertDefaultAction()
        {


            tc.dev_moren[id] = this;

        }

        public void ClosePorts()
        {
            _comPort?.Close();
            InPutDeter?.Close();
           
        }

   
        private byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        // 辅助方法: 字节数组转十六进制字符串
        private string ByteArrayToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }

        public void set_default_set()
        {
            
        }

        public void Dispose()
        {
            try
            {

                ClosePorts();
                _comPort?.Dispose();
                InPutDeter?.Dispose();
                tc.dev_moren.Remove(id);

            }
            catch (Exception ex)
            {
            }
        }
    }

}
