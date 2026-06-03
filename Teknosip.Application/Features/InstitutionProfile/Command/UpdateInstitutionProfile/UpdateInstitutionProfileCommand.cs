using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Features.InstitutionProfile.Command.UpdateInstitutionProfile
{
	public class UpdateInstitutionProfileCommand : IRequest<UpdateInstitutionResult>
	{
		public Guid UserId { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string InstitutionName { get; set; }
		public string City { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public Stream? PhotoStream { get; set; }
		public string? PhotoFileName { get; set; }
	}

}
