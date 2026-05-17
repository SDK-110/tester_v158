# 调试控制窗体 Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** 在 test_antdui 系统中增加一个 ANTDUI 风格的独立浮动调试控制窗体，支持单步执行和运行到指定行两种调试模式

**Architecture:** 在 TestEngine 中增加 TaskCompletionSource 暂停机制，每个测试项执行完毕后检查调试模式决定是否暂停。新 DebugControlForm（AntdUI.Window）通过左侧菜单控制，右侧面板实时显示状态。MainForm 桥接表格双击事件和调试命令。

**Tech Stack:** .NET WinForms, ANTDUI, TaskCompletionSource async/await

---

### Task 1: TestEngine — 添加 DebugMode 枚举和暂停基础设施

**Files:**
- Modify: `testerNew/TestEngine.cs` — 在文件末尾附近添加新类型

- [ ] **Step 1: 在 TestEngine.cs 中添加 DebugMode 枚举**

```csharp
// 在 TestEngine.cs 中，现有的 TestEngine 类之前或之后添加
public enum DebugMode { Normal, StepMode, RunToMode }
```

- [ ] **Step 2: 在 TestEngine.cs 中添加 DebugPausedEventArgs**

```csharp
// 与 TestEngineEventArgs 放在一起
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
```

- [ ] **Step 3: 在 TestEngine 类中添加新字段、属性和事件**

```csharp
// 在现有字段旁添加
private DebugMode _debugMode = DebugMode.Normal;
private int _targetRow = -1;
private TaskCompletionSource<bool> _pauseTcs;

// 在现有属性旁添加
public DebugMode CurrentDebugMode => _debugMode;
public int TargetRow => _targetRow;

// 在现有事件旁添加
public event EventHandler<DebugPausedEventArgs> DebugPaused;
```

- [ ] **Step 4: 在 TestEngine 中添加 DebugPaused 触发方法**

```csharp
protected void OnDebugPaused(int index, TestItem item)
{
    DebugPaused?.Invoke(this, new DebugPausedEventArgs(index, item, _debugMode, _targetRow));
}
```

- [ ] **Step 5: 在 Reset() 方法中添加调试状态重置**

```csharp
public void Reset()
{
    _currentIndex = -1;
    _debugMode = DebugMode.Normal;
    _targetRow = -1;
    foreach (var item in _testItems) item.Reset();
}
```

- [ ] **Step 6: 提交**

```bash
git add testerNew/TestEngine.cs
git commit -m "feat: add DebugMode enum and pause infrastructure to TestEngine"
```

---

### Task 2: TestEngine — 修改 RunAsync 主循环支持暂停

**Files:**
- Modify: `testerNew/TestEngine.cs` — RunAsync 方法中的循环体

- [ ] **Step 1: 在 RunAsync 的循环末尾（测试项执行完毕后）插入暂停检查**

在 `TestItemCompleted?.Invoke(...)` 调用之后、`if (!itemPass...)` 逻辑之前，插入以下代码：

```csharp
// 调试暂停检查
if (!_cts.Token.IsCancellationRequested && _debugMode != DebugMode.Normal)
{
    bool shouldPause = false;
    if (_debugMode == DebugMode.StepMode)
        shouldPause = true;
    else if (_debugMode == DebugMode.RunToMode && i >= _targetRow)
        shouldPause = true;

    if (shouldPause)
    {
        // RunTo 到达目标后自动切换为 StepMode
        if (_debugMode == DebugMode.RunToMode)
            _debugMode = DebugMode.StepMode;

        _pauseTcs = new TaskCompletionSource<bool>();
        OnDebugPaused(i, item);
        OnLogMessage($"[调试] 已暂停在 [{item.Id}] {item.Name}");
        await _pauseTcs.Task;
        OnLogMessage($"[调试] 继续执行...");
    }
}
```

- [ ] **Step 2: 确保 `_debugMode` 在测试完成/取消时复位**

在 `TestCompleted?.Invoke(...)` 之后添加：

```csharp
// 无论如何结束后复位调试模式
_debugMode = DebugMode.Normal;
_targetRow = -1;
```

在 `finally` 块中 `_isRunning = false;` 之前添加这个。

- [ ] **Step 3: 提交**

```bash
git add testerNew/TestEngine.cs
git commit -m "feat: add debug pause check in RunAsync loop"
```

---

### Task 3: TestEngine — 添加 StepNext/RunTo/Continue 控制方法

**Files:**
- Modify: `testerNew/TestEngine.cs`

- [ ] **Step 1: 添加 StepNext、RunTo、Continue 方法**

```csharp
/// <summary>
/// 放行一步，引擎继续保持在 StepMode
/// </summary>
public void StepNext()
{
    _pauseTcs?.TrySetResult(true);
    _debugMode = DebugMode.StepMode;
}

/// <summary>
/// 设置目标行并放行，引擎会一直运行到目标行后暂停
/// 如果目标行已过当前位置，拒绝设置
/// </summary>
public void RunTo(int targetRow)
{
    if (targetRow <= _currentIndex)
    {
        OnLogMessage($"[调试] 目标行 #{targetRow + 1} 已过当前位置 (#{_currentIndex + 1})，无法跳回");
        return;
    }
    _targetRow = targetRow;
    _debugMode = DebugMode.RunToMode;
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
```

- [ ] **Step 2: 提交**

```bash
git add testerNew/TestEngine.cs
git commit -m "feat: add StepNext/RunTo/Continue debug control methods"
```

---

### Task 4: DebugControlForm — 创建 ANTDUI 风格独立调试窗体

**Files:**
- Create: `testerNew/DebugControlForm.cs`
- Modify: `testerNew/testapp.csproj` — 添加新文件引用（可选，取决于项目结构）

- [ ] **Step 1: 创建 DebugControlForm.cs**

```csharp
using AntdUI;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace test_antdui
{
    public partial class DebugControlForm : AntdUI.Window
    {
        private MainForm _mainForm;
        private TestEngine _engine;

        // 左侧菜单
        private AntdUI.Menu _menu;
        // 右侧面板
        private Panel _contentPanel;

        // 状态显示
        private Label _lblMode;
        private Label _lblStatus;
        private Label _lblCurrentItem;
        private Label _lblTargetItem;
        private AntdUI.Progress _progress;

        // 按钮
        private AntdUI.Button _btnStepNext;
        private AntdUI.Button _btnContinue;
        private AntdUI.Button _btnStop;

        // 目标行
        private int _targetRow = -1;
        private string _targetName = "";

        // 单例
        private static DebugControlForm _instance;
        public static DebugControlForm Instance => _instance;

        public DebugControlForm(MainForm mainForm)
        {
            _mainForm = mainForm;
            _engine = GetEngineFromMainForm();
            _instance = this;

            // 窗体设置
            Text = "调试控制";
            Size = new Size(480, 560);
            MinimumSize = new Size(420, 400);
            StartPosition = FormStartPosition.Manual;
            TopMost = false;

            // 默认停靠在主窗体右侧
            if (_mainForm != null)
            {
                Location = new Point(
                    _mainForm.Right + 10,
                    _mainForm.Top + 50
                );
                _mainForm.LocationChanged += (s, e) => AdjustPosition();
                _mainForm.SizeChanged += (s, e) => AdjustPosition();
            }

            InitializeControls();
            SubscribeEngineEvents();
            UpdateUIState();

            // 跟随主题
            AntdUI.Config.IsDarkChanged += (s, e) => { BackColor = Color.FromArgb(24, 24, 24); };
            if (AntdUI.Config.IsDark)
                BackColor = Color.FromArgb(24, 24, 24);
        }

        private TestEngine GetEngineFromMainForm()
        {
            // 通过反射或内部方法获取引擎
            // 由于 MainForm 的 _engine 是私有字段，我们添加一个内部访问属性
            return _mainForm?.GetEngine();
        }

        private void InitializeControls()
        {
            // ═══ 左侧菜单 ════════════════════════════════
            _menu = new AntdUI.Menu
            {
                Dock = DockStyle.Left,
                Width = 52,
                Collapsed = true,
                CollapseWidth = 52,
                CollapsedWidth = 160,
                BackColor = Color.FromArgb(0, 21, 41),
                AutoSize = false
            };

            _menu.Items = new AntdUI.MenuItemCollection
            {
                new AntdUI.MenuItem
                {
                    Text = "单步执行",
                    IconSvg = "StepForwardOutlined"
                },
                new AntdUI.MenuItem
                {
                    Text = "运行到指定行",
                    IconSvg = "CaretRightOutlined"
                },
                new AntdUI.MenuItem
                {
                    Text = "继续运行",
                    IconSvg = "PlayCircleOutlined"
                },
                new AntdUI.MenuItem
                {
                    Text = "停止",
                    IconSvg = "CloseCircleOutlined"
                }
            };

            _menu.SelectChanged += Menu_SelectChanged;
            _menu.MouseEnter += (s, e) => { _menu.Collapsed = false; _menu.Width = 160; };
            _menu.MouseLeave += (s, e) => { _menu.Collapsed = true; _menu.Width = 52; };

            Controls.Add(_menu);

            // ═══ 右侧内容面板 ════════════════════════════
            _contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                AutoScroll = true
            };
            Controls.Add(_contentPanel);

            // 当前模式
            _lblMode = new Label
            {
                Text = "当前模式：正常执行",
                Font = new Font("Microsoft YaHei UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 144, 255),
                Location = new Point(16, 16),
                AutoSize = true
            };
            _contentPanel.Controls.Add(_lblMode);

            // 分隔线
            var divider1 = new AntdUI.Divider
            {
                Location = new Point(16, _lblMode.Bottom + 8),
                Width = _contentPanel.Width - 48
            };
            _contentPanel.Controls.Add(divider1);

            // 状态信息
            int infoY = divider1.Bottom + 12;

            var lblStatusTitle = new Label { Text = "状态信息", Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold), Location = new Point(16, infoY), AutoSize = true };
            _contentPanel.Controls.Add(lblStatusTitle);

            _lblStatus = new Label
            {
                Text = "状态：等待操作...",
                Location = new Point(32, lblStatusTitle.Bottom + 8),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };
            _contentPanel.Controls.Add(_lblStatus);

            _lblCurrentItem = new Label
            {
                Text = "当前项：---",
                Location = new Point(32, _lblStatus.Bottom + 6),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };
            _contentPanel.Controls.Add(_lblCurrentItem);

            _lblTargetItem = new Label
            {
                Text = "目标行：---",
                Location = new Point(32, _lblCurrentItem.Bottom + 6),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };
            _contentPanel.Controls.Add(_lblTargetItem);

            // 进度条
            var lblProgress = new Label { Text = "进度", Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold), Location = new Point(16, _lblTargetItem.Bottom + 16), AutoSize = true };
            _contentPanel.Controls.Add(lblProgress);

            _progress = new AntdUI.Progress
            {
                Location = new Point(32, lblProgress.Bottom + 8),
                Width = _contentPanel.Width - 80,
                Value = 0,
                Shape = AntdUI.TShape.Round,
                ShowText = true
            };
            _contentPanel.Controls.Add(_progress);

            // 分隔线
            var divider2 = new AntdUI.Divider
            {
                Location = new Point(16, _progress.Bottom + 12),
                Width = _contentPanel.Width - 48
            };
            _contentPanel.Controls.Add(divider2);

            // 操作按钮
            int btnY = divider2.Bottom + 12;
            var lblActions = new Label { Text = "操作", Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold), Location = new Point(16, btnY), AutoSize = true };
            _contentPanel.Controls.Add(lblActions);

            _btnStepNext = new AntdUI.Button
            {
                Text = "▶ 执行下一步",
                Location = new Point(32, lblActions.Bottom + 12),
                Size = new Size(140, 38),
                Type = AntdUI.TTypeMini.Primary,
                Enabled = false
            };
            _btnStepNext.Click += BtnStepNext_Click;
            _contentPanel.Controls.Add(_btnStepNext);

            _btnContinue = new AntdUI.Button
            {
                Text = "▶▶ 继续运行",
                Location = new Point(186, lblActions.Bottom + 12),
                Size = new Size(130, 38),
                Type = AntdUI.TTypeMini.Success,
                Enabled = false
            };
            _btnContinue.Click += BtnContinue_Click;
            _contentPanel.Controls.Add(_btnContinue);

            _btnStop = new AntdUI.Button
            {
                Text = "■ 停止测试",
                Location = new Point(32, _btnStepNext.Bottom + 10),
                Size = new Size(284, 38),
                Type = AntdUI.TTypeMini.Error,
                Enabled = false
            };
            _btnStop.Click += BtnStop_Click;
            _contentPanel.Controls.Add(_btnStop);

            // 快捷键提示
            var shortcutHint = new Label
            {
                Text = "快捷键:  F10 单步  |  Ctrl+F10 运行到指定行  |  F5 继续  |  Shift+F5 停止",
                Location = new Point(16, _btnStop.Bottom + 20),
                AutoSize = true,
                ForeColor = Color.FromArgb(120, 120, 120),
                Font = new Font("Microsoft YaHei UI", 9)
            };
            _contentPanel.Controls.Add(shortcutHint);
        }

        private void SubscribeEngineEvents()
        {
            if (_engine == null) return;

            _engine.DebugPaused += OnEngineDebugPaused;
        }

        private void Menu_SelectChanged(object sender, AntdUI.MenuSelectEventArgs e)
        {
            if (e.Value == null) return;
            ExecuteCommand(e.Value.Text);
        }

        private void ExecuteCommand(string cmd)
        {
            switch (cmd)
            {
                case "单步执行":
                    if (_engine == null || !_engine.IsRunning)
                    {
                        AntdUI.Message.info(this, "请先在主窗体开始测试");
                        return;
                    }
                    _engine.StepNext();
                    _lblStatus.Text = "状态：单步模式已激活";
                    break;

                case "运行到指定行":
                    if (_engine == null || !_engine.IsRunning)
                    {
                        AntdUI.Message.info(this, "请先在主窗体开始测试");
                        return;
                    }
                    if (_targetRow < 0)
                    {
                        AntdUI.Message.warn(this, "请先在主表格中双击选择目标行");
                        return;
                    }
                    _engine.RunTo(_targetRow);
                    _lblStatus.Text = $"状态：正在运行到目标行 #{_targetRow + 1}...";
                    break;

                case "继续运行":
                    if (_engine == null || !_engine.IsRunning)
                    {
                        AntdUI.Message.info(this, "请先在主窗体开始测试");
                        return;
                    }
                    _engine.Continue();
                    _lblStatus.Text = "状态：连续运行中";
                    break;

                case "停止":
                    _engine?.Cancel();
                    _lblStatus.Text = "状态：用户停止";
                    break;
            }

            UpdateUIState();
        }

        private void OnEngineDebugPaused(object sender, DebugPausedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnEngineDebugPaused(sender, e)));
                return;
            }

            string modeText = e.Mode == DebugMode.StepMode ? "单步执行" : "运行到指定行";
            _lblMode.Text = $"当前模式：{modeText}";
            _lblStatus.Text = $"状态：已暂停 — {e.CurrentItem?.Name}";
            _lblCurrentItem.Text = $"当前项：#{e.CurrentIndex + 1} - {e.CurrentItem?.Name}";

            if (_targetRow >= 0)
                _lblTargetItem.Text = $"目标行：#{_targetRow + 1} - {_targetName}";

            if (_mainForm != null)
            {
                int total = _engine?.TotalItems ?? 1;
                float v = total > 0 ? (float)(e.CurrentIndex + 1) / total : 0;
                _progress.Value = v;
            }

            UpdateUIState();
        }

        public void SetTargetRow(int rowIndex, TestItem item)
        {
            _targetRow = rowIndex;
            _targetName = item?.Name ?? "";
            _lblTargetItem.Text = $"目标行：#{rowIndex + 1} - {_targetName}";
        }

        public void UpdateTestProgress(int current, int total)
        {
            if (total > 0)
                _progress.Value = (float)((double)current / total);
        }

        public void UpdateUIState()
        {
            bool isRunning = _engine?.IsRunning ?? false;
            bool isPaused = _engine?.CurrentDebugMode != DebugMode.Normal;
            bool canStep = isRunning && isPaused;
            bool canStop = isRunning;

            _btnStepNext.Enabled = canStep;
            _btnContinue.Enabled = canStep;
            _btnStop.Enabled = canStop;
        }

        public void ResetState()
        {
            _lblMode.Text = "当前模式：正常执行";
            _lblStatus.Text = "状态：等待操作...";
            _lblCurrentItem.Text = "当前项：---";
            _progress.Value = 0;
            _targetRow = -1;
            _targetName = "";
            _lblTargetItem.Text = "目标行：---";
            UpdateUIState();
        }

        private void BtnStepNext_Click(object sender, EventArgs e)
        {
            _engine?.StepNext();
            UpdateUIState();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            _engine?.Continue();
            UpdateUIState();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            _engine?.Cancel();
            UpdateUIState();
        }

        private void AdjustPosition()
        {
            if (_mainForm != null && !_mainForm.IsDisposed)
            {
                Location = new Point(
                    _mainForm.Right + 10,
                    Math.Max(0, _mainForm.Top + 50)
                );
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _instance = null;
            base.OnFormClosed(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F10:
                    ExecuteCommand("单步执行");
                    return true;
                case Keys.F5:
                    ExecuteCommand("继续运行");
                    return true;
                case Keys.F5 | Keys.Shift:
                    ExecuteCommand("停止");
                    return true;
                case Keys.F10 | Keys.Control:
                    ExecuteCommand("运行到指定行");
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
```

- [ ] **Step 2: 提交**

```bash
git add testerNew/DebugControlForm.cs
git commit -m "feat: create DebugControlForm with ANTDUI left menu and control panel"
```

---

### Task 5: MainForm — 集成调试控制功能

**Files:**
- Modify: `testerNew/MainControl.cs` — 添加菜单项、桥接方法和事件处理
- Modify: `testerNew/MainControl.Designer.cs` — 可选（我们采用编程式添加菜单项）

- [ ] **Step 1: 在 MainControl.cs 中添加 GetEngine() 方法和 DebugControlForm 字段**

```csharp
// 在现有字段后添加
private DebugControlForm _debugForm;

// 添加内部方法供 DebugControlForm 访问引擎
internal TestEngine GetEngine() => _engine;
```

- [ ] **Step 2: 在 MainControl.cs 的 InitializeTestEngine 调用之后或 MainForm 构造末尾添加调试菜单项**

```csharp
// 在 MainForm() 构造中，现有代码之后添加
AddDebugMenuItem();
```

- [ ] **Step 3: 在 MainControl.cs 中添加 AddDebugMenuItem 方法和菜单事件处理**

```csharp
private void AddDebugMenuItem()
{
    // 在现有菜单中增加"调试控制"项
    menu1.Items.Add(new AntdUI.MenuItem
    {
        Text = "Debug Control",
        IconSvg = "ControlOutlined"
    });
}
```

- [ ] **Step 4: 在 MainControl.cs 的 Menu1_SelectChanged 中添加 "Debug Control" case**

```csharp
// 在 switch 中添加
case "Debug Control":
    OpenDebugControl();
    break;
```

- [ ] **Step 5: 在 MainControl.cs 中添加 OpenDebugControl 方法**

```csharp
private void OpenDebugControl()
{
    if (_debugForm == null || _debugForm.IsDisposed)
    {
        _debugForm = new DebugControlForm(this);
        _debugForm.Show();
    }
    else
    {
        _debugForm.BringToFront();
    }
}
```

- [ ] **Step 6: 在 MainControl.cs 中注册表格双击事件**

在 `SetupTable()` 方法末尾或 `MainForm()` 构造中添加：

```csharp
// 注册表格双击事件
_table.MouseDoubleClick += Table_MouseDoubleClick;
```

- [ ] **Step 7: 添加 Table_MouseDoubleClick 方法**

```csharp
private void Table_MouseDoubleClick(object sender, MouseEventArgs e)
{
    // 通过 ANTDUI Table 的行命中检测确定双击的是哪一行
    // 由于 ANTDUI Table 的 HitTest 实现方式，我们需要获取点击位置对应的行
    if (_engine == null || _project == null) return;

    // 通过坐标估计行号（ANTDUI Table 支持行高计算）
    int rowHeight = _table.RowHeight > 0 ? _table.RowHeight : 36;
    int headerHeight = 32;
    int scrollOffset = 0; // 如果 Table 有滚动，需要获取滚动偏移

    int clickedY = e.Y - headerHeight;
    if (clickedY < 0) return;

    int estimatedRow = clickedY / rowHeight;
    if (estimatedRow >= 0 && estimatedRow < _project.Items.Count)
    {
        var item = _project.Items[estimatedRow];
        if (_debugForm != null && !_debugForm.IsDisposed)
        {
            _debugForm.SetTargetRow(estimatedRow, item);
            AntdUI.Message.success(this, $"已设置目标行：#{estimatedRow + 1} - {item.Name}", autoClose: 2);
        }
        else
        {
            // 打开调试窗体并设置目标行
            OpenDebugControl();
            if (_debugForm != null && !_debugForm.IsDisposed)
                _debugForm.SetTargetRow(estimatedRow, item);
            AntdUI.Message.success(this, $"已设置目标行：#{estimatedRow + 1} - {item.Name}", autoClose: 2);
        }
    }
}
```

- [ ] **Step 8: 在 MainControl.cs 的 destroy() 方法中添加 DebugControlForm 清理**

```csharp
public void destroy()
{
    // 现有清理代码...
    _debugForm?.Close();
    _debugForm?.Dispose();
}
```

- [ ] **Step 9: 在 OnTestItemStarted 和 OnTestCompleted 中同步进度到调试窗体**

在 `OnTestItemStarted` 末尾添加：

```csharp
if (_debugForm != null && !_debugForm.IsDisposed)
{
    _debugForm.UpdateTestProgress(e.Index + 1, e.Total);
}
```

在 `OnTestCompleted` 末尾添加：

```csharp
if (_debugForm != null && !_debugForm.IsDisposed)
{
    _debugForm.ResetState();
}
```

- [ ] **Step 10: 提交**

```bash
git add testerNew/MainControl.cs
git commit -m "feat: integrate debug control form with MainForm - menu, table double-click, bridge"
```

---

### Task 6: 构建验证

- [ ] **Step 1: 尝试构建项目**

```bash
cd F:/tester_v153 && dotnet build testapp/testapp.csproj 2>&1 || msbuild testapp.sln 2>&1
```

- [ ] **Step 2: 修复任何编译错误** — 检查命名空间、类型引用、ANTDUI API 可用性

- [ ] **Step 3: 提交最终修复**

```bash
git add -A
git commit -m "fix: build errors from debug control integration"
```

---

### 最终验证清单

- [ ] `TestEngine.DebugMode` 枚举定义正确
- [ ] `TestEngine.StepNext()` — 放行一步，保持 StepMode
- [ ] `TestEngine.RunTo(target)` — 设置目标行，放行，到达后自动切换 StepMode
- [ ] `TestEngine.Continue()` — 恢复正常执行
- [ ] `TestEngine.Cancel()` — 停止 + 复位调试状态
- [ ] `DebugControlForm` 左侧菜单可展开折叠
- [ ] `DebugControlForm` 右侧面板正确显示模式/状态/进度
- [ ] 快捷键 F10/Ctrl+F10/F5/Shift+F5 生效
- [ ] 双击主窗体 Table 行 → 设置目标行
- [ ] 调试窗体停靠在主窗体右侧
- [ ] 调试关闭不影响引擎运行
- [ ] 引擎自动复位调试状态
