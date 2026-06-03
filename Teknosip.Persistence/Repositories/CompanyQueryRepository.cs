using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class CompanyQueryRepository : ICompanyQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public CompanyQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<CompanyProfileDto?> GetCompanyProfileByUserIdAsync(Guid userId)
		{
			
			string sql = @"
                SELECT                    
                    u.Email,
                    u.PhoneNumber,
                    u.ProfilePhoto AS CurrentPhotoUrl,
                    c.CompanyName,
                    c.TaxNumber,
                    c.Sector,
                    c.FoundedYear,
                    c.EmployeeCount,
                    c.City,
                    c.Address,
                    c.Website,
                    c.About
                FROM AspNetUsers u
                INNER JOIN CompanyProfiles c ON u.Id = c.AppUserId
                WHERE u.Id = @UserId";

			var profile = await _dbConnection.QueryFirstOrDefaultAsync<CompanyProfileDto>(sql, new { UserId = userId });

			return profile;
		}


		public async Task<IEnumerable<CompanySummaryDto>> GetAllApprovedCompaniesAsync(CancellationToken cancellationToken)
		{
			// Dikkat: IsApproved = 1 (Sadece onaylı firmalar)
			string sql = @"
                SELECT 
                    c.AppUserId, 
                    c.CompanyName, 
                    c.Sector, 
                    c.City, 
                    c.Website, 
                    c.EmployeeCount, 
                    u.ProfilePhoto 
                FROM CompanyProfiles c
                INNER JOIN AspNetUsers u ON c.AppUserId = u.Id
                WHERE c.IsApproved = 1
                ORDER BY c.CompanyName ASC";

			var command = new CommandDefinition(sql, cancellationToken: cancellationToken);

			// Birden fazla kayıt döneceği için QueryAsync kullanıyoruz
			return await _dbConnection.QueryAsync<CompanySummaryDto>(command);
		}

		public async Task<CompanyDetailDto?> GetCompanyDetailByIdAsync(Guid id, CancellationToken cancellationToken)
		{
			string sql = @"
        SELECT 
            c.AppUserId, 
            c.CompanyName, 
            c.TaxNumber, 
            c.Sector, 
            c.FoundedYear, 
            c.EmployeeCount, 
            c.Address, 
            c.City, 
            c.Website, 
            c.About,
            u.FullName, 
            u.Email, 
            u.PhoneNumber, 
            u.ProfilePhoto,
            u.CreatedAt
        FROM CompanyProfiles c
        INNER JOIN AspNetUsers u ON c.AppUserId = u.Id
        WHERE c.AppUserId = @Id AND c.IsApproved = 1";

			var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);

			// Tek bir kayıt döneceği için QueryFirstOrDefaultAsync
			return await _dbConnection.QueryFirstOrDefaultAsync<CompanyDetailDto>(command);
		}

		


	}
}

