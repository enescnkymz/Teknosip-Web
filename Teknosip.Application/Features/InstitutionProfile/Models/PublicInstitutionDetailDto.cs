using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.InstitutionProfile.Models
{
	public class PublicInstitutionDetailDto
	{
		public Guid AppUserId { get; set; }
		public string InstitutionName { get; set; }
		public string City { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }

		// AppUser 
		public string? ProfilePhoto { get; set; }
		public string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime JoinedAt { get; set; }

	}
}
