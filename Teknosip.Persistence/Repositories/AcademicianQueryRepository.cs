using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Persistence.Repositories
{
	public class AcademicianQueryRepository : IAcademicianQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public AcademicianQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}


		public async Task<AcademicianProfileDto> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken)
		{
			// İKİ TABLOYU JOIN İLE BİRLEŞTİRİYORUZ!
			// 'u' alias'ı AppUser (AspNetUsers) tablosunu, 
			// 'a' alias'ı AcademicianProfiles tablosunu temsil ediyor.
			string sql = @"
                SELECT 
                    a.FullName, 
                    u.Email, 
                    u.PhoneNumber, 
                    a.Title, 
                    a.University, 
                    a.Department, 
                    a.About, 
                    u.ProfilePhoto AS CurrentPhotoUrl 
                FROM AspNetUsers u
                INNER JOIN AcademicianProfiles a ON u.Id = a.AppUserId
                WHERE u.Id = @UserId";

			// Dapper'da CancellationToken kullanmak için CommandDefinition oluşturuyoruz
			var command = new CommandDefinition(
				commandText: sql,
				parameters: new { UserId = userId },
			cancellationToken: cancellationToken // İptal sinyalini Dapper'a bağladık!
			);

			var profileDto = await _dbConnection.QueryFirstOrDefaultAsync<AcademicianProfileDto>(sql , new { UserId = userId });

			// Eğer adamın profili henüz oluşmamışsa (null dönerse) boş dön
			return profileDto ?? new AcademicianProfileDto();
		}

		public async Task<IEnumerable<PublicAcademicianDto>> GetApprovedAcademiciansAsync(CancellationToken cancellationToken)
		{
			// İki tabloyu (AcademicianProfiles ve AspNetUsers) birleştirip veriyi çekiyoruz
			string sql = @"
                SELECT 
                    a.AppUserId,
                    a.FullName,
                    a.Title,
                    a.University,
                    a.Department,
                    a.About,
                    u.ProfilePhoto
                FROM AcademicianProfiles a
                INNER JOIN AspNetUsers u ON a.AppUserId = u.Id
                WHERE a.IsApproved = 1 AND u.UserType = 1
                ORDER BY a.FullName ASC";

			var command = new CommandDefinition(sql, cancellationToken: cancellationToken);
			return await _dbConnection.QueryAsync<PublicAcademicianDto>(command);
		}

		public async Task<PublicAcademicianDetailDto?> GetAcademicianDetailAsync(Guid id, CancellationToken cancellationToken)
		{
			string sql = @"
        SELECT 
            a.AppUserId,
            a.FullName,
            a.Title,
            a.University,
            a.Department,
            a.About,
            u.ProfilePhoto,
            u.Email,
            u.PhoneNumber,
            u.CreatedAt AS JoinedAt
        FROM AcademicianProfiles a
        INNER JOIN AspNetUsers u ON a.AppUserId = u.Id
        WHERE a.AppUserId = @Id AND a.IsApproved = 1 AND u.UserType = 1";

			var command = new CommandDefinition(sql, new { Id = id }, cancellationToken: cancellationToken);
			return await _dbConnection.QueryFirstOrDefaultAsync<PublicAcademicianDetailDto>(command);
		}


	}
}
