using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;
using Teknosip.Persistence.Contexts;

namespace Teknosip.Persistence.Repositories
{
	public class ProjectApplicationCommandRepository : IProjectApplicationCommandRepository
	{
		private readonly TeknosipDbContext _context;

		public ProjectApplicationCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task<bool> HasAlreadyAppliedAsync(int projectId, Guid userId, CancellationToken cancellationToken)
		{
			return await _context.ProjectApplications
				.AnyAsync(x => x.ProjectId == projectId && x.AppUserId == userId, cancellationToken);
		}

		public async Task AddAsync(ProjectApplication entity, CancellationToken cancellationToken)
		{
			await _context.ProjectApplications.AddAsync(entity, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<ProjectApplication?> GetApplicationWithProjectAsync(int applicationId, CancellationToken cancellationToken)
		{
			
			return await _context.ProjectApplications
				.Include(pa => pa.Project)
				.FirstOrDefaultAsync(pa => pa.Id == applicationId, cancellationToken);
		}


	}
}
