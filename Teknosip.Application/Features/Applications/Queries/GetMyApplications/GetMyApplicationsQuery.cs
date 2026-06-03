using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;

namespace Teknosip.Application.Features.Applications.Queries.GetMyApplications
{
	public class GetMyApplicationsQuery : IRequest<IEnumerable<StudentApplicationDto>>
	{
		public Guid StudentId { get; set; }
	}


}
