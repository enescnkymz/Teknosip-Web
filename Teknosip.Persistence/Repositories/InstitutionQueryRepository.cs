using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class InstitutionQueryRepository : IInstitutionQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public InstitutionQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<InstitutionProfileDto> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken)
		{
			string sql = @"
                SELECT 
                    u.Email, 
                    u.PhoneNumber, 
                    i.InstitutionName, 
                    i.City, 
                    i.Website, 
                    i.About, 
                    u.ProfilePhoto AS CurrentPhotoUrl 
                FROM AspNetUsers u
                INNER JOIN InstitutionProfiles i ON u.Id = i.AppUserId
                WHERE u.Id = @UserId";

			var command = new CommandDefinition(sql, new { UserId = userId }, cancellationToken: cancellationToken);
			var profileDto = await _dbConnection.QueryFirstOrDefaultAsync<InstitutionProfileDto>(command);

			return profileDto ?? new InstitutionProfileDto();
		}

		public async Task<IEnumerable<PublicInstitutionDto>> GetApprovedInstitutionsAsync(CancellationToken cancellationToken)
		{
			
			string sql = @"
                SELECT 
                    i.AppUserId,
                    i.InstitutionName,
                    i.City,
                    i.Website,
                    i.About,
                    u.ProfilePhoto
                FROM InstitutionProfiles i
                INNER JOIN AspNetUsers u ON i.AppUserId = u.Id
                WHERE i.IsApproved = 1 AND u.UserType = 3
                ORDER BY i.InstitutionName ASC";

			var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<PublicInstitutionDto>(command);
		}

		public async Task<PublicInstitutionDetailDto?> GetInstitutionDetailAsync(Guid id, CancellationToken cancellationToken)
		{
			string sql = @"
        SELECT 
            i.AppUserId,
            i.InstitutionName,
            i.City,
            i.Website,
            i.About,
            u.ProfilePhoto,
            u.Email,
            u.PhoneNumber,
            u.CreatedAt AS JoinedAt
        FROM InstitutionProfiles i
        INNER JOIN AspNetUsers u ON i.AppUserId = u.Id
        WHERE i.AppUserId = @Id AND i.IsApproved = 1 AND u.UserType = 3";

			var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryFirstOrDefaultAsync<PublicInstitutionDetailDto>(command);
		}

	}

}
