using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Auth.Models
{
	public class LoginResult
	{

		public bool Success { get; set; }
		public string Message { get; set; }
		public string RedirectUrl { get; set; }
		public AppUser User { get; set; }

	}
}
