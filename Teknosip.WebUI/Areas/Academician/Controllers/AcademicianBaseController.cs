using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Academician.Controllers
{


	[Area("Academician")]
	[Authorize(Roles = "Academician")]
	public abstract class AcademicianBaseController : Controller
	{
	}
}
