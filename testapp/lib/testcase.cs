using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;
using IniParser;
using IniParser.Model;
using VISAInstrument.Port;

namespace testapp_dll
{
    public delegate string pointfun(string a, string b, out string c,string d="");
    public class testcase_dll
    {
        string c = "";
        private IniParser.FileIniDataParser iniread = new FileIniDataParser();
        //   DM3058 dm3058;
        // serial_no_visa relay;
        TDM9001_2A mincurm;
        TMD1501_50 minvm;
        sevy_relay ry;
        led_assy ledassyer;
        TRM1201 TRM1201reader;

        public testcase_dll()
        {

            #region  //注册case 函数
            m.Add("TDM9001_2A_read", TDM9001_2A_read);
            m.Add("TMD1501_50_read", TMD1501_50_read);
            m.Add("relay_set", relay_set);
            m.Add("cloor_assy", cloor_assy);
            m.Add("TRM1201ReadRes", TRM1201ReadRes);
            m.Add("PipRunning", PipRunning);
            m.Add("dd", dd);
            #endregion


            #region   //通讯资源加载
            ///*屏蔽q
            #region 暂时不用的继电器板子
            /*暂时不用
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa"] != null) {
                       try
                       {
                           string sr_n_port = iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa"];
                           int sr_n_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["Relay_board_no_visa_baudrate"]);
                           relay = new ClassLibrary1.serial_no_visa(sr_n_port, sr_n_bautrate);
                       }
                       catch (Exception) {

                           System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["Relay_board"] +"不存在或被霸占,请检查" );
                       }
          
        }
暂时不用*/
            #endregion

            if (iniread.ReadFile("setup.ini")["setport"]["DM3058"] != null) { 
            try
            {
               //string sr_port = iniread.ReadFile("setup.ini")["setport"]["DM3058"];
               //int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["DM3058_baudrate"]);

           //    dm3058 = new DM3058(sr_port,sr_bautrate);
           }
           catch (Exception) {


               //System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["DM3058"] + "不存在或被霸占,请检查");

           }
           }
            if (iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"] != null) { 
            try
           {
                 string sr_port = iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"];
                  int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A_baudrate"]);

               //    dm3058 = new DM3058(sr_port,sr_bautrate);
                mincurm = new TDM9001_2A(sr_port, sr_bautrate);
             //  mincurm.read();
           }
           catch (Exception)
           {


              System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TDM9001_2A"] + "电流表不存在或被霸占,请检查");

           }
            }
            if (iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"] != null) {

            try
           {
               string sr_port = iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"];
               int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TMD1501_50_baudrate"]);

               //    dm3058 = new DM3058(sr_port,sr_bautrate);
               minvm = new TMD1501_50(sr_port, sr_bautrate);

            //   minvm.read();


           }
           catch (Exception)
           {


                System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TMD1501_50"] + "电压表不存在或被霸占,请检查");

           }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["Relay_board"] != null) {
            try
           {
               string sr_port = iniread.ReadFile("setup.ini")["setport"]["Relay_board"];
               int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["Relay_board_baudrate"]);

               //    dm3058 = new DM3058(sr_port,sr_bautrate);
              ry = new sevy_relay(sr_port, sr_bautrate);

               ry.set_relay(0XF0,0x0F);


           }
           catch (Exception)
           {


               System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["Relay_board"] + "继电器不存在或被霸占,请检查");

           }
                //  uSBPort =PortUltility.usbport_op("fdsa");
            }
            if (iniread.ReadFile("setup.ini")["setport"]["color_assyer"]!= null) {
            try
            {
                string color_assyer_port = iniread.ReadFile("setup.ini")["setport"]["color_assyer"];
                int color_assyer_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["color_assyer_baudrate"]);

                //    dm3058 = new DM3058(sr_port,sr_bautrate);
               ledassyer = new led_assy(color_assyer_port, color_assyer_bautrate);

                ledassyer.try_comm();


            }
            catch (Exception)
            {


                System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["color_assyer"] + "颜色模块不存在或被霸占,请检查");

            }
            }


            if (iniread.ReadFile("setup.ini")["setport"]["TRM1201"] != null)
            {
                try
                {
                    string sr_port = iniread.ReadFile("setup.ini")["setport"]["TRM1201"];
                    int sr_bautrate = int.Parse(iniread.ReadFile("setup.ini")["setport"]["TRM1201_baudrate"]);

                    //    dm3058 = new DM3058(sr_port,sr_bautrate);
                    TRM1201reader = new TRM1201(sr_port, sr_bautrate);
                    //  mincurm.read();
                }
                catch (Exception)
                {


                    System.Windows.Forms.MessageBox.Show(iniread.ReadFile("setup.ini")["setport"]["TRM1201"] + "电阻表不存在或被霸占,请检查");

                }
            }






            //屏蔽 */












            #endregion
        }
        private Dictionary<string, pointfun> m = new Dictionary<string, pointfun>();

        string PipRunning(string a, string b, out string c, string d)
        {
            string judge = "";
            string m =  new piprun(d, "").getruninfo();
            if (m.IndexOfAny(a.ToArray()) > 0) {

                judge = "pass";
            } else {
                judge = "fail";
            };
            c = judge;
            return judge;


        }



            string TRM1201ReadRes(string a, string b, out string c, string d) {

            string jud = "";
            int m = TRM1201reader.readres(int.Parse(d));
            c = m + "";
            if (float.Parse(a) >= m && float.Parse(b) <= m)
            {

                jud = "pass";
            }
            else {

                jud = "fail";
            }


            return jud;
        }











        string TDM9001_2A_read(string a, string b, out string c,string d)
        {
            string jud ="";
           
            float z =  mincurm.read();
            if (float.Parse(a) >= z && float.Parse(b) <= z)
            {

                jud = "pass";

            }
            else {

                jud = "fail";
            }

            c = z +"";
           
           return jud;          /*"fail";*/
        }

        string TMD1501_50_read(string a, string b, out string c, string d)
        {
            string jud = "";

            float z = minvm.read();
            if (float.Parse(a) >= z && float.Parse(b) <= z)
            {

                jud = "pass";

            }
            else
            {

                jud = "fail";
            }

            c = z + "";

            return jud;          /*"fail";*/
        }


        string relay_set(string a, string b, out string c, string d)
        {
            string[] p = d.Split(";".ToCharArray());
            ry.set_relay(Byte.Parse(p[0],System.Globalization.NumberStyles.HexNumber),Byte.Parse(p[1], System.Globalization.NumberStyles.HexNumber));

            c = "pass";
            return "pass";
        }



        string cloor_assy(string a, string b, out string c,string d)
        {
           
            string judge1 = "";
            int cu = 0;

            do
            {
                string[] lowlimit = a.Split(";".ToCharArray());
                int[] ll = new int[] { int.Parse(lowlimit[0]), int.Parse(lowlimit[1]), int.Parse(lowlimit[2]), int.Parse(lowlimit[3]) };

                string[] uplimit = b.Split(";".ToCharArray());

                int[] ul = new int[] { int.Parse(uplimit[0]), int.Parse(uplimit[1]), int.Parse(uplimit[2]), int.Parse(uplimit[3]) };

                int[] rsut = ledassyer.getRGBI(int.Parse(d));

                if (rsut[0] > ll[0] && rsut[0] < ul[0])
                {
                    if (rsut[1] > ll[1] && rsut[1] < ul[1])
                    {
                        if (rsut[2] > ll[2] && rsut[2] < ul[2])
                        {
                            if (rsut[3] > ll[3] && rsut[3] < ul[3])
                            {

                                c = "pass";
                                judge1 = "pass";

                            }
                            else
                            {

                                c = "intensity componet ng";
                                judge1 = "fail";


                            }

                        }
                        else
                        {

                            c = "blue componet ng";
                            judge1 = "fail";



                        }

                    }
                    else
                    {
                        c = "green componet ng";
                        judge1 = "fail";


                    }


                }
                else
                {

                    c = "red componet ng";
                    judge1 = "fail";


                }
                cu++;
            } while (judge1 == "fail" && cu < 3);



            return judge1;

        }

        string cc(string a, string b, out string c,string d)
        {

        

          

            c = "tt";
            return "fsdaf";
        }

        string dd(string a, string b, out string c, string d)
        {



           

            c = "tt";
            return "dd";
        }

        public Dictionary<string, pointfun> Getfun()
        {



            return m;
        }

        private void  send_string(string [] abc) {

            foreach (string  a in abc)

            {
                string p = a.Remove(0);



            }


        }

      
    }
}

