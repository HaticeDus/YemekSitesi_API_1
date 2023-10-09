using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Menuler_1  //COMPOSİTE KEY OLABİLİR
    {

        [Range(800, 899, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int menulerID { get; set; } // PK 


        [Range(700, 799, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int menuID { get; set; } // FK 

        [Range(900, 999, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int urunID { get; set; } // FK 


        [JsonIgnore]
        public Menu_1? npMenu_1 { get; set; } // Tekil Navigation Property (M-M) M-1
        [JsonIgnore]
        public Urunler_1? npUrunler_1 { get; set; }  // Tekil Navigation Property (M-M) M-1
        [JsonIgnore]
        public ICollection<Siparisler_1>? npsiparisler_1 { get; set; } // Tekil NP 1-M 
    }
}
