using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IStudentCommandRepository
	{
		Task AddAsync(StudentProfile entity, CancellationToken cancellationToken = default);
		Task SaveAsync(CancellationToken cancellationToken = default);
		void UpdateStudentProfile(StudentProfile entity);
		Task<StudentProfile?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

	}
}
