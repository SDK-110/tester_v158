using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testapp
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            bool createNew;

            using(System.Threading.Mutex mute=new System.Threading.Mutex(true,Application.ProductName,out createNew))
            {

                if (createNew)
                {

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());

                }
                else {

                    MessageBox.Show("app has already running");

                    System.Environment.Exit(1);

                }


            }
           
        }
    }
}
