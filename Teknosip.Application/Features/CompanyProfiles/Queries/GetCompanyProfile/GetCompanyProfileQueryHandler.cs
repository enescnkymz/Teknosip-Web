using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.CompanyProfiles.Queries.GetCompanyProfile
{
	public class GetCompanyProfileQueryHandler : IRequestHandler<GetCompanyProfileQuery, CompanyProfileDto>
	{
		private readonly ICompanyQueryRepository _repository;

		public GetCompanyProfileQueryHandler(ICompanyQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<CompanyProfileDto> Handle(GetCompanyProfileQuery request, CancellationToken cancellationToken)
		{
			
			var profile = await _repository.GetCompanyProfileByUserIdAsync(request.UserId);

			
			if (profile == null)
			{
				return new CompanyProfileDto();
			}

			return profile;
		}
	}
}
