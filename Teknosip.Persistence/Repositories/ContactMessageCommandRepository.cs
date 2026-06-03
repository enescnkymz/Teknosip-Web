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
	public class ContactMessageCommandRepository : IContactMessageCommandRepository
	{
		TeknosipDbContext _context;

		public ContactMessageCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(ContactMessage message)
		{

			await _context.ContactMessages.AddAsync(message);

		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
