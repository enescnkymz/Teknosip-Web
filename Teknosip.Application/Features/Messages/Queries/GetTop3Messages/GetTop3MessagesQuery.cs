using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teknosip.Application.Features.Messages.Models;

namespace Teknosip.Application.Features.Messages.Queries.GetTop3Messages
{
	public class GetTop3MessagesQuery : IRequest<MessageDropwdownDto>
	{
        public Guid UserId { get; set; }
    }
}
