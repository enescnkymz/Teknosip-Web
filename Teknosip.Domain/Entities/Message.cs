using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public class Message
	{
		public Guid Id { get; set; }
		public Guid SenderId { get; set; }
		public Guid ReceiverId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public bool IsRead { get; set; } = false;
		public DateTime? ReadAt { get; set; }


		public virtual AppUser Sender { get; set; }
		public virtual AppUser Receiver { get; set; }
	
	}
}
