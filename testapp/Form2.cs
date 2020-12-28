using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rep2.SaveReport(@"testcasetable");
            MessageBox.Show("保存完成");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            rep2.OpenReport(@"testcasetable");
           
        }

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            rep2.Width = this.Width - 100;
            rep2.Top = 5;
            rep2.Left = 5;
            rep2.Height = this.Height - 50;
            this.button1.Left = this.rep2.Right + 5;
        }
    }
}
