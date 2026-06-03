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
	public class InstitutionCommandRepository : IInstitutionCommandRepository
	{
		
		private readonly TeknosipDbContext _context;

		public InstitutionCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(InstitutionProfile profile, CancellationToken cancellationToken)
		{
			await _context.InstitutionProfiles.AddAsync(profile, cancellationToken);
		}

		public async Task SaveAsync(CancellationToken cancellationToken)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public void UpdateInstitutionProfile(InstitutionProfile entity, CancellationToken cancellationToken = default)
		{
			_context.Update(entity);
		}

		public async Task<InstitutionProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			return await _context.InstitutionProfiles.FirstOrDefaultAsync(i => i.AppUserId == userId, cancellationToken);
		}



	}
}
