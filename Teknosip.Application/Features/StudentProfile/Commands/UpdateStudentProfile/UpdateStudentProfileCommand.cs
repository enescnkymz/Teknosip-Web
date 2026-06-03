using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;

namespace Teknosip.Application.Features.StudentProfile.Commands.UpdateStudentProfile
{
	public class UpdateStudentProfileCommand : IRequest<UpdateStudentResult>
	{
		public Guid UserId { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string University { get; set; }
		public string Department { get; set; }
		public string StudentNumber { get; set; }
		public int Grade { get; set; }
		public string? About { get; set; }

		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }

		public Stream? PhotoStream { get; set; }
		public string? PhotoFileName { get; set; }
	}
}
