using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Users.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Users.Queries.GetPendingApprovals
{
	public class GetPendingApprovalsQueryHandler : IRequestHandler<GetPendingApprovalsQuery, IEnumerable<PendingApprovalDto>>
	{
		private readonly IUserQueryRepository _repository;
		public GetPendingApprovalsQueryHandler(IUserQueryRepository repository)
		{ 
		_repository = repository;
		}	
		public async Task<IEnumerable<PendingApprovalDto>> Handle(GetPendingApprovalsQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetPendingApprovalsAsync(cancellationToken);
		}
	}
}
