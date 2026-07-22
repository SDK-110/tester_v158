using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwGprfGen;
using RohdeSchwarz.RsCmwGprfMeas;

namespace testapp.mylib
{

   
    class cmw100gps
    {
      
        RsCmwGprfGen driver;
        public cmw100gps(string drv = "TCPIP::localhost::INSTR")
        {

            driver = new RsCmwGprfGen(drv, true, true);

        }
      
        public void gps_ModeSet(BasebandModeEnum bbmode= BasebandModeEnum.DTONe) {

            
            driver.Source.BbMode = bbmode;
           

        }

        public void set_ware_file(string file ) {


            driver.Source.Arb.File.Set(file);

        }


        public void set_gpsFreq_level_gain(double freq= 1575420000,double levl=-60,double gain = 30) {


            driver.Source.RfSettings.Frequency = freq;
            driver.Source.RfSettings.Level = levl;
            driver.Source.RfSettings.Dgain = gain;

        }

        public void start_stop_gps(bool status=true) {


            driver.Source.State.Set(status);

        }

        ~cmw100gps() {

            driver.Dispose();

        }
    }
}
