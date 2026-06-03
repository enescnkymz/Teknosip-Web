using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;

namespace Teknosip.Application.Features.InstitutionProfile.Queries.GetApprovedInstitutions
{
	public class GetApprovedInstitutionsQuery : IRequest<IEnumerable<PublicInstitutionDto>>
	{
	}

}
