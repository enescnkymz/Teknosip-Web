using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;
using System.Security.Claims;
using Teknosip.Application.Features.Messages.Commands.ChangeReadStatus;
using Teknosip.Application.Features.Messages.Commands.MarkAllMessagesAsRead;
using Teknosip.Application.Features.Messages.Commands.SendMessage;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Features.Messages.Queries.GetAllMessages;
using Teknosip.Application.Features.Messages.Queries.GetSentMessages;
using Teknosip.Application.Features.Users.Queries.SearchUsers;

namespace Teknosip.WebUI.Controllers
{
	[Authorize]
	public class MessageController : Controller
	{
		private readonly IMediator _mediator;

		public MessageController(IMediator mediator)
		{
			_mediator = mediator;
		}

		
		[HttpGet]
		public async Task<IActionResult> Index(int page = 1)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return RedirectToAction("Login", "Auth");
			}

            var userRole = User.FindFirstValue(ClaimTypes.Role);
			
			if (string.IsNullOrEmpty(userRole))
			{
				return Forbid();
			} 

            								
			ViewBag.LayoutPath = $"~/Areas/{userRole}/Views/Shared/_Layout.cshtml";
			int pageSize = 10; // Her sayfada 10 mesaj gösterelim

			// CQRS Query'ni buraya göre güncellersin
			var query = new GetAllMessagesQuery { UserId = userId, PageNumber = page, PageSize = pageSize };
			PaginatedMessagesDto model = await _mediator.Send(query);

			return View(model);
		}


		[HttpPost]
		public async Task<IActionResult> MarkAllAsRead()
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return Json(new { success = false, message = "Kullanıcı bulunamadı." });
			}

			var command = new MarkAllMessagesAsReadCommand{ UserId = userId };
			await _mediator.Send(command);

			
			return Json(new { success = true });
		}


		[HttpPost]
		public async Task<IActionResult> ChangeReadStatus(int id , bool markAsRead)
		{
			
			var command = new ChangeReadStatusCommand { MessageId=id , MarkAsRead=markAsRead };
			var result = await _mediator.Send(command);
			return Json(new { success = true });


		}


		[HttpGet]
		public IActionResult Send()
		{
			var userRole = User.FindFirstValue(ClaimTypes.Role);

			if (string.IsNullOrEmpty(userRole))
			{
				return Forbid();
			}


			ViewBag.LayoutPath = $"~/Areas/{userRole}/Views/Shared/_Layout.cshtml";
			
			return View(new SendMessageCommand());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Send(SendMessageCommand model)
		{
			var userRole = User.FindFirstValue(ClaimTypes.Role);

			if (string.IsNullOrEmpty(userRole))
			{
				return Forbid();
			}


			ViewBag.LayoutPath = $"~/Areas/{userRole}/Views/Shared/_Layout.cshtml";

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var senderIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (!Guid.TryParse(senderIdString, out Guid senderId))
			{
				return RedirectToAction("Login", "Auth");
			}

			var command = new SendMessageCommand
			{
				SenderId = senderId,
				ReceiverId = model.ReceiverId,
				Content = model.Content
			};

			var result = await _mediator.Send(command);
			if (result)
			{
				TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi.";
			}
			return RedirectToAction("Send"); 
		}

		
		[HttpGet]
		public async Task<IActionResult> SearchUsers(string term)
		{
			if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
			{
				return Json(new List<object>());
			}
			
			// 2 harften azsa boş dön (Performans için)

			var query = new SearchUsersQuery { SearchTerm = term };
			var users = await _mediator.Send(query);

			// RoleName yerine UserType.ToString() kullanıyoruz
			var results = users.Select(u => new
			{
				id = u.Id,
				text = $"{u.FullName} ({u.UserType.ToString()})"
			});

			return Json(new { results = results });
		
		}


		[HttpGet]
		public async Task<IActionResult> Sent(int page = 1)
		{

			var userRole = User.FindFirstValue(ClaimTypes.Role);

			if (string.IsNullOrEmpty(userRole))
			{
				return Forbid();
			}

			ViewBag.LayoutPath = $"~/Areas/{userRole}/Views/Shared/_Layout.cshtml";


			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!Guid.TryParse(userIdString, out Guid userId))
			{
				return RedirectToAction("Login", "Auth");
			}

			var query = new GetSentMessagesQuery
			{
				UserId = userId,
				PageNumber = page,
				PageSize = 10
			};

			var model = await _mediator.Send(query);

			return View(model);
		
		}



	}
}
