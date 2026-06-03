using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.AdminProfile.Queries.GetAdminProfile
{
	public class GetAdminProfileQueryHandler : IRequestHandler<GetAdminProfileQuery, AdminProfileDto>
	{
		private readonly IAdminQueryRepository _repository;
		public GetAdminProfileQueryHandler(IAdminQueryRepository repository) { _repository = repository; }

		public async Task<AdminProfileDto> Handle(GetAdminProfileQuery request, CancellationToken cancellationToken)
		{
			return await _repository.GetProfileByIdAsync(request.UserId, cancellationToken);
		}
	}

}
