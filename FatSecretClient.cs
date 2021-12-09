using fatsecret.NET.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.NET
{
    public class FatSecretClient
    {
        internal HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(15),
        };
        internal Uri url = new Uri("https://platform.fatsecret.com/rest/server.api");
        internal async Task<AccessTokenResult> ProvidedCodeForAccessToken()
        {
            byte[] byteArray = Encoding.ASCII.GetBytes($"{this.client_id}:{this.client_secret}");
            this.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            Dictionary<string, string> values = new Dictionary<string, string>
            {
               { "scope", this.scope },
               { "grant_type", this.grant_type }
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await this.client.PostAsync("https://oauth.fatsecret.com/connect/token", content);

            string responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AccessTokenResult>(responseString);
        }
        public FatSecretClient(string client_secret, string client_id, string scope = "basic")
        {
            this.client_secret = client_secret;
            this.client_id = client_id;
            this.scope = scope;
            this.client.BaseAddress = this.url;
            this.accessToken = (this.ProvidedCodeForAccessToken().GetAwaiter().GetResult()).access_token;
        }
        internal string grant_type = "client_credentials";
        internal string client_secret = string.Empty;
        internal string client_id = string.Empty;
        internal string scope = string.Empty;
        internal string accessToken = string.Empty;
        internal async Task<T> SendAsync<T>(string method, Dictionary<string, string> args)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "");
            args.Add("format", "json");
            args.Add("method", method);
            req.Headers.Add("Authorization", $"Bearer {this.accessToken}");
            req.Content = new FormUrlEncodedContent(args);
            HttpResponseMessage msg = await this.client.SendAsync(req);
            HttpContent content = (msg).Content;
            string finContent = await content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(finContent);
            return result;
        }
        public async Task<FoodsResult> FoodSearch(string expression, int page = 0, int maxResults = 5)
        {
            return await this.SendAsync<FoodsResult>("foods.search", new Dictionary<string, string>()
            {
                {"search_expression", expression},
                {"page_number", page.ToString()},
                {"max_results", maxResults.ToString()}
            });
        }
        public async Task<FoodsGetV2> FoodInfo(long food_id)
        {
            return await this.SendAsync<FoodsGetV2>("food.get.v2", new Dictionary<string, string>()
            {
                {"food_id", food_id.ToString()},
            });
        }
    }
}
