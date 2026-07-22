using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace test_antdui
{
    public class ProductionTracker
    {
        private static ProductionTracker _instance;
        private static readonly object _lock = new object();
        private string _filePath;
        private ProductionData _data;

        public static ProductionTracker Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new ProductionTracker();
                    }
                }
                return _instance;
            }
        }

        private class ProductionData
        {
            // Barcode
            public string BarcodeRegex { get; set; } = "";
            public bool BarcodeEnabled { get; set; }
            public int BarcodeLength { get; set; }

            // Employee / Shift
            public string OperatorName { get; set; } = "";
            public string OperatorPassword { get; set; } = "";
            public string CurrentShift { get; set; } = "白班";
            public string LineName { get; set; } = "N/A";
            public string StationName { get; set; } = "N/A";
            public string ProductName { get; set; } = "N/A";

            // Production stats
            public int TotalCount { get; set; }
            public int TotalPass { get; set; }
            public int TotalFail { get; set; }
            public int[] HourlyPass { get; set; } = new int[24];
            public int[] HourlyFail { get; set; } = new int[24];

            // SN records
            public int SnRecordCount { get; set; }
            public string LastSn { get; set; } = "---";
            public string LastResult { get; set; } = "";
            public string LastTime { get; set; } = "";
            public Dictionary<int, string> SnRecords { get; set; } = new Dictionary<int, string>();
        }

        private ProductionTracker()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProductionTracker.json");
            Load();
        }

        public void Reload()
        {
            Load();
        }

        private void Load()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    _data = JsonConvert.DeserializeObject<ProductionData>(json);
                    if (_data != null)
                    {
                        _data.HourlyPass = _data.HourlyPass ?? new int[24];
                        _data.HourlyFail = _data.HourlyFail ?? new int[24];
                        _data.SnRecords = _data.SnRecords ?? new Dictionary<int, string>();
                        return;
                    }
                }
                catch { }
            }

            _data = new ProductionData();
        }

        private void Save()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_data, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch { }
        }

        // ─── Hourly Production ───

        public int GetHourlyPass(int hour) => _data.HourlyPass[hour];
        public int GetHourlyFail(int hour) => _data.HourlyFail[hour];

        public int CurrentHourPass => _data.HourlyPass[DateTime.Now.Hour];
        public int CurrentHourFail => _data.HourlyFail[DateTime.Now.Hour];
        public int CurrentHourTotal => CurrentHourPass + CurrentHourFail;

        public int TotalPass => _data.TotalPass;
        public int TotalFail => _data.TotalFail;
        public int TotalCount => _data.TotalCount;

        public double TotalYield => TotalCount > 0 ? Math.Round((double)TotalPass / TotalCount * 100, 1) : 0;
        public double HourlyYield
        {
            get
            {
                int total = CurrentHourTotal;
                return total > 0 ? Math.Round((double)CurrentHourPass / total * 100, 1) : 0;
            }
        }

        public void RecordPass()
        {
            int h = DateTime.Now.Hour;
            _data.HourlyPass[h]++;
            _data.TotalPass++;
            _data.TotalCount++;
            Save();
        }

        public void RecordFail()
        {
            int h = DateTime.Now.Hour;
            _data.HourlyFail[h]++;
            _data.TotalFail++;
            _data.TotalCount++;
            Save();
        }

        public void ClearHourlyStats(int? hour = null)
        {
            int h = hour ?? DateTime.Now.Hour;
            _data.HourlyPass[h] = 0;
            _data.HourlyFail[h] = 0;
            Save();
        }

        public void ClearAllHourlyStats()
        {
            for (int i = 0; i < 24; i++)
            {
                _data.HourlyPass[i] = 0;
                _data.HourlyFail[i] = 0;
            }
            Save();
        }

        public void ResetAllStats()
        {
            ClearAllHourlyStats();
            _data.TotalCount = 0;
            _data.TotalPass = 0;
            _data.TotalFail = 0;
            Save();
        }

        // ─── Chart Data ───

        public int[] GetHourlyPassArray() => (int[])_data.HourlyPass.Clone();
        public int[] GetHourlyFailArray() => (int[])_data.HourlyFail.Clone();

        // ─── Barcode ───

        public string BarcodeRegex
        {
            get => _data.BarcodeRegex;
            set { _data.BarcodeRegex = value; Save(); }
        }

        public bool BarcodeEnabled
        {
            get => _data.BarcodeEnabled;
            set { _data.BarcodeEnabled = value; Save(); }
        }

        public int BarcodeLength
        {
            get => _data.BarcodeLength;
            set { _data.BarcodeLength = value; Save(); }
        }

        public bool ValidateBarcode(string barcode)
        {
            if (string.IsNullOrEmpty(barcode)) return false;
            string regex = BarcodeRegex;
            if (string.IsNullOrEmpty(regex)) return true;
            return Regex.IsMatch(barcode.Trim(), regex);
        }

        // ─── SN Recording ───

        public void RecordSnResult(string sn, bool passed)
        {
            if (string.IsNullOrEmpty(sn) || sn == "NA") return;
            int idx = _data.SnRecordCount;
            _data.SnRecords[idx] = $"{sn}|{DateTime.Now:yyyy-MM-dd HH:mm:ss}|{(passed ? "PASS" : "FAIL")}";
            _data.SnRecordCount = idx + 1;
            _data.LastSn = sn;
            _data.LastResult = passed ? "PASS" : "FAIL";
            _data.LastTime = DateTime.Now.ToString("HH:mm:ss");
            Save();
        }

        public string LastSn => _data.LastSn;
        public string LastSnResult => _data.LastResult;
        public string LastSnTime => _data.LastTime;
        public int SnRecordCount => _data.SnRecordCount;

        // ─── Employee / Shift ───

        public string OperatorName
        {
            get => _data.OperatorName;
            set { _data.OperatorName = value; Save(); }
        }

        public string CurrentShift
        {
            get => string.IsNullOrEmpty(_data.CurrentShift) ? "白班" : _data.CurrentShift;
            set { _data.CurrentShift = value; Save(); }
        }

        public bool IsDayShift => CurrentShift == "白班";

        public void ToggleShift()
        {
            CurrentShift = IsDayShift ? "夜班" : "白班";
        }

        public string LineName => _data.LineName;
        public string StationName => _data.StationName;
        public string ProductName => _data.ProductName;

        public string OperatorPassword
        {
            get => _data.OperatorPassword;
            set { _data.OperatorPassword = value; Save(); }
        }
    }
}
