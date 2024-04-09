using AspNetCore;
using brickit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
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

        [HttpGet]
        public IActionResult EditProd(int id)
        {
            var recordToEdit = _repo.Products
                .Single(x => x.product_ID == id);
            return View("AddProduct", recordToEdit);
        }
        [HttpPost]
        public IActionResult EditProd(Product prod)
        {
            _repo.Update(prod);
            _repo.SaveChanges();

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public IActionResult DeleteProd(int id)
        {
            var recordToDelete = _repo.Products
                .Single(x => x.product_ID == id);
            return View(recordToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProd(Product prod)
        {
            //var product = await _repo.Products.FirstOrDefaultAsync(p => p.product_ID == id);
            var product = prod;
  
            _repo.Remove(product); // Correctly removes the product from the repository
            _repo.SaveChanges();

            return RedirectToAction("ProductList"); // Assuming you have a ProductList action
        }

        public IActionResult Orders()
        {
            var orders = _repo.Orders
                .Join(_repo.LineItems, o => o.transaction_ID, li => li.transaction_ID, (o, li) => new { Order = o, LineItem = li })
                .Join(_repo.Products, j => j.LineItem.product_ID, p => p.product_ID, (j, p) => new { j.Order, j.LineItem, Product = p })
                .Join(_repo.Customers, j => j.Order.customer_ID, c => c.customer_ID, (j, c) => new { j.Order, j.LineItem, j.Product, Customer = c })
                .Select(result => new OrderViewModel
                {
                    TransactionID = result.Order.transaction_ID,
                    Date = result.Order.date,
                    CustomerName = result.Customer.first_name + " " + result.Customer.last_name,
                    ProductName = result.Product.name,
                    Quantity = result.LineItem.qty,
                    Price = result.Product.price,
                    // Add other properties you want to display
                })
                .ToList();

            return View(orders);
        }

    }
}
