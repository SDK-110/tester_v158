using System;
using System.Collections.Generic;
using System.Linq;
using testapp.mylib.canopen.services;
using testapp.mylib.canopen.eds;

namespace testapp.mylib.canopen
{
    /// <summary>Represents a remote CANopen slave node.
    /// Provides type-safe SDO access, PDO subscriptions, NMT control, and heartbeat monitoring.</summary>
    public class RemoteNode : IDisposable
    {
        public byte NodeId { get; }
        public string Name { get; }
        /// <summary>EDS-loaded object dictionary. Null if no EDS was provided.</summary>
        public ObjectDictionary ObjectDict { get; private set; }
        public NMTState KnownState => NMT?.KnownState ?? NMTState.Unknown;
        public bool IsAlive => Heartbeat?.IsAlive ?? false;

        internal SDOClient SDO { get; private set; }
        internal NmtMaster NMT { get; private set; }
        internal HeartbeatConsumer Heartbeat { get; private set; }

        private readonly FrameRouter _router;
        private readonly Dictionary<byte, List<Action<PdoData>>> _pdoHandlers =
            new Dictionary<byte, List<Action<PdoData>>>();

        /// <summary>Fires when an emergency message is received from this node.</summary>
        public event EventHandler<EmergencyData> Emergency;
        /// <summary>Fires when heartbeat times out for this node.</summary>
        public event EventHandler HeartbeatLost;
        /// <summary>Fires when heartbeat is restored after timeout.</summary>
        public event EventHandler HeartbeatRestored;

        internal RemoteNode(byte nodeId, string name, ObjectDictionary od, FrameRouter router)
        {
            NodeId = nodeId;
            Name = name ?? $"Node_{nodeId}";
            ObjectDict = od;
            _router = router;

            SDO = new SDOClient(nodeId, router) { ObjectDict = od };
            NMT = new NmtMaster(nodeId, router);
            Heartbeat = new HeartbeatConsumer(nodeId, router);

            Heartbeat.Lost += (s, e) => HeartbeatLost?.Invoke(this, e);
            Heartbeat.Restored += (s, e) => HeartbeatRestored?.Invoke(this, e);
            Heartbeat.StateChanged += (s, state) => NMT.UpdateState(state);

            // Subscribe to PDO COB-IDs
            SubscribePDO(1, CANopenID.TPDO1(nodeId));
            SubscribePDO(2, CANopenID.TPDO2(nodeId));
            SubscribePDO(3, CANopenID.TPDO3(nodeId));
            SubscribePDO(4, CANopenID.TPDO4(nodeId));

            // Subscribe EMCY
            router.Subscribe(CANopenID.EMCY(nodeId), new EmcyHandler(this));
        }

        private void SubscribePDO(byte num, uint cobId)
        {
            _router.Subscribe(cobId, new PdoInternalHandler(this, num));
        }

        // ===== NMT =====
        public void Start() => NMT.Start();
        public void Stop() => NMT.Stop();
        public void EnterPreOperational() => NMT.EnterPreOp();
        public void ResetNode() => NMT.ResetNode();
        public void ResetCommunication() => NMT.ResetComm();

        // ===== SDO Read =====

        /// <summary>Type-safe SDO read. Uses EDS data type info if available.</summary>
        public T SDORead<T>(ushort index, byte subIndex = 0, int timeoutMs = 1000)
        {
            var raw = SDO.Upload(index, subIndex);
            return (T)ConvertRawToType(raw, typeof(T), index, subIndex);
        }

        /// <summary>Dynamic-type SDO read. Returns correct .NET type based on EDS.</summary>
        public object SDORead(ushort index, byte subIndex = 0, int timeoutMs = 1000)
        {
            var raw = SDO.Upload(index, subIndex);
            if (ObjectDict != null)
            {
                var subEntry = ObjectDict.GetSubEntry(index, subIndex);
                if (subEntry != null)
                    return DataTypeMap.FromBytes(subEntry.DataType, raw);
            }
            return raw;
        }

        /// <summary>SDO read by parameter name (requires EDS).</summary>
        public T SDORead<T>(string parameterName)
        {
            var (idx, sub) = ResolveName(parameterName);
            return SDORead<T>(idx, sub);
        }

        // ===== SDO Write =====

        public void SDOWrite<T>(ushort index, byte subIndex, T value)
        {
            byte[] raw = ConvertTypeToRaw(value, index, subIndex);
            SDO.Download(index, subIndex, raw);
        }

        public void SDOWrite(ushort index, byte subIndex, byte[] rawData)
        {
            SDO.Download(index, subIndex, rawData);
        }

        public void SDOWrite<T>(string parameterName, T value)
        {
            var (idx, sub) = ResolveName(parameterName);
            SDOWrite(idx, sub, value);
        }

        // ===== PDO =====

        /// <summary>Register a handler for incoming PDO data.</summary>
        public void OnPDO(byte pdoNumber, Action<PdoData> handler)
        {
            if (!_pdoHandlers.ContainsKey(pdoNumber))
                _pdoHandlers[pdoNumber] = new List<Action<PdoData>>();
            _pdoHandlers[pdoNumber].Add(handler);
        }

        public void UnsubscribePDO(byte pdoNumber)
        {
            _pdoHandlers.Remove(pdoNumber);
        }

        // ===== Heartbeat =====
        public int HeartbeatTimeoutMs
        {
            get => Heartbeat?.HeartbeatTimeoutMs ?? 3000;
            set { if (Heartbeat != null) Heartbeat.HeartbeatTimeoutMs = value; }
        }

        // ===== Emergency Data =====
        public class EmergencyData : EventArgs
        {
            public ushort ErrorCode { get; set; }
            public byte ErrorRegister { get; set; }
            public byte[] ManufacturerData { get; set; }
        }

        // ===== TestCase Integration =====
        /// <summary>Registers SDO/NMT/HEARTBEAT/DI/AI/DO functions into testcase_dll.funcs.</summary>
        public void RegisterToTestCase(testapp.testcase_dll tc, string prefix = null)
        {
            if (tc == null) throw new ArgumentNullException(nameof(tc));
            string p = prefix ?? $"N{NodeId}_";

            tc.funcs.Add(p + "SDO_READ", (string a, string b, out string c, string d) =>
            {
                c = "";
                try
                {
                    string[] parts = a.Split(':');
                    ushort idx = Convert.ToUInt16(parts[0], 16);
                    byte sub = parts.Length > 1 ? Convert.ToByte(parts[1]) : (byte)0;
                    c = SDORead(idx, sub)?.ToString() ?? "null";
                    return "PASS";
                }
                catch (Exception ex) { c = ex.Message; return "FAIL"; }
            });

            tc.funcs.Add(p + "SDO_WRITE", (string a, string b, out string c, string d) =>
            {
                c = "";
                try
                {
                    string[] parts = a.Split(':');
                    ushort idx = Convert.ToUInt16(parts[0], 16);
                    byte sub = Convert.ToByte(parts[1]);
                    byte[] data = utility_func.strByts2ByteArray(b);
                    SDOWrite(idx, sub, data);
                    return "PASS";
                }
                catch (Exception ex) { c = ex.Message; return "FAIL"; }
            });

            tc.funcs.Add(p + "NMT_START", (string a, string b, out string c, string d) =>
            { c = ""; Start(); return "PASS"; });

            tc.funcs.Add(p + "NMT_STOP", (string a, string b, out string c, string d) =>
            { c = ""; Stop(); return "PASS"; });

            tc.funcs.Add(p + "NMT_PREOP", (string a, string b, out string c, string d) =>
            { c = ""; EnterPreOperational(); return "PASS"; });

            tc.funcs.Add(p + "NMT_RESET", (string a, string b, out string c, string d) =>
            { c = ""; ResetNode(); return "PASS"; });

            tc.funcs.Add(p + "HEARTBEAT", (string a, string b, out string c, string d) =>
            { c = IsAlive ? "1" : "0"; return IsAlive ? "PASS" : "FAIL"; });

            tc.funcs.Add(p + "DI_READ", (string a, string b, out string c, string d) =>
            {
                c = "";
                try
                {
                    byte[] raw = SDO.Upload(0x60FD, 0x01);
                    c = raw.Length > 0 ? raw[0].ToString() : "0";
                    return "PASS";
                }
                catch (Exception ex) { c = ex.Message; return "FAIL"; }
            });

            tc.funcs.Add(p + "AI_READ", (string a, string b, out string c, string d) =>
            {
                c = "";
                try
                {
                    int ch = int.TryParse(a, out int val) ? val : 0;
                    byte[] raw = SDO.Upload(0x6401, (byte)(ch + 1));
                    ushort ai = raw.Length >= 2 ? BitConverter.ToUInt16(raw, 0) : (ushort)0;
                    c = ai.ToString();
                    return "PASS";
                }
                catch (Exception ex) { c = ex.Message; return "FAIL"; }
            });

            tc.funcs.Add(p + "DO_WRITE", (string a, string b, out string c, string d) =>
            {
                c = "";
                try
                {
                    byte val = byte.TryParse(a, out byte v) ? v : (byte)0;
                    SDOWrite<byte>(0x6200, 0x01, val);
                    return "PASS";
                }
                catch (Exception ex) { c = ex.Message; return "FAIL"; }
            });
        }

        // ===== Internal Helpers =====

        private (ushort index, byte sub) ResolveName(string name)
        {
            if (ObjectDict == null)
                throw new InvalidOperationException("EDS required for name-based access");
            var entry = ObjectDict.FindByName(name);
            if (entry == null)
                throw new ArgumentException($"Parameter '{name}' not found in EDS");
            byte sub = entry.SubEntries.Count > 0
                ? (entry.SubEntries.ContainsKey(1) ? (byte)1 : (byte)0)
                : (byte)0;
            return (entry.Index, sub);
        }

        private object ConvertRawToType(byte[] raw, Type targetType, ushort index, byte subIndex)
        {
            if (ObjectDict != null)
            {
                var subEntry = ObjectDict.GetSubEntry(index, subIndex);
                if (subEntry != null)
                    return DataTypeMap.FromBytes(subEntry.DataType, raw);
            }
            // Fallback: direct conversion
            if (targetType == typeof(byte))    return raw.Length > 0 ? raw[0] : (byte)0;
            if (targetType == typeof(bool))    return raw.Length > 0 && raw[0] != 0;
            if (targetType == typeof(ushort))  return raw.Length >= 2 ? BitConverter.ToUInt16(raw, 0) : (ushort)0;
            if (targetType == typeof(short))   return raw.Length >= 2 ? BitConverter.ToInt16(raw, 0) : (short)0;
            if (targetType == typeof(uint))    return raw.Length >= 4 ? BitConverter.ToUInt32(raw, 0) : 0U;
            if (targetType == typeof(int))     return raw.Length >= 4 ? BitConverter.ToInt32(raw, 0) : 0;
            if (targetType == typeof(float))   return raw.Length >= 4 ? BitConverter.ToSingle(raw, 0) : 0f;
            return raw;
        }

        private byte[] ConvertTypeToRaw<T>(T value, ushort index, byte subIndex)
        {
            if (ObjectDict != null)
            {
                var subEntry = ObjectDict.GetSubEntry(index, subIndex);
                if (subEntry != null)
                    return DataTypeMap.ToBytes(subEntry.DataType, value);
            }
            if (value is byte b)     return new[] { b };
            if (value is bool bo)    return new[] { (byte)(bo ? 1 : 0) };
            if (value is ushort us)  return BitConverter.GetBytes(us);
            if (value is short s)    return BitConverter.GetBytes(s);
            if (value is uint ui)    return BitConverter.GetBytes(ui);
            if (value is int i)      return BitConverter.GetBytes(i);
            if (value is float f)    return BitConverter.GetBytes(f);
            if (value is byte[] ba)  return ba;
            return new byte[0];
        }

        public void Dispose()
        {
            SDO?.Dispose();
        }

        // ===== Internal PDO handler =====
        private class PdoInternalHandler : IRoutableService
        {
            private readonly RemoteNode _node;
            private readonly byte _pdoNum;
            public PdoInternalHandler(RemoteNode node, byte pdoNum)
            { _node = node; _pdoNum = pdoNum; }
            void IRoutableService.HandleFrame(CanFrame frame)
            {
                if (!_node._pdoHandlers.TryGetValue(_pdoNum, out var handlers)) return;
                var pdoData = new PdoData { PdoNumber = _pdoNum, RawData = frame.Data };
                if (_node.ObjectDict != null)
                    pdoData = PDOProcessor.Process(pdoData, _node.ObjectDict, _pdoNum);
                foreach (var h in handlers) h(pdoData);
            }
        }

        // ===== Internal EMCY handler =====
        private class EmcyHandler : IRoutableService
        {
            private readonly RemoteNode _node;
            public EmcyHandler(RemoteNode node) { _node = node; }
            void IRoutableService.HandleFrame(CanFrame frame)
            {
                if (frame.Data == null || frame.Data.Length < 2) return;
                var emcy = new EmergencyData
                {
                    ErrorCode = BitConverter.ToUInt16(frame.Data, 0),
                    ErrorRegister = frame.Data.Length > 2 ? frame.Data[2] : (byte)0,
                    ManufacturerData = frame.Data.Length > 3
                        ? new ArraySegment<byte>(frame.Data, 3, frame.Data.Length - 3).ToArray()
                        : new byte[0]
                };
                _node.Emergency?.Invoke(_node, emcy);
            }
        }
    }
}
