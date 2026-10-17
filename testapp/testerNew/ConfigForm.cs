using System;
using System.Windows.Forms;
using testapp.glob_set;

namespace test_antdui
{
    public partial class ConfigForm : AntdUI.Window
    {
        private readonly ProductionTracker _tracker;

        public ConfigForm()
        {
            InitializeComponent();
            _tracker = ProductionTracker.Instance;
            LoadConfig();
        }

        private void LoadConfig()
        {
            // Barcode：与 Main_f 一致，以 setup.ini [setbarcode] 为准（没有时才回退旧 JSON 配置）
            var barcodeSec = glob_ini_instance.getInstance().getSetupIniData["setbarcode"];
            if (barcodeSec != null)
            {
                txtBarcodeRegex.Text = barcodeSec["barreg"] ?? _tracker.BarcodeRegex;
                string enabled = barcodeSec["barenable"];
                chkBarcodeEnabled.Checked = string.IsNullOrEmpty(enabled)
                    ? _tracker.BarcodeEnabled
                    : string.Equals(enabled.Trim(), "true", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                txtBarcodeRegex.Text = _tracker.BarcodeRegex;
                chkBarcodeEnabled.Checked = _tracker.BarcodeEnabled;
            }

            // Employee
            txtEmployeeId.Text = _tracker.OperatorName;

            // Shift
            lblShiftValue.Text = _tracker.CurrentShift;
            lblShiftValue.ForeColor = _tracker.IsDayShift
                ? Palette.Warning
                : Palette.AccentSoft;

            // Stats
            UpdateStatsDisplay();
        }

        private void UpdateStatsDisplay()
        {
            lblTotalStats.Text = $"Total: {_tracker.TotalCount} | PASS: {_tracker.TotalPass} | FAIL: {_tracker.TotalFail} | Yield: {_tracker.TotalYield}%";
            lblHourlyStats.Text = $"Hour: {_tracker.CurrentHourTotal} | PASS: {_tracker.CurrentHourPass} | FAIL: {_tracker.CurrentHourFail} | Yield: {_tracker.HourlyYield}%";
        }

        private void btnSaveBarcode_Click(object sender, EventArgs e)
        {
            string regex = txtBarcodeRegex.Text.Trim();
            if (!string.IsNullOrEmpty(regex))
            {
                try
                {
                    new System.Text.RegularExpressions.Regex(regex);
                }
                catch
                {
                    AntdUI.Message.error(this, "Invalid regex format", autoClose: 2);
                    return;
                }
            }
            _tracker.BarcodeRegex = regex;
            _tracker.BarcodeEnabled = chkBarcodeEnabled.Checked;

            // 写回 setup.ini [setbarcode]（Main_f / MainControl 都从这里读）
            var ini = glob_ini_instance.getInstance().getSetupIniData;
            var barcodeSec = ini["setbarcode"];
            if (barcodeSec == null)
            {
                ini.Sections.AddSection("setbarcode");
                barcodeSec = ini["setbarcode"];
            }
            barcodeSec["barenable"] = chkBarcodeEnabled.Checked ? "true" : "false";
            barcodeSec["barreg"] = regex;
            glob_ini_instance.getInstance().write2Ini(ini);

            AntdUI.Message.success(this, "Barcode config saved", autoClose: 2);
        }

        private void btnToggleShift_Click(object sender, EventArgs e)
        {
            _tracker.ToggleShift();
            lblShiftValue.Text = _tracker.CurrentShift;
            lblShiftValue.ForeColor = _tracker.IsDayShift
                ? Palette.Warning
                : Palette.AccentSoft;
            AntdUI.Message.success(this, $"Switched to {_tracker.CurrentShift}");
        }

        private void btnClearHour_Click(object sender, EventArgs e)
        {
            _tracker.ClearHourlyStats();
            UpdateStatsDisplay();
            AntdUI.Message.success(this, "Hourly stats cleared");
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            _tracker.ResetAllStats();
            UpdateStatsDisplay();
            AntdUI.Message.success(this, "All stats cleared");
        }

        private void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            string empId = txtEmployeeId.Text.Trim();
            if (string.IsNullOrEmpty(empId))
            {
                AntdUI.Message.warn(this, "Please enter employee ID", autoClose: 2);
                return;
            }
            _tracker.OperatorName = empId;

            var config = TestConfigManager.Instance;
            config.LastOperatorNo = empId;
            TestConfigManager.Save(config);

            AntdUI.Message.success(this, "Employee info saved", autoClose: 2);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            tabConfig.SelectedIndex = 0;
        }
    }
}
