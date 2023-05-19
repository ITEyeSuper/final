using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebSiteTest.Controllers
{
	public class MemberController : Controller
	{
        Areas.ThriftShop.Models.ComperWebSiteContext _thriftcontext = null;
        public IActionResult Index()
		{
			return View();
		}
		public MemberController(Areas.ThriftShop.Models.ComperWebSiteContext thriftcontext) 
		{
			_thriftcontext = thriftcontext;
		}
        public async Task<JsonResult> GetCustomersCount()
        {
            var query = _thriftcontext.ThriftProducts.GroupBy(c => c.ThriftProductCategory).Select(g => new MemberCount
            {
                ThriftProductCategory = g.Key,
                Count = g.Count()
            });
            return Json(query);
        }
    }
}
