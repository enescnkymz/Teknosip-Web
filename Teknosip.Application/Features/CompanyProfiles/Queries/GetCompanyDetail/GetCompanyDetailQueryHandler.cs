using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.CompanyProfiles.Queries.GetCompanyDetail
{
	public class GetCompanyDetailQueryHandler : IRequestHandler<GetCompanyDetailQuery, CompanyDetailDto?>
	{
		private readonly ICompanyQueryRepository _repository;

		public GetCompanyDetailQueryHandler(ICompanyQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<CompanyDetailDto?> Handle(GetCompanyDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetCompanyDetailByIdAsync(request.Id, cancellationToken);
		}
	}
}
