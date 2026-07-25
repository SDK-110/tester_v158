using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace test_antdui
{
    using TestFunctionDelegate = testapp.pointfun;
    public enum DebugMode { Normal, StepMode, RunToMode }
    public class TestEngine
    {
        private readonly List<TestItem> _testItems;
        private readonly Dictionary<string, TestFunctionDelegate> _functionLib = new Dictionary<string, TestFunctionDelegate>();
        private readonly List<TestFunctionDelegate> _initFunctions = new List<TestFunctionDelegate>();
        private readonly List<TestFunctionDelegate> _cleanupFunctions = new List<TestFunctionDelegate>();

        private CancellationTokenSource _cts;
        private bool _isRunning;
        private int _currentIndex = -1;
        private volatile DebugMode _debugMode = DebugMode.Normal;
        private volatile int _targetRow = -1;
        private TaskCompletionSource<bool> _pauseTcs;

        public event EventHandler<TestEngineEventArgs> TestStarted;
        public event EventHandler<TestEngineEventArgs> TestItemStarted;
        public event EventHandler<TestEngineEventArgs> TestItemCompleted;
        public event EventHandler<TestEngineEventArgs> TestCompleted;
        public event EventHandler<LogEventArgs> LogMessage;
        public event EventHandler<DebugPausedEventArgs> DebugPaused;

        public bool IsRunning => _isRunning;
        public int CurrentIndex => _currentIndex;
        public int TotalItems => _testItems.Count;
        public DebugMode CurrentDebugMode => _debugMode;
        public int TargetRow => _targetRow;
        public double Progress => _testItems.Count > 0 ? (double)(_currentIndex + 1) / _testItems.Count : 0;

        public TestEngine(List<TestItem> testItems)
        {
            _testItems = testItems ?? throw new ArgumentNullException(nameof(testItems));
        }

        public void RegisterFunction(string name, TestFunctionDelegate function)
        {
            if (string.IsNullOrWhiteSpace(name) || function == null)
                throw new ArgumentException("Function name and delegate cannot be null");
            _functionLib[name] = function;
        }

        public void RegisterInitFunction(TestFunctionDelegate function)
        {
            if (function != null)
                _initFunctions.Add(function);
        }

        public void RegisterCleanupFunction(TestFunctionDelegate function)
        {
            if (function != null)
                _cleanupFunctions.Add(function);
        }

        public void ClearFunctions()
        {
            _functionLib.Clear();
            _initFunctions.Clear();
            _cleanupFunctions.Clear();
        }

        public void Reset()
        {
            _currentIndex = -1;
            _debugMode = DebugMode.Normal;
            _targetRow = -1;
            _pauseTcs = null;
            foreach (var item in _testItems)
            {
                item.Reset();
            }
        }

        public async Task<bool> RunAsync()
        {
            if (_isRunning) 
            {
                OnLogMessage("测试引擎已在运行中");
                return false;
            }

            _isRunning = true;
            _cts = new CancellationTokenSource();

            try
            {
                OnLogMessage($"========== 测试开始 ==========");
                OnLogMessage($"共有 {_testItems.Count} 个测试项");
                TestStarted?.Invoke(this, new TestEngineEventArgs(null, 0, 0));

                bool overallResult = true;
                bool stopOnFail = TestConfigManager.Instance.StopOnFail == 1;
                OnLogMessage($"[配置] StopOnFail = {TestConfigManager.Instance.StopOnFail} ({(stopOnFail ? "已启用" : "已禁用")})");
                int jump_cout = 1;
                for (int i = 0; i < _testItems.Count; i++)
                {
                    if (_cts.Token.IsCancellationRequested) break;

                    var item = _testItems[i];
                    _currentIndex = i;
                    item.Reset();

                    // 调试暂停检查（在测试项执行前）
                    if (!_cts.Token.IsCancellationRequested && _debugMode != DebugMode.Normal)
                    {
                        // RunToMode: 到达目标行时暂停，然后切换为 StepMode
                        if (_debugMode == DebugMode.RunToMode && i >= _targetRow)
                        {
                            _debugMode = DebugMode.StepMode;
                            _pauseTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                            OnDebugPaused(i, item);
                            OnLogMessage($"[调试] 已到达目标行 #{_targetRow + 1}，暂停在 [{item.Id}] {item.Name}");
                            await _pauseTcs.Task;
                            OnLogMessage($"[调试] 继续执行...");
                        }
                        // StepMode: 在每项执行前暂停
                        else if (_debugMode == DebugMode.StepMode)
                        {
                            _pauseTcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                            OnDebugPaused(i, item);
                            OnLogMessage($"[调试] 单步暂停在 [{item.Id}] {item.Name}");
                            await _pauseTcs.Task;
                            OnLogMessage($"[调试] 继续执行...");
                        }
                    }

                    OnLogMessage($"[{item.Id}] {item.Name} - 开始");

                    var sw = Stopwatch.StartNew();
                    TestItemStarted?.Invoke(this, new TestEngineEventArgs(item, i, _testItems.Count));

                    string result = await ExecuteTestItemAsync(item, token: _cts.Token);
                    sw.Stop();
                    item.Duration = (int)sw.ElapsedMilliseconds;

                    bool itemPass = result.ToUpper().Contains("PASS") || result.ToUpper().Contains("SKIP");
                    item.State = itemPass ? TestState.Pass : TestState.Fail;

                    string displayValue = item.ResultMessage;
                    if (string.IsNullOrEmpty(displayValue))
                        displayValue = item.State == TestState.Pass ? "PASS" : "FAIL";
                    
                    OnLogMessage($"[{item.Id}] {item.Name} = {displayValue} (实测:{item.MeasuredValue}, 限值:{item.LowLimit}~{item.HighLimit}, {item.Duration}ms)");
                    TestItemCompleted?.Invoke(this, new TestEngineEventArgs(item, i, _testItems.Count));
                    if (!itemPass && i != int.Parse(item.Jumper_Num) && jump_cout-->0) {
                        
                        i = int.Parse(item.Jumper_Num)-2; continue; 
                    
                    
                    }
                    if (!itemPass) 
                    {
                        jump_cout = 1;
                        overallResult = false;
                        OnLogMessage($"[结果] {item.Name} = FAIL (实测值:{item.MeasuredValue})");
                        if (stopOnFail)
                        {
                            OnLogMessage($"[配置] StopOnFail已启用，停止测试...");
                            for (int j = i + 1; j < _testItems.Count; j++)
                            {
                                _testItems[j].State = TestState.Pending;
                            }
                            break;
                        }
                        OnLogMessage($"[配置] StopOnFail未启用，继续测试下一项");
                    }
                }

                OnLogMessage($"========== 测试完成 ==========");
                TestCompleted?.Invoke(this, new TestEngineEventArgs(null, 0, _testItems.Count));
                return overallResult;
            }
            catch (OperationCanceledException)
            {
                OnLogMessage("测试已取消");
                return false;
            }
            catch (Exception ex)
            {
                OnLogMessage($"测试异常: {ex.Message}");
                return false;
            }
            finally
            {
                _isRunning = false;
                // 调试模式复位
                _debugMode = DebugMode.Normal;
                _targetRow = -1;
                _pauseTcs = null;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private Task<string> ExecuteTestItemAsync(TestItem item, CancellationToken token)
        {
            return Task.Run(() =>
            {
                if (item.IsSkipped)
                {
                    item.State = TestState.Skipped;
                    item.ResultMessage = "已跳过";
                    return "SKIP";
                }

                // Remove @ and !! from function name before lookup (same as old engine: test_lib_string.Replace("@", "").Replace("!!",""))
                // @ presence marks special logger items (is_teshu_logger in old engine)
                string funcKey = (item.FunctionName ?? "").Replace("@", "").Replace("!!", "");

                if (!_functionLib.ContainsKey(funcKey))
                {
                    OnLogMessage($"  错误: 函数 [{item.FunctionName}] 未注册!");
                    item.ResultMessage = $"函数未注册: {item.FunctionName}";
                    return "FAIL: Function not found";
                }

                int attempts = 0;
                string lastResult = "FAIL";
                string lastMessage = "";
                string rsu = "";
                while (attempts < item.MaxRetry)
                {
                    attempts++;
                    item.StartTime = DateTime.Now;
                   
                    try
                    {
                        TestFunctionDelegate func = _functionLib[funcKey];
                       
                        lastResult = func(item.HighLimit, item.LowLimit,out rsu , item.Parameter);

                        item.EndTime = DateTime.Now;
                        item.MeasuredValue = rsu;
                        item.ResultMessage = lastResult;

                        if (lastResult.ToUpper().Contains("PASS"))
                        {
                            return lastResult.ToUpper();
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        lastResult = $"FAIL: {ex.Message}";
                        lastMessage = lastResult;
                        item.ResultMessage = lastResult;
                        item.MeasuredValue = "Error";
                        OnLogMessage($"  异常: {ex.Message}");
                    }

                    if (attempts < item.MaxRetry)
                    {
                        OnLogMessage($"  重试 {attempts}/{item.MaxRetry}...");
                        Thread.Sleep(100);
                    }
                }

                item.ResultMessage = lastResult;
                return lastResult;
            }, token);
        }

        private string ExtractValue(string result)
        {
            if (string.IsNullOrEmpty(result)) return "";
            
            int commaIndex = result.IndexOf(',');
            if (commaIndex >= 0 && commaIndex < result.Length - 1)
            {
                string afterComma = result.Substring(commaIndex + 1).Trim();
                var match = System.Text.RegularExpressions.Regex.Match(afterComma, @"[-+]?\d*\.?\d+");
                if (match.Success)
                    return match.Value;
                return afterComma;
            }
            
            var regexMatch = System.Text.RegularExpressions.Regex.Match(result, @"[-+]?\d*\.?\d+");
            if (regexMatch.Success)
                return regexMatch.Value;
            
            return result.Trim();
        }

        public void Cancel()
        {
            _cts?.Cancel();
            _pauseTcs?.TrySetResult(false);
        }

        /// <summary>
        /// 放行一步，引擎继续保持在 StepMode
        /// </summary>
        public void StepNext()
        {
            _debugMode = DebugMode.StepMode;
            _pauseTcs?.TrySetResult(true);
        }

        /// <summary>
        /// 设置目标行并放行，引擎会一直运行到目标行后暂停
        /// 如果目标行已过当前位置，拒绝设置
        /// </summary>
        public void RunTo(int targetRow)
        {
            OnLogMessage($"[调试] RunTo({targetRow + 1}) 被调用, 当前索引={_currentIndex}, 引擎运行={_isRunning}");
            if (targetRow <= _currentIndex)
            {
                OnLogMessage($"[调试] 目标行 #{targetRow + 1} 已过当前位置 (#{_currentIndex + 1})，无法跳回");
                return;
            }
            _targetRow = targetRow;
            _debugMode = DebugMode.RunToMode;
            OnLogMessage($"[调试] 已设置目标行 #{targetRow + 1}, 模式=RunToMode");
            _pauseTcs?.TrySetResult(true);
        }

        /// <summary>
        /// 恢复连续执行模式（Normal），引擎不再暂停
        /// </summary>
        public void Continue()
        {
            _debugMode = DebugMode.Normal;
            _targetRow = -1;
            _pauseTcs?.TrySetResult(true);
        }

        public async Task<bool> RunInitAsync()
        {
            OnLogMessage("执行初始化函数...");
            foreach (var init in _initFunctions)
            {
                try
                {
                    init("", "",out _, "");
                }
                catch (Exception ex)
                {
                    OnLogMessage($"初始化异常: {ex.Message}");
                    return false;
                }
            }
            OnLogMessage("初始化完成");
            return true;
        }

        public async Task RunCleanupAsync()
        {
            OnLogMessage("执行清理函数...");
            foreach (var cleanup in _cleanupFunctions)
            {
                try
                {
                    cleanup("", "", out _,"");
                }
                catch (Exception ex)
                {
                    OnLogMessage($"清理异常: {ex.Message}");
                }
            }
            OnLogMessage("清理完成");
        }

        protected void OnLogMessage(string message)
        {
            LogMessage?.Invoke(this, new LogEventArgs(message, DateTime.Now));
        }

        protected void OnDebugPaused(int index, TestItem item)
        {
            DebugPaused?.Invoke(this, new DebugPausedEventArgs(index, item, _debugMode, _targetRow));
        }
    }

    public class TestEngineEventArgs : EventArgs
    {
        public TestItem Item { get; }
        public int Index { get; }
        public int Total { get; }
        public int Extra { get; }

        public TestEngineEventArgs(TestItem item, int index, int total, int extra = 0)
        {
            Item = item;
            Index = index;
            Total = total;
            Extra = extra;
        }
    }

    public class LogEventArgs : EventArgs
    {
        public string Message { get; }
        public DateTime Timestamp { get; }

        public LogEventArgs(string message, DateTime timestamp)
        {
            Message = message;
            Timestamp = timestamp;
        }
    }

    public class DebugPausedEventArgs : EventArgs
    {
        public int CurrentIndex { get; }
        public TestItem CurrentItem { get; }
        public DebugMode Mode { get; }
        public int TargetRow { get; }

        public DebugPausedEventArgs(int index, TestItem item, DebugMode mode, int targetRow)
        {
            CurrentIndex = index;
            CurrentItem = item;
            Mode = mode;
            TargetRow = targetRow;
        }
    }
}
