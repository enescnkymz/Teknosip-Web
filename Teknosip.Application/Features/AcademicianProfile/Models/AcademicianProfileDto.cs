using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.AcademicianProfile.Models
{
	public class AcademicianProfileDto
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Title { get; set; }
		public string University { get; set; }
		public string Department { get; set; }
		public string About { get; set; }
		public string CurrentPhotoUrl { get; set; }

	}
}
