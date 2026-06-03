using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Listings.Models
{
	public class CompanyListingDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ListingType ListingType { get; set; }
		public CategoryType CategoryType { get; set; }
		public bool Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? Deadline { get; set; }

		// ZAMAN DARALIRKEN HAYAT KURTARAN O ŞIK ÖZELLİK:
		public int ApplicationCount { get; set; }
	}

}
