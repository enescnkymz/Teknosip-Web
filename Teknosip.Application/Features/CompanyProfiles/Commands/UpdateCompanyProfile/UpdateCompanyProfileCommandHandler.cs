using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.CompanyProfiles.Models;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.CompanyProfiles.Commands.UpdateCompanyProfile
{
	public class UpdateCompanyProfileCommandHandler : IRequestHandler<UpdateCompanyProfileCommand, UpdateCompanyResult>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly ICompanyCommandRepository _repository;
		private readonly IImageService _imageService;

		public UpdateCompanyProfileCommandHandler(UserManager<AppUser> userManager, ICompanyCommandRepository repository, IImageService imageService)
		{
			_userManager = userManager;
			_repository = repository;
			_imageService = imageService;
		}

		public async Task<UpdateCompanyResult> Handle(UpdateCompanyProfileCommand request, CancellationToken cancellationToken)
		{
			var result = new UpdateCompanyResult { IsSuccess = false, Errors = new List<string>() };

			// 1. AppUser'ı bul
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null)
			{
				result.Errors.Add("Kullanıcı bulunamadı.");
				return result;
			}

			// 2. Şirket Profilini Bul
			var companyProfile = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);
			if (companyProfile == null)
			{
				result.Errors.Add("Şirket profili bulunamadı.");
				return result;
			}

			// 3. AppUser Bilgilerini Güncelle (Ad Soyad, Telefon vs)
			user.FullName = request.CompanyName;
			user.PhoneNumber = request.PhoneNumber;


			//  FOTOĞRAF YÜKLEME VE WEBP DÖNÜŞÜMÜ
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


			// Şifre Değiştirme Kontrolü
			if (!string.IsNullOrEmpty(request.CurrentPassword) && !string.IsNullOrEmpty(request.NewPassword))
			{
				var pwdResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
				if (!pwdResult.Succeeded)
				{
					result.Errors.AddRange(pwdResult.Errors.Select(e => e.Description));
					return result;
				}
			}

			await _userManager.UpdateAsync(user);

			// 4. Şirket Spesifik Bilgileri Güncelle
			companyProfile.CompanyName = request.CompanyName;
			companyProfile.TaxNumber = request.TaxNumber;
			companyProfile.Sector = request.Sector;
			companyProfile.FoundedYear = request.FoundedYear;
			companyProfile.EmployeeCount = request.EmployeeCount;
			companyProfile.City = request.City;
			companyProfile.Address = request.Address;
			companyProfile.Website = request.Website;
			companyProfile.About = request.About;

			_repository.UpdateCompanyProfile(companyProfile);
			await _repository.SaveAsync(cancellationToken);

			result.IsSuccess = true;
			return result;

		}
	}
}
