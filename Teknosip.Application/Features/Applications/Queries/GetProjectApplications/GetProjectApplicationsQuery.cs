using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;

namespace Teknosip.Application.Features.Applications.Queries.GetProjectApplications
{
	public class GetProjectApplicationsQuery : IRequest<IEnumerable<CompanyApplicationDto>>
	{
		public int ProjectId { get; set; }
		public Guid CompanyId { get; set; }
	}

}
