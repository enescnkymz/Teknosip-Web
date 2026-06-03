using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.AcademicianProfile.Models
{
	public class PublicAcademicianDto
	{
		public Guid AppUserId { get; set; }
		public string FullName { get; set; }
		public string Title { get; set; }
		public string University { get; set; }
		public string Department { get; set; }
		public string? About { get; set; }
		public string? ProfilePhoto { get; set; } 

	}
}
