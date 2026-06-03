using MediatR;
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
	public class RegisterCompanyCommandHandler : IRequestHandler<RegisterCompanyCommand,bool>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICompanyCommandRepository _repository;

		public RegisterCompanyCommandHandler(UserManager<AppUser> userManager, ICompanyCommandRepository repository)
		{
			_userManager = userManager;
			_repository = repository;
		}

		public async Task<bool> Handle(RegisterCompanyCommand request, CancellationToken cancellationToken)
		{
		
			var user = new AppUser
			{
				Id = Guid.NewGuid(),
				FullName = request.CompanyName,
				UserName = request.UserName,
				Email = request.Email,
				PhoneNumber = request.PhoneNumber,
				UserType = UserType.Company, 
				CreatedAt = DateTime.UtcNow,
				ProfilePhoto="/images/profiles/default-company.webp"
				
			};

			var result = await _userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded) return false;

		
			try
			{
				var companyProfile = new CompanyProfile
				{
					AppUserId = user.Id,
					CompanyName = request.CompanyName,
					City = request.City,
					TaxNumber = request.TaxNumber,
					IsApproved = false 
				};

				await _repository.AddAsync(companyProfile, cancellationToken);
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
