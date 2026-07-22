using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class IpecmdRunner
{
    static string ipecmdpath = "D:\\Microchip\\MPLABX\\v6.30\\mplab_platform\\mplab_ipe";
    static string PORTID = "2026";

    /// <summary>
    /// 清理当前用户 ~\.mchp_ipe\ 下当前端口的锁文件
    /// </summary>
    private static void CleanupPortLock()
    {
        string dir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            ".mchp_ipe");
        if (!Directory.Exists(dir)) return;

        foreach (string f in new[] { $"{PORTID}.lock", $"{PORTID}.ini" })
        {
            string path = Path.Combine(dir, f);
            try { if (File.Exists(path)) File.Delete(path); } catch { }
        }
    }

    /// <summary>
    /// cmd /c "java -jar ipecmdboost.jar ... 2>&1" → 逐行读取 stdout
    /// 检测到 "Wait for current operation to complete" → 删锁 → 重试一次
    /// </summary>
    private static string[] RunCmd(string args, int timeoutMs, Predicate<string[]> isFound)
    {
        int maxRetries = 1;
        for (int retry = 0; retry <= maxRetries; retry++)
        {
            if (retry > 0)
            {
                CleanupPortLock();
                testapp.mylib.utility_func.callbackdebuginfo("--- 端口锁已清理，正在重试 ---");
            }

            string cmdLine = $"java -jar ipecmdboost.jar {args} 2>&1";
            string cmdArgs = $"/c \"{cmdLine}\"";

            var psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = cmdArgs,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                WorkingDirectory = ipecmdpath,
            };

            using (var process = new Process { StartInfo = psi })
            {
                process.Start();

                DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
                var lines = new List<string>();
                bool needRetry = false;

                Task<string> readTask = process.StandardOutput.ReadLineAsync();

                while (true)
                {
                    if (readTask.Wait(200))
                    {
                        string line = readTask.Result;
                        if (line == null) break;

                        lines.Add(line);
                        testapp.mylib.utility_func.callbackdebuginfo(line);

                        // 检测端口锁定 → 删锁重试
                        if (line.IndexOf("Wait for current operation to complete", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            needRetry = true;
                            break;
                        }

                        // 检测目标内容 → 成功返回
                        if (isFound(lines.ToArray()))
                            return lines.ToArray();

                        readTask = process.StandardOutput.ReadLineAsync();
                    }

                    if (DateTime.Now > deadline)
                    {
                        process.Kill();
                        process.WaitForExit(3000);
                        throw new TimeoutException($"操作超时（{timeoutMs} ms）。");
                    }
                }

                if (needRetry)
                {
                    process.Kill();
                    process.WaitForExit(3000);
                    continue; // 外层循环重试
                }

                return lines.ToArray();
            }
        }

        CleanupPortLock();
        throw new InvalidOperationException("重试后仍然失败，端口锁已清理，请再次尝试。");
    }

    public static string GetChecksum(string hexFilePath, int timeoutMs = 50000)
    {
        string args = $@"-P24F32KA302 -TPPK4 -F""{hexFilePath}"" -W -K -OY{PORTID}";
        string result = null;
        Regex regex = new Regex(@"checksum\s*=\s*([0-9A-Fa-f]+)", RegexOptions.IgnoreCase);

        RunCmd(args, timeoutMs, (ln) =>
        {
            foreach (string line in ln)
            {
                Match m = regex.Match(line);
                if (m.Success) { result = m.Groups[1].Value; return true; }
            }
            return false;
        });

        return result;
    }

    public static (bool OK, string Output) ProgramDevice(string hexFilePath, int timeoutMs = 120000)
    {
        string args = $@"-P24F32KA302 -TPPK4 -F""{hexFilePath}""  -W -YP -M -OY{PORTID}";
        bool found = false;

        string[] lines = RunCmd(args, timeoutMs, (ln) =>
        {
            foreach (string line in ln)
            {
                if (line.IndexOf("Operation Succeeded", StringComparison.OrdinalIgnoreCase) >= 0)
                { found = true; return true; }
            }
            return false;
        });

        return (found, string.Join(Environment.NewLine, lines));
    }

    public static (int Code, string Checksum, string Status) VerifyAndProgram(
        string expectedChecksum, string hexFilePath, int timeoutMs = 120000)
    {
        testapp.mylib.utility_func.callbackdebuginfo("═════ 阶段1：获取 checksum ═════");
        string actualChecksum;
        try { actualChecksum = GetChecksum(hexFilePath, timeoutMs); }
        catch (Exception ex)
        {
            testapp.mylib.utility_func.callbackdebuginfo($"阶段1 执行失败：{ex.Message}");
            return (-999, "error", "fail");
        }

        if (actualChecksum == null)
        {
            testapp.mylib.utility_func.callbackdebuginfo("错误：未提取到 checksum。");
            return (-1, "null", "fail");
        }

        testapp.mylib.utility_func.callbackdebuginfo($"实际 checksum : {actualChecksum}");
        testapp.mylib.utility_func.callbackdebuginfo($"预期 checksum : {expectedChecksum}");

        if (!string.Equals(actualChecksum, expectedChecksum, StringComparison.OrdinalIgnoreCase))
        {
            testapp.mylib.utility_func.callbackdebuginfo("✗ checksum 不一致，终止。");
            return (-1, actualChecksum, "fail");
        }

        testapp.mylib.utility_func.callbackdebuginfo("✓ checksum 一致，进入阶段2。");

        testapp.mylib.utility_func.callbackdebuginfo("═════ 阶段2：编程 ═════");
        var (ok, output) = ProgramDevice(hexFilePath, timeoutMs);

        if (ok)
        {
            testapp.mylib.utility_func.callbackdebuginfo("✓ 编程成功。");
            return (0, actualChecksum, "pass");
        }
        else
        {
            testapp.mylib.utility_func.callbackdebuginfo("✗ 编程失败。");
            return (-1, actualChecksum, "fail");
        }
    }
}
