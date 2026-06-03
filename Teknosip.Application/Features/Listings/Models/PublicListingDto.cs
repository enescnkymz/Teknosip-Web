using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Listings.Models
{
	public class PublicListingDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public ListingType ListingType { get; set; }
		public CategoryType CategoryType { get; set; }
		public WorkType? WorkType { get; set; }
		public string Image { get; set; } // İlanın kendi afişi
		public DateTime CreatedAt { get; set; }
		public DateTime? Deadline { get; set; }

		// Yapan Kişi/Kurum Bilgileri
		public string PublisherName { get; set; }
		public string PublisherLogo { get; set; }
		public string City { get; set; }
	}
}
