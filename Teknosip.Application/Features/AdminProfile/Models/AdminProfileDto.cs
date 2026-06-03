using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.AdminProfile.Models
{
	public class AdminProfileDto
	{
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? CurrentPhotoUrl { get; set; }
	}

}
