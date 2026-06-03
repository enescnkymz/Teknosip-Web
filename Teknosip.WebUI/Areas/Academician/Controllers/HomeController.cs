using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Academician.Controllers
{

	public class HomeController : AcademicianBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
