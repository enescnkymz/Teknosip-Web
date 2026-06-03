using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Models
{
	public class SentMessageItemDto
	{
		public int Id { get; set; }
		public string ReceiverName { get; set; } 
		public string ReceiverImageUrl { get; set; }
		public string MessageContent { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
