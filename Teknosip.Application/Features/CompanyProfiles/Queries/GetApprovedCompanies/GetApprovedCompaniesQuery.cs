using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;

namespace Teknosip.Application.Features.CompanyProfiles.Queries.GetApprovedCompanies
{
	public class GetApprovedCompaniesQuery : IRequest<IEnumerable<CompanySummaryDto>>
	{
	}


}
