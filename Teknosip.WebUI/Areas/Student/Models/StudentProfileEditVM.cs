using System.ComponentModel.DataAnnotations;

namespace Teknosip.WebUI.Areas.Student.Models
{
	public class StudentProfileEditVM
	{

		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }

		// Öğrenciye Özel Alanlar
		public string University { get; set; }
		public string Department { get; set; }
		public string StudentNumber { get; set; }
		public int Grade { get; set; } 
		public string? About { get; set; }

		// Fotoğraf İşlemleri
		public string? CurrentPhotoUrl { get; set; }
		public IFormFile? NewProfilePhoto { get; set; }

		// Şifre İşlemleri
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		[Compare("NewPassword", ErrorMessage = "Şifreler birbiriyle uyuşmuyor.")]
		public string? ConfirmNewPassword { get; set; }

	}
}
