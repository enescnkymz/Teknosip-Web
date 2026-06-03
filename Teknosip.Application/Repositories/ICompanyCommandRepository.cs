using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface ICompanyCommandRepository
	{
		Task AddAsync(CompanyProfile companyProfile, CancellationToken cancellationToken);
		Task SaveAsync(CancellationToken cancellationToken);
		void UpdateCompanyProfile(CompanyProfile entity);
		Task<CompanyProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

	}
}
