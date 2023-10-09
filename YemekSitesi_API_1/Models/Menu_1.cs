using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Menu_1
    {
        [Range(700, 799, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int menuID { get; set; } //PK

        [Column(TypeName = "nvarchar(70)")]
        [MaxLength(70, ErrorMessage = "Max 70 karakter")]
        [DisplayName("Menü Adı")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string menuAdi { get; set; }

       

        [DisplayName("Menü Resim")]
        [Required(ErrorMessage = "Bu alanın doldurulması zorunludur.")]
        public string menuImg { get; set; }


        [JsonIgnore]
        public ICollection<Restaurant_Menu_1>? npRestaurant_Menu_1 { get; set; } //Çoğul Navigation Property (M-M) M-1

        [JsonIgnore]
        public ICollection<Menuler_1>? npMenuler_1 { get; set; } //Çoğul Navigation Property (M-M) M-1
    }
}
