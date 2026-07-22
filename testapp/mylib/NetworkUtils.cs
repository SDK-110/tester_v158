using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace NetworkUtils
{
    /// <summary>
    /// 用于查询指定描述的网络适配器当前链路速率的工具类
    /// </summary>
    public class NetworkAdapterSpeedChecker
    {
        /// <summary>
        /// 根据网卡的 Description（不区分大小写）查找并返回当前链路速率
        /// </summary>
        /// <param name="description">网卡描述文字（支持部分匹配）</param>
        /// <param name="useExactMatch">是否要求完全匹配（默认 false，使用 Contains 模糊匹配）</param>
        /// <returns>找到则返回格式化后的速率字符串；否则返回错误提示</returns>
        public string GetLinkSpeedByDescription(string description, bool useExactMatch = false)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                return "描述不能为空";
            }

            var nic = FindMatchingAdapter(description, useExactMatch);

            if (nic == null)
            {
                return $"未找到已连接的网卡匹配：{description}";
            }

            long speedBps = nic.Speed;
            return FormatSpeed(speedBps);
        }

        /// <summary>
        /// 根据网卡 Description 获取完整的网卡信息（包括速率、状态等）
        /// </summary>
        /// <param name="description">网卡描述文字</param>
        /// <param name="useExactMatch">是否完全匹配</param>
        /// <returns>找到则返回 AdapterInfo 对象；否则返回 null</returns>
        public AdapterInfo GetAdapterInfoByDescription(string description, bool useExactMatch = false)
        {
            var nic = FindMatchingAdapter(description, useExactMatch);
            if (nic == null) return null;
            
            return new AdapterInfo
            {
                Description = nic.Description,
                Name = nic.Name,
                SpeedBps = nic.Speed,
                SpeedHuman = FormatSpeed(nic.Speed),
                OperationalStatus = nic.OperationalStatus.ToString(),
                NetworkInterfaceType = nic.NetworkInterfaceType.ToString(),
                Id = nic.Id,
                mac_add = nic.GetPhysicalAddress().ToString()

            };
        }

        /// <summary>
        /// 获取所有已连接的物理网卡信息（供调试或选择用）
        /// </summary>
        public AdapterInfo[] GetAllConnectedAdapters()
        {
            var adapters = NetworkInterface.GetAllNetworkInterfaces()
                .Where(ni =>
                    ni.OperationalStatus == OperationalStatus.Up &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                    ni.Speed > 0)
                .Select(ni => new AdapterInfo
                {
                    Description = ni.Description,
                    Name = ni.Name,
                    SpeedBps = ni.Speed,
                    SpeedHuman = FormatSpeed(ni.Speed),
                    OperationalStatus = ni.OperationalStatus.ToString(),
                    NetworkInterfaceType = ni.NetworkInterfaceType.ToString(),
                    Id = ni.Id,
                    mac_add = ni.GetPhysicalAddress().ToString()
                })
                .ToArray();

            return adapters;
        }

        private NetworkInterface FindMatchingAdapter(string description, bool useExactMatch)
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();

            StringComparison comparison = StringComparison.OrdinalIgnoreCase;

            return interfaces.FirstOrDefault(ni =>
            {
                bool nameMatch = useExactMatch
                    ? ni.Description.Equals(description, comparison)
                    : ni.Description.IndexOf(description, comparison) >= 0;

                return nameMatch &&
                       ni.OperationalStatus == OperationalStatus.Up &&
                       ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                       ni.Speed > 0;  // 排除一些虚拟/未连接的适配器
            });
        }

        private static string FormatSpeed(long bps)
        {
            if (bps <= 0) return "未连接 / 未知";

            double value = bps;
            string unit = "bps";

            if (value >= 1_000_000_000)
            {
                value /= 1_000_000_000.0;
                unit = "Gbps";
            }
            else if (value >= 1_000_000)
            {
                value /= 1_000_000.0;
                unit = "Mbps";
            }
            else if (value >= 1_000)
            {
                value /= 1_000.0;
                unit = "Kbps";
            }

            return $"{value:F2} {unit}";
        }
    }

    /// <summary>
    /// 网卡信息 DTO
    /// </summary>
    public class AdapterInfo
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public long SpeedBps { get; set; }
        public string SpeedHuman { get; set; }
        public string OperationalStatus { get; set; }
        public string NetworkInterfaceType { get; set; }
        public string Id { get; set; }
        public string  mac_add { get; set; }

        public override string ToString()
        {
            return $"[{SpeedHuman}] {Description} ({Name}) - {OperationalStatus}";
        }
    }
}