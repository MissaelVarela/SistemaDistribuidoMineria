using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Communication.ApiPruebas
{
    public class Api
    {
        public async Task<string> GetAsync(string path)
        {
            RestClient client = new RestClient(path);
            RestRequest request = new RestRequest();
            RestResponse response = await client.ExecuteAsync(request);

            string content = response.get_Content();

            Console.WriteLine("Llegó: " + response.get_StatusCode().ToString());

            return content;
        }

        public async Task PostItemAsync(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest("items", Method.Post);

            string data = "";

            request.AddParameter("data", data);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.get_Content());
        }

        public async Task<string> Post(string path)
        {
            var client = new RestClient(path);
            var request = new RestRequest();


             string str = "{\"user\":\"Daniel\", \"password\":\"123\"}";
             JObject json2 = JObject.Parse(str);

            string output = JsonConvert.SerializeObject(new Communication.Api.Login() { password = "10", user = "Missa" });
            JObject json = JObject.Parse(output);

            request.AddJsonBody(json2, "application/json");

            request.Method = Method.Post;
            request.AddHeader("Accept", "application/json");

            //request.Parameters;
            //request.AddParameter("application/json", json2, ParameterType.RequestBody);

            RestResponse response = await client.ExecuteAsync(request);
            var content = response.get_Content();

            Console.WriteLine(response.get_StatusCode());

            return content;
        
        

        /*var request = new RestRequest(Method.Post);

        request.Resource = "Api/Score";
        request.RequestFormat = DataFormat.Json;

        request.AddBody(request.JsonSerializer.Serialize(new { A = "foo", B = "bar" }));

        RestResponse response = client.Execute(request);
        Console.WriteLine(response.Content);*/



        /*var client = new RestClient(path);

        //var request = new RestRequest("/resource/", Method.Post);
        var request = new RestRequest();
        request.Method = Method.Post;
        request.Resource = "";

        // Json to post.
        string jsonToSend = json;

        request.AddParameter("application/json; charset=utf-8", jsonToSend, ParameterType.RequestBody);
        request.RequestFormat = DataFormat.Json;

        try
        {
            client.ExecuteAsync(request);
        }
        catch (Exception)
        {
            // Log
        }*/
    }




    /*public dynamic Post(string url, string json, string autorizacion = null)
    {
        try
        {
            var client = new RestClient(url);
            var request = new RestRequest();
            request.Method = Method.Post;

            request.AddHeader("content-type", "applicaction/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            if (autorizacion != null)
            {
                request.AddHeader("Authorization", autorizacion);
            }

            client.execute

        }
    }*/
}
}
