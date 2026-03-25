using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface ICategoryQueryRepository
	{
		Task<IReadOnlyList<Category>> GetAllAsync();
	}
}
