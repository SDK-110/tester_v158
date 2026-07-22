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
    public partial class test2_form : DockContent
    {
        public volatile int loop_flog = 0;
        static test2_form dut2;
    
        private test2_form()
        {
            InitializeComponent();
        }

        public static test2_form get_form_instance()
        {

            if (dut2 == null) dut2 = new test2_form();

            return dut2;


        }

        public void run()
        {

            userControl21.run();
        }

        public void set_ini(testcase_dll dll_m)
        {

            string run_flog =glob_ini_instance.getInstance().getSetupIniData["statu"]["NG_RUN"];
            userControl21.set_testcase_action(run_flog);
            this.userControl21.set_production_info(new production_info() { log_path_name = "DUT2_LOG.csv" });
            this.userControl21.set_init_4runlib_testcase(ref dll_m, SprojFileBrowser.ProjectLoader.Instance.GetProjectNames()[0]);
            this.userControl21.init_flog = 2;
            this.userControl21.done_dealwith_flog = 2;


        }

        private void test2_from_Load(object sender, EventArgs e)
        {

        }

        public void set_sn(string sn)
        {

            userControl21.set_production_info(new production_info() { SN = sn });

        }

        private void test2_from_Shown(object sender, EventArgs e)
        {
            this.userControl21.deal_withmsg += (o1, o2) =>
            {
                var tmp = o1 as msgpacketer;

                if (tmp.msg == "pass")
                {
                    char_form.get_form_instance().set_ok_add();

                    input_form.GetTrigger_Form_instance().set_pass_dut2();
                };
                if (tmp.msg == "fail")
                {
                    char_form.get_form_instance().set_ng_add();

                    input_form.GetTrigger_Form_instance().set_fail_dut2();

                }
               // input_form.GetTrigger_Form_instance().set_input_box_clear_2();

                if (loop_flog == 1) run();
            };
        }
    }
}
