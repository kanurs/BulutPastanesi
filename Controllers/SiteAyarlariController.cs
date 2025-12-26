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
    public class SiteAyarlariController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SiteAyarlariController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiteAyarlari
        public async Task<IActionResult> Index()
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            return View(await _context.SiteAyarlari.ToListAsync());
        }

        // GET: SiteAyarlari/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var siteAyarlari = await _context.SiteAyarlari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteAyarlari == null)
            {
                return NotFound();
            }

            return View(siteAyarlari);
        }

        // GET: SiteAyarlari/Create
        public IActionResult Create()
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: SiteAyarlari/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirmaAdi,Telefon,Email,Adres,Hakkimizda")] SiteAyarlari siteAyarlari)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(siteAyarlari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(siteAyarlari);
        }

        // GET: SiteAyarlari/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var siteAyarlari = await _context.SiteAyarlari.FindAsync(id);
            if (siteAyarlari == null)
            {
                return NotFound();
            }
            return View(siteAyarlari);
        }

        // POST: SiteAyarlari/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaAdi,Telefon,Email,Adres,Hakkimizda")] SiteAyarlari siteAyarlari)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id != siteAyarlari.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siteAyarlari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SiteAyarlariExists(siteAyarlari.Id))
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
            return View(siteAyarlari);
        }

        // GET: SiteAyarlari/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            if (id == null)
            {
                return NotFound();
            }

            var siteAyarlari = await _context.SiteAyarlari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siteAyarlari == null)
            {
                return NotFound();
            }

            return View(siteAyarlari);
        }

        // POST: SiteAyarlari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // GÜVENLİK KONTROLÜ
            if (HttpContext.Session.GetString("Yonetici") == null) return RedirectToAction("Login", "Account");

            var siteAyarlari = await _context.SiteAyarlari.FindAsync(id);
            if (siteAyarlari != null)
            {
                _context.SiteAyarlari.Remove(siteAyarlari);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiteAyarlariExists(int id)
        {
            return _context.SiteAyarlari.Any(e => e.Id == id);
        }
    }
}