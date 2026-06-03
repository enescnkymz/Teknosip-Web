using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Applications.Models;

namespace Teknosip.Application.Repositories
{
	public interface IProjectApplicationQueryRepository
	{
		Task<IEnumerable<StudentApplicationDto>> GetMyApplicationsAsync(Guid studentId, CancellationToken cancellationToken);
		Task<IEnumerable<CompanyApplicationDto>> GetApplicationsByProjectIdAsync(int projectId, Guid companyId, CancellationToken cancellationToken);

	}
}

