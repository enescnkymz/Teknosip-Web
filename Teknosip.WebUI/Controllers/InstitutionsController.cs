using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.InstitutionProfile.Queries.GetApprovedInstitutions;
using Teknosip.Application.Features.InstitutionProfile.Queries.GetInstitutionDetail;

namespace Teknosip.WebUI.Controllers
{
	public class InstitutionsController : Controller
	{
		private readonly IMediator _mediator;

		public InstitutionsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var query = new GetApprovedInstitutionsQuery();
			var institutions = await _mediator.Send(query, cancellationToken);

			return View(institutions);
		}


		public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetInstitutionDetailQuery { Id = id };
			var institution = await _mediator.Send(query, cancellationToken);

			if (institution == null)
			{
				return NotFound();
			}

			return View(institution);
		}


	}
}
