using rebuild.testcase_loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace 重构程序.testcase_loader
{
    [XmlRoot("tester_project")]
    public class tester_project
    {
        [XmlElement("project")]
        public string project;
        [XmlElement("project_tester_name")]
        public string project_tester_name;
        [XmlElement("test_cases")]
        public List<tester_standard_style> test_cases;

        public void clear_result() {

            for (int i = 0; i < test_cases.Count; i++) {

                test_cases[i].get_judge_result = "null";
                test_cases[i].result_msg = "null";
                test_cases[i].runned = false;
                test_cases[i].id = i;
                test_cases[i].runtime = 0;
                test_cases[i].jump_loop_flog = 1;
            }


        }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public tester_standard_style this[int i]{

            get {

                test_cases[i].chuanshuid = i;
                return test_cases[i];

            }

         }
    }
}
