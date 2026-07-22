namespace testapp
{
    partial class coil_project_4_unit
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
            this.components = new System.ComponentModel.Container();
            this.formMenuStrip1 = new ReaLTaiizor.Controls.FormMenuStrip();
            this.abcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugrelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edittestcaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearcounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dut4 = new testapp.test_case_control();
            this.dut3 = new testapp.test_case_control();
            this.dut1 = new testapp.test_case_control();
            this.dut2 = new testapp.test_case_control();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.formMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // formMenuStrip1
            // 
            this.formMenuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.formMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.formMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abcToolStripMenuItem,
            this.defToolStripMenuItem});
            this.formMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.formMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.formMenuStrip1.Name = "formMenuStrip1";
            this.formMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.formMenuStrip1.Size = new System.Drawing.Size(1206, 25);
            this.formMenuStrip1.TabIndex = 4;
            this.formMenuStrip1.Text = "formMenuStrip1";
            // 
            // abcToolStripMenuItem
            // 
            this.abcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugrelayToolStripMenuItem,
            this.edittestcaseToolStripMenuItem,
            this.clearcounterToolStripMenuItem});
            this.abcToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.abcToolStripMenuItem.Name = "abcToolStripMenuItem";
            this.abcToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.abcToolStripMenuItem.Text = "setup";
            // 
            // debugrelayToolStripMenuItem
            // 
            this.debugrelayToolStripMenuItem.Name = "debugrelayToolStripMenuItem";
            this.debugrelayToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.debugrelayToolStripMenuItem.Text = "debug_relay";
            this.debugrelayToolStripMenuItem.Click += new System.EventHandler(this.debugrelayToolStripMenuItem_Click);
            // 
            // edittestcaseToolStripMenuItem
            // 
            this.edittestcaseToolStripMenuItem.Name = "edittestcaseToolStripMenuItem";
            this.edittestcaseToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.edittestcaseToolStripMenuItem.Text = "edit_testcase";
            this.edittestcaseToolStripMenuItem.Click += new System.EventHandler(this.edittestcaseToolStripMenuItem_Click);
            // 
            // clearcounterToolStripMenuItem
            // 
            this.clearcounterToolStripMenuItem.Name = "clearcounterToolStripMenuItem";
            this.clearcounterToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.clearcounterToolStripMenuItem.Text = "clear_counter";
            this.clearcounterToolStripMenuItem.Click += new System.EventHandler(this.clearcounterToolStripMenuItem_Click);
            // 
            // defToolStripMenuItem
            // 
            this.defToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem});
            this.defToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.defToolStripMenuItem.Name = "defToolStripMenuItem";
            this.defToolStripMenuItem.Size = new System.Drawing.Size(99, 21);
            this.defToolStripMenuItem.Text = "set_Language";
            this.defToolStripMenuItem.Click += new System.EventHandler(this.defToolStripMenuItem_DoubleClick);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.englishToolStripMenuItem.Text = "english";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dut4, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dut3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dut1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dut2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(30, 3, 3, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1206, 618);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // dut4
            // 
            this.dut4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dut4.done_dealwith_flog = 0;
            this.dut4.init_flog = 0;
            this.dut4.Location = new System.Drawing.Point(606, 302);
            this.dut4.Name = "dut4";
            this.dut4.Size = new System.Drawing.Size(597, 293);
            this.dut4.TabIndex = 0;
            // 
            // dut3
            // 
            this.dut3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dut3.done_dealwith_flog = 0;
            this.dut3.init_flog = 0;
            this.dut3.Location = new System.Drawing.Point(3, 302);
            this.dut3.Name = "dut3";
            this.dut3.Size = new System.Drawing.Size(597, 293);
            this.dut3.TabIndex = 1;
            // 
            // dut1
            // 
            this.dut1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dut1.done_dealwith_flog = 0;
            this.dut1.init_flog = 0;
            this.dut1.Location = new System.Drawing.Point(3, 3);
            this.dut1.Name = "dut1";
            this.dut1.Size = new System.Drawing.Size(597, 293);
            this.dut1.TabIndex = 2;
            // 
            // dut2
            // 
            this.dut2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dut2.done_dealwith_flog = 0;
            this.dut2.init_flog = 0;
            this.dut2.Location = new System.Drawing.Point(606, 3);
            this.dut2.Name = "dut2";
            this.dut2.Size = new System.Drawing.Size(597, 293);
            this.dut2.TabIndex = 3;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 621);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(1206, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(169, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // coil_project_4_unit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(1206, 643);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.formMenuStrip1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MainMenuStrip = this.formMenuStrip1;
            this.Name = "coil_project_4_unit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "coil_project_4_unit";
            this.Load += new System.EventHandler(this.coil_project_4_unit_Load);
            this.Shown += new System.EventHandler(this.coil_project_4_unit_Shown);
            this.formMenuStrip1.ResumeLayout(false);
            this.formMenuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private test_case_control dut4;
        private test_case_control dut2;
        private test_case_control dut1;
        private test_case_control dut3;
        private ReaLTaiizor.Controls.FormMenuStrip formMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem abcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem debugrelayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edittestcaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem clearcounterToolStripMenuItem;
        public System.Windows.Forms.Timer timer1;
    }
}