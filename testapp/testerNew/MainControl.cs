using AntdUI;
using IniParser.Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using PCHMI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp;
using testapp.glob_set;
using static AntdUI.Modal;
using Column = AntdUI.Column;
using Label = AntdUI.Label;
using Message = System.Windows.Forms.Message;
using Panel = System.Windows.Forms.Panel;

namespace test_antdui
{

    public partial class MainForm : AntdUI.Window
    {

        IniParser.FileIniDataParser iniread;
        IniParser.Model.IniData inidata;
        private TestProject _project;
        private TestEngine _engine;
        private string _currentExcelPath;
        private string _sn = "NA";
        private string _orderNo = "";
        private string _operatorNo = "";
        private TestLoggerForm _logForm;
        private DebugControlForm _debugForm;
        private RichTextBox _logTextBox;
        private CheckBox _chkAutoScroll;

        private object _lock = new object();
        private static testcase_dll testcase_lib;

        // ═══ Chart & Production ══════════════════════════
        private int _selectedHour = -1;
        private Timer _statsTimer;
        private ProductionTracker _tracker;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SN
        {
            get => _sn;
            set { _sn = value;}
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OrderNo
        {
            get => _orderNo;
            set { _orderNo = value; TestConfigManager.UpdateOrderNo(_orderNo); }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string OperatorNo
        {
            get => _operatorNo;
            set { _operatorNo = value; TestConfigManager.UpdateOperatorNo(_operatorNo); }
        }

        public MainForm()
        {
            InitializeComponent();
          

            // 图表暗色主题
            _chartPanel.GridColor = Color.FromArgb(60, 60, 60);
            _chartPanel.AxisColor = Color.FromArgb(60, 60, 60);
            _chartPanel.YMax = null;        // 自动缩放Y轴
            _chartPanel.YMin = 0;
            _chartPanel.ShowLegend = false; // 图例占用~70px，当前高度115px不够，禁用后数据区才有空间
            _chartPanel.ShowXAxisLabels = true;
            _chartPanel.XAxisLabelFormat = "{0}:00";
            _chartPanel.ShowYAxisLabels = true;

            _tracker = ProductionTracker.Instance;

            ApplyLanguage();

            // 从 TestConfig 加载操作员
            var config = TestConfigManager.Instance;
            _operatorNo = config.LastOperatorNo;
            if (!string.IsNullOrEmpty(_operatorNo))
            {
                _tracker.OperatorName = _operatorNo;
                _lblOpId.Text = AppStrings.Get("op_id", _operatorNo);
            }

            UpdateAllStats();
            InitChartData();
            InitializeTestEngine();
            SetupTable();
            AddDebugMenuItem();
            _table.MouseDoubleClick += Table_MouseDoubleClick;
            CreateLogForm2();
            LoadConfig();
            AutoLoadTemplate();
            AntdUI.Config.IsDark = true;

            inidata = glob_ini_instance.getInstance().getSetupIniData;
            iniread = glob_ini_instance.getInstance().fileIni;

            // 串口扫描器
            InitScanner();
            InitDiMonitor();
            windowBar.SubText = inidata["setproduct"]["project"];
            // 定时刷新
            _statsTimer = new Timer { Interval = 1000 };
            _statsTimer.Tick += (s, e) => { UpdateClock(); };
            _statsTimer.Start();

            UpdateClock();
        }

        // ═══ Stats Update ════════════════════════════════

        private void UpdateAllStats()
        {
            UpdateTotalStats();
            UpdateHourlyStats(_selectedHour >= 0 ? _selectedHour : DateTime.Now.Hour);
            UpdateClock();
        }

        private void UpdateTotalStats()
        {
            _lblTotalCount.Text = _tracker.TotalCount.ToString();
            _lblTotalPass.Text = _tracker.TotalPass.ToString();
            _lblTotalFail.Text = _tracker.TotalFail.ToString();
            _lblTotalYield.Text = $"{_tracker.TotalYield}%";

            int w = _barTotalBg.ClientSize.Width;
            if (w > 0 && _tracker.TotalCount > 0)
            {
                int pw = (int)(w * _tracker.TotalYield / 100.0);
                _barTotalPass.Width = Math.Max(pw, 0);
            }
        }

        private void UpdateHourlyStats(int hour)
        {
            int pass = _tracker.GetHourlyPass(hour);
            int fail = _tracker.GetHourlyFail(hour);
            int total = pass + fail;
            double yield = total > 0 ? Math.Round((double)pass / total * 100, 1) : 0;

            _lblHourTitle.Text = AppStrings.Get("hour_title", hour, hour == 23 ? 0 : hour + 1);
            _lblHourCount.Text = total.ToString();
            _lblHourPass.Text = pass.ToString();
            _lblHourFail.Text = fail.ToString();
            _lblHourYield.Text = $"{yield}%";

            int w = _barHourBg.ClientSize.Width;
            if (w > 0 && total > 0)
            {
                int pw = (int)(w * (double)pass / total);
                int fw = (int)(w * (double)fail / total);
                _barHourPass.Width = Math.Max(pw, 0);
                _barHourFail.Width = Math.Max(fw, 0);
                _barHourFail.Location = new Point(_barHourPass.Width, 0);
            }
        }

        private void UpdateClock()
        {
            var now = DateTime.Now;
            _lblOpTime.Text = now.ToString("HH:mm:ss");
            _lblStatusRight.Text = $"{now:yyyy-MM-dd HH:mm:ss}";
            _lblOpShift.Text = _tracker.IsDayShift ? AppStrings.Get("day_shift") : AppStrings.Get("night_shift");

            string emp = string.IsNullOrEmpty(_operatorNo) ? AppStrings.Get("op_not_logged") : _operatorNo;
            _lblStatusLeft.Text = AppStrings.Get("status_line", emp,
                _tracker.TotalCount, _tracker.TotalPass, _tracker.TotalFail, _tracker.CurrentHourTotal);
        }

        // ═══ Localization ═══════════════════════════════

        private void ApplyLanguage()
        {
            // Menu items are set in Designer - update runtime strings
            _lblOpId.Text = string.IsNullOrEmpty(_operatorNo)
                ? AppStrings.Get("op_not_logged")
                : AppStrings.Get("op_id", _operatorNo);
            _lblOpShift.Text = _tracker.IsDayShift ? AppStrings.Get("day_shift") : AppStrings.Get("night_shift");

            // Refresh tables and stats
            SetupTable();
            UpdateAllStats();
            UpdateClock();

            // Alert
            _alertStatus.TextTitle = AppStrings.Get("alert_ready");
            _alertStatus.Text = AppStrings.Get("alert_ready_sn", _sn);

            // Chart
            _chartPanel.Title = AppStrings.Get("chart_title");

            // Status bar
            _lblStatusLeft.Text = AppStrings.Get("status_ready");
        }

        // ═══ Alert Status ═══════════════════════════════

        private void SetAlertSuccess(string text)
        {
            _alertStatus.Icon = TType.Success;
            _alertStatus.TextTitle = AppStrings.Get("alert_pass");
            _alertStatus.Text = text;
        }

        private void SetAlertError(string text)
        {
            _alertStatus.Icon = TType.Error;
            _alertStatus.TextTitle = AppStrings.Get("alert_fail");
            _alertStatus.Text = text;
        }

        private void SetAlertWarning(string text)
        {
            _alertStatus.Icon = TType.Warn;
            _alertStatus.TextTitle = AppStrings.Get("alert_testing");
            _alertStatus.Text = text;
        }

        // ═══ Chart ═══════════════════════════════════════

        private void InitChartData()
        {
            _selectedHour = -1;
            RefreshChart();
        }

        private void RefreshChart()
        {
            var passData = _tracker.GetHourlyPassArray();
            var failData = _tracker.GetHourlyFailArray();

            _chartPanel.ClearDatasets();

            var failDs = new ChartDataset("FAIL", Color.FromArgb(239, 83, 80))
            {
                BorderColor = Color.FromArgb(239, 83, 80),
                BorderWidth = 3
            };
            var passDs = new ChartDataset("PASS", Color.FromArgb(102, 187, 106))
            {
                BorderColor = Color.FromArgb(102, 187, 106),
                BorderWidth = 3
            };

            for (int i = 0; i < 24; i++)
            {
                failDs.AddPoint($"{i:D2}", i, failData[i]);
                passDs.AddPoint($"{i:D2}", i, passData[i]);
            }

            _chartPanel.Datasets.Add(failDs);
            _chartPanel.Datasets.Add(passDs);
            _chartPanel.RefreshChart();
        }

        private void Chart_PointClick(object sender, ChartPointClickEventArgs e)
        {
            int hour = e.PointIndex;
            if (hour >= 0 && hour < 24)
            {
                _selectedHour = (_selectedHour == hour) ? -1 : hour;
                UpdateHourlyStats(_selectedHour >= 0 ? _selectedHour : DateTime.Now.Hour);
            }
        }

        // ═══ Barcode ═════════════════════════════════════

        private void BarcodeInput_TextChanged(object sender, EventArgs e)
        {
            string text = _barcodeInput.Text.Trim();
            _lblBarcodeStatus.Text = GetBarcodeStatusText(text) + "==> " + text;
            SN = text;
        }

        private void BarcodeInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barcode = _barcodeInput.Text.Trim();
                if (string.IsNullOrEmpty(barcode)) return;
              
               
                if (_tracker.ValidateBarcode(barcode))
                {
                    _sn = barcode;
                   // AntdUI.Message.success(this, AppStrings.Get("sn_entered", barcode));
                    AddLog(AppStrings.Get("barcode_scanned", barcode), Color.FromArgb(79, 195, 247));

                    _barcodeInput.Enabled = false;
                    _btnStart.PerformClick();
                }
                else
                {
                    AntdUI.Message.error(this, AppStrings.Get("barcode_invalid"), autoClose: 2);
                    _barcodeInput.Focus();
                    _barcodeInput.SelectAll();
                }
            }
        }

        private string GetBarcodeStatusText(string text)
        {
            string regex = _tracker.BarcodeRegex;
            string regexDisplay = "";

            if (string.IsNullOrEmpty(text))
                return AppStrings.Get("barcode_status", regexDisplay, AppStrings.Get("barcode_waiting"));

            if (_tracker.ValidateBarcode(text))
                return AppStrings.Get("barcode_status", regexDisplay, AppStrings.Get("barcode_match"));
            else
                return AppStrings.Get("barcode_status", regexDisplay, AppStrings.Get("barcode_no_match"));
        }

        // ═══ Mode Toggle ═════════════════════════════════

        private void BtnMode_Click(object sender, EventArgs e)
        {
            AntdUI.Config.IsDark = !AntdUI.Config.IsDark;
        }

        // ═══ Menu Auto-Expand ════════════════════════════

        private void Menu1_MouseEnter(object sender, EventArgs e)
        {
            menu1.Collapsed = false;
            menu1.Width = 160;
        }

        private void Menu1_MouseLeave(object sender, EventArgs e)
        {
            menu1.Collapsed = true;
            menu1.Width = 52;
        }

        private void Menu1_SelectChanged(object sender, AntdUI.MenuSelectEventArgs e)
        {
            var item = e.Value;
            if (item == null) return;

            switch (item.Text)
            {
                case "Settings":
                    using (var cfg = new ConfigForm())
                    cfg.ShowDialog(this);
                    UpdateAllStats();
                    _tracker.Reload();
                    InitChartData();
                    break;

                case "Production":
                    UpdateHourlyStats(DateTime.Now.Hour);
                    AntdUI.Message.info(this,
                        AppStrings.Get("total_vs_hourly",
                            _tracker.TotalCount, _tracker.TotalPass, _tracker.TotalFail,
                            _tracker.CurrentHourTotal, _tracker.CurrentHourPass, _tracker.CurrentHourFail));
                    break;

                case "Clear Data":
                    using (var cfg = new ConfigForm())
                        cfg.ShowDialog(this);
                    UpdateAllStats();
                    InitChartData();
                    break;

                case "Switch Shift":
                    _tracker.ToggleShift();
                    UpdateClock();
                    AntdUI.Message.success(this, AppStrings.Get("menu_shift_switched", _tracker.CurrentShift));
                    break;

                case "Help":
                    AntdUI.Message.info(this, AppStrings.Get("menu_help_text"));
                    break;

                case "Lang: EN":
                case "Lang: 中":
                    AppStrings.Toggle();
                    foreach (AntdUI.MenuItem mi in menu1.Items)
                    {
                        if (mi.IconSvg == "GlobalOutlined")
                        {
                            mi.Text = AppStrings.IsZh ? "Lang: 中" : "Lang: EN";
                            break;
                        }
                    }
                    ApplyLanguage();
                    break;

                case "SK_Relay":
                    {
                        sk_relay32 form4 = sk_relay32.get_instaance();
                        form4.set_main_ptr(this.Handle);
                        form4.Show();

                    }
                    // TODO: implement SK_Relay
                    break;
                case "sevy_relay":
                    {
                        relay_debug_4 form4 = relay_debug_4.get_instance();
                        form4.set_main_win_ptr(this.Handle);
                        form4.Show();

                    }
                    // TODO: implement sevy_relay
                    break;
                case "Debug Control":
                    OpenDebugControl();
                    break;
            }
        }

        // ═══ Existing Methods (preserved) ════════════════

        internal TestEngine GetEngine() => _engine;

        public void destroy()
        {
            _statsTimer?.Stop();
            _statsTimer?.Dispose();
            _scanner?.Dispose();
            _diMonitor?.Dispose();
            if (_logForm != null) _logForm.Dispose();
            _debugForm?.Close();
            _debugForm?.Dispose();
        }

        private void CreateLogForm2()
        {
            _logForm = TestLoggerForm.Instance;
            _logForm.Show();

            Task.Factory.StartNew(new Action(() =>
            {
                testcase_lib = new testcase_dll();
                foreach (var kvp in testcase_lib.Getfun())
                {
                    _engine.RegisterFunction(kvp.Key, kvp.Value);
                }
            }));
        }

        private void Table_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_project == null) return;

            int rowHeight = _table.RowHeight > 0 ? _table.RowHeight : 36;
            int headerHeight = 32;
            int clickedY = e.Y - headerHeight;
            if (clickedY < 0) return;

            int estimatedRow = clickedY / rowHeight;
            if (estimatedRow >= 0 && estimatedRow < _project.Items.Count)
            {
                var item = _project.Items[estimatedRow];
                DebugControlForm.GetInstance(this).SetTargetRow(estimatedRow, item);
                AntdUI.Message.success(this, $"已设置目标行：#{estimatedRow + 1} - {item.Name}", autoClose: 2);
            }
        }

        private void AddDebugMenuItem()
        {
            menu1.Items.Add(new AntdUI.MenuItem
            {
                Text = "Debug Control",
                IconSvg = "ControlOutlined"
            });
        }

        private void OpenDebugControl()
        {
            var form = DebugControlForm.GetInstance(this);
            if (_debugForm == null || _debugForm.IsDisposed)
            {
                _debugForm = form;
            }
            form.Show();
            form.BringToFront();
        }

        private void AddLog(string message, Color? color = null)
        {
            _logForm.AddLog(message, color);
        }

        private void SetupTable()
        {
            _table.Columns = new ColumnCollection
            {
                new Column("Id", AppStrings.Get("col_id")),
                new Column("Name", AppStrings.Get("col_name")),
                new Column("LowLimit", AppStrings.Get("col_low")).SetColAlign(),
                new Column("HighLimit", AppStrings.Get("col_high")).SetColAlign(),
                new Column("MeasuredValue", AppStrings.Get("col_value")).SetColAlign(),
                new Column("StateBadge", AppStrings.Get("col_result")).SetColAlign(),
                new Column("Duration", AppStrings.Get("col_duration")).SetColAlign(),
            };
            _table.RowSelectedBg = Color.FromArgb(24, 144, 255);
            _table.RowSelectedFore = Color.White;
        }

        private void InitializeTestEngine()
        {
            _project = new TestProject();
            _engine = new TestEngine(_project.Items);

            _engine.LogMessage += OnEngineLogMessage;
            _engine.TestItemStarted += OnTestItemStarted;
            _engine.TestItemCompleted += OnTestItemCompleted;
            _engine.TestCompleted += OnTestCompleted;

            RegisterTestFunctions();
        }

        private void RegisterTestFunctions()
        {
            _engine.RegisterFunction("VoltageTest", VoltageTest);
            _engine.RegisterFunction("CurrentTest", CurrentTest);
            _engine.RegisterFunction("ResistanceTest", ResistanceTest);
            _engine.RegisterFunction("ShortCircuitTest", ShortCircuitTest);
            _engine.RegisterFunction("OpenCircuitTest", OpenCircuitTest);
            _engine.RegisterFunction("FlashTest", FlashTest);
            _engine.RegisterFunction("InsulationTest", InsulationTest);

            _engine.RegisterInitFunction(InitHandler);
            _engine.RegisterCleanupFunction(CleanupHandler);
        }

        private string VoltageTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog(AppStrings.Get("test_voltage", low, high));
            System.Threading.Thread.Sleep(20);

            if (!float.TryParse(high, out float h) || !float.TryParse(low, out float l))
            {
                rst = AppStrings.Get("param_error");
                return "FAIL";
            }

            float measured = l + (float)(new Random().NextDouble() * (h - l));
            bool pass = measured >= l && measured <= h;

            TestLoggerForm.Instance.AddLog(AppStrings.Get("test_voltage_result", measured, pass ? "PASS" : "FAIL"));
            rst = measured + "";

            return pass ? $"PASS" : $"FAIL";
        }

        private string CurrentTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog($"  测量电流: 范围 {low}A ~ {high}A");
            System.Threading.Thread.Sleep(20);

            if (!float.TryParse(high, out float h) || !float.TryParse(low, out float l))
            {
                rst = "参数解析错误";
                return "FAIL";
            }

            float measured = l + (float)(new Random().NextDouble() * (h - l));
            bool pass = measured >= l && measured <= h;

            TestLoggerForm.Instance.AddLog($"  实测值: {measured:F4}A, 判定: {(pass ? "PASS" : "FAIL")}");
            rst = measured + "";
            return pass ? $"PASS" : $"FAIL";
        }

        private string ResistanceTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog($"  测量电阻: 范围 {low}Ω ~ {high}Ω");
            System.Threading.Thread.Sleep(20);

            if (!float.TryParse(high, out float h) || !float.TryParse(low, out float l))
            {
                rst = "解析错误";
                return "FAIL";
            }

            float measured = l + (float)(new Random().NextDouble() * (h - l));
            bool pass = measured >= l && measured <= h;
            rst = measured + "";
            TestLoggerForm.Instance.AddLog($"  实测值: {measured:F2}Ω, 判定: {(pass ? "PASS" : "FAIL")}");
            return pass ? $"PASS" : $"FAIL";
        }

        private string ShortCircuitTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("  执行短路测试...");
            System.Threading.Thread.Sleep(20);
            int result = new Random().Next(0, 2);
            bool pass = result == 1;
            rst = $"{(pass ? "正常" : "短路")}";
            TestLoggerForm.Instance.AddLog(rst);
            return pass ? "PASS,正常" : "FAIL,短路";
        }

        private string OpenCircuitTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("  执行开路测试...");
            System.Threading.Thread.Sleep(20);
            int result = new Random().Next(0, 2);
            bool pass = result == 1;
            rst = $"{(pass ? "正常" : "开路")}";
            TestLoggerForm.Instance.AddLog(rst);
            return pass ? "PASS" : "FAIL";
        }

        private string FlashTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("  执行Flash写入测试...");
            System.Threading.Thread.Sleep(20);
            int result = new Random().Next(0, 2);
            bool pass = result == 1;
            rst = $"{(pass ? "成功" : "失败")}";
            TestLoggerForm.Instance.AddLog(rst);
            return pass ? "PASS" : "FAIL";
        }

        private string InsulationTest(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("  执行绝缘测试...");
            System.Threading.Thread.Sleep(20);
            float measured = (float)(new Random().NextDouble() * 500 + 100);
            bool pass = measured > 100;
            rst = $"{measured:F1}MΩ";
            TestLoggerForm.Instance.AddLog(rst);
            return pass ? $"PASS" : $"FAIL";
        }

        private string InitHandler(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("初始化: 开始...");
            System.Threading.Thread.Sleep(20);
            TestLoggerForm.Instance.AddLog("初始化: 完成");
            rst = "PASS";
            return "PASS";
        }

        private string CleanupHandler(string high, string low, out string rst, string parameter)
        {
            TestLoggerForm.Instance.AddLog("清理: 开始...");
            System.Threading.Thread.Sleep(20);
            TestLoggerForm.Instance.AddLog("清理: 完成");
            rst = "PASS";
            return "PASS";
        }

        private void OnEngineLogMessage(object sender, LogEventArgs e)
        {
            Color logColor = Color.White;
            string msg = e.Message.ToUpper();
            if (msg.Contains("FAIL") || msg.Contains("ERROR"))
                logColor = Color.FromArgb(245, 34, 45);
            else if (msg.Contains("PASS"))
                logColor = Color.FromArgb(82, 196, 26);
            else if (msg.Contains("WARN"))
                logColor = Color.FromArgb(250, 173, 20);

            AddLog(e.Message, logColor);
        }

        private void OnTestItemStarted(object sender, TestEngineEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnTestItemStarted(sender, e)));
                return;
            }

            if (e.Item != null)
            {
                e.Item.StateBadge = new CellBadge(TState.Default, AppStrings.Get("state_running"));
                e.Item.BackColor = Color.FromArgb(24, 144, 255);
                RefreshTableData();
                _table.SelectedIndex = e.Index + 1;
                _table.ScrollLine(e.Index + 1);
                _table.Focus();
                Application.DoEvents();
            }
            UpdateProgress(e.Index + 1, e.Total);
            if (_debugForm != null && !_debugForm.IsDisposed)
                _debugForm.UpdateTestProgress(e.Index + 1, e.Total);
        }

        private void OnTestItemCompleted(object sender, TestEngineEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnTestItemCompleted(sender, e)));
                return;
            }

            if (e.Item != null)
            {
                string displayText = e.Item.MeasuredValue;

                if (e.Item.State == TestState.Pass)
                    displayText = "PASS";
                else if (e.Item.State == TestState.Fail)
                    displayText = "FAIL";
                else if (e.Item.State == TestState.Skipped)
                    displayText = AppStrings.Get("state_skip");

                TState badgeState;
                Color backColor = Color.Transparent;
                if (e.Item.State == TestState.Pass)
                {
                    badgeState = TState.Success;
                    backColor = Color.FromArgb(43, 84, 44);
                }
                else if (e.Item.State == TestState.Fail)
                {
                    badgeState = TState.Error;
                    backColor = Color.FromArgb(84, 29, 29);
                }
                else if (e.Item.State == TestState.Skipped)
                {
                    badgeState = TState.Warn;
                    backColor = Color.FromArgb(60, 60, 60);
                }
                else
                {
                    badgeState = TState.Default;
                }

                e.Item.StateBadge = new CellBadge(badgeState, displayText);
                e.Item.BackColor = backColor;

                RefreshTableData();
                _table.ScrollLine(e.Index + 1);
                _table.SelectedIndex = e.Index + 1;
                _table.Focus();
                Application.DoEvents();
            }
            UpdateCurrentTestStats();
        }

        private void OnTestCompleted(object sender, TestEngineEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnTestCompleted(sender, e)));
                return;
            }

            int passCount = 0, failCount = 0;
            foreach (var item in _project.Items)
            {
                if (item.State == TestState.Pass) passCount++;
                else if (item.State == TestState.Fail) failCount++;
            }

            bool allPass = failCount == 0;

            _btnStart.Enabled = true;
            _btnStart.Loading = false;
           
            _barcodeInput.Enabled = true;
            if (allPass)
            {
                AddLog($"+++ SN {_sn} PASS +++", Color.FromArgb(82, 196, 26));
                AddLog(AppStrings.Get("log_all_pass"), Color.FromArgb(82, 196, 26));
                iconState.State = TType.Success;
                tBadge7.State = TState.Success;
                _tracker.RecordPass();
              //  _tracker.RecordSnResult(_sn, true);
                SetAlertSuccess(AppStrings.Get("alert_pass_sn", _sn));
            }
            else
            {
                AddLog($"--- SN {_sn} FAIL ({failCount}) ---", Color.FromArgb(245, 34, 45));
                AddLog(AppStrings.Get("log_fail", failCount), Color.FromArgb(245, 34, 45));
                iconState.State = TType.Error;
                tBadge7.State = TState.Error;
                _tracker.RecordFail();
               // _tracker.RecordSnResult(_sn, false);
                SetAlertError(AppStrings.Get("alert_fail_sn", _sn, failCount));
            }

            _sn2 = _sn;
            _barcodeInput.Text = "";
            TestConfigManager.UpdateAfterTest(allPass);
            UpdateStatistics();
            UpdateAllStats();
            InitChartData();
            SaveReportIfNeeded(allPass);
            _barcodeInput.Focus();
            if (_debugForm != null && !_debugForm.IsDisposed)
                _debugForm.ResetState();
        }

        void RefreshTableData()
        {
            _table.DataSource = null;
            _table.DataSource = _project.Items;
        }

        void UpdateStatistics()
        {
            var config = TestConfigManager.Instance;
            // Stats now shown via production tracker
        }

        void UpdateCurrentTestStats()
        {
            int pass = 0, fail = 0, total = _project.Items.Count;
            foreach (var item in _project.Items)
            {
                if (item.State == TestState.Pass) pass++;
                else if (item.State == TestState.Fail) fail++;
            }
        }

        void UpdateProgress(int current, int total)
        {
            if (total > 0)
            {
                progress3.Value = (float)((double)current / total);
            }
        }

        private async void _btnStart_Click(object sender, EventArgs e)
        {
            _testStartTime = DateTime.Now;
            // 自动生成SN
            if (string.IsNullOrEmpty(_sn) || _sn == "NA")
                _sn = $"SN-{DateTime.Now:yyyyMMdd-HHmmss}";
            if (!_tracker.ValidateBarcode(_sn)) return;
            
                if (!log_append.Checked) TestLoggerForm.Instance.ClearLog();
            TestLoggerForm.SetLogPrefix(_sn);
            AddLog(AppStrings.Get("log_test_start"));
            AddLog($">>> SN: {_sn} <<<", Color.FromArgb(79, 195, 247));
            iconState.State = TType.Warn;
            tBadge7.State = TState.Processing;
            SetAlertWarning(AppStrings.Get("alert_testing_sn", _sn));
            if (_project.Items.Count == 0)
            {
                AddLog(AppStrings.Get("log_no_template"), Color.Red);
                MessageBox.Show(AppStrings.Get("dlg_no_template"), AppStrings.Get("dlg_confirm"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _btnStart.Enabled = false;
            _btnStart.Loading = true;
            progress3.Value = 0;

            _project.ResetAllBadges();
            RefreshTableData();

            AddLog(AppStrings.Get("log_items", _project.Items.Count));

            bool initOk = await _engine.RunInitAsync();
            if (!initOk)
            {
                AddLog(AppStrings.Get("log_init_fail"), Color.Red);
                _btnStart.Enabled = true;
                _btnStart.Loading = false;
                return;
            }

            AddLog(AppStrings.Get("log_test_steps"));
            await _engine.RunAsync();
            await _engine.RunCleanupAsync();
            AddLog(AppStrings.Get("log_test_complete"));
        }

        private void _btnLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = AppStrings.Get("dlg_load_filter");
                dialog.Title = AppStrings.Get("dlg_load_title");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    LoadFromExcel(dialog.FileName);
                }
            }
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = AppStrings.Get("dlg_save_filter");
                dialog.Title = AppStrings.Get("dlg_save_title");
                dialog.FileName = string.Format(AppStrings.Get("dlg_save_filename"), DateTime.Now);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ExcelTestLoader.SaveToExcel(_project, dialog.FileName);
                    MessageBox.Show(AppStrings.Get("dlg_save_ok"), AppStrings.Get("dlg_confirm"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_logForm != null && !_logForm.IsDisposed)
            {
                _logForm.Visible = !_logForm.Visible;
            }
        }

        private void ChkSaveExcel_CheckedChanged(object sender, EventArgs e)
        {
            int method = 0;
            if (chkSaveExcel.Checked) method |= 1;
            if (chkSaveAppend.Checked) method |= 2;
            TestConfigManager.UpdateSaveMethod(method);
        }

        private void LoadFromExcel(string filePath)
        {
            try
            {
                var loadedProject = ExcelTestLoader.LoadFromExcel(filePath);
                _project.Items.Clear();
                foreach (var item in loadedProject.Items)
                {
                    _project.Items.Add(item);
                }
                _currentExcelPath = filePath;
                TestConfigManager.UpdateLastExcelPath(filePath);
                RefreshTableData();
                AddLog(AppStrings.Get("log_template_loaded", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                AddLog(AppStrings.Get("dlg_load_fail", ex.Message), Color.Red);
                MessageBox.Show(AppStrings.Get("dlg_load_fail", ex.Message), AppStrings.Get("dlg_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadConfig()
        {
            var config = TestConfigManager.Instance;
            _orderNo = config.LastOrderNo;
            _operatorNo = config.LastOperatorNo;

            chkSaveExcel.Checked = (config.SaveMethod & 1) == 1;
            chkSaveAppend.Checked = (config.SaveMethod & 2) == 2;
            chkStopOnFail.Checked = config.StopOnFail == 1;

            if (!string.IsNullOrEmpty(config.LastExcelPath) && File.Exists(config.LastExcelPath))
            {
                LoadFromExcel(config.LastExcelPath);
            }
        }

        void AutoLoadTemplate()
        {
            string templatePath = ExcelTestLoader.DefaultTemplatePath;
            if (!File.Exists(templatePath))
            {
                ExcelTestLoader.CreateSampleTemplate(templatePath);
            }
            if (string.IsNullOrEmpty(TestConfigManager.Instance.LastExcelPath) || !File.Exists(TestConfigManager.Instance.LastExcelPath))
            {
                LoadFromExcel(templatePath);
            }
        }

        private void SaveReportIfNeeded(bool allPass)
        {
            var config = TestConfigManager.Instance;

            try
            {
                string sn = string.IsNullOrEmpty(_sn2) ? "NA" : _sn2;
                string orderNo = _orderNo;
                string operatorNo = _operatorNo;

                // Save Excel
                if (config.SaveMethod != 0)
                {
                    var mode = (SaveMode)config.SaveMethod;
                    TestReportSaver.SaveTestReport(_project, sn, orderNo, operatorNo, mode);
                }

                // Save CSV (SMX_LOG format)
                string lineNumber = inidata?["setproduct"]?["line_number"] ?? "N/A";
                string workStation = inidata?["setproduct"]?["work_station"] ?? "N/A";
                string startTime = _testStartTime.ToString("HH:mm:ss:ffff");
                string endTime = DateTime.Now.ToString("HH:mm:ss:ffff");
                TestReportSaver.SaveToCsv(_project, sn, operatorNo, lineNumber, workStation, startTime, endTime, allPass);

                AddLog(AppStrings.Get("log_report_saved"), Color.FromArgb(82, 196, 26));
            }
            catch (Exception ex)
            {
                AddLog(AppStrings.Get("log_report_fail", ex.Message), Color.Red);
            }
        }

        private void MainControl_Load(object sender, EventArgs e) { }
        private void iconState_Click(object sender, EventArgs e) { }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            iconState.State = TType.None;
            tBadge7.State = TState.Default;
            _alertStatus.Icon = TType.Info;
            _alertStatus.TextTitle = AppStrings.Get("alert_ready");
            _alertStatus.Text = AppStrings.Get("alert_ready_sn", _sn);
            _statsPanel.Invalidate();
            _barcodeInput.Focus();
            menu1.Collapsed = true;
        }

        // ═══ Serial Scanner ═════════════════════════════

        private void InitScanner()
        {
            try
            {
                string port = inidata["setport"]?["scanner_port"];
                if (string.IsNullOrEmpty(port)) return;

                int baud = 9600;
                int.TryParse(inidata["setport"]?["scanner_baudrate"] ?? "9600", out baud);

                _scanner = new ScannerService(port, baud);
                _scanner.BarcodeScanned += OnScannerBarcodeScanned;
                _scanner.ConnectionChanged += OnScannerConnectionChanged;
                _scanner.Start();
            }
            catch { }
        }

        private void OnScannerBarcodeScanned(string barcode)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnScannerBarcodeScanned(barcode)));
                return;
            }

            if (!_btnStart.Enabled) return;
            if (string.IsNullOrEmpty(barcode)) return;

            _barcodeInput.Text = barcode;
            BarcodeInput_KeyDown(_barcodeInput, new KeyEventArgs(Keys.Enter));
        }

        private void OnScannerConnectionChanged(bool connected)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnScannerConnectionChanged(connected)));
                return;
            }

            if (connected)
                _lblScannerStatus.Text = AppStrings.Get("scanner_connected", _scanner.PortName);
            else
                _lblScannerStatus.Text = AppStrings.Get("scanner_disconnected");
        }

        // ═══ Digital Input Monitor (SRND-CM-12DI) ═══════

        private void InitDiMonitor()
        {
            try
            {
                string port = inidata["setport"]?["SRND_CM_12DI_port"];
                if (string.IsNullOrEmpty(port)) return;

                _diMonitor = new DigitalInputMonitor(pollIntervalMs: 250);
                _diMonitor.InputRising += OnDiRising;
                _diMonitor.InputFalling += OnDiFalling;
                _diMonitor.ScanError += OnDiError;
                _diMonitor.Start();

                AddLog($"DI monitor started on {port}", Color.FromArgb(79, 195, 247));
            }
            catch { }
        }

        private void OnDiRising(int channel)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiRising(channel)));
                return;
            }
            AddLog($"DI#{channel} ↑ (0→1)", Color.FromArgb(82, 196, 26));
            // TODO: add custom action per channel, e.g.:
            // switch (channel) {
            //     case 0: _btnStart.PerformClick(); break;
            //     case 1: /* toggle something */ break;
            // }
        }

        private void OnDiFalling(int channel)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiFalling(channel)));
                return;
            }
            AddLog($"DI#{channel} ↓ (1→0)", Color.FromArgb(245, 34, 45));
        }

        private void OnDiError(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiError(message)));
                return;
            }
            AddLog($"DI error: {message}", Color.Red);
        }

        private void PwdInput_TextChanged(object sender, EventArgs e)
        {
            string expected = DateTime.Now.ToString("ddmm");
            if (_pwdInput.Text == expected) {

                chkSaveAppend.Enabled = true;
                chkStopOnFail.Enabled = true;
                log_append.Enabled = true;
                _pwdInput.Text = "";

            }
       
        }

        private void chkSaveAppend_CheckedChanged(object sender, BoolEventArgs e)
        {
            
            int method = 0;
            if (chkSaveExcel.Checked) method |= 1;
            if (chkSaveAppend.Checked) method |= 2;
            TestConfigManager.UpdateSaveMethod(method);
            DisableCheckboxes();
            _pwdInput.Text = "";
        }

        private void chkStopOnFail_CheckedChanged(object sender, BoolEventArgs e)
        {
            TestConfigManager.UpdateStopOnFail(chkStopOnFail.Checked ? 1 : 0);
            DisableCheckboxes();
            _pwdInput.Text = "";
        }

        private void log_append_CheckedChanged(object sender, BoolEventArgs e)
        {
            DisableCheckboxes();
            _pwdInput.Text = "";
        }

        private void DisableCheckboxes()
        {
            chkSaveAppend.Enabled = false;
            chkStopOnFail.Enabled = false;
            log_append.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            _engine.Cancel();
        }

        private void breadcrumb1_ItemClick(object sender, BreadcrumbItemEventArgs e)
        {
            if (e.Item.Text == "V_Relay")
            {
               
            }
            if (e.Item.Text == "SK_Relay")
            {
               
            }
        }

        private void windowBar_Click(object sender, EventArgs e) { }

        #region /*--------------message loop dll upload-------------*/

        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        private static extern int PostMessage(
            IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public const int USER = 0x0400;
        public const int WM_SENDA = USER + 101;
        public const int WM_SENDB = USER + 102;
        public const int WM_SENDC = USER + 103;
        public const int WM_SENDD = USER + 104;
        public const int WM_SENDE = USER + 105;
        public const int WM_SHOWNUM = USER + 106;
        public const int WM_FASTID = USER + 107;
        public const int WM_SENDA_2 = USER + 108;
        public const int WM_SENDA_3 = USER + 109;
        public const int WM_SEND_SET_CC1310LOSS = USER + 110;
        public const int WM_SEND_SET_BTLOSS = USER + 111;
        public const int WM_SEND_SET_WIFILOSS = USER + 112;
        public const int WM_SEND_AUTOTEST = USER + 113;
        public const int WM_SEND_RF_REF = USER + 114;
        public const int WM_SENDMYREALY_1 = USER + 115;
        public const int WM_SENDMYREALY_2 = USER + 116;
        public const int WM_SENDMACSAVE = USER + 117;
        public const int WM_BLE_PATH_LOSS_CH0 = USER + 118;
        public const int WM_BLE_PATH_LOSS_CH20 = USER + 119;
        public const int WM_BLE_PATH_LOSS_CH39 = USER + 120;
        public const int WM_TEST_TRIGGER_RUN = USER + 121;
        public const int WM_SENDA_4 = USER + 122;
        public const int WM_SK_RELAY1_SET = USER + 123;
        public const int WM_SK_RELAY2_SET = USER + 124;
        public const int WM_CHANGE_TEXT_BOX1 = USER + 125;
        public const int WM_INNOVE_RELAY1_SET = USER + 126;
        public const int WM_INNOVE_RELAY2_SET = USER + 127;
        IntPtr ptrWnd;
        private string _sn2;
        private DateTime _testStartTime;
        private ScannerService _scanner;
        private DigitalInputMonitor _diMonitor;

        #endregion

        #region /*-------------LOOP FUNCTION BACKPROC-----------*/

        protected override void DefWndProc(ref Message ms)
        {
            switch (ms.Msg)
            {
                case WM_SENDA:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board"] != null)
                            testcase_lib.Getfun()["relay_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                    }
                    break;
                case WM_SENDA_2:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board2"] != null)
                            testcase_lib.Getfun()["relay2_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                    }
                    break;
                case WM_SENDA_3:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board3"] != null)
                            testcase_lib.Getfun()["relay3_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                    }
                    break;
                case WM_SENDA_4:
                    {
                        string tmp = "";
                        if (inidata["setport"]["Relay_board4"] != null)
                            testcase_lib.Getfun()["relay4_set"]("", "", out tmp, Marshal.PtrToStringAnsi(ms.LParam));
                    }
                    break;
                case WM_SENDD:
                    this.Close();
                    break;
                case WM_SEND_AUTOTEST:
                    break;
                case WM_SENDMYREALY_1:
                    {
                        if (inidata["setport"]["myrelay_board"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["myrelay_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_SENDMYREALY_2:
                    {
                        if (inidata["setport"]["myrelay_board2"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["myrelay_set2"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_SK_RELAY1_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["sk_relay1_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_SK_RELAY2_SET:
                    {
                        if (inidata["setport"]["sk_Relay_board2"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["sk_relay2_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_INNOVE_RELAY2_SET:
                    {
                        if (inidata["setport"]["innove_Relay_board2"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["innove_relay2_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_INNOVE_RELAY1_SET:
                    {
                        if (inidata["setport"]["innove_Relay_board"] != null)
                        {
                            string temp;
                            testcase_lib.Getfun()["innove_relay1_set"]("pass", "pass", out temp, Marshal.PtrToStringAnsi(ms.LParam));
                        }
                    }
                    break;
                case WM_TEST_TRIGGER_RUN:
                    break;
                case WM_CHANGE_TEXT_BOX1:
                    break;
                default:
                    break;
            }
            base.DefWndProc(ref ms);
        }

        #endregion

    }
}
