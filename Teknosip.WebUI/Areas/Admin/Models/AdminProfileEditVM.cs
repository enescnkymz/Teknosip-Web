namespace Teknosip.WebUI.Areas.Admin.Models
{

	public class AdminProfileEditVM
	{
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }

		public string? CurrentPhotoUrl { get; set; }
		public IFormFile? NewProfilePhoto { get; set; }

		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public string? ConfirmNewPassword { get; set; }
	}


}
