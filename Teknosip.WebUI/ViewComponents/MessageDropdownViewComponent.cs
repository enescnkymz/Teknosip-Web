using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Features.Messages.Queries.GetTop3Messages;

namespace Teknosip.WebUI.ViewComponents
{
	public class MessageDropdownViewComponent : ViewComponent
	{

		private readonly IMediator _mediator;

		public MessageDropdownViewComponent(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return View(new MessageDropwdownDto());
			}
			var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var query = new GetTop3MessagesQuery
				{
					UserId = userId
				};

				var model = await _mediator.Send(query);
				return View(model);
			}

			return View(new MessageDropwdownDto());
		}

	}
}
