using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteTest.Areas.supplier.Models;

namespace WebSiteTest.Areas.supplier.Controllers
{
    [Authorize]
    [Area("supplier")]
    public class AdvertisesController : Controller
    {
        private readonly ComperWebSiteContext _context;

        public AdvertisesController(ComperWebSiteContext context)
        {
            _context = context;
        }

        // GET: supplier/Advertises
        public async Task<IActionResult> Index()
        {
              return _context.Advertises != null ? 
                          View(await _context.Advertises.ToListAsync()) :
                          Problem("Entity set 'ComperWebSiteContext.Advertises'  is null.");
        }

        // GET: supplier/Advertises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Advertises == null)
            {
                return NotFound();
            }

            var advertises = await _context.Advertises
                .FirstOrDefaultAsync(m => m.Adid == id);
            if (advertises == null)
            {
                return NotFound();
            }

            return View(advertises);
        }

        // GET: supplier/Advertises/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: supplier/Advertises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Adid,Adtitle,Adcontent,Adstart,Adend,Adpicture,Employee,Adviews")] Advertises advertises)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertises);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertises);
        }

        // GET: supplier/Advertises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advertises == null)
            {
                return NotFound();
            }

            var advertises = await _context.Advertises.FindAsync(id);
            if (advertises == null)
            {
                return NotFound();
            }
            return View(advertises);
        }

        // POST: supplier/Advertises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Adid,Adtitle,Adcontent,Adstart,Adend,Adpicture,Employee,Adviews")] Advertises advertises)
        {
            if (id != advertises.Adid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertises);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisesExists(advertises.Adid))
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
            return View(advertises);
        }

        // GET: supplier/Advertises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Advertises == null)
            {
                return NotFound();
            }

            var advertises = await _context.Advertises
                .FirstOrDefaultAsync(m => m.Adid == id);
            if (advertises == null)
            {
                return NotFound();
            }

            return View(advertises);
        }

        // POST: supplier/Advertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Advertises == null)
            {
                return Problem("Entity set 'ComperWebSiteContext.Advertises'  is null.");
            }
            var advertises = await _context.Advertises.FindAsync(id);
            if (advertises != null)
            {
                _context.Advertises.Remove(advertises);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisesExists(int id)
        {
          return (_context.Advertises?.Any(e => e.Adid == id)).GetValueOrDefault();
        }
        //public async Task<FileResult> ShowPhoto(int id)
        //{
        //    Advertises? Advertises = await _context.Advertises.FindAsync(id);
        //    byte[]? content = Advertises?.Adpicture;
        //    return File(content, "image/jpeg");
        //}
    }
}
