using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;

namespace Teknosip.Application.Repositories
{
	public interface IMessageQueryRepository
	{
		Task<int> GetUnreadMessageCountAsync(Guid userId);
		Task<IEnumerable<MessageItemDto>> GetRecentMessagesAsync(Guid userId, int count = 3);
		Task<(IEnumerable<MessageItemDto> Messages, int TotalCount)> GetPaginatedMessagesByUserIdAsync(Guid userId, int pageNumber, int pageSize);
		Task<(IEnumerable<SentMessageItemDto> Messages, int TotalCount)> GetPaginatedSentMessagesByUserIdAsync(Guid userId, int pageNumber, int pageSize);

	}
}
