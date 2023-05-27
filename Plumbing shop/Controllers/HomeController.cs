using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Plumbing_shop.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Plumbing_shop.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly PlumbingDbContext db;

        public HomeController(ILogger<HomeController> logger, PlumbingDbContext context)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            Product product = new Product(db, 1);
            return View(product);
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