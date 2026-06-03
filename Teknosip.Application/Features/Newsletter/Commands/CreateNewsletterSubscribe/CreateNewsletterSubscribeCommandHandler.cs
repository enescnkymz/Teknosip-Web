using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Newsletter.Commands.CreateNewsletterSubscribe
{
	public class CreateNewsletterSubscribeCommandHandler : IRequestHandler<CreateNewsletterSubscribeCommand, bool>
	{
		INewsletterCommandRepository _repository;

		public CreateNewsletterSubscribeCommandHandler(INewsletterCommandRepository repository)
		{
			_repository = repository;
		}

		public async Task<bool> Handle(CreateNewsletterSubscribeCommand request, CancellationToken cancellationToken)
		{
			var entity = new NewsletterSubscription
			{

				Email = request.Email,
								
			};

			if (await _repository.IsEmailRegisteredAsync(entity.Email, cancellationToken))
			return false;

			await _repository.AddAsync(entity,cancellationToken);
			await _repository.SaveChangesAsync(cancellationToken);

			return true;

		}
	}
}
