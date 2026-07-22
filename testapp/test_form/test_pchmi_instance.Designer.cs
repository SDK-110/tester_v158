namespace testapp.test_form
{
    partial class test_pchmi_instance
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
            PCHMI.STARTGIF startgif1 = new PCHMI.STARTGIF();
            PCHMI.KEYBEEP keybeep1 = new PCHMI.KEYBEEP();
            PCHMI.TLOG tlog1 = new PCHMI.TLOG();
            PCHMI.WINDOW_SIZE windoW_SIZE1 = new PCHMI.WINDOW_SIZE();
            PCHMI.limits limits1 = new PCHMI.limits();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(test_pchmi_instance));
            PCHMI.InterLock interLock1 = new PCHMI.InterLock();
            PCHMI.ST_SW_IF sT_SW_IF1 = new PCHMI.ST_SW_IF();
            PCHMI.ST_SW_IF sT_SW_IF2 = new PCHMI.ST_SW_IF();
            this.config1 = new PCHMI.CONFIG();
            this.按钮1 = new PCHMI.按钮(this.components);
            this.SuspendLayout();
            // 
            // config1
            // 
            this.config1.MAIN_HMI_IP = "";
            this.config1.MODBUS服务器配置 = null;
            this.config1.PC时间保存地址 = null;
            this.config1.主页显示到上次位置 = false;
            this.config1.允许同时运行多个程序 = false;
            this.config1.功能权限分配 = null;
            startgif1.动画图片 = null;
            startgif1.动画时间 = 1000;
            startgif1.登录界面 = "";
            this.config1.开机界面 = startgif1;
            this.config1.快速登录注销时间 = ((uint)(60u));
            keybeep1.WAV文件路径 = "";
            keybeep1.启用 = true;
            keybeep1.时长 = 120;
            keybeep1.频率 = 2000;
            this.config1.按键音 = keybeep1;
            tlog1.日志保存目录 = "D:\\TextLog";
            this.config1.操作日志 = tlog1;
            this.config1.数据库连接 = null;
            this.config1.数据路径 = "D:\\";
            this.config1.画面 = null;
            this.config1.登录方式 = PCHMI.CONFIG.LOGType.快速登录;
            windoW_SIZE1.软键盘 = PCHMI.WINDOW_SIZE.SIZE.小;
            this.config1.窗口尺寸 = windoW_SIZE1;
            this.config1.等比缩放 = false;
            limits1.PLC = ((uint)(0u));
            limits1.地址 = "";
            limits1.限制类型 = PCHMI.limits.LType.无效;
            this.config1.运行限制 = limits1;
            this.config1.通讯配置 = new string[] {
        resources.GetString("config1.通讯配置")};
            this.config1.通讯配置文件名 = "";
            this.config1.随机数保存地址 = null;
            // 
            // 按钮1
            // 
            this.按钮1.BackColor = System.Drawing.Color.Lime;
            this.按钮1.ForeColor = System.Drawing.Color.Black;
            this.按钮1.HDADDR = "Y00";
            this.按钮1.Location = new System.Drawing.Point(231, 37);
            this.按钮1.LockValue = ((uint)(0u));
            this.按钮1.Name = "按钮1";
            this.按钮1.PLC = ((uint)(0u));
            this.按钮1.Size = new System.Drawing.Size(122, 36);
            this.按钮1.TabIndex = 0;
            this.按钮1.Text = "fsdaf";
            this.按钮1.UseVisualStyleBackColor = false;
            this.按钮1.Value = ((ulong)(0ul));
            interLock1.HDADDR = "";
            interLock1.PLC = ((uint)(0u));
            interLock1.互锁启用值 = ((uint)(1u));
            interLock1.互锁地址 = "";
            interLock1.互锁显示图标 = null;
            interLock1.互锁显示文本 = "LOCK";
            interLock1.互锁类型 = PCHMI.InterLock.DatType.BIT;
            this.按钮1.互锁 = interLock1;
            this.按钮1.单击事件参数 = "NULL";
            this.按钮1.安全级别 = ((uint)(0u));
            this.按钮1.开关功能.PLC = new uint[] {
        ((uint)(0u))};
            this.按钮1.开关功能.地址 = new string[] {
        "Y00"};
            this.按钮1.开关功能.开关 = new PCHMI.FTYPE.ButtonType[] {
        PCHMI.FTYPE.ButtonType.瞬动};
            this.按钮1.开关功能.扩展 = new string[] {
        ""};
            this.按钮1.快捷键 = "";
            this.按钮1.指示类型 = PCHMI.按钮.DatType.BIT;
            this.按钮1.操作确认 = false;
            this.按钮1.操作确认提示 = new string[] {
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null};
            this.按钮1.操作记录 = "";
            this.按钮1.数值改变事件参数 = "NULL";
            this.按钮1.显示内容.BkImg = null;
            this.按钮1.显示内容.状态切换条件 = PCHMI.Employee.STSW.按序号值切换状态;
            sT_SW_IF1.BkColor = System.Drawing.Color.Lime;
            sT_SW_IF1.Img = null;
            sT_SW_IF1.MaxVal = ((ulong)(0ul));
            sT_SW_IF1.MinVal = ((ulong)(0ul));
            sT_SW_IF1.Txt0 = "fsdaf";
            sT_SW_IF1.Txt1 = "";
            sT_SW_IF1.Txt2 = "";
            sT_SW_IF1.Txt3 = "";
            sT_SW_IF1.Txt4 = "";
            sT_SW_IF1.Txt5 = "";
            sT_SW_IF1.Txt6 = "";
            sT_SW_IF1.Txt7 = "";
            sT_SW_IF1.TxtColor = System.Drawing.Color.Black;
            sT_SW_IF1.TxtNumber = "";
            sT_SW_IF2.BkColor = System.Drawing.Color.Red;
            sT_SW_IF2.Img = null;
            sT_SW_IF2.MaxVal = ((ulong)(0ul));
            sT_SW_IF2.MinVal = ((ulong)(0ul));
            sT_SW_IF2.Txt0 = "ttt";
            sT_SW_IF2.Txt1 = "";
            sT_SW_IF2.Txt2 = "";
            sT_SW_IF2.Txt3 = "";
            sT_SW_IF2.Txt4 = "";
            sT_SW_IF2.Txt5 = "";
            sT_SW_IF2.Txt6 = "";
            sT_SW_IF2.Txt7 = "";
            sT_SW_IF2.TxtColor = System.Drawing.Color.Black;
            sT_SW_IF2.TxtNumber = "";
            this.按钮1.显示内容.状态文本 = new PCHMI.ST_SW_IF[] {
        sT_SW_IF1,
        sT_SW_IF2};
            this.按钮1.显示内容.默认状态文本ID = ((uint)(0u));
            this.按钮1.权限提示文本 = "";
            this.按钮1.语言 = ((uint)(0u));
            // 
            // test_pchmi_instance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.按钮1);
            this.Name = "test_pchmi_instance";
            this.Text = "test_2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.test_2_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private PCHMI.CONFIG config1;
        private PCHMI.按钮 按钮1;
    }
}