using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;
using YemekSitesi_API_1.Models;

namespace YemekSitesi_API_1.Models
{
    public class Kategoriler_1
    {
        [Range(500, 599, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int kategoriID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "Max 50 karakter")]
        [DisplayName("Restaurant Kategori Türü")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string kategoriTuru { get; set; }

        [DisplayName("Restaurant Resim")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string kategoriImg { get; set; }

        [JsonIgnore]
        public ICollection<Restaurant_1>? npRestaurant_1 { get; set; } // Çoğul Navigation Property (np) M-1

        [JsonIgnore]
        public ICollection<Urunler_1>? npUrunler_1 { get; set; } // Çoğul Navigation Property (np) M-1
    }
}
