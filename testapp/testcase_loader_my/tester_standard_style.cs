using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using testapp;

namespace rebuild.testcase_loader
{
  public   enum judge_result {pass,fail,skip};

   
  //  public delegate string pointfun(string high, string low, out string result, string parameter="");
    public delegate void callback_this(tester_standard_style tester,int id);
    [XmlRoot("tester_standard_style")]
    public  class tester_standard_style
    {
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int jump_loop_flog;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
       public  callback_this tf_handler;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public bool is_teshu_logger = false;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        string testcase_high_limit_private;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public bool runned = false;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public int chuanshuid;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public double runtime;
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string utc_long="";
        public tester_standard_style()
        {
            runned = false;
        }
        [XmlElement("id")]
        public int id { get; set; }
        [XmlElement("testcase_description")]
        public  string testcase_description { get; set; }
        [XmlElement("testcase_high_limit")]
        public string testcase_high_limit { get { return testcase_high_limit_private; }
                                            set {
                                                   
                                                   testcase_high_limit_private = value;
                                                 }
                                                }
        [XmlElement("testcase_low_limit")]
        public string testcase_low_limit { get; set; }
        string testcase_test_result { get { return test_result; } set { test_result = value; }}
        string test_result;
        judge_result testcase_judge_result;
        [XmlElement("test_lib_string")]
        public string test_lib_string { get; set; }
        [XmlElement("parameter")]
        public  string parameter { get; set; }
        [XmlElement("test_spik")]
        public string test_spik { get; set; }
        [XmlElement("repeter_goto")]
        public string repeat_goto { get; set; }
        [XmlElement("self_run_count")]
        public string self_run_count { get; set; }
        public tester_standard_style(string testcase_description,
                                     string testcase_high_limit,
                                     string testcase_low_limit,
                                     string parameter = "",
                                     string test_lib_string = "",
                                     string test_spik="0",
                                     string repeat_goto="0",
                                     string self_run_count="0"
                                     )
        {
            this.testcase_description = testcase_description;
            this.testcase_high_limit = testcase_high_limit;
            this.testcase_low_limit = testcase_high_limit;
            this.test_lib_string = test_lib_string;
            this.parameter = parameter;
            this.test_spik = test_spik;
            this.repeat_goto = repeat_goto;
            this.self_run_count = self_run_count;
        }

        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string get_judge_result {
            get {
                if (!runned) return "null";
                if (testcase_judge_result == judge_result.fail) { return "fail"; }
                else if (testcase_judge_result == judge_result.skip) { return "skip"; }
                else
                {
                    return "pass";
                }

            }
            set {


                testcase_judge_result = (value == "fail") ? judge_result.fail : judge_result.pass;
            }

        }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string parallel_get_result
        {
            get
            {
                return testcase_test_result;

            }
      

        }


        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string parallel_judge_result_msg { set; get; }


            [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public string result_msg
        {
            get
            {

                return this.testcase_test_result;

            }

            set {



                testcase_test_result = value;

            }

        }
        public judge_result get_rusult(ref Dictionary<string, pointfun> lib)
        {

            runned = true;
            this.testcase_judge_result = judge_result.fail;
            if (lib == null || this.test_lib_string == "")
            {
                this.testcase_test_result = "error_testcase_is_empty";
                return judge_result.fail;

            }
            else
            {

                if (this.test_spik == "0") {
                    this.testcase_test_result = "test_skip";
                    this.testcase_judge_result = judge_result.skip;
                }
                else {
                    runtime = DateTime.Now.Ticks;
                    utc_long = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    if (this.test_lib_string.IndexOf("@")>=0) { this.is_teshu_logger = true; }
                    string stp = this.test_lib_string.Replace("@", "").Replace("!!","");
                this.testcase_judge_result = (lib[stp](testcase_high_limit, testcase_low_limit, out test_result, this.parameter) == "pass") ? judge_result.pass : judge_result.fail;
                    runtime = (DateTime.Now.Ticks - runtime) / 10000.0000;
                if (this.testcase_judge_result == judge_result.pass)
                {
                    this.testcase_test_result = test_result;

                }
                else {
                    this.testcase_test_result = test_result;

                }

                }
                if (tf_handler != null) tf_handler(this, chuanshuid);
                return this.testcase_judge_result;


            }



        }

        public judge_result parallel_get_rusult(ref Dictionary<string, pointfun> lib)
        {

            runned = true;
            this.testcase_judge_result = judge_result.fail;
            if (lib == null || this.test_lib_string == "")
            {
                this.testcase_test_result = "error_testcase_is_empty";
                return judge_result.fail;

            }
            else
            {

                if (this.test_spik == "0")
                {
                    this.testcase_test_result = "test_skip";
                    this.testcase_judge_result = judge_result.skip;
                }
                else
                {
                    runtime = DateTime.Now.Ticks;
                    utc_long = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                    if (this.test_lib_string.IndexOf("@") >= 0) { this.is_teshu_logger = true; }
                    string stp = this.test_lib_string.Replace("@", "");
                    parallel_judge_result_msg = (lib[stp](testcase_high_limit, testcase_low_limit, out test_result, this.parameter));
                    if (parallel_judge_result_msg.ToUpper() == "pass".ToUpper() || (parallel_judge_result_msg.IndexOf(',') > 0 && parallel_judge_result_msg.ToUpper().IndexOf('F') < 0)) {
                        this.testcase_judge_result = judge_result.pass;
                    }
                    else {

                        this.testcase_judge_result = judge_result.fail;

                    }
                 //   this.testcase_judge_result = (parallel_judge_result_msg == "p,p,p,p".ToUpper()) ? judge_result.pass : judge_result.fail;
                    runtime = (DateTime.Now.Ticks - runtime) / 10000.0000;
            

                }
                if (tf_handler != null) tf_handler(this, chuanshuid);
                return this.testcase_judge_result;


            }



        }
    }
}
