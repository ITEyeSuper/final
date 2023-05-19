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
    public class ThriftShopOrdersController : Controller
    {
        private readonly ComperWebSiteContext _context;

        public ThriftShopOrdersController(ComperWebSiteContext context)
        {
            _context = context;
        }

        // GET: ThriftShop/ThriftShopOrders
        public async Task<IActionResult> Index()
        {
              return _context.ThriftShopOrders != null ? 
                          View(await _context.ThriftShopOrders.ToListAsync()) :
                          Problem("Entity set 'ComperWebSiteContext.ThriftShopOrders'  is null.");
        }

        // GET: ThriftShop/ThriftShopOrders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ThriftShopOrders == null)
            {
                return NotFound();
            }

            var thriftShopOrders = await _context.ThriftShopOrders
                .FirstOrDefaultAsync(m => m.ThriftOrderId == id);
            if (thriftShopOrders == null)
            {
                return NotFound();
            }

            return View(thriftShopOrders);
        }

        // GET: ThriftShop/ThriftShopOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ThriftShop/ThriftShopOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThriftOrderId,ThriftCustomerId,ThriftEmployeeId,ThriftOrderDate,ThriftShipDate,ThriftOrderStatus,ThriftShipAddress,ShippingMethod")] ThriftShopOrders thriftShopOrders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thriftShopOrders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thriftShopOrders);
        }

        // GET: ThriftShop/ThriftShopOrders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ThriftShopOrders == null)
            {
                return NotFound();
            }

            var thriftShopOrders = await _context.ThriftShopOrders.FindAsync(id);
            if (thriftShopOrders == null)
            {
                return NotFound();
            }
            return View(thriftShopOrders);
        }

        // POST: ThriftShop/ThriftShopOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ThriftOrderId,ThriftCustomerId,ThriftEmployeeId,ThriftOrderDate,ThriftShipDate,ThriftOrderStatus,ThriftShipAddress,ShippingMethod")] ThriftShopOrders thriftShopOrders)
        {
            if (id != thriftShopOrders.ThriftOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thriftShopOrders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThriftShopOrdersExists(thriftShopOrders.ThriftOrderId))
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
            return View(thriftShopOrders);
        }

        // GET: ThriftShop/ThriftShopOrders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ThriftShopOrders == null)
            {
                return NotFound();
            }

            var thriftShopOrders = await _context.ThriftShopOrders
                .FirstOrDefaultAsync(m => m.ThriftOrderId == id);
            if (thriftShopOrders == null)
            {
                return NotFound();
            }

            return View(thriftShopOrders);
        }

        // POST: ThriftShop/ThriftShopOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ThriftShopOrders == null)
            {
                return Problem("Entity set 'ComperWebSiteContext.ThriftShopOrders'  is null.");
            }
            var thriftShopOrders = await _context.ThriftShopOrders.FindAsync(id);
            if (thriftShopOrders != null)
            {
                _context.ThriftShopOrders.Remove(thriftShopOrders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThriftShopOrdersExists(string id)
        {
          return (_context.ThriftShopOrders?.Any(e => e.ThriftOrderId == id)).GetValueOrDefault();
        }
    }
}
