using fatsecret.NET.Classes.Enums;
using Newtonsoft.Json;
using System;

namespace fatsecret.NET.Classes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Food
    {
        [JsonConstructor]
        public Food(string _foodType, string description, ulong id, string name, string brandName, string url, Servings servings)
        {
            this._foodType = _foodType;
            this.description = description;
            this.id = id;
            this.name = name;
            this.brandName = brandName;
            this.url = url;
            this.servings = servings;
        }
        [JsonProperty("food_type")]
        private string _foodType;
        public FoodType foodType
        {
            get => (FoodType)Enum.Parse(typeof(FoodType), this._foodType);
            set => this._foodType = value.ToString();
        }

        [JsonProperty("food_description")]
        public string description { get; set; }
        [JsonProperty("food_id")]
        public ulong id { get; set; }
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
