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
    public partial class Form2_1 : Form
    {
        private Worksheet m_sheet;
        public EventHandler is_called_by_other;
        int getrows = 0;
        public Form2_1()
        {
            InitializeComponent();
           // this.tabPage1.Parent = null;
            
            this.tabPage2.Parent = null;
            reoGridControl1.Load("project_tester_name.dll", unvell.ReoGrid.IO.FileFormat.Excel2007);
            m_sheet = reoGridControl1.Worksheets[0]; ;
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
            if (is_called_by_other != null) {

                is_called_by_other(this, new EventArgs());
            }
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



                if (getrows <= 4) return;
                m_sheet.InsertRows(getrows, 1);
                m_sheet.Ranges[$"A{getrows+1}:G{getrows+1}"].Data = new Object[] { "", "", "", "", "", "", "" };

            }

            if (e.Control && e.KeyCode == Keys.D)
            {



                if (getrows <= 4) return;
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
            reoGridControl1.Save("project_tester_name.dll", unvell.ReoGrid.IO.FileFormat.Excel2007);

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
    }
}
