namespace test_antdui
{
    partial class TestLoggerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox _richTextBox;
        private System.Windows.Forms.Panel _panelBottom;
        private System.Windows.Forms.CheckBox chkAutoScroll;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _updateTimer?.Stop();
                _updateTimer?.Dispose();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
            _instance = null;
        }

        private void InitializeComponent()
        {
            this._richTextBox = new System.Windows.Forms.RichTextBox();
            this._panelBottom = new System.Windows.Forms.Panel();
            this.save_button = new AntdUI.Button();
            this.Clear_button = new AntdUI.Button();
            this.chkAutoScroll = new System.Windows.Forms.CheckBox();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.checkbox1 = new AntdUI.Checkbox();
            this._panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // _richTextBox
            // 
            this._richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._richTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._richTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this._richTextBox.ForeColor = System.Drawing.Color.White;
            this._richTextBox.Location = new System.Drawing.Point(0, 0);
            this._richTextBox.Name = "_richTextBox";
            this._richTextBox.ReadOnly = true;
            this._richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this._richTextBox.Size = new System.Drawing.Size(434, 271);
            this._richTextBox.TabIndex = 0;
            this._richTextBox.Text = "";
            this._richTextBox.WordWrap = false;
            // 
            // _panelBottom
            // 
            this._panelBottom.Controls.Add(this.checkbox1);
            this._panelBottom.Controls.Add(this.save_button);
            this._panelBottom.Controls.Add(this.Clear_button);
            this._panelBottom.Controls.Add(this.chkAutoScroll);
            this._panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelBottom.Location = new System.Drawing.Point(0, 284);
            this._panelBottom.Name = "_panelBottom";
            this._panelBottom.Size = new System.Drawing.Size(439, 42);
            this._panelBottom.TabIndex = 1;
            // 
            // save_button
            // 
            this.save_button.Location = new System.Drawing.Point(205, 2);
            this.save_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(64, 29);
            this.save_button.TabIndex = 4;
            this.save_button.Text = "Save";
            this.save_button.Type = AntdUI.TTypeMini.Primary;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // Clear_button
            // 
            this.Clear_button.Location = new System.Drawing.Point(126, 4);
            this.Clear_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Clear_button.Name = "Clear_button";
            this.Clear_button.Size = new System.Drawing.Size(64, 28);
            this.Clear_button.TabIndex = 3;
            this.Clear_button.Text = "Clear";
            this.Clear_button.Type = AntdUI.TTypeMini.Primary;
            this.Clear_button.Click += new System.EventHandler(this.Clear_button_Click);
            // 
            // chkAutoScroll
            // 
            this.chkAutoScroll.AutoSize = true;
            this.chkAutoScroll.Checked = true;
            this.chkAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoScroll.Location = new System.Drawing.Point(12, 8);
            this.chkAutoScroll.Name = "chkAutoScroll";
            this.chkAutoScroll.Size = new System.Drawing.Size(74, 17);
            this.chkAutoScroll.TabIndex = 0;
            this.chkAutoScroll.Text = "自动滚动";
            this.chkAutoScroll.UseVisualStyleBackColor = true;
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.Size = new System.Drawing.Size(439, 18);
            this.pageHeader1.TabIndex = 5;
            this.pageHeader1.Text = "";
            // 
            // checkbox1
            // 
            this.checkbox1.Location = new System.Drawing.Point(308, 7);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(86, 24);
            this.checkbox1.TabIndex = 5;
            this.checkbox1.Text = "TopLevel";
            this.checkbox1.CheckedChanged += new AntdUI.BoolEventHandler(this.checkbox1_CheckedChanged);
            // 
            // TestLoggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(439, 326);
            this.Controls.Add(this.pageHeader1);
            this.Controls.Add(this._richTextBox);
            this.Controls.Add(this._panelBottom);
            this.Name = "TestLoggerForm";
            this._panelBottom.ResumeLayout(false);
            this._panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }
        private AntdUI.Button save_button;
        private AntdUI.Button Clear_button;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Checkbox checkbox1;
    }
}
