using HslCommunication.Enthernet.Redis;
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
    public class vantage_braking_board_test : IDefaultAction, IDisposable
    {
        private SerialPort _comPort;
        private SLCANSerialPort _canPort;
        private const int Timeout = 1000; // 1秒超时
        testcase_dll tc;
        string id = "";
        public vantage_braking_board_test(testcase_dll _tc, string comPortName, string canPortName)
        {
            // 初始化主串口
            _comPort = new SerialPort(comPortName, 9600, Parity.None, 8, StopBits.One);
            _comPort.ReadTimeout = Timeout;
            _comPort.WriteTimeout = Timeout;

            // 初始化CAN串口
            _canPort = new SLCANSerialPort(canPortName);
            _canPort.ReadTimeout = Timeout;
            _canPort.WriteTimeout = Timeout;
            tc= _tc;
        }

        public void OpenPorts()
        {
            try
            {
                if (!_comPort.IsOpen)
                    _comPort.Open();

                if (!_canPort.IsOpen)
                    _canPort.Open();
            }
            catch (Exception ex)
            {
              mylib.utility_func.callbackdebuginfo($"打开端口时出错: {ex.Message}");
                
            }
            add_func_to_libs();
        }
        public void add_func_to_libs()
        {
            //id = this.GetType().Name;
            id = "BrakeBoard_";
            tc.funcs.Add(id + "isp_pin_test", isp_pin_test);
            tc.funcs.Add(id + "can_port_test", can_port_test);
            tc.funcs.Add(id + "led_test", led_test);
            tc.funcs.Add(id + "test_get_bps", test_get_bps);
            tc.funcs.Add(id + "test_dip_sw", test_dip_sw);
            tc.funcs.Add(id + "test_chip_id", test_chip_id);
            tc.funcs.Add(id + "test_FLT_signal", test_FLT_signal);
            tc.funcs.Add(id + "test_ntc_tmp", test_ntc_tmp);
            tc.funcs.Add(id + "test_IGBT_onoff", test_IGBT_onoff);
            tc.funcs.Add(id + "read_cur_fb", read_cur_fb);
            tc.funcs.Add(id + "read_vfb", read_vfb);
            tc.funcs.Add(id + "read_dcbus", read_dcbus);
            tc.funcs.Add(id + "read_voltage", read_voltage);
            tc.funcs.Add(id + "get_Linear_opt_K3_other", get_Linear_opt_K3_other);

            tc.golb_var_default["braking_pcba_tp25"] = "-100";
        }

        private string isp_pin_test(string a, string b, out string c, string d)
        {
            c="fail";
            string rsu = TestISPStatus().Trim();
            string  status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("ISP:STATUS:1") >= 0)
            {
                status = "high";
            }
            if (rsu.IndexOf("ISP:STATUS:0") >= 0)
            {
                status = "low";
            }
            c = status;
            if (a == status) {

               
                return "pass";
            
            }

            return "fail";
        }

        private string can_port_test(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = TestCANInterface().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("pass") >= 0)
            {
                status = "pass";
            }
            if (rsu.IndexOf("fail") >= 0)
            {
                status = "fail";
            }
            c = status;
            if (a == status)
            {


                return "pass";

            }

            return "fail";
        }

        public enum leds
        {
            D11=0,
            D12=1,
            D13=2,
            D14=3,
            D15=4,
            D16=5,
            D17=6,
            D18=7,
            D20=8,
                       
        }
        private string led_test(string a, string b, out string c, string d)
        {
            c = "fail";

            if (a.Length != 9 && a.All(t=>t=='0'||t=='1')) {

                c = "setting parameter is incorrect";
                return "fail";
            }
            ;
            string rsu = ControlLED(a.Replace("'",""));
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("OK") >= 0)
            {
                status = "OK";
            }
            if (rsu.IndexOf("fail") >= 0)
            {
                status = "fail";
            }
            c = status;
            if (status=="OK")
            {


                return "pass";

            }

            return "fail";
        }

        private string test_get_bps(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = TestBPS1Status().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
        
            if (rsu.IndexOf("BPS1:STATUS:1")>=0)
            {
                status ="high";

            }
            else
            {
                status ="low";
            }
            c = status;
            if (a == status)
            {

                return "pass";
            }
            else {


                return "fail";
            }


                return "fail";
        }
        private string test_dip_sw(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = ReadSwitchStatus().Trim();
            mylib.utility_func.callbackdebuginfo($"rev data:{rsu} map switch<==>PRI/SEC | 125/116 | SW3");
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("SW1_KEY:STATUS:") < 0)
            {
                c = "response error";
                return "fail";
            }
            status = c= rsu;
            if (a.Replace("'","") == status.Replace("SW1_KEY:STATUS:",""))
            {


                return "pass";

            }

            return "fail";
        }

        private string read_voltage(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = tc.funcs[d.Split(';')[0]](a,b,out c);
            if (d.Split(';').Count() <= 1) {

                mylib.utility_func.callbackdebuginfo("参数错误");
                return "fail";
            }
            if (rsu == "pass") {

                switch (d.Split(';')[1])
                    {

                    case  "tp25": 
                        {

                            tc.golb_var_default["bk_tp25"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp2":
                        {
                            tc.golb_var_default["bk_tp2"] = double.Parse(c.Trim());
                        }
                        break;

                    case "tp27":
                        {
                            tc.golb_var_default["bk_tp27"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp28": {

                            tc.golb_var_default["bk_tp28"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp23":

                        {
                            tc.golb_var_default["bk_tp23"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp24":

                        {
                            tc.golb_var_default["bk_tp24"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp29":

                        {
                            tc.golb_var_default["bk_tp29"] = double.Parse(c.Trim());
                        }
                        break;
                    case "tp30":

                        {
                            tc.golb_var_default["bk_tp30"] = double.Parse(c.Trim());
                        }
                        break;
                    default:
                        {
                        }
                        break;
                       

                }


                return "pass";



            }


            return "fail";
        }

        private string get_Linear_opt_K3_other(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "" || d == null) d = "VFB_K3";
            try {
                if (d == "VFB_K3") {
                    double k3 = (double)(tc.golb_var_default["bk_tp2"]) / (double)(tc.golb_var_default["bk_tp23"]);
                    c = k3 + "";

                    if (k3 <= double.Parse(a) && k3 >= double.Parse(b))
                    {

                        return "pass";

                    }
                    else {

                        return "fail";
                    }

                }

                    if (d == "DCBUS_FB_K3")
                    {
                        double k3 = (double)(tc.golb_var_default["bk_tp30"]) / (double)(tc.golb_var_default["bk_tp27"]);
                        c = k3 + "";
                        if (k3 <= double.Parse(a) && k3 >= double.Parse(b))
                        {
                        return "pass";

                         }
                        else
                    {

                        return "fail";
                    } }

                if (d == "TP2_ADFB")
                {
                    double tp2_adfb = (double)(tc.golb_var_default["bk_tp2"]) / (double)(tc.golb_var_default["bk_vfb_vol"]);
                    c = tp2_adfb + "";
                    if (tp2_adfb <= double.Parse(a) && tp2_adfb >= double.Parse(b))
                    {
                        return "pass";

                    }
                    else
                    {

                        return "fail";
                    }
                }

                if (d == "TP30_DBUSFB")
                {
                    double tp2_adfb = (double)(tc.golb_var_default["bk_tp30"]) / (double)(tc.golb_var_default["bk_dcbus_vol"]);
                    c = tp2_adfb + "";
                    if (tp2_adfb <= double.Parse(a) && tp2_adfb >= double.Parse(b))
                    {
                        return "pass";

                    }
                    else
                    {

                        return "fail";
                    }
                }


            }
                catch (Exception e){
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "data error";
            
            }

               


            return "fail";
        }

        private string test_chip_id(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = get_chip_id().Trim();
            mylib.utility_func.callbackdebuginfo($"rev data:{rsu}");
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("ID:STATUS:") < 0)
            {
                c = "response error";
                return "fail";
            }
            status = c = rsu;
            if (a.Replace("'", "") == status.Replace("ID:STATUS:", ""))
            {


                return "pass";

            }

            return "fail";
        }
        private string test_ntc_tmp(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = ReadTemperature().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            Regex regex = new Regex(@"[-+]?\d*\.?\d+");
            Match match = regex.Match(rsu);
            c = rsu;
            if (match.Success)
            {
                status = match.Value;
               
            }
            else
            {
                return "fail";
            }

      
            if (double.Parse(a) >= double.Parse(status)&& double.Parse(b) <= double.Parse(status))
            {


                return "pass";

            }

            return "fail";
        }
        private string test_IGBT_onoff(string a, string b, out string c, string d)
        {
            c = "fail";

            string str_send = "00";
            if (a.ToUpper() == "ON")
            {

                str_send = "10";
            }
            else {

                str_send = "01";
            }
            
            string rsu = ControlIGBT(str_send);
          c=rsu;
            mylib.utility_func.callbackdebuginfo(rsu);
            if (rsu.IndexOf("OK") >= 0)
            {

                return "pass";
            }


               

         

            return "fail";
        }

        private string test_FLT_signal(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "") d = "get";

            if (d == "get")
            {
                if (set_FLT_input_mode().IndexOf("OK") < 0)
                {
                    c = "change mode error";
                    return "fail";
                }

                string str_rsu = get_FLT_Status();
                string status = "error";
                if (str_rsu.IndexOf("FLT:STATUS:1") >= 0) {

                    status = "high";
                }
                if (str_rsu.IndexOf("FLT:STATUS:0") >= 0)
                {

                    status = "low";
                }
                c=status;
                if (a == status)
                {

                    return "pass";
                }
                else {

                    return "fail";
                }

            }
            else {

                string _rsu = set_FLT_Status(a == "high" ? "1" : "0");
                c= _rsu;
                if (_rsu.IndexOf("OK") >= 0)
                {

                    return "pass";

                }
                else {

                    return "fail";
                }
                    


            }
           
            
      



            return "fail";
        }

        private string read_cur_fb(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = ReadCurrent().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            Regex regex = new Regex(@"(?<=CUR\:)([-+]?\d*\.?\d+)V");
            Match match = regex.Match(rsu);
            c = rsu;
            if (match.Success)
            {
                status = match.Groups[1].Value;

            }
            else
            {
                return "fail";
            }


            if (double.Parse(a) >= double.Parse(status) && double.Parse(b) <= double.Parse(status))
            {


                return "pass";

            }

            return "fail";
        }
        private string read_vfb(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = ReadVFB().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            Regex regex = new Regex(@"(?<=VFB\:)([-+]?\d*\.?\d+)V");
            Match match = regex.Match(rsu);
            c = rsu;
            if (match.Success)
            {
                status = match.Groups[1].Value;

            }
            else
            {
                return "fail";
            }


            if (double.Parse(a) >= double.Parse(status) && double.Parse(b) <= double.Parse(status))
            {

                tc.golb_var_default["bk_vfb_vol"] = status;
                return "pass";

            }

            return "fail";
        }

        private string read_dcbus(string a, string b, out string c, string d)
        {
            c = "fail";
            string rsu = ReadDCBUS().Trim();
            string status = "error";
            mylib.utility_func.callbackdebuginfo(rsu);
            Regex regex = new Regex(@"(?<=DCBUS\:)([-+]?\d*\.?\d+)V");
            Match match = regex.Match(rsu);
            c = rsu;
            if (match.Success)
            {
                status = match.Groups[1].Value;

            }
            else
            {
                return "fail";
            }


            if (double.Parse(a) >= double.Parse(status) && double.Parse(b) <= double.Parse(status))
            {

                tc.golb_var_default["bk_dcbus_vol"]=status;
                return "pass";

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
            _canPort?.Close();
        }

        // 1. PC通讯主串口测试[=====1]
        public string TestISPStatus()
        {
            try
            {
                SendCommand(_comPort, "GET:ISP:STATUS?\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"测试ISP状态时出错: {ex.Message}");
                return $"ERROR: {ex.Message}";
            }
        }

        // 3. CAN接口测试
        public string TestCANInterface()
        {
            try
            {
                // 发送测试数据
                string testData = "11 12 13 14";
               
                SendCANData(testData);

                // 等待接收响应
                Thread.Sleep(50);
                string response = ReadCANData();

                // 验证响应
                if (response == "0102030400000000")
                    return "canbus_test_pass";
                else
                    return $"canbus_test_fail:{response}";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"测试CAN接口时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        // 4. BPS1状态测试
        public string TestBPS1Status()
        {
            try
            {
                SendCommand(_comPort, "GET:BPS1:STATUS?\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"测试BPS1状态时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        // 8. 控制LED
        public string ControlLED(string ledPattern)
        {
            try
            {
                SendCommand(_comPort, $"OUT:LED_B:{ledPattern}\r\n");
                string sh_str = "";
                int pos = 0;
                foreach (char led in ledPattern) {
                    sh_str = sh_str + ((leds)pos).ToString()+":="+led.ToString() + " ";

                    pos++;
                
                }
                mylib.utility_func.callbackdebuginfo(sh_str);
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"控制LED时出错: {ex.Message}");
                return $"ERROR";
            }
        }


        public string ReadSwitchStatus()
        {
            try
            {
                SendCommand(_comPort, "GET:SW1_KEY:STATUS?\r\n");
                mylib.utility_func.callbackdebuginfo("GET:SW1_KEY:STATUS?\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"读取拨码开关状态时出错: {ex.Message}");
                return $"ERROR";
            }
        }
        // 9. 读取拨码开关状态
        public string get_chip_id()
        {
            try
            {
                SendCommand(_comPort, "GET:ID:STATUS?\r\n");
                mylib.utility_func.callbackdebuginfo("GET:ID:STATUS?");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"GET:ID:STATUS? : {ex.Message}");
                return $"ERROR";
            }
        }
        public string set_FLT_Status(string status)
        {
            try
            {
                SendCommand(_comPort, $"OUT:FLT:{status}\r\n");
                mylib.utility_func.callbackdebuginfo($"OUT:FLT:{status}\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"OUT:FLT:{status}: {ex.Message}");
                return $"ERROR";
            }
        }

        public string set_FLT_input_mode()
        {
            try
            {
                SendCommand(_comPort, $"SET:FLT:IN\r\n");
                mylib.utility_func.callbackdebuginfo($"SET:FLT:IN");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"SET:FLT:IN: {ex.Message}");
                return $"ERROR";
            }
        }
        public string get_FLT_Status()
        {
            try
            {
                SendCommand(_comPort, $"GET:FLT:STATUS?\r\n");
                mylib.utility_func.callbackdebuginfo($"GET:FLT:STATUS?");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"GET:FLT:STATUS?: {ex.Message}");
                return $"ERROR";
            }
        }
        // 10. 读取温度传感器
        public string ReadTemperature()
        {
            try
            {
                SendCommand(_comPort, "TEST:GET_TEM\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"读取温度传感器时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        // 11. 控制IGBT
        public string ControlIGBT(string command)
        {
            try
            {
                SendCommand(_comPort, $"OUT:DRV:{command}\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"控制IGBT时出错: {ex.Message}");
                return $"ERROR: {ex.Message}";
            }
        }

        // 12. 读取电流
        public string ReadCurrent()
        {
            try
            {
                SendCommand(_comPort, "TEST:GET_CUR\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"读取电流时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        // 13. 读取电压
        public string ReadVFB()
        {
            try
            {
                SendCommand(_comPort, "TEST:GET_VFB\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"读取VFB电压时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        public string ReadDCBUS()
        {
            try
            {
                SendCommand(_comPort, "TEST:GET_DCBUS\r\n");
                return ReadResponse(_comPort);
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"读取DCBUS电压时出错: {ex.Message}");
                return $"ERROR";
            }
        }

        // 辅助方法: 发送命令
        private void SendCommand(SerialPort port, string command)
        {
            port.Write(command);
            mylib.utility_func.callbackdebuginfo($"发送命令: {command.Trim()}");
        }

        // 辅助方法: 读取响应
        private string ReadResponse(SerialPort port)
        {
            string response = port.ReadLine().Trim();
            mylib.utility_func.callbackdebuginfo($"接收响应: {response}");
            return response;
        }

        // 辅助方法: 发送CAN数据
        private void SendCANData(string hexData)
        {
            // byte[] data = HexStringToByteArray(hexData);
            //  _canPort.Write(data, 0, data.Length);
            _canPort.SendCANFrame(0, "11 12 13 14", 0);
            mylib.utility_func.callbackdebuginfo($"发送CAN数据: {hexData}");
        }

        // 辅助方法: 读取CAN数据
        private string ReadCANData()
        {
            //int bytesToRead = _canPort.BytesToRead;
            //if (bytesToRead > 0)
            //{
            //    byte[] buffer = new byte[bytesToRead];
            //    _canPort.Read(buffer, 0, bytesToRead);

            //    string hexString = ByteArrayToHexString(buffer);
            //    mylib.utility_func.callbackdebuginfo($"接收CAN数据: {hexString}");
            //    return hexString;
            //}

            string rsu = _canPort.get_data_hex_str(out _, out _);

            return rsu;
        }

        // 辅助方法: 十六进制字符串转字节数组
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
                _canPort?.Dispose();
            }
            catch (Exception ex)
            {
            }
        }
    }

}
