using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.Categories.Commands.CreateCategory;
using Teknosip.Application.Features.Contact.Commands.CreateContactMessage;

namespace Teknosip.WebUI.Controllers
{
	public class ContactController : Controller
	{

		IMediator _mediator;

		public ContactController (IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]	
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(CreateContactMessageCommand command)
		{  
		
		  var newId = await _mediator.Send(command);
			if (newId > 0)
			{
				TempData["Message"] = $"Mesajınız başarıyla gönderilmiştir. Takip Numarası: {newId} ";	
				 return RedirectToAction("Index");
			}

		  return View(command);
		
		}

	}
}
