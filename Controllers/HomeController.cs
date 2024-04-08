using brickit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace brickit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EFLegoRepository _repo;

        public HomeController(ILogger<HomeController> logger, EFLegoRepository temp)
        {
            _logger = logger;
            _repo = temp;
        }

        public IActionResult Index()
        {
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
