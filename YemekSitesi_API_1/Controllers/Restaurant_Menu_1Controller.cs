using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Restaurant_Menu_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Restaurant_Menu_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurant_Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant_Menu_1>>> GetRestMenu()
        {
            if (_context.Restaurant_Menu == null)
            {
                return BadRequest("Kayıtlı Menu bulunamadı");
            }
            return await _context.Restaurant_Menu.ToListAsync();
        }

        // GET: api/Restaurant_Menu/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant_Menu_1>> GetRestMenuById(int id)
        {
            var restMenu = await _context.Restaurant_Menu.FindAsync(id);

            if (restMenu == null)
            {
                return NotFound();
            }

            return restMenu;
        }

        //POST: api/RestMenu

        [HttpPost]
        public async Task<ActionResult<Restaurant_Menu_1>> CreateRestMenu(int restid, int menuid )
        {

            if ((400>restid && restid>499) || (menuid<700 && menuid>799))// Boş veya geçersiz adreslerin kontrolünü yap.
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }


            // Aynı Menuid ve Urunid değerlerine sahip bir menü var mı diye kontrol et.
            var existingRestMenu = await _context.Restaurant_Menu.FirstOrDefaultAsync(rm => rm.restaurantID == restid && rm.menuID == menuid);
            if (existingRestMenu != null)
            {
                return BadRequest("Bu Menuid ve restid'ye sahip bir menü zaten mevcut.");
            }


            Restaurant_Menu_1 restMenu = new Restaurant_Menu_1  // Yeni bir Adres_1 nesnesi oluştur ve verileri ata.
            {
                restaurantID = restid,
                menuID = menuid
            };


            _context.Restaurant_Menu.Add(restMenu); // Oluşturulan adresi veritabanına ekle.
            await _context.SaveChangesAsync();

            // CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndürür.
            return CreatedAtAction(nameof(GetRestMenu), new { id = restMenu.restMenuID }, restMenu);

        }


        //PUT: api/Rest_Menu/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestMenu(int id, Restaurant_Menu_1 updatedRestMenu/*yeni girilen verilerin nesnesi*/)
        {

            if (id != updatedRestMenu.restMenuID) // İsteğin adres ID'si, güncellenen adresin adres ID'siyle eşleşmiyor ise hata döndür.
            {
                return BadRequest("Geçersiz adres ID!");
            }


            var existingRestMenu= await _context.Restaurant_Menu.FindAsync(id); // Güncellenecek adresi veritabanından bul.

            // Eğer adres bulunamaz ise, NotFound (404) hatası döndür.
            if (existingRestMenu == null)
            {
                return NotFound();
            }

            // Aynı Menuid ve Urunid değerlerine sahip bir menü var mı diye kontrol et.
            var duplicateRestMenu = await _context.Restaurant_Menu.FirstOrDefaultAsync(rm => rm.restMenuID != id && rm.menuID == updatedRestMenu.menuID && rm.restaurantID == updatedRestMenu.restaurantID);
            if (duplicateRestMenu != null)
            {
                return BadRequest("Bu Menuid ve restaurantID'ye sahip bir menü zaten mevcut.");
            }


            try
            {
                // Var olan adresin alanlarını güncelle.
                existingRestMenu.restaurantID = updatedRestMenu.restaurantID;
                existingRestMenu.menuID = updatedRestMenu.menuID;
                await _context.SaveChangesAsync();// Değişiklikleri veritabanına kaydet.
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!RestMenuExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// Adres bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }

            return Ok(existingRestMenu);// Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
        }

        private bool RestMenuExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Restaurant_Menu.Any(rm =>rm.restMenuID == id);
        }


        // DELETE: api/RestMenu/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestMenu(int id)
        {

            var restmenu = await _context.Restaurant_Menu.FindAsync(id); // Belirtilen adres ID'sine sahip adresi veritabanından bul.
            if (restmenu == null)
            {

                return NotFound(); // Adres bulunamaz ise, NotFound (404) hatası döndür.
            }
            _context.Restaurant_Menu.Remove(restmenu);// Adresi veritabanından kaldır.
            await _context.SaveChangesAsync();
            return NoContent(); // İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                                // için NoContent (204) state döndür.
        }
    }
}
