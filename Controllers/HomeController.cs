using BulutPastanesi.Data;
using BulutPastanesi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq; // FirstOrDefault için gerekli

namespace BulutPastanesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ANASAYFA
        public async Task<IActionResult> Index(int? k)
        {
            // Kategorileri menü için gönder
            ViewBag.Kategoriler = await _context.Kategoriler.ToListAsync();

            var urunler = _context.Urunler.Include(u => u.Kategori).AsQueryable();

            if (k != null)
            {
                urunler = urunler.Where(x => x.KategoriId == k);
            }

            return View(await urunler.ToListAsync());
        }

        // HAKKIMIZDA SAYFASI (Privacy yerine bunu kullanýyoruz)
        public IActionResult Privacy()
        {
            // Veritabanýndaki ilk ayar kaydýný getir
            var ayar = _context.SiteAyarlari.FirstOrDefault();
            return View(ayar);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}