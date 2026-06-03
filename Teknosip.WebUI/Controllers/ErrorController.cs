using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Controllers
{
	public class ErrorController : Controller
	{
		
		[Route("/Error/{statusCode}")]
		public IActionResult HttpStatusCodeHandler(int statusCode)
		{
			switch (statusCode)
			{
				case 404:
					return View("NotFound404"); 
				case 403:
					return View("AccessDenied");
				default:
					return View("NotFound404"); 
			}
		}

	}
}
