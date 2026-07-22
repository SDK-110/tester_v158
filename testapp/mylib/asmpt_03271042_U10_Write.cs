using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testapp.mylib
{
    internal class asmpt_03271042_U10_Write
    {
        private static readonly Dictionary<string, int> year_map = new Dictionary<string, int> {
            {"S",2024},
            {"T",2025},
            {"U",2026},
            {"V",2027},
            {"W",2028},
            {"X",2029}

        };
        private static readonly Dictionary<string, int> Month_map = new Dictionary<string, int> {
            {"1",1},
            {"2",2},
            {"3",3},
            {"4",4},
            {"5",5},
            {"6",6},
            {"7",7},
            {"8",8},
            {"9",9},
            {"O",10},
            {"N",11},
            {"D",12}

        };

        // 固定字段（前三行）
        public string CommonPart_Header { get; set; } = "0000000000000";
        public string EEPROM_Type { get; set; } = "4";
        public string EEPROM_Size { get; set; } = "2047";

        // 动态字段（按添加顺序写入）
        private  Dictionary<string, string> _dynamicFields = new  Dictionary<string, string>();

        // 添加或更新动态字段（保持顺序）
        public void SetField(string key, string value)
        {

            _dynamicFields[key] = value;
        }

        // 批量设置（可选）
        private void SetFields(Dictionary<string, string> fields)
        {
            foreach (var kv in fields)
            {
                SetField(kv.Key, kv.Value);
            }
        }

        // 直接覆盖写入文件
        public void Save(string filePath)
        {

            if (File.Exists(filePath)) { 
            
             File.Delete(filePath);
            
            }

            var lines = new List<string>
        {
            FormatLine("CommonPart_Header", CommonPart_Header),
            FormatLine("EEPROM_Type", EEPROM_Type),
            FormatLine("EEPROM_Size", EEPROM_Size)
        };

            // 添加所有动态字段（按设置顺序）
            foreach (var kv in _dynamicFields)
            {
                lines.Add(FormatLine(kv.Key, kv.Value));
            }
            string directoryPath = Path.GetDirectoryName(filePath);

            // 2. 若目录不存在则创建（若存在则不做任何操作，不会报错）
            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.WriteAllLines(filePath, lines);

        }

        // 格式化一行
        private static string FormatLine(string key, string value)
        {
            return $"{key} = \"{value}\"";
        }
        public void SetFields(string mac_base,string serial,string y_map,string m_map,string day,string fs ="05",string rs ="01") {

           CommonPart_Header = "0000000000000";
           EEPROM_Type = "4";
           EEPROM_Size = "2047";

           //动态字段（顺序即写入顺序）
           SetField("MAC_Base_Count", mac_base.PadLeft(4,'0'));
           SetField("Manufacture_Date", $"{year_map[y_map],4} {Month_map[m_map],2} {day,2}");
           SetField("Manufacture_code", "SDG");
           SetField("Serial_No", serial.PadLeft(6, '0'));  // 改个新值
           SetField("Material_No", "03271042");
           SetField("NewPart", "-");
           SetField("Functional_status", $"{ fs }");
           SetField("Revision_status", $"{ rs}");


        }

    }
}
