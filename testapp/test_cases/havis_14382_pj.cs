using AntdUI;
using HslCommunication.Enthernet.Redis;
using MathNet.Numerics;
using NationalInstruments.DataInfrastructure;
using NationalInstruments.Restricted;
using NativeUsbLib;
using RohdeSchwarz.RsCmwLteSig;
using SharpExModule;
using SLCANWithEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
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
    public class havis_project : IDefaultAction, IDisposable
    {
        string cmd_str = "D:\\HAVIS_TEST\\ETHERNET\\WIN_USB_PGTOOL_v2.0.20_V4\\WIN_USB_PGTOOL_v2.0.20\\x64\\RTUNicPG64.exe";
       
        testcase_dll tc;
        string id = "";
        private volatile int got_flog = 0;

        public havis_project(testcase_dll _tc)
        {


            tc = _tc;

            add_func_to_libs();
        }


        public void add_func_to_libs()
        {
            //id = this.GetType().Name;
            id = "havis_";
            tc.funcs.Add(id + "test_cfg_gen", test_cfg_gen);
            tc.funcs.Add(id + "verify_hub_DeviceRemovable_status", verify_hub_DeviceRemovable_status);
            tc.funcs.Add(id + "phy_chip_efase_porg", phy_chip_efase_porg);
            tc.funcs.Add(id + "get_network_interface_speed", get_network_interface_speed);
            tc.funcs.Add(id + "get_network_interface_MAC", get_network_interface_MAC);
            tc.funcs.Add(id + "verify_hub_Device_prog", verify_hub_Device_prog);
            tc.funcs.Add(id + "sbu_voltage_check", sbu_voltage_check);
            tc.funcs.Add(id + "usb_analyzer_test", usb_analyzer_test);
            tc.funcs.Add(id + "run_no_return", run_no_return);
            tc.funcs.Add(id + "status_analyzer_test", status_analyzer_test);
        }

        private string verify_hub_DeviceRemovable_status(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {

                c = testapp.mylib.usb_state_check.usb_check();
                if (c.IndexOf("0x70") > 0)
                {
                    return "pass";
                }
                else {
                    if (c == "")
                    {
                        c = "get_empty";
                        c = c.Replace("\r\n", "").Trim();
                    }
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

        private string verify_hub_Device_prog(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {

                c = testapp.mylib.usb_state_check.usb_check();
                if (c.IndexOf("0x70") > 0)
                {
                    c = "Previously programmed ";
                    return "pass";
                }
                else
                {
                    string[] scriptnames = d.Split(";".ToArray());
                    int ds = 1;
                    string reg = "";
                    if (scriptnames.Length == 2) { d = scriptnames[0]; reg = scriptnames[1]; }
                    string m = new piprun(d, "").getruninfofromwhile(1);
                    string rs = mylib.utility_func._findstr_regex(reg, m);
                    if (rs.IndexOf("pass") >= 0)
                    {
                        c = "TUSB8040_driver_ok";
                        return "pass";
                    }
                    return "fail";

                }

            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            finally {

                mylib.utility_func.killproc("TUSB8040 EEPROM Programmer");

            }
            return "fail";
        }

        private string run_no_return(string a, string b, out string c, string d) {

            c = "fail";
            try
            {

                c = testapp.mylib.usb_state_check.usb_check();
                if (c.IndexOf("0x70") > 0)
                {
                    c = "Previously programmed ";
                    return "pass";
                }
                else
                {

                    if (tc.funcs["run_no_return"](a, b, out c, d).ToUpper().Trim() == "pass".ToUpper()) { 
                    
                    return "pass";
                    }
                    
                    return "fail";

                }

            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";

            }
            finally
            {

                mylib.utility_func.killproc("TUSB8040 EEPROM Programmer");

            }
            return "fail";

        }
        private string test_cfg_gen(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {

                var sn_mac = testapp.mylib.CfgGenerator.ReadSnMac();

                mylib.utility_func.callbackdebuginfo(sn_mac.ToString());
                testapp.mylib.CfgGenerator.GenerateCfg("output.cfg", sn_mac.mac, "00 E0 4C 68 00 01", "00 E0 4C 68 FF FF", "12345678");

                testapp.mylib.CfgGenerator.IncreaseSnMac();
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

        /// <summary>
        /// 检测 USB Type-C SBU1/SBU2 电压：用继电器切换通道，dm3058_ac_read_200v 读取 AC 电压。
        /// 判断逻辑：一个通道 ≈5V，另一个 ≈0V（两种接法均可），否则 Fail。
        /// a: 5V 通道上限 (如 "5.2")
        /// b: 5V 通道下限 (如 "4.8")
        /// c: out — "VA=x.xxx;VB=x.xxx" 或 "fail;..."
        /// d: "relayA/relayB/5V_max,5V_min/0V_max,0V_min"
        ///    示例: "33/2/5.2,4.8/0.1,-0.1"
        /// </summary>
        private string sbu_voltage_check(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                // ── 解析 d ────────────────────────────────
                // "3/2/5.2,4.8/0.1,-0.1"
                string[] parts = d.Split('/');
                if (parts.Length < 4)
                {
                    c = "fail;d_param_invalid";
                    return "fail";
                }
                string relayA = parts[0].Trim();   // "3"
                string relayB = parts[1].Trim();   // "2"

                string[] v5_range = parts[2].Split(',');  // ["5.2", "4.8"]
                string[] v0_range = parts[3].Split(',');  // ["0.1", "-0.1"]

                if (v5_range.Length < 2 || v0_range.Length < 2)
                {
                    c = "fail;range_param_invalid";
                    return "fail";
                }

                string v5_max = v5_range[0].Trim(); // 5V 上限
                string v5_min = v5_range[1].Trim(); // 5V 下限
                string v0_max = v0_range[0].Trim(); // 0V 上限
                string v0_min = v0_range[1].Trim(); // 0V 下限

                double d_v5_max = double.Parse(v5_max);
                double d_v5_min = double.Parse(v5_min);
                double d_v0_max = double.Parse(v0_max);
                double d_v0_min = double.Parse(v0_min);

                // ── 读通道 A ──────────────────────────────
                tc.Getfun()["relay_set"]("", "", out _, $"#{relayA}:1#");
                Thread.Sleep(200);
                string readingA = "";
                tc.funcs["dm3058_ac_read_200v"]("9999", "-9999", out readingA, "");
                tc.Getfun()["relay_set"]("", "", out _, $"#{relayA}:0#");
                Thread.Sleep(100);

                double va;
                if (!double.TryParse(readingA, out va))
                {
                    c = $"fail;channel_A_read_error:{readingA}";
                    return "fail";
                }

                // ── 读通道 B ──────────────────────────────
                tc.Getfun()["relay_set"]("", "", out _, $"#{relayB}:1#");
                Thread.Sleep(200);
                string readingB = "";
                tc.funcs["dm3058_ac_read_200v"]("9999", "-9999", out readingB, "");
                tc.Getfun()["relay_set"]("", "", out _, $"#{relayB}:0#");
                Thread.Sleep(100);

                double vb;
                if (!double.TryParse(readingB, out vb))
                {
                    c = $"fail;channel_B_read_error:{readingB}";
                    return "fail";
                }

                utility_func.callbackdebuginfo(
                    $"[havis] sbu_voltage_check: VA={va:F3}V, VB={vb:F3}V");

                // ── 判断 ──────────────────────────────────
                // 情形1: A = 5V, B = 0V
                bool a_is_5v = va >= d_v5_min && va <= d_v5_max;
                bool b_is_0v = vb >= d_v0_min && vb <= d_v0_max;

                // 情形2: A = 0V, B = 5V
                bool a_is_0v = va >= d_v0_min && va <= d_v0_max;
                bool b_is_5v = vb >= d_v5_min && vb <= d_v5_max;

                if ((a_is_5v && b_is_0v) || (a_is_0v && b_is_5v))
                {
                    c = $"VA={va:F3};VB={vb:F3}";
                    return "pass";
                }

                // ── 不满足条件 ─────────────────────────────
                c = $"fail;VA={va:F3};VB={vb:F3}";
                return "fail";
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[havis] sbu_voltage_check error: {ex.Message}");
                c = $"fail;{ex.Message}";
                return "fail";
            }
        }
        private string status_analyzer_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string rsu = "";
                var spstr = d.Split('/');

                mylib.utility_func.callbackdebuginfo("======检测步骤1========");
                if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[0]) == "pass")/*输出端USB2.0光纤*/
                {
                    mylib.utility_func.callbackdebuginfo("=====输出端正面USB2.0亮，则判断输出的两个USB3.0=========");
                    if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[1]) == "pass" && /*输出端正面USB3.0光纤*/
                        tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[2]) == "pass" &&/*单独输出端USB3.0光纤*/
                        tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[3]) == "pass"   /* 排针USB2.0 光纤*/
                        )
                    {

                        if ("fail" == tc.funcs["picture_show"]("pass", "pass", out _, $"__;Please reverse the USB port on the output side.;{spstr[spstr.Length - 1]}"))
                        {

                            c = "Reverse USB Fail";
                            return "fail";
                        }
                        for (int z = 0; z < 3; z++)
                        {
                            mylib.utility_func.callbackdebuginfo("=====反转输出端TYPEC ========");
                            if (tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[0]) == "pass")/*输出端USB2.0光纤*/
                            {

                                tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:1#");
                                mylib.utility_func.callbackdebuginfo("=====将检测脚是能 ========");
                                Thread.Sleep(500);

                                if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[3]) == "pass" &&  /* 排针USB2.0 光纤*/
                                    tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[1]) == "pass")/*输出端正面USB3.0光纤*/
                                {
                                    c = "check Status OK";
                                    tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                                    return "pass";

                                }
                                else
                                {
                                    tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                                    Thread.Sleep(1500);
                                    continue;


                                }
                            }
                        }

                        c = "check Status NG";
                        tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                        return "fail";
                    }


                }
                else
                {

                    mylib.utility_func.callbackdebuginfo("=====输出端正面USB2.0不亮，先判断输出的两个USB3.0=========");
                    if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[1]) == "pass" && /*输出端USB3.0光纤*/
                        tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[2]) == "pass" &&/*单独输出端USB3.0光纤*/
                        tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[3]) == "pass"   /* 排针USB2.0 光纤*/
                        )
                    {
                        for (int p = 0; p < 2; p++)
                        {
                            tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:1#");
                            mylib.utility_func.callbackdebuginfo("=====将检测脚是能 ========");
                            Thread.Sleep(500);

                            if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[3]) == "pass" &&  /* 排针USB2.0 光纤*/
                                       tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[1]) == "pass") /*输出端正面USB3.0光纤*/
                            {


                                tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                                mylib.utility_func.callbackdebuginfo("=====将检测脚是非使能 ========");
                                Thread.Sleep(500);
                                if ("fail" == tc.funcs["picture_show"]("pass", "pass", out _, $"__;Please reverse the USB port on the output side.;{spstr[spstr.Length - 1]}"))
                                {

                                    c = "Reverse USB Fail";
                                    return "fail";
                                }

                                Thread.Sleep(2000);

                                if (tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[0]) == "pass" && /*输出端USB2.0光纤*/
                                    tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[1]) == "pass" && /*输出端USB3.0光纤*/
                                     tc.funcs["cloor_assy_adjustable"]("50;50;256;5000", "50;50;256;500", out _, spstr[2]) == "pass" &&/*单独输出端USB3.0光纤*/
                                     tc.funcs["cloor_assy_adjustable"]("10;10;10;10", "-1;-1;-1;-1", out _, spstr[3]) == "pass")   /* 排针USB2.0 光纤*/
                                {

                                    c = "check Status OK";

                                    return "pass";
                                }
                                else
                                {

                                    tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                                    continue;

                                }



                            }
                            else
                            {
                                tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                                continue;

                            }


                        }









                        c = "check Status NG";
                        tc.funcs["relay_set"]("pass", "pass", out _, $"#{spstr[4]}:0#");
                        return "fail";

                    }
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

        private string usb_analyzer_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                string rsu = "";
               var spstr =  d.Split('/');
                if (tc.funcs["cloor_assy_adjustable"](spstr[0], spstr[1], out rsu, spstr[2]) == "pass")
                {
                    mylib.utility_func.callbackdebuginfo(rsu);
                    c = "pass";
                    return "pass";

                }
                else {

                    mylib.utility_func.callbackdebuginfo(rsu);
                    return "fail";
                }
                    ;
                
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
        private string phy_chip_efase_porg(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {



                cmd_str = @"D:\WIN_USB_PGTOOL_v2.0.20_V4\WIN_USB_PGTOOL_v2.0.20\x64\RTUNicPG64.exe";

                string rsu_ouput = new piprun(cmd_str, "/efuse /r").getruninfofromwhile(1);
                if (rsu_ouput.IndexOf("Not Support This Adapter") >= 0 || rsu_ouput.IndexOf("Get Adapter Info Failed") >= 0)
                {
                    c = "cannot_find_phy_characteristic";
                    return "fail";
                }
                var rs = ExtractAndValidateMacAddress(rsu_ouput);
                c = rs.Item2;
                if (rs.Item1 > 0)
                {


                    return "pass";
                }
                else {


                    return "fail";
                }
                return "fail";



            }
            catch (Exception e)
            {
                mylib.utility_func.callbackdebuginfo(e.ToString());
                c = "error__";
                return "fail";

            }
            return "fail";
        }


        private string get_network_interface_speed(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {



                var checker = new NetworkUtils.NetworkAdapterSpeedChecker();

                var sp_str = checker.GetLinkSpeedByDescription("ASIX USB to Gigabit Ethernet Family Adapter", true);

                utility_func.callbackdebuginfo("test interface info : " + sp_str);
                c = sp_str.Trim();
                if (sp_str == "1.00 Gbps")
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
                c = "error__";
                return "fail";

            }
            return "fail";
        }

        private string get_network_interface_MAC(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {



                var checker = new NetworkUtils.NetworkAdapterSpeedChecker();

                var sp_str = checker.GetAdapterInfoByDescription("Realtek USB GbE Family Controller", false);

                // var sp_str = checker.GetAdapterInfoByDescription("Realtek USB GbE Family Controller", true);
                if (sp_str == null)
                {
                    c = "cannot_find_DUT";
                    return "fail";
                }
                utility_func.callbackdebuginfo("test interface info : \r" + sp_str.mac_add + " " + sp_str.Name);
                c = "'" + sp_str.mac_add;
                if (sp_str.mac_add.IndexOf(a.Replace("'", "")) >= 0)
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
                c = "error__";
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


        /// <summary>
        /// 从命令输出中提取并验证MAC地址
        /// </summary>
        /// <param name="commandOutput">命令输出内容</param>
        /// <returns>符合要求的MAC地址</returns>
        private (int, string) ExtractAndValidateMacAddress(string commandOutput)
        {
            // 正则表达式匹配MAC地址（XX:XX:XX:XX:XX:XX格式）
            string pattern = @"Realtek USB GbE Family Controller\s*(?:#\d+)?\s*\(([0-9A-Fa-f:]{17})\)\s*-\s*RTL8153BvB";
            Match match = Regex.Match(commandOutput, pattern);

            if (match.Success)
            {

                string mac = match.Groups[1].Value;
                if (mac.StartsWith("00:EF:9C"))
                {
                    utility_func.callbackdebuginfo("MAC :" + mac);
                    return (1, mac);
                }
                else
                {


                    string efuseResult = new piprun(cmd_str, "/efuse").getruninfofromwhile(1);


                    string str_id_reg = @"^\s*EFUSE_NODEID\s*=\s*(([0-9A-Fa-f]{2})\s*){6}$";
                    Match match_prog = Regex.Match(efuseResult, str_id_reg, RegexOptions.Multiline);

                    if (!match.Success)
                    {
                        utility_func.callbackdebuginfo("program efase failed");
                        return (-1, "program_efase_failed");
                    }

                    // 提取所有两位十六进制数（忽略空格）
                   string macParts = match_prog.Value.Substring("EFUSE_NODEID=".Length).Trim();


                 

                    // 拼接为标准MAC格式（无空格，用:分隔）
                    return (2, macParts.Replace(" ",":"));

                }

            }else
            {
             
                utility_func.callbackdebuginfo("No valid MAC address found");
                return (-4, "No valid MAC address found");
            } 

                
                
          

      
        }
    }
}


