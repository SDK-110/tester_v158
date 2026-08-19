using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace test_antdui
{
    public enum SaveMode
    {
        None = 0,
        Excel = 1,
        Append = 2,
        Both = 3
    }

    public class TestReportSaver
    {
        private static readonly string[] Headers = new[] { "序号", "测试项目", "下限", "上限", "实测值", "结果", "时长(ms)" };

        public static void SaveToCsv(TestProject project, string sn, string operatorNo,
            string lineNumber, string workStation, string startTime, string endTime, bool allPass)
        {
            try
            {
                string result = allPass ? "Passed" : "Failed";
                string subDir = allPass ? "pass" : "fail";

                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string csvDir = Path.Combine(baseDir, "SMX_LOG", subDir);
                if (!Directory.Exists(csvDir))
                    Directory.CreateDirectory(csvDir);

                string timestamp = DateTime.Now.ToString("yyMMddHHmmssfff");
                string safeSn = string.IsNullOrEmpty(sn) || sn == "NA" ? "NA" : sn;
                // Remove any invalid file name chars
                foreach (char c in Path.GetInvalidFileNameChars())
                    safeSn = safeSn.Replace(c.ToString(), "");
                string fileName = $"{safeSn}_{timestamp}_{result}.csv";
                string filePath = Path.Combine(csvDir, fileName);

                using (var sw = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    sw.WriteLine($"Date,{DateTime.Now:yyMMdd}");
                    sw.WriteLine($"Line,{lineNumber}");
                    sw.WriteLine($"Work Station,{workStation}");
                    sw.WriteLine($"Person ID,{operatorNo}");
                    sw.WriteLine("Program Name,");
                    sw.WriteLine($"Serial Number,{sn}");
                    sw.WriteLine($"Start Time,{startTime}");
                    sw.WriteLine("NO.,item,High Limit,Low Limit,unit,Value,Result,Step Time");

                    int i = 0;
                    int itemCount = project.Items.Count;
                    int totalSlots = Math.Max(itemCount, 15);

                    for (; i < totalSlots; i++)
                    {
                        if (i < itemCount)
                        {
                            var item = project.Items[i];
                            string val = item.State == TestState.Pending ? "---" : item.MeasuredValue;
                            string judge = item.State == TestState.Pass ? "pass"
                                : item.State == TestState.Fail ? "fail"
                                : item.State == TestState.Skipped ? "skip"
                                : "---";
                            sw.WriteLine($"{i},{item.Name},{item.HighLimit},{item.LowLimit},,{val},{judge},{item.Duration}");
                        }
                        else
                        {
                            sw.WriteLine($"{i}, NA,NA,NA,NA,NA,NA,NA");
                        }
                    }

                    sw.WriteLine($"Unix Time,{ConvertDateTimeInt(DateTime.Now):X}");
                    sw.WriteLine($"End Time,{endTime}");
                    sw.WriteLine($"Test result,{result}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Save CSV report failed: {ex.Message}", ex);
            }
        }

        private static uint ConvertDateTimeInt(DateTime time)
        {
            DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), TimeZoneInfo.Local);
            return (uint)(time - startTime).TotalSeconds;
        }

        public static void SaveTestReport(TestProject project, string sn, string orderNo, string operatorNo, SaveMode mode)
        {
            if (mode == SaveMode.None) return;

            if ((mode & SaveMode.Excel) == SaveMode.Excel)
            {
                SaveToExcel(project, sn, orderNo, operatorNo);
            }

            if ((mode & SaveMode.Append) == SaveMode.Append)
            {
                SaveToAppend(sn, orderNo, operatorNo, project);
            }
        }

        private static void SaveToExcel(TestProject project, string sn, string orderNo, string operatorNo)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string reportDir = Path.Combine(baseDir, "Reports");
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);

                string fileName = string.IsNullOrEmpty(sn) || sn == "NA"
                    ? $"NA_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                    : $"{sn}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string filePath = Path.Combine(reportDir, fileName);

                using (var workbook = new XLWorkbook())
                {
                    var sheet = workbook.Worksheets.Add("测试报告");

                    sheet.Cell(1, 1).Value = "产品序列号(SN)";
                    sheet.Cell(1, 2).Value = sn ?? "NA";
                    sheet.Cell(2, 1).Value = "工单号";
                    sheet.Cell(2, 2).Value = orderNo ?? "";
                    sheet.Cell(3, 1).Value = "员工工号";
                    sheet.Cell(3, 2).Value = operatorNo ?? "";
                    sheet.Cell(4, 1).Value = "测试时间";
                    sheet.Cell(4, 2).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sheet.Cell(5, 1).Value = "测试结果";
                    sheet.Cell(5, 2).Value = GetOverallResult(project);

                    for (int i = 0; i < Headers.Length; i++)
                    {
                        sheet.Cell(7, i + 1).Value = Headers[i];
                    }

                    for (int i = 0; i < project.Items.Count; i++)
                    {
                        var item = project.Items[i];
                        int row = i + 8;
                        sheet.Cell(row, 1).Value = item.Id;
                        sheet.Cell(row, 2).Value = item.Name;
                        sheet.Cell(row, 3).Value = item.LowLimit;
                        sheet.Cell(row, 4).Value = item.HighLimit;
                        sheet.Cell(row, 5).Value = item.MeasuredValue;
                        sheet.Cell(row, 6).Value = item.State == TestState.Pending ? "未执行" : GetStateText(item.State);
                        sheet.Cell(row, 7).Value = item.Duration;
                    }

                    // Bold headers
                    for (int i = 1; i <= Headers.Length; i++)
                        sheet.Cell(7, i).Style.Font.Bold = true;

                    sheet.Columns().AdjustToContents();

                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存Excel报告失败: {ex.Message}", ex);
            }
        }

        private static void SaveToAppend(string sn, string orderNo, string operatorNo, TestProject project)
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string reportDir = Path.Combine(baseDir, "Reports");
                if (!Directory.Exists(reportDir))
                    Directory.CreateDirectory(reportDir);

                string fileName = $"测试记录_{DateTime.Now:yyyyMMdd}.xlsx";
                string filePath = Path.Combine(reportDir, fileName);

                bool fileExists = File.Exists(filePath);

                using (var workbook = fileExists ? new XLWorkbook(filePath) : new XLWorkbook())
                {
                    IXLWorksheet sheet;

                    if (fileExists)
                    {
                        sheet = workbook.Worksheet(1);
                    }
                    else
                    {
                        sheet = workbook.Worksheets.Add("测试记录");
                        CreateAppendHeaders(sheet, project);
                    }

                    int lastRow = sheet.LastRowUsed()?.RowNumber() ?? 1;
                    int newRow = lastRow + 1;

                    int col = 1;
                    sheet.Cell(newRow, col++).Value = sn ?? "NA";
                    sheet.Cell(newRow, col++).Value = orderNo ?? "";
                    sheet.Cell(newRow, col++).Value = operatorNo ?? "";
                    sheet.Cell(newRow, col++).Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sheet.Cell(newRow, col++).Value = GetOverallResult(project);

                    foreach (var item in project.Items)
                    {
                        string colName = $"{item.Name}_({item.HighLimit}_{item.LowLimit})";
                        int headerCol = GetHeaderColumn(sheet, colName);
                        if (headerCol > 0)
                        {
                            if (item.State == TestState.Pending)
                                sheet.Cell(newRow, headerCol).Value = "未执行";
                            else
                                sheet.Cell(newRow, headerCol).Value = item.MeasuredValue;
                        }
                    }

                    sheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存追加记录失败: {ex.Message}", ex);
            }
        }

        private static int GetHeaderColumn(IXLWorksheet sheet, string headerName)
        {
            int colCount = sheet.LastColumnUsed()?.ColumnNumber() ?? 0;
            for (int col = 1; col <= colCount; col++)
            {
                var cellValue = sheet.Cell(1, col).Value.ToString();
                if (cellValue == headerName)
                    return col;
            }
            return -1;
        }

        private static void CreateAppendHeaders(IXLWorksheet sheet, TestProject project)
        {
            int col = 1;
            sheet.Cell(1, col++).Value = "SN";
            sheet.Cell(1, col++).Value = "工单号";
            sheet.Cell(1, col++).Value = "员工工号";
            sheet.Cell(1, col++).Value = "测试时间";
            sheet.Cell(1, col++).Value = "最终结果";

            foreach (var item in project.Items)
            {
                string colName = $"{item.Name}_({item.HighLimit}_{item.LowLimit})";
                sheet.Cell(1, col++).Value = colName;
            }

            // Bold all headers in first row
            for (int i = 1; i < col; i++)
                sheet.Cell(1, i).Style.Font.Bold = true;
        }

        private static string GetStateText(TestState state)
        {
            switch (state)
            {
                case TestState.Pass: return "PASS";
                case TestState.Fail: return "FAIL";
                case TestState.Skipped: return "SKIP";
                case TestState.Running: return "RUNNING";
                default: return "---";
            }
        }

        private static string GetOverallResult(TestProject project)
        {
            if (project.Items.Any(x => x.State == TestState.Fail)) return "FAIL";
            if (project.Items.All(x => x.State == TestState.Pass || x.State == TestState.Skipped)) return "PASS";
            return "PARTIAL";
        }

        public static void SaveToExcel(TestProject project, string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var sheet = workbook.Worksheets.Add("测试用例");

                sheet.Cell(1, 1).Value = "序号";
                sheet.Cell(1, 2).Value = "测试项目";
                sheet.Cell(1, 3).Value = "下限";
                sheet.Cell(1, 4).Value = "上限";
                sheet.Cell(1, 5).Value = "跳过(0/1)";
                sheet.Cell(1, 6).Value = "重试次数";
                sheet.Cell(1, 7).Value = "函数名";
                sheet.Cell(1, 8).Value = "参数";

                for (int i = 0; i < project.Items.Count; i++)
                {
                    var item = project.Items[i];
                    int row = i + 2;
                    sheet.Cell(row, 1).Value = item.Id;
                    sheet.Cell(row, 2).Value = item.Name;
                    sheet.Cell(row, 3).Value = item.LowLimit;
                    sheet.Cell(row, 4).Value = item.HighLimit;
                    sheet.Cell(row, 5).Value = item.Skip;
                    sheet.Cell(row, 6).Value = item.RetryCount;
                    sheet.Cell(row, 7).Value = item.FunctionName;
                    sheet.Cell(row, 8).Value = item.Parameter;
                }

                workbook.SaveAs(filePath);
            }
        }
    }
}
