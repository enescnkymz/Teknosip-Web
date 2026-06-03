using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class MessageQueryRepository : IMessageQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public MessageQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<int> GetUnreadMessageCountAsync(Guid userId)
		{
			string sql = "SELECT COUNT(*) FROM Messages WHERE ReceiverId = @UserId AND IsRead = 0";
			return await _dbConnection.ExecuteScalarAsync<int>(sql, new { UserId = userId });
		}

		public async Task<IEnumerable<MessageItemDto>> GetRecentMessagesAsync(Guid userId, int count = 3)
		{
			
			string sql = $@"
            SELECT TOP {count} 
                m.Id, 
                u.FullName AS SenderName, 
                u.ProfilePhoto AS SenderImageUrl, 
                m.Content AS MessageContent, 
                m.CreatedAt, 
                m.IsRead
            FROM Messages m
            INNER JOIN AspNetUsers u ON m.SenderId = u.Id
            WHERE m.ReceiverId = @UserId
            ORDER BY m.CreatedAt DESC";

			return await _dbConnection.QueryAsync<MessageItemDto>(sql, new { UserId = userId });
		}

		public async Task<(IEnumerable<MessageItemDto> Messages, int TotalCount)> GetPaginatedMessagesByUserIdAsync(Guid userId, int pageNumber, int pageSize)
		{
			// Kaç kaydın atlanacağını hesaplıyoruz (Örn: 2. sayfa için ilk 10 kaydı atla)
			int offset = (pageNumber - 1) * pageSize;

			string sql = @"
        -- 1. Sorgu: Toplam Kayıt Sayısı
        SELECT COUNT(*) 
        FROM Messages 
        WHERE ReceiverId = @UserId;

        -- 2. Sorgu: Sadece O Sayfaya Ait Veriler
        SELECT 
            m.Id, 
            u.FullName AS SenderName, 
            u.ProfilePhoto AS SenderImageUrl, 
            m.Content AS MessageContent, 
            m.CreatedAt, 
            m.IsRead
        FROM Messages m
        INNER JOIN AspNetUsers u ON m.SenderId = u.Id
        WHERE m.ReceiverId = @UserId
        ORDER BY m.CreatedAt DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

			// Dapper ile iki sorguyu aynı anda çalıştırıyoruz
			using var multi = await _dbConnection.QueryMultipleAsync(sql, new { UserId = userId, Offset = offset, PageSize = pageSize });

			// Sırasıyla sonuçları okuyoruz
			int totalCount = await multi.ReadFirstAsync<int>();
			var messages = await multi.ReadAsync<MessageItemDto>();

			return (messages, totalCount);
		}

		public async Task<(IEnumerable<SentMessageItemDto> Messages, int TotalCount)> GetPaginatedSentMessagesByUserIdAsync(Guid userId, int pageNumber, int pageSize)
		{
			int offset = (pageNumber - 1) * pageSize;

			string sql = @"
        
        SELECT COUNT(*) 
        FROM Messages 
        WHERE SenderId = @UserId;

       
        SELECT 
            m.Id, 
            u.FullName AS ReceiverName, 
            u.ProfilePhoto AS ReceiverImageUrl, 
            m.Content AS MessageContent, 
            m.CreatedAt 
        FROM Messages m
        INNER JOIN AspNetUsers u ON m.ReceiverId = u.Id
        WHERE m.SenderId = @UserId
        ORDER BY m.CreatedAt DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

			using var multi = await _dbConnection.QueryMultipleAsync(sql, new { UserId = userId, Offset = offset, PageSize = pageSize });

			int totalCount = await multi.ReadFirstAsync<int>();
			var messages = await multi.ReadAsync<SentMessageItemDto>();

			return (messages, totalCount);
		}



	}
}
