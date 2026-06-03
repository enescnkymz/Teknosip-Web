using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;

namespace Teknosip.Application.Features.AcademicianProfile.Queries.GetAcademicianProfile
{
	public class GetAcademicianProfileQuery : IRequest<AcademicianProfileDto>
	{
		public Guid UserID { get; set; }

		public GetAcademicianProfileQuery(Guid userID)
		{
			UserID = userID;
		}

	}
}
