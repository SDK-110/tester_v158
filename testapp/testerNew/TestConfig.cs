using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace test_antdui
{
    [XmlRoot("TestConfig")]
    public class TestConfig
    {
        [XmlElement("TotalCount")]
        public int TotalCount { get; set; }

        [XmlElement("PassCount")]
        public int PassCount { get; set; }

        [XmlElement("FailCount")]
        public int FailCount { get; set; }

        [XmlElement("LastTestTime")]
        public string LastTestTime { get; set; }

        [XmlElement("LastTestResult")]
        public string LastTestResult { get; set; }

        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("LastExcelPath")]
        public string LastExcelPath { get; set; }

        [XmlElement("SaveMethod")]
        public int SaveMethod { get; set; } = 0;

        [XmlElement("AutoSaveLog")]
        public bool AutoSaveLog { get; set; } = false;

        [XmlElement("LastOrderNo")]
        public string LastOrderNo { get; set; } = "";

        [XmlElement("LastOperatorNo")]
        public string LastOperatorNo { get; set; } = "";

        [XmlElement("ReportPath")]
        public string ReportPath { get; set; } = "Reports";

        [XmlElement("LogPath")]
        public string LogPath { get; set; } = "Logs";

        [XmlElement("StopOnFail")]
        public int StopOnFail { get; set; } = 0;

        [XmlElement("LOGAppend")]
        public int LOGAppend { get; set; } = 0;

        public TestConfig()
        {
            TotalCount = 0;
            PassCount = 0;
            FailCount = 0;
            LastTestTime = "";
            LastTestResult = "";
            ProjectName = "";
            LastExcelPath = "";
            SaveMethod = 1;
            AutoSaveLog = false;
            LastOrderNo = "";
            LastOperatorNo = "";
            ReportPath = "Reports";
            LogPath = "Logs";
            StopOnFail = 0;
            LOGAppend = 0;
                }
    }

    /// <summary>
    /// 测试工程文件路径的存取规则：
    /// 程序目录内的文件存相对路径，外部文件存绝对路径；
    /// 读取时原路径失效则按文件名回退到程序目录查找。
    /// 这样整个程序目录搬移/换机器后，只要文件与 exe 在一起就能自动找回。
    /// </summary>
    public static class ProjectPath
    {
        private static string AppDirectory
            => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>把实际路径转成配置中保存的形式。</summary>
        public static string ToStoredPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return path;
            try
            {
                string full = Path.GetFullPath(path);
                string baseDir = Path.GetFullPath(AppDirectory)
                    .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                    + Path.DirectorySeparatorChar;
                if (full.StartsWith(baseDir, StringComparison.OrdinalIgnoreCase))
                    return full.Substring(baseDir.Length);
                return full;
            }
            catch
            {
                return path;
            }
        }

        /// <summary>把配置中保存的路径解析成可直接使用的绝对路径。</summary>
        public static string ResolvePath(string storedPath)
        {
            if (string.IsNullOrWhiteSpace(storedPath)) return "";
            try
            {
                if (Path.IsPathRooted(storedPath))
                {
                    if (File.Exists(storedPath)) return Path.GetFullPath(storedPath);
                    // 旧配置的绝对路径失效：按文件名到程序目录下找
                    string fallback = Path.Combine(AppDirectory, Path.GetFileName(storedPath));
                    return File.Exists(fallback) ? fallback : Path.GetFullPath(storedPath);
                }
                return Path.Combine(AppDirectory, storedPath);
            }
            catch
            {
                return storedPath;
            }
        }
    }

    public class TestConfigManager
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestConfig.xml");
        private static TestConfig _instance;
        private static readonly object _lock = new object();

        public static TestConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = Load();
                        }
                    }
                }
                return _instance;
            }
        }

        // 不用 XmlSerializer：首次使用要动态生成序列化程序集，实测拖慢启动约 0.9 秒。
        public static TestConfig Load()
        {
            var config = new TestConfig();
            try
            {
                if (!File.Exists(ConfigPath)) return config;
                var root = XDocument.Load(ConfigPath).Root;
                if (root == null) return config;

                config.TotalCount = GetInt(root, nameof(config.TotalCount), config.TotalCount);
                config.PassCount = GetInt(root, nameof(config.PassCount), config.PassCount);
                config.FailCount = GetInt(root, nameof(config.FailCount), config.FailCount);
                config.LastTestTime = Get(root, nameof(config.LastTestTime), config.LastTestTime);
                config.LastTestResult = Get(root, nameof(config.LastTestResult), config.LastTestResult);
                config.ProjectName = Get(root, nameof(config.ProjectName), config.ProjectName);
                config.LastExcelPath = Get(root, nameof(config.LastExcelPath), config.LastExcelPath);
                config.SaveMethod = GetInt(root, nameof(config.SaveMethod), config.SaveMethod);
                config.AutoSaveLog = GetBool(root, nameof(config.AutoSaveLog), config.AutoSaveLog);
                config.LastOrderNo = Get(root, nameof(config.LastOrderNo), config.LastOrderNo);
                config.LastOperatorNo = Get(root, nameof(config.LastOperatorNo), config.LastOperatorNo);
                config.ReportPath = Get(root, nameof(config.ReportPath), config.ReportPath);
                config.LogPath = Get(root, nameof(config.LogPath), config.LogPath);
                config.StopOnFail = GetInt(root, nameof(config.StopOnFail), config.StopOnFail);
                config.LOGAppend = GetInt(root, nameof(config.LOGAppend), config.LOGAppend);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载配置失败: {ex.Message}");
            }

            // 归一化：程序目录内的文件改存相对路径；
            // 原路径失效但程序目录下有同名文件时，自动回写为可用路径。
            string resolved = ProjectPath.ResolvePath(config.LastExcelPath);
            string stored = ProjectPath.ToStoredPath(resolved);
            if (!string.Equals(stored, config.LastExcelPath, StringComparison.Ordinal))
            {
                config.LastExcelPath = stored;
                Save(config);
            }

            return config;
        }

        private static string Get(XElement root, string name, string defaultValue)
            => root.Element(name)?.Value ?? defaultValue;

        private static int GetInt(XElement root, string name, int defaultValue)
            => int.TryParse(root.Element(name)?.Value, out int value) ? value : defaultValue;

        private static bool GetBool(XElement root, string name, bool defaultValue)
            => bool.TryParse(root.Element(name)?.Value, out bool value) ? value : defaultValue;

        public static void Save(TestConfig config)
        {
            try
            {
                var doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", null),
                    new XElement("TestConfig",
                        new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                        new XElement(nameof(config.TotalCount), config.TotalCount),
                        new XElement(nameof(config.PassCount), config.PassCount),
                        new XElement(nameof(config.FailCount), config.FailCount),
                        new XElement(nameof(config.LastTestTime), config.LastTestTime),
                        new XElement(nameof(config.LastTestResult), config.LastTestResult),
                        new XElement(nameof(config.ProjectName), config.ProjectName),
                        new XElement(nameof(config.LastExcelPath), config.LastExcelPath),
                        new XElement(nameof(config.SaveMethod), config.SaveMethod),
                        new XElement(nameof(config.AutoSaveLog), config.AutoSaveLog),
                        new XElement(nameof(config.LastOrderNo), config.LastOrderNo),
                        new XElement(nameof(config.LastOperatorNo), config.LastOperatorNo),
                        new XElement(nameof(config.ReportPath), config.ReportPath),
                        new XElement(nameof(config.LogPath), config.LogPath),
                        new XElement(nameof(config.StopOnFail), config.StopOnFail),
                        new XElement(nameof(config.LOGAppend), config.LOGAppend)));
                doc.Save(ConfigPath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"保存配置失败: {ex.Message}");
            }
        }

        public static void Save()
        {
            Save(_instance);
        }

        public static void UpdateAfterTest(bool pass)
        {
            _instance.TotalCount++;
            if (pass)
                _instance.PassCount++;
            else
                _instance.FailCount++;

            _instance.LastTestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _instance.LastTestResult = pass ? "PASS" : "FAIL";
            Save();
        }

        public static void UpdateProjectName(string name)
        {
            _instance.ProjectName = name;
            Save();
        }

        public static void UpdateLastExcelPath(string path)
        {
            // 程序目录内的文件只存相对路径，避免程序目录搬移后路径失效
            _instance.LastExcelPath = ProjectPath.ToStoredPath(path);
            Save();
        }

        /// <summary>
        /// 当前正在测试的工程文件路径 —— 本应用的唯一权威来源。
        /// 由 ExcelTestLoader.LoadFromExcel() 通过 UpdateLastExcelPath() 写入，与实际加载保持同步。
        ///
        /// 不要再用旧版的 SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0]：
        /// testerNew 从不调用它的 AddProject()（全项目唯一写入点在旧版 Main_f.cs:2243），
        /// 于是它的值会永远停在旧版最后一次打开的工程，导致
        /// "界面显示 / 编辑器打开的工程 ≠ 实际加载的工程"。
        /// </summary>
        public static string CurrentProjectPath
        {
            get { return ProjectPath.ResolvePath(_instance.LastExcelPath); }
        }

        public static void UpdateOrderNo(string orderNo)
        {
            _instance.LastOrderNo = orderNo;
            Save();
        }

        public static void UpdateOperatorNo(string operatorNo)
        {
            _instance.LastOperatorNo = operatorNo;
            Save();
        }

        public static void UpdateSaveMethod(int method)
        {
            _instance.SaveMethod = method;
            Save();
        }

        public static void UpdateAutoSaveLog(bool autoSave)
        {
            _instance.AutoSaveLog = autoSave;
            Save();
        }

        public static void UpdateStopOnFail(int stopOnFail)
        {
            _instance.StopOnFail = stopOnFail;
            Save();
        }

        public static void UpdateLogAppend(int is_append)
        {
            _instance.LOGAppend = is_append;
            Save();
        }

        public static string GetReportPath()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _instance.ReportPath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }

        public static string GetLogPath()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _instance.LogPath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            return path;
        }
    }
}
