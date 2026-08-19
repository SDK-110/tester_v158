namespace VMPro
{
    partial class Frm_Welcome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.txtEmployeeNo = new AntdUI.Input();
            this.btn_open = new AntdUI.Button();
            this.btn_cancel = new AntdUI.Button();
            this.lbl_version = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(78, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(212, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Employee Login";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this.lblSubtitle.Location = new System.Drawing.Point(80, 94);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(208, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Enter employee ID and press Open";
            //
            // txtEmployeeNo
            //
            this.txtEmployeeNo.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.txtEmployeeNo.Location = new System.Drawing.Point(44, 145);
            this.txtEmployeeNo.Name = "txtEmployeeNo";
            this.txtEmployeeNo.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.txtEmployeeNo.PlaceholderText = "Employee ID";
            this.txtEmployeeNo.PrefixSvg = "UserOutlined";
            this.txtEmployeeNo.Size = new System.Drawing.Size(280, 55);
            this.txtEmployeeNo.TabIndex = 0;
            this.txtEmployeeNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmployeeNo_KeyDown);
            //
            // btn_open
            //
            this.btn_open.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.btn_open.Location = new System.Drawing.Point(44, 216);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(130, 40);
            this.btn_open.TabIndex = 2;
            this.btn_open.Text = "Open";
            this.btn_open.Type = AntdUI.TTypeMini.Primary;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            //
            // btn_cancel
            //
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.btn_cancel.Location = new System.Drawing.Point(194, 216);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(130, 40);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            //
            // lbl_version
            //
            this.lbl_version.AutoSize = true;
            this.lbl_version.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.lbl_version.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(153)))), ((int)(((byte)(170)))));
            this.lbl_version.Location = new System.Drawing.Point(44, 326);
            this.lbl_version.Name = "lbl_version";
            this.lbl_version.Size = new System.Drawing.Size(80, 16);
            this.lbl_version.TabIndex = 4;
            this.lbl_version.Text = "Version";
            //
            // Frm_Welcome
            //
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(368, 360);
            this.Controls.Add(this.lbl_version);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.txtEmployeeNo);
            this.Controls.Add(this.lblSubtitle);
            this.Controls.Add(this.lblTitle);
            this.DisableTheme = true;
            this.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Welcome";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employee Login";
            this.Load += new System.EventHandler(this.Frm_Welcome_Load);
            this.Shown += new System.EventHandler(this.Frm_Welcome_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lbl_version;
        private AntdUI.Input txtEmployeeNo;
        private AntdUI.Button btn_open;
        private AntdUI.Button btn_cancel;
    }
}
