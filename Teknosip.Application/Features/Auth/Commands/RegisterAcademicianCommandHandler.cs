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
	public class RegisterAcademicianCommandHandler : IRequestHandler<RegisterAcademicianCommand, bool>
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly IAcademicianCommandRepository _repository;

		public RegisterAcademicianCommandHandler(UserManager<AppUser> userManager, IAcademicianCommandRepository repository)
		{
			_userManager = userManager;
			_repository = repository;
		}

		public async Task<bool> Handle(RegisterAcademicianCommand request, CancellationToken cancellationToken)
		{

			var user = new AppUser 
			{ 
				
            Id=Guid.NewGuid(),
            UserType= UserType.Academician,
			FullName= request.FullName,
			UserName= request.UserName,
			Email= request.Email,			
			PhoneNumber = request.PhoneNumber,
			CreatedAt= DateTime.UtcNow,
			ProfilePhoto = "/images/profiles/default.webp"
			};

			var result = await _userManager.CreateAsync(user, request.Password);
			if (!result.Succeeded)
				return false;

			try
			{
				var entity = new Domain.Entities.AcademicianProfile
				{

					FullName = request.FullName,
					AppUserId = user.Id,
					Department = request.Department,
					University = request.University,
					IsApproved = false,
					Title = request.Title,

				};

				await _repository.AddAsync(entity ,cancellationToken);
				await _repository.SaveChangesAsync(cancellationToken);

				return true;

			}
			catch (Exception ) 
			{
				await _userManager.DeleteAsync(user);
				return false;
			}

		
		
		}


	}
}
