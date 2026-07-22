using WeifenLuo.WinFormsUI.Docking;

namespace testapp.duochuangti
{
    partial class test1_form:DockContent
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
            this.userControl = new testapp.test_case_control();
            this.SuspendLayout();
            // 
            // userControl
            // 
            this.userControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControl.done_dealwith_flog = 0;
            this.userControl.init_flog = 0;
            this.userControl.Location = new System.Drawing.Point(0, 0);
            this.userControl.MinimumSize = new System.Drawing.Size(500, 300);
            this.userControl.Name = "userControl";
            this.userControl.Size = new System.Drawing.Size(800, 450);
            this.userControl.TabIndex = 0;
            this.userControl.uut_number = 1;
            this.userControl.Load += new System.EventHandler(this.userControl21_Load);
            // 
            // test1_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.userControl);
            this.Name = "test1_form";
            this.Text = "DUT1_Form";
            this.Load += new System.EventHandler(this.test1_form_Load);
            this.Shown += new System.EventHandler(this.test1_form_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private test_case_control userControl;
    }
}