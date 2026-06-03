using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Applications.Commands.ApplyToListing;
using Teknosip.Application.Features.Listings.Queries.GetActiveListings;
using Teknosip.Application.Features.Listings.Queries.GetListingDetail;

namespace Teknosip.WebUI.Controllers
{
	public class ListingsController : Controller
	{
		private readonly IMediator _mediator;

		public ListingsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{

			var query = new GetActiveListingsQuery();
			var listings = await _mediator.Send(query, cancellationToken);
			return View(listings);

		}


		public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
		{
			var query = new GetListingDetailQuery { Id = id };
			var listing = await _mediator.Send(query, cancellationToken);

			if (listing == null)
			{
				return NotFound(); 
			}

			return View(listing);
		}

		[HttpPost]
		[Authorize(Roles = "Student")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Apply([FromForm] ApplyToListingCommand command, CancellationToken cancellationToken)
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			command.StudentId = Guid.Parse(userIdString);

			var result = await _mediator.Send(command, cancellationToken);

			return Json(new { success = result.Success, message = result.Message });
		}



	}
}
