using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Contact.Commands.CreateContactMessage
{
	public class CreateContactMessageCommandHandler : IRequestHandler<CreateContactMessageCommand , int>
	{

		IContactMessageCommandRepository _contactRepository;

		public CreateContactMessageCommandHandler(IContactMessageCommandRepository contactRepository)
		{
			_contactRepository = contactRepository;
		}

		public  async Task<int> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
		{

			var entity = new ContactMessage 
			{

				Name = request.Name,
				Email = request.Email,
				Subject = request.Subject,
				Message = request.Message,

			};

			await _contactRepository.AddAsync(entity);
			await _contactRepository.SaveChangesAsync();

			return entity.Id;

		}

	}
}
