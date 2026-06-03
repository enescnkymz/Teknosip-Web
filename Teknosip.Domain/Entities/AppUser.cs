using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{

	public enum UserType { 
		Student=0 ,
		Academician=1 ,
		Company=2	, 
		Institution=3 ,
		Admin=4
	}
	public class AppUser : IdentityUser<Guid>
	{
		
		public string? ProfilePhoto {  get; set; }		
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public UserType UserType { get; set; }
		public DateTime? LastNotificationReadAt { get; set; }
		public DateTime? LastMessageReadAt { get; set; }
        public string? FullName { get; set; }


        //Navigation
        public StudentProfile? StudentProfile { get; set; }
		public AcademicianProfile? AcademicianProfile { get; set; }
		public CompanyProfile? CompanyProfile { get; set; }
		public InstitutionProfile? InstitutionProfile { get; set; }
		public ICollection<ProjectApplication>? ProjectApplications { get; set; }
		public ICollection<Message>? SentMessages { get; set; }
		public ICollection<Message>? ReceivedMessages { get; set; }

	}
}
