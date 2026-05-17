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
