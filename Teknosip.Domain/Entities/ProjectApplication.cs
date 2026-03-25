using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public enum ApplicationStatus
	{
		Pending,      // Beklemede
		Accepted,     // Kabul edildi
		Rejected      // Reddedildi
	}
	public class ProjectApplication
	{
		public int Id { get; set; }
		public int ProjectId { get; set; }
		public Guid AppUserId { get; set; }
		public ApplicationStatus Status { get; set; }    // Pending, Accepted, Rejected
		public string? RejectionReason { get; set; }     // Ret sebebi (opsiyonel)
		public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
		public DateTime? ReviewedAt { get; set; }        // Firma ne zaman karar verdi

		// Navigation
		public Project Project { get; set; }
		public AppUser AppUser { get; set; }
	}
}
