namespace testapp
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.button1 = new System.Windows.Forms.Button();
            this.rep2 = new AxReportProj1.AxReportX();
            ((System.ComponentModel.ISupportInitialize)(this.rep2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(684, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // rep2
            // 
            this.rep2.Location = new System.Drawing.Point(-2, 12);
            this.rep2.Name = "rep2";
            this.rep2.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("rep2.OcxState")));
            this.rep2.Size = new System.Drawing.Size(671, 398);
            this.rep2.TabIndex = 1;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 413);
            this.Controls.Add(this.rep2);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.SizeChanged += new System.EventHandler(this.Form2_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.rep2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private AxReportProj1.AxReportX rep2;
    }
}