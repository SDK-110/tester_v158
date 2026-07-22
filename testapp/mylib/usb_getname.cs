using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using NativeUsbLib;
namespace testapp
{
    class SerialProtFindHelper
    {
       public static IntPtr hDevInfo;
        /// <summary>
        /// 枚举win32 api
        /// </summary>
        private enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }

        public const UInt32 DIGCF_DEFAULT = 0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        public const UInt32 DIGCF_PRESENT = 0x00000002;
        public const UInt32 DIGCF_ALLCLASSES = 0x00000004;
        public const UInt32 DIGCF_PROFILE = 0x00000008;
        public const UInt32 DIGCF_DEVICEINTERFACE = 0x00000010;

        [DllImport("SetupAPI.dll")]
        public static extern IntPtr SetupDiGetClassDevs(
    ref Guid ClassGuid,
    UInt32 Enumerator,
    IntPtr hwndParent,
    UInt32 Flags
    );

        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
        public struct SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid ClassGuid;
            public IntPtr DevInst;
            public IntPtr Reserved;
        }




        [DllImport("SetupAPI.dll")]
        public static extern Boolean SetupDiEnumDeviceInfo(
            IntPtr DeviceInfoSet,
            UInt32 MemberIndex,
            ref SP_DEVINFO_DATA DeviceInfoData
        );

        [DllImport("SetupAPI.dll")]
        public static extern Boolean SetupDiGetDeviceInstanceId(
    IntPtr DeviceInfoSet,
    ref SP_DEVINFO_DATA DeviceInfoData,
    byte[] DeviceInstanceId,
    UInt32 DeviceInstanceIdSize,
    ref UInt32 RequiredSize
);

        [DllImport("SetupAPI.dll", CharSet = CharSet.Ansi)]
        public static extern Boolean SetupDiGetDeviceRegistryProperty(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            UInt32 Property,
            ref UInt32 PropertyRegDataType,
            byte[] PropertyBuffer,
            UInt32 PropertyBufferSize,
            ref UInt32 RequiredSize
        );

        [DllImport("SetupAPI.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr SetupDiOpenDevRegKey(
            IntPtr DeviceInfoSet,
            ref SP_DEVINFO_DATA DeviceInfoData,
            UInt32 Scope,
            UInt32 HwProfile,
            UInt32 KeyType,
            UInt32 samDesired
            );






        /// <summary>
        /// WMI取硬件信息
        /// </summary>
        /// <param name="hardType"></param>
        /// <param name="propKey"></param>
        /// <returns></returns>
        private static string[] MulGetHardwareInfo(HardwareEnum hardType, string propKey)
        {
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null && hardInfo.Properties[propKey].Value.ToString().Contains("COM"))
                        {
                            strs.Add(hardInfo.Properties[propKey].Value.ToString());
                        }

                    }
                    searcher.Dispose();
                }

                return strs.ToArray();
            }
            catch
            {
                return strs.ToArray();
            }
        }

        /// <summary>
        /// 串口信息
        /// </summary>
        /// <returns></returns>
        public static string[] GetSerialPort()
        {
            return MulGetHardwareInfo(HardwareEnum.Win32_PnPEntity, "Name");
        }

        public  static  string GetSerialport_fromName(string findstr)
        {

            int count = 0, m = 0;
            MatchCollection matchs;
            do
            {
                string[] serialgroup = SerialProtFindHelper.GetSerialPort();
                var tmp = (from bufstr in serialgroup where bufstr.IndexOf(findstr) >= 0 select bufstr).FirstOrDefault();
                if (tmp == null) continue;

                Regex rex = new Regex(@"(COM\d{1,3})", RegexOptions.IgnoreCase);
                matchs = rex.Matches(tmp);
                m = matchs.Count;
                if (m > 0)
                {


                    return matchs[0].Groups[1].Value;

                }


            } while (m < 0 && count++ < 5);

            return "null";
        }

        public static void get_USB_API() {
            SP_DEVINFO_DATA sDevInfoData;
            string strTemp="";
            sDevInfoData.cbSize = Marshal.SizeOf(new SP_DEVINFO_DATA());
            sDevInfoData.ClassGuid = Guid.Empty;
            sDevInfoData.DevInst = IntPtr.Zero;
            sDevInfoData.Reserved = IntPtr.Zero;
            int vid = 0;
            int pid = 0;
            uint nSize = 0;
            string strVidPid; //= string.Format("VID_{0:X4}&PID_{1:X4}", vid, pid);
            string strVid, strPid;
            if (vid == 0)
                strVid = "";
            else
                strVid = string.Format("VID_{0:X4}", vid);
            if (pid == 0)
                strPid = "";
            else
                strPid = string.Format("PID_{0:X4}", pid);
            strVidPid = strVid + "&" + strPid;
            strVidPid.ToUpper();
            byte[] PropertyBuffer = new byte[4096];
            UInt32 PropertyRegDataType = 0;
            Array.Clear(PropertyBuffer, 0, PropertyBuffer.Length);


            UInt32 dwFlag = (DIGCF_ALLCLASSES | DIGCF_PRESENT);
            Guid usbGuid = new Guid("4d36e978-e325-11ce-bfc1-08002be10318");
            
            hDevInfo = SetupDiGetClassDevs(ref usbGuid, 0, IntPtr.Zero, dwFlag);


            for (UInt32 i = 0; SetupDiEnumDeviceInfo(hDevInfo, i, ref sDevInfoData); i++)
            {

                IntPtr a = SetupDiOpenDevRegKey(hDevInfo, ref sDevInfoData, 1, 0, 1, 1);

               PropertyBuffer = new byte[1024];
                PropertyRegDataType = 0;
                Array.Clear(PropertyBuffer, 0, PropertyBuffer.Length);
                SetupDiGetDeviceRegistryProperty
                (
                    hDevInfo,
                    ref sDevInfoData,
                    12,
                    ref PropertyRegDataType, PropertyBuffer,
                    (UInt32)PropertyBuffer.Length,
                    ref nSize);
                if (nSize > 0) strTemp = System.Text.Encoding.Default.GetString(PropertyBuffer, 0, (int)nSize - 1);
               
                if (strTemp.ToUpper() == "USB")
                {

                }








            }







            //if (!SetupDiGetDeviceInstanceId(hDevInfo, ref sDevInfoData, PropertyBuffer, (UInt32)PropertyBuffer.Length, ref nSize))
            //{
            //    int zz = 1;
            //}
            //string   strTemp = System.Text.Encoding.Default.GetString(PropertyBuffer);






            // SetupDiGetDeviceRegistryProperty(
            //     hDevInfo,
            //     ref sDevInfoData,
            //     1,
            //     ref PropertyRegDataType, PropertyBuffer,
            //     (UInt32)PropertyBuffer.Length,
            //     ref nSize);
            //string   strTemp = System.Text.Encoding.Default.GetString(PropertyBuffer);



  

        }

        public static string []  get_usb_vid_pid_comname(string vid="", string pid="", int portnum=1)
        {

            UsbBus usbbus = new UsbBus();

            List<UsbDevice> usbdevs = usbbus.GetDeviceByVidPid(ushort.Parse(vid,System.Globalization.NumberStyles.HexNumber) /*0x1A86*/, ushort.Parse(pid, System.Globalization.NumberStyles.HexNumber) /*0x7523*/);

            if (usbdevs.Count == 0)
            {

                return new string[] { "-1", ""};
            }
            else {

                for (int i = 0; i < usbdevs.Count; i++) {


                    if (usbdevs[i].AdapterNumber == portnum) {

                        return new string[] { "0", usbdevs[i].ComPort};
                    }

                }

                return new string[] { "-2", "" };
            }

          

        }
    }
}
