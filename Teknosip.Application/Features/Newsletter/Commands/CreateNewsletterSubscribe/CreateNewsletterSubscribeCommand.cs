using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Newsletter.Commands.CreateNewsletterSubscribe
{
	public class CreateNewsletterSubscribeCommand : IRequest<bool>
	{
		public string Email { get; set; }
	}
}
