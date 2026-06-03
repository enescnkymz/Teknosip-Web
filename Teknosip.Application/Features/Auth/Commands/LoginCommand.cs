using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Auth.Models;

namespace Teknosip.Application.Features.Auth.Commands
{
    public class LoginCommand :IRequest<LoginResult>
    {
		public string Email { get; set; }
		public string Password { get; set; }
		public bool RememberMe { get; set; }

	}
}
