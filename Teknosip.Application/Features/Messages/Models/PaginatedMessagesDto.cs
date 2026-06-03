using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Models
{
	public class PaginatedMessagesDto
	{
		public List<MessageItemDto> Messages { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public bool HasPreviousPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPages;

	}
}
