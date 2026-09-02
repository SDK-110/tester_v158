using System;
using System.Collections.Generic;
using testapp.glob_set;
using testapp.mylib;
using testapp.mylib.modbus_ecloab;

namespace testapp.test_cases
{
    public class Ecloab_flamel_pj : IDefaultAction, IDisposable
    {
        testcase_dll tc;
        string id = "";
        SerialPortProvider serialPort;
        ModbusRtuMaster master;
        FlamelBiocideBoard board;

        public Ecloab_flamel_pj(testcase_dll _tc)
        {
            tc = _tc;

            try
            {
                var ini = glob_ini_instance.getInstance().getSetupIniData;
                string port = ini["setport"]["Eelcoab_modbus_port"];
                int baud = int.Parse(ini["setport"]["Eelcoab_modbus_baudrate"]);

                serialPort = new SerialPortProvider(port, baud);
                serialPort.Open();
                master = new ModbusRtuMaster(serialPort);
                board = new FlamelBiocideBoard(master);
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"[Ecloab_flamel_pj] init error: {ex.Message}");
            }

            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "flamel_";
            tc.funcs.Add(id + "read_fw_version", read_fw_version);
            tc.funcs.Add(id + "read_bootloader", read_bootloader);
            tc.funcs.Add(id + "read_delivery_overflow", read_delivery_overflow);
            tc.funcs.Add(id + "read_delivery_empty", read_delivery_empty);
            tc.funcs.Add(id + "read_recirc_full", read_recirc_full);
            tc.funcs.Add(id + "read_board_type", read_board_type);
            tc.funcs.Add(id + "read_cond_temp", read_cond_temp);
            tc.funcs.Add(id + "read_conductivity", read_conductivity);
            tc.funcs.Add(id + "read_product_level", read_product_level);
            tc.funcs.Add(id + "enter_coil_mode", enter_coil_mode);
            tc.funcs.Add(id + "exit_coil_mode", exit_coil_mode);
            tc.funcs.Add(id + "set_spare_relay", set_spare_relay);
            tc.funcs.Add(id + "set_recirc_pump", set_recirc_pump);
            tc.funcs.Add(id + "set_dump_valve", set_dump_valve);
            tc.funcs.Add(id + "set_water_valve", set_water_valve);
            tc.funcs.Add(id + "read_all_relays", read_all_relays);

            tc.golb_var_default["flamel_slave_id"] = "22";
        }

        // ── Firmware & Bootloader ─────────────────────────────────────

        private string read_fw_version(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                var fw = board.ReadFirmwareVersion();

                string[] highLimits = a.Split(';');
                string[] lowLimits = b.Split(';');
                string[] names = { "Major", "Minor", "Build" };
                ushort[] values = { fw.Major, fw.Minor, fw.Build };

                bool allPass = true;
                var details = new List<string>();
                for (int i = 0; i < 3; i++)
                {
                    int hi = int.Parse(highLimits[i].Trim());
                    int lo = int.Parse(lowLimits[i].Trim());
                    bool pass = values[i] >= lo && values[i] <= hi;
                    if (!pass) allPass = false;
                    details.Add($"{names[i]}='{values[i]}'[{lo}-{hi}]={(pass ? "PASS" : "FAIL")}");
                }

                c = string.Join(";", details);
                utility_func.callbackdebuginfo($"[flamel] FW version: {c}");
                return allPass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_bootloader(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                var info = board.ReadBootloaderInfo();
                string[] expected = a.Split(';');

                bool allPass = true;
                var details = new List<string>();
                for (int i = 0; i < info.Length; i++)
                {
                    int exp = int.Parse(expected[i].Trim());
                    bool pass = info[i] == exp;
                    if (!pass) allPass = false;
                    details.Add($"Reg{19+i}='{info[i]}'[exp={exp}]={(pass ? "PASS" : "FAIL")}");
                }

                c = string.Join(";", details);
                utility_func.callbackdebuginfo($"[flamel] Bootloader: {c}");
                return allPass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        // ── Digital Input States ───────────────────────────────────────

        private string read_delivery_overflow(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadDeliveryOverflowState();
                int expected = int.Parse(a.Trim());
                bool pass = val == expected;
                c = $"'{val}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Delivery Overflow (Reg 112): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_delivery_empty(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadDeliveryEmptyState();
                int expected = int.Parse(a.Trim());
                bool pass = val == expected;
                c = $"'{val}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Delivery Empty (Reg 106): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_recirc_full(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadRecircFullState();
                int expected = int.Parse(a.Trim());
                bool pass = val == expected;
                c = $"'{val}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Recirc Full (Reg 109): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        // ── Analog / Sensor Readings ──────────────────────────────────

        private string read_cond_temp(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadConductivityProbeTemp();
                int expected = int.Parse(a.Trim());
                bool pass = val == expected;
                c = $"'{val}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Cond Probe Temp (Reg 117): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_conductivity(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadConductivity();
                int hi = int.Parse(a.Trim());
                int lo = int.Parse(b.Trim());
                bool pass = val >= lo && val <= hi;
                c = $"'{val}'[{lo}-{hi}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Conductivity (Reg 129): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_product_level(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                ushort val = board.ReadProductLevel();
                int expected = int.Parse(a.Trim());
                bool pass = val == expected;
                c = $"'{val}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Product Level (Reg 119): {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_board_type(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                BoardType bt = board.ReadBoardType();
                int expected = int.Parse(a.Trim());
                bool pass = (ushort)bt == expected;
                c = $"'{bt.DisplayName()}'[exp={expected}]={(pass ? "PASS" : "FAIL")}";
                utility_func.callbackdebuginfo($"[flamel] Board Type: {c}");
                return pass ? "pass" : "fail";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        // ── Coil Control Mode ─────────────────────────────────────────

        private string enter_coil_mode(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                board.EnterCoilControlMode();
                c = "'2";
                utility_func.callbackdebuginfo("[flamel] Entered coil control mode (Reg 185 = 2)");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string exit_coil_mode(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                board.ExitCoilControlMode();
                c = "'3";
                utility_func.callbackdebuginfo("[flamel] Exited coil control mode (Reg 185 = 3)");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        // ── Relay Control ─────────────────────────────────────────────

        private string set_spare_relay(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var parts = d.Split(',');
                if (parts.Length != 2) { c = "invalid_param"; return "fail"; }
                board.SlaveId = byte.Parse(parts[0].Trim());
                bool on = parts[1].Trim().ToUpper() == "ON";
                board.SetSpareRelay(on);
                c = on ? "ON" : "OFF";
                utility_func.callbackdebuginfo($"[flamel] Spare Relay (Coil 103): {c}");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string set_recirc_pump(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var parts = d.Split(',');
                if (parts.Length != 2) { c = "invalid_param"; return "fail"; }
                board.SlaveId = byte.Parse(parts[0].Trim());
                bool on = parts[1].Trim().ToUpper() == "ON";
                board.SetRecircPumpRelay(on);
                c = on ? "ON" : "OFF";
                utility_func.callbackdebuginfo($"[flamel] Recirc Pump Relay (Coil 101): {c}");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string set_dump_valve(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var parts = d.Split(',');
                if (parts.Length != 2) { c = "invalid_param"; return "fail"; }
                board.SlaveId = byte.Parse(parts[0].Trim());
                bool on = parts[1].Trim().ToUpper() == "ON";
                board.SetDumpValveRelay(on);
                c = on ? "ON" : "OFF";
                utility_func.callbackdebuginfo($"[flamel] Dump Valve Relay (Coil 104): {c}");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string set_water_valve(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                var parts = d.Split(',');
                if (parts.Length != 2) { c = "invalid_param"; return "fail"; }
                board.SlaveId = byte.Parse(parts[0].Trim());
                bool on = parts[1].Trim().ToUpper() == "ON";
                board.SetWaterValveRelay(on);
                c = on ? "ON" : "OFF";
                utility_func.callbackdebuginfo($"[flamel] Water Valve Relay (Coil 102): {c}");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        private string read_all_relays(string a, string b, out string c, string d)
        {
            c = "fail";
            try
            {
                if (!string.IsNullOrEmpty(d)) board.SlaveId = byte.Parse(d);
                var relays = board.ReadAllRelayStates();
                List<string> parts = new List<string>();
                foreach (var kv in relays)
                {
                    parts.Add($"{kv.Key}={kv.Value}");
                }
                c = string.Join(";", parts);
                utility_func.callbackdebuginfo($"[flamel] All relays: {c}");
                return "pass";
            }
            catch (Exception e)
            {
                utility_func.callbackdebuginfo(e.ToString());
                c = "error";
                return "fail";
            }
        }

        // ── Interface Implementations ─────────────────────────────────

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void ClosePorts()
        {
        }

        public void set_default_set()
        {
        }

        public void Dispose()
        {
            try
            {
                tc.dev_moren.Remove(id);
                board = null;
                master?.Dispose();
                if (serialPort != null)
                {
                    serialPort.Close();
                    serialPort = null;
                }
            }
            catch { }
        }
    }
}
