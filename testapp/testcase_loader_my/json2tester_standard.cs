using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using 重构程序.testcase_loader;

namespace rebuild.testcase_loader
{



    public   class json2tester_standard : tester_project
    {



        public static void save_json_test_cases(ref tester_project  sav,string project_tester_name="project_tester_name")
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{project_tester_name}.json", false))
            {


                file.Write(JsonConvert.SerializeObject(sav));


            }



        }


        public static void save_json_test_cases(ref List<tester_standard_style> sav,string tester_case_name="tester_case_name")
        {


            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{tester_case_name}.json", false))
            {


                file.Write(JsonConvert.SerializeObject(sav));


            }



        }


        public static  List<tester_standard_style> red_json_test_cases(string test_case_json_path)
        {

            try
            {
                string p = System.IO.File.ReadAllText(test_case_json_path);
                return JsonConvert.DeserializeObject<List<tester_standard_style>>(p);
            }
            catch
            {


                return null;
            }


        }


        public static tester_project  red_json_test_project(string tester_project_path)
        {

            try
            {
                string p = System.IO.File.ReadAllText(tester_project_path);
                return JsonConvert.DeserializeObject<tester_project>(p);
            }
            catch
            {


                return null;
            }


        }


    }
}
