using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Applications.Queries.GetProjectApplications
{
	public class GetProjectApplicationsQueryHandler : IRequestHandler<GetProjectApplicationsQuery, IEnumerable<CompanyApplicationDto>>
	{
		private readonly IProjectApplicationQueryRepository _repository;
		public GetProjectApplicationsQueryHandler(IProjectApplicationQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<CompanyApplicationDto>> Handle(GetProjectApplicationsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetApplicationsByProjectIdAsync(request.ProjectId, request.CompanyId, cancellationToken);
		}
	}
}
