using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.InstitutionProfile.Command.UpdateInstitutionProfile;
using Teknosip.Application.Features.InstitutionProfile.Queries.GetInstitutionProfile;
using Teknosip.Domain.Entities;
using Teknosip.WebUI.Areas.Institution.Models;

namespace Teknosip.WebUI.Areas.Institution.Controllers
{
	public class ProfileController : InstitutionBaseController // Base controller'ını kendi adına göre düzelt
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

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = Guid.Parse(userIdString);

			// Kendi yazdığın/yazacağın Query sınıfı
			var query = new GetInstitutionProfileQuery(userId);
			var dto = await _mediator.Send(query, cancellationToken);

			var model = new InstitutionProfileEditVM
			{
				
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				InstitutionName = dto.InstitutionName,
				City = dto.City,
				Website = dto.Website,
				About = dto.About,
				CurrentPhotoUrl = dto.CurrentPhotoUrl
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(InstitutionProfileEditVM model)
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

			var command = new UpdateInstitutionProfileCommand
			{
				UserId = userId,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				InstitutionName = model.InstitutionName,
				City = model.City,
				Website = model.Website,
				About = model.About,
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

				TempData["SuccessMessage"] = "Kurum profil bilgileriniz başarıyla güncellendi.";
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
