using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Institution.Controllers
{
	public class HomeController : InstitutionBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
