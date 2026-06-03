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
	public class ProjectCommandRepository : IProjectCommandRepository
	{
		private readonly TeknosipDbContext _context;

		public ProjectCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Project entity, CancellationToken cancellationToken = default)
		{
			await _context.Projects.AddAsync(entity, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public async Task<Project?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{	
			return await _context.Projects.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
		}

		public void Delete(Project entity)
		{
			_context.Projects.Remove(entity);
		}





	}
}
