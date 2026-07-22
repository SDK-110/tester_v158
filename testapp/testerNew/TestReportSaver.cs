using OfficeOpenXml;
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
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
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

              //  ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("测试报告");

                    sheet.Cells[1, 1].Value = "产品序列号(SN)";
                    sheet.Cells[1, 2].Value = sn ?? "NA";
                    sheet.Cells[2, 1].Value = "工单号";
                    sheet.Cells[2, 2].Value = orderNo ?? "";
                    sheet.Cells[3, 1].Value = "员工工号";
                    sheet.Cells[3, 2].Value = operatorNo ?? "";
                    sheet.Cells[4, 1].Value = "测试时间";
                    sheet.Cells[4, 2].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sheet.Cells[5, 1].Value = "测试结果";
                    sheet.Cells[5, 2].Value = GetOverallResult(project);

                    for (int i = 0; i < Headers.Length; i++)
                    {
                        sheet.Cells[7, i + 1].Value = Headers[i];
                    }

                    for (int i = 0; i < project.Items.Count; i++)
                    {
                        var item = project.Items[i];
                        int row = i + 8;
                        sheet.Cells[row, 1].Value = item.Id;
                        sheet.Cells[row, 2].Value = item.Name;
                        sheet.Cells[row, 3].Value = item.LowLimit;
                        sheet.Cells[row, 4].Value = item.HighLimit;
                        sheet.Cells[row, 5].Value = item.MeasuredValue;
                        sheet.Cells[row, 6].Value = item.State == TestState.Pending ? "未执行" : item.MeasuredValue;
                        sheet.Cells[row, 7].Value = item.Duration;
                    }

                    sheet.Cells[7, 1, 7, Headers.Length].Style.Font.Bold = true;
                    sheet.Cells.AutoFitColumns();

                    package.SaveAs(new FileInfo(filePath));
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

               // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                bool fileExists = File.Exists(filePath);
                ExcelPackage package;
                ExcelWorksheet sheet;

                if (fileExists)
                {
                    byte[] fileBytes = File.ReadAllBytes(filePath);
                    using (var ms = new MemoryStream(fileBytes))
                    {
                        package = new ExcelPackage(ms);
                    }
                    sheet = package.Workbook.Worksheets[1];
                }
                else
                {
                    package = new ExcelPackage();
                    sheet = package.Workbook.Worksheets.Add("测试记录");
                    CreateAppendHeaders(sheet, project);
                }

                int lastRow = sheet.Dimension?.Rows ?? 1;
                int newRow = lastRow + 1;

                int col = 1;
                sheet.Cells[newRow, col++].Value = sn ?? "NA";
                sheet.Cells[newRow, col++].Value = orderNo ?? "";
                sheet.Cells[newRow, col++].Value = operatorNo ?? "";
                sheet.Cells[newRow, col++].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sheet.Cells[newRow, col++].Value = GetOverallResult(project);

                foreach (var item in project.Items)
                {
                    string colName = $"{item.Name}_({item.HighLimit}_{item.LowLimit})";
                    int headerCol = GetHeaderColumn(sheet, colName);
                    if (headerCol > 0)
                    {
                        if (item.State == TestState.Pending)
                            sheet.Cells[newRow, headerCol].Value = "未执行";
                        else
                            sheet.Cells[newRow, headerCol].Value = item.MeasuredValue;
                    }
                }

                sheet.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(filePath));
            }
            catch (Exception ex)
            {
                throw new Exception($"保存追加记录失败: {ex.Message}", ex);
            }
        }

        private static int GetHeaderColumn(ExcelWorksheet sheet, string headerName)
        {
            int colCount = sheet.Dimension?.Columns ?? 0;
            for (int col = 1; col <= colCount; col++)
            {
                var cellValue = sheet.Cells[1, col].Value?.ToString();
                if (cellValue == headerName)
                    return col;
            }
            return -1;
        }

        private static void CreateAppendHeaders(ExcelWorksheet sheet, TestProject project)
        {
            int col = 1;
            sheet.Cells[1, col++].Value = "SN";
            sheet.Cells[1, col++].Value = "工单号";
            sheet.Cells[1, col++].Value = "员工工号";
            sheet.Cells[1, col++].Value = "测试时间";
            sheet.Cells[1, col++].Value = "最终结果";

            foreach (var item in project.Items)
            {
                string colName = $"{item.Name}_({item.HighLimit}_{item.LowLimit})";
                sheet.Cells[1, col++].Value = colName;
            }

            sheet.Cells[1, 1, 1, col].Style.Font.Bold = true;
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
          //  ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("测试用例");

                sheet.Cells[1, 1].Value = "序号";
                sheet.Cells[1, 2].Value = "测试项目";
                sheet.Cells[1, 3].Value = "下限";
                sheet.Cells[1, 4].Value = "上限";
                sheet.Cells[1, 5].Value = "跳过(0/1)";
                sheet.Cells[1, 6].Value = "重试次数";
                sheet.Cells[1, 7].Value = "函数名";
                sheet.Cells[1, 8].Value = "参数";

                for (int i = 0; i < project.Items.Count; i++)
                {
                    var item = project.Items[i];
                    int row = i + 2;
                    sheet.Cells[row, 1].Value = item.Id;
                    sheet.Cells[row, 2].Value = item.Name;
                    sheet.Cells[row, 3].Value = item.LowLimit;
                    sheet.Cells[row, 4].Value = item.HighLimit;
                    sheet.Cells[row, 5].Value = item.Skip;
                    sheet.Cells[row, 6].Value = item.RetryCount;
                    sheet.Cells[row, 7].Value = item.FunctionName;
                    sheet.Cells[row, 8].Value = item.Parameter;
                }

                package.SaveAs(new FileInfo(filePath));
            }
        }
    }
}
