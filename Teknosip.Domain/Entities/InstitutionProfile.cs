using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public class InstitutionProfile
	{
		public int Id { get; set; }
		public Guid AppUserId { get; set; }
		public string InstitutionName { get; set; }
		public string Address { get; set; }
		public string? Website { get; set; }
		public string About { get; set; }

		// Navigation
		public AppUser AppUser { get; set; }
	}
}
