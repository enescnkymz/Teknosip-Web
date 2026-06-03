using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;

namespace Teknosip.Application.Features.AdminProfile.Queries.GetAdminProfile
{
	public class GetAdminProfileQuery : IRequest<AdminProfileDto>
	{
		public Guid UserId { get; set; }
		public GetAdminProfileQuery(Guid userId)
		{ 
			UserId = userId;			
		}




	}
}
