using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Restaurant_Menu_1 //COMPOSİTE KEY OLABİLİR
    {
        [Range(600, 699, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int restMenuID { get; set; } //PK 

        [Range(400, 499, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int restaurantID { get; set; } //FK 

        [Range(700, 799, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int menuID { get; set; } //FK 

        [JsonIgnore]
        public Restaurant_1? npRestaurant_1 { get; set; } //Tekil Navigation Property  M-M
        [JsonIgnore]
        public Menu_1? npMenu_1 { get; set; } //Tekil Navigation Property M-M
    }
}
