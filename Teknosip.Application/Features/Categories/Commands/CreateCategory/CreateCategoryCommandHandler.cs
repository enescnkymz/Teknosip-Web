using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;
using Teknosip.Domain.Entities;

namespace Teknosip.Application.Features.Categories.Commands.CreateCategory
{
	// IRequestHandler<CreateKategoriCommand, int> demek: "Ben CreateKategoriCommand mektubunu işleyen ve geriye int dönen işçiyim."
	public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand , int>
	{
		// İşçimizin veritabanına ulaşmak için o imzaladığımız sözleşmeye (Depoya) ihtiyacı var
		ICategoryCommandRepository _categoryRepository;

		public CreateCategoryCommandHandler(ICategoryCommandRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		// Postacı mektubu getirdiğinde otomatik olarak bu Handle metodu tetiklenir!
		public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
		{
			// 1. Mektuptan (request) gelen verilerle saf Varlığımızı (Entity) oluşturuyoruz.
			var yeniKategori = new Category
			{
				Name = request.Ad
			};

			// 2. Sözleşmedeki depoya "Bunu ekle" diyoruz. (SQL kodunu hiç umursamıyoruz!)
			await _categoryRepository.AddAsync(yeniKategori);
			await _categoryRepository.SaveChangesAsync();

			// 3. İşlem bitti, SQL'in oluşturduğu yeni ID'yi geri dönüyoruz.
			return yeniKategori.Id;
	
		
		}

	}
}
