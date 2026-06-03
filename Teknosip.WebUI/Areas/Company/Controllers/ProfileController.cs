using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.CompanyProfiles.Commands.UpdateCompanyProfile;
using Teknosip.Application.Features.CompanyProfiles.Queries.GetCompanyProfile;
using Teknosip.Domain.Entities;
using Teknosip.WebUI.Areas.Company.Models;

namespace Teknosip.WebUI.Areas.Company.Controllers
{
	public class ProfileController : CompanyBaseController
	{

		private readonly IMediator _mediator;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;

		public ProfileController(IMediator mediator, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_mediator = mediator;
			_signInManager = signInManager;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = Guid.Parse(userIdString);

			// MediatR'dan şirketin mevcut bilgilerini çektiğin query (Yazdığını varsayarak çağırıyorum)
			var query = new GetCompanyProfileQuery(userId);
			var dto = await _mediator.Send(query);

			var model = new CompanyProfileEditVM
			{			
				CompanyName = dto.CompanyName,
				TaxNumber = dto.TaxNumber,
				Sector = dto.Sector,
				FoundedYear = dto.FoundedYear,
				EmployeeCount = dto.EmployeeCount,
				City = dto.City,
				Address = dto.Address,
				Website = dto.Website,
				About = dto.About,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				CurrentPhotoUrl = dto.CurrentPhotoUrl
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(CompanyProfileEditVM model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = Guid.Parse(userIdString);

			Stream photoStream = null;
			string photoFileName = null;

			if (model.NewProfilePhoto != null && model.NewProfilePhoto.Length > 0)
			{
				photoStream = model.NewProfilePhoto.OpenReadStream();
				photoFileName = model.NewProfilePhoto.FileName;
			}

			var command = new UpdateCompanyProfileCommand
			{
				UserId = userId,
				CompanyName = model.CompanyName,
				TaxNumber = model.TaxNumber,
				Sector = model.Sector,
				FoundedYear = model.FoundedYear,
				EmployeeCount = model.EmployeeCount,
				City = model.City,
				Address = model.Address,
				Website = model.Website,
				About = model.About,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				CurrentPassword = model.CurrentPassword,
				NewPassword = model.NewPassword,
				PhotoStream = photoStream,
				PhotoFileName = photoFileName
			};

			var result = await _mediator.Send(command);

			if (result.IsSuccess)
			{
				var currentUser = await _userManager.FindByIdAsync(userIdString);
				if (currentUser != null)
				{
					await _signInManager.RefreshSignInAsync(currentUser);
				}

				TempData["SuccessMessage"] = "Şirket profil bilgileriniz başarıyla güncellendi.";
				return RedirectToAction("Index");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error);
			}

			return View(model);
		}



	}
}
