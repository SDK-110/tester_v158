using System;
using System.Windows.Forms;

namespace testapp
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void rep1ini() {


        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.修改配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改后重新加载ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清理白板数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.请夜班数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同时清除白夜班数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开校验程序表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重新加载测试表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调试DEBUGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.语言配置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.英语ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.汉语ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.rep1 = new AxReportProj1.AxReportX();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep1)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(560, 54);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(304, 139);
            this.chart1.TabIndex = 2;
            this.chart1.TabStop = false;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(554, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(298, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "start";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(560, 206);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(304, 131);
            this.chart2.TabIndex = 4;
            this.chart2.TabStop = false;
            this.chart2.Text = "chart2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.语言配置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1042, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.修改配置ToolStripMenuItem,
            this.修改后重新加载ToolStripMenuItem,
            this.清理白板数据ToolStripMenuItem,
            this.请夜班数据ToolStripMenuItem,
            this.同时清除白夜班数据ToolStripMenuItem,
            this.设置项目ToolStripMenuItem,
            this.打开校验程序表ToolStripMenuItem,
            this.重新加载测试表ToolStripMenuItem,
            this.调试DEBUGToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem1.Text = "配置";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // 修改配置ToolStripMenuItem
            // 
            this.修改配置ToolStripMenuItem.Name = "修改配置ToolStripMenuItem";
            this.修改配置ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.修改配置ToolStripMenuItem.Text = "修改配置";
            this.修改配置ToolStripMenuItem.Click += new System.EventHandler(this.修改配置ToolStripMenuItem_Click);
            // 
            // 修改后重新加载ToolStripMenuItem
            // 
            this.修改后重新加载ToolStripMenuItem.Name = "修改后重新加载ToolStripMenuItem";
            this.修改后重新加载ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.修改后重新加载ToolStripMenuItem.Text = "修改后重新加载";
            this.修改后重新加载ToolStripMenuItem.Click += new System.EventHandler(this.修改后重新加载ToolStripMenuItem_Click);
            // 
            // 清理白板数据ToolStripMenuItem
            // 
            this.清理白板数据ToolStripMenuItem.Name = "清理白板数据ToolStripMenuItem";
            this.清理白板数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.清理白板数据ToolStripMenuItem.Text = "清理白班数据";
            this.清理白板数据ToolStripMenuItem.Click += new System.EventHandler(this.清理白板数据ToolStripMenuItem_Click);
            // 
            // 请夜班数据ToolStripMenuItem
            // 
            this.请夜班数据ToolStripMenuItem.Name = "请夜班数据ToolStripMenuItem";
            this.请夜班数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.请夜班数据ToolStripMenuItem.Text = "清除夜班数据";
            this.请夜班数据ToolStripMenuItem.Click += new System.EventHandler(this.请夜班数据ToolStripMenuItem_Click);
            // 
            // 同时清除白夜班数据ToolStripMenuItem
            // 
            this.同时清除白夜班数据ToolStripMenuItem.Name = "同时清除白夜班数据ToolStripMenuItem";
            this.同时清除白夜班数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.同时清除白夜班数据ToolStripMenuItem.Text = "同时清除白夜班数据";
            this.同时清除白夜班数据ToolStripMenuItem.Click += new System.EventHandler(this.同时清除白夜班数据ToolStripMenuItem_Click);
            // 
            // 设置项目ToolStripMenuItem
            // 
            this.设置项目ToolStripMenuItem.Name = "设置项目ToolStripMenuItem";
            this.设置项目ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.设置项目ToolStripMenuItem.Text = "设置项目";
            this.设置项目ToolStripMenuItem.Click += new System.EventHandler(this.设置项目ToolStripMenuItem_Click);
            // 
            // 打开校验程序表ToolStripMenuItem
            // 
            this.打开校验程序表ToolStripMenuItem.Name = "打开校验程序表ToolStripMenuItem";
            this.打开校验程序表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.打开校验程序表ToolStripMenuItem.Text = "打开校验程序表";
            this.打开校验程序表ToolStripMenuItem.Click += new System.EventHandler(this.打开校验程序表ToolStripMenuItem_Click);
            // 
            // 重新加载测试表ToolStripMenuItem
            // 
            this.重新加载测试表ToolStripMenuItem.Name = "重新加载测试表ToolStripMenuItem";
            this.重新加载测试表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.重新加载测试表ToolStripMenuItem.Text = "重新加载测试表";
            this.重新加载测试表ToolStripMenuItem.Click += new System.EventHandler(this.重新加载测试表ToolStripMenuItem_Click);
            // 
            // 调试DEBUGToolStripMenuItem
            // 
            this.调试DEBUGToolStripMenuItem.Name = "调试DEBUGToolStripMenuItem";
            this.调试DEBUGToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.调试DEBUGToolStripMenuItem.Text = "调试DEBUG";
            this.调试DEBUGToolStripMenuItem.Click += new System.EventHandler(this.调试DEBUGToolStripMenuItem_Click);
            // 
            // 语言配置ToolStripMenuItem
            // 
            this.语言配置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.英语ToolStripMenuItem,
            this.汉语ToolStripMenuItem});
            this.语言配置ToolStripMenuItem.Name = "语言配置ToolStripMenuItem";
            this.语言配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.语言配置ToolStripMenuItem.Text = "语言设置";
            // 
            // 英语ToolStripMenuItem
            // 
            this.英语ToolStripMenuItem.Name = "英语ToolStripMenuItem";
            this.英语ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.英语ToolStripMenuItem.Text = "英语";
            this.英语ToolStripMenuItem.Click += new System.EventHandler(this.英语ToolStripMenuItem_Click);
            // 
            // 汉语ToolStripMenuItem
            // 
            this.汉语ToolStripMenuItem.Name = "汉语ToolStripMenuItem";
            this.汉语ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.汉语ToolStripMenuItem.Text = "汉语";
            this.汉语ToolStripMenuItem.Click += new System.EventHandler(this.汉语ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(577, 347);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "total：100PCS |NG : 3|OK:99";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(554, 445);
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(298, 31);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(560, 430);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "条码扫入：";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(858, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(184, 48);
            this.label3.TabIndex = 10;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.ForeColor = System.Drawing.Color.Lime;
            this.textBox2.Location = new System.Drawing.Point(0, 587);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(902, 91);
            this.textBox2.TabIndex = 11;
            this.textBox2.TabStop = false;
            this.textBox2.SizeChanged += new System.EventHandler(this.textBox2_SizeChanged);
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.progressBar1);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(500, 504);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 97);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(364, 47);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(139, 21);
            this.textBox6.TabIndex = 8;
            this.textBox6.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(268, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "timer index";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(98, 44);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(139, 21);
            this.textBox5.TabIndex = 6;
            this.textBox5.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 14);
            this.label6.TabIndex = 5;
            this.label6.Text = "Totle time";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 66);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(542, 30);
            this.progressBar1.TabIndex = 4;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(364, 20);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(139, 21);
            this.textBox4.TabIndex = 3;
            this.textBox4.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(280, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "end time";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(98, 20);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(139, 21);
            this.textBox3.TabIndex = 1;
            this.textBox3.TabStop = false;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(5, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "start time";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(554, 482);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(476, 100);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.TabStop = false;
            this.richTextBox1.Text = "";
            // 
            // rep1
            // 
            this.rep1.Location = new System.Drawing.Point(0, 28);
            this.rep1.Name = "rep1";
            this.rep1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rep1.OcxState")));
            this.rep1.Size = new System.Drawing.Size(554, 481);
            this.rep1.TabIndex = 0;
            this.rep1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 741);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chart2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.rep1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.richTextBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rep1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxReportProj1.AxReportX rep1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 修改配置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改后重新加载ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private TextBox textBox1;
        private Label label2;
        private ToolStripMenuItem 清理白板数据ToolStripMenuItem;
        private ToolStripMenuItem 请夜班数据ToolStripMenuItem;
        private ToolStripMenuItem 同时清除白夜班数据ToolStripMenuItem;
        private Label label3;
        private ToolStripMenuItem 设置项目ToolStripMenuItem;
        private TextBox textBox2;
        private GroupBox groupBox1;
        private ProgressBar progressBar1;
        private TextBox textBox4;
        private Label label5;
        private TextBox textBox3;
        private Label label4;
        private Timer timer1;
        private TextBox textBox5;
        private Label label6;
        private TextBox textBox6;
        private Label label7;
        private ToolStripMenuItem 语言配置ToolStripMenuItem;
        private ToolStripMenuItem 英语ToolStripMenuItem;
        private ToolStripMenuItem 汉语ToolStripMenuItem;
        private ToolStripMenuItem 打开校验程序表ToolStripMenuItem;
        private ToolStripMenuItem 重新加载测试表ToolStripMenuItem;
        private RichTextBox richTextBox1;
        private ToolStripMenuItem 调试DEBUGToolStripMenuItem;
    }
}

