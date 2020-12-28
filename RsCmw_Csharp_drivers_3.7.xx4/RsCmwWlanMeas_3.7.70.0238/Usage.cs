using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwWlanMeas;

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
			RsCmwWlanMeas driver = new RsCmwWlanMeas("TCPIP::localhost::INSTR", true, true);
			{	// ROUTe:WLAN:MEASurement<Instance>:SMIMo
				RsCmwWlanMeas_Route.Smimo_Data value = driver.Route.Smimo;
			}
			{	// ROUTe:WLAN:MEASurement<Instance>
				RsCmwWlanMeas_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:SALone
				RsCmwWlanMeas_Route_Scenario.Salone_Data value = driver.Route.Scenario.Salone;
				driver.Route.Scenario.Salone = value;
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:CSPath
				string value = driver.Route.Scenario.Cspath;
				driver.Route.Scenario.Cspath = value;
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario
				foreach (GuiScenarioEnum x in new GuiScenarioEnum[] { GuiScenarioEnum.CSPath, GuiScenarioEnum.MIMO2x2, GuiScenarioEnum.MIMO4x4, GuiScenarioEnum.MIMO8x8, GuiScenarioEnum.SALone, GuiScenarioEnum.SMI4, GuiScenarioEnum.TMIMo, GuiScenarioEnum.UNDefined })
				{
					GuiScenarioEnum value = driver.Route.Scenario.Value;
				}
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:SMI<nr>
				RsCmwWlanMeas_Route_Scenario_Smi.Smi_Data value = driver.Route.Scenario.Smi.Get(SmiRepCap.Default);
				value = driver.Route.Scenario.Smi.Get();
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:SMI<nr>
				RsCmwWlanMeas_Route_Scenario_Smi.Smi_Data value = new RsCmwWlanMeas_Route_Scenario_Smi.Smi_Data();
				driver.Route.Scenario.Smi.Set(value, SmiRepCap.Default);
				driver.Route.Scenario.Smi.Set(value);
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:SMIMo<PathCount>
				RsCmwWlanMeas_Route_Scenario_Smimo.Get_Data value = driver.Route.Scenario.Smimo.Get(SMimoPathRepCap.Default);
				value = driver.Route.Scenario.Smimo.Get();
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:SMIMo<PathCount>
				driver.Route.Scenario.Smimo.Set();
				driver.Route.Scenario.Smimo.Set(SMimoPathRepCap.Default);
				foreach (ConnectorTupleEnum x in new ConnectorTupleEnum[] { ConnectorTupleEnum.CT12, ConnectorTupleEnum.CT14, ConnectorTupleEnum.CT18, ConnectorTupleEnum.CT34, ConnectorTupleEnum.CT56, ConnectorTupleEnum.CT58, ConnectorTupleEnum.CT78 })
				{
					driver.Route.Scenario.Smimo.Set(x);
					driver.Route.Scenario.Smimo.Set(x, SMimoPathRepCap.Default);
				}
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:TMIMo<PathCount>
				GuiScenarioEnum value = driver.Route.Scenario.Tmimo.Get(TrueMimoPathRepCap.Default);
				value = driver.Route.Scenario.Tmimo.Get();
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:SCENario:TMIMo<PathCount>
				driver.Route.Scenario.Tmimo.Set(TrueMimoPathRepCap.Default);
				driver.Route.Scenario.Tmimo.SetAndWait(TrueMimoPathRepCap.Default);
			}
			{	// ROUTe:WLAN:MEASurement<Instance>:CATalog:SCENario
				foreach (GuiScenarioEnum x in new GuiScenarioEnum[] { GuiScenarioEnum.CSPath, GuiScenarioEnum.MIMO2x2, GuiScenarioEnum.MIMO4x4, GuiScenarioEnum.MIMO8x8, GuiScenarioEnum.SALone, GuiScenarioEnum.SMI4, GuiScenarioEnum.TMIMo, GuiScenarioEnum.UNDefined })
				{
					List<GuiScenarioEnum> value = driver.Route.Catalog.Scenario;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MODE
				foreach (TrainingModeEnum x in new TrainingModeEnum[] { TrainingModeEnum.MMODe, TrainingModeEnum.TMODe })
				{
					driver.Configure.Mode = x;
					TrainingModeEnum value = driver.Configure.Mode;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:SMIMo:CTUPle
				foreach (ConnectorTupleEnum x in new ConnectorTupleEnum[] { ConnectorTupleEnum.CT12, ConnectorTupleEnum.CT14, ConnectorTupleEnum.CT18, ConnectorTupleEnum.CT34, ConnectorTupleEnum.CT56, ConnectorTupleEnum.CT58, ConnectorTupleEnum.CT78 })
				{
					driver.Configure.Smimo.Ctuple = x;
					ConnectorTupleEnum value = driver.Configure.Smimo.Ctuple;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MIMO:NOANtennas
				int value = driver.Configure.Mimo.NoAntennas;
				driver.Configure.Mimo.NoAntennas = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:STANdard
				foreach (IeeeStandardEnum x in new IeeeStandardEnum[] { IeeeStandardEnum.DSSS, IeeeStandardEnum.HEOFdm, IeeeStandardEnum.HTOFdm, IeeeStandardEnum.LOFDm, IeeeStandardEnum.POFDm, IeeeStandardEnum.VHTofdm })
				{
					driver.Configure.Isignal.Standard = x;
					IeeeStandardEnum value = driver.Configure.Isignal.Standard;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:RMODe
				foreach (ReceiveModeEnum x in new ReceiveModeEnum[] { ReceiveModeEnum.CMIMo, ReceiveModeEnum.SISO, ReceiveModeEnum.SMIMo, ReceiveModeEnum.TMIMo })
				{
					driver.Configure.Isignal.Rmode = x;
					ReceiveModeEnum value = driver.Configure.Isignal.Rmode;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:ELENgth
				foreach (BurstEvalLengthEnum x in new BurstEvalLengthEnum[] { BurstEvalLengthEnum.REDucedburst, BurstEvalLengthEnum.WHOLeburst })
				{
					driver.Configure.Isignal.Elength = x;
					BurstEvalLengthEnum value = driver.Configure.Isignal.Elength;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:BTYPe
				foreach (BurstTypeEnum x in new BurstTypeEnum[] { BurstTypeEnum.AUTO, BurstTypeEnum.DLIN, BurstTypeEnum.GREenfield, BurstTypeEnum.MIXed })
				{
					driver.Configure.Isignal.Btype = x;
					BurstTypeEnum value = driver.Configure.Isignal.Btype;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:BWIDth
				foreach (BandwidthEnum x in new BandwidthEnum[] { BandwidthEnum.BW05mhz, BandwidthEnum.BW10mhz, BandwidthEnum.BW16mhz, BandwidthEnum.BW20mhz, BandwidthEnum.BW40mhz, BandwidthEnum.BW80mhz, BandwidthEnum.BW88mhz })
				{
					driver.Configure.Isignal.Bandwidth = x;
					BandwidthEnum value = driver.Configure.Isignal.Bandwidth;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:CDIStance
				int value = driver.Configure.Isignal.Cdistance;
				driver.Configure.Isignal.Cdistance = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:PCLass
				foreach (PowerClassEnum x in new PowerClassEnum[] { PowerClassEnum.CLA, PowerClassEnum.CLB, PowerClassEnum.CLCD, PowerClassEnum.USERdefined })
				{
					driver.Configure.Isignal.Pclass = x;
					PowerClassEnum value = driver.Configure.Isignal.Pclass;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:IQSWap
				bool value = driver.Configure.Isignal.Iqswap;
				driver.Configure.Isignal.Iqswap = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:MODFilter
				foreach (ModulationFilterEnum x in new ModulationFilterEnum[] { ModulationFilterEnum.ALL, ModulationFilterEnum.BPSK, ModulationFilterEnum.CCK11, ModulationFilterEnum.CCK5_5, ModulationFilterEnum.DBPSk, ModulationFilterEnum.DQPSk, ModulationFilterEnum.QAM1024, ModulationFilterEnum.QAM16, ModulationFilterEnum.QAM256, ModulationFilterEnum.QAM64, ModulationFilterEnum.QPSK })
				{
					driver.Configure.Isignal.Modfilter = x;
					ModulationFilterEnum value = driver.Configure.Isignal.Modfilter;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:ISIGnal:TDATa
				string value = driver.Configure.Isignal.Tdata.Value;
				driver.Configure.Isignal.Tdata.Value = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:ISIGnal:TDATa:FILE:DATE
				string value = driver.Configure.Isignal.Tdata.File.Date;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:DSSS:ELENgth
				RsCmwWlanMeas_Configure_Isignal_Dsss.Elength_Data value = driver.Configure.Isignal.Dsss.Elength;
				driver.Configure.Isignal.Dsss.Elength = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:ISIGnal:OFDM:ELENgth
				int value = driver.Configure.Isignal.Ofdm.Elength;
				driver.Configure.Isignal.Ofdm.Elength = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:TMODe:NOANtennas
				int value = driver.Configure.Tmode.NoAntennas;
				driver.Configure.Tmode.NoAntennas = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:TMODe:FILE:SAVE
				string value = driver.Configure.Tmode.File.Save;
				driver.Configure.Tmode.File.Save = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:TMODe:FILE:DATE
				string value = driver.Configure.Tmode.File.Date;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:SANTennas
				bool value = driver.Configure.RfSettings.Santennas;
				driver.Configure.RfSettings.Santennas = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:MLOFfset
				double value = driver.Configure.RfSettings.MlOffset;
				driver.Configure.RfSettings.MlOffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FOFFset
				double value = driver.Configure.RfSettings.FreqOffset;
				driver.Configure.RfSettings.FreqOffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:ANTenna<n>
				RsCmwWlanMeas_Configure_RfSettings_Antenna.Get_Data value = driver.Configure.RfSettings.Antenna.Get(AntennaRepCap.Default);
				value = driver.Configure.RfSettings.Antenna.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:ANTenna<n>
				driver.Configure.RfSettings.Antenna.Set("r1", 1.0, 1.0);
				driver.Configure.RfSettings.Antenna.Set("r1");
				driver.Configure.RfSettings.Antenna.Set("r1", 1.0, 1.0, AntennaRepCap.Default);
				driver.Configure.RfSettings.Antenna.Set("r1", AntennaRepCap.Default);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FREQuency:SCHannel
				foreach (SlopeTypeEnum x in new SlopeTypeEnum[] { SlopeTypeEnum.NEGative, SlopeTypeEnum.POSitive })
				{
					driver.Configure.RfSettings.Frequency.Schannel = x;
					SlopeTypeEnum value = driver.Configure.RfSettings.Frequency.Schannel;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FREQuency:BAND
				foreach (FrequencyBandEnum x in new FrequencyBandEnum[] { FrequencyBandEnum.B24Ghz, FrequencyBandEnum.B4GHz, FrequencyBandEnum.B5GHz })
				{
					driver.Configure.RfSettings.Frequency.Band = x;
					FrequencyBandEnum value = driver.Configure.RfSettings.Frequency.Band;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FREQuency
				double value = driver.Configure.RfSettings.Frequency.Value;
				driver.Configure.RfSettings.Frequency.Value = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FREQuency:CHANnels<Ch>
				RsCmwWlanMeas_Configure_RfSettings_Frequency_Channels.Get_Data value = driver.Configure.RfSettings.Frequency.Channels.Get(ChannelsRepCap.Default);
				value = driver.Configure.RfSettings.Frequency.Channels.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:FREQuency:CHANnels<Ch>
				driver.Configure.RfSettings.Frequency.Channels.Set(1.0, ChannelsRepCap.Default);
				driver.Configure.RfSettings.Frequency.Channels.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:ENPower<connector>
				double value = driver.Configure.RfSettings.EnvelopePower.Get(ConnectorRepCap.Default);
				value = driver.Configure.RfSettings.EnvelopePower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:ENPower<connector>
				driver.Configure.RfSettings.EnvelopePower.Set(1.0, ConnectorRepCap.Default);
				driver.Configure.RfSettings.EnvelopePower.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:EATTenuation<connector>
				double value = driver.Configure.RfSettings.Eattenuation.Get(ConnectorRepCap.Default);
				value = driver.Configure.RfSettings.Eattenuation.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:EATTenuation<connector>
				driver.Configure.RfSettings.Eattenuation.Set(1.0, ConnectorRepCap.Default);
				driver.Configure.RfSettings.Eattenuation.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:UMARgin<connector>
				double value = driver.Configure.RfSettings.Umargin.Get(ConnectorRepCap.Default);
				value = driver.Configure.RfSettings.Umargin.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:RFSettings:UMARgin<connector>
				driver.Configure.RfSettings.Umargin.Set(1.0, ConnectorRepCap.Default);
				driver.Configure.RfSettings.Umargin.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Configure.MultiEval.Timeout;
				driver.Configure.MultiEval.Timeout = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:CFOestimate
				foreach (CfoEstimationEnum x in new CfoEstimationEnum[] { CfoEstimationEnum.FULLpacket, CfoEstimationEnum.PREamble })
				{
					driver.Configure.MultiEval.CfoEstimate = x;
					CfoEstimationEnum value = driver.Configure.MultiEval.CfoEstimate;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:EMEThod
				foreach (EvmMethodEnum x in new EvmMethodEnum[] { EvmMethodEnum.ST1999, EvmMethodEnum.ST2007 })
				{
					driver.Configure.MultiEval.Emethod = x;
					EvmMethodEnum value = driver.Configure.MultiEval.Emethod;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.MultiEval.Scondition = x;
					StopConditionEnum value = driver.Configure.MultiEval.Scondition;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.MultiEval.Repetition = x;
					RepeatEnum value = driver.Configure.MultiEval.Repetition;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:MOEXception
				bool value = driver.Configure.MultiEval.MoException;
				driver.Configure.MultiEval.MoException = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:COUNt
				int value = driver.Configure.MultiEval.List.Count;
				driver.Configure.MultiEval.List.Count = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:CMODe
				foreach (ParameterSetModeEnum x in new ParameterSetModeEnum[] { ParameterSetModeEnum.GLOBal, ParameterSetModeEnum.LIST })
				{
					driver.Configure.MultiEval.List.Cmode = x;
					ParameterSetModeEnum value = driver.Configure.MultiEval.List.Cmode;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:STIMe
				List<double> value = driver.Configure.MultiEval.List.Stime;
				driver.Configure.MultiEval.List.Stime = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:MTIMe
				List<double> value = driver.Configure.MultiEval.List.Mtime;
				driver.Configure.MultiEval.List.Mtime = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:MOFFset
				List<double> value = driver.Configure.MultiEval.List.Moffset;
				driver.Configure.MultiEval.List.Moffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:ENPower
				List<double> value = driver.Configure.MultiEval.List.EnvelopePower;
				driver.Configure.MultiEval.List.EnvelopePower = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:FREQuency
				List<double> value = driver.Configure.MultiEval.List.Frequency;
				driver.Configure.MultiEval.List.Frequency = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:STANdard
				foreach (IeeeStandardEnum x in new IeeeStandardEnum[] { IeeeStandardEnum.DSSS, IeeeStandardEnum.HEOFdm, IeeeStandardEnum.HTOFdm, IeeeStandardEnum.LOFDm, IeeeStandardEnum.POFDm, IeeeStandardEnum.VHTofdm })
				{
					driver.Configure.MultiEval.List.Standard = new List<IeeeStandardEnum> { x, x, x, x, x };
					List<IeeeStandardEnum> value = driver.Configure.MultiEval.List.Standard;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:BWIDth
				foreach (BandwidthEnum x in new BandwidthEnum[] { BandwidthEnum.BW05mhz, BandwidthEnum.BW10mhz, BandwidthEnum.BW16mhz, BandwidthEnum.BW20mhz, BandwidthEnum.BW40mhz, BandwidthEnum.BW80mhz, BandwidthEnum.BW88mhz })
				{
					driver.Configure.MultiEval.List.Bandwidth = new List<BandwidthEnum> { x, x, x, x, x };
					List<BandwidthEnum> value = driver.Configure.MultiEval.List.Bandwidth;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:BTYPe
				foreach (BurstTypeBenum x in new BurstTypeBenum[] { BurstTypeBenum.GREenfield, BurstTypeBenum.MIXed })
				{
					driver.Configure.MultiEval.List.Btype = new List<BurstTypeBenum> { x, x, x, x, x };
					List<BurstTypeBenum> value = driver.Configure.MultiEval.List.Btype;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:RTRigger
				List<bool> value = driver.Configure.MultiEval.List.Rtrigger;
				driver.Configure.MultiEval.List.Rtrigger = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST
				bool value = driver.Configure.MultiEval.List.Value;
				driver.Configure.MultiEval.List.Value = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:SETup
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = driver.Configure.MultiEval.List.Segment.Setup.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:SETup
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = new RsCmwWlanMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data();
				driver.Configure.MultiEval.List.Segment.Setup.Set(value, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:STIMe
				double value = driver.Configure.MultiEval.List.Segment.Stime.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Stime.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:STIMe
				driver.Configure.MultiEval.List.Segment.Stime.Set(1.0, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Stime.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MTIMe
				double value = driver.Configure.MultiEval.List.Segment.Mtime.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Mtime.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MTIMe
				driver.Configure.MultiEval.List.Segment.Mtime.Set(1.0, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Mtime.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MOFFset
				double value = driver.Configure.MultiEval.List.Segment.Moffset.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Moffset.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MOFFset
				driver.Configure.MultiEval.List.Segment.Moffset.Set(1.0, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Moffset.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:ENPower
				double value = driver.Configure.MultiEval.List.Segment.EnvelopePower.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.EnvelopePower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:ENPower
				driver.Configure.MultiEval.List.Segment.EnvelopePower.Set(1.0, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.EnvelopePower.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:FREQuency
				double value = driver.Configure.MultiEval.List.Segment.Frequency.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Frequency.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:FREQuency
				driver.Configure.MultiEval.List.Segment.Frequency.Set(1.0, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Frequency.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:STANdard
				IeeeStandardEnum value = driver.Configure.MultiEval.List.Segment.Standard.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Standard.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:STANdard
				foreach (IeeeStandardEnum x in new IeeeStandardEnum[] { IeeeStandardEnum.DSSS, IeeeStandardEnum.HEOFdm, IeeeStandardEnum.HTOFdm, IeeeStandardEnum.LOFDm, IeeeStandardEnum.POFDm, IeeeStandardEnum.VHTofdm })
				{
					driver.Configure.MultiEval.List.Segment.Standard.Set(x);
					driver.Configure.MultiEval.List.Segment.Standard.Set(x, SegmentBRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:BWIDth
				BandwidthEnum value = driver.Configure.MultiEval.List.Segment.Bandwidth.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Bandwidth.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:BWIDth
				foreach (BandwidthEnum x in new BandwidthEnum[] { BandwidthEnum.BW05mhz, BandwidthEnum.BW10mhz, BandwidthEnum.BW16mhz, BandwidthEnum.BW20mhz, BandwidthEnum.BW40mhz, BandwidthEnum.BW80mhz, BandwidthEnum.BW88mhz })
				{
					driver.Configure.MultiEval.List.Segment.Bandwidth.Set(x);
					driver.Configure.MultiEval.List.Segment.Bandwidth.Set(x, SegmentBRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:BTYPe
				BurstTypeBenum value = driver.Configure.MultiEval.List.Segment.Btype.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Btype.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:BTYPe
				foreach (BurstTypeBenum x in new BurstTypeBenum[] { BurstTypeBenum.GREenfield, BurstTypeBenum.MIXed })
				{
					driver.Configure.MultiEval.List.Segment.Btype.Set(x);
					driver.Configure.MultiEval.List.Segment.Btype.Set(x, SegmentBRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:RTRigger
				bool value = driver.Configure.MultiEval.List.Segment.Rtrigger.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Rtrigger.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:RTRigger
				driver.Configure.MultiEval.List.Segment.Rtrigger.Set(false, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Rtrigger.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:CMWS:CONNector
				ConnectorSwitchExtEnum value = driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:CMWS:CONNector
				foreach (ConnectorSwitchExtEnum x in new ConnectorSwitchExtEnum[] { ConnectorSwitchExtEnum.OFF, ConnectorSwitchExtEnum.ON, ConnectorSwitchExtEnum.R11, ConnectorSwitchExtEnum.R12, ConnectorSwitchExtEnum.R13, ConnectorSwitchExtEnum.R14, ConnectorSwitchExtEnum.R15, ConnectorSwitchExtEnum.R16, ConnectorSwitchExtEnum.R17, ConnectorSwitchExtEnum.R18, ConnectorSwitchExtEnum.R21, ConnectorSwitchExtEnum.R22, ConnectorSwitchExtEnum.R23, ConnectorSwitchExtEnum.R24, ConnectorSwitchExtEnum.R25, ConnectorSwitchExtEnum.R26, ConnectorSwitchExtEnum.R27, ConnectorSwitchExtEnum.R28, ConnectorSwitchExtEnum.R31, ConnectorSwitchExtEnum.R32, ConnectorSwitchExtEnum.R33, ConnectorSwitchExtEnum.R34, ConnectorSwitchExtEnum.R35, ConnectorSwitchExtEnum.R36, ConnectorSwitchExtEnum.R37, ConnectorSwitchExtEnum.R38, ConnectorSwitchExtEnum.R41, ConnectorSwitchExtEnum.R42, ConnectorSwitchExtEnum.R43, ConnectorSwitchExtEnum.R44, ConnectorSwitchExtEnum.R45, ConnectorSwitchExtEnum.R46, ConnectorSwitchExtEnum.R47, ConnectorSwitchExtEnum.R48, ConnectorSwitchExtEnum.RA1, ConnectorSwitchExtEnum.RA2, ConnectorSwitchExtEnum.RA3, ConnectorSwitchExtEnum.RA4, ConnectorSwitchExtEnum.RA5, ConnectorSwitchExtEnum.RA6, ConnectorSwitchExtEnum.RA7, ConnectorSwitchExtEnum.RA8, ConnectorSwitchExtEnum.RB1, ConnectorSwitchExtEnum.RB2, ConnectorSwitchExtEnum.RB3, ConnectorSwitchExtEnum.RB4, ConnectorSwitchExtEnum.RB5, ConnectorSwitchExtEnum.RB6, ConnectorSwitchExtEnum.RB7, ConnectorSwitchExtEnum.RB8, ConnectorSwitchExtEnum.RC1, ConnectorSwitchExtEnum.RC2, ConnectorSwitchExtEnum.RC3, ConnectorSwitchExtEnum.RC4, ConnectorSwitchExtEnum.RC5, ConnectorSwitchExtEnum.RC6, ConnectorSwitchExtEnum.RC7, ConnectorSwitchExtEnum.RC8, ConnectorSwitchExtEnum.RD1, ConnectorSwitchExtEnum.RD2, ConnectorSwitchExtEnum.RD3, ConnectorSwitchExtEnum.RD4, ConnectorSwitchExtEnum.RD5, ConnectorSwitchExtEnum.RD6, ConnectorSwitchExtEnum.RD7, ConnectorSwitchExtEnum.RD8, ConnectorSwitchExtEnum.RE1, ConnectorSwitchExtEnum.RE2, ConnectorSwitchExtEnum.RE3, ConnectorSwitchExtEnum.RE4, ConnectorSwitchExtEnum.RE5, ConnectorSwitchExtEnum.RE6, ConnectorSwitchExtEnum.RE7, ConnectorSwitchExtEnum.RE8, ConnectorSwitchExtEnum.RF1, ConnectorSwitchExtEnum.RF2, ConnectorSwitchExtEnum.RF3, ConnectorSwitchExtEnum.RF4, ConnectorSwitchExtEnum.RF5, ConnectorSwitchExtEnum.RF6, ConnectorSwitchExtEnum.RF7, ConnectorSwitchExtEnum.RF8, ConnectorSwitchExtEnum.RG1, ConnectorSwitchExtEnum.RG2, ConnectorSwitchExtEnum.RG3, ConnectorSwitchExtEnum.RG4, ConnectorSwitchExtEnum.RG5, ConnectorSwitchExtEnum.RG6, ConnectorSwitchExtEnum.RG7, ConnectorSwitchExtEnum.RG8, ConnectorSwitchExtEnum.RH1, ConnectorSwitchExtEnum.RH2, ConnectorSwitchExtEnum.RH3, ConnectorSwitchExtEnum.RH4, ConnectorSwitchExtEnum.RH5, ConnectorSwitchExtEnum.RH6, ConnectorSwitchExtEnum.RH7, ConnectorSwitchExtEnum.RH8 })
				{
					driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Set(x);
					driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Set(x, SegmentBRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:SCOunt
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data value = driver.Configure.MultiEval.List.Segment.Scount.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:SCOunt
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data value = new RsCmwWlanMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data();
				driver.Configure.MultiEval.List.Segment.Scount.Set(value, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:RESult
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Result.Result_Data value = driver.Configure.MultiEval.List.Segment.Result.Get(SegmentBRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Result.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:RESult
				RsCmwWlanMeas_Configure_MultiEval_List_Segment_Result.Result_Data value = new RsCmwWlanMeas_Configure_MultiEval_List_Segment_Result.Result_Data();
				driver.Configure.MultiEval.List.Segment.Result.Set(value, SegmentBRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Result.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:CMWS:CONNector
				foreach (ConnectorSwitchExtEnum x in new ConnectorSwitchExtEnum[] { ConnectorSwitchExtEnum.OFF, ConnectorSwitchExtEnum.ON, ConnectorSwitchExtEnum.R11, ConnectorSwitchExtEnum.R12, ConnectorSwitchExtEnum.R13, ConnectorSwitchExtEnum.R14, ConnectorSwitchExtEnum.R15, ConnectorSwitchExtEnum.R16, ConnectorSwitchExtEnum.R17, ConnectorSwitchExtEnum.R18, ConnectorSwitchExtEnum.R21, ConnectorSwitchExtEnum.R22, ConnectorSwitchExtEnum.R23, ConnectorSwitchExtEnum.R24, ConnectorSwitchExtEnum.R25, ConnectorSwitchExtEnum.R26, ConnectorSwitchExtEnum.R27, ConnectorSwitchExtEnum.R28, ConnectorSwitchExtEnum.R31, ConnectorSwitchExtEnum.R32, ConnectorSwitchExtEnum.R33, ConnectorSwitchExtEnum.R34, ConnectorSwitchExtEnum.R35, ConnectorSwitchExtEnum.R36, ConnectorSwitchExtEnum.R37, ConnectorSwitchExtEnum.R38, ConnectorSwitchExtEnum.R41, ConnectorSwitchExtEnum.R42, ConnectorSwitchExtEnum.R43, ConnectorSwitchExtEnum.R44, ConnectorSwitchExtEnum.R45, ConnectorSwitchExtEnum.R46, ConnectorSwitchExtEnum.R47, ConnectorSwitchExtEnum.R48, ConnectorSwitchExtEnum.RA1, ConnectorSwitchExtEnum.RA2, ConnectorSwitchExtEnum.RA3, ConnectorSwitchExtEnum.RA4, ConnectorSwitchExtEnum.RA5, ConnectorSwitchExtEnum.RA6, ConnectorSwitchExtEnum.RA7, ConnectorSwitchExtEnum.RA8, ConnectorSwitchExtEnum.RB1, ConnectorSwitchExtEnum.RB2, ConnectorSwitchExtEnum.RB3, ConnectorSwitchExtEnum.RB4, ConnectorSwitchExtEnum.RB5, ConnectorSwitchExtEnum.RB6, ConnectorSwitchExtEnum.RB7, ConnectorSwitchExtEnum.RB8, ConnectorSwitchExtEnum.RC1, ConnectorSwitchExtEnum.RC2, ConnectorSwitchExtEnum.RC3, ConnectorSwitchExtEnum.RC4, ConnectorSwitchExtEnum.RC5, ConnectorSwitchExtEnum.RC6, ConnectorSwitchExtEnum.RC7, ConnectorSwitchExtEnum.RC8, ConnectorSwitchExtEnum.RD1, ConnectorSwitchExtEnum.RD2, ConnectorSwitchExtEnum.RD3, ConnectorSwitchExtEnum.RD4, ConnectorSwitchExtEnum.RD5, ConnectorSwitchExtEnum.RD6, ConnectorSwitchExtEnum.RD7, ConnectorSwitchExtEnum.RD8, ConnectorSwitchExtEnum.RE1, ConnectorSwitchExtEnum.RE2, ConnectorSwitchExtEnum.RE3, ConnectorSwitchExtEnum.RE4, ConnectorSwitchExtEnum.RE5, ConnectorSwitchExtEnum.RE6, ConnectorSwitchExtEnum.RE7, ConnectorSwitchExtEnum.RE8, ConnectorSwitchExtEnum.RF1, ConnectorSwitchExtEnum.RF2, ConnectorSwitchExtEnum.RF3, ConnectorSwitchExtEnum.RF4, ConnectorSwitchExtEnum.RF5, ConnectorSwitchExtEnum.RF6, ConnectorSwitchExtEnum.RF7, ConnectorSwitchExtEnum.RF8, ConnectorSwitchExtEnum.RG1, ConnectorSwitchExtEnum.RG2, ConnectorSwitchExtEnum.RG3, ConnectorSwitchExtEnum.RG4, ConnectorSwitchExtEnum.RG5, ConnectorSwitchExtEnum.RG6, ConnectorSwitchExtEnum.RG7, ConnectorSwitchExtEnum.RG8, ConnectorSwitchExtEnum.RH1, ConnectorSwitchExtEnum.RH2, ConnectorSwitchExtEnum.RH3, ConnectorSwitchExtEnum.RH4, ConnectorSwitchExtEnum.RH5, ConnectorSwitchExtEnum.RH6, ConnectorSwitchExtEnum.RH7, ConnectorSwitchExtEnum.RH8 })
				{
					driver.Configure.MultiEval.List.SingleCmw.Connector = new List<ConnectorSwitchExtEnum> { x, x, x, x, x };
					List<ConnectorSwitchExtEnum> value = driver.Configure.MultiEval.List.SingleCmw.Connector;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SCOunt:MODulation
				List<int> value = driver.Configure.MultiEval.List.Scount.Modulation;
				driver.Configure.MultiEval.List.Scount.Modulation = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:SCOunt:TSMask
				List<int> value = driver.Configure.MultiEval.List.Scount.TsMask;
				driver.Configure.MultiEval.List.Scount.TsMask = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:RESult:MODulation
				List<bool> value = driver.Configure.MultiEval.List.Result.Modulation;
				driver.Configure.MultiEval.List.Result.Modulation = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIST:RESult:TSMask
				List<bool> value = driver.Configure.MultiEval.List.Result.TsMask;
				driver.Configure.MultiEval.List.Result.TsMask = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:PVTime:RPOWer
				foreach (RefPowerEnum x in new RefPowerEnum[] { RefPowerEnum.MAXimum, RefPowerEnum.MEAN })
				{
					driver.Configure.MultiEval.PowerVsTime.Rpower = x;
					RefPowerEnum value = driver.Configure.MultiEval.PowerVsTime.Rpower;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:PVTime:ALENgth
				double value = driver.Configure.MultiEval.PowerVsTime.Alength;
				driver.Configure.MultiEval.PowerVsTime.Alength = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe
				bool value = driver.Configure.MultiEval.PowerVsTime.RisingEdge;
				driver.Configure.MultiEval.PowerVsTime.RisingEdge = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe
				bool value = driver.Configure.MultiEval.PowerVsTime.FallingEdge;
				driver.Configure.MultiEval.PowerVsTime.FallingEdge = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:PVTime:BURSt
				bool value = driver.Configure.MultiEval.PowerVsTime.Burst;
				driver.Configure.MultiEval.PowerVsTime.Burst = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:CESTimation
				foreach (ChannelEstimationEnum x in new ChannelEstimationEnum[] { ChannelEstimationEnum.PAYLoad, ChannelEstimationEnum.PREamble })
				{
					driver.Configure.MultiEval.Compensation.Cestimation = x;
					ChannelEstimationEnum value = driver.Configure.MultiEval.Compensation.Cestimation;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:EFTaps
				RsCmwWlanMeas_Configure_MultiEval_Compensation.EfTaps_Data value = driver.Configure.MultiEval.Compensation.EfTaps;
				driver.Configure.MultiEval.Compensation.EfTaps = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:SKIPsymbols
				RsCmwWlanMeas_Configure_MultiEval_Compensation.SkipSymbols_Data value = driver.Configure.MultiEval.Compensation.SkipSymbols;
				driver.Configure.MultiEval.Compensation.SkipSymbols = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:TRACking:PHASe
				bool value = driver.Configure.MultiEval.Compensation.Tracking.Phase;
				driver.Configure.MultiEval.Compensation.Tracking.Phase = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:TRACking:TIMing
				bool value = driver.Configure.MultiEval.Compensation.Tracking.Timing;
				driver.Configure.MultiEval.Compensation.Tracking.Timing = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:COMPensation:TRACking:LEVel
				bool value = driver.Configure.MultiEval.Compensation.Tracking.Level;
				driver.Configure.MultiEval.Compensation.Tracking.Level = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:DEMod:FFT:OFFSet
				foreach (FftOffsetEnum x in new FftOffsetEnum[] { FftOffsetEnum.AUTO, FftOffsetEnum.CENT, FftOffsetEnum.PEAK })
				{
					driver.Configure.MultiEval.Demod.Fft.Offset = x;
					FftOffsetEnum value = driver.Configure.MultiEval.Demod.Fft.Offset;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TSMask:AFFTnum
				int value = driver.Configure.MultiEval.TsMask.AfftNum;
				driver.Configure.MultiEval.TsMask.AfftNum = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TSMask:TROTime
				double value = driver.Configure.MultiEval.TsMask.TroTime;
				driver.Configure.MultiEval.TsMask.TroTime = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBWPercent
				double value = driver.Configure.MultiEval.TsMask.ObwPercent;
				driver.Configure.MultiEval.TsMask.ObwPercent = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TSMask:MSELection
				double value = driver.Configure.MultiEval.TsMask.Mselection;
				driver.Configure.MultiEval.TsMask.Mselection = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:TSMask:DMODe
				foreach (DisplayModeEnum x in new DisplayModeEnum[] { DisplayModeEnum.ABSolute, DisplayModeEnum.RELative })
				{
					driver.Configure.MultiEval.TsMask.Dmode = x;
					DisplayModeEnum value = driver.Configure.MultiEval.TsMask.Dmode;
				}
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:PVTime
				bool value = driver.Configure.MultiEval.Result.PowerVsTime;
				driver.Configure.MultiEval.Result.PowerVsTime = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:SFLatness
				bool value = driver.Configure.MultiEval.Result.SpectrFlatness;
				driver.Configure.MultiEval.Result.SpectrFlatness = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult[:ALL]
				RsCmwWlanMeas_Configure_MultiEval_Result.All_Data value = driver.Configure.MultiEval.Result.All;
				driver.Configure.MultiEval.Result.All = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:EVM
				bool value = driver.Configure.MultiEval.Result.Evm;
				driver.Configure.MultiEval.Result.Evm = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:EVMCarrier
				bool value = driver.Configure.MultiEval.Result.EvmCarrier;
				driver.Configure.MultiEval.Result.EvmCarrier = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:IQConst
				bool value = driver.Configure.MultiEval.Result.IqConstant;
				driver.Configure.MultiEval.Result.IqConstant = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:UTERror
				bool value = driver.Configure.MultiEval.Result.UtError;
				driver.Configure.MultiEval.Result.UtError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:EVMSymbol
				bool value = driver.Configure.MultiEval.Result.EvmSymbol;
				driver.Configure.MultiEval.Result.EvmSymbol = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:TSMask
				bool value = driver.Configure.MultiEval.Result.TsMask;
				driver.Configure.MultiEval.Result.TsMask = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:RESult:MSCalar
				bool value = driver.Configure.MultiEval.Result.Mscalar;
				driver.Configure.MultiEval.Result.Mscalar = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:SFLatness:DMODe
				foreach (DisplayModeEnum x in new DisplayModeEnum[] { DisplayModeEnum.ABSolute, DisplayModeEnum.RELative })
				{
					driver.Configure.MultiEval.SpectrFlatness.Dmode = x;
					DisplayModeEnum value = driver.Configure.MultiEval.SpectrFlatness.Dmode;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:UTEPower
				foreach (LowHighEnum x in new LowHighEnum[] { LowHighEnum.HIGH, LowHighEnum.LOW })
				{
					driver.Configure.MultiEval.Limit.UtePower = x;
					LowHighEnum value = driver.Configure.MultiEval.Limit.UtePower;
				}
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:UTERror
				bool value = driver.Configure.MultiEval.Limit.UtError;
				driver.Configure.MultiEval.Limit.UtError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:LOFDm:ENABle
				bool value = driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Enable;
				driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Enable = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:LOFDm:UPPer
				double value = driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Upper;
				driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Upper = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:LOFDm:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_Lofdm.Lower_Data value = driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Lower;
				driver.Configure.MultiEval.Limit.SpectrFlatness.Lofdm.Lower = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Enable.Get(BandwidthBRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Enable.Set(false, BandwidthBRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:UPPer
				double value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Upper.Get(BandwidthBRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Upper.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:UPPer
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Upper.Set(1.0, BandwidthBRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Upper.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_Pofdm_Bw_Lower.Lower_Data value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Lower.Get(BandwidthBRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Lower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:POFDm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_Pofdm_Bw_Lower.Lower_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_Pofdm_Bw_Lower.Lower_Data();
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Lower.Set(value, BandwidthBRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.Pofdm.Bw.Lower.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Enable.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Enable.Set(false, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:UPPer
				double value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Upper.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Upper.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:UPPer
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Upper.Set(1.0, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Upper.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HtOfdm_Bw_Lower.Lower_Data value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Lower.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Lower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HTOFdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HtOfdm_Bw_Lower.Lower_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HtOfdm_Bw_Lower.Lower_Data();
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Lower.Set(value, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HtOfdm.Bw.Lower.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Enable.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Enable.Set(false, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:UPPer
				double value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Upper.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Upper.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:UPPer
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Upper.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Upper.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_VhtOfdm_Bw_Lower.Lower_Data value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Lower.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Lower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:VHTofdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_VhtOfdm_Bw_Lower.Lower_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_VhtOfdm_Bw_Lower.Lower_Data();
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Lower.Set(value, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.VhtOfdm.Bw.Lower.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Enable.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Enable.Set(false, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:UPPer
				double value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Upper.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Upper.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:UPPer
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Upper.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Upper.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HeOfdm_Bw_Lower.Lower_Data value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Lower.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Lower.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:SFLatness:HEOFdm:BW<bandwidth>:LOWer
				RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HeOfdm_Bw_Lower.Lower_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_SpectrFlatness_HeOfdm_Bw_Lower.Lower_Data();
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Lower.Set(value, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.SpectrFlatness.HeOfdm.Bw.Lower.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:DSSS:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.Dsss.Enable;
				driver.Configure.MultiEval.Limit.TsMask.Dsss.Enable = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:DSSS:Y:AB
				double value = driver.Configure.MultiEval.Limit.TsMask.Dsss.Y.Ab;
				driver.Configure.MultiEval.Limit.TsMask.Dsss.Y.Ab = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:DSSS:Y:CD
				double value = driver.Configure.MultiEval.Limit.TsMask.Dsss.Y.Cd;
				driver.Configure.MultiEval.Limit.TsMask.Dsss.Y.Cd = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:LOFDm:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.Lofdm.Enable;
				driver.Configure.MultiEval.Limit.TsMask.Lofdm.Enable = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:LOFDm:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.A;
				driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.A = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:LOFDm:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.B;
				driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.B = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:LOFDm:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.C;
				driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.C = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:LOFDm:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.D;
				driver.Configure.MultiEval.Limit.TsMask.Lofdm.Y.D = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Enable.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Enable.Set(false, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.A.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:A
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.A.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.B.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:B
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.B.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.C.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:C
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.C.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.D.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:D
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.D.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:E
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.E.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.E.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CA:Y:E
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.E.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Ca.Y.E.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.A.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:A
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.A.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.B.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:B
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.B.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.C.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:C
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.C.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.D.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:D
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.D.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:E
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.E.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.E.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:CB:Y:E
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.E.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Cb.Y.E.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.A.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:A
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.A.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.B.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:B
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.B.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.C.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:C
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.C.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.D.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:D
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.D.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:E
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.E.Get(BandwidthBRepCap.Bw5);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.E.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:UDEFined:Y:E
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.E.Set(1.0, BandwidthBRepCap.Bw5);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.UserDefined.Y.E.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.A.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:A
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.A.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.B.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:B
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.B.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.C.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:C
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.C.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.D.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:D
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.D.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:E
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.E.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.E.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:E
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.E.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.E.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:F
				double value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.F.Get(BandwidthARepCap.Bw10);
				value = driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.F.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:POFDm:BW<bandwidth>:ABSolute:Y:F
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.F.Set(1.0, BandwidthARepCap.Bw10);
				driver.Configure.MultiEval.Limit.TsMask.Pofdm.Bw.Absolute.Y.F.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Enable.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Enable.Set(false, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.A.Get(BandwidthCRepCap.Default, BandRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:A
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.A.Set(1.0, BandwidthCRepCap.Default, BandRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.B.Get(BandwidthCRepCap.Default, BandRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:B
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.B.Set(1.0, BandwidthCRepCap.Default, BandRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.C.Get(BandwidthCRepCap.Default, BandRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:C
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.C.Set(1.0, BandwidthCRepCap.Default, BandRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.D.Get(BandwidthCRepCap.Default, BandRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:BAND<band>:Y:D
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.D.Set(1.0, BandwidthCRepCap.Default, BandRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.Band.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:ABSLimit
				double value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.AbsLimit.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.AbsLimit.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HTOFdm:BW<bandwidth>:ABSLimit
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.AbsLimit.Set(1.0, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HtOfdm.Bw.AbsLimit.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Enable.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Enable.Set(false, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.A.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:A
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.A.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.B.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:B
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.B.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.C.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:C
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.C.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.D.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:Y:D
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.D.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:ABSLimit
				double value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.AbsLimit.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.AbsLimit.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:VHTofdm:BW<bandwidth>:ABSLimit
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.AbsLimit.Set(1.0, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.VhtOfdm.Bw.AbsLimit.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:ENABle
				bool value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Enable.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Enable.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:ENABle
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Enable.Set(false, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Enable.Set(false);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:A
				double value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.A.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.A.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:A
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.A.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.A.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:B
				double value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.B.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.B.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:B
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.B.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.B.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:C
				double value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.C.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.C.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:C
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.C.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.C.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:D
				double value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.D.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.D.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:Y:D
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.D.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.Y.D.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:ABSLimit
				double value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.AbsLimit.Get(BandwidthDRepCap.Default);
				value = driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.AbsLimit.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:TSMask:HEOFdm:BW<bandwidth>:ABSLimit
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.AbsLimit.Set(1.0, BandwidthDRepCap.Default);
				driver.Configure.MultiEval.Limit.TsMask.HeOfdm.Bw.AbsLimit.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:DSSS:EVMRms
				double value = driver.Configure.MultiEval.Limit.Modulation.Dsss.EvmEms;
				driver.Configure.MultiEval.Limit.Modulation.Dsss.EvmEms = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:DSSS:EVMPeak
				double value = driver.Configure.MultiEval.Limit.Modulation.Dsss.EvmPeak;
				driver.Configure.MultiEval.Limit.Modulation.Dsss.EvmPeak = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:DSSS:IQOFfset
				double value = driver.Configure.MultiEval.Limit.Modulation.Dsss.IqOffset;
				driver.Configure.MultiEval.Limit.Modulation.Dsss.IqOffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:DSSS:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Dsss.CfError;
				driver.Configure.MultiEval.Limit.Modulation.Dsss.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:DSSS:CCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Dsss.CcError;
				driver.Configure.MultiEval.Limit.Modulation.Dsss.CcError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:LOFDm:EVM
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_Lofdm.Evm_Data value = driver.Configure.MultiEval.Limit.Modulation.Lofdm.Evm;
				driver.Configure.MultiEval.Limit.Modulation.Lofdm.Evm = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:LOFDm:EVMPilot
				double value = driver.Configure.MultiEval.Limit.Modulation.Lofdm.EvmPilot;
				driver.Configure.MultiEval.Limit.Modulation.Lofdm.EvmPilot = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:LOFDm:IQOFfset
				double value = driver.Configure.MultiEval.Limit.Modulation.Lofdm.IqOffset;
				driver.Configure.MultiEval.Limit.Modulation.Lofdm.IqOffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:LOFDm:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Lofdm.CfError;
				driver.Configure.MultiEval.Limit.Modulation.Lofdm.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:LOFDm:SCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Lofdm.ScError;
				driver.Configure.MultiEval.Limit.Modulation.Lofdm.ScError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:POFDm:EVM
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_Pofdm.Evm_Data value = driver.Configure.MultiEval.Limit.Modulation.Pofdm.Evm;
				driver.Configure.MultiEval.Limit.Modulation.Pofdm.Evm = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:POFDm:EVMPilot
				double value = driver.Configure.MultiEval.Limit.Modulation.Pofdm.EvmPilot;
				driver.Configure.MultiEval.Limit.Modulation.Pofdm.EvmPilot = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:POFDm:IQOFfset
				double value = driver.Configure.MultiEval.Limit.Modulation.Pofdm.IqOffset;
				driver.Configure.MultiEval.Limit.Modulation.Pofdm.IqOffset = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:POFDm:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Pofdm.CfError;
				driver.Configure.MultiEval.Limit.Modulation.Pofdm.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:POFDm:SCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.Pofdm.ScError;
				driver.Configure.MultiEval.Limit.Modulation.Pofdm.ScError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:EVM
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HtOfdm.Evm_Data value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.Evm;
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.Evm = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:EVMPilot
				double value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.EvmPilot;
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.EvmPilot = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.CfError;
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:SCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.ScError;
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.ScError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:IQOFfset:BW<BW>
				double value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.IqOffset.Bw.Get(BandwidthCRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Modulation.HtOfdm.IqOffset.Bw.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HTOFdm:IQOFfset:BW<BW>
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.IqOffset.Bw.Set(1.0, BandwidthCRepCap.Default);
				driver.Configure.MultiEval.Limit.Modulation.HtOfdm.IqOffset.Bw.Set(1.0);
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:VHTofdm:EVMall
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_VhtOfdm.EvmAll_Data value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.EvmAll;
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.EvmAll = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:VHTofdm:EVMPilot
				double value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.EvmPilot;
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.EvmPilot = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:VHTofdm:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.CfError;
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:VHTofdm:SCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.ScError;
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.ScError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:VHTofdm:IQOFfset:BW<BW>
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_VhtOfdm_IqOffset_Bw.Bw_Data value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.IqOffset.Bw.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.IqOffset.Bw.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:VHTofdm:IQOFfset:BW<BW>
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_VhtOfdm_IqOffset_Bw.Bw_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_VhtOfdm_IqOffset_Bw.Bw_Data();
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.IqOffset.Bw.Set(value, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.Modulation.VhtOfdm.IqOffset.Bw.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HEOFdm:CFERror
				double value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.CfError;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.CfError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HEOFdm:SCERror
				double value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.ScError;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.ScError = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HEOFdm:CFDistrib
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm.CfoDistribution_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.CfoDistribution;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.CfoDistribution = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMall:TBCoderate
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_EvmAll.TbCoderate_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbCoderate;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbCoderate = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMall:TBHigh
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_EvmAll.TbHigh_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbHigh;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbHigh = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMall:TBLow
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_EvmAll.TbLow_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbLow;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.TbLow = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMall
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_EvmAll.Value_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.Value;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmAll.Value = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMPilot:TBHigh
				double value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.TbHigh;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.TbHigh = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMPilot:TBLow
				double value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.TbLow;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.TbLow = value;
			}
			{	// CONFigure:WLAN:MEASurement<instance>:MEValuation:LIMit:MODulation:HEOFdm:EVMPilot
				double value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.Value;
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.EvmPilot.Value = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HEOFdm:IQOFfset:BW<BW>
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_IqOffset_Bw.Bw_Data value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.IqOffset.Bw.Get(BandwidthERepCap.Default);
				value = driver.Configure.MultiEval.Limit.Modulation.HeOfdm.IqOffset.Bw.Get();
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:MODulation:HEOFdm:IQOFfset:BW<BW>
				RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_IqOffset_Bw.Bw_Data value = new RsCmwWlanMeas_Configure_MultiEval_Limit_Modulation_HeOfdm_IqOffset_Bw.Bw_Data();
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.IqOffset.Bw.Set(value, BandwidthERepCap.Default);
				driver.Configure.MultiEval.Limit.Modulation.HeOfdm.IqOffset.Bw.Set(value);
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:PVTime:REDGe
				double value = driver.Configure.MultiEval.Limit.PowerVsTime.RisingEdge;
				driver.Configure.MultiEval.Limit.PowerVsTime.RisingEdge = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:PVTime:FEDGe
				double value = driver.Configure.MultiEval.Limit.PowerVsTime.FallingEdge;
				driver.Configure.MultiEval.Limit.PowerVsTime.FallingEdge = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:PVTime:TERRor
				double value = driver.Configure.MultiEval.Limit.PowerVsTime.Terror;
				driver.Configure.MultiEval.Limit.PowerVsTime.Terror = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:LIMit:PVTime:TEDistrib
				double value = driver.Configure.MultiEval.Limit.PowerVsTime.TeDistribution;
				driver.Configure.MultiEval.Limit.PowerVsTime.TeDistribution = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:SCOunt:TSMask
				int value = driver.Configure.MultiEval.Scount.TsMask;
				driver.Configure.MultiEval.Scount.TsMask = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:SCOunt:PVTime
				int value = driver.Configure.MultiEval.Scount.PowerVsTime;
				driver.Configure.MultiEval.Scount.PowerVsTime = value;
			}
			{	// CONFigure:WLAN:MEASurement<Instance>:MEValuation:SCOunt:MODulation
				int value = driver.Configure.MultiEval.Scount.Modulation;
				driver.Configure.MultiEval.Scount.Modulation = value;
			}
			{	// ABORt:WLAN:MEASurement<Instance>:TMODe
				driver.Tmode.Abort();
				driver.Tmode.AbortAndWait();
			}
			{	// CLEar:WLAN:MEASurement<instance>:TMODe:DATA
				driver.Tmode.Data.Clear();
				driver.Tmode.Data.ClearAndWait();
			}
			{	// READ:WLAN:MEASurement<Instance>:TMODe:ANTenna<Antennas>
				RsCmwWlanMeas_Tmode_Antenna.ResultData value = driver.Tmode.Antenna.Read(AntennaRepCap.Default);
				value = driver.Tmode.Antenna.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:TMODe:ANTenna<Antennas>
				RsCmwWlanMeas_Tmode_Antenna.ResultData value = driver.Tmode.Antenna.Fetch(AntennaRepCap.Default);
				value = driver.Tmode.Antenna.Fetch();
			}
			{	// INITiate:WLAN:MEASurement<Instance>:TMODe:ANTenna<Antennas>
				driver.Tmode.Antenna.Initiate(AntennaRepCap.Default);
				driver.Tmode.Antenna.InitiateAndWait(AntennaRepCap.Default);
			}
			{	// STOP:WLAN:MEASurement<Instance>:MEValuation
				driver.MultiEval.Stop();
				driver.MultiEval.StopAndWait();
			}
			{	// ABORt:WLAN:MEASurement<Instance>:MEValuation
				driver.MultiEval.Abort();
				driver.MultiEval.AbortAndWait();
			}
			{	// INITiate:WLAN:MEASurement<Instance>:MEValuation
				driver.MultiEval.Initiate();
				driver.MultiEval.InitiateAndWait();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:CURRent
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:CURRent
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:AVERage
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:AVERage
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:MAXimum
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:MAXimum
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:SDEViation
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:SDEViation
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.StandardDev.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:MINimum
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:CMIMo:PSTS:MINimum
				List<double> value = driver.MultiEval.Modulation.Cmimo.Psts.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Current.ResultData value = driver.MultiEval.Modulation.Cmimo.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Current.ResultData value = driver.MultiEval.Modulation.Cmimo.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Average.ResultData value = driver.MultiEval.Modulation.Cmimo.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Average.ResultData value = driver.MultiEval.Modulation.Cmimo.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Maximum.ResultData value = driver.MultiEval.Modulation.Cmimo.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_Maximum.ResultData value = driver.MultiEval.Modulation.Cmimo.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Cmimo.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CMIMo:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Cmimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Cmimo.StandardDev.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_Current.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.Current.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_Average.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.Average.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_Maximum.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.Maximum.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_StandardDev.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.StandardDev.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Current.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Current.Fetch(UserRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Average.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Average.Fetch(UserRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Maximum.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Maximum.Fetch(UserRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_StandardDev.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.StandardDev.Fetch(UserRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.StandardDev.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:STReam<str>:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Stream_Current.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Current.Fetch(UserRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:STReam<str>:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Stream_Average.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Average.Fetch(UserRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:STReam<str>:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Stream_Maximum.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Maximum.Fetch(UserRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:EVMagnitude:USER<user>:STReam<str>:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_EvMagnitude_User_Stream_StandardDev.Fetch_Data value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.StandardDev.Fetch(UserRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Modulation.EvMagnitude.User.Stream.StandardDev.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CFDistrib
				RsCmwWlanMeas_MultiEval_Modulation_CfoDistribution.ResultData value = driver.MultiEval.Modulation.CfoDistribution.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CFDistrib
				RsCmwWlanMeas_MultiEval_Modulation_CfoDistribution.ResultData value = driver.MultiEval.Modulation.CfoDistribution.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:CFDistrib
				ResultStatus2enum value = driver.MultiEval.Modulation.CfoDistribution.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Current.ResultData value = driver.MultiEval.Modulation.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Current.ResultData value = driver.MultiEval.Modulation.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Current.Calculate_Data value = driver.MultiEval.Modulation.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Average.ResultData value = driver.MultiEval.Modulation.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Average.ResultData value = driver.MultiEval.Modulation.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Average.Calculate_Data value = driver.MultiEval.Modulation.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Maximum.ResultData value = driver.MultiEval.Modulation.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Maximum.ResultData value = driver.MultiEval.Modulation.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_StandardDev.ResultData value = driver.MultiEval.Modulation.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_StandardDev.ResultData value = driver.MultiEval.Modulation.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.StandardDev.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Minimum.ResultData value = driver.MultiEval.Modulation.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Minimum.ResultData value = driver.MultiEval.Modulation.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Current.ResultData value = driver.MultiEval.Modulation.Segments.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Current.ResultData value = driver.MultiEval.Modulation.Segments.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Current.Calculate_Data value = driver.MultiEval.Modulation.Segments.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Average.ResultData value = driver.MultiEval.Modulation.Segments.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Average.ResultData value = driver.MultiEval.Modulation.Segments.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Average.Calculate_Data value = driver.MultiEval.Modulation.Segments.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Maximum.ResultData value = driver.MultiEval.Modulation.Segments.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Maximum.ResultData value = driver.MultiEval.Modulation.Segments.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Segments.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Segments_StandardDev.ResultData value = driver.MultiEval.Modulation.Segments.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Segments_StandardDev.ResultData value = driver.MultiEval.Modulation.Segments.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Segments_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Segments.StandardDev.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Minimum.ResultData value = driver.MultiEval.Modulation.Segments.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Minimum.ResultData value = driver.MultiEval.Modulation.Segments.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Segments_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Segments.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Current.Read_Data value = driver.MultiEval.Modulation.Dsss.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Current.Fetch_Data value = driver.MultiEval.Modulation.Dsss.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Current.Calculate_Data value = driver.MultiEval.Modulation.Dsss.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Average.ResultData value = driver.MultiEval.Modulation.Dsss.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Average.ResultData value = driver.MultiEval.Modulation.Dsss.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Average.Calculate_Data value = driver.MultiEval.Modulation.Dsss.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Maximum.ResultData value = driver.MultiEval.Modulation.Dsss.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Maximum.ResultData value = driver.MultiEval.Modulation.Dsss.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Dsss.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_StandardDev.ResultData value = driver.MultiEval.Modulation.Dsss.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_StandardDev.ResultData value = driver.MultiEval.Modulation.Dsss.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Dsss.StandardDev.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Minimum.ResultData value = driver.MultiEval.Modulation.Dsss.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Minimum.ResultData value = driver.MultiEval.Modulation.Dsss.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:DSSS:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Dsss_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Dsss.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Current.ResultData value = driver.MultiEval.Modulation.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Current.ResultData value = driver.MultiEval.Modulation.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Current.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Average.ResultData value = driver.MultiEval.Modulation.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Average.ResultData value = driver.MultiEval.Modulation.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Average.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Maximum.ResultData value = driver.MultiEval.Modulation.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Maximum.ResultData value = driver.MultiEval.Modulation.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Mimo.StandardDev.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.StandardDev.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Mimo.StandardDev.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.StandardDev.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Mimo.StandardDev.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.StandardDev.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Minimum.ResultData value = driver.MultiEval.Modulation.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Minimum.ResultData value = driver.MultiEval.Modulation.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Current.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Current.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Current.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Segments.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Average.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Average.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Average.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Segments.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Maximum.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Maximum.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_StandardDev.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_StandardDev.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.StandardDev.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Minimum.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Minimum.ResultData value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_Modulation_Mimo_Segments_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.Modulation.Mimo.Segments.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Current.ResultData value = driver.MultiEval.Modulation.Smimo.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Current.ResultData value = driver.MultiEval.Modulation.Smimo.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Current.Calculate_Data value = driver.MultiEval.Modulation.Smimo.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Average.ResultData value = driver.MultiEval.Modulation.Smimo.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Average.ResultData value = driver.MultiEval.Modulation.Smimo.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Average.Calculate_Data value = driver.MultiEval.Modulation.Smimo.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Maximum.ResultData value = driver.MultiEval.Modulation.Smimo.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Maximum.ResultData value = driver.MultiEval.Modulation.Smimo.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Smimo.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Smimo.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_StandardDev.ResultData value = driver.MultiEval.Modulation.Smimo.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:MODulation:SMIMo:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Smimo_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Smimo.StandardDev.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Current.ResultData value = driver.MultiEval.Modulation.Acsiso.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Current.ResultData value = driver.MultiEval.Modulation.Acsiso.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Current.Calculate_Data value = driver.MultiEval.Modulation.Acsiso.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Average.ResultData value = driver.MultiEval.Modulation.Acsiso.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Average.ResultData value = driver.MultiEval.Modulation.Acsiso.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Average.Calculate_Data value = driver.MultiEval.Modulation.Acsiso.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Maximum.ResultData value = driver.MultiEval.Modulation.Acsiso.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Maximum.ResultData value = driver.MultiEval.Modulation.Acsiso.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Acsiso.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_StandardDev.ResultData value = driver.MultiEval.Modulation.Acsiso.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_StandardDev.ResultData value = driver.MultiEval.Modulation.Acsiso.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:ACSiso:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Acsiso_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Acsiso.StandardDev.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Current.ResultData value = driver.MultiEval.Modulation.Ofdm.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Current.ResultData value = driver.MultiEval.Modulation.Ofdm.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Current.Calculate_Data value = driver.MultiEval.Modulation.Ofdm.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Average.ResultData value = driver.MultiEval.Modulation.Ofdm.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Average.ResultData value = driver.MultiEval.Modulation.Ofdm.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Average.Calculate_Data value = driver.MultiEval.Modulation.Ofdm.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Maximum.ResultData value = driver.MultiEval.Modulation.Ofdm.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Maximum.ResultData value = driver.MultiEval.Modulation.Ofdm.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Ofdm.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_StandardDev.ResultData value = driver.MultiEval.Modulation.Ofdm.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_StandardDev.ResultData value = driver.MultiEval.Modulation.Ofdm.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:MODulation:OFDM:SDEViation
				RsCmwWlanMeas_MultiEval_Modulation_Ofdm_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Ofdm.StandardDev.Calculate();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SRELiability
				List<int> value = driver.MultiEval.List.Sreliability.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:MODulation:SCOunt
				List<int> value = driver.MultiEval.List.Modulation.Scount.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MODulation:CURRent
				RsCmwWlanMeas_MultiEval_List_Segment_Modulation_Current.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Current.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MODulation:AVERage
				RsCmwWlanMeas_MultiEval_List_Segment_Modulation_Average.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Average.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MODulation:MAXimum
				RsCmwWlanMeas_MultiEval_List_Segment_Modulation_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Maximum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MODulation:SDEViation
				RsCmwWlanMeas_MultiEval_List_Segment_Modulation_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:MODulation:MINimum
				RsCmwWlanMeas_MultiEval_List_Segment_Modulation_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Minimum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Minimum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:CURRent
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Current.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Current.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:AVERage
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Average.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Average.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:MAXimum
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Maximum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:MINimum
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Minimum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Minimum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Frequency_Current.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Frequency.Current.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Frequency.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Frequency_Average.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Frequency.Average.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Frequency.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Frequency_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Frequency.Maximum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Frequency.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:SEGMent<segment>:TSMask:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_List_Segment_TsMask_Frequency_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.TsMask.Frequency.Minimum.Fetch(SegmentBRepCap.Default);
				value = driver.MultiEval.List.Segment.TsMask.Frequency.Minimum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:LIST:TSMask:SCOunt
				List<int> value = driver.MultiEval.List.TsMask.Scount.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:OFDMa:INFO
				RsCmwWlanMeas_MultiEval_Ofdma_Info.Fetch_Data value = driver.MultiEval.Ofdma.Info.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:OFDMa:UINFo<user>
				RsCmwWlanMeas_MultiEval_Ofdma_Uinfo.Fetch_Data value = driver.MultiEval.Ofdma.Uinfo.Fetch(UserRepCap.Default);
				value = driver.MultiEval.Ofdma.Uinfo.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RXANtenna<n>:CURRent
				List<double> value = driver.MultiEval.Power.RxAntenna.Current.Fetch(RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.RxAntenna.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RXANtenna<n>:AVERage
				List<double> value = driver.MultiEval.Power.RxAntenna.Average.Fetch(RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.RxAntenna.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RXANtenna<n>:MAXimum
				List<double> value = driver.MultiEval.Power.RxAntenna.Maximum.Fetch(RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.RxAntenna.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RXANtenna<n>:SDEViation
				List<double> value = driver.MultiEval.Power.RxAntenna.StandardDev.Fetch(RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.RxAntenna.StandardDev.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:CURRent
				List<double> value = driver.MultiEval.Power.Runit.Current.Fetch(ResourceUnitRepCap.Default);
				value = driver.MultiEval.Power.Runit.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:AVERage
				List<double> value = driver.MultiEval.Power.Runit.Average.Fetch(ResourceUnitRepCap.Default);
				value = driver.MultiEval.Power.Runit.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:MAXimum
				List<double> value = driver.MultiEval.Power.Runit.Maximum.Fetch(ResourceUnitRepCap.Default);
				value = driver.MultiEval.Power.Runit.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:SDEViation
				List<double> value = driver.MultiEval.Power.Runit.StandardDev.Fetch(ResourceUnitRepCap.Default);
				value = driver.MultiEval.Power.Runit.StandardDev.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:RXANtenna<n>:CURRent
				double value = driver.MultiEval.Power.Runit.RxAntenna.Current.Fetch(ResourceUnitRepCap.Default, RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.Runit.RxAntenna.Current.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:RXANtenna<n>:AVERage
				double value = driver.MultiEval.Power.Runit.RxAntenna.Average.Fetch(ResourceUnitRepCap.Default, RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.Runit.RxAntenna.Average.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:RXANtenna<n>:MAXimum
				double value = driver.MultiEval.Power.Runit.RxAntenna.Maximum.Fetch(ResourceUnitRepCap.Default, RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.Runit.RxAntenna.Maximum.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:POWer:RUNit<ru>:RXANtenna<n>:SDEViation
				double value = driver.MultiEval.Power.Runit.RxAntenna.StandardDev.Fetch(ResourceUnitRepCap.Default, RxAntennaRepCap.Default);
				value = driver.MultiEval.Power.Runit.RxAntenna.StandardDev.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:LSIG:RATE
				RsCmwWlanMeas_MultiEval_Sinfo_Lsig_Rate.Fetch_Data value = driver.MultiEval.Sinfo.Lsig.Rate.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:LSIG:REServed
				RsCmwWlanMeas_MultiEval_Sinfo_Lsig_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Lsig.Reserved.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:LSIG:LENGth
				RsCmwWlanMeas_MultiEval_Sinfo_Lsig_Length.Fetch_Data value = driver.MultiEval.Sinfo.Lsig.Length.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:LSIG:PARity
				RsCmwWlanMeas_MultiEval_Sinfo_Lsig_Parity.Fetch_Data value = driver.MultiEval.Sinfo.Lsig.Parity.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:LSIG:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Lsig_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Lsig.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:MCS
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Mcs.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Mcs.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:CBW
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Cbw.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Cbw.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:HTLength
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_HtLength.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.HtLength.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:SMOothing
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Smoothing.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Smoothing.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:NSOunding
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Nsounding.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Nsounding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:REServed
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Reserved.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:AGGRegation
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Aggregation.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Aggregation.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:STBCoding
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_StbCoding.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.StbCoding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:FECCoding
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_FecCoding.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.FecCoding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:SHORtgi
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_ShortGi.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.ShortGi.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:NESS
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Ness.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Ness.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Crc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HTSig:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Htsig_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Htsig.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:BW
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Bw.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Bw.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:REServed<index>
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Reserved.Fetch(ReservedRepCap.Default);
				value = driver.MultiEval.Sinfo.VhtSig.Reserved.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:STBC
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Stbc.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Stbc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:GID
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Gid.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Gid.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:SUNSts
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Sunsts.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Sunsts.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:PAID
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Paid.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Paid.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:TXOP
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_TxOp.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.TxOp.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:SGI
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Sgi.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Sgi.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:SDISambig
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Sdisambiguity.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Sdisambiguity.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:FECCoding
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_FecCoding.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.FecCoding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:LDPC
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Ldpc.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Ldpc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:SMCS
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Smcs.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Smcs.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:BEAMformed
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Beamformed.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Beamformed.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Crc.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Crc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:VHTSig:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_VhtSig_Tail.Fetch_Data value = driver.MultiEval.Sinfo.VhtSig.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:FORMat
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Format.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Format.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:BEAMchange
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_BeamChange.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.BeamChange.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:ULDL
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_UlDl.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.UlDl.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:MCS
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Mcs.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Mcs.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:DCM
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Dcm.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Dcm.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:BSSColor
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_BssColor.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.BssColor.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:REServed<index>
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Reserved.Fetch(ReservedRepCap.Default);
				value = driver.MultiEval.Sinfo.Hesu.Reserved.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:SPATialreuse
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_SpatialReuse.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.SpatialReuse.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:BW
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Bw.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Bw.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:GILTfsize
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_GiltfSize.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.GiltfSize.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:NSTS
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Nsts.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Nsts.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:TXOP
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_TxOp.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.TxOp.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:CODing
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Coding.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Coding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:LDPC
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Ldpc.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Ldpc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:STBC
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Stbc.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Stbc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:TXBF
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_TxBf.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.TxBf.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:PFECpadding
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_PfecPadding.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.PfecPadding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:PEDisambig
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_PeDisambiguity.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.PeDisambiguity.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:DOPPler
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Doppler.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Doppler.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Crc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HESU:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Hesu_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Hesu.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:ULDL
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_UlDl.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.UlDl.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:BMCS
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Bmcs.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Bmcs.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:BDCM
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Bdcm.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Bdcm.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:BSSColor
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_BssColor.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.BssColor.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:SPATialreuse
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_SpatialReuse.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.SpatialReuse.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:BW
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Bw.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Bw.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:NSBSymbols
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_NsbSymbols.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.NsbSymbols.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:SBCompress
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_SbCompress.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.SbCompress.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:GILTfsize
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_GiltfSize.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.GiltfSize.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:DOPPler
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Doppler.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Doppler.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:TXOP
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_TxOp.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.TxOp.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:REServed
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Reserved.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:NLTFsymbols
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_NltfSymbols.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.NltfSymbols.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:LDPC
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Ldpc.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Ldpc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:STBC
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Stbc.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Stbc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:PFECpadding
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_PfecPadding.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.PfecPadding.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:PEDisambig
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_PeDisambiguity.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.PeDisambiguity.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Crc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEMU:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Hemu_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Hemu.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:FORMat
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_Format.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.Format.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:BSSColor
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_BssColor.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.BssColor.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:SPATialreuse<index>
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_SpatialReuse.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.SpatialReuse.Fetch(SpatialRepCap.Default);
				value = driver.MultiEval.Sinfo.Hetb.SpatialReuse.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:REServed<index>
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.Reserved.Fetch(ReservedRepCap.Default);
				value = driver.MultiEval.Sinfo.Hetb.Reserved.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:BW
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_Bw.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.Bw.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:TXOP
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_TxOp.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.TxOp.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.Crc.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HETB:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Hetb_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Hetb.Tail.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:CFIeld:RUALlocation
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Cfield_RuAllocation.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.RuAllocation.Fetch(ChannelRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.RuAllocation.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:CFIeld:CRU
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Cfield_Cru.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Cru.Fetch(ChannelRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Cru.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:CFIeld:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Cfield_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Crc.Fetch(ChannelRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Crc.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:CFIeld:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Cfield_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Tail.Fetch(ChannelRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Cfield.Tail.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:STAid
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Staid.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Staid.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Staid.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:NSTS
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Nsts.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Nsts.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Nsts.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:TXBeamform
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_TxBeamforming.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.TxBeamforming.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.TxBeamforming.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:SPAConfig
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_SpaConfig.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.SpaConfig.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.SpaConfig.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:MCS
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Mcs.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Mcs.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Mcs.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:DCM
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Dcm.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Dcm.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Dcm.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:REServed
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Reserved.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Reserved.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Reserved.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:CODing
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Coding.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Coding.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Coding.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:CRC
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Crc.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Crc.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Crc.Fetch();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SINFo:HEB:CHANnel<ch_index>:UFIeld<usr_index>:TAIL
				RsCmwWlanMeas_MultiEval_Sinfo_Heb_Channel_Ufield_Tail.Fetch_Data value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Tail.Fetch(ChannelRepCap.Default, UserIxRepCap.Default);
				value = driver.MultiEval.Sinfo.Heb.Channel.Ufield.Tail.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:CURRent
				List<double> value = driver.MultiEval.UtError.Current.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:CURRent
				List<double> value = driver.MultiEval.UtError.Current.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Current.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:AVERage
				List<double> value = driver.MultiEval.UtError.Average.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:AVERage
				List<double> value = driver.MultiEval.UtError.Average.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Average.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MAXimum
				List<double> value = driver.MultiEval.UtError.Maximum.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MAXimum
				List<double> value = driver.MultiEval.UtError.Maximum.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Maximum.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MINimum
				List<double> value = driver.MultiEval.UtError.Minimum.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MINimum
				List<double> value = driver.MultiEval.UtError.Minimum.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Minimum.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:LIMit
				List<double> value = driver.MultiEval.UtError.Limit.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Limit.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:LIMit
				List<double> value = driver.MultiEval.UtError.Limit.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Limit.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:LIMit
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Limit.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Limit.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:CURRent
				List<double> value = driver.MultiEval.UtError.Margin.Current.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:CURRent
				List<double> value = driver.MultiEval.UtError.Margin.Current.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Margin.Current.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:AVERage
				List<double> value = driver.MultiEval.UtError.Margin.Average.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:AVERage
				List<double> value = driver.MultiEval.UtError.Margin.Average.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Margin.Average.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MAXimum
				List<double> value = driver.MultiEval.UtError.Margin.Maximum.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MAXimum
				List<double> value = driver.MultiEval.UtError.Margin.Maximum.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Margin.Maximum.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MINimum
				List<double> value = driver.MultiEval.UtError.Margin.Minimum.Read(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MINimum
				List<double> value = driver.MultiEval.UtError.Margin.Minimum.Fetch(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:UTERror<n>:MARGin:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.UtError.Margin.Minimum.Calculate(UtErrorRepCap.Default);
				value = driver.MultiEval.UtError.Margin.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:CURRent
				List<double> value = driver.MultiEval.SpectrFlatness.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:CURRent
				List<double> value = driver.MultiEval.SpectrFlatness.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:AVERage
				List<double> value = driver.MultiEval.SpectrFlatness.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:AVERage
				List<double> value = driver.MultiEval.SpectrFlatness.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MAXimum
				List<double> value = driver.MultiEval.SpectrFlatness.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MAXimum
				List<double> value = driver.MultiEval.SpectrFlatness.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MINimum
				List<double> value = driver.MultiEval.SpectrFlatness.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MINimum
				List<double> value = driver.MultiEval.SpectrFlatness.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:CURRent
				List<int> value = driver.MultiEval.SpectrFlatness.X.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:CURRent
				List<int> value = driver.MultiEval.SpectrFlatness.X.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:AVERage
				List<int> value = driver.MultiEval.SpectrFlatness.X.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:AVERage
				List<int> value = driver.MultiEval.SpectrFlatness.X.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:MAXimum
				List<int> value = driver.MultiEval.SpectrFlatness.X.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:MAXimum
				List<int> value = driver.MultiEval.SpectrFlatness.X.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:MINimum
				List<int> value = driver.MultiEval.SpectrFlatness.X.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:X:MINimum
				List<int> value = driver.MultiEval.SpectrFlatness.X.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Mimo.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Mimo.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:CURRent
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:CURRent
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:AVERage
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:AVERage
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:MAXimum
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:MAXimum
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:MINimum
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<instance>:MEValuation:SFLatness:MIMO<n>:X:MINimum
				List<int> value = driver.MultiEval.SpectrFlatness.Mimo.X.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.SpectrFlatness.Mimo.X.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW
				RsCmwWlanMeas_MultiEval_TsMask_Obw.ResultData value = driver.MultiEval.TsMask.Obw.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW
				RsCmwWlanMeas_MultiEval_TsMask_Obw.ResultData value = driver.MultiEval.TsMask.Obw.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:SEGMents
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Segments.ResultData value = driver.MultiEval.TsMask.Obw.Segments.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:SEGMents
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Segments.ResultData value = driver.MultiEval.TsMask.Obw.Segments.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:MIMO<n>
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Mimo.ResultData value = driver.MultiEval.TsMask.Obw.Mimo.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Obw.Mimo.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:MIMO<n>
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Mimo.ResultData value = driver.MultiEval.TsMask.Obw.Mimo.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Obw.Mimo.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:MIMO<n>:SEGMents
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Mimo_Segments.ResultData value = driver.MultiEval.TsMask.Obw.Mimo.Segments.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Obw.Mimo.Segments.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OBW:MIMO<n>:SEGMents
				RsCmwWlanMeas_MultiEval_TsMask_Obw_Mimo_Segments.ResultData value = driver.MultiEval.TsMask.Obw.Mimo.Segments.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Obw.Mimo.Segments.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Current.ResultData value = driver.MultiEval.TsMask.Ofdm.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Current.ResultData value = driver.MultiEval.TsMask.Ofdm.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Current.Calculate_Data value = driver.MultiEval.TsMask.Ofdm.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Average.ResultData value = driver.MultiEval.TsMask.Ofdm.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Average.ResultData value = driver.MultiEval.TsMask.Ofdm.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Average.Calculate_Data value = driver.MultiEval.TsMask.Ofdm.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Maximum.ResultData value = driver.MultiEval.TsMask.Ofdm.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Maximum.ResultData value = driver.MultiEval.TsMask.Ofdm.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:OFDM:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Ofdm_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Ofdm.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Current.ResultData value = driver.MultiEval.TsMask.Dsss.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Current.ResultData value = driver.MultiEval.TsMask.Dsss.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Current.Calculate_Data value = driver.MultiEval.TsMask.Dsss.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Average.Read_Data value = driver.MultiEval.TsMask.Dsss.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Average.Fetch_Data value = driver.MultiEval.TsMask.Dsss.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Average.Calculate_Data value = driver.MultiEval.TsMask.Dsss.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Maximum.Read_Data value = driver.MultiEval.TsMask.Dsss.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Maximum.Fetch_Data value = driver.MultiEval.TsMask.Dsss.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:DSSS:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Dsss_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Dsss.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Current.ResultData value = driver.MultiEval.TsMask.Nsiso.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Current.ResultData value = driver.MultiEval.TsMask.Nsiso.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Current.Calculate_Data value = driver.MultiEval.TsMask.Nsiso.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Average.ResultData value = driver.MultiEval.TsMask.Nsiso.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Average.ResultData value = driver.MultiEval.TsMask.Nsiso.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Average.Calculate_Data value = driver.MultiEval.TsMask.Nsiso.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Maximum.Read_Data value = driver.MultiEval.TsMask.Nsiso.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Maximum.Fetch_Data value = driver.MultiEval.TsMask.Nsiso.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:NSISo:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Nsiso_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Nsiso.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Current.ResultData value = driver.MultiEval.TsMask.Acsiso.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Current.ResultData value = driver.MultiEval.TsMask.Acsiso.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Current.Calculate_Data value = driver.MultiEval.TsMask.Acsiso.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Average.ResultData value = driver.MultiEval.TsMask.Acsiso.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Average.ResultData value = driver.MultiEval.TsMask.Acsiso.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Average.Calculate_Data value = driver.MultiEval.TsMask.Acsiso.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Maximum.ResultData value = driver.MultiEval.TsMask.Acsiso.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Maximum.ResultData value = driver.MultiEval.TsMask.Acsiso.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:ACSiso:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Acsiso_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Acsiso.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Current.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Average.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Current.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Average.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Current.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Average.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Segments_Frequency_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Segments.Frequency.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Current.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Average.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MIMO<n>:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Mimo_Frequency_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Calculate(MimoRepCap.Default);
				value = driver.MultiEval.TsMask.Mimo.Frequency.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Current.ResultData value = driver.MultiEval.TsMask.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Current.ResultData value = driver.MultiEval.TsMask.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Current.Calculate_Data value = driver.MultiEval.TsMask.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Average.ResultData value = driver.MultiEval.TsMask.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Average.ResultData value = driver.MultiEval.TsMask.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Average.Calculate_Data value = driver.MultiEval.TsMask.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Maximum.ResultData value = driver.MultiEval.TsMask.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Maximum.ResultData value = driver.MultiEval.TsMask.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Minimum.ResultData value = driver.MultiEval.TsMask.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Minimum.ResultData value = driver.MultiEval.TsMask.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Current.ResultData value = driver.MultiEval.TsMask.Segments.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Current.ResultData value = driver.MultiEval.TsMask.Segments.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Current.Calculate_Data value = driver.MultiEval.TsMask.Segments.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Average.ResultData value = driver.MultiEval.TsMask.Segments.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Average.ResultData value = driver.MultiEval.TsMask.Segments.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Average.Calculate_Data value = driver.MultiEval.TsMask.Segments.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Maximum.ResultData value = driver.MultiEval.TsMask.Segments.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Maximum.ResultData value = driver.MultiEval.TsMask.Segments.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Segments.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Minimum.ResultData value = driver.MultiEval.TsMask.Segments.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Minimum.ResultData value = driver.MultiEval.TsMask.Segments.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Segments.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Current.Calculate_Data value = driver.MultiEval.TsMask.Segments.Frequency.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Average.Calculate_Data value = driver.MultiEval.TsMask.Segments.Frequency.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Segments.Frequency.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Segments.Frequency.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:SEGMents:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Segments_Frequency_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Segments.Frequency.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Frequency.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Current.ResultData value = driver.MultiEval.TsMask.Frequency.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:CURRent
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Current.Calculate_Data value = driver.MultiEval.TsMask.Frequency.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Frequency.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Average.ResultData value = driver.MultiEval.TsMask.Frequency.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:AVERage
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Average.Calculate_Data value = driver.MultiEval.TsMask.Frequency.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Frequency.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Maximum.ResultData value = driver.MultiEval.TsMask.Frequency.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MAXimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Maximum.Calculate_Data value = driver.MultiEval.TsMask.Frequency.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Frequency.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Minimum.ResultData value = driver.MultiEval.TsMask.Frequency.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TSMask:FREQuency:MINimum
				RsCmwWlanMeas_MultiEval_TsMask_Frequency_Minimum.Calculate_Data value = driver.MultiEval.TsMask.Frequency.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mimo.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:MIMO<n>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:MIMO<n>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:MIMO<n>:SEGMent<seg>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:MIMO<n>:SEGMent<seg>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Mimo.Segment.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:SEGMent<seg>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Segment.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MASK:SEGMent<seg>
				List<double> value = driver.MultiEval.Trace.TsMask.Mask.Segment.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Mask.Segment.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:CURRent
				List<double> value = driver.MultiEval.Trace.TsMask.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:AVERage
				List<double> value = driver.MultiEval.Trace.TsMask.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:MAXimum
				List<double> value = driver.MultiEval.Trace.TsMask.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:FREQuency
				List<double> value = driver.MultiEval.Trace.TsMask.Frequency.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Frequency.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TSMask:FREQuency
				List<double> value = driver.MultiEval.Trace.TsMask.Frequency.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.TsMask.Frequency.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:CFERror
				List<double> value = driver.MultiEval.Trace.CfError.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.CfError.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:CFERror
				List<double> value = driver.MultiEval.Trace.CfError.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.CfError.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:TERRor
				List<double> value = driver.MultiEval.Trace.Terror.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.Terror.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:TERRor
				List<double> value = driver.MultiEval.Trace.Terror.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.Terror.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Read(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Read(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Read(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Read(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Read(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Read(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Read(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Read(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Read(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Fetch(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Fetch(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MIMO:RXANtenna<n>:STReam<s>:SEGMent<seg>:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Calculate(1.0, 1.0, 1.0, RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Calculate(RxAntennaRepCap.Default, StreamRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Mimo.RxAntenna.Stream.Segment.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness[:OFDM]:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Ofdm.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Current.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Current.Calculate();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Average.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Average.Calculate();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Maximum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Maximum.Calculate();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Minimum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Calculate(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Current.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Calculate(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Average.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MAXimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Calculate(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Maximum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Fetch();
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:SEGMent<seg>:MINimum
				List<ResultStatus2enum> value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Calculate(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Calculate(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Segment.Minimum.Calculate();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:CURRent
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:AVERage
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:MAXimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:SFLatness:ACSiso:MINimum
				List<double> value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.SpectrFlatness.Acsiso.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:IQConst:INPHase
				List<double> value = driver.MultiEval.Trace.IqConstant.Inphase.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:IQConst:INPHase
				List<double> value = driver.MultiEval.Trace.IqConstant.Inphase.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:IQConst:QUADrature
				List<double> value = driver.MultiEval.Trace.IqConstant.Quadrature.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:IQConst:QUADrature
				List<double> value = driver.MultiEval.Trace.IqConstant.Quadrature.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:DSSS:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Dsss.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Dsss.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:CARRier:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Carrier.Mimo.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:SYMBol:MINimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Symbol.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Symbol.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Carrier.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:OFDM:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Ofdm.Symbol.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:CARRier:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Carrier.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:NSISo:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Nsiso.Symbol.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:CURRent
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:AVERage
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:EVMagnitude:ACSiso:SYMBol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.EvMagnitude.Acsiso.Symbol.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Mimo.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Mimo.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Time.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Time.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:REDGe:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.RisingEdge.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Read(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Fetch(MimoRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Read(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Read(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MIMO<n>:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0, MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Fetch(MimoRepCap.Default, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Mimo.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Time.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Time.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:FEDGe:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.FallingEdge.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Current.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Average.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Minimum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Time.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Time.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Current.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Average.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Maximum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Minimum.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Read(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Read(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Read(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Read();
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:TRACe:PVTime:SEGMent<seg>:TIME
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Fetch(1.0, 1.0, 1.0, SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Fetch(1.0, 1.0, 1.0);
				value = driver.MultiEval.Trace.PowerVsTime.Segment.Time.Fetch();
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:CURRent
				double value = driver.MultiEval.PowerVsTime.Terror.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:CURRent
				double value = driver.MultiEval.PowerVsTime.Terror.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:CURRent
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.Terror.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:AVERage
				double value = driver.MultiEval.PowerVsTime.Terror.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:AVERage
				double value = driver.MultiEval.PowerVsTime.Terror.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:AVERage
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.Terror.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MINimum
				double value = driver.MultiEval.PowerVsTime.Terror.Minimum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MINimum
				double value = driver.MultiEval.PowerVsTime.Terror.Minimum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MINimum
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.Terror.Minimum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MAXimum
				double value = driver.MultiEval.PowerVsTime.Terror.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MAXimum
				double value = driver.MultiEval.PowerVsTime.Terror.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:MAXimum
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.Terror.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:SDEViation
				double value = driver.MultiEval.PowerVsTime.Terror.StandardDev.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:SDEViation
				double value = driver.MultiEval.PowerVsTime.Terror.StandardDev.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TERRor:SDEViation
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.Terror.StandardDev.Calculate();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.RisingEdge.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Current.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Current.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.RisingEdge.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Average.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Average.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.RisingEdge.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Maximum.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:REDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_RisingEdge_Maximum.ResultData value = driver.MultiEval.PowerVsTime.RisingEdge.Maximum.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.FallingEdge.Current.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Current.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Current.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:CURRent
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Current.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Current.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.FallingEdge.Average.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Average.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Average.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:AVERage
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Average.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Average.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.FallingEdge.Maximum.Calculate();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Maximum.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Maximum.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:FEDGe:MAXimum
				RsCmwWlanMeas_MultiEval_PowerVsTime_FallingEdge_Maximum.ResultData value = driver.MultiEval.PowerVsTime.FallingEdge.Maximum.Fetch();				
			}
			{	// READ:WLAN:MEASurement<Instance>:MEValuation:PVTime:TEDistrib
				RsCmwWlanMeas_MultiEval_PowerVsTime_TeDistribution.ResultData value = driver.MultiEval.PowerVsTime.TeDistribution.Read();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:PVTime:TEDistrib
				RsCmwWlanMeas_MultiEval_PowerVsTime_TeDistribution.ResultData value = driver.MultiEval.PowerVsTime.TeDistribution.Fetch();				
			}
			{	// CALCulate:WLAN:MEASurement<Instance>:MEValuation:PVTime:TEDistrib
				ResultStatus2enum value = driver.MultiEval.PowerVsTime.TeDistribution.Calculate();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:STATe
				ResourceStateEnum value = driver.MultiEval.State.Fetch();				
			}
			{	// FETCh:WLAN:MEASurement<Instance>:MEValuation:STATe:ALL
				RsCmwWlanMeas_MultiEval_State_All.Fetch_Data value = driver.MultiEval.State.All.Fetch();				
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:MGAP
				double value = driver.Trigger.MultiEval.Mgap;
				driver.Trigger.MultiEval.Mgap = value;
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:SOURce
				string value = driver.Trigger.MultiEval.Source;
				driver.Trigger.MultiEval.Source = value;
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:THReshold
				double value = driver.Trigger.MultiEval.Threshold;
				driver.Trigger.MultiEval.Threshold = value;
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:SLOPe
				foreach (TriggerSlopeEnum x in new TriggerSlopeEnum[] { TriggerSlopeEnum.FEDGe, TriggerSlopeEnum.OFF, TriggerSlopeEnum.ON, TriggerSlopeEnum.REDGe })
				{
					driver.Trigger.MultiEval.Slope = x;
					TriggerSlopeEnum value = driver.Trigger.MultiEval.Slope;
				}
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Trigger.MultiEval.Timeout;
				driver.Trigger.MultiEval.Timeout = value;
			}
			{	// TRIGger:WLAN:MEASurement<Instance>:MEValuation:CATalog:SOURce
				List<string> value = driver.Trigger.MultiEval.Catalog.GetSource(false);
				value = driver.Trigger.MultiEval.Catalog.GetSource();				
			}
		}
	}
}