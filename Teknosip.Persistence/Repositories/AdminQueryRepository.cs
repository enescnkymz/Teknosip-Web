using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class AdminQueryRepository : IAdminQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public AdminQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<AdminProfileDto> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken)
		{
			string sql = @"
                SELECT 
                    FullName, 
                    Email, 
                    PhoneNumber, 
                    ProfilePhoto AS CurrentPhotoUrl 
                FROM AspNetUsers 
                WHERE Id = @UserId";

			var command = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: cancellationToken);
			var profileDto = await _dbConnection.QueryFirstOrDefaultAsync<AdminProfileDto>(command);

			return profileDto ?? new AdminProfileDto();
		}
	}
}
