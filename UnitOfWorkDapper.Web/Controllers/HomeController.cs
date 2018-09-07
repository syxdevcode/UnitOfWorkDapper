using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UnitOfWorkDapper.Services.Entity;
using UnitOfWorkDapper.Services.Services.Interfaces;
using UnitOfWorkDapper.Web.Models;

namespace UnitOfWorkDapper.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductService productService;

        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        public IActionResult Index()
        {
            Product product = new Product();

            product.Id = 1;
            product.Name = "测试商品1";
            product.Price = 1999;

            productService.SaveAsync(product);

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}