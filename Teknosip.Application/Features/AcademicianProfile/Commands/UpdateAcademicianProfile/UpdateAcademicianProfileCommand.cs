using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;

namespace Teknosip.Application.Features.AcademicianProfile.Commands.UpdateAcademicianProfile
{
	public class UpdateAcademicianProfileCommand :IRequest<UpdateAcademicianResult>
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; }
		public string Title { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string University { get; set; }
		public string Department { get; set; }
		public string About { get; set; }
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public Stream? PhotoStream { get; set; }
		public string? PhotoFileName { get; set; }

	}
}
