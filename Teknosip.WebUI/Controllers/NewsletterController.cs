using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.Newsletter.Commands.CreateNewsletterSubscribe;

namespace Teknosip.WebUI.Controllers
{
	public class NewsletterController : Controller
	{
		IMediator _mediator;

		public NewsletterController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Subscribe(CreateNewsletterSubscribeCommand command)
		{
			if (string.IsNullOrEmpty(command.Email))
			{
				return Json(new {success = false , message= "Lütfen geçerli bir e-posta adresi giriniz" });
			}

			var isSuccess = await _mediator.Send(command);

			if (isSuccess)
			{
				return Json(new { success = true, message = "Bültenimize başarıyla abone oldunuz!" });
			}
			else
			{
				return Json(new { success = false, message = "Bu e-posta adresi sistemimizde zaten kayıtlı." });
			}

		}
	}
}
