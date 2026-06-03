using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.InstitutionProfile.Queries.GetApprovedInstitutions
{
	public class GetApprovedInstitutionsQueryHandler : IRequestHandler<GetApprovedInstitutionsQuery, IEnumerable<PublicInstitutionDto>>
	{
		private readonly IInstitutionQueryRepository _repository;

		public GetApprovedInstitutionsQueryHandler(IInstitutionQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<PublicInstitutionDto>> Handle(GetApprovedInstitutionsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetApprovedInstitutionsAsync(cancellationToken);
		}
	}
}
