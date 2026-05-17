# 调试控制窗体设计文档

## 概述

在现有 ANTDUI 测试系统（test_antdui）中增加一个独立浮动调试控制窗体，支持单步执行和运行到指定行两种调试模式，对标 Visual Studio 的调试体验。

## 相关文件

| 文件 | 角色 |
|---|---|
| `testerNew/MainControl.cs` | 主窗体，包含 `_table`（测试项列表）和 `_engine` |
| `testerNew/TestEngine.cs` | 测试引擎，驱动测试项顺序执行 |
| `testerNew/TestItem.cs` | 测试项数据模型 |
| `testerNew/DebugControlForm.cs` | **新文件** — 调试控制窗体 |
| `testerNew/DebugControlForm.Designer.cs` | **新文件** — 窗体设计器代码 |
| `testerNew/TestEngine.cs` | **修改** — 增加暂停/继续机制 |

## 1. TestEngine 修改 — 调试模式机制

### 新增枚举

```csharp
public enum DebugMode { Normal, StepMode, RunToMode }
```

### 新增字段/属性

```csharp
private DebugMode _debugMode = DebugMode.Normal;
private int _targetRow = -1;
private TaskCompletionSource<bool> _pauseTcs;

public DebugMode CurrentDebugMode => _debugMode;
public int TargetRow => _targetRow;
public event EventHandler<DebugPausedEventArgs> DebugPaused;
```

### 主循环改动（`RunAsync` 方法）

在每个测试项执行完毕后、进入下一项之前插入暂停检查：

```
for (int i = 0; i < _testItems.Count; i++)
{
    // ... 执行测试项逻辑不变 ...

    // 调试暂停检查
    if (_debugMode != DebugMode.Normal && !_cts.Token.IsCancellationRequested)
    {
        bool shouldPause = false;
        if (_debugMode == DebugMode.StepMode)
            shouldPause = true;                          // 每步暂停
        else if (_debugMode == DebugMode.RunToMode && i >= _targetRow)
            shouldPause = true;                          // 到达目标行

        if (shouldPause)
        {
            _pauseTcs = new TaskCompletionSource<bool>();
            DebugPaused?.Invoke(this, new DebugPausedEventArgs(i, _testItems[i]));
            await _pauseTcs.Task;                         // 等待用户操作
        }
    }
}
```

### 新增方法

```csharp
public void StepNext()
{
    // 放行一步，仍保持 StepMode
    _pauseTcs?.TrySetResult(true);
    _debugMode = DebugMode.StepMode;
}

public void RunTo(int targetRow)
{
    // 如果目标行已经过，拒绝设置
    if (targetRow <= _currentIndex)
    {
        OnLogMessage($"警告: 目标行 #{targetRow+1} 已过，无法回退");
        return;
    }
    _targetRow = targetRow;
    _debugMode = DebugMode.RunToMode;
    _pauseTcs?.TrySetResult(true);
}

public void Continue()
{
    // 恢复正常连续执行
    _debugMode = DebugMode.Normal;
    _pauseTcs?.TrySetResult(true);
}

public void Cancel()  // 已有，不变
```

### 新增事件参数

```csharp
public class DebugPausedEventArgs : EventArgs
{
    public int CurrentIndex { get; }
    public TestItem CurrentItem { get; }
    public DebugPausedEventArgs(int index, TestItem item)
    {
        CurrentIndex = index;
        CurrentItem = item;
    }
}
```

### 状态重置

测试完成（正常结束或用户取消）后自动将 `_debugMode` 重置为 `Normal`。

## 2. DebugControlForm 新窗体

### 窗体规格

- 继承 `AntdUI.Window`（与主窗体一致）
- 窗体标题：**调试控制**
- 初始尺寸：450 x 550
- 支持暗色/亮色主题跟随 `AntdUI.Config.IsDark`
- 位置：启动时自动停靠在主窗体的右侧

### 左侧菜单（AntdUI.Menu）

菜单项使用 ANTDUI 内置 SVG 图标，支持 Collapsed 折叠模式：

| 图标 | 文字 | 快捷键 | 命令 |
|---|---|---|---|
| `StepForwardOutlined` | 单步执行 | F10 | 设置为 StepMode + 放行一步 |
| `CaretRightOutlined` | 运行到指定行 | Ctrl+F10 | 设置 RunToMode 到目标行 |
| `PlayCircleOutlined` | 继续运行 | F5 | 切换为 Normal 模式继续 |
| `CloseCircleOutlined` | 停止 | Shift+F5 | `_engine.Cancel()` |

### 右侧控制面板

控制面板实时显示以下信息：

```
┌──────────────────────────────────────┐
│  [AntdUI.PageHeader] 调试控制        │
│                                      │
│  ┌─ 当前模式 ───────────────────────┐│
│  │  ○ 正常执行                      ││
│  │  ○ 单步执行 ◄ (当前)            ││
│  │  ○ 运行到指定行                  ││
│  └──────────────────────────────────┘│
│                                      │
│  ┌─ 状态信息 ───────────────────────┐│
│  │  ▶ 状态: 等待下一步...           ││
│  │  ▶ 当前项: #3 - CurrentTest      ││
│  │  ▶ 目标行: #5 - VoltageTest      ││
│  │  ▶ 进度: [████████░░] 3/10       ││
│  └──────────────────────────────────┘│
│                                      │
│  ┌─ 操作 ───────────────────────────┐│
│  │  [▶ 执行下一步]  [▶▶ 继续运行]   ││
│  │  [■ 停止测试]                    ││
│  └──────────────────────────────────┘│
│                                      │
│  ┌─ 快捷键提示 ─────────────────────┐│
│  │  F10: 单步  Ctrl+F10: 运行到指定  ││
│  │  F5: 继续运行  Shift+F5: 停止    ││
│  └──────────────────────────────────┘│
└──────────────────────────────────────┘
```

### 控件映射

| 显示元素 | ANTDUI 控件 | 说明 |
|---|---|---|
| 标题栏 | `PageHeader` | 显示"调试控制" |
| 模式选择 | `RadioButton` 组 | 3个单选，互斥，只读显示当前模式 |
| 状态行（4行） | `Label` x4 | 显示状态、当前项、目标行、进度 |
| 进度条 | `Progress` | 0~1 浮点值，彩色 |
| 执行下一步按钮 | `Button` | 仅 StepMode 暂停时可点击 |
| 继续运行按钮 | `Button` | 仅暂停时可点击 |
| 停止测试按钮 | `Button` | 仅引擎运行时可用 |
| 快捷键提示 | `Label` | 静态文字 |

## 3. MainForm 修改

### 调试窗体生命周期管理

```csharp
private DebugControlForm _debugForm;
private bool _debugFormVisible = false;

// 在菜单中增加"调试控制"项
private void Menu1_SelectChanged(object sender, MenuSelectEventArgs e)
{
    // ... 现有菜单处理 ...
    case "Debug Control":
        if (_debugForm == null || _debugForm.IsDisposed)
        {
            _debugForm = new DebugControlForm(this);  // 传入主窗体引用
            _debugForm.Show();
        }
        else
        {
            _debugForm.BringToFront();
        }
        break;
}
```

### 表格双击事件

```csharp
// 在 SetupTable 或 MainForm 构造中注册
_table.CellDoubleClick += (s, e) =>
{
    if (e.RowIndex >= 0 && e.RowIndex < _project.Items.Count)
    {
        var item = _project.Items[e.RowIndex];
        DebugControlForm.Instance?.SetTargetRow(e.RowIndex, item);
    }
};
```

### 桥接方法

```csharp
public void DebugStepNext() => _engine.StepNext();
public void DebugRunTo(int row) => _engine.RunTo(row);
public void DebugContinue() => _engine.Continue();
public void DebugStop() => _engine.Cancel();
```

## 4. 数据流完整路径

```
用户双击 Table 第5行
  → _table.CellDoubleClick 触发
  → MainForm 获取 TestItem[4]
  → DebugControlForm.SetTargetRow(4, item) 被调用
  → DebugForm 面板显示 "目标行: #5 - VoltageTest"

用户点击菜单 "运行到指定行"
  → DebugForm 调用 MainForm.DebugRunTo(4)
  → Engine 设置 _targetRow=4, _debugMode=RunToMode
  → Engine 若处于暂停状态则放行
  → Engine 循环继续执行直到 i >= 4
  → Engine 暂停，触发 DebugPaused 事件
  → DebugForm 显示 "状态: 已到达目标行，暂停"
  → 等待用户操作

用户点击 "执行下一步" 或 "继续运行"
  → 对应放行操作，引擎继续
```

## 5. 错误/边界情况处理

| 场景 | 行为 |
|---|---|
| 引擎未运行时点击调试命令 | 弹出提示或按钮置灰 |
| 双击表格时引擎正在运行 | 允许设置目标行，但不中断当前执行 |
| 未设置目标行就点"运行到指定行" | 提示"请先在主表格中双击选择目标行" |
| 设的目标行已经过了当前位置 | `RunTo()` 中检查，自动拒绝并记录日志警告 |
| 关闭调试窗体时引擎在运行 | 不影响引擎，下次打开重新同步状态 |
| 目标行索引超出范围（如删除后） | 清除目标行设置，恢复到 Normal 模式 |
| 调试窗体已打开再次从菜单点击 | 把已存在的窗体 BringToFront |
| 引擎跑完后自动复位 | DebugMode → Normal, 控制面板显示"测试完成" |

## 6. 注意事项

- `TaskCompletionSource` 必须确保在 UI 线程配置同步上下文，避免死锁
- DebugControlForm 需要持有 MainForm 的弱引用，避免阻止 GC
- 所有引擎事件回调中如果涉及 UI 更新，需要使用 `BeginInvoke`（已存在于 MainForm 模式中）
- DebugControlForm 样式跟随 `AntdUI.Config.IsDark`，通过 `Config.IsDarkChanged` 事件同步
- **双击行检测**：如 ANTDUI `Table` 没有直接提供 `CellDoubleClick` 事件，则使用 `MouseDoubleClick` + 坐标命中检测来确定具体行号。在实现阶段确认 API 可用性
