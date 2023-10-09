using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Menuler_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Menuler_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Menuler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menuler_1>>> GetMenuler()
        {
            if (_context.Menuler == null)
            {
                return BadRequest("Kayıtlı Menüler bulunamadı");
            }
            return await _context.Menuler.ToListAsync();
        }


        // GET: api/Menuler/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Menuler_1>> GetMenulerById(int id)
        {
            var menuler = await _context.Menuler.FindAsync(id);

            if (menuler == null)
            {
                return NotFound();
            }

            return menuler;
        }

        // POST: api/Adres
        [HttpPost]
        public async Task<ActionResult<Menuler_1>> CreateMenuler(int Menuid, int Urunid)
        {

            if ((Menuid < 800 && Menuid > 899) || (Urunid < 900 && Urunid > 999))// Boş veya geçersiz Menulerin kontrolünü yap.
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }

            // Aynı Menuid ve Urunid değerlerine sahip bir menü var mı diye kontrol et.
            var existingMenuler = await _context.Menuler.FirstOrDefaultAsync(m => m.menuID == Menuid && m.urunID == Urunid);
            if (existingMenuler != null)
            {
                return BadRequest("Bu Menuid ve Urunid'ye sahip bir menü zaten mevcut.");
            }

            Menuler_1 menuler = new Menuler_1  // Yeni bir Adres_1 nesnesi oluştur ve verileri ata.
            {
                menuID = Menuid,
                urunID = Urunid
            };


            _context.Menuler.Add(menuler); // Oluşturulan adresi veritabanına ekle.
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMenuler), new { id = menuler.menulerID }, menuler);


        }


        // PUT: api/Menuler/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuler(int id, Menuler_1 updatedMenuler/*yeni girilen menuler*/)
        {

            if (id != updatedMenuler.menulerID) // İsteğin menuler ID'si, güncellenen menulerin menuler ID'siyle eşleşmiyor ise hata döndür.
            {
                return BadRequest("Geçersiz adres ID!");
            }


            var existingMenuler = await _context.Menuler.FindAsync(id); // Güncellenecek menuleri veritabanından bul.


            if (existingMenuler == null)   // Eğer menuler bulunamaz ise, NotFound (404) hatası döndür.
            {
                return NotFound();
            }

            // Aynı Menuid ve Urunid değerlerine sahip bir menü var mı diye kontrol et.
            var duplicateMenuler = await _context.Menuler.FirstOrDefaultAsync(m => m.menulerID != id && m.menuID == updatedMenuler.menuID && m.urunID == updatedMenuler.urunID);
            if (duplicateMenuler != null)
            {
                return BadRequest("Bu Menuid ve Urunid'ye sahip bir menü zaten mevcut.");
            }

            try
            {
                // Var olan menuler alanlarını güncelle.
                existingMenuler.menuID = updatedMenuler.menuID;
                existingMenuler.urunID = updatedMenuler.urunID;


                await _context.SaveChangesAsync();// Değişiklikleri veritabanına kaydet.
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!MenulerExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// menuler bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }

            // Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
            return Ok(existingMenuler);


        }

        private bool MenulerExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Menuler.Any(mler => mler.menulerID == id);
        }


        // DELETE: api/Menuler/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuler(int id)
        {

            var menuler = await _context.Menuler.FindAsync(id); // Belirtilen adres ID'sine sahip adresi veritabanından bul.
            if (menuler == null)
            {

                return NotFound(); // Adres bulunamaz ise, NotFound (404) hatası döndür.
            }

            _context.Menuler.Remove(menuler);// Adresi veritabanından kaldır.
            await _context.SaveChangesAsync();

            return NoContent(); // İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                                // için NoContent (204) state döndür.
        }

















    }
}
