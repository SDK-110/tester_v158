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
            tc.funcs.Add(id + "discharge_time_test", discharge_time_test);
            tc.funcs.Add(id + "voltage_after_delay_test", voltage_after_delay_test);
            tc.funcs.Add(id + "cc_capacitance_test", cc_capacitance_test);
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


      

        /// <summary>
        /// 断开指定继电器后，精准计时测量电压从高位下降到阈值以下所用的放电时间(Δt)。
        /// a: 时间上限(秒) — Δt > a 判 fail (放电太慢)
        /// b: 时间下限(秒) — Δt &lt; b 判 fail (放电太快)
        /// c: out — pass 返回 Δt 秒数(如 "1.234")，fail 返回 "fail"
        /// d: 逗号分隔 "acq,dis,th[,max_wait]"
        ///    acq = 电压采集继电器号 (函数内自动闭合)
        ///    dis = 待断开继电器号   (函数内自动断开 → 触发电压下降)
        ///    th  = 阈值电压(V)
        ///    max_wait = 最大等待毫秒(默认60000)
        /// 内部流程:
        ///   1. 闭合 acq 继电器 → 读初始电压确认 ≥ th
        ///   2. Stopwatch 启动 → 断开 dis 继电器 (T₀)
        ///   3. 每轮采样: t_pre→读表→t_post, 中点时间戳 + 电压值
        ///   4. 首过阈值时前后两点线性插值求精确 T₁
        ///   5. Δt = T₁ − T₀, 判定 b ≤ Δt ≤ a → pass
        /// </summary>
        private string discharge_time_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // ── 1. 解析 d ──────────────────────────────────────
                var parts = d.Split(',');
                if (parts.Length < 3)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] discharge_time_test ERR: d format invalid, got '{d}'");
                    return "fail";
                }
                string acqRelay = parts[0].Trim();
                string disRelay = parts[1].Trim();
                double threshold = double.Parse(parts[2].Trim());
                int maxWaitMs = parts.Length > 3 ? int.Parse(parts[3].Trim()) : 60000;
                double timeUpper = double.Parse(a);
                double timeLower = double.Parse(b);

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] discharge_time_test start: " +
                    $"acq=#{acqRelay} dis=#{disRelay} th={threshold}V " +
                    $"limit=[{timeLower},{timeUpper}]s maxWait={maxWaitMs}ms");

                // ── 2. 闭合电压采集继电器 ─────────────────────────
                tc.Getfun()["relay_set"]("", "", out _, $"#{acqRelay}:1#");
                Thread.Sleep(200);

                // ── 3. 读取初始电压 ───────────────────────────────
                string initReading = "";
                tc.funcs["md3058_read_DC_200V"]("9999", "0", out initReading, "");
                double initVolt;
                if (!double.TryParse(initReading, out initVolt) || initVolt <= threshold)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] discharge_time_test FAIL: V_init={initReading}V <= th={threshold}V");
                    return "fail";
                }

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] discharge_time_test: V_init={initVolt:F3}V, start discharge...");

                // ── 4. 断开触发继电器，计时起点 T₀ ──────────────
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double t0 = sw.Elapsed.TotalSeconds;
                tc.Getfun()["relay_set"]("", "", out _, $"#{disRelay}:0#");

                double tPrev = t0;
                double vPrev = initVolt;
                int sampleCount = 0;

                // ── 5. 采样循环 ──────────────────────────────────
                while (true)
                {
                    double elapsed = sw.Elapsed.TotalSeconds - t0;
                    if (elapsed * 1000 > maxWaitMs)
                    {
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] discharge_time_test FAIL: timeout {maxWaitMs}ms, " +
                            $"V_last={vPrev:F3}V, samples={sampleCount}");
                        return "fail";
                    }

                    // 读电压，打时间戳 (t_pre / t_post)
                    long ticksPre = sw.ElapsedTicks;
                    string reading = "";
                    tc.funcs["md3058_read_DC_200V"]("9999", "0", out reading, "");
                    long ticksPost = sw.ElapsedTicks;

                    double vCurr;
                    if (!double.TryParse(reading, out vCurr)) continue;
                    if (vCurr < -5000) continue;  // md3058 异常返回 -10000

                    // 采样时刻取中点（抵消读表耗时）
                    double tCurr = ((double)(ticksPre + ticksPost) / 2)
                                   / System.Diagnostics.Stopwatch.Frequency;

                    sampleCount++;
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] discharge_time_test #{sampleCount}: " +
                        $"t={tCurr - t0:F3}s  V={vCurr:F3}V");

                    // ── 6. 检测首过阈值 ─────────────────────────
                    if (vPrev > threshold && vCurr <= threshold)
                    {
                        double fraction = (threshold - vPrev) / (vCurr - vPrev);
                        fraction = Math.Max(0, Math.Min(1, fraction));
                        double tCross = tPrev + fraction * (tCurr - tPrev);
                        double deltaT = tCross - t0;

                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] discharge_time_test: threshold crossed at " +
                            $"tCross={tCross - t0:F3}s (interpolated), Δt={deltaT:F3}s, " +
                            $"n={sampleCount}, limit=[{timeLower},{timeUpper}]s");

                        if (deltaT < timeLower)
                        {
                            utility_func.callbackdebuginfo(
                                $"[asmpt03214220] discharge_time_test FAIL: too_fast " +
                                $"Δt={deltaT:F3}s < b={timeLower:F3}s");
                            return "fail";
                        }
                        if (deltaT > timeUpper)
                        {
                            utility_func.callbackdebuginfo(
                                $"[asmpt03214220] discharge_time_test FAIL: too_slow " +
                                $"Δt={deltaT:F3}s > a={timeUpper:F3}s");
                            return "fail";
                        }

                        c = deltaT.ToString("F3");
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] discharge_time_test PASS: Δt={deltaT:F3}s " +
                            $"[{timeLower},{timeUpper}]s");
                        return "pass";
                    }

                    tPrev = tCurr;
                    vPrev = vCurr;

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] discharge_time_test error: {ex.Message}");
                return "fail";
            }
        }

        /// <summary>
        /// 断开触发继电器后精准延时 N 秒，读取电压并确认其在 [b, a] 范围内。
        /// 与 discharge_time_test 同构：只测「N 秒后的电压」，而非放电到阈值的时间。
        /// a: 电压上限(V) — 读数 ≤ a 才 pass
        /// b: 电压下限(V) — 读数 ≥ b 才 pass
        /// c: out — pass 返回实测电压(如 "12.345")，fail 返回 "fail;原因"
        /// d: 逗号分隔 "acq,dis,delay_s[,max_wait_ms]"
        ///    acq = 电压采集继电器号 (函数内自动闭合)
        ///    dis = 触发继电器号     (函数内自动断开 → T₀ 计时起点)
        ///    delay_s = 精准延时秒数 (可小数，如 3.5)
        ///    max_wait_ms = 最大等待毫秒(默认 60000)
        /// 内部流程:
        ///   1. 闭合 acq 继电器 → 读初始电压 (读数异常直接 fail)
        ///   2. Stopwatch 启动 → 断开 dis 继电器 (T₀)
        ///   3. 精准延时 N 秒: Sleep 到接近目标 + 忙等补足
        ///   4. 到点读表, t_pre/t_post 中点作为实际采样时刻
        ///   5. 判定 b ≤ V ≤ a → pass
        /// </summary>
        private string voltage_after_delay_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // ── 1. 解析 d ──────────────────────────────────────
                var parts = d.Split(',');
                if (parts.Length < 3)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] voltage_after_delay_test ERR: d format invalid, got '{d}'");
                    return "fail";
                }
                string acqRelay = parts[0].Trim();
                string disRelay = parts[1].Trim();
                double delayS = double.Parse(parts[2].Trim());
                int maxWaitMs = parts.Length > 3 ? int.Parse(parts[3].Trim()) : 60000;
                double voltUpper = double.Parse(a);
                double voltLower = double.Parse(b);

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] voltage_after_delay_test start: " +
                    $"acq=#{acqRelay} dis=#{disRelay} delay={delayS}s " +
                    $"range=[{voltLower},{voltUpper}]V maxWait={maxWaitMs}ms");

                // ── 2. 闭合电压采集继电器 ─────────────────────────
                tc.Getfun()["relay_set"]("", "", out _, $"#{acqRelay}:1#");
                Thread.Sleep(200);

                // ── 3. 读取初始电压 (读数异常直接 fail) ──────────
                string initReading = "";
                tc.funcs["md3058_read_DC_200V"]("9999", "0", out initReading, "");
                double initVolt;
                if (!double.TryParse(initReading, out initVolt) || initVolt < -5000)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] voltage_after_delay_test FAIL: V_init abnormal '{initReading}'");
                    return "fail";
                }

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] voltage_after_delay_test: V_init={initVolt:F3}V, " +
                    $"wait {delayS}s, start...");

                // ── 4. 断开触发继电器，计时起点 T₀ ──────────────
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double t0 = sw.Elapsed.TotalSeconds;
                tc.Getfun()["relay_set"]("", "", out _, $"#{disRelay}:0#");

                // ── 5. 精准延时 N 秒: 接近目标后忙等补足 ────────
                while (true)
                {
                    double elapsed = sw.Elapsed.TotalSeconds - t0;
                    if (elapsed * 1000 > maxWaitMs)
                    {
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] voltage_after_delay_test FAIL: timeout {maxWaitMs}ms");
                        return "fail";
                    }

                    double remainS = delayS - elapsed;
                    if (remainS <= 0) break;

                    // 剩余较多 → 睡到接近目标(留 5ms)；最后 5ms 用 Sleep(1) 补足保证精度
                    int sleepMs = (int)(remainS * 1000);
                    Thread.Sleep(sleepMs > 5 ? sleepMs - 5 : 1);
                }

                // ── 6. 到点读表, 中点时间戳 ─────────────────────
                long ticksPre = sw.ElapsedTicks;
                string reading = "";
                tc.funcs["md3058_read_DC_200V"]("9999", "0", out reading, "");
                long ticksPost = sw.ElapsedTicks;

                double volt;
                if (!double.TryParse(reading, out volt) || volt < -5000)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] voltage_after_delay_test FAIL: read abnormal '{reading}'");
                    return "fail";
                }

                double tSample = ((double)(ticksPre + ticksPost) / 2)
                                 / System.Diagnostics.Stopwatch.Frequency - t0;

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] voltage_after_delay_test: " +
                    $"V={volt:F3}V @ t={tSample:F3}s (target {delayS:F3}s), " +
                    $"range=[{voltLower},{voltUpper}]V");

                // ── 7. 判定范围 ─────────────────────────────────
                if (volt < voltLower || volt > voltUpper)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] voltage_after_delay_test FAIL: " +
                        $"V={volt:F3}V out of range [{voltLower},{voltUpper}]V");
                    c = $"fail;V={volt:F3}V;t={tSample:F3}s";
                    return "fail";
                }

                c = volt.ToString("F3");
                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] voltage_after_delay_test PASS: " +
                    $"V={volt:F3}V [{voltLower},{voltUpper}]V");
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] voltage_after_delay_test error: {ex.Message}");
                return "fail";
            }
        }

        /// <summary>
        /// 恒流充电法测大电容: C = I × Δt / ΔU
        /// 外部提供恒流源(如 100mA)，本函数精准测电压上升和时间累计。
        /// 调用前请将被测电容放电至接近 0V。
        /// a: 电容上限(F)
        /// b: 电容下限(F)
        /// c: out — pass 返回实测电容值(如 "0.412")，fail 返回 "fail"
        /// d: 逗号分隔 "cc_relay,acq_relay,current_A,delta_u_V[,timeout_s]"
        ///    cc_relay  = 恒流源继电器号   (函数内自动闭合→开始充电)
        ///    acq_relay = 电压采集继电器号 (函数内自动闭合→接万用表)
        ///    current_A = 恒流值(A)，如 0.1 表示 100mA
        ///    delta_u_V = 目标电压上升值(V)，如 1.0
        ///    timeout_s = 超时秒数(默认 10)
        /// 流程:
        ///   1. 闭合 acq_relay → 读初始电压 V_start
        ///   2. 闭合 cc_relay → 开始充电, Stopwatch 启动 (T₀)
        ///   3. 高速采样(每点 t_mid + V)，检测 V ≥ V_start + ΔU
        ///   4. 首过时线性插值求精确 T_cross → Δt
        ///   5. C = I × Δt / ΔU, 判定 b ≤ C ≤ a → pass
        /// </summary>
        private string cc_capacitance_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // ── 1. 解析 d ──────────────────────────────────────
                var parts = d.Split(',');
                if (parts.Length < 4)
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] cc_capacitance_test ERR: d need 4+ fields, got '{d}'");
                    return "fail";
                }
                string ccRelay = parts[0].Trim();          // 恒流源继电器号
                string acqRelay = parts[1].Trim();         // 电压采集继电器号
                double currentA = double.Parse(parts[2].Trim());  // 恒流值 (A)
                double deltaUV = double.Parse(parts[3].Trim());   // 目标 ΔU (V)
                int timeoutS = parts.Length > 4 ? int.Parse(parts[4].Trim()) : 10;

                double capUpper = double.Parse(a);         // 电容上限 (F)
                double capLower = double.Parse(b);         // 电容下限 (F)

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] cc_capacitance_test start: " +
                    $"cc=#{ccRelay} acq=#{acqRelay} I={currentA}A " +
                    $"ΔU={deltaUV}V limit=[{capLower},{capUpper}]F timeout={timeoutS}s");

                // ── 2. 闭合电压采集继电器 → 读 V_start ───────────
                tc.Getfun()["relay_set"]("", "", out _, $"#{acqRelay}:1#");
                Thread.Sleep(200);

                string startReading = "";
                tc.funcs["md3058_read_DC_200V"]("9999", "0", out startReading, "");
                double vStart;
                if (!double.TryParse(startReading, out vStart))
                {
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] cc_capacitance_test FAIL: V_start read error '{startReading}'");
                    return "fail";
                }

                double vTarget = vStart + deltaUV;

                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] cc_capacitance_test: V_start={vStart:F4}V, " +
                    $"V_target={vTarget:F4}V, charging...");

                // ── 3. 闭合恒流源继电器，开始充电，计时 T₀ ─────
                var sw = System.Diagnostics.Stopwatch.StartNew();
                double t0 = sw.Elapsed.TotalSeconds;
                tc.Getfun()["relay_set"]("", "", out _, $"#{ccRelay}:1#");

                double vPrev = vStart;
                double tPrev = t0;
                int sampleCount = 0;

                // ── 4. 采样循环 ──────────────────────────────────
                while (true)
                {
                    double elapsed = sw.Elapsed.TotalSeconds - t0;
                    if (elapsed > timeoutS)
                    {
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] cc_capacitance_test FAIL: timeout {timeoutS}s, " +
                            $"V={vPrev:F4}V (target {vTarget:F4}V), samples={sampleCount}");
                        return "fail";
                    }

                    // 读电压 + 时间戳
                    long ticksPre = sw.ElapsedTicks;
                    string reading = "";
                    tc.funcs["md3058_read_DC_200V"]("9999", "0", out reading, "");
                    long ticksPost = sw.ElapsedTicks;

                    double vCurr;
                    if (!double.TryParse(reading, out vCurr)) continue;
                    if (vCurr < -5000) continue;  // md3058 异常值

                    double tCurr = ((double)(ticksPre + ticksPost) / 2)
                                   / System.Diagnostics.Stopwatch.Frequency;

                    sampleCount++;
                    utility_func.callbackdebuginfo(
                        $"[asmpt03214220] cc_capacitance_test #{sampleCount}: " +
                        $"t={tCurr - t0:F3}s  V={vCurr:F4}V  ΔV={vCurr - vStart:F4}V");

                    // ── 5. 检测达到目标电压 ─────────────────────
                    if (vPrev < vTarget && vCurr >= vTarget)
                    {
                        // 线性插值求精确过线时刻
                        double fraction = (vTarget - vPrev) / (vCurr - vPrev);
                        fraction = Math.Max(0, Math.Min(1, fraction));
                        double tCross = tPrev + fraction * (tCurr - tPrev);
                        double deltaT = tCross - t0;

                        // C = I × Δt / ΔU
                        double capacitance = currentA * deltaT / deltaUV;

                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] cc_capacitance_test: V_target reached " +
                            $"tCross={deltaT:F3}s (interpolated), " +
                            $"C={capacitance:F6}F, limit=[{capLower},{capUpper}]F");

                        if (capacitance < capLower)
                        {
                            utility_func.callbackdebuginfo(
                                $"[asmpt03214220] cc_capacitance_test FAIL: " +
                                $"C={capacitance:F6}F < b={capLower:F6}F");
                            return "fail";
                        }
                        if (capacitance > capUpper)
                        {
                            utility_func.callbackdebuginfo(
                                $"[asmpt03214220] cc_capacitance_test FAIL: " +
                                $"C={capacitance:F6}F > a={capUpper:F6}F");
                            return "fail";
                        }

                        c = capacitance.ToString("F6");
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] cc_capacitance_test PASS: " +
                            $"C={capacitance:F6}F [{capLower},{capUpper}]F");
                        return "pass";
                    }

                    // ── 6. 电压下降检查（恒流充电时电压不应下降） ─
                    if (vCurr < vPrev - 0.05 && sampleCount > 2)
                    {
                        utility_func.callbackdebuginfo(
                            $"[asmpt03214220] cc_capacitance_test FAIL: " +
                            $"voltage dropping (V={vCurr:F4}V < prev={vPrev:F4}V) " +
                            $"— possible bad connection or CC source off");
                        return "fail";
                    }

                    tPrev = tCurr;
                    vPrev = vCurr;

                    Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo(
                    $"[asmpt03214220] cc_capacitance_test error: {ex.Message}");
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
