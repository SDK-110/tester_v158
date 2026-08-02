using System;
using System.Windows.Forms;
using ScottPlot.WinForms;

namespace testapp
{
    partial class parallel_form
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
            components = new System.ComponentModel.Container();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            chart1 = new FormsPlot();
            button2 = new Button();
            chart2 = new FormsPlot();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            修改配置ToolStripMenuItem = new ToolStripMenuItem();
            修改后重新加载ToolStripMenuItem = new ToolStripMenuItem();
            清理白板数据ToolStripMenuItem = new ToolStripMenuItem();
            请夜班数据ToolStripMenuItem = new ToolStripMenuItem();
            同时清除白夜班数据ToolStripMenuItem = new ToolStripMenuItem();
            设置项目ToolStripMenuItem = new ToolStripMenuItem();
            打开校验程序表ToolStripMenuItem = new ToolStripMenuItem();
            重新加载测试表ToolStripMenuItem = new ToolStripMenuItem();
            调试DEBUGToolStripMenuItem = new ToolStripMenuItem();
            debugmyrelayToolStripMenuItem = new ToolStripMenuItem();
            skrelaydebugToolStripMenuItem = new ToolStripMenuItem();
            语言配置ToolStripMenuItem = new ToolStripMenuItem();
            英语ToolStripMenuItem = new ToolStripMenuItem();
            汉语ToolStripMenuItem = new ToolStripMenuItem();
            cancelTESTToolStripMenuItem = new ToolStripMenuItem();
            productionInfoToolStripMenuItem = new ToolStripMenuItem();
            toolStripTextBox1 = new ToolStripTextBox();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label3 = new Label();
            textBox2 = new TextBox();
            textBox6 = new TextBox();
            label7 = new Label();
            textBox5 = new TextBox();
            label6 = new Label();
            progressBar1 = new ProgressBar();
            textBox4 = new TextBox();
            label5 = new Label();
            textBox3 = new TextBox();
            label4 = new Label();
            timer1 = new Timer(components);
            richTextBox1 = new RichTextBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            easyChartX1 = new FormsPlot();
            panel1 = new Panel();
            reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            timer2 = new Timer(components);
            timer3 = new Timer(components);
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            toolStripStatusLabel3 = new ToolStripStatusLabel();
            toolStripStatusLabel4 = new ToolStripStatusLabel();
            toolStripStatusLabel5 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // chart1
            // 
            chart1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chart1.BackColor = System.Drawing.Color.Transparent;
            chart1.Location = new System.Drawing.Point(539, 4);
            chart1.Margin = new Padding(4, 4, 4, 4);
            chart1.Name = "chart1";
            chart1.Size = new System.Drawing.Size(421, 141);
            chart1.TabIndex = 2;
            chart1.TabStop = false;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(button2, 2);
            button2.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            button2.Location = new System.Drawing.Point(539, 382);
            button2.Margin = new Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(443, 58);
            button2.TabIndex = 3;
            button2.Text = "start";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // chart2
            // 
            chart2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            chart2.BackColor = System.Drawing.Color.Transparent;
            chart2.Location = new System.Drawing.Point(539, 153);
            chart2.Margin = new Padding(4, 4, 4, 4);
            chart2.Name = "chart2";
            chart2.Size = new System.Drawing.Size(421, 136);
            chart2.TabIndex = 4;
            chart2.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, 语言配置ToolStripMenuItem, productionInfoToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.RenderMode = ToolStripRenderMode.Professional;
            menuStrip1.Size = new System.Drawing.Size(1394, 27);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { 修改配置ToolStripMenuItem, 修改后重新加载ToolStripMenuItem, 清理白板数据ToolStripMenuItem, 请夜班数据ToolStripMenuItem, 同时清除白夜班数据ToolStripMenuItem, 设置项目ToolStripMenuItem, 打开校验程序表ToolStripMenuItem, 重新加载测试表ToolStripMenuItem, 调试DEBUGToolStripMenuItem, debugmyrelayToolStripMenuItem, skrelaydebugToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(44, 21);
            toolStripMenuItem1.Text = "配置";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // 修改配置ToolStripMenuItem
            // 
            修改配置ToolStripMenuItem.Name = "修改配置ToolStripMenuItem";
            修改配置ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            修改配置ToolStripMenuItem.Text = "修改配置";
            修改配置ToolStripMenuItem.Click += 修改配置ToolStripMenuItem_Click;
            // 
            // 修改后重新加载ToolStripMenuItem
            // 
            修改后重新加载ToolStripMenuItem.Name = "修改后重新加载ToolStripMenuItem";
            修改后重新加载ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            修改后重新加载ToolStripMenuItem.Text = "修改后重新加载";
            修改后重新加载ToolStripMenuItem.Click += 修改后重新加载ToolStripMenuItem_Click;
            // 
            // 清理白板数据ToolStripMenuItem
            // 
            清理白板数据ToolStripMenuItem.Name = "清理白板数据ToolStripMenuItem";
            清理白板数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            清理白板数据ToolStripMenuItem.Text = "清理白班数据";
            清理白板数据ToolStripMenuItem.Click += 清理白板数据ToolStripMenuItem_Click;
            // 
            // 请夜班数据ToolStripMenuItem
            // 
            请夜班数据ToolStripMenuItem.Name = "请夜班数据ToolStripMenuItem";
            请夜班数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            请夜班数据ToolStripMenuItem.Text = "清除夜班数据";
            请夜班数据ToolStripMenuItem.Click += 请夜班数据ToolStripMenuItem_Click;
            // 
            // 同时清除白夜班数据ToolStripMenuItem
            // 
            同时清除白夜班数据ToolStripMenuItem.Name = "同时清除白夜班数据ToolStripMenuItem";
            同时清除白夜班数据ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            同时清除白夜班数据ToolStripMenuItem.Text = "同时清除白夜班数据";
            同时清除白夜班数据ToolStripMenuItem.Click += 同时清除白夜班数据ToolStripMenuItem_Click;
            // 
            // 设置项目ToolStripMenuItem
            // 
            设置项目ToolStripMenuItem.Name = "设置项目ToolStripMenuItem";
            设置项目ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            设置项目ToolStripMenuItem.Text = "设置项目";
            设置项目ToolStripMenuItem.Click += 设置项目ToolStripMenuItem_Click;
            // 
            // 打开校验程序表ToolStripMenuItem
            // 
            打开校验程序表ToolStripMenuItem.Name = "打开校验程序表ToolStripMenuItem";
            打开校验程序表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            打开校验程序表ToolStripMenuItem.Text = "打开校验程序表";
            打开校验程序表ToolStripMenuItem.Click += 打开校验程序表ToolStripMenuItem_Click;
            // 
            // 重新加载测试表ToolStripMenuItem
            // 
            重新加载测试表ToolStripMenuItem.Name = "重新加载测试表ToolStripMenuItem";
            重新加载测试表ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            重新加载测试表ToolStripMenuItem.Text = "重新加载测试表";
            重新加载测试表ToolStripMenuItem.Click += 重新加载测试表ToolStripMenuItem_Click;
            // 
            // 调试DEBUGToolStripMenuItem
            // 
            调试DEBUGToolStripMenuItem.Name = "调试DEBUGToolStripMenuItem";
            调试DEBUGToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            调试DEBUGToolStripMenuItem.Text = "调试DEBUG";
            调试DEBUGToolStripMenuItem.Click += 调试DEBUGToolStripMenuItem_Click;
            // 
            // debugmyrelayToolStripMenuItem
            // 
            debugmyrelayToolStripMenuItem.Name = "debugmyrelayToolStripMenuItem";
            debugmyrelayToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            debugmyrelayToolStripMenuItem.Text = "debug_myrelay";
            debugmyrelayToolStripMenuItem.Click += debugmyrelayToolStripMenuItem_Click;
            // 
            // skrelaydebugToolStripMenuItem
            // 
            skrelaydebugToolStripMenuItem.Name = "skrelaydebugToolStripMenuItem";
            skrelaydebugToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            skrelaydebugToolStripMenuItem.Text = "sk_relay_debug";
            skrelaydebugToolStripMenuItem.Click += skrelaydebugToolStripMenuItem_Click;
            // 
            // 语言配置ToolStripMenuItem
            // 
            语言配置ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { 英语ToolStripMenuItem, 汉语ToolStripMenuItem, cancelTESTToolStripMenuItem });
            语言配置ToolStripMenuItem.Name = "语言配置ToolStripMenuItem";
            语言配置ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            语言配置ToolStripMenuItem.Text = "语言设置";
            // 
            // 英语ToolStripMenuItem
            // 
            英语ToolStripMenuItem.Name = "英语ToolStripMenuItem";
            英语ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            英语ToolStripMenuItem.Text = "英语";
            英语ToolStripMenuItem.Click += 英语ToolStripMenuItem_Click;
            // 
            // 汉语ToolStripMenuItem
            // 
            汉语ToolStripMenuItem.Name = "汉语ToolStripMenuItem";
            汉语ToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            汉语ToolStripMenuItem.Text = "汉语";
            汉语ToolStripMenuItem.Click += 汉语ToolStripMenuItem_Click;
            // 
            // cancelTESTToolStripMenuItem
            // 
            cancelTESTToolStripMenuItem.Name = "cancelTESTToolStripMenuItem";
            cancelTESTToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            cancelTESTToolStripMenuItem.Text = "Cancel_TEST";
            cancelTESTToolStripMenuItem.Click += cancelTESTToolStripMenuItem_Click;
            // 
            // productionInfoToolStripMenuItem
            // 
            productionInfoToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripTextBox1 });
            productionInfoToolStripMenuItem.Name = "productionInfoToolStripMenuItem";
            productionInfoToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            productionInfoToolStripMenuItem.Text = "产品信息";
            // 
            // toolStripTextBox1
            // 
            toolStripTextBox1.Name = "toolStripTextBox1";
            toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            toolStripTextBox1.Text = "123456";
            toolStripTextBox1.TextChanged += toolStripTextBox1_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label1, 2);
            label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 134);
            label1.Location = new System.Drawing.Point(539, 331);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(443, 16);
            label1.TabIndex = 7;
            label1.Text = "total：100PCS |NG : 3|OK:99";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(textBox1, 2);
            textBox1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            textBox1.Location = new System.Drawing.Point(539, 499);
            textBox1.Margin = new Padding(4, 4, 4, 4);
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new System.Drawing.Size(443, 31);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label2, 2);
            label2.Location = new System.Drawing.Point(539, 478);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(443, 17);
            label2.TabIndex = 9;
            label2.Text = "条码扫入：";
            // 
            // label3
            // 
            tableLayoutPanel1.SetColumnSpan(label3, 3);
            label3.Dock = DockStyle.Fill;
            label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label3.Location = new System.Drawing.Point(990, 293);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(400, 54);
            label3.TabIndex = 10;
            label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            textBox2.Dock = DockStyle.Fill;
            textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            textBox2.ForeColor = System.Drawing.Color.Lime;
            textBox2.Location = new System.Drawing.Point(4, 714);
            textBox2.Margin = new Padding(4, 4, 4, 4);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = ScrollBars.Vertical;
            textBox2.Size = new System.Drawing.Size(527, 201);
            textBox2.TabIndex = 11;
            textBox2.TabStop = false;
            textBox2.SizeChanged += textBox2_SizeChanged;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // textBox6
            // 
            textBox6.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(textBox6, 2);
            textBox6.Location = new System.Drawing.Point(1100, 679);
            textBox6.Margin = new Padding(4, 4, 4, 4);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new System.Drawing.Size(290, 23);
            textBox6.TabIndex = 8;
            textBox6.TabStop = false;
            // 
            // label7
            // 
            label7.Anchor = AnchorStyles.Left;
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label7.Location = new System.Drawing.Point(990, 684);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(87, 16);
            label7.TabIndex = 7;
            label7.Text = "TimerIndex";
            // 
            // textBox5
            // 
            textBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox5.Location = new System.Drawing.Point(968, 679);
            textBox5.Margin = new Padding(4, 4, 4, 4);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.Size = new System.Drawing.Size(14, 23);
            textBox5.TabIndex = 6;
            textBox5.TabStop = false;
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.Left;
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label6.Location = new System.Drawing.Point(539, 685);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(77, 14);
            label6.TabIndex = 5;
            label6.Text = "Totle time";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(progressBar1, 2);
            progressBar1.Location = new System.Drawing.Point(539, 598);
            progressBar1.Margin = new Padding(4, 4, 4, 4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(443, 38);
            progressBar1.TabIndex = 4;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(textBox4, 2);
            textBox4.Location = new System.Drawing.Point(1100, 644);
            textBox4.Margin = new Padding(4, 4, 4, 4);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new System.Drawing.Size(290, 23);
            textBox4.TabIndex = 3;
            textBox4.TabStop = false;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Left;
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label5.Location = new System.Drawing.Point(990, 649);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(71, 16);
            label5.TabIndex = 2;
            label5.Text = "end time";
            // 
            // textBox3
            // 
            textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox3.Location = new System.Drawing.Point(968, 644);
            textBox3.Margin = new Padding(4, 4, 4, 4);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(14, 23);
            textBox3.TabIndex = 1;
            textBox3.TabStop = false;
            textBox3.TextChanged += textBox3_TextChanged;
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Left;
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            label4.Location = new System.Drawing.Point(539, 649);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(87, 16);
            label4.TabIndex = 0;
            label4.Text = "start time";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 300;
            timer1.Tick += timer1_Tick;
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            tableLayoutPanel1.SetColumnSpan(richTextBox1, 3);
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new System.Drawing.Point(990, 351);
            richTextBox1.Margin = new Padding(4, 4, 4, 4);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            tableLayoutPanel1.SetRowSpan(richTextBox1, 4);
            richTextBox1.Size = new System.Drawing.Size(400, 285);
            richTextBox1.TabIndex = 13;
            richTextBox1.TabStop = false;
            richTextBox1.Text = "";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55.4979248F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44.5020752F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 22F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 44F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 253F));
            tableLayoutPanel1.Controls.Add(textBox6, 4, 8);
            tableLayoutPanel1.Controls.Add(textBox2, 0, 9);
            tableLayoutPanel1.Controls.Add(label7, 3, 8);
            tableLayoutPanel1.Controls.Add(chart1, 1, 0);
            tableLayoutPanel1.Controls.Add(textBox5, 2, 8);
            tableLayoutPanel1.Controls.Add(chart2, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 1, 8);
            tableLayoutPanel1.Controls.Add(label2, 1, 4);
            tableLayoutPanel1.Controls.Add(textBox4, 4, 7);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 5);
            tableLayoutPanel1.Controls.Add(label5, 3, 7);
            tableLayoutPanel1.Controls.Add(button2, 1, 3);
            tableLayoutPanel1.Controls.Add(textBox3, 2, 7);
            tableLayoutPanel1.Controls.Add(progressBar1, 1, 6);
            tableLayoutPanel1.Controls.Add(label1, 1, 2);
            tableLayoutPanel1.Controls.Add(label4, 1, 7);
            tableLayoutPanel1.Controls.Add(richTextBox1, 3, 3);
            tableLayoutPanel1.Controls.Add(label3, 3, 2);
            tableLayoutPanel1.Controls.Add(easyChartX1, 1, 9);
            tableLayoutPanel1.Controls.Add(panel1, 0, 0);
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            tableLayoutPanel1.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.91575F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.08425F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 97F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 51F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 99F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 208F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
            tableLayoutPanel1.Size = new System.Drawing.Size(1394, 919);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // easyChartX1
            // 
            easyChartX1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            easyChartX1.BackColor = System.Drawing.Color.WhiteSmoke;
            tableLayoutPanel1.SetColumnSpan(easyChartX1, 5);
            easyChartX1.Location = new System.Drawing.Point(537, 713);
            easyChartX1.Margin = new Padding(2, 3, 2, 3);
            easyChartX1.Name = "easyChartX1";
            easyChartX1.Size = new System.Drawing.Size(855, 203);
            easyChartX1.TabIndex = 15;
            easyChartX1.Visible = false;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            panel1.Controls.Add(reoGridControl1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(4, 4);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            tableLayoutPanel1.SetRowSpan(panel1, 9);
            panel1.Size = new System.Drawing.Size(527, 702);
            panel1.TabIndex = 14;
            // 
            // reoGridControl1
            // 
            reoGridControl1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            reoGridControl1.ColumnHeaderContextMenuStrip = null;
            reoGridControl1.Dock = DockStyle.Fill;
            reoGridControl1.LeadHeaderContextMenuStrip = null;
            reoGridControl1.Location = new System.Drawing.Point(0, 0);
            reoGridControl1.Margin = new Padding(0, 0, 4, 0);
            reoGridControl1.MinimumSize = new System.Drawing.Size(233, 690);
            reoGridControl1.Name = "reoGridControl1";
            reoGridControl1.RowHeaderContextMenuStrip = null;
            reoGridControl1.Script = null;
            reoGridControl1.SheetTabContextMenuStrip = null;
            reoGridControl1.SheetTabNewButtonVisible = false;
            reoGridControl1.SheetTabVisible = false;
            reoGridControl1.SheetTabWidth = 70;
            reoGridControl1.ShowScrollEndSpacing = true;
            reoGridControl1.Size = new System.Drawing.Size(527, 702);
            reoGridControl1.TabIndex = 0;
            reoGridControl1.Text = "reoGridControl1";
            // 
            // timer2
            // 
            timer2.Interval = 300;
            timer2.Tick += timer2_Tick;
            // 
            // timer3
            // 
            timer3.Tick += timer3_Tick;
            // 
            // statusStrip1
            // 
            statusStrip1.AllowMerge = false;
            statusStrip1.AutoSize = false;
            statusStrip1.BackColor = System.Drawing.Color.WhiteSmoke;
            statusStrip1.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            statusStrip1.GripStyle = ToolStripGripStyle.Visible;
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3, toolStripStatusLabel4, toolStripStatusLabel5 });
            statusStrip1.Location = new System.Drawing.Point(0, 944);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(1, 0, 16, 0);
            statusStrip1.RightToLeft = RightToLeft.No;
            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Size = new System.Drawing.Size(1394, 35);
            statusStrip1.Stretch = false;
            statusStrip1.TabIndex = 15;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.RightToLeft = RightToLeft.No;
            toolStripStatusLabel1.Size = new System.Drawing.Size(222, 30);
            toolStripStatusLabel1.Text = "                                   ";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel2.BorderStyle = Border3DStyle.Etched;
            toolStripStatusLabel2.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.RightToLeft = RightToLeft.No;
            toolStripStatusLabel2.Size = new System.Drawing.Size(226, 30);
            toolStripStatusLabel2.Text = "                                   ";
            toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3
            // 
            toolStripStatusLabel3.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel3.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            toolStripStatusLabel3.Size = new System.Drawing.Size(226, 30);
            toolStripStatusLabel3.Text = "                                   ";
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel4.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new System.Drawing.Size(226, 30);
            toolStripStatusLabel4.Text = "                                   ";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.BorderSides = ToolStripStatusLabelBorderSides.Right;
            toolStripStatusLabel5.Font = new System.Drawing.Font("Microsoft YaHei UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new System.Drawing.Size(226, 30);
            toolStripStatusLabel5.Text = "                                   ";
            // 
            // parallel_form
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            ClientSize = new System.Drawing.Size(1394, 979);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);
            DoubleBuffered = true;
            ForeColor = System.Drawing.SystemColors.ControlText;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 4, 4, 4);
            Name = "parallel_form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "parallel_form";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            Shown += Form1_Shown;
            SizeChanged += Form1_SizeChanged;
            KeyDown += Form1_KeyDown;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private ScottPlot.WinForms.FormsPlot chart1;
        private System.Windows.Forms.Button button2;
        private ScottPlot.WinForms.FormsPlot chart2;
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
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private ScottPlot.WinForms.FormsPlot easyChartX1;
        private ToolStripMenuItem debugmyrelayToolStripMenuItem;
    
        private Timer timer2;
        private ToolStripMenuItem skrelaydebugToolStripMenuItem;
        internal Timer timer3;
       // private Sunisoft.IrisSkin.SkinCollectionItem skinCollectionItem1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel4;
        private ToolStripStatusLabel toolStripStatusLabel5;
        private ToolStripMenuItem cancelTESTToolStripMenuItem;
        private ToolStripMenuItem productionInfoToolStripMenuItem;
        private ToolStripTextBox toolStripTextBox1;
    }
}
