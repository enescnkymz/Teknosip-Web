using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Listings.Queries.GetActiveListings
{
	public class GetActiveListingsQueryHandler : IRequestHandler<GetActiveListingsQuery, IEnumerable<PublicListingDto>>
	{
		private readonly IProjectQueryRepository _repository;

		public GetActiveListingsQueryHandler(IProjectQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<PublicListingDto>> Handle(GetActiveListingsQuery request, CancellationToken cancellationToken)
		{
			
			return await _repository.GetAllActiveListingsAsync(cancellationToken);
	
		}



	}
}
