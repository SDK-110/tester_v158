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
            // .NET 8 迁移：重定向 .NET Framework DLL 对旧版程序集的引用
            // PCHMI.dll / SeeSharpTools.JY.GUI.dll 引用 System.Windows.Forms.DataVisualization v4.0.0.0 (MS原版)
            // HIC 社区移植版程序集版本为 v1.0.0.0，版本不匹配导致运行时 FileNotFoundException
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                if (e.Name.StartsWith("System.Windows.Forms.DataVisualization", StringComparison.OrdinalIgnoreCase))
                {
                    string dllPath = System.IO.Path.Combine(AppContext.BaseDirectory, "System.Windows.Forms.DataVisualization.dll");
                    if (System.IO.File.Exists(dllPath))
                        return System.Reflection.Assembly.LoadFrom(dllPath);
                }
                return null;
            };

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
