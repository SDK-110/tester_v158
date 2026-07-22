using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.test_form
{
    public partial class tff_ziti : Form
    {
        private DataTable dataTable;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        public tff_ziti()
        {
            InitializeComponent();


            this.SetStyle(ControlStyles.DoubleBuffer |
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint,
             true);
            this.UpdateStyles();

            dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Columns.Add("Age", typeof(int));
        }

        private Font LoadFont(string fontPath)
        {
            PrivateFontCollection privateFonts = new PrivateFontCollection();
            privateFonts.AddFontFile(fontPath);

            Font font = new Font(privateFonts.Families[0], 10);
            return font;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage(this.textBox1.Handle, EM_SETCUEBANNER, IntPtr.Zero, "你是谁，为了谁");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 500; i++)
            {
                dataTable.Rows.Add(i * 3, "你好", 25);
                dataTable.Rows.Add(i * 3 + 1, "\uF00C", 30);
                dataTable.Rows.Add(i * 3 + 2, "Bob", 35);
                dataGridView1.Rows[3 * i + 1].Cells[1].Style.ForeColor = Color.Green;
            }

        }
    }
}
