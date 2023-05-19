using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSiteTest.Areas.ProductsFront.Models;
using WebSiteTest.Areas.ProductsFront.Models.DTO;
using WebSiteTest.Areas.ProductsManager.Models;

namespace WebSiteTest.Areas.ProductsFront.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsManager.Models.ComperWebSiteContext _context;
        private readonly WebSiteTest.Areas.ProductsFront.Models.ComperWebSiteContext _contextP ;

        public ProductsController(ProductsManager.Models.ComperWebSiteContext context, WebSiteTest.Areas.ProductsFront.Models.ComperWebSiteContext contextP)
        {
            _context = context;
            _contextP = contextP;
        }

        // GET: api/Products
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
                ProductId = emp.ProductId,
            });
        }


        [HttpGet("{Cid}")]
        public async Task<IEnumerable<FavoriteList>> GetfavotiteProducts(string Cid)
        {
            return _contextP.FavoriteList.Where(pro => pro.CustomerId == Cid).Select(pro => new FavoriteList
            {
                CustomerId = pro.CustomerId,
                ProductId = pro.ProductId,
            });
        }

        //GET: api/Products/5
        [HttpGet("cart/{Cid}/{Pid}")]
        public async Task<IEnumerable<ShoppingCart>> GetCartProducts(string Cid, string Pid)
        {
            return _contextP.ShoppingCart.Where(SC => SC.ProductId == Pid && SC.CustomerId == Cid).Select(SC => new ShoppingCart
            {
                CustomerId = SC.CustomerId,
                ProductId = SC.ProductId,
                ProductQty = SC.ProductQty,
                ProductUnitPrice = SC.ProductUnitPrice,
            });
        }

        [HttpGet("cart/{Cid}")]
        public async Task<IEnumerable<ShoppingCart>> GetCartProducts2(string Cid)
        {
            return _contextP.ShoppingCart.Where(SC => SC.CustomerId == Cid).Select(SC => new ShoppingCart
            {
                CustomerId = SC.CustomerId,
                ProductId = SC.ProductId,
                ProductQty = SC.ProductQty,
                ProductUnitPrice = SC.ProductUnitPrice,
            });
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("cart/{Cid}/{Pid}/{Qty}")]
        public async Task<string> PutCartProducts(string Cid, string Pid, int Qty)
        {
            ShoppingCart cart = await _contextP.ShoppingCart.FindAsync(Cid, Pid);
            cart.ProductQty = Qty;
            _contextP.Entry(cart).State = EntityState.Modified;

            try
            {
                await _contextP.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return "修改成功";
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostProducts(ShoppingCart shoppingcart)
        {
            ShoppingCart cart = new ShoppingCart
            {
                CustomerId = shoppingcart.CustomerId,
                ProductId = shoppingcart.ProductId,
                ProductQty = shoppingcart.ProductQty,
                ProductUnitPrice= shoppingcart.ProductUnitPrice,
            };
            _contextP.ShoppingCart.Add(cart);
            await _contextP.SaveChangesAsync();
            return "成功加入購物車";
        }

        [HttpPost("Favorite")]
        public async Task<string> PostProductstoFavorite(FavoriteList products)
        {
            FavoriteList favoriteList = new FavoriteList
            {
                ProductId = products.ProductId,
                CustomerId = products.CustomerId,
            };
            _contextP.FavoriteList.Add(favoriteList);
            await _contextP.SaveChangesAsync();
            return "成功新增我的最愛";
        }

        // DELETE: api/Products/5
        [HttpDelete("{Productid}/{Customerid}")]
        public async Task<string> DeleteProducts(string Productid, string Customerid)
        {
            //var ID = id.Split('|');
            var favoriteProduct = _contextP.FavoriteList.Where(pro => pro.ProductId == Productid && pro.CustomerId == Customerid).FirstOrDefault();
            if (favoriteProduct == null)
            {
                return "移除失敗";
            }
            _contextP.FavoriteList.Remove(favoriteProduct);
            await _contextP.SaveChangesAsync();
            return "移除成功";
        }

        private bool ProductsExists(string id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
