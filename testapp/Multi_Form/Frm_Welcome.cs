using System;
using System.Windows.Forms;
using testapp.glob_set;

namespace VMPro
{
    internal partial class Frm_Welcome : AntdUI.Window
    {
        /// <summary>
        /// 当前输入的员工工号
        /// </summary>
        public string EmployeeId => txtEmployeeNo.Text.Trim();

        internal Frm_Welcome()
        {
            InitializeComponent();
        }

        private void Frm_Welcome_Load(object sender, EventArgs e)
        {
            lbl_version.Text = "Version " + Application.ProductVersion;

            // 登录框保持为空：员工需输入自己的工号（为空时不进入系统）
            txtEmployeeNo.Text = "";
        }

        private void Frm_Welcome_Shown(object sender, EventArgs e)
        {
            txtEmployeeNo.Focus();
        }

        /// <summary>
        /// 【Open】按钮：保存员工工号到 setup.ini 并进入系统
        /// </summary>
        private void btn_open_Click(object sender, EventArgs e)
        {
            string employeeNo = EmployeeId;
            if (employeeNo.Length == 0)
            {
                AntdUI.Message.warn(this, "Employee ID cannot be empty. Please enter your employee ID!", autoClose: 2);
                txtEmployeeNo.Focus();
                return;
            }

            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                ini["setproduct"]["personal_number"] = employeeNo;
                glob_ini_instance.getInstance().write2Ini(ini);
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this, "Failed to save employee ID: " + ex.Message, autoClose: 3);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// 【Cancel】按钮：取消登录，不进入系统
        /// </summary>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 输入框回车：直接触发【Open】
        /// </summary>
        private void txtEmployeeNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btn_open_Click(sender, e);
            }
        }
    }
}
