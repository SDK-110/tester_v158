using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Text.RegularExpressions;

namespace testapp
{
   
 public    class Rigol_DG1000
    {
         private PortOperatorBase Rigol_DG1000_SingnG;
        public Rigol_DG1000(string devicename) {


            //{

            Rigol_DG1000_SingnG = PortUltility.usbport_op(devicename);

         
               
       try
            {

            
                Rigol_DG1000_SingnG.WriteLine("*IDN?");
                // string v =Rigol_DG1000_SingnG.ReadLine();
                System.Threading.Thread.Sleep(30);
                string v = Rigol_DG1000_SingnG.Read();
                 
             
            }
            catch (Exception)
            {
                 System.Windows.Forms.MessageBox.Show("The Rigol DG1000 is not connected properly or the resource port is not set properly,please restart app again");
            }


        }
        



        public Rigol_DG1000(String devicename, int baudrate) {


            //{
            Rigol_DG1000_SingnG = PortUltility.serial_op(devicename, baudrate);

            try
            {

            //    Rigol_DG1000_SingnG.WriteLine("CMDSET RIGOL");
                Rigol_DG1000_SingnG.WriteLine("*IDN?");
                string v = Rigol_DG1000_SingnG.ReadLine();

            }
            catch (Exception)
            {

                System.Windows.Forms.MessageBox.Show("The Rigol DG1000 is not connected properly or the resource port is not set properly,please restart app again");
            }





        }

        public void reset() {

      //      Rigol_DG1000_SingnG.WriteLine("*RST");
       //     Rigol_DG1000_SingnG.WriteLine("cmdset rigol");
        //    Rigol_DG1000_SingnG.WriteLine("*cls");
        }
        public void Close()
        {

            Rigol_DG1000_SingnG.Close();
        }

        public bool isOpen() {

            return Rigol_DG1000_SingnG.IsPortOpen;
        }
        public string  output_pluse_ch1(double freq, double amplitude, double offset = 0) {

            for (int tryint = 0; tryint < 3; tryint++) { 
            try
            {

                Rigol_DG1000_SingnG.WriteLine($"APPL:PULS {freq},{amplitude},{offset}");
                Rigol_DG1000_SingnG.WriteLine($"APPLy?");
                string tmp = Rigol_DG1000_SingnG.Read();

                Regex rex = new Regex(@"PULS", RegexOptions.IgnoreCase);
                MatchCollection matchs = rex.Matches(tmp);
                    if (matchs.Count == 0) continue;
                    else return "pass";
                //for (int t = 0; t < matchs.Count; t++)
                //{
                //    //  File.AppendAllText("debugdutcomm.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\r--->" + matchs[i].Groups[0].Value + "\r\n");
                //}

            }
            catch {


                    continue;
            }


            }
            return "fail";

        }



        public string output_onoff_ch1(bool onoff)
        {

            for (int tryint = 0; tryint < 3; tryint++)
            {
                try
                {
                    if (onoff == true)
                    {
                        Rigol_DG1000_SingnG.WriteLine($"OUTP ON");
                        Rigol_DG1000_SingnG.WriteLine($"OUTPUT?");
                        string tmp = Rigol_DG1000_SingnG.Read();
                        Regex rex = new Regex(@"ON", RegexOptions.IgnoreCase);
                        MatchCollection matchs = rex.Matches(tmp);
                        if (matchs.Count == 0) continue;
                        else return "pass";
                    }
                    else {

                        Rigol_DG1000_SingnG.WriteLine($"OUTP OFF");
                        Rigol_DG1000_SingnG.WriteLine($"OUTPUT?");
                        string tmp = Rigol_DG1000_SingnG.Read();
                        Regex rex = new Regex(@"OFF", RegexOptions.IgnoreCase);
                        MatchCollection matchs = rex.Matches(tmp);
                        if (matchs.Count == 0) continue;
                        else return "pass";

                    }

                  
                    //for (int t = 0; t < matchs.Count; t++)
                    //{
                    //    //  File.AppendAllText("debugdutcomm.txt", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "\r--->" + matchs[i].Groups[0].Value + "\r\n");
                    //}

                }
                catch
                {


                    continue;
                }


            }
            return "fail";

        }










        public string read_resistance()
        {
            Rigol_DG1000_SingnG.WriteLine(":function:resistance");
            Rigol_DG1000_SingnG.WriteLine(":measure auto");
            Rigol_DG1000_SingnG.WriteLine(":measure:resistance?");

            return Rigol_DG1000_SingnG.ReadLine();

        }

        public string read_resistance(int range)
        {
            Rigol_DG1000_SingnG.WriteLine(":function:resistance");
            Rigol_DG1000_SingnG.WriteLine(":measure:resistance " + range);
            Rigol_DG1000_SingnG.WriteLine(":measure:resistance?");

            return Rigol_DG1000_SingnG.ReadLine();

        }

     

        ~Rigol_DG1000() {


            Rigol_DG1000_SingnG.Close();
        }

    }
}
