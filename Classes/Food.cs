using fatsecret.NET.Classes.Enums;
using Newtonsoft.Json;
using System;

namespace fatsecret.NET.Classes
{
    public class Food
    {
        [JsonProperty("food_type")]
        internal string _foodType;
        [JsonIgnore]
        public FoodType foodType
        {
            get
            {
                if (Enum.TryParse(_foodType, out FoodType f))
                    return f;
                else
                    throw new FormatException();
            }
            set
            {
                _foodType = value.ToString();
            }
        }

        [JsonProperty("food_description")]
        public string description;
        [JsonProperty("food_id")]
        public long id;
        [JsonProperty("food_name")]
        public string name;
        [JsonProperty("brand_name")]
        public string brandName;
        [JsonProperty("food_url")]
        public string url;
        public Servings servings;
    }
}
