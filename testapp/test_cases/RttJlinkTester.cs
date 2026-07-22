using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using DeviceLibrary;
using testapp.glob_set;

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
        private string _device = "nRF52840_xxAA";
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

                // 初始化 RTT（Terminal API 优先）
                if (rtt.UseTerminalApi)
                {
                    rtt.EnableTerminalApi();
                }

                if (!rtt.IsConnected)
                {
                    c = "fail;connect_failed";
                    return "fail";
                }

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
                    if (rtt.WriteString("0xdeadbeef", false) <= 0)
                        return "fail;write_failed";

                    int waited = 0;
                    while (waited < timeoutMs)
                    {
                        string rev = rtt.ReadString(4096);
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
                if (rtt.UseTerminalApi)
                    rtt.EnableTerminalApi();
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
            string[] p = d.Trim().Split(',');
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
