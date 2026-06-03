using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.InstitutionProfile.Models
{
	public class UpdateInstitutionResult
	{
		public bool IsSuccess { get; set; }
		public List<string> Errors { get; set; } = new List<string>();

	}

}
