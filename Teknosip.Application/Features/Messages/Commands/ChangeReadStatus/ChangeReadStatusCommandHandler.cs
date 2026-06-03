using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Messages.Commands.ChangeReadStatus
{
	public class ChangeReadStatusCommandHandler : IRequestHandler<ChangeReadStatusCommand, bool>
	{

		private readonly IMessageCommandRepository _repository;

		public ChangeReadStatusCommandHandler(IMessageCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(ChangeReadStatusCommand request, CancellationToken cancellationToken)
		{

			await _repository.ChangeReadStatusAsync(request.MessageId, request.MarkAsRead, cancellationToken);
			return true;


		}
	
	
	
	}
}
