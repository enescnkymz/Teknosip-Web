using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface INotificationCommandRepository
	{
		Task<bool> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken);	
		Task ChangeReadStatusAsync(int notificationId, bool markAsRead);
		Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default);
	}
}
