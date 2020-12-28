using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwBluetoothSig;

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
			RsCmwBluetoothSig driver = new RsCmwBluetoothSig("TCPIP::localhost::INSTR", true, true);
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:OPMode
				foreach (OperatingModeEnum x in new OperatingModeEnum[] { OperatingModeEnum.AUDio, OperatingModeEnum.CNTest, OperatingModeEnum.ECMode, OperatingModeEnum.LETMode, OperatingModeEnum.PROFiles, OperatingModeEnum.RFTest })
				{
					driver.Configure.OpMode = x;
					OperatingModeEnum value = driver.Configure.OpMode;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CPRotocol
				foreach (CommProtocolEnum x in new CommProtocolEnum[] { CommProtocolEnum.HCI, CommProtocolEnum.TWO })
				{
					driver.Configure.Cprotocol = x;
					CommProtocolEnum value = driver.Configure.Cprotocol;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:STANdard
				foreach (SignalingStandardEnum x in new SignalingStandardEnum[] { SignalingStandardEnum.CLASsic, SignalingStandardEnum.LESignaling })
				{
					driver.Configure.Standard = x;
					SignalingStandardEnum value = driver.Configure.Standard;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:DELay:PTIMeout
				int value = driver.Configure.Delay.Ptimeout;
				driver.Configure.Delay.Ptimeout = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:DELay:TMODe
				int value = driver.Configure.Delay.Tmode;
				driver.Configure.Delay.Tmode = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TMODe:LENergy
				bool value = driver.Configure.Tmode.LowEnergy;
				driver.Configure.Tmode.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TMODe
				foreach (TestModeEnum x in new TestModeEnum[] { TestModeEnum.LOOPback, TestModeEnum.TXTest })
				{
					driver.Configure.Tmode.Value = x;
					TestModeEnum value = driver.Configure.Tmode.Value;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:AUDio:PRFRole
				foreach (ProfileRoleEnum x in new ProfileRoleEnum[] { ProfileRoleEnum.ADGate, ProfileRoleEnum.ASINk, ProfileRoleEnum.HNDFree })
				{
					driver.Configure.Audio.PrfRole = x;
					ProfileRoleEnum value = driver.Configure.Audio.PrfRole;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:AUDio:CMWRole
				foreach (PriorityRoleEnum x in new PriorityRoleEnum[] { PriorityRoleEnum.MASTer, PriorityRoleEnum.SLAVe })
				{
					driver.Configure.Audio.CmwRole = x;
					PriorityRoleEnum value = driver.Configure.Audio.CmwRole;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:BTYPe
				foreach (BurstTypeEnum x in new BurstTypeEnum[] { BurstTypeEnum.BR, BurstTypeEnum.EDR, BurstTypeEnum.LE })
				{
					driver.Configure.Connection.Btype = x;
					BurstTypeEnum value = driver.Configure.Connection.Btype;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:DELay
				bool value = driver.Configure.Connection.Delay;
				driver.Configure.Connection.Delay = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:WHITening
				bool value = driver.Configure.Connection.Whitening;
				driver.Configure.Connection.Whitening = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:SECMode
				foreach (SecurityModeEnum x in new SecurityModeEnum[] { SecurityModeEnum.SEC2, SecurityModeEnum.SEC3 })
				{
					driver.Configure.Connection.Audio.SecMode = x;
					SecurityModeEnum value = driver.Configure.Connection.Audio.SecMode;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:VLINk
				foreach (VoiceLinkTypeEnum x in new VoiceLinkTypeEnum[] { VoiceLinkTypeEnum.ESCO, VoiceLinkTypeEnum.SCO })
				{
					driver.Configure.Connection.Audio.Vlink = x;
					VoiceLinkTypeEnum value = driver.Configure.Connection.Audio.Vlink;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:PINCode
				string value = driver.Configure.Connection.Audio.PinCode;
				driver.Configure.Connection.Audio.PinCode = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:CODec
				foreach (SpeechCodeEnum x in new SpeechCodeEnum[] { SpeechCodeEnum.ALAW, SpeechCodeEnum.CVSD, SpeechCodeEnum.MSBC, SpeechCodeEnum.ULAW })
				{
					driver.Configure.Connection.Audio.Codec = x;
					SpeechCodeEnum value = driver.Configure.Connection.Audio.Codec;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:VOLControl:MICGain
				int value = driver.Configure.Connection.Audio.VolControl.MicGain;
				driver.Configure.Connection.Audio.VolControl.MicGain = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:VOLControl:SPEaker
				int value = driver.Configure.Connection.Audio.VolControl.Speaker;
				driver.Configure.Connection.Audio.VolControl.Speaker = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:HFP:CASTartup
				bool value = driver.Configure.Connection.Audio.Hfp.Castartup;
				driver.Configure.Connection.Audio.Hfp.Castartup = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:ACCSlave
				bool value = driver.Configure.Connection.Audio.A2Dp.AccSlave;
				driver.Configure.Connection.Audio.A2Dp.AccSlave = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:BITRate
				int value = driver.Configure.Connection.Audio.A2Dp.Bitrate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:MAXBitpool
				int value = driver.Configure.Connection.Audio.A2Dp.MaxBitPool;
				driver.Configure.Connection.Audio.A2Dp.MaxBitPool = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:MINBitpool
				int value = driver.Configure.Connection.Audio.A2Dp.MinBitPool;
				driver.Configure.Connection.Audio.A2Dp.MinBitPool = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:ALCMethod
				foreach (AllocMethodEnum x in new AllocMethodEnum[] { AllocMethodEnum.LOUDness, AllocMethodEnum.SNR })
				{
					driver.Configure.Connection.Audio.A2Dp.AlcMethod = x;
					AllocMethodEnum value = driver.Configure.Connection.Audio.A2Dp.AlcMethod;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:SUBBands
				foreach (SubBandsEnum x in new SubBandsEnum[] { SubBandsEnum.SB4, SubBandsEnum.SB8 })
				{
					driver.Configure.Connection.Audio.A2Dp.SubBands = x;
					SubBandsEnum value = driver.Configure.Connection.Audio.A2Dp.SubBands;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:BLKLength
				foreach (BlockLengthEnum x in new BlockLengthEnum[] { BlockLengthEnum.BL12, BlockLengthEnum.BL16, BlockLengthEnum.BL4, BlockLengthEnum.BL8 })
				{
					driver.Configure.Connection.Audio.A2Dp.BlkLength = x;
					BlockLengthEnum value = driver.Configure.Connection.Audio.A2Dp.BlkLength;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:CHMode
				foreach (AudioChannelModeEnum x in new AudioChannelModeEnum[] { AudioChannelModeEnum.DUAL, AudioChannelModeEnum.JSTereo, AudioChannelModeEnum.MONO, AudioChannelModeEnum.STEReo })
				{
					driver.Configure.Connection.Audio.A2Dp.Chmode = x;
					AudioChannelModeEnum value = driver.Configure.Connection.Audio.A2Dp.Chmode;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:SMPFrequency
				foreach (SamplingFrequencyEnum x in new SamplingFrequencyEnum[] { SamplingFrequencyEnum.SF16, SamplingFrequencyEnum.SF32, SamplingFrequencyEnum.SF441, SamplingFrequencyEnum.SF48 })
				{
					driver.Configure.Connection.Audio.A2Dp.SmpFrequency = x;
					SamplingFrequencyEnum value = driver.Configure.Connection.Audio.A2Dp.SmpFrequency;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:A2DP:CODec
				foreach (AudioCodecEnum x in new AudioCodecEnum[] { AudioCodecEnum.SBC })
				{
					driver.Configure.Connection.Audio.A2Dp.Codec = x;
					AudioCodecEnum value = driver.Configure.Connection.Audio.A2Dp.Codec;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:SCO
				foreach (PacketTypeScoEnum x in new PacketTypeScoEnum[] { PacketTypeScoEnum.HV1, PacketTypeScoEnum.HV2, PacketTypeScoEnum.HV3 })
				{
					driver.Configure.Connection.Packets.Ptype.Sco = x;
					PacketTypeScoEnum value = driver.Configure.Connection.Packets.Ptype.Sco;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:ESCO
				foreach (PacketTypeEscoEnum x in new PacketTypeEscoEnum[] { PacketTypeEscoEnum._2EV3, PacketTypeEscoEnum._2EV5, PacketTypeEscoEnum._3EV3, PacketTypeEscoEnum._3EV5, PacketTypeEscoEnum.EV3, PacketTypeEscoEnum.EV4, PacketTypeEscoEnum.EV5, PacketTypeEscoEnum.HV1, PacketTypeEscoEnum.HV2, PacketTypeEscoEnum.HV3 })
				{
					driver.Configure.Connection.Packets.Ptype.Esco = x;
					PacketTypeEscoEnum value = driver.Configure.Connection.Packets.Ptype.Esco;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:BRATe
				foreach (BrPacketTypeEnum x in new BrPacketTypeEnum[] { BrPacketTypeEnum.DH1, BrPacketTypeEnum.DH3, BrPacketTypeEnum.DH5 })
				{
					driver.Configure.Connection.Packets.Ptype.Brate = x;
					BrPacketTypeEnum value = driver.Configure.Connection.Packets.Ptype.Brate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:EDRate
				foreach (EdrPacketTypeEnum x in new EdrPacketTypeEnum[] { EdrPacketTypeEnum.E21P, EdrPacketTypeEnum.E23P, EdrPacketTypeEnum.E25P, EdrPacketTypeEnum.E31P, EdrPacketTypeEnum.E33P, EdrPacketTypeEnum.E35P })
				{
					driver.Configure.Connection.Packets.Ptype.Edrate = x;
					EdrPacketTypeEnum value = driver.Configure.Connection.Packets.Ptype.Edrate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:LENergy[:LE1M]
				foreach (LePacketType2enum x in new LePacketType2enum[] { LePacketType2enum.RFCTe, LePacketType2enum.RFPHytest })
				{
					driver.Configure.Connection.Packets.Ptype.LowEnergy.Le1m = x;
					LePacketType2enum value = driver.Configure.Connection.Packets.Ptype.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:LENergy:LRANge
				foreach (LePacketType2enum x in new LePacketType2enum[] { LePacketType2enum.RFCTe, LePacketType2enum.RFPHytest })
				{
					driver.Configure.Connection.Packets.Ptype.LowEnergy.Lrange = x;
					LePacketType2enum value = driver.Configure.Connection.Packets.Ptype.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PTYPe:LENergy:LE2M
				foreach (LePacketType2enum x in new LePacketType2enum[] { LePacketType2enum.RFCTe, LePacketType2enum.RFPHytest })
				{
					driver.Configure.Connection.Packets.Ptype.LowEnergy.Le2m = x;
					LePacketType2enum value = driver.Configure.Connection.Packets.Ptype.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PLENgth:BRATe
				List<int> value = driver.Configure.Connection.Packets.PacketLength.Brate;
				driver.Configure.Connection.Packets.PacketLength.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PLENgth:EDRate
				List<int> value = driver.Configure.Connection.Packets.PacketLength.Edrate;
				driver.Configure.Connection.Packets.PacketLength.Edrate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PLENgth:LENergy[:LE1M]
				int value = driver.Configure.Connection.Packets.PacketLength.LowEnergy.Le1m;
				driver.Configure.Connection.Packets.PacketLength.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PLENgth:LENergy:LRANge
				int value = driver.Configure.Connection.Packets.PacketLength.LowEnergy.Lrange;
				driver.Configure.Connection.Packets.PacketLength.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PLENgth:LENergy:LE2M
				int value = driver.Configure.Connection.Packets.PacketLength.LowEnergy.Le2m;
				driver.Configure.Connection.Packets.PacketLength.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PATTern:BRATe
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Connection.Packets.Pattern.Brate = x;
					LeRangePaternTypeEnum value = driver.Configure.Connection.Packets.Pattern.Brate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PATTern:EDRate
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Connection.Packets.Pattern.Edrate = x;
					LeRangePaternTypeEnum value = driver.Configure.Connection.Packets.Pattern.Edrate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PATTern:LENergy[:LE1M]
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Connection.Packets.Pattern.LowEnergy.Le1m = x;
					LeRangePaternTypeEnum value = driver.Configure.Connection.Packets.Pattern.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PATTern:LENergy:LRANge
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Connection.Packets.Pattern.LowEnergy.Lrange = x;
					LeRangePaternTypeEnum value = driver.Configure.Connection.Packets.Pattern.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:PATTern:LENergy:LE2M
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Connection.Packets.Pattern.LowEnergy.Le2m = x;
					LeRangePaternTypeEnum value = driver.Configure.Connection.Packets.Pattern.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:UNITs:CTE:LENergy:LE1M
				int value = driver.Configure.Connection.Packets.Units.Cte.LowEnergy.Le1m;
				driver.Configure.Connection.Packets.Units.Cte.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:UNITs:CTE:LENergy:LE2M
				int value = driver.Configure.Connection.Packets.Units.Cte.LowEnergy.Le2m;
				driver.Configure.Connection.Packets.Units.Cte.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:TYPE:CTE:LENergy:LE1M
				foreach (CteTypeEnum x in new CteTypeEnum[] { CteTypeEnum.AOA1us, CteTypeEnum.AOA2us, CteTypeEnum.AOD1us, CteTypeEnum.AOD2us })
				{
					driver.Configure.Connection.Packets.Type.Cte.LowEnergy.Le1m = x;
					CteTypeEnum value = driver.Configure.Connection.Packets.Type.Cte.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:TYPE:CTE:LENergy:LE2M
				foreach (CteTypeEnum x in new CteTypeEnum[] { CteTypeEnum.AOA1us, CteTypeEnum.AOA2us, CteTypeEnum.AOD1us, CteTypeEnum.AOD2us })
				{
					driver.Configure.Connection.Packets.Type.Cte.LowEnergy.Le2m = x;
					CteTypeEnum value = driver.Configure.Connection.Packets.Type.Cte.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SYNWord:LENergy
				string value = driver.Configure.Connection.SynWord.LowEnergy;
				driver.Configure.Connection.SynWord.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:CSCHeme:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.Connection.Cscheme.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.Connection.Cscheme.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:FEC:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.Connection.Fec.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.Connection.Fec.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:FEC:NMODe:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.Connection.Fec.Nmode.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.Connection.Fec.Nmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PHY:LENergy
				foreach (LePhysicalTypeEnum x in new LePhysicalTypeEnum[] { LePhysicalTypeEnum.LE1M, LePhysicalTypeEnum.LE2M, LePhysicalTypeEnum.LELR })
				{
					driver.Configure.Connection.Phy.LowEnergy = x;
					LePhysicalTypeEnum value = driver.Configure.Connection.Phy.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PHY:NMODe:LENergy
				foreach (LePhysicalTypeEnum x in new LePhysicalTypeEnum[] { LePhysicalTypeEnum.LE1M, LePhysicalTypeEnum.LE2M, LePhysicalTypeEnum.LELR })
				{
					driver.Configure.Connection.Phy.Nmode.LowEnergy = x;
					LePhysicalTypeEnum value = driver.Configure.Connection.Phy.Nmode.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PCONtrol:EPCMode
				foreach (PowerControlModeEnum x in new PowerControlModeEnum[] { PowerControlModeEnum.AUTO, PowerControlModeEnum.OFF })
				{
					driver.Configure.Connection.PowerControl.EpcMode = x;
					PowerControlModeEnum value = driver.Configure.Connection.PowerControl.EpcMode;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PCONtrol:STEP:ACTion
				foreach (PowerControlEnum x in new PowerControlEnum[] { PowerControlEnum.DOWN, PowerControlEnum.MAX, PowerControlEnum.UP })
				{
					driver.Configure.Connection.PowerControl.Step.Action = x;					
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PAGing:PSRMode
				foreach (PsrModeEnum x in new PsrModeEnum[] { PsrModeEnum.R0, PsrModeEnum.R1, PsrModeEnum.R2 })
				{
					driver.Configure.Connection.Paging.PsrMode = x;
					PsrModeEnum value = driver.Configure.Connection.Paging.PsrMode;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PAGing:TOUT:LESignaling
				int value = driver.Configure.Connection.Paging.Timeout.LeSignaling;
				driver.Configure.Connection.Paging.Timeout.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PAGing:TOUT
				int value = driver.Configure.Connection.Paging.Timeout.Value;
				driver.Configure.Connection.Paging.Timeout.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PAGing:PTARget:LESignaling
				int value = driver.Configure.Connection.Paging.Ptarget.LeSignaling;
				driver.Configure.Connection.Paging.Ptarget.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PAGing:PTARget
				int value = driver.Configure.Connection.Paging.Ptarget.Value;
				driver.Configure.Connection.Paging.Ptarget.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:BDADdress:CMW
				string value = driver.Configure.Connection.BdAddress.Cmw;
				driver.Configure.Connection.BdAddress.Cmw = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:BDADdress:EUT
				string value = driver.Configure.Connection.BdAddress.Eut;
				driver.Configure.Connection.BdAddress.Eut = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:ILENgth
				int value = driver.Configure.Connection.Inquiry.Ilength;
				driver.Configure.Connection.Inquiry.Ilength = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:PTARgets:CATalog:LESignaling
				RsCmwBluetoothSig_Configure_Connection_Inquiry_Ptargets_Catalog.LeSignaling_Data value = driver.Configure.Connection.Inquiry.Ptargets.Catalog.LeSignaling;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:PTARgets:CATalog
				RsCmwBluetoothSig_Configure_Connection_Inquiry_Ptargets_Catalog.Value_Data value = driver.Configure.Connection.Inquiry.Ptargets.Catalog.Value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:NOResponses:LESignaling
				int value = driver.Configure.Connection.Inquiry.NoResponses.LeSignaling;
				driver.Configure.Connection.Inquiry.NoResponses.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:NOResponses
				int value = driver.Configure.Connection.Inquiry.NoResponses.Value;
				driver.Configure.Connection.Inquiry.NoResponses.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:SINTerval:LESignaling
				int value = driver.Configure.Connection.Inquiry.Sinterval.LeSignaling;
				driver.Configure.Connection.Inquiry.Sinterval.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:DURation:LESignaling
				int value = driver.Configure.Connection.Inquiry.Duration.LeSignaling;
				driver.Configure.Connection.Inquiry.Duration.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INQuiry:SWINdow:LESignaling
				int value = driver.Configure.Connection.Inquiry.Swindow.LeSignaling;
				driver.Configure.Connection.Inquiry.Swindow.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:EUTCharacter:OPCMode
				bool value = driver.Configure.Connection.EutCharacter.OpcMode;
				driver.Configure.Connection.EutCharacter.OpcMode = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:EUTCharacter:SNBehaviour
				foreach (SequenceNumberingEnum x in new SequenceNumberingEnum[] { SequenceNumberingEnum.NORM, SequenceNumberingEnum.TEST })
				{
					driver.Configure.Connection.EutCharacter.SnBehaviour = x;
					SequenceNumberingEnum value = driver.Configure.Connection.EutCharacter.SnBehaviour;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:EUTCharacter:TCPChange
				bool value = driver.Configure.Connection.EutCharacter.TcpChange;
				driver.Configure.Connection.EutCharacter.TcpChange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:EUTCharacter:RLSettling
				double value = driver.Configure.Connection.EutCharacter.RlSettling;
				driver.Configure.Connection.EutCharacter.RlSettling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:WFCMap:LESignaling:CCENtral
				int value = driver.Configure.Connection.WfcMap.LeSignaling.Ccentral;
				driver.Configure.Connection.WfcMap.LeSignaling.Ccentral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SLATency:LESignaling:CPERipheral
				int value = driver.Configure.Connection.Slatency.LeSignaling.Cperipheral;
				driver.Configure.Connection.Slatency.LeSignaling.Cperipheral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SLATency:LESignaling[:CCENtral]
				int value = driver.Configure.Connection.Slatency.LeSignaling.Ccentral;
				driver.Configure.Connection.Slatency.LeSignaling.Ccentral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:RENCryption:LESignaling:CCENtral
				bool value = driver.Configure.Connection.Rencryption.LeSignaling.Ccentral;
				driver.Configure.Connection.Rencryption.LeSignaling.Ccentral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:RENCryption:LESignaling:CPERipheral
				bool value = driver.Configure.Connection.Rencryption.LeSignaling.Cperipheral;
				driver.Configure.Connection.Rencryption.LeSignaling.Cperipheral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:IENCryption:LESignaling:CCENtral
				bool value = driver.Configure.Connection.Iencryption.LeSignaling.Ccentral;
				driver.Configure.Connection.Iencryption.LeSignaling.Ccentral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:IENCryption:LESignaling:CPERipheral
				bool value = driver.Configure.Connection.Iencryption.LeSignaling.Cperipheral;
				driver.Configure.Connection.Iencryption.LeSignaling.Cperipheral = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:CMW:ROLE:LESignaling
				foreach (SignalingCmwRoleEnum x in new SignalingCmwRoleEnum[] { SignalingCmwRoleEnum.CENTral, SignalingCmwRoleEnum.PERipheral })
				{
					driver.Configure.Connection.Cmw.Role.LeSignaling = x;
					SignalingCmwRoleEnum value = driver.Configure.Connection.Cmw.Role.LeSignaling;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:CMW:ROLE
				foreach (PriorityRoleEnum x in new PriorityRoleEnum[] { PriorityRoleEnum.MASTer, PriorityRoleEnum.SLAVe })
				{
					driver.Configure.Connection.Cmw.Role.Value = x;
					PriorityRoleEnum value = driver.Configure.Connection.Cmw.Role.Value;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:ADDRess:CMW:LESignaling
				string value = driver.Configure.Connection.Address.Cmw.LeSignaling;
				driver.Configure.Connection.Address.Cmw.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:ADDRess:EUT:LESignaling
				string value = driver.Configure.Connection.Address.Eut.LeSignaling;
				driver.Configure.Connection.Address.Eut.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:ADDRess:TYPE:LESignaling
				foreach (AddressTypeEnum x in new AddressTypeEnum[] { AddressTypeEnum.PUBLic, AddressTypeEnum.RANDom })
				{
					driver.Configure.Connection.Address.Type.LeSignaling = x;
					AddressTypeEnum value = driver.Configure.Connection.Address.Type.LeSignaling;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:RADDress:CMW:LESignaling
				string value = driver.Configure.Connection.Raddress.Cmw.LeSignaling;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SVTimeout:LESignaling
				int value = driver.Configure.Connection.SvTimeout.LeSignaling;
				driver.Configure.Connection.SvTimeout.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SVTimeout
				int value = driver.Configure.Connection.SvTimeout.Value;
				driver.Configure.Connection.SvTimeout.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:INTerval:LESignaling
				int value = driver.Configure.Connection.Interval.LeSignaling;
				driver.Configure.Connection.Interval.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SINTerval:LESignaling
				int value = driver.Configure.Connection.Sinterval.LeSignaling;
				driver.Configure.Connection.Sinterval.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:AINTerval:LESignaling
				int value = driver.Configure.Connection.Ainterval.LeSignaling;
				driver.Configure.Connection.Ainterval.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:SWINdow:LESignaling
				int value = driver.Configure.Connection.Swindow.LeSignaling;
				driver.Configure.Connection.Swindow.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PPERiod:MINimum
				bool value = driver.Configure.Connection.Pperiod.Minimum;
				driver.Configure.Connection.Pperiod.Minimum = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:CONNection:PPERiod
				int value = driver.Configure.Connection.Pperiod.Value;
				driver.Configure.Connection.Pperiod.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:LENergy:RESet:DELay
				double value = driver.Configure.LowEnergy.Reset.Delay;
				driver.Configure.LowEnergy.Reset.Delay = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:USBSettings<nr>:USBDevice
				int value = driver.Configure.UsbSettings.UsbDevice.Get(UsbSettingsRepCap.Default);
				value = driver.Configure.UsbSettings.UsbDevice.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:USBSettings<nr>:USBDevice
				driver.Configure.UsbSettings.UsbDevice.Set(1, UsbSettingsRepCap.Default);
				driver.Configure.UsbSettings.UsbDevice.Set(1);
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:USBSettings:DEVices:CATalog
				RsCmwBluetoothSig_Configure_UsbSettings_Devices.Catalog_Data value = driver.Configure.UsbSettings.Devices.Catalog;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:STOPbits
				StopBitsEnum value = driver.Configure.ComSettings.StopBits.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.StopBits.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:STOPbits
				foreach (StopBitsEnum x in new StopBitsEnum[] { StopBitsEnum.S1, StopBitsEnum.S2 })
				{
					driver.Configure.ComSettings.StopBits.Set(x);
					driver.Configure.ComSettings.StopBits.Set(x, CommSettingsRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:PARity
				ParityEnum value = driver.Configure.ComSettings.Parity.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.Parity.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:PARity
				foreach (ParityEnum x in new ParityEnum[] { ParityEnum.EVEN, ParityEnum.NONE, ParityEnum.ODD })
				{
					driver.Configure.ComSettings.Parity.Set(x);
					driver.Configure.ComSettings.Parity.Set(x, CommSettingsRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:DBITs
				DataBitsEnum value = driver.Configure.ComSettings.Dbits.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.Dbits.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:DBITs
				foreach (DataBitsEnum x in new DataBitsEnum[] { DataBitsEnum.D7, DataBitsEnum.D8 })
				{
					driver.Configure.ComSettings.Dbits.Set(x);
					driver.Configure.ComSettings.Dbits.Set(x, CommSettingsRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:COMPort
				int value = driver.Configure.ComSettings.ComPort.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.ComPort.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:COMPort
				driver.Configure.ComSettings.ComPort.Set(1, CommSettingsRepCap.Default);
				driver.Configure.ComSettings.ComPort.Set(1);
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:BAUDrate
				BaudRateEnum value = driver.Configure.ComSettings.Baudrate.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.Baudrate.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:BAUDrate
				foreach (BaudRateEnum x in new BaudRateEnum[] { BaudRateEnum.B110, BaudRateEnum.B115k, BaudRateEnum.B12K, BaudRateEnum.B14K, BaudRateEnum.B19K, BaudRateEnum.B1M, BaudRateEnum.B1M5, BaudRateEnum.B234k, BaudRateEnum.B24K, BaudRateEnum.B28K, BaudRateEnum.B2M, BaudRateEnum.B300, BaudRateEnum.B38K, BaudRateEnum.B3M, BaudRateEnum.B3M5, BaudRateEnum.B460k, BaudRateEnum.B48K, BaudRateEnum.B4M, BaudRateEnum.B500k, BaudRateEnum.B576k, BaudRateEnum.B57K, BaudRateEnum.B600, BaudRateEnum.B921k, BaudRateEnum.B96K })
				{
					driver.Configure.ComSettings.Baudrate.Set(x);
					driver.Configure.ComSettings.Baudrate.Set(x, CommSettingsRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:PROTocol
				ProtocolEnum value = driver.Configure.ComSettings.Protocol.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.Protocol.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:PROTocol
				foreach (ProtocolEnum x in new ProtocolEnum[] { ProtocolEnum.CTSRts, ProtocolEnum.NONE, ProtocolEnum.XONXoff })
				{
					driver.Configure.ComSettings.Protocol.Set(x);
					driver.Configure.ComSettings.Protocol.Set(x, CommSettingsRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:ERESet
				bool value = driver.Configure.ComSettings.Ereset.Get(CommSettingsRepCap.Default);
				value = driver.Configure.ComSettings.Ereset.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings<nr>:ERESet
				driver.Configure.ComSettings.Ereset.Set(false, CommSettingsRepCap.Default);
				driver.Configure.ComSettings.Ereset.Set(false);
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:COMSettings:PORTs:CATalog
				RsCmwBluetoothSig_Configure_ComSettings_Ports.Catalog_Data value = driver.Configure.ComSettings.Ports.Catalog;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:HWINterface<nr>
				HwInterfaceEnum value = driver.Configure.HwInterface.Get(HardwareIntfRepCap.Default);
				value = driver.Configure.HwInterface.Get();
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:HWINterface<nr>
				foreach (HwInterfaceEnum x in new HwInterfaceEnum[] { HwInterfaceEnum.NONE, HwInterfaceEnum.RS232, HwInterfaceEnum.USB })
				{
					driver.Configure.HwInterface.Set(x);
					driver.Configure.HwInterface.Set(x, HardwareIntfRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:DEBug:RX:CORRelation:THReshold
				string value = driver.Configure.Debug.Rx.Correlation.Threshold;
				driver.Configure.Debug.Rx.Correlation.Threshold = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:DEBug:RX:CORRelation:TIMeout
				string value = driver.Configure.Debug.Rx.Correlation.Timeout;
				driver.Configure.Debug.Rx.Correlation.Timeout = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:DEBug:RX:TRIGger:PLEVel
				string value = driver.Configure.Debug.Rx.Trigger.Plevel;
				driver.Configure.Debug.Rx.Trigger.Plevel = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:INTerval:LESignaling
				int value = driver.Configure.Tconnection.Interval.LeSignaling;
				driver.Configure.Tconnection.Interval.LeSignaling = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:SPINenable:LENergy
				bool value = driver.Configure.Tconnection.SpinEnable.LowEnergy;
				driver.Configure.Tconnection.SpinEnable.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PINCode:LENergy
				List<int> value = driver.Configure.Tconnection.PinCode.LowEnergy;
				driver.Configure.Tconnection.PinCode.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PATTern:LENergy:LE1M
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Le1m = x;
					LeRangePaternTypeEnum value = driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PATTern:LENergy:LE2M
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Le2m = x;
					LeRangePaternTypeEnum value = driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PATTern:LENergy:LRANge
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Lrange = x;
					LeRangePaternTypeEnum value = driver.Configure.Tconnection.Packets.Pattern.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PLENgth:LENergy:LE1M
				int value = driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Le1m;
				driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PLENgth:LENergy:LE2M
				int value = driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Le2m;
				driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PACKets:PLENgth:LENergy:LRANge
				int value = driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Lrange;
				driver.Configure.Tconnection.Packets.PacketLength.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:PHY:LENergy
				foreach (LePhysicalTypeEnum x in new LePhysicalTypeEnum[] { LePhysicalTypeEnum.LE1M, LePhysicalTypeEnum.LE2M, LePhysicalTypeEnum.LELR })
				{
					driver.Configure.Tconnection.Phy.LowEnergy = x;
					LePhysicalTypeEnum value = driver.Configure.Tconnection.Phy.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:TCONnection:FEC:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.Tconnection.Fec.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.Tconnection.Fec.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:ARPower
				double value = driver.Configure.RfSettings.ArPower;
			}
			{	// CONFigure:BLUetooth:SIGNaling<instance>:RFSettings:ARANging
				bool value = driver.Configure.RfSettings.Aranging;
				driver.Configure.RfSettings.Aranging = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:ENPower
				double value = driver.Configure.RfSettings.EnvelopePower;
				driver.Configure.RfSettings.EnvelopePower = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:LEVel
				double value = driver.Configure.RfSettings.Level;
				driver.Configure.RfSettings.Level = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<instance>:RFSettings:UMARgin
				double value = driver.Configure.RfSettings.Umargin;
				driver.Configure.RfSettings.Umargin = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:PCONtrol
				foreach (PowerControlEnum x in new PowerControlEnum[] { PowerControlEnum.DOWN, PowerControlEnum.MAX, PowerControlEnum.UP })
				{
					driver.Configure.RfSettings.PowerControl = x;
					PowerControlEnum value = driver.Configure.RfSettings.PowerControl;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:HOPPing
				bool value = driver.Configure.RfSettings.Hopping;
				driver.Configure.RfSettings.Hopping = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX
				bool value = driver.Configure.RfSettings.Dtx.Value;
				driver.Configure.RfSettings.Dtx.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:BRATe
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Brate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:NMODe:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Nmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:NMODe:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Nmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:NMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Nmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:TMODe:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.Tmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:TMODe:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.Tmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:TMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:LENergy[:LE1M]
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STANdard:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Standard.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:TMODe:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.Tmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:TMODe:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.Tmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:TMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:LENergy[:LE1M]
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:STABle:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Stable.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:TMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:MINDex:LENergy[:LE1M]
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Mindex.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:EDRate
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Edrate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:BRATe
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Brate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:NMODe:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Nmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:NMODe:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Nmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:NMODe:LENergy:LE1M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Nmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:TMODe:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Tmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:TMODe:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Tmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:TMODe:LENergy:LE1M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:LENergy[:LE1M]
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:STERror:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.StError.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:EDRate
				List<bool> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Edrate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:BRATe
				List<bool> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Brate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:NMODe:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Nmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:NMODe:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Nmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:NMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Nmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:TMODe:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Tmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:TMODe:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Tmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:TMODe:LENergy:LE1M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:LENergy[:LE1M]
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:LENergy:LRANge
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FDRift:LENergy:LE2M
				List<double> value = driver.Configure.RfSettings.Dtx.Stab.Fdrift.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:EDRate
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Edrate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:BRATe
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Brate;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:NMODe:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Nmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:NMODe:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Nmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:NMODe:LENergy:LE1M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Nmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:TMODe:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Tmode.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:TMODe:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Tmode.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:TMODe:LENergy:LE1M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.Tmode.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:LENergy[:LE1M]
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.LowEnergy.Le1m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:LENergy:LRANge
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.LowEnergy.Lrange;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:STAB:FOFFset:LENergy:LE2M
				List<int> value = driver.Configure.RfSettings.Dtx.Stab.FreqOffset.LowEnergy.Le2m;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:BRATe
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Brate;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:NMODe:LENergy:LE2M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:NMODe:LENergy:LRANge
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:NMODe:LENergy:LE1M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:TMODe:LENergy:LRANge
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:TMODe:LENergy:LE2M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:TMODe:LENergy:LE1M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:LENergy[:LE1M]
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:LENergy:LRANge
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STANdard:LENergy:LE2M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Standard.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:TMODe:LENergy:LRANge
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:TMODe:LENergy:LE2M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:TMODe:LENergy:LE1M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:LENergy[:LE1M]
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:LENergy:LRANge
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:STABle:LENergy:LE2M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Stable.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:TMODe:LENergy:LE1M
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.Tmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:MINDex:LENergy[:LE1M]
				double value = driver.Configure.RfSettings.Dtx.Sing.Mindex.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Mindex.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:EDRate
				foreach (SymbolTimeErrorEnum x in new SymbolTimeErrorEnum[] { SymbolTimeErrorEnum.NEG20, SymbolTimeErrorEnum.OFF, SymbolTimeErrorEnum.POS20 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Edrate = x;
					SymbolTimeErrorEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Edrate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:BRATe
				foreach (SymbolTimeErrorEnum x in new SymbolTimeErrorEnum[] { SymbolTimeErrorEnum.NEG20, SymbolTimeErrorEnum.OFF, SymbolTimeErrorEnum.POS20 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Brate = x;
					SymbolTimeErrorEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Brate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:NMODe:LENergy:LE2M
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Le2m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:NMODe:LENergy:LRANge
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Lrange = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:NMODe:LENergy:LE1M
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Le1m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Nmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:TMODe:LENergy:LRANge
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Lrange = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:TMODe:LENergy:LE2M
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Le2m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:TMODe:LENergy:LE1M
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Le1m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.Tmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:LENergy[:LE1M]
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Le1m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:LENergy:LRANge
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Lrange = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:STERror:LENergy:LE2M
				foreach (SymbolTimeErrorLeEnum x in new SymbolTimeErrorLeEnum[] { SymbolTimeErrorLeEnum.NEG50, SymbolTimeErrorLeEnum.OFF, SymbolTimeErrorLeEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Le2m = x;
					SymbolTimeErrorLeEnum value = driver.Configure.RfSettings.Dtx.Sing.StError.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:EDRate
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Edrate;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Edrate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:BRATe
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Brate;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:NMODe:LENergy:LE2M
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:NMODe:LENergy:LRANge
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:NMODe:LENergy:LE1M
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:TMODe:LENergy:LRANge
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:TMODe:LENergy:LE2M
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:TMODe:LENergy:LE1M
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:LENergy[:LE1M]
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:LENergy:LRANge
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FDRift:LENergy:LE2M
				bool value = driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.Fdrift.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:EDRate
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Edrate;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Edrate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:BRATe
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Brate;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:NMODe:LENergy:LE2M
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:NMODe:LENergy:LRANge
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:NMODe:LENergy:LE1M
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:TMODe:LENergy:LRANge
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:TMODe:LENergy:LE2M
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:TMODe:LENergy:LE1M
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:LENergy[:LE1M]
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Le1m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:LENergy:LRANge
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Lrange;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:SING:FOFFset:LENergy:LE2M
				int value = driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Le2m;
				driver.Configure.RfSettings.Dtx.Sing.FreqOffset.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:NMODe:LENergy:LE2M
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Le2m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:NMODe:LENergy:LRANge
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Lrange = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:NMODe:LENergy:LE1M
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Le1m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Nmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:TMODe:LENergy:LRANge
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Lrange = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:TMODe:LENergy:LE2M
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Le2m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:TMODe:LENergy:LE1M
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Le1m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.Tmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:LENergy[:LE1M]
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Le1m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:LENergy:LRANge
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Lrange = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODFrequency:LENergy:LE2M
				foreach (DriftRateEnum x in new DriftRateEnum[] { DriftRateEnum.HDRF, DriftRateEnum.LDRF })
				{
					driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Le2m = x;
					DriftRateEnum value = driver.Configure.RfSettings.Dtx.ModFrequency.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:EDRate
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Edrate = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Edrate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:BRATe
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Brate = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Brate;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:NMODe:LENergy:LE2M
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Le2m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:NMODe:LENergy:LRANge
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Lrange = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:NMODe:LENergy:LE1M
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Le1m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Nmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:TMODe:LENergy:LRANge
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Lrange = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:TMODe:LENergy:LE2M
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Le2m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:TMODe:LENergy:LE1M
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Le1m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.Tmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:LENergy[:LE1M]
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Le1m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:LENergy:LRANge
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Lrange = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MODE:LENergy:LE2M
				foreach (DtxModeEnum x in new DtxModeEnum[] { DtxModeEnum.SINGle, DtxModeEnum.SPEC })
				{
					driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Le2m = x;
					DtxModeEnum value = driver.Configure.RfSettings.Dtx.Mode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:TMODe:LENergy:LRANge
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Lrange = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:TMODe:LENergy:LE2M
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Le2m = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:TMODe:LENergy:LE1M
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Le1m = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.Tmode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:LENergy[:LE1M]
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Le1m = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:LENergy:LRANge
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Lrange = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:DTX:MINDex:MODE:LENergy:LE2M
				foreach (ModIndexTypeEnum x in new ModIndexTypeEnum[] { ModIndexTypeEnum.STAB, ModIndexTypeEnum.STAN })
				{
					driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Le2m = x;
					ModIndexTypeEnum value = driver.Configure.RfSettings.Dtx.Mindex.Mode.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:NMODe:HMODe:LENergy
				foreach (LeHoppingModeEnum x in new LeHoppingModeEnum[] { LeHoppingModeEnum.ALL, LeHoppingModeEnum.CH2 })
				{
					driver.Configure.RfSettings.Nmode.Hmode.LowEnergy = x;
					LeHoppingModeEnum value = driver.Configure.RfSettings.Nmode.Hmode.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:NMODe:MCHannel:LENergy
				int value = driver.Configure.RfSettings.Nmode.Mchannel.LowEnergy;
				driver.Configure.RfSettings.Nmode.Mchannel.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:CHANnel:TMODe
				int value = driver.Configure.RfSettings.Channel.Tmode;
				driver.Configure.RfSettings.Channel.Tmode = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:CHANnel:DTMode
				int value = driver.Configure.RfSettings.Channel.DtMode;
				driver.Configure.RfSettings.Channel.DtMode = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:CHANnel:LOOPback
				RsCmwBluetoothSig_Configure_RfSettings_Channel.Loopback_Data value = driver.Configure.RfSettings.Channel.Loopback;
				driver.Configure.RfSettings.Channel.Loopback = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:CHANnel:TXTest
				int value = driver.Configure.RfSettings.Channel.TxTest;
				driver.Configure.RfSettings.Channel.TxTest = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:FREQuency:TMODe
				double value = driver.Configure.RfSettings.Frequency.Tmode;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:FREQuency:DTMode
				double value = driver.Configure.RfSettings.Frequency.DtMode;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:FREQuency:TXTest
				double value = driver.Configure.RfSettings.Frequency.TxTest;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:FREQuency:LOOPback
				RsCmwBluetoothSig_Configure_RfSettings_Frequency.Loopback_Data value = driver.Configure.RfSettings.Frequency.Loopback;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:AIDoverride:CTE:LENergy
				List<int> value = driver.Configure.RfSettings.AidOverride.Cte.LowEnergy;
				driver.Configure.RfSettings.AidOverride.Cte.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:GOFFset:CTE:LENergy:LE1M
				RsCmwBluetoothSig_Configure_RfSettings_Goffset_Cte_LowEnergy.Le1M_Data value = driver.Configure.RfSettings.Goffset.Cte.LowEnergy.Le1m;
				driver.Configure.RfSettings.Goffset.Cte.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:GOFFset:CTE:LENergy:LE2M
				RsCmwBluetoothSig_Configure_RfSettings_Goffset_Cte_LowEnergy.Le2M_Data value = driver.Configure.RfSettings.Goffset.Cte.LowEnergy.Le2m;
				driver.Configure.RfSettings.Goffset.Cte.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:AOFFset:INPut:CTE:LENergy
				RsCmwBluetoothSig_Configure_RfSettings_Aoffset_Input_Cte.LowEnergy_Data value = driver.Configure.RfSettings.Aoffset.Input.Cte.LowEnergy;
				driver.Configure.RfSettings.Aoffset.Input.Cte.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:AOFFset:OUTPut:CTE:LENergy
				RsCmwBluetoothSig_Configure_RfSettings_Aoffset_Output_Cte.LowEnergy_Data value = driver.Configure.RfSettings.Aoffset.Output.Cte.LowEnergy;
				driver.Configure.RfSettings.Aoffset.Output.Cte.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:NANTenna:CTE:LENergy
				int value = driver.Configure.RfSettings.Nantenna.Cte.LowEnergy;
				driver.Configure.RfSettings.Nantenna.Cte.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:EATTenuation:OUTPut
				double value = driver.Configure.RfSettings.Eattenuation.Output;
				driver.Configure.RfSettings.Eattenuation.Output = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Eattenuation.Input;
				driver.Configure.RfSettings.Eattenuation.Input = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:AFHopping:UCHannels
				List<int> value = driver.Configure.RfSettings.AfHopping.Uchannels;
				driver.Configure.RfSettings.AfHopping.Uchannels = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RFSettings:AFHopping
				RsCmwBluetoothSig_Configure_RfSettings_AfHopping.Value_Data value = driver.Configure.RfSettings.AfHopping.Value;
				driver.Configure.RfSettings.AfHopping.Value = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.RxQuality.Repetition = x;
					RepeatEnum value = driver.Configure.RxQuality.Repetition;
				}
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:TOUT
				double value = driver.Configure.RxQuality.Timeout;
				driver.Configure.RxQuality.Timeout = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SCONdition
				int value = driver.Configure.RxQuality.Scondition;
				driver.Configure.RxQuality.Scondition = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQSDump
				bool value = driver.Configure.RxQuality.Iqsdump;
				driver.Configure.RxQuality.Iqsdump = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SMINdex:LENergy
				bool value = driver.Configure.RxQuality.SmIndex.LowEnergy;
				driver.Configure.RxQuality.SmIndex.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:TOUT
				double value = driver.Configure.RxQuality.Search.Timeout;
				driver.Configure.RxQuality.Search.Timeout = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:LENergy:LRANge
				bool value = driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:LENergy:LE2M
				bool value = driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:LENergy[:LE1M]
				bool value = driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Rintegrity.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:TMODe:LENergy:LE1M
				bool value = driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:TMODe:LENergy:LE2M
				bool value = driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:RINTegrity:TMODe:LENergy:LRANge
				bool value = driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Rintegrity.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:LENergy:LRANge
				double value = driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:LENergy:LE2M
				double value = driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:LENergy[:LE1M]
				double value = driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Limit.Mper.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:TMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:TMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:TMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Limit.Mper.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:NMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:NMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MPER:NMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Limit.Mper.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MBER:BRATe
				double value = driver.Configure.RxQuality.Search.Limit.Mber.Brate;
				driver.Configure.RxQuality.Search.Limit.Mber.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MBER:EDRate
				double value = driver.Configure.RxQuality.Search.Limit.Mber.Edrate;
				driver.Configure.RxQuality.Search.Limit.Mber.Edrate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MBER:TMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MBER:TMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:LIMit:MBER:TMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Limit.Mber.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets[:BEDR]
				int value = driver.Configure.RxQuality.Search.Packets.Bedr;
				driver.Configure.RxQuality.Search.Packets.Bedr = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:LENergy:LRANge
				int value = driver.Configure.RxQuality.Search.Packets.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Packets.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:LENergy:LE2M
				int value = driver.Configure.RxQuality.Search.Packets.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Packets.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:LENergy[:LE1M]
				int value = driver.Configure.RxQuality.Search.Packets.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Packets.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:TMODe:LENergy:LE1M
				int value = driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:TMODe:LENergy:LE2M
				int value = driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:TMODe:LENergy:LRANge
				int value = driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Packets.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:NMODe:LENergy:LE1M
				int value = driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:NMODe:LENergy:LE2M
				int value = driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PACKets:NMODe:LENergy:LRANge
				int value = driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Search.Packets.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:STEP:BREDr
				double value = driver.Configure.RxQuality.Search.Step.Bredr;
				driver.Configure.RxQuality.Search.Step.Bredr = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:STEP:LENergy
				double value = driver.Configure.RxQuality.Search.Step.LowEnergy;
				driver.Configure.RxQuality.Search.Step.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:STEP:TMODe:LENergy
				double value = driver.Configure.RxQuality.Search.Step.Tmode.LowEnergy;
				driver.Configure.RxQuality.Search.Step.Tmode.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:STEP:NMODe:LENergy
				double value = driver.Configure.RxQuality.Search.Step.Nmode.LowEnergy;
				driver.Configure.RxQuality.Search.Step.Nmode.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets[:BEDR]
				int value = driver.Configure.RxQuality.Packets.Bedr;
				driver.Configure.RxQuality.Packets.Bedr = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:LENergy:LRANge
				int value = driver.Configure.RxQuality.Packets.LowEnergy.Lrange;
				driver.Configure.RxQuality.Packets.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:LENergy:LE2M
				int value = driver.Configure.RxQuality.Packets.LowEnergy.Le2m;
				driver.Configure.RxQuality.Packets.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:LENergy[:LE1M]
				int value = driver.Configure.RxQuality.Packets.LowEnergy.Le1m;
				driver.Configure.RxQuality.Packets.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:TMODe:LENergy:LE1M
				int value = driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:TMODe:LENergy:LE2M
				int value = driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:TMODe:LENergy:LRANge
				int value = driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Packets.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:NMODe:LENergy:LE1M
				int value = driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:NMODe:LENergy:LE2M
				int value = driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:PACKets:NMODe:LENergy:LRANge
				int value = driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Packets.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:LENergy:LRANge
				bool value = driver.Configure.RxQuality.Rintegrity.LowEnergy.Lrange;
				driver.Configure.RxQuality.Rintegrity.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:LENergy:LE2M
				bool value = driver.Configure.RxQuality.Rintegrity.LowEnergy.Le2m;
				driver.Configure.RxQuality.Rintegrity.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:LENergy[:LE1M]
				bool value = driver.Configure.RxQuality.Rintegrity.LowEnergy.Le1m;
				driver.Configure.RxQuality.Rintegrity.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:TMODe:LENergy:LE1M
				bool value = driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:TMODe:LENergy:LE2M
				bool value = driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:RINTegrity:TMODe:LENergy:LRANge
				bool value = driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Rintegrity.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:LENergy:LRANge
				double value = driver.Configure.RxQuality.Limit.Mper.LowEnergy.Lrange;
				driver.Configure.RxQuality.Limit.Mper.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:LENergy:LE2M
				double value = driver.Configure.RxQuality.Limit.Mper.LowEnergy.Le2m;
				driver.Configure.RxQuality.Limit.Mper.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:LENergy[:LE1M]
				double value = driver.Configure.RxQuality.Limit.Mper.LowEnergy.Le1m;
				driver.Configure.RxQuality.Limit.Mper.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:TMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:TMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:TMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Limit.Mper.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:NMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:NMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MPER:NMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Limit.Mper.Nmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MBER:BRATe
				double value = driver.Configure.RxQuality.Limit.Mber.Brate;
				driver.Configure.RxQuality.Limit.Mber.Brate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MBER:EDRate
				double value = driver.Configure.RxQuality.Limit.Mber.Edrate;
				driver.Configure.RxQuality.Limit.Mber.Edrate = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MBER:TMODe:LENergy:LE1M
				double value = driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MBER:TMODe:LENergy:LE2M
				double value = driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:LIMit:MBER:TMODe:LENergy:LRANge
				double value = driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Limit.Mber.Tmode.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IBLength:LENergy:LE1M
				RsCmwBluetoothSig_Configure_RxQuality_IbLength_LowEnergy.Le1M_Data value = driver.Configure.RxQuality.IbLength.LowEnergy.Le1m;
				driver.Configure.RxQuality.IbLength.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IBLength:LENergy:LE2M
				RsCmwBluetoothSig_Configure_RxQuality_IbLength_LowEnergy.Le2M_Data value = driver.Configure.RxQuality.IbLength.LowEnergy.Le2m;
				driver.Configure.RxQuality.IbLength.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:MOEXception:LENergy:LE1M
				bool value = driver.Configure.RxQuality.IqCoherency.MoException.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqCoherency.MoException.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:MOEXception:LENergy:LE2M
				bool value = driver.Configure.RxQuality.IqCoherency.MoException.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqCoherency.MoException.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE1M:A0Reference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A0Reference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE1M:A1NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A1Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE1M:A2NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A2Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE1M:A3NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A3Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le1M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE2M:A0Reference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A0Reference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE2M:A1NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A1Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE2M:A2NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A2Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:NOMeas:LENergy:LE2M:A3NReference
				int value = driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A3Nreference;
				driver.Configure.RxQuality.IqCoherency.NoMeas.LowEnergy.Le2M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:PACKets:LENergy:LE2M
				int value = driver.Configure.RxQuality.IqCoherency.Packets.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqCoherency.Packets.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:PACKets:LENergy:LE1M
				int value = driver.Configure.RxQuality.IqCoherency.Packets.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqCoherency.Packets.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le1M.A0Reference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A0Reference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le1M.A1Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A1Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le1M.A2Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A2Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le1M.A3Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A3Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le1M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le2M.A0Reference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A0Reference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le2M.A1Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A1Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le2M.A2Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A2Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LIMit:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_Configure_RxQuality_IqCoherency_Limit_LowEnergy_Le2M.A3Nreference_Data value = driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A3Nreference;
				driver.Configure.RxQuality.IqCoherency.Limit.LowEnergy.Le2M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:MOEXception:LENergy:LE1M
				bool value = driver.Configure.RxQuality.IqDrange.MoException.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqDrange.MoException.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:MOEXception:LENergy:LE2M
				bool value = driver.Configure.RxQuality.IqDrange.MoException.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqDrange.MoException.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE1M:A0Reference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A0Reference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE1M:A1NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A1Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE1M:A2NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A2Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE1M:A3NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A3Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le1M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE2M:A0Reference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A0Reference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A0Reference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE2M:A1NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A1Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A1Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE2M:A2NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A2Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A2Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:NOMeas:LENergy:LE2M:A3NReference
				int value = driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A3Nreference;
				driver.Configure.RxQuality.IqDrange.NoMeas.LowEnergy.Le2M.A3Nreference = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:PACKets:LENergy:LE1M
				int value = driver.Configure.RxQuality.IqDrange.Packets.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqDrange.Packets.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:PACKets:LENergy:LE2M
				int value = driver.Configure.RxQuality.IqDrange.Packets.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqDrange.Packets.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LIMit:LENergy:LE1M
				RsCmwBluetoothSig_Configure_RxQuality_IqDrange_Limit_LowEnergy.Le1M_Data value = driver.Configure.RxQuality.IqDrange.Limit.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqDrange.Limit.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LIMit:LENergy:LE2M
				RsCmwBluetoothSig_Configure_RxQuality_IqDrange_Limit_LowEnergy.Le2M_Data value = driver.Configure.RxQuality.IqDrange.Limit.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqDrange.Limit.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LIMit:LENergy:LE1M
				RsCmwBluetoothSig_Configure_RxQuality_IqDrange_AntMeanAmp_Limit_LowEnergy.Le1M_Data value = driver.Configure.RxQuality.IqDrange.AntMeanAmp.Limit.LowEnergy.Le1m;
				driver.Configure.RxQuality.IqDrange.AntMeanAmp.Limit.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LIMit:LENergy:LE2M
				RsCmwBluetoothSig_Configure_RxQuality_IqDrange_AntMeanAmp_Limit_LowEnergy.Le2M_Data value = driver.Configure.RxQuality.IqDrange.AntMeanAmp.Limit.LowEnergy.Le2m;
				driver.Configure.RxQuality.IqDrange.AntMeanAmp.Limit.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:ITENd:LENergy:LE1M
				bool value = driver.Configure.RxQuality.Itend.LowEnergy.Le1m;
				driver.Configure.RxQuality.Itend.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:ITENd:LENergy:LE2M
				bool value = driver.Configure.RxQuality.Itend.LowEnergy.Le2m;
				driver.Configure.RxQuality.Itend.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:CBITs:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_Configure_RxQuality_Cbits_Tmode_LowEnergy.Le1M_Data value = driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Le1m;
				driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:CBITs:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_Configure_RxQuality_Cbits_Tmode_LowEnergy.Le2M_Data value = driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Le2m;
				driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:SIGNaling<Instance>:RXQuality:CBITs:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_Configure_RxQuality_Cbits_Tmode_LowEnergy.Lrange_Data value = driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Lrange;
				driver.Configure.RxQuality.Cbits.Tmode.LowEnergy.Lrange = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:WCMap
				int value = driver.Diagnostic.Wcmap;
				driver.Diagnostic.Wcmap = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DELay:PTIMeout
				int value = driver.Diagnostic.Delay.Ptimeout;
				driver.Diagnostic.Delay.Ptimeout = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DELay:TMODe
				int value = driver.Diagnostic.Delay.Tmode;
				driver.Diagnostic.Delay.Tmode = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:LE:MODE
				bool value = driver.Diagnostic.Le.Mode;
				driver.Diagnostic.Le.Mode = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:LE:STATe
				foreach (LeDiagStateEnum x in new LeDiagStateEnum[] { LeDiagStateEnum.LOADingvec, LeDiagStateEnum.OFF, LeDiagStateEnum.ON, LeDiagStateEnum.VECTorloaded })
				{
					LeDiagStateEnum value = driver.Diagnostic.Le.State;
				}
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:LE:PLENgth
				int value = driver.Diagnostic.Le.PacketLength;
				driver.Diagnostic.Le.PacketLength = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:LE:CHANnel
				int value = driver.Diagnostic.Le.Channel;
				driver.Diagnostic.Le.Channel = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:LE:PATTern
				foreach (LeRangePaternTypeEnum x in new LeRangePaternTypeEnum[] { LeRangePaternTypeEnum.ALL0, LeRangePaternTypeEnum.ALL1, LeRangePaternTypeEnum.ALT, LeRangePaternTypeEnum.P11, LeRangePaternTypeEnum.P44, LeRangePaternTypeEnum.PRBS9 })
				{
					driver.Diagnostic.Le.Pattern = x;
					LeRangePaternTypeEnum value = driver.Diagnostic.Le.Pattern;
				}
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:UCS:STATe
				foreach (LeDiagStateEnum x in new LeDiagStateEnum[] { LeDiagStateEnum.LOADingvec, LeDiagStateEnum.OFF, LeDiagStateEnum.ON, LeDiagStateEnum.VECTorloaded })
				{
					LeDiagStateEnum value = driver.Diagnostic.Ucs.State;
				}
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:UCS:FREQuency
				RsCmwBluetoothSig_Diagnostic_Ucs.Frequency_Data value = driver.Diagnostic.Ucs.Frequency;
				driver.Diagnostic.Ucs.Frequency = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:UCS:MODE
				bool value = driver.Diagnostic.Ucs.Mode;
				driver.Diagnostic.Ucs.Mode = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:UCS:TESTvector
				foreach (TestVectorEnum x in new TestVectorEnum[] { TestVectorEnum.INITstack, TestVectorEnum.RELoadstack, TestVectorEnum.TV0, TestVectorEnum.TV1, TestVectorEnum.TV10, TestVectorEnum.TV11, TestVectorEnum.TV12, TestVectorEnum.TV13, TestVectorEnum.TV14, TestVectorEnum.TV15, TestVectorEnum.TV16, TestVectorEnum.TV17, TestVectorEnum.TV18, TestVectorEnum.TV19, TestVectorEnum.TV2, TestVectorEnum.TV20, TestVectorEnum.TV21, TestVectorEnum.TV22, TestVectorEnum.TV23, TestVectorEnum.TV24, TestVectorEnum.TV25, TestVectorEnum.TV26, TestVectorEnum.TV27, TestVectorEnum.TV28, TestVectorEnum.TV29, TestVectorEnum.TV3, TestVectorEnum.TV30, TestVectorEnum.TV31, TestVectorEnum.TV32, TestVectorEnum.TV33, TestVectorEnum.TV34, TestVectorEnum.TV35, TestVectorEnum.TV36, TestVectorEnum.TV37, TestVectorEnum.TV38, TestVectorEnum.TV39, TestVectorEnum.TV4, TestVectorEnum.TV40, TestVectorEnum.TV41, TestVectorEnum.TV42, TestVectorEnum.TV43, TestVectorEnum.TV44, TestVectorEnum.TV5, TestVectorEnum.TV6, TestVectorEnum.TV7, TestVectorEnum.TV8, TestVectorEnum.TV9 })
				{
					driver.Diagnostic.Ucs.TestVector = x;
					TestVectorEnum value = driver.Diagnostic.Ucs.TestVector;
				}
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:SUALog
				bool value = driver.Diagnostic.Debug.SuaLog;
				driver.Diagnostic.Debug.SuaLog = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:SUAFswlog
				bool value = driver.Diagnostic.Debug.SuaFswLog;
				driver.Diagnostic.Debug.SuaFswLog = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:SIMulation
				bool value = driver.Diagnostic.Debug.Simulation;
				driver.Diagnostic.Debug.Simulation = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:HCIWindow
				bool value = driver.Diagnostic.Debug.HciWindow;
				driver.Diagnostic.Debug.HciWindow = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:APPWindow
				bool value = driver.Diagnostic.Debug.AppWindow;
				driver.Diagnostic.Debug.AppWindow = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:ATTRibwindow
				bool value = driver.Diagnostic.Debug.AttrWindow;
				driver.Diagnostic.Debug.AttrWindow = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:LINKlayer:IPADdress
				string value = driver.Diagnostic.Debug.LinkLayer.IpAddress;
				driver.Diagnostic.Debug.LinkLayer.IpAddress = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:DEBug:LINKlayer:PORTaddress
				string value = driver.Diagnostic.Debug.LinkLayer.PortAddress;
				driver.Diagnostic.Debug.LinkLayer.PortAddress = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:CONNection:PACKets:EPLength:LENergy:LE2M
				int value = driver.Diagnostic.Connection.Packets.EpLength.LowEnergy.Le2m;
				driver.Diagnostic.Connection.Packets.EpLength.LowEnergy.Le2m = value;
			}
			{	// DIAGnostic:BLUetooth:SIGNaling<Instance>:RXQuality:PERShow
				driver.Diagnostic.RxQuality.PerShow = false;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:CMAP
				List<int> value = driver.Sense.Cmap;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:DPRotocol
				string value = driver.Sense.UsbDevice.Information.Dprotocol;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:DSUBclass
				string value = driver.Sense.UsbDevice.Information.DsubClass;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:DCLass
				string value = driver.Sense.UsbDevice.Information.Dclass;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:IDPRoduct
				string value = driver.Sense.UsbDevice.Information.Idproduct;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:IDVendor
				string value = driver.Sense.UsbDevice.Information.IdVendor;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:PRODuct
				string value = driver.Sense.UsbDevice.Information.Product;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:SERial
				string value = driver.Sense.UsbDevice.Information.Serial;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:USBDevice:INFormation:MANufacturer
				string value = driver.Sense.UsbDevice.Information.Manufacturer;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:ESCO
				RsCmwBluetoothSig_Sense_Eut_Capability.Esco_Data value = driver.Sense.Eut.Capability.Esco;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:SCLass
				string value = driver.Sense.Eut.Capability.Sclass;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:ENCRyption
				bool value = driver.Sense.Eut.Capability.Encryption;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:PCONtrol
				bool value = driver.Sense.Eut.Capability.PowerControl;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:EPControl
				bool value = driver.Sense.Eut.Capability.EpControl;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:PSAVing
				RsCmwBluetoothSig_Sense_Eut_Capability.Psaving_Data value = driver.Sense.Eut.Capability.Psaving;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:CONNection
				RsCmwBluetoothSig_Sense_Eut_Capability.Connection_Data value = driver.Sense.Eut.Capability.Connection;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:SCO
				RsCmwBluetoothSig_Sense_Eut_Capability.Sco_Data value = driver.Sense.Eut.Capability.Sco;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:ACL
				RsCmwBluetoothSig_Sense_Eut_Capability.Acl_Data value = driver.Sense.Eut.Capability.Acl;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:LESignaling
				RsCmwBluetoothSig_Sense_Eut_Capability.LeSignaling_Data value = driver.Sense.Eut.Capability.LeSignaling;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CAPability:ADP[:SBC]
				RsCmwBluetoothSig_Sense_Eut_Capability_Adp.Sbc_Data value = driver.Sense.Eut.Capability.Adp.Sbc;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:BDADdress
				string value = driver.Sense.Eut.Information.BdAddress;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:CLASs
				RsCmwBluetoothSig_Sense_Eut_Information.Class_Data value = driver.Sense.Eut.Information.Class;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:COMPany
				string value = driver.Sense.Eut.Information.Company;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:NAME
				string value = driver.Sense.Eut.Information.Name;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:VERSion
				RsCmwBluetoothSig_Sense_Eut_Information.Version_Data value = driver.Sense.Eut.Information.Version;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:INFormation:LESignaling
				RsCmwBluetoothSig_Sense_Eut_Information.LeSignaling_Data value = driver.Sense.Eut.Information.LeSignaling;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:CSETtings:LESignaling
				RsCmwBluetoothSig_Sense_Eut_Csettings.LeSignaling_Data value = driver.Sense.Eut.Csettings.LeSignaling;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:PCONtrol:STATe:GFSK
				RsCmwBluetoothSig_Sense_Eut_PowerControl_State.Gfsk_Data value = driver.Sense.Eut.PowerControl.State.Gfsk;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:PCONtrol:STATe:DQPSk
				RsCmwBluetoothSig_Sense_Eut_PowerControl_State.Dqpsk_Data value = driver.Sense.Eut.PowerControl.State.Dqpsk;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:PCONtrol:STATe:DPSK
				RsCmwBluetoothSig_Sense_Eut_PowerControl_State.Dpsk_Data value = driver.Sense.Eut.PowerControl.State.Dpsk;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:EUT:PCONtrol:STATe
				RsCmwBluetoothSig_Sense_Eut_PowerControl_State.Value_Data value = driver.Sense.Eut.PowerControl.State.Value;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:CONNection:AUDio:LINFo
				RsCmwBluetoothSig_Sense_Connection_Audio.Linfo_Data value = driver.Sense.Connection.Audio.Linfo;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:ELOGging:LAST
				RsCmwBluetoothSig_Sense_EventLogging.Last_Data value = driver.Sense.EventLogging.Last;
			}
			{	// SENSe:BLUetooth:SIGNaling<Instance>:ELOGging:ALL
				RsCmwBluetoothSig_Sense_EventLogging.All_Data value = driver.Sense.EventLogging.All;
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:CONNection:STATe
				ConnectionStateEnum value = driver.Connection.State.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:CONNection:STATe:ALL
				RsCmwBluetoothSig_Connection_State_All.Fetch_Data value = driver.Connection.State.All.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:CONNection:STATe:LESignaling
				SignalingStateEnum value = driver.Connection.State.LeSignaling.Fetch();				
			}
			{	// SOURce:BLUetooth:SIGNaling<Instance>:STATe
				bool value = driver.Source.State;
				driver.Source.State = value;
			}
			{	// ROUTe:BLUetooth:SIGNaling<Instance>:SCENario:STATe
				foreach (ConnectionStateEnum x in new ConnectionStateEnum[] { ConnectionStateEnum.A2CNnecting, ConnectionStateEnum.A2Connected, ConnectionStateEnum.A2Detaching, ConnectionStateEnum.A2SCnnected, ConnectionStateEnum.A2SDetaching, ConnectionStateEnum.A2SNnecting, ConnectionStateEnum.ACNNecting, ConnectionStateEnum.ACONected, ConnectionStateEnum.AENMode, ConnectionStateEnum.AEXMode, ConnectionStateEnum.AGCNnecting, ConnectionStateEnum.AGConnected, ConnectionStateEnum.CHASmode, ConnectionStateEnum.CNASmode, ConnectionStateEnum.CNNecting, ConnectionStateEnum.CONNected, ConnectionStateEnum.DETaching, ConnectionStateEnum.DHASmode, ConnectionStateEnum.ECNNecting, ConnectionStateEnum.ECONected, ConnectionStateEnum.ECRunning, ConnectionStateEnum.EHASmode, ConnectionStateEnum.ENAGmode, ConnectionStateEnum.ENEMode, ConnectionStateEnum.ENHFp, ConnectionStateEnum.ENHSmode, ConnectionStateEnum.EXAGmode, ConnectionStateEnum.EXEMode, ConnectionStateEnum.EXHFp, ConnectionStateEnum.EXHSmode, ConnectionStateEnum.HFCNnecting, ConnectionStateEnum.HFConnected, ConnectionStateEnum.HSCNnecting, ConnectionStateEnum.HSConnected, ConnectionStateEnum.HSDetaching, ConnectionStateEnum.INQuiring, ConnectionStateEnum.OFF, ConnectionStateEnum.SBY, ConnectionStateEnum.SCONnecting, ConnectionStateEnum.SINQuiry, ConnectionStateEnum.SMCNnecting, ConnectionStateEnum.SMConnected, ConnectionStateEnum.SMDetaching, ConnectionStateEnum.SMIDle, ConnectionStateEnum.TCNNecting, ConnectionStateEnum.TCONected, ConnectionStateEnum.XHASmode })
				{
					ConnectionStateEnum value = driver.Route.Scenario.State;
				}
			}
			{	// ROUTe:BLUetooth:SIGNaling<Instance>:SCENario:OTRX:FLEXible
				RsCmwBluetoothSig_Route_Scenario_OtRx.Flexible_Data value = driver.Route.Scenario.OtRx.Flexible;
				driver.Route.Scenario.OtRx.Flexible = value;
			}
			{	// ROUTe:BLUetooth:SIGNaling<Instance>:SCENario:OTRX
				RsCmwBluetoothSig_Route_Scenario_OtRx.Value_Data value = driver.Route.Scenario.OtRx.Value;
				driver.Route.Scenario.OtRx.Value = value;
			}
			{	// CALL:BLUetooth:SIGNaling<Instance>:HCICustom:SEND
				driver.Call.HciCustom.Send(new List<string> { "raw1", "raw2", "raw3" });
				driver.Call.HciCustom.Send();
			}
			{	// CALL:BLUetooth:SIGNaling:DTMode:ENDTx
				driver.Call.DtMode.EndTx.Set();
				driver.Call.DtMode.EndTx.SetAndWait();
			}
			{	// CALL:BLUetooth:SIGNaling:DTMode:STARttx
				driver.Call.DtMode.StartTx.Set();
				driver.Call.DtMode.StartTx.SetAndWait();
			}
			{	// CALL:BLUetooth:SIGNaling<Instance>:LENergy:RESet
				driver.Call.LowEnergy.Reset();
				driver.Call.LowEnergy.ResetAndWait();
			}
			{	// CALL:BLUetooth:SIGNaling<instance>:CONNection:ACONnect
				bool value = driver.Call.Connection.Aconnect;
				driver.Call.Connection.Aconnect = value;
			}
			{	// CALL:BLUetooth:SIGNaling<Instance>:CONNection:CHECk:LENergy
				foreach (ConTestResultEnum x in new ConTestResultEnum[] { ConTestResultEnum.FAIL, ConTestResultEnum.NRUN, ConTestResultEnum.PASS, ConTestResultEnum.TOUT })
				{
					ConTestResultEnum value = driver.Call.Connection.Check.LowEnergy;
				}
			}
			{	// CALL:BLUetooth:SIGNaling<Instance>:CONNection:ACTion:LESignaling
				foreach (ConnectionActionLeEnum x in new ConnectionActionLeEnum[] { ConnectionActionLeEnum.CONNect, ConnectionActionLeEnum.DETach, ConnectionActionLeEnum.INQuire, ConnectionActionLeEnum.SCONnecting, ConnectionActionLeEnum.SINQuiry, ConnectionActionLeEnum.TMConnect })
				{
					driver.Call.Connection.Action.LeSignaling = x;					
				}
			}
			{	// CALL:BLUetooth:SIGNaling<Instance>:CONNection:ACTion
				foreach (ConnectActionEnum x in new ConnectActionEnum[] { ConnectActionEnum.ADConnect, ConnectActionEnum.ADENter, ConnectActionEnum.ADEXit, ConnectActionEnum.AGConnect, ConnectActionEnum.AUDConnect, ConnectActionEnum.CONNect, ConnectActionEnum.DETach, ConnectActionEnum.EMConnect, ConnectActionEnum.ENAGate, ConnectActionEnum.ENEMode, ConnectActionEnum.ENHFp, ConnectActionEnum.EXAGate, ConnectActionEnum.EXEMode, ConnectActionEnum.EXHFp, ConnectActionEnum.HFPConnect, ConnectActionEnum.INQuire, ConnectActionEnum.REController, ConnectActionEnum.SCONnecting, ConnectActionEnum.SINQuiry, ConnectActionEnum.STMode, ConnectActionEnum.TMConnect })
				{
					driver.Call.Connection.Action.Value = x;					
				}
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:LENergy:STATe
				LeSignalingStateEnum value = driver.LowEnergy.State.Fetch();				
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange
				driver.RxQuality.IqDrange.Initiate();
				driver.RxQuality.IqDrange.InitiateAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange
				driver.RxQuality.IqDrange.Stop();
				driver.RxQuality.IqDrange.StopAndWait();
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange
				driver.RxQuality.IqDrange.Abort();
				driver.RxQuality.IqDrange.AbortAndWait();
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange
				ResourceStateEnum value = driver.RxQuality.IqDrange.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:STATe
				ResourceStateEnum value = driver.RxQuality.IqDrange.State.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A0Reference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A0Reference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A0Reference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A0Reference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A0Reference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A0Reference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A1Nreference.Read_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A1Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A1Nreference.Fetch_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A1Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A1Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A1Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A2Nreference.Read_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A2Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A2Nreference.Fetch_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A2Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A2Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A2Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A3Nreference.Read_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A3Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A3Nreference.Fetch_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A3Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le1M_A3Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le1M.A3Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A0Reference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A0Reference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A0Reference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A0Reference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A0Reference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A0Reference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A1Nreference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A1Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A1Nreference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A1Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A1Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A1Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A2Nreference.Read_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A2Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A2Nreference.Fetch_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A2Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A2Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A2Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A3Nreference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A3Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A3Nreference.ResultData value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A3Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqDrange_LowEnergy_Le2M_A3Nreference.Calculate_Data value = driver.RxQuality.IqDrange.LowEnergy.Le2M.A3Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le1M.ResultData value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le1M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le1M.ResultData value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le1M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le2M.ResultData value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le2M.ResultData value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQDRange:ANTMeanamp:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_IqDrange_AntMeanAmp_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.IqDrange.AntMeanAmp.LowEnergy.Le2M.Calculate();				
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency
				driver.RxQuality.IqCoherency.Initiate();
				driver.RxQuality.IqCoherency.InitiateAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency
				driver.RxQuality.IqCoherency.Stop();
				driver.RxQuality.IqCoherency.StopAndWait();
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency
				driver.RxQuality.IqCoherency.Abort();
				driver.RxQuality.IqCoherency.AbortAndWait();
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency
				ResourceStateEnum value = driver.RxQuality.IqCoherency.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:STATe
				ResourceStateEnum value = driver.RxQuality.IqCoherency.State.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A0Reference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A0Reference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A0Reference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A0Reference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A0Reference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A0Reference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A1Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A1Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A1Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A1Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A1Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A1Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A2Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A2Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A2Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A2Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A2Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A2Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A3Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A3Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A3Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A3Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE1M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le1M_A3Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le1M.A3Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A0Reference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A0Reference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A0Reference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A0Reference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A0Reference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A0Reference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A0Reference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A1Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A1Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A1Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A1Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A1NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A1Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A1Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A2Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A2Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A2Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A2Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A2NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A2Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A2Nreference.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A3Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A3Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A3Nreference.ResultData value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A3Nreference.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:IQCoherency:LENergy:LE2M:A3NReference
				RsCmwBluetoothSig_RxQuality_IqCoherency_LowEnergy_Le2M_A3Nreference.Calculate_Data value = driver.RxQuality.IqCoherency.LowEnergy.Le2M.A3Nreference.Calculate();				
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER
				driver.RxQuality.Search.Per.Initiate();
				driver.RxQuality.Search.Per.InitiateAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER
				driver.RxQuality.Search.Per.Stop();
				driver.RxQuality.Search.Per.StopAndWait();
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER
				driver.RxQuality.Search.Per.Abort();
				driver.RxQuality.Search.Per.AbortAndWait();
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:STATe
				ResourceStateEnum value = driver.RxQuality.Search.Per.State.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Le1M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Le1M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Search.Per.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Search.Per.LowEnergy.Lrange.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Search.Per.LowEnergy.Le2M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le1M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le1M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Le2M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Nmode_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Search.Per.Nmode.LowEnergy.Lrange.Calculate();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le1M.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le1M.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le1M.Read();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le2M.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le2M.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Le2M.Read();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Lrange.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Lrange.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Search_Per_Tmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Search.Per.Tmode.LowEnergy.Lrange.Read();				
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				driver.RxQuality.Search.Ber.Bedr.Initiate();
				driver.RxQuality.Search.Ber.Bedr.InitiateAndWait();
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				driver.RxQuality.Search.Ber.Bedr.Abort();
				driver.RxQuality.Search.Ber.Bedr.AbortAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				driver.RxQuality.Search.Ber.Bedr.Stop();
				driver.RxQuality.Search.Ber.Bedr.StopAndWait();
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Search_Ber_Bedr.ResultData value = driver.RxQuality.Search.Ber.Bedr.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Search_Ber_Bedr.ResultData value = driver.RxQuality.Search.Ber.Bedr.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Search_Ber_Bedr.Calculate_Data value = driver.RxQuality.Search.Ber.Bedr.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER:STATe:ALL[:BEDR]
				RsCmwBluetoothSig_RxQuality_Search_Ber_State_All_Bedr.Fetch_Data value = driver.RxQuality.Search.Ber.State.All.Bedr.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:SEARch:BER:STATe[:BEDR]
				ResourceStateEnum value = driver.RxQuality.Search.Ber.State.Bedr.Fetch();				
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:PER
				driver.RxQuality.Per.Abort();
				driver.RxQuality.Per.AbortAndWait();
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:PER
				driver.RxQuality.Per.Initiate();
				driver.RxQuality.Per.InitiateAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:PER
				driver.RxQuality.Per.Stop();
				driver.RxQuality.Per.StopAndWait();
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:STATe
				ResourceStateEnum value = driver.RxQuality.Per.State.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.LowEnergy.Le1M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.LowEnergy.Le1M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy[:LE1M]
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Per.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Per.LowEnergy.Lrange.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Per.LowEnergy.Le2M.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Le1M.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Le1M.Read();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Per.Nmode.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Per.Nmode.LowEnergy.Le2M.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.Nmode.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:NMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Nmode_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Per.Nmode.LowEnergy.Lrange.Calculate();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le1M.Calculate_Data value = driver.RxQuality.Per.Tmode.LowEnergy.Le1M.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Le1M.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE1M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le1M.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Le1M.Read();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le2M.Calculate_Data value = driver.RxQuality.Per.Tmode.LowEnergy.Le2M.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Le2M.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LE2M
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Le2M.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Le2M.Read();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Lrange.Calculate_Data value = driver.RxQuality.Per.Tmode.LowEnergy.Lrange.Calculate();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Lrange.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:PER:TMODe:LENergy:LRANge
				RsCmwBluetoothSig_RxQuality_Per_Tmode_LowEnergy_Lrange.ResultData value = driver.RxQuality.Per.Tmode.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:BER:STATe:ALL[:BEDR]
				RsCmwBluetoothSig_RxQuality_Ber_State_All_Bedr.Fetch_Data value = driver.RxQuality.Ber.State.All.Bedr.Fetch();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:BER:STATe[:BEDR]
				ResourceStateEnum value = driver.RxQuality.Ber.State.Bedr.Fetch();				
			}
			{	// ABORt:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				driver.RxQuality.Ber.Bedr.Abort();
				driver.RxQuality.Ber.Bedr.AbortAndWait();
			}
			{	// INITiate:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				driver.RxQuality.Ber.Bedr.Initiate();
				driver.RxQuality.Ber.Bedr.InitiateAndWait();
			}
			{	// STOP:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				driver.RxQuality.Ber.Bedr.Stop();
				driver.RxQuality.Ber.Bedr.StopAndWait();
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Ber_Bedr.ResultData value = driver.RxQuality.Ber.Bedr.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Ber_Bedr.ResultData value = driver.RxQuality.Ber.Bedr.Fetch();				
			}
			{	// CALCulate:BLUetooth:SIGNaling<Instance>:RXQuality:BER[:BEDR]
				RsCmwBluetoothSig_RxQuality_Ber_Bedr.Calculate_Data value = driver.RxQuality.Ber.Bedr.Calculate();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A0Reference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A0Reference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A0Reference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A0Reference.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A1NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A1Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A1NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A1Nreference.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A2NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A2Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A2NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A2Nreference.Fetch();				
			}
			{	// READ:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A3NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A3Nreference.Read();				
			}
			{	// FETCh:BLUetooth:SIGNaling<Instance>:RXQuality:TRACe:IQCoherency:A3NReference
				List<double> value = driver.RxQuality.Trace.IqCoherency.A3Nreference.Fetch();				
			}
			{	// CLEan:BLUetooth:SIGNaling<Instance>:ELOGging
				driver.Clean.EventLogging.Set();
				driver.Clean.EventLogging.SetAndWait();
			}
		}
	}
}