namespace testapp.test_form
{
    partial class test_antdui
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
            this.virtualPanel1 = new AntdUI.VirtualPanel();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.battery1 = new AntdUI.Battery();
            this.button1 = new AntdUI.Button();
            this.SuspendLayout();
            // 
            // virtualPanel1
            // 
            this.virtualPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.virtualPanel1.Location = new System.Drawing.Point(77, 69);
            this.virtualPanel1.Name = "virtualPanel1";
            this.virtualPanel1.Size = new System.Drawing.Size(789, 350);
            this.virtualPanel1.TabIndex = 0;
            this.virtualPanel1.Text = "virtualPanel1";
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.Size = new System.Drawing.Size(948, 23);
            this.pageHeader1.TabIndex = 1;
            this.pageHeader1.Text = "pageHeader1";
            // 
            // battery1
            // 
            this.battery1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.battery1.Location = new System.Drawing.Point(566, 146);
            this.battery1.Name = "battery1";
            this.battery1.Size = new System.Drawing.Size(76, 49);
            this.battery1.TabIndex = 2;
            this.battery1.Text = "battery1";
            this.battery1.Value = 33;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(457, 250);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 36);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // test_antdui
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(948, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.battery1);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this.virtualPanel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "test_antdui";
            this.Text = "test_antdui";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.test_antdui_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.VirtualPanel virtualPanel1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Battery battery1;
        private AntdUI.Button button1;
    }
}