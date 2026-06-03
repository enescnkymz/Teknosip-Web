using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;

namespace Teknosip.Application.Repositories
{
	public interface IAdminQueryRepository
	{
		Task<AdminProfileDto> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken);
	}
}
