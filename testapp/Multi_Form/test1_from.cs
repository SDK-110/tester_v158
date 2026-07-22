using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.glob_set;
using WeifenLuo.WinFormsUI.Docking;

namespace testapp.duochuangti
{
    public partial class test1_form : DockContent
    {
       
        private test1_form()
        {
            InitializeComponent();
        }

        public static test1_form get_form_instance()
        {

            if (dut1 == null) dut1 = new test1_form();

            return dut1;


        }


        private void userControl21_Load(object sender, EventArgs e)
        {

            // dll = new testcase_dll();


        }

        public void run()
        {

            userControl.run();
        }

        private void test1_form_Load(object sender, EventArgs e)
        {

        }

        private void test1_form_Shown(object sender, EventArgs e)
        {
            this.userControl.deal_withmsg += (o1, o2) =>
            {
                var tmp = o1 as msgpacketer;

                if (tmp.msg == "pass")
                {
                    char_form.get_form_instance().set_ok_add();

                    input_form.GetTrigger_Form_instance().set_pass_dut1();
                };
                if (tmp.msg == "fail")
                {
                    char_form.get_form_instance().set_ng_add();

                    input_form.GetTrigger_Form_instance().set_fail_dut1();

                }
             //   input_form.GetTrigger_Form_instance().set_input_box_clear_1();

                if (loop_flog == 1) run();
            };

        }

        public void set_ini(testcase_dll dll_m)
        {

            string run_flog =glob_ini_instance.getInstance().getSetupIniData["statu"]["NG_RUN"];
            userControl.set_testcase_action(run_flog);
            userControl.set_init_4runlib_testcase(ref dll_m);
            userControl.set_production_info(new production_info() { log_path_name = "DUT1_LOG.csv" });
            this.userControl.init_flog = 1;
            this.userControl.done_dealwith_flog = 1;
        }

        public void set_sn(string sn)
        {

            userControl.set_production_info(new production_info() { SN = sn });

        }


        public volatile int loop_flog = 0;
       
        static test1_form dut1;
    }
}
