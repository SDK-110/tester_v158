using AntdUI;
using System;
using System.Drawing;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;
using Panel = System.Windows.Forms.Panel;
using Message = System.Windows.Forms.Message;

namespace test_antdui
{
    public class DebugControlForm : AntdUI.Window
    {
        private MainForm _mainForm;
        private TestEngine _engine;

        // 标题栏
        private AntdUI.PageHeader _windowBar;
        // 左侧菜单
        private AntdUI.Menu _menu;
        // 右侧面板
        private Panel _contentPanel;

        // 状态显示
        private Label _lblMode;
        private Label _lblStatus;
        private Label _lblCurrentItem;
        private AntdUI.Progress _progress;

        // 目标行输入
        private Label _lblTargetLabel;
        private NumericUpDown _numTargetRow;
        private int _targetRow = -1;

        // 按钮
        private AntdUI.Button _btnStepNext;
        private AntdUI.Button _btnContinue;
        private AntdUI.Button _btnStop;

        // 单例
        private static DebugControlForm _instance;
        public static DebugControlForm Instance => _instance;

        public static DebugControlForm GetInstance(MainForm mainForm)
        {
            if (_instance == null || _instance.IsDisposed)
                _instance = new DebugControlForm(mainForm);
            else
                _instance.BringToFront();
            return _instance;
        }

        public DebugControlForm(MainForm mainForm)
        {
            if (_instance != null && !_instance.IsDisposed)
            {
                _instance.Close();
            }
            _instance = this;

            _mainForm = mainForm;
            _engine = GetEngineFromMainForm();

            // 窗体设置
            Text = "调试控制";
            Size = new Size(480, 560);
            MinimumSize = new Size(420, 400);
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;

            // 默认显示在屏幕中央，不跟随主窗体

            InitializeControls();
            SubscribeEngineEvents();
            UpdateUIState();

            // 跟随主题
            if (AntdUI.Config.IsDark)
                BackColor = Color.FromArgb(24, 24, 24);
        }

        private TestEngine GetEngineFromMainForm()
        {
            return _mainForm?.GetEngine();
        }

        private void InitializeControls()
        {
            // ═══ 标题栏 ═══════════════════════════════════
            _windowBar = new AntdUI.PageHeader
            {
                Dock = DockStyle.Top,
                Text = "调试控制",
                ShowButton = true,
                Height = 32
            };
            Controls.Add(_windowBar);

            // ═══ 左侧菜单 ════════════════════════════════
            _menu = new AntdUI.Menu
            {
                Dock = DockStyle.Left,
                Width = 52,
                Collapsed = true,
                BackColor = Color.FromArgb(0, 21, 41),
                AutoSize = false
            };

            _menu.Items.Add(new AntdUI.MenuItem { Text = "单步执行", IconSvg = "StepForwardOutlined" });
            _menu.Items.Add(new AntdUI.MenuItem { Text = "运行到指定行", IconSvg = "CaretRightOutlined" });
            _menu.Items.Add(new AntdUI.MenuItem { Text = "继续运行", IconSvg = "PlayCircleOutlined" });
            _menu.Items.Add(new AntdUI.MenuItem { Text = "停止", IconSvg = "CloseCircleOutlined" });

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
                Width = _contentPanel.Width - 48,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
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

            // 目标行输入（NumericUpDown 直接输入行号）
            _lblTargetLabel = new Label
            {
                Text = "目标行：",
                Location = new Point(32, _lblCurrentItem.Bottom + 8),
                AutoSize = true,
                ForeColor = Color.FromArgb(200, 200, 200)
            };
            _contentPanel.Controls.Add(_lblTargetLabel);

            _numTargetRow = new NumericUpDown
            {
                Location = new Point(92, _lblCurrentItem.Bottom + 5),
                Width = 70,
                Minimum = 1,
                Maximum = 9999,
                Value = 1,
                TextAlign = HorizontalAlignment.Center
            };
            _contentPanel.Controls.Add(_numTargetRow);

            var lblTargetHint = new Label
            {
                Text = "(输入行号后点菜单\"运行到指定行\")",
                Location = new Point(168, _lblCurrentItem.Bottom + 8),
                AutoSize = true,
                ForeColor = Color.FromArgb(140, 140, 140),
                Font = new Font("Microsoft YaHei UI", 9)
            };
            _contentPanel.Controls.Add(lblTargetHint);

            // 进度条
            var lblProgress = new Label { Text = "进度", Font = new Font("Microsoft YaHei UI", 10, FontStyle.Bold), Location = new Point(16, _lblTargetLabel.Bottom + 16), AutoSize = true };
            _contentPanel.Controls.Add(lblProgress);

            _progress = new AntdUI.Progress
            {
                Location = new Point(32, lblProgress.Bottom + 8),
                Width = _contentPanel.Width - 80,
                Value = 0,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
            };
            _contentPanel.Controls.Add(_progress);

            // 分隔线
            var divider2 = new AntdUI.Divider
            {
                Location = new Point(16, _progress.Bottom + 12),
                Width = _contentPanel.Width - 48,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
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
                    if (_engine == null)
                    {
                        AntdUI.Message.info(this, "引擎未初始化");
                        return;
                    }
                    _engine.StepNext();
                    _lblStatus.Text = "状态：单步模式已激活";
                    break;

                case "运行到指定行":
                    if (_engine == null)
                    {
                        AntdUI.Message.info(this, "引擎未初始化");
                        return;
                    }
                    // 从输入框获取目标行
                    ApplyTargetRow();
                    if (_targetRow < 0)
                    {
                        AntdUI.Message.warn(this, "请在输入框中输入有效的目标行号");
                        return;
                    }
                    if (_engine.IsRunning && _targetRow <= _engine.CurrentIndex)
                    {
                        AntdUI.Message.warn(this, $"目标行 #{_targetRow + 1} 已过当前位置 (#{_engine.CurrentIndex + 1})");
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
            _numTargetRow.Value = Math.Max(1, rowIndex + 1);
        }

        private void ApplyTargetRow()
        {
            _targetRow = (int)_numTargetRow.Value - 1;
        }

        public void UpdateTestProgress(int current, int total)
        {
            if (total > 0)
                _progress.Value = (float)((double)current / total);
        }

        public void UpdateUIState()
        {
            bool isRunning = _engine?.IsRunning ?? false;
            bool isPaused = _engine != null && _engine.CurrentDebugMode != DebugMode.Normal;
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
            _numTargetRow.Value = 1;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_engine != null)
                _engine.DebugPaused -= OnEngineDebugPaused;
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
