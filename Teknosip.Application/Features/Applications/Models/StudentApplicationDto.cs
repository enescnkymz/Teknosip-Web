using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Applications.Models
{
	public class StudentApplicationDto
	{
		public int ApplicationId { get; set; }
		public int ProjectId { get; set; }
		public string ProjectTitle { get; set; }
		public ListingType ListingType { get; set; }
		
		public string PublisherName { get; set; }

		public ApplicationStatus Status { get; set; }
		public DateTime AppliedAt { get; set; }
		public DateTime? ReviewedAt { get; set; }
		public string? RejectionReason { get; set; }
	}

}
