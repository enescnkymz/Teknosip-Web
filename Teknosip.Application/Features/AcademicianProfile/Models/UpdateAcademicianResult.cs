using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.AcademicianProfile.Models
{
	public class UpdateAcademicianResult
	{
		public bool IsSuccess { get; set; }
		public List<string> Errors { get; set; } = new List<string>();
		
	}
}
