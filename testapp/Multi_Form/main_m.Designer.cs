namespace testapp.duochuangti
{
    partial class main_m
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
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2015LightTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015LightTheme();
            this.vS2015BlueTheme1 = new WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.materialToolStripMenuItem1 = new ReaLTaiizor.Controls.MaterialToolStripMenuItem();
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.setdebugrelayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setdebugrelayseries2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.functionMenuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new ReaLTaiizor.Controls.MaterialToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loopTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(242)))));
            this.dockPanel1.Location = new System.Drawing.Point(0, 26);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Padding = new System.Windows.Forms.Padding(6);
            this.dockPanel1.ShowAutoHideContentOnHover = false;
            this.dockPanel1.Size = new System.Drawing.Size(1370, 701);
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Theme = this.vS2015LightTheme1;
            this.dockPanel1.ActiveContentChanged += new System.EventHandler(this.dockPanel1_ActiveContentChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 727);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1370, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dockPanel1);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1370, 727);
            this.panel1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.materialToolStripMenuItem1,
            this.functionMenuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1370, 26);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // materialToolStripMenuItem1
            // 
            this.materialToolStripMenuItem1.AutoSize = false;
            this.materialToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem,
            this.toolStripSeparator1,
            this.setdebugrelayToolStripMenuItem,
            this.setdebugrelayseries2ToolStripMenuItem});
            this.materialToolStripMenuItem1.Image = global::testapp.Properties.Resources.zhinengshejiis07;
            this.materialToolStripMenuItem1.Name = "materialToolStripMenuItem1";
            this.materialToolStripMenuItem1.Size = new System.Drawing.Size(80, 22);
            this.materialToolStripMenuItem1.Text = "SetUp";
            this.materialToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.materialToolStripMenuItem1.Click += new System.EventHandler(this.materialToolStripMenuItem1_Click);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.resetToolStripMenuItem.Text = "reset_windows_layout";
            this.resetToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // setdebugrelayToolStripMenuItem
            // 
            this.setdebugrelayToolStripMenuItem.Name = "setdebugrelayToolStripMenuItem";
            this.setdebugrelayToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.setdebugrelayToolStripMenuItem.Text = "set_debug_relay_series_1";
            this.setdebugrelayToolStripMenuItem.Click += new System.EventHandler(this.setdebugrelayToolStripMenuItem_Click);
            // 
            // setdebugrelayseries2ToolStripMenuItem
            // 
            this.setdebugrelayseries2ToolStripMenuItem.Name = "setdebugrelayseries2ToolStripMenuItem";
            this.setdebugrelayseries2ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.setdebugrelayseries2ToolStripMenuItem.Text = "set_debug_relay_series_2";
            this.setdebugrelayseries2ToolStripMenuItem.Click += new System.EventHandler(this.setdebugrelayseries2ToolStripMenuItem_Click);
            // 
            // functionMenuToolStripMenuItem
            // 
            this.functionMenuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripSeparator2,
            this.loopTestToolStripMenuItem});
            this.functionMenuToolStripMenuItem.Image = global::testapp.Properties.Resources.tool_pro;
            this.functionMenuToolStripMenuItem.Name = "functionMenuToolStripMenuItem";
            this.functionMenuToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.functionMenuToolStripMenuItem.Text = "Function_Menu";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.AutoSize = false;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(217, 24);
            this.toolStripTextBox1.Text = "Clean_Statistic_Data";
            this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(186, 6);
            // 
            // loopTestToolStripMenuItem
            // 
            this.loopTestToolStripMenuItem.Name = "loopTestToolStripMenuItem";
            this.loopTestToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.loopTestToolStripMenuItem.Text = "Loop_Test_DUT1";
            this.loopTestToolStripMenuItem.Click += new System.EventHandler(this.loopTestTool4dut1StripMenuItem_Click);
            // 
            // main_m
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "main_m";
            this.Text = "TESTER";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_m_FormClosing);
            this.Load += new System.EventHandler(this.main_m_Load);
            this.Shown += new System.EventHandler(this.main_m_Shown);
            this.Resize += new System.EventHandler(this.main_m_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2015LightTheme vS2015LightTheme1;
        private WeifenLuo.WinFormsUI.Docking.VS2015BlueTheme vS2015BlueTheme1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private ReaLTaiizor.Controls.MaterialToolStripMenuItem materialToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem setdebugrelayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setdebugrelayseries2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem functionMenuToolStripMenuItem;
        private ReaLTaiizor.Controls.MaterialToolStripMenuItem toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem loopTestToolStripMenuItem;
    }
}