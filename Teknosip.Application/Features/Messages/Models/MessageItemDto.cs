using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Models
{
	public class MessageItemDto
	{
		public int Id { get; set; }
		public string SenderName { get; set; }
		public string? SenderImageUrl { get; set; } 
		public string MessageContent { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsRead { get; set; }
	}
}
