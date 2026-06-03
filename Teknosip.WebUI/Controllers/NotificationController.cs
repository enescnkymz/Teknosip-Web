using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using System.Security.Claims;
using Teknosip.Application.Features.Notifications.Commands.ChangeReadStatus;
using Teknosip.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead;
using Teknosip.Application.Features.Notifications.Queries.GetAllNotifications;
using Teknosip.Application.Features.Notifications.Queries.GetTop3Notifications;

namespace Teknosip.WebUI.Controllers
{
	[Authorize]
	public class NotificationController : Controller
	{
		private readonly IMediator _mediator;

		public NotificationController(IMediator mediator)
		{
			_mediator = mediator;
		}


		[HttpGet]
		public async Task<IActionResult> Index() 
		{

			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return RedirectToAction("Login", "Auth");
			}

			// 1. Kullanıcının rolünü alıyoruz (Örn: "Academician", "Student")
			var userRole = User.FindFirstValue(ClaimTypes.Role);

			// 2. Eğer rol boş gelirse (hata durumu) veya Area isimlerin ile Rollerin 
			// birebir uyuşmuyorsa buraya ufak bir Switch-Case yazabilirsin. 
			// Ama uyuşuyorsa tek satırda Layout yolunu dinamik oluştururuz:
			string layoutPath = $"~/Areas/{userRole}/Views/Shared/_Layout.cshtml";

			// 3. View'a giyeceği elbiseyi (Layout'u) paketleyip yolluyoruz
			ViewBag.LayoutPath = layoutPath;

			var query = new GetAllNotificationsQuery { UserId = userId };
			var notifications = await _mediator.Send(query);
			return View(notifications);


		}


		[HttpPost]
		public async Task<IActionResult> MarkAllAsRead()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return Json(new { success = false, message = "Kullanıcı bulunamadı." });
			}

			var result = await _mediator.Send(new MarkAllNotificationsAsReadCommand { UserId = userId });

			return Json(new { success = result });
	
		}


		[HttpPost]
		public async Task<IActionResult> ChangeReadStatus(int id, bool markAsRead)
		{
			
			var command = new ChangeReadStatusCommand
			{
			NotificationId = id,
			MarkAsRead = markAsRead 
			
			};

			
			bool result = await _mediator.Send(command);

			if (result)
			{
				return Json(new { success = true });
			}

			return Json(new { success = false });
		}

		[HttpGet]
		public async Task<IActionResult> LoadMore(int page, int pageSize = 10)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return BadRequest();
			}
			
			var query = new GetAllNotificationsQuery { UserId = userId, PageNumber = page, PageSize = pageSize };
			var notifications = await _mediator.Send(query);

			// Eğer veri yoksa tarayıcıya boş içerik dönüyoruz
			if (notifications == null || !notifications.Any())
			{
				return Content("");
			}

			// Verileri az önce oluşturduğumuz Kısmi Görünüme (Partial View) basıp saf HTML döndürüyoruz
			return PartialView("~/Views/Shared/Partials/_NotificationItemsPartial.cshtml", notifications);
		}


	}
}
