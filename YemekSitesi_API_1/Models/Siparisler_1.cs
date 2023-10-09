using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Siparisler_1  //COMPOSİTE KEY OLABİLİR
    {
        [Range(300, 399, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int siparisID { get; set; } //PK

        [Range(200, 299, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int userID { get; set; }  //FK

        [Range(900, 999, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int urunID { get; set; } // FK

        [Range(800, 899, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int menulerID { get; set; } // FK

        [Range(400, 499, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int restaurantID { get; set; } //FK



        [DisplayName("Sipariş Tarihi")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public DateTime siparisTarih { get; set; } = DateTime.Now;

        [DisplayName("Sipariş Adeti")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        [Range(1, 99, ErrorMessage = "Alan yalnızca max 2 basamaklı bir değer alabilir.")]
        public int siparisAdet { get; set; }

        [DisplayName("Sipariş Tutarı")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public int siparisTutar { get; set; }

        [JsonIgnore]
        public Kullanicilar_1? npKullanicilar_1 { get; set; } // Tekil Navigation Property (np) 1-M
        [JsonIgnore]
        public Restaurant_1? npRestaurant_1 { get; set; } // Tekil Navigation Property (np) 1-M
        [JsonIgnore]
        public Urunler_1 ?npUrunler_1 { get; set; } //Çoğul Navigation Property (np) M-1
        [JsonIgnore]
        public Menuler_1? npMenuler_1 { get; set; } //Çoğul Navigation Property (np) M-1
    }
}
