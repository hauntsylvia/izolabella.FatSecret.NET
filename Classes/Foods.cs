using izolabella.FatSecret.NET.Classes.JSONExt;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace izolabella.FatSecret.NET.Classes
{
    /// <summary>
    /// The result of a food search.
    /// </summary>
    public class FoodsResult
    {
        /// <summary>
        /// The foods returned.
        /// </summary>
        [JsonProperty("foods")]
        public Foods Foods { get; set; }
    }
    /// <summary>
    /// A class containing information relevant to a search for foods via string expression.
    /// </summary>
    public class Foods
    {
        /// <summary>
        /// A list of foods returned from the search.
        /// </summary>
        [JsonConverter(typeof(SingleOrArrayConverter<Food>))]
        public List<Food> Food { get; set; }

        /// <summary>
        /// The maximum results.
        /// </summary>
        [JsonProperty("max_results")]
        public int MaxResults { get; set; }

        /// <summary>
        /// Total results yielded.
        /// </summary>
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }

        /// <summary>
        /// The page number for these results.
        /// </summary>
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }
    }
}
