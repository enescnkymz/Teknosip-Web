using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Categories.Queries.GetCategoryList
{
	// Diyoruz ki: "Bu zarf işlendiğinde geriye KategoriDto listesi dön!"
	public class GetCategoryListQuery : IRequest<List<CategoryDto>>
	{

	}
}
