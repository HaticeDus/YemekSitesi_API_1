using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Urunler_1
    {

        [Range(900, 999, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int urunID { get; set; } // PK 

        [Range(500, 599, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int kategoriID { get; set; } //FK

        [Column(TypeName = "nvarchar(50)")]
        [MaxLength(50, ErrorMessage = "Max 50 karakter")]
        [DisplayName("Ürünün Adı")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string urunAdi { get; set; }

        [Column(TypeName = "int")]
        [DisplayName("Ürünün Fiyatı")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public int urunFiyat { get; set; }

        [DisplayName("Ürün Resim")]
        [DefaultValue(" ")]
        //[Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string? urunImg { get; set; } = "";

        [JsonIgnore]
        public ICollection<Menuler_1>? npMenuler_1 { get; set; } //Çoğul Navigation Property (M-M) M-1
        [JsonIgnore]
        public ICollection<Siparisler_1>? npsiparisler_1 { get; set; } // Tekil NP 1-M 
        [JsonIgnore]
        public Kategoriler_1? npKategoriler_1 { get; set; } // Tekil Navigation Property (np) 1-M
        [JsonIgnore]
        public ICollection<Restaurant_Urun_1>? npRestaurant_Urun_1 { get; set; } //Çoğul Navigation Property (M-M) M-1
    }
}
