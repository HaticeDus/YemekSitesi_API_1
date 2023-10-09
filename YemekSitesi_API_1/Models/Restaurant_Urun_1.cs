using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Restaurant_Urun_1
    {
        [Range(1000, 1099, ErrorMessage = "Alan yalnızca 1000-1099 basamaklı bir değer alabilir.")]
        public int restUrunID { get; set; } //PK

        [Range(400, 499, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int restaurantID { get; set; } //FK

        [Range(900, 999, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int urunID { get; set; } // FK

        [JsonIgnore]
        public Restaurant_1? npRestaurant_1 { get; set; } //Tekil Navigation Property  M-M
        [JsonIgnore]
        public Urunler_1? npUrunler_1 { get; set; } //Tekil Navigation Property M-M
    }
}
