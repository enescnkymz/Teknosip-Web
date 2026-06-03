using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Applications.Models
{
	public class CompanyApplicationDto
	{
		public int ApplicationId { get; set; }
		public Guid StudentId { get; set; }
		public string StudentName { get; set; }
		public string StudentPhoto { get; set; }
		public string CoverLetter { get; set; }
		public ApplicationStatus Status { get; set; }
		public DateTime AppliedAt { get; set; }
		public DateTime? ReviewedAt { get; set; }
		public string? RejectionReason { get; set; }
	}

}
