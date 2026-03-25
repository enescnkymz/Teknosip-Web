using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public class StudentProfile
	{
        public int Id { get; set; }
		public Guid AppUserId { get; set; }
		public string University { get; set; }
		public string Department {  get; set; }
		public string StudentNumber { get; set; }
		public int Grade { get; set; }
		public string About { get; set; }

     	//Navigation
		public AppUser AppUser { get; set; }
    }
}
