using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;

namespace Teknosip.Application.Features.Listings.Queries.GetActiveListings
{
	public class GetActiveListingsQuery : IRequest<IEnumerable<PublicListingDto>>
	{ 
	}

}
