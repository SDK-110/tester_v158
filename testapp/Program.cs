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
            // PCHMI.dll / SeeSharpTools.JY.GUI.dll 引用 System.Windows.Forms.DataVisualization v4.0.0.0 (MS原版, PKT=31bf3856ad364e35)
            // HIC 社区移植版程序集版本为 v1.0.0.0，无微软强签名。
            // 使用 Assembly.Load(byte[]) 绕过强签名验证 (FileLoadException 0x80131044)
            AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
            {
                if (e.Name.StartsWith("System.Windows.Forms.DataVisualization", StringComparison.OrdinalIgnoreCase))
                {
                    string dllPath = System.IO.Path.Combine(AppContext.BaseDirectory, "System.Windows.Forms.DataVisualization.dll");
                    if (System.IO.File.Exists(dllPath))
                        return System.Reflection.Assembly.Load(System.IO.File.ReadAllBytes(dllPath));
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

                    // 员工登录窗体：可通过 setup.ini [setproduct] login_enable 配置启用/关闭
                    // login_enable = true/1/yes 时显示登录窗体，取消则不进入系统
                    bool loginEnabled = true;
                    var iniData = testapp.glob_set.glob_ini_instance.getInstance().getSetupIniData;
                    if (iniData["setproduct"] != null && iniData["setproduct"]["login_enable"] != null)
                    {
                        string v = iniData["setproduct"]["login_enable"].ToString().Trim().ToLower();
                        loginEnabled = (v == "1" || v == "true" || v == "yes");
                    }
                    if (loginEnabled)
                    {
                        using (var login = new VMPro.Frm_Welcome())
                        {
                            if (login.ShowDialog() != DialogResult.OK)
                                return;

                            // 将员工工号传入主窗体：
                            // setup.ini 的 personal_number 已由登录窗体写入（主窗体与测试备份均从 setup.ini 读取）；
                            // 同时写入 ProductionTracker（testerNew 主窗体通过它读取）。
                            try
                            {
                                test_antdui.ProductionTracker.Instance.OperatorName = login.EmployeeId;
                            }
                            catch (Exception)
                            {
                                // 生产跟踪不可用时忽略，不影响登录
                            }
                        }
                    }

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
