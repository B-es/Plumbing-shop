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
        public Product[] products;
        private readonly ILogger<HomeController>
    _logger;
        public HomeController(ILogger<HomeController>
            logger)
        {
            _logger = logger;
            Product product = new("Хрен", "Нестарый", "Скоро опишу это поеботу...");
            Product product1 = new("Был", "Стал", "Владислав, ду ай ноу ю ор ноу...");
            Product product2 = new("Тест", "Хуест", "Полнейший выполненный протест...");
            products = new Product[] { product, product1, product2, product, product1, product2, product2, product2,
            product, product1, product2, product, product1, product2, product2, product2};
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            var count = products.Length;
            var items = products.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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
