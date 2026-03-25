using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;


namespace Teknosip.Persistence.Repositories
{
	public class CategoryQueryRepository : ICategoryQueryRepository
	{
		
        private readonly IConfiguration _configuration;

		public CategoryQueryRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<IReadOnlyList<Category>> GetAllAsync()
		{
			// Entity Framework'ü devre dışı bırakıp, ADO.NET ve Dapper ile saf hızda bağlanıyoruz!
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var sql = "SELECT Id, Name FROM Categories";
			var kategoriler = await connection.QueryAsync<Category>(sql);

			return kategoriler.ToList();
		}

	
	}
}
