using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Listings.Commands.CreateListing
{
	public class CreateListingCommandHandler : IRequestHandler<CreateListingCommand, bool>
	{
		private readonly IProjectCommandRepository _repository;
		private readonly IImageService _imageService;
		private readonly INotificationCommandRepository _notificationCommandRepository;

		public CreateListingCommandHandler(IProjectCommandRepository repository, IImageService imageService, INotificationCommandRepository notificationCommandRepository)
		{
			_repository = repository;
			_imageService = imageService;
			_notificationCommandRepository = notificationCommandRepository;
		}

		public async Task<bool> Handle(CreateListingCommand request, CancellationToken cancellationToken)
		{

			string imageUrl = await _imageService.SaveImageAsWebpAsync(request.ImageStream, "listings");


			var project = new Project
			{
				PublishedById = request.PublishedById,
				CategoryType = request.CategoryType,
				ListingType = request.ListingType,
				Title = request.Title,
				Description = request.Description,
				WorkType = request.WorkType,
				SalaryOrBudget = request.SalaryOrBudget,
				Deadline = request.Deadline,
				Image = imageUrl,
				Status = true, // İlan anında yayına girsin
				CreatedAt = DateTime.UtcNow
			};

			await _repository.AddAsync(project, cancellationToken);
			await _repository.SaveChangesAsync(cancellationToken);


			var notification = new Notification
			{

				UserId = request.PublishedById,
				Title = "Başarılı! ",
				Message = $"{request.Title} isimli ilanınız başarıyla yayınlandı!",
				Type = NotificationType.Success,
				RedirectUrl = $"/Listings/Details/{project.Id}",
				CreatedAt = DateTime.UtcNow,
				IsRead = false

			};

			await _notificationCommandRepository.AddNotificationAsync(notification, cancellationToken);




			return true;
		}
	}
}
