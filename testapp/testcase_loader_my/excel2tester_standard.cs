using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MiniExcelLibs;
using unvell.ReoGrid;
using unvell.ReoGrid.IO;
using 重构程序.testcase_loader;

namespace rebuild.testcase_loader
{


    class excel2tester_standard
    {
      static   IWorkbook workbook;
      static Worksheet mtt = null;
        public  excel2tester_standard()
        {
          
          //  workbook = ReoGridControl.CreateMemoryWorkbook();
          //  mtt = workbook.GetWorksheetByName("sheet1");
          //  workbook.InsertWorksheet(1, mtt);
        //    workbook.WorksheetInserted += Workbook_install;
             

        }

        private void Workbook_install(object sender, EventArgs e)
        {

            System.Windows.Forms.MessageBox.Show("Test");

        }

        public  void save_excel_test_cases(ref tester_project sav, string project_tester_name = "project_tester_name.sproj")
        {
            if (workbook == null)
            {
                workbook = ReoGridControl.CreateMemoryWorkbook();
                mtt = workbook.GetWorksheetByName("sheet1");
            }
            else {

                workbook = ReoGridControl.CreateMemoryWorkbook();
                workbook.Load($"{project_tester_name}",FileFormat.Excel2007);
                mtt = workbook.GetWorksheetByName("sheet1");

            }
           
            mtt[0, 0] = "project_name:";
            mtt[0, 1] = sav.project;
            mtt[1, 0] = "tester_name:";
            mtt[1, 1] = sav.project_tester_name;
            mtt[3, 0] = "id";
            mtt[3, 1] = "test_description";
            mtt[3, 2] = "testcase_high_limit";
            mtt[3, 3] = "testcase_low_limit";
            mtt[3, 4] = "test_spik_if";
            mtt[3, 5] = "repeat_goto";
            mtt[3, 6] = "test_lib_string";
            mtt[3, 7] = "test_loop_count";
            mtt[3, 8] = "parameter";
            mtt[2, 0] = "test_case:";
            for (int i = 0; i < sav.test_cases.Count; i++)
            {
                mtt[i+4, 0] = sav.test_cases[i].id;
                mtt[i+4, 1] = sav.test_cases[i].testcase_description;
                mtt[i+4, 2] = sav.test_cases[i].testcase_high_limit;
                mtt[i+4, 3] = sav.test_cases[i].testcase_low_limit;
                mtt[i+4, 4] = sav.test_cases[i].test_spik;
                mtt[i+4, 5] = sav.test_cases[i].repeat_goto;
                mtt[i+4, 6] = sav.test_cases[i].test_lib_string;
                mtt[i + 4, 7] = sav.test_cases[i].self_run_count;
                mtt[i + 4, 8] = sav.test_cases[i].parameter;


            }




            workbook.Save(project_tester_name,FileFormat.Excel2007);



        }




        public static tester_project read_excel_test_cases(string project_tester_name = "project_tester_name.sproj", string worksheet = "sheet1")
        {
            var sav = new tester_project();
            sav.test_cases = new List<tester_standard_style>();

            // MiniExcel.Query 要求文件后缀为 .xlsx，.sproj 实质是 xlsx 格式，先复制临时文件
            string loadPath = project_tester_name;
            string tempFile = null;
            try
            {
                if (project_tester_name.EndsWith(".sproj", StringComparison.OrdinalIgnoreCase))
                {
                    tempFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
                    System.IO.File.Copy(project_tester_name, tempFile, overwrite: true);
                    loadPath = tempFile;
                }

                // MiniExcel 流式读取，不加载整个文件的 XML DOM（比 ReoGrid 快 5-10 倍）
                var rows = MiniExcel.Query(loadPath, useHeaderRow: false).ToList();
                if (rows.Count < 5) return null;

                var r0 = (IDictionary<string, object>)rows[0];
                sav.project = r0.TryGetValue("B", out var v0) ? v0?.ToString() : null;

                var r1 = (IDictionary<string, object>)rows[1];
                sav.project_tester_name = r1.TryGetValue("B", out var v1) ? v1?.ToString() : null;

                for (int i = 4; i < rows.Count; i++)
                {
                    var r = (IDictionary<string, object>)rows[i];
                    if (!r.TryGetValue("A", out var tmp) || tmp == null) break;
                    string id = tmp.ToString();
                    if (string.IsNullOrEmpty(id)) break;

                    string test_description = r.TryGetValue("B", out var c2) ? c2?.ToString() ?? "" : "";
                    string high_limit       = r.TryGetValue("C", out var c3) ? c3?.ToString() ?? "" : "";
                    string low_limit        = r.TryGetValue("D", out var c4) ? c4?.ToString() ?? "" : "";
                    string spik             = r.TryGetValue("E", out var c5) ? c5?.ToString() ?? "" : "";
                    string repeat           = r.TryGetValue("F", out var c6) ? c6?.ToString() ?? "" : "";
                    string lib              = r.TryGetValue("G", out var c7) ? c7?.ToString() ?? "" : "";
                    string loop             = r.TryGetValue("H", out var c8) ? c8?.ToString() ?? "" : "";
                    string param            = r.TryGetValue("I", out var c9) ? c9?.ToString() ?? "" : "";

                    if (string.IsNullOrEmpty(test_description) ||
                        string.IsNullOrEmpty(high_limit) ||
                        string.IsNullOrEmpty(low_limit) ||
                        string.IsNullOrEmpty(spik) ||
                        string.IsNullOrEmpty(repeat) ||
                        string.IsNullOrEmpty(lib))
                        return null;

                    sav.test_cases.Add(new tester_standard_style()
                    {
                        id = int.Parse(id),
                        testcase_description = test_description,
                        repeat_goto = repeat,
                        testcase_high_limit = high_limit,
                        testcase_low_limit = low_limit,
                        self_run_count = loop,
                        test_spik = spik,
                        test_lib_string = lib,
                        parameter = param,
                    });
                }

                return sav;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (tempFile != null && File.Exists(tempFile))
                    System.IO.File.Delete(tempFile);
            }
        }

        private static void Workbook_WorkbookLoaded(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }
    }
}
