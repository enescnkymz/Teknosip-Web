using MediatR;
using Microsoft.AspNetCore.Mvc;
using Teknosip.Application.Features.Users.Commands.ApproveUser;
using Teknosip.Application.Features.Users.Queries.GetPendingApprovals;
using Teknosip.Domain.Entities;

namespace Teknosip.WebUI.Areas.Admin.Controllers
{
	public class ApprovalsController : AdminBaseController
	{
		private readonly IMediator _mediator;

		public ApprovalsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<IActionResult> Index(CancellationToken cancellationToken)
		{
			var query = new GetPendingApprovalsQuery();
			var pendingUsers = await _mediator.Send(query, cancellationToken);
			return View(pendingUsers);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Approve(Guid userId, UserType userType, CancellationToken cancellationToken)
		{
			var command = new ApproveUserCommand { UserId = userId, UserType = userType };
			var result = await _mediator.Send(command, cancellationToken);

			if (result)
			{
				TempData["SuccessMessage"] = "Kullanıcı başarıyla onaylandı ve sisteme erişimi açıldı.";
			}
			else
			{
				TempData["ErrorMessage"] = "Onaylama işlemi sırasında bir hata oluştu.";
			}

			return RedirectToAction("Index");
		}
	}
}
