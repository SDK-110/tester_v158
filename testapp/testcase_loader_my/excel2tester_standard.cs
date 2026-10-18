using System;
using System.Collections.Generic;
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

            try
            {
                // 与 testerNew 共用流式读取：只取 A-I 列，遇到空行即停。
                // 不再整表加载（工作表范围常被撑到几千行×100 列）。
                var rows = test_antdui.ExcelTestLoader.ReadStepRows(project_tester_name);
                if (!rows.ContainsKey(5)) return null;

                sav.project = Cell(rows, 1, 2);
                sav.project_tester_name = Cell(rows, 2, 2);

                for (int row = 5; ; row++)
                {
                    string id = Cell(rows, row, 1);
                    if (string.IsNullOrEmpty(id)) break;

                    string test_description = Cell(rows, row, 2);
                    string high_limit       = Cell(rows, row, 3);
                    string low_limit        = Cell(rows, row, 4);
                    string spik             = Cell(rows, row, 5);
                    string repeat           = Cell(rows, row, 6);
                    string lib              = Cell(rows, row, 7);
                    string loop             = Cell(rows, row, 8);
                    string param            = Cell(rows, row, 9);

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
        }

        private static string Cell(Dictionary<int, string[]> rows, int row, int col)
            => rows.TryGetValue(row, out var cells) ? (cells[col] ?? "") : "";

        private static void Workbook_WorkbookLoaded(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("Test");
        }
    }
}
