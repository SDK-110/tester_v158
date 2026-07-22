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
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tabBarcode = new System.Windows.Forms.TabPage();
            this.lblBarcodeRegex = new System.Windows.Forms.Label();
            this.txtBarcodeRegex = new AntdUI.Input();
            this.chkBarcodeEnabled = new AntdUI.Checkbox();
            this.lblBarcodeHint = new System.Windows.Forms.Label();
            this.btnSaveBarcode = new AntdUI.Button();
            this.tabShift = new System.Windows.Forms.TabPage();
            this.lblShift = new System.Windows.Forms.Label();
            this.lblShiftValue = new System.Windows.Forms.Label();
            this.lblShiftTime = new System.Windows.Forms.Label();
            this.btnToggleShift = new AntdUI.Button();
            this.tabProduction = new System.Windows.Forms.TabPage();
            this.lblProdTitle = new System.Windows.Forms.Label();
            this.lblTotalStats = new System.Windows.Forms.Label();
            this.lblHourlyStats = new System.Windows.Forms.Label();
            this.btnClearHour = new AntdUI.Button();
            this.btnClearAll = new AntdUI.Button();
            this.tabEmployee = new System.Windows.Forms.TabPage();
            this.lblEmpId = new System.Windows.Forms.Label();
            this.txtEmployeeId = new AntdUI.Input();
            this.btnSaveEmployee = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.tabConfig.SuspendLayout();
            this.tabBarcode.SuspendLayout();
            this.tabShift.SuspendLayout();
            this.tabProduction.SuspendLayout();
            this.tabEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.tabBarcode);
            this.tabConfig.Controls.Add(this.tabShift);
            this.tabConfig.Controls.Add(this.tabProduction);
            this.tabConfig.Controls.Add(this.tabEmployee);
            this.tabConfig.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.tabConfig.Location = new System.Drawing.Point(0, 29);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(600, 400);
            this.tabConfig.TabIndex = 0;
            this.tabConfig.Click += new System.EventHandler(this.btnToggleShift_Click);
            // 
            // tabBarcode
            // 
            this.tabBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            this.lblBarcodeRegex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this.lblBarcodeRegex.Location = new System.Drawing.Point(24, 30);
            this.lblBarcodeRegex.Name = "lblBarcodeRegex";
            this.lblBarcodeRegex.Size = new System.Drawing.Size(112, 20);
            this.lblBarcodeRegex.TabIndex = 0;
            this.lblBarcodeRegex.Text = "Barcode Regex:";
            // 
            // txtBarcodeRegex
            // 
            this.txtBarcodeRegex.Font = new System.Drawing.Font("Consolas", 12F);
            this.txtBarcodeRegex.Location = new System.Drawing.Point(24, 58);
            this.txtBarcodeRegex.Name = "txtBarcodeRegex";
            this.txtBarcodeRegex.Padding = new System.Windows.Forms.Padding(8);
            this.txtBarcodeRegex.PlaceholderText = "e.g. ^S\\d{5}$";
            this.txtBarcodeRegex.Size = new System.Drawing.Size(540, 62);
            this.txtBarcodeRegex.TabIndex = 0;
            // 
            // chkBarcodeEnabled
            // 
            this.chkBarcodeEnabled.Checked = true;
            this.chkBarcodeEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBarcodeEnabled.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.chkBarcodeEnabled.Location = new System.Drawing.Point(24, 121);
            this.chkBarcodeEnabled.Name = "chkBarcodeEnabled";
            this.chkBarcodeEnabled.Size = new System.Drawing.Size(160, 24);
            this.chkBarcodeEnabled.TabIndex = 1;
            this.chkBarcodeEnabled.Text = "Enable Barcode";
            // 
            // lblBarcodeHint
            // 
            this.lblBarcodeHint.AutoSize = true;
            this.lblBarcodeHint.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lblBarcodeHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblBarcodeHint.Location = new System.Drawing.Point(24, 148);
            this.lblBarcodeHint.Name = "lblBarcodeHint";
            this.lblBarcodeHint.Size = new System.Drawing.Size(350, 34);
            this.lblBarcodeHint.TabIndex = 2;
            this.lblBarcodeHint.Text = "Hint: Barcode regex saved in setup.ini [setbarcode] barreg\nSave to apply, barcode" +
    " input validates in real-time";
            // 
            // btnSaveBarcode
            // 
            this.btnSaveBarcode.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.btnSaveBarcode.Location = new System.Drawing.Point(24, 200);
            this.btnSaveBarcode.Name = "btnSaveBarcode";
            this.btnSaveBarcode.Size = new System.Drawing.Size(140, 38);
            this.btnSaveBarcode.TabIndex = 2;
            this.btnSaveBarcode.Text = "💾 Save";
            this.btnSaveBarcode.Type = AntdUI.TTypeMini.Primary;
            this.btnSaveBarcode.Click += new System.EventHandler(this.btnSaveBarcode_Click);
            // 
            // tabShift
            // 
            this.tabShift.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            this.lblShift.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
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
            this.lblShiftValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(167)))), ((int)(((byte)(38)))));
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
            this.lblShiftTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblShiftTime.Location = new System.Drawing.Point(24, 100);
            this.lblShiftTime.Name = "lblShiftTime";
            this.lblShiftTime.Size = new System.Drawing.Size(259, 20);
            this.lblShiftTime.TabIndex = 2;
            this.lblShiftTime.Text = "Day: 08:00-20:00  |  Night: 20:00-08:00";
            // 
            // btnToggleShift
            // 
            this.btnToggleShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.btnToggleShift.Location = new System.Drawing.Point(24, 140);
            this.btnToggleShift.Name = "btnToggleShift";
            this.btnToggleShift.Size = new System.Drawing.Size(160, 42);
            this.btnToggleShift.TabIndex = 0;
            this.btnToggleShift.Text = "🔄 Toggle Shift";
            this.btnToggleShift.Type = AntdUI.TTypeMini.Primary;
            // 
            // tabProduction
            // 
            this.tabProduction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            this.lblProdTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
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
            this.lblTotalStats.ForeColor = System.Drawing.Color.White;
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
            this.lblHourlyStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(195)))), ((int)(((byte)(247)))));
            this.lblHourlyStats.Location = new System.Drawing.Point(24, 82);
            this.lblHourlyStats.Name = "lblHourlyStats";
            this.lblHourlyStats.Size = new System.Drawing.Size(301, 20);
            this.lblHourlyStats.TabIndex = 2;
            this.lblHourlyStats.Text = "This Hour: 0 | PASS: 0 | FAIL: 0 | Yield: 0%";
            // 
            // btnClearHour
            // 
            this.btnClearHour.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.btnClearHour.Location = new System.Drawing.Point(24, 130);
            this.btnClearHour.Name = "btnClearHour";
            this.btnClearHour.Size = new System.Drawing.Size(160, 42);
            this.btnClearHour.TabIndex = 0;
            this.btnClearHour.Text = "🗑️ Clear Hour";
            this.btnClearHour.Click += new System.EventHandler(this.btnClearHour_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.btnClearAll.Location = new System.Drawing.Point(200, 130);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(160, 42);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "⚠️ Clear All";
            this.btnClearAll.Type = AntdUI.TTypeMini.Error;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // tabEmployee
            // 
            this.tabEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
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
            this.lblEmpId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this.lblEmpId.Location = new System.Drawing.Point(24, 30);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(97, 20);
            this.lblEmpId.TabIndex = 0;
            this.lblEmpId.Text = "Employee ID:";
            // 
            // txtEmployeeId
            // 
            this.txtEmployeeId.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txtEmployeeId.Location = new System.Drawing.Point(24, 58);
            this.txtEmployeeId.Name = "txtEmployeeId";
            this.txtEmployeeId.Padding = new System.Windows.Forms.Padding(8);
            this.txtEmployeeId.PlaceholderText = "Enter Employee ID";
            this.txtEmployeeId.PrefixSvg = "UserOutlined";
            this.txtEmployeeId.Size = new System.Drawing.Size(280, 64);
            this.txtEmployeeId.TabIndex = 0;
            // 
            // btnSaveEmployee
            // 
            this.btnSaveEmployee.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.btnSaveEmployee.Location = new System.Drawing.Point(28, 128);
            this.btnSaveEmployee.Name = "btnSaveEmployee";
            this.btnSaveEmployee.Size = new System.Drawing.Size(140, 38);
            this.btnSaveEmployee.TabIndex = 1;
            this.btnSaveEmployee.Text = "💾 Save";
            this.btnSaveEmployee.Type = AntdUI.TTypeMini.Primary;
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.ForeColor = System.Drawing.Color.Transparent;
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.tabConfig);
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ForeColor = System.Drawing.Color.White;
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
