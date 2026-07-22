using rebuild.testcase_loader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 重构程序.testcase_loader;

namespace 重构程序.viewmode
{



    public class viewloader_setting
    {

        public tester_project tester_proj;
        public DataTable dt;
        public DataGridView reftb;
        public viewloader_setting(ref DataGridView dataGrid,  tester_project tester_proj)
        {
      
            dt = dataGrid.DataSource as DataTable;
            reftb = dataGrid;
            set_dt_headr_name(new string[] { "ID", "测试描述", "上限", "下限", "屏蔽与否", "跳转标号" ,"测试用例","循环次数","用例参数"});
        }
        public  DataTable get_and_init_tb()
        {
      
            return dt;
        }

        private void set_dt_headr_name(string[] header_names)
        {

            if (dt == null)
            {


                dt = new DataTable();
                dt.Columns.Add(header_names[0], Type.GetType("System.String"));
                dt.Columns.Add(header_names[1], Type.GetType("System.String"));
                dt.Columns.Add(header_names[2], Type.GetType("System.String"));
                dt.Columns.Add(header_names[3], Type.GetType("System.String"));
                dt.Columns.Add(header_names[4], Type.GetType("System.String"));
                dt.Columns.Add(header_names[5], Type.GetType("System.String"));
                dt.Columns.Add(header_names[6], Type.GetType("System.String"));
                dt.Columns.Add(header_names[7], Type.GetType("System.String"));
                dt.Columns.Add(header_names[8], Type.GetType("System.String"));
               
            }




            //dataGridView1.Columns["Column1"].HeaderText = "序号";
            //dataGridView1.Columns["Column1"].Width = (int)(dataGridView1.Width * 0.1);
            //dataGridView1.Columns["Column2"].HeaderText = "EPC";
            //dataGridView1.Columns["Column2"].Width = (int)(dataGridView1.Width * 0.1);
            //dataGridView1.Columns["Column3"].HeaderText = "次数";
            //dataGridView1.Columns["Column3"].Width = (int)(dataGridView1.Width * 0.4);
            //dataGridView1.Columns["Column4"].HeaderText = "RSSI";
            //dataGridView1.Columns["Column4"].Width = (int)(dataGridView1.Width * 0.2);
            //dataGridView1.Columns["Column5"].HeaderText = "天线(4-1)";
            //dataGridView1.Columns["Column5"].Width = (int)(dataGridView1.Width * 0.2);
            reftb.DataSource = dt;
            reftb.Columns[header_names[0]].Width = (int)(reftb.Width * 0.05);
            reftb.Columns[header_names[1]].Width = (int)(reftb.Width * 0.25);
            reftb.Columns[header_names[2]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[3]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[4]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[5]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[6]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[5]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[6]].Width = (int)(reftb.Width * 0.1);
        }



        public  void table_load_into_viewer()
        {

            //  viewloader.reftb.DataSource = viewloader.dt ;

            tester_proj.clear_result();
            dt.Clear();
            for (int i = 0; i < tester_proj.test_cases.Count; i++)
            {

                dt.Rows.Add(new string[] {                          tester_proj.test_cases[i].id.ToString(),
                                                                    tester_proj.test_cases[i].testcase_description,
                                                                    tester_proj.test_cases[i].testcase_high_limit,
                                                                    tester_proj.test_cases[i].testcase_low_limit,
                                                                    tester_proj.test_cases[i].test_spik,
                                                                    tester_proj.test_cases[i].repeat_goto,
                                                                    tester_proj.test_cases[i].test_lib_string,
                                                                    tester_proj.test_cases[i].self_run_count,
                                                                    tester_proj.test_cases[i].parameter,
                                                                    

                });




            }

        }

    }


































    //public class viewloader
    //{
    //    public static Dictionary<string, test_run_lib> testcase_lib;
    //    public static tester_project tester_proj;
    //    public static DataTable dt;
    //    public static DataGridView reftb;
    //    public static DataTable get_and_init_tb(ref DataGridView dataGrid)
    //    {
    //        dt = dataGrid.DataSource as DataTable;
    //        reftb = dataGrid;
    //        viewloader.set_dt_headr_name(new string[] { "ID", "测试描述", "上限", "下限", "测试值", "结论" });
    //      //  dataGrid.DataSource = dt;
    //        return dt;
    //    }

    //    public static void set_dt_headr_name(string[] header_names)
    //    {

    //        if (dt == null)
    //        {


    //            dt = new DataTable();
    //            dt.Columns.Add(header_names[0], Type.GetType("System.String"));
    //            dt.Columns.Add(header_names[1], Type.GetType("System.String"));
    //            dt.Columns.Add(header_names[2], Type.GetType("System.String"));
    //            dt.Columns.Add(header_names[3], Type.GetType("System.String"));
    //            dt.Columns.Add(header_names[4], Type.GetType("System.String"));
    //            dt.Columns.Add(header_names[5], Type.GetType("System.String"));


    //        }




    //        //dataGridView1.Columns["Column1"].HeaderText = "序号";
    //        //dataGridView1.Columns["Column1"].Width = (int)(dataGridView1.Width * 0.1);
    //        //dataGridView1.Columns["Column2"].HeaderText = "EPC";
    //        //dataGridView1.Columns["Column2"].Width = (int)(dataGridView1.Width * 0.1);
    //        //dataGridView1.Columns["Column3"].HeaderText = "次数";
    //        //dataGridView1.Columns["Column3"].Width = (int)(dataGridView1.Width * 0.4);
    //        //dataGridView1.Columns["Column4"].HeaderText = "RSSI";
    //        //dataGridView1.Columns["Column4"].Width = (int)(dataGridView1.Width * 0.2);
    //        //dataGridView1.Columns["Column5"].HeaderText = "天线(4-1)";
    //        //dataGridView1.Columns["Column5"].Width = (int)(dataGridView1.Width * 0.2);
    //        reftb.DataSource = dt;
    //        reftb.Columns[header_names[0]].Width = (int)(reftb.Width * 0.05);
    //        reftb.Columns[header_names[1]].Width = (int)(reftb.Width * 0.35);
    //        reftb.Columns[header_names[2]].Width = (int)(reftb.Width * 0.1);
    //        reftb.Columns[header_names[3]].Width = (int)(reftb.Width * 0.1);
    //        reftb.Columns[header_names[4]].Width = (int)(reftb.Width * 0.2);
    //        reftb.Columns[header_names[5]].Width = (int)(reftb.Width * 0.2);


    //    }


    //    public static void run_test_case_by_id(int id) {




    //        tester_proj[id].get_rusult(ref testcase_lib);




    //    }

    //public static void  table_load_into_viewer(){

    //      //  viewloader.reftb.DataSource = viewloader.dt ;

    //    tester_proj.clear_result();
    //    viewloader.dt.Clear();
    //    for (int i = 0; i < tester_proj.test_cases.Count; i++)
    //    {

    //        viewloader.dt.Rows.Add(new string[] { tester_proj.test_cases[i].id.ToString(),
    //                                                                tester_proj.test_cases[i].testcase_description,
    //                                                                tester_proj.test_cases[i].testcase_high_limit,
    //                                                                tester_proj.test_cases[i].testcase_low_limit,
    //                                                                tester_proj.test_cases[i].result_msg,
    //                                                                tester_proj.test_cases[i].get_judge_result});




    //    }

    //    }

    //}

}
