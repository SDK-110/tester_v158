using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwCdma2kSig;

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
			RsCmwCdma2kSig driver = new RsCmwCdma2kSig("TCPIP::localhost::INSTR", true, true);
			{	// CONFigure:CDMA:SIGNaling<Instance>:DISPlay
				foreach (DisplayTabEnum x in new DisplayTabEnum[] { DisplayTabEnum.FERFch, DisplayTabEnum.FERSch0, DisplayTabEnum.POWer, DisplayTabEnum.RLP, DisplayTabEnum.SPEech })
				{
					driver.Configure.Display = x;
					DisplayTabEnum value = driver.Configure.Display;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:ETOE
				bool value = driver.Configure.Etoe;
				driver.Configure.Etoe = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:ESCode
				bool value = driver.Configure.EsCode;
				driver.Configure.EsCode = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:TEST:MSINfo:ESN
				double value = driver.Configure.Test.MsInfo.Esn;
				driver.Configure.Test.MsInfo.Esn = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:TEST:MSINfo:MEID
				double value = driver.Configure.Test.MsInfo.Meid;
				driver.Configure.Test.MsInfo.Meid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:EATTenuation
				RsCmwCdma2kSig_Configure_RfSettings.Eattenuation_Data value = driver.Configure.RfSettings.Eattenuation;
				driver.Configure.RfSettings.Eattenuation = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:BCLass
				foreach (BandClassEnum x in new BandClassEnum[] { BandClassEnum.AWS, BandClassEnum.B18M, BandClassEnum.IEXT, BandClassEnum.IM2K, BandClassEnum.JTAC, BandClassEnum.KCEL, BandClassEnum.KPCS, BandClassEnum.LBANd, BandClassEnum.LO7C, BandClassEnum.N45T, BandClassEnum.NA7C, BandClassEnum.NA8S, BandClassEnum.NA9C, BandClassEnum.NAPC, BandClassEnum.PA4M, BandClassEnum.PA8M, BandClassEnum.PS7C, BandClassEnum.SBANd, BandClassEnum.TACS, BandClassEnum.U25B, BandClassEnum.U25F, BandClassEnum.USC, BandClassEnum.USPC })
				{
					driver.Configure.RfSettings.Bclass = x;
					BandClassEnum value = driver.Configure.RfSettings.Bclass;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:FREQuency
				RsCmwCdma2kSig_Configure_RfSettings.Frequency_Data value = driver.Configure.RfSettings.Frequency;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:FLFRequency
				double value = driver.Configure.RfSettings.FlFrequency;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:RLFRequency
				double value = driver.Configure.RfSettings.RlFrequency;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:FOFFset
				double value = driver.Configure.RfSettings.FreqOffset;
				driver.Configure.RfSettings.FreqOffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFSettings:CHANnel
				int value = driver.Configure.RfSettings.Channel;
				driver.Configure.RfSettings.Channel = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Fsimulator.Enable;
				driver.Configure.Fading.Fsimulator.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:STANdard
				foreach (FadingSimStandardEnum x in new FadingSimStandardEnum[] { FadingSimStandardEnum.P1, FadingSimStandardEnum.P2, FadingSimStandardEnum.P3, FadingSimStandardEnum.P4, FadingSimStandardEnum.P5, FadingSimStandardEnum.P6 })
				{
					driver.Configure.Fading.Fsimulator.Standard = x;
					FadingSimStandardEnum value = driver.Configure.Fading.Fsimulator.Standard;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:KCONstant
				foreach (KeepConstantEnum x in new KeepConstantEnum[] { KeepConstantEnum.DSHift, KeepConstantEnum.SPEed })
				{
					driver.Configure.Fading.Fsimulator.Kconstant = x;
					KeepConstantEnum value = driver.Configure.Fading.Fsimulator.Kconstant;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:RESTart:MODE
				foreach (FadingSimRestartModeEnum x in new FadingSimRestartModeEnum[] { FadingSimRestartModeEnum.AUTO, FadingSimRestartModeEnum.MANual, FadingSimRestartModeEnum.TRIGger })
				{
					driver.Configure.Fading.Fsimulator.Restart.Mode = x;
					FadingSimRestartModeEnum value = driver.Configure.Fading.Fsimulator.Restart.Mode;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:RESTart
				driver.Configure.Fading.Fsimulator.Restart.Set();
				driver.Configure.Fading.Fsimulator.Restart.SetAndWait();
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Fsimulator.Globale.Seed;
				driver.Configure.Fading.Fsimulator.Globale.Seed = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.LACP, InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Fsimulator.InsertionLoss.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Fsimulator.InsertionLoss.Mode;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Loss;
				driver.Configure.Fading.Fsimulator.InsertionLoss.Loss = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:FSIMulator:ILOSs:CSAMples
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Csamples;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:AWGN:ENABle
				bool value = driver.Configure.Fading.Awgn.Enable;
				driver.Configure.Fading.Awgn.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:AWGN:SNRatio
				double value = driver.Configure.Fading.Awgn.SnRatio;
				driver.Configure.Fading.Awgn.SnRatio = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Awgn.Bandwidth.Ratio;
				driver.Configure.Fading.Awgn.Bandwidth.Ratio = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:AWGN:BWIDth:NOISe
				double value = driver.Configure.Fading.Awgn.Bandwidth.Noise;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:POWer:SIGNal
				double value = driver.Configure.Fading.Power.Signal;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:POWer:SUM
				double value = driver.Configure.Fading.Power.Sum;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Power.Noise.Total;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:FADing:POWer:NOISe
				double value = driver.Configure.Fading.Power.Noise.Value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:IQIN:PATH<n>
				RsCmwCdma2kSig_Configure_IqIn_Path.Path_Data value = driver.Configure.IqIn.Path.Get(PathRepCap.Default);
				value = driver.Configure.IqIn.Path.Get();
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:IQIN:PATH<n>
				RsCmwCdma2kSig_Configure_IqIn_Path.Path_Data value = new RsCmwCdma2kSig_Configure_IqIn_Path.Path_Data();
				driver.Configure.IqIn.Path.Set(value, PathRepCap.Default);
				driver.Configure.IqIn.Path.Set(value);
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MMONitor:IPADdress
				RsCmwCdma2kSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MMONitor:IPADdress
				foreach (IpAddressIndexEnum x in new IpAddressIndexEnum[] { IpAddressIndexEnum.IP1, IpAddressIndexEnum.IP2, IpAddressIndexEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:CSTatus:LOG
				string value = driver.Configure.Cstatus.Log;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CSTatus:VCODer
				string value = driver.Configure.Cstatus.Vcoder;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CSTatus:MOPTion:FCH
				RsCmwCdma2kSig_Configure_Cstatus_Moption.Fch_Data value = driver.Configure.Cstatus.Moption.Fch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CSTatus:MOPTion:SCH
				RsCmwCdma2kSig_Configure_Cstatus_Moption.Sch_Data value = driver.Configure.Cstatus.Moption.Sch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CSTatus:DRATe:SCH
				RsCmwCdma2kSig_Configure_Cstatus_Drate.Sch_Data value = driver.Configure.Cstatus.Drate.Sch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:EXPected
				double value = driver.Configure.RfPower.Expected;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:CDMA
				double value = driver.Configure.RfPower.Cdma;
				driver.Configure.RfPower.Cdma = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:OUTPut
				double value = driver.Configure.RfPower.Output;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:EPMode
				foreach (ExpectedPowerModeEnum x in new ExpectedPowerModeEnum[] { ExpectedPowerModeEnum.MANual, ExpectedPowerModeEnum.MAX, ExpectedPowerModeEnum.MIN, ExpectedPowerModeEnum.OLRule })
				{
					driver.Configure.RfPower.Epmode = x;
					ExpectedPowerModeEnum value = driver.Configure.RfPower.Epmode;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:MANual
				double value = driver.Configure.RfPower.Manual;
				driver.Configure.RfPower.Manual = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:PICH
				double value = driver.Configure.RfPower.Level.Pich;
				driver.Configure.RfPower.Level.Pich = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:SYNC
				double value = driver.Configure.RfPower.Level.Sync;
				driver.Configure.RfPower.Level.Sync = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:PCH
				double value = driver.Configure.RfPower.Level.Pch;
				driver.Configure.RfPower.Level.Pch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:FCH
				double value = driver.Configure.RfPower.Level.Fch;
				driver.Configure.RfPower.Level.Fch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:SCH
				double value = driver.Configure.RfPower.Level.Sch;
				driver.Configure.RfPower.Level.Sch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:QPCH
				int value = driver.Configure.RfPower.Level.Qpch;
				driver.Configure.RfPower.Level.Qpch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:AWGN
				double value = driver.Configure.RfPower.Level.Awgn;
				driver.Configure.RfPower.Level.Awgn = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:OCNS
				RsCmwCdma2kSig_Configure_RfPower_Level_Ocns.Get_Data value = driver.Configure.RfPower.Level.Ocns.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:LEVel:OCNS
				driver.Configure.RfPower.Level.Ocns.Set(false);				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:EBNT:FCH
				double value = driver.Configure.RfPower.Ebnt.Fch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:EBNT:SCH
				double value = driver.Configure.RfPower.Ebnt.Sch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RFPower:MODE:AWGN
				double value = driver.Configure.RfPower.Mode.Awgn;
				driver.Configure.RfPower.Mode.Awgn = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:RCONfig
				foreach (RadioConfigEnum x in new RadioConfigEnum[] { RadioConfigEnum.F1R1, RadioConfigEnum.F2R2, RadioConfigEnum.F3R3, RadioConfigEnum.F4R3, RadioConfigEnum.F5R4 })
				{
					driver.Configure.Layer.Rconfig = x;
					RadioConfigEnum value = driver.Configure.Layer.Rconfig;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:MODulation
				foreach (ModulationEnum x in new ModulationEnum[] { ModulationEnum.HPSK, ModulationEnum.QPSK })
				{
					ModulationEnum value = driver.Configure.Layer.Modulation;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SOPTion:FIRSt
				foreach (ServiceOptionEnum x in new ServiceOptionEnum[] { ServiceOptionEnum.SO1, ServiceOptionEnum.SO17, ServiceOptionEnum.SO2, ServiceOptionEnum.SO3, ServiceOptionEnum.SO32, ServiceOptionEnum.SO33, ServiceOptionEnum.SO55, ServiceOptionEnum.SO68, ServiceOptionEnum.SO70, ServiceOptionEnum.SO73, ServiceOptionEnum.SO8000, ServiceOptionEnum.SO9 })
				{
					driver.Configure.Layer.Soption.First = x;
					ServiceOptionEnum value = driver.Configure.Layer.Soption.First;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:PICH
				RsCmwCdma2kSig_Configure_Layer_Channel.Pich_Data value = driver.Configure.Layer.Channel.Pich;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:PCH
				RsCmwCdma2kSig_Configure_Layer_Channel.Pch_Data value = driver.Configure.Layer.Channel.Pch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:QPCH
				RsCmwCdma2kSig_Configure_Layer_Channel.Qpch_Data value = driver.Configure.Layer.Channel.Qpch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:SYNC
				RsCmwCdma2kSig_Configure_Layer_Channel.Sync_Data value = driver.Configure.Layer.Channel.Sync;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:FCH
				RsCmwCdma2kSig_Configure_Layer_Channel_Fch.Get_Data value = driver.Configure.Layer.Channel.Fch.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:FCH
				driver.Configure.Layer.Channel.Fch.Set(1, 1);
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:SCH
				RsCmwCdma2kSig_Configure_Layer_Channel_Sch.Get_Data value = driver.Configure.Layer.Channel.Sch.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:CHANnel:SCH
				driver.Configure.Layer.Channel.Sch.Set(1, 1);
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:FCH:FOFFset
				int value = driver.Configure.Layer.Fch.FreqOffset;
				driver.Configure.Layer.Fch.FreqOffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:FOFFset
				double value = driver.Configure.Layer.Sch.FreqOffset;
				driver.Configure.Layer.Sch.FreqOffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:MPPL
				RsCmwCdma2kSig_Configure_Layer_Sch.Mppl_Data value = driver.Configure.Layer.Sch.Mppl;
				driver.Configure.Layer.Sch.Mppl = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:FTYPe
				RsCmwCdma2kSig_Configure_Layer_Sch.Ftype_Data value = driver.Configure.Layer.Sch.Ftype;
				driver.Configure.Layer.Sch.Ftype = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:DRATe
				RsCmwCdma2kSig_Configure_Layer_Sch.Drate_Data value = driver.Configure.Layer.Sch.Drate;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:FSIZe
				RsCmwCdma2kSig_Configure_Layer_Sch.Fsize_Data value = driver.Configure.Layer.Sch.Fsize;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:SCH:CODing
				RsCmwCdma2kSig_Configure_Layer_Sch.Coding_Data value = driver.Configure.Layer.Sch.Coding;
				driver.Configure.Layer.Sch.Coding = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:PCH:CHANnel
				int value = driver.Configure.Layer.Pch.Channel;
				driver.Configure.Layer.Pch.Channel = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:PCH:LEVel
				double value = driver.Configure.Layer.Pch.Level;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:PCH:RATE
				foreach (PagingChannelRateEnum x in new PagingChannelRateEnum[] { PagingChannelRateEnum.R4K8, PagingChannelRateEnum.R9K6 })
				{
					PagingChannelRateEnum value = driver.Configure.Layer.Pch.Rate;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:QPCH:CHANnel
				int value = driver.Configure.Layer.Qpch.Channel;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:QPCH:LEVel
				double value = driver.Configure.Layer.Qpch.Level;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:QPCH:RATE
				foreach (PagingChannelRateEnum x in new PagingChannelRateEnum[] { PagingChannelRateEnum.R4K8, PagingChannelRateEnum.R9K6 })
				{
					driver.Configure.Layer.Qpch.Rate = x;
					PagingChannelRateEnum value = driver.Configure.Layer.Qpch.Rate;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:QPCH:IBIT<n>
				bool value = driver.Configure.Layer.Qpch.Ibit.Get(IndicatorRepCap.Default);
				value = driver.Configure.Layer.Qpch.Ibit.Get();
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:LAYer:QPCH:IBIT<n>
				driver.Configure.Layer.Qpch.Ibit.Set(false, IndicatorRepCap.Default);
				driver.Configure.Layer.Qpch.Ibit.Set(false);
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RPControl:PCBits
				foreach (PowerCtrlBitsEnum x in new PowerCtrlBitsEnum[] { PowerCtrlBitsEnum.ADOWn, PowerCtrlBitsEnum.AUP, PowerCtrlBitsEnum.AUTO, PowerCtrlBitsEnum.HOLD, PowerCtrlBitsEnum.PATTern, PowerCtrlBitsEnum.RTESt })
				{
					driver.Configure.RpControl.Pcbits = x;
					PowerCtrlBitsEnum value = driver.Configure.RpControl.Pcbits;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RPControl:SSIZe
				double value = driver.Configure.RpControl.Ssize;
				driver.Configure.RpControl.Ssize = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RPControl:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RpControl.Repetition = x;
					RepeatEnum value = driver.Configure.RpControl.Repetition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RPControl:RUN
				bool value = driver.Configure.RpControl.Run;
				driver.Configure.RpControl.Run = value;
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:RPControl:SEGMent<nr>:BITS
				SegmentBitsEnum value = driver.Configure.RpControl.Segment.Bits.Get(SegmentRepCap.Default);
				value = driver.Configure.RpControl.Segment.Bits.Get();
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:RPControl:SEGMent<nr>:BITS
				foreach (SegmentBitsEnum x in new SegmentBitsEnum[] { SegmentBitsEnum.ALTernating, SegmentBitsEnum.DOWN, SegmentBitsEnum.UP })
				{
					driver.Configure.RpControl.Segment.Bits.Set(x);
					driver.Configure.RpControl.Segment.Bits.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:RPControl:SEGMent<nr>:LENGth
				int value = driver.Configure.RpControl.Segment.Length.Get(SegmentRepCap.Default);
				value = driver.Configure.RpControl.Segment.Length.Get();
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:RPControl:SEGMent<nr>:LENGth
				driver.Configure.RpControl.Segment.Length.Set(1, SegmentRepCap.Default);
				driver.Configure.RpControl.Segment.Length.Set(1);
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:TSOurce
				foreach (TimeSourceEnum x in new TimeSourceEnum[] { TimeSourceEnum.CMWTime, TimeSourceEnum.DATE, TimeSourceEnum.SYNC })
				{
					driver.Configure.System.Tsource = x;
					TimeSourceEnum value = driver.Configure.System.Tsource;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:DATE
				RsCmwCdma2kSig_Configure_System.Date_Data value = driver.Configure.System.Date;
				driver.Configure.System.Date = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:TIME
				RsCmwCdma2kSig_Configure_System.Time_Data value = driver.Configure.System.Time;
				driver.Configure.System.Time = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:SYNC
				string value = driver.Configure.System.Sync;
				driver.Configure.System.Sync = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:ATIMe
				foreach (ApplyTimeAtEnum x in new ApplyTimeAtEnum[] { ApplyTimeAtEnum.EVER, ApplyTimeAtEnum.NEXT, ApplyTimeAtEnum.SUSO })
				{
					driver.Configure.System.Atime = x;
					ApplyTimeAtEnum value = driver.Configure.System.Atime;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:LSEConds
				int value = driver.Configure.System.Lseconds;
				driver.Configure.System.Lseconds = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:DAYLight
				bool value = driver.Configure.System.Daylight;
				driver.Configure.System.Daylight = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:LTOFfset:HEX
				string value = driver.Configure.System.LtOffset.Hex;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SYSTem:LTOFfset
				RsCmwCdma2kSig_Configure_System_LtOffset.Value_Data value = driver.Configure.System.LtOffset.Value;
				driver.Configure.System.LtOffset.Value = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:AMOC
				foreach (MocCallsAcceptModeEnum x in new MocCallsAcceptModeEnum[] { MocCallsAcceptModeEnum.ALL, MocCallsAcceptModeEnum.BUAW, MocCallsAcceptModeEnum.BUFW, MocCallsAcceptModeEnum.FSC1, MocCallsAcceptModeEnum.ICAW, MocCallsAcceptModeEnum.ICFW, MocCallsAcceptModeEnum.ICOR, MocCallsAcceptModeEnum.IGNR, MocCallsAcceptModeEnum.RERO, MocCallsAcceptModeEnum.ROAW, MocCallsAcceptModeEnum.ROFW, MocCallsAcceptModeEnum.ROOR, MocCallsAcceptModeEnum.SCL1 })
				{
					driver.Configure.Sconfig.Amoc = x;
					MocCallsAcceptModeEnum value = driver.Configure.Sconfig.Amoc;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:APCalls
				foreach (AcceptStateEnum x in new AcceptStateEnum[] { AcceptStateEnum.ACCept, AcceptStateEnum.REJect })
				{
					driver.Configure.Sconfig.ApCalls = x;
					AcceptStateEnum value = driver.Configure.Sconfig.ApCalls;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:LOOP:FRATe
				foreach (FrameRateEnum x in new FrameRateEnum[] { FrameRateEnum.EIGHth, FrameRateEnum.FULL, FrameRateEnum.HALF, FrameRateEnum.QUARter })
				{
					driver.Configure.Sconfig.Loop.Frate = x;
					FrameRateEnum value = driver.Configure.Sconfig.Loop.Frate;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:LOOP:PGENeration
				foreach (PatternGenerationEnum x in new PatternGenerationEnum[] { PatternGenerationEnum.FIX, PatternGenerationEnum.RAND })
				{
					driver.Configure.Sconfig.Loop.Pgeneration = x;
					PatternGenerationEnum value = driver.Configure.Sconfig.Loop.Pgeneration;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:LOOP:PATTern
				string value = driver.Configure.Sconfig.Loop.Pattern;
				driver.Configure.Sconfig.Loop.Pattern = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:VCODer
				foreach (VoiceCoderEnum x in new VoiceCoderEnum[] { VoiceCoderEnum.CODE, VoiceCoderEnum.ECHO })
				{
					driver.Configure.Sconfig.Speech.Vcoder = x;
					VoiceCoderEnum value = driver.Configure.Sconfig.Speech.Vcoder;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:EDELay
				double value = driver.Configure.Sconfig.Speech.Edelay;
				driver.Configure.Sconfig.Speech.Edelay = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:EVRC:EOPoint
				int value = driver.Configure.Sconfig.Speech.Evrc.Eopoint;
				driver.Configure.Sconfig.Speech.Evrc.Eopoint = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:EVRC:AERate
				foreach (AvgEncodingRateEnum x in new AvgEncodingRateEnum[] { AvgEncodingRateEnum.R48K, AvgEncodingRateEnum.R58K, AvgEncodingRateEnum.R62K, AvgEncodingRateEnum.R66K, AvgEncodingRateEnum.R70K, AvgEncodingRateEnum.R75K, AvgEncodingRateEnum.R85K, AvgEncodingRateEnum.R93K })
				{
					driver.Configure.Sconfig.Speech.Evrc.AeRate = x;
					AvgEncodingRateEnum value = driver.Configure.Sconfig.Speech.Evrc.AeRate;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:EVRC:RREStriction
				foreach (RateRestrictionEnum x in new RateRestrictionEnum[] { RateRestrictionEnum.AUTO, RateRestrictionEnum.EIGHth, RateRestrictionEnum.FULL, RateRestrictionEnum.HALF, RateRestrictionEnum.QUARter })
				{
					driver.Configure.Sconfig.Speech.Evrc.Rrestriction = x;
					RateRestrictionEnum value = driver.Configure.Sconfig.Speech.Evrc.Rrestriction;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:SPEech:EVRC:IVOCoder
				driver.Configure.Sconfig.Speech.Evrc.IvoCoder = false;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:FCH:PGENeration
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Fch.Pgeneration_Data value = driver.Configure.Sconfig.Tdata.Fch.Pgeneration;
				driver.Configure.Sconfig.Tdata.Fch.Pgeneration = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:FCH:PATTern
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Fch.Pattern_Data value = driver.Configure.Sconfig.Tdata.Fch.Pattern;
				driver.Configure.Sconfig.Tdata.Fch.Pattern = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:FCH:CBFRames
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Fch.CbFrames_Data value = driver.Configure.Sconfig.Tdata.Fch.CbFrames;
				driver.Configure.Sconfig.Tdata.Fch.CbFrames = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:FCH:TXON
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Fch.Txon_Data value = driver.Configure.Sconfig.Tdata.Fch.Txon;
				driver.Configure.Sconfig.Tdata.Fch.Txon = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:FCH:TXOFf
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Fch.Txoff_Data value = driver.Configure.Sconfig.Tdata.Fch.Txoff;
				driver.Configure.Sconfig.Tdata.Fch.Txoff = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:SCH:PGENeration
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Sch.Pgeneration_Data value = driver.Configure.Sconfig.Tdata.Sch.Pgeneration;
				driver.Configure.Sconfig.Tdata.Sch.Pgeneration = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:SCH:PATTern
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Sch.Pattern_Data value = driver.Configure.Sconfig.Tdata.Sch.Pattern;
				driver.Configure.Sconfig.Tdata.Sch.Pattern = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:SCH:CBFRames
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Sch.CbFrames_Data value = driver.Configure.Sconfig.Tdata.Sch.CbFrames;
				driver.Configure.Sconfig.Tdata.Sch.CbFrames = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:SCH:TXON
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Sch.Txon_Data value = driver.Configure.Sconfig.Tdata.Sch.Txon;
				driver.Configure.Sconfig.Tdata.Sch.Txon = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:TDATa:SCH:TXOFf
				RsCmwCdma2kSig_Configure_Sconfig_Tdata_Sch.Txoff_Data value = driver.Configure.Sconfig.Tdata.Sch.Txoff;
				driver.Configure.Sconfig.Tdata.Sch.Txoff = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:PDATa:ITIMer
				int value = driver.Configure.Sconfig.Pdata.Itimer;
				driver.Configure.Sconfig.Pdata.Itimer = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SCONfig:PDATa:DTIMer
				double value = driver.Configure.Sconfig.Pdata.Dtimer;
				driver.Configure.Sconfig.Pdata.Dtimer = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:SID
				int value = driver.Configure.Network.System.Sid;
				driver.Configure.Network.System.Sid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:PREVision
				int value = driver.Configure.Network.System.Prevision;
				driver.Configure.Network.System.Prevision = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:MPRevision
				int value = driver.Configure.Network.System.Mprevision;
				driver.Configure.Network.System.Mprevision = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:BSID
				int value = driver.Configure.Network.System.Bsid;
				driver.Configure.Network.System.Bsid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:AWIN
				RsCmwCdma2kSig_Configure_Network_System_Awin.Get_Data value = driver.Configure.Network.System.Awin.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:AWIN
				driver.Configure.Network.System.Awin.Set(1);				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:NWIN
				RsCmwCdma2kSig_Configure_Network_System_Nwin.Get_Data value = driver.Configure.Network.System.Nwin.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:NWIN
				driver.Configure.Network.System.Nwin.Set(1);				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:RWIN
				RsCmwCdma2kSig_Configure_Network_System_Rwin.Get_Data value = driver.Configure.Network.System.Rwin.Get();				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:SYSTem:RWIN
				driver.Configure.Network.System.Rwin.Set(1);				
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:PNOFfset
				int value = driver.Configure.Network.Property.PnOffset;
				driver.Configure.Network.Property.PnOffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:CLDTime
				int value = driver.Configure.Network.Property.CldTime;
				driver.Configure.Network.Property.CldTime = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:PRTimeout
				int value = driver.Configure.Network.Property.PrTimeout;
				driver.Configure.Network.Property.PrTimeout = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:LTOFfset
				int value = driver.Configure.Network.Property.LtOffset;
				driver.Configure.Network.Property.LtOffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:DLSavings
				bool value = driver.Configure.Network.Property.DlSavings;
				driver.Configure.Network.Property.DlSavings = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:LATitude
				RsCmwCdma2kSig_Configure_Network_Property.Latitude_Data value = driver.Configure.Network.Property.Latitude;
				driver.Configure.Network.Property.Latitude = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PROPerty:LONGitude
				RsCmwCdma2kSig_Configure_Network_Property.Longitude_Data value = driver.Configure.Network.Property.Longitude;
				driver.Configure.Network.Property.Longitude = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:IDENtity:NID
				int value = driver.Configure.Network.Identity.Nid;
				driver.Configure.Network.Identity.Nid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:IDENtity:MCC
				int value = driver.Configure.Network.Identity.Mcc;
				driver.Configure.Network.Identity.Mcc = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:IDENtity:IMSI
				int value = driver.Configure.Network.Identity.Imsi;
				driver.Configure.Network.Identity.Imsi = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:IDENtity:UWCard
				bool value = driver.Configure.Network.Identity.Uwcard;
				driver.Configure.Network.Identity.Uwcard = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:MSETtings:MCC
				int value = driver.Configure.Network.Msettings.Mcc;
				driver.Configure.Network.Msettings.Mcc = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:MSETtings:PLCM
				foreach (PlcmDerivationEnum x in new PlcmDerivationEnum[] { PlcmDerivationEnum.ESN, PlcmDerivationEnum.MEID })
				{
					driver.Configure.Network.Msettings.Plcm = x;
					PlcmDerivationEnum value = driver.Configure.Network.Msettings.Plcm;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:MSETtings:NMSI
				string value = driver.Configure.Network.Msettings.Nmsi;
				driver.Configure.Network.Msettings.Nmsi = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:MSETtings:UMRData
				bool value = driver.Configure.Network.Msettings.Umrdata;
				driver.Configure.Network.Msettings.Umrdata = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:MSETtings:IMIN:USER
				string value = driver.Configure.Network.Msettings.Imin.User;
				driver.Configure.Network.Msettings.Imin.User = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:CINDicator:CID:ENABle
				bool value = driver.Configure.Network.Cindicator.Cid.Enable;
				driver.Configure.Network.Cindicator.Cid.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:CINDicator:CID:PINDicator
				foreach (CallerIdPresentationEnum x in new CallerIdPresentationEnum[] { CallerIdPresentationEnum.NNAV, CallerIdPresentationEnum.PAL, CallerIdPresentationEnum.PRES })
				{
					driver.Configure.Network.Cindicator.Cid.Pindicator = x;
					CallerIdPresentationEnum value = driver.Configure.Network.Cindicator.Cid.Pindicator;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:CINDicator:CID
				string value = driver.Configure.Network.Cindicator.Cid.Value;
				driver.Configure.Network.Cindicator.Cid.Value = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PCHannel:RATE
				foreach (PagingChannelRateEnum x in new PagingChannelRateEnum[] { PagingChannelRateEnum.R4K8, PagingChannelRateEnum.R9K6 })
				{
					driver.Configure.Network.Pchannel.Rate = x;
					PagingChannelRateEnum value = driver.Configure.Network.Pchannel.Rate;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PCHannel:SCINdex
				int value = driver.Configure.Network.Pchannel.ScIndex;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PCHannel:MSCindex
				int value = driver.Configure.Network.Pchannel.MscIndex;
				driver.Configure.Network.Pchannel.MscIndex = value;
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:NETWork:PCHannel:BSCindex
				int value = driver.Configure.Network.Pchannel.BscIndex;
				driver.Configure.Network.Pchannel.BscIndex = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:PCHannel:PRMS
				bool value = driver.Configure.Network.Pchannel.Prms;
				driver.Configure.Network.Pchannel.Prms = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:DBASed
				double value = driver.Configure.Network.Registration.Dbased;
				driver.Configure.Network.Registration.Dbased = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:TBASed
				double value = driver.Configure.Network.Registration.Tbased;
				driver.Configure.Network.Registration.Tbased = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:HOME
				bool value = driver.Configure.Network.Registration.Home;
				driver.Configure.Network.Registration.Home = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:FSID
				bool value = driver.Configure.Network.Registration.Fsid;
				driver.Configure.Network.Registration.Fsid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:FNID
				bool value = driver.Configure.Network.Registration.Fnid;
				driver.Configure.Network.Registration.Fnid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:PUP
				bool value = driver.Configure.Network.Registration.Pup;
				driver.Configure.Network.Registration.Pup = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:PDOWn
				bool value = driver.Configure.Network.Registration.Pdown;
				driver.Configure.Network.Registration.Pdown = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:REGistration:PARameter
				bool value = driver.Configure.Network.Registration.Parameter;
				driver.Configure.Network.Registration.Parameter = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:MODE
				foreach (AccessProbeModeEnum x in new AccessProbeModeEnum[] { AccessProbeModeEnum.ACK, AccessProbeModeEnum.IGN })
				{
					driver.Configure.Network.Aprobes.Mode = x;
					AccessProbeModeEnum value = driver.Configure.Network.Aprobes.Mode;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:NOFFset
				int value = driver.Configure.Network.Aprobes.Noffset;
				driver.Configure.Network.Aprobes.Noffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:IOFFset
				int value = driver.Configure.Network.Aprobes.Ioffset;
				driver.Configure.Network.Aprobes.Ioffset = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:PINCrement
				int value = driver.Configure.Network.Aprobes.Pincrement;
				driver.Configure.Network.Aprobes.Pincrement = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:PPSequence
				int value = driver.Configure.Network.Aprobes.PpSequence;
				driver.Configure.Network.Aprobes.PpSequence = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:SPATtempt:RSP
				int value = driver.Configure.Network.Aprobes.SpAttempt.Rsp;
				driver.Configure.Network.Aprobes.SpAttempt.Rsp = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:NETWork:APRobes:SPATtempt:REQ
				int value = driver.Configure.Network.Aprobes.SpAttempt.Req;
				driver.Configure.Network.Aprobes.SpAttempt.Req = value;
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:CONNection:EDAU:ENABle
				bool value = driver.Configure.Connection.Edau.Enable;
				driver.Configure.Connection.Edau.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:CONNection:EDAU:NSEGment
				foreach (NetworkSegmentEnum x in new NetworkSegmentEnum[] { NetworkSegmentEnum.A, NetworkSegmentEnum.B, NetworkSegmentEnum.C })
				{
					driver.Configure.Connection.Edau.Nsegment = x;
					NetworkSegmentEnum value = driver.Configure.Connection.Edau.Nsegment;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:CONNection:EDAU:NID
				int value = driver.Configure.Connection.Edau.Nid;
				driver.Configure.Connection.Edau.Nid = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:DNUMber
				string value = driver.Configure.MsInfo.Dnumber;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:GECall
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					YesNoStatusEnum value = driver.Configure.MsInfo.Gecall;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:PREVision
				int value = driver.Configure.MsInfo.Prevision;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:MCC
				int value = driver.Configure.MsInfo.Mcc;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:NMSI
				string value = driver.Configure.MsInfo.Nmsi;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:MSUPport
				bool value = driver.Configure.MsInfo.Msupport;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:ESN
				string value = driver.Configure.MsInfo.Esn;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:MEID
				string value = driver.Configure.MsInfo.Meid;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:MSINfo:EIRP
				int value = driver.Configure.MsInfo.Eirp;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:ENABle
				bool value = driver.Configure.Capabilities.Enable;
				driver.Configure.Capabilities.Enable = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:BCSupport
				List<bool> value = driver.Configure.Capabilities.BcSupport;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:SCSupport
				List<bool> value = driver.Configure.Capabilities.ScSupport;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:TERMinal
				RsCmwCdma2kSig_Configure_Capabilities.Terminal_Data value = driver.Configure.Capabilities.Terminal;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:GLOCation
				RsCmwCdma2kSig_Configure_Capabilities.Glocation_Data value = driver.Configure.Capabilities.Glocation;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:WLL
				RsCmwCdma2kSig_Configure_Capabilities.Wll_Data value = driver.Configure.Capabilities.Wll;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:AUTHentic
				RsCmwCdma2kSig_Configure_Capabilities.Authentic_Data value = driver.Configure.Capabilities.Authentic;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:COMMon
				RsCmwCdma2kSig_Configure_Capabilities.Common_Data value = driver.Configure.Capabilities.Common;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:RLPinfo
				RsCmwCdma2kSig_Configure_Capabilities.RlpInfo_Data value = driver.Configure.Capabilities.RlpInfo;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:SOSupport:FFCH
				RsCmwCdma2kSig_Configure_Capabilities_SoSupport.Ffch_Data value = driver.Configure.Capabilities.SoSupport.Ffch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:SOSupport:RFCH
				RsCmwCdma2kSig_Configure_Capabilities_SoSupport.Rfch_Data value = driver.Configure.Capabilities.SoSupport.Rfch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:MUXSupport:FWD
				RsCmwCdma2kSig_Configure_Capabilities_MuxSupport.Fwd_Data value = driver.Configure.Capabilities.MuxSupport.Fwd;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:MUXSupport:REV
				RsCmwCdma2kSig_Configure_Capabilities_MuxSupport.Rev_Data value = driver.Configure.Capabilities.MuxSupport.Rev;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:ROAMing:OCLass
				int value = driver.Configure.Capabilities.Roaming.Oclass;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:ROAMing:HOME
				foreach (SupportedEnum x in new SupportedEnum[] { SupportedEnum.NSUP, SupportedEnum.SUPP })
				{
					SupportedEnum value = driver.Configure.Capabilities.Roaming.Home;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:ROAMing:SID
				RsCmwCdma2kSig_Configure_Capabilities_Roaming.Sid_Data value = driver.Configure.Capabilities.Roaming.Sid;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:ROAMing:NID
				RsCmwCdma2kSig_Configure_Capabilities_Roaming.Nid_Data value = driver.Configure.Capabilities.Roaming.Nid;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:FDRSupport:FCH
				RsCmwCdma2kSig_Configure_Capabilities_FdrSupport.Fch_Data value = driver.Configure.Capabilities.FdrSupport.Fch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:FDRSupport:DCCH
				RsCmwCdma2kSig_Configure_Capabilities_FdrSupport.Dcch_Data value = driver.Configure.Capabilities.FdrSupport.Dcch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:FDRSupport:SCH
				RsCmwCdma2kSig_Configure_Capabilities_FdrSupport.Sch_Data value = driver.Configure.Capabilities.FdrSupport.Sch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:VRSupport:SCH
				RsCmwCdma2kSig_Configure_Capabilities_VrSupport.Sch_Data value = driver.Configure.Capabilities.VrSupport.Sch;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:CAPabilities:VRSupport:MSBits
				RsCmwCdma2kSig_Configure_Capabilities_VrSupport.Msbits_Data value = driver.Configure.Capabilities.VrSupport.Msbits;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:HANDoff:BCLass
				foreach (BandClassEnum x in new BandClassEnum[] { BandClassEnum.AWS, BandClassEnum.B18M, BandClassEnum.IEXT, BandClassEnum.IM2K, BandClassEnum.JTAC, BandClassEnum.KCEL, BandClassEnum.KPCS, BandClassEnum.LBANd, BandClassEnum.LO7C, BandClassEnum.N45T, BandClassEnum.NA7C, BandClassEnum.NA8S, BandClassEnum.NA9C, BandClassEnum.NAPC, BandClassEnum.PA4M, BandClassEnum.PA8M, BandClassEnum.PS7C, BandClassEnum.SBANd, BandClassEnum.TACS, BandClassEnum.U25B, BandClassEnum.U25F, BandClassEnum.USC, BandClassEnum.USPC })
				{
					driver.Configure.Handoff.Bclass = x;
					BandClassEnum value = driver.Configure.Handoff.Bclass;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:HANDoff:CHANnel
				int value = driver.Configure.Handoff.Channel;
				driver.Configure.Handoff.Channel = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:REConfigure:LAYer:RCONfig
				foreach (RadioConfigEnum x in new RadioConfigEnum[] { RadioConfigEnum.F1R1, RadioConfigEnum.F2R2, RadioConfigEnum.F3R3, RadioConfigEnum.F4R3, RadioConfigEnum.F5R4 })
				{
					driver.Configure.Reconfigure.Layer.Rconfig = x;
					RadioConfigEnum value = driver.Configure.Reconfigure.Layer.Rconfig;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:REConfigure:LAYer:SOPTion:FIRSt
				foreach (ServiceOptionEnum x in new ServiceOptionEnum[] { ServiceOptionEnum.SO1, ServiceOptionEnum.SO17, ServiceOptionEnum.SO2, ServiceOptionEnum.SO3, ServiceOptionEnum.SO32, ServiceOptionEnum.SO33, ServiceOptionEnum.SO55, ServiceOptionEnum.SO68, ServiceOptionEnum.SO70, ServiceOptionEnum.SO73, ServiceOptionEnum.SO8000, ServiceOptionEnum.SO9 })
				{
					driver.Configure.Reconfigure.Layer.Soption.First = x;
					ServiceOptionEnum value = driver.Configure.Reconfigure.Layer.Soption.First;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:PREConfigure:LAYer:RCONfig
				foreach (RadioConfigEnum x in new RadioConfigEnum[] { RadioConfigEnum.F1R1, RadioConfigEnum.F2R2, RadioConfigEnum.F3R3, RadioConfigEnum.F4R3, RadioConfigEnum.F5R4 })
				{
					driver.Configure.Preconfigure.Layer.Rconfig = x;
					RadioConfigEnum value = driver.Configure.Preconfigure.Layer.Rconfig;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:PREConfigure:LAYer:SOPTion:FIRSt
				foreach (ServiceOptionEnum x in new ServiceOptionEnum[] { ServiceOptionEnum.SO1, ServiceOptionEnum.SO17, ServiceOptionEnum.SO2, ServiceOptionEnum.SO3, ServiceOptionEnum.SO32, ServiceOptionEnum.SO33, ServiceOptionEnum.SO55, ServiceOptionEnum.SO68, ServiceOptionEnum.SO70, ServiceOptionEnum.SO73, ServiceOptionEnum.SO8000, ServiceOptionEnum.SO9 })
				{
					driver.Configure.Preconfigure.Layer.Soption.First = x;
					ServiceOptionEnum value = driver.Configure.Preconfigure.Layer.Soption.First;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INComing:CSSMs
				bool value = driver.Configure.Sms.Incoming.CsSms;
				driver.Configure.Sms.Incoming.CsSms = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INComing:FILE:INFO
				RsCmwCdma2kSig_Configure_Sms_Incoming_File.Info_Data value = driver.Configure.Sms.Incoming.File.Info;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INComing:FILE
				string value = driver.Configure.Sms.Incoming.File.Value;
				driver.Configure.Sms.Incoming.File.Value = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:SMEThod
				foreach (SmsSendMethodEnum x in new SmsSendMethodEnum[] { SmsSendMethodEnum.ACH, SmsSendMethodEnum.PCH, SmsSendMethodEnum.SO14, SmsSendMethodEnum.SO6, SmsSendMethodEnum.TCH })
				{
					driver.Configure.Sms.Outgoing.Smethod = x;
					SmsSendMethodEnum value = driver.Configure.Sms.Outgoing.Smethod;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:ACKNowledge
				bool value = driver.Configure.Sms.Outgoing.Acknowledge;
				driver.Configure.Sms.Outgoing.Acknowledge = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:ATSTamp
				bool value = driver.Configure.Sms.Outgoing.Atstamp;
				driver.Configure.Sms.Outgoing.Atstamp = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:LHANdling
				foreach (LongSmsHandlingEnum x in new LongSmsHandlingEnum[] { LongSmsHandlingEnum.MSMS, LongSmsHandlingEnum.TRUNcate })
				{
					driver.Configure.Sms.Outgoing.Lhandling = x;
					LongSmsHandlingEnum value = driver.Configure.Sms.Outgoing.Lhandling;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:MESHandling
				foreach (MessageHandlingEnum x in new MessageHandlingEnum[] { MessageHandlingEnum.FILE, MessageHandlingEnum.INTernal })
				{
					driver.Configure.Sms.Outgoing.MesHandling = x;
					MessageHandlingEnum value = driver.Configure.Sms.Outgoing.MesHandling;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:INTernal
				string value = driver.Configure.Sms.Outgoing.Internal;
				driver.Configure.Sms.Outgoing.Internal = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:FILE:INFO
				RsCmwCdma2kSig_Configure_Sms_Outgoing_File.Info_Data value = driver.Configure.Sms.Outgoing.File.Info;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:OUTGoing:FILE
				string value = driver.Configure.Sms.Outgoing.File.Value;
				driver.Configure.Sms.Outgoing.File.Value = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INFO:LSMessage
				RsCmwCdma2kSig_Configure_Sms_Info.LsMessage_Data value = driver.Configure.Sms.Info.LsMessage;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INFO:LRMessage:RFLag
				bool value = driver.Configure.Sms.Info.LrMessage.Rflag;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:INFO:LRMessage
				RsCmwCdma2kSig_Configure_Sms_Info_LrMessage.Value_Data value = driver.Configure.Sms.Info.LrMessage.Value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:BROadcast:CMAS
				bool value = driver.Configure.Sms.Broadcast.Cmas;
				driver.Configure.Sms.Broadcast.Cmas = value;
			}
			{	// CONFigure:CDMA:SIGNaling<instance>:SMS:BROadcast:WEA
				bool value = driver.Configure.Sms.Broadcast.Wea;
				driver.Configure.Sms.Broadcast.Wea = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:BROadcast:INTernal
				string value = driver.Configure.Sms.Broadcast.Internal;
				driver.Configure.Sms.Broadcast.Internal = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:BROadcast:LANGuage
				foreach (LanguageEnum x in new LanguageEnum[] { LanguageEnum.AFRikaans, LanguageEnum.ARABic, LanguageEnum.BAHasa, LanguageEnum.BENGali, LanguageEnum.CHINese, LanguageEnum.CZECh, LanguageEnum.DANish, LanguageEnum.DUTCh, LanguageEnum.ENGLish, LanguageEnum.FINNish, LanguageEnum.FRENch, LanguageEnum.GERMan, LanguageEnum.GREek, LanguageEnum.GUJarati, LanguageEnum.HAUSa, LanguageEnum.HEBRew, LanguageEnum.HINDi, LanguageEnum.HUNGarian, LanguageEnum.ICELandic, LanguageEnum.ITALian, LanguageEnum.JAPanese, LanguageEnum.KANNada, LanguageEnum.KORean, LanguageEnum.MALayalam, LanguageEnum.NORWegian, LanguageEnum.ORIYa, LanguageEnum.POLish, LanguageEnum.PORTuguese, LanguageEnum.PUNJabi, LanguageEnum.RUSSian, LanguageEnum.SPANish, LanguageEnum.SWAHili, LanguageEnum.SWEDish, LanguageEnum.TAGalog, LanguageEnum.TAMil, LanguageEnum.TELugu, LanguageEnum.THAI, LanguageEnum.TURKish, LanguageEnum.UNDefined, LanguageEnum.URDU, LanguageEnum.VIETnamese })
				{
					driver.Configure.Sms.Broadcast.Language = x;
					LanguageEnum value = driver.Configure.Sms.Broadcast.Language;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:BROadcast:PRIority
				foreach (PriorityBenum x in new PriorityBenum[] { PriorityBenum.EMERgency, PriorityBenum.INTeractive, PriorityBenum.NORMal, PriorityBenum.URGent })
				{
					driver.Configure.Sms.Broadcast.Priority = x;
					PriorityBenum value = driver.Configure.Sms.Broadcast.Priority;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:SMS:BROadcast:SERVice:CATegory
				string value = driver.Configure.Sms.Broadcast.Service.Category;
				driver.Configure.Sms.Broadcast.Service.Category = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:URATe
				double value = driver.Configure.RxQuality.Urate;
				driver.Configure.RxQuality.Urate = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:WINDowsize
				int value = driver.Configure.RxQuality.WindowSize;
				driver.Configure.RxQuality.WindowSize = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RESult:FERFch
				bool value = driver.Configure.RxQuality.Result.Ferfch;
				driver.Configure.RxQuality.Result.Ferfch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RESult:FERSch
				bool value = driver.Configure.RxQuality.Result.Fersch;
				driver.Configure.RxQuality.Result.Fersch = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RESult:RLP
				bool value = driver.Configure.RxQuality.Result.Rlp;
				driver.Configure.RxQuality.Result.Rlp = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RESult:SPEech
				bool value = driver.Configure.RxQuality.Result.Speech;
				driver.Configure.RxQuality.Result.Speech = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RESult:PSTRength
				bool value = driver.Configure.RxQuality.Result.Pstrength;
				driver.Configure.RxQuality.Result.Pstrength = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERFch:TOUT
				double value = driver.Configure.RxQuality.Ferfch.Timeout;
				driver.Configure.RxQuality.Ferfch.Timeout = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERFch:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Ferfch.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Ferfch.Repetition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERFch:SCONdition
				foreach (StopConditionBenum x in new StopConditionBenum[] { StopConditionBenum.ALEXeeded, StopConditionBenum.MCLexceeded, StopConditionBenum.MFER, StopConditionBenum.NONE })
				{
					driver.Configure.RxQuality.Ferfch.Scondition = x;
					StopConditionBenum value = driver.Configure.RxQuality.Ferfch.Scondition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERFch:FRAMes
				int value = driver.Configure.RxQuality.Ferfch.Frames;
				driver.Configure.RxQuality.Ferfch.Frames = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERSch:TOUT
				double value = driver.Configure.RxQuality.Fersch.Timeout;
				driver.Configure.RxQuality.Fersch.Timeout = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERSch:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Fersch.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Fersch.Repetition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERSch:SCONdition
				foreach (StopConditionBenum x in new StopConditionBenum[] { StopConditionBenum.ALEXeeded, StopConditionBenum.MCLexceeded, StopConditionBenum.MFER, StopConditionBenum.NONE })
				{
					driver.Configure.RxQuality.Fersch.Scondition = x;
					StopConditionBenum value = driver.Configure.RxQuality.Fersch.Scondition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:FERSch:FRAMes
				int value = driver.Configure.RxQuality.Fersch.Frames;
				driver.Configure.RxQuality.Fersch.Frames = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:RSTatistics
				driver.Configure.RxQuality.Rstatistics.Set();
				driver.Configure.RxQuality.Rstatistics.SetAndWait();
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:PSTRength:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Pstrength.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Pstrength.Repetition;
				}
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:PSTRength:URATe
				double value = driver.Configure.RxQuality.Pstrength.Urate;
				driver.Configure.RxQuality.Pstrength.Urate = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:LIMit:FERFch:MFER
				double value = driver.Configure.RxQuality.Limit.Ferfch.Mfer;
				driver.Configure.RxQuality.Limit.Ferfch.Mfer = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:LIMit:FERFch:CLEVel
				double value = driver.Configure.RxQuality.Limit.Ferfch.Clevel;
				driver.Configure.RxQuality.Limit.Ferfch.Clevel = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:LIMit:FERSch:MFER
				double value = driver.Configure.RxQuality.Limit.Fersch.Mfer;
				driver.Configure.RxQuality.Limit.Fersch.Mfer = value;
			}
			{	// CONFigure:CDMA:SIGNaling<Instance>:RXQuality:LIMit:FERSch:CLEVel
				double value = driver.Configure.RxQuality.Limit.Fersch.Clevel;
				driver.Configure.RxQuality.Limit.Fersch.Clevel = value;
			}
			{	// SENSe:CDMA:SIGNaling<instance>:CVINfo
				RsCmwCdma2kSig_Sense.CvInfo_Data value = driver.Sense.CvInfo;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:TEST:RX:POWer:STATe
				double value = driver.Sense.Test.Rx.Power.State;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:BSADdress:IPV<n>
				string value = driver.Sense.BsAddress.Ipv;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:ATADdress:IPV<n>
				string value = driver.Sense.AtAddress.GetIpv(IpAddressRepCap.Version4);
				value = driver.Sense.AtAddress.GetIpv();
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:DUNSegmented
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.DunSegmented_Data value = driver.Sense.RxQuality.Rlp.DunSegmented;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:DSEGmented
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Dsegmented_Data value = driver.Sense.RxQuality.Rlp.Dsegmented;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:FILL
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Fill_Data value = driver.Sense.RxQuality.Rlp.Fill;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:IDLE
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Idle_Data value = driver.Sense.RxQuality.Rlp.Idle;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:NAK
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Nak_Data value = driver.Sense.RxQuality.Rlp.Nak;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:SYNC
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Sync_Data value = driver.Sense.RxQuality.Rlp.Sync;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:ACK
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Ack_Data value = driver.Sense.RxQuality.Rlp.Ack;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:SACK
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Sack_Data value = driver.Sense.RxQuality.Rlp.Sack;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:BDATa
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Bdata_Data value = driver.Sense.RxQuality.Rlp.Bdata;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:CDATa
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Cdata_Data value = driver.Sense.RxQuality.Rlp.Cdata;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:DDATa
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Ddata_Data value = driver.Sense.RxQuality.Rlp.Ddata;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:REASembly
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Reasembly_Data value = driver.Sense.RxQuality.Rlp.Reasembly;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:BLANk
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Blank_Data value = driver.Sense.RxQuality.Rlp.Blank;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:INValid
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Invalid_Data value = driver.Sense.RxQuality.Rlp.Invalid;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:SUMMary
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Summary_Data value = driver.Sense.RxQuality.Rlp.Summary;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:PPPTotal
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.PppTotal_Data value = driver.Sense.RxQuality.Rlp.PppTotal;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:DRATe
				RsCmwCdma2kSig_Sense_RxQuality_Rlp.Drate_Data value = driver.Sense.RxQuality.Rlp.Drate;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:RLP:STATe
				string value = driver.Sense.RxQuality.Rlp.State;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:THRoughput
				RsCmwCdma2kSig_Sense_RxQuality_Speech.Throughput_Data value = driver.Sense.RxQuality.Speech.Throughput;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:STATe
				string value = driver.Sense.RxQuality.Speech.State;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:BLANked:PERCent
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Blanked.Percent_Data value = driver.Sense.RxQuality.Speech.Blanked.Percent;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:BLANked
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Blanked.Value_Data value = driver.Sense.RxQuality.Speech.Blanked.Value;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:EIGHt:PERCent
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Eight.Percent_Data value = driver.Sense.RxQuality.Speech.Eight.Percent;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:EIGHt
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Eight.Value_Data value = driver.Sense.RxQuality.Speech.Eight.Value;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:QUARter:PERCent
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Quarter.Percent_Data value = driver.Sense.RxQuality.Speech.Quarter.Percent;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:QUARter
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Quarter.Value_Data value = driver.Sense.RxQuality.Speech.Quarter.Value;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:HALF:PERCent
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Half.Percent_Data value = driver.Sense.RxQuality.Speech.Half.Percent;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:HALF
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Half.Value_Data value = driver.Sense.RxQuality.Speech.Half.Value;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:FULL:PERCent
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Full.Percent_Data value = driver.Sense.RxQuality.Speech.Full.Percent;
			}
			{	// SENSe:CDMA:SIGNaling<Instance>:RXQuality:SPEech:FULL
				RsCmwCdma2kSig_Sense_RxQuality_Speech_Full.Value_Data value = driver.Sense.RxQuality.Speech.Full.Value;
			}
			{	// SENSe:CDMA:SIGNaling<instance>:ELOG:LAST
				RsCmwCdma2kSig_Sense_Elog.Last_Data value = driver.Sense.Elog.Last;
			}
			{	// SENSe:CDMA:SIGNaling<instance>:ELOG:ALL
				RsCmwCdma2kSig_Sense_Elog.All_Data value = driver.Sense.Elog.All;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>
				RsCmwCdma2kSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:SCELl
				RsCmwCdma2kSig_Route_Scenario.Scell_Data value = driver.Route.Scenario.Scell;
				driver.Route.Scenario.Scell = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:HMODe
				RsCmwCdma2kSig_Route_Scenario.Hmode_Data value = driver.Route.Scenario.Hmode;
				driver.Route.Scenario.Hmode = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:HMLite
				RsCmwCdma2kSig_Route_Scenario.Hmlite_Data value = driver.Route.Scenario.Hmlite;
				driver.Route.Scenario.Hmlite = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario
				RsCmwCdma2kSig_Route_Scenario.Value_Data value = driver.Route.Scenario.Value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:SCFading[:EXTernal]
				RsCmwCdma2kSig_Route_Scenario_ScFading.External_Data value = driver.Route.Scenario.ScFading.External;
				driver.Route.Scenario.ScFading.External = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:SCFading:INTernal
				RsCmwCdma2kSig_Route_Scenario_ScFading.Internal_Data value = driver.Route.Scenario.ScFading.Internal;
				driver.Route.Scenario.ScFading.Internal = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:HMFading[:EXTernal]
				RsCmwCdma2kSig_Route_Scenario_HmFading.External_Data value = driver.Route.Scenario.HmFading.External;
				driver.Route.Scenario.HmFading.External = value;
			}
			{	// ROUTe:CDMA:SIGNaling<Instance>:SCENario:HMFading:INTernal
				RsCmwCdma2kSig_Route_Scenario_HmFading.Internal_Data value = driver.Route.Scenario.HmFading.Internal;
				driver.Route.Scenario.HmFading.Internal = value;
			}
			{	// SOURce:CDMA:SIGNaling<Instance>:STATe:ALL
				RsCmwCdma2kSig_Source_State.All_Data value = driver.Source.State.All;
			}
			{	// SOURce:CDMA:SIGNaling<Instance>:STATe
				bool value = driver.Source.State.Value;
				driver.Source.State.Value = value;
			}
			{	// CALL:CDMA:SIGNaling<Instance>:SOPTion<So>:ACTion
				foreach (CsActionEnum x in new CsActionEnum[] { CsActionEnum.BROadcast, CsActionEnum.CONNect, CsActionEnum.DISConnect, CsActionEnum.HANDoff, CsActionEnum.SMS, CsActionEnum.UNRegister })
				{
					driver.Call.Soption.Action = x;					
				}
			}
			{	// CALL:CDMA:SIGNaling<Instance>:HANDoff:STARt
				driver.Call.Handoff.Start();
				driver.Call.Handoff.StartAndWait();
			}
			{	// CALL:CDMA:SIGNaling<Instance>:REConfigure:STARt
				driver.Call.Reconfigure.Start();
				driver.Call.Reconfigure.StartAndWait();
			}
			{	// CALL:CDMA:SIGNaling<Instance>:OTASp:SEND:TRANsmit
				driver.Call.Otasp.Send.Transmit = new byte[] { 0, 1, 2, 3, 4 };
			}
			{	// CALL:CDMA:SIGNaling<Instance>:OTASp:SEND:MODE
				foreach (OtaspSendMethodAenum x in new OtaspSendMethodAenum[] { OtaspSendMethodAenum.NONE, OtaspSendMethodAenum.SO18, OtaspSendMethodAenum.SO19 })
				{
					driver.Call.Otasp.Send.Mode = x;
					OtaspSendMethodAenum value = driver.Call.Otasp.Send.Mode;
				}
			}
			{	// CALL:CDMA:SIGNaling<Instance>:OTASp:SEND:STATus
				RsCmwCdma2kSig_Call_Otasp_Send.Status_Data value = driver.Call.Otasp.Send.Status;
			}
			{	// CALL:CDMA:SIGNaling<Instance>:OTASp:RECeive:WATermark
				RsCmwCdma2kSig_Call_Otasp_Receive.Watermark_Data value = driver.Call.Otasp.Receive.Watermark;
			}
			{	// CALL:CDMA:SIGNaling<Instance>:OTASp:RECeive:RESet
				driver.Call.Otasp.Receive.Reset();
				driver.Call.Otasp.Receive.ResetAndWait();
			}
			{	// CALL:CDMA:SIGNaling<Instance>:PDM:SEND:TRANsmit
				driver.Call.Pdm.Send.Transmit = new byte[] { 0, 1, 2, 3, 4 };
			}
			{	// CALL:CDMA:SIGNaling<Instance>:PDM:SEND:MODE
				foreach (PdmSendMethodAenum x in new PdmSendMethodAenum[] { PdmSendMethodAenum.NONE, PdmSendMethodAenum.PCH, PdmSendMethodAenum.SO35, PdmSendMethodAenum.SO36 })
				{
					driver.Call.Pdm.Send.Mode = x;
					PdmSendMethodAenum value = driver.Call.Pdm.Send.Mode;
				}
			}
			{	// CALL:CDMA:SIGNaling<Instance>:PDM:SEND:STATus
				RsCmwCdma2kSig_Call_Pdm_Send.Status_Data value = driver.Call.Pdm.Send.Status;
			}
			{	// CALL:CDMA:SIGNaling<Instance>:PDM:RECeive:WATermark
				RsCmwCdma2kSig_Call_Pdm_Receive.Watermark_Data value = driver.Call.Pdm.Receive.Watermark;
			}
			{	// CALL:CDMA:SIGNaling<Instance>:PDM:RECeive:RESet
				driver.Call.Pdm.Receive.Reset();
				driver.Call.Pdm.Receive.ResetAndWait();
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:SOPTion<So>:STATe
				CsStateEnum value = driver.Soption.State.Fetch();				
			}
			{	// CLEan:CDMA:SIGNaling<Instance>:SMS:INComing:INFO
				driver.Clean.Sms.Incoming.Info.Set();
				driver.Clean.Sms.Incoming.Info.SetAndWait();
			}
			{	// INITiate:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERFch
				driver.RxQuality.Tdata.Ferfch.Initiate();
				driver.RxQuality.Tdata.Ferfch.InitiateAndWait();
			}
			{	// STOP:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERFch
				driver.RxQuality.Tdata.Ferfch.Stop();
				driver.RxQuality.Tdata.Ferfch.StopAndWait();
			}
			{	// ABORt:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERFch
				driver.RxQuality.Tdata.Ferfch.Abort();
				driver.RxQuality.Tdata.Ferfch.AbortAndWait();
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERFch:STATe
				ResourceStateEnum value = driver.RxQuality.Tdata.Ferfch.State.Fetch();				
			}
			{	// INITiate:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERSch
				driver.RxQuality.Tdata.Fersch.Initiate();
				driver.RxQuality.Tdata.Fersch.InitiateAndWait();
			}
			{	// STOP:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERSch
				driver.RxQuality.Tdata.Fersch.Stop();
				driver.RxQuality.Tdata.Fersch.StopAndWait();
			}
			{	// ABORt:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERSch
				driver.RxQuality.Tdata.Fersch.Abort();
				driver.RxQuality.Tdata.Fersch.AbortAndWait();
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERSch:STATe
				ResourceStateEnum value = driver.RxQuality.Tdata.Fersch.State.Fetch();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:TDATa:FERSch:STATe:ALL
				RsCmwCdma2kSig_RxQuality_Tdata_Fersch_State_All.Fetch_Data value = driver.RxQuality.Tdata.Fersch.State.All.Fetch();				
			}
			{	// READ:CDMA:SIGNaling<Instance>:RXQuality:FERFch
				RsCmwCdma2kSig_RxQuality_Ferfch.ResultData value = driver.RxQuality.Ferfch.Read();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:FERFch
				RsCmwCdma2kSig_RxQuality_Ferfch.ResultData value = driver.RxQuality.Ferfch.Fetch();				
			}
			{	// CALCulate:CDMA:SIGNaling<Instance>:RXQuality:FERFch
				RsCmwCdma2kSig_RxQuality_Ferfch.Calculate_Data value = driver.RxQuality.Ferfch.Calculate();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:FERFch:TDATa:STATe:ALL
				RsCmwCdma2kSig_RxQuality_Ferfch_Tdata_State_All.Fetch_Data value = driver.RxQuality.Ferfch.Tdata.State.All.Fetch();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:FERFch:STATe
				string value = driver.RxQuality.Ferfch.State.Fetch();				
			}
			{	// INITiate:CDMA:SIGNaling<Instance>:RXQuality:PSTRength
				driver.RxQuality.Pstrength.Initiate();
				driver.RxQuality.Pstrength.InitiateAndWait();
			}
			{	// STOP:CDMA:SIGNaling<Instance>:RXQuality:PSTRength
				driver.RxQuality.Pstrength.Stop();
				driver.RxQuality.Pstrength.StopAndWait();
			}
			{	// ABORt:CDMA:SIGNaling<Instance>:RXQuality:PSTRength
				driver.RxQuality.Pstrength.Abort();
				driver.RxQuality.Pstrength.AbortAndWait();
			}
			{	// READ:CDMA:SIGNaling<Instance>:RXQuality:PSTRength
				double value = driver.RxQuality.Pstrength.Read();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:PSTRength
				double value = driver.RxQuality.Pstrength.Fetch();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:PSTRength:STATe
				ResourceStateEnum value = driver.RxQuality.Pstrength.State.Fetch();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:PSTRength:STATe:ALL
				RsCmwCdma2kSig_RxQuality_Pstrength_State_All.Fetch_Data value = driver.RxQuality.Pstrength.State.All.Fetch();				
			}
			{	// READ:CDMA:SIGNaling<Instance>:RXQuality:FERSch
				RsCmwCdma2kSig_RxQuality_Fersch.Read_Data value = driver.RxQuality.Fersch.Read();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:FERSch
				RsCmwCdma2kSig_RxQuality_Fersch.Fetch_Data value = driver.RxQuality.Fersch.Fetch();				
			}
			{	// CALCulate:CDMA:SIGNaling<Instance>:RXQuality:FERSch
				RsCmwCdma2kSig_RxQuality_Fersch.Calculate_Data value = driver.RxQuality.Fersch.Calculate();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:FERSch:STATe
				string value = driver.RxQuality.Fersch.State.Fetch();				
			}
			{	// READ:CDMA:SIGNaling<Instance>:RXQuality:SFPower
				double value = driver.RxQuality.SfPower.Read();				
			}
			{	// FETCh:CDMA:SIGNaling<Instance>:RXQuality:SFPower
				double value = driver.RxQuality.SfPower.Fetch();				
			}
		}
	}
}