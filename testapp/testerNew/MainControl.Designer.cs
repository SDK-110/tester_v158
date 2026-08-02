namespace test_antdui
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            AntdUI.MenuItem menuItem10 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem11 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem12 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem13 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem14 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem15 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem16 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem17 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem18 = new AntdUI.MenuItem();
            _table = new AntdUI.Table();
            _btnLoad = new AntdUI.Button();
            _btnStart = new AntdUI.Button();
            _btnSave = new AntdUI.Button();
            chkSaveExcel = new AntdUI.Checkbox();
            chkSaveAppend = new AntdUI.Checkbox();
            chkStopOnFail = new AntdUI.Checkbox();
            progress3 = new AntdUI.Progress();
            button1 = new AntdUI.Button();
            _btnEditSproj = new AntdUI.Button();
            iconState = new AntdUI.IconState();
            tBadge7 = new AntdUI.Badge();
            windowBar = new AntdUI.PageHeader();
            txt_search = new AntdUI.Input();
            colorTheme = new AntdUI.ColorPicker();
            btn_mode = new AntdUI.Button();
            btn_global = new AntdUI.Dropdown();
            btn_setting = new AntdUI.Button();
            btn_more = new AntdUI.Dropdown();
            log_append = new AntdUI.Checkbox();
            _pwdInput = new AntdUI.Input();
            menu1 = new AntdUI.Menu();
            _statsPanel = new AntdUI.Panel();
            panel_Project_name = new AntdUI.Panel();
            label3 = new AntdUI.Label();
            label4 = new AntdUI.Label();
            _cardOperator = new AntdUI.Panel();
            _lblOpTime = new AntdUI.Label();
            _lblOpShift = new AntdUI.Label();
            _lblOpId = new AntdUI.Label();
            _lblOpTitle = new AntdUI.Label();
            _cardHourly = new AntdUI.Panel();
            _barHourBg = new AntdUI.Panel();
            _barHourPass = new AntdUI.Panel();
            _barHourFail = new AntdUI.Panel();
            _lblHourYield = new AntdUI.Label();
            _lblHourFail = new AntdUI.Label();
            _lblHourPass = new AntdUI.Label();
            _lblHourCount = new AntdUI.Label();
            _lblHourTitle = new AntdUI.Label();
            _lblHourSubTotal = new AntdUI.Label();
            _lblHourSubPass = new AntdUI.Label();
            _lblHourSubFail = new AntdUI.Label();
            _lblHourSubYield = new AntdUI.Label();
            _cardTotal = new AntdUI.Panel();
            _barTotalBg = new AntdUI.Panel();
            _barTotalPass = new AntdUI.Panel();
            _lblTotalYield = new AntdUI.Label();
            _lblTotalFail = new AntdUI.Label();
            _lblTotalPass = new AntdUI.Label();
            _lblTotalCount = new AntdUI.Label();
            _lblTotalTitle = new AntdUI.Label();
            _lblTotalSubTotal = new AntdUI.Label();
            _lblTotalSubPass = new AntdUI.Label();
            _lblTotalSubFail = new AntdUI.Label();
            _lblTotalSubYield = new AntdUI.Label();
            _chartPanel = new AntdUI.Chart();
            _barcodePanel = new AntdUI.Panel();
            _barcodeInput = new AntdUI.Input();
            _lblBarcodeStatus = new AntdUI.Label();
            _lblScannerStatus = new AntdUI.Label();
            _statusBar = new AntdUI.Panel();
            _lblStatusLeft = new AntdUI.Label();
            _lblStatusRight = new AntdUI.Label();
            _bottomPanel = new AntdUI.Panel();
            _alertStatus = new AntdUI.Alert();
            windowBar.SuspendLayout();
            _statsPanel.SuspendLayout();
            panel_Project_name.SuspendLayout();
            _cardOperator.SuspendLayout();
            _cardHourly.SuspendLayout();
            _barHourBg.SuspendLayout();
            _cardTotal.SuspendLayout();
            _barTotalBg.SuspendLayout();
            _barcodePanel.SuspendLayout();
            _statusBar.SuspendLayout();
            _bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // _table
            // 
            _table.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            _table.BackColor = System.Drawing.Color.Transparent;
            _table.Bordered = true;
            _table.Dock = System.Windows.Forms.DockStyle.Fill;
            _table.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            _table.Gap = 12;
            _table.Location = new System.Drawing.Point(52, 255);
            _table.Name = "_table";
            _table.Padding = new System.Windows.Forms.Padding(10);
            _table.RowHeight = 35;
            _table.Size = new System.Drawing.Size(1370, 319);
            _table.TabIndex = 10;
            // 
            // _btnLoad
            // 
            _btnLoad.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            _btnLoad.Location = new System.Drawing.Point(12, 10);
            _btnLoad.Name = "_btnLoad";
            _btnLoad.Size = new System.Drawing.Size(120, 35);
            _btnLoad.TabIndex = 0;
            _btnLoad.Text = "📂 Load";
            _btnLoad.Type = AntdUI.TTypeMini.Primary;
            _btnLoad.Click += _btnLoad_Click;
            // 
            // _btnStart
            // 
            _btnStart.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            _btnStart.Location = new System.Drawing.Point(140, 10);
            _btnStart.Name = "_btnStart";
            _btnStart.Size = new System.Drawing.Size(120, 35);
            _btnStart.TabIndex = 1;
            _btnStart.Text = "▶ Start";
            _btnStart.Type = AntdUI.TTypeMini.Primary;
            _btnStart.Click += _btnStart_Click;
            // 
            // _btnSave
            // 
            _btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            _btnSave.Location = new System.Drawing.Point(268, 10);
            _btnSave.Name = "_btnSave";
            _btnSave.Size = new System.Drawing.Size(120, 35);
            _btnSave.TabIndex = 2;
            _btnSave.Text = "💾 Save";
            _btnSave.Type = AntdUI.TTypeMini.Primary;
            _btnSave.Click += _btnSave_Click;
            // 
            // chkSaveExcel
            // 
            chkSaveExcel.Checked = true;
            chkSaveExcel.CheckState = System.Windows.Forms.CheckState.Checked;
            chkSaveExcel.Enabled = false;
            chkSaveExcel.Location = new System.Drawing.Point(12, 95);
            chkSaveExcel.Name = "chkSaveExcel";
            chkSaveExcel.Size = new System.Drawing.Size(130, 20);
            chkSaveExcel.TabIndex = 8;
            chkSaveExcel.Text = "Save Excel";
            chkSaveExcel.CheckedChanged += ChkSaveExcel_CheckedChanged;
            // 
            // chkSaveAppend
            // 
            chkSaveAppend.Enabled = false;
            chkSaveAppend.Location = new System.Drawing.Point(148, 95);
            chkSaveAppend.Name = "chkSaveAppend";
            chkSaveAppend.Size = new System.Drawing.Size(100, 20);
            chkSaveAppend.TabIndex = 9;
            chkSaveAppend.Text = "Append";
            chkSaveAppend.CheckedChanged += chkSaveAppend_CheckedChanged;
            // 
            // chkStopOnFail
            // 
            chkStopOnFail.Enabled = false;
            chkStopOnFail.Location = new System.Drawing.Point(254, 95);
            chkStopOnFail.Name = "chkStopOnFail";
            chkStopOnFail.Size = new System.Drawing.Size(110, 20);
            chkStopOnFail.TabIndex = 10;
            chkStopOnFail.Text = "Stop on Fail";
            chkStopOnFail.CheckedChanged += chkStopOnFail_CheckedChanged;
            // 
            // progress3
            // 
            progress3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            progress3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            progress3.Location = new System.Drawing.Point(12, 56);
            progress3.Name = "progress3";
            progress3.Size = new System.Drawing.Size(839, 28);
            progress3.TabIndex = 7;
            // 
            // button1
            // 
            button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            button1.Location = new System.Drawing.Point(396, 10);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(100, 35);
            button1.TabIndex = 3;
            button1.Text = "⏹ Stop";
            button1.ToggleType = AntdUI.TTypeMini.Primary;
            button1.Click += button1_Click_1;
            // 
            // _btnEditSproj
            // 
            _btnEditSproj.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            _btnEditSproj.Location = new System.Drawing.Point(502, 10);
            _btnEditSproj.Name = "_btnEditSproj";
            _btnEditSproj.Size = new System.Drawing.Size(120, 35);
            _btnEditSproj.TabIndex = 20;
            _btnEditSproj.Text = "📝 Edit";
            _btnEditSproj.Type = AntdUI.TTypeMini.Primary;
            _btnEditSproj.Click += _btnEditSproj_Click;
            // 
            // iconState
            // 
            iconState.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            iconState.Location = new System.Drawing.Point(3522, 36);
            iconState.Name = "iconState";
            iconState.Size = new System.Drawing.Size(48, 65);
            iconState.State = AntdUI.TType.Error;
            iconState.TabIndex = 12;
            // 
            // tBadge7
            // 
            tBadge7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            tBadge7.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F);
            tBadge7.Location = new System.Drawing.Point(3382, 10);
            tBadge7.Name = "tBadge7";
            tBadge7.Size = new System.Drawing.Size(132, 105);
            tBadge7.State = AntdUI.TState.Processing;
            tBadge7.TabIndex = 13;
            // 
            // windowBar
            // 
            windowBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            windowBar.Controls.Add(txt_search);
            windowBar.Controls.Add(colorTheme);
            windowBar.Controls.Add(btn_mode);
            windowBar.Controls.Add(btn_global);
            windowBar.Controls.Add(btn_setting);
            windowBar.Controls.Add(btn_more);
            windowBar.DividerMargin = 3;
            windowBar.DividerShow = true;
            windowBar.Dock = System.Windows.Forms.DockStyle.Top;
            windowBar.IconSvg = "AndroidOutlined";
            windowBar.Location = new System.Drawing.Point(0, 0);
            windowBar.Name = "windowBar";
            windowBar.ShowButton = true;
            windowBar.ShowIcon = true;
            windowBar.Size = new System.Drawing.Size(1422, 40);
            windowBar.SubText = "PCBA Test Platform";
            windowBar.TabIndex = 18;
            windowBar.Text = "Tester";
            windowBar.Click += windowBar_Click;
            // 
            // txt_search
            // 
            txt_search.Dock = System.Windows.Forms.DockStyle.Right;
            txt_search.Location = new System.Drawing.Point(884, 0);
            txt_search.Name = "txt_search";
            txt_search.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            txt_search.PlaceholderText = "Search...";
            txt_search.PrefixSvg = "SearchOutlined";
            txt_search.Size = new System.Drawing.Size(170, 40);
            txt_search.TabIndex = 1;
            // 
            // colorTheme
            // 
            colorTheme.Dock = System.Windows.Forms.DockStyle.Right;
            colorTheme.Location = new System.Drawing.Point(1054, 0);
            colorTheme.Name = "colorTheme";
            colorTheme.Padding = new System.Windows.Forms.Padding(5);
            colorTheme.Size = new System.Drawing.Size(40, 40);
            colorTheme.TabIndex = 2;
            // 
            // btn_mode
            // 
            btn_mode.Dock = System.Windows.Forms.DockStyle.Right;
            btn_mode.Ghost = true;
            btn_mode.IconSvg = "SunOutlined";
            btn_mode.Location = new System.Drawing.Point(1094, 0);
            btn_mode.Name = "btn_mode";
            btn_mode.Radius = 0;
            btn_mode.Size = new System.Drawing.Size(46, 40);
            btn_mode.TabIndex = 3;
            btn_mode.ToggleIconSvg = "MoonOutlined";
            btn_mode.WaveSize = 0;
            btn_mode.Click += BtnMode_Click;
            // 
            // btn_global
            // 
            btn_global.Dock = System.Windows.Forms.DockStyle.Right;
            btn_global.DropDownRadius = 6;
            btn_global.Ghost = true;
            btn_global.IconSvg = "GlobalOutlined";
            btn_global.Location = new System.Drawing.Point(1140, 0);
            btn_global.Name = "btn_global";
            btn_global.Placement = AntdUI.TAlignFrom.BR;
            btn_global.Radius = 0;
            btn_global.Size = new System.Drawing.Size(46, 40);
            btn_global.TabIndex = 4;
            btn_global.WaveSize = 0;
            // 
            // btn_setting
            // 
            btn_setting.Dock = System.Windows.Forms.DockStyle.Right;
            btn_setting.Ghost = true;
            btn_setting.IconSvg = "SettingOutlined";
            btn_setting.Location = new System.Drawing.Point(1186, 0);
            btn_setting.Name = "btn_setting";
            btn_setting.Radius = 0;
            btn_setting.Size = new System.Drawing.Size(46, 40);
            btn_setting.TabIndex = 5;
            btn_setting.WaveSize = 0;
            // 
            // btn_more
            // 
            btn_more.Dock = System.Windows.Forms.DockStyle.Right;
            btn_more.DropDownRadius = 6;
            btn_more.Ghost = true;
            btn_more.IconSvg = "MoreOutlined";
            btn_more.Location = new System.Drawing.Point(1232, 0);
            btn_more.Name = "btn_more";
            btn_more.Placement = AntdUI.TAlignFrom.BR;
            btn_more.Radius = 0;
            btn_more.Size = new System.Drawing.Size(46, 40);
            btn_more.TabIndex = 6;
            btn_more.WaveSize = 0;
            // 
            // log_append
            // 
            log_append.Enabled = false;
            log_append.Location = new System.Drawing.Point(370, 95);
            log_append.Name = "log_append";
            log_append.Size = new System.Drawing.Size(90, 20);
            log_append.TabIndex = 11;
            log_append.Text = "Log Append";
            log_append.CheckedChanged += log_append_CheckedChanged;
            // 
            // _pwdInput
            // 
            _pwdInput.Location = new System.Drawing.Point(470, 88);
            _pwdInput.Name = "_pwdInput";
            _pwdInput.PlaceholderText = "修改密码";
            _pwdInput.Size = new System.Drawing.Size(120, 37);
            _pwdInput.TabIndex = 12;
            _pwdInput.UseSystemPasswordChar = true;
            _pwdInput.TextChanged += PwdInput_TextChanged;
            // 
            // menu1
            // 
            menu1.Collapsed = true;
            menu1.Dock = System.Windows.Forms.DockStyle.Left;
            menuItem10.IconSvg = "SettingOutlined";
            menuItem10.Text = "Settings";
            menuItem11.IconSvg = "BarChartOutlined";
            menuItem11.Text = "Production";
            menuItem12.IconSvg = "DeleteOutlined";
            menuItem12.Text = "Clear Data";
            menuItem13.IconSvg = "SwapOutlined";
            menuItem13.Text = "Switch Shift";
            menuItem14.IconSvg = "QuestionCircleOutlined";
            menuItem14.Text = "Help";
            menuItem15.IconSvg = "GlobalOutlined";
            menuItem15.Text = "Lang: EN";
            menuItem16.IconSvg = "AndroidOutlined";
            menuItem17.Text = "SK_Relay";
            menuItem18.Text = "sevy_relay";
            menuItem16.Sub.Add(menuItem17);
            menuItem16.Sub.Add(menuItem18);
            menuItem16.Text = "Relay";
            menu1.Items.Add(menuItem10);
            menu1.Items.Add(menuItem11);
            menu1.Items.Add(menuItem12);
            menu1.Items.Add(menuItem13);
            menu1.Items.Add(menuItem14);
            menu1.Items.Add(menuItem15);
            menu1.Items.Add(menuItem16);
            menu1.Location = new System.Drawing.Point(0, 40);
            menu1.Name = "menu1";
            menu1.ScrollBarBlock = true;
            menu1.Size = new System.Drawing.Size(52, 733);
            menu1.TabIndex = 20;
            menu1.SelectChanged += Menu1_SelectChanged;
            menu1.MouseEnter += Menu1_MouseEnter;
            menu1.MouseLeave += Menu1_MouseLeave;
            // 
            // _statsPanel
            // 
            _statsPanel.Controls.Add(panel_Project_name);
            _statsPanel.Controls.Add(_cardOperator);
            _statsPanel.Controls.Add(_cardHourly);
            _statsPanel.Controls.Add(_cardTotal);
            _statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            _statsPanel.ForeColor = System.Drawing.Color.Transparent;
            _statsPanel.Location = new System.Drawing.Point(52, 40);
            _statsPanel.Name = "_statsPanel";
            _statsPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            _statsPanel.Size = new System.Drawing.Size(1370, 100);
            _statsPanel.TabIndex = 12;
            // 
            // panel_Project_name
            // 
            panel_Project_name.Controls.Add(label3);
            panel_Project_name.Controls.Add(label4);
            panel_Project_name.Dock = System.Windows.Forms.DockStyle.Right;
            panel_Project_name.Location = new System.Drawing.Point(1002, 8);
            panel_Project_name.Name = "panel_Project_name";
            panel_Project_name.Size = new System.Drawing.Size(356, 84);
            panel_Project_name.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSizeMode = AntdUI.TAutoSize.Auto;
            label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(14, 30);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(126, 27);
            label3.TabIndex = 2;
            label3.Text = "Not Logged";
            // 
            // label4
            // 
            label4.AutoSizeMode = AntdUI.TAutoSize.Auto;
            label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label4.ForeColor = System.Drawing.Color.FromArgb(136, 153, 170);
            label4.Location = new System.Drawing.Point(12, 6);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(46, 21);
            label4.TabIndex = 10;
            label4.Text = "Mode";
            // 
            // _cardOperator
            // 
            _cardOperator.Controls.Add(_lblOpTime);
            _cardOperator.Controls.Add(_lblOpShift);
            _cardOperator.Controls.Add(_lblOpId);
            _cardOperator.Controls.Add(_lblOpTitle);
            _cardOperator.Location = new System.Drawing.Point(844, 0);
            _cardOperator.Name = "_cardOperator";
            _cardOperator.Size = new System.Drawing.Size(246, 100);
            _cardOperator.TabIndex = 2;
            // 
            // _lblOpTime
            // 
            _lblOpTime.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblOpTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblOpTime.ForeColor = System.Drawing.Color.FromArgb(79, 195, 247);
            _lblOpTime.Location = new System.Drawing.Point(80, 52);
            _lblOpTime.Name = "_lblOpTime";
            _lblOpTime.Size = new System.Drawing.Size(63, 19);
            _lblOpTime.TabIndex = 0;
            _lblOpTime.Text = "00:00:00";
            // 
            // _lblOpShift
            // 
            _lblOpShift.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblOpShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblOpShift.ForeColor = System.Drawing.Color.FromArgb(255, 167, 38);
            _lblOpShift.Location = new System.Drawing.Point(14, 52);
            _lblOpShift.Name = "_lblOpShift";
            _lblOpShift.Size = new System.Drawing.Size(25, 17);
            _lblOpShift.TabIndex = 1;
            _lblOpShift.Text = "Day";
            // 
            // _lblOpId
            // 
            _lblOpId.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblOpId.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblOpId.ForeColor = System.Drawing.Color.White;
            _lblOpId.Location = new System.Drawing.Point(14, 30);
            _lblOpId.Name = "_lblOpId";
            _lblOpId.Size = new System.Drawing.Size(88, 19);
            _lblOpId.TabIndex = 2;
            _lblOpId.Text = "Not Logged";
            // 
            // _lblOpTitle
            // 
            _lblOpTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblOpTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblOpTitle.ForeColor = System.Drawing.Color.FromArgb(136, 153, 170);
            _lblOpTitle.Location = new System.Drawing.Point(12, 6);
            _lblOpTitle.Name = "_lblOpTitle";
            _lblOpTitle.Size = new System.Drawing.Size(70, 21);
            _lblOpTitle.TabIndex = 10;
            _lblOpTitle.Text = "Operator";
            // 
            // _cardHourly
            // 
            _cardHourly.Controls.Add(_barHourBg);
            _cardHourly.Controls.Add(_lblHourYield);
            _cardHourly.Controls.Add(_lblHourFail);
            _cardHourly.Controls.Add(_lblHourPass);
            _cardHourly.Controls.Add(_lblHourCount);
            _cardHourly.Controls.Add(_lblHourTitle);
            _cardHourly.Controls.Add(_lblHourSubTotal);
            _cardHourly.Controls.Add(_lblHourSubPass);
            _cardHourly.Controls.Add(_lblHourSubFail);
            _cardHourly.Controls.Add(_lblHourSubYield);
            _cardHourly.Location = new System.Drawing.Point(432, 0);
            _cardHourly.Name = "_cardHourly";
            _cardHourly.Size = new System.Drawing.Size(406, 100);
            _cardHourly.TabIndex = 1;
            // 
            // _barHourBg
            // 
            _barHourBg.Back = System.Drawing.Color.FromArgb(40, 40, 68);
            _barHourBg.Controls.Add(_barHourPass);
            _barHourBg.Controls.Add(_barHourFail);
            _barHourBg.Location = new System.Drawing.Point(10, 79);
            _barHourBg.Name = "_barHourBg";
            _barHourBg.Size = new System.Drawing.Size(387, 4);
            _barHourBg.TabIndex = 0;
            // 
            // _barHourPass
            // 
            _barHourPass.Back = System.Drawing.Color.FromArgb(102, 187, 106);
            _barHourPass.Location = new System.Drawing.Point(0, 0);
            _barHourPass.Name = "_barHourPass";
            _barHourPass.Size = new System.Drawing.Size(0, 4);
            _barHourPass.TabIndex = 0;
            // 
            // _barHourFail
            // 
            _barHourFail.Back = System.Drawing.Color.FromArgb(239, 83, 80);
            _barHourFail.Location = new System.Drawing.Point(0, 0);
            _barHourFail.Name = "_barHourFail";
            _barHourFail.Size = new System.Drawing.Size(0, 4);
            _barHourFail.TabIndex = 1;
            // 
            // _lblHourYield
            // 
            _lblHourYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourYield.ForeColor = System.Drawing.Color.FromArgb(79, 195, 247);
            _lblHourYield.Location = new System.Drawing.Point(295, 30);
            _lblHourYield.Name = "_lblHourYield";
            _lblHourYield.Size = new System.Drawing.Size(31, 26);
            _lblHourYield.TabIndex = 1;
            _lblHourYield.Text = "0%";
            // 
            // _lblHourFail
            // 
            _lblHourFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourFail.ForeColor = System.Drawing.Color.FromArgb(239, 83, 80);
            _lblHourFail.Location = new System.Drawing.Point(200, 30);
            _lblHourFail.Name = "_lblHourFail";
            _lblHourFail.Size = new System.Drawing.Size(14, 29);
            _lblHourFail.TabIndex = 2;
            _lblHourFail.Text = "0";
            // 
            // _lblHourPass
            // 
            _lblHourPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourPass.ForeColor = System.Drawing.Color.FromArgb(102, 187, 106);
            _lblHourPass.Location = new System.Drawing.Point(105, 30);
            _lblHourPass.Name = "_lblHourPass";
            _lblHourPass.Size = new System.Drawing.Size(14, 29);
            _lblHourPass.TabIndex = 3;
            _lblHourPass.Text = "0";
            // 
            // _lblHourCount
            // 
            _lblHourCount.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourCount.ForeColor = System.Drawing.Color.FromArgb(79, 195, 247);
            _lblHourCount.Location = new System.Drawing.Point(10, 30);
            _lblHourCount.Name = "_lblHourCount";
            _lblHourCount.Size = new System.Drawing.Size(14, 29);
            _lblHourCount.TabIndex = 4;
            _lblHourCount.Text = "0";
            // 
            // _lblHourTitle
            // 
            _lblHourTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourTitle.ForeColor = System.Drawing.Color.FromArgb(136, 153, 170);
            _lblHourTitle.Location = new System.Drawing.Point(12, 6);
            _lblHourTitle.Name = "_lblHourTitle";
            _lblHourTitle.Size = new System.Drawing.Size(81, 21);
            _lblHourTitle.TabIndex = 5;
            _lblHourTitle.Text = "⏱ Hourly";
            // 
            // _lblHourSubTotal
            // 
            _lblHourSubTotal.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourSubTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourSubTotal.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblHourSubTotal.Location = new System.Drawing.Point(10, 52);
            _lblHourSubTotal.Name = "_lblHourSubTotal";
            _lblHourSubTotal.Size = new System.Drawing.Size(29, 16);
            _lblHourSubTotal.TabIndex = 10;
            _lblHourSubTotal.Text = "Hour";
            // 
            // _lblHourSubPass
            // 
            _lblHourSubPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourSubPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourSubPass.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblHourSubPass.Location = new System.Drawing.Point(105, 52);
            _lblHourSubPass.Name = "_lblHourSubPass";
            _lblHourSubPass.Size = new System.Drawing.Size(30, 16);
            _lblHourSubPass.TabIndex = 11;
            _lblHourSubPass.Text = "PASS";
            // 
            // _lblHourSubFail
            // 
            _lblHourSubFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourSubFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourSubFail.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblHourSubFail.Location = new System.Drawing.Point(200, 52);
            _lblHourSubFail.Name = "_lblHourSubFail";
            _lblHourSubFail.Size = new System.Drawing.Size(25, 16);
            _lblHourSubFail.TabIndex = 12;
            _lblHourSubFail.Text = "FAIL";
            // 
            // _lblHourSubYield
            // 
            _lblHourSubYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblHourSubYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblHourSubYield.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblHourSubYield.Location = new System.Drawing.Point(295, 52);
            _lblHourSubYield.Name = "_lblHourSubYield";
            _lblHourSubYield.Size = new System.Drawing.Size(29, 16);
            _lblHourSubYield.TabIndex = 13;
            _lblHourSubYield.Text = "Yield";
            // 
            // _cardTotal
            // 
            _cardTotal.Controls.Add(_barTotalBg);
            _cardTotal.Controls.Add(_lblTotalYield);
            _cardTotal.Controls.Add(_lblTotalFail);
            _cardTotal.Controls.Add(_lblTotalPass);
            _cardTotal.Controls.Add(_lblTotalCount);
            _cardTotal.Controls.Add(_lblTotalTitle);
            _cardTotal.Controls.Add(_lblTotalSubTotal);
            _cardTotal.Controls.Add(_lblTotalSubPass);
            _cardTotal.Controls.Add(_lblTotalSubFail);
            _cardTotal.Controls.Add(_lblTotalSubYield);
            _cardTotal.Location = new System.Drawing.Point(0, 0);
            _cardTotal.Name = "_cardTotal";
            _cardTotal.Size = new System.Drawing.Size(396, 100);
            _cardTotal.TabIndex = 0;
            // 
            // _barTotalBg
            // 
            _barTotalBg.Back = System.Drawing.Color.FromArgb(40, 40, 68);
            _barTotalBg.Controls.Add(_barTotalPass);
            _barTotalBg.Location = new System.Drawing.Point(10, 79);
            _barTotalBg.Name = "_barTotalBg";
            _barTotalBg.Size = new System.Drawing.Size(387, 4);
            _barTotalBg.TabIndex = 0;
            // 
            // _barTotalPass
            // 
            _barTotalPass.Back = System.Drawing.Color.FromArgb(102, 187, 106);
            _barTotalPass.Location = new System.Drawing.Point(0, 0);
            _barTotalPass.Name = "_barTotalPass";
            _barTotalPass.Size = new System.Drawing.Size(0, 4);
            _barTotalPass.TabIndex = 0;
            // 
            // _lblTotalYield
            // 
            _lblTotalYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalYield.ForeColor = System.Drawing.Color.FromArgb(79, 195, 247);
            _lblTotalYield.Location = new System.Drawing.Point(295, 30);
            _lblTotalYield.Name = "_lblTotalYield";
            _lblTotalYield.Size = new System.Drawing.Size(31, 26);
            _lblTotalYield.TabIndex = 1;
            _lblTotalYield.Text = "0%";
            // 
            // _lblTotalFail
            // 
            _lblTotalFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalFail.ForeColor = System.Drawing.Color.FromArgb(239, 83, 80);
            _lblTotalFail.Location = new System.Drawing.Point(200, 30);
            _lblTotalFail.Name = "_lblTotalFail";
            _lblTotalFail.Size = new System.Drawing.Size(14, 29);
            _lblTotalFail.TabIndex = 2;
            _lblTotalFail.Text = "0";
            // 
            // _lblTotalPass
            // 
            _lblTotalPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalPass.ForeColor = System.Drawing.Color.FromArgb(102, 187, 106);
            _lblTotalPass.Location = new System.Drawing.Point(105, 30);
            _lblTotalPass.Name = "_lblTotalPass";
            _lblTotalPass.Size = new System.Drawing.Size(14, 29);
            _lblTotalPass.TabIndex = 3;
            _lblTotalPass.Text = "0";
            // 
            // _lblTotalCount
            // 
            _lblTotalCount.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(79, 195, 247);
            _lblTotalCount.Location = new System.Drawing.Point(10, 30);
            _lblTotalCount.Name = "_lblTotalCount";
            _lblTotalCount.Size = new System.Drawing.Size(14, 29);
            _lblTotalCount.TabIndex = 4;
            _lblTotalCount.Text = "0";
            // 
            // _lblTotalTitle
            // 
            _lblTotalTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(136, 153, 170);
            _lblTotalTitle.Location = new System.Drawing.Point(12, 6);
            _lblTotalTitle.Name = "_lblTotalTitle";
            _lblTotalTitle.Size = new System.Drawing.Size(81, 21);
            _lblTotalTitle.TabIndex = 10;
            _lblTotalTitle.Text = "Total Stats";
            // 
            // _lblTotalSubTotal
            // 
            _lblTotalSubTotal.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalSubTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalSubTotal.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblTotalSubTotal.Location = new System.Drawing.Point(10, 52);
            _lblTotalSubTotal.Name = "_lblTotalSubTotal";
            _lblTotalSubTotal.Size = new System.Drawing.Size(29, 16);
            _lblTotalSubTotal.TabIndex = 11;
            _lblTotalSubTotal.Text = "Total";
            // 
            // _lblTotalSubPass
            // 
            _lblTotalSubPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalSubPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalSubPass.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblTotalSubPass.Location = new System.Drawing.Point(105, 52);
            _lblTotalSubPass.Name = "_lblTotalSubPass";
            _lblTotalSubPass.Size = new System.Drawing.Size(30, 16);
            _lblTotalSubPass.TabIndex = 12;
            _lblTotalSubPass.Text = "PASS";
            // 
            // _lblTotalSubFail
            // 
            _lblTotalSubFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalSubFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalSubFail.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblTotalSubFail.Location = new System.Drawing.Point(200, 52);
            _lblTotalSubFail.Name = "_lblTotalSubFail";
            _lblTotalSubFail.Size = new System.Drawing.Size(25, 16);
            _lblTotalSubFail.TabIndex = 13;
            _lblTotalSubFail.Text = "FAIL";
            // 
            // _lblTotalSubYield
            // 
            _lblTotalSubYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblTotalSubYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            _lblTotalSubYield.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblTotalSubYield.Location = new System.Drawing.Point(295, 52);
            _lblTotalSubYield.Name = "_lblTotalSubYield";
            _lblTotalSubYield.Size = new System.Drawing.Size(29, 16);
            _lblTotalSubYield.TabIndex = 14;
            _lblTotalSubYield.Text = "Yield";
            // 
            // _chartPanel
            // 
            _chartPanel.AxisColor = null;
            _chartPanel.Dock = System.Windows.Forms.DockStyle.Top;
            _chartPanel.EnableAnimation = false;
            _chartPanel.GridColor = null;
            _chartPanel.LegendBackColor = null;
            _chartPanel.LegendBorderColor = null;
            _chartPanel.Location = new System.Drawing.Point(52, 140);
            _chartPanel.Margin = new System.Windows.Forms.Padding(10);
            _chartPanel.Name = "_chartPanel";
            _chartPanel.PieColors = null;
            _chartPanel.Size = new System.Drawing.Size(1370, 115);
            _chartPanel.TabIndex = 11;
            _chartPanel.Title = "Today's Production  (click hour for details)";
            _chartPanel.TitleColor = null;
            _chartPanel.TitleFont = null;
            _chartPanel.XAxisLabelFormat = null;
            _chartPanel.XMax = 24D;
            _chartPanel.XMin = 0D;
            _chartPanel.YAxisLabelFormat = null;
            _chartPanel.YMax = null;
            _chartPanel.YMin = 0D;
            _chartPanel.PointClick += Chart_PointClick;
            // 
            // _barcodePanel
            // 
            _barcodePanel.Controls.Add(_barcodeInput);
            _barcodePanel.Controls.Add(_lblBarcodeStatus);
            _barcodePanel.Controls.Add(_lblScannerStatus);
            _barcodePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            _barcodePanel.ForeColor = System.Drawing.Color.Transparent;
            _barcodePanel.Location = new System.Drawing.Point(52, 574);
            _barcodePanel.Name = "_barcodePanel";
            _barcodePanel.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            _barcodePanel.Size = new System.Drawing.Size(1370, 46);
            _barcodePanel.TabIndex = 13;
            // 
            // _barcodeInput
            // 
            _barcodeInput.Font = new System.Drawing.Font("Consolas", 12F);
            _barcodeInput.Location = new System.Drawing.Point(13, 0);
            _barcodeInput.Name = "_barcodeInput";
            _barcodeInput.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            _barcodeInput.PlaceholderText = "Scan barcode / enter SN...";
            _barcodeInput.PrefixSvg = "ScanOutlined";
            _barcodeInput.Size = new System.Drawing.Size(420, 44);
            _barcodeInput.TabIndex = 0;
            _barcodeInput.TextChanged += BarcodeInput_TextChanged;
            _barcodeInput.KeyDown += BarcodeInput_KeyDown;
            // 
            // _lblBarcodeStatus
            // 
            _lblBarcodeStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            _lblBarcodeStatus.ForeColor = System.Drawing.Color.FromArgb(136, 153, 170);
            _lblBarcodeStatus.Location = new System.Drawing.Point(436, 8);
            _lblBarcodeStatus.Name = "_lblBarcodeStatus";
            _lblBarcodeStatus.Size = new System.Drawing.Size(288, 31);
            _lblBarcodeStatus.TabIndex = 1;
            _lblBarcodeStatus.Text = "格式:  等待输入...";
            _lblBarcodeStatus.TextMultiLine = false;
            // 
            // _lblScannerStatus
            // 
            _lblScannerStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            _lblScannerStatus.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblScannerStatus.Location = new System.Drawing.Point(730, 8);
            _lblScannerStatus.Name = "_lblScannerStatus";
            _lblScannerStatus.Size = new System.Drawing.Size(150, 31);
            _lblScannerStatus.TabIndex = 2;
            _lblScannerStatus.Text = "📡 Offline";
            // 
            // _statusBar
            // 
            _statusBar.Controls.Add(_lblStatusLeft);
            _statusBar.Controls.Add(_lblStatusRight);
            _statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            _statusBar.ForeColor = System.Drawing.Color.Transparent;
            _statusBar.Location = new System.Drawing.Point(52, 745);
            _statusBar.Name = "_statusBar";
            _statusBar.Padding = new System.Windows.Forms.Padding(16, 2, 16, 2);
            _statusBar.Size = new System.Drawing.Size(1370, 28);
            _statusBar.TabIndex = 15;
            // 
            // _lblStatusLeft
            // 
            _lblStatusLeft.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblStatusLeft.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            _lblStatusLeft.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblStatusLeft.Location = new System.Drawing.Point(16, 5);
            _lblStatusLeft.Name = "_lblStatusLeft";
            _lblStatusLeft.Size = new System.Drawing.Size(47, 16);
            _lblStatusLeft.TabIndex = 0;
            _lblStatusLeft.Text = "● Ready";
            // 
            // _lblStatusRight
            // 
            _lblStatusRight.AutoSizeMode = AntdUI.TAutoSize.Auto;
            _lblStatusRight.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            _lblStatusRight.ForeColor = System.Drawing.Color.FromArgb(102, 119, 136);
            _lblStatusRight.Location = new System.Drawing.Point(1200, 5);
            _lblStatusRight.Name = "_lblStatusRight";
            _lblStatusRight.Size = new System.Drawing.Size(0, 16);
            _lblStatusRight.TabIndex = 1;
            // 
            // _bottomPanel
            // 
            _bottomPanel.Controls.Add(_btnLoad);
            _bottomPanel.Controls.Add(_btnStart);
            _bottomPanel.Controls.Add(_btnSave);
            _bottomPanel.Controls.Add(button1);
            _bottomPanel.Controls.Add(_btnEditSproj);
            _bottomPanel.Controls.Add(progress3);
            _bottomPanel.Controls.Add(chkSaveExcel);
            _bottomPanel.Controls.Add(chkSaveAppend);
            _bottomPanel.Controls.Add(chkStopOnFail);
            _bottomPanel.Controls.Add(log_append);
            _bottomPanel.Controls.Add(_pwdInput);
            _bottomPanel.Controls.Add(_alertStatus);
            _bottomPanel.Controls.Add(iconState);
            _bottomPanel.Controls.Add(tBadge7);
            _bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            _bottomPanel.ForeColor = System.Drawing.Color.Transparent;
            _bottomPanel.Location = new System.Drawing.Point(52, 620);
            _bottomPanel.Name = "_bottomPanel";
            _bottomPanel.Size = new System.Drawing.Size(1370, 125);
            _bottomPanel.TabIndex = 14;
            // 
            // _alertStatus
            // 
            _alertStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            _alertStatus.Location = new System.Drawing.Point(857, 56);
            _alertStatus.Name = "_alertStatus";
            _alertStatus.Size = new System.Drawing.Size(510, 63);
            _alertStatus.TabIndex = 14;
            _alertStatus.Text = "SN: --- | Waiting...";
            _alertStatus.TextTitle = "System Ready";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(1422, 773);
            Controls.Add(_table);
            Controls.Add(_chartPanel);
            Controls.Add(_statsPanel);
            Controls.Add(_barcodePanel);
            Controls.Add(_bottomPanel);
            Controls.Add(_statusBar);
            Controls.Add(menu1);
            Controls.Add(windowBar);
            Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            ForeColor = System.Drawing.Color.White;
            Name = "MainForm";
            Shown += MainForm_Shown;
            windowBar.ResumeLayout(false);
            _statsPanel.ResumeLayout(false);
            panel_Project_name.ResumeLayout(false);
            panel_Project_name.PerformLayout();
            _cardOperator.ResumeLayout(false);
            _cardOperator.PerformLayout();
            _cardHourly.ResumeLayout(false);
            _cardHourly.PerformLayout();
            _barHourBg.ResumeLayout(false);
            _cardTotal.ResumeLayout(false);
            _cardTotal.PerformLayout();
            _barTotalBg.ResumeLayout(false);
            _barcodePanel.ResumeLayout(false);
            _statusBar.ResumeLayout(false);
            _statusBar.PerformLayout();
            _bottomPanel.ResumeLayout(false);
            ResumeLayout(false);

        }

        // ═══ Existing Controls ═══════════════════════════
        private AntdUI.Table _table;
        private AntdUI.Button _btnLoad;
        private AntdUI.Button _btnStart;
        private AntdUI.Button _btnSave;
        private AntdUI.Checkbox chkSaveExcel;
        private AntdUI.Checkbox chkSaveAppend;
        private AntdUI.Checkbox chkStopOnFail;
        private AntdUI.Progress progress3;
        private AntdUI.Button button1;
        private AntdUI.Button _btnEditSproj;
        private AntdUI.IconState iconState;
        private AntdUI.Badge tBadge7;
        private AntdUI.PageHeader windowBar;
        private AntdUI.Input txt_search;
        private AntdUI.ColorPicker colorTheme;
        private AntdUI.Button btn_mode;
        private AntdUI.Dropdown btn_global;
        private AntdUI.Button btn_setting;
        private AntdUI.Dropdown btn_more;
        private AntdUI.Checkbox log_append;
        private AntdUI.Input _pwdInput;
        private AntdUI.Menu menu1;

        // ═══ New Controls ════════════════════════════════
        private AntdUI.Panel _statsPanel;
        private AntdUI.Chart _chartPanel;
        private AntdUI.Panel _barcodePanel;
        private AntdUI.Input _barcodeInput;
        private AntdUI.Label _lblBarcodeStatus;
        private AntdUI.Panel _statusBar;
        private AntdUI.Label _lblStatusLeft;
        private AntdUI.Label _lblStatusRight;
        private AntdUI.Panel _bottomPanel;

        // ═══ Alert Status ══════════════════════════════
        private AntdUI.Alert _alertStatus;

        // ═══ Stat Card Controls ═════════════════════════
        private AntdUI.Panel _cardTotal;
        private AntdUI.Panel _cardHourly;
        private AntdUI.Panel _cardOperator;
        private AntdUI.Label _lblTotalCount;
        private AntdUI.Label _lblTotalPass;
        private AntdUI.Label _lblTotalFail;
        private AntdUI.Label _lblTotalYield;
        private AntdUI.Panel _barTotalBg;
        private AntdUI.Panel _barTotalPass;
        private AntdUI.Label _lblHourTitle;
        private AntdUI.Label _lblHourCount;
        private AntdUI.Label _lblHourPass;
        private AntdUI.Label _lblHourFail;
        private AntdUI.Label _lblHourYield;
        private AntdUI.Panel _barHourBg;
        private AntdUI.Panel _barHourPass;
        private AntdUI.Panel _barHourFail;
        private AntdUI.Label _lblOpId;
        private AntdUI.Label _lblOpShift;
        private AntdUI.Label _lblOpTime;

        // ═══ Static Subtitle Labels ═════════════════════
        private AntdUI.Label _lblTotalTitle;
        private AntdUI.Label _lblTotalSubTotal;
        private AntdUI.Label _lblTotalSubPass;
        private AntdUI.Label _lblTotalSubFail;
        private AntdUI.Label _lblTotalSubYield;
        private AntdUI.Label _lblHourSubTotal;
        private AntdUI.Label _lblHourSubPass;
        private AntdUI.Label _lblHourSubFail;
        private AntdUI.Label _lblHourSubYield;
        private AntdUI.Label _lblOpTitle;
        private AntdUI.Label _lblScannerStatus;
        private AntdUI.Panel panel_Project_name;
        private AntdUI.Label label3;
        private AntdUI.Label label4;
    }
}
