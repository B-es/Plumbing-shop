using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Plumbing_shop.Controllers
{
	public class AdviserController : Controller
	{
        public IActionResult Index(string button)
        {
            _ = ViewData["ClickedButton"] is null ? ViewData["ClickedButton"] = "Нихрена" : ViewData["ClickedButton"] = button;
            return View();
        }

        // GET: AdviserController
        [HttpPost]
		public IActionResult GetData()
		{
            string button = Request.Form.FirstOrDefault(x => x.Key == "submit").Value;
            Console.WriteLine(button);
            return RedirectToAction("Index");
		}
	}
}
