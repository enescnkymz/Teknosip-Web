using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.StudentProfile.Commands.UpdateStudentProfile;
using Teknosip.Application.Features.StudentProfile.Queries.GetStudentProfile;
using Teknosip.Domain.Entities;
using Teknosip.WebUI.Areas.Student.Models;

namespace Teknosip.WebUI.Areas.Student.Controllers
{
	public class ProfileController : StudentBaseController
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

			// MediatR Query (Dapper ile çektiğini varsayıyoruz)
			var query = new GetStudentProfileQuery(userId);
			var dto = await _mediator.Send(query);

			var model = new StudentProfileEditVM
			{
				FullName = dto.FullName,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				University = dto.University,
				Department = dto.Department,
				StudentNumber = dto.StudentNumber,
				Grade = dto.Grade,
				About = dto.About,
				CurrentPhotoUrl = dto.CurrentPhotoUrl
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(StudentProfileEditVM model)
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

			var command = new UpdateStudentProfileCommand
			{
				UserId = userId,
				FullName = model.FullName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				University = model.University,
				Department = model.Department,
				StudentNumber = model.StudentNumber,
				Grade = model.Grade,
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

				TempData["SuccessMessage"] = "Öğrenci profil bilgileriniz başarıyla güncellendi.";
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
