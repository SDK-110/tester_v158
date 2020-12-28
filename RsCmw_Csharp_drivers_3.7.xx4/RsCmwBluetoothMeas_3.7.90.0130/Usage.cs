using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwBluetoothMeas;

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
			RsCmwBluetoothMeas driver = new RsCmwBluetoothMeas("TCPIP::localhost::INSTR", true, true);
			{	// DIAGnostic:BLUetooth:MEASurement<Instance>:RFControl:TXENable
				bool value = driver.Diagnostic.RfControl.TxEnable;
				driver.Diagnostic.RfControl.TxEnable = value;
			}
			{	// DIAGnostic:BLUetooth:SYNChronise
				RsCmwBluetoothMeas_Diagnostic_Bluetooth.Synchronise_Data value = driver.Diagnostic.Bluetooth.Synchronise;
				driver.Diagnostic.Bluetooth.Synchronise = value;
			}
			{	// INITiate:BLUetooth:MEASurement<Instance>:RXQuality
				driver.RxQuality.Initiate();
				driver.RxQuality.InitiateAndWait();
			}
			{	// STOP:BLUetooth:MEASurement<Instance>:RXQuality
				driver.RxQuality.Stop();
				driver.RxQuality.StopAndWait();
			}
			{	// ABORt:BLUetooth:MEASurement<Instance>:RXQuality
				driver.RxQuality.Abort();
				driver.RxQuality.AbortAndWait();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:STATe
				ResourceStateEnum value = driver.RxQuality.State.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:STATe:ALL
				RsCmwBluetoothMeas_RxQuality_State_All.Fetch_Data value = driver.RxQuality.State.All.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:SENSitivity
				double value = driver.RxQuality.Sensitivity.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:SPOTcheck
				ResultEnum value = driver.RxQuality.SpotCheck.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:PER
				double value = driver.RxQuality.Per.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:PER:RXPackets
				int value = driver.RxQuality.Per.RxPackets.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:RXQuality:ADETected:AADDress
				string value = driver.RxQuality.Adetected.Aaddress.Fetch();				
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:GDELay
				double value = driver.Configure.Gdelay;
				driver.Configure.Gdelay = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:CFILter
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.Cfilter = x;
					FilterWidthEnum value = driver.Configure.Cfilter;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:OTHReshold
				double value = driver.Configure.Othreshold;
				driver.Configure.Othreshold = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:DISPlay
				RsCmwBluetoothMeas_Configure.Display_Data value = driver.Configure.Display;
				driver.Configure.Display = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:DOFFset
				int value = driver.Configure.RxQuality.Doffset;
				driver.Configure.RxQuality.Doffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SADDress
				string value = driver.Configure.RxQuality.Saddress;
				driver.Configure.RxQuality.Saddress = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SATYpe
				foreach (AddressTypeEnum x in new AddressTypeEnum[] { AddressTypeEnum.PUBLic, AddressTypeEnum.RANDom })
				{
					driver.Configure.RxQuality.SaType = x;
					AddressTypeEnum value = driver.Configure.RxQuality.SaType;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:ADETect
				bool value = driver.Configure.RxQuality.Adetect;
				driver.Configure.RxQuality.Adetect = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:MMODe
				foreach (RxQualityMeasModeEnum x in new RxQualityMeasModeEnum[] { RxQualityMeasModeEnum.PER, RxQualityMeasModeEnum.SENS, RxQualityMeasModeEnum.SPOT })
				{
					driver.Configure.RxQuality.Mmode = x;
					RxQualityMeasModeEnum value = driver.Configure.RxQuality.Mmode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:GARB
				bool value = driver.Configure.RxQuality.Garb;
				driver.Configure.RxQuality.Garb = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:AINDex
				int value = driver.Configure.RxQuality.Aindex;
				driver.Configure.RxQuality.Aindex = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SENSitivity:STARtlevel
				double value = driver.Configure.RxQuality.Sensitivity.StartLevel;
				driver.Configure.RxQuality.Sensitivity.StartLevel = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SENSitivity:STEPsize
				double value = driver.Configure.RxQuality.Sensitivity.Stepsize;
				driver.Configure.RxQuality.Sensitivity.Stepsize = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SENSitivity:RETRy
				int value = driver.Configure.RxQuality.Sensitivity.Retry;
				driver.Configure.RxQuality.Sensitivity.Retry = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:SPOTcheck:LEVel
				double value = driver.Configure.RxQuality.SpotCheck.Level;
				driver.Configure.RxQuality.SpotCheck.Level = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:PER:LEVel
				double value = driver.Configure.RxQuality.Per.Level;
				driver.Configure.RxQuality.Per.Level = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:PER:TXPackets
				int value = driver.Configure.RxQuality.Per.TxPackets;
				driver.Configure.RxQuality.Per.TxPackets = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:ROUTe
				RsCmwBluetoothMeas_Configure_RxQuality_Route.Value_Data value = driver.Configure.RxQuality.Route.Value;
				driver.Configure.RxQuality.Route.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:ROUTe:USAGe:ALL
				List<bool> value = driver.Configure.RxQuality.Route.Usage.All.Get(TXConnectorBenchEnum.R118);				
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:ROUTe:USAGe:ALL
				driver.Configure.RxQuality.Route.Usage.All.Set(TXConnectorBenchEnum.R118, new List<bool> { true, false, true });
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RXQuality:EATTenuation:OUTPut
				double value = driver.Configure.RxQuality.Eattenuation.Output;
				driver.Configure.RxQuality.Eattenuation.Output = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:TRX:RESult[:ALL]
				RsCmwBluetoothMeas_Configure_Trx_Result.All_Data value = driver.Configure.Trx.Result.All;
				driver.Configure.Trx.Result.All = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Configure.MultiEval.Timeout;
				driver.Configure.MultiEval.Timeout = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:MOEXception
				bool value = driver.Configure.MultiEval.MoException;
				driver.Configure.MultiEval.MoException = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCONdition
				foreach (StopConditionEnum x in new StopConditionEnum[] { StopConditionEnum.NONE, StopConditionEnum.SLFail })
				{
					driver.Configure.MultiEval.Scondition = x;
					StopConditionEnum value = driver.Configure.MultiEval.Scondition;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.MultiEval.Repetition = x;
					RepeatEnum value = driver.Configure.MultiEval.Repetition;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SYNChronise
				RsCmwBluetoothMeas_Configure_MultiEval.Synchronise_Data value = driver.Configure.MultiEval.Synchronise;
				driver.Configure.MultiEval.Synchronise = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:MEASurement:MECount
				int value = driver.Configure.MultiEval.Measurement.MeCount;
				driver.Configure.MultiEval.Measurement.MeCount = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:COUNt
				int value = driver.Configure.MultiEval.List.Count;
				driver.Configure.MultiEval.List.Count = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:MALGorithm
				foreach (PatternIndependentEnum x in new PatternIndependentEnum[] { PatternIndependentEnum.PINDependent, PatternIndependentEnum.SPECconform })
				{
					driver.Configure.MultiEval.List.Malgorithm = x;
					PatternIndependentEnum value = driver.Configure.MultiEval.List.Malgorithm;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST
				bool value = driver.Configure.MultiEval.List.Value;
				driver.Configure.MultiEval.List.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = driver.Configure.MultiEval.List.Segment.Setup.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data value = new RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup.Setup_Data();
				driver.Configure.MultiEval.List.Segment.Setup.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Set(value);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:BTYPe
				BurstTypeEnum value = driver.Configure.MultiEval.List.Segment.Setup.Btype.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Btype.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:BTYPe
				foreach (BurstTypeEnum x in new BurstTypeEnum[] { BurstTypeEnum.BR, BurstTypeEnum.EDR, BurstTypeEnum.LE })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Btype.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Btype.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PTYPe
				SegmentPacketTypeEnum value = driver.Configure.MultiEval.List.Segment.Setup.Ptype.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Ptype.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PTYPe
				foreach (SegmentPacketTypeEnum x in new SegmentPacketTypeEnum[] { SegmentPacketTypeEnum.ADVertiser, SegmentPacketTypeEnum.DH1, SegmentPacketTypeEnum.DH3, SegmentPacketTypeEnum.DH5, SegmentPacketTypeEnum.E21P, SegmentPacketTypeEnum.E23P, SegmentPacketTypeEnum.E25P, SegmentPacketTypeEnum.E31P, SegmentPacketTypeEnum.E33P, SegmentPacketTypeEnum.E35P, SegmentPacketTypeEnum.RFPHytest })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Ptype.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Ptype.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PATTern
				MevPatternTypeEnum value = driver.Configure.MultiEval.List.Segment.Setup.Pattern.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Pattern.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PATTern
				foreach (MevPatternTypeEnum x in new MevPatternTypeEnum[] { MevPatternTypeEnum.ALL1, MevPatternTypeEnum.ALTernating, MevPatternTypeEnum.OTHer, MevPatternTypeEnum.P11, MevPatternTypeEnum.P44 })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Pattern.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Pattern.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PLENgth
				int value = driver.Configure.MultiEval.List.Segment.Setup.Plength.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Plength.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PLENgth
				driver.Configure.MultiEval.List.Segment.Setup.Plength.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Plength.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:OSLots
				int value = driver.Configure.MultiEval.List.Segment.Setup.Oslots.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Oslots.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:OSLots
				driver.Configure.MultiEval.List.Segment.Setup.Oslots.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Oslots.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:SLENgth
				int value = driver.Configure.MultiEval.List.Segment.Setup.Slength.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Slength.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:SLENgth
				driver.Configure.MultiEval.List.Segment.Setup.Slength.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Slength.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:MOEXception
				bool value = driver.Configure.MultiEval.List.Segment.Setup.MoException.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.MoException.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:MOEXception
				driver.Configure.MultiEval.List.Segment.Setup.MoException.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.MoException.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:ENPower
				double value = driver.Configure.MultiEval.List.Segment.Setup.EnvelopePower.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.EnvelopePower.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:ENPower
				driver.Configure.MultiEval.List.Segment.Setup.EnvelopePower.Set(1.0, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.EnvelopePower.Set(1.0);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:FREQuency
				double value = driver.Configure.MultiEval.List.Segment.Setup.Frequency.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Frequency.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:FREQuency
				driver.Configure.MultiEval.List.Segment.Setup.Frequency.Set(1.0, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Frequency.Set(1.0);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:FILTer
				FilterWidthEnum value = driver.Configure.MultiEval.List.Segment.Setup.Filter.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Filter.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:FILTer
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Filter.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Filter.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:RTRigger
				bool value = driver.Configure.MultiEval.List.Segment.Setup.Rtrigger.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Rtrigger.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:RTRigger
				driver.Configure.MultiEval.List.Segment.Setup.Rtrigger.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Rtrigger.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:CMWS:CONNector
				CmwSingleConnectorEnum value = driver.Configure.MultiEval.List.Segment.Setup.SingleCmw.Connector.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.SingleCmw.Connector.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:CMWS:CONNector
				foreach (CmwSingleConnectorEnum x in new CmwSingleConnectorEnum[] { CmwSingleConnectorEnum.R11, CmwSingleConnectorEnum.R12, CmwSingleConnectorEnum.R13, CmwSingleConnectorEnum.R14, CmwSingleConnectorEnum.R15, CmwSingleConnectorEnum.R16, CmwSingleConnectorEnum.R17, CmwSingleConnectorEnum.R18, CmwSingleConnectorEnum.R21, CmwSingleConnectorEnum.R22, CmwSingleConnectorEnum.R23, CmwSingleConnectorEnum.R24, CmwSingleConnectorEnum.R25, CmwSingleConnectorEnum.R26, CmwSingleConnectorEnum.R27, CmwSingleConnectorEnum.R28, CmwSingleConnectorEnum.R31, CmwSingleConnectorEnum.R32, CmwSingleConnectorEnum.R33, CmwSingleConnectorEnum.R34, CmwSingleConnectorEnum.R35, CmwSingleConnectorEnum.R36, CmwSingleConnectorEnum.R37, CmwSingleConnectorEnum.R38, CmwSingleConnectorEnum.R41, CmwSingleConnectorEnum.R42, CmwSingleConnectorEnum.R43, CmwSingleConnectorEnum.R44, CmwSingleConnectorEnum.R45, CmwSingleConnectorEnum.R46, CmwSingleConnectorEnum.R47, CmwSingleConnectorEnum.R48, CmwSingleConnectorEnum.RA1, CmwSingleConnectorEnum.RA2, CmwSingleConnectorEnum.RA3, CmwSingleConnectorEnum.RA4, CmwSingleConnectorEnum.RA5, CmwSingleConnectorEnum.RA6, CmwSingleConnectorEnum.RA7, CmwSingleConnectorEnum.RA8, CmwSingleConnectorEnum.RB1, CmwSingleConnectorEnum.RB2, CmwSingleConnectorEnum.RB3, CmwSingleConnectorEnum.RB4, CmwSingleConnectorEnum.RB5, CmwSingleConnectorEnum.RB6, CmwSingleConnectorEnum.RB7, CmwSingleConnectorEnum.RB8 })
				{
					driver.Configure.MultiEval.List.Segment.Setup.SingleCmw.Connector.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.SingleCmw.Connector.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PHY
				LePhysicalTypeEnum value = driver.Configure.MultiEval.List.Segment.Setup.Phy.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Phy.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:PHY
				foreach (LePhysicalTypeEnum x in new LePhysicalTypeEnum[] { LePhysicalTypeEnum.LE1M, LePhysicalTypeEnum.LE2M, LePhysicalTypeEnum.LELR })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Phy.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Phy.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:CSCHeme
				CodingSchemeEnum value = driver.Configure.MultiEval.List.Segment.Setup.Cscheme.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Cscheme.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:CSCHeme
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.MultiEval.List.Segment.Setup.Cscheme.Set(x);
					driver.Configure.MultiEval.List.Segment.Setup.Cscheme.Set(x, SegmentRepCap.Default);
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:EXTended
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup_Extended.Extended_Data value = driver.Configure.MultiEval.List.Segment.Setup.Extended.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Setup.Extended.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>[:SETup]:EXTended
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup_Extended.Extended_Data value = new RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Setup_Extended.Extended_Data();
				driver.Configure.MultiEval.List.Segment.Setup.Extended.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Setup.Extended.Set(value);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data value = driver.Configure.MultiEval.List.Segment.Scount.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data value = new RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Scount.Scount_Data();
				driver.Configure.MultiEval.List.Segment.Scount.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Set(value);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:MSCalar
				int value = driver.Configure.MultiEval.List.Segment.Scount.Mscalar.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Mscalar.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:MSCalar
				driver.Configure.MultiEval.List.Segment.Scount.Mscalar.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Mscalar.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:PENCoding
				int value = driver.Configure.MultiEval.List.Segment.Scount.Pencoding.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Pencoding.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:PENCoding
				driver.Configure.MultiEval.List.Segment.Scount.Pencoding.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Pencoding.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:PSCalar
				int value = driver.Configure.MultiEval.List.Segment.Scount.Pscalar.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Pscalar.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:PSCalar
				driver.Configure.MultiEval.List.Segment.Scount.Pscalar.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Pscalar.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SOBW
				int value = driver.Configure.MultiEval.List.Segment.Scount.SoBw.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.SoBw.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SOBW
				driver.Configure.MultiEval.List.Segment.Scount.SoBw.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.SoBw.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SACP
				int value = driver.Configure.MultiEval.List.Segment.Scount.Sacp.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Sacp.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SACP
				driver.Configure.MultiEval.List.Segment.Scount.Sacp.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Sacp.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SGACp
				int value = driver.Configure.MultiEval.List.Segment.Scount.Sgacp.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Scount.Sgacp.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SCOunt:SGACp
				driver.Configure.MultiEval.List.Segment.Scount.Sgacp.Set(1, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Scount.Sgacp.Set(1);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Results.Results_Data value = driver.Configure.MultiEval.List.Segment.Results.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults
				RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Results.Results_Data value = new RsCmwBluetoothMeas_Configure_MultiEval_List_Segment_Results.Results_Data();
				driver.Configure.MultiEval.List.Segment.Results.Set(value, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Set(value);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:MSCalar
				bool value = driver.Configure.MultiEval.List.Segment.Results.Mscalar.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Mscalar.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:MSCalar
				driver.Configure.MultiEval.List.Segment.Results.Mscalar.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Mscalar.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:PENCoding
				bool value = driver.Configure.MultiEval.List.Segment.Results.Pencoding.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Pencoding.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:PENCoding
				driver.Configure.MultiEval.List.Segment.Results.Pencoding.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Pencoding.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:PSCalar
				bool value = driver.Configure.MultiEval.List.Segment.Results.Pscalar.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Pscalar.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:PSCalar
				driver.Configure.MultiEval.List.Segment.Results.Pscalar.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Pscalar.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SOBW
				bool value = driver.Configure.MultiEval.List.Segment.Results.SoBw.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.SoBw.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SOBW
				driver.Configure.MultiEval.List.Segment.Results.SoBw.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.SoBw.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SACP
				bool value = driver.Configure.MultiEval.List.Segment.Results.Sacp.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Sacp.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SACP
				driver.Configure.MultiEval.List.Segment.Results.Sacp.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Sacp.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SGACp
				bool value = driver.Configure.MultiEval.List.Segment.Results.Sgacp.Get(SegmentRepCap.Default);
				value = driver.Configure.MultiEval.List.Segment.Results.Sgacp.Get();
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:RESults:SGACp
				driver.Configure.MultiEval.List.Segment.Results.Sgacp.Set(false, SegmentRepCap.Default);
				driver.Configure.MultiEval.List.Segment.Results.Sgacp.Set(false);
			}
			{	// CONFigure:BLUetooth:MEASurement<instance>:MEValuation:LIST:CMWS:CMODe
				foreach (ParameterSetModeEnum x in new ParameterSetModeEnum[] { ParameterSetModeEnum.GLOBal, ParameterSetModeEnum.LIST })
				{
					driver.Configure.MultiEval.List.SingleCmw.Cmode = x;
					ParameterSetModeEnum value = driver.Configure.MultiEval.List.SingleCmw.Cmode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:MALGorithm:LENergy
				foreach (PatternIndependentEnum x in new PatternIndependentEnum[] { PatternIndependentEnum.PINDependent, PatternIndependentEnum.SPECconform })
				{
					driver.Configure.MultiEval.Malgorithm.LowEnergy = x;
					PatternIndependentEnum value = driver.Configure.MultiEval.Malgorithm.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:MALGorithm:BRATe
				foreach (PatternIndependentEnum x in new PatternIndependentEnum[] { PatternIndependentEnum.PINDependent, PatternIndependentEnum.SPECconform })
				{
					driver.Configure.MultiEval.Malgorithm.Brate = x;
					PatternIndependentEnum value = driver.Configure.MultiEval.Malgorithm.Brate;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:FRANge
				RsCmwBluetoothMeas_Configure_MultiEval_Limit.Frange_Data value = driver.Configure.MultiEval.Limit.Frange;
				driver.Configure.MultiEval.Limit.Frange = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:SACP
				RsCmwBluetoothMeas_Configure_MultiEval_Limit.Sacp_Data value = driver.Configure.MultiEval.Limit.Sacp;
				driver.Configure.MultiEval.Limit.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:SOBW
				RsCmwBluetoothMeas_Configure_MultiEval_Limit.SoBw_Data value = driver.Configure.MultiEval.Limit.SoBw;
				driver.Configure.MultiEval.Limit.SoBw = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:SGACp
				RsCmwBluetoothMeas_Configure_MultiEval_Limit.Sgacp_Data value = driver.Configure.MultiEval.Limit.Sgacp;
				driver.Configure.MultiEval.Limit.Sgacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.PowerVsTime;
				driver.Configure.MultiEval.Limit.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DELTa
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy.Delta_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Delta;
				driver.Configure.MultiEval.Limit.LowEnergy.Delta = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:SACP
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Sacp_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Sacp;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.PowerVsTime;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:FACCuracy
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Faccuracy_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Faccuracy;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Faccuracy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:FOFFset
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.FreqOffset_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.FreqOffset;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:DELTa
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Delta_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Delta;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Delta = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Fdrift_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Fdrift;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Fdrift = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:DAVerage
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Daverage_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Daverage;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Daverage = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:DMINimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Dminimum_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Dminimum;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Dminimum = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LRANge:DMAXimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Lrange.Dmaximum_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Dmaximum;
				driver.Configure.MultiEval.Limit.LowEnergy.Lrange.Dmaximum = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:SACP
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.Sacp_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Sacp;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.PowerVsTime;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:FACCuracy
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.Faccuracy_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Faccuracy;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Faccuracy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:FOFFset
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.FreqOffset_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.FreqOffset;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:MRATio
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.Mratio_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Mratio;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Mratio = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DELTa
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.Delta_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Delta;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Delta = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M.Fdrift_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Fdrift;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Fdrift = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DAVerage:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Daverage.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Daverage.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Daverage.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DAVerage
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Daverage.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Daverage.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Daverage.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DMINimum:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Dminimum.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dminimum.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dminimum.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DMINimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Dminimum.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dminimum.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dminimum.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DMAXimum:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Dmaximum.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dmaximum.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dmaximum.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:LE2M:DMAXimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le2M_Dmaximum.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dmaximum.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Le2M.Dmaximum.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:SACP
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.Sacp_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Sacp;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.PowerVsTime;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:FACCuracy
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.Faccuracy_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Faccuracy;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Faccuracy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:FOFFset
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.FreqOffset_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.FreqOffset;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:MRATio
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.Mratio_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Mratio;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Mratio = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy[:LE1M]:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Le1M.Fdrift_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Fdrift;
				driver.Configure.MultiEval.Limit.LowEnergy.Le1M.Fdrift = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DAVerage:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Daverage.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Daverage.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Daverage.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DAVerage
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Daverage.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Daverage.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Daverage.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DMINimum:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Dminimum.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Dminimum.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Dminimum.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DMINimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Dminimum.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Dminimum.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Dminimum.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DMAXimum:DF2S
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Dmaximum.Df2S_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Dmaximum.Df2s;
				driver.Configure.MultiEval.Limit.LowEnergy.Dmaximum.Df2s = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:LENergy:DMAXimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_LowEnergy_Dmaximum.Value_Data value = driver.Configure.MultiEval.Limit.LowEnergy.Dmaximum.Value;
				driver.Configure.MultiEval.Limit.LowEnergy.Dmaximum.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE1M:PDEViation
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le1M.Pdeviation_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.Pdeviation;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.Pdeviation = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE1M:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le1M.Fdrift_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.Fdrift;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.Fdrift = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE1M:FOFFset
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le1M.FreqOffset_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.FreqOffset;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le1M.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE2M:PDEViation
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le2M.Pdeviation_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.Pdeviation;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.Pdeviation = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE2M:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le2M.Fdrift_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.Fdrift;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.Fdrift = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:CTE:LENergy:LE2M:FOFFset
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Cte_LowEnergy_Le2M.FreqOffset_Data value = driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.FreqOffset;
				driver.Configure.MultiEval.Limit.Cte.LowEnergy.Le2M.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.Edrate.PowerVsTime;
				driver.Configure.MultiEval.Limit.Edrate.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:FSTability
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate.Fstability_Data value = driver.Configure.MultiEval.Limit.Edrate.Fstability;
				driver.Configure.MultiEval.Limit.Edrate.Fstability = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:PENCoding:SSEQuence
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate_Pencoding.Ssequence_Data value = driver.Configure.MultiEval.Limit.Edrate.Pencoding.Ssequence;
				driver.Configure.MultiEval.Limit.Edrate.Pencoding.Ssequence = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:PENCoding
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate_Pencoding.Value_Data value = driver.Configure.MultiEval.Limit.Edrate.Pencoding.Value;
				driver.Configure.MultiEval.Limit.Edrate.Pencoding.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:DPSK:DEVM
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate_Dpsk.Devm_Data value = driver.Configure.MultiEval.Limit.Edrate.Dpsk.Devm;
				driver.Configure.MultiEval.Limit.Edrate.Dpsk.Devm = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:EDRate:DQPSk:DEVM
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Edrate_Dqpsk.Devm_Data value = driver.Configure.MultiEval.Limit.Edrate.Dqpsk.Devm;
				driver.Configure.MultiEval.Limit.Edrate.Dqpsk.Devm = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:PVTime
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.PowerVsTime_Data value = driver.Configure.MultiEval.Limit.Brate.PowerVsTime;
				driver.Configure.MultiEval.Limit.Brate.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:MRATio
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Mratio_Data value = driver.Configure.MultiEval.Limit.Brate.Mratio;
				driver.Configure.MultiEval.Limit.Brate.Mratio = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:DELTa
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Delta_Data value = driver.Configure.MultiEval.Limit.Brate.Delta;
				driver.Configure.MultiEval.Limit.Brate.Delta = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:DAVerage
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Daverage_Data value = driver.Configure.MultiEval.Limit.Brate.Daverage;
				driver.Configure.MultiEval.Limit.Brate.Daverage = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:DMINimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Dminimum_Data value = driver.Configure.MultiEval.Limit.Brate.Dminimum;
				driver.Configure.MultiEval.Limit.Brate.Dminimum = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:DMAXimum
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Dmaximum_Data value = driver.Configure.MultiEval.Limit.Brate.Dmaximum;
				driver.Configure.MultiEval.Limit.Brate.Dmaximum = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:FACCuracy
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate.Faccuracy_Data value = driver.Configure.MultiEval.Limit.Brate.Faccuracy;
				driver.Configure.MultiEval.Limit.Brate.Faccuracy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:FDRift:APACkets
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate_Fdrift.Apackets_Data value = driver.Configure.MultiEval.Limit.Brate.Fdrift.Apackets;
				driver.Configure.MultiEval.Limit.Brate.Fdrift.Apackets = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LIMit:BRATe:FDRift
				RsCmwBluetoothMeas_Configure_MultiEval_Limit_Brate_Fdrift.Value_Data value = driver.Configure.MultiEval.Limit.Brate.Fdrift.Value;
				driver.Configure.MultiEval.Limit.Brate.Fdrift.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LENergy:LRANge:FILTer:BWIDth
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.LowEnergy.Lrange.Filter.Bandwidth = x;
					FilterWidthEnum value = driver.Configure.MultiEval.LowEnergy.Lrange.Filter.Bandwidth;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LENergy:LE2M:FILTer:BWIDth
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.LowEnergy.Le2M.Filter.Bandwidth = x;
					FilterWidthEnum value = driver.Configure.MultiEval.LowEnergy.Le2M.Filter.Bandwidth;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:LENergy[:LE1M]:FILTer:BWIDth
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.LowEnergy.Le1M.Filter.Bandwidth = x;
					FilterWidthEnum value = driver.Configure.MultiEval.LowEnergy.Le1M.Filter.Bandwidth;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:EDRate:FILTer:BWIDth
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.Edrate.Filter.Bandwidth = x;
					FilterWidthEnum value = driver.Configure.MultiEval.Edrate.Filter.Bandwidth;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:BRATe:FILTer:BWIDth
				foreach (FilterWidthEnum x in new FilterWidthEnum[] { FilterWidthEnum.NARRow, FilterWidthEnum.WIDE })
				{
					driver.Configure.MultiEval.Brate.Filter.Bandwidth = x;
					FilterWidthEnum value = driver.Configure.MultiEval.Brate.Filter.Bandwidth;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:PENCoding
				int value = driver.Configure.MultiEval.Scount.Pencoding;
				driver.Configure.MultiEval.Scount.Pencoding = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:FRANge
				int value = driver.Configure.MultiEval.Scount.Frange;
				driver.Configure.MultiEval.Scount.Frange = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:SGACp
				int value = driver.Configure.MultiEval.Scount.Sgacp;
				driver.Configure.MultiEval.Scount.Sgacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:SOBW
				int value = driver.Configure.MultiEval.Scount.SoBw;
				driver.Configure.MultiEval.Scount.SoBw = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:SACP
				int value = driver.Configure.MultiEval.Scount.Sacp;
				driver.Configure.MultiEval.Scount.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:PVTime
				int value = driver.Configure.MultiEval.Scount.PowerVsTime;
				driver.Configure.MultiEval.Scount.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SCOunt:MODulation
				int value = driver.Configure.MultiEval.Scount.Modulation;
				driver.Configure.MultiEval.Scount.Modulation = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:SPOWer
				bool value = driver.Configure.MultiEval.Result.Spower;
				driver.Configure.MultiEval.Result.Spower = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:PENCoding
				bool value = driver.Configure.MultiEval.Result.Pencoding;
				driver.Configure.MultiEval.Result.Pencoding = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:FRANge
				bool value = driver.Configure.MultiEval.Result.Frange;
				driver.Configure.MultiEval.Result.Frange = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:SGACp
				bool value = driver.Configure.MultiEval.Result.Sgacp;
				driver.Configure.MultiEval.Result.Sgacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:SOBW
				bool value = driver.Configure.MultiEval.Result.SoBw;
				driver.Configure.MultiEval.Result.SoBw = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:SACP
				bool value = driver.Configure.MultiEval.Result.Sacp;
				driver.Configure.MultiEval.Result.Sacp = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult[:ALL]
				RsCmwBluetoothMeas_Configure_MultiEval_Result.All_Data value = driver.Configure.MultiEval.Result.All;
				driver.Configure.MultiEval.Result.All = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:PSCalar
				bool value = driver.Configure.MultiEval.Result.Pscalar;
				driver.Configure.MultiEval.Result.Pscalar = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:IQABsolute
				bool value = driver.Configure.MultiEval.Result.IqAbsolute;
				driver.Configure.MultiEval.Result.IqAbsolute = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:IQERror
				bool value = driver.Configure.MultiEval.Result.IqError;
				driver.Configure.MultiEval.Result.IqError = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:IQDiff
				bool value = driver.Configure.MultiEval.Result.IQdifference;
				driver.Configure.MultiEval.Result.IQdifference = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:PVTime
				bool value = driver.Configure.MultiEval.Result.PowerVsTime;
				driver.Configure.MultiEval.Result.PowerVsTime = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:DEVMagnitude
				bool value = driver.Configure.MultiEval.Result.DevMagnitude;
				driver.Configure.MultiEval.Result.DevMagnitude = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:PDIFference
				bool value = driver.Configure.MultiEval.Result.Pdifference;
				driver.Configure.MultiEval.Result.Pdifference = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:MSCalar
				bool value = driver.Configure.MultiEval.Result.Mscalar;
				driver.Configure.MultiEval.Result.Mscalar = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:FDEViation
				bool value = driver.Configure.MultiEval.Result.Fdeviation;
				driver.Configure.MultiEval.Result.Fdeviation = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:RESult:PVSLot
				bool value = driver.Configure.MultiEval.Result.PvSlot;
				driver.Configure.MultiEval.Result.PvSlot = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LE2M:MEASurement:MODE
				foreach (LeChannelsRangeEnum x in new LeChannelsRangeEnum[] { LeChannelsRangeEnum.CH10, LeChannelsRangeEnum.CH40 })
				{
					driver.Configure.MultiEval.Sacp.LowEnergy.Le2M.Measurement.Mode = x;
					LeChannelsRangeEnum value = driver.Configure.MultiEval.Sacp.LowEnergy.Le2M.Measurement.Mode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy[:LE1M]:MEASurement:MODE
				foreach (LeChannelsRangeEnum x in new LeChannelsRangeEnum[] { LeChannelsRangeEnum.CH10, LeChannelsRangeEnum.CH40 })
				{
					driver.Configure.MultiEval.Sacp.LowEnergy.Le1M.Measurement.Mode = x;
					LeChannelsRangeEnum value = driver.Configure.MultiEval.Sacp.LowEnergy.Le1M.Measurement.Mode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SACP:BRATe:MEASurement:MODE
				foreach (BrEdrChannelsRangeEnum x in new BrEdrChannelsRangeEnum[] { BrEdrChannelsRangeEnum.CH21, BrEdrChannelsRangeEnum.CH79 })
				{
					driver.Configure.MultiEval.Sacp.Brate.Measurement.Mode = x;
					BrEdrChannelsRangeEnum value = driver.Configure.MultiEval.Sacp.Brate.Measurement.Mode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:FRANge:BRATe:MEASurement
				RsCmwBluetoothMeas_Configure_MultiEval_Frange_Brate.Measurement_Data value = driver.Configure.MultiEval.Frange.Brate.Measurement;
				driver.Configure.MultiEval.Frange.Brate.Measurement = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:MEValuation:SGACp:EDRate:MEASurement:MODE
				foreach (BrEdrChannelsRangeEnum x in new BrEdrChannelsRangeEnum[] { BrEdrChannelsRangeEnum.CH21, BrEdrChannelsRangeEnum.CH79 })
				{
					driver.Configure.MultiEval.Sgacp.Edrate.Measurement.Mode = x;
					BrEdrChannelsRangeEnum value = driver.Configure.MultiEval.Sgacp.Edrate.Measurement.Mode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:EATTenuation
				double value = driver.Configure.RfSettings.Eattenuation;
				driver.Configure.RfSettings.Eattenuation = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:UMARgin
				double value = driver.Configure.RfSettings.Umargin;
				driver.Configure.RfSettings.Umargin = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:ENPower
				double value = driver.Configure.RfSettings.EnvelopePower;
				driver.Configure.RfSettings.EnvelopePower = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:FREQuency
				double value = driver.Configure.RfSettings.Frequency;
				driver.Configure.RfSettings.Frequency = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:DTX:STERror
				foreach (LeSymolTimeErrorEnum x in new LeSymolTimeErrorEnum[] { LeSymolTimeErrorEnum.NEG50, LeSymolTimeErrorEnum.OFF, LeSymolTimeErrorEnum.POS50 })
				{
					driver.Configure.RfSettings.Dtx.StError = x;
					LeSymolTimeErrorEnum value = driver.Configure.RfSettings.Dtx.StError;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:DTX:FOFFset
				double value = driver.Configure.RfSettings.Dtx.FreqOffset;
				driver.Configure.RfSettings.Dtx.FreqOffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:DTX:MINDex
				double value = driver.Configure.RfSettings.Dtx.Mindex;
				driver.Configure.RfSettings.Dtx.Mindex = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:DTX
				bool value = driver.Configure.RfSettings.Dtx.Value;
				driver.Configure.RfSettings.Dtx.Value = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:CTE:LENergy:NANTenna
				int value = driver.Configure.RfSettings.Cte.LowEnergy.Nantenna;
				driver.Configure.RfSettings.Cte.LowEnergy.Nantenna = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:CTE:LENergy:AOFFset
				RsCmwBluetoothMeas_Configure_RfSettings_Cte_LowEnergy.Aoffset_Data value = driver.Configure.RfSettings.Cte.LowEnergy.Aoffset;
				driver.Configure.RfSettings.Cte.LowEnergy.Aoffset = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:CTE:LENergy:ROFFset
				double value = driver.Configure.RfSettings.Cte.LowEnergy.Roffset;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:MMODe
				foreach (MeasureScopeEnum x in new MeasureScopeEnum[] { MeasureScopeEnum.ALL, MeasureScopeEnum.SINGle })
				{
					driver.Configure.RfSettings.Mmode.Value = x;
					MeasureScopeEnum value = driver.Configure.RfSettings.Mmode.Value;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:MMODe:NMODe:LENergy
				foreach (MeasureScopeEnum x in new MeasureScopeEnum[] { MeasureScopeEnum.ALL, MeasureScopeEnum.SINGle })
				{
					driver.Configure.RfSettings.Mmode.Nmode.LowEnergy = x;
					MeasureScopeEnum value = driver.Configure.RfSettings.Mmode.Nmode.LowEnergy;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:MCHannel[:CLASsic]
				int value = driver.Configure.RfSettings.Mchannel.Classic;
				driver.Configure.RfSettings.Mchannel.Classic = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:RFSettings:MCHannel:LENergy
				int value = driver.Configure.RfSettings.Mchannel.LowEnergy;
				driver.Configure.RfSettings.Mchannel.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:DMODe
				foreach (AutoManualModeEnum x in new AutoManualModeEnum[] { AutoManualModeEnum.AUTO, AutoManualModeEnum.MANual })
				{
					driver.Configure.InputSignal.Dmode = x;
					AutoManualModeEnum value = driver.Configure.InputSignal.Dmode;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:BTYPe
				foreach (BurstTypeEnum x in new BurstTypeEnum[] { BurstTypeEnum.BR, BurstTypeEnum.EDR, BurstTypeEnum.LE })
				{
					driver.Configure.InputSignal.Btype = x;
					BurstTypeEnum value = driver.Configure.InputSignal.Btype;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:NAP
				string value = driver.Configure.InputSignal.Nap;
				driver.Configure.InputSignal.Nap = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:UAP
				string value = driver.Configure.InputSignal.Uap;
				driver.Configure.InputSignal.Uap = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:LAP
				string value = driver.Configure.InputSignal.Lap;
				driver.Configure.InputSignal.Lap = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:BDADdress
				string value = driver.Configure.InputSignal.BdAddress;
				driver.Configure.InputSignal.BdAddress = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:ASYNchronize
				bool value = driver.Configure.InputSignal.Asynchronize;
				driver.Configure.InputSignal.Asynchronize = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:CTE:LENergy:LE1M:TYPE
				foreach (CtePacketTypeEnum x in new CtePacketTypeEnum[] { CtePacketTypeEnum.AOA1us, CtePacketTypeEnum.AOA2us, CtePacketTypeEnum.AOAus, CtePacketTypeEnum.AOD1us, CtePacketTypeEnum.AOD2us })
				{
					driver.Configure.InputSignal.Cte.LowEnergy.Le1M.Type = x;
					CtePacketTypeEnum value = driver.Configure.InputSignal.Cte.LowEnergy.Le1M.Type;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:CTE:LENergy:LE1M:UNITs
				int value = driver.Configure.InputSignal.Cte.LowEnergy.Le1M.Units;
				driver.Configure.InputSignal.Cte.LowEnergy.Le1M.Units = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:CTE:LENergy:LE2M:TYPE
				foreach (CtePacketTypeEnum x in new CtePacketTypeEnum[] { CtePacketTypeEnum.AOA1us, CtePacketTypeEnum.AOA2us, CtePacketTypeEnum.AOAus, CtePacketTypeEnum.AOD1us, CtePacketTypeEnum.AOD2us })
				{
					driver.Configure.InputSignal.Cte.LowEnergy.Le2M.Type = x;
					CtePacketTypeEnum value = driver.Configure.InputSignal.Cte.LowEnergy.Le2M.Type;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:CTE:LENergy:LE2M:UNITs
				int value = driver.Configure.InputSignal.Cte.LowEnergy.Le2M.Units;
				driver.Configure.InputSignal.Cte.LowEnergy.Le2M.Units = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:OSLots:EDRate
				List<int> value = driver.Configure.InputSignal.Oslots.Edrate;
				driver.Configure.InputSignal.Oslots.Edrate = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:OSLots:BRATe
				List<int> value = driver.Configure.InputSignal.Oslots.Brate;
				driver.Configure.InputSignal.Oslots.Brate = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:OSLots:LENergy[:LE1M]
				int value = driver.Configure.InputSignal.Oslots.LowEnergy.Le1m;
				driver.Configure.InputSignal.Oslots.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:OSLots:LENergy:LRANge
				int value = driver.Configure.InputSignal.Oslots.LowEnergy.Lrange;
				driver.Configure.InputSignal.Oslots.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:OSLots:LENergy:LE2M
				int value = driver.Configure.InputSignal.Oslots.LowEnergy.Le2m;
				driver.Configure.InputSignal.Oslots.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:BRATe
				List<int> value = driver.Configure.InputSignal.Plength.Brate;
				driver.Configure.InputSignal.Plength.Brate = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:EDRate
				List<int> value = driver.Configure.InputSignal.Plength.Edrate;
				driver.Configure.InputSignal.Plength.Edrate = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:LENergy[:LE1M]
				int value = driver.Configure.InputSignal.Plength.LowEnergy.Le1m;
				driver.Configure.InputSignal.Plength.LowEnergy.Le1m = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:LENergy:LRANge
				int value = driver.Configure.InputSignal.Plength.LowEnergy.Lrange;
				driver.Configure.InputSignal.Plength.LowEnergy.Lrange = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PLENgth:LENergy:LE2M
				int value = driver.Configure.InputSignal.Plength.LowEnergy.Le2m;
				driver.Configure.InputSignal.Plength.LowEnergy.Le2m = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PTYPe:EDRate
				foreach (EdrPacketTypeEnum x in new EdrPacketTypeEnum[] { EdrPacketTypeEnum.E21P, EdrPacketTypeEnum.E23P, EdrPacketTypeEnum.E25P, EdrPacketTypeEnum.E31P, EdrPacketTypeEnum.E33P, EdrPacketTypeEnum.E35P })
				{
					driver.Configure.InputSignal.Ptype.Edrate = x;
					EdrPacketTypeEnum value = driver.Configure.InputSignal.Ptype.Edrate;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PTYPe:BRATe
				foreach (BrPacketTypeEnum x in new BrPacketTypeEnum[] { BrPacketTypeEnum.DH1, BrPacketTypeEnum.DH3, BrPacketTypeEnum.DH5 })
				{
					driver.Configure.InputSignal.Ptype.Brate = x;
					BrPacketTypeEnum value = driver.Configure.InputSignal.Ptype.Brate;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PTYPe:LENergy[:LE1M]
				foreach (LePacketTypeEnum x in new LePacketTypeEnum[] { LePacketTypeEnum.ADVertiser, LePacketTypeEnum.RFCTe, LePacketTypeEnum.RFPHytest })
				{
					driver.Configure.InputSignal.Ptype.LowEnergy.Le1m = x;
					LePacketTypeEnum value = driver.Configure.InputSignal.Ptype.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PTYPe:LENergy:LRANge
				foreach (LePacketTypeEnum x in new LePacketTypeEnum[] { LePacketTypeEnum.ADVertiser, LePacketTypeEnum.RFCTe, LePacketTypeEnum.RFPHytest })
				{
					driver.Configure.InputSignal.Ptype.LowEnergy.Lrange = x;
					LePacketTypeEnum value = driver.Configure.InputSignal.Ptype.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PTYPe:LENergy:LE2M
				foreach (LePacketTypeEnum x in new LePacketTypeEnum[] { LePacketTypeEnum.ADVertiser, LePacketTypeEnum.RFCTe, LePacketTypeEnum.RFPHytest })
				{
					driver.Configure.InputSignal.Ptype.LowEnergy.Le2m = x;
					LePacketTypeEnum value = driver.Configure.InputSignal.Ptype.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:LENergy:SYNWord
				string value = driver.Configure.InputSignal.LowEnergy.SynWord;
				driver.Configure.InputSignal.LowEnergy.SynWord = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:LENergy:PHY
				foreach (LePhysicalTypeEnum x in new LePhysicalTypeEnum[] { LePhysicalTypeEnum.LE1M, LePhysicalTypeEnum.LE2M, LePhysicalTypeEnum.LELR })
				{
					driver.Configure.InputSignal.LowEnergy.Phy = x;
					LePhysicalTypeEnum value = driver.Configure.InputSignal.LowEnergy.Phy;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:ACCaddress:LENergy
				string value = driver.Configure.InputSignal.AccAddress.LowEnergy;
				driver.Configure.InputSignal.AccAddress.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:SYNWord:LENergy
				string value = driver.Configure.InputSignal.SynWord.LowEnergy;
				driver.Configure.InputSignal.SynWord.LowEnergy = value;
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PATTern
				foreach (DetectedPatternTypeEnum x in new DetectedPatternTypeEnum[] { DetectedPatternTypeEnum.ALTernating, DetectedPatternTypeEnum.OTHer, DetectedPatternTypeEnum.P11, DetectedPatternTypeEnum.P44 })
				{
					driver.Configure.InputSignal.Pattern.Value = x;
					DetectedPatternTypeEnum value = driver.Configure.InputSignal.Pattern.Value;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PATTern:LENergy[:LE1M]
				foreach (LePatternTypeEnum x in new LePatternTypeEnum[] { LePatternTypeEnum.OTHer, LePatternTypeEnum.P11, LePatternTypeEnum.P44 })
				{
					driver.Configure.InputSignal.Pattern.LowEnergy.Le1m = x;
					LePatternTypeEnum value = driver.Configure.InputSignal.Pattern.LowEnergy.Le1m;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PATTern:LENergy:LRANge
				foreach (TransmitPatternTypeEnum x in new TransmitPatternTypeEnum[] { TransmitPatternTypeEnum.ALL1, TransmitPatternTypeEnum.OTHer })
				{
					driver.Configure.InputSignal.Pattern.LowEnergy.Lrange = x;
					TransmitPatternTypeEnum value = driver.Configure.InputSignal.Pattern.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:PATTern:LENergy:LE2M
				foreach (LePatternTypeEnum x in new LePatternTypeEnum[] { LePatternTypeEnum.OTHer, LePatternTypeEnum.P11, LePatternTypeEnum.P44 })
				{
					driver.Configure.InputSignal.Pattern.LowEnergy.Le2m = x;
					LePatternTypeEnum value = driver.Configure.InputSignal.Pattern.LowEnergy.Le2m;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:CSCHeme:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.InputSignal.Cscheme.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.InputSignal.Cscheme.LowEnergy.Lrange;
				}
			}
			{	// CONFigure:BLUetooth:MEASurement<Instance>:ISIGnal:FEC:LENergy:LRANge
				foreach (CodingSchemeEnum x in new CodingSchemeEnum[] { CodingSchemeEnum.S2, CodingSchemeEnum.S8 })
				{
					driver.Configure.InputSignal.Fec.LowEnergy.Lrange = x;
					CodingSchemeEnum value = driver.Configure.InputSignal.Fec.LowEnergy.Lrange;
				}
			}
			{	// INITiate:BLUetooth:MEASurement<Instance>:TRX
				driver.Trx.Initiate();
				driver.Trx.InitiateAndWait();
			}
			{	// STOP:BLUetooth:MEASurement<Instance>:TRX
				driver.Trx.Stop();
				driver.Trx.StopAndWait();
			}
			{	// ABORt:BLUetooth:MEASurement<Instance>:TRX
				driver.Trx.Abort();
				driver.Trx.AbortAndWait();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:STATe
				ResourceStateEnum value = driver.Trx.State.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:STATe:ALL
				RsCmwBluetoothMeas_Trx_State_All.Fetch_Data value = driver.Trx.State.All.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:SPOT
				ResultEnum value = driver.Trx.Spot.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:MODulation
				RsCmwBluetoothMeas_Trx_Modulation.Fetch_Data value = driver.Trx.Modulation.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:POWer
				RsCmwBluetoothMeas_Trx_Power.Fetch_Data value = driver.Trx.Power.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:TRX:ACP
				RsCmwBluetoothMeas_Trx_Acp.Fetch_Data value = driver.Trx.Acp.Fetch();				
			}
			{	// STOP:BLUetooth:MEASurement<Instance>:MEValuation
				driver.MultiEval.Stop();
				driver.MultiEval.StopAndWait();
			}
			{	// ABORt:BLUetooth:MEASurement<Instance>:MEValuation
				driver.MultiEval.Abort();
				driver.MultiEval.AbortAndWait();
			}
			{	// INITiate:BLUetooth:MEASurement<Instance>:MEValuation
				driver.MultiEval.Initiate();
				driver.MultiEval.InitiateAndWait();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PENCoding
				RsCmwBluetoothMeas_MultiEval_List_Segment_Pencoding.Fetch_Data value = driver.MultiEval.List.Segment.Pencoding.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Pencoding.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SACP[:PTX]
				RsCmwBluetoothMeas_MultiEval_List_Segment_Sacp_Ptx.Fetch_Data value = driver.MultiEval.List.Segment.Sacp.Ptx.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Sacp.Ptx.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:SOBW:MAXimum
				RsCmwBluetoothMeas_MultiEval_List_Segment_SoBw_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.SoBw.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.SoBw.Maximum.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:CURRent
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_Current.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Current.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:CURRent:EXTended
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_Current_Extended.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Current.Extended.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Current.Extended.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:AVERage
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_Average.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Average.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:MINimum
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Minimum.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:MAXimum
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.Maximum.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:SDEViation
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_StandardDev.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.StandardDev.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:MODulation:SDEViation:EXTended
				RsCmwBluetoothMeas_MultiEval_List_Segment_Modulation_StandardDev_Extended.Fetch_Data value = driver.MultiEval.List.Segment.Modulation.StandardDev.Extended.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.Modulation.StandardDev.Extended.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PVTime:CURRent
				RsCmwBluetoothMeas_MultiEval_List_Segment_PowerVsTime_Current.Fetch_Data value = driver.MultiEval.List.Segment.PowerVsTime.Current.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.PowerVsTime.Current.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PVTime:AVERage
				RsCmwBluetoothMeas_MultiEval_List_Segment_PowerVsTime_Average.Fetch_Data value = driver.MultiEval.List.Segment.PowerVsTime.Average.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.PowerVsTime.Average.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PVTime:MINimum
				RsCmwBluetoothMeas_MultiEval_List_Segment_PowerVsTime_Minimum.Fetch_Data value = driver.MultiEval.List.Segment.PowerVsTime.Minimum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.PowerVsTime.Minimum.Fetch();
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:LIST:SEGMent<nr>:PVTime:MAXimum
				RsCmwBluetoothMeas_MultiEval_List_Segment_PowerVsTime_Maximum.Fetch_Data value = driver.MultiEval.List.Segment.PowerVsTime.Maximum.Fetch(SegmentRepCap.Default);
				value = driver.MultiEval.List.Segment.PowerVsTime.Maximum.Fetch();
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FRANge:AVERage
				List<double> value = driver.MultiEval.Trace.Frange.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FRANge:AVERage
				List<double> value = driver.MultiEval.Trace.Frange.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:CURRent
				List<double> value = driver.MultiEval.Trace.SoBw.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:CURRent
				List<double> value = driver.MultiEval.Trace.SoBw.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:AVERage
				List<double> value = driver.MultiEval.Trace.SoBw.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:AVERage
				List<double> value = driver.MultiEval.Trace.SoBw.Average.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:MAXimum
				List<double> value = driver.MultiEval.Trace.SoBw.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SOBW:MAXimum
				List<double> value = driver.MultiEval.Trace.SoBw.Maximum.Read();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:CURRent
				List<double> value = driver.MultiEval.Trace.Sacp.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:CURRent
				List<double> value = driver.MultiEval.Trace.Sacp.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:AVERage
				List<double> value = driver.MultiEval.Trace.Sacp.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:AVERage
				List<double> value = driver.MultiEval.Trace.Sacp.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:MAXimum
				List<double> value = driver.MultiEval.Trace.Sacp.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP:MAXimum
				List<double> value = driver.MultiEval.Trace.Sacp.Maximum.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP[:PTX]
				List<double> value = driver.MultiEval.Trace.Sacp.Ptx.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SACP[:PTX]
				List<double> value = driver.MultiEval.Trace.Sacp.Ptx.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:CURRent
				List<double> value = driver.MultiEval.Trace.Sgacp.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:CURRent
				List<double> value = driver.MultiEval.Trace.Sgacp.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:AVERage
				List<double> value = driver.MultiEval.Trace.Sgacp.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:AVERage
				List<double> value = driver.MultiEval.Trace.Sgacp.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:MAXimum
				List<double> value = driver.MultiEval.Trace.Sgacp.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp:MAXimum
				List<double> value = driver.MultiEval.Trace.Sgacp.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp[:PTX]
				List<double> value = driver.MultiEval.Trace.Sgacp.Ptx.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SGACp[:PTX]
				List<double> value = driver.MultiEval.Trace.Sgacp.Ptx.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:CURRent
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:CURRent
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:AVERage
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:AVERage
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:MAXimum
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:DEVMagnitude:MAXimum
				List<double> value = driver.MultiEval.Trace.DevMagnitude.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:CURRent
				List<double> value = driver.MultiEval.Trace.Pdifference.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:CURRent
				List<double> value = driver.MultiEval.Trace.Pdifference.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:AVERage
				List<double> value = driver.MultiEval.Trace.Pdifference.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:AVERage
				List<double> value = driver.MultiEval.Trace.Pdifference.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdifference.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDIFference:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdifference.Maximum.Read();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQABs
				RsCmwBluetoothMeas_MultiEval_Trace_IqAbs.ResultData value = driver.MultiEval.Trace.IqAbs.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQABs
				RsCmwBluetoothMeas_MultiEval_Trace_IqAbs.ResultData value = driver.MultiEval.Trace.IqAbs.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQDiff
				RsCmwBluetoothMeas_MultiEval_Trace_IqDifference.ResultData value = driver.MultiEval.Trace.IqDifference.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQDiff
				RsCmwBluetoothMeas_MultiEval_Trace_IqDifference.ResultData value = driver.MultiEval.Trace.IqDifference.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQERr
				RsCmwBluetoothMeas_MultiEval_Trace_IqError.ResultData value = driver.MultiEval.Trace.IqError.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:IQERr
				RsCmwBluetoothMeas_MultiEval_Trace_IqError.ResultData value = driver.MultiEval.Trace.IqError.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:CURRent
				List<double> value = driver.MultiEval.Trace.Fdeviation.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:CURRent
				List<double> value = driver.MultiEval.Trace.Fdeviation.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:AVERage
				List<double> value = driver.MultiEval.Trace.Fdeviation.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:AVERage
				List<double> value = driver.MultiEval.Trace.Fdeviation.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:MINimum
				List<double> value = driver.MultiEval.Trace.Fdeviation.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:MINimum
				List<double> value = driver.MultiEval.Trace.Fdeviation.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:MAXimum
				List<double> value = driver.MultiEval.Trace.Fdeviation.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:FDEViation:MAXimum
				List<double> value = driver.MultiEval.Trace.Fdeviation.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:CURRent
				List<double> value = driver.MultiEval.Trace.Spower.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:CURRent
				List<double> value = driver.MultiEval.Trace.Spower.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:AVERage
				List<double> value = driver.MultiEval.Trace.Spower.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:AVERage
				List<double> value = driver.MultiEval.Trace.Spower.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:MINimum
				List<double> value = driver.MultiEval.Trace.Spower.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:MINimum
				List<double> value = driver.MultiEval.Trace.Spower.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:MAXimum
				List<double> value = driver.MultiEval.Trace.Spower.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:SPOWer:MAXimum
				List<double> value = driver.MultiEval.Trace.Spower.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MINimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MINimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Minimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MINimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Minimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PDEViation:MAXimum
				List<double> value = driver.MultiEval.Trace.Pdeviation.Maximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:CURRent
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:AVERage
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:MINimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:TRACe:PVTime:MAXimum
				List<double> value = driver.MultiEval.Trace.PowerVsTime.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:FRANge:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Frange_Brate_Current.Calculate_Data value = driver.MultiEval.Frange.Brate.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:FRANge:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Frange_Brate_Current.ResultData value = driver.MultiEval.Frange.Brate.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:FRANge:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Frange_Brate_Current.ResultData value = driver.MultiEval.Frange.Brate.Current.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SGACp:EDRate[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sgacp_Edrate_Ptx.ResultData value = driver.MultiEval.Sgacp.Edrate.Ptx.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SGACp:EDRate[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sgacp_Edrate_Ptx.ResultData value = driver.MultiEval.Sgacp.Edrate.Ptx.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SGACp:EDRate[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sgacp_Edrate_Ptx.Calculate_Data value = driver.MultiEval.Sgacp.Edrate.Ptx.Calculate();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SOBW:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_SoBw_Brate_Maximum.Calculate_Data value = driver.MultiEval.SoBw.Brate.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SOBW:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_SoBw_Brate_Maximum.ResultData value = driver.MultiEval.SoBw.Brate.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SOBW:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_SoBw_Brate_Maximum.ResultData value = driver.MultiEval.SoBw.Brate.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:CLASsic
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_Classic.Calculate_Data value = driver.MultiEval.Sacp.Nmode.Classic.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:CLASsic
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_Classic.ResultData value = driver.MultiEval.Sacp.Nmode.Classic.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:CLASsic
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_Classic.ResultData value = driver.MultiEval.Sacp.Nmode.Classic.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le1M.Calculate_Data value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le1M.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le1M.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le1M.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le1M.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le1M.Read();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Lrange.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Lrange.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Lrange.Calculate_Data value = driver.MultiEval.Sacp.Nmode.LowEnergy.Lrange.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le2M.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le2M.ResultData value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:NMODe:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_Nmode_LowEnergy_Le2M.Calculate_Data value = driver.MultiEval.Sacp.Nmode.LowEnergy.Le2M.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Lrange.ResultData value = driver.MultiEval.Sacp.LowEnergy.Lrange.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Lrange.ResultData value = driver.MultiEval.Sacp.LowEnergy.Lrange.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LRANge
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Lrange.Calculate_Data value = driver.MultiEval.Sacp.LowEnergy.Lrange.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le2M.ResultData value = driver.MultiEval.Sacp.LowEnergy.Le2M.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le2M.ResultData value = driver.MultiEval.Sacp.LowEnergy.Le2M.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy:LE2M
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le2M.Calculate_Data value = driver.MultiEval.Sacp.LowEnergy.Le2M.Calculate();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le1M.Calculate_Data value = driver.MultiEval.Sacp.LowEnergy.Le1M.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le1M.ResultData value = driver.MultiEval.Sacp.LowEnergy.Le1M.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:LENergy[:LE1M]
				RsCmwBluetoothMeas_MultiEval_Sacp_LowEnergy_Le1M.ResultData value = driver.MultiEval.Sacp.LowEnergy.Le1M.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:SACP:BRATe[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sacp_Brate_Ptx.Calculate_Data value = driver.MultiEval.Sacp.Brate.Ptx.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:SACP:BRATe[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sacp_Brate_Ptx.ResultData value = driver.MultiEval.Sacp.Brate.Ptx.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:SACP:BRATe[:PTX]
				RsCmwBluetoothMeas_MultiEval_Sacp_Brate_Ptx.ResultData value = driver.MultiEval.Sacp.Brate.Ptx.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Average.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Current.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_Classic_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.Classic.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Current.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Average.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le1M_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le1M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Current.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Average.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Le2M_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Le2M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Current.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Average.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Nmode_LowEnergy_Lrange_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Nmode.LowEnergy.Lrange.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Edrate.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Current.Read_Data value = driver.MultiEval.PowerVsTime.Edrate.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Edrate.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Edrate.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Average.Read_Data value = driver.MultiEval.PowerVsTime.Edrate.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Edrate.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Edrate.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Edrate.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Edrate.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Edrate.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Edrate.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Edrate_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Edrate.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.Brate.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Average.Read_Data value = driver.MultiEval.PowerVsTime.Brate.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.Brate.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.Brate.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Current.Read_Data value = driver.MultiEval.PowerVsTime.Brate.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.Brate.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.Brate.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.Brate.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.Brate.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.Brate.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.Brate.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_Brate_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.Brate.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Current.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Average.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le1M_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le1M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Current.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Average.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Le2M_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Le2M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Current.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Current.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Current.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Average.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Average.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Average.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Minimum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Minimum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Minimum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Maximum.Calculate_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Maximum.Read_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PVTime:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_PowerVsTime_LowEnergy_Lrange_Maximum.Fetch_Data value = driver.MultiEval.PowerVsTime.LowEnergy.Lrange.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:SSEQuence:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Ssequence_Edrate_Current.Calculate_Data value = driver.MultiEval.Pencoding.Ssequence.Edrate.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:SSEQuence:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Ssequence_Edrate_Current.ResultData value = driver.MultiEval.Pencoding.Ssequence.Edrate.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:SSEQuence:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Ssequence_Edrate_Current.ResultData value = driver.MultiEval.Pencoding.Ssequence.Edrate.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Edrate_Current.ResultData value = driver.MultiEval.Pencoding.Edrate.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Edrate_Current.ResultData value = driver.MultiEval.Pencoding.Edrate.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Pencoding_Edrate_Current.Calculate_Data value = driver.MultiEval.Pencoding.Edrate.Current.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:PENCoding:EDRate:CURRent:C
				RsCmwBluetoothMeas_MultiEval_Pencoding_Edrate_Current_C.Fetch_Data value = driver.MultiEval.Pencoding.Edrate.Current.C.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Xmaximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Xmaximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Xmaximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Xminimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Xminimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Xminimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Maximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Average.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Average.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Average.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Average.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Average.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Minimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Minimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Current.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.Current.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Current.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_Current.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.Current.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Nmode.Classic.StandardDev.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.StandardDev.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:CLASsic:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_Classic_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.Classic.StandardDev.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xmaximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xmaximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xmaximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xminimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xminimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Xminimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Maximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Maximum.Fetch_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Maximum.Read_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Minimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Minimum.Fetch_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Minimum.Read_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Minimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Average.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Average.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Average.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Current.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Current.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.Current.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.StandardDev.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.StandardDev.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le1M.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Current.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Average.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LE2M:YIELd
				List<double> value = driver.MultiEval.Modulation.Nmode.LowEnergy.Le2M.Yield.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xmaximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Xminimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Maximum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Minimum.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Current.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Current.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Average.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_Average.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StandardDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StDev.Calculate_Data value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:NMODe:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_Nmode_LowEnergy_Lrange_StDev.ResultData value = driver.MultiEval.Modulation.Nmode.LowEnergy.Lrange.StDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Current.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Average.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le2M.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Maximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Maximum.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Current.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Average.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:CTE:LENergy:LE1M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Cte_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.Cte.LowEnergy.Le1M.StandardDev.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.Brate.Xmaximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xmaximum.ResultData value = driver.MultiEval.Modulation.Brate.Xmaximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xmaximum.ResultData value = driver.MultiEval.Modulation.Brate.Xmaximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.Brate.Xminimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xminimum.ResultData value = driver.MultiEval.Modulation.Brate.Xminimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Xminimum.ResultData value = driver.MultiEval.Modulation.Brate.Xminimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Brate.Maximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Maximum.ResultData value = driver.MultiEval.Modulation.Brate.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Maximum.ResultData value = driver.MultiEval.Modulation.Brate.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Minimum.Calculate_Data value = driver.MultiEval.Modulation.Brate.Minimum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Minimum.ResultData value = driver.MultiEval.Modulation.Brate.Minimum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Minimum.ResultData value = driver.MultiEval.Modulation.Brate.Minimum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Average.Calculate_Data value = driver.MultiEval.Modulation.Brate.Average.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Average.ResultData value = driver.MultiEval.Modulation.Brate.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Average.ResultData value = driver.MultiEval.Modulation.Brate.Average.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Current.Calculate_Data value = driver.MultiEval.Modulation.Brate.Current.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Current.ResultData value = driver.MultiEval.Modulation.Brate.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_Current.ResultData value = driver.MultiEval.Modulation.Brate.Current.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Brate.StandardDev.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_StandardDev.ResultData value = driver.MultiEval.Modulation.Brate.StandardDev.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Brate_StandardDev.ResultData value = driver.MultiEval.Modulation.Brate.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:BRATe:YIELd
				List<double> value = driver.MultiEval.Modulation.Brate.Yield.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Current.Calculate_Data value = driver.MultiEval.Modulation.Edrate.Current.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Current.ResultData value = driver.MultiEval.Modulation.Edrate.Current.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Current.ResultData value = driver.MultiEval.Modulation.Edrate.Current.Read();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:CURRent:EXTended
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Current_Extended.ResultData value = driver.MultiEval.Modulation.Edrate.Current.Extended.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:CURRent:EXTended
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Current_Extended.ResultData value = driver.MultiEval.Modulation.Edrate.Current.Extended.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Average.Calculate_Data value = driver.MultiEval.Modulation.Edrate.Average.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Average.ResultData value = driver.MultiEval.Modulation.Edrate.Average.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Average.ResultData value = driver.MultiEval.Modulation.Edrate.Average.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Maximum.Calculate_Data value = driver.MultiEval.Modulation.Edrate.Maximum.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Maximum.ResultData value = driver.MultiEval.Modulation.Edrate.Maximum.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_Maximum.ResultData value = driver.MultiEval.Modulation.Edrate.Maximum.Read();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.Edrate.StandardDev.Calculate();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_StandardDev.ResultData value = driver.MultiEval.Modulation.Edrate.StandardDev.Fetch();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:EDRate:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_Edrate_StandardDev.ResultData value = driver.MultiEval.Modulation.Edrate.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Minimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Current.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Average.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le1M.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le1M_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le1M.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy[:LE1M]:YIELd
				List<double> value = driver.MultiEval.Modulation.LowEnergy.Le1M.Yield.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Maximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Minimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Current.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Average.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Le2M.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Le2M_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Le2M.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LE2M:YIELd
				List<double> value = driver.MultiEval.Modulation.LowEnergy.Le2M.Yield.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xmaximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xmaximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xmaximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xmaximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xmaximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xminimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xminimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xminimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:XMINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Xminimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Xminimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Maximum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Maximum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Maximum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MAXimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Maximum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Maximum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Minimum.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Minimum.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Minimum.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:MINimum
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Minimum.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Minimum.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Current.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Current.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Current.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:CURRent
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Current.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Current.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Average.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Average.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.Average.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:AVERage
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_Average.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.Average.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.StandardDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StandardDev.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.StandardDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:SDEViation
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StandardDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.StandardDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.StDev.Fetch();				
			}
			{	// CALCulate:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StDev.Calculate_Data value = driver.MultiEval.Modulation.LowEnergy.Lrange.StDev.Calculate();				
			}
			{	// READ:BLUetooth:MEASurement<Instance>:MEValuation:MODulation:LENergy:LRANge:STDev
				RsCmwBluetoothMeas_MultiEval_Modulation_LowEnergy_Lrange_StDev.ResultData value = driver.MultiEval.Modulation.LowEnergy.Lrange.StDev.Read();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:STATe
				ResourceStateEnum value = driver.MultiEval.State.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:MEValuation:STATe:ALL
				RsCmwBluetoothMeas_MultiEval_State_All.Fetch_Data value = driver.MultiEval.State.All.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:CTE:LENergy:LE1M:TYPE
				CtePacketTypeEnum value = driver.InputSignal.Adetected.Cte.LowEnergy.Le1M.Type.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:CTE:LENergy:LE1M:UNITs
				int value = driver.InputSignal.Adetected.Cte.LowEnergy.Le1M.Units.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:CTE:LENergy:LE2M:TYPE
				CtePacketTypeEnum value = driver.InputSignal.Adetected.Cte.LowEnergy.Le2M.Type.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:CTE:LENergy:LE2M:UNITs
				int value = driver.InputSignal.Adetected.Cte.LowEnergy.Le2M.Units.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:CODing:LENergy:LRANge
				CodingSchemeEnum value = driver.InputSignal.Adetected.Coding.LowEnergy.Lrange.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PLENgth:LENergy:LRANge
				int value = driver.InputSignal.Adetected.Plength.LowEnergy.Lrange.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PLENgth:LENergy:LE2M
				int value = driver.InputSignal.Adetected.Plength.LowEnergy.Le2M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PLENgth:LENergy[:LE1M]
				int value = driver.InputSignal.Adetected.Plength.LowEnergy.Le1M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PLENgth:EDRate
				int value = driver.InputSignal.Adetected.Plength.Edrate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PLENgth:BRATe
				int value = driver.InputSignal.Adetected.Plength.Brate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PTYPe:LENergy:LRANge
				LePacketTypeEnum value = driver.InputSignal.Adetected.Ptype.LowEnergy.Lrange.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PTYPe:LENergy:LE2M
				LePacketTypeEnum value = driver.InputSignal.Adetected.Ptype.LowEnergy.Le2M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PTYPe:LENergy[:LE1M]
				LePacketTypeEnum value = driver.InputSignal.Adetected.Ptype.LowEnergy.Le1M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PTYPe:EDRate
				EdrPacketTypeEnum value = driver.InputSignal.Adetected.Ptype.Edrate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PTYPe:BRATe
				BrPacketTypeEnum value = driver.InputSignal.Adetected.Ptype.Brate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PATTern:LENergy:LRANge
				LeRangePaternTypeEnum value = driver.InputSignal.Adetected.Pattern.LowEnergy.Lrange.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PATTern:LENergy:LE2M
				DetectedPatternTypeEnum value = driver.InputSignal.Adetected.Pattern.LowEnergy.Le2M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PATTern:LENergy[:LE1M]
				DetectedPatternTypeEnum value = driver.InputSignal.Adetected.Pattern.LowEnergy.Le1M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PATTern[:BRATe]
				DetectedPatternTypeEnum value = driver.InputSignal.Adetected.Pattern.Brate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:AADDress:LENergy[:LE1M]
				string value = driver.InputSignal.Adetected.Aaddress.LowEnergy.Le1M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:PDUType:LENergy[:LE1M]
				RsCmwBluetoothMeas_InputSignal_Adetected_PduType_LowEnergy_Le1M.Fetch_Data value = driver.InputSignal.Adetected.PduType.LowEnergy.Le1M.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:NOSLots:EDRate
				int value = driver.InputSignal.Adetected.NoSlots.Edrate.Fetch();				
			}
			{	// FETCh:BLUetooth:MEASurement<Instance>:ISIGnal:ADETected:NOSLots:BRATe
				int value = driver.InputSignal.Adetected.NoSlots.Brate.Fetch();				
			}
			{	// TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:SOURce
				string value = driver.Trigger.MultiEval.Source;
				driver.Trigger.MultiEval.Source = value;
			}
			{	// TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:THReshold
				double value = driver.Trigger.MultiEval.Threshold;
				driver.Trigger.MultiEval.Threshold = value;
			}
			{	// TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:TOUT
				double value = driver.Trigger.MultiEval.Timeout;
				driver.Trigger.MultiEval.Timeout = value;
			}
			{	// TRIGger:BLUetooth:MEASurement<Instance>:MEValuation:CATalog:SOURce
				List<string> value = driver.Trigger.MultiEval.Catalog.Source;
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>
				RsCmwBluetoothMeas_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>:SCENario:CSPath
				string value = driver.Route.Scenario.Cspath;
				driver.Route.Scenario.Cspath = value;
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>:SCENario:SALone
				RsCmwBluetoothMeas_Route_Scenario.Salone_Data value = driver.Route.Scenario.Salone;
				driver.Route.Scenario.Salone = value;
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>:SCENario
				foreach (TestScenarioEnum x in new TestScenarioEnum[] { TestScenarioEnum.CSPath, TestScenarioEnum.SALone, TestScenarioEnum.UNDefined })
				{
					TestScenarioEnum value = driver.Route.Scenario.Value;
				}
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>:SCENario:MAPRotocol
				driver.Route.Scenario.MaProtocol.Set();
				driver.Route.Scenario.MaProtocol.SetAndWait();
			}
			{	// ROUTe:BLUetooth:MEASurement<Instance>:RFSettings:CONNector
				foreach (RxConnectorEnum x in new RxConnectorEnum[] { RxConnectorEnum.I11I, RxConnectorEnum.I13I, RxConnectorEnum.I15I, RxConnectorEnum.I17I, RxConnectorEnum.I21I, RxConnectorEnum.I23I, RxConnectorEnum.I25I, RxConnectorEnum.I27I, RxConnectorEnum.I31I, RxConnectorEnum.I33I, RxConnectorEnum.I35I, RxConnectorEnum.I37I, RxConnectorEnum.I41I, RxConnectorEnum.I43I, RxConnectorEnum.I45I, RxConnectorEnum.I47I, RxConnectorEnum.IF1, RxConnectorEnum.IF2, RxConnectorEnum.IF3, RxConnectorEnum.IQ1I, RxConnectorEnum.IQ3I, RxConnectorEnum.IQ5I, RxConnectorEnum.IQ7I, RxConnectorEnum.R11, RxConnectorEnum.R11C, RxConnectorEnum.R12, RxConnectorEnum.R12C, RxConnectorEnum.R12I, RxConnectorEnum.R13, RxConnectorEnum.R13C, RxConnectorEnum.R14, RxConnectorEnum.R14C, RxConnectorEnum.R14I, RxConnectorEnum.R15, RxConnectorEnum.R16, RxConnectorEnum.R17, RxConnectorEnum.R18, RxConnectorEnum.R21, RxConnectorEnum.R21C, RxConnectorEnum.R22, RxConnectorEnum.R22C, RxConnectorEnum.R22I, RxConnectorEnum.R23, RxConnectorEnum.R23C, RxConnectorEnum.R24, RxConnectorEnum.R24C, RxConnectorEnum.R24I, RxConnectorEnum.R25, RxConnectorEnum.R26, RxConnectorEnum.R27, RxConnectorEnum.R28, RxConnectorEnum.R31, RxConnectorEnum.R31C, RxConnectorEnum.R32, RxConnectorEnum.R32C, RxConnectorEnum.R32I, RxConnectorEnum.R33, RxConnectorEnum.R33C, RxConnectorEnum.R34, RxConnectorEnum.R34C, RxConnectorEnum.R34I, RxConnectorEnum.R35, RxConnectorEnum.R36, RxConnectorEnum.R37, RxConnectorEnum.R38, RxConnectorEnum.R41, RxConnectorEnum.R41C, RxConnectorEnum.R42, RxConnectorEnum.R42C, RxConnectorEnum.R42I, RxConnectorEnum.R43, RxConnectorEnum.R43C, RxConnectorEnum.R44, RxConnectorEnum.R44C, RxConnectorEnum.R44I, RxConnectorEnum.R45, RxConnectorEnum.R46, RxConnectorEnum.R47, RxConnectorEnum.R48, RxConnectorEnum.RA1, RxConnectorEnum.RA2, RxConnectorEnum.RA3, RxConnectorEnum.RA4, RxConnectorEnum.RA5, RxConnectorEnum.RA6, RxConnectorEnum.RA7, RxConnectorEnum.RA8, RxConnectorEnum.RB1, RxConnectorEnum.RB2, RxConnectorEnum.RB3, RxConnectorEnum.RB4, RxConnectorEnum.RB5, RxConnectorEnum.RB6, RxConnectorEnum.RB7, RxConnectorEnum.RB8, RxConnectorEnum.RC1, RxConnectorEnum.RC2, RxConnectorEnum.RC3, RxConnectorEnum.RC4, RxConnectorEnum.RC5, RxConnectorEnum.RC6, RxConnectorEnum.RC7, RxConnectorEnum.RC8, RxConnectorEnum.RD1, RxConnectorEnum.RD2, RxConnectorEnum.RD3, RxConnectorEnum.RD4, RxConnectorEnum.RD5, RxConnectorEnum.RD6, RxConnectorEnum.RD7, RxConnectorEnum.RD8, RxConnectorEnum.RE1, RxConnectorEnum.RE2, RxConnectorEnum.RE3, RxConnectorEnum.RE4, RxConnectorEnum.RE5, RxConnectorEnum.RE6, RxConnectorEnum.RE7, RxConnectorEnum.RE8, RxConnectorEnum.RF1, RxConnectorEnum.RF1C, RxConnectorEnum.RF2, RxConnectorEnum.RF2C, RxConnectorEnum.RF2I, RxConnectorEnum.RF3, RxConnectorEnum.RF3C, RxConnectorEnum.RF4, RxConnectorEnum.RF4C, RxConnectorEnum.RF4I, RxConnectorEnum.RF5, RxConnectorEnum.RF5C, RxConnectorEnum.RF6, RxConnectorEnum.RF6C, RxConnectorEnum.RF7, RxConnectorEnum.RF8, RxConnectorEnum.RFAC, RxConnectorEnum.RFBC, RxConnectorEnum.RFBI, RxConnectorEnum.RG1, RxConnectorEnum.RG2, RxConnectorEnum.RG3, RxConnectorEnum.RG4, RxConnectorEnum.RG5, RxConnectorEnum.RG6, RxConnectorEnum.RG7, RxConnectorEnum.RG8, RxConnectorEnum.RH1, RxConnectorEnum.RH2, RxConnectorEnum.RH3, RxConnectorEnum.RH4, RxConnectorEnum.RH5, RxConnectorEnum.RH6, RxConnectorEnum.RH7, RxConnectorEnum.RH8 })
				{
					driver.Route.RfSettings.Connector = x;
					RxConnectorEnum value = driver.Route.RfSettings.Connector;
				}
			}
		}
	}
}