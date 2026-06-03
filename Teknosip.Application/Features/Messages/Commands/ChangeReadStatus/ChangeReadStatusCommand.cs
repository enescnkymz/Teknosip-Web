using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Commands.ChangeReadStatus
{
	public class ChangeReadStatusCommand : IRequest<bool>
	{

		public int MessageId { get; set; }
		public bool MarkAsRead { get; set; }

	}
}
