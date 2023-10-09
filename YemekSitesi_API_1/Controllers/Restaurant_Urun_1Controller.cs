using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YemekSitesi_API_1.Models;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Restaurant_Urun_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Restaurant_Urun_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurant_Urun
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant_Urun_1>>> GetRestUrun()
        {
            if (_context.Restaurant_Urun == null)
            {
                return BadRequest("Kayıtlı Restaurant_Urun bulunamadı");
            }
            return await _context.Restaurant_Urun.ToListAsync();
        }


        // GET: api/Restaurant_Urun/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant_Urun_1>> GetRestUrunById(int id)
        {
            var restUrun = await _context.Restaurant_Urun.FindAsync(id);

            if (restUrun == null)
            {
                return NotFound();
            }

            return restUrun;
        }


        //POST: api/RestUrun

        [HttpPost]
        public async Task<ActionResult<Restaurant_Urun_1>> CreateRestUrun(int restaurantID, int urunID)
        {

            if ((400 > restaurantID && restaurantID > 499) || (urunID < 1000 && urunID > 1099))// Boş veya geçersiz adreslerin kontrolünü yap.
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }



            // Aynı restaurantID ve urunID değerlerine sahip bir restUrun var mı diye kontrol et.
            var existingRestUrun = await _context.Restaurant_Urun.FirstOrDefaultAsync(ru => ru.restaurantID == restaurantID && ru.urunID == urunID);
            if (existingRestUrun != null)
            {
                return BadRequest("Bu restaurantID ve urunID'ye sahip bir menü zaten mevcut.");
            }


            Restaurant_Urun_1 restUrun = new Restaurant_Urun_1  // Yeni bir Adres_1 nesnesi oluştur ve verileri ata.
            {
                restaurantID = restaurantID,
                urunID = urunID
            };


            _context.Restaurant_Urun.Add(restUrun); // Oluşturulan restUrun veritabanına ekle.
            await _context.SaveChangesAsync();

            // CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndürür.
            return CreatedAtAction(nameof(GetRestUrun), new { id = restUrun.restUrunID }, restUrun);

        }


        //PUT: api/Rest_Urun/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestUrun(int id, Restaurant_Urun_1 updatedRestUrun/*yeni girilen verilerin nesnesi*/)
        {

            if (id != updatedRestUrun.restUrunID) // İsteğin adres ID'si, güncellenen adresin adres ID'siyle eşleşmiyor ise hata döndür.
            {
                return BadRequest("Geçersiz adres ID!");
            }


            var existingRestUrun = await _context.Restaurant_Urun.FindAsync(id); // Güncellenecek adresi veritabanından bul.

            // Eğer adres bulunamaz ise, NotFound (404) hatası döndür.
            if (existingRestUrun == null)
            {
                return NotFound();
            }

            // Aynı restaurantID ve urunID değerlerine sahip bir menü var mı diye kontrol et.
            var duplicateRestUrun = await _context.Restaurant_Urun.FirstOrDefaultAsync(ru => ru.restUrunID != id && ru.restaurantID == updatedRestUrun.restaurantID && ru.urunID == updatedRestUrun.urunID);
            if (duplicateRestUrun != null)
            {
                return BadRequest("Bu restaurantID ve Urunid'ye sahip bir menü zaten mevcut.");
            }

            try
            {
                // Var olan adresin alanlarını güncelle.
                existingRestUrun.restaurantID = updatedRestUrun.restaurantID;
                existingRestUrun.urunID = updatedRestUrun.urunID;
                await _context.SaveChangesAsync();// Değişiklikleri veritabanına kaydet.
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!RestUrunExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// Adres bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }

            return Ok(existingRestUrun);// Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
        }

        private bool RestUrunExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Restaurant_Urun.Any(rm => rm.restUrunID == id);
        }



        // DELETE: api/RestUrun/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestUrun(int id)
        {

            var resturun = await _context.Restaurant_Urun.FindAsync(id); // Belirtilen adres ID'sine sahip adresi veritabanından bul.
            if (resturun == null)
            {

                return NotFound(); // Adres bulunamaz ise, NotFound (404) hatası döndür.
            }
            _context.Restaurant_Urun.Remove(resturun);// Adresi veritabanından kaldır.
            await _context.SaveChangesAsync();
            return NoContent(); // İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                                // için NoContent (204) state döndür.
        }
    }
}
