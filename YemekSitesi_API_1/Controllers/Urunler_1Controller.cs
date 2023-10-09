using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Urunler_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Urunler_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Urunler_1>>> GetUrunler()
        {
            if (_context.Urunler == null)
            {
                return BadRequest("Kayıtlı Menu bulunamadı");
            }
            return await _context.Urunler.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Urunler_1>> GetUrunById(int id)
        {
            var urun = await _context.Urunler.FindAsync(id);

            if (urun == null)
            {
                return BadRequest("Girilen İd'ye sahip bir ürün yok");
            }

            // return urun;
            return Ok(urun);//  Ok (200): İstek başarılı bulunan urunu döner
        }

        //POST: api/Urunler
        [HttpPost]

        public async Task<ActionResult<Urunler_1>> CreateUrun(string UrunAd, int UrunFiyatı/*Urunler_1 urun*/, string UrunResim, int kategoriID)
        {
            if (string.IsNullOrEmpty(UrunAd) || UrunFiyatı <= 0)
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }

            // Aynı ürün adı ve fiyatına sahip bir ürünün olup olmadığını kontrol et
            var existingUrun = await _context.Urunler.FirstOrDefaultAsync(u => u.urunAdi == UrunAd && u.urunFiyat == UrunFiyatı);
            if (existingUrun != null)
            {
                return BadRequest("Bu ürün zaten kayıtlı!");
            }

            // UrunResim'i null olarak atanacak şekilde kontrol edin
            string? urunResim = string.IsNullOrEmpty(UrunResim) ? null : UrunResim;


            // Null değilse yeni bir urun nesnesi oluştururuz

            Urunler_1 urun = new Urunler_1
            {
                urunAdi = UrunAd,
                urunFiyat = UrunFiyatı,
                urunImg = UrunResim,
                kategoriID = kategoriID
            };

            _context.Urunler.Add(urun);//yeni ürünü db ye ekle
            await _context.SaveChangesAsync();// asekron olarak kaydet
            return CreatedAtAction(nameof(GetUrunler), new { id = urun.urunID }, urun);
            //yeni oluşturulan urun nesnesi GetUrunler methoduna döndürülür(yeni id, yeni idnin nesnesi şeklinde)

        }

        //PUT: api/urunler/id//update

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUrun(int id, Urunler_1 updatedUrun/*yeni girilen verler*/)
        {
            if (id != updatedUrun.urunID)
            {
                return BadRequest("Geçersiz urun ID!");
            }

            var existingUrun = await _context.Urunler.FindAsync(id);// Güncellenecek urunu veritabanından asekron olarakbul.

            if (existingUrun == null)
            {
                return NotFound();
            }

            // Aynı ürün adı ve fiyatına sahip başka bir ürünün olup olmadığını kontrol et
            var otherUrunWithSameNameAndPrice = await _context.Urunler.FirstOrDefaultAsync(u => u.urunAdi == updatedUrun.urunAdi && u.urunFiyat == updatedUrun.urunFiyat && u.urunID != id);
            if (otherUrunWithSameNameAndPrice != null)
            {
                return BadRequest("Bu ürün adı ve fiyatı zaten kullanılıyor!");
            }

            try
            {
                //bulunan idnin alanlarını update et
                existingUrun.urunAdi = updatedUrun.urunAdi;
                existingUrun.urunFiyat = updatedUrun.urunFiyat;
                existingUrun.urunImg = updatedUrun.urunImg;
                existingUrun.kategoriID = updatedUrun.kategoriID;
                await _context.SaveChangesAsync();// // Değişiklikleri veritabanına kaydet.

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UrunExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// urun bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }

            }

            return Ok(existingUrun); //Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
        }

        private bool UrunExists(int id) // Verilen ID'ye sahip urunun veritabanında var olup olmadığını kontrol et.
        {

            return _context.Urunler.Any(u => u.urunID == id);
        }

        //DELETE: api/Urununler/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrun(int id)
        {
            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }

            _context.Urunler.Remove(urun); //urunlerden kaldır
            await _context.SaveChangesAsync(); //asekron olarak kaydet

            return NoContent();// İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                               // için NoContent (204) state döndür.
        }
    }
}
