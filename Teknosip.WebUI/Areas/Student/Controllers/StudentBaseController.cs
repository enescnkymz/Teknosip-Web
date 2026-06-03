using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Teknosip.WebUI.Areas.Student.Controllers
{


	[Area("Student")]
	[Authorize(Roles = "Student")]
	public abstract class StudentBaseController : Controller
	{
	}

}
