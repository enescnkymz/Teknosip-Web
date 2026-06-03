using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.AcademicianProfile.Queries.GetAcademicianDetail
{
	public class GetAcademicianDetailQueryHandler : IRequestHandler<GetAcademicianDetailQuery, PublicAcademicianDetailDto?>
	{
		private readonly IAcademicianQueryRepository _repository;

		public GetAcademicianDetailQueryHandler(IAcademicianQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<PublicAcademicianDetailDto?> Handle(GetAcademicianDetailQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetAcademicianDetailAsync(request.Id, cancellationToken);
		}
	}


}
