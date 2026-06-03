using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;

namespace Teknosip.Application.Repositories
{
	public interface INotificationQueryRepository
	{
		
		Task<int> GetUnreadCountAsync(Guid userId);
		Task<IEnumerable<NotificationItemDto>> GetRecentNotificationsAsync(Guid userId, int count = 3);
		Task<IEnumerable<NotificationItemDto>> GetAllByUserIdAsync(Guid userId);
		Task<IEnumerable<NotificationItemDto>> GetPaginatedNotificationsByUserIdAsync(Guid userId, int pageNumber, int pageSize);

	}
}
