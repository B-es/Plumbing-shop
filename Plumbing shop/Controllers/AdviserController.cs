using Microsoft.AspNetCore.Mvc;

namespace Plumbing_shop.Controllers
{
	public class AdviserController : Controller
	{
        public IActionResult Index(string button)
        {
            return View();
        }

        // GET: AdviserController
        [HttpPost]
		public IActionResult GetData()
		{
            string? data = Request.Form.FirstOrDefault(x => x.Key == "submit").Value;

            TempData["data"] = data;
            TempData["isRec"] = true;
            return Redirect("~/Home/Index");
		}
	}
}
