# 日志窗体：显示测试总耗时 + 默认置顶 + 屏幕左上角 实现计划

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 在测试全部结束后于日志窗体中显示总耗时日志，并让日志窗体默认置顶、初始位于屏幕左上角。

**Architecture:** 方案 B（最小改动）。总耗时在 `MainControl._btnStart_Click` 末尾计算并 `AddLog` 一行橙色日志，复用已有 `_testStartTime`；置顶与左上角在 `TestLoggerForm` 构造函数中设置，复用现有 `checkbox1_CheckedChanged` 同步复选框。不改动 `TestEngine`、`DebugControlForm` 与报表逻辑。

**Tech Stack:** C# / .NET 8 / WinForms / AntdUI。

**参考规格:** `docs/superpowers/specs/2026-08-01-test-logger-form-elapsed-topmost-design.md`

**构建命令（两个任务共用）:**
```bash
dotnet build testapp.sln -v q
```
说明：项目很大，可能因历史遗留产生与本任务无关的告警/错误。本计划验证标准为——**没有新增与本任务文件（`TestLoggerForm.cs`、`MainControl.cs`）相关的编译错误**。

---

### Task 1: TestLoggerForm 默认置顶 + 屏幕左上角

**Files:**
- Modify: `testapp/testerNew/TestLoggerForm.cs:45-52`（构造函数）

- [ ] **Step 1: 修改构造函数**

当前代码（`TestLoggerForm.cs:45-52`）：

```csharp
private TestLoggerForm()
{
    _syncContext = SynchronizationContext.Current;
    InitializeComponent();
    CheckForIllegalCrossThreadCalls = false;
    InitTimer();
    StartNewLog();
}
```

改为：

```csharp
private TestLoggerForm()
{
    _syncContext = SynchronizationContext.Current;
    InitializeComponent();
    CheckForIllegalCrossThreadCalls = false;
    InitTimer();
    StartNewLog();
    // 默认置顶 + 屏幕左上角
    TopMost = true;
    StartPosition = FormStartPosition.Manual;
    Location = new Point(0, 0);
    checkbox1.Checked = true;   // 同步「TopLevel」复选框为勾选
}
```

说明：
- `System.Drawing.Point` 与 `System.Windows.Forms.FormStartPosition` 均已被文件顶部 `using` 覆盖（`TestLoggerForm.cs:4,8`）。
- `checkbox1.Checked = true` 会触发 `checkbox1_CheckedChanged`（Designer 已接线），再次确认 `TopMost = true`，状态一致。
- 运行时用户仍可拖动窗体、取消勾选关闭置顶。

- [ ] **Step 2: 构建验证**

Run:
```bash
dotnet build testapp.sln -v q
```
Expected: 无与 `TestLoggerForm.cs` 相关的编译错误。

- [ ] **Step 3: 提交**

```bash
git add testapp/testerNew/TestLoggerForm.cs
git commit -m "feat: 日志窗体默认置顶并定位到屏幕左上角

Co-Authored-By: Claude <noreply@anthropic.com>"
```

---

### Task 2: MainControl 测试结束时显示总耗时

**Files:**
- Modify: `testapp/testerNew/MainControl.cs:897-901`（`_btnStart_Click` 尾部）

- [ ] **Step 1: 修改 `_btnStart_Click` 尾部**

当前代码（`MainControl.cs:897-901`）：

```csharp
            AddLog(AppStrings.Get("log_test_steps"));
            await _engine.RunAsync();
            await _engine.RunCleanupAsync();
            AddLog(AppStrings.Get("log_test_complete"));
        }
```

改为：

```csharp
            AddLog(AppStrings.Get("log_test_steps"));
            await _engine.RunAsync();
            await _engine.RunCleanupAsync();
            AddLog(AppStrings.Get("log_test_complete"));

            // 测试总耗时（含初始化与清理）
            var elapsed = DateTime.Now - _testStartTime;
            AddLog($"总耗时: {elapsed:hh\\:mm\\:ss\\.fff}", Color.FromArgb(250, 173, 20));
        }
```

说明：
- `_testStartTime` 已在 `_btnStart_Click` 开头（`MainControl.cs:859`）赋值，此处计算从点击开始到清理完毕的真实总耗时。
- `Color.FromArgb` 需要 `using System.Drawing;`（`MainControl.cs:8` 已有）。
- 初始化失败提前 `return` 时不会走到此处，因此只有真正跑完的测试才会显示总耗时。

- [ ] **Step 2: 构建验证**

Run:
```bash
dotnet build testapp.sln -v q
```
Expected: 无与 `MainControl.cs` 相关的编译错误。

- [ ] **Step 3: 提交**

```bash
git add testapp/testerNew/MainControl.cs
git commit -m "feat: 测试结束时在日志窗体显示总耗时

Co-Authored-By: Claude <noreply@anthropic.com>"
```

---

### Task 3: 手动功能验证

**Files:** 无（运行验证）

- [ ] **Step 1: 运行程序并逐项验证**

启动应用（Debug 下运行 `testapp`），依次确认：

1. 日志窗体初始出现在**屏幕左上角** `(0, 0)`。
2. 日志窗体**默认置顶**，且底部「TopLevel」复选框为**勾选**状态。
3. 勾选/取消「TopLevel」：置顶状态随之切换。
4. 跑一次完整测试：结束时日志框末尾出现一行橙色 `总耗时: hh:mm:ss.fff`（含初始化与清理时间）。
5. 测试中途取消：同样显示总耗时。
