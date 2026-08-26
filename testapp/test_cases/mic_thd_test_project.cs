using NAudio.Wave;
using System;
using System.Text;
using System.Threading;
using testapp.glob_set;

namespace testapp.test_cases
{
    /// <summary>
    /// 通过麦克风采集喇叭声音、用 Goertzel 算法计算 THD（总谐波失真）+ 声音强度的测试模块。
    /// 框架结构参考 Domeitc_CPV_599_project，符合测试引擎的 IDefaultAction 约定。
    ///
    /// 提供 3 个标准测试函数:
    ///   1) mic_thd_measure        — 测 THD，可顺便判音量（dBFS 下限，可选）
    ///   2) mic_thd_measure_level  — 只测声音强度（RMS/Peak/dBFS），不算 THD
    ///   3) mic_thd_list_devices   — 列出系统可用麦克风
    ///
    /// 用法（在测试用例配置里）:
    ///   【测 THD（同时暴露音量数据）】
    ///     函数名: mic_thd_measure
    ///     high (a): THD 上限%，如 "5.0"
    ///     low  (b): THD 下限%，如 "0"     （一般填 0）
    ///     parameter (d): "基频Hz,采集时长ms[,设备索引][,谐波个数][,FFT大小][,音量下限dBFS]"
    ///                     默认 "1000,2000,0,5,8192"
    ///                     第6字段"音量下限dBFS"可选，填了就同时判音量
    ///                     例: "1000,2000,0,5,8192,-20"  要求音量 ≥ -20 dBFS
    ///     返回: pass / fail
    ///     out c: 测到的 THD%（如 "0.2345"）或 "fail;reason"
    ///
    ///   【只测声音强度】
    ///     函数名: mic_thd_measure_level
    ///     high (a): dBFS 上限，如 "-3"  （过载保护，可空=不限上限）
    ///     low  (b): dBFS 下限，如 "-30" （喇叭必须 ≥ 此值才算响，必填）
    ///     parameter (d): "采集时长ms[,设备索引]"  默认 "1000,0"
    ///     返回: pass / fail
    ///     out c: 实测 dBFS（如 "-15.23"）或 "fail;reason"
    ///
    ///   【列设备】
    ///     函数名: mic_thd_list_devices
    ///     out c 输出 "0: DeviceName\n1: DeviceName..."
    ///
    /// 声音强度指标:
    ///   RMS   = sqrt(mean(x²))       均方根，0~1.0，接近人耳响度感
    ///   Peak  = max(|x|)             峰值，0~1.0，过载检测用
    ///   dBFS  = 20·log10(RMS)        满量程分贝，0=满刻度，负值越接近0越响
    ///                                  （满刻度正弦约 -3 dBFS，静音 ≈ -∞）
    ///
    /// 全局变量（供后续测试步骤引用）:
    ///   mic_thd_last_thd_pct      上次测到的 THD%
    ///   mic_thd_last_clipped      是否削顶
    ///   mic_thd_last_fund_freq   实测基波频率
    ///   mic_thd_last_rms_pct      RMS 占满量程百分比
    ///   mic_thd_last_peak_pct     Peak 占满量程百分比
    ///   mic_thd_last_dbfs         dBFS（-999 表示静音/未测到）
    /// </summary>
    public class mic_thd_test_project : IDefaultAction, IDisposable
    {
        private testcase_dll tc;
        private string id = "mic_thd_";

        // —— 采集默认参数 ——
        private const int DefaultSampleRate = 48000;   // 与 mic_test.cs 保持一致
        private const int DefaultFftSize = 8192;
        private const int DefaultHarmonics = 5;
        private const int DefaultDurationMs = 2000;
        private const int DefaultDeviceIndex = 0;

        public mic_thd_test_project(testcase_dll _tc)
        {
            tc = _tc;
            Initialize();
        }

        public void Initialize()
        {
            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            tc.funcs.Add(id + "measure", measure_thd);
            tc.funcs.Add(id + "measure_level", measure_level);
            tc.funcs.Add(id + "list_devices", list_mic_devices);
            tc.golb_var_default["mic_thd_last_thd_pct"] = "-100";
            tc.golb_var_default["mic_thd_last_clipped"] = "false";
            tc.golb_var_default["mic_thd_last_fund_freq"] = "-100";
            tc.golb_var_default["mic_thd_last_rms_pct"] = "-100";
            tc.golb_var_default["mic_thd_last_peak_pct"] = "-100";
            tc.golb_var_default["mic_thd_last_dbfs"] = "-999";
        }

        // ──────────────────────────────────────────────
        // 标准测试函数: 通过 MIC 检测喇叭 THD（同时暴露声音强度数据）
        // a = THD 上限% (high)，b = THD 下限% (low)
        // d = "基频Hz,采集时长ms[,设备索引][,谐波个数][,FFT大小][,音量下限dBFS]"
        //     默认 "1000,2000,0,5,8192"
        //     示例: "1000,2000"          1kHz, 2s, 设备0, 5谐波, FFT8192, 不判音量
        //           "1000,3000,1,5,16384" 1kHz, 3s, 设备1, 5谐波, FFT16384
        //           "1000,2000,0,5,8192,-20" 1kHz, 2s, ... 音量需 ≥ -20 dBFS
        // c = out: 测到的 THD%（"0.2345"）或 "fail;reason"
        // 返回: "pass" / "fail"
        // ──────────────────────────────────────────────
        private string measure_thd(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // —— 解析参数 d ——
                // 格式: 基频Hz,采集时长ms,设备索引,谐波个数,FFT大小,音量下限dBFS
                //        第3~6字段均可省略；第6字段省略=不判音量
                if (string.IsNullOrEmpty(d)) d = "1000,2000,0,5,8192";
                string[] p = d.Trim().Replace("'","").Split(',');
                double targetFreq = double.Parse(p[0]);
                int durationMs = p.Length > 1 ? int.Parse(p[1]) : DefaultDurationMs;
                int deviceIndex = p.Length > 2 ? int.Parse(p[2]) : DefaultDeviceIndex;
                int harmonicCount = p.Length > 3 ? int.Parse(p[3]) : DefaultHarmonics;
                int fftSize = p.Length > 4 ? int.Parse(p[4]) : DefaultFftSize;
                // 第6字段: 音量下限 dBFS（可省略，省略=NaN=不判音量）
                double levelMinDbfs = (p.Length > 5 && !string.IsNullOrWhiteSpace(p[5]))
                    ? double.Parse(p[5]) : double.NaN;

                if (targetFreq <= 0)
                {
                    c = "fail;invalid_freq";
                    return "fail";
                }
                if (harmonicCount < 1)
                {
                    c = "fail;invalid_harmonic_count";
                    return "fail";
                }
                // Nyquist 校验
                double highestHarmonic = targetFreq * (harmonicCount + 1);
                if (highestHarmonic >= DefaultSampleRate / 2.0)
                {
                    c = $"fail;nyquist;highest={highestHarmonic}Hz>={DefaultSampleRate / 2}Hz";
                    return "fail";
                }

                // —— 解析判定上下限 ——
                if (string.IsNullOrEmpty(a))
                {
                    c = "fail;no_high_limit";
                    return "fail";
                }
                double highThd = double.Parse(a);
                double lowThd = string.IsNullOrEmpty(b) ? 0 : double.Parse(b);

                // —— 调用核心算法（同时算 THD 和声音强度）——
                double thdPct;
                bool clipped;
                double fundFreqMeasured;
                double rmsPct, peakPct, dbfs;
                MeasureThdCore(targetFreq, durationMs, deviceIndex, harmonicCount, fftSize,
                               out thdPct, out clipped, out fundFreqMeasured,
                               out rmsPct, out peakPct, out dbfs);

                // —— 保存结果到全局变量，供后续步骤引用 ——
                tc.golb_var_default["mic_thd_last_thd_pct"] = thdPct.ToString("F4");
                tc.golb_var_default["mic_thd_last_clipped"] = clipped ? "true" : "false";
                tc.golb_var_default["mic_thd_last_fund_freq"] = fundFreqMeasured.ToString("F2");
                tc.golb_var_default["mic_thd_last_rms_pct"] = rmsPct.ToString("F4");
                tc.golb_var_default["mic_thd_last_peak_pct"] = peakPct.ToString("F4");
                tc.golb_var_default["mic_thd_last_dbfs"] = double.IsInfinity(dbfs) ? "-999" : dbfs.ToString("F2");

                // —— 削顶优先报 fail ——
                if (clipped)
                {
                    c = $"fail;clipped;thd={thdPct:F4};dbfs={dbfs:F2}";
                    mylib.utility_func.callbackdebuginfo(
                        $"[MicThd] NG: audio clipped! thd={thdPct:F4}% dbfs={dbfs:F2}dBFS (mic gain too high)");
                    return "fail";
                }

                // —— 判定 ——
                mylib.utility_func.callbackdebuginfo(
                    $"[MicThd] f0={targetFreq}Hz measured_f0={fundFreqMeasured:F1}Hz " +
                    $"thd={thdPct:F4}% limit=[{lowThd},{highThd}] " +
                    $"rms={rmsPct:F4} peak={peakPct:F4} dbfs={dbfs:F2}dBFS");

                bool thdOk = thdPct >= lowThd && thdPct <= highThd;
                bool levelOk = double.IsNaN(levelMinDbfs) || dbfs >= levelMinDbfs;

                if (thdOk && levelOk)
                {
                    c = $"pass;thd={thdPct:F4};rms={rmsPct:F4} peak={peakPct:F4} dbfs={dbfs:F2}dBFS";
                    return "pass";
                }
                else
                {
                    if (!thdOk && !levelOk)
                        c = $"fail;thd={thdPct:F4};limit={lowThd}-{highThd};dbfs={dbfs:F2}<min{levelMinDbfs}";
                    else if (!thdOk)
                        c = $"fail;thd={thdPct:F4};limit={lowThd}-{highThd}";
                    else
                        c = $"fail;dbfs={dbfs:F2}<min{levelMinDbfs}";
                    return "fail";
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[MicThd] measure_thd error: {ex.Message}");
                c = "fail;exception;" + ex.Message;
                return "fail";
            }
        }

        // ──────────────────────────────────────────────
        // 标准测试函数: 通过 MIC 检测声音强度（RMS/Peak/dBFS）
        // a = dBFS 上限 (high)，可空=不设上限
        // b = dBFS 下限 (low)，必填（喇叭必须 ≥ 此值才算响）
        //     典型用法: a="-3"（过载保护上限）, b="-30"（喇叭必须响到 -30 dBFS 以上）
        // d = "采集时长ms[,设备索引]"  默认 "1000,0"
        // c = out: 实测 dBFS（如 "-15.23"）或 "fail;reason"
        // 返回: "pass" / "fail"
        // ──────────────────────────────────────────────
        private string measure_level(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // —— 解析参数 d ——
                if (string.IsNullOrEmpty(d)) d = "1000,0";
                string[] p = d.Trim().Split(',');
                int durationMs = int.Parse(p[0]);
                int deviceIndex = p.Length > 1 ? int.Parse(p[1]) : DefaultDeviceIndex;
                if (durationMs <= 0) durationMs = DefaultDurationMs;

                // —— 解析判定上下限 ——
                // a 可空（不设上限），b 必填（下限）
                double highDbfs = string.IsNullOrEmpty(a) ? double.NaN : double.Parse(a);
                if (string.IsNullOrEmpty(b))
                {
                    c = "fail;no_low_limit";
                    return "fail";
                }
                double lowDbfs = double.Parse(b);

                // —— 调用核心算法 ——
                double rmsPct, peakPct, dbfs;
                bool clipped;
                MeasureLevelCore(durationMs, deviceIndex,
                                 out rmsPct, out peakPct, out dbfs, out clipped);

                // —— 保存到全局变量 ——
                tc.golb_var_default["mic_thd_last_rms_pct"] = rmsPct.ToString("F4");
                tc.golb_var_default["mic_thd_last_peak_pct"] = peakPct.ToString("F4");
                tc.golb_var_default["mic_thd_last_dbfs"] = double.IsInfinity(dbfs) ? "-999" : dbfs.ToString("F2");
                tc.golb_var_default["mic_thd_last_clipped"] = clipped ? "true" : "false";

                // —— 判定 ——
                mylib.utility_func.callbackdebuginfo(
                    $"[MicLevel] rms={rmsPct:F4}% peak={peakPct:F4}% dbfs={dbfs:F2}dBFS " +
                    $"limit=[{lowDbfs},{(double.IsNaN(highDbfs) ? double.PositiveInfinity : highDbfs)}]" +
                    (clipped ? " CLIPPED" : ""));

                // 削顶优先报 fail
                if (clipped)
                {
                    c = $"fail;clipped;dbfs={dbfs:F2};peak={peakPct:F4}";
                    return "fail";
                }

                bool lowOk = dbfs >= lowDbfs;
                bool highOk = double.IsNaN(highDbfs) || dbfs <= highDbfs;
                if (lowOk && highOk)
                {
                    c = double.IsInfinity(dbfs) ? "-999" : dbfs.ToString("F2");
                    return "pass";
                }
                else
                {
                    if (!lowOk && !highOk)
                        c = $"fail;dbfs={dbfs:F2};limit={lowDbfs}-{highDbfs}";
                    else if (!lowOk)
                        c = $"fail;dbfs={dbfs:F2}<min{lowDbfs}";
                    else
                        c = $"fail;dbfs={dbfs:F2}>max{highDbfs}";
                    return "fail";
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[MicLevel] measure_level error: {ex.Message}");
                c = "fail;exception;" + ex.Message;
                return "fail";
            }
        }

        // ──────────────────────────────────────────────
        // 核心实现: 启动 mic → 采集 durationMs → 选信号最强的一帧 → Goertzel 算 THD
        //           同时累积全部样本算 RMS/Peak/dBFS
        //
        // 设计要点:
        //   1. 后台采集，DataAvailable 回调里只做"环形缓冲 + 满帧选优 + 音量累积"，不做重活
        //   2. 信号最强 = 基波频率上的 Goertzel 功率最大，避免静音帧/噪声帧
        //   3. 采集结束后用最优帧算全部谐波，归一化得 THD%
        //   4. 削顶检测: 任一样本 |x| >= 0.99 即标记 clipped
        //   5. 音量指标（RMS/Peak/dBFS）用整段未加窗样本累积，反映真实音量
        // ──────────────────────────────────────────────
        private void MeasureThdCore(double targetFreq, int durationMs, int deviceIndex,
            int harmonicCount, int fftSize,
            out double thdPct, out bool clipped, out double fundFreqMeasured,
            out double rmsPct, out double peakPct, out double dbfs)
        {
            thdPct = 0;
            clipped = false;
            fundFreqMeasured = targetFreq;
            rmsPct = 0;
            peakPct = 0;
            dbfs = double.NegativeInfinity;

            int sampleRate = DefaultSampleRate;
            int blockAlign = 2; // 16bit mono

            // C# 不允许 lambda 捕获 ref/out 参数，用本地变量中转
            bool clippedLocal = false;

            // —— 预计算窗函数和归一化因子 ——
            // Goertzel 不要求 N 是 2 的幂，但保留 fftSize 命名以与 FFT 方法一致
            double[] window = BuildHannWindow(fftSize);
            const double windowCoherentGain = 0.5; // Hann
            double normFactor = (fftSize / 2.0) * windowCoherentGain;

            // —— 状态 ——
            var ring = new RingBuffer(fftSize);
            double[] bestWindowedFrame = null;
            double bestFundPower = -1;
            object frameLock = new object();

            // 声音强度累积器（全段样本，不加窗）
            double sumSq = 0;       // Σx²
            long sampleCount = 0;   // 样本数
            float maxPeak = 0;      // max(|x|)
            // 注: sumSq/maxPeak/sampleCount 只在音频回调线程修改（NAudio 单线程回调），无需 lock

            // —— 采集 ——
            using (var waveIn = new WaveInEvent
            {
                DeviceNumber = deviceIndex,
                WaveFormat = new WaveFormat(sampleRate, 16, 1),
                BufferMilliseconds = 50
            })
            {
                waveIn.DataAvailable += (s, e) =>
                {
                    for (int i = 0; i < e.BytesRecorded; i += blockAlign)
                    {
                        short s16 = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i]);
                        float v = s16 / 32768f;
                        ring.Push(v);

                        // —— 声音强度累积 ——
                        sumSq += (double)v * v;
                        sampleCount++;
                        float absV = Math.Abs(v);
                        if (absV > maxPeak) maxPeak = absV;

                        if (!ring.HasFullFrame) continue;

                        float[] frame = ring.GrabFrame();

                        // 削顶检测
                        for (int j = 0; j < frame.Length; j++)
                        {
                            if (Math.Abs(frame[j]) >= 0.99f) { clippedLocal = true; break; }
                        }

                        // 加窗
                        double[] windowed = new double[fftSize];
                        for (int j = 0; j < fftSize; j++)
                            windowed[j] = frame[j] * window[j];

                        // 只算基波功率（选帧用，速度快）
                        double fundPower = Math.Abs(GoertzelPower(windowed, targetFreq, sampleRate));

                        lock (frameLock)
                        {
                            if (fundPower > bestFundPower)
                            {
                                bestFundPower = fundPower;
                                bestWindowedFrame = windowed;
                            }
                        }
                    }
                };

                waveIn.StartRecording();

                // 等待采集时长（测试引擎主线程在此阻塞）
                Thread.Sleep(durationMs);

                waveIn.StopRecording();
                // 给 NAudio 一点时间完成 RecordingStopped 回调
                Thread.Sleep(120);
            }

            // —— 算声音强度 ——
            if (sampleCount > 0)
            {
                double rms = Math.Sqrt(sumSq / sampleCount);
                rmsPct = rms * 100.0;          // 0~100（占满量程的百分比）
                peakPct = maxPeak * 100.0;
                dbfs = rms > 0 ? 20.0 * Math.Log10(rms) : double.NegativeInfinity;
            }

            // —— 用信号最强的一帧算最终 THD ——
            if (bestWindowedFrame == null)
                throw new InvalidOperationException(
                    "no valid audio frame captured (mic not working / too quiet)");

            double fundMag = Math.Sqrt(bestFundPower);
            double fundAmp = fundMag / normFactor;

            double[] harmonics = new double[harmonicCount];
            for (int h = 0; h < harmonicCount; h++)
            {
                int order = h + 2;
                double hFreq = order * targetFreq;
                if (hFreq >= sampleRate / 2.0) { harmonics[h] = 0; continue; }
                double hPower = Math.Abs(GoertzelPower(bestWindowedFrame, hFreq, sampleRate));
                harmonics[h] = Math.Sqrt(hPower) / normFactor;
            }

            double sumSqH = 0;
            foreach (var amp in harmonics) sumSqH += amp * amp;
            thdPct = fundAmp > 0 ? Math.Sqrt(sumSqH) / fundAmp * 100.0 : 0;

            clipped = clippedLocal;
        }

        // ──────────────────────────────────────────────
        // 核心实现: 启动 mic → 采集 durationMs → 算 RMS/Peak/dBFS
        // 与 MeasureThdCore 不同: 不做 Goertzel/窗函数/选帧，只算整段音量指标，速度快
        // 适合"只想知道喇叭响不响"的场景
        // ──────────────────────────────────────────────
        private void MeasureLevelCore(int durationMs, int deviceIndex,
            out double rmsPct, out double peakPct, out double dbfs, out bool clipped)
        {
            rmsPct = 0;
            peakPct = 0;
            dbfs = double.NegativeInfinity;
            clipped = false;

            int sampleRate = DefaultSampleRate;
            int blockAlign = 2;

            // C# 不允许 lambda 捕获 ref/out 参数，用本地变量中转
            bool clippedLocal = false;

            double sumSq = 0;
            long sampleCount = 0;
            float maxPeak = 0;

            using (var waveIn = new WaveInEvent
            {
                DeviceNumber = deviceIndex,
                WaveFormat = new WaveFormat(sampleRate, 16, 1),
                BufferMilliseconds = 50
            })
            {
                waveIn.DataAvailable += (s, e) =>
                {
                    for (int i = 0; i < e.BytesRecorded; i += blockAlign)
                    {
                        short s16 = (short)((e.Buffer[i + 1] << 8) | e.Buffer[i]);
                        float v = s16 / 32768f;

                        sumSq += (double)v * v;
                        sampleCount++;
                        float absV = Math.Abs(v);
                        if (absV > maxPeak) maxPeak = absV;
                        if (absV >= 0.99f) clippedLocal = true;
                    }
                };

                waveIn.StartRecording();
                Thread.Sleep(durationMs);
                waveIn.StopRecording();
                Thread.Sleep(120);
            }

            if (sampleCount > 0)
            {
                double rms = Math.Sqrt(sumSq / sampleCount);
                rmsPct = rms * 100.0;
                peakPct = maxPeak * 100.0;
                dbfs = rms > 0 ? 20.0 * Math.Log10(rms) : double.NegativeInfinity;
            }

            clipped = clippedLocal;
        }

        // ──────────────────────────────────────────────
        // 辅助函数: 列出系统可用麦克风设备
        // d 可空。out c 输出 "0: DeviceName\n1: DeviceName..."
        // ──────────────────────────────────────────────
        private string list_mic_devices(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                int n = WaveInEvent.DeviceCount;
                var sb = new StringBuilder();
                for (int i = 0; i < n; i++)
                {
                    var cap = WaveInEvent.GetCapabilities(i);
                    sb.AppendLine(i + ": " + cap.ProductName);
                }
                if (n == 0)
                {
                    c = "no_mic_device";
                    mylib.utility_func.callbackdebuginfo("[MicThd] no microphone device found");
                    return "fail";
                }
                c = sb.ToString().TrimEnd();
                mylib.utility_func.callbackdebuginfo("[MicThd] devices:\n" + sb);
                return "pass";
            }
            catch (Exception ex)
            {
                c = "fail;" + ex.Message;
                return "fail";
            }
        }

        // ════════════════════════════════════════════
        //  Goertzel 算法: 在精确频率 freq 上算 DFT 功率
        //  优点: 不需要 FFT bin 对齐，不需要插值，已知基频时精度极高
        // ════════════════════════════════════════════
        private static double GoertzelPower(double[] samples, double freq, double sampleRate)
        {
            int N = samples.Length;
            double k = freq * N / sampleRate;
            double omega = 2.0 * Math.PI * k / N;
            double coeff = 2.0 * Math.Cos(omega);
            double sPrev = 0, sPrev2 = 0;
            for (int i = 0; i < N; i++)
            {
                double s = samples[i] + coeff * sPrev - sPrev2;
                sPrev2 = sPrev;
                sPrev = s;
            }
            return sPrev2 * sPrev2 + sPrev * sPrev - coeff * sPrev * sPrev2;
        }

        // Hann 窗: 旁瓣衰减好（-31dB），适合频谱泄漏抑制
        private static double[] BuildHannWindow(int n)
        {
            double[] w = new double[n];
            for (int i = 0; i < n; i++)
                w[i] = 0.5 - 0.5 * Math.Cos(2 * Math.PI * i / (n - 1));
            return w;
        }

        // ════════════════════════════════════════════
        //  环形缓冲: 累积样本，满 FftSize 即可取一帧
        // ════════════════════════════════════════════
        private class RingBuffer
        {
            private readonly float[] _buf;
            private int _head;
            private int _filled;
            private readonly int _frameSize;

            public bool HasFullFrame => _filled >= _frameSize;

            public RingBuffer(int frameSize)
            {
                _frameSize = frameSize;
                _buf = new float[frameSize];
                _head = 0;
                _filled = 0;
            }

            public void Push(float v)
            {
                _buf[_head] = v;
                _head = (_head + 1) % _buf.Length;
                if (_filled < _frameSize) _filled++;
            }

            // 取最新的 frameSize 个样本（老 → 新）
            public float[] GrabFrame()
            {
                float[] f = new float[_frameSize];
                int start = (_head - _frameSize + _buf.Length) % _buf.Length;
                for (int i = 0; i < _frameSize; i++)
                    f[i] = _buf[(start + i) % _buf.Length];
                return f;
            }
        }

        // ──────────────────────────────────────────────
        // IDefaultAction / 生命周期
        // ──────────────────────────────────────────────
        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void ClosePorts()
        {
            // 本模块不持有串口/设备，无需关闭
        }

        public void set_default_set()
        {
        }

        public void Dispose()
        {
            try
            {
                ClosePorts();
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
