using fatsecret.NET.Classes.Enums;
using Newtonsoft.Json;
using System;

namespace fatsecret.NET.Classes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Food
    {
        [JsonProperty("food_type")]
        internal string _foodType;
        public FoodType foodType
        {
            get => (FoodType)Enum.Parse(typeof(FoodType), this._foodType);
            set => this._foodType = value.ToString();
        }

        [JsonProperty("food_description")]
        public string description { get; set; }
        [JsonProperty("food_id")]
        public long id { get; set; }
        [JsonProperty("food_name")]
        public string name { get; set; }
        [JsonProperty("brand_name")]
        public string brandName { get; set; }
        [JsonProperty("food_url")]
        public string url { get; set; }
        [JsonProperty("servings")]
        public Servings servings { get; set; }
    }
}
