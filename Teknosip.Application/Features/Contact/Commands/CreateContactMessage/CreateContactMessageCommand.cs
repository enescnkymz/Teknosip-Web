using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Contact.Commands.CreateContactMessage
{
	public class CreateContactMessageCommand : IRequest<int>
	{

		public string Name { get; set; }
		public string Email { get; set; }
		public string Subject { get; set; }
		public string Message { get; set; }

	}
}
