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
	public class AcademicianCommandRepository : IAcademicianCommandRepository
	{

		TeknosipDbContext _context;

		public AcademicianCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(AcademicianProfile entity, CancellationToken cancellationToken = default)
		{
			await _context.AddAsync(entity, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public void UpdateAcademicanProfile(AcademicianProfile entity, CancellationToken cancellationToken = default)
		{
			_context.Update(entity);
		}

		public async Task<AcademicianProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{

			return await _context.AcademicianProfiles.FirstOrDefaultAsync(a => a.AppUserId == userId , cancellationToken);
		}

	}
}
