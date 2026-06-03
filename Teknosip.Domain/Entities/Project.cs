using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
    public enum WorkType
	{
		[Display(Name = "Uzaktan")]
		Remote,
		[Display(Name = "Yüz Yüze")]
		OnSite,
		[Display(Name = "Hibrit")]
		Hybrid
	}

	public enum ListingType
	{
		[Display(Name = "Proje")]
		Project = 1,
		[Display(Name = "İş")]
		Job = 2,
		[Display(Name = "Staj")]
		Internship = 3  
	}

	public enum CategoryType
	{

		[Display(Name = "Yazılım ve Bilişim Teknolojileri")]
		SoftwareAndIT = 1,

		[Display(Name = "Mühendislik ve İnşaat")]
		EngineeringAndConstruction = 2,

		[Display(Name = "Sağlık ve Medikal")]
		HealthcareAndMedical = 3,

		[Display(Name = "Enerji ve Madencilik")]
		EnergyAndMining = 4,

		[Display(Name = "Bankacılık ve Finans")]
		BankingAndFinance = 5,

		[Display(Name = "Otomotiv ve Makine")]
		AutomotiveAndMachinery = 6,

		[Display(Name = "Üretim ve Sanayi")]
		ManufacturingAndIndustry = 7,

		[Display(Name = "Perakende ve Ticaret")]
		RetailAndCommerce = 8,

		[Display(Name = "Tekstil ve Giyim")]
		TextileAndApparel = 9,

		[Display(Name = "Turizm ve Hizmet")]
		TourismAndServices = 10,

		[Display(Name = "Lojistik ve Ulaştırma")]
		LogisticsAndTransportation = 11,

		[Display(Name = "Diğer")]
		Other = 12
	
	}

	public class Project
	{
		public int Id { get; set; }
		public Guid PublishedById { get; set; }
        public string Image { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }		            
		public WorkType? WorkType { get; set; }      
		public ListingType ListingType { get; set; }
		public decimal? SalaryOrBudget { get; set; }
		public Boolean Status { get; set; }         
		public DateTime? Deadline { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }

		// Navigation
		public AppUser PublishedBy { get; set; }
		public ICollection<ProjectApplication> ProjectApplications { get; set; }
	}
}
