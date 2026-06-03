using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.StudentProfile.Models;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.StudentProfile.Commands.UpdateStudentProfile
{

	public class UpdateStudentProfileCommandHandler : IRequestHandler<UpdateStudentProfileCommand, UpdateStudentResult>
	{
		private readonly IStudentCommandRepository _studentCommandRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly IImageService _imageService;

		public UpdateStudentProfileCommandHandler(IStudentCommandRepository studentCommandRepository, UserManager<AppUser> userManager, IImageService imageService)
		{
			_studentCommandRepository = studentCommandRepository;
			_userManager = userManager;
			_imageService = imageService;
		}

		public async Task<UpdateStudentResult> Handle(UpdateStudentProfileCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
				return new UpdateStudentResult { IsSuccess = false, Errors = new List<string> { "Kullanıcı bulunamadı." } };

			// ŞİFRE DEĞİŞTİRME
			if (!string.IsNullOrEmpty(request.NewPassword))
			{
				if (string.IsNullOrEmpty(request.CurrentPassword))
					return new UpdateStudentResult { IsSuccess = false, Errors = new List<string> { "Şifrenizi değiştirmek için mevcut şifrenizi girmelisiniz." } };

				var passwordChangeResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
				if (!passwordChangeResult.Succeeded)
				{
					return new UpdateStudentResult { IsSuccess = false, Errors = passwordChangeResult.Errors.Select(e => e.Description).ToList() };
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
			user.FullName = request.FullName;

			var identityResult = await _userManager.UpdateAsync(user);
			if (!identityResult.Succeeded)
			{
				return new UpdateStudentResult { IsSuccess = false, Errors = identityResult.Errors.Select(e => e.Description).ToList() };
			}

			// STUDENT PROFILE GÜNCELLE
			var studentProfile = await _studentCommandRepository.GetByUserIdAsync(user.Id, cancellationToken);
			if (studentProfile != null)
			{
				studentProfile.FullName = request.FullName;
				studentProfile.University = request.University;
				studentProfile.Department = request.Department;
				studentProfile.StudentNumber = request.StudentNumber;
				studentProfile.Grade = request.Grade;
				studentProfile.About = request.About;

				_studentCommandRepository.UpdateStudentProfile(studentProfile);
				await _studentCommandRepository.SaveAsync(cancellationToken);
			}

			return new UpdateStudentResult { IsSuccess = true };
		}
	}

}
