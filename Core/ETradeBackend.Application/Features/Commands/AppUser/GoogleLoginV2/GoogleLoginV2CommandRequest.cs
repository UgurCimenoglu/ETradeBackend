using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ETradeBackend.Application.Features.Commands.AppUser.GoogleLoginV2
{
    public class GoogleLoginV2CommandRequest : IRequest<GoogleLoginV2CommandResponse>
    {
        public string Code { get; set; }
    }
}
