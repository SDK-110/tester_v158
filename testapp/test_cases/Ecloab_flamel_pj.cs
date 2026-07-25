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
                c = $"Major='{fw.Major}';Minor='{fw.Minor}';Build='{fw.Build}'";
                utility_func.callbackdebuginfo($"[flamel] FW version: Major={fw.Major}, Minor={fw.Minor}, Build={fw.Build}");
                return "pass";
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
                var info = board.ReadBootloaderInfo();
                var list = new List<string>();
                foreach (var v in info) list.Add("'" + v.ToString());
                c = string.Join(";", list);
                utility_func.callbackdebuginfo($"[flamel] Bootloader: [{string.Join(", ", info)}]");
                return "pass";
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
                ushort val = board.ReadDeliveryOverflowState();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Delivery Overflow (Reg 112): {val}");
                return "pass";
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
                ushort val = board.ReadDeliveryEmptyState();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Delivery Empty (Reg 106): {val}");
                return "pass";
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
                ushort val = board.ReadRecircFullState();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Recirc Full (Reg 109): {val}");
                return "pass";
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
                ushort val = board.ReadConductivityProbeTemp();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Cond Probe Temp (Reg 117): {val}");
                return "pass";
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
                ushort val = board.ReadConductivity();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Conductivity (Reg 129): {val}");
                return "pass";
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
                ushort val = board.ReadProductLevel();
                c = "'" + val.ToString();
                utility_func.callbackdebuginfo($"[flamel] Product Level (Reg 119): {val}");
                return "pass";
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
                BoardType bt = board.ReadBoardType();
                c = bt.DisplayName();
                utility_func.callbackdebuginfo($"[flamel] Board Type: {bt.DisplayName()} (Reg 141 = '{(ushort)bt}')");
                return "pass";
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
