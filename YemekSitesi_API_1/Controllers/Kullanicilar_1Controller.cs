using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
//using System.Data.Entity;
using YemekSitesi_API_1.Models;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Kullanicilar_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Kullanicilar_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Kullanicilar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanicilar_1>>> GetKullanicilar()
        {
            if (_context.Kullanıcılar == null)
            {
                return BadRequest("Kayıtlı kullanıcı bulunamadı");
            }
            return await _context.Kullanıcılar.ToListAsync();
        }

        // GET: api/Kullanicilar/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanicilar_1>> GetKullanici(int id)
        {
            var kullanici = await _context.Kullanıcılar.FindAsync(id);

            if (kullanici == null)
            {
                return BadRequest("Girilen ID bulunamadı!");
            }

            return Ok(kullanici);
            // return kullanici; // ya da
        }


        //POST: api/Kullanici

        [HttpPost]
        public async Task<ActionResult<Kullanicilar_1>> CreateKullanici(string Ad, string Soyad, string Email, string Password, string Tel,string Role,bool IsAcvtive, int adresID)
        {
            if (string.IsNullOrEmpty(Ad) || string.IsNullOrEmpty(Soyad) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Tel))
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }

            // Aynı isim, soyisim ve e-postaya sahip bir kullanıcının olup olmadığını kontrol et
            var existingKullanici = await _context.Kullanıcılar.FirstOrDefaultAsync(k => k.ad == Ad && k.soyad == Soyad && k.email == Email && k.password == Password);
            if (existingKullanici != null)
            {
                return BadRequest("Bu kullanıcı zaten kayıtlı!");
            }


            Kullanicilar_1 kullanici = new Kullanicilar_1
            {
                ad = Ad,
                soyad = Soyad,
                email = Email,
                password = Password,
                tel = Tel,
                role=Role,
                isactive= IsAcvtive,
                adresId = adresID

            };

            _context.Kullanıcılar/*dbcontext Kullanıcılar tablosu*/.Add(kullanici);
            await _context.SaveChangesAsync(); // asekron olarak tabloya kaydet

            // CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndür.
            return CreatedAtAction(nameof(GetKullanici), new { id = kullanici.userID }, kullanici);
            // Oluşturulan kaynağın ayrıntılarını almak için GetAdres metoduyla 
            //birlikte yeni bir Created (201) yanıtı döndürülür. adres nesnesi,
            //oluşturulan kaynağı temsil eder ve kaynağın ayrıntılarına 
            //erişilebilir hale gelir.
        }


        //PUT: api/Kullanıcı/id

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKullanıcı(int id, Kullanicilar_1 userUpdated) //userUpdated güncellenecek kullanıcı nesnesi
        {
            if (id != userUpdated.userID)
            {
                return BadRequest("Geçersiz kullanıcı ID");
            }

            var existingUser = await _context.Kullanıcılar.FindAsync(id); //kullanıcı tablosundan id i asekron olarak ara


            if (existingUser == null)//eğer id kullanıcı tablosunda bulunmazsa 
            {
                return NotFound(); //404 notFound döndür
            }

            // Aynı isim, soyisim ve e-postaya sahip başka bir kullanıcının olup olmadığını kontrol et
            var otherUserWithSameInfo = await _context.Kullanıcılar.FirstOrDefaultAsync(k => k.ad == userUpdated.ad && k.soyad == userUpdated.soyad && k.email == userUpdated.email &&k.password==userUpdated.password && k.userID != id);
            if (otherUserWithSameInfo != null)
            {
                return BadRequest("Bu kullanıcı adı, soyadı ve e-postası zaten kullanılıyor!");
            }

            try
            {
                //var olan kullanıcının alanlarını güncelle
                existingUser.ad = userUpdated.ad;
                existingUser.soyad = userUpdated.soyad;
                existingUser.email = userUpdated.email;
                existingUser.password = userUpdated.password;
                existingUser.tel = userUpdated.tel;
                existingUser.role = userUpdated.role;
                existingUser.isactive = userUpdated.isactive;
                //existingUser.adresId=userUpdated.adresId;

                await _context.SaveChangesAsync(); //değişiklikleri veritabanına asekron kaydet
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id)) //id UserExists fonksiyonuna gönderildi eğer id kullanıcılar tablosunda yok ise 0 döner var ise 1 döner
                {
                    return NotFound();  //404
                }

                else
                {
                    throw; //başka bir hata durumunda hatayı fırlat
                }
            }

            return Ok(existingUser); //(status 200): var olan güncellenmiş kullanıcı bilgilerini verir


        }


        private bool UserExists(int id) //verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et
        {
            return _context.Kullanıcılar.Any(k => k.userID == id);
        }


        //DELETE: api/Kullanıcı/id

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Kullanıcılar.FindAsync(id);
            //asekron olarak kullanıcı tablosundan bu id yi bul

            if (user == null)
            {
                return NotFound();// kullanıcı bulunmazsa 404 döndür 
            }

            _context.Kullanıcılar.Remove(user);// id si buluna user'ı kaldır
            await _context.SaveChangesAsync(); //asekron olarak değişiklikleri kaydet

            return NoContent();//isteğin başarıyla işlendiğini belirtmek için NoContent (204 status kodu döndür)
        }

    }


}
