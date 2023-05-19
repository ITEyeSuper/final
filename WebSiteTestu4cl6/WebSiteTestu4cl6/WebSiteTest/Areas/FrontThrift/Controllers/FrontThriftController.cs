using Microsoft.AspNetCore.Mvc;
using WebSiteTest.Areas.supplier.Models;

namespace WebSiteTest.Areas.FrontThrift.Controllers
{
    [Area("FrontThrift")]
    public class FrontThriftController : Controller
    {
        ComperWebSiteContext _context;
        public FrontThriftController(ComperWebSiteContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products() 
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}
