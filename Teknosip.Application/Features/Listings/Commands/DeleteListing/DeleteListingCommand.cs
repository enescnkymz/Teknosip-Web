using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Listings.Commands.DeleteListing
{
	public class DeleteListingCommand : IRequest<bool>
	{
		public int Id { get; set; }
		public Guid CompanyId { get; set; } 
	
	}

}
