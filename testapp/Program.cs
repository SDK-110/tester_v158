using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using testapp.duochuangti;
using testapp.From_yangshi;
using testapp.MyVisa;
using VMPro;

namespace testapp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            bool createNew;
            string setstr = testapp.glob_set.glob_ini_instance.getInstance().getSetupIniData["setproduct"]?["appmodel"];
       
                using (System.Threading.Mutex mute=new System.Threading.Mutex(true,Application.ProductName,out createNew))
            {

                if (createNew)
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);


                        switch (setstr)
                        {
                            case "mf":
                                {
                                    Application.Run(new main_m());

                                }
                                break;
                            case "debug":
                                {

                                    Application.Run(new FrmMain());

                                }
                                break;

                            case "test":
                                {

                                    Application.Run(new SGW_PROGRAM());
                                }
                                break;
                            case "test2":
                                {

                                    Application.Run(new whirlpool.whirlpool2());
                                }
                                break;
                            case "test3":
                                {
                                   
                                    Application.Run(new parallel_form());
                                }
                                break;
                            case "test4":
                                {

                                    Application.Run(new test_control());
                                }
                                break;
                            case "test5":
                                {

                                    Application.Run(new SGW_PROGRAM());
                                }
                                break;
                            case "test6":
                                {
                                 //  var p= testapp.test_form.test_2.get_instance();
                                    
                                    Application.Run(new testapp.test_form.test_plc_other());
                                }
                                break;
                            case "tmp":
                                {

                                    Application.Run(new testapp.test_form.test_temp());
                                }
                                break;
                            case "test_andt":
                                { 
                                
                                Application.Run(new testapp.test_form.test_antdui());
                                }
                                break;
                            case "tester":
                                {
                                    using (var login = new test_antdui.LoginForm())
                                    {
                                        if (login.ShowDialog() != DialogResult.OK)
                                            return;
                                    }
                                    try
                                    {
                                        Application.Run(new test_antdui.MainForm());
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show($"启动主窗体失败:\n{ex.Message}\n\n{ex.StackTrace}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                break;
                            default:
                                Application.Run(new Main_f());
                                break;
                        


                    }


                        //}
                        //Application.Run(new FrmMain());
                        //Application.Run(new HBI_SN_CREATE());

                        //  Frm_Welcome.Instance.Show();
                        // Application.DoEvents();
                        //   Application.Run(new yangshimoban());
                        // Application.Run(new Pictureshow(""));
                        //  Application.Run(new Main2());
                        //  Application.Run(new main_m());
                        //  Application.Run(new sgw_customer_test());
                        // Application.Run(new coil_project_4_unit());
                        // Application.Run(new TestcaseEdit4());
                        // Application.Run(new hayco());
                        // Application.Run(new pycom_form());

                    }
                else {

                    MessageBox.Show("app has already running");

                    System.Environment.Exit(1);

                }


            }
           
        }
    }
}
