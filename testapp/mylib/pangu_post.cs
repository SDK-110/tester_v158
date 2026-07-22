using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace testapp.mylib.pangu
{
    class pangu_post
    {


        public static uint get_time_strip()
        {






            DateTimeOffset currentTime = DateTimeOffset.Now;
            uint timestamp = (uint)currentTime.ToUnixTimeMilliseconds();
            return timestamp;


        }

        static void post_gangu(string[] args)
        {
            uint ts = get_time_strip();
            string url_str = "http://127.0.0.1:8888";
            Request_data updata = new Request_data
            {
                requestTime = ts,
                sourceId = "FCT",
                data = new _data { devCode = "FCT-Desay-8962-001" },
                curLbs = new List<lb>() { new lb { lbId = "sn_123456789", partNum = "xxxxxxxxxxxx" } },
                _params = new List<Param>() { new Param { FieldName = "fsdf", FieldValue = "444444" } }

            };

            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(updata).Replace("_params", "params");
            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PostAsync(url_str, content).Result;
                string pattern = "\"resultCode\":\"(\\d{4})\"";
                if (response.IsSuccessStatusCode)
                {
                    string rst = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(rst);
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(rst);

                    if (match.Success)
                    {
                        string resultCode = match.Groups[1].Value;
                        Console.WriteLine("Extracted result code: " + resultCode);
                    }
                    else
                    {
                        Console.WriteLine("No match found");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to send data. Status code: " + response.StatusCode);
                }
            }

            Console.WriteLine(jsonData);
            Console.ReadKey();
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
