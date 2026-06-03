using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;

namespace Teknosip.Application.Features.AdminProfile.Commands.UpdateAdminProfile
{
	public class UpdateAdminProfileCommand : IRequest<UpdateAdminResult>
	{
		public Guid UserId { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public Stream? PhotoStream { get; set; }
		public string? PhotoFileName { get; set; }
	}

}
