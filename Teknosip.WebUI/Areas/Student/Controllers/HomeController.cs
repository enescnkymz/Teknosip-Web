using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Student.Controllers
{
	public class HomeController : StudentBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
