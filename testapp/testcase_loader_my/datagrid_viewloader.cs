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



    public class datagrid_viewloader
    {
        public Dictionary<string, testapp.pointfun> testcase_lib;
        public tester_project tester_proj;
        public DataTable dt;
        public DataGridView reftb;
        public datagrid_viewloader(ref DataGridView dataGrid, Dictionary<string, testapp.pointfun> testcase_lib, tester_project tester_proj)
        {

            this.testcase_lib = testcase_lib;
            dt = dataGrid.DataSource as DataTable;
            reftb = dataGrid;
            this.tester_proj = tester_proj;
            set_dt_headr_name(new string[] { "ID", "Test_Case_Description", "High_Limit", "LOW_Limit", "Test_Result", "Test_Judge", "Test_Time" });
            reftb.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            reftb.ReadOnly = true;
            foreach (DataGridViewColumn column in reftb.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            reftb.AllowUserToResizeRows = false;
        }
        public DataTable get_and_init_tb()
        {

            return dt;
        }
        public void set_cell_back_color(int row, int col, Color setter)
        {


            reftb.Rows[row].Cells[col].Style.BackColor = setter;
        }

        public void _set_cell_front_color(int row, int col, Color setter)
        {


            reftb.Rows[row].Cells[col].Style.ForeColor = setter;
        }

        public void set_cell_value(int row, int col, string _value)
        {

            reftb.Rows[row].Cells[col].Value = _value;

        }

        public void set_sig_line()
        {

            reftb.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


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

                reftb.DataSource = dt;
            }




            //reftb.Columns["Column1"].HeaderText = "序号";
            //reftb.Columns["Column1"].Width = (int)(reftb.Width * 0.1);
            //reftb.Columns["Column2"].HeaderText = "EPC";
            //reftb.Columns["Column2"].Width = (int)(reftb.Width * 0.1);
            //reftb.Columns["Column3"].HeaderText = "次数";
            //reftb.Columns["Column3"].Width = (int)(reftb.Width * 0.4);
            //reftb.Columns["Column4"].HeaderText = "RSSI";
            //reftb.Columns["Column4"].Width = (int)(reftb.Width * 0.2);
            //reftb.Columns["Column5"].HeaderText = "天线(4-1)";
            //reftb.Columns["Column5"].Width = (int)(reftb.Width * 0.2);

            reftb.Columns[header_names[0]].Width = (int)(reftb.Width * 0.05);
            reftb.Columns[header_names[1]].Width = (int)(reftb.Width * 0.25);
            reftb.Columns[header_names[2]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[3]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[4]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[5]].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[header_names[6]].Width = (int)(reftb.Width * 0.1);
            reftb.RowHeadersVisible = false;
            reftb.GridColor = SystemColors.ControlDark; ;

        }

        public void set_view_update()
        {


            reftb.Columns[0].Width = (int)(reftb.Width * 0.05);
            reftb.Columns[1].Width = (int)(reftb.Width * 0.35);
            reftb.Columns[2].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[3].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[4].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[5].Width = (int)(reftb.Width * 0.1);
            reftb.Columns[6].Width = (int)(reftb.Width * 0.2);


        }

        public void table_load_into_viewer()
        {

            //  viewloader.reftb.DataSource = viewloader.dt ;

            tester_proj.clear_result();

            // 批量加载：暂停布局与数据通知，避免逐行触发 DataGridView 刷新
            reftb.SuspendLayout();
            dt.BeginLoadData();
            try
            {
                dt.Clear();
                for (int i = 0; i < tester_proj.test_cases.Count; i++)
                {
                    dt.Rows.Add(new string[] {                          tester_proj.test_cases[i].id.ToString(),
                                                                    tester_proj.test_cases[i].testcase_description,
                                                                    tester_proj.test_cases[i].testcase_high_limit,
                                                                    tester_proj.test_cases[i].testcase_low_limit,
                                                                    "",
                                                                    "",
                                                                    "",
                                                                    //tester_proj.test_cases[i].test_spik,
                                                                    //tester_proj.test_cases[i].repeat_goto,
                                                                    //tester_proj.test_cases[i].test_lib_string,
                                                                    //tester_proj.test_cases[i].self_run_count,
                                                                    //tester_proj.test_cases[i].parameter,
                                                                    

                });
                }
            }
            finally
            {
                dt.EndLoadData();
                reftb.ResumeLayout();
            }
        }



        public void set_enable_edit(bool is_edit)
        {

            if (is_edit == true)
            {

                reftb.ReadOnly = false;

                reftb.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                reftb.KeyDown += DataGridView_KeyDown;
            }
            else
            {


                reftb.ReadOnly = true;
                reftb.KeyDown -= DataGridView_KeyDown;
            }
        }

        public void set_row_color_and_2show(int i)
        {
            i = i + 1;
            if (i + 1 > reftb.Rows.Count) return;

            if (i > 0)
            {

                reftb.Rows[i - 1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                reftb.Rows[i - 1].Selected = false;
            }
            reftb.Rows[i].DefaultCellStyle.BackColor = Color.CadetBlue;
            reftb.Rows[i].Selected = true;


            if (i > 5) reftb.FirstDisplayedScrollingRowIndex = i - 5;



        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            // 如果用户按下 Ctrl + C 键，则复制所选行到剪贴板
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedRowsToClipboard();
            }
            // 如果用户按下 Ctrl + V 键，则将剪贴板中的行粘贴到当前位置
            else if (e.Control && e.KeyCode == Keys.V)
            {
                PasteRowsFromClipboard();
            }
        }


        private void CopySelectedRowsToClipboard()
        {
            // 获取所选行的索引
            var rowIndexes = reftb.SelectedRows.Cast<DataGridViewRow>()
                                                      .Select(row => row.Index)
                                                      .OrderBy(index => index);

            // 将行标题和行数据复制到剪贴板中
            var stringBuilder = new StringBuilder();
            foreach (var rowIndex in rowIndexes)
            {
                var row = reftb.Rows[rowIndex];
                foreach (DataGridViewCell cell in row.Cells)
                {
                    stringBuilder.Append(cell.Value?.ToString() ?? "");
                    stringBuilder.Append("\t");
                }
                stringBuilder.Length--; // 删除最后一个制表符
                stringBuilder.AppendLine();
            }
            Clipboard.SetText(stringBuilder.ToString(), TextDataFormat.UnicodeText);
        }


        private void PasteRowsFromClipboard()
        {
            // 获取当前单元格的行和列索引
            int rowIndex = reftb.CurrentCell.RowIndex;
            int columnIndex = reftb.CurrentCell.ColumnIndex;

            // 从剪贴板中获取行数据
            string clipboardData = Clipboard.GetText(TextDataFormat.UnicodeText);
            string[] rowStrings = clipboardData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray().Reverse().ToArray();

            // 将行数据插入到 DataGridView 中
            foreach (string rowString in rowStrings)
            {

                string[] cellStrings = rowString.Split('\t');
                DataRow row = dt.NewRow();

                for (int i = 0; i < cellStrings.Length && i < reftb.Columns.Count; i++)
                {
                    row[i] = cellStrings[i];
                }
                dt.Rows.InsertAt(row, rowIndex);
                // rowIndex++;
            }
        }
    }

































}
