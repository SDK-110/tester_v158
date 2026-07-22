using System;
using System.Windows.Forms;

namespace test_antdui
{
    public partial class LoginForm : AntdUI.Window
    {
        public string EmployeeId => txtEmployeeId.Text.Trim();

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string empId = EmployeeId;
            if (string.IsNullOrEmpty(empId))
            {
                AntdUI.Message.warn(this, "Please enter employee ID", autoClose: 2);
                txtEmployeeId.Focus();
                return;
            }

            var tracker = ProductionTracker.Instance;
            string savedPwd = tracker.OperatorPassword;

            string pwd = txtPassword.Text;
            if (!string.IsNullOrEmpty(savedPwd) && pwd != savedPwd)
            {
                AntdUI.Message.error(this, "Invalid password", autoClose: 2);
                txtPassword.Focus();
                return;
            }

            // 保存到 TestConfig
            var config = TestConfigManager.Instance;
            config.LastOperatorNo = empId;
            TestConfigManager.Save(config);

            // 保存到 setup.ini
            tracker.OperatorName = empId;
            if (!string.IsNullOrEmpty(pwd))
            {
                tracker.OperatorPassword = pwd;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLogin_Click(sender, e);
        }

        private void txtEmployeeId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) txtPassword.Focus();
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            txtEmployeeId.Focus();
        }
    }
}
