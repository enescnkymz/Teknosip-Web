using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.AdminProfile.Commands.UpdateAdminProfile;
using Teknosip.Application.Features.AdminProfile.Queries.GetAdminProfile;
using Teknosip.Domain.Entities;
using Teknosip.WebUI.Areas.Admin.Models;

namespace Teknosip.WebUI.Areas.Admin.Controllers
{
	public class ProfileController : AdminBaseController // Kendi Base Controller'ın
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

			var query = new GetAdminProfileQuery(userId);
			var dto = await _mediator.Send(query, cancellationToken);

			var model = new AdminProfileEditVM
			{
				FullName = dto.FullName,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				CurrentPhotoUrl = dto.CurrentPhotoUrl
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(AdminProfileEditVM model)
		{
			if (!ModelState.IsValid) return View(model);

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = Guid.Parse(userIdString);

			Stream photoStream = null;
			string photoFileName = null;

			if (model.NewProfilePhoto != null && model.NewProfilePhoto.Length > 0)
			{
				photoStream = model.NewProfilePhoto.OpenReadStream();
				photoFileName = model.NewProfilePhoto.FileName;
			}

			var command = new UpdateAdminProfileCommand
			{
				UserId = userId,
				FullName = model.FullName,
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
				if (currentUser != null) await _signInManager.RefreshSignInAsync(currentUser);

				TempData["SuccessMessage"] = "Yönetici profiliniz başarıyla güncellendi.";
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
