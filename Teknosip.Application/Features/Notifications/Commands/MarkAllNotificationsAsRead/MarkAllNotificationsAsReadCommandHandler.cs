using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead
{
	public class MarkAllNotificationsAsReadCommandHandler : IRequestHandler<MarkAllNotificationsAsReadCommand, bool>
	{

		private readonly INotificationCommandRepository _repository;
	
		public MarkAllNotificationsAsReadCommandHandler(INotificationCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
		{

			await _repository.MarkAllAsReadAsync(request.UserId, cancellationToken);
			return true;

		}
	}
}
