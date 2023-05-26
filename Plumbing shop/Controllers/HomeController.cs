using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using Plumbing_shop.Models;
using System.Diagnostics;

namespace Plumbing_shop.Controllers
{
    public class HomeController : Controller
    {
        public Product[] products;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Product product = new("Хрен", "Нестарый", "Скоро опишу это поеботу...");
            Product product1 = new("Был", "Стал", "Владислав, ду ай ноу ю ор ноу...");
            Product product2 = new("Тест", "Хуест", "Полнейший выполненный протест...");
            products = new Product[] { product, product1, product2, product, product1, product2, product2, product2 };
            ViewBag.prod_blocks = showBlocks();
            ViewBag.len = products.Length;
            return View();
        }

        public IActionResult Adviser()
        {
            return View();
        }

        [JSInvokable]
        public string showBlocks()
        {
            int click = 0;
            string htmlcode = "<div class='product-place'>";
            for (int i = click; i < click+6; i++)
            {
                htmlcode += "<div class='product'>";
                htmlcode += $"<h2>Имя - {products[i].Name}</h2>";
                htmlcode += $"<h2>Вид - {products[i].Type}</h2>";
                htmlcode += "<br><br>";
                htmlcode += "<h4 style = 'text-align:center'>Описание:</h4>";
                htmlcode += $"<p style = 'color:lightslategray; text-align:center'>{products[i].Description}</p></div>";
            }
            htmlcode += "</div>";
            
            return htmlcode;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}