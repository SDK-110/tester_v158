using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwLteMeas;

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
			RsCmwLteMeas driver = new RsCmwLteMeas("TCPIP::localhost::INSTR", true, true);
			{	// ROUTe:LTE:MEASurement<Instance>
				RsCmwLteMeas_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:LTE:MEASurement<Instance>:SCENario:SALone
				RsCmwLteMeas_Route_Scenario.Salone_Data value = driver.Route.Scenario.Salone;
				driver.Route.Scenario.Salone = value;
			}
			{	// ROUTe:LTE:MEASurement<Instance>:SCENario
				foreach (ScenarioEnum x in new ScenarioEnum[] { ScenarioEnum.CSPath, ScenarioEnum.MAPRotocol, ScenarioEnum.NAV, ScenarioEnum.SALone })
				{
					ScenarioEnum value = driver.Route.Scenario.Value;
				}
			}
			{	// ROUTe:LTE:MEASurement<Instance>:SCENario:CSPath
				RsCmwLteMeas_Route_Scenario_CombinedSignalPath.Get_Data value = driver.Route.Scenario.CombinedSignalPath.Get();				
			}
			{	// ROUTe:LTE:MEASurement<Instance>:SCENario:CSPath
				driver.Route.Scenario.CombinedSignalPath.Set("1", "1");
				driver.Route.Scenario.CombinedSignalPath.Set("1");
			}
			{	// ROUTe:LTE:MEASurement<Instance>:SCENario:MAPRotocol
				driver.Route.Scenario.MaProtocol.Set("1");
				driver.Route.Scenario.MaProtocol.Set();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:BAND
				foreach (BandEnum x in new BandEnum[] { BandEnum.OB1, BandEnum.OB10, BandEnum.OB11, BandEnum.OB12, BandEnum.OB13, BandEnum.OB14, BandEnum.OB15, BandEnum.OB16, BandEnum.OB17, BandEnum.OB18, BandEnum.OB19, BandEnum.OB2, BandEnum.OB20, BandEnum.OB21, BandEnum.OB22, BandEnum.OB23, BandEnum.OB24, BandEnum.OB25, BandEnum.OB250, BandEnum.OB26, BandEnum.OB27, BandEnum.OB28, BandEnum.OB3, BandEnum.OB30, BandEnum.OB31, BandEnum.OB33, BandEnum.OB34, BandEnum.OB35, BandEnum.OB36, BandEnum.OB37, BandEnum.OB38, BandEnum.OB39, BandEnum.OB4, BandEnum.OB40, BandEnum.OB41, BandEnum.OB42, BandEnum.OB43, BandEnum.OB44, BandEnum.OB45, BandEnum.OB46, BandEnum.OB47, BandEnum.OB48, BandEnum.OB49, BandEnum.OB5, BandEnum.OB50, BandEnum.OB51, BandEnum.OB52, BandEnum.OB53, BandEnum.OB6, BandEnum.OB65, BandEnum.OB66, BandEnum.OB68, BandEnum.OB7, BandEnum.OB70, BandEnum.OB71, BandEnum.OB72, BandEnum.OB73, BandEnum.OB74, BandEnum.OB8, BandEnum.OB85, BandEnum.OB9 })
				{
					driver.Configure.Band = x;
					BandEnum value = driver.Configure.Band;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:STYPe
				foreach (SignalTypeEnum x in new SignalTypeEnum[] { SignalTypeEnum.SL, SignalTypeEnum.UL })
				{
					driver.Configure.Stype = x;
					SignalTypeEnum value = driver.Configure.Stype;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:DMODe
				foreach (DuplexModeEnum x in new DuplexModeEnum[] { DuplexModeEnum.FDD, DuplexModeEnum.FTDD, DuplexModeEnum.TDD })
				{
					driver.Configure.Dmode = x;
					DuplexModeEnum value = driver.Configure.Dmode;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:FSTRucture
				foreach (FrameStructureEnum x in new FrameStructureEnum[] { FrameStructureEnum.T1, FrameStructureEnum.T2 })
				{
					FrameStructureEnum value = driver.Configure.Fstructure;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:EATTenuation
				double value = driver.Configure.RfSettings.Eattenuation;
				driver.Configure.RfSettings.Eattenuation = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:UMARgin
				double value = driver.Configure.RfSettings.Umargin;
				driver.Configure.RfSettings.Umargin = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:ENPower
				double value = driver.Configure.RfSettings.EnvelopePower;
				driver.Configure.RfSettings.EnvelopePower = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:FOFFset
				int value = driver.Configure.RfSettings.FreqOffset;
				driver.Configure.RfSettings.FreqOffset = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:MLOFfset
				double value = driver.Configure.RfSettings.MlOffset;
				driver.Configure.RfSettings.MlOffset = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings[:PCC]:FREQuency
				double value = driver.Configure.RfSettings.Pcc.Frequency;
				driver.Configure.RfSettings.Pcc.Frequency = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:CC<Nr>:FREQuency
				double value = driver.Configure.RfSettings.Cc.Frequency.Get(CarrierComponentRepCap.Default);
				value = driver.Configure.RfSettings.Cc.Frequency.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:RFSettings:CC<Nr>:FREQuency
				driver.Configure.RfSettings.Cc.Frequency.Set(1.0, CarrierComponentRepCap.Default);
				driver.Configure.RfSettings.Cc.Frequency.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MODE:CSPath
				foreach (CarrAggrModeEnum x in new CarrAggrModeEnum[] { CarrAggrModeEnum.ICD, CarrAggrModeEnum.ICE, CarrAggrModeEnum.INTRaband, CarrAggrModeEnum.OFF })
				{
					CarrAggrModeEnum value = driver.Configure.CarrierAggregation.Mode.CombinedSignalPath;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MODE
				foreach (CarrAggrModeEnum x in new CarrAggrModeEnum[] { CarrAggrModeEnum.ICD, CarrAggrModeEnum.ICE, CarrAggrModeEnum.INTRaband, CarrAggrModeEnum.OFF })
				{
					driver.Configure.CarrierAggregation.Mode.Value = x;
					CarrAggrModeEnum value = driver.Configure.CarrierAggregation.Mode.Value;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MCARrier:ENHanced
				foreach (MeasCarrierEnhancedEnum x in new MeasCarrierEnhancedEnum[] { MeasCarrierEnhancedEnum.CC1, MeasCarrierEnhancedEnum.CC2, MeasCarrierEnhancedEnum.CC3, MeasCarrierEnhancedEnum.CC4 })
				{
					driver.Configure.CarrierAggregation.Mcarrier.Enhanced = x;
					MeasCarrierEnhancedEnum value = driver.Configure.CarrierAggregation.Mcarrier.Enhanced;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:FREQuency:AGGRegated:LOW
				double value = driver.Configure.CarrierAggregation.Frequency.Aggregated.Low;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:FREQuency:AGGRegated:CENTer
				double value = driver.Configure.CarrierAggregation.Frequency.Aggregated.Center;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:FREQuency:AGGRegated:HIGH
				double value = driver.Configure.CarrierAggregation.Frequency.Aggregated.High;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:CBANdwidth:AGGRegated
				double value = driver.Configure.CarrierAggregation.ChannelBw.Aggregated;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation[:SCC<Nr>]:ACSPacing
				driver.Configure.CarrierAggregation.Scc.AcSpacing.Set(SecondaryCCRepCap.Default);
				driver.Configure.CarrierAggregation.Scc.AcSpacing.SetAndWait(SecondaryCCRepCap.Default);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MAPing:SCC<Carrier>
				string value = driver.Configure.CarrierAggregation.Maping.GetScc(SecondaryCCRepCap.CC1);
				value = driver.Configure.CarrierAggregation.Maping.GetScc();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MAPing:PCC
				string value = driver.Configure.CarrierAggregation.Maping.Pcc;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CAGGregation:MAPing
				RsCmwLteMeas_Configure_CarrierAggregation_Maping.Value_Data value = driver.Configure.CarrierAggregation.Maping.Value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:EMTC:ENABle
				bool value = driver.Configure.Emtc.Enable;
				driver.Configure.Emtc.Enable = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:EMTC:NBANd
				int value = driver.Configure.Emtc.Nband;
				driver.Configure.Emtc.Nband = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Configure.MultiEval.Timeout;
				driver.Configure.MultiEval.Timeout = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MMODe
				foreach (MeasurementModeEnum x in new MeasurementModeEnum[] { MeasurementModeEnum.MELMode, MeasurementModeEnum.NORMal, MeasurementModeEnum.TMODe })
				{
					driver.Configure.MultiEval.Mmode = x;
					MeasurementModeEnum value = driver.Configure.MultiEval.Mmode;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.MultiEval.Repetition = x;
					RepeatEnum value = driver.Configure.MultiEval.Repetition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.MultiEval.Scondition = x;
					StopConditionEnum value = driver.Configure.MultiEval.Scondition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:ULDL
				int value = driver.Configure.MultiEval.UlDl;
				driver.Configure.MultiEval.UlDl = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SSUBframe
				int value = driver.Configure.MultiEval.Ssubframe;
				driver.Configure.MultiEval.Ssubframe = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MOEXception
				bool value = driver.Configure.MultiEval.MoException;
				driver.Configure.MultiEval.MoException = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:CPRefix
				foreach (CyclicPrefixEnum x in new CyclicPrefixEnum[] { CyclicPrefixEnum.EXTended, CyclicPrefixEnum.NORMal })
				{
					driver.Configure.MultiEval.Cprefix = x;
					CyclicPrefixEnum value = driver.Configure.MultiEval.Cprefix;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:CTYPe
				foreach (ChannelTypeDetectionEnum x in new ChannelTypeDetectionEnum[] { ChannelTypeDetectionEnum.AUTO, ChannelTypeDetectionEnum.PUCCh, ChannelTypeDetectionEnum.PUSCh })
				{
					driver.Configure.MultiEval.Ctype = x;
					ChannelTypeDetectionEnum value = driver.Configure.MultiEval.Ctype;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCTYpe
				foreach (SidelinkChannelTypeEnum x in new SidelinkChannelTypeEnum[] { SidelinkChannelTypeEnum.PSCCh, SidelinkChannelTypeEnum.PSSCh })
				{
					driver.Configure.MultiEval.Sctype = x;
					SidelinkChannelTypeEnum value = driver.Configure.MultiEval.Sctype;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:PSEarch
				bool value = driver.Configure.MultiEval.PeakSearch;
				driver.Configure.MultiEval.PeakSearch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:PFORmat
				foreach (PucchFormatEnum x in new PucchFormatEnum[] { PucchFormatEnum.F1, PucchFormatEnum.F1A, PucchFormatEnum.F1B, PucchFormatEnum.F2, PucchFormatEnum.F2A, PucchFormatEnum.F2B, PucchFormatEnum.F3 })
				{
					driver.Configure.MultiEval.Pformat = x;
					PucchFormatEnum value = driver.Configure.MultiEval.Pformat;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:NVFilter
				int value = driver.Configure.MultiEval.Nvfilter;
				driver.Configure.MultiEval.Nvfilter = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:ORVFilter
				int value = driver.Configure.MultiEval.OrvFilter;
				driver.Configure.MultiEval.OrvFilter = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:CTVFilter
				foreach (ChannelTypeVewFilterEnum x in new ChannelTypeVewFilterEnum[] { ChannelTypeVewFilterEnum.OFF, ChannelTypeVewFilterEnum.ON, ChannelTypeVewFilterEnum.PUCCh, ChannelTypeVewFilterEnum.PUSCh })
				{
					driver.Configure.MultiEval.CtvFilter = x;
					ChannelTypeVewFilterEnum value = driver.Configure.MultiEval.CtvFilter;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:DSSPusch
				int value = driver.Configure.MultiEval.DssPusch;
				driver.Configure.MultiEval.DssPusch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:GHOPping
				bool value = driver.Configure.MultiEval.Ghopping;
				driver.Configure.MultiEval.Ghopping = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MSUBframes
				RsCmwLteMeas_Configure_MultiEval.Msubframes_Data value = driver.Configure.MultiEval.Msubframes;
				driver.Configure.MultiEval.Msubframes = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MSLot
				foreach (MeasureSlotEnum x in new MeasureSlotEnum[] { MeasureSlotEnum.ALL, MeasureSlotEnum.MS0, MeasureSlotEnum.MS1 })
				{
					driver.Configure.MultiEval.Mslot = x;
					MeasureSlotEnum value = driver.Configure.MultiEval.Mslot;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:LRANge
				RsCmwLteMeas_Configure_MultiEval_List.Lrange_Data value = driver.Configure.MultiEval.List.Lrange;
				driver.Configure.MultiEval.List.Lrange = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:OSINdex
				int value = driver.Configure.MultiEval.List.OsIndex;
				driver.Configure.MultiEval.List.OsIndex = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST
				bool value = driver.Configure.MultiEval.List.Value;
				driver.Configure.MultiEval.List.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCC<c>
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Scc.Scc_Data value = driver.Configure.MultiEval.List.Segment.Scc.Get(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scc.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCC<c>
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Scc.Scc_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Scc.Scc_Data();
				driver.Configure.MultiEval.List.Segment.Scc.Set(value, SegmentRepCap.Default, SecondaryCCRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scc.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CC<c>
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Cc.Cc_Data value = driver.Configure.MultiEval.List.Segment.Cc.Get(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Cc.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CC<c>
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Cc.Cc_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Cc.Cc_Data();
				driver.Configure.MultiEval.List.Segment.Cc.Set(value, SegmentRepCap.Default, CarrierComponentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Cc.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CAGGregation:ACSPacing
				driver.Configure.MultiEval.List.Segment.CarrierAggregation.AcSpacing.Set(SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.CarrierAggregation.AcSpacing.SetAndWait(SegmentRepCap.Default);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CAGGregation:MCARrier
				MeasCarrierEnum value = driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CAGGregation:MCARrier
				foreach (MeasCarrierEnum x in new MeasCarrierEnum[] { MeasCarrierEnum.PCC, MeasCarrierEnum.SCC1 })
				{
					driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Set(x);
					driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CAGGregation:MCARrier:ENHanced
				MeasCarrierEnhancedEnum value = driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Enhanced.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Enhanced.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CAGGregation:MCARrier:ENHanced
				foreach (MeasCarrierEnhancedEnum x in new MeasCarrierEnhancedEnum[] { MeasCarrierEnhancedEnum.CC1, MeasCarrierEnhancedEnum.CC2, MeasCarrierEnhancedEnum.CC3, MeasCarrierEnhancedEnum.CC4 })
				{
					driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Enhanced.Set(x);
					driver.Configure.MultiEval.List.Segment.CarrierAggregation.Mcarrier.Enhanced.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SETup
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = driver.Configure.MultiEval.List.Segment.Setup.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SETup
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data();
				driver.Configure.MultiEval.List.Segment.Setup.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:TDD
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Tdd.Tdd_Data value = driver.Configure.MultiEval.List.Segment.Tdd.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Tdd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:TDD
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Tdd.Tdd_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Tdd.Tdd_Data();
				driver.Configure.MultiEval.List.Segment.Tdd.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Tdd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RBALlocation
				RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation.RbAllocation_Data value = driver.Configure.MultiEval.List.Segment.RbAllocation.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.RbAllocation.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RBALlocation
				RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation.RbAllocation_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation.RbAllocation_Data();
				driver.Configure.MultiEval.List.Segment.RbAllocation.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.RbAllocation.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RBALlocation:SIDelink
				RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation_Sidelink.Sidelink_Data value = driver.Configure.MultiEval.List.Segment.RbAllocation.Sidelink.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.RbAllocation.Sidelink.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RBALlocation:SIDelink
				RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation_Sidelink.Sidelink_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_RbAllocation_Sidelink.Sidelink_Data();
				driver.Configure.MultiEval.List.Segment.RbAllocation.Sidelink.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.RbAllocation.Sidelink.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Modulation.Modulation_Data value = driver.Configure.MultiEval.List.Segment.Modulation.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Modulation.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Modulation.Modulation_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Modulation.Modulation_Data();
				driver.Configure.MultiEval.List.Segment.Modulation.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Modulation.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask
				RsCmwLteMeas_Configure_MultiEval_List_Segment_SeMask.SeMask_Data value = driver.Configure.MultiEval.List.Segment.SeMask.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.SeMask.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask
				RsCmwLteMeas_Configure_MultiEval_List_Segment_SeMask.SeMask_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_SeMask.SeMask_Data();
				driver.Configure.MultiEval.List.Segment.SeMask.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.SeMask.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Aclr.Aclr_Data value = driver.Configure.MultiEval.List.Segment.Aclr.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Aclr.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Aclr.Aclr_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Aclr.Aclr_Data();
				driver.Configure.MultiEval.List.Segment.Aclr.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Aclr.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor
				bool value = driver.Configure.MultiEval.List.Segment.Pmonitor.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Pmonitor.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor
				driver.Configure.MultiEval.List.Segment.Pmonitor.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Pmonitor.Set(false);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Power.Power_Data value = driver.Configure.MultiEval.List.Segment.Power.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Power.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer
				RsCmwLteMeas_Configure_MultiEval_List_Segment_Power.Power_Data value = new RsCmwLteMeas_Configure_MultiEval_List_Segment_Power.Power_Data();
				driver.Configure.MultiEval.List.Segment.Power.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Power.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:EMTC:NBANd
				int value = driver.Configure.MultiEval.List.Segment.Emtc.Nband.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Emtc.Nband.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:EMTC:NBANd
				driver.Configure.MultiEval.List.Segment.Emtc.Nband.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Emtc.Nband.Set(1);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CMWS:CONNector
				CmwsConnectorEnum value = driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:CMWS:CONNector
				foreach (CmwsConnectorEnum x in new CmwsConnectorEnum[] { CmwsConnectorEnum.R11, CmwsConnectorEnum.R12, CmwsConnectorEnum.R13, CmwsConnectorEnum.R14, CmwsConnectorEnum.R15, CmwsConnectorEnum.R16, CmwsConnectorEnum.R17, CmwsConnectorEnum.R18, CmwsConnectorEnum.R21, CmwsConnectorEnum.R22, CmwsConnectorEnum.R23, CmwsConnectorEnum.R24, CmwsConnectorEnum.R25, CmwsConnectorEnum.R26, CmwsConnectorEnum.R27, CmwsConnectorEnum.R28, CmwsConnectorEnum.R31, CmwsConnectorEnum.R32, CmwsConnectorEnum.R33, CmwsConnectorEnum.R34, CmwsConnectorEnum.R35, CmwsConnectorEnum.R36, CmwsConnectorEnum.R37, CmwsConnectorEnum.R38, CmwsConnectorEnum.R41, CmwsConnectorEnum.R42, CmwsConnectorEnum.R43, CmwsConnectorEnum.R44, CmwsConnectorEnum.R45, CmwsConnectorEnum.R46, CmwsConnectorEnum.R47, CmwsConnectorEnum.R48, CmwsConnectorEnum.RA1, CmwsConnectorEnum.RA2, CmwsConnectorEnum.RA3, CmwsConnectorEnum.RA4, CmwsConnectorEnum.RA5, CmwsConnectorEnum.RA6, CmwsConnectorEnum.RA7, CmwsConnectorEnum.RA8, CmwsConnectorEnum.RB1, CmwsConnectorEnum.RB2, CmwsConnectorEnum.RB3, CmwsConnectorEnum.RB4, CmwsConnectorEnum.RB5, CmwsConnectorEnum.RB6, CmwsConnectorEnum.RB7, CmwsConnectorEnum.RB8 })
				{
					driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Set(x);
					driver.Configure.MultiEval.List.Segment.SingleCmw.Connector.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIST:CMWS:CMODe
				foreach (ParameterSetModeEnum x in new ParameterSetModeEnum[] { ParameterSetModeEnum.GLOBal, ParameterSetModeEnum.LIST })
				{
					driver.Configure.MultiEval.List.SingleCmw.Cmode = x;
					ParameterSetModeEnum value = driver.Configure.MultiEval.List.SingleCmw.Cmode;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:TMODe:SCOunt
				List<int> value = driver.Configure.MultiEval.Tmode.Scount;
				driver.Configure.MultiEval.Tmode.Scount = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:TMODe:ENPower
				List<double> value = driver.Configure.MultiEval.Tmode.EnvelopePower;
				driver.Configure.MultiEval.Tmode.EnvelopePower = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:TMODe:RLEVel
				List<double> value = driver.Configure.MultiEval.Tmode.Rlevel;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation[:PCC]:PLCid
				int value = driver.Configure.MultiEval.Pcc.PlcId;
				driver.Configure.MultiEval.Pcc.PlcId = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:CC<Nr>:PLCid
				int value = driver.Configure.MultiEval.Cc.PlcId.Get(CarrierComponentRepCap.Default);
				value = driver.Configure.MultiEval.Cc.PlcId.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:CC<Nr>:PLCid
				driver.Configure.MultiEval.Cc.PlcId.Set(1, CarrierComponentRepCap.Default);
				driver.Configure.MultiEval.Cc.PlcId.Set(1);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:NSValue:CAGGregation
				foreach (NetworkSigValueEnum x in new NetworkSigValueEnum[] { NetworkSigValueEnum.NS01, NetworkSigValueEnum.NS02, NetworkSigValueEnum.NS03, NetworkSigValueEnum.NS04, NetworkSigValueEnum.NS05, NetworkSigValueEnum.NS06, NetworkSigValueEnum.NS07, NetworkSigValueEnum.NS08, NetworkSigValueEnum.NS09, NetworkSigValueEnum.NS10, NetworkSigValueEnum.NS11, NetworkSigValueEnum.NS12, NetworkSigValueEnum.NS13, NetworkSigValueEnum.NS14, NetworkSigValueEnum.NS15, NetworkSigValueEnum.NS16, NetworkSigValueEnum.NS17, NetworkSigValueEnum.NS18, NetworkSigValueEnum.NS19, NetworkSigValueEnum.NS20, NetworkSigValueEnum.NS21, NetworkSigValueEnum.NS22, NetworkSigValueEnum.NS23, NetworkSigValueEnum.NS24, NetworkSigValueEnum.NS25, NetworkSigValueEnum.NS26, NetworkSigValueEnum.NS27, NetworkSigValueEnum.NS28, NetworkSigValueEnum.NS29, NetworkSigValueEnum.NS30, NetworkSigValueEnum.NS31, NetworkSigValueEnum.NS32 })
				{
					driver.Configure.MultiEval.Nsvalue.CarrierAggregation = x;
					NetworkSigValueEnum value = driver.Configure.MultiEval.Nsvalue.CarrierAggregation;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:NSValue
				foreach (NetworkSigValueNoCarrAggrEnum x in new NetworkSigValueNoCarrAggrEnum[] { NetworkSigValueNoCarrAggrEnum.NS01, NetworkSigValueNoCarrAggrEnum.NS02, NetworkSigValueNoCarrAggrEnum.NS03, NetworkSigValueNoCarrAggrEnum.NS04, NetworkSigValueNoCarrAggrEnum.NS05, NetworkSigValueNoCarrAggrEnum.NS06, NetworkSigValueNoCarrAggrEnum.NS07, NetworkSigValueNoCarrAggrEnum.NS08, NetworkSigValueNoCarrAggrEnum.NS09, NetworkSigValueNoCarrAggrEnum.NS10, NetworkSigValueNoCarrAggrEnum.NS100, NetworkSigValueNoCarrAggrEnum.NS101, NetworkSigValueNoCarrAggrEnum.NS102, NetworkSigValueNoCarrAggrEnum.NS103, NetworkSigValueNoCarrAggrEnum.NS104, NetworkSigValueNoCarrAggrEnum.NS105, NetworkSigValueNoCarrAggrEnum.NS106, NetworkSigValueNoCarrAggrEnum.NS107, NetworkSigValueNoCarrAggrEnum.NS108, NetworkSigValueNoCarrAggrEnum.NS109, NetworkSigValueNoCarrAggrEnum.NS11, NetworkSigValueNoCarrAggrEnum.NS110, NetworkSigValueNoCarrAggrEnum.NS111, NetworkSigValueNoCarrAggrEnum.NS112, NetworkSigValueNoCarrAggrEnum.NS113, NetworkSigValueNoCarrAggrEnum.NS114, NetworkSigValueNoCarrAggrEnum.NS115, NetworkSigValueNoCarrAggrEnum.NS116, NetworkSigValueNoCarrAggrEnum.NS117, NetworkSigValueNoCarrAggrEnum.NS118, NetworkSigValueNoCarrAggrEnum.NS119, NetworkSigValueNoCarrAggrEnum.NS12, NetworkSigValueNoCarrAggrEnum.NS120, NetworkSigValueNoCarrAggrEnum.NS121, NetworkSigValueNoCarrAggrEnum.NS122, NetworkSigValueNoCarrAggrEnum.NS123, NetworkSigValueNoCarrAggrEnum.NS124, NetworkSigValueNoCarrAggrEnum.NS125, NetworkSigValueNoCarrAggrEnum.NS126, NetworkSigValueNoCarrAggrEnum.NS127, NetworkSigValueNoCarrAggrEnum.NS128, NetworkSigValueNoCarrAggrEnum.NS129, NetworkSigValueNoCarrAggrEnum.NS13, NetworkSigValueNoCarrAggrEnum.NS130, NetworkSigValueNoCarrAggrEnum.NS131, NetworkSigValueNoCarrAggrEnum.NS132, NetworkSigValueNoCarrAggrEnum.NS133, NetworkSigValueNoCarrAggrEnum.NS134, NetworkSigValueNoCarrAggrEnum.NS135, NetworkSigValueNoCarrAggrEnum.NS136, NetworkSigValueNoCarrAggrEnum.NS137, NetworkSigValueNoCarrAggrEnum.NS138, NetworkSigValueNoCarrAggrEnum.NS139, NetworkSigValueNoCarrAggrEnum.NS14, NetworkSigValueNoCarrAggrEnum.NS140, NetworkSigValueNoCarrAggrEnum.NS141, NetworkSigValueNoCarrAggrEnum.NS142, NetworkSigValueNoCarrAggrEnum.NS143, NetworkSigValueNoCarrAggrEnum.NS144, NetworkSigValueNoCarrAggrEnum.NS145, NetworkSigValueNoCarrAggrEnum.NS146, NetworkSigValueNoCarrAggrEnum.NS147, NetworkSigValueNoCarrAggrEnum.NS148, NetworkSigValueNoCarrAggrEnum.NS149, NetworkSigValueNoCarrAggrEnum.NS15, NetworkSigValueNoCarrAggrEnum.NS150, NetworkSigValueNoCarrAggrEnum.NS151, NetworkSigValueNoCarrAggrEnum.NS152, NetworkSigValueNoCarrAggrEnum.NS153, NetworkSigValueNoCarrAggrEnum.NS154, NetworkSigValueNoCarrAggrEnum.NS155, NetworkSigValueNoCarrAggrEnum.NS156, NetworkSigValueNoCarrAggrEnum.NS157, NetworkSigValueNoCarrAggrEnum.NS158, NetworkSigValueNoCarrAggrEnum.NS159, NetworkSigValueNoCarrAggrEnum.NS16, NetworkSigValueNoCarrAggrEnum.NS160, NetworkSigValueNoCarrAggrEnum.NS161, NetworkSigValueNoCarrAggrEnum.NS162, NetworkSigValueNoCarrAggrEnum.NS163, NetworkSigValueNoCarrAggrEnum.NS164, NetworkSigValueNoCarrAggrEnum.NS165, NetworkSigValueNoCarrAggrEnum.NS166, NetworkSigValueNoCarrAggrEnum.NS167, NetworkSigValueNoCarrAggrEnum.NS168, NetworkSigValueNoCarrAggrEnum.NS169, NetworkSigValueNoCarrAggrEnum.NS17, NetworkSigValueNoCarrAggrEnum.NS170, NetworkSigValueNoCarrAggrEnum.NS171, NetworkSigValueNoCarrAggrEnum.NS172, NetworkSigValueNoCarrAggrEnum.NS173, NetworkSigValueNoCarrAggrEnum.NS174, NetworkSigValueNoCarrAggrEnum.NS175, NetworkSigValueNoCarrAggrEnum.NS176, NetworkSigValueNoCarrAggrEnum.NS177, NetworkSigValueNoCarrAggrEnum.NS178, NetworkSigValueNoCarrAggrEnum.NS179, NetworkSigValueNoCarrAggrEnum.NS18, NetworkSigValueNoCarrAggrEnum.NS180, NetworkSigValueNoCarrAggrEnum.NS181, NetworkSigValueNoCarrAggrEnum.NS182, NetworkSigValueNoCarrAggrEnum.NS183, NetworkSigValueNoCarrAggrEnum.NS184, NetworkSigValueNoCarrAggrEnum.NS185, NetworkSigValueNoCarrAggrEnum.NS186, NetworkSigValueNoCarrAggrEnum.NS187, NetworkSigValueNoCarrAggrEnum.NS188, NetworkSigValueNoCarrAggrEnum.NS189, NetworkSigValueNoCarrAggrEnum.NS19, NetworkSigValueNoCarrAggrEnum.NS190, NetworkSigValueNoCarrAggrEnum.NS191, NetworkSigValueNoCarrAggrEnum.NS192, NetworkSigValueNoCarrAggrEnum.NS193, NetworkSigValueNoCarrAggrEnum.NS194, NetworkSigValueNoCarrAggrEnum.NS195, NetworkSigValueNoCarrAggrEnum.NS196, NetworkSigValueNoCarrAggrEnum.NS197, NetworkSigValueNoCarrAggrEnum.NS198, NetworkSigValueNoCarrAggrEnum.NS199, NetworkSigValueNoCarrAggrEnum.NS20, NetworkSigValueNoCarrAggrEnum.NS200, NetworkSigValueNoCarrAggrEnum.NS201, NetworkSigValueNoCarrAggrEnum.NS202, NetworkSigValueNoCarrAggrEnum.NS203, NetworkSigValueNoCarrAggrEnum.NS204, NetworkSigValueNoCarrAggrEnum.NS205, NetworkSigValueNoCarrAggrEnum.NS206, NetworkSigValueNoCarrAggrEnum.NS207, NetworkSigValueNoCarrAggrEnum.NS208, NetworkSigValueNoCarrAggrEnum.NS209, NetworkSigValueNoCarrAggrEnum.NS21, NetworkSigValueNoCarrAggrEnum.NS210, NetworkSigValueNoCarrAggrEnum.NS211, NetworkSigValueNoCarrAggrEnum.NS212, NetworkSigValueNoCarrAggrEnum.NS213, NetworkSigValueNoCarrAggrEnum.NS214, NetworkSigValueNoCarrAggrEnum.NS215, NetworkSigValueNoCarrAggrEnum.NS216, NetworkSigValueNoCarrAggrEnum.NS217, NetworkSigValueNoCarrAggrEnum.NS218, NetworkSigValueNoCarrAggrEnum.NS219, NetworkSigValueNoCarrAggrEnum.NS22, NetworkSigValueNoCarrAggrEnum.NS220, NetworkSigValueNoCarrAggrEnum.NS221, NetworkSigValueNoCarrAggrEnum.NS222, NetworkSigValueNoCarrAggrEnum.NS223, NetworkSigValueNoCarrAggrEnum.NS224, NetworkSigValueNoCarrAggrEnum.NS225, NetworkSigValueNoCarrAggrEnum.NS226, NetworkSigValueNoCarrAggrEnum.NS227, NetworkSigValueNoCarrAggrEnum.NS228, NetworkSigValueNoCarrAggrEnum.NS229, NetworkSigValueNoCarrAggrEnum.NS23, NetworkSigValueNoCarrAggrEnum.NS230, NetworkSigValueNoCarrAggrEnum.NS231, NetworkSigValueNoCarrAggrEnum.NS232, NetworkSigValueNoCarrAggrEnum.NS233, NetworkSigValueNoCarrAggrEnum.NS234, NetworkSigValueNoCarrAggrEnum.NS235, NetworkSigValueNoCarrAggrEnum.NS236, NetworkSigValueNoCarrAggrEnum.NS237, NetworkSigValueNoCarrAggrEnum.NS238, NetworkSigValueNoCarrAggrEnum.NS239, NetworkSigValueNoCarrAggrEnum.NS24, NetworkSigValueNoCarrAggrEnum.NS240, NetworkSigValueNoCarrAggrEnum.NS241, NetworkSigValueNoCarrAggrEnum.NS242, NetworkSigValueNoCarrAggrEnum.NS243, NetworkSigValueNoCarrAggrEnum.NS244, NetworkSigValueNoCarrAggrEnum.NS245, NetworkSigValueNoCarrAggrEnum.NS246, NetworkSigValueNoCarrAggrEnum.NS247, NetworkSigValueNoCarrAggrEnum.NS248, NetworkSigValueNoCarrAggrEnum.NS249, NetworkSigValueNoCarrAggrEnum.NS25, NetworkSigValueNoCarrAggrEnum.NS250, NetworkSigValueNoCarrAggrEnum.NS251, NetworkSigValueNoCarrAggrEnum.NS252, NetworkSigValueNoCarrAggrEnum.NS253, NetworkSigValueNoCarrAggrEnum.NS254, NetworkSigValueNoCarrAggrEnum.NS255, NetworkSigValueNoCarrAggrEnum.NS256, NetworkSigValueNoCarrAggrEnum.NS257, NetworkSigValueNoCarrAggrEnum.NS258, NetworkSigValueNoCarrAggrEnum.NS259, NetworkSigValueNoCarrAggrEnum.NS26, NetworkSigValueNoCarrAggrEnum.NS260, NetworkSigValueNoCarrAggrEnum.NS261, NetworkSigValueNoCarrAggrEnum.NS262, NetworkSigValueNoCarrAggrEnum.NS263, NetworkSigValueNoCarrAggrEnum.NS264, NetworkSigValueNoCarrAggrEnum.NS265, NetworkSigValueNoCarrAggrEnum.NS266, NetworkSigValueNoCarrAggrEnum.NS267, NetworkSigValueNoCarrAggrEnum.NS268, NetworkSigValueNoCarrAggrEnum.NS269, NetworkSigValueNoCarrAggrEnum.NS27, NetworkSigValueNoCarrAggrEnum.NS270, NetworkSigValueNoCarrAggrEnum.NS271, NetworkSigValueNoCarrAggrEnum.NS272, NetworkSigValueNoCarrAggrEnum.NS273, NetworkSigValueNoCarrAggrEnum.NS274, NetworkSigValueNoCarrAggrEnum.NS275, NetworkSigValueNoCarrAggrEnum.NS276, NetworkSigValueNoCarrAggrEnum.NS277, NetworkSigValueNoCarrAggrEnum.NS278, NetworkSigValueNoCarrAggrEnum.NS279, NetworkSigValueNoCarrAggrEnum.NS28, NetworkSigValueNoCarrAggrEnum.NS280, NetworkSigValueNoCarrAggrEnum.NS281, NetworkSigValueNoCarrAggrEnum.NS282, NetworkSigValueNoCarrAggrEnum.NS283, NetworkSigValueNoCarrAggrEnum.NS284, NetworkSigValueNoCarrAggrEnum.NS285, NetworkSigValueNoCarrAggrEnum.NS286, NetworkSigValueNoCarrAggrEnum.NS287, NetworkSigValueNoCarrAggrEnum.NS288, NetworkSigValueNoCarrAggrEnum.NS29, NetworkSigValueNoCarrAggrEnum.NS30, NetworkSigValueNoCarrAggrEnum.NS31, NetworkSigValueNoCarrAggrEnum.NS32, NetworkSigValueNoCarrAggrEnum.NS33, NetworkSigValueNoCarrAggrEnum.NS34, NetworkSigValueNoCarrAggrEnum.NS35, NetworkSigValueNoCarrAggrEnum.NS36, NetworkSigValueNoCarrAggrEnum.NS37, NetworkSigValueNoCarrAggrEnum.NS38, NetworkSigValueNoCarrAggrEnum.NS39, NetworkSigValueNoCarrAggrEnum.NS40, NetworkSigValueNoCarrAggrEnum.NS41, NetworkSigValueNoCarrAggrEnum.NS42, NetworkSigValueNoCarrAggrEnum.NS43, NetworkSigValueNoCarrAggrEnum.NS44, NetworkSigValueNoCarrAggrEnum.NS45, NetworkSigValueNoCarrAggrEnum.NS46, NetworkSigValueNoCarrAggrEnum.NS47, NetworkSigValueNoCarrAggrEnum.NS48, NetworkSigValueNoCarrAggrEnum.NS49, NetworkSigValueNoCarrAggrEnum.NS50, NetworkSigValueNoCarrAggrEnum.NS51, NetworkSigValueNoCarrAggrEnum.NS52, NetworkSigValueNoCarrAggrEnum.NS53, NetworkSigValueNoCarrAggrEnum.NS54, NetworkSigValueNoCarrAggrEnum.NS55, NetworkSigValueNoCarrAggrEnum.NS56, NetworkSigValueNoCarrAggrEnum.NS57, NetworkSigValueNoCarrAggrEnum.NS58, NetworkSigValueNoCarrAggrEnum.NS59, NetworkSigValueNoCarrAggrEnum.NS60, NetworkSigValueNoCarrAggrEnum.NS61, NetworkSigValueNoCarrAggrEnum.NS62, NetworkSigValueNoCarrAggrEnum.NS63, NetworkSigValueNoCarrAggrEnum.NS64, NetworkSigValueNoCarrAggrEnum.NS65, NetworkSigValueNoCarrAggrEnum.NS66, NetworkSigValueNoCarrAggrEnum.NS67, NetworkSigValueNoCarrAggrEnum.NS68, NetworkSigValueNoCarrAggrEnum.NS69, NetworkSigValueNoCarrAggrEnum.NS70, NetworkSigValueNoCarrAggrEnum.NS71, NetworkSigValueNoCarrAggrEnum.NS72, NetworkSigValueNoCarrAggrEnum.NS73, NetworkSigValueNoCarrAggrEnum.NS74, NetworkSigValueNoCarrAggrEnum.NS75, NetworkSigValueNoCarrAggrEnum.NS76, NetworkSigValueNoCarrAggrEnum.NS77, NetworkSigValueNoCarrAggrEnum.NS78, NetworkSigValueNoCarrAggrEnum.NS79, NetworkSigValueNoCarrAggrEnum.NS80, NetworkSigValueNoCarrAggrEnum.NS81, NetworkSigValueNoCarrAggrEnum.NS82, NetworkSigValueNoCarrAggrEnum.NS83, NetworkSigValueNoCarrAggrEnum.NS84, NetworkSigValueNoCarrAggrEnum.NS85, NetworkSigValueNoCarrAggrEnum.NS86, NetworkSigValueNoCarrAggrEnum.NS87, NetworkSigValueNoCarrAggrEnum.NS88, NetworkSigValueNoCarrAggrEnum.NS89, NetworkSigValueNoCarrAggrEnum.NS90, NetworkSigValueNoCarrAggrEnum.NS91, NetworkSigValueNoCarrAggrEnum.NS92, NetworkSigValueNoCarrAggrEnum.NS93, NetworkSigValueNoCarrAggrEnum.NS94, NetworkSigValueNoCarrAggrEnum.NS95, NetworkSigValueNoCarrAggrEnum.NS96, NetworkSigValueNoCarrAggrEnum.NS97, NetworkSigValueNoCarrAggrEnum.NS98, NetworkSigValueNoCarrAggrEnum.NS99 })
				{
					driver.Configure.MultiEval.Nsvalue.Value = x;
					NetworkSigValueNoCarrAggrEnum value = driver.Configure.MultiEval.Nsvalue.Value;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SRS:ENABle
				bool value = driver.Configure.MultiEval.Srs.Enable;
				driver.Configure.MultiEval.Srs.Enable = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EQUalizer
				bool value = driver.Configure.MultiEval.Modulation.Equalizer;
				driver.Configure.MultiEval.Modulation.Equalizer = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:MSCHeme
				foreach (ModSchemeEnum x in new ModSchemeEnum[] { ModSchemeEnum.AUTO, ModSchemeEnum.Q16, ModSchemeEnum.Q256, ModSchemeEnum.Q64, ModSchemeEnum.QPSK })
				{
					driver.Configure.MultiEval.Modulation.Mscheme = x;
					ModSchemeEnum value = driver.Configure.MultiEval.Modulation.Mscheme;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:LLOCation
				foreach (LocalOscLocationEnum x in new LocalOscLocationEnum[] { LocalOscLocationEnum.CCB, LocalOscLocationEnum.CN })
				{
					driver.Configure.MultiEval.Modulation.Llocation = x;
					LocalOscLocationEnum value = driver.Configure.MultiEval.Modulation.Llocation;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EWLength
				RsCmwLteMeas_Configure_MultiEval_Modulation_EwLength.Value_Data value = driver.Configure.MultiEval.Modulation.EwLength.Value;
				driver.Configure.MultiEval.Modulation.EwLength.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EWLength:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Modulation_EwLength_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Modulation.EwLength.ChannelBw.Get(ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Modulation.EwLength.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EWLength:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Modulation_EwLength_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Modulation_EwLength_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Modulation.EwLength.ChannelBw.Set(value, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Modulation.EwLength.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EEPeriods:PUCCh
				bool value = driver.Configure.MultiEval.Modulation.EePeriods.Pucch;
				driver.Configure.MultiEval.Modulation.EePeriods.Pucch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EEPeriods:PUSCh:LEADing
				foreach (LeadingExclPeriodEnum x in new LeadingExclPeriodEnum[] { LeadingExclPeriodEnum.MS25, LeadingExclPeriodEnum.OFF })
				{
					driver.Configure.MultiEval.Modulation.EePeriods.Pusch.Leading = x;
					LeadingExclPeriodEnum value = driver.Configure.MultiEval.Modulation.EePeriods.Pusch.Leading;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:EEPeriods:PUSCh:LAGGing
				foreach (LaggingExclPeriodEnum x in new LaggingExclPeriodEnum[] { LaggingExclPeriodEnum.MS05, LaggingExclPeriodEnum.MS25, LaggingExclPeriodEnum.OFF })
				{
					driver.Configure.MultiEval.Modulation.EePeriods.Pusch.Lagging = x;
					LaggingExclPeriodEnum value = driver.Configure.MultiEval.Modulation.EePeriods.Pusch.Lagging;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:MODulation:CAGGregation:LLOCation
				foreach (CarrAggrLocalOscLocationEnum x in new CarrAggrLocalOscLocationEnum[] { CarrAggrLocalOscLocationEnum.AUTO, CarrAggrLocalOscLocationEnum.CACB, CarrAggrLocalOscLocationEnum.CECC })
				{
					driver.Configure.MultiEval.Modulation.CarrierAggregation.Llocation = x;
					CarrAggrLocalOscLocationEnum value = driver.Configure.MultiEval.Modulation.CarrierAggregation.Llocation;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SPECtrum:SEMask:MFILter
				foreach (MeasFilterEnum x in new MeasFilterEnum[] { MeasFilterEnum.BANDpass, MeasFilterEnum.GAUSs })
				{
					driver.Configure.MultiEval.Spectrum.SeMask.Mfilter = x;
					MeasFilterEnum value = driver.Configure.MultiEval.Spectrum.SeMask.Mfilter;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SPECtrum:ACLR:ENABle
				RsCmwLteMeas_Configure_MultiEval_Spectrum_Aclr.Enable_Data value = driver.Configure.MultiEval.Spectrum.Aclr.Enable;
				driver.Configure.MultiEval.Spectrum.Aclr.Enable = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:AUTO
				bool value = driver.Configure.MultiEval.RbAllocation.Auto;
				driver.Configure.MultiEval.RbAllocation.Auto = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:NRB:PSCCh
				int value = driver.Configure.MultiEval.RbAllocation.Nrb.Pscch;
				driver.Configure.MultiEval.RbAllocation.Nrb.Pscch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:NRB:PSSCh
				int value = driver.Configure.MultiEval.RbAllocation.Nrb.Pssch;
				driver.Configure.MultiEval.RbAllocation.Nrb.Pssch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:NRB
				int value = driver.Configure.MultiEval.RbAllocation.Nrb.Value;
				driver.Configure.MultiEval.RbAllocation.Nrb.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:MCLuster
				bool value = driver.Configure.MultiEval.RbAllocation.Mcluster.Value;
				driver.Configure.MultiEval.RbAllocation.Mcluster.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:MCLuster:NRB<Number>
				int value = driver.Configure.MultiEval.RbAllocation.Mcluster.Nrb.Get(RBcountRepCap.Default);
				value = driver.Configure.MultiEval.RbAllocation.Mcluster.Nrb.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:MCLuster:NRB<Number>
				driver.Configure.MultiEval.RbAllocation.Mcluster.Nrb.Set(1, RBcountRepCap.Default);
				driver.Configure.MultiEval.RbAllocation.Mcluster.Nrb.Set(1);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:MCLuster:ORB<Number>
				int value = driver.Configure.MultiEval.RbAllocation.Mcluster.Orb.Get(RBoffsetRepCap.Default);
				value = driver.Configure.MultiEval.RbAllocation.Mcluster.Orb.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:MCLuster:ORB<Number>
				driver.Configure.MultiEval.RbAllocation.Mcluster.Orb.Set(1, RBoffsetRepCap.Default);
				driver.Configure.MultiEval.RbAllocation.Mcluster.Orb.Set(1);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:ORB:PSCCh
				int value = driver.Configure.MultiEval.RbAllocation.Orb.Pscch;
				driver.Configure.MultiEval.RbAllocation.Orb.Pscch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:ORB:PSSCh
				int value = driver.Configure.MultiEval.RbAllocation.Orb.Pssch;
				driver.Configure.MultiEval.RbAllocation.Orb.Pssch = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RBALlocation:ORB
				int value = driver.Configure.MultiEval.RbAllocation.Orb.Value;
				driver.Configure.MultiEval.RbAllocation.Orb.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:POWer:HDMode
				bool value = driver.Configure.MultiEval.Power.Hdmode;
				driver.Configure.MultiEval.Power.Hdmode = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:PDYNamics:TMASk
				foreach (TimeMaskEnum x in new TimeMaskEnum[] { TimeMaskEnum.GOO, TimeMaskEnum.PPSRs, TimeMaskEnum.SBLanking })
				{
					driver.Configure.MultiEval.Pdynamics.Tmask = x;
					TimeMaskEnum value = driver.Configure.MultiEval.Pdynamics.Tmask;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:PDYNamics:AEOPower:LEADing
				int value = driver.Configure.MultiEval.Pdynamics.AeoPower.Leading;
				driver.Configure.MultiEval.Pdynamics.AeoPower.Leading = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:PDYNamics:AEOPower:LAGGing
				int value = driver.Configure.MultiEval.Pdynamics.AeoPower.Lagging;
				driver.Configure.MultiEval.Pdynamics.AeoPower.Lagging = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCOunt:MODulation
				int value = driver.Configure.MultiEval.Scount.Modulation;
				driver.Configure.MultiEval.Scount.Modulation = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCOunt:POWer
				int value = driver.Configure.MultiEval.Scount.Power;
				driver.Configure.MultiEval.Scount.Power = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCOunt:SPECtrum:SEMask
				int value = driver.Configure.MultiEval.Scount.Spectrum.SeMask;
				driver.Configure.MultiEval.Scount.Spectrum.SeMask = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:SCOunt:SPECtrum:ACLR
				int value = driver.Configure.MultiEval.Scount.Spectrum.Aclr;
				driver.Configure.MultiEval.Scount.Spectrum.Aclr = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult[:ALL]
				RsCmwLteMeas_Configure_MultiEval_Result.All_Data value = driver.Configure.MultiEval.Result.All;
				driver.Configure.MultiEval.Result.All = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:MERRor
				bool value = driver.Configure.MultiEval.Result.Merror;
				driver.Configure.MultiEval.Result.Merror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:PERRor
				bool value = driver.Configure.MultiEval.Result.Perror;
				driver.Configure.MultiEval.Result.Perror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:IEMissions
				bool value = driver.Configure.MultiEval.Result.Iemissions;
				driver.Configure.MultiEval.Result.Iemissions = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:EVMC
				bool value = driver.Configure.MultiEval.Result.Evmc;
				driver.Configure.MultiEval.Result.Evmc = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:ESFLatness
				bool value = driver.Configure.MultiEval.Result.EsFlatness;
				driver.Configure.MultiEval.Result.EsFlatness = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:TXM
				bool value = driver.Configure.MultiEval.Result.Txm;
				driver.Configure.MultiEval.Result.Txm = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:IQ
				bool value = driver.Configure.MultiEval.Result.Iq;
				driver.Configure.MultiEval.Result.Iq = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:SEMask
				bool value = driver.Configure.MultiEval.Result.SeMask;
				driver.Configure.MultiEval.Result.SeMask = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:ACLR
				bool value = driver.Configure.MultiEval.Result.Aclr;
				driver.Configure.MultiEval.Result.Aclr = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:RBATable
				bool value = driver.Configure.MultiEval.Result.RbaTable;
				driver.Configure.MultiEval.Result.RbaTable = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:PMONitor
				bool value = driver.Configure.MultiEval.Result.Pmonitor;
				driver.Configure.MultiEval.Result.Pmonitor = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:PDYNamics
				bool value = driver.Configure.MultiEval.Result.Pdynamics;
				driver.Configure.MultiEval.Result.Pdynamics = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:BLER
				bool value = driver.Configure.MultiEval.Result.Bler;
				driver.Configure.MultiEval.Result.Bler = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:EVMagnitude:EVMSymbol
				RsCmwLteMeas_Configure_MultiEval_Result_EvMagnitude.EvmSymbol_Data value = driver.Configure.MultiEval.Result.EvMagnitude.EvmSymbol;
				driver.Configure.MultiEval.Result.EvMagnitude.EvmSymbol = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:RESult:EVMagnitude
				bool value = driver.Configure.MultiEval.Result.EvMagnitude.Value;
				driver.Configure.MultiEval.Result.EvMagnitude.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:EVMagnitude
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.EvMagnitude_Data value = driver.Configure.MultiEval.Limit.Qpsk.EvMagnitude;
				driver.Configure.MultiEval.Limit.Qpsk.EvMagnitude = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:MERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.Merror_Data value = driver.Configure.MultiEval.Limit.Qpsk.Merror;
				driver.Configure.MultiEval.Limit.Qpsk.Merror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:PERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.Perror_Data value = driver.Configure.MultiEval.Limit.Qpsk.Perror;
				driver.Configure.MultiEval.Limit.Qpsk.Perror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:FERRor
				double value = driver.Configure.MultiEval.Limit.Qpsk.FreqError;
				driver.Configure.MultiEval.Limit.Qpsk.FreqError = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.IqOffset_Data value = driver.Configure.MultiEval.Limit.Qpsk.IqOffset;
				driver.Configure.MultiEval.Limit.Qpsk.IqOffset = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:SFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.Sflatness_Data value = driver.Configure.MultiEval.Limit.Qpsk.Sflatness;
				driver.Configure.MultiEval.Limit.Qpsk.Sflatness = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:ESFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk.EsFlatness_Data value = driver.Configure.MultiEval.Limit.Qpsk.EsFlatness;
				driver.Configure.MultiEval.Limit.Qpsk.EsFlatness = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:IBE:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk_Ibe.IqOffset_Data value = driver.Configure.MultiEval.Limit.Qpsk.Ibe.IqOffset;
				driver.Configure.MultiEval.Limit.Qpsk.Ibe.IqOffset = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QPSK:IBE
				RsCmwLteMeas_Configure_MultiEval_Limit_Qpsk_Ibe.Value_Data value = driver.Configure.MultiEval.Limit.Qpsk.Ibe.Value;
				driver.Configure.MultiEval.Limit.Qpsk.Ibe.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:EVMagnitude
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EvMagnitude.EvMagnitude_Data value = driver.Configure.MultiEval.Limit.Qam.EvMagnitude.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.EvMagnitude.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:EVMagnitude
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EvMagnitude.EvMagnitude_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EvMagnitude.EvMagnitude_Data();
				driver.Configure.MultiEval.Limit.Qam.EvMagnitude.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.EvMagnitude.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:MERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Merror.Merror_Data value = driver.Configure.MultiEval.Limit.Qam.Merror.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.Merror.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:MERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Merror.Merror_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Merror.Merror_Data();
				driver.Configure.MultiEval.Limit.Qam.Merror.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.Merror.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:PERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Perror.Perror_Data value = driver.Configure.MultiEval.Limit.Qam.Perror.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.Perror.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:PERRor
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Perror.Perror_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Perror.Perror_Data();
				driver.Configure.MultiEval.Limit.Qam.Perror.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.Perror.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:FERRor
				double value = driver.Configure.MultiEval.Limit.Qam.FreqError.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.FreqError.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:FERRor
				driver.Configure.MultiEval.Limit.Qam.FreqError.Set(1.0, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.FreqError.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_IqOffset.IqOffset_Data value = driver.Configure.MultiEval.Limit.Qam.IqOffset.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.IqOffset.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_IqOffset.IqOffset_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_IqOffset.IqOffset_Data();
				driver.Configure.MultiEval.Limit.Qam.IqOffset.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.IqOffset.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IBE
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe.Ibe_Data value = driver.Configure.MultiEval.Limit.Qam.Ibe.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.Ibe.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IBE
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe.Ibe_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe.Ibe_Data();
				driver.Configure.MultiEval.Limit.Qam.Ibe.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.Ibe.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IBE:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe_IqOffset.IqOffset_Data value = driver.Configure.MultiEval.Limit.Qam.Ibe.IqOffset.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.Ibe.IqOffset.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:IBE:IQOFfset
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe_IqOffset.IqOffset_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Ibe_IqOffset.IqOffset_Data();
				driver.Configure.MultiEval.Limit.Qam.Ibe.IqOffset.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.Ibe.IqOffset.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:SFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Sflatness.Sflatness_Data value = driver.Configure.MultiEval.Limit.Qam.Sflatness.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.Sflatness.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:SFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Sflatness.Sflatness_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_Sflatness.Sflatness_Data();
				driver.Configure.MultiEval.Limit.Qam.Sflatness.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.Sflatness.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:ESFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EsFlatness.EsFlatness_Data value = driver.Configure.MultiEval.Limit.Qam.EsFlatness.Get(QAMmodOrderRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Qam.EsFlatness.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:QAM<ModOrder>:ESFLatness
				RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EsFlatness.EsFlatness_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Qam_EsFlatness.EsFlatness_Data();
				driver.Configure.MultiEval.Limit.Qam.EsFlatness.Set(value, QAMmodOrderRepCap.Default);
				driver.Configure.MultiEval.Limit.Qam.EsFlatness.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CBANdwidth<Band>
				double value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.ChannelBw.Get(ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CBANdwidth<Band>
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.ChannelBw.Set(1.0, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.ChannelBw.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CAGGregation:OCOMbination
				double value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.Ocombination;
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.Ocombination = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				double value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get(FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(1.0, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				double value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get(FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:OBWLimit:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(1.0, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.ObwLimit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.ChannelBw.Get(LimitRepCap.Default, ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.ChannelBw.Set(value, LimitRepCap.Default, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Get(LimitRepCap.Default, TableRepCap.Default, ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Set(value, LimitRepCap.Default, TableRepCap.Default, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CBANdwidth<Band>:SIDelink
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw_Sidelink.Sidelink_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Sidelink.Get(LimitRepCap.Default, TableRepCap.Default, ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Sidelink.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CBANdwidth<Band>:SIDelink
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw_Sidelink.Sidelink_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_ChannelBw_Sidelink.Sidelink_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Sidelink.Set(value, LimitRepCap.Default, TableRepCap.Default, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.ChannelBw.Sidelink.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get(LimitRepCap.Default, TableRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value, LimitRepCap.Default, TableRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_Ocombination.Ocombination_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.Ocombination.Get(LimitRepCap.Default, TableRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.Ocombination.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:ADDitional{tableCmdVal}:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_Ocombination.Ocombination_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_Additional_CarrierAggregation_Ocombination.Ocombination_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.Ocombination.Set(value, LimitRepCap.Default, TableRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.Additional.CarrierAggregation.Ocombination.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get(LimitRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value, LimitRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get(LimitRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value, LimitRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_Ocombination.Ocombination_Data value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.Ocombination.Get(LimitRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.Ocombination.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:LIMit<nr>:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_Ocombination.Ocombination_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_SeMask_Limit_CarrierAggregation_Ocombination.Ocombination_Data();
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.Ocombination.Set(value, LimitRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.Limit.CarrierAggregation.Ocombination.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:ATTolerance<EUTRAband>
				double value = driver.Configure.MultiEval.Limit.SeMask.AtTolerance.Get(EutraBandRepCap.Default);
				value = driver.Configure.MultiEval.Limit.SeMask.AtTolerance.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:SEMask:ATTolerance<EUTRAband>
				driver.Configure.MultiEval.Limit.SeMask.AtTolerance.Set(1.0, EutraBandRepCap.Default);
				driver.Configure.MultiEval.Limit.SeMask.AtTolerance.Set(1.0);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Limit.Aclr.Utra.ChannelBw.Get(UtraAdjChannelRepCap.Default, ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Utra.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Limit.Aclr.Utra.ChannelBw.Set(value, UtraAdjChannelRepCap.Default, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Utra.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get(UtraAdjChannelRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data();
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value, UtraAdjChannelRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get(UtraAdjChannelRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data();
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value, UtraAdjChannelRepCap.Default, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_Ocombination.Ocombination_Data value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.Ocombination.Get(UtraAdjChannelRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.Ocombination.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:UTRA<nr>:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_Ocombination.Ocombination_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Utra_CarrierAggregation_Ocombination.Ocombination_Data();
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.Ocombination.Set(value, UtraAdjChannelRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Utra.CarrierAggregation.Ocombination.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Limit.Aclr.Eutra.ChannelBw.Get(ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Eutra.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Limit.Aclr.Eutra.ChannelBw.Set(value, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Eutra.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CAGGregation:OCOMbination
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation.Ocombination_Data value = driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.Ocombination;
				driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.Ocombination = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get(FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd.ChannelBw2nd_Data();
				driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get(FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:ACLR:EUTRa:CAGGregation:CBANdwidth<Band1>:CBANdwidth<Band2>:CBANdwidth<Band3>
				RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Aclr_Eutra_CarrierAggregation_ChannelBw1st_ChannelBw2nd_ChannelBw3rd.ChannelBw3rd_Data();
				driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value, FirstChannelBwRepCap.Default, SecondChannelBwRepCap.Default, ThirdChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Aclr.Eutra.CarrierAggregation.ChannelBw1st.ChannelBw2nd.ChannelBw3rd.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:PDYNamics:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Pdynamics_ChannelBw.ChannelBw_Data value = driver.Configure.MultiEval.Limit.Pdynamics.ChannelBw.Get(ChannelBwRepCap.Default);
				value = driver.Configure.MultiEval.Limit.Pdynamics.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:LIMit:PDYNamics:CBANdwidth<Band>
				RsCmwLteMeas_Configure_MultiEval_Limit_Pdynamics_ChannelBw.ChannelBw_Data value = new RsCmwLteMeas_Configure_MultiEval_Limit_Pdynamics_ChannelBw.ChannelBw_Data();
				driver.Configure.MultiEval.Limit.Pdynamics.ChannelBw.Set(value, ChannelBwRepCap.Default);
				driver.Configure.MultiEval.Limit.Pdynamics.ChannelBw.Set(value);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:MEValuation:BLER:SFRames
				RsCmwLteMeas_Configure_MultiEval_Bler.Sframes_Data value = driver.Configure.MultiEval.Bler.Sframes;
				driver.Configure.MultiEval.Bler.Sframes = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>[:PCC]:CBANdwidth
				foreach (ChannelBandwidthEnum x in new ChannelBandwidthEnum[] { ChannelBandwidthEnum.B014, ChannelBandwidthEnum.B030, ChannelBandwidthEnum.B050, ChannelBandwidthEnum.B100, ChannelBandwidthEnum.B150, ChannelBandwidthEnum.B200 })
				{
					driver.Configure.Pcc.ChannelBw = x;
					ChannelBandwidthEnum value = driver.Configure.Pcc.ChannelBw;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CC<Nr>:CBANdwidth
				ChannelBandwidthEnum value = driver.Configure.Cc.ChannelBw.Get(CarrierComponentRepCap.Default);
				value = driver.Configure.Cc.ChannelBw.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:CC<Nr>:CBANdwidth
				foreach (ChannelBandwidthEnum x in new ChannelBandwidthEnum[] { ChannelBandwidthEnum.B014, ChannelBandwidthEnum.B030, ChannelBandwidthEnum.B050, ChannelBandwidthEnum.B100, ChannelBandwidthEnum.B150, ChannelBandwidthEnum.B200 })
				{
					driver.Configure.Cc.ChannelBw.Set(x);
					driver.Configure.Cc.ChannelBw.Set(x, CarrierComponentRepCap.Default);
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:TOUT
				double value = driver.Configure.Prach.Timeout;
				driver.Configure.Prach.Timeout = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Prach.Repetition = x;
					RepeatEnum value = driver.Configure.Prach.Repetition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.Prach.Scondition = x;
					StopConditionEnum value = driver.Configure.Prach.Scondition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MOEXception
				bool value = driver.Configure.Prach.MoException;
				driver.Configure.Prach.MoException = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:PCINdex
				int value = driver.Configure.Prach.PcIndex;
				driver.Configure.Prach.PcIndex = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:NOPReambles
				int value = driver.Configure.Prach.NoPreambles;
				driver.Configure.Prach.NoPreambles = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:POPReambles
				foreach (PeriodPreambleEnum x in new PeriodPreambleEnum[] { PeriodPreambleEnum.MS05, PeriodPreambleEnum.MS10, PeriodPreambleEnum.MS20 })
				{
					driver.Configure.Prach.PoPreambles = x;
					PeriodPreambleEnum value = driver.Configure.Prach.PoPreambles;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:PFOFfset:AUTO
				bool value = driver.Configure.Prach.PfOffset.Auto;
				driver.Configure.Prach.PfOffset.Auto = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:PFOFfset
				int value = driver.Configure.Prach.PfOffset.Value;
				driver.Configure.Prach.PfOffset.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:LRSindex
				int value = driver.Configure.Prach.Modulation.LrsIndex;
				driver.Configure.Prach.Modulation.LrsIndex = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:ZCZConfig
				int value = driver.Configure.Prach.Modulation.ZczConfig;
				driver.Configure.Prach.Modulation.ZczConfig = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:EWPosition
				foreach (LowHighEnum x in new LowHighEnum[] { LowHighEnum.HIGH, LowHighEnum.LOW })
				{
					driver.Configure.Prach.Modulation.EwPosition = x;
					LowHighEnum value = driver.Configure.Prach.Modulation.EwPosition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:SINDex:AUTO
				bool value = driver.Configure.Prach.Modulation.Sindex.Auto;
				driver.Configure.Prach.Modulation.Sindex.Auto = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:SINDex
				int value = driver.Configure.Prach.Modulation.Sindex.Value;
				driver.Configure.Prach.Modulation.Sindex.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:EWLength
				List<int> value = driver.Configure.Prach.Modulation.EwLength.Value;
				driver.Configure.Prach.Modulation.EwLength.Value = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:EWLength:PFORmat{preambleFormatCmdVal}
				int value = driver.Configure.Prach.Modulation.EwLength.Pformat.Get(PreambleFormatRepCap.Default);
				value = driver.Configure.Prach.Modulation.EwLength.Pformat.Get();
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:MODulation:EWLength:PFORmat{preambleFormatCmdVal}
				driver.Configure.Prach.Modulation.EwLength.Pformat.Set(1, PreambleFormatRepCap.Default);
				driver.Configure.Prach.Modulation.EwLength.Pformat.Set(1);
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:POWer:HDMode
				bool value = driver.Configure.Prach.Power.Hdmode;
				driver.Configure.Prach.Power.Hdmode = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:SCOunt:MODulation
				int value = driver.Configure.Prach.Scount.Modulation;
				driver.Configure.Prach.Scount.Modulation = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:SCOunt:PDYNamics
				int value = driver.Configure.Prach.Scount.Pdynamics;
				driver.Configure.Prach.Scount.Pdynamics = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult[:ALL]
				RsCmwLteMeas_Configure_Prach_Result.All_Data value = driver.Configure.Prach.Result.All;
				driver.Configure.Prach.Result.All = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:EVMagnitude
				bool value = driver.Configure.Prach.Result.EvMagnitude;
				driver.Configure.Prach.Result.EvMagnitude = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:EVPReamble
				bool value = driver.Configure.Prach.Result.EvPreamble;
				driver.Configure.Prach.Result.EvPreamble = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:MERRor
				bool value = driver.Configure.Prach.Result.Merror;
				driver.Configure.Prach.Result.Merror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:PERRor
				bool value = driver.Configure.Prach.Result.Perror;
				driver.Configure.Prach.Result.Perror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:IQ
				bool value = driver.Configure.Prach.Result.Iq;
				driver.Configure.Prach.Result.Iq = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:PDYNamics
				bool value = driver.Configure.Prach.Result.Pdynamics;
				driver.Configure.Prach.Result.Pdynamics = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:PVPReamble
				bool value = driver.Configure.Prach.Result.PvPreamble;
				driver.Configure.Prach.Result.PvPreamble = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:RESult:TXM
				bool value = driver.Configure.Prach.Result.Txm;
				driver.Configure.Prach.Result.Txm = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:LIMit:EVMagnitude
				RsCmwLteMeas_Configure_Prach_Limit.EvMagnitude_Data value = driver.Configure.Prach.Limit.EvMagnitude;
				driver.Configure.Prach.Limit.EvMagnitude = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:LIMit:MERRor
				RsCmwLteMeas_Configure_Prach_Limit.Merror_Data value = driver.Configure.Prach.Limit.Merror;
				driver.Configure.Prach.Limit.Merror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:LIMit:PERRor
				RsCmwLteMeas_Configure_Prach_Limit.Perror_Data value = driver.Configure.Prach.Limit.Perror;
				driver.Configure.Prach.Limit.Perror = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:LIMit:FERRor
				double value = driver.Configure.Prach.Limit.FreqError;
				driver.Configure.Prach.Limit.FreqError = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:PRACh:LIMit:PDYNamics
				RsCmwLteMeas_Configure_Prach_Limit.Pdynamics_Data value = driver.Configure.Prach.Limit.Pdynamics;
				driver.Configure.Prach.Limit.Pdynamics = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:TOUT
				double value = driver.Configure.Srs.Timeout;
				driver.Configure.Srs.Timeout = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Srs.Repetition = x;
					RepeatEnum value = driver.Configure.Srs.Repetition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.Srs.Scondition = x;
					StopConditionEnum value = driver.Configure.Srs.Scondition;
				}
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:MOEXception
				bool value = driver.Configure.Srs.MoException;
				driver.Configure.Srs.MoException = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:HDMode
				bool value = driver.Configure.Srs.Hdmode;
				driver.Configure.Srs.Hdmode = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:SCOunt:PDYNamics
				int value = driver.Configure.Srs.Scount.Pdynamics;
				driver.Configure.Srs.Scount.Pdynamics = value;
			}
			{	// CONFigure:LTE:MEASurement<Instance>:SRS:LIMit:PDYNamics
				RsCmwLteMeas_Configure_Srs_Limit.Pdynamics_Data value = driver.Configure.Srs.Limit.Pdynamics;
				driver.Configure.Srs.Limit.Pdynamics = value;
			}
			{	// SENSe:LTE:MEASurement<Instance>:CAGGregation:FSHWare
				bool value = driver.Sense.CarrierAggregation.Fshware;
			}
			{	// SENSe:LTE:MEASurement<Instance>:MEValuation:SPECtrum:SEMask:RBW:USED
				RsCmwLteMeas_Sense_MultiEval_Spectrum_SeMask_Rbw.Used_Data value = driver.Sense.MultiEval.Spectrum.SeMask.Rbw.Used;
			}
			{	// INITiate:LTE:MEASurement<Instance>:MEValuation
				driver.MultiEval.Initiate();
				driver.MultiEval.InitiateAndWait();
			}
			{	// STOP:LTE:MEASurement<Instance>:MEValuation
				driver.MultiEval.Stop();
				driver.MultiEval.StopAndWait();
			}
			{	// ABORt:LTE:MEASurement<Instance>:MEValuation
				driver.MultiEval.Abort();
				driver.MultiEval.AbortAndWait();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:STATe
				ResourceStateEnum value = driver.MultiEval.State.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:STATe:ALL
				RsCmwLteMeas_MultiEval_State_All.Fetch_Data value = driver.MultiEval.State.All.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:RBATable:CC<Nr>
				RsCmwLteMeas_MultiEval_Trace_RbaTable_Cc.ResultData value = driver.MultiEval.Trace.RbaTable.Cc.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.RbaTable.Cc.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:RBATable:CC<Nr>
				RsCmwLteMeas_MultiEval_Trace_RbaTable_Cc.ResultData value = driver.MultiEval.Trace.RbaTable.Cc.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.RbaTable.Cc.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:IQ:LOW
				RsCmwLteMeas_MultiEval_Trace_Iq_Low.Fetch_Data value = driver.MultiEval.Trace.Iq.Low.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:IQ:HIGH
				RsCmwLteMeas_MultiEval_Trace_Iq_High.Fetch_Data value = driver.MultiEval.Trace.Iq.High.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:IEMissions:CC<Nr>
				List<double> value = driver.MultiEval.Trace.Iemissions.Cc.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.Iemissions.Cc.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:IEMissions:CC<Nr>
				List<double> value = driver.MultiEval.Trace.Iemissions.Cc.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.Iemissions.Cc.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMC
				List<double> value = driver.MultiEval.Trace.Evmc.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMC
				List<double> value = driver.MultiEval.Trace.Evmc.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:CURRent
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:CURRent
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:AVERage
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:AVERage
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:EVMSymbol:MAXimum
				List<double> value = driver.MultiEval.Trace.EvmSymbol.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:ESFLatness
				List<double> value = driver.MultiEval.Trace.EsFlatness.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:ESFLatness
				List<double> value = driver.MultiEval.Trace.EsFlatness.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:ESFLatness:PHASe
				List<double> value = driver.MultiEval.Trace.EsFlatness.Phase.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:ESFLatness:PHASe
				List<double> value = driver.MultiEval.Trace.EsFlatness.Phase.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:CURRent
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Current.Read(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Current.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:CURRent
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Current.Fetch(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Current.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:AVERage
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Average.Read(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Average.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:AVERage
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Average.Fetch(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Average.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:MAXimum
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Maximum.Read(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Maximum.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:SEMask:RBW<kHz>:MAXimum
				List<double> value = driver.MultiEval.Trace.SeMask.Rbw.Maximum.Fetch(RBWkHzRepCap.Default);
				value = driver.MultiEval.Trace.SeMask.Rbw.Maximum.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:ACLR:CURRent
				RsCmwLteMeas_MultiEval_Trace_Aclr_Current.ResultData value = driver.MultiEval.Trace.Aclr.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:ACLR:CURRent
				RsCmwLteMeas_MultiEval_Trace_Aclr_Current.ResultData value = driver.MultiEval.Trace.Aclr.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:ACLR:AVERage
				RsCmwLteMeas_MultiEval_Trace_Aclr_Average.ResultData value = driver.MultiEval.Trace.Aclr.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:ACLR:AVERage
				RsCmwLteMeas_MultiEval_Trace_Aclr_Average.ResultData value = driver.MultiEval.Trace.Aclr.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:PMONitor:CC<Nr>
				List<double> value = driver.MultiEval.Trace.Pmonitor.Cc.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.Pmonitor.Cc.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:PMONitor:CC<Nr>
				List<double> value = driver.MultiEval.Trace.Pmonitor.Cc.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Trace.Pmonitor.Cc.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:CURRent
				List<double> value = driver.MultiEval.Trace.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:CURRent
				List<double> value = driver.MultiEval.Trace.Pdynamics.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:AVERage
				List<double> value = driver.MultiEval.Trace.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:AVERage
				List<double> value = driver.MultiEval.Trace.Pdynamics.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:TRACe:PDYNamics:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdynamics.Maximum.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:VFTHroughput
				double value = driver.MultiEval.VfThroughput.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:CURRent
				RsCmwLteMeas_MultiEval_EvMagnitude_Current.ResultData value = driver.MultiEval.EvMagnitude.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:CURRent
				RsCmwLteMeas_MultiEval_EvMagnitude_Current.ResultData value = driver.MultiEval.EvMagnitude.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:AVERage
				RsCmwLteMeas_MultiEval_EvMagnitude_Average.ResultData value = driver.MultiEval.EvMagnitude.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:AVERage
				RsCmwLteMeas_MultiEval_EvMagnitude_Average.ResultData value = driver.MultiEval.EvMagnitude.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:MAXimum
				RsCmwLteMeas_MultiEval_EvMagnitude_Maximum.ResultData value = driver.MultiEval.EvMagnitude.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:MAXimum
				RsCmwLteMeas_MultiEval_EvMagnitude_Maximum.ResultData value = driver.MultiEval.EvMagnitude.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:CURRent
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Current.ResultData value = driver.MultiEval.EvMagnitude.Peak.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:CURRent
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Current.ResultData value = driver.MultiEval.EvMagnitude.Peak.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:AVERage
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Average.ResultData value = driver.MultiEval.EvMagnitude.Peak.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:AVERage
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Average.ResultData value = driver.MultiEval.EvMagnitude.Peak.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:MAXimum
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Maximum.ResultData value = driver.MultiEval.EvMagnitude.Peak.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMagnitude:PEAK:MAXimum
				RsCmwLteMeas_MultiEval_EvMagnitude_Peak_Maximum.ResultData value = driver.MultiEval.EvMagnitude.Peak.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MERRor:CURRent
				RsCmwLteMeas_MultiEval_Merror_Current.ResultData value = driver.MultiEval.Merror.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MERRor:CURRent
				RsCmwLteMeas_MultiEval_Merror_Current.ResultData value = driver.MultiEval.Merror.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MERRor:AVERage
				RsCmwLteMeas_MultiEval_Merror_Average.ResultData value = driver.MultiEval.Merror.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MERRor:AVERage
				RsCmwLteMeas_MultiEval_Merror_Average.ResultData value = driver.MultiEval.Merror.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MERRor:MAXimum
				RsCmwLteMeas_MultiEval_Merror_Maximum.ResultData value = driver.MultiEval.Merror.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MERRor:MAXimum
				RsCmwLteMeas_MultiEval_Merror_Maximum.ResultData value = driver.MultiEval.Merror.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PERRor:CURRent
				RsCmwLteMeas_MultiEval_Perror_Current.ResultData value = driver.MultiEval.Perror.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PERRor:CURRent
				RsCmwLteMeas_MultiEval_Perror_Current.ResultData value = driver.MultiEval.Perror.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PERRor:AVERage
				RsCmwLteMeas_MultiEval_Perror_Average.ResultData value = driver.MultiEval.Perror.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PERRor:AVERage
				RsCmwLteMeas_MultiEval_Perror_Average.ResultData value = driver.MultiEval.Perror.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PERRor:MAXimum
				RsCmwLteMeas_MultiEval_Perror_Maximum.ResultData value = driver.MultiEval.Perror.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PERRor:MAXimum
				RsCmwLteMeas_MultiEval_Perror_Maximum.ResultData value = driver.MultiEval.Perror.Maximum.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:CURRent
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_Current.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.Current.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:CURRent:RBINdex
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_Current_RbIndex.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.Current.RbIndex.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.Current.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:AVERage
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_Average.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.Average.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:EXTReme
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_Extreme.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.Extreme.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.Extreme.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:EXTReme:RBINdex
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_Extreme_RbIndex.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.Extreme.RbIndex.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.Extreme.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:IEMission:CC<Nr>:MARGin:SDEViation
				RsCmwLteMeas_MultiEval_InbandEmission_Cc_Margin_StandardDev.Fetch_Data value = driver.MultiEval.InbandEmission.Cc.Margin.StandardDev.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.InbandEmission.Cc.Margin.StandardDev.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ESFLatness:CURRent
				RsCmwLteMeas_MultiEval_EsFlatness_Current.ResultData value = driver.MultiEval.EsFlatness.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ESFLatness:CURRent
				RsCmwLteMeas_MultiEval_EsFlatness_Current.ResultData value = driver.MultiEval.EsFlatness.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:ESFLatness:CURRent
				RsCmwLteMeas_MultiEval_EsFlatness_Current.Calculate_Data value = driver.MultiEval.EsFlatness.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ESFLatness:CURRent:SCINdex
				RsCmwLteMeas_MultiEval_EsFlatness_Current_ScIndex.Fetch_Data value = driver.MultiEval.EsFlatness.Current.ScIndex.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ESFLatness:AVERage
				RsCmwLteMeas_MultiEval_EsFlatness_Average.ResultData value = driver.MultiEval.EsFlatness.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ESFLatness:AVERage
				RsCmwLteMeas_MultiEval_EsFlatness_Average.ResultData value = driver.MultiEval.EsFlatness.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:ESFLatness:AVERage
				RsCmwLteMeas_MultiEval_EsFlatness_Average.Calculate_Data value = driver.MultiEval.EsFlatness.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ESFLatness:EXTReme
				RsCmwLteMeas_MultiEval_EsFlatness_Extreme.ResultData value = driver.MultiEval.EsFlatness.Extreme.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ESFLatness:EXTReme
				RsCmwLteMeas_MultiEval_EsFlatness_Extreme.ResultData value = driver.MultiEval.EsFlatness.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:ESFLatness:EXTReme
				RsCmwLteMeas_MultiEval_EsFlatness_Extreme.Calculate_Data value = driver.MultiEval.EsFlatness.Extreme.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ESFLatness:SDEViation
				RsCmwLteMeas_MultiEval_EsFlatness_StandardDev.ResultData value = driver.MultiEval.EsFlatness.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ESFLatness:SDEViation
				RsCmwLteMeas_MultiEval_EsFlatness_StandardDev.ResultData value = driver.MultiEval.EsFlatness.StandardDev.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:CURRent
				double value = driver.MultiEval.Evmc.Peak.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:CURRent
				double value = driver.MultiEval.Evmc.Peak.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:AVERage
				double value = driver.MultiEval.Evmc.Peak.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:AVERage
				double value = driver.MultiEval.Evmc.Peak.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:MAXimum
				double value = driver.MultiEval.Evmc.Peak.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:MAXimum
				double value = driver.MultiEval.Evmc.Peak.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:SDEViation
				double value = driver.MultiEval.Evmc.Peak.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:EVMC:PEAK:SDEViation
				double value = driver.MultiEval.Evmc.Peak.StandardDev.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwLteMeas_MultiEval_Modulation_Current.ResultData value = driver.MultiEval.Modulation.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwLteMeas_MultiEval_Modulation_Current.ResultData value = driver.MultiEval.Modulation.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:MODulation:CURRent
				RsCmwLteMeas_MultiEval_Modulation_Current.Calculate_Data value = driver.MultiEval.Modulation.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwLteMeas_MultiEval_Modulation_Average.ResultData value = driver.MultiEval.Modulation.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwLteMeas_MultiEval_Modulation_Average.ResultData value = driver.MultiEval.Modulation.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:MODulation:AVERage
				RsCmwLteMeas_MultiEval_Modulation_Average.Calculate_Data value = driver.MultiEval.Modulation.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MODulation:EXTReme
				RsCmwLteMeas_MultiEval_Modulation_Extreme.ResultData value = driver.MultiEval.Modulation.Extreme.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:EXTReme
				RsCmwLteMeas_MultiEval_Modulation_Extreme.ResultData value = driver.MultiEval.Modulation.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:MODulation:EXTReme
				RsCmwLteMeas_MultiEval_Modulation_Extreme.Calculate_Data value = driver.MultiEval.Modulation.Extreme.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:MODulation:SDEViation
				RsCmwLteMeas_MultiEval_Modulation_StandardDev.ResultData value = driver.MultiEval.Modulation.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:SDEViation
				RsCmwLteMeas_MultiEval_Modulation_StandardDev.ResultData value = driver.MultiEval.Modulation.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:DMODulation
				ModulationEnum value = driver.MultiEval.Modulation.Dmodulation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:DCHType
				UplinkChannelTypeEnum value = driver.MultiEval.Modulation.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:SCHType
				SidelinkChannelTypeEnum value = driver.MultiEval.Modulation.Schtype.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:MODulation:DALLocation
				RsCmwLteMeas_MultiEval_Modulation_Dallocation.Fetch_Data value = driver.MultiEval.Modulation.Dallocation.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:SEMask:CURRent
				RsCmwLteMeas_MultiEval_SeMask_Current.ResultData value = driver.MultiEval.SeMask.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:CURRent
				RsCmwLteMeas_MultiEval_SeMask_Current.ResultData value = driver.MultiEval.SeMask.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:SEMask:CURRent
				RsCmwLteMeas_MultiEval_SeMask_Current.Calculate_Data value = driver.MultiEval.SeMask.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:SEMask:AVERage
				RsCmwLteMeas_MultiEval_SeMask_Average.ResultData value = driver.MultiEval.SeMask.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:AVERage
				RsCmwLteMeas_MultiEval_SeMask_Average.ResultData value = driver.MultiEval.SeMask.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:SEMask:AVERage
				RsCmwLteMeas_MultiEval_SeMask_Average.Calculate_Data value = driver.MultiEval.SeMask.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:SEMask:EXTReme
				RsCmwLteMeas_MultiEval_SeMask_Extreme.ResultData value = driver.MultiEval.SeMask.Extreme.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:EXTReme
				RsCmwLteMeas_MultiEval_SeMask_Extreme.ResultData value = driver.MultiEval.SeMask.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:SEMask:EXTReme
				RsCmwLteMeas_MultiEval_SeMask_Extreme.Calculate_Data value = driver.MultiEval.SeMask.Extreme.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:SEMask:SDEViation
				RsCmwLteMeas_MultiEval_SeMask_StandardDev.ResultData value = driver.MultiEval.SeMask.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:SDEViation
				RsCmwLteMeas_MultiEval_SeMask_StandardDev.ResultData value = driver.MultiEval.SeMask.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:ALL
				RsCmwLteMeas_MultiEval_SeMask_Margin_All.Fetch_Data value = driver.MultiEval.SeMask.Margin.All.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:CURRent:NEGativ
				RsCmwLteMeas_MultiEval_SeMask_Margin_Current_Negativ.Fetch_Data value = driver.MultiEval.SeMask.Margin.Current.Negativ.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:CURRent:POSitiv
				RsCmwLteMeas_MultiEval_SeMask_Margin_Current_Positiv.Fetch_Data value = driver.MultiEval.SeMask.Margin.Current.Positiv.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:AVERage:NEGativ
				RsCmwLteMeas_MultiEval_SeMask_Margin_Average_Negativ.Fetch_Data value = driver.MultiEval.SeMask.Margin.Average.Negativ.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:AVERage:POSitiv
				RsCmwLteMeas_MultiEval_SeMask_Margin_Average_Positiv.Fetch_Data value = driver.MultiEval.SeMask.Margin.Average.Positiv.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:MINimum:NEGativ
				RsCmwLteMeas_MultiEval_SeMask_Margin_Minimum_Negativ.Fetch_Data value = driver.MultiEval.SeMask.Margin.Minimum.Negativ.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:MARGin:MINimum:POSitiv
				RsCmwLteMeas_MultiEval_SeMask_Margin_Minimum_Positiv.Fetch_Data value = driver.MultiEval.SeMask.Margin.Minimum.Positiv.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:DCHType
				UplinkChannelTypeEnum value = driver.MultiEval.SeMask.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:SEMask:DALLocation
				RsCmwLteMeas_MultiEval_SeMask_Dallocation.Fetch_Data value = driver.MultiEval.SeMask.Dallocation.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ACLR:CURRent
				RsCmwLteMeas_MultiEval_Aclr_Current.ResultData value = driver.MultiEval.Aclr.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ACLR:CURRent
				RsCmwLteMeas_MultiEval_Aclr_Current.ResultData value = driver.MultiEval.Aclr.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:ACLR:CURRent
				RsCmwLteMeas_MultiEval_Aclr_Current.Calculate_Data value = driver.MultiEval.Aclr.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:ACLR:AVERage
				RsCmwLteMeas_MultiEval_Aclr_Average.ResultData value = driver.MultiEval.Aclr.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ACLR:AVERage
				RsCmwLteMeas_MultiEval_Aclr_Average.ResultData value = driver.MultiEval.Aclr.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:ACLR:AVERage
				RsCmwLteMeas_MultiEval_Aclr_Average.Calculate_Data value = driver.MultiEval.Aclr.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ACLR:DCHType
				UplinkChannelTypeEnum value = driver.MultiEval.Aclr.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:ACLR:DALLocation
				RsCmwLteMeas_MultiEval_Aclr_Dallocation.Fetch_Data value = driver.MultiEval.Aclr.Dallocation.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PDYNamics:CURRent
				RsCmwLteMeas_MultiEval_Pdynamics_Current.ResultData value = driver.MultiEval.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PDYNamics:CURRent
				RsCmwLteMeas_MultiEval_Pdynamics_Current.ResultData value = driver.MultiEval.Pdynamics.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PDYNamics:CURRent
				RsCmwLteMeas_MultiEval_Pdynamics_Current.Calculate_Data value = driver.MultiEval.Pdynamics.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PDYNamics:AVERage
				RsCmwLteMeas_MultiEval_Pdynamics_Average.ResultData value = driver.MultiEval.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PDYNamics:AVERage
				RsCmwLteMeas_MultiEval_Pdynamics_Average.ResultData value = driver.MultiEval.Pdynamics.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PDYNamics:AVERage
				RsCmwLteMeas_MultiEval_Pdynamics_Average.Calculate_Data value = driver.MultiEval.Pdynamics.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MAXimum
				RsCmwLteMeas_MultiEval_Pdynamics_Maximum.ResultData value = driver.MultiEval.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MAXimum
				RsCmwLteMeas_MultiEval_Pdynamics_Maximum.ResultData value = driver.MultiEval.Pdynamics.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MAXimum
				RsCmwLteMeas_MultiEval_Pdynamics_Maximum.Calculate_Data value = driver.MultiEval.Pdynamics.Maximum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MINimum
				RsCmwLteMeas_MultiEval_Pdynamics_Minimum.ResultData value = driver.MultiEval.Pdynamics.Minimum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MINimum
				RsCmwLteMeas_MultiEval_Pdynamics_Minimum.ResultData value = driver.MultiEval.Pdynamics.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PDYNamics:MINimum
				RsCmwLteMeas_MultiEval_Pdynamics_Minimum.Calculate_Data value = driver.MultiEval.Pdynamics.Minimum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PDYNamics:SDEViation
				RsCmwLteMeas_MultiEval_Pdynamics_StandardDev.ResultData value = driver.MultiEval.Pdynamics.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PDYNamics:SDEViation
				RsCmwLteMeas_MultiEval_Pdynamics_StandardDev.ResultData value = driver.MultiEval.Pdynamics.StandardDev.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:CURRent
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Current.ResultData value = driver.MultiEval.Pmonitor.Cc.Current.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Current.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:CURRent
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Current.ResultData value = driver.MultiEval.Pmonitor.Cc.Current.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Current.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:AVERage
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Average.ResultData value = driver.MultiEval.Pmonitor.Cc.Average.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Average.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:AVERage
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Average.ResultData value = driver.MultiEval.Pmonitor.Cc.Average.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Average.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:MAXimum
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Maximum.ResultData value = driver.MultiEval.Pmonitor.Cc.Maximum.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Maximum.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:MAXimum
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Maximum.ResultData value = driver.MultiEval.Pmonitor.Cc.Maximum.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Maximum.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:MINimum
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Minimum.ResultData value = driver.MultiEval.Pmonitor.Cc.Minimum.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Minimum.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:MINimum
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_Minimum.ResultData value = driver.MultiEval.Pmonitor.Cc.Minimum.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.Minimum.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:SDEViation
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_StandardDev.ResultData value = driver.MultiEval.Pmonitor.Cc.StandardDev.Read(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.StandardDev.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CC<Nr>:SDEViation
				RsCmwLteMeas_MultiEval_Pmonitor_Cc_StandardDev.ResultData value = driver.MultiEval.Pmonitor.Cc.StandardDev.Fetch(CarrierComponentRepCap.Default);
				value = driver.MultiEval.Pmonitor.Cc.StandardDev.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:CURRent
				RsCmwLteMeas_MultiEval_Pmonitor_Current.ResultData value = driver.MultiEval.Pmonitor.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:CURRent
				RsCmwLteMeas_MultiEval_Pmonitor_Current.ResultData value = driver.MultiEval.Pmonitor.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PMONitor:CURRent
				RsCmwLteMeas_MultiEval_Pmonitor_Current.Calculate_Data value = driver.MultiEval.Pmonitor.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:AVERage
				RsCmwLteMeas_MultiEval_Pmonitor_Average.ResultData value = driver.MultiEval.Pmonitor.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:AVERage
				RsCmwLteMeas_MultiEval_Pmonitor_Average.ResultData value = driver.MultiEval.Pmonitor.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PMONitor:AVERage
				RsCmwLteMeas_MultiEval_Pmonitor_Average.Calculate_Data value = driver.MultiEval.Pmonitor.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:MAXimum
				RsCmwLteMeas_MultiEval_Pmonitor_Maximum.ResultData value = driver.MultiEval.Pmonitor.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:MAXimum
				RsCmwLteMeas_MultiEval_Pmonitor_Maximum.ResultData value = driver.MultiEval.Pmonitor.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PMONitor:MAXimum
				RsCmwLteMeas_MultiEval_Pmonitor_Maximum.Calculate_Data value = driver.MultiEval.Pmonitor.Maximum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:MINimum
				RsCmwLteMeas_MultiEval_Pmonitor_Minimum.ResultData value = driver.MultiEval.Pmonitor.Minimum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:MINimum
				RsCmwLteMeas_MultiEval_Pmonitor_Minimum.ResultData value = driver.MultiEval.Pmonitor.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:PMONitor:MINimum
				RsCmwLteMeas_MultiEval_Pmonitor_Minimum.Calculate_Data value = driver.MultiEval.Pmonitor.Minimum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:PMONitor:SDEViation
				RsCmwLteMeas_MultiEval_Pmonitor_StandardDev.ResultData value = driver.MultiEval.Pmonitor.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:PMONitor:SDEViation
				RsCmwLteMeas_MultiEval_Pmonitor_StandardDev.ResultData value = driver.MultiEval.Pmonitor.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:BLER
				RsCmwLteMeas_MultiEval_Bler.ResultData value = driver.MultiEval.Bler.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:MEValuation:BLER
				RsCmwLteMeas_MultiEval_Bler.ResultData value = driver.MultiEval.Bler.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SRELiability
				List<int> value = driver.MultiEval.List.Sreliability.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:RMS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Rms.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:PEAK:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Peak.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:EVM:DMRS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Evm.Dmrs.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:RMS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Rms.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:PEAK:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Peak.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:MERRor:DMRS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Merror.Dmrs.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:RMS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Rms.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:PEAK:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Peak.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:LOW:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.Low.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PERRor:DMRS:HIGH:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Perror.Dmrs.High.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:CURRent
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:CURRent
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:AVERage
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:AVERage
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:IQOFfset:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.IqOffset.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:CURRent
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:CURRent
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:AVERage
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:AVERage
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.FreqError.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:FERRor:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.FreqError.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Terror.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Terror.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Terror.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Terror.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Terror.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:EXTReme
				List<double> value = driver.MultiEval.List.Modulation.Terror.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TERRor:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Terror.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Minimum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Tpower.Maximum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:TPOWer:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Tpower.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Minimum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Ppower.Maximum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PPOWer:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Ppower.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Psd.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:CURRent
				List<double> value = driver.MultiEval.List.Modulation.Psd.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Psd.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:AVERage
				List<double> value = driver.MultiEval.List.Modulation.Psd.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Psd.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:MINimum
				List<double> value = driver.MultiEval.List.Modulation.Psd.Minimum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Psd.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:MAXimum
				List<double> value = driver.MultiEval.List.Modulation.Psd.Maximum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:PSD:SDEViation
				List<double> value = driver.MultiEval.List.Modulation.Psd.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:DMODulation
				List<ModulationEnum> value = driver.MultiEval.List.Modulation.Dmodulation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:DCHType
				List<UplinkChannelTypeEnum> value = driver.MultiEval.List.Modulation.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:SCHType
				List<SidelinkChannelTypeEnum> value = driver.MultiEval.List.Modulation.Schtype.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:MODulation:DALLocation
				RsCmwLteMeas_MultiEval_List_Modulation_Dallocation.Fetch_Data value = driver.MultiEval.List.Modulation.Dallocation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:CURRent
				List<double> value = driver.MultiEval.List.InbandEmission.Margin.Current.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:AVERage
				List<double> value = driver.MultiEval.List.InbandEmission.Margin.Average.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:EXTReme
				List<double> value = driver.MultiEval.List.InbandEmission.Margin.Extreme.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:SDEViation
				List<double> value = driver.MultiEval.List.InbandEmission.Margin.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:RBINdex:CURRent
				List<int> value = driver.MultiEval.List.InbandEmission.Margin.RbIndex.Current.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:IEMission:MARGin:RBINdex:EXTReme
				List<int> value = driver.MultiEval.List.InbandEmission.Margin.RbIndex.Extreme.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Current.Fetch(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Current.Calculate(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Average.Fetch(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Average.Calculate(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Extreme.Fetch(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.Extreme.Calculate(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:RIPPle<nr>:SDEViation
				List<double> value = driver.MultiEval.List.EsFlatness.Ripple.StandardDev.Fetch(RippleRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Ripple.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Current.Fetch(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Current.Calculate(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Average.Fetch(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Average.Calculate(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Extreme.Fetch(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.Extreme.Calculate(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:DIFFerence<nr>:SDEViation
				List<double> value = driver.MultiEval.List.EsFlatness.Difference.StandardDev.Fetch(DifferenceRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Difference.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Current.Fetch(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Current.Calculate(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Average.Fetch(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Average.Calculate(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Extreme.Fetch(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.Extreme.Calculate(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MINR<nr>:SDEViation
				List<double> value = driver.MultiEval.List.EsFlatness.Minr.StandardDev.Fetch(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Minr.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Current.Fetch(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:CURRent
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Current.Calculate(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Average.Fetch(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:AVERage
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Average.Calculate(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Extreme.Fetch(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:EXTReme
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.Extreme.Calculate(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:MAXR<nr>:SDEViation
				List<double> value = driver.MultiEval.List.EsFlatness.Maxr.StandardDev.Fetch(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.Maxr.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:SCINdex:MINimum<nr>:CURRent
				List<int> value = driver.MultiEval.List.EsFlatness.ScIndex.Minimum.Current.Fetch(MinRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.ScIndex.Minimum.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ESFLatness:SCINdex:MAXimum<nr>:CURRent
				List<int> value = driver.MultiEval.List.EsFlatness.ScIndex.Maximum.Current.Fetch(MaxRangeRepCap.Default);
				value = driver.MultiEval.List.EsFlatness.ScIndex.Maximum.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Current.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Current.Calculate_Data value = driver.MultiEval.List.Segment.Modulation.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Average.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Average.Calculate_Data value = driver.MultiEval.List.Segment.Modulation.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Extreme.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Extreme.Calculate_Data value = driver.MultiEval.List.Segment.Modulation.Extreme.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:DMODulation
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Dmodulation.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Dmodulation.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Dmodulation.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:DCHType
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_DchType.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.DchType.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.DchType.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:SCHType
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Schtype.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Schtype.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Schtype.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:DALLocation
				RsCmwLteMeas_MultiEval_List_Segment_Modulation_Dallocation.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Dallocation.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Dallocation.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_Current.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Current.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:CURRent:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_Current_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Current.RbIndex.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Current.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_Average.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Average.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Extreme.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Extreme.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:EXTReme:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_Extreme_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Extreme.RbIndex.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.Extreme.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:SCC<c>:MARGin:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Scc_Margin_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.StandardDev.Fetch(SegmentRepCap.Default, SecondaryCCRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Scc.Margin.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_Current.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Current.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:CURRent:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_Current_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Current.RbIndex.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Current.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_Average.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Average.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Extreme.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Extreme.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:EXTReme:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_Extreme_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Extreme.RbIndex.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.Extreme.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:CC<c>:MARGin:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Cc_Margin_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.StandardDev.Fetch(SegmentRepCap.Default, CarrierComponentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Cc.Margin.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_Current.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:CURRent:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_Current_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.Current.RbIndex.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.Current.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_Average.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.Extreme.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.Extreme.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:EXTReme:RBINdex
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_Extreme_RbIndex.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.Extreme.RbIndex.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.Extreme.RbIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:IEMission:MARGin:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_InbandEmission_Margin_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.InbandEmission.Margin.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.InbandEmission.Margin.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Current.Fetch_Data value = driver.MultiEval.List.Segment.EsFlatness.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Current.Calculate_Data value = driver.MultiEval.List.Segment.EsFlatness.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:CURRent:SCINdex
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Current_ScIndex.Fetch_Data value = driver.MultiEval.List.Segment.EsFlatness.Current.ScIndex.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Current.ScIndex.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Average.Fetch_Data value = driver.MultiEval.List.Segment.EsFlatness.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Average.Calculate_Data value = driver.MultiEval.List.Segment.EsFlatness.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.EsFlatness.Extreme.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_Extreme.Calculate_Data value = driver.MultiEval.List.Segment.EsFlatness.Extreme.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ESFLatness:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_EsFlatness_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.EsFlatness.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.EsFlatness.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Current.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Current.Calculate_Data value = driver.MultiEval.List.Segment.SeMask.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Average.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Average.Calculate_Data value = driver.MultiEval.List.Segment.SeMask.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Extreme.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Extreme.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Extreme.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:EXTReme
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Extreme.Calculate_Data value = driver.MultiEval.List.Segment.SeMask.Extreme.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Extreme.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:ALL
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_All.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.All.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.All.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:CURRent:NEGativ
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Current_Negativ.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Current.Negativ.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Current.Negativ.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:CURRent:POSitiv
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Current_Positiv.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Current.Positiv.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Current.Positiv.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:AVERage:NEGativ
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Average_Negativ.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Average.Negativ.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Average.Negativ.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:AVERage:POSitiv
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Average_Positiv.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Average.Positiv.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Average.Positiv.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:MINimum:NEGativ
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Minimum_Negativ.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Minimum.Negativ.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Minimum.Negativ.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:MARGin:MINimum:POSitiv
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Margin_Minimum_Positiv.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Margin.Minimum.Positiv.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Margin.Minimum.Positiv.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:DCHType
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_DchType.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.DchType.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.DchType.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SEMask:DALLocation
				RsCmwLteMeas_MultiEval_List_Segment_SeMask_Dallocation.Fetch_Data value = driver.MultiEval.List.Segment.SeMask.Dallocation.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SeMask.Dallocation.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_Current.Fetch_Data value = driver.MultiEval.List.Segment.Aclr.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_Current.Calculate_Data value = driver.MultiEval.List.Segment.Aclr.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_Average.Fetch_Data value = driver.MultiEval.List.Segment.Aclr.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_Average.Calculate_Data value = driver.MultiEval.List.Segment.Aclr.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:DCHType
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_DchType.Fetch_Data value = driver.MultiEval.List.Segment.Aclr.DchType.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.DchType.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:ACLR:DALLocation
				RsCmwLteMeas_MultiEval_List_Segment_Aclr_Dallocation.Fetch_Data value = driver.MultiEval.List.Segment.Aclr.Dallocation.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Aclr.Dallocation.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Power_Current.Fetch_Data value = driver.MultiEval.List.Segment.Power.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:CURRent
				RsCmwLteMeas_MultiEval_List_Segment_Power_Current.Calculate_Data value = driver.MultiEval.List.Segment.Power.Current.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Power_Average.Fetch_Data value = driver.MultiEval.List.Segment.Power.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:AVERage
				RsCmwLteMeas_MultiEval_List_Segment_Power_Average.Calculate_Data value = driver.MultiEval.List.Segment.Power.Average.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:MINimum
				RsCmwLteMeas_MultiEval_List_Segment_Power_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.Power.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Minimum.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:MINimum
				RsCmwLteMeas_MultiEval_List_Segment_Power_Minimum.Calculate_Data value = driver.MultiEval.List.Segment.Power.Minimum.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Minimum.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:MAXimum
				RsCmwLteMeas_MultiEval_List_Segment_Power_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.Power.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Maximum.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:MAXimum
				RsCmwLteMeas_MultiEval_List_Segment_Power_Maximum.Calculate_Data value = driver.MultiEval.List.Segment.Power.Maximum.Calculate(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.Maximum.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:POWer:SDEViation
				RsCmwLteMeas_MultiEval_List_Segment_Power_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.Power.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Power.StandardDev.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor:RMS
				RsCmwLteMeas_MultiEval_List_Segment_Pmonitor_Rms.Fetch_Data value = driver.MultiEval.List.Segment.Pmonitor.Rms.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Pmonitor.Rms.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor:PEAK
				RsCmwLteMeas_MultiEval_List_Segment_Pmonitor_Peak.Fetch_Data value = driver.MultiEval.List.Segment.Pmonitor.Peak.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Pmonitor.Peak.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor:ARRay:STARt
				int value = driver.MultiEval.List.Segment.Pmonitor.Array.Start.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Pmonitor.Array.Start.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PMONitor:ARRay:LENGth
				int value = driver.MultiEval.List.Segment.Pmonitor.Array.Length.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Pmonitor.Array.Length.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:CURRent
				List<double> value = driver.MultiEval.List.SeMask.Obw.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:CURRent
				List<double> value = driver.MultiEval.List.SeMask.Obw.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:AVERage
				List<double> value = driver.MultiEval.List.SeMask.Obw.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:AVERage
				List<double> value = driver.MultiEval.List.SeMask.Obw.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:EXTReme
				List<double> value = driver.MultiEval.List.SeMask.Obw.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:EXTReme
				List<double> value = driver.MultiEval.List.SeMask.Obw.Extreme.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:OBW:SDEViation
				List<double> value = driver.MultiEval.List.SeMask.Obw.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:CURRent
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:CURRent
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:AVERage
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:AVERage
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:MINimum
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:MINimum
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Minimum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:MAXimum
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:MAXimum
				List<double> value = driver.MultiEval.List.SeMask.TxPower.Maximum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:TXPower:SDEViation
				List<double> value = driver.MultiEval.List.SeMask.TxPower.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:NEGativ:CURRent
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Negativ_Current.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Current.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:NEGativ:AVERage
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Negativ_Average.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Average.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:NEGativ:MINimum
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Negativ_Minimum.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Minimum.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Negativ.Minimum.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:POSitiv:CURRent
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Positiv_Current.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Current.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Current.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:POSitiv:AVERage
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Positiv_Average.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Average.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Average.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:MARGin:AREA<nr>:POSitiv:MINimum
				RsCmwLteMeas_MultiEval_List_SeMask_Margin_Area_Positiv_Minimum.Fetch_Data value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Minimum.Fetch(AreaRepCap.Default);
				value = driver.MultiEval.List.SeMask.Margin.Area.Positiv.Minimum.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:DCHType
				List<UplinkChannelTypeEnum> value = driver.MultiEval.List.SeMask.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:SEMask:DALLocation
				RsCmwLteMeas_MultiEval_List_SeMask_Dallocation.Fetch_Data value = driver.MultiEval.List.SeMask.Dallocation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:NEGativ:CURRent
				List<double> value = driver.MultiEval.List.Aclr.Utra.Negativ.Current.Fetch(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Negativ.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:NEGativ:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Utra.Negativ.Current.Calculate(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Negativ.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:NEGativ:AVERage
				List<double> value = driver.MultiEval.List.Aclr.Utra.Negativ.Average.Fetch(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Negativ.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:NEGativ:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Utra.Negativ.Average.Calculate(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Negativ.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:POSitiv:CURRent
				List<double> value = driver.MultiEval.List.Aclr.Utra.Positiv.Current.Fetch(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Positiv.Current.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:POSitiv:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Utra.Positiv.Current.Calculate(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Positiv.Current.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:POSitiv:AVERage
				List<double> value = driver.MultiEval.List.Aclr.Utra.Positiv.Average.Fetch(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Positiv.Average.Fetch();
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:UTRA<nr>:POSitiv:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Utra.Positiv.Average.Calculate(UtraAdjChannelRepCap.Default);
				value = driver.MultiEval.List.Aclr.Utra.Positiv.Average.Calculate();
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:NEGativ:CURRent
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Negativ.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:NEGativ:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Negativ.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:NEGativ:AVERage
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Negativ.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:NEGativ:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Negativ.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:CURRent
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:POSitiv:CURRent
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Positiv.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:POSitiv:CURRent
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Positiv.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:POSitiv:AVERage
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Positiv.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:POSitiv:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Positiv.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:AVERage
				List<double> value = driver.MultiEval.List.Aclr.Eutra.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:EUTRa:AVERage
				List<ResultStatus2enum> value = driver.MultiEval.List.Aclr.Eutra.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:DCHType
				List<UplinkChannelTypeEnum> value = driver.MultiEval.List.Aclr.DchType.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:ACLR:DALLocation
				RsCmwLteMeas_MultiEval_List_Aclr_Dallocation.Fetch_Data value = driver.MultiEval.List.Aclr.Dallocation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:CURRent
				List<double> value = driver.MultiEval.List.Power.TxPower.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:CURRent
				List<double> value = driver.MultiEval.List.Power.TxPower.Current.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:AVERage
				List<double> value = driver.MultiEval.List.Power.TxPower.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:AVERage
				List<double> value = driver.MultiEval.List.Power.TxPower.Average.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:MINimum
				List<double> value = driver.MultiEval.List.Power.TxPower.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:MINimum
				List<double> value = driver.MultiEval.List.Power.TxPower.Minimum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:MAXimum
				List<double> value = driver.MultiEval.List.Power.TxPower.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:MAXimum
				List<double> value = driver.MultiEval.List.Power.TxPower.Maximum.Calculate();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:POWer:TXPower:SDEViation
				List<double> value = driver.MultiEval.List.Power.TxPower.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:PMONitor:RMS
				List<double> value = driver.MultiEval.List.Pmonitor.Rms.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:MEValuation:LIST:PMONitor:PEAK
				List<double> value = driver.MultiEval.List.Pmonitor.Peak.Fetch();				
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:SOURce
				string value = driver.Trigger.MultiEval.Source;
				driver.Trigger.MultiEval.Source = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:THReshold
				double value = driver.Trigger.MultiEval.Threshold;
				driver.Trigger.MultiEval.Threshold = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.MultiEval.Slope = x;
					SignalSlopeEnum value = driver.Trigger.MultiEval.Slope;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:DELay
				double value = driver.Trigger.MultiEval.Delay;
				driver.Trigger.MultiEval.Delay = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Trigger.MultiEval.Timeout;
				driver.Trigger.MultiEval.Timeout = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:MGAP
				int value = driver.Trigger.MultiEval.Mgap;
				driver.Trigger.MultiEval.Mgap = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:SMODe
				foreach (SyncModeEnum x in new SyncModeEnum[] { SyncModeEnum.ENHanced, SyncModeEnum.NORMal })
				{
					driver.Trigger.MultiEval.Smode = x;
					SyncModeEnum value = driver.Trigger.MultiEval.Smode;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:AMODe
				foreach (MevAcquisitionModeEnum x in new MevAcquisitionModeEnum[] { MevAcquisitionModeEnum.SLOT, MevAcquisitionModeEnum.SUBFrame })
				{
					driver.Trigger.MultiEval.Amode = x;
					MevAcquisitionModeEnum value = driver.Trigger.MultiEval.Amode;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:CATalog:SOURce
				List<string> value = driver.Trigger.MultiEval.Catalog.Source;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:MEValuation:LIST:MODE
				foreach (ListModeEnum x in new ListModeEnum[] { ListModeEnum.ONCE, ListModeEnum.SEGMent })
				{
					driver.Trigger.MultiEval.List.Mode = x;
					ListModeEnum value = driver.Trigger.MultiEval.List.Mode;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:SOURce
				string value = driver.Trigger.Prach.Source;
				driver.Trigger.Prach.Source = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:THReshold
				double value = driver.Trigger.Prach.Threshold;
				driver.Trigger.Prach.Threshold = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.Prach.Slope = x;
					SignalSlopeEnum value = driver.Trigger.Prach.Slope;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:TOUT
				double value = driver.Trigger.Prach.Timeout;
				driver.Trigger.Prach.Timeout = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:MGAP
				double value = driver.Trigger.Prach.Mgap;
				driver.Trigger.Prach.Mgap = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:PRACh:CATalog:SOURce
				List<string> value = driver.Trigger.Prach.Catalog.Source;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:SOURce
				string value = driver.Trigger.Srs.Source;
				driver.Trigger.Srs.Source = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:THReshold
				double value = driver.Trigger.Srs.Threshold;
				driver.Trigger.Srs.Threshold = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.Srs.Slope = x;
					SignalSlopeEnum value = driver.Trigger.Srs.Slope;
				}
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:TOUT
				double value = driver.Trigger.Srs.Timeout;
				driver.Trigger.Srs.Timeout = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:MGAP
				double value = driver.Trigger.Srs.Mgap;
				driver.Trigger.Srs.Mgap = value;
			}
			{	// TRIGger:LTE:MEASurement<Instance>:SRS:CATalog:SOURce
				List<string> value = driver.Trigger.Srs.Catalog.Source;
			}
			{	// INITiate:LTE:MEASurement<Instance>:PRACh
				driver.Prach.Initiate();
				driver.Prach.InitiateAndWait();
			}
			{	// STOP:LTE:MEASurement<Instance>:PRACh
				driver.Prach.Stop();
				driver.Prach.StopAndWait();
			}
			{	// ABORt:LTE:MEASurement<Instance>:PRACh
				driver.Prach.Abort();
				driver.Prach.AbortAndWait();
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:STATe
				ResourceStateEnum value = driver.Prach.State.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:STATe:ALL
				RsCmwLteMeas_Prach_State_All.Fetch_Data value = driver.Prach.State.All.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:CURRent
				List<double> value = driver.Prach.Trace.Evm.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:CURRent
				List<double> value = driver.Prach.Trace.Evm.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:AVERage
				List<double> value = driver.Prach.Trace.Evm.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:AVERage
				List<double> value = driver.Prach.Trace.Evm.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:MAXimum
				List<double> value = driver.Prach.Trace.Evm.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:EVM:MAXimum
				List<double> value = driver.Prach.Trace.Evm.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:EVPReamble
				List<double> value = driver.Prach.Trace.EvPreamble.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:EVPReamble
				List<double> value = driver.Prach.Trace.EvPreamble.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:CURRent
				List<double> value = driver.Prach.Trace.Merror.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:CURRent
				List<double> value = driver.Prach.Trace.Merror.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:AVERage
				List<double> value = driver.Prach.Trace.Merror.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:AVERage
				List<double> value = driver.Prach.Trace.Merror.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:MAXimum
				List<double> value = driver.Prach.Trace.Merror.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:MERRor:MAXimum
				List<double> value = driver.Prach.Trace.Merror.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:CURRent
				List<double> value = driver.Prach.Trace.Perror.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:CURRent
				List<double> value = driver.Prach.Trace.Perror.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:AVERage
				List<double> value = driver.Prach.Trace.Perror.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:AVERage
				List<double> value = driver.Prach.Trace.Perror.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:MAXimum
				List<double> value = driver.Prach.Trace.Perror.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PERRor:MAXimum
				List<double> value = driver.Prach.Trace.Perror.Maximum.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:IQ
				RsCmwLteMeas_Prach_Trace_Iq.Fetch_Data value = driver.Prach.Trace.Iq.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:CURRent
				List<double> value = driver.Prach.Trace.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:CURRent
				List<double> value = driver.Prach.Trace.Pdynamics.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:AVERage
				List<double> value = driver.Prach.Trace.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:AVERage
				List<double> value = driver.Prach.Trace.Pdynamics.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:MAXimum
				List<double> value = driver.Prach.Trace.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PDYNamics:MAXimum
				List<double> value = driver.Prach.Trace.Pdynamics.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:TRACe:PVPReamble
				List<double> value = driver.Prach.Trace.PvPreamble.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:TRACe:PVPReamble
				List<double> value = driver.Prach.Trace.PvPreamble.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:MODulation:CURRent
				RsCmwLteMeas_Prach_Modulation_Current.ResultData value = driver.Prach.Modulation.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:CURRent
				RsCmwLteMeas_Prach_Modulation_Current.ResultData value = driver.Prach.Modulation.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:MODulation:CURRent
				RsCmwLteMeas_Prach_Modulation_Current.Calculate_Data value = driver.Prach.Modulation.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:MODulation:PREamble<Number>
				RsCmwLteMeas_Prach_Modulation_Preamble.ResultData value = driver.Prach.Modulation.Preamble.Read(PreambleRepCap.Default);
				value = driver.Prach.Modulation.Preamble.Read();
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:PREamble<Number>
				RsCmwLteMeas_Prach_Modulation_Preamble.ResultData value = driver.Prach.Modulation.Preamble.Fetch(PreambleRepCap.Default);
				value = driver.Prach.Modulation.Preamble.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:MODulation:AVERage
				RsCmwLteMeas_Prach_Modulation_Average.ResultData value = driver.Prach.Modulation.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:AVERage
				RsCmwLteMeas_Prach_Modulation_Average.ResultData value = driver.Prach.Modulation.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:MODulation:AVERage
				RsCmwLteMeas_Prach_Modulation_Average.Calculate_Data value = driver.Prach.Modulation.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:MODulation:EXTReme
				RsCmwLteMeas_Prach_Modulation_Extreme.ResultData value = driver.Prach.Modulation.Extreme.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:EXTReme
				RsCmwLteMeas_Prach_Modulation_Extreme.ResultData value = driver.Prach.Modulation.Extreme.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:MODulation:EXTReme
				RsCmwLteMeas_Prach_Modulation_Extreme.Calculate_Data value = driver.Prach.Modulation.Extreme.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:MODulation:SDEViation
				RsCmwLteMeas_Prach_Modulation_StandardDev.ResultData value = driver.Prach.Modulation.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:SDEViation
				RsCmwLteMeas_Prach_Modulation_StandardDev.ResultData value = driver.Prach.Modulation.StandardDev.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:DPFoffset
				int value = driver.Prach.Modulation.DpfOffset.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:DPFoffset:PREamble<Number>
				int value = driver.Prach.Modulation.DpfOffset.Preamble.Fetch(PreambleRepCap.Default);
				value = driver.Prach.Modulation.DpfOffset.Preamble.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:DSINdex
				int value = driver.Prach.Modulation.DsIndex.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:DSINdex:PREamble<Number>
				int value = driver.Prach.Modulation.DsIndex.Preamble.Fetch(PreambleRepCap.Default);
				value = driver.Prach.Modulation.DsIndex.Preamble.Fetch();
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:SCORrelation
				double value = driver.Prach.Modulation.Scorrelation.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:MODulation:SCORrelation:PREamble<Number>
				double value = driver.Prach.Modulation.Scorrelation.Preamble.Fetch(PreambleRepCap.Default);
				value = driver.Prach.Modulation.Scorrelation.Preamble.Fetch();
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:PDYNamics:CURRent
				RsCmwLteMeas_Prach_Pdynamics_Current.ResultData value = driver.Prach.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:PDYNamics:CURRent
				RsCmwLteMeas_Prach_Pdynamics_Current.ResultData value = driver.Prach.Pdynamics.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:PDYNamics:CURRent
				RsCmwLteMeas_Prach_Pdynamics_Current.Calculate_Data value = driver.Prach.Pdynamics.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:PDYNamics:AVERage
				RsCmwLteMeas_Prach_Pdynamics_Average.ResultData value = driver.Prach.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:PDYNamics:AVERage
				RsCmwLteMeas_Prach_Pdynamics_Average.ResultData value = driver.Prach.Pdynamics.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:PDYNamics:AVERage
				RsCmwLteMeas_Prach_Pdynamics_Average.Calculate_Data value = driver.Prach.Pdynamics.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:PDYNamics:MAXimum
				RsCmwLteMeas_Prach_Pdynamics_Maximum.ResultData value = driver.Prach.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:PDYNamics:MAXimum
				RsCmwLteMeas_Prach_Pdynamics_Maximum.ResultData value = driver.Prach.Pdynamics.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:PDYNamics:MAXimum
				RsCmwLteMeas_Prach_Pdynamics_Maximum.Calculate_Data value = driver.Prach.Pdynamics.Maximum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:PDYNamics:MINimum
				RsCmwLteMeas_Prach_Pdynamics_Minimum.ResultData value = driver.Prach.Pdynamics.Minimum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:PDYNamics:MINimum
				RsCmwLteMeas_Prach_Pdynamics_Minimum.ResultData value = driver.Prach.Pdynamics.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:PRACh:PDYNamics:MINimum
				RsCmwLteMeas_Prach_Pdynamics_Minimum.Calculate_Data value = driver.Prach.Pdynamics.Minimum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:PRACh:PDYNamics:SDEViation
				RsCmwLteMeas_Prach_Pdynamics_StandardDev.ResultData value = driver.Prach.Pdynamics.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:PRACh:PDYNamics:SDEViation
				RsCmwLteMeas_Prach_Pdynamics_StandardDev.ResultData value = driver.Prach.Pdynamics.StandardDev.Fetch();				
			}
			{	// INITiate:LTE:MEASurement<Instance>:SRS
				driver.Srs.Initiate();
				driver.Srs.InitiateAndWait();
			}
			{	// STOP:LTE:MEASurement<Instance>:SRS
				driver.Srs.Stop();
				driver.Srs.StopAndWait();
			}
			{	// ABORt:LTE:MEASurement<Instance>:SRS
				driver.Srs.Abort();
				driver.Srs.AbortAndWait();
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:STATe
				ResourceStateEnum value = driver.Srs.State.Fetch();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:STATe:ALL
				RsCmwLteMeas_Srs_State_All.Fetch_Data value = driver.Srs.State.All.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:CURRent
				List<double> value = driver.Srs.Trace.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:CURRent
				List<double> value = driver.Srs.Trace.Pdynamics.Current.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:AVERage
				List<double> value = driver.Srs.Trace.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:AVERage
				List<double> value = driver.Srs.Trace.Pdynamics.Average.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:MAXimum
				List<double> value = driver.Srs.Trace.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:TRACe:PDYNamics:MAXimum
				List<double> value = driver.Srs.Trace.Pdynamics.Maximum.Fetch();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:PDYNamics:CURRent
				RsCmwLteMeas_Srs_Pdynamics_Current.ResultData value = driver.Srs.Pdynamics.Current.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:PDYNamics:CURRent
				RsCmwLteMeas_Srs_Pdynamics_Current.ResultData value = driver.Srs.Pdynamics.Current.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:SRS:PDYNamics:CURRent
				RsCmwLteMeas_Srs_Pdynamics_Current.Calculate_Data value = driver.Srs.Pdynamics.Current.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:PDYNamics:AVERage
				RsCmwLteMeas_Srs_Pdynamics_Average.ResultData value = driver.Srs.Pdynamics.Average.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:PDYNamics:AVERage
				RsCmwLteMeas_Srs_Pdynamics_Average.ResultData value = driver.Srs.Pdynamics.Average.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:SRS:PDYNamics:AVERage
				RsCmwLteMeas_Srs_Pdynamics_Average.Calculate_Data value = driver.Srs.Pdynamics.Average.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:PDYNamics:MAXimum
				RsCmwLteMeas_Srs_Pdynamics_Maximum.ResultData value = driver.Srs.Pdynamics.Maximum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:PDYNamics:MAXimum
				RsCmwLteMeas_Srs_Pdynamics_Maximum.ResultData value = driver.Srs.Pdynamics.Maximum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:SRS:PDYNamics:MAXimum
				RsCmwLteMeas_Srs_Pdynamics_Maximum.Calculate_Data value = driver.Srs.Pdynamics.Maximum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:PDYNamics:MINimum
				RsCmwLteMeas_Srs_Pdynamics_Minimum.ResultData value = driver.Srs.Pdynamics.Minimum.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:PDYNamics:MINimum
				RsCmwLteMeas_Srs_Pdynamics_Minimum.ResultData value = driver.Srs.Pdynamics.Minimum.Fetch();				
			}
			{	// CALCulate:LTE:MEASurement<Instance>:SRS:PDYNamics:MINimum
				RsCmwLteMeas_Srs_Pdynamics_Minimum.Calculate_Data value = driver.Srs.Pdynamics.Minimum.Calculate();				
			}
			{	// READ:LTE:MEASurement<Instance>:SRS:PDYNamics:SDEViation
				RsCmwLteMeas_Srs_Pdynamics_StandardDev.ResultData value = driver.Srs.Pdynamics.StandardDev.Read();				
			}
			{	// FETCh:LTE:MEASurement<Instance>:SRS:PDYNamics:SDEViation
				RsCmwLteMeas_Srs_Pdynamics_StandardDev.ResultData value = driver.Srs.Pdynamics.StandardDev.Fetch();				
			}
		}
	}
}