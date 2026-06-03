using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.CompanyProfiles.Queries.GetApprovedCompanies
{
	public class GetApprovedCompaniesQueryHandler : IRequestHandler<GetApprovedCompaniesQuery, IEnumerable<CompanySummaryDto>>
	{
		private readonly ICompanyQueryRepository _repository;

		public GetApprovedCompaniesQueryHandler(ICompanyQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<CompanySummaryDto>> Handle(GetApprovedCompaniesQuery request, CancellationToken cancellationToken)
		{
			
			return await _repository.GetAllApprovedCompaniesAsync(cancellationToken);
		}
	}

}
