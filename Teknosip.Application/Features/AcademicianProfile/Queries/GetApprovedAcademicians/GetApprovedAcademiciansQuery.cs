using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;

namespace Teknosip.Application.Features.AcademicianProfile.Queries.GetApprovedAcademicians
{
	public class GetApprovedAcademiciansQuery : IRequest<IEnumerable<PublicAcademicianDto>> 
	{ 
	}

}
