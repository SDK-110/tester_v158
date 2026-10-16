﻿using System.Drawing;

namespace test_antdui
{
    /// <summary>
    /// 统一主题色板 —— 色值对齐 WPF 版参考界面 F:\WPF_TESTER\MainWindow.xaml。
    /// 调色只需改这里, 不要在各窗体里写死颜色。
    /// </summary>
    internal static class Palette
    {
        // ---- 背景 ----
        /// <summary>窗体底 #0A0F1E</summary>
        public static readonly Color Bg = Color.FromArgb(0x0A, 0x0F, 0x1E);
        /// <summary>卡片/面板底 #12192D</summary>
        public static readonly Color Card = Color.FromArgb(0x12, 0x19, 0x2D);
        /// <summary>更深的面板底 #0A101B</summary>
        public static readonly Color CardDark = Color.FromArgb(0x0A, 0x10, 0x1B);
        /// <summary>输入框底 #1C253E</summary>
        public static readonly Color Input = Color.FromArgb(0x1C, 0x25, 0x3E);
        /// <summary>表头/标题栏底 #151D35（WPF 里 DataGrid 表头色）</summary>
        public static readonly Color Header = Color.FromArgb(0x15, 0x1D, 0x35);
        /// <summary>分隔线/网格线 #1F2B44</summary>
        public static readonly Color Divider = Color.FromArgb(0x1F, 0x2B, 0x44);
        /// <summary>进度条底槽 #1F2B44</summary>
        public static readonly Color Track = Color.FromArgb(0x1F, 0x2B, 0x44);

        // ---- 强调 ----
        /// <summary>主强调 #3E8CFF</summary>
        public static readonly Color Accent = Color.FromArgb(0x3E, 0x8C, 0xFF);
        /// <summary>次强调/强调文字 #6AA9FF</summary>
        public static readonly Color AccentSoft = Color.FromArgb(0x6A, 0xA9, 0xFF);

        // ---- 状态 ----
        /// <summary>通过 #2EE5A0</summary>
        public static readonly Color Pass = Color.FromArgb(0x2E, 0xE5, 0xA0);
        /// <summary>失败 #FF7A6E</summary>
        public static readonly Color Fail = Color.FromArgb(0xFF, 0x7A, 0x6E);
        /// <summary>警告 #FFB020</summary>
        public static readonly Color Warning = Color.FromArgb(0xFF, 0xB0, 0x20);
        /// <summary>次级警告 #FFD166</summary>
        public static readonly Color WarnSoft = Color.FromArgb(0xFF, 0xD1, 0x66);
        /// <summary>通过行底色 #12352C</summary>
        public static readonly Color PassDim = Color.FromArgb(0x12, 0x35, 0x2C);
        /// <summary>失败行底色 #3A1F22</summary>
        public static readonly Color FailDim = Color.FromArgb(0x3A, 0x1F, 0x22);

        // ---- 文字 ----
        /// <summary>主文字 #EEF5FF</summary>
        public static readonly Color TextPrimary = Color.FromArgb(0xEE, 0xF5, 0xFF);
        /// <summary>次文字 #9BB3E0</summary>
        public static readonly Color TextSecondary = Color.FromArgb(0x9B, 0xB3, 0xE0);
        /// <summary>弱文字 #6C86B0</summary>
        public static readonly Color TextMuted = Color.FromArgb(0x6C, 0x86, 0xB0);
    }
}
