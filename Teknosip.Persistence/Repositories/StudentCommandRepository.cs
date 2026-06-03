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
	public class StudentCommandRepository : IStudentCommandRepository
	{
		private readonly TeknosipDbContext _context;

		public StudentCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(StudentProfile entity, CancellationToken cancellationToken = default)
		{
			await _context.AddAsync(entity, cancellationToken);
		}

		public async Task SaveAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}

		public void UpdateStudentProfile(StudentProfile entity)
		{
			_context.Update(entity);
		}

		public async Task<StudentProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
		{
			return await _context.StudentProfiles.FirstOrDefaultAsync(s => s.AppUserId == userId, cancellationToken);
		}


	}
}
