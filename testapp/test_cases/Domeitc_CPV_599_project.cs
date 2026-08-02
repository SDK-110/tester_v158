using DeviceLibrary;
using HslCommunication.Enthernet.Redis;
using NationalInstruments.DataInfrastructure;
using Org.BouncyCastle.Math.EC.Rfc7748;
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

namespace testapp.test_cases
{
    public class Domeitc_CPV_599_project : IDefaultAction, IDisposable
    {
        TH6300 ivps = null;
        testcase_dll tc;
        string id = "";
       private volatile int got_flog = 0;
        byte[] rsu_byts = null; 
        public Domeitc_CPV_599_project(testcase_dll _tc, ref TH6300 ivps)
        {
            // 初始化主串口
        

            tc = _tc;
            if(ivps == null)
            {
                if (glob_ini_instance.getInstance().getSetupIniData["setport"]["PSU6300_port"] == null) { throw new Exception("PSU6300_port null"); }
               string temp_port = glob_ini_instance.getInstance().getSetupIniData["setport"]["PSU6300_port"];
               int temp_port_baudrate = int.Parse(glob_ini_instance.getInstance().getSetupIniData["setport"]["PSU6300_baudrate"]);
               ivps =  new TH6300(temp_port, temp_port_baudrate);
             
            }
            this.ivps = ivps;
            Initialize();
        }

        public void Initialize()
        {
   
            add_func_to_libs();
        }

       

        public void add_func_to_libs()
        {
            
            //id = this.GetType().Name;
            id = "dometic_599_";
         
            tc.funcs.Add(id + "power_on_test", power_on_test);
            tc.funcs.Add(id + "power_off_test", power_off_test);
            tc.funcs.Add(id + "power_on_test_search", power_on_test_search);
            tc.funcs.Add(id + "power_off_test_search", power_off_test_search);
            tc.funcs.Add(id + "verify_and_program", verify_and_program);
            tc.funcs.Add(id + "set_poewer_cur_vol", set_poewer_cur_vol);
            tc.funcs.Add(id + "get_poewer_cur", get_poewer_cur);
            tc.funcs.Add(id + "SetStandbyMode", SetStandbyMode);
            tc.funcs.Add(id + "SwitchFridgeMode", SwitchFridgeMode);
            tc.funcs.Add(id + "Get_Current_by_DMM", Get_Current_by_DMM);
            tc.funcs.Add(id + "get_led_color", get_led_color);
            tc.golb_var_default["braking_pcba_tp25"] = "-100";
        }


        private string set_poewer_cur_vol(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();
                if (ivps.set_vol_cur(double.Parse(d.Split(';')[0]), double.Parse(d.Split(';')[1])) == 1)
                {
                    if (1 == ivps.set_on_off(1))
                    {
                        c = "pass";
                        return "pass";
                    }
                    else
                    {

                        c = "DC_power_error2";
                        return "fail";
                    }

                }
                else
                {
                    c = "DC_Power_error1";
                    return "fail";
                }
            }
            catch (Exception e)
            {

                mylib.utility_func.callbackdebuginfo("set_poewer_cur_vol:" + e.Message);

            }


            return "fail";

        }


        private string get_led_color(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {

                if (tc.funcs["cloor_assy"](d.Split('/')[0].Split(',')[0], d.Split('/')[0].Split(',')[1], out _, d.Split('/')[0].Split(',')[2]) == "pass" &&
                                  tc.funcs["cloor_assy"](d.Split('/')[1].Split(',')[0], d.Split('/')[1].Split(',')[1], out _, d.Split('/')[1].Split(',')[2]) == "pass")

                {

                    c = "pass";

                    return "pass";



                }
                else { 
                
                return "fail";

                }
            }
            catch (Exception e)
            {

                mylib.utility_func.callbackdebuginfo("color analyzer error:" + e.Message);

            }


            return "fail";

        }
        private string get_poewer_cur(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();
                double cur = double.NaN;
                double.TryParse(ivps.getCurrent(), out cur);
                c = cur + "";
                if (double.Parse(a) >= cur && double.Parse(b) <= cur)
                {



                    return "pass";
                }
                else
                {
                    c = "DC_Power_error";
                    return "fail";
                }
            }
            catch (Exception e)
            {
                c = "error";
                mylib.utility_func.callbackdebuginfo("get_poewer_cur : " + e.Message);

            }


            return "fail";

        }

        private string SetStandbyMode(string a, string b, out string c, string d)
        {
            c = "fail";

            try
            {
                int cont = 0;
                for (int i = 0; i < 5; i++)
                {
                    cont = i;
                    if (tc.funcs["cloor_assy_Min"]("10;10;10;10", "-1;-1;-1;-1", out _, d.Split('/')[0]) == "pass" ||
                       tc.funcs["cloor_assy_Min"]("256;256;256'5000", "200;200;200;500", out _, d.Split('/')[1]) == "pass")
                    {

                        break;
                    }
                    else
                    {
                        tc.funcs["sk_relay1_set"]("pass", "pass", out _, d.Split('/')[2]);
                        Thread.Sleep(200);
                        tc.funcs["sk_relay1_set"]("pass", "pass", out _, d.Split('/')[3]);
                    }

                }

            }
            catch (Exception e)
            {

                mylib.utility_func.callbackdebuginfo("SetStandbyMode: " + e.Message);

            }


            return "fail";

        }

        private string SwitchFridgeMode(string a, string b, out string c, string d)
        {
            c = "fail";

            try
            {
                int cont = 0;
                for (int i = 0; i < 2; i++)
                {

                    if (tc.funcs["cloor_assy"](d.Split('/')[0].Split(',')[0], d.Split('/')[0].Split(',')[1], out _, d.Split('/')[0].Split(',')[2]) == "pass" ||
                       tc.funcs["cloor_assy"](d.Split('/')[1].Split(',')[0], d.Split('/')[1].Split(',')[1], out _, d.Split('/')[1].Split(',')[2]) == "pass")
                    {
                        c = "pass";
                        return "pass";
                    }
                    else
                    {
                        for (int z = 0; z < 3; z++)
                        {
                            tc.funcs["sk_relay1_set"]("pass", "pass", out _, d.Split('/')[2]);
                            Thread.Sleep(200);
                            tc.funcs["sk_relay1_set"]("pass", "pass", out _, d.Split('/')[3]);
                            Thread.Sleep(200);
                        }
                    }

                }

            }
            catch (Exception e)
            {

                mylib.utility_func.callbackdebuginfo("SwitchFridgeMode:" + e.Message);

            }


            return "fail";

        }

        private string Get_Current_by_DMM(string a, string b, out string c, string d)
        {
            c = "fail";

            try
            {
                string cur = "";
                int cont = 0;
                do
                {



                    if (tc.funcs["md3058_read_DC_10A"]("15", "-1", out cur, "") == "pass")
                    {

                        mylib.utility_func.callbackdebuginfo("md3058_read_DC_10A: " + cur);

                        if (double.Parse(a) >= double.Parse(cur) && double.Parse(b) <= double.Parse(cur))
                        {
                            c = cur;
                            return "pass";
                        }


                    }


                    Thread.Sleep(1000);


                } while (cont++ < 30);



            }
            catch (Exception e)
            {

                mylib.utility_func.callbackdebuginfo("Get_Current_by_DMM :" + e.Message);

            }


            return "fail";

        }

        private string power_on_test(string a, string b, out string c, string d) {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();

                // d format: "device_standby_v,standby_sec,standby_limit,device_start_v,start_sec,start_threshold,wire_loss"
                // default:  "10.1,10,0.15,11.0,60,3.0,0.3"
                if (string.IsNullOrEmpty(d)) d = "10.4,10,0.15,11.0,60,3.0,0.01";
                string[] p = d.Trim().Split(",".ToArray());

                double dev_standby_v = double.Parse(p[0]);
                int standby_sec = int.Parse(p[1]);
                double standby_limit = double.Parse(p[2]);
                double dev_start_v = double.Parse(p[3]);
                int start_sec = int.Parse(p[4]);
                double start_threshold = double.Parse(p[5]);
                double wire_loss = double.Parse(p[6]);


                if (ivps.CarRefrigeratorPowerONTest(dev_standby_v, standby_sec, standby_limit, dev_start_v, start_sec, start_threshold, wire_loss))
                {

                    c = "pass";
                    return "pass";
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] power_on_test param error: {ex.Message}");
                return "fail";
            }

            return "fail";
        }

        private string power_off_test(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();

                // d format: "device_start_v,start_timeout,start_threshold,device_hold_v,hold_sec,hold_threshold,device_shutdown_v,shutdown_sec,shutdown_limit,wire_loss"
                // default:  "11.7,20,4.0,10.1,5,3.0,9.4,5,0.15,0.1"
                if (string.IsNullOrEmpty(d)) d = "11.7,20,3.0,10.1,5,3.0,9.4,5,0.15,0.1";
                string[] p = d.Trim().Split(",".ToArray());

                double dev_start_v = double.Parse(p[0]);
                int start_timeout = int.Parse(p[1]);
                double start_threshold = double.Parse(p[2]);
                double dev_hold_v = double.Parse(p[3]);
                int hold_sec = int.Parse(p[4]);
                double hold_threshold = double.Parse(p[5]);
                double dev_shutdown_v = double.Parse(p[6]);
                int shutdown_sec = int.Parse(p[7]);
                double shutdown_limit = double.Parse(p[8]);
                double wire_loss = double.Parse(p[9]);

                c = "fail";
                if (ivps.CarRefrigeratorPowerOFFTest(dev_start_v, start_timeout, start_threshold, dev_hold_v, hold_sec, hold_threshold, dev_shutdown_v, shutdown_sec, shutdown_limit, wire_loss))
                {
                    c="pass";

                    return "pass";
                }
                   
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] power_off_test param error: {ex.Message}");
                return "fail";
            }

            return "fail";
        }

        // ──────────────────────────────────────────────
        // 找寻法 — 开机电压测试
        // d format: "standby_limit,start_current,confirm_current,sample_count,sample_interval_ms,confirm_sec,range_min,range_max,wire_loss"
        // default:  "0.15,0.15,2.0,5,1000,20,10.4,11.0,0.05"
        // ──────────────────────────────────────────────
        private string power_on_test_search(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();

                if (string.IsNullOrEmpty(d)) d = "0.15,0.15,2.0,5,1000,40,10.4,11.0,0.1";
                string[] p = d.Trim().Split(",".ToArray());

                double standby_limit = double.Parse(p[0]);
                double start_current = double.Parse(p[1]);
                double confirm_current = double.Parse(p[2]);
                int sample_count = int.Parse(p[3]);
                int sample_interval_ms = int.Parse(p[4]);
                int confirm_sec = int.Parse(p[5]);
                double range_min = double.Parse(p[6]);
                double range_max = double.Parse(p[7]);
                double wire_loss = double.Parse(p[8]);

                double start_v = range_min - 0.1;   // 10.3V
                double end_v = range_max;            // 11.0V
                int steps = 7;
                double step_v = (end_v - start_v) / steps; // 0.1V
                int max_fail = 3;
                double found_v = 0; // 记录找到的启动电压，供 CONFIRM_PHASE 使用
                ivps.set_vol_cur(start_v, 10.0);
                ivps.set_on_off(1);
                Thread.Sleep(1000);
                // ── 逐档扫描 ──
                for (int step = 0; step <= steps; step++)
                {
                    double v = start_v + step_v * step;
                    // 最后一档强制到终点
                    if (step == steps) v = end_v;

                    double supply_v = v + wire_loss;
                    ivps.set_vol_cur(supply_v, 10.0);
                    Thread.Sleep(100);

                    int consecutive_fail = 0;

                    for (int s = 0; s < sample_count; s++)
                    {
                        Thread.Sleep(sample_interval_ms);
                        double cur = double.NaN;
                        if (!double.TryParse(ivps.getCurrent(), out cur))
                        {
                            consecutive_fail++;
                            if (consecutive_fail >= max_fail)
                            {
                                c = "fail;comm_error";
                                return "fail";
                            }
                            continue;
                        }
                        consecutive_fail = 0;

                        // 第一点(范围外)就启动 → NG
                        if (step == 0 && cur > start_current)
                        {
                            mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: start outside range at {v:F2}V, cur={cur:F3}A");
                            c = "fail;start_outside_range";
                            return "fail";
                        }

                        // 找到启动点
                        if (cur > start_current)
                        {
                            found_v = v;
                            mylib.utility_func.callbackdebuginfo($"[FridgeTest] start detected at {found_v:F2}V, cur={cur:F3}A");
                            goto CONFIRM_PHASE;
                        }
                    }
                }

                // 到终点都没超过阈值
                c = "fail;no_start";
                return "fail";

            CONFIRM_PHASE:
                // 保持当前电压 found_v，确认压缩机正常工作
                ivps.set_on_off(0);
                Thread.Sleep(1000);
                ivps.set_on_off(1);
                Thread.Sleep(2000);
                List<double> recent = new List<double>();
               
                for (int i = 0; i < confirm_sec; i++)
                {
                    Thread.Sleep(1000);
                    double cur = double.NaN;
                    if (!double.TryParse(ivps.getCurrent(), out cur))
                        continue;

                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] confirm sec {i + 1}: {cur:F3}A");

                    if (cur < standby_limit/1.3)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: current dropped to {cur:F3}A during confirm");
                        c = "fail;current_dropped";
                        return "fail";
                    }

                    recent.Add(cur);
                    if (recent.Count > 3) recent.RemoveAt(0);

                    // 有连续 3 次读数且平均值 > confirm_current
                    if (recent.Count == 3)
                    {
                        double avg = (recent[0] + recent[1] + recent[2]) / 3.0;
                        if (avg > confirm_current)
                        {
                            if (found_v >= range_min && found_v <= range_max)
                            {
                                mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: start at {found_v:F2}V, avg cur={avg:F3}A > {confirm_current}A");
                                c = "pass"+ $"@{found_v:F2}V";
                                return "pass";
                            }
                            else
                            {
                                mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: start voltage {found_v:F2}V out of range [{range_min},{range_max}]");
                                c = "fail;voltage_out_of_range " + $"{found_v:F2}V";
                                return "fail";
                            }
                        }
                    }
                }

                c = "fail;current_not_enough";
                return "fail";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] power_on_test_search error: {ex.Message}");
                return "fail";
            }
        }

        // ──────────────────────────────────────────────
        // 找寻法 — 关机电压测试
        // d format: "boost_v,boost_current,start_check_current,sample_count,sample_interval_ms,shutdown_current,drop_step_v,range_min,range_max,wire_loss"
        // default:  "12.0,3.0,3.0,5,1000,0.15,0.1,9.4,10.0,0.05"
        // ──────────────────────────────────────────────
        private string power_off_test_search(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!ivps.IsOpen)
                    ivps.Open();

                if (string.IsNullOrEmpty(d)) d = "12.0,3.0,3.0,5,1000,0.15,0.1,9.4,10.0,0.1";
                string[] p = d.Trim().Split(",".ToArray());

                double boost_v = double.Parse(p[0]);
                double boost_current = double.Parse(p[1]);
                double start_check_current = double.Parse(p[2]);
                int sample_count = int.Parse(p[3]);
                int sample_interval_ms = int.Parse(p[4]);
                double shutdown_current = double.Parse(p[5]);
                double drop_step_v = double.Parse(p[6]);
                double range_min = double.Parse(p[7]);
                double range_max = double.Parse(p[8]);
                double wire_loss = double.Parse(p[9]);

                int max_fail = 3;

                // ── Phase A: 启动压缩机 ──
                double supply_boost = boost_v + wire_loss;
                ivps.set_vol_cur(supply_boost, 10.0);
                ivps.set_on_off(1);
                Thread.Sleep(500);

                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase A: boost to {supply_boost:F2}V, wait for compressor start...");

                bool started = false;
                for (int i = 0; i < 60; i++)
                {
                    Thread.Sleep(1000);
                    double cur = double.NaN;
                    if (!double.TryParse(ivps.getCurrent(), out cur))
                        continue;

                    if (cur > boost_current)
                    {
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] compressor started at {supply_boost:F2}V, cur={cur:F3}A");
                        started = true;
                        break;
                    }
                }

                if (!started)
                {
                    ivps.set_on_off(0);
                    c = "fail;compressor_not_start";
                    return "fail";
                }

                // ── Phase B: 降到 10.1V, 确认电流正常 ──
                double start_v = range_max + 0.1; // 10.1V
                double supply_start_v = start_v + wire_loss;
                ivps.set_vol_cur(supply_start_v, 10.0);
                Thread.Sleep(500);

                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase B: drop to {supply_start_v:F2}V, check current...");

                int consecutive_fail = 0;
                for (int s = 0; s < sample_count; s++)
                {
                    Thread.Sleep(sample_interval_ms);
                    double cur = double.NaN;
                    if (!double.TryParse(ivps.getCurrent(), out cur))
                    {
                        consecutive_fail++;
                        if (consecutive_fail >= max_fail)
                        {
                            ivps.set_on_off(0);
                            c = "fail;comm_error";
                            return "fail";
                        }
                        continue;
                    }
                    consecutive_fail = 0;

                    mylib.utility_func.callbackdebuginfo($"[FridgeTest] {start_v:F2}V - check {s + 1}: {cur:F3}A");

                    if (cur < start_check_current)
                    {
                        ivps.set_on_off(0);
                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: current {cur:F3}A < {start_check_current}A at {start_v:F2}V");
                        c = "fail;current_dropped_at_start";
                        return "fail";
                    }
                }

                // ── Phase C: 降压扫描找关机点 ──
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] Phase C: scanning down from {start_v:F2}V to {range_min:F2}V...");

                for (double v = start_v; v >= range_min - 0.001; v -= drop_step_v)
                {
                    // 不降到 range_min 以下
                    if (v < range_min) v = range_min;

                    double supply_v = v + wire_loss;
                    ivps.set_vol_cur(supply_v, 10.0);
                    Thread.Sleep(100);
                    consecutive_fail = 0;

                    for (int s = 0; s < sample_count; s++)
                    {
                        Thread.Sleep(sample_interval_ms);
                        double cur = double.NaN;
                        if (!double.TryParse(ivps.getCurrent(), out cur))
                        {
                            consecutive_fail++;
                            if (consecutive_fail >= max_fail)
                            {
                                ivps.set_on_off(0);
                                c = "fail;comm_error";
                                return "fail";
                            }
                            continue;
                        }
                        consecutive_fail = 0;

                        mylib.utility_func.callbackdebuginfo($"[FridgeTest] {v:F2}V - sec {s + 1}: {cur:F3}A");

                        if (cur < shutdown_current)
                        {
                            ivps.set_on_off(0);
                            if (v >= range_min && v <= range_max)
                            {
                                mylib.utility_func.callbackdebuginfo($"[FridgeTest] OK: shutdown at {v:F2}V, cur={cur:F3}A < {shutdown_current}A");
                                c = $"pass@{v:F2}V";
                                return "pass";
                            }
                            else
                            {
                                mylib.utility_func.callbackdebuginfo($"[FridgeTest] NG: shutdown at {v:F2}V out of range [{range_min},{range_max}]");
                                c = $"fail;shutdown_voltage_out_of_range {v:F2}V";
                                return "fail";
                            }
                        }
                    }

                    // 如果已到 range_min，不再继续
                    if (Math.Abs(v - range_min) < 0.001)
                        break;
                }

                ivps.set_on_off(0);
                c = "fail;no_shutdown";
                return "fail";
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"[FridgeTest] power_off_test_search error: {ex.Message}");
                try { ivps.set_on_off(0); } catch { }
                return "fail";
            }
        }

        /// <summary>
        /// 两阶段校验-编程流程（通过 d 参数传递 expectedChecksum 和 HEX 路径）。
        /// d 格式: "&lt;expected_checksum&gt;,&lt;hex_file_path&gt;"
        /// 示例:   "A1B2C3D4,C:\hex\firmware.hex"
        /// 返回（out c）: "pass;checksum=xxx" 或 "fail;code=-1;checksum=xxx"
        /// </summary>
        private string verify_and_program(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (string.IsNullOrEmpty(d))
                {
                    c = "fail;d_param_empty";
                    return "fail";
                }

                string[] p = d.Trim().Split(",".ToArray(), 2);
                if (p.Length < 2)
                {
                    c = "fail;d_param_invalid";
                    return "fail";
                }

                string expectedChecksum = p[0].Trim();
                string hexFilePath = p[1].Trim();

                var result = IpecmdRunner.VerifyAndProgram(expectedChecksum, hexFilePath);

                if (result.Code == 0)
                {
                    c = $"pass;checksum={result.Checksum}";
                    return "pass";
                }
                else
                {
                    c = $"fail;code={result.Code};checksum={result.Checksum}";
                    return "fail";
                }
            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo(
                    $"[Domeitc_CPV_599] verify_and_program error: {ex.Message}");
                c = $"fail;{ex.Message}";
                return "fail";
            }
        }

        public void InsertDefaultAction()
        {


            tc.dev_moren[id] = this;

        }

        public void ClosePorts()
        {
            ivps?.Close();
         
           
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

                ClosePorts();
                ivps?.Dispose();
                tc.dev_moren.Remove(id);

            }
            catch (Exception ex)
            {
            }
        }
    }

}
