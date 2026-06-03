using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;

namespace Teknosip.Application.Repositories
{
	public interface IProjectQueryRepository
	{

		Task<IEnumerable<CompanyListingDto>> GetListingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken);
		Task<IEnumerable<PublicListingDto>> GetAllActiveListingsAsync(CancellationToken cancellationToken);
		Task<PublicListingDetailDto?> GetListingDetailAsync(int id, CancellationToken cancellationToken);

	}
}
