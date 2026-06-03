using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.CompanyProfiles.Queries.GetApprovedCompanies;

namespace Teknosip.WebUI.Controllers
{
	public class HomeController : Controller
	{
		
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Faq()
		{ 
			return View();
		}

		public IActionResult About() 
		{
			return View();
		}
							
	}
}
