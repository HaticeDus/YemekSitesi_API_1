using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using YemekSitesi_API_1.Models;

namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Kategoriler_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Kategoriler_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }



        // GET: api/Kategoriler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategoriler_1>>> GetKategoriler()
        {
            if (_context.Kategoriler == null)
            {
                return BadRequest("Kayıtlı Kategori bulunamadı");
            }
            return await _context.Kategoriler.ToListAsync();
        }




        // GET: api/Kategoriler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kategoriler_1>> GetAdres(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return BadRequest("Aranan ID bulunamadı!");
            }

            return Ok(kategori);//İstek başarılı ve aranan adresi döndürüyoruz.
        }

        // POST: api/Adres
        //[HttpPost("Post")]
        [HttpPost]
        public async Task<ActionResult<Kategoriler_1>> CreateKategori(string kategoriName, string KategoriResim)
        {

            var existingKategori = await _context.Kategoriler.FirstOrDefaultAsync(k => k.kategoriTuru.ToLower() == kategoriName.ToLower());
            if (existingKategori != null)
            {
                return BadRequest("Bu kategori zaten mevcut!");
            }


            else if (string.IsNullOrEmpty(kategoriName))
            {
                return BadRequest("Kayıtlı bir kategori ismi bulunamadı!");
            }

            Kategoriler_1 kategori = new Kategoriler_1
            {
                kategoriTuru = kategoriName,
                kategoriImg = KategoriResim
            };

            _context.Kategoriler /*context kısmındaki dbset ismi */.Add(kategori); // Oluşturulan kategoriyi veritabanına (Kategori tablosuna) ekle.
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKategoriler), new { id = kategori.kategoriID }, kategori);
        }


        // PUT: api/Kategoriler/5
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateKategori(int id, Kategoriler_1 updatedKategori)
        {
            if (id != updatedKategori.kategoriID)
            {
                return BadRequest("Geçersiz Kategori ID!");
            }
            var existingKategori = await _context.Kategoriler.FindAsync(id);

            if (existingKategori == null)
            {
                return BadRequest("Güncellemek istediğiniz kategori Bulunamadı!");
                //return NotFound();
            }

            // Kategori adının benzersiz olup olmadığını kontrol et
            var existingKategoriWithSameName = await _context.Kategoriler.FirstOrDefaultAsync(k => k.kategoriTuru.ToLower() == updatedKategori.kategoriTuru.ToLower() && k.kategoriID != id);
            if (existingKategoriWithSameName != null)
            {
                return BadRequest("Bu kategori adı zaten kullanılıyor!");
            }

            try
            {
                //existingKategori.kategoriID = id;
                existingKategori.kategoriTuru = updatedKategori.kategoriTuru;
                existingKategori.kategoriImg = updatedKategori.kategoriImg;
                await _context.SaveChangesAsync(); //değişiklikleri veritabanına kaydet
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!KategoriExists(id))
                {
                    return NotFound(); //kategori bulunmamzsa NotFound(404) hatası döndür
                }
                else
                {
                    throw;
                }
            }

            return Ok(existingKategori); //İstek başarılı ve güncellenmiş kategoriyi döndürüyoruz.
        }



        //DELETE: api/kategori/id

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteKategori(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }

            _context.Kategoriler/*'dbcontext' kategoriler tablsosunu temsil ediyor*/.Remove(kategori);
            await _context.SaveChangesAsync(); //değişiklikleri asekron kaydet
            return NoContent(); // İsteğin başarıyla işlendiğini belirtmek için NoContent (204) yanıtı döndür.
        }



        // Verilen ID'ye sahip adresin veritabanında var olup olmadığını kontrol et.
        private bool KategoriExists(int id)
        {
            return _context.Kategoriler.Any(K => K.kategoriID == id);
        }

    }


}
