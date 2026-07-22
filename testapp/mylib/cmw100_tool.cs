using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwWlanMeas;
using RohdeSchwarz.RsCmwLteMeas;
using RohdeSchwarz.RsCmwGprfGen;
using static ReaLTaiizor.Controls.ExtendedPanel;
using log4net.Core;
using Org.BouncyCastle.Ocsp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using RohdeSchwarz.RsCmwGprfMeas;
namespace testapp.mylib
{


    public class cmw100_lte_test {
        RsCmwLteMeas driver = null;
        bool run_ok= false;
        double _tx_power = -9999;
        public cmw100_lte_test(String resource = "TCPIP0::127.0.0.1::inst2::INSTR")
        {
          

            try
            {
                int cout = 1;

                do
                {

                    run_ok = false;

                    Task.Factory.StartNew(() =>
                    {

                        driver = new RsCmwLteMeas(resource, false, false, "VisaTimeout = 3500 ,OpcTimeout = 3500");


                    }).Wait(4000);

                    if (driver != null) break;
                } while (driver == null && cout > 0);


            }
            catch (Exception ex)
            {
                driver = null;
                mylib.utility_func.callbackdebuginfo(ex.ToString());
            }
            if (driver == null) return;

            driver.Configure.Dmode = DuplexModeEnum.FDD;
            driver.Configure.Stype = SignalTypeEnum.UL;
            driver.Configure.Band = (BandEnum)(1);
            driver.Configure.RfSettings.EnvelopePower = 20;
            driver.Configure.RfSettings.Eattenuation = 0;
            driver.Configure.MultiEval.Mmode = RohdeSchwarz.RsCmwLteMeas.MeasurementModeEnum.NORMal;
           
            driver.MultiEval.InitiateAndWait();

            var p = driver.MultiEval.Modulation.Average.Fetch();


        }




    }



    //public class cmw100_Spectrum {

    //    public RsCmwGprfMeas driver = null;
    //    public double max_power;
    //    public double offset;
    //    public double freqrs;
    //    public cmw100_Spectrum(String resource = "TCPIP0::127.0.0.1::inst1::INSTR",
    //                            double centerFreq = 2402000000 /*GHz*/,
    //                            double offset = 0/*dbm*/,
    //                            double span = 10000000/*MHz*/,
    //                            double RBW = 4000000/*KHz*/,
    //                            double VBW = 100000/*KHz*/,
    //                            int sweep = 20,/*sweep points*/
    //                            double swttime = 0.05,
    //                            double envelopepower=10
    //                            )


    //    {
    //        try
    //        {
    //            int cout = 1;

    //            do
    //            {

                

    //            Task.Factory.StartNew(() =>
    //            {

    //                driver = new RsCmwGprfMeas(resource, false, false, "VisaTimeout = 4000 ,OpcTimeout = 4000");


    //            }).Wait(4000);

    //                if (driver != null) break;
    //            } while(driver==null && cout>0 );


    //        }
    //        catch(Exception ex) {
    //            driver = null;
    //            mylib.utility_func.callbackdebuginfo(ex.ToString());
    //        }
    //        if (driver == null) return;
    //      //  driver.Utilities.VisaTimeout = 100000;
    //        // driver.Route.Value.RfConnector = RfConnectorEnum.RF1;
    //        //driver.Route.Value.RfConverter = RohdeSchwarz.RsCmwGprfMeas.RxConverterEnum.RX1; 
    //        driver.Configure.RfSettings.Eattenuation = offset;
    //        driver.Configure.RfSettings.EnvelopePower = envelopepower;
    //        driver.Configure.Display = MeasTabEnum.SPECtrum;
    //        driver.Configure.RfSettings.Frequency = centerFreq;
    //        driver.Configure.Spectrum.Frequency.Span.Value = span;
    //        driver.Configure.Spectrum.FreqSweep.Vbw.Value = VBW;
    //        driver.Configure.Spectrum.FreqSweep.Rbw.Value = RBW;
    //        // driver.Configure.Spectrum.FreqSweep.Swt.Auto=true;
    //        driver.Configure.Spectrum.Scount = sweep;
    //        driver.Configure.Spectrum.Timeout = 5;
    //        driver.Configure.Spectrum.Amode = AveragingModeEnum.LINear;
    //        driver.Configure.Spectrum.Repetition = RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.SINGleshot;   //RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.CONTinuous;
    //                                                                                                   //  driver.Route.Value.RfConnector = RFConnectorEnum.RF3;
    //        driver.Configure.Spectrum.FreqSweep.Swt.Auto = false;
    //        driver.Configure.Spectrum.FreqSweep.Swt.Value = swttime;
    //        driver.Spectrum.InitiateAndWait();
    //      //  System.Threading.Thread.Sleep(1500);
    //        driver.Utilities.QueryOpc();
    //        List<double> restlt = driver.Spectrum.Average.Maximum.Fetch();
    //        double max = -100;
    //        int max_num = 0;
    //        for (int i = 0; i < restlt.Count; i++) { 
            
    //        if(max < restlt[i]) { max = restlt[i]; max_num = i; }
            
    //        }

    //        double offset_rsu =( Math.Abs(restlt.Count / 2 + 1 - restlt[max_num]) * span / 1000.000) ;
    //        double freq = (centerFreq - span / 2) + (max_num * span / 1000);
    //        max_power = max;
    //        offset = offset_rsu;
    //        freqrs = freq;
    //        driver.Dispose();
    //    }

    //     ~cmw100_Spectrum()
    //    {
    //        if (driver != null) driver.Dispose();
    //    }




    //}






    public static class cmw100_Spectrum
    {
      static public bool run_ok = false;
     static   public RsCmwGprfMeas driver = null;
        static public double max_power;
        static public double offset;
        static public double freqrs;
        static public void _cmw100_Spectrum(String resource = "TCPIP0::127.0.0.1::inst1::INSTR",
                                double centerFreq = 2402000000 /*GHz*/,
                                double _offset = 0/*dbm*/,
                                double span = 10000000/*MHz*/,
                                double RBW = 4000000/*KHz*/,
                                double VBW = 100000/*KHz*/,
                                int sweep = 20,/*sweep points*/
                                double swttime = 0.05,
                                double envelopepower = 10
                                )


        {
            try
            {
                run_ok = false;
                max_power = double.NaN;
               
                freqrs = double.NaN;

                driver = new RsCmwGprfMeas(resource, false, false, "VisaTimeout = 4000 ,OpcTimeout = 4000,ReadDelay = 5");
                //  driver.Utilities.VisaTimeout = 100000;
                // driver.Route.Value.RfConnector = RfConnectorEnum.RF1;
                //driver.Route.Value.RfConverter = RohdeSchwarz.RsCmwGprfMeas.RxConverterEnum.RX1; 
                driver.Configure.RfSettings.Eattenuation = _offset;
                driver.Configure.RfSettings.EnvelopePower = envelopepower;
                driver.Configure.Display = MeasTabEnum.SPECtrum;
                driver.Configure.RfSettings.Frequency = centerFreq;
                driver.Configure.Spectrum.Frequency.Span.Value = span;
                driver.Configure.Spectrum.FreqSweep.Vbw.Value = VBW;
                driver.Configure.Spectrum.FreqSweep.Rbw.Value = RBW;
                // driver.Configure.Spectrum.FreqSweep.Swt.Auto=true;
                driver.Configure.Spectrum.Scount = sweep;
                driver.Configure.Spectrum.Timeout = 5;
                driver.Configure.Spectrum.Amode = AveragingModeEnum.LINear;
                driver.Configure.Spectrum.Repetition = RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.SINGleshot;   //RohdeSchwarz.RsCmwGprfMeas.RepeatEnum.CONTinuous;
                                                                                                           //  driver.Route.Value.RfConnector = RFConnectorEnum.RF3;
                driver.Configure.Spectrum.FreqSweep.Swt.Auto = false;
                driver.Configure.Spectrum.FreqSweep.Swt.Value = swttime;
                driver.Spectrum.InitiateAndWait();
                //  System.Threading.Thread.Sleep(1500);
                driver.Utilities.QueryOpc();
                List<double> restlt = driver.Spectrum.Average.Maximum.Fetch();
                double max = -100;
                int max_num = 0;
                for (int i = 0; i < restlt.Count; i++)
                {

                    if (max < restlt[i]) { max = restlt[i]; max_num = i; }

                }

                double offset_rsu = (Math.Abs(restlt.Count / 2 + 1 - restlt[max_num]) * span / 1000.000);
                double freq = (centerFreq - span / 2) + (max_num * span / 1000);
                max_power = max;
                offset = offset_rsu;
                freqrs = freq;
                run_ok = true;

            }
            catch (Exception ex)
            {
                driver = null;
                mylib.utility_func.callbackdebuginfo(ex.ToString());
               max_power = double.NaN;
               
                freqrs = double.NaN;
            }
            
            finally
            {
                driver?.Dispose();

            }
           
           
        }
}




    



    //public class cmw100_wv_generator {

    //   public RsCmwGprfGen driver = null;
    //    public cmw100_wv_generator(String resource = "TCPIP0::127.0.0.1::inst2::INSTR", string wv_file= "BT_LE_TestPacket.wv", double freq=2442000000, double levl=-30, double gain=0,
    //                                int cycle=1500)
    //    {

    //        string file = $"C:\\ProgramData\\Rohde-Schwarz\\CMW\\Data\\waveform\\{wv_file}";
    //        int count = 1;
    //        do
    //        {
    //            try
    //            {
    //                Task.Factory.StartNew(() =>
    //                {
    //                    driver = new RsCmwGprfGen(resource, false, false, "VisaTimeout = 1500 ,OpcTimeout = 1500"); ;
    //                   // driver.Utilities.VisaTimeout = 4000;
    //                  //  driver.Utilities.OpcTimeout = 4000;

    //                }).Wait(4000);
                   
    //            }
    //            catch (Exception e){
    //                driver = null;
    //                utility_func.callbackdebuginfo(e.ToString());

    //            }
    //            if (driver != null) break;

               
    //        } while (count-- > 0);
    //        if (driver == null) return;
    //      //  driver.Utilities.VisaTimeout = 5000;
    //        driver.Source.BbMode = BasebandModeEnum.ARB;
    //        driver.Source.Arb.File.Set(file);
    //        driver.Source.RfSettings.Frequency = freq;
    //        driver.Source.RfSettings.Level = levl;
    //        driver.Source.RfSettings.Dgain = gain;
    //        driver.Source.Arb.Cycles = cycle;
    //        driver.Source.Arb.Repetition = RepeatModeEnum.SINGle;
    //        driver.Source.State.Set(true);
    //    }

    //    ~cmw100_wv_generator()
    //    {
    //        if(driver!=null)driver.Dispose();
    //    }

    //}

    public static class cmw100_wv_generator
    {
        static public bool run_ok = false;
        static public RsCmwGprfGen driver = null;
        static public void  _cmw100_wv_generator(String resource = "TCPIP0::127.0.0.1::inst2::INSTR", string wv_file = "BT_LE_TestPacket.wv", double freq = 2442000000, double levl = -30, double gain = 0,
                                    int cycle = 1500)
        {

            string file = $"C:\\ProgramData\\Rohde-Schwarz\\CMW\\Data\\waveform\\{wv_file}";
            int count = 1;
        
                try
                {
                 run_ok = false;
                driver = new RsCmwGprfGen(resource, false, false, "VisaTimeout = 1500 ,OpcTimeout = 1500,ReadDelay = 5"); ;
                //  driver.Utilities.VisaTimeout = 5000;
                driver.Source.BbMode = BasebandModeEnum.ARB;
                driver.Source.Arb.File.Set(file);
                driver.Source.RfSettings.Frequency = freq;
                driver.Source.RfSettings.Level = levl;
                driver.Source.RfSettings.Dgain = gain;
                driver.Source.Arb.Cycles = cycle;
                driver.Source.Arb.Repetition = RepeatModeEnum.SINGle;
                driver.Source.State.Set(true);
                run_ok = true;

            }
                catch (Exception e)
                {
                    driver = null;
                    utility_func.callbackdebuginfo(e.ToString());

                }finally
                {
                   driver?.Dispose();
                }


         
        }

    }



//    public class cmw100_wlan_test
//    {
//        public static RsCmwWlanMeas driver = null;
//        double _tx_power = -9999;
//        double _offset = -9999999999;
//        double _evm_peak = 999999;
//        public cmw100_wlan_test(String resource = "TCPIP0::127.0.0.1::inst0::INSTR",int ch=1,double eattenuation=0)
//        {
//            int count = 1;
//            do
//            {
//                try
//                {

              
//                Task.Factory.StartNew(() =>
//                {
//                    driver = new RsCmwWlanMeas(resource, false, false, "VisaTimeout = 5000 ,OpcTimeout = 5000"); ;
//                    //driver.Utilities.VisaTimeout = 4000;
//                  //  driver.Utilities.OpcTimeout = 4000;

//                }).Wait(5500);

//                }
//                catch (Exception ex)
//                {
//                    driver = null;
//                    utility_func.callbackdebuginfo(ex.ToString());
//                }
//                if (driver != null) break;
//            } while (count-- > 0);
//            if (driver == null) return;

            
//         //   driver.Utilities.VisaTimeout = 5000;


//            //  driver.Configure.RfSettings.Frequency.Value = 2442000000;

//            driver.Configure.RfSettings.Frequency.Band = FrequencyBandEnum.B24Ghz;

//            driver.Configure.RfSettings.Frequency.Channels.Set(ch);


//            driver.Configure.RfSettings.Umargin.Set(0);

//            driver.Configure.RfSettings.Eattenuation.Set(eattenuation);
//            driver.Configure.RfSettings.EnvelopePower.Set(23);

//            driver.Configure.RfSettings.MlOffset = 0;
//            //driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;

//            driver.Configure.Isignal.Standard = IeeeStandardEnum.DSSS;

//            // driver.MultiEval.Modulation
//            // driver.Route.Value.Scenario = MimoScenarioEnum.SALone;
//            //  driver.Configure.MultiEval.PowerVsTime.Burst = true;
//            driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.CONTinuous;
//            driver.Configure.MultiEval.Scount.Modulation = 50;
//            driver.Configure.MultiEval.Scount.PowerVsTime = 50;
//for(int i = 0; i < 10; i++) { 
//            driver.MultiEval.InitiateAndWait();
//            driver.MultiEval.StopAndWait();
//            var p = driver.MultiEval.Modulation.Dsss.Average.Read();
//            //driver.Configure.MultiEval.PowerVsTime.Burst
//            _tx_power = p.BurstPower;
//           //  p = driver.MultiEval.Modulation.Dsss.Current.Read();

//            //driver.Configure.MultiEval.PowerVsTime.Burst
//             _evm_peak= p.EvmPeak;



//            //driver.Configure.MultiEval.PowerVsTime.Burst
//           _offset =  p.FreqError;

//                if (_tx_power != Double.NaN && _evm_peak != double.NaN &&_offset!=double.NaN ) break;
//            }

//            driver.Dispose();
//        }
        
//        public double txpower => _tx_power;
//        public double offset => _offset;
//        public double evm_peak => _evm_peak;

       
//        ~cmw100_wlan_test()
//        {
//            if (driver != null) driver.Dispose();
//        }
//    }

    public static class cmw100_wlan_test
    {
        static public bool run_ok = false;
        public static RsCmwWlanMeas driver = null;
        static double _tx_power = double.NaN;
        static double _offset = double.NaN;
        static double _evm_peak = double.NaN;
      static  public void  _cmw100_wlan_test(String resource = "TCPIP0::127.0.0.1::inst0::INSTR", int ch = 1, double eattenuation = 0)
        {
            run_ok = false;
            _tx_power = double.NaN;
            _offset = double.NaN;
            _evm_peak = double.NaN;
            try
                {


              
                        driver = new RsCmwWlanMeas(resource, false, false, "VisaTimeout = 5000 ,OpcTimeout = 5000 , ReadDelay = 5"); ;
                    //driver.Utilities.VisaTimeout = 4000;
                    //  driver.Utilities.OpcTimeout = 4000;
                    //   driver.Utilities.VisaTimeout = 5000;


                    //  driver.Configure.RfSettings.Frequency.Value = 2442000000;

                    driver.Configure.RfSettings.Frequency.Band = FrequencyBandEnum.B24Ghz;

                    driver.Configure.RfSettings.Frequency.Channels.Set(ch);


                    driver.Configure.RfSettings.Umargin.Set(0);

                    driver.Configure.RfSettings.Eattenuation.Set(eattenuation);
                    driver.Configure.RfSettings.EnvelopePower.Set(23);

                    driver.Configure.RfSettings.MlOffset = 0;
                    //driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;

                    driver.Configure.Isignal.Standard = IeeeStandardEnum.DSSS;

                    // driver.MultiEval.Modulation
                    // driver.Route.Value.Scenario = MimoScenarioEnum.SALone;
                    //  driver.Configure.MultiEval.PowerVsTime.Burst = true;
                    driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.CONTinuous;
                    driver.Configure.MultiEval.Scount.Modulation = 50;
                    driver.Configure.MultiEval.Scount.PowerVsTime = 50;
                    for (int i = 0; i < 10; i++)
                    {
                        driver.MultiEval.InitiateAndWait();
                        driver.MultiEval.StopAndWait();
                        var p = driver.MultiEval.Modulation.Dsss.Average.Read();
                        //driver.Configure.MultiEval.PowerVsTime.Burst
                        _tx_power = p.BurstPower;
                        //  p = driver.MultiEval.Modulation.Dsss.Current.Read();

                        //driver.Configure.MultiEval.PowerVsTime.Burst
                        _evm_peak = p.EvmPeak;



                        //driver.Configure.MultiEval.PowerVsTime.Burst
                        _offset = p.FreqError;

                        if (_tx_power != Double.NaN && _evm_peak != double.NaN && _offset != double.NaN) break;
                    }

                    run_ok = true;

            }
                catch (Exception ex)
                {
                    driver = null;
                    utility_func.callbackdebuginfo(ex.ToString());
                }
                finally
                {
                     driver?.Dispose();
                }
         
           
        }

      static  public double txpower => _tx_power;
        static public double offset => _offset;
        static public double evm_peak => _evm_peak;


    }


    //public class cmw100_wlan_test_N_mode
    //{
    //    public static RsCmwWlanMeas driver = null;
    //    double _tx_power = -9999;
    //    double _offset = -9999999999;
    //    double _evm_peak = 999999;
    //    public cmw100_wlan_test_N_mode(String resource = "TCPIP0::127.0.0.1::inst0::INSTR", int ch = 1, double eattenuation = 0)
    //    {

    //        int count = 1;
    //        do
    //        {
    //            try
    //            {
    //                Task.Factory.StartNew(() =>
    //                {

    //                    driver = new RsCmwWlanMeas(resource, false, false, "VisaTimeout = 3500 ,OpcTimeout = 3500"); ;
                    

    //                }).Wait(4000);

    //            }
    //            catch(Exception ex) {

    //                mylib.utility_func.callbackdebuginfo(ex.ToString());
    //                driver = null;
    //            }
    //            if (driver != null) break;

    //        } while (count-- > 0);
    //        //    driver.Utilities.VisaTimeout = 5000;

    //        if (driver == null) return;
    //        //  driver.Configure.RfSettings.Frequency.Value = 2442000000;

    //        driver.Configure.RfSettings.Frequency.Band = FrequencyBandEnum.B24Ghz;

    //        driver.Configure.RfSettings.Frequency.Channels.Set(ch);


    //        driver.Configure.RfSettings.Umargin.Set(0);

    //        driver.Configure.RfSettings.Eattenuation.Set(eattenuation);
    //        driver.Configure.RfSettings.EnvelopePower.Set(25);

    //        driver.Configure.RfSettings.MlOffset = 0;
    //        //driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;

    //        driver.Configure.Isignal.Standard = IeeeStandardEnum.HTOFdm;

    //        // driver.MultiEval.Modulation
    //        // driver.Route.Value.Scenario = MimoScenarioEnum.SALone;
    //        //  driver.Configure.MultiEval.PowerVsTime.Burst = true;
    //        driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;
    //        driver.Configure.MultiEval.Scount.Modulation = 100;
    //        driver.Configure.MultiEval.Scount.PowerVsTime = 100;
    //        for (int i = 0; i < 10; i++)
    //        {
    //            driver.MultiEval.InitiateAndWait();
    //            driver.MultiEval.StopAndWait();
    //            var p = driver.MultiEval.Modulation.Ofdm.Average.Read();
    //            //driver.Configure.MultiEval.PowerVsTime.Burst
    //            _tx_power = p.BurstPower;
    //            //  p = driver.MultiEval.Modulation.Dsss.Current.Read();

    //            //driver.Configure.MultiEval.PowerVsTime.Burst
    //            _evm_peak = p.EvmAllCarr;



    //            //driver.Configure.MultiEval.PowerVsTime.Burst
    //            _offset = p.FreqError;

    //            if (_tx_power != Double.NaN && _evm_peak != double.NaN&& _offset !=Double.NaN) break;
    //        }

    //        driver.Dispose();
    //    }

    //    public double txpower => _tx_power;
    //    public double offset => _offset;
    //    public double evm_peak => _evm_peak;

    //    ~cmw100_wlan_test_N_mode()
    //    {
    //        if (driver != null) driver.Dispose();
    //    }
    //}



    public static class cmw100_wlan_test_N_mode
    {
        static public bool run_ok = false;
        static public  RsCmwWlanMeas driver = null;
        static double _tx_power = double.NaN;
        static double _offset = double.NaN;
        static double _evm_peak = double.NaN;
     static  public void _cmw100_wlan_test_N_mode(String resource = "TCPIP0::127.0.0.1::inst0::INSTR", int ch = 1, double eattenuation = 0)
        {

           _tx_power = double.NaN;
           _offset = double.NaN;
           _evm_peak = double.NaN;
            try
            {
              run_ok = false;
                driver = new RsCmwWlanMeas(resource, false, false, "VisaTimeout = 3500 ,OpcTimeout = 3500,ReadDelay = 5"); ;
              driver.Configure.RfSettings.Frequency.Band = FrequencyBandEnum.B24Ghz;

                driver.Configure.RfSettings.Frequency.Channels.Set(ch);


                driver.Configure.RfSettings.Umargin.Set(0);

                driver.Configure.RfSettings.Eattenuation.Set(eattenuation);
                driver.Configure.RfSettings.EnvelopePower.Set(25);

                driver.Configure.RfSettings.MlOffset = 0;
                //driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;

                driver.Configure.Isignal.Standard = IeeeStandardEnum.HTOFdm;

                // driver.MultiEval.Modulation
                // driver.Route.Value.Scenario = MimoScenarioEnum.SALone;
                //  driver.Configure.MultiEval.PowerVsTime.Burst = true;
                driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwWlanMeas.RepeatEnum.SINGleshot;
                driver.Configure.MultiEval.Scount.Modulation = 100;
                driver.Configure.MultiEval.Scount.PowerVsTime = 100;
                for (int i = 0; i < 10; i++)
                {
                    driver.MultiEval.InitiateAndWait();
                    driver.MultiEval.StopAndWait();
                    var p = driver.MultiEval.Modulation.Ofdm.Average.Read();
                    //driver.Configure.MultiEval.PowerVsTime.Burst
                    _tx_power = p.BurstPower;
                    //  p = driver.MultiEval.Modulation.Dsss.Current.Read();

                    //driver.Configure.MultiEval.PowerVsTime.Burst
                    _evm_peak = p.EvmAllCarr;



                    //driver.Configure.MultiEval.PowerVsTime.Burst
                    _offset = p.FreqError;

                    if (_tx_power != Double.NaN && _evm_peak != double.NaN && _offset != Double.NaN) break;
                }
                run_ok= true;
            }
            catch (Exception ex)
            {

                mylib.utility_func.callbackdebuginfo(ex.ToString());
                driver = null;
            }
            finally { 
            
             driver?.Dispose();
            }
           
            //  driver.Configure.RfSettings.Frequency.Value = 2442000000;

           

           
        }

     static   public double txpower => _tx_power;
        static public double offset => _offset;
        static public double evm_peak => _evm_peak;

   
    }
    public static class cmw100_bluetooth_tx_pycom
    {
        static public bool run_ok = false;
        static double _tx_power= double.NaN;
        static double _offset= double.NaN;
        static public  RsCmwBluetoothMeas driver = null;
        static public void _cmw100_bluetooth_tx_pycom(String resource = "TCPIP0::127.0.0.1::inst0::INSTR",double Eattenuation=1,double freq = 2402000000)
        {
           
                try
                {
                    run_ok = false;
                _tx_power = double.NaN;
                _offset = double.NaN;
                driver = new RsCmwBluetoothMeas(resource, false, false, "VisaTimeout = 5000 ,OpcTimeout = 5000,ReadDelay = 5");
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:EATTenuation
                driver.Configure.RfSettings.Eattenuation = Eattenuation;

                driver.Configure.RfSettings.EnvelopePower = 25;
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:UMARgin
                driver.Configure.RfSettings.Umargin = 0;
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:FREQuency
                driver.Configure.RfSettings.Frequency = freq;
                driver.Configure.InputSignal.Dmode = AutoManualModeEnum.MANual;
                // driver.Configure.InputSignal.BdAddress = "#H0717664129";
                driver.Configure.InputSignal.Asynchronize = true;

                driver.Configure.InputSignal.Btype = RohdeSchwarz.RsCmwBluetoothMeas.BurstTypeEnum.LE;
                driver.Configure.InputSignal.LowEnergy.Phy = LePhysicalTypeEnum.LE1M;
                driver.Configure.InputSignal.LowEnergy.SynWord = "#H071764129";
                //driver.Configure.InputSignal.Ptype.Brate = BrPacketTypeEnum.DH5;
                driver.Configure.MultiEval.Repetition = RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum.CONTinuous;
                for (int i = 0; i < 10; i++)
                {
                    driver.MultiEval.InitiateAndWait();
                    driver.MultiEval.StopAndWait();
                    _tx_power = driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Read().NominalPower;
                    _offset = driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Read().FreqAccuracy;

                    if (_tx_power != Double.NaN && _offset != Double.NaN) break;
                }
                run_ok = true;
            }
                catch (Exception e) {
                    utility_func.callbackdebuginfo(e.ToString());
                    driver = null;
                }
            finally {
            
            driver?.Dispose();
            }
         
           

        }

       static  public double txpower => _tx_power;
       static  public double offset => _offset;

 
    }

 


}

