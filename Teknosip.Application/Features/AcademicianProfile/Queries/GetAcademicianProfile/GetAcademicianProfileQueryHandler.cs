using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.AcademicianProfile.Queries.GetAcademicianProfile
{
	public class GetAcademicianProfileQueryHandler : IRequestHandler<GetAcademicianProfileQuery, AcademicianProfileDto>
	{

		IAcademicianQueryRepository _repository;

		public GetAcademicianProfileQueryHandler(IAcademicianQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<AcademicianProfileDto> Handle(GetAcademicianProfileQuery request, CancellationToken cancellationToken)
		{
			
			var user = await _repository.GetProfileByIdAsync(request.UserID , cancellationToken);
			return user;

		}
	}
}
