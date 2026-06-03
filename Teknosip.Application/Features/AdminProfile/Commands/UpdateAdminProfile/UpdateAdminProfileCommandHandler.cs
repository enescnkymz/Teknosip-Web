using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AdminProfile.Models;
using Teknosip.Application.Interfaces;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.AdminProfile.Commands.UpdateAdminProfile
{
	public class UpdateAdminProfileCommandHandler : IRequestHandler<UpdateAdminProfileCommand, UpdateAdminResult>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IImageService _imageService;

		public UpdateAdminProfileCommandHandler(UserManager<AppUser> userManager, IImageService imageService)
		{
			_userManager = userManager;
			_imageService = imageService;
		}

		public async Task<UpdateAdminResult> Handle(UpdateAdminProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return new UpdateAdminResult { IsSuccess = false, Errors = new List<string> { "Kullanıcı bulunamadı." } };

			// ŞİFRE DEĞİŞTİRME
			if (!string.IsNullOrEmpty(request.NewPassword))
			{
				if (string.IsNullOrEmpty(request.CurrentPassword))
					return new UpdateAdminResult { IsSuccess = false, Errors = new List<string> { "Mevcut şifrenizi girmelisiniz." } };

				var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
				if (!passwordChangeResult.Succeeded)
					return new UpdateAdminResult { IsSuccess = false, Errors = passwordChangeResult.Errors.Select(e => e.Description).ToList() };
			}

			// FOTOĞRAF YÜKLEME
			if (request.PhotoStream != null && request.PhotoStream.Length > 0)
			{
				string photoUrl = await _imageService.SaveImageAsWebpAsync(request.PhotoStream, "profiles");
				if (!string.IsNullOrEmpty(user.ProfilePhoto))
					await _imageService.DeleteImageAsync(user.ProfilePhoto);

				user.ProfilePhoto = photoUrl;
			}

			// BİLGİLERİ GÜNCELLE (Sadece AppUser)
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.FullName = request.FullName;

			var identityResult = await _userManager.UpdateAsync(user);
			if (!identityResult.Succeeded)
				return new UpdateAdminResult { IsSuccess = false, Errors = identityResult.Errors.Select(e => e.Description).ToList() };

			return new UpdateAdminResult { IsSuccess = true };
		}

	}
}
