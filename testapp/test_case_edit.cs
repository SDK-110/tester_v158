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
using unvell.ReoGrid.Events;

namespace testapp
{
    public partial class test_case_edit : Form
    {
        private static test_case_edit instance_obj = null;
        private Worksheet m_sheet;
        public EventHandler is_called_by_other;
        int getrows = 0;

        public static test_case_edit get_instance() {

            if (instance_obj == null) {
                instance_obj = new test_case_edit();
            }
            instance_obj.tabPage1.Parent = instance_obj.tabControl1;
            instance_obj.tabPage2.Parent = null;
            instance_obj.textBox1.Text = "";
           instance_obj.reoGridControl1.Load(SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0], unvell.ReoGrid.IO.FileFormat.Excel2007);
            instance_obj.m_sheet = instance_obj.reoGridControl1.Worksheets["sheet1"]; ;
            instance_obj.reoGridControl1.CurrentWorksheet = instance_obj.m_sheet;
            instance_obj.m_sheet.CellMouseDown += instance_obj.sheet_CellMouseDown;
            instance_obj.Show();
            return instance_obj;
        }
       private test_case_edit()
        {
            InitializeComponent();
           // this.tabPage1.Parent = null;
            this.tabPage1.Parent= tabControl1;
            this.tabPage2.Parent = null;
            reoGridControl1.Load(SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0], unvell.ReoGrid.IO.FileFormat.Excel2007);
            m_sheet = reoGridControl1.Worksheets["sheet1"]; ;
            reoGridControl1.CurrentWorksheet = m_sheet;
            m_sheet.CellMouseDown += sheet_CellMouseDown;
        }

        private void sheet_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            if(e.Cell != null) {

                getrows = e.Cell.Row;

               // MessageBox.Show(getrows+"");
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button1.Text = "保存中..";
            //button1.Enabled = false;
            //reoGridControl1.Save("project_tester_name.dll",unvell.ReoGrid.IO.FileFormat.Excel2007);
            //reoGridControl1.Save("project_tester_name.dll", unvell.ReoGrid.IO.FileFormat.Excel2007);
            //new Task(() =>
            //{

            //    System.Threading.Thread.Sleep(3000);
               
            //    this.Invoke((Action)delegate
            //    {
            //        button1.Text = "保存OK";
            //        button1.Enabled = true;

            //    });
            //}).Start();

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
          
           
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
        
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            if (this.textBox1.Text == "hrr" + DateTime.Now.ToString("ddmm")) {

                this.tabPage2.Parent = tabControl1;
                this.tabPage1.Parent = null;
            };
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {

                if (this.textBox1.Text == "hrr" + DateTime.Now.ToString("ddmm"))
                {

                    this.tabPage2.Parent = tabControl1;
                    this.tabPage1.Parent = null;
                };

            }
        }

        private void reoGridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.I) {


              
                if (getrows < 4) return;
                m_sheet.InsertRows(getrows, 1);
                m_sheet.Ranges[$"A{getrows+1}:G{getrows+1}"].Data = new Object[] { "", "", "", "", "", "", "" };

            }

            if (e.Control && e.KeyCode == Keys.D)
            {



                if (getrows < 4) return;
                m_sheet.DeleteRows(getrows , 1);
              
            }
        }

        private void Form2_1_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = "加载中....";
            button3.Enabled = false;
            reoGridControl1.Save(SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0], unvell.ReoGrid.IO.FileFormat.Excel2007);

            new Task(() =>
            {

                System.Threading.Thread.Sleep(3000);
                if (is_called_by_other != null)
                {

                    is_called_by_other(this, new EventArgs());
                }
                this.Invoke((Action)delegate
                {
                    button3.Text = "加载OK";
                    button3.Enabled = true;

                });
            }).Start();

          
        }

        private void test_case_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_called_by_other != null)
            {

                is_called_by_other(this, new EventArgs());
            }
            this.Hide();
            e.Cancel = true;
        }
    }
}
