using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Sepet_1
    {
        public int sepetID { get; set; } //PK

        [Range(200, 299, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int userID { get; set; } //FK

        [DisplayName("Product name ")]
        [Column(TypeName = "nvarchar(150)")]
        public string productAd { get; set; }

        [DisplayName("Product adet ")]
        [Range(1, 50, ErrorMessage = "tek seferde Max 50 urun secilebilir.")]
        public int productAdet { get; set; }

        [DisplayName("Product Img ")]
        public string productImg { get; set; }

        [DisplayName("Product Fiyat ")]
        public int productFiyat { get; set; }


        [DisplayName("Product Date ")]
        public DateTime productDate { get; set; } = DateTime.Now; //yıl/ay/gün/saat/dk/sn

        [JsonIgnore]
        public Kullanicilar_1? npKullanicilar_1 { get; set; } // bir sepetin 1 kullanıcısı olabilir




    }
}
