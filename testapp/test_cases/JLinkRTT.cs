using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

// ================================================================
// J-Link RTT 双向通讯封装 — 直接 P/Invoke JLinkARM.dll
// 优先使用 Terminal API，不支持时降级手动模式
// 移植自: D:\Temp\liguan\JLinkRTTDemo\JLinkRTT.cs
// ================================================================
namespace testapp.test_cases
{
    public class JLinkRTT : IDisposable
    {
        // ============================================================
        // DLL 导入
        // ============================================================

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary(string name);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string name);
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_Open();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_Close();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool JLINKARM_IsOpen();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool JLINKARM_IsConnected();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_Connect();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint JLINKARM_GetDLLVersion();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint JLINKARM_GetHardwareVersion();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern uint JLINKARM_GetSN();
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_TIF_Select(int type);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_SetSpeed(int speed);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int JLINKARM_ExecCommand([In] string sIn, StringBuilder sOut, int outSize);

        // 内存读写（手动模式）
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int JLINKARM_ReadMem(uint addr, uint size, [Out] byte[] buf);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_ReadMemU32(uint addr, uint count, ref uint buf, ref byte status);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_WriteU32(uint addr, uint dat);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_ReadMemU8(uint addr, uint count, ref byte buf, ref byte status);
        [DllImport("JLinkARM.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void JLINKARM_WriteU8(uint addr, byte dat);

        // ============================================================
        // Terminal API 委托 — 动态加载
        // ============================================================

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DLL_RTT_Read(int idx, IntPtr buf, int size);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DLL_RTT_Write(int idx, IntPtr buf, int len);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DLL_RTT_Control(int cmd, IntPtr cfg);

        private DLL_RTT_Read _rttRead;
        private DLL_RTT_Write _rttWrite;
        private DLL_RTT_Control _rttControl;
        private bool _useTerminalApi = false;

        // ============================================================
        // 手动模式状态
        // ============================================================

        private const int RTT_CB_ACID_OFFSET = 0x00;
        private const int RTT_CB_AUP_OFFSET = 0x18;
        private const int RTT_BUFFER_ENTRY_SIZE = 0x18;
        private const int BUF_PBUFFER_OFFSET = 0x04;
        private const int BUF_SIZE_OFFSET = 0x08;
        private const int BUF_WROFF_OFFSET = 0x0C;
        private const int BUF_RDOFF_OFFSET = 0x10;
        private const string RTT_ID_STRING = "SEGGER";

        private bool _rttReady = false;
        private uint _aUp0_mask = 0;
        private uint _aDown0_bufAddr = 0;
        private uint _aDown0_mask = 0;

        // ============================================================
        // 属性
        // ============================================================

        private bool _isOpen = false, _connected = false, _disposed = false;
        public bool IsOpen => _isOpen;
        public bool IsConnected => _connected;
        public bool RttReady => _rttReady;
        public bool UseTerminalApi => _useTerminalApi;
        public uint DllVersion { get; private set; }
        public uint HardwareVersion { get; private set; }
        public uint SerialNumber { get; private set; }

        // ============================================================
        // 连接管理
        // ============================================================

        public void Open()
        {
            if (_isOpen) return;
            EnsureDllLoaded();
            JLINKARM_Open();
            if (!JLINKARM_IsOpen())
                throw new JLinkException("JLINKARM_Open 失败");
            _isOpen = true;
            DllVersion = JLINKARM_GetDLLVersion();
            HardwareVersion = JLINKARM_GetHardwareVersion();
            SerialNumber = JLINKARM_GetSN();
        }

        public void Close()
        {
            _rttReady = false;
            _connected = false;
            if (_isOpen) { JLINKARM_Close(); _isOpen = false; }
        }

        public void SetDevice(string name) => Cmd($"device = {name}");
        public void SetSpeed(int khz) => Cmd($"Speed = {khz}");
        public void SetTIF(int tif) => JLINKARM_TIF_Select(tif);

        public void Connect()
        {
            JLINKARM_Connect();
            _connected = JLINKARM_IsConnected();
            if (!_connected) throw new JLinkException("连接目标失败");
        }

        /// <summary>初始化 RTT（使用 Terminal API）</summary>
        public bool EnableTerminalApi()
        {
            if (!_useTerminalApi) return false;
            if (_rttControl != null)
                _rttControl(0, IntPtr.Zero);
            return TestTerminalApi(100);
        }

        /// <summary>校验 Terminal API 是否能读到数据</summary>
        public bool TestTerminalApi(int timeoutMs = 50)
        {
            if (!_useTerminalApi) return false;
            for (int i = 0; i < Math.Max(1, timeoutMs / 10); i++)
            {
                IntPtr buf = Marshal.AllocHGlobal(16);
                try
                {
                    if (_rttRead(0, buf, 16) > 0)
                        return true;
                }
                finally { Marshal.FreeHGlobal(buf); }
                Thread.Sleep(10);
            }
            return false;
        }

        // ============================================================
        // RTT 读写
        // ============================================================

        /// <summary>读取目标发来的数据</summary>
        public byte[] Read(int maxSize = 4096)
        {
            if (_useTerminalApi)
            {
                IntPtr buf = Marshal.AllocHGlobal(maxSize);
                try
                {
                    int n = _rttRead(0, buf, maxSize);
                    if (n <= 0) return Array.Empty<byte>();
                    byte[] result = new byte[n];
                    Marshal.Copy(buf, result, 0, n);
                    return result;
                }
                finally { Marshal.FreeHGlobal(buf); }
            }

            if (!_rttReady) return Array.Empty<byte>();
            // 手动模式（暂无 CB 地址时不执行）
            return Array.Empty<byte>();
        }

        /// <summary>读取字符串</summary>
        public string ReadString(int maxSize = 4096)
        {
            byte[] d = Read(maxSize);
            return d.Length > 0 ? Encoding.UTF8.GetString(d).TrimEnd('\0', '\r', '\n') : "";
        }

        /// <summary>发送数据到目标</summary>
        public int Write(byte[] data)
        {
            if (data == null || data.Length == 0) return 0;
            if (_useTerminalApi)
            {
                IntPtr buf = Marshal.AllocHGlobal(data.Length);
                try
                {
                    Marshal.Copy(data, 0, buf, data.Length);
                    return _rttWrite(0, buf, data.Length);
                }
                finally { Marshal.FreeHGlobal(buf); }
            }
            return 0;
        }

        /// <summary>发送字符串</summary>
        public int WriteString(string text, bool newLine = true)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            return Write(Encoding.UTF8.GetBytes(newLine ? text + "\n" : text));
        }

        // ============================================================
        // 内部方法
        // ============================================================

        private void EnsureDllLoaded()
        {
            if (_hDll != IntPtr.Zero) return;
            _hDll = LoadLibrary("JLinkARM.dll");
            if (_hDll == IntPtr.Zero)
                throw new DllNotFoundException("JLINKARM.dll 未找到");

            _rttRead = TryLoad<DLL_RTT_Read>("JLINK_RTTERMINAL_Read");
            _rttWrite = TryLoad<DLL_RTT_Write>("JLINK_RTTERMINAL_Write");
            _rttControl = TryLoad<DLL_RTT_Control>("JLINK_RTTERMINAL_Control");
            _useTerminalApi = (_rttRead != null && _rttWrite != null);
        }

        private T TryLoad<T>(string name) where T : class
        {
            IntPtr p = GetProcAddress(_hDll, name);
            return p != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<T>(p) : null;
        }

        private string Cmd(string s)
        {
            var sb = new StringBuilder(1024);
            int ret = JLINKARM_ExecCommand(s, sb, sb.Capacity);
            return ret == 0 ? sb.ToString().TrimEnd('\0', '\r', '\n') : null;
        }

        // ============================================================
        // IDisposable
        // ============================================================

        private IntPtr _hDll = IntPtr.Zero;

        public void Dispose()
        {
            if (_disposed) return;
            Close();
            if (_hDll != IntPtr.Zero) { FreeLibrary(_hDll); _hDll = IntPtr.Zero; }
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
