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

        public IActionResult Index(int page = 1, int page2 = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            //Выбранные
            List<Product> Products = new List<Product>();

            if (TempData["data"] != null)
                Products = RecModule.selectProducts(TempData["data"].ToString(), db);

            if (Products.Count == 0) Products = Product.createProducts(db);

            var count = Products.Count;

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Products = Products
            };

            ViewBag.viewModel = viewModel;

            //Рекомендованные
            List<Product> ProductsRec = new List<Product>();

            if (TempData["data"] != null)
                ProductsRec = RecModule.selectRecProduct(TempData["data"].ToString(), db);

            ViewBag.ProductRec = ProductsRec;

            return View();
        }

        public IActionResult ProductPlace()
        {
            return PartialView();
        }

        public IActionResult RecPlace()
        {

            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
