using System.ComponentModel.DataAnnotations;

namespace Teknosip.WebUI.Areas.Academician.Models
{
	public class AcademicianProfileEditVM
	{

		[Required(ErrorMessage = "Lütfen mail adresinizi giriniz.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
		public string PhoneNumber { get; set; }
		public string? CurrentPhotoUrl { get; set; }	
		public IFormFile? NewProfilePhoto { get; set; }
		public string? CurrentPassword { get; set; }
		public string? NewPassword { get; set; }
		public string? ConfirmNewPassword { get; set; }

		[Required(ErrorMessage = "Lütfen ad soyad giriniz.")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Bu alan zorunludur.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Lütfen üniversitenizi seçiniz.")]
		public string University { get; set; }

		[Required(ErrorMessage = "Lütfen bölümünüzü seçiniz.")]
		public string Department { get; set; }
		public string? About { get; set; }

	}
}
