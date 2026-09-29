using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using testapp.mylib;

namespace testapp.test_cases
{
    /// <summary>
    /// ConPTY 会话引擎（静态类）。
    ///
    /// 用途：驱动"自己读控制台"的交互式命令行程序（msys2/git-bash、python -i、
    /// cmd 外壳、getpass 密码输入、choice 菜单、各种 TUI）。
    /// 这类程序调的是 Win32 控制台 API（ReadConsole / ReadConsoleInput），
    /// 不读 stdin 句柄，所以 RedirectStandardInput/Output 对它无效。
    /// ConPTY 给子进程一个真正的控制台，所有控制台 API 照常工作。
    ///
    /// 要求：Windows 10 1809+。
    /// </summary>
    public static class ConPty
    {
        #region API_WIN
        internal const uint EXTENDED_STARTUPINFO_PRESENT = 0x00080000;
        internal static readonly IntPtr PROC_THREAD_ATTRIBUTE_PSEUDOCONSOLE = (IntPtr)0x00020016;

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD { public short X, Y; }

        [StructLayout(LayoutKind.Sequential)]
        internal struct SECURITY_ATTRIBUTES { public int nLength; public IntPtr lpSecurityDescriptor; public int bInheritHandle; }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct STARTUPINFOW
        {
            public uint cb;
            public string lpReserved, lpDesktop, lpTitle;
            public uint dwX, dwY, dwXSize, dwYSize, dwXCountChars, dwYCountChars, dwFillAttribute, dwFlags;
            public short wShowWindow, cbReserved2;
            public IntPtr lpReserved2, hStdInput, hStdOutput, hStdError;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct STARTUPINFOEX { public STARTUPINFOW StartupInfo; public IntPtr lpAttributeList; }

        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION { public IntPtr hProcess, hThread; public uint dwProcessId, dwThreadId; }

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CreatePipe(out IntPtr hRead, out IntPtr hWrite, ref SECURITY_ATTRIBUTES sa, uint nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int CreatePseudoConsole(COORD size, IntPtr hInput, IntPtr hOutput, uint dwFlags, out IntPtr phPC);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int ResizePseudoConsole(IntPtr hPC, COORD size);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void ClosePseudoConsole(IntPtr hPC);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool CloseHandle(IntPtr h);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool InitializeProcThreadAttributeList(IntPtr list, int count, int flags, ref IntPtr size);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool UpdateProcThreadAttribute(IntPtr list, uint flags, IntPtr attribute, IntPtr value,
            IntPtr size, IntPtr prev, IntPtr retSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void DeleteProcThreadAttributeList(IntPtr list);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool CreateProcessW(string app, StringBuilder cmdLine, IntPtr procAttr, IntPtr threadAttr,
            bool inheritHandles, uint flags, IntPtr env, string cwd, ref STARTUPINFOEX si, out PROCESS_INFORMATION pi);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern uint WaitForSingleObject(IntPtr h, uint ms);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetExitCodeProcess(IntPtr h, out uint code);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool TerminateProcess(IntPtr h, uint exitCode);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetStdHandle(int nStdHandle, IntPtr h);
        #endregion
        /// <summary>启动一个会话。commandLine 例："cmd.exe /k"、"python -i"。</summary>
        public static ConPtySession Start(string commandLine, short cols = 120, short rows = 50, string cwd = null)
        {
            return new ConPtySession(commandLine, cols, rows, cwd);
        }
    }

    /// <summary>一个 ConPTY 会话：像坐在键盘前一样发命令、实时收屏幕输出。</summary>
    public sealed class ConPtySession : IDisposable
    {
        IntPtr _hPC, _hProcess, _hThread;
        readonly IntPtr[] _ptyEnds = new IntPtr[2];
        readonly FileStream _input;
        readonly StringBuilder _raw = new StringBuilder();     // 原始输出（含 VT 序列），累计
        readonly StringBuilder _clean = new StringBuilder();   // 剥掉 VT 后的干净文本，累计
        readonly StringBuilder _frame = new StringBuilder();   // 当前"一屏"的干净文本（清屏重绘时重置）
        readonly ManualResetEventSlim _processExited = new ManualResetEventSlim(false);
        readonly object _gate = new object();
        int _scanPos;
        int _lineStart;
        long _version;
        bool _disposed;

        /// <summary>每输出一整行触发一次（已剥 VT 序列）。</summary>
        public event Action<string> OutputLine;

        /// <summary>每收到一块新内容就触发（含还没换行的提示符，如 "Your choice: "）。</summary>
        public event Action<string> OutputChunk;

        public int Pid { get; private set; }
        public int? ExitCode { get; private set; }
        public Exception LastError { get; private set; }

        static readonly Regex Ansi = new Regex(
            @"\x1B\[[0-9;?]*[ -/]*[@-~]" +
            @"|\x1B\][^\x07\x1B]*(?:\x07|\x1B\\)" +
            @"|\x1B[@-Z\\-_]",
            RegexOptions.Compiled);

        /// <summary>累计纯文本。做条件判断用这个。</summary>
        public string CleanText { get { lock (_gate) return _clean.ToString(); } }

        /// <summary>当前"一屏"的纯文本，等价于人此刻在屏幕上看到的。</summary>
        public string ScreenText { get { lock (_gate) return _frame.ToString(); } }

        /// <summary>内容版本号，变了说明有新输出。</summary>
        public long Version { get { return Interlocked.Read(ref _version); } }

        internal ConPtySession(string commandLine, short cols, short rows, string cwd)
        {
            var sa = new ConPty.SECURITY_ATTRIBUTES();
            sa.nLength = Marshal.SizeOf(typeof(ConPty.SECURITY_ATTRIBUTES));
            sa.bInheritHandle = 0;

            IntPtr inRead, inWrite, outRead, outWrite;
            if (!ConPty.CreatePipe(out inRead, out inWrite, ref sa, 0)) throw Win32("CreatePipe(in)");
            if (!ConPty.CreatePipe(out outRead, out outWrite, ref sa, 0)) throw Win32("CreatePipe(out)");

            var size = new ConPty.COORD();
            size.X = cols;
            size.Y = rows;
            int hr = ConPty.CreatePseudoConsole(size, inRead, outWrite, 0, out _hPC);
            if (hr != 0) throw new Exception("CreatePseudoConsole 失败: 0x" + hr.ToString("X8"));

            IntPtr attrList = IntPtr.Zero;
            try
            {
                IntPtr need = IntPtr.Zero;
                ConPty.InitializeProcThreadAttributeList(IntPtr.Zero, 1, 0, ref need);
                attrList = Marshal.AllocHGlobal(need);
                if (!ConPty.InitializeProcThreadAttributeList(attrList, 1, 0, ref need))
                    throw Win32("InitializeProcThreadAttributeList");
                if (!ConPty.UpdateProcThreadAttribute(attrList, 0, ConPty.PROC_THREAD_ATTRIBUTE_PSEUDOCONSOLE, _hPC,
                        (IntPtr)IntPtr.Size, IntPtr.Zero, IntPtr.Zero))
                    throw Win32("UpdateProcThreadAttribute");

                var si = new ConPty.STARTUPINFOEX();
                si.StartupInfo.cb = (uint)Marshal.SizeOf(typeof(ConPty.STARTUPINFOEX));
                si.lpAttributeList = attrList;

                // 关键：把本进程的标准句柄临时清空。
                // bInheritHandles=false 还不够 —— CreateProcess 会把父进程的标准句柄复制给子进程。
                // 如果父进程 stdout 被重定向（本程序是 WinExe，stdout 常常无效/被重定向），
                // 子进程的输出就会跑到那个句柄去，ConPTY 的屏幕上一片空白、管道永远读不到数据。
                IntPtr sIn = ConPty.GetStdHandle(-10);
                IntPtr sOut = ConPty.GetStdHandle(-11);
                IntPtr sErr = ConPty.GetStdHandle(-12);
                ConPty.SetStdHandle(-10, IntPtr.Zero);
                ConPty.SetStdHandle(-11, IntPtr.Zero);
                ConPty.SetStdHandle(-12, IntPtr.Zero);
                ConPty.PROCESS_INFORMATION pi;
                bool ok;
                try
                {
                    ok = ConPty.CreateProcessW(null, new StringBuilder(commandLine), IntPtr.Zero, IntPtr.Zero,
                        false, ConPty.EXTENDED_STARTUPINFO_PRESENT, IntPtr.Zero, cwd, ref si, out pi);
                }
                finally
                {
                    ConPty.SetStdHandle(-10, sIn);
                    ConPty.SetStdHandle(-11, sOut);
                    ConPty.SetStdHandle(-12, sErr);
                }
                if (!ok) throw Win32("CreateProcessW('" + commandLine + "')");

                _hProcess = pi.hProcess;
                _hThread = pi.hThread;
                Pid = (int)pi.dwProcessId;
            }
            finally
            {
                if (attrList != IntPtr.Zero)
                {
                    ConPty.DeleteProcThreadAttributeList(attrList);
                    Marshal.FreeHGlobal(attrList);
                }
                // Win10 上这两个"PTY 端"副本必须保留到会话结束，
                // 提前 CloseHandle 会让伪控制台停止渲染（表现为读端永远没数据）。
                _ptyEnds[0] = inRead;
                _ptyEnds[1] = outWrite;
            }

            _input = new FileStream(new SafeFileHandle(inWrite, true), FileAccess.Write);
            var output = new FileStream(new SafeFileHandle(outRead, true), FileAccess.Read);

            var reader = new Thread(delegate ()
            {
                try
                {
                    using (var sr = new StreamReader(output, new UTF8Encoding(false), false, 4096))
                    {
                        var buf = new char[4096];
                        int n;
                        while ((n = sr.Read(buf, 0, buf.Length)) > 0)
                            OnChunk(new string(buf, 0, n));
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is IOException) && !(ex is ObjectDisposedException)) LastError = ex;
                }
            });
            reader.IsBackground = true;
            reader.Name = "conpty-reader";
            reader.Start();

            // 看门狗：进程一退出就置位。不能只靠管道 EOF —— 我们持有 PTY 输出端副本，
            // 子进程退出后管道不会立刻 EOF。
            var watchdog = new Thread(delegate ()
            {
                ConPty.WaitForSingleObject(_hProcess, 0xFFFFFFFF);
                uint code;
                ConPty.GetExitCodeProcess(_hProcess, out code);
                ExitCode = (int)code;
                _processExited.Set();
            });
            watchdog.IsBackground = true;
            watchdog.Name = "conpty-watchdog";
            watchdog.Start();
        }

        static Exception Win32(string op)
        {
            return new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error(), op + " 失败");
        }

        void OnChunk(string chunk)
        {
            var lines = new List<string>();
            string delta = null;
            lock (_gate)
            {
                _raw.Append(chunk);

                // ConPTY 重绘会发 \x1b[2J（清屏），之后的内容是新的一屏。
                // 不处理的话同一屏内容会被反复抛成事件。
                int clear = chunk.LastIndexOf("\x1b[2J", StringComparison.Ordinal);
                string tail = clear >= 0 ? chunk.Substring(clear + 4) : chunk;
                string clean = Ansi.Replace(tail, "");
                if (clear >= 0) { _frame.Length = 0; _scanPos = 0; _lineStart = 0; }

                _clean.Append(clean);
                _frame.Append(clean);
                delta = clean;
                Interlocked.Increment(ref _version);

                for (int i = _scanPos; i < _frame.Length; i++)
                {
                    if (_frame[i] != '\n') continue;
                    string line = _frame.ToString(_lineStart, i - _lineStart).TrimEnd('\r');
                    // 终端语义：\r 是回到行首覆盖，取最后一次覆盖后的内容
                    int cr = line.LastIndexOf('\r');
                    if (cr >= 0) line = line.Substring(cr + 1);
                    _lineStart = i + 1;
                    if (line.Trim().Length > 0) lines.Add(line);
                }
                _scanPos = _frame.Length;
            }
            var onChunk = OutputChunk;
            if (onChunk != null && !string.IsNullOrEmpty(delta)) onChunk(delta);
            if (OutputLine != null)
                foreach (var l in lines) OutputLine(l);
        }

        /// <summary>
        /// 条件触发器：后台监控输出，condition 一旦成立就执行 action，然后自动注销
        /// （每注册一次只触发一次）。条件基于整段文本，所以"没有换行的提示符"也能立刻命中。
        /// 回调在后台线程执行，别在里面直接碰 WinForms 控件。
        /// </summary>
        public IDisposable When(Func<string, bool> condition, Action<ConPtySession> action)
        {
            var reg = new Subscription();
            var t = new Thread(delegate ()
            {
                long seen = -1;
                while (!_disposed && !reg.Disposed)
                {
                    Thread.Sleep(20);
                    long v = Version;
                    if (v == seen) continue;      // 没新内容就不重跑条件，避免空转
                    seen = v;
                    if (condition(CleanText))
                    {
                        try { action(this); }
                        catch (Exception ex) { LastError = ex; }
                        reg.Dispose();            // 只触发一次
                        return;
                    }
                }
            });
            t.IsBackground = true;
            t.Name = "conpty-when";
            t.Start();
            return reg;
        }

        sealed class Subscription : IDisposable
        {
            public volatile bool Disposed;
            public void Dispose() { Disposed = true; }
        }

        /// <summary>发一行并回车。控制台的"回车键"是 \r。</summary>
        public void SendLine(string line)
        {
            Write((line ?? "") + "\r");
        }

        /// <summary>发裸按键，不追加回车（choice / getwch 这类单键读取用）。</summary>
        public void SendRaw(string keys)
        {
            Write(keys ?? "");
        }

        void Write(string s)
        {
            try
            {
                byte[] b = Encoding.UTF8.GetBytes(s);
                _input.Write(b, 0, b.Length);
                _input.Flush();
            }
            catch (Exception ex) { LastError = ex; }
        }

        /// <summary>等输出里出现指定文本。</summary>
        public bool WaitFor(string text, int timeoutMs = 10000)
        {
            return WaitFor(delegate (string t) { return t.IndexOf(text, StringComparison.Ordinal) >= 0; }, timeoutMs);
        }

        /// <summary>等条件成立（进程退出后立即再判一次）。onTick 每次轮询调用一次，用来刷实时日志。</summary>
        public bool WaitFor(Func<string, bool> predicate, int timeoutMs, Action onTick = null)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.ElapsedMilliseconds < timeoutMs)
            {
                if (predicate(CleanText)) return true;
                if (_processExited.IsSet) return predicate(CleanText);
                if (onTick != null) onTick();
                Thread.Sleep(25);
            }
            return predicate(CleanText);
        }

        public bool WaitForExit(int timeoutMs = 15000) { return _processExited.Wait(timeoutMs); }

        public void Resize(short cols, short rows)
        {
            var c = new ConPty.COORD(); c.X = cols; c.Y = rows;
            ConPty.ResizePseudoConsole(_hPC, c);
        }

        /// <summary>强制结束子进程（退出码默认 -1，可与正常退出区分）。</summary>
        public void Kill(int exitCode = -1)
        {
            if (_hProcess != IntPtr.Zero) ConPty.TerminateProcess(_hProcess, (uint)exitCode);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            try { _input.Dispose(); } catch { }
            if (_hPC != IntPtr.Zero) ConPty.ClosePseudoConsole(_hPC);
            _processExited.Wait(3000);
            if (_hProcess != IntPtr.Zero) ConPty.CloseHandle(_hProcess);
            if (_hThread != IntPtr.Zero) ConPty.CloseHandle(_hThread);
            foreach (var h in _ptyEnds) if (h != IntPtr.Zero) ConPty.CloseHandle(h);
            _processExited.Dispose();
        }
    }

    /// <summary>
    /// 交互式命令行测试用例。注册函数 conpty_run，脚本里这样用：
    ///
    ///   conpty_run("", "", rst, "cmd=C:\\OpenPLC_Runtime\\usr\\bin\\bash.exe -i\n" +
    ///                            "pass=bin\n" +
    ///                            "send=date\n" +
    ///                            "expect=2026\n" +
    ///                            "send=ls /\n" +
    ///                            "exit=bin\n" +
    ///                            "timeout=30000");
    ///
    /// d 参数里的 DSL 规则（一行一条，# 开头与空行忽略）：
    ///   cmd=      被测命令行（必填）
    ///   timeout=  整场总超时毫秒（默认 30000）
    ///   pass=     通过关键字（出现即记"见过"）
    ///   fail=     失败关键字（出现即记失败并中止）
    ///   kill=     出现该关键字 → 立即 Kill 进程并判 fail
    ///   exit=     出现该关键字 → 立即发 exit 命令结束程序（动作，不直接决定判定）
    ///   send=     发一行 + 回车
    ///   key=      发裸按键（不带回车）
    ///   expect=   等到该关键字出现才走下一步，超时判 fail
    ///   sleep=    固定等待毫秒
    ///
    /// 判定：命中 kill / fail → fail；否则跑完且 pass 出现过 → pass；其余（含超时）→ fail。
    /// </summary>
    public class conpty_term : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "";

        public conpty_term(testcase_dll _tc)
        {
            tc = _tc;
            InsertDefaultAction();
            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "conpty_";
            tc.funcs.Add(id + "run", conpty_run);
        }

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void set_default_set() { }

        public void Dispose()
        {
            try { tc.dev_moren.Remove(id); } catch { }
        }

        // ================= 注册给引擎的函数 =================

        private string conpty_run(string a, string b, out string c, string d = "")
        {
            c = "fail";                       // 兜底：任何异常路径都判 fail
            try
            {
                string cmd = null;
                int timeout = 30000;
                var passKeys = new List<string>();
                var failKeys = new List<string>();
                var killKeys = new List<string>();
                var exitKeys = new List<string>();
                var steps = new List<Step>();

                if (string.IsNullOrEmpty(d))
                {
                    utility_func.callbackdebuginfo("[conpty] d 参数为空，没有可执行的规格");
                    return "fail";
                }

                foreach (var rawLine in d.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n'))
                {
                    string line = rawLine.Trim();
                    if (line.Length == 0 || line[0] == '#') continue;
                    int eq = line.IndexOf('=');
                    if (eq <= 0) continue;
                    string k = line.Substring(0, eq).Trim().ToLowerInvariant();
                    string v = line.Substring(eq + 1).Trim();

                    switch (k)
                    {
                        case "cmd": cmd = v; break;
                        case "timeout": int.TryParse(v, out timeout); break;
                        case "pass": passKeys.Add(v); break;
                        case "fail": failKeys.Add(v); break;
                        case "kill": killKeys.Add(v); break;
                        case "exit": exitKeys.Add(v); break;
                        case "send": steps.Add(new Step { Kind = StepKind.Send, Arg = v }); break;
                        case "key": steps.Add(new Step { Kind = StepKind.Key, Arg = v }); break;
                        case "expect": steps.Add(new Step { Kind = StepKind.Expect, Arg = v }); break;
                        case "sleep": steps.Add(new Step { Kind = StepKind.Sleep, Arg = v }); break;
                    }
                }

                if (string.IsNullOrEmpty(cmd))
                {
                    utility_func.callbackdebuginfo("[conpty] 缺少 cmd= 被测命令行");
                    return "fail";
                }
                if (timeout <= 0) timeout = 30000;

                utility_func.callbackdebuginfo("[conpty] 启动: " + cmd + "  (timeout=" + timeout + "ms)");

                var logQueue = new Queue<string>();     // 后台线程只入队，主线程出队写日志（避免跨线程碰 UI）
                int logged = 0;
                bool passSeen = false, failSeen = false, killed = false;

                using (var s = ConPty.Start(cmd))
                {
                    s.OutputLine += delegate (string line)
                    {
                        lock (logQueue) logQueue.Enqueue(line);
                    };

                    // 全局异步规则：随时盯着
                    foreach (var key in killKeys)
                    {
                        string kw = key;
                        s.When(delegate (string t) { return t.IndexOf(kw, StringComparison.Ordinal) >= 0; },
                            delegate (ConPtySession ss)
                            {
                                killed = true;
                                lock (logQueue) logQueue.Enqueue(">> 命中 kill=" + kw + " → 强制结束进程");
                                ss.Kill();
                            });
                    }
                    foreach (var key in exitKeys)
                    {
                        string kw = key;
                        s.When(delegate (string t) { return t.IndexOf(kw, StringComparison.Ordinal) >= 0; },
                            delegate (ConPtySession ss)
                            {
                                lock (logQueue) logQueue.Enqueue(">> 命中 exit=" + kw + " → 发送 exit 结束程序");
                                ss.SendLine("exit");
                            });
                    }

                    var deadline = DateTime.UtcNow.AddMilliseconds(timeout);

                    // 顺序执行交互步骤，边等边把实时输出刷到框架日志
                    foreach (var step in steps)
                    {
                        if (killed) break;
                        if (DateTime.UtcNow >= deadline)
                        {
                            utility_func.callbackdebuginfo("[conpty] 超时（步骤未跑完）");
                            break;
                        }
                        int remain = (int)Math.Max(1, (deadline - DateTime.UtcNow).TotalMilliseconds);

                        switch (step.Kind)
                        {
                            case StepKind.Send:
                                utility_func.callbackdebuginfo("[conpty] >>> " + step.Arg);
                                s.SendLine(step.Arg);
                                FlushLog(logQueue, ref logged);
                                Thread.Sleep(200);
                                break;

                            case StepKind.Key:
                                utility_func.callbackdebuginfo("[conpty] >>> [按键] " + step.Arg);
                                s.SendRaw(step.Arg);
                                FlushLog(logQueue, ref logged);
                                Thread.Sleep(200);
                                break;

                            case StepKind.Expect:
                                {
                                    utility_func.callbackdebuginfo("[conpty] ... 等待: " + step.Arg);
                                    string kw = step.Arg;
                                    bool hit = s.WaitFor(delegate (string t)
                                    {
                                        return t.IndexOf(kw, StringComparison.Ordinal) >= 0;
                                    }, remain, delegate () { FlushLog(logQueue, ref logged); });
                                    if (!hit)
                                    {
                                        utility_func.callbackdebuginfo("[conpty] 等待超时，未出现: " + step.Arg);
                                        goto done;
                                    }
                                    break;
                                }

                            case StepKind.Sleep:
                                {
                                    int ms;
                                    if (int.TryParse(step.Arg, out ms))
                                    {
                                        var sw = System.Diagnostics.Stopwatch.StartNew();
                                        while (sw.ElapsedMilliseconds < ms)
                                        {
                                            FlushLog(logQueue, ref logged);
                                            Thread.Sleep(30);
                                        }
                                    }
                                    break;
                                }
                        }
                    }

                    // 步骤跑完：再给一点时间让 pass/fail 关键字出现
                    while (DateTime.UtcNow < deadline && !killed)
                    {
                        string all = s.CleanText;
                        if (CheckKeys(all, failKeys)) { failSeen = true; break; }
                        if (CheckKeys(all, passKeys)) { passSeen = true; break; }
                        if (s.WaitForExit(50)) break;
                        FlushLog(logQueue, ref logged);
                    }

                    string txt = s.CleanText;
                    if (CheckKeys(txt, failKeys)) failSeen = true;
                    if (CheckKeys(txt, passKeys)) passSeen = true;

                    FlushLog(logQueue, ref logged);

                done:
                    s.WaitForExit(2000);
                }

                FlushLog(logQueue, ref logged);

                bool pass = !killed && !failSeen && passSeen;
                c = pass ? "pass" : "fail";
                utility_func.callbackdebuginfo("[conpty] 结束，判定 " + c +
                    "  (pass关键字=" + (passSeen ? "出现" : "未出现") +
                    ", fail关键字=" + (failSeen ? "出现" : "未出现") +
                    ", 被强制结束=" + killed + ")");
                return c;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[conpty] 异常: " + ex.Message);
                c = "fail";
                return "fail";
            }
        }

        static bool CheckKeys(string text, List<string> keys)
        {
            foreach (var k in keys)
                if (text.IndexOf(k, StringComparison.Ordinal) >= 0) return true;
            return false;
        }

        const int MAX_LOG_LINES = 5000;

        static void FlushLog(Queue<string> q, ref int logged)
        {
            List<string> batch = null;
            lock (q)
            {
                if (q.Count > 0)
                {
                    batch = new List<string>(q);
                    q.Clear();
                }
            }
            if (batch == null) return;
            foreach (var line in batch)
            {
                if (logged >= MAX_LOG_LINES)
                {
                    if (logged == MAX_LOG_LINES)
                    {
                        utility_func.callbackdebuginfo("[conpty] 输出过多，后续不再逐行显示");
                        logged++;
                    }
                    continue;
                }
                utility_func.callbackdebuginfo("[conpty] " + line);
                logged++;
            }
        }

        enum StepKind { Send, Key, Expect, Sleep }

        class Step
        {
            public StepKind Kind;
            public string Arg;
        }
    }
}
