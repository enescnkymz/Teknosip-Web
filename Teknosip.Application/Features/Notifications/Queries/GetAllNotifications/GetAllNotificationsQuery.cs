using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;

namespace Teknosip.Application.Features.Notifications.Queries.GetAllNotifications
{
	public class GetAllNotificationsQuery : IRequest<List<NotificationItemDto>>
	{
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } =1;
        public int PageSize { get; set; } = 10;
    }
}
