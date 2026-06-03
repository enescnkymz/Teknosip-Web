namespace Teknosip.WebUI.Areas.Institution.Models
{
	public class InstitutionProfileEditVM
	{
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string InstitutionName { get; set; }
		public string City { get; set; }
		public string? Website { get; set; }
		public string? About { get; set; }

		public string? CurrentPhotoUrl { get; set; }
		public IFormFile? NewProfilePhoto { get; set; }

		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public string? ConfirmNewPassword { get; set; }
	}

}
