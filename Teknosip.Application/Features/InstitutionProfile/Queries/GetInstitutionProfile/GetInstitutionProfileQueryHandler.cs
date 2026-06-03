using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.InstitutionProfile.Queries.GetInstitutionProfile
{
	public class GetInstitutionProfileQueryHandler : IRequestHandler<GetInstitutionProfileQuery, InstitutionProfileDto>
	{
		private readonly IInstitutionQueryRepository _repository;

		public GetInstitutionProfileQueryHandler(IInstitutionQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<InstitutionProfileDto> Handle(GetInstitutionProfileQuery request, CancellationToken cancellationToken)
		{
			
			var profile = await _repository.GetProfileByIdAsync(request.UserId, cancellationToken);

			
			if (profile == null)
			{
				return new InstitutionProfileDto();
			}

			return profile;
		}
	}
}
