using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Auth.Commands
{
	public class RegisterInstitutionCommandHandler : IRequestHandler<RegisterInstitutionCommand,bool>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IInstitutionCommandRepository _repository;

		public RegisterInstitutionCommandHandler(UserManager<AppUser> userManager, IInstitutionCommandRepository repository)
		{
			_userManager = userManager;
			_repository = repository;
		}

		public async Task<bool> Handle(RegisterInstitutionCommand request, CancellationToken cancellationToken)
		{
			var user = new AppUser
			{
				Id = Guid.NewGuid(),
				UserName = request.UserName,
				FullName = request.InstitutionName,
				Email = request.Email,
				PhoneNumber = request.PhoneNumber,
				UserType = UserType.Institution, 
				CreatedAt = DateTime.UtcNow
			};

			var result = await _userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return false;

			try
			{
				var institutionProfile = new Domain.Entities.InstitutionProfile
				{
					AppUserId = user.Id,
					InstitutionName = request.InstitutionName,
					City = request.City,
					IsApproved = false
				};

				await _repository.AddAsync(institutionProfile, cancellationToken);
				await _repository.SaveAsync(cancellationToken);

				return true;
			}
			catch (Exception)
			{
				await _userManager.DeleteAsync(user);
				return false;
			}
		}
	}
}
