using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwEvdoSig;

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
			RsCmwEvdoSig driver = new RsCmwEvdoSig("TCPIP::localhost::INSTR", true, true);
			{	// CONFigure:EVDO:SIGNaling<Instance>:DISPlay
				foreach (DisplayTabEnum x in new DisplayTabEnum[] { DisplayTabEnum.CTRLchper, DisplayTabEnum.DATA, DisplayTabEnum.OVERview, DisplayTabEnum.PER, DisplayTabEnum.RLQ, DisplayTabEnum.THRoughput })
				{
					driver.Configure.Display = x;
					DisplayTabEnum value = driver.Configure.Display;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:ETOE
				bool value = driver.Configure.Etoe;
				driver.Configure.Etoe = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:TEST:CSTatus:MEID
				double value = driver.Configure.Test.Cstatus.Meid;
				driver.Configure.Test.Cstatus.Meid = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:TEST:CSTatus:ESN
				double value = driver.Configure.Test.Cstatus.Esn;
				driver.Configure.Test.Cstatus.Esn = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:EATTenuation
				RsCmwEvdoSig_Configure_RfSettings.Eattenuation_Data value = driver.Configure.RfSettings.Eattenuation;
				driver.Configure.RfSettings.Eattenuation = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:BCLass
				foreach (BandClassEnum x in new BandClassEnum[] { BandClassEnum.AWS, BandClassEnum.B18M, BandClassEnum.IEXT, BandClassEnum.IM2K, BandClassEnum.JTAC, BandClassEnum.KCEL, BandClassEnum.KPCS, BandClassEnum.LBANd, BandClassEnum.LO7C, BandClassEnum.N45T, BandClassEnum.NA7C, BandClassEnum.NA8S, BandClassEnum.NA9C, BandClassEnum.NAPC, BandClassEnum.PA4M, BandClassEnum.PA8M, BandClassEnum.PS7C, BandClassEnum.SBANd, BandClassEnum.TACS, BandClassEnum.U25B, BandClassEnum.U25F, BandClassEnum.USC, BandClassEnum.USPC })
				{
					driver.Configure.RfSettings.Bclass = x;
					BandClassEnum value = driver.Configure.RfSettings.Bclass;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:FREQuency
				RsCmwEvdoSig_Configure_RfSettings.Frequency_Data value = driver.Configure.RfSettings.Frequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:FLFRequency
				double value = driver.Configure.RfSettings.FlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:RLFRequency
				double value = driver.Configure.RfSettings.RlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:FOFFset
				double value = driver.Configure.RfSettings.FreqOffset;
				driver.Configure.RfSettings.FreqOffset = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFSettings:CHANnel
				int value = driver.Configure.RfSettings.Channel;
				driver.Configure.RfSettings.Channel = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:KCONstant
				foreach (KeepConstantEnum x in new KeepConstantEnum[] { KeepConstantEnum.DSHift, KeepConstantEnum.SPEed })
				{
					driver.Configure.Fading.Fsimulator.Kconstant = x;
					KeepConstantEnum value = driver.Configure.Fading.Fsimulator.Kconstant;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Fsimulator.Enable;
				driver.Configure.Fading.Fsimulator.Enable = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:STANdard
				foreach (FsimStandardEnum x in new FsimStandardEnum[] { FsimStandardEnum.P1, FsimStandardEnum.P2, FsimStandardEnum.P3, FsimStandardEnum.P4, FsimStandardEnum.P5 })
				{
					driver.Configure.Fading.Fsimulator.Standard = x;
					FsimStandardEnum value = driver.Configure.Fading.Fsimulator.Standard;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Fsimulator.Globale.Seed;
				driver.Configure.Fading.Fsimulator.Globale.Seed = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:RESTart:MODE
				foreach (AutoManualModeEnum x in new AutoManualModeEnum[] { AutoManualModeEnum.AUTO, AutoManualModeEnum.MANual })
				{
					driver.Configure.Fading.Fsimulator.Restart.Mode = x;
					AutoManualModeEnum value = driver.Configure.Fading.Fsimulator.Restart.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:RESTart
				driver.Configure.Fading.Fsimulator.Restart.Set();
				driver.Configure.Fading.Fsimulator.Restart.SetAndWait();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Fsimulator.InsertionLoss.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Fsimulator.InsertionLoss.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Loss;
				driver.Configure.Fading.Fsimulator.InsertionLoss.Loss = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:FSIMulator:ILOSs:CSAMples
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Csamples;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:AWGN:ENABle
				bool value = driver.Configure.Fading.Awgn.Enable;
				driver.Configure.Fading.Awgn.Enable = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:AWGN:SNRatio
				double value = driver.Configure.Fading.Awgn.SnRatio;
				driver.Configure.Fading.Awgn.SnRatio = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Awgn.Bandwidth.Ratio;
				driver.Configure.Fading.Awgn.Bandwidth.Ratio = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:AWGN:BWIDth:NOISe
				double value = driver.Configure.Fading.Awgn.Bandwidth.Noise;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:POWer:SIGNal
				double value = driver.Configure.Fading.Power.Signal;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:POWer:SUM
				double value = driver.Configure.Fading.Power.Sum;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Power.Noise.Total;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:FADing:POWer:NOISe
				double value = driver.Configure.Fading.Power.Noise.Value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:IQIN:PATH<n>
				RsCmwEvdoSig_Configure_IqIn_Path.Path_Data value = driver.Configure.IqIn.Path.Get(PathRepCap.Default);
				value = driver.Configure.IqIn.Path.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:IQIN:PATH<n>
				RsCmwEvdoSig_Configure_IqIn_Path.Path_Data value = new RsCmwEvdoSig_Configure_IqIn_Path.Path_Data();
				driver.Configure.IqIn.Path.Set(value, PathRepCap.Default);
				driver.Configure.IqIn.Path.Set(value);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:SETTing
				int value = driver.Configure.Carrier.Setting;
				driver.Configure.Carrier.Setting = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:CHANnel
				int value = driver.Configure.Carrier.Channel;
				driver.Configure.Carrier.Channel = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:FLFRequency
				int value = driver.Configure.Carrier.FlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:RLFRequency
				int value = driver.Configure.Carrier.RlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:LEVel:ABSolute
				int value = driver.Configure.Carrier.Level.Absolute;
				driver.Configure.Carrier.Level.Absolute = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CARRier:LEVel:RELative
				int value = driver.Configure.Carrier.Level.Relative;
				driver.Configure.Carrier.Level.Relative = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SECTor:SETTing
				int value = driver.Configure.Sector.Setting;
				driver.Configure.Sector.Setting = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:PILot:SETTing
				int value = driver.Configure.Pilot.Setting;
				driver.Configure.Pilot.Setting = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:AFLCarriers
				foreach (LinkCarrierEnum x in new LinkCarrierEnum[] { LinkCarrierEnum.ACTive, LinkCarrierEnum.DISabled, LinkCarrierEnum.NACTive, LinkCarrierEnum.NCConnected })
				{
					LinkCarrierEnum value = driver.Configure.Cstatus.AflCarriers;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:ARLCarriers
				foreach (LinkCarrierEnum x in new LinkCarrierEnum[] { LinkCarrierEnum.ACTive, LinkCarrierEnum.DISabled, LinkCarrierEnum.NACTive, LinkCarrierEnum.NCConnected })
				{
					LinkCarrierEnum value = driver.Configure.Cstatus.Arlcarriers;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:PLSubtype
				foreach (PlSubtypeEnum x in new PlSubtypeEnum[] { PlSubtypeEnum.ST01, PlSubtypeEnum.ST2, PlSubtypeEnum.ST3 })
				{
					PlSubtypeEnum value = driver.Configure.Cstatus.PlSubtype;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:IRAT
				bool value = driver.Configure.Cstatus.Irat;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:APPLication
				string value = driver.Configure.Cstatus.Application;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:UATI
				string value = driver.Configure.Cstatus.Uati;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:ESN
				string value = driver.Configure.Cstatus.Esn;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:MEID
				string value = driver.Configure.Cstatus.Meid;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:EHRPd
				bool value = driver.Configure.Cstatus.Ehrpd;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:LOG
				string value = driver.Configure.Cstatus.Log;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:ILCMask
				string value = driver.Configure.Cstatus.IlcMask;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:QLCMask
				string value = driver.Configure.Cstatus.Qlcmask;
			}
			{	// CONFigure:EVDO:SIGNaling<Instance>:CSTatus:MRBandwidth
				double value = driver.Configure.Cstatus.MrBandwidth;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:MODE
				foreach (PrefAppModeEnum x in new PrefAppModeEnum[] { PrefAppModeEnum.EHRPd, PrefAppModeEnum.HRPD })
				{
					PrefAppModeEnum value = driver.Configure.Cstatus.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:PCCHannel:ENABle
				bool value = driver.Configure.Cstatus.PcChannel.Enable;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CSTatus:PCCHannel:CYCLe
				int value = driver.Configure.Cstatus.PcChannel.Cycle;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:MAC:INDex
				RsCmwEvdoSig_Configure_Mac.Index_Data value = driver.Configure.Mac.Index;
				driver.Configure.Mac.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:CONNection:ROMessages
				bool value = driver.Configure.Layer.Connection.RoMessages;
				driver.Configure.Layer.Connection.RoMessages = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:CONNection:PDTHreshold
				double value = driver.Configure.Layer.Connection.PdThreshold;
				driver.Configure.Layer.Connection.PdThreshold = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:CONNection:RLFoffset
				int value = driver.Configure.Layer.Connection.RlfOffset;
				driver.Configure.Layer.Connection.RlfOffset = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:DRC:TYPE
				int value = driver.Configure.Layer.Application.Fmctap.Drc.Type;
				driver.Configure.Layer.Application.Fmctap.Drc.Type = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:DRC:INDex
				int value = driver.Configure.Layer.Application.Fmctap.Drc.Index;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:DRC:SIZE
				int value = driver.Configure.Layer.Application.Fmctap.Drc.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:DRC:RATE
				double value = driver.Configure.Layer.Application.Fmctap.Drc.Rate;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:DRC:SLOTs
				int value = driver.Configure.Layer.Application.Fmctap.Drc.Slots;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:LBACk:ENABle
				bool value = driver.Configure.Layer.Application.Fmctap.Lback.Enable;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:ACK:FMODe
				foreach (FModeEnum x in new FModeEnum[] { FModeEnum.AALWays, FModeEnum.NAALways, FModeEnum.NUSed })
				{
					driver.Configure.Layer.Application.Fmctap.Ack.Fmode = x;
					FModeEnum value = driver.Configure.Layer.Application.Fmctap.Ack.Fmode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FMCTap:ACK:MTYPe
				bool value = driver.Configure.Layer.Application.Fmctap.Ack.Mtype;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RMCTap:SMIN:INDex
				int value = driver.Configure.Layer.Application.Rmctap.Smin.Index;
				driver.Configure.Layer.Application.Rmctap.Smin.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RMCTap:SMIN:SIZE
				int value = driver.Configure.Layer.Application.Rmctap.Smin.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RMCTap:SMAX:INDex
				int value = driver.Configure.Layer.Application.Rmctap.Smax.Index;
				driver.Configure.Layer.Application.Rmctap.Smax.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RMCTap:SMAX:SIZE
				int value = driver.Configure.Layer.Application.Rmctap.Smax.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FTAP:DRC:INDex
				int value = driver.Configure.Layer.Application.Ftap.Drc.Index;
				driver.Configure.Layer.Application.Ftap.Drc.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FTAP:DRC:RATE
				double value = driver.Configure.Layer.Application.Ftap.Drc.Rate;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FTAP:DRC:SLOTs
				int value = driver.Configure.Layer.Application.Ftap.Drc.Slots;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FTAP:LBACk:ENABle
				bool value = driver.Configure.Layer.Application.Ftap.Lback.Enable;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FTAP:ACK:FMODe
				foreach (FModeEnum x in new FModeEnum[] { FModeEnum.AALWays, FModeEnum.NAALways, FModeEnum.NUSed })
				{
					driver.Configure.Layer.Application.Ftap.Ack.Fmode = x;
					FModeEnum value = driver.Configure.Layer.Application.Ftap.Ack.Fmode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RTAP:RMIN:INDex
				int value = driver.Configure.Layer.Application.Rtap.Rmin.Index;
				driver.Configure.Layer.Application.Rtap.Rmin.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RTAP:RMIN:RATE
				double value = driver.Configure.Layer.Application.Rtap.Rmin.Rate;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RTAP:RMAX:INDex
				int value = driver.Configure.Layer.Application.Rtap.Rmax.Index;
				driver.Configure.Layer.Application.Rtap.Rmax.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RTAP:RMAX:RATE
				double value = driver.Configure.Layer.Application.Rtap.Rmax.Rate;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:DRC:TYPE
				int value = driver.Configure.Layer.Application.Fetap.Drc.Type;
				driver.Configure.Layer.Application.Fetap.Drc.Type = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:DRC:INDex
				int value = driver.Configure.Layer.Application.Fetap.Drc.Index;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:DRC:SIZE
				int value = driver.Configure.Layer.Application.Fetap.Drc.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:DRC:RATE
				double value = driver.Configure.Layer.Application.Fetap.Drc.Rate;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:DRC:SLOTs
				int value = driver.Configure.Layer.Application.Fetap.Drc.Slots;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:LBACk:ENABle
				bool value = driver.Configure.Layer.Application.Fetap.Lback.Enable;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:ACK:FMODe
				foreach (FModeEnum x in new FModeEnum[] { FModeEnum.AALWays, FModeEnum.NAALways, FModeEnum.NUSed })
				{
					driver.Configure.Layer.Application.Fetap.Ack.Fmode = x;
					FModeEnum value = driver.Configure.Layer.Application.Fetap.Ack.Fmode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:FETap:ACK:MTYPe
				bool value = driver.Configure.Layer.Application.Fetap.Ack.Mtype;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RETap:TTARget
				int value = driver.Configure.Layer.Application.Retap.Ttarget;
				driver.Configure.Layer.Application.Retap.Ttarget = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RETap:SMIN:INDex
				int value = driver.Configure.Layer.Application.Retap.Smin.Index;
				driver.Configure.Layer.Application.Retap.Smin.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RETap:SMIN:SIZE
				int value = driver.Configure.Layer.Application.Retap.Smin.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RETap:SMAX:INDex
				int value = driver.Configure.Layer.Application.Retap.Smax.Index;
				driver.Configure.Layer.Application.Retap.Smax.Index = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:RETap:SMAX:SIZE
				int value = driver.Configure.Layer.Application.Retap.Smax.Size;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:PACKet:PREFerred
				foreach (PrefApplicationEnum x in new PrefApplicationEnum[] { PrefApplicationEnum.DPA, PrefApplicationEnum.EMPA })
				{
					driver.Configure.Layer.Application.Packet.Preferred = x;
					PrefApplicationEnum value = driver.Configure.Layer.Application.Packet.Preferred;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:APPLication:PACKet:MODE
				foreach (PrefAppModeEnum x in new PrefAppModeEnum[] { PrefAppModeEnum.EHRPd, PrefAppModeEnum.HRPD })
				{
					driver.Configure.Layer.Application.Packet.Mode = x;
					PrefAppModeEnum value = driver.Configure.Layer.Application.Packet.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:TTOPt
				foreach (T2PmodeEnum x in new T2PmodeEnum[] { T2PmodeEnum.RFCO, T2PmodeEnum.TPUT })
				{
					driver.Configure.Layer.Mac.Ttopt = x;
					T2PmodeEnum value = driver.Configure.Layer.Mac.Ttopt;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DRATe
				foreach (CtrlChannelDataRateEnum x in new CtrlChannelDataRateEnum[] { CtrlChannelDataRateEnum.R384, CtrlChannelDataRateEnum.R768 })
				{
					driver.Configure.Layer.Mac.Drate = x;
					CtrlChannelDataRateEnum value = driver.Configure.Layer.Mac.Drate;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:SSEed
				string value = driver.Configure.Layer.Mac.Sseed;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:MPSequences
				int value = driver.Configure.Layer.Mac.MpSequences;
				driver.Configure.Layer.Mac.MpSequences = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:IPBackoff
				int value = driver.Configure.Layer.Mac.IpBackoff;
				driver.Configure.Layer.Mac.IpBackoff = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:IPSBackoff
				int value = driver.Configure.Layer.Mac.IpsBackoff;
				driver.Configure.Layer.Mac.IpsBackoff = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:DRC:COVer
				int value = driver.Configure.Layer.Mac.EftProtocol.Drc.Cover;
				driver.Configure.Layer.Mac.EftProtocol.Drc.Cover = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:DRC:LENGth
				int value = driver.Configure.Layer.Mac.EftProtocol.Drc.Length;
				driver.Configure.Layer.Mac.EftProtocol.Drc.Length = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:DRC:CGAin
				double value = driver.Configure.Layer.Mac.EftProtocol.Drc.Cgain;
				driver.Configure.Layer.Mac.EftProtocol.Drc.Cgain = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:DSC:VALue
				int value = driver.Configure.Layer.Mac.EftProtocol.Dsc.Value;
				driver.Configure.Layer.Mac.EftProtocol.Dsc.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:DSC:CGAin
				double value = driver.Configure.Layer.Mac.EftProtocol.Dsc.Cgain;
				driver.Configure.Layer.Mac.EftProtocol.Dsc.Cgain = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:EFTProtocol:ACK:CGAin
				double value = driver.Configure.Layer.Mac.EftProtocol.Ack.Cgain;
				driver.Configure.Layer.Mac.EftProtocol.Ack.Cgain = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DFTProtocol:DRC:COVer
				int value = driver.Configure.Layer.Mac.DftProtocol.Drc.Cover;
				driver.Configure.Layer.Mac.DftProtocol.Drc.Cover = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DFTProtocol:DRC:LENGth
				int value = driver.Configure.Layer.Mac.DftProtocol.Drc.Length;
				driver.Configure.Layer.Mac.DftProtocol.Drc.Length = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DFTProtocol:DRC:CGAin
				double value = driver.Configure.Layer.Mac.DftProtocol.Drc.Cgain;
				driver.Configure.Layer.Mac.DftProtocol.Drc.Cgain = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DFTProtocol:ACK:CGAin
				double value = driver.Configure.Layer.Mac.DftProtocol.Ack.Cgain;
				driver.Configure.Layer.Mac.DftProtocol.Ack.Cgain = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DRTProtocol:DONom
				double value = driver.Configure.Layer.Mac.DrtProtocol.Donom;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DRTProtocol:DRATe
				RsCmwEvdoSig_Configure_Layer_Mac_DrtProtocol.Drate_Data value = driver.Configure.Layer.Mac.DrtProtocol.Drate;
				driver.Configure.Layer.Mac.DrtProtocol.Drate = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DRTProtocol:ITRansition
				RsCmwEvdoSig_Configure_Layer_Mac_DrtProtocol.Itransition_Data value = driver.Configure.Layer.Mac.DrtProtocol.Itransition;
				driver.Configure.Layer.Mac.DrtProtocol.Itransition = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:MAC:DRTProtocol:DTRansition
				RsCmwEvdoSig_Configure_Layer_Mac_DrtProtocol.Dtransition_Data value = driver.Configure.Layer.Mac.DrtProtocol.Dtransition;
				driver.Configure.Layer.Mac.DrtProtocol.Dtransition = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:SESSion:ISTimeout
				int value = driver.Configure.Layer.Session.IsTimeout;
				driver.Configure.Layer.Session.IsTimeout = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:LAYer:SESSion:SNINcluded
				bool value = driver.Configure.Layer.Session.SnIncluded;
				driver.Configure.Layer.Session.SnIncluded = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SID
				int value = driver.Configure.Network.Sid;
				driver.Configure.Network.Sid = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:RELease
				foreach (NetworkReleaseEnum x in new NetworkReleaseEnum[] { NetworkReleaseEnum.R0, NetworkReleaseEnum.RA, NetworkReleaseEnum.RB })
				{
					driver.Configure.Network.Release = x;
					NetworkReleaseEnum value = driver.Configure.Network.Release;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:PNOFfset
				int value = driver.Configure.Network.Sector.PnOffset;
				driver.Configure.Network.Sector.PnOffset = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:CLRCode
				int value = driver.Configure.Network.Sector.ClrCode;
				driver.Configure.Network.Sector.ClrCode = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:SMASk
				int value = driver.Configure.Network.Sector.Smask;
				driver.Configure.Network.Sector.Smask = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:CNTCode
				int value = driver.Configure.Network.Sector.CntCode;
				driver.Configure.Network.Sector.CntCode = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:FORMat
				foreach (SectorIdFormatEnum x in new SectorIdFormatEnum[] { SectorIdFormatEnum.A41N, SectorIdFormatEnum.MANual })
				{
					driver.Configure.Network.Sector.Format = x;
					SectorIdFormatEnum value = driver.Configure.Network.Sector.Format;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:NPBits
				int value = driver.Configure.Network.Sector.Npbits;
				driver.Configure.Network.Sector.Npbits = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:IDOVerall
				string value = driver.Configure.Network.Sector.IdOverall;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:ID:ANSI
				double value = driver.Configure.Network.Sector.Id.Ansi;
				driver.Configure.Network.Sector.Id.Ansi = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECTor:ID:MANual
				double value = driver.Configure.Network.Sector.Id.Manual;
				driver.Configure.Network.Sector.Id.Manual = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PILot:AN:ACTive
				bool value = driver.Configure.Network.Pilot.An.Active;
				driver.Configure.Network.Pilot.An.Active = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PILot:AT:ASSigned
				bool value = driver.Configure.Network.Pilot.At.Assigned;
				driver.Configure.Network.Pilot.At.Assigned = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PILot:AT:ACQuired
				bool value = driver.Configure.Network.Pilot.At.Acquired;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PROPerty:CLDTime
				double value = driver.Configure.Network.Property.CldTime;
				driver.Configure.Network.Property.CldTime = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PROPerty:FPACtivity
				int value = driver.Configure.Network.Property.Fpactivity;
				driver.Configure.Network.Property.Fpactivity = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:PROPerty:IRAT
				bool value = driver.Configure.Network.Property.Irat;
				driver.Configure.Network.Property.Irat = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:MODE
				foreach (ProbesAckModeEnum x in new ProbesAckModeEnum[] { ProbesAckModeEnum.ACKN, ProbesAckModeEnum.IGN })
				{
					driver.Configure.Network.Aprobes.Mode = x;
					ProbesAckModeEnum value = driver.Configure.Network.Aprobes.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:IADJust
				int value = driver.Configure.Network.Aprobes.Iadjust;
				driver.Configure.Network.Aprobes.Iadjust = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:OLADjust
				int value = driver.Configure.Network.Aprobes.OlAdjust;
				driver.Configure.Network.Aprobes.OlAdjust = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:PINCrement
				double value = driver.Configure.Network.Aprobes.Pincrement;
				driver.Configure.Network.Aprobes.Pincrement = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:PPSequence
				int value = driver.Configure.Network.Aprobes.PpSequence;
				driver.Configure.Network.Aprobes.PpSequence = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:PLENgth
				int value = driver.Configure.Network.Aprobes.Plength;
				driver.Configure.Network.Aprobes.Plength = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:ACDuration
				foreach (AccessDurationEnum x in new AccessDurationEnum[] { AccessDurationEnum.S128, AccessDurationEnum.S16, AccessDurationEnum.S32, AccessDurationEnum.S64 })
				{
					driver.Configure.Network.Aprobes.AcDuration = x;
					AccessDurationEnum value = driver.Configure.Network.Aprobes.AcDuration;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:PLSLots
				foreach (PlSlotsEnum x in new PlSlotsEnum[] { PlSlotsEnum.S16, PlSlotsEnum.S4 })
				{
					driver.Configure.Network.Aprobes.PlSlots = x;
					PlSlotsEnum value = driver.Configure.Network.Aprobes.PlSlots;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:APRobes:SAMRate
				foreach (SamRateEnum x in new SamRateEnum[] { SamRateEnum.R19K, SamRateEnum.R38K, SamRateEnum.R9K })
				{
					driver.Configure.Network.Aprobes.SamRate = x;
					SamRateEnum value = driver.Configure.Network.Aprobes.SamRate;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECurity:SKEY
				string value = driver.Configure.Network.Security.Skey;
				driver.Configure.Network.Security.Skey = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECurity:OPC
				string value = driver.Configure.Network.Security.Opc;
				driver.Configure.Network.Security.Opc = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECurity:AUTHenticat
				string value = driver.Configure.Network.Security.Authenticate;
				driver.Configure.Network.Security.Authenticate = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NETWork:SECurity:SQN
				string value = driver.Configure.Network.Security.Sqn;
				driver.Configure.Network.Security.Sqn = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:BCLass
				foreach (BandClassEnum x in new BandClassEnum[] { BandClassEnum.AWS, BandClassEnum.B18M, BandClassEnum.IEXT, BandClassEnum.IM2K, BandClassEnum.JTAC, BandClassEnum.KCEL, BandClassEnum.KPCS, BandClassEnum.LBANd, BandClassEnum.LO7C, BandClassEnum.N45T, BandClassEnum.NA7C, BandClassEnum.NA8S, BandClassEnum.NA9C, BandClassEnum.NAPC, BandClassEnum.PA4M, BandClassEnum.PA8M, BandClassEnum.PS7C, BandClassEnum.SBANd, BandClassEnum.TACS, BandClassEnum.U25B, BandClassEnum.U25F, BandClassEnum.USC, BandClassEnum.USPC })
				{
					driver.Configure.Handoff.Bclass = x;
					BandClassEnum value = driver.Configure.Handoff.Bclass;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:CHANnel
				int value = driver.Configure.Handoff.Channel;
				driver.Configure.Handoff.Channel = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:CARRier:CHANnel
				int value = driver.Configure.Handoff.Carrier.Channel;
				driver.Configure.Handoff.Carrier.Channel = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:CARRier:FLFRequency
				int value = driver.Configure.Handoff.Carrier.FlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:CARRier:RLFRequency
				int value = driver.Configure.Handoff.Carrier.RlFrequency;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:NETWork:PILot:AN:ACTive
				bool value = driver.Configure.Handoff.Network.Pilot.An.Active;
				driver.Configure.Handoff.Network.Pilot.An.Active = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:HANDoff:NETWork:PILot:AT:ASSigned
				bool value = driver.Configure.Handoff.Network.Pilot.At.Assigned;
				driver.Configure.Handoff.Network.Pilot.At.Assigned = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:MMONitor:IPADdress
				RsCmwEvdoSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:MMONitor:IPADdress
				foreach (IpAddressIndexEnum x in new IpAddressIndexEnum[] { IpAddressIndexEnum.IP1, IpAddressIndexEnum.IP2, IpAddressIndexEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:APPLication:DSIGnaling
				int value = driver.Configure.Application.Dsignaling;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:APPLication:MODE
				foreach (ApplicationModeEnum x in new ApplicationModeEnum[] { ApplicationModeEnum.FAR, ApplicationModeEnum.FWD, ApplicationModeEnum.PACKet, ApplicationModeEnum.REV })
				{
					driver.Configure.Application.Mode = x;
					ApplicationModeEnum value = driver.Configure.Application.Mode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:APPLication
				int value = driver.Configure.Application.Value;
				driver.Configure.Application.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:EVDO
				double value = driver.Configure.RfPower.Evdo;
				driver.Configure.RfPower.Evdo = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:OUTPut
				double value = driver.Configure.RfPower.Output;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:EPMode
				foreach (ExpPowerModeEnum x in new ExpPowerModeEnum[] { ExpPowerModeEnum.AUTO, ExpPowerModeEnum.MANual, ExpPowerModeEnum.MAX, ExpPowerModeEnum.MIN, ExpPowerModeEnum.OLRule })
				{
					driver.Configure.RfPower.Epmode = x;
					ExpPowerModeEnum value = driver.Configure.RfPower.Epmode;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:MANual
				double value = driver.Configure.RfPower.Manual;
				driver.Configure.RfPower.Manual = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:EXPected
				double value = driver.Configure.RfPower.Expected;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:MODE:AWGN
				foreach (AwgnModeEnum x in new AwgnModeEnum[] { AwgnModeEnum.HPOWer, AwgnModeEnum.NORMal })
				{
					driver.Configure.RfPower.Mode.Awgn = x;
					AwgnModeEnum value = driver.Configure.RfPower.Mode.Awgn;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RFPower:LEVel:AWGN
				double value = driver.Configure.RfPower.Level.Awgn;
				driver.Configure.RfPower.Level.Awgn = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:PCBits
				foreach (PowerCtrlBitsEnum x in new PowerCtrlBitsEnum[] { PowerCtrlBitsEnum.ADOWn, PowerCtrlBitsEnum.AUP, PowerCtrlBitsEnum.AUTO, PowerCtrlBitsEnum.HOLD, PowerCtrlBitsEnum.PATTern, PowerCtrlBitsEnum.RTESt })
				{
					driver.Configure.RpControl.Pcbits = x;
					PowerCtrlBitsEnum value = driver.Configure.RpControl.Pcbits;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:SSIZe
				double value = driver.Configure.RpControl.Ssize;
				driver.Configure.RpControl.Ssize = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RpControl.Repetition = x;
					RepeatEnum value = driver.Configure.RpControl.Repetition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:RUN
				bool value = driver.Configure.RpControl.Run;
				driver.Configure.RpControl.Run = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:SEGMent<nr>:BITS
				SegmentBitsEnum value = driver.Configure.RpControl.Segment.Bits.Get(SegmentRepCap.Default);
				value = driver.Configure.RpControl.Segment.Bits.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:SEGMent<nr>:BITS
				foreach (SegmentBitsEnum x in new SegmentBitsEnum[] { SegmentBitsEnum.ALTernating, SegmentBitsEnum.DOWN, SegmentBitsEnum.UP })
				{
					driver.Configure.RpControl.Segment.Bits.Set(x);
					driver.Configure.RpControl.Segment.Bits.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:SEGMent<nr>:LENGth
				int value = driver.Configure.RpControl.Segment.Length.Get(SegmentRepCap.Default);
				value = driver.Configure.RpControl.Segment.Length.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RPControl:SEGMent<nr>:LENGth
				driver.Configure.RpControl.Segment.Length.Set(1, SegmentRepCap.Default);
				driver.Configure.RpControl.Segment.Length.Set(1);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:TSOurce
				foreach (TimeSourceEnum x in new TimeSourceEnum[] { TimeSourceEnum.CMWTime, TimeSourceEnum.DATE, TimeSourceEnum.SYNC })
				{
					driver.Configure.System.Tsource = x;
					TimeSourceEnum value = driver.Configure.System.Tsource;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:DATE
				RsCmwEvdoSig_Configure_System.Date_Data value = driver.Configure.System.Date;
				driver.Configure.System.Date = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:TIME
				RsCmwEvdoSig_Configure_System.Time_Data value = driver.Configure.System.Time;
				driver.Configure.System.Time = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:SYNC
				string value = driver.Configure.System.Sync;
				driver.Configure.System.Sync = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:ATIMe
				foreach (ApplyTimeAtEnum x in new ApplyTimeAtEnum[] { ApplyTimeAtEnum.EVER, ApplyTimeAtEnum.NEXT, ApplyTimeAtEnum.SUSO })
				{
					driver.Configure.System.Atime = x;
					ApplyTimeAtEnum value = driver.Configure.System.Atime;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:LSEConds
				int value = driver.Configure.System.Lseconds;
				driver.Configure.System.Lseconds = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:LTOFfset:HEX
				string value = driver.Configure.System.LtOffset.Hex;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:SYSTem:LTOFfset
				RsCmwEvdoSig_Configure_System_LtOffset.Value_Data value = driver.Configure.System.LtOffset.Value;
				driver.Configure.System.LtOffset.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:RLMeutra
				int value = driver.Configure.Ncell.RlmEutra;
				driver.Configure.Ncell.RlmEutra = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:THRServing
				int value = driver.Configure.Ncell.ThrServing;
				driver.Configure.Ncell.ThrServing = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:MRTimer
				int value = driver.Configure.Ncell.MrTimer;
				driver.Configure.Ncell.MrTimer = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:ALL:THResholds:LOW
				RsCmwEvdoSig_Configure_Ncell_All_Thresholds.Low_Data value = driver.Configure.Ncell.All.Thresholds.Low;
				driver.Configure.Ncell.All.Thresholds.Low = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:ALL:THResholds
				RsCmwEvdoSig_Configure_Ncell_All_Thresholds.Value_Data value = driver.Configure.Ncell.All.Thresholds.Value;
				driver.Configure.Ncell.All.Thresholds.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:EVDO:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Evdo_Cell.Cell_Data value = driver.Configure.Ncell.Evdo.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Evdo.Cell.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:EVDO:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Evdo_Cell.Cell_Data value = new RsCmwEvdoSig_Configure_Ncell_Evdo_Cell.Cell_Data();
				driver.Configure.Ncell.Evdo.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Evdo.Cell.Set(value);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:EVDO:THResholds:LOW
				int value = driver.Configure.Ncell.Evdo.Thresholds.Low;
				driver.Configure.Ncell.Evdo.Thresholds.Low = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:EVDO:THResholds
				RsCmwEvdoSig_Configure_Ncell_Evdo_Thresholds.Value_Data value = driver.Configure.Ncell.Evdo.Thresholds.Value;
				driver.Configure.Ncell.Evdo.Thresholds.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:CDMA:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Cdma_Cell.Cell_Data value = driver.Configure.Ncell.Cdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Cdma.Cell.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:CDMA:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Cdma_Cell.Cell_Data value = new RsCmwEvdoSig_Configure_Ncell_Cdma_Cell.Cell_Data();
				driver.Configure.Ncell.Cdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Cdma.Cell.Set(value);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:CDMA:THResholds:LOW
				int value = driver.Configure.Ncell.Cdma.Thresholds.Low;
				driver.Configure.Ncell.Cdma.Thresholds.Low = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:CDMA:THResholds
				RsCmwEvdoSig_Configure_Ncell_Cdma_Thresholds.Value_Data value = driver.Configure.Ncell.Cdma.Thresholds.Value;
				driver.Configure.Ncell.Cdma.Thresholds.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Lte_Cell.Cell_Data value = driver.Configure.Ncell.Lte.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Lte.Cell.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwEvdoSig_Configure_Ncell_Lte_Cell.Cell_Data value = new RsCmwEvdoSig_Configure_Ncell_Lte_Cell.Cell_Data();
				driver.Configure.Ncell.Lte.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Lte.Cell.Set(value);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:THResholds:LOW
				int value = driver.Configure.Ncell.Lte.Thresholds.Low;
				driver.Configure.Ncell.Lte.Thresholds.Low = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:THResholds
				RsCmwEvdoSig_Configure_Ncell_Lte_Thresholds.Value_Data value = driver.Configure.Ncell.Lte.Thresholds.Value;
				driver.Configure.Ncell.Lte.Thresholds.Value = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:THRX<n>
				int value = driver.Configure.Ncell.Lte.Thrx.Get(NeighborCellRepCap.Default);
				value = driver.Configure.Ncell.Lte.Thrx.Get();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:NCELl:LTE:THRX<n>
				driver.Configure.Ncell.Lte.Thrx.Set(1, NeighborCellRepCap.Default);
				driver.Configure.Ncell.Lte.Thrx.Set(1);
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CONNection:EDAU:ENABle
				bool value = driver.Configure.Connection.Edau.Enable;
				driver.Configure.Connection.Edau.Enable = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CONNection:EDAU:NSEGment
				foreach (NetworkSegmentEnum x in new NetworkSegmentEnum[] { NetworkSegmentEnum.A, NetworkSegmentEnum.B, NetworkSegmentEnum.C })
				{
					driver.Configure.Connection.Edau.Nsegment = x;
					NetworkSegmentEnum value = driver.Configure.Connection.Edau.Nsegment;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:CONNection:EDAU:NID
				int value = driver.Configure.Connection.Edau.Nid;
				driver.Configure.Connection.Edau.Nid = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:UPERiod
				double value = driver.Configure.RxQuality.Uperiod;
				driver.Configure.RxQuality.Uperiod = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Repetition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:CARRier:SELect
				int value = driver.Configure.RxQuality.Carrier.Select;
				driver.Configure.RxQuality.Carrier.Select = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RSTatistics
				driver.Configure.RxQuality.Rstatistics.Set();
				driver.Configure.RxQuality.Rstatistics.SetAndWait();
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:PER:TOUT
				double value = driver.Configure.RxQuality.Per.Timeout;
				driver.Configure.RxQuality.Per.Timeout = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:PER:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Per.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Per.Repetition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:FLPer:MTPSent
				int value = driver.Configure.RxQuality.FlPer.Mtpsent;
				driver.Configure.RxQuality.FlPer.Mtpsent = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:FLPer:SCONdition
				foreach (PerStopConditionEnum x in new PerStopConditionEnum[] { PerStopConditionEnum.ALEXceeded, PerStopConditionEnum.MCLexceeded, PerStopConditionEnum.MPERexceeded, PerStopConditionEnum.NONE })
				{
					driver.Configure.RxQuality.FlPer.Scondition = x;
					PerStopConditionEnum value = driver.Configure.RxQuality.FlPer.Scondition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RLPer:MPSent
				int value = driver.Configure.RxQuality.RlPer.MpSent;
				driver.Configure.RxQuality.RlPer.MpSent = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RLPer:SCONdition
				foreach (PerStopConditionEnum x in new PerStopConditionEnum[] { PerStopConditionEnum.ALEXceeded, PerStopConditionEnum.MCLexceeded, PerStopConditionEnum.MPERexceeded, PerStopConditionEnum.NONE })
				{
					driver.Configure.RxQuality.RlPer.Scondition = x;
					PerStopConditionEnum value = driver.Configure.RxQuality.RlPer.Scondition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:THRoughput:TOUT
				double value = driver.Configure.RxQuality.Throughput.Timeout;
				driver.Configure.RxQuality.Throughput.Timeout = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:THRoughput:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Throughput.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Throughput.Repetition;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:FLPFormance:MFRames
				int value = driver.Configure.RxQuality.FlPerformance.Mframes;
				driver.Configure.RxQuality.FlPerformance.Mframes = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RLPFormance:MFRames
				int value = driver.Configure.RxQuality.RlPerformance.Mframes;
				driver.Configure.RxQuality.RlPerformance.Mframes = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RESult:FLPer
				bool value = driver.Configure.RxQuality.Result.FlPer;
				driver.Configure.RxQuality.Result.FlPer = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RESult:RLPer
				bool value = driver.Configure.RxQuality.Result.RlPer;
				driver.Configure.RxQuality.Result.RlPer = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RESult:FLPFormance
				bool value = driver.Configure.RxQuality.Result.FlPerformance;
				driver.Configure.RxQuality.Result.FlPerformance = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RESult:RLPFormance
				bool value = driver.Configure.RxQuality.Result.RlPerformance;
				driver.Configure.RxQuality.Result.RlPerformance = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:RESult:IPSTatistics
				bool value = driver.Configure.RxQuality.Result.IpStatistics;
				driver.Configure.RxQuality.Result.IpStatistics = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:LIMit:PER:EVALuation
				foreach (PerEvaluationEnum x in new PerEvaluationEnum[] { PerEvaluationEnum.ALLCarriers, PerEvaluationEnum.PERCarrier })
				{
					driver.Configure.RxQuality.Limit.Per.Evaluation = x;
					PerEvaluationEnum value = driver.Configure.RxQuality.Limit.Per.Evaluation;
				}
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:LIMit:FLPer:MPER
				double value = driver.Configure.RxQuality.Limit.FlPer.Mper;
				driver.Configure.RxQuality.Limit.FlPer.Mper = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:LIMit:FLPer:CLEVel
				double value = driver.Configure.RxQuality.Limit.FlPer.Clevel;
				driver.Configure.RxQuality.Limit.FlPer.Clevel = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:LIMit:RLPer:MPER
				double value = driver.Configure.RxQuality.Limit.RlPer.Mper;
				driver.Configure.RxQuality.Limit.RlPer.Mper = value;
			}
			{	// CONFigure:EVDO:SIGNaling<instance>:RXQuality:LIMit:RLPer:CLEVel
				double value = driver.Configure.RxQuality.Limit.RlPer.Clevel;
				driver.Configure.RxQuality.Limit.RlPer.Clevel = value;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:TEST:RX:POWer:STATe
				foreach (RxSignalStateEnum x in new RxSignalStateEnum[] { RxSignalStateEnum.HIGH, RxSignalStateEnum.LOW, RxSignalStateEnum.NAV, RxSignalStateEnum.OK })
				{
					RxSignalStateEnum value = driver.Sense.Test.Rx.Power.State;
				}
			}
			{	// SENSe:EVDO:SIGNaling<instance>:IQOut:PATH<n>
				RsCmwEvdoSig_Sense_IqOut.GetPath_Data value = driver.Sense.IqOut.GetPath(PathRepCap.Nr1);
				value = driver.Sense.IqOut.GetPath();
			}
			{	// SENSe:EVDO:SIGNaling<instance>:ANADdress:IPV<n>
				string value = driver.Sense.AnAddress.Ipv;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:ATADdress:IPV<n>
				string value = driver.Sense.AtAddress.GetIpv(IpAddressRepCap.Version4);
				value = driver.Sense.AtAddress.GetIpv();
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:STATe
				string value = driver.Sense.RxQuality.IpStatistics.State;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:RESet
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.Reset_Data value = driver.Sense.RxQuality.IpStatistics.Reset;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:RACK
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.Rack_Data value = driver.Sense.RxQuality.IpStatistics.Rack;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:NAK
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.Nak_Data value = driver.Sense.RxQuality.IpStatistics.Nak;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:SUMMary
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.Summary_Data value = driver.Sense.RxQuality.IpStatistics.Summary;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:PPPTotal
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.PppTotal_Data value = driver.Sense.RxQuality.IpStatistics.PppTotal;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:RXQuality:IPSTatistics:DRATe
				RsCmwEvdoSig_Sense_RxQuality_IpStatistics.Drate_Data value = driver.Sense.RxQuality.IpStatistics.Drate;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:ELOG:LAST
				RsCmwEvdoSig_Sense_Elog.Last_Data value = driver.Sense.Elog.Last;
			}
			{	// SENSe:EVDO:SIGNaling<instance>:ELOG:ALL
				RsCmwEvdoSig_Sense_Elog.All_Data value = driver.Sense.Elog.All;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>
				RsCmwEvdoSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:SCELl
				RsCmwEvdoSig_Route_Scenario.Scell_Data value = driver.Route.Scenario.Scell;
				driver.Route.Scenario.Scell = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:HMODe
				RsCmwEvdoSig_Route_Scenario.Hmode_Data value = driver.Route.Scenario.Hmode;
				driver.Route.Scenario.Hmode = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:HMLite
				RsCmwEvdoSig_Route_Scenario.Hmlite_Data value = driver.Route.Scenario.Hmlite;
				driver.Route.Scenario.Hmlite = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario
				RsCmwEvdoSig_Route_Scenario.Value_Data value = driver.Route.Scenario.Value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:SCFading[:EXTernal]
				RsCmwEvdoSig_Route_Scenario_ScFading.External_Data value = driver.Route.Scenario.ScFading.External;
				driver.Route.Scenario.ScFading.External = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:SCFading:INTernal
				RsCmwEvdoSig_Route_Scenario_ScFading.Internal_Data value = driver.Route.Scenario.ScFading.Internal;
				driver.Route.Scenario.ScFading.Internal = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:HMFading[:EXTernal]
				RsCmwEvdoSig_Route_Scenario_HmFading.External_Data value = driver.Route.Scenario.HmFading.External;
				driver.Route.Scenario.HmFading.External = value;
			}
			{	// ROUTe:EVDO:SIGNaling<instance>:SCENario:HMFading:INTernal
				RsCmwEvdoSig_Route_Scenario_HmFading.Internal_Data value = driver.Route.Scenario.HmFading.Internal;
				driver.Route.Scenario.HmFading.Internal = value;
			}
			{	// SOURce:EVDO:SIGNaling<instance>:RFSettings:TX:EATTenuation
				double value = driver.Source.RfSettings.Tx.Eattenuation;
				driver.Source.RfSettings.Tx.Eattenuation = value;
			}
			{	// SOURce:EVDO:SIGNaling<instance>:RFSettings:RX:EATTenuation
				double value = driver.Source.RfSettings.Rx.Eattenuation;
				driver.Source.RfSettings.Rx.Eattenuation = value;
			}
			{	// SOURce:EVDO:SIGNaling<instance>:STATe:ALL
				RsCmwEvdoSig_Source_State.All_Data value = driver.Source.State.All;
			}
			{	// SOURce:EVDO:SIGNaling<instance>:STATe
				bool value = driver.Source.State.Value;
				driver.Source.State.Value = value;
			}
			{	// CALL:EVDO:SIGNaling<instance>:CSWitched:ACTion
				foreach (CSwitchedActionEnum x in new CSwitchedActionEnum[] { CSwitchedActionEnum.CLOSe, CSwitchedActionEnum.CONNect, CSwitchedActionEnum.DISConnect, CSwitchedActionEnum.HANDoff })
				{
					driver.Call.Cswitched.Action = x;					
				}
			}
			{	// CALL:EVDO:SIGNaling<instance>:HANDoff:STARt
				driver.Call.Handoff.Start();
				driver.Call.Handoff.StartAndWait();
			}
			{	// FETCh:EVDO:SIGNaling<instance>:CSWitched:STATe
				ConnectionStateEnum value = driver.Cswitched.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:PDATa:STATe
				PdStateEnum value = driver.Pdata.State.Fetch();				
			}
			{	// INITiate:EVDO:SIGNaling<instance>:PER
				driver.Per.Initiate();
				driver.Per.InitiateAndWait();
			}
			{	// STOP:EVDO:SIGNaling<instance>:PER
				driver.Per.Stop();
				driver.Per.StopAndWait();
			}
			{	// ABORt:EVDO:SIGNaling<instance>:PER
				driver.Per.Abort();
				driver.Per.AbortAndWait();
			}
			{	// FETCh:EVDO:SIGNaling<instance>:PER:STATe
				ResourceStateEnum value = driver.Per.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:PER:STATe:ALL
				RsCmwEvdoSig_Per_State_All.Fetch_Data value = driver.Per.State.All.Fetch();				
			}
			{	// INITiate:EVDO:SIGNaling<instance>:THRoughput
				driver.Throughput.Initiate();
				driver.Throughput.InitiateAndWait();
			}
			{	// STOP:EVDO:SIGNaling<instance>:THRoughput
				driver.Throughput.Stop();
				driver.Throughput.StopAndWait();
			}
			{	// ABORt:EVDO:SIGNaling<instance>:THRoughput
				driver.Throughput.Abort();
				driver.Throughput.AbortAndWait();
			}
			{	// FETCh:EVDO:SIGNaling<instance>:THRoughput:STATe
				ResourceStateEnum value = driver.Throughput.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:THRoughput:STATe:ALL
				RsCmwEvdoSig_Throughput_State_All.Fetch_Data value = driver.Throughput.State.All.Fetch();				
			}
			{	// INITiate:EVDO:SIGNaling<instance>:RXQuality
				driver.RxQuality.Initiate();
				driver.RxQuality.InitiateAndWait();
			}
			{	// STOP:EVDO:SIGNaling<instance>:RXQuality
				driver.RxQuality.Stop();
				driver.RxQuality.StopAndWait();
			}
			{	// ABORt:EVDO:SIGNaling<instance>:RXQuality
				driver.RxQuality.Abort();
				driver.RxQuality.AbortAndWait();
			}
			{	// READ:EVDO:SIGNaling<instance>:RXQuality:FLPer
				RsCmwEvdoSig_RxQuality_FlPer.ResultData value = driver.RxQuality.FlPer.Read();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPer
				RsCmwEvdoSig_RxQuality_FlPer.ResultData value = driver.RxQuality.FlPer.Fetch();				
			}
			{	// CALCulate:EVDO:SIGNaling<instance>:RXQuality:FLPer
				RsCmwEvdoSig_RxQuality_FlPer.Calculate_Data value = driver.RxQuality.FlPer.Calculate();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPer:STATe
				string value = driver.RxQuality.FlPer.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPer:CSTate
				List<CarrierStatusEnum> value = driver.RxQuality.FlPer.Cstate.Fetch();				
			}
			{	// READ:EVDO:SIGNaling<instance>:RXQuality:RLPer
				RsCmwEvdoSig_RxQuality_RlPer.ResultData value = driver.RxQuality.RlPer.Read(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPer.Read();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPer
				RsCmwEvdoSig_RxQuality_RlPer.ResultData value = driver.RxQuality.RlPer.Fetch(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPer.Fetch();				
			}
			{	// CALCulate:EVDO:SIGNaling<instance>:RXQuality:RLPer
				RsCmwEvdoSig_RxQuality_RlPer.Calculate_Data value = driver.RxQuality.RlPer.Calculate(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPer.Calculate();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPer:STATe
				string value = driver.RxQuality.RlPer.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPer:CSTate
				List<CarrierStatusEnum> value = driver.RxQuality.RlPer.Cstate.Fetch();				
			}
			{	// READ:EVDO:SIGNaling<instance>:RXQuality:FLPFormance
				RsCmwEvdoSig_RxQuality_FlPerformance.ResultData value = driver.RxQuality.FlPerformance.Read(PacketSizeEnum.S128);
				value = driver.RxQuality.FlPerformance.Read();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPFormance
				RsCmwEvdoSig_RxQuality_FlPerformance.ResultData value = driver.RxQuality.FlPerformance.Fetch(PacketSizeEnum.S128);
				value = driver.RxQuality.FlPerformance.Fetch();				
			}
			{	// CALCulate:EVDO:SIGNaling<instance>:RXQuality:FLPFormance
				RsCmwEvdoSig_RxQuality_FlPerformance.Calculate_Data value = driver.RxQuality.FlPerformance.Calculate(PacketSizeEnum.S128);
				value = driver.RxQuality.FlPerformance.Calculate();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPFormance:STATe
				string value = driver.RxQuality.FlPerformance.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:FLPFormance:CSTate
				List<CarrierStatusEnum> value = driver.RxQuality.FlPerformance.Cstate.Fetch();				
			}
			{	// READ:EVDO:SIGNaling<instance>:RXQuality:RLPFormance
				RsCmwEvdoSig_RxQuality_RlPerformance.ResultData value = driver.RxQuality.RlPerformance.Read(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPerformance.Read();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPFormance
				RsCmwEvdoSig_RxQuality_RlPerformance.ResultData value = driver.RxQuality.RlPerformance.Fetch(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPerformance.Fetch();				
			}
			{	// CALCulate:EVDO:SIGNaling<instance>:RXQuality:RLPFormance
				RsCmwEvdoSig_RxQuality_RlPerformance.Calculate_Data value = driver.RxQuality.RlPerformance.Calculate(RevLinkPerDataRateEnum.R0K0);
				value = driver.RxQuality.RlPerformance.Calculate();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPFormance:STATe
				string value = driver.RxQuality.RlPerformance.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:RLPFormance:CSTate
				List<CarrierStatusEnum> value = driver.RxQuality.RlPerformance.Cstate.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:STATe
				ResourceStateEnum value = driver.RxQuality.State.Fetch();				
			}
			{	// FETCh:EVDO:SIGNaling<instance>:RXQuality:STATe:ALL
				RsCmwEvdoSig_RxQuality_State_All.Fetch_Data value = driver.RxQuality.State.All.Fetch();				
			}
		}
	}
}