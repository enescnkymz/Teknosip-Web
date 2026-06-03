using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Auth.Commands
{
	public class RegisterStudentCommand : IRequest<bool>
	{
		public string FullName { get; set; }
		public string UserName { get; set; }      
		public string PhoneNumber { get; set; }   
		public string University { get; set; }
		public string Department { get; set; }
		public string StudentNumber { get; set; }
		public int Grade { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }

	}
}
