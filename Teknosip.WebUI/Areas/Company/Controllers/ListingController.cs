using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Listings.Commands.CreateListing;
using Teknosip.Application.Features.Listings.Commands.DeleteListing;
using Teknosip.Application.Features.Listings.Queries.GetCompanyListings;
using Teknosip.WebUI.Areas.Company.Models;

namespace Teknosip.WebUI.Areas.Company.Controllers
{
	public class ListingController : CompanyBaseController
	{
		private readonly IMediator _mediator;
	
		public ListingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateListingVM model)
		{
			if (!ModelState.IsValid)
			{

				return View(model);

			}

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var command = new CreateListingCommand
			{
				PublishedById = Guid.Parse(userIdString),
				Title = model.Title,
				Description = model.Description,
				CategoryType = model.CategoryType,
				ListingType = model.ListingType,
				WorkType = model.WorkType,
				SalaryOrBudget = model.SalaryOrBudget,
				Deadline = model.Deadline,
				ImageStream = model.ImageFile.OpenReadStream(),
				ImageFileName = model.ImageFile.FileName
			};

			var result = await _mediator.Send(command);

			if (result)
			{
				TempData["SuccessMessage"] = "İlanınız başarıyla oluşturuldu ve yayına alındı!";
				return RedirectToAction("Index", "Listing"); 
			}

			ModelState.AddModelError("", "İlan oluşturulurken bir hata meydana geldi.");
			return View(model);

		}

		[HttpGet]
		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var query = new GetCompanyListingsQuery { CompanyId = Guid.Parse(userIdString) };
			var listings = await _mediator.Send(query, cancellationToken);

			return View(listings);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var command = new DeleteListingCommand
			{
				Id = id,
				CompanyId = Guid.Parse(userIdString)
			};

			var result = await _mediator.Send(command, cancellationToken);

			if (result)
			{
				TempData["SuccessMessage"] = "İlanınız başarıyla silindi.";
			}
			else
			{
				TempData["ErrorMessage"] = "İlan silinirken bir hata oluştu veya yetkiniz yok.";
			}

			return RedirectToAction("Index");
		}























	}
}
