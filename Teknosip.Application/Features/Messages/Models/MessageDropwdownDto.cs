using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Models
{
	public class MessageDropwdownDto
	{
		public int UnreadCount { get; set; }
		public List<MessageItemDto> Messages { get; set; } = new();

	}
}
