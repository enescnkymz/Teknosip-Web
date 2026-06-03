using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Repositories;


namespace Teknosip.Application.Features.Messages.Queries.GetAllMessages
{
	public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, PaginatedMessagesDto>
	{
		private readonly IMessageQueryRepository _repository;

		public GetAllMessagesQueryHandler(IMessageQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<PaginatedMessagesDto> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
		{
			// 1. Repository'den verileri (Mesaj Listesi ve Toplam Sayı) çekiyoruz
			var result = await _repository.GetPaginatedMessagesByUserIdAsync(
				request.UserId,
				request.PageNumber,
				request.PageSize
			);

			// 2. Toplam Sayfa Sayısını Hesaplıyoruz
			// Örnek: Toplam 25 mesaj var, her sayfada 10 tane gösteriyoruz.
			// 25 / 10 = 2.5 -> Math.Ceiling (Yukarı Yuvarla) = 3 sayfa eder.
			int totalPages = 0;
			if (result.TotalCount > 0)
			{
				totalPages = (int)Math.Ceiling(result.TotalCount / (double)request.PageSize);
			}

			// 3. ViewModel'i oluşturup Controller'a geri gönderiyoruz
			var viewModel = new PaginatedMessagesDto
			{
				Messages = result.Messages?.ToList() ?? new List<MessageItemDto>(),
				CurrentPage = request.PageNumber,
				TotalPages = totalPages
			};

			return viewModel;
		}
	}
}
