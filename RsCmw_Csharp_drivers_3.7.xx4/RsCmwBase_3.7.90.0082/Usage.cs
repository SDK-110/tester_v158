using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwBase;

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
			RsCmwBase driver = new RsCmwBase("TCPIP::localhost::INSTR", true, true);
			{	// *EMC
				bool value = driver.MacroEnable;
				driver.MacroEnable = value;
			}
			{	// *DEV
				int value = driver.DeviceNumber;
				driver.DeviceNumber = value;
			}
			{	// *GOPC
				bool value = driver.GlobalOpc;
			}
			{	// CONFigure:BASE:FCONtrol
				foreach (FanModeEnum x in new FanModeEnum[] { FanModeEnum.HIGH, FanModeEnum.LOW, FanModeEnum.NORMal })
				{
					driver.Configure.Fcontrol = x;
					FanModeEnum value = driver.Configure.Fcontrol;
				}
			}
			{	// CONFigure:SPOint:CATalog
				RsCmwBase_Configure_Spoint.Catalog_Data value = driver.Configure.Spoint.Catalog;
			}
			{	// CONFigure:SPOint:UNDefine
				driver.Configure.Spoint.Undefine = "1";
			}
			{	// CONFigure:SPOint:DEFine
				RsCmwBase_Configure_Spoint.Define_Data value = new RsCmwBase_Configure_Spoint.Define_Data();
				driver.Configure.Spoint.Define = value;
			}
			{	// CONFigure:SPOint:REWait
				RsCmwBase_Configure_Spoint.GetRewait_Data value = driver.Configure.Spoint.GetRewait("1");				
			}
			{	// CONFigure:SPOint:JOIN
				RsCmwBase_Configure_Spoint.GetJoin_Data value = driver.Configure.Spoint.GetJoin("1", JoinActionEnum.CTASk, SyncPollingEnum.NPOLling, 1.0);
				value = driver.Configure.Spoint.GetJoin("1");				
			}
			{	// CONFigure:SEMaphore:CATalog
				RsCmwBase_Configure_Semaphore.Catalog_Data value = driver.Configure.Semaphore.Catalog;
			}
			{	// CONFigure:SEMaphore:COUNt
				int value = driver.Configure.Semaphore.GetCount("1");				
			}
			{	// CONFigure:SEMaphore:RELease
				RsCmwBase_Configure_Semaphore.Release_Data value = new RsCmwBase_Configure_Semaphore.Release_Data();
				driver.Configure.Semaphore.Release = value;
			}
			{	// CONFigure:SEMaphore:ACQuire
				int value = driver.Configure.Semaphore.GetAcquire("1");				
			}
			{	// CONFigure:SEMaphore:UNDefine
				driver.Configure.Semaphore.Undefine = "1";
			}
			{	// CONFigure:SEMaphore:DEFine
				RsCmwBase_Configure_Semaphore.Define_Data value = new RsCmwBase_Configure_Semaphore.Define_Data();
				driver.Configure.Semaphore.Define = value;
			}
			{	// CONFigure:MUTex:LOCK
				int value = driver.Configure.Mutex.GetLock("1", 1.0);
				value = driver.Configure.Mutex.GetLock("1");				
			}
			{	// CONFigure:MUTex:UNLock
				RsCmwBase_Configure_Mutex.Unlock_Data value = new RsCmwBase_Configure_Mutex.Unlock_Data();
				driver.Configure.Mutex.Unlock = value;
			}
			{	// CONFigure:MUTex:STATe
				RsCmwBase_Configure_Mutex.GetState_Data value = driver.Configure.Mutex.GetState("1", MutexActionEnum.DONothing, 1.0);
				value = driver.Configure.Mutex.GetState("1");				
			}
			{	// CONFigure:MUTex:DEFine
				RsCmwBase_Configure_Mutex.Define_Data value = new RsCmwBase_Configure_Mutex.Define_Data();
				driver.Configure.Mutex.Define = value;
			}
			{	// CONFigure:MUTex:UNDefine
				driver.Configure.Mutex.Undefine = "1";
			}
			{	// CONFigure:MUTex:CATalog
				RsCmwBase_Configure_Mutex.Catalog_Data value = driver.Configure.Mutex.Catalog;
			}
			{	// CONFigure:BASE:MCMW:REARrange
				foreach (BoxNumberEnum x in new BoxNumberEnum[] { BoxNumberEnum.BOX1, BoxNumberEnum.BOX2, BoxNumberEnum.BOX3, BoxNumberEnum.BOX4, BoxNumberEnum.BOX5, BoxNumberEnum.BOX6, BoxNumberEnum.BOX7, BoxNumberEnum.BOX8, BoxNumberEnum.NAV })
				{
					driver.Configure.MultiCmw.Rearrange = new List<BoxNumberEnum> { x, x, x, x, x };					
				}
			}
			{	// CONFigure:BASE:MCMW:IDENtify:BTIMe
				int value = driver.Configure.MultiCmw.Identify.Btime;
				driver.Configure.MultiCmw.Identify.Btime = value;
			}
			{	// CONFigure:BASE:IPSet:NWADapter<n>
				RsCmwBase_Configure_IpSubnet_NwAdapter.Get_Data value = driver.Configure.IpSubnet.NwAdapter.Get(NwAdapterRepCap.Default);
				value = driver.Configure.IpSubnet.NwAdapter.Get();
			}
			{	// CONFigure:BASE:IPSet:NWADapter<n>
				driver.Configure.IpSubnet.NwAdapter.Set(false, NwAdapterRepCap.Default);
				driver.Configure.IpSubnet.NwAdapter.Set(false);
			}
			{	// CONFigure:BASE:ADJustment:TYPE
				foreach (OscillatorTypeEnum x in new OscillatorTypeEnum[] { OscillatorTypeEnum.OCXO, OscillatorTypeEnum.TCXO })
				{
					OscillatorTypeEnum value = driver.Configure.Adjustment.Type;
				}
			}
			{	// CONFigure:BASE:ADJustment:VALue
				double value = driver.Configure.Adjustment.Value;
				driver.Configure.Adjustment.Value = value;
			}
			{	// CONFigure:BASE:ADJustment:SAVE
				driver.Configure.Adjustment.Save();
				driver.Configure.Adjustment.SaveAndWait();
			}
			{	// CONFigure:BASE:IPCR:ENABle
				List<bool> value = driver.Configure.Ipcr.Enable;
				driver.Configure.Ipcr.Enable = value;
			}
			{	// CONFigure:BASE:IPCR:IDENt
				List<string> value = driver.Configure.Ipcr.Ident;
			}
			{	// CONFigure:BASE:FDCorrection:SAV
				driver.Configure.FreqCorrection.Save("1");
				driver.Configure.FreqCorrection.Save();
			}
			{	// CONFigure:BASE:FDCorrection:RCL
				driver.Configure.FreqCorrection.Recall("1");
				driver.Configure.FreqCorrection.Recall();
			}
			{	// CONFigure:FDCorrection:DEACtivate
				driver.Configure.FreqCorrection.Deactivate("r1", RxTxDirectionEnum.RX, RfConverterInPathEnum.RF1);
				driver.Configure.FreqCorrection.Deactivate("r1");
			}
			{	// CONFigure:FDCorrection:DEACtivate:ALL
				driver.Configure.FreqCorrection.DeactivateAll(RxTxDirectionEnum.RX, "1");
				driver.Configure.FreqCorrection.DeactivateAll();
			}
			{	// CONFigure:FDCorrection:USAGe
				RsCmwBase_Configure_FreqCorrection.GetUsage_Data value = driver.Configure.FreqCorrection.GetUsage("r1", RfConverterInPathEnum.RF1);
				value = driver.Configure.FreqCorrection.GetUsage("r1");				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:CREate
				RsCmwBase_Configure_FreqCorrection_CorrectionTable.Create_Data value = new RsCmwBase_Configure_FreqCorrection_CorrectionTable.Create_Data();
				driver.Configure.FreqCorrection.CorrectionTable.Create = value;
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:ERASe
				RsCmwBase_Configure_FreqCorrection_CorrectionTable.Erase_Data value = new RsCmwBase_Configure_FreqCorrection_CorrectionTable.Erase_Data();
				driver.Configure.FreqCorrection.CorrectionTable.Erase = value;
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:ADD
				RsCmwBase_Configure_FreqCorrection_CorrectionTable.Add_Data value = new RsCmwBase_Configure_FreqCorrection_CorrectionTable.Add_Data();
				driver.Configure.FreqCorrection.CorrectionTable.Add = value;
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:DELete
				driver.Configure.FreqCorrection.CorrectionTable.Delete("1");				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:LENGth
				int value = driver.Configure.FreqCorrection.CorrectionTable.GetLength("1");				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:DETails
				RsCmwBase_Configure_FreqCorrection_CorrectionTable.GetDetails_Data value = driver.Configure.FreqCorrection.CorrectionTable.GetDetails("1", 1.0, 1.0);
				value = driver.Configure.FreqCorrection.CorrectionTable.GetDetails("1");				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:CATalog
				List<string> value = driver.Configure.FreqCorrection.CorrectionTable.GetCatalog("1");
				value = driver.Configure.FreqCorrection.CorrectionTable.GetCatalog();				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:COUNt
				int value = driver.Configure.FreqCorrection.CorrectionTable.GetCount("1");
				value = driver.Configure.FreqCorrection.CorrectionTable.GetCount();				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:EXISt
				int value = driver.Configure.FreqCorrection.CorrectionTable.GetExist("1");				
			}
			{	// CONFigure:BASE:FDCorrection:CTABle:DELete:ALL
				driver.Configure.FreqCorrection.CorrectionTable.DeleteAll.Set("1");
				driver.Configure.FreqCorrection.CorrectionTable.DeleteAll.Set();
			}
			{	// CONFigure:FDCorrection:ACTivate
				RsCmwBase_Configure_FreqCorrection_Activate.Get_Data value = driver.Configure.FreqCorrection.Activate.Get("r1");				
			}
			{	// CONFigure:FDCorrection:ACTivate
				driver.Configure.FreqCorrection.Activate.Set("r1", "1", RxTxDirectionEnum.RX, RfConverterInPathEnum.RF1);
				driver.Configure.FreqCorrection.Activate.Set("r1", "1");
			}
			{	// CONFigure:CMWS:FDCorrection:DEACtivate:ALL
				driver.Configure.SingleCmw.FreqCorrection.DeactivateAll("1");
				driver.Configure.SingleCmw.FreqCorrection.DeactivateAll();
			}
			{	// CONFigure:CMWS:FDCorrection:USAGe
				RsCmwBase_Configure_SingleCmw_FreqCorrection.GetUsage_Data value = driver.Configure.SingleCmw.FreqCorrection.GetUsage("r1");				
			}
			{	// CONFigure:CMWS:FDCorrection:ACTivate:RX
				RsCmwBase_Configure_SingleCmw_FreqCorrection_Activate.Rx_Data value = new RsCmwBase_Configure_SingleCmw_FreqCorrection_Activate.Rx_Data();
				driver.Configure.SingleCmw.FreqCorrection.Activate.Rx = value;
			}
			{	// CONFigure:CMWS:FDCorrection:ACTivate:TX
				RsCmwBase_Configure_SingleCmw_FreqCorrection_Activate.Tx_Data value = new RsCmwBase_Configure_SingleCmw_FreqCorrection_Activate.Tx_Data();
				driver.Configure.SingleCmw.FreqCorrection.Activate.Tx = value;
			}
			{	// CONFigure:CMWS:FDCorrection:DEACtivate:RX
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Rx.Value = "r1";
			}
			{	// CONFigure:CMWS:FDCorrection:DEACtivate:RX:ALL
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Rx.All.Set("1");
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Rx.All.Set();
			}
			{	// CONFigure:CMWS:FDCorrection:DEACtivate:TX
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Tx.Value = "r1";
			}
			{	// CONFigure:CMWS:FDCorrection:DEACtivate:TX:ALL
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Tx.All.Set("1");
				driver.Configure.SingleCmw.FreqCorrection.Deactivate.Tx.All.Set();
			}
			{	// CONFigure:CMWD:TIMeout
				double value = driver.Configure.Cmwd.Timeout;
				driver.Configure.Cmwd.Timeout = value;
			}
			{	// CONFigure:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:RXFilter:SELect
				List<bool> value = driver.Configure.Correction.IfEqualizer.Slot.RxFilter.Select.Get(SlotRepCap.Default);
				value = driver.Configure.Correction.IfEqualizer.Slot.RxFilter.Select.Get();
			}
			{	// CONFigure:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:RXFilter:SELect
				driver.Configure.Correction.IfEqualizer.Slot.RxFilter.Select.Set(new List<bool> { true, false, true }, SlotRepCap.Default);
				driver.Configure.Correction.IfEqualizer.Slot.RxFilter.Select.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:TXFilter:SELect
				List<bool> value = driver.Configure.Correction.IfEqualizer.Slot.TxFilter.Select.Get(SlotRepCap.Default);
				value = driver.Configure.Correction.IfEqualizer.Slot.TxFilter.Select.Get();
			}
			{	// CONFigure:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:TXFilter:SELect
				driver.Configure.Correction.IfEqualizer.Slot.TxFilter.Select.Set(new List<bool> { true, false, true }, SlotRepCap.Default);
				driver.Configure.Correction.IfEqualizer.Slot.TxFilter.Select.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:BASE:MMONitor:IPADdress<n>
				RsCmwBase_Configure_Mmonitor_IpAddress.IpAddress_Data value = driver.Configure.Mmonitor.IpAddress.Get(IpAddressRepCap.Default);
				value = driver.Configure.Mmonitor.IpAddress.Get();
			}
			{	// CONFigure:BASE:MMONitor:IPADdress<n>
				RsCmwBase_Configure_Mmonitor_IpAddress.IpAddress_Data value = new RsCmwBase_Configure_Mmonitor_IpAddress.IpAddress_Data();
				driver.Configure.Mmonitor.IpAddress.Set(value, IpAddressRepCap.Default);
				driver.Configure.Mmonitor.IpAddress.Set(value);
			}
			{	// INITiate:BASE:MCMW
				driver.MultiCmw.Initiate(CmwSetStatusEnum.MCMW, CmwSetStatusEnum.MCMW, CmwSetStatusEnum.MCMW, CmwSetStatusEnum.MCMW);
			}
			{	// STARt:BASE:MCMW:IDENtify
				driver.MultiCmw.Identify.Start(BoxNumberEnum.BOX1, 1);
				driver.MultiCmw.Identify.Start(BoxNumberEnum.BOX1);
			}
			{	// FETCh:BASE:MCMW:SNUMber
				string value = driver.MultiCmw.Snumber.Fetch(BoxNumberEnum.BOX1);				
			}
			{	// FETCh:BASE:MCMW:STATe
				RsCmwBase_MultiCmw_State.Fetch_Data value = driver.MultiCmw.State.Fetch();				
			}
			{	// SENSe:BASE:IPSet:SNODe:NNAMe
				string value = driver.Sense.IpSubnet.Snode.Nname;
			}
			{	// SENSe:BASE:IPSet:SNODe:NTYPe
				string value = driver.Sense.IpSubnet.Snode.Ntype;
			}
			{	// SENSe:BASE:IPSet:SNODe:NSEGment
				RsCmwBase_Sense_IpSubnet_Snode.Nsegment_Data value = driver.Sense.IpSubnet.Snode.Nsegment;
			}
			{	// SENSe:BASE:IPSet:SMONitor:NAME
				List<string> value = driver.Sense.IpSubnet.SubMonitor.Name;
			}
			{	// SENSe:BASE:IPSet:SMONitor:TYPE
				List<string> value = driver.Sense.IpSubnet.SubMonitor.Type;
			}
			{	// SENSe:BASE:IPSet:SMONitor:ID
				List<int> value = driver.Sense.IpSubnet.SubMonitor.Id;
			}
			{	// SENSe:BASE:IPSet:SMONitor:DESCription
				List<string> value = driver.Sense.IpSubnet.SubMonitor.Description;
			}
			{	// SENSe:BASE:TEMPerature:ENVironment
				double value = driver.Sense.Temperature.Environment;
			}
			{	// SENSe:BASE:TEMPerature:OPERating:INTernal
				double value = driver.Sense.Temperature.Operating.Internal;
			}
			{	// SENSe:BASE:TEMPerature:EXCeeded:LIST
				RsCmwBase_Sense_Temperature_Exceeded.List_Data value = driver.Sense.Temperature.Exceeded.List;
			}
			{	// SENSe:BASE:TEMPerature:EXCeeded
				bool value = driver.Sense.Temperature.Exceeded.Value;
			}
			{	// SENSe:BASE:REFerence:FREQuency:LOCKed
				bool value = driver.Sense.Reference.Frequency.Locked;
			}
			{	// SENSe:FWUPdate:INFO
				string value = driver.Sense.FirmwareUpdate.Info;
			}
			{	// SYSTem:BASE:RELiability
				int value = driver.System.Reliability;
			}
			{	// SYSTem:DID
				string value = driver.System.Did;
			}
			{	// SYSTem:KLOCk
				bool value = driver.System.Klock;
				driver.System.Klock = value;
			}
			{	// SYSTem:PRESet
				driver.System.Preset("1");
				driver.System.Preset();
			}
			{	// SYSTem:PRESet:ALL
				driver.System.PresetAll();
				driver.System.PresetAllAndWait();
			}
			{	// SYSTem:PRESet:BASE
				driver.System.PresetBase();
				driver.System.PresetBaseAndWait();
			}
			{	// SYSTem:RESet
				driver.System.Reset("1");
				driver.System.Reset();
			}
			{	// SYSTem:RESet:ALL
				driver.System.ResetAll();
				driver.System.ResetAllAndWait();
			}
			{	// SYSTem:RESet:BASE
				driver.System.ResetBase();
				driver.System.ResetBaseAndWait();
			}
			{	// SYSTem:TZONe
				RsCmwBase_System.Tzone_Data value = driver.System.Tzone;
				driver.System.Tzone = value;
			}
			{	// SYSTem:VERSion
				double value = driver.System.Version;
			}
			{	// SYSTem:BASE:IPSet:SMONitor:REFResh
				driver.System.IpSubnet.SubMonitor.Refresh.Set();
				driver.System.IpSubnet.SubMonitor.Refresh.SetAndWait();
			}
			{	// SYSTem:BASE:DEVice:SUBinst
				RsCmwBase_System_Device.Subinst_Data value = driver.System.Device.Subinst;
			}
			{	// SYSTem:DEVice:ID
				string value = driver.System.Device.Id;
			}
			{	// SYSTem:BASE:DEVice:LICense
				RsCmwBase_System_Device.License_Data value = driver.System.Device.License;
				driver.System.Device.License = value;
			}
			{	// SYSTem:BASE:DEVice:COUNt
				int value = driver.System.Device.Count;
				driver.System.Device.Count = value;
			}
			{	// SYSTem:BASE:DEVice:RESet
				driver.System.Device.Reset();
				driver.System.Device.ResetAndWait();
			}
			{	// SYSTem:BASE:DEVice:SETup
				RsCmwBase_System_Device.Setup_Data value = driver.System.Device.Setup;
				driver.System.Device.Setup = value;
			}
			{	// SYSTem:BASE:DEVice:MSCont
				int value = driver.System.Device.Mscont;
			}
			{	// SYSTem:BASE:DEVice:MSCCount
				int value = driver.System.Device.MscCount;
			}
			{	// SYSTem:CONNector:TRANslation
				RsCmwBase_System_Connector.GetTranslation_Data value = driver.System.Connector.GetTranslation("r1");				
			}
			{	// SYSTem:ROUTing:POSSible
				List<string> value = driver.System.Routing.GetPossible("1");
				value = driver.System.Routing.GetPossible();				
			}
			{	// SYSTem:BASE:REFerence:FREQuency:SOURce
				foreach (SourceIntExtEnum x in new SourceIntExtEnum[] { SourceIntExtEnum.EINTernal, SourceIntExtEnum.EXTernal, SourceIntExtEnum.INTernal })
				{
					driver.System.Reference.Frequency.Source = x;
					SourceIntExtEnum value = driver.System.Reference.Frequency.Source;
				}
			}
			{	// SYSTem:BASE:REFerence:FREQuency
				double value = driver.System.Reference.Frequency.Value;
				driver.System.Reference.Frequency.Value = value;
			}
			{	// SYSTem:BASE:REFerence:FREQuency<n>:ADVanced:SOURce
				SourceIntExtEnum value = driver.System.Reference.Frequency.Advanced.Source.Get(FrequencyRepCap.Default);
				value = driver.System.Reference.Frequency.Advanced.Source.Get();
			}
			{	// SYSTem:BASE:REFerence:FREQuency<n>:ADVanced:SOURce
				foreach (SourceIntExtEnum x in new SourceIntExtEnum[] { SourceIntExtEnum.EINTernal, SourceIntExtEnum.EXTernal, SourceIntExtEnum.INTernal })
				{
					driver.System.Reference.Frequency.Advanced.Source.Set(x);
					driver.System.Reference.Frequency.Advanced.Source.Set(x, FrequencyRepCap.Default);
				}
			}
			{	// SYSTem:BASE:REFerence:PHASe:OFFSet
				double value = driver.System.Reference.Phase.Offset;
				driver.System.Reference.Phase.Offset = value;
			}
			{	// SYSTem:BASE:REFerence:DC:OFFSet:ENABle
				bool value = driver.System.Reference.Dc.Offset.Enable;
				driver.System.Reference.Dc.Offset.Enable = value;
			}
			{	// SYSTem:BASE:SSYNc:MODE
				foreach (CmwModeEnum x in new CmwModeEnum[] { CmwModeEnum.GENerator, CmwModeEnum.LISTener, CmwModeEnum.STANdalone })
				{
					driver.System.Ssync.Mode = x;
					CmwModeEnum value = driver.System.Ssync.Mode;
				}
			}
			{	// SYSTem:BASE:SSYNc:OFFSet
				int value = driver.System.Ssync.Offset;
				driver.System.Ssync.Offset = value;
			}
			{	// SYSTem:TIME:LOCal
				RsCmwBase_System_Time.Local_Data value = driver.System.Time.Local;
				driver.System.Time.Local = value;
			}
			{	// SYSTem:TIME:UTC
				RsCmwBase_System_Time.Utc_Data value = driver.System.Time.Utc;
				driver.System.Time.Utc = value;
			}
			{	// SYSTem:TIME
				RsCmwBase_System_Time.Value_Data value = driver.System.Time.Value;
				driver.System.Time.Value = value;
			}
			{	// SYSTem:TIME:DSTime:MODE
				bool value = driver.System.Time.DaylightSavingTime.Mode;
				driver.System.Time.DaylightSavingTime.Mode = value;
			}
			{	// SYSTem:TIME:DSTime:RULE:CATalog
				string value = driver.System.Time.DaylightSavingTime.Rule.Catalog;
			}
			{	// SYSTem:TIME:DSTime:RULE
				string value = driver.System.Time.DaylightSavingTime.Rule.Value;
				driver.System.Time.DaylightSavingTime.Rule.Value = value;
			}
			{	// SYSTem:TIME:HRTimer:RELative
				driver.System.Time.HrTimer.Relative = 1;
			}
			{	// SYSTem:TIME:HRTimer:ABSolute:CLEar
				driver.System.Time.HrTimer.Absolute.Clear();
				driver.System.Time.HrTimer.Absolute.ClearAndWait();
			}
			{	// SYSTem:TIME:HRTimer:ABSolute
				driver.System.Time.HrTimer.Absolute.Value = 1.0;
			}
			{	// SYSTem:TIME:HRTimer:ABSolute:SET
				RsCmwBase_System_Time_HrTimer_Absolute_Set.Get_Data value = driver.System.Time.HrTimer.Absolute.Set.Get();				
			}
			{	// SYSTem:TIME:HRTimer:ABSolute:SET
				driver.System.Time.HrTimer.Absolute.Set.Set();
				driver.System.Time.HrTimer.Absolute.Set.SetAndWait();
			}
			{	// SYSTem:DATE:LOCal
				RsCmwBase_System_Date.Local_Data value = driver.System.Date.Local;
				driver.System.Date.Local = value;
			}
			{	// SYSTem:DATE:UTC
				RsCmwBase_System_Date.Utc_Data value = driver.System.Date.Utc;
				driver.System.Date.Utc = value;
			}
			{	// SYSTem:DATE
				RsCmwBase_System_Date.Value_Data value = driver.System.Date.Value;
				driver.System.Date.Value = value;
			}
			{	// SYSTem:DISPlay:UPDate
				bool value = driver.System.Display.Update;
				driver.System.Display.Update = value;
			}
			{	// SYSTem:BASE:DISPlay:MWINdow
				bool value = driver.System.Display.Mwindow;
				driver.System.Display.Mwindow = value;
			}
			{	// SYSTem:BASE:DISPlay:COLorset
				foreach (ColorSetEnum x in new ColorSetEnum[] { ColorSetEnum.DEF })
				{
					driver.System.Display.ColorSet = x;
					ColorSetEnum value = driver.System.Display.ColorSet;
				}
			}
			{	// SYSTem:BASE:DISPlay:FONTset
				foreach (FontTypeEnum x in new FontTypeEnum[] { FontTypeEnum.DEF, FontTypeEnum.LRG })
				{
					driver.System.Display.FontSet = x;
					FontTypeEnum value = driver.System.Display.FontSet;
				}
			}
			{	// SYSTem:BASE:DISPlay:ROLLkeymode
				foreach (RollkeyModeEnum x in new RollkeyModeEnum[] { RollkeyModeEnum.CURSors, RollkeyModeEnum.VERTical, RollkeyModeEnum.ZIGZag })
				{
					driver.System.Display.RollKeymode = x;
					RollkeyModeEnum value = driver.System.Display.RollKeymode;
				}
			}
			{	// SYSTem:BASE:DISPlay:LANGuage
				foreach (DisplayLanguageEnum x in new DisplayLanguageEnum[] { DisplayLanguageEnum.AR, DisplayLanguageEnum.CS, DisplayLanguageEnum.DA, DisplayLanguageEnum.DE, DisplayLanguageEnum.EN, DisplayLanguageEnum.ES, DisplayLanguageEnum.FR, DisplayLanguageEnum.IT, DisplayLanguageEnum.JA, DisplayLanguageEnum.KO, DisplayLanguageEnum.RU, DisplayLanguageEnum.SV, DisplayLanguageEnum.TR, DisplayLanguageEnum.ZH })
				{
					driver.System.Display.Language = x;
					DisplayLanguageEnum value = driver.System.Display.Language;
				}
			}
			{	// SYSTem:DISPlay:MONitor
				driver.System.Display.Monitor.Value = false;
			}
			{	// SYSTem:DISPlay:MONitor:OFF
				driver.System.Display.Monitor.Off.Set();
				driver.System.Display.Monitor.Off.SetAndWait();
			}
			{	// SYSTem:ERRor:ALL
				RsCmwBase_System_Error.All_Data value = driver.System.Error.All;
			}
			{	// SYSTem:ERRor:COUNt
				int value = driver.System.Error.Count;
			}
			{	// SYSTem:ERRor[:NEXT]
				RsCmwBase_System_Error.Next_Data value = driver.System.Error.Next;
			}
			{	// SYSTem:ERRor:CODE:ALL
				int value = driver.System.Error.Code.All;
			}
			{	// SYSTem:ERRor:CODE[:NEXT]
				int value = driver.System.Error.Code.Next;
			}
			{	// SYSTem:HELP:HEADers
				byte[] value = driver.System.Help.GetHeaders("1");
				value = driver.System.Help.GetHeaders();				
			}
			{	// SYSTem:HELP:SYNTax
				byte[] value = driver.System.Help.GetSyntax("1");				
			}
			{	// SYSTem:HELP:SYNTax:ALL
				byte[] value = driver.System.Help.Syntax.All;
			}
			{	// SYSTem:HELP:STATus:BITS
				List<string> value = driver.System.Help.Status.Bits;
			}
			{	// SYSTem:HELP:STATus[:REGister]
				List<string> value = driver.System.Help.Status.Register;
			}
			{	// SYSTem:RECord:MACRo:FILE:STARt
				driver.System.Record.Macro.File.Start("1");				
			}
			{	// SYSTem:RECord:MACRo:FILE:STOP
				driver.System.Record.Macro.File.Stop();
				driver.System.Record.Macro.File.StopAndWait();
			}
			{	// SYSTem:STARtup:PREPare:FDEFault
				bool value = driver.System.Startup.Prepare.Fdefault;
				driver.System.Startup.Prepare.Fdefault = value;
			}
			{	// SYSTem:UPDate:DGRoup
				string value = driver.System.Update.Dgroup;
				driver.System.Update.Dgroup = value;
			}
			{	// SYSTem:COMMunicate:NET:ADAPter
				string value = driver.System.Communicate.Net.Adapter;
				driver.System.Communicate.Net.Adapter = value;
			}
			{	// SYSTem:COMMunicate:NET:GATeway
				List<string> value = driver.System.Communicate.Net.Gateway;
				driver.System.Communicate.Net.Gateway = value;
			}
			{	// SYSTem:COMMunicate:NET:IPADdress
				List<string> value = driver.System.Communicate.Net.IpAddress;
				driver.System.Communicate.Net.IpAddress = value;
			}
			{	// SYSTem:COMMunicate:NET:HOSTname
				string value = driver.System.Communicate.Net.Hostname;
				driver.System.Communicate.Net.Hostname = value;
			}
			{	// SYSTem:COMMunicate:NET:DHCP
				bool value = driver.System.Communicate.Net.Dhcp;
				driver.System.Communicate.Net.Dhcp = value;
			}
			{	// SYSTem:COMMunicate:NET:DNS:ENABle
				bool value = driver.System.Communicate.Net.Dns.Enable;
				driver.System.Communicate.Net.Dns.Enable = value;
			}
			{	// SYSTem:COMMunicate:NET:DNS
				List<string> value = driver.System.Communicate.Net.Dns.Value;
				driver.System.Communicate.Net.Dns.Value = value;
			}
			{	// SYSTem:COMMunicate:NET:SUBNet:MASK
				List<string> value = driver.System.Communicate.Net.Subnet.Mask;
				driver.System.Communicate.Net.Subnet.Mask = value;
			}
			{	// SYSTem:COMMunicate:GPIB<inst>:VRESource
				string value = driver.System.Communicate.Gpib.GetVresource(GpibInstanceRepCap.Default);
				value = driver.System.Communicate.Gpib.GetVresource();
			}
			{	// SYSTem:COMMunicate:GPIB<inst>[:SELF]:ENABle
				bool value = driver.System.Communicate.Gpib.Self.Enable.Get(GpibInstanceRepCap.Default);
				value = driver.System.Communicate.Gpib.Self.Enable.Get();
			}
			{	// SYSTem:COMMunicate:GPIB<inst>[:SELF]:ENABle
				driver.System.Communicate.Gpib.Self.Enable.Set(false, GpibInstanceRepCap.Default);
				driver.System.Communicate.Gpib.Self.Enable.Set(false);
			}
			{	// SYSTem:COMMunicate:GPIB<inst>[:SELF]:ADDR
				int value = driver.System.Communicate.Gpib.Self.Addr.Get(GpibInstanceRepCap.Default);
				value = driver.System.Communicate.Gpib.Self.Addr.Get();
			}
			{	// SYSTem:COMMunicate:GPIB<inst>[:SELF]:ADDR
				driver.System.Communicate.Gpib.Self.Addr.Set(1, GpibInstanceRepCap.Default);
				driver.System.Communicate.Gpib.Self.Addr.Set(1);
			}
			{	// SYSTem:COMMunicate:USB:VRESource
				string value = driver.System.Communicate.Usb.Vresource;
			}
			{	// SYSTem:COMMunicate:RSIB<inst>:VRESource
				string value = driver.System.Communicate.Rsib.GetVresource(RsibInstanceRepCap.Default);
				value = driver.System.Communicate.Rsib.GetVresource();
			}
			{	// SYSTem:COMMunicate:SOCKet<inst>:VRESource
				string value = driver.System.Communicate.Socket.GetVresource(SocketInstanceRepCap.Default);
				value = driver.System.Communicate.Socket.GetVresource();
			}
			{	// SYSTem:COMMunicate:SOCKet<inst>:MODE
				SocketProtocolEnum value = driver.System.Communicate.Socket.Mode.Get(SocketInstanceRepCap.Default);
				value = driver.System.Communicate.Socket.Mode.Get();
			}
			{	// SYSTem:COMMunicate:SOCKet<inst>:MODE
				foreach (SocketProtocolEnum x in new SocketProtocolEnum[] { SocketProtocolEnum.AGILent, SocketProtocolEnum.IEEE1174, SocketProtocolEnum.RAW })
				{
					driver.System.Communicate.Socket.Mode.Set(x);
					driver.System.Communicate.Socket.Mode.Set(x, SocketInstanceRepCap.Default);
				}
			}
			{	// SYSTem:COMMunicate:SOCKet<inst>:PORT
				int value = driver.System.Communicate.Socket.Port.Get(SocketInstanceRepCap.Default);
				value = driver.System.Communicate.Socket.Port.Get();
			}
			{	// SYSTem:COMMunicate:SOCKet<inst>:PORT
				driver.System.Communicate.Socket.Port.Set(1, SocketInstanceRepCap.Default);
				driver.System.Communicate.Socket.Port.Set(1);
			}
			{	// SYSTem:COMMunicate:VXI<inst>:VRESource
				string value = driver.System.Communicate.Vxi.GetVresource(VxiInstanceRepCap.Default);
				value = driver.System.Communicate.Vxi.GetVresource();
			}
			{	// SYSTem:COMMunicate:VXI<inst>:GTR
				bool value = driver.System.Communicate.Vxi.Gtr.Get(VxiInstanceRepCap.Default);
				value = driver.System.Communicate.Vxi.Gtr.Get();
			}
			{	// SYSTem:COMMunicate:VXI<inst>:GTR
				driver.System.Communicate.Vxi.Gtr.Set(false, VxiInstanceRepCap.Default);
				driver.System.Communicate.Vxi.Gtr.Set(false);
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:VRESource
				string value = driver.System.Communicate.Serial.Receive.GetVresource(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.GetVresource();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:PACE
				SerialHandshakeEnum value = driver.System.Communicate.Serial.Receive.Pace.Get(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.Pace.Get();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:PACE
				foreach (SerialHandshakeEnum x in new SerialHandshakeEnum[] { SerialHandshakeEnum.BOTH, SerialHandshakeEnum.HW, SerialHandshakeEnum.OFF, SerialHandshakeEnum.XONoff })
				{
					driver.System.Communicate.Serial.Receive.Pace.Set(x);
					driver.System.Communicate.Serial.Receive.Pace.Set(x, SerialInstanceRepCap.Default);
				}
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:PARity[:TYPE]
				SerialParityBitEnum value = driver.System.Communicate.Serial.Receive.Parity.Type.Get(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.Parity.Type.Get();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:PARity[:TYPE]
				foreach (SerialParityBitEnum x in new SerialParityBitEnum[] { SerialParityBitEnum.EVEN, SerialParityBitEnum.MARK, SerialParityBitEnum.NONE, SerialParityBitEnum.ODD, SerialParityBitEnum.SPACe })
				{
					driver.System.Communicate.Serial.Receive.Parity.Type.Set(x);
					driver.System.Communicate.Serial.Receive.Parity.Type.Set(x, SerialInstanceRepCap.Default);
				}
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:BITS
				int value = driver.System.Communicate.Serial.Receive.Bits.Get(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.Bits.Get();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:BITS
				driver.System.Communicate.Serial.Receive.Bits.Set(1, SerialInstanceRepCap.Default);
				driver.System.Communicate.Serial.Receive.Bits.Set(1);
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:SBITs
				int value = driver.System.Communicate.Serial.Receive.Sbits.Get(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.Sbits.Get();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:SBITs
				driver.System.Communicate.Serial.Receive.Sbits.Set(1, SerialInstanceRepCap.Default);
				driver.System.Communicate.Serial.Receive.Sbits.Set(1);
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:BAUD
				int value = driver.System.Communicate.Serial.Receive.Baud.Get(SerialInstanceRepCap.Default);
				value = driver.System.Communicate.Serial.Receive.Baud.Get();
			}
			{	// SYSTem:COMMunicate:SERial<inst>:RECeive:BAUD
				driver.System.Communicate.Serial.Receive.Baud.Set(1, SerialInstanceRepCap.Default);
				driver.System.Communicate.Serial.Receive.Baud.Set(1);
			}
			{	// SYSTem:COMMunicate:HISLip<inst>:VRESource
				string value = driver.System.Communicate.Hislip.GetVresource(HislipInstanceRepCap.Default);
				value = driver.System.Communicate.Hislip.GetVresource();
			}
			{	// SYSTem:CMW<n>:DEVice:ID
				string value = driver.System.Cmw.Device.GetId(CmwVariantRepCap.Default);
				value = driver.System.Cmw.Device.GetId();
			}
			{	// SYSTem:CMWS:DEVice:ID
				string value = driver.System.SingleCmw.Device.Id;
			}
			{	// SYSTem:BASE:OPTion:LIST
				string value = driver.System.Option.GetList(ProductTypeEnum.ALL, ValidityScopeEnum.ALL, ValidityScopeBenum.INSTrument, 1.0);
				value = driver.System.Option.GetList();				
			}
			{	// SYSTem:BASE:OPTion:DESCription
				string value = driver.System.Option.GetDescription(ProductTypeEnum.ALL, ValidityScopeEnum.ALL, ValidityScopeBenum.INSTrument, 1.0);
				value = driver.System.Option.GetDescription();				
			}
			{	// SYSTem:BASE:OPTion:VERSion
				string value = driver.System.Option.GetVersion("1");
				value = driver.System.Option.GetVersion();				
			}
			{	// SYSTem:BASE:PASSword:CDISable
				foreach (UserRoleEnum x in new UserRoleEnum[] { UserRoleEnum.ADMin, UserRoleEnum.DEVeloper, UserRoleEnum.SERVice, UserRoleEnum.UEXTended, UserRoleEnum.USER })
				{
					driver.System.Password.Cdisable = x;					
				}
			}
			{	// SYSTem:PASSword:NEW
				RsCmwBase_System_Password.New_Data value = new RsCmwBase_System_Password.New_Data();
				driver.System.Password.New = value;
			}
			{	// SYSTem:BASE:PASSword[:CENable]:STATe
				foreach (UserRoleEnum x in new UserRoleEnum[] { UserRoleEnum.ADMin, UserRoleEnum.DEVeloper, UserRoleEnum.SERVice, UserRoleEnum.UEXTended, UserRoleEnum.USER })
				{
					UserRoleEnum value = driver.System.Password.Cenable.State;
				}
			}
			{	// SYSTem:BASE:PASSword[:CENable]
				RsCmwBase_System_Password_Cenable.Value_Data value = new RsCmwBase_System_Password_Cenable.Value_Data();
				driver.System.Password.Cenable.Value = value;
			}
			{	// SYSTem:BASE:STICon:ENABle
				bool value = driver.System.StIcon.Enable;
				driver.System.StIcon.Enable = value;
			}
			{	// SYSTem:BASE:STICon:OPEN
				driver.System.StIcon.Open();
				driver.System.StIcon.OpenAndWait();
			}
			{	// SYSTem:BASE:STICon:CLOSe
				driver.System.StIcon.Close();
				driver.System.StIcon.CloseAndWait();
			}
			{	// SYSTem:DFPRint
				byte[] value = driver.System.DeviceFootprint.Get();				
			}
			{	// SYSTem:DFPRint
				driver.System.DeviceFootprint.Set("1");
				driver.System.DeviceFootprint.Set();
			}
			{	// SYSTem:GENerator:ALL:OFF
				driver.System.Generator.All.Off.Set();
				driver.System.Generator.All.Off.SetAndWait();
			}
			{	// SYSTem:MEASurement:ALL:OFF
				driver.System.Measurement.All.Off.Set();
				driver.System.Measurement.All.Off.SetAndWait();
			}
			{	// SYSTem:SIGNaling:ALL:OFF
				driver.System.Signaling.All.Off.Set();
				driver.System.Signaling.All.Off.SetAndWait();
			}
			{	// SOURce:BASE:ADJustment:STATe
				BaseAdjStateEnum value = driver.Source.Adjustment.State.Get();				
			}
			{	// SOURce:BASE:ADJustment:STATe
				driver.Source.Adjustment.State.Set(false);				
			}
			{	// CALibration:BASE:ALL
				RsCmwBase_Calibration.All_Data value = driver.Calibration.All;
			}
			{	// CALibration:BASE:LATest
				RsCmwBase_Calibration.GetLatest_Data value = driver.Calibration.GetLatest(TypeEnum.CALibration);
				value = driver.Calibration.GetLatest();				
			}
			{	// CALibration:BASE:ACFile
				RsCmwBase_Calibration.AcFile_Data value = driver.Calibration.AcFile;
			}
			{	// CALibration:BASE:LATest:SPECific
				RsCmwBase_Calibration_Latest.GetSpecific_Data value = driver.Calibration.Latest.GetSpecific(TypeEnum.CALibration);				
			}
			{	// CALibration:BASE:IPCR:DATE
				string value = driver.Calibration.Ipcr.Date;
			}
			{	// CALibration:BASE:IPCR:STATe
				List<int> value = driver.Calibration.Ipcr.State;
			}
			{	// CALibration:BASE:IPCR:RESult
				List<string> value = driver.Calibration.Ipcr.Result;
			}
			{	// CALibration:BASE:IPC:RESult
				RsCmwBase_Calibration_Ipc.Result_Data value = driver.Calibration.Ipc.Result;
			}
			{	// CALibration:BASE:IPC:VALues
				RsCmwBase_Calibration_Ipc.Values_Data value = driver.Calibration.Ipc.Values;
			}
			{	// CALibration:BASE:IPC:LOG
				List<string> value = driver.Calibration.Ipc.Log;
			}
			{	// INITiate:BASE:IPC
				driver.Ipc.Initiate();
				driver.Ipc.InitiateAndWait();
			}
			{	// ABORt:BASE:IPC
				driver.Ipc.Abort();
				driver.Ipc.AbortAndWait();
			}
			{	// FETCh:BASE:IPC
				ResourceStateEnum value = driver.Ipc.Fetch();				
			}
			{	// FETCh:BASE:IPC:RESult
				RsCmwBase_Ipc_Result.Fetch_Data value = driver.Ipc.Result.Fetch();				
			}
			{	// DIAGnostic:SDBM
				driver.Diagnostic.Sdbm = "1";
			}
			{	// DIAGnostic:ROUTing:CATalog
				List<string> value = driver.Diagnostic.Routing.Catalog;
			}
			{	// DIAGnostic:ROUTing:EXPert:SETup
				List<ExpertSetupEnum> value = driver.Diagnostic.Routing.Expert.Setup.Get("1");				
			}
			{	// DIAGnostic:ROUTing:EXPert:SETup
				driver.Diagnostic.Routing.Expert.Setup.Set("1", new List<ExpertSetupEnum> { ExpertSetupEnum.BBG1, ExpertSetupEnum.SUW7 });
			}
			{	// DIAGnostic:EEPRom:HEADer
				string value = driver.Diagnostic.Eeprom.Header.Get();				
			}
			{	// DIAGnostic:EEPRom:HEADer
				driver.Diagnostic.Eeprom.Header.Set("1", 1);
				driver.Diagnostic.Eeprom.Header.Set("1");
			}
			{	// DIAGnostic:EEPRom:DATA
				string value = driver.Diagnostic.Eeprom.Data.Get();				
			}
			{	// DIAGnostic:EEPRom:DATA
				driver.Diagnostic.Eeprom.Data.Set("1", 1, 1);
				driver.Diagnostic.Eeprom.Data.Set("1", 1);
			}
			{	// DIAGnostic:BGINfo:CATalog
				List<string> value = driver.Diagnostic.BgInfo.Catalog;
			}
			{	// DIAGnostic:CMWS:LEDTest
				driver.Diagnostic.SingleCmw.LedTest = false;
			}
			{	// DIAGnostic:CMW<variant>:LEDTest:TX
				driver.Diagnostic.Cmw.LedTest.Tx.Set(false, CmwVariantRepCap.Default);
				driver.Diagnostic.Cmw.LedTest.Tx.Set(false);
			}
			{	// DIAGnostic:CMW<variant>:LEDTest:RX
				driver.Diagnostic.Cmw.LedTest.Rx.Set(false, CmwVariantRepCap.Default);
				driver.Diagnostic.Cmw.LedTest.Rx.Set(false);
			}
			{	// DIAGnostic:LOG:DUMP
				driver.Diagnostic.Log.Dump.Set();
				driver.Diagnostic.Log.Dump.SetAndWait();
			}
			{	// DIAGnostic:FOOTprint:ELEMent:PROPerties
				string value = driver.Diagnostic.FootPrint.Element.GetProperties(1.0, false);				
			}
			{	// DIAGnostic:FOOTprint:ELEMent:IDS
				List<int> value = driver.Diagnostic.FootPrint.Element.Ids;
			}
			{	// DIAGnostic:FOOTprint:ELEMent:REFerences
				List<int> value = driver.Diagnostic.FootPrint.Element.GetReferences(1.0, "1");				
			}
			{	// DIAGnostic:FOOTprint:ELEMent:DATA
				RsCmwBase_Diagnostic_FootPrint_Element.GetData_Data value = driver.Diagnostic.FootPrint.Element.GetData(1.0);				
			}
			{	// DIAGnostic:FOOTprint:ELEMent:CONNection:TARGet:IDS
				string value = driver.Diagnostic.FootPrint.Element.Connection.Target.GetIds(1.0);				
			}
			{	// DIAGnostic:FOOTprint:USECase:DATA
				RsCmwBase_Diagnostic_FootPrint_UseCase.GetData_Data value = driver.Diagnostic.FootPrint.UseCase.GetData(1);				
			}
			{	// DIAGnostic:FOOTprint:USECase:IDS
				List<int> value = driver.Diagnostic.FootPrint.UseCase.Ids;
			}
			{	// DIAGnostic:FOOTprint:LI:USECases
				string value = driver.Diagnostic.FootPrint.Li.GetUsecases(1);				
			}
			{	// DIAGnostic:STATus:OPC
				RsCmwBase_Diagnostic_Status.Opc_Data value = driver.Diagnostic.Status.Opc;
			}
			{	// DIAGnostic:ERRor:QUEue:SIZE
				int value = driver.Diagnostic.Error.Queue.Size;
				driver.Diagnostic.Error.Queue.Size = value;
			}
			{	// DIAGnostic:ERRor:QUEue:LENGth
				int value = driver.Diagnostic.Error.Queue.Length;
				driver.Diagnostic.Error.Queue.Length = value;
			}
			{	// DIAGnostic:ERRor:QUEue:PUSH
				RsCmwBase_Diagnostic_Error_Queue.Push_Data value = new RsCmwBase_Diagnostic_Error_Queue.Push_Data();
				driver.Diagnostic.Error.Queue.Push = value;
			}
			{	// DIAGnostic:HELP:SYNTax
				string value = driver.Diagnostic.Help.GetSyntax("1");				
			}
			{	// DIAGnostic:HELP:SYNTax:ALL
				byte[] value = driver.Diagnostic.Help.Syntax.All;
			}
			{	// DIAGnostic:HELP:HEADers
				byte[] value = driver.Diagnostic.Help.Headers.Value;
			}
			{	// DIAGnostic:HELP:HEADers:ACCess:ENABled
				RsCmwBase_Diagnostic_Help_Headers_Access.Enabled_Data value = driver.Diagnostic.Help.Headers.Access.Enabled;
			}
			{	// DIAGnostic:HELP:HEADers:ACCess:DENied
				RsCmwBase_Diagnostic_Help_Headers_Access.Denied_Data value = driver.Diagnostic.Help.Headers.Access.Denied;
			}
			{	// DIAGnostic:ACCess:RESTore
				driver.Diagnostic.Access.Restore.Set();
				driver.Diagnostic.Access.Restore.SetAndWait();
			}
			{	// DIAGnostic:ACCess:SCENario
				driver.Diagnostic.Access.Scenario.Set(new List<bool> { true, false, true }, new List<int> { 1, 2, 3 }, new List<string> { "1", "2", "3" });
				driver.Diagnostic.Access.Scenario.Set();
			}
			{	// DIAGnostic:INSTrument:LOAD
				driver.Diagnostic.Instrument.Load("1");				
			}
			{	// DIAGnostic:INSTrument:UNLoad
				driver.Diagnostic.Instrument.Unload = "1";
			}
			{	// DIAGnostic:KREMote:TMONitor:DUMP
				byte[] value = driver.Diagnostic.Kremote.Tmonitor.GetDump(TextFormattingEnum.TXT);
				value = driver.Diagnostic.Kremote.Tmonitor.GetDump();				
			}
			{	// DIAGnostic:KREMote:TMONitor:RESet
				driver.Diagnostic.Kremote.Tmonitor.Reset();
				driver.Diagnostic.Kremote.Tmonitor.ResetAndWait();
			}
			{	// DIAGnostic:KREMote:TMONitor:STATistic
				byte[] value = driver.Diagnostic.Kremote.Tmonitor.GetStatistic(TextFormattingEnum.TXT);
				value = driver.Diagnostic.Kremote.Tmonitor.GetStatistic();				
			}
			{	// DIAGnostic:KREMote:TMONitor:TRACe
				byte[] value = driver.Diagnostic.Kremote.Tmonitor.GetTrace(TextFormattingEnum.TXT);
				value = driver.Diagnostic.Kremote.Tmonitor.GetTrace();				
			}
			{	// DIAGnostic:KREMote:TMONitor:ENABle:STATistic
				bool value = driver.Diagnostic.Kremote.Tmonitor.Enable.Statistic;
				driver.Diagnostic.Kremote.Tmonitor.Enable.Statistic = value;
			}
			{	// DIAGnostic:KREMote:TMONitor:ENABle:TIMing
				bool value = driver.Diagnostic.Kremote.Tmonitor.Enable.Timing;
				driver.Diagnostic.Kremote.Tmonitor.Enable.Timing = value;
			}
			{	// DIAGnostic:KREMote:TMONitor:ENABle:TRACe
				bool value = driver.Diagnostic.Kremote.Tmonitor.Enable.Trace;
				driver.Diagnostic.Kremote.Tmonitor.Enable.Trace = value;
			}
			{	// DIAGnostic:KREMote:TMONitor:ENABle:RPC
				bool value = driver.Diagnostic.Kremote.Tmonitor.Enable.Rpc;
				driver.Diagnostic.Kremote.Tmonitor.Enable.Rpc = value;
			}
			{	// DIAGnostic:KREMote:TMONitor:ENABle
				bool value = driver.Diagnostic.Kremote.Tmonitor.Enable.Value;
				driver.Diagnostic.Kremote.Tmonitor.Enable.Value = value;
			}
			{	// DIAGnostic:COMPass:VERSion
				string value = driver.Diagnostic.Compass.Version;
			}
			{	// DIAGnostic:COMPass:HEAPcheck
				bool value = driver.Diagnostic.Compass.HeapCheck;
				driver.Diagnostic.Compass.HeapCheck = value;
			}
			{	// DIAGnostic:COMPass:STATistics:PROCess
				string value = driver.Diagnostic.Compass.Statistics.GetProcess("1");				
			}
			{	// DIAGnostic:COMPass:DEBug:MODE
				bool value = driver.Diagnostic.Compass.Debug.Mode;
				driver.Diagnostic.Compass.Debug.Mode = value;
			}
			{	// DIAGnostic:COMPass:DBASe:RLOGging:MODE
				foreach (DiagLoggigModeEnum x in new DiagLoggigModeEnum[] { DiagLoggigModeEnum.DETailed, DiagLoggigModeEnum.OFF, DiagLoggigModeEnum.SIMPle })
				{
					driver.Diagnostic.Compass.Dbase.Rlogging.Mode = x;
					DiagLoggigModeEnum value = driver.Diagnostic.Compass.Dbase.Rlogging.Mode;
				}
			}
			{	// DIAGnostic:COMPass:DBASe:RLOGging:DEVice
				foreach (DiagLoggingDeviceEnum x in new DiagLoggingDeviceEnum[] { DiagLoggingDeviceEnum.ALL, DiagLoggingDeviceEnum.DEBug, DiagLoggingDeviceEnum.MEMory })
				{
					driver.Diagnostic.Compass.Dbase.Rlogging.Device = x;
					DiagLoggingDeviceEnum value = driver.Diagnostic.Compass.Dbase.Rlogging.Device;
				}
			}
			{	// DIAGnostic:COMPass:DBASe:RLOGging:CLEar
				driver.Diagnostic.Compass.Dbase.Rlogging.Clear();
				driver.Diagnostic.Compass.Dbase.Rlogging.ClearAndWait();
			}
			{	// DIAGnostic:COMPass:DBASe:RLOGging:PROTocol
				string value = driver.Diagnostic.Compass.Dbase.Rlogging.GetProtocol("1");				
			}
			{	// DIAGnostic:COMPass:DBASe:TALogging:PROTocol
				int value = driver.Diagnostic.Compass.Dbase.TaLogging.GetProtocol("1");				
			}
			{	// DIAGnostic:COMPass:DBASe:TALogging:CLEar
				driver.Diagnostic.Compass.Dbase.TaLogging.Clear();
				driver.Diagnostic.Compass.Dbase.TaLogging.ClearAndWait();
			}
			{	// DIAGnostic:COMPass:DBASe:TALogging:DEVice
				foreach (DiagLoggingDeviceEnum x in new DiagLoggingDeviceEnum[] { DiagLoggingDeviceEnum.ALL, DiagLoggingDeviceEnum.DEBug, DiagLoggingDeviceEnum.MEMory })
				{
					driver.Diagnostic.Compass.Dbase.TaLogging.Device = x;
					DiagLoggingDeviceEnum value = driver.Diagnostic.Compass.Dbase.TaLogging.Device;
				}
			}
			{	// DIAGnostic:COMPass:DBASe:TALogging:MODE
				DiagLoggigModeEnum value = driver.Diagnostic.Compass.Dbase.TaLogging.Mode.Get("1");				
			}
			{	// DIAGnostic:COMPass:DBASe:TALogging:MODE
				driver.Diagnostic.Compass.Dbase.TaLogging.Mode.Set("1", DiagLoggigModeEnum.DETailed);
			}
			{	// DIAGnostic:RECord:MACRo:FILE:SIZE
				int value = driver.Diagnostic.Record.Macro.File.Size;
				driver.Diagnostic.Record.Macro.File.Size = value;
			}
			{	// DIAGnostic:RECord:MACRo:FILE:FILTer
				RsCmwBase_Diagnostic_Record_Macro_File.Filter_Data value = driver.Diagnostic.Record.Macro.File.Filter;
				driver.Diagnostic.Record.Macro.File.Filter = value;
			}
			{	// DIAGnostic:PIAS:HOST
				string value = driver.Diagnostic.Pias.Host;
			}
			{	// DIAGnostic:PIAS:ID
				string value = driver.Diagnostic.Pias.Id;
			}
			{	// DIAGnostic:PIAS:SCAN
				string value = driver.Diagnostic.Pias.GetScan(1, SubnetScopeEnum.ALL, 1, new List<string> { "1", "2", "3" });				
			}
			{	// DIAGnostic:PIAS:CONNect
				int value = driver.Diagnostic.Pias.GetConnect("1", "1");				
			}
			{	// DIAGnostic:PIAS:CONNect:MULTiple
				int value = driver.Diagnostic.Pias.Connect.GetMultiple(new List<string> { "1", "2", "3" });				
			}
			{	// DIAGnostic:PRODuct:ID
				RsCmwBase_Diagnostic_Product.Id_Data value = driver.Diagnostic.Product.Id;
			}
			{	// DIAGnostic:PRODuct:DESCription
				byte[] value = driver.Diagnostic.Product.Description;
			}
			{	// DIAGnostic:PRODuct:CATalog
				string value = driver.Diagnostic.Product.Catalog;
			}
			{	// DIAGnostic:PRODuct:SELect
				string value = driver.Diagnostic.Product.Select;
				driver.Diagnostic.Product.Select = value;
			}
			{	// DIAGnostic:PRODuct:GROup
				string value = driver.Diagnostic.Product.Group;
				driver.Diagnostic.Product.Group = value;
			}
			{	// DIAGnostic:PRODuct:TIME:OPERating
				string value = driver.Diagnostic.Product.Time.Operating;
			}
			{	// DIAGnostic:PRODuct:MACaddress:STORe
				foreach (StoragePlaceEnum x in new StoragePlaceEnum[] { StoragePlaceEnum.EEPRom, StoragePlaceEnum.FILE, StoragePlaceEnum.SIM })
				{
					driver.Diagnostic.Product.MacAddress.Store = x;					
				}
			}
			{	// DIAGnostic:PRODuct:MACaddress:RESTore
				foreach (StoragePlaceEnum x in new StoragePlaceEnum[] { StoragePlaceEnum.EEPRom, StoragePlaceEnum.FILE, StoragePlaceEnum.SIM })
				{
					driver.Diagnostic.Product.MacAddress.Restore = x;					
				}
			}
			{	// DIAGnostic:PRODuct:MACaddress
				string value = driver.Diagnostic.Product.MacAddress.Value;
			}
			{	// TRIGger:BASE:EXTA:SOURce
				string value = driver.Trigger.ExtA.Source;
				driver.Trigger.ExtA.Source = value;
			}
			{	// TRIGger:BASE:EXTA:DIRection
				foreach (DirectionIoEnum x in new DirectionIoEnum[] { DirectionIoEnum.IN, DirectionIoEnum.OUT })
				{
					driver.Trigger.ExtA.Direction = x;
					DirectionIoEnum value = driver.Trigger.ExtA.Direction;
				}
			}
			{	// TRIGger:BASE:EXTA:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.ExtA.Slope = x;
					SignalSlopeEnum value = driver.Trigger.ExtA.Slope;
				}
			}
			{	// TRIGger:BASE:EXTA:CATalog:SOURce
				List<string> value = driver.Trigger.ExtA.Catalog.Source;
			}
			{	// TRIGger:BASE:EXTB:DIRection
				foreach (DirectionIoEnum x in new DirectionIoEnum[] { DirectionIoEnum.IN, DirectionIoEnum.OUT })
				{
					driver.Trigger.ExtB.Direction = x;
					DirectionIoEnum value = driver.Trigger.ExtB.Direction;
				}
			}
			{	// TRIGger:BASE:EXTB:SOURce
				string value = driver.Trigger.ExtB.Source;
				driver.Trigger.ExtB.Source = value;
			}
			{	// TRIGger:BASE:EXTB:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.ExtB.Slope = x;
					SignalSlopeEnum value = driver.Trigger.ExtB.Slope;
				}
			}
			{	// TRIGger:BASE:EXTB:CATalog:SOURce
				List<string> value = driver.Trigger.ExtB.Catalog.Source;
			}
			{	// TRIGger:BASE:UINitiated<n>:EXECute
				driver.Trigger.UserInitiated.Execute.Set(TriggerRepCap.Default);
				driver.Trigger.UserInitiated.Execute.SetAndWait(TriggerRepCap.Default);
			}
			{	// TRIGger:BASE:EOUT<n>:CATalog:SOURce
				List<string> value = driver.Trigger.Eout.Catalog.GetSource(EoutRepCap.Default);
				value = driver.Trigger.Eout.Catalog.GetSource();
			}
			{	// TRIGger:BASE:EOUT<n>:SOURce
				string value = driver.Trigger.Eout.Source.Get(EoutRepCap.Default);
				value = driver.Trigger.Eout.Source.Get();
			}
			{	// TRIGger:BASE:EOUT<n>:SOURce
				driver.Trigger.Eout.Source.Set("1", EoutRepCap.Default);
				driver.Trigger.Eout.Source.Set("1");
			}
			{	// INITiate:CMWD
				driver.Cmwd.Initiate();
				driver.Cmwd.InitiateAndWait();
			}
			{	// STOP:CMWD
				driver.Cmwd.Stop();
				driver.Cmwd.StopAndWait();
			}
			{	// ABORt:CMWD
				driver.Cmwd.Abort();
				driver.Cmwd.AbortAndWait();
			}
			{	// FETCh:CMWD
				string value = driver.Cmwd.Fetch();				
			}
			{	// FETCh:CMWD:STATe
				ResourceStateEnum value = driver.Cmwd.State.Fetch();				
			}
			{	// PROCedure:CMWD
				string value = driver.Procedure.Cmwd;
				driver.Procedure.Cmwd = value;
			}
			{	// GET:XVALues
				List<double> value = driver.Get.Xvalues;
			}
			{	// INITiate:BASE:CORRection:IFEQualizer
				driver.Correction.IfEqualizer.Initiate();
				driver.Correction.IfEqualizer.InitiateAndWait();
			}
			{	// ABORt:BASE:CORRection:IFEQualizer
				driver.Correction.IfEqualizer.Abort();
				driver.Correction.IfEqualizer.AbortAndWait();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:STATe
				ResourceStateEnum value = driver.Correction.IfEqualizer.State.Fetch();				
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:RXFilter
				List<CorrResultEnum> value = driver.Correction.IfEqualizer.Slot.RxFilter.Fetch(SlotRepCap.Default);
				value = driver.Correction.IfEqualizer.Slot.RxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:TXFilter
				List<CorrResultEnum> value = driver.Correction.IfEqualizer.Slot.TxFilter.Fetch(SlotRepCap.Default);
				value = driver.Correction.IfEqualizer.Slot.TxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:MAGNitude:CORRected:SLOT{slotCmdVal}:TXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Magnitude.Corrected.Slot.TxFilter.Fetch(SlotRepCap.Default, TxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Magnitude.Corrected.Slot.TxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:MAGNitude:CORRected:SLOT{slotCmdVal}:RXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Magnitude.Corrected.Slot.RxFilter.Fetch(SlotRepCap.Default, RxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Magnitude.Corrected.Slot.RxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:MAGNitude:UNCorrected:SLOT{slotCmdVal}:TXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Magnitude.Uncorrected.Slot.TxFilter.Fetch(SlotRepCap.Default, TxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Magnitude.Uncorrected.Slot.TxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:MAGNitude:UNCorrected:SLOT{slotCmdVal}:RXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Magnitude.Uncorrected.Slot.RxFilter.Fetch(SlotRepCap.Default, RxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Magnitude.Uncorrected.Slot.RxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:GDELay:CORRected:SLOT{slotCmdVal}:TXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Gdelay.Corrected.Slot.TxFilter.Fetch(SlotRepCap.Default, TxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Gdelay.Corrected.Slot.TxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:GDELay:CORRected:SLOT{slotCmdVal}:RXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Gdelay.Corrected.Slot.RxFilter.Fetch(SlotRepCap.Default, RxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Gdelay.Corrected.Slot.RxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:GDELay:UNCorrected:SLOT{slotCmdVal}:TXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Gdelay.Uncorrected.Slot.TxFilter.Fetch(SlotRepCap.Default, TxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Gdelay.Uncorrected.Slot.TxFilter.Fetch();
			}
			{	// FETCh:BASE:CORRection:IFEQualizer:TRACe:GDELay:UNCorrected:SLOT{slotCmdVal}:RXFilter<Filter>
				List<double> value = driver.Correction.IfEqualizer.Trace.Gdelay.Uncorrected.Slot.RxFilter.Fetch(SlotRepCap.Default, RxFilterRepCap.Default);
				value = driver.Correction.IfEqualizer.Trace.Gdelay.Uncorrected.Slot.RxFilter.Fetch();
			}
			{	// CATalog:BASE:CORRection:IFEQualizer:SNAMe
				List<string> value = driver.Catalog.Correction.IfEqualizer.Sname;
			}
			{	// CATalog:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:RXFilter
				List<string> value = driver.Catalog.Correction.IfEqualizer.Slot.GetRxFilter(SlotRepCap.Default);
				value = driver.Catalog.Correction.IfEqualizer.Slot.GetRxFilter();
			}
			{	// CATalog:BASE:CORRection:IFEQualizer:SLOT{slotCmdVal}:TXFilter
				List<string> value = driver.Catalog.Correction.IfEqualizer.Slot.GetTxFilter(SlotRepCap.Default);
				value = driver.Catalog.Correction.IfEqualizer.Slot.GetTxFilter();
			}
			{	// WRITe:EEPRom:DATA
				RsCmwBase_Write_Eeprom.Data_Data value = new RsCmwBase_Write_Eeprom.Data_Data();
				driver.Write.Eeprom.Data = value;
			}
			{	// FETCh:FWUPdate:VERSions
				List<string> value = driver.FirmwareUpdate.Versions;
			}
			{	// *DMC
				string value = driver.MacroCreate.Get("1");				
			}
			{	// *DMC
				driver.MacroCreate.Set("1", "1");
			}
			{	// *TRG
				driver.TriggerInvoke.Set();
				driver.TriggerInvoke.SetAndWait();
			}
			{	// *GWAI
				driver.GlobalWait.Set();
				driver.GlobalWait.SetAndWait();
			}
			{	// *GCLS
				driver.GlobalClearStatus.Set();
				driver.GlobalClearStatus.SetAndWait();
			}
			{	// STATus:PRESet
				driver.Status.Preset();
				driver.Status.PresetAndWait();
			}
			{	// STATus:QUEue[:NEXT]
				RsCmwBase_Status_Queue.Next_Data value = driver.Status.Queue.Next;
			}
			{	// STATus:OPERation[:EVENt]
				int value = driver.Status.Operation.Event;
			}
			{	// STATus:OPERation:CONDition
				int value = driver.Status.Operation.Condition;
			}
			{	// STATus:OPERation:ENABle
				int value = driver.Status.Operation.Enable;
				driver.Status.Operation.Enable = value;
			}
			{	// STATus:OPERation:PTRansition
				int value = driver.Status.Operation.Ptransition;
				driver.Status.Operation.Ptransition = value;
			}
			{	// STATus:OPERation:NTRansition
				int value = driver.Status.Operation.Ntransition;
				driver.Status.Operation.Ntransition = value;
			}
			{	// STATus:OPERation:BIT<bitno>:CONDition
				bool value = driver.Status.Operation.Bit.GetCondition(BitNrRepCap.Default);
				value = driver.Status.Operation.Bit.GetCondition();
			}
			{	// STATus:OPERation:BIT<bitno>[:EVENt]
				bool value = driver.Status.Operation.Bit.GetEvent(BitNrRepCap.Default);
				value = driver.Status.Operation.Bit.GetEvent();
			}
			{	// STATus:OPERation:BIT<bitno>:ENABle
				double value = driver.Status.Operation.Bit.Enable.Get(BitNrRepCap.Default);
				value = driver.Status.Operation.Bit.Enable.Get();
			}
			{	// STATus:OPERation:BIT<bitno>:ENABle
				driver.Status.Operation.Bit.Enable.Set(1.0, BitNrRepCap.Default);
				driver.Status.Operation.Bit.Enable.Set(1.0);
			}
			{	// STATus:OPERation:BIT<bitno>:NTRansition
				bool value = driver.Status.Operation.Bit.Ntransition.Get(BitNrRepCap.Default);
				value = driver.Status.Operation.Bit.Ntransition.Get();
			}
			{	// STATus:OPERation:BIT<bitno>:NTRansition
				driver.Status.Operation.Bit.Ntransition.Set(false, BitNrRepCap.Default);
				driver.Status.Operation.Bit.Ntransition.Set(false);
			}
			{	// STATus:OPERation:BIT<bitno>:PTRansition
				bool value = driver.Status.Operation.Bit.Ptransition.Get(BitNrRepCap.Default);
				value = driver.Status.Operation.Bit.Ptransition.Get();
			}
			{	// STATus:OPERation:BIT<bitno>:PTRansition
				driver.Status.Operation.Bit.Ptransition.Set(false, BitNrRepCap.Default);
				driver.Status.Operation.Bit.Ptransition.Set(false);
			}
			{	// STATus:QUEStionable[:EVENt]
				int value = driver.Status.Questionable.Event;
			}
			{	// STATus:QUEStionable:CONDition
				int value = driver.Status.Questionable.Condition;
			}
			{	// STATus:QUEStionable:ENABle
				int value = driver.Status.Questionable.Enable;
				driver.Status.Questionable.Enable = value;
			}
			{	// STATus:QUEStionable:PTRansition
				int value = driver.Status.Questionable.Ptransition;
				driver.Status.Questionable.Ptransition = value;
			}
			{	// STATus:QUEStionable:NTRansition
				int value = driver.Status.Questionable.Ntransition;
				driver.Status.Questionable.Ntransition = value;
			}
			{	// STATus:QUEStionable:BIT<bitno>:CONDition
				bool value = driver.Status.Questionable.Bit.GetCondition(BitNrRepCap.Default);
				value = driver.Status.Questionable.Bit.GetCondition();
			}
			{	// STATus:QUEStionable:BIT<bitno>[:EVENt]
				bool value = driver.Status.Questionable.Bit.GetEvent(BitNrRepCap.Default);
				value = driver.Status.Questionable.Bit.GetEvent();
			}
			{	// STATus:QUEStionable:BIT<bitno>:ENABle
				bool value = driver.Status.Questionable.Bit.Enable.Get(BitNrRepCap.Default);
				value = driver.Status.Questionable.Bit.Enable.Get();
			}
			{	// STATus:QUEStionable:BIT<bitno>:ENABle
				driver.Status.Questionable.Bit.Enable.Set(false, BitNrRepCap.Default);
				driver.Status.Questionable.Bit.Enable.Set(false);
			}
			{	// STATus:QUEStionable:BIT<bitno>:NTRansition
				bool value = driver.Status.Questionable.Bit.Ntransition.Get(BitNrRepCap.Default);
				value = driver.Status.Questionable.Bit.Ntransition.Get();
			}
			{	// STATus:QUEStionable:BIT<bitno>:NTRansition
				driver.Status.Questionable.Bit.Ntransition.Set(false, BitNrRepCap.Default);
				driver.Status.Questionable.Bit.Ntransition.Set(false);
			}
			{	// STATus:QUEStionable:BIT<bitno>:PTRansition
				bool value = driver.Status.Questionable.Bit.Ptransition.Get(BitNrRepCap.Default);
				value = driver.Status.Questionable.Bit.Ptransition.Get();
			}
			{	// STATus:QUEStionable:BIT<bitno>:PTRansition
				driver.Status.Questionable.Bit.Ptransition.Set(false, BitNrRepCap.Default);
				driver.Status.Questionable.Bit.Ptransition.Set(false);
			}
			{	// STATus:CONDition:BITS:CATaloge
				List<string> value = driver.Status.Condition.Bits.GetCataloge("1", ExpressionModeEnum.REGex);
				value = driver.Status.Condition.Bits.GetCataloge();				
			}
			{	// STATus:CONDition:BITS:ALL
				List<string> value = driver.Status.Condition.Bits.GetAll("1", ExpressionModeEnum.REGex);
				value = driver.Status.Condition.Bits.GetAll();				
			}
			{	// STATus:CONDition:BITS:COUNt
				int value = driver.Status.Condition.Bits.GetCount("1", ExpressionModeEnum.REGex);
				value = driver.Status.Condition.Bits.GetCount();				
			}
			{	// STATus:EVENt:BITS:ALL
				List<string> value = driver.Status.Event.Bits.GetAll("1", ExpressionModeEnum.REGex);
				value = driver.Status.Event.Bits.GetAll();				
			}
			{	// STATus:EVENt:BITS:COUNt
				int value = driver.Status.Event.Bits.GetCount("1", ExpressionModeEnum.REGex);
				value = driver.Status.Event.Bits.GetCount();				
			}
			{	// STATus:EVENt:BITS:NEXT
				string value = driver.Status.Event.Bits.GetNext("1", ExpressionModeEnum.REGex);
				value = driver.Status.Event.Bits.GetNext();				
			}
			{	// STATus:EVENt:BITS:CLEar
				driver.Status.Event.Bits.Clear("1", ExpressionModeEnum.REGex);
				driver.Status.Event.Bits.Clear();
			}
			{	// STATus:MEASurement:CONDition:OFF
				string value = driver.Status.Measurement.Condition.GetOff("1", ExpressionModeEnum.REGex);
				value = driver.Status.Measurement.Condition.GetOff();				
			}
			{	// STATus:MEASurement:CONDition:QUED
				string value = driver.Status.Measurement.Condition.GetQued("1", ExpressionModeEnum.REGex);
				value = driver.Status.Measurement.Condition.GetQued();				
			}
			{	// STATus:MEASurement:CONDition:RUN
				string value = driver.Status.Measurement.Condition.GetRun("1", ExpressionModeEnum.REGex);
				value = driver.Status.Measurement.Condition.GetRun();				
			}
			{	// STATus:MEASurement:CONDition:RDY
				string value = driver.Status.Measurement.Condition.GetRdy("1", ExpressionModeEnum.REGex);
				value = driver.Status.Measurement.Condition.GetRdy();				
			}
			{	// STATus:MEASurement:CONDition:SDReached
				string value = driver.Status.Measurement.Condition.GetSdReached("1", ExpressionModeEnum.REGex);
				value = driver.Status.Measurement.Condition.GetSdReached();				
			}
			{	// STATus:GENerator:CONDition:OFF
				string value = driver.Status.Generator.Condition.GetOff("1", ExpressionModeEnum.REGex);
				value = driver.Status.Generator.Condition.GetOff();				
			}
			{	// STATus:GENerator:CONDition:PENDing
				string value = driver.Status.Generator.Condition.GetPending("1", ExpressionModeEnum.REGex);
				value = driver.Status.Generator.Condition.GetPending();				
			}
			{	// STATus:GENerator:CONDition:ON
				string value = driver.Status.Generator.Condition.GetOn("1", ExpressionModeEnum.REGex);
				value = driver.Status.Generator.Condition.GetOn();				
			}
			{	// INSTrument:NSELect
				int value = driver.Instrument.Nselect;
				driver.Instrument.Nselect = value;
			}
			{	// INSTrument[:SELect]:DSTRategy
				RsCmwBase_Instrument_Select.Dstrategy_Data value = driver.Instrument.Select.Dstrategy;
				driver.Instrument.Select.Dstrategy = value;
			}
			{	// INSTrument[:SELect]
				string value = driver.Instrument.Select.Value;
				driver.Instrument.Select.Value = value;
			}
			{	// INSTrument:DISPlay:CAT
				string value = driver.Instrument.Display.Cat;
			}
			{	// INSTrument:DISPlay:MODE
				foreach (DisplayModeEnum x in new DisplayModeEnum[] { DisplayModeEnum.AUTomatic, DisplayModeEnum.MANual })
				{
					driver.Instrument.Display.Mode = x;
					DisplayModeEnum value = driver.Instrument.Display.Mode;
				}
			}
			{	// INSTrument:DISPlay:OPEN
				driver.Instrument.Display.Open("1");				
			}
			{	// INSTrument:DISPlay:CLOSe
				driver.Instrument.Display.Close("1");				
			}
			{	// INSTrument:DISPlay
				int value = driver.Instrument.Display.Value;
				driver.Instrument.Display.Value = value;
			}
			{	// DISPlay:FORMat
				string value = driver.Display.Format;
				driver.Display.Format = value;
			}
			{	// DISPlay[:WINDow<1-n>]:SELect
				driver.Display.Window.Select.Set(WindowRepCap.Default);
				driver.Display.Window.Select.SetAndWait(WindowRepCap.Default);
			}
			{	// FORMat:BASE[:DATA]
				RsCmwBase_Format.Data_Data value = driver.Format.Data;
				driver.Format.Data = value;
			}
			{	// FORMat:BASE:BORDer
				foreach (ByteOrderEnum x in new ByteOrderEnum[] { ByteOrderEnum.NORMal, ByteOrderEnum.SWAPped })
				{
					driver.Format.Border = x;
					ByteOrderEnum value = driver.Format.Border;
				}
			}
			{	// FORMat:BASE:DINTerchange
				bool value = driver.Format.Dinterchange;
				driver.Format.Dinterchange = value;
			}
			{	// FORMat:BASE:SREGister
				foreach (StatRegFormatEnum x in new StatRegFormatEnum[] { StatRegFormatEnum.ASCii, StatRegFormatEnum.BINary, StatRegFormatEnum.HEXadecimal, StatRegFormatEnum.OCTal })
				{
					driver.Format.Sregister = x;
					StatRegFormatEnum value = driver.Format.Sregister;
				}
			}
			{	// UNIT:CONDuctance
				foreach (DefaultUnitConductanceEnum x in new DefaultUnitConductanceEnum[] { DefaultUnitConductanceEnum.ASIE, DefaultUnitConductanceEnum.EXSie, DefaultUnitConductanceEnum.FSIE, DefaultUnitConductanceEnum.GSIE, DefaultUnitConductanceEnum.KSIE, DefaultUnitConductanceEnum.MISie, DefaultUnitConductanceEnum.MSIE, DefaultUnitConductanceEnum.NSIE, DefaultUnitConductanceEnum.PESie, DefaultUnitConductanceEnum.PSIE, DefaultUnitConductanceEnum.SIE, DefaultUnitConductanceEnum.TSIE, DefaultUnitConductanceEnum.USIE })
				{
					driver.Unit.Conductance = x;
					DefaultUnitConductanceEnum value = driver.Unit.Conductance;
				}
			}
			{	// UNIT:CHARge
				foreach (DefaultUnitChargeEnum x in new DefaultUnitChargeEnum[] { DefaultUnitChargeEnum.AC, DefaultUnitChargeEnum.C, DefaultUnitChargeEnum.EXC, DefaultUnitChargeEnum.FC, DefaultUnitChargeEnum.GC, DefaultUnitChargeEnum.KC, DefaultUnitChargeEnum.MC, DefaultUnitChargeEnum.MIC, DefaultUnitChargeEnum.NC, DefaultUnitChargeEnum.PC, DefaultUnitChargeEnum.PEC, DefaultUnitChargeEnum.TC, DefaultUnitChargeEnum.UC })
				{
					driver.Unit.Charge = x;
					DefaultUnitChargeEnum value = driver.Unit.Charge;
				}
			}
			{	// UNIT:CAPacity
				foreach (DefaultUnitCapacityEnum x in new DefaultUnitCapacityEnum[] { DefaultUnitCapacityEnum.AF, DefaultUnitCapacityEnum.EXF, DefaultUnitCapacityEnum.F, DefaultUnitCapacityEnum.FF, DefaultUnitCapacityEnum.GF, DefaultUnitCapacityEnum.KF, DefaultUnitCapacityEnum.MF, DefaultUnitCapacityEnum.MIF, DefaultUnitCapacityEnum.NF, DefaultUnitCapacityEnum.PEF, DefaultUnitCapacityEnum.PF, DefaultUnitCapacityEnum.TF, DefaultUnitCapacityEnum.UF })
				{
					driver.Unit.Capacity = x;
					DefaultUnitCapacityEnum value = driver.Unit.Capacity;
				}
			}
			{	// UNIT:ENERgy
				foreach (DefaultUnitEnergyEnum x in new DefaultUnitEnergyEnum[] { DefaultUnitEnergyEnum.AJ, DefaultUnitEnergyEnum.EXJ, DefaultUnitEnergyEnum.FJ, DefaultUnitEnergyEnum.GJ, DefaultUnitEnergyEnum.J, DefaultUnitEnergyEnum.KJ, DefaultUnitEnergyEnum.MIJ, DefaultUnitEnergyEnum.MJ, DefaultUnitEnergyEnum.NJ, DefaultUnitEnergyEnum.PEJ, DefaultUnitEnergyEnum.PJ, DefaultUnitEnergyEnum.TJ, DefaultUnitEnergyEnum.UJ })
				{
					driver.Unit.Energy = x;
					DefaultUnitEnergyEnum value = driver.Unit.Energy;
				}
			}
			{	// UNIT:FREQuency
				foreach (DefaultUnitFrequencyEnum x in new DefaultUnitFrequencyEnum[] { DefaultUnitFrequencyEnum.AHZ, DefaultUnitFrequencyEnum.EXHZ, DefaultUnitFrequencyEnum.FHZ, DefaultUnitFrequencyEnum.GHZ, DefaultUnitFrequencyEnum.HZ, DefaultUnitFrequencyEnum.KHZ, DefaultUnitFrequencyEnum.MHZ, DefaultUnitFrequencyEnum.MIHZ, DefaultUnitFrequencyEnum.NHZ, DefaultUnitFrequencyEnum.PEHZ, DefaultUnitFrequencyEnum.PHZ, DefaultUnitFrequencyEnum.THZ, DefaultUnitFrequencyEnum.UHZ })
				{
					driver.Unit.Frequency = x;
					DefaultUnitFrequencyEnum value = driver.Unit.Frequency;
				}
			}
			{	// UNIT:RESistor
				foreach (DefaultUnitResistorEnum x in new DefaultUnitResistorEnum[] { DefaultUnitResistorEnum.AOHM, DefaultUnitResistorEnum.EXOHm, DefaultUnitResistorEnum.FOHM, DefaultUnitResistorEnum.GOHM, DefaultUnitResistorEnum.KOHM, DefaultUnitResistorEnum.MIOHm, DefaultUnitResistorEnum.MOHM, DefaultUnitResistorEnum.NOHM, DefaultUnitResistorEnum.OHM, DefaultUnitResistorEnum.PEOHm, DefaultUnitResistorEnum.POHM, DefaultUnitResistorEnum.TOHM, DefaultUnitResistorEnum.UOHM })
				{
					driver.Unit.Resistor = x;
					DefaultUnitResistorEnum value = driver.Unit.Resistor;
				}
			}
			{	// UNIT:VOLTage
				foreach (DefaultUnitVoltageEnum x in new DefaultUnitVoltageEnum[] { DefaultUnitVoltageEnum.AV, DefaultUnitVoltageEnum.DBMV, DefaultUnitVoltageEnum.DBNV, DefaultUnitVoltageEnum.DBPV, DefaultUnitVoltageEnum.DBUV, DefaultUnitVoltageEnum.DBV, DefaultUnitVoltageEnum.EXV, DefaultUnitVoltageEnum.FV, DefaultUnitVoltageEnum.GV, DefaultUnitVoltageEnum.KV, DefaultUnitVoltageEnum.MAV, DefaultUnitVoltageEnum.MV, DefaultUnitVoltageEnum.NV, DefaultUnitVoltageEnum.PEV, DefaultUnitVoltageEnum.PV, DefaultUnitVoltageEnum.TV, DefaultUnitVoltageEnum.UV, DefaultUnitVoltageEnum.V })
				{
					driver.Unit.Voltage = x;
					DefaultUnitVoltageEnum value = driver.Unit.Voltage;
				}
			}
			{	// UNIT:ANGLe
				foreach (DefaultUnitAngleEnum x in new DefaultUnitAngleEnum[] { DefaultUnitAngleEnum.DEG, DefaultUnitAngleEnum.GRAD, DefaultUnitAngleEnum.RAD })
				{
					driver.Unit.Angle = x;
					DefaultUnitAngleEnum value = driver.Unit.Angle;
				}
			}
			{	// UNIT:LENGth
				foreach (DefaultUnitLenghtEnum x in new DefaultUnitLenghtEnum[] { DefaultUnitLenghtEnum.AM, DefaultUnitLenghtEnum.EXM, DefaultUnitLenghtEnum.FM, DefaultUnitLenghtEnum.GM, DefaultUnitLenghtEnum.KM, DefaultUnitLenghtEnum.M, DefaultUnitLenghtEnum.MAM, DefaultUnitLenghtEnum.MM, DefaultUnitLenghtEnum.NM, DefaultUnitLenghtEnum.PEM, DefaultUnitLenghtEnum.PM, DefaultUnitLenghtEnum.TM, DefaultUnitLenghtEnum.UM })
				{
					driver.Unit.Length = x;
					DefaultUnitLenghtEnum value = driver.Unit.Length;
				}
			}
			{	// UNIT:CURRent
				foreach (DefaultUnitCurrentEnum x in new DefaultUnitCurrentEnum[] { DefaultUnitCurrentEnum.A, DefaultUnitCurrentEnum.AA, DefaultUnitCurrentEnum.DBA, DefaultUnitCurrentEnum.DBMA, DefaultUnitCurrentEnum.DBNA, DefaultUnitCurrentEnum.DBPA, DefaultUnitCurrentEnum.DBUA, DefaultUnitCurrentEnum.EXA, DefaultUnitCurrentEnum.FA, DefaultUnitCurrentEnum.GA, DefaultUnitCurrentEnum.KA, DefaultUnitCurrentEnum.MA, DefaultUnitCurrentEnum.MAA, DefaultUnitCurrentEnum.NA, DefaultUnitCurrentEnum.PA, DefaultUnitCurrentEnum.PEA, DefaultUnitCurrentEnum.TA, DefaultUnitCurrentEnum.UA })
				{
					driver.Unit.Current = x;
					DefaultUnitCurrentEnum value = driver.Unit.Current;
				}
			}
			{	// UNIT:POWer
				foreach (DefaultUnitPowerEnum x in new DefaultUnitPowerEnum[] { DefaultUnitPowerEnum.AW, DefaultUnitPowerEnum.DBC, DefaultUnitPowerEnum.DBMW, DefaultUnitPowerEnum.DBNW, DefaultUnitPowerEnum.DBPW, DefaultUnitPowerEnum.DBUW, DefaultUnitPowerEnum.DBW, DefaultUnitPowerEnum.EXW, DefaultUnitPowerEnum.FW, DefaultUnitPowerEnum.GW, DefaultUnitPowerEnum.KW, DefaultUnitPowerEnum.MIW, DefaultUnitPowerEnum.MW, DefaultUnitPowerEnum.NW, DefaultUnitPowerEnum.PEW, DefaultUnitPowerEnum.PW, DefaultUnitPowerEnum.TW, DefaultUnitPowerEnum.UW, DefaultUnitPowerEnum.W })
				{
					driver.Unit.Power = x;
					DefaultUnitPowerEnum value = driver.Unit.Power;
				}
			}
			{	// UNIT:TEMPerature
				foreach (DefaultUnitTemperatureEnum x in new DefaultUnitTemperatureEnum[] { DefaultUnitTemperatureEnum.C, DefaultUnitTemperatureEnum.CEL, DefaultUnitTemperatureEnum.F, DefaultUnitTemperatureEnum.FAR, DefaultUnitTemperatureEnum.K, DefaultUnitTemperatureEnum.KEL })
				{
					driver.Unit.Temperature = x;
					DefaultUnitTemperatureEnum value = driver.Unit.Temperature;
				}
			}
			{	// UNIT:TIME
				foreach (DefaultUnitTimeEnum x in new DefaultUnitTimeEnum[] { DefaultUnitTimeEnum.AS, DefaultUnitTimeEnum.EXS, DefaultUnitTimeEnum.FS, DefaultUnitTimeEnum.GS, DefaultUnitTimeEnum.H, DefaultUnitTimeEnum.HOUR, DefaultUnitTimeEnum.KS, DefaultUnitTimeEnum.M, DefaultUnitTimeEnum.MAS, DefaultUnitTimeEnum.MIN, DefaultUnitTimeEnum.MS, DefaultUnitTimeEnum.NS, DefaultUnitTimeEnum.PES, DefaultUnitTimeEnum.PS, DefaultUnitTimeEnum.S, DefaultUnitTimeEnum.SEC, DefaultUnitTimeEnum.TS, DefaultUnitTimeEnum.US })
				{
					driver.Unit.Time = x;
					DefaultUnitTimeEnum value = driver.Unit.Time;
				}
			}
			{	// *GTL
				driver.GoToLocal.Set();
				driver.GoToLocal.SetAndWait();
			}
			{	// STARt:BASE:BUFFer
				driver.Buffer.Start("1");				
			}
			{	// STOP:BASE:BUFFer
				driver.Buffer.Stop();
				driver.Buffer.StopAndWait();
			}
			{	// CONTinue:BASE:BUFFer
				driver.Buffer.Continue("1");				
			}
			{	// DELete:BASE:BUFFer
				driver.Buffer.Delete("1");				
			}
			{	// CLEar:BASE:BUFFer
				driver.Buffer.Clear("1");				
			}
			{	// FETCh:BASE:BUFFer
				string value = driver.Buffer.Fetch("1", 1);				
			}
			{	// FETCh:BASE:BUFFer:LINecount
				int value = driver.Buffer.LineCount.Fetch("1");				
			}
			{	// TRACe:REMote:MODE:DISPlay:CLEar
				driver.Trace.Remote.Mode.Display.Clear();
				driver.Trace.Remote.Mode.Display.ClearAndWait();
			}
			{	// TRACe:REMote:MODE:DISPlay:ENABle
				foreach (RemoteTraceEnableEnum x in new RemoteTraceEnableEnum[] { RemoteTraceEnableEnum.ANALysis, RemoteTraceEnableEnum.LIVE, RemoteTraceEnableEnum.OFF, RemoteTraceEnableEnum.ON })
				{
					driver.Trace.Remote.Mode.Display.Enable = x;
					RemoteTraceEnableEnum value = driver.Trace.Remote.Mode.Display.Enable;
				}
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:DEXecution:DURation
				bool value = driver.Trace.Remote.Mode.File.Dexecution.Duration.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Dexecution.Duration.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:DEXecution:DURation
				driver.Trace.Remote.Mode.File.Dexecution.Duration.Set(false, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Dexecution.Duration.Set(false);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:STOPmode
				RemoteTraceStopModeEnum value = driver.Trace.Remote.Mode.File.StopMode.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.StopMode.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:STOPmode
				foreach (RemoteTraceStopModeEnum x in new RemoteTraceStopModeEnum[] { RemoteTraceStopModeEnum.AUTO, RemoteTraceStopModeEnum.BUFFerfull, RemoteTraceStopModeEnum.ERRor, RemoteTraceStopModeEnum.EXPLicit })
				{
					driver.Trace.Remote.Mode.File.StopMode.Set(x);
					driver.Trace.Remote.Mode.File.StopMode.Set(x, FileNrRepCap.Default);
				}
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:STARtmode
				RemoteTraceStartModeEnum value = driver.Trace.Remote.Mode.File.StartMode.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.StartMode.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:STARtmode
				foreach (RemoteTraceStartModeEnum x in new RemoteTraceStartModeEnum[] { RemoteTraceStartModeEnum.AUTO, RemoteTraceStartModeEnum.EXPLicit })
				{
					driver.Trace.Remote.Mode.File.StartMode.Set(x);
					driver.Trace.Remote.Mode.File.StartMode.Set(x, FileNrRepCap.Default);
				}
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:NAME
				string value = driver.Trace.Remote.Mode.File.Name.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Name.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:NAME
				driver.Trace.Remote.Mode.File.Name.Set("1", FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Name.Set("1");
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FORMat
				RemoteTraceFileFormatEnum value = driver.Trace.Remote.Mode.File.Format.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Format.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FORMat
				foreach (RemoteTraceFileFormatEnum x in new RemoteTraceFileFormatEnum[] { RemoteTraceFileFormatEnum.ASCii, RemoteTraceFileFormatEnum.XML })
				{
					driver.Trace.Remote.Mode.File.Format.Set(x);
					driver.Trace.Remote.Mode.File.Format.Set(x, FileNrRepCap.Default);
				}
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:SIZE
				int value = driver.Trace.Remote.Mode.File.Size.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Size.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:SIZE
				driver.Trace.Remote.Mode.File.Size.Set(1, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Size.Set(1);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:RPC
				bool value = driver.Trace.Remote.Mode.File.Rpc.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Rpc.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:RPC
				driver.Trace.Remote.Mode.File.Rpc.Set(false, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Rpc.Set(false);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FUNCtions
				bool value = driver.Trace.Remote.Mode.File.Functions.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Functions.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FUNCtions
				driver.Trace.Remote.Mode.File.Functions.Set(false, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Functions.Set(false);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:PARSer
				bool value = driver.Trace.Remote.Mode.File.Parser.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Parser.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:PARSer
				driver.Trace.Remote.Mode.File.Parser.Set(false, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Parser.Set(false);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FILTer
				RsCmwBase_Trace_Remote_Mode_File_Filter.Filter_Data value = driver.Trace.Remote.Mode.File.Filter.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Filter.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:FILTer
				RsCmwBase_Trace_Remote_Mode_File_Filter.Filter_Data value = new RsCmwBase_Trace_Remote_Mode_File_Filter.Filter_Data();
				driver.Trace.Remote.Mode.File.Filter.Set(value, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Filter.Set(value);
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:ENABle
				bool value = driver.Trace.Remote.Mode.File.Enable.Get(FileNrRepCap.Default);
				value = driver.Trace.Remote.Mode.File.Enable.Get();
			}
			{	// TRACe:REMote:MODE:FILE<instrument>:ENABle
				driver.Trace.Remote.Mode.File.Enable.Set(false, FileNrRepCap.Default);
				driver.Trace.Remote.Mode.File.Enable.Set(false);
			}
			{	// HCOPy:DATA
				byte[] value = driver.HardCopy.Data;
			}
			{	// HCOPy:FILE
				driver.HardCopy.File = "1";
			}
			{	// HCOPy:DEVice:FORMat
				foreach (ScreenshotFormatEnum x in new ScreenshotFormatEnum[] { ScreenshotFormatEnum.BMP, ScreenshotFormatEnum.JPG, ScreenshotFormatEnum.PNG })
				{
					driver.HardCopy.Device.Format = x;
					ScreenshotFormatEnum value = driver.HardCopy.Device.Format;
				}
			}
			{	// HCOPy:INTerior:DATA
				byte[] value = driver.HardCopy.Interior.Data;
			}
			{	// HCOPy:INTerior:FILE
				driver.HardCopy.Interior.File = "1";
			}
			{	// *SAV
				driver.SaveState.Set(1.0);				
			}
			{	// *RCL
				driver.RecallState.Set(1.0);				
			}
			{	// MMEMory:CATalog
				RsCmwBase_MassMemory.GetCatalog_Data value = driver.MassMemory.GetCatalog("1", CatalogFormatEnum.ALL);
				value = driver.MassMemory.GetCatalog("1");				
			}
			{	// MMEMory:COPY
				driver.MassMemory.Copy("1", "1");
				driver.MassMemory.Copy("1");
			}
			{	// MMEMory:DCATalog
				List<string> value = driver.MassMemory.GetDcatalog("1");
				value = driver.MassMemory.GetDcatalog();				
			}
			{	// MMEMory:DELete
				driver.MassMemory.Delete("1");				
			}
			{	// MMEMory:DRIVes
				List<string> value = driver.MassMemory.Drives;
			}
			{	// MMEMory:MDIRectory
				driver.MassMemory.MakeDirectory("1");				
			}
			{	// MMEMory:MOVE
				driver.MassMemory.Move("1", "1");
			}
			{	// MMEMory:MSIS
				string value = driver.MassMemory.StoreUnit;
				driver.MassMemory.StoreUnit = value;
			}
			{	// MMEMory:RDIRectory
				driver.MassMemory.DeleteDirectory("1");				
			}
			{	// MMEMory:SAV
				driver.MassMemory.Save("1", "1");
				driver.MassMemory.Save("1");
			}
			{	// MMEMory:RCL
				driver.MassMemory.Recall("1", "1");
				driver.MassMemory.Recall("1");
			}
			{	// MMEMory:ALIases
				RsCmwBase_MassMemory.Aliases_Data value = driver.MassMemory.Aliases;
			}
			{	// MMEMory:LOAD:MACRo
				RsCmwBase_MassMemory_Load.Macro_Data value = new RsCmwBase_MassMemory_Load.Macro_Data();
				driver.MassMemory.Load.Macro = value;
			}
			{	// MMEMory:LOAD:STATe
				RsCmwBase_MassMemory_Load.State_Data value = new RsCmwBase_MassMemory_Load.State_Data();
				driver.MassMemory.Load.State = value;
			}
			{	// MMEMory:LOAD:ITEM
				RsCmwBase_MassMemory_Load.Item_Data value = new RsCmwBase_MassMemory_Load.Item_Data();
				driver.MassMemory.Load.Item = value;
			}
			{	// MMEMory:STORe:MACRo
				RsCmwBase_MassMemory_Store.Macro_Data value = new RsCmwBase_MassMemory_Store.Macro_Data();
				driver.MassMemory.Store.Macro = value;
			}
			{	// MMEMory:STORe:STATe
				RsCmwBase_MassMemory_Store.State_Data value = new RsCmwBase_MassMemory_Store.State_Data();
				driver.MassMemory.Store.State = value;
			}
			{	// MMEMory:STORe:ITEM
				RsCmwBase_MassMemory_Store.Item_Data value = new RsCmwBase_MassMemory_Store.Item_Data();
				driver.MassMemory.Store.Item = value;
			}
			{	// MMEMory:ATTRibute
				List<string> value = driver.MassMemory.Attribute.Get("1");				
			}
			{	// MMEMory:ATTRibute
				driver.MassMemory.Attribute.Set("1", "1");
			}
			{	// MMEMory:CATalog:LENGth
				int value = driver.MassMemory.Catalog.GetLength("1");
				value = driver.MassMemory.Catalog.GetLength();				
			}
			{	// MMEMory:CDIRectory
				string value = driver.MassMemory.CurrentDirectory.Get("1");
				value = driver.MassMemory.CurrentDirectory.Get();				
			}
			{	// MMEMory:CDIRectory
				driver.MassMemory.CurrentDirectory.Set("1");
				driver.MassMemory.CurrentDirectory.Set();
			}
			{	// MMEMory:DCATalog:LENGth
				int value = driver.MassMemory.Dcatalog.GetLength("1");
				value = driver.MassMemory.Dcatalog.GetLength();				
			}
		}
	}
}