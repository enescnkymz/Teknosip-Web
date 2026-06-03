using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Notifications.Queries.GetAllNotifications
{
	public class GetAllNotificationsQueryHandler : IRequestHandler<GetAllNotificationsQuery, List<NotificationItemDto>>
	{

		private readonly INotificationQueryRepository _repository;

		public GetAllNotificationsQueryHandler(INotificationQueryRepository repository)
		{
			_repository = repository;
		}
		public async Task<List<NotificationItemDto>> Handle(GetAllNotificationsQuery request, CancellationToken cancellationToken)
		{
			
			var notifications = await _repository.GetPaginatedNotificationsByUserIdAsync(request.UserId , request.PageNumber , request.PageSize);

			// 2. IEnumerable gelen veriyi View'in beklediği List formatına çevirip (null kontrolü ile) döndür
			return notifications?.ToList() ?? new List<NotificationItemDto>();

		}
	}
}
