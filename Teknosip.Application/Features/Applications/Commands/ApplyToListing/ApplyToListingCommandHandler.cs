using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Applications.Commands.ApplyToListing
{
	public class ApplyToListingCommandHandler : IRequestHandler<ApplyToListingCommand, ApplyToListingResult>
	{
		private readonly IProjectApplicationCommandRepository _repository;

		public ApplyToListingCommandHandler(IProjectApplicationCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<ApplyToListingResult> Handle(ApplyToListingCommand request, CancellationToken cancellationToken)
		{
			
			bool alreadyApplied = await _repository.HasAlreadyAppliedAsync(request.ProjectId, request.StudentId, cancellationToken);

			if (alreadyApplied)
			{
				return new ApplyToListingResult { Success = false, Message = "Bu ilana zaten başvuru yaptınız." };
			}

			// 2. Başvuruyu Oluştur (Varsayılan durum: Pending)
			var application = new ProjectApplication
			{
				ProjectId = request.ProjectId,
				AppUserId = request.StudentId,
				CoverLetter = request.CoverLetter,
				Status = ApplicationStatus.Pending,
				AppliedAt = DateTime.UtcNow
			};

			// 3. Veritabanına Yaz
			await _repository.AddAsync(application, cancellationToken);
			await _repository.SaveChangesAsync(cancellationToken);

			return new ApplyToListingResult { Success = true, Message = "Başvurunuz başarıyla iletildi!" };
		}
	}
}
