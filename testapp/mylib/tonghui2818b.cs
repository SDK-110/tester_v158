using log4net.Core;
using Org.BouncyCastle.Ocsp;
using System;
using System.IO.Ports;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace testapp.mylib
{
    /// <summary>
    /// Tonghui TH2810B+/TH2817B+ LCR 数字电桥驱动
    /// 参考 JY_YL2 构造方式
    /// </summary>
    public class tonghui2818b : IDefaultAction, IDisposable
    {
        private SerialPort _sr;
        private testcase_dll tc;
        private string id = "";
        private const int TIMEOUT = 5000;

        public tonghui2818b(testcase_dll _tc, string comPortName, int baudRate = 9600)
        {
            _sr = new SerialPort(comPortName, baudRate, Parity.None, 8, StopBits.One);
            _sr.RtsEnable = true;
            _sr.ReadTimeout = TIMEOUT;
            _sr.WriteTimeout = TIMEOUT;
            _sr.NewLine = "\r\n";
            tc = _tc;
            InsertDefaultAction();
            OpenPorts();
        }

        public void OpenPorts()
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    if (!_sr.IsOpen)
                        _sr.Open();

                    SendCmd("FUNC:IMP CPD");          // Cp-D 模式
                    SendCmd("FUNC:IMP:RANG:AUTO ON");  // 自动量程
                    SendCmd($"FREQ 100HZ");            // 频率
                    SendCmd($"VOLT 1V");           // 电平
                    SendCmd($"APER MED");           // 速度
                    SendCmd("TRIG:SOUR BUS");           // BUS 触发
          

                });
                 
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b open port error: {ex.Message}");
            }
            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "th2818_";
            tc.funcs.Add(id + "read_cap", read_cap);
            tc.funcs.Add(id + "open_cal", open_cal);
            tc.funcs.Add(id + "short_cal", short_cal);
        }

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void ClosePorts()
        {
            if (_sr != null && _sr.IsOpen)
                _sr.Close();
        }

        // ── 底层 SCPI 通信 ───────────────────────────────────────────

        /// <summary>
        /// 发送命令（不等待应答）
        /// </summary>
        private void SendCmd(string cmd)
        {
            try
            {
                if (!_sr.IsOpen) _sr.Open();
                _sr.DiscardInBuffer();
                _sr.WriteLine(cmd);
              
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b SendCmd error: {ex.Message}");
            }
        }

        /// <summary>
        /// 发送查询命令并读取应答
        /// </summary>
        private string Query(string cmd)
        {
            try
            {
                if (!_sr.IsOpen) _sr.Open();
                _sr.DiscardInBuffer();
                _sr.WriteLine(cmd);
                return _sr.ReadLine().Trim();
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b Query error: {ex.Message}");
                return "";
            }
        }

        // ── 公开 API（供 C# 代码直接调用）────────────────────────────

        /// <summary>
        /// 读取电容值（Cp-D 模式）
        /// </summary>
        /// <param name="freq">测试频率，默认 100Hz</param>
        /// <param name="level">测试电平，默认 1V</param>
        /// <param name="speed">测试速度: FAST/MED/SLOW，默认 MED</param>
        /// <returns>电容值（F），失败返回 double.MinValue</returns>
        public double ReadCapacitance(string freq = "100HZ", string level = "1V", string speed = "MED",bool sikpset=false)
        {
            try
            {
                if (!_sr.IsOpen) _sr.Open();
                if (sikpset) { 
                SendCmd("FUNC:IMP CPD");          // Cp-D 模式
                SendCmd("FUNC:IMP:RANG:AUTO ON");  // 自动量程
                SendCmd($"FREQ {freq}");            // 频率
                SendCmd($"VOLT {level}");           // 电平
                SendCmd($"APER {speed}");           // 速度
                SendCmd("TRIG:SOUR BUS");           // BUS 触发
                }
                string resp = Query("TRIG\r\nFETC?");
              
                if (string.IsNullOrEmpty(resp)) return double.MinValue;

                // FETCh? 返回: SN.NNNNNESNN,SN.NNNNNESNN,SN,SN
                // 第一部分 = 主参数（电容值）
                string[] parts = resp.Split(',');
                if (parts.Length < 1) return double.MinValue;

                if (double.TryParse(parts[0],
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double val))
                    return val;

                return double.MinValue;
            }
            catch
            {
                return double.MinValue;
            }
        }

        /// <summary>
        /// 开路校准（全频点）
        /// </summary>
        public void OpenCal()
        {
            SendCmd("CORR:OPEN");
            SendCmd("CORR:OPEN:STAT ON");
            System.Threading.Thread.Sleep(1500);
        }

        /// <summary>
        /// 短路校准（全频点）
        /// </summary>
        public void ShortCal()
        {
            SendCmd("CORR:SHOR");
            SendCmd("CORR:SHOR:STAT ON");
            SendCmd("CORR:LOAD:STAT OFF");

            System.Threading.Thread.Sleep(1500);
        }

        /// <summary>
        /// 清除所有校准数据
        /// </summary>
        public void ClearCal()
        {
            SendCmd("CORR:CLE");
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// 查询设备 ID
        /// </summary>
        public string GetId()
        {
            return Query("*IDN?");
        }

        // ── 注册给测试序列调用的函数（string,string,out string,string 模式）──

        /// <summary>
        /// 测试序列中读取电容值
        /// d 格式: "freq,level,speed"（可选），默认 "100HZ,1V,MED"
        /// </summary>
        private string read_cap(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string freq = "100HZ", level = "1V", speed = "MED";
                bool setskip = false;
                if (!string.IsNullOrEmpty(d))
                {
                    string[] p = d.Trim().Split(",".ToArray());
                    if (p.Length > 0 && !string.IsNullOrEmpty(p[0])) freq = p[0].Trim();
                    if (p.Length > 1 && !string.IsNullOrEmpty(p[1])) level = p[1].Trim();
                    if (p.Length > 2 && !string.IsNullOrEmpty(p[2])) speed = p[2].Trim();
                   setskip = true;
                }
                else
                {
                    setskip = false;
                }

                double val = ReadCapacitance(freq, level, speed,setskip);
                if (val == double.MinValue)
                {
                    c = "read_error";
                    return "fail";
                }

                c = $"{val:F6}";
                if (double.Parse(a) >= val && double.Parse(b) <= val)
                {
                    return "pass";
                }
                else {

                    return "fail";
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b read_cap error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 开路校准
        /// </summary>
        private string open_cal(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                OpenCal();
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b open_cal error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 短路校准
        /// </summary>
        private string short_cal(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                ShortCal();
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"tonghui2818b short_cal error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        public void set_default_set()
        {
            try
            {
                ClosePorts();
                if (_sr != null)
                    _sr.Open();
            }
            catch { }
        }

        public void Dispose()
        {
            try
            {
                ClosePorts();
                _sr?.Dispose();
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
