using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Restaurant_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Restaurant_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant_1>>> GetRestaurant()
        {
            if (_context.Restaurantlar == null)
            {
                return BadRequest("Kayıtlı Menu bulunamadı");
            }
            return await _context.Restaurantlar.ToListAsync();
        }

        // GET: api/Restaurant/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant_1>> GetRestaurantById(int id)
        {
            var restaurant = await _context.Restaurantlar.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return restaurant;
        }


        //POST: api/Restaurant

        [HttpPost]
        public async Task<ActionResult<Restaurant_1>> CreateRestaurant(string restAdi, string restAdres, string restTel, string restImg)
        {

            if (string.IsNullOrEmpty(restAdi) || string.IsNullOrEmpty(restAdres) || string.IsNullOrEmpty(restTel))// Boş veya geçersiz kontrolünü yap.
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }


            Restaurant_1 restaurant = new Restaurant_1  // Yeni bir Adres_1 nesnesi oluştur ve verileri ata.
            {

                restaurantAd = restAdi,
                restaurantAdres = restAdres,
                restaurantTel = restTel,
                restaurantImg = restImg
            };


            _context.Restaurantlar.Add(restaurant); // Oluşturulan adresi veritabanına ekle.
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRestaurant), new { id = restaurant.restaurantID }, restaurant); // CreatedAtAction yöntemiyle 201 Created durumu ile oluşturulan adresi döndürür.

        }




        //PUT: api/Restaurant/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRestaurant(int id, Restaurant_1 updatedRestaurant/*yeni girilen nesne*/)
        {

            if (id != updatedRestaurant.restaurantID) // İsteğin adres ID'si, güncellenen adresin adres ID'siyle eşleşmiyor ise hata döndür.
            {
                return BadRequest("Geçersiz adres ID!");
            }


            var existingRestaurant = await _context.Restaurantlar.FindAsync(id); // Güncellenecek adresi veritabanından bul.

            // Eğer adres bulunamaz ise, NotFound (404) hatası döndür.
            if (existingRestaurant == null)
            {
                return NotFound();
            }

            try
            {
                // Var olan adresin alanlarını güncelle.

                existingRestaurant.restaurantAd = updatedRestaurant.restaurantAd;
                existingRestaurant.restaurantAdres = updatedRestaurant.restaurantAdres;
                existingRestaurant.restaurantTel = updatedRestaurant.restaurantTel;
                existingRestaurant.restaurantImg = updatedRestaurant.restaurantImg;

                await _context.SaveChangesAsync();// Değişiklikleri veritabanına kaydet.
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!RestaurantExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, adresin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// Adres bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }


            return Ok(existingRestaurant);// Ok (200): İstek başarılı ve güncellenmiş adresi döndürüyoruz.

        }

        private bool RestaurantExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Restaurantlar.Any(r => r.restaurantID == id);
        }


        // DELETE: api/Restaurant/id

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {

            var restaurant = await _context.Restaurantlar.FindAsync(id); // Belirtilen adres ID'sine sahip adresi veritabanından bul.
            if (restaurant == null)
            {

                return NotFound(); // Adres bulunamaz ise, NotFound (404) hatası döndür.
            }


            _context.Restaurantlar.Remove(restaurant);// Adresi veritabanından kaldır.
            await _context.SaveChangesAsync();
            return NoContent(); // İsteğin başarıyla işlendiğini belirtir herhangi bir değer geriye döndürmez
                                // için NoContent (204) state döndür.
        }
    }
}
