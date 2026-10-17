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
            tabBarcode.BackColor = System.Drawing.Color.FromArgb(10, 15, 30);
            tabBarcode.Controls.Add(lblBarcodeRegex);
            tabBarcode.Controls.Add(txtBarcodeRegex);
            tabBarcode.Controls.Add(chkBarcodeEnabled);
            tabBarcode.Controls.Add(lblBarcodeHint);
            tabBarcode.Controls.Add(btnSaveBarcode);
            tabBarcode.Location = new System.Drawing.Point(4, 29);
            tabBarcode.Name = "tabBarcode";
            tabBarcode.Padding = new System.Windows.Forms.Padding(3);
            tabBarcode.Size = new System.Drawing.Size(592, 367);
            tabBarcode.TabIndex = 0;
            tabBarcode.Text = "📏 Barcode";
            // 
            // lblBarcodeRegex
            // 
            lblBarcodeRegex.AutoSize = true;
            lblBarcodeRegex.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            lblBarcodeRegex.ForeColor = System.Drawing.Color.FromArgb(155, 179, 224);
            lblBarcodeRegex.Location = new System.Drawing.Point(24, 30);
            lblBarcodeRegex.Name = "lblBarcodeRegex";
            lblBarcodeRegex.Size = new System.Drawing.Size(112, 20);
            lblBarcodeRegex.TabIndex = 0;
            lblBarcodeRegex.Text = "Barcode Regex:";
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
            lblBarcodeHint.AutoSize = true;
            lblBarcodeHint.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            lblBarcodeHint.ForeColor = System.Drawing.Color.FromArgb(108, 134, 176);
            lblBarcodeHint.Location = new System.Drawing.Point(24, 148);
            lblBarcodeHint.Name = "lblBarcodeHint";
            lblBarcodeHint.Size = new System.Drawing.Size(350, 34);
            lblBarcodeHint.TabIndex = 2;
            lblBarcodeHint.Text = "Hint: saved in setup.ini [setbarcode] barreg\nMulti-barcode: add count=N + barreg1..N/name1..N (1st = SN, rest -> global vars)";
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
            tabShift.BackColor = System.Drawing.Color.FromArgb(10, 15, 30);
            tabShift.Controls.Add(lblShift);
            tabShift.Controls.Add(lblShiftValue);
            tabShift.Controls.Add(lblShiftTime);
            tabShift.Controls.Add(btnToggleShift);
            tabShift.Location = new System.Drawing.Point(4, 29);
            tabShift.Name = "tabShift";
            tabShift.Padding = new System.Windows.Forms.Padding(3);
            tabShift.Size = new System.Drawing.Size(592, 367);
            tabShift.TabIndex = 1;
            tabShift.Text = "🔄 Shift";
            // 
            // lblShift
            // 
            lblShift.AutoSize = true;
            lblShift.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            lblShift.ForeColor = System.Drawing.Color.FromArgb(155, 179, 224);
            lblShift.Location = new System.Drawing.Point(24, 30);
            lblShift.Name = "lblShift";
            lblShift.Size = new System.Drawing.Size(97, 20);
            lblShift.TabIndex = 0;
            lblShift.Text = "Current Shift:";
            // 
            // lblShiftValue
            // 
            lblShiftValue.AutoSize = true;
            lblShiftValue.Font = new System.Drawing.Font("Microsoft YaHei UI", 24F, System.Drawing.FontStyle.Bold);
            lblShiftValue.ForeColor = System.Drawing.Color.FromArgb(255, 176, 32);
            lblShiftValue.Location = new System.Drawing.Point(24, 56);
            lblShiftValue.Name = "lblShiftValue";
            lblShiftValue.Size = new System.Drawing.Size(82, 42);
            lblShiftValue.TabIndex = 1;
            lblShiftValue.Text = "白班";
            // 
            // lblShiftTime
            // 
            lblShiftTime.AutoSize = true;
            lblShiftTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            lblShiftTime.ForeColor = System.Drawing.Color.FromArgb(108, 134, 176);
            lblShiftTime.Location = new System.Drawing.Point(24, 100);
            lblShiftTime.Name = "lblShiftTime";
            lblShiftTime.Size = new System.Drawing.Size(259, 20);
            lblShiftTime.TabIndex = 2;
            lblShiftTime.Text = "Day: 08:00-20:00  |  Night: 20:00-08:00";
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
            tabProduction.BackColor = System.Drawing.Color.FromArgb(10, 15, 30);
            tabProduction.Controls.Add(lblProdTitle);
            tabProduction.Controls.Add(lblTotalStats);
            tabProduction.Controls.Add(lblHourlyStats);
            tabProduction.Controls.Add(btnClearHour);
            tabProduction.Controls.Add(btnClearAll);
            tabProduction.Location = new System.Drawing.Point(4, 29);
            tabProduction.Name = "tabProduction";
            tabProduction.Padding = new System.Windows.Forms.Padding(3);
            tabProduction.Size = new System.Drawing.Size(592, 367);
            tabProduction.TabIndex = 2;
            tabProduction.Text = "🗑️ Production";
            // 
            // lblProdTitle
            // 
            lblProdTitle.AutoSize = true;
            lblProdTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            lblProdTitle.ForeColor = System.Drawing.Color.FromArgb(155, 179, 224);
            lblProdTitle.Location = new System.Drawing.Point(24, 30);
            lblProdTitle.Name = "lblProdTitle";
            lblProdTitle.Size = new System.Drawing.Size(123, 20);
            lblProdTitle.TabIndex = 0;
            lblProdTitle.Text = "Production Stats:";
            // 
            // lblTotalStats
            // 
            lblTotalStats.AutoSize = true;
            lblTotalStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            lblTotalStats.ForeColor = System.Drawing.Color.FromArgb(238, 245, 255);
            lblTotalStats.Location = new System.Drawing.Point(24, 56);
            lblTotalStats.Name = "lblTotalStats";
            lblTotalStats.Size = new System.Drawing.Size(268, 20);
            lblTotalStats.TabIndex = 1;
            lblTotalStats.Text = "Total: 0 | PASS: 0 | FAIL: 0 | Yield: 0%";
            // 
            // lblHourlyStats
            // 
            lblHourlyStats.AutoSize = true;
            lblHourlyStats.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            lblHourlyStats.ForeColor = System.Drawing.Color.FromArgb(106, 169, 255);
            lblHourlyStats.Location = new System.Drawing.Point(24, 82);
            lblHourlyStats.Name = "lblHourlyStats";
            lblHourlyStats.Size = new System.Drawing.Size(301, 20);
            lblHourlyStats.TabIndex = 2;
            lblHourlyStats.Text = "This Hour: 0 | PASS: 0 | FAIL: 0 | Yield: 0%";
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
            tabEmployee.BackColor = System.Drawing.Color.FromArgb(10, 15, 30);
            tabEmployee.Controls.Add(lblEmpId);
            tabEmployee.Controls.Add(txtEmployeeId);
            tabEmployee.Controls.Add(btnSaveEmployee);
            tabEmployee.Location = new System.Drawing.Point(4, 29);
            tabEmployee.Name = "tabEmployee";
            tabEmployee.Padding = new System.Windows.Forms.Padding(3);
            tabEmployee.Size = new System.Drawing.Size(592, 367);
            tabEmployee.TabIndex = 3;
            tabEmployee.Text = "👤 Employee";
            // 
            // lblEmpId
            // 
            lblEmpId.AutoSize = true;
            lblEmpId.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            lblEmpId.ForeColor = System.Drawing.Color.FromArgb(155, 179, 224);
            lblEmpId.Location = new System.Drawing.Point(24, 30);
            lblEmpId.Name = "lblEmpId";
            lblEmpId.Size = new System.Drawing.Size(97, 20);
            lblEmpId.TabIndex = 0;
            lblEmpId.Text = "Employee ID:";
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
            pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            pageHeader1.ForeColor = System.Drawing.Color.FromArgb(238, 245, 255);
            pageHeader1.Location = new System.Drawing.Point(0, 0);
            pageHeader1.Name = "pageHeader1";
            pageHeader1.ShowBack = true;
            pageHeader1.ShowButton = true;
            pageHeader1.Size = new System.Drawing.Size(600, 23);
            pageHeader1.TabIndex = 1;
            pageHeader1.Text = "";
            // 
            // ConfigForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(10, 15, 30);
            ClientSize = new System.Drawing.Size(600, 400);
            Controls.Add(pageHeader1);
            Controls.Add(tabConfig);
            Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            ForeColor = System.Drawing.Color.FromArgb(238, 245, 255);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConfigForm";
            ShowIcon = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Settings";
            tabConfig.ResumeLayout(false);
            tabBarcode.ResumeLayout(false);
            tabBarcode.PerformLayout();
            tabShift.ResumeLayout(false);
            tabShift.PerformLayout();
            tabProduction.ResumeLayout(false);
            tabProduction.PerformLayout();
            tabEmployee.ResumeLayout(false);
            tabEmployee.PerformLayout();
            ResumeLayout(false);

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
