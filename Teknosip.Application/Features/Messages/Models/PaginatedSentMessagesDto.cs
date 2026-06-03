using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Models
{
	public class PaginatedSentMessagesDto
	{
		public List<SentMessageItemDto> Messages { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public bool HasPreviousPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPages;

	}
}
