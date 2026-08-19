using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Renci.SshNet;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    public class hero_final_assembly : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "hero_final_";
        SshClient ssh;
        bool ssh_connected = false;
        SerialPort ubootPort;
        StringBuilder ubootRx = new StringBuilder();
        object ubootLock = new object();
        const string UbootPromptPattern = @"=>\s*$";

        public hero_final_assembly(testcase_dll _tc)
        {
            tc = _tc;
            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                string host = ini["setport"]["hero_ssh_host"];
                int port = int.Parse(ini["setport"]["hero_ssh_port"] ?? "22");
                string user = ini["setport"]["hero_ssh_user"] ?? "root";
                string pass = ini["setport"]["hero_ssh_password"] ?? "EcoTest";
                ssh = new SshClient(host, port, user, pass);
                ssh.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                ssh.Connect();
                ssh_connected = true;
                utility_func.callbackdebuginfo($"[HERO_FINAL] SSH connected to {host}:{port}");

                string sport = ini["setport"]["hero_uboot_serial_port"];
                int sbaud = int.Parse(ini["setport"]["hero_uboot_serial_baudrate"] ?? "115200");
                ubootPort = new SerialPort(sport, sbaud, Parity.None, 8, StopBits.One);
                ubootPort.ReadTimeout = 5000;
                ubootPort.WriteTimeout = 2000;
                ubootPort.NewLine = "\r\n";
                ubootPort.Encoding = Encoding.UTF8;
                ubootPort.DataReceived += UbootPort_DataReceived;
                ubootPort.Open();
                utility_func.callbackdebuginfo($"[HERO_FINAL] u-boot serial connected: {sport}@{sbaud}");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] init error: {ex.Message}");
            }
            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            tc.funcs.Add(id + "ssh_exec", ssh_exec);
            tc.funcs.Add(id + "ssh_login", ssh_login);
            tc.funcs.Add(id + "ssh_exec_wait_reboot", ssh_exec_wait_reboot);
            tc.funcs.Add(id + "serial_wait_text", serial_wait_text);
            tc.funcs.Add(id + "uboot_interrupt", uboot_interrupt);
            tc.funcs.Add(id + "uboot_exec", uboot_exec);

            tc.funcs.Add(id + "measure_voltage", measure_voltage);
            tc.funcs.Add(id + "measure_current", measure_current);
            tc.funcs.Add(id + "measure_resistance", measure_resistance);

            tc.funcs.Add(id + "manual_prompt", manual_prompt);
            tc.funcs.Add(id + "manual_confirm", manual_confirm);

            tc.funcs.Add(id + "ssh_then_measure", ssh_then_measure);
            tc.funcs.Add(id + "manual_then_ssh", manual_then_ssh);
            tc.funcs.Add(id + "power_off_scan", power_off_scan);

            // ── 电源控制与产品连接验证 ──
            tc.funcs.Add(id + "power_on", power_on);
            tc.funcs.Add(id + "power_off", power_off);
            tc.funcs.Add(id + "product_connect", product_connect);
            tc.funcs.Add(id + "power_cycle", power_cycle);
            tc.funcs.Add(id + "get_psu_current", get_psu_current);
        }

        private string ssh_exec(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string cmd = get_required(p, "cmd");
                string expect = get_optional(p, "expect", "");
                int timeout = get_int(p, "timeout", 15000);
                if (!ssh_connected) { utility_func.callbackdebuginfo("[HERO_FINAL] SSH not connected"); return "fail"; }
                var sw = Stopwatch.StartNew();
                using var sc = ssh.CreateCommand(cmd);
                sc.CommandTimeout = TimeSpan.FromMilliseconds(timeout);
                var result = sc.Execute();
                var output = new StringBuilder();
                if (!string.IsNullOrEmpty(result)) output.Append(result);
                if (!string.IsNullOrEmpty(sc.Error)) output.Append(sc.Error);
                c = output.ToString().TrimEnd();
                sw.Stop();
                utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_exec: {cmd} => {c.Substring(0, Math.Min(c.Length, 200))} ({sw.ElapsedMilliseconds}ms)");
                if (!string.IsNullOrEmpty(expect)) { if (!Regex.IsMatch(c, expect, RegexOptions.None, TimeSpan.FromSeconds(1))) return "fail"; }
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_exec error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string ssh_login(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ssh_connected) { utility_func.callbackdebuginfo("[HERO_FINAL] SSH not connected"); return "fail"; }
                using var sc = ssh.CreateCommand("echo HERO_SSH_OK");
                sc.CommandTimeout = TimeSpan.FromSeconds(5);
                var result = sc.Execute();
                if (result.Contains("HERO_SSH_OK")) { c = "CONNECTED"; return "pass"; }
                c = ""; return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_login error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string ssh_exec_wait_reboot(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string cmd = get_required(p, "cmd");
                int rebootTimeout = get_int(p, "timeout", 120000);
                utility_func.callbackdebuginfo($"[HERO_FINAL] sending reboot cmd: {cmd}");
                try { using var sc = ssh.CreateCommand(cmd); sc.CommandTimeout = TimeSpan.FromSeconds(5); sc.Execute(); }
                catch { }
                ssh_connected = false;
                if (ubootPort != null && ubootPort.IsOpen)
                {
                    clear_uboot_rx();
                    var deadline = DateTime.UtcNow.AddMilliseconds(rebootTimeout);
                    while (DateTime.UtcNow < deadline)
                    {
                        Thread.Sleep(200);
                        lock (ubootLock) { if (Regex.IsMatch(ubootRx.ToString(), @"login:|Hit any key", RegexOptions.None, TimeSpan.FromSeconds(1))) { c = "REBOOTED"; return "pass"; } }
                    }
                }
                else { Thread.Sleep(Math.Min(rebootTimeout, 90000)); }
                c = "REBOOTED"; return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_exec_wait_reboot error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string serial_wait_text(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string text = get_required(p, "text");
                int timeout = get_int(p, "timeout", 10000);
                if (ssh_connected)
                {
                    var deadline = DateTime.UtcNow.AddMilliseconds(timeout);
                    while (DateTime.UtcNow < deadline)
                    {
                        try { using var sc = ssh.CreateCommand($"dmesg | grep -i '{text}'"); sc.CommandTimeout = TimeSpan.FromSeconds(3); var result = sc.Execute(); if (!string.IsNullOrEmpty(result) && result.Contains(text)) { c = text; return "pass"; } }
                        catch { }
                        Thread.Sleep(500);
                    }
                }
                if (ubootPort != null && ubootPort.IsOpen)
                {
                    var deadline = DateTime.UtcNow.AddMilliseconds(timeout);
                    while (DateTime.UtcNow < deadline) { Thread.Sleep(100); lock (ubootLock) { if (ubootRx.ToString().Contains(text)) { c = text; return "pass"; } } }
                }
                c = ""; return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] serial_wait_text error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string uboot_interrupt(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int timeout = get_int(p, "timeout", 30000);
                if (ubootPort == null || !ubootPort.IsOpen) { utility_func.callbackdebuginfo("[HERO_FINAL] u-boot serial not open"); return "fail"; }
                clear_uboot_rx();
                var deadline = DateTime.UtcNow.AddMilliseconds(timeout);
                while (DateTime.UtcNow < deadline)
                {
                    ubootPort.Write(new[] { ' ' }, 0, 1);
                    Thread.Sleep(100);
                    lock (ubootLock) { if (Regex.IsMatch(ubootRx.ToString(), UbootPromptPattern, RegexOptions.None, TimeSpan.FromSeconds(1))) { c = "UBOOT_PROMPT"; utility_func.callbackdebuginfo("[HERO_FINAL] u-boot interrupted successfully"); return "pass"; } }
                }
                c = ""; return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] uboot_interrupt error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string uboot_exec(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string cmd = get_required(p, "cmd");
                string expect = get_optional(p, "expect", "");
                int timeout = get_int(p, "timeout", 30000);
                if (ubootPort == null || !ubootPort.IsOpen) { utility_func.callbackdebuginfo("[HERO_FINAL] u-boot serial not open"); return "fail"; }
                clear_uboot_rx();
                lock (ubootLock) { ubootPort.WriteLine(cmd); }
                var deadline = DateTime.UtcNow.AddMilliseconds(timeout);
                while (DateTime.UtcNow < deadline)
                {
                    Thread.Sleep(50);
                    lock (ubootLock)
                    {
                        var data = ubootRx.ToString();
                        if (Regex.IsMatch(data, UbootPromptPattern, RegexOptions.None, TimeSpan.FromSeconds(1)))
                        {
                            c = clean_uboot_output(data, cmd);
                            utility_func.callbackdebuginfo($"[HERO_FINAL] uboot_exec: {cmd} => {c.Substring(0, Math.Min(c.Length, 200))}");
                            if (!string.IsNullOrEmpty(expect)) { if (!Regex.IsMatch(c, expect, RegexOptions.None, TimeSpan.FromSeconds(1))) return "fail"; }
                            return "pass";
                        }
                    }
                }
                c = ""; return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] uboot_exec error: {ex.Message}"); c = "error"; return "fail"; }
        }

        // ── 测量封装 ──

        private string measure_voltage(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_DC_200V" : d;
                utility_func.callbackdebuginfo($"[HERO_FINAL] measure_voltage via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] measure_voltage error: {ex.Message}"); c = "error"; return "fail"; }
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
                        utility_func.callbackdebuginfo($"[HERO_FINAL] measure_current: {cur}A PASS");
                        c = cur;
                        return "pass";
                    }
                    utility_func.callbackdebuginfo($"[HERO_FINAL] measure_current retry {cont + 1}/{count}: {cur}A");
                    Thread.Sleep(1000);
                } while (cont++ < count);
                c = cur;
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] measure_current error: {ex.Message}"); c = "error"; }
            return "fail";
        }

        private string measure_resistance(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_resistance" : d;
                utility_func.callbackdebuginfo($"[HERO_FINAL] measure_resistance via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] measure_resistance error: {ex.Message}"); c = "error"; return "fail"; }
        }

        // ── 人工交互封装 ──

        private string manual_prompt(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请执行人工操作后点击确定");
                HeroPromptForm.Show("HERO 总成 人工操作提示", msg, imgPath, false);
                c = "confirmed";
                utility_func.callbackdebuginfo($"[HERO_FINAL] manual_prompt: {msg} -> confirmed");
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] manual_prompt error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string manual_confirm(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请确认测试结果是否正常?");
                var result = HeroPromptForm.Show("HERO 总成 人工确认", msg, imgPath, true);
                if (result == DialogResult.OK) { c = "yes"; utility_func.callbackdebuginfo($"[HERO_FINAL] manual_confirm: {msg} -> YES"); return "pass"; }
                c = "no";
                utility_func.callbackdebuginfo($"[HERO_FINAL] manual_confirm: {msg} -> NO");
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] manual_confirm error: {ex.Message}"); c = "error"; return "fail"; }
        }

        // ── 组合步骤封装 ──

        private string ssh_then_measure(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string measureFunc = get_optional(p, "measure", "md3058_read_DC_200V");
                string commOut;
                if (ssh_exec("", "", out commOut, d) != "pass")
                {
                    c = $"comm_fail;{commOut}";
                    utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_then_measure: SSH failed: {commOut}");
                    return "fail";
                }
                utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_then_measure: measuring via {measureFunc}");
                return tc.funcs[measureFunc](a, b, out c, "");
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] ssh_then_measure error: {ex.Message}"); c = "error"; return "fail"; }
        }

        private string manual_then_ssh(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_required(p, "prompt");
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO 总成 人工操作", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_FINAL] manual_then_ssh: prompted '{prompt}'");
                return ssh_exec(a, b, out c, d);
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] manual_then_ssh error: {ex.Message}"); c = "error"; return "fail"; }
        }

        // ── 电源控制与产品连接验证 ──

        /// <summary>
        /// 上电 — 通过 TH6300 电源设定电压电流并开启输出。
        /// 参考 Domeitc_CPV_599_project.set_poewer_cur_vol 模式。
        ///
        /// 流程: 设定电压电流 → 开启电源输出 → 等待产品启动
        ///
        /// d 参数: voltage=电压(默认12.0);current=电流(默认5.0);boot_delay=启动等待ms(默认5000)
        /// a/b = 无效, c = "pass@X.XV" 或 "fail;reason"
        /// </summary>
        private string power_on(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                double voltage = double.Parse(get_optional(p, "voltage", "12.0"), CultureInfo.InvariantCulture);
                double current = double.Parse(get_optional(p, "current", "5.0"), CultureInfo.InvariantCulture);
                int bootDelay = get_int(p, "boot_delay", 5000);

                // Step 1: 设定电压电流
                string setOut;
                if (tc.funcs["th6300_dc_powersupply_set"](
                    $"{voltage.ToString("F2", CultureInfo.InvariantCulture)};{current.ToString("F2", CultureInfo.InvariantCulture)}",
                    "", out setOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_FINAL] power_on: set voltage/current failed: {setOut}");
                    c = "fail;set_error";
                    return "fail";
                }

                // Step 2: 开启电源输出
                string onOffOut;
                if (tc.funcs["th6300_dc_powersupply_on_off"]("ON", "", out onOffOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_FINAL] power_on: power on failed: {onOffOut}");
                    c = "fail;on_error";
                    return "fail";
                }

                utility_func.callbackdebuginfo($"[HERO_FINAL] power_on: {voltage}V/{current}A ON, waiting {bootDelay}ms for boot");
                Thread.Sleep(bootDelay);
                c = $"pass@{voltage:F1}V";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] power_on error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 断电 — 通过 TH6300 电源关闭输出，并清理连接状态。
        ///
        /// 流程: 关闭电源输出 → 标记SSH断开 → 清空串口缓冲
        ///
        /// d 参数: settle_delay=断电后稳定等待ms(默认1000)
        /// a/b = 无效, c = "pass" 或 "fail;reason"
        /// </summary>
        private string power_off(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int settleDelay = get_int(p, "settle_delay", 1000);

                // 关闭电源输出
                string onOffOut;
                if (tc.funcs["th6300_dc_powersupply_on_off"]("OFF", "", out onOffOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_FINAL] power_off: power off failed: {onOffOut}");
                    c = "fail;off_error";
                    return "fail";
                }

                // 标记 SSH 断开 (产品已断电，连接必然失效)
                ssh_connected = false;

                // 清空 u-boot 串口缓冲
                if (ubootPort != null && ubootPort.IsOpen)
                    clear_uboot_rx();

                utility_func.callbackdebuginfo($"[HERO_FINAL] power_off: power OFF, SSH marked disconnected, waiting {settleDelay}ms");
                Thread.Sleep(settleDelay);
                c = "pass";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] power_off error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 产品连接验证 — 上电后等待产品启动并验证可通过 SSH 通讯。
        ///
        /// 流程: 尝试SSH重连 → echo验证 → (可选)串口等待启动信息
        ///
        /// d 参数: retry=重试次数(默认10);interval=重试间隔ms(默认3000);
        ///         ssh_timeout=SSH连接超时ms(默认15000);
        ///         serial_wait=是否等待串口启动信息(默认true);
        ///         serial_text=串口等待文本(默认"login:")
        /// a/b = 无效, c = "pass@attemptN" 或 "fail;reason"
        /// </summary>
        private string product_connect(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int retryCount = get_int(p, "retry", 10);
                int retryInterval = get_int(p, "interval", 3000);
                int sshTimeout = get_int(p, "ssh_timeout", 15000);
                bool serialWait = get_optional(p, "serial_wait", "true").Equals("true", StringComparison.OrdinalIgnoreCase);
                string serialText = get_optional(p, "serial_text", "login:");

                utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: waiting for product (retry={retryCount}, interval={retryInterval}ms)");

                // 可选: 先等待串口出现启动信息
                if (serialWait && ubootPort != null && ubootPort.IsOpen)
                {
                    clear_uboot_rx();
                    var serialDeadline = DateTime.UtcNow.AddMilliseconds(retryCount * retryInterval);
                    bool serialReady = false;
                    while (DateTime.UtcNow < serialDeadline)
                    {
                        Thread.Sleep(200);
                        lock (ubootLock)
                        {
                            if (ubootRx.ToString().Contains(serialText))
                            {
                                serialReady = true;
                                break;
                            }
                        }
                    }
                    utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: serial {(serialReady ? "ready" : "timeout")} (text='{serialText}')");
                }

                // 尝试 SSH 重连 + 验证
                for (int i = 0; i < retryCount; i++)
                {
                    // 如果 SSH 未连接，尝试重连
                    if (!ssh_connected || (ssh != null && !ssh.IsConnected))
                    {
                        try
                        {
                            if (ssh != null)
                            {
                                ssh.ConnectionInfo.Timeout = TimeSpan.FromMilliseconds(sshTimeout);
                                ssh.Connect();
                                ssh_connected = true;
                                utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: SSH reconnected on attempt {i + 1}");
                            }
                        }
                        catch (Exception exSsh)
                        {
                            utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: SSH reconnect attempt {i + 1} failed: {exSsh.Message}");
                            Thread.Sleep(retryInterval);
                            continue;
                        }
                    }

                    // 验证连接 (echo 测试)
                    string loginOut;
                    if (ssh_login("", "", out loginOut, d) == "pass")
                    {
                        utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: product connected on attempt {i + 1}");
                        c = $"pass@attempt{i + 1}";
                        return "pass";
                    }

                    utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect: attempt {i + 1}/{retryCount} login failed, retrying...");
                    Thread.Sleep(retryInterval);
                }

                c = "fail;no_response";
                utility_func.callbackdebuginfo("[HERO_FINAL] product_connect: product did not respond after all retries");
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] product_connect error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 换产品流程 — 断电→弹窗提示换产品→上电→验证连接。
        ///
        /// 标准换件流程封装，一次调用完成完整的换件+连接验证。
        ///
        /// d 参数: prompt=提示文本(默认"请更换产品后点击确定");
        ///         voltage=上电电压(默认12.0);current=上电电流(默认5.0);
        ///         boot_delay=启动等待ms(默认5000);
        ///         retry=连接重试次数(默认10);interval=重试间隔ms(默认3000)
        /// a/b = 无效, c = "pass@attemptN" 或 "fail;reason"
        /// </summary>
        private string power_cycle(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_optional(p, "prompt", "请更换产品后点击确定");

                // Step 1: 断电
                string offOut;
                if (power_off("", "", out offOut, d) != "pass")
                {
                    c = $"fail;power_off_failed;{offOut}";
                    return "fail";
                }

                // Step 2: 弹窗提示操作员换产品
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO 总成 换件提示", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_FINAL] power_cycle: prompted '{prompt}'");

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
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] power_cycle error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 读取电源电流 — 从 TH6300 电源直接读取输出电流。
        ///
        /// d 参数: 无
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
                    utility_func.callbackdebuginfo($"[HERO_FINAL] get_psu_current: {cur:F4}A");
                    return judge_range(cur, a, b);
                }
                c = "parse_error";
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_FINAL] get_psu_current error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 多阶段降压扫描找关机点 — 参考 power_off_test_search 模式。
        /// 通过 TH6300 电源直接控制输出电压逐步降低，监测电流变化，
        /// 当电流低于关机阈值时判定关机电压是否在合格范围内。
        ///
        /// d 格式: "start_v=<起始电压>;range_min=<下限>;range_max=<上限>;step=<降压步进>;shutdown_cur=<关机电流>;interval=<采样间隔ms>;psu_current=<电源电流限值(默认10.0)>;use_psu_measure=<是否用电源读电流(默认true)>;measure=<DMM函数名>"
        /// a/b = 无效, c = "pass@X.XXV" 或 "fail;reason"
        /// </summary>
        private string power_off_scan(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                double startV = double.Parse(get_optional(p, "start_v", "12.0"), CultureInfo.InvariantCulture);
                double rangeMin = double.Parse(get_optional(p, "range_min", "9.4"), CultureInfo.InvariantCulture);
                double rangeMax = double.Parse(get_optional(p, "range_max", "10.0"), CultureInfo.InvariantCulture);
                double step = double.Parse(get_optional(p, "step", "0.1"), CultureInfo.InvariantCulture);
                double shutdownCur = double.Parse(get_optional(p, "shutdown_cur", "0.15"), CultureInfo.InvariantCulture);
                int interval = get_int(p, "interval", 1000);
                string psuCur = get_optional(p, "psu_current", "10.0");
                bool usePsuMeasure = get_optional(p, "use_psu_measure", "true").Equals("true", StringComparison.OrdinalIgnoreCase);
                string measureFunc = get_optional(p, "measure", "md3058_read_DC_10A");
                int maxFail = 3;

                utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan: start={startV}V, range=[{rangeMin},{rangeMax}], step={step}V, shutdown_cur={shutdownCur}A, measure={(usePsuMeasure ? "PSU" : measureFunc)}");

                string curStr = "";
                int consecutiveFail = 0;

                for (double v = startV; v >= rangeMin - 0.001; v -= step)
                {
                    if (v < rangeMin) v = rangeMin;

                    // 通过 TH6300 电源直接设定输出电压
                    string setOut;
                    if (tc.funcs["th6300_dc_powersupply_set"](
                        $"{v.ToString("F2", CultureInfo.InvariantCulture)};{psuCur}",
                        "", out setOut, "") != "pass")
                    {
                        utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan: set voltage failed at {v:F2}V: {setOut}");
                        consecutiveFail++;
                        if (consecutiveFail >= maxFail) { c = "fail;psu_error"; return "fail"; }
                        continue;
                    }

                    consecutiveFail = 0;
                    Thread.Sleep(interval);

                    // 读取电流: 优先使用 TH6300 电源电流, 可选 DMM
                    double cur;
                    if (usePsuMeasure)
                    {
                        string curOut;
                        tc.funcs["th6300_dc_powersupply_get_current"]("9999", "-9999", out curOut, "");
                        if (!double.TryParse(curOut, NumberStyles.Any, CultureInfo.InvariantCulture, out cur))
                        {
                            consecutiveFail++;
                            if (consecutiveFail >= maxFail) { c = "fail;psu_read_error"; return "fail"; }
                            continue;
                        }
                    }
                    else
                    {
                        string measureRet = tc.funcs[measureFunc]("9999", "-9999", out curStr, "");
                        if (!double.TryParse(curStr, NumberStyles.Any, CultureInfo.InvariantCulture, out cur))
                        {
                            consecutiveFail++;
                            if (consecutiveFail >= maxFail) { c = "fail;measure_error"; return "fail"; }
                            continue;
                        }
                    }

                    utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan: {v:F2}V - cur={cur:F3}A");

                    if (cur < shutdownCur)
                    {
                        if (v >= rangeMin && v <= rangeMax)
                        {
                            utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan: PASS shutdown at {v:F2}V, cur={cur:F3}A");
                            c = $"pass@{v:F2}V";
                            return "pass";
                        }
                        else
                        {
                            utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan: NG shutdown at {v:F2}V out of range");
                            c = $"fail;shutdown_voltage_out_of_range {v:F2}V";
                            return "fail";
                        }
                    }

                    if (Math.Abs(v - rangeMin) < 0.001) break;
                }

                c = "fail;no_shutdown";
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] power_off_scan error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 接口实现 ──

        public void InsertDefaultAction() { tc.dev_moren[id] = this; }
        public void set_default_set() { }

        // ── 串口事件与工具 ──

        private void UbootPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType != SerialData.Chars) return;
            try { var data = ubootPort.ReadExisting(); lock (ubootLock) ubootRx.Append(data); }
            catch { }
        }

        private void clear_uboot_rx() { lock (ubootLock) ubootRx.Clear(); }

        private static string clean_uboot_output(string raw, string command)
        {
            var lines = raw.Split('\n', '\r');
            var result = new StringBuilder();
            foreach (var line in lines)
            {
                var trimmed = line.Trim();
                if (string.IsNullOrEmpty(trimmed)) continue;
                if (trimmed == command.Trim()) continue;
                if (Regex.IsMatch(trimmed, @"^[$#=>]\s*$")) continue;
                if (Regex.IsMatch(trimmed, @"^\w+@[\w\-]+:.*[$#]\s*$")) continue;
                result.AppendLine(trimmed);
            }
            return result.ToString().TrimEnd();
        }

        // ── 范围判定工具 ──

        private static string judge_range(double value, string a, string b)
        {
            if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b)) return "pass";
            double upper = double.Parse(a, CultureInfo.InvariantCulture);
            double lower = double.Parse(b, CultureInfo.InvariantCulture);
            return value <= upper && value >= lower ? "pass" : "fail";
        }

        // ── 参数解析工具 ──

        private static Dictionary<string, string> parse_d(string d)
        {
            var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrEmpty(d)) return result;
            foreach (var pair in d.Split(';', StringSplitOptions.RemoveEmptyEntries))
            {
                int eq = pair.IndexOf('=');
                if (eq > 0) result[pair.Substring(0, eq).Trim()] = pair.Substring(eq + 1).Trim();
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

        // ── 资源释放 ──

        public void Dispose()
        {
            try
            {
                if (ssh != null) { if (ssh.IsConnected) ssh.Disconnect(); ssh.Dispose(); }
                if (ubootPort != null) { if (ubootPort.IsOpen) ubootPort.Close(); ubootPort.Dispose(); }
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
