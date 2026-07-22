using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    public class asmpt03214220_pj : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "";
        sevy_relay ry;
        tonghui2818b th2818;

        /// <summary>
        /// 构造函数，自动检查 ry 和 th2818 是否为空，为空则从 setup.ini 创建
        /// </summary>
        public asmpt03214220_pj(testcase_dll _tc, ref sevy_relay _ry, ref tonghui2818b _th2818)
        {
            tc = _tc;

            // ── 检查并创建继电器对象 ──────────────────────────
            if (_ry == null)
            {
                try
                {
                    var ini = glob_ini_instance.getInstance().getSetupIniData;
                    string port = ini["setport"]["Relay_board"];
                    int baud = int.Parse(ini["setport"]["Relay_board_baudrate"]);
                    _ry = new sevy_relay(port, baud);
                }
                catch (Exception ex)
                {
                    utility_func.callbackdebuginfo($"[asmpt77221] create ry failed: {ex.Message}");
                }
            }
            ry = _ry;

            // ── 检查并创建 LCR 电桥对象 ──────────────────────
            if (_th2818 == null)
            {
                try
                {
                    var ini = glob_ini_instance.getInstance().getSetupIniData;
                    string port = ini["setport"]["tonghui2818b"];
                    int baud = int.Parse(ini["setport"]["tonghui2818b_baudrate"]);
                    _th2818 = new tonghui2818b(_tc, port, baud);
                }
                catch (Exception ex)
                {
                    utility_func.callbackdebuginfo($"[asmpt77221] create th2818 failed: {ex.Message}");
                }
            }
            th2818 = _th2818;

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "asmpt77221_";
            tc.funcs.Add(id + "cal_lcr", cal_lcr);
            tc.funcs.Add(id + "loop_waiting_read_voltage", loop_waiting_read_voltage);
        }

        /// <summary>
        /// LCR 电桥开路/短路校准
        /// d 参数:
        ///   空或 ALL = 开路 + 短路校准
        ///   "OPEN"   = 仅开路校准
        ///   "SHORT"  = 仅短路校准
        /// </summary>
        private string cal_lcr(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (th2818 == null) { c = "th2818_is_null"; return "fail"; }

                string mode = string.IsNullOrEmpty(d) ? "ALL" : d.Trim().ToUpper();

                // ── 开路校准 ──────────────────────────────────
                if (mode == "ALL" || mode == "OPEN")
                {
                    // 吸合1#继电器（接开路标准）
                    tc.Getfun()["relay_set"]("", "", out _, "#1:1#");
                    System.Threading.Thread.Sleep(300);

                    th2818.OpenCal();

                    // 释放1#继电器
                    tc.Getfun()["relay_set"]("", "", out _, "#1:0#");
                    System.Threading.Thread.Sleep(200);
                }

                // ── 短路校准 ──────────────────────────────────
                if (mode == "ALL" || mode == "SHORT")
                {
                    // 吸合2#继电器（接短路标准）
                    tc.Getfun()["relay_set"]("", "", out _, "#2:1#");
                    System.Threading.Thread.Sleep(300);

                    th2818.ShortCal();

                    // 释放2#继电器
                    tc.Getfun()["relay_set"]("", "", out _, "#2:0#");
                    System.Threading.Thread.Sleep(200);
                }

                // 确保所有继电器释放
                tc.Getfun()["relay_set"]("", "", out _, "#1:0#");
                tc.Getfun()["relay_set"]("", "", out _, "#2:0#");

                c = "pass";
                return "pass";
            }
            catch (Exception ex)
            {
                // 异常时确保继电器释放
                try { tc.Getfun()["relay_set"]("", "", out _, "#1:0#"); } catch { }
                try { tc.Getfun()["relay_set"]("", "", out _, "#2:0#"); } catch { }

                utility_func.callbackdebuginfo($"[asmpt77221] cal_lcr error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 在指定总时长内每 500ms 调用 md3058_read_DC_200V 读取电压，监测电压下降至阈值。
        /// a: 电压上限（用于 md3058 的读数范围上限，传 "9999" 即可）
        /// b: 电压下降阈值 — 读数 ≤ 此值即认为下降完成
        /// c: out — 最终电压读数 + 采样信息
        /// d: 总监测时长(ms)，默认 30000ms（30 秒）
        /// 返回 "pass"（电压降到阈值以下）或 "fail"（超时未达到）
        /// </summary>
        private string loop_waiting_read_voltage(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                int totalWaitMs = string.IsNullOrEmpty(d) ? 30000 : int.Parse(d);
                const int intervalMs = 500;
                int maxLoops = totalWaitMs / intervalMs;

                double threshold = double.Parse(b);
                List<double> readings = new List<double>();

                for (int i = 0; i < maxLoops; i++)
                {
                    Thread.Sleep(intervalMs);

                    string reading = "";
                    tc.funcs["md3058_read_DC_200V"](a, b, out reading, "");

                    double volt;
                    if (double.TryParse(reading, out volt))
                    {
                        readings.Add(volt);
                        utility_func.callbackdebuginfo(
                            $"[asmpt77221] loop_waiting_read_voltage #{i + 1}: {volt:F3}V");

                        // 电压降到阈值以下 → 判定完成
                        if (volt <= threshold)
                        {
                            c = $"{volt:F3};loop={i + 1};samples={readings.Count}";
                            return "pass";
                        }
                    }
                }

                // 超时仍未降到阈值
                string last = readings.Count > 0 ? readings.Last().ToString("F3") : "N/A";
                c = $"fail;timeout;last={last};samples={readings.Count}";
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[asmpt77221] loop_waiting_read_voltage error: {ex.Message}");
                c = $"fail;{ex.Message}";
                return "fail";
            }
        }

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void ClosePorts()
        {
        }

        public void set_default_set()
        {
        }

        public void Dispose()
        {
            try
            {
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
