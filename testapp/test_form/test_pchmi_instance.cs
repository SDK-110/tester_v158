using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp.test_form
{
    public partial class test_pchmi_instance : Form
    {
        private static test_pchmi_instance instance = null;

        public static  test_pchmi_instance get_instance() {

            if (instance == null) {

             instance =    new test_pchmi_instance();

            }
            instance.Show();
            return instance;
        }
        private test_pchmi_instance()
        {
            InitializeComponent();
            config1.START(this);
        }

        private void test_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Test");
            this.Hide();
            e.Cancel = true;
        }
    }
}
