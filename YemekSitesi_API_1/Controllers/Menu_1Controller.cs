using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Menu_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Menu_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu_1>>> GetMenuAll()
        {
            if (_context.Menu == null)
            {
                return BadRequest("Kayıtlı Menu bulunamadı");
            }
            return await _context.Menu.ToListAsync();
        }


        //GET: api/Menu/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu_1>> GetMenuById(int id)
        {
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return Ok(menu); //(200) istek başarılı istenilen değer döndürülür
        }

        //POST: api/Menu
        [HttpPost]
        public async Task<ActionResult<Menu_1>> CreateMenu(string MenuAdi, string MenuImg)
        {
            if (string.IsNullOrEmpty(MenuAdi))
            {
                return BadRequest("Girilen değerler Boş veya geçersiz");
            }

            // Aynı MenuAdi'ye sahip menüyü veritabanında arayın
            var existingMenu = await _context.Menu.FirstOrDefaultAsync(m => m.menuAdi == MenuAdi);
            if (existingMenu != null)
            {
                // Eğer aynı MenuAdi'ye sahip menü zaten varsa BadRequest döndürün.
                return BadRequest("Bu MenuAdi'na sahip bir menü zaten mevcut.");
            }

            Menu_1 menu = new Menu_1
            {
                menuAdi = MenuAdi,
                menuImg = MenuImg
            };

            _context.Menu.Add(menu); //oluşturulan menu nesnesi Menu tablosuna ekle
            await _context.SaveChangesAsync(); //asekron olarak kaydet
            return CreatedAtAction(nameof(GetMenuAll), new { id = menu.menuID }, menu);// CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndürür.
        }

        //PUT: api/Menu/id //update

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenu(int id, Menu_1 updatedMenu/*yeni girilen veriler*/)
        {
            if (id != updatedMenu.menuID)
            {
                return BadRequest("geçersiz Menu ID");
            }

            var existingMenu = await _context.Menu.FindAsync(id); //veri tabanından id yi asekron olarak bul

            if (existingMenu == null) //eğer yok ise
            {
                return NotFound();
            }

            // Güncelleme işlemi öncesinde, yeni girilen menuAdi değeri ile diğer menülerin menuAdi değerlerini karşılaştır
            var duplicateMenu = await _context.Menu.FirstOrDefaultAsync(m => m.menuID != id && m.menuAdi == updatedMenu.menuAdi);
            if (duplicateMenu != null)
            {
                return BadRequest("Bu MenuAdi'na sahip bir menü zaten mevcut.");
            }

            try
            {
                //bulunan idnin ilgili alanlarını güncelle
                existingMenu.menuAdi = updatedMenu.menuAdi;
                existingMenu.menuImg = updatedMenu.menuImg;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {
                    return NotFound();
                }
                else
                {
                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }

            return Ok(existingMenu);  // Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.
        }

        private bool MenuExists(int id) //// Verilen ID'ye sahip menunün veritabanında var olup olmadığını kontrol et.  bool tipinde değer dön.
        {
            return _context.Menu.Any(m => m.menuID == id);
        }


        //DELETE: api/Menu/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id); //girilen idyi asekron olarak menu tablosundan bul
            if (menu == null)
            {
                return NotFound();// menu ıdsi bulunmazsa, NotFound(404) hatası döndür
            }

            _context.Menu.Remove(menu); //idsi eşleşen menüyü veritabanından kaldır
            await _context.SaveChangesAsync();// veritabanındaki değişiklikleri asekron olarak kaydet

            return NoContent();// İsteğin başarıyla işlendiğini belirtmek için NoContent (204) yanıtı döndür.
        }

    }
}
