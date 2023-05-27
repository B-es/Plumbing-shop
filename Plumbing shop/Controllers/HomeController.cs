using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using Plumbing_shop.Models;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace Plumbing_shop.Controllers
{
    public class HomeController : Controller
    {
        public Test[] products;
        private readonly ILogger<HomeController> _logger;
        private readonly PlumbingDbContext db;

        public HomeController(ILogger<HomeController>logger, PlumbingDbContext context)
        {
            _logger = logger;
            Test product = new("Хрен", "Нестарый", "Скоро опишу это поеботу...");
            Test product1 = new("Был", "Стал", "Владислав, ду ай ноу ю ор ноу...");
            Test product2 = new("Тест", "Хуест", "Полнейший выполненный протест...");
            products = new Test[] { product, product1, product2, product, product1, product2, product2, product2,
            product, product1, product2, product, product1, product2, product2, product2};
            db = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            var count = products.Length;

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Products = products
            };
            return View(viewModel);
        }

        public IActionResult Adviser()
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
