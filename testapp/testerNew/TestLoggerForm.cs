using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace test_antdui
{
    public partial class TestLoggerForm : AntdUI.Window
    {
        private static TestLoggerForm _instance;
        private static readonly object _lock = new object();
        private readonly ConcurrentQueue<LogEntry> _logQueue = new ConcurrentQueue<LogEntry>();
        private readonly SynchronizationContext _syncContext;
        private System.Windows.Forms.Timer _updateTimer;
        private bool _autoScroll = true;
        private int _maxLines = 10000;
        private int _lineCount = 0;
        private string _currentLogFile;
        private string _logPrefix = "";

        public static void SetLogPrefix(string prefix)
        {
            if (Instance != null && !Instance.IsDisposed)
                Instance._logPrefix = prefix ?? "";
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool AutoScroll
        {
            get => _autoScroll;
            set => _autoScroll = value;
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaxLines
        {
            get => _maxLines;
            set => _maxLines = value;
        }

        private TestLoggerForm()
        {
            _syncContext = SynchronizationContext.Current;
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            InitTimer();
            StartNewLog();
        }

        public static TestLoggerForm Instance
        {
            //get
            //{

            //    if (_instance == null || _instance.IsDisposed)
            //    {

            //        lock (_lock)
            //        {

            //            if (SynchronizationContext.Current == null)
            //            {
            //                throw new InvalidOperationException("必须在UI线程调用首次日志！");
            //            }
            //            if (_instance == null || _instance.IsDisposed)
            //            {
            //                _instance = new TestLoggerForm();
            //            }
            //        }
            //    }

            //    return _instance;


            //}

            get
            {
                lock (_lock)
                {
                    // 关键：如果窗体已关闭/释放，直接重建
                    if (_instance == null || _instance.IsDisposed || _instance.IsDisposed)
                    {
                        // 强制在UI线程创建，杜绝多开
                        if (Application.OpenForms.OfType<TestLoggerForm>().Any())
                        {
                            _instance = Application.OpenForms[typeof(TestLoggerForm).ToString()] as TestLoggerForm;
                        }
                        else
                        {
                            // UI线程创建
                            var form = new TestLoggerForm();
                            _instance = form;
                        }
                    }
                    return _instance;
                }
            }
        }

        private void InitTimer()
        {
            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 300;
            _updateTimer.Tick += (s, e) => ProcessLogQueue();
            _updateTimer.Start();
        }

        private void StartNewLog()
        {
            try
            {
                var config = TestConfigManager.Instance;
                string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, config.LogPath ?? "Logs");
                if (!Directory.Exists(logDir))
                    Directory.CreateDirectory(logDir);
                string prefix = string.IsNullOrEmpty(_logPrefix) ? "" : _logPrefix + "_";
                _currentLogFile = Path.Combine(logDir, $"{prefix}test_{DateTime.Now:yyyyMMdd_HHmmss}.log");
            }
            catch
            {
                _currentLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"test_{DateTime.Now:yyyyMMdd_HHmmss}.log");
            }
        }

        public static void Log(string message)
        {
            Instance.AddLog(message);
        }

        public static void Log(string message, Color color)
        {
            Instance.AddLog(message, color);
        }

        public void AddLog(string message, Color? color = null)
        {
            var entry = new LogEntry
            {
                Message = message,
                Timestamp = DateTime.Now,
                Color = color ?? Color.White
            };
            _logQueue.Enqueue(entry);

            try
            {
                if (TestConfigManager.Instance.AutoSaveLog && !string.IsNullOrEmpty(_currentLogFile))
                {
                    string logLine = $"[{entry.Timestamp:HH:mm:ss.fff}] {message}";
                    File.AppendAllText(_currentLogFile, logLine + Environment.NewLine);
                }
            }
            catch { }
        }

        private void ProcessLogQueue()
        {
            if (_logQueue.IsEmpty || IsDisposed || !IsHandleCreated) return;

            try
            {
                while (_logQueue.TryDequeue(out var entry))
                {
                    AppendLine(entry);
                }

                if (_autoScroll && _richTextBox.Lines.Length > 0)
                {
                    _richTextBox.SelectionStart = _richTextBox.Text.Length;
                    _richTextBox.ScrollToCaret();
                }
            }
            catch { }
        }

        private void AppendLine(LogEntry entry)
        {
            if (_lineCount >= _maxLines)
            {
                ClearLog();
            }

            string timeStr = entry.Timestamp.ToString("HH:mm:ss.fff");
            string line = $"[{timeStr}] {entry.Message}";

            int start = _richTextBox.Text.Length;
            _richTextBox.AppendText(line + Environment.NewLine);
            _richTextBox.SelectionStart = start;
            _richTextBox.SelectionLength = line.Length;
            _richTextBox.SelectionColor = entry.Color;
            _lineCount++;
        }

        public void ClearLog()
        {
            try
            {
                _richTextBox.Clear();
                _lineCount = 0;
            }
            catch { }
        }

        public string GetLogText()
        {
            return _richTextBox.Text;
        }

        public void SaveLog(string filePath = null)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Filter = "日志文件|*.log|文本文件|*.txt";
                    dialog.FileName = $"测试日志_{DateTime.Now:yyyyMMdd_HHmmss}.log";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        SaveLogToFile(dialog.FileName);
                    }
                }
            }
            else
            {
                StartNewLog();
                SaveLogToFile(_currentLogFile);
            }
        }

        private void SaveLogToFile(string filePath)
        {
            try
            {
                System.IO.File.WriteAllText(filePath, _richTextBox.Text);
            }
            catch { }
        }



        private void chkAutoScroll_CheckedChanged(object sender, EventArgs e)
        {
            _autoScroll = chkAutoScroll.Checked;
        }

        private class LogEntry
        {
            public string Message { get; set; }
            public DateTime Timestamp { get; set; }
            public Color Color { get; set; }
        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            ClearLog();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            SaveLog();
            StartNewLog();
        }

        private void _richTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkbox1_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if (checkbox1.Checked)
            {
                this.TopMost = true;
            }
            else
            {
                this.TopMost = false;
            }
        }
    }
}
