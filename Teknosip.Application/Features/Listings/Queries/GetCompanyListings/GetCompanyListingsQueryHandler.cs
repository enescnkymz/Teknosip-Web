using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Listings.Queries.GetCompanyListings
{
	public class GetCompanyListingsQueryHandler : IRequestHandler<GetCompanyListingsQuery, IEnumerable<CompanyListingDto>>
	{
		private readonly IProjectQueryRepository _repository;
		public GetCompanyListingsQueryHandler(IProjectQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<CompanyListingDto>> Handle(GetCompanyListingsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetListingsByCompanyIdAsync(request.CompanyId, cancellationToken);
		}


	}
}
