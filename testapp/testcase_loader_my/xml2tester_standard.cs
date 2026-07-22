using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using 重构程序.testcase_loader;

namespace rebuild.testcase_loader
{
    class xml2tester_standard : tester_project
    {



        public static void save_xml_test_cases(ref List<tester_standard_style> sav, string savename= "test_cases.xml")
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{savename}.xml", false))
            {


               
                file.Write(Serializer<List<tester_standard_style>>(sav));


            }



        }

        public static void save_xml_project_test(ref tester_project sav, string savename = "test_cases")
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{savename}.xml", false))
            {



                file.Write(Serializer<tester_project>(sav));


            }



        }

        public static List<tester_standard_style> red_xml_test_cases(string test_case_xml_path)
        {

            try
            {
                string p = System.IO.File.ReadAllText(test_case_xml_path);
                return Deserialize<List<tester_standard_style>>(p);
            }
            catch
            {


                return null;
            }


        }


        public static tester_project red_xml_project_tester(string test_case_xml_path)
        {

            try
            {
                string p = System.IO.File.ReadAllText(test_case_xml_path);
                return Deserialize<tester_project>(p);
            }
            catch
            {


                return null;
            }


        }



        /// <summary>
        /// XML & Datacontract Serialize & Deserialize Helper
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialObject"></param>
        /// <returns></returns>
        public static string Serializer<T>(T serialObject) where T : class
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    System.IO.MemoryStream mem = new MemoryStream();
                    XmlTextWriter writer = new XmlTextWriter(mem, Encoding.UTF8);
                    ser.Serialize(writer, serialObject);
                    writer.Close();

                    return Encoding.UTF8.GetString(mem.ToArray());
                }
                catch (Exception ex)
                {
                    return null;
                }
            }

            public static T Deserialize<T>(string str) where T : class
            {
                try
                {
                    XmlSerializer mySerializer = new XmlSerializer(typeof(T));
                    StreamReader mem2 = new StreamReader(
                            new MemoryStream(System.Text.Encoding.UTF8.GetBytes(str)),
                            System.Text.Encoding.UTF8);

                    return (T)mySerializer.Deserialize(mem2);
                }
                catch (Exception)
                {
                    return null;
                }
            }

        }
    }


