using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Messages.Queries.GetSentMessages
{
	public class GetSentMessagesQueryHandler : IRequestHandler<GetSentMessagesQuery , PaginatedSentMessagesDto>	
	{

		private readonly IMessageQueryRepository _repository;

		public GetSentMessagesQueryHandler(IMessageQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedSentMessagesDto> Handle(GetSentMessagesQuery request, CancellationToken cancellationToken)
		{
			var result = await _repository.GetPaginatedSentMessagesByUserIdAsync(request.UserId, request.PageNumber, request.PageSize);

			int totalPages = 0;
			if (result.TotalCount > 0)
			{
				totalPages = (int)Math.Ceiling(result.TotalCount / (double)request.PageSize);
			}

			return new PaginatedSentMessagesDto
			{
				Messages = result.Messages?.ToList() ?? new List<SentMessageItemDto>(),
				CurrentPage = request.PageNumber,
				TotalPages = totalPages
			};
		}
	}
}
