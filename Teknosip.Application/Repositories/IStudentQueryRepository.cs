using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;

namespace Teknosip.Application.Repositories
{
	public interface IStudentQueryRepository
	{
		Task<StudentProfileDto?> GetStudentProfileByUserIdAsync(Guid userId);
	}
}
