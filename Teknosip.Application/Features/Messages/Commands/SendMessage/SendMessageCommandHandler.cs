using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Messages.Commands.SendMessage
{
	public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, bool>
	{
		private readonly IMessageCommandRepository _repository;

		public SendMessageCommandHandler(IMessageCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(SendMessageCommand request, CancellationToken cancellationToken)
		{
			var message = new Message
			{
				SenderId = request.SenderId,
				ReceiverId = request.ReceiverId,
				Content = request.Content,
				CreatedAt = DateTime.UtcNow,
				IsRead = false
			};

			await _repository.AddMessageAsync(message , cancellationToken);
			await _repository.SaveChangesAsync(cancellationToken);
			return true;
		}

	}
}
