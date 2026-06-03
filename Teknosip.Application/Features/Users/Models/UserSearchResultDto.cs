using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Users.Models
{
	public class UserSearchResultDto
	{
		public Guid Id { get; set; }
		public string FullName { get; set; }
		public UserType UserType { get; set; }

	}
}
