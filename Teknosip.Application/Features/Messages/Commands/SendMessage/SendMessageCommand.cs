using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teknosip.Application.Features.Messages.Commands.SendMessage
{
	public class SendMessageCommand : IRequest<bool>
	{

		public Guid SenderId { get; set; }

		[Required(ErrorMessage = "Lütfen bir alıcı seçin.")]
		public Guid ReceiverId { get; set; }

		[Required(ErrorMessage = "Mesaj içeriği boş olamaz.")]
		[StringLength(2000, MinimumLength = 2, ErrorMessage = "Mesaj 2 ile 2000 karakter arasında olmalıdır.")]
		public string Content { get; set; }

	}
}
