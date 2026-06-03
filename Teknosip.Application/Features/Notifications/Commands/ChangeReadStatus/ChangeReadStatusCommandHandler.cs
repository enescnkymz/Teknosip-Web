using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Notifications.Commands.ChangeReadStatus
{
	public class ChangeReadStatusCommandHandler : IRequestHandler<ChangeReadStatusCommand, bool>
	{

		private readonly INotificationCommandRepository _repository;

		public ChangeReadStatusCommandHandler(INotificationCommandRepository repository)
		{
			_repository = repository;
		}
		public async Task<bool> Handle(ChangeReadStatusCommand request, CancellationToken cancellationToken)
		{

			await _repository.ChangeReadStatusAsync(request.NotificationId, request.MarkAsRead);
			return true;

		}
	}
}
