using rebuild.testcase_loader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp;
using 重构程序.testcase_loader;
using unvell.ReoGrid;
using System.Diagnostics.Eventing.Reader;

namespace 重构程序.viewmode
{



    public class p_reogridviewloader
    {
        public Dictionary<string, pointfun> testcase_lib;
        public tester_project tester_proj;
        public Worksheet myworksheet1;
        public ReoGridControl reftb;
        public p_reogridviewloader(ref ReoGridControl workbook, Dictionary<string, pointfun> testcase_lib, tester_project tester_proj)
        {
            this.testcase_lib = testcase_lib;    
            this.tester_proj = tester_proj;
           
            reftb = workbook;
            myworksheet1 = workbook.CurrentWorksheet;
           
            set_dt_headr_name(new string[] { "ID", "Test_Case_Description", "High_Limit", "LOW_Limit", "DUT1_Result", "DUT2_Result", "DUT3_Result", "DUT4_Result", "Test_Judge", "Test_Time" });
        }
        public  Worksheet get_and_init_tb()
        {
      
            return myworksheet1;
        }

        private void set_dt_headr_name(string[] header_names)
        {


           
              
                myworksheet1.Columns = 10;
                myworksheet1.Rows = 1000;
                myworksheet1.SetSettings(WorksheetSettings.View_ShowColumnHeader, true);
                myworksheet1.SetSettings(WorksheetSettings.View_ShowRowHeader, false);

                // myworksheet1.Cells[0, 0].Style.BackColor = Color.Red;
                myworksheet1.ColumnHeaders[0].Text = header_names[0];
                myworksheet1.ColumnHeaders[0].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[1].Text = header_names[1];
                myworksheet1.ColumnHeaders[1].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[2].Text = header_names[2];
                myworksheet1.ColumnHeaders[2].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[3].Text = header_names[3];
                myworksheet1.ColumnHeaders[3].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[4].Text = header_names[4];
                myworksheet1.ColumnHeaders[4].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[5].Text = header_names[5];
                myworksheet1.ColumnHeaders[5].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[6].Text = header_names[6];
                myworksheet1.ColumnHeaders[6].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[7].Text = header_names[7];
                myworksheet1.ColumnHeaders[7].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[8].Text = header_names[8];
                myworksheet1.ColumnHeaders[8].Style.HorizontalAlign = ReoGridHorAlign.Left;
                myworksheet1.ColumnHeaders[9].Text = header_names[9];
                myworksheet1.ColumnHeaders[9].Style.HorizontalAlign = ReoGridHorAlign.Left;

            myworksheet1.ColumnHeaders[0].Width = (ushort)(reftb.Width * 0.05);
            myworksheet1.ColumnHeaders[1].Width = (ushort)(reftb.Width * 0.255);
            myworksheet1.ColumnHeaders[2].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[3].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[4].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[5].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[6].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[7].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[8].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[9].Width = (ushort)(reftb.Width * 0.1);

            reftb.Readonly = true;


           




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
       

        }

        public void view_update() {



            myworksheet1.ColumnHeaders[0].Width = (ushort)(reftb.Width * 0.05);
            myworksheet1.ColumnHeaders[1].Width = (ushort)(reftb.Width * 0.255);
            myworksheet1.ColumnHeaders[2].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[3].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[4].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[5].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[6].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[7].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[8].Width = (ushort)(reftb.Width * 0.085);
            myworksheet1.ColumnHeaders[9].Width = (ushort)(reftb.Width * 0.1);








        }

        public  void table_load_into_viewer(int updateflog=1)
        {

            //  viewloader.reftb.DataSource = viewloader.dt ;

            tester_proj.clear_result();

            if (updateflog == 3) {

                for (int i = tester_proj.test_cases.Count; i < myworksheet1.RowCount; i++) {

                    if (myworksheet1.Cells[i, 1].Data ==null) break;
                    myworksheet1[$"A{i + 1}:I{i + 1}"] = new string[] { "", "", "", "", "", "", "","","" };

                }
            }
            for (int i = 0; i < tester_proj.test_cases.Count; i++)
            {
                myworksheet1.Cells[i, 4].Style.TextColor = Color.Black;
                myworksheet1.Cells[i, 5].Style.TextColor = Color.Black;
                myworksheet1.Cells[i, 6].Style.TextColor = Color.Black;
                myworksheet1.Cells[i, 7].Style.TextColor = Color.Black;
                myworksheet1.Cells[i, 8].Style.TextColor = Color.Black;

                if (tester_proj.test_cases[i].result_msg.Split(',').Length != 4)
                {


                    myworksheet1[$"A{i + 1}:I{i + 1}"] = new string[] { tester_proj.test_cases[i].id.ToString(),
                                                                    tester_proj.test_cases[i].testcase_description,
                                                                    tester_proj.test_cases[i].testcase_high_limit,
                                                                    tester_proj.test_cases[i].testcase_low_limit,
                                                                    tester_proj.test_cases[i].result_msg,//.Split(',')[0],
                                                                    tester_proj.test_cases[i].result_msg,//.Split(',')[1],
                                                                    tester_proj.test_cases[i].result_msg,//.Split(',')[2],
                                                                    tester_proj.test_cases[i].result_msg,//.Split(',')[3],
                                                                    tester_proj.test_cases[i].get_judge_result,
                                                                    tester_proj.test_cases[i].runtime.ToString(),
                };

                }
                else {
                    myworksheet1[$"A{i + 1}:I{i + 1}"] = new string[] { tester_proj.test_cases[i].id.ToString(),
                                                                    tester_proj.test_cases[i].testcase_description,
                                                                    tester_proj.test_cases[i].testcase_high_limit,
                                                                    tester_proj.test_cases[i].testcase_low_limit,
                                                                    tester_proj.test_cases[i].result_msg.Split(',')[0],
                                                                    tester_proj.test_cases[i].result_msg.Split(',')[1],
                                                                    tester_proj.test_cases[i].result_msg.Split(',')[2],
                                                                    tester_proj.test_cases[i].result_msg.Split(',')[3],
                                                                    tester_proj.test_cases[i].get_judge_result,
                                                                    tester_proj.test_cases[i].runtime.ToString(),
                };

                }
              




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
