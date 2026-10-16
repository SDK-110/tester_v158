using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using AntdUI;

namespace test_antdui
{
    // public delegate string TestFunctionDelegate(string high, string low, out string rsu , string parameter);
    using TestFunctionDelegate = testapp.pointfun;
    public enum TestState { Pending, Running, Pass, Fail, Skipped }
    public enum TestSkipMode { No, Yes }

    /// <summary>
    /// 测试项数据模型。
    ///
    /// 实现 INotifyPropertyChanged 是必需的，不是可选项：
    /// AntdUI.Table 会缓存每行的渲染内容，光靠重绘（Invalidate）不会重读数据源；
    /// 实测（tableprobe 探针）只有两种方式能让单元格更新 ——
    ///   1) 调 Table.Refresh()：整表重绘，实测差异 3078px，是"测试时每项抖一下"的来源；
    ///   2) 数据对象实现 INotifyPropertyChanged：只重绘受影响的那一格，实测差异 240px。
    /// 所以走第 2 条：属性变化自动通知表格，主窗体不再需要每项调 Refresh()。
    ///
    /// 每个 setter 都做了相等判断 —— 批量重置（ResetAllBadges）时值没变就不通知，
    /// 避免整表逐格重绘。
    /// </summary>
    [XmlRoot("TestItem")]
    public class TestItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void Notify([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int _id;
        private string _name;
        private string _highLimit;
        private string _lowLimit;
        private string _skip = "0";
        private string _retryCount = "1";
        private string _functionName;
        private string _jumperNum = "1";
        private string _parameter = "";
        private TestState _state = TestState.Pending;
        private string _measuredValue = "";
        private string _resultMessage = "";
        private int _duration;
        private CellBadge _stateBadge = new CellBadge(TState.Default, "---");
        private DateTime _startTime;
        private DateTime _endTime;
        private Color _backColor = Color.Transparent;

        [XmlElement("Id")]
        public int Id
        {
            get => _id;
            set { if (_id == value) return; _id = value; Notify(); }
        }

        [XmlElement("Name")]
        public string Name
        {
            get => _name;
            set { if (_name == value) return; _name = value; Notify(); }
        }

        [XmlElement("HighLimit")]
        public string HighLimit
        {
            get => _highLimit;
            set { if (_highLimit == value) return; _highLimit = value; Notify(); }
        }

        [XmlElement("LowLimit")]
        public string LowLimit
        {
            get => _lowLimit;
            set { if (_lowLimit == value) return; _lowLimit = value; Notify(); }
        }

        [XmlElement("Skip")]
        public string Skip
        {
            get => _skip;
            set { if (_skip == value) return; _skip = value; Notify(); }
        }

        [XmlElement("RetryCount")]
        public string RetryCount
        {
            get => _retryCount;
            set { if (_retryCount == value) return; _retryCount = value; Notify(); }
        }

        [XmlElement("FunctionName")]
        public string FunctionName
        {
            get => _functionName;
            set { if (_functionName == value) return; _functionName = value; Notify(); }
        }

        [XmlElement("Jumper_Num")]
        public string Jumper_Num
        {
            get => _jumperNum;
            set { if (_jumperNum == value) return; _jumperNum = value; Notify(); }
        }

        [XmlElement("Parameter")]
        public string Parameter
        {
            get => _parameter;
            set { if (_parameter == value) return; _parameter = value; Notify(); }
        }

        [XmlIgnore]
        public TestState State
        {
            get => _state;
            set { if (_state == value) return; _state = value; Notify(); }
        }

        [XmlIgnore]
        public string MeasuredValue
        {
            get => _measuredValue;
            set { if (_measuredValue == value) return; _measuredValue = value; Notify(); }
        }

        [XmlIgnore]
        public string ResultMessage
        {
            get => _resultMessage;
            set { if (_resultMessage == value) return; _resultMessage = value; Notify(); }
        }

        [XmlIgnore]
        public int Duration
        {
            get => _duration;
            set { if (_duration == value) return; _duration = value; Notify(); }
        }

        [XmlIgnore]
        public CellBadge StateBadge
        {
            get => _stateBadge;
            set { _stateBadge = value; Notify(); }
        }

        [XmlIgnore]
        public DateTime StartTime
        {
            get => _startTime;
            set { if (_startTime == value) return; _startTime = value; Notify(); }
        }

        [XmlIgnore]
        public DateTime EndTime
        {
            get => _endTime;
            set { if (_endTime == value) return; _endTime = value; Notify(); }
        }

        [XmlIgnore]
        public Color BackColor
        {
            get => _backColor;
            set { if (_backColor == value) return; _backColor = value; Notify(); }
        }

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
