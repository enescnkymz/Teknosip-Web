using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Notifications.Models
{
	public class NotificationDropdownDto
	{
		public int UnreadCount { get; set; }
		public string AreaName { get; set; }
		public List<NotificationItemDto> Notifications { get; set; } = new();
	}
	



}
