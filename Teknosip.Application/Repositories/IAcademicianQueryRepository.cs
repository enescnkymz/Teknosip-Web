using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Repositories
{
	public interface IAcademicianQueryRepository
	{

		Task<AcademicianProfileDto> GetProfileByIdAsync(Guid id, CancellationToken cancellation);
		Task<IEnumerable<PublicAcademicianDto>> GetApprovedAcademiciansAsync(CancellationToken cancellationToken);
		Task<PublicAcademicianDetailDto?> GetAcademicianDetailAsync(Guid id, CancellationToken cancellationToken);
		
	}
}
