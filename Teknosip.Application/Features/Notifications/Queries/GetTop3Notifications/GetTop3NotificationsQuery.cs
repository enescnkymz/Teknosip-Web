using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;

namespace Teknosip.Application.Features.Notifications.Queries.GetTop3Notifications
{
	public class GetTop3NotificationsQuery : IRequest<NotificationDropdownDto>
	{
		public Guid UserId { get; set; }
		public string AreaName { get; set; }

	}
}
