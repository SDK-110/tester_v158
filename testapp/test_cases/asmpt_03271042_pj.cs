using HslCommunication.Enthernet.Redis;
using MathNet.Numerics;
using NationalInstruments.DataInfrastructure;
using NationalInstruments.Restricted;
using SharpExModule;
using SLCANWithEvents;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using testapp.glob_set;
using testapp.mylib;

namespace testapp.test_cases
{
    public class asmpt_03271042 : IDefaultAction, IDisposable
    {
     
        testcase_dll tc;
        string id = "";
       private volatile int got_flog = 0;
      asmpt_03271042_U10_Write u10_Writer = new asmpt_03271042_U10_Write();
        circular_data_serial asmpt_debug_port = null;
        public asmpt_03271042(testcase_dll _tc,string debug_port)
        {

            asmpt_debug_port = new circular_data_serial(debug_port);
            tc = _tc;
           
            add_func_to_libs();
        }

   
        public void add_func_to_libs()
        {
            //id = this.GetType().Name;
            id = "asmpt_main_";
            tc.funcs.Add(id + "save_U10_data", save_U10_data);
            tc.funcs.Add(id + "u10_write_prg", run_u10_write_prg);
            tc.funcs.Add(id + "programm_u3", programm_u3);
            tc.funcs.Add(id + "u10_read_prg", run_u10_read_prg);
            tc.funcs.Add(id + "debug_info_clean", debug_port_math_clean);
            tc.funcs.Add(id + "debug_info_math", debug_port_math);
            tc.golb_var_default["braking_pcba_tp25"] = "-100";
        }

        private string save_U10_data(string a, string b, out string c, string d)
        {
            c="fail";
            try
            {
                string proj = glob_ini_instance.getInstance().getSetupIniData["setproduct"]["project"];
                if (proj.Length != 19) {

                    mylib.utility_func.callbackdebuginfo("订单信息错误");
                    return "fail";
                
                }
                if (int.TryParse(proj.Substring(proj.Length - 2, 2), out _) == false) {

                    mylib.utility_func.callbackdebuginfo("Revision_status format error in project name");
                    return "fail";
                }
                tc.golb_var_default["asmpt_1042_sn"] = "";
                tc.golb_var_default["asmpt_1042_Revision_Status"] = "";
                if (!tc.trf.StartsWith("1P") || tc.trf.Length != 28) {

                    c = "SN_Format_Error";
                    return "fail";
                }
                string fs = tc.trf.Substring(11, 2);
                if (int.TryParse(fs, out _) == false) {

                    c = "Functional_Ver_Error";
                    return "fail";
                }
                string _year = tc.trf.Substring(19, 1);
                string _month = tc.trf.Substring(20, 1);
                if (!(_year.IndexOfAny(new char[] { 'T', 'U', 'V', 'W', 'S' }) >= 0 &&
                     _month.IndexOfAny(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', 'O', 'N', 'D' }) >= 0)

                    ) {

                    c = "Date_Error";
                    return "fail";
                }
                string day = tc.trf.Substring(21, 2);
                string _sn = tc.trf.Substring(24, 4);
                if (int.Parse(_sn) > 999) {

                    c = "SN_Cout_Error";
                    return "fail";
                }
                string mac_base = (int.Parse(_sn) + 1000 - 1) + "";
                u10_Writer.SetFields(mac_base: mac_base, serial: _sn, _year,  _month, day, fs:fs,rs: proj.Substring(proj.Length - 2, 2));
                tc.golb_var_default["asmpt_1042"] = _sn;
                u10_Writer.Save(d);
                c = "pass";
                return "pass";
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            
            }
            return "fail";
        }
        private string debug_port_math_clean(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "") d = "Debug";
            try
            {
              
                asmpt_debug_port.match = false;
                asmpt_debug_port.str_mat = d;
                c = "pass";
                return "pass";
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            return "fail";
        }

        private string debug_port_math(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "") d = "Debug";
            try
            {
                Thread.Sleep(2000);
                if (asmpt_debug_port.match == true)
                {
                    c = "pass";
                    return "pass";
                }
                else
                {

                    return "fail";
                }
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            return "fail";
        }
        private string run_u10_write_prg(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "") d = @"D:\my_workspac\temp\U10_PROG\TUV_U3_U10_Programming_Script\Write_MRAM_Data_U10_v02\Write_MRAM_Data.exe";
            try
            {
               var rsu =  mylib.utility_func.run_console_pip(d,10000);
                if (rsu.ToString().IndexOf("-------EEPROM Data Retrieved successfully: START-------") >= 0) {
                    c = rsu.ToString().Substring(rsu.ToString().IndexOf("-------EEPROM Data Retrieved successfully: START-------")).Replace(","," ");

                }
                string _sn_tmp = (string)tc.golb_var_default["asmpt_1042"];
                if (c.IndexOf($"Serial No: {_sn_tmp.PadLeft(6,'0')}") >=0 &&

                    c.IndexOf($"Revision Status: {"01"}")>=0)

                    
                {
                   
                    return "pass";

                }
                else
                {
                    return "fail";
                }

                    return "fail";
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            return "fail";
        }

        private string run_u10_read_prg(string a, string b, out string c, string d)
        {
            c = "fail";
            if (d == "") d = @"D:\my_workspac\temp\U10_PROG\TUV_U3_U10_Programming_Script\Read_MRAM_Data_U10_v02\Read_MRAM_Data.exe";
            try
            {
                var rsu = mylib.utility_func.run_console_pip(d, 10000);
                int start = rsu.ToString().IndexOf("-------EEPROM Data Retrieved successfully: START-------");
                int end = rsu.ToString().IndexOf("-------EEPROM Data Retrieved successfully: END-------");
                if (start >= 0 && end >= 0 && start<end)
                {

                    c = rsu.ToString().Substring(start,end - start).Replace(",", " ");

                }
                else {

                    c = "rev error";
                    return "fail";
                
                }
                    string _sn_tmp = (string)tc.golb_var_default["asmpt_1042"];
                if (c.IndexOf($"Serial No: {_sn_tmp.PadLeft(6, '0')}") >= 0 &&

                    c.IndexOf($"Revision Status: {"01"}") >= 0)


                {

                    return "pass";

                }
                else
                {
                    return "fail";
                }

                return "fail";
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            return "fail";
        }
        private string programm_u3(string a, string b, out string c, string d) {

            c = "fail";
            if (d == "") d = @"D:\my_workspac\temp\U10_PROG\TUV_U3_U10_Programming_Script\TUV_manufacturing_package_U3_v02\TUV_manufacturing_package_v02\TUV_ProductionProgrammer.bat";
            try
            {
                var rsu = mylib.utility_func.run_console_pip(d);
                c = rsu.ToString();
                if (rsu.ToString().IndexOf(a.Trim()) >= 0)
                {
                    c = "pass";
                    return "pass";

                }
                else
                {
                    c = "blhost fial";
                    return "fail";
                }

                return "fail";
            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            return "fail";

        }





        public void InsertDefaultAction()
        {


            tc.dev_moren[id] = this;

        }

 

   
        private byte[] HexStringToByteArray(string hex)
        {
            hex = hex.Replace(" ", "");
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }

            return bytes;
        }

        // 辅助方法: 字节数组转十六进制字符串
        private string ByteArrayToHexString(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", " ");
        }

        public void set_default_set()
        {
            
        }

        public void Dispose()
        {
            try
            {

                tc.dev_moren.Remove(id);

            }
            catch (Exception ex)
            {
            }
        }
    }

}
