using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace test_antdui
{
    public class ExcelTestLoader
    {
        private static readonly XNamespace Sml = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        public static string DefaultTemplatePath
        {
            get
            {
                // 优先当前工程；没有则退回固定默认文件。
                // 原实现取 ProjectLoader.GetProjectNames()[0] —— 旧版遗留源，新版从不更新它。
                try
                {
                    var current = TestConfigManager.CurrentProjectPath;
                    if (!string.IsNullOrEmpty(current) && File.Exists(current))
                        return current;
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
                // 直接流式读取 xlsx（zip+xml），只取前 9 列、读到第一个空行即停。
                // 不用 ClosedXML 读：它会把整张表（例如 3318 行 × 100 列）全部实例化，耗时 3~5 秒。
                var rows = ReadStepRows(filePath);

                // Detect sproj format: Row 1 Col A contains "project_name"
                bool isSproj = Cell(rows, 1, 1).Contains("project_name");

                if (isSproj)
                {
                    // sproj format: read project name from B1
                    string projName = Cell(rows, 1, 2);
                    if (!string.IsNullOrWhiteSpace(projName))
                        project.ProjectName = projName;

                    int seq = 1;
                    for (int row = 5; ; row++, seq++)
                    {
                        string idStr = Cell(rows, row, 1);
                        if (string.IsNullOrWhiteSpace(idStr))
                            break; // Stop at first empty row (same as old engine: while Data!=null)

                        string spikVal = Cell(rows, row, 5, "0");

                        project.Items.Add(new TestItem
                        {
                            Id = int.TryParse(idStr, out int id) ? id : seq,
                            Name = Cell(rows, row, 2),
                            HighLimit = Cell(rows, row, 3),  // C = testcase_high_limit (swapped)
                            LowLimit = Cell(rows, row, 4),   // D = testcase_low_limit (swapped)
                            Skip = (spikVal == "0") ? "1" : "0",      // Reverse: spik "0"→skip "1", spik "1"→skip "0"
                            RetryCount = Cell(rows, row, 6, "1"),
                            FunctionName = Cell(rows, row, 7),
                            Jumper_Num = Cell(rows, row, 8),
                            Parameter = Cell(rows, row, 9)
                        });
                    }
                }
                else
                {
                    // Normal xlsx format: Row 1 = header, Row 2+ = data
                    int seq = 1;
                    for (int row = 2; ; row++, seq++)
                    {
                        string name = Cell(rows, row, 2);
                        if (string.IsNullOrWhiteSpace(name))
                            break;

                        project.Items.Add(new TestItem
                        {
                            Id = int.TryParse(Cell(rows, row, 1), out int id) ? id : seq,
                            Name = name,
                            LowLimit = Cell(rows, row, 3),
                            HighLimit = Cell(rows, row, 4),
                            Skip = Cell(rows, row, 5, "0"),
                            RetryCount = Cell(rows, row, 6, "1"),
                            FunctionName = Cell(rows, row, 7),
                            Jumper_Num = Cell(rows, row, 8),
                            Parameter = Cell(rows, row, 9)
                        });
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

        /// <summary>
        /// 读取第一张工作表：只保留 A..I 列，遇到数据区末尾即停止。
        /// 返回 行号 -> 列值(下标 1..9)，供 Main_f 等其它加载器共用。
        /// </summary>
        public static Dictionary<int, string[]> ReadStepRows(string filePath)
        {
            var rows = new Dictionary<int, string[]>();
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var zip = new ZipArchive(fs, ZipArchiveMode.Read);
            var shared = ReadSharedStrings(zip);
            using var sheet = GetFirstSheet(zip);
            if (sheet == null) return rows;

            using var reader = XmlReader.Create(sheet, new XmlReaderSettings { IgnoreWhitespace = true });
            reader.MoveToContent();

            bool? isSproj = null;
            int rowNum = 0;
            while (!reader.EOF)
            {
                // 只有读到行首才解析；XNode.ReadFrom 之后 reader 已在下一节点，不能再多读一次
                if (reader.NodeType == XmlNodeType.Element
                    && reader.LocalName == "row"
                    && reader.NamespaceURI == Sml.NamespaceName)
                {
                    var row = (XElement)XNode.ReadFrom(reader);
                    rowNum = int.TryParse((string)row.Attribute("r"), out int n) ? n : rowNum + 1;

                    var cells = new string[10];   // 下标 1..9
                    foreach (var c in row.Elements(Sml + "c"))
                    {
                        int col = ColumnIndex((string)c.Attribute("r"));
                        if (col >= 1 && col <= 9)
                            cells[col] = CellValue(c, shared);
                    }
                    rows[rowNum] = cells;

                    isSproj ??= (cells[1] ?? "").Contains("project_name");
                    bool done = isSproj.Value
                        ? rowNum >= 5 && string.IsNullOrWhiteSpace(cells[1])
                        : rowNum >= 2 && string.IsNullOrWhiteSpace(cells[2]);
                    if (done) break;
                    continue;
                }
                reader.Read();
            }
            return rows;
        }

        private static string Cell(Dictionary<int, string[]> rows, int row, int col, string defaultValue = "")
            => rows.TryGetValue(row, out var cells) ? (cells[col] ?? defaultValue) : defaultValue;

        private static string CellValue(XElement cell, List<string> shared)
        {
            string type = (string)cell.Attribute("t");
            if (type == "inlineStr")
                return string.Concat(cell.Descendants(Sml + "t").Select(t => t.Value));

            var v = cell.Element(Sml + "v");
            if (v == null) return "";
            string raw = v.Value;
            if (type == "s")
                return int.TryParse(raw, out int idx) && idx >= 0 && idx < shared.Count ? shared[idx] : "";
            return raw;
        }

        private static List<string> ReadSharedStrings(ZipArchive zip)
        {
            var list = new List<string>();
            var entry = zip.GetEntry("xl/sharedStrings.xml");
            if (entry == null) return list;

            using var stream = entry.Open();
            var doc = XDocument.Load(stream);
            foreach (var si in doc.Root.Elements(Sml + "si"))
            {
                var sb = new StringBuilder();
                foreach (var t in si.Descendants(Sml + "t"))
                {
                    if (t.Parent?.Name == Sml + "rPh") continue;   // 注音不是单元格内容
                    sb.Append(t.Value);
                }
                list.Add(sb.ToString());
            }
            return list;
        }

        /// <summary>按 workbook.xml + rels 找到第一张工作表的 xml 流。</summary>
        private static Stream GetFirstSheet(ZipArchive zip)
        {
            string target = null;
            var wbEntry = zip.GetEntry("xl/workbook.xml");
            var relsEntry = zip.GetEntry("xl/_rels/workbook.xml.rels");
            if (wbEntry != null && relsEntry != null)
            {
                XNamespace rel = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";
                XNamespace pkg = "http://schemas.openxmlformats.org/package/2006/relationships";
                XDocument wb, rels;
                using (var s = wbEntry.Open()) wb = XDocument.Load(s);
                using (var s = relsEntry.Open()) rels = XDocument.Load(s);

                var sheet = wb.Root?.Element(Sml + "sheets")?.Elements(Sml + "sheet").FirstOrDefault();
                string id = (string)sheet?.Attribute(rel + "id");
                string path = (string)rels.Root?.Elements(pkg + "Relationship")
                    .FirstOrDefault(e => (string)e.Attribute("Id") == id)?.Attribute("Target");
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.TrimStart('/');
                    target = path.StartsWith("xl/") ? path : "xl/" + path;
                }
            }

            var entry = target != null ? zip.GetEntry(target) : null;
            entry ??= zip.GetEntry("xl/worksheets/sheet1.xml");
            entry ??= zip.Entries.FirstOrDefault(e =>
                e.FullName.StartsWith("xl/worksheets/") && e.FullName.EndsWith(".xml"));
            return entry?.Open();
        }

        /// <summary>"B12" → 2。</summary>
        private static int ColumnIndex(string cellRef)
        {
            int col = 0;
            if (cellRef == null) return 0;
            foreach (char ch in cellRef)
            {
                if (ch >= 'A' && ch <= 'Z') col = col * 26 + (ch - 'A' + 1);
                else if (ch >= 'a' && ch <= 'z') col = col * 26 + (ch - 'a' + 1);
                else break;
            }
            return col;
        }
    }
}
