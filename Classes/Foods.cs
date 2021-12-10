using fatsecret.NET.Classes.JSONExt;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fatsecret.NET.Classes
{
    public class FoodsResult
    {
        public Foods foods;
    }
    public class Foods
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Food>))]
        public List<Food> food;
        public int max_results;
        public int total_results;
        public int page_number;
    }
}
