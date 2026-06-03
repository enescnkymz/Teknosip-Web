using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Listings.Commands.CreateListing
{
	public class CreateListingCommand : IRequest<bool>
	{
		public Guid PublishedById { get; set; }
		public CategoryType	CategoryType{ get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public WorkType? WorkType { get; set; }
		public ListingType ListingType { get; set; }
		public decimal? SalaryOrBudget { get; set; }
		public DateTime? Deadline { get; set; }

		
		public Stream ImageStream { get; set; }
		public string ImageFileName { get; set; }
	
	}

}
