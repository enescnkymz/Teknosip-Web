using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;
using Teknosip.Persistence.Contexts;

namespace Teknosip.Persistence.Repositories
{
	public class UserCommandRepository : IUserCommandRepository
	{
		private readonly TeknosipDbContext _context;

		public UserCommandRepository(TeknosipDbContext context)
		{
			_context = context;
		}

		public async Task<bool> ApproveUserAsync(Guid userId, UserType userType, CancellationToken cancellationToken)
		{
			switch (userType)
			{
				case UserType.Academician:
					var academician = await _context.AcademicianProfiles.FirstOrDefaultAsync(x => x.AppUserId == userId, cancellationToken);
					if (academician != null) academician.IsApproved = true;
					break;

				case UserType.Company:
					var company = await _context.CompanyProfiles.FirstOrDefaultAsync(x => x.AppUserId == userId, cancellationToken);
					if (company != null) company.IsApproved = true;
					break;

				case UserType.Institution:
					var institution = await _context.InstitutionProfiles.FirstOrDefaultAsync(x => x.AppUserId == userId, cancellationToken);
					if (institution != null) institution.IsApproved = true;
					break;

				default:
					return false;
			}

			var result = await _context.SaveChangesAsync(cancellationToken);
			return result > 0;
		}
	}
}
