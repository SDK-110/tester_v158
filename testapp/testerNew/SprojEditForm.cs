using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.Events;
using SprojFileBrowser;

namespace test_antdui
{
    public partial class SprojEditForm : Form
    {
        private static SprojEditForm instance_obj = null;
        private Worksheet m_sheet;
        public EventHandler is_called_by_other;
        int getrows = 0;

        public static SprojEditForm get_instance()
        {
            if (instance_obj == null || instance_obj.IsDisposed)
            {
                instance_obj = new SprojEditForm();
            }
            instance_obj.Show();
            return instance_obj;
        }

        private SprojEditForm()
        {
            InitializeComponent();
            this.tabPage1.Parent = null;

            reoGridControl1.Load(ProjectLoader.Instance.GetProjectNames()[0], unvell.ReoGrid.IO.FileFormat.Excel2007);
            m_sheet = reoGridControl1.Worksheets[0];
            reoGridControl1.CurrentWorksheet = m_sheet;
            m_sheet.CellMouseDown += sheet_CellMouseDown;
        }

        private void sheet_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            if (e.Cell != null)
            {
                getrows = e.Cell.Row;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text == "hrr" + DateTime.Now.ToString("ddmm"))
            {
                this.tabPage2.Parent = tabControl1;
                this.tabPage1.Parent = null;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.textBox1.Text == "hrr" + DateTime.Now.ToString("ddmm"))
                {
                    this.tabPage2.Parent = tabControl1;
                    this.tabPage1.Parent = null;
                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Text = "加载中....";
            button3.Enabled = false;
            reoGridControl1.Save(ProjectLoader.Instance.GetProjectNames()[0], unvell.ReoGrid.IO.FileFormat.Excel2007);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != null || comboBox1.Text != "")
            {
                m_sheet = reoGridControl1.Worksheets[comboBox1.Text];
                reoGridControl1.CurrentWorksheet = m_sheet;
            }
        }

        private void SprojEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (is_called_by_other != null)
            {
                is_called_by_other(this, new EventArgs());
            }
            this.Hide();
            e.Cancel = true;
        }

        private void SprojEditForm_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            comboBox1.SelectedIndex = 0;
        }
    }
}
