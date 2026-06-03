using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Interfaces;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Listings.Commands.DeleteListing
{
	public class DeleteListingCommandHandler : IRequestHandler<DeleteListingCommand, bool>
	{
		private readonly IProjectCommandRepository _repository;
		private readonly IImageService _imageService;

		public DeleteListingCommandHandler(IProjectCommandRepository repository, IImageService imageService)
		{
			_repository = repository;
			_imageService = imageService;
		}

		public async Task<bool> Handle(DeleteListingCommand request, CancellationToken cancellationToken)
		{
			var project = await _repository.GetByIdAsync(request.Id, cancellationToken);

			
			if (project == null || project.PublishedById != request.CompanyId)
			{
				return false;
			}

			
			if (!string.IsNullOrEmpty(project.Image))
			{
				await _imageService.DeleteImageAsync(project.Image);
			}

			_repository.Delete(project);

			await _repository.SaveChangesAsync(cancellationToken);

			return true;
		}
	}

}
