using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp
{
    public partial class HBI_SN_CREATE : Form
    {
        public HBI_SN_CREATE()
        {
            InitializeComponent();
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ArrayList sns = new ArrayList();

            this.richTextBox1.Text = "";
            string rs = "";

            int w = (int)DateTime.Now.DayOfWeek==0?7: (int)DateTime.Now.DayOfWeek;
           int ws = mylib.utility_func.getweekday();
           
            mylib.Mysql mysql = new mylib.Mysql("192.168.89.76", $"hbi_test", "root", "root");
            
                for (int i = 0; i < int.Parse(this.textBox1.Text); i++) {
                DataTable rst = mysql.Query($"call get_rand()");
                if (rst.Rows.Count > 0)
                {

                    rs = rst.Rows[0][0].ToString();

                }
                else {

                    MessageBox.Show("error");
                    return;
                }

                sns.Add(rs.Substring(0, 4) + DateTime.Now.Year.ToString().Substring(2, 2) + ws + this.comboBox1.Text + "23" + w +
                    rs.Substring(4, 4));
               
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter("SN_"+DateTime.Now.ToString("yy-MM-ddhhmmssfff") + "_"+ sns.Count + ".csv", false))
            {

                foreach (var sn in sns) {
                    file.Write((string)sn + '\n');
                    this.richTextBox1.AppendText((string)sn + "\n");
                }

                


            }
            MessageBox.Show("完成");


        }



   

  

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
