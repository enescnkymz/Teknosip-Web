using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.AcademicianProfile.Queries.GetApprovedAcademicians
{
	public class GetApprovedAcademiciansQueryHandler : IRequestHandler<GetApprovedAcademiciansQuery, IEnumerable<PublicAcademicianDto>>
	{
		private readonly IAcademicianQueryRepository _repository;

		public GetApprovedAcademiciansQueryHandler(IAcademicianQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<PublicAcademicianDto>> Handle(GetApprovedAcademiciansQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetApprovedAcademiciansAsync(cancellationToken);
		}
	}

}
