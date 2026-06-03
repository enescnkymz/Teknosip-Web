using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Notifications.Models;
using Teknosip.Application.Features.Notifications.Queries.GetTop3Notifications;

namespace Teknosip.WebUI.ViewComponents
{
	public class NotificationDropdownViewComponent : ViewComponent
	{

		private readonly IMediator _mediator;

		public NotificationDropdownViewComponent(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			if (!User.Identity.IsAuthenticated)
			{ 
				return View(new NotificationDropdownDto()); 
			}

			var userIdString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var userRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value ?? "Academician";

			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var query = new GetTop3NotificationsQuery
				{
					UserId = userId,
					AreaName = userRole
				};

				var model = await _mediator.Send(query);
				return View(model);
			}

			return View(new NotificationDropdownDto());
		}

	}
}
