using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniExcelLibs;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Sparkline;
namespace testapp.mylib
{
    class excel_crash
    {


        public excel_crash()
        {




            ExcelWorksheet workSheet;

            using (var p = new ExcelPackage(new FileInfo("d:/test.xlsx")))
            {
                workSheet = p.Workbook.Worksheets["测试数据"];

                int dectect_position=0;
                for (int i = 1; i < workSheet.Cells.Rows; i++) {
                    if (workSheet.Cells[i, 1].Value == null) { dectect_position=i;break; }

                }
            
                workSheet.Cells[dectect_position, 1].Value = 11;
                workSheet.Cells[dectect_position, 3].Value = 22;

                p.Save();
            }
                //   }

                //var p = new List<Test>();
                //for (int i = 0; i < 1000000; i++)
                //{

                //    Test a = new Test() { Id = i, testcase_high_limit = "" + i, test_descriptione = i.ToString() };
                //    p.Add(a);
                //}

                //var t = MiniExcel.GetReader("./project_tester_name.dll", startCell: "A4");
                //Test[] gg = new Test[] { };


                //MiniExcel.SaveAs("d:/fsdfsd.xlsx", GetExcelList(p)); ;

                //var m = readtable_from_excel();

        }

        /// <summary>
        /// miniEXCEL LIB 调用
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<Test> readtable_from_excel(string path = "d:/fsdfsd.xlsx")
        {

          return  MiniExcel.Query<Test>(path).ToList();

        }
        /// <summary>
        /// miniEXCEL 调用
        /// </summary>
        public void write_object2xls() {



            var p = new List<Test>();
            for (int i = 0; i < 1000000; i++)
            {

                Test a = new Test() { Id = i, testcase_high_limit = "" + i, test_descriptione = i.ToString() };
                p.Add(a);
            }

            MiniExcel.SaveAs("d:/fsdfsd.xlsx", GetExcelList(p)); 

          
        


        }

        public void epplus_xls_save() {



            int[] testData = { 1, 2, 3 };

            ExcelWorksheet workSheet;

            using (var p = new ExcelPackage())
            {
                workSheet = p.Workbook.Worksheets.Add("测试数据");

                workSheet.Cells[1, 1].Value = "数据1";
                workSheet.Cells[1, 2].Value = "数据2";
                workSheet.Cells[1, 3].Value = "数据3";
                for (int i = 2; i < 1000000; i++)
                {
                    workSheet.Cells[i, 1].Value = testData[0];
                    workSheet.Cells[i, 3].Value = testData[2];
                }
                p.SaveAs(new FileInfo("d:/test.xlsx"));


            }




            }


        ~excel_crash() {




        }


        private IEnumerable<Dictionary<string, object>> GetExcelList(List<Test> test)
        {
            foreach (var item in test)
            {
                var newCompanyPrepareds = new Dictionary<string, object>();
                newCompanyPrepareds.Add("Id", item.Id);
                newCompanyPrepareds.Add("test_descriptione", item.test_descriptione);
                newCompanyPrepareds.Add("testcase_high_limit", item.testcase_high_limit);
                //newCompanyPrepareds.Add("testcase_low_limit", item.testcase_low_limit);
                //newCompanyPrepareds.Add("test_spik_if", item.test_spik_if);
                //newCompanyPrepareds.Add("repeat_goto", item.repeat_goto);
                //newCompanyPrepareds.Add("test_lib_string", item.test_lib_string);
                //newCompanyPrepareds.Add("test_loop_count", item.test_loop_count);
                //newCompanyPrepareds.Add("parameter", item.parameter);

                yield return newCompanyPrepareds;
            }
        }
    }




    public class Test
    {
        public int Id { get; set; }
        public string test_descriptione { get; set; }
        public string testcase_high_limit { get; set; }
        //public string testcase_low_limit { get; set; }
        //public string test_spik_if{ get; set; }
        //public string repeat_goto        { get; set; }
        //public string test_lib_string { get; set; }
        //public string test_loop_count { get; set; }
        //public string parameter { get; set; }

    }
}
