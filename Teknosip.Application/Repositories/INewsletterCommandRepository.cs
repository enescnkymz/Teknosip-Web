using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface INewsletterCommandRepository
	{

		Task<bool> IsEmailRegisteredAsync(string email, CancellationToken cancellationToken = default);
		Task AddAsync(NewsletterSubscription entity,CancellationToken cancellationToken=default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);

	}
}
