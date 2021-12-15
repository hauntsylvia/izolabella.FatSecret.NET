using fatsecret.NET.Classes.JSONExt;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace fatsecret.NET.Classes
{
    public class Servings
    {
        [JsonConverter(typeof(SingleOrArrayConverter<Serving>))]
        public List<Serving> serving;
    }
    public class Serving
    {
        /*
            "added_sugars":"10",
            "calcium":"8",
            "calories":"159",
            "carbohydrate":"20.02",
            "cholesterol":"90",
            "fat":"6.13",
            "fiber":"0.8",
            "iron":"9",
            "measurement_description":"regular slice",
            "metric_serving_amount":"65.000",
            "metric_serving_unit":"g",
            "monounsaturated_fat":"2.298",
            "number_of_units":"1.000",
            "polyunsaturated_fat":"1.578",
            "potassium":"80",
            "protein":"5.58",
            "saturated_fat":"1.585",
            "serving_description":"regular slice",
            "serving_id":"16758",
            "serving_url":"http:\/\/www.fatsecret.com\/calories-nutrition\/generic\/french-toast-plain?portionid=16758&portionamount=1.000",
            "sodium":"320",
            "sugar":"4.87",
            "trans_fat":"0",
            "vitamin_a":"0",
            "vitamin_c":"0",
            "vitamin_d":"2"
        */
        public decimal added_sugars;
        public decimal calcium;
        public decimal calories;
        public decimal carbohydrate;
        public decimal cholesterol;
        public decimal fat;
        public decimal fiber;
        public decimal iron;
        public string measurement_description;
        public decimal metric_serving_amount;
        public string metric_serving_unit;
        public decimal monounsaturated_fat;
        public decimal number_of_units;
        public decimal polyunsaturated_fat;
        public decimal potassium;
        public decimal protein;
        public decimal saturated_fat;
        public string serving_description;
        public long serving_id;
        public string serving_url;
        public decimal sodium;
        public decimal sugar;
        public decimal trans_fat;
        public decimal vitamin_a;
        public decimal vitamin_c;
        public decimal vitamin_d;
    }
}
