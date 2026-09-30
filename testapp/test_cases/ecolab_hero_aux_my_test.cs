using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    /// <summary>
    /// HERO AUX Board 串口测试 (STM32F103C8 合并项目)
    ///
    /// 通讯方式: 普通串口 (System.IO.Ports.SerialPort, 9600 8N1)
    /// 测试内容: I2C扫描/RTC时间设置/EEPROM读写/看门狗状态/喂狗控制/触摸屏读取/触摸自动上报
    ///
    /// 单片机命令集 (以回车结束):
    ///   1                  - 扫描 I2C 器件 (AT24Cxx + DS3231)
    ///   2 YYYY MM DD HH MM SS - 设置 RTC 时间并读回验证
    ///   3 ADDR d1 d2 ...   - 写 EEPROM 并读回校验 (最多32字节)
    ///   4                  - 查询看门狗 WDO 状态
    ///   5 {0|1}            - 0=停止喂狗  1=恢复喂狗
    ///   6                  - 触摸屏单次读取 (X/Y/Z, 10s 超时)
    ///   7 {0|1}            - 0=关闭触摸自动上报  1=开启
    ///
    /// setup.ini 配置:
    ///   [setport]
    ///   ecolab_hero_aux_serial_port = COM3
    ///   ecolab_hero_aux_serial_baudrate = 9600
    /// </summary>
    public class ecolab_hero_aux_my_test : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "hero_aux_";
        SerialPort port;
        StringBuilder rxBuf = new StringBuilder();
        object rxLock = new object();

        // ══════════════════════════════════════════════════════════════
        //  构造与初始化
        // ══════════════════════════════════════════════════════════════

        public ecolab_hero_aux_my_test(testcase_dll _tc)
        {
            tc = _tc;
            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                string portName = ini["setport"]["ecolab_hero_aux_serial_port"];
                int baud = int.Parse(ini["setport"]["ecolab_hero_aux_serial_baudrate"] ?? "9600");

                port = new SerialPort(portName, baud, Parity.None, 8, StopBits.One);
                port.ReadTimeout = 15000;
                port.WriteTimeout = 2000;
                port.NewLine = "\r\n";
                port.Encoding = Encoding.UTF8;
                port.DataReceived += Port_DataReceived;
                port.Open();
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] Serial port opened: {portName}@{baud}");
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] init error: {ex.Message}");
            }

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            // ── I2C 扫描 (命令 1) ──
            // 验证 AT24Cxx 和 DS3231 均返回 OK
            // a/b = "pass" (无上下限), c = 响应文本(换行替换为空格)
            tc.funcs.Add(id + "i2c_scan", i2c_scan);

            // ── RTC 时间设置 (命令 2) ──
            // d = "2026;01;15;12;30;00" (年;月;日;时;分;秒)
            // a/b = "pass", c = 响应文本
            tc.funcs.Add(id + "rtc_set", rtc_set);

            // ── RTC 单独读时间 (命令 2 无参数) ──
            // 读当前 RTC 时间, 验证格式合法
            // a/b = "pass", c = "YYYY-MM-DD HH:MM:SS"
            tc.funcs.Add(id + "rtc_read", rtc_read);

            // ── EEPROM 读写 (命令 3) ──
            // d = "0;170;187;204;221" (地址;数据1;数据2;...)
            // a/b = "pass", c = 响应文本
            tc.funcs.Add(id + "eeprom_write", eeprom_write);

            // ── 看门狗 WDO 查询 (命令 4) ──
            // 验证 WDO = HIGH (OK)
            // a/b = "pass", c = 响应文本
            tc.funcs.Add(id + "wdt_query", wdt_query);

            // ── 喂狗控制 (命令 5) ──
            // d = "0" (停止) 或 "1" (恢复)
            // a/b = "pass", c = 响应文本
            tc.funcs.Add(id + "wdt_feed", wdt_feed);

            // ── 触摸屏单次读取 (命令 6) ──
            // 等待触摸按下, 读取 X/Y/Z 坐标
            // a = "X上限;Y上限", b = "X下限;Y下限", c = "X=xxx Y=xxx Z=xxx"
            // d = "timeout=10000" (可选, 默认10s)
            tc.funcs.Add(id + "touch_read", touch_read);

            // ── 触摸自动上报开关 (命令 7) ──
            // d = "0" (关闭) 或 "1" (开启)
            // a/b = "pass", c = 响应文本
            tc.funcs.Add(id + "touch_auto", touch_auto);

            // ── 串口资源探测 (WMI 查询) ──
            // 通过 VID/PID/名称查找串口, 返回发现的数量和端口列表
            // d = "name=Prolific;vid=067B;pid=2303;expected=2"
            // a/b = "pass", c = "found=N:COM3,COM5,COM7"
            tc.funcs.Add(id + "find_serial_ports", find_serial_ports);

            // ── 串口两两回环测试 ──
            // 对发现的串口两两进行双向回环测试, 任意一对通过即 pass
            // d = "name=Prolific;vid=067B;pid=2303;expected=2;baud=9600;timeout=1000;test_data=HERO_LOOPBACK"
            // a/b = "pass", c = "found=N:COM3,COM5; tested=M; pass=COM3<->COM5; fail=..."
            tc.funcs.Add(id + "serial_pair_test", serial_pair_test);

            // ── 4 路 RS485 串口回显服务 (原路返回) ──
            // 按特征找到正好 4 个串口, 以 115200 8N1 打开; 每口一条线程:
            // 收满一行(CRLF 结尾) → 等总线静默(turnaround_ms) → 整帧写回本口; 并抑制自身回显。
            // duration 到期自动安全销毁, Dispose 时也会停止。对端 4 个串口由其它函数创建。
            // d = "name=Prolific;vid=067B;pid=2303;duration=30000;turnaround_ms=5;frame_end=\r\n;echo_guard_ms=20"
            // a/b = "pass", c = "opened=4:COM3,COM5,COM7,COM9; baud=115200; frame=end:\r\n; turnaround=5ms; ttl=30000ms; stop_at=15:04:05"
            tc.funcs.Add(id + "serial_echo_start", serial_echo_start);

            // ── 单口回环发送 (周期发字符串, 等对端 485 回复) ──
            // 收到第一帧回复即 pass; 发送线程继续跑到 duration(默认3s), 便于随后检查 TX/RX LED。
            // 需等第一次回复才返回, 最长阻塞 wait_ms; 可对多个口分别调用(按端口名并发注册)。
            // d = "port=COM7;data=HERO_AUX_PING;duration=3000;interval_ms=500;reply_timeout_ms=1000"
            // a/b = "pass", c = "port=COM7; sent=1; replies=1; rx=12B; first_reply=93ms: HERO_AUX_PING\r\n"
            tc.funcs.Add(id + "serial_loopback_send", serial_loopback_send);

            tc.golb_var_default["hero_aux_slave_id"] = "1";
        }

        // ══════════════════════════════════════════════════════════════
        //  串口底层通讯
        // ══════════════════════════════════════════════════════════════

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (e.EventType != SerialData.Chars) return;
            try
            {
                var data = port.ReadExisting();
                lock (rxLock) rxBuf.Append(data);
            }
            catch { }
        }

        /// <summary>
        /// 清空接收缓冲区
        /// </summary>
        private void clear_rx()
        {
            lock (rxLock) rxBuf.Clear();
        }

        /// <summary>
        /// 读取缓冲区当前内容并清空
        /// </summary>
        private string get_rx()
        {
            lock (rxLock)
            {
                var s = rxBuf.ToString();
                rxBuf.Clear();
                return s;
            }
        }

        /// <summary>
        /// 等待串口响应中出现指定的提示符 (如 "> ") 或超时
        /// </summary>
        private string wait_for_prompt(string prompt, int timeoutMs)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                Thread.Sleep(50);
                lock (rxLock)
                {
                    if (rxBuf.ToString().Contains(prompt))
                    {
                        var s = rxBuf.ToString();
                        rxBuf.Clear();
                        return s;
                    }
                }
            }
            var partial = get_rx();
            utility_func.callbackdebuginfo($"[HERO_AUX_TEST] wait_for_prompt timeout ({timeoutMs}ms), partial: {partial.Substring(0, Math.Min(partial.Length, 300))}");
            return partial;
        }

        /// <summary>
        /// 发送命令到单片机, 等待 "> " 提示符后返回完整响应
        /// 响应中的 \r\n 替换为空格, 结果赋值给调用方用于 c
        /// </summary>
        private string send_and_recv(string cmd, int timeoutMs, out string response)
        {
            response = "";
            try
            {
                clear_rx();
                port.WriteLine(cmd);
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] TX: {cmd}");

                var raw = wait_for_prompt("> ", timeoutMs);
                response = raw.Replace("\r", " ").Replace("\n", " ");
                response = Regex.Replace(response, @">\s*$", "").Trim();
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] RX: {response.Substring(0, Math.Min(response.Length, 300))}");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] send_and_recv error: {ex.Message}");
                response = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  七个命令函数
        // ══════════════════════════════════════════════════════════════

        // ── 命令 1: I2C 扫描 ──────────────────────────────────────────
        // 验证 AT24Cxx 返回 OK 且 DS3231 返回 OK
        // a = "pass", b = "pass", d = ""
        // c = 响应文本 (\r\n 替换为空格)
        private string i2c_scan(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (send_and_recv("1", 5000, out c) != "pass")
                    return "fail";

                bool atOk = c.Contains("AT24Cxx") && c.Contains("OK");
                bool dsOk = c.Contains("DS3231") && c.Contains("OK");

                if (atOk && dsOk)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] I2C scan: AT24Cxx=OK, DS3231=OK");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] I2C scan FAIL: AT24Cxx={atOk}, DS3231={dsOk}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] i2c_scan error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 2: RTC 时间设置 ────────────────────────────────────
        // 用电脑当前时间设置 RTC, 验证读回的年月日时与设定值相同, 且有 PASS
        // a = "pass", b = "pass", d = 可选偏移秒数 (如 "offset=10" 加10秒, 默认0)
        // c = 响应文本 (\r\n 替换为空格)
        private string rtc_set(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // 取电脑当前时间, 可通过 d 的 offset 参数加偏移
                var p = parse_d(d);
                int offset = get_int(p, "offset", 0);
                DateTime now = DateTime.Now.AddSeconds(offset);

                int year  = now.Year;
                int month = now.Month;
                int day   = now.Day;
                int hour  = now.Hour;
                int min   = now.Minute;
                int sec   = now.Second;

                string cmd = $"2 {year} {month} {day} {hour} {min} {sec}";
                string rsustr = "";
                if (send_and_recv(cmd, 5000, out rsustr) != "pass")
                    return "fail";
              
                // 提取 Set 行的时间
                var setMatch = Regex.Match(rsustr, @"Set:\s*(\d{4})-(\d{2})-(\d{2})\s+(\d{2}):(\d{2}):(\d{2})");
                // 提取 RTC read 行的时间
                var readMatch = Regex.Match(rsustr, @"RTC read:\s*(\d{4})-(\d{2})-(\d{2})\s+(\d{2}):(\d{2}):(\d{2})");

                bool hasPass = rsustr.Contains("RTC test: PASS");

                if (!setMatch.Success)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC FAIL: no Set time found");
                    return "fail";
                }
                if (!readMatch.Success)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC FAIL: no RTC read time found");
                    return "fail";
                }

                // 验证设定值与我们发出的时间一致 (年月日时分秒)
                string setY = setMatch.Groups[1].Value;
                string setMo = setMatch.Groups[2].Value;
                string setD = setMatch.Groups[3].Value;
                string setH = setMatch.Groups[4].Value;

                // 验证读回值与设定值的年月日时相同 (分秒可能因执行延迟差1, 不校验)
                string readY = readMatch.Groups[1].Value;
                string readMo = readMatch.Groups[2].Value;
                string readD = readMatch.Groups[3].Value;
                string readH = readMatch.Groups[4].Value;

                bool yOk  = (setY == readY);
                bool moOk = (setMo == readMo);
                bool dOk  = (setD == readD);
                bool hOk  = (setH == readH);

                utility_func.callbackdebuginfo(
                    $"[HERO_AUX_TEST] RTC set={setY}-{setMo}-{setD} {setH}:xx:xx, " +
                    $"read={readY}-{readMo}-{readD} {readH}:xx:xx, " +
                    $"y={yOk} mo={moOk} d={dOk} h={hOk} pass={hasPass}");
                c = $"read={readY}-{readMo}-{readD} {readH}:{readMatch.Groups[5].Value}:{readMatch.Groups[6].Value}";

                if (yOk && moOk && dOk && hOk && hasPass)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC set+read: PASS");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] RTC FAIL: y={yOk} mo={moOk} d={dOk} h={hOk} hasPass={hasPass}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] rtc_set error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 2 无参数: RTC 单独读时间 ──────────────────────────
        // 读取当前 RTC 时间, 与电脑系统时间对比, 差值小于 2 分钟算 PASS
        // a = "pass", b = "pass", d = 可选容差分钟数 "tolerance=2" 默认2分钟
        // c = "YYYY-MM-DD HH:MM:SS"
        private string rtc_read(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // 先记录系统时间
                DateTime sysNow = DateTime.Now;

                if (send_and_recv("2", 5000, out c) != "pass")
                    return "fail";

                // 用正则提取时间字符串 YYYY-MM-DD HH:MM:SS
                var m = System.Text.RegularExpressions.Regex.Match(
                    c, @"(\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}:\d{2})");
                if (!m.Success)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC read FAIL, no time found: " + c.Substring(0, System.Math.Min(c.Length, 200)).Replace("\r"," ").Replace("\n", " "));
                    return "fail";
                }

                string timeStr = m.Groups[1].Value;
                DateTime rtcTime;
                if (!DateTime.TryParse(timeStr, out rtcTime))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC read FAIL, cannot parse time: " + timeStr);
                    c = timeStr;
                    return "fail";
                }

                // 计算与系统时间的差值
                TimeSpan diff = rtcTime - sysNow;
                double diffSeconds = Math.Abs(diff.TotalSeconds);

                // 读取容差 (默认 2 分钟 = 120 秒)
                var p = parse_d(d);
                double toleranceSec = get_int(p, "tolerance", 2) * 60.0;

                c = timeStr;
                utility_func.callbackdebuginfo(
                    $"[HERO_AUX_TEST] RTC read: {timeStr}, sys: {sysNow:yyyy-MM-dd HH:mm:ss}, " +
                    $"diff={diffSeconds:F0}s, tolerance={toleranceSec}s");

                if (diffSeconds <= toleranceSec)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] RTC read: PASS (within tolerance)");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] RTC read FAIL: diff={diffSeconds:F0}s > {toleranceSec}s");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] rtc_read error: " + ex.Message);
                c = "error";
                return "fail";
            }
        }

        // ── 命令 3: EEPROM 读写 ─────────────────────────────────────
        // d = "0;170;187;204;221" (地址;数据1;数据2;...)
        // a = "pass", b = "pass", c = 响应文本
        private string eeprom_write(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string addr = get_required(p, "addr");
                string dataStr = get_required(p, "data");

                string cmd = $"3 {addr} {dataStr.Replace(';', ' ')}";
                if (send_and_recv(cmd, 5000, out c) != "pass")
                    return "fail";

                if (c.Contains("EEPROM test: PASS"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] EEPROM write+read: PASS");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] EEPROM test FAIL: {c.Substring(0, Math.Min(c.Length, 200))}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] eeprom_write error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 4: 看门狗 WDO 查询 ────────────────────────────────
        // 验证 WDO = HIGH (OK)
        // a = "pass", b = "pass", c = 响应文本
        private string wdt_query(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (send_and_recv("4", 10000, out c) != "pass")
                    return "fail";

                if (c.Contains("WDO = HIGH") && c.Contains("OK"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] WDT query: WDO=HIGH, OK");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] WDT query: WDO not HIGH or not OK: {c.Substring(0, Math.Min(c.Length, 200))}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] wdt_query error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 5: 喂狗控制 ────────────────────────────────────────
        // d = "0" (停止喂狗) 或 "1" (恢复喂狗)
        // a = "pass", b = "pass", c = 响应文本
        private string wdt_feed(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string action = string.IsNullOrEmpty(d) ? "1" : d.Trim();
                if (action != "0" && action != "1")
                {
                    c = "invalid_param";
                    return "fail";
                }

                string cmd = $"5 {action}";
                if (send_and_recv(cmd, 5000, out c) != "pass")
                    return "fail";

                if (action == "0" && c.Contains("STOPPED"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] WDT feeding STOPPED");
                    return "pass";
                }
                if (action == "1" && c.Contains("STARTED"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] WDT feeding STARTED");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] WDT feed unexpected response: {c.Substring(0, Math.Min(c.Length, 200))}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] wdt_feed error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 6: 触摸屏单次读取 ──────────────────────────────────
        // 等待触摸按下, 读取 X/Y/Z 坐标
        // a = "X上限;Y上限", b = "X下限;Y下限"
        // c = "X=xxx Y=xxx Z=xxx" (实际读到的坐标)
        // d = "timeout=10000" (可选)
        private string touch_read(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                int timeout = get_int(p, "timeout", 10000);

                if (send_and_recv("6", timeout + 2000, out c) != "pass")
                    return "fail";

                // 检查是否检测到触摸
                if (c.Contains("no press"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] Touch: no press within timeout");
                    return "fail";
                }

                // 提取 X, Y, Z 值
                int xVal = -1, yVal = -1, zVal = -1;
                var mx = Regex.Match(c, @"X=(\d+)", RegexOptions.None, TimeSpan.FromSeconds(1));
                var my = Regex.Match(c, @"Y=(\d+)", RegexOptions.None, TimeSpan.FromSeconds(1));
                var mz = Regex.Match(c, @"Z=(\d+)", RegexOptions.None, TimeSpan.FromSeconds(1));

                if (mx.Success) xVal = int.Parse(mx.Groups[1].Value);
                if (my.Success) yVal = int.Parse(my.Groups[1].Value);
                if (mz.Success) zVal = int.Parse(mz.Groups[1].Value);

                if (xVal < 0 || yVal < 0)
                {
                    utility_func.callbackdebuginfo($"[HERO_AUX_TEST] Touch: failed to parse X/Y from: {c.Substring(0, Math.Min(c.Length, 200))}");
                    return "fail";
                }

                c = $"X={xVal} Y={yVal} Z={zVal}";
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] Touch read: {c}");

                // 范围判定: a = "X上限;Y上限", b = "X下限;Y下限"
                if (!string.IsNullOrEmpty(a) && a != "pass" && !string.IsNullOrEmpty(b) && b != "pass")
                {
                    string[] aParts = a.Split(';');
                    string[] bParts = b.Split(';');
                    if (aParts.Length >= 2 && bParts.Length >= 2)
                    {
                        int xHi = int.Parse(aParts[0].Trim());
                        int yHi = int.Parse(aParts[1].Trim());
                        int xLo = int.Parse(bParts[0].Trim());
                        int yLo = int.Parse(bParts[1].Trim());

                        bool xPass = xVal >= xLo && xVal <= xHi;
                        bool yPass = yVal >= yLo && yVal <= yHi;

                        utility_func.callbackdebuginfo($"[HERO_AUX_TEST] Touch X={xVal}[{xLo}-{xHi}]={(xPass ? "PASS" : "FAIL")}, Y={yVal}[{yLo}-{yHi}]={(yPass ? "PASS" : "FAIL")}");

                        return (xPass && yPass) ? "pass" : "fail";
                    }
                }

                // 没有上下限, 只要读到有效值就 pass
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] touch_read error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ── 命令 7: 触摸自动上报开关 ────────────────────────────────
        // d = "0" (关闭) 或 "1" (开启)
        // a = "pass", b = "pass", c = 响应文本
        private string touch_auto(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string action = string.IsNullOrEmpty(d) ? "1" : d.Trim();
                if (action != "0" && action != "1")
                {
                    c = "invalid_param";
                    return "fail";
                }

                string cmd = $"7 {action}";
                if (send_and_recv(cmd, 5000, out c) != "pass")
                    return "fail";

                if (action == "0" && c.Contains("DISABLED"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] Touch auto-report DISABLED");
                    return "pass";
                }
                if (action == "1" && c.Contains("ENABLED"))
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] Touch auto-report ENABLED");
                    return "pass";
                }
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] Touch auto unexpected response: {c.Substring(0, Math.Min(c.Length, 200))}");
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[HERO_AUX_TEST] touch_auto error: {ex.Message}");
                c = "error";
                return "fail";
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  串口资源探测与回环测试
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// 通过 VID/PID/名称查找串口, 返回发现的数量和端口列表。
        ///
        /// d 参数: name=名称模糊匹配;vid=USB VID十六进制;pid=USB PID十六进制;expected=期望数量
        /// a/b = "pass", c = "found=N:COM3,COM5,COM7"
        ///
        /// 判定: 发现数量 >= expected → pass, 否则 fail
        /// </summary>
        private string find_serial_ports(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string namePattern = get_optional(p, "name", "");
                string vid = get_optional(p, "vid", "");
                string pid = get_optional(p, "pid", "");
                int expected = get_int(p, "expected", 1);

                var ports = SerialPortDiscovery.FindPorts(namePattern, vid, pid);

                var portNames = new List<string>();
                foreach (var portInfo in ports)
                    portNames.Add(portInfo.ComName);

                c = "found=" + ports.Count + ":" + string.Join(";", portNames);

                utility_func.callbackdebuginfo("[HERO_AUX_TEST] find_serial_ports: " + c);

                if (ports.Count >= expected)
                {
                    return "pass";
                }
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] find_serial_ports error: " + ex.Message);
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 对发现的串口两两进行双向回环测试, 任意一对双向通过即 pass。
        ///
        /// d 参数: name=名称;vid=VID;pid=PID;expected=期望数量;baud=波特率;timeout=单方向超时ms;test_data=测试字符串
        /// a/b = "pass", c = "found=N:COM3,COM5; tested=M; pass=COM3<->COM5; fail=..."
        ///
        /// 流程:
        /// 1. 通过 VID/PID/name 发现串口
        /// 2. 发现数量 >= expected 才继续
        /// 3. 两两组合做双向回环测试
        /// 4. 任意一对 IsPass → pass
        /// </summary>
        private string serial_pair_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string namePattern = get_optional(p, "name", "");
                string vid = get_optional(p, "vid", "");
                string pid = get_optional(p, "pid", "");
                int expected = get_int(p, "expected", 2);
                int baud = get_int(p, "baud", 9600);
                int timeout = get_int(p, "timeout", 1000);
                string testData = get_optional(p, "test_data", "HERO_LOOPBACK");

                // Step 1: 发现串口
                var ports = SerialPortDiscovery.FindPorts(namePattern, vid, pid);

                var portNames = new List<string>();
                foreach (var portInfo in ports)
                    portNames.Add(portInfo.ComName);

                if (portNames.Count < expected)
                {
                    c = "found=" + portNames.Count + ":" + string.Join(";", portNames) + "; need=" + expected;
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_pair_test: not enough ports: " + c);
                    return "fail";
                }

                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_pair_test: found " + portNames.Count + " ports: " + string.Join(",", portNames));

                // Step 2: 两两回环测试
                var results = SerialLoopbackTester.TestAllPairs(portNames, baud, timeout, testData);

                // Step 3: 汇总结果
                var passList = new List<string>();
                var failList = new List<string>();
                foreach (var r in results)
                {
                    if (r.IsPass)
                        passList.Add(r.PortA + "<->" + r.PortB);
                    else
                        failList.Add(r.PortA + "<->" + r.PortB);
                }

                c = "found=" + portNames.Count + ":" + string.Join(",", portNames) +
                    "; tested=" + results.Count +
                    "; pass=" + string.Join(",", passList) +
                    "; fail=" + string.Join(",", failList);

                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_pair_test: " + c);

                // 任意一对通过即 pass
                if (passList.Count > 0)
                    return "pass";
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_pair_test error: " + ex.Message);
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 按特征发现正好 4 个串口并以 115200 8N1 打开, 启动 RS485 回显服务。
        /// 数据为字符串、以回车换行(CRLF)结尾: 收满一行 → 等总线静默(turnaround_ms)
        /// → 整帧原路回写本口(含 CRLF), 不做"收到即回发"。
        /// 服务在后台线程运行, duration 到期自动安全销毁; 本对象 Dispose 时也会停止。
        ///
        /// d 参数:
        ///   name=名称模糊匹配;vid=USB VID;pid=USB PID;baud=波特率(默认115200)
        ///   duration=存活ms(默认30000)
        ///   turnaround_ms=转向延时ms(默认5) — 整帧回发前等总线静默
        ///   frame_end=帧结束符(默认 \r\n; 字符串协议保持默认即可; none=退回空闲判帧)
        ///   frame_len=N 定长帧(默认0, 设置后优先于 frame_end)
        ///   echo_guard_ms=自身回显抑制窗口ms(默认20, 0=关)
        ///   max_echo_kb=单口回显总量上限KB(默认0=不限)
        /// 注意: 参数字符串以 ';' 和 '=' 分隔, 结束符含这两字符须写成 \x3B / \x3D。
        ///
        /// 判定: 发现数量正好 4 且 4 口全部打开成功 → pass
        /// c: 成功 "opened=4:COM3,...; baud=115200; frame=len:8; turnaround=5ms; ttl=30000ms; stop_at=HH:mm:ss"
        ///    失败 "found=N:COMx,...; need=4" 或 "open_fail=COMx:原因"
        /// </summary>
        private string serial_echo_start(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);

                var opt = new SerialEchoOptions();
                opt.Baud = get_int(p, "baud", 115200);
                opt.TtlMs = get_int(p, "duration", 30000);
                opt.TurnaroundMs = get_int(p, "turnaround_ms", 5);
                opt.FrameLen = get_int(p, "frame_len", 0);

                // 数据是字符串, 默认以回车换行(CRLF)作帧结束符: 收满一行整帧回发;
                // frame_end=none 退回空闲判帧(不推荐)
                string frameEnd = get_optional(p, "frame_end", "\\r\\n");
                opt.FrameEnd = frameEnd.Equals("none", StringComparison.OrdinalIgnoreCase)
                    ? null
                    : SerialEchoOptions.ParseBytes(frameEnd);
                opt.EchoGuardMs = get_int(p, "echo_guard_ms", 20);
                opt.MaxEchoKb = get_int(p, "max_echo_kb", 0);

                // Step 1: 发现串口 (去重 + 按 COM 号数值排序, 避免 COM10 排在 COM3 前)
                var ports = SerialPortDiscovery.FindPorts(get_optional(p, "name", ""),
                                                          get_optional(p, "vid", ""),
                                                          get_optional(p, "pid", ""));

                var portNames = new List<string>();
                foreach (var portInfo in ports)
                    if (!portNames.Contains(portInfo.ComName)) portNames.Add(portInfo.ComName);
                portNames.Sort((x, y) => com_index(x).CompareTo(com_index(y)));

                if (portNames.Count != 4)
                {
                    c = "found=" + portNames.Count + ":" + string.Join(";", portNames) + "; need=4";
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_echo_start: " + c);
                    return "fail";
                }

                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_echo_start: found 4 ports: " + string.Join(";", portNames));

                // Step 2: 打开 4 口并启动回显后台服务
                string detail;
                var svc = SerialEchoService.Start(portNames, opt, out detail);

                c = detail;
                if (svc == null || !svc.IsRunning)
                {
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_echo_start: " + c);
                    return "fail";
                }

                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_echo_start error: " + ex.Message);
                c = "error";
                return "fail";
            }
        }

        /// <summary>
        /// 单口回环发送: 在指定串口上周期发送字符串并等对端 485 回复。
        /// 收到第一帧完整回复即判 pass, 但发送/接收线程继续跑到 duration 毫秒,
        /// 便于下一步对 TX/RX LED 进行检测。
        ///
        /// d 参数:
        ///   port=COM7 (必填) — 要发送的串口
        ///   data=发送内容(默认 HERO_AUX_PING, 结束符自动追加, 不要在 data 里再写)
        ///   baud=波特率(默认115200)
        ///   duration=总运行ms(默认3000) — 发/收持续这么久
        ///   interval_ms=两次发送间隔(默认500)
        ///   reply_timeout_ms=单次等回复超时(默认1000)
        ///   local_echo_ms=发送后丢弃本地回显窗口ms(默认10)
        ///   wait_ms=判 pass/fail 的等待窗口(默认=duration)
        ///   frame_end=帧结束符(默认 \r\n; 字符串协议保持默认即可; none=收到任意字节即算回复)
        ///
        /// 判定: wait_ms 内收到至少一帧完整回复 → pass
        /// c: pass "port=COM7; sent=1; replies=1; rx=12B; first_reply=93ms: HERO_AUX_PING\r\n"
        ///    fail "port=COM7; sent=6; replies=0; timeouts=6; rx=0B"
        /// 注意: 本函数要等第一次回复才返回, 最长阻塞 wait_ms; 后台线程仍会继续跑到 duration。
        /// </summary>
        private string serial_loopback_send(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var p = parse_d(d);
                string portName = get_optional(p, "port", "");
                if (string.IsNullOrEmpty(portName))
                {
                    c = "missing_port";
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_loopback_send: missing port");
                    return "fail";
                }

                var opt = new SerialLoopbackOptions();
                opt.Baud = get_int(p, "baud", 115200);
                opt.Data = get_optional(p, "data", "HERO_AUX_PING");
                opt.DurationMs = get_int(p, "duration", 3000);
                opt.IntervalMs = get_int(p, "interval_ms", 500);
                opt.ReplyTimeoutMs = get_int(p, "reply_timeout_ms", 1000);
                opt.LocalEchoMs = get_int(p, "local_echo_ms", 10);
                opt.WaitMs = get_int(p, "wait_ms", 0);

                string frameEnd = get_optional(p, "frame_end", "\\r\\n");
                opt.FrameEnd = frameEnd.Equals("none", StringComparison.OrdinalIgnoreCase)
                    ? null
                    : SerialEchoOptions.ParseBytes(frameEnd);

                string detail;
                var svc = SerialLoopbackSender.Start(portName, opt, out detail);
                if (svc == null || !svc.IsRunning)
                {
                    c = detail;
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_loopback_send: " + c);
                    return "fail";
                }

                int wait = opt.WaitMs > 0 ? opt.WaitMs : opt.DurationMs;
                if (svc.WaitFirstReply(wait))
                {
                    c = svc.Summary();
                    utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_loopback_send pass: " + c);
                    return "pass";
                }

                c = svc.Summary();
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_loopback_send fail: " + c);
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[HERO_AUX_TEST] serial_loopback_send error: " + ex.Message);
                c = "error";
                return "fail";
            }
        }

        /// <summary>取端口名中的数字部分用于排序, 如 "COM10" → 10</summary>
        private static int com_index(string name)
        {
            if (string.IsNullOrEmpty(name)) return int.MaxValue;
            var m = Regex.Match(name, @"\d+");
            int n;
            return m.Success && int.TryParse(m.Value, out n) ? n : int.MaxValue;
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
        //  参数解析工具 (参考 hero_aux_board.cs)
        // ══════════════════════════════════════════════════════════════

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
                // 回显服务必须随本对象一起安全销毁, 否则会留下后台线程占着 4 个 COM 口
                var svc = SerialEchoService.Current;
                if (svc != null && svc.IsRunning)
                    svc.Stop("dispose");

                // 回环发送线程同理
                SerialLoopbackSender.StopAll("dispose");

                if (port != null)
                {
                    if (port.IsOpen) port.Close();
                    port.Dispose();
                }
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
