using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Domain.Entities
{
	public class AcademicianProfile
	{
        public int Id { get; set; }
        public Guid AppUserId { get; set; }
        public string University { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string About { get; set; }

		//Navigation
		public AppUser AppUser { get; set; }

    }
}
