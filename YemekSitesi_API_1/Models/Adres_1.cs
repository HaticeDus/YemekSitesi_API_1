using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace YemekSitesi_API_1.Models
{
    public class Adres_1
    {
        [Range(100, 199, ErrorMessage = "Alan yalnızca 3 basamaklı bir değer alabilir.")]
        public int adresID { get; set; } //PK

        [Column(TypeName = "nvarchar(200)")]
        [DisplayName("Ev Adresi")]
        public string evAdresi { get; set; }

        [Column(TypeName = "nvarchar(200)")]
        [DisplayName("İş Adresi")]
        public string isAdresi { get; set; }

        [JsonIgnore]
        public Kullanicilar_1 ? npKullanicilar_1 { get; set; }  // Navigation Property (np) 1-1

        //npKullanicilar_1 alanını (?) nullable olarak tanımlamak, bir adresin bir kullanıcıya bağlı
        //olabileceği durumları ifade etmek için kullanılabilir. Örneğin, bazı adreslerin bir
        //kullanıcıya ait olması zorunlu olmayabilir ve npKullanicilar_1 alanı null olarak belirtilebilir.
        //Böylece, bir adresin ilişkili kullanıcısı olmadığı durumları temsil edebilirsiniz.
    }
}
