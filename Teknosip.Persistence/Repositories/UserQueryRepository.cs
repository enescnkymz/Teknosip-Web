using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Users.Models;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Persistence.Repositories
{
	public class UserQueryRepository : IUserQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public UserQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<UserSearchResultDto>> SearchUsersAsync(string searchTerm)
		{
			
			string sql = @"
        SELECT TOP 10 
            Id,
            FullName,
            UserType
        FROM AspNetUsers       
        WHERE FullName LIKE @SearchTerm
        ORDER BY FullName";

			
			return await _dbConnection.QueryAsync<UserSearchResultDto>(
				sql,
				new { SearchTerm = $"%{searchTerm}%" }
			);
		
		}

		public async Task<IEnumerable<PendingApprovalDto>> GetPendingApprovalsAsync(CancellationToken cancellationToken)
		{
			// COALESCE SQL'de "ilk dolu (null olmayan) değeri al demektir
			string sql = @"
        SELECT 
            u.Id AS UserId,
            u.UserType,
            u.Email,
            u.CreatedAt AS RequestDate,
            COALESCE(c.CompanyName, a.FullName, i.InstitutionName) AS DisplayName,
            COALESCE(c.TaxNumber, a.Title, i.City) AS DetailInfo
        FROM AspNetUsers u
        LEFT JOIN CompanyProfiles c ON u.Id = c.AppUserId
        LEFT JOIN AcademicianProfiles a ON u.Id = a.AppUserId
        LEFT JOIN InstitutionProfiles i ON u.Id = i.AppUserId
        WHERE 
            (u.UserType = 1 AND a.IsApproved = 0) OR
            (u.UserType = 2 AND c.IsApproved = 0) OR
            (u.UserType = 3 AND i.IsApproved = 0)
        ORDER BY u.CreatedAt ASC";

			var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<PendingApprovalDto>(command);
		}


		public async Task<bool> IsUserApprovedAsync(Guid userId, UserType userType, CancellationToken cancellationToken)
		{
			// Hangi tabloya bakacağımızı UserType'a göre belirliyoruz
			string tableName = userType switch
			{
				UserType.Company => "CompanyProfiles",
				UserType.Academician => "AcademicianProfiles",
				UserType.Institution => "InstitutionProfiles",
				_ => null // Öğrenci ve Admin gelirse null dönecek
			};

			
			if (tableName == null)
			{
				return true;
			}
		
			string sql = $@" SELECT CAST ( CASE WHEN     EXISTS ( SELECT 1 FROM {tableName}  WHERE AppUserId = @UserId AND IsApproved = 1 )      THEN 1 ELSE 0 END AS BIT )";

			var command = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: cancellationToken);
		
			return await _dbConnection.QuerySingleAsync<bool>(command);

		}





	}

}
