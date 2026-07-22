using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Security.Policy;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using IniParser;
using testapp.glob_set;

namespace testapp.mylib.json_upload2
{





    public class mes_uploder
    {
        private IniParser.FileIniDataParser iniread =  glob_ini_instance.getInstance().fileIni;
        string url, sourceId, devCode;
        List<Param>  _params_ = new  List<Param>();
        public mes_uploder()
        {
            devCode = glob_ini_instance.getInstance().getSetupIniData["MES_SET"]["deviceID"];
            sourceId = glob_ini_instance.getInstance().getSetupIniData["MES_SET"]["sourceID"];
            url = glob_ini_instance.getInstance().getSetupIniData["MES_SET"]["url"];

            var p = glob_ini_instance.getInstance().getSetupIniData["_params_"];

            foreach (var m in p)
            {
                _params_.Add(new Param() { FieldName = m.KeyName, FieldValue = m.Value });
            }
        }

        public (string, string) _mes_uploder(string _lbId = "sn_123456789", string _partNum = "xxxxxxxxxxxx")
        {


            uint ts = get_time_strip();
            string url_str = this.url;
            Request_data updata = new Request_data
            {
                requestTime = ts,
                sourceId = this.sourceId,
                data = new _data { devCode = this.devCode },
                curLbs = new List<lb>() { new lb { lbId = _lbId, partNum = _partNum } },
                _params = _params_ ?? new List<Param>() { new Param { FieldName = "xxxx", FieldValue = "xxxx" } }

            };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(updata).Replace("_params", "params");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(3);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = client.PostAsync(url_str, content).Result;
                    string pattern = @"""resultCode"":""(\d{4})""";
                    if (response.IsSuccessStatusCode)
                    {
                        string rst = response.Content.ReadAsStringAsync().Result.Replace("\\", "");
                        Regex regex = new Regex(pattern);
                        Match match = regex.Match(rst);
                        testapp.mylib.utility_func.callbackdebuginfo("server return : \n" + rst);
                        if (match.Success)
                        {
                            string resultCode = match.Groups[1].Value;

                            testapp.mylib.utility_func.callbackdebuginfo("Extracted result code: " + resultCode);
                            if (resultCode == "0000")
                            {

                                return ("pass", resultCode);
                            }

                            if (resultCode == "2001" || resultCode == "2004" || resultCode == "2003")
                            {


                                testapp.mylib.utility_func.WinExec("@lock.exe  Error!_{resultCode}", 1);

                            }

                            return ("fail", resultCode);

                        }
                        else
                        {

                            testapp.mylib.utility_func.callbackdebuginfo("seriver_return string No match found");

                            return ("fail", "server_error");
                        }
                    }
                    else
                    {

                        testapp.mylib.utility_func.callbackdebuginfo("Failed to send data. Status code: " + response.StatusCode);
                        return ("fail", response.StatusCode.ToString());
                    }
                }
            }
            catch
            {

                testapp.mylib.utility_func.callbackdebuginfo("server timeout, it delay more than 2s");
            }
            return ("fail", "_error_timeout");

        }






        public static uint get_time_strip()
        {






            DateTimeOffset currentTime = DateTimeOffset.Now;
            uint timestamp = (uint)currentTime.ToUnixTimeMilliseconds();
            return timestamp;


        }

        class Request_data
        {

            public uint requestTime { set; get; }
            public string sourceId { set; get; }
            public _data data { set; get; }
            public List<lb> curLbs { set; get; }
            public List<Param>  _params { set; get; }



        }
        class _data
        {

            public string devCode { set; get; }
        }

  


        public class Param
        {
            public string FieldName { get; set; }


            public string FieldValue { get; set; }

        }

        public class lb
        {

            public string lbId { set; get; }
            public string partNum { set; get; }

        }
    }










    class Request_data
    {

        public uint requestTime { set; get; }
        public string sourceId { set; get; }
        public _data data { set; get; }
        public List<lb> curLbs { set; get; }
        public List<Param> _params { set; get; }



    }
    class _data
    {

        public string devCode { set; get; }
    }

    public class Param
    {
        public string FieldName { get; set; }


        public string FieldValue { get; set; }

    }


    public class lb
    {

        public string lbId { set; get; }
        public string partNum { set; get; }

    }
}
