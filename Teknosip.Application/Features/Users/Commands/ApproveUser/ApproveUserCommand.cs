using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Users.Commands.ApproveUser
{
	public class ApproveUserCommand : IRequest<bool>
	{
		public Guid UserId { get; set; }
		public UserType UserType { get; set; }
	}
}
