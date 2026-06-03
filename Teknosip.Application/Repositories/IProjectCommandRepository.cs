using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IProjectCommandRepository
	{
		Task AddAsync(Project entity, CancellationToken cancellationToken = default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		void Delete(Project entity);

	}

}
