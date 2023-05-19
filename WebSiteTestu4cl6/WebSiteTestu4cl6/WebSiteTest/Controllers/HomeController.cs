using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSiteTest.Areas.ThriftShop.Models;
using WebSiteTest.Models;

namespace WebSiteTest.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ComperWebSiteContext _context;

        public HomeController(ILogger<HomeController> logger, ComperWebSiteContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index_front()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult thrift() 
        {
            return View();
        }
        public IActionResult Widgets()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}