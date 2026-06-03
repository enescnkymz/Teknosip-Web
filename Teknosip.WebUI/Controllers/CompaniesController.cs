using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.CompanyProfiles.Queries.GetApprovedCompanies;
using Teknosip.Application.Features.CompanyProfiles.Queries.GetCompanyDetail;

namespace Teknosip.WebUI.Controllers
{
	public class CompaniesController : Controller
	{
		private readonly IMediator _mediator;

		public CompaniesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var query = new GetApprovedCompaniesQuery();
			var companies = await _mediator.Send(query, cancellationToken);

			return View(companies);
		}

		public async Task<IActionResult> Details(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetCompanyDetailQuery(id);
			var company = await _mediator.Send(query, cancellationToken);

			// Firma bulunamazsa (veya onaylı değilse) bizim o şık 404 sayfasına düşsün
			if (company == null)
			{
				return NotFound();
			}

			return View(company);
		}


	}

}
