using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Repositories
{
	public interface IInstitutionQueryRepository
	{
		Task<InstitutionProfileDto> GetProfileByIdAsync(Guid userId, CancellationToken cancellationToken);
		Task<IEnumerable<PublicInstitutionDto>> GetApprovedInstitutionsAsync(CancellationToken cancellationToken);
		Task<PublicInstitutionDetailDto?> GetInstitutionDetailAsync(Guid id, CancellationToken cancellationToken);
	}
}
