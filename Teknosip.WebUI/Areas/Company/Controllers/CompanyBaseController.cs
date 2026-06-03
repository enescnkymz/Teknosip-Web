using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Company.Controllers
{


	[Area("Company")]
	[Authorize(Roles = "Company")]
	public abstract class CompanyBaseController : Controller
	{

		
	}


}
