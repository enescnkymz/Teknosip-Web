using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.AcademicianProfile.Queries.GetAcademicianDetail;
using Teknosip.Application.Features.AcademicianProfile.Queries.GetApprovedAcademicians;

namespace Teknosip.WebUI.Controllers
{
	public class AcademiciansController : Controller
	{

		private readonly IMediator _mediator;

		public AcademiciansController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var query = new GetApprovedAcademiciansQuery();
			var academicians = await _mediator.Send(query, cancellationToken);

			return View(academicians);
		}


		public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetAcademicianDetailQuery { Id = id };
			var academician = await _mediator.Send(query, cancellationToken);

			if (academician == null)
			{
				return NotFound();
			}

			return View(academician);
		}






	}
}
