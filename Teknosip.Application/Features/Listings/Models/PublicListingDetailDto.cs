using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Listings.Models
{
	public class PublicListingDetailDto : PublicListingDto
	{
		public Guid PublishedById { get; set; }
		public string Description { get; set; }
		public decimal? SalaryOrBudget { get; set; }
	}
}
