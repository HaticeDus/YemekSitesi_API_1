using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Kullanicilar_1
    {
        [Range(200, 299, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int userID { get; set; }

        [DisplayName("Kullanıcı Ad ")]
        [Column(TypeName = "nvarchar(25)")]
        public string ad { get; set; }

        [DisplayName("Kullanıcı Soyad ")]
        [Column(TypeName = "nvarchar(25)")]
        public string soyad { get; set; }

      
        [DisplayName("Kullanıcı E-mail adres ")]
        [Column(TypeName = "nvarchar(50)")]
        public string email { get; set; }

        [DisplayName("Password")]
        [Column(TypeName = "nvarchar(8)")]
        public string password { get; set; }

        [DisplayName("Kullanıcı Telefon ")]
        [Column(TypeName = "nvarchar(10)")]
        public string tel { get; set; }

        [DisplayName("Rol")]
        [Column(TypeName = "nvarchar(10)")]
        public string role { get; set; }

        [DisplayName("isactive")]
        public Boolean isactive { get; set; }


        [DisplayName("Kullanıcı Adres ID ")]
        [Range(100, 199, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int adresId { get; set; } //FK


        [JsonIgnore]
        public Adres_1? npAdres_1 { get; set; } // Navigation Property (np) 1-1 +
        [JsonIgnore]
        public ICollection<Siparisler_1>? npSiparisler_1 { get; set; } // Çoğul Navigation Property (np) 1-M
    }
}
