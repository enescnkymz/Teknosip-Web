using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;

namespace Teknosip.Application.Features.Applications.Commands.ApplyToListing
{
	public class ApplyToListingCommand : IRequest<ApplyToListingResult>
	{
		public int ProjectId { get; set; }
		public Guid StudentId { get; set; }
		public string? CoverLetter { get; set; }
	}

}
