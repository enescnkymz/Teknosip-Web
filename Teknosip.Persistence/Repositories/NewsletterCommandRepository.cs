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
	public class NewsletterCommandRepository : INewsletterCommandRepository
	{
		TeknosipDbContext _context;

		public NewsletterCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(NewsletterSubscription entity, CancellationToken cancellationToken = default)
		{
			await _context.AddAsync(entity , cancellationToken);

		}

		public async Task<bool> IsEmailRegisteredAsync(string email, CancellationToken cancellationToken = default)
		{
			return await _context.NewsletterSubscriptions.AnyAsync(x => x.Email == email, cancellationToken);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
