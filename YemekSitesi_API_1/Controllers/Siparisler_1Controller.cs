using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YemekSitesi_API_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    //[Route("Siparisler")]
    [ApiController]
    public class Siparisler_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Siparisler_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }

        // GET: api/Adres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Siparisler_1>>> GetSiparisler()
        {
            if (_context.Siparisler == null)
            {
                return BadRequest("Kayıtlı Menu bulunamadı");
            }
            return await _context.Siparisler.ToListAsync();
        }

        //GET: api/Siparisler/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Siparisler_1>> GetSiparisById(int id)
        {
            var siparis = await _context.Siparisler.FindAsync(id); //db'den id bul

            if (siparis == null)
            {
                return NotFound();
            }

            return Ok(siparis); //200 değeri döndür
        }

        //POST: api/Siparisler

        [HttpPost]
        public async Task<ActionResult<Siparisler_1>> CreateSiparis(int userid, int urunid, int menulerid, int restid,/*DateTime sTarih,*/ int sAdet, int sTutar)
        {
            if (/*sTarih == DateTime.MinValue || */ sAdet < 0 || sTutar < 0)
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }

            Siparisler_1 siparis = new Siparisler_1
            {
                userID = userid,
                urunID = urunid,
                menulerID = menulerid,
                restaurantID = restid,
                //siparisTarih = sTarih,
                siparisAdet = sAdet,
                siparisTutar = sTutar

            };

            _context.Siparisler.Add(siparis);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSiparisler), new { id = siparis.siparisID }, siparis);
        }

        //PUT: api/Siparisler/id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiparis(int id, Siparisler_1 updatedSiparis/*yeni girilen değerler*/)
        {
            if (id != updatedSiparis.siparisID)
            {
                return BadRequest("Geçersiz siparis ID!");
            }

            var existingSiparis = await _context.Siparisler.FindAsync(id);

            if (existingSiparis == null)
            {
                return NotFound();
            }
            try
            {
                existingSiparis.userID = updatedSiparis.userID;
                existingSiparis.urunID = updatedSiparis.urunID;
                existingSiparis.menulerID = updatedSiparis.menulerID;
                existingSiparis.restaurantID = updatedSiparis.restaurantID;
                existingSiparis.siparisTarih = DateTime.Now;
                existingSiparis.siparisAdet = updatedSiparis.siparisAdet;
                existingSiparis.siparisTutar = updatedSiparis.siparisTutar;

            }

            catch
            {
                if (!SiparisExists(id)) // DbUpdateConcurrencyException hatası fırlatıldığında, siparişin hala mevcut olduğunu kontrol et.
                {

                    return NotFound();// sipariş bulunamaz ise, NotFound (404) hatası döndür.
                }
                else
                {

                    throw;// Başka bir hata durumunda hatayı fırlat.
                }
            }

            return Ok(existingSiparis);
        }

        private bool SiparisExists(int id) // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        {

            return _context.Siparisler.Any(s => s.siparisID == id);
        }


        // DELETE: api/Siparisler/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiparis(int id)
        {
            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }

            _context.Siparisler.Remove(siparis); //siparis nesnesini Siparisler tablosundan kaldır
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
