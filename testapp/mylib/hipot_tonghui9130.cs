using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace testapp
{
    /// <summary>
    /// 同惠 TH9130 多功能安规综合分析仪驱动
    /// 支持 SCPI 命令集，通过 RS232/USB 控制
    /// 包含：OSC开短路检测、IR绝缘电阻、AC/DC耐压、GB接地电阻等测试
    /// </summary>
  public  class hipot_tonghui9130 : SerialPort
    {
        public hipot_tonghui9130(string port, int baudrate = 9600) : base(port)
        {
            base.BaudRate = baudrate;
            base.Parity = Parity.None;
            base.StopBits = StopBits.One;
            base.DataBits = 8;
            base.Handshake = Handshake.None;
            base.RtsEnable = true;
            base.NewLine = "\n";
            base.WriteTimeout = 3000;
            base.ReadTimeout = 120000;  // 耐压测试可能长达999秒，设大一点
            base.Open();
        }

        /// <summary>
        /// 发送命令并读取一行响应
        /// </summary>
        private string Query(string cmd)
        {
            this.DiscardInBuffer();
            this.WriteLine(cmd);
            return this.ReadLine().Trim();
        }

        /// <summary>
        /// 发送命令（无返回值）
        /// </summary>
        private void SendCmd(string cmd)
        {
            this.DiscardInBuffer();
            this.WriteLine(cmd);
        }

        /// <summary>
        /// 查询当前测试步骤数量
        /// </summary>
        public int GetStepCount()
        {
            try
            {
                string r = Query("FUNC:SOUR:STEP ?");
                int count;
                if (int.TryParse(r, out count))
                    return count;
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 删除指定步骤
        /// </summary>
        public void DeleteStep(int stepNum)
        {
            SendCmd($"FUNC:SOUR:STEP_{stepNum}:DEL");
        }

        /// <summary>
        /// 在指定位置插入新步骤（空步骤，之后需设置模式和参数）
        /// </summary>
        public void InsertStep(int stepNum)
        {
            SendCmd($"FUNC:SOUR:STEP_{stepNum}:INS");
        }

        /// <summary>
        /// 清空所有步骤
        /// </summary>
        /// <returns>成功返回true</returns>
        public bool ClearAllSteps()
        {
            try
            {
                int count = GetStepCount();
                if (count <= 0) return true;

                // 从后往前删除，避免序号变化
                for (int i = count; i >= 1; i--)
                {
                    DeleteStep(i);
                    System.Threading.Thread.Sleep(50);
                }

                int remain = GetStepCount();
                return remain == 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从配置字典加载所有步骤
        /// 流程：先清空所有步骤 → 按顺序插入并配置每一步
        /// </summary>
        /// <param name="stepConfigs">键为 step1, step2...，值为分号分隔的参数串</param>
        /// <param name="saveFileName">配置加载完后保存到仪器内部的文件名（不含.sta后缀），为空则不保存</param>
        /// <returns>成功返回true</returns>
        public bool LoadConfigFromDictionary(Dictionary<string, string> stepConfigs, string saveFileName = "")
        {
            try
            {
                if (stepConfigs == null || stepConfigs.Count == 0)
                    return false;

                // 1. 清空所有步骤
                if (!ClearAllSteps())
                {
                    mylib.utility_func.callbackdebuginfo("[th9130] ClearAllSteps failed");
                    return false;
                }

                mylib.utility_func.callbackdebuginfo($"[th9130] All steps cleared, loading {stepConfigs.Count} steps...");

                // 2. 按步骤号排序
                var sortedSteps = new SortedDictionary<int, string>();
                foreach (var kv in stepConfigs)
                {
                    string key = kv.Key.ToLower().Trim();
                    if (key.StartsWith("step"))
                    {
                        int stepNum;
                        if (int.TryParse(key.Substring(4), out stepNum))
                        {
                            sortedSteps[stepNum] = kv.Value;
                        }
                    }
                }

                // 3. 依次插入并配置每一步
                foreach (var kv in sortedSteps)
                {
                    int stepNum = kv.Key;
                    string paramStr = kv.Value;

                    // 插入新步骤
                    InsertStep(stepNum);
                    System.Threading.Thread.Sleep(50);

                    // 解析分号分隔的参数，逐条发送
                    string[] paramPairs = paramStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string param in paramPairs)
                    {
                        string p = param.Trim();
                        if (string.IsNullOrEmpty(p)) continue;

                        // 构造完整命令: FUNC:SOUR:STEP N:XXX YYY
                        string fullCmd = $"FUNC:SOUR:STEP {stepNum}:{p}";
                        SendCmd(fullCmd);
                        System.Threading.Thread.Sleep(20);
                    }

                    mylib.utility_func.callbackdebuginfo($"[th9130] Step {stepNum} configured: {paramStr}");
                }

                // 4. 保存配置到仪器内部（可选）
                if (!string.IsNullOrEmpty(saveFileName))
                {
                    try
                    {
                        string safeName = saveFileName.Trim();
                        if (!safeName.EndsWith(".sta", StringComparison.OrdinalIgnoreCase))
                            safeName += ".sta";
                        SendCmd($"MMEM:SAVE {safeName}");
                        mylib.utility_func.callbackdebuginfo($"[th9130] Config saved to {safeName}");
                    }
                    catch (Exception ex)
                    {
                        mylib.utility_func.callbackdebuginfo($"[th9130] Save config warning: {ex.Message}");
                    }
                }

                mylib.utility_func.callbackdebuginfo($"[th9130] Config load complete, total {sortedSteps.Count} steps");
                return true;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[th9130] LoadConfigFromDictionary error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 启动全部步骤测试
        /// </summary>
        public int StartTest()
        {
            try
            {
                this.DiscardInBuffer();
                this.WriteLine("FUNC:START");
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 启动指定单步测试
        /// </summary>
        public int StartStep(int stepNum)
        {
            try
            {
                this.DiscardInBuffer();
                this.WriteLine($"FUNC:START {stepNum}");
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 停止测试
        /// </summary>
        public int StopTest()
        {
            try
            {
                this.DiscardInBuffer();
                this.WriteLine("*STOP");
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// 获取测试结果（启动测试后调用，会阻塞直到测试完成）
        /// 返回格式：原始 FETCh? 结果字符串
        /// 如: "STEP 1:OSC,1.000e-8,PASS;STEP 2:IR,0.500,1.000e+7,PASS;..."
        /// </summary>
        public string GetResult()
        {
            try
            {
                this.DiscardInBuffer();
                this.WriteLine("FETCh?");
                string result = this.ReadLine();
                mylib.utility_func.callbackdebuginfo($"[th9130] result: {result}");
                return result;
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[th9130] GetResult error: {ex.Message}");
                return "comm err";
            }
        }

        /// <summary>
        /// 解析测试结果，返回每步的 PASS/FAIL 状态
        /// </summary>
        /// <param name="result">FETCh? 返回的原始字符串</param>
        /// <returns>字典：key=步骤号，value=PASS/FAIL</returns>
        public Dictionary<int, string> ParseResult(string result)
        {
            var dict = new Dictionary<int, string>();
            if (string.IsNullOrEmpty(result) || result == "comm err")
                return dict;

            // 按分号分割各步骤
            string[] steps = result.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string step in steps)
            {
                string s = step.Trim();
                // 格式: "STEP 1:OSC,1.000e-8,PASS"
                int colonIdx = s.IndexOf(':');
                if (colonIdx <= 0) continue;

                string stepPart = s.Substring(0, colonIdx).Trim();
                string rest = s.Substring(colonIdx + 1).Trim();

                // 提取步骤号
                if (stepPart.StartsWith("STEP", StringComparison.OrdinalIgnoreCase))
                {
                    string numStr = stepPart.Substring(4).Trim();
                    int stepNum;
                    if (int.TryParse(numStr, out stepNum))
                    {
                        // 找最后一个逗号后的 PASS/FAIL
                        int lastComma = rest.LastIndexOf(',');
                        if (lastComma >= 0 && lastComma < rest.Length - 1)
                        {
                            string status = rest.Substring(lastComma + 1).Trim().ToUpper();
                            dict[stepNum] = status;
                        }
                    }
                }
            }

            return dict;
        }

        /// <summary>
        /// 查询仪器型号和版本
        /// </summary>
        public string GetIDN()
        {
            try
            {
                return Query("*IDN?");
            }
            catch
            {
                return "";
            }
        }

        ~hipot_tonghui9130()
        {
            if (this.IsOpen)
            {
                try { this.Close(); } catch { }
            }
        }
    }
}
