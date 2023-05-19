using Microsoft.AspNetCore.Mvc;
using WebSiteTest.Areas.ProductsFront.Models.DTO;
using WebSiteTest.Areas.ProductsManager.Models;

namespace WebSiteTest.Areas.ProductsFront.Controllers
{
    [Area("ProductsFront")]
    public class ProductsFrontController : Controller
    {
        private readonly ComperWebSiteContext _context;

        public ProductsFrontController(ComperWebSiteContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult showproduct()
        {
            return View();
        }

        [HttpGet]
        public async Task<IEnumerable<ProductsDTO>> GetProducts()
        {
            return _context.Products.Select(emp => new ProductsDTO
            {
                ProductName = emp.ProductName,
                ProductCategory = emp.ProductCategory,
                ProductNotes = emp.ProductNotes,
                ProductPic = emp.ProductPic,
                ProductQty = emp.ProductQty,
                ProductStatus = emp.ProductStatus,
                ProductUnitPrice = emp.ProductUnitPrice,
            });
        }

    }
}
