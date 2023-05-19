using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteTest.Areas.ProductsManager.Models;
using WebSiteTest.Areas.ProductsManager.Models.ViewModel;

namespace WebSiteTest.Areas.ProductsManager.Controllers
{
    [Authorize]
    [Area("ProductsManager")]
    public class OrdersController : Controller
    {
        private readonly ComperWebSiteContext _context;
        public OrdersController(ComperWebSiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var OrderDetails = from order in _context.Orders
                               select new OrderViewModel
                               {
                                   OrderId = order.OrderId,
                                   CustomerId = order.CustomerId,
                                   EmployeeId = order.EmployeeId,
                                   OrderOnDate = order.OrderOnDate,
                                   ShipDate = order.ShipDate,
                                   OrderStatus = order.OrderStatus,
                                   ShipAddress = order.ShipAddress,
                                   ShippingMethod = order.ShippingMethod,
                                   ProductId = order.OrderDetail.ProductId,
                                   ProductQty = order.OrderDetail.ProductQty,
                                   ProductUnitPrice = order.OrderDetail.ProductUnitPrice,
                               };
            return View(OrderDetails);
        }
        public IActionResult OrdersCreate()
        {
            return View();
        }
        public IActionResult OrderDetailCreate()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrdersCreate([Bind ("OrderId,CustomerId,EmployeeId,OrderOnDate,ShipDate,OrderStatus,ShipAddress,ShippingMethod")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                TempData["Ordersid"] = orders.OrderId;
                return RedirectToAction(nameof(OrderDetailCreate));
            }
            return View(orders);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetailCreate([Bind ("OrderId,ProductId,ProductQty,ProductUnitPrice")] OrderDetail od)
        {
            if (ModelState.IsValid)
            {
                _context.Add(od);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(od);
        }
        public IActionResult Details(string orderid)
        {
            if(orderid == null || _context.OrderDetail == null) { return NotFound(); }
            var orderdetail = _context.OrderDetail.Join(_context.Products, od => od.ProductId, p => p.ProductId, (od, p) => new
            {
                od.OrderId,
                od.ProductId,
                od.ProductQty,
                p.ProductName,
                p.ProductUnitPrice,
                p.ProductCategory
            });
            var orderdetails = from ods in orderdetail.Where(ods => ods.OrderId == orderid)
                               select new OrderViewModel
                               {
                                   OrderId = ods.OrderId,
                                   ProductId = ods.ProductId,
                                   ProductQty = ods.ProductQty,
                                   ProductName = ods.ProductName,
                                   ProductUnitPrice = ods.ProductUnitPrice,
                                   ProductCategory = ods.ProductCategory
                               };
            if (orderdetails == null) { return NotFound(); }
            return View(orderdetails);
        }
    }
}
