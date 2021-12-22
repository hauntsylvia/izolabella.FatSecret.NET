using fatsecret.NET.Classes.JSONExt;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fatsecret.NET.Classes
{
    public class FoodsResult
    {
        [JsonProperty("foods")]
        public Foods foods { get; set; }
    }
    public class Foods
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Food>))]
        public List<Food> food { get; set; }
        [JsonProperty("max_results")]
        public int maxResults { get; set; }
        [JsonProperty("total_results")]
        public int totalResults { get; set; }
        [JsonProperty("page_number")]
        public int pageNumber { get; set; }
    }
}
