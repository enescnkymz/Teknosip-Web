using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Auth.Commands
{
	public class RegisterAcademicianCommand : IRequest<bool>
	{
		public string UserName { get; set; }
		public string PhoneNumber { get; set; }
		public string Title { get; set; }
		public string FullName { get; set; }
		public string Department { get; set; }
		public string University { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }

	}
}
