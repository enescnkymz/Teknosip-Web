using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.AdminProfile.Models
{
	public class UpdateAdminResult
	{
		public bool IsSuccess { get; set; }
		public List<string> Errors { get; set; } = new List<string>();
	}
}
