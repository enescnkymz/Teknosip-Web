using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;
using Teknosip.Persistence.Contexts;

namespace Teknosip.Persistence.Repositories
{
	public class MessageCommandRepository : IMessageCommandRepository
	{
		private readonly TeknosipDbContext _context;

		public MessageCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task AddMessageAsync(Message entity, CancellationToken cancellation = default)
		{
			await _context.Messages.AddAsync(entity , cancellation);
		}

		public async Task<bool> ChangeReadStatusAsync(int messageId ,bool markAsRead , CancellationToken cancellationToken = default)
		{
			await _context.Messages
		   .Where(m => m.Id == messageId)
		   .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsRead, markAsRead));

			return true;

		}

		public async Task MarkAllAsReadAsync(Guid userId)
		{
			await _context.Messages
				.Where(m => m.ReceiverId == userId && !m.IsRead)
				.ExecuteUpdateAsync(setters => setters.SetProperty(m => m.IsRead, true));
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			await _context.SaveChangesAsync(cancellationToken);
		}
	}
}
