using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Messages.Commands.MarkAllMessagesAsRead
{
	public class MarkAllMessagesAsReadCommandHandler : IRequestHandler<MarkAllMessagesAsReadCommand , bool>
	{
		private readonly IMessageCommandRepository _repository;

		public MarkAllMessagesAsReadCommandHandler(IMessageCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(MarkAllMessagesAsReadCommand request, CancellationToken cancellationToken)
		{
			await _repository.MarkAllAsReadAsync(request.UserId);
			return true; 
		}
	}
}
