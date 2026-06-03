using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Users.Models;

namespace Teknosip.Application.Features.Users.Queries.GetPendingApprovals
{
	public class GetPendingApprovalsQuery : IRequest<IEnumerable<PendingApprovalDto>> 
	{

	}

}
