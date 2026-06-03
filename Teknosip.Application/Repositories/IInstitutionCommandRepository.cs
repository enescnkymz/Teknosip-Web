using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IInstitutionCommandRepository
	{
		Task AddAsync(InstitutionProfile profile, CancellationToken cancellationToken);
		Task SaveAsync(CancellationToken cancellationToken);
		void UpdateInstitutionProfile(InstitutionProfile entity, CancellationToken cancellationToken = default);
		Task<InstitutionProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

	}
}
