using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Notifications.Models
{
	public class NotificationItemDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public string? RedirectUrl { get; set; }
		public NotificationType Type { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsRead { get; set; }
	}
}
