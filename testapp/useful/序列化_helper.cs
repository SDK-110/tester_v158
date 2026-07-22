using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

using System.Xml.Serialization;

namespace testapp.useful
{
    public class json_helper
    {
        public static void SaveToJson<T>(T t, string AssetPath)
        {
            //讲对象转化为Json字符串
            string JsonData = JsonConvert.SerializeObject(t);
            string filePath = Application.StartupPath + AssetPath;

            if (!File.Exists(filePath))
            {
                FileStream f = File.Create(filePath);
                f.Close();
            }
            //打开文件流，create模式表示如果不存在则创建，如果存在则覆盖
            FileStream fs = File.Open(filePath, FileMode.Create);
            //创建StreamWriter
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
            //写入Json字符
            sw.Write(JsonData);
            //清空缓冲区，确保写入
            sw.Flush();
            //关闭StreamWriter
            sw.Close();

        }

        public static T loadjson_file<T>(string file, T obj)
        {

            string jsonFromFile = File.ReadAllText(file);
            return (T)JsonConvert.DeserializeObject<T>(jsonFromFile);


        }
    }


  public   class XmlHelper
    {
        public static void SerializeToXml<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public static T DeserializeFromXml<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
