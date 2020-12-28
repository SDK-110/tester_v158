using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwWlanSig;

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
			RsCmwWlanSig driver = new RsCmwWlanSig("TCPIP::localhost::INSTR", true, true);
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:FSIMulator:STANdard
				foreach (ProfileEnum x in new ProfileEnum[] { ProfileEnum.MODA, ProfileEnum.MODB, ProfileEnum.MODC, ProfileEnum.MODD, ProfileEnum.MODE, ProfileEnum.MODF })
				{
					driver.Configure.Fading.Fsimulator.Standard = x;
					ProfileEnum value = driver.Configure.Fading.Fsimulator.Standard;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Fsimulator.Enable;
				driver.Configure.Fading.Fsimulator.Enable = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Fsimulator.InsertionLoss.Loss;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:AWGN:ENABle
				bool value = driver.Configure.Fading.Awgn.Enable;
				driver.Configure.Fading.Awgn.Enable = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:AWGN:SNRatio
				double value = driver.Configure.Fading.Awgn.SnRatio;
				driver.Configure.Fading.Awgn.SnRatio = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:FADing:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Awgn.Bandwidth.Ratio;
				driver.Configure.Fading.Awgn.Bandwidth.Ratio = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:EDAU:NID
				int value = driver.Configure.Edau.Nid;
				driver.Configure.Edau.Nid = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:EDAU:NSEGment
				foreach (SegmentNumberEnum x in new SegmentNumberEnum[] { SegmentNumberEnum.A, SegmentNumberEnum.B, SegmentNumberEnum.C })
				{
					driver.Configure.Edau.Nsegment = x;
					SegmentNumberEnum value = driver.Configure.Edau.Nsegment;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:EDAU:ENABle
				bool value = driver.Configure.Edau.Enable;
				driver.Configure.Edau.Enable = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:MIMO:TMMode
				foreach (MimoModeEnum x in new MimoModeEnum[] { MimoModeEnum.SMULtiplexin, MimoModeEnum.STBC, MimoModeEnum.TXDiversity })
				{
					driver.Configure.Mimo.TmMode = x;
					MimoModeEnum value = driver.Configure.Mimo.TmMode;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:MIMO:TCSD
				RsCmwWlanSig_Configure_Mimo.Tcsd_Data value = driver.Configure.Mimo.Tcsd;
				driver.Configure.Mimo.Tcsd = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:ETOE:DUIP
				RsCmwWlanSig_Configure_Etoe.DuIp_Data value = driver.Configure.Etoe.DuIp;
				driver.Configure.Etoe.DuIp = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:ETOE:IRList:IPRaddress<n>
				RsCmwWlanSig_Configure_Etoe_IrList_IprAddress.IprAddress_Data value = driver.Configure.Etoe.IrList.IprAddress.Get(IpRouteAddressRepCap.Default);
				value = driver.Configure.Etoe.IrList.IprAddress.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:ETOE:IRList:IPRaddress<n>
				RsCmwWlanSig_Configure_Etoe_IrList_IprAddress.IprAddress_Data value = new RsCmwWlanSig_Configure_Etoe_IrList_IprAddress.IprAddress_Data();
				driver.Configure.Etoe.IrList.IprAddress.Set(value, IpRouteAddressRepCap.Default);
				driver.Configure.Etoe.IrList.IprAddress.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:OCWidth
				foreach (ChannelBandwidthDutEnum x in new ChannelBandwidthDutEnum[] { ChannelBandwidthDutEnum.BW160, ChannelBandwidthDutEnum.BW20, ChannelBandwidthDutEnum.BW40, ChannelBandwidthDutEnum.BW80 })
				{
					driver.Configure.RfSettings.OcWidth = x;
					ChannelBandwidthDutEnum value = driver.Configure.RfSettings.OcWidth;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:FOFFset
				int value = driver.Configure.RfSettings.FreqOffset;
				driver.Configure.RfSettings.FreqOffset = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:CHANnel
				int value = driver.Configure.RfSettings.Channel;
				driver.Configure.RfSettings.Channel = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:BAND
				foreach (FrequencyBandEnum x in new FrequencyBandEnum[] { FrequencyBandEnum.B6GHz, FrequencyBandEnum.BS6Ghz })
				{
					driver.Configure.RfSettings.Band = x;
					FrequencyBandEnum value = driver.Configure.RfSettings.Band;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:FREQuency
				double value = driver.Configure.RfSettings.Frequency;
				driver.Configure.RfSettings.Frequency = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:NPINdex
				int value = driver.Configure.RfSettings.NpIndex;
				driver.Configure.RfSettings.NpIndex = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:NPFRequency
				double value = driver.Configure.RfSettings.NpFrequency;
				driver.Configure.RfSettings.NpFrequency = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:NPCHannel
				int value = driver.Configure.RfSettings.NpChannel;
				driver.Configure.RfSettings.NpChannel = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:MLOFfset
				int value = driver.Configure.RfSettings.MlOffset;
				driver.Configure.RfSettings.MlOffset = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:EPEPower
				double value = driver.Configure.RfSettings.EpePower;
				driver.Configure.RfSettings.EpePower = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:TSRatio
				double value = driver.Configure.RfSettings.TsRatio;
				driver.Configure.RfSettings.TsRatio = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:BOPower
				double value = driver.Configure.RfSettings.BoPower;
				driver.Configure.RfSettings.BoPower = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:MLOFfset
				int value = driver.Configure.RfSettings.Antenna.MlOffset.Get(AntennaRepCap.Default);
				value = driver.Configure.RfSettings.Antenna.MlOffset.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:MLOFfset
				driver.Configure.RfSettings.Antenna.MlOffset.Set(1, AntennaRepCap.Default);
				driver.Configure.RfSettings.Antenna.MlOffset.Set(1);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EPEPower
				double value = driver.Configure.RfSettings.Antenna.EpePower.Get(AntennaRepCap.Default);
				value = driver.Configure.RfSettings.Antenna.EpePower.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EPEPower
				driver.Configure.RfSettings.Antenna.EpePower.Set(1.0, AntennaRepCap.Default);
				driver.Configure.RfSettings.Antenna.EpePower.Set(1.0);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Antenna.Eattenuation.Input.Get(AntennaRepCap.Default);
				value = driver.Configure.RfSettings.Antenna.Eattenuation.Input.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EATTenuation:INPut
				driver.Configure.RfSettings.Antenna.Eattenuation.Input.Set(1.0, AntennaRepCap.Default);
				driver.Configure.RfSettings.Antenna.Eattenuation.Input.Set(1.0);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EATTenuation:OUTPut
				double value = driver.Configure.RfSettings.Antenna.Eattenuation.Output.Get(AntennaRepCap.Default);
				value = driver.Configure.RfSettings.Antenna.Eattenuation.Output.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:ANTenna<n>:EATTenuation:OUTPut
				driver.Configure.RfSettings.Antenna.Eattenuation.Output.Set(1.0, AntennaRepCap.Default);
				driver.Configure.RfSettings.Antenna.Eattenuation.Output.Set(1.0);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:RFSettings:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Eattenuation.Input;
				driver.Configure.RfSettings.Eattenuation.Input = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:IVSupport
				foreach (IpVersionExtEnum x in new IpVersionExtEnum[] { IpVersionExtEnum.IV4, IpVersionExtEnum.IV4V6, IpVersionExtEnum.IV6 })
				{
					driver.Configure.Connection.IvSupport = x;
					IpVersionExtEnum value = driver.Configure.Connection.IvSupport;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:OMODe
				foreach (EntityOperationModeEnum x in new EntityOperationModeEnum[] { EntityOperationModeEnum.AP, EntityOperationModeEnum.HSPot2, EntityOperationModeEnum.IBSS, EntityOperationModeEnum.STATion, EntityOperationModeEnum.TESTmode, EntityOperationModeEnum.WDIRect })
				{
					driver.Configure.Connection.Omode = x;
					EntityOperationModeEnum value = driver.Configure.Connection.Omode;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SMOothing
				foreach (SmoothingBitEnum x in new SmoothingBitEnum[] { SmoothingBitEnum.NRECommended, SmoothingBitEnum.RECommended })
				{
					driver.Configure.Connection.Smoothing = x;
					SmoothingBitEnum value = driver.Configure.Connection.Smoothing;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:PAINterrupt
				bool value = driver.Configure.Connection.PaInterrupt;
				driver.Configure.Connection.PaInterrupt = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:MFDef
				RsCmwWlanSig_Configure_Connection.MfDef_Data value = driver.Configure.Connection.MfDef;
				driver.Configure.Connection.MfDef = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFDef
				RsCmwWlanSig_Configure_Connection.Dfdef_Data value = driver.Configure.Connection.Dfdef;
				driver.Configure.Connection.Dfdef = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SYNC
				bool value = driver.Configure.Connection.Sync;
				driver.Configure.Connection.Sync = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SSID
				string value = driver.Configure.Connection.Ssid;
				driver.Configure.Connection.Ssid = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:AID
				RsCmwWlanSig_Configure_Connection.Aid_Data value = driver.Configure.Connection.Aid;
				driver.Configure.Connection.Aid = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BSSColor
				int value = driver.Configure.Connection.BssColor;
				driver.Configure.Connection.BssColor = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BSSid
				string value = driver.Configure.Connection.Bssid;
				driver.Configure.Connection.Bssid = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BEACon
				int value = driver.Configure.Connection.Beacon;
				driver.Configure.Connection.Beacon = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DPERiod
				int value = driver.Configure.Connection.Dperiod;
				driver.Configure.Connection.Dperiod = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:STANdard
				foreach (StandardTypeEnum x in new StandardTypeEnum[] { StandardTypeEnum.ACSTd, StandardTypeEnum.ANSTd, StandardTypeEnum.ASTD, StandardTypeEnum.AXSTd, StandardTypeEnum.BSTD, StandardTypeEnum.GNSTd, StandardTypeEnum.GONStd, StandardTypeEnum.GOSTd, StandardTypeEnum.GSTD, StandardTypeEnum.NGFStd })
				{
					driver.Configure.Connection.Standard = x;
					StandardTypeEnum value = driver.Configure.Connection.Standard;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:AMPDu
				RsCmwWlanSig_Configure_Connection.Ampdu_Data value = driver.Configure.Connection.Ampdu;
				driver.Configure.Connection.Ampdu = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DYFRagment
				RsCmwWlanSig_Configure_Connection.DyFragment_Data value = driver.Configure.Connection.DyFragment;
				driver.Configure.Connection.DyFragment = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DSSS
				bool value = driver.Configure.Connection.Dsss;
				driver.Configure.Connection.Dsss = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:CUTil
				RsCmwWlanSig_Configure_Connection_Hotspot.Cutil_Data value = driver.Configure.Connection.Hotspot.Cutil;
				driver.Configure.Connection.Hotspot.Cutil = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:HSSPar
				RsCmwWlanSig_Configure_Connection_Hotspot.Hsspar_Data value = driver.Configure.Connection.Hotspot.Hsspar;
				driver.Configure.Connection.Hotspot.Hsspar = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:MNDigits
				foreach (NumOfDigitsEnum x in new NumOfDigitsEnum[] { NumOfDigitsEnum.THDigits, NumOfDigitsEnum.TWDigits })
				{
					driver.Configure.Connection.Hotspot.MnDigits = x;
					NumOfDigitsEnum value = driver.Configure.Connection.Hotspot.MnDigits;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:HSPar
				RsCmwWlanSig_Configure_Connection_Hotspot.Hspar_Data value = driver.Configure.Connection.Hotspot.Hspar;
				driver.Configure.Connection.Hotspot.Hspar = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:REALm<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Realm.Realm_Data value = driver.Configure.Connection.Hotspot.Realm.Get(RealmRepCap.Default);
				value = driver.Configure.Connection.Hotspot.Realm.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:REALm<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Realm.Realm_Data value = new RsCmwWlanSig_Configure_Connection_Hotspot_Realm.Realm_Data();
				driver.Configure.Connection.Hotspot.Realm.Set(value, RealmRepCap.Default);
				driver.Configure.Connection.Hotspot.Realm.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:DNAMe<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Dname.Dname_Data value = driver.Configure.Connection.Hotspot.Dname.Get(DomainNameRepCap.Default);
				value = driver.Configure.Connection.Hotspot.Dname.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:DNAMe<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Dname.Dname_Data value = new RsCmwWlanSig_Configure_Connection_Hotspot_Dname.Dname_Data();
				driver.Configure.Connection.Hotspot.Dname.Set(value, DomainNameRepCap.Default);
				driver.Configure.Connection.Hotspot.Dname.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:PLMN<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Plmn.Plmn_Data value = driver.Configure.Connection.Hotspot.Plmn.Get(PlnmRepCap.Default);
				value = driver.Configure.Connection.Hotspot.Plmn.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HOTSpot:PLMN<nr>
				RsCmwWlanSig_Configure_Connection_Hotspot_Plmn.Plmn_Data value = new RsCmwWlanSig_Configure_Connection_Hotspot_Plmn.Plmn_Data();
				driver.Configure.Connection.Hotspot.Plmn.Set(value, PlnmRepCap.Default);
				driver.Configure.Connection.Hotspot.Plmn.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:WDIRect:ATYPe
				RsCmwWlanSig_Configure_Connection_Wdirect.Atype_Data value = driver.Configure.Connection.Wdirect.Atype;
				driver.Configure.Connection.Wdirect.Atype = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:WDIRect:WDConf
				RsCmwWlanSig_Configure_Connection_Wdirect.Wdconf_Data value = driver.Configure.Connection.Wdirect.Wdconf;
				driver.Configure.Connection.Wdirect.Wdconf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:STATion:SCONnection
				foreach (ConnectionAllowedEnum x in new ConnectionAllowedEnum[] { ConnectionAllowedEnum.ANY, ConnectionAllowedEnum.SSID })
				{
					driver.Configure.Connection.Station.Sconnection = x;
					ConnectionAllowedEnum value = driver.Configure.Connection.Station.Sconnection;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:STATion:CMODe
				foreach (ConnectionModeEnum x in new ConnectionModeEnum[] { ConnectionModeEnum.ACONnect, ConnectionModeEnum.MANual })
				{
					driver.Configure.Connection.Station.Cmode = x;
					ConnectionModeEnum value = driver.Configure.Connection.Station.Cmode;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:TYPE
				RsCmwWlanSig_Configure_Connection_Security.Type_Data value = driver.Configure.Connection.Security.Type;
				driver.Configure.Connection.Security.Type = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:PASSphrase
				RsCmwWlanSig_Configure_Connection_Security.Passphrase_Data value = driver.Configure.Connection.Security.Passphrase;
				driver.Configure.Connection.Security.Passphrase = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:ENCRyption
				foreach (EncryptionTypeEnum x in new EncryptionTypeEnum[] { EncryptionTypeEnum.AES, EncryptionTypeEnum.DISabled, EncryptionTypeEnum.TKIP })
				{
					driver.Configure.Connection.Security.Encryption = x;
					EncryptionTypeEnum value = driver.Configure.Connection.Security.Encryption;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:EAKA:KALGo
				RsCmwWlanSig_Configure_Connection_Security_Eaka.Kalgo_Data value = driver.Configure.Connection.Security.Eaka.Kalgo;
				driver.Configure.Connection.Security.Eaka.Kalgo = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:ESIM:KTTHree
				RsCmwWlanSig_Configure_Connection_Security_Esim.KtThree_Data value = driver.Configure.Connection.Security.Esim.KtThree;
				driver.Configure.Connection.Security.Esim.KtThree = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:ESIM:KTTWo
				RsCmwWlanSig_Configure_Connection_Security_Esim.KtTwo_Data value = driver.Configure.Connection.Security.Esim.KtTwo;
				driver.Configure.Connection.Security.Esim.KtTwo = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:ESIM:KTONe
				RsCmwWlanSig_Configure_Connection_Security_Esim.Ktone_Data value = driver.Configure.Connection.Security.Esim.Ktone;
				driver.Configure.Connection.Security.Esim.Ktone = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:RSERver:MODE
				foreach (SourceIntEnum x in new SourceIntEnum[] { SourceIntEnum.EXTernal, SourceIntEnum.INTernal })
				{
					driver.Configure.Connection.Security.Rserver.Mode = x;
					SourceIntEnum value = driver.Configure.Connection.Security.Rserver.Mode;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:RSERver:SKEY
				string value = driver.Configure.Connection.Security.Rserver.Skey;
				driver.Configure.Connection.Security.Rserver.Skey = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:RSERver:PNUMber
				int value = driver.Configure.Connection.Security.Rserver.Pnumber;
				driver.Configure.Connection.Security.Rserver.Pnumber = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SECurity:RSERver:ICONf
				RsCmwWlanSig_Configure_Connection_Security_Rserver.Iconf_Data value = driver.Configure.Connection.Security.Rserver.Iconf;
				driver.Configure.Connection.Security.Rserver.Iconf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:QOS:ETOE
				foreach (TidEnum x in new TidEnum[] { TidEnum.TID0, TidEnum.TID1, TidEnum.TID2, TidEnum.TID3, TidEnum.TID4, TidEnum.TID5, TidEnum.TID6, TidEnum.TID7 })
				{
					driver.Configure.Connection.Qos.Etoe = x;
					TidEnum value = driver.Configure.Connection.Qos.Etoe;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:QOS:PRIoritiz
				foreach (PrioModeEnum x in new PrioModeEnum[] { PrioModeEnum.ROURobin, PrioModeEnum.TIDPriority })
				{
					driver.Configure.Connection.Qos.Prioritiz = x;
					PrioModeEnum value = driver.Configure.Connection.Qos.Prioritiz;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:QOS:BARMethod
				foreach (BarMethodEnum x in new BarMethodEnum[] { BarMethodEnum.EXPBar, BarMethodEnum.IMPBar, BarMethodEnum.MUBar })
				{
					driver.Configure.Connection.Qos.BarMethod = x;
					BarMethodEnum value = driver.Configure.Connection.Qos.BarMethod;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:QOS:BLACk
				RsCmwWlanSig_Configure_Connection_Qos.Black_Data value = driver.Configure.Connection.Qos.Black;
				driver.Configure.Connection.Qos.Black = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SRATes:VHTConf
				foreach (VhtRatesEnum x in new VhtRatesEnum[] { VhtRatesEnum.MC07, VhtRatesEnum.MC08, VhtRatesEnum.MC09 })
				{
					driver.Configure.Connection.Srates.VhtConf = x;
					VhtRatesEnum value = driver.Configure.Connection.Srates.VhtConf;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SRATes:OMCSconf
				RsCmwWlanSig_Configure_Connection_Srates.OmcsConf_Data value = driver.Configure.Connection.Srates.OmcsConf;
				driver.Configure.Connection.Srates.OmcsConf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SRATes:OFDMconf
				RsCmwWlanSig_Configure_Connection_Srates.OfdmConf_Data value = driver.Configure.Connection.Srates.OfdmConf;
				driver.Configure.Connection.Srates.OfdmConf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SRATes:DSSSconf
				RsCmwWlanSig_Configure_Connection_Srates.DsssConf_Data value = driver.Configure.Connection.Srates.DsssConf;
				driver.Configure.Connection.Srates.DsssConf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:SRATes
				foreach (EnableStateEnum x in new EnableStateEnum[] { EnableStateEnum.DISable, EnableStateEnum.ENABle })
				{
					driver.Configure.Connection.Srates.Value = x;
					EnableStateEnum value = driver.Configure.Connection.Srates.Value;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:BLALlocation
				RsCmwWlanSig_Configure_Connection_Dframe_Hemu.GetBlAllocation_Data value = driver.Configure.Connection.Dframe.Hemu.GetBlAllocation(Ch20IndexEnum.CHA1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:ALSField
				SubfieldEnum value = driver.Configure.Connection.Dframe.Hemu.AlsField.Get(Ch20IndexEnum.CHA1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:ALSField
				driver.Configure.Connection.Dframe.Hemu.AlsField.Set(Ch20IndexEnum.CHA1, SubfieldEnum.A000);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:RUALlocation
				RsCmwWlanSig_Configure_Connection_Dframe_Hemu_RuAllocation.Get_Data value = driver.Configure.Connection.Dframe.Hemu.RuAllocation.Get(Ch20IndexEnum.CHA1, RuIndexEnum.RU1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:RUALlocation
				driver.Configure.Connection.Dframe.Hemu.RuAllocation.Set(Ch20IndexEnum.CHA1, RuIndexEnum.RU1, RuAllocEnum.DMY1);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:ALLocation
				RsCmwWlanSig_Configure_Connection_Dframe_Hemu_User_Allocation.Allocation_Data value = driver.Configure.Connection.Dframe.Hemu.User.Allocation.Get(UserRepCap.Default);
				value = driver.Configure.Connection.Dframe.Hemu.User.Allocation.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:ALLocation
				RsCmwWlanSig_Configure_Connection_Dframe_Hemu_User_Allocation.Allocation_Data value = new RsCmwWlanSig_Configure_Connection_Dframe_Hemu_User_Allocation.Allocation_Data();
				driver.Configure.Connection.Dframe.Hemu.User.Allocation.Set(value, UserRepCap.Default);
				driver.Configure.Connection.Dframe.Hemu.User.Allocation.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:MCS
				McsIndexEnum value = driver.Configure.Connection.Dframe.Hemu.User.Mcs.Get(UserRepCap.Default);
				value = driver.Configure.Connection.Dframe.Hemu.User.Mcs.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:MCS
				foreach (McsIndexEnum x in new McsIndexEnum[] { McsIndexEnum.MCS, McsIndexEnum.MCS1, McsIndexEnum.MCS10, McsIndexEnum.MCS11, McsIndexEnum.MCS2, McsIndexEnum.MCS3, McsIndexEnum.MCS4, McsIndexEnum.MCS5, McsIndexEnum.MCS6, McsIndexEnum.MCS7, McsIndexEnum.MCS8, McsIndexEnum.MCS9 })
				{
					driver.Configure.Connection.Dframe.Hemu.User.Mcs.Set(x);
					driver.Configure.Connection.Dframe.Hemu.User.Mcs.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:STReams
				StreamsEnum value = driver.Configure.Connection.Dframe.Hemu.User.Streams.Get(UserRepCap.Default);
				value = driver.Configure.Connection.Dframe.Hemu.User.Streams.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:STReams
				foreach (StreamsEnum x in new StreamsEnum[] { StreamsEnum.STR1, StreamsEnum.STR2 })
				{
					driver.Configure.Connection.Dframe.Hemu.User.Streams.Set(x);
					driver.Configure.Connection.Dframe.Hemu.User.Streams.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:CTYPe
				CodingTypeEnum value = driver.Configure.Connection.Dframe.Hemu.User.Ctype.Get(UserRepCap.Default);
				value = driver.Configure.Connection.Dframe.Hemu.User.Ctype.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:USER<index>:CTYPe
				foreach (CodingTypeEnum x in new CodingTypeEnum[] { CodingTypeEnum.BCC, CodingTypeEnum.LDPC })
				{
					driver.Configure.Connection.Dframe.Hemu.User.Ctype.Set(x);
					driver.Configure.Connection.Dframe.Hemu.User.Ctype.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:DUMMy<index>:MCS
				McsIndexEnum value = driver.Configure.Connection.Dframe.Hemu.Dummy.Mcs.Get(DummyRepCap.Default);
				value = driver.Configure.Connection.Dframe.Hemu.Dummy.Mcs.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:DFRame:HEMU:DUMMy<index>:MCS
				foreach (McsIndexEnum x in new McsIndexEnum[] { McsIndexEnum.MCS, McsIndexEnum.MCS1, McsIndexEnum.MCS10, McsIndexEnum.MCS11, McsIndexEnum.MCS2, McsIndexEnum.MCS3, McsIndexEnum.MCS4, McsIndexEnum.MCS5, McsIndexEnum.MCS6, McsIndexEnum.MCS7, McsIndexEnum.MCS8, McsIndexEnum.MCS9 })
				{
					driver.Configure.Connection.Dframe.Hemu.Dummy.Mcs.Set(x);
					driver.Configure.Connection.Dframe.Hemu.Dummy.Mcs.Set(x, DummyRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TXP
				int value = driver.Configure.Connection.Hetf.Txp;
				driver.Configure.Connection.Hetf.Txp = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TXEN
				bool value = driver.Configure.Connection.Hetf.Txen;
				driver.Configure.Connection.Hetf.Txen = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:NSS
				int value = driver.Configure.Connection.Hetf.Nss;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:SSS
				int value = driver.Configure.Connection.Hetf.Sss;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:DCM
				bool value = driver.Configure.Connection.Hetf.Dcm;
				driver.Configure.Connection.Hetf.Dcm = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:MCS
				foreach (McsIndexEnum x in new McsIndexEnum[] { McsIndexEnum.MCS, McsIndexEnum.MCS1, McsIndexEnum.MCS10, McsIndexEnum.MCS11, McsIndexEnum.MCS2, McsIndexEnum.MCS3, McsIndexEnum.MCS4, McsIndexEnum.MCS5, McsIndexEnum.MCS6, McsIndexEnum.MCS7, McsIndexEnum.MCS8, McsIndexEnum.MCS9 })
				{
					driver.Configure.Connection.Hetf.Mcs = x;
					McsIndexEnum value = driver.Configure.Connection.Hetf.Mcs;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:CTYP
				foreach (CodingTypeEnum x in new CodingTypeEnum[] { CodingTypeEnum.BCC, CodingTypeEnum.LDPC })
				{
					driver.Configure.Connection.Hetf.Ctyp = x;
					CodingTypeEnum value = driver.Configure.Connection.Hetf.Ctyp;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:RUAL
				foreach (RuAllocationEnum x in new RuAllocationEnum[] { RuAllocationEnum.RU0, RuAllocationEnum.RU1, RuAllocationEnum.RU10, RuAllocationEnum.RU11, RuAllocationEnum.RU12, RuAllocationEnum.RU13, RuAllocationEnum.RU14, RuAllocationEnum.RU15, RuAllocationEnum.RU16, RuAllocationEnum.RU17, RuAllocationEnum.RU18, RuAllocationEnum.RU19, RuAllocationEnum.RU2, RuAllocationEnum.RU20, RuAllocationEnum.RU21, RuAllocationEnum.RU22, RuAllocationEnum.RU23, RuAllocationEnum.RU24, RuAllocationEnum.RU25, RuAllocationEnum.RU26, RuAllocationEnum.RU27, RuAllocationEnum.RU28, RuAllocationEnum.RU29, RuAllocationEnum.RU3, RuAllocationEnum.RU30, RuAllocationEnum.RU31, RuAllocationEnum.RU32, RuAllocationEnum.RU33, RuAllocationEnum.RU34, RuAllocationEnum.RU35, RuAllocationEnum.RU36, RuAllocationEnum.RU37, RuAllocationEnum.RU38, RuAllocationEnum.RU39, RuAllocationEnum.RU4, RuAllocationEnum.RU40, RuAllocationEnum.RU41, RuAllocationEnum.RU42, RuAllocationEnum.RU43, RuAllocationEnum.RU44, RuAllocationEnum.RU45, RuAllocationEnum.RU46, RuAllocationEnum.RU47, RuAllocationEnum.RU48, RuAllocationEnum.RU49, RuAllocationEnum.RU5, RuAllocationEnum.RU50, RuAllocationEnum.RU51, RuAllocationEnum.RU52, RuAllocationEnum.RU53, RuAllocationEnum.RU54, RuAllocationEnum.RU55, RuAllocationEnum.RU56, RuAllocationEnum.RU57, RuAllocationEnum.RU58, RuAllocationEnum.RU59, RuAllocationEnum.RU6, RuAllocationEnum.RU60, RuAllocationEnum.RU61, RuAllocationEnum.RU62, RuAllocationEnum.RU63, RuAllocationEnum.RU64, RuAllocationEnum.RU65, RuAllocationEnum.RU66, RuAllocationEnum.RU67, RuAllocationEnum.RU68, RuAllocationEnum.RU7, RuAllocationEnum.RU8, RuAllocationEnum.RU9 })
				{
					driver.Configure.Connection.Hetf.Rual = x;
					RuAllocationEnum value = driver.Configure.Connection.Hetf.Rual;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:LDPC
				bool value = driver.Configure.Connection.Hetf.Ldpc;
				driver.Configure.Connection.Hetf.Ldpc = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:APTXpower
				RsCmwWlanSig_Configure_Connection_Hetf.ApTxPower_Data value = driver.Configure.Connection.Hetf.ApTxPower;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:MLTF
				foreach (MuMimoLongTrainFieldEnum x in new MuMimoLongTrainFieldEnum[] { MuMimoLongTrainFieldEnum.MASK, MuMimoLongTrainFieldEnum.SING })
				{
					driver.Configure.Connection.Hetf.Mltf = x;
					MuMimoLongTrainFieldEnum value = driver.Configure.Connection.Hetf.Mltf;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:GILT
				foreach (GiltfEnum x in new GiltfEnum[] { GiltfEnum.L116, GiltfEnum.L216, GiltfEnum.L432 })
				{
					driver.Configure.Connection.Hetf.Gilt = x;
					GiltfEnum value = driver.Configure.Connection.Hetf.Gilt;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:CHBW
				foreach (ChannelBandwidthDutEnum x in new ChannelBandwidthDutEnum[] { ChannelBandwidthDutEnum.BW160, ChannelBandwidthDutEnum.BW20, ChannelBandwidthDutEnum.BW40, ChannelBandwidthDutEnum.BW80 })
				{
					driver.Configure.Connection.Hetf.Chbw = x;
					ChannelBandwidthDutEnum value = driver.Configure.Connection.Hetf.Chbw;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:CSR
				bool value = driver.Configure.Connection.Hetf.Csr;
				driver.Configure.Connection.Hetf.Csr = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:NOFSymbols
				int value = driver.Configure.Connection.Hetf.NofSymbols;
				driver.Configure.Connection.Hetf.NofSymbols = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TTYP
				foreach (TriggerTypeEnum x in new TriggerTypeEnum[] { TriggerTypeEnum.BQRP, TriggerTypeEnum.BRP, TriggerTypeEnum.BSRP, TriggerTypeEnum.BTR, TriggerTypeEnum.MRTS })
				{
					driver.Configure.Connection.Hetf.Ttyp = x;
					TriggerTypeEnum value = driver.Configure.Connection.Hetf.Ttyp;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TRSSi
				RsCmwWlanSig_Configure_Connection_Hetf.Trssi_Data value = driver.Configure.Connection.Hetf.Trssi;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TRSMode
				foreach (TriggerFrmPowerModeEnum x in new TriggerFrmPowerModeEnum[] { TriggerFrmPowerModeEnum.AUTO, TriggerFrmPowerModeEnum.MANual, TriggerFrmPowerModeEnum.MAXPower })
				{
					driver.Configure.Connection.Hetf.TrsMode = x;
					TriggerFrmPowerModeEnum value = driver.Configure.Connection.Hetf.TrsMode;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:TSRControl
				int value = driver.Configure.Connection.Hetf.TsrControl;
				driver.Configure.Connection.Hetf.TsrControl = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HETF:SSTX
				driver.Configure.Connection.Hetf.SsTx.Set();
				driver.Configure.Connection.Hetf.SsTx.SetAndWait();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:CCODe:CCSTate
				foreach (EnableStateEnum x in new EnableStateEnum[] { EnableStateEnum.DISable, EnableStateEnum.ENABle })
				{
					driver.Configure.Connection.Ccode.CcState = x;
					EnableStateEnum value = driver.Configure.Connection.Ccode.CcState;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:CCODe:CCConf
				RsCmwWlanSig_Configure_Connection_Ccode.Ccconf_Data value = driver.Configure.Connection.Ccode.Ccconf;
				driver.Configure.Connection.Ccode.Ccconf = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:EDCA:ACBE
				RsCmwWlanSig_Configure_Connection_Edca.Acbe_Data value = driver.Configure.Connection.Edca.Acbe;
				driver.Configure.Connection.Edca.Acbe = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:EDCA:ACBK
				RsCmwWlanSig_Configure_Connection_Edca.Acbk_Data value = driver.Configure.Connection.Edca.Acbk;
				driver.Configure.Connection.Edca.Acbk = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:EDCA:ACVI
				RsCmwWlanSig_Configure_Connection_Edca.Acvi_Data value = driver.Configure.Connection.Edca.Acvi;
				driver.Configure.Connection.Edca.Acvi = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:EDCA:ACVO
				RsCmwWlanSig_Configure_Connection_Edca.Acvo_Data value = driver.Configure.Connection.Edca.Acvo;
				driver.Configure.Connection.Edca.Acvo = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:MUEDca:ACBE
				RsCmwWlanSig_Configure_Connection_Muedca.Acbe_Data value = driver.Configure.Connection.Muedca.Acbe;
				driver.Configure.Connection.Muedca.Acbe = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:MUEDca:ACBK
				RsCmwWlanSig_Configure_Connection_Muedca.Acbk_Data value = driver.Configure.Connection.Muedca.Acbk;
				driver.Configure.Connection.Muedca.Acbk = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:MUEDca:ACVI
				RsCmwWlanSig_Configure_Connection_Muedca.Acvi_Data value = driver.Configure.Connection.Muedca.Acvi;
				driver.Configure.Connection.Muedca.Acvi = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:MUEDca:ACVO
				RsCmwWlanSig_Configure_Connection_Muedca.Acvo_Data value = driver.Configure.Connection.Muedca.Acvo;
				driver.Configure.Connection.Muedca.Acvo = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:HEMac:BSRSupport
				bool value = driver.Configure.Connection.Hemac.BsrSupport;
				driver.Configure.Connection.Hemac.BsrSupport = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:METHod
				foreach (NdpSoundingMethodEnum x in new NdpSoundingMethodEnum[] { NdpSoundingMethodEnum.NONTrigger, NdpSoundingMethodEnum.TBASed })
				{
					driver.Configure.Connection.NdpSounding.Method = x;
					NdpSoundingMethodEnum value = driver.Configure.Connection.NdpSounding.Method;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:TYPE
				foreach (NdpSoundingTypeEnum x in new NdpSoundingTypeEnum[] { NdpSoundingTypeEnum.CQI, NdpSoundingTypeEnum.MU, NdpSoundingTypeEnum.SU })
				{
					driver.Configure.Connection.NdpSounding.Type = x;
					NdpSoundingTypeEnum value = driver.Configure.Connection.NdpSounding.Type;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:BW
				foreach (ChannelBandwidthDutEnum x in new ChannelBandwidthDutEnum[] { ChannelBandwidthDutEnum.BW160, ChannelBandwidthDutEnum.BW20, ChannelBandwidthDutEnum.BW40, ChannelBandwidthDutEnum.BW80 })
				{
					driver.Configure.Connection.NdpSounding.Bw = x;
					ChannelBandwidthDutEnum value = driver.Configure.Connection.NdpSounding.Bw;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:SPSTreams
				foreach (StreamsEnum x in new StreamsEnum[] { StreamsEnum.STR1, StreamsEnum.STR2 })
				{
					driver.Configure.Connection.NdpSounding.SpStreams = x;
					StreamsEnum value = driver.Configure.Connection.NdpSounding.SpStreams;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:LTFGi
				foreach (LtfGiEnum x in new LtfGiEnum[] { LtfGiEnum.L208, LtfGiEnum.L216, LtfGiEnum.L432 })
				{
					driver.Configure.Connection.NdpSounding.Ltfgi = x;
					LtfGiEnum value = driver.Configure.Connection.NdpSounding.Ltfgi;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:RUSTart
				int value = driver.Configure.Connection.NdpSounding.Rustart;
				driver.Configure.Connection.NdpSounding.Rustart = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:RUENd
				int value = driver.Configure.Connection.NdpSounding.Ruend;
				driver.Configure.Connection.NdpSounding.Ruend = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:TXP
				int value = driver.Configure.Connection.NdpSounding.Txp;
				driver.Configure.Connection.NdpSounding.Txp = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:TXEN
				bool value = driver.Configure.Connection.NdpSounding.Txen;
				driver.Configure.Connection.NdpSounding.Txen = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:NDPSounding:SSTX
				driver.Configure.Connection.NdpSounding.SsTx.Set();
				driver.Configure.Connection.NdpSounding.SsTx.SetAndWait();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:TWT:REQuired
				bool value = driver.Configure.Connection.Twt.Required;
				driver.Configure.Connection.Twt.Required = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:ENABle
				bool value = driver.Configure.Connection.Btwt.Enable;
				driver.Configure.Connection.Btwt.Enable = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:ENABle
				bool value = driver.Configure.Connection.Btwt.Schedule.Enable.Get(1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:ENABle
				driver.Configure.Connection.Btwt.Schedule.Enable.Set(1, false);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:FTYPe
				FlowTypeEnum value = driver.Configure.Connection.Btwt.Schedule.Ftype.Get(1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:FTYPe
				driver.Configure.Connection.Btwt.Schedule.Ftype.Set(1, FlowTypeEnum.ANNounced);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:STIMe
				double value = driver.Configure.Connection.Btwt.Schedule.Stime.Get(1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:STIMe
				driver.Configure.Connection.Btwt.Schedule.Stime.Set(1, 1.0);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:MWDuration
				double value = driver.Configure.Connection.Btwt.Schedule.MwDuration.Get(1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:MWDuration
				driver.Configure.Connection.Btwt.Schedule.MwDuration.Set(1, 1.0);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:TENable
				bool value = driver.Configure.Connection.Btwt.Schedule.Tenable.Get(1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:CONNection:BTWT:SCHedule:TENable
				driver.Configure.Connection.Btwt.Schedule.Tenable.Set(1, false);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:IPVersion
				IpVersionEnum value = driver.Configure.Pgen.IpVersion.Get(PacketGeneratorRepCap.Default);
				value = driver.Configure.Pgen.IpVersion.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:IPVersion
				foreach (IpVersionEnum x in new IpVersionEnum[] { IpVersionEnum.IV4, IpVersionEnum.IV6 })
				{
					driver.Configure.Pgen.IpVersion.Set(x);
					driver.Configure.Pgen.IpVersion.Set(x, PacketGeneratorRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:UPORts
				RsCmwWlanSig_Configure_Pgen_Uports.Uports_Data value = driver.Configure.Pgen.Uports.Get(PacketGeneratorRepCap.Default);
				value = driver.Configure.Pgen.Uports.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:UPORts
				RsCmwWlanSig_Configure_Pgen_Uports.Uports_Data value = new RsCmwWlanSig_Configure_Pgen_Uports.Uports_Data();
				driver.Configure.Pgen.Uports.Set(value, PacketGeneratorRepCap.Default);
				driver.Configure.Pgen.Uports.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:PROTocol
				ProtocolTypeEnum value = driver.Configure.Pgen.Protocol.Get(PacketGeneratorRepCap.Default);
				value = driver.Configure.Pgen.Protocol.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:PROTocol
				foreach (ProtocolTypeEnum x in new ProtocolTypeEnum[] { ProtocolTypeEnum.ICMP, ProtocolTypeEnum.UDP })
				{
					driver.Configure.Pgen.Protocol.Set(x);
					driver.Configure.Pgen.Protocol.Set(x, PacketGeneratorRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:CONFig
				RsCmwWlanSig_Configure_Pgen_Config.Config_Data value = driver.Configure.Pgen.Config.Get(PacketGeneratorRepCap.Default);
				value = driver.Configure.Pgen.Config.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PGEN<index>:CONFig
				RsCmwWlanSig_Configure_Pgen_Config.Config_Data value = new RsCmwWlanSig_Configure_Pgen_Config.Config_Data();
				driver.Configure.Pgen.Config.Set(value, PacketGeneratorRepCap.Default);
				driver.Configure.Pgen.Config.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVSix:PREFix
				string value = driver.Configure.Ipv6.Prefix;
				driver.Configure.Ipv6.Prefix = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:DHCP
				bool value = driver.Configure.Ipv4.Dhcp;
				driver.Configure.Ipv4.Dhcp = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:SMASk
				RsCmwWlanSig_Configure_Ipv4_Static.Smask_Data value = driver.Configure.Ipv4.Static.Smask;
				driver.Configure.Ipv4.Static.Smask = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:CMW
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Cmw_Data value = driver.Configure.Ipv4.Static.IpAddress.Cmw;
				driver.Configure.Ipv4.Static.IpAddress.Cmw = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:UE
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Ue_Data value = driver.Configure.Ipv4.Static.IpAddress.Ue;
				driver.Configure.Ipv4.Static.IpAddress.Ue = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:GATeway
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Gateway_Data value = driver.Configure.Ipv4.Static.IpAddress.Gateway;
				driver.Configure.Ipv4.Static.IpAddress.Gateway = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:DNS
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Dns_Data value = driver.Configure.Ipv4.Static.IpAddress.Dns;
				driver.Configure.Ipv4.Static.IpAddress.Dns = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:STACk
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Stack_Data value = driver.Configure.Ipv4.Static.IpAddress.Stack;
				driver.Configure.Ipv4.Static.IpAddress.Stack = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:IPVFour:STATic:IPADdress:DESTination
				RsCmwWlanSig_Configure_Ipv4_Static_IpAddress.Destination_Data value = driver.Configure.Ipv4.Static.IpAddress.Destination;
				driver.Configure.Ipv4.Static.IpAddress.Destination = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:HETBased:FRAMes
				int value = driver.Configure.HetBased.Frames;
				driver.Configure.HetBased.Frames = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:FDEF
				RsCmwWlanSig_Configure_Per.Fdef_Data value = driver.Configure.Per.Fdef;
				driver.Configure.Per.Fdef = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DPATtern
				foreach (PatternEnum x in new PatternEnum[] { PatternEnum.AONE, PatternEnum.AZERo, PatternEnum.PN1, PatternEnum.PN10, PatternEnum.PN11, PatternEnum.PN12, PatternEnum.PN13, PatternEnum.PN14, PatternEnum.PN15, PatternEnum.PN16, PatternEnum.PN17, PatternEnum.PN18, PatternEnum.PN19, PatternEnum.PN2, PatternEnum.PN20, PatternEnum.PN21, PatternEnum.PN22, PatternEnum.PN23, PatternEnum.PN24, PatternEnum.PN25, PatternEnum.PN26, PatternEnum.PN27, PatternEnum.PN28, PatternEnum.PN29, PatternEnum.PN3, PatternEnum.PN30, PatternEnum.PN31, PatternEnum.PN32, PatternEnum.PN4, PatternEnum.PN5, PatternEnum.PN6, PatternEnum.PN7, PatternEnum.PN8, PatternEnum.PN9, PatternEnum.PRANdom, PatternEnum.PT01, PatternEnum.PT10 })
				{
					driver.Configure.Per.Dpattern = x;
					PatternEnum value = driver.Configure.Per.Dpattern;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DINTerval
				int value = driver.Configure.Per.Dinterval;
				driver.Configure.Per.Dinterval = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:TIDentifier
				foreach (TidEnum x in new TidEnum[] { TidEnum.TID0, TidEnum.TID1, TidEnum.TID2, TidEnum.TID3, TidEnum.TID4, TidEnum.TID5, TidEnum.TID6, TidEnum.TID7 })
				{
					driver.Configure.Per.Tidentifier = x;
					TidEnum value = driver.Configure.Per.Tidentifier;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:PACKets
				int value = driver.Configure.Per.Packets;
				driver.Configure.Per.Packets = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:ATYPe
				foreach (AckTypeEnum x in new AckTypeEnum[] { AckTypeEnum.ACK })
				{
					driver.Configure.Per.Atype = x;
					AckTypeEnum value = driver.Configure.Per.Atype;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Per.Repetition = x;
					RepeatEnum value = driver.Configure.Per.Repetition;
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:BLALlocation
				RsCmwWlanSig_Configure_Per_Dframe_Hemu.GetBlAllocation_Data value = driver.Configure.Per.Dframe.Hemu.GetBlAllocation(Ch20IndexEnum.CHA1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:ALSField
				SubfieldEnum value = driver.Configure.Per.Dframe.Hemu.AlsField.Get(Ch20IndexEnum.CHA1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:ALSField
				driver.Configure.Per.Dframe.Hemu.AlsField.Set(Ch20IndexEnum.CHA1, SubfieldEnum.A000);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:RUALlocation
				RsCmwWlanSig_Configure_Per_Dframe_Hemu_RuAllocation.Get_Data value = driver.Configure.Per.Dframe.Hemu.RuAllocation.Get(Ch20IndexEnum.CHA1, RuIndexEnum.RU1);				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:RUALlocation
				driver.Configure.Per.Dframe.Hemu.RuAllocation.Set(Ch20IndexEnum.CHA1, RuIndexEnum.RU1, RuAllocEnum.DMY1);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:ALLocation
				RsCmwWlanSig_Configure_Per_Dframe_Hemu_User_Allocation.Allocation_Data value = driver.Configure.Per.Dframe.Hemu.User.Allocation.Get(UserRepCap.Default);
				value = driver.Configure.Per.Dframe.Hemu.User.Allocation.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:ALLocation
				RsCmwWlanSig_Configure_Per_Dframe_Hemu_User_Allocation.Allocation_Data value = new RsCmwWlanSig_Configure_Per_Dframe_Hemu_User_Allocation.Allocation_Data();
				driver.Configure.Per.Dframe.Hemu.User.Allocation.Set(value, UserRepCap.Default);
				driver.Configure.Per.Dframe.Hemu.User.Allocation.Set(value);
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:MCS
				McsIndexEnum value = driver.Configure.Per.Dframe.Hemu.User.Mcs.Get(UserRepCap.Default);
				value = driver.Configure.Per.Dframe.Hemu.User.Mcs.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:MCS
				foreach (McsIndexEnum x in new McsIndexEnum[] { McsIndexEnum.MCS, McsIndexEnum.MCS1, McsIndexEnum.MCS10, McsIndexEnum.MCS11, McsIndexEnum.MCS2, McsIndexEnum.MCS3, McsIndexEnum.MCS4, McsIndexEnum.MCS5, McsIndexEnum.MCS6, McsIndexEnum.MCS7, McsIndexEnum.MCS8, McsIndexEnum.MCS9 })
				{
					driver.Configure.Per.Dframe.Hemu.User.Mcs.Set(x);
					driver.Configure.Per.Dframe.Hemu.User.Mcs.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:STReams
				StreamsEnum value = driver.Configure.Per.Dframe.Hemu.User.Streams.Get(UserRepCap.Default);
				value = driver.Configure.Per.Dframe.Hemu.User.Streams.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:STReams
				foreach (StreamsEnum x in new StreamsEnum[] { StreamsEnum.STR1, StreamsEnum.STR2 })
				{
					driver.Configure.Per.Dframe.Hemu.User.Streams.Set(x);
					driver.Configure.Per.Dframe.Hemu.User.Streams.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:CTYPe
				CodingTypeEnum value = driver.Configure.Per.Dframe.Hemu.User.Ctype.Get(UserRepCap.Default);
				value = driver.Configure.Per.Dframe.Hemu.User.Ctype.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:USER<index>:CTYPe
				foreach (CodingTypeEnum x in new CodingTypeEnum[] { CodingTypeEnum.BCC, CodingTypeEnum.LDPC })
				{
					driver.Configure.Per.Dframe.Hemu.User.Ctype.Set(x);
					driver.Configure.Per.Dframe.Hemu.User.Ctype.Set(x, UserRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:DUMMy<index>:MCS
				McsIndexEnum value = driver.Configure.Per.Dframe.Hemu.Dummy.Mcs.Get(DummyRepCap.Default);
				value = driver.Configure.Per.Dframe.Hemu.Dummy.Mcs.Get();
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:DFRame:HEMU:DUMMy<index>:MCS
				foreach (McsIndexEnum x in new McsIndexEnum[] { McsIndexEnum.MCS, McsIndexEnum.MCS1, McsIndexEnum.MCS10, McsIndexEnum.MCS11, McsIndexEnum.MCS2, McsIndexEnum.MCS3, McsIndexEnum.MCS4, McsIndexEnum.MCS5, McsIndexEnum.MCS6, McsIndexEnum.MCS7, McsIndexEnum.MCS8, McsIndexEnum.MCS9 })
				{
					driver.Configure.Per.Dframe.Hemu.Dummy.Mcs.Set(x);
					driver.Configure.Per.Dframe.Hemu.Dummy.Mcs.Set(x, DummyRepCap.Default);
				}
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:PER:PAYLoad:SIZE
				int value = driver.Configure.Per.Payload.Size;
				driver.Configure.Per.Payload.Size = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:MMONitor:IPADdress
				RsCmwWlanSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:WLAN:SIGNaling<instance>:MMONitor:IPADdress
				foreach (IpAddrIndexEnum x in new IpAddrIndexEnum[] { IpAddrIndexEnum.IP1, IpAddrIndexEnum.IP2, IpAddrIndexEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo:RXBPower
				double value = driver.Sense.UesInfo.RxbPower;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo:DRATe
				RsCmwWlanSig_Sense_UesInfo.Drate_Data value = driver.Sense.UesInfo.Drate;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo:ABSReport
				RsCmwWlanSig_Sense_UesInfo.AbsReport_Data value = driver.Sense.UesInfo.AbsReport;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo[:ANTenna<n>]:ARXBpower
				double value = driver.Sense.UesInfo.Antenna.GetArxbPower(AntennaRepCap.Default);
				value = driver.Sense.UesInfo.Antenna.GetArxbPower();
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo:UEADdress:IPV<n>
				string value = driver.Sense.UesInfo.UeAddress.GetIpv(IpVersionRepCap.Default);
				value = driver.Sense.UesInfo.UeAddress.GetIpv();
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UESinfo:CMWaddress:IPV<n>
				string value = driver.Sense.UesInfo.CmwAddress.GetIpv(IpVersionRepCap.Default);
				value = driver.Sense.UesInfo.CmwAddress.GetIpv();
			}
			{	// SENSe:WLAN:SIGNaling<instance>:STAinfo:APSSid
				string value = driver.Sense.StaInfo.ApSsid;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:SINFo:EAPStat
				RsCmwWlanSig_Sense_Sinfo.EapStat_Data value = driver.Sense.Sinfo.EapStat;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:SINFo[:ANTenna<n>]:RXPindicator
				RsCmwWlanSig_Sense_Sinfo_Antenna.GetRxpIndicator_Data value = driver.Sense.Sinfo.Antenna.GetRxpIndicator(AntennaRepCap.Default);
				value = driver.Sense.Sinfo.Antenna.GetRxpIndicator();
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UECapability:HE
				RsCmwWlanSig_Sense_UeCapability.He_Data value = driver.Sense.UeCapability.He;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:UECapability:MAC:ADDRess
				string value = driver.Sense.UeCapability.Mac.Address;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:HETBinfo:UPHinfo
				RsCmwWlanSig_Sense_HetbInfo.UphInfo_Data value = driver.Sense.HetbInfo.UphInfo;
			}
			{	// SENSe:WLAN:SIGNaling<instance>:ELOGging:ALL
				RsCmwWlanSig_Sense_EventLogging.All_Data value = driver.Sense.EventLogging.All;
			}
			{	// ROUTe:WLAN:SIGNaling<instance>
				RsCmwWlanSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:WLAN:SIGNaling<instance>:SCENario
				foreach (ScenarioEnum x in new ScenarioEnum[] { ScenarioEnum.MIMFading, ScenarioEnum.MIMO, ScenarioEnum.MIMO2, ScenarioEnum.SCFading, ScenarioEnum.STANdard, ScenarioEnum.UNDefined })
				{
					ScenarioEnum value = driver.Route.Scenario.Value;
				}
			}
			{	// ROUTe:WLAN:SIGNaling<instance>:SCENario:MIMO:FLEXible
				RsCmwWlanSig_Route_Scenario_Mimo.Flexible_Data value = driver.Route.Scenario.Mimo.Flexible;
				driver.Route.Scenario.Mimo.Flexible = value;
			}
			{	// ROUTe:WLAN:SIGNaling<instance>:SCENario:SCELl:FLEXible
				RsCmwWlanSig_Route_Scenario_Scell.Flexible_Data value = driver.Route.Scenario.Scell.Flexible;
				driver.Route.Scenario.Scell.Flexible = value;
			}
			{	// ROUTe:WLAN:SIGNaling<instance>:SCENario:SCFading:FLEXible
				RsCmwWlanSig_Route_Scenario_ScFading.Flexible_Data value = driver.Route.Scenario.ScFading.Flexible;
				driver.Route.Scenario.ScFading.Flexible = value;
			}
			{	// ROUTe:WLAN:SIGNaling<instance>:SCENario:MIMFading:FLEXible
				RsCmwWlanSig_Route_Scenario_MimFading.Flexible_Data value = driver.Route.Scenario.MimFading.Flexible;
				driver.Route.Scenario.MimFading.Flexible = value;
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:DSMLength
				RsCmwWlanSig_Trigger_Rx_MacFrame.DsmLength_Data value = driver.Trigger.Rx.MacFrame.DsmLength;
				driver.Trigger.Rx.MacFrame.DsmLength = value;
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:OFMLength
				RsCmwWlanSig_Trigger_Rx_MacFrame.OfmLength_Data value = driver.Trigger.Rx.MacFrame.OfmLength;
				driver.Trigger.Rx.MacFrame.OfmLength = value;
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:BTYPe
				foreach (BurstTypeEnum x in new BurstTypeEnum[] { BurstTypeEnum.ABURsts, BurstTypeEnum.DCBursts, BurstTypeEnum.HESBursts, BurstTypeEnum.HTBursts, BurstTypeEnum.NHTBursts, BurstTypeEnum.OBURsts, BurstTypeEnum.OFF, BurstTypeEnum.ON, BurstTypeEnum.VHTBursts })
				{
					driver.Trigger.Rx.MacFrame.Btype = x;
					BurstTypeEnum value = driver.Trigger.Rx.MacFrame.Btype;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:BW
				foreach (TriggerBandwidthEnum x in new TriggerBandwidthEnum[] { TriggerBandwidthEnum.ALL, TriggerBandwidthEnum.BW160, TriggerBandwidthEnum.BW20, TriggerBandwidthEnum.BW40, TriggerBandwidthEnum.BW80, TriggerBandwidthEnum.OFF, TriggerBandwidthEnum.ON })
				{
					driver.Trigger.Rx.MacFrame.Bw = x;
					TriggerBandwidthEnum value = driver.Trigger.Rx.MacFrame.Bw;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:STReams
				foreach (SpatialStreamsEnum x in new SpatialStreamsEnum[] { SpatialStreamsEnum.ALL, SpatialStreamsEnum.OFF, SpatialStreamsEnum.ON, SpatialStreamsEnum.STR1, SpatialStreamsEnum.STR2 })
				{
					driver.Trigger.Rx.MacFrame.Streams = x;
					SpatialStreamsEnum value = driver.Trigger.Rx.MacFrame.Streams;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:RATE
				foreach (TriggerRateEnum x in new TriggerRateEnum[] { TriggerRateEnum.ALL, TriggerRateEnum.BR12, TriggerRateEnum.BR34, TriggerRateEnum.C11Mbits, TriggerRateEnum.C55Mbits, TriggerRateEnum.D1MBit, TriggerRateEnum.D2MBits, TriggerRateEnum.MCS0, TriggerRateEnum.MCS1, TriggerRateEnum.MCS10, TriggerRateEnum.MCS11, TriggerRateEnum.MCS12, TriggerRateEnum.MCS13, TriggerRateEnum.MCS14, TriggerRateEnum.MCS15, TriggerRateEnum.MCS2, TriggerRateEnum.MCS3, TriggerRateEnum.MCS4, TriggerRateEnum.MCS5, TriggerRateEnum.MCS6, TriggerRateEnum.MCS7, TriggerRateEnum.MCS8, TriggerRateEnum.MCS9, TriggerRateEnum.OFF, TriggerRateEnum.ON, TriggerRateEnum.Q1M12, TriggerRateEnum.Q1M34, TriggerRateEnum.Q6M23, TriggerRateEnum.Q6M34, TriggerRateEnum.QR12, TriggerRateEnum.QR34 })
				{
					driver.Trigger.Rx.MacFrame.Rate = x;
					TriggerRateEnum value = driver.Trigger.Rx.MacFrame.Rate;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:CTDelay
				foreach (DelayTypeEnum x in new DelayTypeEnum[] { DelayTypeEnum.BURSt, DelayTypeEnum.CONStant })
				{
					driver.Trigger.Rx.MacFrame.CtDelay = x;
					DelayTypeEnum value = driver.Trigger.Rx.MacFrame.CtDelay;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:RREStriction
				bool value = driver.Trigger.Rx.MacFrame.Rrestriction;
				driver.Trigger.Rx.MacFrame.Rrestriction = value;
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:SLOPe
				foreach (TriggerSlopeEnum x in new TriggerSlopeEnum[] { TriggerSlopeEnum.FEDGe, TriggerSlopeEnum.OFF, TriggerSlopeEnum.ON, TriggerSlopeEnum.REDGe })
				{
					driver.Trigger.Rx.MacFrame.Slope = x;
					TriggerSlopeEnum value = driver.Trigger.Rx.MacFrame.Slope;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:PLENgth:MODE
				foreach (LenModeEnum x in new LenModeEnum[] { LenModeEnum.DEFault, LenModeEnum.OFF, LenModeEnum.ON, LenModeEnum.UDEFined })
				{
					driver.Trigger.Rx.MacFrame.Plength.Mode = x;
					LenModeEnum value = driver.Trigger.Rx.MacFrame.Plength.Mode;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:RX:MACFrame:PLENgth:VALue
				double value = driver.Trigger.Rx.MacFrame.Plength.Value;
				driver.Trigger.Rx.MacFrame.Plength.Value = value;
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:TX:MACFrame:SLOPe
				foreach (TriggerSlopeEnum x in new TriggerSlopeEnum[] { TriggerSlopeEnum.FEDGe, TriggerSlopeEnum.OFF, TriggerSlopeEnum.ON, TriggerSlopeEnum.REDGe })
				{
					driver.Trigger.Tx.MacFrame.Slope = x;
					TriggerSlopeEnum value = driver.Trigger.Tx.MacFrame.Slope;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:TX:MACFrame:PLENgth:MODE
				foreach (PulseLengthModeEnum x in new PulseLengthModeEnum[] { PulseLengthModeEnum.BLENgth, PulseLengthModeEnum.DEFault, PulseLengthModeEnum.OFF, PulseLengthModeEnum.ON, PulseLengthModeEnum.UDEFined })
				{
					driver.Trigger.Tx.MacFrame.Plength.Mode = x;
					PulseLengthModeEnum value = driver.Trigger.Tx.MacFrame.Plength.Mode;
				}
			}
			{	// TRIGger:WLAN:SIGNaling<instance>:TX:MACFrame:PLENgth:VALue
				double value = driver.Trigger.Tx.MacFrame.Plength.Value;
				driver.Trigger.Tx.MacFrame.Plength.Value = value;
			}
			{	// CALL:WLAN:SIGNaling<Instance>:ACTion:WPS:SCONnection
				driver.Call.Action.Wps.Sconnection.Set();
				driver.Call.Action.Wps.Sconnection.SetAndWait();
			}
			{	// CALL:WLAN:SIGNaling<Instance>:ACTion:WDIRect:SCONnection
				driver.Call.Action.Wdirect.Sconnection.Set();
				driver.Call.Action.Wdirect.Sconnection.SetAndWait();
			}
			{	// CALL:WLAN:SIGNaling<Instance>:ACTion:STATion:REConnect
				driver.Call.Action.Station.Reconnect.Set();
				driver.Call.Action.Station.Reconnect.SetAndWait();
			}
			{	// CALL:WLAN:SIGNaling<Instance>:ACTion:STATion:CONNect
				driver.Call.Action.Station.Connect.Set();
				driver.Call.Action.Station.Connect.SetAndWait();
			}
			{	// CALL:WLAN:SIGNaling<Instance>:ACTion:DISConnect
				driver.Call.Action.Disconnect.Set();
				driver.Call.Action.Disconnect.SetAndWait();
			}
			{	// FETCh:WLAN:SIGNaling<instance>:PSWitched:STATe
				PsStateEnum value = driver.Pswitched.State.Fetch();				
			}
			{	// SOURce:WLAN:SIGNaling<instance>:STATe:ALL
				RsCmwWlanSig_Source_State.All_Data value = driver.Source.State.All;
			}
			{	// SOURce:WLAN:SIGNaling<instance>:STATe
				bool value = driver.Source.State.Value;
				driver.Source.State.Value = value;
			}
			{	// INITiate:WLAN:SIGNaling<instance>:HETBased
				driver.HetBased.Initiate();
				driver.HetBased.InitiateAndWait();
			}
			{	// STOP:WLAN:SIGNaling<instance>:HETBased
				driver.HetBased.Stop();
				driver.HetBased.StopAndWait();
			}
			{	// ABORt:WLAN:SIGNaling<instance>:HETBased
				driver.HetBased.Abort();
				driver.HetBased.AbortAndWait();
			}
			{	// FETCh:WLAN:SIGNaling<instance>:HETBased:STATe
				HeTbMainMeasStateEnum value = driver.HetBased.State.Fetch();				
			}
			{	// FETCh:WLAN:SIGNaling<instance>:HETBased:UPHinfo
				RsCmwWlanSig_HetBased_UphInfo.Fetch_Data value = driver.HetBased.UphInfo.Fetch();				
			}
			{	// FETCh:WLAN:SIGNaling<instance>:PACKrate
				CodeRateEnum value = driver.PackRate.Fetch();				
			}
			{	// READ:WLAN:SIGNaling<instance>:PER
				RsCmwWlanSig_Per.ResultData value = driver.Per.Read();				
			}
			{	// FETCh:WLAN:SIGNaling<instance>:PER
				RsCmwWlanSig_Per.ResultData value = driver.Per.Fetch();				
			}
			{	// STOP:WLAN:SIGNaling<instance>:PER
				driver.Per.Stop();
				driver.Per.StopAndWait();
			}
			{	// ABORt:WLAN:SIGNaling<instance>:PER
				driver.Per.Abort();
				driver.Per.AbortAndWait();
			}
			{	// INITiate:WLAN:SIGNaling<instance>:PER
				driver.Per.Initiate();
				driver.Per.InitiateAndWait();
			}
			{	// FETCh:WLAN:SIGNaling<instance>:PER:STATe
				ResourceStateEnum value = driver.Per.State.Fetch();				
			}
			{	// FETCh:WLAN:SIGNaling<instance>:PER:STATe:ALL
				RsCmwWlanSig_Per_State_All.Fetch_Data value = driver.Per.State.All.Fetch();				
			}
		}
	}
}