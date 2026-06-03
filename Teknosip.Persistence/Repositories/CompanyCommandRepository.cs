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
    public class CompanyCommandRepository : ICompanyCommandRepository
    {

		private readonly TeknosipDbContext _context;

		public CompanyCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(CompanyProfile entity, CancellationToken cancellationToken)
		{
			await _context.CompanyProfiles.AddAsync(entity, cancellationToken);
		}

		public async Task SaveAsync(CancellationToken cancellationToken)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}


		public void UpdateCompanyProfile(CompanyProfile entity)
		{
			_context.Update(entity);
		}

		public async Task<CompanyProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			return await _context.CompanyProfiles.FirstOrDefaultAsync(c => c.AppUserId == userId, cancellationToken);
		}




	}
}
