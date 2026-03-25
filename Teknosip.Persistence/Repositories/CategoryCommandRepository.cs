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
	public class CategoryCommandRepository : ICategoryCommandRepository
	{
		TeknosipDbContext _context;

		public CategoryCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(Category category)
		{
			 await _context.Categories.AddAsync(category);	
		}

		public async Task<int> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync();
			
		}
	}
}
