using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBase;
using RohdeSchwarz.RsCmwBluetoothMeas;
using RohdeSchwarz.RsCmwBluetoothSig;
using BurstTypeEnum = RohdeSchwarz.RsCmwBluetoothSig.BurstTypeEnum;
using LeRangePaternTypeEnum = RohdeSchwarz.RsCmwBluetoothSig.LeRangePaternTypeEnum;
using RepetitionEnum = RohdeSchwarz.RsCmwBluetoothMeas.RepeatEnum;
using RxConnectorEnum = RohdeSchwarz.RsCmwBluetoothSig.RxConnectorEnum;
using RxConverterEnum = RohdeSchwarz.RsCmwBluetoothSig.RxConverterEnum;
using TxConnectorEnum = RohdeSchwarz.RsCmwBluetoothSig.TxConnectorEnum;
using TxConverterEnum = RohdeSchwarz.RsCmwBluetoothSig.TxConverterEnum;

namespace RsCmwBase_and_RsCmwBluetooth
{
    class Program
    {
        static void Main()
        {
            // Open new VISA session for the Base driver
            var cmwBase = new RsCmwBase("TCPIP::10.122.3.185::INSTR", false, true);

            // For the other drivers, reuse the same VISA session
            var cmwBtMeas = new RsCmwBluetoothMeas(cmwBase.Session);
            var cmwBtSig = new RsCmwBluetoothSig(cmwBase.Session);

            Console.WriteLine($"CMW Base IDN '{cmwBase.Utilities.Identification.IdnString}'");
            Console.WriteLine($"CMW Btm  IDN '{cmwBtMeas.Utilities.Identification.IdnString}'");
            Console.WriteLine($"CMW Bts  IDN '{cmwBtSig.Utilities.Identification.IdnString}'");

            // Handling of the return value Reliability is through a driver interface Reliability:
            cmwBase.Reliability.ExceptionOnError = true;

            // Register a callback, which is a Console.WriteLine:
            // This way we are informed about each change in the reliability
            cmwBase.Reliability.Updated += (sender, eventArgs) =>
                Console.WriteLine($"Base Reliability updated.\nContext: {eventArgs.Context}\nMessage: {eventArgs.Message}");

            cmwBtSig.Reliability.Updated += (sender, eventArgs) =>
                Console.WriteLine($"BtSig Reliability updated.\nContext: {eventArgs.Context}\nMessage: {eventArgs.Message}");

            cmwBtMeas.Reliability.Updated += (sender, eventArgs) =>
                Console.WriteLine($"BtMeas Reliability updated.\nContext: {eventArgs.Context}\nMessage: {eventArgs.Message}");

            // You can obtain the last value of the returned reliability
            Console.WriteLine($"\nReliability last value: {cmwBase.Reliability.LastValue}, context '{cmwBase.Reliability.LastContext}', message: {cmwBase.Reliability.LastMessage}");

            var data = cmwBase.Ipc.Result.Fetch();
            Console.WriteLine($"\nReliability last value: {cmwBase.Reliability.LastValue}, context '{cmwBase.Reliability.LastContext}', message: {cmwBase.Reliability.LastMessage}");

            //System settings
            cmwBase.Utilities.OpcQueryAfterEachSetting = true;
            cmwBtMeas.Utilities.OpcQueryAfterEachSetting = true;
            cmwBtSig.Utilities.OpcQueryAfterEachSetting = true;
            cmwBase.System.Reference.Frequency.Source = SourceIntExtEnum.INTernal;

            //Routing
            cmwBtMeas.Route.Scenario.Cspath = "Bluetooth SIG1";

            cmwBtSig.Route.Scenario.OtRx.Value = new RsCmwBluetoothSig_Route_Scenario_OtRx.Value_Data()
            {
                RxConnector = RxConnectorEnum.RF1C,
                RxConverter = RxConverterEnum.RX1,
                TxConnector = TxConnectorEnum.RF1C,
                TxConverter = TxConverterEnum.TX1
            };

            var cmwBtSigRfSettings = cmwBtSig.Configure.RfSettings;
            cmwBtSigRfSettings.Eattenuation.Output = 2;
            cmwBtSigRfSettings.Eattenuation.Input = 2;
            cmwBtSigRfSettings.Level = -40;
            cmwBtSigRfSettings.EnvelopePower = 10;
            cmwBtSigRfSettings.Aranging = false;
            cmwBtSig.Source.State = true;
            cmwBtSig.Diagnostic.Delay.Ptimeout = 1;

            cmwBtSig.Configure.Connection.Btype = BurstTypeEnum.LE;
            cmwBtSigRfSettings.Channel.DtMode = 0;
            cmwBtSig.Configure.Connection.Packets.PacketLength.LowEnergy.Le1m = 37;
            cmwBtSig.Configure.HwInterface.Set(HwInterfaceEnum.RS232);
            cmwBtSig.Configure.Cprotocol = CommProtocolEnum.TWO;
            var catalog = cmwBtSig.Configure.ComSettings.Ports.Catalog;
            cmwBtSig.Configure.ComSettings.ComPort.Set(0); // Why 0? Docu says 1..4
            cmwBtSig.Configure.ComSettings.Baudrate.Set(BaudRateEnum.B19K);
            cmwBtSig.Configure.ComSettings.StopBits.Set(StopBitsEnum.S1);
            cmwBtSig.Configure.ComSettings.Parity.Set(ParityEnum.NONE);
            cmwBtSig.Configure.ComSettings.Protocol.Set(ProtocolEnum.NONE);
            cmwBtSig.Configure.ComSettings.Ereset.Set(false);
            cmwBtSigRfSettings.Hopping = false;
            cmwBtSig.Configure.Connection.Whitening = false;

            // Meas settings
            cmwBtMeas.Trigger.MultiEval.Timeout = 1;
            cmwBtMeas.Configure.MultiEval.Repetition = RepetitionEnum.SINGleshot;
            cmwBtMeas.Configure.MultiEval.Scount.PowerVsTime = 5;
            cmwBtMeas.Configure.MultiEval.Scount.Modulation = 5;
            var measurements = cmwBtMeas.Configure.MultiEval.Result.All;
            cmwBtMeas.Configure.MultiEval.Result.All = new RsCmwBluetoothMeas_Configure_MultiEval_Result.All_Data()
            {
                ModScalars = true,
                PowerScalars = true,
                PhaseEncoding = true,
                PowerVsSlot = false
            };

            // Changing settings for BTM
            var setups = new List<Tuple<int, LeRangePaternTypeEnum>>();
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(0, LeRangePaternTypeEnum.P11));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(0, LeRangePaternTypeEnum.P44));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(19, LeRangePaternTypeEnum.P11));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(19, LeRangePaternTypeEnum.P44));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(30, LeRangePaternTypeEnum.P11));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(30, LeRangePaternTypeEnum.P44));
            setups.Add(new Tuple<int, LeRangePaternTypeEnum>(39, LeRangePaternTypeEnum.P44));

            // Measurement loop
            foreach (var setup in setups)
            {
                cmwBtSigRfSettings.Channel.DtMode = setup.Item1;
                cmwBtSig.Configure.Connection.Packets.Pattern.LowEnergy.Le1m = setup.Item2;

                cmwBtMeas.MultiEval.InitiateAndWait();
                var resultsPvtAvg = cmwBtMeas.MultiEval.PowerVsTime.LowEnergy.Le1M.Average.Fetch();
                var resultsPvtMax = cmwBtMeas.MultiEval.PowerVsTime.LowEnergy.Le1M.Maximum.Fetch();
                var resultsPvtMin = cmwBtMeas.MultiEval.PowerVsTime.LowEnergy.Le1M.Minimum.Fetch();

                var resultsModAvg = cmwBtMeas.MultiEval.Modulation.LowEnergy.Le1M.Average.Fetch();
                var resultsModMax = cmwBtMeas.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Fetch();

                cmwBtMeas.MultiEval.StopAndWait();
            }

            // switch all meas off
            cmwBtMeas.Configure.MultiEval.Result.All = new RsCmwBluetoothMeas_Configure_MultiEval_Result.All_Data();

            cmwBtSig.Configure.Connection.Packets.Pattern.LowEnergy.Le1m = LeRangePaternTypeEnum.PRBS9;
            cmwBtSig.Configure.Connection.Packets.PacketLength.LowEnergy.Le1m = 37;
            cmwBtSigRfSettings.Level = -70;
            cmwBtSigRfSettings.Dtx.Mode.LowEnergy.Le1m = DtxModeEnum.SPEC;
            cmwBtSigRfSettings.Dtx.Value = false;
            cmwBtSig.Configure.RxQuality.Packets.LowEnergy.Le1m = 200;
            cmwBtSig.Configure.RxQuality.Timeout = 200;

            cmwBtMeas.Configure.MultiEval.Repetition = RepetitionEnum.SINGleshot;
            cmwBtSig.Configure.RxQuality.Limit.Mper.LowEnergy.Le1m = 30.8;

            // Changing settings for BTS
            cmwBtSigRfSettings.Channel.DtMode = 0;
            cmwBtSigRfSettings.Channel.DtMode = 19;
            cmwBtSigRfSettings.Channel.DtMode = 39;

            cmwBtSig.RxQuality.Per.InitiateAndWait();
            var rxQual = cmwBtSig.RxQuality.Per.LowEnergy.Le1M.Fetch();
            cmwBtSig.RxQuality.Per.StopAndWait();

            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}
