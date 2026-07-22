using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using AntdUI;

namespace test_antdui
{
    // public delegate string TestFunctionDelegate(string high, string low, out string rsu , string parameter);

    using TestFunctionDelegate = testapp.pointfun;
    public enum TestState { Pending, Running, Pass, Fail, Skipped }
    public enum TestSkipMode { No, Yes }

    [XmlRoot("TestItem")]
    public class TestItem
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("HighLimit")]
        public string HighLimit { get; set; }

        [XmlElement("LowLimit")]
        public string LowLimit { get; set; }

        [XmlElement("Skip")]
        public string Skip { get; set; } = "0";

        [XmlElement("RetryCount")]
        public string RetryCount { get; set; } = "1";

        [XmlElement("FunctionName")]
        public string FunctionName { get; set; }
        [XmlElement("Jumper_Num")]
        public string Jumper_Num { get; set; } = "1";

        [XmlElement("Parameter")]
        public string Parameter { get; set; } = "";

        [XmlIgnore]
        public TestState State { get; set; } = TestState.Pending;

        [XmlIgnore]
        public string MeasuredValue { get; set; } = "";

        [XmlIgnore]
        public string ResultMessage { get; set; } = "";

        [XmlIgnore]
        public int Duration { get; set; }

        [XmlIgnore]
        public CellBadge StateBadge { get; set; } = new CellBadge(TState.Default, "---");

        [XmlIgnore]
        public DateTime StartTime { get; set; }

        [XmlIgnore]
        public DateTime EndTime { get; set; }

        [XmlIgnore]
        public Color BackColor { get; set; } = Color.Transparent;

        public bool IsSkipped => Skip == "1" || Skip.ToUpper() == "YES";
        public bool IsPass => State == TestState.Pass;
        public int MaxRetry => int.TryParse(RetryCount, out int r) ? Math.Max(1, r) : 1;

        public void Reset()
        {
            State = TestState.Pending;
            MeasuredValue = "";
            ResultMessage = "";
            Duration = 0;
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            StateBadge = new CellBadge(TState.Default, "---");
            BackColor = Color.Transparent;
        }
    }

    [XmlRoot("TestProject")]
    public class TestProject
    {
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("TestItems")]
        public List<TestItem> Items { get; set; } = new List<TestItem>();

        public void ResetAll()
        {
            foreach (var item in Items)
            {
                item.Reset();
            }
        }

        public void ResetAllBadges()
        {
            foreach (var item in Items)
            {
                item.State = TestState.Pending;
                item.StateBadge = new CellBadge(TState.Default, "---");
                item.MeasuredValue = "";
                item.Duration = 0;
                item.BackColor = Color.Transparent;
            }
        }
    }
}
