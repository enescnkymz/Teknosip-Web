using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Users.Models
{
	public class PendingApprovalDto
	{
		public Guid UserId { get; set; }
		public UserType UserType { get; set; }
		public string Email { get; set; }
		public DateTime RequestDate { get; set; }
		public string DisplayName { get; set; }	
		public string DetailInfo { get; set; }

	}
}
