using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    public class th9130_hipot_pj : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "";
        hipot_tonghui9130 th9130;

        /// <summary>
        /// 构造函数，自动检查 th9130 是否为空，为空则从 setup.ini 创建
        /// 创建后在后台线程加载测试步骤配置（不阻塞UI）
        /// </summary>
        public th9130_hipot_pj(testcase_dll _tc, ref hipot_tonghui9130 _th9130)
        {
            tc = _tc;

            // ── 检查并创建 TH9130 高压仪对象 ──────────────
            if (_th9130 == null)
            {
                try
                {
                    var ini = glob_ini_instance.getInstance().getSetupIniData;
                    string port = ini["setport"]["tonghu_th9130_port"];
                    int baud = int.Parse(ini["setport"]["tonghu_th9130_port_baudrate"]);
                    _th9130 = new hipot_tonghui9130(port, baud);
                    hipot_tonghui9130 dev = _th9130;  // 用本地变量中转，避免 lambda 内使用 ref 参数

                    // ── 后台线程加载配置，不阻塞窗体 ───────────
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            LoadConfigFromIni(dev, ini);
                        }
                        catch (Exception ex)
                        {
                            utility_func.callbackdebuginfo(
                                $"[th9130_hipot_pj] load config error: {ex.Message}");
                        }
                    });
                }
                catch (Exception ex)
                {
                    utility_func.callbackdebuginfo(
                        $"[th9130_hipot_pj] create th9130 failed: {ex.Message}");
                }
            }
            th9130 = _th9130;

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "th9130_";

            tc.funcs.Add(id + "start_test", start_test);
            tc.funcs.Add(id + "stop_test", stop_test);
            tc.funcs.Add(id + "reload_config", reload_config);
        }

        /// <summary>
        /// 从 setup.ini 的 [TH_9130_HIPOT] 段加载测试步骤配置
        /// 流程：清空所有步骤 → 按 step1~stepN 顺序重建
        /// </summary>
        private void LoadConfigFromIni(hipot_tonghui9130 dev, dynamic ini)
        {
            if (dev == null) return;

            if (ini["TH_9130_HIPOT"] == null)
            {
                utility_func.callbackdebuginfo("[th9130] [TH_9130_HIPOT] section not found in setup.ini");
                return;
            }

            var stepDict = new Dictionary<string, string>();
            foreach (var vpm in ini["TH_9130_HIPOT"])
            {
                stepDict[vpm.Key] = vpm.Value;
            }

            if (stepDict.Count == 0)
            {
                utility_func.callbackdebuginfo("[th9130] [TH_9130_HIPOT] is empty");
                return;
            }

            utility_func.callbackdebuginfo($"[th9130] Loading {stepDict.Count} steps from setup.ini...");

            bool ok = dev.LoadConfigFromDictionary(stepDict, "TH9130_PROJ");

            if (ok)
            {
                utility_func.callbackdebuginfo("[th9130] Config loaded successfully");
            }
            else
            {
                utility_func.callbackdebuginfo("[th9130] Config load FAILED");
            }
        }

        /// <summary>
        /// 启动全步骤安规测试并读取结果
        /// a: 保留（传空即可）
        /// b: 保留（传空即可）
        /// c: out — 返回详细结果字符串（各步骤 PASS/FAIL + 数值）
        /// d: 保留
        /// 返回 "pass"（所有步骤都PASS）或 "fail"（任一步骤FAIL或通信异常）
        /// </summary>
        private string start_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (th9130 == null) { c = "th9130_is_null"; return "fail"; }

                utility_func.callbackdebuginfo("[th9130] Start full test...");

                // 启动测试并读取结果（阻塞直到测试完成）
                string result = th9130.GetResult();

                if (result == "comm err")
                {
                    c = "comm_error";
                    return "fail";
                }

                c = result.Replace("\n"," ").Replace("\r"," ");

                // 解析结果，检查所有步骤是否都 PASS
                var stepResults = th9130.ParseResult(result);
                if (stepResults.Count == 0)
                {
                    utility_func.callbackdebuginfo("[th9130] No steps in result");
                    return "fail";
                }

                bool allPass = true;
                foreach (var kv in stepResults)
                {
                    utility_func.callbackdebuginfo(
                        $"[th9130] Step {kv.Key}: {kv.Value}");
                    if (kv.Value != "PASS")
                        allPass = false;
                }

                if (allPass)
                {
                    utility_func.callbackdebuginfo("[th9130] ALL PASS");
                    return "pass";
                }
                else
                {
                    utility_func.callbackdebuginfo("[th9130] HAS FAIL");
                    return "fail";
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[th9130] start_test error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 停止测试（紧急停止）
        /// a/b/c/d: 保留
        /// 返回 "pass" 或 "fail"
        /// </summary>
        private string stop_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (th9130 == null) { c = "th9130_is_null"; return "fail"; }

                int ret = th9130.StopTest();
                c = ret == 1 ? "pass" : "fail";

                utility_func.callbackdebuginfo($"[th9130] Stop test: {c}");
                return c;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[th9130] stop_test error: {ex.Message}");
                c = ex.Message;
                return "fail";
            }
        }

        /// <summary>
        /// 重新从 setup.ini 加载配置（运行时手动刷新）
        /// a/b/c/d: 保留
        /// 返回 "pass" 或 "fail"
        /// </summary>
        private string reload_config(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (th9130 == null) { c = "th9130_is_null"; return "fail"; }

                var ini = glob_ini_instance.getInstance().getSetupIniData;

                bool ok = false;
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        LoadConfigFromIni(th9130, ini);
                    }
                    catch { }
                });

                c = "reloading_in_background";
                return "pass";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[th9130] reload_config error: {ex.Message}");
                c = ex.Message;
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
