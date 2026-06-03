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
	public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, bool>
	{

		private readonly UserManager<AppUser> _userManager;
		private readonly IStudentCommandRepository _studentCommandRepository;

		public RegisterStudentCommandHandler(UserManager<AppUser> userManager, IStudentCommandRepository studentCommandRepository)
		{
			_userManager = userManager;
			_studentCommandRepository = studentCommandRepository;
		}

		public async Task<bool> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
		{
			var user = new AppUser
			{
				Id = Guid.NewGuid(),
				FullName = request.FullName,
				UserName = request.UserName,
				Email = request.Email,
				PhoneNumber = request.PhoneNumber,
				CreatedAt = DateTime.UtcNow,
				UserType = UserType.Student,
				ProfilePhoto = "/images/profiles/default.webp"

			};

			var result = await _userManager.CreateAsync(user, request.Password);

			 if (!result.Succeeded)
				return false;

			 try
			 {
				var studentProfile = new Domain.Entities.StudentProfile
				{
					AppUserId = user.Id,
					FullName = request.FullName,
					University = request.University,
					Department = request.Department,
					StudentNumber = request.StudentNumber,
					Grade = request.Grade,
				};


				await _studentCommandRepository.AddAsync(studentProfile, cancellationToken);
				await _studentCommandRepository.SaveAsync(cancellationToken);

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
