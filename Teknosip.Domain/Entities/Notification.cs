using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{

	public enum NotificationType
	{
		Info = 1,       
		Success = 2,    
		Warning = 3,     
		Message = 4      
	}

	public class Notification
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public NotificationType Type { get; set; }
		public string? RedirectUrl { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public bool IsRead { get; set; } = false;

	}
}
