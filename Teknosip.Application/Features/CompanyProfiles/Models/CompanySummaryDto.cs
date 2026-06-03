using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.CompanyProfiles.Models
{
	public class CompanySummaryDto
	{
		public Guid AppUserId { get; set; }
		public string CompanyName { get; set; }
		public string? Sector { get; set; }
		public string City { get; set; }
		public string? Website { get; set; }
		public int? EmployeeCount { get; set; }
		public string? ProfilePhoto { get; set; }
	}


}
