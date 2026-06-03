using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Users.Models;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Users.Queries.SearchUsers
{
	public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, List<UserSearchResultDto>>
	{
		private readonly IUserQueryRepository _repository;

		public SearchUsersQueryHandler(IUserQueryRepository repository)
		{
			_repository = repository;
		}

		public async Task<List<UserSearchResultDto>> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
		{
			var users = await _repository.SearchUsersAsync(request.SearchTerm);
			return users?.ToList() ?? new List<UserSearchResultDto>();
		}
	}
}
