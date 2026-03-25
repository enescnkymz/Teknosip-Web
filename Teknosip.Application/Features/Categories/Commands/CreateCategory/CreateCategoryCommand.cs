using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Categories.Commands.CreateCategory
{
	// IRequest<int> demek: "Ey MediatR postacısı, bu mektup işlendiğinde bana geriye int (yeni eklenen ID) döndür."
	public class CreateCategoryCommand : IRequest<int>
	{
		// Kullanıcıdan sadece bu bilgi gelecek
		public string Ad { get; set; }
	}
}
