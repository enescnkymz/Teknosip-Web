using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;

namespace Teknosip.Application.Features.CompanyProfiles.Queries.GetCompanyProfile
{
	public class GetCompanyProfileQuery : IRequest<CompanyProfileDto>
	{
		public Guid UserId { get; set; }

		public GetCompanyProfileQuery(Guid userId)
		{
			UserId = userId;
		}
	}

}
