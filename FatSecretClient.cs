using izolabella.FatSecret.NET.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace izolabella.FatSecret.NET
{
    /// <summary>
    /// Provides a client for interacting with the FatSecret API.
    /// </summary>
    public class FatSecretClient
    {
        private readonly HttpClient Client = new()
        {
            Timeout = TimeSpan.FromSeconds(15),
        };
        private readonly Uri Address = new("https://platform.fatsecret.com/rest/server.api");
        internal async Task<AccessTokenResult> ProvidedCodeForAccessToken()
        {
            byte[] ByteArray = Encoding.ASCII.GetBytes($"{this.ClientId}:{this.ClientSecret}");
            this.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(ByteArray));
            Dictionary<string, string> Values = new()
            {
               { "scope", this.Scope },
               { "grant_type", this.GrantType }
            };
            FormUrlEncodedContent Content = new(Values);
            HttpResponseMessage Response = await this.Client.PostAsync("https://oauth.fatsecret.com/connect/token", Content);

            string ResponseString = await Response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<AccessTokenResult>(ResponseString);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FatSecretClient"/> class.
        /// </summary>
        /// <param name="client_secret">The client secret./</param>
        /// <param name="client_id">The client id.</param>
        /// <param name="scope">The application's scope.</param>
        public FatSecretClient(string client_secret, string client_id, string scope = "basic")
        {
            this.ClientSecret = client_secret;
            this.ClientId = client_id;
            this.Scope = scope;
            this.Client.BaseAddress = this.Address;
            this.AccessToken = this.ProvidedCodeForAccessToken().GetAwaiter().GetResult().AccessToken;
        }
        internal string GrantType = "client_credentials";
        internal string ClientSecret = string.Empty;
        internal string ClientId = string.Empty;
        internal string Scope = string.Empty;
        internal string AccessToken = string.Empty;
        internal async Task<T> SendAsync<T>(string Method, Dictionary<string, string> Args, int AttemptNumber = 0)
        {
            try
            {
                HttpRequestMessage Req = new(HttpMethod.Post, "");
                Args.Add("format", "json");
                Args.Add("method", Method);
                Req.Headers.Add("Authorization", $"Bearer {this.AccessToken}");
                Req.Content = new FormUrlEncodedContent(Args);
                HttpResponseMessage Msg = await this.Client.SendAsync(Req);
                HttpContent Content = Msg.Content;
                string StringContent = await Content.ReadAsStringAsync();
                T ParsedContent = JsonConvert.DeserializeObject<T>(StringContent);
                return ParsedContent;
            }
            catch (Exception)
            {
                if (AttemptNumber <= 2)
                {
                    await Task.Delay(TimeSpan.FromSeconds(2));
                    this.AccessToken = (await this.ProvidedCodeForAccessToken()).AccessToken;
                    return await this.SendAsync<T>(Method, Args, AttemptNumber++);
                }
                throw;
            }
        }
        /// <summary>
        /// Search the FatSecret API for foods based on a given expression.
        /// </summary>
        /// <param name="Expression">The expression to search for.</param>
        /// <param name="Page">The page number.</param>
        /// <param name="MaxResults">Maximum number of results that can be returned per page.</param>
        /// <returns>A <see cref="FoodsResult"/> object for representing searched-for foods.</returns>
        public async Task<FoodsResult> FoodSearch(string Expression, int Page = 0, int MaxResults = 5)
        {
            return await this.SendAsync<FoodsResult>("foods.search", new Dictionary<string, string>()
            {
                {"search_expression", Expression},
                {"page_number", Page.ToString()},
                {"max_results", MaxResults.ToString()}
            });
        }
        /// <summary>
        /// Return specific information from a food's id.
        /// </summary>
        /// <param name="FoodId">The id of the food to return information for.</param>
        /// <returns></returns>
        public async Task<FoodInformationResult> FoodInfo(ulong FoodId)
        {
            return await this.SendAsync<FoodInformationResult>("food.get.v2", new Dictionary<string, string>()
            {
                {"food_id", FoodId.ToString()},
            });
        }
    }
}
