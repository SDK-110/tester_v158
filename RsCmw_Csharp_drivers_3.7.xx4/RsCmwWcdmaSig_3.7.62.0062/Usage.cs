using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwWcdmaSig;

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
			RsCmwWcdmaSig driver = new RsCmwWcdmaSig("TCPIP::localhost::INSTR", true, true);
			{	// CONFigure:WCDMa:SIGNaling<instance>:ETOE
				bool value = driver.Configure.Etoe;
				driver.Configure.Etoe = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ESCode
				bool value = driver.Configure.EsCode;
				driver.Configure.EsCode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:PSETtings:ERGM
				RsCmwWcdmaSig_Configure_Psettings.Ergm_Data value = driver.Configure.Psettings.Ergm;
				driver.Configure.Psettings.Ergm = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:PSETtings:HUMP
				foreach (SubTestEnum x in new SubTestEnum[] { SubTestEnum.S1, SubTestEnum.S2, SubTestEnum.S3, SubTestEnum.S4, SubTestEnum.S5 })
				{
					driver.Configure.Psettings.Hump = x;
					SubTestEnum value = driver.Configure.Psettings.Hump;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:PSETtings
				foreach (WizzardSelectionEnum x in new WizzardSelectionEnum[] { WizzardSelectionEnum.DHIP, WizzardSelectionEnum.ERGM, WizzardSelectionEnum.HCQI, WizzardSelectionEnum.HDMT, WizzardSelectionEnum.HSMT, WizzardSelectionEnum.HUMP, WizzardSelectionEnum.HUMT, WizzardSelectionEnum.OOS })
				{
					driver.Configure.Psettings.Value = x;					
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:MMONitor:IPADdress
				RsCmwWcdmaSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:MMONitor:IPADdress
				foreach (IpAddrIndexEnum x in new IpAddrIndexEnum[] { IpAddrIndexEnum.IP1, IpAddrIndexEnum.IP2, IpAddrIndexEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:ENABle
				bool value = driver.Configure.UeReport.Enable;
				driver.Configure.UeReport.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:RINTerval
				double value = driver.Configure.UeReport.Rinterval;
				driver.Configure.UeReport.Rinterval = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:CCELl:ENABle
				RsCmwWcdmaSig_Configure_UeReport_Ccell.Enable_Data value = driver.Configure.UeReport.Ccell.Enable;
				driver.Configure.UeReport.Ccell.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:NCELl:ENABle
				RsCmwWcdmaSig_Configure_UeReport_Ncell.Enable_Data value = driver.Configure.UeReport.Ncell.Enable;
				driver.Configure.UeReport.Ncell.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:NCELl:GSM:ENABle
				RsCmwWcdmaSig_Configure_UeReport_Ncell_Gsm.Enable_Data value = driver.Configure.UeReport.Ncell.Gsm.Enable;
				driver.Configure.UeReport.Ncell.Gsm.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:NCELl:WCDMa:ENABle
				RsCmwWcdmaSig_Configure_UeReport_Ncell_Wcdma.Enable_Data value = driver.Configure.UeReport.Ncell.Wcdma.Enable;
				driver.Configure.UeReport.Ncell.Wcdma.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UEReport:NCELl:LTE:ENABle
				RsCmwWcdmaSig_Configure_UeReport_Ncell_Lte.Enable_Data value = driver.Configure.UeReport.Ncell.Lte.Enable;
				driver.Configure.UeReport.Ncell.Lte.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:PATTern
				foreach (CmodePatternSelectionEnum x in new CmodePatternSelectionEnum[] { CmodePatternSelectionEnum.NONE, CmodePatternSelectionEnum.SINGle, CmodePatternSelectionEnum.UEReport, CmodePatternSelectionEnum.ULCM })
				{
					driver.Configure.Cmode.Pattern = x;
					CmodePatternSelectionEnum value = driver.Configure.Cmode.Pattern;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:ULCM:TYPE
				foreach (TransGapTypeEnum x in new TransGapTypeEnum[] { TransGapTypeEnum.AF, TransGapTypeEnum.AR, TransGapTypeEnum.B })
				{
					driver.Configure.Cmode.Ulcm.Type = x;
					TransGapTypeEnum value = driver.Configure.Cmode.Ulcm.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:ULCM:ACTivation
				driver.Configure.Cmode.Ulcm.Activation.Set();
				driver.Configure.Cmode.Ulcm.Activation.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:SINGle:TYPE
				foreach (TransGapTypeExtendedEnum x in new TransGapTypeExtendedEnum[] { TransGapTypeExtendedEnum.A, TransGapTypeExtendedEnum.B, TransGapTypeExtendedEnum.C, TransGapTypeExtendedEnum.D, TransGapTypeExtendedEnum.E, TransGapTypeExtendedEnum.F, TransGapTypeExtendedEnum.RFA, TransGapTypeExtendedEnum.RFB })
				{
					driver.Configure.Cmode.Single.Type = x;
					TransGapTypeExtendedEnum value = driver.Configure.Cmode.Single.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:SINGle:ACTivation
				foreach (CmodeActivationEnum x in new CmodeActivationEnum[] { CmodeActivationEnum.MEASurement, CmodeActivationEnum.RAB })
				{
					driver.Configure.Cmode.Single.Activation = x;
					CmodeActivationEnum value = driver.Configure.Cmode.Single.Activation;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:UEReport:ACTivation
				RsCmwWcdmaSig_Configure_Cmode_UeReport.Activation_Data value = driver.Configure.Cmode.UeReport.Activation;
				driver.Configure.Cmode.UeReport.Activation = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CMODe:UEReport:ENABle
				RsCmwWcdmaSig_Configure_Cmode_UeReport.Enable_Data value = driver.Configure.Cmode.UeReport.Enable;
				driver.Configure.Cmode.UeReport.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:DBDC
				RsCmwWcdmaSig_Configure_RfSettings.Dbdc_Data value = driver.Configure.RfSettings.Dbdc;
				driver.Configure.RfSettings.Dbdc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:ENPMode
				foreach (NominalPowerModeEnum x in new NominalPowerModeEnum[] { NominalPowerModeEnum.AUToranging, NominalPowerModeEnum.MANual, NominalPowerModeEnum.ULPC })
				{
					driver.Configure.RfSettings.EnpMode = x;
					NominalPowerModeEnum value = driver.Configure.RfSettings.EnpMode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:ENPower
				double value = driver.Configure.RfSettings.EnvelopePower;
				driver.Configure.RfSettings.EnvelopePower = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:MARGin
				double value = driver.Configure.RfSettings.Margin;
				driver.Configure.RfSettings.Margin = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:DCARrier:SEParation
				double value = driver.Configure.RfSettings.Dcarrier.Separation;
				driver.Configure.RfSettings.Dcarrier.Separation = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:GMTFactor
				double value = driver.Configure.RfSettings.Carrier.GmtFactor;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:AWGN
				RsCmwWcdmaSig_Configure_RfSettings_Carrier.Awgn_Data value = driver.Configure.RfSettings.Carrier.Awgn;
				driver.Configure.RfSettings.Carrier.Awgn = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:COPower
				double value = driver.Configure.RfSettings.Carrier.CoPower;
				driver.Configure.RfSettings.Carrier.CoPower = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:TOPower
				double value = driver.Configure.RfSettings.Carrier.ToPower;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:EDC:INPut
				double value = driver.Configure.RfSettings.Carrier.Edc.Input;
				driver.Configure.RfSettings.Carrier.Edc.Input = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:EDC:OUTPut
				double value = driver.Configure.RfSettings.Carrier.Edc.Output;
				driver.Configure.RfSettings.Carrier.Edc.Output = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Carrier.Eattenuation.Input;
				driver.Configure.RfSettings.Carrier.Eattenuation.Input = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:EATTenuation:OUTPut
				double value = driver.Configure.RfSettings.Carrier.Eattenuation.Output;
				driver.Configure.RfSettings.Carrier.Eattenuation.Output = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:CHANnel:UL
				int value = driver.Configure.RfSettings.Carrier.Channel.Uplink;
				driver.Configure.RfSettings.Carrier.Channel.Uplink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:CHANnel:DL
				int value = driver.Configure.RfSettings.Carrier.Channel.Downlink;
				driver.Configure.RfSettings.Carrier.Channel.Downlink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:FREQuency:UL
				double value = driver.Configure.RfSettings.Carrier.Frequency.Uplink;
				driver.Configure.RfSettings.Carrier.Frequency.Uplink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:FREQuency:DL
				double value = driver.Configure.RfSettings.Carrier.Frequency.Downlink;
				driver.Configure.RfSettings.Carrier.Frequency.Downlink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:FOFFset:UL
				double value = driver.Configure.RfSettings.Carrier.FreqOffset.Uplink;
				driver.Configure.RfSettings.Carrier.FreqOffset.Uplink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:FOFFset:DL
				double value = driver.Configure.RfSettings.Carrier.FreqOffset.Downlink;
				driver.Configure.RfSettings.Carrier.FreqOffset.Downlink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:UL
				RsCmwWcdmaSig_Configure_RfSettings_Carrier_Uplink.Get_Data value = driver.Configure.RfSettings.Carrier.Uplink.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:UL
				driver.Configure.RfSettings.Carrier.Uplink.Set(OperationBandEnum.OB1, 1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:DL
				RsCmwWcdmaSig_Configure_RfSettings_Carrier_Downlink.Get_Data value = driver.Configure.RfSettings.Carrier.Downlink.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:CARRier<carrier>:DL
				driver.Configure.RfSettings.Carrier.Downlink.Set(OperationBandEnum.OB1, 1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:COPower:TOTal
				double value = driver.Configure.RfSettings.CoPower.Total;
				driver.Configure.RfSettings.CoPower.Total = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:TOPower:TOTal
				double value = driver.Configure.RfSettings.ToPower.Total;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:UDSeparation
				double value = driver.Configure.RfSettings.UserDefined.UdSeparation;
				driver.Configure.RfSettings.UserDefined.UdSeparation = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:CHANnel:DL:MINimum
				int value = driver.Configure.RfSettings.UserDefined.Channel.Downlink.Minimum;
				driver.Configure.RfSettings.UserDefined.Channel.Downlink.Minimum = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:CHANnel:DL:MAXimum
				int value = driver.Configure.RfSettings.UserDefined.Channel.Downlink.Maximum;
				driver.Configure.RfSettings.UserDefined.Channel.Downlink.Maximum = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:CHANnel:UL:MINimum
				int value = driver.Configure.RfSettings.UserDefined.Channel.Uplink.Minimum;
				driver.Configure.RfSettings.UserDefined.Channel.Uplink.Minimum = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:CHANnel:UL:MAXimum
				int value = driver.Configure.RfSettings.UserDefined.Channel.Uplink.Maximum;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:FREQuency:DL:MINimum
				double value = driver.Configure.RfSettings.UserDefined.Frequency.Downlink.Minimum;
				driver.Configure.RfSettings.UserDefined.Frequency.Downlink.Minimum = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:FREQuency:DL:MAXimum
				double value = driver.Configure.RfSettings.UserDefined.Frequency.Downlink.Maximum;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:FREQuency:UL:MINimum
				double value = driver.Configure.RfSettings.UserDefined.Frequency.Uplink.Minimum;
				driver.Configure.RfSettings.UserDefined.Frequency.Uplink.Minimum = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:RFSettings:UDEFined:FREQuency:UL:MAXimum
				double value = driver.Configure.RfSettings.UserDefined.Frequency.Uplink.Maximum;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CARRier<carrier>:BAND
				foreach (OperationBandEnum x in new OperationBandEnum[] { OperationBandEnum.OB1, OperationBandEnum.OB10, OperationBandEnum.OB11, OperationBandEnum.OB12, OperationBandEnum.OB13, OperationBandEnum.OB14, OperationBandEnum.OB15, OperationBandEnum.OB16, OperationBandEnum.OB17, OperationBandEnum.OB18, OperationBandEnum.OB19, OperationBandEnum.OB2, OperationBandEnum.OB20, OperationBandEnum.OB21, OperationBandEnum.OB22, OperationBandEnum.OB25, OperationBandEnum.OB26, OperationBandEnum.OB3, OperationBandEnum.OB32, OperationBandEnum.OB4, OperationBandEnum.OB5, OperationBandEnum.OB6, OperationBandEnum.OB7, OperationBandEnum.OB8, OperationBandEnum.OB9, OperationBandEnum.OBL1, OperationBandEnum.OBS1, OperationBandEnum.OBS2, OperationBandEnum.OBS3, OperationBandEnum.UDEFined })
				{
					driver.Configure.Carrier.Band = x;
					OperationBandEnum value = driver.Configure.Carrier.Band;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:IQIN:CARRier<carrier>
				RsCmwWcdmaSig_Configure_IqIn.Carrier_Data value = driver.Configure.IqIn.Carrier;
				driver.Configure.IqIn.Carrier = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:OCNS:TYPE
				foreach (OcnsChannelTypeEnum x in new OcnsChannelTypeEnum[] { OcnsChannelTypeEnum.AUTO, OcnsChannelTypeEnum.R5, OcnsChannelTypeEnum.R6, OcnsChannelTypeEnum.R7, OcnsChannelTypeEnum.R99 })
				{
					driver.Configure.Downlink.Carrier.Ocns.Type = x;
					OcnsChannelTypeEnum value = driver.Configure.Downlink.Carrier.Ocns.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:OCNS:LEVel
				double value = driver.Configure.Downlink.Carrier.Ocns.Level;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:APOWer
				double value = driver.Configure.Downlink.Carrier.Level.Apower;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:EHICh
				double value = driver.Configure.Downlink.Carrier.Level.Ehich;
				driver.Configure.Downlink.Carrier.Level.Ehich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:ERGCh
				double value = driver.Configure.Downlink.Carrier.Level.Ergch;
				driver.Configure.Downlink.Carrier.Level.Ergch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:EAGCh
				double value = driver.Configure.Downlink.Carrier.Level.Eagch;
				driver.Configure.Downlink.Carrier.Level.Eagch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:HSPDsch
				double value = driver.Configure.Downlink.Carrier.Level.Hspdsch;
				driver.Configure.Downlink.Carrier.Level.Hspdsch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:PSCH
				double value = driver.Configure.Downlink.Carrier.Level.Psch;
				driver.Configure.Downlink.Carrier.Level.Psch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:SSCH
				double value = driver.Configure.Downlink.Carrier.Level.Ssch;
				driver.Configure.Downlink.Carrier.Level.Ssch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:PCPich
				double value = driver.Configure.Downlink.Carrier.Level.Pcpich;
				driver.Configure.Downlink.Carrier.Level.Pcpich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:PCCPch
				double value = driver.Configure.Downlink.Carrier.Level.Pccpch;
				driver.Configure.Downlink.Carrier.Level.Pccpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:FDPCh
				double value = driver.Configure.Downlink.Carrier.Level.Fdpch;
				driver.Configure.Downlink.Carrier.Level.Fdpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:HSSCch<nr>
				double value = driver.Configure.Downlink.Carrier.Level.Hsscch.Get(HSSCchRepCap.Default);
				value = driver.Configure.Downlink.Carrier.Level.Hsscch.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:LEVel:HSSCch<nr>
				driver.Configure.Downlink.Carrier.Level.Hsscch.Set(1.0, HSSCchRepCap.Default);
				driver.Configure.Downlink.Carrier.Level.Hsscch.Set(1.0);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:CONFlict
				RsCmwWcdmaSig_Configure_Downlink_Carrier_Code.Conflict_Data value = driver.Configure.Downlink.Carrier.Code.Conflict;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:HSPDsch
				int value = driver.Configure.Downlink.Carrier.Code.Hspdsch;
				driver.Configure.Downlink.Carrier.Code.Hspdsch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:EAGCh
				int value = driver.Configure.Downlink.Carrier.Code.Eagch;
				driver.Configure.Downlink.Carrier.Code.Eagch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:ERGCh
				int value = driver.Configure.Downlink.Carrier.Code.Ergch;
				driver.Configure.Downlink.Carrier.Code.Ergch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:EHICh
				int value = driver.Configure.Downlink.Carrier.Code.Ehich;
				driver.Configure.Downlink.Carrier.Code.Ehich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:PCPich
				int value = driver.Configure.Downlink.Carrier.Code.Pcpich;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:HSSCch<nr>
				int value = driver.Configure.Downlink.Carrier.Code.Hsscch.Get(HSSCchRepCap.Default);
				value = driver.Configure.Downlink.Carrier.Code.Hsscch.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:CODE:HSSCch<nr>
				driver.Configure.Downlink.Carrier.Code.Hsscch.Set(1, HSSCchRepCap.Default);
				driver.Configure.Downlink.Carrier.Code.Hsscch.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:DPCH:FSFormat
				int value = driver.Configure.Downlink.Carrier.Enhanced.Dpch.FsFormat;
				driver.Configure.Downlink.Carrier.Enhanced.Dpch.FsFormat = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:PCPich:SLEVel
				double value = driver.Configure.Downlink.Carrier.Enhanced.Pcpich.Slevel;
				driver.Configure.Downlink.Carrier.Enhanced.Pcpich.Slevel = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:HSPDsch:USFRames
				foreach (UnscheduledTransTypeEnum x in new UnscheduledTransTypeEnum[] { UnscheduledTransTypeEnum.DTX, UnscheduledTransTypeEnum.DUMMy })
				{
					driver.Configure.Downlink.Carrier.Enhanced.Hspdsch.UsFrames = x;
					UnscheduledTransTypeEnum value = driver.Configure.Downlink.Carrier.Enhanced.Hspdsch.UsFrames;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:HSPDsch:POFFset
				RsCmwWcdmaSig_Configure_Downlink_Carrier_Enhanced_Hspdsch.Poffset_Data value = driver.Configure.Downlink.Carrier.Enhanced.Hspdsch.Poffset;
				driver.Configure.Downlink.Carrier.Enhanced.Hspdsch.Poffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:HSSCch:USFRames
				foreach (UnscheduledTransTypeEnum x in new UnscheduledTransTypeEnum[] { UnscheduledTransTypeEnum.DTX, UnscheduledTransTypeEnum.DUMMy })
				{
					driver.Configure.Downlink.Carrier.Enhanced.Hsscch.UsFrames = x;
					UnscheduledTransTypeEnum value = driver.Configure.Downlink.Carrier.Enhanced.Hsscch.UsFrames;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:HSSCch:NUMBer
				int value = driver.Configure.Downlink.Carrier.Enhanced.Hsscch.Number;
				driver.Configure.Downlink.Carrier.Enhanced.Hsscch.Number = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:HSSCch:SELection
				foreach (HsScchTypeEnum x in new HsScchTypeEnum[] { HsScchTypeEnum.AUTomatic, HsScchTypeEnum.CH1, HsScchTypeEnum.CH2, HsScchTypeEnum.CH3, HsScchTypeEnum.CH4, HsScchTypeEnum.RANDom })
				{
					driver.Configure.Downlink.Carrier.Enhanced.Hsscch.Selection = x;
					HsScchTypeEnum value = driver.Configure.Downlink.Carrier.Enhanced.Hsscch.Selection;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:HSSCch<nr>:UEID
				double value = driver.Configure.Downlink.Carrier.Hsscch.Ueid.Get(HSSCchRepCap.Default);
				value = driver.Configure.Downlink.Carrier.Hsscch.Ueid.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:HSSCch<nr>:UEID
				driver.Configure.Downlink.Carrier.Hsscch.Ueid.Set(1.0, HSSCchRepCap.Default);
				driver.Configure.Downlink.Carrier.Hsscch.Ueid.Set(1.0);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:HSSCch<nr>:IDDummy
				double value = driver.Configure.Downlink.Carrier.Hsscch.IdDummy.Get(HSSCchRepCap.Default);
				value = driver.Configure.Downlink.Carrier.Hsscch.IdDummy.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:HSSCch<nr>:IDDummy
				driver.Configure.Downlink.Carrier.Hsscch.IdDummy.Set(1.0, HSSCchRepCap.Default);
				driver.Configure.Downlink.Carrier.Hsscch.IdDummy.Set(1.0);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:SCPich
				double value = driver.Configure.Downlink.Level.Scpich;
				driver.Configure.Downlink.Level.Scpich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:SCCPch
				double value = driver.Configure.Downlink.Level.Sccpch;
				driver.Configure.Downlink.Level.Sccpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:PICH
				double value = driver.Configure.Downlink.Level.Pich;
				driver.Configure.Downlink.Level.Pich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:AICH
				double value = driver.Configure.Downlink.Level.Aich;
				driver.Configure.Downlink.Level.Aich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:DPCH
				double value = driver.Configure.Downlink.Level.Dpch;
				driver.Configure.Downlink.Level.Dpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:LEVel:ADJust
				driver.Configure.Downlink.Level.Adjust.Set();
				driver.Configure.Downlink.Level.Adjust.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:SCPich
				int value = driver.Configure.Downlink.Code.Scpich;
				driver.Configure.Downlink.Code.Scpich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:PCCPch
				int value = driver.Configure.Downlink.Code.Pccpch;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:SCCPch
				int value = driver.Configure.Downlink.Code.Sccpch;
				driver.Configure.Downlink.Code.Sccpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:PICH
				int value = driver.Configure.Downlink.Code.Pich;
				driver.Configure.Downlink.Code.Pich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:AICH
				int value = driver.Configure.Downlink.Code.Aich;
				driver.Configure.Downlink.Code.Aich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:DPCH
				int value = driver.Configure.Downlink.Code.Dpch;
				driver.Configure.Downlink.Code.Dpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:CODE:FDPCh
				int value = driver.Configure.Downlink.Code.Fdpch;
				driver.Configure.Downlink.Code.Fdpch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:RXLStrategy
				foreach (PowerStrategyEnum x in new PowerStrategyEnum[] { PowerStrategyEnum.AF, PowerStrategyEnum.BF, PowerStrategyEnum.CE })
				{
					driver.Configure.Downlink.Enhanced.Dpch.RxlStrategy = x;
					PowerStrategyEnum value = driver.Configure.Downlink.Enhanced.Dpch.RxlStrategy;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:PHASe
				foreach (PhaseReferenceEnum x in new PhaseReferenceEnum[] { PhaseReferenceEnum.PCPich, PhaseReferenceEnum.SCPich })
				{
					driver.Configure.Downlink.Enhanced.Dpch.Phase = x;
					PhaseReferenceEnum value = driver.Configure.Downlink.Enhanced.Dpch.Phase;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:SSCode
				int value = driver.Configure.Downlink.Enhanced.Dpch.Sscode;
				driver.Configure.Downlink.Enhanced.Dpch.Sscode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:TOFFset
				double value = driver.Configure.Downlink.Enhanced.Dpch.Toffset;
				driver.Configure.Downlink.Enhanced.Dpch.Toffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:RANGe
				RsCmwWcdmaSig_Configure_Downlink_Enhanced_Dpch.Range_Data value = driver.Configure.Downlink.Enhanced.Dpch.Range;
				driver.Configure.Downlink.Enhanced.Dpch.Range = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:LSEQuence:STATe
				foreach (LevelSeqStateEnum x in new LevelSeqStateEnum[] { LevelSeqStateEnum.FAILed, LevelSeqStateEnum.IDLE, LevelSeqStateEnum.RUNNing, LevelSeqStateEnum.SCHanged, LevelSeqStateEnum.SCONflict })
				{
					LevelSeqStateEnum value = driver.Configure.Downlink.Enhanced.Dpch.Lsequence.State;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:LSEQuence
				RsCmwWcdmaSig_Configure_Downlink_Enhanced_Dpch_Lsequence.Value_Data value = driver.Configure.Downlink.Enhanced.Dpch.Lsequence.Value;
				driver.Configure.Downlink.Enhanced.Dpch.Lsequence.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:DPCH:LSEQuence:EXECute
				driver.Configure.Downlink.Enhanced.Dpch.Lsequence.Execute.Set();
				driver.Configure.Downlink.Enhanced.Dpch.Lsequence.Execute.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:AICH:ACKNowledge
				foreach (SlopeTypeEnum x in new SlopeTypeEnum[] { SlopeTypeEnum.NEGative, SlopeTypeEnum.POSitive })
				{
					driver.Configure.Downlink.Enhanced.Aich.Acknowledge = x;
					SlopeTypeEnum value = driver.Configure.Downlink.Enhanced.Aich.Acknowledge;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:AICH:TTIMing
				double value = driver.Configure.Downlink.Enhanced.Aich.Ttiming;
				driver.Configure.Downlink.Enhanced.Aich.Ttiming = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:SCPich:PHASe
				int value = driver.Configure.Downlink.Enhanced.Scpich.Phase;
				driver.Configure.Downlink.Enhanced.Scpich.Phase = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:ENHanced:SCPich:SSCode
				int value = driver.Configure.Downlink.Enhanced.Scpich.Sscode;
				driver.Configure.Downlink.Enhanced.Scpich.Sscode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:PCONtrol:MODE
				foreach (PowerControlModeEnum x in new PowerControlModeEnum[] { PowerControlModeEnum.M0, PowerControlModeEnum.M1, PowerControlModeEnum.OFF, PowerControlModeEnum.ON })
				{
					driver.Configure.Downlink.Pcontrol.Mode = x;
					PowerControlModeEnum value = driver.Configure.Downlink.Pcontrol.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:PCONtrol:STEP
				double value = driver.Configure.Downlink.Pcontrol.Step;
				driver.Configure.Downlink.Pcontrol.Step = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:PCONtrol:DTQuality
				double value = driver.Configure.Downlink.Pcontrol.DtQuality;
				driver.Configure.Downlink.Pcontrol.DtQuality = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:DL:PCONtrol:FTERate
				double value = driver.Configure.Downlink.Pcontrol.Fterate;
				driver.Configure.Downlink.Pcontrol.Fterate = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:MUEPower
				double value = driver.Configure.Uplink.MuePower;
				driver.Configure.Uplink.MuePower = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:UEPClass:REPorted
				bool value = driver.Configure.Uplink.UepClass.Reported;
				driver.Configure.Uplink.UepClass.Reported = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:UEPClass:MANual
				foreach (UePowerClassEnum x in new UePowerClassEnum[] { UePowerClassEnum.PC1, UePowerClassEnum.PC2, UePowerClassEnum.PC3, UePowerClassEnum.PC3B, UePowerClassEnum.PC4 })
				{
					driver.Configure.Uplink.UepClass.Manual = x;
					UePowerClassEnum value = driver.Configure.Uplink.UepClass.Manual;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:CARRier<carrier>:POFFset
				double value = driver.Configure.Uplink.Carrier.Poffset;
				driver.Configure.Uplink.Carrier.Poffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:CARRier<carrier>:SCODe
				double value = driver.Configure.Uplink.Carrier.Scode;
				driver.Configure.Uplink.Carrier.Scode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:CARRier<carrier>:TPC:TPOWer
				double value = driver.Configure.Uplink.Carrier.Tpc.Tpower;
				driver.Configure.Uplink.Carrier.Tpc.Tpower = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:OLPControl:INTerference
				double value = driver.Configure.Uplink.OlpControl.Interference;
				driver.Configure.Uplink.OlpControl.Interference = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:OLPControl:CVALue
				double value = driver.Configure.Uplink.OlpControl.Cvalue;
				driver.Configure.Uplink.OlpControl.Cvalue = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:DRXCycle
				int value = driver.Configure.Uplink.Prach.DrxCycle;
				driver.Configure.Uplink.Prach.DrxCycle = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:AICH
				int value = driver.Configure.Uplink.Prach.Preamble.Aich;
				driver.Configure.Uplink.Prach.Preamble.Aich = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:SSIZe
				int value = driver.Configure.Uplink.Prach.Preamble.Ssize;
				driver.Configure.Uplink.Prach.Preamble.Ssize = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:SUBChannels
				double value = driver.Configure.Uplink.Prach.Preamble.SubChannels;
				driver.Configure.Uplink.Prach.Preamble.SubChannels = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:MCYCles
				int value = driver.Configure.Uplink.Prach.Preamble.Mcycles;
				driver.Configure.Uplink.Prach.Preamble.Mcycles = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:MRETrans
				int value = driver.Configure.Uplink.Prach.Preamble.Mretrans;
				driver.Configure.Uplink.Prach.Preamble.Mretrans = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:PREamble:SIGNature
				double value = driver.Configure.Uplink.Prach.Preamble.Signature;
				driver.Configure.Uplink.Prach.Preamble.Signature = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:MESSage:POFFset
				double value = driver.Configure.Uplink.Prach.Message.Poffset;
				driver.Configure.Uplink.Prach.Message.Poffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:PRACh:MESSage:LENGth
				double value = driver.Configure.Uplink.Prach.Message.Length;
				driver.Configure.Uplink.Prach.Message.Length = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:VIDeo
				RsCmwWcdmaSig_Configure_Uplink_Gfactor.Video_Data value = driver.Configure.Uplink.Gfactor.Video;
				driver.Configure.Uplink.Gfactor.Video = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:VOICe
				RsCmwWcdmaSig_Configure_Uplink_Gfactor.Voice_Data value = driver.Configure.Uplink.Gfactor.Voice;
				driver.Configure.Uplink.Gfactor.Voice = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSDPa
				RsCmwWcdmaSig_Configure_Uplink_Gfactor.Hsdpa_Data value = driver.Configure.Uplink.Gfactor.Hsdpa;
				driver.Configure.Uplink.Gfactor.Hsdpa = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:PDATa<nr>
				RsCmwWcdmaSig_Configure_Uplink_Gfactor_Pdata.Pdata_Data value = driver.Configure.Uplink.Gfactor.Pdata.Get(PacketDataRepCap.Default);
				value = driver.Configure.Uplink.Gfactor.Pdata.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:PDATa<nr>
				RsCmwWcdmaSig_Configure_Uplink_Gfactor_Pdata.Pdata_Data value = new RsCmwWcdmaSig_Configure_Uplink_Gfactor_Pdata.Pdata_Data();
				driver.Configure.Uplink.Gfactor.Pdata.Set(value, PacketDataRepCap.Default);
				driver.Configure.Uplink.Gfactor.Pdata.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:RMC<nr>
				RsCmwWcdmaSig_Configure_Uplink_Gfactor_Rmc.Rmc_Data value = driver.Configure.Uplink.Gfactor.Rmc.Get(RefMeasChannelRepCap.Default);
				value = driver.Configure.Uplink.Gfactor.Rmc.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:RMC<nr>
				RsCmwWcdmaSig_Configure_Uplink_Gfactor_Rmc.Rmc_Data value = new RsCmwWcdmaSig_Configure_Uplink_Gfactor_Rmc.Rmc_Data();
				driver.Configure.Uplink.Gfactor.Rmc.Set(value, RefMeasChannelRepCap.Default);
				driver.Configure.Uplink.Gfactor.Rmc.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:EDPCch
				int value = driver.Configure.Uplink.Gfactor.Hsupa.Edpcch;
				driver.Configure.Uplink.Gfactor.Hsupa.Edpcch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:DTTP
				int value = driver.Configure.Uplink.Gfactor.Hsupa.Dttp;
				driver.Configure.Uplink.Gfactor.Hsupa.Dttp = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:EDPFormula
				foreach (UeAlgorithmEnum x in new UeAlgorithmEnum[] { UeAlgorithmEnum.EXTRapolation, UeAlgorithmEnum.INTerpolation })
				{
					driver.Configure.Uplink.Gfactor.Hsupa.EdpFormula = x;
					UeAlgorithmEnum value = driver.Configure.Uplink.Gfactor.Hsupa.EdpFormula;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:ETFCi:POFFset
				List<int> value = driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Poffset;
				driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Poffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:ETFCi:REFerence
				List<int> value = driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Reference;
				driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Reference = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:ETFCi:NUMBer
				int value = driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Number;
				driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Number = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:GFACtor:HSUPa:ETFCi:BOOSt
				int value = driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Boost;
				driver.Configure.Uplink.Gfactor.Hsupa.Etfci.Boost = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:STATe
				foreach (TpcStateEnum x in new TpcStateEnum[] { TpcStateEnum.ALTernating, TpcStateEnum.CONTinous, TpcStateEnum.FAILed, TpcStateEnum.IDLE, TpcStateEnum.MAXPower, TpcStateEnum.MINPower, TpcStateEnum.MRESource, TpcStateEnum.SCHanged, TpcStateEnum.SCONflict, TpcStateEnum.SEARching, TpcStateEnum.SINGle, TpcStateEnum.TPLocked, TpcStateEnum.TPUNlocked, TpcStateEnum.TRANsition })
				{
					TpcStateEnum value = driver.Configure.Uplink.Tpc.State;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:PATTern
				string value = driver.Configure.Uplink.Tpc.Pattern;
				driver.Configure.Uplink.Tpc.Pattern = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:MODE
				foreach (TpcModeEnum x in new TpcModeEnum[] { TpcModeEnum.A1S1, TpcModeEnum.A1S2, TpcModeEnum.A2S1 })
				{
					driver.Configure.Uplink.Tpc.Mode = x;
					TpcModeEnum value = driver.Configure.Uplink.Tpc.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:SET
				RsCmwWcdmaSig_Configure_Uplink_Tpc_Set.Get_Data value = driver.Configure.Uplink.Tpc.Set.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:SET
				foreach (TpcSetTypeEnum x in new TpcSetTypeEnum[] { TpcSetTypeEnum.ALL0, TpcSetTypeEnum.ALL1, TpcSetTypeEnum.ALTernating, TpcSetTypeEnum.CLOop, TpcSetTypeEnum.CONTinuous, TpcSetTypeEnum.CTFC, TpcSetTypeEnum.DHIB, TpcSetTypeEnum.MPEDch, TpcSetTypeEnum.PHDown, TpcSetTypeEnum.PHUP, TpcSetTypeEnum.SAL0, TpcSetTypeEnum.SAL1, TpcSetTypeEnum.SALT, TpcSetTypeEnum.TSABc, TpcSetTypeEnum.TSE, TpcSetTypeEnum.TSEF, TpcSetTypeEnum.TSF, TpcSetTypeEnum.TSGH, TpcSetTypeEnum.ULCM })
				{
					driver.Configure.Uplink.Tpc.Set.Set(x);					
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:TPOWer:OFFSet
				double value = driver.Configure.Uplink.Tpc.Tpower.Offset;
				driver.Configure.Uplink.Tpc.Tpower.Offset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:TPOWer:REFerence
				foreach (ClosedLoopPowerEnum x in new ClosedLoopPowerEnum[] { ClosedLoopPowerEnum.DPCH, ClosedLoopPowerEnum.TOTal })
				{
					driver.Configure.Uplink.Tpc.Tpower.Reference = x;
					ClosedLoopPowerEnum value = driver.Configure.Uplink.Tpc.Tpower.Reference;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:MPEDch:STATe
				RsCmwWcdmaSig_Configure_Uplink_Tpc_Mpedch.State_Data value = driver.Configure.Uplink.Tpc.Mpedch.State;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:PRECondition
				driver.Configure.Uplink.Tpc.Precondition.Set();
				driver.Configure.Uplink.Tpc.Precondition.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPC:PEXecute
				driver.Configure.Uplink.Tpc.Pexecute.Set();
				driver.Configure.Uplink.Tpc.Pexecute.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:TSEF
				int value = driver.Configure.Uplink.Tpcset.Pconfig.Tsef;
				driver.Configure.Uplink.Tpcset.Pconfig.Tsef = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:TSGH
				int value = driver.Configure.Uplink.Tpcset.Pconfig.Tsgh;
				driver.Configure.Uplink.Tpcset.Pconfig.Tsgh = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:TSSegment
				bool value = driver.Configure.Uplink.Tpcset.Pconfig.TsSegment;
				driver.Configure.Uplink.Tpcset.Pconfig.TsSegment = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:PHDown
				int value = driver.Configure.Uplink.Tpcset.Pconfig.Phdown;
				driver.Configure.Uplink.Tpcset.Pconfig.Phdown = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:PHUP
				int value = driver.Configure.Uplink.Tpcset.Pconfig.Phup;
				driver.Configure.Uplink.Tpcset.Pconfig.Phup = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PCONfig:DHIB
				RsCmwWcdmaSig_Configure_Uplink_Tpcset_Pconfig.Dhib_Data value = driver.Configure.Uplink.Tpcset.Pconfig.Dhib;
				driver.Configure.Uplink.Tpcset.Pconfig.Dhib = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PRECondition:PHDown
				foreach (ConditionBenum x in new ConditionBenum[] { ConditionBenum.ALTernating, ConditionBenum.MAXPower, ConditionBenum.MINPower, ConditionBenum.TPOWer })
				{
					driver.Configure.Uplink.Tpcset.Precondition.Phdown = x;
					ConditionBenum value = driver.Configure.Uplink.Tpcset.Precondition.Phdown;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PRECondition:PHUP
				foreach (ConditionBenum x in new ConditionBenum[] { ConditionBenum.ALTernating, ConditionBenum.MAXPower, ConditionBenum.MINPower, ConditionBenum.TPOWer })
				{
					driver.Configure.Uplink.Tpcset.Precondition.Phup = x;
					ConditionBenum value = driver.Configure.Uplink.Tpcset.Precondition.Phup;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PRECondition:CONTinuous
				foreach (ConditionEnum x in new ConditionEnum[] { ConditionEnum.ALTernating, ConditionEnum.MAXPower, ConditionEnum.MINPower, ConditionEnum.NONE, ConditionEnum.TPOWer })
				{
					driver.Configure.Uplink.Tpcset.Precondition.Continuous = x;
					ConditionEnum value = driver.Configure.Uplink.Tpcset.Precondition.Continuous;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:UL:TPCSet:PRECondition:SINGle
				foreach (ConditionBenum x in new ConditionBenum[] { ConditionBenum.ALTernating, ConditionBenum.MAXPower, ConditionBenum.MINPower, ConditionBenum.TPOWer })
				{
					driver.Configure.Uplink.Tpcset.Precondition.Single = x;
					ConditionBenum value = driver.Configure.Uplink.Tpcset.Precondition.Single;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:SRBData
				RsCmwWcdmaSig_Configure_Connection.SrbSata_Data value = driver.Configure.Connection.SrbSata;
				driver.Configure.Connection.SrbSata = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:UETerminate
				foreach (TerminatingTypeEnum x in new TerminatingTypeEnum[] { TerminatingTypeEnum.RMC, TerminatingTypeEnum.SRB, TerminatingTypeEnum.TEST, TerminatingTypeEnum.VIDeo, TerminatingTypeEnum.VOICe })
				{
					driver.Configure.Connection.UeTerminate = x;
					TerminatingTypeEnum value = driver.Configure.Connection.UeTerminate;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:CID
				string value = driver.Configure.Connection.Cid;
				driver.Configure.Connection.Cid = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:DTX
				bool value = driver.Configure.Connection.Voice.Dtx;
				driver.Configure.Connection.Voice.Dtx = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:SOURce
				foreach (VoiceSourceEnum x in new VoiceSourceEnum[] { VoiceSourceEnum.LOOPback, VoiceSourceEnum.SPEech })
				{
					driver.Configure.Connection.Voice.Source = x;
					VoiceSourceEnum value = driver.Configure.Connection.Voice.Source;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:CODec
				foreach (VoiceCodecEnum x in new VoiceCodecEnum[] { VoiceCodecEnum.NB, VoiceCodecEnum.WB })
				{
					driver.Configure.Connection.Voice.Codec = x;
					VoiceCodecEnum value = driver.Configure.Connection.Voice.Codec;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:TFCI
				bool value = driver.Configure.Connection.Voice.Tfci;
				driver.Configure.Connection.Voice.Tfci = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:DELay:LOOPback
				double value = driver.Configure.Connection.Voice.Delay.Loopback;
				driver.Configure.Connection.Voice.Delay.Loopback = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:AMR:NARRow
				foreach (AmrCodecModeNarrowEnum x in new AmrCodecModeNarrowEnum[] { AmrCodecModeNarrowEnum.A, AmrCodecModeNarrowEnum.B, AmrCodecModeNarrowEnum.C, AmrCodecModeNarrowEnum.D, AmrCodecModeNarrowEnum.E, AmrCodecModeNarrowEnum.F, AmrCodecModeNarrowEnum.G, AmrCodecModeNarrowEnum.H, AmrCodecModeNarrowEnum.M })
				{
					driver.Configure.Connection.Voice.Amr.Narrow = x;
					AmrCodecModeNarrowEnum value = driver.Configure.Connection.Voice.Amr.Narrow;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VOICe:AMR:WIDE
				foreach (AmrCodecModeWideEnum x in new AmrCodecModeWideEnum[] { AmrCodecModeWideEnum.A, AmrCodecModeWideEnum.B, AmrCodecModeWideEnum.C, AmrCodecModeWideEnum.D, AmrCodecModeWideEnum.E, AmrCodecModeWideEnum.F, AmrCodecModeWideEnum.G, AmrCodecModeWideEnum.H, AmrCodecModeWideEnum.I, AmrCodecModeWideEnum.M, AmrCodecModeWideEnum.M1, AmrCodecModeWideEnum.M2 })
				{
					driver.Configure.Connection.Voice.Amr.Wide = x;
					AmrCodecModeWideEnum value = driver.Configure.Connection.Voice.Amr.Wide;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:KTLReconfig
				bool value = driver.Configure.Connection.Tmode.KtlreConfig;
				driver.Configure.Connection.Tmode.KtlreConfig = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:TYPE
				foreach (TestModeTypeEnum x in new TestModeTypeEnum[] { TestModeTypeEnum.BTFD, TestModeTypeEnum.FACH, TestModeTypeEnum.HSPA, TestModeTypeEnum.RHSPa, TestModeTypeEnum.RMC })
				{
					driver.Configure.Connection.Tmode.Type = x;
					TestModeTypeEnum value = driver.Configure.Connection.Tmode.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:BTFD:TFORmat
				foreach (BtfdDataRateEnum x in new BtfdDataRateEnum[] { BtfdDataRateEnum.R10K2, BtfdDataRateEnum.R12K2, BtfdDataRateEnum.R1K95, BtfdDataRateEnum.R4K75, BtfdDataRateEnum.R5K15, BtfdDataRateEnum.R5K9, BtfdDataRateEnum.R6K7, BtfdDataRateEnum.R7K4, BtfdDataRateEnum.R7K95 })
				{
					driver.Configure.Connection.Tmode.Btfd.Tformat = x;
					BtfdDataRateEnum value = driver.Configure.Connection.Tmode.Btfd.Tformat;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:DOMain
				foreach (RmcDomainEnum x in new RmcDomainEnum[] { RmcDomainEnum.CS, RmcDomainEnum.PS })
				{
					driver.Configure.Connection.Tmode.Rmc.Domain = x;
					RmcDomainEnum value = driver.Configure.Connection.Tmode.Rmc.Domain;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:DATA
				foreach (BitPatternEnum x in new BitPatternEnum[] { BitPatternEnum.ALL0, BitPatternEnum.ALL1, BitPatternEnum.ALTernating, BitPatternEnum.PRBS11, BitPatternEnum.PRBS13, BitPatternEnum.PRBS15, BitPatternEnum.PRBS9 })
				{
					driver.Configure.Connection.Tmode.Rmc.Data = x;
					BitPatternEnum value = driver.Configure.Connection.Tmode.Rmc.Data;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:DLRessources
				foreach (FilledBlocksEnum x in new FilledBlocksEnum[] { FilledBlocksEnum.P0031, FilledBlocksEnum.P0033, FilledBlocksEnum.P0036, FilledBlocksEnum.P0038, FilledBlocksEnum.P0042, FilledBlocksEnum.P0045, FilledBlocksEnum.P0050, FilledBlocksEnum.P0056, FilledBlocksEnum.P0062, FilledBlocksEnum.P0071, FilledBlocksEnum.P0083, FilledBlocksEnum.P0100, FilledBlocksEnum.P0125, FilledBlocksEnum.P0167, FilledBlocksEnum.P0250, FilledBlocksEnum.P0500, FilledBlocksEnum.P1000 })
				{
					driver.Configure.Connection.Tmode.Rmc.DlResources = x;
					FilledBlocksEnum value = driver.Configure.Connection.Tmode.Rmc.DlResources;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:UCRC
				bool value = driver.Configure.Connection.Tmode.Rmc.Ucrc;
				driver.Configure.Connection.Tmode.Rmc.Ucrc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:RLCMode
				foreach (RlcModeEnum x in new RlcModeEnum[] { RlcModeEnum.ACKNowledge, RlcModeEnum.TRANsparent })
				{
					driver.Configure.Connection.Tmode.Rmc.RlcMode = x;
					RlcModeEnum value = driver.Configure.Connection.Tmode.Rmc.RlcMode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:DRATe
				RsCmwWcdmaSig_Configure_Connection_Tmode_Rmc.Drate_Data value = driver.Configure.Connection.Tmode.Rmc.Drate;
				driver.Configure.Connection.Tmode.Rmc.Drate = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:RMC:TMODe
				foreach (UtranTestModeEnum x in new UtranTestModeEnum[] { UtranTestModeEnum.MODE1, UtranTestModeEnum.MODE2, UtranTestModeEnum.OFF })
				{
					driver.Configure.Connection.Tmode.Rmc.Tmode = x;
					UtranTestModeEnum value = driver.Configure.Connection.Tmode.Rmc.Tmode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:HSPA:PROCedure
				foreach (ProcedureEnum x in new ProcedureEnum[] { ProcedureEnum.CSOPs, ProcedureEnum.CSPS, ProcedureEnum.PS })
				{
					driver.Configure.Connection.Tmode.Hspa.Procedure = x;
					ProcedureEnum value = driver.Configure.Connection.Tmode.Hspa.Procedure;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:HSPA:DIRection
				foreach (HspaTestModeDirectionEnum x in new HspaTestModeDirectionEnum[] { HspaTestModeDirectionEnum.HSDPa, HspaTestModeDirectionEnum.HSPA })
				{
					driver.Configure.Connection.Tmode.Hspa.Direction = x;
					HspaTestModeDirectionEnum value = driver.Configure.Connection.Tmode.Hspa.Direction;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:HSPA:DATA
				foreach (BitPatternEnum x in new BitPatternEnum[] { BitPatternEnum.ALL0, BitPatternEnum.ALL1, BitPatternEnum.ALTernating, BitPatternEnum.PRBS11, BitPatternEnum.PRBS13, BitPatternEnum.PRBS15, BitPatternEnum.PRBS9 })
				{
					driver.Configure.Connection.Tmode.Hspa.Data = x;
					BitPatternEnum value = driver.Configure.Connection.Tmode.Hspa.Data;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:HSPA:EINSertion
				double value = driver.Configure.Connection.Tmode.Hspa.Einsertion;
				driver.Configure.Connection.Tmode.Hspa.Einsertion = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:TMODe:HSPA:USDU
				int value = driver.Configure.Connection.Tmode.Hspa.Usdu;
				driver.Configure.Connection.Tmode.Hspa.Usdu = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:DRATe
				RsCmwWcdmaSig_Configure_Connection_Packet.Drate_Data value = driver.Configure.Connection.Packet.Drate;
				driver.Configure.Connection.Packet.Drate = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:HSDPa:RWINdow
				RsCmwWcdmaSig_Configure_Connection_Packet_Hsdpa.Rwindow_Data value = driver.Configure.Connection.Packet.Hsdpa.Rwindow;
				driver.Configure.Connection.Packet.Hsdpa.Rwindow = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:HSDPa:TIMer
				RsCmwWcdmaSig_Configure_Connection_Packet_Hsdpa.Timer_Data value = driver.Configure.Connection.Packet.Hsdpa.Timer;
				driver.Configure.Connection.Packet.Hsdpa.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:NETWork:ENABle
				bool value = driver.Configure.Connection.Packet.Inactivity.Dch.Network.Enable;
				driver.Configure.Connection.Packet.Inactivity.Dch.Network.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:NETWork:TIMer
				int value = driver.Configure.Connection.Packet.Inactivity.Dch.Network.Timer;
				driver.Configure.Connection.Packet.Inactivity.Dch.Network.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:NETWork:DSTate
				foreach (DestinationStateEnum x in new DestinationStateEnum[] { DestinationStateEnum.CPCH, DestinationStateEnum.FACH, DestinationStateEnum.IDLE, DestinationStateEnum.UPCH })
				{
					driver.Configure.Connection.Packet.Inactivity.Dch.Network.Dstate = x;
					DestinationStateEnum value = driver.Configure.Connection.Packet.Inactivity.Dch.Network.Dstate;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:UEFDormancy:ENABle
				bool value = driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Enable;
				driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:UEFDormancy:TIMer
				int value = driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Timer;
				driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:DCH:UEFDormancy:DSTate
				foreach (DestinationStateEnum x in new DestinationStateEnum[] { DestinationStateEnum.CPCH, DestinationStateEnum.FACH, DestinationStateEnum.IDLE, DestinationStateEnum.UPCH })
				{
					driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Dstate = x;
					DestinationStateEnum value = driver.Configure.Connection.Packet.Inactivity.Dch.UefDormancy.Dstate;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:FACH:TIMer
				int value = driver.Configure.Connection.Packet.Inactivity.Fach.Timer;
				driver.Configure.Connection.Packet.Inactivity.Fach.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:FACH:DSTate
				foreach (DestinationStateEnum x in new DestinationStateEnum[] { DestinationStateEnum.CPCH, DestinationStateEnum.FACH, DestinationStateEnum.IDLE, DestinationStateEnum.UPCH })
				{
					driver.Configure.Connection.Packet.Inactivity.Fach.Dstate = x;
					DestinationStateEnum value = driver.Configure.Connection.Packet.Inactivity.Fach.Dstate;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:CPCH:TIMer
				int value = driver.Configure.Connection.Packet.Inactivity.Cpch.Timer;
				driver.Configure.Connection.Packet.Inactivity.Cpch.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:INACtivity:UPCH:TIMer
				int value = driver.Configure.Connection.Packet.Inactivity.Upch.Timer;
				driver.Configure.Connection.Packet.Inactivity.Upch.Timer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:ROHC:ENABle
				bool value = driver.Configure.Connection.Packet.Rohc.Enable;
				driver.Configure.Connection.Packet.Rohc.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:PACKet:ROHC:PROFiles
				RsCmwWcdmaSig_Configure_Connection_Packet_Rohc.Profiles_Data value = driver.Configure.Connection.Packet.Rohc.Profiles;
				driver.Configure.Connection.Packet.Rohc.Profiles = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:SRBSingle:TYPE
				foreach (SrbSingleTypeEnum x in new SrbSingleTypeEnum[] { SrbSingleTypeEnum.CDCH, SrbSingleTypeEnum.CFACh })
				{
					driver.Configure.Connection.SrbSingle.Type = x;
					SrbSingleTypeEnum value = driver.Configure.Connection.SrbSingle.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:VIDeo:DRATe
				foreach (VideoRateEnum x in new VideoRateEnum[] { VideoRateEnum.R64K })
				{
					VideoRateEnum value = driver.Configure.Connection.Video.Drate;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CONNection:CSWitched:CRELease
				foreach (CallReleaseEnum x in new CallReleaseEnum[] { CallReleaseEnum.LOCal, CallReleaseEnum.NORMal })
				{
					driver.Configure.Connection.Cswitched.Crelease = x;
					CallReleaseEnum value = driver.Configure.Connection.Cswitched.Crelease;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:IHMobility:HANDover
				foreach (HandoverEnum x in new HandoverEnum[] { HandoverEnum.PACKet, HandoverEnum.TM, HandoverEnum.VOICe })
				{
					driver.Configure.IhMobility.Handover = x;
					HandoverEnum value = driver.Configure.IhMobility.Handover;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:IHMobility:MTCS
				foreach (CsFallbackConnectionTypeEnum x in new CsFallbackConnectionTypeEnum[] { CsFallbackConnectionTypeEnum.TMRMc, CsFallbackConnectionTypeEnum.VOICe })
				{
					driver.Configure.IhMobility.Mtcs = x;
					CsFallbackConnectionTypeEnum value = driver.Configure.IhMobility.Mtcs;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:MRVersion
				foreach (MaxRelVersionEnum x in new MaxRelVersionEnum[] { MaxRelVersionEnum.AUTO, MaxRelVersionEnum.R10, MaxRelVersionEnum.R11, MaxRelVersionEnum.R5, MaxRelVersionEnum.R6, MaxRelVersionEnum.R7, MaxRelVersionEnum.R8, MaxRelVersionEnum.R9, MaxRelVersionEnum.R99 })
				{
					driver.Configure.Cell.MrVersion = x;
					MaxRelVersionEnum value = driver.Configure.Cell.MrVersion;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RSIGnaling
				bool value = driver.Configure.Cell.Rsignaling;
				driver.Configure.Cell.Rsignaling = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:PSDomain
				bool value = driver.Configure.Cell.Psdomain;
				driver.Configure.Cell.Psdomain = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:IDENtity
				double value = driver.Configure.Cell.Identity;
				driver.Configure.Cell.Identity = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:IDNode
				double value = driver.Configure.Cell.IdNode;
				driver.Configure.Cell.IdNode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RNC
				double value = driver.Configure.Cell.Rnc;
				driver.Configure.Cell.Rnc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:URA
				double value = driver.Configure.Cell.Ura;
				driver.Configure.Cell.Ura = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RAC
				double value = driver.Configure.Cell.Rac;
				driver.Configure.Cell.Rac = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:LAC
				double value = driver.Configure.Cell.Lac;
				driver.Configure.Cell.Lac = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:NTOPeration
				foreach (NtOperModeEnum x in new NtOperModeEnum[] { NtOperModeEnum.M1, NtOperModeEnum.M2 })
				{
					driver.Configure.Cell.NtOperation = x;
					NtOperModeEnum value = driver.Configure.Cell.NtOperation;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:MCC
				int value = driver.Configure.Cell.Mcc;
				driver.Configure.Cell.Mcc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:BINDicator
				bool value = driver.Configure.Cell.Bindicator;
				driver.Configure.Cell.Bindicator = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:SCODe
				double value = driver.Configure.Cell.Carrier.Scode;
				driver.Configure.Cell.Carrier.Scode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:CQI:ENABle
				bool value = driver.Configure.Cell.Carrier.Hsdpa.Cqi.Enable;
				driver.Configure.Cell.Carrier.Hsdpa.Cqi.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:CQI:FIXed
				int value = driver.Configure.Cell.Carrier.Hsdpa.Cqi.Fixed;
				driver.Configure.Cell.Carrier.Hsdpa.Cqi.Fixed = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:CQI:CONFormance
				int value = driver.Configure.Cell.Carrier.Hsdpa.Cqi.Conformance;
				driver.Configure.Cell.Carrier.Hsdpa.Cqi.Conformance = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:ENABle
				bool value = driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Enable;
				driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:MODulation
				foreach (HsdpaModulationEnum x in new HsdpaModulationEnum[] { HsdpaModulationEnum.Q16, HsdpaModulationEnum.Q64, HsdpaModulationEnum.QPSK })
				{
					driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Modulation = x;
					HsdpaModulationEnum value = driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Modulation;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:NCODes
				int value = driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Ncodes;
				driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Ncodes = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:TTI
				int value = driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Tti;
				driver.Configure.Cell.Carrier.Hsdpa.UserDefined.Tti = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:TBLock
				RsCmwWcdmaSig_Configure_Cell_Carrier_Hsdpa_UserDefined_TransportBlock.Get_Data value = driver.Configure.Cell.Carrier.Hsdpa.UserDefined.TransportBlock.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSDPa:UDEFined:TBLock
				driver.Configure.Cell.Carrier.Hsdpa.UserDefined.TransportBlock.Set(1);				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ENABle
				bool value = driver.Configure.Cell.Carrier.Hsupa.Enable;
				driver.Configure.Cell.Carrier.Hsupa.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EHRCh:FUFDummies
				bool value = driver.Configure.Cell.Carrier.Hsupa.Ehrch.FufDummies;
				driver.Configure.Cell.Carrier.Hsupa.Ehrch.FufDummies = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:UEID
				RsCmwWcdmaSig_Configure_Cell_Carrier_Hsupa_Eagch.Ueid_Data value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Ueid;
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Ueid = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:LENGth
				int value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Length;
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Length = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:INDex
				List<int> value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Index;
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Index = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:SCOPe
				List<bool> value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Scope;
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Scope = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:TYPE
				List<bool> value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Type;
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Type = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:REPetition
				foreach (RepetitionBenum x in new RepetitionBenum[] { RepetitionBenum.CONTinuous, RepetitionBenum.ONCE, RepetitionBenum.SGINit })
				{
					driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Repetition = x;
					RepetitionBenum value = driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EAGCh:PATTern:EXECute
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Execute.Set();
				driver.Configure.Cell.Carrier.Hsupa.Eagch.Pattern.Execute.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EHICh:MODE
				foreach (EhichIndicatorModeEnum x in new EhichIndicatorModeEnum[] { EhichIndicatorModeEnum.ACK, EhichIndicatorModeEnum.ALTernating, EhichIndicatorModeEnum.CRC, EhichIndicatorModeEnum.DTX, EhichIndicatorModeEnum.NACK })
				{
					driver.Configure.Cell.Carrier.Hsupa.Ehich.Mode = x;
					EhichIndicatorModeEnum value = driver.Configure.Cell.Carrier.Hsupa.Ehich.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:EHICh:SIGNature
				int value = driver.Configure.Cell.Carrier.Hsupa.Ehich.Signature;
				driver.Configure.Cell.Carrier.Hsupa.Ehich.Signature = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ERGCh:MODE
				foreach (ErgchIndicatorModeEnum x in new ErgchIndicatorModeEnum[] { ErgchIndicatorModeEnum.ALTernating, ErgchIndicatorModeEnum.CONTinuous, ErgchIndicatorModeEnum.DOWN, ErgchIndicatorModeEnum.DTX, ErgchIndicatorModeEnum.HARQ, ErgchIndicatorModeEnum.SINGle, ErgchIndicatorModeEnum.UP })
				{
					driver.Configure.Cell.Carrier.Hsupa.Ergch.Mode = x;
					ErgchIndicatorModeEnum value = driver.Configure.Cell.Carrier.Hsupa.Ergch.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ERGCh:SIGNature
				int value = driver.Configure.Cell.Carrier.Hsupa.Ergch.Signature;
				driver.Configure.Cell.Carrier.Hsupa.Ergch.Signature = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ERGCh:PATTern:LENGth
				int value = driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Length;
				driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Length = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ERGCh:PATTern
				string value = driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Value;
				driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ERGCh:PATTern:EXECute
				driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Execute.Set();
				driver.Configure.Cell.Carrier.Hsupa.Ergch.Pattern.Execute.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HSUPa:ETFCi:MSET
				int value = driver.Configure.Cell.Carrier.Hsupa.Etfci.Mset;
				driver.Configure.Cell.Carrier.Hsupa.Etfci.Mset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HORDer:DL
				bool value = driver.Configure.Cell.Carrier.Horder.Downlink;
				driver.Configure.Cell.Carrier.Horder.Downlink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CARRier<carrier>:HORDer:UL
				bool value = driver.Configure.Cell.Carrier.Horder.Uplink;
				driver.Configure.Cell.Carrier.Horder.Uplink = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:RRCRequest
				foreach (RejectCauseEnum x in new RejectCauseEnum[] { RejectCauseEnum.CSCongestion, RejectCauseEnum.CSUNspecific, RejectCauseEnum.OFF, RejectCauseEnum.ON, RejectCauseEnum.PSCongestion, RejectCauseEnum.PSUNspecific })
				{
					driver.Configure.Cell.Rcause.RrcRequest = x;
					RejectCauseEnum value = driver.Configure.Cell.Rcause.RrcRequest;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:LOCation
				foreach (RejectionCauseAenum x in new RejectionCauseAenum[] { RejectionCauseAenum.C100, RejectionCauseAenum.C101, RejectionCauseAenum.C11, RejectionCauseAenum.C111, RejectionCauseAenum.C12, RejectionCauseAenum.C13, RejectionCauseAenum.C15, RejectionCauseAenum.C17, RejectionCauseAenum.C2, RejectionCauseAenum.C20, RejectionCauseAenum.C21, RejectionCauseAenum.C22, RejectionCauseAenum.C23, RejectionCauseAenum.C25, RejectionCauseAenum.C3, RejectionCauseAenum.C32, RejectionCauseAenum.C33, RejectionCauseAenum.C34, RejectionCauseAenum.C38, RejectionCauseAenum.C4, RejectionCauseAenum.C48, RejectionCauseAenum.C5, RejectionCauseAenum.C6, RejectionCauseAenum.C95, RejectionCauseAenum.C96, RejectionCauseAenum.C97, RejectionCauseAenum.C98, RejectionCauseAenum.C99, RejectionCauseAenum.OFF, RejectionCauseAenum.ON })
				{
					driver.Configure.Cell.Rcause.Location = x;
					RejectionCauseAenum value = driver.Configure.Cell.Rcause.Location;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:ATTach
				foreach (RejectionCauseBenum x in new RejectionCauseBenum[] { RejectionCauseBenum.C10, RejectionCauseBenum.C100, RejectionCauseBenum.C101, RejectionCauseBenum.C11, RejectionCauseBenum.C111, RejectionCauseBenum.C12, RejectionCauseBenum.C13, RejectionCauseBenum.C14, RejectionCauseBenum.C15, RejectionCauseBenum.C16, RejectionCauseBenum.C17, RejectionCauseBenum.C2, RejectionCauseBenum.C20, RejectionCauseBenum.C21, RejectionCauseBenum.C22, RejectionCauseBenum.C23, RejectionCauseBenum.C25, RejectionCauseBenum.C28, RejectionCauseBenum.C3, RejectionCauseBenum.C32, RejectionCauseBenum.C33, RejectionCauseBenum.C34, RejectionCauseBenum.C38, RejectionCauseBenum.C4, RejectionCauseBenum.C40, RejectionCauseBenum.C48, RejectionCauseBenum.C5, RejectionCauseBenum.C6, RejectionCauseBenum.C7, RejectionCauseBenum.C8, RejectionCauseBenum.C9, RejectionCauseBenum.C95, RejectionCauseBenum.C96, RejectionCauseBenum.C97, RejectionCauseBenum.C98, RejectionCauseBenum.C99, RejectionCauseBenum.OFF, RejectionCauseBenum.ON })
				{
					driver.Configure.Cell.Rcause.Attach = x;
					RejectionCauseBenum value = driver.Configure.Cell.Rcause.Attach;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:ROUTing
				foreach (RejectionCauseBenum x in new RejectionCauseBenum[] { RejectionCauseBenum.C10, RejectionCauseBenum.C100, RejectionCauseBenum.C101, RejectionCauseBenum.C11, RejectionCauseBenum.C111, RejectionCauseBenum.C12, RejectionCauseBenum.C13, RejectionCauseBenum.C14, RejectionCauseBenum.C15, RejectionCauseBenum.C16, RejectionCauseBenum.C17, RejectionCauseBenum.C2, RejectionCauseBenum.C20, RejectionCauseBenum.C21, RejectionCauseBenum.C22, RejectionCauseBenum.C23, RejectionCauseBenum.C25, RejectionCauseBenum.C28, RejectionCauseBenum.C3, RejectionCauseBenum.C32, RejectionCauseBenum.C33, RejectionCauseBenum.C34, RejectionCauseBenum.C38, RejectionCauseBenum.C4, RejectionCauseBenum.C40, RejectionCauseBenum.C48, RejectionCauseBenum.C5, RejectionCauseBenum.C6, RejectionCauseBenum.C7, RejectionCauseBenum.C8, RejectionCauseBenum.C9, RejectionCauseBenum.C95, RejectionCauseBenum.C96, RejectionCauseBenum.C97, RejectionCauseBenum.C98, RejectionCauseBenum.C99, RejectionCauseBenum.OFF, RejectionCauseBenum.ON })
				{
					driver.Configure.Cell.Rcause.Routing = x;
					RejectionCauseBenum value = driver.Configure.Cell.Rcause.Routing;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:CSRequest
				foreach (RejectionCauseAenum x in new RejectionCauseAenum[] { RejectionCauseAenum.C100, RejectionCauseAenum.C101, RejectionCauseAenum.C11, RejectionCauseAenum.C111, RejectionCauseAenum.C12, RejectionCauseAenum.C13, RejectionCauseAenum.C15, RejectionCauseAenum.C17, RejectionCauseAenum.C2, RejectionCauseAenum.C20, RejectionCauseAenum.C21, RejectionCauseAenum.C22, RejectionCauseAenum.C23, RejectionCauseAenum.C25, RejectionCauseAenum.C3, RejectionCauseAenum.C32, RejectionCauseAenum.C33, RejectionCauseAenum.C34, RejectionCauseAenum.C38, RejectionCauseAenum.C4, RejectionCauseAenum.C48, RejectionCauseAenum.C5, RejectionCauseAenum.C6, RejectionCauseAenum.C95, RejectionCauseAenum.C96, RejectionCauseAenum.C97, RejectionCauseAenum.C98, RejectionCauseAenum.C99, RejectionCauseAenum.OFF, RejectionCauseAenum.ON })
				{
					driver.Configure.Cell.Rcause.CsRequest = x;
					RejectionCauseAenum value = driver.Configure.Cell.Rcause.CsRequest;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RCAuse:CSTYpe
				foreach (CmserRejectTypeEnum x in new CmserRejectTypeEnum[] { CmserRejectTypeEnum.ECALl, CmserRejectTypeEnum.ECSMs, CmserRejectTypeEnum.NCALl, CmserRejectTypeEnum.NCECall, CmserRejectTypeEnum.NCSMs, CmserRejectTypeEnum.NESMs, CmserRejectTypeEnum.SMS })
				{
					driver.Configure.Cell.Rcause.CsType = x;
					CmserRejectTypeEnum value = driver.Configure.Cell.Rcause.CsType;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:MOC
				int value = driver.Configure.Cell.Timeout.Moc;
				driver.Configure.Cell.Timeout.Moc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:ATOFfset
				int value = driver.Configure.Cell.Timeout.AtOffset;
				driver.Configure.Cell.Timeout.AtOffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:PPIF
				int value = driver.Configure.Cell.Timeout.Ppif;
				driver.Configure.Cell.Timeout.Ppif = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:PREPetitions
				int value = driver.Configure.Cell.Timeout.Prepetitions;
				driver.Configure.Cell.Timeout.Prepetitions = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:OSYNch
				int value = driver.Configure.Cell.Timeout.Osynch;
				driver.Configure.Cell.Timeout.Osynch = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:N<nr>
				CounterValueEnum value = driver.Configure.Cell.Timeout.N.Get(CounterNoRepCap.Default);
				value = driver.Configure.Cell.Timeout.N.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:N<nr>
				foreach (CounterValueEnum x in new CounterValueEnum[] { CounterValueEnum.N1, CounterValueEnum.N10, CounterValueEnum.N100, CounterValueEnum.N2, CounterValueEnum.N20, CounterValueEnum.N200, CounterValueEnum.N4, CounterValueEnum.N50 })
				{
					driver.Configure.Cell.Timeout.N.Set(x);
					driver.Configure.Cell.Timeout.N.Set(x, CounterNoRepCap.Default);
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:T<nr>
				int value = driver.Configure.Cell.Timeout.T.Get(TimerRepCap.Default);
				value = driver.Configure.Cell.Timeout.T.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TOUT:T<nr>
				driver.Configure.Cell.Timeout.T.Set(1, TimerRepCap.Default);
				driver.Configure.Cell.Timeout.T.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:REQuest:IMEI
				bool value = driver.Configure.Cell.Request.Imei;
				driver.Configure.Cell.Request.Imei = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:REQuest:ADETach
				bool value = driver.Configure.Cell.Request.Adetach;
				driver.Configure.Cell.Request.Adetach = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:REQuest:RCUR
				bool value = driver.Configure.Cell.Request.Rcur;
				driver.Configure.Cell.Request.Rcur = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:CIPHering
				foreach (CipherEnum x in new CipherEnum[] { CipherEnum.UEA0, CipherEnum.UEA1, CipherEnum.UEA2 })
				{
					driver.Configure.Cell.Security.Ciphering = x;
					CipherEnum value = driver.Configure.Cell.Security.Ciphering;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:OPC
				double value = driver.Configure.Cell.Security.Opc;
				driver.Configure.Cell.Security.Opc = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:SIMCard
				foreach (SimCardTypeEnum x in new SimCardTypeEnum[] { SimCardTypeEnum.C2G, SimCardTypeEnum.C3G, SimCardTypeEnum.MILenage })
				{
					driver.Configure.Cell.Security.SimCard = x;
					SimCardTypeEnum value = driver.Configure.Cell.Security.SimCard;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:SKEY
				double value = driver.Configure.Cell.Security.Skey;
				driver.Configure.Cell.Security.Skey = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:ENABle
				bool value = driver.Configure.Cell.Security.Enable;
				driver.Configure.Cell.Security.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SECurity:AUTHenticat
				bool value = driver.Configure.Cell.Security.Authenticate;
				driver.Configure.Cell.Security.Authenticate = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:UEIDentity:FILTer
				bool value = driver.Configure.Cell.UeIdentity.Filter;
				driver.Configure.Cell.UeIdentity.Filter = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:UEIDentity:IMSI
				string value = driver.Configure.Cell.UeIdentity.Imsi;
				driver.Configure.Cell.UeIdentity.Imsi = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:UEIDentity:USE
				foreach (EnableEnum x in new EnableEnum[] { EnableEnum.ON })
				{
					driver.Configure.Cell.UeIdentity.Use = x;
					EnableEnum value = driver.Configure.Cell.UeIdentity.Use;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:MNC:DIGits
				foreach (NrOfDigitsEnum x in new NrOfDigitsEnum[] { NrOfDigitsEnum.D2, NrOfDigitsEnum.D3 })
				{
					driver.Configure.Cell.Mnc.Digits = x;
					NrOfDigitsEnum value = driver.Configure.Cell.Mnc.Digits;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:MNC
				RsCmwWcdmaSig_Configure_Cell_Mnc.Value_Data value = driver.Configure.Cell.Mnc.Value;
				driver.Configure.Cell.Mnc.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RESelection:SEARch
				RsCmwWcdmaSig_Configure_Cell_ReSelection.Search_Data value = driver.Configure.Cell.ReSelection.Search;
				driver.Configure.Cell.ReSelection.Search = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RESelection:QUALity
				RsCmwWcdmaSig_Configure_Cell_ReSelection.Quality_Data value = driver.Configure.Cell.ReSelection.Quality;
				driver.Configure.Cell.ReSelection.Quality = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:RESelection:TIME
				double value = driver.Configure.Cell.ReSelection.Time;
				driver.Configure.Cell.ReSelection.Time = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Cell.Time.Tsource = x;
					SourceTimeEnum value = driver.Configure.Cell.Time.Tsource;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:DATE
				RsCmwWcdmaSig_Configure_Cell_Time.Date_Data value = driver.Configure.Cell.Time.Date;
				driver.Configure.Cell.Time.Date = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:TIME
				RsCmwWcdmaSig_Configure_Cell_Time.Time_Data value = driver.Configure.Cell.Time.Time;
				driver.Configure.Cell.Time.Time = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:DSTime
				foreach (DsTimeEnum x in new DsTimeEnum[] { DsTimeEnum.OFF, DsTimeEnum.ON, DsTimeEnum.P1H, DsTimeEnum.P2H })
				{
					driver.Configure.Cell.Time.DaylightSavingTime = x;
					DsTimeEnum value = driver.Configure.Cell.Time.DaylightSavingTime;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:LTZoffset
				double value = driver.Configure.Cell.Time.LtzOffset;
				driver.Configure.Cell.Time.LtzOffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:SREGister
				bool value = driver.Configure.Cell.Time.Sregister;
				driver.Configure.Cell.Time.Sregister = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:SNName
				bool value = driver.Configure.Cell.Time.Snname;
				driver.Configure.Cell.Time.Snname = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:TIME:SNOW
				driver.Configure.Cell.Time.Snow.Set();
				driver.Configure.Cell.Time.Snow.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SYNC:ZONE
				foreach (ZoneEnum x in new ZoneEnum[] { ZoneEnum.NONE, ZoneEnum.Z1 })
				{
					driver.Configure.Cell.Sync.Zone = x;
					ZoneEnum value = driver.Configure.Cell.Sync.Zone;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:SYNC:OFFSet
				double value = driver.Configure.Cell.Sync.Offset;
				driver.Configure.Cell.Sync.Offset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:ANRFactor
				int value = driver.Configure.Cell.Hsdpa.AnrFactor;
				driver.Configure.Cell.Hsdpa.AnrFactor = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:TYPE
				foreach (ChannelTypeEnum x in new ChannelTypeEnum[] { ChannelTypeEnum.CQI, ChannelTypeEnum.FIXed, ChannelTypeEnum.UDEFined })
				{
					driver.Configure.Cell.Hsdpa.Type = x;
					ChannelTypeEnum value = driver.Configure.Cell.Hsdpa.Type;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UECategory:MANual
				int value = driver.Configure.Cell.Hsdpa.UeCategory.Manual;
				driver.Configure.Cell.Hsdpa.UeCategory.Manual = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UECategory:REPorted
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_UeCategory_Reported.Get_Data value = driver.Configure.Cell.Hsdpa.UeCategory.Reported.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UECategory:REPorted
				driver.Configure.Cell.Hsdpa.UeCategory.Reported.Set(false);				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:FIXed:HSET
				foreach (HsetFixedEnum x in new HsetFixedEnum[] { HsetFixedEnum.H1AI, HsetFixedEnum.H1BI, HsetFixedEnum.H1CI, HsetFixedEnum.H1M1, HsetFixedEnum.H1M2, HsetFixedEnum.H1MI, HsetFixedEnum.H2M1, HsetFixedEnum.H2M2, HsetFixedEnum.H3A1, HsetFixedEnum.H3A2, HsetFixedEnum.H3B1, HsetFixedEnum.H3B2, HsetFixedEnum.H3C1, HsetFixedEnum.H3C2, HsetFixedEnum.H3M1, HsetFixedEnum.H3M2, HsetFixedEnum.H4M1, HsetFixedEnum.H5M1, HsetFixedEnum.H6A1, HsetFixedEnum.H6A2, HsetFixedEnum.H6B1, HsetFixedEnum.H6B2, HsetFixedEnum.H6C1, HsetFixedEnum.H6C2, HsetFixedEnum.H6M1, HsetFixedEnum.H6M2, HsetFixedEnum.H8A3, HsetFixedEnum.H8AI, HsetFixedEnum.H8B3, HsetFixedEnum.H8BI, HsetFixedEnum.H8C3, HsetFixedEnum.H8CI, HsetFixedEnum.H8M3, HsetFixedEnum.H8MI, HsetFixedEnum.H8MT, HsetFixedEnum.HAA1, HsetFixedEnum.HAA2, HsetFixedEnum.HAB1, HsetFixedEnum.HAB2, HsetFixedEnum.HAC1, HsetFixedEnum.HAC2, HsetFixedEnum.HAM1, HsetFixedEnum.HAM2, HsetFixedEnum.HCM1, HsetFixedEnum.HCMT })
				{
					driver.Configure.Cell.Hsdpa.Fixed.Hset = x;
					HsetFixedEnum value = driver.Configure.Cell.Hsdpa.Fixed.Hset;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RFACtor
				int value = driver.Configure.Cell.Hsdpa.Cqi.Rfactor;
				driver.Configure.Cell.Hsdpa.Cqi.Rfactor = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:FBCYcle
				double value = driver.Configure.Cell.Hsdpa.Cqi.FbCycle;
				driver.Configure.Cell.Hsdpa.Cqi.FbCycle = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:TTI
				int value = driver.Configure.Cell.Hsdpa.Cqi.Tti;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:HARQ
				int value = driver.Configure.Cell.Hsdpa.Cqi.Harq;
				driver.Configure.Cell.Hsdpa.Cqi.Harq = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:TINDex
				foreach (TableIndexEnum x in new TableIndexEnum[] { TableIndexEnum.CONFormance, TableIndexEnum.FIXed, TableIndexEnum.FOLLow, TableIndexEnum.SEQuence })
				{
					driver.Configure.Cell.Hsdpa.Cqi.Tindex = x;
					TableIndexEnum value = driver.Configure.Cell.Hsdpa.Cqi.Tindex;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:SEQuence
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi.Sequence_Data value = driver.Configure.Cell.Hsdpa.Cqi.Sequence;
				driver.Configure.Cell.Hsdpa.Cqi.Sequence = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:FOLLow
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi.Follow_Data value = driver.Configure.Cell.Hsdpa.Cqi.Follow;
				driver.Configure.Cell.Hsdpa.Cqi.Follow = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:CONFormance:MODE
				bool value = driver.Configure.Cell.Hsdpa.Cqi.Conformance.Mode;
				driver.Configure.Cell.Hsdpa.Cqi.Conformance.Mode = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QPSK:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi_RvcSequences_Qpsk.UserDefined_Data value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qpsk.UserDefined;
				driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qpsk.UserDefined = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QPSK
				foreach (RvcSequenceEnum x in new RvcSequenceEnum[] { RvcSequenceEnum.S1, RvcSequenceEnum.S2, RvcSequenceEnum.S3, RvcSequenceEnum.S4, RvcSequenceEnum.S5, RvcSequenceEnum.S6, RvcSequenceEnum.S7, RvcSequenceEnum.UDEFined })
				{
					driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qpsk.Value = x;
					RvcSequenceEnum value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qpsk.Value;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QAM<nr>
				RvcSequenceEnum value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.Get(QuadratureAMRepCap.Default);
				value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QAM<nr>
				foreach (RvcSequenceEnum x in new RvcSequenceEnum[] { RvcSequenceEnum.S1, RvcSequenceEnum.S2, RvcSequenceEnum.S3, RvcSequenceEnum.S4, RvcSequenceEnum.S5, RvcSequenceEnum.S6, RvcSequenceEnum.S7, RvcSequenceEnum.UDEFined })
				{
					driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.Set(x);
					driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.Set(x, QuadratureAMRepCap.Default);
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QAM<nr>:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi_RvcSequences_Qam_UserDefined.UserDefined_Data value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.UserDefined.Get(QuadratureAMRepCap.Default);
				value = driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.UserDefined.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:CQI:RVCSequences:QAM<nr>:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi_RvcSequences_Qam_UserDefined.UserDefined_Data value = new RsCmwWcdmaSig_Configure_Cell_Hsdpa_Cqi_RvcSequences_Qam_UserDefined.UserDefined_Data();
				driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.UserDefined.Set(value, QuadratureAMRepCap.Default);
				driver.Configure.Cell.Hsdpa.Cqi.RvcSequences.Qam.UserDefined.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:HARQ
				int value = driver.Configure.Cell.Hsdpa.UserDefined.Harq;
				driver.Configure.Cell.Hsdpa.UserDefined.Harq = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:IRBuffer
				int value = driver.Configure.Cell.Hsdpa.UserDefined.IrBuffer;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QPSK:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_UserDefined_RvcSequences_Qpsk.Udefined_Data value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qpsk.Udefined;
				driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qpsk.Udefined = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QPSK
				foreach (RvcSequenceEnum x in new RvcSequenceEnum[] { RvcSequenceEnum.S1, RvcSequenceEnum.S2, RvcSequenceEnum.S3, RvcSequenceEnum.S4, RvcSequenceEnum.S5, RvcSequenceEnum.S6, RvcSequenceEnum.S7, RvcSequenceEnum.UDEFined })
				{
					driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qpsk.Value = x;
					RvcSequenceEnum value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qpsk.Value;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QAM<nr>
				RvcSequenceEnum value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Get(QuadratureAMRepCap.Default);
				value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QAM<nr>
				foreach (RvcSequenceEnum x in new RvcSequenceEnum[] { RvcSequenceEnum.S1, RvcSequenceEnum.S2, RvcSequenceEnum.S3, RvcSequenceEnum.S4, RvcSequenceEnum.S5, RvcSequenceEnum.S6, RvcSequenceEnum.S7, RvcSequenceEnum.UDEFined })
				{
					driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Set(x);
					driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Set(x, QuadratureAMRepCap.Default);
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QAM<nr>:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_UserDefined_RvcSequences_Qam_Udefined.Udefined_Data value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Udefined.Get(QuadratureAMRepCap.Default);
				value = driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Udefined.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSDPa:UDEFined:RVCSequences:QAM<nr>:UDEFined
				RsCmwWcdmaSig_Configure_Cell_Hsdpa_UserDefined_RvcSequences_Qam_Udefined.Udefined_Data value = new RsCmwWcdmaSig_Configure_Cell_Hsdpa_UserDefined_RvcSequences_Qam_Udefined.Udefined_Data();
				driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Udefined.Set(value, QuadratureAMRepCap.Default);
				driver.Configure.Cell.Hsdpa.UserDefined.RvcSequences.Qam.Udefined.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:TTI
				foreach (TransTimeIntervalEnum x in new TransTimeIntervalEnum[] { TransTimeIntervalEnum.M10, TransTimeIntervalEnum.M2 })
				{
					driver.Configure.Cell.Hsupa.Tti = x;
					TransTimeIntervalEnum value = driver.Configure.Cell.Hsupa.Tti;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HRVersion
				foreach (HrVersionEnum x in new HrVersionEnum[] { HrVersionEnum.RV0, HrVersionEnum.TABLe })
				{
					driver.Configure.Cell.Hsupa.HrVersion = x;
					HrVersionEnum value = driver.Configure.Cell.Hsupa.HrVersion;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HBDConition
				double value = driver.Configure.Cell.Hsupa.HbdCondition;
				driver.Configure.Cell.Hsupa.HbdCondition = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:PLPLnonmax
				double value = driver.Configure.Cell.Hsupa.PlPlNonMax;
				driver.Configure.Cell.Hsupa.PlPlNonMax = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:MCCode
				foreach (MaxChanCodeEnum x in new MaxChanCodeEnum[] { MaxChanCodeEnum.S16, MaxChanCodeEnum.S22, MaxChanCodeEnum.S224, MaxChanCodeEnum.S24, MaxChanCodeEnum.S32, MaxChanCodeEnum.S4, MaxChanCodeEnum.S64, MaxChanCodeEnum.S8 })
				{
					driver.Configure.Cell.Hsupa.Mccode = x;
					MaxChanCodeEnum value = driver.Configure.Cell.Hsupa.Mccode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:ISGRant
				RsCmwWcdmaSig_Configure_Cell_Hsupa.IsGrant_Data value = driver.Configure.Cell.Hsupa.IsGrant;
				driver.Configure.Cell.Hsupa.IsGrant = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:MODulation
				foreach (HsupaModulationEnum x in new HsupaModulationEnum[] { HsupaModulationEnum.Q16, HsupaModulationEnum.QPSK })
				{
					driver.Configure.Cell.Hsupa.Modulation = x;
					HsupaModulationEnum value = driver.Configure.Cell.Hsupa.Modulation;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:PDU:FLEXible
				int value = driver.Configure.Cell.Hsupa.Pdu.Flexible;
				driver.Configure.Cell.Hsupa.Pdu.Flexible = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:PDU
				int value = driver.Configure.Cell.Hsupa.Pdu.Value;
				driver.Configure.Cell.Hsupa.Pdu.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:UECategory:MANual
				int value = driver.Configure.Cell.Hsupa.UeCategory.Manual;
				driver.Configure.Cell.Hsupa.UeCategory.Manual = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:UECategory:REPorted
				RsCmwWcdmaSig_Configure_Cell_Hsupa_UeCategory_Reported.Get_Data value = driver.Configure.Cell.Hsupa.UeCategory.Reported.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:UECategory:REPorted
				driver.Configure.Cell.Hsupa.UeCategory.Reported.Set(false);				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:EAGCh:TINDex
				int value = driver.Configure.Cell.Hsupa.Eagch.Tindex;
				driver.Configure.Cell.Hsupa.Eagch.Tindex = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:EAGCh:UTTI
				foreach (UnscheduledTransTypeEnum x in new UnscheduledTransTypeEnum[] { UnscheduledTransTypeEnum.DTX, UnscheduledTransTypeEnum.DUMMy })
				{
					driver.Configure.Cell.Hsupa.Eagch.Utti = x;
					UnscheduledTransTypeEnum value = driver.Configure.Cell.Hsupa.Eagch.Utti;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HORDer:SDCorder
				bool value = driver.Configure.Cell.Hsupa.Horder.SdcOrder;
				driver.Configure.Cell.Hsupa.Horder.SdcOrder = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HORDer:SUForder
				bool value = driver.Configure.Cell.Hsupa.Horder.Suforder;
				driver.Configure.Cell.Hsupa.Horder.Suforder = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HORDer:SEND
				RsCmwWcdmaSig_Configure_Cell_Hsupa_Horder_Send.Get_Data value = driver.Configure.Cell.Hsupa.Horder.Send.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HORDer:SEND
				driver.Configure.Cell.Hsupa.Horder.Send.Set();
				driver.Configure.Cell.Hsupa.Horder.Send.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:ETFCi:TINDex
				int value = driver.Configure.Cell.Hsupa.Etfci.Tindex;
				driver.Configure.Cell.Hsupa.Etfci.Tindex = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HARQ:POFFset
				double value = driver.Configure.Cell.Hsupa.Harq.Poffset;
				driver.Configure.Cell.Hsupa.Harq.Poffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HSUPa:HARQ:RETX
				int value = driver.Configure.Cell.Hsupa.Harq.ReTx;
				driver.Configure.Cell.Hsupa.Harq.ReTx = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HORDer:SEND
				RsCmwWcdmaSig_Configure_Cell_Horder_Send.Get_Data value = driver.Configure.Cell.Horder.Send.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:HORDer:SEND
				driver.Configure.Cell.Horder.Send.Set();
				driver.Configure.Cell.Horder.Send.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:SFORmat
				int value = driver.Configure.Cell.Cpc.Sformat;
				driver.Configure.Cell.Cpc.Sformat = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DTRX:DELay
				int value = driver.Configure.Cell.Cpc.Dtrx.Delay;
				driver.Configure.Cell.Cpc.Dtrx.Delay = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DTRX:OFFSet
				int value = driver.Configure.Cell.Cpc.Dtrx.Offset;
				driver.Configure.Cell.Cpc.Dtrx.Offset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:ENABle
				bool value = driver.Configure.Cell.Cpc.Udtx.Enable;
				driver.Configure.Cell.Cpc.Udtx.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:LPLength
				int value = driver.Configure.Cell.Cpc.Udtx.LpLength;
				driver.Configure.Cell.Cpc.Udtx.LpLength = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CQITimer
				int value = driver.Configure.Cell.Cpc.Udtx.CqiTimer;
				driver.Configure.Cell.Cpc.Udtx.CqiTimer = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:APATtern:TTI<ms>
				int value = driver.Configure.Cell.Cpc.Udtx.Cycle.Apattern.Tti.Get(CycleRepCap.Default, TransTimeIntervalRepCap.Default);
				value = driver.Configure.Cell.Cpc.Udtx.Cycle.Apattern.Tti.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:APATtern:TTI<ms>
				driver.Configure.Cell.Cpc.Udtx.Cycle.Apattern.Tti.Set(1, CycleRepCap.Default, TransTimeIntervalRepCap.Default);
				driver.Configure.Cell.Cpc.Udtx.Cycle.Apattern.Tti.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:BURSt
				int value = driver.Configure.Cell.Cpc.Udtx.Cycle.Burst.Get(CycleRepCap.Default);
				value = driver.Configure.Cell.Cpc.Udtx.Cycle.Burst.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:BURSt
				driver.Configure.Cell.Cpc.Udtx.Cycle.Burst.Set(1, CycleRepCap.Default);
				driver.Configure.Cell.Cpc.Udtx.Cycle.Burst.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:ITHReshold
				int value = driver.Configure.Cell.Cpc.Udtx.Cycle.Ithreshold.Get(CycleRepCap.Default);
				value = driver.Configure.Cell.Cpc.Udtx.Cycle.Ithreshold.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:ITHReshold
				driver.Configure.Cell.Cpc.Udtx.Cycle.Ithreshold.Set(1, CycleRepCap.Default);
				driver.Configure.Cell.Cpc.Udtx.Cycle.Ithreshold.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:DSG
				int value = driver.Configure.Cell.Cpc.Udtx.Cycle.Dsg.Get(CycleRepCap.Default);
				value = driver.Configure.Cell.Cpc.Udtx.Cycle.Dsg.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:UDTX:CYCLe<nr>:DSG
				driver.Configure.Cell.Cpc.Udtx.Cycle.Dsg.Set(1, CycleRepCap.Default);
				driver.Configure.Cell.Cpc.Udtx.Cycle.Dsg.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DDRX:ENABle
				bool value = driver.Configure.Cell.Cpc.Ddrx.Enable;
				driver.Configure.Cell.Cpc.Ddrx.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DDRX:CYCLe:APATtern
				int value = driver.Configure.Cell.Cpc.Ddrx.Cycle.Apattern;
				driver.Configure.Cell.Cpc.Ddrx.Cycle.Apattern = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DDRX:CYCLe:ITHReshold
				int value = driver.Configure.Cell.Cpc.Ddrx.Cycle.Ithreshold;
				driver.Configure.Cell.Cpc.Ddrx.Cycle.Ithreshold = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DDRX:GMONitoring:ENABle
				bool value = driver.Configure.Cell.Cpc.Ddrx.Gmonitoring.Enable;
				driver.Configure.Cell.Cpc.Ddrx.Gmonitoring.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:DDRX:GMONitoring:ITHReshold
				int value = driver.Configure.Cell.Cpc.Ddrx.Gmonitoring.Ithreshold;
				driver.Configure.Cell.Cpc.Ddrx.Gmonitoring.Ithreshold = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:MAC:CYCLe:ITHReshold
				int value = driver.Configure.Cell.Cpc.Mac.Cycle.Ithreshold;
				driver.Configure.Cell.Cpc.Mac.Cycle.Ithreshold = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:MAC:CYCLe:TTI<ms>
				int value = driver.Configure.Cell.Cpc.Mac.Cycle.Tti.Get(TransTimeIntervalRepCap.Default);
				value = driver.Configure.Cell.Cpc.Mac.Cycle.Tti.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:MAC:CYCLe:TTI<ms>
				driver.Configure.Cell.Cpc.Mac.Cycle.Tti.Set(1, TransTimeIntervalRepCap.Default);
				driver.Configure.Cell.Cpc.Mac.Cycle.Tti.Set(1);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:ENABle
				bool value = driver.Configure.Cell.Cpc.HlOperation.Enable;
				driver.Configure.Cell.Cpc.HlOperation.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:NTBLock
				int value = driver.Configure.Cell.Cpc.HlOperation.NtBlock;
				driver.Configure.Cell.Cpc.HlOperation.NtBlock = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:TBLock<index>
				List<int> value = driver.Configure.Cell.Cpc.HlOperation.TransportBlock.Get(TransportBlockRepCap.Default);
				value = driver.Configure.Cell.Cpc.HlOperation.TransportBlock.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:TBLock<index>
				driver.Configure.Cell.Cpc.HlOperation.TransportBlock.Set(new List<int> { 1, 2, 3 }, TransportBlockRepCap.Default);
				driver.Configure.Cell.Cpc.HlOperation.TransportBlock.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:SCSupport<index>
				List<bool> value = driver.Configure.Cell.Cpc.HlOperation.ScSupport.Get(SecondCodeRepCap.Default);
				value = driver.Configure.Cell.Cpc.HlOperation.ScSupport.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HLOPeration:SCSupport<index>
				driver.Configure.Cell.Cpc.HlOperation.ScSupport.Set(new List<bool> { true, false, true }, SecondCodeRepCap.Default);
				driver.Configure.Cell.Cpc.HlOperation.ScSupport.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HORDer:SEND
				RsCmwWcdmaSig_Configure_Cell_Cpc_Horder_Send.Get_Data value = driver.Configure.Cell.Cpc.Horder.Send.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CELL:CPC:HORDer:SEND
				driver.Configure.Cell.Cpc.Horder.Send.Set();
				driver.Configure.Cell.Cpc.Horder.Send.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Lte_Cell.Cell_Data value = driver.Configure.Ncell.Lte.Cell.Get(CellRepCap.Default);
				value = driver.Configure.Ncell.Lte.Cell.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Lte_Cell.Cell_Data value = new RsCmwWcdmaSig_Configure_Ncell_Lte_Cell.Cell_Data();
				driver.Configure.Ncell.Lte.Cell.Set(value, CellRepCap.Default);
				driver.Configure.Ncell.Lte.Cell.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:LTE:THResholds:HIGH
				int value = driver.Configure.Ncell.Lte.Thresholds.High;
				driver.Configure.Ncell.Lte.Thresholds.High = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:GSM:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Gsm_Cell.Cell_Data value = driver.Configure.Ncell.Gsm.Cell.Get(CellRepCap.Default);
				value = driver.Configure.Ncell.Gsm.Cell.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:GSM:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Gsm_Cell.Cell_Data value = new RsCmwWcdmaSig_Configure_Ncell_Gsm_Cell.Cell_Data();
				driver.Configure.Ncell.Gsm.Cell.Set(value, CellRepCap.Default);
				driver.Configure.Ncell.Gsm.Cell.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:WCDMa:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = driver.Configure.Ncell.Wcdma.Cell.Get(CellRepCap.Default);
				value = driver.Configure.Ncell.Wcdma.Cell.Get();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:NCELl:WCDMa:CELL<n>
				RsCmwWcdmaSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = new RsCmwWcdmaSig_Configure_Ncell_Wcdma_Cell.Cell_Data();
				driver.Configure.Ncell.Wcdma.Cell.Set(value, CellRepCap.Default);
				driver.Configure.Ncell.Wcdma.Cell.Set(value);
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:PNResync
				bool value = driver.Configure.Ber.PnResync;
				driver.Configure.Ber.PnResync = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:LIMit
				RsCmwWcdmaSig_Configure_Ber.Limit_Data value = driver.Configure.Ber.Limit;
				driver.Configure.Ber.Limit = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:TBLocks
				int value = driver.Configure.Ber.Tblocks;
				driver.Configure.Ber.Tblocks = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.Ber.Scondition = x;
					StopConditionEnum value = driver.Configure.Ber.Scondition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Ber.Repetition = x;
					RepeatEnum value = driver.Configure.Ber.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:BER:TOUT
				double value = driver.Configure.Ber.Timeout;
				driver.Configure.Ber.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:THRoughput:TOUT
				double value = driver.Configure.Throughput.Timeout;
				driver.Configure.Throughput.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:THRoughput:UPDate
				double value = driver.Configure.Throughput.Update;
				driver.Configure.Throughput.Update = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:THRoughput:WINDow
				double value = driver.Configure.Throughput.Window;
				driver.Configure.Throughput.Window = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:THRoughput:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Throughput.Repetition = x;
					RepeatEnum value = driver.Configure.Throughput.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HACK:TOUT
				double value = driver.Configure.Hack.Timeout;
				driver.Configure.Hack.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HACK:HARQ
				foreach (MonitoredHarqEnum x in new MonitoredHarqEnum[] { MonitoredHarqEnum.ALL, MonitoredHarqEnum.H0, MonitoredHarqEnum.H1, MonitoredHarqEnum.H2, MonitoredHarqEnum.H3, MonitoredHarqEnum.H4, MonitoredHarqEnum.H5, MonitoredHarqEnum.H6, MonitoredHarqEnum.H7 })
				{
					driver.Configure.Hack.Harq = x;
					MonitoredHarqEnum value = driver.Configure.Hack.Harq;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HACK:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Hack.Repetition = x;
					RepeatEnum value = driver.Configure.Hack.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HACK:MSFRames
				int value = driver.Configure.Hack.MsFrames;
				driver.Configure.Hack.MsFrames = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HACK:SMODe:AVERage
				foreach (AveragingModeEnum x in new AveragingModeEnum[] { AveragingModeEnum.CONTinuous, AveragingModeEnum.WINDow })
				{
					driver.Configure.Hack.Smode.Average = x;
					AveragingModeEnum value = driver.Configure.Hack.Smode.Average;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:TOUT
				double value = driver.Configure.Hcqi.Timeout;
				driver.Configure.Hcqi.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:TCASe
				foreach (TestCaseEnum x in new TestCaseEnum[] { TestCaseEnum.AWGN, TestCaseEnum.FADing })
				{
					driver.Configure.Hcqi.Tcase = x;
					TestCaseEnum value = driver.Configure.Hcqi.Tcase;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:CQI:MSFRames
				int value = driver.Configure.Hcqi.Cqi.MsFrames;
				driver.Configure.Hcqi.Cqi.MsFrames = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:BLER:MSFRames
				int value = driver.Configure.Hcqi.Bler.MsFrames;
				driver.Configure.Hcqi.Bler.MsFrames = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:LIMit:AWGN:BLER
				RsCmwWcdmaSig_Configure_Hcqi_Limit_Awgn.Bler_Data value = driver.Configure.Hcqi.Limit.Awgn.Bler;
				driver.Configure.Hcqi.Limit.Awgn.Bler = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:LIMit:AWGN:DTX
				RsCmwWcdmaSig_Configure_Hcqi_Limit_Awgn.Dtx_Data value = driver.Configure.Hcqi.Limit.Awgn.Dtx;
				driver.Configure.Hcqi.Limit.Awgn.Dtx = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:LIMit:AWGN
				double value = driver.Configure.Hcqi.Limit.Awgn.Value;
				driver.Configure.Hcqi.Limit.Awgn.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:LIMit:FADing:BLER
				RsCmwWcdmaSig_Configure_Hcqi_Limit_Fading.Bler_Data value = driver.Configure.Hcqi.Limit.Fading.Bler;
				driver.Configure.Hcqi.Limit.Fading.Bler = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:HCQI:LIMit:FADing:DTX
				RsCmwWcdmaSig_Configure_Hcqi_Limit_Fading.Dtx_Data value = driver.Configure.Hcqi.Limit.Fading.Dtx;
				driver.Configure.Hcqi.Limit.Fading.Dtx = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ULLogging:TOUT
				double value = driver.Configure.UplinkLogging.Timeout;
				driver.Configure.UplinkLogging.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ULLogging:SCCYcle
				bool value = driver.Configure.UplinkLogging.Sccycle;
				driver.Configure.UplinkLogging.Sccycle = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ULLogging:SSFN
				int value = driver.Configure.UplinkLogging.Ssfn;
				driver.Configure.UplinkLogging.Ssfn = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ULLogging:MSFRames
				int value = driver.Configure.UplinkLogging.MsFrames;
				driver.Configure.UplinkLogging.MsFrames = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ULLogging:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.UplinkLogging.Repetition = x;
					RepeatEnum value = driver.Configure.UplinkLogging.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:TOUT
				double value = driver.Configure.Eagch.Timeout;
				driver.Configure.Eagch.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Eagch.Repetition = x;
					RepeatEnum value = driver.Configure.Eagch.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:MFRames
				int value = driver.Configure.Eagch.Mframes;
				driver.Configure.Eagch.Mframes = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:MTYPe
				foreach (MeasTypeEnum x in new MeasTypeEnum[] { MeasTypeEnum.GENeral, MeasTypeEnum.MISSed })
				{
					driver.Configure.Eagch.Mtype = x;
					MeasTypeEnum value = driver.Configure.Eagch.Mtype;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:LIMit
				double value = driver.Configure.Eagch.Limit;
				driver.Configure.Eagch.Limit = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:ETFCi:MODE
				foreach (AutoManualModeEnum x in new AutoManualModeEnum[] { AutoManualModeEnum.AUTO, AutoManualModeEnum.MANual })
				{
					driver.Configure.Eagch.Etfci.Mode = x;
					AutoManualModeEnum value = driver.Configure.Eagch.Etfci.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:ETFCi:MANual
				List<int> value = driver.Configure.Eagch.Etfci.Manual;
				driver.Configure.Eagch.Etfci.Manual = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EAGCh:ETFCi:AUTO
				List<int> value = driver.Configure.Eagch.Etfci.Auto;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EHICh:TOUT
				double value = driver.Configure.Ehich.Timeout;
				driver.Configure.Ehich.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EHICh:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Ehich.Repetition = x;
					RepeatEnum value = driver.Configure.Ehich.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EHICh:MFRames
				int value = driver.Configure.Ehich.Mframes;
				driver.Configure.Ehich.Mframes = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EHICh:LIMit
				double value = driver.Configure.Ehich.Limit;
				driver.Configure.Ehich.Limit = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:EHICh:SMODe:AVERage
				foreach (AveragingModeEnum x in new AveragingModeEnum[] { AveragingModeEnum.CONTinuous, AveragingModeEnum.WINDow })
				{
					driver.Configure.Ehich.Smode.Average = x;
					AveragingModeEnum value = driver.Configure.Ehich.Smode.Average;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:TOUT
				double value = driver.Configure.Ergch.Timeout;
				driver.Configure.Ergch.Timeout = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Ergch.Repetition = x;
					RepeatEnum value = driver.Configure.Ergch.Repetition;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:MFRames
				int value = driver.Configure.Ergch.Mframes;
				driver.Configure.Ergch.Mframes = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:LIMit
				RsCmwWcdmaSig_Configure_Ergch.Limit_Data value = driver.Configure.Ergch.Limit;
				driver.Configure.Ergch.Limit = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:ETFCi:MODE
				foreach (AutoManualModeEnum x in new AutoManualModeEnum[] { AutoManualModeEnum.AUTO, AutoManualModeEnum.MANual })
				{
					driver.Configure.Ergch.Etfci.Mode = x;
					AutoManualModeEnum value = driver.Configure.Ergch.Etfci.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:ETFCi:EXPected
				int value = driver.Configure.Ergch.Etfci.Expected;
				driver.Configure.Ergch.Etfci.Expected = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:ETFCi:INITial
				int value = driver.Configure.Ergch.Etfci.Initial;
				driver.Configure.Ergch.Etfci.Initial = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:ETFCi:MANual
				List<int> value = driver.Configure.Ergch.Etfci.Manual;
				driver.Configure.Ergch.Etfci.Manual = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:ERGCh:ETFCi:AUTO
				List<int> value = driver.Configure.Ergch.Etfci.Auto;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:KTLoop
				bool value = driver.Configure.Sms.KtLoop;
				driver.Configure.Sms.KtLoop = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:BINary
				double value = driver.Configure.Sms.Outgoing.Binary;
				driver.Configure.Sms.Outgoing.Binary = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:PIDentifier
				double value = driver.Configure.Sms.Outgoing.Pidentifier;
				driver.Configure.Sms.Outgoing.Pidentifier = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<Instance>:SMS:OUTGoing:UDHeader
				double value = driver.Configure.Sms.Outgoing.Udheader;
				driver.Configure.Sms.Outgoing.Udheader = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:DCODing
				foreach (SmsDataCodingEnum x in new SmsDataCodingEnum[] { SmsDataCodingEnum.BIT7, SmsDataCodingEnum.BIT8, SmsDataCodingEnum.REServed })
				{
					driver.Configure.Sms.Outgoing.Dcoding = x;
					SmsDataCodingEnum value = driver.Configure.Sms.Outgoing.Dcoding;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:CGRoup
				foreach (CodingGroupEnum x in new CodingGroupEnum[] { CodingGroupEnum.DCMClass, CodingGroupEnum.GDCoding, CodingGroupEnum.REServed })
				{
					driver.Configure.Sms.Outgoing.Cgroup = x;
					CodingGroupEnum value = driver.Configure.Sms.Outgoing.Cgroup;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:MCLass
				foreach (MessageClassEnum x in new MessageClassEnum[] { MessageClassEnum.CL0, MessageClassEnum.CL1, MessageClassEnum.CL2, MessageClassEnum.CL3, MessageClassEnum.NONE })
				{
					driver.Configure.Sms.Outgoing.Mclass = x;
					MessageClassEnum value = driver.Configure.Sms.Outgoing.Mclass;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:OSADdress
				string value = driver.Configure.Sms.Outgoing.OsAddress;
				driver.Configure.Sms.Outgoing.OsAddress = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:OADDress
				string value = driver.Configure.Sms.Outgoing.Oaddress;
				driver.Configure.Sms.Outgoing.Oaddress = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:RMCDelay
				double value = driver.Configure.Sms.Outgoing.RmcDelay;
				driver.Configure.Sms.Outgoing.RmcDelay = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:LHANdling
				foreach (LongSmsHandlingEnum x in new LongSmsHandlingEnum[] { LongSmsHandlingEnum.MSMS, LongSmsHandlingEnum.TRUNcate })
				{
					driver.Configure.Sms.Outgoing.Lhandling = x;
					LongSmsHandlingEnum value = driver.Configure.Sms.Outgoing.Lhandling;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:MESHandling
				foreach (MessageHandlingEnum x in new MessageHandlingEnum[] { MessageHandlingEnum.FILE, MessageHandlingEnum.INTernal })
				{
					driver.Configure.Sms.Outgoing.MesHandling = x;
					MessageHandlingEnum value = driver.Configure.Sms.Outgoing.MesHandling;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:INTernal
				string value = driver.Configure.Sms.Outgoing.Internal;
				driver.Configure.Sms.Outgoing.Internal = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:SCTStamp:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Sms.Outgoing.SctStamp.Tsource = x;
					SourceTimeEnum value = driver.Configure.Sms.Outgoing.SctStamp.Tsource;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:SCTStamp:DATE
				RsCmwWcdmaSig_Configure_Sms_Outgoing_SctStamp.Date_Data value = driver.Configure.Sms.Outgoing.SctStamp.Date;
				driver.Configure.Sms.Outgoing.SctStamp.Date = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:SCTStamp:TIME
				RsCmwWcdmaSig_Configure_Sms_Outgoing_SctStamp.Time_Data value = driver.Configure.Sms.Outgoing.SctStamp.Time;
				driver.Configure.Sms.Outgoing.SctStamp.Time = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:FILE:INFO
				RsCmwWcdmaSig_Configure_Sms_Outgoing_File.Info_Data value = driver.Configure.Sms.Outgoing.File.Info;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:OUTGoing:FILE
				string value = driver.Configure.Sms.Outgoing.File.Value;
				driver.Configure.Sms.Outgoing.File.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:INComing:FILE:INFO
				RsCmwWcdmaSig_Configure_Sms_Incoming_File.Info_Data value = driver.Configure.Sms.Incoming.File.Info;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:SMS:INComing:FILE
				string value = driver.Configure.Sms.Incoming.File.Value;
				driver.Configure.Sms.Incoming.File.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:CTCH:ENABle
				bool value = driver.Configure.Cbs.Ctch.Enable;
				driver.Configure.Cbs.Ctch.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:CTCH:PERiod
				int value = driver.Configure.Cbs.Ctch.Period;
				driver.Configure.Cbs.Ctch.Period = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:CTCH:FOFFset
				int value = driver.Configure.Cbs.Ctch.FreqOffset;
				driver.Configure.Cbs.Ctch.FreqOffset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:CTCH:FMPLength
				foreach (TtiExtendedEnum x in new TtiExtendedEnum[] { TtiExtendedEnum.M10, TtiExtendedEnum.M20, TtiExtendedEnum.M40, TtiExtendedEnum.M80 })
				{
					driver.Configure.Cbs.Ctch.FmpLength = x;
					TtiExtendedEnum value = driver.Configure.Cbs.Ctch.FmpLength;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:DRX:ENABle
				bool value = driver.Configure.Cbs.Drx.Enable;
				driver.Configure.Cbs.Drx.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:DRX:PERiod
				int value = driver.Configure.Cbs.Drx.Period;
				driver.Configure.Cbs.Drx.Period = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:DRX:LENGth
				int value = driver.Configure.Cbs.Drx.Length;
				driver.Configure.Cbs.Drx.Length = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:DRX:OFFSet
				int value = driver.Configure.Cbs.Drx.Offset;
				driver.Configure.Cbs.Drx.Offset = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:DRX:FEMPty
				bool value = driver.Configure.Cbs.Drx.Fempty;
				driver.Configure.Cbs.Drx.Fempty = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:ENABle
				bool value = driver.Configure.Cbs.Message.Enable;
				driver.Configure.Cbs.Message.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:ID
				int value = driver.Configure.Cbs.Message.Id;
				driver.Configure.Cbs.Message.Id = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:IDTYpe
				foreach (CbsMessageSeverityEnum x in new CbsMessageSeverityEnum[] { CbsMessageSeverityEnum.AAMBer, CbsMessageSeverityEnum.AEXTreme, CbsMessageSeverityEnum.APResidentia, CbsMessageSeverityEnum.ASEVere, CbsMessageSeverityEnum.EARThquake, CbsMessageSeverityEnum.ETWarning, CbsMessageSeverityEnum.ETWTest, CbsMessageSeverityEnum.TSUNami, CbsMessageSeverityEnum.UDEFined })
				{
					driver.Configure.Cbs.Message.Idtype = x;
					CbsMessageSeverityEnum value = driver.Configure.Cbs.Message.Idtype;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:SERial
				RsCmwWcdmaSig_Configure_Cbs_Message.Serial_Data value = driver.Configure.Cbs.Message.Serial;
				driver.Configure.Cbs.Message.Serial = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:CGRoup
				int value = driver.Configure.Cbs.Message.Cgroup;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:CATegory
				foreach (PriorityEnum x in new PriorityEnum[] { PriorityEnum.BACKground, PriorityEnum.HIGH, PriorityEnum.NORMal })
				{
					driver.Configure.Cbs.Message.Category = x;
					PriorityEnum value = driver.Configure.Cbs.Message.Category;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:SOURce
				foreach (MessageHandlingEnum x in new MessageHandlingEnum[] { MessageHandlingEnum.FILE, MessageHandlingEnum.INTernal })
				{
					driver.Configure.Cbs.Message.Source = x;
					MessageHandlingEnum value = driver.Configure.Cbs.Message.Source;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:DATA
				string value = driver.Configure.Cbs.Message.Data;
				driver.Configure.Cbs.Message.Data = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:PERiod
				double value = driver.Configure.Cbs.Message.Period;
				driver.Configure.Cbs.Message.Period = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:LANGuage
				RsCmwWcdmaSig_Configure_Cbs_Message_Language.Get_Data value = driver.Configure.Cbs.Message.Language.Get();				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:LANGuage
				driver.Configure.Cbs.Message.Language.Set(1);				
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:FILE:INFO
				RsCmwWcdmaSig_Configure_Cbs_Message_File.Info_Data value = driver.Configure.Cbs.Message.File.Info;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:FILE
				string value = driver.Configure.Cbs.Message.File.Value;
				driver.Configure.Cbs.Message.File.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:ETWS:ALERt
				bool value = driver.Configure.Cbs.Message.Etws.Alert;
				driver.Configure.Cbs.Message.Etws.Alert = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:CBS:MESSage:ETWS:POPup
				bool value = driver.Configure.Cbs.Message.Etws.Popup;
				driver.Configure.Cbs.Message.Etws.Popup = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Carrier.Fsimulator.Enable;
				driver.Configure.Fading.Carrier.Fsimulator.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:STANdard
				foreach (FadingStandardEnum x in new FadingStandardEnum[] { FadingStandardEnum.B261, FadingStandardEnum.B262, FadingStandardEnum.B263, FadingStandardEnum.BDEath, FadingStandardEnum.C1, FadingStandardEnum.C2, FadingStandardEnum.C3, FadingStandardEnum.C4, FadingStandardEnum.C5, FadingStandardEnum.C6, FadingStandardEnum.C8, FadingStandardEnum.HST, FadingStandardEnum.MPRopagation, FadingStandardEnum.PA3, FadingStandardEnum.PB3, FadingStandardEnum.USER, FadingStandardEnum.VA12, FadingStandardEnum.VA3, FadingStandardEnum.VA30 })
				{
					driver.Configure.Fading.Carrier.Fsimulator.Standard = x;
					FadingStandardEnum value = driver.Configure.Fading.Carrier.Fsimulator.Standard;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Carrier.Fsimulator.Globale.Seed;
				driver.Configure.Fading.Carrier.Fsimulator.Globale.Seed = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:RESTart:MODE
				foreach (AutoManualModeEnum x in new AutoManualModeEnum[] { AutoManualModeEnum.AUTO, AutoManualModeEnum.MANual })
				{
					driver.Configure.Fading.Carrier.Fsimulator.Restart.Mode = x;
					AutoManualModeEnum value = driver.Configure.Fading.Carrier.Fsimulator.Restart.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:RESTart
				driver.Configure.Fading.Carrier.Fsimulator.Restart.Set();
				driver.Configure.Fading.Carrier.Fsimulator.Restart.SetAndWait();
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Carrier.Fsimulator.InsertionLoss.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Carrier.Fsimulator.InsertionLoss.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Carrier.Fsimulator.InsertionLoss.Loss;
				driver.Configure.Fading.Carrier.Fsimulator.InsertionLoss.Loss = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:DSHift:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Carrier.Fsimulator.Dshift.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Carrier.Fsimulator.Dshift.Mode;
				}
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:DSHift
				double value = driver.Configure.Fading.Carrier.Fsimulator.Dshift.Value;
				driver.Configure.Fading.Carrier.Fsimulator.Dshift.Value = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:AWGN:NOISe
				double value = driver.Configure.Fading.Carrier.Awgn.Noise;
				driver.Configure.Fading.Carrier.Awgn.Noise = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:AWGN:ENABle
				bool value = driver.Configure.Fading.Carrier.Awgn.Enable;
				driver.Configure.Fading.Carrier.Awgn.Enable = value;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:AWGN:SNRatio
				double value = driver.Configure.Fading.Carrier.Awgn.SnRatio;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:POWer:SUM
				double value = driver.Configure.Fading.Carrier.Power.Sum;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Carrier.Power.Noise.Total;
			}
			{	// CONFigure:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:POWer:NOISe
				double value = driver.Configure.Fading.Carrier.Power.Noise.Value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:DESTination
				string value = driver.Prepare.Handover.Destination;
				driver.Prepare.Handover.Destination = value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:MMODe
				foreach (MobilityModeEnum x in new MobilityModeEnum[] { MobilityModeEnum.CCORder, MobilityModeEnum.HANDover, MobilityModeEnum.NAV, MobilityModeEnum.REDirection })
				{
					driver.Prepare.Handover.Mmode = x;
					MobilityModeEnum value = driver.Prepare.Handover.Mmode;
				}
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:CATalog:DESTination
				List<string> value = driver.Prepare.Handover.Catalog.Destination;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:DESTination
				foreach (HoverExtDestinationEnum x in new HoverExtDestinationEnum[] { HoverExtDestinationEnum.CDMA, HoverExtDestinationEnum.EVDO, HoverExtDestinationEnum.GSM, HoverExtDestinationEnum.LTE, HoverExtDestinationEnum.WCDMa })
				{
					driver.Prepare.Handover.External.Destination = x;
					HoverExtDestinationEnum value = driver.Prepare.Handover.External.Destination;
				}
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:LTE
				RsCmwWcdmaSig_Prepare_Handover_External.Lte_Data value = driver.Prepare.Handover.External.Lte;
				driver.Prepare.Handover.External.Lte = value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:GSM
				RsCmwWcdmaSig_Prepare_Handover_External.Gsm_Data value = driver.Prepare.Handover.External.Gsm;
				driver.Prepare.Handover.External.Gsm = value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:CDMA
				RsCmwWcdmaSig_Prepare_Handover_External.Cdma_Data value = driver.Prepare.Handover.External.Cdma;
				driver.Prepare.Handover.External.Cdma = value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:EVDO
				RsCmwWcdmaSig_Prepare_Handover_External.Evdo_Data value = driver.Prepare.Handover.External.Evdo;
				driver.Prepare.Handover.External.Evdo = value;
			}
			{	// PREPare:WCDMa:SIGNaling<instance>:HANDover:EXTernal:WCDMa
				RsCmwWcdmaSig_Prepare_Handover_External.Wcdma_Data value = driver.Prepare.Handover.External.Wcdma;
				driver.Prepare.Handover.External.Wcdma = value;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:ELOGging:LAST
				RsCmwWcdmaSig_Sense_EventLogging.Last_Data value = driver.Sense.EventLogging.Last;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:ELOGging:ALL
				RsCmwWcdmaSig_Sense_EventLogging.All_Data value = driver.Sense.EventLogging.All;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UEReport:CCELl
				RsCmwWcdmaSig_Sense_UeReport.Ccell_Data value = driver.Sense.UeReport.Ccell;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UEReport:NCELl<nr>
				RsCmwWcdmaSig_Sense_UeReport.GetNcell_Data value = driver.Sense.UeReport.GetNcell(DownCarrierRepCap.Dc1);
				value = driver.Sense.UeReport.GetNcell();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UEReport:NCELl:GSM:CELL<nr>
				RsCmwWcdmaSig_Sense_UeReport_Ncell_Gsm.GetCell_Data value = driver.Sense.UeReport.Ncell.Gsm.GetCell(CellRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Gsm.GetCell();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UEReport:NCELl:WCDMa:CELL<nr>
				RsCmwWcdmaSig_Sense_UeReport_Ncell_Wcdma.GetCell_Data value = driver.Sense.UeReport.Ncell.Wcdma.GetCell(CellRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Wcdma.GetCell();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UEReport:NCELl:LTE:CELL<nr>
				RsCmwWcdmaSig_Sense_UeReport_Ncell_Lte.GetCell_Data value = driver.Sense.UeReport.Ncell.Lte.GetCell(CellRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Lte.GetCell();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:HSUPa
				RsCmwWcdmaSig_Sense_UeCapability.Hsupa_Data value = driver.Sense.UeCapability.Hsupa;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:HSDPa
				RsCmwWcdmaSig_Sense_UeCapability.Hsdpa_Data value = driver.Sense.UeCapability.Hsdpa;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:GENeral
				RsCmwWcdmaSig_Sense_UeCapability.General_Data value = driver.Sense.UeCapability.General;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MRAT
				RsCmwWcdmaSig_Sense_UeCapability.Mrat_Data value = driver.Sense.UeCapability.Mrat;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MMODe
				foreach (UtraModeEnum x in new UtraModeEnum[] { UtraModeEnum.BOTH, UtraModeEnum.FDD, UtraModeEnum.TDD })
				{
					UtraModeEnum value = driver.Sense.UeCapability.Mmode;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:PUPLink
				RsCmwWcdmaSig_Sense_UeCapability.PupLink_Data value = driver.Sense.UeCapability.PupLink;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:PDOWnlink
				RsCmwWcdmaSig_Sense_UeCapability.Pdownlink_Data value = driver.Sense.UeCapability.Pdownlink;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RLC
				RsCmwWcdmaSig_Sense_UeCapability.Rlc_Data value = driver.Sense.UeCapability.Rlc;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:PDCP
				RsCmwWcdmaSig_Sense_UeCapability.Pdcp_Data value = driver.Sense.UeCapability.Pdcp;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:IMSVoice
				RsCmwWcdmaSig_Sense_UeCapability.ImsVoice_Data value = driver.Sense.UeCapability.ImsVoice;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:CODec:GSM
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					List<YesNoStatusEnum> value = driver.Sense.UeCapability.Codec.Gsm;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:CODec:UMTS
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					List<YesNoStatusEnum> value = driver.Sense.UeCapability.Codec.Umts;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MEASurement
				RsCmwWcdmaSig_Sense_UeCapability_Measurement.Value_Data value = driver.Sense.UeCapability.Measurement.Value;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MEASurement:CMODe:WCDMa
				List<CompressedModeEnum> value = driver.Sense.UeCapability.Measurement.Cmode.GetWcdma(CompressedModeBandEnum.OB1);				
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MEASurement:CMODe:GSM
				List<CompressedModeEnum> value = driver.Sense.UeCapability.Measurement.Cmode.GetGsm(CompressedModeBandEnum.OB1);				
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MEASurement:CMODe:LTE
				List<CompressedModeEnum> value = driver.Sense.UeCapability.Measurement.Cmode.GetLte(CompressedModeBandEnum.OB1);				
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:MEASurement:CMODe:WCDMa:MCARrier
				CompressedModeEnum value = driver.Sense.UeCapability.Measurement.Cmode.Wcdma.GetMcarrier(CompressedModeBandEnum.OB1);				
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition
				RsCmwWcdmaSig_Sense_UeCapability_UePosition.Value_Data value = driver.Sense.UeCapability.UePosition.Value;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs:GALileo
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Galileo_Data value = driver.Sense.UeCapability.UePosition.Ganss.Galileo;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs:SBAS
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Sbas_Data value = driver.Sense.UeCapability.UePosition.Ganss.Sbas;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs:MGPS
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Mgps_Data value = driver.Sense.UeCapability.UePosition.Ganss.Mgps;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs:QZSS
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Qzss_Data value = driver.Sense.UeCapability.UePosition.Ganss.Qzss;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs:GLONass
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Glonass_Data value = driver.Sense.UeCapability.UePosition.Ganss.Glonass;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:UEPosition:GANSs
				RsCmwWcdmaSig_Sense_UeCapability_UePosition_Ganss.Value_Data value = driver.Sense.UeCapability.UePosition.Ganss.Value;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RFParameter:BAND<band>
				RsCmwWcdmaSig_Sense_UeCapability_RfParameter.GetBand_Data value = driver.Sense.UeCapability.RfParameter.GetBand(BandRepCap.B1);
				value = driver.Sense.UeCapability.RfParameter.GetBand();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RFParameter:BCList
				RsCmwWcdmaSig_Sense_UeCapability_RfParameter.BcList_Data value = driver.Sense.UeCapability.RfParameter.BcList;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RFParameter:BC<nr>
				RsCmwWcdmaSig_Sense_UeCapability_RfParameter.GetBc_Data value = driver.Sense.UeCapability.RfParameter.GetBc(BandCombinationRepCap.Nr1);
				value = driver.Sense.UeCapability.RfParameter.GetBc();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RFParameter
				RsCmwWcdmaSig_Sense_UeCapability_RfParameter.Value_Data value = driver.Sense.UeCapability.RfParameter.Value;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UECapability:RFParameter:BAND<band>:NC<cell>
				RsCmwWcdmaSig_Sense_UeCapability_RfParameter_Band.GetNc_Data value = driver.Sense.UeCapability.RfParameter.Band.GetNc(BandRepCap.Default, NonContigCellRepCap.Nc2);
				value = driver.Sense.UeCapability.RfParameter.Band.GetNc();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:APN
				List<string> value = driver.Sense.UesInfo.Apn;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:DULalignment
				RsCmwWcdmaSig_Sense_UesInfo.DulAlignment_Data value = driver.Sense.UesInfo.DulAlignment;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:DINFo
				RsCmwWcdmaSig_Sense_UesInfo.Dinfo_Data value = driver.Sense.UesInfo.Dinfo;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:IMEI
				string value = driver.Sense.UesInfo.Imei;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:RIDentity
				string value = driver.Sense.UesInfo.Ridentity;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:RITYpe
				string value = driver.Sense.UesInfo.RiType;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:TTY
				string value = driver.Sense.UesInfo.Tty;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:CNUMber
				string value = driver.Sense.UesInfo.Cnumber;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:DNUMber
				string value = driver.Sense.UesInfo.Dnumber;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:EMERgency
				bool value = driver.Sense.UesInfo.Emergency;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:ESCategory
				RsCmwWcdmaSig_Sense_UesInfo.EsCategory_Data value = driver.Sense.UesInfo.EsCategory;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:RRC
				foreach (RrcStateEnum x in new RrcStateEnum[] { RrcStateEnum.CPCH, RrcStateEnum.DCH, RrcStateEnum.FACH, RrcStateEnum.IDLE, RrcStateEnum.UPCH })
				{
					RrcStateEnum value = driver.Sense.UesInfo.Rrc;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:UEADdress:IPV<n>
				List<string> value = driver.Sense.UesInfo.UeAddress.GetIpv(IPversionRepCap.IPv4);
				value = driver.Sense.UesInfo.UeAddress.GetIpv();
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:CONNection:PACKet
				string value = driver.Sense.UesInfo.Connection.Packet;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UESinfo:CONNection:CIRCuit
				string value = driver.Sense.UesInfo.Connection.Circuit;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:CELL:CONFig
				foreach (CellConfigEnum x in new CellConfigEnum[] { CellConfigEnum._3CHS, CellConfigEnum._3DUPlus, CellConfigEnum._3HDU, CellConfigEnum._4CHS, CellConfigEnum._4DUPlus, CellConfigEnum._4HDU, CellConfigEnum.DCHS, CellConfigEnum.DDUPlus, CellConfigEnum.DHDU, CellConfigEnum.HDUPlus, CellConfigEnum.HSDPa, CellConfigEnum.HSPA, CellConfigEnum.HSPLus, CellConfigEnum.HSUPa, CellConfigEnum.QPSK, CellConfigEnum.WCDMa })
				{
					CellConfigEnum value = driver.Sense.Cell.Config;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:IQOut:CARRier<carrier>
				RsCmwWcdmaSig_Sense_IqOut.Carrier_Data value = driver.Sense.IqOut.Carrier;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:DL:CARRier<carrier>:ENHanced:DPCH:REPorted
				double value = driver.Sense.Downlink.Carrier.Enhanced.Dpch.Reported;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UL:EIPower
				double value = driver.Sense.Uplink.EiPower;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:UL:OLPControl:EIPPower
				double value = driver.Sense.Uplink.OlpControl.EipPower;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:CONNection:CURRent
				foreach (CurrentConnectionTypeEnum x in new CurrentConnectionTypeEnum[] { CurrentConnectionTypeEnum.NONE, CurrentConnectionTypeEnum.PACKet, CurrentConnectionTypeEnum.SRB, CurrentConnectionTypeEnum.TEST, CurrentConnectionTypeEnum.VIDeo, CurrentConnectionTypeEnum.VIPacket, CurrentConnectionTypeEnum.VOICe, CurrentConnectionTypeEnum.VOPacket })
				{
					CurrentConnectionTypeEnum value = driver.Sense.Connection.Current;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:CONNection:CSWitched:ATTempt
				int value = driver.Sense.Connection.Cswitched.Attempt;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:CONNection:CSWitched:REJect
				int value = driver.Sense.Connection.Cswitched.Reject;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:SMS:OUTGoing:INFO:LMSent
				foreach (SucessStateEnum x in new SucessStateEnum[] { SucessStateEnum.FAILed, SucessStateEnum.SUCCessful })
				{
					SucessStateEnum value = driver.Sense.Sms.Outgoing.Info.Lmsent;
				}
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:SMS:INComing:INFO:MTEXt
				string value = driver.Sense.Sms.Incoming.Info.Mtext;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:SMS:INComing:INFO:MLENgth
				int value = driver.Sense.Sms.Incoming.Info.Mlength;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:SMS:INFO:LRMessage:RFLag
				bool value = driver.Sense.Sms.Info.LrMessage.Rflag;
			}
			{	// SENSe:WCDMa:SIGNaling<instance>:FADing:CARRier<carrier>:FSIMulator:ILOSs:CSAMples
				double value = driver.Sense.Fading.Carrier.Fsimulator.InsertionLoss.Csamples;
			}
			{	// CLEan:WCDMa:SIGNaling<instance>:ELOGging
				driver.Clean.EventLogging.Set();
				driver.Clean.EventLogging.SetAndWait();
			}
			{	// CLEan:WCDMa:SIGNaling<instance>:CONNection:CSWitched:ATTempt
				driver.Clean.Connection.Cswitched.Attempt.Set();
				driver.Clean.Connection.Cswitched.Attempt.SetAndWait();
			}
			{	// CLEan:WCDMa:SIGNaling<instance>:CONNection:CSWitched:REJect
				driver.Clean.Connection.Cswitched.Reject.Set();
				driver.Clean.Connection.Cswitched.Reject.SetAndWait();
			}
			{	// CLEan:WCDMa:SIGNaling<instance>:SMS:INComing:INFO:MTEXt
				driver.Clean.Sms.Incoming.Info.Mtext.Set();
				driver.Clean.Sms.Incoming.Info.Mtext.SetAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:UEReport:STATe
				ResourceStateEnum value = driver.UeReport.State.Fetch();				
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>
				RsCmwWcdmaSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario
				RsCmwWcdmaSig_Route_Scenario.Value_Data value = driver.Route.Scenario.Value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCELl:FLEXible
				RsCmwWcdmaSig_Route_Scenario_Scell.Flexible_Data value = driver.Route.Scenario.Scell.Flexible;
				driver.Route.Scenario.Scell.Flexible = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCELl
				RsCmwWcdmaSig_Route_Scenario_Scell.Value_Data value = driver.Route.Scenario.Scell.Value;
				driver.Route.Scenario.Scell.Value = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCARrier:FLEXible
				RsCmwWcdmaSig_Route_Scenario_Dcarrier.Flexible_Data value = driver.Route.Scenario.Dcarrier.Flexible;
				driver.Route.Scenario.Dcarrier.Flexible = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCARrier
				RsCmwWcdmaSig_Route_Scenario_Dcarrier.Value_Data value = driver.Route.Scenario.Dcarrier.Value;
				driver.Route.Scenario.Dcarrier.Value = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFading[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_ScFading.External_Data value = driver.Route.Scenario.ScFading.External;
				driver.Route.Scenario.ScFading.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFading:INTernal
				RsCmwWcdmaSig_Route_Scenario_ScFading.Internal_Data value = driver.Route.Scenario.ScFading.Internal;
				driver.Route.Scenario.ScFading.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFading:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_ScFading_Flexible.External_Data value = driver.Route.Scenario.ScFading.Flexible.External;
				driver.Route.Scenario.ScFading.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFading:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_ScFading_Flexible.Internal_Data value = driver.Route.Scenario.ScFading.Flexible.Internal;
				driver.Route.Scenario.ScFading.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFDiversity[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_ScfDiversity.External_Data value = driver.Route.Scenario.ScfDiversity.External;
				driver.Route.Scenario.ScfDiversity.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFDiversity:INTernal
				RsCmwWcdmaSig_Route_Scenario_ScfDiversity.Internal_Data value = driver.Route.Scenario.ScfDiversity.Internal;
				driver.Route.Scenario.ScfDiversity.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFDiversity:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_ScfDiversity_Flexible.External_Data value = driver.Route.Scenario.ScfDiversity.Flexible.External;
				driver.Route.Scenario.ScfDiversity.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:SCFDiversity:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_ScfDiversity_Flexible.Internal_Data value = driver.Route.Scenario.ScfDiversity.Flexible.Internal;
				driver.Route.Scenario.ScfDiversity.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFading[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DcFading.External_Data value = driver.Route.Scenario.DcFading.External;
				driver.Route.Scenario.DcFading.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFading:INTernal
				RsCmwWcdmaSig_Route_Scenario_DcFading.Internal_Data value = driver.Route.Scenario.DcFading.Internal;
				driver.Route.Scenario.DcFading.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFading:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DcFading_Flexible.External_Data value = driver.Route.Scenario.DcFading.Flexible.External;
				driver.Route.Scenario.DcFading.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFading:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_DcFading_Flexible.Internal_Data value = driver.Route.Scenario.DcFading.Flexible.Internal;
				driver.Route.Scenario.DcFading.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFDiversity[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DcfDiversity.External_Data value = driver.Route.Scenario.DcfDiversity.External;
				driver.Route.Scenario.DcfDiversity.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFDiversity:INTernal
				RsCmwWcdmaSig_Route_Scenario_DcfDiversity.Internal_Data value = driver.Route.Scenario.DcfDiversity.Internal;
				driver.Route.Scenario.DcfDiversity.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFDiversity:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DcfDiversity_Flexible.External_Data value = driver.Route.Scenario.DcfDiversity.Flexible.External;
				driver.Route.Scenario.DcfDiversity.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCFDiversity:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_DcfDiversity_Flexible.Internal_Data value = driver.Route.Scenario.DcfDiversity.Flexible.Internal;
				driver.Route.Scenario.DcfDiversity.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFading[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DbFading.External_Data value = driver.Route.Scenario.DbFading.External;
				driver.Route.Scenario.DbFading.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFading:INTernal
				RsCmwWcdmaSig_Route_Scenario_DbFading.Internal_Data value = driver.Route.Scenario.DbFading.Internal;
				driver.Route.Scenario.DbFading.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFading:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DbFading_Flexible.External_Data value = driver.Route.Scenario.DbFading.Flexible.External;
				driver.Route.Scenario.DbFading.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFading:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_DbFading_Flexible.Internal_Data value = driver.Route.Scenario.DbFading.Flexible.Internal;
				driver.Route.Scenario.DbFading.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFDiversity[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DbfDiversity.External_Data value = driver.Route.Scenario.DbfDiversity.External;
				driver.Route.Scenario.DbfDiversity.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFDiversity:INTernal
				RsCmwWcdmaSig_Route_Scenario_DbfDiversity.Internal_Data value = driver.Route.Scenario.DbfDiversity.Internal;
				driver.Route.Scenario.DbfDiversity.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFDiversity:FLEXible[:EXTernal]
				RsCmwWcdmaSig_Route_Scenario_DbfDiversity_Flexible.External_Data value = driver.Route.Scenario.DbfDiversity.Flexible.External;
				driver.Route.Scenario.DbfDiversity.Flexible.External = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DBFDiversity:FLEXible:INTernal
				RsCmwWcdmaSig_Route_Scenario_DbfDiversity_Flexible.Internal_Data value = driver.Route.Scenario.DbfDiversity.Flexible.Internal;
				driver.Route.Scenario.DbfDiversity.Flexible.Internal = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCHSpa:FLEXible
				RsCmwWcdmaSig_Route_Scenario_Dchspa.Flexible_Data value = driver.Route.Scenario.Dchspa.Flexible;
				driver.Route.Scenario.Dchspa.Flexible = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:DCHSpa
				RsCmwWcdmaSig_Route_Scenario_Dchspa.Value_Data value = driver.Route.Scenario.Dchspa.Value;
				driver.Route.Scenario.Dchspa.Value = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:TCHSpa:FLEXible
				RsCmwWcdmaSig_Route_Scenario_Tchspa.Flexible_Data value = driver.Route.Scenario.Tchspa.Flexible;
				driver.Route.Scenario.Tchspa.Flexible = value;
			}
			{	// ROUTe:WCDMa:SIGNaling<instance>:SCENario:TCHSpa
				RsCmwWcdmaSig_Route_Scenario_Tchspa.Value_Data value = driver.Route.Scenario.Tchspa.Value;
				driver.Route.Scenario.Tchspa.Value = value;
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:RSIGnaling:STATe
				ReducedSignStateEnum value = driver.Rsignaling.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:PSWitched:STATe
				PswitchedStateEnum value = driver.Pswitched.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:CSWitched:STATe
				CswitchedStateEnum value = driver.Cswitched.State.Fetch();				
			}
			{	// CALL:WCDMa:SIGNaling<instance>:RSIGnaling:ACTion
				driver.Call.Rsignaling.Action = false;
			}
			{	// CALL:WCDMa:SIGNaling<instance>:PSWitched:ACTion
				foreach (PswitchedActionEnum x in new PswitchedActionEnum[] { PswitchedActionEnum.ACONnect, PswitchedActionEnum.CONNect, PswitchedActionEnum.DISConnect, PswitchedActionEnum.HANDover })
				{
					driver.Call.Pswitched.Action = x;					
				}
			}
			{	// CALL:WCDMa:SIGNaling<instance>:CSWitched:ACTion
				foreach (CswitchedActionEnum x in new CswitchedActionEnum[] { CswitchedActionEnum.CONNect, CswitchedActionEnum.DISConnect, CswitchedActionEnum.HANDover, CswitchedActionEnum.SSMS, CswitchedActionEnum.UNRegister })
				{
					driver.Call.Cswitched.Action = x;					
				}
			}
			{	// SOURce:WCDMa:SIGNaling<instance>:CELL:STATe:ALL
				RsCmwWcdmaSig_Source_Cell_State.All_Data value = driver.Source.Cell.State.All;
			}
			{	// SOURce:WCDMa:SIGNaling<instance>:CELL:STATe
				bool value = driver.Source.Cell.State.Value;
				driver.Source.Cell.State.Value = value;
			}
			{	// CALCulate:WCDMa:SIGNaling<instance>:BER
				RsCmwWcdmaSig_Ber.Calculate_Data value = driver.Ber.Calculate();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:BER
				RsCmwWcdmaSig_Ber.ResultData value = driver.Ber.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:BER
				RsCmwWcdmaSig_Ber.ResultData value = driver.Ber.Read();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:BER
				driver.Ber.Stop();
				driver.Ber.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:BER
				driver.Ber.Abort();
				driver.Ber.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:BER
				driver.Ber.Initiate();
				driver.Ber.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:BER:STATe
				ResourceStateEnum value = driver.Ber.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:BER:STATe:ALL
				RsCmwWcdmaSig_Ber_State_All.Fetch_Data value = driver.Ber.State.All.Fetch();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:THRoughput
				driver.Throughput.Stop();
				driver.Throughput.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:THRoughput
				driver.Throughput.Abort();
				driver.Throughput.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:THRoughput
				driver.Throughput.Initiate();
				driver.Throughput.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput
				RsCmwWcdmaSig_Throughput.ResultData value = driver.Throughput.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput
				RsCmwWcdmaSig_Throughput.ResultData value = driver.Throughput.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:STATe
				ResourceStateEnum value = driver.Throughput.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:STATe:ALL
				RsCmwWcdmaSig_Throughput_State_All.Fetch_Data value = driver.Throughput.State.All.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Sdu.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:SDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Sdu.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Read();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:HACK
				driver.Hack.Stop();
				driver.Hack.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:HACK
				driver.Hack.Abort();
				driver.Hack.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:HACK
				driver.Hack.Initiate();
				driver.Hack.InitiateAndWait();
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:TBLock:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.TransportBlock.Minimum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:TBLock:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.TransportBlock.Minimum.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:TBLock:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.TransportBlock.Maximum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:TBLock:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.TransportBlock.Maximum.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:CODE:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Code.Minimum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:CODE:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Code.Minimum.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:CODE:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Code.Maximum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:CODE:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Code.Maximum.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:MODulation:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Modulation.Minimum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:MODulation:MINimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Modulation.Minimum.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:MODulation:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Modulation.Maximum.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:SUBFrame:CARRier<carrier>:MODulation:MAXimum
				List<int> value = driver.Hack.Trace.Subframe.Carrier.Modulation.Maximum.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:TOTal:AVERage
				List<double> value = driver.Hack.Trace.Throughput.Total.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:TOTal:AVERage
				List<double> value = driver.Hack.Trace.Throughput.Total.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:TOTal:CURRent
				List<double> value = driver.Hack.Trace.Throughput.Total.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:TOTal:CURRent
				List<double> value = driver.Hack.Trace.Throughput.Total.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:CARRier<carrier>:AVERage
				List<double> value = driver.Hack.Trace.Throughput.Carrier.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:CARRier<carrier>:AVERage
				List<double> value = driver.Hack.Trace.Throughput.Carrier.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:CARRier<carrier>:CURRent
				List<double> value = driver.Hack.Trace.Throughput.Carrier.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:THRoughput:CARRier<carrier>:CURRent
				List<double> value = driver.Hack.Trace.Throughput.Carrier.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRACe:MCQI:CARRier<carrier>:CURRent
				List<int> value = driver.Hack.Trace.Mcqi.Carrier.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRACe:MCQI:CARRier<carrier>:CURRent
				List<int> value = driver.Hack.Trace.Mcqi.Carrier.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:MCQI:CARRier<carrier>
				int value = driver.Hack.Mcqi.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:MCQI:CARRier<carrier>
				int value = driver.Hack.Mcqi.Carrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:MSFRames
				int value = driver.Hack.MsFrames.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:MSFRames
				int value = driver.Hack.MsFrames.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:BLER:CARRier<carrier>
				double value = driver.Hack.Bler.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:BLER:CARRier<carrier>
				double value = driver.Hack.Bler.Carrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:THRoughput:CARRier<carrier>:RELative
				RsCmwWcdmaSig_Hack_Throughput_Carrier_Relative.ResultData value = driver.Hack.Throughput.Carrier.Relative.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:THRoughput:CARRier<carrier>:RELative
				RsCmwWcdmaSig_Hack_Throughput_Carrier_Relative.ResultData value = driver.Hack.Throughput.Carrier.Relative.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:THRoughput:CARRier<carrier>:ABSolute
				RsCmwWcdmaSig_Hack_Throughput_Carrier_Absolute.ResultData value = driver.Hack.Throughput.Carrier.Absolute.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:THRoughput:CARRier<carrier>:ABSolute
				RsCmwWcdmaSig_Hack_Throughput_Carrier_Absolute.ResultData value = driver.Hack.Throughput.Carrier.Absolute.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:TRANsmission:CARRier<carrier>
				RsCmwWcdmaSig_Hack_Transmission_Carrier.ResultData value = driver.Hack.Transmission.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HACK:TRANsmission:CARRier<carrier>
				RsCmwWcdmaSig_Hack_Transmission_Carrier.ResultData value = driver.Hack.Transmission.Carrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:STATe
				ResourceStateEnum value = driver.Hack.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HACK:STATe:ALL
				RsCmwWcdmaSig_Hack_State_All.Fetch_Data value = driver.Hack.State.All.Fetch();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:HCQI
				driver.Hcqi.Stop();
				driver.Hcqi.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:HCQI
				driver.Hcqi.Abort();
				driver.Hcqi.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:HCQI
				driver.Hcqi.Initiate();
				driver.Hcqi.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:STATe
				ResourceStateEnum value = driver.Hcqi.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:STATe:ALL
				RsCmwWcdmaSig_Hcqi_State_All.Fetch_Data value = driver.Hcqi.State.All.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:RSTate
				ResultStateEnum value = driver.Hcqi.Rstate.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>
				RsCmwWcdmaSig_Hcqi_Carrier.ResultData value = driver.Hcqi.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>
				RsCmwWcdmaSig_Hcqi_Carrier.ResultData value = driver.Hcqi.Carrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:BLER
				RsCmwWcdmaSig_Hcqi_Carrier_Bler.ResultData value = driver.Hcqi.Carrier.Bler.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:BLER
				RsCmwWcdmaSig_Hcqi_Carrier_Bler.ResultData value = driver.Hcqi.Carrier.Bler.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:DTX
				RsCmwWcdmaSig_Hcqi_Carrier_Dtx.ResultData value = driver.Hcqi.Carrier.Dtx.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:DTX
				RsCmwWcdmaSig_Hcqi_Carrier_Dtx.ResultData value = driver.Hcqi.Carrier.Dtx.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:MSFRames
				RsCmwWcdmaSig_Hcqi_Carrier_MsFrames.ResultData value = driver.Hcqi.Carrier.MsFrames.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HCQI:CARRier<carrier>:MSFRames
				RsCmwWcdmaSig_Hcqi_Carrier_MsFrames.ResultData value = driver.Hcqi.Carrier.MsFrames.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:HCQI:TRACe:CARRier<carrier>
				List<double> value = driver.Hcqi.Trace.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:HCQI:TRACe:CARRier<carrier>
				List<double> value = driver.Hcqi.Trace.Carrier.Read();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:ULLogging
				driver.UplinkLogging.Stop();
				driver.UplinkLogging.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:ULLogging
				driver.UplinkLogging.Abort();
				driver.UplinkLogging.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:ULLogging
				driver.UplinkLogging.Initiate();
				driver.UplinkLogging.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:STATe
				ResourceStateEnum value = driver.UplinkLogging.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:STATe:ALL
				RsCmwWcdmaSig_UplinkLogging_State_All.Fetch_Data value = driver.UplinkLogging.State.All.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:SFN
				List<int> value = driver.UplinkLogging.Sfn.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:SFN
				List<int> value = driver.UplinkLogging.Sfn.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:SLOT
				List<int> value = driver.UplinkLogging.Slot.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:SLOT
				List<int> value = driver.UplinkLogging.Slot.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:ETFCi
				List<EtfciEnum> value = driver.UplinkLogging.Carrier.Etfci.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:ETFCi
				List<EtfciEnum> value = driver.UplinkLogging.Carrier.Etfci.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:RSN
				List<RetransmisionSeqNrEnum> value = driver.UplinkLogging.Carrier.Rsn.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:RSN
				List<RetransmisionSeqNrEnum> value = driver.UplinkLogging.Carrier.Rsn.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:HBIT
				List<HappyBitEnum> value = driver.UplinkLogging.Carrier.Hbit.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:HBIT
				List<HappyBitEnum> value = driver.UplinkLogging.Carrier.Hbit.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:DPCCh
				RsCmwWcdmaSig_UplinkLogging_Carrier_Dpcch.ResultData value = driver.UplinkLogging.Carrier.Dpcch.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:DPCCh
				RsCmwWcdmaSig_UplinkLogging_Carrier_Dpcch.ResultData value = driver.UplinkLogging.Carrier.Dpcch.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:ANACk
				List<AckNackEnum> value = driver.UplinkLogging.Carrier.Anack.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:ANACk
				List<AckNackEnum> value = driver.UplinkLogging.Carrier.Anack.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:CQI
				List<CqiEnum> value = driver.UplinkLogging.Carrier.Cqi.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:CARRier<carrier>:CQI
				List<CqiEnum> value = driver.UplinkLogging.Carrier.Cqi.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging[:SCELl]
				RsCmwWcdmaSig_UplinkLogging_Scell.ResultData value = driver.UplinkLogging.Scell.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging[:SCELl]
				RsCmwWcdmaSig_UplinkLogging_Scell.ResultData value = driver.UplinkLogging.Scell.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:DCARrier
				RsCmwWcdmaSig_UplinkLogging_Dcarrier.ResultData value = driver.UplinkLogging.Dcarrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:DCARrier
				RsCmwWcdmaSig_UplinkLogging_Dcarrier.ResultData value = driver.UplinkLogging.Dcarrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ULLogging:DCHSpa
				RsCmwWcdmaSig_UplinkLogging_Dchspa.ResultData value = driver.UplinkLogging.Dchspa.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ULLogging:DCHSpa
				RsCmwWcdmaSig_UplinkLogging_Dchspa.ResultData value = driver.UplinkLogging.Dchspa.Read();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:EAGCh
				driver.Eagch.Stop();
				driver.Eagch.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:EAGCh
				driver.Eagch.Abort();
				driver.Eagch.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:EAGCh
				driver.Eagch.Initiate();
				driver.Eagch.InitiateAndWait();
			}
			{	// READ:WCDMa:SIGNaling<instance>:EAGCh
				RsCmwWcdmaSig_Eagch.ResultData value = driver.Eagch.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EAGCh
				RsCmwWcdmaSig_Eagch.ResultData value = driver.Eagch.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EAGCh:STATe
				ResourceStateEnum value = driver.Eagch.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EAGCh:STATe:ALL
				RsCmwWcdmaSig_Eagch_State_All.Fetch_Data value = driver.Eagch.State.All.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EAGCh:TRACe:GENeral
				RsCmwWcdmaSig_Eagch_Trace_General.ResultData value = driver.Eagch.Trace.General.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EAGCh:TRACe:GENeral
				RsCmwWcdmaSig_Eagch_Trace_General.ResultData value = driver.Eagch.Trace.General.Fetch();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:EHICh
				driver.Ehich.Stop();
				driver.Ehich.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:EHICh
				driver.Ehich.Abort();
				driver.Ehich.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:EHICh
				driver.Ehich.Initiate();
				driver.Ehich.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:STATe
				ResourceStateEnum value = driver.Ehich.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:STATe:ALL
				RsCmwWcdmaSig_Ehich_State_All.Fetch_Data value = driver.Ehich.State.All.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:TRACe:MPTHroughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.MpThroughput.Carrier.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:TRACe:MPTHroughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.MpThroughput.Carrier.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:TRACe:METHroughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.MeThroughput.Carrier.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:TRACe:METHroughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.MeThroughput.Carrier.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:TRACe:THRoughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.Throughput.Carrier.Current.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:TRACe:THRoughput:CARRier<carrier>:CURRent
				List<double> value = driver.Ehich.Trace.Throughput.Carrier.Current.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:TRACe:THRoughput:CARRier<carrier>:AVERage
				List<double> value = driver.Ehich.Trace.Throughput.Carrier.Average.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:TRACe:THRoughput:CARRier<carrier>:AVERage
				List<double> value = driver.Ehich.Trace.Throughput.Carrier.Average.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:CARRier<carrier>
				RsCmwWcdmaSig_Ehich_Carrier.ResultData value = driver.Ehich.Carrier.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:CARRier<carrier>
				RsCmwWcdmaSig_Ehich_Carrier.ResultData value = driver.Ehich.Carrier.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:EHICh:THRoughput:TOTal
				RsCmwWcdmaSig_Ehich_Throughput_Total.ResultData value = driver.Ehich.Throughput.Total.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:EHICh:THRoughput:TOTal
				RsCmwWcdmaSig_Ehich_Throughput_Total.ResultData value = driver.Ehich.Throughput.Total.Fetch();				
			}
			{	// STOP:WCDMa:SIGNaling<instance>:ERGCh
				driver.Ergch.Stop();
				driver.Ergch.StopAndWait();
			}
			{	// ABORt:WCDMa:SIGNaling<instance>:ERGCh
				driver.Ergch.Abort();
				driver.Ergch.AbortAndWait();
			}
			{	// INITiate:WCDMa:SIGNaling<instance>:ERGCh
				driver.Ergch.Initiate();
				driver.Ergch.InitiateAndWait();
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ERGCh
				RsCmwWcdmaSig_Ergch.ResultData value = driver.Ergch.Fetch();				
			}
			{	// READ:WCDMa:SIGNaling<instance>:ERGCh
				RsCmwWcdmaSig_Ergch.ResultData value = driver.Ergch.Read();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ERGCh:STATe
				ResourceStateEnum value = driver.Ergch.State.Fetch();				
			}
			{	// FETCh:WCDMa:SIGNaling<instance>:ERGCh:STATe:ALL
				RsCmwWcdmaSig_Ergch_State_All.Fetch_Data value = driver.Ergch.State.All.Fetch();				
			}
		}
	}
}