using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Notifications.Queries.GetTop3Notifications
{
	public class GetTop3NotificationsQueryHandler : IRequestHandler<GetTop3NotificationsQuery, NotificationDropdownDto>
	{
		private readonly INotificationQueryRepository _notificationRepository;

		public GetTop3NotificationsQueryHandler(INotificationQueryRepository notificationRepository)
		{
			_notificationRepository = notificationRepository;
		}

		public async Task<NotificationDropdownDto> Handle(GetTop3NotificationsQuery request, CancellationToken cancellationToken)
		{
			
			int unreadCount = await _notificationRepository.GetUnreadCountAsync(request.UserId);
			var topNotifications = await _notificationRepository.GetRecentNotificationsAsync(request.UserId, 3);

			
			return new NotificationDropdownDto
			{
				UnreadCount = unreadCount,
				AreaName = request.AreaName,
				Notifications = topNotifications.ToList()
			};
		}
	}
}
