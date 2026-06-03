using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Admin.Controllers
{
	public class HomeController : AdminBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
