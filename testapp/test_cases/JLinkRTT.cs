using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

// ================================================================
// J-Link RTT 双向通讯封装 — 直接 P/Invoke JLinkARM.dll
// 移植自 JLinkRTTOptimized 项目（已通过硬件验证）
//
// 核心改进（对比旧版）：
// 1. RTT 函数用直接 DllImport + Cdecl（旧版用 StdCall 动态加载）
// 2. ExecCommand 用 byte[]（旧版用 StringBuilder）
// 3. RTT 地址缓存 + RAM 扫描定位（旧版无缓存，每次走 DLL 自动搜索）
// 4. 读内存前 Halt CPU，扫描完恢复 Go（旧版不停 CPU）
// ================================================================
namespace testapp.test_cases
{
    public class JLinkRTT : IDisposable
    {
        // ============================================================
        // 常量
        // ============================================================

        private const string DLL = "JLinkARM.dll";
        private const CallingConvention CC = CallingConvention.Cdecl;

        // RTT Control 命令常量
        private const uint RTT_CMD_START     = 0;
        private const uint RTT_CMD_STOP      = 1;
        private const uint RTT_CMD_GETDESC   = 2;
        private const uint RTT_CMD_GETNUMBUF = 3;

        // 接口类型
        public const int TIF_JTAG = 0;
        public const int TIF_SWD  = 1;

        // RTT 地址缓存文件路径（exe 同目录）
        private static readonly string CacheFilePath =
            Path.Combine(AppContext.BaseDirectory, "rtt_cache.txt");

        // ============================================================
        // P/Invoke 声明 — 全部 Cdecl（移植自 JLinkApi.cs）
        // ============================================================

        [DllImport(DLL, CallingConvention = CC, CharSet = CharSet.Ansi)]
        private static extern IntPtr JLINKARM_Open();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern void JLINKARM_Close();

        [DllImport(DLL, CallingConvention = CC)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool JLINKARM_IsOpen();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_Connect();

        [DllImport(DLL, CallingConvention = CC)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool JLINKARM_IsConnected();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern uint JLINKARM_GetDLLVersion();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern uint JLINKARM_GetHardwareVersion();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern uint JLINKARM_GetSN();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_GetId();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern void JLINKARM_TIF_Select(int type);

        [DllImport(DLL, CallingConvention = CC)]
        private static extern void JLINKARM_SetSpeed(int speed);

        [DllImport(DLL, CallingConvention = CC, CharSet = CharSet.Ansi)]
        private static extern int JLINKARM_ExecCommand(
            string sIn, byte[] sError, int BufferSize);

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_ReadMem(
            uint addr, uint size, [Out] byte[] buf);

        /// <summary>
        /// 读取内存（逐字节方式）。pStatus 是 NumItems 个字节的数组。
        /// addr: 起始地址, leng: 读取字节数, buf: 接收缓冲区, status: 每个字节的状态数组
        /// </summary>
        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_ReadMemU8(
            uint addr, uint leng, byte[] buf, byte[] status);

        [DllImport(DLL, CallingConvention = CC)]
        private static extern void JLINKARM_WriteMem(
            uint addr, uint size, byte[] buf);

        // 调试运行控制：扫描 RAM 前需要 halt CPU
        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_Halt();

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINKARM_Go();

        [DllImport(DLL, CallingConvention = CC)]
        [return: MarshalAs(UnmanagedType.I1)]
        private static extern bool JLINKARM_IsHalted();

        // ── SEGGER 官方 RTT API（直接 DllImport，不再动态加载）──

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINK_RTTERMINAL_Control(
            uint Cmd, IntPtr pCmdData);

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINK_RTTERMINAL_Read(
            uint BufferIndex, byte[] pBuffer, uint BufferSize);

        [DllImport(DLL, CallingConvention = CC)]
        private static extern int JLINK_RTTERMINAL_Write(
            uint BufferIndex, byte[] pBuffer, uint NumBytes);

        // ============================================================
        // 状态
        // ============================================================

        private bool _isOpen = false, _connected = false, _disposed = false;
        private string _deviceName = "";
        private uint _cachedRttAddr = 0;

        // ============================================================
        // 属性
        // ============================================================

        public bool IsOpen => _isOpen;
        public bool IsConnected => _connected;
        public uint DllVersion { get; private set; }
        public uint HardwareVersion { get; private set; }
        public uint SerialNumber { get; private set; }

        // ============================================================
        // 连接管理
        // ============================================================

        /// <summary>打开 J-Link</summary>
        public void Open()
        {
            if (_isOpen) return;

            IntPtr ret = JLINKARM_Open();
            if (ret != IntPtr.Zero)
            {
                string err = Marshal.PtrToStringAnsi(ret);
                throw new JLinkException($"JLINKARM_Open 失败: {err}");
            }

            if (!JLINKARM_IsOpen())
                throw new JLinkException("JLINKARM_Open 失败");

            _isOpen = true;
            DllVersion = JLINKARM_GetDLLVersion();
            HardwareVersion = JLINKARM_GetHardwareVersion();
            SerialNumber = JLINKARM_GetSN();
        }

        /// <summary>关闭 J-Link 并停止 RTT</summary>
        public void Close()
        {
            try { RTT_Stop(); } catch { }
            _connected = false;
            if (_isOpen)
            {
                JLINKARM_Close();
                _isOpen = false;
            }
        }

        /// <summary>设置目标设备名（同时用于 RTT 地址缓存键）</summary>
        public void SetDevice(string name)
        {
            _deviceName = name;
            ExecCmd($"device = {name}");
        }

        public void SetSpeed(int khz) => JLINKARM_SetSpeed(khz);
        public void SetTIF(int tif) => JLINKARM_TIF_Select(tif);

        /// <summary>
        /// 连接目标 + 自动启动 RTT（内部调用 StartRttWithSmartAddress）
        /// </summary>
        public void Connect()
        {
            JLINKARM_Connect();
            _connected = JLINKARM_IsConnected();
            if (!_connected)
                throw new JLinkException("连接目标失败");

            // 自动启动 RTT（三级策略：缓存→DLL搜索→RAM扫描）
            if (!StartRttWithSmartAddress())
                throw new JLinkException("RTT 启动失败，请确认固件已包含 SEGGER_RTT 组件");
        }

        // ============================================================
        // RTT 读写（直接 P/Invoke Terminal API）
        // ============================================================

        /// <summary>读取目标发来的数据</summary>
        public byte[] Read(int maxSize = 4096)
        {
            byte[] buf = new byte[maxSize];
            int n = JLINK_RTTERMINAL_Read(0, buf, (uint)maxSize);
            if (n <= 0) return Array.Empty<byte>();
            byte[] result = new byte[n];
            Array.Copy(buf, result, n);
            return result;
        }

        /// <summary>读取字符串</summary>
        public string ReadString(int maxSize = 4096)
        {
            byte[] d = Read(maxSize);
            return d.Length > 0
                ? Encoding.UTF8.GetString(d).TrimEnd('\0', '\r', '\n')
                : "";
        }

        /// <summary>发送数据到目标</summary>
        public int Write(byte[] data)
        {
            if (data == null || data.Length == 0) return 0;
            return JLINK_RTTERMINAL_Write(0, data, (uint)data.Length);
        }

        /// <summary>发送字符串</summary>
        public int WriteString(string text, bool newLine = true)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            return Write(Encoding.UTF8.GetBytes(newLine ? text + "\n" : text));
        }

        // ============================================================
        // P/Invoke 安全包装（移植自 JLinkApi.cs）
        // ============================================================

        /// <summary>ExecCommand 用 byte[] 接收错误信息</summary>
        private (int ret, string error) ExecCmd(string cmd)
        {
            byte[] errBuf = new byte[256];
            int ret = JLINKARM_ExecCommand(cmd, errBuf, errBuf.Length);
            string err = null;
            int len = Array.IndexOf(errBuf, (byte)0);
            if (len > 0)
                err = Encoding.ASCII.GetString(errBuf, 0, len);
            return (ret, err ?? "");
        }

        /// <summary>安全读取内存，返回实际读取字节数，&lt;0 表示失败</summary>
        private int ReadMemSafe(uint addr, byte[] buf, int len)
        {
            if (len > buf.Length) len = buf.Length;
            if (len <= 0) return 0;
            return JLINKARM_ReadMem(addr, (uint)len, buf);
        }

        /// <summary>读取单个字节</summary>
        private byte ReadU8(uint addr)
        {
            byte[] buf = new byte[1];
            byte[] status = new byte[1];
            JLINKARM_ReadMemU8(addr, 1, buf, status);
            return buf[0];
        }

        /// <summary>
        /// 批量读取内存（逐字节方式）。一次 DLL 调用读取整块。
        /// 返回 true=成功(所有 status 均为 0 且返回值 >=0)，false=该地址区间读取失败。
        /// </summary>
        private bool ReadMemU8Batch(uint addr, byte[] buf, out byte status)
        {
            status = 0;
            if (buf == null || buf.Length == 0) return false;

            byte[] statusArr = new byte[buf.Length];
            int ret = JLINKARM_ReadMemU8(addr, (uint)buf.Length, buf, statusArr);
            if (ret < 0)
            {
                status = 0xFF;
                return false;
            }

            for (int i = 0; i < statusArr.Length; i++)
            {
                if (statusArr[i] != 0)
                {
                    status = statusArr[i];
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 启动 RTT，直接通过地址参数传入控制块。
        /// addr=0 则传 NULL 走 DLL 自动搜索。
        /// </summary>
        private int RTT_Start(uint addr)
        {
            if (addr == 0)
                return JLINK_RTTERMINAL_Control(RTT_CMD_START, IntPtr.Zero);

            IntPtr pAddr = Marshal.AllocHGlobal(sizeof(uint));
            try
            {
                Marshal.WriteInt32(pAddr, (int)addr);
                return JLINK_RTTERMINAL_Control(RTT_CMD_START, pAddr);
            }
            finally
            {
                Marshal.FreeHGlobal(pAddr);
            }
        }

        /// <summary>停止 RTT</summary>
        private int RTT_Stop()
        {
            return JLINK_RTTERMINAL_Control(RTT_CMD_STOP, IntPtr.Zero);
        }

        // ============================================================
        // RTT 地址管理（移植自 RttEngine.cs）
        // ============================================================

        /// <summary>
        /// 根据设备名返回 RAM 起始地址和建议扫描大小。
        /// </summary>
        private (uint start, uint size) GetRamRange()
        {
            string dev = _deviceName?.ToUpperInvariant() ?? "";

            if (dev.Contains("NRF52840"))
                return (0x20000000, 0x00040000); // 256 KB
            if (dev.Contains("NRF52833"))
                return (0x20000000, 0x00020000); // 128 KB
            if (dev.Contains("NRF52832"))
                return (0x20000000, 0x00020000); // 64 KB，扫 128 KB 留余量
            if (dev.Contains("NRF5340"))
                return (0x20000000, 0x00080000); // 512 KB

            if (dev.Contains("STM32F103"))
                return (0x20000000, 0x00008000); // 20/64 KB，扫 32 KB
            if (dev.Contains("STM32F407") || dev.Contains("STM32F401") || dev.Contains("STM32F411"))
                return (0x20000000, 0x00040000); // 192 KB
            if (dev.Contains("STM32F429") || dev.Contains("STM32F439"))
                return (0x20000000, 0x00040000); // 256 KB
            if (dev.Contains("STM32H743") || dev.Contains("STM32H750"))
                return (0x20000000, 0x00100000); // 1 MB
            if (dev.Contains("STM32H7"))
                return (0x20000000, 0x00100000); // 1 MB 系列

            if (dev.Contains("GD32F303"))
                return (0x20000000, 0x00010000); // 48/64/96 KB
            if (dev.Contains("GD32F407"))
                return (0x20000000, 0x00040000); // 192 KB

            // 默认保守扫描 1MB（覆盖绝大多数 Cortex-M）
            return (0x20000000, 0x00100000);
        }

        /// <summary>
        /// 用 JLINKARM_ReadMem 扫描 RAM 查找 "SEGGER RTT" 控制块。
        /// 扫描前 Halt CPU，扫描完恢复 Go。
        /// </summary>
        private uint FindRttAddress()
        {
            byte[] signature = Encoding.ASCII.GetBytes("SEGGER RTT");
            var (ramStart, scanSize) = GetRamRange();
            uint ramEnd = ramStart + scanSize;

            const int CHUNK = 4096;
            int overlap = signature.Length + 8;
            byte[] chunk = new byte[CHUNK];

            Debug.WriteLine($"[JLinkRTT] 定位 RTT 控制块: 0x{ramStart:X8}-0x{ramEnd:X8} (chunk={CHUNK})...");
            var sw = Stopwatch.StartNew();

            bool haltedByUs = false;
            try
            {
                if (!JLINKARM_IsHalted())
                {
                    JLINKARM_Halt();
                    haltedByUs = true;
                    Thread.Sleep(10);
                }

                uint addr = ramStart;
                while (addr < ramEnd)
                {
                    int readLen = (int)Math.Min(CHUNK, ramEnd - addr);
                    int ret = ReadMemSafe(addr, chunk, readLen);

                    if (ret < 0)
                    {
                        // 这块不可读，跳过
                        addr += (uint)CHUNK;
                        continue;
                    }

                    int scanLen = Math.Min(readLen, chunk.Length);
                    for (int i = 0; i <= scanLen - signature.Length; i++)
                    {
                        if (chunk[i] != signature[0]) continue; // 不是 'S'

                        bool match = true;
                        for (int j = 1; j < signature.Length; j++)
                        {
                            if (chunk[i + j] != signature[j]) { match = false; break; }
                        }

                        if (match)
                        {
                            uint foundAddr = addr + (uint)i;
                            Debug.WriteLine($"[JLinkRTT] 在 0x{foundAddr:X8} 找到 RTT 控制块（耗时 {sw.ElapsedMilliseconds}ms）");
                            return foundAddr;
                        }
                    }

                    int advance = readLen - overlap;
                    if (advance <= 0) advance = readLen;
                    addr += (uint)advance;
                }

                Debug.WriteLine($"[JLinkRTT] RAM 扫描未找到 RTT 控制块（耗时 {sw.ElapsedMilliseconds}ms）");
                return 0;
            }
            finally
            {
                if (haltedByUs)
                {
                    Thread.Sleep(5);
                    JLINKARM_Go();
                }
            }
        }

        /// <summary>从缓存文件加载指定设备的 RTT 地址。返回 0=未缓存。</summary>
        private uint LoadCachedAddr(string device)
        {
            try
            {
                if (!File.Exists(CacheFilePath)) return 0;
                foreach (string line in File.ReadAllLines(CacheFilePath))
                {
                    int eq = line.IndexOf('=');
                    if (eq > 0 && line.Substring(0, eq).Trim() == device)
                    {
                        string hexStr = line.Substring(eq + 1).Trim();
                        if (hexStr.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                            hexStr = hexStr[2..];
                        return Convert.ToUInt32(hexStr, 16);
                    }
                }
            }
            catch { }
            return 0;
        }

        /// <summary>保存 RTT 地址到缓存文件（按设备名区分）。</summary>
        private void SaveCachedAddr(string device, uint addr)
        {
            try
            {
                var lines = new List<string>();
                bool found = false;

                if (File.Exists(CacheFilePath))
                {
                    foreach (string line in File.ReadAllLines(CacheFilePath))
                    {
                        int eq = line.IndexOf('=');
                        if (eq > 0 && line.Substring(0, eq).Trim() == device)
                        {
                            lines.Add($"{device}=0x{addr:X8}");
                            found = true;
                        }
                        else if (!string.IsNullOrWhiteSpace(line))
                        {
                            lines.Add(line);
                        }
                    }
                }

                if (!found)
                    lines.Add($"{device}=0x{addr:X8}");

                File.WriteAllLines(CacheFilePath, lines);
                Debug.WriteLine($"[JLinkRTT] RTT 地址已缓存: {device}=0x{addr:X8}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[JLinkRTT] 缓存保存失败: {ex.Message}");
            }
        }

        /// <summary>
        /// RTT 地址智能解析 + 启动（三级策略）。
        ///
        /// 策略 A：有缓存地址 → RTT_Start(cachedAddr)，毫秒级启动
        /// 策略 B：无缓存 → DLL 自动搜索（RTT_START(0)）→ 搜索成功后扫描 RAM 定位地址并缓存
        /// </summary>
        private bool StartRttWithSmartAddress()
        {
            // ── 策略 A：使用缓存地址 ──
            uint cachedAddr = LoadCachedAddr(_deviceName);
            if (cachedAddr > 0)
            {
                Debug.WriteLine($"[JLinkRTT] 命中缓存 RTT 地址: 0x{cachedAddr:X8}，直接启动");
                int rttRet = RTT_Start(cachedAddr);

                if (rttRet >= 0)
                {
                    _cachedRttAddr = cachedAddr;
                    Debug.WriteLine("[JLinkRTT] RTT 启动成功（缓存地址）");
                    return true;
                }

                Debug.WriteLine($"[JLinkRTT] 缓存地址 0x{cachedAddr:X8} 启动失败 (ret={rttRet})，重新搜索...");
            }
            else
            {
                Debug.WriteLine("[JLinkRTT] 无缓存地址，使用 DLL 自动搜索...");
            }

            // ── 策略 B：DLL 自动搜索 + RAM 扫描定位缓存 ──
            int dllRet = RTT_Start(0);
            if (dllRet < 0)
            {
                Debug.WriteLine($"[JLinkRTT] RTT 启动失败 (ret={dllRet})");
                return false;
            }

            Debug.WriteLine("[JLinkRTT] RTT 启动成功（DLL 自动搜索）");

            // DLL 搜索成功后，用 ReadMem 扫描定位地址，找到就缓存
            uint foundAddr = FindRttAddress();
            if (foundAddr > 0)
            {
                _cachedRttAddr = foundAddr;
                SaveCachedAddr(_deviceName, foundAddr);
                Debug.WriteLine($"[JLinkRTT] 已缓存 RTT 地址: 0x{foundAddr:X8}");
            }
            else
            {
                Debug.WriteLine("[JLinkRTT] 未能定位控制块地址，本次不缓存");
            }

            return true;
        }

        // ============================================================
        // IDisposable
        // ============================================================

        public void Dispose()
        {
            if (_disposed) return;
            Close();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        ~JLinkRTT() { Dispose(); }
    }

    public class JLinkException : Exception
    {
        public JLinkException(string msg) : base(msg) { }
    }
}
