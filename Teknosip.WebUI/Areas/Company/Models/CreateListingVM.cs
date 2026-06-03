using System.ComponentModel.DataAnnotations;
using Teknosip.Domain.Entities;

namespace Teknosip.WebUI.Areas.Company.Models
{
	public class CreateListingVM
	{
		[Required(ErrorMessage = "İlan başlığı zorunludur.")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Açıklama zorunludur.")]
		public string Description { get; set; }

		[Required(ErrorMessage = "Kategori seçimi zorunludur.")]
		public CategoryType CategoryType{ get; set; }

		[Required(ErrorMessage = "İlan tipi seçimi zorunludur.")]
		public ListingType ListingType { get; set; }

		public WorkType? WorkType { get; set; }

		public decimal? SalaryOrBudget { get; set; }

		public DateTime? Deadline { get; set; }

		[Required(ErrorMessage = "Lütfen ilan için bir görsel seçin.")]
		public IFormFile ImageFile { get; set; }

	}
}
