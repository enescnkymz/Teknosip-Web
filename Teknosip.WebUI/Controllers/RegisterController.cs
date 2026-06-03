using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.Auth.Commands;
using Teknosip.Persistence.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Teknosip.WebUI.Controllers
{
	public class RegisterController : Controller
	{
		IMediator _mediator;

		public RegisterController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public IActionResult Index()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> RegisterStudent(RegisterStudentCommand command)
		{

			if (string.IsNullOrWhiteSpace(command.FullName) ||
				string.IsNullOrWhiteSpace(command.UserName) ||
				string.IsNullOrWhiteSpace(command.PhoneNumber) ||
				string.IsNullOrWhiteSpace(command.Email) ||
				string.IsNullOrWhiteSpace(command.Password))
			{
				return Json(new { success = false, message = "Lütfen tüm zorunlu alanları eksiksiz doldurun." });
			}

			if (command.Password != command.PasswordConfirm)
			{
				return Json(new { success = false, message = "Şifreler birbiriyle uyuşmuyor!" });
			}


			if (command.Password.Length < 6)
			{
				return Json(new { success = false, message = "Şifreniz güvenlik için en az 6 karakter olmalıdır." });
			}


			var isSuccess = await _mediator.Send(command);


			if (isSuccess)
			{
				return Json(new { success = true, message = "Öğrenci kaydınız başarıyla tamamlandı! Artık giriş yapabilirsiniz." });
			}
			else
			{
				return Json(new { success = false, message = "Kayıt sırasında bir hata oluştu. E-posta veya Kullanıcı Adı zaten sistemde kayıtlı olabilir." });
			}
		}

		[HttpPost]
		public async Task<IActionResult> RegisterAcademician(RegisterAcademicianCommand command)
		{

			if (string.IsNullOrWhiteSpace(command.Title) || string.IsNullOrWhiteSpace(command.FullName) ||
			string.IsNullOrWhiteSpace(command.UserName) || string.IsNullOrWhiteSpace(command.PhoneNumber) ||
			string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
			{
				return Json(new { success = false, message = "Lütfen tüm zorunlu alanları doldurun." });
			}

			if (command.Password != command.PasswordConfirm)
				return Json(new { success = false, message = "Şifreler uyuşmuyor!" });

			if (command.Password.Length < 6)
				return Json(new { success = false, message = "Şifre en az 6 karakter olmalıdır." });

			var result = await _mediator.Send(command);

			if (result)
				return Json(new { success = true, message = "Akademisyen kaydınız tamamlandı! Giriş yapabilirsiniz." });
			else
				return Json(new { success = false, message = "Kayıt hatası. E-posta veya Kullanıcı Adı zaten kullanımda olabilir." });
		}


		[HttpPost]
		public async Task<IActionResult> RegisterInstitution(RegisterInstitutionCommand command) 
		{
			if (string.IsNullOrWhiteSpace(command.UserName) || string.IsNullOrWhiteSpace(command.InstitutionName) ||
		     string.IsNullOrWhiteSpace(command.City) || string.IsNullOrWhiteSpace(command.Email) ||
		     string.IsNullOrWhiteSpace(command.Password))
			{
				return Json(new { success = false, message = "Lütfen tüm zorunlu alanları doldurun." });
			}

			
			if (command.Password != command.PasswordConfirm)
				return Json(new { success = false, message = "Şifreler uyuşmuyor!" });

			if (command.Password.Length < 6)
				return Json(new { success = false, message = "Şifre en az 6 karakter olmalıdır." });

			//kurumsallık kontrolü
			var emailDomain = command.Email.Split('@').LastOrDefault()?.ToLower();
			var blockedDomains = new List<string> { "gmail.com", "hotmail.com", "yahoo.com", "outlook.com", "yandex.com" };

			if (emailDomain == null || blockedDomains.Contains(emailDomain))
			{
				return Json(new
				{
					success = false,
					message = "Lütfen kurumsal e-posta adresinizi giriniz."
				});
			}


			var isSuccess = await _mediator.Send(command);

			if (isSuccess)
			{
				return Json(new { success = true, message = "Kurum kaydınız başarıyla tamamlandı. Yönetici onayının ardından giriş yapabilirsiniz." });
			}
			else
			{
				return Json(new { success = false, message = "Kayıt başarısız. Kullanıcı Adı veya E-posta sistemde zaten kayıtlı olabilir." });
			}
		}

		[HttpPost]
		public async Task<IActionResult> RegisterCompany(RegisterCompanyCommand command)
		{
		
			if (string.IsNullOrWhiteSpace(command.UserName) || string.IsNullOrWhiteSpace(command.CompanyName) ||
				string.IsNullOrWhiteSpace(command.City) || string.IsNullOrWhiteSpace(command.TaxNumber) ||
				string.IsNullOrWhiteSpace(command.Email) || string.IsNullOrWhiteSpace(command.Password))
			{
				return Json(new { success = false, message = "Lütfen tüm zorunlu alanları doldurun." });
			}

			
			if (command.Password != command.PasswordConfirm)
				return Json(new { success = false, message = "Şifreler uyuşmuyor!" });

			if (command.Password.Length < 6)
				return Json(new { success = false, message = "Şifre en az 6 karakter olmalıdır." });

			
			var isSuccess = await _mediator.Send(command);

			if (isSuccess)
			{				
				return Json(new { success = true, message = "Şirket kaydınız başarıyla alındı. Yönetici onayının ardından giriş yapabilirsiniz." });
			}
			else
			{
				return Json(new { success = false, message = "Kayıt işlemi başarısız. Kullanıcı Adı veya E-posta sistemde zaten kayıtlı olabilir." });
			}
		}



    }
}
