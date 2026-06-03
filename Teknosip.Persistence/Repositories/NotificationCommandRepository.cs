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
    public class NotificationCommandRepository : INotificationCommandRepository
    {
		private readonly TeknosipDbContext _context; 

		public NotificationCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task<bool> MarkAllAsReadAsync(Guid userId, CancellationToken cancellationToken)
		{
			

			await _context.Notifications
				.Where(n => n.UserId == userId && !n.IsRead)
				.ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, true), cancellationToken);

			return true;

		}

		public async Task ChangeReadStatusAsync(int notificationId, bool markAsRead)
		{
			
			await _context.Notifications
				.Where(n => n.Id == notificationId)
				.ExecuteUpdateAsync(s => s.SetProperty(n => n.IsRead, markAsRead));
		}

		public async Task AddNotificationAsync(Notification notification, CancellationToken cancellationToken = default)
		{
			await _context.Notifications.AddAsync(notification, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

	}
}
