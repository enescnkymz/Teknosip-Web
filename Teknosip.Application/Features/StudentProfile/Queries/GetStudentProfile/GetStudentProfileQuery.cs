using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;

namespace Teknosip.Application.Features.StudentProfile.Queries.GetStudentProfile
{
	public class GetStudentProfileQuery : IRequest<StudentProfileDto>
	{
		public Guid UserId { get; set; }

		public GetStudentProfileQuery(Guid userId)
		{
			UserId = userId;
		}
	}
}
