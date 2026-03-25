using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.Categories.Commands.CreateCategory;
using Teknosip.Application.Features.Categories.Queries.GetCategoryList;

namespace Teknosip.WebUI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly IMediator _mediator;
		
		public CategoryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Listele()
		{
			// Boş zarfımızı oluşturuyoruz
			var query = new GetCategoryListQuery();

			// Zarfı postacıya veriyoruz, o gidip Dapper'dan veriyi alıp bize getirecek
			var sonuc = await _mediator.Send(query);

			return Ok(sonuc);
		}

		[HttpPost]
		public async Task<IActionResult> Ekle([FromBody] CreateCategoryCommand command)
		{
			// Sihir burada başlıyor! 
			// Postmana'dan gelen "command" mektubunu alıp Send (Gönder) diyoruz.
			// MediatR arka planda gidip CreateKategoriCommandHandler sınıfını bulacak ve çalıştıracak!
			int yeniId = await _mediator.Send(command);

			return Ok(yeniId);
		}
	}
}
