using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;

namespace Teknosip.Application.Features.Messages.Queries.GetSentMessages
{
	public class GetSentMessagesQuery : IRequest<PaginatedSentMessagesDto>
	{
		public Guid UserId { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 10;

	}
}
