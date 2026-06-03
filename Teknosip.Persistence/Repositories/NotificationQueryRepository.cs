using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Notifications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class NotificationQueryRepository : INotificationQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public NotificationQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<int> GetUnreadCountAsync(Guid userId)
		{
			string sql = "SELECT COUNT(*) FROM Notifications WHERE UserId = @UserId AND IsRead = 0";

			return await _dbConnection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
		}

		public async Task<IEnumerable<NotificationItemDto>> GetRecentNotificationsAsync(Guid userId, int count = 3)
		{
			// Güvenli string interpolation kullanarak dinamik TOP sayısı alıyoruz
			string sql = $@"
            SELECT TOP {count} Id, Title, Message, RedirectUrl, Type, CreatedAt, IsRead 
            FROM Notifications 
            WHERE UserId = @UserId 
            ORDER BY CreatedAt DESC";

			return await _dbConnection.QueryAsync<NotificationItemDto>(sql, new { UserId = userId });
		}


		public async Task<IEnumerable<NotificationItemDto>> GetAllByUserIdAsync(Guid userId)
		{
			// TOP veya LIMIT kullanmıyoruz, kullanıcının tüm bildirimlerini tarihe göre tersten çekiyoruz
			string sql = @"
        SELECT Id, Title, Message, RedirectUrl, Type, CreatedAt, IsRead 
        FROM Notifications 
        WHERE UserId = @UserId 
        ORDER BY CreatedAt DESC";

			return await _dbConnection.QueryAsync<NotificationItemDto>(sql, new { UserId = userId });
		}


		public async Task<IEnumerable<NotificationItemDto>> GetPaginatedNotificationsByUserIdAsync(Guid userId, int pageNumber, int pageSize)
		{
			int offset = (pageNumber - 1) * pageSize;

			string sql = @"
        SELECT 
            Id, Title, Message, CreatedAt, IsRead, Type, RedirectUrl
        FROM Notifications
        WHERE UserId = @UserId
        ORDER BY CreatedAt DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

			return await _dbConnection.QueryAsync<NotificationItemDto>(sql, new { UserId = userId, Offset = offset, PageSize = pageSize });
		}


	}
}
