using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Repositories
{
	public interface ICompanyQueryRepository
	{

		Task<CompanyProfileDto?> GetCompanyProfileByUserIdAsync(Guid userId);
		Task<IEnumerable<CompanySummaryDto>> GetAllApprovedCompaniesAsync(CancellationToken cancellationToken);
		Task<CompanyDetailDto?> GetCompanyDetailByIdAsync(Guid id, CancellationToken cancellationToken);
		

	}
}
