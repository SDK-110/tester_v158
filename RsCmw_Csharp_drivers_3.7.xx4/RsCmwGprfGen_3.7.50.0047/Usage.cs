using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwGprfGen;

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
			RsCmwGprfGen driver = new RsCmwGprfGen("TCPIP::localhost::INSTR", true, true);
			{	// SOURce:GPRF:GENerator<Instance>:BBMode
				foreach (BasebandModeEnum x in new BasebandModeEnum[] { BasebandModeEnum.ARB, BasebandModeEnum.CW, BasebandModeEnum.DTONe })
				{
					driver.Source.BbMode = x;
					BasebandModeEnum value = driver.Source.BbMode;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:STATe
				GeneratorStateEnum value = driver.Source.State.Get();				
			}
			{	// SOURce:GPRF:GENerator<Instance>:STATe
				driver.Source.State.Set(false);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:STATe:ALL
				foreach (GeneratorStateEnum x in new GeneratorStateEnum[] { GeneratorStateEnum.ADJusted, GeneratorStateEnum.AUTonomous, GeneratorStateEnum.COUPled, GeneratorStateEnum.INValid, GeneratorStateEnum.OFF, GeneratorStateEnum.ON, GeneratorStateEnum.PENDing, GeneratorStateEnum.RDY })
				{
					List<GeneratorStateEnum> value = driver.Source.State.All;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:IQSettings:SRATe
				double value = driver.Source.IqSettings.SymbolRate;
				driver.Source.IqSettings.SymbolRate = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:IQSettings:TMODe
				foreach (TransferModeEnum x in new TransferModeEnum[] { TransferModeEnum.ENABlemode, TransferModeEnum.REQuestmode })
				{
					driver.Source.IqSettings.Tmode = x;
					TransferModeEnum value = driver.Source.IqSettings.Tmode;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:IQSettings:LEVel
				double value = driver.Source.IqSettings.Level;
			}
			{	// SOURce:GPRF:GENerator<Instance>:IQSettings:PEP
				double value = driver.Source.IqSettings.Pep;
			}
			{	// SOURce:GPRF:GENerator<Instance>:IQSettings:CRESt
				double value = driver.Source.IqSettings.Crest;
			}
			{	// SOURce:GPRF:GENerator<Instance>:RFSettings:DGAin
				double value = driver.Source.RfSettings.Dgain;
				driver.Source.RfSettings.Dgain = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:RFSettings:PEPower
				double value = driver.Source.RfSettings.PePower;
			}
			{	// SOURce:GPRF:GENerator<Instance>:RFSettings:EATTenuation
				double value = driver.Source.RfSettings.Eattenuation;
				driver.Source.RfSettings.Eattenuation = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:RFSettings:FREQuency
				double value = driver.Source.RfSettings.Frequency;
				driver.Source.RfSettings.Frequency = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:RFSettings:LEVel
				double value = driver.Source.RfSettings.Level;
				driver.Source.RfSettings.Level = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FOFFset
				double value = driver.Source.Arb.FreqOffset;
				driver.Source.Arb.FreqOffset = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:SCOunt
				RsCmwGprfGen_Source_Arb.Scount_Data value = driver.Source.Arb.Scount;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:ASAMples
				int value = driver.Source.Arb.Asamples;
				driver.Source.Arb.Asamples = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:REPetition
				foreach (RepeatModeEnum x in new RepeatModeEnum[] { RepeatModeEnum.CONTinuous, RepeatModeEnum.SINGle })
				{
					driver.Source.Arb.Repetition = x;
					RepeatModeEnum value = driver.Source.Arb.Repetition;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:CYCLes
				int value = driver.Source.Arb.Cycles;
				driver.Source.Arb.Cycles = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:POFFset
				double value = driver.Source.Arb.Poffset;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:CRATe
				double value = driver.Source.Arb.Crate;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:LOFFset
				double value = driver.Source.Arb.Loffset;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:CRCProtect
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					YesNoStatusEnum value = driver.Source.Arb.CrcProtect;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:STATus
				int value = driver.Source.Arb.Status;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:SAMPles:RANGe
				RsCmwGprfGen_Source_Arb_Samples.Range_Data value = driver.Source.Arb.Samples.Range;
				driver.Source.Arb.Samples.Range = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:SAMPles
				double value = driver.Source.Arb.Samples.Value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:UDMarker
				RsCmwGprfGen_Source_Arb_UdMarker.Value_Data value = driver.Source.Arb.UdMarker.Value;
				driver.Source.Arb.UdMarker.Value = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:UDMarker:CLISt
				driver.Source.Arb.UdMarker.Clist.Set();
				driver.Source.Arb.UdMarker.Clist.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FILE
				string value = driver.Source.Arb.File.Get("1");
				value = driver.Source.Arb.File.Get();				
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FILE
				driver.Source.Arb.File.Set("1");
				driver.Source.Arb.File.Set();
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FILE:DATE
				string value = driver.Source.Arb.File.Date;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FILE:VERSion
				string value = driver.Source.Arb.File.Version;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:FILE:OPTion
				string value = driver.Source.Arb.File.Option;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:NAME
				List<string> value = driver.Source.Arb.Msegment.Name;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:POFFset
				List<double> value = driver.Source.Arb.Msegment.Poffset;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:PAR
				List<double> value = driver.Source.Arb.Msegment.Par;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:DURation
				List<double> value = driver.Source.Arb.Msegment.Duration;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:SAMPles
				List<int> value = driver.Source.Arb.Msegment.Samples;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:CRATe
				List<double> value = driver.Source.Arb.Msegment.Crate;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MSEGment:NUMBer
				List<int> value = driver.Source.Arb.Msegment.Number;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:MARKer:DELays
				RsCmwGprfGen_Source_Arb_Marker.Delays_Data value = driver.Source.Arb.Marker.Delays;
				driver.Source.Arb.Marker.Delays = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:SEGMents:NEXT
				int value = driver.Source.Arb.Segments.Next;
				driver.Source.Arb.Segments.Next = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:ARB:SEGMents:CURRent
				RsCmwGprfGen_Source_Arb_Segments.Current_Data value = driver.Source.Arb.Segments.Current;
			}
			{	// SOURce:GPRF:GENerator<Instance>:DTONe:RATio
				double value = driver.Source.Dtone.Ratio;
				driver.Source.Dtone.Ratio = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:DTONe:LEVel<source>
				double value = driver.Source.Dtone.GetLevel(LevelSourceRepCap.Src1);
				value = driver.Source.Dtone.GetLevel();
			}
			{	// SOURce:GPRF:GENerator<Instance>:DTONe:OFRequency<source>
				double value = driver.Source.Dtone.Ofrequency.Get(FrequencySourceRepCap.Default);
				value = driver.Source.Dtone.Ofrequency.Get();
			}
			{	// SOURce:GPRF:GENerator<Instance>:DTONe:OFRequency<source>
				driver.Source.Dtone.Ofrequency.Set(1.0, FrequencySourceRepCap.Default);
				driver.Source.Dtone.Ofrequency.Set(1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:AINDex
				int value = driver.Source.List.Aindex;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:FILL
				RsCmwGprfGen_Source_List.Fill_Data value = new RsCmwGprfGen_Source_List.Fill_Data();
				driver.Source.List.Fill = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:GOTO
				int value = driver.Source.List.Goto;
				driver.Source.List.Goto = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:REPetition
				foreach (RepeatModeEnum x in new RepeatModeEnum[] { RepeatModeEnum.CONTinuous, RepeatModeEnum.SINGle })
				{
					driver.Source.List.Repetition = x;
					RepeatModeEnum value = driver.Source.List.Repetition;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:STARt
				int value = driver.Source.List.Start;
				driver.Source.List.Start = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:STOP
				int value = driver.Source.List.Stop;
				driver.Source.List.Stop = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:COUNt
				int value = driver.Source.List.Count;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST
				bool value = driver.Source.List.Value;
				driver.Source.List.Value = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:CMWS:CSET
				foreach (ParameterSetModeEnum x in new ParameterSetModeEnum[] { ParameterSetModeEnum.GLOBal, ParameterSetModeEnum.LIST })
				{
					driver.Source.List.SingleCmw.Cset = x;
					ParameterSetModeEnum value = driver.Source.List.SingleCmw.Cset;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:CMWS:USAGe:TX
				List<bool> value = driver.Source.List.SingleCmw.Usage.Tx.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:CMWS:USAGe:TX
				driver.Source.List.SingleCmw.Usage.Tx.Set(1, new List<bool> { true, false, true });
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:SLISt
				driver.Source.List.Slist.Set();
				driver.Source.List.Slist.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:ESINgle
				driver.Source.List.Esingle.Set();
				driver.Source.List.Esingle.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:RLISt
				driver.Source.List.Rlist.Set();
				driver.Source.List.Rlist.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:INCRement:CATalog
				List<string> value = driver.Source.List.Increment.Catalog;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:INCRement
				string value = driver.Source.List.Increment.Value;
				driver.Source.List.Increment.Value = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:INCRement:ENABling:CATalog
				List<string> value = driver.Source.List.Increment.Enabling.Catalog;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:INCRement:ENABling
				string value = driver.Source.List.Increment.Enabling.Value;
				driver.Source.List.Increment.Enabling.Value = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:SSTop
				RsCmwGprfGen_Source_List_Sstop.Get_Data value = driver.Source.List.Sstop.Get();				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:SSTop
				driver.Source.List.Sstop.Set(1, 1, 1);
				driver.Source.List.Sstop.Set(1, 1);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:RFLevel
				double value = driver.Source.List.RfLevel.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:RFLevel
				driver.Source.List.RfLevel.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:RFLevel:ALL
				List<double> value = driver.Source.List.RfLevel.All;
				driver.Source.List.RfLevel.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:FREQuency
				double value = driver.Source.List.Frequency.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:FREQuency
				driver.Source.List.Frequency.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:FREQuency:ALL
				List<double> value = driver.Source.List.Frequency.All;
				driver.Source.List.Frequency.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:IREPetition
				int value = driver.Source.List.Irepetition.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:IREPetition
				driver.Source.List.Irepetition.Set(1, 1);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:IREPetition:ALL
				List<int> value = driver.Source.List.Irepetition.All;
				driver.Source.List.Irepetition.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DGAin
				double value = driver.Source.List.Dgain.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DGAin
				driver.Source.List.Dgain.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DGAin:ALL
				List<double> value = driver.Source.List.Dgain.All;
				driver.Source.List.Dgain.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DTIMe
				double value = driver.Source.List.Dtime.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DTIMe
				driver.Source.List.Dtime.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:DTIMe:ALL
				List<double> value = driver.Source.List.Dtime.All;
				driver.Source.List.Dtime.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:MODulation
				bool value = driver.Source.List.Modulation.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:MODulation
				driver.Source.List.Modulation.Set(1, false);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:MODulation:ALL
				List<bool> value = driver.Source.List.Modulation.All;
				driver.Source.List.Modulation.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:REENabling
				bool value = driver.Source.List.Reenabling.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:REENabling
				driver.Source.List.Reenabling.Set(1, false);
			}
			{	// SOURce:GPRF:GENerator<Instance>:LIST:REENabling:ALL
				List<bool> value = driver.Source.List.Reenabling.All;
				driver.Source.List.Reenabling.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:REPetition
				foreach (RepeatModeEnum x in new RepeatModeEnum[] { RepeatModeEnum.CONTinuous, RepeatModeEnum.SINGle })
				{
					driver.Source.Sequencer.Repetition = x;
					RepeatModeEnum value = driver.Source.Sequencer.Repetition;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:SIGNal
				bool value = driver.Source.Sequencer.Signal;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:CENTry
				int value = driver.Source.Sequencer.Centry;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:UOPTions
				string value = driver.Source.Sequencer.Uoptions;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:VALid
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					YesNoStatusEnum value = driver.Source.Sequencer.Apool.Valid;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:LOADed
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					YesNoStatusEnum value = driver.Source.Sequencer.Apool.Loaded;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:RREQuired
				double value = driver.Source.Sequencer.Apool.Rrequired;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:RTOTal
				double value = driver.Source.Sequencer.Apool.Rtotal;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:FILE
				driver.Source.Sequencer.Apool.File = "1";
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:REMove
				driver.Source.Sequencer.Apool.Remove = new List<int> { 1, 2, 3 };
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:CLEar
				driver.Source.Sequencer.Apool.Clear();
				driver.Source.Sequencer.Apool.ClearAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:MINDex
				int value = driver.Source.Sequencer.Apool.Mindex;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:PATH
				string value = driver.Source.Sequencer.Apool.GetPath(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:CRCProtect
				YesNoStatusEnum value = driver.Source.Sequencer.Apool.GetCrcProtect(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:PARatio
				double value = driver.Source.Sequencer.Apool.GetParatio(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:POFFset
				double value = driver.Source.Sequencer.Apool.GetPoffset(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:ROPTion
				string value = driver.Source.Sequencer.Apool.GetRoption(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:DURation
				double value = driver.Source.Sequencer.Apool.GetDuration(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:SAMPles
				int value = driver.Source.Sequencer.Apool.GetSamples(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:SRATe
				double value = driver.Source.Sequencer.Apool.GetSymbolRate(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:WAVeform
				string value = driver.Source.Sequencer.Apool.GetWaveform(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:RELiability
				int value = driver.Source.Sequencer.Apool.GetReliability(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:RMESsage
				string value = driver.Source.Sequencer.Apool.GetRmessage(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:DOWNload
				driver.Source.Sequencer.Apool.Download.Set();
				driver.Source.Sequencer.Apool.Download.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:PATH:ALL
				List<string> value = driver.Source.Sequencer.Apool.Path.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:CRCProtect:ALL
				foreach (YesNoStatusEnum x in new YesNoStatusEnum[] { YesNoStatusEnum.NO, YesNoStatusEnum.YES })
				{
					List<YesNoStatusEnum> value = driver.Source.Sequencer.Apool.CrcProtect.All;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:PARatio:ALL
				List<double> value = driver.Source.Sequencer.Apool.Paratio.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:POFFset:ALL
				List<double> value = driver.Source.Sequencer.Apool.Poffset.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:ROPTion:ALL
				List<string> value = driver.Source.Sequencer.Apool.Roption.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:DURation:ALL
				List<double> value = driver.Source.Sequencer.Apool.Duration.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:SAMPles:ALL
				List<int> value = driver.Source.Sequencer.Apool.Samples.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:SRATe:ALL
				List<double> value = driver.Source.Sequencer.Apool.SymbolRate.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:WAVeform:ALL
				List<string> value = driver.Source.Sequencer.Apool.Waveform.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:APOol:RMESsage:ALL
				List<string> value = driver.Source.Sequencer.Apool.Rmessage.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:STATe
				GeneratorStateEnum value = driver.Source.Sequencer.State.Get();				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:STATe
				driver.Source.Sequencer.State.Set(false);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:STATe:ALL
				foreach (GeneratorStateEnum x in new GeneratorStateEnum[] { GeneratorStateEnum.ADJusted, GeneratorStateEnum.AUTonomous, GeneratorStateEnum.COUPled, GeneratorStateEnum.INValid, GeneratorStateEnum.OFF, GeneratorStateEnum.ON, GeneratorStateEnum.PENDing, GeneratorStateEnum.RDY })
				{
					List<GeneratorStateEnum> value = driver.Source.Sequencer.State.All;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:RFSettings:CMWS:CSET
				foreach (ParameterSetModeEnum x in new ParameterSetModeEnum[] { ParameterSetModeEnum.GLOBal, ParameterSetModeEnum.LIST })
				{
					driver.Source.Sequencer.RfSettings.SingleCmw.Cset = x;
					ParameterSetModeEnum value = driver.Source.Sequencer.RfSettings.SingleCmw.Cset;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:CREate
				driver.Source.Sequencer.List.Create = 1.0;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:INDex
				int value = driver.Source.Sequencer.List.Index;
				driver.Source.Sequencer.List.Index = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:MINDex
				int value = driver.Source.Sequencer.List.Mindex;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SRATe
				double value = driver.Source.Sequencer.List.GetSymbolRate(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:TTIMe
				double value = driver.Source.Sequencer.List.GetTtime(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:SINDex
				int value = driver.Source.Sequencer.List.Fill.Sindex;
				driver.Source.Sequencer.List.Fill.Sindex = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:RANGe
				int value = driver.Source.Sequencer.List.Fill.Range;
				driver.Source.Sequencer.List.Fill.Range = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:APPLy
				driver.Source.Sequencer.List.Fill.Apply.Set();
				driver.Source.Sequencer.List.Fill.Apply.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:FREQuency:SVALue
				double value = driver.Source.Sequencer.List.Fill.Frequency.Svalue;
				driver.Source.Sequencer.List.Fill.Frequency.Svalue = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:FREQuency:INCRement
				double value = driver.Source.Sequencer.List.Fill.Frequency.Increment;
				driver.Source.Sequencer.List.Fill.Frequency.Increment = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:FREQuency:KEEP
				bool value = driver.Source.Sequencer.List.Fill.Frequency.Keep;
				driver.Source.Sequencer.List.Fill.Frequency.Keep = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:LRMS:SVALue
				double value = driver.Source.Sequencer.List.Fill.Lrms.Svalue;
				driver.Source.Sequencer.List.Fill.Lrms.Svalue = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:LRMS:INCRement
				double value = driver.Source.Sequencer.List.Fill.Lrms.Increment;
				driver.Source.Sequencer.List.Fill.Lrms.Increment = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:LRMS:KEEP
				bool value = driver.Source.Sequencer.List.Fill.Lrms.Keep;
				driver.Source.Sequencer.List.Fill.Lrms.Keep = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:DGAin:SVALue
				double value = driver.Source.Sequencer.List.Fill.Dgain.Svalue;
				driver.Source.Sequencer.List.Fill.Dgain.Svalue = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:DGAin:INCRement
				double value = driver.Source.Sequencer.List.Fill.Dgain.Increment;
				driver.Source.Sequencer.List.Fill.Dgain.Increment = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FILL:DGAin:KEEP
				bool value = driver.Source.Sequencer.List.Fill.Dgain.Keep;
				driver.Source.Sequencer.List.Fill.Dgain.Keep = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ENTRy:DELete
				driver.Source.Sequencer.List.Entry.Delete(1);
				driver.Source.Sequencer.List.Entry.Delete();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ENTRy:INSert
				driver.Source.Sequencer.List.Entry.Insert.Set(1);
				driver.Source.Sequencer.List.Entry.Insert.Set();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ENTRy:CALL
				driver.Source.Sequencer.List.Entry.Call.Set();
				driver.Source.Sequencer.List.Entry.Call.SetAndWait();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ENTRy:MUP
				driver.Source.Sequencer.List.Entry.Mup.Set(1);
				driver.Source.Sequencer.List.Entry.Mup.Set();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ENTRy:MDOWn
				driver.Source.Sequencer.List.Entry.Mdown.Set(1);
				driver.Source.Sequencer.List.Entry.Mdown.Set();
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FREQuency
				double value = driver.Source.Sequencer.List.Frequency.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FREQuency
				driver.Source.Sequencer.List.Frequency.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:FREQuency:ALL
				List<double> value = driver.Source.Sequencer.List.Frequency.All;
				driver.Source.Sequencer.List.Frequency.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LRMS
				double value = driver.Source.Sequencer.List.Lrms.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LRMS
				driver.Source.Sequencer.List.Lrms.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LRMS:ALL
				List<double> value = driver.Source.Sequencer.List.Lrms.All;
				driver.Source.Sequencer.List.Lrms.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DGAin
				double value = driver.Source.Sequencer.List.Dgain.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DGAin
				driver.Source.Sequencer.List.Dgain.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DGAin:ALL
				List<double> value = driver.Source.Sequencer.List.Dgain.All;
				driver.Source.Sequencer.List.Dgain.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SIGNal:CATalog
				List<string> value = driver.Source.Sequencer.List.Signal.Catalog;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SIGNal
				string value = driver.Source.Sequencer.List.Signal.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SIGNal
				driver.Source.Sequencer.List.Signal.Set(1, "1");
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SIGNal:ALL
				List<string> value = driver.Source.Sequencer.List.Signal.All;
				driver.Source.Sequencer.List.Signal.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:SRATe:ALL
				List<double> value = driver.Source.Sequencer.List.SymbolRate.All;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LINCrement
				ListIncrementEnum value = driver.Source.Sequencer.List.Lincrement.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LINCrement
				driver.Source.Sequencer.List.Lincrement.Set(1, ListIncrementEnum.ACYCles);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:LINCrement:ALL
				foreach (ListIncrementEnum x in new ListIncrementEnum[] { ListIncrementEnum.ACYCles, ListIncrementEnum.DTIMe, ListIncrementEnum.MEASurement, ListIncrementEnum.TRIGger, ListIncrementEnum.USER })
				{
					driver.Source.Sequencer.List.Lincrement.All = new List<ListIncrementEnum> { x, x, x, x, x };
					List<ListIncrementEnum> value = driver.Source.Sequencer.List.Lincrement.All;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ITRansition
				IncTransitionEnum value = driver.Source.Sequencer.List.Itransition.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ITRansition
				driver.Source.Sequencer.List.Itransition.Set(1, IncTransitionEnum.IMMediate);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ITRansition:ALL
				foreach (IncTransitionEnum x in new IncTransitionEnum[] { IncTransitionEnum.IMMediate, IncTransitionEnum.RMARker, IncTransitionEnum.WMA1, IncTransitionEnum.WMA2, IncTransitionEnum.WMA3, IncTransitionEnum.WMA4 })
				{
					driver.Source.Sequencer.List.Itransition.All = new List<IncTransitionEnum> { x, x, x, x, x };
					List<IncTransitionEnum> value = driver.Source.Sequencer.List.Itransition.All;
				}
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ACYCles
				int value = driver.Source.Sequencer.List.Acycles.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ACYCles
				driver.Source.Sequencer.List.Acycles.Set(1, 1);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:ACYCles:ALL
				List<int> value = driver.Source.Sequencer.List.Acycles.All;
				driver.Source.Sequencer.List.Acycles.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DTIMe
				double value = driver.Source.Sequencer.List.Dtime.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DTIMe
				driver.Source.Sequencer.List.Dtime.Set(1, 1.0);
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:DTIMe:ALL
				List<double> value = driver.Source.Sequencer.List.Dtime.All;
				driver.Source.Sequencer.List.Dtime.All = value;
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:TTIMe:ALL
				List<double> value = driver.Source.Sequencer.List.Ttime.GetAll(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:CMWS:USAGe:TX
				List<bool> value = driver.Source.Sequencer.List.SingleCmw.Usage.Tx.Get(1);				
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:LIST:CMWS:USAGe:TX
				driver.Source.Sequencer.List.SingleCmw.Usage.Tx.Set(1, new List<bool> { true, false, true });
			}
			{	// SOURce:GPRF:GENerator<Instance>:SEQuencer:MARKer:DELays
				RsCmwGprfGen_Source_Sequencer_Marker.Delays_Data value = driver.Source.Sequencer.Marker.Delays;
				driver.Source.Sequencer.Marker.Delays = value;
			}
			{	// CONFigure:GPRF:GENerator<Instance>:TYPE
				foreach (InstrumentTypeEnum x in new InstrumentTypeEnum[] { InstrumentTypeEnum.PROTocol, InstrumentTypeEnum.SIGNaling })
				{
					driver.Configure.Type = x;
					InstrumentTypeEnum value = driver.Configure.Type;
				}
			}
			{	// CONFigure:GPRF:GENerator<Instance>:CMWS:USAGe:TX
				bool value = driver.Configure.SingleCmw.Usage.Tx.Get(TxConnectorCmwsEnum.R11);				
			}
			{	// CONFigure:GPRF:GENerator<Instance>:CMWS:USAGe:TX
				driver.Configure.SingleCmw.Usage.Tx.Set(TxConnectorCmwsEnum.R11, false);
			}
			{	// CONFigure:GPRF:GENerator<Instance>:CMWS:USAGe:TX:ALL
				List<bool> value = driver.Configure.SingleCmw.Usage.Tx.All.Get(TxConnectorBenchEnum.R118);				
			}
			{	// CONFigure:GPRF:GENerator<Instance>:CMWS:USAGe:TX:ALL
				driver.Configure.SingleCmw.Usage.Tx.All.Set(TxConnectorBenchEnum.R118, new List<bool> { true, false, true });
			}
			{	// ROUTe:GPRF:GENerator<Instance>
				RsCmwGprfGen_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:GPRF:GENerator<Instance>:SCENario:SALone
				RsCmwGprfGen_Route_Scenario.Salone_Data value = driver.Route.Scenario.Salone;
				driver.Route.Scenario.Salone = value;
			}
			{	// ROUTe:GPRF:GENerator<Instance>:SCENario:IQOut
				RsCmwGprfGen_Route_Scenario.IqOut_Data value = driver.Route.Scenario.IqOut;
				driver.Route.Scenario.IqOut = value;
			}
			{	// ROUTe:GPRF:GENerator<Instance>:SCENario
				foreach (ScenarioEnum x in new ScenarioEnum[] { ScenarioEnum.IQOut, ScenarioEnum.NAV, ScenarioEnum.SALone })
				{
					ScenarioEnum value = driver.Route.Scenario.Value;
				}
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:SLOPe
				foreach (SignalSlopeEnum x in new SignalSlopeEnum[] { SignalSlopeEnum.FEDGe, SignalSlopeEnum.REDGe })
				{
					driver.Trigger.Arb.Slope = x;
					SignalSlopeEnum value = driver.Trigger.Arb.Slope;
				}
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:DELay
				double value = driver.Trigger.Arb.Delay;
				driver.Trigger.Arb.Delay = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:SOURce
				string value = driver.Trigger.Arb.Source;
				driver.Trigger.Arb.Source = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:RETRigger
				bool value = driver.Trigger.Arb.ReTrigger;
				driver.Trigger.Arb.ReTrigger = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:AUTostart
				bool value = driver.Trigger.Arb.Autostart;
				driver.Trigger.Arb.Autostart = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:CATalog:SOURce
				List<string> value = driver.Trigger.Arb.Catalog.Source;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:MANual:EXECute
				driver.Trigger.Arb.Manual.Execute.Set();
				driver.Trigger.Arb.Manual.Execute.SetAndWait();
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:SEGMents:MODE
				foreach (ArbSegmentsModeEnum x in new ArbSegmentsModeEnum[] { ArbSegmentsModeEnum.AUTO, ArbSegmentsModeEnum.CONTinuous, ArbSegmentsModeEnum.CSEamless })
				{
					driver.Trigger.Arb.Segments.Mode = x;
					ArbSegmentsModeEnum value = driver.Trigger.Arb.Segments.Mode;
				}
			}
			{	// TRIGger:GPRF:GENerator<Instance>:ARB:SEGMents:MANual:EXECute
				driver.Trigger.Arb.Segments.Manual.Execute.Set();
				driver.Trigger.Arb.Segments.Manual.Execute.SetAndWait();
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:TOUT
				double value = driver.Trigger.Sequencer.Timeout;
				driver.Trigger.Sequencer.Timeout = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:ISMeas:CATalog
				List<string> value = driver.Trigger.Sequencer.IsMeas.Catalog;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:ISMeas:SOURce
				string value = driver.Trigger.Sequencer.IsMeas.Source;
				driver.Trigger.Sequencer.IsMeas.Source = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:ISTRigger:CATalog
				List<string> value = driver.Trigger.Sequencer.IsTrigger.Catalog;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:ISTRigger:SOURce
				string value = driver.Trigger.Sequencer.IsTrigger.Source;
				driver.Trigger.Sequencer.IsTrigger.Source = value;
			}
			{	// TRIGger:GPRF:GENerator<Instance>:SEQuencer:MANual:EXECute
				driver.Trigger.Sequencer.Manual.Execute.Set();
				driver.Trigger.Sequencer.Manual.Execute.SetAndWait();
			}
		}
	}
}