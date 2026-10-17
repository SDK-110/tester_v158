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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
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
        /// <summary>本轮测试是否走到过 OnTestCompleted（正常跑完）。用于判断 finally 里要不要兜底复位界面。</summary>
        private bool _testCompleted;
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
            // 深色主题必须在控件创建前设定：等整棵控件树建好再切主题会触发全量重绘，拖慢启动
            AntdUI.Config.IsDark = true;
            AntdUI.Style.SetPrimary(Palette.Accent);

            // 【关键】改写 AntdUI 暗色主题自带的"中性灰"色卡（#141414/#1D1D1D/#3A414B…）。
            // 不改这里，凡是 AntdUI 默认绘制的地方都会是灰的 —— Panel 的默认底、Table 表头、
            // 表格边框、Input 底 —— 与我们的海军蓝系配色割裂，也会让卡片圆角缺口露出灰底。
            // 必须在 IsDark = true 之后调用，Set 才会写进"暗色"这份色卡。
            AntdUI.Style.Set(AntdUI.Colour.BgBase, Palette.Bg);                 // 窗体底
            AntdUI.Style.Set(AntdUI.Colour.BgLayout, Palette.Bg);               // 布局底
            AntdUI.Style.Set(AntdUI.Colour.BgContainer, Palette.Card);          // 容器/卡片底
            AntdUI.Style.Set(AntdUI.Colour.BgElevated, Palette.Card);           // 浮层底
            AntdUI.Style.Set(AntdUI.Colour.Split, Palette.Divider);             // 分隔线
            AntdUI.Style.Set(AntdUI.Colour.BorderColor, Palette.Divider);       // 边框
            AntdUI.Style.Set(AntdUI.Colour.Fill, Palette.Input);                // 输入框/填充底
            AntdUI.Style.Set(AntdUI.Colour.FillSecondary, Palette.Input);
            AntdUI.Style.Set(AntdUI.Colour.HoverBg, Palette.Track);             // hover 底
            AntdUI.Style.Set(AntdUI.Colour.TextBase, Palette.TextPrimary);      // 主文字
            AntdUI.Style.Set(AntdUI.Colour.Text, Palette.TextPrimary);
            AntdUI.Style.Set(AntdUI.Colour.TextSecondary, Palette.TextSecondary); // 次文字
            AntdUI.Style.Set(AntdUI.Colour.TextTertiary, Palette.TextMuted);    // 弱文字
            AntdUI.Style.Set(AntdUI.Colour.Success, Palette.Pass);
            AntdUI.Style.Set(AntdUI.Colour.Error, Palette.Fail);
            AntdUI.Style.Set(AntdUI.Colour.Warning, Palette.Warning);

            InitializeComponent();

            // KPI 卡片行自适应：原为硬编码绝对坐标，总宽超出面板、且最右卡 Dock=Right 会压住前一卡。
            // 挂上重排后随窗体宽度变化自动适配。
            _statsPanel.SizeChanged += (s, e) => LayoutKpiCards();
            LayoutKpiCards();

            // 图表高度按窗体高度自动分配（原来写死在 Designer 里，窗口越高图表越显扁）
            SizeChanged += (s, e) => LayoutChartHeight();
            LayoutChartHeight();
          

            // 图表暗色主题
            _chartPanel.GridColor = Palette.Divider;
            _chartPanel.AxisColor = Palette.Divider;
            _chartPanel.YMax = null;        // 自动缩放Y轴
            _chartPanel.YMin = 0;
            // 柱状图：PASS/FAIL 两个数据集按小时分组并列，观感对齐参考界面
            _chartPanel.ChartType = TChartType.Bar;
            // 高度已由 LayoutChartHeight() 按窗体高度自动分配，图例放得下了
            _chartPanel.ShowLegend = true;
            // 图例底色/边框在 Designer 里是 null（取主题默认 -> 灰），改成透明，
            // 让图例直接落在图表底色上；图例小方块的颜色由数据集上的 LegendBoxFillColor 决定。
            _chartPanel.LegendBackColor = Color.Transparent;
            _chartPanel.LegendBorderColor = Color.Transparent;

            // 【紧凑化】AntdUI.Chart 在竖直方向吃掉大量空间：实测 190px 控件里绘图区只剩 23px。
            // 用独立探针(chartprobe)逐项量过 —— 图例 -34px、标题 -28px、Padding -16px。
            // 图例默认 TopRight 会独占一条横带；改成 MiddleRight 让它落到绘图区右侧空白处，
            // 绘图区 23px -> 71px（3 倍）。Padding 默认 20、LegendPadding 默认 10，一并归零。
            _chartPanel.Padding = 0;
            _chartPanel.LegendPadding = 0;
            _chartPanel.LegendPosition = ContentAlignment.MiddleRight;

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

        // ═══ KPI 卡片行自适应布局 ════════════════════════

        private const int KpiPad = 12;   // 卡片行左右留白
        private const int KpiGap = 12;   // 卡片间距

        /// <summary>
        /// 按面板实际宽度重排 KPI 卡片行。
        ///
        /// 为什么需要：设计期这四张卡是硬编码绝对坐标，总宽 396+406+246+356 = 1404，
        /// 而 _statsPanel 可用宽度只有 1370-24 = 1346 —— 即使在设计尺寸下也放不下；
        /// 且 panel_Project_name 用 Dock=Right，窗体一变窄就左移压住 _cardOperator
        /// （实测窗体 1370 宽时重叠约 88px）。另外四张卡高度还不一致（三张 100、右侧 84）。
        /// 这里统一：高度取满面板、宽度按原设计比例等分、间距固定。
        /// </summary>
        private void LayoutKpiCards()
        {
            if (_statsPanel == null || _statsPanel.IsDisposed) return;

            var cards = new Control[] { _cardTotal, _cardHourly, _cardOperator, panel_Project_name };
            // 原设计期宽度，仅作为"相对宽窄"的权重，不再决定绝对尺寸
            float[] weights = { 396f, 406f, 246f, 356f };

            int panelW = _statsPanel.ClientSize.Width;
            int panelH = _statsPanel.ClientSize.Height;
            int n = cards.Length;
            int avail = panelW - KpiPad * 2 - KpiGap * (n - 1);
            if (avail <= 0 || panelH <= 0) return;

            float sum = 0f;
            for (int i = 0; i < n; i++) sum += weights[i];

            int x = KpiPad;
            int used = 0;
            for (int i = 0; i < n; i++)
            {
                // 最后一格吃余量，避免逐格取整累积出几像素的缝
                int w = (i == n - 1)
                    ? Math.Max(avail - used, 1)
                    : (int)Math.Round(avail * weights[i] / sum);
                used += w;
                cards[i].SetBounds(x, 0, w, panelH);
                x += w + KpiGap;
            }

            LayoutMetricCard(_lblTotalTitle,
                new Control[] { _lblTotalCount, _lblTotalPass, _lblTotalFail, _lblTotalYield },
                new Control[] { _lblTotalSubTotal, _lblTotalSubPass, _lblTotalSubFail, _lblTotalSubYield },
                _barTotalBg);

            LayoutMetricCard(_lblHourTitle,
                new Control[] { _lblHourCount, _lblHourPass, _lblHourFail, _lblHourYield },
                new Control[] { _lblHourSubTotal, _lblHourSubPass, _lblHourSubFail, _lblHourSubYield },
                _barHourBg);
        }

        /// <summary>
        /// 重排一张"四指标"卡片的内容：标题 / 大数字 / 小字说明 / 进度条。
        ///
        /// 1) 竖直方向重新分带，消除设计期"大数字 30..59"与"小字 52..68"重叠 7px 的问题；
        /// 2) 把小字提到 z-order 最前 —— 设计器的 Controls.Add 顺序让小字排在最靠后，
        ///    会被大数字的不透明控件矩形盖住上半截，这就是"数字下面的字被遮挡"。
        /// </summary>
        private void LayoutMetricCard(Control title, Control[] numbers, Control[] subs, Control bar)
        {
            Control card = bar.Parent;          // _barTotalBg / _barHourBg 挂在卡片上
            if (card == null) return;
            int inner = Math.Max(card.ClientSize.Width - KpiPad * 2, 1);

            // 竖直分带（卡片高 100）：8 / 31 / 63 / 86，互不重叠
            if (title != null) title.Top = 8;
            foreach (var c in numbers) c.Top = 31;
            foreach (var c in subs) c.Top = 63;
            bar.Top = 86;

            // 四个指标列等分
            for (int i = 0; i < 4; i++)
            {
                int cx = KpiPad + (int)((long)inner * i / 4);
                if (i < numbers.Length) numbers[i].Left = cx;
                if (i < subs.Length) subs[i].Left = cx;
            }

            bar.Left = KpiPad;
            bar.Width = inner;                  // 进度条宽度自适应后，pass/fail 填充会跟着变

            // z-order：先抬小字，再抬进度条（两者空间上不重叠，顺序仅为确定性）
            foreach (var c in subs) c.BringToFront();
            bar.BringToFront();
        }

        // ═══ 图表高度自适应 ══════════════════════════════

        // 图表高度 = 窗体客户区高度 × ChartHeightRatio，再夹到 [Min, Max]。
        //
        // 用独立探针(chartprobe)量过：绘图区高 ≈ 控件高 × 0.63（标题+图例+Padding 都已在
        // 构造函数里紧凑化，见 _chartPanel.Padding / LegendPosition 那几行）。实测曲线：
        //     控件 120 -> 绘图区  75px      控件 200 -> 121px
        //     控件 160 -> 绘图区  89px      控件 240 -> 153px
        //     控件 180 -> 绘图区 105px      控件 280 -> 185px
        // 所以 150~190 的控件已能给出 88~115px 的绘图区 —— 够用且不占地方。
        //
        // ⚠ 下限不要再往下调：控件低于 ~110 时绘图区会趋近于 0，表现为"看不到柱状图"
        //   （控件的标题/X轴标签/内边距是固定开销）。0.11 那版就是这么翻车的。
        private const float ChartHeightRatio = 0.16f;
        private const int   ChartMinHeight   = 150;
        private const int   ChartMaxHeight   = 190;
        private const int   TableMinHeight   = 180;   // 表格保底（约 4 行 + 表头）

        /// <summary>
        /// 按窗体高度自动分配 _chartPanel 的高度。
        /// 它 Dock=Top、_table Dock=Fill，所以在这里改高度会自动让 _table 让出/收回空间。
        /// 原来高度写死在 Designer（115，后改 190），窗口越大图表越显扁。
        /// </summary>
        private void LayoutChartHeight()
        {
            if (_chartPanel == null || _chartPanel.IsDisposed) return;
            int h = ClientSize.Height;
            if (h <= 0) return;

            // 停靠面板实际占掉的高度（不写死，面板高度以后可能改）
            int occupied = _statsPanel.Height + _statusBar.Height + _bottomPanel.Height + _barcodePanel.Height;
            int avail = h - occupied;                       // 图表 + 表格 可分配的总高

            int target = (int)(h * ChartHeightRatio);
            int cap = Math.Max(avail - TableMinHeight, ChartMinHeight);   // 别把表格挤没了
            target = Math.Min(Math.Min(target, cap), ChartMaxHeight);
            target = Math.Max(target, ChartMinHeight);

            if (_chartPanel.Height != target)
                _chartPanel.Height = target;
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

            // ChartDataset(label, fillColor) 第二个参数就是填充色，柱状图直接用这个
            var failDs = new ChartDataset("FAIL", Palette.Fail)
            {
                BorderWidth = 0,                  // 折线时代是 3px 描边，柱状下会变成粗边框
                LegendBoxFillColor = Palette.Fail // 图例小方块，不设会取主题默认的灰
            };
            var passDs = new ChartDataset("PASS", Palette.Pass)
            {
                BorderWidth = 0,
                LegendBoxFillColor = Palette.Pass
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
                    AddLog(AppStrings.Get("barcode_scanned", barcode), Palette.AccentSoft);

                    _barcodeInput.Enabled = false;
                    _btnStart.Focus();          // SN 框禁用时把焦点交给 Start
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
                    ApplyBarcodeMode();     // 配置里可能改了"启用条码" 
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
                    ApplyBarcodeMode();     // 配置里可能改了"启用条码" 
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

            int rowHeight = (_table.RowHeight ?? 0) > 0 ? _table.RowHeight.Value : 36;
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
            // 每列必须显式给 Width。AutoSizeColumnsMode=Fill 且不给宽度时, AntdUI 会按
            // **单元格内容**计算列宽 —— 测试中 MeasuredValue/StateBadge/Duration 一变,
            // 整行所有列就横向重排, 表现为"表格里的字符串一直位移"(实测: 改一个单元格,
            // 其他列区域差异 1671px; 给定宽度后降到 137px)。Width 是字符串, 支持百分比。
            _table.Columns = new ColumnCollection
            {
                new Column("Id", AppStrings.Get("col_id")) { Width = "6%" },
                new Column("Name", AppStrings.Get("col_name")) { Width = "30%" },
                new Column("LowLimit", AppStrings.Get("col_low")) { Width = "10%" }.SetColAlign(),
                new Column("HighLimit", AppStrings.Get("col_high")) { Width = "10%" }.SetColAlign(),
                new Column("MeasuredValue", AppStrings.Get("col_value")) { Width = "15%" }.SetColAlign(),
                new Column("StateBadge", AppStrings.Get("col_result")) { Width = "18%" }.SetColAlign(),
                new Column("Duration", AppStrings.Get("col_duration")) { Width = "11%" }.SetColAlign(),
            };
            _table.RowSelectedBg = Palette.Accent;
            _table.RowSelectedFore = Palette.TextPrimary;
            // 表头/边框：不改会沿用 AntdUI 暗色主题的中性灰（实测表头 #1D1D1D、分隔线 #3A414B）
            _table.ColumnBack = Palette.Header;
            _table.ColumnFore = Palette.TextSecondary;
            _table.BorderColor = Palette.Divider;
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
        #region TEST_CASE_DEMO
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

        #endregion
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
            Color logColor = Palette.TextPrimary;
            string msg = e.Message.ToUpper();
            if (msg.Contains("FAIL") || msg.Contains("ERROR"))
                logColor = Palette.Fail;
            else if (msg.Contains("PASS"))
                logColor = Palette.Pass;
            else if (msg.Contains("WARN"))
                logColor = Palette.WarnSoft;

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
                e.Item.BackColor = Palette.Accent;
                // 不再需要 _table.Refresh()：TestItem 已实现 INotifyPropertyChanged，
                // 上面两行赋值会自动只重绘受影响的单元格（实测差异 240px vs 整表重绘 3078px）。
                // 留着 Refresh() 反而会整表重绘 —— 那正是"每项抖一下"的来源。
                _table.SelectedIndex = e.Index + 1;
                _table.ScrollLine(e.Index + 1);
                _table.Focus();
                Application.DoEvents();     // 保持 UI 响应
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
                    backColor = Palette.PassDim;
                }
                else if (e.Item.State == TestState.Fail)
                {
                    badgeState = TState.Error;
                    backColor = Palette.FailDim;
                }
                else if (e.Item.State == TestState.Skipped)
                {
                    badgeState = TState.Warn;
                    backColor = Palette.Divider;
                }
                else
                {
                    badgeState = TState.Default;
                }

                e.Item.StateBadge = new CellBadge(badgeState, displayText);
                e.Item.BackColor = backColor;

                // 同上：单元格改由 INPC 自动刷新，不需要整表 Refresh()
                _table.ScrollLine(e.Index + 1);
                _table.SelectedIndex = e.Index + 1;
                _table.Focus();
                Application.DoEvents();     // 保持 UI 响应
            }
            UpdateCurrentTestStats();
        }

        private void OnTestCompleted(object sender, TestEngineEventArgs e)
        {
            // 先置位：即便下面因跨线程要 BeginInvoke 延迟执行，也算"已完成"，
            // 避免 _btnStart_Click 的 finally 重复复位界面。
            _testCompleted = true;
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnTestCompleted(sender, e)));
                return;
            }

            foreach (var dev in testcase_lib.dev_moren) {

                try
                {
                    if (dev.Value != null) dev.Value.set_default_set();
                }
                catch { }

            }
            try
            {

                testcase_lib.funcs["testsysini"]?.Invoke("","",out _ ,"");
            }
            catch { }
            int passCount = 0, failCount = 0;
            foreach (var item in _project.Items)
            {
                if (item.State == TestState.Pass) passCount++;
                else if (item.State == TestState.Fail) failCount++;
            }

            bool allPass = failCount == 0;

            _btnStart.Enabled = true;
            _btnStart.Loading = false;
           
            _barcodeInput.Enabled = _tracker.BarcodeEnabled;
            if (allPass)
            {
                AddLog($"+++ SN {_sn} PASS +++", Palette.Pass);
                AddLog(AppStrings.Get("log_all_pass"), Palette.Pass);
                iconState.State = TType.Success;
                tBadge7.State = TState.Success;
                _tracker.RecordPass();
              //  _tracker.RecordSnResult(_sn, true);
                SetAlertSuccess(AppStrings.Get("alert_pass_sn", _sn));
            }
            else
            {
                AddLog($"--- SN {_sn} FAIL ({failCount}) ---", Palette.Fail);
                AddLog(AppStrings.Get("log_fail", failCount), Palette.Fail);
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
            if (!log_append.Checked) TestLoggerForm.Instance.SaveLog("FF");
            // 测试完成后按"条码是否启用"决定焦点: 启用则回 SN 框(继续扫码), 禁用则给 Start
            ApplyBarcodeMode();
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
            AddLog($">>> SN: {_sn} <<<", Palette.AccentSoft);
            iconState.State = TType.Warn;
            tBadge7.State = TState.Processing;
            SetAlertWarning(AppStrings.Get("alert_testing_sn", _sn));
            if (_project.Items.Count == 0)
            {
                AddLog(AppStrings.Get("log_no_template"), Palette.Fail);
                MessageBox.Show(AppStrings.Get("dlg_no_template"), AppStrings.Get("dlg_confirm"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _btnStart.Enabled = false;
            _btnStart.Loading = true;
            progress3.Value = 0;

            _testCompleted = false;
            try
            {
                _project.ResetAllBadges();
                _table.Refresh();           // 仅重绘，不需重建 DataSource

                AddLog(AppStrings.Get("log_items", _project.Items.Count));

                bool initOk = await _engine.RunInitAsync();
                if (!initOk)
                {
                    AddLog(AppStrings.Get("log_init_fail"), Palette.Fail);
                    return;
                }

                AddLog(AppStrings.Get("log_test_steps"));
                await _engine.RunAsync();
                await _engine.RunCleanupAsync();
                AddLog(AppStrings.Get("log_test_complete"));

                // 测试总耗时（含初始化与清理）
                var elapsed = DateTime.Now - _testStartTime;
                AddLog($"总耗时: {elapsed:hh\\:mm\\:ss\\.fff}", Palette.WarnSoft);
            }
            finally
            {
                // TestEngine 只在"正常跑完"时触发 TestCompleted；取消（调试单步中途强行停止）、
                // 初始化失败、异常这三条路径都不会触发，界面会永远停在禁用状态。
                // 这里兜底复位，避免主窗体闭塞后无法恢复。
                if (!_testCompleted) RestoreUiAfterAbort();
            }
        }

        private void _btnLoad_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = AppStrings.Get("dlg_load_filter");
                dialog.Title = AppStrings.Get("dlg_load_title");
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // 不在这里设 label3 —— 交给 LoadFromExcel() 统一按"实际解析出的工程名"设置，
                    // 否则手动加载后显示文件名、重启后显示 sproj 的 B1 工程名，两不一致。
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

                // 工程名统一取自实际解析出的 config.ProjectName（sproj 格式取 B1 单元格，普通 xlsx 取文件名）。
                // 这样无论手动加载、自动加载还是重启，label3 显示的都与实际加载的工程一致。
                string projName = TestConfigManager.Instance.ProjectName;
                label3.Text = Path.GetFileNameWithoutExtension(
                    string.IsNullOrEmpty(projName) ? filePath : projName);
                RefreshTableData();
                AddLog(AppStrings.Get("log_template_loaded", Path.GetFileName(filePath)));
            }
            catch (Exception ex)
            {
                AddLog(AppStrings.Get("dlg_load_fail", ex.Message), Palette.Fail);
                MessageBox.Show(AppStrings.Get("dlg_load_fail", ex.Message), AppStrings.Get("dlg_error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadConfig()
        {
            var config = TestConfigManager.Instance;
            _orderNo = config.LastOrderNo;
            _operatorNo = config.LastOperatorNo;
            this.label3.Text = Path.GetFileNameWithoutExtension(config.ProjectName);
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

        /// <summary>
        /// 测试中断后的界面兜底复位。
        ///
        /// 为什么需要：恢复主窗体可用状态的代码原本只在 OnTestCompleted 里，
        /// 而 TestEngine.RunAsync 只在"正常跑完"时才触发 TestCompleted 事件 ——
        /// 取消（调试单步中途强行停止）、初始化失败、异常这三条路径都走不到，
        /// 于是 _btnStart 一直停在 Enabled=false + Loading=true、_barcodeInput 也一直禁用，
        /// 主窗体彻底闭塞、无法恢复。
        ///
        /// 注意这里只复位"界面可用性"，不碰统计与报告 —— 中断的这轮不应计入
        /// _tracker 也不应生成报告，那是 OnTestCompleted 正常路径才做的事。
        /// </summary>
        private void RestoreUiAfterAbort()
        {
            _btnStart.Enabled = true;
            _btnStart.Loading = false;
            _barcodeInput.Enabled = _tracker.BarcodeEnabled;
            iconState.State = TType.None;
            tBadge7.State = TState.Default;
            SetAlertWarning(AppStrings.Get("alert_ready"));
            if (_debugForm != null && !_debugForm.IsDisposed)
                _debugForm.ResetState();
            ApplyBarcodeMode();
            AddLog("测试已中断（未走完整流程），界面已复位", Palette.WarnSoft);
        }

        /// <summary>
        /// 按"条码是否启用"决定 SN 输入框的可用状态与焦点。
        ///
        /// 焦点始终跟随"条码输入是否启用":
        ///   启用 -> SN 框可用, 焦点放回 SN 框(测试完成后也要回来, 否则下一枪扫码收不到)
        ///   禁用 -> SN 框禁用, 焦点交给 Start(测试号会自动生成, 按一下就能开工)
        /// </summary>
        private void ApplyBarcodeMode()
        {
            bool enabled = _tracker.BarcodeEnabled;
            _barcodeInput.Enabled = enabled;
            if (enabled)
                _barcodeInput.Focus();
            else
                _btnStart.Focus();
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

                AddLog(AppStrings.Get("log_report_saved"), Palette.Pass);
            }
            catch (Exception ex)
            {
                AddLog(AppStrings.Get("log_report_fail", ex.Message), Palette.Fail);
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
            // label3（工程名）已在 LoadConfig() 里由 config.ProjectName 设好，此处不要覆盖。
            // 原代码用 ProjectLoader.GetProjectNames()[0] 覆盖 —— 那是旧版遗留源，新版从不更新，
            // 显示的是旧版最后一次打开的工程，与实际加载的工程不一致。
            _statsPanel.Invalidate();
            ApplyBarcodeMode();
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

                _diMonitor =  DigitalInputMonitor.Instance; 
                _diMonitor.InputRising += OnDiRising;
                _diMonitor.InputFalling += OnDiFalling;
                _diMonitor.ScanError += OnDiError;
                _diMonitor.Start();

                AddLog($"DI monitor started on {port}", Palette.AccentSoft);
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
            AddLog($"DI#{channel} ↑ (0→1)", Palette.Pass);
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
            AddLog($"DI#{channel} ↓ (1→0)", Palette.Fail);
        }

        private void OnDiError(string message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() => OnDiError(message)));
                return;
            }
            AddLog($"DI error: {message}", Palette.Fail);
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

        private void _btnEditSproj_Click(object sender, EventArgs e)
        {
            // 密码验证 — 输入密码后才能编辑
            string expected = DateTime.Now.ToString("ddmm");
            using (var pwdForm = new Form())
            {
                pwdForm.Text = "Password Required";
                pwdForm.Size = new System.Drawing.Size(320, 160);
                pwdForm.StartPosition = FormStartPosition.CenterParent;
                pwdForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                pwdForm.MaximizeBox = false;
                pwdForm.MinimizeBox = false;
                pwdForm.ShowInTaskbar = false;

                var lbl = new System.Windows.Forms.Label
                {
                    Text = "Enter password to edit test cases:",
                    Location = new System.Drawing.Point(20, 20),
                    Size = new System.Drawing.Size(260, 20),
                    Font = new System.Drawing.Font("Microsoft YaHei UI", 10F)
                };

                var txtPwd = new System.Windows.Forms.TextBox
                {
                    UseSystemPasswordChar = true,
                    Location = new System.Drawing.Point(20, 50),
                    Size = new System.Drawing.Size(260, 25),
                    Font = new System.Drawing.Font("Microsoft YaHei UI", 11F)
                };

                var btnOk = new System.Windows.Forms.Button
                {
                    Text = "OK",
                    Location = new System.Drawing.Point(80, 90),
                    Size = new System.Drawing.Size(70, 30),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new System.Windows.Forms.Button
                {
                    Text = "Cancel",
                    Location = new System.Drawing.Point(170, 90),
                    Size = new System.Drawing.Size(70, 30),
                    DialogResult = DialogResult.Cancel
                };

                pwdForm.Controls.Add(lbl);
                pwdForm.Controls.Add(txtPwd);
                pwdForm.Controls.Add(btnOk);
                pwdForm.Controls.Add(btnCancel);
                pwdForm.AcceptButton = btnOk;
                pwdForm.CancelButton = btnCancel;

                if (pwdForm.ShowDialog(this) != DialogResult.OK)
                    return;

                if (txtPwd.Text != expected)
                {
                    AntdUI.Message.error(this, "Incorrect password!", autoClose: 2);
                    return;
                }
            }

            var form = SprojEditForm.get_instance();
            form.is_called_by_other = (s, args) =>
            {
                // 保存后重新加载测试用例
                this.BeginInvoke(new Action(() =>
                {
                    try
                    {
                        string path = TestConfigManager.CurrentProjectPath;
                        if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                        {
                            LoadFromExcel(path);
                            AddLog("Test cases reloaded from sproj", Palette.Pass);
                        }
                    }
                    catch { }
                }));
            };
            form.Show();
            form.BringToFront();
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
