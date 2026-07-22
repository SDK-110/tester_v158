using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;

namespace testapp
{
  public   delegate void set_callback_reset();
    public partial class testcase_table_edit : Form
    {
       public static set_callback_reset reset;
        unvell.ReoGrid.Worksheet m_sheet = null;
        unvell.ReoGrid.Worksheet worksheet;
        public static string  test_case_table="";
        public static int selection;
        public testcase_table_edit()
        {
            InitializeComponent();

        }

        private void testcase_table_edit_Load(object sender, EventArgs e)
        {
            reoGridControl1.Load(test_case_table, unvell.ReoGrid.IO.FileFormat._Auto);
            m_sheet = reoGridControl1.Worksheets[selection];
            reoGridControl1.CurrentWorksheet = m_sheet;
        }

        private void testcase_table_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            reoGridControl1.Save(test_case_table);
            reset();
        }
    }
}
