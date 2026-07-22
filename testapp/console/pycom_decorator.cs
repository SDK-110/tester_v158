#define zhoukai_upload
using System;
using System.IO.Ports;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.Windows.Forms;

namespace testapp

{
    class console_pycom
    {
      static  volatile int flog = 0;
        static void Main(string[] args)
        {
#if zhoukai_upload
            if (args.Length != 2) { Console.WriteLine("input error"); return; }
            string sn = args[0];
            string resu = args[1].ToUpper();
            try
            {
                if (testapp.mylib.utility_func.instert_mysql_value(
                     tablename: "sgw_airseal_data",
                     serial_number: sn,
                     status_code: resu == "FAIL"?"-1" : "1"


                     ) == 1)
                {

                    Console.WriteLine("upload_ok");
                    return;
                };

                Console.WriteLine("upload_error");
                return;
            }
            catch {

                Console.WriteLine("upload_error2");
                return;
            }
#endif

#if abc
            Console.Title = "pycom_01_left";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            var ws=   windows_find.windowns_find.FindWindow(null, "pycom_01_left");

            Console.SetWindowSize(50, 20);

            // setting buffer size  
            // Console.SetBufferSize(80, 80);

            // using the method 
            windows_find.windowns_find.SetWindowPos(ws, IntPtr.Zero, 0, Screen.PrimaryScreen.Bounds.Height / 2 - 500/2, 500, 500, 0);

           Process process1 = new Process();
            process1.StartInfo.FileName = @"L01pgm.bat";
            process1.StartInfo.Arguments = "";
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.RedirectStandardError = true;
            process1.StartInfo.CreateNoWindow = false;//true表示不显示黑框，false表示显示dos界面 
            process1.StartInfo.UseShellExecute = false;

            process1.EnableRaisingEvents = true;

            //  process1.Exited += new EventHandler(p_Exited);
            process1.OutputDataReceived += new DataReceivedEventHandler(GethOutputHandler);

            process1.ErrorDataReceived += new DataReceivedEventHandler(GethOutputHandler);




            //process1.Start();

            //process1.BeginOutputReadLine();
            //process1.BeginErrorReadLine();
            while (flog == 1) { System.Threading.Thread.Sleep(500); }
            Console.WriteLine(" please input next dut");
            Console.ReadKey();
            Console.Clear();
#endif
        }




    
    public static void GethOutputHandler(object sender, DataReceivedEventArgs dataReceived)
    {
        if (dataReceived.Data == null)
            return;
        if ("WLAN MAC address reading succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: WLAN MAC address reading failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("VDD_SDIO voltage setting succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: VDD_SDIO voltage setting failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("Batch erasing succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: Batch erasing failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("Batch programming succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: Batch erasing failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("Batch programming succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: Batch firmware programming failed on some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("Batch testing succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: Batch testing failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        if ("Batch MAC programming succeeded :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        if ("ERROR: Batch MAC programming failed in some boards!" == dataReceived.Data)
        {


            Console.ForegroundColor = ConsoleColor.DarkRed;
        }


        if ("Final test succeeded on all boards :-)" == dataReceived.Data)
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            flog = 0;
        }
        if ("ERROR: Some boards failed the final test!" == dataReceived.Data)
        {

            flog = 0;
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }



        Console.WriteLine(dataReceived.Data);

        Console.ForegroundColor = ConsoleColor.Black;
    }

}
}
