using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwBluetoothSig;
using RohdeSchwarz.RsCmwGprfGen;

using RohdeSchwarz.RsCmwBase;
using BurstTypeEnum = RohdeSchwarz.RsCmwBluetoothSig.BurstTypeEnum;
using LeRangePaternTypeEnum = RohdeSchwarz.RsCmwBluetoothSig.LeRangePaternTypeEnum;
using RepetitionEnum = RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum;
using RxConnectorEnum = RohdeSchwarz.RsCmwBluetoothSig.RxConnectorEnum;
using RxConverterEnum = RohdeSchwarz.RsCmwBluetoothSig.RxConverterEnum;
using TxConnectorEnum = RohdeSchwarz.RsCmwBluetoothSig.TxConnectorEnum;
using TxConverterEnum = RohdeSchwarz.RsCmwBluetoothSig.TxConverterEnum;

namespace testapp.mylib
{
    class cmw100_bluetooth_tx
    {
        static RsCmwBluetoothMeas driver = null;
        public cmw100_bluetooth_tx(String resource = "TCPIP0::127.0.0.1::inst0::INSTR")
        {
            int count = 3;
            do
            {
                // driver = new RsCmwBluetoothMeas(resource, false, true);
                //driver = new RsCmwBluetoothMeas(resource);
                driver = new RsCmwBluetoothMeas(resource, false, true);
                if (driver != null) break;
                System.Threading.Thread.Sleep(500);
                
            } while (count-- > 0);
          

            

        }
        //CONFigure:BLUetooth:MEASurement<Instance>:DISPlay
        public int DisplayView(DisplayViewEnum p = DisplayViewEnum.OVERview) {

            try
            {
                RsCmwBluetoothMeas_Configure.Display_Data dd = new RsCmwBluetoothMeas_Configure.Display_Data();

                dd.View = p;
                driver.Configure.Display = dd;
                System.Threading.Thread.Sleep(50);
                return 1;

            }
            catch {

                return -1;
            }









        }

        public int  set_input_config(double eattenation = 0, double enpower = 0, double umargin = 0, double centerFreq = 2442000000) {


            try
            {
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:EATTenuation
                driver.Configure.RfSettings.Eattenuation = eattenation;

                driver.Configure.RfSettings.EnvelopePower = enpower;
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:UMARgin
                driver.Configure.RfSettings.Umargin = umargin;
                // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:FREQuency
                driver.Configure.RfSettings.Frequency = centerFreq;
                return 1;
            }
            catch {

                return -1;
            }

        }

        public int set_input_mode(AutoManualModeEnum mode = AutoManualModeEnum.AUTO,
                                   bool asyn_status = true,
                                   RohdeSchwarz.RsCmwBluetoothMeas.BurstTypeEnum burstty = RohdeSchwarz.RsCmwBluetoothMeas.BurstTypeEnum.LE,
                                   RohdeSchwarz.RsCmwBluetoothMeas.LePhysicalTypeEnum lePacketType = RohdeSchwarz.RsCmwBluetoothMeas.LePhysicalTypeEnum.LE1M,
                                   LePatternTypeEnum lePatternType = LePatternTypeEnum.OTHer,
                                   int plength = 37,
                                   double threshold_value = -40,
                                   RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum repeat_status = RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum.SINGleshot



            )
        {
            try
            {

                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:DMODe
                driver.Configure.InputSignal.Dmode = mode;
                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:ASYNchronize
                driver.Configure.InputSignal.Asynchronize = asyn_status;
                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:BTYPe
                driver.Configure.InputSignal.Btype = burstty;
                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:LENergy:PHY
                driver.Configure.InputSignal.LowEnergy.Phy = lePacketType;
                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PATTern:LENergy[:LE1M]
                driver.Configure.InputSignal.Pattern.LowEnergy.Le1M = lePatternType;
                // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:LENergy[:LE1M]
                driver.Configure.InputSignal.Plength.LowEnergy.Le1M = plength;
                // TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:THReshold
                driver.Trigger.MultiEval.Threshold = threshold_value;
                driver.Configure.MultiEval.Timeout = 5000;

                driver.Configure.MultiEval.Repetition = repeat_status;

                driver.Configure.MultiEval.Scondition = StopConditionEnum.NONE;

              
                return 1;
            }
            catch {

                return -1;
            }
        }

        public void driver_start() {

            driver.MultiEval.Initiate();
        }

        public void driver_stop(){

            if (driver != null) try { driver.MultiEval.Stop(); } catch { }
            }

        public void measure_static_count(int count=10,int type=0) {


            if (type == 0) { driver.Configure.MultiEval.Scount.PowerVsTime = count; return; }
            if (type == 1){ driver.Configure.MultiEval.Scount.Modulation = count; return; }
            if (type == 2) {driver.Configure.MultiEval.Scount.SoBw = count; ; return;}
            if (type == 3) { driver.Configure.MultiEval.Scount.Sacp = count; return; }
            if (type == 4) { driver.Configure.MultiEval.Scount.Frange = count; return; }
            if (type == 5) { driver.Configure.MultiEval.Scount.Pencoding = count; return; }




        }

        public double getTxPowerResult(int power_freq_accuraytype =1/*0是freq，1是 offset*/,int counttype = 2) {

            measure_static_count(10, (power_freq_accuraytype==0)?0:1);
            if (power_freq_accuraytype == 0){
                if (counttype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Current.Fetch().NominalPower;
                if (counttype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Average.Fetch().NominalPower;
                if (counttype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Maximum.Fetch().LeakagePower;
                if (counttype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Minimum.Fetch().LeakagePower;
            }

            if (power_freq_accuraytype == 1)
            {
                if (counttype == 0) return driver.MultiEval.Modulation.LowEnergy.Le1M.Current.Fetch().FreqAccuracy;
                if (counttype == 1) return driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Fetch().FreqAccuracy;
                if (counttype == 2) return driver.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Fetch().FreqAccuracy;
                if (counttype == 3) return driver.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Fetch().FreqAccuracy;
            }

            if (power_freq_accuraytype == 2)
            {
                if (counttype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Current.Fetch().NominalPower;
                if (counttype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Average.Fetch().LeakagePower;
                if (counttype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Maximum.Fetch().LeakagePower;
                if (counttype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Minimum.Fetch().LeakagePower;
            }

            if (power_freq_accuraytype == 3)
            {
                if (counttype == 0) return driver.MultiEval.Modulation.LowEnergy.Le2M.Current.Fetch().FreqAccuracy;
                if (counttype == 1) return driver.MultiEval.Modulation.LowEnergy.Le2M.Average.Fetch().FreqAccuracy;
                if (counttype == 2) return driver.MultiEval.Modulation.LowEnergy.Le2M.Maximum.Fetch().FreqAccuracy;
                if (counttype == 3) return driver.MultiEval.Modulation.LowEnergy.Le2M.Maximum.Fetch().FreqAccuracy;
            }


            return -100000;
        }

        private double [] getTxfreq_offsetResult( )
        {

          
            double power = getTxPowerResult(0,1);
          
            double offset = getTxPowerResult(1, 1);

            return new double []{ power,offset };


        }




        public int getTxfreq_offsetResult(out double[] m,double centerfreq = 2442000000, double Eattenuation = 0 , double EnvelopePower=10, double threshold_value = -50) {


            m = new double[] {8888,8888 };

            if( this.DisplayView() < 0 ) return -1;
            if (set_input_config(eattenation: Eattenuation, enpower: EnvelopePower, centerFreq: centerfreq) < 0) return -1;
            if (set_input_mode(threshold_value:threshold_value)<0) return -1;
            driver_start();
            m =   this.getTxfreq_offsetResult();
            return 1;


        }





        ~cmw100_bluetooth_tx() {



            if(driver!=null) driver.Dispose();

        }

    }
    enum MyEnum
    {
        CW = 0,
        Dwal_tone = 1,
        ARB = 2,
        LIST = 3

    }
    class cmw100_bluetooth_rx
    {
        static RsCmwGprfGen driver= null;
        public cmw100_bluetooth_rx(String resource = "TCPIP0::127.0.0.1::inst0::INSTR")
        {

            
            int count = 3;
            do
            {
                // driver = new RsCmwBluetoothMeas(resource, false, true);
                driver = new RsCmwGprfGen(resource, false, true);
                if (driver != null) break;
                System.Threading.Thread.Sleep(500);

            } while (count-- > 0);

        }
       private int bluetooth_ModeSet(int mode)
        {

            try
            {
                if (mode == 0 || mode == 1 || mode == 2)
                {
                    driver.Source.List.Value = false;
                    BasebandModeEnum bbmode = (BasebandModeEnum)mode;
                    driver.Source.BbMode = bbmode;
                }

                if (mode == 3)
                {
                    driver.Source.List.Value = true;

                }

                return 1;
            }
            catch {

                return -1;
            
            }



        }

        private int set_ware_file(string file)
        {

            try
            {
                driver.Source.Arb.File.Set(file);

                return 1;

            }
            catch {

                return -1;
            }

        }

        private int  set_bluetoothFreq_level_gain(double freq = 2442000000, double levl = -80, double gain = 0)
        {

            try
            {
                driver.Source.RfSettings.Frequency = freq;
                driver.Source.RfSettings.Level = levl;
                driver.Source.RfSettings.Dgain = gain;

                return 1;
            }
            catch {

                return -1;
            }

        }

        private int bluetooth_static(int cycle=1500, RepeatModeEnum repeatMode=RepeatModeEnum.SINGle) {

            try
            {
                driver.Source.Arb.Cycles = cycle;
                driver.Source.Arb.Repetition = repeatMode;
                return 1;
            }
            catch {

                return -1;

            }

        }

       private int start_stop_bluetooth(bool status = true)
        {

            try
            {
                driver.Source.State.Set(status);
                return 1;
            }
            catch {

                return -1;
            }



            }





        public int setsignal_general(int mode=0,double cfreq=2442,double level=-60,double gain=0,int cyclecout=1500) {

            if(bluetooth_ModeSet(0)<=0) return -1;
            if (set_ware_file(@"C:\ProgramData\Rohde-Schwarz\CMW\Data\waveform\BT_LE_TestPacket.wv") <= 0) return -1; ;
            if (set_bluetoothFreq_level_gain(freq:cfreq,levl:level,gain:gain) <= 0) return -1;
            if (bluetooth_static(cycle:cyclecout) <= 0) return -1;
            System.Threading.Thread.Sleep(1000);
            start_stop_bluetooth(true);
            return 1;

        }






      
        ~cmw100_bluetooth_rx()
        {
            try
            {

                if (driver != null)
                {
                    if (driver != null) driver.Dispose();

                }

            }
            catch { 
            
            
            }
            

        }

    }
}
