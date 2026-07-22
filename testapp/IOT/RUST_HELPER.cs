using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
namespace testapp.IOT
{
    class RUST_HELPER
    {
        RestClient rust_client = null;
        RestRequest request = null;
        RestResponse resp = null;
        public RUST_HELPER(string webbase = "http://192.168.89.47:60000")
        {
            rust_client = new RestSharp.RestClient(webbase);
            request = new RestRequest();
            request.Method = Method.Post;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Accept", "application/json");
        }

        public void test() {


            string data = Newtonsoft.Json.JsonConvert.SerializeObject(new { name = "123", age = 33 });
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            var p = rust_client.Execute(request);
        }

        ~RUST_HELPER(){




        }
    }
}
