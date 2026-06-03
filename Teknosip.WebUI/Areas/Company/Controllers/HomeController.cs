using Microsoft.AspNetCore.Mvc;


namespace Teknosip.WebUI.Areas.Company.Controllers
{
	public class HomeController : CompanyBaseController
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
