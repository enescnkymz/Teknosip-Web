using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public class CompanyProfile
	{

		public int Id { get; set; }
		public Guid AppUserId { get; set; }		
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string Sector { get; set; }
		public int FoundedYear { get; set; }
		public int EmployeeCount { get; set; }
		public string? Website { get; set; }
		public string About {  get; set; }

		// Navigation		
		public AppUser AppUser { get; set; }

	}
}
