using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Applications.Queries.GetMyApplications;

namespace Teknosip.WebUI.Areas.Student.Controllers
{
	public class ApplicationController : StudentBaseController 
	{
		private readonly IMediator _mediator;

		public ApplicationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var query = new GetMyApplicationsQuery
			{
				StudentId = Guid.Parse(userIdString)
			};

			var applications = await _mediator.Send(query, cancellationToken);

			return View(applications);
		}
	}
}
