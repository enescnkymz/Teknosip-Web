using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IAcademicianCommandRepository
	{
		Task AddAsync(AcademicianProfile entity, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken=default);
		Task<AcademicianProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
		void UpdateAcademicanProfile(AcademicianProfile entity ,CancellationToken cancellationToken=default);

	}
}
