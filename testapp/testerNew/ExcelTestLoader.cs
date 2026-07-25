using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;

namespace test_antdui
{
    public class ExcelTestLoader
    {
        public static string DefaultTemplatePath
        {
            get
            {
                try
                {
                    var names = SprojFileBrowser.ProjectLoader.Instance.GetProjectNames();
                    if (names != null && names.Count > 0 && File.Exists(names[0]))
                        return names[0];
                }
                catch { }
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TH2818TEST.sproj");
            }
        }

        static ExcelTestLoader()
        {
            // 已迁移到 ClosedXML，避免 EPPlusFree(.NET 4.0 DLL) 在 .NET 8 下触发 ConfigurationManager 初始化失败
        }

        public static TestProject LoadFromExcel(string filePath)
        {
            if (!File.Exists(filePath))
            {
                CreateSampleTemplate(filePath);
            }

            var project = new TestProject
            {
                ProjectName = Path.GetFileNameWithoutExtension(filePath)
            };

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var workbook = new XLWorkbook(stream))
                {
                    var sheet = workbook.Worksheet(1);
                    if (sheet == null) return project;

                    // Detect sproj format: Row 1 Col A contains "project_name"
                    string cellA1 = GetCellValue(sheet, 1, 1);
                    bool isSproj = cellA1 != null && cellA1.Contains("project_name");

                    if (isSproj)
                    {
                        // sproj format: read project name from B1
                        string projName = GetCellValue(sheet, 1, 2);
                        if (!string.IsNullOrWhiteSpace(projName))
                            project.ProjectName = projName;

                        int row = 5;
                        int seq = 1;
                        int maxRow = sheet.LastRowUsed()?.RowNumber() ?? 1;

                        while (row <= maxRow)
                        {
                            string idStr = GetCellValue(sheet, row, 1);
                            if (string.IsNullOrWhiteSpace(idStr))
                            {
                                break; // Stop at first empty row (same as old engine: while Data!=null)
                            }

                            string spikVal = GetCellValue(sheet, row, 5, "0");

                            var item = new TestItem
                            {
                                Id = int.TryParse(idStr, out int id) ? id : seq,
                                Name = GetCellValue(sheet, row, 2),
                                HighLimit = GetCellValue(sheet, row, 3),  // C = testcase_high_limit (swapped)
                                LowLimit = GetCellValue(sheet, row, 4),   // D = testcase_low_limit (swapped)
                                Skip = (spikVal == "0") ? "1" : "0",      // Reverse: spik "0"→skip "1", spik "1"→skip "0"
                                RetryCount = GetCellValue(sheet, row, 6, "1"),
                                FunctionName = GetCellValue(sheet, row, 7),
                                Jumper_Num = GetCellValue(sheet, row, 8),
                                Parameter = GetCellValue(sheet, row, 9)
                            };

                            project.Items.Add(item);
                            row++;
                            seq++;
                        }
                    }
                    else
                    {
                        // Normal xlsx format: Row 1 = header, Row 2+ = data
                        int row = 2;
                        int seq = 1;

                        while (true)
                        {
                            string name = GetCellValue(sheet, row, 2);
                            if (string.IsNullOrWhiteSpace(name))
                                break;

                            var item = new TestItem
                            {
                                Id = int.TryParse(GetCellValue(sheet, row, 1), out int id) ? id : seq,
                                Name = name,
                                LowLimit = GetCellValue(sheet, row, 3),
                                HighLimit = GetCellValue(sheet, row, 4),
                                Skip = GetCellValue(sheet, row, 5, "0"),
                                RetryCount = GetCellValue(sheet, row, 6, "1"),
                                FunctionName = GetCellValue(sheet, row, 7),
                                Jumper_Num = GetCellValue(sheet, row, 8),
                                Parameter = GetCellValue(sheet, row, 9)
                            };

                            project.Items.Add(item);
                            row++;
                            seq++;
                        }
                    }
                }

                TestConfigManager.UpdateLastExcelPath(filePath);
                TestConfigManager.UpdateProjectName(project.ProjectName);
            }
            catch (Exception ex)
            {
                throw new Exception($"加载Excel失败: {ex.Message}", ex);
            }

            return project;
        }

        public static void CreateSampleTemplate(string filePath = null)
        {
            string path = filePath ?? DefaultTemplatePath;

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
                sheet.Cell(1, 8).Value = "跳转编号";
                sheet.Cell(1, 9).Value = "参数";

                var sampleItems = new List<TestItem>
                {
                    new TestItem { Id = 1, Name = "电压测试 3.3V", LowLimit = "3.2", HighLimit = "3.4", FunctionName = "VoltageTest", RetryCount = "1", Jumper_Num = "1", Skip = "0" },
                    new TestItem { Id = 2, Name = "电压测试 5V", LowLimit = "4.8", HighLimit = "5.2", FunctionName = "VoltageTest", RetryCount = "1", Jumper_Num = "2", Skip = "0" },
                    new TestItem { Id = 3, Name = "电压测试 12V", LowLimit = "11.5", HighLimit = "12.5", FunctionName = "VoltageTest", RetryCount = "1", Jumper_Num = "3", Skip = "0" },
                    new TestItem { Id = 4, Name = "电流测试", LowLimit = "0", HighLimit = "0.5", FunctionName = "CurrentTest", RetryCount = "1", Jumper_Num = "4", Skip = "0" },
                    new TestItem { Id = 5, Name = "短路测试", LowLimit = "0", HighLimit = "1", FunctionName = "ShortCircuitTest", RetryCount = "1", Jumper_Num = "5", Skip = "0" },
                    new TestItem { Id = 6, Name = "绝缘电阻测试", LowLimit = "10", HighLimit = "999999", FunctionName = "InsulationTest", RetryCount = "1", Jumper_Num = "6", Skip = "0" },
                    new TestItem { Id = 7, Name = "开路测试", LowLimit = "0", HighLimit = "1", FunctionName = "OpenCircuitTest", RetryCount = "1", Jumper_Num = "7", Skip = "0" },
                    new TestItem { Id = 8, Name = "Flash写入测试", LowLimit = "0", HighLimit = "1", FunctionName = "FlashTest", RetryCount = "3", Jumper_Num = "8", Skip = "0" },
                    new TestItem { Id = 9, Name = "预留测试项", LowLimit = "0", HighLimit = "1", FunctionName = "", RetryCount = "1", Jumper_Num = "9", Skip = "1" },
                };

                for (int i = 0; i < sampleItems.Count; i++)
                {
                    var item = sampleItems[i];
                    int row = i + 2;
                    sheet.Cell(row, 1).Value = item.Id;
                    sheet.Cell(row, 2).Value = item.Name;
                    sheet.Cell(row, 3).Value = item.LowLimit;
                    sheet.Cell(row, 4).Value = item.HighLimit;
                    sheet.Cell(row, 5).Value = item.Skip;
                    sheet.Cell(row, 6).Value = item.RetryCount;
                    sheet.Cell(row, 7).Value = item.FunctionName;
                    sheet.Cell(row, 8).Value = item.Jumper_Num;
                    sheet.Cell(row, 9).Value = item.Parameter;
                }

                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                workbook.SaveAs(path);
            }
        }

        public static void SaveToExcel(TestProject project, string filePath)
        {
            try
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
                    sheet.Cell(1, 8).Value = "跳转编号";
                    sheet.Cell(1, 9).Value = "参数";

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
                        sheet.Cell(row, 8).Value = item.Jumper_Num;
                        sheet.Cell(row, 9).Value = item.Parameter;
                    }

                    workbook.SaveAs(filePath);
                    LoadFromExcel(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存Excel失败: {ex.Message}", ex);
            }
        }

        private static string GetCellValue(IXLWorksheet sheet, int row, int col, string defaultValue = "")
        {
            var cell = sheet.Cell(row, col);
            return cell.IsEmpty() ? defaultValue : cell.GetValue<string>() ?? defaultValue;
        }
    }
}
