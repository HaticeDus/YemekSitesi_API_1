using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Restaurant_1
    {
        [Range(400, 499, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int restaurantID { get; set; } //PK

        [Column(TypeName = "nvarchar(70)")]
        [MaxLength(70, ErrorMessage = "Max 70 karakter")]
        [DisplayName("Restaurant Adı")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string restaurantAd { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [MaxLength(255, ErrorMessage = "Max 255 karakter")]
        [DisplayName("Restaurant Adresi")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string restaurantAdres { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        [MaxLength(10, ErrorMessage = "Max 10 karakter")]
        [DisplayName("Restaurant Telefon")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string restaurantTel { get; set; }


        [DisplayName("Restaurant Resim")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string restaurantImg { get; set; }

        [JsonIgnore]
        public ICollection<Siparisler_1>? npSiparisler_1 { get; set; } // Çoğul Navigation Property (np) M-1
        [JsonIgnore]
        public ICollection<Restaurant_Menu_1>? npRestaurant_Menu_1 { get; set; } //Çoğul Navigation Property (M-M) M-1
        [JsonIgnore]
        public ICollection<Restaurant_Urun_1>? npRestaurant_Urun_1 { get; set; } //Çoğul Navigation Property (M-M) M-1
    }
}
