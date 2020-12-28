using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwGsmSig;

namespace TestProject
{
	/// 
	/// This test program contains all the implemented SCPI commands and the use of their
	/// corresponding driver properties / methods
	/// 
	class Usage
	{
		static void Main(string[] args)
		{
			RsCmwGsmSig driver = new RsCmwGsmSig("TCPIP::localhost::INSTR", true, true);
			{	// ROUTe:GSM:SIGNaling<Instance>
				RsCmwGsmSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario
				RsCmwGsmSig_Route_Scenario.Value_Data value = driver.Route.Scenario.Value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:SCELl:FLEXible
				RsCmwGsmSig_Route_Scenario_Scell.Flexible_Data value = driver.Route.Scenario.Scell.Flexible;
				driver.Route.Scenario.Scell.Flexible = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:IORI:FLEXible
				RsCmwGsmSig_Route_Scenario_Iori.Flexible_Data value = driver.Route.Scenario.Iori.Flexible;
				driver.Route.Scenario.Iori.Flexible = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:BATCh:FLEXible
				RsCmwGsmSig_Route_Scenario_Batch.Flexible_Data value = driver.Route.Scenario.Batch.Flexible;
				driver.Route.Scenario.Batch.Flexible = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:SCFading:FLEXible[:EXTernal]
				RsCmwGsmSig_Route_Scenario_ScFading_Flexible.External_Data value = driver.Route.Scenario.ScFading.Flexible.External;
				driver.Route.Scenario.ScFading.Flexible.External = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:SCFading:FLEXible:INTernal
				RsCmwGsmSig_Route_Scenario_ScFading_Flexible.Internal_Data value = driver.Route.Scenario.ScFading.Flexible.Internal;
				driver.Route.Scenario.ScFading.Flexible.Internal = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:SCFDiversity:FLEXible[:EXTernal]
				RsCmwGsmSig_Route_Scenario_ScfDiversity_Flexible.External_Data value = driver.Route.Scenario.ScfDiversity.Flexible.External;
				driver.Route.Scenario.ScfDiversity.Flexible.External = value;
			}
			{	// ROUTe:GSM:SIGNaling<Instance>:SCENario:SCFDiversity:FLEXible:INTernal
				RsCmwGsmSig_Route_Scenario_ScfDiversity_Flexible.Internal_Data value = driver.Route.Scenario.ScfDiversity.Flexible.Internal;
				driver.Route.Scenario.ScfDiversity.Flexible.Internal = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:ETOE
				bool value = driver.Configure.Etoe;
				driver.Configure.Etoe = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BAND:BCCH
				foreach (OperBandGsmEnum x in new OperBandGsmEnum[] { OperBandGsmEnum.G04, OperBandGsmEnum.G085, OperBandGsmEnum.G09, OperBandGsmEnum.G18, OperBandGsmEnum.G19, OperBandGsmEnum.GT081 })
				{
					driver.Configure.Band.Bcch = x;
					OperBandGsmEnum value = driver.Configure.Band.Bcch;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:DUALband:BAND:TCH
				foreach (OperBandGsmEnum x in new OperBandGsmEnum[] { OperBandGsmEnum.G04, OperBandGsmEnum.G085, OperBandGsmEnum.G09, OperBandGsmEnum.G18, OperBandGsmEnum.G19, OperBandGsmEnum.GT081 })
				{
					driver.Configure.DualBand.Band.Tch = x;
					OperBandGsmEnum value = driver.Configure.DualBand.Band.Tch;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:DUALband:COMBined:CS
				RsCmwGsmSig_Configure_DualBand_Combined.Cs_Data value = driver.Configure.DualBand.Combined.Cs;
				driver.Configure.DualBand.Combined.Cs = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:MSLot:UL
				int value = driver.Configure.Mslot.Uplink;
				driver.Configure.Mslot.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:MLOFfset
				int value = driver.Configure.RfSettings.MlOffset;
				driver.Configure.RfSettings.MlOffset = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:RFSettings:ENPower
				double value = driver.Configure.RfSettings.EnvelopePower;
				driver.Configure.RfSettings.EnvelopePower = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:RFSettings:ENPMode
				foreach (NominalPowerModeEnum x in new NominalPowerModeEnum[] { NominalPowerModeEnum.AUToranging, NominalPowerModeEnum.MANual, NominalPowerModeEnum.ULPC })
				{
					driver.Configure.RfSettings.EnpMode = x;
					NominalPowerModeEnum value = driver.Configure.RfSettings.EnpMode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:RFSettings:UMARgin
				double value = driver.Configure.RfSettings.Umargin;
				driver.Configure.RfSettings.Umargin = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Eattenuation.Input;
				driver.Configure.RfSettings.Eattenuation.Input = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EATTenuation:OUTPut<n>
				double value = driver.Configure.RfSettings.Eattenuation.Output.Get(OutputRepCap.Default);
				value = driver.Configure.RfSettings.Eattenuation.Output.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EATTenuation:OUTPut<n>
				driver.Configure.RfSettings.Eattenuation.Output.Set(1.0, OutputRepCap.Default);
				driver.Configure.RfSettings.Eattenuation.Output.Set(1.0);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EATTenuation:BCCH:OUTPut
				double value = driver.Configure.RfSettings.Eattenuation.Bcch.Output;
				driver.Configure.RfSettings.Eattenuation.Bcch.Output = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:CHANnel:BCCH
				int value = driver.Configure.RfSettings.Channel.Bcch;
				driver.Configure.RfSettings.Channel.Bcch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:CHANnel:TCH[:CARRier{carrierCmdVal}]
				int value = driver.Configure.RfSettings.Channel.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Channel.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:CHANnel:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Channel.Tch.Carrier.Set(1, CarrierRepCap.Default);
				driver.Configure.RfSettings.Channel.Tch.Carrier.Set(1);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:LEVel:BCCH
				double value = driver.Configure.RfSettings.Level.Bcch.Value;
				driver.Configure.RfSettings.Level.Bcch.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:LEVel:BCCH:MINimum:ENABle
				bool value = driver.Configure.RfSettings.Level.Bcch.Minimum.Enable;
				driver.Configure.RfSettings.Level.Bcch.Minimum.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:LEVel:TCH[:CARRier{carrierCmdVal}]
				double value = driver.Configure.RfSettings.Level.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Level.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:LEVel:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Level.Tch.Carrier.Set(1.0, CarrierRepCap.Default);
				driver.Configure.RfSettings.Level.Tch.Carrier.Set(1.0);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:PMAX:BCCH
				int value = driver.Configure.RfSettings.PowerMax.Bcch;
				driver.Configure.RfSettings.PowerMax.Bcch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:FOFFset:DL
				int value = driver.Configure.RfSettings.FreqOffset.Downlink;
				driver.Configure.RfSettings.FreqOffset.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:FOFFset:UL
				int value = driver.Configure.RfSettings.FreqOffset.Uplink;
				driver.Configure.RfSettings.FreqOffset.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:PCL:TCH:CSWitched
				int value = driver.Configure.RfSettings.Pcl.Tch.Cswitched;
				driver.Configure.RfSettings.Pcl.Tch.Cswitched = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:CHCCombined:TCH:CSWitched
				RsCmwGsmSig_Configure_RfSettings_ChcCombined_Tch.Cswitched_Data value = driver.Configure.RfSettings.ChcCombined.Tch.Cswitched;
				driver.Configure.RfSettings.ChcCombined.Tch.Cswitched = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EDC:OUTPut
				double value = driver.Configure.RfSettings.Edc.Output;
				driver.Configure.RfSettings.Edc.Output = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:EDC:INPut
				double value = driver.Configure.RfSettings.Edc.Input;
				driver.Configure.RfSettings.Edc.Input = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:ENABle:TCH[:CARRier{carrierCmdVal}]
				bool value = driver.Configure.RfSettings.Hopping.Enable.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Hopping.Enable.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:ENABle:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Hopping.Enable.Tch.Carrier.Set(false, CarrierRepCap.Default);
				driver.Configure.RfSettings.Hopping.Enable.Tch.Carrier.Set(false);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:SEQuence:TCH[:CARRier{carrierCmdVal}]
				List<int> value = driver.Configure.RfSettings.Hopping.Sequence.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Hopping.Sequence.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:SEQuence:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Hopping.Sequence.Tch.Carrier.Set(new List<int> { 1, 2, 3 }, CarrierRepCap.Default);
				driver.Configure.RfSettings.Hopping.Sequence.Tch.Carrier.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:HSN:TCH[:CARRier{carrierCmdVal}]
				int value = driver.Configure.RfSettings.Hopping.Hsn.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Hopping.Hsn.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:HSN:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Hopping.Hsn.Tch.Carrier.Set(1, CarrierRepCap.Default);
				driver.Configure.RfSettings.Hopping.Hsn.Tch.Carrier.Set(1);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:MAIO:TCH[:CARRier{carrierCmdVal}]
				int value = driver.Configure.RfSettings.Hopping.Maio.Tch.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.RfSettings.Hopping.Maio.Tch.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RFSettings:HOPPing:MAIO:TCH[:CARRier{carrierCmdVal}]
				driver.Configure.RfSettings.Hopping.Maio.Tch.Carrier.Set(1, CarrierRepCap.Default);
				driver.Configure.RfSettings.Hopping.Maio.Tch.Carrier.Set(1);
			}
			{	// CONFigure:GSM:SIGNaling<instance>:IQIN:PATH<n>
				RsCmwGsmSig_Configure_IqIn_Path.Path_Data value = driver.Configure.IqIn.Path.Get(PathRepCap.Default);
				value = driver.Configure.IqIn.Path.Get();
			}
			{	// CONFigure:GSM:SIGNaling<instance>:IQIN:PATH<n>
				RsCmwGsmSig_Configure_IqIn_Path.Path_Data value = new RsCmwGsmSig_Configure_IqIn_Path.Path_Data();
				driver.Configure.IqIn.Path.Set(value, PathRepCap.Default);
				driver.Configure.IqIn.Path.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Fsimulator.Enable;
				driver.Configure.Fading.Fsimulator.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:FSIMulator:STANdard
				foreach (FadingStandardEnum x in new FadingStandardEnum[] { FadingStandardEnum.E100, FadingStandardEnum.E50, FadingStandardEnum.E60, FadingStandardEnum.H100, FadingStandardEnum.H120, FadingStandardEnum.H200, FadingStandardEnum.HT100, FadingStandardEnum.HT120, FadingStandardEnum.HT200, FadingStandardEnum.R130, FadingStandardEnum.R250, FadingStandardEnum.R300, FadingStandardEnum.R500, FadingStandardEnum.T100, FadingStandardEnum.T1P5, FadingStandardEnum.T25, FadingStandardEnum.T3, FadingStandardEnum.T3P6, FadingStandardEnum.T50, FadingStandardEnum.T6, FadingStandardEnum.T60, FadingStandardEnum.TI5, FadingStandardEnum.TU100, FadingStandardEnum.TU1P5, FadingStandardEnum.TU25, FadingStandardEnum.TU3, FadingStandardEnum.TU3P6, FadingStandardEnum.TU50, FadingStandardEnum.TU6, FadingStandardEnum.TU60 })
				{
					driver.Configure.Fading.Fsimulator.Standard = x;
					FadingStandardEnum value = driver.Configure.Fading.Fsimulator.Standard;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Fsimulator.Globale.Seed;
				driver.Configure.Fading.Fsimulator.Globale.Seed = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:FSIMulator:RESTart:MODE
				foreach (RestartModeEnum x in new RestartModeEnum[] { RestartModeEnum.AUTO, RestartModeEnum.MANual, RestartModeEnum.TRIGger })
				{
					driver.Configure.Fading.Fsimulator.Restart.Mode = x;
					RestartModeEnum value = driver.Configure.Fading.Fsimulator.Restart.Mode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:FSIMulator:RESTart
				driver.Configure.Fading.Fsimulator.Restart.Set();
				driver.Configure.Fading.Fsimulator.Restart.SetAndWait();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.LACP, InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Fsimulator.InsertionLoss.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Fsimulator.InsertionLoss.Mode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:ILOSs:CSAMples
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Csamples;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:ILOSs:LOSS[:USER]
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Loss.User;
				driver.Configure.Fading.Fsimulator.InsertionLoss.Loss.User = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:ILOSs:LOSS:NORMal
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Loss.Normal;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:DSHift:MODE
				foreach (FadingModeEnum x in new FadingModeEnum[] { FadingModeEnum.NORMal, FadingModeEnum.USER })
				{
					driver.Configure.Fading.Fsimulator.Dshift.Mode = x;
					FadingModeEnum value = driver.Configure.Fading.Fsimulator.Dshift.Mode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:FSIMulator:DSHift
				double value = driver.Configure.Fading.Fsimulator.Dshift.Value;
				driver.Configure.Fading.Fsimulator.Dshift.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:FADing:AWGN:ENABle
				bool value = driver.Configure.Fading.Awgn.Enable;
				driver.Configure.Fading.Awgn.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:AWGN:SNRatio
				double value = driver.Configure.Fading.Awgn.SnRatio;
				driver.Configure.Fading.Awgn.SnRatio = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Awgn.Bandwidth.Ratio;
				driver.Configure.Fading.Awgn.Bandwidth.Ratio = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:AWGN:BWIDth:NOISe
				double value = driver.Configure.Fading.Awgn.Bandwidth.Noise;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:POWer:SUM
				double value = driver.Configure.Fading.Power.Sum;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Power.Noise.Total;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:FADing:POWer:NOISe
				double value = driver.Configure.Fading.Power.Noise.Value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:ASConfig
				bool value = driver.Configure.Connection.AsConfig;
				driver.Configure.Connection.AsConfig = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:DSConfig
				bool value = driver.Configure.Connection.DsConfig;
				driver.Configure.Connection.DsConfig = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:TADVance
				int value = driver.Configure.Connection.Tadvance;
				driver.Configure.Connection.Tadvance = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:RFOFfset
				bool value = driver.Configure.Connection.Rfoffset;
				driver.Configure.Connection.Rfoffset = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:TSLot
				int value = driver.Configure.Connection.Cswitched.Tslot;
				driver.Configure.Connection.Cswitched.Tslot = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:TMODe
				foreach (SpeechChannelCodingModeEnum x in new SpeechChannelCodingModeEnum[] { SpeechChannelCodingModeEnum.ANFG, SpeechChannelCodingModeEnum.ANH8, SpeechChannelCodingModeEnum.ANHG, SpeechChannelCodingModeEnum.AWF8, SpeechChannelCodingModeEnum.AWFG, SpeechChannelCodingModeEnum.AWH8, SpeechChannelCodingModeEnum.FV1, SpeechChannelCodingModeEnum.FV2, SpeechChannelCodingModeEnum.HV1 })
				{
					driver.Configure.Connection.Cswitched.Tmode = x;
					SpeechChannelCodingModeEnum value = driver.Configure.Connection.Cswitched.Tmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:HRSubchannel
				int value = driver.Configure.Connection.Cswitched.HrsubChannel;
				driver.Configure.Connection.Cswitched.HrsubChannel = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:DSOurce
				foreach (SwitchedSourceModeEnum x in new SwitchedSourceModeEnum[] { SwitchedSourceModeEnum.ALL0, SwitchedSourceModeEnum.ALL1, SwitchedSourceModeEnum.ALTernating, SwitchedSourceModeEnum.ECHO, SwitchedSourceModeEnum.PR11, SwitchedSourceModeEnum.PR15, SwitchedSourceModeEnum.PR16, SwitchedSourceModeEnum.PR9, SwitchedSourceModeEnum.SP1, SwitchedSourceModeEnum.SP2, SwitchedSourceModeEnum.UPATtern })
				{
					driver.Configure.Connection.Cswitched.Dsource = x;
					SwitchedSourceModeEnum value = driver.Configure.Connection.Cswitched.Dsource;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:CRELease
				foreach (CallReleaseEnum x in new CallReleaseEnum[] { CallReleaseEnum.IRELease, CallReleaseEnum.LERelease, CallReleaseEnum.NRELease })
				{
					driver.Configure.Connection.Cswitched.Crelease = x;
					CallReleaseEnum value = driver.Configure.Connection.Cswitched.Crelease;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:EDELay
				int value = driver.Configure.Connection.Cswitched.Edelay;
				driver.Configure.Connection.Cswitched.Edelay = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:LOOP
				foreach (CswLoopEnum x in new CswLoopEnum[] { CswLoopEnum.A, CswLoopEnum.B, CswLoopEnum.C, CswLoopEnum.D, CswLoopEnum.I, CswLoopEnum.OFF, CswLoopEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Loop = x;
					CswLoopEnum value = driver.Configure.Connection.Cswitched.Loop;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:LREClose
				bool value = driver.Configure.Connection.Cswitched.Lreclose;
				driver.Configure.Connection.Cswitched.Lreclose = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:CID
				string value = driver.Configure.Connection.Cswitched.Cid;
				driver.Configure.Connection.Cswitched.Cid = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:TCHassign
				foreach (TchAssignmentEnum x in new TchAssignmentEnum[] { TchAssignmentEnum.EARLy, TchAssignmentEnum.LATE, TchAssignmentEnum.OFF, TchAssignmentEnum.ON, TchAssignmentEnum.VEARly })
				{
					driver.Configure.Connection.Cswitched.TchAssign = x;
					TchAssignmentEnum value = driver.Configure.Connection.Cswitched.TchAssign;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:RFACch
				bool value = driver.Configure.Connection.Cswitched.Rfacch;
				driver.Configure.Connection.Cswitched.Rfacch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:RSACch
				bool value = driver.Configure.Connection.Cswitched.Rsacch;
				driver.Configure.Connection.Cswitched.Rsacch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:DTX:DL
				RsCmwGsmSig_Configure_Connection_Cswitched_Dtx.Downlink_Data value = driver.Configure.Connection.Cswitched.Dtx.Downlink;
				driver.Configure.Connection.Cswitched.Dtx.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:SIGNaling:MODE
				foreach (SignalingModeEnum x in new SignalingModeEnum[] { SignalingModeEnum.LTRR, SignalingModeEnum.RATScch })
				{
					driver.Configure.Connection.Cswitched.Amr.Signaling.Mode = x;
					SignalingModeEnum value = driver.Configure.Connection.Cswitched.Amr.Signaling.Mode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:NB:FRATe:GMSK
				foreach (NbCodecEnum x in new NbCodecEnum[] { NbCodecEnum.C0475, NbCodecEnum.C0515, NbCodecEnum.C0590, NbCodecEnum.C0670, NbCodecEnum.C0740, NbCodecEnum.C0795, NbCodecEnum.C1020, NbCodecEnum.C1220, NbCodecEnum.OFF, NbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Frate.Gmsk = new List<NbCodecEnum> { x, x, x, x, x };
					List<NbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Frate.Gmsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:NB:HRATe:GMSK
				foreach (NbCodecEnum x in new NbCodecEnum[] { NbCodecEnum.C0475, NbCodecEnum.C0515, NbCodecEnum.C0590, NbCodecEnum.C0670, NbCodecEnum.C0740, NbCodecEnum.C0795, NbCodecEnum.C1020, NbCodecEnum.C1220, NbCodecEnum.OFF, NbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Hrate.Gmsk = new List<NbCodecEnum> { x, x, x, x, x };
					List<NbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Hrate.Gmsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:NB:HRATe:EPSK
				foreach (NbCodecEnum x in new NbCodecEnum[] { NbCodecEnum.C0475, NbCodecEnum.C0515, NbCodecEnum.C0590, NbCodecEnum.C0670, NbCodecEnum.C0740, NbCodecEnum.C0795, NbCodecEnum.C1020, NbCodecEnum.C1220, NbCodecEnum.OFF, NbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Hrate.Epsk = new List<NbCodecEnum> { x, x, x, x, x };
					List<NbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Nb.Hrate.Epsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:WB:FRATe:GMSK
				foreach (WbCodecEnum x in new WbCodecEnum[] { WbCodecEnum.C0660, WbCodecEnum.C0885, WbCodecEnum.C1265, WbCodecEnum.C1585, WbCodecEnum.C2385, WbCodecEnum.OFF, WbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Frate.Gmsk = new List<WbCodecEnum> { x, x, x, x, x };
					List<WbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Frate.Gmsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:WB:FRATe:EPSK
				foreach (WbCodecEnum x in new WbCodecEnum[] { WbCodecEnum.C0660, WbCodecEnum.C0885, WbCodecEnum.C1265, WbCodecEnum.C1585, WbCodecEnum.C2385, WbCodecEnum.OFF, WbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Frate.Epsk = new List<WbCodecEnum> { x, x, x, x, x };
					List<WbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Frate.Epsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:RSET:WB:HRATe:EPSK
				foreach (WbCodecEnum x in new WbCodecEnum[] { WbCodecEnum.C0660, WbCodecEnum.C0885, WbCodecEnum.C1265, WbCodecEnum.C1585, WbCodecEnum.C2385, WbCodecEnum.OFF, WbCodecEnum.ON })
				{
					driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Hrate.Epsk = new List<WbCodecEnum> { x, x, x, x, x };
					List<WbCodecEnum> value = driver.Configure.Connection.Cswitched.Amr.Rset.Wb.Hrate.Epsk;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:FRATe:GMSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Frate.Gmsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Frate.Gmsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:FRATe:GMSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Frate.Gmsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Frate.Gmsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:HRATe:GMSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Gmsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Gmsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:HRATe:GMSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Gmsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Gmsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:HRATe:EPSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Epsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Epsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:NB:HRATe:EPSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Epsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Nb.Hrate.Epsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:FRATe:GMSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Gmsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Gmsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:FRATe:GMSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Gmsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Gmsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:FRATe:EPSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Epsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Epsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:FRATe:EPSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Epsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Frate.Epsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:HRATe:EPSK:DL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Hrate.Epsk.Downlink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Hrate.Epsk.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:CMODe:WB:HRATe:EPSK:UL
				int value = driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Hrate.Epsk.Uplink;
				driver.Configure.Connection.Cswitched.Amr.Cmode.Wb.Hrate.Epsk.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:NB:FRATe:GMSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Frate.Gmsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Frate.Gmsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:NB:HRATe:GMSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Hrate.Gmsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Hrate.Gmsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:NB:HRATe:EPSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Hrate.Epsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Nb.Hrate.Epsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:WB:FRATe:GMSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Frate.Gmsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Frate.Gmsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:WB:FRATe:EPSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Frate.Epsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Frate.Epsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:AMR:THReshold:WB:HRATe:EPSK
				List<double> value = driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Hrate.Epsk;
				driver.Configure.Connection.Cswitched.Amr.Threshold.Wb.Hrate.Epsk = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:VAMos:ENABle
				bool value = driver.Configure.Connection.Cswitched.Vamos.Enable;
				driver.Configure.Connection.Cswitched.Vamos.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:VAMos:MSLevel
				foreach (VamosModeEnum x in new VamosModeEnum[] { VamosModeEnum.AUTO, VamosModeEnum.VAM1, VamosModeEnum.VAM2 })
				{
					driver.Configure.Connection.Cswitched.Vamos.MsLevel = x;
					VamosModeEnum value = driver.Configure.Connection.Cswitched.Vamos.MsLevel;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:CSWitched:VAMos
				RsCmwGsmSig_Configure_Connection_Cswitched_Vamos.Value_Data value = driver.Configure.Connection.Cswitched.Vamos.Value;
				driver.Configure.Connection.Cswitched.Vamos.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SERVice
				foreach (PswitchedServiceEnum x in new PswitchedServiceEnum[] { PswitchedServiceEnum.BLER, PswitchedServiceEnum.SRB, PswitchedServiceEnum.TMA, PswitchedServiceEnum.TMB })
				{
					driver.Configure.Connection.Pswitched.Service = x;
					PswitchedServiceEnum value = driver.Configure.Connection.Pswitched.Service;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DSOurce
				foreach (SwitchedSourceModeEnum x in new SwitchedSourceModeEnum[] { SwitchedSourceModeEnum.ALL0, SwitchedSourceModeEnum.ALL1, SwitchedSourceModeEnum.ALTernating, SwitchedSourceModeEnum.ECHO, SwitchedSourceModeEnum.PR11, SwitchedSourceModeEnum.PR15, SwitchedSourceModeEnum.PR16, SwitchedSourceModeEnum.PR9, SwitchedSourceModeEnum.SP1, SwitchedSourceModeEnum.SP2, SwitchedSourceModeEnum.UPATtern })
				{
					driver.Configure.Connection.Pswitched.Dsource = x;
					SwitchedSourceModeEnum value = driver.Configure.Connection.Pswitched.Dsource;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:TLEVel
				foreach (TbfLevelEnum x in new TbfLevelEnum[] { TbfLevelEnum.EG2A, TbfLevelEnum.EG2B, TbfLevelEnum.EGPRs, TbfLevelEnum.GPRS })
				{
					driver.Configure.Connection.Pswitched.Tlevel = x;
					TbfLevelEnum value = driver.Configure.Connection.Pswitched.Tlevel;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:EDALlocation
				foreach (AutoModeEnum x in new AutoModeEnum[] { AutoModeEnum.AUTO, AutoModeEnum.OFF, AutoModeEnum.ON })
				{
					driver.Configure.Connection.Pswitched.EdAllocation = x;
					AutoModeEnum value = driver.Configure.Connection.Pswitched.EdAllocation;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:NOPDus
				int value = driver.Configure.Connection.Pswitched.Nopdus;
				driver.Configure.Connection.Pswitched.Nopdus = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SOFFset
				int value = driver.Configure.Connection.Pswitched.Soffset;
				driver.Configure.Connection.Pswitched.Soffset = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:CATYpe
				foreach (ControlAckBurstEnum x in new ControlAckBurstEnum[] { ControlAckBurstEnum.ABURsts, ControlAckBurstEnum.NBURsts })
				{
					driver.Configure.Connection.Pswitched.CaType = x;
					ControlAckBurstEnum value = driver.Configure.Connection.Pswitched.CaType;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:BPERiod<Nr>
				int value = driver.Configure.Connection.Pswitched.Bperiod;
				driver.Configure.Connection.Pswitched.Bperiod = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:BDCRate
				int value = driver.Configure.Connection.Pswitched.BdcRate;
				driver.Configure.Connection.Pswitched.BdcRate = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:ASRDblocks
				bool value = driver.Configure.Connection.Pswitched.Asrdblocks;
				driver.Configure.Connection.Pswitched.Asrdblocks = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:IREDundancy
				bool value = driver.Configure.Connection.Pswitched.Iredundancy;
				driver.Configure.Connection.Pswitched.Iredundancy = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:COMBined:CARRier{carrierCmdVal}
				RsCmwGsmSig_Configure_Connection_Pswitched_Sconfig_Combined_Carrier.Carrier_Data value = driver.Configure.Connection.Pswitched.Sconfig.Combined.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.Connection.Pswitched.Sconfig.Combined.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:COMBined:CARRier{carrierCmdVal}
				RsCmwGsmSig_Configure_Connection_Pswitched_Sconfig_Combined_Carrier.Carrier_Data value = new RsCmwGsmSig_Configure_Connection_Pswitched_Sconfig_Combined_Carrier.Carrier_Data();
				driver.Configure.Connection.Pswitched.Sconfig.Combined.Carrier.Set(value, CarrierRepCap.Default);
				driver.Configure.Connection.Pswitched.Sconfig.Combined.Carrier.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:ENABle:UL
				List<bool> value = driver.Configure.Connection.Pswitched.Sconfig.Enable.Uplink;
				driver.Configure.Connection.Pswitched.Sconfig.Enable.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:ENABle:DL:CARRier{carrierCmdVal}
				List<bool> value = driver.Configure.Connection.Pswitched.Sconfig.Enable.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.Connection.Pswitched.Sconfig.Enable.Downlink.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:ENABle:DL:CARRier{carrierCmdVal}
				driver.Configure.Connection.Pswitched.Sconfig.Enable.Downlink.Carrier.Set(new List<bool> { true, false, true }, CarrierRepCap.Default);
				driver.Configure.Connection.Pswitched.Sconfig.Enable.Downlink.Carrier.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:GAMMa:UL
				List<int> value = driver.Configure.Connection.Pswitched.Sconfig.Gamma.Uplink;
				driver.Configure.Connection.Pswitched.Sconfig.Gamma.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:LEVel:DL:CARRier{carrierCmdVal}
				List<double> value = driver.Configure.Connection.Pswitched.Sconfig.Level.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.Connection.Pswitched.Sconfig.Level.Downlink.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:LEVel:DL:CARRier{carrierCmdVal}
				driver.Configure.Connection.Pswitched.Sconfig.Level.Downlink.Carrier.Set(new List<double> { 1.1, 2.2, 3.3 }, CarrierRepCap.Default);
				driver.Configure.Connection.Pswitched.Sconfig.Level.Downlink.Carrier.Set(new List<double> { 1.1, 2.2, 3.3 });
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:CSCHeme:DL:CARRier{carrierCmdVal}
				List<CodingSchemeDownlinkEnum> value = driver.Configure.Connection.Pswitched.Sconfig.Cscheme.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Configure.Connection.Pswitched.Sconfig.Cscheme.Downlink.Carrier.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:CSCHeme:DL:CARRier{carrierCmdVal}
				foreach (CodingSchemeDownlinkEnum x in new CodingSchemeDownlinkEnum[] { CodingSchemeDownlinkEnum.C1, CodingSchemeDownlinkEnum.C2, CodingSchemeDownlinkEnum.C3, CodingSchemeDownlinkEnum.C4, CodingSchemeDownlinkEnum.DA10, CodingSchemeDownlinkEnum.DA11, CodingSchemeDownlinkEnum.DA12, CodingSchemeDownlinkEnum.DA5, CodingSchemeDownlinkEnum.DA6, CodingSchemeDownlinkEnum.DA7, CodingSchemeDownlinkEnum.DA8, CodingSchemeDownlinkEnum.DA9, CodingSchemeDownlinkEnum.DB10, CodingSchemeDownlinkEnum.DB11, CodingSchemeDownlinkEnum.DB12, CodingSchemeDownlinkEnum.DB5, CodingSchemeDownlinkEnum.DB6, CodingSchemeDownlinkEnum.DB7, CodingSchemeDownlinkEnum.DB8, CodingSchemeDownlinkEnum.DB9, CodingSchemeDownlinkEnum.MC1, CodingSchemeDownlinkEnum.MC2, CodingSchemeDownlinkEnum.MC3, CodingSchemeDownlinkEnum.MC4, CodingSchemeDownlinkEnum.MC5, CodingSchemeDownlinkEnum.MC6, CodingSchemeDownlinkEnum.MC7, CodingSchemeDownlinkEnum.MC8, CodingSchemeDownlinkEnum.MC9 })
				{
					driver.Configure.Connection.Pswitched.Sconfig.Cscheme.Downlink.Carrier.Set(new List<CodingSchemeDownlinkEnum> { x, x, x, x, x });
					driver.Configure.Connection.Pswitched.Sconfig.Cscheme.Downlink.Carrier.Set(new List<CodingSchemeDownlinkEnum> { x, x, x, x, x }, CarrierRepCap.Default);
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:UDCYcle:DL:CARRier<Carrier>
				List<int> value = driver.Configure.Connection.Pswitched.Sconfig.UdCycle.Downlink.Carrier;
				driver.Configure.Connection.Pswitched.Sconfig.UdCycle.Downlink.Carrier = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:SCONfig:UDCYcle:DL
				List<int> value = driver.Configure.Connection.Pswitched.Sconfig.UdCycle.Downlink.Value;
				driver.Configure.Connection.Pswitched.Sconfig.UdCycle.Downlink.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DPControl:ENABle
				bool value = driver.Configure.Connection.Pswitched.DpControl.Enable;
				driver.Configure.Connection.Pswitched.DpControl.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DPControl:P
				foreach (PswPowerReductionEnum x in new PswPowerReductionEnum[] { PswPowerReductionEnum.DB0, PswPowerReductionEnum.DB10, PswPowerReductionEnum.DB12, PswPowerReductionEnum.DB14, PswPowerReductionEnum.DB16, PswPowerReductionEnum.DB18, PswPowerReductionEnum.DB2, PswPowerReductionEnum.DB20, PswPowerReductionEnum.DB22, PswPowerReductionEnum.DB24, PswPowerReductionEnum.DB26, PswPowerReductionEnum.DB28, PswPowerReductionEnum.DB30, PswPowerReductionEnum.DB4, PswPowerReductionEnum.DB6, PswPowerReductionEnum.DB8 })
				{
					driver.Configure.Connection.Pswitched.DpControl.P = x;
					PswPowerReductionEnum value = driver.Configure.Connection.Pswitched.DpControl.P;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DPControl:PMODe
				foreach (PowerReductionModeEnum x in new PowerReductionModeEnum[] { PowerReductionModeEnum.PMA, PowerReductionModeEnum.PMB })
				{
					driver.Configure.Connection.Pswitched.DpControl.Pmode = x;
					PowerReductionModeEnum value = driver.Configure.Connection.Pswitched.DpControl.Pmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DPControl:PFIeld
				foreach (PowerReductionFieldEnum x in new PowerReductionFieldEnum[] { PowerReductionFieldEnum.DB0, PowerReductionFieldEnum.DB3, PowerReductionFieldEnum.DB7, PowerReductionFieldEnum.NUSable })
				{
					driver.Configure.Connection.Pswitched.DpControl.Pfield = x;
					PowerReductionFieldEnum value = driver.Configure.Connection.Pswitched.DpControl.Pfield;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:CSCHeme:UL
				foreach (CodingSchemeUplinkEnum x in new CodingSchemeUplinkEnum[] { CodingSchemeUplinkEnum.C1, CodingSchemeUplinkEnum.C2, CodingSchemeUplinkEnum.C3, CodingSchemeUplinkEnum.C4, CodingSchemeUplinkEnum.MC1, CodingSchemeUplinkEnum.MC2, CodingSchemeUplinkEnum.MC3, CodingSchemeUplinkEnum.MC4, CodingSchemeUplinkEnum.MC5, CodingSchemeUplinkEnum.MC6, CodingSchemeUplinkEnum.MC7, CodingSchemeUplinkEnum.MC8, CodingSchemeUplinkEnum.MC9, CodingSchemeUplinkEnum.UA10, CodingSchemeUplinkEnum.UA11, CodingSchemeUplinkEnum.UA7, CodingSchemeUplinkEnum.UA8, CodingSchemeUplinkEnum.UA9, CodingSchemeUplinkEnum.UB10, CodingSchemeUplinkEnum.UB11, CodingSchemeUplinkEnum.UB12, CodingSchemeUplinkEnum.UB5, CodingSchemeUplinkEnum.UB6, CodingSchemeUplinkEnum.UB7, CodingSchemeUplinkEnum.UB8, CodingSchemeUplinkEnum.UB9 })
				{
					driver.Configure.Connection.Pswitched.Cscheme.Uplink = x;
					CodingSchemeUplinkEnum value = driver.Configure.Connection.Pswitched.Cscheme.Uplink;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:PSWitched:DLDCarrier:ENABle
				bool value = driver.Configure.Connection.Pswitched.DldCarrier.Enable;
				driver.Configure.Connection.Pswitched.DldCarrier.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:FOFFset[:UL]
				int value = driver.Configure.Connection.FreqOffset.Uplink;
				driver.Configure.Connection.FreqOffset.Uplink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CONNection:FOFFset:DL
				int value = driver.Configure.Connection.FreqOffset.Downlink;
				driver.Configure.Connection.FreqOffset.Downlink = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:ALL:THResholds:HIGH
				RsCmwGsmSig_Configure_Ncell_All_Thresholds.High_Data value = driver.Configure.Ncell.All.Thresholds.High;
				driver.Configure.Ncell.All.Thresholds.High = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:LTE:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Lte_Cell.Cell_Data value = driver.Configure.Ncell.Lte.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Lte.Cell.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:LTE:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Lte_Cell.Cell_Data value = new RsCmwGsmSig_Configure_Ncell_Lte_Cell.Cell_Data();
				driver.Configure.Ncell.Lte.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Lte.Cell.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:LTE:THResholds:HIGH
				int value = driver.Configure.Ncell.Lte.Thresholds.High;
				driver.Configure.Ncell.Lte.Thresholds.High = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:GSM:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Gsm_Cell.Cell_Data value = driver.Configure.Ncell.Gsm.Cell.Get(GsmCellNoRepCap.Default);
				value = driver.Configure.Ncell.Gsm.Cell.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:GSM:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Gsm_Cell.Cell_Data value = new RsCmwGsmSig_Configure_Ncell_Gsm_Cell.Cell_Data();
				driver.Configure.Ncell.Gsm.Cell.Set(value, GsmCellNoRepCap.Default);
				driver.Configure.Ncell.Gsm.Cell.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:GSM:THResholds:HIGH
				int value = driver.Configure.Ncell.Gsm.Thresholds.High;
				driver.Configure.Ncell.Gsm.Thresholds.High = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:WCDMa:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = driver.Configure.Ncell.Wcdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Wcdma.Cell.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:WCDMa:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = new RsCmwGsmSig_Configure_Ncell_Wcdma_Cell.Cell_Data();
				driver.Configure.Ncell.Wcdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Wcdma.Cell.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:WCDMa:THResholds:HIGH
				int value = driver.Configure.Ncell.Wcdma.Thresholds.High;
				driver.Configure.Ncell.Wcdma.Thresholds.High = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:TDSCdma:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Tdscdma_Cell.Cell_Data value = driver.Configure.Ncell.Tdscdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Tdscdma.Cell.Get();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:TDSCdma:CELL<n>
				RsCmwGsmSig_Configure_Ncell_Tdscdma_Cell.Cell_Data value = new RsCmwGsmSig_Configure_Ncell_Tdscdma_Cell.Cell_Data();
				driver.Configure.Ncell.Tdscdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Tdscdma.Cell.Set(value);
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:NCELl:TDSCdma:THResholds:HIGH
				int value = driver.Configure.Ncell.Tdscdma.Thresholds.High;
				driver.Configure.Ncell.Tdscdma.Thresholds.High = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:PSDomain
				bool value = driver.Configure.Cell.Psdomain;
				driver.Configure.Cell.Psdomain = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:NSUPport
				foreach (NetworkSupportEnum x in new NetworkSupportEnum[] { NetworkSupportEnum.EGPRs, NetworkSupportEnum.GPRS })
				{
					driver.Configure.Cell.Nsupport = x;
					NetworkSupportEnum value = driver.Configure.Cell.Nsupport;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:ECIot
				bool value = driver.Configure.Cell.Eciot;
				driver.Configure.Cell.Eciot = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:DTMode
				bool value = driver.Configure.Cell.DtMode;
				driver.Configure.Cell.DtMode = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:BSAGblksres
				int value = driver.Configure.Cell.BsAgBlksRes;
				driver.Configure.Cell.BsAgBlksRes = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:BSPamfrms
				int value = driver.Configure.Cell.BsPaMfrms;
				driver.Configure.Cell.BsPaMfrms = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:BINDicator
				foreach (BandIndicatorEnum x in new BandIndicatorEnum[] { BandIndicatorEnum.G18, BandIndicatorEnum.G19 })
				{
					driver.Configure.Cell.Bindicator = x;
					BandIndicatorEnum value = driver.Configure.Cell.Bindicator;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PMODe
				foreach (PageModeEnum x in new PageModeEnum[] { PageModeEnum.NPAGing, PageModeEnum.PREorganize })
				{
					driver.Configure.Cell.Pmode = x;
					PageModeEnum value = driver.Configure.Cell.Pmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:MRETrans
				int value = driver.Configure.Cell.Mretrans;
				driver.Configure.Cell.Mretrans = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:IPReduction
				int value = driver.Configure.Cell.IpReduction;
				driver.Configure.Cell.IpReduction = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:CBARring
				bool value = driver.Configure.Cell.Cbarring;
				driver.Configure.Cell.Cbarring = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PMIDentity
				foreach (PagingEnum x in new PagingEnum[] { PagingEnum.IMSI, PagingEnum.TMSI })
				{
					driver.Configure.Cell.PmIdentity = x;
					PagingEnum value = driver.Configure.Cell.PmIdentity;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:CDEScription
				List<int> value = driver.Configure.Cell.Cdescription;
				driver.Configure.Cell.Cdescription = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:ECSending
				bool value = driver.Configure.Cell.EcSending;
				driver.Configure.Cell.EcSending = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:LUPDate
				foreach (LocationUpdateEnum x in new LocationUpdateEnum[] { LocationUpdateEnum.ALWays, LocationUpdateEnum.AUTO })
				{
					driver.Configure.Cell.Lupdate = x;
					LocationUpdateEnum value = driver.Configure.Cell.Lupdate;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:DTX
				bool value = driver.Configure.Cell.Dtx;
				driver.Configure.Cell.Dtx = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:IDENtity
				int value = driver.Configure.Cell.Identity;
				driver.Configure.Cell.Identity = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:MCC
				int value = driver.Configure.Cell.Mcc;
				driver.Configure.Cell.Mcc = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:LAC
				int value = driver.Configure.Cell.Lac;
				driver.Configure.Cell.Lac = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:RAC
				int value = driver.Configure.Cell.Rac;
				driver.Configure.Cell.Rac = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:BCC
				int value = driver.Configure.Cell.Bcc;
				driver.Configure.Cell.Bcc = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:IMEirequest
				bool value = driver.Configure.Cell.ImeiRequest;
				driver.Configure.Cell.ImeiRequest = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:CREQuest
				bool value = driver.Configure.Cell.Crequest;
				driver.Configure.Cell.Crequest = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PRAupdate
				int value = driver.Configure.Cell.PraUpdate;
				driver.Configure.Cell.PraUpdate = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PLUPdate
				int value = driver.Configure.Cell.PlUpdate;
				driver.Configure.Cell.PlUpdate = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:RESelection:TRESelection
				int value = driver.Configure.Cell.ReSelection.TreSelection;
				driver.Configure.Cell.ReSelection.TreSelection = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RESelection:HYSTeresis
				int value = driver.Configure.Cell.ReSelection.Hysteresis;
				driver.Configure.Cell.ReSelection.Hysteresis = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RESelection:QUALity:RXLevmin:EUTRan
				int value = driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Eutran;
				driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Eutran = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RESelection:QUALity:RXLevmin:UTRan
				int value = driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Utran;
				driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Utran = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RESelection:QUALity:RXLevmin:ACCess
				int value = driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Access;
				driver.Configure.Cell.ReSelection.Quality.RxLevelMin.Access = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:IMSI:FILTer
				bool value = driver.Configure.Cell.Imsi.Filter;
				driver.Configure.Cell.Imsi.Filter = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:IMSI
				RsCmwGsmSig_Configure_Cell_Imsi.Value_Data value = driver.Configure.Cell.Imsi.Value;
				driver.Configure.Cell.Imsi.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:NCC:PERMitted
				int value = driver.Configure.Cell.Ncc.Permitted;
				driver.Configure.Cell.Ncc.Permitted = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:NCC
				int value = driver.Configure.Cell.Ncc.Value;
				driver.Configure.Cell.Ncc.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:CSWitched:CREQuest
				RsCmwGsmSig_Configure_Cell_Cswitched.Crequest_Data value = driver.Configure.Cell.Cswitched.Crequest;
				driver.Configure.Cell.Cswitched.Crequest = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:CSWitched:IARTimer
				int value = driver.Configure.Cell.Cswitched.IarTimer;
				driver.Configure.Cell.Cswitched.IarTimer = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:PDPContext
				foreach (ReactionModeEnum x in new ReactionModeEnum[] { ReactionModeEnum.ACCept, ReactionModeEnum.REJect })
				{
					driver.Configure.Cell.Pswitched.PdpContext = x;
					ReactionModeEnum value = driver.Configure.Cell.Pswitched.PdpContext;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:TAVGtw
				int value = driver.Configure.Cell.Pswitched.Tavgtw;
				driver.Configure.Cell.Pswitched.Tavgtw = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:BPERiod
				int value = driver.Configure.Cell.Pswitched.Bperiod;
				driver.Configure.Cell.Pswitched.Bperiod = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:PCMChannel
				foreach (PcmChannelEnum x in new PcmChannelEnum[] { PcmChannelEnum.BCCH, PcmChannelEnum.PDCH })
				{
					driver.Configure.Cell.Pswitched.PcmChannel = x;
					PcmChannelEnum value = driver.Configure.Cell.Pswitched.PcmChannel;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:CREQuest
				RsCmwGsmSig_Configure_Cell_Pswitched.Crequest_Data value = driver.Configure.Cell.Pswitched.Crequest;
				driver.Configure.Cell.Pswitched.Crequest = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:NEUTbf
				bool value = driver.Configure.Cell.Pswitched.Neutbf;
				driver.Configure.Cell.Pswitched.Neutbf = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:EUNodata
				bool value = driver.Configure.Cell.Pswitched.EunoData;
				driver.Configure.Cell.Pswitched.EunoData = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:IARTimer
				int value = driver.Configure.Cell.Pswitched.IarTimer;
				driver.Configure.Cell.Pswitched.IarTimer = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:PSWitched:TRTimer
				int value = driver.Configure.Cell.Pswitched.TrTimer;
				driver.Configure.Cell.Pswitched.TrTimer = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:SECurity:AUTHenticat
				bool value = driver.Configure.Cell.Security.Authenticate;
				driver.Configure.Cell.Security.Authenticate = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:SECurity:SKEY
				double value = driver.Configure.Cell.Security.Skey;
				driver.Configure.Cell.Security.Skey = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:SECurity:SIMCard
				foreach (SimCardTypeEnum x in new SimCardTypeEnum[] { SimCardTypeEnum.C2G, SimCardTypeEnum.C3G })
				{
					driver.Configure.Cell.Security.SimCard = x;
					SimCardTypeEnum value = driver.Configure.Cell.Security.SimCard;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RCAuse:LOCation
				foreach (RejectionCause1enum x in new RejectionCause1enum[] { RejectionCause1enum.C100, RejectionCause1enum.C101, RejectionCause1enum.C11, RejectionCause1enum.C111, RejectionCause1enum.C12, RejectionCause1enum.C13, RejectionCause1enum.C15, RejectionCause1enum.C17, RejectionCause1enum.C2, RejectionCause1enum.C20, RejectionCause1enum.C21, RejectionCause1enum.C22, RejectionCause1enum.C23, RejectionCause1enum.C25, RejectionCause1enum.C3, RejectionCause1enum.C32, RejectionCause1enum.C33, RejectionCause1enum.C34, RejectionCause1enum.C38, RejectionCause1enum.C4, RejectionCause1enum.C48, RejectionCause1enum.C5, RejectionCause1enum.C6, RejectionCause1enum.C95, RejectionCause1enum.C96, RejectionCause1enum.C97, RejectionCause1enum.C98, RejectionCause1enum.C99, RejectionCause1enum.OFF, RejectionCause1enum.ON })
				{
					driver.Configure.Cell.Rcause.Location = x;
					RejectionCause1enum value = driver.Configure.Cell.Rcause.Location;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RCAuse:ATTach
				foreach (RejectionCause2enum x in new RejectionCause2enum[] { RejectionCause2enum.C10, RejectionCause2enum.C100, RejectionCause2enum.C101, RejectionCause2enum.C11, RejectionCause2enum.C111, RejectionCause2enum.C12, RejectionCause2enum.C13, RejectionCause2enum.C14, RejectionCause2enum.C15, RejectionCause2enum.C16, RejectionCause2enum.C17, RejectionCause2enum.C2, RejectionCause2enum.C20, RejectionCause2enum.C21, RejectionCause2enum.C22, RejectionCause2enum.C23, RejectionCause2enum.C25, RejectionCause2enum.C28, RejectionCause2enum.C3, RejectionCause2enum.C32, RejectionCause2enum.C33, RejectionCause2enum.C34, RejectionCause2enum.C38, RejectionCause2enum.C4, RejectionCause2enum.C40, RejectionCause2enum.C48, RejectionCause2enum.C5, RejectionCause2enum.C6, RejectionCause2enum.C7, RejectionCause2enum.C8, RejectionCause2enum.C9, RejectionCause2enum.C95, RejectionCause2enum.C96, RejectionCause2enum.C97, RejectionCause2enum.C98, RejectionCause2enum.C99, RejectionCause2enum.OFF, RejectionCause2enum.ON })
				{
					driver.Configure.Cell.Rcause.Attach = x;
					RejectionCause2enum value = driver.Configure.Cell.Rcause.Attach;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RCAuse:RAUPdate
				foreach (RejectionCause2enum x in new RejectionCause2enum[] { RejectionCause2enum.C10, RejectionCause2enum.C100, RejectionCause2enum.C101, RejectionCause2enum.C11, RejectionCause2enum.C111, RejectionCause2enum.C12, RejectionCause2enum.C13, RejectionCause2enum.C14, RejectionCause2enum.C15, RejectionCause2enum.C16, RejectionCause2enum.C17, RejectionCause2enum.C2, RejectionCause2enum.C20, RejectionCause2enum.C21, RejectionCause2enum.C22, RejectionCause2enum.C23, RejectionCause2enum.C25, RejectionCause2enum.C28, RejectionCause2enum.C3, RejectionCause2enum.C32, RejectionCause2enum.C33, RejectionCause2enum.C34, RejectionCause2enum.C38, RejectionCause2enum.C4, RejectionCause2enum.C40, RejectionCause2enum.C48, RejectionCause2enum.C5, RejectionCause2enum.C6, RejectionCause2enum.C7, RejectionCause2enum.C8, RejectionCause2enum.C9, RejectionCause2enum.C95, RejectionCause2enum.C96, RejectionCause2enum.C97, RejectionCause2enum.C98, RejectionCause2enum.C99, RejectionCause2enum.OFF, RejectionCause2enum.ON })
				{
					driver.Configure.Cell.Rcause.RaUpdate = x;
					RejectionCause2enum value = driver.Configure.Cell.Rcause.RaUpdate;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RCAuse:CSRequest
				foreach (RejectionCause1enum x in new RejectionCause1enum[] { RejectionCause1enum.C100, RejectionCause1enum.C101, RejectionCause1enum.C11, RejectionCause1enum.C111, RejectionCause1enum.C12, RejectionCause1enum.C13, RejectionCause1enum.C15, RejectionCause1enum.C17, RejectionCause1enum.C2, RejectionCause1enum.C20, RejectionCause1enum.C21, RejectionCause1enum.C22, RejectionCause1enum.C23, RejectionCause1enum.C25, RejectionCause1enum.C3, RejectionCause1enum.C32, RejectionCause1enum.C33, RejectionCause1enum.C34, RejectionCause1enum.C38, RejectionCause1enum.C4, RejectionCause1enum.C48, RejectionCause1enum.C5, RejectionCause1enum.C6, RejectionCause1enum.C95, RejectionCause1enum.C96, RejectionCause1enum.C97, RejectionCause1enum.C98, RejectionCause1enum.C99, RejectionCause1enum.OFF, RejectionCause1enum.ON })
				{
					driver.Configure.Cell.Rcause.CsRequest = x;
					RejectionCause1enum value = driver.Configure.Cell.Rcause.CsRequest;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:RCAuse:CSTYpe
				foreach (CmSerRejectTypeEnum x in new CmSerRejectTypeEnum[] { CmSerRejectTypeEnum.ECALl, CmSerRejectTypeEnum.ECSMs, CmSerRejectTypeEnum.NCALl, CmSerRejectTypeEnum.NCECall, CmSerRejectTypeEnum.NCSMs, CmSerRejectTypeEnum.NESMs, CmSerRejectTypeEnum.SMS })
				{
					driver.Configure.Cell.Rcause.CsType = x;
					CmSerRejectTypeEnum value = driver.Configure.Cell.Rcause.CsType;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:MNC:DIGits
				foreach (DigitsCountEnum x in new DigitsCountEnum[] { DigitsCountEnum.THRee, DigitsCountEnum.TWO })
				{
					driver.Configure.Cell.Mnc.Digits = x;
					DigitsCountEnum value = driver.Configure.Cell.Mnc.Digits;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:MNC
				int value = driver.Configure.Cell.Mnc.Value;
				driver.Configure.Cell.Mnc.Value = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:RTMS[:CSWitched]
				int value = driver.Configure.Cell.Rtms.Cswitched;
				driver.Configure.Cell.Rtms.Cswitched = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:RTBS[:CSWitched]
				int value = driver.Configure.Cell.Rtbs.Cswitched;
				driver.Configure.Cell.Rtbs.Cswitched = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:ATIMeout[:MTC]
				int value = driver.Configure.Cell.Atimeout.Mtc;
				driver.Configure.Cell.Atimeout.Mtc = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:ATIMeout:MOC
				int value = driver.Configure.Cell.Atimeout.Moc;
				driver.Configure.Cell.Atimeout.Moc = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Cell.Time.Tsource = x;
					SourceTimeEnum value = driver.Configure.Cell.Time.Tsource;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:DATE
				RsCmwGsmSig_Configure_Cell_Time.Date_Data value = driver.Configure.Cell.Time.Date;
				driver.Configure.Cell.Time.Date = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:TIME
				RsCmwGsmSig_Configure_Cell_Time.Time_Data value = driver.Configure.Cell.Time.Time;
				driver.Configure.Cell.Time.Time = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:DSTime
				foreach (DsTimeEnum x in new DsTimeEnum[] { DsTimeEnum.OFF, DsTimeEnum.ON, DsTimeEnum.P1H, DsTimeEnum.P2H })
				{
					driver.Configure.Cell.Time.DaylightSavingTime = x;
					DsTimeEnum value = driver.Configure.Cell.Time.DaylightSavingTime;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:TIME:LTZoffset
				double value = driver.Configure.Cell.Time.LtzOffset;
				driver.Configure.Cell.Time.LtzOffset = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:SATTach
				bool value = driver.Configure.Cell.Time.Sattach;
				driver.Configure.Cell.Time.Sattach = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CELL:TIME:SNName
				bool value = driver.Configure.Cell.Time.Snname;
				driver.Configure.Cell.Time.Snname = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:TIME:SNOW
				driver.Configure.Cell.Time.Snow.Set();
				driver.Configure.Cell.Time.Snow.SetAndWait();
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:SYNC:ZONE
				foreach (SyncZoneEnum x in new SyncZoneEnum[] { SyncZoneEnum.NONE, SyncZoneEnum.Z1 })
				{
					driver.Configure.Cell.Sync.Zone = x;
					SyncZoneEnum value = driver.Configure.Cell.Sync.Zone;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:CELL:SYNC:OFFSet
				double value = driver.Configure.Cell.Sync.Offset;
				driver.Configure.Cell.Sync.Offset = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:TRIGger:FTMode
				foreach (FrameTriggerModEnum x in new FrameTriggerModEnum[] { FrameTriggerModEnum.EVERy, FrameTriggerModEnum.EWIDle, FrameTriggerModEnum.M104, FrameTriggerModEnum.M26, FrameTriggerModEnum.M52 })
				{
					driver.Configure.Trigger.Ftmode = x;
					FrameTriggerModEnum value = driver.Configure.Trigger.Ftmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:RREPort:CSWitched:EMReport:ENABle
				bool value = driver.Configure.Rreport.Cswitched.EmReport.Enable;
				driver.Configure.Rreport.Cswitched.EmReport.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:SDOMain
				foreach (SmsDomainEnum x in new SmsDomainEnum[] { SmsDomainEnum.AUTO, SmsDomainEnum.CS, SmsDomainEnum.PS })
				{
					driver.Configure.Sms.Outgoing.Sdomain = x;
					SmsDomainEnum value = driver.Configure.Sms.Outgoing.Sdomain;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:INTernal
				string value = driver.Configure.Sms.Outgoing.Internal;
				driver.Configure.Sms.Outgoing.Internal = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:BINary
				double value = driver.Configure.Sms.Outgoing.Binary;
				driver.Configure.Sms.Outgoing.Binary = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:DCODing
				foreach (SmsDataCodingEnum x in new SmsDataCodingEnum[] { SmsDataCodingEnum.BIT7, SmsDataCodingEnum.BIT8 })
				{
					driver.Configure.Sms.Outgoing.Dcoding = x;
					SmsDataCodingEnum value = driver.Configure.Sms.Outgoing.Dcoding;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:CGRoup
				foreach (CodingGroupEnum x in new CodingGroupEnum[] { CodingGroupEnum.DCMClass, CodingGroupEnum.GDCoding })
				{
					driver.Configure.Sms.Outgoing.Cgroup = x;
					CodingGroupEnum value = driver.Configure.Sms.Outgoing.Cgroup;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:MCLass
				foreach (MessageClassEnum x in new MessageClassEnum[] { MessageClassEnum.CL0, MessageClassEnum.CL1, MessageClassEnum.CL2, MessageClassEnum.CL3, MessageClassEnum.NONE })
				{
					driver.Configure.Sms.Outgoing.Mclass = x;
					MessageClassEnum value = driver.Configure.Sms.Outgoing.Mclass;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:OSADdress
				string value = driver.Configure.Sms.Outgoing.OsAddress;
				driver.Configure.Sms.Outgoing.OsAddress = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:OADDress
				string value = driver.Configure.Sms.Outgoing.Oaddress;
				driver.Configure.Sms.Outgoing.Oaddress = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:UDHeader
				double value = driver.Configure.Sms.Outgoing.Udheader;
				driver.Configure.Sms.Outgoing.Udheader = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:SMS:OUTGoing:PIDentifier
				double value = driver.Configure.Sms.Outgoing.Pidentifier;
				driver.Configure.Sms.Outgoing.Pidentifier = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Sms.Outgoing.SctStamp.Tsource = x;
					SourceTimeEnum value = driver.Configure.Sms.Outgoing.SctStamp.Tsource;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:DATE
				RsCmwGsmSig_Configure_Sms_Outgoing_SctStamp.Date_Data value = driver.Configure.Sms.Outgoing.SctStamp.Date;
				driver.Configure.Sms.Outgoing.SctStamp.Date = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:TIME
				RsCmwGsmSig_Configure_Sms_Outgoing_SctStamp.Time_Data value = driver.Configure.Sms.Outgoing.SctStamp.Time;
				driver.Configure.Sms.Outgoing.SctStamp.Time = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:CBCH:ENABle
				bool value = driver.Configure.Cbs.Cbch.Enable;
				driver.Configure.Cbs.Cbch.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:DRX:ENABle
				bool value = driver.Configure.Cbs.Drx.Enable;
				driver.Configure.Cbs.Drx.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:DRX:LENGth
				int value = driver.Configure.Cbs.Drx.Length;
				driver.Configure.Cbs.Drx.Length = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:DRX:OFFSet
				int value = driver.Configure.Cbs.Drx.Offset;
				driver.Configure.Cbs.Drx.Offset = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:ENABle
				bool value = driver.Configure.Cbs.Message.Enable;
				driver.Configure.Cbs.Message.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:ID
				int value = driver.Configure.Cbs.Message.Id;
				driver.Configure.Cbs.Message.Id = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:IDTYpe
				foreach (MsgIdSeverityEnum x in new MsgIdSeverityEnum[] { MsgIdSeverityEnum.AAMBer, MsgIdSeverityEnum.AEXTreme, MsgIdSeverityEnum.APResidentia, MsgIdSeverityEnum.ASEVere, MsgIdSeverityEnum.UDEFined })
				{
					driver.Configure.Cbs.Message.Idtype = x;
					MsgIdSeverityEnum value = driver.Configure.Cbs.Message.Idtype;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:SERial
				RsCmwGsmSig_Configure_Cbs_Message.Serial_Data value = driver.Configure.Cbs.Message.Serial;
				driver.Configure.Cbs.Message.Serial = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:DCSCheme
				int value = driver.Configure.Cbs.Message.DcScheme;
				driver.Configure.Cbs.Message.DcScheme = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:CATegory
				foreach (PriorityEnum x in new PriorityEnum[] { PriorityEnum.BACKground, PriorityEnum.HIGH, PriorityEnum.NORMal })
				{
					driver.Configure.Cbs.Message.Category = x;
					PriorityEnum value = driver.Configure.Cbs.Message.Category;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:DATA
				string value = driver.Configure.Cbs.Message.Data;
				driver.Configure.Cbs.Message.Data = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CBS:MESSage:PERiod
				int value = driver.Configure.Cbs.Message.Period;
				driver.Configure.Cbs.Message.Period = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:TOUT
				double value = driver.Configure.Ber.Cswitched.Timeout;
				driver.Configure.Ber.Cswitched.Timeout = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:MMODe
				foreach (BerCsMeasModeEnum x in new BerCsMeasModeEnum[] { BerCsMeasModeEnum.AIFer, BerCsMeasModeEnum.BBBurst, BerCsMeasModeEnum.BER, BerCsMeasModeEnum.BFI, BerCsMeasModeEnum.FFACch, BerCsMeasModeEnum.FSACch, BerCsMeasModeEnum.MBEP, BerCsMeasModeEnum.RFER, BerCsMeasModeEnum.RUFR, BerCsMeasModeEnum.SQUality })
				{
					driver.Configure.Ber.Cswitched.Mmode = x;
					BerCsMeasModeEnum value = driver.Configure.Ber.Cswitched.Mmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:SCONdition
				int value = driver.Configure.Ber.Cswitched.Scondition;
				driver.Configure.Ber.Cswitched.Scondition = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:SCOunt
				int value = driver.Configure.Ber.Cswitched.Scount;
				driver.Configure.Ber.Cswitched.Scount = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:RTDelay
				RsCmwGsmSig_Configure_Ber_Cswitched.Rtdelay_Data value = driver.Configure.Ber.Cswitched.Rtdelay;
				driver.Configure.Ber.Cswitched.Rtdelay = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:BER
				double value = driver.Configure.Ber.Cswitched.Limit.Ber;
				driver.Configure.Ber.Cswitched.Limit.Ber = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:CIIBits
				double value = driver.Configure.Ber.Cswitched.Limit.CiiBits;
				driver.Configure.Ber.Cswitched.Limit.CiiBits = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:CIBBits
				double value = driver.Configure.Ber.Cswitched.Limit.CibBits;
				driver.Configure.Ber.Cswitched.Limit.CibBits = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:FER
				double value = driver.Configure.Ber.Cswitched.Limit.Fer;
				driver.Configure.Ber.Cswitched.Limit.Fer = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:FFACch
				double value = driver.Configure.Ber.Cswitched.Limit.Ffacch;
				driver.Configure.Ber.Cswitched.Limit.Ffacch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:CSWitched:LIMit:FSACch
				double value = driver.Configure.Ber.Cswitched.Limit.Fsacch;
				driver.Configure.Ber.Cswitched.Limit.Fsacch = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:TOUT
				double value = driver.Configure.Ber.Pswitched.Timeout;
				driver.Configure.Ber.Pswitched.Timeout = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:MMODe
				foreach (BerPsMeasModeEnum x in new BerPsMeasModeEnum[] { BerPsMeasModeEnum.BDBLer, BerPsMeasModeEnum.MBEP, BerPsMeasModeEnum.UBONly })
				{
					driver.Configure.Ber.Pswitched.Mmode = x;
					BerPsMeasModeEnum value = driver.Configure.Ber.Pswitched.Mmode;
				}
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:SCONdition
				int value = driver.Configure.Ber.Pswitched.Scondition;
				driver.Configure.Ber.Pswitched.Scondition = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:SCOunt
				int value = driver.Configure.Ber.Pswitched.Scount;
				driver.Configure.Ber.Pswitched.Scount = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:LIMit:CIIBits
				double value = driver.Configure.Ber.Pswitched.Limit.CiiBits;
				driver.Configure.Ber.Pswitched.Limit.CiiBits = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:LIMit:DBLer
				double value = driver.Configure.Ber.Pswitched.Limit.Dbler;
				driver.Configure.Ber.Pswitched.Limit.Dbler = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BER:PSWitched:LIMit:USFBler
				double value = driver.Configure.Ber.Pswitched.Limit.Usfbler;
				driver.Configure.Ber.Pswitched.Limit.Usfbler = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BLER:TOUT
				double value = driver.Configure.Bler.Timeout;
				driver.Configure.Bler.Timeout = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:BLER:SCOunt
				int value = driver.Configure.Bler.Scount;
				driver.Configure.Bler.Scount = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:THRoughput:TOUT
				double value = driver.Configure.Throughput.Timeout;
				driver.Configure.Throughput.Timeout = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:THRoughput:WINDow
				int value = driver.Configure.Throughput.Window;
				driver.Configure.Throughput.Window = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:THRoughput:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Throughput.Repetition = x;
					RepeatEnum value = driver.Configure.Throughput.Repetition;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CPERformance:TOUT
				double value = driver.Configure.Cperformance.Timeout;
				driver.Configure.Cperformance.Timeout = value;
			}
			{	// CONFigure:GSM:SIGNaling<instance>:CPERformance:TLEVel
				double value = driver.Configure.Cperformance.Tlevel;
				driver.Configure.Cperformance.Tlevel = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:MMONitor:IPADdress
				RsCmwGsmSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:GSM:SIGNaling<Instance>:MMONitor:IPADdress
				foreach (IpAddrIndexEnum x in new IpAddrIndexEnum[] { IpAddrIndexEnum.IP1, IpAddrIndexEnum.IP2, IpAddrIndexEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:MSReport:WMQuantity
				foreach (WmQuantityEnum x in new WmQuantityEnum[] { WmQuantityEnum.ECNO, WmQuantityEnum.RSCP })
				{
					driver.Configure.MsReport.WmQuantity = x;
					WmQuantityEnum value = driver.Configure.MsReport.WmQuantity;
				}
			}
			{	// CONFigure:GSM:SIGNaling<instance>:MSReport:LMQuantity
				foreach (LmQuantityEnum x in new LmQuantityEnum[] { LmQuantityEnum.RSRP, LmQuantityEnum.RSRQ })
				{
					driver.Configure.MsReport.LmQuantity = x;
					LmQuantityEnum value = driver.Configure.MsReport.LmQuantity;
				}
			}
			{	// SENSe:GSM:SIGNaling<instance>:CVINfo
				RsCmwGsmSig_Sense.CvInfo_Data value = driver.Sense.CvInfo;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:BAND:TCH
				foreach (OperBandGsmEnum x in new OperBandGsmEnum[] { OperBandGsmEnum.G04, OperBandGsmEnum.G085, OperBandGsmEnum.G09, OperBandGsmEnum.G18, OperBandGsmEnum.G19, OperBandGsmEnum.GT081 })
				{
					OperBandGsmEnum value = driver.Sense.Band.Tch;
				}
			}
			{	// SENSe:GSM:SIGNaling<instance>:IQOut:PATH<n>
				RsCmwGsmSig_Sense_IqOut.GetPath_Data value = driver.Sense.IqOut.GetPath(PathRepCap.Nr1);
				value = driver.Sense.IqOut.GetPath();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CONNection:CSWitched:CONNection:ATTempt
				int value = driver.Sense.Connection.Cswitched.Connection.Attempt;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CONNection:CSWitched:CONNection:REJect
				int value = driver.Sense.Connection.Cswitched.Connection.Reject;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CONNection:ETHRoughput:UL
				double value = driver.Sense.Connection.Ethroughput.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CONNection:ETHRoughput:DL
				double value = driver.Sense.Connection.Ethroughput.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:RXPower
				foreach (RxPowerEnum x in new RxPowerEnum[] { RxPowerEnum.INV, RxPowerEnum.NAV, RxPowerEnum.NCAP, RxPowerEnum.OFL, RxPowerEnum.OK, RxPowerEnum.UFL })
				{
					RxPowerEnum value = driver.Sense.MssInfo.RxPower;
				}
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:APN
				List<string> value = driver.Sense.MssInfo.Apn;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:IMSI
				string value = driver.Sense.MssInfo.Imsi;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:IMEI
				string value = driver.Sense.MssInfo.Imei;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:DNUMber
				string value = driver.Sense.MssInfo.Dnumber;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:TTY
				string value = driver.Sense.MssInfo.Tty;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:SCATegory
				RsCmwGsmSig_Sense_MssInfo.Scategory_Data value = driver.Sense.MssInfo.Scategory;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:BANDs
				RsCmwGsmSig_Sense_MssInfo.Bands_Data value = driver.Sense.MssInfo.Bands;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:EDALlocation
				RsCmwGsmSig_Sense_MssInfo.EdAllocation_Data value = driver.Sense.MssInfo.EdAllocation;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:FRATe:GMSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Frate.Gmsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:FRATe:GMSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Frate.Gmsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:HRATe:GMSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Hrate.Gmsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:HRATe:GMSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Hrate.Gmsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:HRATe:EPSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Hrate.Epsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:NB:HRATe:EPSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Nb.Hrate.Epsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:FRATe:GMSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Frate.Gmsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:FRATe:GMSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Frate.Gmsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:FRATe:EPSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Frate.Epsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:FRATe:EPSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Frate.Epsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:HRATe:EPSK:DL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Hrate.Epsk.Downlink;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:AMR:CMODe:WB:HRATe:EPSK:UL
				int value = driver.Sense.MssInfo.Amr.Cmode.Wb.Hrate.Epsk.Uplink;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:MSADdress:IPV<n>
				List<string> value = driver.Sense.MssInfo.MsAddress.GetIpv(IPversionRepCap.IPv4);
				value = driver.Sense.MssInfo.MsAddress.GetIpv();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:MSCLass:GPRS
				int value = driver.Sense.MssInfo.MsClass.Gprs;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:MSCLass:EGPRs
				int value = driver.Sense.MssInfo.MsClass.Egprs;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:MSCLass:DGPRs
				int value = driver.Sense.MssInfo.MsClass.Dgprs;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:MSSinfo:MSCLass:DEGPrs
				int value = driver.Sense.MssInfo.MsClass.Degprs;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:CODec:GSM
				List<bool> value = driver.Sense.MssInfo.Codec.Gsm;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:CODec:UMTS
				List<bool> value = driver.Sense.MssInfo.Codec.Umts;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:VAMos:LEVel
				int value = driver.Sense.MssInfo.Vamos.Level;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:TCAPability:SSCHannels
				bool value = driver.Sense.MssInfo.Tcapability.SsChannels;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:TCAPability:GEGPrs
				bool value = driver.Sense.MssInfo.Tcapability.Gegprs;
			}
			{	// SENSe:GSM:SIGNaling<instance>:MSSinfo:TCAPability:ETWO
				bool value = driver.Sense.MssInfo.Tcapability.Etwo;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CELL:FNUMber
				int value = driver.Sense.Cell.Fnumber;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CELL:CERRor
				foreach (ConnectErrorEnum x in new ConnectErrorEnum[] { ConnectErrorEnum.ATIMeout, ConnectErrorEnum.IGNored, ConnectErrorEnum.NERRor, ConnectErrorEnum.PTIMeout, ConnectErrorEnum.REJected, ConnectErrorEnum.RLTimeout, ConnectErrorEnum.STIMeout })
				{
					ConnectErrorEnum value = driver.Sense.Cell.Cerror;
				}
			}
			{	// SENSe:GSM:SIGNaling<Instance>:CELL:PSWitched:CERRor
				foreach (ConnectErrorEnum x in new ConnectErrorEnum[] { ConnectErrorEnum.ATIMeout, ConnectErrorEnum.IGNored, ConnectErrorEnum.NERRor, ConnectErrorEnum.PTIMeout, ConnectErrorEnum.REJected, ConnectErrorEnum.RLTimeout, ConnectErrorEnum.STIMeout })
				{
					ConnectErrorEnum value = driver.Sense.Cell.Pswitched.Cerror;
				}
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:COUNt
				int value = driver.Sense.Rreport.Count;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CSWitched:NRBLocks
				int value = driver.Sense.Rreport.Cswitched.NrBlocks;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CSWitched:MBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_Cswitched_Mbep.Range_Data value = driver.Sense.Rreport.Cswitched.Mbep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CSWitched:MBEP
				int value = driver.Sense.Rreport.Cswitched.Mbep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CSWitched:CBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_Cswitched_Cbep.Range_Data value = driver.Sense.Rreport.Cswitched.Cbep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CSWitched:CBEP
				int value = driver.Sense.Rreport.Cswitched.Cbep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXLevel:RANGe
				RsCmwGsmSig_Sense_Rreport_RxLevel.Range_Data value = driver.Sense.Rreport.RxLevel.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXLevel
				int value = driver.Sense.Rreport.RxLevel.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXLevel:SUB:RANGe
				RsCmwGsmSig_Sense_Rreport_RxLevel_Sub.Range_Data value = driver.Sense.Rreport.RxLevel.Sub.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXLevel:SUB
				int value = driver.Sense.Rreport.RxLevel.Sub.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXQuality:RANGe
				RsCmwGsmSig_Sense_Rreport_RxQuality.Range_Data value = driver.Sense.Rreport.RxQuality.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXQuality
				int value = driver.Sense.Rreport.RxQuality.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXQuality:SUB:RANGe
				RsCmwGsmSig_Sense_Rreport_RxQuality_Sub.Range_Data value = driver.Sense.Rreport.RxQuality.Sub.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:RXQuality:SUB
				int value = driver.Sense.Rreport.RxQuality.Sub.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CVALue:RANGe
				RsCmwGsmSig_Sense_Rreport_Cvalue.Range_Data value = driver.Sense.Rreport.Cvalue.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:CVALue
				int value = driver.Sense.Rreport.Cvalue.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:SVARiance:RANGe
				RsCmwGsmSig_Sense_Rreport_Svariance.Range_Data value = driver.Sense.Rreport.Svariance.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:SVARiance
				int value = driver.Sense.Rreport.Svariance.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:GMBep:RANGe
				RsCmwGsmSig_Sense_Rreport_Gmbep.Range_Data value = driver.Sense.Rreport.Gmbep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:GMBep
				int value = driver.Sense.Rreport.Gmbep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:GCBep:RANGe
				RsCmwGsmSig_Sense_Rreport_Gcbep.Range_Data value = driver.Sense.Rreport.Gcbep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:GCBep
				int value = driver.Sense.Rreport.Gcbep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:EMBep:RANGe
				RsCmwGsmSig_Sense_Rreport_Embep.Range_Data value = driver.Sense.Rreport.Embep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:EMBep
				int value = driver.Sense.Rreport.Embep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:ECBep:RANGe
				RsCmwGsmSig_Sense_Rreport_Ecbep.Range_Data value = driver.Sense.Rreport.Ecbep.Range;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:ECBep
				int value = driver.Sense.Rreport.Ecbep.Value;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:NSRQam<ModOrder>:MBEP
				int value = driver.Sense.Rreport.Nsrqam.GetMbep(NsrQAMRepCap.Default);
				value = driver.Sense.Rreport.Nsrqam.GetMbep();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:NSRQam<ModOrder>:CBEP
				int value = driver.Sense.Rreport.Nsrqam.GetCbep(NsrQAMRepCap.Default);
				value = driver.Sense.Rreport.Nsrqam.GetCbep();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:NSRQam<ModOrder>:MBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_Nsrqam_Mbep.GetRange_Data value = driver.Sense.Rreport.Nsrqam.Mbep.GetRange(NsrQAMRepCap.Default);
				value = driver.Sense.Rreport.Nsrqam.Mbep.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:NSRQam<ModOrder>:CBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_Nsrqam_Cbep.GetRange_Data value = driver.Sense.Rreport.Nsrqam.Cbep.GetRange(NsrQAMRepCap.Default);
				value = driver.Sense.Rreport.Nsrqam.Cbep.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:HSRQam<ModOrder>:MBEP
				int value = driver.Sense.Rreport.HsrQam.GetMbep(HsrQAMRepCap.Default);
				value = driver.Sense.Rreport.HsrQam.GetMbep();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:HSRQam<ModOrder>:CBEP
				int value = driver.Sense.Rreport.HsrQam.GetCbep(HsrQAMRepCap.Default);
				value = driver.Sense.Rreport.HsrQam.GetCbep();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:HSRQam<ModOrder>:MBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_HsrQam_Mbep.GetRange_Data value = driver.Sense.Rreport.HsrQam.Mbep.GetRange(HsrQAMRepCap.Default);
				value = driver.Sense.Rreport.HsrQam.Mbep.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RREPort:HSRQam<ModOrder>:CBEP:RANGe
				RsCmwGsmSig_Sense_Rreport_HsrQam_Cbep.GetRange_Data value = driver.Sense.Rreport.HsrQam.Cbep.GetRange(HsrQAMRepCap.Default);
				value = driver.Sense.Rreport.HsrQam.Cbep.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:LTE:CELL<nr>
				RsCmwGsmSig_Sense_Rreport_Ncell_Lte.GetCell_Data value = driver.Sense.Rreport.Ncell.Lte.GetCell(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Lte.GetCell();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:LTE:CELL<nr>:RANGe
				RsCmwGsmSig_Sense_Rreport_Ncell_Lte_Cell.GetRange_Data value = driver.Sense.Rreport.Ncell.Lte.Cell.GetRange(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Lte.Cell.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:GSM:CELL<nr>
				int value = driver.Sense.Rreport.Ncell.Gsm.GetCell(GsmCellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Gsm.GetCell();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:GSM:CELL<nr>:RANGe
				RsCmwGsmSig_Sense_Rreport_Ncell_Gsm_Cell.GetRange_Data value = driver.Sense.Rreport.Ncell.Gsm.Cell.GetRange(GsmCellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Gsm.Cell.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:WCDMa:CELL<nr>
				RsCmwGsmSig_Sense_Rreport_Ncell_Wcdma.GetCell_Data value = driver.Sense.Rreport.Ncell.Wcdma.GetCell(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Wcdma.GetCell();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:WCDMa:CELL<nr>:RANGe
				RsCmwGsmSig_Sense_Rreport_Ncell_Wcdma_Cell.GetRange_Data value = driver.Sense.Rreport.Ncell.Wcdma.Cell.GetRange(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Wcdma.Cell.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:TDSCdma:CELL<nr>
				int value = driver.Sense.Rreport.Ncell.Tdscdma.GetCell(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Tdscdma.GetCell();
			}
			{	// SENSe:GSM:SIGNaling<instance>:RREPort:NCELl:TDSCdma:CELL<nr>:RANGe
				RsCmwGsmSig_Sense_Rreport_Ncell_Tdscdma_Cell.GetRange_Data value = driver.Sense.Rreport.Ncell.Tdscdma.Cell.GetRange(CellNoRepCap.Nr1);
				value = driver.Sense.Rreport.Ncell.Tdscdma.Cell.GetRange();
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:OUTGoing:INFO:SEGMent
				RsCmwGsmSig_Sense_Sms_Outgoing_Info.Segment_Data value = driver.Sense.Sms.Outgoing.Info.Segment;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:OUTGoing:INFO:LMSent
				foreach (LastMessageSentEnum x in new LastMessageSentEnum[] { LastMessageSentEnum.FAILed, LastMessageSentEnum.OFF, LastMessageSentEnum.ON, LastMessageSentEnum.SUCCessful })
				{
					LastMessageSentEnum value = driver.Sense.Sms.Outgoing.Info.Lmsent;
				}
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:INComing:INFO:DCODing
				string value = driver.Sense.Sms.Incoming.Info.Dcoding;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:INComing:INFO:MTEXt
				string value = driver.Sense.Sms.Incoming.Info.Mtext;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:INComing:INFO:MLENgth
				int value = driver.Sense.Sms.Incoming.Info.Mlength;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:INComing:INFO:SEGMent
				RsCmwGsmSig_Sense_Sms_Incoming_Info.Segment_Data value = driver.Sense.Sms.Incoming.Info.Segment;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:SMS:INFO:LRMessage:RFLag
				bool value = driver.Sense.Sms.Info.LrMessage.Rflag;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:BER:CSWitched:RTDelay
				int value = driver.Sense.Ber.Cswitched.Rtdelay;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RFSettings:EPOWer
				double value = driver.Sense.RfSettings.Epower;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:RFSettings:EFRequency
				double value = driver.Sense.RfSettings.Efrequency;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:ELOG:LAST
				RsCmwGsmSig_Sense_Elog.Last_Data value = driver.Sense.Elog.Last;
			}
			{	// SENSe:GSM:SIGNaling<Instance>:ELOG:ALL
				RsCmwGsmSig_Sense_Elog.All_Data value = driver.Sense.Elog.All;
			}
			{	// CLEan:GSM:SIGNaling<Instance>:CONNection:CSWitched:CONNection:ATTempt
				driver.Clean.Connection.Cswitched.Connection.Attempt.Set();
				driver.Clean.Connection.Cswitched.Connection.Attempt.SetAndWait();
			}
			{	// CLEan:GSM:SIGNaling<Instance>:CONNection:CSWitched:CONNection:REJect
				driver.Clean.Connection.Cswitched.Connection.Reject.Set();
				driver.Clean.Connection.Cswitched.Connection.Reject.SetAndWait();
			}
			{	// CLEan:GSM:SIGNaling<Instance>:SMS:INComing:INFO:MTEXt
				driver.Clean.Sms.Incoming.Info.Mtext.Set();
				driver.Clean.Sms.Incoming.Info.Mtext.SetAndWait();
			}
			{	// CLEan:GSM:SIGNaling<Instance>:ELOG
				driver.Clean.Elog.Set();
				driver.Clean.Elog.SetAndWait();
			}
			{	// SOURce:GSM:SIGNaling<Instance>:CELL:STATe:ALL
				RsCmwGsmSig_Source_Cell_State.All_Data value = driver.Source.Cell.State.All;
			}
			{	// SOURce:GSM:SIGNaling<Instance>:CELL:STATe
				bool value = driver.Source.Cell.State.Value;
				driver.Source.Cell.State.Value = value;
			}
			{	// CALL:GSM:SIGNaling<Instance>:CSWitched:ACTion
				foreach (CswActionEnum x in new CswActionEnum[] { CswActionEnum.CONNect, CswActionEnum.DISConnect, CswActionEnum.HANDover, CswActionEnum.OFF, CswActionEnum.ON, CswActionEnum.SMS })
				{
					driver.Call.Cswitched.Action = x;					
				}
			}
			{	// CALL:GSM:SIGNaling<Instance>:PSWitched:ACTion
				foreach (PswActionEnum x in new PswActionEnum[] { PswActionEnum.CONNect, PswActionEnum.DISConnect, PswActionEnum.HANDover, PswActionEnum.OFF, PswActionEnum.ON, PswActionEnum.RPContext, PswActionEnum.SMS })
				{
					driver.Call.Pswitched.Action = x;					
				}
			}
			{	// CALL:GSM:SIGNaling<Instance>:HANDover:STARt
				driver.Call.Handover.Start();
				driver.Call.Handover.StartAndWait();
			}
			{	// FETCh:GSM:SIGNaling<Instance>:CSWitched:STATe
				CswStateEnum value = driver.Cswitched.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:PSWitched:STATe
				PswStateEnum value = driver.Pswitched.State.Fetch();				
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:DESTination
				string value = driver.Prepare.Handover.Destination;
				driver.Prepare.Handover.Destination = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:MMODe
				foreach (HandoverModeEnum x in new HandoverModeEnum[] { HandoverModeEnum.CCORder, HandoverModeEnum.DUALband, HandoverModeEnum.HANDover, HandoverModeEnum.REDirection })
				{
					driver.Prepare.Handover.Mmode = x;
					HandoverModeEnum value = driver.Prepare.Handover.Mmode;
				}
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:TARGet
				foreach (OperBandGsmEnum x in new OperBandGsmEnum[] { OperBandGsmEnum.G04, OperBandGsmEnum.G085, OperBandGsmEnum.G09, OperBandGsmEnum.G18, OperBandGsmEnum.G19, OperBandGsmEnum.GT081 })
				{
					driver.Prepare.Handover.Target = x;
					OperBandGsmEnum value = driver.Prepare.Handover.Target;
				}
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PCL
				int value = driver.Prepare.Handover.Pcl;
				driver.Prepare.Handover.Pcl = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:TSLot
				int value = driver.Prepare.Handover.Tslot;
				driver.Prepare.Handover.Tslot = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:CATalog:DESTination
				List<string> value = driver.Prepare.Handover.Catalog.Destination;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:CHANnel:TCH
				int value = driver.Prepare.Handover.Channel.Tch;
				driver.Prepare.Handover.Channel.Tch = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:LEVel:TCH
				double value = driver.Prepare.Handover.Level.Tch;
				driver.Prepare.Handover.Level.Tch = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:ENABle:UL
				List<bool> value = driver.Prepare.Handover.Pswitched.Enable.Uplink;
				driver.Prepare.Handover.Pswitched.Enable.Uplink = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:ENABle:DL:CARRier{carrierCmdVal}
				List<bool> value = driver.Prepare.Handover.Pswitched.Enable.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Prepare.Handover.Pswitched.Enable.Downlink.Carrier.Get();
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:ENABle:DL:CARRier{carrierCmdVal}
				driver.Prepare.Handover.Pswitched.Enable.Downlink.Carrier.Set(new List<bool> { true, false, true }, CarrierRepCap.Default);
				driver.Prepare.Handover.Pswitched.Enable.Downlink.Carrier.Set(new List<bool> { true, false, true });
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:GAMMa:UL
				List<int> value = driver.Prepare.Handover.Pswitched.Gamma.Uplink;
				driver.Prepare.Handover.Pswitched.Gamma.Uplink = value;
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:LEVel:DL:CARRier{carrierCmdVal}
				List<double> value = driver.Prepare.Handover.Pswitched.Level.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Prepare.Handover.Pswitched.Level.Downlink.Carrier.Get();
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:LEVel:DL:CARRier{carrierCmdVal}
				driver.Prepare.Handover.Pswitched.Level.Downlink.Carrier.Set(new List<double> { 1.1, 2.2, 3.3 }, CarrierRepCap.Default);
				driver.Prepare.Handover.Pswitched.Level.Downlink.Carrier.Set(new List<double> { 1.1, 2.2, 3.3 });
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:CSCHeme:UL
				foreach (UplinkCodingSchemeEnum x in new UplinkCodingSchemeEnum[] { UplinkCodingSchemeEnum.C1, UplinkCodingSchemeEnum.C2, UplinkCodingSchemeEnum.C3, UplinkCodingSchemeEnum.C4, UplinkCodingSchemeEnum.MC1, UplinkCodingSchemeEnum.MC2, UplinkCodingSchemeEnum.MC3, UplinkCodingSchemeEnum.MC4, UplinkCodingSchemeEnum.MC5, UplinkCodingSchemeEnum.MC6, UplinkCodingSchemeEnum.MC7, UplinkCodingSchemeEnum.MC8, UplinkCodingSchemeEnum.MC9, UplinkCodingSchemeEnum.OFF, UplinkCodingSchemeEnum.ON, UplinkCodingSchemeEnum.UA10, UplinkCodingSchemeEnum.UA11, UplinkCodingSchemeEnum.UA7, UplinkCodingSchemeEnum.UA8, UplinkCodingSchemeEnum.UA9, UplinkCodingSchemeEnum.UB10, UplinkCodingSchemeEnum.UB11, UplinkCodingSchemeEnum.UB12, UplinkCodingSchemeEnum.UB5, UplinkCodingSchemeEnum.UB6, UplinkCodingSchemeEnum.UB7, UplinkCodingSchemeEnum.UB8, UplinkCodingSchemeEnum.UB9 })
				{
					driver.Prepare.Handover.Pswitched.Cscheme.Uplink = x;
					UplinkCodingSchemeEnum value = driver.Prepare.Handover.Pswitched.Cscheme.Uplink;
				}
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:CSCHeme:DL:CARRier{carrierCmdVal}
				List<DownlinkCodingSchemeEnum> value = driver.Prepare.Handover.Pswitched.Cscheme.Downlink.Carrier.Get(CarrierRepCap.Default);
				value = driver.Prepare.Handover.Pswitched.Cscheme.Downlink.Carrier.Get();
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:CSCHeme:DL:CARRier{carrierCmdVal}
				foreach (DownlinkCodingSchemeEnum x in new DownlinkCodingSchemeEnum[] { DownlinkCodingSchemeEnum.C1, DownlinkCodingSchemeEnum.C2, DownlinkCodingSchemeEnum.C3, DownlinkCodingSchemeEnum.C4, DownlinkCodingSchemeEnum.DA10, DownlinkCodingSchemeEnum.DA11, DownlinkCodingSchemeEnum.DA12, DownlinkCodingSchemeEnum.DA5, DownlinkCodingSchemeEnum.DA6, DownlinkCodingSchemeEnum.DA7, DownlinkCodingSchemeEnum.DA8, DownlinkCodingSchemeEnum.DA9, DownlinkCodingSchemeEnum.DB10, DownlinkCodingSchemeEnum.DB11, DownlinkCodingSchemeEnum.DB12, DownlinkCodingSchemeEnum.DB5, DownlinkCodingSchemeEnum.DB6, DownlinkCodingSchemeEnum.DB7, DownlinkCodingSchemeEnum.DB8, DownlinkCodingSchemeEnum.DB9, DownlinkCodingSchemeEnum.MC1, DownlinkCodingSchemeEnum.MC2, DownlinkCodingSchemeEnum.MC3, DownlinkCodingSchemeEnum.MC4, DownlinkCodingSchemeEnum.MC5, DownlinkCodingSchemeEnum.MC6, DownlinkCodingSchemeEnum.MC7, DownlinkCodingSchemeEnum.MC8, DownlinkCodingSchemeEnum.MC9, DownlinkCodingSchemeEnum.OFF, DownlinkCodingSchemeEnum.ON })
				{
					driver.Prepare.Handover.Pswitched.Cscheme.Downlink.Carrier.Set(new List<DownlinkCodingSchemeEnum> { x, x, x, x, x });
					driver.Prepare.Handover.Pswitched.Cscheme.Downlink.Carrier.Set(new List<DownlinkCodingSchemeEnum> { x, x, x, x, x }, CarrierRepCap.Default);
				}
			}
			{	// PREPare:GSM:SIGNaling<Instance>:HANDover:PSWitched:UDCYcle:DL
				List<int> value = driver.Prepare.Handover.Pswitched.UdCycle.Downlink;
				driver.Prepare.Handover.Pswitched.UdCycle.Downlink = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:DESTination
				foreach (HandoverDestinationEnum x in new HandoverDestinationEnum[] { HandoverDestinationEnum.CDMA, HandoverDestinationEnum.EVDO, HandoverDestinationEnum.GSM, HandoverDestinationEnum.LTE, HandoverDestinationEnum.TDSCdma, HandoverDestinationEnum.WCDMa })
				{
					driver.Prepare.Handover.External.Destination = x;
					HandoverDestinationEnum value = driver.Prepare.Handover.External.Destination;
				}
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:LTE
				RsCmwGsmSig_Prepare_Handover_External.Lte_Data value = driver.Prepare.Handover.External.Lte;
				driver.Prepare.Handover.External.Lte = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:GSM
				RsCmwGsmSig_Prepare_Handover_External.Gsm_Data value = driver.Prepare.Handover.External.Gsm;
				driver.Prepare.Handover.External.Gsm = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:CDMA
				RsCmwGsmSig_Prepare_Handover_External.Cdma_Data value = driver.Prepare.Handover.External.Cdma;
				driver.Prepare.Handover.External.Cdma = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:EVDO
				RsCmwGsmSig_Prepare_Handover_External.Evdo_Data value = driver.Prepare.Handover.External.Evdo;
				driver.Prepare.Handover.External.Evdo = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:WCDMa
				RsCmwGsmSig_Prepare_Handover_External.Wcdma_Data value = driver.Prepare.Handover.External.Wcdma;
				driver.Prepare.Handover.External.Wcdma = value;
			}
			{	// PREPare:GSM:SIGNaling<instance>:HANDover:EXTernal:TDSCdma
				RsCmwGsmSig_Prepare_Handover_External.Tdscdma_Data value = driver.Prepare.Handover.External.Tdscdma;
				driver.Prepare.Handover.External.Tdscdma = value;
			}
			{	// FETCh:GSM:SIGNaling<Instance>:HANDover:STATe
				HandoverStateEnum value = driver.Handover.State.Fetch();				
			}
			{	// INITiate:GSM:SIGNaling<Instance>:BER:CSWitched
				driver.Ber.Cswitched.Initiate();
				driver.Ber.Cswitched.InitiateAndWait();
			}
			{	// STOP:GSM:SIGNaling<Instance>:BER:CSWitched
				driver.Ber.Cswitched.Stop();
				driver.Ber.Cswitched.StopAndWait();
			}
			{	// ABORt:GSM:SIGNaling<Instance>:BER:CSWitched
				driver.Ber.Cswitched.Abort();
				driver.Ber.Cswitched.AbortAndWait();
			}
			{	// READ:GSM:SIGNaling<Instance>:BER:CSWitched
				RsCmwGsmSig_Ber_Cswitched.ResultData value = driver.Ber.Cswitched.Read();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:CSWitched
				RsCmwGsmSig_Ber_Cswitched.ResultData value = driver.Ber.Cswitched.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:CSWitched:STATe
				ResourceStateEnum value = driver.Ber.Cswitched.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:CSWitched:STATe:ALL
				RsCmwGsmSig_Ber_Cswitched_State_All.Fetch_Data value = driver.Ber.Cswitched.State.All.Fetch();				
			}
			{	// INITiate:GSM:SIGNaling<Instance>:BER:PSWitched
				driver.Ber.Pswitched.Initiate();
				driver.Ber.Pswitched.InitiateAndWait();
			}
			{	// STOP:GSM:SIGNaling<Instance>:BER:PSWitched
				driver.Ber.Pswitched.Stop();
				driver.Ber.Pswitched.StopAndWait();
			}
			{	// ABORt:GSM:SIGNaling<Instance>:BER:PSWitched
				driver.Ber.Pswitched.Abort();
				driver.Ber.Pswitched.AbortAndWait();
			}
			{	// READ:GSM:SIGNaling<Instance>:BER:PSWitched
				RsCmwGsmSig_Ber_Pswitched.ResultData value = driver.Ber.Pswitched.Read();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:PSWitched
				RsCmwGsmSig_Ber_Pswitched.ResultData value = driver.Ber.Pswitched.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:PSWitched:STATe
				ResourceStateEnum value = driver.Ber.Pswitched.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:PSWitched:STATe:ALL
				RsCmwGsmSig_Ber_Pswitched_State_All.Fetch_Data value = driver.Ber.Pswitched.State.All.Fetch();				
			}
			{	// READ:GSM:SIGNaling<Instance>:BER:PSWitched:CARRier<Carrier>
				RsCmwGsmSig_Ber_Pswitched_Carrier.ResultData value = driver.Ber.Pswitched.Carrier.Read();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BER:PSWitched:CARRier<Carrier>
				RsCmwGsmSig_Ber_Pswitched_Carrier.ResultData value = driver.Ber.Pswitched.Carrier.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BER:CSWitched
				RsCmwGsmSig_Intermediate_Ber_Cswitched.Fetch_Data value = driver.Intermediate.Ber.Cswitched.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BER:CSWitched:MBEP
				RsCmwGsmSig_Intermediate_Ber_Cswitched_Mbep.Fetch_Data value = driver.Intermediate.Ber.Cswitched.Mbep.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BER:PSWitched
				RsCmwGsmSig_Intermediate_Ber_Pswitched.Fetch_Data value = driver.Intermediate.Ber.Pswitched.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BER:PSWitched:MBEP
				RsCmwGsmSig_Intermediate_Ber_Pswitched_Mbep.Fetch_Data value = driver.Intermediate.Ber.Pswitched.Mbep.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BER:PSWitched:MBEP:ENHanced
				RsCmwGsmSig_Intermediate_Ber_Pswitched_Mbep_Enhanced.Fetch_Data value = driver.Intermediate.Ber.Pswitched.Mbep.Enhanced.Fetch();				
			}
			{	// FETCh:INTermediate:GSM:SIGNaling<Instance>:BLER:OALL
				RsCmwGsmSig_Intermediate_Bler_Oall.Fetch_Data value = driver.Intermediate.Bler.Oall.Fetch();				
			}
			{	// INITiate:GSM:SIGNaling<Instance>:BLER
				driver.Bler.Initiate();
				driver.Bler.InitiateAndWait();
			}
			{	// STOP:GSM:SIGNaling<Instance>:BLER
				driver.Bler.Stop();
				driver.Bler.StopAndWait();
			}
			{	// ABORt:GSM:SIGNaling<Instance>:BLER
				driver.Bler.Abort();
				driver.Bler.AbortAndWait();
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BLER:STATe
				ResourceStateEnum value = driver.Bler.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BLER:STATe:ALL
				RsCmwGsmSig_Bler_State_All.Fetch_Data value = driver.Bler.State.All.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BLER:CARRier{carrierCmdVal}
				RsCmwGsmSig_Bler_Carrier.ResultData value = driver.Bler.Carrier.Fetch(CarrierRepCap.Default);
				value = driver.Bler.Carrier.Fetch();
			}
			{	// READ:GSM:SIGNaling<Instance>:BLER:CARRier{carrierCmdVal}
				RsCmwGsmSig_Bler_Carrier.ResultData value = driver.Bler.Carrier.Read(CarrierRepCap.Default);
				value = driver.Bler.Carrier.Read();
			}
			{	// FETCh:GSM:SIGNaling<Instance>:BLER:OALL
				RsCmwGsmSig_Bler_Oall.ResultData value = driver.Bler.Oall.Fetch();				
			}
			{	// READ:GSM:SIGNaling<Instance>:BLER:OALL
				RsCmwGsmSig_Bler_Oall.ResultData value = driver.Bler.Oall.Read();				
			}
			{	// STOP:GSM:SIGNaling<instance>:THRoughput
				driver.Throughput.Stop();
				driver.Throughput.StopAndWait();
			}
			{	// ABORt:GSM:SIGNaling<instance>:THRoughput
				driver.Throughput.Abort();
				driver.Throughput.AbortAndWait();
			}
			{	// INITiate:GSM:SIGNaling<instance>:THRoughput
				driver.Throughput.Initiate();
				driver.Throughput.InitiateAndWait();
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput
				RsCmwGsmSig_Throughput.ResultData value = driver.Throughput.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput
				RsCmwGsmSig_Throughput.ResultData value = driver.Throughput.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:STATe
				ResourceStateEnum value = driver.Throughput.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:STATe:ALL
				RsCmwGsmSig_Throughput_State_All.Fetch_Data value = driver.Throughput.State.All.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Current.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Current.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Average.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Average.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Current.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Current.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Average.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Average.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Fetch();				
			}
			{	// READ:GSM:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Read();				
			}
			{	// STOP:GSM:SIGNaling<instance>:CPERformance
				driver.Cperformance.Stop();
				driver.Cperformance.StopAndWait();
			}
			{	// ABORt:GSM:SIGNaling<instance>:CPERformance
				driver.Cperformance.Abort();
				driver.Cperformance.AbortAndWait();
			}
			{	// INITiate:GSM:SIGNaling<instance>:CPERformance
				driver.Cperformance.Initiate();
				driver.Cperformance.InitiateAndWait();
			}
			{	// READ:GSM:SIGNaling<instance>:CPERformance
				List<int> value = driver.Cperformance.Read();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:CPERformance
				List<int> value = driver.Cperformance.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:CPERformance:STATe
				ResourceStateEnum value = driver.Cperformance.State.Fetch();				
			}
			{	// FETCh:GSM:SIGNaling<instance>:CPERformance:STATe:ALL
				RsCmwGsmSig_Cperformance_State_All.Fetch_Data value = driver.Cperformance.State.All.Fetch();				
			}
		}
	}
}