using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.InstitutionProfile.Queries.GetInstitutionDetail
{
	public class GetInstitutionDetailQueryHandler : IRequestHandler<GetInstitutionDetailQuery, PublicInstitutionDetailDto?>
	{
		private readonly IInstitutionQueryRepository _repository;

		public GetInstitutionDetailQueryHandler(IInstitutionQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<PublicInstitutionDetailDto?> Handle(GetInstitutionDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetInstitutionDetailAsync(request.Id, cancellationToken);
		}
	}

}
