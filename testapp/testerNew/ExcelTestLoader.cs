using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;

namespace test_antdui
{
    public class ExcelTestLoader
    {
        public static readonly string DefaultTemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestTemplate.xlsx");

        static ExcelTestLoader()
        {
           // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
                using (var package = new ExcelPackage())
                {
                    package.Load(stream);
                    var sheet = package.Workbook.Worksheets[1];
                    if (sheet == null) return project;

                    int row = 2;
                    int seq = 1;

                    while (true)
                    {
                        var nameCell = sheet.Cells[row, 2];
                        if (nameCell.Value == null || string.IsNullOrWhiteSpace(nameCell.Value.ToString()))
                            break;

                        var item = new TestItem
                        {
                            Id = int.TryParse(GetCellValue(sheet, row, 1), out int id) ? id : seq,
                            Name = nameCell.Value?.ToString() ?? "",
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
                sheet.Cells[1, 8].Value = "跳转编号";
                sheet.Cells[1, 9].Value = "参数";

                var sampleItems = new List<TestItem>
                {
                    new TestItem { Id = 1, Name = "电压测试 3.3V", LowLimit = "3.2", HighLimit = "3.4", FunctionName = "VoltageTest", RetryCount = "1",Jumper_Num="1", Skip = "0" },
                    new TestItem { Id = 2, Name = "电压测试 5V", LowLimit = "4.8", HighLimit = "5.2", FunctionName = "VoltageTest", RetryCount = "1",Jumper_Num="2", Skip = "0" },
                    new TestItem { Id = 3, Name = "电压测试 12V", LowLimit = "11.5", HighLimit = "12.5", FunctionName = "VoltageTest", RetryCount = "1",Jumper_Num="3", Skip = "0" },
                    new TestItem { Id = 4, Name = "电流测试", LowLimit = "0", HighLimit = "0.5", FunctionName = "CurrentTest", RetryCount = "1",Jumper_Num="4", Skip = "0" },
                    new TestItem { Id = 5, Name = "短路测试", LowLimit = "0", HighLimit = "1", FunctionName = "ShortCircuitTest", RetryCount = "1",Jumper_Num="5", Skip = "0" },
                    new TestItem { Id = 6, Name = "绝缘电阻测试", LowLimit = "10", HighLimit = "999999", FunctionName = "InsulationTest", RetryCount = "1",Jumper_Num="6", Skip = "0" },
                    new TestItem { Id = 7, Name = "开路测试", LowLimit = "0", HighLimit = "1", FunctionName = "OpenCircuitTest", RetryCount = "1",Jumper_Num="7", Skip = "0" },
                    new TestItem { Id = 8, Name = "Flash写入测试", LowLimit = "0", HighLimit = "1", FunctionName = "FlashTest", RetryCount = "3",Jumper_Num="8", Skip = "0" },
                    new TestItem { Id = 9, Name = "预留测试项", LowLimit = "0", HighLimit = "1", FunctionName = "", RetryCount = "1",Jumper_Num="9", Skip = "1" },
                };

                for (int i = 0; i < sampleItems.Count; i++)
                {
                    var item = sampleItems[i];
                    int row = i + 2;
                    sheet.Cells[row, 1].Value = item.Id;
                    sheet.Cells[row, 2].Value = item.Name;
                    sheet.Cells[row, 3].Value = item.LowLimit;
                    sheet.Cells[row, 4].Value = item.HighLimit;
                    sheet.Cells[row, 5].Value = item.Skip;
                    sheet.Cells[row, 6].Value = item.RetryCount;
                    sheet.Cells[row, 7].Value = item.FunctionName;
                    sheet.Cells[row, 8].Value = item.Jumper_Num;
                    sheet.Cells[row, 9].Value = item.Parameter;
                }

                var dir = Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                package.SaveAs(new FileInfo(path));
            }
        }

        public static void SaveToExcel(TestProject project, string filePath)
        {
            try
            {
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
                    sheet.Cells[1, 8].Value = "跳转编号";
                    sheet.Cells[1, 9].Value = "参数";

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
                        sheet.Cells[row, 8].Value = item.Jumper_Num;
                        sheet.Cells[row, 9].Value = item.Parameter;
                    }

                    package.SaveAs(new FileInfo(filePath));
                    LoadFromExcel(filePath);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"保存Excel失败: {ex.Message}", ex);
            }
        }

        private static string GetCellValue(ExcelWorksheet sheet, int row, int col, string defaultValue = "")
        {
            var cell = sheet.Cells[row, col];
            return cell.Value?.ToString() ?? defaultValue;
        }
    }
}
