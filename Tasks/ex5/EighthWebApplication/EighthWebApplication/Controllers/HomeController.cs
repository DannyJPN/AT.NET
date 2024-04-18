using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EighthWebApplication.Models;
using EighthWebApplication.Services;
using Microsoft.AspNetCore.Http;

namespace EighthWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private ProductReturner productret;
        public HomeController(ProductReturner prodret)
        {
            productret = prodret;
        }
        public IActionResult Index()
        {
            ViewBag.Products = productret.GetProducts();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public IActionResult ProductDetail(uint prodID)
        {

            this.Request.Cookies.TryGetValue("basket", out string basketData);
            ViewBag.Basket = basketData;
            ViewBag.Product = productret.GetProduct(prodID);
            return View(new BasketForm() { ProductID = prodID, Count = 1 });
        }

        [HttpPost]
        public IActionResult ProductDetail(BasketForm bform)
        {

            this.Response.Cookies.Append("Basket", bform.ProductID + "|" + bform.Count, new CookieOptions() { IsEssential = true });
            ViewBag.Product = productret.GetProduct(bform.ProductID.Value);
            if (!ModelState.IsValid)
            {
                
                return View(bform);
            }

            return View();

        }




    }
}
