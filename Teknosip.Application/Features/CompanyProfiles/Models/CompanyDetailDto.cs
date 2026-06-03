using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.CompanyProfiles.Models
{
	public class CompanyDetailDto
	{
		public Guid AppUserId { get; set; }
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string? Sector { get; set; }
		public int? FoundedYear { get; set; }
		public int? EmployeeCount { get; set; }
		public string? Address { get; set; }
		public string City { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }

		// AppUser Tablosundan Gelenler
		public string? FullName { get; set; } // Yetkili Kişi
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ProfilePhoto { get; set; }
		public DateTime CreatedAt { get; set; } // Sisteme Kayıt Tarihi
	}

}
