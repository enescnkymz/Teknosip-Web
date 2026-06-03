using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.StudentProfile.Queries.GetStudentProfile
{
	public class GetStudentProfileQueryHandler : IRequestHandler<GetStudentProfileQuery, StudentProfileDto>
	{
		private readonly IStudentQueryRepository _repository;

		public GetStudentProfileQueryHandler(IStudentQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<StudentProfileDto> Handle(GetStudentProfileQuery request, CancellationToken cancellationToken)
		{
			
			var profile = await _repository.GetStudentProfileByUserIdAsync(request.UserId);
		
			if (profile == null)
			{
				return new StudentProfileDto();
			}

			return profile;
		}
	}
}
