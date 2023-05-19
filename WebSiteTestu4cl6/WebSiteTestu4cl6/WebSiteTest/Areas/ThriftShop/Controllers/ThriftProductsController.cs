using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebSiteTest.Areas.ThriftShop.Models;

namespace WebSiteTest.Areas.ThriftShop.Controllers
{
    [Area("ThriftShop")]
    public class ThriftProductsController : Controller
    {
        private readonly ComperWebSiteContext _context;

        public ThriftProductsController(ComperWebSiteContext context)
        {
            _context = context;
        }

        // GET: ThriftShop/ThriftProducts
        public async Task<IActionResult> Index()
        {
            return _context.ThriftProducts != null ?
            View(await _context.ThriftProducts.ToListAsync()) :
            Problem("Entity set 'ComperWebSiteContext.Products'  is null.");
        }

        //[HttpPost]
        //[Route("~/Areas/ThriftShop/Views/ThriftProducts/Index/Json")]
        //public async Task<JsonResult> IndexJson()
        //{
        //    return Json(await _context.ThriftProducts.ToListAsync());
        //}

        // GET: ThriftShop/ThriftProducts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ThriftProducts == null)
            {
                return NotFound();
            }

            var thriftProducts = await _context.ThriftProducts
                .FirstOrDefaultAsync(m => m.ThriftProductId == id);
            if (thriftProducts == null)
            {
                return NotFound();
            }

            return View(thriftProducts);
        }

        // GET: ThriftShop/ThriftProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThriftShop/ThriftProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThriftProductId,ThriftProductName,ThriftProductUnitPrice,ThriftProductQty,ThriftProductCategory,ThriftProductSupplierId,ThriftProductStatus,ThriftProductOnDate,ThriftProductOffDate")] ThriftProducts thriftProducts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thriftProducts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thriftProducts);
        }

        // GET: ThriftShop/ThriftProducts/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ThriftProducts == null)
            {
                return NotFound();
            }

            var thriftProducts = await _context.ThriftProducts.FindAsync(id);
            if (thriftProducts == null)
            {
                return NotFound();
            }
            return View(thriftProducts);
        }

        // POST: ThriftShop/ThriftProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ThriftProductId,ThriftProductName,ThriftProductUnitPrice,ThriftProductQty,ThriftProductCategory,ThriftProductSupplierId,ThriftProductStatus,ThriftProductOnDate,ThriftProductOffDate")] ThriftProducts thriftProducts)
        {
            if (id != thriftProducts.ThriftProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thriftProducts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThriftProductsExists(thriftProducts.ThriftProductId))
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
            return View(thriftProducts);
        }

        // GET: ThriftShop/ThriftProducts/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ThriftProducts == null)
            {
                return NotFound();
            }

            var thriftProducts = await _context.ThriftProducts
                .FirstOrDefaultAsync(m => m.ThriftProductId == id);
            if (thriftProducts == null)
            {
                return NotFound();
            }

            return View(thriftProducts);
        }

        // POST: ThriftShop/ThriftProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ThriftProducts == null)
            {
                return Problem("Entity set 'ComperWebSiteContext.ThriftProducts'  is null.");
            }
            var thriftProducts = await _context.ThriftProducts.FindAsync(id);
            if (thriftProducts != null)
            {
                _context.ThriftProducts.Remove(thriftProducts);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThriftProductsExists(string id)
        {
            return (_context.ThriftProducts?.Any(e => e.ThriftProductId == id)).GetValueOrDefault();
        }

        public string AAAA()
        {
            return _context.ThriftProducts.Count().ToString();
        }
    }
}
