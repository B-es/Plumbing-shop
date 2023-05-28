using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Plumbing_shop.Controllers
{
	public class AdviserController : Controller
	{
		// GET: AdviserController
		[HttpGet]
		public IActionResult check(string button)
		{
			ViewData[button] = "block"; 
			return View("~/Views/Home/Adviser.cshtml");
		}
	}
}
