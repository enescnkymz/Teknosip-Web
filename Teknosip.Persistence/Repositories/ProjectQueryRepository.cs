using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Listings.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class ProjectQueryRepository : IProjectQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public ProjectQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<CompanyListingDto>> GetListingsByCompanyIdAsync(Guid companyId, CancellationToken cancellationToken)
		{
			
			string sql = @"
                SELECT 
                    p.Id, 
                    p.Title, 
                    p.ListingType, 
                    p.CategoryType, 
                    p.Status, 
                    p.CreatedAt, 
                    p.Deadline,
                    (SELECT COUNT(*) FROM ProjectApplications a WHERE a.ProjectId = p.Id) AS ApplicationCount
                FROM Projects p
                WHERE p.PublishedById = @CompanyId
                ORDER BY p.CreatedAt DESC";

			var command = new CommandDefinition(sql, new { CompanyId = companyId }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<CompanyListingDto>(command);
		}

		public async Task<IEnumerable<PublicListingDto>> GetAllActiveListingsAsync(CancellationToken cancellationToken)
		{
			string sql = @"
                SELECT 
                    p.Id, p.Title, p.ListingType, p.CategoryType, p.WorkType, p.Image, p.CreatedAt, p.Deadline,
                    COALESCE(c.CompanyName, a.FullName, i.InstitutionName) AS PublisherName,
                    u.ProfilePhoto AS PublisherLogo,
                    COALESCE(c.City, i.City, 'Belirtilmemiş') AS City
                FROM Projects p
                INNER JOIN AspNetUsers u ON p.PublishedById = u.Id
                LEFT JOIN CompanyProfiles c ON u.Id = c.AppUserId
                LEFT JOIN AcademicianProfiles a ON u.Id = a.AppUserId
                LEFT JOIN InstitutionProfiles i ON u.Id = i.AppUserId
                WHERE p.Status = 1
                ORDER BY p.CreatedAt DESC";

			var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<PublicListingDto>(command);
		}


		public async Task<PublicListingDetailDto?> GetListingDetailAsync(int id, CancellationToken cancellationToken)
		{
			string sql = @"
                SELECT 
                    p.Id, p.PublishedById, p.Title, p.Description, p.ListingType, p.CategoryType, p.WorkType, 
                    p.SalaryOrBudget, p.Image, p.CreatedAt, p.Deadline,
                    COALESCE(c.CompanyName, a.FullName, i.InstitutionName) AS PublisherName,
                    u.ProfilePhoto AS PublisherLogo,
                    COALESCE(c.City, i.City, 'Belirtilmemiş') AS City
                FROM Projects p
                INNER JOIN AspNetUsers u ON p.PublishedById = u.Id
                LEFT JOIN CompanyProfiles c ON u.Id = c.AppUserId
                LEFT JOIN AcademicianProfiles a ON u.Id = a.AppUserId
                LEFT JOIN InstitutionProfiles i ON u.Id = i.AppUserId
                WHERE p.Id = @Id AND p.Status = 1";

			var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryFirstOrDefaultAsync<PublicListingDetailDto>(command);
		}



	}

}
