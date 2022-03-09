using izolabella.FatSecret.NET.Classes.Enums;
using Newtonsoft.Json;
using System;

namespace izolabella.FatSecret.NET.Classes
{
    /// <summary>
    /// Represents a <see cref="Food"/> object with information returned from the API.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Food
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Food"/> class.
        /// </summary>
        /// <param name="FoodType"></param>
        /// <param name="FoodDescription"></param>
        /// <param name="FoodId"></param>
        /// <param name="FoodName"></param>
        /// <param name="BrandName"></param>
        /// <param name="FoodUrl"></param>
        /// <param name="Servings"></param>
        [JsonConstructor]
        public Food(string FoodType, string FoodDescription, ulong FoodId, string FoodName, string BrandName, string FoodUrl, ServingsW Servings)
        {
            this.foodType = FoodType;
            this.FoodDescription = FoodDescription;
            this.FoodId = FoodId;
            this.FoodName = FoodName;
            this.BrandName = BrandName;
            this.FoodUrl = FoodUrl;
            this.Servings = Servings;
        }

        [JsonProperty("food_type")]
        private string foodType;
        /// <summary>
        /// Whether this food is generic or branded food.
        /// </summary>
        public FoodType FoodType
        {
            get => (FoodType)Enum.Parse(typeof(FoodType), this.foodType);
            set => this.foodType = value.ToString();
        }

        /// <summary>
        /// A description summarizing this food.
        /// </summary>
        [JsonProperty("food_description")]
        public string FoodDescription { get; set; }

        /// <summary>
        /// The id of this food resource.
        /// </summary>
        [JsonProperty("food_id")]
        public ulong FoodId { get; set; }

        /// <summary>
        /// The display name of this food.
        /// </summary>
        [JsonProperty("food_name")]
        public string FoodName { get; set; }

        /// <summary>
        /// The name of the brand who produced this food.
        /// </summary>
        [JsonProperty("brand_name")]
        public string BrandName { get; set; }

        /// <summary>
        /// The url for this food on the FatSecret website.
        /// </summary>
        [JsonProperty("food_url")]
        public string FoodUrl { get; set; }

        /// <summary>
        /// Servings for this food.
        /// </summary>
        [JsonProperty("servings")]
        public ServingsW Servings { get; set; }
    }
}
