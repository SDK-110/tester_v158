using HslCommunication.Enthernet.Redis;
using NationalInstruments.DataInfrastructure;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SharpExModule;
using SLCANWithEvents;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace testapp.test_cases
{
    public class USB_LIN_BOARD : IDefaultAction, IDisposable
    {

        testcase_dll tc;
        private SerialPort _sr;
        string id = "";
        private const byte FRAME_HEAD1 = 0x55;
        private const byte FRAME_HEAD2 = 0xAA;
        private const byte FRAME_END = 0x5A;
        private const int LIN_BAUD = 10470;
        private Timer _cycleTimer = null;
        private volatile bool _cycleRunning;
        private readonly object _cycleLock = new object();

        private Timer _burstTimer;
        private volatile bool _burstRunning;
        private readonly object _burstLock = new object();
        public USB_LIN_BOARD(testcase_dll _tc, string comPortName)
        {
            _sr = new SerialPort(comPortName);
            _sr.BaudRate = 460800;
            _sr.Parity = Parity.None;
            _sr.StopBits = StopBits.One;
            _sr.DataBits = 8;
            _sr.Handshake = Handshake.None;
            _sr.WriteTimeout = 2000;
            _sr.ReadTimeout = 1000;


            tc = _tc;
            InsertDefaultAction();
            OpenPorts();
        }

        public void OpenPorts()
        {
            try
            {
                if (!_sr.IsOpen)
                    _sr.Open();
                SetLinBaudRate(LIN_BAUD);
                EnableInternalBoost();

            }
            catch (Exception ex)
            {
                mylib.utility_func.callbackdebuginfo($"打开端口时出错: {ex.Message}");

            }
            add_func_to_libs();
        }


        public void add_func_to_libs()
        {
            //id = this.GetType().Name;
            id = "USB_LIN_";

            tc.funcs.Add(id + "master_send", lin_master_send);
            tc.funcs.Add(id + "start_cycle", lin_start_cycle);
            tc.funcs.Add(id + "stop_cycle", lin_stop_cycle);
            tc.golb_var_default["braking_pcba_tp25"] = "-100";
        }





        public void InsertDefaultAction()
        {


            tc.dev_moren[id] = this;

        }

        public void ClosePorts()
        {
            _sr?.Close();


        }

        private static byte XorCheck(byte[] data, int len)
        {
            byte xor = 0;
            for (int i = 0; i < len; i++) xor ^= data[i];
            return xor;
        }

        private byte[] BuildFrame(byte cmd, byte[] data, bool check = false)
        {
            int nLen = 7 + data.Length;
            byte[] frame = new byte[nLen];
            frame[0] = FRAME_HEAD1;
            frame[1] = FRAME_HEAD2;
            frame[2] = (byte)nLen;
            frame[3] = check ? (byte)1 : (byte)0;
            frame[4] = cmd;
            if (data.Length > 0)
                Array.Copy(data, 0, frame, 5, data.Length);
            frame[nLen - 2] = XorCheck(frame, nLen - 2);
            frame[nLen - 1] = FRAME_END;
            return frame;
        }

        private byte[] ReadResponse()
        {
            System.Threading.Thread.Sleep(80);
            int avail = _sr.BytesToRead;
            if (avail == 0) return Array.Empty<byte>();

            byte[] buf = new byte[avail];
            _sr.Read(buf, 0, avail);

            // Find frame: 55 AA ... 5A
            for (int i = 0; i < buf.Length; i++)
            {
                if (buf[i] == FRAME_HEAD1 && i + 1 < buf.Length && buf[i + 1] == FRAME_HEAD2)
                {
                    int nLen = buf[i + 2];
                    if (i + nLen <= buf.Length && buf[i + nLen - 1] == FRAME_END)
                    {
                        byte[] frame = new byte[nLen];
                        Array.Copy(buf, i, frame, 0, nLen);
                        return frame;
                    }
                }
            }
            return buf;
        }

        public void Send(byte cmd, byte[] data)
        {
            var frame = BuildFrame(cmd, data);
            _sr.Write(frame, 0, frame.Length);
        }

        public byte[] Query(byte cmd, byte[] data)
        {
            Send(cmd, data);
            return ReadResponse();
        }

        // ===== LIN Configuration =====

        public void SetLinBaudRate(int baud)
        {
            byte[] data = new byte[4];
            data[0] = 0x00; // 0=set, 1=query
            data[3] = (byte)((baud >> 16) & 0xFF);
            data[2] = (byte)((baud >> 8) & 0xFF);
            data[1] = (byte)(baud & 0xFF);
            Query(0x02, data);
        }

        public void EnableInternalBoost()
        {
            Send(0x06, new byte[] { 0x00, 0x01 });
        }

        public int QueryLinBaudRate()
        {
            byte[] data = new byte[] { 0x01 }; // query
            var resp = Query(0x02, data);
            if (resp.Length >= 9 && resp[4] == 0x02)
            {
                // data starts at offset 5: [status(1)] [baud3] [baud2] [baud1]
                int baud = (resp[6] << 16) | (resp[7] << 8) | resp[8];
                return baud;
            }
            return -1;
        }

        // ===== Communication Check =====

        public bool CheckCommunication()
        {
            var resp = Query(0x00, Array.Empty<byte>());
            return resp.Length >= 5 && resp[4] == 0x00;
        }

        // ===== Registered Test Functions =====

        private byte[] build_master_send_cmd(byte linId, byte[] payload)
        {
            byte[] cmdData = new byte[15];
            cmdData[0] = 0x00; // reserved
            cmdData[1] = 0x01; // subFn = master send
            cmdData[2] = 0x01; // return flag = return result
            cmdData[3] = linId;
            cmdData[4] = 0x01; // checksum type = enhanced
            cmdData[5] = (byte)Math.Min(payload.Length, 8); // data length
            Array.Copy(payload, 0, cmdData, 6, Math.Min(payload.Length, 8));
            cmdData[14] = 0x00; // checksum value (enhanced: no meaning)
            return cmdData;
        }

        /// <summary>
        /// LIN master send.
        /// Parameter format: "LIN_ID;BYTE1,BYTE2,..."  (单次发送)
        ///                  "LIN_ID;BYTE1,BYTE2,...;DURATION_MS"  (burst模式，每500ms发一次，持续DURATION_MS毫秒后自动停)
        /// Example: "0x12;0x01,0x02,0x03" 或 "0x12;0x01,0xFF;3000"
        /// </summary>
        private string lin_master_send(string high, string low, out string rst, string parameter)
        {
            rst = "";
            try
            {
                if (string.IsNullOrEmpty(parameter))
                {
                    rst = "no parameter";
                    return "fail";
                }

                string[] parts = parameter.Split(';');
                if (parts.Length < 1 || string.IsNullOrEmpty(parts[0]))
                {
                    rst = "need LIN_ID";
                    return "fail";
                }

                NumberStyles hex = NumberStyles.HexNumber;
                byte linId = byte.Parse(parts[0].Trim(), hex);

                byte[] payload;
                if (parts.Length > 1 && !string.IsNullOrEmpty(parts[1]))
                {
                    string[] hexStrs = parts[1].Split(',');
                    payload = new byte[hexStrs.Length];
                    for (int i = 0; i < hexStrs.Length; i++)
                        payload[i] = byte.Parse(hexStrs[i].Trim(), hex);
                }
                else
                {
                    payload = Array.Empty<byte>();
                }

                if (payload.Length > 8)
                {
                    rst = "data > 8 bytes";
                    return "fail";
                }

                var cmdData = build_master_send_cmd(linId, payload);

                // ── Burst mode (带持续时间参数) ──
                if (parts.Length > 2 && int.TryParse(parts[2].Trim(), out int durationMs) && durationMs > 0)
                {
                    StopBurst();
                    var startTime = DateTime.UtcNow;

                    lock (_burstLock)
                    {
                        // 立即发一次
                        Send(0x20, cmdData);
                        _burstRunning = true;
                        _burstTimer = new Timer(_ =>
                        {
                            if (!_burstRunning) return;
                            // 检查是否超时
                            if ((DateTime.UtcNow - startTime).TotalMilliseconds >= durationMs)
                            {
                                StopBurst();
                                return;
                            }
                            lock (_burstLock)
                            {
                                if (!_burstRunning) return;
                                try { Send(0x20, cmdData); }
                                catch { StopBurst(); }
                            }
                        }, null, 500, 500);
                    }

                    rst = $"burst send ok, duration={durationMs}ms";
                    mylib.utility_func.callbackdebuginfo(rst);
                    return "pass";
                }

                // ── 单次发送（原行为） ──
                var resp = Query(0x20, cmdData);
                resp = Query(0x20, cmdData);
                // Response: same 22-byte layout, byte[7] = status
                if (resp.Length >= 8 && resp[4] == 0x20)
                {
                    byte status = resp[7];
                    if (status == 0)
                    {
                        rst = "send ok";
                        return "pass";
                    }
                    rst = $"send fail, status=0x{status:X2}";
                    mylib.utility_func.callbackdebuginfo(rst);
                    return "fail";
                }

                rst = "no response";
                return "fail";
            }
            catch (Exception ex)
            {
                rst = ex.Message;
                mylib.utility_func.callbackdebuginfo($"lin_master_send error: {ex.Message}");
                return "fail";
            }
        }

        private void StopBurst()
        {
            lock (_burstLock)
            {
                _burstRunning = false;
                _burstTimer?.Dispose();
                _burstTimer = null;
            }
        }

        /// <summary>
        /// LIN master receive.
        /// Parameter format: "LIN_ID;EXPECTED_LENGTH"
        /// Example: "0x12;8" receives up to 8 bytes from LIN ID 0x12
        /// </summary>
        private string lin_master_receive(string high, string low, out string rst, string parameter)
        {
            rst = "";
            try
            {
                if (string.IsNullOrEmpty(parameter))
                {
                    rst = "no parameter";
                    return "fail";
                }

                string[] parts = parameter.Split(';');
                if (parts.Length < 2 || string.IsNullOrEmpty(parts[0]))
                {
                    rst = "need LIN_ID;LENGTH";
                    return "fail";
                }

                NumberStyles hex = NumberStyles.HexNumber;
                byte linId = byte.Parse(parts[0].Trim(), hex);
                byte expectedLen = byte.Parse(parts[1].Trim());

                // 0x20 sub-function 2: master receive — fixed 22-byte frame
                // Offsets: 5=reserved(0) 6=subFn(2) 7=retFlag 8=ID
                //  9=expectedLen 10=timeout(s)
                byte[] cmdData = new byte[15];
                cmdData[0] = 0x00; // reserved
                cmdData[1] = 0x02; // subFn = master receive
                cmdData[2] = 0x01; // return flag = return result
                cmdData[3] = linId;
                cmdData[4] = expectedLen;
                cmdData[5] = 250; // timeout (ms)
                // rest zero-padded

                var resp = Query(0x20, cmdData);

                // Response: byte[7] = status, byte[10] = received data length
                if (resp.Length >= 20 && resp[4] == 0x20)
                {
                    byte status = resp[7];
                    if (status == 0)
                    {
                        int rxLen = resp[10]; // actual received data length
                        if (rxLen > 0)
                        {
                            string[] hexVals = new string[rxLen];
                            for (int i = 0; i < rxLen; i++)
                                hexVals[i] = resp[11 + i].ToString("X2");
                            rst = string.Join(",", hexVals);
                        }
                        else
                        {
                            rst = "receive ok, no data";
                        }
                        return "pass";
                    }
                    rst = $"receive fail, status=0x{status:X2}";
                    return "fail";
                }

                rst = "no response";
                return "fail";
            }
            catch (Exception ex)
            {
                rst = ex.Message;
                return "fail";
            }
        }

        // ===== Cyclic Send (2-second interval) =====

        public void StartCycle()
        {
            StopCycle();
            _cycleRunning = true;
            _cycleTimer = new Timer(_ =>
            {
                if (!_cycleRunning) return;
                if (!Monitor.TryEnter(_cycleLock)) return;
                try
                {
                    if (!_cycleRunning) return;
                    // Frame 1: ID=0x01, data=01 FF FF
                    Send(0x20, build_master_send_cmd(0x01, new byte[] { 0x01, 0xFF, 0xFF }));
                    // Frame 2: ID=0x01, data=02 FF FF
                    Send(0x20, build_master_send_cmd(0x01, new byte[] { 0x02, 0xFF, 0xFF }));
                }
                catch (Exception e)
                {
                    mylib.utility_func.callbackdebuginfo(e.ToString());

                    StopCycle();
                }
                finally { Monitor.Exit(_cycleLock); }
            }, null, 0, 2000);
        }

        public void StopCycle()
        {
            _cycleRunning = false;
            lock (_cycleLock)
            {
                _cycleTimer?.Dispose();
                _cycleTimer = null;
            }
        }

        private string lin_start_cycle(string high, string low, out string rst, string parameter)
        {
            try
            {
                StartCycle();
                rst = "cycle started";
                return "pass";
            }
            catch (Exception ex)
            {
                rst = ex.Message;
                return "fail";
            }
        }

        private string lin_stop_cycle(string high, string low, out string rst, string parameter)
        {
            try
            {
                StopCycle();
                rst = "cycle stopped";
                return "pass";
            }
            catch (Exception ex)
            {
                rst = ex.Message;
                return "fail";
            }
        }




        public void set_default_set()
        {
            try
            {
                StopBurst();
                StopCycle();
                if (_sr != null && _sr.IsOpen)
                {
                    _sr.Close();
                    _sr.Open();
                    SetLinBaudRate(LIN_BAUD);
                    EnableInternalBoost();
                }
            }
            catch { }
        }

        public void Dispose()
        {
            try
            {

                StopBurst();
                ClosePorts();
                _sr?.Dispose();
                tc.dev_moren.Remove(id);

            }
            catch (Exception ex)
            {
            }
        }
    }

}
