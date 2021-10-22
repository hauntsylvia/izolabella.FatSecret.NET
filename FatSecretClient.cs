using fatsecret.NET.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace fatsecret.NET
{
    public class FatSecretClient
    {
        internal HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5),
        };
        internal string[] urls = new string[] { "https://platform.fatsecret.com/rest/server.api" };
        internal Uri ResolveCorrectUrl()
        {
            foreach (string url in urls)
            {
                try
                {
                    Uri thisUrl = new Uri(url);
                    PingReply pr = (new Ping()).Send(thisUrl.Host, 3000);
                    if (pr.Status == IPStatus.Success)
                        return thisUrl;
                    else
                        throw new Exception();
                }
                catch { }
            }
            return null;
        }
        internal async Task<AccessTokenResult> ProvidedCodeForAccessToken()
        {
            var byteArray = Encoding.ASCII.GetBytes($"{this.client_id}:{this.client_secret}");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            var values = new Dictionary<string, string>
            {
               { "scope", this.scope },
               { "grant_type", this.grant_type }
            };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("https://oauth.fatsecret.com/connect/token", content);

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AccessTokenResult>(responseString);
        }
        public FatSecretClient(string client_secret, string client_id, string scope = "basic")
        {
            this.client_secret = client_secret;
            this.client_id = client_id;
            this.scope = scope;
            client.BaseAddress = ResolveCorrectUrl();
            this.accessToken = (ProvidedCodeForAccessToken().GetAwaiter().GetResult()).access_token;
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
            req.Headers.Add("Authorization", $"Bearer {accessToken}");
            req.Content = new FormUrlEncodedContent(args);
            HttpResponseMessage msg = await client.SendAsync(req);
            HttpContent content = (msg).Content;
            string finContent = await content.ReadAsStringAsync();
            T result = JsonConvert.DeserializeObject<T>(finContent);
            return result;
        }
        public async Task<FoodsResult> FoodSearch(string expression, int page = 0)
        {
            return await SendAsync<FoodsResult>("foods.search", new Dictionary<string, string>()
            {
                {"search_expression", expression},
                {"page_number", page.ToString()},
            });
        }
        public async Task<FoodsGetV2> FoodInfo(long food_id)
        {
            return await SendAsync<FoodsGetV2>("food.get.v2", new Dictionary<string, string>()
            {
                {"food_id", food_id.ToString()},
            });
        }
    }
}
