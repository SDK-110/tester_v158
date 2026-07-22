using System;
using System.IO.Ports;
using System.Threading;
using System.Text;
using testapp;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace testapp.mylib
{
    /// <summary>
    /// TDP8500 直流负载控制器 - 实现恒流、恒压、恒功率模式控制
    /// </summary>
    public class TDP8500:IDisposable, IDefaultAction
    {
        // 通信参数
        private SerialPort serialPort;
        private string portName;
        private const string END_SYMBOL = "\r\n"; // 结束符号为换行符(0x0d 0x0A)
        testcase_dll tc;
        string id = "";
        string div_name = "";
        string num = "0";
        // 工作模式状态
        private LoadMode currentMode = LoadMode.CURRent;
        private bool isModeEnabled = false;
        
        /// <summary>
        /// 负载工作模式枚举
        /// </summary>
        public enum LoadMode
        {
            CURRent,    // 恒流模式
            VOLTage,    // 恒压模式
            POWer       // 恒功率模式
        }

        public TDP8500(testcase_dll ref_tc, string div_name,string num):this(div_name)
        {
            tc = ref_tc;
            this.div_name = div_name;
            this.num = num;
            Task.Run(() => {


                Open();
                serialPort.WriteLine("*IDN?");
                string tmp = serialPort.ReadLine();
                mylib.utility_func.callbackdebuginfo(tmp);
                if (tmp.Length < 0) { throw new Exception("TDP8500 error"); }
                InsertDefaultAction();
                add_func_to_libs();


            });
       
        }
        public void add_func_to_libs()
        {
            id = this.GetType().Name + "_" + this.num+"_";
            tc.funcs.Add(id + "e_load_onoff", e_load_onoff);
            tc.funcs.Add(id + "constant_curr_set", constant_curr_set);
            tc.funcs.Add(id + "measure_get", measure_get);
            tc.golb_var_default["123"] = "fdsafds";
        }

    

        public void InsertDefaultAction()
        {


            tc.dev_moren[id] = this;

        }


        /// <summary>
        /// 构造函数，初始化串口连接参数
        /// </summary>
        /// <param name="portName">串口号（如"COM1"）</param>
        public TDP8500(string portName)
        {
            this.portName = portName;
            this.serialPort = new SerialPort
            {
                BaudRate = 19200,       // 默认波特率
                DataBits = 8,           // 数据位
                Parity = Parity.None,   // 无校验
                StopBits = StopBits.One // 1位停止位
            };
        }
        
        /// <summary>
        /// 打开与设备的连接
        /// </summary>
        public void Open()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = portName;
                    serialPort.Open();
                    serialPort.ReadTimeout = 2000;
                    serialPort.WriteTimeout = 2000;
                    testapp.mylib.utility_func.callbackdebuginfo("成功连接到 TDP8500 设备");
                }
            }
            catch (Exception ex)
            {
                testapp.mylib.utility_func.callbackdebuginfo($"连接设备失败: {ex.Message}");
            }
        }
        
        /// <summary>
        /// 关闭与设备的连接
        /// </summary>
        public void Close()
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                if (isModeEnabled)
                    TurnOffMode(); // 关闭模式再断开连接
                
                serialPort.Close();
             testapp.mylib.utility_func.callbackdebuginfo("已断开与 TDP8500 设备的连接");
            }
        }

        private string e_load_onoff(string a, string b, out string c, string d)
        {
            c = "fail";

            if (a.ToUpper() == "ON")
            {

                TurnOnMode();

            }
            else {

                TurnOffMode();
            
            }
            c = "pass";
                return "pass";
        }
        private string constant_curr_set(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                SetCurrentMode(double.Parse(a), 1);

                c = "pass";

              return "pass";

            }
            catch {  c = "error"; }

            return "fail";
        }

        private string measure_get(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (d == null || d == "") { d = "VOLT"; }
               double mrsu=  measure_value(d);
                c = "" + mrsu;
                if (mrsu >= double.Parse(b) && mrsu <= double.Parse(a))
                {
                    return "pass";
                   
                }
               

                return "fail";

            }
            catch { c = "error"; }

            return "fail";
        }
        /// <summary>
        /// 发送命令到设备并获取响应
        /// </summary>
        private string SendCommand(string command)
        {
            if (!serialPort.IsOpen)
            {
                utility_func.callbackdebuginfo("设备未连接，请先调用 Open 方法");

                return "error";
            }

                // 确保命令以结束符结尾
                if (!command.EndsWith(END_SYMBOL))
                command += END_SYMBOL;
                
            try
            {
                serialPort.Write(command);
                Thread.Sleep(50); // 等待响应
                
                StringBuilder response = new StringBuilder();
                while (serialPort.BytesToRead > 0)
                    response.Append((char)serialPort.ReadByte());
                    
                return response.ToString().TrimEnd(END_SYMBOL.ToCharArray());
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"命令执行失败 [{command}]: {ex.Message}");
                return "error";
            }
        }
        
        /// <summary>
        /// 设置恒流模式
        /// </summary>
        /// <param name="current">电流值(A)，范围0~MAX</param>
        /// <param name="range">电流档位(0/MIN或1/MAX)</param>
        public void SetCurrentMode(double current, uint range = 1)
        {
            SetLoadMode(LoadMode.CURRent);
            // 设置电流档位
            string rangeCmd = $"CURRent:RANGe {range}";
            SendCommand(rangeCmd);
            
            // 设置电流值
            string currentCmd = $"CURRent {current:F3}";
            SendCommand(currentCmd);
            
            // 切换到恒流模式
           
            utility_func.callbackdebuginfo($"恒流模式已设置: {current}A，档位:{range}");
        }
        
        /// <summary>
        /// 设置恒压模式
        /// </summary>
        /// <param name="voltage">电压值(V)，范围0~MAX</param>
        /// <param name="range">电压档位(0/MIN或1/MAX)</param>
        public void SetVoltageMode(double voltage, uint range = 1)
        {
            // 设置电压档位
            string rangeCmd = $"VOLTage:RANGe {range}";
            SendCommand(rangeCmd);
            
            // 设置电压值
            string voltageCmd = $"VOLTage {voltage:F3}";
            SendCommand(voltageCmd);
            
            // 切换到恒压模式
            SetLoadMode(LoadMode.VOLTage);
            mylib.utility_func.callbackdebuginfo($"恒压模式已设置: {voltage}V，档位:{range}");
        }
        
        /// <summary>
        /// 设置恒功率模式
        /// </summary>
        /// <param name="power">功率值(W)，范围0~MAX</param>
        public void SetPowerMode(double power)
        {
            // 设置功率值
            string powerCmd = $"POWer {power:F3}";
            SendCommand(powerCmd);
            
            // 切换到恒功率模式
            SetLoadMode(LoadMode.POWer);
            mylib.utility_func.callbackdebuginfo($"恒功率模式已设置: {power}W");
        }
        
        /// <summary>
        /// 切换负载工作模式
        /// </summary>
        private void SetLoadMode(LoadMode mode)
        {
            if (currentMode == mode) return; // 无需切换
            
            string modeCmd = $"MODE {mode.ToString().ToUpper()}";
            SendCommand(modeCmd);
            currentMode = mode;
           mylib.utility_func.callbackdebuginfo($"工作模式已切换至: {mode}");
        }
        
        /// <summary>
        /// 开启当前设置的模式
        /// </summary>
        public void TurnOnMode()
        {
            string cmd = "INPut 1";
            SendCommand(cmd);
            isModeEnabled = true;
            mylib.utility_func.callbackdebuginfo($"[{currentMode}] 模式已开启");
        }
        
        /// <summary>
        /// 关闭当前工作模式
        /// </summary>
        public void TurnOffMode()
        {
            string cmd = "INPut 0";
            SendCommand(cmd);
            isModeEnabled = false;
            mylib.utility_func.callbackdebuginfo($"[{currentMode}] 模式已关闭");
        }
        
        /// <summary>
        /// 查询设备型号信息
        /// </summary>
        public string QueryDeviceInfo()
        {
            string response = SendCommand("*IDN?");
            mylib.utility_func.callbackdebuginfo($"设备信息: {response}");
            return response;
        }
        
        /// <summary>
        /// 读取设备错误信息
        /// </summary>
        public string QueryError()
        {
            string response = SendCommand("SYST:ERR?");
            mylib.utility_func.callbackdebuginfo($"错误查询: {response}");
            return response;
        }

        public void Dispose()
        {
            try
            {

                if (serialPort.IsOpen) serialPort.Close();
                serialPort.Dispose();

            }
            catch (Exception e) { }
           
        }

        public double measure_value(string mode) {

            if (mode.ToUpper().IndexOf("VOL") >= 0)
            {
                try
                {
                    if(!serialPort.IsOpen) serialPort.Open();
                    serialPort.WriteLine("MEAS:VOLT?");
                    string rsu = serialPort.ReadLine();
                    string pattern = @"\d+\.\d+";
                    if (Regex.Match(rsu, pattern).Success) { 
                    mylib.utility_func.callbackdebuginfo(rsu);
                    return double.Parse(rsu);
                    
                    }
                    return double.NaN;

                }
                catch (Exception e) {
                    mylib.utility_func.callbackdebuginfo(e.ToString());
                    return double.NaN; }
            
            
            }
            if (mode.ToUpper().IndexOf("CURR") >= 0)
            {
                try
                {
                    if (!serialPort.IsOpen) serialPort.Open();
                    serialPort.WriteLine("MEAS:CURR?");
                    string rsu = serialPort.ReadLine();
                    string pattern = @"\d+\.\d+";
                    if (Regex.Match(rsu, pattern).Success)
                    {
                        mylib.utility_func.callbackdebuginfo(rsu);
                        return double.Parse(rsu);

                    }
                    return double.NaN;

                }
                catch (Exception e)
                {
                    mylib.utility_func.callbackdebuginfo(e.ToString());
                    return double.NaN;
                }


            }
            if (mode.ToUpper().IndexOf("RES") >= 0)
            {
                try
                {
                    if (!serialPort.IsOpen) serialPort.Open();
                    serialPort.WriteLine("MEAS:RES?");
                    string rsu = serialPort.ReadLine();
                    string pattern = @"\d+\.\d+";
                    if (Regex.Match(rsu, pattern).Success)
                    {
                        mylib.utility_func.callbackdebuginfo(rsu);
                        return double.Parse(rsu);

                    }
                    return double.NaN;

                }
                catch (Exception e)
                {
                    mylib.utility_func.callbackdebuginfo(e.ToString());
                    return double.NaN;
                }


            }

            if (mode.ToUpper().IndexOf("POW") >= 0)
            {
                try
                {
                    if (!serialPort.IsOpen) serialPort.Open();
                    serialPort.WriteLine("MEAS:POW?");
                    string rsu = serialPort.ReadLine();
                    string pattern = @"\d+\.\d+";
                    if (Regex.Match(rsu, pattern).Success)
                    {
                        mylib.utility_func.callbackdebuginfo(rsu);
                        return double.Parse(rsu);

                    }
                    return double.NaN;

                }
                catch (Exception e)
                {
                    mylib.utility_func.callbackdebuginfo(e.ToString());
                    return double.NaN;
                }


            }
            return double.NaN ;
        }
        public void set_default_set()
        {
            try {
                if (!serialPort.IsOpen) serialPort.Open();
                serialPort.WriteLine("SYSTem:REMote");
                SetCurrentMode(0.1, 1);
                serialPort.WriteLine("CURR:SLEW 0.1");
                TurnOffMode();
            }
            catch (Exception e) { }
           
        }
    }
}