using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testapp.mylib
{


    public  class CfgGenerator
    {
        private static Mysql _mysql;
        public static Mysql MysqlInstance
        {
            get
            {
                if (_mysql == null)
                {
                    _mysql = new Mysql("127.0.0.1", "sg_test_db", "root", "root");
                }
                return _mysql;
            }
        }
        /// <summary>
        /// 生成CFG文件
        /// </summary>
        /// <param name="templatePath">模板CFG路径</param>
        /// <param name="outputPath">生成CFG路径</param>
        /// <param name="nodeMac">当前MAC (NODEID)</param>
        /// <param name="startMac">起始MAC</param>
        /// <param name="endMac">结束MAC</param>
        /// <param name="sn">序列号</param>
        public static void GenerateCfg(string outputPath,
                                       string nodeMac,
                                       string startMac,
                                       string endMac,
                                       string sn)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
// string text = File.ReadAllText(templatePath);

string text = @"NODEID = 00 E0 4C 68 00 01
STARTID = 00 E0 4C 68 00 01
ENDID = 00 E0 4C 68 FF FF
VID = 0B DA
PID = 81 53

;Do not change following parameters without Realtek approval
;
; 00: disable, 01: enable
SPI_FLASH_EN = 00
NO_REMOTE_WAKEUP = 00
GPHY_FLOW_CTRL = 01
;DOCKING = 00
LAN_WAKE_EN = 00
CTAPSHORT = 01
APPLE_LIGHTNING = 00
MAC_CLONE = 00
WU_EN = 00
FACTORY_MODE_EN = 00
;LED_SEL_CFG = Low-Byte High-Byte
LED_SEL_CFG = A9 7C
;Maximum MANUFACTURE string length allowed - 9 characters
MANUFACTURE = Realtek
;Maximum PRODUCT string length allowed - 19 characters
PRODUCT = USB 10/100/1000 LAN
;Serial Number
SN = 00 00 01
BCD_DEVICE = 31 00
LINK_CAPA = 06
EXT_PATCH = 00 00 00 00
VERSION_INFO = 1.029
";





            string node = FormatMac(nodeMac);
            string start = FormatMac(startMac);
            string end = FormatMac(endMac);
            string snFormat = FormatSN(sn);

            text = Regex.Replace(text, @"NODEID\s*=.*", $"NODEID = {node}");
            text = Regex.Replace(text, @"STARTID\s*=.*", $"STARTID = {start}");
            text = Regex.Replace(text, @"ENDID\s*=.*", $"ENDID = {end}");
            text = Regex.Replace(text, @"SN\s*=.*", $"SN = {snFormat}");

            File.WriteAllText(outputPath, text, Encoding.ASCII);
        }

        /// <summary>
        /// MAC转换为CFG格式
        /// </summary>
        private static string FormatMac(string mac)
        {
            mac = mac.Replace("-", "")
                     .Replace(":", "")
                     .Replace(" ", "");

            if (mac.Length != 12)
                throw new Exception("MAC地址长度错误");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 12; i += 2)
            {
                sb.Append(mac.Substring(i, 2).ToUpper());
                if (i != 10)
                    sb.Append(" ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// SN格式转换
        /// </summary>
        private static string FormatSN(string sn)
        {
            sn = sn.Replace(" ", "");

            if (sn.Length % 2 != 0)
                throw new Exception("SN必须是偶数HEX长度");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < sn.Length; i += 2)
            {
                sb.Append(sn.Substring(i, 2).ToUpper());
                if (i < sn.Length - 2)
                    sb.Append(" ");
            }

            return sb.ToString();
        }



        public static (string sn, string mac) ReadSnMac()
        {
            string dbname = "sg_test_db";
            string tablename = "havis_mac_sn";



            DataTable rst = MysqlInstance.Query($"select sn,mac from {tablename} limit 1");

            if (rst.Rows.Count == 0)
                throw new Exception("数据库没有数据");

            string sn = rst.Rows[0]["sn"].ToString();
            string mac = rst.Rows[0]["mac"].ToString();

            return (sn, mac);
        }


        private static string SnAdd1(string sn)
        {
            int num = int.Parse(sn);
            num++;

            return num.ToString("D6"); //保持6位
        }

        private static string MacAdd1(string mac)
        {
            string hex = mac.Replace(":", "");

            ulong value = Convert.ToUInt64(hex, 16);

            value++;

            string newHex = value.ToString("X12");

            return string.Join(":", Enumerable.Range(0, 6)
                .Select(i => newHex.Substring(i * 2, 2)));
        }
        public static int IncreaseSnMac()
        {
            string dbname = "sg_test_db";
            string tablename = "havis_mac_sn";

            

            DataTable rst = MysqlInstance.Query($"select sn,mac from {tablename} limit 1");

            if (rst.Rows.Count == 0)
                return -1;

            string sn = rst.Rows[0]["sn"].ToString();
            string mac = rst.Rows[0]["mac"].ToString();

            string newSn = SnAdd1(sn);
            string newMac = MacAdd1(mac);

            string sql = $"update {tablename} set sn='{newSn}', mac='{newMac}' where id=1";

            return MysqlInstance.ExecNonQuery(sql);
        }

    }

}