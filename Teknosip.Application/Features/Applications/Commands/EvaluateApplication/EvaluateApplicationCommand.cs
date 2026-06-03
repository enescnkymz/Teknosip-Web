using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Applications.Commands.EvaluateApplication
{
	public class EvaluateApplicationCommand : IRequest<bool>
	{
		public int ApplicationId { get; set; }
		public Guid CompanyId { get; set; }
		public ApplicationStatus Status { get; set; }
		public string? RejectionReason { get; set; }
	}

}
