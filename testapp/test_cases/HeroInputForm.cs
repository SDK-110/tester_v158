using System;
using System.Drawing;
using System.Windows.Forms;

namespace testapp.test_cases
{
    /// <summary>
    /// HERO 测试文本输入窗体 — 用于条码/MAC 地址等需要操作员手动输入的场景。
    ///
    /// 用法:
    ///   var result = HeroInputForm.Show("标题", "提示文本", "");
    ///   result == DialogResult.OK   → 确认输入
    ///   result == DialogResult.Cancel → 取消
    /// </summary>
    public class HeroInputForm : Form
    {
        private readonly Label lblMessage;
        private readonly TextBox txtInput;
        private readonly Button btnOK;
        private readonly Button btnCancel;
        private readonly Panel pnlButtons;

        /// <param name="title">窗体标题</param>
        /// <param name="message">提示文本</param>
        /// <param name="defaultText">输入框默认文本</param>
        public HeroInputForm(string title, string message, string defaultText = "")
        {
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Microsoft YaHei", 10F);

            int formWidth = 480;
            int formHeight = 0;

            // ── 消息文本 ──
            lblMessage = new Label
            {
                Dock = DockStyle.Top,
                Text = message,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false,
                Height = 60,
                Padding = new Padding(20, 15, 20, 10),
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            int lineEstimate = Math.Max(1, message.Length / 35 + (message.Contains("\n") ? message.Split('\n').Length : 0));
            lblMessage.Height = Math.Max(50, lineEstimate * 28 + 20);
            formHeight += lblMessage.Height;

            // ── 输入框 ──
            txtInput = new TextBox
            {
                Dock = DockStyle.Top,
                Font = new Font("Consolas", 14F, FontStyle.Regular),
                Height = 40,
                Margin = new Padding(20, 5, 20, 5),
                Text = defaultText,
                CharacterCasing = CharacterCasing.Upper
            };
            txtInput.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            };
            formHeight += 50;

            // ── 按钮面板 ──
            pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(0, 12, 0, 12)
            };

            btnOK = new Button
            {
                Text = "OK",
                Size = new Size(120, 36),
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(34, 139, 34),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOK.FlatAppearance.BorderSize = 0;
            btnOK.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            btnCancel = new Button
            {
                Text = "Cancel",
                Size = new Size(120, 36),
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(120, 120, 120),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            pnlButtons.Controls.Add(btnCancel);
            pnlButtons.Controls.Add(btnOK);

            this.Load += (s, e) =>
            {
                btnOK.Location = new Point((pnlButtons.Width - 260) / 2, 12);
                btnCancel.Location = new Point(btnOK.Right + 20, 12);
                txtInput.Focus();
                txtInput.SelectAll();
            };

            formHeight += pnlButtons.Height + 20;

            this.ClientSize = new Size(formWidth, formHeight);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(txtInput);
            this.Controls.Add(lblMessage);

            this.KeyPreview = true;
            this.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Escape)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            };

            this.TopMost = true;
        }

        /// <summary>
        /// 静态调用方法 — 显示输入窗体并返回操作员输入的文本。
        /// </summary>
        public static DialogResult Show(string title, string message, string defaultText = "")
        {
            using var form = new HeroInputForm(title, message, defaultText);
            var result = form.ShowDialog();
            InputValue = form.txtInput.Text;
            return result;
        }

        /// <summary>
        /// 静态调用方法 — 返回输入文本 (Show 之后调用)。
        /// </summary>
        public static string InputValue { get; private set; } = "";
    }
}
