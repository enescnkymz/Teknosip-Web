using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.StudentProfile.Models
{
	public class StudentProfileDto
	{
		// AppUser Tablosu
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? CurrentPhotoUrl { get; set; }

		// StudentProfile Tablosu
		public string University { get; set; }
		public string Department { get; set; }
		public string StudentNumber { get; set; }
		public int Grade { get; set; }
		public string? About { get; set; }

	}
}
