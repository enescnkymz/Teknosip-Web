using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Applications.Queries.GetMyApplications
{
	public class GetMyApplicationsQueryHandler : IRequestHandler<GetMyApplicationsQuery, IEnumerable<StudentApplicationDto>>
	{
		private readonly IProjectApplicationQueryRepository _repository;

		public GetMyApplicationsQueryHandler(IProjectApplicationQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<StudentApplicationDto>> Handle(GetMyApplicationsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetMyApplicationsAsync(request.StudentId, cancellationToken);
		}
	}
}
