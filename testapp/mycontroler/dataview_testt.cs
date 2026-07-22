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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class dataview_testt : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        DataTable dataTable;
        public dataview_testt()
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
       
            dataGridView1.DataSource = dataTable;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            string fontPath = "FontAwesome.ttf"; // 替换为您的实际字体路径
            Font customFont = LoadFont(fontPath);
            dataGridView1.DefaultCellStyle.Font = customFont;
            button2.Font = new Font(customFont.FontFamily, 24); ;
            
            button2.Text = "\uF00C pp";
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
            testapp.windows_find2.sendmessage.SendMessage(this.textBox1.Handle, EM_SETCUEBANNER, IntPtr.Zero, "fffffffffffff");
            hollowCircularProgressControl1.Progress += 1;
        }

        private void customInputBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 创建数据源
           

            // 添加数据行
            for (int i = 0; i < 500; i++) { 
            dataTable.Rows.Add(i*3, "你好", 25);
            dataTable.Rows.Add(i*3+1, "\uF00C", 30);
            dataTable.Rows.Add(i*3+2, "Bob", 35);
                dataGridView1.Rows[3*i+1].Cells[1].Style.ForeColor = Color.Green;
            }

            // 设置数据源


            // 自动调整列宽
            // dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void customInputBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void uiSwitch1_ValueChanged(object sender, bool value)
        {

        }
    }
}
