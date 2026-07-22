using System;
using System.IO;
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

        public static TestConfig Load()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var serializer = new XmlSerializer(typeof(TestConfig));
                    using (var stream = new FileStream(ConfigPath, FileMode.Open))
                    {
                        return (TestConfig)serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"加载配置失败: {ex.Message}");
            }
            return new TestConfig();
        }

        public static void Save(TestConfig config)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(TestConfig));
                using (var stream = new FileStream(ConfigPath, FileMode.Create))
                {
                    serializer.Serialize(stream, config);
                }
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
            _instance.LastExcelPath = path;
            Save();
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
