using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class ProjectApplicationQueryRepository :IProjectApplicationQueryRepository
	{

		private readonly IDbConnection _dbConnection;

		public ProjectApplicationQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<IEnumerable<StudentApplicationDto>> GetMyApplicationsAsync(Guid studentId, CancellationToken cancellationToken)
		{
			
			string sql = @"
                SELECT 
                    pa.Id AS ApplicationId,
                    p.Id AS ProjectId,
                    p.Title AS ProjectTitle,
                    p.ListingType,
                    COALESCE(c.CompanyName, a.FullName, i.InstitutionName) AS PublisherName,
                    pa.Status,
                    pa.AppliedAt,
                    pa.ReviewedAt,
                    pa.RejectionReason
                FROM ProjectApplications pa
                INNER JOIN Projects p ON pa.ProjectId = p.Id
                INNER JOIN AspNetUsers u ON p.PublishedById = u.Id
                LEFT JOIN CompanyProfiles c ON u.Id = c.AppUserId
                LEFT JOIN AcademicianProfiles a ON u.Id = a.AppUserId
                LEFT JOIN InstitutionProfiles i ON u.Id = i.AppUserId
                WHERE pa.AppUserId = @StudentId
                ORDER BY pa.AppliedAt DESC";

			var command = new CommandDefinition(sql, new { StudentId = studentId }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<StudentApplicationDto>(command);
		}

		public async Task<IEnumerable<CompanyApplicationDto>> GetApplicationsByProjectIdAsync(int projectId, Guid companyId, CancellationToken cancellationToken)
		{
			string sql = @"
        SELECT 
            pa.Id AS ApplicationId,
            pa.AppUserId AS StudentId,
            COALESCE(u.FullName, 'İsimsiz Öğrenci') AS StudentName,
            u.ProfilePhoto AS StudentPhoto,
            pa.CoverLetter,
            pa.Status,
            pa.AppliedAt,
            pa.ReviewedAt,
            pa.RejectionReason
        FROM ProjectApplications pa
        INNER JOIN Projects p ON pa.ProjectId = p.Id
        INNER JOIN AspNetUsers u ON pa.AppUserId = u.Id
        WHERE pa.ProjectId = @ProjectId AND p.PublishedById = @CompanyId
        ORDER BY pa.AppliedAt DESC";

			var command = new CommandDefinition(sql, new { ProjectId = projectId, CompanyId = companyId }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<CompanyApplicationDto>(command);
		}



	}

}
