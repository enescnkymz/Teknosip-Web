using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Teknosip.Application.Features.Applications.Commands.EvaluateApplication;
using Teknosip.Application.Features.Applications.Queries.GetProjectApplications;
using Teknosip.Domain.Entities;

namespace Teknosip.WebUI.Areas.Company.Controllers
{
	public class ApplicationsController : CompanyBaseController
	{
		private readonly IMediator _mediator;

		public ApplicationsController(IMediator mediator)
		{
			_mediator = mediator;
		}


		public async Task<IActionResult> Index(int projectId, string title, CancellationToken cancellationToken)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var query = new GetProjectApplicationsQuery
			{
				ProjectId = projectId,
				CompanyId = Guid.Parse(userIdString)
			};

			var applications = await _mediator.Send(query, cancellationToken);

			ViewBag.ProjectId = projectId;

			// SİHİRLİ DOKUNUŞ: Başlığı View'a yolluyoruz
			ViewBag.ProjectTitle = title;

			return View(applications);
		}

		// AJAX: Başvuruyu Kabul veya Reddet
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Evaluate([FromForm] int applicationId, [FromForm] ApplicationStatus status, [FromForm] string? rejectionReason)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

			var command = new EvaluateApplicationCommand
			{
				ApplicationId = applicationId,
				CompanyId = Guid.Parse(userIdString),
				Status = status,
				RejectionReason = rejectionReason
			};

			var result = await _mediator.Send(command);

			if (result)
				return Json(new { success = true, message = "Değerlendirme işlemi başarıyla kaydedildi." });
			else
				return Json(new { success = false, message = "Bir hata oluştu veya bu işlem için yetkiniz yok." });
		}
	}
}
