using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Applications.Commands.EvaluateApplication
{
	public class EvaluateApplicationCommandHandler : IRequestHandler<EvaluateApplicationCommand, bool>
	{
		private readonly IProjectApplicationCommandRepository _repository;

		public EvaluateApplicationCommandHandler(IProjectApplicationCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(EvaluateApplicationCommand request, CancellationToken cancellationToken)
		{
			var application = await _repository.GetApplicationWithProjectAsync(request.ApplicationId, cancellationToken);


			if (application == null || application.Project.PublishedById != request.CompanyId)
			{
				return false;
			}
			
			application.Status = request.Status;
			application.RejectionReason = request.RejectionReason;
			application.ReviewedAt = DateTime.UtcNow;

			await _repository.SaveChangesAsync(cancellationToken);
			return true;
	
		}
	}
}
