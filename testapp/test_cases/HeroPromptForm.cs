using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace testapp.test_cases
{
    /// <summary>
    /// HERO 测试人工交互窗体 — 替代 MessageBox。
    /// 支持图片展示、OK/NG 双按钮确认。
    ///
    /// 用法:
    ///   var result = HeroPromptForm.Show("标题", "提示文本", "C:\\img.png", true);
    ///   result == DialogResult.OK   → 操作员确认
    ///   result == DialogResult.No  → 操作员判 NG
    /// </summary>
    public class HeroPromptForm : Form
    {
        private readonly Label lblMessage;
        private readonly PictureBox picImage;
        private readonly Button btnOK;
        private readonly Button btnNG;
        private readonly Panel pnlImage;
        private readonly Panel pnlButtons;

        /// <param name="title">窗体标题</param>
        /// <param name="message">提示文本</param>
        /// <param name="imagePath">图片路径 (可选, 空则不显示图片区)</param>
        /// <param name="showNG">true=显示OK+NG; false=仅显示OK</param>
        public HeroPromptForm(string title, string message, string imagePath, bool showNG)
        {
            this.Text = title;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Microsoft YaHei", 10F);

            int formWidth = 560;
            int formHeight = 0;

            // ── 图片面板 ──
            pnlImage = new Panel
            {
                Dock = DockStyle.Top,
                Height = 250,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(10)
            };

            picImage = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            bool hasImage = !string.IsNullOrEmpty(imagePath) && File.Exists(imagePath);
            if (hasImage)
            {
                try
                {
                    using var img = Image.FromFile(imagePath);
                    int maxW = formWidth - 40;
                    int maxH = 250;
                    double ratio = Math.Min((double)maxW / img.Width, (double)maxH / img.Height);
                    pnlImage.Height = (int)(img.Height * ratio) + 20;
                    picImage.Image = Image.FromFile(imagePath);
                    pnlImage.Controls.Add(picImage);
                    formHeight += pnlImage.Height;
                }
                catch
                {
                    hasImage = false;
                }
            }

            if (!hasImage)
            {
                pnlImage.Height = 0;
                pnlImage.Visible = false;
            }

            // ── 消息文本 ──
            lblMessage = new Label
            {
                Dock = DockStyle.Top,
                Text = message,
                TextAlign = ContentAlignment.MiddleCenter,
                AutoSize = false,
                Height = 80,
                Padding = new Padding(20, 15, 20, 15),
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Regular),
                ForeColor = Color.FromArgb(40, 40, 40)
            };

            int lineEstimate = Math.Max(1, message.Length / 30 + (message.Contains("\n") ? message.Split('\n').Length : 0));
            lblMessage.Height = Math.Max(60, lineEstimate * 28 + 30);
            formHeight += lblMessage.Height;

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

            btnNG = new Button
            {
                Text = "NG",
                Size = new Size(120, 36),
                Font = new Font("Microsoft YaHei", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnNG.FlatAppearance.BorderSize = 0;
            btnNG.Click += (s, e) => { this.DialogResult = DialogResult.No; this.Close(); };

            if (showNG)
            {
                pnlButtons.Controls.Add(btnNG);
            }
            pnlButtons.Controls.Add(btnOK);

            this.Load += (s, e) =>
            {
                if (showNG)
                {
                    btnOK.Location = new Point((pnlButtons.Width - 260) / 2, 12);
                    btnNG.Location = new Point(btnOK.Right + 20, 12);
                }
                else
                {
                    btnOK.Location = new Point((pnlButtons.Width - 120) / 2, 12);
                }
            };

            formHeight += pnlButtons.Height + 40;

            this.ClientSize = new Size(formWidth, formHeight);
            this.Controls.Add(pnlButtons);
            this.Controls.Add(lblMessage);
            this.Controls.Add(pnlImage);

            this.KeyPreview = true;
            this.KeyPress += (s, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else if (e.KeyChar == (char)Keys.Escape && showNG)
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
            };

            this.TopMost = true;
        }

        /// <summary>
        /// 静态调用方法 — 显示提示窗体并返回操作员选择。
        /// </summary>
        /// <param name="title">窗体标题</param>
        /// <param name="message">提示文本</param>
        /// <param name="imagePath">图片路径 (可选)</param>
        /// <param name="showNG">true=显示OK+NG按钮; false=仅OK</param>
        /// <returns>DialogResult.OK 或 DialogResult.No</returns>
        public static DialogResult Show(string title, string message, string imagePath = "", bool showNG = false)
        {
            using var form = new HeroPromptForm(title, message, imagePath, showNG);
            return form.ShowDialog();
        }

        /// <summary>
        /// 解析 d 参数中的 prompt 和 image 字段。
        /// 兼容两种格式:
        ///   1. 原始文本: "请检查产品外观" → msg=原文, image=""
        ///   2. 键值对: "prompt=请检查;image=C:\x.png" → msg=值, image=路径
        /// </summary>
        /// <param name="d">原始 d 参数</param>
        /// <param name="defaultMsg">d 为空时的默认提示文本</param>
        /// <returns>(提示文本, 图片路径)</returns>
        public static (string msg, string image) ParsePrompt(string d, string defaultMsg)
        {
            if (string.IsNullOrEmpty(d))
                return (defaultMsg, "");

            if (d.Contains("prompt=") || d.Contains("image="))
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var pair in d.Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    int eq = pair.IndexOf('=');
                    if (eq > 0)
                        dict[pair.Substring(0, eq).Trim()] = pair.Substring(eq + 1).Trim();
                }
                string msg = defaultMsg;
                if (dict.TryGetValue("prompt", out var m) && !string.IsNullOrEmpty(m))
                    msg = m;
                else if (dict.TryGetValue("msg", out var m2) && !string.IsNullOrEmpty(m2))
                    msg = m2;
                string img = dict.TryGetValue("image", out var im) ? im : "";
                return (msg, img);
            }

            return (d, "");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                picImage.Image?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
