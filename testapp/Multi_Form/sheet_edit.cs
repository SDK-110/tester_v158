using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.Events;

namespace testapp.duochuangti
{
    public partial class sheet_edit : Form
    {
        string test_cases_file;
        private   Worksheet m_sheet;
        public EventHandler is_called_by_other;
        int getrows = 0;

        public sheet_edit()
        {
            InitializeComponent();
        }

        private void sheet_edit_Load(object sender, EventArgs e)
        {
           
        }

        public void load_projectt_file(string projectt_name = "project_tester_name.sproj") {

            test_cases_file=projectt_name;
            reoGridControl1.Load(projectt_name, unvell.ReoGrid.IO.FileFormat.Excel2007);
            m_sheet = reoGridControl1.Worksheets[0]; ;
            reoGridControl1.CurrentWorksheet = m_sheet;
            m_sheet.CellMouseDown += sheet_CellMouseDown;


        }
        private void sheet_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            if (e.Cell != null)
            {

                getrows = e.Cell.Row;

                // MessageBox.Show(getrows+"");
            }

        }
        private void reoGridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.I)
            {



                if (getrows <= 4) return;
                m_sheet.InsertRows(getrows, 1);
                m_sheet.Ranges[$"A{getrows + 1}:G{getrows + 1}"].Data = new Object[] { "", "", "", "", "", "", "" };

            }

            if (e.Control && e.KeyCode == Keys.D)
            {



                if (getrows <= 4) return;
                m_sheet.DeleteRows(getrows, 1);

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
            Task.Factory.StartNew(() =>
            {

                this.Invoke(new Action(() =>
                {

                    reoGridControl1.Save(test_cases_file, unvell.ReoGrid.IO.FileFormat.Excel2007);

                }));
              

            });
            }

        private void sheet_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //reoGridControl1.Save(test_cases_file, unvell.ReoGrid.IO.FileFormat.Excel2007);
        }
    }
}
