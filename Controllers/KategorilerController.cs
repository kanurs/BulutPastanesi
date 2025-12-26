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
    public class KategorilerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KategorilerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kategoriler
        public async Task<IActionResult> Index()
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            return View(await _context.Kategoriler.ToListAsync());
        }

        // GET: Kategoriler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Kategoriler/Create
        public IActionResult Create()
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Kategoriler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad")] Kategori kategori)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategori);
        }

        // GET: Kategoriler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            return View(kategori);
        }

        // POST: Kategoriler/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad")] Kategori kategori)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kategori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.Id))
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
            return View(kategori);
        }

        // GET: Kategoriler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Kategoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategoriler.Any(e => e.Id == id);
        }
    }
}