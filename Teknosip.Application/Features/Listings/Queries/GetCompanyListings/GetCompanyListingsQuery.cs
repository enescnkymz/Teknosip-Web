using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Listings.Queries.GetCompanyListings
{
	public class GetCompanyListingsQuery : IRequest<IEnumerable<CompanyListingDto>>
	{
		public Guid CompanyId { get; set; }
	
	}

}
