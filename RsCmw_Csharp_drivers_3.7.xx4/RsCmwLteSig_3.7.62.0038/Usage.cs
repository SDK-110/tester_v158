using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using RohdeSchwarz.RsCmwLteSig;

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
			RsCmwLteSig driver = new RsCmwLteSig("TCPIP::localhost::INSTR", true, true);
			{	// CATalog:LTE:SIGNaling<instance>:SCENario
				foreach (ScenarioEnum x in new ScenarioEnum[] { ScenarioEnum.AD, ScenarioEnum.ADF, ScenarioEnum.BF, ScenarioEnum.BFF, ScenarioEnum.BFSM4, ScenarioEnum.BH, ScenarioEnum.BHF, ScenarioEnum.CAFF, ScenarioEnum.CAFR, ScenarioEnum.CATF, ScenarioEnum.CATR, ScenarioEnum.CC, ScenarioEnum.CCMP, ScenarioEnum.CCMS1, ScenarioEnum.CF, ScenarioEnum.CFF, ScenarioEnum.CH, ScenarioEnum.CHF, ScenarioEnum.CHSM4, ScenarioEnum.CJ, ScenarioEnum.CJF, ScenarioEnum.CJFS4, ScenarioEnum.CJSM4, ScenarioEnum.CL, ScenarioEnum.DD, ScenarioEnum.DH, ScenarioEnum.DHF, ScenarioEnum.DJ, ScenarioEnum.DJSM4, ScenarioEnum.DL, ScenarioEnum.DLSM4, ScenarioEnum.DN, ScenarioEnum.DNSM4, ScenarioEnum.DP, ScenarioEnum.DPF, ScenarioEnum.EE, ScenarioEnum.EJ, ScenarioEnum.EJF, ScenarioEnum.EL, ScenarioEnum.ELSM4, ScenarioEnum.EN, ScenarioEnum.ENSM4, ScenarioEnum.EP, ScenarioEnum.EPF, ScenarioEnum.EPFS4, ScenarioEnum.EPSM4, ScenarioEnum.ER, ScenarioEnum.ERSM4, ScenarioEnum.ET, ScenarioEnum.FF, ScenarioEnum.FL, ScenarioEnum.FLF, ScenarioEnum.FN, ScenarioEnum.FNSM4, ScenarioEnum.FP, ScenarioEnum.FPF, ScenarioEnum.FPFS4, ScenarioEnum.FPSM4, ScenarioEnum.FR, ScenarioEnum.FRSM4, ScenarioEnum.FT, ScenarioEnum.FTSM4, ScenarioEnum.FV, ScenarioEnum.FVSM4, ScenarioEnum.FX, ScenarioEnum.GG, ScenarioEnum.GN, ScenarioEnum.GNF, ScenarioEnum.GP, ScenarioEnum.GPF, ScenarioEnum.GPFS4, ScenarioEnum.GPSM4, ScenarioEnum.GR, ScenarioEnum.GRSM4, ScenarioEnum.GT, ScenarioEnum.GTSM4, ScenarioEnum.GV, ScenarioEnum.GVSM4, ScenarioEnum.GX, ScenarioEnum.GXSM4, ScenarioEnum.HH, ScenarioEnum.HP, ScenarioEnum.HPF, ScenarioEnum.HT, ScenarioEnum.HTSM4, ScenarioEnum.NAV, ScenarioEnum.SCEL, ScenarioEnum.SCF, ScenarioEnum.TRO, ScenarioEnum.TROF })
				{
					List<ScenarioEnum> value = driver.Catalog.Scenario;
				}
			}
			{	// CATalog:LTE:SIGNaling<instance>:CONNection:DEFBearer
				List<string> value = driver.Catalog.Connection.Defbearer;
			}
			{	// CATalog:LTE:SIGNaling<instance>:CONNection:DEDBearer
				List<string> value = driver.Catalog.Connection.DedBearer;
			}
			{	// ROUTe:LTE:SIGNaling<instance>
				RsCmwLteSig_Route.Value_Data value = driver.Route.Value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario
				RsCmwLteSig_Route_Scenario.Value_Data value = driver.Route.Scenario.Value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:SCELl:FLEXible
				RsCmwLteSig_Route_Scenario_Scell.Flexible_Data value = driver.Route.Scenario.Scell.Flexible;
				driver.Route.Scenario.Scell.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:TRO:FLEXible
				RsCmwLteSig_Route_Scenario_Tro.Flexible_Data value = driver.Route.Scenario.Tro.Flexible;
				driver.Route.Scenario.Tro.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:AD[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ad.Flexible_Data value = driver.Route.Scenario.Ad.Flexible;
				driver.Route.Scenario.Ad.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:SCFading:FLEXible[:EXTernal]
				RsCmwLteSig_Route_Scenario_ScFading_Flexible.External_Data value = driver.Route.Scenario.ScFading.Flexible.External;
				driver.Route.Scenario.ScFading.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:SCFading:FLEXible:INTernal
				RsCmwLteSig_Route_Scenario_ScFading_Flexible.Internal_Data value = driver.Route.Scenario.ScFading.Flexible.Internal;
				driver.Route.Scenario.ScFading.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:TROFading:FLEXible[:EXTernal]
				RsCmwLteSig_Route_Scenario_TroFading_Flexible.External_Data value = driver.Route.Scenario.TroFading.Flexible.External;
				driver.Route.Scenario.TroFading.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:TROFading:FLEXible:INTernal
				RsCmwLteSig_Route_Scenario_TroFading_Flexible.Internal_Data value = driver.Route.Scenario.TroFading.Flexible.Internal;
				driver.Route.Scenario.TroFading.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ADF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Adf_Flexible.Internal_Data value = driver.Route.Scenario.Adf.Flexible.Internal;
				driver.Route.Scenario.Adf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CATRfout:FLEXible
				RsCmwLteSig_Route_Scenario_CatRfOut.Flexible_Data value = driver.Route.Scenario.CatRfOut.Flexible;
				driver.Route.Scenario.CatRfOut.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CAFRfout:FLEXible
				RsCmwLteSig_Route_Scenario_CafrfOut.Flexible_Data value = driver.Route.Scenario.CafrfOut.Flexible;
				driver.Route.Scenario.CafrfOut.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:BF[:FLEXible]
				RsCmwLteSig_Route_Scenario_Bf.Flexible_Data value = driver.Route.Scenario.Bf.Flexible;
				driver.Route.Scenario.Bf.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:BFSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Bfsm.Flexible_Data value = driver.Route.Scenario.Bfsm.Flexible;
				driver.Route.Scenario.Bfsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:BH[:FLEXible]
				RsCmwLteSig_Route_Scenario_Bh.Flexible_Data value = driver.Route.Scenario.Bh.Flexible;
				driver.Route.Scenario.Bh.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CATF:FLEXible[:EXTernal]
				RsCmwLteSig_Route_Scenario_Catf_Flexible.External_Data value = driver.Route.Scenario.Catf.Flexible.External;
				driver.Route.Scenario.Catf.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CATF:FLEXible:INTernal
				RsCmwLteSig_Route_Scenario_Catf_Flexible.Internal_Data value = driver.Route.Scenario.Catf.Flexible.Internal;
				driver.Route.Scenario.Catf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CAFF:FLEXible[:EXTernal]
				RsCmwLteSig_Route_Scenario_Caff_Flexible.External_Data value = driver.Route.Scenario.Caff.Flexible.External;
				driver.Route.Scenario.Caff.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CAFF:FLEXible:INTernal
				RsCmwLteSig_Route_Scenario_Caff_Flexible.Internal_Data value = driver.Route.Scenario.Caff.Flexible.Internal;
				driver.Route.Scenario.Caff.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:BFF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Bff_Flexible.Internal_Data value = driver.Route.Scenario.Bff.Flexible.Internal;
				driver.Route.Scenario.Bff.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:BHF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Bhf_Flexible.Internal_Data value = driver.Route.Scenario.Bhf.Flexible.Internal;
				driver.Route.Scenario.Bhf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CC:FLEXible
				RsCmwLteSig_Route_Scenario_Cc.Flexible_Data value = driver.Route.Scenario.Cc.Flexible;
				driver.Route.Scenario.Cc.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CCMP:FLEXible
				RsCmwLteSig_Route_Scenario_Ccmp.Flexible_Data value = driver.Route.Scenario.Ccmp.Flexible;
				driver.Route.Scenario.Ccmp.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CCMS<Carrier>:FLEXible
				RsCmwLteSig_Route_Scenario_Ccms.Flexible_Data value = driver.Route.Scenario.Ccms.Flexible;
				driver.Route.Scenario.Ccms.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CF[:FLEXible]
				RsCmwLteSig_Route_Scenario_Cf.Flexible_Data value = driver.Route.Scenario.Cf.Flexible;
				driver.Route.Scenario.Cf.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CH[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ch.Flexible_Data value = driver.Route.Scenario.Ch.Flexible;
				driver.Route.Scenario.Ch.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CHSM<MIMO44>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Chsm.Flexible_Data value = driver.Route.Scenario.Chsm.Flexible;
				driver.Route.Scenario.Chsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CJ[:FLEXible]
				RsCmwLteSig_Route_Scenario_Cj.Flexible_Data value = driver.Route.Scenario.Cj.Flexible;
				driver.Route.Scenario.Cj.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CJSM<MIMO44>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Cjsm.Flexible_Data value = driver.Route.Scenario.Cjsm.Flexible;
				driver.Route.Scenario.Cjsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CL[:FLEXible]
				RsCmwLteSig_Route_Scenario_Cl.Flexible_Data value = driver.Route.Scenario.Cl.Flexible;
				driver.Route.Scenario.Cl.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CFF[:FLEXible][:EXTernal]
				RsCmwLteSig_Route_Scenario_Cff_Flexible.External_Data value = driver.Route.Scenario.Cff.Flexible.External;
				driver.Route.Scenario.Cff.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CFF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Cff_Flexible.Internal_Data value = driver.Route.Scenario.Cff.Flexible.Internal;
				driver.Route.Scenario.Cff.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CHF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Chf_Flexible.Internal_Data value = driver.Route.Scenario.Chf.Flexible.Internal;
				driver.Route.Scenario.Chf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CJF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Cjf_Flexible.Internal_Data value = driver.Route.Scenario.Cjf.Flexible.Internal;
				driver.Route.Scenario.Cjf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:CJFS<MIMO44>[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Cjfs_Flexible.Internal_Data value = driver.Route.Scenario.Cjfs.Flexible.Internal;
				driver.Route.Scenario.Cjfs.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DD:FLEXible
				RsCmwLteSig_Route_Scenario_Dd.Flexible_Data value = driver.Route.Scenario.Dd.Flexible;
				driver.Route.Scenario.Dd.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DH[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dh.Flexible_Data value = driver.Route.Scenario.Dh.Flexible;
				driver.Route.Scenario.Dh.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DJ[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dj.Flexible_Data value = driver.Route.Scenario.Dj.Flexible;
				driver.Route.Scenario.Dj.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DJSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Djsm.Flexible_Data value = driver.Route.Scenario.Djsm.Flexible;
				driver.Route.Scenario.Djsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DL[:FLEXible]
				RsCmwLteSig_Route_Scenario_Downlink.Flexible_Data value = driver.Route.Scenario.Downlink.Flexible;
				driver.Route.Scenario.Downlink.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DLSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dlsm.Flexible_Data value = driver.Route.Scenario.Dlsm.Flexible;
				driver.Route.Scenario.Dlsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DN[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dn.Flexible_Data value = driver.Route.Scenario.Dn.Flexible;
				driver.Route.Scenario.Dn.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DNSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dnsm.Flexible_Data value = driver.Route.Scenario.Dnsm.Flexible;
				driver.Route.Scenario.Dnsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DP[:FLEXible]
				RsCmwLteSig_Route_Scenario_Dp.Flexible_Data value = driver.Route.Scenario.Dp.Flexible;
				driver.Route.Scenario.Dp.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DHF[:FLEXible][:EXTernal]
				RsCmwLteSig_Route_Scenario_Dhf_Flexible.External_Data value = driver.Route.Scenario.Dhf.Flexible.External;
				driver.Route.Scenario.Dhf.Flexible.External = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DHF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Dhf_Flexible.Internal_Data value = driver.Route.Scenario.Dhf.Flexible.Internal;
				driver.Route.Scenario.Dhf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:DPF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Dpf_Flexible.Internal_Data value = driver.Route.Scenario.Dpf.Flexible.Internal;
				driver.Route.Scenario.Dpf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EE[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ee.Flexible_Data value = driver.Route.Scenario.Ee.Flexible;
				driver.Route.Scenario.Ee.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EJ[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ej.Flexible_Data value = driver.Route.Scenario.Ej.Flexible;
				driver.Route.Scenario.Ej.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EJF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Ejf_Flexible.Internal_Data value = driver.Route.Scenario.Ejf.Flexible.Internal;
				driver.Route.Scenario.Ejf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EL[:FLEXible]
				RsCmwLteSig_Route_Scenario_El.Flexible_Data value = driver.Route.Scenario.El.Flexible;
				driver.Route.Scenario.El.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ELSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Elsm.Flexible_Data value = driver.Route.Scenario.Elsm.Flexible;
				driver.Route.Scenario.Elsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EN[:FLEXible]
				RsCmwLteSig_Route_Scenario_En.Flexible_Data value = driver.Route.Scenario.En.Flexible;
				driver.Route.Scenario.En.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ENSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ensm.Flexible_Data value = driver.Route.Scenario.Ensm.Flexible;
				driver.Route.Scenario.Ensm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EP[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ep.Flexible_Data value = driver.Route.Scenario.Ep.Flexible;
				driver.Route.Scenario.Ep.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EPSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Epsm.Flexible_Data value = driver.Route.Scenario.Epsm.Flexible;
				driver.Route.Scenario.Epsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EPF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Epf_Flexible.Internal_Data value = driver.Route.Scenario.Epf.Flexible.Internal;
				driver.Route.Scenario.Epf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:EPFS<MIMO4x4>[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Epfs_Flexible.Internal_Data value = driver.Route.Scenario.Epfs.Flexible.Internal;
				driver.Route.Scenario.Epfs.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ER[:FLEXible]
				RsCmwLteSig_Route_Scenario_Er.Flexible_Data value = driver.Route.Scenario.Er.Flexible;
				driver.Route.Scenario.Er.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ERSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ersm.Flexible_Data value = driver.Route.Scenario.Ersm.Flexible;
				driver.Route.Scenario.Ersm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:ET[:FLEXible]
				RsCmwLteSig_Route_Scenario_Et.Flexible_Data value = driver.Route.Scenario.Et.Flexible;
				driver.Route.Scenario.Et.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FRSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Frsm.Flexible_Data value = driver.Route.Scenario.Frsm.Flexible;
				driver.Route.Scenario.Frsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FR[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fr.Flexible_Data value = driver.Route.Scenario.Fr.Flexible;
				driver.Route.Scenario.Fr.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FNSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fnsm.Flexible_Data value = driver.Route.Scenario.Fnsm.Flexible;
				driver.Route.Scenario.Fnsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FN[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fn.Flexible_Data value = driver.Route.Scenario.Fn.Flexible;
				driver.Route.Scenario.Fn.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FTSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ftsm.Flexible_Data value = driver.Route.Scenario.Ftsm.Flexible;
				driver.Route.Scenario.Ftsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FT[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ft.Flexible_Data value = driver.Route.Scenario.Ft.Flexible;
				driver.Route.Scenario.Ft.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FP[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fp.Flexible_Data value = driver.Route.Scenario.Fp.Flexible;
				driver.Route.Scenario.Fp.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FPSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fpsm.Flexible_Data value = driver.Route.Scenario.Fpsm.Flexible;
				driver.Route.Scenario.Fpsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FV[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fv.Flexible_Data value = driver.Route.Scenario.Fv.Flexible;
				driver.Route.Scenario.Fv.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FVSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fvsm.Flexible_Data value = driver.Route.Scenario.Fvsm.Flexible;
				driver.Route.Scenario.Fvsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FX[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fx.Flexible_Data value = driver.Route.Scenario.Fx.Flexible;
				driver.Route.Scenario.Fx.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FF[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ff.Flexible_Data value = driver.Route.Scenario.Ff.Flexible;
				driver.Route.Scenario.Ff.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FL[:FLEXible]
				RsCmwLteSig_Route_Scenario_Fl.Flexible_Data value = driver.Route.Scenario.Fl.Flexible;
				driver.Route.Scenario.Fl.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FLF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Flf_Flexible.Internal_Data value = driver.Route.Scenario.Flf.Flexible.Internal;
				driver.Route.Scenario.Flf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FPF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Fpf_Flexible.Internal_Data value = driver.Route.Scenario.Fpf.Flexible.Internal;
				driver.Route.Scenario.Fpf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:FPFS<MIMO4x4>[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Fpfs_Flexible.Internal_Data value = driver.Route.Scenario.Fpfs.Flexible.Internal;
				driver.Route.Scenario.Fpfs.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GRSM<MIMO4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Grsm.Flexible_Data value = driver.Route.Scenario.Grsm.Flexible;
				driver.Route.Scenario.Grsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GR[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gr.Flexible_Data value = driver.Route.Scenario.Gr.Flexible;
				driver.Route.Scenario.Gr.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GTSM<Mimo4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gtsm.Flexible_Data value = driver.Route.Scenario.Gtsm.Flexible;
				driver.Route.Scenario.Gtsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GT[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gt.Flexible_Data value = driver.Route.Scenario.Gt.Flexible;
				driver.Route.Scenario.Gt.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GG[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gg.Flexible_Data value = driver.Route.Scenario.Gg.Flexible;
				driver.Route.Scenario.Gg.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GN[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gn.Flexible_Data value = driver.Route.Scenario.Gn.Flexible;
				driver.Route.Scenario.Gn.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GNF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Gnf_Flexible.Internal_Data value = driver.Route.Scenario.Gnf.Flexible.Internal;
				driver.Route.Scenario.Gnf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GPSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gpsm.Flexible_Data value = driver.Route.Scenario.Gpsm.Flexible;
				driver.Route.Scenario.Gpsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GPFS<MIMO4x4>[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Gpfs_Flexible.Internal_Data value = driver.Route.Scenario.Gpfs.Flexible.Internal;
				driver.Route.Scenario.Gpfs.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GP[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gp.Flexible_Data value = driver.Route.Scenario.Gp.Flexible;
				driver.Route.Scenario.Gp.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GPF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Gpf_Flexible.Internal_Data value = driver.Route.Scenario.Gpf.Flexible.Internal;
				driver.Route.Scenario.Gpf.Flexible.Internal = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GV[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gv.Flexible_Data value = driver.Route.Scenario.Gv.Flexible;
				driver.Route.Scenario.Gv.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GVSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gvsm.Flexible_Data value = driver.Route.Scenario.Gvsm.Flexible;
				driver.Route.Scenario.Gvsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GX[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gx.Flexible_Data value = driver.Route.Scenario.Gx.Flexible;
				driver.Route.Scenario.Gx.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:GXSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Gxsm.Flexible_Data value = driver.Route.Scenario.Gxsm.Flexible;
				driver.Route.Scenario.Gxsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:HTSM<MIMO4x4>[:FLEXible]
				RsCmwLteSig_Route_Scenario_Htsm.Flexible_Data value = driver.Route.Scenario.Htsm.Flexible;
				driver.Route.Scenario.Htsm.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:HT[:FLEXible]
				RsCmwLteSig_Route_Scenario_Ht.Flexible_Data value = driver.Route.Scenario.Ht.Flexible;
				driver.Route.Scenario.Ht.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:HH[:FLEXible]
				RsCmwLteSig_Route_Scenario_Hh.Flexible_Data value = driver.Route.Scenario.Hh.Flexible;
				driver.Route.Scenario.Hh.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:HP[:FLEXible]
				RsCmwLteSig_Route_Scenario_Hp.Flexible_Data value = driver.Route.Scenario.Hp.Flexible;
				driver.Route.Scenario.Hp.Flexible = value;
			}
			{	// ROUTe:LTE:SIGNaling<instance>:SCENario:HPF[:FLEXible]:INTernal
				RsCmwLteSig_Route_Scenario_Hpf_Flexible.Internal_Data value = driver.Route.Scenario.Hpf.Flexible.Internal;
				driver.Route.Scenario.Hpf.Flexible.Internal = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:ETOE
				bool value = driver.Configure.Etoe;
				driver.Configure.Etoe = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC:AMODe
				foreach (AutoManualModeExtEnum x in new AutoManualModeExtEnum[] { AutoManualModeExtEnum.AUTO, AutoManualModeExtEnum.MANual, AutoManualModeExtEnum.SEMiauto })
				{
					driver.Configure.Scc.Amode = x;
					AutoManualModeExtEnum value = driver.Configure.Scc.Amode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:UUL
				RsCmwLteSig_Configure_Scc_Uul.Uul_Data value = driver.Configure.Scc.Uul.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Scc.Uul.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:UUL
				RsCmwLteSig_Configure_Scc_Uul.Uul_Data value = new RsCmwLteSig_Configure_Scc_Uul.Uul_Data();
				driver.Configure.Scc.Uul.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Scc.Uul.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:DMODe
				DuplexModeEnum value = driver.Configure.Scc.Dmode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Scc.Dmode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:DMODe
				foreach (DuplexModeEnum x in new DuplexModeEnum[] { DuplexModeEnum.FDD, DuplexModeEnum.FTDD, DuplexModeEnum.TDD })
				{
					driver.Configure.Scc.Dmode.Set(x);
					driver.Configure.Scc.Dmode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:BAND
				OperatingBandCenum value = driver.Configure.Scc.Band.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Scc.Band.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:BAND
				foreach (OperatingBandCenum x in new OperatingBandCenum[] { OperatingBandCenum.OB1, OperatingBandCenum.OB10, OperatingBandCenum.OB11, OperatingBandCenum.OB12, OperatingBandCenum.OB13, OperatingBandCenum.OB14, OperatingBandCenum.OB15, OperatingBandCenum.OB16, OperatingBandCenum.OB17, OperatingBandCenum.OB18, OperatingBandCenum.OB19, OperatingBandCenum.OB2, OperatingBandCenum.OB20, OperatingBandCenum.OB21, OperatingBandCenum.OB22, OperatingBandCenum.OB23, OperatingBandCenum.OB24, OperatingBandCenum.OB25, OperatingBandCenum.OB250, OperatingBandCenum.OB252, OperatingBandCenum.OB255, OperatingBandCenum.OB26, OperatingBandCenum.OB27, OperatingBandCenum.OB28, OperatingBandCenum.OB29, OperatingBandCenum.OB3, OperatingBandCenum.OB30, OperatingBandCenum.OB31, OperatingBandCenum.OB32, OperatingBandCenum.OB33, OperatingBandCenum.OB34, OperatingBandCenum.OB35, OperatingBandCenum.OB36, OperatingBandCenum.OB37, OperatingBandCenum.OB38, OperatingBandCenum.OB39, OperatingBandCenum.OB4, OperatingBandCenum.OB40, OperatingBandCenum.OB41, OperatingBandCenum.OB42, OperatingBandCenum.OB43, OperatingBandCenum.OB44, OperatingBandCenum.OB45, OperatingBandCenum.OB46, OperatingBandCenum.OB48, OperatingBandCenum.OB49, OperatingBandCenum.OB5, OperatingBandCenum.OB50, OperatingBandCenum.OB51, OperatingBandCenum.OB52, OperatingBandCenum.OB53, OperatingBandCenum.OB6, OperatingBandCenum.OB65, OperatingBandCenum.OB66, OperatingBandCenum.OB67, OperatingBandCenum.OB68, OperatingBandCenum.OB69, OperatingBandCenum.OB7, OperatingBandCenum.OB70, OperatingBandCenum.OB71, OperatingBandCenum.OB72, OperatingBandCenum.OB73, OperatingBandCenum.OB74, OperatingBandCenum.OB75, OperatingBandCenum.OB76, OperatingBandCenum.OB8, OperatingBandCenum.OB85, OperatingBandCenum.OB9, OperatingBandCenum.UDEFined })
				{
					driver.Configure.Scc.Band.Set(x);
					driver.Configure.Scc.Band.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:FSTRucture
				FrameStructureEnum value = driver.Configure.Scc.Fstructure.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Scc.Fstructure.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SCC<Carrier>:FSTRucture
				foreach (FrameStructureEnum x in new FrameStructureEnum[] { FrameStructureEnum.T1, FrameStructureEnum.T2, FrameStructureEnum.T3 })
				{
					driver.Configure.Scc.Fstructure.Set(x);
					driver.Configure.Scc.Fstructure.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SCC<Carrier>:CAGGregation:MODE
				CarrAggregationModeEnum value = driver.Configure.Scc.Caggregation.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Scc.Caggregation.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SCC<Carrier>:CAGGregation:MODE
				foreach (CarrAggregationModeEnum x in new CarrAggregationModeEnum[] { CarrAggregationModeEnum.INTRaband, CarrAggregationModeEnum.OFF })
				{
					driver.Configure.Scc.Caggregation.Mode.Set(x);
					driver.Configure.Scc.Caggregation.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:BAND
				foreach (OperatingBandCenum x in new OperatingBandCenum[] { OperatingBandCenum.OB1, OperatingBandCenum.OB10, OperatingBandCenum.OB11, OperatingBandCenum.OB12, OperatingBandCenum.OB13, OperatingBandCenum.OB14, OperatingBandCenum.OB15, OperatingBandCenum.OB16, OperatingBandCenum.OB17, OperatingBandCenum.OB18, OperatingBandCenum.OB19, OperatingBandCenum.OB2, OperatingBandCenum.OB20, OperatingBandCenum.OB21, OperatingBandCenum.OB22, OperatingBandCenum.OB23, OperatingBandCenum.OB24, OperatingBandCenum.OB25, OperatingBandCenum.OB250, OperatingBandCenum.OB252, OperatingBandCenum.OB255, OperatingBandCenum.OB26, OperatingBandCenum.OB27, OperatingBandCenum.OB28, OperatingBandCenum.OB29, OperatingBandCenum.OB3, OperatingBandCenum.OB30, OperatingBandCenum.OB31, OperatingBandCenum.OB32, OperatingBandCenum.OB33, OperatingBandCenum.OB34, OperatingBandCenum.OB35, OperatingBandCenum.OB36, OperatingBandCenum.OB37, OperatingBandCenum.OB38, OperatingBandCenum.OB39, OperatingBandCenum.OB4, OperatingBandCenum.OB40, OperatingBandCenum.OB41, OperatingBandCenum.OB42, OperatingBandCenum.OB43, OperatingBandCenum.OB44, OperatingBandCenum.OB45, OperatingBandCenum.OB46, OperatingBandCenum.OB48, OperatingBandCenum.OB49, OperatingBandCenum.OB5, OperatingBandCenum.OB50, OperatingBandCenum.OB51, OperatingBandCenum.OB52, OperatingBandCenum.OB53, OperatingBandCenum.OB6, OperatingBandCenum.OB65, OperatingBandCenum.OB66, OperatingBandCenum.OB67, OperatingBandCenum.OB68, OperatingBandCenum.OB69, OperatingBandCenum.OB7, OperatingBandCenum.OB70, OperatingBandCenum.OB71, OperatingBandCenum.OB72, OperatingBandCenum.OB73, OperatingBandCenum.OB74, OperatingBandCenum.OB75, OperatingBandCenum.OB76, OperatingBandCenum.OB8, OperatingBandCenum.OB85, OperatingBandCenum.OB9, OperatingBandCenum.UDEFined })
				{
					driver.Configure.Pcc.Band = x;
					OperatingBandCenum value = driver.Configure.Pcc.Band;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:FSTRucture
				foreach (FrameStructureEnum x in new FrameStructureEnum[] { FrameStructureEnum.T1, FrameStructureEnum.T2, FrameStructureEnum.T3 })
				{
					driver.Configure.Pcc.Fstructure = x;
					FrameStructureEnum value = driver.Configure.Pcc.Fstructure;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:DMODe:UCSPecific
				bool value = driver.Configure.Pcc.Dmode.UcSpecific;
				driver.Configure.Pcc.Dmode.UcSpecific = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:DMODe
				foreach (DuplexModeEnum x in new DuplexModeEnum[] { DuplexModeEnum.FDD, DuplexModeEnum.FTDD, DuplexModeEnum.TDD })
				{
					driver.Configure.Pcc.Dmode.Value = x;
					DuplexModeEnum value = driver.Configure.Pcc.Dmode.Value;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:ENABle
				bool value = driver.Configure.Pcc.Emtc.Enable;
				driver.Configure.Pcc.Emtc.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:MPDCch:SSPace
				foreach (SearchSpaceEnum x in new SearchSpaceEnum[] { SearchSpaceEnum.COMM, SearchSpaceEnum.UESP })
				{
					driver.Configure.Pcc.Emtc.Mpdcch.Sspace = x;
					SearchSpaceEnum value = driver.Configure.Pcc.Emtc.Mpdcch.Sspace;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:MPDCch:RLEVel
				foreach (RepetitionLevelEnum x in new RepetitionLevelEnum[] { RepetitionLevelEnum.RL1, RepetitionLevelEnum.RL2, RepetitionLevelEnum.RL3, RepetitionLevelEnum.RL4 })
				{
					driver.Configure.Pcc.Emtc.Mpdcch.Rlevel = x;
					RepetitionLevelEnum value = driver.Configure.Pcc.Emtc.Mpdcch.Rlevel;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:MPDCch:MREPetitions
				foreach (MpdcchRepetitionsEnum x in new MpdcchRepetitionsEnum[] { MpdcchRepetitionsEnum.MR1, MpdcchRepetitionsEnum.MR128, MpdcchRepetitionsEnum.MR16, MpdcchRepetitionsEnum.MR2, MpdcchRepetitionsEnum.MR256, MpdcchRepetitionsEnum.MR32, MpdcchRepetitionsEnum.MR4, MpdcchRepetitionsEnum.MR64, MpdcchRepetitionsEnum.MR8 })
				{
					driver.Configure.Pcc.Emtc.Mpdcch.Mrepetitions = x;
					MpdcchRepetitionsEnum value = driver.Configure.Pcc.Emtc.Mpdcch.Mrepetitions;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:MPDCch:MRPaging
				foreach (MpdcchRepetitionsEnum x in new MpdcchRepetitionsEnum[] { MpdcchRepetitionsEnum.MR1, MpdcchRepetitionsEnum.MR128, MpdcchRepetitionsEnum.MR16, MpdcchRepetitionsEnum.MR2, MpdcchRepetitionsEnum.MR256, MpdcchRepetitionsEnum.MR32, MpdcchRepetitionsEnum.MR4, MpdcchRepetitionsEnum.MR64, MpdcchRepetitionsEnum.MR8 })
				{
					driver.Configure.Pcc.Emtc.Mpdcch.MrPaging = x;
					MpdcchRepetitionsEnum value = driver.Configure.Pcc.Emtc.Mpdcch.MrPaging;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PDSCh:B:CERepetition
				foreach (CeRepetitionsBenum x in new CeRepetitionsBenum[] { CeRepetitionsBenum.R1, CeRepetitionsBenum.R1024, CeRepetitionsBenum.R128, CeRepetitionsBenum.R1536, CeRepetitionsBenum.R16, CeRepetitionsBenum.R192, CeRepetitionsBenum.R2048, CeRepetitionsBenum.R256, CeRepetitionsBenum.R32, CeRepetitionsBenum.R384, CeRepetitionsBenum.R4, CeRepetitionsBenum.R512, CeRepetitionsBenum.R64, CeRepetitionsBenum.R768, CeRepetitionsBenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pdsch.B.CeRepetition = x;
					CeRepetitionsBenum value = driver.Configure.Pcc.Emtc.Pdsch.B.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PDSCh:B:MRCE
				foreach (MpschBrepetitionsEnum x in new MpschBrepetitionsEnum[] { MpschBrepetitionsEnum.MR1024, MpschBrepetitionsEnum.MR1536, MpschBrepetitionsEnum.MR192, MpschBrepetitionsEnum.MR2048, MpschBrepetitionsEnum.MR256, MpschBrepetitionsEnum.MR384, MpschBrepetitionsEnum.MR512, MpschBrepetitionsEnum.MR768, MpschBrepetitionsEnum.NCON })
				{
					driver.Configure.Pcc.Emtc.Pdsch.B.Mrce = x;
					MpschBrepetitionsEnum value = driver.Configure.Pcc.Emtc.Pdsch.B.Mrce;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PDSCh:A:CERepetition
				foreach (CeRepetitionsAenum x in new CeRepetitionsAenum[] { CeRepetitionsAenum.R1, CeRepetitionsAenum.R16, CeRepetitionsAenum.R2, CeRepetitionsAenum.R32, CeRepetitionsAenum.R4, CeRepetitionsAenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pdsch.A.CeRepetition = x;
					CeRepetitionsAenum value = driver.Configure.Pcc.Emtc.Pdsch.A.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PDSCh:A:MRCE
				foreach (MpschArepetitionsEnum x in new MpschArepetitionsEnum[] { MpschArepetitionsEnum.MR16, MpschArepetitionsEnum.MR32, MpschArepetitionsEnum.NCON })
				{
					driver.Configure.Pcc.Emtc.Pdsch.A.Mrce = x;
					MpschArepetitionsEnum value = driver.Configure.Pcc.Emtc.Pdsch.A.Mrce;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUCCh:B:CERepetition
				foreach (CePucchRepsBenum x in new CePucchRepsBenum[] { CePucchRepsBenum.R128, CePucchRepsBenum.R16, CePucchRepsBenum.R32, CePucchRepsBenum.R4, CePucchRepsBenum.R64, CePucchRepsBenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pucch.B.CeRepetition = x;
					CePucchRepsBenum value = driver.Configure.Pcc.Emtc.Pucch.B.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUCCh:A:CERepetition
				foreach (CePucchRepsAenum x in new CePucchRepsAenum[] { CePucchRepsAenum.R1, CePucchRepsAenum.R2, CePucchRepsAenum.R4, CePucchRepsAenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pucch.A.CeRepetition = x;
					CePucchRepsAenum value = driver.Configure.Pcc.Emtc.Pucch.A.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUSCh:B:CERepetition
				foreach (CeRepetitionsBenum x in new CeRepetitionsBenum[] { CeRepetitionsBenum.R1, CeRepetitionsBenum.R1024, CeRepetitionsBenum.R128, CeRepetitionsBenum.R1536, CeRepetitionsBenum.R16, CeRepetitionsBenum.R192, CeRepetitionsBenum.R2048, CeRepetitionsBenum.R256, CeRepetitionsBenum.R32, CeRepetitionsBenum.R384, CeRepetitionsBenum.R4, CeRepetitionsBenum.R512, CeRepetitionsBenum.R64, CeRepetitionsBenum.R768, CeRepetitionsBenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pusch.B.CeRepetition = x;
					CeRepetitionsBenum value = driver.Configure.Pcc.Emtc.Pusch.B.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUSCh:B:MRCE
				foreach (MpschBrepetitionsEnum x in new MpschBrepetitionsEnum[] { MpschBrepetitionsEnum.MR1024, MpschBrepetitionsEnum.MR1536, MpschBrepetitionsEnum.MR192, MpschBrepetitionsEnum.MR2048, MpschBrepetitionsEnum.MR256, MpschBrepetitionsEnum.MR384, MpschBrepetitionsEnum.MR512, MpschBrepetitionsEnum.MR768, MpschBrepetitionsEnum.NCON })
				{
					driver.Configure.Pcc.Emtc.Pusch.B.Mrce = x;
					MpschBrepetitionsEnum value = driver.Configure.Pcc.Emtc.Pusch.B.Mrce;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUSCh:A:CERepetition
				foreach (CeRepetitionsAenum x in new CeRepetitionsAenum[] { CeRepetitionsAenum.R1, CeRepetitionsAenum.R16, CeRepetitionsAenum.R2, CeRepetitionsAenum.R32, CeRepetitionsAenum.R4, CeRepetitionsAenum.R8 })
				{
					driver.Configure.Pcc.Emtc.Pusch.A.CeRepetition = x;
					CeRepetitionsAenum value = driver.Configure.Pcc.Emtc.Pusch.A.CeRepetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:PUSCh:A:MRCE
				foreach (MpschArepetitionsEnum x in new MpschArepetitionsEnum[] { MpschArepetitionsEnum.MR16, MpschArepetitionsEnum.MR32, MpschArepetitionsEnum.NCON })
				{
					driver.Configure.Pcc.Emtc.Pusch.A.Mrce = x;
					MpschArepetitionsEnum value = driver.Configure.Pcc.Emtc.Pusch.A.Mrce;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:MODE
				foreach (CoverageEnhModeEnum x in new CoverageEnhModeEnum[] { CoverageEnhModeEnum.A, CoverageEnhModeEnum.B })
				{
					driver.Configure.Pcc.Emtc.Ce.Mode = x;
					CoverageEnhModeEnum value = driver.Configure.Pcc.Emtc.Ce.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:ILEVel
				foreach (IdleLevelEnum x in new IdleLevelEnum[] { IdleLevelEnum.LEV0, IdleLevelEnum.LEV1, IdleLevelEnum.LEV2, IdleLevelEnum.LEV3, IdleLevelEnum.UE })
				{
					driver.Configure.Pcc.Emtc.Ce.Ilevel = x;
					IdleLevelEnum value = driver.Configure.Pcc.Emtc.Ce.Ilevel;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:ENABle
				bool value = driver.Configure.Pcc.Emtc.Ce.Level.Enable.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:ENABle
				driver.Configure.Pcc.Emtc.Ce.Level.Enable.Set(1, false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:QRXLevmin
				int value = driver.Configure.Pcc.Emtc.Ce.Level.Qrxlevmin.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:QRXLevmin
				driver.Configure.Pcc.Emtc.Ce.Level.Qrxlevmin.Set(1, 1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:FOFFset
				int value = driver.Configure.Pcc.Emtc.Ce.Level.Prach.FreqOffset.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:FOFFset
				driver.Configure.Pcc.Emtc.Ce.Level.Prach.FreqOffset.Set(1, 1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:MPATtempts
				TransmitAttemptsEnum value = driver.Configure.Pcc.Emtc.Ce.Level.Prach.MpAttempts.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:MPATtempts
				driver.Configure.Pcc.Emtc.Ce.Level.Prach.MpAttempts.Set(1, TransmitAttemptsEnum.A10);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:RPATtempt
				PreambleTransmRepsEnum value = driver.Configure.Pcc.Emtc.Ce.Level.Prach.RpAttempt.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:RPATtempt
				driver.Configure.Pcc.Emtc.Ce.Level.Prach.RpAttempt.Set(1, PreambleTransmRepsEnum.R1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:MMRRepetitio
				MprachRepetitionsEnum value = driver.Configure.Pcc.Emtc.Ce.Level.Prach.MmrRepetition.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:MMRRepetitio
				driver.Configure.Pcc.Emtc.Ce.Level.Prach.MmrRepetition.Set(1, MprachRepetitionsEnum.R1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:CINDex
				int value = driver.Configure.Pcc.Emtc.Ce.Level.Prach.Cindex.Get(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:CE:LEVel:PRACh:CINDex
				driver.Configure.Pcc.Emtc.Ce.Level.Prach.Cindex.Set(1, 1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:UL:HOFFset
				int value = driver.Configure.Pcc.Emtc.Hopping.Uplink.Hoffset;
				driver.Configure.Pcc.Emtc.Hopping.Uplink.Hoffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:UL:ENABle
				bool value = driver.Configure.Pcc.Emtc.Hopping.Uplink.Enable;
				driver.Configure.Pcc.Emtc.Hopping.Uplink.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:UL:B:INTerval
				foreach (IntervalBenum x in new IntervalBenum[] { IntervalBenum.I16, IntervalBenum.I2, IntervalBenum.I4, IntervalBenum.I8 })
				{
					driver.Configure.Pcc.Emtc.Hopping.Uplink.B.Interval = x;
					IntervalBenum value = driver.Configure.Pcc.Emtc.Hopping.Uplink.B.Interval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:UL:A:INTerval
				foreach (IntervalAenum x in new IntervalAenum[] { IntervalAenum.I1, IntervalAenum.I2, IntervalAenum.I4, IntervalAenum.I8 })
				{
					driver.Configure.Pcc.Emtc.Hopping.Uplink.A.Interval = x;
					IntervalAenum value = driver.Configure.Pcc.Emtc.Hopping.Uplink.A.Interval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:DL:HOFFset
				int value = driver.Configure.Pcc.Emtc.Hopping.Downlink.Hoffset;
				driver.Configure.Pcc.Emtc.Hopping.Downlink.Hoffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:DL:ENABle
				bool value = driver.Configure.Pcc.Emtc.Hopping.Downlink.Enable;
				driver.Configure.Pcc.Emtc.Hopping.Downlink.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:DL:B:INTerval
				foreach (IntervalBenum x in new IntervalBenum[] { IntervalBenum.I16, IntervalBenum.I2, IntervalBenum.I4, IntervalBenum.I8 })
				{
					driver.Configure.Pcc.Emtc.Hopping.Downlink.B.Interval = x;
					IntervalBenum value = driver.Configure.Pcc.Emtc.Hopping.Downlink.B.Interval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>[:PCC]:EMTC:HOPPing:DL:A:INTerval
				foreach (IntervalAenum x in new IntervalAenum[] { IntervalAenum.I1, IntervalAenum.I2, IntervalAenum.I4, IntervalAenum.I8 })
				{
					driver.Configure.Pcc.Emtc.Hopping.Downlink.A.Interval = x;
					IntervalAenum value = driver.Configure.Pcc.Emtc.Hopping.Downlink.A.Interval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:MLOFfset
				int value = driver.Configure.RfSettings.Pcc.MixerLevelOffset;
				driver.Configure.RfSettings.Pcc.MixerLevelOffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDSeparation
				int value = driver.Configure.RfSettings.Pcc.UdSeparation;
				driver.Configure.RfSettings.Pcc.UdSeparation = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:ENPower
				double value = driver.Configure.RfSettings.Pcc.EnvelopePower;
				driver.Configure.RfSettings.Pcc.EnvelopePower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:ENPMode
				foreach (NominalPowerModeEnum x in new NominalPowerModeEnum[] { NominalPowerModeEnum.AUToranging, NominalPowerModeEnum.MANual, NominalPowerModeEnum.ULPC })
				{
					driver.Configure.RfSettings.Pcc.EnpMode = x;
					NominalPowerModeEnum value = driver.Configure.RfSettings.Pcc.EnpMode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UMARgin
				double value = driver.Configure.RfSettings.Pcc.Umargin;
				driver.Configure.RfSettings.Pcc.Umargin = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:AFBands:ALL
				RsCmwLteSig_Configure_RfSettings_Pcc_AfBands.All_Data value = driver.Configure.RfSettings.Pcc.AfBands.All;
				driver.Configure.RfSettings.Pcc.AfBands.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:UDSeparation
				int value = driver.Configure.RfSettings.Pcc.UserDefined.UdSeparation;
				driver.Configure.RfSettings.Pcc.UserDefined.UdSeparation = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:BINDicator
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Bindicator;
				driver.Configure.RfSettings.Pcc.UserDefined.Bindicator = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:CHANnel:DL:MINimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Channel.Downlink.Minimum;
				driver.Configure.RfSettings.Pcc.UserDefined.Channel.Downlink.Minimum = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:CHANnel:DL:MAXimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Channel.Downlink.Maximum;
				driver.Configure.RfSettings.Pcc.UserDefined.Channel.Downlink.Maximum = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:CHANnel:UL:MINimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Channel.Uplink.Minimum;
				driver.Configure.RfSettings.Pcc.UserDefined.Channel.Uplink.Minimum = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:CHANnel:UL:MAXimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Channel.Uplink.Maximum;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:FREQuency:DL:MINimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Frequency.Downlink.Minimum;
				driver.Configure.RfSettings.Pcc.UserDefined.Frequency.Downlink.Minimum = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:FREQuency:DL:MAXimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Frequency.Downlink.Maximum;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:FREQuency:UL:MINimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Frequency.Uplink.Minimum;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:UDEFined:FREQuency:UL:MAXimum
				int value = driver.Configure.RfSettings.Pcc.UserDefined.Frequency.Uplink.Maximum;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Pcc.Eattenuation.Input;
				driver.Configure.RfSettings.Pcc.Eattenuation.Input = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:EATTenuation:OUTPut<n>
				double value = driver.Configure.RfSettings.Pcc.Eattenuation.Output.Get(OutputRepCap.Default);
				value = driver.Configure.RfSettings.Pcc.Eattenuation.Output.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:EATTenuation:OUTPut<n>
				driver.Configure.RfSettings.Pcc.Eattenuation.Output.Set(1.0, OutputRepCap.Default);
				driver.Configure.RfSettings.Pcc.Eattenuation.Output.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:CHANnel:DL
				int value = driver.Configure.RfSettings.Pcc.Channel.Downlink;
				driver.Configure.RfSettings.Pcc.Channel.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:CHANnel:UL
				int value = driver.Configure.RfSettings.Pcc.Channel.Uplink;
				driver.Configure.RfSettings.Pcc.Channel.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:FOFFset:DL:UCSPecific
				bool value = driver.Configure.RfSettings.Pcc.FreqOffset.Downlink.UcSpecific;
				driver.Configure.RfSettings.Pcc.FreqOffset.Downlink.UcSpecific = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:FOFFset:DL
				int value = driver.Configure.RfSettings.Pcc.FreqOffset.Downlink.Value;
				driver.Configure.RfSettings.Pcc.FreqOffset.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:FOFFset:UL:UCSPecific
				bool value = driver.Configure.RfSettings.Pcc.FreqOffset.Uplink.UcSpecific;
				driver.Configure.RfSettings.Pcc.FreqOffset.Uplink.UcSpecific = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings[:PCC]:FOFFset:UL
				int value = driver.Configure.RfSettings.Pcc.FreqOffset.Uplink.Value;
				driver.Configure.RfSettings.Pcc.FreqOffset.Uplink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:BINDicator
				int value = driver.Configure.RfSettings.Scc.UserDefined.Bindicator.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Bindicator.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:BINDicator
				driver.Configure.RfSettings.Scc.UserDefined.Bindicator.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.Bindicator.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:DL:MINimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Minimum.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Minimum.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:DL:MINimum
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Minimum.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Minimum.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:DL:MAXimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Maximum.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Maximum.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:DL:MAXimum
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Maximum.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Downlink.Maximum.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:UL:MAXimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.GetMaximum(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.GetMaximum();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:UL:MINimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.Minimum.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.Minimum.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:CHANnel:UL:MINimum
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.Minimum.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.Channel.Uplink.Minimum.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:FREQuency:DL:MAXimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.GetMaximum(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.GetMaximum();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:FREQuency:DL:MINimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.Minimum.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.Minimum.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:FREQuency:DL:MINimum
				driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.Minimum.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.Frequency.Downlink.Minimum.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:FREQuency:UL:MINimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Uplink.GetMinimum(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Uplink.GetMinimum();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:FREQuency:UL:MAXimum
				int value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Uplink.GetMaximum(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.Frequency.Uplink.GetMaximum();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:UDSeparation
				int value = driver.Configure.RfSettings.Scc.UserDefined.UdSeparation.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UserDefined.UdSeparation.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDEFined:UDSeparation
				driver.Configure.RfSettings.Scc.UserDefined.UdSeparation.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UserDefined.UdSeparation.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:MLOFfset
				int value = driver.Configure.RfSettings.Scc.MixerLevelOffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.MixerLevelOffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:MLOFfset
				driver.Configure.RfSettings.Scc.MixerLevelOffset.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.MixerLevelOffset.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:INPut
				double value = driver.Configure.RfSettings.Scc.Eattenuation.Input.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.Eattenuation.Input.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:INPut
				driver.Configure.RfSettings.Scc.Eattenuation.Input.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.Eattenuation.Input.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:OUTPut<n>
				double value = driver.Configure.RfSettings.Scc.Eattenuation.Output.Get(SecondaryCompCarrierRepCap.Default, OutputRepCap.Default);
				value = driver.Configure.RfSettings.Scc.Eattenuation.Output.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:EATTenuation:OUTPut<n>
				driver.Configure.RfSettings.Scc.Eattenuation.Output.Set(1.0, SecondaryCompCarrierRepCap.Default, OutputRepCap.Default);
				driver.Configure.RfSettings.Scc.Eattenuation.Output.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:CHANnel:DL
				int value = driver.Configure.RfSettings.Scc.Channel.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.Channel.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:CHANnel:DL
				driver.Configure.RfSettings.Scc.Channel.Downlink.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.Channel.Downlink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:CHANnel:UL
				int value = driver.Configure.RfSettings.Scc.Channel.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.Channel.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:CHANnel:UL
				driver.Configure.RfSettings.Scc.Channel.Uplink.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.Channel.Uplink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:FOFFset:DL
				int value = driver.Configure.RfSettings.Scc.FreqOffset.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.FreqOffset.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:FOFFset:DL
				driver.Configure.RfSettings.Scc.FreqOffset.Downlink.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.FreqOffset.Downlink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:FOFFset:UL
				int value = driver.Configure.RfSettings.Scc.FreqOffset.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.FreqOffset.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:FOFFset:UL
				driver.Configure.RfSettings.Scc.FreqOffset.Uplink.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.FreqOffset.Uplink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDSeparation
				int value = driver.Configure.RfSettings.Scc.UdSeparation.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.UdSeparation.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UDSeparation
				driver.Configure.RfSettings.Scc.UdSeparation.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.UdSeparation.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:ENPower
				double value = driver.Configure.RfSettings.Scc.EnvelopePower.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.EnvelopePower.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:ENPower
				driver.Configure.RfSettings.Scc.EnvelopePower.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.EnvelopePower.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:ENPMode
				NominalPowerModeEnum value = driver.Configure.RfSettings.Scc.EnpMode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.EnpMode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:ENPMode
				foreach (NominalPowerModeEnum x in new NominalPowerModeEnum[] { NominalPowerModeEnum.AUToranging, NominalPowerModeEnum.MANual, NominalPowerModeEnum.ULPC })
				{
					driver.Configure.RfSettings.Scc.EnpMode.Set(x);
					driver.Configure.RfSettings.Scc.EnpMode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UMARgin
				double value = driver.Configure.RfSettings.Scc.Umargin.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.RfSettings.Scc.Umargin.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:SCC<Carrier>:UMARgin
				driver.Configure.RfSettings.Scc.Umargin.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.RfSettings.Scc.Umargin.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:EDC:OUTPut
				double value = driver.Configure.RfSettings.Edc.Output;
				driver.Configure.RfSettings.Edc.Output = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:EDC:INPut
				double value = driver.Configure.RfSettings.Edc.Input;
				driver.Configure.RfSettings.Edc.Input = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:RFSettings:ALL:BWCHannel
				RsCmwLteSig_Configure_RfSettings_All.BwChannel_Data value = driver.Configure.RfSettings.All.BwChannel;
				driver.Configure.RfSettings.All.BwChannel = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:IQIN[:PCC]:PATH<n>
				RsCmwLteSig_Configure_IqIn_Pcc_Path.Path_Data value = driver.Configure.IqIn.Pcc.Path.Get(PathRepCap.Default);
				value = driver.Configure.IqIn.Pcc.Path.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:IQIN[:PCC]:PATH<n>
				RsCmwLteSig_Configure_IqIn_Pcc_Path.Path_Data value = new RsCmwLteSig_Configure_IqIn_Pcc_Path.Path_Data();
				driver.Configure.IqIn.Pcc.Path.Set(value, PathRepCap.Default);
				driver.Configure.IqIn.Pcc.Path.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:IQIN:SCC<Carrier>:PATH<n>
				RsCmwLteSig_Configure_IqIn_Scc_Path.Path_Data value = driver.Configure.IqIn.Scc.Path.Get(SecondaryCompCarrierRepCap.Default, PathRepCap.Default);
				value = driver.Configure.IqIn.Scc.Path.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:IQIN:SCC<Carrier>:PATH<n>
				RsCmwLteSig_Configure_IqIn_Scc_Path.Path_Data value = new RsCmwLteSig_Configure_IqIn_Scc_Path.Path_Data();
				driver.Configure.IqIn.Scc.Path.Set(value, SecondaryCompCarrierRepCap.Default, PathRepCap.Default);
				driver.Configure.IqIn.Scc.Path.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT
				List<double> value = driver.Configure.Fading.Scc.FadingSimulator.GetHmat(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.GetHmat();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Scc.FadingSimulator.Globale.Seed.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Globale.Seed.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:GLOBal:SEED
				driver.Configure.Fading.Scc.FadingSimulator.Globale.Seed.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Globale.Seed.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Scc.FadingSimulator.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ENABle
				driver.Configure.Fading.Scc.FadingSimulator.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:BYPass:STATe
				bool value = driver.Configure.Fading.Scc.FadingSimulator.Bypass.State.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Bypass.State.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:BYPass:STATe
				driver.Configure.Fading.Scc.FadingSimulator.Bypass.State.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Bypass.State.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:STANdard:ENABle
				bool value = driver.Configure.Fading.Scc.FadingSimulator.Standard.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Standard.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:STANdard:ENABle
				driver.Configure.Fading.Scc.FadingSimulator.Standard.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Standard.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:STANdard:PROFile
				FadingProfileEnum value = driver.Configure.Fading.Scc.FadingSimulator.Standard.Profile.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Standard.Profile.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:STANdard:PROFile
				foreach (FadingProfileEnum x in new FadingProfileEnum[] { FadingProfileEnum.CTESt, FadingProfileEnum.EP5High, FadingProfileEnum.EP5Low, FadingProfileEnum.EP5Medium, FadingProfileEnum.ET3High, FadingProfileEnum.ET3Low, FadingProfileEnum.ET3Medium, FadingProfileEnum.ET7High, FadingProfileEnum.ET7Low, FadingProfileEnum.ET7Medium, FadingProfileEnum.ETH30, FadingProfileEnum.ETL30, FadingProfileEnum.ETM30, FadingProfileEnum.EV5High, FadingProfileEnum.EV5Low, FadingProfileEnum.EV5Medium, FadingProfileEnum.EV7High, FadingProfileEnum.EV7Low, FadingProfileEnum.EV7Medium, FadingProfileEnum.EVH200, FadingProfileEnum.EVL200, FadingProfileEnum.EVM200, FadingProfileEnum.HST, FadingProfileEnum.HST2, FadingProfileEnum.HSTRain, FadingProfileEnum.IILS, FadingProfileEnum.IINL, FadingProfileEnum.IRALos, FadingProfileEnum.IRANlos, FadingProfileEnum.ISALos, FadingProfileEnum.ISANlos, FadingProfileEnum.IUALos, FadingProfileEnum.IUANlos, FadingProfileEnum.IULS, FadingProfileEnum.IUNLos1, FadingProfileEnum.IUNLos2, FadingProfileEnum.UMA3, FadingProfileEnum.UMA30, FadingProfileEnum.UMI3, FadingProfileEnum.UMI30, FadingProfileEnum.USER })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Standard.Profile.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Standard.Profile.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:RESTart
				driver.Configure.Fading.Scc.FadingSimulator.Restart.Set(SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Restart.SetAndWait(SecondaryCompCarrierRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:RESTart:MODE
				RestartModeEnum value = driver.Configure.Fading.Scc.FadingSimulator.Restart.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Restart.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:RESTart:MODE
				foreach (RestartModeEnum x in new RestartModeEnum[] { RestartModeEnum.AUTO, RestartModeEnum.MANual, RestartModeEnum.TRIGger })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Restart.Mode.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Restart.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:PROFile
				FadingProfileEnum value = driver.Configure.Fading.Scc.FadingSimulator.Profile.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Profile.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:PROFile
				foreach (FadingProfileEnum x in new FadingProfileEnum[] { FadingProfileEnum.CTESt, FadingProfileEnum.EP5High, FadingProfileEnum.EP5Low, FadingProfileEnum.EP5Medium, FadingProfileEnum.ET3High, FadingProfileEnum.ET3Low, FadingProfileEnum.ET3Medium, FadingProfileEnum.ET7High, FadingProfileEnum.ET7Low, FadingProfileEnum.ET7Medium, FadingProfileEnum.ETH30, FadingProfileEnum.ETL30, FadingProfileEnum.ETM30, FadingProfileEnum.EV5High, FadingProfileEnum.EV5Low, FadingProfileEnum.EV5Medium, FadingProfileEnum.EV7High, FadingProfileEnum.EV7Low, FadingProfileEnum.EV7Medium, FadingProfileEnum.EVH200, FadingProfileEnum.EVL200, FadingProfileEnum.EVM200, FadingProfileEnum.HST, FadingProfileEnum.HST2, FadingProfileEnum.HSTRain, FadingProfileEnum.IILS, FadingProfileEnum.IINL, FadingProfileEnum.IRALos, FadingProfileEnum.IRANlos, FadingProfileEnum.ISALos, FadingProfileEnum.ISANlos, FadingProfileEnum.IUALos, FadingProfileEnum.IUANlos, FadingProfileEnum.IULS, FadingProfileEnum.IUNLos1, FadingProfileEnum.IUNLos2, FadingProfileEnum.UMA3, FadingProfileEnum.UMA30, FadingProfileEnum.UMI3, FadingProfileEnum.UMI30, FadingProfileEnum.USER })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Profile.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Profile.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ILOSs:MODE
				InsertLossModeEnum value = driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.LACP, InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Mode.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Loss.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Loss.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ILOSs:LOSS
				driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Loss.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.InsertionLoss.Loss.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:DSHift
				double value = driver.Configure.Fading.Scc.FadingSimulator.Dshift.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Dshift.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:DSHift
				driver.Configure.Fading.Scc.FadingSimulator.Dshift.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Dshift.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:DSHift:MODE
				FadingModeEnum value = driver.Configure.Fading.Scc.FadingSimulator.Dshift.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Dshift.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:DSHift:MODE
				foreach (FadingModeEnum x in new FadingModeEnum[] { FadingModeEnum.NORMal, FadingModeEnum.USER })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Dshift.Mode.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Dshift.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:MATRix:MODE
				FadingMatrixModeEnum value = driver.Configure.Fading.Scc.FadingSimulator.Matrix.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Matrix.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:MATRix:MODE
				foreach (FadingMatrixModeEnum x in new FadingMatrixModeEnum[] { FadingMatrixModeEnum.KRONecker, FadingMatrixModeEnum.NORMal, FadingMatrixModeEnum.SCWI })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Matrix.Mode.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Matrix.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT:RST
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Rst.Set(SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Rst.SetAndWait(SecondaryCompCarrierRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT:ROW<row>:COL<col>:IMAG
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Row.Col.Imag.Set(1.0, SecondaryCompCarrierRepCap.Default, HMatrixRowRepCap.Default, HMatrixColumnRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Row.Col.Imag.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT:ROW<row>:COL<col>:REAL
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Row.Col.Real.Set(1.0, SecondaryCompCarrierRepCap.Default, HMatrixRowRepCap.Default, HMatrixColumnRepCap.Default);
				driver.Configure.Fading.Scc.FadingSimulator.Hmat.Row.Col.Real.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT:MODE
				FadingModeEnum value = driver.Configure.Fading.Scc.FadingSimulator.Hmat.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.FadingSimulator.Hmat.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:HMAT:MODE
				foreach (FadingModeEnum x in new FadingModeEnum[] { FadingModeEnum.NORMal, FadingModeEnum.USER })
				{
					driver.Configure.Fading.Scc.FadingSimulator.Hmat.Mode.Set(x);
					driver.Configure.Fading.Scc.FadingSimulator.Hmat.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:ENABle
				bool value = driver.Configure.Fading.Scc.Awgn.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:ENABle
				driver.Configure.Fading.Scc.Awgn.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.Awgn.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:FOFFset
				double value = driver.Configure.Fading.Scc.Awgn.FreqOffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.FreqOffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:FOFFset
				driver.Configure.Fading.Scc.Awgn.FreqOffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.Awgn.FreqOffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:BWIDth:NOISe
				double value = driver.Configure.Fading.Scc.Awgn.Bandwidth.GetNoise(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.Bandwidth.GetNoise();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Scc.Awgn.Bandwidth.Ratio.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.Bandwidth.Ratio.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:BWIDth:RATio
				driver.Configure.Fading.Scc.Awgn.Bandwidth.Ratio.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.Awgn.Bandwidth.Ratio.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:SNRatio
				double value = driver.Configure.Fading.Scc.Awgn.SnRatio.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.SnRatio.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:SNRatio
				driver.Configure.Fading.Scc.Awgn.SnRatio.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Fading.Scc.Awgn.SnRatio.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:MEASurement
				AwgnMeasurementEnum value = driver.Configure.Fading.Scc.Awgn.Measurement.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Awgn.Measurement.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:AWGN:MEASurement
				foreach (AwgnMeasurementEnum x in new AwgnMeasurementEnum[] { AwgnMeasurementEnum.NOISe, AwgnMeasurementEnum.OFF, AwgnMeasurementEnum.SIGNal })
				{
					driver.Configure.Fading.Scc.Awgn.Measurement.Set(x);
					driver.Configure.Fading.Scc.Awgn.Measurement.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:POWer:SIGNal
				double value = driver.Configure.Fading.Scc.Power.GetSignal(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Power.GetSignal();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:POWer:NOISe
				double value = driver.Configure.Fading.Scc.Power.GetNoise(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Power.GetNoise();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:POWer:SUM
				double value = driver.Configure.Fading.Scc.Power.GetSum(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Power.GetSum();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Scc.Power.Noise.GetTotal(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Fading.Scc.Power.Noise.GetTotal();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:KCONstant
				foreach (KeepConstantEnum x in new KeepConstantEnum[] { KeepConstantEnum.DSHift, KeepConstantEnum.SPEed })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Kconstant = x;
					KeepConstantEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Kconstant;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:ENABle
				bool value = driver.Configure.Fading.Pcc.FadingSimulator.Enable;
				driver.Configure.Fading.Pcc.FadingSimulator.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:PROFile
				foreach (FadingProfileEnum x in new FadingProfileEnum[] { FadingProfileEnum.CTESt, FadingProfileEnum.EP5High, FadingProfileEnum.EP5Low, FadingProfileEnum.EP5Medium, FadingProfileEnum.ET3High, FadingProfileEnum.ET3Low, FadingProfileEnum.ET3Medium, FadingProfileEnum.ET7High, FadingProfileEnum.ET7Low, FadingProfileEnum.ET7Medium, FadingProfileEnum.ETH30, FadingProfileEnum.ETL30, FadingProfileEnum.ETM30, FadingProfileEnum.EV5High, FadingProfileEnum.EV5Low, FadingProfileEnum.EV5Medium, FadingProfileEnum.EV7High, FadingProfileEnum.EV7Low, FadingProfileEnum.EV7Medium, FadingProfileEnum.EVH200, FadingProfileEnum.EVL200, FadingProfileEnum.EVM200, FadingProfileEnum.HST, FadingProfileEnum.HST2, FadingProfileEnum.HSTRain, FadingProfileEnum.IILS, FadingProfileEnum.IINL, FadingProfileEnum.IRALos, FadingProfileEnum.IRANlos, FadingProfileEnum.ISALos, FadingProfileEnum.ISANlos, FadingProfileEnum.IUALos, FadingProfileEnum.IUANlos, FadingProfileEnum.IULS, FadingProfileEnum.IUNLos1, FadingProfileEnum.IUNLos2, FadingProfileEnum.UMA3, FadingProfileEnum.UMA30, FadingProfileEnum.UMI3, FadingProfileEnum.UMI30, FadingProfileEnum.USER })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Profile = x;
					FadingProfileEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Profile;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:GLOBal:SEED
				int value = driver.Configure.Fading.Pcc.FadingSimulator.Globale.Seed;
				driver.Configure.Fading.Pcc.FadingSimulator.Globale.Seed = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:BYPass:STATe
				bool value = driver.Configure.Fading.Pcc.FadingSimulator.Bypass.State;
				driver.Configure.Fading.Pcc.FadingSimulator.Bypass.State = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:STANdard:ENABle
				bool value = driver.Configure.Fading.Pcc.FadingSimulator.Standard.Enable;
				driver.Configure.Fading.Pcc.FadingSimulator.Standard.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:STANdard:PROFile
				foreach (FadingProfileEnum x in new FadingProfileEnum[] { FadingProfileEnum.CTESt, FadingProfileEnum.EP5High, FadingProfileEnum.EP5Low, FadingProfileEnum.EP5Medium, FadingProfileEnum.ET3High, FadingProfileEnum.ET3Low, FadingProfileEnum.ET3Medium, FadingProfileEnum.ET7High, FadingProfileEnum.ET7Low, FadingProfileEnum.ET7Medium, FadingProfileEnum.ETH30, FadingProfileEnum.ETL30, FadingProfileEnum.ETM30, FadingProfileEnum.EV5High, FadingProfileEnum.EV5Low, FadingProfileEnum.EV5Medium, FadingProfileEnum.EV7High, FadingProfileEnum.EV7Low, FadingProfileEnum.EV7Medium, FadingProfileEnum.EVH200, FadingProfileEnum.EVL200, FadingProfileEnum.EVM200, FadingProfileEnum.HST, FadingProfileEnum.HST2, FadingProfileEnum.HSTRain, FadingProfileEnum.IILS, FadingProfileEnum.IINL, FadingProfileEnum.IRALos, FadingProfileEnum.IRANlos, FadingProfileEnum.ISALos, FadingProfileEnum.ISANlos, FadingProfileEnum.IUALos, FadingProfileEnum.IUANlos, FadingProfileEnum.IULS, FadingProfileEnum.IUNLos1, FadingProfileEnum.IUNLos2, FadingProfileEnum.UMA3, FadingProfileEnum.UMA30, FadingProfileEnum.UMI3, FadingProfileEnum.UMI30, FadingProfileEnum.USER })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Standard.Profile = x;
					FadingProfileEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Standard.Profile;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:RESTart:MODE
				foreach (RestartModeEnum x in new RestartModeEnum[] { RestartModeEnum.AUTO, RestartModeEnum.MANual, RestartModeEnum.TRIGger })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Restart.Mode = x;
					RestartModeEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Restart.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:RESTart
				driver.Configure.Fading.Pcc.FadingSimulator.Restart.Set();
				driver.Configure.Fading.Pcc.FadingSimulator.Restart.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:ILOSs:MODE
				foreach (InsertLossModeEnum x in new InsertLossModeEnum[] { InsertLossModeEnum.LACP, InsertLossModeEnum.NORMal, InsertLossModeEnum.USER })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.InsertionLoss.Mode = x;
					InsertLossModeEnum value = driver.Configure.Fading.Pcc.FadingSimulator.InsertionLoss.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:ILOSs:LOSS
				double value = driver.Configure.Fading.Pcc.FadingSimulator.InsertionLoss.Loss;
				driver.Configure.Fading.Pcc.FadingSimulator.InsertionLoss.Loss = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:DSHift:MODE
				foreach (FadingModeEnum x in new FadingModeEnum[] { FadingModeEnum.NORMal, FadingModeEnum.USER })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Dshift.Mode = x;
					FadingModeEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Dshift.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:DSHift
				double value = driver.Configure.Fading.Pcc.FadingSimulator.Dshift.Value;
				driver.Configure.Fading.Pcc.FadingSimulator.Dshift.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:MATRix:MODE
				foreach (FadingMatrixModeEnum x in new FadingMatrixModeEnum[] { FadingMatrixModeEnum.KRONecker, FadingMatrixModeEnum.NORMal, FadingMatrixModeEnum.SCWI })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Matrix.Mode = x;
					FadingMatrixModeEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Matrix.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:HMAT:MODE
				foreach (FadingModeEnum x in new FadingModeEnum[] { FadingModeEnum.NORMal, FadingModeEnum.USER })
				{
					driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Mode = x;
					FadingModeEnum value = driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:HMAT
				List<double> value = driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:HMAT:RST
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Rst.Set();
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Rst.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:HMAT:ROW<row>:COL<col>:IMAG
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Row.Col.Imag.Set(1.0, HMatrixRowRepCap.Default, HMatrixColumnRepCap.Default);
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Row.Col.Imag.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:HMAT:ROW<row>:COL<col>:REAL
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Row.Col.Real.Set(1.0, HMatrixRowRepCap.Default, HMatrixColumnRepCap.Default);
				driver.Configure.Fading.Pcc.FadingSimulator.Hmat.Row.Col.Real.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:ENABle
				bool value = driver.Configure.Fading.Pcc.Awgn.Enable;
				driver.Configure.Fading.Pcc.Awgn.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:FOFFset
				double value = driver.Configure.Fading.Pcc.Awgn.FreqOffset;
				driver.Configure.Fading.Pcc.Awgn.FreqOffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:SNRatio
				double value = driver.Configure.Fading.Pcc.Awgn.SnRatio;
				driver.Configure.Fading.Pcc.Awgn.SnRatio = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:MEASurement
				foreach (AwgnMeasurementEnum x in new AwgnMeasurementEnum[] { AwgnMeasurementEnum.NOISe, AwgnMeasurementEnum.OFF, AwgnMeasurementEnum.SIGNal })
				{
					driver.Configure.Fading.Pcc.Awgn.Measurement = x;
					AwgnMeasurementEnum value = driver.Configure.Fading.Pcc.Awgn.Measurement;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:BWIDth:RATio
				double value = driver.Configure.Fading.Pcc.Awgn.Bandwidth.Ratio;
				driver.Configure.Fading.Pcc.Awgn.Bandwidth.Ratio = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:AWGN:BWIDth:NOISe
				double value = driver.Configure.Fading.Pcc.Awgn.Bandwidth.Noise;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:POWer:SIGNal
				double value = driver.Configure.Fading.Pcc.Power.Signal;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:POWer:SUM
				double value = driver.Configure.Fading.Pcc.Power.Sum;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:POWer:NOISe:TOTal
				double value = driver.Configure.Fading.Pcc.Power.Noise.Total;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:FADing[:PCC]:POWer:NOISe
				double value = driver.Configure.Fading.Pcc.Power.Noise.Value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CAGGregation:SET
				RsCmwLteSig_Configure_Caggregation.Set_Data value = driver.Configure.Caggregation.Set;
				driver.Configure.Caggregation.Set = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:ALL:THResholds:LOW
				RsCmwLteSig_Configure_Ncell_All_Thresholds.Low_Data value = driver.Configure.Ncell.All.Thresholds.Low;
				driver.Configure.Ncell.All.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:ALL:THResholds
				RsCmwLteSig_Configure_Ncell_All_Thresholds.Value_Data value = driver.Configure.Ncell.All.Thresholds.Value;
				driver.Configure.Ncell.All.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwLteSig_Configure_Ncell_Lte_Cell.Cell_Data value = driver.Configure.Ncell.Lte.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Lte.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:LTE:CELL<n>
				RsCmwLteSig_Configure_Ncell_Lte_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Lte_Cell.Cell_Data();
				driver.Configure.Ncell.Lte.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Lte.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:LTE:THResholds:LOW
				int value = driver.Configure.Ncell.Lte.Thresholds.Low;
				driver.Configure.Ncell.Lte.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:LTE:THResholds
				RsCmwLteSig_Configure_Ncell_Lte_Thresholds.Value_Data value = driver.Configure.Ncell.Lte.Thresholds.Value;
				driver.Configure.Ncell.Lte.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:GSM:CELL<n>
				RsCmwLteSig_Configure_Ncell_Gsm_Cell.Cell_Data value = driver.Configure.Ncell.Gsm.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Gsm.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:GSM:CELL<n>
				RsCmwLteSig_Configure_Ncell_Gsm_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Gsm_Cell.Cell_Data();
				driver.Configure.Ncell.Gsm.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Gsm.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:GSM:THResholds:LOW
				int value = driver.Configure.Ncell.Gsm.Thresholds.Low;
				driver.Configure.Ncell.Gsm.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:GSM:THResholds
				RsCmwLteSig_Configure_Ncell_Gsm_Thresholds.Value_Data value = driver.Configure.Ncell.Gsm.Thresholds.Value;
				driver.Configure.Ncell.Gsm.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:WCDMa:CELL<n>
				RsCmwLteSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = driver.Configure.Ncell.Wcdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Wcdma.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:WCDMa:CELL<n>
				RsCmwLteSig_Configure_Ncell_Wcdma_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Wcdma_Cell.Cell_Data();
				driver.Configure.Ncell.Wcdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Wcdma.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:WCDMa:THResholds:LOW
				int value = driver.Configure.Ncell.Wcdma.Thresholds.Low;
				driver.Configure.Ncell.Wcdma.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:WCDMa:THResholds
				RsCmwLteSig_Configure_Ncell_Wcdma_Thresholds.Value_Data value = driver.Configure.Ncell.Wcdma.Thresholds.Value;
				driver.Configure.Ncell.Wcdma.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:CDMA:CELL<n>
				RsCmwLteSig_Configure_Ncell_Cdma_Cell.Cell_Data value = driver.Configure.Ncell.Cdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Cdma.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:CDMA:CELL<n>
				RsCmwLteSig_Configure_Ncell_Cdma_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Cdma_Cell.Cell_Data();
				driver.Configure.Ncell.Cdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Cdma.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:CDMA:THResholds:LOW
				int value = driver.Configure.Ncell.Cdma.Thresholds.Low;
				driver.Configure.Ncell.Cdma.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:CDMA:THResholds
				RsCmwLteSig_Configure_Ncell_Cdma_Thresholds.Value_Data value = driver.Configure.Ncell.Cdma.Thresholds.Value;
				driver.Configure.Ncell.Cdma.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:EVDO:CELL<n>
				RsCmwLteSig_Configure_Ncell_Evdo_Cell.Cell_Data value = driver.Configure.Ncell.Evdo.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Evdo.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:EVDO:CELL<n>
				RsCmwLteSig_Configure_Ncell_Evdo_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Evdo_Cell.Cell_Data();
				driver.Configure.Ncell.Evdo.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Evdo.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:EVDO:THResholds:LOW
				int value = driver.Configure.Ncell.Evdo.Thresholds.Low;
				driver.Configure.Ncell.Evdo.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:EVDO:THResholds
				RsCmwLteSig_Configure_Ncell_Evdo_Thresholds.Value_Data value = driver.Configure.Ncell.Evdo.Thresholds.Value;
				driver.Configure.Ncell.Evdo.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:TDSCdma:CELL<n>
				RsCmwLteSig_Configure_Ncell_Tdscdma_Cell.Cell_Data value = driver.Configure.Ncell.Tdscdma.Cell.Get(CellNoRepCap.Default);
				value = driver.Configure.Ncell.Tdscdma.Cell.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:TDSCdma:CELL<n>
				RsCmwLteSig_Configure_Ncell_Tdscdma_Cell.Cell_Data value = new RsCmwLteSig_Configure_Ncell_Tdscdma_Cell.Cell_Data();
				driver.Configure.Ncell.Tdscdma.Cell.Set(value, CellNoRepCap.Default);
				driver.Configure.Ncell.Tdscdma.Cell.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:TDSCdma:THResholds:LOW
				int value = driver.Configure.Ncell.Tdscdma.Thresholds.Low;
				driver.Configure.Ncell.Tdscdma.Thresholds.Low = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:NCELl:TDSCdma:THResholds
				RsCmwLteSig_Configure_Ncell_Tdscdma_Thresholds.Value_Data value = driver.Configure.Ncell.Tdscdma.Thresholds.Value;
				driver.Configure.Ncell.Tdscdma.Thresholds.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:A:SCC<Carrier>:ENABle
				bool value = driver.Configure.A.Scc.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.A.Scc.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:A:SCC<Carrier>:ENABle
				driver.Configure.A.Scc.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.A.Scc.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:B:SCC<Carrier>:ENABle
				bool value = driver.Configure.B.Scc.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.B.Scc.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:B:SCC<Carrier>:ENABle
				driver.Configure.B.Scc.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.B.Scc.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:RSEPre:LEVel
				double value = driver.Configure.Downlink.Scc.Rsepre.Level.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Rsepre.Level.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:RSEPre:LEVel
				driver.Configure.Downlink.Scc.Rsepre.Level.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Rsepre.Level.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PSS:POFFset
				double value = driver.Configure.Downlink.Scc.Pss.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pss.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PSS:POFFset
				driver.Configure.Downlink.Scc.Pss.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Pss.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:SSS:POFFset
				double value = driver.Configure.Downlink.Scc.Sss.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Sss.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:SSS:POFFset
				driver.Configure.Downlink.Scc.Sss.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Sss.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PBCH:POFFset
				double value = driver.Configure.Downlink.Scc.Pbch.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pbch.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PBCH:POFFset
				driver.Configure.Downlink.Scc.Pbch.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Pbch.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PCFich:POFFset
				double value = driver.Configure.Downlink.Scc.Pcfich.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pcfich.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PCFich:POFFset
				driver.Configure.Downlink.Scc.Pcfich.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Pcfich.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PHICh:POFFset
				double value = driver.Configure.Downlink.Scc.Phich.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Phich.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PHICh:POFFset
				driver.Configure.Downlink.Scc.Phich.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Phich.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDCCh:POFFset
				double value = driver.Configure.Downlink.Scc.Pdcch.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pdcch.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDCCh:POFFset
				driver.Configure.Downlink.Scc.Pdcch.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Pdcch.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDSCh:PA
				PowerOffsetEnum value = driver.Configure.Downlink.Scc.Pdsch.Pa.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pdsch.Pa.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDSCh:PA
				foreach (PowerOffsetEnum x in new PowerOffsetEnum[] { PowerOffsetEnum.N3DB, PowerOffsetEnum.N6DB, PowerOffsetEnum.ZERO })
				{
					driver.Configure.Downlink.Scc.Pdsch.Pa.Set(x);
					driver.Configure.Downlink.Scc.Pdsch.Pa.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDSCh:RINDex
				int value = driver.Configure.Downlink.Scc.Pdsch.Rindex.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Pdsch.Rindex.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:PDSCh:RINDex
				driver.Configure.Downlink.Scc.Pdsch.Rindex.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Pdsch.Rindex.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:CSIRs:MODE
				CsirsModeEnum value = driver.Configure.Downlink.Scc.Csirs.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Csirs.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:CSIRs:MODE
				foreach (CsirsModeEnum x in new CsirsModeEnum[] { CsirsModeEnum.ACSirs, CsirsModeEnum.MANual })
				{
					driver.Configure.Downlink.Scc.Csirs.Mode.Set(x);
					driver.Configure.Downlink.Scc.Csirs.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:CSIRs:POFFset
				double value = driver.Configure.Downlink.Scc.Csirs.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Csirs.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:CSIRs:POFFset
				driver.Configure.Downlink.Scc.Csirs.Poffset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Csirs.Poffset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:OCNG
				bool value = driver.Configure.Downlink.Scc.Ocng.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Ocng.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:OCNG
				driver.Configure.Downlink.Scc.Ocng.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Ocng.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:AWGN
				double value = driver.Configure.Downlink.Scc.Awgn.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Awgn.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<Carrier>:AWGN
				driver.Configure.Downlink.Scc.Awgn.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Awgn.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<carrier>:POWer:PORTs
				int value = driver.Configure.Downlink.Scc.Power.Ports.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Downlink.Scc.Power.Ports.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL:SCC<carrier>:POWer:PORTs
				driver.Configure.Downlink.Scc.Power.Ports.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Downlink.Scc.Power.Ports.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:OCNG
				bool value = driver.Configure.Downlink.Pcc.Ocng;
				driver.Configure.Downlink.Pcc.Ocng = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:AWGN
				double value = driver.Configure.Downlink.Pcc.Awgn;
				driver.Configure.Downlink.Pcc.Awgn = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:RSEPre:LEVel
				double value = driver.Configure.Downlink.Pcc.Rsepre.Level;
				driver.Configure.Downlink.Pcc.Rsepre.Level = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PSS:POFFset
				double value = driver.Configure.Downlink.Pcc.Pss.Poffset;
				driver.Configure.Downlink.Pcc.Pss.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:SSS:POFFset
				double value = driver.Configure.Downlink.Pcc.Sss.Poffset;
				driver.Configure.Downlink.Pcc.Sss.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PBCH:POFFset
				double value = driver.Configure.Downlink.Pcc.Pbch.Poffset;
				driver.Configure.Downlink.Pcc.Pbch.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PCFich:POFFset
				double value = driver.Configure.Downlink.Pcc.Pcfich.Poffset;
				driver.Configure.Downlink.Pcc.Pcfich.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PHICh:POFFset
				double value = driver.Configure.Downlink.Pcc.Phich.Poffset;
				driver.Configure.Downlink.Pcc.Phich.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PDCCh:POFFset
				double value = driver.Configure.Downlink.Pcc.Pdcch.Poffset;
				driver.Configure.Downlink.Pcc.Pdcch.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PDSCh:PA
				foreach (PowerOffsetEnum x in new PowerOffsetEnum[] { PowerOffsetEnum.N3DB, PowerOffsetEnum.N6DB, PowerOffsetEnum.ZERO })
				{
					driver.Configure.Downlink.Pcc.Pdsch.Pa = x;
					PowerOffsetEnum value = driver.Configure.Downlink.Pcc.Pdsch.Pa;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:PDSCh:RINDex
				int value = driver.Configure.Downlink.Pcc.Pdsch.Rindex;
				driver.Configure.Downlink.Pcc.Pdsch.Rindex = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:CSIRs:MODE
				foreach (CsirsModeEnum x in new CsirsModeEnum[] { CsirsModeEnum.ACSirs, CsirsModeEnum.MANual })
				{
					driver.Configure.Downlink.Pcc.Csirs.Mode = x;
					CsirsModeEnum value = driver.Configure.Downlink.Pcc.Csirs.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:CSIRs:POFFset
				double value = driver.Configure.Downlink.Pcc.Csirs.Poffset;
				driver.Configure.Downlink.Pcc.Csirs.Poffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:DL[:PCC]:POWer:PORTs
				int value = driver.Configure.Downlink.Pcc.Power.Ports;
				driver.Configure.Downlink.Pcc.Power.Ports = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PMCC
				UlPwrMasterEnum value = driver.Configure.Uplink.Scc.GetPmcc(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.GetPmcc();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:SET
				SetTypeEnum value = driver.Configure.Uplink.Scc.Pusch.Tpc.Set.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.Set.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:SET
				foreach (SetTypeEnum x in new SetTypeEnum[] { SetTypeEnum.ALT0, SetTypeEnum.CLOop, SetTypeEnum.CONStant, SetTypeEnum.FULPower, SetTypeEnum.MAXPower, SetTypeEnum.MINPower, SetTypeEnum.RPControl, SetTypeEnum.SINGle, SetTypeEnum.UDContinuous, SetTypeEnum.UDSingle })
				{
					driver.Configure.Uplink.Scc.Pusch.Tpc.Set.Set(x);
					driver.Configure.Uplink.Scc.Pusch.Tpc.Set.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:PEXecute
				driver.Configure.Uplink.Scc.Pusch.Tpc.Pexecute.Set(SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.Pexecute.SetAndWait(SecondaryCompCarrierRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:RPControl
				RpControlPatternEnum value = driver.Configure.Uplink.Scc.Pusch.Tpc.RpControl.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.RpControl.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:RPControl
				foreach (RpControlPatternEnum x in new RpControlPatternEnum[] { RpControlPatternEnum.RDA, RpControlPatternEnum.RDB, RpControlPatternEnum.RDC, RpControlPatternEnum.RUA, RpControlPatternEnum.RUB, RpControlPatternEnum.RUC })
				{
					driver.Configure.Uplink.Scc.Pusch.Tpc.RpControl.Set(x);
					driver.Configure.Uplink.Scc.Pusch.Tpc.RpControl.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_Single.Single_Data value = driver.Configure.Uplink.Scc.Pusch.Tpc.Single.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.Single.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_Single.Single_Data value = new RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_Single.Single_Data();
				driver.Configure.Uplink.Scc.Pusch.Tpc.Single.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.Single.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:CLTPower
				double value = driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:CLTPower
				driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:CLTPower:OFFSet
				double value = driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Offset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Offset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:CLTPower:OFFSet
				driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Offset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.CltPower.Offset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_UdPattern.UdPattern_Data value = driver.Configure.Uplink.Scc.Pusch.Tpc.UdPattern.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.UdPattern.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_UdPattern.UdPattern_Data value = new RsCmwLteSig_Configure_Uplink_Scc_Pusch_Tpc_UdPattern.UdPattern_Data();
				driver.Configure.Uplink.Scc.Pusch.Tpc.UdPattern.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.UdPattern.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:TPOWer
				double value = driver.Configure.Uplink.Scc.Pusch.Tpc.Tpower.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.Tpc.Tpower.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:TPC:TPOWer
				driver.Configure.Uplink.Scc.Pusch.Tpc.Tpower.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.Tpc.Tpower.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:OLNPower
				double value = driver.Configure.Uplink.Scc.Pusch.OlnPower.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pusch.OlnPower.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUSCh:OLNPower
				driver.Configure.Uplink.Scc.Pusch.OlnPower.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pusch.OlnPower.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:EASettings
				bool value = driver.Configure.Uplink.Scc.ApPower.EaSettings.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.EaSettings.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:EASettings
				driver.Configure.Uplink.Scc.ApPower.EaSettings.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.ApPower.EaSettings.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:RSPower:ADVanced
				double value = driver.Configure.Uplink.Scc.ApPower.RsPower.Advanced.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.RsPower.Advanced.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:RSPower:ADVanced
				driver.Configure.Uplink.Scc.ApPower.RsPower.Advanced.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.ApPower.RsPower.Advanced.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PIRPower:ADVanced
				double value = driver.Configure.Uplink.Scc.ApPower.PirPower.Advanced.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.PirPower.Advanced.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PIRPower:ADVanced
				driver.Configure.Uplink.Scc.ApPower.PirPower.Advanced.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.ApPower.PirPower.Advanced.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PNPusch:ADVanced
				double value = driver.Configure.Uplink.Scc.ApPower.Pnpusch.Advanced.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.Pnpusch.Advanced.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PNPusch:ADVanced
				driver.Configure.Uplink.Scc.ApPower.Pnpusch.Advanced.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.ApPower.Pnpusch.Advanced.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PCALpha:ADVanced
				PathCompAlphaEnum value = driver.Configure.Uplink.Scc.ApPower.PcAlpha.Advanced.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.PcAlpha.Advanced.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PCALpha:ADVanced
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					driver.Configure.Uplink.Scc.ApPower.PcAlpha.Advanced.Set(x);
					driver.Configure.Uplink.Scc.ApPower.PcAlpha.Advanced.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:TPRRcsetup:ADVanced
				bool value = driver.Configure.Uplink.Scc.ApPower.TprrcSetup.Advanced.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.ApPower.TprrcSetup.Advanced.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:TPRRcsetup:ADVanced
				driver.Configure.Uplink.Scc.ApPower.TprrcSetup.Advanced.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.ApPower.TprrcSetup.Advanced.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUCCh:CLTPower
				int value = driver.Configure.Uplink.Scc.Pucch.CltPower.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.Pucch.CltPower.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PUCCh:CLTPower
				driver.Configure.Uplink.Scc.Pucch.CltPower.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.Pucch.CltPower.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PMAX
				double value = driver.Configure.Uplink.Scc.PowerMax.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Uplink.Scc.PowerMax.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SCC<Carrier>:PMAX
				driver.Configure.Uplink.Scc.PowerMax.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Uplink.Scc.PowerMax.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PMAX
				double value = driver.Configure.Uplink.Seta.PowerMax;
				driver.Configure.Uplink.Seta.PowerMax = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:OLNPower
				double value = driver.Configure.Uplink.Seta.Pusch.OlnPower;
				driver.Configure.Uplink.Seta.Pusch.OlnPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:SET
				foreach (SetTypeEnum x in new SetTypeEnum[] { SetTypeEnum.ALT0, SetTypeEnum.CLOop, SetTypeEnum.CONStant, SetTypeEnum.FULPower, SetTypeEnum.MAXPower, SetTypeEnum.MINPower, SetTypeEnum.RPControl, SetTypeEnum.SINGle, SetTypeEnum.UDContinuous, SetTypeEnum.UDSingle })
				{
					driver.Configure.Uplink.Seta.Pusch.Tpc.Set = x;
					SetTypeEnum value = driver.Configure.Uplink.Seta.Pusch.Tpc.Set;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:RPControl
				foreach (RpControlPatternEnum x in new RpControlPatternEnum[] { RpControlPatternEnum.RDA, RpControlPatternEnum.RDB, RpControlPatternEnum.RDC, RpControlPatternEnum.RUA, RpControlPatternEnum.RUB, RpControlPatternEnum.RUC })
				{
					driver.Configure.Uplink.Seta.Pusch.Tpc.RpControl = x;
					RpControlPatternEnum value = driver.Configure.Uplink.Seta.Pusch.Tpc.RpControl;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Seta_Pusch_Tpc.Single_Data value = driver.Configure.Uplink.Seta.Pusch.Tpc.Single;
				driver.Configure.Uplink.Seta.Pusch.Tpc.Single = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:TPOWer
				double value = driver.Configure.Uplink.Seta.Pusch.Tpc.Tpower;
				driver.Configure.Uplink.Seta.Pusch.Tpc.Tpower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Seta_Pusch_Tpc.UdPattern_Data value = driver.Configure.Uplink.Seta.Pusch.Tpc.UdPattern;
				driver.Configure.Uplink.Seta.Pusch.Tpc.UdPattern = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:PEXecute
				driver.Configure.Uplink.Seta.Pusch.Tpc.Pexecute.Set();
				driver.Configure.Uplink.Seta.Pusch.Tpc.Pexecute.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:CLTPower:OFFSet
				double value = driver.Configure.Uplink.Seta.Pusch.Tpc.CltPower.Offset;
				driver.Configure.Uplink.Seta.Pusch.Tpc.CltPower.Offset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUSCh:TPC:CLTPower
				double value = driver.Configure.Uplink.Seta.Pusch.Tpc.CltPower.Value;
				driver.Configure.Uplink.Seta.Pusch.Tpc.CltPower.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:EASettings
				bool value = driver.Configure.Uplink.Seta.ApPower.EaSettings;
				driver.Configure.Uplink.Seta.ApPower.EaSettings = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:RSPower:ADVanced
				double value = driver.Configure.Uplink.Seta.ApPower.RsPower.Advanced;
				driver.Configure.Uplink.Seta.ApPower.RsPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:PIRPower:ADVanced
				double value = driver.Configure.Uplink.Seta.ApPower.PirPower.Advanced;
				driver.Configure.Uplink.Seta.ApPower.PirPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:PNPusch:ADVanced
				double value = driver.Configure.Uplink.Seta.ApPower.Pnpusch.Advanced;
				driver.Configure.Uplink.Seta.ApPower.Pnpusch.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:PCALpha:ADVanced
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					driver.Configure.Uplink.Seta.ApPower.PcAlpha.Advanced = x;
					PathCompAlphaEnum value = driver.Configure.Uplink.Seta.ApPower.PcAlpha.Advanced;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:APPower:TPRRcsetup:ADVanced
				bool value = driver.Configure.Uplink.Seta.ApPower.TprrcSetup.Advanced;
				driver.Configure.Uplink.Seta.ApPower.TprrcSetup.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETA:PUCCh:CLTPower
				int value = driver.Configure.Uplink.Seta.Pucch.CltPower;
				driver.Configure.Uplink.Seta.Pucch.CltPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PMAX
				double value = driver.Configure.Uplink.Setb.PowerMax;
				driver.Configure.Uplink.Setb.PowerMax = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:OLNPower
				double value = driver.Configure.Uplink.Setb.Pusch.OlnPower;
				driver.Configure.Uplink.Setb.Pusch.OlnPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:SET
				foreach (SetTypeEnum x in new SetTypeEnum[] { SetTypeEnum.ALT0, SetTypeEnum.CLOop, SetTypeEnum.CONStant, SetTypeEnum.FULPower, SetTypeEnum.MAXPower, SetTypeEnum.MINPower, SetTypeEnum.RPControl, SetTypeEnum.SINGle, SetTypeEnum.UDContinuous, SetTypeEnum.UDSingle })
				{
					driver.Configure.Uplink.Setb.Pusch.Tpc.Set = x;
					SetTypeEnum value = driver.Configure.Uplink.Setb.Pusch.Tpc.Set;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:RPControl
				foreach (RpControlPatternEnum x in new RpControlPatternEnum[] { RpControlPatternEnum.RDA, RpControlPatternEnum.RDB, RpControlPatternEnum.RDC, RpControlPatternEnum.RUA, RpControlPatternEnum.RUB, RpControlPatternEnum.RUC })
				{
					driver.Configure.Uplink.Setb.Pusch.Tpc.RpControl = x;
					RpControlPatternEnum value = driver.Configure.Uplink.Setb.Pusch.Tpc.RpControl;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Setb_Pusch_Tpc.Single_Data value = driver.Configure.Uplink.Setb.Pusch.Tpc.Single;
				driver.Configure.Uplink.Setb.Pusch.Tpc.Single = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:TPOWer
				double value = driver.Configure.Uplink.Setb.Pusch.Tpc.Tpower;
				driver.Configure.Uplink.Setb.Pusch.Tpc.Tpower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Setb_Pusch_Tpc.UdPattern_Data value = driver.Configure.Uplink.Setb.Pusch.Tpc.UdPattern;
				driver.Configure.Uplink.Setb.Pusch.Tpc.UdPattern = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:PEXecute
				driver.Configure.Uplink.Setb.Pusch.Tpc.Pexecute.Set();
				driver.Configure.Uplink.Setb.Pusch.Tpc.Pexecute.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:CLTPower:OFFSet
				double value = driver.Configure.Uplink.Setb.Pusch.Tpc.CltPower.Offset;
				driver.Configure.Uplink.Setb.Pusch.Tpc.CltPower.Offset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUSCh:TPC:CLTPower
				double value = driver.Configure.Uplink.Setb.Pusch.Tpc.CltPower.Value;
				driver.Configure.Uplink.Setb.Pusch.Tpc.CltPower.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:EASettings
				bool value = driver.Configure.Uplink.Setb.ApPower.EaSettings;
				driver.Configure.Uplink.Setb.ApPower.EaSettings = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:RSPower:ADVanced
				double value = driver.Configure.Uplink.Setb.ApPower.RsPower.Advanced;
				driver.Configure.Uplink.Setb.ApPower.RsPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:PIRPower:ADVanced
				double value = driver.Configure.Uplink.Setb.ApPower.PirPower.Advanced;
				driver.Configure.Uplink.Setb.ApPower.PirPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:PNPusch:ADVanced
				double value = driver.Configure.Uplink.Setb.ApPower.Pnpusch.Advanced;
				driver.Configure.Uplink.Setb.ApPower.Pnpusch.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:PCALpha:ADVanced
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					driver.Configure.Uplink.Setb.ApPower.PcAlpha.Advanced = x;
					PathCompAlphaEnum value = driver.Configure.Uplink.Setb.ApPower.PcAlpha.Advanced;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:APPower:TPRRcsetup:ADVanced
				bool value = driver.Configure.Uplink.Setb.ApPower.TprrcSetup.Advanced;
				driver.Configure.Uplink.Setb.ApPower.TprrcSetup.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETB:PUCCh:CLTPower
				int value = driver.Configure.Uplink.Setb.Pucch.CltPower;
				driver.Configure.Uplink.Setb.Pucch.CltPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PMAX
				double value = driver.Configure.Uplink.Setc.PowerMax;
				driver.Configure.Uplink.Setc.PowerMax = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:OLNPower
				double value = driver.Configure.Uplink.Setc.Pusch.OlnPower;
				driver.Configure.Uplink.Setc.Pusch.OlnPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:SET
				foreach (SetTypeEnum x in new SetTypeEnum[] { SetTypeEnum.ALT0, SetTypeEnum.CLOop, SetTypeEnum.CONStant, SetTypeEnum.FULPower, SetTypeEnum.MAXPower, SetTypeEnum.MINPower, SetTypeEnum.RPControl, SetTypeEnum.SINGle, SetTypeEnum.UDContinuous, SetTypeEnum.UDSingle })
				{
					driver.Configure.Uplink.Setc.Pusch.Tpc.Set = x;
					SetTypeEnum value = driver.Configure.Uplink.Setc.Pusch.Tpc.Set;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:RPControl
				foreach (RpControlPatternEnum x in new RpControlPatternEnum[] { RpControlPatternEnum.RDA, RpControlPatternEnum.RDB, RpControlPatternEnum.RDC, RpControlPatternEnum.RUA, RpControlPatternEnum.RUB, RpControlPatternEnum.RUC })
				{
					driver.Configure.Uplink.Setc.Pusch.Tpc.RpControl = x;
					RpControlPatternEnum value = driver.Configure.Uplink.Setc.Pusch.Tpc.RpControl;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Setc_Pusch_Tpc.Single_Data value = driver.Configure.Uplink.Setc.Pusch.Tpc.Single;
				driver.Configure.Uplink.Setc.Pusch.Tpc.Single = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:TPOWer
				double value = driver.Configure.Uplink.Setc.Pusch.Tpc.Tpower;
				driver.Configure.Uplink.Setc.Pusch.Tpc.Tpower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Setc_Pusch_Tpc.UdPattern_Data value = driver.Configure.Uplink.Setc.Pusch.Tpc.UdPattern;
				driver.Configure.Uplink.Setc.Pusch.Tpc.UdPattern = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:PEXecute
				driver.Configure.Uplink.Setc.Pusch.Tpc.Pexecute.Set();
				driver.Configure.Uplink.Setc.Pusch.Tpc.Pexecute.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:CLTPower:OFFSet
				double value = driver.Configure.Uplink.Setc.Pusch.Tpc.CltPower.Offset;
				driver.Configure.Uplink.Setc.Pusch.Tpc.CltPower.Offset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUSCh:TPC:CLTPower
				double value = driver.Configure.Uplink.Setc.Pusch.Tpc.CltPower.Value;
				driver.Configure.Uplink.Setc.Pusch.Tpc.CltPower.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:EASettings
				bool value = driver.Configure.Uplink.Setc.ApPower.EaSettings;
				driver.Configure.Uplink.Setc.ApPower.EaSettings = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:RSPower:ADVanced
				double value = driver.Configure.Uplink.Setc.ApPower.RsPower.Advanced;
				driver.Configure.Uplink.Setc.ApPower.RsPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:PIRPower:ADVanced
				double value = driver.Configure.Uplink.Setc.ApPower.PirPower.Advanced;
				driver.Configure.Uplink.Setc.ApPower.PirPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:PNPusch:ADVanced
				double value = driver.Configure.Uplink.Setc.ApPower.Pnpusch.Advanced;
				driver.Configure.Uplink.Setc.ApPower.Pnpusch.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:PCALpha:ADVanced
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					driver.Configure.Uplink.Setc.ApPower.PcAlpha.Advanced = x;
					PathCompAlphaEnum value = driver.Configure.Uplink.Setc.ApPower.PcAlpha.Advanced;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:APPower:TPRRcsetup:ADVanced
				bool value = driver.Configure.Uplink.Setc.ApPower.TprrcSetup.Advanced;
				driver.Configure.Uplink.Setc.ApPower.TprrcSetup.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL:SETC:PUCCh:CLTPower
				int value = driver.Configure.Uplink.Setc.Pucch.CltPower;
				driver.Configure.Uplink.Setc.Pucch.CltPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PMAX
				double value = driver.Configure.Uplink.Pcc.PowerMax;
				driver.Configure.Uplink.Pcc.PowerMax = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:JUPower
				bool value = driver.Configure.Uplink.Pcc.JuPower;
				driver.Configure.Uplink.Pcc.JuPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:OLNPower
				double value = driver.Configure.Uplink.Pcc.Pusch.OlnPower;
				driver.Configure.Uplink.Pcc.Pusch.OlnPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:SET
				foreach (SetTypeEnum x in new SetTypeEnum[] { SetTypeEnum.ALT0, SetTypeEnum.CLOop, SetTypeEnum.CONStant, SetTypeEnum.FULPower, SetTypeEnum.MAXPower, SetTypeEnum.MINPower, SetTypeEnum.RPControl, SetTypeEnum.SINGle, SetTypeEnum.UDContinuous, SetTypeEnum.UDSingle })
				{
					driver.Configure.Uplink.Pcc.Pusch.Tpc.Set = x;
					SetTypeEnum value = driver.Configure.Uplink.Pcc.Pusch.Tpc.Set;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:RPControl
				foreach (RpControlPatternEnum x in new RpControlPatternEnum[] { RpControlPatternEnum.RDA, RpControlPatternEnum.RDB, RpControlPatternEnum.RDC, RpControlPatternEnum.RUA, RpControlPatternEnum.RUB, RpControlPatternEnum.RUC })
				{
					driver.Configure.Uplink.Pcc.Pusch.Tpc.RpControl = x;
					RpControlPatternEnum value = driver.Configure.Uplink.Pcc.Pusch.Tpc.RpControl;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:SINGle
				RsCmwLteSig_Configure_Uplink_Pcc_Pusch_Tpc.Single_Data value = driver.Configure.Uplink.Pcc.Pusch.Tpc.Single;
				driver.Configure.Uplink.Pcc.Pusch.Tpc.Single = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:CLTPower
				double value = driver.Configure.Uplink.Pcc.Pusch.Tpc.CltPower;
				driver.Configure.Uplink.Pcc.Pusch.Tpc.CltPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:TPOWer
				double value = driver.Configure.Uplink.Pcc.Pusch.Tpc.Tpower;
				driver.Configure.Uplink.Pcc.Pusch.Tpc.Tpower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:UDPattern
				RsCmwLteSig_Configure_Uplink_Pcc_Pusch_Tpc.UdPattern_Data value = driver.Configure.Uplink.Pcc.Pusch.Tpc.UdPattern;
				driver.Configure.Uplink.Pcc.Pusch.Tpc.UdPattern = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUSCh:TPC:PEXecute
				driver.Configure.Uplink.Pcc.Pusch.Tpc.Pexecute.Set();
				driver.Configure.Uplink.Pcc.Pusch.Tpc.Pexecute.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:EASettings
				bool value = driver.Configure.Uplink.Pcc.ApPower.EaSettings;
				driver.Configure.Uplink.Pcc.ApPower.EaSettings = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:RSPower:ADVanced
				double value = driver.Configure.Uplink.Pcc.ApPower.RsPower.Advanced;
				driver.Configure.Uplink.Pcc.ApPower.RsPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PIRPower:ADVanced
				double value = driver.Configure.Uplink.Pcc.ApPower.PirPower.Advanced;
				driver.Configure.Uplink.Pcc.ApPower.PirPower.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PNPusch:ADVanced
				double value = driver.Configure.Uplink.Pcc.ApPower.Pnpusch.Advanced;
				driver.Configure.Uplink.Pcc.ApPower.Pnpusch.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PCALpha:ADVanced
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					driver.Configure.Uplink.Pcc.ApPower.PcAlpha.Advanced = x;
					PathCompAlphaEnum value = driver.Configure.Uplink.Pcc.ApPower.PcAlpha.Advanced;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:APPower:TPRRcsetup:ADVanced
				bool value = driver.Configure.Uplink.Pcc.ApPower.TprrcSetup.Advanced;
				driver.Configure.Uplink.Pcc.ApPower.TprrcSetup.Advanced = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UL[:PCC]:PUCCh:CLTPower
				int value = driver.Configure.Uplink.Pcc.Pucch.CltPower;
				driver.Configure.Uplink.Pcc.Pucch.CltPower = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:CPRefix
				foreach (CyclicPrefixEnum x in new CyclicPrefixEnum[] { CyclicPrefixEnum.EXTended, CyclicPrefixEnum.NORMal })
				{
					driver.Configure.Cell.Cprefix = x;
					CyclicPrefixEnum value = driver.Configure.Cell.Cprefix;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:MCC
				int value = driver.Configure.Cell.Mcc;
				driver.Configure.Cell.Mcc = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TAC
				int value = driver.Configure.Cell.Tac;
				driver.Configure.Cell.Tac = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:BANDwidth[:PCC]:DL
				foreach (BandwidthEnum x in new BandwidthEnum[] { BandwidthEnum.B014, BandwidthEnum.B030, BandwidthEnum.B050, BandwidthEnum.B100, BandwidthEnum.B150, BandwidthEnum.B200 })
				{
					driver.Configure.Cell.Bandwidth.Pcc.Downlink = x;
					BandwidthEnum value = driver.Configure.Cell.Bandwidth.Pcc.Downlink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:BANDwidth:SCC<Carrier>:DL
				BandwidthEnum value = driver.Configure.Cell.Bandwidth.Scc.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Bandwidth.Scc.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:BANDwidth:SCC<Carrier>:DL
				foreach (BandwidthEnum x in new BandwidthEnum[] { BandwidthEnum.B014, BandwidthEnum.B030, BandwidthEnum.B050, BandwidthEnum.B100, BandwidthEnum.B150, BandwidthEnum.B200 })
				{
					driver.Configure.Cell.Bandwidth.Scc.Downlink.Set(x);
					driver.Configure.Cell.Bandwidth.Scc.Downlink.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:PCID
				int value = driver.Configure.Cell.Pcc.Pcid;
				driver.Configure.Cell.Pcc.Pcid = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:ULDL
				int value = driver.Configure.Cell.Pcc.UlDl;
				driver.Configure.Cell.Pcc.UlDl = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SSUBframe
				int value = driver.Configure.Cell.Pcc.Ssubframe;
				driver.Configure.Cell.Pcc.Ssubframe = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:ULSupport:QAM<ModOrder>:ENABle
				bool value = driver.Configure.Cell.Pcc.UlSupport.Qam.Enable.Get(QAMmodulationOrderRepCap.Default);
				value = driver.Configure.Cell.Pcc.UlSupport.Qam.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:ULSupport:QAM<ModOrder>:ENABle
				driver.Configure.Cell.Pcc.UlSupport.Qam.Enable.Set(false, QAMmodulationOrderRepCap.Default);
				driver.Configure.Cell.Pcc.UlSupport.Qam.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:HBANdwidth
				int value = driver.Configure.Cell.Pcc.Srs.Hbandwidth;
				driver.Configure.Cell.Pcc.Srs.Hbandwidth = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:DBANdwidth
				int value = driver.Configure.Cell.Pcc.Srs.Dbandwidth;
				driver.Configure.Cell.Pcc.Srs.Dbandwidth = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:BWConfig
				int value = driver.Configure.Cell.Pcc.Srs.BwConfig;
				driver.Configure.Cell.Pcc.Srs.BwConfig = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SRS:ENABle
				bool value = driver.Configure.Cell.Pcc.Srs.Enable;
				driver.Configure.Cell.Pcc.Srs.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:MCENable
				bool value = driver.Configure.Cell.Pcc.Srs.McEnable;
				driver.Configure.Cell.Pcc.Srs.McEnable = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:SFConfig
				int value = driver.Configure.Cell.Pcc.Srs.SfConfig;
				driver.Configure.Cell.Pcc.Srs.SfConfig = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SRS:DCONfig
				bool value = driver.Configure.Cell.Pcc.Srs.Dconfig;
				driver.Configure.Cell.Pcc.Srs.Dconfig = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SRS:SCINdex:FDD
				int value = driver.Configure.Cell.Pcc.Srs.ScIndex.Fdd;
				driver.Configure.Cell.Pcc.Srs.ScIndex.Fdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SRS:SCINdex:TDD
				int value = driver.Configure.Cell.Pcc.Srs.ScIndex.Tdd;
				driver.Configure.Cell.Pcc.Srs.ScIndex.Tdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:POFFset
				RsCmwLteSig_Configure_Cell_Pcc_Srs_Poffset.Get_Data value = driver.Configure.Cell.Pcc.Srs.Poffset.Get();				
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:CELL[:PCC]:SRS:POFFset
				driver.Configure.Cell.Pcc.Srs.Poffset.Set(1);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:CID:EUTRan
				string value = driver.Configure.Cell.Pcc.Cid.Eutran;
				driver.Configure.Cell.Pcc.Cid.Eutran = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SYNC:ZONE
				foreach (SyncZoneEnum x in new SyncZoneEnum[] { SyncZoneEnum.NONE, SyncZoneEnum.Z1 })
				{
					driver.Configure.Cell.Pcc.Sync.Zone = x;
					SyncZoneEnum value = driver.Configure.Cell.Pcc.Sync.Zone;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL[:PCC]:SYNC:OFFSet
				double value = driver.Configure.Cell.Pcc.Sync.Offset;
				driver.Configure.Cell.Pcc.Sync.Offset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:PCID
				int value = driver.Configure.Cell.Scc.Pcid.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Pcid.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:PCID
				driver.Configure.Cell.Scc.Pcid.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Pcid.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:ULDL
				int value = driver.Configure.Cell.Scc.UlDl.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.UlDl.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:ULDL
				driver.Configure.Cell.Scc.UlDl.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.UlDl.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SSUBframe
				int value = driver.Configure.Cell.Scc.Ssubframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Ssubframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SSUBframe
				driver.Configure.Cell.Scc.Ssubframe.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Ssubframe.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CSAT:ENABle
				bool value = driver.Configure.Cell.Scc.Csat.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Csat.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CSAT:ENABle
				driver.Configure.Cell.Scc.Csat.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Csat.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CSAT:DMTCperiod
				LdsPeriodEnum value = driver.Configure.Cell.Scc.Csat.DmtcPeriod.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Csat.DmtcPeriod.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CSAT:DMTCperiod
				foreach (LdsPeriodEnum x in new LdsPeriodEnum[] { LdsPeriodEnum.M160, LdsPeriodEnum.M40, LdsPeriodEnum.M80 })
				{
					driver.Configure.Cell.Scc.Csat.DmtcPeriod.Set(x);
					driver.Configure.Cell.Scc.Csat.DmtcPeriod.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:ONSDuration
				int value = driver.Configure.Cell.Scc.ScMuting.OnsDuration.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.ScMuting.OnsDuration.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:ONSDuration
				driver.Configure.Cell.Scc.ScMuting.OnsDuration.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.ScMuting.OnsDuration.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:OFFSduration
				int value = driver.Configure.Cell.Scc.ScMuting.OffsDuration.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.ScMuting.OffsDuration.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:OFFSduration
				driver.Configure.Cell.Scc.ScMuting.OffsDuration.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.ScMuting.OffsDuration.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:PMAC
				bool value = driver.Configure.Cell.Scc.ScMuting.Pmac.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.ScMuting.Pmac.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SCMuting:PMAC
				driver.Configure.Cell.Scc.ScMuting.Pmac.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.ScMuting.Pmac.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:ULSupport:QAM<ModOrder>:ENABle
				bool value = driver.Configure.Cell.Scc.UlSupport.Qam.Enable.Get(SecondaryCompCarrierRepCap.Default, QAMmodulationOrderRepCap.Default);
				value = driver.Configure.Cell.Scc.UlSupport.Qam.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:ULSupport:QAM<ModOrder>:ENABle
				driver.Configure.Cell.Scc.UlSupport.Qam.Enable.Set(false, SecondaryCompCarrierRepCap.Default, QAMmodulationOrderRepCap.Default);
				driver.Configure.Cell.Scc.UlSupport.Qam.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:DCONfig
				bool value = driver.Configure.Cell.Scc.Srs.Dconfig.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.Dconfig.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:DCONfig
				driver.Configure.Cell.Scc.Srs.Dconfig.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.Dconfig.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:ENABle
				bool value = driver.Configure.Cell.Scc.Srs.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:ENABle
				driver.Configure.Cell.Scc.Srs.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:BWConfig
				int value = driver.Configure.Cell.Scc.Srs.BwConfig.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.BwConfig.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:BWConfig
				driver.Configure.Cell.Scc.Srs.BwConfig.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.BwConfig.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:HBANdwidth
				int value = driver.Configure.Cell.Scc.Srs.Hbandwidth.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.Hbandwidth.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:HBANdwidth
				driver.Configure.Cell.Scc.Srs.Hbandwidth.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.Hbandwidth.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:MCENable
				bool value = driver.Configure.Cell.Scc.Srs.McEnable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.McEnable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:MCENable
				driver.Configure.Cell.Scc.Srs.McEnable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.McEnable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SFConfig
				int value = driver.Configure.Cell.Scc.Srs.SfConfig.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.SfConfig.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SFConfig
				driver.Configure.Cell.Scc.Srs.SfConfig.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.SfConfig.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SCINdex:FDD
				int value = driver.Configure.Cell.Scc.Srs.ScIndex.Fdd.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.ScIndex.Fdd.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SCINdex:FDD
				driver.Configure.Cell.Scc.Srs.ScIndex.Fdd.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.ScIndex.Fdd.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SCINdex:TDD
				int value = driver.Configure.Cell.Scc.Srs.ScIndex.Tdd.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.ScIndex.Tdd.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:SCINdex:TDD
				driver.Configure.Cell.Scc.Srs.ScIndex.Tdd.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.ScIndex.Tdd.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:POFFset
				RsCmwLteSig_Configure_Cell_Scc_Srs_Poffset.Get_Data value = driver.Configure.Cell.Scc.Srs.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Srs.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:SRS:POFFset
				driver.Configure.Cell.Scc.Srs.Poffset.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Srs.Poffset.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:DBANdwidth
				int value = driver.Configure.Cell.Scc.Dbandwidth.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Dbandwidth.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<carrier>:DBANdwidth
				driver.Configure.Cell.Scc.Dbandwidth.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Dbandwidth.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CID:EUTRan
				string value = driver.Configure.Cell.Scc.Cid.Eutran.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Cid.Eutran.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:CID:EUTRan
				driver.Configure.Cell.Scc.Cid.Eutran.Set("r1", SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Cid.Eutran.Set("r1");
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SYNC:OFFSet
				double value = driver.Configure.Cell.Scc.Sync.Offset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Cell.Scc.Sync.Offset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SCC<Carrier>:SYNC:OFFSet
				driver.Configure.Cell.Scc.Sync.Offset.Set(1.0, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Cell.Scc.Sync.Offset.Set(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TDD:SPECific
				bool value = driver.Configure.Cell.Tdd.Specific;
				driver.Configure.Cell.Tdd.Specific = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:NRPReambles
				foreach (EnablePreamblesEnum x in new EnablePreamblesEnum[] { EnablePreamblesEnum.NIPReambles, EnablePreamblesEnum.OFF, EnablePreamblesEnum.ON })
				{
					driver.Configure.Cell.Prach.NrPreambles = x;
					EnablePreamblesEnum value = driver.Configure.Cell.Prach.NrPreambles;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:NIPRach
				int value = driver.Configure.Cell.Prach.Niprach;
				driver.Configure.Cell.Prach.Niprach = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:PRSTep
				foreach (PrStepEnum x in new PrStepEnum[] { PrStepEnum.P2DB, PrStepEnum.P4DB, PrStepEnum.P6DB, PrStepEnum.ZERO })
				{
					driver.Configure.Cell.Prach.Prstep = x;
					PrStepEnum value = driver.Configure.Cell.Prach.Prstep;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:PFOFfset
				int value = driver.Configure.Cell.Prach.PfOffset;
				driver.Configure.Cell.Prach.PfOffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:LRSindex
				int value = driver.Configure.Cell.Prach.LrsIndex;
				driver.Configure.Cell.Prach.LrsIndex = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:ZCZConfig
				int value = driver.Configure.Cell.Prach.ZczConfig;
				driver.Configure.Cell.Prach.ZczConfig = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:PCINdex:FDD
				int value = driver.Configure.Cell.Prach.PcIndex.Fdd;
				driver.Configure.Cell.Prach.PcIndex.Fdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:PRACh:PCINdex:TDD
				int value = driver.Configure.Cell.Prach.PcIndex.Tdd;
				driver.Configure.Cell.Prach.PcIndex.Tdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RAR:CMCS:ENABle
				bool value = driver.Configure.Cell.Rar.Cmcs.Enable;
				driver.Configure.Cell.Rar.Cmcs.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RAR:CMCS
				int value = driver.Configure.Cell.Rar.Cmcs.Value;
				driver.Configure.Cell.Rar.Cmcs.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:MNC:DIGits
				foreach (NoOfDigitsEnum x in new NoOfDigitsEnum[] { NoOfDigitsEnum.THRee, NoOfDigitsEnum.TWO })
				{
					driver.Configure.Cell.Mnc.Digits = x;
					NoOfDigitsEnum value = driver.Configure.Cell.Mnc.Digits;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:MNC
				int value = driver.Configure.Cell.Mnc.Value;
				driver.Configure.Cell.Mnc.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:AUTHenticat
				bool value = driver.Configure.Cell.Security.Authenticate;
				driver.Configure.Cell.Security.Authenticate = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:NAS
				bool value = driver.Configure.Cell.Security.Nas;
				driver.Configure.Cell.Security.Nas = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:AS
				bool value = driver.Configure.Cell.Security.As;
				driver.Configure.Cell.Security.As = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:IALGorithm
				foreach (SecurityAlgorithmEnum x in new SecurityAlgorithmEnum[] { SecurityAlgorithmEnum.NULL, SecurityAlgorithmEnum.S3G })
				{
					driver.Configure.Cell.Security.Ialgorithm = x;
					SecurityAlgorithmEnum value = driver.Configure.Cell.Security.Ialgorithm;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:MILenage
				bool value = driver.Configure.Cell.Security.Milenage;
				driver.Configure.Cell.Security.Milenage = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:SKEY
				string value = driver.Configure.Cell.Security.Skey;
				driver.Configure.Cell.Security.Skey = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:OPC
				string value = driver.Configure.Cell.Security.Opc;
				driver.Configure.Cell.Security.Opc = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:SECurity:RVALue
				foreach (RandomValueModeEnum x in new RandomValueModeEnum[] { RandomValueModeEnum.EVEN, RandomValueModeEnum.ODD })
				{
					driver.Configure.Cell.Security.Rvalue = x;
					RandomValueModeEnum value = driver.Configure.Cell.Security.Rvalue;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:UEIDentity:IMSI
				string value = driver.Configure.Cell.UeIdentity.Imsi;
				driver.Configure.Cell.UeIdentity.Imsi = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TOUT:OSYNch
				int value = driver.Configure.Cell.Timeout.Osynch;
				driver.Configure.Cell.Timeout.Osynch = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TOUT:T<nr>
				int value = driver.Configure.Cell.Timeout.T.Get(TextRepCap.T3324);
				value = driver.Configure.Cell.Timeout.T.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TOUT:T<nr>
				driver.Configure.Cell.Timeout.T.Set(1, TextRepCap.T3324);
				driver.Configure.Cell.Timeout.T.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TOUT:TEXT<nr>
				int value = driver.Configure.Cell.Timeout.Text.Get(TextRepCap.Default);
				value = driver.Configure.Cell.Timeout.Text.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TOUT:TEXT<nr>
				driver.Configure.Cell.Timeout.Text.Set(1, TextRepCap.Default);
				driver.Configure.Cell.Timeout.Text.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RESelection:TSLow
				double value = driver.Configure.Cell.ReSelection.Tslow;
				driver.Configure.Cell.ReSelection.Tslow = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RESelection:SEARch:INTRasearch
				double value = driver.Configure.Cell.ReSelection.Search.Intrasearch;
				driver.Configure.Cell.ReSelection.Search.Intrasearch = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RESelection:SEARch:NINTrasearch
				double value = driver.Configure.Cell.ReSelection.Search.Nintrasearch;
				driver.Configure.Cell.ReSelection.Search.Nintrasearch = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RESelection:QUALity:RXLevmin
				double value = driver.Configure.Cell.ReSelection.Quality.RxLevelMin;
				driver.Configure.Cell.ReSelection.Quality.RxLevelMin = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Cell.Time.Tsource = x;
					SourceTimeEnum value = driver.Configure.Cell.Time.Tsource;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:DATE
				RsCmwLteSig_Configure_Cell_Time.Date_Data value = driver.Configure.Cell.Time.Date;
				driver.Configure.Cell.Time.Date = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:TIME
				RsCmwLteSig_Configure_Cell_Time.Time_Data value = driver.Configure.Cell.Time.Time;
				driver.Configure.Cell.Time.Time = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:DSTime
				foreach (DsTimeEnum x in new DsTimeEnum[] { DsTimeEnum.OFF, DsTimeEnum.ON, DsTimeEnum.P1H, DsTimeEnum.P2H })
				{
					driver.Configure.Cell.Time.DaylightSavingTime = x;
					DsTimeEnum value = driver.Configure.Cell.Time.DaylightSavingTime;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:LTZoffset
				double value = driver.Configure.Cell.Time.LtzOffset;
				driver.Configure.Cell.Time.LtzOffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:SATTach
				bool value = driver.Configure.Cell.Time.Sattach;
				driver.Configure.Cell.Time.Sattach = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:SNName
				bool value = driver.Configure.Cell.Time.Snname;
				driver.Configure.Cell.Time.Snname = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:TIME:SNOW
				driver.Configure.Cell.Time.Snow.Set();
				driver.Configure.Cell.Time.Snow.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:NAS:EPSNetwork
				bool value = driver.Configure.Cell.Nas.EpsNetwork;
				driver.Configure.Cell.Nas.EpsNetwork = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:NAS:IMSVops
				foreach (SupportedEnum x in new SupportedEnum[] { SupportedEnum.NSUPported, SupportedEnum.SUPPorted })
				{
					driver.Configure.Cell.Nas.Imsvops = x;
					SupportedEnum value = driver.Configure.Cell.Nas.Imsvops;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:NAS:EMCBs
				foreach (SupportedEnum x in new SupportedEnum[] { SupportedEnum.NSUPported, SupportedEnum.SUPPorted })
				{
					driver.Configure.Cell.Nas.Emcbs = x;
					SupportedEnum value = driver.Configure.Cell.Nas.Emcbs;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:NAS:EPCLcs
				foreach (SupportedEnum x in new SupportedEnum[] { SupportedEnum.NSUPported, SupportedEnum.SUPPorted })
				{
					driver.Configure.Cell.Nas.Epclcs = x;
					SupportedEnum value = driver.Configure.Cell.Nas.Epclcs;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:NAS:CSLCs
				foreach (SupportedExtEnum x in new SupportedExtEnum[] { SupportedExtEnum.NINFormation, SupportedExtEnum.NSUPported, SupportedExtEnum.SUPPorted })
				{
					driver.Configure.Cell.Nas.Cslcs = x;
					SupportedExtEnum value = driver.Configure.Cell.Nas.Cslcs;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:ACAuse:ATTach
				foreach (AcceptAttachCauseEnum x in new AcceptAttachCauseEnum[] { AcceptAttachCauseEnum.C18, AcceptAttachCauseEnum.OFF, AcceptAttachCauseEnum.ON })
				{
					driver.Configure.Cell.Acause.Attach = x;
					AcceptAttachCauseEnum value = driver.Configure.Cell.Acause.Attach;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RCAuse:ATTach
				foreach (RejectAttachCauseEnum x in new RejectAttachCauseEnum[] { RejectAttachCauseEnum.C10, RejectAttachCauseEnum.C100, RejectAttachCauseEnum.C101, RejectAttachCauseEnum.C111, RejectAttachCauseEnum.C13, RejectAttachCauseEnum.C14, RejectAttachCauseEnum.C15, RejectAttachCauseEnum.C16, RejectAttachCauseEnum.C17, RejectAttachCauseEnum.C18, RejectAttachCauseEnum.C19, RejectAttachCauseEnum.C2, RejectAttachCauseEnum.C20, RejectAttachCauseEnum.C21, RejectAttachCauseEnum.C23, RejectAttachCauseEnum.C24, RejectAttachCauseEnum.C25, RejectAttachCauseEnum.C26, RejectAttachCauseEnum.C35, RejectAttachCauseEnum.C39, RejectAttachCauseEnum.C40, RejectAttachCauseEnum.C42, RejectAttachCauseEnum.C5, RejectAttachCauseEnum.C6, RejectAttachCauseEnum.C8, RejectAttachCauseEnum.C9, RejectAttachCauseEnum.C95, RejectAttachCauseEnum.C96, RejectAttachCauseEnum.C97, RejectAttachCauseEnum.C98, RejectAttachCauseEnum.C99, RejectAttachCauseEnum.CONG22, RejectAttachCauseEnum.EPS7, RejectAttachCauseEnum.IUE3, RejectAttachCauseEnum.OFF, RejectAttachCauseEnum.ON, RejectAttachCauseEnum.PLMN11, RejectAttachCauseEnum.TANA12 })
				{
					driver.Configure.Cell.Rcause.Attach = x;
					RejectAttachCauseEnum value = driver.Configure.Cell.Rcause.Attach;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CELL:RCAuse:TAU
				foreach (RejectAttachCauseEnum x in new RejectAttachCauseEnum[] { RejectAttachCauseEnum.C10, RejectAttachCauseEnum.C100, RejectAttachCauseEnum.C101, RejectAttachCauseEnum.C111, RejectAttachCauseEnum.C13, RejectAttachCauseEnum.C14, RejectAttachCauseEnum.C15, RejectAttachCauseEnum.C16, RejectAttachCauseEnum.C17, RejectAttachCauseEnum.C18, RejectAttachCauseEnum.C19, RejectAttachCauseEnum.C2, RejectAttachCauseEnum.C20, RejectAttachCauseEnum.C21, RejectAttachCauseEnum.C23, RejectAttachCauseEnum.C24, RejectAttachCauseEnum.C25, RejectAttachCauseEnum.C26, RejectAttachCauseEnum.C35, RejectAttachCauseEnum.C39, RejectAttachCauseEnum.C40, RejectAttachCauseEnum.C42, RejectAttachCauseEnum.C5, RejectAttachCauseEnum.C6, RejectAttachCauseEnum.C8, RejectAttachCauseEnum.C9, RejectAttachCauseEnum.C95, RejectAttachCauseEnum.C96, RejectAttachCauseEnum.C97, RejectAttachCauseEnum.C98, RejectAttachCauseEnum.C99, RejectAttachCauseEnum.CONG22, RejectAttachCauseEnum.EPS7, RejectAttachCauseEnum.IUE3, RejectAttachCauseEnum.OFF, RejectAttachCauseEnum.ON, RejectAttachCauseEnum.PLMN11, RejectAttachCauseEnum.TANA12 })
				{
					driver.Configure.Cell.Rcause.Tau = x;
					RejectAttachCauseEnum value = driver.Configure.Cell.Rcause.Tau;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:DEDBearer
				string value = driver.Configure.Connection.DedBearer;
				driver.Configure.Connection.DedBearer = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:RLCMode
				foreach (RlcModeEnum x in new RlcModeEnum[] { RlcModeEnum.AM, RlcModeEnum.UM })
				{
					driver.Configure.Connection.RlcMode = x;
					RlcModeEnum value = driver.Configure.Connection.RlcMode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:IPVersion
				foreach (IpVersionEnum x in new IpVersionEnum[] { IpVersionEnum.IPV4, IpVersionEnum.IPV46, IpVersionEnum.IPV6 })
				{
					driver.Configure.Connection.IpVersion = x;
					IpVersionEnum value = driver.Configure.Connection.IpVersion;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:APN
				string value = driver.Configure.Connection.Apn;
				driver.Configure.Connection.Apn = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:QCI
				int value = driver.Configure.Connection.Qci;
				driver.Configure.Connection.Qci = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UDSCheduling
				bool value = driver.Configure.Connection.UdScheduling;
				driver.Configure.Connection.UdScheduling = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:IUGNrb
				int value = driver.Configure.Connection.Iugnrb;
				driver.Configure.Connection.Iugnrb = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:IUGMcsidx
				int value = driver.Configure.Connection.Iugmcsidx;
				driver.Configure.Connection.Iugmcsidx = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UETSelection
				foreach (TransmitAntenaSelectionEnum x in new TransmitAntenaSelectionEnum[] { TransmitAntenaSelectionEnum.OFF, TransmitAntenaSelectionEnum.OLOop })
				{
					driver.Configure.Connection.UetSelection = x;
					TransmitAntenaSelectionEnum value = driver.Configure.Connection.UetSelection;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SRPRindex
				int value = driver.Configure.Connection.SrprIndex;
				driver.Configure.Connection.SrprIndex = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SRCindex
				int value = driver.Configure.Connection.SrcIndex;
				driver.Configure.Connection.SrcIndex = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:TAControl
				bool value = driver.Configure.Connection.TaControl;
				driver.Configure.Connection.TaControl = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:IDCHsindic
				bool value = driver.Configure.Connection.Idchsindic;
				driver.Configure.Connection.Idchsindic = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SIBReconfig
				foreach (UeChangesTypeEnum x in new UeChangesTypeEnum[] { UeChangesTypeEnum.RRCReconfig, UeChangesTypeEnum.SIBPaging })
				{
					driver.Configure.Connection.SibreConfig = x;
					UeChangesTypeEnum value = driver.Configure.Connection.SibreConfig;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:GHOPping
				bool value = driver.Configure.Connection.Ghopping;
				driver.Configure.Connection.Ghopping = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:PSMallowed
				bool value = driver.Configure.Connection.PsmAllowed;
				driver.Configure.Connection.PsmAllowed = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:IEMergency
				bool value = driver.Configure.Connection.Iemergency;
				driver.Configure.Connection.Iemergency = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:EOISupport
				bool value = driver.Configure.Connection.EoiSupport;
				driver.Configure.Connection.EoiSupport = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SDNSpco
				bool value = driver.Configure.Connection.Sdnspco;
				driver.Configure.Connection.Sdnspco = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:DPCYcle
				foreach (DpCycleEnum x in new DpCycleEnum[] { DpCycleEnum.P032, DpCycleEnum.P064, DpCycleEnum.P128, DpCycleEnum.P256 })
				{
					driver.Configure.Connection.DpCycle = x;
					DpCycleEnum value = driver.Configure.Connection.DpCycle;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:PCNB
				foreach (NbValueEnum x in new NbValueEnum[] { NbValueEnum.NB2T, NbValueEnum.NB4T, NbValueEnum.NBT, NbValueEnum.NBT128, NbValueEnum.NBT16, NbValueEnum.NBT2, NbValueEnum.NBT256, NbValueEnum.NBT32, NbValueEnum.NBT4, NbValueEnum.NBT64, NbValueEnum.NBT8 })
				{
					driver.Configure.Connection.Pcnb = x;
					NbValueEnum value = driver.Configure.Connection.Pcnb;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CTYPe
				foreach (ConnectionTypeEnum x in new ConnectionTypeEnum[] { ConnectionTypeEnum.DAPPlication, ConnectionTypeEnum.TESTmode })
				{
					driver.Configure.Connection.Ctype = x;
					ConnectionTypeEnum value = driver.Configure.Connection.Ctype;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:KRRC
				bool value = driver.Configure.Connection.Krrc;
				driver.Configure.Connection.Krrc = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:RITimer
				int value = driver.Configure.Connection.RiTimer;
				driver.Configure.Connection.RiTimer = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:FCOefficient
				foreach (FilterCoefficientEnum x in new FilterCoefficientEnum[] { FilterCoefficientEnum.FC4, FilterCoefficientEnum.FC8 })
				{
					driver.Configure.Connection.Fcoefficient = x;
					FilterCoefficientEnum value = driver.Configure.Connection.Fcoefficient;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:TMODe
				bool value = driver.Configure.Connection.Tmode;
				driver.Configure.Connection.Tmode = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:DLEinsertion
				int value = driver.Configure.Connection.DleInsertion;
				driver.Configure.Connection.DleInsertion = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:DLPadding
				bool value = driver.Configure.Connection.DlPadding;
				driver.Configure.Connection.DlPadding = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ASEMission
				foreach (AddSpectrumEmissionEnum x in new AddSpectrumEmissionEnum[] { AddSpectrumEmissionEnum.NS01, AddSpectrumEmissionEnum.NS02, AddSpectrumEmissionEnum.NS03, AddSpectrumEmissionEnum.NS04, AddSpectrumEmissionEnum.NS05, AddSpectrumEmissionEnum.NS06, AddSpectrumEmissionEnum.NS07, AddSpectrumEmissionEnum.NS08, AddSpectrumEmissionEnum.NS09, AddSpectrumEmissionEnum.NS10, AddSpectrumEmissionEnum.NS100, AddSpectrumEmissionEnum.NS101, AddSpectrumEmissionEnum.NS102, AddSpectrumEmissionEnum.NS103, AddSpectrumEmissionEnum.NS104, AddSpectrumEmissionEnum.NS105, AddSpectrumEmissionEnum.NS106, AddSpectrumEmissionEnum.NS107, AddSpectrumEmissionEnum.NS108, AddSpectrumEmissionEnum.NS109, AddSpectrumEmissionEnum.NS11, AddSpectrumEmissionEnum.NS110, AddSpectrumEmissionEnum.NS111, AddSpectrumEmissionEnum.NS112, AddSpectrumEmissionEnum.NS113, AddSpectrumEmissionEnum.NS114, AddSpectrumEmissionEnum.NS115, AddSpectrumEmissionEnum.NS116, AddSpectrumEmissionEnum.NS117, AddSpectrumEmissionEnum.NS118, AddSpectrumEmissionEnum.NS119, AddSpectrumEmissionEnum.NS12, AddSpectrumEmissionEnum.NS120, AddSpectrumEmissionEnum.NS121, AddSpectrumEmissionEnum.NS122, AddSpectrumEmissionEnum.NS123, AddSpectrumEmissionEnum.NS124, AddSpectrumEmissionEnum.NS125, AddSpectrumEmissionEnum.NS126, AddSpectrumEmissionEnum.NS127, AddSpectrumEmissionEnum.NS128, AddSpectrumEmissionEnum.NS129, AddSpectrumEmissionEnum.NS13, AddSpectrumEmissionEnum.NS130, AddSpectrumEmissionEnum.NS131, AddSpectrumEmissionEnum.NS132, AddSpectrumEmissionEnum.NS133, AddSpectrumEmissionEnum.NS134, AddSpectrumEmissionEnum.NS135, AddSpectrumEmissionEnum.NS136, AddSpectrumEmissionEnum.NS137, AddSpectrumEmissionEnum.NS138, AddSpectrumEmissionEnum.NS139, AddSpectrumEmissionEnum.NS14, AddSpectrumEmissionEnum.NS140, AddSpectrumEmissionEnum.NS141, AddSpectrumEmissionEnum.NS142, AddSpectrumEmissionEnum.NS143, AddSpectrumEmissionEnum.NS144, AddSpectrumEmissionEnum.NS145, AddSpectrumEmissionEnum.NS146, AddSpectrumEmissionEnum.NS147, AddSpectrumEmissionEnum.NS148, AddSpectrumEmissionEnum.NS149, AddSpectrumEmissionEnum.NS15, AddSpectrumEmissionEnum.NS150, AddSpectrumEmissionEnum.NS151, AddSpectrumEmissionEnum.NS152, AddSpectrumEmissionEnum.NS153, AddSpectrumEmissionEnum.NS154, AddSpectrumEmissionEnum.NS155, AddSpectrumEmissionEnum.NS156, AddSpectrumEmissionEnum.NS157, AddSpectrumEmissionEnum.NS158, AddSpectrumEmissionEnum.NS159, AddSpectrumEmissionEnum.NS16, AddSpectrumEmissionEnum.NS160, AddSpectrumEmissionEnum.NS161, AddSpectrumEmissionEnum.NS162, AddSpectrumEmissionEnum.NS163, AddSpectrumEmissionEnum.NS164, AddSpectrumEmissionEnum.NS165, AddSpectrumEmissionEnum.NS166, AddSpectrumEmissionEnum.NS167, AddSpectrumEmissionEnum.NS168, AddSpectrumEmissionEnum.NS169, AddSpectrumEmissionEnum.NS17, AddSpectrumEmissionEnum.NS170, AddSpectrumEmissionEnum.NS171, AddSpectrumEmissionEnum.NS172, AddSpectrumEmissionEnum.NS173, AddSpectrumEmissionEnum.NS174, AddSpectrumEmissionEnum.NS175, AddSpectrumEmissionEnum.NS176, AddSpectrumEmissionEnum.NS177, AddSpectrumEmissionEnum.NS178, AddSpectrumEmissionEnum.NS179, AddSpectrumEmissionEnum.NS18, AddSpectrumEmissionEnum.NS180, AddSpectrumEmissionEnum.NS181, AddSpectrumEmissionEnum.NS182, AddSpectrumEmissionEnum.NS183, AddSpectrumEmissionEnum.NS184, AddSpectrumEmissionEnum.NS185, AddSpectrumEmissionEnum.NS186, AddSpectrumEmissionEnum.NS187, AddSpectrumEmissionEnum.NS188, AddSpectrumEmissionEnum.NS189, AddSpectrumEmissionEnum.NS19, AddSpectrumEmissionEnum.NS190, AddSpectrumEmissionEnum.NS191, AddSpectrumEmissionEnum.NS192, AddSpectrumEmissionEnum.NS193, AddSpectrumEmissionEnum.NS194, AddSpectrumEmissionEnum.NS195, AddSpectrumEmissionEnum.NS196, AddSpectrumEmissionEnum.NS197, AddSpectrumEmissionEnum.NS198, AddSpectrumEmissionEnum.NS199, AddSpectrumEmissionEnum.NS20, AddSpectrumEmissionEnum.NS200, AddSpectrumEmissionEnum.NS201, AddSpectrumEmissionEnum.NS202, AddSpectrumEmissionEnum.NS203, AddSpectrumEmissionEnum.NS204, AddSpectrumEmissionEnum.NS205, AddSpectrumEmissionEnum.NS206, AddSpectrumEmissionEnum.NS207, AddSpectrumEmissionEnum.NS208, AddSpectrumEmissionEnum.NS209, AddSpectrumEmissionEnum.NS21, AddSpectrumEmissionEnum.NS210, AddSpectrumEmissionEnum.NS211, AddSpectrumEmissionEnum.NS212, AddSpectrumEmissionEnum.NS213, AddSpectrumEmissionEnum.NS214, AddSpectrumEmissionEnum.NS215, AddSpectrumEmissionEnum.NS216, AddSpectrumEmissionEnum.NS217, AddSpectrumEmissionEnum.NS218, AddSpectrumEmissionEnum.NS219, AddSpectrumEmissionEnum.NS22, AddSpectrumEmissionEnum.NS220, AddSpectrumEmissionEnum.NS221, AddSpectrumEmissionEnum.NS222, AddSpectrumEmissionEnum.NS223, AddSpectrumEmissionEnum.NS224, AddSpectrumEmissionEnum.NS225, AddSpectrumEmissionEnum.NS226, AddSpectrumEmissionEnum.NS227, AddSpectrumEmissionEnum.NS228, AddSpectrumEmissionEnum.NS229, AddSpectrumEmissionEnum.NS23, AddSpectrumEmissionEnum.NS230, AddSpectrumEmissionEnum.NS231, AddSpectrumEmissionEnum.NS232, AddSpectrumEmissionEnum.NS233, AddSpectrumEmissionEnum.NS234, AddSpectrumEmissionEnum.NS235, AddSpectrumEmissionEnum.NS236, AddSpectrumEmissionEnum.NS237, AddSpectrumEmissionEnum.NS238, AddSpectrumEmissionEnum.NS239, AddSpectrumEmissionEnum.NS24, AddSpectrumEmissionEnum.NS240, AddSpectrumEmissionEnum.NS241, AddSpectrumEmissionEnum.NS242, AddSpectrumEmissionEnum.NS243, AddSpectrumEmissionEnum.NS244, AddSpectrumEmissionEnum.NS245, AddSpectrumEmissionEnum.NS246, AddSpectrumEmissionEnum.NS247, AddSpectrumEmissionEnum.NS248, AddSpectrumEmissionEnum.NS249, AddSpectrumEmissionEnum.NS25, AddSpectrumEmissionEnum.NS250, AddSpectrumEmissionEnum.NS251, AddSpectrumEmissionEnum.NS252, AddSpectrumEmissionEnum.NS253, AddSpectrumEmissionEnum.NS254, AddSpectrumEmissionEnum.NS255, AddSpectrumEmissionEnum.NS256, AddSpectrumEmissionEnum.NS257, AddSpectrumEmissionEnum.NS258, AddSpectrumEmissionEnum.NS259, AddSpectrumEmissionEnum.NS26, AddSpectrumEmissionEnum.NS260, AddSpectrumEmissionEnum.NS261, AddSpectrumEmissionEnum.NS262, AddSpectrumEmissionEnum.NS263, AddSpectrumEmissionEnum.NS264, AddSpectrumEmissionEnum.NS265, AddSpectrumEmissionEnum.NS266, AddSpectrumEmissionEnum.NS267, AddSpectrumEmissionEnum.NS268, AddSpectrumEmissionEnum.NS269, AddSpectrumEmissionEnum.NS27, AddSpectrumEmissionEnum.NS270, AddSpectrumEmissionEnum.NS271, AddSpectrumEmissionEnum.NS272, AddSpectrumEmissionEnum.NS273, AddSpectrumEmissionEnum.NS274, AddSpectrumEmissionEnum.NS275, AddSpectrumEmissionEnum.NS276, AddSpectrumEmissionEnum.NS277, AddSpectrumEmissionEnum.NS278, AddSpectrumEmissionEnum.NS279, AddSpectrumEmissionEnum.NS28, AddSpectrumEmissionEnum.NS280, AddSpectrumEmissionEnum.NS281, AddSpectrumEmissionEnum.NS282, AddSpectrumEmissionEnum.NS283, AddSpectrumEmissionEnum.NS284, AddSpectrumEmissionEnum.NS285, AddSpectrumEmissionEnum.NS286, AddSpectrumEmissionEnum.NS287, AddSpectrumEmissionEnum.NS288, AddSpectrumEmissionEnum.NS29, AddSpectrumEmissionEnum.NS30, AddSpectrumEmissionEnum.NS31, AddSpectrumEmissionEnum.NS32, AddSpectrumEmissionEnum.NS33, AddSpectrumEmissionEnum.NS34, AddSpectrumEmissionEnum.NS35, AddSpectrumEmissionEnum.NS36, AddSpectrumEmissionEnum.NS37, AddSpectrumEmissionEnum.NS38, AddSpectrumEmissionEnum.NS39, AddSpectrumEmissionEnum.NS40, AddSpectrumEmissionEnum.NS41, AddSpectrumEmissionEnum.NS42, AddSpectrumEmissionEnum.NS43, AddSpectrumEmissionEnum.NS44, AddSpectrumEmissionEnum.NS45, AddSpectrumEmissionEnum.NS46, AddSpectrumEmissionEnum.NS47, AddSpectrumEmissionEnum.NS48, AddSpectrumEmissionEnum.NS49, AddSpectrumEmissionEnum.NS50, AddSpectrumEmissionEnum.NS51, AddSpectrumEmissionEnum.NS52, AddSpectrumEmissionEnum.NS53, AddSpectrumEmissionEnum.NS54, AddSpectrumEmissionEnum.NS55, AddSpectrumEmissionEnum.NS56, AddSpectrumEmissionEnum.NS57, AddSpectrumEmissionEnum.NS58, AddSpectrumEmissionEnum.NS59, AddSpectrumEmissionEnum.NS60, AddSpectrumEmissionEnum.NS61, AddSpectrumEmissionEnum.NS62, AddSpectrumEmissionEnum.NS63, AddSpectrumEmissionEnum.NS64, AddSpectrumEmissionEnum.NS65, AddSpectrumEmissionEnum.NS66, AddSpectrumEmissionEnum.NS67, AddSpectrumEmissionEnum.NS68, AddSpectrumEmissionEnum.NS69, AddSpectrumEmissionEnum.NS70, AddSpectrumEmissionEnum.NS71, AddSpectrumEmissionEnum.NS72, AddSpectrumEmissionEnum.NS73, AddSpectrumEmissionEnum.NS74, AddSpectrumEmissionEnum.NS75, AddSpectrumEmissionEnum.NS76, AddSpectrumEmissionEnum.NS77, AddSpectrumEmissionEnum.NS78, AddSpectrumEmissionEnum.NS79, AddSpectrumEmissionEnum.NS80, AddSpectrumEmissionEnum.NS81, AddSpectrumEmissionEnum.NS82, AddSpectrumEmissionEnum.NS83, AddSpectrumEmissionEnum.NS84, AddSpectrumEmissionEnum.NS85, AddSpectrumEmissionEnum.NS86, AddSpectrumEmissionEnum.NS87, AddSpectrumEmissionEnum.NS88, AddSpectrumEmissionEnum.NS89, AddSpectrumEmissionEnum.NS90, AddSpectrumEmissionEnum.NS91, AddSpectrumEmissionEnum.NS92, AddSpectrumEmissionEnum.NS93, AddSpectrumEmissionEnum.NS94, AddSpectrumEmissionEnum.NS95, AddSpectrumEmissionEnum.NS96, AddSpectrumEmissionEnum.NS97, AddSpectrumEmissionEnum.NS98, AddSpectrumEmissionEnum.NS99 })
				{
					driver.Configure.Connection.AsEmission = x;
					AddSpectrumEmissionEnum value = driver.Configure.Connection.AsEmission;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:OBCHange
				foreach (InterBandHandoverModeEnum x in new InterBandHandoverModeEnum[] { InterBandHandoverModeEnum.BHANdover, InterBandHandoverModeEnum.REDirection })
				{
					driver.Configure.Connection.ObChange = x;
					InterBandHandoverModeEnum value = driver.Configure.Connection.ObChange;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:FCHange
				foreach (InterBandHandoverModeEnum x in new InterBandHandoverModeEnum[] { InterBandHandoverModeEnum.BHANdover, InterBandHandoverModeEnum.REDirection })
				{
					driver.Configure.Connection.Fchange = x;
					InterBandHandoverModeEnum value = driver.Configure.Connection.Fchange;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:AMDBearer
				bool value = driver.Configure.Connection.AmdBearer;
				driver.Configure.Connection.AmdBearer = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ROHC:ENABle
				bool value = driver.Configure.Connection.Rohc.Enable;
				driver.Configure.Connection.Rohc.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ROHC:EFOR
				foreach (HeaderCompressionEnum x in new HeaderCompressionEnum[] { HeaderCompressionEnum.ADB, HeaderCompressionEnum.VVB })
				{
					driver.Configure.Connection.Rohc.Efor = x;
					HeaderCompressionEnum value = driver.Configure.Connection.Rohc.Efor;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ROHC:PROFiles
				RsCmwLteSig_Configure_Connection_Rohc.Profiles_Data value = driver.Configure.Connection.Rohc.Profiles;
				driver.Configure.Connection.Rohc.Profiles = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ROHC:ULONly:PROFiles
				bool value = driver.Configure.Connection.Rohc.UlOnly.Profiles;
				driver.Configure.Connection.Rohc.UlOnly.Profiles = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:ROHC:ULONly:ENABle
				bool value = driver.Configure.Connection.Rohc.UlOnly.Enable;
				driver.Configure.Connection.Rohc.UlOnly.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:HDUPlex
				bool value = driver.Configure.Connection.Pcc.Hduplex;
				driver.Configure.Connection.Pcc.Hduplex = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:STYPe
				RsCmwLteSig_Configure_Connection_Pcc.Stype_Data value = driver.Configure.Connection.Pcc.Stype;
				driver.Configure.Connection.Pcc.Stype = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TTIBundling
				bool value = driver.Configure.Connection.Pcc.TtiBundling;
				driver.Configure.Connection.Pcc.TtiBundling = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:DLEQual
				bool value = driver.Configure.Connection.Pcc.DlEqual;
				driver.Configure.Connection.Pcc.DlEqual = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TRANsmission
				foreach (TransmissionModeEnum x in new TransmissionModeEnum[] { TransmissionModeEnum.TM1, TransmissionModeEnum.TM2, TransmissionModeEnum.TM3, TransmissionModeEnum.TM4, TransmissionModeEnum.TM6, TransmissionModeEnum.TM7, TransmissionModeEnum.TM8, TransmissionModeEnum.TM9 })
				{
					driver.Configure.Connection.Pcc.Transmission = x;
					TransmissionModeEnum value = driver.Configure.Connection.Pcc.Transmission;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:DCIFormat
				foreach (DciFormatEnum x in new DciFormatEnum[] { DciFormatEnum.D1, DciFormatEnum.D1A, DciFormatEnum.D1B, DciFormatEnum.D2, DciFormatEnum.D2A, DciFormatEnum.D2B, DciFormatEnum.D2C, DciFormatEnum.D61 })
				{
					driver.Configure.Connection.Pcc.DciFormat = x;
					DciFormatEnum value = driver.Configure.Connection.Pcc.DciFormat;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:NENBantennas
				foreach (AntennasTxAenum x in new AntennasTxAenum[] { AntennasTxAenum.FOUR, AntennasTxAenum.ONE, AntennasTxAenum.TWO })
				{
					driver.Configure.Connection.Pcc.NenbAntennas = x;
					AntennasTxAenum value = driver.Configure.Connection.Pcc.NenbAntennas;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:NOLayers
				foreach (NoOfLayersEnum x in new NoOfLayersEnum[] { NoOfLayersEnum.L2, NoOfLayersEnum.L4 })
				{
					driver.Configure.Connection.Pcc.NoLayers = x;
					NoOfLayersEnum value = driver.Configure.Connection.Pcc.NoLayers;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:PMATrix
				foreach (PrecodingMatrixModeEnum x in new PrecodingMatrixModeEnum[] { PrecodingMatrixModeEnum.PMI0, PrecodingMatrixModeEnum.PMI1, PrecodingMatrixModeEnum.PMI10, PrecodingMatrixModeEnum.PMI11, PrecodingMatrixModeEnum.PMI12, PrecodingMatrixModeEnum.PMI13, PrecodingMatrixModeEnum.PMI14, PrecodingMatrixModeEnum.PMI15, PrecodingMatrixModeEnum.PMI2, PrecodingMatrixModeEnum.PMI3, PrecodingMatrixModeEnum.PMI4, PrecodingMatrixModeEnum.PMI5, PrecodingMatrixModeEnum.PMI6, PrecodingMatrixModeEnum.PMI7, PrecodingMatrixModeEnum.PMI8, PrecodingMatrixModeEnum.PMI9, PrecodingMatrixModeEnum.RANDom_pmi })
				{
					driver.Configure.Connection.Pcc.Pmatrix = x;
					PrecodingMatrixModeEnum value = driver.Configure.Connection.Pcc.Pmatrix;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:MCLuster:UL
				bool value = driver.Configure.Connection.Pcc.Mcluster.Uplink;
				driver.Configure.Connection.Pcc.Mcluster.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:MCLuster:DL
				bool value = driver.Configure.Connection.Pcc.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:HPUSch:ENABle
				bool value = driver.Configure.Connection.Pcc.Hpusch.Enable;
				driver.Configure.Connection.Pcc.Hpusch.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TIA<Nr>
				bool value = driver.Configure.Connection.Pcc.Tia.Get(TbsIndexAltRepCap.Default);
				value = driver.Configure.Connection.Pcc.Tia.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TIA<Nr>
				driver.Configure.Connection.Pcc.Tia.Set(false, TbsIndexAltRepCap.Default);
				driver.Configure.Connection.Pcc.Tia.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:BEAMforming:MODE
				foreach (BeamformingModeEnum x in new BeamformingModeEnum[] { BeamformingModeEnum.OFF, BeamformingModeEnum.ON, BeamformingModeEnum.PMAT, BeamformingModeEnum.TSBF })
				{
					driver.Configure.Connection.Pcc.Beamforming.Mode = x;
					BeamformingModeEnum value = driver.Configure.Connection.Pcc.Beamforming.Mode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:BEAMforming:NOLayers
				foreach (BeamformingNoOfLayersEnum x in new BeamformingNoOfLayersEnum[] { BeamformingNoOfLayersEnum.L1, BeamformingNoOfLayersEnum.L1I, BeamformingNoOfLayersEnum.L2 })
				{
					driver.Configure.Connection.Pcc.Beamforming.NoLayers = x;
					BeamformingNoOfLayersEnum value = driver.Configure.Connection.Pcc.Beamforming.NoLayers;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:BEAMforming:MATRix
				RsCmwLteSig_Configure_Connection_Pcc_Beamforming.Matrix_Data value = driver.Configure.Connection.Pcc.Beamforming.Matrix;
				driver.Configure.Connection.Pcc.Beamforming.Matrix = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel
				RsCmwLteSig_Configure_Connection_Pcc_SchModel.Value_Data value = driver.Configure.Connection.Pcc.SchModel.Value;
				driver.Configure.Connection.Pcc.SchModel.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel:ENABle:MIMO<Mimo>
				bool value = driver.Configure.Connection.Pcc.SchModel.Enable.Mimo;
				driver.Configure.Connection.Pcc.SchModel.Enable.Mimo = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel:ENABle
				bool value = driver.Configure.Connection.Pcc.SchModel.Enable.Value;
				driver.Configure.Connection.Pcc.SchModel.Enable.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel:MSELection:MIMO<Mimo>
				foreach (MimoMatrixSelectionEnum x in new MimoMatrixSelectionEnum[] { MimoMatrixSelectionEnum.CM3Gpp, MimoMatrixSelectionEnum.HADamard, MimoMatrixSelectionEnum.IDENtity, MimoMatrixSelectionEnum.UDEFined })
				{
					driver.Configure.Connection.Pcc.SchModel.Mselection.Mimo = x;
					MimoMatrixSelectionEnum value = driver.Configure.Connection.Pcc.SchModel.Mselection.Mimo;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel:MIMO{mimoCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_SchModel_Mimo.Mimo_Data value = driver.Configure.Connection.Pcc.SchModel.Mimo.Get(MimoRepCap.Default);
				value = driver.Configure.Connection.Pcc.SchModel.Mimo.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SCHModel:MIMO{mimoCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_SchModel_Mimo.Mimo_Data value = new RsCmwLteSig_Configure_Connection_Pcc_SchModel_Mimo.Mimo_Data();
				driver.Configure.Connection.Pcc.SchModel.Mimo.Set(value, MimoRepCap.Default);
				driver.Configure.Connection.Pcc.SchModel.Mimo.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<8>:CHMatrix
				RsCmwLteSig_Configure_Connection_Pcc_Tm.ChMatrix_Data value = driver.Configure.Connection.Pcc.Tm.ChMatrix;
				driver.Configure.Connection.Pcc.Tm.ChMatrix = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:PMATrix
				foreach (PrecodingMatrixModeEnum x in new PrecodingMatrixModeEnum[] { PrecodingMatrixModeEnum.PMI0, PrecodingMatrixModeEnum.PMI1, PrecodingMatrixModeEnum.PMI10, PrecodingMatrixModeEnum.PMI11, PrecodingMatrixModeEnum.PMI12, PrecodingMatrixModeEnum.PMI13, PrecodingMatrixModeEnum.PMI14, PrecodingMatrixModeEnum.PMI15, PrecodingMatrixModeEnum.PMI2, PrecodingMatrixModeEnum.PMI3, PrecodingMatrixModeEnum.PMI4, PrecodingMatrixModeEnum.PMI5, PrecodingMatrixModeEnum.PMI6, PrecodingMatrixModeEnum.PMI7, PrecodingMatrixModeEnum.PMI8, PrecodingMatrixModeEnum.PMI9, PrecodingMatrixModeEnum.RANDom_pmi })
				{
					driver.Configure.Connection.Pcc.Tm.Pmatrix = x;
					PrecodingMatrixModeEnum value = driver.Configure.Connection.Pcc.Tm.Pmatrix;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CODewords
				foreach (AntennasTxAenum x in new AntennasTxAenum[] { AntennasTxAenum.FOUR, AntennasTxAenum.ONE, AntennasTxAenum.TWO })
				{
					driver.Configure.Connection.Pcc.Tm.Codewords = x;
					AntennasTxAenum value = driver.Configure.Connection.Pcc.Tm.Codewords;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:NTXantennas
				foreach (AntennasTxBenum x in new AntennasTxBenum[] { AntennasTxBenum.EIGHt, AntennasTxBenum.FOUR, AntennasTxBenum.TWO })
				{
					driver.Configure.Connection.Pcc.Tm.NtxAntennas = x;
					AntennasTxBenum value = driver.Configure.Connection.Pcc.Tm.NtxAntennas;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:EIGHt<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Eight.Get_Data value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Eight.Get(MatrixEightLineRepCap.Default);
				value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Eight.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:EIGHt<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Eight.Set_Data value = new RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Eight.Set_Data();
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Eight.Set(value, MatrixEightLineRepCap.Default);
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Eight.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:FOUR<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Four.Get_Data value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Four.Get(MatrixFourLineRepCap.Default);
				value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Four.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:FOUR<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Four.Set_Data value = new RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Four.Set_Data();
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Four.Set(value, MatrixFourLineRepCap.Default);
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Four.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:TWO<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Two.Get_Data value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Two.Get(MatrixTwoLineRepCap.Default);
				value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Two.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:TWO<line>
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Two.Set(1.0, 1, 1);
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Two.Set(1.0, 1, 1, MatrixTwoLineRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:MIMO<Mimo>:MSELection
				foreach (MimoMatrixSelectionEnum x in new MimoMatrixSelectionEnum[] { MimoMatrixSelectionEnum.CM3Gpp, MimoMatrixSelectionEnum.HADamard, MimoMatrixSelectionEnum.IDENtity, MimoMatrixSelectionEnum.UDEFined })
				{
					driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Mselection = x;
					MimoMatrixSelectionEnum value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Mselection;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:MIMO<Mimo>:LINE<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Mimo_Line.Get_Data value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Line.Get(MatrixLineRepCap.Default);
				value = driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Line.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CMATrix:MIMO<Mimo>:LINE<line>
				RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Mimo_Line.Set_Data value = new RsCmwLteSig_Configure_Connection_Pcc_Tm_Cmatrix_Mimo_Line.Set_Data();
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Line.Set(value, MatrixLineRepCap.Default);
				driver.Configure.Connection.Pcc.Tm.Cmatrix.Mimo.Line.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:ZP:BITS
				string value = driver.Configure.Connection.Pcc.Tm.Zp.Bits;
				driver.Configure.Connection.Pcc.Tm.Zp.Bits = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:ZP:CSIRs:SUBFrame
				int value = driver.Configure.Connection.Pcc.Tm.Zp.Csirs.Subframe;
				driver.Configure.Connection.Pcc.Tm.Zp.Csirs.Subframe = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CSIRs:APORts
				foreach (AntennaPortsEnum x in new AntennaPortsEnum[] { AntennaPortsEnum.NONE, AntennaPortsEnum.P15, AntennaPortsEnum.P1516, AntennaPortsEnum.P1518, AntennaPortsEnum.P1522 })
				{
					driver.Configure.Connection.Pcc.Tm.Csirs.Aports = x;
					AntennaPortsEnum value = driver.Configure.Connection.Pcc.Tm.Csirs.Aports;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CSIRs:SUBFrame
				int value = driver.Configure.Connection.Pcc.Tm.Csirs.Subframe;
				driver.Configure.Connection.Pcc.Tm.Csirs.Subframe = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CSIRs:RESource
				int value = driver.Configure.Connection.Pcc.Tm.Csirs.Resource;
				driver.Configure.Connection.Pcc.Tm.Csirs.Resource = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:TM<nr>:CSIRs:POWer
				int value = driver.Configure.Connection.Pcc.Tm.Csirs.Power;
				driver.Configure.Connection.Pcc.Tm.Csirs.Power = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:PZERo:MAPPing
				foreach (PortsMappingEnum x in new PortsMappingEnum[] { PortsMappingEnum.R1, PortsMappingEnum.R1R2 })
				{
					driver.Configure.Connection.Pcc.Pzero.Mapping = x;
					PortsMappingEnum value = driver.Configure.Connection.Pcc.Pzero.Mapping;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:UL
				RsCmwLteSig_Configure_Connection_Pcc_Rmc.Uplink_Data value = driver.Configure.Connection.Pcc.Rmc.Uplink;
				driver.Configure.Connection.Pcc.Rmc.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Pcc_Rmc_Mcluster.Uplink_Data value = driver.Configure.Connection.Pcc.Rmc.Mcluster.Uplink;
				driver.Configure.Connection.Pcc.Rmc.Mcluster.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:EMTC:SFPattern
				foreach (EmtcRmcPatternEnum x in new EmtcRmcPatternEnum[] { EmtcRmcPatternEnum.P1, EmtcRmcPatternEnum.P2, EmtcRmcPatternEnum.P3, EmtcRmcPatternEnum.P4, EmtcRmcPatternEnum.P5 })
				{
					driver.Configure.Connection.Pcc.Rmc.Emtc.SfPattern = x;
					EmtcRmcPatternEnum value = driver.Configure.Connection.Pcc.Rmc.Emtc.SfPattern;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:EMTC:NBPosition:UL
				foreach (UplinkNarrowBandPositionEnum x in new UplinkNarrowBandPositionEnum[] { UplinkNarrowBandPositionEnum.HIGH, UplinkNarrowBandPositionEnum.LOW, UplinkNarrowBandPositionEnum.NB1, UplinkNarrowBandPositionEnum.NB10, UplinkNarrowBandPositionEnum.NB11, UplinkNarrowBandPositionEnum.NB12, UplinkNarrowBandPositionEnum.NB13, UplinkNarrowBandPositionEnum.NB14, UplinkNarrowBandPositionEnum.NB2, UplinkNarrowBandPositionEnum.NB3, UplinkNarrowBandPositionEnum.NB4, UplinkNarrowBandPositionEnum.NB5, UplinkNarrowBandPositionEnum.NB6, UplinkNarrowBandPositionEnum.NB7, UplinkNarrowBandPositionEnum.NB8, UplinkNarrowBandPositionEnum.NB9 })
				{
					driver.Configure.Connection.Pcc.Rmc.Emtc.NbPosition.Uplink = x;
					UplinkNarrowBandPositionEnum value = driver.Configure.Connection.Pcc.Rmc.Emtc.NbPosition.Uplink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:EMTC:NBPosition:DL
				foreach (DownlinkNarrowBandPositionEnum x in new DownlinkNarrowBandPositionEnum[] { DownlinkNarrowBandPositionEnum.GPP3, DownlinkNarrowBandPositionEnum.HIGH, DownlinkNarrowBandPositionEnum.LOW, DownlinkNarrowBandPositionEnum.MID })
				{
					driver.Configure.Connection.Pcc.Rmc.Emtc.NbPosition.Downlink = x;
					DownlinkNarrowBandPositionEnum value = driver.Configure.Connection.Pcc.Rmc.Emtc.NbPosition.Downlink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_Rmc_Downlink.Downlink_Data value = driver.Configure.Connection.Pcc.Rmc.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.Rmc.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_Rmc_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Pcc_Rmc_Downlink.Downlink_Data();
				driver.Configure.Connection.Pcc.Rmc.Downlink.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.Rmc.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:RBPosition:UL
				foreach (RbPositionEnum x in new RbPositionEnum[] { RbPositionEnum.FULL, RbPositionEnum.HIGH, RbPositionEnum.LOW, RbPositionEnum.MID, RbPositionEnum.P0, RbPositionEnum.P1, RbPositionEnum.P10, RbPositionEnum.P11, RbPositionEnum.P12, RbPositionEnum.P13, RbPositionEnum.P14, RbPositionEnum.P15, RbPositionEnum.P16, RbPositionEnum.P19, RbPositionEnum.P2, RbPositionEnum.P20, RbPositionEnum.P21, RbPositionEnum.P22, RbPositionEnum.P24, RbPositionEnum.P25, RbPositionEnum.P28, RbPositionEnum.P3, RbPositionEnum.P30, RbPositionEnum.P31, RbPositionEnum.P33, RbPositionEnum.P36, RbPositionEnum.P37, RbPositionEnum.P39, RbPositionEnum.P4, RbPositionEnum.P40, RbPositionEnum.P43, RbPositionEnum.P44, RbPositionEnum.P45, RbPositionEnum.P48, RbPositionEnum.P49, RbPositionEnum.P50, RbPositionEnum.P51, RbPositionEnum.P52, RbPositionEnum.P54, RbPositionEnum.P56, RbPositionEnum.P57, RbPositionEnum.P58, RbPositionEnum.P6, RbPositionEnum.P62, RbPositionEnum.P63, RbPositionEnum.P66, RbPositionEnum.P68, RbPositionEnum.P7, RbPositionEnum.P70, RbPositionEnum.P74, RbPositionEnum.P75, RbPositionEnum.P8, RbPositionEnum.P83, RbPositionEnum.P9, RbPositionEnum.P96, RbPositionEnum.P99 })
				{
					driver.Configure.Connection.Pcc.Rmc.RbPosition.Uplink = x;
					RbPositionEnum value = driver.Configure.Connection.Pcc.Rmc.RbPosition.Uplink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:RBPosition:DL{streamCmdVal}
				DownlinkRsrcBlockPositionEnum value = driver.Configure.Connection.Pcc.Rmc.RbPosition.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.Rmc.RbPosition.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:RBPosition:DL{streamCmdVal}
				foreach (DownlinkRsrcBlockPositionEnum x in new DownlinkRsrcBlockPositionEnum[] { DownlinkRsrcBlockPositionEnum.HIGH, DownlinkRsrcBlockPositionEnum.LOW, DownlinkRsrcBlockPositionEnum.P10, DownlinkRsrcBlockPositionEnum.P23, DownlinkRsrcBlockPositionEnum.P35, DownlinkRsrcBlockPositionEnum.P48, DownlinkRsrcBlockPositionEnum.P5 })
				{
					driver.Configure.Connection.Pcc.Rmc.RbPosition.Downlink.Set(x);
					driver.Configure.Connection.Pcc.Rmc.RbPosition.Downlink.Set(x, StreamRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:VERSion:DL{streamCmdVal}
				int value = driver.Configure.Connection.Pcc.Rmc.Version.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.Rmc.Version.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:RMC:VERSion:DL{streamCmdVal}
				driver.Configure.Connection.Pcc.Rmc.Version.Downlink.Set(1, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.Rmc.Version.Downlink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:UL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels.Uplink_Data value = driver.Configure.Connection.Pcc.UdChannels.Uplink;
				driver.Configure.Connection.Pcc.UdChannels.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Mcluster.Uplink_Data value = driver.Configure.Connection.Pcc.UdChannels.Mcluster.Uplink;
				driver.Configure.Connection.Pcc.UdChannels.Mcluster.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Pcc.UdChannels.Mcluster.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.UdChannels.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Pcc.UdChannels.Mcluster.Downlink.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.UdChannels.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:NBPosition:DL
				foreach (DownlinkNarrowBandPositionEnum x in new DownlinkNarrowBandPositionEnum[] { DownlinkNarrowBandPositionEnum.GPP3, DownlinkNarrowBandPositionEnum.HIGH, DownlinkNarrowBandPositionEnum.LOW, DownlinkNarrowBandPositionEnum.MID })
				{
					driver.Configure.Connection.Pcc.UdChannels.Emtc.NbPosition.Downlink = x;
					DownlinkNarrowBandPositionEnum value = driver.Configure.Connection.Pcc.UdChannels.Emtc.NbPosition.Downlink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:NBPosition:UL
				foreach (UplinkNarrowBandPositionEnum x in new UplinkNarrowBandPositionEnum[] { UplinkNarrowBandPositionEnum.HIGH, UplinkNarrowBandPositionEnum.LOW, UplinkNarrowBandPositionEnum.NB1, UplinkNarrowBandPositionEnum.NB10, UplinkNarrowBandPositionEnum.NB11, UplinkNarrowBandPositionEnum.NB12, UplinkNarrowBandPositionEnum.NB13, UplinkNarrowBandPositionEnum.NB14, UplinkNarrowBandPositionEnum.NB2, UplinkNarrowBandPositionEnum.NB3, UplinkNarrowBandPositionEnum.NB4, UplinkNarrowBandPositionEnum.NB5, UplinkNarrowBandPositionEnum.NB6, UplinkNarrowBandPositionEnum.NB7, UplinkNarrowBandPositionEnum.NB8, UplinkNarrowBandPositionEnum.NB9 })
				{
					driver.Configure.Connection.Pcc.UdChannels.Emtc.NbPosition.Uplink = x;
					UplinkNarrowBandPositionEnum value = driver.Configure.Connection.Pcc.UdChannels.Emtc.NbPosition.Uplink;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:B:UL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Emtc_B.Uplink_Data value = driver.Configure.Connection.Pcc.UdChannels.Emtc.B.Uplink;
				driver.Configure.Connection.Pcc.UdChannels.Emtc.B.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:B:DL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Emtc_B.Downlink_Data value = driver.Configure.Connection.Pcc.UdChannels.Emtc.B.Downlink;
				driver.Configure.Connection.Pcc.UdChannels.Emtc.B.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:A:DL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Emtc_A.Downlink_Data value = driver.Configure.Connection.Pcc.UdChannels.Emtc.A.Downlink;
				driver.Configure.Connection.Pcc.UdChannels.Emtc.A.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:EMTC:A:UL
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Emtc_A.Uplink_Data value = driver.Configure.Connection.Pcc.UdChannels.Emtc.A.Uplink;
				driver.Configure.Connection.Pcc.UdChannels.Emtc.A.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Downlink.Downlink_Data value = driver.Configure.Connection.Pcc.UdChannels.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.UdChannels.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Pcc_UdChannels_Downlink.Downlink_Data();
				driver.Configure.Connection.Pcc.UdChannels.Downlink.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.UdChannels.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:SINTerval
				foreach (SpsIntevalEnum x in new SpsIntevalEnum[] { SpsIntevalEnum.S10, SpsIntevalEnum.S128, SpsIntevalEnum.S160, SpsIntevalEnum.S20, SpsIntevalEnum.S32, SpsIntevalEnum.S320, SpsIntevalEnum.S40, SpsIntevalEnum.S64, SpsIntevalEnum.S640, SpsIntevalEnum.S80 })
				{
					driver.Configure.Connection.Pcc.Sps.Sinterval = x;
					SpsIntevalEnum value = driver.Configure.Connection.Pcc.Sps.Sinterval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:TIConfig
				bool value = driver.Configure.Connection.Pcc.Sps.TiConfig;
				driver.Configure.Connection.Pcc.Sps.TiConfig = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:UL
				RsCmwLteSig_Configure_Connection_Pcc_Sps.Uplink_Data value = driver.Configure.Connection.Pcc.Sps.Uplink;
				driver.Configure.Connection.Pcc.Sps.Uplink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_Sps_Downlink.Downlink_Data value = driver.Configure.Connection.Pcc.Sps.Downlink.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.Sps.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_Sps_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Pcc_Sps_Downlink.Downlink_Data();
				driver.Configure.Connection.Pcc.Sps.Downlink.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.Sps.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink.Get_Data value = driver.Configure.Connection.Pcc.UdttiBased.Downlink.Get(1.0, StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.UdttiBased.Downlink.Get(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink.Set_Data value = new RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink.Set_Data();
				driver.Configure.Connection.Pcc.UdttiBased.Downlink.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.UdttiBased.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink_All.All_Data value = driver.Configure.Connection.Pcc.UdttiBased.Downlink.All.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.UdttiBased.Downlink.All.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink_All.All_Data value = new RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Downlink_All.All_Data();
				driver.Configure.Connection.Pcc.UdttiBased.Downlink.All.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.UdttiBased.Downlink.All.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:UL
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Uplink.Get_Data value = driver.Configure.Connection.Pcc.UdttiBased.Uplink.Get(1.0);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:UL
				driver.Configure.Connection.Pcc.UdttiBased.Uplink.Set(1.0, 1, 1, ModulationEnum.Q1024, 1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:UL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_UdttiBased_Uplink.All_Data value = driver.Configure.Connection.Pcc.UdttiBased.Uplink.All;
				driver.Configure.Connection.Pcc.UdttiBased.Uplink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:QAM<ModOrder>:DL
				bool value = driver.Configure.Connection.Pcc.Qam.Downlink.Get(QAMmodulationOrderBRepCap.Default);
				value = driver.Configure.Connection.Pcc.Qam.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:QAM<ModOrder>:DL
				driver.Configure.Connection.Pcc.Qam.Downlink.Set(false, QAMmodulationOrderBRepCap.Default);
				driver.Configure.Connection.Pcc.Qam.Downlink.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Pcc_FcttiBased_Downlink.Get_Data value = driver.Configure.Connection.Pcc.FcttiBased.Downlink.Get(1.0, StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.FcttiBased.Downlink.Get(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCTTibased:DL{streamCmdVal}
				driver.Configure.Connection.Pcc.FcttiBased.Downlink.Set(1.0, 1, 1, 1);
				driver.Configure.Connection.Pcc.FcttiBased.Downlink.Set(1.0, 1, 1, 1, StreamRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Pcc_FcttiBased_Downlink_All.All_Data value = driver.Configure.Connection.Pcc.FcttiBased.Downlink.All.Get(StreamRepCap.Default);
				value = driver.Configure.Connection.Pcc.FcttiBased.Downlink.All.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Pcc_FcttiBased_Downlink_All.All_Data value = new RsCmwLteSig_Configure_Connection_Pcc_FcttiBased_Downlink_All.All_Data();
				driver.Configure.Connection.Pcc.FcttiBased.Downlink.All.Set(value, StreamRepCap.Default);
				driver.Configure.Connection.Pcc.FcttiBased.Downlink.All.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fwbcqi_Mcluster.Downlink_Data value = driver.Configure.Connection.Pcc.Fwbcqi.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Fwbcqi.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:STTI
				List<bool> value = driver.Configure.Connection.Pcc.Fwbcqi.Downlink.Stti;
				driver.Configure.Connection.Pcc.Fwbcqi.Downlink.Stti = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fwbcqi_Downlink.Value_Data value = driver.Configure.Connection.Pcc.Fwbcqi.Downlink.Value;
				driver.Configure.Connection.Pcc.Fwbcqi.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.UserDefined;
				driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:CSIRs:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined;
				driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined;
				driver.Configure.Connection.Pcc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPMI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fpmi_Mcluster.Downlink_Data value = driver.Configure.Connection.Pcc.Fpmi.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Fpmi.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPMI:DL:STTI
				List<bool> value = driver.Configure.Connection.Pcc.Fpmi.Downlink.Stti;
				driver.Configure.Connection.Pcc.Fpmi.Downlink.Stti = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPMI:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fpmi_Downlink.Value_Data value = driver.Configure.Connection.Pcc.Fpmi.Downlink.Value;
				driver.Configure.Connection.Pcc.Fpmi.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fcri_Mcluster.Downlink_Data value = driver.Configure.Connection.Pcc.Fcri.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Fcri.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:STTI
				List<bool> value = driver.Configure.Connection.Pcc.Fcri.Downlink.Stti;
				driver.Configure.Connection.Pcc.Fcri.Downlink.Stti = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fcri_Downlink.Value_Data value = driver.Configure.Connection.Pcc.Fcri.Downlink.Value;
				driver.Configure.Connection.Pcc.Fcri.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fcri.Downlink.McsTable.UserDefined;
				driver.Configure.Connection.Pcc.Fcri.Downlink.McsTable.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fcri.Downlink.McsTable.Ssubframe.UserDefined;
				driver.Configure.Connection.Pcc.Fcri.Downlink.McsTable.Ssubframe.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fcpri_Mcluster.Downlink_Data value = driver.Configure.Connection.Pcc.Fcpri.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Fcpri.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:STTI
				List<bool> value = driver.Configure.Connection.Pcc.Fcpri.Downlink.Stti;
				driver.Configure.Connection.Pcc.Fcpri.Downlink.Stti = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fcpri_Downlink.Value_Data value = driver.Configure.Connection.Pcc.Fcpri.Downlink.Value;
				driver.Configure.Connection.Pcc.Fcpri.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.UserDefined;
				driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:CSIRs:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.Csirs.UserDefined;
				driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.Csirs.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined;
				driver.Configure.Connection.Pcc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fpri_Mcluster.Downlink_Data value = driver.Configure.Connection.Pcc.Fpri.Mcluster.Downlink;
				driver.Configure.Connection.Pcc.Fpri.Mcluster.Downlink = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPRI:DL:STTI
				List<bool> value = driver.Configure.Connection.Pcc.Fpri.Downlink.Stti;
				driver.Configure.Connection.Pcc.Fpri.Downlink.Stti = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:FPRI:DL
				RsCmwLteSig_Configure_Connection_Pcc_Fpri_Downlink.Value_Data value = driver.Configure.Connection.Pcc.Fpri.Downlink.Value;
				driver.Configure.Connection.Pcc.Fpri.Downlink.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:EMAMode:A:DL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Emamode_A_Downlink.All_Data value = driver.Configure.Connection.Pcc.Emamode.A.Downlink.All;
				driver.Configure.Connection.Pcc.Emamode.A.Downlink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:EMAMode:A:UL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Emamode_A_Uplink.All_Data value = driver.Configure.Connection.Pcc.Emamode.A.Uplink.All;
				driver.Configure.Connection.Pcc.Emamode.A.Uplink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:EMAMode:B:DL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Emamode_B_Downlink.All_Data value = driver.Configure.Connection.Pcc.Emamode.B.Downlink.All;
				driver.Configure.Connection.Pcc.Emamode.B.Downlink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:EMAMode:B:UL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Emamode_B_Uplink.All_Data value = driver.Configure.Connection.Pcc.Emamode.B.Uplink.All;
				driver.Configure.Connection.Pcc.Emamode.B.Uplink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:CSCHeduling:B:DL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Cscheduling_B_Downlink.All_Data value = driver.Configure.Connection.Pcc.Cscheduling.B.Downlink.All;
				driver.Configure.Connection.Pcc.Cscheduling.B.Downlink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:CSCHeduling:B:UL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Cscheduling_B_Uplink.All_Data value = driver.Configure.Connection.Pcc.Cscheduling.B.Uplink.All;
				driver.Configure.Connection.Pcc.Cscheduling.B.Uplink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:CSCHeduling:A:DL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Cscheduling_A_Downlink.All_Data value = driver.Configure.Connection.Pcc.Cscheduling.A.Downlink.All;
				driver.Configure.Connection.Pcc.Cscheduling.A.Downlink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:CSCHeduling:A:UL:ALL
				RsCmwLteSig_Configure_Connection_Pcc_Cscheduling_A_Uplink.All_Data value = driver.Configure.Connection.Pcc.Cscheduling.A.Uplink.All;
				driver.Configure.Connection.Pcc.Cscheduling.A.Uplink.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:PDCCh:SYMBol
				foreach (PdcchSymbolsCountEnum x in new PdcchSymbolsCountEnum[] { PdcchSymbolsCountEnum.AUTO, PdcchSymbolsCountEnum.P1, PdcchSymbolsCountEnum.P2, PdcchSymbolsCountEnum.P3, PdcchSymbolsCountEnum.P4 })
				{
					driver.Configure.Connection.Pcc.Pdcch.Symbol = x;
					PdcchSymbolsCountEnum value = driver.Configure.Connection.Pcc.Pdcch.Symbol;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:PDCCh:ALEVel
				foreach (AggregationlevelEnum x in new AggregationlevelEnum[] { AggregationlevelEnum.AUTO, AggregationlevelEnum.D1U1, AggregationlevelEnum.D4U2, AggregationlevelEnum.D4U4, AggregationlevelEnum.D8U4, AggregationlevelEnum.D8U8 })
				{
					driver.Configure.Connection.Pcc.Pdcch.Alevel = x;
					AggregationlevelEnum value = driver.Configure.Connection.Pcc.Pdcch.Alevel;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection[:PCC]:PUCCh:FFCA
				foreach (PucchFormatEnum x in new PucchFormatEnum[] { PucchFormatEnum.F1BCs, PucchFormatEnum.F3, PucchFormatEnum.F4, PucchFormatEnum.F5 })
				{
					driver.Configure.Connection.Pcc.Pucch.Ffca = x;
					PucchFormatEnum value = driver.Configure.Connection.Pcc.Pucch.Ffca;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:EASY:BFBW
				bool value = driver.Configure.Connection.Easy.Bfbw;
				driver.Configure.Connection.Easy.Bfbw = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:TDBearer:RLCMode
				foreach (RlcModeEnum x in new RlcModeEnum[] { RlcModeEnum.AM, RlcModeEnum.UM })
				{
					driver.Configure.Connection.Tdbearer.RlcMode = x;
					RlcModeEnum value = driver.Configure.Connection.Tdbearer.RlcMode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SIPHandling:ENABle
				bool value = driver.Configure.Connection.SipHandling.Enable;
				driver.Configure.Connection.SipHandling.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SIPHandling:APN
				string value = driver.Configure.Connection.SipHandling.Apn;
				driver.Configure.Connection.SipHandling.Apn = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:ENABle
				foreach (EnableDrxEnum x in new EnableDrxEnum[] { EnableDrxEnum.DRXL, EnableDrxEnum.DRXS, EnableDrxEnum.OFF, EnableDrxEnum.ON, EnableDrxEnum.UDEFined })
				{
					driver.Configure.Connection.Cdrx.Enable = x;
					EnableDrxEnum value = driver.Configure.Connection.Cdrx.Enable;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:ODTimer
				foreach (OnDurationTimerEnum x in new OnDurationTimerEnum[] { OnDurationTimerEnum.PSF1, OnDurationTimerEnum.PSF10, OnDurationTimerEnum.PSF100, OnDurationTimerEnum.PSF1000, OnDurationTimerEnum.PSF1200, OnDurationTimerEnum.PSF1600, OnDurationTimerEnum.PSF2, OnDurationTimerEnum.PSF20, OnDurationTimerEnum.PSF200, OnDurationTimerEnum.PSF3, OnDurationTimerEnum.PSF30, OnDurationTimerEnum.PSF300, OnDurationTimerEnum.PSF4, OnDurationTimerEnum.PSF40, OnDurationTimerEnum.PSF400, OnDurationTimerEnum.PSF5, OnDurationTimerEnum.PSF50, OnDurationTimerEnum.PSF500, OnDurationTimerEnum.PSF6, OnDurationTimerEnum.PSF60, OnDurationTimerEnum.PSF600, OnDurationTimerEnum.PSF8, OnDurationTimerEnum.PSF80, OnDurationTimerEnum.PSF800 })
				{
					driver.Configure.Connection.Cdrx.OdTimer = x;
					OnDurationTimerEnum value = driver.Configure.Connection.Cdrx.OdTimer;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:ITIMer
				foreach (InactivityTimerEnum x in new InactivityTimerEnum[] { InactivityTimerEnum.PSF1, InactivityTimerEnum.PSF10, InactivityTimerEnum.PSF100, InactivityTimerEnum.PSF1280, InactivityTimerEnum.PSF1920, InactivityTimerEnum.PSF2, InactivityTimerEnum.PSF20, InactivityTimerEnum.PSF200, InactivityTimerEnum.PSF2560, InactivityTimerEnum.PSF3, InactivityTimerEnum.PSF30, InactivityTimerEnum.PSF300, InactivityTimerEnum.PSF4, InactivityTimerEnum.PSF40, InactivityTimerEnum.PSF5, InactivityTimerEnum.PSF50, InactivityTimerEnum.PSF500, InactivityTimerEnum.PSF6, InactivityTimerEnum.PSF60, InactivityTimerEnum.PSF750, InactivityTimerEnum.PSF8, InactivityTimerEnum.PSF80 })
				{
					driver.Configure.Connection.Cdrx.Itimer = x;
					InactivityTimerEnum value = driver.Configure.Connection.Cdrx.Itimer;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:RTIMer
				foreach (RetransmissionTimerEnum x in new RetransmissionTimerEnum[] { RetransmissionTimerEnum.PSF0, RetransmissionTimerEnum.PSF1, RetransmissionTimerEnum.PSF112, RetransmissionTimerEnum.PSF128, RetransmissionTimerEnum.PSF16, RetransmissionTimerEnum.PSF160, RetransmissionTimerEnum.PSF2, RetransmissionTimerEnum.PSF24, RetransmissionTimerEnum.PSF320, RetransmissionTimerEnum.PSF33, RetransmissionTimerEnum.PSF4, RetransmissionTimerEnum.PSF40, RetransmissionTimerEnum.PSF6, RetransmissionTimerEnum.PSF64, RetransmissionTimerEnum.PSF8, RetransmissionTimerEnum.PSF80, RetransmissionTimerEnum.PSF96 })
				{
					driver.Configure.Connection.Cdrx.Rtimer = x;
					RetransmissionTimerEnum value = driver.Configure.Connection.Cdrx.Rtimer;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:LDCYcle
				foreach (LdCycleEnum x in new LdCycleEnum[] { LdCycleEnum.SF10, LdCycleEnum.SF1024, LdCycleEnum.SF10240, LdCycleEnum.SF128, LdCycleEnum.SF1280, LdCycleEnum.SF160, LdCycleEnum.SF20, LdCycleEnum.SF2048, LdCycleEnum.SF256, LdCycleEnum.SF2560, LdCycleEnum.SF32, LdCycleEnum.SF320, LdCycleEnum.SF40, LdCycleEnum.SF512, LdCycleEnum.SF5120, LdCycleEnum.SF60, LdCycleEnum.SF64, LdCycleEnum.SF640, LdCycleEnum.SF70, LdCycleEnum.SF80 })
				{
					driver.Configure.Connection.Cdrx.Ldcycle = x;
					LdCycleEnum value = driver.Configure.Connection.Cdrx.Ldcycle;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:SOFFset
				int value = driver.Configure.Connection.Cdrx.Soffset;
				driver.Configure.Connection.Cdrx.Soffset = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:SCENable
				bool value = driver.Configure.Connection.Cdrx.ScEnable;
				driver.Configure.Connection.Cdrx.ScEnable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:SDCYcle
				foreach (SdCycleEnum x in new SdCycleEnum[] { SdCycleEnum.SF10, SdCycleEnum.SF128, SdCycleEnum.SF16, SdCycleEnum.SF160, SdCycleEnum.SF2, SdCycleEnum.SF20, SdCycleEnum.SF256, SdCycleEnum.SF32, SdCycleEnum.SF320, SdCycleEnum.SF4, SdCycleEnum.SF40, SdCycleEnum.SF5, SdCycleEnum.SF512, SdCycleEnum.SF64, SdCycleEnum.SF640, SdCycleEnum.SF8, SdCycleEnum.SF80 })
				{
					driver.Configure.Connection.Cdrx.Sdcycle = x;
					SdCycleEnum value = driver.Configure.Connection.Cdrx.Sdcycle;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:SCTimer
				int value = driver.Configure.Connection.Cdrx.ScTimer;
				driver.Configure.Connection.Cdrx.ScTimer = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:IMODe:CLENgth
				foreach (IdleDrxLengthEnum x in new IdleDrxLengthEnum[] { IdleDrxLengthEnum.L1024, IdleDrxLengthEnum.L10240, IdleDrxLengthEnum.L12288, IdleDrxLengthEnum.L131072, IdleDrxLengthEnum.L14336, IdleDrxLengthEnum.L16384, IdleDrxLengthEnum.L2048, IdleDrxLengthEnum.L262144, IdleDrxLengthEnum.L32768, IdleDrxLengthEnum.L4096, IdleDrxLengthEnum.L512, IdleDrxLengthEnum.L6144, IdleDrxLengthEnum.L65536, IdleDrxLengthEnum.L8192 })
				{
					driver.Configure.Connection.Cdrx.Imode.Clength = x;
					IdleDrxLengthEnum value = driver.Configure.Connection.Cdrx.Imode.Clength;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:IMODe:PTWindow
				foreach (WindowEnum x in new WindowEnum[] { WindowEnum.W10240, WindowEnum.W11520, WindowEnum.W1280, WindowEnum.W12800, WindowEnum.W14080, WindowEnum.W15360, WindowEnum.W16640, WindowEnum.W17920, WindowEnum.W19200, WindowEnum.W20480, WindowEnum.W2560, WindowEnum.W3840, WindowEnum.W5120, WindowEnum.W6400, WindowEnum.W7680, WindowEnum.W8960 })
				{
					driver.Configure.Connection.Cdrx.Imode.PtWindow = x;
					WindowEnum value = driver.Configure.Connection.Cdrx.Imode.PtWindow;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CDRX:IMODe:ENABle
				bool value = driver.Configure.Connection.Cdrx.Imode.Enable;
				driver.Configure.Connection.Cdrx.Imode.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:MCLuster:UL
				bool value = driver.Configure.Connection.Scc.Mcluster.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Mcluster.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:MCLuster:UL
				driver.Configure.Connection.Scc.Mcluster.Uplink.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Mcluster.Uplink.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:MCLuster:DL
				bool value = driver.Configure.Connection.Scc.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:MCLuster:DL
				driver.Configure.Connection.Scc.Mcluster.Downlink.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Mcluster.Downlink.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:STYPe
				RsCmwLteSig_Configure_Connection_Scc_Stype.Stype_Data value = driver.Configure.Connection.Scc.Stype.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Stype.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:STYPe
				RsCmwLteSig_Configure_Connection_Scc_Stype.Stype_Data value = new RsCmwLteSig_Configure_Connection_Scc_Stype.Stype_Data();
				driver.Configure.Connection.Scc.Stype.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Stype.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:ASEMission:CAGGregation
				SemissionValueEnum value = driver.Configure.Connection.Scc.AsEmission.Caggregation.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.AsEmission.Caggregation.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:ASEMission:CAGGregation
				foreach (SemissionValueEnum x in new SemissionValueEnum[] { SemissionValueEnum.NS01, SemissionValueEnum.NS02, SemissionValueEnum.NS03, SemissionValueEnum.NS04, SemissionValueEnum.NS05, SemissionValueEnum.NS06, SemissionValueEnum.NS07, SemissionValueEnum.NS08, SemissionValueEnum.NS09, SemissionValueEnum.NS10, SemissionValueEnum.NS11, SemissionValueEnum.NS12, SemissionValueEnum.NS13, SemissionValueEnum.NS14, SemissionValueEnum.NS15, SemissionValueEnum.NS16, SemissionValueEnum.NS17, SemissionValueEnum.NS18, SemissionValueEnum.NS19, SemissionValueEnum.NS20, SemissionValueEnum.NS21, SemissionValueEnum.NS22, SemissionValueEnum.NS23, SemissionValueEnum.NS24, SemissionValueEnum.NS25, SemissionValueEnum.NS26, SemissionValueEnum.NS27, SemissionValueEnum.NS28, SemissionValueEnum.NS29, SemissionValueEnum.NS30, SemissionValueEnum.NS31, SemissionValueEnum.NS32 })
				{
					driver.Configure.Connection.Scc.AsEmission.Caggregation.Set(x);
					driver.Configure.Connection.Scc.AsEmission.Caggregation.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SEXecute
				driver.Configure.Connection.Scc.Sexecute.Set(SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Sexecute.SetAndWait(SecondaryCompCarrierRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:CEXecute
				driver.Configure.Connection.Scc.Cexecute.Set(SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Cexecute.SetAndWait(SecondaryCompCarrierRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:HPUSch:ENABle
				bool value = driver.Configure.Connection.Scc.Hpusch.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Hpusch.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:HPUSch:ENABle
				driver.Configure.Connection.Scc.Hpusch.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Hpusch.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:TBURsts
				BurstsEnum value = driver.Configure.Connection.Scc.Laa.Tbursts.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Tbursts.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:TBURsts
				foreach (BurstsEnum x in new BurstsEnum[] { BurstsEnum.FBURst, BurstsEnum.RBURst })
				{
					driver.Configure.Connection.Scc.Laa.Tbursts.Set(x);
					driver.Configure.Connection.Scc.Laa.Tbursts.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:PSFConfig
				PallocConfigEnum value = driver.Configure.Connection.Scc.Laa.Rburst.PsfConfig.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Rburst.PsfConfig.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:PSFConfig
				foreach (PallocConfigEnum x in new PallocConfigEnum[] { PallocConfigEnum.BOTH, PallocConfigEnum.END, PallocConfigEnum.INIT, PallocConfigEnum.NO })
				{
					driver.Configure.Connection.Scc.Laa.Rburst.PsfConfig.Set(x);
					driver.Configure.Connection.Scc.Laa.Rburst.PsfConfig.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:BLENgth
				List<bool> value = driver.Configure.Connection.Scc.Laa.Rburst.Blength.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Rburst.Blength.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:BLENgth
				driver.Configure.Connection.Scc.Laa.Rburst.Blength.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Rburst.Blength.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:LSConfig
				List<bool> value = driver.Configure.Connection.Scc.Laa.Rburst.LsConfig.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Rburst.LsConfig.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:LSConfig
				driver.Configure.Connection.Scc.Laa.Rburst.LsConfig.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Rburst.LsConfig.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:IPSubframe
				bool value = driver.Configure.Connection.Scc.Laa.Rburst.IpSubframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Rburst.IpSubframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:IPSubframe
				driver.Configure.Connection.Scc.Laa.Rburst.IpSubframe.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Rburst.IpSubframe.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:TPRobability
				int value = driver.Configure.Connection.Scc.Laa.Rburst.Tprobability.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Rburst.Tprobability.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:RBURst:TPRobability
				driver.Configure.Connection.Scc.Laa.Rburst.Tprobability.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Rburst.Tprobability.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:BLENgth
				int value = driver.Configure.Connection.Scc.Laa.Fburst.Blength.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Fburst.Blength.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:BLENgth
				driver.Configure.Connection.Scc.Laa.Fburst.Blength.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Fburst.Blength.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:PBTR
				int value = driver.Configure.Connection.Scc.Laa.Fburst.Pbtr.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Fburst.Pbtr.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:PBTR
				driver.Configure.Connection.Scc.Laa.Fburst.Pbtr.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Laa.Fburst.Pbtr.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:SPFSubframe
				StartingPositionEnum value = driver.Configure.Connection.Scc.Laa.Fburst.SpfSubframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Fburst.SpfSubframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:SPFSubframe
				foreach (StartingPositionEnum x in new StartingPositionEnum[] { StartingPositionEnum.OFDM0, StartingPositionEnum.OFDM7 })
				{
					driver.Configure.Connection.Scc.Laa.Fburst.SpfSubframe.Set(x);
					driver.Configure.Connection.Scc.Laa.Fburst.SpfSubframe.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:OSLSubframe
				OccOfdmSymbolsEnum value = driver.Configure.Connection.Scc.Laa.Fburst.OslSubframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Laa.Fburst.OslSubframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:LAA:FBURst:OSLSubframe
				foreach (OccOfdmSymbolsEnum x in new OccOfdmSymbolsEnum[] { OccOfdmSymbolsEnum.SYM0, OccOfdmSymbolsEnum.SYM1, OccOfdmSymbolsEnum.SYM10, OccOfdmSymbolsEnum.SYM11, OccOfdmSymbolsEnum.SYM12, OccOfdmSymbolsEnum.SYM13, OccOfdmSymbolsEnum.SYM14, OccOfdmSymbolsEnum.SYM2, OccOfdmSymbolsEnum.SYM3, OccOfdmSymbolsEnum.SYM4, OccOfdmSymbolsEnum.SYM5, OccOfdmSymbolsEnum.SYM6, OccOfdmSymbolsEnum.SYM7, OccOfdmSymbolsEnum.SYM8, OccOfdmSymbolsEnum.SYM9 })
				{
					driver.Configure.Connection.Scc.Laa.Fburst.OslSubframe.Set(x);
					driver.Configure.Connection.Scc.Laa.Fburst.OslSubframe.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TIA<Nr>
				bool value = driver.Configure.Connection.Scc.Tia.Get(SecondaryCompCarrierRepCap.Default, TbsIndexAltRepCap.Default);
				value = driver.Configure.Connection.Scc.Tia.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TIA<Nr>
				driver.Configure.Connection.Scc.Tia.Set(false, SecondaryCompCarrierRepCap.Default, TbsIndexAltRepCap.Default);
				driver.Configure.Connection.Scc.Tia.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:PZERo:MAPPing
				PortsMappingEnum value = driver.Configure.Connection.Scc.Pzero.Mapping.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Pzero.Mapping.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:PZERo:MAPPing
				foreach (PortsMappingEnum x in new PortsMappingEnum[] { PortsMappingEnum.R1, PortsMappingEnum.R1R2 })
				{
					driver.Configure.Connection.Scc.Pzero.Mapping.Set(x);
					driver.Configure.Connection.Scc.Pzero.Mapping.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<8>:CHMatrix
				RsCmwLteSig_Configure_Connection_Scc_Tm_ChMatrix.ChMatrix_Data value = driver.Configure.Connection.Scc.Tm.ChMatrix.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.ChMatrix.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<8>:CHMatrix
				RsCmwLteSig_Configure_Connection_Scc_Tm_ChMatrix.ChMatrix_Data value = new RsCmwLteSig_Configure_Connection_Scc_Tm_ChMatrix.ChMatrix_Data();
				driver.Configure.Connection.Scc.Tm.ChMatrix.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.ChMatrix.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:CMATrix:EIGHt<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Eight.Get_Data value = driver.Configure.Connection.Scc.Tm.Cmatrix.Eight.Get(SecondaryCompCarrierRepCap.Default, MatrixEightLineRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Cmatrix.Eight.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:CMATrix:EIGHt<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Eight.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Eight.Set_Data();
				driver.Configure.Connection.Scc.Tm.Cmatrix.Eight.Set(value, SecondaryCompCarrierRepCap.Default, MatrixEightLineRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Cmatrix.Eight.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:FOUR<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Four.Get_Data value = driver.Configure.Connection.Scc.Tm.Cmatrix.Four.Get(SecondaryCompCarrierRepCap.Default, MatrixFourLineRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Cmatrix.Four.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:FOUR<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Four.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Four.Set_Data();
				driver.Configure.Connection.Scc.Tm.Cmatrix.Four.Set(value, SecondaryCompCarrierRepCap.Default, MatrixFourLineRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Cmatrix.Four.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:TWO<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Two.Get_Data value = driver.Configure.Connection.Scc.Tm.Cmatrix.Two.Get(SecondaryCompCarrierRepCap.Default, MatrixTwoLineRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Cmatrix.Two.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:TWO<line>
				driver.Configure.Connection.Scc.Tm.Cmatrix.Two.Set(1.0, 1, 1);
				driver.Configure.Connection.Scc.Tm.Cmatrix.Two.Set(1.0, 1, 1, SecondaryCompCarrierRepCap.Default, MatrixTwoLineRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:CMATrix:MIMO<Mimo>:MSELection
				MimoMatrixSelectionEnum value = driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Mselection.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Mselection.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:CMATrix:MIMO<Mimo>:MSELection
				foreach (MimoMatrixSelectionEnum x in new MimoMatrixSelectionEnum[] { MimoMatrixSelectionEnum.CM3Gpp, MimoMatrixSelectionEnum.HADamard, MimoMatrixSelectionEnum.IDENtity, MimoMatrixSelectionEnum.UDEFined })
				{
					driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Mselection.Set(x);
					driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Mselection.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:MIMO<Mimo>:LINE<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Mimo_Line.Get_Data value = driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Line.Get(SecondaryCompCarrierRepCap.Default, MatrixLineRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Line.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CMATrix:MIMO<Mimo>:LINE<line>
				RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Mimo_Line.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_Tm_Cmatrix_Mimo_Line.Set_Data();
				driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Line.Set(value, SecondaryCompCarrierRepCap.Default, MatrixLineRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Cmatrix.Mimo.Line.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:ZP:BITS
				string value = driver.Configure.Connection.Scc.Tm.Zp.Bits.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Zp.Bits.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:ZP:BITS
				driver.Configure.Connection.Scc.Tm.Zp.Bits.Set("r1", SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Zp.Bits.Set("r1");
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:ZP:CSIRs:SUBFrame
				int value = driver.Configure.Connection.Scc.Tm.Zp.Csirs.Subframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Zp.Csirs.Subframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:ZP:CSIRs:SUBFrame
				driver.Configure.Connection.Scc.Tm.Zp.Csirs.Subframe.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Zp.Csirs.Subframe.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:APORts
				AntennaPortsEnum value = driver.Configure.Connection.Scc.Tm.Csirs.Aports.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Csirs.Aports.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:APORts
				foreach (AntennaPortsEnum x in new AntennaPortsEnum[] { AntennaPortsEnum.NONE, AntennaPortsEnum.P15, AntennaPortsEnum.P1516, AntennaPortsEnum.P1518, AntennaPortsEnum.P1522 })
				{
					driver.Configure.Connection.Scc.Tm.Csirs.Aports.Set(x);
					driver.Configure.Connection.Scc.Tm.Csirs.Aports.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:SUBFrame
				int value = driver.Configure.Connection.Scc.Tm.Csirs.Subframe.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Csirs.Subframe.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:SUBFrame
				driver.Configure.Connection.Scc.Tm.Csirs.Subframe.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Csirs.Subframe.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:RESource
				int value = driver.Configure.Connection.Scc.Tm.Csirs.Resource.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Csirs.Resource.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:RESource
				driver.Configure.Connection.Scc.Tm.Csirs.Resource.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Csirs.Resource.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:POWer
				int value = driver.Configure.Connection.Scc.Tm.Csirs.Power.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Csirs.Power.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CSIRs:POWer
				driver.Configure.Connection.Scc.Tm.Csirs.Power.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Tm.Csirs.Power.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:PMATrix
				PrecodingMatrixModeEnum value = driver.Configure.Connection.Scc.Tm.Pmatrix.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Pmatrix.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TM<nr>:PMATrix
				foreach (PrecodingMatrixModeEnum x in new PrecodingMatrixModeEnum[] { PrecodingMatrixModeEnum.PMI0, PrecodingMatrixModeEnum.PMI1, PrecodingMatrixModeEnum.PMI10, PrecodingMatrixModeEnum.PMI11, PrecodingMatrixModeEnum.PMI12, PrecodingMatrixModeEnum.PMI13, PrecodingMatrixModeEnum.PMI14, PrecodingMatrixModeEnum.PMI15, PrecodingMatrixModeEnum.PMI2, PrecodingMatrixModeEnum.PMI3, PrecodingMatrixModeEnum.PMI4, PrecodingMatrixModeEnum.PMI5, PrecodingMatrixModeEnum.PMI6, PrecodingMatrixModeEnum.PMI7, PrecodingMatrixModeEnum.PMI8, PrecodingMatrixModeEnum.PMI9, PrecodingMatrixModeEnum.RANDom_pmi })
				{
					driver.Configure.Connection.Scc.Tm.Pmatrix.Set(x);
					driver.Configure.Connection.Scc.Tm.Pmatrix.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CODewords
				AntennasTxAenum value = driver.Configure.Connection.Scc.Tm.Codewords.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.Codewords.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:CODewords
				foreach (AntennasTxAenum x in new AntennasTxAenum[] { AntennasTxAenum.FOUR, AntennasTxAenum.ONE, AntennasTxAenum.TWO })
				{
					driver.Configure.Connection.Scc.Tm.Codewords.Set(x);
					driver.Configure.Connection.Scc.Tm.Codewords.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:NTXantennas
				AntennasTxBenum value = driver.Configure.Connection.Scc.Tm.NtxAntennas.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Tm.NtxAntennas.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:TM<nr>:NTXantennas
				foreach (AntennasTxBenum x in new AntennasTxBenum[] { AntennasTxBenum.EIGHt, AntennasTxBenum.FOUR, AntennasTxBenum.TWO })
				{
					driver.Configure.Connection.Scc.Tm.NtxAntennas.Set(x);
					driver.Configure.Connection.Scc.Tm.NtxAntennas.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:DLEQual
				bool value = driver.Configure.Connection.Scc.DlEqual.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.DlEqual.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:DLEQual
				driver.Configure.Connection.Scc.DlEqual.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.DlEqual.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TRANsmission
				TransmissionModeEnum value = driver.Configure.Connection.Scc.Transmission.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Transmission.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TRANsmission
				foreach (TransmissionModeEnum x in new TransmissionModeEnum[] { TransmissionModeEnum.TM1, TransmissionModeEnum.TM2, TransmissionModeEnum.TM3, TransmissionModeEnum.TM4, TransmissionModeEnum.TM6, TransmissionModeEnum.TM7, TransmissionModeEnum.TM8, TransmissionModeEnum.TM9 })
				{
					driver.Configure.Connection.Scc.Transmission.Set(x);
					driver.Configure.Connection.Scc.Transmission.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:DCIFormat
				DciFormatEnum value = driver.Configure.Connection.Scc.DciFormat.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.DciFormat.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:DCIFormat
				foreach (DciFormatEnum x in new DciFormatEnum[] { DciFormatEnum.D1, DciFormatEnum.D1A, DciFormatEnum.D1B, DciFormatEnum.D2, DciFormatEnum.D2A, DciFormatEnum.D2B, DciFormatEnum.D2C, DciFormatEnum.D61 })
				{
					driver.Configure.Connection.Scc.DciFormat.Set(x);
					driver.Configure.Connection.Scc.DciFormat.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:NENBantennas
				AntennasTxAenum value = driver.Configure.Connection.Scc.NenbAntennas.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.NenbAntennas.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:NENBantennas
				foreach (AntennasTxAenum x in new AntennasTxAenum[] { AntennasTxAenum.FOUR, AntennasTxAenum.ONE, AntennasTxAenum.TWO })
				{
					driver.Configure.Connection.Scc.NenbAntennas.Set(x);
					driver.Configure.Connection.Scc.NenbAntennas.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:NOLayers
				NoOfLayersEnum value = driver.Configure.Connection.Scc.NoLayers.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.NoLayers.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:NOLayers
				foreach (NoOfLayersEnum x in new NoOfLayersEnum[] { NoOfLayersEnum.L2, NoOfLayersEnum.L4 })
				{
					driver.Configure.Connection.Scc.NoLayers.Set(x);
					driver.Configure.Connection.Scc.NoLayers.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:MODE
				BeamformingModeEnum value = driver.Configure.Connection.Scc.Beamforming.Mode.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Beamforming.Mode.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:MODE
				foreach (BeamformingModeEnum x in new BeamformingModeEnum[] { BeamformingModeEnum.OFF, BeamformingModeEnum.ON, BeamformingModeEnum.PMAT, BeamformingModeEnum.TSBF })
				{
					driver.Configure.Connection.Scc.Beamforming.Mode.Set(x);
					driver.Configure.Connection.Scc.Beamforming.Mode.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:NOLayers
				BeamformingNoOfLayersEnum value = driver.Configure.Connection.Scc.Beamforming.NoLayers.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Beamforming.NoLayers.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:NOLayers
				foreach (BeamformingNoOfLayersEnum x in new BeamformingNoOfLayersEnum[] { BeamformingNoOfLayersEnum.L1, BeamformingNoOfLayersEnum.L1I, BeamformingNoOfLayersEnum.L2 })
				{
					driver.Configure.Connection.Scc.Beamforming.NoLayers.Set(x);
					driver.Configure.Connection.Scc.Beamforming.NoLayers.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:MATRix
				RsCmwLteSig_Configure_Connection_Scc_Beamforming_Matrix.Matrix_Data value = driver.Configure.Connection.Scc.Beamforming.Matrix.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Beamforming.Matrix.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:BEAMforming:MATRix
				RsCmwLteSig_Configure_Connection_Scc_Beamforming_Matrix.Matrix_Data value = new RsCmwLteSig_Configure_Connection_Scc_Beamforming_Matrix.Matrix_Data();
				driver.Configure.Connection.Scc.Beamforming.Matrix.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Beamforming.Matrix.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PMATrix
				PrecodingMatrixModeEnum value = driver.Configure.Connection.Scc.Pmatrix.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Pmatrix.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PMATrix
				foreach (PrecodingMatrixModeEnum x in new PrecodingMatrixModeEnum[] { PrecodingMatrixModeEnum.PMI0, PrecodingMatrixModeEnum.PMI1, PrecodingMatrixModeEnum.PMI10, PrecodingMatrixModeEnum.PMI11, PrecodingMatrixModeEnum.PMI12, PrecodingMatrixModeEnum.PMI13, PrecodingMatrixModeEnum.PMI14, PrecodingMatrixModeEnum.PMI15, PrecodingMatrixModeEnum.PMI2, PrecodingMatrixModeEnum.PMI3, PrecodingMatrixModeEnum.PMI4, PrecodingMatrixModeEnum.PMI5, PrecodingMatrixModeEnum.PMI6, PrecodingMatrixModeEnum.PMI7, PrecodingMatrixModeEnum.PMI8, PrecodingMatrixModeEnum.PMI9, PrecodingMatrixModeEnum.RANDom_pmi })
				{
					driver.Configure.Connection.Scc.Pmatrix.Set(x);
					driver.Configure.Connection.Scc.Pmatrix.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel
				RsCmwLteSig_Configure_Connection_Scc_SchModel.SchModel_Data value = driver.Configure.Connection.Scc.SchModel.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.SchModel.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel
				RsCmwLteSig_Configure_Connection_Scc_SchModel.SchModel_Data value = new RsCmwLteSig_Configure_Connection_Scc_SchModel.SchModel_Data();
				driver.Configure.Connection.Scc.SchModel.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.SchModel.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:ENABle
				bool value = driver.Configure.Connection.Scc.SchModel.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.SchModel.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:ENABle
				driver.Configure.Connection.Scc.SchModel.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.SchModel.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:ENABle:MIMO<Mimo>
				bool value = driver.Configure.Connection.Scc.SchModel.Enable.Mimo.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.SchModel.Enable.Mimo.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:ENABle:MIMO<Mimo>
				driver.Configure.Connection.Scc.SchModel.Enable.Mimo.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.SchModel.Enable.Mimo.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:MSELection:MIMO<Mimo>
				MimoMatrixSelectionEnum value = driver.Configure.Connection.Scc.SchModel.Mselection.Mimo.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.SchModel.Mselection.Mimo.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:MSELection:MIMO<Mimo>
				foreach (MimoMatrixSelectionEnum x in new MimoMatrixSelectionEnum[] { MimoMatrixSelectionEnum.CM3Gpp, MimoMatrixSelectionEnum.HADamard, MimoMatrixSelectionEnum.IDENtity, MimoMatrixSelectionEnum.UDEFined })
				{
					driver.Configure.Connection.Scc.SchModel.Mselection.Mimo.Set(x);
					driver.Configure.Connection.Scc.SchModel.Mselection.Mimo.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:MIMO{mimoCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_SchModel_Mimo.Mimo_Data value = driver.Configure.Connection.Scc.SchModel.Mimo.Get(SecondaryCompCarrierRepCap.Default, MimoRepCap.Default);
				value = driver.Configure.Connection.Scc.SchModel.Mimo.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:SCHModel:MIMO{mimoCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_SchModel_Mimo.Mimo_Data value = new RsCmwLteSig_Configure_Connection_Scc_SchModel_Mimo.Mimo_Data();
				driver.Configure.Connection.Scc.SchModel.Mimo.Set(value, SecondaryCompCarrierRepCap.Default, MimoRepCap.Default);
				driver.Configure.Connection.Scc.SchModel.Mimo.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:RMC:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Mcluster_Uplink.Uplink_Data value = driver.Configure.Connection.Scc.Rmc.Mcluster.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.Mcluster.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:RMC:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Mcluster_Uplink.Uplink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Rmc_Mcluster_Uplink.Uplink_Data();
				driver.Configure.Connection.Scc.Rmc.Mcluster.Uplink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Rmc.Mcluster.Uplink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Rmc.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Rmc_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Rmc.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.Rmc.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:RBPosition:DL{streamCmdVal}
				DownlinkRsrcBlockPositionEnum value = driver.Configure.Connection.Scc.Rmc.RbPosition.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.RbPosition.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:RBPosition:DL{streamCmdVal}
				foreach (DownlinkRsrcBlockPositionEnum x in new DownlinkRsrcBlockPositionEnum[] { DownlinkRsrcBlockPositionEnum.HIGH, DownlinkRsrcBlockPositionEnum.LOW, DownlinkRsrcBlockPositionEnum.P10, DownlinkRsrcBlockPositionEnum.P23, DownlinkRsrcBlockPositionEnum.P35, DownlinkRsrcBlockPositionEnum.P48, DownlinkRsrcBlockPositionEnum.P5 })
				{
					driver.Configure.Connection.Scc.Rmc.RbPosition.Downlink.Set(x);
					driver.Configure.Connection.Scc.Rmc.RbPosition.Downlink.Set(x, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:RBPosition:UL
				RbPositionEnum value = driver.Configure.Connection.Scc.Rmc.RbPosition.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.RbPosition.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:RBPosition:UL
				foreach (RbPositionEnum x in new RbPositionEnum[] { RbPositionEnum.FULL, RbPositionEnum.HIGH, RbPositionEnum.LOW, RbPositionEnum.MID, RbPositionEnum.P0, RbPositionEnum.P1, RbPositionEnum.P10, RbPositionEnum.P11, RbPositionEnum.P12, RbPositionEnum.P13, RbPositionEnum.P14, RbPositionEnum.P15, RbPositionEnum.P16, RbPositionEnum.P19, RbPositionEnum.P2, RbPositionEnum.P20, RbPositionEnum.P21, RbPositionEnum.P22, RbPositionEnum.P24, RbPositionEnum.P25, RbPositionEnum.P28, RbPositionEnum.P3, RbPositionEnum.P30, RbPositionEnum.P31, RbPositionEnum.P33, RbPositionEnum.P36, RbPositionEnum.P37, RbPositionEnum.P39, RbPositionEnum.P4, RbPositionEnum.P40, RbPositionEnum.P43, RbPositionEnum.P44, RbPositionEnum.P45, RbPositionEnum.P48, RbPositionEnum.P49, RbPositionEnum.P50, RbPositionEnum.P51, RbPositionEnum.P52, RbPositionEnum.P54, RbPositionEnum.P56, RbPositionEnum.P57, RbPositionEnum.P58, RbPositionEnum.P6, RbPositionEnum.P62, RbPositionEnum.P63, RbPositionEnum.P66, RbPositionEnum.P68, RbPositionEnum.P7, RbPositionEnum.P70, RbPositionEnum.P74, RbPositionEnum.P75, RbPositionEnum.P8, RbPositionEnum.P83, RbPositionEnum.P9, RbPositionEnum.P96, RbPositionEnum.P99 })
				{
					driver.Configure.Connection.Scc.Rmc.RbPosition.Uplink.Set(x);
					driver.Configure.Connection.Scc.Rmc.RbPosition.Uplink.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:VERSion:DL{streamCmdVal}
				int value = driver.Configure.Connection.Scc.Rmc.Version.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.Version.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:VERSion:DL{streamCmdVal}
				driver.Configure.Connection.Scc.Rmc.Version.Downlink.Set(1, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.Rmc.Version.Downlink.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:UL
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Uplink.Uplink_Data value = driver.Configure.Connection.Scc.Rmc.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Rmc.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:RMC:UL
				RsCmwLteSig_Configure_Connection_Scc_Rmc_Uplink.Uplink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Rmc_Uplink.Uplink_Data();
				driver.Configure.Connection.Scc.Rmc.Uplink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Rmc.Uplink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:UDCHannels:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Uplink.Uplink_Data value = driver.Configure.Connection.Scc.UdChannels.Mcluster.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Mcluster.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:UDCHannels:MCLuster:UL
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Uplink.Uplink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Uplink.Uplink_Data();
				driver.Configure.Connection.Scc.UdChannels.Mcluster.Uplink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Mcluster.Uplink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:FSUBframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:FSUBframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:FSUBframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:FSUBframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_FullSubframes_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.FullSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PIPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PIPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PIPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PIPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PipSubframes_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PipSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PEPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PEPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PEPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:PEPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Fburst_PepSubframes_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Fburst.PepSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:FSUBframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:FSUBframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:FSUBframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:FSUBframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_FullSubframes_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.FullSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PIPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PIPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PIPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PIPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PipSubframes_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PipSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PEPSubframes:MCLuster:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PepSubframes_Mcluster_Downlink.Get_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Mcluster.Downlink.Get(SymbolsEnum.S0, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Mcluster.Downlink.Get(SymbolsEnum.S0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PEPSubframes:MCLuster:DL{streamCmdVal}
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Mcluster.Downlink.Set(SymbolsEnum.S0, 1.0, ModulationEnum.Q1024, 1);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Mcluster.Downlink.Set(SymbolsEnum.S0, 1.0, ModulationEnum.Q1024, 1, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PEPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PepSubframes_Downlink.Get_Data value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Downlink.Get(SymbolsEnum.S0, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Downlink.Get(SymbolsEnum.S0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:PEPSubframes:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PepSubframes_Downlink.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Laa_Rburst_PepSubframes_Downlink.Set_Data();
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Laa.Rburst.PepSubframes.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.UdChannels.Downlink.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.UdChannels.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:UL
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Uplink.Uplink_Data value = driver.Configure.Connection.Scc.UdChannels.Uplink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.UdChannels.Uplink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:UL
				RsCmwLteSig_Configure_Connection_Scc_UdChannels_Uplink.Uplink_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdChannels_Uplink.Uplink_Data();
				driver.Configure.Connection.Scc.UdChannels.Uplink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.UdChannels.Uplink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:UDTTibased:QAM<256>
				bool value = driver.Configure.Connection.Scc.UdttiBased.Qam.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.UdttiBased.Qam.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<carrier>:UDTTibased:QAM<256>
				driver.Configure.Connection.Scc.UdttiBased.Qam.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.UdttiBased.Qam.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink.Get_Data value = driver.Configure.Connection.Scc.UdttiBased.Downlink.Get(1.0, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdttiBased.Downlink.Get(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink.Set_Data();
				driver.Configure.Connection.Scc.UdttiBased.Downlink.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdttiBased.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink_All.All_Data value = driver.Configure.Connection.Scc.UdttiBased.Downlink.All.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.UdttiBased.Downlink.All.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink_All.All_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Downlink_All.All_Data();
				driver.Configure.Connection.Scc.UdttiBased.Downlink.All.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.UdttiBased.Downlink.All.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:UL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink.Get_Data value = driver.Configure.Connection.Scc.UdttiBased.Uplink.Get(1.0, SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.UdttiBased.Uplink.Get(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:UL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink.Set_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink.Set_Data();
				driver.Configure.Connection.Scc.UdttiBased.Uplink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.UdttiBased.Uplink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:UL:ALL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink_All.All_Data value = driver.Configure.Connection.Scc.UdttiBased.Uplink.All.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.UdttiBased.Uplink.All.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:UL:ALL
				RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink_All.All_Data value = new RsCmwLteSig_Configure_Connection_Scc_UdttiBased_Uplink_All.All_Data();
				driver.Configure.Connection.Scc.UdttiBased.Uplink.All.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.UdttiBased.Uplink.All.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:QAM<ModOrder>:DL
				bool value = driver.Configure.Connection.Scc.Qam.Downlink.Get(SecondaryCompCarrierRepCap.Default, QAMmodulationOrderBRepCap.Default);
				value = driver.Configure.Connection.Scc.Qam.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:QAM<ModOrder>:DL
				driver.Configure.Connection.Scc.Qam.Downlink.Set(false, SecondaryCompCarrierRepCap.Default, QAMmodulationOrderBRepCap.Default);
				driver.Configure.Connection.Scc.Qam.Downlink.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCTTibased:DL{streamCmdVal}
				RsCmwLteSig_Configure_Connection_Scc_FcttiBased_Downlink.Get_Data value = driver.Configure.Connection.Scc.FcttiBased.Downlink.Get(1.0, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.FcttiBased.Downlink.Get(1.0);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCTTibased:DL{streamCmdVal}
				driver.Configure.Connection.Scc.FcttiBased.Downlink.Set(1.0, 1, 1, 1);
				driver.Configure.Connection.Scc.FcttiBased.Downlink.Set(1.0, 1, 1, 1, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Scc_FcttiBased_Downlink_All.All_Data value = driver.Configure.Connection.Scc.FcttiBased.Downlink.All.Get(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Configure.Connection.Scc.FcttiBased.Downlink.All.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCTTibased:DL{streamCmdVal}:ALL
				RsCmwLteSig_Configure_Connection_Scc_FcttiBased_Downlink_All.All_Data value = new RsCmwLteSig_Configure_Connection_Scc_FcttiBased_Downlink_All.All_Data();
				driver.Configure.Connection.Scc.FcttiBased.Downlink.All.Set(value, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				driver.Configure.Connection.Scc.FcttiBased.Downlink.All.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fwbcqi.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fwbcqi.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL
				RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL
				RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fwbcqi_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:STTI
				List<bool> value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.Stti.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.Stti.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:STTI
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.Stti.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.Stti.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:UDEFined
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:CSIRs:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:CSIRs:UDEFined
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:SSUBframe:UDEFined
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpmi_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fpmi.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpmi.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpmi_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fpmi_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fpmi.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpmi.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpmi_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fpmi.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpmi.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpmi_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fpmi_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fpmi.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpmi.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:DL:STTI
				List<bool> value = driver.Configure.Connection.Scc.Fpmi.Downlink.Stti.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpmi.Downlink.Stti.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPMI:DL:STTI
				driver.Configure.Connection.Scc.Fpmi.Downlink.Stti.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpmi.Downlink.Stti.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcri_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fcri.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcri.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcri_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fcri_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fcri.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcri.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcri_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fcri.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcri.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcri_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fcri_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fcri.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcri.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:STTI
				List<bool> value = driver.Configure.Connection.Scc.Fcri.Downlink.Stti.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcri.Downlink.Stti.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:STTI
				driver.Configure.Connection.Scc.Fcri.Downlink.Stti.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcri.Downlink.Stti.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:UDEFined
				driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:SSUBframe:UDEFined
				driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcpri_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fcpri.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcpri_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fcpri_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fcpri.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcpri_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fcpri.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL
				RsCmwLteSig_Configure_Connection_Scc_Fcpri_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fcpri_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fcpri.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:STTI
				List<bool> value = driver.Configure.Connection.Scc.Fcpri.Downlink.Stti.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Downlink.Stti.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:STTI
				driver.Configure.Connection.Scc.Fcpri.Downlink.Stti.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Downlink.Stti.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:UDEFined
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:CSIRs:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:CSIRs:UDEFined
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:SSUBframe:UDEFined
				List<int> value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:SSUBframe:UDEFined
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.UserDefined.Set(new List<int> { 1, 2, 3 });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpri_Mcluster_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fpri.Mcluster.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpri.Mcluster.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:MCLuster:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpri_Mcluster_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fpri_Mcluster_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fpri.Mcluster.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpri.Mcluster.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpri_Downlink.Downlink_Data value = driver.Configure.Connection.Scc.Fpri.Downlink.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpri.Downlink.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:DL
				RsCmwLteSig_Configure_Connection_Scc_Fpri_Downlink.Downlink_Data value = new RsCmwLteSig_Configure_Connection_Scc_Fpri_Downlink.Downlink_Data();
				driver.Configure.Connection.Scc.Fpri.Downlink.Set(value, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpri.Downlink.Set(value);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:DL:STTI
				List<bool> value = driver.Configure.Connection.Scc.Fpri.Downlink.Stti.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Fpri.Downlink.Stti.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FPRI:DL:STTI
				driver.Configure.Connection.Scc.Fpri.Downlink.Stti.Set(new List<bool> { true, false, true }, SecondaryCompCarrierRepCap.Default);
				driver.Configure.Connection.Scc.Fpri.Downlink.Stti.Set(new List<bool> { true, false, true });
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:SYMBol
				PdcchSymbolsCountEnum value = driver.Configure.Connection.Scc.Pdcch.Symbol.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Pdcch.Symbol.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:SYMBol
				foreach (PdcchSymbolsCountEnum x in new PdcchSymbolsCountEnum[] { PdcchSymbolsCountEnum.AUTO, PdcchSymbolsCountEnum.P1, PdcchSymbolsCountEnum.P2, PdcchSymbolsCountEnum.P3, PdcchSymbolsCountEnum.P4 })
				{
					driver.Configure.Connection.Scc.Pdcch.Symbol.Set(x);
					driver.Configure.Connection.Scc.Pdcch.Symbol.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:ALEVel
				AggregationlevelEnum value = driver.Configure.Connection.Scc.Pdcch.Alevel.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.Connection.Scc.Pdcch.Alevel.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:ALEVel
				foreach (AggregationlevelEnum x in new AggregationlevelEnum[] { AggregationlevelEnum.AUTO, AggregationlevelEnum.D1U1, AggregationlevelEnum.D4U2, AggregationlevelEnum.D4U4, AggregationlevelEnum.D8U4, AggregationlevelEnum.D8U8 })
				{
					driver.Configure.Connection.Scc.Pdcch.Alevel.Set(x);
					driver.Configure.Connection.Scc.Pdcch.Alevel.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UEPosition:RESet
				driver.Configure.Connection.UePosition.Reset();
				driver.Configure.Connection.UePosition.ResetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UECategory:MANual
				int value = driver.Configure.Connection.UeCategory.Manual;
				driver.Configure.Connection.UeCategory.Manual = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UECategory:CZALlowed
				bool value = driver.Configure.Connection.UeCategory.CzAllowed;
				driver.Configure.Connection.UeCategory.CzAllowed = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UECategory:REPorted
				RsCmwLteSig_Configure_Connection_UeCategory_Reported.Get_Data value = driver.Configure.Connection.UeCategory.Reported.Get();				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:UECategory:REPorted
				driver.Configure.Connection.UeCategory.Reported.Set(false);				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:EDAU:ENABle
				bool value = driver.Configure.Connection.Edau.Enable;
				driver.Configure.Connection.Edau.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:EDAU:NSEGment
				foreach (NetworkSegmentEnum x in new NetworkSegmentEnum[] { NetworkSegmentEnum.A, NetworkSegmentEnum.B, NetworkSegmentEnum.C })
				{
					driver.Configure.Connection.Edau.Nsegment = x;
					NetworkSegmentEnum value = driver.Configure.Connection.Edau.Nsegment;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:EDAU:NID
				int value = driver.Configure.Connection.Edau.Nid;
				driver.Configure.Connection.Edau.Nid = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CSFB:DESTination
				foreach (CsbfDestinationEnum x in new CsbfDestinationEnum[] { CsbfDestinationEnum.CDMA, CsbfDestinationEnum.GSM, CsbfDestinationEnum.NONE, CsbfDestinationEnum.TDSCdma, CsbfDestinationEnum.WCDMa })
				{
					driver.Configure.Connection.Csfb.Destination = x;
					CsbfDestinationEnum value = driver.Configure.Connection.Csfb.Destination;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CSFB:GSM
				RsCmwLteSig_Configure_Connection_Csfb.Gsm_Data value = driver.Configure.Connection.Csfb.Gsm;
				driver.Configure.Connection.Csfb.Gsm = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CSFB:WCDMa
				RsCmwLteSig_Configure_Connection_Csfb.Wcdma_Data value = driver.Configure.Connection.Csfb.Wcdma;
				driver.Configure.Connection.Csfb.Wcdma = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:CSFB:TDSCdma
				RsCmwLteSig_Configure_Connection_Csfb.Tdscdma_Data value = driver.Configure.Connection.Csfb.Tdscdma;
				driver.Configure.Connection.Csfb.Tdscdma = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:UL:MAXTx
				int value = driver.Configure.Connection.Harq.Uplink.Maxtx;
				driver.Configure.Connection.Harq.Uplink.Maxtx = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:UL:ENABle
				bool value = driver.Configure.Connection.Harq.Uplink.Enable;
				driver.Configure.Connection.Harq.Uplink.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:UL:NHT
				int value = driver.Configure.Connection.Harq.Uplink.Nht;
				driver.Configure.Connection.Harq.Uplink.Nht = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:UL:DPHich
				foreach (UlHarqModeEnum x in new UlHarqModeEnum[] { UlHarqModeEnum.D0ONly, UlHarqModeEnum.D0PHich, UlHarqModeEnum.PHIChonly, UlHarqModeEnum.PNACk, UlHarqModeEnum.PND0 })
				{
					driver.Configure.Connection.Harq.Uplink.Dphich = x;
					UlHarqModeEnum value = driver.Configure.Connection.Harq.Uplink.Dphich;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:DL:ENABle
				bool value = driver.Configure.Connection.Harq.Downlink.Enable;
				driver.Configure.Connection.Harq.Downlink.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:DL:NHT
				int value = driver.Configure.Connection.Harq.Downlink.Nht;
				driver.Configure.Connection.Harq.Downlink.Nht = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:DL:RVCSequence
				foreach (RedundancyVerSequenceEnum x in new RedundancyVerSequenceEnum[] { RedundancyVerSequenceEnum.TS1, RedundancyVerSequenceEnum.TS4, RedundancyVerSequenceEnum.UDEFined })
				{
					driver.Configure.Connection.Harq.Downlink.RvcSequence = x;
					RedundancyVerSequenceEnum value = driver.Configure.Connection.Harq.Downlink.RvcSequence;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:DL:UDSequence:LENGth
				int value = driver.Configure.Connection.Harq.Downlink.UdSequence.Length;
				driver.Configure.Connection.Harq.Downlink.UdSequence.Length = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CONNection:HARQ:DL:UDSequence
				RsCmwLteSig_Configure_Connection_Harq_Downlink_UdSequence.Value_Data value = driver.Configure.Connection.Harq.Downlink.UdSequence.Value;
				driver.Configure.Connection.Harq.Downlink.UdSequence.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:ENABle
				foreach (EnableCqiReportEnum x in new EnableCqiReportEnum[] { EnableCqiReportEnum.OFF, EnableCqiReportEnum.PERiodic })
				{
					driver.Configure.CqiReporting.Enable = x;
					EnableCqiReportEnum value = driver.Configure.CqiReporting.Enable;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:CSIRmode
				foreach (CsiReportingModeEnum x in new CsiReportingModeEnum[] { CsiReportingModeEnum.S1, CsiReportingModeEnum.S2 })
				{
					driver.Configure.CqiReporting.CsirMode = x;
					CsiReportingModeEnum value = driver.Configure.CqiReporting.CsirMode;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SANCqi
				bool value = driver.Configure.CqiReporting.Sancqi;
				driver.Configure.CqiReporting.Sancqi = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex:LAA
				int value = driver.Configure.CqiReporting.Scc.Cindex.Laa.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.CqiReporting.Scc.Cindex.Laa.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex:LAA
				driver.Configure.CqiReporting.Scc.Cindex.Laa.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.CqiReporting.Scc.Cindex.Laa.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex[:FDD]
				int value = driver.Configure.CqiReporting.Scc.Cindex.Fdd.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.CqiReporting.Scc.Cindex.Fdd.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex[:FDD]
				driver.Configure.CqiReporting.Scc.Cindex.Fdd.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.CqiReporting.Scc.Cindex.Fdd.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex:TDD
				int value = driver.Configure.CqiReporting.Scc.Cindex.Tdd.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.CqiReporting.Scc.Cindex.Tdd.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:CINDex:TDD
				driver.Configure.CqiReporting.Scc.Cindex.Tdd.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.CqiReporting.Scc.Cindex.Tdd.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting:PRIReporting:ENABle
				bool value = driver.Configure.CqiReporting.PriReporting.Enable;
				driver.Configure.CqiReporting.PriReporting.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting[:PCC]:CINDex[:FDD]
				int value = driver.Configure.CqiReporting.Pcc.Cindex.Fdd;
				driver.Configure.CqiReporting.Pcc.Cindex.Fdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CQIReporting[:PCC]:CINDex:TDD
				int value = driver.Configure.CqiReporting.Pcc.Cindex.Tdd;
				driver.Configure.CqiReporting.Pcc.Cindex.Tdd = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:ENABle
				bool value = driver.Configure.UeReport.Enable;
				driver.Configure.UeReport.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:WMQuantity
				foreach (WmQuantityEnum x in new WmQuantityEnum[] { WmQuantityEnum.ECNO, WmQuantityEnum.RSCP })
				{
					driver.Configure.UeReport.WmQuantity = x;
					WmQuantityEnum value = driver.Configure.UeReport.WmQuantity;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:MGENable
				bool value = driver.Configure.UeReport.MgEnable;
				driver.Configure.UeReport.MgEnable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:MGPeriod
				foreach (TransGapEnum x in new TransGapEnum[] { TransGapEnum.G040, TransGapEnum.G080 })
				{
					driver.Configure.UeReport.MgPeriod = x;
					TransGapEnum value = driver.Configure.UeReport.MgPeriod;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:RINTerval
				foreach (ReportIntervalEnum x in new ReportIntervalEnum[] { ReportIntervalEnum.I1024, ReportIntervalEnum.I10240, ReportIntervalEnum.I120, ReportIntervalEnum.I2048, ReportIntervalEnum.I240, ReportIntervalEnum.I480, ReportIntervalEnum.I5120, ReportIntervalEnum.I640 })
				{
					driver.Configure.UeReport.Rinterval = x;
					ReportIntervalEnum value = driver.Configure.UeReport.Rinterval;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:MCSCell
				foreach (MeasCellCycleEnum x in new MeasCellCycleEnum[] { MeasCellCycleEnum.OFF, MeasCellCycleEnum.SF1024, MeasCellCycleEnum.SF1280, MeasCellCycleEnum.SF160, MeasCellCycleEnum.SF256, MeasCellCycleEnum.SF320, MeasCellCycleEnum.SF512, MeasCellCycleEnum.SF640 })
				{
					driver.Configure.UeReport.McsCell = x;
					MeasCellCycleEnum value = driver.Configure.UeReport.McsCell;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:AINTerrupt
				bool value = driver.Configure.UeReport.Ainterrupt;
				driver.Configure.UeReport.Ainterrupt = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:FCOefficient:RSRP
				foreach (FilterRsrpqCoefficientEnum x in new FilterRsrpqCoefficientEnum[] { FilterRsrpqCoefficientEnum.FC0, FilterRsrpqCoefficientEnum.FC4 })
				{
					driver.Configure.UeReport.Fcoefficient.Rsrp = x;
					FilterRsrpqCoefficientEnum value = driver.Configure.UeReport.Fcoefficient.Rsrp;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:FCOefficient:RSRQ
				foreach (FilterRsrpqCoefficientEnum x in new FilterRsrpqCoefficientEnum[] { FilterRsrpqCoefficientEnum.FC0, FilterRsrpqCoefficientEnum.FC4 })
				{
					driver.Configure.UeReport.Fcoefficient.Rsrq = x;
					FilterRsrpqCoefficientEnum value = driver.Configure.UeReport.Fcoefficient.Rsrq;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:DMTC:PERiod
				LaaPeriodEnum value = driver.Configure.UeReport.Scc.Dmtc.Period.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Dmtc.Period.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:DMTC:PERiod
				foreach (LaaPeriodEnum x in new LaaPeriodEnum[] { LaaPeriodEnum.MS160, LaaPeriodEnum.MS40, LaaPeriodEnum.MS80 })
				{
					driver.Configure.UeReport.Scc.Dmtc.Period.Set(x);
					driver.Configure.UeReport.Scc.Dmtc.Period.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:DMTC:POFFset
				int value = driver.Configure.UeReport.Scc.Dmtc.Poffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Dmtc.Poffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:DMTC:POFFset
				driver.Configure.UeReport.Scc.Dmtc.Poffset.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.UeReport.Scc.Dmtc.Poffset.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:ENABle
				bool value = driver.Configure.UeReport.Scc.Rssi.Enable.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Rssi.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:ENABle
				driver.Configure.UeReport.Scc.Rssi.Enable.Set(false, SecondaryCompCarrierRepCap.Default);
				driver.Configure.UeReport.Scc.Rssi.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:RMTC:PERiod
				LaaUePeriodEnum value = driver.Configure.UeReport.Scc.Rssi.Rmtc.Period.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Rssi.Rmtc.Period.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:RMTC:PERiod
				foreach (LaaUePeriodEnum x in new LaaUePeriodEnum[] { LaaUePeriodEnum.MS160, LaaUePeriodEnum.MS320, LaaUePeriodEnum.MS40, LaaUePeriodEnum.MS640, LaaUePeriodEnum.MS80 })
				{
					driver.Configure.UeReport.Scc.Rssi.Rmtc.Period.Set(x);
					driver.Configure.UeReport.Scc.Rssi.Rmtc.Period.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:RMTC:SOFFset
				int value = driver.Configure.UeReport.Scc.Rssi.Rmtc.Soffset.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Rssi.Rmtc.Soffset.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:RMTC:SOFFset
				driver.Configure.UeReport.Scc.Rssi.Rmtc.Soffset.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.UeReport.Scc.Rssi.Rmtc.Soffset.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:COTHreshold
				int value = driver.Configure.UeReport.Scc.Rssi.CoThreshold.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Rssi.CoThreshold.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:COTHreshold
				driver.Configure.UeReport.Scc.Rssi.CoThreshold.Set(1, SecondaryCompCarrierRepCap.Default);
				driver.Configure.UeReport.Scc.Rssi.CoThreshold.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:MDURation
				SymbolsDurationEnum value = driver.Configure.UeReport.Scc.Rssi.Mduration.Get(SecondaryCompCarrierRepCap.Default);
				value = driver.Configure.UeReport.Scc.Rssi.Mduration.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSSI:MDURation
				foreach (SymbolsDurationEnum x in new SymbolsDurationEnum[] { SymbolsDurationEnum.S1, SymbolsDurationEnum.S14, SymbolsDurationEnum.S28, SymbolsDurationEnum.S42, SymbolsDurationEnum.S70 })
				{
					driver.Configure.UeReport.Scc.Rssi.Mduration.Set(x);
					driver.Configure.UeReport.Scc.Rssi.Mduration.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:RUTRa
				bool value = driver.Configure.UeCapability.Rutra;
				driver.Configure.UeCapability.Rutra = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:RGCS
				bool value = driver.Configure.UeCapability.Rgcs;
				driver.Configure.UeCapability.Rgcs = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:RGPS
				bool value = driver.Configure.UeCapability.Rgps;
				driver.Configure.UeCapability.Rgps = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:RRFormat
				bool value = driver.Configure.UeCapability.RrFormat;
				driver.Configure.UeCapability.RrFormat = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:SFC
				bool value = driver.Configure.UeCapability.Sfc;
				driver.Configure.UeCapability.Sfc = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:UECapability:RFBands:ALL
				RsCmwLteSig_Configure_UeCapability_RfBands.All_Data value = driver.Configure.UeCapability.RfBands.All;
				driver.Configure.UeCapability.RfBands.All = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:UDHeader
				double value = driver.Configure.Sms.Outgoing.Udheader;
				driver.Configure.Sms.Outgoing.Udheader = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:MESHandling
				foreach (MessageHandlingEnum x in new MessageHandlingEnum[] { MessageHandlingEnum.FILE, MessageHandlingEnum.INTernal })
				{
					driver.Configure.Sms.Outgoing.MesHandling = x;
					MessageHandlingEnum value = driver.Configure.Sms.Outgoing.MesHandling;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:INTernal
				string value = driver.Configure.Sms.Outgoing.Internal;
				driver.Configure.Sms.Outgoing.Internal = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:BINary
				double value = driver.Configure.Sms.Outgoing.Binary;
				driver.Configure.Sms.Outgoing.Binary = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:PIDentifier
				double value = driver.Configure.Sms.Outgoing.Pidentifier;
				driver.Configure.Sms.Outgoing.Pidentifier = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:DCODing
				foreach (SmsDataCodingEnum x in new SmsDataCodingEnum[] { SmsDataCodingEnum.BIT7, SmsDataCodingEnum.BIT8 })
				{
					driver.Configure.Sms.Outgoing.Dcoding = x;
					SmsDataCodingEnum value = driver.Configure.Sms.Outgoing.Dcoding;
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:CGRoup
				foreach (SmsCodingGroupEnum x in new SmsCodingGroupEnum[] { SmsCodingGroupEnum.DCMClass, SmsCodingGroupEnum.GDCoding })
				{
					driver.Configure.Sms.Outgoing.Cgroup = x;
					SmsCodingGroupEnum value = driver.Configure.Sms.Outgoing.Cgroup;
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:MCLass
				foreach (MessageClassEnum x in new MessageClassEnum[] { MessageClassEnum.CL0, MessageClassEnum.CL1, MessageClassEnum.CL2, MessageClassEnum.CL3, MessageClassEnum.NONE })
				{
					driver.Configure.Sms.Outgoing.Mclass = x;
					MessageClassEnum value = driver.Configure.Sms.Outgoing.Mclass;
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:OSADdress
				string value = driver.Configure.Sms.Outgoing.OsAddress;
				driver.Configure.Sms.Outgoing.OsAddress = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:OADDress
				string value = driver.Configure.Sms.Outgoing.Oaddress;
				driver.Configure.Sms.Outgoing.Oaddress = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:LHANdling
				foreach (LongSmsHandlingEnum x in new LongSmsHandlingEnum[] { LongSmsHandlingEnum.MSMS, LongSmsHandlingEnum.TRUNcate })
				{
					driver.Configure.Sms.Outgoing.Lhandling = x;
					LongSmsHandlingEnum value = driver.Configure.Sms.Outgoing.Lhandling;
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:TSOurce
				foreach (SourceTimeEnum x in new SourceTimeEnum[] { SourceTimeEnum.CMWTime, SourceTimeEnum.DATE })
				{
					driver.Configure.Sms.Outgoing.SctStamp.Tsource = x;
					SourceTimeEnum value = driver.Configure.Sms.Outgoing.SctStamp.Tsource;
				}
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:DATE
				RsCmwLteSig_Configure_Sms_Outgoing_SctStamp.Date_Data value = driver.Configure.Sms.Outgoing.SctStamp.Date;
				driver.Configure.Sms.Outgoing.SctStamp.Date = value;
			}
			{	// CONFigure:LTE:SIGNaling<Instance>:SMS:OUTGoing:SCTStamp:TIME
				RsCmwLteSig_Configure_Sms_Outgoing_SctStamp.Time_Data value = driver.Configure.Sms.Outgoing.SctStamp.Time;
				driver.Configure.Sms.Outgoing.SctStamp.Time = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:FILE:INFO
				RsCmwLteSig_Configure_Sms_Outgoing_File.Info_Data value = driver.Configure.Sms.Outgoing.File.Info;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:OUTGoing:FILE
				string value = driver.Configure.Sms.Outgoing.File.Value;
				driver.Configure.Sms.Outgoing.File.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:INComing:FILE:INFO
				RsCmwLteSig_Configure_Sms_Incoming_File.Info_Data value = driver.Configure.Sms.Incoming.File.Info;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SMS:INComing:FILE
				string value = driver.Configure.Sms.Incoming.File.Value;
				driver.Configure.Sms.Incoming.File.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:ENABle
				bool value = driver.Configure.Cbs.Message.Enable;
				driver.Configure.Cbs.Message.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:ID
				int value = driver.Configure.Cbs.Message.Id;
				driver.Configure.Cbs.Message.Id = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:IDTYpe
				foreach (MessageTypeEnum x in new MessageTypeEnum[] { MessageTypeEnum.AAMBer, MessageTypeEnum.AEXTreme, MessageTypeEnum.APResidentia, MessageTypeEnum.ASEVere, MessageTypeEnum.EARThquake, MessageTypeEnum.ETWarning, MessageTypeEnum.ETWTest, MessageTypeEnum.TSUNami, MessageTypeEnum.UDCMas, MessageTypeEnum.UDEFined, MessageTypeEnum.UDETws })
				{
					driver.Configure.Cbs.Message.Idtype = x;
					MessageTypeEnum value = driver.Configure.Cbs.Message.Idtype;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:SERial
				RsCmwLteSig_Configure_Cbs_Message.Serial_Data value = driver.Configure.Cbs.Message.Serial;
				driver.Configure.Cbs.Message.Serial = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:CGRoup
				int value = driver.Configure.Cbs.Message.Cgroup;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:LANGuage
				RsCmwLteSig_Configure_Cbs_Message.Language_Data value = driver.Configure.Cbs.Message.Language;
				driver.Configure.Cbs.Message.Language = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:CATegory
				foreach (PriorityEnum x in new PriorityEnum[] { PriorityEnum.BACKground, PriorityEnum.HIGH, PriorityEnum.NORMal })
				{
					driver.Configure.Cbs.Message.Category = x;
					PriorityEnum value = driver.Configure.Cbs.Message.Category;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:SOURce
				foreach (MessageHandlingEnum x in new MessageHandlingEnum[] { MessageHandlingEnum.FILE, MessageHandlingEnum.INTernal })
				{
					driver.Configure.Cbs.Message.Source = x;
					MessageHandlingEnum value = driver.Configure.Cbs.Message.Source;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:DATA
				string value = driver.Configure.Cbs.Message.Data;
				driver.Configure.Cbs.Message.Data = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:PERiod
				double value = driver.Configure.Cbs.Message.Period;
				driver.Configure.Cbs.Message.Period = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:DCSCheme
				RsCmwLteSig_Configure_Cbs_Message_DcScheme.Get_Data value = driver.Configure.Cbs.Message.DcScheme.Get();				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:DCSCheme
				driver.Configure.Cbs.Message.DcScheme.Set(1, 1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:FILE:INFO
				RsCmwLteSig_Configure_Cbs_Message_File.Info_Data value = driver.Configure.Cbs.Message.File.Info;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:FILE
				string value = driver.Configure.Cbs.Message.File.Value;
				driver.Configure.Cbs.Message.File.Value = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:ETWS:ALERt
				bool value = driver.Configure.Cbs.Message.Etws.Alert;
				driver.Configure.Cbs.Message.Etws.Alert = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:CBS:MESSage:ETWS:POPup
				bool value = driver.Configure.Cbs.Message.Etws.Popup;
				driver.Configure.Cbs.Message.Etws.Popup = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EELog:ENABle
				bool value = driver.Configure.EeLog.Enable;
				driver.Configure.EeLog.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:TOUT
				double value = driver.Configure.ExtendedBler.Timeout;
				driver.Configure.ExtendedBler.Timeout = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:SFRames
				int value = driver.Configure.ExtendedBler.Sframes;
				driver.Configure.ExtendedBler.Sframes = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:ERCalc
				foreach (BlerAlgorithmEnum x in new BlerAlgorithmEnum[] { BlerAlgorithmEnum.ERC1, BlerAlgorithmEnum.ERC2, BlerAlgorithmEnum.ERC3, BlerAlgorithmEnum.ERC4 })
				{
					driver.Configure.ExtendedBler.ErCalc = x;
					BlerAlgorithmEnum value = driver.Configure.ExtendedBler.ErCalc;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.ExtendedBler.Repetition = x;
					RepeatEnum value = driver.Configure.ExtendedBler.Repetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:SCONdition
				foreach (EblerStopConditionEnum x in new EblerStopConditionEnum[] { EblerStopConditionEnum.CLEVel, EblerStopConditionEnum.NONE })
				{
					driver.Configure.ExtendedBler.Scondition = x;
					EblerStopConditionEnum value = driver.Configure.ExtendedBler.Scondition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:CONFidence:OASCondition
				foreach (BlerStopConditionEnum x in new BlerStopConditionEnum[] { BlerStopConditionEnum.AC1St, BlerStopConditionEnum.ACWait, BlerStopConditionEnum.PCC, BlerStopConditionEnum.SCC1, BlerStopConditionEnum.SCC2 })
				{
					driver.Configure.ExtendedBler.Confidence.OasCondition = x;
					BlerStopConditionEnum value = driver.Configure.ExtendedBler.Confidence.OasCondition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:CONFidence:MTTime
				int value = driver.Configure.ExtendedBler.Confidence.MtTime;
				driver.Configure.ExtendedBler.Confidence.MtTime = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:EBLer:CONFidence:LERate
				foreach (LimitErrRationEnum x in new LimitErrRationEnum[] { LimitErrRationEnum.P001, LimitErrRationEnum.P010, LimitErrRationEnum.P050 })
				{
					driver.Configure.ExtendedBler.Confidence.Lerate = x;
					LimitErrRationEnum value = driver.Configure.ExtendedBler.Confidence.Lerate;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:THRoughput:TOUT
				double value = driver.Configure.Throughput.Timeout;
				driver.Configure.Throughput.Timeout = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:THRoughput:UPDate
				double value = driver.Configure.Throughput.Update;
				driver.Configure.Throughput.Update = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:THRoughput:WINDow
				double value = driver.Configure.Throughput.Window;
				driver.Configure.Throughput.Window = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:THRoughput:REPetition
				foreach (RepeatEnum x in new RepeatEnum[] { RepeatEnum.CONTinuous, RepeatEnum.SINGleshot })
				{
					driver.Configure.Throughput.Repetition = x;
					RepeatEnum value = driver.Configure.Throughput.Repetition;
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:MMONitor:ENABle
				bool value = driver.Configure.Mmonitor.Enable;
				driver.Configure.Mmonitor.Enable = value;
			}
			{	// CONFigure:LTE:SIGNaling<instance>:MMONitor:IPADdress
				RsCmwLteSig_Configure_Mmonitor_IpAddress.Get_Data value = driver.Configure.Mmonitor.IpAddress.Get();				
			}
			{	// CONFigure:LTE:SIGNaling<instance>:MMONitor:IPADdress
				foreach (IPADdressEnum x in new IPADdressEnum[] { IPADdressEnum.IP1, IPADdressEnum.IP2, IPADdressEnum.IP3 })
				{
					driver.Configure.Mmonitor.IpAddress.Set(x);					
				}
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:UPDate
				driver.Configure.Sib.Update.Set();
				driver.Configure.Sib.Update.SetAndWait();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:ENABle
				bool value = driver.Configure.Sib.Enable.Get(SystemInfoBlockRepCap.Default);
				value = driver.Configure.Sib.Enable.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:ENABle
				driver.Configure.Sib.Enable.Set(false, SystemInfoBlockRepCap.Default);
				driver.Configure.Sib.Enable.Set(false);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:SYST:SYNC
				int value = driver.Configure.Sib.Syst.Sync.Get(SystemInfoBlockRepCap.Default);
				value = driver.Configure.Sib.Syst.Sync.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:SYST:SYNC
				driver.Configure.Sib.Syst.Sync.Set(1, SystemInfoBlockRepCap.Default);
				driver.Configure.Sib.Syst.Sync.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:TNFO<tnfo>:UTC
				int value = driver.Configure.Sib.Tnfo.Utc.Get(SystemInfoBlockRepCap.Default);
				value = driver.Configure.Sib.Tnfo.Utc.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:TNFO<tnfo>:UTC
				driver.Configure.Sib.Tnfo.Utc.Set(1, SystemInfoBlockRepCap.Default);
				driver.Configure.Sib.Tnfo.Utc.Set(1);
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:TNFO<tnfo>:LEAP
				int value = driver.Configure.Sib.Tnfo.Leap.Get(SystemInfoBlockRepCap.Default);
				value = driver.Configure.Sib.Tnfo.Leap.Get();
			}
			{	// CONFigure:LTE:SIGNaling<instance>:SIB<n>:TNFO<tnfo>:LEAP
				driver.Configure.Sib.Tnfo.Leap.Set(1, SystemInfoBlockRepCap.Default);
				driver.Configure.Sib.Tnfo.Leap.Set(1);
			}
			{	// SENSe:LTE:SIGNaling<instance>:RRCState
				foreach (RrcStateEnum x in new RrcStateEnum[] { RrcStateEnum.CONNected, RrcStateEnum.IDLE })
				{
					RrcStateEnum value = driver.Sense.RrcState;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:IQOut[:PCC]:PATH<n>
				RsCmwLteSig_Sense_IqOut_Pcc.GetPath_Data value = driver.Sense.IqOut.Pcc.GetPath(PathRepCap.Path1);
				value = driver.Sense.IqOut.Pcc.GetPath();
			}
			{	// SENSe:LTE:SIGNaling<instance>:IQOut:SCC<Carrier>:PATH<n>
				RsCmwLteSig_Sense_IqOut_Scc.GetPath_Data value = driver.Sense.IqOut.Scc.GetPath(SecondaryCompCarrierRepCap.Default, PathRepCap.Path1);
				value = driver.Sense.IqOut.Scc.GetPath();
			}
			{	// SENSe:LTE:SIGNaling<instance>:FADing:SCC<Carrier>:FSIMulator:ILOSs:CSAMples{clippingCounterCmdVal}
				double value = driver.Sense.Fading.Scc.FadingSimulator.InsertionLoss.GetCsamples(SecondaryCompCarrierRepCap.Default, ClippingCounterRepCap.Nr1);
				value = driver.Sense.Fading.Scc.FadingSimulator.InsertionLoss.GetCsamples();
			}
			{	// SENSe:LTE:SIGNaling<instance>:FADing[:PCC]:FSIMulator:ILOSs:CSAMples{clippingCounterCmdVal}
				double value = driver.Sense.Fading.Pcc.FadingSimulator.InsertionLoss.GetCsamples(ClippingCounterRepCap.Nr1);
				value = driver.Sense.Fading.Pcc.FadingSimulator.InsertionLoss.GetCsamples();
			}
			{	// SENSe:LTE:SIGNaling<instance>:DL:SCC<Carrier>:FCPower
				double value = driver.Sense.Downlink.Scc.GetFcPower(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Downlink.Scc.GetFcPower();
			}
			{	// SENSe:LTE:SIGNaling<instance>:DL[:PCC]:FCPower
				double value = driver.Sense.Downlink.Pcc.FcPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PATHloss
				double value = driver.Sense.Uplink.Scc.ApPower.GetPathloss(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.GetPathloss();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:EPPPower
				double value = driver.Sense.Uplink.Scc.ApPower.GetEppPower(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.GetEppPower();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:EOPower
				double value = driver.Sense.Uplink.Scc.ApPower.GetEoPower(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.GetEoPower();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:RSPower:BASic
				double value = driver.Sense.Uplink.Scc.ApPower.RsPower.GetBasic(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.RsPower.GetBasic();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PIRPower:BASic
				double value = driver.Sense.Uplink.Scc.ApPower.PirPower.GetBasic(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.PirPower.GetBasic();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PNPusch:BASic
				double value = driver.Sense.Uplink.Scc.ApPower.Pnpusch.GetBasic(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.Pnpusch.GetBasic();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:PCALpha:BASic
				PathCompAlphaEnum value = driver.Sense.Uplink.Scc.ApPower.PcAlpha.GetBasic(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.PcAlpha.GetBasic();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SCC<Carrier>:APPower:TPRRcsetup:BASic
				bool value = driver.Sense.Uplink.Scc.ApPower.TprrcSetup.GetBasic(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Uplink.Scc.ApPower.TprrcSetup.GetBasic();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:PATHloss
				double value = driver.Sense.Uplink.Seta.ApPower.Pathloss;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:EPPPower
				double value = driver.Sense.Uplink.Seta.ApPower.EppPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:EOPower
				double value = driver.Sense.Uplink.Seta.ApPower.EoPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:RSPower:BASic
				double value = driver.Sense.Uplink.Seta.ApPower.RsPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:PIRPower:BASic
				double value = driver.Sense.Uplink.Seta.ApPower.PirPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:PNPusch:BASic
				double value = driver.Sense.Uplink.Seta.ApPower.Pnpusch.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:PCALpha:BASic
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					PathCompAlphaEnum value = driver.Sense.Uplink.Seta.ApPower.PcAlpha.Basic;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETA:APPower:TPRRcsetup:BASic
				bool value = driver.Sense.Uplink.Seta.ApPower.TprrcSetup.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:PATHloss
				double value = driver.Sense.Uplink.Setb.ApPower.Pathloss;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:EPPPower
				double value = driver.Sense.Uplink.Setb.ApPower.EppPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:EOPower
				double value = driver.Sense.Uplink.Setb.ApPower.EoPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:RSPower:BASic
				double value = driver.Sense.Uplink.Setb.ApPower.RsPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:PIRPower:BASic
				double value = driver.Sense.Uplink.Setb.ApPower.PirPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:PNPusch:BASic
				double value = driver.Sense.Uplink.Setb.ApPower.Pnpusch.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:PCALpha:BASic
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					PathCompAlphaEnum value = driver.Sense.Uplink.Setb.ApPower.PcAlpha.Basic;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETB:APPower:TPRRcsetup:BASic
				bool value = driver.Sense.Uplink.Setb.ApPower.TprrcSetup.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:PATHloss
				double value = driver.Sense.Uplink.Setc.ApPower.Pathloss;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:EPPPower
				double value = driver.Sense.Uplink.Setc.ApPower.EppPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:EOPower
				double value = driver.Sense.Uplink.Setc.ApPower.EoPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:RSPower:BASic
				double value = driver.Sense.Uplink.Setc.ApPower.RsPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:PIRPower:BASic
				double value = driver.Sense.Uplink.Setc.ApPower.PirPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:PNPusch:BASic
				double value = driver.Sense.Uplink.Setc.ApPower.Pnpusch.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:PCALpha:BASic
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					PathCompAlphaEnum value = driver.Sense.Uplink.Setc.ApPower.PcAlpha.Basic;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL:SETC:APPower:TPRRcsetup:BASic
				bool value = driver.Sense.Uplink.Setc.ApPower.TprrcSetup.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PATHloss
				double value = driver.Sense.Uplink.Pcc.ApPower.Pathloss;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:EPPPower
				double value = driver.Sense.Uplink.Pcc.ApPower.EppPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:EOPower
				double value = driver.Sense.Uplink.Pcc.ApPower.EoPower;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:RSPower:BASic
				double value = driver.Sense.Uplink.Pcc.ApPower.RsPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PIRPower:BASic
				double value = driver.Sense.Uplink.Pcc.ApPower.PirPower.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PNPusch:BASic
				double value = driver.Sense.Uplink.Pcc.ApPower.Pnpusch.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:PCALpha:BASic
				foreach (PathCompAlphaEnum x in new PathCompAlphaEnum[] { PathCompAlphaEnum.DOT4, PathCompAlphaEnum.DOT5, PathCompAlphaEnum.DOT6, PathCompAlphaEnum.DOT7, PathCompAlphaEnum.DOT8, PathCompAlphaEnum.DOT9, PathCompAlphaEnum.ONE, PathCompAlphaEnum.ZERO })
				{
					PathCompAlphaEnum value = driver.Sense.Uplink.Pcc.ApPower.PcAlpha.Basic;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UL[:PCC]:APPower:TPRRcsetup:BASic
				bool value = driver.Sense.Uplink.Pcc.ApPower.TprrcSetup.Basic;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:TSCHeme
				foreach (TransmSchemeEnum x in new TransmSchemeEnum[] { TransmSchemeEnum.CLSingle, TransmSchemeEnum.CLSMultiplex, TransmSchemeEnum.DBF78, TransmSchemeEnum.FBF710, TransmSchemeEnum.OLSMultiplex, TransmSchemeEnum.S7I8, TransmSchemeEnum.SBF5, TransmSchemeEnum.SBF8, TransmSchemeEnum.SIMO, TransmSchemeEnum.SISO, TransmSchemeEnum.TBF79, TransmSchemeEnum.TXDiversity, TransmSchemeEnum.UNDefined })
				{
					TransmSchemeEnum value = driver.Sense.Connection.Pcc.Tscheme;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:HPUSch:ACTive
				bool value = driver.Sense.Connection.Pcc.Hpusch.Active;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:DL{streamCmdVal}:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.UdChannels.Downlink.Crate.GetAll(StreamRepCap.Default);
				value = driver.Sense.Connection.Pcc.UdChannels.Downlink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:UDCHannels:UL:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.UdChannels.Uplink.Crate.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:DL{streamCmdVal}:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.Sps.Downlink.Crate.GetAll(StreamRepCap.Default);
				value = driver.Sense.Connection.Pcc.Sps.Downlink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:SPS:UL:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.Sps.Uplink.Crate.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:DL{streamCmdVal}:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.UdttiBased.Downlink.Crate.GetAll(StreamRepCap.Default);
				value = driver.Sense.Connection.Pcc.UdttiBased.Downlink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:UDTTibased:UL:CRATe:ALL
				List<double> value = driver.Sense.Connection.Pcc.UdttiBased.Uplink.Crate.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCS:ATABle:LIST
				foreach (TableEnum x in new TableEnum[] { TableEnum.ANY, TableEnum.CW1, TableEnum.CW2, TableEnum.OTLC1, TableEnum.OTLC2, TableEnum.TFLC1, TableEnum.TFLC2 })
				{
					List<TableEnum> value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.Mcs.Atable.List;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:CSIRs:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FWBCqi:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:MCS:ATABle:LIST
				foreach (TableEnum x in new TableEnum[] { TableEnum.ANY, TableEnum.CW1, TableEnum.CW2, TableEnum.OTLC1, TableEnum.OTLC2, TableEnum.TFLC1, TableEnum.TFLC2 })
				{
					List<TableEnum> value = driver.Sense.Connection.Pcc.Fcri.Downlink.Mcs.Atable.List;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fcri.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fcri.Downlink.McsTable.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCRI:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCS:ATABle:LIST
				foreach (TableEnum x in new TableEnum[] { TableEnum.ANY, TableEnum.CW1, TableEnum.CW2, TableEnum.OTLC1, TableEnum.OTLC2, TableEnum.TFLC1, TableEnum.TFLC2 })
				{
					List<TableEnum> value = driver.Sense.Connection.Pcc.Fcpri.Downlink.Mcs.Atable.List;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:CSIRs:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.Csirs.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:FCPRi:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Pcc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:PDCCh:PSYMbols
				int value = driver.Sense.Connection.Pcc.Pdcch.Psymbols;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:PDCCh:ALEVel
				RsCmwLteSig_Sense_Connection_Pcc_Pdcch.Alevel_Data value = driver.Sense.Connection.Pcc.Pdcch.Alevel;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection[:PCC]:PUCCh:FFCA
				foreach (PucchFormatEnum x in new PucchFormatEnum[] { PucchFormatEnum.F1BCs, PucchFormatEnum.F3, PucchFormatEnum.F4, PucchFormatEnum.F5 })
				{
					PucchFormatEnum value = driver.Sense.Connection.Pcc.Pucch.Ffca;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:TSCHeme
				TransmSchemeEnum value = driver.Sense.Connection.Scc.GetTscheme(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.GetTscheme();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:HPUSch:ACTive
				bool value = driver.Sense.Connection.Scc.Hpusch.GetActive(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Hpusch.GetActive();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:DL{streamCmdVal}:FSUBframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.FullSubframes.GetCrate(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.FullSubframes.GetCrate();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:DL{streamCmdVal}:PIPSubframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.PipSubframes.GetCrate(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.PipSubframes.GetCrate();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:FBURst:DL{streamCmdVal}:PEPSubframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.PepSubframes.GetCrate(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Fburst.Downlink.PepSubframes.GetCrate();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:DL{streamCmdVal}:FSUBframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.FullSubframes.GetCrate(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.FullSubframes.GetCrate();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:DL{streamCmdVal}:PIPSubframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.PipSubframes.GetCrate(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.PipSubframes.GetCrate();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:LAA:RBURst:DL{streamCmdVal}:PEPSubframes:CRATe
				double value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.PepSubframes.GetCrate(SymbolsEnum.S0, SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Laa.Rburst.Downlink.PepSubframes.GetCrate(SymbolsEnum.S0);
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:DL{streamCmdVal}:CRATe:ALL
				List<double> value = driver.Sense.Connection.Scc.UdChannels.Downlink.Crate.GetAll(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Downlink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDCHannels:UL:CRATe:ALL
				List<double> value = driver.Sense.Connection.Scc.UdChannels.Uplink.Crate.GetAll(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.UdChannels.Uplink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:DL{streamCmdVal}:CRATe:ALL
				List<double> value = driver.Sense.Connection.Scc.UdttiBased.Downlink.Crate.GetAll(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Sense.Connection.Scc.UdttiBased.Downlink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:UDTTibased:UL:CRATe:ALL
				List<double> value = driver.Sense.Connection.Scc.UdttiBased.Uplink.Crate.GetAll(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.UdttiBased.Uplink.Crate.GetAll();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCS:ATABle:LIST
				List<TableEnum> value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.Mcs.Atable.GetList(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.Mcs.Atable.GetList();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:CSIRs:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Csirs.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FWBCqi:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fwbcqi.Downlink.McsTable.Ssubframe.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCS:ATABle:LIST
				List<TableEnum> value = driver.Sense.Connection.Scc.Fcri.Downlink.Mcs.Atable.GetList(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.Mcs.Atable.GetList();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCRI:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fcri.Downlink.McsTable.Ssubframe.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCS:ATABle:LIST
				List<TableEnum> value = driver.Sense.Connection.Scc.Fcpri.Downlink.Mcs.Atable.GetList(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.Mcs.Atable.GetList();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:CSIRs:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Csirs.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:FCPRi:DL:MCSTable:SSUBframe:DETermined
				List<int> value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY, SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined(TableEnum.ANY);
				value = driver.Sense.Connection.Scc.Fcpri.Downlink.McsTable.Ssubframe.GetDetermined();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:PSYMbols
				int value = driver.Sense.Connection.Scc.Pdcch.GetPsymbols(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Pdcch.GetPsymbols();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:SCC<Carrier>:PDCCh:ALEVel
				RsCmwLteSig_Sense_Connection_Scc_Pdcch.GetAlevel_Data value = driver.Sense.Connection.Scc.Pdcch.GetAlevel(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.Connection.Scc.Pdcch.GetAlevel();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:DL:SCC<Carrier>
				double value = driver.Sense.Connection.Ethroughput.Downlink.GetScc(SecondaryCompCarrierRepCap.CC1);
				value = driver.Sense.Connection.Ethroughput.Downlink.GetScc();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:DL:ALL
				double value = driver.Sense.Connection.Ethroughput.Downlink.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:DL[:PCC]:STReam{streamCmdVal}
				double value = driver.Sense.Connection.Ethroughput.Downlink.Pcc.GetStream(StreamRepCap.S1);
				value = driver.Sense.Connection.Ethroughput.Downlink.Pcc.GetStream();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:DL[:PCC]
				double value = driver.Sense.Connection.Ethroughput.Downlink.Pcc.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:DL:SCC<Carrier>:STReam{streamCmdVal}
				double value = driver.Sense.Connection.Ethroughput.Downlink.Scc.GetStream(SecondaryCompCarrierRepCap.Default, StreamRepCap.S1);
				value = driver.Sense.Connection.Ethroughput.Downlink.Scc.GetStream();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:UL[:PCC]
				double value = driver.Sense.Connection.Ethroughput.Uplink.Pcc;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:UL:SCC<Carrier>
				double value = driver.Sense.Connection.Ethroughput.Uplink.GetScc(SecondaryCompCarrierRepCap.CC1);
				value = driver.Sense.Connection.Ethroughput.Uplink.GetScc();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CONNection:ETHRoughput:UL:ALL
				double value = driver.Sense.Connection.Ethroughput.Uplink.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CQIReporting[:PCC]:RPERiod
				int value = driver.Sense.CqiReporting.Pcc.Rperiod;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CQIReporting[:PCC]:ROFFset
				int value = driver.Sense.CqiReporting.Pcc.Roffset;
			}
			{	// SENSe:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:RPERiod
				int value = driver.Sense.CqiReporting.Scc.GetRperiod(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.CqiReporting.Scc.GetRperiod();
			}
			{	// SENSe:LTE:SIGNaling<instance>:CQIReporting:SCC<Carrier>:ROFFset
				int value = driver.Sense.CqiReporting.Scc.GetRoffset(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.CqiReporting.Scc.GetRoffset();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:RSRP:RANGe
				RsCmwLteSig_Sense_UeReport_Pcc_Rsrp.Range_Data value = driver.Sense.UeReport.Pcc.Rsrp.Range;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:RSRP
				int value = driver.Sense.UeReport.Pcc.Rsrp.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:RSRQ:RANGe
				RsCmwLteSig_Sense_UeReport_Pcc_Rsrq.Range_Data value = driver.Sense.UeReport.Pcc.Rsrq.Range;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:RSRQ
				int value = driver.Sense.UeReport.Pcc.Rsrq.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:SCELl:RANGe
				RsCmwLteSig_Sense_UeReport_Pcc_Scell.Range_Data value = driver.Sense.UeReport.Pcc.Scell.Range;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport[:PCC]:SCELl
				RsCmwLteSig_Sense_UeReport_Pcc_Scell.Value_Data value = driver.Sense.UeReport.Pcc.Scell.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSRP
				int value = driver.Sense.UeReport.Scc.GetRsrp(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.GetRsrp();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSRQ
				int value = driver.Sense.UeReport.Scc.GetRsrq(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.GetRsrq();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:SCELl
				RsCmwLteSig_Sense_UeReport_Scc.GetScell_Data value = driver.Sense.UeReport.Scc.GetScell(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.GetScell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:COCC
				int value = driver.Sense.UeReport.Scc.GetCocc(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.GetCocc();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RRESult
				int value = driver.Sense.UeReport.Scc.GetRresult(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.GetRresult();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSRP:RANGe
				RsCmwLteSig_Sense_UeReport_Scc_Rsrp.GetRange_Data value = driver.Sense.UeReport.Scc.Rsrp.GetRange(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.Rsrp.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:RSRQ:RANGe
				RsCmwLteSig_Sense_UeReport_Scc_Rsrq.GetRange_Data value = driver.Sense.UeReport.Scc.Rsrq.GetRange(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.Rsrq.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:SCC<Carrier>:SCELl:RANGe
				RsCmwLteSig_Sense_UeReport_Scc_Scell.GetRange_Data value = driver.Sense.UeReport.Scc.Scell.GetRange(SecondaryCompCarrierRepCap.Default);
				value = driver.Sense.UeReport.Scc.Scell.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:LTE:CELL<nr>
				RsCmwLteSig_Sense_UeReport_Ncell_Lte.GetCell_Data value = driver.Sense.UeReport.Ncell.Lte.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Lte.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:LTE:CELL<nr>:RANGe
				RsCmwLteSig_Sense_UeReport_Ncell_Lte_Cell.GetRange_Data value = driver.Sense.UeReport.Ncell.Lte.Cell.GetRange(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Lte.Cell.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:GSM:CELL<nr>
				int value = driver.Sense.UeReport.Ncell.Gsm.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Gsm.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:GSM:CELL<nr>:RANGe
				RsCmwLteSig_Sense_UeReport_Ncell_Gsm_Cell.GetRange_Data value = driver.Sense.UeReport.Ncell.Gsm.Cell.GetRange(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Gsm.Cell.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:WCDMa:CELL<nr>
				RsCmwLteSig_Sense_UeReport_Ncell_Wcdma.GetCell_Data value = driver.Sense.UeReport.Ncell.Wcdma.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Wcdma.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:WCDMa:CELL<nr>:RANGe
				RsCmwLteSig_Sense_UeReport_Ncell_Wcdma_Cell.GetRange_Data value = driver.Sense.UeReport.Ncell.Wcdma.Cell.GetRange(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Wcdma.Cell.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:CDMA:CELL<nr>
				RsCmwLteSig_Sense_UeReport_Ncell_Cdma.GetCell_Data value = driver.Sense.UeReport.Ncell.Cdma.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Cdma.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:EVDO:CELL<nr>
				RsCmwLteSig_Sense_UeReport_Ncell_Evdo.GetCell_Data value = driver.Sense.UeReport.Ncell.Evdo.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Evdo.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:TDSCdma:CELL<nr>
				int value = driver.Sense.UeReport.Ncell.Tdscdma.GetCell(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Tdscdma.GetCell();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UEReport:NCELl:TDSCdma:CELL<nr>:RANGe
				RsCmwLteSig_Sense_UeReport_Ncell_Tdscdma_Cell.GetRange_Data value = driver.Sense.UeReport.Ncell.Tdscdma.Cell.GetRange(CellNoRepCap.Default);
				value = driver.Sense.UeReport.Ncell.Tdscdma.Cell.GetRange();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:UEUSage
				foreach (UeUsageEnum x in new UeUsageEnum[] { UeUsageEnum.DCENtric, UeUsageEnum.VCENtric })
				{
					UeUsageEnum value = driver.Sense.UesInfo.UeUsage;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:VDPReference
				foreach (VdPreferenceEnum x in new VdPreferenceEnum[] { VdPreferenceEnum.CVONly, VdPreferenceEnum.CVPRefered, VdPreferenceEnum.IPVonly, VdPreferenceEnum.IPVPrefered })
				{
					VdPreferenceEnum value = driver.Sense.UesInfo.VdPreference;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:IMEI
				string value = driver.Sense.UesInfo.Imei;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:IMSI
				string value = driver.Sense.UesInfo.Imsi;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:UEADdress:IPV<n>
				List<string> value = driver.Sense.UesInfo.UeAddress.GetIpv(IPversionRepCap.IPv4);
				value = driver.Sense.UesInfo.UeAddress.GetIpv();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:UEADdress:DEDBearer:SEParate
				RsCmwLteSig_Sense_UesInfo_UeAddress_DedBearer.Separate_Data value = driver.Sense.UesInfo.UeAddress.DedBearer.Separate;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UESinfo:UEADdress:DEDBearer
				RsCmwLteSig_Sense_UesInfo_UeAddress_DedBearer.Value_Data value = driver.Sense.UesInfo.UeAddress.DedBearer.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:ASRelease
				foreach (AccStratReleaseEnum x in new AccStratReleaseEnum[] { AccStratReleaseEnum.REL10, AccStratReleaseEnum.REL11, AccStratReleaseEnum.REL12, AccStratReleaseEnum.REL13, AccStratReleaseEnum.REL14, AccStratReleaseEnum.REL8, AccStratReleaseEnum.REL9 })
				{
					AccStratReleaseEnum value = driver.Sense.UeCapability.AsRelease;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:DCIulca
				bool value = driver.Sense.UeCapability.Dciulca;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:URTTimediff
				bool value = driver.Sense.UeCapability.UrtTimeDiff;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IDCindex
				bool value = driver.Sense.UeCapability.IdcIndex;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PPINdex
				bool value = driver.Sense.UeCapability.PpIndex;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:DTYPe
				foreach (DeviceTypeEnum x in new DeviceTypeEnum[] { DeviceTypeEnum.NBFBcopt })
				{
					DeviceTypeEnum value = driver.Sense.UeCapability.Dtype;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RREPort
				bool value = driver.Sense.UeCapability.Rreport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:ERLField
				bool value = driver.Sense.UeCapability.ErlField;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:LMMeas
				bool value = driver.Sense.UeCapability.LmMeas;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:UECategory
				int value = driver.Sense.UeCapability.UeCategory.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:UECategory:DL:ENHanced
				string value = driver.Sense.UeCapability.UeCategory.Downlink.Enhanced;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:UECategory:UL:ENHanced
				string value = driver.Sense.UeCapability.UeCategory.Uplink.Enhanced;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PDCP:SRPRofiles
				RsCmwLteSig_Sense_UeCapability_Pdcp.Srprofiles_Data value = driver.Sense.UeCapability.Pdcp.Srprofiles;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PDCP:MRCSessions
				foreach (MaxNuRohcConSesEnum x in new MaxNuRohcConSesEnum[] { MaxNuRohcConSesEnum.CS1024, MaxNuRohcConSesEnum.CS12, MaxNuRohcConSesEnum.CS128, MaxNuRohcConSesEnum.CS16, MaxNuRohcConSesEnum.CS16384, MaxNuRohcConSesEnum.CS2, MaxNuRohcConSesEnum.CS24, MaxNuRohcConSesEnum.CS256, MaxNuRohcConSesEnum.CS32, MaxNuRohcConSesEnum.CS4, MaxNuRohcConSesEnum.CS48, MaxNuRohcConSesEnum.CS512, MaxNuRohcConSesEnum.CS64, MaxNuRohcConSesEnum.CS8 })
				{
					MaxNuRohcConSesEnum value = driver.Sense.UeCapability.Pdcp.MrcSessions;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PDCP:SNEXtension
				bool value = driver.Sense.UeCapability.Pdcp.SnExtension;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PDCP:SRCContinue
				bool value = driver.Sense.UeCapability.Pdcp.Srccontinue;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:UTASupported
				bool value = driver.Sense.UeCapability.Player.UtaSupported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:USRSsupport
				bool value = driver.Sense.UeCapability.Player.UsrsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:EDLFsupport
				bool value = driver.Sense.UeCapability.Player.EdlfSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:EDLTsupport
				bool value = driver.Sense.UeCapability.Player.EdltSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TAPPsupport
				bool value = driver.Sense.UeCapability.Player.TappSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TWEFsupport
				bool value = driver.Sense.UeCapability.Player.TwefSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:PDSupport
				bool value = driver.Sense.UeCapability.Player.PdSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:CCSSupport
				bool value = driver.Sense.UeCapability.Player.CcsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:SPPSupport
				bool value = driver.Sense.UeCapability.Player.SppSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:MCPCsupport
				bool value = driver.Sense.UeCapability.Player.McpcSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:NURClist
				List<bool> value = driver.Sense.UeCapability.Player.NurcList;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:CIHandl
				bool value = driver.Sense.UeCapability.Player.Cihandl;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:EPDCch
				bool value = driver.Sense.UeCapability.Player.Epdcch;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:MACReporting
				bool value = driver.Sense.UeCapability.Player.MacReporting;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:SCIHandl
				bool value = driver.Sense.UeCapability.Player.SciHandl;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TSSubframe
				bool value = driver.Sense.UeCapability.Player.TsSubframe;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TDPChselect
				bool value = driver.Sense.UeCapability.Player.TdpchSelect;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:ULComp
				bool value = driver.Sense.UeCapability.Player.UlComp;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:ITCWithdiff
				string value = driver.Sense.UeCapability.Player.ItcWithDiff;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:EHPFdd
				bool value = driver.Sense.UeCapability.Player.Ehpfdd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:EFTCodebook
				bool value = driver.Sense.UeCapability.Player.Eftcodebook;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TFCPcelldplx
				string value = driver.Sense.UeCapability.Player.TfcPcellDplx;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TRCTddpcell
				bool value = driver.Sense.UeCapability.Player.TrcTddpCell;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:TRCFddpcell
				bool value = driver.Sense.UeCapability.Player.TrcFddpCell;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:PFMode
				bool value = driver.Sense.UeCapability.Player.PfMode;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:PSPSfset
				bool value = driver.Sense.UeCapability.Player.PspsfSet;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:CSFSet
				bool value = driver.Sense.UeCapability.Player.CsfSet;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:NRRT
				bool value = driver.Sense.UeCapability.Player.Nrrt;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:PLAYer:DSDCell
				bool value = driver.Sense.UeCapability.Player.DsdCell;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:MTADvance
				int value = driver.Sense.UeCapability.Rf.MtAdvance;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:SUPPorted
				List<bool> value = driver.Sense.UeCapability.Rf.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:HDUPlex
				List<bool> value = driver.Sense.UeCapability.Rf.Hduplex;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:DL<qam>
				int value = driver.Sense.UeCapability.Rf.Downlink;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:UL<qam>
				int value = driver.Sense.UeCapability.Rf.GetUplink(ULqamRepCap.QAM64);
				value = driver.Sense.UeCapability.Rf.GetUplink();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:FBRetrieval
				bool value = driver.Sense.UeCapability.Rf.FbRetrieval;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:RBANds
				foreach (OperatingBandCenum x in new OperatingBandCenum[] { OperatingBandCenum.OB1, OperatingBandCenum.OB10, OperatingBandCenum.OB11, OperatingBandCenum.OB12, OperatingBandCenum.OB13, OperatingBandCenum.OB14, OperatingBandCenum.OB15, OperatingBandCenum.OB16, OperatingBandCenum.OB17, OperatingBandCenum.OB18, OperatingBandCenum.OB19, OperatingBandCenum.OB2, OperatingBandCenum.OB20, OperatingBandCenum.OB21, OperatingBandCenum.OB22, OperatingBandCenum.OB23, OperatingBandCenum.OB24, OperatingBandCenum.OB25, OperatingBandCenum.OB250, OperatingBandCenum.OB252, OperatingBandCenum.OB255, OperatingBandCenum.OB26, OperatingBandCenum.OB27, OperatingBandCenum.OB28, OperatingBandCenum.OB29, OperatingBandCenum.OB3, OperatingBandCenum.OB30, OperatingBandCenum.OB31, OperatingBandCenum.OB32, OperatingBandCenum.OB33, OperatingBandCenum.OB34, OperatingBandCenum.OB35, OperatingBandCenum.OB36, OperatingBandCenum.OB37, OperatingBandCenum.OB38, OperatingBandCenum.OB39, OperatingBandCenum.OB4, OperatingBandCenum.OB40, OperatingBandCenum.OB41, OperatingBandCenum.OB42, OperatingBandCenum.OB43, OperatingBandCenum.OB44, OperatingBandCenum.OB45, OperatingBandCenum.OB46, OperatingBandCenum.OB48, OperatingBandCenum.OB49, OperatingBandCenum.OB5, OperatingBandCenum.OB50, OperatingBandCenum.OB51, OperatingBandCenum.OB52, OperatingBandCenum.OB53, OperatingBandCenum.OB6, OperatingBandCenum.OB65, OperatingBandCenum.OB66, OperatingBandCenum.OB67, OperatingBandCenum.OB68, OperatingBandCenum.OB69, OperatingBandCenum.OB7, OperatingBandCenum.OB70, OperatingBandCenum.OB71, OperatingBandCenum.OB72, OperatingBandCenum.OB73, OperatingBandCenum.OB74, OperatingBandCenum.OB75, OperatingBandCenum.OB76, OperatingBandCenum.OB8, OperatingBandCenum.OB85, OperatingBandCenum.OB9, OperatingBandCenum.UDEFined })
				{
					List<OperatingBandCenum> value = driver.Sense.UeCapability.Rf.Rbands;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:FBPadjust
				bool value = driver.Sense.UeCapability.Rf.FbpAdjust;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:MMPRbehavior
				string value = driver.Sense.UeCapability.Rf.MmprBehavior;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:SRTX
				int value = driver.Sense.UeCapability.Rf.Srtx;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:SNCap
				string value = driver.Sense.UeCapability.Rf.Sncap;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>
				List<OperatingBandCenum> value = driver.Sense.UeCapability.Rf.Bcombination.V.GetEutra(UeReportRepCap.V1020, EutraBandRepCap.Band1);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.GetEutra();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:BCSet
				List<string> value = driver.Sense.UeCapability.Rf.Bcombination.V.Bcset;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>:SCPRoc
				UeProcessesCountEnum value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.GetScproc(EutraBandRepCap.Default);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.GetScproc();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>:BCLass:UL
				List<string> value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Bclass.GetUplink(EutraBandRepCap.Default);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Bclass.GetUplink();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>:BCLass:DL
				List<string> value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Bclass.GetDownlink(EutraBandRepCap.Default);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Bclass.GetDownlink();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>:MCAPability:UL
				List<int> value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Mcapability.GetUplink(EutraBandRepCap.Default);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Mcapability.GetUplink();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:BCOMbination:V<Number>:EUTRa<BandNr>:MCAPability:DL
				List<int> value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Mcapability.GetDownlink(EutraBandRepCap.Default);
				value = driver.Sense.UeCapability.Rf.Bcombination.V.Eutra.Mcapability.GetDownlink();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:DCSupport:ASYNchronous
				int value = driver.Sense.UeCapability.Rf.DcSupport.Asynchronous;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:RF:DCSupport:SCGRouping
				int value = driver.Sense.UeCapability.Rf.DcSupport.Scgrouping;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IFNGaps
				List<bool> value = driver.Sense.UeCapability.Meas.GetInterFreqNgaps(OperatingBandCenum.OB1);
				value = driver.Sense.UeCapability.Meas.GetInterFreqNgaps();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:RMWideband
				bool value = driver.Sense.UeCapability.Meas.RmWideband;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:BFINterrupt
				bool value = driver.Sense.UeCapability.Meas.BfInterrupt;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:RCOReporting
				int value = driver.Sense.UeCapability.Meas.RcoReporting;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:UFDD
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.GetUfdd(OperatingBandDenum.OB1);
				value = driver.Sense.UeCapability.Meas.IrnGaps.GetUfdd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:UTDD<n>
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.GetUtdd(OperatingBandDenum.OB1);
				value = driver.Sense.UeCapability.Meas.IrnGaps.GetUtdd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:GERan
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.GetGeran(GeranBbandEnum.G045);
				value = driver.Sense.UeCapability.Meas.IrnGaps.GetGeran();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:CHRPd
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.GetChrpd(Cdma2kBandEnum.BC0);
				value = driver.Sense.UeCapability.Meas.IrnGaps.GetChrpd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:CXRTt
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.GetCxrtt(Cdma2kBandEnum.BC0);
				value = driver.Sense.UeCapability.Meas.IrnGaps.GetCxrtt();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:V<number>:UFDD
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetUfdd(OperatingBandDenum.OB1);
				value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetUfdd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:V<number>:UTDD<n>
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetUtdd(OperatingBandDenum.OB1);
				value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetUtdd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:V<number>:GERan
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetGeran(GeranBbandEnum.G045);
				value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetGeran();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:V<number>:CHRPd
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetChrpd(Cdma2kBandEnum.BC0);
				value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetChrpd();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IRNGaps:V<number>:CXRTt
				List<bool> value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetCxrtt(Cdma2kBandEnum.BC0);
				value = driver.Sense.UeCapability.Meas.IrnGaps.V.GetCxrtt();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MEAS:IFNGaps:V<number>
				List<bool> value = driver.Sense.UeCapability.Meas.InterFreqNgaps.GetV(OperatingBandCenum.OB1);
				value = driver.Sense.UeCapability.Meas.InterFreqNgaps.GetV();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FGINdicators:RNADd
				string value = driver.Sense.UeCapability.FgIndicators.Rnadd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FGINdicators:RTEN
				string value = driver.Sense.UeCapability.FgIndicators.Rten;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FGINdicators
				string value = driver.Sense.UeCapability.FgIndicators.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:UFDD:SUPPorted
				List<bool> value = driver.Sense.UeCapability.InterRat.Ufdd.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:UFDD:EREDirection:UTRA
				bool value = driver.Sense.UeCapability.InterRat.Ufdd.Eredirection.Utra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:UTDD<frequency>:SUPPorted
				List<bool> value = driver.Sense.UeCapability.InterRat.Utdd.GetSupported(UTddFreqRepCap.Default);
				value = driver.Sense.UeCapability.InterRat.Utdd.GetSupported();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:UTDD<frequency>:EREDirection:UTDD
				bool value = driver.Sense.UeCapability.InterRat.Utdd.Eredirection.GetUtdd(UTddFreqRepCap.Default);
				value = driver.Sense.UeCapability.InterRat.Utdd.Eredirection.GetUtdd();
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:GERan:SUPPorted
				List<bool> value = driver.Sense.UeCapability.InterRat.Geran.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:GERan:PHGeran
				bool value = driver.Sense.UeCapability.InterRat.Geran.Phgeran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:GERan:EREDirection
				bool value = driver.Sense.UeCapability.InterRat.Geran.Eredirection;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:GERan:DTM
				bool value = driver.Sense.UeCapability.InterRat.Geran.Dtm;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CHRPd:SUPPorted
				List<bool> value = driver.Sense.UeCapability.InterRat.Chrpd.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CHRPd:TCONfig
				foreach (TxRxConfigurationEnum x in new TxRxConfigurationEnum[] { TxRxConfigurationEnum.DUAL, TxRxConfigurationEnum.SINGle })
				{
					TxRxConfigurationEnum value = driver.Sense.UeCapability.InterRat.Chrpd.Tconfig;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CHRPd:RCONfig
				foreach (TxRxConfigurationEnum x in new TxRxConfigurationEnum[] { TxRxConfigurationEnum.DUAL, TxRxConfigurationEnum.SINGle })
				{
					TxRxConfigurationEnum value = driver.Sense.UeCapability.InterRat.Chrpd.Rconfig;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:SUPPorted
				List<bool> value = driver.Sense.UeCapability.InterRat.Cxrtt.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:TCONfig
				foreach (TxRxConfigurationEnum x in new TxRxConfigurationEnum[] { TxRxConfigurationEnum.DUAL, TxRxConfigurationEnum.SINGle })
				{
					TxRxConfigurationEnum value = driver.Sense.UeCapability.InterRat.Cxrtt.Tconfig;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:RCONfig
				foreach (TxRxConfigurationEnum x in new TxRxConfigurationEnum[] { TxRxConfigurationEnum.DUAL, TxRxConfigurationEnum.SINGle })
				{
					TxRxConfigurationEnum value = driver.Sense.UeCapability.InterRat.Cxrtt.Rconfig;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:ECSFb
				bool value = driver.Sense.UeCapability.InterRat.Cxrtt.Ecsfb;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:ECCMob
				bool value = driver.Sense.UeCapability.InterRat.Cxrtt.Eccmob;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CXRTt:ECDual
				bool value = driver.Sense.UeCapability.InterRat.Cxrtt.Ecdual;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:IRAT:CDMA<2000>:NWSHaring
				bool value = driver.Sense.UeCapability.InterRat.Cdma.NwSharing;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MBMS:NSCell
				bool value = driver.Sense.UeCapability.Mbms.Nscell;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MBMS:SCELl
				bool value = driver.Sense.UeCapability.Mbms.Scell;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:CPINdication:UTRan
				bool value = driver.Sense.UeCapability.CpIndication.Utran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:CPINdication:FREQuency:INTRa
				bool value = driver.Sense.UeCapability.CpIndication.Frequency.Intra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:CPINdication:FREQuency:INTer
				bool value = driver.Sense.UeCapability.CpIndication.Frequency.Inter;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:NCSacq:UTRan
				bool value = driver.Sense.UeCapability.Ncsacq.Utran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:NCSacq:FREQuency:INTRa
				bool value = driver.Sense.UeCapability.Ncsacq.Frequency.Intra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:NCSacq:FREQuency:INTer
				bool value = driver.Sense.UeCapability.Ncsacq.Frequency.Inter;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:UBNPmeas:LMIDle
				bool value = driver.Sense.UeCapability.UbnpMeas.Lmidle;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:UBNPmeas:SGLocation
				bool value = driver.Sense.UeCapability.UbnpMeas.SgLocation;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:WIW:WIAPolicies
				bool value = driver.Sense.UeCapability.Wiw.WiaPolicies;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:WIW:WIRRules
				bool value = driver.Sense.UeCapability.Wiw.WirRules;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:LAA:DL
				int value = driver.Sense.UeCapability.Laa.Downlink;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:LAA:EDPTs
				int value = driver.Sense.UeCapability.Laa.Edpts;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:LAA:SSSPosition
				int value = driver.Sense.UeCapability.Laa.SssPosition;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:LAA:TM<TMnr>
				int value = driver.Sense.UeCapability.Laa.Tm;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:CEParameters:MODE:A
				bool value = driver.Sense.UeCapability.CeParameters.Mode.A;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:CEParameters:MODE:B
				bool value = driver.Sense.UeCapability.CeParameters.Mode.B;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:DCParameters:DTSCg
				bool value = driver.Sense.UeCapability.DcParameters.Dtscg;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:DCParameters:DTSPlit
				bool value = driver.Sense.UeCapability.DcParameters.DtSplit;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MAC:LDRXcommand
				bool value = driver.Sense.UeCapability.Mac.LdrxCommand;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:MAC:LCSPtimer
				bool value = driver.Sense.UeCapability.Mac.LcspTimer;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:SL:DSLSs
				bool value = driver.Sense.UeCapability.Sidelink.Dslss;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:SL:CSTX
				bool value = driver.Sense.UeCapability.Sidelink.Cstx;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:SL:DSRalloc
				bool value = driver.Sense.UeCapability.Sidelink.DsrAlloc;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:SL:DUSRalloc
				bool value = driver.Sense.UeCapability.Sidelink.Dusralloc;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:SL:DSPRoc
				foreach (UeSidelinkProcessesCountEnum x in new UeSidelinkProcessesCountEnum[] { UeSidelinkProcessesCountEnum.N400, UeSidelinkProcessesCountEnum.N50 })
				{
					UeSidelinkProcessesCountEnum value = driver.Sense.UeCapability.Sidelink.Dsproc;
				}
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:UTASupported
				bool value = driver.Sense.UeCapability.FaueEutra.Player.UtaSupported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:USRSsupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.UsrsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:TAPPsupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.TappSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:TWEFsupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.TwefSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:PDSupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.PdSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:CCSSupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.CcsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:SPPSupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.SppSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:MCPCsupport
				bool value = driver.Sense.UeCapability.FaueEutra.Player.McpcSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:PLAYer:NURClist
				List<bool> value = driver.Sense.UeCapability.FaueEutra.Player.NurcList;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:FGINdicators:RNADd
				string value = driver.Sense.UeCapability.FaueEutra.FgIndicators.Rnadd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:FGINdicators:RTEN
				string value = driver.Sense.UeCapability.FaueEutra.FgIndicators.Rten;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:FGINdicators
				string value = driver.Sense.UeCapability.FaueEutra.FgIndicators.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:EREDirection:UTRA
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Eredirection.Utra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:EREDirection:UTDD
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Eredirection.Utdd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:GERan:SUPPorted
				List<bool> value = driver.Sense.UeCapability.FaueEutra.InterRat.Geran.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:GERan:PHGeran
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Geran.Phgeran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:CXRTt:ECSFb
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Cxrtt.Ecsfb;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:CXRTt:ECCMob
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Cxrtt.Eccmob;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:IRAT:CXRTt:ECDual
				bool value = driver.Sense.UeCapability.FaueEutra.InterRat.Cxrtt.Ecdual;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:NCSacq:UTRan
				bool value = driver.Sense.UeCapability.FaueEutra.Ncsacq.Utran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:NCSacq:FREQuency:INTRa
				bool value = driver.Sense.UeCapability.FaueEutra.Ncsacq.Frequency.Intra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:FAUeeutra:NCSacq:FREQuency:INTer
				bool value = driver.Sense.UeCapability.FaueEutra.Ncsacq.Frequency.Inter;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:UTASupported
				bool value = driver.Sense.UeCapability.TaueEutra.Player.UtaSupported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:USRSsupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.UsrsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:TAPPsupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.TappSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:TWEFsupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.TwefSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:PDSupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.PdSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:CCSSupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.CcsSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:SPPSupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.SppSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:MCPCsupport
				bool value = driver.Sense.UeCapability.TaueEutra.Player.McpcSupport;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:PLAYer:NURClist
				List<bool> value = driver.Sense.UeCapability.TaueEutra.Player.NurcList;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:FGINdicators:RNADd
				string value = driver.Sense.UeCapability.TaueEutra.FgIndicators.Rnadd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:FGINdicators:RTEN
				string value = driver.Sense.UeCapability.TaueEutra.FgIndicators.Rten;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:FGINdicators
				string value = driver.Sense.UeCapability.TaueEutra.FgIndicators.Value;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:EREDirection:UTRA
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Eredirection.Utra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:EREDirection:UTDD
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Eredirection.Utdd;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:GERan:SUPPorted
				List<bool> value = driver.Sense.UeCapability.TaueEutra.InterRat.Geran.Supported;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:GERan:PHGeran
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Geran.Phgeran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:CXRTt:ECSFb
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Cxrtt.Ecsfb;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:CXRTt:ECCMob
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Cxrtt.Eccmob;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:IRAT:CXRTt:ECDual
				bool value = driver.Sense.UeCapability.TaueEutra.InterRat.Cxrtt.Ecdual;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:NCSacq:UTRan
				bool value = driver.Sense.UeCapability.TaueEutra.Ncsacq.Utran;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:NCSacq:FREQuency:INTRa
				bool value = driver.Sense.UeCapability.TaueEutra.Ncsacq.Frequency.Intra;
			}
			{	// SENSe:LTE:SIGNaling<instance>:UECapability:TAUeeutra:NCSacq:FREQuency:INTer
				bool value = driver.Sense.UeCapability.TaueEutra.Ncsacq.Frequency.Inter;
			}
			{	// SENSe:LTE:SIGNaling<Instance>:SMS:OUTGoing:INFO:LMSent
				foreach (LastMessageSentEnum x in new LastMessageSentEnum[] { LastMessageSentEnum.FAILed, LastMessageSentEnum.OFF, LastMessageSentEnum.ON, LastMessageSentEnum.SUCCessful })
				{
					LastMessageSentEnum value = driver.Sense.Sms.Outgoing.Info.Lmsent;
				}
			}
			{	// SENSe:LTE:SIGNaling<Instance>:SMS:INComing:INFO:DCODing
				string value = driver.Sense.Sms.Incoming.Info.Dcoding;
			}
			{	// SENSe:LTE:SIGNaling<instance>:SMS:INComing:INFO:MTEXt
				string value = driver.Sense.Sms.Incoming.Info.Mtext;
			}
			{	// SENSe:LTE:SIGNaling<instance>:SMS:INComing:INFO:MLENgth
				int value = driver.Sense.Sms.Incoming.Info.Mlength;
			}
			{	// SENSe:LTE:SIGNaling<instance>:SMS:INFO:LRMessage:RFLag
				bool value = driver.Sense.Sms.Info.LrMessage.Rflag;
			}
			{	// SENSe:LTE:SIGNaling<instance>:EELog:LAST
				RsCmwLteSig_Sense_EeLog.Last_Data value = driver.Sense.EeLog.Last;
			}
			{	// SENSe:LTE:SIGNaling<instance>:EELog:ALL
				RsCmwLteSig_Sense_EeLog.All_Data value = driver.Sense.EeLog.All;
			}
			{	// SENSe:LTE:SIGNaling<instance>:ELOG:LAST
				RsCmwLteSig_Sense_Elog.GetLast_Data value = driver.Sense.Elog.GetLast(TimeResolutionEnum.HRES);
				value = driver.Sense.Elog.GetLast();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:ELOG:ALL
				RsCmwLteSig_Sense_Elog.GetAll_Data value = driver.Sense.Elog.GetAll(TimeResolutionEnum.HRES);
				value = driver.Sense.Elog.GetAll();				
			}
			{	// SENSe:LTE:SIGNaling<instance>:SIB<n>:TTIMing
				RsCmwLteSig_Sense_Sib.Ttiming_Data value = driver.Sense.Sib.Ttiming;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:ENHanced
				RsCmwLteSig_Prepare_Handover.Enhanced_Data value = driver.Prepare.Handover.Enhanced;
				driver.Prepare.Handover.Enhanced = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:DESTination
				string value = driver.Prepare.Handover.Destination;
				driver.Prepare.Handover.Destination = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:MMODe
				foreach (HandoverModeEnum x in new HandoverModeEnum[] { HandoverModeEnum.HANDover, HandoverModeEnum.MTCSfallback, HandoverModeEnum.REDirection })
				{
					driver.Prepare.Handover.Mmode = x;
					HandoverModeEnum value = driver.Prepare.Handover.Mmode;
				}
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:CTYPe
				foreach (VolteHandoverTypeEnum x in new VolteHandoverTypeEnum[] { VolteHandoverTypeEnum.PSData, VolteHandoverTypeEnum.PSVolte })
				{
					driver.Prepare.Handover.Ctype = x;
					VolteHandoverTypeEnum value = driver.Prepare.Handover.Ctype;
				}
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover
				RsCmwLteSig_Prepare_Handover.Value_Data value = driver.Prepare.Handover.Value;
				driver.Prepare.Handover.Value = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:CATalog:DESTination
				List<string> value = driver.Prepare.Handover.Catalog.Destination;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:DESTination
				foreach (HandoverDestinationEnum x in new HandoverDestinationEnum[] { HandoverDestinationEnum.CDMA, HandoverDestinationEnum.EVDO, HandoverDestinationEnum.GSM, HandoverDestinationEnum.LTE, HandoverDestinationEnum.TDSCdma, HandoverDestinationEnum.WCDMa })
				{
					driver.Prepare.Handover.External.Destination = x;
					HandoverDestinationEnum value = driver.Prepare.Handover.External.Destination;
				}
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:LTE
				RsCmwLteSig_Prepare_Handover_External.Lte_Data value = driver.Prepare.Handover.External.Lte;
				driver.Prepare.Handover.External.Lte = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:GSM
				RsCmwLteSig_Prepare_Handover_External.Gsm_Data value = driver.Prepare.Handover.External.Gsm;
				driver.Prepare.Handover.External.Gsm = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:CDMA
				RsCmwLteSig_Prepare_Handover_External.Cdma_Data value = driver.Prepare.Handover.External.Cdma;
				driver.Prepare.Handover.External.Cdma = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:EVDO
				RsCmwLteSig_Prepare_Handover_External.Evdo_Data value = driver.Prepare.Handover.External.Evdo;
				driver.Prepare.Handover.External.Evdo = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:WCDMa
				RsCmwLteSig_Prepare_Handover_External.Wcdma_Data value = driver.Prepare.Handover.External.Wcdma;
				driver.Prepare.Handover.External.Wcdma = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:HANDover:EXTernal:TDSCdma
				RsCmwLteSig_Prepare_Handover_External.Tdscdma_Data value = driver.Prepare.Handover.External.Tdscdma;
				driver.Prepare.Handover.External.Tdscdma = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:CONNection:DEDBearer:SEParate
				RsCmwLteSig_Prepare_Connection_DedBearer.Separate_Data value = driver.Prepare.Connection.DedBearer.Separate;
				driver.Prepare.Connection.DedBearer.Separate = value;
			}
			{	// PREPare:LTE:SIGNaling<instance>:CONNection:DEDBearer
				RsCmwLteSig_Prepare_Connection_DedBearer.Value_Data value = driver.Prepare.Connection.DedBearer.Value;
				driver.Prepare.Connection.DedBearer.Value = value;
			}
			{	// SOURce:LTE:SIGNaling<instance>:CELL:STATe:ALL
				RsCmwLteSig_Source_Cell_State.All_Data value = driver.Source.Cell.State.All;
			}
			{	// SOURce:LTE:SIGNaling<instance>:CELL:STATe
				bool value = driver.Source.Cell.State.Value;
				driver.Source.Cell.State.Value = value;
			}
			{	// CALL:LTE:SIGNaling<instance>:PSWitched:ACTion
				foreach (PswActionEnum x in new PswActionEnum[] { PswActionEnum.CONNect, PswActionEnum.DETach, PswActionEnum.DISConnect, PswActionEnum.HANDover, PswActionEnum.OFF, PswActionEnum.ON, PswActionEnum.SMS })
				{
					driver.Call.Pswitched.Action = x;					
				}
			}
			{	// CALL:LTE:SIGNaling<instance>:SCC<Carrier>:ACTion
				foreach (SccActionEnum x in new SccActionEnum[] { SccActionEnum.MACactivate, SccActionEnum.MACDeactivat, SccActionEnum.OFF, SccActionEnum.ON, SccActionEnum.RRCadd, SccActionEnum.RRCDelete })
				{
					driver.Call.Scc.Action.Set(x);
					driver.Call.Scc.Action.Set(x, SecondaryCompCarrierRepCap.Default);
				}
			}
			{	// CALL:LTE:SIGNaling<instance>:A:ACTion
				foreach (SccActionEnum x in new SccActionEnum[] { SccActionEnum.MACactivate, SccActionEnum.MACDeactivat, SccActionEnum.OFF, SccActionEnum.ON, SccActionEnum.RRCadd, SccActionEnum.RRCDelete })
				{
					driver.Call.A.Action = x;					
				}
			}
			{	// CALL:LTE:SIGNaling<instance>:B:ACTion
				foreach (SccActionEnum x in new SccActionEnum[] { SccActionEnum.MACactivate, SccActionEnum.MACDeactivat, SccActionEnum.OFF, SccActionEnum.ON, SccActionEnum.RRCadd, SccActionEnum.RRCDelete })
				{
					driver.Call.B.Action = x;					
				}
			}
			{	// FETCh:LTE:SIGNaling<instance>:A:STATe
				SyncStateEnum value = driver.A.State.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:B:STATe
				SyncStateEnum value = driver.B.State.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:PSWitched:STATe
				PswStateEnum value = driver.Pswitched.State.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:SCC<Carrier>:STATe
				SyncStateEnum value = driver.Scc.State.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.Scc.State.Fetch();
			}
			{	// CLEan:LTE:SIGNaling<instance>:SMS:INComing:INFO:MTEXt
				driver.Clean.Sms.Incoming.Info.Mtext.Set();
				driver.Clean.Sms.Incoming.Info.Mtext.SetAndWait();
			}
			{	// CLEan:LTE:SIGNaling<instance>:EELog
				driver.Clean.EeLog.Set();
				driver.Clean.EeLog.SetAndWait();
			}
			{	// CLEan:LTE:SIGNaling<instance>:ELOG
				driver.Clean.Elog.Set();
				driver.Clean.Elog.SetAndWait();
			}
			{	// INITiate:LTE:SIGNaling<instance>:EBLer
				driver.ExtendedBler.Initiate();
				driver.ExtendedBler.InitiateAndWait();
			}
			{	// ABORt:LTE:SIGNaling<instance>:EBLer
				driver.ExtendedBler.Abort();
				driver.ExtendedBler.AbortAndWait();
			}
			{	// STOP:LTE:SIGNaling<instance>:EBLer
				driver.ExtendedBler.Stop();
				driver.ExtendedBler.StopAndWait();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:ALL:ABSolute
				RsCmwLteSig_ExtendedBler_All_Absolute.Fetch_Data value = driver.ExtendedBler.All.Absolute.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:ALL:RELative
				RsCmwLteSig_ExtendedBler_All_Relative.Fetch_Data value = driver.ExtendedBler.All.Relative.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:ALL:CONFidence
				ConfidenceEnum value = driver.ExtendedBler.All.Confidence.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:CONFidence
				ConfidenceEnum value = driver.ExtendedBler.Pcc.Confidence.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:ABSolute
				RsCmwLteSig_ExtendedBler_Pcc_Absolute.Fetch_Data value = driver.ExtendedBler.Pcc.Absolute.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:RELative
				RsCmwLteSig_ExtendedBler_Pcc_Relative.Fetch_Data value = driver.ExtendedBler.Pcc.Relative.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:STReam{streamCmdVal}:ABSolute
				RsCmwLteSig_ExtendedBler_Pcc_Stream_Absolute.Fetch_Data value = driver.ExtendedBler.Pcc.Stream.Absolute.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Stream.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:STReam{streamCmdVal}:RELative
				RsCmwLteSig_ExtendedBler_Pcc_Stream_Relative.Fetch_Data value = driver.ExtendedBler.Pcc.Stream.Relative.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Stream.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:HARQ:STReam{streamCmdVal}:TRANsmission:ABSolute
				RsCmwLteSig_ExtendedBler_Pcc_Harq_Stream_Transmission_Absolute.Fetch_Data value = driver.ExtendedBler.Pcc.Harq.Stream.Transmission.Absolute.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Harq.Stream.Transmission.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:HARQ:STReam{streamCmdVal}:TRANsmission:RELative
				RsCmwLteSig_ExtendedBler_Pcc_Harq_Stream_Transmission_Relative.Fetch_Data value = driver.ExtendedBler.Pcc.Harq.Stream.Transmission.Relative.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Harq.Stream.Transmission.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:HARQ:STReam{streamCmdVal}:SUBFrame:ABSolute
				RsCmwLteSig_ExtendedBler_Pcc_Harq_Stream_Subframe_Absolute.Fetch_Data value = driver.ExtendedBler.Pcc.Harq.Stream.Subframe.Absolute.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Harq.Stream.Subframe.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:HARQ:STReam{streamCmdVal}:SUBFrame:RELative
				RsCmwLteSig_ExtendedBler_Pcc_Harq_Stream_Subframe_Relative.Fetch_Data value = driver.ExtendedBler.Pcc.Harq.Stream.Subframe.Relative.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.Harq.Stream.Subframe.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:CQIReporting:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Pcc_CqiReporting_Stream.Fetch_Data value = driver.ExtendedBler.Pcc.CqiReporting.Stream.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Pcc.CqiReporting.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:RI
				List<int> value = driver.ExtendedBler.Pcc.Ri.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:PMI:RI<no>
				List<int> value = driver.ExtendedBler.Pcc.Pmi.Ri.Fetch(ReliabilityIndicatorNoRepCap.Default);
				value = driver.ExtendedBler.Pcc.Pmi.Ri.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer[:PCC]:UPLink
				RsCmwLteSig_ExtendedBler_Pcc_Uplink.Fetch_Data value = driver.ExtendedBler.Pcc.Uplink.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:CONFidence
				ConfidenceEnum value = driver.ExtendedBler.Scc.Confidence.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Scc.Confidence.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:ABSolute
				RsCmwLteSig_ExtendedBler_Scc_Absolute.Fetch_Data value = driver.ExtendedBler.Scc.Absolute.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Scc.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:RELative
				RsCmwLteSig_ExtendedBler_Scc_Relative.Fetch_Data value = driver.ExtendedBler.Scc.Relative.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Scc.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:STReam{streamCmdVal}:ABSolute
				RsCmwLteSig_ExtendedBler_Scc_Stream_Absolute.Fetch_Data value = driver.ExtendedBler.Scc.Stream.Absolute.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Stream.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:STReam{streamCmdVal}:RELative
				RsCmwLteSig_ExtendedBler_Scc_Stream_Relative.Fetch_Data value = driver.ExtendedBler.Scc.Stream.Relative.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Stream.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:HARQ:STReam{streamCmdVal}:TRANsmission:ABSolute
				RsCmwLteSig_ExtendedBler_Scc_Harq_Stream_Transmission_Absolute.Fetch_Data value = driver.ExtendedBler.Scc.Harq.Stream.Transmission.Absolute.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Harq.Stream.Transmission.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:HARQ:STReam{streamCmdVal}:TRANsmission:RELative
				RsCmwLteSig_ExtendedBler_Scc_Harq_Stream_Transmission_Relative.Fetch_Data value = driver.ExtendedBler.Scc.Harq.Stream.Transmission.Relative.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Harq.Stream.Transmission.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:HARQ:STReam{streamCmdVal}:SUBFrame:ABSolute
				RsCmwLteSig_ExtendedBler_Scc_Harq_Stream_Subframe_Absolute.Fetch_Data value = driver.ExtendedBler.Scc.Harq.Stream.Subframe.Absolute.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Harq.Stream.Subframe.Absolute.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:HARQ:STReam{streamCmdVal}:SUBFrame:RELative
				RsCmwLteSig_ExtendedBler_Scc_Harq_Stream_Subframe_Relative.Fetch_Data value = driver.ExtendedBler.Scc.Harq.Stream.Subframe.Relative.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.Harq.Stream.Subframe.Relative.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:CQIReporting:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Scc_CqiReporting_Stream.Fetch_Data value = driver.ExtendedBler.Scc.CqiReporting.Stream.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Scc.CqiReporting.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:RI
				List<int> value = driver.ExtendedBler.Scc.Ri.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Scc.Ri.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:PMI:RI<no>
				List<int> value = driver.ExtendedBler.Scc.Pmi.Ri.Fetch(SecondaryCompCarrierRepCap.Default, ReliabilityIndicatorNoRepCap.Default);
				value = driver.ExtendedBler.Scc.Pmi.Ri.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:UPLink
				RsCmwLteSig_ExtendedBler_Scc_Uplink.Fetch_Data value = driver.ExtendedBler.Scc.Uplink.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Scc.Uplink.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput:ALL
				RsCmwLteSig_ExtendedBler_Trace_Throughput_All.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.All.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput[:PCC]
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Pcc.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Pcc.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput[:PCC]:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Pcc_Stream.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Pcc.Stream.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.Throughput.Pcc.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput[:PCC]:MCQI:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Pcc_Mcqi_Stream.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Pcc.Mcqi.Stream.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.Throughput.Pcc.Mcqi.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput:SCC<Carrier>
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Scc.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Scc.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.ExtendedBler.Trace.Throughput.Scc.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput:SCC<Carrier>:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Scc_Stream.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Scc.Stream.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.Throughput.Scc.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:THRoughput:SCC<Carrier>:MCQI:STReam{streamCmdVal}
				RsCmwLteSig_ExtendedBler_Trace_Throughput_Scc_Mcqi_Stream.Fetch_Data value = driver.ExtendedBler.Trace.Throughput.Scc.Mcqi.Stream.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.Throughput.Scc.Mcqi.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:CQIReporting[:PCC]:STReam{streamCmdVal}
				List<double> value = driver.ExtendedBler.Trace.CqiReporting.Pcc.Stream.Fetch(StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.CqiReporting.Pcc.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:TRACe:CQIReporting:SCC<Carrier>:STReam{streamCmdVal}
				List<double> value = driver.ExtendedBler.Trace.CqiReporting.Scc.Stream.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.ExtendedBler.Trace.CqiReporting.Scc.Stream.Fetch();
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:STATe
				ResourceStateEnum value = driver.ExtendedBler.State.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:EBLer:STATe:ALL
				RsCmwLteSig_ExtendedBler_State_All.Fetch_Data value = driver.ExtendedBler.State.All.Fetch();				
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:ALL:ABSolute
				RsCmwLteSig_Intermediate_ExtendedBler_All_Absolute.Fetch_Data value = driver.Intermediate.ExtendedBler.All.Absolute.Fetch();				
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:ALL:RELative
				RsCmwLteSig_Intermediate_ExtendedBler_All_Relative.Fetch_Data value = driver.Intermediate.ExtendedBler.All.Relative.Fetch();				
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer[:PCC]:ABSolute
				RsCmwLteSig_Intermediate_ExtendedBler_Pcc_Absolute.Fetch_Data value = driver.Intermediate.ExtendedBler.Pcc.Absolute.Fetch();				
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer[:PCC]:RELative
				RsCmwLteSig_Intermediate_ExtendedBler_Pcc_Relative.Fetch_Data value = driver.Intermediate.ExtendedBler.Pcc.Relative.Fetch();				
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer[:PCC]:STReam{streamCmdVal}:ABSolute
				RsCmwLteSig_Intermediate_ExtendedBler_Pcc_Stream_Absolute.Fetch_Data value = driver.Intermediate.ExtendedBler.Pcc.Stream.Absolute.Fetch(StreamRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Pcc.Stream.Absolute.Fetch();
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer[:PCC]:STReam{streamCmdVal}:RELative
				RsCmwLteSig_Intermediate_ExtendedBler_Pcc_Stream_Relative.Fetch_Data value = driver.Intermediate.ExtendedBler.Pcc.Stream.Relative.Fetch(StreamRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Pcc.Stream.Relative.Fetch();
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:ABSolute
				RsCmwLteSig_Intermediate_ExtendedBler_Scc_Absolute.Fetch_Data value = driver.Intermediate.ExtendedBler.Scc.Absolute.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Scc.Absolute.Fetch();
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:RELative
				RsCmwLteSig_Intermediate_ExtendedBler_Scc_Relative.Fetch_Data value = driver.Intermediate.ExtendedBler.Scc.Relative.Fetch(SecondaryCompCarrierRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Scc.Relative.Fetch();
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:STReam{streamCmdVal}:ABSolute
				RsCmwLteSig_Intermediate_ExtendedBler_Scc_Stream_Absolute.Fetch_Data value = driver.Intermediate.ExtendedBler.Scc.Stream.Absolute.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Scc.Stream.Absolute.Fetch();
			}
			{	// FETCh:INTermediate:LTE:SIGNaling<instance>:EBLer:SCC<Carrier>:STReam{streamCmdVal}:RELative
				RsCmwLteSig_Intermediate_ExtendedBler_Scc_Stream_Relative.Fetch_Data value = driver.Intermediate.ExtendedBler.Scc.Stream.Relative.Fetch(SecondaryCompCarrierRepCap.Default, StreamRepCap.Default);
				value = driver.Intermediate.ExtendedBler.Scc.Stream.Relative.Fetch();
			}
			{	// STOP:LTE:SIGNaling<instance>:THRoughput
				driver.Throughput.Stop();
				driver.Throughput.StopAndWait();
			}
			{	// ABORt:LTE:SIGNaling<instance>:THRoughput
				driver.Throughput.Abort();
				driver.Throughput.AbortAndWait();
			}
			{	// INITiate:LTE:SIGNaling<instance>:THRoughput
				driver.Throughput.Initiate();
				driver.Throughput.InitiateAndWait();
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput
				RsCmwLteSig_Throughput.ResultData value = driver.Throughput.Fetch();				
			}
			{	// READ:LTE:SIGNaling<instance>:THRoughput
				RsCmwLteSig_Throughput.ResultData value = driver.Throughput.Read();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:STATe
				ResourceStateEnum value = driver.Throughput.State.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:STATe:ALL
				RsCmwLteSig_Throughput_State_All.Fetch_Data value = driver.Throughput.State.All.Fetch();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Fetch();				
			}
			{	// READ:LTE:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Current.Read();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Fetch();				
			}
			{	// READ:LTE:SIGNaling<instance>:THRoughput:TRACe:DL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Downlink.Pdu.Average.Read();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Fetch();				
			}
			{	// READ:LTE:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:CURRent
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Current.Read();				
			}
			{	// FETCh:LTE:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Fetch();				
			}
			{	// READ:LTE:SIGNaling<instance>:THRoughput:TRACe:UL:PDU:AVERage
				List<double> value = driver.Throughput.Trace.Uplink.Pdu.Average.Read();				
			}
		}
	}
}