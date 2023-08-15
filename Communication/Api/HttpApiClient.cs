using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace Communication.Api
{
    public class HttpApiClient
    {
        public void PostLogin(string path)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(path);

                var newPost = new Login()
                {
                    user = "dani",
                    password = "123"
                };

                var newPostJson = JsonConvert.SerializeObject(newPost);

                var payload = new StringContent(newPostJson, System.Text.Encoding.UTF8, "application/json");

                var result = client.PostAsync(endpoint, payload).Result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(result);

            }
        }

        public void Get()
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://administradorsemaforos.herokuapp.com/");

                var result = client.GetAsync(endpoint).Result;

                var data = result.Content.ReadAsStringAsync().Result;

                Console.WriteLine(data);

            }
        }
    }


}
