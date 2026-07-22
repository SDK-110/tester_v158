# CANopen Master Protocol Stack on SLCAN

**Date:** 2026-07-14  
**Project:** testapp (.NET Framework 4.8)  
**Target Directory:** `mylib/canopen/`

## 1. Goals

Build a CANopen master protocol stack on top of the existing SLCAN serial-port transport, with EDS file parsing and a registration-based API for type-safe SDO/PDO/NMT access. Primary target devices are I/O modules (DS-401) with digital and analog channels.

### Non-Goals

- No CANopen slave/device mode
- No full device emulation (no NMT slave state machine)
- No SDO server (client only)
- No LSS (Layer Setting Services) in v1
- No SYNC consumer in v1

## 2. Architecture Overview

```
┌──────────────────────────────────────────────────────────────┐
│                      TestCase Layer                            │
│  tc.funcs["CAN_SDO_READ"](nodeId, index, subIndex, ...)       │
│  tc.funcs["CAN_PDO_MONITOR"](nodeId, timeout, ...)            │
└──────────────────────────┬───────────────────────────────────┘
                           │
┌──────────────────────────▼───────────────────────────────────┐
│                     CanOpenMaster                               │
│                                                                 │
│  ┌─────────────────────────────────────────────────────────┐  │
│  │  RemoteNode Manager                                      │  │
│  │  RegisterNode(id, name, edsPath) → RemoteNode           │  │
│  │  GetNode(id) → RemoteNode                                │  │
│  │  Nodes: Dictionary<byte, RemoteNode>                     │  │
│  └────────────────────┬────────────────────────────────────┘  │
│                        │                                        │
│  ┌────────────────────▼────────────────────────────────────┐  │
│  │  RemoteNode                                              │  │
│  │  ┌──────────┐ ┌──────────┐ ┌───────────┐ ┌─────────┐  │  │
│  │  │ SDOClient│ │ NmtMaster│ │ PDOProc.  │ │ HbCons. │  │  │
│  │  │ SDORead  │ │ Start    │ │ OnPDO     │ │ 心跳监测 │  │  │
│  │  │ SDOWrite │ │ Stop     │ │ AutoMap   │ │ 超时回调 │  │  │
│  │  └──────────┘ └──────────┘ └───────────┘ └─────────┘  │  │
│  └────────────────────┬────────────────────────────────────┘  │
└──────────────────────────┬───────────────────────────────────┘
                           │
┌──────────────────────────▼───────────────────────────────────┐
│                     FrameRouter                                 │
│  COB-ID → dispatch to owning node's service                    │
│  0x000  → NMTHandler                                           │
│  0x180~ → PDOHandler(whichNode, whichPDO)                      │
│  0x580~ → SDOResponseHandler(whichNode)                        │
│  0x700~ → HeartbeatHandler(whichNode)                          │
│  0x080~ → EMCYHandler(whichNode)                               │
└──────────────────────────┬───────────────────────────────────┘
                           │
┌──────────────────────────▼───────────────────────────────────┐
│                    SLCANTransport                               │
│  ISLCANTransport interface                                     │
│  └─ SLCANSerialPort (existing CAN_SLCAN.cs)                    │
│  └─ VirtualSLCAN (loopback for testing)                        │
└──────────────────────────────────────────────────────────────┘
```

## 3. File Structure

```
mylib/canopen/
├── CanOpenMaster.cs          — Main entry. Node management, bus lifecycle.
├── CanOpenTypes.cs           — Enums (NMTState, NMTCommand, COBType...), COB-ID helpers.
├── CanFrame.cs               — CanFrame struct, frame builder/parser utilities.
├── SLCANTransport.cs         — ISLCANTransport interface + SLCANSerialPort adapter.
├── FrameRouter.cs            — Internal: receive-thread dispatcher by COB-ID.
├── RemoteNode.cs             — Single slave abstraction, owns service instances.
│
├── services/
│   ├── SDOClient.cs          — SDO upload/download (expedited + segmented).
│   ├── NmtMaster.cs          — NMT command sender + state tracker.
│   └── HeartbeatConsumer.cs  — Heartbeat timeout monitoring.
│
├── eds/
│   ├── EDSParser.cs          — EDS/DCF → ObjectDictionary converter.
│   ├── ObjectDictionary.cs   — In-memory OD: lookup by index/subindex or name.
│   ├── ODEntry.cs            — One OD entry (index, subindex list, name, type, access).
│   └── DataTypeMap.cs        — CANopen type ↔ .NET type + byte conversion.
│
└── pdo/
    ├── PDOMapping.cs         — PDO communication + mapping parameter model.
    └── PDOProcessor.cs       — Incoming PDO data → typed values by EDS mapping.
```

~12 files total. Each has one clear responsibility.

## 4. Component Design

### 4.1 CanOpenMaster

```csharp
public class CanOpenMaster : IDisposable
{
    public CanOpenMaster(string comPort, int baudRate = 500000);

    // ---- Bus control ----
    public void Start();         // Open SLCAN, start receive loop
    public void Stop();          // Close SLCAN, stop all timers
    public bool IsRunning { get; }

    // ---- Node management ----
    public RemoteNode RegisterNode(byte nodeId, string name, string edsPath = null);
    public void UnregisterNode(byte nodeId);
    public RemoteNode GetNode(byte nodeId);
    public IReadOnlyDictionary<byte, RemoteNode> Nodes { get; }

    // ---- Broadcast NMT ----
    public void NMTBroadcast(NMTCommand command);

    // ---- SYNC producer ----
    public void SendSync();

    // ---- Raw CAN ----
    public void SendRawFrame(uint cobId, byte[] data);
    public void SubscribeRaw(uint cobIdStart, uint cobIdEnd, Action<CanFrame> handler);
    public void UnsubscribeRaw(uint cobIdStart, uint cobIdEnd);

    // ---- Events ----
    public event EventHandler<EmergencyData> EmergencyReceived;
    public event EventHandler<HeartbeatEvent> HeartbeatStatusChanged;
}
```

**Responsibilities:**
- Owns the SLCAN transport and receive thread
- Routes incoming CAN frames to FrameRouter
- Manages node lifecycle and service instances
- Provides the application-facing API

### 4.2 RemoteNode

```csharp
public class RemoteNode
{
    public byte NodeId { get; }
    public string Name { get; }
    public NMTState KnownState { get; }
    public ObjectDictionary ObjectDict { get; }  // null if no EDS loaded

    // ---- NMT ----
    public void Start();
    public void Stop();
    public void EnterPreOperational();
    public void ResetNode();
    public void ResetCommunication();

    // ---- SDO read (type-safe) ----
    public T SDORead<T>(ushort index, byte subIndex = 0, int timeoutMs = 1000);
    public object SDORead(ushort index, byte subIndex = 0, int timeoutMs = 1000);
    // When EDS loaded: the non-generic overload returns the correct .NET type automatically.

    // ---- SDO read by name (requires EDS) ----
    public T SDORead<T>(string parameterName);

    // ---- SDO write ----
    public void SDOWrite<T>(ushort index, byte subIndex, T value);
    public void SDOWrite<T>(string parameterName, T value);
    public void SDOWrite(ushort index, byte subIndex, byte[] rawData);

    // ---- SDO async (for non-blocking scenarios) ----
    public Task<T> SDOReadAsync<T>(ushort index, byte subIndex = 0);
    public Task<bool> SDOWriteAsync<T>(ushort index, byte subIndex, T value);

    // ---- PDO ----
    public void OnPDO(byte pdoNumber, Action<PdoData> handler);
    public void UnsubscribePDO(byte pdoNumber);
    public void AutoConfigurePDO();  // Apply EDS PDO mapping entries

    // ---- EMCY ----
    public event EventHandler<EmergencyData> Emergency;

    // ---- Heartbeat ----
    public int HeartbeatTimeoutMs { get; set; }
    public bool IsAlive { get; }
    public event EventHandler HeartbeatLost;
    public event EventHandler HeartbeatRestored;

    // ---- TestCase integration ----
    public void RegisterToTestCase(testcase_dll tc, string prefix = null);
}
```

**Responsibilities:**
- Owns one slave's SDOClient, NmtMaster, HeartbeatConsumer
- Exposes convenience methods that delegate to the services
- Provides optional EDS-backed type-aware access
- Generates the pointfun registrations for testcase_dll.funcs

### 4.3 SDOClient (services/SDOClient.cs)

```csharp
internal class SDOClient
{
    public SDOClient(byte nodeId, FrameRouter router, int defaultTimeoutMs);

    // Expedited: used automatically when data ≤ 4 bytes
    public byte[] ExpeditedUpload(ushort index, byte subIndex);
    public bool ExpeditedDownload(ushort index, byte subIndex, byte[] data);

    // Segmented: used when data > 4 bytes or server requires it
    public byte[] SegmentedUpload(ushort index, byte subIndex);
    public bool SegmentedDownload(ushort index, byte subIndex, byte[] data);

    // Auto-dispatch: try expedited first, fall back to segmented
    public byte[] Upload(ushort index, byte subIndex);
    public bool Download(ushort index, byte subIndex, byte[] data);

    // Abort running transfer
    public void Abort(ushort index, byte subIndex, uint abortCode);

    // Events
    public event EventHandler<SDOAbortEvent> TransferAborted;
}
```

**Protocol details:**

SDO upload request frame (COB-ID = 0x600 + nodeId):
```
Byte 0: [CCS=2][0][0][0][x]  — Initiate upload
Byte 1-2: Index (LE)
Byte 3: SubIndex
Byte 4-7: Reserved (0)
```

SDO upload response frame (COB-ID = 0x580 + nodeId):
```
Byte 0: [SCS=2][n][e][s][1]  — Expedited response, data in bytes 4-7
Byte 1-2: Index (LE)
Byte 3: SubIndex
Byte 4-7: Data (n+1 bytes, zero-padded)
```

SDO abort frame:
```
Byte 0: [SCS=4][0][0][0][0]  — Abort
Byte 1-2: Index (LE)
Byte 3: SubIndex
Byte 4-7: Abort code (U32 LE)
```

Common abort codes:
- 0x05040001: Toggle bit not alternated
- 0x05040005: Out of memory
- 0x06010000: Unsupported access
- 0x06010001: Attempt to read write-only object
- 0x06010002: Attempt to write read-only object
- 0x06020000: Object does not exist
- 0x06040041: PDO mapping error
- 0x08000000: General error

**Timeout/retry pattern** (reference: `chuangxincan.cs:send_rev`):
```csharp
public byte[] Upload(ushort index, byte subIndex)
{
    for (int retry = 0; retry < 3; retry++)
    {
        SendRequest(index, subIndex);
        var resp = WaitForResponse(_defaultTimeoutMs);
        if (resp != null)
            return ParseResponse(resp.Value);
    }
    throw new SDOException($"SDO upload timeout: 0x{index:X4}:{subIndex}", 0x08000000);
}
```

### 4.4 NmtMaster (services/NmtMaster.cs)

```csharp
internal class NmtMaster
{
    public NmtMaster(byte nodeId, ISLCANTransport transport);
    public void SendCommand(NMTCommand command);
    public NMTState KnownState { get; private set; }
    public event EventHandler<NMTState> StateChanged;
}
```

NMT command frame (COB-ID = 0x000):
```
Byte 0: Command specifier (0x01=Start, 0x02=Stop, 0x80=PreOp, 0x81=ResetNode, 0x82=ResetComm)
Byte 1: Node ID (0 = all nodes)
```

State transitions inferred from:
- NMT command sent → expected target state
- Heartbeat boot-up message → PreOperational confirmed
- Heartbeat operational message → Operational confirmed
- Heartbeat stopped → Stopped (or Unknown after timeout)

### 4.5 HeartbeatConsumer (services/HeartbeatConsumer.cs)

```csharp
internal class HeartbeatConsumer
{
    public HeartbeatConsumer(byte nodeId);
    
    public void SetExpectedTimeout(int ms);  // EDS value or manual
    public bool IsAlive { get; }
    
    public void OnHeartbeatReceived(NMTState state);
    public void CheckTimeout();  // Call from master timer
    
    public event EventHandler Lost;
    public event EventHandler Restored;
}
```

Heartbeat frame (COB-ID = 0x700 + nodeId, 1 byte):
```
Byte 0: NMT state (0x00=BootUp, 0x05=Operational, 0x7F=PreOp, 0x04=Stopped)
```

**Timeout logic:**
- Master runs a 100ms timer scanning all registered nodes
- For each node: if (now - lastHeartbeat) > HeartbeatTimeoutMs → fire Lost
- Timeout = ProducerHeartbeatTime × 3 (per CiA 301 recommendation)

### 4.6 EDSParser (eds/EDSParser.cs)

Reuses the project's existing `ini-parser` v2.5.2 NuGet dependency.

Interface:
```csharp
public static class EDSParser
{
    // From file system
    public static ObjectDictionary LoadFromFile(string edsPath);
    
    // From embedded resource or stream
    public static ObjectDictionary LoadFromStream(Stream stream);
    
    // From raw string
    public static ObjectDictionary LoadFromString(string edsContent);
}
```

EDS section parsing:

| Section | Maps to | Example |
|---------|---------|---------|
| `[FileInfo]` | OD metadata | `FileName`, `FileVersion`, `Description` |
| `[DeviceInfo]` | Device identity | `VendorID`, `ProductCode`, `BaudRate_500` |
| `[DummyUsage]` | Dummy mapping | `Dummy0001`, `Dummy0002` |
| `[MandatoryObjects]` | Required indexes | `SupportedObjects=6` |
| `[OptionalObjects]` | Optional indexes | `SupportedObjects=20` |
| `[ManufacturerObjects]` | Vendor-specific | `SupportedObjects=10` |
| `[IndexXXXX]` | OD entry | `ParameterName`, `ObjectType`, `DataType`, `AccessType` |
| `[IndexXXXXSubYY]` | Sub-entry | `ParameterName`, `DataType`, `DefaultValue`, `LowLimit`, `HighLimit` |

### 4.7 ObjectDictionary (eds/ObjectDictionary.cs)

```csharp
public class ObjectDictionary
{
    // Device identity
    public uint VendorId { get; set; }
    public uint ProductCode { get; set; }
    public uint RevisionNumber { get; set; }
    public string IdentityString { get; set; }

    // OD entries
    private Dictionary<ushort, ODEntry> _entries = new();

    public void AddEntry(ODEntry entry);
    public ODEntry GetEntry(ushort index);
    public bool TryGetEntry(ushort index, out ODEntry entry);
    public ODSubEntry GetSubEntry(ushort index, byte subIndex);
    public bool TryGetSubEntry(ushort index, byte subIndex, out ODSubEntry entry);
    
    // Name-based lookup (case-insensitive)
    public ODEntry FindByName(string name);
    public ushort GetIndexByName(string name);
}
```

### 4.8 ODEntry (eds/ODEntry.cs)

```csharp
public class ODEntry
{
    public ushort Index { get; set; }
    public string Name { get; set; }
    public byte ObjectType { get; set; }      // 0x07=VAR, 0x08=ARRAY, 0x09=RECORD
    public byte DataType { get; set; }        // CANopen data type code
    public string AccessType { get; set; }    // "ro", "rw", "wo"
    public string DefaultValue { get; set; }
    public bool IsPDOMappable { get; set; }
    
    public Dictionary<byte, ODSubEntry> SubEntries { get; set; }
}

public class ODSubEntry
{
    public byte SubIndex { get; set; }
    public string Name { get; set; }
    public byte DataType { get; set; }
    public string AccessType { get; set; }
    public string DefaultValue { get; set; }
    public string LowLimit { get; set; }
    public string HighLimit { get; set; }
}
```

### 4.9 DataTypeMap (eds/DataTypeMap.cs)

```csharp
public static class DataTypeMap
{
    /// <summary>CANopen data type code → .NET Type</summary>
    public static Type ToNetType(byte canopenType);
    
    /// <summary>CANopen data type code → byte count</summary>
    public static int GetByteCount(byte canopenType);

    /// <summary>Convert raw CAN bytes to a .NET object, using the EDS type code</summary>
    public static object FromBytes(byte canopenType, byte[] data);
    
    /// <summary>Convert a .NET object to CAN bytes, using the EDS type code</summary>
    public static byte[] ToBytes(byte canopenType, object value);

    /// <summary>Generic helpers</summary>
    public static T FromBytes<T>(byte[] data);
    public static byte[] ToBytes<T>(T value);
}
```

| Type Code | CANopen Type | .NET Type | Bytes | FromBytes | ToBytes |
|:---------:|:------------:|:---------:|:----:|:---------:|:-------:|
| 0x01 | BOOLEAN | `bool` | 1 | `BitConverter.ToBoolean` | `BitConverter.GetBytes` |
| 0x02 | INTEGER8 | `sbyte` | 1 | `(sbyte)data[0]` | `new[]{ (byte)value }` |
| 0x03 | INTEGER16 | `short` | 2 | `BitConverter.ToInt16` | `BitConverter.GetBytes` |
| 0x04 | INTEGER32 | `int` | 4 | `BitConverter.ToInt32` | `BitConverter.GetBytes` |
| 0x05 | UNSIGNED8 | `byte` | 1 | `data[0]` | `new[]{ value }` |
| 0x06 | UNSIGNED16 | `ushort` | 2 | `BitConverter.ToUInt16` | `BitConverter.GetBytes` |
| 0x07 | UNSIGNED32 | `uint` | 4 | `BitConverter.ToUInt32` | `BitConverter.GetBytes` |
| 0x08 | REAL32 | `float` | 4 | `BitConverter.ToSingle` | `BitConverter.GetBytes` |
| 0x09 | VISIBLE_STRING | `string` | var | `Encoding.ASCII.GetString` | `Encoding.ASCII.GetBytes` |
| 0x0B | INTEGER24 | `int` | 3 | Sign-extend 3B | LE 3 bytes |
| 0x0C | REAL64 | `double` | 8 | `BitConverter.ToDouble` | `BitConverter.GetBytes` |

### 4.10 FrameRouter

```csharp
internal class FrameRouter
{
    public FrameRouter(ISLCANTransport transport);

    // Register a handler for a specific COB-ID range
    public void Subscribe(uint cobId, IRoutableService handler);
    public void Unsubscribe(uint cobId);

    // Called from SLCAN receive thread
    public void Dispatch(CanFrame frame);

    // Raw frame subscriptions
    public void SubscribeRaw(uint cobIdStart, uint cobIdEnd, Action<CanFrame> handler);
}
```

**Dispatch table (COB-ID → handler resolution):**

| COB-ID Range | Type | Routing |
|:------------:|:----:|:--------|
| `0x000` | NMT | Master-wide NMT broadcast |
| `0x080` | SYNC | Master SYNC handler |
| `0x081..0x0FF` | EMCY | `NodeId = cobId & 0x7F` → node.EMCY |
| `0x101..0x17F` | — | Reserved (or custom) |
| `0x181..0x1FF` | TPDO1 | `NodeId = cobId & 0x7F` → node.PDO1 |
| `0x201..0x27F` | RPDO1 | Not consumed by master (we send these) |
| `0x281..0x2FF` | TPDO2 | `NodeId = cobId & 0x7F` → node.PDO2 |
| `0x301..0x37F` | RPDO2 | Not consumed by master |
| `0x381..0x3FF` | TPDO3 | `NodeId = cobId & 0x7F` → node.PDO3 |
| `0x401..0x47F` | RPDO3 | Not consumed by master |
| `0x481..0x4FF` | TPDO4 | `NodeId = cobId & 0x7F` → node.PDO4 |
| `0x501..0x57F` | RPDO4 | Not consumed by master |
| `0x581..0x5FF` | SDO TX | `NodeId = cobId & 0x7F` → node.SDO |
| `0x601..0x67F` | SDO RX | Not consumed by master (we send these) |
| `0x701..0x77F` | Heartbeat | `NodeId = cobId & 0x7F` → node.Heartbeat |

### 4.11 PDOProcessor (pdo/PDOProcessor.cs)

```csharp
public class PDOData : EventArgs
{
    public byte PdoNumber { get; set; }    // 1-4
    public byte[] RawData { get; set; }
    public Dictionary<string, object> Values { get; set; }  // Named values from EDS mapping
    
    // Convenience accessors
    public T Get<T>(string name);
    public T Get<T>(int channelIndex);
}

public class PDOProcessor
{
    public PDOProcessor(byte nodeId, byte pdoNum, ObjectDictionary od = null);
    
    // Set PDO mapping (manually or from EDS)
    public void SetMapping(List<PDOMappingEntry> mappings);
    public void LoadMappingFromEDS(byte pdoNumber);
    
    // Process incoming PDO data
    public PDOData Process(byte[] rawData);
    
    // Event
    public event EventHandler<PDOData> PDOReceived;
}

public class PDOMappingEntry
{
    public ushort Index { get; set; }
    public byte SubIndex { get; set; }
    public int BitLength { get; set; }
    public string Name { get; set; }
    public byte DataType { get; set; }  // CANopen type code from EDS
}
```

**PDO Processing Flow:**
```
SLCAN receives: COB-ID=0x182, data=[0x05, 0x00, 0x64, 0x00, ...]
       │
       ▼
FrameRouter: COBID 0x182 → TPDO1, NodeId=2
       │
       ▼
RemoteNode[2].PDO1.Process(data)
       │  EDS says PDO1 mapping:
       │    Byte 0-0 (8 bits) → 0x60FD:01 "Digital Inputs" (U8)
       │    Byte 1-2 (16 bits) → 0x6401:01 "Analog Input 0" (U16)
       │    Byte 3-4 (16 bits) → 0x6401:02 "Analog Input 1" (U16)
       ▼
PDOData {
    PdoNumber = 1,
    RawData = [0x05, 0x00, 0x64, 0x00],
    Values = {
        {"Digital Inputs", (byte)0x05},      // DI0=1, DI2=1
        {"Analog Input 0", (ushort)0x0000},  // 0V
        {"Analog Input 1", (ushort)0x0064}   // ~0.6V (scaled)
    }
}
       │
       ▼
User's handler(data) → data.Get<bool>("DI0") = true
```

### 4.12 SLCANTransport

```csharp
public interface ISLCANTransport
{
    bool IsOpen { get; }
    void Open();
    void Close();
    void Send(uint cobId, byte[] data, bool isExtended = false, bool isRemote = false);
    event EventHandler<CanFrame> FrameReceived;
}

public class SLCANSerialPortTransport : ISLCANTransport
{
    private SLCANSerialPort _port;  // wraps your existing CAN_SLCAN.cs
    
    public SLCANSerialPortTransport(string comPort, int baudRate = 500000) { ... }
    // ...
}

public class VirtualSLCANTransport : ISLCANTransport
{
    // In-process loopback for testing
    // Sends go directly to subscribers, no hardware needed
}
```

## 5. Registration API Summary

The "标准注册函数" manifests at three levels:

### Level 1: RemoteNode API (programmatic, for C# code)
```csharp
node.SDORead<ushort>(0x6401, 0x01);        // Read analog input by index
node.SDORead<ushort>("AI Channel 0");       // Read analog input by EDS name
node.OnPDO(1, OnPdoReceived);               // Subscribe to PDO events
```

### Level 2: TestCase function registration (for testcase_dll.funcs)
```csharp
node.RegisterToTestCase(tc, "IO_");
// Generates:
//   tc.funcs["IO_SDO_READ"]    → SDO read wrapper
//   tc.funcs["IO_SDO_WRITE"]   → SDO write wrapper
//   tc.funcs["IO_NMT_START"]   → NMT start
//   tc.funcs["IO_NMT_STOP"]    → NMT stop
//   tc.funcs["IO_PDO_MONITOR"] → poll PDO status
//   tc.funcs["IO_DI_READ"]     → read digital input channels (from EDS)
//   tc.funcs["IO_AI_READ"]     → read all analog input channels
//   tc.funcs["IO_DO_WRITE"]    → write digital output channels
//   tc.funcs["IO_HEARTBEAT"]   → check heartbeat status
```

### Level 3: Raw CAN subscription (for custom protocols)
```csharp
master.SubscribeRaw(0x300, 0x3FF, OnCustomFrame);
```

## 6. Receive Thread & Synchronization

```
SLCAN.DataReceived (serial port event thread)
       │
       ▼
FrameRouter.Dispatch(frame)  ← runs on event thread
       │
       ├─ NMT/Heartbeat → update node state (lock)
       ├─ SDO response  → signal waiting SDO caller (ManualResetEvent)
       └─ PDO/EMCY      → post to ConcurrentQueue, fire on timer
```

**Thread safety:**
- `SDOClient` uses `ManualResetEvent` to block the caller until the response arrives on the receive thread
- `PDOProcessor` fires events on a dedicated timer (not the serial port thread)
- Node state changes are protected by `lock(_stateLock)`
- `ConcurrentQueue<CanFrame>` for non-blocking frame dispatch

## 7. Error Handling

| Error | Signal | Recovery |
|-------|--------|----------|
| SDO timeout | `SDOException` | Retry × 3, then propagate |
| SDO abort | `SDOException` with abort code | Log abort code, abort transfer |
| Heartbeat loss | Event `HeartbeatLost` | User callback, auto-retry monitoring |
| SLCAN disconnect | Exception on next send | User calls `Start()` to reconnect |
| EDS file not found | `FileNotFoundException` | Node created without EDS (manual mode) |
| EDS parse error | `EDSParseException` with line info | Partial OD may still be available |

## 8. Implementation Roadmap

### Iteration 1: Foundation (~2 files)
- `CanOpenTypes.cs` — Enums, COB-ID helpers
- `CanFrame.cs` — Frame struct, builder
- `SLCANTransport.cs` — Interface + SLCAN adapter + Virtual adapter
- `FrameRouter.cs` — Dispatch engine

### Iteration 2: EDS Engine (~4 files)
- `DataTypeMap.cs` — Type conversion
- `ODEntry.cs` — Data model
- `ObjectDictionary.cs` — Storage + lookup
- `EDSParser.cs` — INI → ObjectDictionary (uses ini-parser)

### Iteration 3: Core Master (~4 files)
- `SDOClient.cs` — Expedited upload/download + timeout/retry
- `NmtMaster.cs` — Command send + state tracking
- `HeartbeatConsumer.cs` — Timeout monitoring
- `RemoteNode.cs` + `CanOpenMaster.cs` — Wires everything together

### Iteration 4: PDO & TestCase Integration (~3 files)
- `PDOMapping.cs` + `PDOProcessor.cs` — PDO reception with EDS mapping
- `RemoteNode.RegisterToTestCase()` — pointfun wrappers

## 9. Testing Strategy

### Unit tests (no hardware)
| Test | What it verifies |
|------|:----------------:|
| `COB-ID calculation` | `TPDO1(5) = 0x185`, `Heartbeat(3) = 0x703` |
| `EDS parsing` | Load a known EDS, verify all entries parsed correctly |
| `DataTypeMap round-trip` | `ToBytes(FromBytes(x)) == x` for all types |
| `SDO frame encode/decode` | Build request → parse response → verify all fields |
| `NMT frame` | Start(5) → `[0x01, 0x05]` on COB-ID 0 |
| `Heartbeat timeout` | Virtual transport + timer → event fires correctly |

### Integration tests (loopback)
| Test | What it verifies |
|------|:----------------:|
| Two VirtualSLCAN instances | SDO request → response round-trip |
| Master + virtual slave node | Full protocol cycle |

### Hardware tests (real device)
| Test | What it verifies |
|------|:----------------:|
| Connect to DS-401 I/O module | SDO read digital inputs, write digital outputs |
| Connect to analog module | SDO read analog channels, verify scaling |
| PDO reception | Configure PDO on slave, verify master receives |
| EDS load + SDO by name | Load device EDS, read parameter by name |
| Heartbeat monitoring | Slave stops → master detects timeout |

## 10. EDS file used for reference testing

A standard DS-401 Generic I/O module EDS file can be used for parser validation:

```ini
[FileInfo]
FileName=DS401_IO.eds
FileVersion=1
Description=CANopen Generic I/O Device
...
[DeviceInfo]
VendorID=0x00000000
ProductCode=0x00000000
...
[Index60FD]
ParameterName=Digital Inputs
ObjectType=0x07
DataType=0x05
AccessType=ro
...
[Index60FDSub0]
ParameterName=Number of entries
DataType=0x05
DefaultValue=1
...
[Index60FDSub1]
ParameterName=Digital Input 1-8
DataType=0x05
AccessType=ro
...
[Index6401]
ParameterName=Analog Inputs
ObjectType=0x08
DataType=0x00
...
[Index6401Sub0]
ParameterName=Number of entries
DataType=0x05
DefaultValue=4
...
[Index6401Sub1]
ParameterName=AI 0
DataType=0x06
AccessType=ro
...
[Index6401Sub2]
ParameterName=AI 1
DataType=0x06
AccessType=ro
```
