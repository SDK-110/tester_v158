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
            this._table = new AntdUI.Table();
            this._btnLoad = new AntdUI.Button();
            this._btnStart = new AntdUI.Button();
            this._btnSave = new AntdUI.Button();
            this.chkSaveExcel = new AntdUI.Checkbox();
            this.chkSaveAppend = new AntdUI.Checkbox();
            this.chkStopOnFail = new AntdUI.Checkbox();
            this.progress3 = new AntdUI.Progress();
            this.button1 = new AntdUI.Button();
            this._btnEditSproj = new AntdUI.Button();
            this.iconState = new AntdUI.IconState();
            this.tBadge7 = new AntdUI.Badge();
            this.windowBar = new AntdUI.PageHeader();
            this.txt_search = new AntdUI.Input();
            this.colorTheme = new AntdUI.ColorPicker();
            this.btn_mode = new AntdUI.Button();
            this.btn_global = new AntdUI.Dropdown();
            this.btn_setting = new AntdUI.Button();
            this.btn_more = new AntdUI.Dropdown();
            this.log_append = new AntdUI.Checkbox();
            this._pwdInput = new AntdUI.Input();
            this.menu1 = new AntdUI.Menu();
            this._statsPanel = new AntdUI.Panel();
            this.panel_Project_name = new AntdUI.Panel();
            this.label3 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this._cardOperator = new AntdUI.Panel();
            this._lblOpTime = new AntdUI.Label();
            this._lblOpShift = new AntdUI.Label();
            this._lblOpId = new AntdUI.Label();
            this._lblOpTitle = new AntdUI.Label();
            this._cardHourly = new AntdUI.Panel();
            this._barHourBg = new AntdUI.Panel();
            this._barHourPass = new AntdUI.Panel();
            this._barHourFail = new AntdUI.Panel();
            this._lblHourYield = new AntdUI.Label();
            this._lblHourFail = new AntdUI.Label();
            this._lblHourPass = new AntdUI.Label();
            this._lblHourCount = new AntdUI.Label();
            this._lblHourTitle = new AntdUI.Label();
            this._lblHourSubTotal = new AntdUI.Label();
            this._lblHourSubPass = new AntdUI.Label();
            this._lblHourSubFail = new AntdUI.Label();
            this._lblHourSubYield = new AntdUI.Label();
            this._cardTotal = new AntdUI.Panel();
            this._barTotalBg = new AntdUI.Panel();
            this._barTotalPass = new AntdUI.Panel();
            this._lblTotalYield = new AntdUI.Label();
            this._lblTotalFail = new AntdUI.Label();
            this._lblTotalPass = new AntdUI.Label();
            this._lblTotalCount = new AntdUI.Label();
            this._lblTotalTitle = new AntdUI.Label();
            this._lblTotalSubTotal = new AntdUI.Label();
            this._lblTotalSubPass = new AntdUI.Label();
            this._lblTotalSubFail = new AntdUI.Label();
            this._lblTotalSubYield = new AntdUI.Label();
            this._chartPanel = new AntdUI.Chart();
            this._barcodePanel = new AntdUI.Panel();
            this._barcodeInput = new AntdUI.Input();
            this._lblBarcodeStatus = new AntdUI.Label();
            this._lblScannerStatus = new AntdUI.Label();
            this._statusBar = new AntdUI.Panel();
            this._lblStatusLeft = new AntdUI.Label();
            this._lblStatusRight = new AntdUI.Label();
            this._bottomPanel = new AntdUI.Panel();
            this._alertStatus = new AntdUI.Alert();
            this.windowBar.SuspendLayout();
            this._statsPanel.SuspendLayout();
            this.panel_Project_name.SuspendLayout();
            this._cardOperator.SuspendLayout();
            this._cardHourly.SuspendLayout();
            this._barHourBg.SuspendLayout();
            this._cardTotal.SuspendLayout();
            this._barTotalBg.SuspendLayout();
            this._barcodePanel.SuspendLayout();
            this._statusBar.SuspendLayout();
            this._bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _table
            // 
            this._table.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            this._table.BackColor = System.Drawing.Color.Transparent;
            this._table.Bordered = true;
            this._table.Dock = System.Windows.Forms.DockStyle.Fill;
            this._table.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this._table.Gap = 12;
            this._table.Location = new System.Drawing.Point(52, 255);
            this._table.Name = "_table";
            this._table.Padding = new System.Windows.Forms.Padding(10);
            this._table.RowHeight = 35;
            this._table.Size = new System.Drawing.Size(1370, 319);
            this._table.TabIndex = 10;
            // 
            // _btnLoad
            // 
            this._btnLoad.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this._btnLoad.Location = new System.Drawing.Point(12, 10);
            this._btnLoad.Name = "_btnLoad";
            this._btnLoad.Size = new System.Drawing.Size(120, 35);
            this._btnLoad.TabIndex = 0;
            this._btnLoad.Text = "📂 Load";
            this._btnLoad.Type = AntdUI.TTypeMini.Primary;
            this._btnLoad.Click += new System.EventHandler(this._btnLoad_Click);
            // 
            // _btnStart
            // 
            this._btnStart.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this._btnStart.Location = new System.Drawing.Point(140, 10);
            this._btnStart.Name = "_btnStart";
            this._btnStart.Size = new System.Drawing.Size(120, 35);
            this._btnStart.TabIndex = 1;
            this._btnStart.Text = "▶ Start";
            this._btnStart.Type = AntdUI.TTypeMini.Primary;
            this._btnStart.Click += new System.EventHandler(this._btnStart_Click);
            // 
            // _btnSave
            // 
            this._btnSave.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this._btnSave.Location = new System.Drawing.Point(268, 10);
            this._btnSave.Name = "_btnSave";
            this._btnSave.Size = new System.Drawing.Size(120, 35);
            this._btnSave.TabIndex = 2;
            this._btnSave.Text = "💾 Save";
            this._btnSave.Type = AntdUI.TTypeMini.Primary;
            this._btnSave.Click += new System.EventHandler(this._btnSave_Click);
            // 
            // chkSaveExcel
            // 
            this.chkSaveExcel.Checked = true;
            this.chkSaveExcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveExcel.Enabled = false;
            this.chkSaveExcel.Location = new System.Drawing.Point(12, 95);
            this.chkSaveExcel.Name = "chkSaveExcel";
            this.chkSaveExcel.Size = new System.Drawing.Size(130, 20);
            this.chkSaveExcel.TabIndex = 8;
            this.chkSaveExcel.Text = "Save Excel";
            this.chkSaveExcel.CheckedChanged += new AntdUI.BoolEventHandler(this.ChkSaveExcel_CheckedChanged);
            // 
            // chkSaveAppend
            // 
            this.chkSaveAppend.Enabled = false;
            this.chkSaveAppend.Location = new System.Drawing.Point(148, 95);
            this.chkSaveAppend.Name = "chkSaveAppend";
            this.chkSaveAppend.Size = new System.Drawing.Size(100, 20);
            this.chkSaveAppend.TabIndex = 9;
            this.chkSaveAppend.Text = "Append";
            this.chkSaveAppend.CheckedChanged += new AntdUI.BoolEventHandler(this.chkSaveAppend_CheckedChanged);
            // 
            // chkStopOnFail
            // 
            this.chkStopOnFail.Enabled = false;
            this.chkStopOnFail.Location = new System.Drawing.Point(254, 95);
            this.chkStopOnFail.Name = "chkStopOnFail";
            this.chkStopOnFail.Size = new System.Drawing.Size(110, 20);
            this.chkStopOnFail.TabIndex = 10;
            this.chkStopOnFail.Text = "Stop on Fail";
            this.chkStopOnFail.CheckedChanged += new AntdUI.BoolEventHandler(this.chkStopOnFail_CheckedChanged);
            // 
            // progress3
            // 
            this.progress3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress3.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.progress3.Location = new System.Drawing.Point(12, 56);
            this.progress3.Name = "progress3";
            this.progress3.Size = new System.Drawing.Size(839, 28);
            this.progress3.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.button1.Location = new System.Drawing.Point(396, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 35);
            this.button1.TabIndex = 3;
            this.button1.Text = "⏹ Stop";
            this.button1.ToggleType = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            //
            // _btnEditSproj
            //
            this._btnEditSproj.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this._btnEditSproj.Location = new System.Drawing.Point(502, 10);
            this._btnEditSproj.Name = "_btnEditSproj";
            this._btnEditSproj.Size = new System.Drawing.Size(120, 35);
            this._btnEditSproj.TabIndex = 20;
            this._btnEditSproj.Text = "📝 Edit";
            this._btnEditSproj.Type = AntdUI.TTypeMini.Primary;
            this._btnEditSproj.Click += new System.EventHandler(this._btnEditSproj_Click);
            //
            // iconState
            // 
            this.iconState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.iconState.Location = new System.Drawing.Point(3522, 36);
            this.iconState.Name = "iconState";
            this.iconState.Size = new System.Drawing.Size(48, 65);
            this.iconState.State = AntdUI.TType.Error;
            this.iconState.TabIndex = 12;
            // 
            // tBadge7
            // 
            this.tBadge7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tBadge7.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F);
            this.tBadge7.Location = new System.Drawing.Point(3382, 10);
            this.tBadge7.Name = "tBadge7";
            this.tBadge7.Size = new System.Drawing.Size(132, 105);
            this.tBadge7.State = AntdUI.TState.Processing;
            this.tBadge7.TabIndex = 13;
            // 
            // windowBar
            // 
            this.windowBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.windowBar.Controls.Add(this.txt_search);
            this.windowBar.Controls.Add(this.colorTheme);
            this.windowBar.Controls.Add(this.btn_mode);
            this.windowBar.Controls.Add(this.btn_global);
            this.windowBar.Controls.Add(this.btn_setting);
            this.windowBar.Controls.Add(this.btn_more);
            this.windowBar.DividerMargin = 3;
            this.windowBar.DividerShow = true;
            this.windowBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.windowBar.IconSvg = "AndroidOutlined";
            this.windowBar.Location = new System.Drawing.Point(0, 0);
            this.windowBar.Name = "windowBar";
            this.windowBar.ShowButton = true;
            this.windowBar.ShowIcon = true;
            this.windowBar.Size = new System.Drawing.Size(1422, 40);
            this.windowBar.SubText = "PCBA Test Platform";
            this.windowBar.TabIndex = 18;
            this.windowBar.Text = "Tester";
            this.windowBar.Click += new System.EventHandler(this.windowBar_Click);
            // 
            // txt_search
            // 
            this.txt_search.Dock = System.Windows.Forms.DockStyle.Right;
            this.txt_search.Location = new System.Drawing.Point(884, 0);
            this.txt_search.Name = "txt_search";
            this.txt_search.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.txt_search.PlaceholderText = "Search...";
            this.txt_search.PrefixSvg = "SearchOutlined";
            this.txt_search.Size = new System.Drawing.Size(170, 40);
            this.txt_search.TabIndex = 1;
            // 
            // colorTheme
            // 
            this.colorTheme.Dock = System.Windows.Forms.DockStyle.Right;
            this.colorTheme.Location = new System.Drawing.Point(1054, 0);
            this.colorTheme.Name = "colorTheme";
            this.colorTheme.Padding = new System.Windows.Forms.Padding(5);
            this.colorTheme.Size = new System.Drawing.Size(40, 40);
            this.colorTheme.TabIndex = 2;
            // 
            // btn_mode
            // 
            this.btn_mode.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_mode.Ghost = true;
            this.btn_mode.IconSvg = "SunOutlined";
            this.btn_mode.Location = new System.Drawing.Point(1094, 0);
            this.btn_mode.Name = "btn_mode";
            this.btn_mode.Radius = 0;
            this.btn_mode.Size = new System.Drawing.Size(46, 40);
            this.btn_mode.TabIndex = 3;
            this.btn_mode.ToggleIconSvg = "MoonOutlined";
            this.btn_mode.WaveSize = 0;
            this.btn_mode.Click += new System.EventHandler(this.BtnMode_Click);
            // 
            // btn_global
            // 
            this.btn_global.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_global.DropDownRadius = 6;
            this.btn_global.Ghost = true;
            this.btn_global.IconSvg = "GlobalOutlined";
            this.btn_global.Location = new System.Drawing.Point(1140, 0);
            this.btn_global.Name = "btn_global";
            this.btn_global.Placement = AntdUI.TAlignFrom.BR;
            this.btn_global.Radius = 0;
            this.btn_global.Size = new System.Drawing.Size(46, 40);
            this.btn_global.TabIndex = 4;
            this.btn_global.WaveSize = 0;
            // 
            // btn_setting
            // 
            this.btn_setting.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_setting.Ghost = true;
            this.btn_setting.IconSvg = "SettingOutlined";
            this.btn_setting.Location = new System.Drawing.Point(1186, 0);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Radius = 0;
            this.btn_setting.Size = new System.Drawing.Size(46, 40);
            this.btn_setting.TabIndex = 5;
            this.btn_setting.WaveSize = 0;
            // 
            // btn_more
            // 
            this.btn_more.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_more.DropDownRadius = 6;
            this.btn_more.Ghost = true;
            this.btn_more.IconSvg = "MoreOutlined";
            this.btn_more.Location = new System.Drawing.Point(1232, 0);
            this.btn_more.Name = "btn_more";
            this.btn_more.Placement = AntdUI.TAlignFrom.BR;
            this.btn_more.Radius = 0;
            this.btn_more.Size = new System.Drawing.Size(46, 40);
            this.btn_more.TabIndex = 6;
            this.btn_more.WaveSize = 0;
            // 
            // log_append
            // 
            this.log_append.Enabled = false;
            this.log_append.Location = new System.Drawing.Point(370, 95);
            this.log_append.Name = "log_append";
            this.log_append.Size = new System.Drawing.Size(90, 20);
            this.log_append.TabIndex = 11;
            this.log_append.Text = "Log Append";
            this.log_append.CheckedChanged += new AntdUI.BoolEventHandler(this.log_append_CheckedChanged);
            // 
            // _pwdInput
            // 
            this._pwdInput.Location = new System.Drawing.Point(470, 88);
            this._pwdInput.Name = "_pwdInput";
            this._pwdInput.PlaceholderText = "修改密码";
            this._pwdInput.Size = new System.Drawing.Size(120, 37);
            this._pwdInput.TabIndex = 12;
            this._pwdInput.UseSystemPasswordChar = true;
            this._pwdInput.TextChanged += new System.EventHandler(this.PwdInput_TextChanged);
            // 
            // menu1
            // 
            this.menu1.Collapsed = true;
            this.menu1.Dock = System.Windows.Forms.DockStyle.Left;
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
            this.menu1.Items.Add(menuItem10);
            this.menu1.Items.Add(menuItem11);
            this.menu1.Items.Add(menuItem12);
            this.menu1.Items.Add(menuItem13);
            this.menu1.Items.Add(menuItem14);
            this.menu1.Items.Add(menuItem15);
            this.menu1.Items.Add(menuItem16);
            this.menu1.Location = new System.Drawing.Point(0, 40);
            this.menu1.Name = "menu1";
            this.menu1.ScrollBarBlock = true;
            this.menu1.Size = new System.Drawing.Size(52, 733);
            this.menu1.TabIndex = 20;
            this.menu1.SelectChanged += new AntdUI.SelectEventHandler(this.Menu1_SelectChanged);
            this.menu1.MouseEnter += new System.EventHandler(this.Menu1_MouseEnter);
            this.menu1.MouseLeave += new System.EventHandler(this.Menu1_MouseLeave);
            // 
            // _statsPanel
            // 
            this._statsPanel.Controls.Add(this.panel_Project_name);
            this._statsPanel.Controls.Add(this._cardOperator);
            this._statsPanel.Controls.Add(this._cardHourly);
            this._statsPanel.Controls.Add(this._cardTotal);
            this._statsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._statsPanel.ForeColor = System.Drawing.Color.Transparent;
            this._statsPanel.Location = new System.Drawing.Point(52, 40);
            this._statsPanel.Name = "_statsPanel";
            this._statsPanel.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this._statsPanel.Size = new System.Drawing.Size(1370, 100);
            this._statsPanel.TabIndex = 12;
            // 
            // panel_Project_name
            // 
            this.panel_Project_name.Controls.Add(this.label3);
            this.panel_Project_name.Controls.Add(this.label4);
            this.panel_Project_name.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_Project_name.Location = new System.Drawing.Point(1058, 8);
            this.panel_Project_name.Name = "panel_Project_name";
            this.panel_Project_name.Size = new System.Drawing.Size(300, 84);
            this.panel_Project_name.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei UI", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(174, 37);
            this.label3.TabIndex = 2;
            this.label3.Text = "Not Logged";
            // 
            // label4
            // 
            this.label4.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this.label4.Location = new System.Drawing.Point(12, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "Mode";
            // 
            // _cardOperator
            // 
            this._cardOperator.Controls.Add(this._lblOpTime);
            this._cardOperator.Controls.Add(this._lblOpShift);
            this._cardOperator.Controls.Add(this._lblOpId);
            this._cardOperator.Controls.Add(this._lblOpTitle);
            this._cardOperator.Location = new System.Drawing.Point(844, 0);
            this._cardOperator.Name = "_cardOperator";
            this._cardOperator.Size = new System.Drawing.Size(246, 100);
            this._cardOperator.TabIndex = 2;
            // 
            // _lblOpTime
            // 
            this._lblOpTime.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblOpTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblOpTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this._lblOpTime.Location = new System.Drawing.Point(80, 52);
            this._lblOpTime.Name = "_lblOpTime";
            this._lblOpTime.Size = new System.Drawing.Size(63, 19);
            this._lblOpTime.TabIndex = 0;
            this._lblOpTime.Text = "00:00:00";
            // 
            // _lblOpShift
            // 
            this._lblOpShift.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblOpShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblOpShift.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(167)))), ((int)(((byte)(38)))));
            this._lblOpShift.Location = new System.Drawing.Point(14, 52);
            this._lblOpShift.Name = "_lblOpShift";
            this._lblOpShift.Size = new System.Drawing.Size(25, 17);
            this._lblOpShift.TabIndex = 1;
            this._lblOpShift.Text = "Day";
            // 
            // _lblOpId
            // 
            this._lblOpId.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblOpId.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblOpId.ForeColor = System.Drawing.Color.White;
            this._lblOpId.Location = new System.Drawing.Point(14, 30);
            this._lblOpId.Name = "_lblOpId";
            this._lblOpId.Size = new System.Drawing.Size(88, 19);
            this._lblOpId.TabIndex = 2;
            this._lblOpId.Text = "Not Logged";
            // 
            // _lblOpTitle
            // 
            this._lblOpTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblOpTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblOpTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this._lblOpTitle.Location = new System.Drawing.Point(12, 6);
            this._lblOpTitle.Name = "_lblOpTitle";
            this._lblOpTitle.Size = new System.Drawing.Size(70, 21);
            this._lblOpTitle.TabIndex = 10;
            this._lblOpTitle.Text = "Operator";
            // 
            // _cardHourly
            // 
            this._cardHourly.Controls.Add(this._barHourBg);
            this._cardHourly.Controls.Add(this._lblHourYield);
            this._cardHourly.Controls.Add(this._lblHourFail);
            this._cardHourly.Controls.Add(this._lblHourPass);
            this._cardHourly.Controls.Add(this._lblHourCount);
            this._cardHourly.Controls.Add(this._lblHourTitle);
            this._cardHourly.Controls.Add(this._lblHourSubTotal);
            this._cardHourly.Controls.Add(this._lblHourSubPass);
            this._cardHourly.Controls.Add(this._lblHourSubFail);
            this._cardHourly.Controls.Add(this._lblHourSubYield);
            this._cardHourly.Location = new System.Drawing.Point(432, 0);
            this._cardHourly.Name = "_cardHourly";
            this._cardHourly.Size = new System.Drawing.Size(406, 100);
            this._cardHourly.TabIndex = 1;
            // 
            // _barHourBg
            // 
            this._barHourBg.Back = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(68)))));
            this._barHourBg.Controls.Add(this._barHourPass);
            this._barHourBg.Controls.Add(this._barHourFail);
            this._barHourBg.Location = new System.Drawing.Point(10, 79);
            this._barHourBg.Name = "_barHourBg";
            this._barHourBg.Size = new System.Drawing.Size(387, 4);
            this._barHourBg.TabIndex = 0;
            // 
            // _barHourPass
            // 
            this._barHourPass.Back = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(187)))), ((int)(((byte)(106)))));
            this._barHourPass.Location = new System.Drawing.Point(0, 0);
            this._barHourPass.Name = "_barHourPass";
            this._barHourPass.Size = new System.Drawing.Size(0, 4);
            this._barHourPass.TabIndex = 0;
            // 
            // _barHourFail
            // 
            this._barHourFail.Back = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this._barHourFail.Location = new System.Drawing.Point(0, 0);
            this._barHourFail.Name = "_barHourFail";
            this._barHourFail.Size = new System.Drawing.Size(0, 4);
            this._barHourFail.TabIndex = 1;
            // 
            // _lblHourYield
            // 
            this._lblHourYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this._lblHourYield.Location = new System.Drawing.Point(295, 30);
            this._lblHourYield.Name = "_lblHourYield";
            this._lblHourYield.Size = new System.Drawing.Size(31, 26);
            this._lblHourYield.TabIndex = 1;
            this._lblHourYield.Text = "0%";
            // 
            // _lblHourFail
            // 
            this._lblHourFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this._lblHourFail.Location = new System.Drawing.Point(200, 30);
            this._lblHourFail.Name = "_lblHourFail";
            this._lblHourFail.Size = new System.Drawing.Size(14, 29);
            this._lblHourFail.TabIndex = 2;
            this._lblHourFail.Text = "0";
            // 
            // _lblHourPass
            // 
            this._lblHourPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(187)))), ((int)(((byte)(106)))));
            this._lblHourPass.Location = new System.Drawing.Point(105, 30);
            this._lblHourPass.Name = "_lblHourPass";
            this._lblHourPass.Size = new System.Drawing.Size(14, 29);
            this._lblHourPass.TabIndex = 3;
            this._lblHourPass.Text = "0";
            // 
            // _lblHourCount
            // 
            this._lblHourCount.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this._lblHourCount.Location = new System.Drawing.Point(10, 30);
            this._lblHourCount.Name = "_lblHourCount";
            this._lblHourCount.Size = new System.Drawing.Size(14, 29);
            this._lblHourCount.TabIndex = 4;
            this._lblHourCount.Text = "0";
            // 
            // _lblHourTitle
            // 
            this._lblHourTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this._lblHourTitle.Location = new System.Drawing.Point(12, 6);
            this._lblHourTitle.Name = "_lblHourTitle";
            this._lblHourTitle.Size = new System.Drawing.Size(81, 21);
            this._lblHourTitle.TabIndex = 5;
            this._lblHourTitle.Text = "⏱ Hourly";
            // 
            // _lblHourSubTotal
            // 
            this._lblHourSubTotal.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourSubTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourSubTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblHourSubTotal.Location = new System.Drawing.Point(10, 52);
            this._lblHourSubTotal.Name = "_lblHourSubTotal";
            this._lblHourSubTotal.Size = new System.Drawing.Size(29, 16);
            this._lblHourSubTotal.TabIndex = 10;
            this._lblHourSubTotal.Text = "Hour";
            // 
            // _lblHourSubPass
            // 
            this._lblHourSubPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourSubPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourSubPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblHourSubPass.Location = new System.Drawing.Point(105, 52);
            this._lblHourSubPass.Name = "_lblHourSubPass";
            this._lblHourSubPass.Size = new System.Drawing.Size(30, 16);
            this._lblHourSubPass.TabIndex = 11;
            this._lblHourSubPass.Text = "PASS";
            // 
            // _lblHourSubFail
            // 
            this._lblHourSubFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourSubFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourSubFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblHourSubFail.Location = new System.Drawing.Point(200, 52);
            this._lblHourSubFail.Name = "_lblHourSubFail";
            this._lblHourSubFail.Size = new System.Drawing.Size(25, 16);
            this._lblHourSubFail.TabIndex = 12;
            this._lblHourSubFail.Text = "FAIL";
            // 
            // _lblHourSubYield
            // 
            this._lblHourSubYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblHourSubYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblHourSubYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblHourSubYield.Location = new System.Drawing.Point(295, 52);
            this._lblHourSubYield.Name = "_lblHourSubYield";
            this._lblHourSubYield.Size = new System.Drawing.Size(29, 16);
            this._lblHourSubYield.TabIndex = 13;
            this._lblHourSubYield.Text = "Yield";
            // 
            // _cardTotal
            // 
            this._cardTotal.Controls.Add(this._barTotalBg);
            this._cardTotal.Controls.Add(this._lblTotalYield);
            this._cardTotal.Controls.Add(this._lblTotalFail);
            this._cardTotal.Controls.Add(this._lblTotalPass);
            this._cardTotal.Controls.Add(this._lblTotalCount);
            this._cardTotal.Controls.Add(this._lblTotalTitle);
            this._cardTotal.Controls.Add(this._lblTotalSubTotal);
            this._cardTotal.Controls.Add(this._lblTotalSubPass);
            this._cardTotal.Controls.Add(this._lblTotalSubFail);
            this._cardTotal.Controls.Add(this._lblTotalSubYield);
            this._cardTotal.Location = new System.Drawing.Point(0, 0);
            this._cardTotal.Name = "_cardTotal";
            this._cardTotal.Size = new System.Drawing.Size(396, 100);
            this._cardTotal.TabIndex = 0;
            // 
            // _barTotalBg
            // 
            this._barTotalBg.Back = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(68)))));
            this._barTotalBg.Controls.Add(this._barTotalPass);
            this._barTotalBg.Location = new System.Drawing.Point(10, 79);
            this._barTotalBg.Name = "_barTotalBg";
            this._barTotalBg.Size = new System.Drawing.Size(387, 4);
            this._barTotalBg.TabIndex = 0;
            // 
            // _barTotalPass
            // 
            this._barTotalPass.Back = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(187)))), ((int)(((byte)(106)))));
            this._barTotalPass.Location = new System.Drawing.Point(0, 0);
            this._barTotalPass.Name = "_barTotalPass";
            this._barTotalPass.Size = new System.Drawing.Size(0, 4);
            this._barTotalPass.TabIndex = 0;
            // 
            // _lblTotalYield
            // 
            this._lblTotalYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this._lblTotalYield.Location = new System.Drawing.Point(295, 30);
            this._lblTotalYield.Name = "_lblTotalYield";
            this._lblTotalYield.Size = new System.Drawing.Size(31, 26);
            this._lblTotalYield.TabIndex = 1;
            this._lblTotalYield.Text = "0%";
            // 
            // _lblTotalFail
            // 
            this._lblTotalFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this._lblTotalFail.Location = new System.Drawing.Point(200, 30);
            this._lblTotalFail.Name = "_lblTotalFail";
            this._lblTotalFail.Size = new System.Drawing.Size(14, 29);
            this._lblTotalFail.TabIndex = 2;
            this._lblTotalFail.Text = "0";
            // 
            // _lblTotalPass
            // 
            this._lblTotalPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(187)))), ((int)(((byte)(106)))));
            this._lblTotalPass.Location = new System.Drawing.Point(105, 30);
            this._lblTotalPass.Name = "_lblTotalPass";
            this._lblTotalPass.Size = new System.Drawing.Size(14, 29);
            this._lblTotalPass.TabIndex = 3;
            this._lblTotalPass.Text = "0";
            // 
            // _lblTotalCount
            // 
            this._lblTotalCount.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalCount.Font = new System.Drawing.Font("Microsoft YaHei UI", 17F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this._lblTotalCount.Location = new System.Drawing.Point(10, 30);
            this._lblTotalCount.Name = "_lblTotalCount";
            this._lblTotalCount.Size = new System.Drawing.Size(14, 29);
            this._lblTotalCount.TabIndex = 4;
            this._lblTotalCount.Text = "0";
            // 
            // _lblTotalTitle
            // 
            this._lblTotalTitle.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this._lblTotalTitle.Location = new System.Drawing.Point(12, 6);
            this._lblTotalTitle.Name = "_lblTotalTitle";
            this._lblTotalTitle.Size = new System.Drawing.Size(81, 21);
            this._lblTotalTitle.TabIndex = 10;
            this._lblTotalTitle.Text = "Total Stats";
            // 
            // _lblTotalSubTotal
            // 
            this._lblTotalSubTotal.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalSubTotal.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalSubTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblTotalSubTotal.Location = new System.Drawing.Point(10, 52);
            this._lblTotalSubTotal.Name = "_lblTotalSubTotal";
            this._lblTotalSubTotal.Size = new System.Drawing.Size(29, 16);
            this._lblTotalSubTotal.TabIndex = 11;
            this._lblTotalSubTotal.Text = "Total";
            // 
            // _lblTotalSubPass
            // 
            this._lblTotalSubPass.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalSubPass.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalSubPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblTotalSubPass.Location = new System.Drawing.Point(105, 52);
            this._lblTotalSubPass.Name = "_lblTotalSubPass";
            this._lblTotalSubPass.Size = new System.Drawing.Size(30, 16);
            this._lblTotalSubPass.TabIndex = 12;
            this._lblTotalSubPass.Text = "PASS";
            // 
            // _lblTotalSubFail
            // 
            this._lblTotalSubFail.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalSubFail.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalSubFail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblTotalSubFail.Location = new System.Drawing.Point(200, 52);
            this._lblTotalSubFail.Name = "_lblTotalSubFail";
            this._lblTotalSubFail.Size = new System.Drawing.Size(25, 16);
            this._lblTotalSubFail.TabIndex = 13;
            this._lblTotalSubFail.Text = "FAIL";
            // 
            // _lblTotalSubYield
            // 
            this._lblTotalSubYield.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblTotalSubYield.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this._lblTotalSubYield.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblTotalSubYield.Location = new System.Drawing.Point(295, 52);
            this._lblTotalSubYield.Name = "_lblTotalSubYield";
            this._lblTotalSubYield.Size = new System.Drawing.Size(29, 16);
            this._lblTotalSubYield.TabIndex = 14;
            this._lblTotalSubYield.Text = "Yield";
            // 
            // _chartPanel
            // 
            this._chartPanel.AxisColor = null;
            this._chartPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._chartPanel.EnableAnimation = false;
            this._chartPanel.GridColor = null;
            this._chartPanel.LegendBackColor = null;
            this._chartPanel.LegendBorderColor = null;
            this._chartPanel.Location = new System.Drawing.Point(52, 140);
            this._chartPanel.Margin = new System.Windows.Forms.Padding(10);
            this._chartPanel.Name = "_chartPanel";
            this._chartPanel.PieColors = null;
            this._chartPanel.Size = new System.Drawing.Size(1370, 115);
            this._chartPanel.TabIndex = 11;
            this._chartPanel.Title = "Today\'s Production  (click hour for details)";
            this._chartPanel.TitleColor = null;
            this._chartPanel.TitleFont = null;
            this._chartPanel.XAxisLabelFormat = null;
            this._chartPanel.XMax = 24D;
            this._chartPanel.XMin = 0D;
            this._chartPanel.YAxisLabelFormat = null;
            this._chartPanel.YMax = null;
            this._chartPanel.YMin = 0D;
            this._chartPanel.PointClick += new System.EventHandler<AntdUI.ChartPointClickEventArgs>(this.Chart_PointClick);
            // 
            // _barcodePanel
            // 
            this._barcodePanel.Controls.Add(this._barcodeInput);
            this._barcodePanel.Controls.Add(this._lblBarcodeStatus);
            this._barcodePanel.Controls.Add(this._lblScannerStatus);
            this._barcodePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._barcodePanel.ForeColor = System.Drawing.Color.Transparent;
            this._barcodePanel.Location = new System.Drawing.Point(52, 574);
            this._barcodePanel.Name = "_barcodePanel";
            this._barcodePanel.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this._barcodePanel.Size = new System.Drawing.Size(1370, 46);
            this._barcodePanel.TabIndex = 13;
            // 
            // _barcodeInput
            // 
            this._barcodeInput.Font = new System.Drawing.Font("Consolas", 12F);
            this._barcodeInput.Location = new System.Drawing.Point(13, 0);
            this._barcodeInput.Name = "_barcodeInput";
            this._barcodeInput.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this._barcodeInput.PlaceholderText = "Scan barcode / enter SN...";
            this._barcodeInput.PrefixSvg = "ScanOutlined";
            this._barcodeInput.Size = new System.Drawing.Size(420, 44);
            this._barcodeInput.TabIndex = 0;
            this._barcodeInput.TextChanged += new System.EventHandler(this.BarcodeInput_TextChanged);
            this._barcodeInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BarcodeInput_KeyDown);
            // 
            // _lblBarcodeStatus
            // 
            this._lblBarcodeStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this._lblBarcodeStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this._lblBarcodeStatus.Location = new System.Drawing.Point(436, 8);
            this._lblBarcodeStatus.Name = "_lblBarcodeStatus";
            this._lblBarcodeStatus.Size = new System.Drawing.Size(288, 31);
            this._lblBarcodeStatus.TabIndex = 1;
            this._lblBarcodeStatus.Text = "格式:  等待输入...";
            this._lblBarcodeStatus.TextMultiLine = false;
            // 
            // _lblScannerStatus
            // 
            this._lblScannerStatus.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this._lblScannerStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblScannerStatus.Location = new System.Drawing.Point(730, 8);
            this._lblScannerStatus.Name = "_lblScannerStatus";
            this._lblScannerStatus.Size = new System.Drawing.Size(150, 31);
            this._lblScannerStatus.TabIndex = 2;
            this._lblScannerStatus.Text = "📡 Offline";
            // 
            // _statusBar
            // 
            this._statusBar.Controls.Add(this._lblStatusLeft);
            this._statusBar.Controls.Add(this._lblStatusRight);
            this._statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._statusBar.ForeColor = System.Drawing.Color.Transparent;
            this._statusBar.Location = new System.Drawing.Point(52, 745);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Padding = new System.Windows.Forms.Padding(16, 2, 16, 2);
            this._statusBar.Size = new System.Drawing.Size(1370, 28);
            this._statusBar.TabIndex = 15;
            // 
            // _lblStatusLeft
            // 
            this._lblStatusLeft.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblStatusLeft.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this._lblStatusLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblStatusLeft.Location = new System.Drawing.Point(16, 5);
            this._lblStatusLeft.Name = "_lblStatusLeft";
            this._lblStatusLeft.Size = new System.Drawing.Size(47, 16);
            this._lblStatusLeft.TabIndex = 0;
            this._lblStatusLeft.Text = "● Ready";
            // 
            // _lblStatusRight
            // 
            this._lblStatusRight.AutoSizeMode = AntdUI.TAutoSize.Auto;
            this._lblStatusRight.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this._lblStatusRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(119)))), ((int)(((byte)(136)))));
            this._lblStatusRight.Location = new System.Drawing.Point(1200, 5);
            this._lblStatusRight.Name = "_lblStatusRight";
            this._lblStatusRight.Size = new System.Drawing.Size(0, 16);
            this._lblStatusRight.TabIndex = 1;
            // 
            // _bottomPanel
            // 
            this._bottomPanel.Controls.Add(this._btnLoad);
            this._bottomPanel.Controls.Add(this._btnStart);
            this._bottomPanel.Controls.Add(this._btnSave);
            this._bottomPanel.Controls.Add(this.button1);
            this._bottomPanel.Controls.Add(this._btnEditSproj);
            this._bottomPanel.Controls.Add(this.progress3);
            this._bottomPanel.Controls.Add(this.chkSaveExcel);
            this._bottomPanel.Controls.Add(this.chkSaveAppend);
            this._bottomPanel.Controls.Add(this.chkStopOnFail);
            this._bottomPanel.Controls.Add(this.log_append);
            this._bottomPanel.Controls.Add(this._pwdInput);
            this._bottomPanel.Controls.Add(this._alertStatus);
            this._bottomPanel.Controls.Add(this.iconState);
            this._bottomPanel.Controls.Add(this.tBadge7);
            this._bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._bottomPanel.ForeColor = System.Drawing.Color.Transparent;
            this._bottomPanel.Location = new System.Drawing.Point(52, 620);
            this._bottomPanel.Name = "_bottomPanel";
            this._bottomPanel.Size = new System.Drawing.Size(1370, 125);
            this._bottomPanel.TabIndex = 14;
            // 
            // _alertStatus
            // 
            this._alertStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._alertStatus.Location = new System.Drawing.Point(857, 56);
            this._alertStatus.Name = "_alertStatus";
            this._alertStatus.Size = new System.Drawing.Size(510, 63);
            this._alertStatus.TabIndex = 14;
            this._alertStatus.Text = "SN: --- | Waiting...";
            this._alertStatus.TextTitle = "System Ready";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1422, 773);
            this.Controls.Add(this._table);
            this.Controls.Add(this._chartPanel);
            this.Controls.Add(this._statsPanel);
            this.Controls.Add(this._barcodePanel);
            this.Controls.Add(this._bottomPanel);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this.menu1);
            this.Controls.Add(this.windowBar);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "MainForm";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.windowBar.ResumeLayout(false);
            this._statsPanel.ResumeLayout(false);
            this.panel_Project_name.ResumeLayout(false);
            this.panel_Project_name.PerformLayout();
            this._cardOperator.ResumeLayout(false);
            this._cardOperator.PerformLayout();
            this._cardHourly.ResumeLayout(false);
            this._cardHourly.PerformLayout();
            this._barHourBg.ResumeLayout(false);
            this._cardTotal.ResumeLayout(false);
            this._cardTotal.PerformLayout();
            this._barTotalBg.ResumeLayout(false);
            this._barcodePanel.ResumeLayout(false);
            this._statusBar.ResumeLayout(false);
            this._statusBar.PerformLayout();
            this._bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

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
