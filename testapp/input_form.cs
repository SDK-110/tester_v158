using rebuild.testcase_loader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
namespace testapp
{
    public partial class input_form : MetroForm
    {
      
        string _optstr = "";
        public input_form()
        {
            
            InitializeComponent();
           this.metroButton1.Click += metroButton1_Click;
         
        }
        
        private void input_form_Shown(object sender, EventArgs e)
        {
            
            
          
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public string oper_str
        {
            get
            {

                return _optstr;
            }


        }

   

       
        private void metroButton1_Click(object sender, EventArgs e)
        {
            if (_optstr.Length > 0) this.Hide();

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            _optstr = metroButton1.Text;
        }
    }
}
