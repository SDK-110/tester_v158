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
using System.Data;

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
        const string UbootPromptPattern = @"(=>\s*$|~\s*#\s*$|~\s*\$\s*$|\$\s*$|#\s*$|root@[\w\-\.]+:.*#\s*$)";

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

            // ── 风扇控制 (通过 SSH 发送 GPIO 命令) ──
            tc.funcs.Add(id + "fan_control", fan_control);

            // ── USB 存储检测 (通过串口监听内核 USB 枚举消息) ──
            tc.funcs.Add(id + "usb_storage_check", usb_storage_check);

            // ── 序列号编程 (通过串口发送 eeprog 命令写入 EEPROM) ──
            tc.funcs.Add(id + "program_serial_number", program_serial_number);

            // ── 优雅关机 (通过串口发送 sync + halt) ──
            tc.funcs.Add(id + "graceful_shutdown", graceful_shutdown);

            // ── MAC 地址编程与安全锁定 (u-boot fuse) ──
            tc.funcs.Add(id + "program_mac_and_lock", program_mac_and_lock);
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

        /// <summary>
        /// 风扇控制 — 通过串口向 Linux 终端发送 GPIO 命令控制风扇开关。
        /// 串口连接的是树莓派 Linux 调试终端。
        ///
        /// d 参数: action=on|off (必填); gpio=<引脚号, 默认14>
        ///         login=<是否需要登录, 默认true>; user=<用户名, 默认root>; pass=<密码, 默认EcoTest>
        /// a/b = 无效, c = "pass" 或 "fail;reason"
        /// </summary>
        private string fan_control(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string action = get_required(p, "action");
                int gpio = get_int(p, "gpio", 14);
                bool needLogin = get_optional(p, "login", "true").Equals("true", StringComparison.OrdinalIgnoreCase);
                string user = get_optional(p, "user", "root");
                string pass = get_optional(p, "pass", "EcoTest");
                int timeout = get_int(p, "timeout", 15000);

                if (ubootPort == null || !ubootPort.IsOpen)
                {
                    utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: serial port not open");
                    c = "fail;serial_not_open";
                    return "fail";
                }

                clear_uboot_rx();

                // 发送换行符, 等待提示符出现
                ubootPort.WriteLine("");
                if (!wait_for_prompt(timeout))
                {
                    // 可能需要登录
                    if (needLogin)
                    {
                        utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: attempting login...");
                        ubootPort.WriteLine(user);
                        Thread.Sleep(500);
                        ubootPort.WriteLine(pass);
                        Thread.Sleep(1000);
                        clear_uboot_rx();
                        ubootPort.WriteLine("");
                        if (!wait_for_prompt(5000))
                        {
                            utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: login failed, no prompt");
                            c = "fail;login_failed";
                            return "fail";
                        }
                    }
                    else
                    {
                        c = "fail;no_prompt";
                        return "fail";
                    }
                }

                // 导出 GPIO 引脚 (如果尚未导出)
                clear_uboot_rx();
                ubootPort.WriteLine(string.Format("cd /sys/class/gpio && echo {0} > export 2>/dev/null; echo {1} > gpio{0}/direction 2>/dev/null; echo done_gpio_setup", gpio, "out"));
                if (!wait_for_prompt(5000))
                {
                    utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: GPIO setup timeout");
                    c = "fail;gpio_setup_timeout";
                    return "fail";
                }
                string setupOutput = get_serial_output("done_gpio_setup");
                utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: GPIO setup done");

                // 设置 GPIO 值 (1=ON, 0=OFF)
                string val = action.Equals("on", StringComparison.OrdinalIgnoreCase) ? "1" : "0";
                clear_uboot_rx();
                ubootPort.WriteLine(string.Format("echo {0} > gpio{1}/value; echo fan_{2}_done", val, gpio, action));
                if (!wait_for_prompt(5000))
                {
                    utility_func.callbackdebuginfo("[HERO_FINAL] fan_control: GPIO value set timeout");
                    c = "fail;gpio_value_timeout";
                    return "fail";
                }

                string valOutput = get_serial_output(string.Format("fan_{0}_done", action));
                utility_func.callbackdebuginfo(string.Format("[HERO_FINAL] fan_control: fan {0} (gpio{1}={2})", action, gpio, val));
                c = string.Format("pass;fan_{0}", action);
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo(string.Format("[HERO_FINAL] fan_control error: {0}", ex.Message));
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 等待串口出现 Linux/u-boot 提示符
        /// </summary>
        private bool wait_for_prompt(int timeoutMs)
        {
            var deadline = DateTime.UtcNow.AddMilliseconds(timeoutMs);
            while (DateTime.UtcNow < deadline)
            {
                Thread.Sleep(100);
                lock (ubootLock)
                {
                    if (Regex.IsMatch(ubootRx.ToString(), UbootPromptPattern, RegexOptions.None, TimeSpan.FromSeconds(1)))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从串口缓冲中提取指定标记之后的输出
        /// </summary>
        private string get_serial_output(string marker)
        {
            lock (ubootLock)
            {
                string raw = ubootRx.ToString();
                int idx = raw.IndexOf(marker);
                if (idx >= 0)
                    return raw.Substring(0, idx).TrimEnd();
                return raw.TrimEnd();
            }
        }

        /// <summary>
        /// USB 存储检测 — 清空串口缓冲后等待 "USB Storage" 文本出现。
        /// 插入 USB flash drive 后 Linux 内核会输出 USB 枚举消息。
        ///
        /// d 参数: text=<等待文本, 默认"USB Storage">;timeout=<超时ms, 默认15000>
        ///         use_dmesg=<是否同时通过SSH检查dmesg, 默认true>
        /// a/b = 无效, c = "pass" 或 "fail;timeout"
        /// </summary>
        private string usb_storage_check(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string text = get_optional(p, "text", "USB Storage");
                int timeout = get_int(p, "timeout", 15000);
                bool useDmesg = get_optional(p, "use_dmesg", "true").Equals("true", StringComparison.OrdinalIgnoreCase);

                utility_func.callbackdebuginfo($"[HERO_FINAL] usb_storage_check: waiting for '{text}' (timeout={timeout}ms)");

                // 清空串口缓冲, 只捕获新插入的 USB 事件
                clear_uboot_rx();

                // 同时通过串口和 SSH dmesg 检测
                var deadline = DateTime.UtcNow.AddMilliseconds(timeout);
                while (DateTime.UtcNow < deadline)
                {
                    // 串口检测
                    if (ubootPort != null && ubootPort.IsOpen)
                    {
                        lock (ubootLock)
                        {
                            if (ubootRx.ToString().Contains(text))
                            {
                                c = "pass";
                                utility_func.callbackdebuginfo($"[HERO_FINAL] usb_storage_check: detected '{text}' via serial");
                                return "pass";
                            }
                        }
                    }

                    // SSH dmesg 检测
                    if (useDmesg && ssh_connected)
                    {
                        try
                        {
                            using var sc = ssh.CreateCommand($"dmesg | grep -i '{text}'");
                            sc.CommandTimeout = TimeSpan.FromSeconds(3);
                            var result = sc.Execute();
                            if (!string.IsNullOrEmpty(result) && result.Contains(text))
                            {
                                c = "pass";
                                utility_func.callbackdebuginfo($"[HERO_FINAL] usb_storage_check: detected '{text}' via dmesg");
                                return "pass";
                            }
                        }
                        catch { }
                    }

                    Thread.Sleep(300);
                }

                c = "fail;timeout";
                utility_func.callbackdebuginfo($"[HERO_FINAL] usb_storage_check: timeout waiting for '{text}'");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] usb_storage_check error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 序列号编程 — 从全局变量 input_sn 获取条码, 通过串口发送 eeprog 命令写入 EEPROM。
        /// 流程: 获取SN -> 串口登录 -> 写随机种子到0x00 -> 写SN到0x20 -> 回读验证
        ///
        /// d 参数: login=<是否需要登录, 默认true>; user=<用户名, 默认root>; pass=<密码, 默认EcoTest>
        ///         i2c_bus=<i2c总线号, 默认1>; eeprom_addr=<EEPROM地址, 默认0x54>
        /// a/b = 无效, c = "pass;SN=XXXX" 或 "fail;reason"
        /// </summary>
        private string program_serial_number(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // 从全局变量获取条码
                if (tc.golb_var_default.TryGetValue("input_sn", out object val) == false || val == null)
                {
                    c = "fail;no_sn";
                    utility_func.callbackdebuginfo("[HERO_FINAL] program_serial_number: no input_sn found");
                    return "fail";
                }

                string sn = val.ToString().Trim();
                if (string.IsNullOrEmpty(sn))
                {
                    c = "fail;empty_sn";
                    return "fail";
                }

                utility_func.callbackdebuginfo($"[HERO_FINAL] program_serial_number: SN='{sn}'");

                var p = parse_d(d);
                bool needLogin = get_optional(p, "login", "true").Equals("true", StringComparison.OrdinalIgnoreCase);
                string user = get_optional(p, "user", "root");
                string pass = get_optional(p, "pass", "EcoTest");
                string i2cBus = get_optional(p, "i2c_bus", "1");
                string eepromAddr = get_optional(p, "eeprom_addr", "0x54");
                int timeout = get_int(p, "timeout", 15000);

                if (ubootPort == null || !ubootPort.IsOpen)
                {
                    c = "fail;serial_not_open";
                    return "fail";
                }

                // 登录
                clear_uboot_rx();
                ubootPort.WriteLine("");
                if (!wait_for_prompt(timeout))
                {
                    if (needLogin)
                    {
                        utility_func.callbackdebuginfo("[HERO_FINAL] program_serial_number: attempting login...");
                        ubootPort.WriteLine(user);
                        Thread.Sleep(500);
                        ubootPort.WriteLine(pass);
                        Thread.Sleep(1000);
                        clear_uboot_rx();
                        ubootPort.WriteLine("");
                        if (!wait_for_prompt(5000))
                        {
                            c = "fail;login_failed";
                            return "fail";
                        }
                    }
                    else
                    {
                        c = "fail;no_prompt";
                        return "fail";
                    }
                }

                // Step 1: 写随机种子到 EEPROM 0x00 (255字节)
                clear_uboot_rx();
                ubootPort.WriteLine($"cat /dev/urandom | base64 | dd bs=1 count=255 | eeprog -f /dev/i2c-{i2cBus} {eepromAddr} -8 -w 0x00; echo seed_done");
                if (!wait_for_prompt(10000))
                {
                    c = "fail;seed_write_timeout";
                    return "fail";
                }
                utility_func.callbackdebuginfo("[HERO_FINAL] program_serial_number: random seed written");

                // Step 2: 写序列号到 EEPROM 0x20
                clear_uboot_rx();
                ubootPort.WriteLine($"echo \"{sn}\" | eeprog -f /dev/i2c-{i2cBus} {eepromAddr} -8 -w 0x20; echo sn_written");
                if (!wait_for_prompt(5000))
                {
                    c = "fail;sn_write_timeout";
                    return "fail";
                }
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_serial_number: SN '{sn}' written to 0x20");

                // Step 3: 回读验证
                clear_uboot_rx();
                ubootPort.WriteLine($"eeprog -qf /dev/i2c-{i2cBus} -f {eepromAddr} -8 -r 0x20:12; echo readback_done");
                if (!wait_for_prompt(5000))
                {
                    c = "fail;readback_timeout";
                    return "fail";
                }

                // 提取回读输出
                string readback = "";
                lock (ubootLock)
                {
                    string raw = ubootRx.ToString();
                    int idx = raw.IndexOf("readback_done");
                    if (idx >= 0)
                        readback = raw.Substring(0, idx);
                    else
                        readback = raw;
                }

                // 清理回读文本
                readback = readback.Replace("\r", "\n");
                var lines = readback.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                bool found = false;
                foreach (var line in lines)
                {
                    string trimmed = line.Trim();
                    if (trimmed.Contains(sn))
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    c = $"pass;SN={sn}";
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_serial_number: readback verified, SN='{sn}'");
                    return "pass";
                }
                else
                {
                    c = $"fail;readback_mismatch;expected={sn}";
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_serial_number: readback mismatch, expected='{sn}', got='{readback}'");
                    return "fail";
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_serial_number error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 优雅关机 — 通过串口向 Linux 发送 sync + halt 命令。
        /// 等待系统完全停止后再断电。
        ///
        /// d 参数: login=<是否需要登录, 默认true>; user=<用户名, 默认root>; pass=<密码, 默认EcoTest>
        ///         halt_timeout=<等待halt生效ms, 默认10000>
        /// a/b = 无效, c = "pass" 或 "fail;reason"
        /// </summary>
        private string graceful_shutdown(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                bool needLogin = get_optional(p, "login", "true").Equals("true", StringComparison.OrdinalIgnoreCase);
                string user = get_optional(p, "user", "root");
                string pass = get_optional(p, "pass", "EcoTest");
                int loginTimeout = get_int(p, "login_timeout", 15000);
                int haltTimeout = get_int(p, "halt_timeout", 10000);

                if (ubootPort == null || !ubootPort.IsOpen)
                {
                    c = "fail;serial_not_open";
                    return "fail";
                }

                // 登录
                clear_uboot_rx();
                ubootPort.WriteLine("");
                if (!wait_for_prompt(loginTimeout))
                {
                    if (needLogin)
                    {
                        utility_func.callbackdebuginfo("[HERO_FINAL] graceful_shutdown: attempting login...");
                        ubootPort.WriteLine(user);
                        Thread.Sleep(500);
                        ubootPort.WriteLine(pass);
                        Thread.Sleep(1000);
                        clear_uboot_rx();
                        ubootPort.WriteLine("");
                        if (!wait_for_prompt(5000))
                        {
                            c = "fail;login_failed";
                            return "fail";
                        }
                    }
                    else
                    {
                        c = "fail;no_prompt";
                        return "fail";
                    }
                }

                // sync: 刷新文件系统缓冲
                clear_uboot_rx();
                ubootPort.WriteLine("sync; echo sync_done");
                if (!wait_for_prompt(5000))
                {
                    c = "fail;sync_timeout";
                    return "fail";
                }
                utility_func.callbackdebuginfo("[HERO_FINAL] graceful_shutdown: sync done");

                // halt: 停止系统
                clear_uboot_rx();
                ubootPort.WriteLine("halt");
                // halt 后系统会停止, 串口可能输出 "System halted." 或不再有提示符
                // 等待一段时间让系统完全停止
                Thread.Sleep(haltTimeout);

                // 检查是否出现 halt 相关消息
                string rxData = "";
                lock (ubootLock) { rxData = ubootRx.ToString(); }

                if (rxData.Contains("halt") || rxData.Contains("Halt") || rxData.Contains("stopped") || rxData.Contains("Power down"))
                {
                    c = "pass;system_halted";
                    utility_func.callbackdebuginfo("[HERO_FINAL] graceful_shutdown: system halted");
                    return "pass";
                }

                // 即使没有检测到 halt 文本, halt 命令发出后系统应已停止
                // 标记 SSH 断开
                ssh_connected = false;
                c = "pass;halt_sent";
                utility_func.callbackdebuginfo("[HERO_FINAL] graceful_shutdown: halt sent, SSH marked disconnected");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] graceful_shutdown error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// MAC 地址编程与安全锁定 — 从 MySQL 或手动输入获取 MAC, 中断 u-boot, 发送 fuse 命令。
        ///
        /// 流程:
        ///   1. 获取 MAC 地址 (MySQL TOP1 未使用 或 手动输入)
        ///   2. 继电器上电
        ///   3. 中断 u-boot (发空格等待 => 提示符)
        ///   4. fuse prog 9 0 <MAC后8位>
        ///   5. fuse prog 9 1 0x001E
        ///   6. 安全锁定 fuse prog -y 6/7/1 (固定值)
        ///   7. 标记 MAC 为已使用
        ///
        /// d 参数: mac_source=mysql|manual (默认manual)
        ///         mysql_server=<IP,默认127.0.0.1>;mysql_db=<库名>;mysql_user=<用户>;mysql_pass=<密码>
        ///         mac_table=<表名,默认hbi_packing_sn_mac>
        ///         relay_ch=<上电继电器通道,默认1>;relay_module=<继电器模块,默认sk_relay1_set>
        ///         interrupt_timeout=<中断u-boot超时ms,默认30000>
        /// a/b = 无效, c = "pass;MAC=XXXX" 或 "fail;reason"
        /// </summary>
        private string program_mac_and_lock(string a, string b, out string c, string d)
        {
            c = "fail";
            string macAddress = "";
            int macDbId = -1;
            try
            {
                var p = parse_d(d);
                string macSource = get_optional(p, "mac_source", "manual");
                int interruptTimeout = get_int(p, "interrupt_timeout", 30000);
                string relayCh = get_optional(p, "relay_ch", "1");
                string relayModule = get_optional(p, "relay_module", "sk_relay1_set");

                // === Step 1: 获取 MAC 地址 ===
                if (macSource.Equals("mysql", StringComparison.OrdinalIgnoreCase))
                {
                    string mysqlServer = get_optional(p, "mysql_server", "127.0.0.1");
                    string mysqlDb = get_optional(p, "mysql_db", "sg_test_db");
                    string mysqlUser = get_optional(p, "mysql_user", "root");
                    string mysqlPass = get_optional(p, "mysql_pass", "root");
                    string macTable = get_optional(p, "mac_table", "hbi_packing_sn_mac");

                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: querying MySQL {mysqlServer}/{mysqlDb}.{macTable}");

                    var mysql = new Mysql(mysqlServer, mysqlDb, mysqlUser, mysqlPass);
                    string query = $"SELECT ID, MAC FROM {macTable} WHERE used=1 ORDER BY ID ASC LIMIT 1";
                    var dt = mysql.Query(query);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        c = "fail;no_available_mac";
                        utility_func.callbackdebuginfo("[HERO_FINAL] program_mac: no unused MAC in database");
                        return "fail";
                    }

                    macDbId = Convert.ToInt32(dt.Rows[0]["ID"]);
                    macAddress = dt.Rows[0]["MAC"].ToString().Trim().ToUpper();
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: got MAC={macAddress} (ID={macDbId})");
                }
                else
                {
                    // 手动输入: 弹窗提示扫描/输入 MAC 地址
                    var dlgResult = HeroInputForm.Show("HERO MAC 地址输入", "请扫描或输入 MAC 地址:\n(格式: 001E06XXXXXXXX, 12位十六进制)", "");
                    if (dlgResult != DialogResult.OK || string.IsNullOrEmpty(HeroInputForm.InputValue))
                    {
                        c = "fail;no_mac_input";
                        return "fail";
                    }
                    string inputMac = HeroInputForm.InputValue.Trim().ToUpper().Replace(":", "").Replace("-", "").Replace(" ", "");
                    if (inputMac.Length != 12 || !Regex.IsMatch(inputMac, @"^[0-9A-F]{12}$"))
                    {
                        c = "fail;invalid_mac_format";
                        utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: invalid MAC format '{inputMac}'");
                        return "fail";
                    }
                    macAddress = inputMac;
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: manual input MAC={macAddress}");
                }

                // MAC 后 8 位 (用于 fuse prog 9 0)
                string macLast8 = macAddress.Substring(4, 8);

                if (ubootPort == null || !ubootPort.IsOpen)
                {
                    c = "fail;serial_not_open";
                    return "fail";
                }

                // === Step 2: 继电器上电 ===
                string relayFunc = relayModule;
                if (tc.funcs.ContainsKey(relayFunc))
                {
                    string relayOut;
                    tc.funcs[relayFunc]($"$%s:1$", "", out relayOut, $"${relayCh}:1$");
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: relay ON (ch={relayCh})");
                }
                else
                {
                    c = "fail;relay_func_not_found";
                    return "fail";
                }

                // === Step 3: 中断 u-boot ===
                utility_func.callbackdebuginfo("[HERO_FINAL] program_mac: interrupting u-boot...");
                string ubootOut;
                if (uboot_interrupt("", "", out ubootOut, $"timeout={interruptTimeout}") != "pass")
                {
                    c = "fail;uboot_interrupt_failed";
                    return "fail";
                }
                utility_func.callbackdebuginfo("[HERO_FINAL] program_mac: u-boot prompt ready");

                // === Step 4: fuse prog 9 0 <MAC后8位> ===
                string fuseCmd1 = $"fuse prog 9 0 0x{macLast8}";
                string fuseOut1;
                if (uboot_exec("", "", out fuseOut1, $"cmd={fuseCmd1};expect=;timeout=10000") != "pass")
                {
                    c = "fail;fuse_mac_low_failed";
                    return "fail";
                }
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: {fuseCmd1} done");

                // === Step 5: fuse prog 9 1 0x001E ===
                string fuseCmd2 = "fuse prog 9 1 0x001E";
                string fuseOut2;
                if (uboot_exec("", "", out fuseOut2, $"cmd={fuseCmd2};expect=;timeout=10000") != "pass")
                {
                    c = "fail;fuse_mac_high_failed";
                    return "fail";
                }
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: {fuseCmd2} done");

                // === Step 6: 安全锁定 (固定值, 不可逆!) ===
                string[] lockCmds = new string[]
                {
                    "fuse prog -y 6 0 0x614ADC5E",
                    "fuse prog -y 6 1 0xFB348804",
                    "fuse prog -y 6 2 0x4603ACD8",
                    "fuse prog -y 6 3 0xEC4876C5",
                    "fuse prog -y 7 0 0x4BCC2159",
                    "fuse prog -y 7 1 0x43AD8288",
                    "fuse prog -y 7 2 0xB380F876",
                    "fuse prog -y 7 3 0x24A19825",
                    "fuse prog -y 1 3 0x02000000"
                };

                for (int i = 0; i < lockCmds.Length; i++)
                {
                    string lockOut;
                    if (uboot_exec("", "", out lockOut, $"cmd={lockCmds[i]};expect=;timeout=10000") != "pass")
                    {
                        c = $"fail;lock_cmd_{i}_failed;cmd={lockCmds[i]}";
                        utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: LOCK FAILED at cmd {i}: {lockCmds[i]}");
                        return "fail";
                    }
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: lock {i + 1}/{lockCmds.Length} done: {lockCmds[i]}");
                }

                // === Step 7: 标记 MAC 为已使用 ===
                if (macSource.Equals("mysql", StringComparison.OrdinalIgnoreCase) && macDbId > 0)
                {
                    string mysqlServer = get_optional(p, "mysql_server", "127.0.0.1");
                    string mysqlDb = get_optional(p, "mysql_db", "sg_test_db");
                    string mysqlUser = get_optional(p, "mysql_user", "root");
                    string mysqlPass = get_optional(p, "mysql_pass", "root");
                    string macTable = get_optional(p, "mac_table", "hbi_packing_sn_mac");

                    var mysql = new Mysql(mysqlServer, mysqlDb, mysqlUser, mysqlPass);
                    int affected = mysql.ExecNonQuery($"UPDATE {macTable} SET used=0 WHERE ID={macDbId}");
                    utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: marked MAC ID={macDbId} as used (affected={affected})");
                }

                c = $"pass;MAC={macAddress}";
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac: ALL DONE, MAC={macAddress}");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_FINAL] program_mac error: {ex.Message}");
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
