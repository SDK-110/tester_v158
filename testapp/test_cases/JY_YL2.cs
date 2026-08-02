using System;
using System.IO.Ports;
using testapp.mylib;

namespace testapp.test_cases
{
    public class JY_YL2 : IDefaultAction, IDisposable
    {
        private SerialPort _sr;
        private testcase_dll tc;
        private string id = "";
        private const byte DEFAULT_ADDR = 0x20;
        private const int TIMEOUT = 1000;

        public JY_YL2(testcase_dll _tc, string comPortName)
        {
            _sr = new SerialPort(comPortName, 9600, Parity.None, 8, StopBits.One);
            _sr.ReadTimeout = TIMEOUT;
            _sr.WriteTimeout = TIMEOUT;
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
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"JY_YL2 open port error: {ex.Message}");
            }
            add_func_to_libs();
        }

        public void add_func_to_libs()
        {
            id = "jyyl2_";
            tc.funcs.Add(id + "read_wind", read_wind);
        }

        public void InsertDefaultAction()
        {
            tc.dev_moren[id] = this;
        }

        public void ClosePorts()
        {
            if (_sr != null && _sr.IsOpen)
                _sr.Close();
        }

        private ushort ModbusCrc16(byte[] data, int len)
        {
            byte[] subset = new byte[len];
            Array.Copy(data, 0, subset, 0, len);
            return testapp.ModbusCrc16.Compute(subset);
        }

        private byte[] BuildModbusFrame(byte[] data)
        {
            ushort crcVal = ModbusCrc16(data, data.Length);
            byte[] frame = new byte[data.Length + 2];
            Array.Copy(data, 0, frame, 0, data.Length);
            frame[data.Length] = (byte)(crcVal & 0xFF);       // low byte first
            frame[data.Length + 1] = (byte)((crcVal >> 8) & 0xFF);
            return frame;
        }

        private bool VerifyResponse(byte[] response, out byte[] payload)
        {
            payload = null;
            if (response == null || response.Length < 5)
                return false;

       
            if (response.Length < 8)
                return false;

            //byte[] crcData = new byte[response.Length - 2];
            //Array.Copy(response, 0, crcData, 0, response.Length - 2);
            //ushort calcCrc = ModbusCrc16(crcData, crcData.Length);
            //byte recvLo = response[6];
            //byte recvHi = response[7];
            //if ((byte)(calcCrc & 0xFF) != recvLo || (byte)((calcCrc >> 8) & 0xFF) != recvHi)
            //    return false;

            payload = new byte[3];
            Array.Copy(response, 3, payload, 0, 3);
            return true;
        }

        private byte[] Query(byte[] data)
        {
            try
            {
                if (!_sr.IsOpen) _sr.Open();
                var frame = BuildModbusFrame(data);
                _sr.DiscardInBuffer();
                _sr.Write(frame, 0, frame.Length);
               for(int i=0;i<10;i++)
                {
                    if (_sr.BytesToRead > 0)
                        break;
                    System.Threading.Thread.Sleep(50);
                }
                System.Threading.Thread.Sleep(100);
                if (_sr.BytesToRead == 0)
                    return null;

                byte[] buf = new byte[_sr.BytesToRead];
                _sr.Read(buf, 0, buf.Length);
                return buf;
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo($"JY_YL2 query error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Read wind speed and direction.
        /// Parameter: device address in hex (optional, default=14)
        /// </summary>
        private string read_wind(string high, string low, out string rst, string parameter)
        {
            rst = "";
            try
            {
                byte addr = DEFAULT_ADDR;
                if (!string.IsNullOrEmpty(parameter))
                    addr = Convert.ToByte(parameter.Trim(), 16);

                // Modbus 0x04 read input registers, start 0x0006, count 2
                var resp = Query(new byte[] { addr, 0x04, 0x00, 0x06, 0x00, 0x02 });
                if (resp == null || resp.Length < 8)
                {
                    rst = "no response";
                    return "fail";
                }

                if (!VerifyResponse(resp, out byte[] payload) || payload.Length < 2)
                {
                    rst = "crc error";
                    return "fail";
                }

                int intPart = payload[0];
                int decPart = payload[1];
                string windStr = $"{(decPart*256+ intPart)/10.000}";

                string direction = "unknown";
               
                    if (payload[2] == 0xAA)
                    direction = "reverse";
                else if (payload[2] == 0xBB)     
                direction = "forward";

               
                rst = $"wind:{windStr}m/s,dir:{direction}";
                mylib.utility_func.callbackdebuginfo($"JY_YL2 read wind: {rst}");
               
                if (double.Parse(high.Split(';')[0]) < double.Parse(windStr) && double.Parse(windStr)<10&& high.Split(';')[1]== "forward")
                {
                    
                    return "pass";

                }

                return "fail";

            }
            catch (Exception ex)
            {
                rst = ex.Message;
                utility_func.callbackdebuginfo($"JY_YL2 read error: {ex.Message}");
                return "fail";
            }
        }

        public void set_default_set()
        {
            try
            {
                ClosePorts();
                if (_sr != null)
                    _sr.Open();
            }
            catch { }
        }

        public void Dispose()
        {
            try
            {
                ClosePorts();
                _sr?.Dispose();
                tc.dev_moren.Remove(id);
            }
            catch { }
        }
    }
}
