using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.InstitutionProfile.Models;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.InstitutionProfile.Command.UpdateInstitutionProfile
{
	public class UpdateInstitutionProfileCommandHandler : IRequestHandler<UpdateInstitutionProfileCommand, UpdateInstitutionResult>
	{
		private readonly IInstitutionCommandRepository _institutionCommandRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly IImageService _imageService;

		public UpdateInstitutionProfileCommandHandler(IInstitutionCommandRepository institutionCommandRepository, UserManager<AppUser> userManager, IImageService imageService)
		{
			_institutionCommandRepository = institutionCommandRepository;
			_userManager = userManager;
			_imageService = imageService;
		}

		public async Task<UpdateInstitutionResult> Handle(UpdateInstitutionProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return new UpdateInstitutionResult { IsSuccess = false, Errors = new List<string> { "Kullanıcı bulunamadı." } };

			// ŞİFRE DEĞİŞTİRME
			if (!string.IsNullOrEmpty(request.NewPassword))
			{
				if (string.IsNullOrEmpty(request.CurrentPassword))
					return new UpdateInstitutionResult { IsSuccess = false, Errors = new List<string> { "Şifrenizi değiştirmek için mevcut şifrenizi girmelisiniz." } };

				var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
				if (!passwordChangeResult.Succeeded)
				{
					return new UpdateInstitutionResult { IsSuccess = false, Errors = passwordChangeResult.Errors.Select(e => e.Description).ToList() };
				}
			}

			// FOTOĞRAF YÜKLEME
			if (request.PhotoStream != null && request.PhotoStream.Length > 0)
			{
				string photoUrl = await _imageService.SaveImageAsWebpAsync(request.PhotoStream, "profiles");
				if (!string.IsNullOrEmpty(user.ProfilePhoto))
				{
					await _imageService.DeleteImageAsync(user.ProfilePhoto);
				}
				user.ProfilePhoto = photoUrl;
			}

			// APPUSER GÜNCELLE
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.FullName = request.InstitutionName;

			var identityResult = await _userManager.UpdateAsync(user);
			if (!identityResult.Succeeded)
			{
				return new UpdateInstitutionResult { IsSuccess = false, Errors = identityResult.Errors.Select(e => e.Description).ToList() };
			}

			// INSTITUTION PROFILE GÜNCELLE
			var institutionProfile = await _institutionCommandRepository.GetByUserIdAsync(user.Id, cancellationToken);

			if (institutionProfile != null)
			{
				institutionProfile.InstitutionName = request.InstitutionName;
				institutionProfile.City = request.City;
				institutionProfile.Website = request.Website;
				institutionProfile.About = request.About;

				_institutionCommandRepository.UpdateInstitutionProfile(institutionProfile, cancellationToken);
				await _institutionCommandRepository.SaveAsync(cancellationToken);
			}

			return new UpdateInstitutionResult { IsSuccess = true };
		}
	}
}
