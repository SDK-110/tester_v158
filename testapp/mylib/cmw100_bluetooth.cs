using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwBluetoothSig;
using RohdeSchwarz.RsCmwGprfGen;

namespace testapp.mylib
{
    class cmw100_bluetooth_tx
    {
        static RsCmwBluetoothMeas driver = null;
        public cmw100_bluetooth_tx(String resource = "TCPIP0::192.168.8.89::inst0::INSTR")
        {

            driver = new RsCmwBluetoothMeas(resource);



        }
        //CONFigure:BLUetooth:MEASurement<Instance>:DISPlay
        public void DisplayView(DisplayViewEnum p = DisplayViewEnum.OVERview) {


            RsCmwBluetoothMeas_Configure.Display_Data dd = new RsCmwBluetoothMeas_Configure.Display_Data();

            dd.View = p;
            driver.Configure.Display = dd;

        }

        public void set_input_config(double eattenation = 0, double enpower = 0, double umargin = 0, double centerFreq = 2402000000) {

            // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:EATTenuation
            driver.Configure.RfSettings.Eattenuation = eattenation;

            driver.Configure.RfSettings.EnvelopePower = enpower +5;
            // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:UMARgin
            driver.Configure.RfSettings.Umargin = umargin;
            // CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:FREQuency
            driver.Configure.RfSettings.Frequency = centerFreq;

        }

        public void set_input_mode(AutoManualModeEnum mode = AutoManualModeEnum.AUTO,
                                   bool asyn_status = true,
                                   RohdeSchwarz.RsCmwBluetoothMeas.BurstTypeEnum burstty = RohdeSchwarz.RsCmwBluetoothMeas.BurstTypeEnum.LE,
                                   RohdeSchwarz.RsCmwBluetoothMeas.LePhysicalTypeEnum lePacketType = RohdeSchwarz.RsCmwBluetoothMeas.LePhysicalTypeEnum.LE1M,
                                   LePatternTypeEnum lePatternType = LePatternTypeEnum.OTHer,
                                   int plength = 37,
                                   double threshold_value = -70,
                                   RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum repeat_status = RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum.SINGleshot



            )
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
            driver.Configure.InputSignal.Pattern.LowEnergy.Le1m = lePatternType;
            // CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:LENergy[:LE1M]
            driver.Configure.InputSignal.Plength.LowEnergy.Le1m = plength;
            // TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:THReshold
            driver.Trigger.MultiEval.Threshold = threshold_value;
            driver.Configure.MultiEval.Timeout = 5000;

            driver.Configure.MultiEval.Repetition = repeat_status;

            driver.Configure.MultiEval.Scondition = StopConditionEnum.NONE;

            driver_start();
        }

        public void driver_start() {

            driver.MultiEval.Initiate();
        }

        public void driver_stop(){


                driver.MultiEval.Stop();
            }

        public void measure_static_count(int count=10,int type=0) {

       
            if (type == 0) driver.Configure.MultiEval.Scount.PowerVsTime = count;
            if (type == 1) driver.Configure.MultiEval.Scount.Modulation = count;
            if (type == 2) driver.Configure.MultiEval.Scount.SoBw = count;
            if (type == 3) driver.Configure.MultiEval.Scount.Sacp = count;
            if (type == 4) driver.Configure.MultiEval.Scount.Frange = count;
            if (type == 5) driver.Configure.MultiEval.Scount.Pencoding = count;




        }

        public double getTxPowerResult(int powertype=1,int type=1) {

            measure_static_count(10, 0);
            if (type == 0){
                if (powertype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Fetch().LeakagePower;
                if (powertype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Fetch().LeakagePower;
                if (powertype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Fetch().LeakagePower;
                if (powertype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Fetch().LeakagePower;
            }

            if (type ==1)
            {
                if (powertype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Fetch().NominalPower;
                if (powertype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Fetch().NominalPower;
                if (powertype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Fetch().NominalPower;
                if (powertype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Fetch().NominalPower;
            }

            if (type == 2)
            {
                if (powertype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Fetch().PeakPower;
                if (powertype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Fetch().PeakPower;
                if (powertype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Fetch().PeakPower;
                if (powertype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Fetch().PeakPower;
            }

            if (type == 3)
            {
                if (powertype == 0) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Fetch().PeakMinAvgPow;
                if (powertype == 1) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Fetch().PeakMinAvgPow;
                if (powertype == 2) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Fetch().PeakMinAvgPow;
                if (powertype == 3) return driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Fetch().PeakMinAvgPow;
            }


            return -100000;
        }

        public double getTxfreqResult(int type)
        {

            measure_static_count(10, 1);
            return driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Fetch().NominalPower;
         
        }


        ~cmw100_bluetooth_tx() {



            driver.Dispose();

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
        public cmw100_bluetooth_rx(String resource = "TCPIP::localhost::INSTR")
        {

            driver = new RsCmwGprfGen(resource);


        }
        public void bluetooth_ModeSet(int mode)
        {

            if (mode == 0 || mode == 1 || mode == 2)
            {
                driver.Source.List.Value = false;
                BasebandModeEnum bbmode = (BasebandModeEnum)mode;
                driver.Source.BbMode = bbmode;
            }

            if (mode == 3 )
            {
                driver.Source.List.Value = true;
              
            }



        }

        public void set_ware_file(string file)
        {


            driver.Source.Arb.File.Set(file);

        }

        public void set_bluetoothFreq_level_gain(double freq = 2402000000, double levl = -60, double gain = 30)
        {


            driver.Source.RfSettings.Frequency = freq;
            driver.Source.RfSettings.Level = levl;
            driver.Source.RfSettings.Dgain = gain;

        }

        public void bluetooth_static(int cycle=1000, RepeatModeEnum repeatMode=RepeatModeEnum.SINGle) {


            driver.Source.Arb.Cycles = cycle;
            driver.Source.Arb.Repetition = repeatMode;

        }

        public void start_stop_bluetooth(bool status = true)
        {


            driver.Source.State.Set(status);

        }





      
        ~cmw100_bluetooth_rx()
        {



            driver.Dispose();

        }

    }
}
