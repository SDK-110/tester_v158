using DeviceLibrary;
using NAudio.Wave;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.glob_set;
using testapp.mylib;
using variable_space;

namespace testapp.test_cases
{
    /// <summary>
    /// J-Link RTT 测试类 — 注册到引擎供 PCBA 测试使用
    /// 生命周期: 每块 PCBA 创建实例 → 注册函数 → 引擎逐项调用 → Dispose
    ///
    /// 函数签名: string xxx(string a, string b, out string c, string d)
    ///   a,b — 通常留空
    ///   c   — 输出结果: "pass" / "fail;原因" / 提取的值
    ///   d   — 参数串，用 , 分隔
    ///   返回 — "pass" / "fail;原因"
    /// </summary>
    public class RttJlinkTester : IDefaultAction, IDisposable
    {
        private testcase_dll tc;
  
        private JLinkRTT rtt;
        private string id = "rtt_";
        // 连接参数 — 供重连使用
        private string _device = "nRF52832_xxAA";
        private int _speedKhz = 4000;
        private int _tif = 1;
        private bool _interactiveMode = false;

        public RttJlinkTester(testcase_dll _tc)
        {
            tc = _tc;
            Initialize();
        }

        public void Initialize()
        {
            add_func_to_libs();
        }

        // ============================================================
        // 函数注册
        // ============================================================

        public void add_func_to_libs()
        {
          
            tc.funcs[id + "jlink_connect"]       = jlink_connect;
            tc.funcs[id + "jlink_close"]         = jlink_close;
            tc.funcs[id + "enter_interactive"]   = enter_interactive;
            tc.funcs[id + "send"]                = rtt_send;
            tc.funcs[id + "read_all"]            = rtt_read_all;
            tc.funcs[id + "query_contains"]      = rtt_query_contains;
            tc.funcs[id + "query_match"]         = rtt_query_match;
            tc.funcs[id + "query_numeric"]       = rtt_query_numeric;
            tc.funcs[id + "query_range"]         = rtt_query_range;
            tc.funcs[id + "query_multi"]         = rtt_query_multi;
            tc.funcs[id + "query_regex"]         = rtt_query_regex;
            tc.funcs[id + "write_sn"]           = rtt_write_sn;
            tc.funcs[id + "read_sn"]            = rtt_read_sn;
            tc.funcs[id + "read_deviceid"]      = rtt_read_deviceid;
            tc.funcs[id + "ble_dongle_rtt_read_batty"] = ble_dongle_rtt_read_batty;
            tc.funcs[id + "read_hwid_bluetooth"] = read_hwid_bluetooth;
            tc.funcs[id + "enter_test_mode_usb_dongle"] = enter_test_mode_usb_dongle;
            tc.funcs[id + "read_gpio_pin"] = rtt_read_gpio_pin;
            tc.funcs[id + "write_gpio_pin"] = rtt_write_gpio_pin;
            tc.funcs[id + "read_gpio_pin_ble_dongle"] = read_gpio_pin_ble_dongle;
            tc.funcs[id + "read_rssi_from_usb_dongle"] = read_rssi_from_usb_dongle;
            tc.funcs[id + "save_mes"]           = rtt_save_mes;
            tc.funcs[id + "play_mp3"]           = rtt_play_mp3;
            tc.funcs[id + "scan_barcode"]       = rtt_scan_barcode;
            tc.funcs[id + "ble_tx_power"]       = cmw_ble_tx_power;
            tc.funcs[id + "ble_tx_offset"]      = cmw_ble_tx_offset;
            tc.funcs[id + "write_deviceid"]      = rtt_write_deviceid;
            tc.funcs[id + "dongle_read_sn"]      = rtt_dongle_read_sn;
            tc.funcs[id + "dongle_connect"]      = rtt_dongle_connect;
        }









        private string  cmw_ble_tx_power(string a, string b, out string c, string d)
        {


           
                    try
                    {

                        cmw100_bluetooth_tx_pycom._cmw100_bluetooth_tx_pycom(freq: double.Parse(d.Split(';')[1]), Eattenuation: double.Parse(d.Split(';')[2]));
                        //if (cmw100_bluetooth_tx_pycom.driver == null) { c = "cmw100 error"; return "fail"; }
                        if (!cmw100_bluetooth_tx_pycom.run_ok) { c = "cmw100 error"; return "fail"; }
                    tc.golb_var_default["ble_tx_offset"] = cmw100_bluetooth_tx_pycom.offset;
                    tc.golb_var_default["ble_tx_power "] = cmw100_bluetooth_tx_pycom.txpower;
                        c = "" + cmw100_bluetooth_tx_pycom.txpower;
                        if (double.Parse(a) >= cmw100_bluetooth_tx_pycom.txpower && double.Parse(b) <= cmw100_bluetooth_tx_pycom.txpower)
                {

                            return "pass";
                        }
                        else
                        {

                            return "fail";
                        }
                    }
                    catch
                    {

                        c = "cmw100 error";
                        return "fail";
                    }

               
                return "fail";
              



            }

        private string cmw_ble_tx_offset(string a, string b, out string c, string d) {


            double ble_tx_offset = double.NaN;
            if (tc.golb_var_default.TryGetValue("ble_tx_offset", out object val) && val != null)
            {
                ble_tx_offset = (double)val;
                c = "" + ble_tx_offset;
                if (double.Parse(a) >= ble_tx_offset && double.Parse(b) <= ble_tx_offset)
                {

                    return "pass";
                }
                else
                {

                    return "fail";
                }
            }
            c= "ble_tx_offset not found";
            return "fail";



        }




        // ============================================================
        // 核心函数实现
        // ============================================================

        /// <summary>连接 J-Link + 初始化 RTT</summary>
        /// d: "device,speed_khz,tif"
        /// 例: "nRF52840_xxAA,4000,1"
        private string jlink_connect(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // 解析参数
                if (!string.IsNullOrEmpty(d))
                {
                    string[] p = d.Trim().Split(',');
                    if (p.Length >= 1) _device = p[0].Trim();
                    if (p.Length >= 2) int.TryParse(p[1].Trim(), out _speedKhz);
                    if (p.Length >= 3) int.TryParse(p[2].Trim(), out _tif);
                }

                // 连接 J-Link
                rtt?.Dispose();
                rtt = new JLinkRTT();
                rtt.Open();
                rtt.SetDevice(_device);
                rtt.SetSpeed(_speedKhz);
                rtt.SetTIF(_tif);
                rtt.Connect();

                if (!rtt.IsConnected)
                {
                    c = "fail;connect_failed";
                    return "fail";
                }
                mylib.utility_func.callbackdebuginfo($"[RTT] jlink_connect: pass");
                _interactiveMode = false;
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] jlink_connect: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>断开 J-Link</summary>
        private string jlink_close(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                rtt?.Close();
                _interactiveMode = false;
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] jlink_close: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>发送 0xdeadbeef\n 使单片机进入交互模式</summary>
        /// d: "超时ms"（可选，默认 500）
        private string enter_interactive(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                int timeoutMs = 500;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                string result = RetryQuery("enter_interactive", () =>
                {
                    // 清缓冲区
                    FlushRtt();
                    if (rtt.WriteString("test enter 0xdeadbeef", false) <= 0)
                        return "fail;write_failed";

                    int waited = 0;
                    while (waited < timeoutMs)
                    {
                        string rev = rtt.ReadString(4096);
                        mylib.utility_func.callbackdebuginfo($"[RTT] enter_interactive: received [{rev}]");
                        if (!string.IsNullOrEmpty(rev))
                        {
                            _interactiveMode = true;
                            localC = "pass";
                            return "pass";
                        }
                        Thread.Sleep(10);
                        waited += 10;
                    }
                    return "fail;timeout";
                });

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] enter_interactive: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>只发不等</summary>
        /// d: "命令内容"
        private string rtt_send(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                if (string.IsNullOrEmpty(d))
                {
                    c = "fail;no_command";
                    return "fail";
                }

                string result = RetryQuery("send", () =>
                {
                    if (rtt.WriteString(d) > 0)
                    {
                        localC = "pass";
                        return "pass";
                    }
                    return "fail;write_failed";
                });

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] rtt_send: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>读取缓冲区全部内容</summary>
        /// d: "超时ms"（可选，默认 1000）
        private string rtt_read_all(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                int timeoutMs = 1000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                var sb = new StringBuilder();
                int elapsed = 0;
                while (elapsed < timeoutMs)
                {
                    string s = rtt.ReadString(4096);
                    if (!string.IsNullOrEmpty(s))
                    {
                        sb.Append(s);
                        break;
                    }
                    Thread.Sleep(5);
                    elapsed += 5;
                }

                string all = sb.ToString().Trim();
                mylib.utility_func.callbackdebuginfo(all);
                c = string.IsNullOrEmpty(all) ? "fail;no_data" : all.Replace("\r", "").Replace("\n", "");
                return string.IsNullOrEmpty(all) ? "fail" : "pass";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] rtt_read_all: {ex.Message}");

                c = "fail;";
                return "fail";
            }
        }

        /// <summary>包含判定 — 回应中含指定关键词即通过</summary>
        /// d: "命令,关键词,超时ms"
        /// 例: "get_ver,VERSION,3000"
        private string rtt_query_contains(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string cmd, keyword;
                int timeoutMs;
                if (!ParseQueryArgs(d, 3, out cmd, out keyword, out timeoutMs))
                {
                    c = "fail;param_error";
                    return "fail";
                }
                FlushRtt();
                string result = RetryQuery("query_contains", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                        resp.Contains(keyword)
                            ? Ok(out localC, "pass")
                            : Fail(out localC, "keyword_not_found", $"未找到 [{keyword}]")));
                mylib.utility_func.callbackdebuginfo($"[RTT] query_contains result: {result}, c: {localC}");
                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_contains: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>精确匹配 — 回应完全等于期望值</summary>
        /// d: "命令,期望值,超时ms"
        /// 例: "ping,pong,2000"
        private string rtt_query_match(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string cmd, expected;
                int timeoutMs;
                if (!ParseQueryArgs(d, 3, out cmd, out expected, out timeoutMs))
                {
                    c = "fail;param_error";
                    return "fail";
                }

                string result = RetryQuery("query_match", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                        resp.Trim() == expected.Trim()
                            ? Ok(out localC, "pass")
                            : Fail(out localC, "mismatch", $"期望 [{expected}] 实际 [{resp.Trim()}]")));
                mylib.utility_func.callbackdebuginfo($"[RTT] query_match result: {result}, c: {localC}");
                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_match: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>数值比较 — 正则提取数值后做比较</summary>
        /// d: "命令,正则式,比较符,期望值,超时ms"
        /// 比较符: >, <, >=, <=, ==, !=
        /// 例: "get_volt,(\\d+\\.?\\d*)V,>=,3.3,5000"
        private string rtt_query_numeric(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string[] p = SplitParam(d, 5);
                if (p == null) { c = "fail;param_error"; return "fail"; }

                string cmd = p[0];
                string pattern = p[1];
                string op = p[2];
                double expected = double.Parse(p[3]);
                int timeoutMs = int.Parse(p[4]);

                string result = RetryQuery("query_numeric", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, pattern);
                        if (!m.Success)
                            return Fail(out localC, "regex_no_match", $"正则 [{pattern}] 未匹配");
                        double val = double.Parse(m.Groups[1].Value);
                        bool ok = CompareNumeric(val, op, expected);
                        localC = ok ? "pass" : $"fail;{val}{op}{expected}不成立";
                        return ok ? "pass" : "fail";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_numeric: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>范围判定 — 正则提取数值后在 [min, max] 区间</summary>
        /// d: "命令,正则式,最小值,最大值,超时ms"
        /// 例: "get_temp,(\\d+\\.?\\d*),25.0,85.0,3000"
        private string rtt_query_range(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string[] p = SplitParam(d, 5);
                if (p == null) { c = "fail;param_error"; return "fail"; }

                string cmd = p[0];
                string pattern = p[1];
                double minVal = double.Parse(p[2]);
                double maxVal = double.Parse(p[3]);
                int timeoutMs = int.Parse(p[4]);

                string result = RetryQuery("query_range", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, pattern);
                        if (!m.Success)
                            return Fail(out localC, "regex_no_match", $"正则 [{pattern}] 未匹配");
                        double val = double.Parse(m.Groups[1].Value);
                        if (val >= minVal && val <= maxVal)
                        {
                            localC = $"pass@{val}";
                            return "pass";
                        }
                        return Fail(out localC, "out_of_range", $"{val} 不在 [{minVal},{maxVal}]");
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_range: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>多关键词 — 回应同时包含所有关键词才通过</summary>
        /// d: "命令,key1&key2&key3,超时ms"
        /// 例: "self_test,PASS&DONE&OK,5000"
        private string rtt_query_multi(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string[] p = SplitParam(d, 3);
                if (p == null) { c = "fail;param_error"; return "fail"; }

                string cmd = p[0];
                string[] keywords = p[1].Split('&');
                int timeoutMs = int.Parse(p[2]);

                string result = RetryQuery("query_multi", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                    {
                        foreach (string kw in keywords)
                        {
                            if (!resp.Contains(kw.Trim()))
                                return Fail(out localC, "keyword_missing", $"缺少 [{kw}]");
                        }
                        localC = "pass";
                        return "pass";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_multi: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>正则提取 — 提取原始值返回，引擎自己判断</summary>
        /// d: "命令,正则式,超时ms"
        /// 例: "get_mac,([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2},5000"
        private string rtt_query_regex(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                string[] p = SplitParam(d, 3);
                if (p == null) { c = "fail;param_error"; return "fail"; }

                string cmd = p[0];
                string pattern = p[1];
                int timeoutMs = int.Parse(p[2]);

                string result = RetryQuery("query_regex", () =>
                    DoQuery(cmd, timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, pattern);
                        if (!m.Success)
                            return Fail(out localC, "regex_no_match", $"正则 [{pattern}] 未匹配");
                        localC = m.Groups[1].Success ? m.Groups[1].Value : m.Value;
                        return "pass";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] query_regex: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 写入 SN 到设备 — SN 从全局字典 golb_var_default["input_sn"] 获取</summary>
        /// d: "超时ms"（可选，默认 3000）
        private string rtt_write_sn(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 从全局字典获取 SN
                string sn = null;
                if (tc.golb_var_default.TryGetValue("input_sn", out object val) && val != null)
                    sn = val.ToString();

                if (string.IsNullOrWhiteSpace(sn))
                {
                    mylib.utility_func.callbackdebuginfo("[RTT] write_sn: 全局字典中未找到 input_sn");
                    c = "fail;no_sn";
                    return "fail";
                }

                // 2) 解析超时
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                // 3) 发送 write serial_number {sn} 并等待回应包含 ok
                string result = RetryQuery("write_sn", () =>
                    DoQuery($"write serial_number {sn}", timeoutMs, resp =>
                    {
                        if (resp.Contains("ok"))
                        {
                            localC = $"{sn}";
                            return "pass";
                        }
                        return Fail(out localC, "write_failed", $"回应不含 ok: {resp.Trim()}");
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] write_sn: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取设备 SN，并与全局字典 golb_var_default["input_sn"] 对比</summary>
        /// d: "超时ms"（可选，默认 3000）
        private string rtt_read_sn(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析超时
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                // 2) 从全局字典获取期望的 SN
                string expectedSn = null;
                if (tc.golb_var_default.TryGetValue("input_sn", out object val) && val != null)
                    expectedSn = val.ToString();

                // 3) 发送 read serial_number 并提取设备 SN
                string result = RetryQuery("read_sn", () =>
                    DoQuery("read serial_number", timeoutMs, resp =>
                    {
                        // 解析 "ok: xxxxxxxxxx" 格式
                        var m = Regex.Match(resp, @"ok:\s*(\w+)");
                        if (!m.Success)
                            return Fail(out localC, "invalid_response", $"回应格式错误: {resp.Trim()}");

                        string deviceSn = m.Groups[1].Value;

                        // 4) 对比
                        if (string.IsNullOrEmpty(expectedSn))
                        {
                            // 字典无 SN，只返回设备值
                            localC = deviceSn;
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_sn: 设备SN={deviceSn} (全局未设置，仅返回设备值)");
                            return "pass";
                        }

                        if (deviceSn == expectedSn)
                        {
                            localC = "pass";
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_sn: SN 匹配 [{deviceSn}]");
                            return "pass";
                        }

                        return Fail(out localC, "sn_mismatch",
                            $"dut=[{deviceSn}] scanner=[{expectedSn}]");
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_sn: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }


        private string rtt_dongle_read_sn(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析超时
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

    
                // 3) 发送 read serial_number 并提取设备 SN
                string result = RetryQuery("read_sn", () =>
                    DoQuery("read serial_number", timeoutMs, resp =>
                    {
                        // 解析 "ok: xxxxxxxxxx" 格式
                        var m = Regex.Match(resp, @"ok:\s*(\w+)");
                        if (!m.Success)
                            return Fail(out localC, "invalid_response", $"回应格式错误: {resp.Trim()}");

                        string deviceSn = m.Groups[1].Value;

                    
                            // 字典无 SN，只返回设备值
                            localC = deviceSn;
                            tc.trf = deviceSn;
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_sn: 设备SN={deviceSn} (全局未设置，仅返回设备值)");
                            return "pass";
                       



                        return Fail(out localC, "ERROR",
                            $"dut=[{deviceSn}] ");
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_sn: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取设备 ID，存入 golb_var_default["deviceid"]</summary>
        /// d: "超时ms"（可选，默认 3000）
        private string rtt_read_deviceid(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                string result = RetryQuery("read_deviceid", () =>
                    DoQuery("read deviceid", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s*(0x[0-9a-fA-F]{7,8})");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_deviceid: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        string deviceId = m.Groups[1].Value;
                        tc.golb_var_default["deviceid"] = deviceId;
                        mylib.utility_func.callbackdebuginfo($"[RTT] read_deviceid: {deviceId} → golb_var_default[deviceid]");
                        localC = deviceId;
                        return "pass";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_deviceid: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }



        private string rtt_dongle_connect(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                {

                    string ble_hwid = "";
                    if (tc.golb_var_default.TryGetValue("input_sn", out object val) == false || val == null)
                    {
                        c = "fail;no_sn";
                        return "fail";
                    }
                    ble_hwid = val.ToString();
                    string result = RetryQuery($"rtt_dongle_connect", () =>
                    DoQuery($"ble connect {ble_hwid.Replace("0x", "").Replace("0X", "")} ", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] rtt_dongle_connect: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        localC = "pass";
                        return "pass";
                    }));

                    c = localC;
                    return result;
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] rtt_dongle_connect: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        private string rtt_write_deviceid(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                {

                    string ble_hwid = "";
                    if (tc.golb_var_default.TryGetValue("blemac", out object val) == false || val == null)
                    {
                        c= "fail;no_hwid_bluetooth";
                        return "fail";
                    }
                    ble_hwid = val.ToString();
                    string result = RetryQuery($"rtt_write_deviceid", () =>
                    DoQuery($"write deviceid {ble_hwid} ", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] write_deviceid: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        localC = "pass";
                        return "pass";
                    }));

                    c = localC;
                    return result;
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_deviceid: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取 BLE 加密狗电池电量，与 a(下限) b(上限) 比较</summary>
        /// a: "电量下限"（百分比整数，必填）
        /// b: "电量上限"（百分比整数，必填）
        /// d: "超时ms"（可选，默认 3000）
        /// 例: b="10" a="90" → 电池在 10%~90% 之间则通过
        private string ble_dongle_rtt_read_batty(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析上下限
                if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                {
                    c = "fail;param_error";
                    return "fail";
                }
                if (!int.TryParse(b.Trim(), out int minLevel) || !int.TryParse(a.Trim(), out int maxLevel))
                {
                    c = "fail;invalid_limit";
                    return "fail";
                }
                if (minLevel < 0 || maxLevel > 100 || minLevel > maxLevel)
                {
                    c = "fail;invalid_range";
                    return "fail";
                }

                // 2) 解析超时
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                // 3) 发送 status battery 并解析回应
                string result = RetryQuery("ble_dongle_rtt_read_batty", () =>
                    DoQuery("status battery", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s*([0-9]{1,3})%");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] ble_dongle_rtt_read_batty: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        int batteryLevel = int.Parse(m.Groups[1].Value);

                        if (batteryLevel >= minLevel && batteryLevel <= maxLevel)
                        {
                            localC = $"pass@{batteryLevel}%";
                            mylib.utility_func.callbackdebuginfo($"[RTT] ble_dongle_rtt_read_batty: {batteryLevel}% 在 [{minLevel},{maxLevel}] 范围内");
                            return "pass";
                        }

                        mylib.utility_func.callbackdebuginfo($"[RTT] ble_dongle_rtt_read_batty: {batteryLevel}% 超出范围 [{minLevel},{maxLevel}]");
                        localC = $"fail;out_of_range:{batteryLevel}%";
                        return "fail";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] ble_dongle_rtt_read_batty: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取蓝牙 HWID（48位硬件地址），存入 golb_var_default["hwid_bluetooth"]</summary>
        /// d: "超时ms"（可选，默认 3000）
        private string read_hwid_bluetooth(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                string result = RetryQuery("read_hwid_bluetooth", () =>
                    DoQuery("hwid bluetooth", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s*(0x[0-9|a-f|A-F]{12})");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_hwid_bluetooth: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        string hwid = m.Groups[1].Value;
                        tc.golb_var_default["blemac"] = hwid;
                        mylib.utility_func.callbackdebuginfo($"[RTT] read_hwid_bluetooth: {hwid} → golb_var_default[hwid_bluetooth]");
                        localC = hwid;
                        return "pass";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_hwid_bluetooth: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>BLE 加密狗进入测试模式：ble connect → get_connect 两步验证</summary>
        /// 前置: golb_var_default["hwid_bluetooth"] 必须有值（由 rtt_read_hwid_bluetooth 写入）
        /// d: "超时ms"（可选，默认 5000）
        private string enter_test_mode_usb_dongle(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 获取蓝牙 MAC（前置步骤必须已执行 rtt_read_hwid_bluetooth）
                string mac = null;
                if (!tc.golb_var_default.TryGetValue("hwid_bluetooth", out object val) || val == null || string.IsNullOrWhiteSpace(val.ToString()))
                {
                    mylib.utility_func.callbackdebuginfo($"[RTT] enter_test_mode_usb_dongle: 全局字典中未找到 hwid_bluetooth");
                    c = "fail;no_hwid";
                    return "fail";
                }
                mac = val.ToString();

                // 2) 解析超时（默认 5000ms）
                int timeoutMs = 5000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                // 3) 两步操作 — RetryQuery 确保首次失败后重连再试一次完整流程
                string result = RetryQuery("enter_test_mode_usb_dongle", () =>
                {
                    // Step 1: BLE 连接目标设备
                    string r1 = DoQuery($"ble connect {mac}", timeoutMs, resp =>
                    {
                        if (resp.Contains("ok"))
                        {
                            localC = "ble_connected";
                            return "pass";
                        }
                        return Fail(out localC, "ble_connect_failed", $"回应不含 ok: {resp.Trim()}");
                    });
                    if (r1 != "pass") return r1;

                    // Step 2: 验证连接状态
                    return DoQuery("get_connect", timeoutMs, resp =>
                    {
                        if (resp.Contains("connect"))
                        {
                            localC = "pass";
                            return "pass";
                        }
                        return Fail(out localC, "no_connect", $"回应不含 connect: {resp.Trim()}");
                    });
                });

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] enter_test_mode_usb_dongle: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取 GPIO 引脚电平，与 a(期望值) 对比</summary>
        /// a: "期望值"（0 或 1，必填）
        /// d: "引脚号"（必填）
        /// c: 输出实际读取的值（"0" 或 "1"）
        /// 例: a="1" d="5" → 读取 GPIO5，期望高电平
        private string rtt_read_gpio_pin(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析引脚号
                if (string.IsNullOrEmpty(d))
                {
                    c = "fail;no_pin";
                    return "fail";
                }
                if (!int.TryParse(d.Trim(), out int pinNum) || pinNum < 0)
                {
                    c = "fail;invalid_pin";
                    return "fail";
                }

                // 2) 解析期望值
                if (string.IsNullOrEmpty(a))
                {
                    c = "fail;no_expected";
                    return "fail";
                }
                string expected = a.Trim();
                if (expected != "0" && expected != "1")
                {
                    c = "fail;invalid_expected";
                    return "fail";
                }

                // 3) 发送命令并解析回应
                string result = RetryQuery("read_gpio_pin", () =>
                    DoQuery($"gpio read {pinNum}", 3000, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s*(\d)");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        string value = m.Groups[1].Value;
                        localC = value;

                        if (value == expected)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin: GPIO{pinNum}={value} == 期望 {expected} ✓");
                            return "pass";
                        }

                        mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin: GPIO{pinNum}={value} != 期望 {expected} ✗");
                        return "fail;mismatch";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 设置 GPIO 引脚电平</summary>
        /// d: "引脚号;电平值" — 例: "5;1" → GPIO5 输出高
        private string rtt_write_gpio_pin(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析 d: "pin_num;status"
                string[] p = SplitParam(d, 2);
                if (p == null)
                {
                    c = "fail;param_error";
                    return "fail";
                }
                if (!int.TryParse(p[0].Trim(), out int pinNum) || pinNum < 0)
                {
                    c = "fail;invalid_pin";
                    return "fail";
                }
                string status = p[1].Trim();
                if (status != "0" && status != "1")
                {
                    c = "fail;invalid_status";
                    return "fail";
                }

                // 2) 发送命令
                string result = RetryQuery("write_gpio_pin", () =>
                    DoQuery($"gpio write {pinNum} {status}", 3000, resp =>
                    {
                        if (resp.Contains("OOK"))
                        {
                            localC = "pass";
                            mylib.utility_func.callbackdebuginfo($"[RTT] write_gpio_pin: GPIO{pinNum} ← {status} ✓");
                            return "pass";
                        }
                        return Fail(out localC, "write_failed", $"回应不含 OOK: {resp.Trim()}");
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] write_gpio_pin: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取 BLE 加密狗 GPIO 引脚电平（与 rtt_read_gpio_pin 回应格式不同）</summary>
        /// a: "期望值"（0 或 1，必填）
        /// d: "引脚号"（必填）
        /// c: 输出实际读取的值（"0" 或 "1"）
        /// 回应格式: "ok: {pin_num} {value}"，例 "ok: 5 1"
        private string read_gpio_pin_ble_dongle(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析引脚号
                if (string.IsNullOrEmpty(d))
                {
                    c = "fail;no_pin";
                    return "fail";
                }
                if (!int.TryParse(d.Trim(), out int pinNum) || pinNum < 0)
                {
                    c = "fail;invalid_pin";
                    return "fail";
                }

                // 2) 解析期望值
                if (string.IsNullOrEmpty(a))
                {
                    c = "fail;no_expected";
                    return "fail";
                }
                string expected = a.Trim();
                if (expected != "0" && expected != "1")
                {
                    c = "fail;invalid_expected";
                    return "fail";
                }

                // 3) 发送命令并解析回应
                string result = RetryQuery("read_gpio_pin_ble_dongle", () =>
                    DoQuery($"gpio read {pinNum}", 3000, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s\d{1,2}\s(\d{1})");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin_ble_dongle: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        string value = m.Groups[1].Value;
                        localC = value;

                        if (value == expected)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin_ble_dongle: GPIO{pinNum}={value} == 期望 {expected} ✓");
                            return "pass";
                        }

                        mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin_ble_dongle: GPIO{pinNum}={value} != 期望 {expected} ✗");
                        return "fail;mismatch";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_gpio_pin_ble_dongle: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>通过 RTT 读取 USB 加密狗 RSSI（信号强度），与 a(上限) b(下限) 比较</summary>
        /// a: "上限/hi-limit"（dBm，必填），如 "-30"
        /// b: "下限/low-limit"（dBm，必填），如 "-80"
        /// d: "超时ms"（可选，默认 3000）
        /// 有效范围: b ≤ RSSI ≤ a
        /// 例: a="-30" b="-80" → RSSI 在 -80 ~ -30 dBm 之间通过
        private string read_rssi_from_usb_dongle(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
                // 1) 解析上下限
                if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
                {
                    c = "fail;param_error";
                    return "fail";
                }
                if (!int.TryParse(a.Trim(), out int hiLimit) || !int.TryParse(b.Trim(), out int loLimit))
                {
                    c = "fail;invalid_limit";
                    return "fail";
                }
                if (loLimit > hiLimit)
                {
                    c = "fail;invalid_range";
                    return "fail";
                }

                // 2) 解析超时
                int timeoutMs = 3000;
                if (!string.IsNullOrEmpty(d)) int.TryParse(d.Trim(), out timeoutMs);

                // 3) 发送命令并解析
                string result = RetryQuery("read_rssi_from_usb_dongle", () =>
                    DoQuery("rread rssi", timeoutMs, resp =>
                    {
                        var m = Regex.Match(resp, @"ok:\s{0,1}([-|+][0-9]{1,2})");
                        if (!m.Success)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_rssi_from_usb_dongle: 回应格式错误: {resp.Trim()}");
                            return "fail;invalid_response";
                        }

                        int rssi = int.Parse(m.Groups[1].Value);
                        localC = rssi.ToString();

                        if (rssi >= loLimit && rssi <= hiLimit)
                        {
                            mylib.utility_func.callbackdebuginfo($"[RTT] read_rssi_from_usb_dongle: RSSI={rssi}dBm 在 [{loLimit},{hiLimit}] ✓");
                            return "pass";
                        }

                        mylib.utility_func.callbackdebuginfo($"[RTT] read_rssi_from_usb_dongle: RSSI={rssi}dBm 超出范围 [{loLimit},{hiLimit}] ✗");
                        localC = $"fail;out_of_range:{rssi}";
                        return "fail";
                    }));

                c = localC;
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] read_rssi_from_usb_dongle: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }


      

        /// <summary>将 SN + deviceid 保存到 MES_DATA/mes_data.csv（覆盖写入）</summary>
        /// 从 golb_var_default 取 input_sn 和 deviceid
        /// d: 无用，传空即可
        private string rtt_save_mes(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string sn = tc.golb_var_default.TryGetValue("input_sn", out object v1) ? v1?.ToString() : null;
                string hwid = tc.golb_var_default.TryGetValue("blemac", out object v2) ? v2?.ToString() : null;

                if (string.IsNullOrWhiteSpace(sn) || string.IsNullOrWhiteSpace(hwid))
                {
                    mylib.utility_func.callbackdebuginfo($"[RTT] save_mes: 数据不完整 sn=[{sn}] deviceid=[{hwid}]");
                    c = "fail;incomplete_data";
                    return "fail";
                }

                string dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MES_DATA");
                string filePath = System.IO.Path.Combine(dir, "mes_data.csv");

                System.IO.Directory.CreateDirectory(dir);
                //System.IO.File.WriteAllText(filePath, $"SN,deviceid\n{sn},{hwid}");

                System.IO.File.WriteAllText(filePath, $"{sn},,{hwid.Replace("0X","").Replace("0x","")},,pass");
                mylib.utility_func.callbackdebuginfo($"[RTT] save_mes: 已保存 {filePath}");
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] save_mes: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>异步播放 MP3/WAV 文件（后台播放，立即返回）</summary>
        /// d: "文件路径"
        /// 例: d="D:\\sounds\\alarm.mp3"
        private string rtt_play_mp3(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (string.IsNullOrWhiteSpace(d))
                {
                    c = "fail;no_path";
                    return "fail";
                }

                if (!System.IO.File.Exists(d))
                {
                    mylib.utility_func.callbackdebuginfo($"[RTT] play_mp3: 文件不存在 [{d}]");
                    c = "fail;file_not_found";
                    return "fail";
                }

                Task.Run(() =>
                {
                    try
                    {
                        using (var audioFile = new AudioFileReader(d))
                        using (var outputDevice = new WaveOutEvent())
                        {
                            outputDevice.Init(audioFile);
                            outputDevice.Play();
                            while (outputDevice.PlaybackState == PlaybackState.Playing)
                            {
                                Thread.Sleep(100);
                            }
                        }
                        mylib.utility_func.callbackdebuginfo($"[RTT] play_mp3: 播放完成 [{d}]");
                    }
                    catch (Exception ex)
                    {
                        mylib.utility_func.callbackdebuginfo($"[RTT] play_mp3: 播放失败 [{d}]: {ex.Message}");
                    }
                });

                mylib.utility_func.callbackdebuginfo($"[RTT] play_mp3: 已启动异步播放 [{d}]");
                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] play_mp3: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 弹出扫码输入框 → 正则验证条码 → RTT "read UUID" → 对比 MAC
        /// d: "正则表达式"（用于验证扫描的条码格式）
        /// </summary>
        private string rtt_scan_barcode(string a, string b, out string c, string d)
        {
            c = "fail";
            string localC = "fail";
            try
            {
        

                string pattern = d?.Trim();
                if (string.IsNullOrEmpty(pattern))
                { c = "fail;no_regex"; return "fail"; }
                pattern = d.Split(';')[0];
                // 验证正则有效性
                try { new Regex(pattern); }
                catch { c = "fail;invalid_regex"; return "fail"; }

                // 弹出扫码输入框（UI 线程）
                string mac_str = null;
                var mainForm = Application.OpenForms[0];
                if (mainForm == null || !mainForm.IsHandleCreated)
                { c = "fail;no_window"; return "fail"; }

                // rtt_scan_barcode 跑在 Task.Run 后台线程, 必须回到 UI 线程弹模态窗,
                // 否则会在线程池线程上创建消息泵, 弹窗行为不可靠
                mainForm.Invoke((MethodInvoker)(() =>
                {
                    try
                    {
                        using (var frm = new Form())
                        {
                            frm.Text = "Scan BLE MAC_ADDRESS";
                            frm.Size = new Size(420, 150);
                            frm.StartPosition = FormStartPosition.CenterParent;
                            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                            frm.MaximizeBox = false;
                            frm.MinimizeBox = false;
                            frm.TopMost = true;

                            var lbl = new Label { Text = "Scan BLE MAC_ADDRESS:", Location = new Point(20, 15), Size = new Size(380, 20) };
                            var txt = new TextBox { Location = new Point(20, 40), Size = new Size(380, 25), Font = new Font("Microsoft YaHei UI", 11) };
                            var btnOk = new Button { Text = "OK", Location = new Point(110, 75), Size = new Size(80, 30), DialogResult = DialogResult.OK };
                            var btnCancel = new Button { Text = "Cancel", Location = new Point(200, 75), Size = new Size(80, 30), DialogResult = DialogResult.Cancel };

                            frm.Controls.Add(lbl);
                            frm.Controls.Add(txt);
                            frm.Controls.Add(btnOk);
                            frm.Controls.Add(btnCancel);
                            frm.AcceptButton = btnOk;
                            frm.CancelButton = btnCancel;

                            if (frm.ShowDialog() == DialogResult.OK)
                                mac_str = txt.Text.Trim();
                        }
                    }
                    catch { }
                }));

                if (string.IsNullOrEmpty(mac_str))
                { c = "fail;cancelled"; return "fail"; }
                mac_str = "7C51" + mac_str.Replace(":", "").Replace("-", "").ToUpper();
                // 正则验证条码
                if (!Regex.IsMatch(mac_str, pattern))
                {
                    mylib.utility_func.callbackdebuginfo($"[RTT] scan_barcode: 条码 [{mac_str}] 不匹配正则 [{pattern}]");
                    c = "fail;barcode_mismatch";
                    return "fail";
                }

                mylib.utility_func.callbackdebuginfo($"[RTT] scan_barcode: 条码验证通过 [{mac_str}]");

                // RTT 读取 UUID（蓝牙 MAC 无冒号）
              string  ct = mylib.utility_func.run_console_pip(d.Split(';')[1], 50000, "pass",mac_str).ToString();
              string   result = "";
                if (ct.IndexOf(d.Split(';')[2]) >= 0)
                {

                    c = "pass";
                    return "pass";
                }
                else {

                    c = "fail";
                    return "fail";
                }
                   
           
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] scan_mac: {ex.Message}");
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        // ============================================================
        // 内部工具
        // ============================================================

        /// <summary>发送命令并等待回应，用 predicate 判定</summary>
        private string DoQuery(string cmd, int timeoutMs, Func<string, string> predicate)
        {
            // 清缓冲区预读
            FlushRtt();

            // 发送命令
            if (rtt.WriteString(cmd) <= 0)
                return "fail;write_failed";

            // 累积读取
            var sb = new StringBuilder();
            int elapsed = 0;
            while (elapsed < timeoutMs)
            {
                string chunk = rtt.ReadString(4096);
                if (!string.IsNullOrEmpty(chunk))
                {
                    sb.Append(chunk);
                    string all = sb.ToString();

                    // 每次有新数据就判定一次
                    string verdict = predicate(all);
                    if (verdict == "pass" || verdict.StartsWith("pass"))
                        return verdict;
                }
                Thread.Sleep(5);
                elapsed += 5;
            }

            string final = sb.ToString().Trim();
            mylib.utility_func.callbackdebuginfo($"[RTT] DoQuery timeout, received: {(final.Length > 200 ? final.Substring(0, 200) + "..." : final)}");
            return string.IsNullOrEmpty(final) ? "fail;no_response" : "fail;timeout";
        }

        /// <summary>清空 RTT 接收缓冲区</summary>
        private void FlushRtt()
        {
            for (int i = 0; i < 5; i++)
            {
                rtt.Read(4096);
                Thread.Sleep(5);
            }
        }

        /// <summary>自动重试包装：失败 → 重连 → 再试一次</summary>
        private string RetryQuery(string name, Func<string> action)
        {
            string result = action();
            if (result == "pass" || result.StartsWith("pass"))
                return result;

            mylib.utility_func.callbackdebuginfo($"[RTT] {name} 失败，尝试重连后重试...");
            if (!Reconnect())
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] {name} 重连失败");
                return "fail;reconnect_failed";
            }

            // 如果在交互模式，重新进入
            if (_interactiveMode)
            {
                rtt.WriteString("0xdeadbeef", false);
                Thread.Sleep(100);
            }

            return action();
        }

        /// <summary>重连 J-Link</summary>
        private bool Reconnect()
        {
            try
            {
                rtt?.Dispose();
                rtt = new JLinkRTT();
                rtt.Open();
                rtt.SetDevice(_device);
                rtt.SetSpeed(_speedKhz);
                rtt.SetTIF(_tif);
                rtt.Connect();
                return true;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[RTT] Reconnect: {ex.Message}");
                return false;
            }
        }

        /// <summary>解析 d 参数的 query 基础格式: "命令,关键词,超时ms"</summary>
        private bool ParseQueryArgs(string d, int minParts, out string cmd, out string kw, out int timeoutMs)
        {
            cmd = null; kw = null; timeoutMs = 3000;
            string[] p = SplitParam(d, minParts);
            if (p == null) return false;
            cmd = p[0];
            kw = p[1];
            timeoutMs = int.Parse(p[2]);
            return true;
        }

        /// <summary>分割 d 参数，失败返回 null</summary>
        private string[] SplitParam(string d, int expectedCount)
        {
            if (string.IsNullOrEmpty(d)) return null;
            string[] p = d.Trim().Split(';');
            if (p.Length < expectedCount) return null;
            return p;
        }

        private string Ok(out string c, string val)
        {
            c = val;
            return "pass";
        }

        private string Fail(out string c, string code, string detail)
        {
            c = $"fail;{code}";
            mylib.utility_func.callbackdebuginfo($"[RTT] {code}: {detail}");
            return "fail";
        }

        private bool CompareNumeric(double val, string op, double expected)
        {
            switch (op)
            {
                case ">" : return val >  expected;
                case "<" : return val <  expected;
                case ">=": return val >= expected;
                case "<=": return val <= expected;
                case "==": return Math.Abs(val - expected) < 0.0001;
                case "!=": return Math.Abs(val - expected) >= 0.0001;
                default:  return false;
            }
        }

        // ============================================================
        // IDefaultAction
        // ============================================================

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void set_default_set()
        {
            // RTT 测试暂无需要预设的全局变量
        }

        public void ClosePorts()
        {
            try { rtt?.Close(); } catch { }
        }

        // ============================================================
        // IDisposable
        // ============================================================

        public void Dispose()
        {
            try
            {
                ClosePorts();
                rtt?.Dispose();
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
