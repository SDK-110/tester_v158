namespace test_antdui
{
    partial class ConfigForm
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
            tabConfig = new System.Windows.Forms.TabControl();
            tabBarcode = new System.Windows.Forms.TabPage();
            lblBarcodeRegex = new System.Windows.Forms.Label();
            txtBarcodeRegex = new AntdUI.Input();
            chkBarcodeEnabled = new AntdUI.Checkbox();
            lblBarcodeHint = new System.Windows.Forms.Label();
            btnSaveBarcode = new AntdUI.Button();
            tabShift = new System.Windows.Forms.TabPage();
            lblShift = new System.Windows.Forms.Label();
            lblShiftValue = new System.Windows.Forms.Label();
            lblShiftTime = new System.Windows.Forms.Label();
            btnToggleShift = new AntdUI.Button();
            tabProduction = new System.Windows.Forms.TabPage();
            lblProdTitle = new System.Windows.Forms.Label();
            lblTotalStats = new System.Windows.Forms.Label();
            lblHourlyStats = new System.Windows.Forms.Label();
            btnClearHour = new AntdUI.Button();
            btnClearAll = new AntdUI.Button();
            tabEmployee = new System.Windows.Forms.TabPage();
            lblEmpId = new System.Windows.Forms.Label();
            txtEmployeeId = new AntdUI.Input();
            btnSaveEmployee = new AntdUI.Button();
            pageHeader1 = new AntdUI.PageHeader();
            tabConfig.SuspendLayout();
            tabBarcode.SuspendLayout();
            tabShift.SuspendLayout();
            tabProduction.SuspendLayout();
            tabEmployee.SuspendLayout();
            SuspendLayout();
            // 
            // tabConfig
            // 
            tabConfig.Controls.Add(tabBarcode);
            tabConfig.Controls.Add(tabShift);
            tabConfig.Controls.Add(tabProduction);
            tabConfig.Controls.Add(tabEmployee);
            tabConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            tabConfig.Location = new System.Drawing.Point(0, 29);
            tabConfig.Name = "tabConfig";
            tabConfig.SelectedIndex = 0;
            tabConfig.Size = new System.Drawing.Size(600, 400);
            tabConfig.TabIndex = 0;
            tabConfig.Click += btnToggleShift_Click;
            // 
            // tabBarcode
            // 
            this.tabBarcode.BackColor = Palette.Bg;
            this.tabBarcode.Controls.Add(this.lblBarcodeRegex);
            this.tabBarcode.Controls.Add(this.txtBarcodeRegex);
            this.tabBarcode.Controls.Add(this.chkBarcodeEnabled);
            this.tabBarcode.Controls.Add(this.lblBarcodeHint);
            this.tabBarcode.Controls.Add(this.btnSaveBarcode);
            this.tabBarcode.Location = new System.Drawing.Point(4, 29);
            this.tabBarcode.Name = "tabBarcode";
            this.tabBarcode.Padding = new System.Windows.Forms.Padding(3);
            this.tabBarcode.Size = new System.Drawing.Size(592, 367);
            this.tabBarcode.TabIndex = 0;
            this.tabBarcode.Text = "📏 Barcode";
            // 
            // lblBarcodeRegex
            // 
            this.lblBarcodeRegex.AutoSize = true;
            this.lblBarcodeRegex.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblBarcodeRegex.ForeColor = Palette.TextSecondary;
            this.lblBarcodeRegex.Location = new System.Drawing.Point(24, 30);
            this.lblBarcodeRegex.Name = "lblBarcodeRegex";
            this.lblBarcodeRegex.Size = new System.Drawing.Size(112, 20);
            this.lblBarcodeRegex.TabIndex = 0;
            this.lblBarcodeRegex.Text = "Barcode Regex:";
            // 
            // txtBarcodeRegex
            // 
            txtBarcodeRegex.Font = new System.Drawing.Font("Consolas", 12F);
            txtBarcodeRegex.Location = new System.Drawing.Point(24, 58);
            txtBarcodeRegex.Name = "txtBarcodeRegex";
            txtBarcodeRegex.Padding = new System.Windows.Forms.Padding(8);
            txtBarcodeRegex.PlaceholderText = "e.g. ^S\\d{5}$";
            txtBarcodeRegex.Size = new System.Drawing.Size(540, 62);
            txtBarcodeRegex.TabIndex = 0;
            // 
            // chkBarcodeEnabled
            // 
            chkBarcodeEnabled.Checked = true;
            chkBarcodeEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            chkBarcodeEnabled.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            chkBarcodeEnabled.Location = new System.Drawing.Point(24, 121);
            chkBarcodeEnabled.Name = "chkBarcodeEnabled";
            chkBarcodeEnabled.Size = new System.Drawing.Size(160, 24);
            chkBarcodeEnabled.TabIndex = 1;
            chkBarcodeEnabled.Text = "Enable Barcode";
            // 
            // lblBarcodeHint
            // 
            this.lblBarcodeHint.AutoSize = true;
            this.lblBarcodeHint.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblBarcodeHint.ForeColor = Palette.TextMuted;
            this.lblBarcodeHint.Location = new System.Drawing.Point(24, 148);
            this.lblBarcodeHint.Name = "lblBarcodeHint";
            this.lblBarcodeHint.Size = new System.Drawing.Size(350, 34);
            this.lblBarcodeHint.TabIndex = 2;
            this.lblBarcodeHint.Text = "Hint: Barcode regex saved in setup.ini [setbarcode] barreg\nSave to apply, barcode" +
    " input validates in real-time";
            // 
            // btnSaveBarcode
            // 
            btnSaveBarcode.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            btnSaveBarcode.Location = new System.Drawing.Point(24, 200);
            btnSaveBarcode.Name = "btnSaveBarcode";
            btnSaveBarcode.Size = new System.Drawing.Size(140, 38);
            btnSaveBarcode.TabIndex = 2;
            btnSaveBarcode.Text = "💾 Save";
            btnSaveBarcode.Type = AntdUI.TTypeMini.Primary;
            btnSaveBarcode.Click += btnSaveBarcode_Click;
            // 
            // tabShift
            // 
            this.tabShift.BackColor = Palette.Bg;
            this.tabShift.Controls.Add(this.lblShift);
            this.tabShift.Controls.Add(this.lblShiftValue);
            this.tabShift.Controls.Add(this.lblShiftTime);
            this.tabShift.Controls.Add(this.btnToggleShift);
            this.tabShift.Location = new System.Drawing.Point(4, 29);
            this.tabShift.Name = "tabShift";
            this.tabShift.Padding = new System.Windows.Forms.Padding(3);
            this.tabShift.Size = new System.Drawing.Size(592, 367);
            this.tabShift.TabIndex = 1;
            this.tabShift.Text = "🔄 Shift";
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblShift.ForeColor = Palette.TextSecondary;
            this.lblShift.Location = new System.Drawing.Point(24, 30);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(97, 20);
            this.lblShift.TabIndex = 0;
            this.lblShift.Text = "Current Shift:";
            // 
            // lblShiftValue
            // 
            this.lblShiftValue.AutoSize = true;
            this.lblShiftValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblShiftValue.ForeColor = Palette.Warning;
            this.lblShiftValue.Location = new System.Drawing.Point(24, 56);
            this.lblShiftValue.Name = "lblShiftValue";
            this.lblShiftValue.Size = new System.Drawing.Size(82, 42);
            this.lblShiftValue.TabIndex = 1;
            this.lblShiftValue.Text = "白班";
            // 
            // lblShiftTime
            // 
            this.lblShiftTime.AutoSize = true;
            this.lblShiftTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblShiftTime.ForeColor = Palette.TextMuted;
            this.lblShiftTime.Location = new System.Drawing.Point(24, 100);
            this.lblShiftTime.Name = "lblShiftTime";
            this.lblShiftTime.Size = new System.Drawing.Size(259, 20);
            this.lblShiftTime.TabIndex = 2;
            this.lblShiftTime.Text = "Day: 08:00-20:00  |  Night: 20:00-08:00";
            // 
            // btnToggleShift
            // 
            btnToggleShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            btnToggleShift.Location = new System.Drawing.Point(24, 140);
            btnToggleShift.Name = "btnToggleShift";
            btnToggleShift.Size = new System.Drawing.Size(160, 42);
            btnToggleShift.TabIndex = 0;
            btnToggleShift.Text = "🔄 Toggle Shift";
            btnToggleShift.Type = AntdUI.TTypeMini.Primary;
            // 
            // tabProduction
            // 
            this.tabProduction.BackColor = Palette.Bg;
            this.tabProduction.Controls.Add(this.lblProdTitle);
            this.tabProduction.Controls.Add(this.lblTotalStats);
            this.tabProduction.Controls.Add(this.lblHourlyStats);
            this.tabProduction.Controls.Add(this.btnClearHour);
            this.tabProduction.Controls.Add(this.btnClearAll);
            this.tabProduction.Location = new System.Drawing.Point(4, 29);
            this.tabProduction.Name = "tabProduction";
            this.tabProduction.Padding = new System.Windows.Forms.Padding(3);
            this.tabProduction.Size = new System.Drawing.Size(592, 367);
            this.tabProduction.TabIndex = 2;
            this.tabProduction.Text = "🗑️ Production";
            // 
            // lblProdTitle
            // 
            this.lblProdTitle.AutoSize = true;
            this.lblProdTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblProdTitle.ForeColor = Palette.TextSecondary;
            this.lblProdTitle.Location = new System.Drawing.Point(24, 30);
            this.lblProdTitle.Name = "lblProdTitle";
            this.lblProdTitle.Size = new System.Drawing.Size(123, 20);
            this.lblProdTitle.TabIndex = 0;
            this.lblProdTitle.Text = "Production Stats:";
            // 
            // lblTotalStats
            // 
            this.lblTotalStats.AutoSize = true;
            this.lblTotalStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.lblTotalStats.ForeColor = Palette.TextPrimary;
            this.lblTotalStats.Location = new System.Drawing.Point(24, 56);
            this.lblTotalStats.Name = "lblTotalStats";
            this.lblTotalStats.Size = new System.Drawing.Size(268, 20);
            this.lblTotalStats.TabIndex = 1;
            this.lblTotalStats.Text = "Total: 0 | PASS: 0 | FAIL: 0 | Yield: 0%";
            // 
            // lblHourlyStats
            // 
            this.lblHourlyStats.AutoSize = true;
            this.lblHourlyStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.lblHourlyStats.ForeColor = Palette.AccentSoft;
            this.lblHourlyStats.Location = new System.Drawing.Point(24, 82);
            this.lblHourlyStats.Name = "lblHourlyStats";
            this.lblHourlyStats.Size = new System.Drawing.Size(301, 20);
            this.lblHourlyStats.TabIndex = 2;
            this.lblHourlyStats.Text = "This Hour: 0 | PASS: 0 | FAIL: 0 | Yield: 0%";
            // 
            // btnClearHour
            // 
            btnClearHour.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            btnClearHour.Location = new System.Drawing.Point(24, 130);
            btnClearHour.Name = "btnClearHour";
            btnClearHour.Size = new System.Drawing.Size(160, 42);
            btnClearHour.TabIndex = 0;
            btnClearHour.Text = "🗑️ Clear Hour";
            btnClearHour.Click += btnClearHour_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            btnClearAll.Location = new System.Drawing.Point(200, 130);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new System.Drawing.Size(160, 42);
            btnClearAll.TabIndex = 1;
            btnClearAll.Text = "⚠️ Clear All";
            btnClearAll.Type = AntdUI.TTypeMini.Error;
            btnClearAll.Click += btnClearAll_Click;
            // 
            // tabEmployee
            // 
            this.tabEmployee.BackColor = Palette.Bg;
            this.tabEmployee.Controls.Add(this.lblEmpId);
            this.tabEmployee.Controls.Add(this.txtEmployeeId);
            this.tabEmployee.Controls.Add(this.btnSaveEmployee);
            this.tabEmployee.Location = new System.Drawing.Point(4, 29);
            this.tabEmployee.Name = "tabEmployee";
            this.tabEmployee.Padding = new System.Windows.Forms.Padding(3);
            this.tabEmployee.Size = new System.Drawing.Size(592, 367);
            this.tabEmployee.TabIndex = 3;
            this.tabEmployee.Text = "👤 Employee";
            // 
            // lblEmpId
            // 
            this.lblEmpId.AutoSize = true;
            this.lblEmpId.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblEmpId.ForeColor = Palette.TextSecondary;
            this.lblEmpId.Location = new System.Drawing.Point(24, 30);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(97, 20);
            this.lblEmpId.TabIndex = 0;
            this.lblEmpId.Text = "Employee ID:";
            // 
            // txtEmployeeId
            // 
            txtEmployeeId.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            txtEmployeeId.Location = new System.Drawing.Point(24, 58);
            txtEmployeeId.Name = "txtEmployeeId";
            txtEmployeeId.Padding = new System.Windows.Forms.Padding(8);
            txtEmployeeId.PlaceholderText = "Enter Employee ID";
            txtEmployeeId.PrefixSvg = "UserOutlined";
            txtEmployeeId.Size = new System.Drawing.Size(280, 64);
            txtEmployeeId.TabIndex = 0;
            // 
            // btnSaveEmployee
            // 
            btnSaveEmployee.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            btnSaveEmployee.Location = new System.Drawing.Point(28, 128);
            btnSaveEmployee.Name = "btnSaveEmployee";
            btnSaveEmployee.Size = new System.Drawing.Size(140, 38);
            btnSaveEmployee.TabIndex = 1;
            btnSaveEmployee.Text = "💾 Save";
            btnSaveEmployee.Type = AntdUI.TTypeMini.Primary;
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            // 原为 Transparent: 会把标题栏的关闭/返回按钮画成透明 —— 看不见但命中区域还在,
            // 表现为"关闭按钮不显示却功能正常"。改成可见前景色, 与窗体 ForeColor 一致。
            this.pageHeader1.ForeColor = Palette.TextPrimary;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowBack = true;
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(600, 34);
            this.pageHeader1.TabIndex = 1;
            this.pageHeader1.Text = "";
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Palette.Bg;
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.tabConfig);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ForeColor = Palette.TextPrimary;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabConfig.ResumeLayout(false);
            this.tabBarcode.ResumeLayout(false);
            this.tabBarcode.PerformLayout();
            this.tabShift.ResumeLayout(false);
            this.tabShift.PerformLayout();
            this.tabProduction.ResumeLayout(false);
            this.tabProduction.PerformLayout();
            this.tabEmployee.ResumeLayout(false);
            this.tabEmployee.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage tabBarcode;
        private System.Windows.Forms.TabPage tabShift;
        private System.Windows.Forms.TabPage tabProduction;
        private System.Windows.Forms.TabPage tabEmployee;

        private AntdUI.Input txtBarcodeRegex;
        private AntdUI.Checkbox chkBarcodeEnabled;
        private AntdUI.Button btnSaveBarcode;

        private System.Windows.Forms.Label lblShiftValue;
        private AntdUI.Button btnToggleShift;

        private System.Windows.Forms.Label lblTotalStats;
        private System.Windows.Forms.Label lblHourlyStats;
        private AntdUI.Button btnClearHour;
        private AntdUI.Button btnClearAll;

        private AntdUI.Input txtEmployeeId;
        private AntdUI.Button btnSaveEmployee;
        private System.Windows.Forms.Label lblBarcodeRegex;
        private System.Windows.Forms.Label lblBarcodeHint;
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.Label lblShiftTime;
        private System.Windows.Forms.Label lblProdTitle;
        private System.Windows.Forms.Label lblEmpId;
        private AntdUI.PageHeader pageHeader1;
    }
}
