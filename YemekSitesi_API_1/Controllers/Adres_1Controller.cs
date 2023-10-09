using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekSitesi_API_1.Models;
//using System.Data.Entity;
//using System.Data.Entity;
//using System.Net;


namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Adres_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Adres_1Controller(YemekSitesiDbContext context)
        {
            _context = context; // Veritabanı bağlantısı ve işlemlerini gerçekleştirmek
                                // için bir Context nesnesi enjekte edilir.
        }


        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Adres_1>>> GetAdresler()
        {
            if (_context.Adressler == null)
            {
                return BadRequest("Kayıtlı Adres bulunamadı");
            }
            return await _context.Adressler.ToListAsync();
        }




        // GET: api/Adres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adres_1>> GetAdres(int id)
        {
            var adres = await _context.Adressler.FindAsync(id);

            if (adres == null)
            {
                return NotFound();
            }

            return adres;
        }




        // POST: api/Adres
        //[HttpPost]
        //public async Task<ActionResult<Adres_1>> CreateAdres(Adres_1 adres)
        //{
        //    _context.Adresler.Add(adres);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetAdres), new { id = adres.adresID }, adres);
        //}



        // POST: api/Adres

        [HttpPost]
        public async Task<ActionResult<Adres_1>> CreateAdres(string isAdres, string evAdres)
        {


            
            var existingEvAdres = await _context.Adressler.FirstOrDefaultAsync(a => a.evAdresi == evAdres); // Ev adresinin benzersiz olup olmadığını kontrol et
            var existingIsAdres = await _context.Adressler.FirstOrDefaultAsync(a => a.isAdresi == isAdres); // İş adresinin benzersiz olup olmadığını kontrol et

            if (existingEvAdres != null && existingIsAdres != null)//aynı ev ve iş adresi iki farklı kullanıcı tarafından tekrar tekrar girilemez.
            {
                return BadRequest("Bu ev ve iş adresi zaten kullanılıyor!");
            }


           else if (string.IsNullOrEmpty(isAdres) || string.IsNullOrEmpty(evAdres))// Boş veya geçersiz adreslerin kontrolünü yap.
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }


            Adres_1 adres = new Adres_1  // Yeni bir Adres_1 nesnesi oluştur ve verileri ata.
            {
                evAdresi = evAdres,
                isAdresi = isAdres
            };


            _context.Adressler.Add(adres); // Oluşturulan adresi veritabanına ekle.
            await _context.SaveChangesAsync();

            // CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndürür.
            return CreatedAtAction(nameof(GetAdres), new { id = adres.adresID }, adres);

            // Oluşturulan kaynağın ayrıntılarını almak için GetAdres metoduyla 
            //birlikte yeni bir Created (201) yanıtı döndürülür. adres nesnesi,
            //oluşturulan kaynağı temsil eder ve kaynağın ayrıntılarına 
            //erişilebilir hale gelir.
        }



        //// PUT: api/Adres/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAdres(int id, Adres_1 adres)
        //{
        //    if (id != adres.adresID)
        //    {
        //        return BadRequest("Geçersiz adres ID!");
        //    }

        //    var existingAdres = await _context.Adresler.FindAsync(id);

        //    if (existingAdres == null)
        //    {
        //        return NotFound();
        //    }


        //    try
        //    {

        //        // Güncellenen alanları geçici bir örneğe atayın
        //        var tempAdres = new Adres_1
        //        {
        //            //adresID = existingAdres.adresID,
        //            evAdresi = adres.evAdresi,
        //            isAdresi = adres.isAdresi,
        //           // npKullanicilar_1 = existingAdres.npKullanicilar_1
        //        };
        //        _context.Entry(tempAdres).State = EntityState.Modified;// adres nesnesinin durumunu değiştirerek güncelleme işlemi yapılmasını belirtiyoruz
        //        await _context.SaveChangesAsync();
        //    }

        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AdresExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdres(int id, Adres_1 updatedAdres)
        {

            if (id != updatedAdres.adresID) // İsteğin adres ID'si, güncellenen adresin adres ID'siyle eşleşmiyor ise hata döndür.
            {
                return BadRequest("Geçersiz adres ID!");
            }


            var existingAdres = await _context.Adressler.FindAsync(id); // Güncellenecek adresi veritabanından bul.

            // Eğer adres bulunamaz ise, NotFound (404) hatası döndür.
            if (existingAdres == null)
            {
                return NotFound();
            }

            try
            {
                // Var olan adresin alanlarını güncelle.
                existingAdres.evAdresi = updatedAdres.evAdresi;
                existingAdres.isAdresi = updatedAdres.isAdresi;


                await _context.SaveChangesAsync();// Değişiklikleri veritabanına kaydet.
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!AdresExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// Adres bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }


            // Güncelleme işlemi başarılı olduğunda, isteğe bağlı olarak farklı durum kodlarını kullanabilirsiniz.
            // Örnek alternatifler:

            // Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
            return Ok(existingAdres);

            // Accepted (202): İstek kabul edildi, ancak işlemin tamamlanması daha sonra gerçekleşecek.
            // return Accepted();

            // Custom status code (örneğin, 201 Created): Güncelleme başarıyla tamamlandı ve yeni bir kaynak oluşturuldu.
            // return StatusCode(201);

            // return NoContent(); // 204 No Content durumu, isteğin başarıyla işlendiğini ve herhangi bir içerik döndürülmediğini belirtir.

        }

        private bool AdresExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Adressler.Any(a => a.adresID == id);
        }


        // DELETE: api/Adres/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdres(int id)
        {

            var adres = await _context.Adressler.FindAsync(id); // Belirtilen adres ID'sine sahip adresi veritabanından bul.
            if (adres == null)
            {

                return NotFound(); // Adres bulunamaz ise, NotFound (404) hatası döndür.
            }


            _context.Adressler.Remove(adres);// Adresi veritabanından kaldır.
            await _context.SaveChangesAsync();
            //SaveChangesAsync metodu aynı zamanda bir asenkron yöntemdir, yani veritabanı işlemlerinin
            //ardışık bir şekilde tamamlanmasını beklemek yerine,
            //işlem asenkron olarak gerçekleştirilir ve işlem tamamlandığında bir sonuç döndürülür.

            //Bu kod satırı, asenkron olarak çalıştığı için await ifadesi kullanılır ve
            //bu sayede diğer işlemlerin devam etmesine izin verilirken
            //değişikliklerin veritabanına kaydedilmesi beklenir.


            return NoContent(); // İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                               // için NoContent (204) state döndür.
        }


    }
}
