using Microsoft.AspNetCore.Mvc;
using Plumbing_shop.Models;
using System.Diagnostics;


namespace Plumbing_shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlumbingDbContext db;

        public HomeController(ILogger<HomeController>logger, PlumbingDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            var Products = Product.createProducts(db);
            int pageSize = 6;   // количество элементов на странице

            var count = Products.Count;

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Products = Products
            };
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
