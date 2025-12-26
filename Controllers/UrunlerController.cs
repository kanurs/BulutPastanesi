using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulutPastanesi.Data;
using BulutPastanesi.Models;

namespace BulutPastanesi.Controllers
{
    public class UrunlerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UrunlerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Urunler
        public async Task<IActionResult> Index()
        {
            // GÜVENLİK KONTROLÜ: Giriş yapmamışsa Login sayfasına at
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            var applicationDbContext = _context.Urunler.Include(u => u.Kategori);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Urunler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // GET: Urunler/Create
        public IActionResult Create()
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad");
            return View();
        }

        // POST: Urunler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Aciklama,Fiyat,ResimUrl,KategoriId,VitrinDurumu")] Urun urun)
        {
            // GÜVENLİK KONTROLÜ (Post işlemine de ekledik ki hackerlar direkt istek atamasın)
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(urun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler.FindAsync(id);
            if (urun == null)
            {
                return NotFound();
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", urun.KategoriId);
            return View(urun);
        }

        // POST: Urunler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Aciklama,Fiyat,ResimUrl,KategoriId,VitrinDurumu")] Urun urun)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(urun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UrunExists(urun.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KategoriId"] = new SelectList(_context.Kategoriler, "Id", "Ad", urun.KategoriId);
            return View(urun);
        }

        // GET: Urunler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (urun == null)
            {
                return NotFound();
            }

            return View(urun);
        }

        // POST: Urunler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            var urun = await _context.Urunler.FindAsync(id);
            if (urun != null)
            {
                _context.Urunler.Remove(urun);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UrunExists(int id)
        {
            return _context.Urunler.Any(e => e.Id == id);
        }
    }
}