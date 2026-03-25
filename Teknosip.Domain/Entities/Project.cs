using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
    public enum WorkType
	{
		Remote,
		OnSite,
		Hybrid
	}
	public class Project
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public Guid PublishedById { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }		            
		public WorkType? WorkType { get; set; }            
		public Boolean Status { get; set; }         
		public DateTime? Deadline { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }

		// Navigation
		public AppUser PublishedBy { get; set; }
		public Category Category { get; set; }
		public ICollection<ProjectApplication> ProjectApplications { get; set; }
	}
}
