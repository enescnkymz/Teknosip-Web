using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Notifications.Commands.ChangeReadStatus
{
	public class ChangeReadStatusCommand : IRequest<bool>
	{
		public int NotificationId { get; set; }
		public bool MarkAsRead { get; set; }

	}
}
