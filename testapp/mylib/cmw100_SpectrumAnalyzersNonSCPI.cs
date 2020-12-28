using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwBluetoothSig;
using RohdeSchwarz.RsCmwGprfGen;
using RohdeSchwarz.RsCmwGprfMeas;
using RohdeSchwarz.RsInstrument;
namespace testapp.mylib
{
    class cmw100_SpectrumAnalyzersNonSCPI
    {

        private double offset;

        public double OFFSET {

            set
            {

                offset = value;
            }

            get {

                return offset;
            }


        }
       private RsCmwGprfMeas driver  = null;
        public cmw100_SpectrumAnalyzersNonSCPI(String resource  = "TCPIP0::192.168.8.89::inst0::INSTR")
        {

            try // Separate try-catch for initialization prevents accessing uninitialized object
            {
                //-----------------------------------------------------------
                // Initialization:
                //-----------------------------------------------------------
                driver = new RsCmwGprfMeas(resource);
               


            }
            catch (RsInstrumentException e)
            {

                System.Windows.Forms.MessageBox.Show("instrument load error");


            }

           
        }

        public bool init_SpectrumAnalyzers(double centerFreq = 2402000000 /*GHz*/,
                                           double offset = 0/*dbm*/,
                                            double span = 10000000/*MHz*/,
                                            double RBW = 4000000/*KHz*/,
                                            double VBW = 100000/*KHz*/,
                                            int sweep = 10,/*sweep points*/
                                            double swttime = 0.05
                                            )
        {
            try
            {

                driver.Utilities.VisaTimeout = 100000;
                
                driver.Configure.RfSettings.Eattenuation = offset;
                driver.Configure.Display = MeasTabEnum.SPECtrum;
                driver.Configure.RfSettings.Frequency = centerFreq;
                driver.Configure.Spectrum.Frequency.Span.Value = span;
                driver.Configure.Spectrum.FreqSweep.Vbw.Value = VBW;
                driver.Configure.Spectrum.FreqSweep.Rbw.Value = RBW;
               // driver.Configure.Spectrum.FreqSweep.Swt.Auto=true;
                driver.Configure.Spectrum.Scount = sweep;
                driver.Configure.Spectrum.Timeout =1000000000;
                driver.Configure.Spectrum.Amode = AveragingModeEnum.LINear;
                driver.Configure.Spectrum.Repetition = RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.SINGleshot;   //RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.CONTinuous;
                                                                                                           //  driver.Route.Value.RfConnector = RFConnectorEnum.RF3;
               driver.Configure.Spectrum.FreqSweep.Swt.Auto = false;
                 driver.Configure.Spectrum.FreqSweep.Swt.Value = swttime;
                driver.Spectrum.Initiate();
                //  driver.Utilities.QueryOpc();
                return true;
              
            }
            catch (RsInstrumentException e) {

              //  System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }

        }

        public bool getmark_feq_level(int boardwdth, out double level, out double freqdev,out bool isfreqdev , int sample = 0, int wait = 3000) {
           
            level = -1000;     
            freqdev = -1000000;
            if (boardwdth < 250)
            {
                boardwdth = 250 - boardwdth;
            }
            else {

                boardwdth = 0;
            }



            try
            {


  


                int count = sample;
                double[] freqanalysis =  new double[501] ;

                for (int i = 0; i < 501; i++) {

                    freqanalysis[i] = -200;
                }

                double comp=-1000;
                double centerlevel=-1000;
                do
                {

                    driver.Spectrum.Initiate();
                    System.Threading.Thread.Sleep(wait);
                    driver.Utilities.QueryOpc();
                 //  List<double> restlt =  driver.Spectrum.Sample.Maximum.Read();
                     List<double> restlt = driver.Spectrum.Maximum.Maximum.Fetch();
                    // driver.Utilities.QueryOpc();
                    //  driver.Spectrum.Initiate();

                   
                    if (false)
                    {  //debug read value//
                        string ggg = "";
                        for (int i = 0; i < restlt.Count; i++)
                        {

                            ggg = ggg + "," + restlt[i];
                        }
                        ggg = ggg + "\n";
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\ENG-TE\Desktop\12.csv", true))
                        {
                            file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff") + "," + ggg);


                        }
                    }


               

                   // centerlevel = restlt[(restlt.Count-1)/2];
                  //  int t = restlt.Count();
                    if (comp < restlt.Max()) {
                    for (int i = 0; i < 501; i++) {
                            if(freqanalysis[i]<( restlt[(restlt.Count - 1) / 2 - 250 + i])){
                                freqanalysis[i] = restlt[(restlt.Count - 1) / 2 - 250  + i];
                            }

                        }
                        comp = restlt.Max();
                    }

                 

                } while (--count >= 0);

                

                
             //   double bdlevl = (freqanalysis[250] - (Math.Abs(freqanalysis[250] * 0.3)));

              //  string p = "";
                double mx = freqanalysis.Max();
                double maxpower = -200;
                int z = 0;
                for (int i = 0; i < 501; i++) {
                    if (Math.Abs(freqanalysis[i] - mx) < 0.001) { z = i; }
                    // p = p + ";[" + i + "]--->" +  freqanalysis[i] + ";";
                    
                }

             
                  maxpower = mx;
                 offset =  ( z- (freqanalysis.Count() - 1) / 2 )* (driver.Configure.Spectrum.Frequency.Span.Value/ 1000);
                freqdev = offset;
                if(Math.Abs((freqanalysis.Count() - 1) / 2 - z) < boardwdth) {

                    isfreqdev = false;
                }
                else
                {

                    isfreqdev = true;

                }



  


                level = maxpower;


            }
            catch(Exception e) {

              //  System.Windows.Forms.MessageBox.Show(e.ToString());
                level = -10000;
                offset = 10000000000000;
                isfreqdev = true;
                return false;

            }


          


            return true;
        }

        void delay(int ms) {




            System.Threading.Thread.Sleep(ms);
        
        
        
        
        
        }

        ~cmw100_SpectrumAnalyzersNonSCPI() {


            // driver.Spectrum.StopAndWait();
          
            driver.Dispose();

        }

    }

    
}
