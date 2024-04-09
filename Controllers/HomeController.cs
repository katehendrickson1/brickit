using brickit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace brickit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILegoRepository _repo;

        public HomeController( ILegoRepository temp)
        {
            _repo = temp;
        }

        public IActionResult Index()
        {
        var limit = 30;

        ViewBag.Customers = _repo.Customer.ToList().Take(limit);
        var orders = _repo.Orders.ToList().Take(limit);
        return View(orders);
        }

        public IActionResult testView()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _repo.Add(product);
            _repo.SaveChanges();
            return View();
        }

        public IActionResult ProductList()
        {
            var products = _repo.Products.ToList();

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
