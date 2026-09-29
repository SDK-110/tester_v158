using System;
using System.Collections.Generic;
using System.Management;
using System.Text.RegularExpressions;

namespace testapp.mylib
{
    /// <summary>
    /// COM 端口信息
    /// </summary>
    public class ComPortInfo
    {
        public string ComName;       // "COM3"
        public string FriendlyName;  // "Prolific USB-to-Serial Comm Port (COM4)"
        public string Vid;           // "067B"
        public string Pid;           // "2303"
        public string PnpDeviceId;   // 完整 PNPDeviceID

        public override string ToString()
        {
            return string.Format("{0} | Name={1} | VID={2} | PID={3}", ComName, FriendlyName, Vid, Pid);
        }
    }

    /// <summary>
    /// 通过 WMI 查询 Win32_PnPEntity 发现 COM 端口，
    /// 支持按友好名称模糊匹配和 VID/PID 精确匹配过滤。
    /// </summary>
    public class SerialPortDiscovery
    {
        /// <summary>
        /// 查询所有 COM 端口
        /// </summary>
        public static List<ComPortInfo> GetAllComPorts()
        {
            var result = new List<ComPortInfo>();
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%COM%'"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        var info = ParseComPort(obj);
                        if (info != null)
                            result.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                utility_func.callbackdebuginfo("[SerialPortDiscovery] GetAllComPorts WMI error: " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 按条件过滤 COM 端口
        /// </summary>
        /// <param name="namePattern">友好名称模糊匹配，为空则不过滤</param>
        /// <param name="vid">USB VID 十六进制，如 "067B"，为空则不过滤</param>
        /// <param name="pid">USB PID 十六进制，如 "2303"，为空则不过滤</param>
        public static List<ComPortInfo> FindPorts(string namePattern, string vid, string pid)
        {
            var all = GetAllComPorts();
            var result = new List<ComPortInfo>();

            foreach (var p in all)
            {
                bool match = true;

                if (match && !string.IsNullOrEmpty(namePattern))
                {
                    if (string.IsNullOrEmpty(p.FriendlyName) ||
                        p.FriendlyName.IndexOf(namePattern, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        match = false;
                    }
                }

                if (match && !string.IsNullOrEmpty(vid))
                {
                    if (string.IsNullOrEmpty(p.Vid) ||
                        !p.Vid.Equals(vid, StringComparison.OrdinalIgnoreCase))
                        match = false;
                }

                if (match && !string.IsNullOrEmpty(pid))
                {
                    if (string.IsNullOrEmpty(p.Pid) ||
                        !p.Pid.Equals(pid, StringComparison.OrdinalIgnoreCase))
                        match = false;
                }

                if (match)
                    result.Add(p);
            }

            return result;
        }

        /// <summary>
        /// 从 WMI 对象中解析 COM 端口信息
        /// </summary>
        private static ComPortInfo ParseComPort(ManagementBaseObject obj)
        {
            string name = obj["Name"] != null ? obj["Name"].ToString() : "";
            string pnpId = obj["PNPDeviceID"] != null ? obj["PNPDeviceID"].ToString() : "";

            var comMatch = Regex.Match(name, @"(COM\d{1,3})", RegexOptions.IgnoreCase);
            if (!comMatch.Success)
                return null;

            var info = new ComPortInfo();
            info.ComName = comMatch.Groups[1].Value;
            info.FriendlyName = name;
            info.PnpDeviceId = pnpId;

            var vpMatch = Regex.Match(pnpId, @"VID_([0-9A-Fa-f]{4})&PID_([0-9A-Fa-f]{4})", RegexOptions.IgnoreCase);
            if (vpMatch.Success)
            {
                info.Vid = vpMatch.Groups[1].Value;
                info.Pid = vpMatch.Groups[2].Value;
            }

            return info;
        }
    }
}
