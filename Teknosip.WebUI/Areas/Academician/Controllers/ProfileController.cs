using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.AcademicianProfile.Commands.UpdateAcademicianProfile;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Features.AcademicianProfile.Queries.GetAcademicianProfile;
using Teknosip.Domain.Entities;
using Teknosip.WebUI.Areas.Academician.Models;

namespace Teknosip.WebUI.Areas.Academician.Controllers
{
	public class ProfileController : AcademicianBaseController
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

			// 1. Claim'den gelen string ID'yi okuyoruz
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			// 2. String'i Guid tipine çeviriyoruz! (Güvenli çevirim için TryParse da kullanılabilir)
			Guid userId = Guid.Parse(userIdString);

			// 3. Guid parametresiyle MediatR'ı çağırıyoruz
			var query = new GetAcademicianProfileQuery(userId);
			AcademicianProfileDto dto = await _mediator.Send(query);

			var model = new AcademicianProfileEditVM
			{
				FullName = dto.FullName,
				Email = dto.Email,
				PhoneNumber = dto.PhoneNumber,
				Title = dto.Title,
				University = dto.University,
				Department = dto.Department,
				About = dto.About,
				CurrentPhotoUrl = dto.CurrentPhotoUrl
			};

			return View(model);

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(AcademicianProfileEditVM model)
		{

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Guid userId = Guid.Parse(userIdString);

			Stream photoStream = null;
			string photoFileName = null;

			// Eğer yeni bir fotoğraf seçildiyse IFormFile'dan Stream'i çıkar (UI bağımlılığını kes)
			if (model.NewProfilePhoto != null && model.NewProfilePhoto.Length > 0)
			{
				photoStream = model.NewProfilePhoto.OpenReadStream();
				photoFileName = model.NewProfilePhoto.FileName;
			}

			var command = new UpdateAcademicianProfileCommand
			{
				UserId = userId,
				FullName = model.FullName,
				Title = model.Title,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				University = model.University,
				Department = model.Department,
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

				TempData["SuccessMessage"] = "Profil bilgileriniz başarıyla güncellendi.";
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
