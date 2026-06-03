using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Features.InstitutionProfile.Queries.GetInstitutionProfile
{
	public class GetInstitutionProfileQuery : IRequest<InstitutionProfileDto>
	{
		public Guid UserId { get; set; }

		public GetInstitutionProfileQuery(Guid userId)
		{
			UserId = userId;
		}
	}
}
