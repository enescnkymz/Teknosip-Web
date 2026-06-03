using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Users.Commands.ApproveUser
{
	public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, bool>
	{
		private readonly IUserCommandRepository _repository;
		private readonly INotificationCommandRepository _notificationCommandRepository;

		public ApproveUserCommandHandler(IUserCommandRepository repository, INotificationCommandRepository notificationCommandRepository)
		{
			_repository = repository;
			_notificationCommandRepository = notificationCommandRepository;
		}

		public async Task<bool> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
		{
			var result = await _repository.ApproveUserAsync(request.UserId, request.UserType, cancellationToken);

			if (result)
			{

				var area = request.UserType;

				var notification = new Notification
				{
					UserId = request.UserId,
					Title = "Hesabınız Onaylandı! 🎉",
					Message = "Tebrikler, hesabınız sistem yöneticisi tarafından incelendi ve onaylandı. Profilinizi düzenleyebilir, fotoğrafınızı ekleyebilirsiniz.",
					Type = NotificationType.Success, 
					RedirectUrl = $"/{area}/Profile", 
					CreatedAt = DateTime.UtcNow,
					IsRead = false
				};

				await _notificationCommandRepository.AddNotificationAsync(notification, cancellationToken);

			}
			return result;
		
		}
	}
}
