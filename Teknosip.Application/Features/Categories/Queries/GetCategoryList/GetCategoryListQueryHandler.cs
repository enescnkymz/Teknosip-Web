using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Repositories;

namespace Teknosip.Application.Features.Categories.Queries.GetCategoryList
{
	public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, List<CategoryDto>>
	{

		ICategoryQueryRepository _categoryRepository;

		public GetCategoryListQueryHandler(ICategoryQueryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public async Task<List<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
		{
			// 1. Depodan Dapper ile şimşek hızında verileri çek!
			var kategoriler = await _categoryRepository.GetAllAsync();

			// 2. Saf Varlıkları (Entity), vitrin modeline (DTO) MANUEL olarak çevir. (AutoMapper'dan %300 daha hızlı çalışır)
			var dtoList = kategoriler.Select(k => new CategoryDto
			{
				Id = k.Id,
				Ad = k.Name
			}).ToList();

			// 3. Postacıya DTO listesini teslim et
			return dtoList;
		}
	}
}
