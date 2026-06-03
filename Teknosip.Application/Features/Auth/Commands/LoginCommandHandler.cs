using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Auth.Models;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Auth.Commands
{
	public class LoginCommandHandler : IRequestHandler<LoginCommand,LoginResult>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IUserQueryRepository _userQueryRepository;

		public LoginCommandHandler(UserManager<AppUser> userManager, IUserQueryRepository userQueryRepository)
		{
			_userManager = userManager;
			_userQueryRepository = userQueryRepository;
		}

		public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				return new LoginResult { Success = false, Message = "E-posta veya şifre hatalı!" };
			}

			var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

			if (!isPasswordValid)
			{
				return new LoginResult { Success = false, Message = "E-posta veya şifre hatalı!" };
			}

			bool isApproved = await _userQueryRepository.IsUserApprovedAsync(user.Id, user.UserType, cancellationToken);

			if (!isApproved)
			{
				return new LoginResult { Success = false, Message = "Hesabınız henüz onaylanmamış! Lütfen bekleyin." };
			}


			string targetUrl = "/Home/Index";
			if (user.UserType == UserType.Student) targetUrl = "/Home/Index";
			else if (user.UserType == UserType.Company) targetUrl = "/Home/Index";
			else if (user.UserType == UserType.Academician) targetUrl = "/Home/Index";
			else if(user.UserType == UserType.Institution) targetUrl = "/Home/Index";

			return new LoginResult
			{
				Success = true,
				RedirectUrl = targetUrl,
				User = user,
			};
			
		
		}
	}
}
