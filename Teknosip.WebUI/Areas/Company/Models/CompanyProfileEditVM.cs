namespace Teknosip.WebUI.Areas.Company.Models
{
	public class CompanyProfileEditVM
	{
		public string CompanyName { get; set; }
		public string TaxNumber { get; set; }
		public string? Sector { get; set; }
		public int? FoundedYear { get; set; }
		public int? EmployeeCount { get; set; }
		public string City { get; set; }
		public string? Address { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }

		public string? CurrentPhotoUrl { get; set; }
		public IFormFile? NewProfilePhoto { get; set; }

		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public string? ConfirmNewPassword { get; set; }

	}
}
