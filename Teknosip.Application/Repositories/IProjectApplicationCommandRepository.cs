using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IProjectApplicationCommandRepository
	{

		Task<bool> HasAlreadyAppliedAsync(int projectId, Guid userId, CancellationToken cancellationToken);
		Task AddAsync(ProjectApplication entity, CancellationToken cancellationToken);
		Task SaveChangesAsync(CancellationToken cancellationToken);
		Task<ProjectApplication?> GetApplicationWithProjectAsync(int applicationId, CancellationToken cancellationToken);

	}

}
