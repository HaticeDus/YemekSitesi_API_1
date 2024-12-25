using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using System.Web.Http;
using YemekSitesi_API_1.Migrations;
using YemekSitesi_API_1.Models;


namespace YemekSitesi_API_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sepet_1Controller : ControllerBase
    {
        private readonly YemekSitesiDbContext _context;

        public Sepet_1Controller(YemekSitesiDbContext context)
        {
            _context = context;
        }


        // GET: api/Sepet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sepet_1>>> GetSepet()
        {
            if (_context.Sepet == null)
            {
                return BadRequest("Sepet bulunamadı");
            }
            return await _context.Sepet.ToListAsync();
        }


        // GET: api/Sepet/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanicilar_1>> GetSepetById(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);

            if (sepet == null)
            {
                return BadRequest("girilen sepet bulunamadı!");
            }

            return Ok(sepet);
        }

        [HttpPost]

        public async Task<ActionResult<Sepet_1>> CreateSepet(int userID,string productAd, int productAdet,string productImg, int productFiyat)
        {
            if (string.IsNullOrEmpty(userID.ToString()) || string.IsNullOrEmpty(productAd) || string.IsNullOrEmpty(productAdet.ToString()) || string.IsNullOrEmpty(productImg) || string.IsNullOrEmpty(productFiyat.ToString()))
            {
                return BadRequest("Girilen değerler Boş veya geçersiz!");
            }

            Sepet_1 sepet = new Sepet_1
            {
                userID = userID,
                productAd = productAd,
                productAdet = productAdet,
                productImg = productImg,
                productFiyat = productFiyat,
                //productDate otomatik olarak DateTime ayarlı 

            };

            _context.Sepet.Add(sepet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSepetById), new {id=sepet.sepetID}, sepet);
        }



        [HttpPatch]
        public async Task<IActionResult> UpdateProductAdet(int sepetID, int newProductAdet)
        {
            // Burada sepetID parametresiyle gelen sepetin productAdet alanını güncelliyoruz.
            var updatingSepetobject = await _context.Sepet.FindAsync(sepetID);

            if (updatingSepetobject != null)
            {
                updatingSepetobject.productAdet = newProductAdet;
                await _context.SaveChangesAsync(); // Değişiklikleri kaydet

                return Ok("productAdet alanı güncellendi.");
            }
            else
            {
                return NotFound();
            }
        }



        //DELETE: api/Kullanıcı/id

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSepet(int id)
        {
            var sepet = await _context.Sepet.FindAsync(id);
            //asekron olarak kullanıcı tablosundan bu id yi bul

            if (sepet == null)
            {
                return NotFound();// sepet bulunmazsa 404 döndür 
            }

            _context.Sepet.Remove(sepet);// id si buluna user'ı kaldır
            await _context.SaveChangesAsync(); //asekron olarak değişiklikleri kaydet

            return NoContent();//isteğin başarıyla işlendiğini belirtmek için NoContent (204 status kodu döndür)
        }
    }
}
