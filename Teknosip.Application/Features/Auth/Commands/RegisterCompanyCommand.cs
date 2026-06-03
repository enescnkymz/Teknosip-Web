using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Auth.Commands
{
	public class RegisterCompanyCommand : IRequest<bool>
	{
		public string UserName { get; set; }
		public string City { get; set; }
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }


	}
}
