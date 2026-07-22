using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace testapp.duochuangti
{
    public partial class char_form : DockContent
    {
        static char_form char_form_obj;
        private char_form()
        {
            test_unit_count.chart_data_path = "chart_data";
            InitializeComponent();
           
        }

        public static  char_form get_form_instance() { 
        
        if(char_form_obj == null) { char_form_obj = new char_form(); }

        return char_form_obj;
        
        }

        public void set_ok_add() {

            this.userControl31.data_.addOK(1);

        }

        public void set_ng_add()
        {

            this.userControl31.data_.addNG(1);

        }

        public void clear_data() {

            this.userControl31.data_.clear_data();
            this.userControl31.chart_display();

        }
    }
}
