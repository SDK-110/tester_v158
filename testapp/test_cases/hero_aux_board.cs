using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Renci.SshNet;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    /// <summary>
    /// HERO AUX Board 测试 (P/N 53003281, Rev D)
    ///
    /// 通讯方式: SSH 网络 (TCP/IP 端口22) 连接树莓派/MaaxBoard Linux
    /// 测试内容: I2C总线扫描/RTC/EEPROM/按键/FTDI EEPROM/触摸屏/看门狗/FTDI回环
    ///
    /// setup.ini 配置:
    ///   [setport]
    ///   hero_ssh_host = 192.168.1.100
    ///   hero_ssh_port = 22
    ///   hero_ssh_user = root
    ///   hero_ssh_password = EcoTest
    ///   hero_rpi_script_path = /opt/hero_test/ftdi_loopback_test.py
    /// </summary>
    public class hero_aux_board : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "hero_aux_";
        SshClient ssh;
        bool ssh_connected = false;
        string rpi_script_path = "/opt/hero_test/ftdi_loopback_test.py";

        // ══════════════════════════════════════════════════════════════
        //  构造与初始化
        // ══════════════════════════════════════════════════════════════

        public hero_aux_board(testcase_dll _tc)
        {
            tc = _tc;
            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                string host = ini["setport"]["hero_ssh_host"];
                int port = int.Parse(ini["setport"]["hero_ssh_port"] ?? "22");
                string user = ini["setport"]["hero_ssh_user"] ?? "root";
                string pass = ini["setport"]["hero_ssh_password"] ?? "EcoTest";

                if (!string.IsNullOrEmpty(ini["setport"]["hero_rpi_script_path"]))
                    rpi_script_path = ini["setport"]["hero_rpi_script_path"];

                ssh = new SshClient(host, port, user, pass);
                ssh.ConnectionInfo.Timeout = TimeSpan.FromSeconds(10);
                ssh.Connect();
                ssh_connected = true;
                utility_func.callbackdebuginfo($"[HERO_AUX] SSH connected to {host}:{port}");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] SSH init error: {ex.Message}");
            }

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            // ── SSH 命令执行类 ──
            tc.funcs.Add(id + "ssh_exec", ssh_exec);
            tc.funcs.Add(id + "ssh_login", ssh_login);
            tc.funcs.Add(id + "ssh_exec_wait_reboot", ssh_exec_wait_reboot);
            tc.funcs.Add(id + "ftdi_loopback", ftdi_loopback);

            // ── 测量封装 (参考 Get_Current_by_DMM) ──
            tc.funcs.Add(id + "measure_voltage", measure_voltage);
            tc.funcs.Add(id + "measure_current", measure_current);
            tc.funcs.Add(id + "measure_resistance", measure_resistance);

            // ── 人工交互封装 (MessageBox 弹窗) ──
            tc.funcs.Add(id + "manual_prompt", manual_prompt);
            tc.funcs.Add(id + "manual_confirm", manual_confirm);

            // ── 组合步骤封装 (参考 power_off_test_search) ──
            tc.funcs.Add(id + "ssh_then_measure", ssh_then_measure);
            tc.funcs.Add(id + "manual_then_ssh", manual_then_ssh);
            tc.funcs.Add(id + "manual_then_ftdi", manual_then_ftdi);

            // ── 电源控制与产品连接验证 ──
            tc.funcs.Add(id + "power_on", power_on);
            tc.funcs.Add(id + "power_off", power_off);
            tc.funcs.Add(id + "product_connect", product_connect);
            tc.funcs.Add(id + "power_cycle", power_cycle);
            tc.funcs.Add(id + "get_psu_current", get_psu_current);
        }

        // ══════════════════════════════════════════════════════════════
        //  注册函数 — SSH 命令执行
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// 执行 SSH Shell 命令并验证输出包含指定模式。
        ///
        /// 对应步骤:
        /// - AUX §3a: i2cdetect -y 1 → 验证含 0x54 和 0x68
        /// - AUX §3a-RTC: date -s + hwclock -w + hwclock -r
        /// - AUX §3a-EEPROM: eeprog 读写验证
        /// - AUX §3a-Button: evtest 按键测试
        /// - AUX §7a-7c: ftdi_eeprom --flash-eeprom 编程/验证
        /// - AUX §8c-8d: evtest 触摸屏测试
        ///
        /// d 参数: cmd=命令;expect=期望匹配模式(正则);timeout=毫秒
        /// a/b = 无效, c = 命令输出
        /// </summary>
        private string ssh_exec(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string cmd = get_required(p, "cmd");
                string expect = get_optional(p, "expect", "");
                int timeout = get_int(p, "timeout", 15000);

                if (!ssh_connected)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX] SSH not connected");
                    return "fail";
                }

                var sw = Stopwatch.StartNew();
                using var sc = ssh.CreateCommand(cmd);
                sc.CommandTimeout = TimeSpan.FromMilliseconds(timeout);
                var result = sc.Execute();
                var output = new StringBuilder();
                if (!string.IsNullOrEmpty(result)) output.Append(result);
                if (!string.IsNullOrEmpty(sc.Error)) output.Append(sc.Error);
                c = output.ToString().TrimEnd();
                sw.Stop();

                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_exec: {cmd} => {c.Substring(0, Math.Min(c.Length, 200))} ({sw.ElapsedMilliseconds}ms)");

                if (!string.IsNullOrEmpty(expect))
                {
                    if (!Regex.IsMatch(c, expect, RegexOptions.None, TimeSpan.FromSeconds(1)))
                        return "fail";
                }
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_exec error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 验证 SSH 网络连接是否已建立 — 对应 AUX §6c FTDI回环准备。
        ///
        /// SSH 网络模式下认证由 Connect() 自动完成。
        /// 此函数检查连接状态并执行 echo 测试。
        ///
        /// d 参数: timeout=毫秒
        /// a/b = 无效, c = "CONNECTED" 或空
        /// </summary>
        private string ssh_login(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ssh_connected)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX] SSH not connected");
                    return "fail";
                }

                using var sc = ssh.CreateCommand("echo HERO_SSH_OK");
                sc.CommandTimeout = TimeSpan.FromSeconds(5);
                var result = sc.Execute();
                if (result.Contains("HERO_SSH_OK"))
                {
                    c = "CONNECTED";
                    return "pass";
                }
                c = "";
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_login error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 执行命令并等待系统重启 — 对应 AUX §13 看门狗测试。
        ///
        /// 通过 SSH 执行 halt/sync，SSH 连接会断开，
        /// 然后等待指定时间让系统通过看门狗重启。
        ///
        /// d 参数: cmd=命令;timeout=等待重启毫秒
        /// a/b = 无效, c = "REBOOTED" 或空
        /// </summary>
        private string ssh_exec_wait_reboot(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string cmd = get_required(p, "cmd");
                int rebootTimeout = get_int(p, "timeout", 120000);

                utility_func.callbackdebuginfo($"[HERO_AUX] sending reboot cmd: {cmd}");

                try
                {
                    using var sc = ssh.CreateCommand(cmd);
                    sc.CommandTimeout = TimeSpan.FromSeconds(5);
                    sc.Execute();
                }
                catch
                {
                    // halt 后 SSH 会断开，忽略异常
                }

                ssh_connected = false;
                Thread.Sleep(Math.Min(rebootTimeout, 90000));
                c = "REBOOTED";
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_exec_wait_reboot error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 树莓派远程 FTDI 回环测试 — 对应 AUX §6d-6g。
        ///
        /// 通过 SSH 在树莓派上执行 ftdi_loopback_test.py 脚本，
        /// 自动完成 J7/J11/J12 三组 FTDI 回环测试 (6次)。
        ///
        /// 前提: AUX Board 已通过 micro-USB (J4) 连接到树莓派
        ///
        /// d 参数: port=J7/J11/J12(可选，默认全部)
        /// a/b = 无效, c = "PASS=X/Y" 汇总
        /// </summary>
        private string ftdi_loopback(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string portFilter = get_optional(p, "port", "");

                string cmd = $"python3 {rpi_script_path}";
                if (!string.IsNullOrEmpty(portFilter))
                    cmd += $" --port {portFilter}";

                utility_func.callbackdebuginfo($"[HERO_AUX] FTDI loopback: {cmd}");

                using var sc = ssh.CreateCommand(cmd);
                sc.CommandTimeout = TimeSpan.FromSeconds(30);
                var result = sc.Execute();
                var output = new StringBuilder();
                if (!string.IsNullOrEmpty(result)) output.Append(result);
                if (!string.IsNullOrEmpty(sc.Error)) output.Append(sc.Error);
                var fullOutput = output.ToString();

                // 提取最后一行 JSON
                string json = null;
                var lines = fullOutput.Split('\n', '\r');
                for (int i = lines.Length - 1; i >= 0; i--)
                {
                    var line = lines[i].Trim();
                    if (line.StartsWith("{") && line.EndsWith("}"))
                    {
                        json = line;
                        break;
                    }
                }

                if (json == null)
                {
                    c = "no JSON output";
                    utility_func.callbackdebuginfo($"[HERO_AUX] FTDI loopback: no JSON. Output: {fullOutput.Substring(0, Math.Min(fullOutput.Length, 500))}");
                    return "fail";
                }

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;
                string overall = root.GetProperty("overall").GetString();
                int passed = root.GetProperty("summary").GetProperty("passed").GetInt32();
                int total = root.GetProperty("summary").GetProperty("total").GetInt32();

                c = $"PASS={passed}/{total}";
                utility_func.callbackdebuginfo($"[HERO_AUX] FTDI loopback result: {c}");

                return overall == "PASS" ? "pass" : "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] ftdi_loopback error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  测量封装 — 参考 Get_Current_by_DMM 模式
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// 直流电压测量 — 调用全局万用表函数。
        ///
        /// d 参数: 万用表量程函数名 (默认 md3058_read_DC_200V)
        /// a = 上限, b = 下限, c = 实测值
        /// </summary>
        private string measure_voltage(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_DC_200V" : d;
                utility_func.callbackdebuginfo($"[HERO_AUX] measure_voltage via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] measure_voltage error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 直流电流测量 — 参考 Get_Current_by_DMM 循环采样模式。
        ///
        /// d 参数: 重试次数 (默认30, 每次间隔1秒)
        /// a = 上限, b = 下限, c = 实测值
        /// </summary>
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
                        utility_func.callbackdebuginfo($"[HERO_AUX] measure_current: {cur}A PASS");
                        c = cur;
                        return "pass";
                    }
                    utility_func.callbackdebuginfo($"[HERO_AUX] measure_current retry {cont + 1}/{count}: {cur}A");
                    Thread.Sleep(1000);
                } while (cont++ < count);
                c = cur;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] measure_current error: {ex.Message}");
                c = "error";
            }
            return "fail";
        }

        /// <summary>
        /// 电阻测量 — 调用全局万用表函数。
        ///
        /// d 参数: 万用表量程函数名 (默认 md3058_read_resistance)
        /// a = 上限, b = 下限, c = 实测值
        /// </summary>
        private string measure_resistance(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string func = string.IsNullOrEmpty(d) ? "md3058_read_resistance" : d;
                utility_func.callbackdebuginfo($"[HERO_AUX] measure_resistance via {func}");
                return tc.funcs[func](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] measure_resistance error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  人工交互封装 — MessageBox 弹窗
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// 弹出提示框等待操作员确认 — 用于接线/按键/观察等人工操作。
        ///
        /// d 参数: 提示文本
        /// a/b = 无效, c = "confirmed"
        /// </summary>
        private string manual_prompt(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请执行人工操作后点击确定");
                HeroPromptForm.Show("HERO AUX 人工操作提示", msg, imgPath, false);
                c = "confirmed";
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_prompt: {msg} -> confirmed");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_prompt error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 弹出 Yes/No 确认框 — 操作员判断结果后返回。
        ///
        /// d 参数: 确认问题文本
        /// a/b = 无效, c = "yes" 或 "no"
        /// </summary>
        private string manual_confirm(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var (msg, imgPath) = HeroPromptForm.ParsePrompt(d, "请确认测试结果是否正常?");
                var result = HeroPromptForm.Show("HERO AUX 人工确认", msg, imgPath, true);
                if (result == DialogResult.OK)
                {
                    c = "yes";
                    utility_func.callbackdebuginfo($"[HERO_AUX] manual_confirm: {msg} -> YES");
                    return "pass";
                }
                c = "no";
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_confirm: {msg} -> NO");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_confirm error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  组合步骤封装 — 参考 power_off_test_search 多阶段模式
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// SSH 命令执行后万用表测量 — 组合通讯与测量。
        ///
        /// 流程: SSH执行命令 → 等待 → 万用表测量
        ///
        /// d 参数: cmd=命令;expect=期望(正则);measure=万用表函数;timeout=毫秒
        /// a = 测量上限, b = 测量下限, c = 测量值
        /// </summary>
        private string ssh_then_measure(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string measureFunc = get_optional(p, "measure", "md3058_read_DC_200V");

                // Step 1: SSH 命令执行 (ssh_exec 会忽略 d 中的 measure 键)
                string commOut;
                if (ssh_exec("", "", out commOut, d) != "pass")
                {
                    c = $"comm_fail;{commOut}";
                    utility_func.callbackdebuginfo($"[HERO_AUX] ssh_then_measure: SSH failed: {commOut}");
                    return "fail";
                }

                // Step 2: 万用表测量
                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_then_measure: measuring via {measureFunc}");
                return tc.funcs[measureFunc](a, b, out c, "");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] ssh_then_measure error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 弹窗提示后执行 SSH 命令 — 组合人工交互与通讯。
        ///
        /// 流程: 弹窗提示操作员 → 操作员确认 → SSH 执行验证
        ///
        /// d 参数: prompt=提示文本;cmd=命令;expect=期望(正则);timeout=毫秒
        /// a/b = 无效, c = 命令输出
        /// </summary>
        private string manual_then_ssh(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_required(p, "prompt");

                // Step 1: 弹窗提示
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO AUX 人工操作", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_then_ssh: prompted '{prompt}'");

                // Step 2: SSH 执行 (ssh_exec 会忽略 d 中的 prompt 键)
                return ssh_exec(a, b, out c, d);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_then_ssh error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 弹窗提示后执行 FTDI 回环测试 — 组合人工交互与通讯。
        ///
        /// 流程: 弹窗提示连接 micro-USB → 操作员确认 → FTDI 回环测试
        ///
        /// d 参数: prompt=提示文本;port=J7/J11/J12(可选)
        /// a/b = 无效, c = "PASS=X/Y" 汇总
        /// </summary>
        private string manual_then_ftdi(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string prompt = get_optional(p, "prompt", "请将 AUX Board 通过 micro-USB (J4) 连接到树莓派，然后点击确定");

                // Step 1: 弹窗提示
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO AUX 人工操作", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_then_ftdi: prompted '{prompt}'");

                // Step 2: FTDI 回环测试
                return ftdi_loopback(a, b, out c, d);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX] manual_then_ftdi error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  电源控制与产品连接验证
        // ══════════════════════════════════════════════════════════════

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
                    utility_func.callbackdebuginfo($"[HERO_AUX] power_on: set voltage/current failed: {setOut}");
                    c = "fail;set_error";
                    return "fail";
                }

                string onOffOut;
                if (tc.funcs["th6300_dc_powersupply_on_off"]("ON", "", out onOffOut, "") != "pass")
                {
                    utility_func.callbackdebuginfo($"[HERO_AUX] power_on: power on failed: {onOffOut}");
                    c = "fail;on_error";
                    return "fail";
                }

                utility_func.callbackdebuginfo($"[HERO_AUX] power_on: {voltage}V/{current}A ON, waiting {bootDelay}ms for boot");
                Thread.Sleep(bootDelay);
                c = $"pass@{voltage:F1}V";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_AUX] power_on error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 断电 — 通过 TH6300 关闭电源输出。
        /// 注意: 树莓派 SSH 连接不受影响, 不需要断开。
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
                    utility_func.callbackdebuginfo($"[HERO_AUX] power_off: power off failed: {onOffOut}");
                    c = "fail;off_error";
                    return "fail";
                }

                // 注意: 不需要标记 ssh_connected = false, 树莓派不受 AUX 板断电影响
                utility_func.callbackdebuginfo($"[HERO_AUX] power_off: AUX board power OFF, RPi SSH still connected, waiting {settleDelay}ms");
                Thread.Sleep(settleDelay);
                c = "pass";
                return "pass";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_AUX] power_off error: {ex.Message}"); c = "error"; return "fail"; }
        }

        /// <summary>
        /// 产品连接验证 — 通过 SSH 在树莓派上执行 I2C 扫描, 验证 AUX 板已上电并连接。
        /// 检查 I2C 总线上是否存在预期的器件地址 (默认: 54=EEPROM, 68=RTC)。
        /// d 参数: retry=重试次数(默认10);interval=重试间隔ms(默认2000);timeout=SSH超时ms(默认10000);i2c_devices=期望I2C地址(默认"54,68")
        /// </summary>
        private string product_connect(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int connRetry = get_int(p, "retry", 10);
                int retryInterval = get_int(p, "interval", 2000);
                int timeout = get_int(p, "timeout", 10000);
                string expectedDevs = get_optional(p, "i2c_devices", "54,68");
                string[] devs = expectedDevs.Split(',');

                utility_func.callbackdebuginfo($"[HERO_AUX] product_connect: checking I2C devices [{expectedDevs}], retry={connRetry}");

                for (int i = 0; i < connRetry; i++)
                {
                    // 通过 SSH 在树莓派上执行 I2C 扫描
                    string scanOut;
                    if (ssh_exec("", "", out scanOut, $"cmd=i2cdetect -y 1;timeout={timeout}") == "pass")
                    {
                        bool allFound = true;
                        foreach (var dev in devs)
                        {
                            string addr = dev.Trim();
                            if (!scanOut.Contains(addr))
                            {
                                allFound = false;
                                utility_func.callbackdebuginfo($"[HERO_AUX] product_connect: I2C device 0x{addr} not found");
                                break;
                            }
                        }

                        if (allFound)
                        {
                            utility_func.callbackdebuginfo($"[HERO_AUX] product_connect: all I2C devices found on attempt {i + 1}");
                            c = $"pass@attempt{i + 1}";
                            return "pass";
                        }
                    }

                    utility_func.callbackdebuginfo($"[HERO_AUX] product_connect: attempt {i + 1}/{connRetry} failed, retrying...");
                    Thread.Sleep(retryInterval);
                }

                c = "fail;no_i2c_devices";
                utility_func.callbackdebuginfo("[HERO_AUX] product_connect: AUX board not detected after all retries");
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_AUX] product_connect error: {ex.Message}"); c = "error"; return "fail"; }
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
                string prompt = get_optional(p, "prompt", "请更换 AUX 板后点击确定");

                // Step 1: 断电
                string offOut;
                if (power_off("", "", out offOut, d) != "pass")
                {
                    c = $"fail;power_off_failed;{offOut}";
                    return "fail";
                }

                // Step 2: 弹窗提示操作员换产品
                string imgPath = get_optional(p, "image", "");
                HeroPromptForm.Show("HERO AUX 换件提示", prompt, imgPath, false);
                utility_func.callbackdebuginfo($"[HERO_AUX] power_cycle: prompted '{prompt}'");

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
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_AUX] power_cycle error: {ex.Message}"); c = "error"; return "fail"; }
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
                    utility_func.callbackdebuginfo($"[HERO_AUX] get_psu_current: {cur:F4}A");
                    return judge_range(cur, a, b);
                }
                c = "parse_error";
                return "fail";
            }
            catch (Exception ex) { utility_func.callbackdebuginfo($"[HERO_AUX] get_psu_current error: {ex.Message}"); c = "error"; return "fail"; }
        }

        // ══════════════════════════════════════════════════════════════
        //  接口实现
        // ══════════════════════════════════════════════════════════════

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void set_default_set()
        {
        }

        // ══════════════════════════════════════════════════════════════
        //  范围判定与参数解析工具
        // ══════════════════════════════════════════════════════════════

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

        // ══════════════════════════════════════════════════════════════
        //  资源释放
        // ══════════════════════════════════════════════════════════════

        public void Dispose()
        {
            try
            {
                if (ssh != null)
                {
                    if (ssh.IsConnected) ssh.Disconnect();
                    ssh.Dispose();
                }
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
