using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.AcademicianProfile.Models;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.AcademicianProfile.Commands.UpdateAcademicianProfile
{
	public class UpdateAcademicianProfileCommandHandler : IRequestHandler<UpdateAcademicianProfileCommand, UpdateAcademicianResult>
	{

		private readonly IAcademicianCommandRepository _academicianCommandRepository;	
		private readonly UserManager<AppUser> _userManager;
		private readonly IImageService _imageService;

		public UpdateAcademicianProfileCommandHandler(IAcademicianCommandRepository academicianCommandRepository, UserManager<AppUser> userManager, IImageService imageService)
		{
			_academicianCommandRepository = academicianCommandRepository;
			_userManager = userManager;
			_imageService = imageService;
		}

		public async Task<UpdateAcademicianResult> Handle(UpdateAcademicianProfileCommand request, CancellationToken cancellationToken)
		{
			// 1. Kullanıcıyı bul
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return new UpdateAcademicianResult { IsSuccess = false , Errors= new List<string> {"Kullanıcı bulunamadı."} };
				

			// 2. ŞİFRE DEĞİŞTİRME KONTROLÜ
			if (!string.IsNullOrEmpty(request.NewPassword))
			{
				if (string.IsNullOrEmpty(request.CurrentPassword))
					return new UpdateAcademicianResult { IsSuccess = false ,Errors = new List<string> { "Şifrenizi değiştirmek için mevcut şifrenizi girmelisiniz." } };

				var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
				if (!passwordChangeResult.Succeeded)
				{
					
					return new UpdateAcademicianResult { IsSuccess = false , Errors = passwordChangeResult.Errors.Select(e =>e.Description).ToList() };
					
				}
			}

			// 3. FOTOĞRAF YÜKLEME VE WEBP DÖNÜŞÜMÜ
			if (request.PhotoStream != null && request.PhotoStream.Length > 0)
			{
				// Ortak servisimizi çağırıyoruz, resmi WebP yapıp kaydediyor ve bize URL'i dönüyor
				string photoUrl = await _imageService.SaveImageAsWebpAsync(request.PhotoStream, "profiles");

				if (!string.IsNullOrEmpty(user.ProfilePhoto))
				{
					await _imageService.DeleteImageAsync(user.ProfilePhoto);
				}

				user.ProfilePhoto = photoUrl;
			}

			// 4. APPUSER (IDENTITY) TABLOSUNU GÜNCELLE
			user.Email = request.Email;
			user.PhoneNumber = request.PhoneNumber;
			user.FullName = request.FullName;

			var identityResult = await _userManager.UpdateAsync(user);
			if (!identityResult.Succeeded)
			{
				return new UpdateAcademicianResult {IsSuccess=false , Errors =identityResult.Errors.Select(e =>e.Description).ToList() };

			}


			// 5. ACADEMICIANPROFILE TABLOSUNU GÜNCELLE
			var academicianProfile = await _academicianCommandRepository.GetByUserIdAsync(user.Id,cancellationToken);
		

			if (academicianProfile != null)
			{
				academicianProfile.FullName = request.FullName;
				academicianProfile.Title = request.Title;
				academicianProfile.University = request.University;
				academicianProfile.Department = request.Department;
				academicianProfile.About = request.About;

			   _academicianCommandRepository.UpdateAcademicanProfile(academicianProfile);
				await _academicianCommandRepository.SaveChangesAsync(cancellationToken);
			}

			return new UpdateAcademicianResult { IsSuccess = true };
		}
	}
}
