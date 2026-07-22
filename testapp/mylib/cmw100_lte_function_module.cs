using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RohdeSchwarz.RsCmwBase;
using RohdeSchwarz.RsCmwLteSig;
using RohdeSchwarz.RsCmwLteMeas;
namespace testapp.mylib
{
 

    class cmw100_lte_function_module
    {




        RsCmwLteMeas meas_inst = null;
        RsCmwLteSig singal_inst = null;
        RsCmwBase rsCmwBase = null;
        public cmw100_lte_function_module(string inst = "TCPIP::localhost::INSTR")
        {
            rsCmwBase = new RsCmwBase(inst,true,true);
            singal_inst = new RsCmwLteSig(rsCmwBase.Session);
            meas_inst = new RsCmwLteMeas(rsCmwBase.Session);


        }

        public void measure_set() {
            // CONFigure:LTE:MEASurement<Instance>:DMODe
            meas_inst.Configure.Dmode = RohdeSchwarz.RsCmwLteMeas.DuplexModeEnum.FDD;
            meas_inst.Route.Scenario.CombinedSignalPath.Set("LTE Sig1");
            meas_inst.Configure.MultiEval.Repetition =  RohdeSchwarz.RsCmwLteMeas.RepeatEnum.CONTinuous;

            meas_inst.Configure.MultiEval.Cprefix =  RohdeSchwarz.RsCmwLteMeas.CyclicPrefixEnum.NORMal  ;
            meas_inst.Configure.Band = BandEnum.OB17; 

        }

        public double get_max_tx_power() {


            // FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:MAXimum
            List<double> value = meas_inst.MultiEval.List.Power.TxPower.Maximum.Fetch();


            return value.Average();

        }


        public  int  set_signal_config_all() {

            if (RFRouting_Attenuation() == -1) return -1;
            if (Physical_Cell_Configuration() == -1) return -2;
            if (DuplexMode_OperatingBand() == -1) return -3;
            if (Physical_Cell_Configuration() == -1) return -4;
            if (Power_Settings() == -1) return -5;

            return 1;
        }



        public int  Physical_Cell_Configuration(
                                                 RohdeSchwarz.RsCmwLteSig.BandwidthEnum bandwidth = BandwidthEnum.B100,
                                                int int_pcid=0
                                                ) {
            try
            {
                // CONFigure:LTE:SIGNaling<instance>:CELL:BANDwidth:SCC<Carrier>:DL

                singal_inst.Configure.Cell.Bandwidth.Scc.Downlink.Set(bandwidth);
                //  driver.Configure.Cell.Bandwidth.Scc.Downlink.Set(x, SecondaryCompCarrierRepCap.Default);
                singal_inst.Configure.Cell.Bandwidth.Pcc.Downlink = bandwidth;



                // CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:PCID
                // driver.Configure.Cell.Scc.Pcid.Set(1, SecondaryCompCarrierRepCap.Default);
                singal_inst.Configure.Cell.Scc.Pcid.Set(int_pcid);
                // CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:PCID
                //  int value = driver.Configure.Cell.Pcc.Pcid;
                singal_inst.Configure.Cell.Pcc.Pcid = int_pcid;

                //CONFigure: LTE: SIGNaling<instance>:CELL: CPRefix
                singal_inst.Configure.Cell.Cprefix = RohdeSchwarz.RsCmwLteSig.CyclicPrefixEnum.NORMal;


                // CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:STYPe

                singal_inst.Configure.Connection.Pcc.Stype.Type = SchedulingTypeEnum.RMC;

                singal_inst.Configure.Connection.Scc.Rmc.Downlink.Set(new RsCmwLteSig_Configure_Connection_Scc_Rmc_Downlink.Downlink_Data()
                {
                    NumberRb = NumberRbEnum.N50,
                    Modulation = RohdeSchwarz.RsCmwLteSig.ModulationEnum.QPSK,
                    TransBlockSizeIdx = TransBlockSizeIdxEnum.T5

                });

                return 1;

            }
            catch {


                return -1;

            }



        }
        public int Power_Settings(double Downlink_Pcc_Rsepre_Level = -85,
                                   double Pusch_OlnPower = -20,
                                   double Pusch_Tpc_CltPower=-20
                                   ) {

            try
            {
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:RSEPre:LEVel
                singal_inst.Configure.Downlink.Pcc.Rsepre.Level = Downlink_Pcc_Rsepre_Level;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PSS:POFFset
                singal_inst.Configure.Downlink.Pcc.Pss.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:SSS:POFFset
                singal_inst.Configure.Downlink.Pcc.Sss.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PBCH:POFFset
                singal_inst.Configure.Downlink.Pcc.Pbch.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PCFich:POFFset
                singal_inst.Configure.Downlink.Pcc.Pcfich.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PCFich:POFFset
                singal_inst.Configure.Downlink.Pcc.Pcfich.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PHICh:POFFset
                singal_inst.Configure.Downlink.Pcc.Phich.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PDCCh:POFFset
                singal_inst.Configure.Downlink.Pcc.Pdcch.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PDCCh:POFFset	
                singal_inst.Configure.Downlink.Pcc.Pdcch.Poffset = 0;
                // CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:OCNG
                singal_inst.Configure.Downlink.Pcc.Ocng = false;
                // CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:OLNPower
                singal_inst.Configure.Uplink.Pcc.Pusch.OlnPower = Pusch_OlnPower;

                // CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:CLTPower
                singal_inst.Configure.Uplink.Pcc.Pusch.Tpc.CltPower = Pusch_Tpc_CltPower;

                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:RSEPre:LEVel
                singal_inst.Configure.Downlink.Scc.Rsepre.Level.Set(0.0);

                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PSS:POFFset
                singal_inst.Configure.Downlink.Scc.Pss.Poffset.Set(00);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PSS:POFFset
                singal_inst.Configure.Downlink.Scc.Pss.Poffset.Set(00);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:SSS:POFFset
                singal_inst.Configure.Downlink.Scc.Sss.Poffset.Set(0);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PBCH:POFFset
                singal_inst.Configure.Downlink.Scc.Pbch.Poffset.Set(0);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PCFich:POFFset
                singal_inst.Configure.Downlink.Scc.Pcfich.Poffset.Set(0);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PHICh:POFFset
                singal_inst.Configure.Downlink.Scc.Phich.Poffset.Set(0);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDCCh:POFFset
                singal_inst.Configure.Downlink.Scc.Pdcch.Poffset.Set(0);
                // CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:OCNG
                singal_inst.Configure.Downlink.Scc.Ocng.Set(false);

                // CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:OLNPower
                singal_inst.Configure.Uplink.Scc.Pusch.OlnPower.Set(Pusch_OlnPower);

                // CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:TPOWer
                singal_inst.Configure.Uplink.Scc.Pusch.Tpc.Tpower.Set(Pusch_Tpc_CltPower);

                singal_inst.Configure.Uplink.Scc.Pusch.Tpc.Set.Set(SetTypeEnum.CLOop);

                return 1;
            }
            catch {

                return -1;
            }

        }


        public int  get_Cell_State() {

            // SOURce:LTE:SIGNaling<instance>:CELL:STATe
          
            singal_inst.Source.Cell.State.Value = true;

            // SOURce:LTE:SIGNaling<instance>:CELL:STATe:ALL
            for(int i=0; i < 50; i++) {
                if (singal_inst.Source.Cell.State.All.SyncState == SignalingGeneratorStateEnum.ADJusted && singal_inst.Source.Cell.State.All.MainState == MainStateEnum.ON) { return 1; };
                System.Threading.Thread.Sleep(100);
            }
            return -1;
        }

        public int ue_connect()
        {

            singal_inst.Call.Pswitched.Action =  PswActionEnum.CONNect;
            // CONFigure:LTE:SIGNaling<instance>:UEReport:ENABle
  
            singal_inst.Configure.UeReport.Enable =  true;

            // FETCh:LTE:SIGNaling<instance>:PSWitched:STATe

            for(int i = 0; i < 100; i++)
            {

                PswStateEnum value = singal_inst.Pswitched.State.Fetch();
                if (value == PswStateEnum.ATTached) return 1;
            }

            return -1;

        }

        public void get_UE_Info() {

            // SENSe:LTE:SIGNaling<instance>:UESinfo:IMEI
            string value = singal_inst.Sense.UesInfo.Imei;

            // SENSe:LTE:SIGNaling<instance>:UESinfo:IMSI
            string value2 = singal_inst.Sense.UesInfo.Imsi;
            // SENSe:LTE:SIGNaling<instance>:UESinfo:UEADdress:IPV<n>
            var value3 = singal_inst.Sense.UesInfo.UeAddress.Ipv
          ;
        }

        public void get_ue_info2() {

          // SENSe:LTE: SIGNaling<instance>:UEReport[:PCC]:RSRP
        int   value = singal_inst.Sense.UeReport.Pcc.Rsrp.Value;
            // SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:RSRQ
            int value2 = singal_inst.Sense.UeReport.Pcc.Rsrq.Value;



        }

        /// <summary>
        /// 设定DuplexMode 和OperatingBand
        /// </summary>
        /// <param name="duplexMode"></param>
        /// <param name="operatingBand"></param>
        public int  DuplexMode_OperatingBand( RohdeSchwarz.RsCmwLteSig.DuplexModeEnum duplexMode= RohdeSchwarz.RsCmwLteSig.DuplexModeEnum.FDD,
                                              RohdeSchwarz.RsCmwLteSig.OperatingBandCenum operatingBand= OperatingBandCenum.OB17)
        {

            try
            {
                singal_inst.Configure.Scc.Dmode.Set(duplexMode);
                //driver.Configure.Scc.Dmode.Set(x, SecondaryCompCarrierRepCap.Default);
                // CONFigure:LTE:SIGNaling<instance>[:PCC]:DMODe
                singal_inst.Configure.Pcc.Dmode.Value = duplexMode;
                //driver.Configure.Pcc.Dmode.Set(x, SecondaryCompCarrierRepCap.Default);

                // CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:BAND
                singal_inst.Configure.Scc.Band.Set(operatingBand);
                //driver.Configure.Scc.Band.Set(x, SecondaryCompCarrierRepCap.Default);
                // CONFigure:LTE:SIGNaling<instance>[:PCC]:BAND
                singal_inst.Configure.Pcc.Band = operatingBand;
                return 1;
            }
            catch {

                return -1;
            }
        }

        /// <summary>
        /// 衰减设置
        /// </summary>
        public int  RFRouting_Attenuation() {
            try
            {
                RsCmwLteSig_Route_Scenario_Scell.Flexible_Data value = singal_inst.Route.Scenario.Scell.Flexible;
                //RF input Connector    
                value.RxConnector = RohdeSchwarz.RsCmwLteSig.RxConnectorEnum.RF1C;
                //RF input Converter
                value.RxConverter = RohdeSchwarz.RsCmwLteSig.RxConverterEnum.RX1;
                // RF output Connector
                value.TxConnector = TxConnectorEnum.RF1C;
                // RF output Converter
                value.TxConverter = TxConverterEnum.TX1;

                singal_inst.Route.Scenario.Scell.Flexible = value;
                // CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:EATTenuation:INPut
                singal_inst.Configure.RfSettings.Pcc.Eattenuation.Input = 0;

                // CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:INPut
                // driver.Configure.RfSettings.Scc.Eattenuation.Input.Set(1.0, SecondaryCompCarrierRepCap.Default);
                singal_inst.Configure.RfSettings.Scc.Eattenuation.Input.Set(0.0);
                // CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:OUTPut<n>
                // driver.Configure.RfSettings.Scc.Eattenuation.Output.Set(1.0, SecondaryCompCarrierRepCap.Default, OutputRepCap.Default);
                singal_inst.Configure.RfSettings.Scc.Eattenuation.Output.Set(1.0);
                // CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:EATTenuation:OUTPut<n>
                //  driver.Configure.RfSettings.Pcc.Eattenuation.Output.Set(1.0, OutputRepCap.Default);
                singal_inst.Configure.RfSettings.Pcc.Eattenuation.Output.Set(0.0);
                return 1;
            }
            catch {


                return -1;

            }


        }

      ~cmw100_lte_function_module()
        {

            try
            {
                if (singal_inst != null) singal_inst.Dispose();
                if (meas_inst != null) meas_inst.Dispose();
                if (rsCmwBase != null) rsCmwBase.Dispose();
            }
            catch (Exception e) {

                System.Windows.Forms.MessageBox.Show(e.ToString());
            }

        }








    }











}
