using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Persistence.Repositories
{
	public class StudentQueryRepository : IStudentQueryRepository
	{
		private readonly IDbConnection _dbConnection;

		public StudentQueryRepository(IDbConnection dbConnection)
		{
			_dbConnection = dbConnection;
		}

		public async Task<StudentProfileDto?> GetStudentProfileByUserIdAsync(Guid userId)
		{
			// AppUser ve StudentProfile tablolarını Inner Join ile birleştiriyoruz
			string sql = @"
                SELECT 
                    u.FullName,
                    u.Email,
                    u.PhoneNumber,
                    u.ProfilePhoto AS CurrentPhotoUrl,
                    s.University,
                    s.Department,
                    s.StudentNumber,
                    s.Grade,
                    s.About
                FROM AspNetUsers u
                INNER JOIN StudentProfiles s ON u.Id = s.AppUserId
                WHERE u.Id = @UserId";

			// Dapper'ın gücü: SQL'i çalıştır ve DTO'ya otomatik haritala (map)
			var profile = await _dbConnection.QueryFirstOrDefaultAsync<StudentProfileDto>(sql, new { UserId = userId });

			return profile;
		}
	}
}
