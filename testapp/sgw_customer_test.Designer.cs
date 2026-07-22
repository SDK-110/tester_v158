
namespace testapp
{
    partial class sgw_customer_test
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
            this.led1 = new SeeSharpTools.JY.GUI.LED();
            this.gaugeLinear1 = new SeeSharpTools.JY.GUI.GaugeLinear();
            this.led2 = new SeeSharpTools.JY.GUI.LED();
            this.led3 = new SeeSharpTools.JY.GUI.LED();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gaugeLinear2 = new SeeSharpTools.JY.GUI.GaugeLinear();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // led1
            // 
            this.led1.BlinkColor = System.Drawing.Color.Lime;
            this.led1.BlinkInterval = 1000;
            this.led1.BlinkOn = false;
            this.led1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led1.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led1.Location = new System.Drawing.Point(31, 25);
            this.led1.Name = "led1";
            this.led1.OffColor = System.Drawing.Color.DarkRed;
            this.led1.OnColor = System.Drawing.Color.Lime;
            this.led1.Size = new System.Drawing.Size(87, 88);
            this.led1.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led1.TabIndex = 0;
            this.led1.Value = false;
            this.led1.Load += new System.EventHandler(this.led1_Load);
            this.led1.Click += new System.EventHandler(this.led1_Click);
            // 
            // gaugeLinear1
            // 
            this.gaugeLinear1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gaugeLinear1.LineColor = System.Drawing.Color.White;
            this.gaugeLinear1.Location = new System.Drawing.Point(50, 225);
            this.gaugeLinear1.Maximum = 5D;
            this.gaugeLinear1.Minimum = 0D;
            this.gaugeLinear1.Name = "gaugeLinear1";
            this.gaugeLinear1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.gaugeLinear1.Sidedirection = SeeSharpTools.JY.GUI.GaugeLinear.SideDirection.RightToLeft;
            this.gaugeLinear1.Size = new System.Drawing.Size(68, 288);
            this.gaugeLinear1.TabIndex = 1;
            this.gaugeLinear1.TextColor = System.Drawing.Color.Black;
            this.gaugeLinear1.TextDecimals = 0;
            this.gaugeLinear1.TickColor = System.Drawing.Color.Black;
            this.gaugeLinear1.TickMajorLength = 5;
            this.gaugeLinear1.TickMinorLength = 1;
            this.gaugeLinear1.TrackerColor = System.Drawing.SystemColors.ControlDark;
            this.gaugeLinear1.TrackerSize = new System.Drawing.Size(20, 10);
            this.gaugeLinear1.Value = 5D;
            this.gaugeLinear1.ValueChanged += new SeeSharpTools.JY.GUI.GaugeLinear.ValueChangedHandler(this.gaugeLinear1_ValueChanged);
            // 
            // led2
            // 
            this.led2.BlinkColor = System.Drawing.Color.Lime;
            this.led2.BlinkInterval = 1000;
            this.led2.BlinkOn = false;
            this.led2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led2.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led2.Location = new System.Drawing.Point(1047, 278);
            this.led2.Name = "led2";
            this.led2.OffColor = System.Drawing.Color.Silver;
            this.led2.OnColor = System.Drawing.Color.Lime;
            this.led2.Size = new System.Drawing.Size(86, 81);
            this.led2.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led2.TabIndex = 2;
            this.led2.Value = false;
            // 
            // led3
            // 
            this.led3.BlinkColor = System.Drawing.Color.Lime;
            this.led3.BlinkInterval = 1000;
            this.led3.BlinkOn = false;
            this.led3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.led3.Interacton = SeeSharpTools.JY.GUI.LED.InteractionStyle.Indicator;
            this.led3.Location = new System.Drawing.Point(1049, 417);
            this.led3.Name = "led3";
            this.led3.OffColor = System.Drawing.Color.DimGray;
            this.led3.OnColor = System.Drawing.Color.Lime;
            this.led3.Size = new System.Drawing.Size(86, 81);
            this.led3.Style = SeeSharpTools.JY.GUI.LED.LedStyle.Circular;
            this.led3.TabIndex = 3;
            this.led3.Value = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(124, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 28);
            this.label1.TabIndex = 4;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 300;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1017, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "SIMCCID:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1084, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "_________________________";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1017, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "CSQ:";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(1086, 86);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(225, 173);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "___________________________";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(1157, 561);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 21);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "112";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(992, 564);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(161, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "按红色键前请输入电话号码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(48, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 11;
            this.label6.Text = "LTE音量设定";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1084, 255);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(221, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "测试状态按产品灰色键会亮起可以挂电话";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1084, 399);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(233, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "测试状态按产品红色键会亮起并会拨打电话";
            // 
            // gaugeLinear2
            // 
            this.gaugeLinear2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.gaugeLinear2.LineColor = System.Drawing.Color.White;
            this.gaugeLinear2.Location = new System.Drawing.Point(206, 225);
            this.gaugeLinear2.Maximum = 5D;
            this.gaugeLinear2.Minimum = 0D;
            this.gaugeLinear2.Name = "gaugeLinear2";
            this.gaugeLinear2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.gaugeLinear2.Sidedirection = SeeSharpTools.JY.GUI.GaugeLinear.SideDirection.RightToLeft;
            this.gaugeLinear2.Size = new System.Drawing.Size(68, 288);
            this.gaugeLinear2.TabIndex = 14;
            this.gaugeLinear2.TextColor = System.Drawing.Color.Black;
            this.gaugeLinear2.TextDecimals = 0;
            this.gaugeLinear2.TickColor = System.Drawing.Color.Black;
            this.gaugeLinear2.TickMajorLength = 5;
            this.gaugeLinear2.TickMinorLength = 1;
            this.gaugeLinear2.TrackerColor = System.Drawing.SystemColors.Control;
            this.gaugeLinear2.TrackerSize = new System.Drawing.Size(20, 10);
            this.gaugeLinear2.Value = 3D;
            this.gaugeLinear2.ValueChanged += new SeeSharpTools.JY.GUI.GaugeLinear.ValueChangedHandler(this.gaugeLinear2_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(203, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 12);
            this.label9.TabIndex = 15;
            this.label9.Text = "MIC灵敏度";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(710, 25);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(227, 12);
            this.label10.TabIndex = 17;
            this.label10.Text = "_____________________________________";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(643, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 16;
            this.label11.Text = "IMEI:";
            // 
            // sgw_customer_test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImage = global::testapp.Properties.Resources._20220530210617;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1323, 668);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.gaugeLinear2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.led3);
            this.Controls.Add(this.led2);
            this.Controls.Add(this.gaugeLinear1);
            this.Controls.Add(this.led1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "sgw_customer_test";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "sgw_customer_test";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.sgw_customer_test_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SeeSharpTools.JY.GUI.LED led1;
        private SeeSharpTools.JY.GUI.GaugeLinear gaugeLinear1;
        private SeeSharpTools.JY.GUI.LED led2;
        private SeeSharpTools.JY.GUI.LED led3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private SeeSharpTools.JY.GUI.GaugeLinear gaugeLinear2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}