using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead
{
	public class MarkAllNotificationsAsReadCommand : IRequest<bool>
	{

		public Guid UserId { get; set; }

	}
}
