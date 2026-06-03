using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Repositories
{
	public interface IMessageCommandRepository
	{
		Task MarkAllAsReadAsync(Guid userId);
		Task<bool> ChangeReadStatusAsync(int messageId , bool markAsRead , CancellationToken cancellationToken = default);
		Task AddMessageAsync(Message entity , CancellationToken cancellation=default);
		Task SaveChangesAsync(CancellationToken cancellationToken = default);

	}
}
