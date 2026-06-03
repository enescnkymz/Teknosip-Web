using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IContactMessageCommandRepository
	{ 

		Task AddAsync(ContactMessage message);
		Task SaveChangesAsync();

	}

}
