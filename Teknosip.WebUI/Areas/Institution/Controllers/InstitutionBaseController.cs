using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Institution.Controllers
{

	[Area("Institution")]
	[Authorize(Roles = "Institution")]
    public abstract class InstitutionBaseController : Controller
	{
		
	}
}
