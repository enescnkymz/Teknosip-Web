using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Users.Models;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IUserQueryRepository
	{
		Task<IEnumerable<UserSearchResultDto>> SearchUsersAsync(string searchTerm);
		Task<IEnumerable<PendingApprovalDto>> GetPendingApprovalsAsync(CancellationToken cancellationToken);
		Task<bool> IsUserApprovedAsync(Guid userId, UserType userType, CancellationToken cancellationToken);
	}
}
