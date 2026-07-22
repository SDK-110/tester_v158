using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp;

namespace testapp.test_cases
{
    internal class mydriver_test: IDefaultAction, IDisposable
    {


            string id = "";

            testcase_dll tc;
            string div_name = "";
            public mydriver_test(testcase_dll ref_tc, string div_name)
            {
                tc = ref_tc;
                this.div_name = div_name;
                add_func_to_libs();
                InsertDefaultAction();

            }

            public void add_func_to_libs()
            {
                id = this.GetType().Name + "." + div_name;
                tc.funcs.Add(id, test1);
                tc.golb_var_default["123"] = "fdsafds";
            }

            public void InsertDefaultAction()
            {


                tc.dev_moren[id] = this;

            }

            string test1(string a, string b, out string c, string d)
            {

                MessageBox.Show(this.GetType().Name + "." + div_name + " " + tc.golb_var_default["123"]);
                c = "pass";
                return "pass";
            }


            public void Dispose()
            {

                tc.dev_moren.Remove(id);
                tc.funcs.Remove(id);
            mylib.utility_func.callbackdebuginfo("close");

            }
            public void set_default_set()
            {
                mylib.utility_func.callbackdebuginfo("Test");
            }
        
    }
}
