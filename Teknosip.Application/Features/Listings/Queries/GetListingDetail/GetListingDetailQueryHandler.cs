using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Listings.Queries.GetListingDetail
{
	public class GetListingDetailQueryHandler : IRequestHandler<GetListingDetailQuery, PublicListingDetailDto?>
	{
		private readonly IProjectQueryRepository _repository;

		public GetListingDetailQueryHandler(IProjectQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<PublicListingDetailDto?> Handle(GetListingDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetListingDetailAsync(request.Id, cancellationToken);
		}



	}

}
