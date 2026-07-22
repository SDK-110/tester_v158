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
    public partial class parameterset : Form
    {
        public parameterset()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            testcase_dll z = Main_f.testcase_lib;
            chroma19701.setred_step pp = new chroma19701.setred_step();
            pp.head = 0xab;
            pp.target_add = 0x01;
            pp.source_add = 0x70;
            pp.datalen = 0x1d;
            pp.comm = 0x24;
            pp.mod = byte.Parse(this.comboBox2.Text);
            pp.voltage = int.Parse(this.textBox1.Text);
            pp.RampTimems = int.Parse(this.textBox2.Text);
            pp.fallms = int.Parse(this.textBox5.Text);
            pp.hilimitmA = float.Parse(this.textBox6.Text);
            pp.lowlimitmA = float.Parse(this.textBox7.Text);
            pp.reserved = new byte[] { 0, 0, 0,0 };
            pp.Reserved = int.Parse(this.textBox3.Text);
            pp.testtimems = int.Parse(this.textBox4.Text);
            pp.step = byte.Parse(this.comboBox1.Text);
            pp.arclimitmA = float.Parse(this.textBox8.Text);


            z.Chroma19701t.setparameters(pp);
            
            
        
        }
    }
}
