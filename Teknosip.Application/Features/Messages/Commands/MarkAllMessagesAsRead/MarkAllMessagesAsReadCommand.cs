using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Commands.MarkAllMessagesAsRead
{
	public class MarkAllMessagesAsReadCommand : IRequest<bool>
	{
		public Guid UserId { get; set; }

	}
}
