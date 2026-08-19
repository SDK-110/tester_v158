using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using NModbus;
using NModbus.Serial;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    public class hero_input_board : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "hero_input_";
        SerialPort modbusPort;
        IModbusMaster master;
        byte slaveId = 40;
        int retryCount = 2;
        int readTimeout = 3000;

        public hero_input_board(testcase_dll _tc)
        {
            tc = _tc;
            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                string port = ini["setport"]["hero_modbus_port"];
                int baud = int.Parse(ini["setport"]["hero_modbus_baudrate"] ?? "115200");
                slaveId = byte.Parse(ini["setport"]["hero_modbus_slave_id"] ?? "40");

                modbusPort = new SerialPort(port, baud, Parity.None, 8, StopBits.One);
                modbusPort.ReadTimeout = readTimeout;
                modbusPort.WriteTimeout = 2000;
                modbusPort.Open();

                var adapter = new SerialPortAdapter(modbusPort);
                var factory = new ModbusFactory();
                master = factory.CreateRtuMaster(adapter);

                utility_func.callbackdebuginfo($"[HERO_INPUT] Modbus RTU connected: {port}@{baud}, SlaveId={slaveId}");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] Modbus init error: {ex.Message}");
            }

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            tc.funcs.Add(id + "read_input_reg", read_input_reg);
            tc.funcs.Add(id + "read_input_reg_float", read_input_reg_float);
            tc.funcs.Add(id + "read_holding_reg", read_holding_reg);
            tc.funcs.Add(id + "read_coil", read_coil);
            tc.funcs.Add(id + "write_holding_reg", write_holding_reg);
            tc.funcs.Add(id + "write_coil", write_coil);

            tc.funcs.Add(id + "measure_voltage", measure_voltage);
            tc.funcs.Add(id + "measure_current", measure_current);
            tc.funcs.Add(id + "measure_resistance", measure_resistance);

            tc.funcs.Add(id + "manual_prompt", manual_prompt);
            tc.funcs.Add(id + "manual_confirm", manual_confirm);

            tc.funcs.Add(id + "modbus_then_measure", modbus_then_measure);
            tc.funcs.Add(id + "manual_then_modbus", manual_then_modbus);
            tc.funcs.Add(id + "conductivity_probe_test", conductivity_probe_test);

            // ── 电源控制与产品连接验证 ──
            tc.funcs.Add(id + "power_on", power_on);
            tc.funcs.Add(id + "power_off", power_off);
            tc.funcs.Add(id + "product_connect", product_connect);
            tc.funcs.Add(id + "power_cycle", power_cycle);
            tc.funcs.Add(id + "get_psu_current", get_psu_current);
        }

        private string read_input_reg(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                ushort value = exec_with_retry(() => master!.ReadInputRegistersAsync(slaveId, addr, 1).Result[0]);
                c = value.ToString();
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_input_reg addr={addr} => {value}");
                return judge_range(value, a, b);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_input_reg error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string read_input_reg_float(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                ushort[] regs = exec_with_retry(() => master!.ReadInputRegistersAsync(slaveId, addr, 2).Result);
                float value = BitConverter.ToSingle(BitConverter.GetBytes(regs[0] << 16 | regs[1]), 0);
                c = value.ToString("F4", CultureInfo.InvariantCulture);
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_input_reg_float addr={addr} => {value:F4}");
                return judge_range(value, a, b);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_input_reg_float error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string read_holding_reg(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                ushort value = exec_with_retry(() => master!.ReadHoldingRegistersAsync(slaveId, addr, 1).Result[0]);
                c = value.ToString();
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_holding_reg addr={addr} => {value}");
                return judge_range(value, a, b);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_holding_reg error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string read_coil(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                bool value = exec_with_retry(() => master!.ReadCoilsAsync(slaveId, addr, 1).Result[0]);
                c = value ? "1" : "0";
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_coil addr={addr} => {c}");
                bool expected = a == "1" || a.Equals("true", StringComparison.OrdinalIgnoreCase);
                return value == expected ? "pass" : "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] read_coil error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string write_holding_reg(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                ushort value = ushort.Parse(b);
                exec_with_retry(() => master!.WriteSingleRegisterAsync(slaveId, addr, value));
                Thread.Sleep(100);
                ushort readback = exec_with_retry(() => master!.ReadHoldingRegistersAsync(slaveId, addr, 1).Result[0]);
                c = readback.ToString();
                utility_func.callbackdebuginfo($"[HERO_INPUT] write_holding_reg addr={addr}, wrote={value}, readback={readback}");
                return judge_range(readback, a, b);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] write_holding_reg error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string write_coil(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                bool value = b == "1" || b.Equals("true", StringComparison.OrdinalIgnoreCase);
                exec_with_retry(() => master!.WriteSingleCoilAsync(slaveId, addr, value));
                Thread.Sleep(50);
                bool readback = exec_with_retry(() => master!.ReadCoilsAsync(slaveId, addr, 1).Result[0]);
                c = readback ? "1" : "0";
                utility_func.callbackdebuginfo($"[HERO_INPUT] write_coil addr={addr}, wrote={value}, readback={readback}");
                bool expected = a == "1" || a.Equals("true", StringComparison.OrdinalIgnoreCase);
                return readback == expected ? "pass" : "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] write_coil error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string measure_voltage(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_DC_200V" : d;
                utility_func.callbackdebuginfo($"[HERO_INPUT] measure_voltage via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] measure_voltage error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string measure_current(string a, string b, out string c, string d)
        {
            c = "fail";
            int count = string.IsNullOrEmpty(d) ? 30 : int.Parse(d);
            try
            {
                string cur = "";
                int cont = 0;
                do
                {
                    if (tc.funcs["md3058_read_DC_10A"](a, b, out cur, "") == "pass")
                    {
                        utility_func.callbackdebuginfo($"[HERO_INPUT] measure_current: {cur}A PASS");
                        c = cur;
                        return "pass";
                    }
                    utility_func.callbackdebuginfo($"[HERO_INPUT] measure_current retry {cont + 1}/{count}: {cur}A");
                    Thread.Sleep(1000);
                } while (cont++ < count);
                c = cur;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] measure_current error: {ex.Message}");
                c = "error";
            }
            return "fail";
        }

        private string measure_resistance(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_resistance" : d;
                utility_func.callbackdebuginfo($"[HERO_INPUT] measure_resistance via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] measure_resistance error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string manual_prompt(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请执行人工操作后点击确定");
                HeroPromptForm.Show("HERO Input 人工操作提示", msg, imgPath, false);
                c = "confirmed";
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_prompt: {msg} -> confirmed");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_prompt error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string manual_confirm(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请确认测试结果是否正常?");
                var result = HeroPromptForm.Show("HERO Input 人工确认", msg, imgPath, true);
                if (result == DialogResult.OK)
                {
                    c = "yes";
                    utility_func.callbackdebuginfo($"[HERO_INPUT] manual_confirm: {msg} -> YES");
                    return "pass";
                }
                c = "no";
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_confirm: {msg} -> NO");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_confirm error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string modbus_then_measure(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string type = get_optional(p, "type", "coil");
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                string measureFunc = get_optional(p, "measure", "md3058_read_DC_200V");
                int delay = get_int(p, "delay", 200);

                if (type == "coil")
                {
                    bool value = get_optional(p, "value", "0") == "1";
                    exec_with_retry(() => master!.WriteSingleCoilAsync(slaveId, addr, value));
                    utility_func.callbackdebuginfo($"[HERO_INPUT] modbus_then_measure: wrote coil addr={addr}={value}");
                }
                else
                {
                    ushort value = ushort.Parse(get_required(p, "value"));
                    exec_with_retry(() => master!.WriteSingleRegisterAsync(slaveId, addr, value));
                    utility_func.callbackdebuginfo($"[HERO_INPUT] modbus_then_measure: wrote holding addr={addr}={value}");
                }

                Thread.Sleep(delay);
                utility_func.callbackdebuginfo($"[HERO_INPUT] modbus_then_measure: measuring via {measureFunc}");
                return tc.funcs[measureFunc](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] modbus_then_measure error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string manual_then_modbus(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_optional(p, "prompt", "请确认操作后点击确定");
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO Input 人工操作", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_then_modbus: prompted '{prompt}'");
                return read_coil(a, b, out c, d);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] manual_then_modbus error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        private string conductivity_probe_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_optional(p, "prompt", "请设置电导探头开关位置后点击确定");
                ushort addr = (ushort)int.Parse(get_required(p, "addr"));
                string measureFunc = get_optional(p, "measure", "md3058_read_resistance");

                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO Input 电导探头测试", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_INPUT] conductivity_probe_test: prompted '{prompt}'");

                ushort regValue = exec_with_retry(() => master!.ReadInputRegistersAsync(slaveId, addr, 1).Result[0]);
                utility_func.callbackdebuginfo($"[HERO_INPUT] conductivity_probe_test: reg addr={addr} => {regValue}");

                string resistanceVal;
                string measureRet = tc.funcs[measureFunc](a, b, out resistanceVal, "");
                c = $"{regValue};{resistanceVal}";
                return measureRet;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_INPUT] conductivity_probe_test error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 电源控制与产品连接验证 ──

        /// <summary>
        /// 上电 — 通过 TH6300 电源设定电压电流并开启输出。
        /// d 参数: voltage=电压(默认12.0);current=电流(默认5.0);boot_delay=启动等待ms(默认3000)
        /// </summary>
        private string power_on(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                double voltage = double.Parse(get_optional(p, "voltage", "12.0"), CultureInfo.InvariantCulture);
                double current = double.Parse(get_optional(p, "current", "5.0"), CultureInfo.InvariantCulture);
                int bootDelay = get_int(p, "boot_delay", 3000);

                string setOut;
                if (tc.funcs["th6300_dc_powersupply_set"](
                    $"{voltage.ToString("F2", CultureInfo.InvariantCulture)};{current.ToString("F2", CultureInfo.InvariantCulture)}",
                    "", out setOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_INPUT] power_on: set voltage/current failed: {setOut}");
                    c = "fail;set_error";
                    return "fail";
                }

                string onOffOut;
                if (tc.funcs["th6300_dc_powersupply_on_off"]("ON", "", out onOffOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_INPUT] power_on: power on failed: {onOffOut}");
                    c = "fail;on_error";
                    return "fail";
                }

                utility_func.callbackdebuginfo($"[HERO_INPUT] power_on: {voltage}V/{current}A ON, waiting {bootDelay}ms for boot");
                Thread.Sleep(bootDelay);
                c = $"pass@{voltage:F1}V";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_INPUT] power_on error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 断电 — 通过 TH6300 关闭电源输出。
        /// d 参数: settle_delay=断电后稳定等待ms(默认1000)
        /// </summary>
        private string power_off(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int settleDelay = get_int(p, "settle_delay", 1000);

                string onOffOut;
                if (tc.funcs["th6300_dc_powersupply_on_off"]("OFF", "", out onOffOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_INPUT] power_off: power off failed: {onOffOut}");
                    c = "fail;off_error";
                    return "fail";
                }

                utility_func.callbackdebuginfo($"[HERO_INPUT] power_off: power OFF, waiting {settleDelay}ms");
                Thread.Sleep(settleDelay);
                c = "pass";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_INPUT] power_off error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 产品连接验证 — 上电后通过 Modbus 读取已知寄存器验证通讯。
        /// 如果串口未打开, 尝试重新打开。
        /// d 参数: retry=重试次数(默认10);interval=重试间隔ms(默认2000);test_addr=测试寄存器地址(默认0)
        /// </summary>
        private string product_connect(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int connRetry = get_int(p, "retry", 10);
                int retryInterval = get_int(p, "interval", 2000);
                ushort testAddr = (ushort)get_int(p, "test_addr", 0);

                // 如果串口未打开, 尝试重新打开
                if (modbusPort != null && !modbusPort.IsOpen)
                {
                    try
                    {
                        modbusPort.Open();
                        utility_func.callbackdebuginfo("[HERO_INPUT] product_connect: serial port reopened");
                    }
                    catch (Exception exOpen)
                    {
                        utility_func.callbackdebuginfo($"[HERO_INPUT] product_connect: failed to reopen serial: {exOpen.Message}");
                    }
                }

                utility_func.callbackdebuginfo($"[HERO_INPUT] product_connect: checking Modbus reg[{testAddr}], retry={connRetry}");

                for (int i = 0; i < connRetry; i++)
                {
                    try
                    {
                        ushort value = exec_with_retry(() => master!.ReadHoldingRegistersAsync(slaveId, testAddr, 1).Result[0]);
                        utility_func.callbackdebuginfo($"[HERO_INPUT] product_connect: Modbus OK, reg[{testAddr}]={value} on attempt {i + 1}");
                        c = $"pass@attempt{i + 1};reg={value}";
                        return "pass";
                    }
                    catch (Exception exModbus)
                    {
                        utility_func.callbackdebuginfo($"[HERO_INPUT] product_connect: attempt {i + 1}/{connRetry} failed: {exModbus.Message}");
                        Thread.Sleep(retryInterval);
                    }
                }

                c = "fail;no_modbus_response";
                utility_func.callbackdebuginfo("[HERO_INPUT] product_connect: no Modbus response after all retries");
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_INPUT] product_connect error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 换产品流程 — 断电→弹窗提示换产品→上电→验证连接。
        /// d 参数: prompt=提示文本;voltage=上电电压;current=上电电流;boot_delay=启动等待ms;retry=重试次数;interval=重试间隔ms
        /// </summary>
        private string power_cycle(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_optional(p, "prompt", "请更换 Input 板后点击确定");

                // Step 1: 断电
                string offOut;
                if (power_off("", "", out offOut, d) != "pass")
                {
                    c = $"fail;power_off_failed;{offOut}";
                    return "fail";
                }

                // Step 2: 弹窗提示操作员换产品
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO Input 换件提示", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_INPUT] power_cycle: prompted '{prompt}'");

                // Step 3: 上电
                string onOut;
                if (power_on("", "", out onOut, d) != "pass")
                {
                    c = $"fail;power_on_failed;{onOut}";
                    return "fail";
                }

                // Step 4: 验证产品连接
                return product_connect("", "", out c, d);
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_INPUT] power_cycle error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 读取电源电流 — 从 TH6300 电源直接读取输出电流。
        /// a = 上限, b = 下限, c = 实测电流值
        /// </summary>
        private string get_psu_current(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string curOut;
                tc.funcs["th6300_dc_powersupply_get_current"]("9999", "-9999", out curOut, "");
                double cur;
                if (double.TryParse(curOut, NumberStyles.Any, CultureInfo.InvariantCulture, out cur))
                {
                    c = cur.ToString("F4", CultureInfo.InvariantCulture);
                    utility_func.callbackdebuginfo($"[HERO_INPUT] get_psu_current: {cur:F4}A");
                    return judge_range(cur, a, b);
                }
                c = "parse_error";
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_INPUT] get_psu_current error: {ex.Message}"); c = "error"; return "fail"; }
        }

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void set_default_set()
        {
        }

        private T exec_with_retry<T>(Func<T> action)
        {
            Exception lastEx = null;
            for (int i = 0; i <= retryCount; i++)
            {
                try { return action(); }
                catch (Exception ex) { lastEx = ex; if (i < retryCount) Thread.Sleep(100); }
            }
            throw lastEx!;
        }

        private void exec_with_retry(Action action)
        {
            exec_with_retry(() => { action(); return true; });
        }

        private static string judge_range(double value, string upperStr, string lowerStr)
        {
            if (!double.TryParse(upperStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double upper))
                return "fail";
            if (!double.TryParse(lowerStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double lower))
                return "fail";
            return value >= lower && value <= upper ? "pass" : "fail";
        }

        private static Dictionary<string, string> parse_d(string d)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrEmpty(d)) return result;
            foreach (var pair in d.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                int eq = pair.IndexOf('=');
                if (eq > 0)
                    result[pair.Substring(0, eq).Trim()] = pair.Substring(eq + 1).Trim();
            }
            return result;
        }

        private static string get_required(Dictionary<string, string> p, string key)
        {
            if (!p.TryGetValue(key, out var val) || string.IsNullOrEmpty(val))
                throw new ArgumentException($"missing required param: {key}");
            return val;
        }

        private static string get_optional(Dictionary<string, string> p, string key, string def)
        {
            return p.TryGetValue(key, out var val) && !string.IsNullOrEmpty(val) ? val : def;
        }

        private static int get_int(Dictionary<string, string> p, string key, int def)
        {
            return p.TryGetValue(key, out var val) && int.TryParse(val, out var result) ? result : def;
        }

        public void Dispose()
        {
            try
            {
                modbusPort?.Close();
                modbusPort?.Dispose();
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
