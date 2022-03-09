using Newtonsoft.Json;

namespace izolabella.FatSecret.NET.Classes
{
    /// <summary>
    /// A class representing an API call.
    /// </summary>
    [JsonObject("FoodsGetV2")]
    public class FoodInformationResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FoodInformationResult"/> class with a provided <see cref="Food"/> object.
        /// </summary>
        /// <param name="Food"></param>
        public FoodInformationResult(Food Food)
        {
            this.Food = Food;
        }

        /// <summary>
        /// The returned <see cref="Food"/> object.
        /// </summary>
        public Food Food { get; }
    }
}
