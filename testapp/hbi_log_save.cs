using IniParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using testapp.glob_set;

namespace testapp
{
    enum hbi_items
    {
        sn, driverid, uuid

    }
   
    class hbi_log_tool
    {
   
        private IniParser.Model.IniData iniData = glob_ini_instance.getInstance().getSetupIniData;
        string test_stestion_type = "";
        // Dictionary<hbi_items, string> logger = new Dictionary<hbi_items, string>();
        test_log_record tlr = new test_log_record();
   
        public hbi_log_tool(string serial_number="", string mac = "", string start_utc_str="", string final_status="FAIL" )
        {
            tlr.start_time_utc = start_utc_str;
            int station_type=0, station_id_select=0;
            var TestStestionType = iniData["setproduct"]["hbi_test_stestion_type"];
            if (TestStestionType != null)
            {

                station_type = 0;
                station_id_select = 0;
            }
            else {


                station_type = 1;
                station_id_select = 1;
            }


            switch (station_type) {
                case 0:
                    {
                        tlr.test_station_type = "board-level-test";
                    }
                    break;
                case 1:
                    {
                        tlr.test_station_type = "system-level-test";

                    }
                    break;
            }
            switch (station_id_select) {
            case 0: {
                tlr.test_station_id = "SGW_PCB1";
                }
                    break;
                case 1:
                  {

                tlr.test_station_id = "SGW_FG";
              }
                    break;
            }
            if (serial_number == "") {
                tlr.serial_number = mac.Substring(0, 2) + ":" + mac.Substring(2, 2) + ":" + mac.Substring(4, 2) + ":" +
                                  mac.Substring(6, 2) + ":" + mac.Substring(8, 2) + ":" + mac.Substring(10, 2); ;
            } else {
                tlr.serial_number = serial_number;
            }
         
                tlr.mac_address = mac.Substring(0, 2) + ":" + mac.Substring(2, 2) + ":" + mac.Substring(4, 2) + ":" +
                                  mac.Substring(6, 2) + ":" + mac.Substring(8, 2) + ":" + mac.Substring(10, 2);
               tlr.final_status = final_status;





        }
        public void add_item(hbi_items item, string save_str) {

           // logger[item] = save_str;

        }
        public void add_item(string test_id, string test_duration,string test_start_time,string status,object observation)
        {

            var  tcr = new test_case_record();
            tcr.test_id = test_id;
            tcr.test_duration = test_duration;
            tcr.test_start_time_utc = test_start_time;
            tcr.status = status;
            tcr.add_test_observation(observation);
            tlr.add_test_result(tcr);
            
        }
        public void log_csv_save(string path = "../hbi/log/",int if_pcba=0)
        {
            string save_header = "";
            if (if_pcba == 0)
            {

                save_header = tlr.mac_address;
            }
            else {

                save_header = tlr.serial_number.Replace(":","");
            }
            
            string filename = save_header.Replace(":","") + "_" + DateTime.Now.Year.ToString().PadLeft(4, '0') +
                                                 DateTime.Now.Month.ToString().PadLeft(2, '0') +
                                                 DateTime.Now.Day.ToString().PadLeft(2, '0') +
                                                  DateTime.Now.Hour.ToString().PadLeft(2, '0') +
                                                  DateTime.Now.Minute.ToString().PadLeft(2, '0') +
                                                   DateTime.Now.Second.ToString().PadLeft(2, '0') +
                                                   DateTime.Now.Millisecond.ToString().PadLeft(3, '0')+".json";


            if (tlr.final_status == "PASS")
            {

                path = path+ @"\OK";
            }
            else {

                path = path + @"\NG";
            }
            if (!Directory.Exists(path + @"\"))
            {

                Directory.CreateDirectory(path);
            }
            using (StreamWriter sw = new StreamWriter((path.Length > 0) ? path + @"\" + filename : filename, true))
            {

                string p = JsonConvert.SerializeObject(tlr);

                sw.Write(p);

            }

        }
       

        string get_station_type() {


            return "";
        }

        ~hbi_log_tool()
        {
            
        }
    }

 

    class test_log_record {
        [JsonProperty(Order =1)]
        public string start_time_utc { get; set; } = mylib.utility_func.get_utc_str();
        [JsonProperty(Order = 2)]
        public string test_run_id { get; set; } = mylib.utility_func.get_uuid_str();
        [JsonProperty(Order = 3)]
        public string test_station_type { get; set; }
        [JsonProperty(Order = 4)]
        public string test_station_site { get; set; } = "Season";
        [JsonProperty(Order = 5)]
        public string test_station_id { get; set; }
        [JsonProperty(Order = 6)]
        public string test_sdk_version { get; set; } = "hbi_test_v0.0.1";
        [JsonProperty(Order = 7)]
        public string serial_number { get; set; }
        [JsonProperty(Order = 8)]
        public int device_type { get; set; } = 3;
        [JsonProperty(Order = 9)]
        public string mac_address { get; set; }
      //  [JsonProperty(Order = 10)]
      //  public string secret_key { get; set; }
        [JsonProperty(Order = 10)]
        public string final_status { get; set; } = "FAIL";
        [JsonProperty(Order = 11)]
        public ArrayList test_results = new ArrayList();

        public void add_test_result(object rsu) {

            test_results.Add(rsu);

        }


      
    }

    class test_case_record
    {
        [JsonProperty(Order = 1)]
        public string test_id { set; get; }
        [JsonProperty(Order = 2)]
        public string status { set; get; } = "FAIL";
        [JsonProperty(Order = 3)]
        public string test_start_time_utc { set; get; } = mylib.utility_func.get_utc_str_long();
        [JsonProperty(Order = 4)]
        public string test_duration { set; get; }
        [JsonProperty(Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public object observations;
        //public ArrayList observations = new ArrayList();

        public void add_test_observation(object rsu) {

            observations = rsu;
            //observations.Add(rsu);
        }

    }

}
