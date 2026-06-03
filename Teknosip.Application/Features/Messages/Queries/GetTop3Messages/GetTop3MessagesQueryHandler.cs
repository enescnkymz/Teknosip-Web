using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Messages.Queries.GetTop3Messages
{
	public class GetTop3MessagesQueryHandler : IRequestHandler<GetTop3MessagesQuery , MessageDropwdownDto>
	{
		
		private readonly IMessageQueryRepository _repository;

		public GetTop3MessagesQueryHandler(IMessageQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<MessageDropwdownDto> Handle(GetTop3MessagesQuery request, CancellationToken cancellationToken)
		{
			// 1. Okunmamış mesaj sayısını çek
			int unreadCount = await _repository.GetUnreadMessageCountAsync(request.UserId);

			// 2. Son 3 mesajı çek (Resim yolları dahil)
			var recentMessages = await _repository.GetRecentMessagesAsync(request.UserId, 3);

			// 3. Paketi hazırla ve ViewComponent'e gönder (Dapper'ın AsList'i ile performansı koruyarak)
			return new MessageDropwdownDto
			{
				UnreadCount = unreadCount,
				Messages = recentMessages?.ToList() ?? new List<MessageItemDto>()
			};
		}
	}
}
